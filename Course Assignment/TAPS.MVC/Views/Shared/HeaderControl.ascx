<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ OutputCache Duration="3000" VaryByParam="None" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<table width="100%"><tr><td>
<div id="title"><%=Html.ActionLink("Teaching Assignment Problem Solver (TAPS)", "Index", "Home")%>
        <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Solving TAP using Constraint Satisfaction Problem Technique...</span> 
</div>
</td>
<td><%= Html.Image("~/Content/Images/TAs.png") %></td>
<%--<td>
<table>
<tr>
<td>
<div id="navigation">
<ul>
  <li id="logindisplay">
                            <%
                                 if (Request.IsAuthenticated)
                                 {
%>
                                    Welcome <b><%: Page.User.Identity.Name %></b>!
                                    [ <%=Html.ActionLink("Logout", "Logout", "Account")%> ]
                            <%
                                 }
                                 else
                                 {
%> 
                                    <%=Html.ActionLink("Login", "Login", "Account")%> 
                            <%
                                 }
%>                            </li>
</ul>
</div></td>
</tr>

</table>
</td>
--%></tr></table>

