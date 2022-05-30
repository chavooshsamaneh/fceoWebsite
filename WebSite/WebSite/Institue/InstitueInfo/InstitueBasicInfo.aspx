<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="InstitueBasicInfo.aspx.cs" Inherits="Institue_InstitueInfo_InstitueBasicInfo" Title="مشخصات موسسه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>

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

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script type="text/javascript" language="javascript">

        function SetCityControlValues() {
            gridCity.GetRowValues(gridCity.GetFocusedRowIndex(), 'CitName;CitId;AgentName;AgentCode;AgentAddress', SetCityValue);
        }

        function SetCityValue(values) {
            txtCity.SetText(values[0]);
            HiddenFieldInstitue.Set('CitId', values[1])
        }
    </script>

    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click" UseSubmitBehavior="False">
                                            <image url="~/Images/icons/Back.png"></image>

                                            <hoverstyle backcolor="#FFE0C0">
<Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
</hoverstyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <div style="vertical-align: top; width: 100%; text-align: right" dir="rtl">
                <TSPControls:CustomAspxMenuHorizontal ID="MenuInstitue" runat="server" OnItemClick="ASPxMenu1_ItemClick">
                    <Items>
                        <dxm:MenuItem Name="BasicInfo" Text="اطلاعات پایه" Selected="True">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Manager" Text="هیئت اجرایی">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Activity" Text="زمینه های فعالیت">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Facility" Text="امکانات و تجهیزات">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="InsTeacher" Text="اساتید مؤسسه">
                        </dxm:MenuItem>
                    </Items>

                </TSPControls:CustomAspxMenuHorizontal>
            </div>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table dir="rtl" cellpadding="1" width="100%">
                            <tbody>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="نام مؤسسه" ID="ASPxLabel1"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" ID="txtInsName" Text="- - -"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">&nbsp;</td>
                                    <td align="right" valign="top"></td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="شهر" ID="ASPxLabel6"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" ID="txtCity" Text="- - -"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="نام مدیر مؤسسه" Width="86px" ID="ASPxLabel4"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" ID="txtManager" Text="- - -"></dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="شماره مجوز آموزشی" ID="ASPxLabel7"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="- - -" ID="txtFileNo"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">&nbsp;<dxe:ASPxLabel runat="server" Text="شماره ثبت مؤسسه" Width="102px" ID="ASPxLabel8"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" ID="txtRegNo" Text="- - -"></dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ ثبت مؤسسه" ID="ASPxLabel12"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" ID="txtRegDate" Text="- - -"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="محل ثبت مؤسسه" ID="ASPxLabel11"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" ID="txtRegPlace" Text="- - -"></dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="شماره تلفن1" ID="ASPxLabel9"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" ID="txtTel1" Text="- - -"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="شماره تلفن2" ID="ASPxLabel5"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" ID="txtTel2" Text="- - -"></dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="شماره تلفن همراه" Width="100px" ID="ASPxLabel13"></dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" ID="txtMobileNo" Text="- - -"></dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top"></td>
                                    <td dir="ltr" align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="آدرس" ID="ASPxLabel3"></dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" colspan="3" align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Width="461px" ID="txtAddress" Text="- - -"></dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top"></td>
                                    <td dir="ltr" colspan="3" align="right" valign="top"></td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="آدرس پست الکترونیکی" Width="120px" ID="ASPxLabel16"></dxe:ASPxLabel>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Width="461px" ID="txtEmail" Text="- - -"></dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top"></td>
                                    <td colspan="3" align="right" valign="top"></td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="آدرس وب سایت" Width="120px" ID="ASPxLabel2"></dxe:ASPxLabel>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Width="461px" ID="txtWebSite" Text="- - -"></dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top"></td>
                                    <td colspan="3" align="right" valign="top"></td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" Width="120px" ID="ASPxLabel15"></dxe:ASPxLabel>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Height="37px" Width="461px" ID="txtDesc" Text="- - -"></dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <dxhf:ASPxHiddenField ID="HiddenFieldInstitue" runat="server" ClientInstanceName="HiddenFieldInstitue">
            </dxhf:ASPxHiddenField>
            <asp:HiddenField ID="InstitueId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
                 <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " EnableTheming="False" ToolTip="بازگشت" ID="ASPxButton6" EnableViewState="False" OnClick="btnBack_Click" UseSubmitBehavior="False">
                                            <image url="~/Images/icons/Back.png"></image>

                                            <hoverstyle backcolor="#FFE0C0">
<Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
</hoverstyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

