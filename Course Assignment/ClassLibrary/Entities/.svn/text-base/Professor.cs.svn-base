using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using ClassLibrary.Interfaces;

namespace ClassLibrary
{
    [Serializable]
    public partial class Professor : ICloneable, IEntity

    {
        #region Constructors

        public Professor()
        {
        }

        public Professor(int pProfId)
        {
            ProfId = pProfId;
        }

        public Professor(string pTitle, string pFirstName, string pLastName, int pNoOfCourses, bool pUnassignedProf)
        {
            PTitle = pTitle;
            FirstName = pFirstName;
            LastName = pLastName;
            NoOfCourses = pNoOfCourses;
            UnassignedProf = pUnassignedProf;
        }

        #endregion

        #region Properties

        [XmlIgnore]
        public virtual IList<Course> Courses { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string FullName
        {
            get
            {
                string fullname;
                try
                {
                    fullname = PTitle + " " + FirstName + " " + LastName;
                }
                catch (Exception)
                {
                    fullname = "";
                }
                return fullname;
            }
        }

        public virtual int NoOfCourses { get; set; }

        [XmlIgnore]
        public virtual IList<Preference> Preferences { get; set; }

        public virtual int ProfId { get; set; }

        public virtual string PTitle { get; set; }

        //[XmlIgnore]
        //public virtual IList<Solution> Solutions { get; set; }

        public virtual bool UnassignedProf { get; set; }

        public virtual int Version { get; set; }

        #endregion

        #region Public Methods

        #region ICloneable Members

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        #endregion

        #region IEntity Members

        /// <summary>
        /// Method for entity class deserialization from XML file. Does not change this object content but returns another deserialized object instance
        /// </summary>
        /// <param name="xmlFilePath">Path of the XML file to read from.</param>
        /// <returns>Professor object restored from XML file</returns>
        public virtual Professor DeserializeFromFile<Professor>(string xmlFilePath)
        {
            Professor result;

            var seriliaser = new XmlSerializer(GetType());
            using (TextReader txtReader = new StreamReader(xmlFilePath))
            {
                result = (Professor) seriliaser.Deserialize(txtReader);
                txtReader.Close();
            }
            return result;
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

        /// <summary>
        /// local implementation of Equals based on unique value members
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            Professor castObj;
            try
            {
                castObj = (Professor) obj;
            }
            catch (Exception)
            {
                return false;
            }
            return (castObj != null) &&
                   (ProfId == castObj.ProfId);
        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode()
        {
            int hash = 57;
            hash = 27*hash*ProfId.GetHashCode();
            return hash;
        }

        #endregion

        //Nested class to provide strongly-typed access to property names (for .NET databinding, etc.)

        //Nested class to provide strongly-typed access to mapping names (for NHibernate Queries, etc.)

        #region Nested type: MappingNames

        public static class MappingNames
        {
            public const string Courses = "Courses";
            public const string FirstName = "FirstName";
            public const string LastName = "LastName";
            public const string NoOfCourses = "NoOfCourses";
            public const string Preferences = "Preferences";
            public const string Professor = "Professor";
            public const string ProfId = "ProfId";
            public const string Solutions = "Solutions";
            public const string Title = "Title";
            public const string UnassignedProf = "UnassignedProf";
            public const string Version = "Version";
        }

        #endregion

        #region Nested type: PropertyNames

        public static class PropertyNames
        {
            public const string Courses = "Courses";
            public const string FirstName = "FirstName";
            public const string LastName = "LastName";
            public const string NoOfCourses = "NoOfCourses";
            public const string Preferences = "Preferences";
            public const string Professor = "Professor";
            public const string ProfId = "ProfId";
            public const string Solutions = "Solutions";
            public const string Title = "Title";
            public const string UnassignedProf = "UnassignedProf";
            public const string Version = "Version";
        }

        #endregion
    }
}