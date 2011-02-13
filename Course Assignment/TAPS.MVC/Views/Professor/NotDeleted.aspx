<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<meta http-equiv="refresh" content="10; url=<%=Url.Action("Index", "Professor", new {Id=""})%>"> 
<br />
            <%= Html.AntiForgeryToken() %>
    <h2>There is a problem...</h2>
    <h1 style="color:#ff0000; font-size:14px;">
        <%=Html.Encode(ViewData["ErrorMessage"])%>
    </h1>        
    <br />
       <%=Html.ActionLink("Click here if you have not been transferred within 10 seconds", "Index", "Professor")%>
    <br />
    &nbsp;
</asp:Content>
