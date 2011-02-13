// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Solution.cs" company="PC">
//   Copyright 2008-2009 Ali Hmer
// </copyright>
// <summary>
//   Defines the Solution type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace ClassLibrary
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using Interfaces;

    /// <summary>
    /// Class Solution
    /// </summary>
    [Serializable]
    public class Solution : ICloneable, IEntity
    {
        #region Fields

        /// <summary>
        /// isCHanged field
        /// </summary>
        private bool isChanged;

        /// <summary>
        /// isDeleted field
        /// </summary>
        private bool isDeleted;

        /// <summary>
        /// course field
        /// </summary>
        private string course;

        protected Guid id;

        protected string professor;

        protected int solutionno;

        protected int weight;

        protected int version;

        protected long time;

        #endregion

        #region Constructors

        public Solution(Guid pId)
        {
            id = pId;
        }

        public Solution()
        {
        }

        public Solution(int pSolutionNo, string pCourse, string pProfessor, int pWeight, long pTime)
        {
            solutionno = pSolutionNo;
            course = pCourse;
            professor = pProfessor;
            weight = pWeight;
            time = pTime;
        }

        #endregion

        #region Properties

        public virtual string Course
        {
            get { return course; }
            set
            {
                isChanged |= (course != value);
                course = value;
            }
        }

        public virtual Guid Id
        {
            get { return id; }
            set
            {
                isChanged |= (id != value);
                id = value;
            }
        }

        public virtual bool IsChanged
        {
            get { return isChanged; }
            set { isChanged = value; }
        }

        public virtual bool IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }

        public virtual string Professor
        {
            get { return professor; }
            set
            {
                isChanged |= (professor != value);
                professor = value;
            }
        }

        public virtual int SolutionNo
        {
            get { return solutionno; }
            set
            {
                isChanged |= (solutionno != value);
                solutionno = value;
            }
        }

        public virtual int Version
        {
            get { return version; }
            set
            {
                isChanged |= (version != value);
                version = value;
            }
        }

        public virtual int Weight
        {
            get { return weight; }
            set
            {
                isChanged |= (weight != value);
                weight = value;
            }
        }

        public virtual long Time
        {
            get { return time;}
            set
            {
                isChanged |= (time != value);
                time = value;
            }
        }

        #endregion

        #region Public Methods

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Method for entity class deserialization from XML file. Does not change this object content but returns another deserialized object instance
        /// </summary>
        /// <param name="xmlFilePath">Path of the XML file to read from.</param>
        /// <returns>Solution object restored from XML file</returns>
        public virtual Solution DeserializeFromFile<Solution>(string xmlFilePath)
        {
            Solution result;

            var seriliaser = new XmlSerializer(GetType());
            using (TextReader txtReader = new StreamReader(xmlFilePath))
            {
                result = (Solution)seriliaser.Deserialize(txtReader);
                txtReader.Close();
            }
            return result;
        }

        /// <summary>
        /// local implementation of Equals based on unique value members
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            Solution castObj;
            try
            {
                castObj = (Solution)obj;
            }
            catch (Exception)
            {
                return false;
            }
            return (castObj != null) &&
                   (id == castObj.Id);
        }

        /// <summary>
        /// local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode()
        {
            int hash = 57;
            hash = 27 * hash * id.GetHashCode();
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

        //Nested class to provide strongly-typed access to property names (for .NET databinding, etc.)

        //Nested class to provide strongly-typed access to mapping names (for NHibernate Queries, etc.)

        public static class MappingNames
        {
            public const string Course = "Course";
            public const string Id = "Id";
            public const string Professor = "Professor";
            public const string Solution = "Solution";
            public const string SolutionNo = "SolutionNo";
            public const string Weight = "Weight";
            public const string Time = "Time";
            public const string Version = "Version";
        }

        public static class PropertyNames
        {
            public const string Course = "Course";
            public const string Id = "Id";
            public const string Professor = "Professor";
            public const string Solution = "Solution";
            public const string SolutionNo = "SolutionNo";
            public const string Weight = "Weight";
            public const string Time = "Time";
            public const string Version = "Version";
        }

    }
}