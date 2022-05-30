<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="MemberImpCapacity.aspx.cs" Inherits="Employee_TechnicalServices_BaseInfo_MemberImpCapacity"
    Title="مدیریت ظرفیت اشتغال اجرا شخص حقیقی" %>

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
                            href="#">بستن</a>]<br />
                    </div>
                   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                                                        <table >
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
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال"
                                                                            ID="btnInActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                                            EnableTheming="False" OnClick="btnInActive_Click">
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
                                                                            <Image  Url="~/Images/icons/disactive.png">
                                                                            </Image>
                                                                        </TSPControls:CustomAspxButton>
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
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
                    <TSPControls:CustomAspxMenuHorizontal ID="MenuDsgn" runat="server"   OnItemClick="MenuDsgn_ItemClick">
                        <Items>
                            <dxm:MenuItem Name="Member" Text="شخص حقیقی" Selected="True">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Office" Text="شخص حقوقی">
                            </dxm:MenuItem>
                        </Items>                       
                    </TSPControls:CustomAspxMenuHorizontal>
                <br />
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" DataSourceID="ObjectDataSourceEngOfficeImpQualification"
                    Width="100%"  
                    OnCustomCallback="CustomAspxDevGridView1_CustomCallback" AutoGenerateColumns="False"
                    ClientInstanceName="mygrid" KeyFieldName="EnIqId" RightToLeft="True" OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared"
                    OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize">
                    <ClientSideEvents EndCallback="function(s, e) {
            if(s.cpDoPrint==1)
            {
                s.cpDoPrint = 0;
	window.open('../../../Print.aspx');
            }
}" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="EnIqId"
                            Caption="EnIqId" Name="EnIqId">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GrdName" Caption="پایه های مهندسی"
                            Name="GrdName">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MaxFloorName" Caption="حداکثر تعداد طبقات مجاز از روی شالوده"
                            Name="MaxFloor">
                            <headerstyle wrap="True" />
                            <cellstyle wrap="False" horizontalalign="center"></cellstyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MaxJobCapacity" Caption="حداکثر ظرفیت اشتغال"
                            Name="MaxJobCapacity">
                            <cellstyle wrap="False" horizontalalign="center"></cellstyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="حداکثر تعداد واحد ساختمانی همزمان" FieldName="MaxUnitCount"
                            Name="MaxUnitCount" VisibleIndex="3">
                            <headerstyle wrap="True" />
                            <cellstyle wrap="False" horizontalalign="center"></cellstyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="CreateDate" Caption="تاریخ تعریف"
                            Name="CreateDate">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActivStatus" Name="InActivStatus"
                            VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="14" Caption=" " ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                </TSPControls:CustomAspxDevGridView>
                <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="CustomAspxDevGridView1"
                    ExportEmptyDetailGrid="True">
                </dxwgv:ASPxGridViewExporter>
                <br />
       <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
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
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال"
                                                                        ID="btnInActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnInActive_Click">
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
                                                                        <Image  Url="~/Images/icons/disactive.png">
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
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ObjectDataSource ID="ObjectDataSourceEngOfficeImpQualification" runat="server"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="TSP.DataManager.DocOffEngOfficeImpQualificationManager">
                </asp:ObjectDataSource>
</asp:Content>
