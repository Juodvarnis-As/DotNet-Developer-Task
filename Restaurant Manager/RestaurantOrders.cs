using System;
using System.Collections.Generic;
using System.Text;

namespace DotNET_Developer_Task
{
    /// <summary>
    /// Conteiner class for saving restaurant orders
    /// </summary>
    public class RestaurantOrders
    {
        /// <summary>
        /// List of orders in a restaurant
        /// </summary>
        private List<OrderItem> orders = new List<OrderItem>();

        /// <summary>
        /// Method for adding a new order into List
        /// </summary>
        /// <param name="newOrder">Addable order</param>
        /// <returns>
        /// Returns true if succesfully added
        /// Returns false if adding was unsuccessful
        /// </returns>
        public bool AddNewEntry(OrderItem newOrder)
        {
            if (!DoesItemExistsByID(newOrder.GetId()))
            {
                orders.Add(newOrder);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Method for retrieving all order unique menu elements
        /// Used in order not to delete menu items which have been ordered
        /// </summary>
        /// <returns></returns>
        public List<int> GetAllOrderMenuItems()
        {
            List<int> returnable = new List<int>();
            foreach (OrderItem order in orders)
            {
                foreach (MenuItem i in order.GetMenuItems())
                {
                    if (!returnable.Contains(i.GetId()))
                        returnable.Add(i.GetId());
                }
            }
            return returnable;
        }


        /// <summary>
        /// Method for checking if an item with the same Id exists
        /// In order to keep unique Ids in the program
        /// </summary>
        /// <param name="id">Searchable ID</param>
        /// <returns>
        /// Returns true if there is an item existing
        /// Returns false if the item does not exist
        /// </returns>
        public bool DoesItemExistsByID(int id)
        {
            var foundItems = orders.FindAll(p => p.GetId() == id);
            return foundItems.Count > 0 ? true : false;
        }

        /// <summary>
        /// Method for creating a string of all orders in the List
        /// </summary>
        /// <returns>Returns string object with orders printed</returns>
        public string ElementsToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(new string('-', 48) + "\r\n");
            sb.Append(" Orders Table\r\n");
            sb.Append(new string('-', 48) + "\r\n");
            sb.AppendFormat(" | {0, -3} | {1,-20} | {2, -14} |\r\n",
                    "Id", "DateTime", "Menu Items");
            foreach (OrderItem item in orders)
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
            return orders.Count;
        }

    }
}
