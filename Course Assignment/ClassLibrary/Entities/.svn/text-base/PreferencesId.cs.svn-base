// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="PreferencesId.cs" company="PC">
// Copy right Ali Hmer 2008-2009  
// </copyright>
// <summary>
//   Defines the PreferencesId type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace ClassLibrary
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using Interfaces;

    /// <summary>
    /// This class is to 
    /// </summary>
    [Serializable]
    public class PreferencesId : ICloneable, IEntity
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PreferencesId"/> class. 
        /// </summary>
        /// <param name="professor">
        /// The professor.
        /// </param>
        /// <param name="course">
        /// The course.
        /// </param>
        public PreferencesId(Professor professor, Course course)
        {
            Professor = professor;
            Course = course;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreferencesId"/> class. 
        /// </summary>
        public PreferencesId()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Course.
        /// </summary>
        /// <value>
        /// The course.
        /// </value>
        public virtual Course Course { get; set; }

        /// <summary>
        /// Gets or sets Professor.
        /// </summary>
        /// <value>
        /// The professor.
        /// </value>
        public virtual Professor Professor { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Method for entity class deserialization from XML file. Does not change this object content but returns another deserialized object instance
        /// </summary>
        /// <typeparam name="PreferencesId">The type of the references id.</typeparam>
        /// <param name="xmlFilePath">Path of the XML file to read from.</param>
        /// <returns>
        /// PreferencesId object restored from XML file
        /// </returns>
        public virtual PreferencesId DeserializeFromFile<PreferencesId>(string xmlFilePath)
        {
            PreferencesId result;

            var seriliaser = new XmlSerializer(GetType());
            using (TextReader txtReader = new StreamReader(xmlFilePath))
            {
                result = (PreferencesId)seriliaser.Deserialize(txtReader);
                txtReader.Close();
            }

            return result;
        }

        /// <summary>
        /// local implementation of Equals based on unique value members
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            PreferencesId castObj;
            try
            {
                castObj = (PreferencesId)obj;
            }
            catch (Exception)
            {
                return false;
            }

            return (castObj != null) &&
                   Professor.Equals(castObj.Professor) &&
                   Course.Equals(castObj.Course);
        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int hash = 57;
            hash = 27 * hash * Professor.GetHashCode();
            hash = 27 * hash * Course.GetHashCode();
            return hash;
        }

        /// <summary>
        /// Method for entity class serialization to XML file
        /// </summary>
        /// <param name="xmlFilePath">Path of the XML file to write to. Will be overwritten if already exists.</param>
        public virtual void SerializeToFile(string xmlFilePath)
        {
            var seriliaser = new XmlSerializer(GetType());
            using (TextWriter txtWriter = new StreamWriter(xmlFilePath))
            {
                seriliaser.Serialize(txtWriter, this);
                txtWriter.Close();
            }
        }

        #endregion

        // Nested class to provide strongly-typed access to property names (for .NET databinding, etc.)

        // Nested class to provide strongly-typed access to mapping names (for NHibernate Queries, etc.)

        /// <summary>
        /// Mapping class
        /// </summary>
        public static class MappingNames
        {
            /// <summary>
            /// const CourseId
            /// </summary>
            public const string CourseId = "CourseId";

            /// <summary>
            /// Const PreferencesId
            /// </summary>
            public const string PreferencesId = "PreferencesId";

            /// <summary>
            /// COnst ProfId
            /// </summary>
            public const string ProfId = "ProfId";
        }

        /// <summary>
        /// Class Property names
        /// </summary>
        public static class PropertyNames
        {
            /// <summary>
            /// const Course Id
            /// </summary>
            public const string CourseId = "CourseId";

            /// <summary>
            /// COnst PreferenceId
            /// </summary>
            public const string PreferencesId = "PreferencesId";

            /// <summary>
            /// Const ProfId
            /// </summary>
            public const string ProfId = "ProfId";
        }
    }
}