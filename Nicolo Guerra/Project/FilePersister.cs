using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Project
{
    /// <summary>
    /// Saves object on a file.
    /// </summary>
    /// <typeparam name="T">The type of object to save.</typeparam>
    public class FilePersister<T> : IPersister<T>
    {
        private readonly String _filePath;
        /// <summary>
        /// Creates a new FilePersister which saves object in form of a binary file. 
        /// </summary>
        /// <param name="filePath">The path where the binary file will be saved</param>
        public FilePersister(String filePath)
        {
            this._filePath = filePath;
        }
        /// <inheritdoc/>
        public T Load()
        {
            Stream fileStream = null;
            try
            {
                BinaryFormatter des = new BinaryFormatter();
                fileStream = File.OpenRead(_filePath);
                return (T)des.Deserialize(fileStream);
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
        }
        /// <inheritdoc/>
        public bool Reset()
        {
            try
            {
                File.Delete(_filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <inheritdoc/>
        public void Save(T objectToSave)
        {
            if (!Directory.Exists(_filePath))
            {
                Directory.CreateDirectory(Directory.GetParent(_filePath).ToString());
                Stream fileStream = null;
                try
                {
                    BinaryFormatter ser = new BinaryFormatter();
                    fileStream = File.Create(_filePath);
                    ser.Serialize(fileStream, objectToSave);
                }
                finally
                {
                    if (fileStream != null)
                        fileStream.Close();
                }
            }
        }
    }
}
