// --------------------------------------------------------------------------------------------------------------------

// <copyright file="SettingsController.cs" company="U of R">

//   Ali Hmer 2009

// </copyright>

// <summary>

//   Defines the SettingsController type.

// </summary>

// --------------------------------------------------------------------------------------------------------------------


using TAPS.MVC.Controllers.Validators;
using System;
using System.Web.Mvc;
using ClassLibrary;
using ClassLibrary.Rules;

namespace TAPS.MVC.Controllers
{
	/// <summary>
	/// A controller class for Settings
	/// </summary>
	[HandleError]
	public class SettingsController : Controller
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsController"/> class.
		/// </summary>
		public SettingsController()
		{
			ViewData["Message"] = "Application Settings";
			ViewData["Title"] = "Settings Edit";
		}

		/// <summary>
		/// Indexes this instance.
		/// </summary>
		/// <returns>a view of Index page</returns>
		public object Index()
		{
			var settings = new Settings();
			return View(settings);
		}

		/// <summary>
		/// Indexes the specified settings.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <returns>return view of saved page on post or of index page on exception</returns>
		[ValidateAntiForgeryToken]
		[AcceptVerbs(HttpVerbs.Post)]
		public object Index([Bind(Prefix = "")] Settings settings)
		{
			// redisplay form immediately if there are input format errors
			if (!ModelState.IsValid)
			{
				ModelState.CopyValidationExceptions();
				return View("Index", settings);
			}

			try
			{
				settings.Save();
				 
				return View("Saved");
			}
			catch (RuleViolationException vex)
			{
				ViewData.ModelState.CopyValidationExceptions(vex);
				return View("Index", settings);
			}
			catch (Exception)
			{
				return View("Index", settings);
			}
		}
	}
}


