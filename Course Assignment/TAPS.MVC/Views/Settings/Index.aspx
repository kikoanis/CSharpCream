<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ClassLibrary" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%using (Html.BeginForm()) {%>    
				<%= Html.AntiForgeryToken()%>
			 <fieldset styel="align:top;">
		<h3><%= Html.Encode(ViewData["Message"])%></h3>
        <%=Html.ValidationSummary(" An error has occured... Please review the following erorr(s) and try again",new { style = "font-size:14px" })%>
		<table width="700px" align="top">
			<tr>
				<td style="width:40%;" align="right" class="tdspec">No. of Hours per Course</td>
				<td style="width:60%;" ><%=Html.TextBox("MaxNumberOfHoursPerCourse", null, new
																			{
					        														style = "width:70px;",
							    													TabIndex = 1,
								    												onfocus = "this.className = 'Form-Focus';",
																					onblur = "this.className = 'Form';"
									    									})%>
				</td>
			</tr>
			<tr>
				<td  style="width:40%;" align="right" class="tdspec">Max Break Minutes Per Session</td>
				<td style="width:60%;"><%=Html.TextBox("MaxBreakMinutesPerSession", null, new
																			{
																					style = "width:70px;",
																					TabIndex = 2,
																					onfocus = "this.className = 'Form-Focus';",
				    																onblur = "this.className = 'Form';"
                                                                            }) %>
				</td>
			</tr>
			<tr>
				<td  style="width:40%;" align="right" class="tdspec">Max no. of courses per professor</td>
				<td style="width:60%;" ><%=Html.TextBox("MaxNumberOfCoursesPerProfessor", null, new
																			{
																			    	style = "width:70px;",
																					TabIndex = 3,
																					onfocus = "this.className = 'Form-Focus';",
																				    onblur = "this.className = 'Form';"
																			})%>
				</td>
			</tr>
			<tr>
				<td  style="width:40%;" align="right" class="tdspec">No. of preferences per professor</td>
				<td style="width:60%;" ><%=Html.TextBox("NumberOfPreferencesPerProfessor", null, new
																			{
																					style = "width:70px;",
																					TabIndex = 4,
																					onfocus = "this.className = 'Form-Focus';",
																			    	onblur = "this.className = 'Form';"
																			})%>
				</td>
			</tr>
			<tr>
				<td  style="width:40%;" align="right" class="tdspec">Max No. of generated solutions</td>
				<td style="width:60%;" ><%=Html.TextBox("MaxNumberOfGeneratedSolutions", null, new
                                                                            {
                                                                                   style = "width:70px;",
                                                                                   TabIndex = 5,
                                                                                   onfocus = "this.className = 'Form-Focus';",
                                                                                   onblur = "this.className = 'Form';"
                                                                            })%>
				</td>
			</tr>
			<tr>
				<td  style="width:40%;" align="right" class="tdspec">Generate only same or better weighted solutions</td>
				<td style="width:60%;" ><%=Html.CheckBox("GenerateOnlySameOrBetterWeightedSolutions", new { TabIndex = 6, @class = "checkButton" })%>
				<label style="color:black;font-size:11px;" for="GenerateOnlySameOrBetterWeightedSolutions" ></label>
				</td>
			</tr>
			<tr><td colspan="3"></td>&nbsp;</tr>
			<tr><td colspan="3"></td>&nbsp;</tr>
			<tr><td colspan="3"></td>&nbsp;</tr>
			<tr>
				<td  style="width:40%;" align="right" class="tdspec">Display constraints details on solutions page</td>
				<td style="width:60%;" ><%=Html.CheckBox("DisplayConstraintsDetails", new { TabIndex = 7, @class = "checkButton" })%>
				<label style="color:black;font-size:11px;" for="DisplayConstraintsDetails" ></label>
				</td>
			</tr>
			<tr><td colspan="3"></td>&nbsp;</tr>
			<tr><td colspan="3"></td>&nbsp;</tr>
			<tr><td colspan="3"></td>&nbsp;</tr>
			<tr>
				<td  style="width:40%;" align="right" class="tdspec">Relax automatically count constraint when number of assigned courses to professors exceeds the number of courses</td>
				<td style="width:60%;" ><%=Html.CheckBox("RelaxCountConstraint", new { TabIndex = 7, @class = "checkButton" })%>
				<label style="color:black;font-size:11px;" for="RelaxCountConstraint" ></label>
				</td>
			</tr>
			<tr><td colspan="3"></td>&nbsp;</tr>
			<tr><td colspan="3"></td>&nbsp;</tr>
			<tr><td colspan="3"></td>&nbsp;</tr>
			<tr>
				<td  style="width:40%;" align="right" class="tdspec">Use Preferences Approach</td>
				<td style="width:60%;" ><%=Html.CheckBox("UsePreferencesApproach", new { TabIndex = 8, @class = "checkButton" })%>
				<label style="color:black;font-size:11px;" for="UsePreferencesApproach" ></label>
				</td>
			</tr>
			<tr><td colspan="3"></td>&nbsp;</tr>
			<tr><td colspan="3"></td>&nbsp;</tr>
			<tr><td colspan="3"></td>&nbsp;</tr>
			<tr>
				<td  style="width:40%;" valign="middle" align="right" class="tdspec">&nbsp;</td>
				<td style="width:60%;" >
				<%=Html.RadioButton("SolvingMethod", Settings.SolvingMethods.BranchAndBound, 
				    ((Settings)ViewData.Model).SolvingMethod == Settings.SolvingMethods.BranchAndBound, 
				                    new { id = "BranchAndBound" })%>
				<label for="BranchAndBound">Branch And Bound</label>  
				</td>
			</tr>
			<tr>
				<td  style="width:40%;" valign="middle" align="right" class="tdspec">&nbsp;</td>
				<td style="width:60%;" >
				<%=Html.RadioButton("SolvingMethod", Settings.SolvingMethods.IterativeBranchAndBound, 
				    ((Settings)ViewData.Model).SolvingMethod == Settings.SolvingMethods.IterativeBranchAndBound, 
				                    new { id = "IterativeBranchAndBound" })%>
				<label for="IterativeBranchAndBound">Iterative Branch And Bound</label>  
				</td>
			</tr>
			<tr>
				<td  style="width:40%;" valign="middle" align="right" class="tdspec">Solver method</td>
				<td style="width:60%;" >
                <%=Html.RadioButton("SolvingMethod", Settings.SolvingMethods.Taboo, 
                    ((Settings)ViewData.Model).SolvingMethod == Settings.SolvingMethods.Taboo, 
                                    new { id = "TabooSearch" })%>
                <label for="TabooSearch">Taboo Search (Only one solution will be generated!)</label>  
				</td>
			</tr>
			<tr>
				<td  style="width:40%;" valign="middle" align="right" class="tdspec">&nbsp;</td>
				<td style="width:60%;" >
                <%=Html.RadioButton("SolvingMethod", Settings.SolvingMethods.RandomWalk, 
                    ((Settings)ViewData.Model).SolvingMethod == Settings.SolvingMethods.RandomWalk, 
                                    new { id = "RandomWalkSearch" })%>
                <label for="RandomWalkSearch">Random Walk Search</label>  
				</td>
			</tr>
			<tr>
				<td  style="width:30%;" valign="middle" align="right" class="tdspec">&nbsp;</td>
				<td style="width:70%;" >
                <%=Html.RadioButton("SolvingMethod", Settings.SolvingMethods.SimulatedAnnealing, 
                    ((Settings)ViewData.Model).SolvingMethod == Settings.SolvingMethods.SimulatedAnnealing, 
                                    new { id = "SimulatedAnnealing" })%>
                <label for="SimulatedAnnealing">Simulated Annealing</label>  
				</td>
			</tr>

			<tr>
				<td  style="width:40%;" align="right" class="tdspec">Generating solution time out (in ms)</td>
				<td style="width:60%;" ><%=Html.TextBox("TimeOut", null, new
                                                                            {
                                                                                   style = "width:70px;",
                                                                                   TabIndex = 10,
                                                                                   onfocus = "this.className = 'Form-Focus';",
                                                                                   onblur = "this.className = 'Form';"
                                                                            })%>
				</td>
			</tr>
<%--			<tr>
			    <td><span style="font-size:09px;color:Maroon;">&nbsp;&nbsp;* Only for branch and bound method</span>
			    </td>
			</tr>--%>
			<tr >
												<td style="border-top:1px solid #999999" align="center">
														<%=Html.ActionLink("Go To Home Page", "Index", "Home")%>
												</td>
			<td>
			<table>
			<tr>
			
												<td align="center" style="width:190px;border-top:1px solid #999999" >
														<%=Html.SubmitButton("submit", "Save", new {@class="Button1",style="width:94px;cursor:hand;"})%>
												</td>
												<td align="center" style="width:190px;border-top:1px solid #999999" >
														<input class="Button1" id="reset" name="reset" style="width:94px;height:24px;cursor:hand;" type="reset" value="Reset" />
												</td>
												</tr>
												</table>
												</td>

																							
				</tr>
		</table>
 </fieldset>
 
		<%} %>
		
		<script language="javascript" type="text/javascript">
		    $(document).ready(function() {
		        $('input[type=checkbox],input[type=radio]').prettyCheckboxes();
		    });
		        $('input[type=text]').numeric();
		        
		    
	
</script>

</asp:Content>
