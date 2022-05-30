<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="EngOfficeMemberInsert.aspx.cs" Inherits="Employee_Document_EngOffice_EngOfficeMemberInsert"
    Title="مشخصات عضو" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div align="right" dir="rtl" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]<br />
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
                                                ClientInstanceName="save" OnClick="btnSave_Click">
                                                <ClientSideEvents Click="function(s, e) {

var flag=0;

if (chb.GetChecked()== true)
{
if(HDEmza.Get('name')!=1)
{
lblEmza.SetVisible(true);
e.processOnServer=false;
flag=1;
}
}
else
{
lblEmza.SetVisible(false);
}
}"></ClientSideEvents>

                                                <Image Url="~/Images/icons/save.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
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
                <br />
                <ul id="lblNote" runat="server" class="HelpUL">
                    <li>ثبت مدیر مسئول دفتر الزامی می باشد  </li>
                </ul>
                <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


                            <table dir="rtl" id="Table2" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="کد عضویت *" ID="lblMeNo" ClientInstanceName="lMeNo">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" AutoPostBack="True"
                                                ID="txtMeNo" ClientInstanceName="MeNo"
                                                OnTextChanged="txtMeNo_TextChanged">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="کد را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right" style="width: 15%"></td>
                                        <td valign="top" align="right" style="width: 35%"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="نام" ID="Label4" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ClientEnabled="False"
                                                ID="txtFirstName" ClientInstanceName="FirstName">
                                                <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="نام را وارد نمایید"
                                                    ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText=""></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents Validation="function(s, e) {
		if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="نام خانوادگی" ID="Label5" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ClientEnabled="False"
                                                ID="txtLastName" ClientInstanceName="LastName">
                                                <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="نام خانوادگی را وارد نمایید"
                                                    ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText=""></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents Validation="function(s, e) {
		if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="نام پدر" ID="Label11" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ClientEnabled="False"
                                                ID="txtFatherName" ClientInstanceName="FatherName">
                                                <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="نام پدر را وارد نمایید"
                                                    ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText=""></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents Validation="function(s, e) {
	if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right"></td>
                                        <td valign="top" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره پروانه اشتغال" Width="100%" ID="Label22"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ClientEnabled="False"
                                                ID="txtFileNo" Style="direction: ltr" ClientInstanceName="FileNo">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RegularExpression ErrorText=""></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="تاریخ اعتبار پروانه" Width="100%" ID="Label1"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ClientEnabled="False"
                                                ID="txtFileNoDate" ClientInstanceName="FileNoDate">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RegularExpression ErrorText=""></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="تاریخ تولد" ID="Label12" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="300px" Enabled="False"
                                                ShowPickerOnTop="True" ID="txtBirthDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="محل تولد" ID="Label13"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ClientEnabled="False"
                                                ID="txtBirthPlace" ClientInstanceName="BirthPlace">
                                                <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText=""
                                                    ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText=""></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents Validation="function(s, e) {
	if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره شناسنامه" ID="Label14" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" MaxLength="10" ClientEnabled="False"
                                                ID="txtIdNo" ClientInstanceName="IdNo">
                                                <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="شماره شناسنامه را وارد نمایید"
                                                    ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText=""></RequiredField>
                                                    <RegularExpression ErrorText=""></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents Validation="function(s, e) {
	if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="کد ملی" ID="Label15" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" MaxLength="10" AutoPostBack="True"
                                                ClientEnabled="False" ID="txtSSN" ClientInstanceName="SSN">
                                                <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="کد ملی را وارد نمایید"
                                                    ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText=""></RequiredField>
                                                    <RegularExpression ErrorText=""></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents Validation="function(s, e) {
	if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="تلفن" ID="Lhe69" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td style="vertical-align: top" width="70%">
                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" MaxLength="8" ClientEnabled="False"
                                                                ID="txtTel" ClientInstanceName="Tel">
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <RequiredField ErrorText=""></RequiredField>
                                                                    <RegularExpression ErrorText=""></RegularExpression>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                        <td style="vertical-align: top" width="5%">
                                                            <asp:Label runat="server" Text="-" ID="Lbjjje71" Width="100%"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: top" width="25%">
                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" MaxLength="4" ClientEnabled="False"
                                                                ID="txtTel_pre" ClientInstanceName="Tel_pre">
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <RequiredField ErrorText=""></RequiredField>
                                                                    <RegularExpression ErrorText=""></RegularExpression>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره همراه" ID="Lal75"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" MaxLength="11" ClientEnabled="False"
                                                ID="txtMobile" ClientInstanceName="MobileNo">
                                                <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="شماره همراه را وارد نمایید"
                                                    ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText=""></RequiredField>
                                                    <RegularExpression ErrorText=""></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents Validation="function(s, e) {
	if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="آدرس" ID="Lbه76" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%" ClientEnabled="False"
                                                ID="txtAddress" ClientInstanceName="Address">
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
                                        <td valign="top" align="right" style="width: 15%">
                                            <asp:Label runat="server" Text="تصویر" ID="Label17" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" colspan="3" style="width: 85%">
                                            <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgMember" ClientInstanceName="img">
                                                <EmptyImage Url="~/Images/person.png">
                                                </EmptyImage>
                                            </dxe:ASPxImage>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="تصویر امضا" ID="Label2"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgSign" ClientInstanceName="imgs">
                                                <EmptyImage Url="~/Images/noimage.gif">
                                                </EmptyImage>
                                            </dxe:ASPxImage>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="رشته" ID="ASPxLabel5" ClientInstanceName="lblMjId"
                                                Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="rtl" valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                TextField="MjName" ID="ComboMjId"
                                                ClientEnabled="False" DataSourceID="OdbMajor" ValueType="System.String" ValueField="MjId"
                                                ClientInstanceName="MjId" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="مدرک" ID="ASPxLabel6" ClientInstanceName="lblMjName"
                                                Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ClientEnabled="False"
                                                ID="txtMjName" ClientInstanceName="MjName">
                                                <ClientSideEvents Validation="function(s, e) {
		if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="سمت *" ID="Label32" Width="100%"></asp:Label>
                                        </td>
                                        <td dir="rtl" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                TextField="OfpName" ID="drdPosition"
                                                DataSourceID="ODBPosition" ValueType="System.String" ValueField="OfpId" ClientInstanceName="position"
                                                RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                    <RequiredField IsRequired="True" ErrorText="سمت را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right"></td>
                                        <td dir="ltr" valign="top" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="نوع همکاری" ID="Label53" Width="100%"></asp:Label>
                                        </td>
                                        <td dir="rtl" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server"
                                                ID="ComboTime" ValueType="System.String"
                                                SelectedIndex="1" ClientInstanceName="time"
                                                RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <Items>
                                                    <dxe:ListEditItem Value="0" Text="پاره وقت"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Value="1" Text="تمام وقت"></dxe:ListEditItem>
                                                </Items>
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                    <RequiredField IsRequired="True" ErrorText="نوع همکاری را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="تاریخ شروع همکاری" ID="Label42" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="300px" ShowPickerOnTop="True"
                                                ID="txtStartDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" colspan="4">
                                            <TSPControls:CustomASPxCheckBox runat="server" Text="امکان عضویت در دفتر گاز دارد. توجه به موجب اعلام این درخواست 25 درصد از ظرفیت نظارت فرد پیشاپیش کاسته می شود تا بتواند به عضویت شرکت دیگری با زمینه فعالیت نظارت گاز درآید "
                                                EnableClientSideAPI="True" Width="100%" ID="chbHasGasCert" ClientInstanceName="chbHasGasCert">
                                            </TSPControls:CustomASPxCheckBox>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" colspan="4">
                                            <TSPControls:CustomASPxCheckBox runat="server" Text="دارای حق امضا" EnableClientSideAPI="True"
                                                Width="100%" ID="chbHaghEmza" ClientInstanceName="chb">
                                                <ClientSideEvents CheckedChanged="function(s, e) {
	//if(chb.GetChecked()==true)
