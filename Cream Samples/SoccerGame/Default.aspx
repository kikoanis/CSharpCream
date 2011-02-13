<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SoccerGame._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Soccer Problem</title>
    <style type="text/css">
        .style1
        {
            text-decoration: underline;
            font-weight: bold;
            color: #800000;
        }
        .style2
        {
            font-family: "Bookman Old Style";
        }
        .style3
        {
            text-decoration: underline;
            font-weight: bold;
        }
        .style4
        {
            font-family: "Bookman Old Style";
            color: #009933;
        }
        .style5
        {
            width: 53px;
        }
        .style6
        {
            width: 50px;
        }
        .style7
        {
            width: 51px;
        }
        .style8
        {
            width: 120px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <span class="style1">&nbsp;</span><span class="style3"><span class="style4">The 
        Soccer Problem:</span></span><br /><br />
        <span class="style2">John, Mary and Wendy separately rode to the soccer game. It 
        takes John 30 minutes, Mary 20 minutes and Wendy 50 minutes to get to the soccer 
        game. John either started or arrived just as Mary started. John left home 
        between 7:00 and 7:10. Mary arrived at work between 7:55 and 8:00. Wendy left 
        home between 7:00 and 7:10. John’s trip overlapped the soccer game. Mary’s trip 
        took place during the game or else the game took place during her trip. The 
        soccer game starts at 7:30 and lasts 105 minutes. John either started or arrived 
        just as Wendy started. Mary and Wendy arrived together but started at different 
        times.</span></div>
        <br />
        <div align="center">
    <asp:Button ID="Button5" runat="server" Text="Find Solutions" 
        onclick="Button5_Click" />
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <contenttemplate>
        <asp:Panel  ID="mainPanel" runat="server" Visible="false">
        <br />
        <table align="center">
        <tr>
        <td><br /><asp:Label ID="total" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label><br />
        </td></tr></table>
        <table>
        <tr>
        <td>
        <table>
        <tr>
        <td class="style8">&nbsp;</td>
        <td>&nbsp;&nbsp;</td>
        <td>
        <asp:Panel ID="JohnPanelN" runat="server" Height="17px" Width="125px" 
                BorderColor="#990000">&nbsp;</asp:Panel>
        </td>
        <td>
        <asp:Label runat="server" ID="JohnNumber" Font-Size="Smaller"></asp:Label>
        </td>
        <td>
        <asp:Label runat="server" ID="JohnF" Font-Size="Smaller"></asp:Label>
        </td>
        </tr>
        
        <tr>
        <td class="style8">John (30 mins)</td>
        <td>&nbsp;|&nbsp;</td>
        <td>
        <asp:Panel ID="JohnStart" runat="server" Height="17px" Width="125px" 
                BorderColor="#990000">&nbsp;</asp:Panel>
        </td>
        <td>
        <asp:Panel ID="JohnPanel" runat="server" Height="17px" Width="125px" 
                BackColor="Maroon" BorderColor="#990000">&nbsp;</asp:Panel>
        </td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
        <td>
        <table>
        <tr>
        <td class="style8">&nbsp;</td>
        <td>&nbsp;&nbsp;</td>
        <td>
        <asp:Panel ID="MaryPanelN" runat="server" Height="17px" Width="125px" 
                BorderColor="#990000">&nbsp;</asp:Panel>
        </td>
        <td>
        <asp:Label runat="server" ID="MaryNumber" Font-Size="Smaller"></asp:Label>
        </td>
                <td>
        <asp:Label runat="server" ID="MaryF" Font-Size="Smaller"></asp:Label>
                    
        </td>

        </tr>
        
        <tr>
        <td class="style8">Mary (20 mins)</td>
        <td>&nbsp;|&nbsp;</td>
        <td>
        <asp:Panel ID="MaryStart" runat="server" Height="17px" Width="125px" 
                BorderColor="#990000">&nbsp;</asp:Panel>
        </td>
        <td>
        <asp:Panel ID="MaryPanel" runat="server" Height="17px" Width="125px" 
                BackColor="Green" BorderColor="#990000">&nbsp;</asp:Panel>
        </td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
        <td>
        <table>
        <tr>
        <td class="style8">&nbsp;</td>
        <td>&nbsp;&nbsp;</td>
        <td>
        <asp:Panel ID="WendyPanelN" runat="server" Height="17px" Width="125px" 
                BorderColor="#990000">&nbsp;</asp:Panel>
        </td>
        <td>
        <asp:Label runat="server" ID="WendyNumber" Font-Size="Smaller"></asp:Label>
        </td>
                <td>
        <asp:Label runat="server" ID="WendyF" Font-Size="Smaller"></asp:Label>
        </td>

        </tr>
        
        <tr>
        <td class="style8">Wendy (50 mins)</td>
        <td>&nbsp;|&nbsp;</td>
        <td>
        <asp:Panel ID="WendyStart" runat="server" Height="17px" Width="125px" 
                BorderColor="#990000">&nbsp;</asp:Panel>
        </td>
        <td>
        <asp:Panel ID="WendyPanel" runat="server" Height="17px" Width="125px" 
                BackColor="Blue" BorderColor="#990000">&nbsp;</asp:Panel>
        </td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
        <td>
        <table>
        <tr>
        <td class="style8">&nbsp;</td>
        <td>&nbsp;&nbsp;</td>
        <td>
        <asp:Panel ID="SoccerPanelN" runat="server" Height="17px" Width="125px" 
                BorderColor="#990000">&nbsp;</asp:Panel>
        </td>
        <td>
        <asp:Label runat="server" ID="SoccerNumber" Font-Size="Smaller"></asp:Label>
        </td>
                <td>
        <asp:Label runat="server" ID="SoccerF" Font-Size="Smaller"></asp:Label>
        </td>

        </tr>
        
        <tr>
        <td class="style8">Soccer (105 mins)</td>
        <td>&nbsp;|&nbsp;</td>
        <td>
        <asp:Panel ID="SoccerStart" runat="server" Height="17px" Width="125px" 
                 BorderColor="#990000">&nbsp;</asp:Panel>
        </td>
        <td>
        <asp:Panel ID="SoccerPanel" runat="server" Height="17px" Width="125px" 
                BackColor="Black" BorderColor="#990000">&nbsp;</asp:Panel>
        </td>
        </tr>
        </table>
        </td>
        </tr>
        </table>
        <br />
        <table align="center"> 
        <tr>
        <td><asp:Label runat="server" ID="soln" Text=""></asp:Label>
        </td></tr></table>
        <br />
        <div align="center"  >
    <asp:Button ID="Button1" runat="server" Text="First" onclick="Button1_Click" 
            Enabled="False" Width="80px" />&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button2" runat="server" Text="Previous" Enabled="False" 
            onclick="Button2_Click" Width="80px"  />&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button3" runat="server" Text="Next" onclick="Button3_Click" 
                Width="80px"  />&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button4" runat="server" Text="Last" onclick="Button4_Click" 
                Width="80px" />
    </div>
    </asp:Panel>
    </contenttemplate>
     </asp:UpdatePanel>
    </form>
</body>
</html>
