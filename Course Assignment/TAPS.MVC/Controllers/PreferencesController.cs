using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ClassLibrary;
using ClassLibrary.Rules;
using DataAccessLayer;
using System.Linq;
using TAPS.MVC.Controllers.Validators;

namespace TAPS.MVC.Controllers
{
	[HandleError]
	public class PreferencesController : Controller {

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CourseController class.
		/// </summary>
		public PreferencesController() {
			ViewData["Title"] = "Preferences management";
			ViewData["Message"] = "Create - Edit - Delete Preferences";
		}

		#endregion

		#region Public Methods

		public object Create(string Id) {
			ViewData["Title"] = "Create Preference";
			ViewData["Message"] = "Create a Preference";
			if (string.IsNullOrEmpty(Id)) {
				return RedirectToAction("Index", "Professor");

			}
			int profId;
			try {
				profId = Convert.ToInt32(Id);
			} catch (Exception)   //not an integer
			{
				return RedirectToAction("Index", "Professor");
			}
			Professor professor = new ProfessorProvider().GetProfessorByID(profId);
			if (professor == null) {
				return RedirectToAction("Index", "Professor");
			}
			var ProfPrefs = (new PreferencesProvider().GetAllPreferencesByProfId(profId));
			var settings = new Settings();
			int lookFor = settings.NumberOfPreferencesPerProfessor;
			while (true)
			{
				int lookFor1 = lookFor;
				if (ProfPrefs.Where(s => s.Weight == lookFor1).Count() == 0)
				{
					break;
				}
				lookFor--;
				if (lookFor == 0)
				{
					break;
				}
			}
			int weight = lookFor;
			var preference = new Preference
			                 	{
			                 		Id = { Professor = professor },
			                 		Weight = weight
			                 	};
			PrepareCoursesList(preference);
			ViewData["ProfId"] = preference.Id.Professor.ProfId;
			return View(preference);
		}

		[ValidateAntiForgeryToken]
		[AcceptVerbs(HttpVerbs.Post)]
		public object Create([Bind(Prefix = "")]Preference preference, string CourseList, string ProfId)
		{
			ViewData["Title"] = "Create Preference";
			ViewData["Message"] = "Create a Preference";
			try
			{
				Course course = (new CourseProvider()).GetCourseByID(Convert.ToInt32(CourseList));
				Professor professor = new ProfessorProvider().GetProfessorByID(Convert.ToInt32(ProfId));
				preference.Id = new PreferencesId {Professor = professor, Course = course};
				new PreferencesProvider().AddPreference(preference);
				ViewData["Message"] = "Preference created";
				ViewData["ProfId"] = preference.Id.Professor.ProfId;
				return View("Saved");
			}
			catch (RuleViolationException vex)
			{
				ViewData.ModelState.CopyValidationExceptions(vex);
				ViewData["ProfId"] = preference.Id.Professor.ProfId;
				PrepareCoursesList(preference);
				return View(preference);
			}
			catch (Exception err)
			{
				ViewData.ModelState.CopyValidationExceptions(err, "preference");
				ViewData["ProfId"] = preference.Id.Professor.ProfId;
				PrepareCoursesList(preference);
				return View(preference);
			}
		}

		public ActionResult Delete(string prof, string course) {
			int profId, courseId;
			try {
				profId = Convert.ToInt32(prof);
				ViewData["id"] = prof;
			} catch (Exception)   //not an integer
			{
				return RedirectToAction("Index", "Professor");
			}
			try {
				courseId = Convert.ToInt32(course);
			} catch (Exception)   //not an integer
			{
				return this.RedirectToAction(c => c.Show(prof));
			}

			try {
				var prefProvider = new PreferencesProvider();
				Preference preference = prefProvider.GetPreferenceByProfIdAndCourseId(profId, courseId);
				if (preference != null) {
					prefProvider.DeletePreference(preference);
					return View("Deleted");
				}
				return this.RedirectToAction(c => c.Show(prof));
			} catch (Exception err) {
				if (err.InnerException != null &&
				    err.InnerException.Message.Contains(
				    	"The DELETE statement conflicted with the REFERENCE constraint")) {
				    		ViewData["ErrorMessage"] =
				    			"Course could not be deleted.. there are assoiciated data.. cannot delete";
				    	} else {
				    		ViewData["ErrorMessage"] = "Preference Could not be deleted.. there is a problem";
				    	}
				return View("NotDeleted");
			}
		}

