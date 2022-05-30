<%@ Page Title="دریافت اولویت ارجاع کار نظارت شهرداری شیراز" Async="true"  Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="QueueListMunicipality.aspx.cs" Inherits="Members_TechnicalServices_Capacity_QueueListMunicipality" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>



<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="BtnNew" CssClass="ButtonMenue" OnClick="BtnNew_Click" runat="server">دریافت اولویت ارجاع کار نظارت شهرداری شیراز</asp:LinkButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <br />
                        <div style="text-align:center">
                            <dxcp:ASPxLabel CssClass="HelpUL" Font-Bold="true" runat="server" Width="100%" Font-Size="15pt" ClientInstanceName="lblWarning" ID="lblWarning" Text="">
                            </dxcp:ASPxLabel>
                        </div>
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelPage" HeaderText="دریافت اولویت ارجاع کار نظارت شهرداری شیراز" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td width="15%">زیربنا نظارت شهرداری شیراز</td>
                                    <td width="35%">
                                        <dxcp:ASPxLabel ClientInstanceName="lblObsShirazMunicipality" runat="server" ID="lblObsShirazMunicipality" Text="">
                                        </dxcp:ASPxLabel>

                                    </td>
                                    <td width="15%">زیربنا طراحی شهرداری شیراز</td>
                                    <td width="35%">
                                        <dxcp:ASPxLabel ClientInstanceName="lblShirazMunicipalityDesignMeter" runat="server" ID="lblShirazMunicipalityDesignMeter" Text="">
                                        </dxcp:ASPxLabel>

                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnNew2" CssClass="ButtonMenue" OnClick="BtnNew_Click" runat="server">دریافت اولیت ارجاع کار نظارت شهرداری شیراز</asp:LinkButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldPage" ClientInstanceName="HiddenFieldPage">
            </dxhf:ASPxHiddenField>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

