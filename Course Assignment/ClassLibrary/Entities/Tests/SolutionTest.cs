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
// Module:       SolutionTest.cs
// Description:  Tests for the Solution class in the ClassLibrary assembly.
//  
// Date:       Author:           Comments:
// 21/08/2010 5:09 PM  Ali     Module created.
///////////////////////////////////////////////////////////////////////////////
namespace ClassLibraryTest
{

    /// <summary>
    /// Tests for the Solution Class
    /// Documentation: Class Solution
    /// </summary>
    [TestFixture(Description="Tests for Solution")]
    public class SolutionTest
    {
        #region Class Variables
        private Solution _solution = null;
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
            //New instance of Solution
            _solution = new Solution();
        }

        /// <summary>
        /// Code that is run after each test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            //TODO:  Put dispose in here for _solution or delete this line
        }
        #endregion

        #region Property Tests


        /// <summary>
        /// Course Property Test
        /// Documentation:  
        /// Property Type:  string
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void CourseTest()
        {
            string expected = "test";
            _solution.Course = expected;
            Assert.AreEqual(expected, _solution.Course, "ClassLibrary.Solution.Course property test failed");
        }

        /// <summary>
        /// Id Property Test
        /// Documentation:  
        /// Property Type:  Guid
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void IdTest()
        {
            Guid expected = new Guid();
            _solution.Id = expected;
            Assert.AreEqual(expected, _solution.Id, "ClassLibrary.Solution.Id property test failed");
        }

        /// <summary>
        /// Is Changed Property Test
        /// Documentation:  
        /// Property Type:  bool
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void IsChangedTest()
        {
            bool expected = true;
            _solution.IsChanged = expected;
            Assert.AreEqual(expected, _solution.IsChanged, "ClassLibrary.Solution.IsChanged property test failed");
        }

        /// <summary>
        /// Is Deleted Property Test
        /// Documentation:  
        /// Property Type:  bool
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void IsDeletedTest()
        {
            bool expected = true;
            _solution.IsDeleted = expected;
            Assert.AreEqual(expected, _solution.IsDeleted, "ClassLibrary.Solution.IsDeleted property test failed");
        }

        /// <summary>
        /// Professor Property Test
        /// Documentation:  
        /// Property Type:  string
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void ProfessorTest()
        {
            string expected = "test";
            _solution.Professor = expected;
            Assert.AreEqual(expected, _solution.Professor, "ClassLibrary.Solution.Professor property test failed");
        }

        /// <summary>
        /// Solution No Property Test
        /// Documentation:  
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SolutionNoTest()
        {
            int expected = 123;
            _solution.SolutionNo = expected;
            Assert.AreEqual(expected, _solution.SolutionNo, "ClassLibrary.Solution.SolutionNo property test failed");
        }

        /// <summary>
        /// Time Property Test
        /// Documentation:  
        /// Property Type:  long
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void TimeTest()
        {
            long expected = 123456L;
            _solution.Time = expected;
            Assert.AreEqual(expected, _solution.Time, "ClassLibrary.Solution.Time property test failed");
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
            _solution.Version = expected;
            Assert.AreEqual(expected, _solution.Version, "ClassLibrary.Solution.Version property test failed");
        }

        /// <summary>
        /// Weight Property Test
        /// Documentation:  
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void WeightTest()
        {
            int expected = 123;
            _solution.Weight = expected;
            Assert.AreEqual(expected, _solution.Weight, "ClassLibrary.Solution.Weight property test failed");
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
            results = _solution.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.Solution.Clone method default test failed");

            //Min Test
            
            results = _solution.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.Solution.Clone method min test failed");

            //Max Test
            
            results = _solution.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.Solution.Clone method max test failed");

            //Null Test
            
            results = _solution.Clone();
            Assert.AreEqual(expected, results, "ClassLibrary.Solution.Clone method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Solution.Clone Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Deserialize From File< Solution> Method Test
        /// Documentation   :  Method for entity class deserialization from XML file. Does not change this object content but returns another deserialized object instance
        /// Method Signature:  Solution DeserializeFromFile<Solution>(string xmlFilePath)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void DeserializeFromFile<Solution>Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            Solution expected = new Solution();
            Solution results;

            //Parameter Definitions
            string xmlFilePath = "test";

            //Default Value Test
            results = _solution.DeserializeFromFile<Solution>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.Solution.DeserializeFromFile<Solution> method default test failed");

            //Min Test
            xmlFilePath = String.Empty;

            results = _solution.DeserializeFromFile<Solution>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.Solution.DeserializeFromFile<Solution> method min test failed");

            //Max Test
            xmlFilePath = string.Empty.PadRight(4097, 'K');

            results = _solution.DeserializeFromFile<Solution>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.Solution.DeserializeFromFile<Solution> method max test failed");

            //Null Test
            xmlFilePath = null;

            results = _solution.DeserializeFromFile<Solution>(xmlFilePath);
            Assert.AreEqual(expected, results, "ClassLibrary.Solution.DeserializeFromFile<Solution> method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Solution.DeserializeFromFile<Solution> Time Elapsed: {0}", methodDuration.ToString()));
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
            results = _solution.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.Solution.Equals method default test failed");

            //Min Test
            obj = new object();

            results = _solution.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.Solution.Equals method min test failed");

            //Max Test
            obj = new object();

            results = _solution.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.Solution.Equals method max test failed");

            //Null Test
            obj = null;

            results = _solution.Equals(obj);
            Assert.AreEqual(expected, results, "ClassLibrary.Solution.Equals method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Solution.Equals Time Elapsed: {0}", methodDuration.ToString()));
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
            results = _solution.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.Solution.GetHashCode method default test failed");

            //Min Test
            
            results = _solution.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.Solution.GetHashCode method min test failed");

            //Max Test
            
            results = _solution.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.Solution.GetHashCode method max test failed");

            //Null Test
            
            results = _solution.GetHashCode();
            Assert.AreEqual(expected, results, "ClassLibrary.Solution.GetHashCode method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Solution.GetHashCode Time Elapsed: {0}", methodDuration.ToString()));
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
             _solution.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Solution.SerializeToFile method default test failed");

            //Min Test
            xmlFilePath = String.Empty;

             _solution.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Solution.SerializeToFile method min test failed");

            //Max Test
            xmlFilePath = string.Empty.PadRight(4097, 'K');

             _solution.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Solution.SerializeToFile method max test failed");

            //Null Test
            xmlFilePath = null;

             _solution.SerializeToFile(xmlFilePath);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Solution.SerializeToFile method null test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Solution.SerializeToFile Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Solution Constructor Test
        /// Documentation   :  
        /// Constructor Signature:  Solution()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SolutionConstructor1Test()
        {
            System.DateTime constructorStartTime = System.DateTime.Now;
                        
            //Parameter Definitions
            
            //Default Value Test
             Solution solution = new Solution();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Solution.SolutionConstructor1 constructor default test failed");

            //Min Test
            
             Solution solution = new Solution();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Solution.SolutionConstructor1 constructor min test failed");

            //Max Test
            
             Solution solution = new Solution();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Solution.SolutionConstructor1 constructor max test failed");

            //Null Test
            
             Solution solution = new Solution();
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Solution.SolutionConstructor1 constructor null test failed");

            System.TimeSpan constructorDuration = System.DateTime.Now.Subtract(constructorStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Solution.SolutionConstructor1 Time Elapsed: {0}", constructorDuration.ToString()));
        }

        /// <summary>
        /// Solution Constructor Test
        /// Documentation   :  course field
        /// Constructor Signature:  Solution(Guid pId)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SolutionConstructor2Test()
        {
            System.DateTime constructorStartTime = System.DateTime.Now;
                        
            //Parameter Definitions
            Guid pId = new Guid();

            //Default Value Test
             Solution solution = new Solution(pId);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Solution.SolutionConstructor2 constructor default test failed");

            //Min Test
            pId = new Guid();

             Solution solution = new Solution(pId);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Solution.SolutionConstructor2 constructor min test failed");

            //Max Test
            pId = new Guid();

             Solution solution = new Solution(pId);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Solution.SolutionConstructor2 constructor max test failed");

            //Null Test
            pId = null;

             Solution solution = new Solution(pId);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Solution.SolutionConstructor2 constructor null test failed");

            System.TimeSpan constructorDuration = System.DateTime.Now.Subtract(constructorStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Solution.SolutionConstructor2 Time Elapsed: {0}", constructorDuration.ToString()));
        }

        /// <summary>
        /// Solution Constructor Test
        /// Documentation   :  
        /// Constructor Signature:  Solution(int pSolutionNo, string pCourse, string pProfessor, int pWeight, long pTime)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SolutionConstructor3Test()
        {
            System.DateTime constructorStartTime = System.DateTime.Now;
                        
            //Parameter Definitions
            int pSolutionNo = 123;
			string pCourse = "test";
			string pProfessor = "test";
			int pWeight = 123;
			long pTime = 123456L;

            //Default Value Test
             Solution solution = new Solution(pSolutionNo, pCourse, pProfessor, pWeight, pTime);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Solution.SolutionConstructor3 constructor default test failed");

            //Min Test
            pSolutionNo = int.MinValue;
			pCourse = String.Empty;
			pProfessor = String.Empty;
			pWeight = int.MinValue;
			pTime = long.MinValue;

             Solution solution = new Solution(pSolutionNo, pCourse, pProfessor, pWeight, pTime);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Solution.SolutionConstructor3 constructor min test failed");

            //Max Test
            pSolutionNo = int.MaxValue;
			pCourse = string.Empty.PadRight(4097, 'K');
			pProfessor = string.Empty.PadRight(4097, 'K');
			pWeight = int.MaxValue;
			pTime = long.MaxValue;

             Solution solution = new Solution(pSolutionNo, pCourse, pProfessor, pWeight, pTime);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Solution.SolutionConstructor3 constructor max test failed");

            //Null Test
            pSolutionNo = 123;
			pCourse = null;
			pProfessor = null;
			pWeight = 123;
			pTime = 123456L;

             Solution solution = new Solution(pSolutionNo, pCourse, pProfessor, pWeight, pTime);
            Assert.AreEqual(String.Empty, String.Empty, "ClassLibrary.Solution.SolutionConstructor3 constructor null test failed");

            System.TimeSpan constructorDuration = System.DateTime.Now.Subtract(constructorStartTime);
            System.Console.WriteLine(String.Format("ClassLibrary.Solution.SolutionConstructor3 Time Elapsed: {0}", constructorDuration.ToString()));
        }


        #endregion

    }
}
