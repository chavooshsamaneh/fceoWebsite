<%@ Page Title="مشخصات استان" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ProvinceInsert.aspx.cs" Inherits="Employee_Management_ProvinceInsert" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]</div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent1" runat="server">
                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                        ID="btnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        CausesValidation="False" AutoPostBack="True" OnClick="btnNew_Click">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/new.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                        Width="25px" ID="btnEdit" AutoPostBack="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" CausesValidation="False" OnClick="btnEdit_Click">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/edit.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server"  EnableTheming="False"
                                            EnableViewState="False" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False" OnClick="btnSave_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server"  EnableTheming="False"
                                        EnableViewState="False" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False" OnClick="btnBack_Click"
                                        CausesValidation="false">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/back.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelMain" ClientInstanceName="RoundPanelMain"
                HeaderText="مشاهده" runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent>
                        <table width="100%">
                            <tr>
                                <td width="15%" valign="top" align="right">
                                    نام استان
                                </td>
                                <td width="35%" valign="top" align="right">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtTittle"  Width="100%" >
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RequiredField IsRequired="true" ErrorText="نام استان را وارد نمایید"></RequiredField>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td width="15%" valign="top" align="right">
                                    کد استان
                                </td>
                                <td width="35%" valign="top" align="right">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtCode"  Width="100%" MaxLength="9" >
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RequiredField IsRequired="true" ErrorText="کد نظام مهندسی استان را وارد نمایید">
                                            </RequiredField>
                                            <RegularExpression ValidationExpression="\d*" ErrorText="کد نظام مهندسی عدد صحیح می باشد" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                   
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            </br>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent2" runat="server">
                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                        ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        CausesValidation="False" AutoPostBack="True" OnClick="btnNew_Click">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/new.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                        Width="25px" ID="btnEdit2" AutoPostBack="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" CausesValidation="False" OnClick="btnEdit_Click">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/edit.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False" OnClick="btnSave_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server"  EnableTheming="False"
                                        EnableViewState="False" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False" OnClick="btnBack_Click"
                                        CausesValidation="false">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/back.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ObjectDataSource ID="ObjdsbserverProvince" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.ProvinceManager"></asp:ObjectDataSource>
            <dx:ASPxHiddenField ID="HiddenFieldProvince" runat="server">
            </dx:ASPxHiddenField>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                DisplayAfter="0">
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
