<%@ Page Language="C#"  MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="TAPS.MVC.Controllers"%>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Import Namespace="ClassLibrary"%>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
  <h1>Professors</h1>
    <h2><%: ViewData["Message"] %></h2>
  <table cellspacing="0" class="tablesorter"  style="width:596px;">
    <thead> 
        <tr style="background:white"><td colspan="5" align="right"><%=Html.ActionLink(" Add New Professor", "Create")%></td></tr>
        <tr style="border:1px Black;"> 
            <th style="width:250px;font-size:12px;">Title & Name</th> 
            <th style="width:133px;font-size:12px;"># of Courses</th> 
            <th style="width:120px;font-size:12px;">Preferences</th> 
            <th align="center" style="width:40px;" class="{sorter: false}">&nbsp;</th> 
            <th align="center" style="width:53px;" class="{sorter: false}">&nbsp;</th> 
        </tr> 
    </thead> 
    <tbody> 
    <% if (((IList<Professor>)ViewData.Model).Count == 0)
      {%>
          <tr> 
            <td colspan="5" align="center">No Professors Data has been entered</td> 
          </tr>  
    <%}%>
    <% else %>
    <% foreach (var professor in (IList<Professor>)ViewData.Model)
       {%>
        <tr> 
            <td>
               <%: professor.FullName %>
            </td> 
            <td align="center"><%: professor.NoOfCourses %></td> 
            <td align="center">
             <a href="<%=Url.Action("Show","Preferences",new { id=professor.ProfId} ) %>">
                      <%=professor.Preferences.Count%> wiegh <%=professor.Preferences.Sum(s=>s.Weight)%>
             </a>
            </td> 
            <td align="center">
              <%Professor professor1 = professor;%><%=(professor.ProfId == 0? null: Html.ActionLink<ProfessorController>(c => c.Edit(professor1.ProfId.ToString()), "Edit"))%>
            </td> 
            <td align="center">
              <%=(professor.ProfId == 0? null: Html.ActionLink<ProfessorController>(c => c.Delete(professor1.ProfId.ToString()), "Delete",new {@class = "Delete"}))%>
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
    var answer = confirm("Confirm deletion of this professor information?!!", function() {
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

