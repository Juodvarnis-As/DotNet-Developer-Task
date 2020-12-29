using System;
using System.Collections.Generic;
using System.Text;

namespace DotNET_Developer_Task
{
    /// <summary>
    /// Class for holding order items and the methods
    /// </summary>
    public class OrderItem : ParentItem
    {
        /// <summary>
        /// DateTime object, for saving order placement time
        /// </summary>
        private DateTime dateTime;

        /// <summary>
        /// List of menu items in the order
        /// </summary>
        private List<MenuItem> menuItems;

        /// <summary>
        /// Constructor for OrderItem class
        /// </summary>
        /// <param name="Id">Id of the item</param>
        /// <param name="dateTime">Date and time of created order</param>
        /// <param name="menuItems">List of menu items in order</param>
        public OrderItem(int Id, DateTime dateTime, List<MenuItem> menuItems)
        {
            SetId(Id);
            this.dateTime = dateTime;
            this.menuItems = menuItems;
        }

        /// <summary>
        /// Method for putting new items into order
        /// </summary>
        /// <param name="items">Puttable item list</param>
        public void AddItemsToOrder(List<MenuItem> items)
        {
            foreach (MenuItem item in items)
            {
                menuItems.Add(item);
            }
        }

        /// <summary>
        /// Method for putting one item into order
        /// </summary>
        /// <param name="item">Puttable item</param>
        public void AddItemsToOrder(MenuItem item)
        {
            menuItems.Add(item);
        }

        /// <summary>
        /// ToString method, used for printing orders
        /// </summary>
        /// <returns>Returns order information in string format</returns>
        public override string ToString()
        {
            try
            {
                string MenuIds = "";
                foreach (MenuItem item in menuItems)
                {
                    MenuIds += item.GetId() + " ";
                }

                return String.Format(" | {0, -3} | {1,-20} | {2, -14} |",
                        GetId(), dateTime, MenuIds);
            }
            catch (NullReferenceException)
            {
                return String.Format(" >> Error reading Id {0} entry. " +
                    "Ordered item does not exist in the menu.", GetId());
            }
        }

        /// <summary>
        /// Method for getting single order items in the menu
        /// </summary>
        /// <returns>
        /// Returns a List object with product Ids, used in single order
        /// Which will be later used to deduct ingredients
        /// </returns>
        public List<int> GetProductIds()
        {
            List<int> menuIds = new List<int>();

            foreach (MenuItem item in menuItems)
            {
                List<int> products = item.GetProducts();
                foreach (int i in products)
                {
                    menuIds.Add(i);
                }
            }

            return menuIds;
        }

        /// <summary>
        /// Method for returning menu items
        /// </summary>
        /// <returns>Returns menu items in order</returns>
        public List<MenuItem> GetMenuItems()
        {
            return menuItems;
        }

        /// <summary>
        /// Method for returning a string about order item in CSV format
        /// </summary>
        /// <returns>Returns order item data as CSV string</returns>
        public string ToCsvFormat()
        {
            try
            {
                string MenuIds = "";
                foreach (MenuItem item in menuItems)
                {
                    MenuIds += item.GetId() + " ";
                }

                return String.Format("{0},{1},{2}",
                        GetId(), dateTime, MenuIds);
            }
            catch (NullReferenceException)
            {
                return String.Format(" >> Error reading Id {0} entry. " +
                    "Ordered item does not exist in the menu.", GetId());
            }
        }
    }
}
