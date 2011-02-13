#region Includes
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ClassLibrary;
#endregion

///////////////////////////////////////////////////////////////////////////////
// Copyright 2010 (c) by Class Library All Rights Reserved.
//  
// Project:      
// Module:       CourseTest.cs
// Description:  Tests for the Course class in the ClassLibrary assembly.
//  
// Date:       Author:           Comments:
// 21/08/2010 5:09 PM  Ali     Module created.
///////////////////////////////////////////////////////////////////////////////
namespace ClassLibraryTest
{

    /// <summary>
    /// Tests for the Course Class
    /// Documentation: Class Course
    /// </summary>
    [TestFixture(Description="Tests for Course")]
    public class CourseTest
    {
        #region Class Variables
        private Course _course = null;
        #endregion

        #region Setup/Teardown

        /// <summary>
        /// Code that is run once for a suite of tests
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {

        }

        /// <summary>
        /// Code that is run once after a suite of tests has finished executing
        /// </summary>
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {

        }

        /// <summary>
        /// Code that is run before each test
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            //New instance of Course
            _course = new Course();
        }

        /// <summary>
        /// Code that is run after each test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            //TODO:  Put dispose in here for _course or delete this line
        }
        #endregion

        #region Property Tests


        /// <summary>
        /// Assigned Professor Property Test
        /// Documentation:  Gets or sets the assigned professor.
        /// Property Type:  Professor
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void AssignedProfessorTest()
        {
            Professor expected = new Professor();
            _course.AssignedProfessor = expected;
            Assert.AreEqual(expected, _course.AssignedProfessor, "ClassLibrary.Course.AssignedProfessor property test failed");
        }

        /// <summary>
        /// Course Id Property Test
        /// Documentation:  Gets or sets the course id.
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void CourseIdTest()
        {
            int expected = 123;
            _course.CourseId = expected;
            Assert.AreEqual(expected, _course.CourseId, "ClassLibrary.Course.CourseId property test failed");
        }

        /// <summary>
        /// Course Name Property Test
        /// Documentation:  Gets or sets the name of the course.
        /// Property Type:  string
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void CourseNameTest()
        {
            string expected = "test";
            _course.CourseName = expected;
            Assert.AreEqual(expected, _course.CourseName, "ClassLibrary.Course.CourseName property test failed");
        }

        /// <summary>
        /// Days Of Week Property Test
        /// Documentation:  Gets the days of week.
        /// Property Type:  string
        /// Access       :  Read Only
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void DaysOfWeekTest()
        {
            string expected = "test";
            //TODO: Read Only Property, Delete this line and change the expected value ;
            Assert.AreEqual(expected, _course.DaysOfWeek, "ClassLibrary.Course.DaysOfWeek property test failed");
        }

        /// <summary>
        /// End Hour Property Test
        /// Documentation:  Gets or sets the end hour.
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void EndHourTest()
        {
            int expected = 123;
            _course.EndHour = expected;
            Assert.AreEqual(expected, _course.EndHour, "ClassLibrary.Course.EndHour property test failed");
        }

        /// <summary>
        /// End Minute Property Test
        /// Documentation:  Gets or sets the end minute.
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void EndMinuteTest()
        {
            int expected = 123;
            _course.EndMinute = expected;
            Assert.AreEqual(expected, _course.EndMinute, "ClassLibrary.Course.EndMinute property test failed");
        }

        /// <summary>
        /// Friday Property Test
        /// Documentation:  Gets or sets a value indicating whether this <see cref="Course"/> is friday.
        /// Property Type:  bool
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void FridayTest()
        {
            bool expected = true;
            _course.Friday = expected;
            Assert.AreEqual(expected, _course.Friday, "ClassLibrary.Course.Friday property test failed");
        }

        /// <summary>
        /// Monday Property Test
        /// Documentation:  Gets or sets a value indicating whether this <see cref="Course"/> is monday.
        /// Property Type:  bool
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void MondayTest()
        {
            bool expected = true;
            _course.Monday = expected;
            Assert.AreEqual(expected, _course.Monday, "ClassLibrary.Course.Monday property test failed");
        }

        /// <summary>
        /// Professors Associated List Property Test
        /// Documentation:  Gets or sets the professors interested and interested.
        /// Property Type:  IList<Preference>
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void ProfessorsAssociatedListTest()
        {
            IList<Preference> expected = new List<Preference>();
            _course.ProfessorsAssociatedList = expected;
            Assert.AreEqual(expected, _course.ProfessorsAssociatedList, "ClassLibrary.Course.ProfessorsAssociatedList property test failed");
        }

        /// <summary>
        /// Professors Interested List Property Test
        /// Documentation:  Gets the professors interested list.
        /// Property Type:  IEnumerable<Preference>
        /// Access       :  Read Only
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void ProfessorsInterestedListTest()
        {
            IEnumerable<Preference> expected = new IEnumerable<Preference>();
            //TODO: Read Only Property, Delete this line and change the expected value ;
            Assert.AreEqual(expected, _course.ProfessorsInterestedList, "ClassLibrary.Course.ProfessorsInterestedList property test failed");
        }

        /// <summary>
        /// Professors Not Interested List Property Test
        /// Documentation:  Gets the professors not interested list.
        /// Property Type:  IEnumerable<Preference>
        /// Access       :  Read Only
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void ProfessorsNotInterestedListTest()
        {
            IEnumerable<Preference> expected = new IEnumerable<Preference>();
            //TODO: Read Only Property, Delete this line and change the expected value ;
            Assert.AreEqual(expected, _course.ProfessorsNotInterestedList, "ClassLibrary.Course.ProfessorsNotInterestedList property test failed");
        }

        /// <summary>
        /// Saturday Property Test
        /// Documentation:  Gets or sets a value indicating whether this <see cref="Course"/> is saturday.
        /// Property Type:  bool
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SaturdayTest()
        {
            bool expected = true;
            _course.Saturday = expected;
            Assert.AreEqual(expected, _course.Saturday, "ClassLibrary.Course.Saturday property test failed");
        }

        /// <summary>
        /// Start Hour Property Test
        /// Documentation:  Gets or sets StartHour.
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void StartHourTest()
        {
            int expected = 123;
            _course.StartHour = expected;
            Assert.AreEqual(expected, _course.StartHour, "ClassLibrary.Course.StartHour property test failed");
        }

        /// <summary>
        /// Start Minute Property Test
        /// Documentation:  Gets or sets StartMinute.
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void StartMinuteTest()
        {
            int expected = 123;
            _course.StartMinute = expected;
            Assert.AreEqual(expected, _course.StartMinute, "ClassLibrary.Course.StartMinute property test failed");
        }

        /// <summary>
        /// Sunday Property Test
        /// Documentation:  Gets or sets a value indicating whether Sunday.
        /// Property Type:  bool
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SundayTest()
        {
            bool expected = true;
            _course.Sunday = expected;
            Assert.AreEqual(expected, _course.Sunday, "ClassLibrary.Course.Sunday property test failed");
        }

        /// <summary>
        /// Thursday Property Test
        /// Documentation:  Gets or sets a value indicating whether Thursday.
        /// Property Type:  bool
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void ThursdayTest()
        {
            bool expected = true;
            _course.Thursday = expected;
            Assert.AreEqual(expected, _course.Thursday, "ClassLibrary.Course.Thursday property test failed");
        }

        /// <summary>
        /// Time Slot Property Test
        /// Documentation:  Gets TimeSlot.
        /// Property Type:  string
        /// Access       :  Read Only
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void TimeSlotTest()
        {
            string expected = "test";
            //TODO: Read Only Property, Delete this line and change the expected value ;
            Assert.AreEqual(expected, _course.TimeSlot, "ClassLibrary.Course.TimeSlot property test failed");
        }

        /// <summary>
        /// Tuesday Property Test
        /// Documentation:  Gets or sets a value indicating whether Tuesday.
        /// Property Type:  bool
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void TuesdayTest()
        {
            bool expected = true;
            _course.Tuesday = expected;
            Assert.AreEqual(expected, _course.Tuesday, "ClassLibrary.Course.Tuesday property test failed");
        }

        /// <summary>
        /// Version Property Test
        /// Documentation:  Gets or sets Version.
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void VersionTest()
        {
            int expected = 123;
            _course.Version = expected;
            Assert.AreEqual(expected, _course.Version, "ClassLibrary.Course.Version property test failed");
        }

        /// <summary>
        /// Wednesday Property Test
        /// Documentation:  Gets or sets a value indicating whether Wednesday.
        /// Property Type:  bool
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void WednesdayTest()
        {
            bool expected = true;
            _course.Wednesday = expected;
            Assert.AreEqual(expected, _course.Wednesday, "ClassLibrary.Course.Wednesday property test failed");
        }


        #endregion

        #region Method Tests


        /// <summary>
        /// Clone Method Test
        /// Documentation   :  Clones this instance
        /// Method Signature:  object Clone()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void CloneTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            object expected = new object();
            object results;

            //Parameter Definitions
            
            //Default Value Test
            results = _course.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.Course.Clone method default test failed");

            //Min Test
            
            results = _course.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.Course.Clone method min test failed");

            //Max Test
            
            results = _course.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.Course.Clone method max test failed");

            //Null Test
            
            results = _course.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.Course.Clone method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Course.Clone Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Course Constructor Test
        /// Documentation   :  Initializes a new instance of the <see cref="Course"/> class.
        /// Constructor Signature:  Course( string pCourseName, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday, bool saturday, bool sunday, int startH, int startMin, int endH, int endMin, Professor pAssignedProfessor)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void CourseConstructor1Test()
        {
            System.DateTime constructorStartTime = System.DateTime.Now;
                        
            //Parameter Definitions
            string pCourseName = "test";
			bool monday = true;
			bool tuesday = true;
			bool wednesday = true;
			bool thursday = true;
			bool friday = true;
			bool saturday = true;
			bool sunday = true;
			int startH = 123;
			int startMin = 123;
			int endH = 123;
			int endMin = 123;
			Professor pAssignedProfessor = new Professor();

            //Default Value Test
             Course course = new Course(pCourseName, monday, tuesday, wednesday, thursday, friday, saturday, sunday, startH, startMin, endH, endMin, pAssignedProfessor);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Course.CourseConstructor1 constructor default test failed");

            //Min Test
            pCourseName = String.Empty;
			monday = false;
			tuesday = false;
			wednesday = false;
			thursday = false;
			friday = false;
			saturday = false;
			sunday = false;
			startH = int.MinValue;
			startMin = int.MinValue;
			endH = int.MinValue;
			endMin = int.MinValue;
			pAssignedProfessor = new Professor();

             Course course = new Course(pCourseName, monday, tuesday, wednesday, thursday, friday, saturday, sunday, startH, startMin, endH, endMin, pAssignedProfessor);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Course.CourseConstructor1 constructor min test failed");

            //Max Test
            pCourseName = string.Empty.PadRight(4097, 'K');
			monday = true;
			tuesday = true;
			wednesday = true;
			thursday = true;
			friday = true;
			saturday = true;
			sunday = true;
			startH = int.MaxValue;
			startMin = int.MaxValue;
			endH = int.MaxValue;
			endMin = int.MaxValue;
			pAssignedProfessor = new Professor();

             Course course = new Course(pCourseName, monday, tuesday, wednesday, thursday, friday, saturday, sunday, startH, startMin, endH, endMin, pAssignedProfessor);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Course.CourseConstructor1 constructor max test failed");

            //Null Test
            pCourseName = null;
			monday = true;
			tuesday = true;
			wednesday = true;
			thursday = true;
			friday = true;
			saturday = true;
			sunday = true;
			startH = 123;
			startMin = 123;
			endH = 123;
			endMin = 123;
			pAssignedProfessor = null;

             Course course = new Course(pCourseName, monday, tuesday, wednesday, thursday, friday, saturday, sunday, startH, startMin, endH, endMin, pAssignedProfessor);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Course.CourseConstructor1 constructor null test failed");

            System.TimeSpan constructorDuration = System.DateTime.Now.Subtract(constructorStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Course.CourseConstructor1 Time Elapsed: {0}", constructorDuration.ToString()));
        }

        /// <summary>
        /// Course Constructor Test
        /// Documentation   :  Initializes a new instance of the <see cref="Course"/> class.
        /// Constructor Signature:  Course()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void CourseConstructor2Test()
        {
            System.DateTime constructorStartTime = System.DateTime.Now;
                        
            //Parameter Definitions
            
            //Default Value Test
             Course course = new Course();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Course.CourseConstructor2 constructor default test failed");

            //Min Test
            
             Course course = new Course();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Course.CourseConstructor2 constructor min test failed");

            //Max Test
            
             Course course = new Course();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Course.CourseConstructor2 constructor max test failed");

            //Null Test
            
             Course course = new Course();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Course.CourseConstructor2 constructor null test failed");

            System.TimeSpan constructorDuration = System.DateTime.Now.Subtract(constructorStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Course.CourseConstructor2 Time Elapsed: {0}", constructorDuration.ToString()));
        }

        /// <summary>
        /// Course Constructor Test
        /// Documentation   :  Initializes a new instance of the <see cref="Course"/> class.
        /// Constructor Signature:  Course(int pCourseId)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void CourseConstructor3Test()
        {
            System.DateTime constructorStartTime = System.DateTime.Now;
                        
            //Parameter Definitions
            int pCourseId = 123;

            //Default Value Test
             Course course = new Course(pCourseId);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Course.CourseConstructor3 constructor default test failed");

            //Min Test
            pCourseId = int.MinValue;

             Course course = new Course(pCourseId);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Course.CourseConstructor3 constructor min test failed");

            //Max Test
            pCourseId = int.MaxValue;

             Course course = new Course(pCourseId);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Course.CourseConstructor3 constructor max test failed");

            //Null Test
            pCourseId = 123;

             Course course = new Course(pCourseId);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Course.CourseConstructor3 constructor null test failed");

            System.TimeSpan constructorDuration = System.DateTime.Now.Subtract(constructorStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Course.CourseConstructor3 Time Elapsed: {0}", constructorDuration.ToString()));
        }

        /// <summary>
        /// Deserialize From File< Course> Method Test
        /// Documentation   :  Method for entity class deserialization from XML file. Does not change this object content but returns another deserialized object instance
        /// Method Signature:  Course DeserializeFromFile<Course>(string xmlFilePath)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void DeserializeFromFile<Course>Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            Course expected = new Course();
            Course results;

            //Parameter Definitions
            string xmlFilePath = "test";

            //Default Value Test
            results = _course.DeserializeFromFile<Course>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.Course.DeserializeFromFile<Course> method default test failed");

            //Min Test
            xmlFilePath = String.Empty;

            results = _course.DeserializeFromFile<Course>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.Course.DeserializeFromFile<Course> method min test failed");

            //Max Test
            xmlFilePath = string.Empty.PadRight(4097, 'K');

            results = _course.DeserializeFromFile<Course>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.Course.DeserializeFromFile<Course> method max test failed");

            //Null Test
            xmlFilePath = null;

            results = _course.DeserializeFromFile<Course>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.Course.DeserializeFromFile<Course> method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Course.DeserializeFromFile<Course> Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Equals Method Test
        /// Documentation   :  local implementation of Equals based on unique value members
        /// Method Signature:  bool Equals(object obj)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void EqualsTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            bool expected = true;
            bool results;

            //Parameter Definitions
            object obj = new object();

            //Default Value Test
            results = _course.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.Course.Equals method default test failed");

            //Min Test
            obj = new object();

            results = _course.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.Course.Equals method min test failed");

            //Max Test
            obj = new object();

            results = _course.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.Course.Equals method max test failed");

            //Null Test
            obj = null;

            results = _course.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.Course.Equals method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Course.Equals Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Get Hash Code Method Test
        /// Documentation   :  local implementation of GetHashCode based on unique value members
        /// Method Signature:  int GetHashCode()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void GetHashCodeTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            int expected = 123;
            int results;

            //Parameter Definitions
            
            //Default Value Test
            results = _course.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.Course.GetHashCode method default test failed");

            //Min Test
            
            results = _course.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.Course.GetHashCode method min test failed");

            //Max Test
            
            results = _course.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.Course.GetHashCode method max test failed");

            //Null Test
            
            results = _course.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.Course.GetHashCode method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Course.GetHashCode Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Serialize To File Method Test
        /// Documentation   :  Method for entity class serialization to XML file
        /// Method Signature:  void SerializeToFile(string xmlFilePath)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SerializeToFileTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameter Definitions
            string xmlFilePath = "test";

            //Default Value Test
             _course.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Course.SerializeToFile method default test failed");

            //Min Test
            xmlFilePath = String.Empty;

             _course.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Course.SerializeToFile method min test failed");

            //Max Test
            xmlFilePath = string.Empty.PadRight(4097, 'K');

             _course.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Course.SerializeToFile method max test failed");

            //Null Test
            xmlFilePath = null;

             _course.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Course.SerializeToFile method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Course.SerializeToFile Time Elapsed: {0}", methodDuration.ToString()));
        }


        #endregion

    }
}
