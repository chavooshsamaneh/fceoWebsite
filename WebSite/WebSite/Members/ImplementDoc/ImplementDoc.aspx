<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ImplementDoc.aspx.cs" Inherits="Members_ImplementDoc_ImplementDoc"
    Title="مدیریت مجوز مجری حقیقی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>




<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>


<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#"><span style="color: #000000">ب</span>ستن</a>]</div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                    cellpadding="0">
                    <tbody>
                        <tr>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                    ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="BtnNew_Click">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image Height="25px" Width="25px" Url="~/Images/icons/new.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top; width: 30px">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                    Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnEdit_Click">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image Height="25px" Width="25px" Url="~/Images/icons/edit.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                    Width="25px" ID="btnView" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnView_Click">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image Height="25px" Width="25px" Url="~/Images/icons/view.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="لغو درخواست"
                                    Width="25px" ID="btnDelete" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnDelete_Click">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image Height="25px" Width="25px" Url="~/Images/icons/delete.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                          
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست صدور المثنی"
                                    CausesValidation="False" Width="25px" ID="btnReDuplicate" AutoPostBack="False"
                                    UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnReDuplicate_Click">
                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image Height="25px" Width="25px" Url="~/Images/icons/copy.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تمدید"
                                    CausesValidation="False" Width="25px" ID="btnRevival" AutoPostBack="False" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False" OnClick="btnRevival_Click">
                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image Height="25px" Width="25px" Url="~/Images/icons/Revival.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تغییرات"
                                    CausesValidation="False" Width="25px" ID="btnChange" AutoPostBack="False" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False" OnClick="btnChange_Click">
                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image Height="25px" Width="25px" Url="~/Images/icons/Change.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گردش کار"
                                    ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
	txtDescription.SetText('');
	CallbackPanelWorkFlow.PerformCallback('');
	PopupWorkFlow.Show();
}
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image Height="25px" Width="25px" Url="~/Images/icons/reload.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="پیگیری"
                                    CausesValidation="False" ID="btnTracing" AutoPostBack="False" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False" OnClick="btnTracing_Click">
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image Height="25px" Width="25px" Url="~/Images/icons/Cheque Status ReChange.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnprint" runat="server" AutoPostBack="False" 
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../Print.aspx&quot;);	
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/printers.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="width: 27px; height: 27px">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                    ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnExportExcel_Click">
                                    <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image Height="25px" Width="25px" Url="~/Images/icons/ExportExcel.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="width: 100%" align="left" valign="top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="راهنما"
                                    CausesValidation="False" ID="btnHelp" AutoPostBack="False" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False">
                                    <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }">
                                    </ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image Width="25px" Height="25px" Url="~/Images/Help.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />

        <ul class="HelpUL">
       <li> در صورتی که عضو دفتر و یا شرکت مهندسی باشید قادر به دریافت مجوز اجرا نمی باشید</li></ul>
   
    <TSPControls:CustomAspxDevGridView ID="GridViewMemberFile" runat="server" DataSourceID="ObjdsMemberFile"
        Width="100%" 
        KeyFieldName="MfId" AutoGenerateColumns="False" ClientInstanceName="GridViewMemberFile"
        OnAutoFilterCellEditorInitialize="GridViewMemberFile_AutoFilterCellEditorInitialize"
        OnHtmlDataCellPrepared="GridViewMemberFile_HtmlDataCellPrepared" OnDetailRowExpandedChanged="GridViewMemberFile_DetailRowExpandedChanged"
        OnFocusedRowChanged="GridViewMemberFile_FocusedRowChanged">
        <Columns>
            <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                VisibleIndex="0" Width="40px">
                <DataItemTemplate>
                    <div align="center">
                        <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl="~/Images/WFStart.png">
                        </dxe:ASPxImage>
                    </div>
                </DataItemTemplate>
                <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                    ValueType="System.String">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="RequeterTypeName" Caption="ثبت کننده درخواست"
                Width="100px">
                <HeaderStyle Wrap="True"></HeaderStyle>
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="SerialNo" Caption="شماره سریال">
                <HeaderStyle Wrap="True"></HeaderStyle>
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MFNo" Caption="شماره مجوز">
                <HeaderStyle Wrap="True"></HeaderStyle>
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="RegDate" Caption="تاریخ تمدید">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ExpireDate" Caption="پایان اعتبار">
                <HeaderStyle Wrap="True"></HeaderStyle>
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="MFType" Caption="وضعیت پروانه">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="InActives" Caption="وضعیت"
                Width="80px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn VisibleIndex="7" Width="30px" Caption=" " ShowClearFilterButton="true">
           
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowHorizontalScrollBar="True"></Settings>
        <SettingsDetail ExportMode="All"></SettingsDetail>
    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table >
                    <tbody>
                        <tr>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                    ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False">
                                   
                                    <Image Height="25px" Width="25px" Url="~/Images/icons/new.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                    Width="25px" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnEdit_Click">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                  
                                    <Image Height="25px" Width="25px" Url="~/Images/icons/edit.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                    Width="25px" ID="btnView2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                    
                                    <Image Height="25px" Width="25px" Url="~/Images/icons/view.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="لغو درخواست"
                                    Width="25px" ID="btnDelete2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnDelete_Click">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                 
                                    <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست صدور المثنی"
                                    CausesValidation="False" Width="25px" ID="btnReDuplicate2" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False" OnClick="btnReDuplicate_Click">
                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
}"></ClientSideEvents>
                                 
                                    <Image  Url="~/Images/icons/copy.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تمدید"
                                    CausesValidation="False" Width="25px" ID="btnRevival2" AutoPostBack="False" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False" OnClick="btnRevival_Click">
                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
}"></ClientSideEvents>
                                   
                                    <Image Height="25px" Width="25px" Url="~/Images/icons/Revival.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تغییرات"
                                    CausesValidation="False" Width="25px" ID="btnChange2" AutoPostBack="False" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False" OnClick="btnChange_Click">
                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
}"></ClientSideEvents>
                               
                                    <Image  Url="~/Images/icons/Change.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گردش کار"
                                    ID="btnSendNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
	txtDescription.SetText('');
	CallbackPanelWorkFlow.PerformCallback('');
	PopupWorkFlow.Show();
}
}"></ClientSideEvents>
                                   
                                    <Image Url="~/Images/icons/reload.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="پیگیری"
                                    CausesValidation="False" ID="btnTracing2" AutoPostBack="False" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False" OnClick="btnTracing_Click">
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                  
                                    <Image Url="~/Images/icons/Cheque Status ReChange.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnprint2" runat="server" AutoPostBack="False" 
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../Print.aspx&quot;);	
}" />
                                 
                                    <Image  Url="~/Images/icons/printers.png" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                    ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnExportExcel_Click">
                                    <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                    
                                    <Image  Url="~/Images/icons/ExportExcel.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                              <td style="width: 100%" align="left" valign="top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="راهنما"
                                    CausesValidation="False" ID="btnHelp2" AutoPostBack="False" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False">
                                    <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }">
                                    </ClientSideEvents>
                                   
                                    <Image  Url="~/Images/Help.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewMemberFile">
    </dxwgv:ASPxGridViewExporter>

    <asp:ObjectDataSource ID="ObjdsMemberFile" runat="server" SelectMethod="SelectImpDocSubRequest"
        TypeName="TSP.DataManager.DocMemberFileManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="MeId"></asp:Parameter>
            <asp:Parameter DefaultValue="-1" Name="TaskCode" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
        TypeName="TSP.DataManager.WorkFlowTaskManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <TSPControls:CustomASPxPopupControl ID="PopupWorkFlow" runat="server" Width="387px" CssPostfix="Glass"
        CssFilePath="~/App_Themes/Glass/{0}/styles.css" ClientInstanceName="PopupWorkFlow"
        AllowDragging="True" CloseAction="CloseButton" HeaderText="" ImageFolder="~/App_Themes/Glass/{0}/"
        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <TSPControls:CustomAspxCallbackPanel runat="server"  Width="100%"
                    ID="CallbackPanelWorkFlow" ClientInstanceName="CallbackPanelWorkFlow" OnCallback="CallbackPanelWorkFlow_Callback">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxp:ASPxPanel runat="server" Width="100%" ID="PanelMain" ClientInstanceName="PanelMain">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <dxe:ASPxLabel runat="server" Text="ASPxLabel" Font-Size="X-Small" ID="lblError"
                                                            ForeColor="Red" Visible="False">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="ارسال به مرحله" Font-Size="X-Small" Width="77px"
                                                            ID="lblSenBack">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td dir="ltr" valign="top" align="right">
                                                        <TSPControls:CustomAspxComboBox runat="server" CssPostfix="Glass" Width="290px" ImageFolder="~/App_Themes/Glass/{0}/"
                                                            ID="cmbSendBackTask" ValueType="System.String" CssFilePath="~/App_Themes/Glass/{0}/styles.css">
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <ButtonStyle Width="13px">
                                                            </ButtonStyle>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="توضیحات" Font-Size="X-Small" Width="56px" ID="ASPxLabel1">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td dir="rtl" valign="top" align="right">
                                                        <dxe:ASPxMemo runat="server" Height="71px" ID="txtDescription" CssPostfix="Glass"
                                                            Width="100%" ClientInstanceName="txtDescription" CssFilePath="~/App_Themes/Glass/{0}/styles.css">
                                                            <ValidationSettings>
                                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </dxe:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td dir="ltr" valign="top" align="center" colspan="2">
                                                        <TSPControls:CustomAspxButton runat="server" Text="ارسال" CssPostfix="Glass" Width="93px" ID="btnSendNextWorkStep"
                                                            AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep"
                                                            CssFilePath="~/App_Themes/Glass/{0}/styles.css">
                                                            <ClientSideEvents Click="function(s, e) {	
	CallbackPanelWorkFlow.PerformCallback('Send');
	GridViewMemberFile.PerformCallback('');
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxp:ASPxPanel>
                            <dxp:ASPxPanel runat="server" Height="100%" Width="100%" ID="PanelSaveSuccessfully"
                                ClientInstanceName="PanelSaveSuccessfully">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <div align="center">
                                            <br />
                                            <dxe:ASPxLabel runat="server" Text="ذخیره با موفقیت انجام شد." Font-Size="X-Small"
                                                ID="lblInstitueWarning" ForeColor="Red">
                                            </dxe:ASPxLabel>
                                            <br />
                                            <br />
                                            <TSPControls:CustomAspxButton runat="server" Text="خروج" CssPostfix="Glass" Width="93px" ID="btnClose"
                                                AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep"
                                                CssFilePath="~/App_Themes/Glass/{0}/styles.css">
                                                <ClientSideEvents Click="function(s, e) {	
	GridViewMemberFile.PerformCallback('');
	PopupWorkFlow.Hide();
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </div>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxp:ASPxPanel>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomAspxCallbackPanel>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
        <HeaderStyle>
            <Paddings PaddingTop="1px" PaddingRight="6px" PaddingLeft="10px"></Paddings>
        </HeaderStyle>
        <SizeGripImage Height="12px" Width="12px">
        </SizeGripImage>
        <CloseButtonImage Height="17px" Width="17px">
        </CloseButtonImage>
    </TSPControls:CustomASPxPopupControl>
    <dxhf:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
    </dxhf:ASPxHiddenField>
</asp:Content>
