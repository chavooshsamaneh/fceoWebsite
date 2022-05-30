<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" Title="خطا" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div align="center">
        <TSPControls:CustomASPxRoundPanel ID="RoundPanelGeneralInfo" HeaderText="خطا" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>
                    <div align="center">    
                        <br />        
                        <asp:Label ID="label" runat="server" Text="شما در یکی از صفحات دچار این خطا شده اید : "
                            ForeColor="DarkBlue"></asp:Label>
                        <br />
                        <br />
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Italic="False"
                            Font-Size="10pt" ForeColor="#C00000">
                        </dxe:ASPxLabel>
                        <br />
                        <br />
                        <br />
                        <TSPControls:CustomAspxButton runat="server" UseSubmitBehavior="False" CausesValidation="False"
                            Text="&nbsp;&nbsp;&nbsp;بازگشت" Width="122px" ID="btnBack" PostBackUrl="javascript:history.go(-1)"
                             >
                            <Image Width="20px" Height="20px" Url="~/Images/back.png" />
                        </TSPControls:CustomAspxButton>
                    </div>
                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanel>
    </div>
</asp:Content>
