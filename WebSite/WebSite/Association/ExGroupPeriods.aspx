<%@ Page Title="دوره های انتخاباتی تشکل ها" Language="C#" MasterPageFile="~/MasterPageWebsite.master"
    AutoEventWireup="true" CodeFile="ExGroupPeriods.aspx.cs" Inherits="ExGroups" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <TSPControls:CustomAspxMenuHorizontal ID="MenuExpGroup" runat="server"
                OnItemClick="MenuExpGroup_ItemClick"
                Visible="false">

                <Items>
                    <dxm:MenuItem Name="News" Text="اخبار گروه تخصصی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="ExpInfo" Enabled="false" Selected="true" Text="مشخصات گروه تخصصی">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>
            <TSPControls:CustomAspxDevDataView ID="DataViewExGroupPeriods" runat="server" ColumnCount="1"
                RowPerPage="10" Width="100%" DataSourceID="OdbExGroupPeriod" PagerSettings-EndlessPagingMode="OnClick">

                <ItemTemplate>
                    <table style="padding: 0px" class="TableBorder" width="100%">
                        <tr>
                            <td class="TableTitle" align="right">
                                <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("PeriodName") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="middle" align="right">
                                <table width="100%" cellpadding="3" cellspacing="0">
                                    <tr>
                                        <td rowspan="2" align="right" valign="middle" style="width: 20%">
                                            <dxe:ASPxImage ID="PeriodImage" runat="server" Height="100px" ImageUrl='<%# Bind("Attachment") %>'
                                                Width="100px">
                                                <EmptyImage Height="100px" Url="~/Images/person.png" Width="100px">
                                                </EmptyImage>
                                            </dxe:ASPxImage>
                                        </td>
                                        <td align="right" valign="middle" style="width: 80%">
                                            <table width="100%" cellpadding="3" cellspacing="3">
                                                <tbody>
                                                    <tr>
                                                        <td align="right" valign="middle">نوع تشکل :
                                                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="true" RightToLeft="True"
                                                                    Text='<%# Bind("ExGroupName") %>'>
                                                                </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="middle">تاریخ شروع دوره :
                                                                <dxe:ASPxLabel ID="lblstatus" runat="server" RightToLeft="True" Font-Bold="true"
                                                                    Text='<%# Bind("StartDate") %>'>
                                                                </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="middle">تاریخ پایان دوره :
                                                                <dxe:ASPxLabel ID="Label1" runat="server" Font-Bold="true" RightToLeft="True" Text='<%# Bind("EndDate") %>'>
                                                                </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="bottom">
                                            <asp:LinkButton ID="btnCandids" CssClass="continueLink" OnClick="btnCandids_Click" runat="server" CommandArgument='<%# Bind("ExGroupPeriodId") %>'>مشاهده اعضا</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <PagerStyle ItemSpacing="10px" Spacing="10px"></PagerStyle>
            </TSPControls:CustomAspxDevDataView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div style="font-size: 9pt; font-family: Tahoma" class="modalPopup">
                <img alt="" id="IMG1" src="../Image/indicator.gif" align="middle" />
                لطفا صبر نمایید ...
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
    <asp:ObjectDataSource ID="OdbExGroupPeriod" runat="server" SelectMethod="SelectActiveExpertGroupByCode"
        TypeName="TSP.DataManager.ExGroupPeriodManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DbType="Int32" DefaultValue="-1" Name="ExGroupCode" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
