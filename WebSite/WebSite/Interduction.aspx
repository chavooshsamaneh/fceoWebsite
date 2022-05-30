<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPageWebsite.master"
    CodeFile="Interduction.aspx.cs" Inherits="Interduction" Title="معرفی سازمان" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
	<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="معرفی سازمان" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                    <table align="center" dir="rtl" border="0" cellpadding="0" cellspacing="0" style="border-left-color: #ffffff;
                        border-bottom-color: #ffffff; vertical-align: top; border-top-color: #ffffff;
                        border-collapse: collapse; height: auto; border-right-color: #ffffff" width="100%">
                        <tr>
                            <td align="justify" width="100%">
                                <br />
                                <blockquote>
                                    <asp:literal ID="literalIntroduction" runat="server"></asp:literal>
                                    <asp:Label ID="labelIntroduction" runat="server"></asp:Label>
                         </blockquote>
                            </td>
                        </tr>
                    </table>
                    </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
</asp:Content>
