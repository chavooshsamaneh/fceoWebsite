<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="EmployeeInsert.aspx.cs" Inherits="Employee_Employee_EmployeeInsert"
    Title="مشخصات کارمند" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="idcontent" style="width: 100%;" align="center">
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
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
                                                CausesValidation="False" ID="btnNew" EnableClientSideAPI="True" EnableViewState="False"
                                                EnableTheming="False" UseSubmitBehavior="False" OnClick="btnNew_Click">
                                                <ClientSideEvents Click="function(s, e) {
	//SetNewMode();
}"></ClientSideEvents>

                                                <Image Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                CausesValidation="False" Width="25px" ID="btnEdit" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnEdit_Click" ClientInstanceName="btnEditClient" EnableClientSideAPI="True"
                                                UseSubmitBehavior="False">
                                                <ClientSideEvents Click="function(s, e) {

}
"></ClientSideEvents>

                                                <Image Url="~/Images/icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                                ID="btnSave" EnableClientSideAPI="True" EnableTheming="False" ClientInstanceName="btnSaveClient"
                                                OnClick="btnSave_Click" EnableViewState="False" UseSubmitBehavior="False">

                                                <Image Url="~/Images/icons/save.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
	
if(CheckCharacterEncoding(txtFirstNameEnClient.GetText())==false)
 {
txtFirstNameEnClient.SetIsValid(false);
txtFirstNameEnClient.SetErrorText('حروف وارد شده نامعتبر است');
	e.processOnServer=false;
}
if(CheckCharacterEncoding(txtLastNameEnClient.GetText())==false)
 {
txtLastNameEnClient.SetIsValid(false);
txtLastNameEnClient.SetErrorText('حروف وارد شده نامعتبر است');
	e.processOnServer=false;
}

