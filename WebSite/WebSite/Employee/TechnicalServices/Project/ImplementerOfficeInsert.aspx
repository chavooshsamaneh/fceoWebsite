<%@ Page Title="مشخصات مجریان انبوه ساز/پیمانکار" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ImplementerOfficeInsert.aspx.cs" Inherits="Employee_TechnicalServices_Project_ImplementerOfficeInsert" %>

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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<%@ Register Src="~/UserControl/CapacityUserControl.ascx" TagPrefix="TSP" TagName="Capacity" %>
<%@ Register Src="~/UserControl/WorkRequestUserControl.ascx" TagPrefix="TSP" TagName="WorkRequestUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text=""></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
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
                                            CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                        </TSPControls:MenuSeprator>
                                    </td>

                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
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
            <TSPControls:CustomAspxMenuHorizontal ID="MainMenu" runat="server" CssClass="ProjectMainMenuHorizontal" OnItemClick="MainMenu_ItemClick">
                <Items>
                    <dxm:MenuItem Text="مشخصات شرکت مجری" Name="OfficeInfo" ItemStyle-CssClass="ProjectMainMenuItemStyle" Selected="true">
                    </dxm:MenuItem>

                    <dxm:MenuItem Text="اعضای شرکت" Name="OfficeMember" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>
            <br />

            <TSPControls:CustomASPxRoundPanel ID="RoundPanelPage" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <fieldset id="Fieldset1"
                            runat="server">
                            <legend class="HelpUL">مشخصات شرکت</legend>
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
                                        <td align="right" valign="top" width="20%">کد نظام مهندسی
                                        </td>
                                        <td align="right" valign="top" width="30%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtImpOfficeId" Width="100%"
                                                Enabled="false">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td align="right" valign="top" width="20%">نوع شرکت مجری
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                ID="ComboOfficeType" ClientInstanceName="ComboOfficeType"
                                                ValueType="System.String" AutoPostBack="false"
                                                RightToLeft="True">
                                                <Items>
                                                    <dxe:ListEditItem Value="0" Text="انبوه ساز"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Value="1" Text="پیمانکار"></dxe:ListEditItem>
                                                </Items>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                    <RequiredField IsRequired="True" ErrorText="نوع شرکت مجری را انتخاب نمایید"></RequiredField>
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
                                        <td align="right" valign="top" width="20%">کد انبوه ساز/پیمانکار
                                        </td>
                                        <td align="right" valign="top" width="30%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtMeNo" Width="100%">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td align="right" valign="top" width="20%">
                                            <asp:Label runat="server" Text="شماره پروانه *" ID="Label1"></asp:Label>
                                        </td>
                                        <td colspan="3" align="right" valign="top">
                                            <TSPControls:CustomTextBox runat="server" ID="txtFileNo" Width="100%">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                    <RequiredField IsRequired="True" ErrorText="نشماره پروانه را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>

                                    </tr>
                                    <tr>
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
                                            <asp:Label runat="server" Text="تاریخ صدور اولین پروانه *" ID="Label2"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                Width="230px" ShowPickerOnTop="True" ID="txtDocFirstDate" PickerDirection="ToRight"
                                                IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                            <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                ErrorMessage="تاریخ را انتخاب نمایید" ControlToValidate="txtDocFirstDate" ID="PersianDateValidator1">تاریخ نامعتبر</pdc:PersianDateValidator>
                                        </td>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="تاریخ تمدید پروانه *" ID="Label3"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                Width="230px" ShowPickerOnTop="True" ID="txtDocDate" PickerDirection="ToRight"
                                                IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                            <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                ErrorMessage="تاریخ را انتخاب نمایید" ControlToValidate="txtDocDate" ID="PersianDateValidator3">تاریخ نامعتبر</pdc:PersianDateValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="تاریخ اعتبار پروانه *" ID="Label4"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                Width="230px" ShowPickerOnTop="True" ID="txtDocExpireDate" PickerDirection="ToRight"
                                                IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                            <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                ErrorMessage="تاریخ را انتخاب نمایید" ControlToValidate="txtDocExpireDate" ID="PersianDateValidator4">تاریخ نامعتبر</pdc:PersianDateValidator>
                                        </td>
                                        <td>استان محل صدور</td>
                                        <td>
                                            <TSPControls:CustomAspxComboBox ID="CombProvince" runat="server" ClientInstanceName="CombProvince"
                                                DataSourceID="OdbProvince"
                                                TextField="PrName" ValueField="PrId" ValueType="System.String"
                                                Width="100%" RightToLeft="True">
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
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
                                        <td>پایه اجرا</td>
                                        <td>
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                ID="CoboGrade" ClientInstanceName="CoboGrade" ValueType="System.String"
                                                EnableIncrementalFiltering="True"
                                                IncrementalFilteringMode="StartsWith" RightToLeft="True" DataSourceID="ObjectDataSourceGrade"
                                                TextField="GrdName" ValueField="GrdId">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="پایه را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                            <asp:ObjectDataSource ID="ObjectDataSourceGrade" runat="server" SelectMethod="GetData"
                                                TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>
                                        </td>
                                        <td>کد پستی</td>
                                        <td align="right" valign="top" width="30%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtPostalCode" Width="100%" ClientInstanceName="txtPostalCode" MaxLength="10">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="کد پستی را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,10}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
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
                                            <asp:Label runat="server" Text="تصویر روی پروانه *" ID="Label79"></asp:Label>
                                        </td>
                                        <td colspan="3" align="right" valign="top">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                ID="flpOfDocumentFront" InputType="Images" ClientInstanceName="flpOfDocumentFront" OnFileUploadComplete="flpOfDocumentFront_FileUploadComplete">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
                                                            if(e.isValid){
	    imgEndUploadDocumentFront.SetVisible(true);
	    HiddenFieldPage.Set('DocumentFront',1);
	    imgDocumentFront.SetVisible(true);
	    imgDocumentFront.SetImageUrl('../../../Image/TechnicalServices/Implementer/ImpelementerOffice/'+e.callbackData);
    }
    else
    {
        imgEndUploadDocumentFront.SetVisible(false);
	    HiddenFieldPage.Set('DocumentFront',0);
	    imgDocumentFront.SetVisible(false);
	    imgDocumentFront.SetImageUrl('');
    }
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxUploadControl>
                                                            <dxe:ASPxLabel runat="server" Text="تصویر روی پروانه شرکت را انتخاب نمایید" ClientVisible="False"
                                                                ID="ASPxLabel1" ForeColor="Red" ClientInstanceName="lblArm">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                ID="imgEndUploadDocumentFront" ClientInstanceName="imgEndUploadDocumentFront">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <dxe:ASPxImage runat="server" Height="75px" ClientVisible="False" Width="75px" ID="imgDocumentFront"
                                                ClientInstanceName="imgDocumentFront">
                                                <Border BorderWidth="1px" BorderStyle="Solid"></Border>
                                            </dxe:ASPxImage>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="تصویر پشت پروانه *" ID="Label80"></asp:Label>
                                        </td>
                                        <td colspan="3" align="right" valign="top">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                ID="flpOfDocumentBack" InputType="Images" ClientInstanceName="flpOfDocumentBack" OnFileUploadComplete="flpOfDocumentBack_FileUploadComplete">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
                                                            if(e.isValid){
	    imgEndUploadDocumentBack.SetVisible(true);
	    HiddenFieldPage.Set('DocumentBack',1);
	    ImageDocumentBack.SetVisible(true);
	    ImageDocumentBack.SetImageUrl('../../../Image/TechnicalServices/Implementer/ImpelementerOffice/'+e.callbackData);
    }
    else
    {
        imgEndUploadDocumentBack.SetVisible(false);
	    HiddenFieldPage.Set('DocumentBack',0);
	    ImageDocumentBack.SetVisible(false);
	    ImageDocumentBack.SetImageUrl('');
    }
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxUploadControl>
                                                            <dxe:ASPxLabel runat="server" Text="تصویر مهر شرکت را انتخاب نمایید" ClientVisible="False"
                                                                ID="ASPxLabel2" ForeColor="Red" ClientInstanceName="lblSign">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                ID="imgEndUploadDocumentBack" ClientInstanceName="imgEndUploadDocumentBack">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <dxe:ASPxImage runat="server" Height="75px" ClientVisible="False" Width="75px" ID="ImageDocumentBack"
                                                ClientInstanceName="ImageDocumentBack">
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
                                </tbody>
                            </table>
                        </fieldset>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">

                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">

                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5">
                                        </TSPControls:MenuSeprator>
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

            <dxhf:ASPxHiddenField ID="HiddenFieldPage" ClientInstanceName="HiddenFieldPage"
                runat="server">
            </dxhf:ASPxHiddenField>

            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                        <img alt="" id="IMG2" src="../../Image/indicator.gif" align="middle" />
                        لطفا صبر نمایید ...
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

