// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppHelper.cs" company="U of R">
//   Copyright 2008-2009
// </copyright>
// <summary>
//   Application helper class
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using System.Web.Routing;

namespace TAPS.MVC.Helpers
{
	/// <summary>
	/// Application helper class
	/// </summary>
	public static class AppHelper
	{
		/// <summary>
		/// Gets an absolute reference to the Content directory
		/// </summary>
		public static string ContentRoot 
		{
			get
			{
				const string ContentVirtualRoot = "~/Content";
				return VirtualPathUtility.ToAbsolute(ContentVirtualRoot);
			}
		}

		/// <summary>
		/// Gets an absolute reference to the Images directory
		/// </summary>
		public static string ImageRoot 
		{
			get { return string.Format("{0}/{1}", ContentRoot, "Images"); }
		}

		/// <summary>
		/// Gets an absolute reference to the CSS directory
		/// </summary>
		public static string CssRoot
		{
			get { return string.Format("{0}/{1}", ContentRoot, "CSS"); }
		}

		/// <summary>
		/// Builds an Image URL
		/// </summary>
		/// <param name="imageFile">
		/// The file name of the image
		/// </param>
		/// <returns>
		/// The image url.
		/// </returns>
		public static string ImageUrl(string imageFile) 
		{
			string result = string.Format("{0}/{1}", ImageRoot, imageFile);
			return result;
		}

		/// <summary>
		/// Builds a CSS URL
		/// </summary>
		/// <param name="cssFile">
		/// The name of the CSS file
		/// </param>
		/// <returns>
		/// The css url.
		/// </returns>
		public static string CssUrl(string cssFile) 
		{
			string result = string.Format("{0}/{1}", CssRoot, cssFile);
			return result;
		}

		/// <summary>
		/// Determines whether the site recognizes the current user
		/// </summary>
		/// <returns>
		/// The user is logged in.
		/// </returns>
		public static bool UserIsLoggedIn() 
		{
			bool result = false;
			if (HttpContext.Current != null)
			{
				result = HttpContext.Current.User.Identity.IsAuthenticated;
			}

			return result;
		}

		/// <summary>
		/// Returns string represents action string
		/// </summary>
		/// <param name="helper">
		/// The helper.
		/// </param>
		/// <param name="action">
		/// The action.
		/// </param>
		/// <param name="imageRelativeUrl">
		/// The image relative url.
		/// </param>
		/// <param name="alt">
		/// The alternative.
		/// </param>
		/// <param name="includeText">
		/// The include text.
		/// </param>
		/// <typeparam name="T">Type of action string
		/// </typeparam>
		/// <returns>
		/// string for action string
		/// </returns>
		public static string ActionLinkImage<T>(this HtmlHelper helper, Expression<Action<T>> action, string imageRelativeUrl, string alt, bool includeText) 
			where T : Controller
		{
			string html;
			if (includeText)
			{
				html = String.Format(
					helper.ActionLink(action, "{0}  {1}").ToString(),
					helper.Image(imageRelativeUrl, (object) alt).ToString(),
					alt);
			}
			else
			{
				html = String.Format(
					helper.ActionLink(action, "{0}").ToString(),
					helper.Image(imageRelativeUrl, (object) alt).ToString());
			}

			return html.ToString();
		}

		 /// return image link
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id">Id of link control</param>
        /// <param name="controller">target controller name</param>
        /// <param name="action">target action name</param>
        /// <param name="strOthers">other URL parts like querystring, etc</param>
        /// <param name="strImageURL">URL for image</param>
        /// <param name="alternateText">Alternate Text for the image</param>
        /// <param name="strStyle">style of the image like border properties, etc</param>
        /// <returns></returns>
        public static string ImageLink(this HtmlHelper helper, string id, string controller, string action, string strOthers, string strImageURL, string alternateText, string strStyle)
        {
            return ImageLink(helper, id, controller, action, strOthers, strImageURL, alternateText,strStyle,null);
        }



        /// <summary>
        /// return image link
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id">Id of link control</param>
        /// <param name="controller">target controller name</param>
        /// <param name="action">target action name</param>
        /// <param name="strOthers">other URL parts like querystring, etc</param>
        /// <param name="strImageURL">URL for image</param>
        /// <param name="alternateText">Alternate Text for the image</param>
        /// <param name="strStyle">style of the image like border properties, etc</param>
        /// <param name="htmlAttributes">html attribues for link</param>
        /// <returns></returns>
        public static string ImageLink(this HtmlHelper helper, string id, string controller, string action, string strOthers, string strImageURL, string alternateText,string strStyle, object htmlAttributes)
        {
            // Create tag builder
            var builder = new TagBuilder("a");

            // Create valid id
            builder.GenerateId(id);

            // Add attributes
            builder.MergeAttribute("href", "/" + controller + "/" + action + strOthers); //form target URL
			builder.InnerHtml = "<img src='" + strImageURL + "' alt='" + alternateText + "' style=\"" + strStyle + "\">" + alternateText; //set the image as inner html
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            // Render tag
			
            return builder.ToString(TagRenderMode.Normal); //to add </a> as end tag
        }
	}
}