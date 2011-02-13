// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Course.cs" company="PC">
//   Ali Hmer 2008-2009
// </copyright>
// <summary>
//   Defines the Course type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using Interfaces;
    using System.Linq;

    /// <summary>
    /// Class Course
    /// </summary>
    [Serializable]
    public partial class Course : ICloneable, IEntity, IValidated
    {
        #region Fields

        /// <summary>
        /// Field courseName
        /// </summary>
        private string courseName = string.Empty;

        /// <summary>
        /// Field daysOfWeek
        /// </summary>
        private string daysOfWeek;

        /// <summary>
        /// Field endHour
        /// </summary>
        private int endHour;

        /// <summary>
        /// Field endMinute
        /// </summary>
        private int endMinute;

        /// <summary>
        /// Field friday
        /// </summary>
        private bool friday;

        /// <summary>
        /// Field monday
        /// </summary>
        private bool monday;

        /// <summary>
        /// Field saturday
        /// </summary>
        private bool saturday;

        /// <summary>
        /// Field startHour
        /// </summary>
        private int startHour;

        /// <summary>
        /// Field startMinute
        /// </summary>
        private int startMinute;

        /// <summary>
        /// Field sunday
        /// </summary>
        private bool sunday;

        /// <summary>
        /// Field thursday
        /// </summary>
        private bool thursday;

        /// <summary>
        /// Field tiemSlot
        /// </summary>
        private string timeSlot;

        /// <summary>
        /// Field tuesday
        /// </summary>
        private bool tuesday;

        /// <summary>
        /// Field wednesday
        /// </summary>
        private bool wednesday;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Course"/> class.
        /// </summary>
        public Course()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Course"/> class.
        /// </summary>
        /// <param name="pCourseName">Name of the p course.</param>
        /// <param name="monday">if set to <c>true</c> [monday].</param>
        /// <param name="tuesday">if set to <c>true</c> [tuesday].</param>
        /// <param name="wednesday">if set to <c>true</c> [wednesday].</param>
        /// <param name="thursday">if set to <c>true</c> [thursday].</param>
        /// <param name="friday">if set to <c>true</c> [friday].</param>
        /// <param name="saturday">if set to <c>true</c> [saturday].</param>
        /// <param name="sunday">if set to <c>true</c> [sunday].</param>
        /// <param name="startH">The start H.</param>
        /// <param name="startMin">The start min.</param>
        /// <param name="endH">The end H.</param>
        /// <param name="endMin">The end min.</param>
        /// <param name="pAssignedProfessor">The p assigned professor.</param>
        /// <exception cref="ArgumentNullException"><c>pCourseName</c> is null.</exception>
        public Course(
                        string pCourseName, 
                        bool monday, 
                        bool tuesday, 
                        bool wednesday, 
                        bool thursday,
                        bool friday, 
                        bool saturday, 
                        bool sunday, 
                        int startH,
                        int startMin, 
                        int endH, 
                        int endMin, 
                        Professor pAssignedProfessor)
        {
            if (pCourseName == null)
            {
                throw new ArgumentNullException("pCourseName");
            }

            CourseName = pCourseName;
            StartHour = startH;
            StartMinute = startMin;
            EndHour = endH;
            EndMinute = endMin;
            Monday = monday;
            Tuesday = tuesday;
            Wednesday = wednesday;
            Thursday = thursday;
            Friday = friday;
            Saturday = saturday;
            Sunday = sunday;
            AssignedProfessor = pAssignedProfessor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Course"/> class.
        /// </summary>
        /// <param name="pCourseId">The course id.</param>
        public Course(int pCourseId)
        {
            CourseId = pCourseId;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the assigned professor.
        /// </summary>
        /// <value>The assigned professor.</value>
        public virtual Professor AssignedProfessor { get; set; }

        /// <summary>
        /// Gets or sets the course id.
        /// </summary>
        /// <value>The course id.</value>
        public virtual int CourseId { get; set; }

        /// <summary>
        /// Gets or sets the name of the course.
        /// </summary>
        /// <value>The name of the course.</value>
        public virtual string CourseName
        {
            get
            {
                return courseName;
            }

            set
            {
                courseName = value.ToUpper();
            }
        }

        /// <summary>
        /// Gets the days of week.
        /// </summary>
        /// <value>The days of week.</value>
        public virtual string DaysOfWeek
        {
            get
            {
                return daysOfWeek;
            }

            private set
            {
                daysOfWeek = value;
                SetBooleanDays();
            }
        }

        /// <summary>
        /// Gets or sets the end hour.
        /// </summary>
        /// <value>The end hour.</value>
        public virtual int EndHour
        {
            get
            {
                return endHour;
            }

            set
            {
                endHour = value;
                SetTimeSlot();
            }
        }

        /// <summary>
        /// Gets or sets the end minute.
        /// </summary>
        /// <value>The end minute.</value>
        public virtual int EndMinute
        {
            get
            {
                return endMinute;
            }

            set
            {
                endMinute = value;
                SetTimeSlot();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Course"/> is friday.
        /// </summary>
        /// <value><c>true</c> if friday; otherwise, <c>false</c>.</value>
        public virtual bool Friday
        {
            get
            {
                return friday;
            }

            set
            {
                friday = value;
                SetDaysOfWeek();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Course"/> is monday.
        /// </summary>
        /// <value><c>true</c> if monday; otherwise, <c>false</c>.</value>
        public virtual bool Monday
        {
            get
            {
                return monday;
            }

            set
            {
                monday = value;
                SetDaysOfWeek();
            }
        }

        /// <summary>
        /// Gets or sets the professors interested and interested.
        /// </summary>
        /// <value>The professors interested and interested.</value>
        [XmlIgnore]
        public virtual IList<Preference> ProfessorsAssociatedList { get; set; }

        /// <summary>
        /// Gets the professors interested list.
        /// </summary>
        /// <value>The professors interested list.</value>
        public virtual IEnumerable<Preference> ProfessorsInterestedList
        {
            get
            {
                return ProfessorsAssociatedList
                                .Where(p => p.PreferenceType == Preference.PreferenceTypes.Equal)
                                .Select(p => p);
            }
        }

        /// <summary>
        /// Gets the professors not interested list.
        /// </summary>
        /// <value>The professors not interested list.</value>
        public virtual IEnumerable<Preference> ProfessorsNotInterestedList
        {
            get
            {
                return ProfessorsAssociatedList
                                .Where(p => p.PreferenceType == Preference.PreferenceTypes.NotEqual)
                                .Select(p => p);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Course"/> is saturday.
        /// </summary>
        /// <value><c>true</c> if saturday; otherwise, <c>false</c>.</value>
        public virtual bool Saturday
        {
            get
            {
                return saturday;
            }

            set
            {
                saturday = value;
                SetDaysOfWeek();
            }
        }

        /// <summary>
        /// Gets or sets StartHour.
        /// </summary>
        /// <value>
        /// The start hour.
        /// </value>
        public virtual int StartHour
        {
            get
            {
                return startHour;
            }

            set
            {
                startHour = value;
                SetTimeSlot();
            }
        }

        /// <summary>
        /// Gets or sets StartMinute.
        /// </summary>
        /// <value>
        /// The start minute.
        /// </value>
        public virtual int StartMinute
        {
            get
            {
                return startMinute;
            }

            set
            {
                startMinute = value;
                SetTimeSlot();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Sunday.
        /// </summary>
        /// <value>
        /// The sunday.
        /// </value>
        public virtual bool Sunday
        {
            get
            {
                return sunday;
            }

            set
            {
                sunday = value;
                SetDaysOfWeek();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Thursday.
        /// </summary>
        /// <value>
        /// The thursday.
        /// </value>
        public virtual bool Thursday
        {
            get
            {
                return thursday;
            }

            set
            {
                thursday = value;
                SetDaysOfWeek();
            }
        }

        /// <summary>
        /// Gets TimeSlot.
        /// </summary>
        /// <value>
        /// The time slot.
        /// </value>
        public virtual string TimeSlot
        {
            get
            {
                return timeSlot;
            }

            private set
            {
                timeSlot = value;
                SplitTimeSlot();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Tuesday.
        /// </summary>
        /// <value>
        /// The tuesday.
        /// </value>
        public virtual bool Tuesday
        {
            get
            {
                return tuesday;
            }

            set
            {
                tuesday = value;
                SetDaysOfWeek();
            }
        }

        /// <summary>
        /// Gets or sets Version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public virtual int Version { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Wednesday.
        /// </summary>
        /// <value>
        /// The wednesday.
        /// </value>
        public virtual bool Wednesday
        {
            get
            {
                return wednesday;
            }

            set
            {
                wednesday = value;
                SetDaysOfWeek();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Clones this instance
        /// </summary>
        /// <returns>
        /// an onject represents a clone of this instance
        /// </returns>
        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Method for entity class deserialization from XML file. Does not change this object content but returns another deserialized object instance
        /// </summary>
        /// <typeparam name="Course">The type of the ourse.</typeparam>
        /// <param name="xmlFilePath">Path of the XML file to read from.</param>
        /// <returns>Course object restored from XML file</returns>
        public virtual Course DeserializeFromFile<Course>(string xmlFilePath)
        {
            Course result;

            var seriliaser = new XmlSerializer(GetType());
            using (TextReader txtReader = new StreamReader(xmlFilePath))
            {
                result = (Course)seriliaser.Deserialize(txtReader);
                txtReader.Close();
            }

            return result;
        }

        /// <summary>
        /// local implementation of Equals based on unique value members
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref=/// System.Object/// /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            Course castObj;
            try
            {
                castObj = (Course) obj;
            }
            catch (Exception)
            {
                return false;
            }

            return (castObj != null) &&
                   (CourseId == castObj.CourseId);
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
            hash = 27 * hash * CourseId.GetHashCode();
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

        #region Private Methods

        /// <summary>
        /// Gets hour and minute out of string
        /// </summary>
        /// <param name="part">
        /// The part string.
        /// </param>
        /// <param name="hour">
        /// The hour .
        /// </param>
        /// <param name="min">
        /// The minutes.
        /// </param>
        private static void GetHourAndMinute(string part, out int hour, out int min)
        {
            string extStr = part.Contains("AM") ? part.Split('A')[0] : part.Split('P')[0];
            hour = Convert.ToInt32(extStr.Split(':')[0]);
            if (hour == 12 && part.Contains("AM"))
            {
                hour = 0;
            }

            if (part.Contains("PM") && hour < 12)
            {
                hour += 12;
            }

            min = Convert.ToInt32(extStr.Split(':')[1]);
        }

        /// <summary>
        /// Set days as boolean
        /// </summary>
        private void SetBooleanDays()
        {
            monday = false;
            tuesday = false;
            wednesday = false;
            thursday = false;
            friday = false;
            saturday = false;
            sunday = false;
            if (DaysOfWeek.Contains("M"))
            {
                monday = true;
            }

            if (DaysOfWeek.Contains("T"))
            {
                tuesday = true;
            }
            
            if (DaysOfWeek.Contains("W"))
            {
                wednesday = true;
            }
            
            if (DaysOfWeek.Contains("R"))
            {
                thursday = true;
            }
            
            if (DaysOfWeek.Contains("F"))
            {
                friday = true;
            }

            if (DaysOfWeek.Contains("A"))
            {
                saturday = true;
            }

            if (DaysOfWeek.Contains("S"))
            {
                sunday = true;
            }
        }

        /// <summary>
        /// Sets days of the week out of boolean values
        /// </summary>
        private void SetDaysOfWeek()
        {
            var dow = new StringBuilder(string.Empty);
            if (Monday)
            {
                dow.Append("M");
            }

            if (Tuesday)
            {
                dow.Append("T");
            }

            if (Wednesday)
            {
                dow.Append("W");
            }

            if (Thursday)
            {
                dow.Append("R");
            }

            if (Friday)
            {
                dow.Append("F");
            }

            if (Saturday)
            {
                dow.Append("A");
            }

            if (Sunday)
            {
                dow.Append("S");
            }

            DaysOfWeek = dow.ToString();
        }

        /// <summary>
        /// Sets time slot
        /// </summary>
        private void SetTimeSlot()
        {
            if (startHour >= 0 && startHour <= 23 &&
                endHour >= 0 && endHour <= 23 &&
                startMinute >= 0 && startMinute <= 59 &&
                endMinute >= 0 && endMinute <= 59)
            {
                var sb = new StringBuilder();
                if (startHour == 0 || startHour == 12)
                {
                    sb.Append(12.ToString());
                }
                else
                {
                    sb.Append(startHour % 12);
                }

                sb.Append(":");
                sb.Append(startMinute.ToString("00"));
                if (startHour == 0 || startHour < 12)
                {
                    sb.Append("AM");
                }
                else
                {
                    sb.Append("PM");
                }

                sb.Append("-");
                if (endHour == 0 || endHour == 12)
                {
                    sb.Append(12.ToString());
                }
                else
                {
                    sb.Append(endHour % 12);
                }

                sb.Append(":");
                sb.Append(endMinute.ToString("00"));
                if (endHour == 0 || endHour < 12)
                {
                    sb.Append("AM");
                }
                else
                {
                    sb.Append("PM");
                }

                TimeSlot = sb.ToString();
            }
        }

        /// <summary>
        /// Splits time slot
        /// </summary>
        private void SplitTimeSlot()
        {
            GetHourAndMinute(TimeSlot.Split('-')[0], out startHour, out startMinute);
            GetHourAndMinute(TimeSlot.Split('-')[1], out endHour, out endMinute);
        }

        #endregion

        // Nested class to provide strongly-typed access to property names (for .NET databinding, etc.)

        // Nested class to provide strongly-typed access to mapping names (for NHibernate Queries, etc.)
        
        /// <summary>
        /// Mapping names
        /// </summary>
        public static class MappingNames
        {
            /// <summary>
            /// Assigned Professor mapping name
            /// </summary>
            public const string AssignedProfessor = "AssignedProfessor";

            /// <summary>
            /// Course  mapping name
            /// </summary>
            public const string Course = "Course";

            /// <summary>
            /// CourseId  mapping name
            /// </summary>
            public const string CourseId = "CourseId";

            /// <summary>
            /// Course name  mapping name
            /// </summary>
            public const string CourseName = "CourseName";

            /// <summary>
            /// Days of week  mapping name
            /// </summary>
            public const string DaysOfWeek = "DaysOfWeek";

            /// <summary>
            /// Professors Interested
            /// </summary>
            public const string ProfessorsInterestedList = "Interested Professors List";

            /// <summary>
            /// Professors not Interested
            /// </summary>
            public const string ProfessorsNotInterestedList = "Not Interested Professors List";

            /// <summary>
            /// Professors Associated List
            /// </summary>
            public const string ProfessorsAssociatedList = "Interested and Not Interested Professors List";

            /// <summary>
            /// SOlutions  mapping name
            /// </summary>
            public const string Solutions = "Solutions";

            /// <summary>
            /// Time slot  mapping name
            /// </summary>
            public const string TimeSlot = "TimeSlot";

            /// <summary>
            /// Version  mapping name
            /// </summary>
            public const string Version = "Version";
        }

        /// <summary>
        /// Class Property names
        /// </summary>
        public static class PropertyNames
        {
            /// <summary>
            /// Assigned Professor Property Name
            /// </summary>
            public const string AssignedProfessor = "Assigned Professor";

            /// <summary>
            /// Course  Property Name
            /// </summary>
            public const string Course = "Course";

            /// <summary>
            /// Course Id Property Name
            /// </summary>
            public const string CourseId = "CourseId";

            /// <summary>
            /// Course name  Property Name
            /// </summary>
            public const string CourseName = "CourseName";

            /// <summary>
            /// Days of week
            /// </summary>
            public const string DaysOfWeek = "DaysOfWeek";

            /// <summary>
            /// Professors interested Property Name
            /// </summary>
            public const string ProfessorsInterested = "ProfessorsAssociatedList";

            /// <summary>
            /// Solution  Property Name
            /// </summary>
            public const string Solutions = "Solutions";

            /// <summary>
            /// Time slot  Property Name
            /// </summary>
            public const string TimeSlot = "TimeSlot";

            /// <summary>
            /// Version  Property Name
            /// </summary>
            public const string Version = "Version";
        }
    }
}