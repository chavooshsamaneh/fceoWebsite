<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true" CodeFile="Introduction.aspx.cs" 
Inherits="Introduction" Title="معرفی سازمان" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1"  HeaderText="معرفی سازمان" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                <div class="row">
                    <span id="labelIntroduction" runat="server"></span>
                </div>
                    </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
</asp:Content>

