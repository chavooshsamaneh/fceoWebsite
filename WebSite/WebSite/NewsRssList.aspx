<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true" CodeFile="NewsRssList.aspx.cs" Inherits="NewsRssList" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <TSPControls:CustomASPxRoundPanel  ID="RoundPanelNewsSubject"
        HeaderText="" ShowHeader="false" runat="server" Width="100%" RightToLeft="True">
        <ContentPaddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
        <PanelCollection>
            <dxp:PanelContent>

                <div runat="server" id="divNewsSubject"  align="right" dir="rtl">
                </div>

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>

</asp:Content>


