<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberInfoUserControl.ascx.cs"
    Inherits="UserControl_MemberInfoUserControl" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>

                        
<TSPControls:CustomASPxRoundPanel ID="CustomASPxRoundPanel1" runat="server" Width="100%"
    HeaderText="مشخصات عضو" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" >


    <PanelCollection>

        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
 
 
                <table width="100%">
                    <tr>
                        <td width="100%" align="center">
                            <dxe:ASPxLabel ID="lblErrorText" runat="server" Visible="false" ForeColor="Red">
                            </dxe:ASPxLabel>
                        </td>
                    </tr> <tr>
                            <td align="center" valign="top" colspan="4" dir="ltr">
                                <dxe:ASPxLabel runat="server" Text="" Font-Bold="False" ID="lblWorkFlowState"
                                    ForeColor="Red">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    <tr>
                        <td width="100%" align="right">
                            <table runat="server" id="TblMemberInfo" width="100%">
                                <tbody>
                                    <tr>
                                        <td align="right" valign="top" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="کد عضویت :" ID="lblMeIdTitle">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top" width="35%">
                                            <dxe:ASPxLabel ID="lblMeId" runat="server" Font-Bold="true">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="شماره عضویت :" ID="ASPxLabel16">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" dir="ltr" valign="top" width="35%">
                                            <dxe:ASPxLabel ID="lblMeNo" runat="server" Font-Bold="true">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام و نام خانوادگی :" ID="ASPxLabel4">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel ID="lblName" runat="server" Font-Bold="true">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد ملی :" ID="ASPxLabel5">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" dir="ltr" align="right">
                                            <dxe:ASPxLabel ID="lblCodeMelli" runat="server" Font-Bold="true">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره پروانه :" ID="ASPxLabel8">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <dxe:ASPxLabel ID="lblFileNo" runat="server" Font-Bold="true">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ اعتبار پروانه :" ID="ASPxLabel6">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <dxe:ASPxLabel ID="lblFileDate" runat="server" Font-Bold="true">
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
