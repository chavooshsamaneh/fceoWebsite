<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="IntroductionInsert.aspx.cs"
    Inherits="Employee_Management_IntroductionInsert" Title="مدیریت معرفی سازمان" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%--<%@ Register Assembly="obout_Editor" Namespace="OboutInc.Editor" TagPrefix="obout" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                                        <table >
                                            <tbody>
                                                <tr>
                                                    <td style="width: 30px">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره" CausesValidation="False" ID="btnSave" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnSaveClient2" OnClick="btnSave_Click">
                                                            <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>
                                                            <Image Url="~/Images/icons/save.png"></Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelMemberInfo" HeaderText="معرفی سازمان" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                                     <%--   <obout:Editor ID="txtIntText" runat="server" Appearance="full" AutoFocus="False" ModeSwitch="False"
                                        PathPrefix="~" Rtl="True" ShowQuickFormat="False" ShowWaitMessage="False" Submit="False">
                                        <Buttons>
                                            <obout:Custom ImageName="ed_upload_image_n.gif" OnClientClick="myImageUpload" ToolTip="Upload image" />
                                        </Buttons>
                                        <DefaultTable BorderStyle="solid" HeightUnit="px" WidthUnit="px" />
                                    </obout:Editor>--%>
                                        <TSPControls:CustomASPxHtmlEditor ID="txtIntText" runat="server" Width="100%">
                                            <Settings AllowHtmlView="true" />
                                            <SettingsHtmlEditing AllowIFrames="true" />
                                        </TSPControls:CustomASPxHtmlEditor>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table >
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 30px">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره" CausesValidation="False" ID="btnSave2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnSaveClient2" OnClick="btnSave_Click">
                                                            <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>



                                                            <Image Url="~/Images/icons/save.png"></Image>
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
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0" BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
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

