<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MeEngOfficeInfoUserControl.ascx.cs"
    Inherits="UserControl_MeEngOfficeInfoUserControl" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<fieldset id="RoundPanelMain"
    runat="server">
    <legend class="HelpUL">اطلاعات دفتر عضو</legend>
    <table width="100%">
        <tr>
            <td class="TdFirst">کد دفتر:
            </td>
            <td class="TdSecond">
                <dxe:ASPxLabel runat="server" Text="---" ID="lblEngOfficeId" Width="100%" Font-Bold="true">
                </dxe:ASPxLabel>
            </td>
            <td class="TdFirst">نام دفتر:
            </td>
            <td class="TdSecond">
                <dxe:ASPxLabel runat="server" Text="---" ID="lblEngOfficeMembership" Width="100%"
                    Font-Bold="true">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td class="TdAlignment">پروانه دفتر:
            </td>
            <td class="TdAlignment">
                <dxe:ASPxLabel runat="server" Text="---" ID="lblEngOfficeFileNo" Width="100%" Font-Bold="true">
                </dxe:ASPxLabel>
            </td>
            <td class="TdAlignment">تاریخ اعتبار دفتر:
            </td>
            <td class="TdAlignment">
                <dxe:ASPxLabel runat="server" Text="---" ID="lblengOffExp" Width="100%" Font-Bold="true">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td class="TdAlignment">پایه عضو در دفتر:
            </td>
            <td class="TdAlignment">
                <dxe:ASPxLabel runat="server" Text="---" ID="lblEngOfficeMemberGrade" Width="100%"
                    Font-Bold="true">
                </dxe:ASPxLabel>
            </td>
            <td class="TdAlignment">پایه دفتر:
            </td>
            <td class="TdAlignment">
                <dxe:ASPxLabel runat="server" Text="---" ID="lblEngOfficeGrade" Width="100%" Font-Bold="true">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td class="TdAlignment">مسئول دفتر:
            </td>
            <td class="TdAlignment">
                <dxe:ASPxLabel runat="server" Text="---" ID="lblEngOfficeManager" Width="100%" Font-Bold="true">
                </dxe:ASPxLabel>
            </td>
            <td valign="top" align="right">وضعیت عضویت دفتر:
            </td>
            <td valign="top" align="right">
                <dxe:ASPxLabel ID="lblDocStatus" runat="server" Text="---" Font-Bold="true">
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

       
    </table>
</fieldset>
