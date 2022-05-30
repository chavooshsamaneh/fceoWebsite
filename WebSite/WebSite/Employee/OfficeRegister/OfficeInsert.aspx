﻿<%@ Page Title="مشخصات عضو حقوقی" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeInsert.aspx.cs" Inherits="Employee_OfficeRegister_OfficeInsert" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls.FormCreatorComponents"
    TagPrefix="cc2" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
                CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
                FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
            </pdc:PersianDateScriptManager>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">

                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" ID="btnEdit" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">

                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
if(CheckCharacterEncoding(txt1.GetText())==false)
    {
     txt1.SetIsValid(false);
     txt1.SetErrorText('حروف وارد شده نامعتبر است');
	 e.processOnServer=false;
    }
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False"
                                            AutoPostBack="False">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/printers.png" />
                                            <ClientSideEvents Click="function(s, e) {
	Callback.PerformCallback('Print');
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <TSPControls:CustomAspxMenuHorizontal ID="MenuDetails" runat="server"
                OnItemClick="MenuDetails_ItemClick">

                <Items>
                    <dxm:MenuItem Name="Office" Text="مشخصات شرکت" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Member" Text="اعضا">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Agent" Text="شعبه ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Letters" Text="روزنامه های رسمی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Financial" Text="وضعیت مالی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Attach" Text="مستندات">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Group" Text="گروه ها">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>

            <ul class="HelpUL">
                <li>در صورتی که وضعیت عضویت شرکت در حالت ''تایید مشروط'' باشد ، تنها اعضای غیرفعال شده
                    شرکت قادر به عضویت در سایر شرکت و یا دفاتر در سازمان می باشند و فعالیت سایر اعضای
                    شرکت تا مشخص شدن وضعیت شرکت و تایید مجدد آن منع می گردد </li>
            </ul>

            <TSPControls:CustomASPxRoundPanel ID="RoundPanelOffice" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="text-align: right" dir="rtl" width="100%">
                            <tbody>
                                <tr>
                                    <td valign="top" align="center" colspan="4">
                                        <dxe:ASPxLabel runat="server" Text="وضعیت درخواست:" Font-Bold="False" ID="lblWorkFlowState"
                                            ForeColor="Red">
                                        </dxe:ASPxLabel>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" width="20%">
                                        <asp:Label runat="server" Text="کد عضویت" ID="Label4" Width="100%"></asp:Label>
                                    </td>
                                    <td align="right" valign="top" width="30%">
                                        <TSPControls:CustomTextBox runat="server" ID="txtOfId" Width="100%"
                                            Enabled="false">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top" width="20%">شماره عضویت
                                    </td>
                                    <td align="right" valign="top" width="30%">
                                        <TSPControls:CustomTextBox runat="server" ID="txtMeNo" Width="100%"
                                            Enabled="false">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" width="20%">
                                        <asp:Label runat="server" Text="نام شرکت *" ID="Labe59"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" ID="txtOfName" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="نام شرکت را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" width="20%">
                                        <asp:Label runat="server" Text="نام شرکت(انگلیسی)" ID="Label67"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" ID="txtOfNameEn" Width="100%"
                                            ClientInstanceName="txt1">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="false" ErrorText="نام شرکت را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" width="20%">
                                        <asp:Label runat="server" Text="موضوع شرکت *" ID="Label61"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" ID="txtOfSubject" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="موضوع شرکت را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" width="20%">
                                        <dxe:ASPxLabel runat="server" Text="زمینه موضوعی شرکت*" ID="lblMembershipRequstType" ClientInstanceName="lblMembershipRequstType">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" width="30%">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            ID="cmbMembershipRequstType" ClientInstanceName="cmbMembershipRequstType"
                                            ValueType="System.String" AutoPostBack="false"
                                            RightToLeft="True">
                                            <Items>
                                                <dxe:ListEditItem Value="1" Text="انبوه ساز"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="2" Text="سازندگان مسکن و ساختمان"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="3" Text="شرکت خدمات فنی آزمایشگاهی"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="4" Text="شرکت کنترل نظارت ساختمان"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="5" Text="شرکت طراحی و نظارت"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="6" Text="مجری لوله کشی گاز"></dxe:ListEditItem>
                                            </Items>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="زمینه موضوعی شرکت را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td align="right" valign="top" width="20%">
                                        <dxe:ASPxLabel runat="server" Text="نوع فعالیت اجرایی*" ID="lblActivityType" ClientInstanceName="lblActivityType"
                                            ClientVisible="false">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" width="30%">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            ID="cmbActivityType" ClientInstanceName="cmbActivityType"
                                            ValueType="System.String" AutoPostBack="false"
                                            RightToLeft="True" ClientVisible="false">
                                            <Items>
                                                <dxe:ListEditItem Value="0" Text="پیمان مدیریت"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="1" Text="پیمانکاری یا پیمان مدیریت"></dxe:ListEditItem>
                                            </Items>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="نوع فعالیت اجرایی را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td align="right" valign="top" style="width: 20%">
                                        <asp:Label runat="server" Text="نوع شرکت*" ID="Label60"></asp:Label>
                                    </td>
                                    <td dir="ltr" align="right" valign="top" width="30%">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="OtName" ID="drdOfType" DataSourceID="OdbOfType"
                                            ValueType="System.String" ValueField="OtId"
                                            EnableIncrementalFiltering="True" RightToLeft="True" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="نوع شرکت را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                        <asp:ObjectDataSource ID="OdbOfType" runat="server" TypeName="TSP.DataManager.OfficeTypeManager"
                                            SelectMethod="GetData" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                                    </td>
                                    <td align="right" valign="top" width="20%">
                                        <asp:Label runat="server" Text="شماره ثبت شرکت *" ID="Label62"></asp:Label>
                                    </td>
                                    <td align="right" valign="top" width="30%">
                                        <TSPControls:CustomTextBox runat="server" ID="txtOfRegNo" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="شماره ثبت را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,10}"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="محل ثبت شرکت *" ID="Label63"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" ID="txtOfRegPlace" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="محل ثبت شرکت را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="تاریخ ثبت شرکت *" ID="Label64"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                            Width="230px" ShowPickerOnTop="True" ID="txtOfRegDate" PickerDirection="ToRight"
                                            IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                            ErrorMessage="تاریخ را انتخاب نمایید" ControlToValidate="txtOfRegDate" ID="PersianDateValidator2">تاریخ نامعتبر</pdc:PersianDateValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="سرمایه شرکت(ریال) *" ID="Label65"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" ID="txtOfValue" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="سرمایه شرکت را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,11}"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="تعداد سهام شرکت *" ID="Label66"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" ID="txtOfStock" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="تعداد سهام شرکت را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,11}"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="نوع فعالیت" ID="Label68" Visible="False"></asp:Label>
                                    </td>
                                    <td dir="ltr" align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox runat="server" Visible="False"
                                            TextField="OatName" ID="aspxcmbAttype"
                                            DataSourceID="OdbOfAtType" ValueType="System.String" ValueField="OatId"
                                            EnableIncrementalFiltering="True"
                                            Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="نوع فعالیت را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                        <asp:ObjectDataSource ID="OdbOfAtType" runat="server" TypeName="TSP.DataManager.OfficeActivityTypeManager"
                                            SelectMethod="GetData"></asp:ObjectDataSource>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="نوع تأئید" ID="lblTaeed" Visible="False"></asp:Label>
                                    </td>
                                    <td dir="ltr" align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox runat="server" Visible="False"
                                            TextField="MrsName" ID="drdMrsId" DataSourceID="ODBMrsId"
                                            ValueType="System.String" ValueField="MrsId"
                                            EnableIncrementalFiltering="True" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="نوع تأیید را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                        <asp:ObjectDataSource ID="ODBMrsId" runat="server" TypeName="TSP.DataManager.MembershipRegistrationStatusManager"
                                            SelectMethod="GetData"></asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="آدرس شرکت *" ID="Labe76"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtOfAddress" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="آدرس شرکت را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="تلفن 1 *" ID="Labe69"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td align="right" valign="top" width="70%">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtOfTel1" Width="100%" MaxLength="8">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="شماره تلفن را وارد نمایید"></RequiredField>
                                                            <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{5,8}"></RegularExpression>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td align="right" valign="top" width="10%">
                                                    <asp:Label runat="server" Text="-" Width="13px" ID="Labe71"></asp:Label>
                                                </td>
                                                <td align="right" valign="top" width="20%">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtOfTel1_pre" Width="100%"
                                                        MaxLength="4">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField ErrorText="پیش شماره تلفن را وارد نمایید"></RequiredField>
                                                            <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}"></RegularExpression>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="تلفن 2" ID="Label70"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td align="right" valign="top" width="70%">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtOfTel2" Width="100%" MaxLength="8">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{5,8}"></RegularExpression>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td align="right" valign="top" width="10%">
                                                    <asp:Label runat="server" Text="-" Width="13px" ID="Label72"></asp:Label>
                                                </td>
                                                <td align="right" valign="top" width="20%">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtOfTel2_pre" Width="100%"
                                                        MaxLength="4">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}"></RegularExpression>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="فکس" ID="Labe73"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td align="right" valign="top" width="70%">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtOfFax" Width="100%" MaxLength="8">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{5,8}"></RegularExpression>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td align="right" valign="top" width="10%">
                                                    <asp:Label runat="server" Text="-" Width="13px" ID="Labe74"></asp:Label>
                                                </td>
                                                <td align="right" valign="top" width="20%">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtOfFax_pre" Width="100%"
                                                        MaxLength="4">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}"></RegularExpression>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="شماره همراه مدیر عامل *" ID="Label75"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" ID="txtOfMobile" Width="100%"
                                            MaxLength="11">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="شماره همراه را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{1,10}"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="آدرس وب سایت" ID="Labe77"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" ID="txtOfWebsite" Width="300px">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText="آدرس وب سایت را با فرمت http://www.Example.com وارد نمایید"
                                                    ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="آدرس پست الکترونیکی" ID="Labe82"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" ID="txtOfEmail" Width="300px">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText="آدرس پست الکترونیکی را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText=" آدرس پست الکترونیکی را با فرمت صحیح وارد نمایید" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="تصویر آرم شرکت *" ID="Label79"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                            ID="flpOfArm" InputType="Images" ClientInstanceName="flpArm" OnFileUploadComplete="flpOfArm_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                            if(e.isValid){
	imgArm.SetVisible(true);
	flpArm2.Set('name',1);
	ImageArm.SetVisible(true);
	ImageArm.SetImageUrl('../../Image/Temp/'+e.callbackData);
    }
    else
    {
    imgArm.SetVisible(false);
	flpArm2.Set('name',0);
	ImageArm.SetVisible(false);
	ImageArm.SetImageUrl('');
    }
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="تصویر آرم شرکت را انتخاب نمایید" ClientVisible="False"
                                                            ID="ASPxLabel1" ForeColor="Red" ClientInstanceName="lblArm">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="imgEndUploadImg" ClientInstanceName="imgArm">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxImage runat="server" Height="75px" ClientVisible="False" Width="75px" ID="imgOfArm"
                                            ClientInstanceName="ImageArm">
                                            <Border BorderWidth="1px" BorderStyle="Solid"></Border>
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="تصویر مهر شرکت *" ID="Label80"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                            ID="flpOfSign" InputType="Images" ClientInstanceName="flpSign" OnFileUploadComplete="flpOfSign_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                            if(e.isValid){
	imgSign.SetVisible(true);
	flpSign2.Set('name',1);
	ImageSign.SetVisible(true);
	ImageSign.SetImageUrl('../../Image/Temp/'+e.callbackData);
    }
    else
    {
    imgSign.SetVisible(false);
	flpSign2.Set('name',0);
	ImageSign.SetVisible(false);
	ImageSign.SetImageUrl('');
    }
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="تصویر مهر شرکت را انتخاب نمایید" ClientVisible="False"
                                                            ID="ASPxLabel2" ForeColor="Red" ClientInstanceName="lblSign">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="ASPxImage1" ClientInstanceName="imgSign">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxImage runat="server" Height="75px" ClientVisible="False" Width="75px" ID="imgOfSign"
                                            ClientInstanceName="ImageSign">
                                            <Border BorderWidth="1px" BorderStyle="Solid"></Border>
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="توضیحات" ID="Label81"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtOfDesc" Width="100%">
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" colspan="4">
                                        <TSPControls:CustomASPxCheckBox ForeColor="DarkViolet" Font-Bold="true" ID="CheckBoxConditionalApprove" Width="100%"
                                            RightToLeft="True" Text="با آگاهی کامل از قوانین سازمان نظام مهندسی و مبحث دوم قصد تایید مشروط این عضویت حقوقی را دارا می باشم و هرگونه عواقب آن را به عهده می گیرم"
                                            runat="server">
                                        </TSPControls:CustomASPxCheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right" style="width: 15%">
                                        <asp:Label runat="server" Text="شرح درخواست" Width="100%" ID="Label19"></asp:Label>
                                    </td>
                                    <td style="width: 85%" colspan="3" align="right" valign="top">
                                        <TSPControls:CustomASPXMemo runat="server" Height="40px" ID="txtReRequestDesc"
                                            Width="100%">
                                            <ClientSideEvents KeyPress="function(s,e){ CheckDevExpressTextboxLengthOnKeyPress(s,e,255); }" />
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="کد پیگیری" Width="100%" ID="ASPxLabel3">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td dir="rtl" valign="top" align="right" style="width: 35%">
                                        <TSPControls:CustomTextBox runat="server" Text="0000000000" ID="txtFollowCode"
                                            Width="100%" Enabled="False" ReadOnly="True">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>
                        <fieldset>
                            <legend class="HelpUL">فایل های پیوست</legend>
                            <dx:ASPxPanel id="RoundPanelFileAttachment"
                            runat="server"><PanelCollection><dx:PanelContent>
                            <table runat="server" id="TblFile" dir="rtl" width="100%">
                                <tr runat="server" id="Tr14">
                                    <td runat="server" id="Td41" style="vertical-align: top; text-align: right">
                                        <asp:Label runat="server" Text="فایل" Width="24px" ID="lblimg"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td42" align="right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                            ID="flp" InputType="Files" ClientInstanceName="flpc" OnFileUploadComplete="flp_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                        if(e.isValid){
	imgEndUploadImgClient3.SetVisible(true);
