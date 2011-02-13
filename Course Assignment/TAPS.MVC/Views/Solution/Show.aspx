<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<fieldset>
  <h1 ><%: ViewData["Message"] %></h1>
<table>
		<tr style="background:white;"><td></td>
		<td align="center" style="border: 1px solid #C0C0C0; background-color: #FFD5D5; color: #FFFFFF;" >
		<span style="color:Green;"><%=Html.ActionLink("Generate another set of solutions", "Generate")%></span>&nbsp;&nbsp;</td>
		</tr>
		</table>

	 <%using (Html.BeginForm())
	   {%>
  
<table>
		<tr style="background:white;">
			<td colspan="3"> 
			<pre><div>Solver used method: <span style="color:White; background-color:Maroon;"><%: Session["UsedMethod"] %></span></div></pre>
			</td>
			<td colspan="3"> 
			<pre><span style="color:White; background-color:Maroon;"><%: Session["FeedbackMessage"] %></span></pre>
			</td>
		</tr>
        
<tr>
	<% if (ViewData["NoOfGeneratedSolutions"] != null && (int)ViewData["NoOfGeneratedSolutions"] > 1)
{%>
	<td><div>Viewing Solution No. <%: ViewData["SolutionID"] %></div></td>
	<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
	<%
}%>
	<td><div>Solution Weight:&nbsp;<span style="color:Green;"><%: ViewData["Weight"] %></div></td>
	</tr> </table>
  <table cellspacing="0"   style="width:620px;border:0px;">             
		<tr style="background:white;">
			<td colspan="2"> 
			<%
	   	const long longZero = 0; %>
			Time spent to generate <span style="color:Green;"><%: ViewData["NoOfGeneratedSolutions"]??0 %></span> solution(s):&nbsp;
								   <span style="color:Green;"><%: (long)(ViewData["TimeSpent"]??longZero)/10.0 %></span> ms 
								  (<span style="color:Green;"><%: (long)(ViewData["TimeSpent"]??longZero)/10000.0 %></span> seconds)</td>
		</tr>
		<tr style="background:white;">
			<td colspan="2"> 
			Time spent to generate this solution:&nbsp;
								   <span style="color:Green;"><%: (long)(ViewData["Time"]??longZero)/10.0 %></span> ms 
								  (<span style="color:Green;"><%: (long)(ViewData["Time"]??longZero)/10000.0 %></span> seconds)</td>
		</tr>
<%--		<tr style="background:white;">
			<td colspan="2"> Solver used method:&nbsp;<span style="color:Green;"><%: ViewData["UsedMethod"] %></span></td>
		</tr>--%>	
	</table>
	<br />
	<% if (ViewData["NoOfGeneratedSolutions"] != null && (int)ViewData["NoOfGeneratedSolutions"] > 1)
	   { %>
	<table style="border:1px solid;">
		<tr>
			<% if ((int)ViewData["SolutionID"] > 1)
			   { %>
			<td>&nbsp;&nbsp;<%=Html.ActionLink("First Solution", "show"+"/1")%>&nbsp;&nbsp;</td>
			<td>&nbsp;&nbsp;<%=Html.ActionLink("Prev. Solution", "show/" + (((int)ViewData["SolutionID"]) - 1))%>&nbsp;&nbsp;</td>
			<%}
			   else
			   {%>
			 <td style="color:Gray;">&nbsp;&nbsp;First Solution&nbsp;&nbsp;</td>  
			 <td style="color:Gray;">&nbsp;&nbsp;Prev. Solution&nbsp;&nbsp;</td>  
			<%
				}%>
			<% if ((int)ViewData["SolutionID"] < (int)ViewData["NoOfGeneratedSolutions"])
			   { %>
			<td>&nbsp;&nbsp;<%=Html.ActionLink("Next Solution", "show/" + (((int)ViewData["SolutionID"]) + 1))%>&nbsp;&nbsp;</td>
			<td>&nbsp;&nbsp;<%=Html.ActionLink("Last Solution", "show/" + ViewData["NoOfGeneratedSolutions"])%>&nbsp;&nbsp;</td>
			<%}
			   else
			   {%>
			 <td style="color:Gray;">&nbsp;&nbsp;Next Solution&nbsp;&nbsp;</td>  
			 <td style="color:Gray;">&nbsp;&nbsp;Last Solution&nbsp;&nbsp;</td>  
			<%
				}%>
		</tr>
	</table>
	<%} %>
	<table>
		<tr>
			<td align="center" style="border:solid 1px #B7B7B7; background-color: #F7F9E3;">
				<span>Courses</span>
			</td>
			<td align="center" style="border:1px solid #B7B7B7; background-color: #F7F9E3;">
				<span>Professors</span>
			</td>
		</tr>
		<tr>
			<td>
				<table cellspacing="0" class="tablesorter"  style="width:420px;">             
					<thead>
						<tr style="border:1px Black;"> 
							<th style="width:16px;font-size:12px;">&nbsp;</th> 
							<th style="width:160px;font-size:12px;">Course Code</th> 
							<th style="width:260px;font-size:12px;">Professor Name</th> 
						</tr> 
					</thead>
					<tbody> 
						<% if (((IList<ClassLibrary.Solution>)ViewData.Model).Count == 0)
						   {%>
						<tr> 
							<td colspan="5" align="center">No Solutions have been generated..<br /> Problem is too tightly constrained</td> 
						</tr>  
						<%}%>
						<% else
							{%>
							   
							<% int count = 0;
							   foreach (var solution in (IList<ClassLibrary.Solution>)ViewData.Model)
							   {%>
						<tr> 
							<td><%: ++count %></td> 
							<td><%: solution.Course %></td> 
							<td style='<%=(solution.Professor.Trim() == "Not Assigned")?"color:maroon;font-style:italic;":string.Empty%>'>
										<%: solution.Professor %>
							</td> 
						</tr> 
							<%
								}
							}%>    
					</tbody>
				</table>   
			</td>
			<td valign="top">
				<table cellspacing="0" class="tablesorter1"  style="width:420px;">             
					<thead>
						<tr style="border:1px Black;"> 
							<th style="width:16px;font-size:12px;">&nbsp;</th> 
							<th style="width:260px;font-size:12px;">Professor Name</th> 
							<th style="width:160px;font-size:12px;"># Assigned Courses</th> 
						</tr> 
					</thead>
					<tbody> 
						<% if (((IList<object[]>)ViewData["ProfessorsAssignedCourses"]).Count == 0)
         {%>
						<tr> 
							<td colspan="5" align="center">No Solutions have been generated..<br /> Problem is too tightly constrained</td> 
						</tr>  
						<%}%>
						<% else
                            {%>
							<%
                            int count = 0;
                            foreach (var professor in ((IList<object[]>)ViewData["ProfessorsAssignedCourses"]))
                            {%>
						<tr> 
							<td><%: ++count %></td> 
							<td><%: (string)professor[0] %></td> 
							<td><%: (long)professor[1] %></td> 
						</tr> 
							<%
                            }
                            }%>    
					</tbody>
				</table>   
			</td>
		</tr>
		<% if ((bool)ViewData["DisplayConstraintsDetails"])
     {  %>
		<tr>
<td valign="top" colspan="2">
				<table cellspacing="0" class="tablesorter1"  style="width:660px;">             
					<thead>
						<tr style="border:1px Black;"> 
							<th style="width:16px;font-size:12px;">&nbsp;</th> 
							<th style="width:620px;font-size:12px;">Constraints</th> 
						</tr> 
					</thead>
					<tbody> 
						<% if (ViewData["Constraints"] == null)
         {%>
						<tr> 
							<td colspan="5" align="center">No Constraints found..</td> 
						</tr>  
						<%}%>
						<% else
            {%>
							<%
            int count = 0;
            foreach (var constraint in ((IOrderedEnumerable<string>)ViewData["Constraints"]))
            {%>
						<tr> 
							<td><%: ++count %></td> 
							<td><%: constraint %></td> 
						</tr> 
							<%
            }
            }%>    
					</tbody>
				</table>   
			</td>        
		</tr>
			<%} %>
	</table>
				
 
  <%}%>
  
	 
</fieldset>
<script language="javascript" type="text/javascript">
  $(document).ready(function() {
	// extend the default setting to always include the zebra widget. 
	$.tablesorter.defaults.widgets = ['zebra'];
	// extend the default setting to always sort on the first column 
	$.tablesorter.defaults.sortList = [[0, 0]];
	// call the tablesorter plugin
	$("table").tablesorter() ;
  }); 
  
</script>
</asp:Content>
