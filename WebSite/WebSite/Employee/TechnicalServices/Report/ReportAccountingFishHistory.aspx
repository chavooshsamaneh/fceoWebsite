<%@ Page Title="گزارش چاپ فیش های بانکی مالکین" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ReportAccountingFishHistory.aspx.cs" Inherits="Employee_TechnicalServices_Report_ReportAccountingFishHistory" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table cellpadding="0">
                    <tr>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                ID="btnPrint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False">
                                <ClientSideEvents Click="function(s,e){grid.PerformCallback('Print'); }"></ClientSideEvents>
                                <HoverStyle BackColor="#FFE0C0">
                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                </HoverStyle>
                                <Image Url="~/Images/icons/Printers.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی Excel"
                                ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                OnClick="btnExportExcel_Click">
                                <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                <HoverStyle BackColor="#FFE0C0">
                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                </HoverStyle>
                                <Image Url="~/Images/icons/ExportExcel.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table width="100%">


                    <tr>
                        <td align="right">تاریخ از
                                 
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox ID="txtCreateDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right">تاریخ تا
                                  
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox ID="txtCreateDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>


                    <tr>
                        <td align="center" colspan="4" dir="ltr" valign="top">
                            <br />
                            <table>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton ID="ASPxButton1" runat="server" AutoPostBack="true" OnClick="btnSearch_Click"
                                            Text="پاک کردن فرم" UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s, e) {
	   	 ClearSearch();
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton ID="btnSearch" runat="server" AutoPostBack="true" OnClick="btnSearch_Click"
                                            Text="جستجو" ClientInstanceName="btnSearch" Width="98px" UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s, e) {
 e.processOnServer=false;
 if (ASPxClientEdit.ValidateGroup('SearchValid')){
 
	   if(CheckSearch()==0)
        {
          alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.'); 
          return; 
        }
        else
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
    <div align="right">
        <ul class="HelpUL">
            <li>درصورت انتخاب بازه تاریخی بزرگ امکان دربافت خطا در گزارش وجود دارد و درضمن نمی توانید از آن خروجی اکسل تهیه کنید. </li>
        </ul>
    </div>
    <br />
    <TSPControls:CustomAspxDevGridView ID="GridViewPrintingHistory" runat="server" AutoGenerateColumns="False"
        ClientInstanceName="grid" DataSourceID="objdsPrintingHistory" KeyFieldName="PrtHId"
        OnCustomCallback="GridViewPrintingHistory_CustomCallback" RightToLeft="True"
        Width="100%">
        <Columns>
            <dxwgv:GridViewDataTextColumn Caption="مبلغ" FieldName="Amount" VisibleIndex="3"
                Width="200px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="FishStatusName" VisibleIndex="3"
                Width="200px">
                <CellStyle Wrap="false">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="نام کاربر" FieldName="UserFullName" VisibleIndex="3"
                Width="200px">
                <CellStyle Wrap="false">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Width="100px" Caption="تاریخ چاپ" FieldName="CreateDate"
                VisibleIndex="4">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Width="100px" Caption="ساعت چاپ" FieldName="CreateTime"
                VisibleIndex="5">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Width="100px" Caption="نام نمایندگی" FieldName="AgentName"
                VisibleIndex="5">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Width="100px" Caption="نحوه پرداخت" FieldName="TypeName"
                VisibleIndex="5">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Width="100px" Caption="کد پروژه" FieldName="ProjectId"
                VisibleIndex="5">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="5"
                Width="300px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="6" Width="50px" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowHorizontalScrollBar="True" />
        <ClientSideEvents EndCallback="function(s, e) {
	 if(s.cpPrint==1)
        {
            window.open('../../../Print.aspx');
           s.cpPrint=0;
        }
}" />
    </TSPControls:CustomAspxDevGridView>
    <br />

    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table cellpadding="0">
                    <tr>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                ID="btnPrint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False">
                                <ClientSideEvents Click="function(s,e){grid.PerformCallback('Print'); }"></ClientSideEvents>
                                <HoverStyle BackColor="#FFE0C0">
                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                </HoverStyle>
                                <Image Url="~/Images/icons/Printers.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی Excel"
                                ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                OnClick="btnExportExcel_Click">
                                <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                <HoverStyle BackColor="#FFE0C0">
                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                </HoverStyle>
                                <Image Url="~/Images/icons/ExportExcel.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:ObjectDataSource ID="objdsPrintingHistory" runat="server" TypeName="TSP.DataManager.PrintingHistoryManager"
        SelectMethod="SelectPrintingHistoryForTS">
        <SelectParameters>
            <asp:Parameter DefaultValue="1" Name="CreateDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="CreateDateTo" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dx:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewPrintingHistory">
    </dx:ASPxGridViewExporter>
</asp:Content>
