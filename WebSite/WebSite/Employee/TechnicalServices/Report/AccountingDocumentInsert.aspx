<%@ Page Title="مشخصات لیست حق الزحمه ناظرین و مانده واریزی" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="AccountingDocumentInsert.aspx.cs" Inherits="Employee_TechnicalServices_Report_AccountingDocumentInsert" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
        <asp:Label ID="LabelWarning" runat="server" Text=""></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server" Width="100%">
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent2" runat="server">
                <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                    width="100%">
                    <tr>
                        <td align="right">
                            <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
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
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره لیست پرداخت حق الزحمه ناظران"
                                            ID="btnSave" UseSubmitBehavior="False" OnClick="btnSave_Click" EnableTheming="False">

                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" Width="25px" ID="btnBack" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <TSPControls:CustomAspxMenuHorizontal ID="MainMenu" runat="server"
        OnItemClick="MainMenu_ItemClick">
        <Items>
            <dxm:MenuItem Text="مشخصات لیست" Name="AccDoc" Enabled="false">
            </dxm:MenuItem>
            <dxm:MenuItem Text="لیست ناظران" Name="AccDocDetail">
            </dxm:MenuItem>

        </Items>
    </TSPControls:CustomAspxMenuHorizontal>
    <br />
    <TSPControls:CustomASPxRoundPanel ID="RoundPanelPage" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td valign="top" align="right" width="15%">شماره لیست                 
                            </td>
                            <td valign="top" align="right" width="35%">
                                <dx:ASPxLabel runat="server" ID="txtListNo" Width="100%"
                                    ClientInstanceName="txtListNo">
                                </dx:ASPxLabel>
                            </td>
                            <td width="15%">تاریخ لیست
                            </td>
                            <td width="35%">
                                <dx:ASPxLabel runat="server" ID="txtListDate" Width="100%"
                                    ClientInstanceName="txtListDate">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>وضعیت ارسال
                            </td>
                            <td>
                                <dx:ASPxLabel runat="server" ID="txtStatusName" Width="100%"
                                    ClientInstanceName="txtStatusName">
                                </dx:ASPxLabel>
                            </td>

                            <td>نمایندگی
                            </td>
                            <td>
                                <dx:ASPxLabel runat="server" ID="txtAgentName" Width="100%"
                                    ClientInstanceName="txtAgentName">
                                </dx:ASPxLabel>
                            </td>


                        </tr>
                        <tr>
                            <td valign="top" align="right">نوع لیست                              
                            </td>
                            <td valign="top" align="right">
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                    ID="CmbType" ValueType="System.String" SelectedIndex="0" ClientInstanceName="CmbType"
                                    RightToLeft="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {

}"></ClientSideEvents>
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="True" ErrorText="نوع لیست را انتخاب نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                    <Items>
                                        <dxe:ListEditItem Value="0" Text="پرداخت حق الزحمه ناظر"></dxe:ListEditItem>
                                        <dxe:ListEditItem Value="1" Text="آزاد سازی بر اساس پرداخت اقساط"></dxe:ListEditItem>
                                        <dxe:ListEditItem Value="2" Text="اصلاحیه" />
                                    </Items>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                </TSPControls:CustomAspxComboBox>
                            </td>
                            <td valign="top" align="right">نام لیست *
                            </td>
                            <td valign="top" align="right">
                                <TSPControls:CustomTextBox runat="server" ID="txtListName" Width="100%"
                                    ClientInstanceName="txtListName">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="true" ErrorText="نام لیست را وارد نمایید" />
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>

                        </tr>
                        <tr>
                            <td valign="top" align="right">توضیحات
                            </td>
                            <td colspan="3" valign="top" align="right">
                                <TSPControls:CustomASPXMemo runat="server" ID="txtDescription" Width="100%"
                                    ClientInstanceName="txtDescription">
                                </TSPControls:CustomASPXMemo>
                            </td>

                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent1" runat="server">
                <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                    width="100%">
                    <tr>
                        <td align="right">
                            <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
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
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره لیست پرداخت حق الزحمه ناظران"
                                            ID="btnSave2" UseSubmitBehavior="False" OnClick="btnSave_Click" EnableTheming="False">

                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" Width="25px" ID="btnBack2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <dx:ASPxHiddenField ID="HiddenFieldPage" runat="server"></dx:ASPxHiddenField>

</asp:Content>

