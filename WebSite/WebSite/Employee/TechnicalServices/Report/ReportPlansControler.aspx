<%@ Page Title="گزارش بازبین های نقشه ها" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ReportPlansControler.aspx.cs" Inherits="Employee_TechnicalServices_Report_ReportPlansControler" %>

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
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSPControls:CustomAspxDevGridView ID="GridViewPlansControler" runat="server" DataSourceID="ObjdsReportTSPlansControler"
        Width="100%"
        KeyFieldName="PlansControlerId" AutoGenerateColumns="False"
        ClientInstanceName="GridViewPlansControler">

        <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>

        <Columns>

            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="CountPlants" Width="150px" Caption="تعداد کل نقشه های ارجاع داده شده">
                <HeaderStyle Wrap="True" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="Date" Caption="تاریخ" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectId" Caption="کد پروژه">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MeId" Caption="کد عضویت">
                <HeaderStyle Wrap="True"></HeaderStyle>
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectId" Caption="کد پروژه"
                Name="ProjectId">
                <HeaderStyle Wrap="True"></HeaderStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Foundation" Caption="زیربنا پروژه">
                <HeaderStyle Wrap="True"></HeaderStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GroupName" Caption="گروه ساختمانی پروژه">
                <HeaderStyle Wrap="True"></HeaderStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="StructureSkeletonName" Caption="نوع اسکلت">
                <HeaderStyle Wrap="True"></HeaderStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FoundationMixSkeleton" Caption="متراژ طراح سازه(ساختمان ترکیبی)">
                <HeaderStyle Wrap="True"></HeaderStyle>
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FullName" Caption="نام و نام خانوادگی">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="BankAccNo" Caption="شماره حساب بازبین">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="PlansType" Caption="نوع نقشه">
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="InActiveName" Caption="وضعیت">
            </dxwgv:GridViewDataTextColumn>
              <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="CreateDatePlanControler" Caption="تاریخ ثبت بازبین">
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="LastStateDate" Caption="تاریخ ارسال مرحله جاری">
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewDataComboBoxColumn Caption="مرحله نقشه" FieldName="TaskId" Name="WFState"
                VisibleIndex="0">
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
            <dxwgv:GridViewCommandColumn VisibleIndex="14" Caption=" " ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowHorizontalScrollBar="true"></Settings>
    </TSPControls:CustomAspxDevGridView>
    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" TypeName="TSP.DataManager.WorkFlowTaskManager"
        SelectMethod="SelectByWorkCode">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewPlansControler"
        ExportedRowType="All">
    </dxwgv:ASPxGridViewExporter>
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
                                <td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:ObjectDataSource ID="ObjdsReportTSPlansControler" runat="server" TypeName="TSP.DataManager.TechnicalServices.PlansManager"
        SelectMethod="ReportTSPlansControler" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>

</asp:Content>


