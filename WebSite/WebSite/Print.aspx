<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print.aspx.cs" Inherits="Print" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dxwpg" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" dir="rtl">
<head id="Head1" runat="server">
    <title>چاپ گزارش</title>
</head>
<body style="vertical-align: middle" align="center">
    <form id="form1" runat="server" style="text-align: center">
    <div id="DivContent" runat="server" style="width: 100%" align="center">
        <table width="100%" id="TablMain" runat="server">
            <tr>
                <td align="center">
                    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" runat="server" ShowHeader="False"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <table width="100%">
                                <tr>
                                    <td style="width: 12%" align="center">
                                        <asp:Image ID="Image2" runat="server" Height="80px" ImageUrl="~/Images/arm_report.png"
                                            Width="80px" />
                                    </td>
                                    <td align="center" dir="rtl" style="width: 76%">
                                        <dxe:ASPxLabel ID="LabelTitle" runat="server" Font-Names="Tahoma" Font-Size="11pt"
                                            Text="" Width="100%" Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" colspan="2">
                                        <dxe:ASPxLabel ID="LabelHeader" runat="server" Font-Names="Tahoma" Font-Size="9pt"
                                            ForeColor="Highlight" Text="سازمان نظام مهندسی ساختمان استان فارس" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanel>
                    <br />
                </td>
            </tr>  <tr>
            <td align="center" valign="top">
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                <dxe:ASPxLabel ID="lblPrintDate" runat="server" Font-Names="Tahoma" Font-Size="9pt"
                 Text="" Width="100%">
                </dxe:ASPxLabel>
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
