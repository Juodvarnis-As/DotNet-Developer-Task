using System;
using System.Collections.Generic;
using System.Text;

namespace DotNET_Developer_Task
{
    /// <summary>
    /// Container class for restaurant menu and its methods
    /// </summary>
    public class RestaurantMenu : IEntry<MenuItem>
    {
        /// <summary>
        /// List of all restaurant menu entries
        /// </summary>
        private List<MenuItem> menuItems = new List<MenuItem>();

        /// <summary>
        /// Method for adding a new menu entry into restaurant menu
        /// </summary>
        /// <param name="item">Addable entry</param>
        /// <returns>
        /// Returns true if successfully added
        /// Returns false if not added
        /// </returns>
        public bool AddNewEntry(MenuItem item)
        {
            if (!DoesItemExistsByID(item.GetId()))
            {
                menuItems.Add(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method for checking if an item exists in menu by its Id
        /// </summary>
        /// <returns>
        /// Returns true if the item exists
        /// Returns false if the item does not exist
        /// </returns>
        public bool DoesItemExistsByID(int id)
        {
            var foundItems = menuItems.FindAll(p => p.GetId() == id);
            return foundItems.Count > 0 ? true : false;
        }

        /// <summary>
        /// Method for formatting all the menu entries into one unified string
        /// </summary>
        /// <returns>Returns unified menu items string</returns>
        public string ElementsToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(new string('-', 66) + "\r\n");
            sb.Append(" Menu Table\r\n");
            sb.Append(new string('-', 66) + "\r\n");
            sb.AppendFormat(" | {0, -3} | {1,-36} | {2, -16} |\r\n",
                    "Id", "Menu Item Name", "Products");
            foreach (MenuItem item in menuItems)
            {
                sb.AppendFormat(item.ToString());
                sb.Append("\r\n");
            }
            return sb.ToString();
        }


        /// <summary>
        /// Method for getting an element by Id from menu item list
        /// </summary>
        /// <param name="id">Seachable ID</param>
        /// <returns>
        /// Returns MenuItem element with specified Id
        /// Otherwise returns null
        /// </returns>
        public MenuItem GetItemByID(int id)
        {
            return menuItems.FindLast(p => p.GetId() == id);
        }

        /// <summary>
        /// Method for removing a menu element from the menu list by Id
        /// </summary>
        /// <param name="id"></param>
        public void RemoveItem(int id)
        {
            menuItems.RemoveAll(p => p.GetId() == id);
        }

        /// <summary>
        /// Method for updating a specific menu element with the new element
        /// </summary>
        /// <param name="obj">New menu element</param>
        /// <returns>
        /// Returns true if successfully changed the element
        /// Returns false if changes are unsucessful
        /// </returns>
        public bool UpdateItem(MenuItem obj)
        {
            try
            {
                int index = menuItems.FindIndex(p => p.GetId() == obj.GetId());
                menuItems[index].UpdateItems(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Method for getting count of items in container class
        /// </summary>
        /// <returns>Returns element count</returns>
        public int GetElementCount()
        {
            return menuItems.Count;
        }
    }
}
