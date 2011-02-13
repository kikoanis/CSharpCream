namespace ClassLibrary.Interfaces
{
    public interface IEntity
    {
        #region Serialize

        /// <summary>
        /// Method for entity class serialization to XML file
        /// </summary>
        /// <param name="xmlFilePath">Path of the XML file to write to. Will be overwritten if already exists.</param>
        void SerializeToFile(string xmlFilePath);

        /// <summary>
        /// Method for entity class deserialization from XML file. Does not change this object content but returns another deserialized object instance
        /// </summary>
        /// <param name="xmlFilePath">Path of the XML file to read from.</param>
        /// <returns>Course object restored from XML file</returns>
        T DeserializeFromFile<T>(string xmlFilePath);

        #endregion
    }
    
}