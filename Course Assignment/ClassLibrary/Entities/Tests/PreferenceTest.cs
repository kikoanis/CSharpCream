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
// Module:       PreferenceTest.cs
// Description:  Tests for the Preference class in the ClassLibrary assembly.
//  
// Date:       Author:           Comments:
// 21/08/2010 5:09 PM  Ali     Module created.
///////////////////////////////////////////////////////////////////////////////
namespace ClassLibraryTest
{

    /// <summary>
    /// Tests for the Preference Class
    /// Documentation: Class preference
    /// </summary>
    [TestFixture(Description="Tests for Preference")]
    public class PreferenceTest
    {
        #region Class Variables
        private Preference _preference = null;
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
            //New instance of Preference
            _preference = new Preference();
        }

        /// <summary>
        /// Code that is run after each test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            //TODO:  Put dispose in here for _preference or delete this line
        }
        #endregion

        #region Property Tests


        /// <summary>
        /// Id Property Test
        /// Documentation:  Gets or sets the id.
        /// Property Type:  PreferencesId
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void IdTest()
        {
            PreferencesId expected = new PreferencesId();
            _preference.Id = expected;
            Assert.AreEqual(expected, _preference.Id, "ClassLibrary.Preference.Id property test failed");
        }

        /// <summary>
        /// Preference Type Property Test
        /// Documentation:  Gets or sets the type of the preference.
        /// Property Type:  PreferenceTypes
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void PreferenceTypeTest()
        {
            PreferenceTypes expected = new PreferenceTypes();
            _preference.PreferenceType = expected;
            Assert.AreEqual(expected, _preference.PreferenceType, "ClassLibrary.Preference.PreferenceType property test failed");
        }

        /// <summary>
        /// Version Property Test
        /// Documentation:  Gets or sets the version.
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void VersionTest()
        {
            int expected = 123;
            _preference.Version = expected;
            Assert.AreEqual(expected, _preference.Version, "ClassLibrary.Preference.Version property test failed");
        }

        /// <summary>
        /// Weight Property Test
        /// Documentation:  Gets or sets the weight.
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void WeightTest()
        {
            int expected = 123;
            _preference.Weight = expected;
            Assert.AreEqual(expected, _preference.Weight, "ClassLibrary.Preference.Weight property test failed");
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
            results = _preference.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.Preference.Clone method default test failed");

            //Min Test
            
            results = _preference.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.Preference.Clone method min test failed");

            //Max Test
            
            results = _preference.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.Preference.Clone method max test failed");

            //Null Test
            
            results = _preference.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.Preference.Clone method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Preference.Clone Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Deserialize From File< Preference> Method Test
        /// Documentation   :  Method for entity class deserialization from XML file. Does not change this object content but returns another deserialized object instance
        /// Method Signature:  Preference DeserializeFromFile<Preference>(string xmlFilePath)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void DeserializeFromFile<Preference>Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            Preference expected = new Preference();
            Preference results;

            //Parameter Definitions
            string xmlFilePath = "test";

            //Default Value Test
            results = _preference.DeserializeFromFile<Preference>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.Preference.DeserializeFromFile<Preference> method default test failed");

            //Min Test
            xmlFilePath = String.Empty;

            results = _preference.DeserializeFromFile<Preference>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.Preference.DeserializeFromFile<Preference> method min test failed");

            //Max Test
            xmlFilePath = string.Empty.PadRight(4097, 'K');

            results = _preference.DeserializeFromFile<Preference>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.Preference.DeserializeFromFile<Preference> method max test failed");

            //Null Test
            xmlFilePath = null;

            results = _preference.DeserializeFromFile<Preference>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.Preference.DeserializeFromFile<Preference> method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Preference.DeserializeFromFile<Preference> Time Elapsed: {0}", methodDuration.ToString()));
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
            results = _preference.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.Preference.Equals method default test failed");

            //Min Test
            obj = new object();

            results = _preference.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.Preference.Equals method min test failed");

            //Max Test
            obj = new object();

            results = _preference.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.Preference.Equals method max test failed");

            //Null Test
            obj = null;

            results = _preference.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.Preference.Equals method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Preference.Equals Time Elapsed: {0}", methodDuration.ToString()));
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
            results = _preference.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.Preference.GetHashCode method default test failed");

            //Min Test
            
            results = _preference.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.Preference.GetHashCode method min test failed");

            //Max Test
            
            results = _preference.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.Preference.GetHashCode method max test failed");

            //Null Test
            
            results = _preference.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.Preference.GetHashCode method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Preference.GetHashCode Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Preference Constructor Test
        /// Documentation   :  Initializes a new instance of the <see cref="Preference"/> class.
        /// Constructor Signature:  Preference()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void PreferenceConstructor1Test()
        {
            System.DateTime constructorStartTime = System.DateTime.Now;
                        
            //Parameter Definitions
            
            //Default Value Test
             Preference preference = new Preference();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Preference.PreferenceConstructor1 constructor default test failed");

            //Min Test
            
             Preference preference = new Preference();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Preference.PreferenceConstructor1 constructor min test failed");

            //Max Test
            
             Preference preference = new Preference();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Preference.PreferenceConstructor1 constructor max test failed");

            //Null Test
            
             Preference preference = new Preference();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Preference.PreferenceConstructor1 constructor null test failed");

            System.TimeSpan constructorDuration = System.DateTime.Now.Subtract(constructorStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Preference.PreferenceConstructor1 Time Elapsed: {0}", constructorDuration.ToString()));
        }

        /// <summary>
        /// Preference Constructor Test
        /// Documentation   :  Initializes a new instance of the <see cref="Preference"/> class.
        /// Constructor Signature:  Preference(PreferencesId id)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void PreferenceConstructor2Test()
        {
            System.DateTime constructorStartTime = System.DateTime.Now;
                        
            //Parameter Definitions
            PreferencesId id = new PreferencesId();

            //Default Value Test
             Preference preference = new Preference(id);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Preference.PreferenceConstructor2 constructor default test failed");

            //Min Test
            id = new PreferencesId();

             Preference preference = new Preference(id);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Preference.PreferenceConstructor2 constructor min test failed");

            //Max Test
            id = new PreferencesId();

             Preference preference = new Preference(id);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Preference.PreferenceConstructor2 constructor max test failed");

            //Null Test
            id = null;

             Preference preference = new Preference(id);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Preference.PreferenceConstructor2 constructor null test failed");

            System.TimeSpan constructorDuration = System.DateTime.Now.Subtract(constructorStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Preference.PreferenceConstructor2 Time Elapsed: {0}", constructorDuration.ToString()));
        }

        /// <summary>
        /// Preference Constructor Test
        /// Documentation   :  Initializes a new instance of the <see cref="Preference"/> class.
        /// Constructor Signature:  Preference(PreferencesId id, int weight)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void PreferenceConstructor3Test()
        {
            System.DateTime constructorStartTime = System.DateTime.Now;
                        
            //Parameter Definitions
            PreferencesId id = new PreferencesId();
			int weight = 123;

            //Default Value Test
             Preference preference = new Preference(id, weight);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Preference.PreferenceConstructor3 constructor default test failed");

            //Min Test
            id = new PreferencesId();
			weight = int.MinValue;

             Preference preference = new Preference(id, weight);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Preference.PreferenceConstructor3 constructor min test failed");

            //Max Test
            id = new PreferencesId();
			weight = int.MaxValue;

             Preference preference = new Preference(id, weight);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Preference.PreferenceConstructor3 constructor max test failed");

            //Null Test
            id = null;
			weight = 123;

             Preference preference = new Preference(id, weight);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Preference.PreferenceConstructor3 constructor null test failed");

            System.TimeSpan constructorDuration = System.DateTime.Now.Subtract(constructorStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Preference.PreferenceConstructor3 Time Elapsed: {0}", constructorDuration.ToString()));
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
             _preference.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Preference.SerializeToFile method default test failed");

            //Min Test
            xmlFilePath = String.Empty;

             _preference.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Preference.SerializeToFile method min test failed");

            //Max Test
            xmlFilePath = string.Empty.PadRight(4097, 'K');

             _preference.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Preference.SerializeToFile method max test failed");

            //Null Test
            xmlFilePath = null;

             _preference.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Preference.SerializeToFile method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Preference.SerializeToFile Time Elapsed: {0}", methodDuration.ToString()));
        }


        #endregion

    }
}
