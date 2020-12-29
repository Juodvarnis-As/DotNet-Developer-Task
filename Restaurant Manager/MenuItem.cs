using System;
using System.Collections.Generic;
using System.Text;

namespace DotNET_Developer_Task
{
    /// <summary>
    /// Class for saving Menu items and for approporiate methods
    /// </summary>
    public class MenuItem : ParentItem
    {
        /// <summary>
        /// The name of the menu item
        /// </summary>
        private string Name;

        /// <summary>
        /// The products of menu item, saved in array
        /// </summary>
        private List<int> Products;

        /// <summary>
        /// Constructor for creating a menu item object
        /// </summary>
        /// <param name="id">The Id of menu item</param>
        /// <param name="Name">The name of menu item</param>
        public MenuItem(int id, string Name, List<int> Products)
        {
            SetId(id);
            this.Name = Name;
            this.Products = Products;
        }

        /// <summary>
        /// Method for putting items into Products list of menu item
        /// </summary>
        /// <param name="item">Addable object Id</param>
        public void AddItemsToMenuItem(int item)
        {
            Products.Add(item);
        }

        /// <summary>
        /// Method for returning item name
        /// </summary>
        /// <returns>Returns item name</returns>
        public string GetItemName()
        {
            return Name;
        }

        /// <summary>
        /// Method for updating menu item
        /// </summary>
        /// <param name="item">New updatable item</param>
        public void UpdateItems(MenuItem item)
        {
            this.Name = item.Name;
            this.Products = item.Products;
        }

        /// <summary>
        /// Method for returning data about stock item
        /// </summary>
        /// <returns>Returns stock item data as string</returns>
        public override string ToString()
        {
            // Format the products into one string
            string formattedProducts = "";
            foreach (int i in Products)
            {
                formattedProducts += i + " ";
            }
            return String.Format(" | {0, -3} | {1,-36} | {2, -16} |",
                    GetId(), Name, formattedProducts);
        }

        /// <summary>
        /// Method for returning products in a menu item
        /// </summary>
        /// <returns>
        /// Returns a list of products required for menu item
        /// </returns>
        public List<int> GetProducts()
        {
            return Products;
        }

        /// <summary>
        /// Method for returning a string about menu item in CSV format
        /// </summary>
        /// <returns>Returns menu item data as CSV string</returns>
        public string ToCsvFormat()
        {
            // Format the products into one string
            string formattedProducts = "";
            foreach (int i in Products)
            {
                formattedProducts += i + " ";
            }
            return String.Format("{0},{1},{2}",
                    GetId(), Name, formattedProducts);
        }
    }
}