if(flpme.Get('name')!=1)
{
lbl1.SetVisible(true);

e.processOnServer=false;
}
}
" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                CausesValidation="False" ID="btnBack" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnBack_Click" UseSubmitBehavior="False">

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
                <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldEmployee" ClientInstanceName="HiddenFieldEmployeeClient">
                </dxhf:ASPxHiddenField>
                <br />

                <TSPControls:CustomASPxRoundPanel ID="RoundPanelEmlpoyee" HeaderText="مشاهده" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table dir="rtl" id="Table2" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" width="15%">
                                            <asp:Label runat="server" Text="کد پرسنلی" ID="Label1"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtEmpCode"
                                                EnableClientSideAPI="True" Width="100%" ReadOnly="True" ClientInstanceName="txtEmpCodeClient">
                                                <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText=" کد کارمند را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td width="15%"></td>
                                        <td width="35%"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="نام" ID="Label2"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtFirstName"
                                                EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtFirstNameClient">
                                                <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="(انگلیسی)" ID="Label4" Style="right: 301px; top: -18px"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtFirstNameEn"
                                                EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtFirstNameEnClient">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نام(انگلیسی) را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="نام خانوادگی" ID="Label3"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtLastName"
                                                EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtLastNameClient">
                                                <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText=" نام خانوادگی را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="(انگلیسی)" ID="Label5"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtLastNameEn"
                                                EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtLastNameEnClient">
                                                <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نام خانوادگی(انگلیسی) را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="نام پدر" ID="Label6"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtFatherName"
                                                EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtFatherNameClient">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نام پدر را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right"></td>
                                        <td valign="top" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="تاریخ تولد" ID="Label7"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                Width="185px" ShowPickerOnTop="True" ID="txtBirthDate" PickerDirection="ToRight"
                                                RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBirthDate" ID="RequiredFieldValidator1"
                                                Display="Dynamic">تاریخ را وارد نمایید</asp:RequiredFieldValidator>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="محل تولد" ID="Label8"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtBirthPlace"
                                                EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtBirthPlaceClient">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره شناسنامه" Width="91px" ID="Label10"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtIdNo" EnableClientSideAPI="True"
                                                Width="100%" MaxLength="10" ClientInstanceName="txtIdNoClient">
                                                <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="شماره شناسنامه را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,10}"></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="کد ملی" ID="Label9"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtSSN" EnableClientSideAPI="True"
                                                Width="100%" MaxLength="10" ClientInstanceName="txtSSNClient">
                                                <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="کد ملی را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="\d{10}"></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="جنسیت" ID="Label11"></asp:Label>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                TextField="SexName" ID="cmbSexId" DataSourceID="ObjectDataSourceDrpSex" EnableClientSideAPI="True"
                                                ValueType="System.String" ValueField="SexId" ClientInstanceName="cmbSexIdClient">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="جنسیت را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <asp:Label runat="server" Text="وضعیت تاهل" Width="82px" ID="Label12"></asp:Label>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                TextField="MarName" ID="cmbMarId" DataSourceID="ObjectDataSourceDrpMar" EnableClientSideAPI="True"
                                                ValueType="System.String" ValueField="MarId" ClientInstanceName="cmbMarIdClient">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="آدرس" ID="Label22"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" colspan="5">
                                            <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtAddress" EnableClientSideAPI="True"
                                                Width="100%" ClientInstanceName="txtAddressClient">
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
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره تلفن" ID="Label13"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtTel" EnableClientSideAPI="True"
                                                Width="100%" ClientInstanceName="txtTelClient">
                                                <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RegularExpression ErrorText="شماره را با پیش شماره وارد نمایید" ValidationExpression="\d{11,12}"></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره همراه" ID="Label14"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtMobileNo"
                                                EnableClientSideAPI="True" Width="100%" MaxLength="11" ClientInstanceName="txtMobileNoClient">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="شماره همراه را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="0\d{1,10}"></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="آدرس وب سایت" ID="Label15"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" colspan="5">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtWebSite"
                                                EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtWebSiteClient">
                                                <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RegularExpression ErrorText="آدرس وب سایت را با فرمت http://www.Example.com وارد نمایید"
                                                        ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="آدرس پست الکترونیکی" Width="123px" ID="Label16"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" colspan="5">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtEmail" EnableClientSideAPI="True"
                                                Width="100%" ClientInstanceName="txtEmailClient">
                                                <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="آدرس پست الکترونیکی را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="آدرس را با فرمت صحیح وارد نمایید" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="ملیت" ID="Label17"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtNationality"
                                                EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtNationalityClient">
                                                <ValidationSettings>
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="مذهب" ID="Label18" Visible="False"></asp:Label>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" ClientVisible="False" Width="100%"
                                                TextField="RelName" ID="cmbRelId" DataSourceID="ObjectDataSourceDrpRel"
                                                EnableClientSideAPI="True" ValueType="System.String" ValueField="RelId" Height="22px"
                                                ClientInstanceName="cmbRelIdClient">
                                                <ValidationSettings>
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="نمایندگی" ID="Label20"></asp:Label>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                TextField="Name" ID="cmbAgent" DataSourceID="ObjectDataSourceAgent" EnableClientSideAPI="True"
                                                ValueType="System.Int32" ValueField="AgentId" Height="22px" ClientInstanceName="cmbAgentIdClient">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نمایندگی را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="بخش" ID="Label19"></asp:Label>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                TextField="PartName" ID="cmbPartId" DataSourceID="ObjectDataSourceDrpPart" EnableClientSideAPI="True"
                                                ValueType="System.String" ValueField="PartId" Height="22px" ClientInstanceName="cmbPartIdClient">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="بخش را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="پست های سازمانی" ID="Label23"></asp:Label>
                                        </td>
                                        <td dir="rtl" valign="top" align="right" colspan="5">
                                            <TSPControls:CustomASPXMemo runat="server" Height="28px" ID="txtNcName" EnableClientSideAPI="True"
                                                Width="100%" ReadOnly="True" ClientInstanceName="txtNcNameClient">
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
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="توضیحات" ID="Label21"></asp:Label>
                                        </td>
                                        <td dir="rtl" valign="top" align="right" colspan="5">
                                            <TSPControls:CustomASPXMemo runat="server" Height="28px" ID="txtDescription"
                                                EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtDescriptionClient">
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
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="انتخاب تصویر" EnableClientSideAPI="True"
                                                ID="lblChooseImg" ClientInstanceName="lblChooseImgClient">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="5">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                ID="flpImg" InputType="Images" ClientInstanceName="flpImgClient" OnFileUploadComplete="flpImg_FileUploadComplete">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                        if(e.isValid){
	imgEndUploadImgClient.SetVisible(true);
	flpme.Set('name',1);
	lbl1.SetVisible(false);
	img1.SetVisible(true);
	img1.SetImageUrl('../../image/Employee/Pic/'+e.callbackData);
    }
    else
    {
	imgEndUploadImgClient.SetVisible(false);
    }
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxUploadControl>
                                                            <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                                ID="ASPxLabel1" ForeColor="Red" ClientInstanceName="lbl1">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                ID="imgEndUploadImg" ClientInstanceName="imgEndUploadImgClient">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <TSPControls:CustomAspxButton runat="server" Text="حذف تصویر" CausesValidation="False"
                                                ClientVisible="False" ID="btnDeleteImg" UseSubmitBehavior="False" ClientInstanceName="btnDeleteImgClient"
                                                OnClick="btnDeleteImg_Click">
                                            </TSPControls:CustomAspxButton>
                                            <dxe:ASPxLabel runat="server" ID="lblImg" ForeColor="Blue">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <br />
                                            <asp:Label runat="server" Text="تصویر" ID="lblPic"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <br />
                                            <dxe:ASPxImage runat="server" Height="100px" Width="100px" ID="imgPic" ClientInstanceName="img1"
                                                Border-BorderWidth="1px" Border-BorderStyle="Solid">
                                                <EmptyImage Height="100px" Width="100px" Url="~/Images/Person.png">
                                                </EmptyImage>
                                            </dxe:ASPxImage>
                                        </td>
                                        <td valign="top" align="right"></td>
                                        <td valign="top" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تصویر امضا" EnableClientSideAPI="True"
                                                ID="ASPxLabel2" ClientInstanceName="lblChooseImgClient">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="5">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                ID="flpSign" InputType="Images" ClientInstanceName="flpSignClient" OnFileUploadComplete="flpSign_FileUploadComplete">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                        if(e.isValid){
	imgEndUploadImgClientSign.SetVisible(true);
	
	hp.SetVisible(true);
	hp.SetNavigateUrl('../../image/Employee/Sign/'+e.callbackData);
    }
    else{
	imgEndUploadImgClientSign.SetVisible(false);
    }
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxUploadControl>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                ID="ASPxImage1" ClientInstanceName="imgEndUploadImgClientSign">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <dxe:ASPxHyperLink runat="server" Text="تصویر" ClientVisible="False" Target="_blank"
                                                                ID="HpSign" ClientInstanceName="hp">
                                                            </dxe:ASPxHyperLink>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <br />
                                            <dxe:ASPxLabel runat="server" Text="وضعيت" ID="ASPxLabelStatus" Visible="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <br />
                                            <TSPControls:CustomAspxComboBox runat="server" Visible="False" Width="100%"
                                                ID="CmbStatus" EnableClientSideAPI="True"
                                                ValueType="System.String" Height="22px">
                                                <ValidationSettings>
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <Items>
                                                    <dxe:ListEditItem Value="0" Text="فعال"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Value="1" Text="غير فعال"></dxe:ListEditItem>
                                                </Items>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right"></td>
                                        <td valign="top" align="right"></td>
                                    </tr>

                                </tbody>
                            </table>
                            <fieldset runat="server" id="RoundPanelPcPos">
                                <legend class="fieldset-legend" dir="rtl"><b>مشخصات Pc-Pos</b>
                                </legend>
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="right" width="15%">شماره سریال PC-Posطراح
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtSerialNoPcPos"
                                                    EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtSerialNoPcPos">
                                                    <ValidationSettings>
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right" width="15%">شناسه پذیرنده PC-Posطراح
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtAcceptorIdPcPos"
                                                    EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtAcceptorIdPcPos">
                                                    <ValidationSettings>
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RegularExpression ErrorText="کد باید 15 رقمی باشد" ValidationExpression="\d{15}"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                                <tr>
                                            <td valign="top" align="right" width="15%">پورت com PC-Posطراح
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtComPortPcPos"
                                                    EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtComPortPcPos">
                                                    <ValidationSettings>
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RegularExpression ErrorText=" باید با com شروع شود و طولش 5 کارکتر بیشتر نباشد" ValidationExpression="com\d{1,2}"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right" width="15%">شناسه ترمینال PC-Posطراح
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtTerminalIdPcPos"
                                                    EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtTerminalIdPcPos">
                                                    <ValidationSettings>
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RegularExpression ErrorText="کد باید 8 رقمی باشد" ValidationExpression="\d{8}"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                                <tr>
                                            <td valign="top" align="right" width="15%">شماره سریال PC-Posناظر
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtSerialNoPcPos2"
                                                    EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtSerialNoPcPos2">
                                                    <ValidationSettings>
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right" width="15%">شناسه پذیرنده PC-Posناظر
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                               <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtAcceptorIdPcPos2"
                                                    EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtAcceptorIdPcPos2">
                                                    <ValidationSettings>
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RegularExpression ErrorText="کد باید 15 رقمی باشد" ValidationExpression="\d{15}"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox> 
                                            </td>
                                        </tr>
                                                 <tr>
                                            <td valign="top" align="right" width="15%">پورت com PC-Posناظر
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtComPortPcPos2"
                                                    EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtComPortPcPos2">
                                                    <ValidationSettings>
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RegularExpression ErrorText=" باید با com شروع شود و طولش 5 کارکتر بیشتر نباشد" ValidationExpression="com\d{1,2}"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right" width="15%">شناسه ترمینال PC-Posناظر
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                                 <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Height="22px" ID="txtTerminalIdPcPos2"
                                                    EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtTerminalIdPcPos2">
                                                    <ValidationSettings>
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RegularExpression ErrorText="کد باید 8 رقمی باشد" ValidationExpression="\d{8}"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>           <tr>
                                            <td valign="top" align="right" width="15%">شناسه اموال رایانه
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                                <TSPControls:CustomTextBox MaxLength="30" IsMenuButton="true" runat="server" Height="22px" ID="txtPropertyCodePC"
                                                    EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtPropertyCodePC">
                                                    <ValidationSettings>
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right" width="15%">
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                               
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
                                                CausesValidation="False" ID="btnNew2" EnableViewState="False" EnableTheming="False"
                                                ClientInstanceName="btnNewClient2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                OnClick="btnNew_Click">
                                                <ClientSideEvents Click="function(s, e) {
	//SetNewMode();
}"></ClientSideEvents>

                                                <Image Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                CausesValidation="False" Width="25px" ID="btnEdit2" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnEdit_Click" ClientInstanceName="btnEditClient2" EnableClientSideAPI="True"
                                                UseSubmitBehavior="False">
                                                <ClientSideEvents Click="function(s, e) {
	
}
"></ClientSideEvents>

                                                <Image Url="~/Images/icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                                ID="btnSave2" EnableClientSideAPI="True" EnableTheming="False" ClientInstanceName="btnSaveClient2"
                                                OnClick="btnSave_Click" EnableViewState="False" UseSubmitBehavior="False">

                                                <Image Url="~/Images/icons/save.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {

