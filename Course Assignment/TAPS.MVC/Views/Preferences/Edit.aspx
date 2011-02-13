<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<asp:Content id="indexContent" ContentPlaceHolderID="MainContent" runat="server">    
<% using(Html.BeginForm())
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
            <td class="tdspec"  align="right">Course Name</td>
            <td></td>
            <td>
                <%= Html.DropDownList("CourseList")%>
            </td>
        </tr>
        <tr>
            <td class="tdspec"  align="right">Weight</td>
            <td></td>
            <td><%=Html.TextBox("Weight", null, new {  style = "width:22px",
                                                 TabIndex = 9,
                                                 onfocus = "this.className = 'Form-Focus';",
                                                 onblur = "this.className = 'smallTextBox';"})%>
            </td>
        </tr>
        <tr >
            <td class="tdspec" style="border-top:1px solid #999999">&nbsp;</td>
            <td style="border-top:1px solid #999999">&nbsp;</td>
            <td style="border-top:1px solid #999999" colspan="2">
                <table>
                    <tr >
                        <td align="center" style="width:220px">
                            <%=Html.SubmitButton("submit", "Save", new {@class="Button1",style="width:94px"})%>
                        </td>
                        <td class="tdspec">&nbsp;&nbsp;&nbsp;</td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>                        
                        <td >
                            <%string id = (ViewData["id"]).ToString();%>
                            <%=Html.ActionLink("Go back to preferences", "Show/"+id, "Preferences")%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%} %>
<%--<script   type="text/javascript">
    $(document).ready(function() {
        $("#Create-form").submit(function() {
            // get the form
            var f = $("#Create-form");
            // get the action attribute
            var action = f.attr("action");
            // get the serialized data
            var serialisedForm = f.serialize();
            $.post(action, serialisedForm);
            
            return false;
        });

        jQuery().ajaxStart(function() {
            $("#Create-form").fadeOut("slow");
        });
        jQuery().ajaxStop(function() {
            $("#Create-form").fadeIn("fast");
        });
    });
</script>
--%></asp:Content>