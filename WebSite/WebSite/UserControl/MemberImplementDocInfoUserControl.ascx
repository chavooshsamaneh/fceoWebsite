<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberImplementDocInfoUserControl.ascx.cs"
    Inherits="UserControl_MemberImplementDocInfoUserControl" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>




<fieldset id="Fieldset1"
    runat="server">
    <legend class="HelpUL">اطلاعات مجوز اجرا</legend>
    <table dir="rtl" width="100%">
        <tbody>
            <tr>
                <td class="TdFirst">مجوز اجرا:
                </td>
                <td class="TdSecond">
                    <dxe:ASPxLabel runat="server" Text="---" ID="lblImplementDoc" Width="100%" Font-Bold="true">
                    </dxe:ASPxLabel>
                </td>
                <td class="TdFirst">تاریخ اعتبار:
                </td>
                <td class="TdSecond">
                    <dxe:ASPxLabel runat="server" Text="---" ID="lblImplementDocFileDate" Width="100%"
                        Font-Bold="true">
                    </dxe:ASPxLabel>
                </td>
            </tr>

            <tr>
                <td class="TdAlignment">تاریخ آخرین درخواست:
                </td>
                <td class="TdAlignment">
                    <dxe:ASPxLabel runat="server" Text="---" ID="lblLastReqCreatDate" Width="100%" Font-Bold="true">
                    </dxe:ASPxLabel>
                </td>
                <td valign="top" align="right">وضعیت آخرین درخواست :
                </td>
                <td valign="top" align="right">
                    <dxe:ASPxLabel ID="lblLastTaskName" runat="server" Text="---" Font-Bold="true">
                    </dxe:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td class="TdAlignment">نوع آخرین درخواست:
                </td>
                <td class="TdAlignment">
                    <dxe:ASPxLabel runat="server" Text="---" ID="lblLastReqMFType" Width="100%" Font-Bold="true">
                    </dxe:ASPxLabel>
                </td>
                <td></td>
                <td></td>
            </tr>
        </tbody>
    </table>
</fieldset>
