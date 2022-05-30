<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberObservationDocInfoUserControl.ascx.cs"
    Inherits="UserControl_MemberObservationDocInfoUserControl" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<fieldset id="Fieldset1"
    runat="server">
    <legend class="HelpUL">اطلاعات آماده به کاری</legend>
            <table dir="rtl" width="100%">
                <tbody>
                    <tr>
                        <td class="TdFirst">
                           وضعیت ثبت آماده بکاری
                        </td>
                        <td class="TdSecond">
                            <dxe:ASPxLabel runat="server" Text="---" ID="lblHasReqSaved" Width="100%" Font-Bold="true">
                            </dxe:ASPxLabel>
                        </td>
                        <td class="TdFirst">
                        تاریخ ثبت آخرین درخواست
                        </td>
                        <td class="TdSecond">
                          <dxe:ASPxLabel runat="server" Text="---" ID="lblRequestDate" Width="100%" Font-Bold="true">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </tbody>
            </table>
      </fieldset>