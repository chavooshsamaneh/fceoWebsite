<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="CapacityAssignment.aspx.cs" Inherits="Employee_TechnicalServices_BaseInfo_CapacityAssignment"
    Title="مدیریت اختصاص ظرفیت" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
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

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
                <div dir="rtl" id="DivReport" class="DivErrors" align="right" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>

                            <table>
                                <tbody>
                                    <tr>
                                        <td align="right">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                ID="btnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnNew_Click">
                                                <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </hoverstyle>
                                                <image url="~/Images/icons/new.png">
                                                                        </image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnEdit_Click">
                                                <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </hoverstyle>
                                                <image url="~/Images/icons/edit.png">
                                                                        </image>
                                                <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnView_Click">
                                                <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </hoverstyle>
                                                <image url="~/Images/icons/view.png">
                                                                        </image>
                                                <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف"
                                                ID="btnDelete" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnDelete_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
	 	e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </hoverstyle>
                                                <image url="~/Images/icons/delete.png">
                                                                        </image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                                ID="btnPrint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {	
	mygrid.PerformCallback('Print');
}"></ClientSideEvents>
                                                <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </hoverstyle>
                                                <image url="~/Images/icons/printers.png">
                                                                        </image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False"
                                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                                UseSubmitBehavior="False" Visible="true">
                                                <ClientSideEvents Click="function(s,e){ btnTempExport.DoClick(); }"></ClientSideEvents>
                                                <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </hoverstyle>
                                                <image height="25px" url="~/Images/icons/ExportExcel.png" width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridViewCapacityAssignment" runat="server"
                    DataSourceID="ObjectDataSourceCapacityAssignment" Width="100%"
                    AutoGenerateColumns="False" KeyFieldName="CapacityAssignmentId"
                    OnCustomCallback="CustomAspxDevGridViewCapacityAssignment_CustomCallback" ClientInstanceName="mygrid"
                    OnHtmlDataCellPrepared="CustomAspxDevGridViewCapacityAssignment_HtmlDataCellPrepared"
                    OnAutoFilterCellEditorInitialize="CustomAspxDevGridViewCapacityAssignment_AutoFilterCellEditorInitialize">
                    <Columns>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="CapacityAssignmentId" Caption="کد"
                            Name="CapacityAssignmentId" Visible="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Year" Caption="سال" Name="Year">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="CapacityPrcnt" Caption="N"
                            Name="CapacityPrcnt">
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False" HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ شروع نمایندگی شیراز" FieldName="AssignmentDate" Name="AssignmentDate"
                            VisibleIndex="0">
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False" HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان نمایندگی شیراز" FieldName="EndDate" Name="EndDate"
                            VisibleIndex="0">
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False" HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        
                        <dxwgv:GridViewDataTextColumn Caption="تعداد کار مجاز شیراز" FieldName="WorkCountMainAgent" Name="WorkCountMainAgent"
                            VisibleIndex="0">
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False" HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تعداد کار زیر 400 شیراز" FieldName="WorkCountUnder400MainAgent" Name="WorkCountUnder400MainAgent"
                            VisibleIndex="0">
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False" HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataCheckColumn VisibleIndex="0" FieldName="FreeObsCapacityMainAgent" Caption="آزادسازی کارکرد نظارت شیراز"
                            Name="FreeObsCapacityMainAgent">
                            <PropertiesCheckEdit DisplayTextChecked="انتخاب شده" DisplayTextUnchecked="انتخاب نشده">
                            </PropertiesCheckEdit>
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False" HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataCheckColumn>
                        <dxwgv:GridViewDataCheckColumn VisibleIndex="0" FieldName="FreeDesCapacityMainAgent" Caption="آزادسازی کارکرد طراحی شیراز"
                            Name="FreeDesCapacityMainAgent">
                            <PropertiesCheckEdit DisplayTextChecked="انتخاب شده" DisplayTextUnchecked="انتخاب نشده">
                            </PropertiesCheckEdit>
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False" HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataCheckColumn>
                          <dxwgv:GridViewDataTextColumn Caption="تاریخ شروع سایر نمایندگی ها" FieldName="AssignmentDateOtherAgent" Name="AssignmentDateOtherAgent"
                            VisibleIndex="0">
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False" HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان سایر نمایندگی ها" FieldName="EndDateOtherAgents" Name="EndDateOtherAgents"
                            VisibleIndex="0">
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False" HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        
                        <dxwgv:GridViewDataTextColumn Caption="تعداد کار مجاز سایر نمایندگی ها" FieldName="WorkCountOtherAgents" Name="WorkCountOtherAgents"
                            VisibleIndex="0">
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False" HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تعداد کار زیر 400 سایر نمایندگی ها" FieldName="WorkCountUnder400OtherAgents" Name="WorkCountUnder400OtherAgents"
                            VisibleIndex="0">
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False" HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        
                        <dxwgv:GridViewDataTextColumn Caption="اجباری نبودن آپلود نامه شهرداری و بنیاد مسکن شیراز" FieldName="StopmandatoryFileUploadingMainAgent" Name="StopmandatoryFileUploadingMainAgent"
                            VisibleIndex="0">
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False" HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn><dxwgv:GridViewDataTextColumn Caption="اجباری نبودن آپلود نامه شهرداری و بنیاد مسکن شهرستان ها" FieldName="StopmandatoryFileUploadingOtherAgent" Name="StopmandatoryFileUploadingOtherAgent"
                            VisibleIndex="0">
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False" HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        
                        <dxwgv:GridViewDataCheckColumn VisibleIndex="0" FieldName="FreeObsCapacityOtherAgents" Caption="آزادسازی کارکرد نظارت سایر نمایندگی ها"
                            Name="FreeObsCapacityOtherAgents">
                            <PropertiesCheckEdit DisplayTextChecked="انتخاب شده" DisplayTextUnchecked="انتخاب نشده">
                            </PropertiesCheckEdit>
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False" HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataCheckColumn>
                        <dxwgv:GridViewDataCheckColumn VisibleIndex="0" FieldName="FreeDesCapacityOtherAgents" Caption="آزادسازی کارکرد طراحی سایر نمایندگی ها"
                            Name="FreeDesCapacityOtherAgents">
                            <PropertiesCheckEdit DisplayTextChecked="انتخاب شده" DisplayTextUnchecked="انتخاب نشده">
                            </PropertiesCheckEdit>
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False" HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataCheckColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="14" Caption=" " Width="30px" ShowClearFilterButton="true">
                 
                            <HeaderStyle Wrap="True" />
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <ClientSideEvents EndCallback="function(s, e) {
            if(s.cpDoPrint==1)
            {
                s.cpDoPrint = 0;
	window.open('../../../Print.aspx');
            }
}" />
                    <Settings ShowHorizontalScrollBar="true"></Settings>
                </TSPControls:CustomAspxDevGridView>
                <br />
                 <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                                        ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnNew_Click">
                                                                        <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </hoverstyle>
                                                                        <image url="~/Images/icons/new.png">
                                                                        </image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                                        Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnEdit_Click">
                                                                        <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </hoverstyle>
                                                                        <image url="~/Images/icons/edit.png">
                                                                        </image>
                                                                        <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                                        ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnView_Click">
                                                                        <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </hoverstyle>
                                                                        <image url="~/Images/icons/view.png">
                                                                        </image>
                                                                        <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td width="10px" align="center">
                                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2">
                                                                    </TSPControls:MenuSeprator>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف"
                                                                        ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnDelete_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
	 	e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                                        <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </hoverstyle>
                                                                        <image url="~/Images/icons/delete.png">
                                                                        </image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td width="10px" align="center">
                                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3">
                                                                    </TSPControls:MenuSeprator>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                                                        ID="btnPrint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False">
                                                                        <ClientSideEvents Click="function(s, e) {
	mygrid.PerformCallback('Print');
}"></ClientSideEvents>
                                                                        <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </hoverstyle>
                                                                        <image url="~/Images/icons/printers.png">
                                                                        </image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False"
                                                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                                                        UseSubmitBehavior="False" Visible="true">
                                                                        <ClientSideEvents Click="function(s,e){ btnTempExport.DoClick(); }"></ClientSideEvents>
                                                                        <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </hoverstyle>
                                                                        <image height="25px" url="~/Images/icons/ExportExcel.png" width="25px" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                               </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
           
        <asp:ObjectDataSource ID="ObjectDataSourceCapacityAssignment" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="TSP.DataManager.TechnicalServices.CapacityAssignmentManager"></asp:ObjectDataSource>
        <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="CustomAspxDevGridViewCapacityAssignment"
            ExportEmptyDetailGrid="True">
        </dxwgv:ASPxGridViewExporter>
        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTempExport" ClientVisible="false" ClientInstanceName="btnTempExport"
            runat="server" OnClick="btntemp_Click">
        </TSPControls:CustomAspxButton>
</asp:Content>
