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
// Module:       SettingsTest.cs
// Description:  Tests for the Settings class in the ClassLibrary assembly.
//  
// Date:       Author:           Comments:
// 21/08/2010 5:09 PM  Ali     Module created.
///////////////////////////////////////////////////////////////////////////////
namespace ClassLibraryTest
{

    /// <summary>
    /// Tests for the Settings Class
    /// Documentation: Settings class
    /// </summary>
    [TestFixture(Description="Tests for Settings")]
    public class SettingsTest
    {
        #region Class Variables
        private Settings _settings = null;
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
            //New instance of Settings
            _settings = new Settings();
        }

        /// <summary>
        /// Code that is run after each test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            //TODO:  Put dispose in here for _settings or delete this line
        }
        #endregion

        #region Property Tests


        /// <summary>
        /// Display Constraints Details Property Test
        /// Documentation:  Gets or sets a value indicating whether DisplayConstraintsDetails.
        /// Property Type:  bool
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void DisplayConstraintsDetailsTest()
        {
            bool expected = true;
            _settings.DisplayConstraintsDetails = expected;
            Assert.AreEqual(expected, _settings.DisplayConstraintsDetails, "ClassLibrary.Settings.DisplayConstraintsDetails property test failed");
        }

        /// <summary>
        /// Document Property Test
        /// Documentation:  Gets or sets Document.
        /// Property Type:  XDocument
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void DocumentTest()
        {
            XDocument expected = new XDocument();
            _settings.Document = expected;
            Assert.AreEqual(expected, _settings.Document, "ClassLibrary.Settings.Document property test failed");
        }

        /// <summary>
        /// Full File Name Property Test
        /// Documentation:  Gets FullFileName.
        /// Property Type:  string
        /// Access       :  Read Only
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void FullFileNameTest()
        {
            string expected = "test";
            //TODO: Read Only Property, Delete this line and change the expected value ;
            Assert.AreEqual(expected, Settings.FullFileName, "ClassLibrary.Settings.FullFileName property test failed");
        }

        /// <summary>
        /// Generate Only Same Or Better Weighted Solutions Property Test
        /// Documentation:  Gets or sets a value indicating whether GenerateOnlySameOrBetterWeightedSolutions.
        /// Property Type:  bool
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void GenerateOnlySameOrBetterWeightedSolutionsTest()
        {
            bool expected = true;
            _settings.GenerateOnlySameOrBetterWeightedSolutions = expected;
            Assert.AreEqual(expected, _settings.GenerateOnlySameOrBetterWeightedSolutions, "ClassLibrary.Settings.GenerateOnlySameOrBetterWeightedSolutions property test failed");
        }

        /// <summary>
        /// Max Break Minutes Per Session Property Test
        /// Documentation:  Gets or sets MaxBreakMinutesPerSession.
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void MaxBreakMinutesPerSessionTest()
        {
            int expected = 123;
            _settings.MaxBreakMinutesPerSession = expected;
            Assert.AreEqual(expected, _settings.MaxBreakMinutesPerSession, "ClassLibrary.Settings.MaxBreakMinutesPerSession property test failed");
        }

        /// <summary>
        /// Max Number Of Courses Per Professor Property Test
        /// Documentation:  Gets or sets MaxNumberOfCoursesPerProfessor.
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void MaxNumberOfCoursesPerProfessorTest()
        {
            int expected = 123;
            _settings.MaxNumberOfCoursesPerProfessor = expected;
            Assert.AreEqual(expected, _settings.MaxNumberOfCoursesPerProfessor, "ClassLibrary.Settings.MaxNumberOfCoursesPerProfessor property test failed");
        }

        /// <summary>
        /// Max Number Of Generated Solutions Property Test
        /// Documentation:  Gets or sets MaxNumberOfGeneratedSolutions.
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void MaxNumberOfGeneratedSolutionsTest()
        {
            int expected = 123;
            _settings.MaxNumberOfGeneratedSolutions = expected;
            Assert.AreEqual(expected, _settings.MaxNumberOfGeneratedSolutions, "ClassLibrary.Settings.MaxNumberOfGeneratedSolutions property test failed");
        }

        /// <summary>
        /// Max Number Of Hours Per Course Property Test
        /// Documentation:  Gets or sets MaxNumberOfHoursPerCourse.
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void MaxNumberOfHoursPerCourseTest()
        {
            int expected = 123;
            _settings.MaxNumberOfHoursPerCourse = expected;
            Assert.AreEqual(expected, _settings.MaxNumberOfHoursPerCourse, "ClassLibrary.Settings.MaxNumberOfHoursPerCourse property test failed");
        }

        /// <summary>
        /// Number Of Preferences Per Professor Property Test
        /// Documentation:  Gets or sets NumberOfPreferencesPerProfessor.
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void NumberOfPreferencesPerProfessorTest()
        {
            int expected = 123;
            _settings.NumberOfPreferencesPerProfessor = expected;
            Assert.AreEqual(expected, _settings.NumberOfPreferencesPerProfessor, "ClassLibrary.Settings.NumberOfPreferencesPerProfessor property test failed");
        }

        /// <summary>
        /// Relax Count Constraint Property Test
        /// Documentation:  Gets or sets a value indicating whether RelaxCountConstraint.
        /// Property Type:  bool
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void RelaxCountConstraintTest()
        {
            bool expected = true;
            _settings.RelaxCountConstraint = expected;
            Assert.AreEqual(expected, _settings.RelaxCountConstraint, "ClassLibrary.Settings.RelaxCountConstraint property test failed");
        }

        /// <summary>
        /// Solving Method Property Test
        /// Documentation:  Gets or sets SolvingMethod.
        /// Property Type:  SolvingMethods
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SolvingMethodTest()
        {
            SolvingMethods expected = new SolvingMethods();
            _settings.SolvingMethod = expected;
            Assert.AreEqual(expected, _settings.SolvingMethod, "ClassLibrary.Settings.SolvingMethod property test failed");
        }

        /// <summary>
        /// This String Indexer Property Test
        /// Documentation:  Solving methods strings
        /// Property Type:  string[]
        /// Access       :  KS_PROPERTYACCESS
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void ThisStringIndexerTest()
        {
            string[] expected = new string[20];
            //TODO: Read Only Property, Delete this line and change the expected value ;
            //TODO: Write Only Property, Delete this line.  Assert.AreEqual(expected, _settings["Test"], "ClassLibrary.Settings.ThisStringIndexer property test failed");
        }

        /// <summary>
        /// Time Out Property Test
        /// Documentation:  Gets or sets TimeOut.
        /// Property Type:  long
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void TimeOutTest()
        {
            long expected = 123456L;
            _settings.TimeOut = expected;
            Assert.AreEqual(expected, _settings.TimeOut, "ClassLibrary.Settings.TimeOut property test failed");
        }

        /// <summary>
        /// Use Preferences Approach Property Test
        /// Documentation:  
        /// Property Type:  bool
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void UsePreferencesApproachTest()
        {
            bool expected = true;
            _settings.UsePreferencesApproach = expected;
            Assert.AreEqual(expected, _settings.UsePreferencesApproach, "ClassLibrary.Settings.UsePreferencesApproach property test failed");
        }


        #endregion

        #region Method Tests


        /// <summary>
        /// Save Method Test
        /// Documentation   :  Save method
        /// Method Signature:  void Save()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SaveTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameter Definitions
            
            //Default Value Test
             _settings.Save();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Settings.Save method default test failed");

            //Min Test
            
             _settings.Save();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Settings.Save method min test failed");

            //Max Test
            
             _settings.Save();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Settings.Save method max test failed");

            //Null Test
            
             _settings.Save();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Settings.Save method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Settings.Save Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Save Method Test
        /// Documentation   :  Saves this settings object to desired location
        /// Method Signature:  void Save(string fileName)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void Save1Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameter Definitions
            string fileName = "test";

            //Default Value Test
             _settings.Save(fileName);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Settings.Save1 method default test failed");

            //Min Test
            fileName = String.Empty;

             _settings.Save(fileName);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Settings.Save1 method min test failed");

            //Max Test
            fileName = string.Empty.PadRight(4097, 'K');

             _settings.Save(fileName);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Settings.Save1 method max test failed");

            //Null Test
            fileName = null;

             _settings.Save(fileName);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Settings.Save1 method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Settings.Save1 Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Settings Constructor Test
        /// Documentation   :  Initializes a new instance of the <see cref="Settings"/> class.
        /// Constructor Signature:  Settings()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SettingsConstructorTest()
        {
            System.DateTime constructorStartTime = System.DateTime.Now;
                        
            //Parameter Definitions
            
            //Default Value Test
             Settings settings = new Settings();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Settings.SettingsConstructor constructor default test failed");

            //Min Test
            
             Settings settings = new Settings();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Settings.SettingsConstructor constructor min test failed");

            //Max Test
            
             Settings settings = new Settings();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Settings.SettingsConstructor constructor max test failed");

            //Null Test
            
             Settings settings = new Settings();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Settings.SettingsConstructor constructor null test failed");

            System.TimeSpan constructorDuration = System.DateTime.Now.Subtract(constructorStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Settings.SettingsConstructor Time Elapsed: {0}", constructorDuration.ToString()));
        }


        #endregion

    }
}
