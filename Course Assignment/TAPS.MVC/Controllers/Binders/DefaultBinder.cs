using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ClassLibrary.Base;
using ClassLibrary.Interfaces;

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
            IModelBinder binder = ModelBinders.GetBinder(typeof(T));
            object value = null;// binder.GetValue(controllerContext, propertyName, typeof(T), modelState);
            return (value is T) ? (T)value : default(T);
        }


        //
        // Helper method (probably best refactored as a static property on another class 
        // so that it can be shared across multiple controllers)
        //
        public static void UpdateModelStateWithViolations(IRuleEntity entity, ModelStateDictionary modelState)
        {
            IList<RuleViolation> issues = entity.GetRuleViolations();

            foreach (var issue in issues)
            {
                if (issue.PropertyValue != null)
                    modelState.AddModelError(issue.PropertyName,
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

        //ToDO finish this method
        public ModelBinderResult BindModel(ModelBindingContext bindingContext)
        {
            ModelBinderResult mr = new ModelBinderResult(bindingContext);
            
            //string strModel = bindingContext.Model.ToString();
            //string strModelName = bindingContext.ModelName;
            //int strModelState = bindingContext.ModelState.Count;
            //string strModelType = bindingContext.ModelType.ToString();
            //string strModelValueProvider = bindingContext.ValueProvider.ToString();

            //throw new System.NotImplementedException();
            return mr;
        }
    }
}
