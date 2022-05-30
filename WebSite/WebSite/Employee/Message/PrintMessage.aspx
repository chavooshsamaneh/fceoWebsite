<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintMessage.aspx.cs" Inherits="Employee_Message_PrintMessage" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>چاپ پیام</title>
    <script language="javascript" type="text/javascript">
        function PrintPage() {
            window.print();
        }
    </script>
</head>
<body onload="PrintPage();">
    <form id="form1" runat="server">
            <div style="width: 100%" dir="rtl">
                <table >
                    <tbody>
                        <tr>
                            <td >
                                <TSPControls:CustomAspxButton runat="server" UseSubmitBehavior="False"
                                    ToolTip="چاپ"
                                    ID="btnPrint" EnableViewState="False" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){ PrintPage(); }" />
                                    <Image  Url="~/Images/icons/printers.png">
                                    </Image>
                                  
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        <hr />
        <table width="100%" style="font-family: Tahoma; font-size: 11px">
            <tr>
                <td width="100%" align="left">
                    <asp:Image ID="imgArm" runat="server" ImageUrl="~/Images/arme nezam black.png" Width="100px"
                        Height="100px" />
                </td>
            </tr>
            <tr>
                <td width="100%" align="right" dir="rtl">
                    <asp:Label ID="lblMail" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
