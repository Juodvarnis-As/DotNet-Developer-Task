using DotNET_Developer_Task;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Manager
{
    /// <summary>
    /// Class for working with given csv files
    /// Has required read and write methods, for adding, updating and deleting elements
    /// </summary>
    public class FileInterface
    {
        /// <summary>
        /// Method for reading restaurant stock file from given path
        /// </summary>
        /// <param name="path">String Path to file</param>
        /// <returns>Returns RestaurantStock object with read data from file</returns>
        public RestaurantStock ReadStockFile(string path)
        {
            RestaurantStock stock = new RestaurantStock();
            /// Check if stock item file exists
            /// If not, create it with headers
            if (!File.Exists(path))
            {
                using (StreamWriter wr = new StreamWriter(path))
                {
                    wr.WriteLine("Id,Name,Portion Count,Unit,Portion Size");
                    wr.Close();
                }
            }
            /// Else read the file
            else
            {
                using (StreamReader rd = new StreamReader(path))
                {
                    /// Read header
                    string line = rd.ReadLine();
                    while ((line = rd.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');
                        int id = int.Parse(data[0]);
                        string name = data[1];
                        double portionCount = double.Parse(data[2]);
                        string unit = data[3];
                        double portionSize = double.Parse(data[4]);
                        StockItem item = new StockItem(id, name, portionCount, unit, portionSize);
                        stock.AddNewEntry(item);

                    }
                    rd.Close();
                }
            }
            return stock;
        }

        /// <summary>
        /// Method for reading menu file from given path
        /// </summary>
        /// <param name="path">Path to menu file</param>
        /// <returns>Returns RestaurantMenu object with read data from file</returns>
        public RestaurantMenu ReadMenuFile(string path)
        {
            RestaurantMenu menu = new RestaurantMenu();
            /// Check if menu item file exists
            /// If not, create it with headers
            if (!File.Exists(path))
            {
                using (StreamWriter wr = new StreamWriter(path))
                {
                    wr.WriteLine("Id,Name,Products");
                    wr.Close();
                }
            }
            /// Else read the file
            else
            {
                using (StreamReader rd = new StreamReader(path))
                {
                    /// Read header
                    string line = rd.ReadLine();
                    while ((line = rd.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');
                        int id = int.Parse(data[0]);
                        string name = data[1];
                        var productsLine = data[2].Trim().Split(' ');
                        List<int> products = new List<int>();
                        foreach (string i in productsLine)
                        {
                            products.Add(int.Parse(i));
                        }
                        MenuItem item = new MenuItem(id, name, products);
                        menu.AddNewEntry(item);

                    }
                    rd.Close();
                }
            }
            return menu;
        }

        /// <summary>
        /// Method for reading restaurant orders csv file
        /// </summary>
        /// <param name="path">Path to orders file</param>
        /// <param name="menu">Reference of previously read RestaurantMenu object</param>
        /// <returns>Returns RestaurantOrders object with read data from file</returns>
        public RestaurantOrders ReadOrdersFile(string path, RestaurantMenu menu)
        {
            RestaurantOrders orders = new RestaurantOrders();
            if (!File.Exists(path))
            {
                using (StreamWriter wr = new StreamWriter(path))
                {
                    wr.WriteLine("Id,DateTime,Menu Items");
                    wr.Close();
                }
            }
            /// Else read file
            else
            {
                using (StreamReader rd = new StreamReader(path))
                {
                    /// Read header
                    string line = rd.ReadLine();
                    while ((line = rd.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');
                        int id = int.Parse(data[0]);
                        DateTime date = DateTime.Parse(data[1]);
                        var productsLine = data[2].Trim().Split(' ');
                        List<MenuItem> items = new List<MenuItem>();
                        foreach (string i in productsLine)
                        {
                            int menuItem = int.Parse(i);
                            items.Add(menu.GetItemByID(menuItem));
                        }
                        OrderItem item = new OrderItem(id, date, items);
                        orders.AddNewEntry(item);

                    }
                    rd.Close();
                }
            }
            return orders;
        }

        /// <summary>
        /// Method for adding an element to specified file, adding element at the last row
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <param name="writable">StockItem object, which is written into file</param>
        public void AddEntryToFile(string path, StockItem writable)
        {
            using (StreamWriter wr = new StreamWriter(path, true))
            {
                wr.WriteLine(writable.ToCsvFormat());
            }
        }

        /// <summary>
        /// Method for adding an element to specified file, adding element at the last row
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <param name="writable">MenuItem object, which is written into file</param>
        public void AddEntryToFile(string path, MenuItem writable)
        {
            using (StreamWriter wr = new StreamWriter(path, true))
            {
                wr.WriteLine(writable.ToCsvFormat());
            }
        }

        /// <summary>
        /// Method for adding an element to specified file, adding element at the last row
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <param name="writable">OrderItem object, which is written into file</param>
        public void AddEntryToFile(string path, OrderItem writable)
        {
            using (StreamWriter wr = new StreamWriter(path, true))
            {
                wr.WriteLine(writable.ToCsvFormat());
            }
        }

        /// <summary>
        /// Method for deleting a line from file
        /// Not the greatest method, as it can crash when working with large files
        /// However, the only one currently acceptable
        /// </summary>
        /// <param name="path">File path from which remove element</param>
        /// <param name="Id">Removable element Id</param>
        public void DeleteEntryFromFile(string path, int Id)
        { 
            var fileLines = File.ReadAllLines(path).ToList<string>();
            fileLines.RemoveAll(p => p.Split(',')[0].Equals(Id.ToString()));

            using (var rd = new StreamWriter(path))
            {
                foreach (string line in fileLines)
                {
                    rd.WriteLine(line);
                }
            }

        }

        /// <summary>
        /// Method for updating stock list with new entries
        /// </summary>
        /// <param name="path">File path for stock file</param>
        /// <param name="updatableItems">List of updatable items</param>
        public void UpdateEntryInFile(string path, List<StockItem> updatableItems)
        {
            var fileLines = File.ReadAllLines(path).ToList<string>();
            foreach (StockItem updatable in updatableItems)
            {
                int index = fileLines.FindIndex(p => p.Split(',')[0].Equals(updatable.GetId().ToString()));
                fileLines[index] = updatable.ToCsvFormat();
            }
            using (var rd = new StreamWriter(path))
            {
                foreach (string line in fileLines)
                {
                    rd.WriteLine(line);
                }
            }
        }

        /// <summary>
        /// Method for updating a single entry in restaurant stock file
        /// </summary>
        /// <param name="path">File path to restaurant stock file</param>
        /// <param name="updatable">Updatable entry as StockItem object</param>
        public void UpdateEntryInFile(string path, StockItem updatable)
        {
            var fileLines = File.ReadAllLines(path).ToList<string>();
            int index = fileLines.FindIndex(p => p.Split(',')[0].Equals(updatable.GetId().ToString()));
            fileLines[index] = updatable.ToCsvFormat();

            using (var rd = new StreamWriter(path))
            {
                foreach (string line in fileLines)
                {
                    rd.WriteLine(line);
                }
            }

        }

        /// <summary>
        /// Method for updating a single entry in restaurant menu file
        /// </summary>
        /// <param name="path">File path to restaurant stock file</param>
        /// <param name="updatable">Updatable entry as MenuItem object</param>
        public void UpdateEntryInFile(string path, MenuItem updatable)
        {
            var fileLines = File.ReadAllLines(path).ToList<string>();
            int index = fileLines.FindIndex(p => p.Split(',')[0].Equals(updatable.GetId().ToString()));
            fileLines[index] = updatable.ToCsvFormat();

            using (var rd = new StreamWriter(path))
            {
                foreach (string line in fileLines)
                {
                    rd.WriteLine(line);
                }
            }

        }
    }
}
