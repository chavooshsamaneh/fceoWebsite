<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewImageGallery.aspx.cs"
    Inherits="ImageGallery_ViewImageGallery" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" href="style.css" />
</head>
<body alink="white" vlink="white" link="white">
    <form id="Form1" runat="server">

        <script language="javascript" type="text/javascript">
            var postponedCallBackValue = null;
            function ImageChanged(item) {
                if (CallBackImageInfo.InCallback())
                    postponedCallBackValue = item;
                else
                    CallBackImageInfo.PerformCallback(item);
            }
            function CallBackImageInfo_OnEndCallback(s, e) {
                if (postponedCallBackValue != null) {
                    CallBackImageInfo.PerformCallback(postponedCallBackValue);
                    postponedCallBackValue = null;
                }
            }
        </script>

        <div style="vertical-align: top" align="center">
            <div align="center">
                <asp:Panel ID="panelNoPic" runat="server" Visible="false">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <dxe:ASPxLabel ID="lblNoPic" runat="server" Text="تصویری برای این آلبوم ثبت شده است"
                        Font-Names="tahoma" Font-Size="14pt" ForeColor="White">
                    </dxe:ASPxLabel>
                </asp:Panel>
            </div>
            <asp:Panel ID="panelGallery" runat="server">
                <div id="gallery">
                    <dxhf:ASPxHiddenField ID="hiddenImageInfo" runat="server" ClientInstanceName="hiddenImageInfo">
                    </dxhf:ASPxHiddenField>
                    <div align="center">
                        <dxe:ASPxLabel ID="lblAlbumName" runat="server" Font-Names="tahoma" Font-Size="14pt"
                            ForeColor="White">
                        </dxe:ASPxLabel>
                    </div>
                    <br />
                    <div id="imagearea">
                        <div id="image">
                            <a href="javascript:slideShow.nav(-1)" class="imgnav" id="previmg"></a><a href="javascript:slideShow.nav(1)"
                                class="imgnav" id="nextimg"></a>
                        </div>
                    </div>
                    <div align="center" style="border-bottom: dimgray 2px solid; border-left: dimgray 2px solid;
                        padding-bottom: 5px; padding-left: 5px; padding-right: 5px; border-right: dimgray 2px solid;
                        padding-top: 5px;">
                        <TSPControls:CustomAspxCallbackPanel runat="server"  ClientInstanceName="CallBackImageInfo"
                            CssPostfix="BlackGlass" 
                            ID="CallBackImageInfo" OnCallback="CallBackImageInfo_Callback">
                            <ClientSideEvents EndCallback="CallBackImageInfo_OnEndCallback"></ClientSideEvents>
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <dxe:ASPxLabel ID="lblImageInfo" runat="server" Font-Names="tahoma" Font-Size="10pt"
                                        ForeColor="White" ClientInstanceName="lblImageInfo">
                                    </dxe:ASPxLabel>
                                </dx:PanelContent>
                            </PanelCollection>
                        </TSPControls:CustomAspxCallbackPanel>
                    </div>
                    <asp:Label ID="lblSlidShow" runat="server"></asp:Label>
                    <br />
                    <div align="center">
                        <asp:Label ID="lblDescription" runat="server" Font-Names="tahoma" Font-Size="10pt"
                            ForeColor="White"></asp:Label>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
