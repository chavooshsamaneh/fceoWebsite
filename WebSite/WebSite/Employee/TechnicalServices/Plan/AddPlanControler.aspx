<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="AddPlanControler.aspx.cs" Inherits="Employee_TechnicalServices_Plan_AddPlanControler"
    Title="مشخصات بازبین نقشه های ساختمانی" %>

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
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>




<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID='UpdatePanel1' runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]</div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                            Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                            CausesValidation="False" Width="25px" ID="btnBack" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
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
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelControler" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%" dir="rtl">
                            <tbody>
                                <tr>
                                    <td style="width: 15%" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="کد عضویت" Width="100%" ID="lblMeNo" ClientInstanceName="lblMeNo">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" style="width: 35%">
                                        <TSPControls:CustomTextBox runat="server" ID="txtMeNo"  Width="100%" AutoPostBack="True"
                                             OnTextChanged="txtMeNo_TextChanged">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="کد را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td style="width: 15%">
                                    </td>
                                    <td style="width: 35%">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 116px" valign="top" align="right" dir="ltr">
                                        <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel1">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" dir="rtl">
                                        <TSPControls:CustomTextBox runat="server" ID="txtName"  Width="100%" ReadOnly="True"
                                            >
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right" dir="rtl">
                                        <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="lblName" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" dir="rtl">
                                        <TSPControls:CustomTextBox runat="server" ID="txtFamily"  Width="100%" ReadOnly="True"
                                            >
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
                                        <dxe:ASPxLabel runat="server" Text="نام پدر" ID="ASPxLabel2" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" dir="rtl">
                                        <TSPControls:CustomTextBox runat="server" ID="txtFather"  Width="100%" ReadOnly="True"
                                            >
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right" dir="rtl">
                                        <dxe:ASPxLabel runat="server" Text="شماره پروانه اشتغال" ID="ASPxLabel3" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" dir="rtl">
                                        <TSPControls:CustomTextBox runat="server" ID="txtMfNo"  Width="100%" ReadOnly="True"
                                            >
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
                                    <td valign="top" align="right" dir="rtl">
                                        <dxe:ASPxLabel runat="server" Text="صلاحیت اجرا" ID="ASPxLabel6" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" dir="rtl">
                                        <TSPControls:CustomTextBox runat="server" ID="txtImplement"  Width="100%"
                                            ReadOnly="True" >
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="صلاحیت نظارت" ID="ASPxLabel4" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" dir="rtl">
                                        <TSPControls:CustomTextBox runat="server" ID="txtObservation"  Width="100%"
                                            ReadOnly="True" >
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
                                        <dxe:ASPxLabel runat="server" Text="صلاحیت طراحی" ID="ASPxLabel7" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" dir="rtl">
                                        <TSPControls:CustomTextBox runat="server" ID="txtDesign"  Width="100%" ReadOnly="True"
                                            >
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right" dir="rtl">
                                        <dxe:ASPxLabel runat="server" Text="صلاحیت نقشه برداری" ID="ASPxLabel8" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" dir="rtl">
                                        <TSPControls:CustomTextBox runat="server" ID="txtMapping"  Width="100%" ReadOnly="True"
                                            >
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
                                        <dxe:ASPxLabel runat="server" Text="صلاحیت ترافیک" ID="ASPxLabel5" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" dir="rtl">
                                        <TSPControls:CustomTextBox runat="server" ID="txtTraffice"  Width="100%"
                                            ReadOnly="True" >
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right" dir="rtl">
                                        <dxe:ASPxLabel runat="server" Text="صلاحیت شهرسازی" ID="ASPxLabel9" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" dir="rtl">
                                        <TSPControls:CustomTextBox runat="server" ID="txtUrbenism"  Width="100%"
                                            ReadOnly="True" >
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
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                            CausesValidation="False" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top" dir="ltr">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                            Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                            CausesValidation="False" Width="25px" ID="btnBack2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
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
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldControler">
            </dxhf:ASPxHiddenField>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                        <img src="../../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
