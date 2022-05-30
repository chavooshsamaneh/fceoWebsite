<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddTeachers.aspx.cs" Inherits="Employee_Amoozesh_AddTeacher"
    Title="مشخصات فردی استاد" %>

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
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script language="javascript">
        function SetControlValues() {

            grid.GetRowValues(grid.GetFocusedRowIndex(), 'MeId;FirstName;LastName;FatherName;IdNo;SSN;TiId;LastMjName;HomeTel;HomeAdr;MobileNo;Email;BirhtDate;LastLiId;LastMjId', SetValue);
        }
        function SetValue(values) {
            ID.SetText(values[0]);
            mFirstName.SetText(values[1]);
            mLastName.SetText(values[2]);
            txtFatherNameClient.SetText(values[3]);
            txtIdNoClient.SetText(values[4]);
            txtSSNClient.SetText(values[5]);
            //cmbTiIdClient.SetValue(values[6]);
            //txtLastMajorClient.SetText(values[7]);
            txtTelClient.SetText(values[8]);
            txtAddressClient.SetText(values[9]);
            txtMobileNoClient.SetText(values[10]);
            txtEmailClient.SetText(values[11]);
            document.getElementById('<%=txtBrithDate.ClientID%>').value = values[12];

            cmbLicenceClient.SetSelectedIndex(0);
            cmbLicenceClient.SetValue(values[13]);
            cmbMajorClient.SetValue(values[14]);

        }

        function SetEnable(flag) {
            //alert(flag);
            //lblMeId.SetText('');
            txtMeNo.SetText('');
            txtSSN.SetText('');
            txtName.SetText('');
            txtLastName.SetText('');
            txtName.SetText('');
            txtFatherName.SetText('');
            txtIdNo.SetText('');
            txtTel.SetText('');
            txtMobileNo.SetText('');
            txtAddress.SetText('');
            txtEmail.SetText('');
            cmbLicence.SetText('');
            cmbMajor.SetText('');
            document.getElementById('<%=txtBrithDate.ClientID%>').value = '';

            lblMeId.SetVisible(!flag);
            txtMeNo.SetVisible(!flag);
            txtSSN.SetEnabled(flag);
            txtName.SetEnabled(flag);
            txtLastName.SetEnabled(flag);
            txtName.SetEnabled(flag);
            txtFatherName.SetEnabled(flag);
            txtIdNo.SetEnabled(flag);
            txtTel.SetEnabled(flag);
            txtMobileNo.SetEnabled(flag);
            txtAddress.SetEnabled(flag);
            txtEmail.SetEnabled(flag);
            cmbLicence.SetEnabled(flag);
            cmbMajor.SetEnabled(flag);
            document.getElementById('<%=txtBrithDate.ClientID%>').disabled = !flag;
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
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; width: 100%">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                            CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="BtnNew_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/new.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                            CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/edit.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                            ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnSave_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/save.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>

                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnBack_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/Back.png">
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
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <div style="width: 100%" align="right">
                <TSPControls:CustomAspxMenuHorizontal ID="MenuTeacherInfo" runat="server" 
                     Enabled="False" 
                    OnItemClick="ASPxMenu1_ItemClick" >
                    <Items>
                        <dxm:MenuItem Text="مشخصات فردی" Name="Teacher" Selected="true">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="مدارک تحصیلی" Name="Madrak">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="سوابق آموزشی" Name="Job">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="تالیفات و تحقیقات" Name="Research">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="مستندات" Name="Atachment">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="دروس" Name="Course" Visible="False">
                        </dxm:MenuItem>
                    </Items>
                    
                </TSPControls:CustomAspxMenuHorizontal>
            </div>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="vertical-align: top; text-align: right" dir="rtl" id="Table2" cellpadding="1"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; height: 30px" align="center" colspan="4">
                                        <dxe:ASPxLabel runat="server" Text="وضعیت درخواست:نامشخص" Font-Bold="False" ForeColor="Red"
                                            ID="lblWorkFlowState">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top" align="center" colspan="4"></td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="نوع عضویت" ID="ASPxLabel17">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="35%">
                                        <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" Width="100%" AutoPostBack="True"
                                            Enabled="False"   
                                            ClientInstanceName="cmbMemberType" ID="cmbMemberType" OnSelectedIndexChanged="cmbMemberType_SelectedIndexChanged">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <Items>
                                                <dxe:ListEditItem Text="عضو نظام" Value="0"></dxe:ListEditItem>
                                                <dxe:ListEditItem Text="شخص جدید" Value="1"></dxe:ListEditItem>
                                            </Items>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right" width="15%"></td>
                                    <td valign="top" align="right" width="35%"></td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="کد عضویت" ClientInstanceName="lblMeId" ID="lblMeId">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" AutoPostBack="True" 
                                             ClientInstanceName="txtMeNo"
                                            ID="txtMeNo" OnTextChanged="txtMeID_TextChanged">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                <RegularExpression ValidationExpression="\d*" ErrorText="کدعضویت را با فرمت صحیح وارد نمایید" />
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right"></td>
                                    <td valign="top" align="right"></td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام" ClientInstanceName="lblMname" ID="lblMeFirstName">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"  
                                            ClientInstanceName="txtName" ID="txtName">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText=""></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ClientInstanceName="lblMfamily"
                                            ID="lblMeLastName">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"  
                                            ClientInstanceName="txtLastName" ID="txtLastName">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام پدر" ID="ASPxLabel7">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td dir="rtl" valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"  
                                            ClientInstanceName="txtFatherName" EnableClientSideAPI="True" ID="txtFatherName">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ تولد" ID="ASPxLabel11">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" ShowPickerOnTop="True" Width="300px" ID="txtBrithDate"
                                            Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBrithDate" ErrorMessage="تاریخ تولد"
                                            Display="Dynamic" ToolTip="لطفاً این فیلد را پر کنید." ID="RequiredFieldValidator8">لطفاً این فیلد را پر کنید.</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="شماره شناسنامه" ID="ASPxLabel8">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"  
                                            ClientInstanceName="txtIdNo" EnableClientSideAPI="True" ID="txtIdNo">
                                            <ValidationSettings ErrorDisplayMode="ImageWithText" Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RegularExpression ErrorText="این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="\d{0,10}"></RegularExpression>
                                                <RequiredField IsRequired="true" ErrorText="شماره شناسنامه را وارد نمایید" />
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="کد ملی" ID="ASPxLabel12">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" MaxLength="10"  
                                            ClientInstanceName="txtSSN" EnableClientSideAPI="True" ID="txtSSN">
                                            <MaskSettings IncludeLiterals="DecimalSymbol"></MaskSettings>
                                            <ValidationSettings ErrorDisplayMode="ImageWithText" Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="true" ErrorText="کد ملی را وارد نمایید" />
                                                <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="\d{10}"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="آخرین مدرک تحصیلی" ID="lblicence">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ODBLicence"
                                            TextField="LiName" ValueField="LiId" HorizontalAlign="Right"
                                            Width="100%" 
                                            ClientInstanceName="cmbLicence" ID="cmbLicence">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" ErrorText="مدرک را انتخاب نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="رشته" ID="lblMajor">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ODBMajor"
                                            TextField="MjName" ValueField="MjId"  HorizontalAlign="Right"
                                            Width="100%" 
                                            ClientInstanceName="cmbMajor" EnableClientSideAPI="True" ID="cmbMajor">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" ErrorText="رشته را انتخاب نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="شماره تلفن" ID="ASPxLabel9">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" MaxLength="12"  
                                            ClientInstanceName="txtTel" EnableClientSideAPI="True" ID="txtTel">
                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorText="شماره تلفن به صورت چهار رقم پیش شماره و هفت رقم شماره تلفن می باشد(07116360332)"
                                                ErrorTextPosition="Bottom">
                                                <ErrorFrameStyle Wrap="True">
                                                </ErrorFrameStyle>
                                                <RegularExpression ErrorText="شماره تلفن به صورت چهار رقم پیش شماره و هفت رقم شماره تلفن می باشد(07116360332)"
                                                    ValidationExpression="0\d{8,11}"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="شماره تلفن همراه" ID="ASPxLabel13">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td  valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" MaxLength="11"  
                                            ClientInstanceName="txtMobileNo" EnableClientSideAPI="True" ID="txtMobileNo">
                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="0\d{10}"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="پست الکترونیک" ID="ASPxLabel16">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"  
                                            ClientInstanceName="txtEmail" EnableClientSideAPI="True" ID="txtEmail">
                                            <ValidationSettings ErrorDisplayMode="ImageWithText" Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                ErrorTextPosition="Bottom">
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                                <RequiredField IsRequired="True" ErrorText="آدرس پست الکترونیکی را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right"></td>
                                    <td valign="top" align="right"></td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="آدرس" ID="ASPxLabel3">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%"  
                                            ClientInstanceName="txtAddress" ID="txtAddress">
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
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel15">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%"  
                                            ClientInstanceName="txtDesc" EnableClientSideAPI="True" ID="txtDesc">
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
                            <legend class="HelpUL">مشخصات گواهینامه</legend>
                            <dxp:ASPxPanel runat="server" ID="PanelTeacherCertificateInfo" ClientInstanceName="PanelTeacherCertificateInfo">
                                <PanelCollection>
                                    <dxp:PanelContent>
                                        <table width="100%">
                                            <tbody>
                                                    <tr>
                                                <td valign="top" align="right" width="15%">
                                                    <dxe:ASPxLabel runat="server" Text="شماره مجوز تدریس:" Width="100%" ID="ASPxLabel1">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" width="35%">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFileNo"  Width="100%" 
                                                        Style="direction: ltr">
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
                                                <td dir="rtl" valign="top" align="right" width="15%">
                                                    <dxe:ASPxLabel runat="server" Text="شماره سریال:" Width="100%" ID="ASPxLabel2">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" width="35%">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtSerialNo"  Width="100%"
                                                         Style="direction: ltr">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                          
                                                            <%--<RequiredField IsRequired="True" ErrorText="شماره سریال را وارد نمایید"></RequiredField>--%>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="تاریخ صدور مجوز:" ID="ASPxLabel4" Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td  valign="top" align="right">
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="300px" ShowPickerOnTop="True"
                                                        ID="txtFileDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                    <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                        ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtFileDate" ID="PersianDateValidator1">تاریخ صحیح را وارد نمایید</pdc:PersianDateValidator>
                                                </td>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="تاریخ پایان اعتبار:" ID="ASPxLabel5" Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td  valign="top" align="right">
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="300px" ShowPickerOnTop="True"
                                                        ID="txtEndDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                    <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                        ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtEndDate" ID="PersianDateValidator2">تاریخ صحیح را وارد نمایید</pdc:PersianDateValidator>
                                                </td>
                                            </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxp:ASPxPanel>
                        </fieldset>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <dxhf:ASPxHiddenField ID="HiddenFieldTeacher" runat="server">
            </dxhf:ASPxHiddenField>
            <asp:HiddenField ID="TeacherId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="LastMjId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="LastLiId" runat="server" Visible="False"></asp:HiddenField>
            <asp:ObjectDataSource ID="ODBMajor" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager"
                OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBLicence" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.LicenceManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBTitle" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.TitleManager"></asp:ObjectDataSource>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; text-align: right;">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                            CausesValidation="False" ID="BtnNew2" EnableViewState="False" EnableTheming="False"
                                                            OnClick="BtnNew_Click" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/new.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                            CausesValidation="False" Width="25px" ID="btnEdit2" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/edit.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                            ID="btnSave2" EnableViewState="False" EnableTheming="False" OnClick="btnSave_Click"
                                                            UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/save.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>

                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                            CausesValidation="False" ID="ASPxButton6" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnBack_Click" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/Back.png">
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
