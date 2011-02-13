<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>About this website</h1>
                <%= Html.AntiForgeryToken() %>
    <p>
        TAPS is a Teaching Assignment Problem Solver and it presents a preferences based 

approach to the teaching assignment problem (TAP) that is believed to offer performance over 

previous mathematical assignment methods. <br/>A variety of criteria such as professor 

preferences over courses (Soft Constraints), the maximum number of courses assigned to each professor and the 

principle of fairness when assigning courses to professors trying to satisfy everyone’s 

preferences are used in designing the system. <br/>
The TAP is solved using a solver called C-Sharp Cream which was recoded by the author using 

C# from a well-known solver called Cream which was written in Java and can be found <A 

Href="http://bach.istc.kobe-u.ac.jp/cream/">here</A><br/>
The solver has been enhanced to include weighed soft constraints to be used to solve TAPS and any oher problems that
can be modeled using soft+hard constraints.<br />
This website demonstrates that this approach can be an effective tool for professor-course 

assignment problem. The developed website can be easily implemented for the educational 

institutions.<br/>
The application is powered by:<br/>
<ul>
<li>C-Sharp Cream Solver (Constraints Satisifaction Problem based solver)</li>
<li>ASP.NET MVC (Model-View-Controller)</li>
<li>JQUERY (Java Script based Library)</li>
<li>N-Hibernate ORM (Object Relational Mapping)</li>
<li>Microsoft SQL Server 2008 (Database engine)</li>
</ul>
    </p>
</asp:Content>
