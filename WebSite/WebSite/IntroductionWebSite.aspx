<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="IntroductionWebSite.aspx.cs" Inherits="IntroductionWebSite" Title="معرفی سامانه الکترونیکی نظام مهندسی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="معرفی سامانه الکترونیکی نظام مهندسی"  runat="server">
        <PanelCollection>
            <dxp:PanelContent>

      
                    <iframe frameborder="0" scrolling="no" style="height: 3150px; width: 100%" runat="server"
                        id="FrameHmlFile"></iframe>
                     </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
</asp:Content>
