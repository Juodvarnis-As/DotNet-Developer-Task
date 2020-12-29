using System;
using System.Collections.Generic;
using System.Text;

namespace DotNET_Developer_Task
{
    /// <summary>
    /// An interface for lists with elements
    /// </summary>
    interface IEntry<T>
    {
        /// <summary>
        /// Method for adding new entry to the system
        /// </summary>
        /// <param name="obj">Addable object</param>
        /// <returns>
        /// Returns true if successfully added
        /// Returns false if unsuccessful
        /// </returns>
        bool AddNewEntry(T obj);

        /// <summary>
        /// Method for updating a specific entry 
        /// </summary>
        /// <param name="obj">Updatable object</param>
        ///<returns>
        /// Returns true if successfully updated
        /// Returns false if there are any issues
        /// </returns>
        bool UpdateItem(T obj);

        /// <summary>
        /// Method for checking if an item exists, by its Id
        /// </summary>
        /// <param name="id">Searchable Id</param>
        /// <returns>
        /// Returns true if the item does exist in stock
        /// Returns false if the item does not exist in stock
        /// </returns>
        bool DoesItemExistsByID(int id);

        /// <summary>
        /// Method for getting an object from class
        /// </summary>
        /// <param name="id">Searchable Id</param>
        /// <returns>
        /// Returns object from the class
        /// Else returns null if such object does not exist
        /// </returns>
        T GetItemByID(int id);

        /// <summary>
        /// Method for removing an element from the object by Id
        /// </summary>
        /// <param name="id">Removable Id</param>
        void RemoveItem(int id);

        /// <summary>
        /// Method for iterating the elements of object into one string
        /// Which would be displayed in the console window
        /// </summary>
        /// <returns>Returns elements in one string to display</returns>
        string ElementsToString();

        /// <summary>
        /// Method for getting element count in corresponding object
        /// Used entirely by testing methods
        /// </summary>
        /// <returns>Returns element count in container</returns>
        int GetElementCount();
    }
}
