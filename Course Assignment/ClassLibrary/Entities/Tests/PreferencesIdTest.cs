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
// Module:       PreferencesIdTest.cs
// Description:  Tests for the Preferences Id class in the ClassLibrary assembly.
//  
// Date:       Author:           Comments:
// 21/08/2010 5:09 PM  Ali     Module created.
///////////////////////////////////////////////////////////////////////////////
namespace ClassLibraryTest
{

    /// <summary>
    /// Tests for the Preferences Id Class
    /// Documentation: This class is to
    /// </summary>
    [TestFixture(Description="Tests for Preferences Id")]
    public class PreferencesIdTest
    {
        #region Class Variables
        private PreferencesId _preferencesId = null;
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
            //New instance of Preferences Id
            _preferencesId = new PreferencesId();
        }

        /// <summary>
        /// Code that is run after each test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            //TODO:  Put dispose in here for _preferencesId or delete this line
        }
        #endregion

        #region Property Tests


        /// <summary>
        /// Course Property Test
        /// Documentation:  Gets or sets Course.
        /// Property Type:  Course
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void CourseTest()
        {
            Course expected = new Course();
            _preferencesId.Course = expected;
            Assert.AreEqual(expected, _preferencesId.Course, "ClassLibrary.PreferencesId.Course property test failed");
        }

        /// <summary>
        /// Professor Property Test
        /// Documentation:  Gets or sets Professor.
        /// Property Type:  Professor
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void ProfessorTest()
        {
            Professor expected = new Professor();
            _preferencesId.Professor = expected;
            Assert.AreEqual(expected, _preferencesId.Professor, "ClassLibrary.PreferencesId.Professor property test failed");
        }


        #endregion

        #region Method Tests


        /// <summary>
        /// Clone Method Test
        /// Documentation   :  Creates a new object that is a copy of the current instance.
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
            results = _preferencesId.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.PreferencesId.Clone method default test failed");

            //Min Test
            
            results = _preferencesId.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.PreferencesId.Clone method min test failed");

            //Max Test
            
            results = _preferencesId.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.PreferencesId.Clone method max test failed");

            //Null Test
            
            results = _preferencesId.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.PreferencesId.Clone method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.PreferencesId.Clone Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Deserialize From File< Preferences Id> Method Test
        /// Documentation   :  Method for entity class deserialization from XML file. Does not change this object content but returns another deserialized object instance
        /// Method Signature:  PreferencesId DeserializeFromFile<PreferencesId>(string xmlFilePath)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void DeserializeFromFile<PreferencesId>Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            PreferencesId expected = new PreferencesId();
            PreferencesId results;

            //Parameter Definitions
            string xmlFilePath = "test";

            //Default Value Test
            results = _preferencesId.DeserializeFromFile<PreferencesId>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.PreferencesId.DeserializeFromFile<PreferencesId> method default test failed");

            //Min Test
            xmlFilePath = String.Empty;

            results = _preferencesId.DeserializeFromFile<PreferencesId>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.PreferencesId.DeserializeFromFile<PreferencesId> method min test failed");

            //Max Test
            xmlFilePath = string.Empty.PadRight(4097, 'K');

            results = _preferencesId.DeserializeFromFile<PreferencesId>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.PreferencesId.DeserializeFromFile<PreferencesId> method max test failed");

            //Null Test
            xmlFilePath = null;

            results = _preferencesId.DeserializeFromFile<PreferencesId>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.PreferencesId.DeserializeFromFile<PreferencesId> method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.PreferencesId.DeserializeFromFile<PreferencesId> Time Elapsed: {0}", methodDuration.ToString()));
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
            results = _preferencesId.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.PreferencesId.Equals method default test failed");

            //Min Test
            obj = new object();

            results = _preferencesId.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.PreferencesId.Equals method min test failed");

            //Max Test
            obj = new object();

            results = _preferencesId.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.PreferencesId.Equals method max test failed");

            //Null Test
            obj = null;

            results = _preferencesId.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.PreferencesId.Equals method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.PreferencesId.Equals Time Elapsed: {0}", methodDuration.ToString()));
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
            results = _preferencesId.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.PreferencesId.GetHashCode method default test failed");

            //Min Test
            
            results = _preferencesId.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.PreferencesId.GetHashCode method min test failed");

            //Max Test
            
            results = _preferencesId.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.PreferencesId.GetHashCode method max test failed");

            //Null Test
            
            results = _preferencesId.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.PreferencesId.GetHashCode method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.PreferencesId.GetHashCode Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Preferences Id Constructor Test
        /// Documentation   :  Initializes a new instance of the <see cref="PreferencesId"/> class.
        /// Constructor Signature:  PreferencesId()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void PreferencesIdConstructor1Test()
        {
            System.DateTime constructorStartTime = System.DateTime.Now;
                        
            //Parameter Definitions
            
            //Default Value Test
             PreferencesId preferencesId = new PreferencesId();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.PreferencesId.PreferencesIdConstructor1 constructor default test failed");

            //Min Test
            
             PreferencesId preferencesId = new PreferencesId();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.PreferencesId.PreferencesIdConstructor1 constructor min test failed");

            //Max Test
            
             PreferencesId preferencesId = new PreferencesId();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.PreferencesId.PreferencesIdConstructor1 constructor max test failed");

            //Null Test
            
             PreferencesId preferencesId = new PreferencesId();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.PreferencesId.PreferencesIdConstructor1 constructor null test failed");

            System.TimeSpan constructorDuration = System.DateTime.Now.Subtract(constructorStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.PreferencesId.PreferencesIdConstructor1 Time Elapsed: {0}", constructorDuration.ToString()));
        }

        /// <summary>
        /// Preferences Id Constructor Test
        /// Documentation   :  Initializes a new instance of the <see cref="PreferencesId"/> class.
        /// Constructor Signature:  PreferencesId(Professor professor, Course course)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void PreferencesIdConstructor2Test()
        {
            System.DateTime constructorStartTime = System.DateTime.Now;
                        
            //Parameter Definitions
            Professor professor = new Professor();
			Course course = new Course();

            //Default Value Test
             PreferencesId preferencesId = new PreferencesId(professor, course);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.PreferencesId.PreferencesIdConstructor2 constructor default test failed");

            //Min Test
            professor = new Professor();
			course = new Course();

             PreferencesId preferencesId = new PreferencesId(professor, course);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.PreferencesId.PreferencesIdConstructor2 constructor min test failed");

            //Max Test
            professor = new Professor();
			course = new Course();

             PreferencesId preferencesId = new PreferencesId(professor, course);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.PreferencesId.PreferencesIdConstructor2 constructor max test failed");

            //Null Test
            professor = null;
			course = null;

             PreferencesId preferencesId = new PreferencesId(professor, course);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.PreferencesId.PreferencesIdConstructor2 constructor null test failed");

            System.TimeSpan constructorDuration = System.DateTime.Now.Subtract(constructorStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.PreferencesId.PreferencesIdConstructor2 Time Elapsed: {0}", constructorDuration.ToString()));
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
             _preferencesId.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.PreferencesId.SerializeToFile method default test failed");

            //Min Test
            xmlFilePath = String.Empty;

             _preferencesId.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.PreferencesId.SerializeToFile method min test failed");

            //Max Test
            xmlFilePath = string.Empty.PadRight(4097, 'K');

             _preferencesId.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.PreferencesId.SerializeToFile method max test failed");

            //Null Test
            xmlFilePath = null;

             _preferencesId.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.PreferencesId.SerializeToFile method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.PreferencesId.SerializeToFile Time Elapsed: {0}", methodDuration.ToString()));
        }


        #endregion

    }
}
