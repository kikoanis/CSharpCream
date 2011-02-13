using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;
using System.Collections;

    public static class ViewExtensions {


        /// <summary>
        /// Renders a Component to the page, using Dependency Injection
        /// </summary>
        public static string RenderMappedComponent<TController>(this HtmlHelper helper, Expression<Action<TController>> action) where TController : ComponentController, new() {

            Type contollerType = typeof(TController);
            ComponentController local = StructureMap.ObjectFactory.GetInstance(contollerType) as ComponentController;

            local.Context = helper.ViewContext;

            MethodCallExpression body = action.Body as MethodCallExpression;
            if (body == null) {
                throw new InvalidOperationException("Expression must be a method call");
            }
            if (body.Object != action.Parameters[0]) {
                throw new InvalidOperationException("Method call must target lambda argument");
            }
            string name = body.Method.Name;
            MethodInfo method = local.GetType().GetMethod(name);
            List<object> list = new List<object>();
            foreach (Expression expression2 in body.Arguments) {
                ConstantExpression expression3 = (ConstantExpression)expression2;
                object item = expression3.Value;
                list.Add(item);
            }

            method.Invoke(local, list.ToArray());
            return local.RenderedHtml;


        }
        /// <summary>
        /// Renders a Commerce.MVC-specific user control
        /// </summary>
        /// <param name="controlName">Name of the control</param>
        /// <param name="data">ViewData to pass in</param>
        public static string RenderCommerceControl(this System.Web.Mvc.HtmlHelper helper,
            CommerceControls control, object data) {

            return RenderCommerceControl(helper, control, data, null);
        }

        /// <summary>
        /// Renders a Commerce.MVC-specific user control
        /// </summary>
        /// <param name="controlName">Name of the control</param>
        /// <param name="data">ViewData to pass in</param>
        /// <param name="propertySettings">Settings for the control</param>
        public static string RenderCommerceControl(this System.Web.Mvc.HtmlHelper helper,
        CommerceControls control, object data, object propertySettings) {

            string controlName = string.Format("{0}.ascx", Enum.GetName(typeof(CommerceControls), control));
            string controlPath = string.Format("~/Views/Shared/{0}", controlName);
            string absControlPath = VirtualPathUtility.ToAbsolute(controlPath);

            return helper.RenderUserControl(absControlPath, data, propertySettings);
        }


        /// <summary>
        /// A helper for registering script tags on an MVC View page.
        /// </summary>
        public static string RegisterJS(this System.Web.Mvc.HtmlHelper helper, ScriptLibrary scriptLib) {
            //get the directory where the scripts are
            string scriptRoot = VirtualPathUtility.ToAbsolute("~/Scripts");
            string scriptFormat="<script src=\"{0}/{1}\" type=\"text/javascript\"></script>\r\n";
            
            string scriptLibFile="";
            string result = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            //all of the script tags
            if (scriptLib == ScriptLibrary.Ajax) {
                sb.AppendLine(RegisterScript(helper,"MicrosoftAjax.js"));
                sb.AppendLine(RegisterScript(helper, "MicrosoftAjaxMVC.js"));

                result = sb.ToString();
            }
            return result;
        }


        /// <summary>
        /// A helper for registering script tags on an MVC View page.
        /// </summary>
        public static string RegisterScript(this System.Web.Mvc.HtmlHelper helper, string scriptFileName) {
            //get the directory where the scripts are
            string scriptRoot = VirtualPathUtility.ToAbsolute("~/Scripts");
            string scriptFormat = "<script src=\"{0}/{1}\" type=\"text/javascript\"></script>\r\n";

            string scriptLibFile = "";
            string result = "";

            return string.Format(scriptFormat, scriptRoot, scriptFileName);

        }

        public static string GetNumericPager(this HtmlHelper helper, string urlFormat, int totalRecords, int pageSize, int currentPage) {

            string linkFormat = "<a href=\"{0}\">{1}</a>";

            int totalPages = totalRecords / pageSize;
            if (totalRecords % pageSize > 0) {
                totalPages++;
            }

            bool isFirst = true;
            StringBuilder sb = new StringBuilder();

            for (int i = 1; i < totalPages + 1; i++) {
                if (!isFirst)
                    sb.Append(" | ");

                string pageLink = i.ToString();

                if (currentPage != i) {

                    sb.AppendFormat(linkFormat, string.Format(urlFormat, i), pageLink);

                } else {
                    sb.AppendFormat("<b>{0}</b>", pageLink);
                }


                isFirst = false;
            }

            return sb.ToString();
        }



        /// <summary>
        /// Creates a formatted list of items based on the passed in format
        /// </summary>
        /// <param name="list">The item list</param>
        /// <param name="format">The single-place format string to use</param>
        public static string ToFormattedList(this IEnumerable list, ListType listType) {
            StringBuilder sb = new StringBuilder();
            IEnumerator en = list.GetEnumerator();

            string outerListFormat = "";
            string listFormat = "";

            switch (listType) {
                case ListType.Ordered:
                    outerListFormat = "<ol>{0}</ol>";
                    listFormat = "<li>{0}</li>";
                    break;
                case ListType.Unordered:
                    outerListFormat = "<ul>{0}</ul>";
                    listFormat = "<li>{0}</li>";
                    break;
                case ListType.TableCell:
                    outerListFormat = "{0}";
                    listFormat = "<td>{0}</td>";
                    break;
                default:
                    break;
            }


            return string.Format(outerListFormat, ToFormattedList(list, listFormat));
        }

        /// <summary>
        /// Creates a formatted list of items based on the passed in format
        /// </summary>
        /// <param name="list">The item list</param>
        /// <param name="format">The single-place format string to use</param>
        public static string ToFormattedList(IEnumerable list, string format) {

            StringBuilder sb = new StringBuilder();
            foreach (object item in list) {
                sb.AppendFormat(format, item.ToString());
            }

            return sb.ToString();

        }

    }
