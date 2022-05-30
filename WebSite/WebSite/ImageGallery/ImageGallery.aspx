<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="ImageGallery.aspx.cs" Inherits="ImageGallery_ImageGallery" Title="گزارش تصویری" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script language="javascript" type="text/javascript">
        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
        function ChangeVisible(id) {
            id.style.display = 'none';
            return true;
        }

        function ChangeIcon(id) {
            id.style.cursor = "hand";
            return true;
        }

        function SearchKeyPress(e, Type) {
            if (Type == 1)//DevExpress controls
            {
                if (e.htmlEvent.keyCode == 13) {
                    btnSearch.DoClick();
                }
            }
            else if (Type == 2)//asp controls
            {
                if (e.keyCode == 13)
                    btnSearch.DoClick();
            }
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 100%" dir="rtl" align="center">
                <div align="right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text=""></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]
                </div>

                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>

                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" width="15%">
                                            <dx:ASPxLabel runat="server" Text="نام آلبوم" ID="ASPxLabel1">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td valign="top"   align="right" width="35%">
                                            <dx:ASPxTextBox runat="server" Width="100%" ID="txtAlbumName" ClientInstanceName="txtAlbumName">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td valign="top" align="right" width="15%"></td>
                                        <td valign="top" align="right" width="35%"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" width="15%">
                                            <dx:ASPxLabel runat="server" Text="تاریخ ثبت از" ID="ASPxLabel2">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="193px" ID="txtFromDate"
                                                PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                                Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                        </td>
                                        <td valign="top" align="right" width="15%">
                                            <dx:ASPxLabel runat="server" Text="تا" ID="ASPxLabel7">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td valign="top"  width="35%">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="193px" ID="txtToDate"
                                                PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                                Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center">
                                       
                                            <table width="100%">
                                                <tr>
                                                    <td width="50%" align="left">
                                                        <TSPControls:CustomAspxButton Width="90px" ID="btnSearch" runat="server" AutoPostBack="false"
                                                            Text="جستجو" ClientInstanceName="btnSearch" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {	
DataViewAlbum.PerformCallback('search');
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="50%" align="right">
                                                        <TSPControls:CustomAspxButton ID="btnClear" runat="server" AutoPostBack="false"
                                                            Text="پاک کردن فرم" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {	
txtAlbumName.SetText('');
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxButton>
                                               
                                                </tr>
                                            </table>
                                            <br />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomAspxDevDataView ID="DataViewAlbum" ClientInstanceName="DataViewAlbum"
                    runat="server" DataSourceID="ObjdsAlbums" Width="100%" RowPerPage="10" SettingsTableLayout-ColumnCount="1" PagerSettings-EndlessPagingMode="OnScroll"
                    OnCustomCallback="DataViewAlbum_CustomCallback">
                    <ItemTemplate>
                        <table class="DataViewOneColumn" width="100%">
                            <tr>
                                <td style="vertical-align: top; width: 20%;" align="center"
                                    rowspan="2">
                                    <asp:Image ID="Image2" runat="server" Width="150px" OnDataBinding="Image_DataBinding"
                                        Height="100px" ImageUrl='<%# Eval("ImgUrl").ToString().Replace("../","") %>'></asp:Image>
                                </td>
                                <td style="vertical-align: top; width: 80%; padding: 5px 5px 5px 5px;" align="right">
                                    <table width="100%" style="padding: 7px 7px 7px 7px;">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("AlbumName") %>' Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td dir="ltr" align="right">تاریخ ثبت :
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("UploadDate") %>' Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="bottom" style="vertical-align: bottom; padding: 5px 5px 5px 5px"
                                    colspan="2">
                                    <asp:LinkButton CssClass="continueLink" ID="btnViewGallery" runat="server" OnDataBinding="btnViewGallery_DataBinding"
                                        ToolTip='<%# Bind("AlbumId")%>' Text="مشاهده گالری"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </TSPControls:CustomAspxDevDataView>
                <br />
                <asp:ObjectDataSource ID="ObjdsAlbums" runat="server" TypeName="TSP.DataManager.GalleryAlbumsManager"
                    SelectMethod="SelectViewGalleryAlbums">
                    <SelectParameters>
                        <asp:Parameter Name="AlbumName" Type="String" DefaultValue="%" />
                        <asp:Parameter Name="FromDate" Type="String" DefaultValue="1" />
                        <asp:Parameter Name="ToDate" Type="String" DefaultValue="2" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                    BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                            <img alt="" src="../Image/indicator.gif" align="middle" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
