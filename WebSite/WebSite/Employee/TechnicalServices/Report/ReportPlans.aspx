<%@ Page Title="گزارش نقشه ها" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ReportPlans.aspx.cs" Inherits="Employee_TechnicalServices_Report_ReportPlans" %>

<%@ Register Src="../../../UserControl/WFUserControl.ascx" TagName="WFUserControl"
    TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>

            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی اکسل"
                                            ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnExportExcel_Click">

                                            <Image Url="~/Images/icons/ExportExcel.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                        <td>
                                          
                                    </td>
                                </tr>
                            </tbody>
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
                                <td valign="top" align="right">
                                    <asp:Label runat="server" Text="نمایندگی" ID="Label1"></asp:Label>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                        TextField="Name" ID="ComboAgent" ClientInstanceName="ComboAgent" DataSourceID="ObjectdatasourceAgent" IncrementalFilteringMode="Contains"
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
                                <td>مرحله
                                </td>
                                <td>
                                    <TSPControls:CustomAspxComboBox ID="CmbTask" ClientInstanceName="CmbTask" runat="server"
                                        ValueType="System.String"
                                        TextField="TaskName" ValueField="TaskCode"
                                        DataSourceID="ObjdsWorkFlowTask" HorizontalAlign="Right" EnableIncrementalFiltering="True">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                    </TSPControls:CustomAspxComboBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="center" colspan="4" dir="ltr" valign="top">
                                    <br />
                                    <table>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton ID="btClear" runat="server" AutoPostBack="true" OnClick="btnSearch_Click"
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
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewPlans" runat="server" DataSourceID="ObjdsPlans"
                Width="100%"
                KeyFieldName="PlansId" AutoGenerateColumns="False"
                ClientInstanceName="GridViewPlans"
                OnCustomCallback="GridViewPlans_CustomCallback">
                <ClientSideEvents FocusedRowChanged="function(s, e) {
	
	if(GridViewPlans.cpIsPostBack==0)
	{
		GridViewPlans.ExpandDetailRow(GridViewPlans.GetFocusedRowIndex());
		GridViewPlans.cpIsPostBack=0;
	}
	else
	{
		GridViewPlans.cpIsPostBack=0;
	}
}"></ClientSideEvents>

                <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                <Columns>
                    <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                        VisibleIndex="0" Width="40px">
                        <DataItemTemplate>
                            <div align="center">
                                <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>'>
                                </dxe:ASPxImage>
                            </div>
                        </DataItemTemplate>
                        <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                            ValueType="System.String">
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" Visible="False" FieldName="Status"
                        Width="150px" Caption="Status">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="No" Width="150px" Caption="شماره نقشه">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="PlansTypeId" Caption="نوع نقشه" VisibleIndex="0"
                        Width="150px">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <PropertiesComboBox ValueType="System.String" TextField="Title" DataSourceID="ObjdsPlansType"
                            ValueField="PlansTypeId">
                        </PropertiesComboBox>
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="PlanDate" Caption="تاریخ ثبت نقشه">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="WFStateDate" Caption="تاریخ آخرین گردش کار">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectId" Caption="کد پروژه"
                        Name="ProjectId">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Foundation" Caption="متراژ پروژه">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="HasControler" Caption="دارای بازبین">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwgv:GridViewDataTextColumn>

                    <%--      <dxwgv:GridViewDataTextColumn VisibleIndex="11" Width="100px" FieldName="DesAcceptPlan"
                        Caption="وضعیت تایید">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwgv:GridViewDataTextColumn>--%>
                    <%--             <dxwgv:GridViewDataTextColumn VisibleIndex="12" FieldName="InActives" Caption="وضعیت">
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" Visible="False" FieldName="TaskName"
                        Width="150px" Caption="وضعیت درخواست">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="100px" FieldName="CitName"
                                    Caption="شهر" Name="CitName">
                                    <CellStyle Wrap="True" HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="100px" FieldName="MunName"
                                    Caption="شهرداری" Name="MunName">
                                    <CellStyle Wrap="True" HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <Settings ShowHorizontalScrollBar="true"></Settings>
            </TSPControls:CustomAspxDevGridView>
            <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewPlans"
                ExportedRowType="All">
            </dxwgv:ASPxGridViewExporter>
            <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewPlans" SessionName="SendBackDataTable_EmpPln"
                OnCallback="CallbackPanelWorkFlow_Callback" />

            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی اکسل"
                                            ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnExportExcel_Click">

                                            <Image Url="~/Images/icons/ExportExcel.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                      
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldPlan" ClientInstanceName="HDPlan">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" TypeName="TSP.DataManager.WorkFlowTaskManager"
                SelectMethod="SelectByWorkCode">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsPlans" runat="server" TypeName="TSP.DataManager.TechnicalServices.PlansManager"
                SelectMethod="SelectTSPlansByProjectForControlerManagmentPage" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="ProjectAgentId" Type="Int32" />
                    <asp:Parameter DefaultValue="-2" Name="TaskCode" Type="Int32" />
                    <asp:Parameter DefaultValue="2" Name="WfStateDateTo" Type="String" />
                    <asp:Parameter DefaultValue="1" Name="WfStateDateFrom" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsPlansType" runat="server" TypeName="TSP.DataManager.TechnicalServices.PlansTypeManager"
                SelectMethod="GetData"></asp:ObjectDataSource>
</asp:Content>
