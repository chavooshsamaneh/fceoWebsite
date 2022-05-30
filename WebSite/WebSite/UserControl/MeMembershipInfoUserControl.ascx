<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MeMembershipInfoUserControl.ascx.cs"
    Inherits="UserControl_MeMembershipInfoUserControl" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>


            <fieldset id="RoundPanelMain"
                runat="server">
                <legend class="HelpUL">اطلاعات عضو حقیقی</legend>
                <table dir="rtl" width="100%">
                    <tbody>
                        <tr>
                            <td class="TdFirst">کد عضویت
                            </td>
                            <td class="TdSecond">
                                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtbMeId" Font-Bold="true">
                                </dxe:ASPxLabel>
                            </td>
                            <td class="TdFirst">نمایندگی
                            </td>
                            <td class="TdSecond">
                                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtbMeAgentName" Font-Bold="true">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="TdAlignment">نام و نام خانوادگی
                            </td>
                            <td class="TdAlignment">
                                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtbMeName" Font-Bold="true">
                                </dxe:ASPxLabel>
                            </td>
                            <td class="TdAlignment">نام پدر
                            </td>
                            <td class="TdAlignment">
                                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtbMeFatherName" Font-Bold="true">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="TdAlignment">کد ملی
                            </td>
                            <td class="TdAlignment">
                                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtbMeSSN" Font-Bold="true">
                                </dxe:ASPxLabel>
                            </td>
                            <td class="TdAlignment">شماره شناسنامه
                            </td>
                            <td class="TdAlignment">
                                <dxe:ASPxLabel runat="server" Text="- - -" ID="txtbMeIdNo" Font-Bold="true">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                        </tr>
                    </tbody>
                </table>
      </fieldset>
