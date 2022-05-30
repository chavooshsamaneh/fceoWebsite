<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SeminarInfoUserControl.ascx.cs" Inherits="UserControl_SeminarInfoUserControl" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>    


                <TSPControls:CustomASPxRoundPanel ID="CustomASPxRoundPanel1" runat="server" Width="100%"
    HeaderText="مشخصات سمینار" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" >


    <PanelCollection>

        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <table width="100%">
                    <tr>
                        <td width="100%" align="center">
                            <dxe:ASPxLabel ID="lblErrorText" runat="server" Visible="false" ForeColor="Red">
                            </dxe:ASPxLabel>
                        </td>
                    </tr> 
                    <tr>
                        <td width="100%" align="right">
                            <table runat="server" id="TblMemberInfo" width="100%">
                                <tbody>
                                    <tr>
                                        <td align="right" valign="top" width="15%">
                                          موضوع:
                                        </td>
                                        <td align="right" valign="top" colspan="3" width="85%">
                                            <dxe:ASPxLabel ID="lblSeminarSubject" runat="server" Font-Bold="true">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right"  width="15%">
                                         موسسه:
                                        </td>
                                        <td valign="top" align="right" colspan="3" width="85%">
                                            <dxe:ASPxLabel ID="lblIns" runat="server" Font-Bold="true">
                                            </dxe:ASPxLabel>
                                        </td>
                                    
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right"  width="15%">
                                           تاریخ شروع:
                                        </td>
                                        <td dir="ltr" valign="top" align="right"  width="35%">
                                            <dxe:ASPxLabel ID="lblStartDate" runat="server" Font-Bold="true">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right"  width="15%">
                                          تاریخ پایان:
                                        </td>
                                        <td dir="ltr" valign="top"  align="right"  width="35%">
                                            <dxe:ASPxLabel ID="lblEndDate" runat="server" Font-Bold="true">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
              </dx:PanelContent>
    </PanelCollection>
</TSPControls:CustomASPxRoundPanel>

