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
using System.Xml.Linq;

    /// <summary>
    /// Enums which describe script libraries
    /// </summary>
    public enum ScriptLibrary {
        Ajax
    }

    /// <summary>
    /// Types of HTML Lists
    /// </summary>
    public enum ListType {
        Ordered,
        Unordered,
        TableCell
    }

    public enum CommerceControls{
        ProductDisplay,
        CategoryList
    }

