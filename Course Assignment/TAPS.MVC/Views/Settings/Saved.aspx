<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content id="indexContent" ContentPlaceHolderID="MainContent" runat="server">    
<meta http-equiv="refresh" content="5;url=<%=Url.Action("Index", "Home", new { id = "" })%>" />
            <%= Html.AntiForgeryToken() %>
    <br />
    <h2>Application Settings Data Saved Successfully...</h2>
    <br />
        <%=Html.ActionLink("Click Here if you have not been transferred within 5 seconds", "Index", "Home")%>
    <br />&nbsp;
</asp:Content>    