//{
	//flpEmza.SetVisible(true);
	//lblE.SetVisible(true);
//}
//else
//{
//flpEmza.SetVisible(false);
//lblE.SetVisible(false);
//}
}"></ClientSideEvents>
                                            </TSPControls:CustomASPxCheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تصویر امضا" ID="lbEmza" ClientInstanceName="lblE"
                                                Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxUploadControl runat="server" ID="flp_Emza" InputType="Images"
                                                                UploadWhenFileChoosed="true" ClientInstanceName="flpEmza" OnFileUploadComplete="flp_Emza_FileUploadComplete">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEmzatik.SetVisible(true);
	HDEmza.Set('name',1);
	lblEmza.SetVisible(false);
	imgEmza.SetVisible(true);
	imgEmza.SetImageUrl('../../../Image/Temp/'+e.callbackData);
	HDEmza.Set('Url','../../../Image/Temp/'+e.callbackData);
    }
    else
    {
    imgEmzatik.SetVisible(false);
	HDEmza.Set('name',0);
	lblEmza.SetVisible(true);
	imgEmza.SetVisible(false);
	imgEmza.SetImageUrl('');
    }
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxUploadControl>
                                                            <dxe:ASPxLabel runat="server" Text="تصویر امضا را انتخاب نمایید" ClientVisible="False"
                                                                ID="ASPxLabel166" ForeColor="Red" ClientInstanceName="lblEmza">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                ID="imgEndUploadImg123" ClientInstanceName="imgEmzatik">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <dxe:ASPxImage runat="server" Height="75px" ClientVisible="False" Width="75px" ID="imgEmza"
                                                ClientInstanceName="imgEmza">
                                            </dxe:ASPxImage>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="click" CausesValidation="False" ClientVisible="False"
                                                Width="62px" ID="ASPxButton3" EnableClientSideAPI="True" AutoPostBack="False"
                                                UseSubmitBehavior="False" ClientInstanceName="but">
                                                <ClientSideEvents Click="function(s, e) {
