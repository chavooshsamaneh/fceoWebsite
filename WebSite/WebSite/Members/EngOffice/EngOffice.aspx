<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="EngOffice.aspx.cs" Inherits="Members_EngOffice_EngOffice"
    Title="مدیریت دفاتر" %>

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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="Content" runat="server" style="width: 100%" align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table dir="rtl" width="100%" cellpadding="0">
                                <tr>
                                    <td style="width: 27px; height: 27px;">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="BtnNew_Click" ToolTip="جدید" Text=" " UseSubmitBehavior="False" Visible="false">
                                            <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="width: 27px; height: 27px;">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnEdit_Click" ToolTip="ویرایش" Width="25px"
                                            Text=" " UseSubmitBehavior="False" Visible="false">
                                            <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="width: 27px; height: 27px;">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnView_Click" ToolTip="مشاهده" Text=" " UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator"></TSPControls:MenuSeprator>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnDelete_Click" ToolTip="لغو درخواست" Text=" "
                                            UseSubmitBehavior="False" Visible="false">
                                            <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
 e.processOnServer= confirm('آیا مطمئن به لغو کردن این درخواست هستید؟');
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="width: 27px; height: 27px;">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChange" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnChange_Click" ToolTip="درخواست تغییرات" Text=" "
                                            UseSubmitBehavior="False" Visible="false">
                                            <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/Change.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnRevival" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnRevival_Click" ToolTip="درخواست تمدید" Text=" "
                                            UseSubmitBehavior="False" Visible="false">
                                            <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/Revival.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnReduplicate" runat="server" 
                                            EnableTheming="False" EnableViewState="False" OnClick="btnReduplicate_Click"
                                            Text=" " ToolTip="درخواست المثنی" UseSubmitBehavior="False" Visible="false">
                                            <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/Copy2.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep" runat="server" AutoPostBack="False" 
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="ارسال به مرحله بعد"
                                            UseSubmitBehavior="False" Visible="false">
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	

    TextDesc.SetText('');
	CallbackPanelWorkFlow.PerformCallback('');
	PopupWorkFlow.Show();
}
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/reload.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing" runat="server" AutoPostBack="False" 
                                            EnableTheming="False" EnableViewState="False" OnClick="btnTracing_Click" Text=" "
                                            ToolTip="پیگیری جریان کار" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/Cheque Status ReChange.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="left">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnHelp" runat="server" CausesValidation="False" 
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                            Visible="true" AutoPostBack="false">
                                            <Image Height="25px" Url="~/Images/Help.png" Width="25px" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }">
                                            </ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="EngOfId" 
                     DataSourceID="OdbEngOffice" ClientInstanceName="grid" OnDetailRowExpandedChanged="CustomAspxDevGridView1_DetailRowExpandedChanged"
                    OnFocusedRowChanged="CustomAspxDevGridView1_FocusedRowChanged" OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared"
                    OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize">
                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" ColumnResizeMode="Control" />
                    <Settings ShowGroupPanel="True" ShowHorizontalScrollBar="true" ShowFilterRow="true"
                        ShowFilterRowMenu="true" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="نام دفتر" Width="170px" FieldName="EngOffName"
                            VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نوع دفتر" FieldName="OfTName" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="OfpName" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت عضو" FieldName="MeInActive" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره مشارکت نامه" FieldName="ParticipateLetterNo"
                            VisibleIndex="3">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ مشارکت نامه" FieldName="ParticipateLetterDate"
                            VisibleIndex="4">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره دفتر اسناد رسمی" FieldName="EngOffNo"
                            VisibleIndex="5">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تعداد اعضا" FieldName="MeCount" Name="MeCount"
                            VisibleIndex="6">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت دفتر" FieldName="InActiveName" VisibleIndex="7">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                            VisibleIndex="8">
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
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="10" Width="30px" ShowClearFilterButton="true">                   
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <div align="center">
                                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridViewRequest" runat="server"
                                    AutoGenerateColumns="False"  
                                    DataSourceID="OdbRequest" KeyFieldName="EOfId" OnBeforePerformDataSelect="CustomAspxDevGridViewRequest_BeforePerformDataSelect"
                                    OnHtmlDataCellPrepared="CustomAspxDevGridViewRequest_HtmlDataCellPrepared" Width="100%"
                                    OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize">
                                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                    <Columns>
                                        <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                                            VisibleIndex="0" Width="50px">
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
                                        <dxwgv:GridViewDataTextColumn Caption="نوع درخواست" FieldName="TypeName" VisibleIndex="1">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="CreateDate" VisibleIndex="2">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="وضعیت تأیید" FieldName="Confirm" VisibleIndex="3">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="درخواست دهنده" FieldName="RequesterType" VisibleIndex="4">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Width="150px" Caption="شماره پروانه اشتغال" FieldName="FileNo"
                                            VisibleIndex="5">
                                            <HeaderStyle Wrap="False" />
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="تاریخ صدور پروانه" FieldName="RegDate" VisibleIndex="6">
                                            <HeaderStyle Wrap="False" />
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان اعتبار پروانه" FieldName="ExpireDate"
                                            VisibleIndex="7">
                                            <HeaderStyle Wrap="False" />
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="شماره سریال" FieldName="SerialNo" VisibleIndex="8">
                                            <HeaderStyle Wrap="False" />
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Width="300px" Caption="وضعیت درخواست" FieldName="TaskName"
                                            VisibleIndex="9">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="10" Width="30px" ShowClearFilterButton="true">                                  
                                        </dxwgv:GridViewCommandColumn>
                                    </Columns>
                                    <Settings ShowFilterRow="True" ShowGroupPanel="True" />
                                </TSPControls:CustomAspxDevGridView>
                            </div>
                        </DetailRow>
                    </Templates>
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                </TSPControls:CustomAspxDevGridView>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table dir="rtl" width="100%" cellpadding="0">
                                <tr>
                                    <td style="width: 27px; height: 27px;">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="BtnNew_Click" ToolTip="جدید" Text=" " UseSubmitBehavior="False" Visible="false">
                                            <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="width: 27px; height: 27px;">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnEdit_Click" ToolTip="ویرایش" Width="25px"
                                            Text=" " UseSubmitBehavior="False" Visible="false">
                                            <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="width: 27px; height: 27px;">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnView_Click" ToolTip="مشاهده" Text=" " UseSubmitBehavior="False">
                                            <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnDelete_Click" ToolTip="لغو درخواست" Text=" "
                                            UseSubmitBehavior="False" Visible="false">
                                            <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
 e.processOnServer= confirm('آیا مطمئن به لغو کردن این درخواست هستید؟');
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="width: 27px; height: 27px;">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChange2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnChange_Click" ToolTip="درخواست تغییرات" Text=" "
                                            UseSubmitBehavior="False" AllowFocus="False" Visible="false">
                                            <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/Change.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnRevival2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnRevival_Click" ToolTip="درخواست تمدید" Text=" "
                                            UseSubmitBehavior="False" Visible="false">
                                            <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/Revival.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnReduplicate2" runat="server" 
                                            EnableTheming="False" EnableViewState="False" OnClick="btnReduplicate_Click"
                                            Text=" " ToolTip="درخواست المثنی" UseSubmitBehavior="False" Visible="false">
                                            <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/Copy2.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep2" runat="server" AutoPostBack="False" 
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="ارسال به مرحله بعد"
                                            UseSubmitBehavior="False" Visible="false">
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	

    TextDesc.SetText('');
	CallbackPanelWorkFlow.PerformCallback('');
	PopupWorkFlow.Show();
}
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/reload.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing2" runat="server" AutoPostBack="False" 
                                            EnableTheming="False" EnableViewState="False" OnClick="btnTracing_Click" Text=" "
                                            ToolTip="پیگیری جریان کار" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/Cheque Status ReChange.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="left">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server" CausesValidation="False" 
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                            Visible="true" AutoPostBack="false">
                                            <Image Height="25px" Url="~/Images/Help.png" Width="25px" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }">
                                            </ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ObjectDataSource ID="OdbEngOffice" runat="server" SelectMethod="SelectEngOfficeByMeId"
                    TypeName="TSP.DataManager.EngOfficeManager" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="OdbRequest" runat="server" SelectMethod="FindByEngOfficeId"
                    TypeName="TSP.DataManager.EngOffFileManager" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="" Name="EngOfId" SessionField="EngOfficeId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkId"
                    TypeName="TSP.DataManager.WorkFlowTaskManager">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="WorkFlowId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="TaskOrder" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <TSPControls:CustomASPxPopupControl ID="PopupWorkFlow" runat="server" AllowDragging="True" ClientInstanceName="PopupWorkFlow"
                    CloseAction="CloseButton"  
                    HeaderText=""  Modal="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" Width="387px">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <div dir="rtl">
                                <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelWorkFlow" runat="server" ClientInstanceName="CallbackPanelWorkFlow"
                                   OnCallback="CallbackPanelWorkFlow_Callback" Width="100%">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            <dxp:ASPxPanel ID="PanelMain" runat="server" ClientInstanceName="PanelMain" Width="100%">
                                                <PanelCollection>
                                                    <dxp:PanelContent runat="server">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <dxe:ASPxLabel runat="server" Text="ASPxLabel" Font-Size="X-Small" ID="lblError"
                                                                            ForeColor="Red" Visible="False">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="left" width="20%">
                                                                        <dxe:ASPxLabel runat="server" Text="ارسال به مرحله:" Font-Size="X-Small" ID="lblSenBack">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td valign="top" align="right" width="80%">
                                                                        <TSPControls:CustomAspxComboBox runat="server" Width="230px" 
                                                                            ID="cmbSendBackTask" RightToLeft="True"  ValueType="System.String"
                                                                            >
                                                                            <ItemStyle HorizontalAlign="Right" />
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
                                                                    <td valign="top" align="left" width="20%">
                                                                        <dxe:ASPxLabel runat="server" Text="توضیحات:" Font-Size="X-Small" ID="ASPxLabel7">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td valign="top" align="left" width="80%">
                                                                        <TSPControls:CustomASPXMemo runat="server" Height="71px"  Width="100%" ID="txtDescription"
                                                                            ClientInstanceName="TextDesc" >
                                                                        </TSPControls:CustomASPXMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 37px; text-align: center" dir="ltr" colspan="2">
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="ارسال"  Width="93px" ID="btnSendNextWorkStep"
                                                                            AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep"
                                                                            >
                                                                            <ClientSideEvents Click="function(s, e) {	
	CallbackPanelWorkFlow.PerformCallback('Send');
	grid.PerformCallback('');
}"></ClientSideEvents>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </dxp:ASPxPanel>
                                            <dxp:ASPxPanel ID="PanelSaveSuccessfully" runat="server" ClientInstanceName="PanelSaveSuccessfully"
                                                Height="100%" Width="100%">
                                                <PanelCollection>
                                                    <dxp:PanelContent runat="server">
                                                        <br />
                                                        <div align="center" width="100%">
                                                            <dxe:ASPxLabel runat="server" Text="ذخیره با موفقیت انجام شد." Font-Size="X-Small"
                                                                ID="lblInstitueWarning" ForeColor="Red">
                                                            </dxe:ASPxLabel>
                                                            <br />
                                                            <br />
                                                            <TSPControls:CustomAspxButton  runat="server" Text="خروج"  Width="93px" ID="btnClose"
                                                                AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep"
                                                                >
                                                                <ClientSideEvents Click="function(s, e) {	
	//CallbackPanelWorkFlow.PerformCallback('');
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
                            </div>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                    </HeaderStyle>
                    <SizeGripImage Height="12px" Width="12px" />
                    <CloseButtonImage Height="17px" Width="17px" />
                </TSPControls:CustomASPxPopupControl>
                <dx:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
                </dx:ASPxHiddenField>
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                    DisplayAfter="0">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
