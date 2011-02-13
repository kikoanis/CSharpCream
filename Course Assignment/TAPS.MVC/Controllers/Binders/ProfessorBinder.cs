using System;
using System.Web.Mvc;
using ClassLibrary;

namespace CourseAssignment.MVC.Controllers.Binders
{
    public class ProfessorBinder : DefaultBinder
    {

        //
        // IModelBinder Member Implementation
        //

        public override object GetValue(ControllerContext controllerContext, string modelName, Type modelType, ModelStateDictionary modelState)
        {
            base.GetValue(controllerContext, modelName, modelType, modelState);
            if (modelType != typeof(Professor))
            {
                throw new ArgumentException("This binder only works with Professor models.", "modelType");
            }

            // Instantiate an product object, then bind values to each property
            var e = new Professor
                        {
                            FirstName = LookupValue<string>(controllerContext, Concat(null, "FirstName"), modelState),
                            LastName = LookupValue<string>(controllerContext, Concat(null, "LastName"), modelState),
                            NoOfCourses = LookupValue<Int32>(controllerContext, Concat(null, "NoOfCourses"), modelState),
                            PTitle = LookupValue<string>(controllerContext, Concat(null, "PTitle"), modelState),
                            UnassignedProf = LookupValue<bool>(controllerContext, Concat(null, "UnassignedProf"), modelState)
                        };
            if (modelState.Count == 0) UpdateModelStateWithViolations(e, modelState);
            return e;
        }


    }
}
