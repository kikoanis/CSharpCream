using System;
using System.Linq;
using DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CourseAssignment.MVC.Tests
{
    /// <summary>
    /// Summary description for TestSolver
    /// </summary>
    [TestClass]
    public class TestSolver
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestRandomNumbers()
        {
            //var OldProofessors = 15;
            const int profCOunter = 15;
            Random ran;
            var array = new int[profCOunter];
            for (int i = 0; i < array.Length; i++) 
            {
                array[i] = -1;
            }
            
            for (int i = 0; i < array.Length; i++) 
            {
                int nextNumber;
                while (true) 
                {
                    ran = new Random();
                    nextNumber = ran.Next(0, profCOunter);
                	var number = nextNumber;
                	var found = array.Any(t => t == number);

					if (!found)
					{
						break;
					}
                }
                array[i] = nextNumber;
            }


        }

        [TestMethod]
        public void TestMethod1()
        {
        	var s = new CourseProvider();
        	s.GetAllCourses();
        }

        
    }
}
