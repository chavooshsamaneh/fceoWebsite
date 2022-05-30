<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddTests.aspx.cs" MasterPageFile="~/MasterPagePortals.master" Inherits="Employee_Amoozesh_AddTests" Title="مشخصات آزمون دوره" %>



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

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0" width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False" Text=" "  EnableTheming="False" ToolTip="جدید" ID="BtnNew" EnableViewState="False" OnClick="BtnNew_Click">
                                                            <Image  Url="~/Images/icons/new.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False" Text=" "  Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit" EnableViewState="False" OnClick="btnEdit_Click">
                                                            <Image  Url="~/Images/icons/edit.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "  EnableTheming="False" ToolTip="ذخیره" ID="btnSave" EnableViewState="False" OnClick="btnSave_Click">
                                                            <Image  Url="~/Images/icons/save.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True" CausesValidation="False" Text=" "  EnableTheming="False" ToolTip="حذف" ID="btnDelete" EnableViewState="False" OnClick="btnDelete_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>

                                                            <Image  Url="~/Images/icons/delete.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False" Text=" "  EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click">
                                                            <Image  Url="~/Images/icons/Back.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>



                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table style="vertical-align: top; text-align: right" dir="rtl" cellpadding="1">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <dxe:ASPxLabel runat="server" Text="کد آزمون" ID="ASPxLabel17"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; text-align: right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="150px" MaxLength="10" ID="txtTestCode"  >
                                            <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را تکمیل نمایید." ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="لطفاً این فیلد را تکمیل نمایید."></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td style="vertical-align: top; text-align: right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ آزمون" Width="63px" ID="lbTypeOfReg"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; text-align: right">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" ShowPickerOnTop="True" Width="145px" ID="txtDate"></pdc:PersianDateTextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDate" ErrorMessage="تاریخ تولد" ToolTip="لطفاً این فیلد را پر کنید." ID="RequiredFieldValidator2">لطفاً این فیلد را پر کنید.</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <dxe:ASPxLabel runat="server" Text="ساعت شروع" Width="73px" ID="ASPxLabel12"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; text-align: right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td style="vertical-align: top; height: 37px; text-align: left">
                                                        <dxe:ASPxSpinEdit runat="server" MaxValue="59" Number="0" Width="40px" Height="21px" ID="txtMin"  >
                                                            <SpinButtons Position="Left"></SpinButtons>
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td style="vertical-align: top; height: 37px; text-align: left">
                                                        <dxe:ASPxLabel runat="server" Text=":" Width="1px" ID="ASPxLabel2"></dxe:ASPxLabel>
                                                    </td>
                                                    <td style="vertical-align: top; height: 37px; text-align: left">
                                                        <dxe:ASPxSpinEdit runat="server" MaxValue="24" MinValue="1" Number="0" Width="40px" Height="21px" ID="txtHour"  >
                                                            <SpinButtons Position="Left"></SpinButtons>
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td style="vertical-align: top; text-align: right">
                                        <dxe:ASPxLabel runat="server" Text="ساعت پایان" ID="ASPxLabel11"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; text-align: right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td style="vertical-align: top; height: 37px; text-align: left">
                                                        <dxe:ASPxSpinEdit runat="server" MaxValue="59" Number="0" Width="40px" Height="21px" ID="txtMinE"  >
                                                            <SpinButtons Position="Left"></SpinButtons>
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                    <td style="vertical-align: top; height: 37px; text-align: left">
                                                        <dxe:ASPxLabel runat="server" Text=":" Width="1px" ID="ASPxLabel3"></dxe:ASPxLabel>
                                                    </td>
                                                    <td style="vertical-align: top; height: 37px; text-align: left">
                                                        <dxe:ASPxSpinEdit runat="server" MaxValue="24" MinValue="1" Number="0" Width="40px" Height="21px" ID="txtHourE"  >
                                                            <SpinButtons Position="Left"></SpinButtons>
                                                        </dxe:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <dxe:ASPxLabel runat="server" Text="محل برگزاری" ID="ASPxLabel7"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; text-align: right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="71px" Width="370px" ID="txtPlace"  >
                                            <ValidationSettings ErrorText="لطفاً این فیلد را تکمیل نمادیذ." ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="لطفاً این فیلد را تکمیل نمادیذ."></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel1"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; text-align: right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="71px" Width="370px" ID="txtDesc"  ></TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <dxe:ASPxLabel runat="server" Text="وضعیت" ID="ASPxLabel6"></dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; text-align: right" colspan="3">
                                        <TSPControls:CustomASPxCheckBox runat="server" Text="غیر فعال" Width="97px" ID="chbInActive" OnCheckedChanged="chbInActive_CheckedChanged"></TSPControls:CustomASPxCheckBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <asp:HiddenField ID="TestId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0" width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False" Text=" "  EnableTheming="False" ToolTip="جدید" ID="BtnNew2" EnableViewState="False" OnClick="BtnNew_Click">
                                                            <Image  Url="~/Images/icons/new.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False" Text=" "  Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit2" EnableViewState="False" OnClick="btnEdit_Click">
                                                            <Image  Url="~/Images/icons/edit.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "  EnableTheming="False" ToolTip="ذخیره" ID="btnSave2" EnableViewState="False" OnClick="btnSave_Click">
                                                            <Image  Url="~/Images/icons/save.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True" CausesValidation="False" Text=" "  EnableTheming="False" ToolTip="حذف" ID="btnDelete2" EnableViewState="False" OnClick="btnDelete_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>

                                                            <Image  Url="~/Images/icons/delete.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False" Text=" "  EnableTheming="False" ToolTip="بازگشت" ID="ASPxButton6" EnableViewState="False" OnClick="btnBack_Click">
                                                            <Image  Url="~/Images/icons/Back.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
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
