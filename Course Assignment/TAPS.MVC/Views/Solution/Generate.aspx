<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content id="indexContent" ContentPlaceHolderID="MainContent" runat="server"> 
<%=Session["UsedMethod"] = null %>   
<meta http-equiv="refresh" content="0;url=<%=Url.Action("Show", "Solution", new { id = 1 })%>"/>
            <%= Html.AntiForgeryToken() %>
    <br />
    <br />
    <br />
    <h2><pre><span style="color:Maroon;" >         Generating Solutions...Please have a patience...</span></pre>
    </h2>
    <br />
    <br />&nbsp;
</asp:Content>    
