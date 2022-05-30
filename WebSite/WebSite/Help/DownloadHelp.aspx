<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DownloadHelp.aspx.cs" Inherits="Help_DownloadHelp" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>  
                            <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                width="100%" height="28px" align="right">
                                <tr>
                                    <td style="vertical-align: top;" align="right">
                                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent"
                                            align="right">
                                            <tr>
                                                <td style="width: 27px; height: 27px;">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDownloadHelpFile" runat="server" 
                                                        EnableTheming="False" EnableViewState="False" ToolTip="دانلود فایل راهنما" CausesValidation="False"
                                                        OnClick="btnDownloadHelpFile_Click">
                                                        <Image Height="25px" Url="~/Images/Download.png" Width="25px" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td style="width: 27px; height: 27px;">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint" runat="server"  AutoPostBack="false"
                                                        EnableTheming="False" EnableViewState="False" ToolTip="چاپ" CausesValidation="False">
                                                        <Image Height="25px" Url="~/Images/icons/printers.png" Width="25px" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <ClientSideEvents Click="function(s,e){ parent['HelpFrame'].focus(); parent['HelpFrame'].print(); }" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                       </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
    </form>
</body>
</html>
