using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Spark.Web.Mvc;
using Spark;
using TAPS.MVC.Controllers;

namespace TAPS.MVC
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("elmah.axd");
			routes.MapRoute(
				"Default.mvc", // Route name
				"{controller}.MVC/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = "" }, // Parameter defaults
				new { controller = @"[^\.]*" } // Parameter constraints - Do not allow dots in the controller name
				);
			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = "" }, // Parameter defaults
				new { controller = @"[^\.]*" } // Parameter constraints - Do not allow dots in the controller name
				);

		}

		protected void Application_Start()
		{
			//ModelBinders.Binders[typeof(Course)] = new CourseBinder();
			//ModelBinders.Binders[typeof(Professor)] = new ProfessorBinder();
			RegisterRoutes(RouteTable.Routes);
			HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
			//ViewEngines.Engines.Clear();
			//ViewEngines.Engines.Add(new SparkViewFactory());

			//HibernatingRhinos.Profiler.Appender.LinqToSqlProfiler.Initialize();
		}

	}
}