<%@ Page Title="گزارش خزانه" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AccountingFishes.aspx.cs" Inherits="Accounting_Reports_AccountingFishes" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>

    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">

        <PanelCollection>
            <dx:PanelContent>

                <table>
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
            </dx:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSPControls:CustomASPxRoundPanel ID="RoundPanelsearch" HeaderText="جستجو" runat="server"
        Width="100%">
        <PanelCollection>
            <dx:PanelContent>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td valign="top" align="right" width="15%">
                                <dx:ASPxLabel runat="server" Text="شماره فیش" ID="ASPxLabel1">
                                </dx:ASPxLabel>
                            </td>
                            <td valign="top" align="right" width="35%">
                                <TSPControls:CustomTextBox runat="server" ID="txtNumber" ClientInstanceName="txtNumber">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                </TSPControls:CustomTextBox>
                            </td>
                            <td valign="top" align="right" width="15%">
                                <dx:ASPxLabel runat="server" Text="نوع پرداخت" ID="ASPxLabel6">
                                </dx:ASPxLabel>
                            </td>
                            <td valign="top" width="35%">
                                <TSPControls:CustomAspxComboBox runat="server" IncrementalFilteringMode="StartsWith"
                                    ID="cmbType" ClientInstanceName="cmbType"
                                    EnableIncrementalFiltering="True"
                                    RightToLeft="True">
                                    <Items>
                                        <dx:ListEditItem Text="فیش" Value="1" />
                                        <dx:ListEditItem Text="چک" Value="2" />
                                    </Items>
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </TSPControls:CustomAspxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" width="15%">
                                <dx:ASPxLabel runat="server" Text="تاریخ از" ID="ASPxLabel2">
                                </dx:ASPxLabel>
                            </td>
                            <td valign="top" align="right" width="35%">
                                <pdc:PersianDateTextBox runat="server" DefaultDate="" ValidationGroup="Complain"
                                    ID="txtFromDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                    Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                            </td>
                            <td valign="top" align="right" width="15%">
                                <dx:ASPxLabel runat="server" Text="تاریخ تا" ID="ASPxLabel7">
                                </dx:ASPxLabel>
                            </td>
                            <td valign="top" align="left" width="35%">
                                <pdc:PersianDateTextBox runat="server" DefaultDate="" ValidationGroup="Complain"
                                    ID="txtToDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                    Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" width="15%">
                                <dx:ASPxLabel runat="server" Text="مبلغ" ID="ASPxLabel3">
                                </dx:ASPxLabel>
                            </td>
                            <td valign="top" align="right" width="35%">
                                <TSPControls:CustomTextBox runat="server" ID="txtAmount" ClientInstanceName="txtAmount">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                </TSPControls:CustomTextBox>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td width="50%" align="left">
                                            <TSPControls:CustomAspxButton ID="btnSearch" runat="server" AutoPostBack="true"
                                                Text="جستجو" ClientInstanceName="btnSearch" Width="100px"
                                                UseSubmitBehavior="False" OnClick="btnSearch_Click">


                                                <ClientSideEvents Click="function(s, e) {
	   
        e.processOnServer=false;
       if(CheckSearch()==0)
        {
          alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.'); 
          return; 
        }
        else{
       e.processOnServer=true;
         }

}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="50%" align="right">
                                            <TSPControls:CustomAspxButton ID="btnClear" runat="server" AutoPostBack="true"
                                                Text="پاک کردن فرم" Width="100px" UseSubmitBehavior="False"  OnClick="btnClear_Click">

                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <ul class="HelpUL">
        <li>کد شناسایی پرداخت کننده برای فیش های خدمات مهندسی "کد پروژه" می باشد</li>
    </ul>
    <TSPControls:CustomAspxDevGridView ID="GridViewFish" runat="server" AutoGenerateColumns="False"
        ClientInstanceName="grid" DataSourceID="objdsAccounting" KeyFieldName="AccountingId"
        OnCustomCallback="GridViewFish_CustomCallback" RightToLeft="True" Width="100%" ShowFooter="true">
        <TotalSummary>
            <dxwgv:ASPxSummaryItem FieldName="Amount" SummaryType="Sum" />
        </TotalSummary>
        <Columns>

            <dxwgv:GridViewDataTextColumn Caption="PaymentId" FieldName="PaymentId" VisibleIndex="0">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="PaymentDate" FieldName="PaymentDate" VisibleIndex="0">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ReferenceId" FieldName="ReferenceId" VisibleIndex="0">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="شماره فیش" FieldName="Number" VisibleIndex="0">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ" FieldName="Date" VisibleIndex="1" Width="70px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="مبلغ" FieldName="Amount" VisibleIndex="2"
                Width="100px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نوع پرداخت" FieldName="TypeName" VisibleIndex="3"
                Width="50px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت پرداخت" FieldName="StatusName" VisibleIndex="3"
                Width="50px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewDataTextColumn Caption="بابت" FieldName="AccTypeName" VisibleIndex="4"
                Width="200px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام پرداخت کننده" FieldName="FishPayerName"
                VisibleIndex="5" Width="150px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="FishPayerMeId"
                VisibleIndex="5" Width="150px">
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewDataTextColumn Caption="کد شناسایی پرداخت کننده" FieldName="FishPayerId"
                VisibleIndex="6" Width="100px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Width="80px" Caption="بانک" FieldName="Bank" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Width="100px" Caption="شعبه" FieldName="BranchName"
                VisibleIndex="8">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Width="100px" Caption="نمایندگی" FieldName="AgentName"
                VisibleIndex="8">
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="9"
                Width="150px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="10" Width="50px" ShowClearFilterButton="true">
                <%--         <clearfilterbutton visible="True">
                </clearfilterbutton>--%>
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowHorizontalScrollBar="True" />
        <ClientSideEvents EndCallback="function(s, e) {
	 if(s.cpPrint==1)
        {
            window.open('../../Print.aspx');
           s.cpPrint=0;
        }
}" />
    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dx:PanelContent>



                <table>
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
            </dx:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:ObjectDataSource ID="objdsAccounting" runat="server" TypeName="TSP.DataManager.TechnicalServices.AccountingManager"
        SelectMethod="SelectForReport">
        <SelectParameters>
            <asp:Parameter Name="Number" DbType="String" DefaultValue="%" />
            <asp:Parameter Name="Amount" DbType="String" DefaultValue="%" />
            <asp:Parameter Name="FromDate" DbType="String" DefaultValue="1" />
            <asp:Parameter Name="ToDate" DbType="String" DefaultValue="2" />
            <asp:Parameter Name="Type" DbType="Int32" DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dx:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewFish">
    </dx:ASPxGridViewExporter>
</asp:Content>
