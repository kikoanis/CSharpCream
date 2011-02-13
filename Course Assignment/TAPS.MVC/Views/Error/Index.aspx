<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <fieldset id="fs">
    <h2><%: ViewData["Message"] %></h2>
    <%: "An error has occured... the error has been logged and will be sent to the website administrator...Please try again by " %>
    <a href="javascript:history.back();">going back</a> 
    <%: " or go to " %>
    <%=Html.ActionLink("Home Page","Index", "Home")%>
 </fieldset>
 <br />
</asp:Content>
