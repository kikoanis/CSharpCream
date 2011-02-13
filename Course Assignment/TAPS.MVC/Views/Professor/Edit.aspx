<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<asp:Content id="indexContent" ContentPlaceHolderID="MainContent" runat="server">    
<%using (Html.BeginForm())
			 {%>
									 <%= Html.AntiForgeryToken() %>
 
<h2><%: ViewData["Message"] %></h2>
 <%=Html.ValidationSummary(" An error has occured... Please review the following erorr(s) and try again",new {style="font-size:14px"}) %>
<table>
				<tr >
						<td colspan="3" style="border-bottom:1px solid #999999;">&nbsp;
						</td>
				</tr>
				<tr>
				<%=Html.Hidden("Version") %>
						<td style="width:20%;" align="right" class="tdspec">Title</td>
						<td></td>
						<td style="width:80%">
								<table>
										<tr>
												<td class="alphaDot"><%=Html.TextBox("PTitle", null, new { style = "width:70px;",
																																TabIndex = 1,
																																onfocus = "this.className = 'Form-Focus';",
																																onblur = "this.className = 'Form';"})%>
												</td>
												<td><%=Html.ValidationMessage("PTitle", "*")%></td>
										</tr>
								</table>
						</td>
				</tr>
				<tr>
						<td style="width:20%;" align="right" class="tdspec">First Name</td>
						<td></td>
						<td style="width:80%">
								<table>
										<tr>
												<td class="alpha"><%=Html.TextBox("FirstName", null, new { style = "width:170px;",
																																TabIndex = 2,
																																onfocus = "this.className = 'Form-Focus';",
																																onblur = "this.className = 'Form';"})%>
												</td>
												<td><%=Html.ValidationMessage("FirstName", "*")%></td>
										</tr>
								</table>
						</td>
				</tr>
				<tr>
						<td style="width:20%;" align="right" class="tdspec">Last Name</td>
						<td></td>
						<td style="width:80%">
								<table>
										<tr>
												<td class="alpha"><%=Html.TextBox("LastName", null, new { style = "width:170px;",
																																TabIndex = 3,
																																onfocus = "this.className = 'Form-Focus';",
																																onblur = "this.className = 'Form';"})%>
												</td>
												<td><%=Html.ValidationMessage("LastName", "*")%></td>
										</tr>
								</table>
						</td>
				</tr>
				<tr>
						<td style="width:20%;" align="right" class="tdspec">No Of Courses</td>
						<td></td>
						<td style="width:80%">
								<table>
										<tr>
												<td class="numeric"><%=Html.TextBox("NoOfCourses", null, new { style = "width:170px;",
																																TabIndex = 4,
																																onfocus = "this.className = 'Form-Focus';",
																																onblur = "this.className = 'Form';"})%>
																															 
												</td>
												<td><%=Html.ValidationMessage("NoOfCourses", "*")%></td>
										</tr>
								</table>
						</td>
				</tr>
				<tr >
						<td class="tdspec" style="border-top:1px solid #999999"><%=Html.ActionLink("Go to Professors", "Index", "Professor")%></td>
						<td style="border-top:1px solid #999999">&nbsp;</td>
						<td style="border-top:1px solid #999999" colspan="2">
								<table>
										<tr style="text-align:center" >
												<td align="left" style="width:220px">
														<%=Html.SubmitButton("submit", "Save", new {@class="Button1",style="width:94px"})%>
												</td>
												<td  align="left" class="tdspec"><input class="Button1" id="reset" name="reset" style="width:94px;cursor:hand;" type="reset" value="Reset" /></td>
												<td>
														&nbsp;&nbsp;&nbsp;&nbsp;
												</td>                        
												<td >
														
												</td>
										</tr>
								</table>
						</td>
				</tr>
	 </table>
		<%} %>
		<script language="javascript" type="text/javascript">
			$('.alphaDot').alpha({ allow: "." });
			$('.alpha').alpha();
			$('.numeric').numeric();
</script>  
</asp:Content>