flpEmza.SetVisible(false);
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                    <tr id="Tr6" runat="server">
                                        <td align="right" valign="top">تصویر خوداظهاری کاری
                                        </td>
                                        <td colspan="3" align="right" valign="top">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxUploadControl runat="server" InputType="Files" UploadWhenFileChoosed="True"
                                                                ClientInstanceName="FileUploadImageSelfreported" ID="CustomAspxUploadControl1" OnFileUploadComplete="FileUploadSelfreported_FileUploadComplete">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClientSelfreported.SetVisible(true);
    HDEmza.Set('SelfreportedImage',1);
	lblImageWarningSelfreported.SetVisible(false);
	ImageSelfreported.SetVisible(true);
	ImageSelfreported.SetNavigateUrl('../../../image/EngOffice/Selfreported/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientSelfreported.SetVisible(false);
    HDEmza.Set('SelfreportedImage',0);
	lblImageWarningSelfreported.SetVisible(true);
	ImageSelfreported.SetVisible(false);
	ImageSelfreported.SetNavigateUrl('');
	}
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxUploadControl>
                                                            <dxe:ASPxLabel runat="server" Text="خوداظهاری کاری را انتخاب نمایید" ClientInstanceName="lblImageWarningSelfreported"
                                                                ClientVisible="False" ForeColor="Red" ID="ASPxLabel14">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxImage runat="server" ImageUrl="~/Images/icons/button_ok.png" ToolTip="خوداظهاری کاری انتخاب شد"
                                                                ClientInstanceName="imgEndUploadImgClientSelfreported" ClientVisible="False" ID="ASPxImage3">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <div id="div3" onclick="GetImage(ImageSelfreported)">
                                                <dxe:ASPxHyperLink runat="server" Text="خوداظهاری کاری" Width="150px"
                                                    ID="ImageSelfreported" ClientInstanceName="ImageSelfreported" Target="_blank">
                                                </dxe:ASPxHyperLink>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="توضیحات" ID="Label45" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%" ID="txtDesc"
                                                ClientInstanceName="Desc">
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
                            <fieldset>
                                <legend class="HelpUL">صلاحیت ها</legend>
                                <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                                    ID="GridViewMemberDocRes" KeyFieldName="MfdId" AutoGenerateColumns="False">

                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GrdName" Caption="پایه">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="ResName" Caption="صلاحیت">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjName" Caption="رشته">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                </TSPControls:CustomAspxDevGridView2>
                            </fieldset>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />

                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>



                            <table align="right">
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
                                                ID="btnSave1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                ClientInstanceName="save1" OnClick="btnSave_Click">
                                                <ClientSideEvents Click="function(s, e) {
		var flag=0;

if (chb.GetChecked()== true)
{
if(HDEmza.Get('name')!=1)
{
lblEmza.SetVisible(true);
e.processOnServer=false;
flag=1;
}
}
else
{
lblEmza.SetVisible(false);
}

}"></ClientSideEvents>

                                                <Image Url="~/Images/icons/save.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
            DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img align="middle" src="../../../Image/indicator.gif" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
        <asp:HiddenField ID="EngOfficeId" runat="server" Visible="False" />
        <asp:HiddenField ID="OfPersonId" runat="server" Visible="False" />
        <asp:HiddenField ID="OffMemberId" runat="server" Visible="False" />
        <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
        <asp:HiddenField ID="EngFileId" runat="server" Visible="False" />
        <asp:HiddenField ID="EngFileIdOrigin" runat="server" Visible="False" />
        <asp:HiddenField ID="HDMode" runat="server" Visible="False" />
        <asp:ObjectDataSource ID="ODBPosition" runat="server" FilterExpression="OfType={0}"
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="TSP.DataManager.OfficePositionManager">
            <FilterParameters>
                <asp:Parameter Name="newparameter" />
            </FilterParameters>
        </asp:ObjectDataSource>
        <dxhf:ASPxHiddenField ID="HD_Emza" runat="server" ClientInstanceName="HDEmza">
        </dxhf:ASPxHiddenField>
        <asp:ObjectDataSource ID="OdbGrade" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="OdbResponsibility" runat="server" SelectMethod="GetData"
            TypeName="TSP.DataManager.ResponcibilityTypeManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="OdbMajor" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>

</asp:Content>