		[ValidateAntiForgeryToken]
		[AcceptVerbs(HttpVerbs.Post)]
		public object Edit([Bind(Prefix = "")]Preference preference, string course,
		                   string prof, string CourseList) {
			ViewData["Title"] = "Edit Preference";
			ViewData["Message"] = "Edit a Preference";

			// redisplay form immediately if there are input format errors
			if (!ViewData.ModelState.IsValid)
				return View(preference);
			int profId;
			try {
				profId = Convert.ToInt32(prof);
				ViewData["id"] = prof;
			} catch (Exception) {  //not an integer
				return RedirectToAction("Index", "Professor");
			}
			int courseId;
			try {
				courseId = Convert.ToInt32(course);
			} catch (Exception) { //not an integer
				return this.RedirectToAction(c => c.Show(prof));
			}
			try {
				var prefProv = new PreferencesProvider();
				Preference pref = prefProv.GetPreferenceByProfIdAndCourseId(profId, courseId);
				if (pref != null) {
					Course cour = (new CourseProvider()).GetCourseByID(Convert.ToInt32(CourseList));
					pref.Id.Course = cour;
					pref.Weight = preference.Weight;
					prefProv.UpdatePreference(pref);
					return View("Saved");
				}
				return this.RedirectToAction(c => c.Show(prof));
			} catch (RuleViolationException vex) {
				ViewData.ModelState.CopyValidationExceptions(vex);
				PrepareCoursesList(preference);
				return View("Edit", preference);
			} catch (Exception err) {
				ViewData.ModelState.CopyValidationExceptions(err, "preference");
				PrepareCoursesList(preference);
				return View("Edit", preference);
			}
		                   }

		//[ValidateAntiForgeryToken]
		public ActionResult Edit(string Course, string Prof) {
			ViewData["Title"] = "Edit Preference";
			ViewData["Message"] = "Edit a Preference";
			int profID;
			try {
				profID = Convert.ToInt32(Prof);
				ViewData["id"] = Prof;
			} catch (Exception)   //not an integer
			{
				return RedirectToAction("Index", "Professor");
			}
			int courseId;
			try {
				courseId = Convert.ToInt32(Course);
			} catch (Exception)   //not an integer
			{
				return this.RedirectToAction(c => c.Show(Prof));
			}
			try {
				var preference = (new PreferencesProvider()).GetPreferenceByProfIdAndCourseId(profID, courseId);
				PrepareCoursesList(preference);
				return View(preference);
			} catch (Exception) {
				return this.RedirectToAction(c => c.Show(Prof));
			}
		}

		public object Show(string Id) {
			ViewData["id"] = Id;
			int prefId;
			try {
				prefId = Convert.ToInt32(Id);
			} catch (Exception) //not an integer
			{
				return RedirectToAction("Index", "Professor");
			}
			var ProfPrefs = (new PreferencesProvider().GetAllPreferencesByProfId(prefId));
			var settings = new Settings();
			if (ProfPrefs != null) {
				ViewData["NoMore"] = ProfPrefs.Count == settings.NumberOfPreferencesPerProfessor;
			}
			Professor professor = (new ProfessorProvider()).GetProfessorByID(prefId);
			if (professor != null) {
				ViewData["Message"] = "Create - Delete Preferences for [" + professor.FullName + "]";
			} else {
				return RedirectToAction("Index", "Professor");
			}
			return View(ProfPrefs);
		}

		#endregion

		#region Private Methods

		private void PrepareCoursesList(Preference preference)
		{
			IList<Course> pl = (new CourseProvider()).GetAllCourses();
			//ViewData["CourseList"] = preference.Id.Course.CourseId != 0
			//                             ? new SelectList(pl, "CourseId", "CourseName", preference.Id.Course.CourseId)
			//                             : new SelectList(pl, "CourseId", "CourseName", "0");
			if (preference.Id != null)
			{
				ViewData["CourseList"] = preference.Id.Course.CourseId != 0
				                         	? new SelectList(pl, "CourseId", "CourseName",
				                         	                 preference.Id.Course.CourseId)
				                         	: new SelectList(pl, "CourseId", "CourseName", "0");
			}
			else
			{
				ViewData["CourseList"] = new SelectList(pl, "CourseId", "CourseName", "0");
			}
		}

		#endregion
	}
}