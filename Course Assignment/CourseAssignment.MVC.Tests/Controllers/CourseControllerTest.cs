using System.Collections.Generic;
using System.Linq;
using ClassLibrary;
using System.Web.Mvc;
using TAPS.MVC.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseAssignment.MVC.Tests.Controllers
{
    /// <summary>
    /// Summary description for CourseControllerTest
    /// </summary>
    [TestClass]
    public class CourseControllerTest
    {
        public CourseControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        public void CourseController_Should_return_Courses()
        {
            var c = new CourseController();
            var result = c.Index() as ViewResult;
            Assert.IsNotNull(result);

            var viewData = result.ViewData;
            var courses = result.ViewData.Model as IEnumerable<Course>;
            Assert.AreEqual("Course management", viewData["Title"], "Expected different title");
            Assert.AreEqual("Create - Edit - Delete Courses", viewData["Message"], "Expected different message");
            Assert.IsNotNull(courses, "Expected sequence of courses");
            Assert.AreEqual(40, courses.Count(), "Expected 26 courses");

        }

        [TestMethod]
        public void CourseController_Should_Contain_Index_Method_Which_Takes_No_Argument()
        {
            CourseController c = new CourseController();
            ActionResult result = c.Index();
            Assert.IsNotNull(result, "Expected courses");

        }

        [TestMethod]
        public void CourseController_Should_not_Edit_Unknown_Courses()
        {
            CourseController c = new CourseController();
            var result = c.Edit("245345") as ViewResult;
            Assert.IsNull(result.ViewData.Model, "Expected null");
        }

        [TestMethod]
        public void CourseController_Should_Edit_Known_Courses()
        {
            CourseController c = new CourseController();
            var result = c.Edit("2") as ViewResult;
            Assert.IsNotNull(result.ViewData.Model, "Expected a course");
        }

        [TestMethod]
        public void CourseController_should_return_all_Prof_and_selected_index()
        {
            CourseController c = new CourseController();
            var result = c.Edit("3") as ViewResult;
            Assert.IsNotNull(result);
            var viewData = result.ViewData;
            var course = result.ViewData.Model as Course;
            Assert.AreEqual("Course Edit", viewData["Title"], "Expected different title");
            Assert.AreEqual("Edit Course Details!", viewData["Message"], "Expected different message");
            Assert.AreEqual(0, viewData["SelectedIndex"], "Expected different selected index");
            Assert.AreEqual(20, (viewData["AllProfsView"] as IEnumerable<Professor>).Count(), "Expected different count");
            Assert.IsNotNull(course, "Expected a course");
        }

        [TestMethod]
        public void CourseController_should_Edit_And_Save()
        {
            CourseController controller = new CourseController();

            //FormCollection form = new FormCollection();

            //form["CourseName"] = "CSTesting";
            //form["Monday"] = "True";
            //form["StartHour"] = "14";

            var course = new Course
                             {
                                 CourseName = "CSTesting",
                                 Monday = true,
                                 StartHour = 14,
                                 EndHour = 17,
                                 AssignedProfessor = null
                             };
            var result = controller.Edit(course, "139", "3");

            Assert.IsNotNull(result, "Expected client side redirect");
            Assert.AreEqual("Course Updated",controller.ViewData["Message"], "Expected another message!!");
            //Assert.AreEqual(controller.);

        }
    }
}
