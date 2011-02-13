<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ClassLibrary"%>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<asp:Content id="indexContent" ContentPlaceHolderID="MainContent" runat="server">    
<%//using (Html.BeginForm<PreferencesController>(action => action.Create((string) ViewData["id"]), new FormMethod(), new { id = "Create-form" }))
 using(Html.BeginForm())
       {%>    
                   <%= Html.AntiForgeryToken() %>
<h2><%: ViewData["Message"] %> for <%: ((Preference)ViewData.Model).Id.Professor.FullName %></h2>
 <%=Html.ValidationSummary(" An error has occured... Please review the following erorr(s) and try again",new {style="font-size:14px"}) %>
<table>
        <tr >
            <td colspan="4" style="border-bottom:1px solid #999999;">&nbsp;
            </td>
        </tr>
        <tr>
            <td style="width:150px;" class="tdspec"  align="right">Course Name</td>
            <td>&nbsp;</td>
            <td>
                <%= Html.DropDownList("CourseList")%>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width:150px;" class="tdspec"  align="right">Weight</td>
            <td>&nbsp;</td>
            <%=Html.Hidden("ProfId") %>
            <td>
                <table>
                    <tr>
                        <td>
                            <%=Html.TextBox("Weight", null, new {   id = "weight", style = "width:22px",
                                                 TabIndex = 9,
                                                 onfocus = "this.className = 'Form-Focus';",
                                                 onblur = "this.className = 'smallTextBox';"})%>&nbsp;
                        </td>
                        <td>
                            <%: "[Read only]" %>
                        </td>
                    </tr>
                </table>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width:150px;" class="tdspec"  align="right">Preference Type</td>
            <td>&nbsp;</td>
            <td>
            <table><tr><td>
            <%=Html.RadioButton("PreferenceType", Preference.PreferenceTypes.Equal,
                                    ((Preference)ViewData.Model).PreferenceType == Preference.PreferenceTypes.Equal, 
                                    new { id = "Equal" })%>
                <label for="Equal">Interested</label>  
                </td>
                <td>
            <%=Html.RadioButton("PreferenceType", Preference.PreferenceTypes.NotEqual,
                                    ((Preference)ViewData.Model).PreferenceType == Preference.PreferenceTypes.NotEqual, 
                                    new { id = "NotEqual" })%>
                <label for="NotEqual">Not Interested</label>  
            </td></tr></table>    
            </td>
        </tr>

        <tr >
            <td class="tdspec" style="border-top:1px solid #999999">&nbsp;</td>
            <td style="border-top:1px solid #999999">&nbsp;</td>
            <td style="border-top:1px solid #999999" colspan="2">
                <table>
                    <tr >
                                               <td align="left" style="width:220px">
                            <%=Html.SubmitButton("submit", "Save", new {@class="Button1",style="width:94px"})%>
                        </td>
                        <td  align="left" class="tdspec"><input class="Button1" id="reset" name="reset" style="width:94px;cursor:hand;" type="reset" value="Reset" /></td>

                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>                        
                        <td >
                            <%var id = ((Preference) ViewData.Model).Id.Professor.ProfId.ToString();%>
                            <%=Html.ActionLink("Go back to preferences", "Show/" + id, "Preferences")%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%} %>
<script   type="text/javascript">
    
    $(document).ready(function() {
        $('#weight').attr("readonly", true);
        $('input[type=checkbox],input[type=radio]').prettyCheckboxes();
    });
    $('input[type=text]').numeric();
</script>
</asp:Content>