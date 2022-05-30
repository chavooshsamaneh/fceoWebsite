<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="NewSms.aspx.cs" Inherits="Employee_SMS_NewSms" Title="ارسال پیام کوتاه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxt" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>



<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanelNewSms" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td align="right" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="btnNew" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnNew_Click" CausesValidation="False">

                                            <Image Height="25px" Url="~/Images/Icons/new.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top" align="right">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnEdit_Click" CausesValidation="False">

                                            <Image Url="~/Images/Icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>

                                    <td style="vertical-align: top" align="right">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click">

                                            <Image Url="~/Images/Icons/save.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s,e){
                                                    if(CheckBeforeSave()==false)
                                                    e.processOnServer=false;
                                                    }" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top" align="right">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار"
                                            ID="btnWF" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" CausesValidation="False">
                                            <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/reload.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top" dir="ltr" align="right">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">

                                            <Image Url="~/Images/Icons/back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelSMS" HeaderText="ویرایش" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>

                        <fieldset>
                            <legend dir="rtl">
                                <strong class="HelpUL">مشخصات پیام کوتاه</strong>
                            </legend>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td width="15%">بخش
                                        </td>
                                        <td width="35%">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                TextField="PartName" ID="cmbPartition" DataSourceID="ObjectDataSourceDrpPart"
                                                EnableClientSideAPI="True" ValueType="System.String" ValueField="PartId" Height="22px"
                                                ClientInstanceName="cmbPartition">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                    <RequiredField IsRequired="True" ErrorText="بخش را انتخاب نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                            <asp:ObjectDataSource ID="ObjectDataSourceDrpPart" runat="server" SelectMethod="GetData"
                                                TypeName="TSP.DataManager.PartitionManager"></asp:ObjectDataSource>
                                        </td>
                                        <td width="15%">
                                            <dxe:ASPxLabel runat="server" Text="نوع پیام" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td width="35%">
                                            <TSPControls:CustomAspxComboBox runat="server" TextField="SmsTypeName"
                                                ID="cmbSMSType" DataSourceID="ObjdsSmsType" ValueType="System.String"
                                                ValueField="SmsTypeId" RightToLeft="True" Width="100%">
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">مهلت تایید و ارسال از :
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="300px" ShowPickerOnTop="True"
                                                ID="txtbSMSDotoDate" ShowPickerOnEvent="OnClick" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                            <asp:RequiredFieldValidator runat="server" ErrorMessage="تاریخ را وارد نمایید" ToolTip="تاریخ را وارد نمایید"
                                                ControlToValidate="txtbSMSDotoDate" Width="127px" ID="RequiredFieldValidator1"
                                                Display="Dynamic">تاریخ را وارد نمایید</asp:RequiredFieldValidator>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="مهلت تایید و ارسال تا" ID="ASPxLabel5">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="300px" ShowPickerOnTop="True"
                                                ID="txtbExipreDate" ShowPickerOnEvent="OnClick" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                            <asp:RequiredFieldValidator runat="server" ErrorMessage="تاریخ را وارد نمایید" ToolTip="تاریخ را وارد نمایید"
                                                ControlToValidate="txtbExipreDate" Width="127px" ID="RequiredFieldValidatorMailDate"
                                                Display="Dynamic">تاریخ را وارد نمایید</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel runat="server" Text="عنوان" ID="lblSubject">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%" ID="txtbSubject"
                                                AutoResizeWithContainer="True">
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">

                                                    <RequiredField IsRequired="True" ErrorText="عنوان پیام کوتاه را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel runat="server" Text="گیرندگان" ID="ASPxLabel6">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="3">
                                            <table width="100%">
                                                <tr>
                                                    <td width="95%">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="100px" Width="100%" ID="txtRecievers"
                                                            ReadOnly="true" ReadOnlyStyle-BackColor="snow" AutoResizeWithContainer="True"
                                                            ClientInstanceName="txtRecievers">
                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">

                                                                <RequiredField IsRequired="True" ErrorText="گیرندگان پیام را مشخص کنید."></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                    <td align="right">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جستجو"
                                                            CausesValidation="False" ID="btnRecievers" AutoPostBack="False" UseSubmitBehavior="False"
                                                            Visible="true" EnableViewState="False" EnableTheming="False">
                                                            <ClientSideEvents Click="function(s,e){
    Fill_lstNezamChartReciever();
	popupChooseReciever.Show();
}"></ClientSideEvents>

                                                            <Image Url="~/Images/icons/search.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxe:ASPxLabel runat="server" Text="گیرندگان بدون شماره همراه"
                                                ID="lblRecieverWithoutTel">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="70px" Width="100%" ReadOnly="True"
                                                ID="txtbRecieverWithoutTel" ClientInstanceName="txtbRecieverWithoutTel">
                                                <ValidationSettings>
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>

                        <br />
                        <fieldset>
                            <legend dir="rtl">
                                <strong class="HelpUL">متن پیام</strong>
                            </legend>

                            <table style="width: 100%">
                                <tr>
                                    <td colspan="3" style="vertical-align: top;" align="right">

                                        <ul class="HelpUL">
                                            <li>وب سرویس پیش فرض
                                                                        <asp:Label ID="lblCurrentWebService" runat="server"></asp:Label>
                                                می باشد</li>
                                            <li>به منظور تغییر زبان از دکمه FA استفاده نمائید</li>
                                            <li>حداکثر تعداد پیامک های ارسالی نمی تواند بیشتر از 4 عدد باشد</li>
                                        </ul>



                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 2%">
                                        <dxe:ASPxLabel runat="server" Text="70" Width="30px" ID="lblSMSLenght"
                                            ClientInstanceName="lblSMSLenght">
                                        </dxe:ASPxLabel>

                                    </td>
                                    <td style="width: 1%">
                                        <dxe:ASPxLabel runat="server" Text="/" Width="10px" ID="ASPxLabel4">
                                        </dxe:ASPxLabel>

                                    </td>
                                    <td style="width: 97%;" align="right">
                                        <dxe:ASPxLabel runat="server" Text="1" Width="30px" ID="lblSMSCount"
                                            ClientInstanceName="lblSMSCount">
                                        </dxe:ASPxLabel>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <textarea runat="server" id="txtbSMSBody" style="width: 100% !important;max-width:100%; height: 180px;resize:none;"
                                            lang="fa" onchange="StartSMSBodyCallBack('SMSBody');"></textarea>

                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="متن پیام کوتاه را مشخص کنید"
                                            ControlToValidate="txtbSMSBody" ID="RequiredFieldValidatorSMSBody">متن پیام کوتاه را مشخص کنید</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                            <TSPControls:CustomAspxCallbackPanel runat="server" Width="100%"
                                ID="CallbackPanelSMSBody" ClientInstanceName="CallbackPanelSMSBody" OnCallback="CallbackPanelSMSBody_Callback">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="تعداد گیرندگان" ID="ASPxLabel2">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <TSPControls:CustomTextBox runat="server" Width="100%" ReadOnly="true"
                                                            ID="txtbRecieverCount" Text="0" ClientInstanceName="txtbRecieverCount"
                                                            ReadOnlyStyle-BackColor="snow">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="گیرندگان بدون شماره" ID="ASPxLabel8">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <TSPControls:CustomTextBox runat="server" Width="100%" ReadOnly="true"
                                                            ID="txtbRecieverCountWithoutTel" ClientInstanceName="txtbRecieverCountWithoutTel"
                                                            Text="0" ReadOnlyStyle-BackColor="snow">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="هزینه ارسال پیام کوتاه" ID="ASPxLabel3">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" Width="100%" ReadOnly="true"
                                                            ID="txtbSMSCost" Text="0" ReadOnlyStyle-BackColor="snow">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="اعتبار باقیمانده" ID="ASPxLabel7">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox Text="0" runat="server" Width="100%" ReadOnly="true"
                                                            ID="txtRemainingCredit" ReadOnlyStyle-BackColor="snow">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                                <ClientSideEvents EndCallback="function(s,e){
                txtbRecieverWithoutTel.SetText(s.cpRecieverWithoutTel);
                s.cpRecieverWithoutTel='';
                if(s.cpError!=''){
                ShowMessage(s.cpError);
                s.cpError='';
                }
                HiddenFieldSMSDetail.Set('CostID',s.cpCostId);
                s.cpCostId='';
                }" />
                            </TSPControls:CustomAspxCallbackPanel>
                        </fieldset>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenuDown" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <div dir="rtl">
                            <table>
                                <tbody>
                                    <tr>
                                        <td align="right" dir="ltr" style="vertical-align: top">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnNew_Click" CausesValidation="False">

                                                <Image Height="25px" Url="~/Images/Icons/new.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td style="vertical-align: top" dir="ltr" align="right">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnEdit_Click" CausesValidation="False">

                                                <Image Url="~/Images/Icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td style="vertical-align: top" dir="ltr" align="right">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                                ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnSave_Click">

                                                <Image Url="~/Images/Icons/save.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s,e){
                                                    if(CheckBeforeSave()==false)
                                                    e.processOnServer=false;
                                                    }" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td style="width: 30px">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار"
                                                ID="btnWF2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" CausesValidation="False">
                                                <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>

                                                <Image Url="~/Images/icons/reload.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td style="vertical-align: top" align="right">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                CausesValidation="False" ID="btnBack2" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">

                                                <Image Url="~/Images/Icons/back.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomASPxPopupControl ID="popupChooseReciever" runat="server" Width="550px"
                ClientInstanceName="popupChooseReciever"
                HeaderText="انتخاب گیرندگان">
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        <table class="TableBorder" width="100%">
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td style="height: 28px">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="اضافه کردن"
                                                            CausesValidation="False" ID="btnChooseReciever" AutoPostBack="False" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False">
                                                            <ClientSideEvents Click="function(s,e){SaveRecivers();}"></ClientSideEvents>

                                                            <Image Url="~/Images/icons/button_ok.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="پاک کردن گیرندگان"
                                                            CausesValidation="False" ID="btnClearReciever" AutoPostBack="False" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False">
                                                            <ClientSideEvents Click="function(s,e){ ClearRecivers(); }"></ClientSideEvents>
                                                            <Image Url="~/Images/icons/Clear-Form.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروج"
                                                            CausesValidation="False" ID="btnClosePopupReciever" AutoPostBack="False" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False">
                                                            <ClientSideEvents Click="function(s,e){popupChooseReciever.Hide()}"></ClientSideEvents>
                                                            <Image Url="~/Images/Close-box-red.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <dxtc:ASPxPageControl runat="server" Width="100%"
                            ActiveTabIndex="0" ID="PageControlReceiver" ClientInstanceName="tbcReceiver"
                            TabSpacing="0px">
                            <TabPages>
                                <dxtc:TabPage Name="Member" Text="عضو حقیقی">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server">
                                            <div align="right" dir="rtl">
                                                کد عضویت
                                                <br />
                                                <TSPControls:CustomASPXMemo runat="server" Height="100px" ID="txtMembers_Reciever"
                                                    ClientInstanceName="txtMembers_Reciever">

                                                    <ClientSideEvents KeyPress="function(s,e){
        HandleGotoPageKeyDown(e.htmlEvent);
        }"
                                                        TextChanged="function(s,e){getMemberList(s.GetText());}" />
                                                </TSPControls:CustomASPXMemo>
                                                <br />
                                                <asp:Label runat="server" Text="* در صورتیکه تعداد اعضا بیشتر از 1 می باشد، کد عضویت را با ; جدا کنید."
                                                    ForeColor="DarkRed" ID="Label2"></asp:Label>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Name="Clerck" Text="کارمند">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server">
                                            <div align="right" dir="rtl">
                                                پست سازمانى
                                                <br />
                                                <TSPControls:CustomASPxListBox ID="lstNezamChartReciever" runat="server" Width="100%" Height="150px"
                                                    RightToLeft="True" EnableSynchronization="False"
                                                    ValueField="EmpId" TextField="FullName" DataSourceID="ObjectDataSource_Employees"
                                                    SelectionMode="CheckColumn"
                                                    ClientInstanceName="lstNezamChartReciever">
                                                    <ValidationSettings>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <Columns>
                                                        <dxe:ListBoxColumn FieldName="FullName" Caption="نام و نام خانوادگی"></dxe:ListBoxColumn>
                                                        <dxe:ListBoxColumn FieldName="NcName" Caption="سمت"></dxe:ListBoxColumn>
                                                    </Columns>
                                                </TSPControls:CustomASPxListBox>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Name="NewPerson" Text="شماره های دستی">
                                    <ContentCollection>
                                        <dxw:ContentControl runat="server">
                                            <div align="right" dir="rtl">
                                                شماره تلفن همراه
                                                <br />
                                                <TSPControls:CustomASPXMemo runat="server" Height="100px" ID="txtOtherMobileNo"
                                                    Width="100%" ClientInstanceName="txtOtherMobileNo">
                                                    <ValidationSettings>
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <ClientSideEvents KeyPress="function(s,e){
        HandleGotoPageKeyDown(e.htmlEvent);
        }"
                                                        TextChanged="function(s,e){getNumberList(s.GetText());}" />
                                                </TSPControls:CustomASPXMemo>
                                                <br />
                                                <asp:Label runat="server" Text="* هر کدام از شماره ها باید در یک ردیف وارد شود<br>* نمونه شماره تلفن همراه : 09xxxxxxxxx"
                                                    ForeColor="DarkRed" ID="Label1"></asp:Label>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                            </TabPages>


                        </dxtc:ASPxPageControl>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>

            </TSPControls:CustomASPxPopupControl>
            <asp:ObjectDataSource ID="ObjdsSmsType" runat="server" SelectMethod="SelectByLastModified"
                TypeName="TSP.DataManager.SmsTypeModifiedManager">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="SmstypeId"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>

            <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="mygrid" SessionName="SendBackDataTable_SMSIns"
                OnCallback="WFUserControl_Callback" GridHasCallback="true" />
            <asp:ObjectDataSource ID="ObjectDataSource_Employees" runat="server" TypeName="TSP.DataManager.EmployeeManager"
                SelectMethod="SelectEmployeeWithNezamChart"></asp:ObjectDataSource>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldSMSDetail" ClientInstanceName="HiddenFieldSMSDetail">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="hiddenRecieverName" runat="server" ClientInstanceName="hiddenRecieverName">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="hiddenRecieverGroups" runat="server" ClientInstanceName="hiddenRecieverGroups">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="hiddenRecieverMembers" runat="server" ClientInstanceName="hiddenRecieverMembers">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="hiddenRecieverManualNo" runat="server" ClientInstanceName="hiddenRecieverManualNo">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="hiddenRecieverEmployee" runat="server" ClientInstanceName="hiddenRecieverEmployee">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="hiddenSaveID" runat="server">
            </dxhf:ASPxHiddenField>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanelNewSms"
                DisplayAfter="0" BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                        <img alt="" src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="TypeSMS.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function SetSmsBody() {
            txtAreaName = '<%=txtbSMSBody.ClientID%>';
        });
        $(document).ready(function () {
            PreInit();
        });

        function contentPageLoad() {
            PreInit();
        }

        function CheckBeforeSave() {
            try {
                if (parseInt(lblSMSCount.GetText()) > 4) {
                    alert(lblSMSCountWarning.GetText());
                    return false;
                }
            }
            catch (error) { }
            return true;
        }

        function StartSMSBodyCallBack(type) {
            CallbackPanelSMSBody.PerformCallback(hiddenRecieverMembers.Get('Members') + '&' + hiddenRecieverEmployee.Get('Id') + '#' + type);
        }
     
        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
        /****************************************************/
        function getMemberList(str) {
            str = str.replace('\n', '');
            str = str.replace('\r', '');
            var patt = new RegExp("^([0-9]+(;(?=[0-9]))?)*$");
            var res = patt.test(str);

            if (!res) {
                alert('فرمت لیست کد های عضویت قابل قبول نمی باشد.در انتهای لیست نبایستی کاراکتر اضافه بر کد عضویت وجود داشته باشد.');
                return false;
            }

            return true;
        }
        function getNumberList(str) {
            var Error = '';
            var arrStr = str.trim().replace(/\r/gi, '').replace(/\n/gi, ';').split(';');
            for (var i = 0; i < arrStr.length; i++)
                if (arrStr[i] != '' && CheckRowLength(arrStr[i]) == false) {
                    if (Error != '') Error += ',';
                    Error += (i + 1).toString();
                }
            if (arrStr.length > 0 && Error != '') {
                alert(': شماره های دستی وارد شده در خطوط زیر دارای خطا هستند\n' + Error);
                return false;
            }
            else if (arrStr.length > 1000) {
                alert('تعداد شماره های دستی نمی تواند بیشتر از 1000 شماره باشد');
                return false;
            }
            return true;
        }
        function CheckCode(key) {
            if (((key > 47) && (key < 58)) || (key == 13) || (key == 8) || (key == 59))
                return true;
            return false;
        }
        function CheckRowLength(InputStr) {
            var Str = InputStr.replace('\n', '');
            Str = Str.replace('\r', '');
            if (Str.length != 11)
                return false;
            if (Str[0] != '0' || Str[1] != '9')
                return false;
            return true;
        }


        function HandleGotoPageKeyDown(event) {

            var key;
            var evt;

            if (window.event) {
                key = window.event.keyCode;
                if (CheckCode(key)) {
                    window.event.returnValue = true;
                }
                else {
                    window.event.returnValue = false;
                }
            }
            else if (event) {
                key = event.which;
                if (CheckCode(key)) {
                    event.returnValue = true;
                }
                else {
                    event.preventDefault();
                }
            }
        }
        /************* popupChooseRecievers *****************/
        function SaveRecivers() {
            var NoOfRecievers = 0;
            if (getNumberList(txtOtherMobileNo.GetText()) == false) return;
            if (getMemberList(txtMembers_Reciever.GetText()) == false) return;
            txtRecievers.SetText('');

            if (txtMembers_Reciever.GetText() != '') {
                if (txtMembers_Reciever.GetText()[txtOtherMobileNo.GetText().length] == ';')
                    txtMembers_Reciever.SetText(txtMembers_Reciever.GetText().substring(0, txtMembers_Reciever.GetText().length));
            }
            hiddenRecieverMembers.Set('Members', '');
            if (txtMembers_Reciever.GetText() != '') {
                txtRecievers.SetText('اعضای حقیقی: ' + txtMembers_Reciever.GetText());
                NoOfRecievers += txtMembers_Reciever.GetText().split(';').length;
                hiddenRecieverMembers.Set('Members', txtMembers_Reciever.GetText());
            }

            if (txtRecievers.GetText() != '')
                txtRecievers.SetText(txtRecievers.GetText() + '\n');
            var EmployeeSelectedItems = lstNezamChartReciever.GetSelectedItems();
            NoOfRecievers += EmployeeSelectedItems.length;
            var EmployeeSelectedItemIds = '';
            var EmployeeSelectedItemNames = '';
            for (var i = 0; i < EmployeeSelectedItems.length; i++) {
                if (EmployeeSelectedItemIds != '') EmployeeSelectedItemIds += ';';
                EmployeeSelectedItemIds += EmployeeSelectedItems[i].value;
                if (EmployeeSelectedItemNames != '') EmployeeSelectedItemNames += '; ';
                EmployeeSelectedItemNames += EmployeeSelectedItems[i].text.split(';')[0]
            }
            if (EmployeeSelectedItems.length > 0) {
                txtRecievers.SetText(txtRecievers.GetText() + 'كارمندان: ' + EmployeeSelectedItemNames);
                //hiddenRecieverEmployee.Set('NcName', EmployeeSelectedItemNames);        
                hiddenRecieverEmployee.Set('Id', EmployeeSelectedItemIds);
            }
            else {
                //hiddenRecieverEmployee.Set('NcName','');
                hiddenRecieverEmployee.Set('Id', '');
            }


            if (txtOtherMobileNo.GetText() != '') {
                if (txtOtherMobileNo.GetText()[txtOtherMobileNo.GetText().length] == '\n')
                    txtOtherMobileNo.SetText(txtOtherMobileNo.GetText().substring(0, txtOtherMobileNo.GetText().length - 1));
            }

            if (txtOtherMobileNo.GetText() != '') {
                if (txtRecievers.GetText() != '')
                    txtRecievers.SetText(txtRecievers.GetText() + '\n');
                var OtherMobileNos = txtOtherMobileNo.GetText().replace(/\r/gi, '').replace(/\n/gi, ';').split(';');
                var MobileNos = '';
                for (var i = 0; i < OtherMobileNos.length; i++) {
                    if (MobileNos != '') MobileNos += ';';
                    MobileNos += OtherMobileNos[i];
                }
                txtRecievers.SetText(txtRecievers.GetText() + 'شماره های دستی: ' + MobileNos);
                NoOfRecievers += OtherMobileNos.length;
                hiddenRecieverManualNo.Set('MobileNo', MobileNos);
            }

            txtbRecieverCount.SetText(NoOfRecievers);
            popupChooseReciever.Hide();
            StartSMSBodyCallBack('Reciever');
        }

        function ClearRecivers() {
            if (confirm('آیا از حذف تمامی گیرندگان مطمئن هستید؟') == false) return;

            txtRecievers.SetText('');

            hiddenRecieverManualNo.Clear();
            txtOtherMobileNo.SetText('');
            hiddenRecieverMembers.Clear();
            txtMembers_Reciever.SetText('');
            hiddenRecieverEmployee.Clear();
            lstNezamChartReciever.UnselectAll();
        }
        function Fill_lstNezamChartReciever() {
            var tmpEmployee = hiddenRecieverEmployee.Get('Id').split(';');
            for (var i = 0; i < tmpEmployee.length; i++)
                if (tmpEmployee[i] != '')
                    lstNezamChartReciever.FindItemByValue(tmpEmployee[i]).selected = true;
        }
    </script>
</asp:Content>
