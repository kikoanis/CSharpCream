using System;
using System.Collections.Generic;
using System.Web.Mvc;
//using Microsoft.Web.Mvc;
using ClassLibrary.Base;
using ClassLibrary.Interfaces;
using ClassLibrary.Rules;

namespace CourseAssignment.MVC.Controllers.Binders
{
    public class DefaultBinder : IModelBinder
    {
        //
        // Helper Methods
        //

        protected static string Concat(string modelName, string propertyName)
        {
            return (String.IsNullOrEmpty(modelName)) ? propertyName : modelName + "." + propertyName;
        }

        protected static T LookupValue<T>(ControllerContext controllerContext, string propertyName, ModelStateDictionary modelState)
        {
            //IModelBinder binder = ModelBinders.GetBinder(typeof(T));
            IModelBinder binder = ModelBinders.GetBinder(typeof(T));
            object value = binder.GetValue(controllerContext, propertyName, typeof(T), modelState);
            return (value is T) ? (T)value : default(T);
        }


        //
        // Helper method (probably best refactored as a static property on another class 
        // so that it can be shared across multiple controllers)
        //
        public static void UpdateModelStateWithViolations(IRuleEntity entity, ModelStateDictionary modelState)
        {
            List<RuleViolation> issues = entity.GetRuleViolations();

            foreach (var issue in issues)
            {
                if (issue.PropertyValue != null)
                    modelState.AddModelError(issue.PropertyName,
                                            // issue.PropertyValue.ToString(),
                                             issue.ErrorMessage);
            }
        }
        public virtual object GetValue(ControllerContext controllerContext, string modelName, Type modelType, ModelStateDictionary modelState)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            return null;
        }

        public ModelBinderResult BindModel(ModelBindingContext bindingContext)
        {
            throw new System.NotImplementedException();
        }
    }
}
