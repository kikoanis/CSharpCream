using System.Web.Mvc;

namespace TAPS.MVC.Controllers
{
	[HandleError]
	public class ErrorController : Controller
	{
		public ErrorController()
		{
			ViewData["Message"] = "Error!";
			ViewData["Title"] = "Error!";
		}

		public object Index() 
		{
			return View();
		}
        
		public object RestrictedPage() 
		{
			return View();
		}
		public object PageNotFound() 
		{
			return View();
		}

	}
}


