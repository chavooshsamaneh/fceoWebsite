<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="EngOfficeMemberShow.aspx.cs" Inherits="Settlement_EngOfficeDocument_EngOfficeMemberShow"
    Title="مشخصات عضو" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" WorkDayCSS="PickerWorkDayCSS"
        WeekDayCSS="PickerWeekDayCSS" SelectedCSS="PickerSelectedCSS" HeaderCSS="PickerHeaderCSS"
        FrameCSS="PickerCSS" ForbidenCSS="PickerForbidenCSS" FooterCSS="PickerFooterCSS"
        CalendarDayWidth="50" CalendarCSS="PickerCalendarCSS">
    </pdc:PersianDateScriptManager>
            <div align="right" dir="rtl" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]<br />
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table  >
                            <tbody>
                                <tr>
                                    <td align="right">
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
            <TSPControls:CustomASPxRoundPanel Enabled="false" ID="RoundPanelPage" HeaderText="مشخصات عضو" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table dir="rtl" width="100%">
                            <tr>
                                <td valign="top" align="right" width="15%">
                                    <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="lblMeNo" ClientInstanceName="lMeNo">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="top" align="right" width="30%">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" AutoPostBack="True"
                                        ID="txtMeNo" ClientInstanceName="MeNo"
                                        ReadOnly="True">
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
                                <td valign="top" align="right" width="10%"></td>
                                <td valign="top" align="right" width="15%"></td>
                                <td valign="top" align="right" width="30%"></td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    <asp:Label runat="server" Text="نام" ID="Label4"></asp:Label>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtFirstName"
                                        ClientInstanceName="FirstName"
                                        ReadOnly="True">
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
                                <td></td>
                                <td valign="top" align="right">
                                    <asp:Label runat="server" Text="نام خانوادگی" ID="Label5"></asp:Label>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtLastName"
                                        ClientInstanceName="LastName"
                                        ReadOnly="True">
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
                                    <asp:Label runat="server" Text="نام پدر" ID="Label11"></asp:Label>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtFatherName"
                                        ClientInstanceName="FatherName"
                                        ReadOnly="True">
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
                                <td></td>
                                <td valign="top" align="right"></td>
                                <td valign="top" align="right"></td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <asp:Label runat="server" Text="شماره پروانه اشتغال" ID="Label22"></asp:Label>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtFileNo" Style="direction: ltr"
                                        ClientInstanceName="FileNo" ReadOnly="True">
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
                                <td></td>
                                <td align="right" valign="top">
                                    <asp:Label ID="Label1" runat="server" Text="تاریخ اعتبار پروانه"></asp:Label>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtFileNoDate"
                                        ClientInstanceName="FileNoDate"
                                        ReadOnly="True">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                            <RegularExpression ErrorText="" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    <asp:Label runat="server" Text="تاریخ تولد" ID="Label12"></asp:Label>
                                </td>
                                <td valign="top" align="right">
                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="210px" ShowPickerOnTop="True"
                                        ID="txtBirthDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" ReadOnly="True"></pdc:PersianDateTextBox>
                                </td>
                                <td></td>
                                <td valign="top" align="right">
                                    <asp:Label runat="server" Text="محل تولد" ID="Label13"></asp:Label>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtBirthPlace"
                                        ClientInstanceName="BirthPlace"
                                        ReadOnly="True">
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
                                    <asp:Label runat="server" Text="شماره شناسنامه" ID="Label14"></asp:Label>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="10" ID="txtIdNo"
                                        ClientInstanceName="IdNo" ReadOnly="True">
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
                                <td></td>
                                <td valign="top" align="right">
                                    <asp:Label runat="server" Text="کد ملی" ID="Label15"></asp:Label>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="10" AutoPostBack="True"
                                        ID="txtSSN" ClientInstanceName="SSN"
                                        ReadOnly="True">
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
                                    <asp:Label runat="server" Text="تلفن" ID="Lhe69"></asp:Label>
                                </td>
                                <td valign="top" align="right">
                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: top" width="70%">
                                                    <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="8" ID="txtTel"
                                                        ClientInstanceName="Tel" ReadOnly="True">
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
                                                    <asp:Label runat="server" Text="-" Width="100%" ID="Lbjjje71"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top" width="25%">
                                                    <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="4" ID="txtTel_pre"
                                                        ClientInstanceName="Tel_pre"
                                                        ReadOnly="True">
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
                                <td></td>
                                <td valign="top" align="right">
                                    <asp:Label runat="server" Text="شماره همراه" ID="Lal75"></asp:Label>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="11" ID="txtMobile"
                                        ClientInstanceName="MobileNo"
                                        ReadOnly="True">
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
                                    <asp:Label runat="server" Text="آدرس" ID="Lbه76"></asp:Label>
                                </td>
                                <td valign="top" align="right" colspan="4">
                                    <TSPControls:CustomASPXMemo runat="server" Height="40px" Width="100%" ID="txtAddress"
                                        ClientInstanceName="Address"
                                        ReadOnly="True">
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
                                    <asp:Label runat="server" Text="تصویر" ID="Label17"></asp:Label>
                                </td>
                                <td valign="top" align="right" colspan="4">
                                    <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgMember" ClientInstanceName="img">
                                        <EmptyImage Url="~/Images/person.gif" />
                                    </dxe:ASPxImage>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    <asp:Label ID="Label2" runat="server" Text="تصویر امضا"></asp:Label>
                                </td>
                                <td valign="top" align="right" colspan="4">
                                    <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgSign" ClientInstanceName="imgs">
                                        <EmptyImage Url="~/Images/noimage.gif" />
                                    </dxe:ASPxImage>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    <TSPControls:CustomASPxCheckBox runat="server" Text="دارای حق امضا" EnableClientSideAPI="True"
                                        Width="105px" ID="chbHaghEmza" ClientInstanceName="chb" ReadOnly="True">
                                        <ClientSideEvents CheckedChanged="function(s, e) {
	if(chb.GetChecked()==true)
{
	flpEmza.SetVisible(true);
	lblE.SetVisible(true);
}
else
{
flpEmza.SetVisible(false);
lblE.SetVisible(false);
}
}"></ClientSideEvents>
                                    </TSPControls:CustomASPxCheckBox>
                                </td>
                                <td valign="top" align="right"></td>
                                <td valign="top" align="right"></td>
                                <td valign="top" align="right"></td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="تصویر امضا" ClientVisible="False" ID="lbEmza"
                                        ClientInstanceName="lblE">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="top" align="right" colspan="4">
                                    <dxe:ASPxImage runat="server" Height="75px" ClientVisible="False" Width="75px" ID="imgEmza"
                                        ClientInstanceName="imgEmza">
                                        <EmptyImage Url="~/Images/noimage.gif" />
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="رشته" ID="ASPxLabel5" ClientInstanceName="lblMjId">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%" 
                                        RightToLeft="True" TextField="MjName" ID="ComboMjId"
                                        DataSourceID="OdbMajor" ValueType="System.String" ValueField="MjId"
                                        ClientInstanceName="MjId" ReadOnly="True">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RequiredField ErrorText="رشته را انتخاب نمایید"></RequiredField>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                    </TSPControls:CustomAspxComboBox>
                                </td>
                                <td></td>
                                <td valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="مدرک" ID="ASPxLabel6" ClientInstanceName="lblMjName">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtMjName" ClientInstanceName="MjName"
                                        ReadOnly="True">
                                        <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
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
                                    <asp:Label runat="server" Text="سمت" ID="Label32"></asp:Label>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                        RightToLeft="True" TextField="OfpName"
                                        ID="drdPosition" DataSourceID="ODBPosition" ValueType="System.String"
                                        ValueField="OfpId" ClientInstanceName="position"
                                        ReadOnly="True">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RequiredField ErrorText=""></RequiredField>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                    </TSPControls:CustomAspxComboBox>
                                </td>
                                <td></td>
                                <td valign="top" align="right"></td>
                                <td dir="ltr" valign="top" align="right"></td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    <asp:Label runat="server" Text="نوع همکاری" ID="Label53"></asp:Label>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%" 
                                        RightToLeft="True" ID="ComboTime"
                                        ValueType="System.String" SelectedIndex="1" ClientInstanceName="time"
                                        ReadOnly="True">
                                        <Items>
                                            <dxe:ListEditItem Value="0" Text="پاره وقت"></dxe:ListEditItem>
                                            <dxe:ListEditItem Value="1" Text="تمام وقت"></dxe:ListEditItem>
                                        </Items>
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RequiredField ErrorText=""></RequiredField>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                    </TSPControls:CustomAspxComboBox>
                                </td>
                                <td></td>
                                <td valign="top" align="right">
                                    <asp:Label runat="server" Text="تاریخ شروع همکاری" ID="Label42"></asp:Label>
                                </td>
                                <td valign="top" align="right">
                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="200px" ShowPickerOnTop="True"
                                        ID="txtStartDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" ReadOnly="True"></pdc:PersianDateTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    <asp:Label runat="server" Text="توضیحات" ID="Label45"></asp:Label>
                                </td>
                                <td valign="top" align="right" colspan="4">
                                    <TSPControls:CustomASPXMemo runat="server" Height="40px" Width="100%" ID="txtDesc"
                                        ClientInstanceName="Desc" ReadOnly="True">
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
                        </table>
                        <fieldset>
                            <legend class="HelpUL">صلاحیت ها</legend>
                            <TSPControls:CustomAspxDevGridView runat="server" ID="CustomAspxDevGridView1"
                                RightToLeft="True" KeyFieldName="MfdId" Width="100%" AutoGenerateColumns="False">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GrdName" Caption="پایه">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="ResName" Caption="صلاحیت">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjName" Caption="رشته">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView>
                        </fieldset>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td align="right">
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
            <asp:HiddenField ID="EngOfficeId" runat="server" Visible="False" />
            <asp:HiddenField ID="OffMemberId" runat="server" Visible="False" />
            <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
            <asp:ObjectDataSource ID="ODBPosition" runat="server" FilterExpression="OfType={0}"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="TSP.DataManager.OfficePositionManager">
                <FilterParameters>
                    <asp:Parameter Name="newparameter" />
                </FilterParameters>
            </asp:ObjectDataSource>
            <asp:HiddenField ID="EngFileId" runat="server" Visible="False" />
            <asp:ObjectDataSource ID="OdbGrade" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbResponsibility" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.ResponcibilityTypeManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbMajor" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
</asp:Content>
