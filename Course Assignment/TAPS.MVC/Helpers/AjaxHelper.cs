using System;
using System.Linq;
using System.Web;
using System.Text;
using System.Linq.Expressions;
using System.Web.Routing;

namespace System.Web.Mvc {
    
    public static class AjaxHelper {
        /// <summary>
        /// A simple helper to setup a form for remote callback
        /// </summary>
        /// <param name="action">The action to call on your controller</param>
        /// <param name="outputControl">The html element to use for the results</param>
        /// <returns></returns>
        public static MvcForm<T> AjaxForm<T>(this HtmlHelper helper, 
            Expression<Action<T>> action, string outputControl, 
            object formAttributes) where T : Controller {

            RouteValueDictionary settings = new RouteValueDictionary(formAttributes);
            settings.Add("onsubmit", "getPostContent(this, '"+outputControl+"'); return false;");
            MvcForm<T> form = new MvcForm<T>(helper, helper.ViewContext.HttpContext, 
                action, FormMethod.Post, settings);
            
            form.WriteStartTag();
            return form;

        }
        /// <summary>
        /// A simple helper to setup a form for remote callback
        /// </summary>
        /// <param name="action">The action to call on your controller</param>
        /// <param name="outputControl">The html element to use for the results</param>
        /// <returns></returns>
        public static MvcForm<T> AjaxForm<T>(this HtmlHelper helper, 
            Expression<Action<T>> action, string outputControl) where T : Controller {
            return AjaxForm<T>(helper, action, outputControl, null);

        }
    }
}
