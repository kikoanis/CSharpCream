using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ClassLibrary.Rules;

namespace TAPS.MVC.Controllers.Validators
{
	public static class ModelStateDictionaryExtensions
	{
		public static void CopyValidationExceptions(this ModelStateDictionary modelState, RuleViolationException validationException) {
			foreach (var vex in validationException.RuleValidationIssues) {
				modelState.AddModelError(vex.PropertyName, vex.ErrorMessage);
			}

		}

		public static void CopyValidationExceptions(this ModelStateDictionary modelState, Exception err, string name) {
			switch (name.ToLower())
			{
				case "course":

					if (err.InnerException != null)
					{
						if (err.InnerException.Message.Contains("Cannot insert duplicate key row "))
						{
							modelState.AddModelError("CourseName",
							                         "Course Name Should be unique.. another course has the same name!!");
						}
						else
						{
							if (err.InnerException.Message != null)
							{
								modelState.AddModelError("CourseId",
								                         "A problem occurred.. please try again!!" +
								                         err.InnerException.Message);
							}
							else
							{
								modelState.AddModelError("CourseId",
								                         "A problem occurred.. please try again!!");

							}
						}
					}
					else if (err.Message != null)
					{
						modelState.AddModelError("CourseId",
						                         "A problem occurred.. please try again!! Another process updated or deleted this course");
					}
					break;
				case "professor":
					if (err.InnerException != null) {
						if (err.InnerException.Message.Contains("Cannot insert duplicate key row ")) {
							modelState.AddModelError("FirstName",
							                         "Professor Name Should be unique.. another professor has the same name!!");
						} else {
							if (err.InnerException.Message != null)
								modelState.AddModelError("ProfId",
								                         "A problem occurred.. please try again!!" +
								                         err.InnerException.Message);
							else {
								modelState.AddModelError("ProfId",
								                         "A problem occurred.. please try again!!");
							}
						}
					}
					else if (err.Message != null) {
						modelState.AddModelError("ProfId",
						                         "A problem occurred.. please try again!! Another process updated or deleted this professor");
					}
					break;
				case "preference":
					if (err.InnerException != null) {
						if (err.InnerException.Message.Contains("Violation of PRIMARY KEY constraint")) {
							modelState.AddModelError("CourseList",
							                         "Course as a preference should be selected only once for this professor!!");
						} else {
							modelState.AddModelError("ProfId",
							                         "A problem occurred.. please try again!!");
						}
					} else if (err.Message != null)
					{
						modelState.AddModelError("ProfId",
						                         "A problem occurred.. please try again!! Another process updated or deleted this preference");
					}
					break;
				default:
					modelState.AddModelError("",
					                         "A problem occurred.. please try again!! ");
					break;
			}


		}

		public static void CopyValidationExceptions(this ModelStateDictionary modelState)
		{
			var errorMesages = new Dictionary<string, string>();
			int count = 0;
			IList<string> list = new List<string>();
			foreach (var key in modelState.Keys)
			{
				list.Add(key);
			}

			foreach (ModelState value in modelState.Values)
			{
				foreach (ModelError error in value.Errors)
				{
					if (error.Exception != null)
						errorMesages.Add(list[count], error.Exception.InnerException.Message+" ["+list[count]+"]");
				}
				count++;
			}
			foreach (var message in errorMesages)
			{
				modelState.AddModelError(message.Key, message.Value);
			}
		}

	}
}


