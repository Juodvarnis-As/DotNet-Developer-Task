using System;
using System.Collections.Generic;
using System.Text;

namespace DotNET_Developer_Task
{
    /// <summary>
    /// Main parent class, which will be inherited by Stock, Menu and Orders classes
    /// Class created in order to show use of abstractions
    /// </summary>
    public class ParentItem
    {
        /// <summary>
        /// The unique identifier of the item
        /// </summary>
        private int Id;

        /// <summary>
        /// Method for getting element Id
        /// </summary>
        public int GetId()
        {
            return Id;
        }

        /// <summary>
        /// Method for setting new element Id
        /// </summary>
        /// <param name="newId">New Id</param>
        public void SetId(int newId)
        {
            Id = newId;
        }


    }
}
