using System;
using System.Web.Mvc;
using ClassLibrary;
using ClassLibrary.Rules;
using DataAccessLayer;
using TAPS.MVC.Controllers.Validators;

namespace TAPS.MVC.Controllers
{
	[HandleError]
	public class ProfessorController : Controller
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CourseController class.
		/// </summary>
		public ProfessorController()
		{
			ViewData["Title"] = "Professors management";
			ViewData["Message"] = "Create - Edit - Delete Professors";
		}

		#endregion

		#region Public Methods

		[ValidateAntiForgeryToken]
		[AcceptVerbs(HttpVerbs.Post)]
		public object Create([Bind(Prefix = "")]Professor professor)
		{
			ViewData["Title"] = "Create Professor";
			ViewData["Message"] = "Create a Professor";

			// redisplay form immediately if there are input format errors
			if (!ViewData.ModelState.IsValid)
			{
				ViewData.ModelState.CopyValidationExceptions(); 
				return View(professor);
			}

			try
			{
				new ProfessorProvider().AddProf(professor);
				ViewData["Message"] = "Professor created";
				return View("Saved");
			}
			catch (RuleViolationException vex)
			{
				ViewData.ModelState.CopyValidationExceptions(vex);
				return View("Create", professor);
			}
			catch (Exception err)
			{
				ViewData.ModelState.CopyValidationExceptions(err, "professor");
				return View(professor);
			}
		}

		public object Create()
		{
			ViewData["Title"] = "Create Profesor";
			ViewData["Message"] = "Create a Professor";
			var professor = new Professor();
			return View(professor);
		}

		public ActionResult Delete(string id)
		{
			var pp = new ProfessorProvider();
			if (!string.IsNullOrEmpty(id))
			{
				int profId;
				try
				{
					profId = Convert.ToInt32(id);
				}
				catch (Exception)   //not an integer
				{
					return RedirectToAction("Index", "Professor");
				}
				try
				{
					Professor professor = pp.GetProfessorByID(profId);
					if (professor != null)
					{
						pp.DeleteProf(professor);
						return View("Deleted");
					}
					return RedirectToAction("Index");
				}
				catch (Exception err)
				{
					if (err.InnerException != null &&
						err.InnerException.Message.Contains(
							"The DELETE statement conflicted with the REFERENCE constraint"))
					{
						ViewData["ErrorMessage"] =
							"Professor could not be deleted.. there is assoiciated data.. cannot delete";
					}
					else
					{
						ViewData["ErrorMessage"] = "Professor Could not be deleted.. there is a problem";
					}
					return View("NotDeleted");
				}
			}
			return RedirectToAction("Index");
		}

		[ValidateAntiForgeryToken]
		[AcceptVerbs(HttpVerbs.Post)]
		public object Edit(string id, Professor oldProfessor)
		{
			ViewData["Title"] = "Edit Professor";
			ViewData["Message"] = "Edit Professor Details!";
			var pp = new ProfessorProvider();
			int profId;
			try
			{
				profId = Convert.ToInt32(id);
			}
			catch (Exception)   //not an integer
			{
				return RedirectToAction("Index", "Professor");
			}
			Professor professor = pp.GetProfessorByID(profId);
			// redisplay form immediately if there are input format errors
			if (!ViewData.ModelState.IsValid)
			{
				ViewData.ModelState.CopyValidationExceptions();
				return View(oldProfessor);
			}
			try
			{
				UpdateModel(professor, new[] { "FirstName", "LastName", "UnassignedProf", "NoOfCourses", "PTitle" });
				pp.UpdateProf(professor);
				ViewData["Message"] = "Professor updated";
				return View("Saved");
			} 
			catch (RuleViolationException vex) 
			{
				ViewData.ModelState.CopyValidationExceptions(vex);
				return View("Edit", professor);
			}
			catch (Exception err)
			{
				ViewData.ModelState.CopyValidationExceptions(err, "professor"); 
				return View(professor);
			}
		}

		public ActionResult Edit(string id)
		{
			// default value if nothing found
			ActionResult result = View("Index");
			ViewData["Title"] = "Professor Edit";
			ViewData["Message"] = "Edit Professor Details!";
			if (!string.IsNullOrEmpty(id))
			{
				int profId;
				try
				{
					profId = Convert.ToInt32(id);
				}
				catch (Exception)   //not an integer
				{
					return RedirectToAction("Index", "Professor");
				}
				try
				{
					Professor professor = (new ProfessorProvider()).GetProfessorByID(profId);
					if (professor != null)
					{
						result = View(professor);
					}
				}
				catch (Exception)
				{
					result = View("Index");
				}
			}
			return result;
		}

		public ActionResult Index()
		{
			return View(new ProfessorProvider().GetAssignedProfessors());
		}

		#endregion

	}
}