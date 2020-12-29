using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotNET_Developer_Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_Manager;
using System.IO;

namespace DotNET_Developer_Task.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        /// <summary>
        /// Path to test restaurant stock file
        /// </summary>
        const string TestStockCsv = @"../../TestingFiles/stock.csv";
        /// <summary>
        /// Path to original restaurant stock file
        /// </summary>
        const string OriginalStockCsv = @"../../TestingFiles/stockOriginal.csv";
        /// <summary>
        /// Path to test restaurant menu file
        /// </summary>
        const string TestMenuCsv = @"../../TestingFiles/menu.csv";
        /// <summary>
        /// Path to original restaurant menu file
        /// </summary>
        const string OriginalMenuCsv = @"../../TestingFiles/menuOriginal.csv";
        /// <summary>
        /// Path to test restaurant orders file
        /// </summary>
        const string TestOrdersCsv = @"../../TestingFiles/orders.csv";
        /// <summary>
        /// Path to original restaurant menu file
        /// </summary>
        const string OriginalOrdersCsv = @"../../TestingFiles/ordersOriginal.csv";

        private FileInterface fileInterface = new FileInterface();

        static RestaurantStock stock = new RestaurantStock();

        static RestaurantMenu menu = new RestaurantMenu();

        static RestaurantOrders orders = new RestaurantOrders();

        /// <summary>
        /// File reading test method
        /// </summary>
        [TestMethod()]
        public void InitializeReadingTest()
        {
            RedoFiles();
            /// Read Stock file elements
            stock = fileInterface.ReadStockFile(TestStockCsv);
            Assert.AreEqual(5, stock.GetElementCount());
            /// Read Menu file elements
            menu = fileInterface.ReadMenuFile(TestMenuCsv);
            Assert.AreEqual(3, menu.GetElementCount());
            orders = fileInterface.ReadOrdersFile(TestOrdersCsv, menu);
            Assert.AreEqual(5, orders.GetElementCount());

        }

        /// <summary>
        /// Method for recreating test files from backup
        /// Used in order to get consistent results for testing
        /// </summary>
        private void RedoFiles()
        {
            File.Copy(OriginalStockCsv, TestStockCsv, true);
            File.Copy(OriginalMenuCsv, TestMenuCsv, true);
            File.Copy(OriginalOrdersCsv, TestOrdersCsv, true);
        }

        /// <summary>
        /// New stock item addition test method
        /// </summary>
        [TestMethod()]
        public void AddNewStockItem()
        {
            /// Read elements from list
            InitializeReadingTest();
            /// Addable elements
            StockItem addableStockItem = new StockItem(6, "Chicken Wings", 2.0, "kg", 0.4);
            /// Check if items are added in array
            Program.AddElement(stock, addableStockItem, TestStockCsv);
            Assert.AreEqual(6, stock.GetElementCount());
            /// Check if items are stored in csv file
            RestaurantStock newStock = fileInterface.ReadStockFile(TestStockCsv);
            Assert.AreEqual(6, newStock.GetElementCount());
           
        }

        /// <summary>
        /// New menu item addition test method
        /// </summary>
        [TestMethod()]
        public void AddNewMenuItem()
        {
            /// Read elements from list
            InitializeReadingTest();
            /// Addable elements
            MenuItem addableMenuItem = new MenuItem(4, "Grilled Chicken Wings with fries",
                new List<int> { 6, 1, 3 });

            /// Check if items are added in array
            Program.AddElement(menu, addableMenuItem, TestMenuCsv);
            Assert.AreEqual(4, menu.GetElementCount());
            /// Check if items are stored in csv file
            RestaurantMenu newMenu = fileInterface.ReadMenuFile(TestMenuCsv);
            Assert.AreEqual(4, newMenu.GetElementCount());
           
        }

        /// <summary>
        /// New successful order creation test method
        /// </summary>
        [TestMethod()]
        public void CreateNewOrder_Successful()
        {
            /// Read elements from list
            InitializeReadingTest();
            OrderItem newOrder = new OrderItem(6, DateTime.Now,
                new List<MenuItem> { menu.GetItemByID(1) });

            /// Check if items are added to array
            Program.AddElement(newOrder, TestOrdersCsv, TestStockCsv, stock, orders);
            Assert.AreEqual(6, orders.GetElementCount());
            /// Check if order is existing in file
            RestaurantOrders newOrders = fileInterface.ReadOrdersFile(TestOrdersCsv, menu);
            RestaurantStock newStock = fileInterface.ReadStockFile(TestStockCsv);
            Assert.AreEqual(4.7, newStock.GetItemByID(2).GetPortionCount());
            Assert.AreEqual(6, newOrders.GetElementCount());
            
        }

        /// <summary>
        /// New unsuccessful order creation test method
        /// </summary>
        [TestMethod()]
        public void CreateNewOrder_Unsuccesful()
        {
            /// Read elements from list
            InitializeReadingTest();
            OrderItem newOrder = new OrderItem(6, DateTime.Now,
                new List<MenuItem> { menu.GetItemByID(3) });
            /// Check if items are added to array
            /// Should fail as there is not enough lamb in stock
            Program.AddElement(newOrder, TestOrdersCsv, TestStockCsv, stock, orders);
            Assert.AreEqual(5, orders.GetElementCount());
            /// Check if order was not written in file
            RestaurantOrders newOrders = fileInterface.ReadOrdersFile(TestOrdersCsv, menu);
            Assert.AreEqual(5, newOrders.GetElementCount());
        }

        /// <summary>
        /// Stock item update test method
        /// </summary>
        [TestMethod()]
        public void UpdateStockItem()
        {
            /// Read elements from list
            InitializeReadingTest();
            StockItem updatedItem = new StockItem(3, "Ketchup", 2000, "ml", 50);
            /// Update stock items proportion
            Program.UpdateElement(stock, updatedItem, TestStockCsv);
            Assert.AreEqual(2000, stock.GetItemByID(3).GetPortionCount());
            /// Check if writing is successful to file
            RestaurantStock newStock = fileInterface.ReadStockFile(TestStockCsv);
            Assert.AreEqual(2000, newStock.GetItemByID(3).GetPortionCount());
          

        }

        /// <summary>
        /// New menu item update method
        /// </summary>
        [TestMethod()]
        public void UpdateMenuItem()
        {
            /// Read elements from list
            InitializeReadingTest();
            MenuItem updatedItem = new MenuItem(2, "Cooked Pork Chop with fries and coleslaw",
                new List<int> { 1, 2, 3, 4 });
            /// Update menu item
            Program.UpdateElement(menu, updatedItem, TestMenuCsv);
            Assert.AreEqual("Cooked Pork Chop with fries and coleslaw",
                menu.GetItemByID(2).GetItemName());
            List<int> ingredientList = new List<int> { 1, 2, 3, 4 };
            CollectionAssert.AreEqual(ingredientList, menu.GetItemByID(2).GetProducts());
            /// Check if writing is successful to file
            RestaurantMenu newMenu = fileInterface.ReadMenuFile(TestMenuCsv);
            Assert.AreEqual("Cooked Pork Chop with fries and coleslaw",
                newMenu.GetItemByID(2).GetItemName());
            CollectionAssert.AreEqual(ingredientList, newMenu.GetItemByID(2).GetProducts());
           
            
        }

        /// <summary>
        /// Element removal from stock test method
        /// </summary>
        [TestMethod()]

        public void RemoveStockItem()
        {
            /// Read elements from list
            InitializeReadingTest();
            StockItem removableItem = stock.GetItemByID(2);
            int removableId = removableItem.GetId();
            /// Remove item from list
            Program.RemoveElement(TestStockCsv, stock, removableId);
            Assert.IsFalse(stock.DoesItemExistsByID(removableId));
            RestaurantStock newStock = fileInterface.ReadStockFile(TestStockCsv);
            Assert.IsFalse(newStock.DoesItemExistsByID(removableId));
        }

        [TestMethod()]
        public void RemoveMenuItem_Successful()
        {
            /// Read elements from list
            InitializeReadingTest();
            MenuItem removableItem = menu.GetItemByID(1);
            int removableId = removableItem.GetId();
            /// Remove item from list
            Program.RemoveElement(TestMenuCsv, menu, removableId, orders);
            Assert.IsFalse(menu.DoesItemExistsByID(removableId));
            RestaurantMenu newMenu = fileInterface.ReadMenuFile(TestMenuCsv);
            Assert.IsFalse(newMenu.DoesItemExistsByID(removableId));
        }

        [TestMethod()]
        public void RemoveMenuItem_Unsuccessful()
        {
            /// Read elements from list
            InitializeReadingTest();
            MenuItem removableItem = menu.GetItemByID(2);
            int removableId = removableItem.GetId();
            /// Remove item from list
            Program.RemoveElement(TestMenuCsv, menu, removableId, orders);
            /// Removal would be unsuccessful, so checking if item does exist
            Assert.IsTrue(menu.DoesItemExistsByID(removableId));
        }
    }
}