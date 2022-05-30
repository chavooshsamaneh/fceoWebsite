<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MeDocumentInfoUserControl.ascx.cs" Inherits="UserControl_MeDocumentInfoUserControl" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>



<fieldset id="RoundPanelMain"
    runat="server">
    <legend class="HelpUL">اطلاعات پروانه اشتغال به کار شخص حقیقی</legend>
    <table width="100%">
        <tr>
            <td class="TdFirst">
                <dxe:ASPxLabel runat="server" Text="شماره پروانه" ID="ASPxLabel5">
                </dxe:ASPxLabel>
            </td>
            <td class="TdSecond">
                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtbFileNo" Font-Bold="true">
                </dxe:ASPxLabel>
            </td>
            <td class="TdFirst">تاریخ اعتبار پروانه
            </td>
            <td class="TdSecond">
                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtbFileDate" Font-Bold="true">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td class="TdAlignment">
                <dxe:ASPxLabel runat="server" Text="پایه نظارت" ID="ASPxLabel4">
                </dxe:ASPxLabel>
            </td>
            <td class="TdAlignment">
                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtbObsGrade" Font-Bold="true">
                </dxe:ASPxLabel>
            </td>
            <td class="TdAlignment">
                <dxe:ASPxLabel runat="server" Text="پایه طراحی" ID="ASPxLabel1">
                </dxe:ASPxLabel>
            </td>
            <td class="TdAlignment">
                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtbDesGrade" Font-Bold="true">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td class="TdAlignment">
                <dxe:ASPxLabel runat="server" Text="پایه اجرا" ID="ASPxLabel7">
                </dxe:ASPxLabel>
            </td>
            <td class="TdAlignment">
                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtbImpGrade" Font-Bold="true">
                </dxe:ASPxLabel>
            </td>
            <td class="TdAlignment">پایه ترافیک
            </td>
            <td class="TdAlignment">
                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtbTrafficeId" Font-Bold="true">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td class="TdAlignment">پایه نقشه برداری
            </td>
            <td class="TdAlignment">
                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtbMappingId" Font-Bold="true">
                </dxe:ASPxLabel>
            </td>
            <td class="TdAlignment">پایه شهرسازی
            </td>
            <td class="TdAlignment">
                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtbUrbenismId" Font-Bold="true">
                </dxe:ASPxLabel>
            </td>
        </tr>
          <tr>
            <td class="TdAlignment">پایه گاز
            </td>
            <td class="TdAlignment">
                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtGasId" Font-Bold="true">
                </dxe:ASPxLabel>
            </td>
            <td class="TdAlignment">
            </td>
            <td class="TdAlignment">
            </td>
        </tr>
    </table>
</fieldset>
