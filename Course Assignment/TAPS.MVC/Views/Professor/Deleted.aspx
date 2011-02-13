<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content id="indexContent" ContentPlaceHolderID="MainContent" runat="server">    
<meta http-equiv="refresh" content="3;url=<%=Url.Action("Index", "Professor", new {Id=""})%>"> 
            <%= Html.AntiForgeryToken() %>
    <br />
    <h2>Professor deleted Successfully...</h2>
    <br />
        <%=Html.ActionLink("Click Here if you have not been transferred within 3 seconds", "Index", "Professor")%>
    <br />&nbsp;
</asp:Content>    

