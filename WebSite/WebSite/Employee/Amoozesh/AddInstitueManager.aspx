<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddInstitueManager.aspx.cs" Inherits="Employee_Amoozesh_AddInstitueManager"
    Title="مشخصات هیت مد یره مؤسسه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
        function SetControlValues() {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'MeId;FirstName;LastName', SetValue);
        }
        function SetValue(values) {
            txtMeNo.SetText(values[0]);
            //mFirstName.SetText(values[1]);
            //mLastName.SetText(values[2]);
            CallbackPanelManager.PerformCallback('');
        }

    </script>

    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <div align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div align="center">
                    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                        [<a class="closeLink" href="#">بستن</a>]
                    </div>

                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>



                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                    cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                    CausesValidation="False" ID="btnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnNew_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                    CausesValidation="False" Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/edit.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td  dir="ltr">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                    Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnSave_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/save.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                    CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnBack_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/Back.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>
                    <div style="width: 100%" align="right">
                        <dxe:ASPxLabel ID="lblInsName" runat="server" Text="ASPxLabel">
                        </dxe:ASPxLabel>
                    </div>
                    <br />
                    <TSPControls:CustomASPxRoundPanel ID="RoundPanelInsManager" HeaderText="ویرایش" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>


                                <TSPControls:CustomAspxCallbackPanel runat="server"   ID="CallbackPanelManager"
                                    ClientInstanceName="CallbackPanelManager" OnCallback="CallbackPanelManager_Callback">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            <div dir="rtl" id="Div4" onclick="return DIV3_onclick()">
                                                <table style="vertical-align: top; text-align: right" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td dir="ltr" align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="نوع عضویت" ID="ASPxLabel14">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td dir="ltr" align="right" valign="top">
                                                                <TSPControls:CustomAspxComboBox runat="server"  ID="cmbIsMember"
                                                                     AutoPostBack="True" ValueType="System.String" 
                                                                    OnSelectedIndexChanged="cmbIsMember_SelectedIndexChanged">
                                                                    <Items>
                                                                        <dxe:ListEditItem Value="0" Text="عضو نظام"></dxe:ListEditItem>
                                                                        <dxe:ListEditItem Value="1" Text="شخص جدید"></dxe:ListEditItem>
                                                                    </Items>
                                                                    <ValidationSettings>
                                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomAspxComboBox>
                                                            </td>
                                                            <td align="right" valign="top"></td>
                                                            <td dir="ltr" align="right" valign="top"></td>
                                                        </tr>
                                                        <tr>
                                                            <td dir="ltr" align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="lblMeNo">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="170px" AutoPostBack="True"
                                                                    ID="txtMeNo" ClientInstanceName="txtMeNo" 
                                                                    OnTextChanged="txtMeNo_TextChanged">
                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <RequiredField ErrorText="نام خانوادگی را وارد نمایید"></RequiredField>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                            <td align="right" colspan="2" valign="top"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="نام" ID="lblName">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="170px" ID="txtName" >
                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <RequiredField ErrorText="نام را وارد نمایید" IsRequired="True"></RequiredField>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="lblFamily">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="170px" ID="txtFamily" >
                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <RequiredField ErrorText="نام خانوادگی را وارد نمایید" IsRequired="True"></RequiredField>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="نام پدر" ID="ASPxLabel3">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="170px" ID="txtFather" >
                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <RequiredField ErrorText="نام پدر را وارد نمایید"></RequiredField>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="تاریخ تولد" ID="ASPxLabel4">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="170px" ShowPickerOnTop="True"
                                                                    ID="txtBrithDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;" RightToLeft="False"></pdc:PersianDateTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="شماره شناسنامه" ID="ASPxLabel5">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="170px" ID="txtIdNo" >
                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <RegularExpression ErrorText="شماره شناسنامه عداد صحیح می باشد" ValidationExpression="\d*"></RegularExpression>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="کد ملی" ID="ASPxLabel6">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="170px" ID="txtSSN" >
                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <RegularExpression ErrorText="کد ملی عددی ده رقمی می باشد" ValidationExpression="\d{10}"></RegularExpression>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="شماره تلفن" ID="ASPxLabel7">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="170px" ID="txtTell" >
                                                                    <ValidationSettings>
                                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <RegularExpression ErrorText="شماره تلفن عدد است" ValidationExpression="\d*"></RegularExpression>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="تلفن همراه" Width="57px" ID="ASPxLabel8">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="170px" ID="txtMobile" >
                                                                    <ValidationSettings>
                                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <RegularExpression ErrorText="تلفن همراه عدد است" ValidationExpression="\d*"></RegularExpression>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="مدرک تحصیلی" ID="ASPxLabel10">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td dir="ltr" align="right" valign="top">
                                                                <TSPControls:CustomAspxComboBox runat="server"  TextField="LiName"
                                                                    ID="cmbLicence"  DataSourceID="ObjdsLicence" ValueType="System.String"
                                                                    ValueField="LiId" >
                                                                    <ValidationSettings>
                                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomAspxComboBox>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="رشته تحصیلی" Width="93px" ID="ASPxLabel11">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td dir="ltr" align="right" valign="top">
                                                                <TSPControls:CustomAspxComboBox runat="server"  TextField="MjName"
                                                                    ID="cmbMajor"  DataSourceID="ObjdsMajor" ValueType="System.String"
                                                                    ValueField="MjId" >
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
                                                            <td align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="پست الکترونیکی" ID="ASPxLabel9">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="170px" ID="txtEmail" >
                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <RegularExpression ErrorText="آدرس پست الکترونیکی را صحیح وارد نمایید" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                            <td align="right" valign="top"></td>
                                                            <td align="right" valign="top"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="آدرس" ID="ASPxLabel12">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td colspan="3" align="right" valign="top">
                                                                <TSPControls:CustomASPXMemo runat="server" Height="37px"  Width="555px" ID="txtAddress"
                                                                    >
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
                                                            <td align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="میزان سهم(درصد)" ID="ASPxLabel1" Width="97px">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="170px" ID="txtInsShares"
                                                                    >
                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <RequiredField IsRequired="True" ErrorText="میزان سهم را وارد نمایید."></RequiredField>
                                                                        <RegularExpression ErrorText="آدرس پست الکترونیکی را صحیح وارد نمایید"></RegularExpression>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="مدت سابقه آموزشی(ماه)" Width="131px" ID="ASPxLabel2">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="170px" ID="txtJobDuration"
                                                                    >
                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <RequiredField IsRequired="True" ErrorText="مدت سابقه آموزشی را واد نمایید"></RequiredField>
                                                                        <RegularExpression ErrorText="مدت سابقه (ماه) آموزشی را صحیح وارد نمایید." ValidationExpression="\d*"></RegularExpression>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel13">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td colspan="3" align="right" valign="top">
                                                                <TSPControls:CustomASPXMemo runat="server" Height="37px"  Width="555px" ID="txtDescription"
                                                                    >
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
                                            </div>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </TSPControls:CustomAspxCallbackPanel>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanel>
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>



                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                    cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                    CausesValidation="False" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnNew_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                    CausesValidation="False" Width="25px" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/edit.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td  dir="ltr">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                    Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnSave_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/save.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                    CausesValidation="False" ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnBack_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/Back.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>
                    <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldInsManager">
                    </dxhf:ASPxHiddenField>

                    <asp:ObjectDataSource ID="ObjdsLicence" runat="server" UpdateMethod="Update" SelectMethod="GetData"
                        DeleteMethod="Delete" TypeName="TSP.DataManager.LicenceManager" InsertMethod="Insert"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsMajor" runat="server" UpdateMethod="Update" SelectMethod="GetData"
                        DeleteMethod="Delete" TypeName="TSP.DataManager.MajorManager" InsertMethod="Insert"></asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                <img align="middle" src="../../Image/indicator.gif" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
    </div>
</asp:Content>
