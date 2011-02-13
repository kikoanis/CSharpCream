<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<meta http-equiv="refresh" content="3;url=<%=Url.Action("Show", "Preferences", new { id = ViewData["id"] })%>"> 
    <br />
                <%= Html.AntiForgeryToken() %>
    <h2>Preference Deleted Successfully...</h2>
    <br />
        <%=Html.ActionLink("Click Here if you have not been transferred within 3 seconds", "Show/" + ViewData["id"], "Preferences")%>
        <br />&nbsp;
    <br />&nbsp;
</asp:Content>
