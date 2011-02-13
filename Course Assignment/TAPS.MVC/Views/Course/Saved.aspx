<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content id="indexContent" ContentPlaceHolderID="MainContent" runat="server">    
<meta http-equiv="refresh" content="3;url=<%=Url.Action("Index", "Course", new {Id=""})%>"> 
    <br />
            <%= Html.AntiForgeryToken() %>

    <h2>Course Saved Successfully...</h2>
    <br />
        <%=Html.ActionLink("Click Here if you have not been transferred within 3 seconds", "Index", "Course")%>
    <br />&nbsp;
</asp:Content>    

