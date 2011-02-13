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
            <td style="width:20%;" align="right" class="tdspec">Course code</td>
            <td></td>
            <td style="width:80%">
                <table>
                    <tr>
                        <td class="courseName"><%=Html.TextBox("CourseName", null, new { style = "width:70px;",
                                                                TabIndex = 1,
                                                                onfocus = "this.className = 'Form-Focus';",
                                                                onblur = "this.className = 'Form';"})%>
                        </td>
                        <%--<td><%=Html.ValidationMessage("CourseName", "*")%></td>--%>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width:20%;" class="tdspec"  align="right">Days Of Week:</td>
            <td>&nbsp;</td>
            <td >
                <table>
                    <tr>
                        <td><%=Html.CheckBox("Monday", new { TabIndex = 2 })%><label style="cursor:hand;color:black;font-size:11px;" for="Monday" accesskey="M"><u>M</u>on</label></td>
                        <td><%=Html.CheckBox("Tuesday", new { TabIndex = 3, @class = "checkButton" })%><label style="color:black;font-size:11px;" for="Tuesday" accesskey="t"><u>T</u>ue</label></td>
                        <td><%=Html.CheckBox("Wednesday", new { TabIndex = 4, @class = "checkButton" })%><label style="color:black;font-size:11px;" for="Wednesday" accesskey="w"><u>W</u>ed</label></td>
                        <td><%=Html.CheckBox("Thursday", new { TabIndex = 5, @class = "checkButton" })%><label style="color:black;font-size:11px;" for="Thursday" accesskey="r">Th<u>u</u></label></td>
                        <td><%=Html.CheckBox("Friday", new { TabIndex = 6, @class = "checkButton" })%><label style="color:black;font-size:11px;" for="Friday" accesskey="f"><u>F</u>ri</label></td>
                        <td><%=Html.CheckBox("Saturday", new { TabIndex = 7, @class = "checkButton" })%><label style="color:black;font-size:11px;" for="Saturday" accesskey="a">S<u>a</u>t</label></td>
                        <td><%=Html.CheckBox("Sunday", new { TabIndex = 8, @class = "checkButton" })%><label style="color:black;font-size:11px;" for="Sunday" accesskey="s"><u>S</u>un</label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width:20%;" class="tdspec"  align="right">Time Slot</td>
            <td></td>
            <td>
                <table>
                    <tr>
                        <td class="time"><%=Html.TextBox("StartHour", null,new {  style = "width:22px",
                                                                TabIndex = 9,
                                                                onfocus = "this.className = 'Form-Focus';",
                                                                onblur = "this.className = 'smallTextBox';"})%>
                        </td>
                        <td style="width:2px">:</td>
                        <td class="time"><%=Html.TextBox("StartMinute", null, new { style = "width:22px",
                                                                  TabIndex = 10,
                                                                  onfocus = "this.className = 'Form-Focus';",
                                                                  onblur = "this.className = 'smallTextBox';"})%>
                        </td>
                        <td style="width:8px;"><span style="font-weight: bold;font-size: 2em;">-</span></td>
                        <td class="time"><%=Html.TextBox("EndHour", null,new {  style = "width:22px",
                                                              TabIndex = 11,
                                                              onfocus = "this.className = 'Form-Focus';",
                                                              onblur = "this.className = 'smallTextBox';"})%>
                        </td>
                        <td style="width:2px">:</td>
                        <td class="time"><%=Html.TextBox("EndMinute", null,new {  style = "width:22px",
                                                                TabIndex = 12,
                                                                onfocus = "this.className = 'Form-Focus';",
                                                                onblur = "this.className = 'smallTextBox';"})%>
                        </td>
                        <td style="font-size:0.7em;color:#999999;">[hh:mm - hh:mm] 24h time</td>
                    </tr>
                </table>
                
            </td>
        </tr>
        <tr>
            <td style="width:20%;" class="tdspec"  align="right">Assigned Professor</td>
            <td></td>
            <td>
                <%=Html.DropDownList("ProfessorList")%>
            </td>
        </tr>
        <tr >
            <td style="width:20%;"  align="center" class="tdspec" style="border-top:1px solid #999999">
                     <%=Html.ActionLink("Go back to Courses", "Index", "Course")%>
            </td>
            <td style="border-top:1px solid #999999">&nbsp;</td>
            <td style="border-top:1px solid #999999" colspan="2">
                <table>
                    <tr >
                        <td align="center" style="width:160px">
                        <%=Html.SubmitButton("submit", "Save", new {@class="Button1",style="width:94px;height:24px;cursor:hand;"})%>
                        </td>
                        <td align="center" style="width:160px">
                        <input class="Button1" id="reset" name="reset" style="width:94px;height:24px;cursor:hand;" type="reset" value="Reset" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%} %>
<script language="javascript" type="text/javascript">
  $(document).ready(function() {
  $('input[type=checkbox],input[type=radio]').prettyCheckboxes();
  });
  $('.courseName').alphanumeric({ allow: "-" });
$('.time').numeric();
	
</script>

</asp:Content>