<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddInstitues.aspx.cs" Inherits="Employee_Amoozesh_AddInstitues"
    Title="مشخصات مؤسسه" %>

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
            CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td align="right">
                                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                                EnableTheming="False" ToolTip="جدید" ID="BtnNew" EnableViewState="False" OnClick="BtnNew_Click"
                                                                UseSubmitBehavior="False">
                                                                <Image  Url="~/Images/icons/new.png">
                                                                </Image>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                </HoverStyle>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                                Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit" EnableViewState="False"
                                                                OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                                                <Image  Url="~/Images/icons/edit.png">
                                                                </Image>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                </HoverStyle>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  EnableTheming="False"
                                                                ToolTip="ذخیره" ID="btnSave" EnableViewState="False" OnClick="btnSave_Click"
                                                                UseSubmitBehavior="False">
                                                                <Image  Url="~/Images/icons/save.png">
                                                                </Image>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                </HoverStyle>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td width="10px" align="center">
                                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                                            </TSPControls:MenuSeprator>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                                EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click"
                                                                UseSubmitBehavior="False">
                                                                <Image  Url="~/Images/icons/Back.png">
                                                                </Image>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
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
                    <TSPControls:CustomAspxMenuHorizontal ID="MenuInstitue" runat="server" 
                        OnItemClick="ASPxMenu1_ItemClick">
                        <Items>
                            <dxm:MenuItem Name="Institue" Text="اطلاعات پایه" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Manager" Text="هیئت اجرایی">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Activity" Text="زمینه های فعالیت">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Facility" Text="امکانات و تجهیزات">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="InsTeacher" Text="اساتید">
                            </dxm:MenuItem>
                        </Items>
                    </TSPControls:CustomAspxMenuHorizontal>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelMain" HeaderText="مشاهده" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="vertical-align: top; text-align: right" dir="rtl" cellpadding="1" width="100%">
                                <tbody>
                                    <tr>
                                        <td align="center" colspan="4" valign="top">
                                            <dxe:ASPxLabel Width="100%" runat="server" Text="وضعیت درخواست :" ID="lblWorkFlowState" Font-Bold="False"
                                                ForeColor="Red">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td width="15%" style="vertical-align: top;" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام مؤسسه *" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td width="35%" style="vertical-align: top;" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="100%" ID="txtInsName"
                                                >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="لطفاً این فیلد را تکمیل نمائید."></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td width="15%" style="vertical-align: top;" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شهر *" ID="ASPxLabel6">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td width="35%" style="vertical-align: top;" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server"  
                                                TextField="CitName" ID="ASPxComboBoxCity" ClientInstanceName="ASPxComboBoxCity"
                                                DataSourceID="ObjdsCity" ValueType="System.Int32"
                                                ValueField="CitId"  EnableIncrementalFiltering="True"
                                                Width="100%" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                   <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                   
                                                    <RequiredField IsRequired="True" ErrorText="شهر را انتخاب نمائید."></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                            <asp:ObjectDataSource ID="ObjdsCity" runat="server" OldValuesParameterFormatString="original_{0}"
                                                SelectMethod="SelectByAgent" TypeName="TSP.DataManager.CityManager">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="-1" Name="AgentId" Type="Int32" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام مدیر مؤسسه *" ID="ASPxLabel4">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top;" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="100%" ID="txtManager"
                                                >
                                                <ValidationSettings Display="Dynamic" ErrorText="این فیلد را با فرمت صحیح تکمیل نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="لطفاً این فیلد را تکمیل نمائید."></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td style="vertical-align: top;" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره ثبت مؤسسه *" Width="100%" ID="ASPxLabel8">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top;" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Style="direction: ltr"  Width="100%"
                                                MaxLength="15" ID="txtRegNo" >
                                                <ValidationSettings Display="Dynamic" ErrorText="این فیلد را با فرمت صحیح تکمیل نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="لطفاً این فیلد را تکمیل نمائید."></RequiredField>
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح تکمیل نمائید." ValidationExpression="\d{0,15}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;" align="right">
                                            <dxe:ASPxLabel runat="server" Text="محل ثبت مؤسسه *" Width="100%" ID="ASPxLabel11">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top;" dir="rtl" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="100%" ID="txtRegPlace"
                                                >
                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح تکمیل نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="لطفاً این فیلد را تکمیل نمائید."></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td style="vertical-align: top;" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ ثبت مؤسسه *" ID="ASPxLabel12" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top;" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="245px" ShowPickerOnTop="True"
                                                ID="txtRegDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                            <asp:RequiredFieldValidator runat="server" ErrorMessage="لطفاً این فیلد را پر کنید."
                                                ToolTip="لطفاً این فیلد را پر کنید." ControlToValidate="txtRegDate" ID="RequiredFieldValidator8"
                                                Display="Dynamic">لطفاً این فیلد را پر کنید.</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره تلفن1" ID="ASPxLabel9">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top;" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="100%" MaxLength="12" ID="txtTel1"
                                                >
                                                <ValidationSettings Display="Dynamic" ErrorText="این فیلد را با فرمت صحیح وارد نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="0\d{8,11}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td style="vertical-align: top;" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره تلفن2" ID="ASPxLabel5">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top;" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="100%" MaxLength="12" ID="txtTel2"
                                                >
                                                <ValidationSettings Display="Dynamic" ErrorText="این فیلد را با فرمت صحیح وارد نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="0\d{8,11}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره تلفن همراه" Width="100%" ID="ASPxLabel13">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top;" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="100%" MaxLength="11" ID="txtMobileNo"
                                                >
                                                <ValidationSettings Display="Dynamic" ErrorText="این فیلد را با فرمت صحیح وارد نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="0\d{10}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td style="vertical-align: top;" align="right"></td>
                                        <td style="vertical-align: top;" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;" align="right">
                                            <dxe:ASPxLabel runat="server" Text="آدرس" ID="ASPxLabel3">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top;" colspan="3" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="100%" ID="txtAddress"
                                                >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;" align="right">
                                            <dxe:ASPxLabel runat="server" Text="آدرس پست الکترونیکی" Width="100%" ID="ASPxLabel16">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top;" colspan="3" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="100%" ID="txtEmail" >
                                                <ValidationSettings Display="Dynamic" ErrorText="این فیلد را با فرمت صحیح وارد نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;" align="right">
                                            <dxe:ASPxLabel runat="server" Text="آدرس وب سایت" Width="100%" ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top;" colspan="3" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="100%" ID="txtWebSite"
                                                >
                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" Width="100%" ID="ASPxLabel15">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top;" colspan="3" align="right">
                                            <TSPControls:CustomASPXMemo runat="server" Height="71px"  Width="100%" ID="txtDesc"
                                                >
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                   

                                </tbody>
                            </table>
                            <fieldset>
                                <legend class="HelpUL">مشخصات گواهینامه</legend>

                                <table width="100%">
                                    <tbody>
                                         <tr>
                                        <td align="right" valign="top">شماره مجوز:                                       
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="100%" ID="txtFileNo" Style="direction: ltr"
                                                 RightToLeft="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="شماره مجوز را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td dir="rtl" align="right" valign="top">شماره سریال:
                                          
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="100%" ID="txtSerialNo"
                                                Style="direction: ltr" >
                                               <%-- <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="شماره سریال را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>--%>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                        <tr>

                                            <td align="right" valign="top">تاریخ صدور مجوز:                                            
                                            </td>
                                            <td align="right" valign="top">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="250px" ShowPickerOnTop="True"
                                                    ID="txtStartDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                    ErrorMessage="تاریخ صدور را وارد نمایید" ControlToValidate="txtStartDate" ID="PersianDateValidator1"></pdc:PersianDateValidator>
                                            </td>
                                            <td align="right" valign="top">تاریخ پایان اعتبار:
                                          
                                            </td>
                                            <td align="right" valign="top">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="250px" ShowPickerOnTop="True"
                                                    ID="txtEndDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                    ErrorMessage="تاریخ پایان اعتباررا وارد نمایید" ControlToValidate="txtEndDate"
                                                    ID="PersianDateValidator2"></pdc:PersianDateValidator>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
         
                <dxhf:ASPxHiddenField ID="HiddenFieldInstitue" runat="server" ClientInstanceName="HiddenFieldInstitue">
                </dxhf:ASPxHiddenField>
                <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
                </br>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                            <table >
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                                EnableTheming="False" ToolTip="جدید" ID="BtnNew2" EnableViewState="False" OnClick="BtnNew_Click"
                                                                UseSubmitBehavior="False">
                                                                <Image  Url="~/Images/icons/new.png">
                                                                </Image>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                </HoverStyle>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                                Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit2" EnableViewState="False"
                                                                OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                                                <Image  Url="~/Images/icons/edit.png">
                                                                </Image>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                </HoverStyle>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  EnableTheming="False"
                                                                ToolTip="ذخیره" ID="btnSave2" EnableViewState="False" OnClick="btnSave_Click"
                                                                UseSubmitBehavior="False">
                                                                <Image  Url="~/Images/icons/save.png">
                                                                </Image>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                </HoverStyle>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td width="10px" align="center">
                                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3">
                                                            </TSPControls:MenuSeprator>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" " 
                                                                EnableTheming="False" ToolTip="بازگشت" ID="ASPxButton6" EnableViewState="False"
                                                                OnClick="btnBack_Click" UseSubmitBehavior="False">
                                                                <Image  Url="~/Images/icons/Back.png">
                                                                </Image>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                </HoverStyle>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                      
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                    BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
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
