<%@ Page Language="C#"  MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="TAPS.MVC.Controllers"%>
<%@ Import Namespace="ClassLibrary"%>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<h1>Courses</h1>
		<%= Html.AntiForgeryToken() %>
		  <h2><%: ViewData["Message"] %></h2>        
<table cellspacing="0" class="tablesorter"  style="width:725px;position: relative;">
	<thead> 
			<tr style="background:white"><td colspan="8" align="right"><%=Html.ActionLink(" Add New Course", "Create")%></td></tr>
		<tr style="border:1px Black;"> 
			<th style="width:95px;vertical-align:middle;font-size:10px;">Course Code</th> 
			<th style="width:90px;vertical-align:middle;font-size:10px;">Week Days</th> 
			<th style="width:80px;vertical-align:middle;font-size:10px;">Time Slot</th> 
			<th style="width:185px;vertical-align:middle;font-size:10px;">Assigned Professor</th> 
			<th align="center" style="vertical-align:middle;width:75px;font-size:09px;">Interested</th> 
			<th align="center" style="vertical-align:middle;width:75px;font-size:09px;">Not Interested</th> 
			<th align="center" style="vertical-align:middle;width:40px;font-size:10px;" class="{sorter: false}">&nbsp;</th> 
			<th align="center" style="vertical-align:middle;width:53px;font-size:10px;" class="{sorter: false}">&nbsp;</th> 
		</tr> 
	</thead> 
	<tbody> 
		<% if (((IList<Course>)ViewData.Model).Count == 0)
	  {%>
		  <tr> 
			<td colspan="6" align="center">No Courses Data has been entered</td> 
		  </tr>  
	<%}%>
	<% else %>

	<% foreach (var course in (IList<Course>)ViewData.Model)
	   {%>
		<tr> 
			<td style="vertical-align:middle;">
				<%: course.CourseName %>
			</td> 
			<td style="vertical-align:middle;"><%: course.DaysOfWeek %></td> 
			<td style="vertical-align:middle;"><%: course.TimeSlot %></td> 
			<td style="vertical-align:middle;"><%: course.AssignedProfessor == null ? " " : course.AssignedProfessor.FullName %></td> 
			<td style="vertical-align:middle;" align="center"><%: course.ProfessorsInterestedList.Count() %></td> 
			<td style="vertical-align:middle;" align="center"><%: course.ProfessorsNotInterestedList.Count() %></td> 
			<td style="vertical-align:middle;" align="center">
			  <%Course course1 = course;%><%=(course.CourseId == 0? null: Html.ActionLink<CourseController>(c => c.Edit(course1.CourseId.ToString()), "Edit"))%>
              <%--<%=(course.CourseId == 0 ? null : Html.ActionLink("Edit", "Edit/" + course.CourseId.ToString()))%>--%>
			</td> 
			<td style="vertical-align:middle;" align="center">
			  <%Course course2 = course;%><%=(course.CourseId == 0? null: Html.ActionLink<CourseController>(c => c.Delete(course2.CourseId.ToString()), "Delete",new {@class = "Delete"}))%>
              <%--<%=(course.CourseId == 0 ? null : Html.ActionLink("Delete", "Delete/" + course.CourseId.ToString(), null,new {@class = "Delete"}))%>--%>
			</td> 
		</tr> 
	<%
	   }%>    
	</tbody>
 </table>
<div id='confirm' style='display:none'>
	<a href='#' title='Close' class='modalCloseX simplemodal-close'>x</a>
	<div class='header'><span>Confirm</span></div>
	<p class='message'></p>
	<div class='buttons' style="text-align:center;">
	<div class='no simplemodal-close'>No</div><div class='yes'>Yes</div></div>
</div>

<script language="javascript" type="text/javascript" >
  $('.Delete').click(function(e) {
	e.preventDefault();
	$.myvariable = this;
	var answer = confirm("Confirm deletion of this course information?!!", function() {
	  window.location.href = $.myvariable;
	});
  });
  $(document).ready(function() {
	// extend the default setting to always include the zebra widget. 
	$.tablesorter.defaults.widgets = ['zebra'];
	// extend the default setting to always sort on the first column 
	$.tablesorter.defaults.sortList = [[0, 0]];
	// call the tablesorter plugin 
	$("table").tablesorter();
  }); 
  
</script>
</asp:Content>

