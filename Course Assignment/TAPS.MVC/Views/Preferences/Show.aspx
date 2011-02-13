<%@ Page Language="C#"  MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="TAPS.MVC.Controllers"%>
<%@ Import Namespace="ClassLibrary"%>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
	<h1>Professors</h1>
	 <h2><%: ViewData["Message"] %></h2> 
<table cellspacing="0" class="tablesorter"  style="width:436px;">
		<thead> 
				<tr style="background:white">
				<td colspan="4" align="right">
				<% if (!(bool)ViewData["NoMore"])
							 {%>
								 <% var settings = new Settings();%>
								<%=Html.ActionLink(" Add New Preference [Max "+settings.NumberOfPreferencesPerProfessor+"]", "Create/" + ViewData["id"])%>            
								<%
							 }
							 else
							 {%>
										No more perferences to be added 
							 <%
							 }%>
				</td></tr>
				<tr style="border:1px Black;"> 
						<th style="width:180px;">Course Name</th> 
						<th style="width:133px;">Weight</th> 
						<th style="width:163px;">Type</th> 
						<th align="center" style="width:53px;" class="{sorter: false}">&nbsp;</th> 
				</tr> 
		</thead> 
		<tbody> 
		<% if (((IList<Preference>)ViewData.Model).Count == 0)
			{%>
					<tr> 
						<td colspan="5" align="center">No Preferences Data has been entered</td> 
					</tr>  
		<%}%>
		<% else %>
		<% foreach (var preference in (IList<Preference>)ViewData.Model)
			 {%>
				<tr> 
						<td><%: preference.Id.Course.CourseName %></td> 
						<td align="center"><%: preference.Weight %></td> 
						<td align="center"><%: preference.PreferenceType==0?"Interested":"Not Interested" %></td> 
						<td align="center">
							<%Preference preference1 = preference;%><%=(preference.Id.Course.CourseId == 0 || preference.Id.Professor.ProfId == 0) ? null :
									 Html.ActionLink<PreferencesController>(c => c.Delete(preference1.Id.Professor.ProfId.ToString(),
																		 preference1.Id.Course.CourseId.ToString()), "Delete", new { @class = "Delete" })%>
						</td> 
				</tr> 
		<%
			 }%>    
		</tbody>
 </table>
 <%=Html.ActionLink("Go back to Professors", "Index", "Professor")%>
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
		var answer = confirm("Confirm deletion of this preference information?!!", function() {
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

