using Restaurant_Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNET_Developer_Task
{

    public class Program
    {
        /// <summary>
        /// The location of stock items file
        /// </summary>
        const string StockCsv = @"../../stock.csv";

        /// <summary>
        /// The location of menu items file
        /// </summary>
        const string MenuCsv = @"../../menu.csv";

        /// <summary>
        /// The location of order items file
        /// </summary>
        const string OrdersCsv = @"../../orders.csv";

        /// <summary>
        /// RestaurantStock object, in which entire restaurant stock is saved
        /// </summary>
        private static RestaurantStock stock = new RestaurantStock();

        /// <summary>
        /// RestaurantMenu object, in which entire restaurant menu is saved
        /// </summary>
        private static RestaurantMenu menu = new RestaurantMenu();

        /// <summary>
        /// RestaurantOrders object, in which entire restaurant orders are saved
        /// </summary>
        private static RestaurantOrders orders = new RestaurantOrders();

        /// <summary>
        /// FileInterface object for altering files and data in them
        /// </summary>
        private static FileInterface fileInterface = new FileInterface();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding("Windows-1257");
            

            /// Display welcome message
            PrintWelcomeMessage();

            /// Checking if files exist
            InitializeReading();

        Entry:
            string command = Console.ReadLine().ToUpper();
            Console.WriteLine();
            ProcessCommandInput(command);
            goto Entry;

        }

        /// <summary>
        /// Method for printing welcome message to console window
        /// </summary>
        static private void PrintWelcomeMessage()
        {
            Console.WriteLine(" Program created by Aidas Senkevičius");
            Console.WriteLine(" Contacts: aidas.senkevicius@gmail.com");
            Console.WriteLine("\n\r Restaurant Manager Program");
            Console.WriteLine(" Enter Help for help message and commands.");
            Console.WriteLine(" Enter Exit for quiting this program.\r\n");
        }

        /// <summary>
        /// Method for checking if the files in specific path exists
        /// If not, creates an empty file with the specified name in path
        /// </summary>
        static public void InitializeReading()
        {
            try
            {
                stock = fileInterface.ReadStockFile(StockCsv);
                menu = fileInterface.ReadMenuFile(MenuCsv);
                orders = fileInterface.ReadOrdersFile(OrdersCsv, menu);
            }
            catch (IOException)
            {
                Console.WriteLine(" ERROR: Close open .csv files and try again!");
                Environment.Exit(0);
            }
        }

        static private void ProcessCommandInput(string commandLine)
        {
            /// Separate the line into different pieces
            string[] separatedLine = commandLine.Split(' ');

            
            switch (separatedLine[0])
            {
                case "ADD":
                    if (separatedLine.Length == 1)
                    {
                        Console.WriteLine("Input incorrect. Check input.");
                        break;
                    }
                    switch (separatedLine[1])
                    {
                        case "STOCK":
                            AddNewElement("STOCK");
                            break;
                        case "MENU":
                            AddNewElement("MENU");
                            break;
                        case "ORDERS":
                            AddNewElement("ORDERS");
                            break;
                        default:
                            Console.WriteLine("Input incorrect. Check input.");
                            break;
                    }
                    break;
                case "UPDATE":
                    if (separatedLine.Length == 1)
                    {
                        Console.WriteLine("Input incorrect. Check input.");
                        break;
                    }
                    switch (separatedLine[1])
                    {
                        case "STOCK":
                            UpdateElement("STOCK");
                            break;
                        case "MENU":
                            UpdateElement("MENU");
                            break;
                        case "ORDERS":
                            UpdateElement("ORDERS");
                            break;
                        default:
                            Console.WriteLine("Input incorrect. Check input.");
                            break;
                    }
                    break;
                case "DELETE":
                    if (separatedLine.Length == 1)
                    {
                        Console.WriteLine("Input incorrect. Check input.");
                        break;
                    }
                    switch (separatedLine[1])
                    {
                        case "STOCK":
                            RemoveElement("STOCK");
                            break;
                        case "MENU":
                            RemoveElement("MENU");
                            break;
                        default:
                            Console.WriteLine("Input incorrect. Check input.");
                            break;
                    }
                    break;
                case "VIEW":
                    if (separatedLine.Length == 1)
                    {
                        Console.WriteLine("Input incorrect. Check input.");
                        break;
                    }
                    switch (separatedLine[1])
                    {
                        case "STOCK":
                            Console.WriteLine(stock.ElementsToString());
                            break;
                        case "MENU":
                            Console.WriteLine(menu.ElementsToString());
                            break;
                        case "ORDERS":
                            Console.WriteLine(orders.ElementsToString());
                            break;
                        default:
                            Console.WriteLine("Input incorrect. Check input.");
                            break;
                    }
                    break;
                case "HELP":
                    DisplayHelpMessage();
                    break;
                case "CLEAR":
                    Console.Clear();
                    PrintWelcomeMessage();
                    break;
                case "EXIT":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Input incorrect. Check input");
                    break;
            }
        }

        /// <summary>
        /// Method for displaying help message with commands
        /// </summary>
        static private void DisplayHelpMessage()
        {
            Console.WriteLine("\n\r Possible commands in this program are:");
            Console.WriteLine("     Add [Stock, Menu, Orders]   - add new item in stock, menu or orders");
            Console.WriteLine("     Update [Stock, Menu]        - update an item in stock or menu");
            Console.WriteLine("     Delete [Stock, Menu]        - delete an item in stock or menu");
            Console.WriteLine("     View [Stock, Menu, Orders]  - view all items in stock, menu or orders");
            Console.WriteLine("     Help                        - display this message");
            Console.WriteLine("     Clear                       - clear console window");
            Console.WriteLine("     Exit                        - close this program");
        }

        /// <summary>
        /// Method for adding new elements to corresponding objects (Stock, Menu, Orders)
        /// </summary>
        /// <param name="table">Addable object name, given by switch case statement</param>
        static public void AddNewElement(string table)
        {
            try
            {
                /// Check if adding to stock table
                if (table.Equals("STOCK"))
                {
                    /// Read entry
                    Console.WriteLine(" Beginning input of new stock item...");
                RetryStock:
                    Console.Write(" Enter new items ID: ");
                    int newId = int.Parse(Console.ReadLine());

                    /// Check if there is an item with the same Id
                    if (stock.DoesItemExistsByID(newId))
                    {
                        Console.WriteLine(" Item with this ID exists. Enter a new ID.");
                        goto RetryStock;
                    }

                    Console.Write(" Input new items name: ");
                    string name = Console.ReadLine();
                    Console.Write(" Input new items portion count in stock: ");
                    double portionCount = double.Parse(Console.ReadLine());
                    Console.Write(" Input new items unit: ");
                    string unit = Console.ReadLine();
                    Console.Write(" Input new items portion size: ");
                    double portionSize = double.Parse(Console.ReadLine());

                    StockItem newItem = new StockItem(newId, name, portionCount, unit, portionSize);
                    AddElement(stock, newItem, StockCsv);
                }
                /// Check if adding to menu table
                else if (table.Equals("MENU"))
                {
                    /// Read entry
                    Console.WriteLine(" Beginning input of new menu item...");
                RetryMenu:
                    Console.Write(" Enter new items ID: ");
                    int newId = int.Parse(Console.ReadLine());

                    /// Check if there is an item with the same Id
                    if (menu.DoesItemExistsByID(newId))
                    {
                        Console.WriteLine(" Item with this ID exists. Enter a new ID.");
                        goto RetryMenu;
                    }

                    Console.Write(" Input new items name: ");
                    string name = Console.ReadLine();
                    Console.Write(" Input new items products IDs, separated by space: ");
                    string[] products = Console.ReadLine().Split(' ');
                    /// Parse product list 
                    List<int> productList = new List<int>();
                    foreach (string i in products)
                    {
                        productList.Add(int.Parse(i));
                    }
                    MenuItem item = new MenuItem(newId, name, productList);
                    AddElement(menu, item, MenuCsv);
                }
                /// Check if adding to orders table 
                else if (table.Equals("ORDERS"))
                {
                    Console.WriteLine(" Beginning input of new order item...");
                RetryOrder:
                    Console.Write(" Enter new order ID: ");
                    int newId = int.Parse(Console.ReadLine());

                    /// Check if there is an item with the same Id
                    if (orders.DoesItemExistsByID(newId))
                    {
                        Console.WriteLine(" Item with this ID exists. Enter a new ID.");
                        goto RetryOrder;
                    }

                    Console.Write(" Input date time, format YYYY-MM-DD HH:mm:ss, " +
                        "or leave blank for current time: ");
                    string line = Console.ReadLine();
                    DateTime time;
                    if (line.Equals(""))
                    {
                        time = DateTime.Now;
                    }
                    else
                    {
                        time = DateTime.Parse(line);
                    }

                RetryOrderItems:
                    /// Parse entered menu items
                    List<MenuItem> orderedItems = new List<MenuItem>();
                    Console.Write(" Input ordered menu items, separated by space: ");
                    string[] items = Console.ReadLine().Split(' ');
                    /// Checking all items...
                    foreach (string i in items)
                    {
                        int index = int.Parse(i);
                        MenuItem addable = menu.GetItemByID(index);
                        if (addable != null) orderedItems.Add(addable);
                        else
                        {
                            Console.WriteLine(" Error parsing menu item! Please check if it exists.");
                            goto RetryOrderItems;
                        }
                    }
                    OrderItem item = new OrderItem(newId, time, orderedItems);
                    /// Check if possible to add an order or shall it be cancelled
                    AddElement(item, OrdersCsv, StockCsv, stock, orders);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\r\n Incorrect numeric input! Please try again!");
                Console.WriteLine(" Stopping insertion...\r\n");
            }
            catch (IOException)
            {
                Console.WriteLine("\r\n One of the files is open! Close the .csv files and try again!");
                Console.WriteLine(" Stopping insertion...\r\n");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("\r\n An order contains menu items with no items in stock!");
                Console.WriteLine(" Stopping insertion...\r\n");
            }
        }

        /// <summary>
        /// Method for creating an order
        /// Separated from the rest in order to test
        /// </summary>
        /// <param name="newOrder">New order item</param>
        /// <param name="OrderPath">Path to orders csv file</param>
        /// <param name="StockPath">Path to stock csv file</param>
        /// <param name="orders">Orders object</param>
        /// <param name="stock">Stock object</param>
        static public void AddElement(OrderItem newOrder, string OrderPath, 
            string StockPath, RestaurantStock stock, RestaurantOrders orders)
        {
            List<int> productIds = newOrder.GetProductIds();
            /// Cycle trough items
            bool canBeAdded = true;
            foreach (int i in productIds)
            {
                /// Check if the component can be added
                if (stock.GetItemByID(i).CheckIfEnoughInStock())
                {
                    canBeAdded = true;
                }
                else
                {
                    canBeAdded = false;
                    break;
                }
            }

            if (!canBeAdded)
            {
                Console.WriteLine(" Error: There is not enough of product for this order!\r\n");
            }
            else if (orders.AddNewEntry(newOrder))
            {
                List<StockItem> updatedStock = new List<StockItem>();
                /// Reduce components
                foreach (int i in productIds)
                {
                    updatedStock.Add(stock.GetItemByID(i));
                    stock.GetItemByID(i).UpdateElement();
                }

                Console.WriteLine(" Item added succesfully\r\n");
                fileInterface.UpdateEntryInFile(StockPath, updatedStock);
                fileInterface.AddEntryToFile(OrderPath, newOrder);
            }
            else
            {
                Console.WriteLine(" Error: Item was not added\r\n");
            }
        }

       /// <summary>
       /// Method for adding a new MenuItem element into array and csv file
       /// </summary>
       /// <param name="menuRef">RestaurantMenu reference, to which new element will be added</param>
       /// <param name="item">New addable MenuItem element</param>
       /// <param name="filePath">Path to RestaurantMenu file</param>
        static public void AddElement(RestaurantMenu menuRef, MenuItem item, string filePath)
        {
            if (menuRef.AddNewEntry(item))
            {
                Console.WriteLine(" Item added succesfully\r\n");
                fileInterface.AddEntryToFile(filePath, item);
            }
            else
            {
                Console.WriteLine(" Error: Item was not added\r\n");
            }
        }

        /// <summary>
        /// Method for adding a new MenuItem element into array and csv file
        /// </summary>
        /// <param name="stockRef">RestaurantStock reference, to which new element will be added</param>
        /// <param name="item">New addable StockItem element</param>
        /// <param name="filePath">Path to RestaurantStock file</param>
        static public void AddElement(RestaurantStock stockRef, StockItem item, string filePath)
        {
            if (stockRef.AddNewEntry(item))
            {
                Console.WriteLine(" Item added succesfully\r\n");
                fileInterface.AddEntryToFile(filePath, item);
            }
            else
            {
                Console.WriteLine(" Error: Item was not added\r\n");
            }
        }

            /// <summary>
            /// Method for removing an element from the files
            /// </summary>
            /// <param name="table">
            /// Removable object name, given by switch case statement
            /// </param>
            static public void RemoveElement(string table)
        {
            try
            {
                if (table.Equals("STOCK"))
                {
                    Console.Write(" Enter Id of the element you want to remove: ");
                    int removableID = int.Parse(Console.ReadLine());
                    RemoveElement(StockCsv, stock, removableID);
                    
                }
                else if (table.Equals("MENU"))
                {
                    Console.Write(" Enter Id of the element you want to remove: ");
                    int removableID = int.Parse(Console.ReadLine());
                    RemoveElement(MenuCsv, menu, removableID, orders);
                    
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\r\n Incorrect numeric input! Please try again!");
                Console.WriteLine(" Stopping deletion...\r\n");
            }
            catch (IOException)
            {
                Console.WriteLine("\r\n One of the files is open! Close the .csv files and try again!");
                Console.WriteLine(" Stopping deletion...\r\n");
            }
        }

        /// <summary>
        /// Method for removing element from stock list and file
        /// </summary>
        /// <param name="filePath">Path to stock file</param>
        /// <param name="list">RestaurantStock object, where stock is saved in memory</param>
        /// <param name="removableID">Removable item id</param>
        static public void RemoveElement(string filePath, RestaurantStock list, int removableID)
        {

            if (list.DoesItemExistsByID(removableID))
            {
                list.RemoveItem(removableID);
                fileInterface.DeleteEntryFromFile(filePath, removableID);
                Console.WriteLine(" Item removed succesfully\r\n");
            }
            else
            {
                Console.WriteLine(" Element with specified ID does not exist!");
            }
        }

        /// <summary>
        /// Method for removing element from stock list and file
        /// </summary>
        /// <param name="filePath">Path to stock file</param>
        /// <param name="list">RestaurantStock object, where stock is saved in memory</param>
        /// <param name="removableID">Removable item id</param>
        /// <param name="orders">RestaurantOrders reference object, where orders are saved</param>
        static public void RemoveElement(string filePath, RestaurantMenu list, 
            int removableID, RestaurantOrders orderList)
        {

            /// Check if there are orders with this item, else removal is cancelled
            List<int> usedIn = orderList.GetAllOrderMenuItems();
            if (usedIn.Contains(removableID))
            {
                Console.WriteLine(" Cannot remove element as it exists in orders!");

            }
            else if (list.DoesItemExistsByID(removableID))
            {
                list.RemoveItem(removableID);
                fileInterface.DeleteEntryFromFile(filePath, removableID);
                Console.WriteLine(" Item removed succesfully\r\n");
            }
            else
            {
                Console.WriteLine(" Element with specified ID does not exist!");
            }
        }

        /// <summary>
        /// Method for updating a specific element in either stock or menu lists
        /// </summary>
        /// <param name="table">Updatable table name. Can be STOCK or MENU only.</param>
        static public void UpdateElement(string table)
        {
            try
            {
                if (table.Equals("STOCK"))
                {
                    Console.Write(" Enter Id of the element you want to update: ");
                    int updatableID = int.Parse(Console.ReadLine());
                    if (stock.DoesItemExistsByID(updatableID))
                    {
                        Console.Write(" Input new items name: ");
                        string name = Console.ReadLine();
                        Console.Write(" Input new items portion count in stock: ");
                        double portionCount = double.Parse(Console.ReadLine());
                        Console.Write(" Input new items unit: ");
                        string unit = Console.ReadLine();
                        Console.Write(" Input new items portion size: ");
                        double portionSize = double.Parse(Console.ReadLine());

                        StockItem newItem = new StockItem(updatableID, name, portionCount, unit, portionSize);
                        UpdateElement(stock, newItem, StockCsv);
                    }
                    else
                    {
                        Console.WriteLine(" Element with specified ID does not exist!");
                    }
                }
                else if (table.Equals("MENU"))
                {
                    Console.Write(" Enter Id of the element you want to update: ");
                    int updatableID = int.Parse(Console.ReadLine());
                    if (menu.DoesItemExistsByID(updatableID))
                    {
                        Console.Write(" Input new items name: ");
                        string name = Console.ReadLine();
                        Console.Write(" Input new items products IDs, separated by space: ");
                        string[] products = Console.ReadLine().Split(' ');
                        /// Parse product list 
                        List<int> productList = new List<int>();
                        foreach (string i in products)
                        {
                            productList.Add(int.Parse(i));
                        }
                        MenuItem item = new MenuItem(updatableID, name, productList);
                        UpdateElement(menu, item, MenuCsv);
                    }
                    else
                    {
                        Console.WriteLine(" Element with specified ID does not exist!");
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\r\n Incorrect numeric input! Please try again!");
                Console.WriteLine(" Stopping update...\r\n");
            }
            catch (IOException)
            {
                Console.WriteLine("\r\n One of the files is open! Close the .csv files and try again!");
                Console.WriteLine(" Stopping update...\r\n");
            }
        }

        /// <summary>
        /// Method for updating StockItem element in array and Restaurant stock file
        /// </summary>
        /// <param name="refStock">Reference to RestaurantStock object</param>
        /// <param name="item">Updated item</param>
        /// <param name="path">Path to Restaurant Stock file</param>
        public static void UpdateElement(RestaurantStock refStock, StockItem item, string path)
        {
            if (refStock.UpdateItem(item))
            {
                fileInterface.UpdateEntryInFile(path, item);
                Console.WriteLine(" Item was updated succesfully\r\n");
            }
            else
            {
                Console.WriteLine(" Item was not updated!\r\n");
            }
        }

        /// <summary>
        /// Method for updating MenuItem element in array and Restaurant Menu csv file
        /// </summary>
        /// <param name="refMenu">Reference to RestaurantMenu object</param>
        /// <param name="item">Updated item</param>
        /// <param name="path">Path to Restaurant Menu file</param>
        public static void UpdateElement(RestaurantMenu refMenu, MenuItem item, string path)
        {
            if (refMenu.UpdateItem(item))
            {
                fileInterface.UpdateEntryInFile(path, item);
                Console.WriteLine(" Item was updated succesfully\r\n");
            }
            else
            {
                Console.WriteLine(" Item was not updated!\r\n");
            }
        }
    }

    
}
