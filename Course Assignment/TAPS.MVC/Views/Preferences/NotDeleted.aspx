<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<meta http-equiv="refresh" content="3;url=<%=Url.Action("Show", "Preferences", new { id = ViewData["id"] })%>"> 
<br />
            <%= Html.AntiForgeryToken() %>
    <h2>There is a problem...</h2>
    <h1 style="color:#ff0000; font-size:14px;">
    <h1 style="color:#ff0000; font-size:14px;"><%= Html.Encode(ViewData["ErrorMessage"]) %></h1>        
    </h1>        
    <br />
       <%=Html.ActionLink("Click Here if you have not been transferred within 3 seconds", "Show/" + ViewData["id"], "Preferences")%>
    <br />
    &nbsp;
</asp:Content>
