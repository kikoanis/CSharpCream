// --------------------------------------------------------------------------------------------------------------------

// <copyright file="SolutionController.cs" company="U of R">

//   Ali Hmer 2009

// </copyright>

// <summary>

//   Defines the SolutionController type.

// </summary>

// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Web.Mvc;
using ClassLibrary;
using DataAccessLayer;

namespace TAPS.MVC.Controllers
{
	/// <summary>
	/// Controller class for solution
	/// </summary>
	public class SolutionController : Controller
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SolutionController"/> class.
		/// </summary>
		public SolutionController()
		{
			ViewData["Title"] = "Generating Solutions";
			ViewData["Message"] = "View / Generate Solutions";
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Generates this instance.
		/// </summary>
		/// <returns>a view of generate page</returns>
		public ActionResult Generate()
		{
			return View();
		}

		// [ValidateAntiForgeryToken]
		// [AcceptVerbs(HttpVerbs.Post)]
		// public object Index(FormCollection form) {
		//    if (form.AllKeys[0] == "Generate") {
		//        var solutions = new SolutionProvider();
		//        solutions.GenerateSolutions();
		//    }
		//    return RedirectToAction("Show/1");
		// }

		// public ActionResult Index() {
		//    Generate();
		//    return View("Show");
		// }

		// [ValidateAntiForgeryToken]
		// [AcceptVerbs(HttpVerbs.Post)]

		/// <summary>
		/// Shows the specified id.
		/// </summary>
		/// <param name="id">The id.of the solution</param>
		/// <returns>a view of show page</returns>
		public ActionResult Show(int id)
		{
			if (Session["UsedMethod"] == null)
			{
				GenerateSolutions();
			}

			var solutionProvider = new SolutionProvider();
			var allSolutions = solutionProvider.GetAllSolutions();
			if (id > allSolutions.Count && allSolutions.Count != 0)
			{
				return RedirectToAction("Show/" + allSolutions.Count);
			}

			if (id < 1)
			{
				return RedirectToAction("Show"+"/1");
			}

			var settings = new Settings();
			ViewData["DisplayConstraintsDetails"] = settings.DisplayConstraintsDetails;

			ViewData["TimeSpent"] = Session["TimeSpent"];
			ViewData["SolutionID"] = id;
			ViewData["MaxNumberOfGeneratedSolutions"] = Session["MaxNumberOfGeneratedSolutions"];
			ViewData["UsedMethod"] = Session["UsedMethod"];
			ViewData["NoOfGeneratedSolutions"] = Session["NoOfGeneratedSolutions"];
			ViewData["Title"] = "View/Generate Solutions";
			ViewData["Constraints"] = Session["Constraints"];
			ViewData["Message"] = "Solutions";
			ViewData["Weight"] = string.Empty;
			ViewData["SolutionNo"] = string.Empty;
			var singleSolution = solutionProvider.SolutionsBySolutionNo(id);
			ViewData["ProfessorsAssignedCourses"] = solutionProvider.GetAllProfessorsAssignedCourses(id);

			if (singleSolution.Count != 0)
			{
				ViewData["Weight"] = singleSolution[0].Weight;
				ViewData["SolutionNo"] = id;
				ViewData["Time"] = singleSolution[0].Time;
			}

			ViewData["AllSolutions"] = allSolutions;
			ViewData["singleSolution"] = singleSolution;
			return View("Show", singleSolution);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Generates the solutions.
		/// </summary>
		private void GenerateSolutions()
		{
			var solutions = new SolutionProvider();
			var settings = new Settings();
			solutions.MaxNumberOfSolutions = settings.MaxNumberOfGeneratedSolutions;
			solutions.UsedMethod = settings.SolvingMethod;
			solutions.TimeOut = settings.TimeOut;
			solutions.DistributeEvenly = settings.RelaxCountConstraint;
			solutions.UsePreferencesApproach = settings.UsePreferencesApproach;
			solutions.GenerateOnlySameOrBetterWeightedSolutions = settings.GenerateOnlySameOrBetterWeightedSolutions;
			var result = solutions.GenerateSolutions();
			if (result)
			{
				Session["Constraints"] = solutions.ConstraintsStrings.Select(s => s).OrderBy(s => s.ToLower());
				Session["TimeSpent"] = solutions.TimeSpent;
				Session["MaxNumberOfGeneratedSolutions"] = solutions.MaxNumberOfSolutions;
				Session["UsedMethod"] = " "+settings.SolvingMethodsStrings[(int) solutions.UsedMethod]+" ";
				Session["NoOfGeneratedSolutions"] = solutions.NoOfGeneratedSolutions;
				RedirectToAction("Show"+"/1");
			}
			else
			{
				Session["Constraints"] = solutions.ConstraintsStrings.Select(s => s).OrderBy(s => s.ToLower());
				Session["TimeSpent"] = (long)0;
				Session["MaxNumberOfGeneratedSolutions"] = (long)0;
				Session["UsedMethod"] = settings.SolvingMethodsStrings[(int)solutions.UsedMethod];
				Session["NoOfGeneratedSolutions"] = 0;
			}
			Session["FeedbackMessage"] = " " + solutions.FeedbackMessage + " ";
		}

		#endregion
	}
}