flpme.Set('name',1);
	lbl1.SetVisible(false);
    }
    else{
	imgEndUploadImgClient3.SetVisible(false);
flpme.Set('name',0);
	lbl1.SetVisible(false);
    }
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                            ID="ASPxLabel6" ForeColor="Red" ClientInstanceName="lbl1">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="ASPxImage2" ClientInstanceName="imgEndUploadImgClient3">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr15">
                                    <td runat="server" id="Td43" style="vertical-align: top; text-align: right">
                                        <asp:Label runat="server" Text="توضیحات" Width="82px" ID="Label52"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td44" style="text-align: right">
                                        <TSPControls:CustomASPXMemo runat="server" Height="30px" ID="txtDescImg" Width="530px">
                                            <ValidationSettings>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr16">
                                    <td runat="server" id="Td45" align="center" colspan="2">
                                        <br />
                                        <TSPControls:CustomAspxButton runat="server" Text="اضافه" CausesValidation="False"
                                            Width="70px" ID="btnAddFlp" UseSubmitBehavior="False"
                                            OnClick="btnAddFlp_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if(flpme.Get('name')!=1)
{
lbl1.SetVisible(true);

e.processOnServer=false;
}
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <TSPControls:CustomAspxDevGridView2 runat="server" EnableViewState="False"
                                Width="100%" ID="AspxGridFlp" KeyFieldName="Id" AutoGenerateColumns="False">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FilePath" Caption="فایل"
                                        Name="FilePath">
                                        <DataItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" Text="آدرس فایل" NavigateUrl='<%# Bind("TempImgUrl") %>'
                                                Target="_blank"></asp:HyperLink>
                                        </DataItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Description" Caption="توضیحات"
                                        Name="Description">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView2>
                                </dx:PanelContent></PanelCollection></dx:ASPxPanel>
                        </fieldset>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" ID="btnEdit2" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if(CheckCharacterEncoding(txt1.GetText())==false)
 {
txt1.SetIsValid(false);
txt1.SetErrorText('حروف وارد شده نامعتبر است');
	e.processOnServer=false;
}
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>

                                    <td>
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint2" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False"
                                            AutoPostBack="False">

                                            <Image Url="~/Images/icons/printers.png" />
                                            <ClientSideEvents Click="function(s, e) {
		Callback.PerformCallback('Print');
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">

                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField ID="HiddenFieldOffice" runat="server" ClientInstanceName="HiddenFieldOffice">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpArm" runat="server" ClientInstanceName="flpArm2">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpSign" runat="server" ClientInstanceName="flpSign2">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpMember" runat="server" ClientInstanceName="flpme">
            </dxhf:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
