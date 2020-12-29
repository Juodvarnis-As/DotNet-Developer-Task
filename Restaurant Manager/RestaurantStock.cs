using System;
using System.Collections.Generic;
using System.Text;

namespace DotNET_Developer_Task
{
    /// <summary>
    /// Container class in which the entire restaurant stock will be kept
    /// Additionally, methods for work with stock are implemented here
    /// </summary>
    public class RestaurantStock : IEntry<StockItem>
    {
        /// <summary>
        /// Array in which stock items are being held in memory
        /// </summary>
        private List<StockItem> StockItems = new List<StockItem>();


        /// <summary>
        /// Method for adding new item to restaurant stock
        /// </summary>
        /// <returns>
        /// Returns true if an item with specific ID does not exist
        /// Returns false if an item with specific ID does exist
        /// </returns>
        public bool AddNewEntry(StockItem item)
        {
            // If no items were found
            if (!DoesItemExistsByID(item.GetId()))
            {
                StockItems.Add(item);
                return true;
            }
            // If any items with the same Id are found
            else return false;
        }

        /// <summary>
        /// Method for updating a specific item in the list
        /// </summary>
        /// <param name="item">Updatable StockItem object</param>
        /// <returns>
        /// Returns true if successfully updated
        /// Returns false if there are any issues
        /// </returns>
        public bool UpdateItem(StockItem item)
        {
            try
            {
                int index = StockItems.FindIndex(p => p.GetId() == item.GetId());
                StockItems[index].UpdateElement(item);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        /// <summary>
        /// Method for checking if an item exists, by its Id
        /// </summary>
        /// <param name="id">Searchable Id</param>
        /// <returns>
        /// Returns true if the item does exist in stock
        /// Returns false if the item does not exist in stock
        /// </returns>
        public bool DoesItemExistsByID(int id)
        {
            var foundItems = StockItems.FindAll(p => p.GetId() == id);
            return foundItems.Count > 0 ? true : false;
        }

        /// <summary>
        /// Method for getting a StockItem object from restaurant stock
        /// </summary>
        /// <param name="id">Searchable Id</param>
        /// <returns>
        /// Returns StockItem object from the restaurant stock list
        /// Else returns null if such element does not exist
        /// </returns>
        public StockItem GetItemByID(int id)
        {
            return StockItems.FindLast(p => p.GetId() == id);
            
        }

        /// <summary>
        /// Method for removing a stock element from the stock by Id
        /// </summary>
        /// <param name="id">Removable Id</param>
        public void RemoveItem(int id)
        {
            StockItems.RemoveAll(p => p.GetId() == id);
        }

        /// <summary>
        /// Method for getting all stock items into one unified string
        /// </summary>
        /// <returns>Returns string of all elements</returns>
        public string ElementsToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(new string('-', 75) + "\r\n");
            sb.Append(" Stock Table\r\n");
            sb.Append(new string('-', 75) + "\r\n");
            sb.AppendFormat(" | {0, -3} | {1,-25} | {2, -14} | {3, -4} | {4, -12} |\r\n",
                    "Id", "Stock Item Name", "Portion Count", "Unit", "Portion Size");
            foreach (StockItem item in StockItems)
            {
                sb.AppendFormat(item.ToString());
                sb.Append("\r\n");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Method for getting count of items in container class
        /// </summary>
        /// <returns>Returns element count</returns>
        public int GetElementCount()
        {
            return StockItems.Count;
        }
    }
}
