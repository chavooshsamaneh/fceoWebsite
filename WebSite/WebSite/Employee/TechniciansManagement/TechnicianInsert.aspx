<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="TechnicianInsert.aspx.cs" Inherits="Employee_TechniciansManagement_TechnicianInsert"
    Title="مشخصات کاردان/معمار تجربی" %>

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
    <script language="javascript">
        function SetCityControlValues() {
            gridCity.GetRowValues(gridCity.GetFocusedRowIndex(), 'CitName;CitId;CitCode;AgentName;AgentCode;AgentAddress', SetCityValue);
        }

        function SetCityValue(values) {
            txtCity.SetText(values[0]);
            HiddenFieldTn.Set('CitId', values[1]);
            HiddenFieldTn.Set('CitCode', values[2]);
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" WorkDayCSS="PickerWorkDayCSS"
        WeekDayCSS="PickerWeekDayCSS" SelectedCSS="PickerSelectedCSS" HeaderCSS="PickerHeaderCSS"
        FrameCSS="PickerCSS" ForbidenCSS="PickerForbidenCSS" FooterCSS="PickerFooterCSS"
        CalendarDayWidth="50" CalendarCSS="PickerCalendarCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#"><span style="color: #000000">بس</span>تن</a>]<br />
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
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
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
                                            ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            ClientInstanceName="save" OnClick="btnSave_Click">

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
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشخصات فردی" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table dir="rtl" id="Table2" width="100%">
                            <tbody>
                                <tr>
                                    <td valign="top" align="center" colspan="4">
                                        <dxe:ASPxLabel runat="server" Text="وضعیت پرونده: نامشخص" ID="lblWfState" ForeColor="Red">
                                        </dxe:ASPxLabel>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right" width="20%">
                                        <dxe:ASPxLabel runat="server" Text="نوع عضو*" ID="ASPxLabel1">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="30%">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            ID="ComboType" ValueType="System.String"
                                            SelectedIndex="0" ClientInstanceName="cmbType"
                                            EnableIncrementalFiltering="True" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(cmbType.GetValue()=='3')
	{
		
		MjId.SetEnabled(false);
        MjId.SetValue('3');
        txtOtpCode.SetVisible(false);
        lblOtpCode.SetVisible(false);
     
        ComboResp.SetSelectedIndex(1);
   
        
	}
	else
	{

		MjId.SetValue('');
		MjId.SetEnabled(true);
        txtOtpCode.SetVisible(true);
        lblOtpCode.SetVisible(true);
        ComboResp.SetValue('');
        ComboResp.SetVisible(true);
        lblResp.SetVisible(true);

	}
    FileNo.SetText('');
    FileNo.SetIsValid(true);
}"></ClientSideEvents>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="نوع عضو را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <Items>
                                                <dxe:ListEditItem Value="1" Text="کاردان" Selected="True"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="3" Text="معمار تجربی"></dxe:ListEditItem>
                                            </Items>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right" width="20%">
                                        <dxe:ASPxLabel runat="server" Text="کد کانون کاردان ها*" ID="lblOtpCode" ClientInstanceName="lblOtpCode">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="30%">
                                        <TSPControls:CustomTextBox runat="server" ID="txtOtpCode" ClientInstanceName="txtOtpCode"
                                            Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="کد کانون را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="کد وارد شده صحیح نیست" ValidationExpression="\d*"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ClientSideEvents Validation="function(s, e) {
	
}"></ClientSideEvents>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="نام*" ID="Label4"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtFirstName" Width="100%"
                                            ClientInstanceName="FirstName">
                                            <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ClientSideEvents Validation="function(s, e) {
	
}"></ClientSideEvents>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="نام خانوادگی*" ID="Label5"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtLastName" Width="100%"
                                            ClientInstanceName="LastName">
                                            <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="نام خانوادگی را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ClientSideEvents Validation="function(s, e) {
		
}"></ClientSideEvents>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="نام پدر" ID="Label11"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtFatherName" Width="100%"
                                            ClientInstanceName="FatherName">
                                            <ValidationSettings Display="Dynamic" ErrorText="نام پدر را وارد نمایید" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField ErrorText=""></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ClientSideEvents Validation="function(s, e) {
	
}"></ClientSideEvents>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right"></td>
                                    <td valign="top" align="right"></td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="تاریخ تولد*" ID="Label12"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                            Width="230px" ShowPickerOnTop="True" ID="txtBirthDate" PickerDirection="ToRight"
                                            IconUrl="~/Image/Calendar.gif" Style="direction: ltr" RightToLeft="False"></pdc:PersianDateTextBox>
                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                            ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtBirthDate" ID="PersianDateValidator1">تاریخ نامعتبر</pdc:PersianDateValidator>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="محل تولد" ID="Label13"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtBirthPlace" Width="100%"
                                            ClientInstanceName="BirthPlace">
                                            <ValidationSettings Display="Dynamic" ErrorText="محل تولد را وارد نمایید" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField ErrorText=""></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ClientSideEvents Validation="function(s, e) {

}"></ClientSideEvents>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="شماره شناسنامه*" ID="Label14"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtIdNo" Width="100%" MaxLength="10"
                                            ClientInstanceName="IdNo">
                                            <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="شماره شناسنامه را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,10}"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ClientSideEvents Validation="function(s, e) {
	
}"></ClientSideEvents>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="کد ملی*" ID="Label15"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtSSN" Width="100%" MaxLength="10"
                                            ClientInstanceName="SSN">
                                            <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="کد ملی را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="\d{10}"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ClientSideEvents Validation="function(s, e) {
	
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
                                                    <td valign="top" width="70%">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtTel" Width="100%" MaxLength="8"
                                                            ClientInstanceName="Tel">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField ErrorText=""></RequiredField>
                                                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{5,8}"></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="center" width="10%">
                                                        <asp:Label runat="server" Text="-" Width="13px" ID="Lbjjje71"></asp:Label>
                                                    </td>
                                                    <td valign="top" width="20%">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtTel_pre" Width="100%" MaxLength="4"
                                                            ClientInstanceName="Tel_pre">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField ErrorText=""></RequiredField>
                                                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}"></RegularExpression>
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
                                        <asp:Label runat="server" Text="شماره همراه*" ID="Lal75"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtMobile" Width="100%" MaxLength="11"
                                            ClientInstanceName="MobileNo">
                                            <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="شماره همراه را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{10}"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ClientSideEvents Validation="function(s, e) {
	
}"></ClientSideEvents>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="نمایندگی*" ID="Label2"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            TextField="Name" ID="CmbAgent" DataSourceID="ObjectDataSourceAgent" ValueType="System.String"
                                            ValueField="AgentId" RightToLeft="True" OnSelectedIndexChanged="CmbAgent_SelectedIndexChanged" AutoPostBack="true"
                                            EnableIncrementalFiltering="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">                                        
                                                <RequiredField IsRequired="True" ErrorText="نمایندگی را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="شهر محل اقامت*" ID="Label3"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%" AutoPostBack="true"
                                            TextField="CitName" ID="CmbCity" DataSourceID="ObjectDataSourceCity" ValueType="System.String"
                                            ValueField="CitId" ClientInstanceName="CmbCity" RightToLeft="True"
                                            EnableIncrementalFiltering="True" OnSelectedIndexChanged="CmbCity_SelectedIndexChanged">
                                            <ItemStyle HorizontalAlign="Right" />

                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="شهر را انتخاب نمایید"></RequiredField>
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
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="شماره حساب*" ID="Label6"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtBankAccNo" Width="100%"
                                            ClientInstanceName="txtBankAccNo">
                                            <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="شماره حساب را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="بانک عامل*" ID="Label7"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">

                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            ID="cmbBank" ValueType="System.String"
                                            ValueField="Bank" ClientInstanceName="CmbBank" RightToLeft="True"
                                            EnableIncrementalFiltering="True">
                                            <Items>
                                                <%-- //تجارت- ملت- مسکن- کشاورزی- صادرات- ملی- رفاه- سپه- پارسیان- اقتصاد نوین- توسعه صادرات- شهر--%>
                                                <dxe:ListEditItem Text="------" Value="-1" Selected="True" />
                                                <dxe:ListEditItem Text="تجارت" Value="1" />
                                                <dxe:ListEditItem Text="ملت" Value="2" />
                                                <dxe:ListEditItem Text="مسکن" Value="3" />
                                                <dxe:ListEditItem Text="کشاورزی" Value="4" />
                                                <dxe:ListEditItem Text="صادرات" Value="5" />
                                                <dxe:ListEditItem Text="ملی" Value="6" />
                                                <dxe:ListEditItem Text="رفاه" Value="7" />
                                                <dxe:ListEditItem Text="سپه" Value="8" />
                                                <dxe:ListEditItem Text="پارسیان" Value="9" />
                                                <dxe:ListEditItem Text="اقتصاد نوین" Value="10" />
                                                <dxe:ListEditItem Text="توسعه صادرات" Value="11" />
                                                <dxe:ListEditItem Text="شهر" Value="12" />
                                            </Items>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="بانک عامل را انتخاب کنید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="آدرس" ID="Lbه76"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtAddress" Width="100%"
                                            ClientInstanceName="Address">
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
                                    <td valign="top" align="right" colspan="3">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" ID="flp_Image" InputType="Images"
                                                            UploadWhenFileChoosed="true" Width="100%" ClientInstanceName="flpimg" OnFileUploadComplete="flp_Image_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                        if(e.isValid){
    imgtik.SetVisible(true);
    HD.Set('PesonImg',1);
	lblPersonImageValidation.SetVisible(false);
    img.SetImageUrl('../../Image/Temp/'+e.callbackData);
	}
	else{
    imgtik.SetVisible(false);
    HD.Set('PesonImg',0);
	lblPersonImageValidation.SetVisible(true);
    img.SetImageUrl('');
	}
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                            ID="lblPersonImageValidation" ForeColor="Red" ClientInstanceName="lblPersonImageValidation">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="imgtik" ClientInstanceName="imgtik">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgMember" ClientInstanceName="img">
                                            <EmptyImage Url="~/Images/Person.png">
                                            </EmptyImage>
                                        </dxe:ASPxImage>
                                        <br />
                                        <dxe:ASPxLabel runat="server" ID="lblImg" ForeColor="Blue">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <ul class="HelpUL">
                                            <li>در صورت عضویت شخص در شرکت فیلد عنوان رشته در پشت گواهینامه شرکت چاپ می گردد
                                            </li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="رشته*" ID="ASPxLabel5" ClientInstanceName="lblMjId">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            TextField="MjName" ID="ComboMjId" DataSourceID="OdbMajor"
                                            ValueType="System.String" ValueField="MjId" ClientInstanceName="MjId"
                                            EnableIncrementalFiltering="True" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="رشته را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="عنوان رشته*" ID="ASPxLabel6" ClientInstanceName="lblMjName">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtMjName" Width="100%" ClientInstanceName="MjName">
                                            <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="مدرک را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ClientSideEvents Validation="function(s, e) {
		
}"></ClientSideEvents>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="شماره پروانه اشتغال*" ID="Label22"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtFileNo" Width="100%" ClientInstanceName="FileNo">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="شماره پروانه را وارد نمایید"></RequiredField>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ClientSideEvents TextChanged="function(s, e) {
       var fiNo = FileNo.GetText();
     	if(cmbType.GetValue()=='3')
	{
        var p = /\d{1}-\d{2}-\d{3,5}/i;
        var path=new RegExp(p);
        if(!path.test(fiNo))
        {
           FileNo.SetIsValid(false);
           FileNo.SetErrorText('شماره را با فرمت *****-**-* وارد نمایید'); 
        }
    }
    else
    {
        var p = /\d{1}-\d{2}-\d{3}-\d{3,5}/i;
        var path=new RegExp(p);
        if(!path.test(fiNo))
        {
           FileNo.SetIsValid(false);
           FileNo.SetErrorText('شماره را با فرمت *****-***-**-* وارد نمایید'); 
        }
    }
		
}"></ClientSideEvents>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="تاریخ اعتبار پروانه*" ID="Label1"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                            Width="230px" ShowPickerOnTop="True" ID="txtFileNoDate" PickerDirection="ToRight"
                                            IconUrl="~/Image/Calendar.gif" Style="direction: ltr" RightToLeft="False"></pdc:PersianDateTextBox>
                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                            ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtFileNoDate" ID="PersianDateValidator2">تاریخ نامعتبر</pdc:PersianDateValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="توضیحات" ID="Label45"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtDesc" Width="100%"
                                            ClientInstanceName="Desc">
                                            <ValidationSettings>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تصویر پروانه اشتغال" ID="ASPxLabel2">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl ID="flpCommit" runat="server" ClientInstanceName="flp"
                                                            InputType="Images" UploadWhenFileChoosed="True" OnFileUploadComplete="flpCommit_FileUploadComplete"
                                                            Width="100%">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                        if(e.isValid){
	imgl.SetVisible(true);
	HD.Set('name',1);
	lblDocImageValidation.SetVisible(false);
	hp.SetVisible(true);
	hp.SetNavigateUrl('../../Image/Temp/'+e.callbackData);  
	}
	else{
    imgl.SetVisible(false);
	HD.Set('name',0);
	lblDocImageValidation.SetVisible(true);
	hp.SetVisible(false);
	hp.SetNavigateUrl('');
	}
}" />
                                                            <%--   <CancelButton Text="انصراف">
                                                                </CancelButton>--%>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="تصویر پروانه اشتغال را انتخاب نمایید" ClientVisible="False"
                                                            ID="lblDocImageValidation" ForeColor="Red" ClientInstanceName="lblDocImageValidation">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="imgEndUploadImg" ClientInstanceName="imgl">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink runat="server" Text="آدرس فایل" ClientVisible="False" Target="_blank"
                                            ID="HpCommit" ClientInstanceName="hp">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                        <fieldset id="ASPxRoundPanelGrade" runat="server">
                            <legend class="HelpUL">تعیین صلاحیت ها</legend>
                            <table runat="server" id="tblgr" dir="rtl" width="100%">
                                <tr>
                                    <td valign="top" align="right" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="پایه*" ID="ASPxLabel3">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="35%">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            TextField="GrdName" ID="ComboGrade" DataSourceID="OdbGrade" ValueType="System.String"
                                            ValueField="GrdId" RightToLeft="True"
                                            EnableIncrementalFiltering="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ValidationGroup="Grd" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="پایه را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="صلاحیت*" ID="lblResp" ClientInstanceName="lblResp">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="35%">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            TextField="ResName" ID="ComboResp" ClientInstanceName="ComboResp" DataSourceID="OdbResponsibility"
                                            ValueType="System.String" ValueField="ResId" RightToLeft="True"
                                            EnableIncrementalFiltering="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ValidationGroup="Grd" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="صلاحیت را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                        <asp:ObjectDataSource ID="OdbResponsibility" runat="server" SelectMethod="GetWithFilterByRes"
                                            TypeName="TSP.DataManager.ResponcibilityTypeManager">
                                            <SelectParameters>
                                                <asp:Parameter DbType="String" Name="ResIdFilterString" DefaultValue="(0)" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="center" colspan="4">
                                        <br />
                                        <TSPControls:CustomAspxButton runat="server" Text="&nbsp;&nbsp;اضافه به لیست"
                                            ID="btnAdd" ValidationGroup="Grd"
                                            OnClick="btnAddResponseblity_Click">
                                        </TSPControls:CustomAspxButton>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div dir="rtl">
                                <TSPControls:CustomAspxDevGridView2 Width="100%" runat="server"
                                    ID="GridViewResponseblity" KeyFieldName="Id" AutoGenerateColumns="False"
                                    ClientInstanceName="GridViewResponseblity"
                                    OnRowDeleting="GridViewResponseblity_RowDeleting" OnHtmlRowPrepared="GridViewResponseblity_HtmlRowPrepared"
                                    OnCustomCallback="GridViewResponseblity_CustomCallback">
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " Name="ColDeleteGrade" ShowDeleteButton="true">
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GrdName" Caption="پایه">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" Width="200px" FieldName="ResName"
                                            Caption="صلاحیت">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" Width="150px" FieldName="MjName" Caption="رشته">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Status" Caption="وضعیت"
                                            Name="InActiveCol">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                </TSPControls:CustomAspxDevGridView2>
                            </div>
                        </fieldset>
                        <dxcp:ASPxPanel ID="RoundPanelCity" ClientInstanceName="RoundPanelCity"
                            runat="server">
                            <PanelCollection>
                                <dxcp:PanelContent>
                                    <fieldset id="FieldsetCity" runat="server">
                                        <legend class="HelpUL">منطقه نظارت</legend>
                                        <table width="100%" id="tblCity" runat="server">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="شهر*" ID="ASPxLabel8">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                            TextField="CitName" ID="cmbRegionOfCity" AutoPostBack="true" ValueType="System.String"
                                                            ValueField="CitId" RightToLeft="True"
                                                            EnableIncrementalFiltering="True">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ValidationSettings Display="Dynamic" ValidationGroup="ValidCity" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" ErrorText="شهر را انتخاب نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <ButtonStyle Width="13px">
                                                            </ButtonStyle>
                                                        </TSPControls:CustomAspxComboBox>

                                                    </td>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="شهرستان" ID="ASPxLabel9">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtRegionOfCity" Width="100%"
                                                            ReadOnly="True">
                                                            <ValidationSettings Display="Dynamic" ValidationGroup="ValidCity" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" ErrorText="شهرستان محل اقامت را وارد نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>

                                                </tr>


                                                <tr>
                                                    <td valign="top" align="center" colspan="4">
                                                        <br />
                                                        <TSPControls:CustomAspxButton runat="server" Text="&nbsp;&nbsp;اضافه به لیست"
                                                            ID="btnAddCity" ValidationGroup="ValidCity"
                                                            OnClick="btnAddCity_Click">
                                                            <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                                        </TSPControls:CustomAspxButton>
                                                        <br />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <TSPControls:CustomAspxDevGridView2 Width="100%" runat="server"
                                            ID="GridViewCity" KeyFieldName="Id" AutoGenerateColumns="False" ClientInstanceName="GridViewCity"
                                            OnRowDeleting="GridViewCity_RowDeleting"
                                            OnCustomCallback="GridViewCity_CustomCallback" OnHtmlRowPrepared="GridViewCity_HtmlRowPrepared">
                                            <Columns>
                                                <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " Name="ClmDelete" ShowClearFilterButton="true" ShowDeleteButton="true">
                                                </dxwgv:GridViewCommandColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="CitCode" Caption="کد شهر">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="CitName" Caption="شهر">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="InActiveName" Caption="وضعیت"
                                                    Name="InActiveCol">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="Id">
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>

                                        </TSPControls:CustomAspxDevGridView2>

                                    </fieldset>
                                </dxcp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxPanel>
                        <br />
                        <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                            ID="GridViewOffice" KeyFieldName="OfmId" AutoGenerateColumns="False" ClientInstanceName="GridViewOffice" DataSourceID="ObjectDataSourceOfficeMember"
                            Caption="اطلاعات شرکت حقوقی عضو نظام مهندسی">
                            <Columns>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="OfId" Caption="کد شرکت">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="OfName" Caption="نام شرکت">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="FileNo" Caption="شماره پروانه شرکت">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="DocumentStatusName" Caption="وضعیت پروانه شرکت">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>

                        </TSPControls:CustomAspxDevGridView2>
                        <asp:ObjectDataSource ID="ObjectDataSourceOfficeMember" runat="server" SelectMethod="SelectOfficeByKardan"
                            TypeName="TSP.DataManager.OfficeMemberManager" OldValuesParameterFormatString="original_{0}">
                            <SelectParameters>
                                <asp:Parameter DbType="Int32" DefaultValue="-2" Name="PersonId" />
                                <asp:Parameter DbType="Int32" DefaultValue="-2" Name="OfmType" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
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
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
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
                                            ID="btnSave1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            ClientInstanceName="save1" OnClick="btnSave_Click">
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

            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                            <img align="middle" src="../../Image/indicator.gif" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
            <asp:HiddenField ID="OtherPersonId" runat="server" Visible="False" />
            <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
            <asp:HiddenField ID="TnReId" runat="server" Visible="False" />
            <dxhf:ASPxHiddenField ID="HDFlpCommit" runat="server" ClientInstanceName="HD">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="OdbGrade" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbMajor" runat="server" SelectMethod="FindMjParents" TypeName="TSP.DataManager.MajorManager"
                OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>

            <asp:ObjectDataSource ID="ObjectDataSourceAgent" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.AccountingAgentManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceCity" runat="server" SelectMethod="SelectByAgent"
                TypeName="TSP.DataManager.CityManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DbType="Int32" DefaultValue="-1" Name="AgentId" />
                </SelectParameters>
            </asp:ObjectDataSource>

            <dxhf:ASPxHiddenField ID="HiddenFieldTechnician" runat="server" ClientInstanceName="HiddenFieldTn">
            </dxhf:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
