<%@ Page Title="تاییدیه سوابق کاری" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddMemberJobConfirm.aspx.cs" Inherits="Members_Documents_AddMemberJobConfirm" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Content" runat="server" style="width: 100%; display: block;" align="center">
        <script type="text/javascript" language="javascript">
            function SetConfimVisible() {
                if (cmbConfirmType.GetValue() == 0) {
                    lblDescription.SetVisible(true);
                    txtDescription.SetVisible(true);
                    lblOfficeName.SetVisible(true);
                    txtOfficeName.SetVisible(true);
                    lblOfficeMfNo.SetVisible(false);
                    txtOfficeMfNo.SetVisible(false);
                    lblHelpDes.SetVisible(true);
                    RoundPanelconfMember1.SetVisible(false);
                    lblGrid.SetVisible(true);
                    flpGrdAttach.SetVisible(true);
                    ImageGrd.SetVisible(true);
                    if (HiddenFieldPage.Get('GrdnameURL') != null)
                    { ImageGrd.SetImageUrl(HiddenFieldPage.Get('GrdnameURL')); }
                    lblProvince.SetVisible(false);
                    ComboProvince.SetVisible(false);
                }
                if (cmbConfirmType.GetValue() == 1) {
                    lblDescription.SetVisible(false);
                    txtDescription.SetVisible(false);
                    lblOfficeName.SetVisible(false);
                    txtOfficeName.SetVisible(false);
                    lblOfficeMfNo.SetVisible(false);
                    txtOfficeMfNo.SetVisible(false);
                    lblHelpDes.SetVisible(false);
                  
                    RoundPanelconfMember1.SetVisible(true);
                    lblGrid.SetVisible(false);
                    flpGrdAttach.SetVisible(false);
                    imgEndUploadGrd.SetVisible(false);
                 
                    lblValidationGrd.SetVisible(false);
                    ImageGrd.SetImageUrl('');
                 
                    HiddenFieldPage.Set('Grdname', 0);
                
                    ImageGrd.SetVisible(false);
                    lblProvince.SetVisible(false);
                    ComboProvince.SetVisible(false);
                }
                if (cmbConfirmType.GetValue() == 2) {
                    lblDescription.SetVisible(true);
                    txtDescription.SetVisible(true);
                    lblOfficeName.SetVisible(true);
                    txtOfficeName.SetVisible(true);
                    lblOfficeMfNo.SetVisible(false);
                    txtOfficeMfNo.SetVisible(false);
                    lblHelpDes.SetVisible(true);
                    RoundPanelconfMember1.SetVisible(false);
                    lblGrid.SetVisible(false);
                    flpGrdAttach.SetVisible(false);
                    imgEndUploadGrd.SetVisible(false);
                    lblValidationGrd.SetVisible(false);
                    ImageGrd.SetImageUrl('');
                    HiddenFieldPage.Set('Grdname', 0);
                    ImageGrd.SetVisible(false);
                    lblProvince.SetVisible(false);
                    ComboProvince.SetVisible(false);
                }
                if (cmbConfirmType.GetValue() == 3) {
                    lblProvince.SetVisible(true);
                    ComboProvince.SetVisible(true);
                    lblHelpDes.SetVisible(false);
                    //lblHelpDes.SetText('توضیحات کاملی در ارتباط با نام استان تاریخ تکمیل فرم اضافه شود');
                    RoundPanelconfMember1.SetVisible(false);
                    lblGrid.SetVisible(false);
                    flpGrdAttach.SetVisible(false);
                    imgEndUploadGrd.SetVisible(false);
                    lblValidationGrd.SetVisible(false);
                    ImageGrd.SetImageUrl('');
                    HiddenFieldPage.Set('Grdname', 0);
                    ImageGrd.SetVisible(false);
                    lblDescription.SetVisible(false);
                    txtDescription.SetVisible(false);
                    lblOfficeName.SetVisible(false);
                    txtOfficeName.SetVisible(false);
                    lblOfficeMfNo.SetVisible(false);
                    txtOfficeMfNo.SetVisible(false);
                }
            }
        </script>
           <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div align="right" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>

                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <div style="width: 100%" dir="rtl">
                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                    cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="جدید"  ToolTip="جدید"
                                                    CausesValidation="False" ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                    <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>
                                                
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="ویرایش"  ToolTip="ویرایش"
                                                    CausesValidation="true" Width="25px" ID="btnEdit" AutoPostBack="False"  UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                 
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="ذخیره"  ToolTip="ذخیره"
                                                    Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" CausesValidation="true"  EnableViewState="False"  ValidationGroup="j"
                                                    EnableTheming="False" OnClick="btnSave_Click">
                                               
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="بازگشت"  ToolTip="بازگشت"
                                                    CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnBack_Click">                                              
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>

                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelJobConfirm" HeaderText="مشاهده"
                    runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table width="100%">
                                  <tr>

                                                <td width="20%" align="right" valign="top"><strong>تاریخ همکاری از* :</strong>
                                                </td>
                                                <td width="30%" align="right" valign="top">
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="240px" ShowPickerOnTop="True"
                                                        ID="txtDateFrom" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                                        ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                                      <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ValidationGroup="j" ClientValidationFunction="PersianDateValidator"
                                                        ErrorMessage="ابتدای تاریخ همکاری را وارد نمایید" ControlToValidate="txtDateFrom"
                                                        ID="PersianDateValidator1">ابتدای تاریخ همکاری را وارد نمایید</pdc:PersianDateValidator>
                                                </td>
                                                <td width="20%" align="right" valign="top"><strong>تاریخ همکاری تا* :</strong>
                                                </td>
                                                <td width="30%" align="right" valign="top">
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="240px" ShowPickerOnTop="True"
                                                        ID="txtDateTo" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                                        ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                                     <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ValidationGroup="j" ClientValidationFunction="PersianDateValidator"
                                                        ErrorMessage="انتهای تاریخ همکاری را وارد نمایید" ControlToValidate="txtDateTo"
                                                        ID="PersianDateValidator2">انتهای تاریخ همکاری را وارد نمایید</pdc:PersianDateValidator>

                                                </td>

                                            </tr>
                                            <tr>

                                                <td align="right" valign="top">سمت* :
                                                </td>
                                                <td align="right" valign="top" colspan="3">
                                                    <TSPControls:CustomTextBox runat="server"  Width="100%" MaxLength="150" ID="txtPosition"
                                                        ClientInstanceName="txtPosition">
                                                        <ValidationSettings Display="Dynamic" ValidationGroup="j"  ErrorTextPosition="Bottom">
                                                            <RequiredField ErrorText="سمت را وارد نمایید" IsRequired="true"></RequiredField>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>

                                <tr>
                                    <td width="20%" align="right" valign="top">
                                        نوع تایید کننده*
                                    </td>
                                    <td width="30%" align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox EnableIncrementalFiltering="true" runat="server" Width="100%" RightToLeft="True"
                                             ID="cmbConfirmType"  ValueType="System.String"
                                            ClientInstanceName="cmbConfirmType">
                                            <Items>
                                                <dxe:ListEditItem Text="شرکت خصوصی" Value="0" Selected="true" />
                                                <dxe:ListEditItem Text="عضو حقیقی سازمان(تایید دو نفر مهندس از استان فارس)" Value="1" />
                                                <dxe:ListEditItem Text="شرکت یا نهاد دولتی" Value="2" />
                                                <dxe:ListEditItem Text="عضو حقیقی در دیگر استانها(تایید دو نفر مهندس از سایر استان ها)"
                                                    Value="3" />
                                            </Items>
                                            <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                ErrorTextPosition="Bottom">
                                              
                                                <RequiredField IsRequired="True" ErrorText="نوع تایید کننده را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){
                                                        SetConfimVisible();
                                                        }" />
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td width="20%" align="right" valign="top">
                                    </td>
                                    <td width="30%" align="right" valign="top">
                                    </td>
                                </tr>
                                <tr>
                                    <td width="20%" align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="نام شرکت*" ID="lblOfficeName" ClientInstanceName="lblOfficeName"
                                            Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td colspan="3" width="80%" align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server"  Width="100%" MaxLength="30" ID="txtOfficeName"
                                            ClientInstanceName="txtOfficeName">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText="نام شرکت را وارد نمایید" IsRequired="true"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel runat="server" Text="استان*" ID="lblProvince" ClientInstanceName="lblProvince"
                                            Width="100%" ClientVisible="false">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxComboBox ID="ComboProvince" runat="server" ClientInstanceName="ComboProvince"
                                            DataSourceID="OdbProvince"
                                             TextField="PrName" ValueField="PrId" ValueType="System.String"
                                            Width="100%" RightToLeft="True" ClientVisible="false">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="j">
                                             
                                                <RequiredField ErrorText="استان را انتخاب نمایید" IsRequired="True" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                        <asp:ObjectDataSource ID="OdbProvince" runat="server" TypeName="TSP.DataManager.ProvinceManager"
                                            SelectMethod="GetData" CacheDuration="30" FilterExpression="NezamCode<>{0}">
                                            <FilterParameters>
                                                <asp:Parameter Name="newparameter" />
                                            </FilterParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="شماره پروانه شرکت" ID="lblOfficeMfNo" ClientVisible="false"
                                            ClientInstanceName="lblOfficeMfNo" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server"  Width="100%" MaxLength="30" ClientVisible="false"
                                            ID="txtOfficeMfNo" ClientInstanceName="txtOfficeMfNo">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText="شماره پروانه شرکت را وارد نمایید" IsRequired="true"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="20%" align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="شرح فعالیت شرکت" ID="lblDescription" ClientInstanceName="lblDescription"
                                            Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="30px"  Width="100%" ID="txtDescription"
                                            ClientInstanceName="txtDescription">
                                        </TSPControls:CustomASPXMemo>
                                        <dxe:ASPxLabel runat="server" Text="به صورت خلاصه رشته هایی که شرکت مورد نظر در آن زمینه ها فعالیت می کند را نام ببرید.(عمران/یک یا چند رشته از رشته های نقشه برداری،معماری،شهرسازی،تاسیسات و یا تاسیسات مکانیکی)  "
                                            ID="lblHelpDes" ClientInstanceName="lblHelpDes" ForeColor="Red" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="تصویر فرم تاییدیه سابقه کار *" ID="Label50"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl ID="flpConfAttach" runat="server" ClientInstanceName="flpConfAttach"
                                                            UploadWhenFileChoosed="true" OnFileUploadComplete="flpConfAttach_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadConf.SetVisible(true);
  	 HiddenFieldPage.Set('Confname',1);
	lblValidationConf.SetVisible(false);
	
    ImageConf.SetVisible(true);
	ImageConf.SetImageUrl('../../Image/DocMeFile/JobConfirm/'+e.callbackData);
	}
	else{
	imgEndUploadConf.SetVisible(false);
	lblValidationConf.SetVisible(true);

    ImageConf.SetVisible(false);
	ImageConf.SetImageUrl('');
	}
}" />
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="lblValidationConf" runat="server" ClientInstanceName="lblValidationConf"
                                                            ClientVisible="False" ForeColor="Red" Text="تصویر  فرم سابقه کار را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage ID="imgEndUploadConf" runat="server" ClientInstanceName="imgEndUploadConf"
                                                            ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="ImageConf" ClientInstanceName="ImageConf"
                                            Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                            <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                            </EmptyImage>
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تصویر پروانه یا گواهی رتبه بندی*" ClientInstanceName="lblGrid"
                                            ID="lblGrid">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl ID="flpGrdAttach" runat="server" ClientInstanceName="flpGrdAttach"
                                                            UploadWhenFileChoosed="true" OnFileUploadComplete="flpConfAttach_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadGrd.SetVisible(true);
  	 HiddenFieldPage.Set('Grdname',1);
	lblValidationGrd.SetVisible(false);
    ImageGrd.SetVisible(true);
	ImageGrd.SetImageUrl('../../Image/DocMeFile/OfficeGrade/'+e.callbackData);
    HiddenFieldPage.Set('GrdnameURL','../../Image/DocMeFile/OfficeGrade/'+e.callbackData);
	}
	else{
	imgEndUploadGrd.SetVisible(false);
	lblValidationGrd.SetVisible(true);
    ImageGrd.SetVisible(false);
	ImageGrd.SetImageUrl('');
	}
}" />
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="lblValidationGrd" runat="server" ClientInstanceName="lblValidationGrd"
                                                            ClientVisible="False" ForeColor="Red" Text="تصویر  پروانه یا گواهی رتبه بندی را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage ID="imgEndUploadGrd" runat="server" ClientInstanceName="imgEndUploadGrd"
                                                            ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="ImageGrd" ClientInstanceName="ImageGrd"
                                            Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                            <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                            </EmptyImage>
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <TSPControls:CustomASPxRoundPanel ID="RoundPanelconfMember1" ClientInstanceName="RoundPanelconfMember1"
                                            ClientVisible="false" HeaderText="مشخصات تایید کننده " runat="server" Width="100%">
                                            <PanelCollection>
                                                <dxp:PanelContent>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="right" valign="top" colspan="4">
                                                               <dxe:ASPxLabel CssClass="HelpUL" runat="server" Text="توجه! جهت اطلاع رسانی سیستم به صورت اتوماتیک پیام کوتاه و نامه الکترونیک به عضوانتخاب شده ارسال می کند. همچنین جهت اطمینان بیشتر کارشناسان واحد پروانه با وی تماس تلفنی حاصل می نمایند. بنابراین قبلا هماهنگی های لازم را انجام دهید"
                                            ID="lblHelpMeId"  ClientInstanceName="lblHelpMeId" Width="100%">
                                        </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="30%" align="right" valign="top">
                                                                کد عضویت:
                                                            </td>
                                                            <td width="30%" align="right" valign="top">
                                                                <TSPControls:CustomTextBox runat="server"  Width="100%" MaxLength="30" ID="txtMeId"
                                                                    OnTextChanged="txtMeId_TextChanged" AutoPostBack="true">
                                                                    <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                                        <RequiredField ErrorText="کد عضویت را وارد نمایید" IsRequired="true"></RequiredField>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                            <td width="20%" align="right" valign="top">
                                                            </td>
                                                            <td width="20%" align="right" valign="top">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                نام و نام خوانوادگی:
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="- - -" ID="lblMeName" Font-Bold="true" Width="100%">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                شماره پروانه:
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="- - -" ID="lblMeFileNo" Font-Bold="true" Width="100%">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                تاریخ اخذ مدرک فارغ التحصیلی:
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="- - -" ID="lblLicenseDate" Font-Bold="true" Width="100%">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="right" valign="top">
                                                            </td>
                                                            <td align="right" valign="top">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </TSPControls:CustomASPxRoundPanel>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                    <table  >
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="جدید"  ToolTip="جدید"
                                                        ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False">
                                                   
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="ویرایش"  ToolTip="ویرایش"
                                                        Width="25px" ID="btnEdit2"  CausesValidation="true" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False">
                                                        <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>
                                                  
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="ذخیره"  ToolTip="ذخیره"
                                                        Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False"  CausesValidation="true" EnableViewState="False" ValidationGroup="j"
                                                        EnableTheming="False" OnClick="btnSave_Click">
                                                    
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="بازگشت"  ToolTip="بازگشت"
                                                        ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                        OnClick="btnBack_Click">
                                                    
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>
                                    <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldExam" ClientInstanceName="HiddenFieldExam">
                                    </dxhf:ASPxHiddenField>

                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                    AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
                    <ProgressTemplate>
                        <div style="font-family: Tahoma; font-size: 9pt; text-align: center; padding-top: 25px;
                            width: 300px; height: 41px; background-image: url(../Images/UploadBg.png);">
                            <img id="IMG1" src="../../Images/indicator.gif" align="middle" />
                            لطفا صبر نمایید ...</div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>

                       <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldPage" ClientInstanceName="HiddenFieldPage">
                </dxhf:ASPxHiddenField>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