if(CheckCharacterEncoding(txtFirstNameEnClient.GetText())==false)
 {
txtFirstNameEnClient.SetIsValid(false);
txtFirstNameEnClient.SetErrorText('حروف وارد شده نامعتبر است');
	e.processOnServer=false;
}
if(CheckCharacterEncoding(txtLastNameEnClient.GetText())==false)
 {
txtLastNameEnClient.SetIsValid(false);
txtLastNameEnClient.SetErrorText('حروف وارد شده نامعتبر است');
	e.processOnServer=false;
}


if(flpme.Get('name')!=1)
{
lbl1.SetVisible(true);

e.processOnServer=false;
}
}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                CausesValidation="False" ID="btnBack2" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnBack_Click" ClientInstanceName="btnBackClient2" EnableClientSideAPI="True"
                                                UseSubmitBehavior="False">

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
                <asp:ObjectDataSource ID="ObjectDataSourceDrpSex" runat="server" InsertMethod="Insert"
                    SelectMethod="GetData" TypeName="TSP.DataManager.SexManager" CacheDuration="30">
                    <InsertParameters>
                        <asp:Parameter Name="SexName" Type="String" />
                        <asp:Parameter Name="ModifiedDate" Type="DateTime" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceDrpMar" runat="server" InsertMethod="Insert"
                    SelectMethod="GetData" TypeName="TSP.DataManager.MaritalStatusManager" CacheDuration="30"
                    DeleteMethod="Delete" UpdateMethod="Update" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceDrpPart" runat="server" InsertMethod="Insert"
                    SelectMethod="GetData" TypeName="TSP.DataManager.PartitionManager" CacheDuration="30"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceDrpRel" runat="server" InsertMethod="Insert"
                    SelectMethod="GetData" TypeName="TSP.DataManager.ReligionManager" CacheDuration="30"
                    DeleteMethod="Delete" UpdateMethod="Update"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceAgent" runat="server" SelectMethod="GetData"
                    TypeName="TSP.DataManager.AccountingAgentManager" CacheDuration="30"></asp:ObjectDataSource>
                <dxhf:ASPxHiddenField ID="HDFlpMember" runat="server" ClientInstanceName="flpme">
                </dxhf:ASPxHiddenField>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
            DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
</asp:Content>
