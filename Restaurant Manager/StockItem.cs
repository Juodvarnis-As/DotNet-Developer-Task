using System;
using System.Collections.Generic;
using System.Text;

namespace DotNET_Developer_Task
{
    /// <summary>
    /// Main class for saving restaurant stock items and methods
    /// Inherits ParentItem class, with Id field and other methods
    /// </summary>
    public class StockItem : ParentItem
    {
        /// <summary>
        /// The name of stock item
        /// </summary>
        private string Name;

        /// <summary>
        /// The count of portions of stock item
        /// </summary>
        private double PortionCount;

        /// <summary>
        /// The unit of portion of stock item
        /// </summary>
        private string Unit;

        /// <summary>
        /// The size of one portion of stock item
        /// Used in Menu class
        /// </summary>
        private double PortionSize;

        /// <summary>
        /// Main constructor of Stock class
        /// </summary>
        /// <param name="id">The Id of the stock item</param>
        /// <param name="name">Name of stock item</param>
        /// <param name="portionCount">Amount of portion in stock</param>
        /// <param name="unit">Unit of portion</param>
        /// <param name="portionSize">Single portion size</param>
        public StockItem(int id, string name, double portionCount, string unit, double portionSize)
        {
            SetId(id);
            Name = name;
            PortionCount = portionCount;
            Unit = unit;
            PortionSize = portionSize;
        }

        /// <summary>
        /// Method for returning item name
        /// </summary>
        /// <returns>Returns the name of the stock item</returns>
        public string GetName()
        {
            return Name;
        }

        /// <summary>
        /// Method for updating a Stock item with new values
        /// </summary>
        /// <param name="Name">New name of the stock item</param>
        /// <param name="PortionCount">New portion count of the stock item</param>
        /// <param name="Unit">New portion unit of stock item</param>
        /// <param name="PortionSize">New portion size of stock item</param>
        public void UpdateElement(string Name, double PortionCount, string Unit, double PortionSize)
        {
            this.Name = Name;
            this.PortionCount = PortionCount;
            this.Unit = Unit;
            this.PortionSize = PortionSize;
        }

        /// <summary>
        /// Method for updating a Stock item with new object
        /// </summary>
        /// <param name="item">StockItem object</param>
        public void UpdateElement(StockItem item)
        {
            this.Name = item.Name;
            this.PortionCount = item.PortionCount;
            this.Unit = item.Unit;
            this.PortionSize = item.PortionSize;
        }

        /// <summary>
        /// Method for reducing portion by one portion size
        /// </summary>
        public void UpdateElement()
        {
            PortionCount -= PortionSize;
        }

        /// <summary>
        /// Method for getting portion count 
        /// </summary>
        /// <returns>Return potion count</returns>
        public double GetPortionCount()
        {
            return PortionCount;
        }

        /// <summary>
        /// Method for checking if there is enough items in stock
        /// For single portion reduction
        /// </summary>
        /// <returns>
        /// Returns true if there is enough in stock
        /// Returns false if there are not enough of item in stock
        /// </returns>
        public bool CheckIfEnoughInStock()
        {
            /// Check if it is possible to reduce the portion count
            return PortionCount >= PortionSize ?  true :  false;
            
        }

        /// <summary>
        /// Method for returning data about stock item
        /// </summary>
        /// <returns>Returns stock item data as string</returns>
        public override string ToString()
        {
            return String.Format(" | {0, -3} | {1,-25} | {2, 14} | {3, 4} | {4, 12} |",
                    GetId(), Name, PortionCount, Unit, PortionSize);
        }

        /// <summary>
        /// Method for returning a string about stock item in CSV format
        /// </summary>
        /// <returns>Returns stock item data as CSV string</returns>
        public string ToCsvFormat()
        {
            return String.Format("{0},{1},{2},{3},{4}",
                    GetId(), Name, PortionCount, Unit, PortionSize);
        }

    }
}
