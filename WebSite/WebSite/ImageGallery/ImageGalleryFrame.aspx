<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="ImageGalleryFrame.aspx.cs" Inherits="ImageGallery_ImageGalleryFrame"
    Title="تصاویر" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    Text=" " EnableTheming="False" ToolTip="بازگشت"
                                    ID="btnBack" EnableViewState="False" PostBackUrl="ImageGallery.aspx">
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
    <iframe frameborder="0" scrolling="no" style="height: 710px; width: 100%" runat="server"
        id="FrameHmlFile"></iframe>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    Text=" " EnableTheming="False" ToolTip="بازگشت"
                                    ID="btnBack2" EnableViewState="False" PostBackUrl="ImageGallery.aspx">
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
</asp:Content>
