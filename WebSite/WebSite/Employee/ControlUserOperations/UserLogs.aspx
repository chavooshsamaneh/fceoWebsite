<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="UserLogs.aspx.cs" Inherits="Employee_ControlUserOperations_UserLogs"
    Title="کنترل ورود و خروج کاربران" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

    <script type="text/javascript" language="javascript">
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
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                                    <table >
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                                        CausesValidation="False" ID="btnPrint" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" AutoPostBack="false">
                                                        
                                                        <Image  Url="~/Images/icons/printers.png">
                                                        </Image>
                                                        <ClientSideEvents Click="function(s,e){ GridViewUserLogs.PerformCallback('Print'); }" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False" 
                                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                                        UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                                     
                                                        <Image Height="25px" Url="~/Images/icons/ExportExcel.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
       <br />
        <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 15%">
                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="نام کاربری" Width="100%">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" style="width: 35%">
                                <TSPControls:CustomTextBox  ID="txtUserName" runat="server" ClientInstanceName="txtUserName" 
                                      Width="100%">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="SearchValid">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                            <td align="right" style="width: 15%"></td>
                            <td align="right" style="width: 35%"></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="از تاریخ" Width="100%">
                                </dxe:ASPxLabel>
                            </td>
                            <td id="tdDate" runat="server" style="width: 35%" align="right">
                                <pdc:PersianDateTextBox ID="txtDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                    PickerDirection="ToRight" RightToLeft="False"  Width="220px" Style="direction: ltr; text-align: right;"
                                     onkeypress="SearchKeyPress(event,2,btnSearch);">
                                </pdc:PersianDateTextBox>
                            </td>
                            <td align="right">
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="تا تاریخ" Width="100%">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right">
                                <pdc:PersianDateTextBox ID="txtDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                    PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                    Width="220px" onkeypress="SearchKeyPress(event,2,btnSearch);"></pdc:PersianDateTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4" dir="ltr" valign="top">
                                <br />
                                <table>
                                    <tr>
                                        <td style="width: 100px">
                                            <TSPControls:CustomAspxButton  ID="ASPxButton1" runat="server" AutoPostBack="true" OnClick="btnSearch_OnClick"
                                                  
                                                Text="پاک کردن فرم" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s, e) {
	   	 ClearSearch();
}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td style="width: 100px">
                                            <TSPControls:CustomAspxButton  ID="btnSearch" runat="server" AutoPostBack="true" OnClick="btnSearch_OnClick"
                                                  
                                                Text="جستجو" ClientInstanceName="btnSearch" Width="98px" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s, e) {
 e.processOnServer=false;
	   if(CheckSearch()==0)
        {
          alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.'); 
          return; 
        }
        else
        {                                       
         e.processOnServer=true;
        }
  
}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanel>


        <br />
        <TSPControls:CustomAspxDevGridView ID="GridViewUserLogs" Width="100%" runat="server"
            DataSourceID="ObjdsTrace" KeyFieldName="TrId" RightToLeft="True" ClientInstanceName="GridViewUserLogs"
            OnCustomCallback="GridViewUserLogs_CustomCallback" OnAutoFilterCellEditorInitialize="GridViewUserLogs_AutoFilterCellEditorInitialize"
            OnHtmlDataCellPrepared="GridViewUserLogs_HtmlDataCellPrepared">
            <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowFilterRowMenu="True" ShowHorizontalScrollBar="true" />
            <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True" ColumnResizeMode="Control"></SettingsBehavior>
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="نام کاربری" FieldName="Username" Width="140px"
                    VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FullName" VisibleIndex="1" Width="200px">
                    <CellStyle Wrap="false"></CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="نوع کاربری" FieldName="UltName" Width="100px"
                    VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="نوع عملیات" FieldName="TypeName" Name="TypeName"
                    VisibleIndex="3" Width="80px">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="IP" FieldName="Address" Width="110px" VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="تاریخ" FieldName="Date" Width="100px" VisibleIndex="5">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="ساعت" FieldName="Time" Width="80px" VisibleIndex="6">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewCommandColumn Caption=" " Width="40px" VisibleIndex="7" ShowClearFilterButton="true">
                
                </dxwgv:GridViewCommandColumn>
            </Columns>
            <SettingsDetail ExportMode="All" />
            <StylesPager>
                <PageNumber HorizontalAlign="Center" VerticalAlign="Middle">
                </PageNumber>
            </StylesPager>
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsCookies CookiesID="TraceCookieId" StoreFiltering="False" StorePaging="False" />
            <ClientSideEvents EndCallback="function(s, e) {
	 if(s.cpPrint==1)
        {
         s.cpPrint=0;
            window.open('../../Print.aspx');
        }
}" />
        </TSPControls:CustomAspxDevGridView>
        <dx:ASPxGridViewExporter ID="GridViewExporter" GridViewID="GridViewUserLogs" runat="server">
        </dx:ASPxGridViewExporter>
        <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                                    <table >
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                                        CausesValidation="False" ID="btnPrint2" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" AutoPostBack="false">
                                                       
                                                        <Image  Url="~/Images/icons/printers.png">
                                                        </Image>
                                                        <ClientSideEvents Click="function(s,e){ GridViewUserLogs.PerformCallback('Print'); }" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False" 
                                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                                        UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                                       
                                                        <Image Height="25px" Url="~/Images/icons/ExportExcel.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>

    <asp:ObjectDataSource ID="ObjdsTrace" runat="server" TypeName="TSP.DataManager.TraceManager"
        SelectMethod="SearchForManagmentPage">
        <SelectParameters>
            <asp:Parameter DefaultValue="%" Name="UserName" Type="String" />
            <asp:Parameter DefaultValue="9999/99/99" Name="DateFrom" Type="String" />
            <asp:Parameter DefaultValue="9999/99/99" Name="DateTo" Type="String" />
        </SelectParameters>

    </asp:ObjectDataSource>

</asp:Content>
