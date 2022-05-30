<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="CandolenceDetail.aspx.cs" Inherits="CandolenceDetail" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>


    <%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:panelcontent>


  
    
                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                        cellpadding="0">
                        <tbody>
                            <tr>
                                <td >
                                    <dx:ASPxButton runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                        Text=" "  EnableTheming="False" ToolTip="بازگشت"
                                        ID="btnBack" EnableViewState="False" PostBackUrl="Default.aspx">
                                        <Image  Url="~/Images/icons/Back.png">
                                        </Image>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                        </HoverStyle>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
               </dxp:panelcontent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <table width="100%" class="TableBorder">
        <tr>
            <td class="TableTitle" colspan="2" align="right">
                <asp:Label ID="lblTitle" runat="server" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; width: 20%; background-color: whitesmoke;" align="center">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblDate" runat="server" Font-Size="7pt" ForeColor="Gray"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            <img alt="" src="" style="width: 100%; height: 100%" id="imgImage" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" style="padding: 5px 5px 5px 5px; text-align: justify">
                            <br />
                            <asp:Label ID="lblSummary" runat="server" Width="100%"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" style="padding: 5px 5px 5px 5px; text-align: justify">
                            <asp:Label ID="lblBody" runat="server" Width="100%"></asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            <asp:Label ID="lblFrom" runat="server" Width="100%" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:panelcontent>



                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                        cellpadding="0">
                        <tbody>
                            <tr>
                                <td >
                                    <dx:ASPxButton runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                        Text=" "  EnableTheming="False" ToolTip="بازگشت"
                                        ID="btnBack2" EnableViewState="False" PostBackUrl="Default.aspx">
                                        <Image  Url="~/Images/icons/Back.png">
                                        </Image>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                        </HoverStyle>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                 </dxp:panelcontent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
</asp:Content>
