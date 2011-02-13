using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TAPS.MVC.Controllers
{
	[HandleError]
	public class HomeController : Controller
	{
		[OutputCache(VaryByParam = "none", Duration = 3600)]

		public ActionResult Index()
		{
			ViewData["Title"] = "Home Page";
			ViewData["Message"] = "Welcome to Teaching Assignment Problem Solver!";

			return View();
		}


		[OutputCache(VaryByParam = "none", Duration = 3600)]

		public ActionResult About()
		{
			ViewData["Title"] = "About Page";

			return View();
		}
	}
}
