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

public class Notify {

    public static string Error(string message) {
        string result = "";
        if (!string.IsNullOrEmpty(message)) {
            result = string.Format("<div class=\"notify-error\">{0}</div>", message);
        }
        return result;
    }

}
