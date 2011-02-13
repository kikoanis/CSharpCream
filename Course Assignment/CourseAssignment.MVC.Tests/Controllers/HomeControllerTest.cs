using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseAssignment.MVC;
using TAPS.MVC.Controllers;

namespace CourseAssignment.MVC.Tests.Controllers
{
    /// <summary>
    /// Summary description for HomeControllerTest
    /// </summary>
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Setup
            HomeController controller = new HomeController();

            // Execute
            ViewResult result = controller.Index() as ViewResult;

            // Verify
            ViewDataDictionary viewData = result.ViewData;
            Assert.AreEqual("Home Page", viewData["Title"]);
            Assert.AreEqual("Welcome to ASP.NET MVC!", viewData["Message"]);
        }

        [TestMethod]
        public void About()
        {
            // Setup
            HomeController controller = new HomeController();

            // Execute
            ViewResult result = controller.About() as ViewResult;

            // Verify
            ViewDataDictionary viewData = result.ViewData;
            Assert.AreEqual("About Page", viewData["Title"]);
        }
    }
}
