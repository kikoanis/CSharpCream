using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ClassLibrary;

namespace CourseAssignment.MVC.Controllers.Binders
{
    public class CourseBinder : DefaultBinder
    {

        #region Public Methods

        //
        // IModelBinder Member Implementation
        //

        public override object GetValue(ControllerContext controllerContext, string modelName, Type modelType, ModelStateDictionary modelState)
        {
            base.GetValue(controllerContext, modelName, modelType, modelState);
            if (modelType != typeof(Course))
            {
                throw new ArgumentException("This binder only works with Course models.", "modelType");
            }

            // Instantiate an product object, then bind values to each property
            var e = new Course
                        {
                            CourseName = LookupValue<string>(controllerContext, Concat(null, "CourseName"), modelState),
                            Monday = LookupValue<bool>(controllerContext, Concat(null, "Monday"), modelState),
                            Tuesday = LookupValue<bool>(controllerContext, Concat(null, "Tuesday"), modelState),
                            Wednesday = LookupValue<bool>(controllerContext, Concat(null, "Wednesday"), modelState),
                            Thursday = LookupValue<bool>(controllerContext, Concat(null, "Thursday"), modelState),
                            Friday = LookupValue<bool>(controllerContext, Concat(null, "Friday"), modelState),
                            Saturday = LookupValue<bool>(controllerContext, Concat(null, "Saturday"), modelState),
                            Sunday = LookupValue<bool>(controllerContext, Concat(null, "Sunday"), modelState),
                            StartHour = LookupValue<int>(controllerContext, Concat(null, "StartHour"), modelState),
                            StartMinute = LookupValue<int>(controllerContext, Concat(null, "StartMinute"), modelState),
                            EndHour = LookupValue<int>(controllerContext, Concat(null, "EndHour"), modelState),
                            EndMinute = LookupValue<int>(controllerContext, Concat(null, "EndMinute"), modelState),
                            AssignedProfessor =
                                LookupValue<Professor>(controllerContext, Concat(null, "AssignedProfessor"), modelState)
                        };
            if (modelState.Count == 0) UpdateModelStateWithViolations(e, modelState);
            return e;
        }

        #endregion
    }
}
