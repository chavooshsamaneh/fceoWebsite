<%@ Page Title="مشخصات شماره حساب عضو" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="MembersBankAccNoInsert.aspx.cs" Inherits="Employee_MembersRegister_MembersBankAccNoInsert" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]</div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server"  EnableTheming="False"
                                    EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                  
                                    <Image  Url="~/Images/icons/save.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False" 
                                    EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                    ToolTip="بازگشت" UseSubmitBehavior="False">
                                 
                                    <Image  Url="~/Images/icons/Back.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanel1" ClientInstanceName="RoundPanel1"
                HeaderText="ویرایش" runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent>
                        <table width="100%" cellpadding="5">
                            <tr>
                                <td align="right" valign="top" width="15%">
                                    <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="ASPxLabel2">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" width="35%">
                                    <dxe:ASPxLabel runat="server" Font-Bold="true" ID="lblMeId">
                                    </dxe:ASPxLabel>
                                </td>
                                <td rowspan="4" align="center">
                                    <dxe:ASPxImage ID="imgMember" Width="100px" Height="120px" runat="server">
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="15%">
                                    <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel4">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" width="35%">
                                    <dxe:ASPxLabel runat="server" Font-Bold="true" ID="lblFirstName">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="15%">
                                    <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="ASPxLabel1">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" width="35%">
                                    <dxe:ASPxLabel runat="server" Font-Bold="true" ID="lblLastName">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="15%">
                                    <dxe:ASPxLabel runat="server" Text="کد ملی" ID="ASPxLabel3">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" width="35%">
                                    <dxe:ASPxLabel runat="server" Font-Bold="true" ID="lblSSN">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="15%">
                                    <dxe:ASPxLabel runat="server" Text="شماره حساب" ID="ASPxLabel5">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" width="35%">
                                  <TSPControls:CustomTextBox runat="server" ID="txtBanckNo" ClientInstanceName="txtBanckNo"></TSPControls:CustomTextBox>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader2" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server"  EnableTheming="False"
                                    EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                   
                                    <Image  Url="~/Images/icons/save.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server" CausesValidation="False" 
                                    EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                    ToolTip="بازگشت" UseSubmitBehavior="False">
                                    
                                    <Image  Url="~/Images/icons/Back.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نماييد
                        <img alt="" src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
            <dx:ASPxHiddenField ID="HiddenFieldPage" ClientInstanceName="HiddenFieldPage" runat="server">
            </dx:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


