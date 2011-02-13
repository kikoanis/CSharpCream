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
// Module:       ProfessorTest.cs
// Description:  Tests for the Professor class in the ClassLibrary assembly.
//  
// Date:       Author:           Comments:
// 21/08/2010 5:09 PM  Ali     Module created.
///////////////////////////////////////////////////////////////////////////////
namespace ClassLibraryTest
{

    /// <summary>
    /// Tests for the Professor Class
    /// Documentation: 
    /// </summary>
    [TestFixture(Description="Tests for Professor")]
    public class ProfessorTest
    {
        #region Class Variables
        private Professor _professor = null;
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
            //New instance of Professor
            _professor = new Professor();
        }

        /// <summary>
        /// Code that is run after each test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            //TODO:  Put dispose in here for _professor or delete this line
        }
        #endregion

        #region Property Tests


        /// <summary>
        /// Courses Property Test
        /// Documentation:  
        /// Property Type:  IList<Course>
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void CoursesTest()
        {
            IList<Course> expected = new List<Course>();
            _professor.Courses = expected;
            Assert.AreEqual(expected, _professor.Courses, "ClassLibrary.Professor.Courses property test failed");
        }

        /// <summary>
        /// First Name Property Test
        /// Documentation:  
        /// Property Type:  string
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void FirstNameTest()
        {
            string expected = "test";
            _professor.FirstName = expected;
            Assert.AreEqual(expected, _professor.FirstName, "ClassLibrary.Professor.FirstName property test failed");
        }

        /// <summary>
        /// Full Name Property Test
        /// Documentation:  
        /// Property Type:  string
        /// Access       :  Read Only
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void FullNameTest()
        {
            string expected = "test";
            //TODO: Read Only Property, Delete this line and change the expected value ;
            Assert.AreEqual(expected, _professor.FullName, "ClassLibrary.Professor.FullName property test failed");
        }

        /// <summary>
        /// Last Name Property Test
        /// Documentation:  
        /// Property Type:  string
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void LastNameTest()
        {
            string expected = "test";
            _professor.LastName = expected;
            Assert.AreEqual(expected, _professor.LastName, "ClassLibrary.Professor.LastName property test failed");
        }

        /// <summary>
        /// No Of Courses Property Test
        /// Documentation:  
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void NoOfCoursesTest()
        {
            int expected = 123;
            _professor.NoOfCourses = expected;
            Assert.AreEqual(expected, _professor.NoOfCourses, "ClassLibrary.Professor.NoOfCourses property test failed");
        }

        /// <summary>
        /// Preferences Property Test
        /// Documentation:  
        /// Property Type:  IList<Preference>
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void PreferencesTest()
        {
            IList<Preference> expected = new List<Preference>();
            _professor.Preferences = expected;
            Assert.AreEqual(expected, _professor.Preferences, "ClassLibrary.Professor.Preferences property test failed");
        }

        /// <summary>
        /// Prof Id Property Test
        /// Documentation:  
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void ProfIdTest()
        {
            int expected = 123;
            _professor.ProfId = expected;
            Assert.AreEqual(expected, _professor.ProfId, "ClassLibrary.Professor.ProfId property test failed");
        }

        /// <summary>
        /// P Title Property Test
        /// Documentation:  
        /// Property Type:  string
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void PTitleTest()
        {
            string expected = "test";
            _professor.PTitle = expected;
            Assert.AreEqual(expected, _professor.PTitle, "ClassLibrary.Professor.PTitle property test failed");
        }

        /// <summary>
        /// Unassigned Prof Property Test
        /// Documentation:  
        /// Property Type:  bool
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void UnassignedProfTest()
        {
            bool expected = true;
            _professor.UnassignedProf = expected;
            Assert.AreEqual(expected, _professor.UnassignedProf, "ClassLibrary.Professor.UnassignedProf property test failed");
        }

        /// <summary>
        /// Version Property Test
        /// Documentation:  
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void VersionTest()
        {
            int expected = 123;
            _professor.Version = expected;
            Assert.AreEqual(expected, _professor.Version, "ClassLibrary.Professor.Version property test failed");
        }


        #endregion

        #region Method Tests


        /// <summary>
        /// Clone Method Test
        /// Documentation   :  
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
            results = _professor.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.Professor.Clone method default test failed");

            //Min Test
            
            results = _professor.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.Professor.Clone method min test failed");

            //Max Test
            
            results = _professor.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.Professor.Clone method max test failed");

            //Null Test
            
            results = _professor.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.Professor.Clone method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Professor.Clone Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Deserialize From File< Professor> Method Test
        /// Documentation   :  Method for entity class deserialization from XML file. Does not change this object content but returns another deserialized object instance
        /// Method Signature:  Professor DeserializeFromFile<Professor>(string xmlFilePath)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void DeserializeFromFile<Professor>Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            Professor expected = new Professor();
            Professor results;

            //Parameter Definitions
            string xmlFilePath = "test";

            //Default Value Test
            results = _professor.DeserializeFromFile<Professor>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.Professor.DeserializeFromFile<Professor> method default test failed");

            //Min Test
            xmlFilePath = String.Empty;

            results = _professor.DeserializeFromFile<Professor>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.Professor.DeserializeFromFile<Professor> method min test failed");

            //Max Test
            xmlFilePath = string.Empty.PadRight(4097, 'K');

            results = _professor.DeserializeFromFile<Professor>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.Professor.DeserializeFromFile<Professor> method max test failed");

            //Null Test
            xmlFilePath = null;

            results = _professor.DeserializeFromFile<Professor>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.Professor.DeserializeFromFile<Professor> method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Professor.DeserializeFromFile<Professor> Time Elapsed: {0}", methodDuration.ToString()));
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
            results = _professor.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.Professor.Equals method default test failed");

            //Min Test
            obj = new object();

            results = _professor.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.Professor.Equals method min test failed");

            //Max Test
            obj = new object();

            results = _professor.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.Professor.Equals method max test failed");

            //Null Test
            obj = null;

            results = _professor.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.Professor.Equals method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Professor.Equals Time Elapsed: {0}", methodDuration.ToString()));
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
            results = _professor.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.Professor.GetHashCode method default test failed");

            //Min Test
            
            results = _professor.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.Professor.GetHashCode method min test failed");

            //Max Test
            
            results = _professor.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.Professor.GetHashCode method max test failed");

            //Null Test
            
            results = _professor.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.Professor.GetHashCode method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Professor.GetHashCode Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Professor Constructor Test
        /// Documentation   :  
        /// Constructor Signature:  Professor()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void ProfessorConstructor1Test()
        {
            System.DateTime constructorStartTime = System.DateTime.Now;
                        
            //Parameter Definitions
            
            //Default Value Test
             Professor professor = new Professor();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Professor.ProfessorConstructor1 constructor default test failed");

            //Min Test
            
             Professor professor = new Professor();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Professor.ProfessorConstructor1 constructor min test failed");

            //Max Test
            
             Professor professor = new Professor();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Professor.ProfessorConstructor1 constructor max test failed");

            //Null Test
            
             Professor professor = new Professor();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Professor.ProfessorConstructor1 constructor null test failed");

            System.TimeSpan constructorDuration = System.DateTime.Now.Subtract(constructorStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Professor.ProfessorConstructor1 Time Elapsed: {0}", constructorDuration.ToString()));
        }

        /// <summary>
        /// Professor Constructor Test
        /// Documentation   :  
        /// Constructor Signature:  Professor(int pProfId)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void ProfessorConstructor2Test()
        {
            System.DateTime constructorStartTime = System.DateTime.Now;
                        
            //Parameter Definitions
            int pProfId = 123;

            //Default Value Test
             Professor professor = new Professor(pProfId);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Professor.ProfessorConstructor2 constructor default test failed");

            //Min Test
            pProfId = int.MinValue;

             Professor professor = new Professor(pProfId);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Professor.ProfessorConstructor2 constructor min test failed");

            //Max Test
            pProfId = int.MaxValue;

             Professor professor = new Professor(pProfId);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Professor.ProfessorConstructor2 constructor max test failed");

            //Null Test
            pProfId = 123;

             Professor professor = new Professor(pProfId);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Professor.ProfessorConstructor2 constructor null test failed");

            System.TimeSpan constructorDuration = System.DateTime.Now.Subtract(constructorStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Professor.ProfessorConstructor2 Time Elapsed: {0}", constructorDuration.ToString()));
        }

        /// <summary>
        /// Professor Constructor Test
        /// Documentation   :  
        /// Constructor Signature:  Professor(string pTitle, string pFirstName, string pLastName, int pNoOfCourses, bool pUnassignedProf)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void ProfessorConstructor3Test()
        {
            System.DateTime constructorStartTime = System.DateTime.Now;
                        
            //Parameter Definitions
            string pTitle = "test";
			string pFirstName = "test";
			string pLastName = "test";
			int pNoOfCourses = 123;
			bool pUnassignedProf = true;

            //Default Value Test
             Professor professor = new Professor(pTitle, pFirstName, pLastName, pNoOfCourses, pUnassignedProf);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Professor.ProfessorConstructor3 constructor default test failed");

            //Min Test
            pTitle = String.Empty;
			pFirstName = String.Empty;
			pLastName = String.Empty;
			pNoOfCourses = int.MinValue;
			pUnassignedProf = false;

             Professor professor = new Professor(pTitle, pFirstName, pLastName, pNoOfCourses, pUnassignedProf);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Professor.ProfessorConstructor3 constructor min test failed");

            //Max Test
            pTitle = string.Empty.PadRight(4097, 'K');
			pFirstName = string.Empty.PadRight(4097, 'K');
			pLastName = string.Empty.PadRight(4097, 'K');
			pNoOfCourses = int.MaxValue;
			pUnassignedProf = true;

             Professor professor = new Professor(pTitle, pFirstName, pLastName, pNoOfCourses, pUnassignedProf);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Professor.ProfessorConstructor3 constructor max test failed");

            //Null Test
            pTitle = null;
			pFirstName = null;
			pLastName = null;
			pNoOfCourses = 123;
			pUnassignedProf = true;

             Professor professor = new Professor(pTitle, pFirstName, pLastName, pNoOfCourses, pUnassignedProf);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Professor.ProfessorConstructor3 constructor null test failed");

            System.TimeSpan constructorDuration = System.DateTime.Now.Subtract(constructorStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Professor.ProfessorConstructor3 Time Elapsed: {0}", constructorDuration.ToString()));
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
             _professor.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Professor.SerializeToFile method default test failed");

            //Min Test
            xmlFilePath = String.Empty;

             _professor.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Professor.SerializeToFile method min test failed");

            //Max Test
            xmlFilePath = string.Empty.PadRight(4097, 'K');

             _professor.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Professor.SerializeToFile method max test failed");

            //Null Test
            xmlFilePath = null;

             _professor.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Professor.SerializeToFile method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Professor.SerializeToFile Time Elapsed: {0}", methodDuration.ToString()));
        }


        #endregion

    }
}
