<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="ProjectIngridientMajors.aspx.cs" Inherits="Employee_TechnicalServices_BaseInfo_ProjectIngridientMajors"
    Title="مدیریت رشته های عوامل پروِژه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
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
            href="#">بستن</a>]<br />
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                    cellpadding="0" align="right">
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
                                    UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
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
        <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" DataSourceID="ObjectDataSourceProjectIngridientMajors"
            Width="100%"
            OnCustomCallback="CustomAspxDevGridView1_CustomCallback" AutoGenerateColumns="False"
            ClientInstanceName="mygrid" KeyFieldName="ProjectIngridientMajorsId">

            <columns>
                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="ProjectIngridientMajorsId"
                    Caption="ProjectIngridientMajorsId" Name="ProjectIngridientMajorsId">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectIngridientType"
                    Caption="عامل پروژه" Width="100px" Name="ProjectIngridientType">
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="1" Width="150px" FieldName="Type" Caption="نوع"
                    Name="Type">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="2" Width="200px" FieldName="MjName" Caption="رشته"
                    Name="MjName">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="GroupName" Caption="گروه ساختمانی"
                    Name="GroupName">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Step2" Caption="تعداد طبقات"
                    Name="Step">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="StructureSkeletonName" Caption="نوع اسکلت"
                    Name="StructureSkeletonName">
                </dxwgv:GridViewDataTextColumn>
                

                
                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="FoundationMin" Caption="حداقل متراژ"
                    Name="FoundationMin">
                </dxwgv:GridViewDataTextColumn>

                
                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="FoundationMax" Caption="حداکثر متراژ"
                    Name="FoundationMax">
                </dxwgv:GridViewDataTextColumn>

                
                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Percent" Caption="درصد متراژ"
                    Name="Percent">
                </dxwgv:GridViewDataTextColumn>

                
                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="ObserverGroup" Caption="گروه ناظران"
                    Name="ObserverGroup">
                </dxwgv:GridViewDataTextColumn>
                   <dxwgv:GridViewDataTextColumn VisibleIndex="4"  Caption="پایه ی مجاز" FieldName="GrdName" 
                   >
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewCommandColumn VisibleIndex="14" Caption=" " ShowClearFilterButton="true">
            
                </dxwgv:GridViewCommandColumn>
            </columns>
            <clientsideevents endcallback="function(s, e) {
            if(s.cpDoPrint==1)
            {
                s.cpDoPrint = 0;
	window.open('../../../Print.aspx');
            }
}" />

        </TSPControls:CustomAspxDevGridView>
        <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="CustomAspxDevGridView1"
            ExportEmptyDetailGrid="True">
        </dxwgv:ASPxGridViewExporter>
        <br />
        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
            <panelcollection>
                        <dxp:PanelContent>

                                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                cellpadding="0" align="right">
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
}"></ClientSideEvents>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/delete.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
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
                                                                UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
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
                    </panelcollection>
        </TSPControls:CustomASPxRoundPanelMenu>
        <asp:ObjectDataSource ID="ObjectDataSourceProjectIngridientMajors" runat="server"
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager"></asp:ObjectDataSource>
</asp:Content>
