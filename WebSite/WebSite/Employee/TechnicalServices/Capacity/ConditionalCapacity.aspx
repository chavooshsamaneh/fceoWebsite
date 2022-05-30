<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="ConditionalCapacity.aspx.cs" Inherits="Employee_TechnicalServices_Capacity_ConditionalCapacity"
    Title="مدیریت کاهش/افزایش ظرفیت" %>

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
    <div id="divcontent" style="width: 100%" align="center" dir="rtl">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div dir="rtl" id="DivReport" class="DivErrors" align="right" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
               	   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
        
                                                    <table  >
                                                        <tbody>
                                                            <tr>
                                                                <td align="right">
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                        ID="btnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnNew_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/new.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                        Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnEdit_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/edit.png">
                                                                        </Image>
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
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                                        ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnView_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/view.png">
                                                                        </Image>
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
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
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
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/delete.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td width="10px" align="center">
                                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                                                    </TSPControls:MenuSeprator>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                                                        ID="btnPrint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False">
                                                                        <ClientSideEvents Click="function(s, e) {	
	mygrid.PerformCallback('Print');
}"></ClientSideEvents>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/printers.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False" 
                                                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                                                        UseSubmitBehavior="False" Visible="true">
                                                                        <ClientSideEvents Click="function(s,e){ btnTempExport.DoClick(); }"></ClientSideEvents>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </HoverStyle>
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
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridViewConditionalCapacity"
                    runat="server" DataSourceID="ObjectDataSourceConditionalCapacity" Width="100%"
                      ClientInstanceName="mygrid"
                    KeyFieldName="ConditionalCapacityId" AutoGenerateColumns="False" OnHtmlRowPrepared="CustomAspxDevGridViewConditionalCapacity_HtmlRowPrepared"
                   
                    OnCustomCallback="CustomAspxDevGridViewConditionalCapacity_CustomCallback">
                    <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="ConditionalCapacityId"
                            Caption="کد" Name="ConditionalCapacityId">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MeOfficeEngOId" Caption="کد عضویت"
                            Name="MeOfficeEngOId" Width="80px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="Name" Name="Name" VisibleIndex="0"
                            Width="200px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MjName" Caption="رشته"
                            Name="MjName" Width="80px">
                            <CellStyle HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ImpGrdName" Caption="پایه اجرا"
                            Name="ImpGrdName" Width="80px">
                            <CellStyle HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ObsGrdName" Caption="پایه نظارت"
                            Name="ObsGrdName" Width="80px">
                            <CellStyle HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="DesGrdName" Caption="پایه طراحی"
                            Name="DesGrdName" Width="80px">
                            <CellStyle HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="UrbanismGrdName" Caption="پایه شهرسازی"
                            Name="UrbanismGrdName" Width="80px">
                            <CellStyle HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TrafficGrdName" Caption="پایه ترافیک"
                            Name="TrafficGrdName" Width="80px">
                            <CellStyle HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MappingGrdName" Caption="پایه نقشه برداری"
                            Name="MappingGrdName" Width="80px">
                            <CellStyle HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MemberType" Caption="نوع عضو"
                            Name="MemberType">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Capacity" Caption="ظرفیت"
                            Name="Capacity" Width="80px">
                            <CellStyle HorizontalAlign="center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="04" FieldName="Reason" Caption="دلیل"
                            Name="Reason" Width="150px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FromDate" Caption="از تاریخ"
                            Name="FromDate" Width="100px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                            <PropertiesTextEdit EnableFocusedStyle="False">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MyToDate" Caption="تا تاریخ"
                            Name="MyToDate" Width="100px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                            <PropertiesTextEdit EnableFocusedStyle="False">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Type" Caption="صلاحیت"
                            Name="Type" Width="100px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Caption="وضعیت" Name="Status" FieldName="Status">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="14" Caption=" " Width="30px" ShowClearFilterButton="true">
                 
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <ClientSideEvents EndCallback="function(s, e) {
            if(s.cpDoPrint==1)
            {
                s.cpDoPrint = 0;
	window.open('../../../Print.aspx');
            }
}" />                 
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
                </TSPControls:CustomAspxDevGridView>
                <br />
                        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                    <table >
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                        ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnNew_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/new.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                        Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnEdit_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/edit.png">
                                                                        </Image>
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
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                                        ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnView_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/view.png">
                                                                        </Image>
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
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
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
}
"></ClientSideEvents>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/delete.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td width="10px" align="center">
                                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3">
                                                                    </TSPControls:MenuSeprator>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                                                        ID="btnPrint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False">
                                                                        <ClientSideEvents Click="function(s, e) {
	mygrid.PerformCallback('Print');
}"></ClientSideEvents>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/printers.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False" 
                                                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                                                        UseSubmitBehavior="False" Visible="true">
                                                                        <ClientSideEvents Click="function(s,e){ btnTempExport.DoClick(); }"></ClientSideEvents>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Url="~/Images/icons/ExportExcel.png" Width="25px" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                              </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
            AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
            <ProgressTemplate>
                <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                    <img id="IMG1" src="../../../Image/indicator.gif" align="middle" />
                    لطفا صبر نمایید ...</div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
        <asp:ObjectDataSource ID="ObjectDataSourceConditionalCapacity" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="TSP.DataManager.TechnicalServices.ConditionalCapacityManager">
        </asp:ObjectDataSource>
        <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="CustomAspxDevGridViewConditionalCapacity"
            ExportEmptyDetailGrid="True">
        </dxwgv:ASPxGridViewExporter>
        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTempExport" ClientVisible="false" ClientInstanceName="btnTempExport"
            runat="server" OnClick="btntemp_Click">
        </TSPControls:CustomAspxButton>
    </div>
</asp:Content>
