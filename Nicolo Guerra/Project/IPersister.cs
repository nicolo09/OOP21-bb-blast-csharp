namespace Project
{
    /// <summary>
    /// Interface that represents an object that can save and load an object.
    /// </summary>
    /// <typeparam name="T">The type of object to persist.</typeparam>
    public interface IPersister<T>
    {
        /// <summary>
        /// Loads an object.
        /// </summary>
        /// <returns>The loaded object</returns>
        T Load();
        /// <summary>
        /// Saves an object.
        /// </summary>
        /// <param name="objectToSave"></param>
        void Save(T objectToSave);
        /// <summary>
        /// Deletes the object persistance.
        /// </summary>
        /// <returns>true if successfully deleted</returns>
        bool Reset();
    }
}
