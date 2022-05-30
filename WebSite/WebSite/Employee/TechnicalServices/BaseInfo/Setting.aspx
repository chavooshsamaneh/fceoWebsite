<%@ Page Title="مدیریت تنظیمات خدمات مهندسی" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Setting.aspx.cs" Inherits="Employee_TechnicalServices_Project_Setting" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]</div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table cellpadding="0" dir="rtl">
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server"  EnableTheming="False"
                                        EnableViewState="False" CausesValidation="false" OnClick="BtnNew_Click" Text=" "
                                        ToolTip="جدید" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnEdit" runat="server"  EnableTheming="False"
                                        EnableViewState="False" CausesValidation="false" OnClick="BtnEdit_Click" Text=" "
                                        ToolTip="ویرایش" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnView" runat="server"  EnableTheming="False"
                                        EnableViewState="False" CausesValidation="false" OnClick="BtnView_Click" Text=" "
                                        ToolTip="مشاهده" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator"></TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive" runat="server" EnableClientSideAPI="True" 
                                        EnableTheming="False" EnableViewState="False" OnClick="btnInActive_Click" Text=" "
                                        ToolTip="غیر فعال" UseSubmitBehavior="False" CausesValidation="false">
                                        <ClientSideEvents Click="function(s, e) {
 if (mygrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');


}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
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
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewSetting" runat="server" AutoGenerateColumns="False"
                  DataSourceID="ObjectDataSource1"
                KeyFieldName="SettingId" ClientInstanceName="mygrid" Width="100%" OnHtmlDataCellPrepared="GridViewSetting_HtmlDataCellPrepared"
                OnAutoFilterCellEditorInitialize="GridViewSetting_AutoFilterCellEditorInitialize"
                OnCustomCallback="GridViewSetting_CustomCallback">
                <ClientSideEvents EndCallback="function(s, e) {
            if(s.cpDoPrint==1)
            {
                s.cpDoPrint = 0;
	window.open('../../../Print.aspx');
            }
}" />
                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                <Styles  >
                    <GroupPanel ForeColor="Black">
                    </GroupPanel>
                    <Header HorizontalAlign="Center">
                    </Header>
                </Styles>
                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="SettingId" VisibleIndex="0" Visible="false">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="InActive" VisibleIndex="0" Visible="false">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="حداقل زیربنا" FieldName="Foundation" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="حداقل تعداد طبقات" FieldName="Step" VisibleIndex="1">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ ایجاد" FieldName="CreateDate" VisibleIndex="2">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="3">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ تغییر وضعیت" FieldName="InActiveDate"
                        VisibleIndex="4">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="14" Caption=" " ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table cellpadding="0" dir="rtl">
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server"  EnableTheming="False"
                                        EnableViewState="False" CausesValidation="false" OnClick="BtnNew_Click" Text=" "
                                        ToolTip="جدید" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnEdit2" runat="server"  EnableTheming="False"
                                        EnableViewState="False" CausesValidation="false" OnClick="BtnEdit_Click" Text=" "
                                        ToolTip="ویرایش" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnView2" runat="server"  EnableTheming="False"
                                        EnableViewState="False" CausesValidation="false" OnClick="BtnView_Click" Text=" "
                                        ToolTip="مشاهده" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive2" runat="server" EnableClientSideAPI="True" 
                                        EnableTheming="False" EnableViewState="False" OnClick="btnInActive_Click" Text=" "
                                        ToolTip="غیر فعال" UseSubmitBehavior="False" CausesValidation="false">
                                        <ClientSideEvents Click="function(s, e) {
 if (mygrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');


}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
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
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                        <img src="../../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.TechnicalServices.SettingManager"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <TSPControls:CustomTextBox ID="btnTempExport" ClientVisible="false" ClientInstanceName="btnTempExport"
        runat="server" OnClick="btntemp_Click">
    </TSPControls:CustomTextBox>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="CustomAspxDevGridView1"
        ExportEmptyDetailGrid="True">
    </dxwgv:ASPxGridViewExporter>
</asp:Content>
