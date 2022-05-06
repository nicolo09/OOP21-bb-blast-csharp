using System;
using System.IO;
using System.Xml.Serialization;

namespace Project
{
    /// <summary>
    /// Saves object on a file.
    /// </summary>
    /// <typeparam name="T">The type of object to save.</typeparam>
    public class FilePersister<T> : IPersister<T>
    {
        private static readonly bool APPEND = false;
        private readonly String _filePath;
        /// <summary>
        /// Creates a new FilePersister which saves object in form of an XML file. 
        /// </summary>
        /// <param name="filePath">The path where the XML file will be saved</param>
        public FilePersister(String filePath)
        {
            this._filePath = filePath;
        }
        /// <inheritdoc/>
        public T Load()
        {
            TextReader reader = null;
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(T));
                reader = new StreamReader(_filePath);
                return (T)s.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
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
                TextWriter writer = null;
                try
                {
                    XmlSerializer s = new XmlSerializer(typeof(T));
                    writer = new StreamWriter(_filePath, APPEND);
                    s.Serialize(writer, objectToSave);
                }
                finally
                {
                    if (writer != null)
                        writer.Close();
                }
            }
        }
    }
}
