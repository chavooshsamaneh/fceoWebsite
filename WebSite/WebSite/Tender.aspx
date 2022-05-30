<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="Tender.aspx.cs" Inherits="Tender" Title="مناقصات" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            ----
	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="جستجو" runat="server">
        <PanelCollection>
            <dxp:PanelContent>


                <table dir="rtl" width="100%" >
                    <tbody>
                        <tr>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="نام مناقصه" Width="55px" ID="ASPxLabel3">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right" colspan="3">
                                <TSPControls:CustomTextBox runat="server" ID="txtName" Width="100%">
                                    <ValidationSettings>
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="از تاریخ" ID="ASPxLabel6">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right">
                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="300px" ShowPickerOnTop="True"
                                    ID="txtFromDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                    Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                            </td>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="تا تاریخ" ID="ASPxLabel1">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="left">
                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="300px" ShowPickerOnTop="True"
                                    ID="txtToDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                    Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <br />
                                <TSPControls:CustomAspxButton runat="server" Text="جستجو" ID="btnSearch" UseSubmitBehavior="False"
                                    OnClick="btnSearch_Click">
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <asp:ObjectDataSource runat="server" SelectMethod="Search" ID="ObjectDataSource1"
                    TypeName="TSP.DataManager.TenderManager" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="%" Name="TeName"
                            ControlID="txtName"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="1" Name="FromDate"
                            ControlID="txtFromDate"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="2" Name="ToDate"
                            ControlID="txtToDate"></asp:ControlParameter>
                    </SelectParameters>
                </asp:ObjectDataSource>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
            <TSPControls:CustomAspxDevDataView runat="server"
                AlwaysShowPager="True" PagerPanelSpacing="0px" RightToLeft="True" 
                ID="ASPxDataView1" EnableViewState="False"
                ColumnCount="1" Width="100%" PagerSettings-EndlessPagingMode="OnClick">

                <ItemTemplate>
                <table dir="rtl" width="100%" class="DataViewOneColumn">
                    <tbody>
                        <tr>
                            <td class="TableTitle" valign="middle" align="right" colspan="4">
                                <dxe:ASPxLabel ID="ASPxLabel3" Font-Bold="True" runat="server" Width="100px" Text="مناقصه شماره :">
                                </dxe:ASPxLabel>
                                <dxe:ASPxLabel ID="ASPxLabel7" Font-Bold="True" runat="server" Text='<%# Bind("TeId") %>'>
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" colspan="4">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 55px" valign="top" align="right">
                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="نام ">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" colspan="3">
                                                <dxe:ASPxLabel ID="lblName" runat="server" Text='<%# Bind("TeName") %>'>
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="left" rowspan="2">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <a id="link1" href='<%# Bind("PdfUrl") %>' target="_blank" runat="server">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Download.png"></asp:Image>
                                                                </a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxHyperLink ID="HpLink" CssClass="continueLink" runat="server" Text="دانلود" NavigateUrl='<%# Bind("PdfUrl") %>'
                                                                    Target="_blank">
                                                                </dxe:ASPxHyperLink>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Width="55px" Text="تاریخ">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" dir="ltr" align="right" colspan="3">
                                                <dxe:ASPxLabel ID="lblDesc" runat="server" RightToLeft="False" Text='<%# Bind("Date") %>'>
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                </ItemTemplate>
            </TSPControls:CustomAspxDevDataView>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img src="Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
