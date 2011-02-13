// --------------------------------------------------------------------------------------------------------------------- 

// <copyright file="CourseController.cs" company="PC">

//   Ali Hmer 2008-2009

// </copyright>

// <summary>

//   Defines the CourseController type.

// </summary>

// ---------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ClassLibrary;
using ClassLibrary.Rules;
using DataAccessLayer;
using TAPS.MVC.Controllers.Validators;

namespace TAPS.MVC.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[HandleError]
	public class CourseController : Controller
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CourseController class.
		/// </summary>
		public CourseController()
		{
			ViewData["Title"] = "Course management";
			ViewData["Message"] = "Create - Edit - Delete Courses";
		}

		#endregion

		#region Public Methods

		[ValidateAntiForgeryToken]
		[AcceptVerbs(HttpVerbs.Post)]
		public object Create([Bind(Prefix = "")]Course course, string ProfessorList)
		{
			ViewData["Title"] = "Create Course";
			ViewData["Message"] = "Create a Course";
			//To DO
			//UpdateModel(course);  // to do 

			// redisplay form immediately if there are input format errors
			if (!ModelState.IsValid)
			{
				ModelState.CopyValidationExceptions();
				PrepareProfessorList(course);
				return View("Create", course);
			}

			try
			{
				if (ProfessorList != null)
					if (!ProfessorList.Equals("0"))
					{
						var profp = new ProfessorProvider();
						Professor prof = profp.GetProfessorByID(Convert.ToInt32(ProfessorList));
						course.AssignedProfessor = prof;
					}
				new CourseProvider().AddCourse(course);
				ViewData["Message"] = "Course created";
				return View("Saved");
			}
			catch (RuleViolationException vex)
			{
				ViewData.ModelState.CopyValidationExceptions(vex);
				PrepareProfessorList(course);
				return View("Create", course);
			}
			catch (Exception err)
			{
				ViewData.ModelState.CopyValidationExceptions(err, "course");
				PrepareProfessorList(course);
				return View("Create");
			}
		}

		public object Create()
		{
			ViewData["Title"] = "Create Course";
			ViewData["Message"] = "Create a Course";
			var course = new Course();
			PrepareProfessorList(course);
			return View(course);
		}

		public ActionResult Delete(string id)
		{
			var cp = new CourseProvider();
			if (!string.IsNullOrEmpty(id))
			{
				int courseId;
				try
				{
					courseId = Convert.ToInt32(id);
				}
				catch (Exception)   //not an integer
				{
					return RedirectToAction("Index", "Course");
				}

				try
				{
					Course course = cp.GetCourseByID(courseId);
					if (course != null)
					{
						cp.DeleteCourse(course);
						return View("Deleted");
					}
					return RedirectToAction("Index");
				}
				catch (Exception err)
				{
					if (err.InnerException != null && err.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
					{
						ViewData["ErrorMessage"] = 
							"Course could not be deleted.. there are assoiciated data.. cannot delete";
					}
					else
					{
						ViewData["ErrorMessage"] = 
							"Course Could not be deleted.. there is a problem";
					}
					return View("NotDeleted");
				}
			}
			return RedirectToAction("Index");
		}

		[ValidateAntiForgeryToken]
		[AcceptVerbs(HttpVerbs.Post)]
		public object Edit([Bind(Prefix = "")] Course course, string id, string ProfessorList)
		{
			ViewData["Title"] = "Edit Course";
			ViewData["Message"] = "Edit Course Details!";
			var cp = new CourseProvider();
			int courseId;
			try
			{
				courseId = Convert.ToInt32(id);
			}
			catch (Exception)   //not an integer
			{
				return RedirectToAction("Index", "Course");
			}

			course = cp.GetCourseByID(courseId);
			// redisplay form immediately if there are input format errors
			if (!ModelState.IsValid)
			{
				ModelState.CopyValidationExceptions();
				PrepareProfessorList(course); 
				return View("Edit", course);
			}
			try {
				UpdateModel(course, new[] { "CourseName", "Monday", "Tuesday", "Wednesday",
				                            "Thursday", "Friday", "Saturday", "Sunday", 
				                            "StartHour", "StartMinute", "EndHour", "EndMinute"});
				if (!ProfessorList.Equals("0")) {
					Professor prof = new ProfessorProvider().GetProfessorByID(Convert.ToInt32(ProfessorList));
					course.AssignedProfessor = prof;
				} else {
					course.AssignedProfessor = null;
				}
				cp.UpdateCourse(course);
				return View("Saved");
			} catch (RuleViolationException vex) {
				ViewData.ModelState.CopyValidationExceptions(vex);
				PrepareProfessorList(course);
				return View(course);
			} 
			catch (Exception err) {
				ViewData.ModelState.CopyValidationExceptions(err, "course");
				PrepareProfessorList(course);
				return View(course);
			}
		}

		public ActionResult Edit(string id)
		{
			// default value if nothing found
			ActionResult result = View("Index"); // = RedirectToAction("Index");
			ViewData["Title"] = "Course Edit";
			ViewData["Message"] = "Edit Course Details!";
			int courseId;
			try
			{
				courseId = Convert.ToInt32(id);
			}
			catch (Exception) //not an integer
			{
				return RedirectToAction("Index", "Course");
			}

			try
			{
				Course course = (new CourseProvider()).GetCourseByID(courseId);
				if (course != null)
				{
					PrepareProfessorList(course);
					result = View(course);
				}
			}
			catch (Exception)
			{
				result = View("Index");
			}
			return result;
		}

		//[OutputCache(VaryByParam = "none", Duration = 3600)]
		public ActionResult Index()
		{
			//ViewData["CourseGridView"] = (new CourseProvider()).GetAllCourses();
			return View((new CourseProvider()).GetAllCourses());
		}

		#endregion

		#region Private Methods

		private static Professor AddDummyProf()
		{
			var p = new Professor { FirstName = "None" };
			return p;
		}

		private void PrepareProfessorList(Course course)
		{
			IList<Professor> pl = (new ProfessorProvider()).GetAssignedProfessors();
			pl.Add(AddDummyProf());
			if (course != null)
			{
				ViewData["ProfessorList"] =
					course.AssignedProfessor != null
						?
							new SelectList(pl, "ProfId", "FullName", course.AssignedProfessor.ProfId)
						:
							new SelectList(pl, "ProfId", "FullName", "0");
			}
			else
			{
				ViewData["ProfessorList"] = new SelectList(pl, "ProfId", "FullName", "0"); 
			}
		}

		#endregion

	}
}


