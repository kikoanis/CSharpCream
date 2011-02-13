using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Web.Mvc;

namespace TAPS.MVC.Controllers
{
	public static class ControllerExtensions
	{
		public static RedirectToRouteResult RedirectToAction<T>(this T controller,
		                                                        Expression<Action<T>> action) where T : Controller
		{
			return RedirectToAction<T>(controller, action, null);
		}

		public static RedirectToRouteResult RedirectToAction<T>(this T controller,
		                                                        Expression<Action<T>> action, object values) where T : Controller
		{
			return RedirectToAction<T>(controller, action, new RouteValueDictionary(values));

		}

		public static RedirectToRouteResult RedirectToAction<T>(this T controller,
		                                                        Expression<Action<T>> action, RouteValueDictionary values) where T : Controller
		{
			var body = action.Body as MethodCallExpression;
			if (body == null)
				throw new InvalidOperationException("Expression must be a method call.");
			if (body.Object != action.Parameters[0])
				throw new InvalidOperationException("Method call must target lambda argument.");

			string actionName = body.Method.Name;
			string controllerName = typeof(T).Name;

			if (controllerName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
				controllerName = controllerName.Remove(controllerName.Length - 10, 10);

			RouteValueDictionary parameters = LinkBuilder.BuildParameterValuesFromExpression(body);

			values = values ?? new RouteValueDictionary();
			values.Add("controller", controllerName);
			values.Add("action", actionName);

			if (parameters != null)
			{
				foreach (KeyValuePair<string, object> parameter in parameters)
				{
					values.Add(parameter.Key, parameter.Value);
				}
			}
			return new RedirectToRouteResult(values);
		}
	}
}