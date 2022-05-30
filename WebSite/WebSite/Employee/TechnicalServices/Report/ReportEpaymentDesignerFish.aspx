<%@ Page Title="گزارش فیش های پرداخت الکترونیکی طراحی" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ReportEpaymentDesignerFish.aspx.cs" Inherits="Employee_TechnicalServices_Report_ReportEpaymentDesignerFish" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
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
                                <ClientSideEvents Click="function(s,e){GridReportEpaymentFish.PerformCallback('Print'); }"></ClientSideEvents>
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
                        <td>
                            <asp:Label runat="server" Text="وضعیت فیش" ID="Label48"></asp:Label>
                        </td>
                        <td>
                            <TSPControls:CustomAspxComboBox runat="server" IncrementalFilteringMode="StartsWith"
                                ID="cmbStatus" ClientInstanceName="cmbStatus"
                                EnableIncrementalFiltering="True"
                                RightToLeft="True">
                                <Items>
                                    <dx:ListEditItem Text="عدم پرداخت" Value="1" />
                                    <dx:ListEditItem Text="پرداخت" Value="3" />
                                </Items>
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                <ItemStyle HorizontalAlign="Right" />
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td valign="top" align="right">
                            <asp:Label runat="server" Text="نمایندگی" ID="Label1"></asp:Label>
                        </td>
                        <td valign="top" align="right">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                TextField="Name" ID="ComboAgent" ClientInstanceName="ComboAgent" DataSourceID="ObjectdatasourceAgent"  IncrementalFilteringMode="Contains"
                                ValueType="System.Int32" ValueField="AgentId" RightToLeft="True"
                                EnableIncrementalFiltering="True">
                                <ItemStyle HorizontalAlign="Right" />
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />

                            </TSPControls:CustomAspxComboBox>

                            <asp:ObjectDataSource ID="ObjectdatasourceAgent" runat="server" SelectMethod="FindByCode"
                                TypeName="TSP.DataManager.AccountingAgentManager">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="-1" Name="AgentId" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
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
    <TSPControls:CustomAspxDevGridView ID="GridReportEpaymentFish" runat="server" AutoGenerateColumns="False"
        ClientInstanceName="GridReportEpaymentFish" DataSourceID="ObjectReportEpaymentDesignerFish" KeyFieldName="AccountingId"
        OnCustomCallback="GridReportEpaymentFish_CustomCallback" RightToLeft="True"
        Width="100%">
        <Columns>
            <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="CurrentWfTasId" Name="WFState"
                VisibleIndex="0">
                <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                    ValueType="System.String">
                </PropertiesComboBox>
                <DataItemTemplate>
                    <div align="center">
                        <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>' ToolTip='<%# Bind("WFStateTaskName") %>'>
                        </dxe:ASPxImage>
                    </div>
                </DataItemTemplate>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="AgentName" Caption="نمایندگی پروژه">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="CitName" Caption="شهر پروژه">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectId" Caption="کد پروژه">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="PrjOwnerName" Caption="مالک پروژه">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TypeName" Caption="نحوه پرداخت">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="StatusName" Caption="وضعیت"
                Width="80px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="IsSMSSentName" Caption="درخواست پیامک">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="SendSMSDate" Caption="تاریخ درخواست پیامک"
                Width="80px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FishPayerName"
                Caption="نام پرداخت کننده" Width="170px">
                <CellStyle Wrap="False" HorizontalAlign="Right">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="2" Width="230px" FieldName="AccTypeName"
                Caption="بابت">
                <CellStyle Wrap="False" HorizontalAlign="Right">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Number" Caption="شماره">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="CreateDate" Caption="تاریخ ثبت فیش"
                Width="90px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Date" Caption="تاریخ فیش"
                Width="90px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="PaymentDate" Caption="تاریخ پرداخت"
                Width="90px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Width="200px" VisibleIndex="4" FieldName="Amount" Caption="مبلغ">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn VisibleIndex="6" Caption=" " ShowClearFilterButton="true">
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
                                <ClientSideEvents Click="function(s,e){GridReportEpaymentFish.PerformCallback('Print'); }"></ClientSideEvents>
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

    <asp:ObjectDataSource ID="ObjectReportEpaymentDesignerFish" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="SelectTSAccountingEpaymentDesingerReport" TypeName="TSP.DataManager.TechnicalServices.AccountingManager">
        <SelectParameters>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="AgentId"></asp:Parameter>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="ProjectId"></asp:Parameter>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="TableTypeId"></asp:Parameter>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="ProjectIngridientTypeId"></asp:Parameter>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="AccType"></asp:Parameter>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="Status"></asp:Parameter>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="FishPayerId"></asp:Parameter>
            <asp:Parameter Type="String" DefaultValue="1" Name="FromDate"></asp:Parameter>
            <asp:Parameter Type="String" DefaultValue="2" Name="ToDate"></asp:Parameter>
            <asp:Parameter Type="string" DefaultValue="" Name="AccTypeList"></asp:Parameter>
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
        TypeName="TSP.DataManager.WorkFlowTaskManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dx:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridReportEpaymentFish">
    </dx:ASPxGridViewExporter>
</asp:Content>

