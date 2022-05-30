<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintNews.aspx.cs" Inherits="PrintNews" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
	<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>چاپ خبر</title>

    <script language="javascript" type="text/javascript">
    function PrintPage()
    {
    window.print();
    }
    </script>

</head>
<body dir="rtl" align="center" style="font-family: Tahoma, 'Microsoft Sans Serif';
    font-size: 9pt;" onload="PrintPage()">
    <form id="form1" runat="server">
                    <table>
                        <tbody>
                            <tr>
                                <td >
                                    <TSPControls:CustomAspxButton runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                        Text=" "  EnableTheming="False" ToolTip="چاپ"
                                        ID="btnPrint" EnableViewState="False" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e){ PrintPage(); }" />
                                        <Image  Url="~/Images/icons/printers.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
               
            <hr />
            <table width="100%">
                <tr>
                    <td width="90%">
                        <asp:Label ID="lblNewsDetail" runat="server"></asp:Label>
                    </td>
                    <td width="10%" align="left">
                        <asp:Image ID="imgArm" runat="server" ImageUrl="~/Images/arme nezam black.png" Width="100px"
                            Height="100px" />
                    </td>
                </tr>
            </table>
            <hr />
            <div align="right">
                <asp:Label ID="lblNews" runat="server"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
