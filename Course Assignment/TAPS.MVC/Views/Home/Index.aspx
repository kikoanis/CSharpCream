<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<%using (Html.BeginForm()) {%>
    <%= Html.AntiForgeryToken()%>
    <h1 ><%: ViewData["Message"] %></h1>
    <p>
          Solving TAP using Constraint Satisfaction Problem Technique... <br />
          Please select from the side menu:<br />
          <ul>
          <li>Course: to add/delete/update entries for course details. This includes time, day of week and if possible professor who is going to teach it.</li>
          <li>Professor: to add/delete/update entries for professor details. This includes preferences</li>
          <li>Solution: to try to solve the problem and get a solution for it.</li>
          <li>Settings: to update solver/website settings</li>
          </ul>
    </p>
    <%} %>
</asp:Content>
