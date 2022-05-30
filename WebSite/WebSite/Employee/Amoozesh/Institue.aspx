<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPagePortals.master"
    CodeFile="Institue.aspx.cs" Inherits="Employee_Amoozesh_Institue" Title="مدیریت مؤ سسه ها" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="Content" runat="server" style="width: 100%" align="center">
        <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
            [<a class="closeLink" href="#">بستن</a>]</div>
        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>
                                    <table >
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                        EnableTheming="False" ToolTip="جدید" ID="BtnNew" EnableViewState="False" OnClick="BtnNew_Click">
                                                        <Image  Url="~/Images/icons/new.png">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                        Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit" EnableViewState="False"
                                                        OnClick="btnEdit_Click">
                                                        <Image  Url="~/Images/icons/edit.png">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                        EnableTheming="False" ToolTip="مشاهده" ID="btnView" EnableViewState="False" OnClick="btnView_Click">
                                                        <Image  Url="~/Images/icons/view.png">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                                    </TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChangeCertificate" runat="server" AutoPostBack="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnChangeCertificate_Click"
                                                        Text=" " ToolTip="درخواست تغییرات" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewInstitue.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/Change.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnRevival" runat="server" AutoPostBack="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnRevival_Click" Text=" "
                                                        ToolTip="درخواست تمدید" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewInstitue.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/Revival.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="لغو درخواست"
                                                        Width="25px" ID="btnDeleteReq2" OnClick="btnDeleteReq_Click" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) {
if (GridViewInstitue.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{
    e.processOnServer=confirm('آیا مطمئن به لغو درخواست انتخاب شده هستید؟')		
	}
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                  <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیر فعال"
                                                        Width="25px" ID="btnInActive" OnClick="btnInActive_Click" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) {
if (GridViewInstitue.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{
    e.processOnServer=confirm('آیا مطمئن به غیرفعال کردن موسسه انتخاب شده هستید؟')		
	}
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                 <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="فعال"
                                                        Width="25px" ID="btnActive" OnClick="btnActive_Click" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) {
if (GridViewInstitue.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{
    e.processOnServer=confirm('آیا مطمئن به فعال کردن موسسه انتخاب شده هستید؟')		
	}
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/Active.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                                    </TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnReset" runat="server" CausesValidation="False" 
                                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="بازیابی رمز عبور"
                                                        UseSubmitBehavior="False" OnClick="btnReset_Click">
                                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewInstitue.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/ChangePassword.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2">
                                                    </TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep" runat="server" AutoPostBack="False" 
                                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewInstitue.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
    ShowWf();
}
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/reload.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing" runat="server" AutoPostBack="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnTracing_Click" Text=" "
                                                        ToolTip="پیگیری" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewInstitue.GetFocusedRowIndex()&lt;0)
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
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3">
                                                    </TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                                        ID="btnPrint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False">
                                                        <ClientSideEvents Click="function(s,e){GridViewInstitue.PerformCallback('Print'); }">
                                                        </ClientSideEvents>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/Printers.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                                        ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                        OnClick="btnExportExcel_Click">
                                                        <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/ExportExcel.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                              
                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanelMenu>
        <dx:ASPxGridViewExporter ID="GridViewExporter" GridViewID="GridViewInstitue" runat="server">
        </dx:ASPxGridViewExporter>
        <br />
        <TSPControls:CustomAspxCallbackPanel ID="CallbackPanel" runat="server" ClientInstanceName="CallbackPanel"
            OnCallback="CallbackPanel_Callback" Width="100%" >
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <table width="100%">
                                    <tr>
                                        <td align="right" valign="top" width="15%">
                                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="نام موسسه">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top" width="35%">
                                            <TSPControls:CustomTextBox IsMenuButton="true" ID="txtInsName" runat="server" ClientInstanceName="txtInsName" 
                                                  Width="100%">
                                                <ValidationSettings>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td align="right" valign="top" width="15%">
                                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="شماره ثبت">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top" width="35%">
                                            <TSPControls:CustomTextBox IsMenuButton="true" ID="txtRegNo" runat="server" ClientInstanceName="txtRegNo" 
                                                  Width="100%">
                                                <ValidationSettings>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="تاریخ اعتبار تا">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right"  valign="top">
                                            <pdc:PersianDateTextBox ID="txtEndDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                                Width="244px"></pdc:PersianDateTextBox>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="تاریخ اعتبار از">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <pdc:PersianDateTextBox ID="txtEndDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                                Width="245px"></pdc:PersianDateTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4" dir="ltr" valign="top">
                                            <br />
                                            <table>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton  ID="ASPxButton1" runat="server" 
                                                              Text="پاک کردن فرم"
                                                            AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
		CallbackPanel.PerformCallback('Clear');
		GridViewInstitue.PerformCallback('Clear');
}" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton  ID="btnSearch" runat="server" 
                                                              Text="جستجو"
                                                            Width="98px" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
	GridViewInstitue.PerformCallback('Search');
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
                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomAspxCallbackPanel>
        <br />
        <TSPControls:CustomAspxDevGridView ID="GridViewInstitue" runat="server" Width="100%"
            KeyFieldName="InsId" DataSourceID="OdbInstitue" AutoGenerateColumns="False" ClientInstanceName="GridViewInstitue"
              EnableViewState="False"
            OnCustomCallback="GridViewInstitue_CustomCallback" OnDetailRowExpandedChanged="GridViewInstitue_DetailRowExpandedChanged"
            OnHtmlRowPrepared="GridViewInstitue_HtmlRowPrepared" OnHtmlDataCellPrepared="GridViewInstitue_HtmlDataCellPrepared"
            RightToLeft="True">
            <Columns>
                <dxwgv:GridViewDataComboBoxColumn Caption=" " FieldName="IsExpired" Name="IsExpired"
                    VisibleIndex="0" Width="30px">
                    <PropertiesComboBox ValueType="System.String">
                        <Items>
                            <dxe:ListEditItem Text="پایان اعتبار" Value="1"></dxe:ListEditItem>
                            <dxe:ListEditItem Text="دارای اعتبار" Value="0"></dxe:ListEditItem>
                        </Items>
                    </PropertiesComboBox>
                    <DataItemTemplate>
                        <div align="center">
                            <dxe:ASPxImage ID="btnIsExpired" runat="server" Width="16px" ImageUrl="~/Images/CertificateValid.png"
                                Height="16px" __designer:wfdid="w4">
                            </dxe:ASPxImage>
                        </div>
                    </DataItemTemplate>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                    VisibleIndex="0">
                    <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                        ValueType="System.String">
                    </PropertiesComboBox>
                    <DataItemTemplate>
                        <div align="center">
                            <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" ImageUrl="~/Images/WFStart.png"
                                Height="16px">
                            </dxe:ASPxImage>
                        </div>
                    </DataItemTemplate>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataTextColumn Caption="نام مؤسسه" FieldName="InsName" VisibleIndex="0">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                
                <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="نام مدیر مؤسسه" FieldName="Manager" VisibleIndex="2">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="شماره تلفن1 " FieldName="Tel1" VisibleIndex="3">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="شماره تلفن2" FieldName="Tel2" VisibleIndex="4">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="شماره همراه" FieldName="MobileNo" VisibleIndex="5">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="تاریخ اعتبار" FieldName="EndDate" Name="EndDate"
                    VisibleIndex="7">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="9" Width="30px" ShowClearFilterButton="true">
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn Caption="نام کاربری" FieldName="UserName" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <Settings ShowHorizontalScrollBar="True" />
            <Templates>
                <DetailRow>
                    <TSPControls:CustomAspxDevGridView ID="GridViewInsCertificate" runat="server" AutoGenerateColumns="False"
                          DataSourceID="ObjdsInsCertificate"
                        KeyFieldName="InsCId" OnBeforePerformDataSelect="CustomAspxDevGridView1_BeforePerformDataSelect"
                        OnHtmlDataCellPrepared="GridViewInsCertificate_HtmlDataCellPrepared" Width="100%"
                        OnAutoFilterCellEditorInitialize="GridViewInsCertificate_AutoFilterCellEditorInitialize">
                        <Columns>
                            
                            <dxwgv:GridViewDataTextColumn Caption="تاریخ ثبت درخواست" FieldName="Date" VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="شماره مجوز" FieldName="FileNo" VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="شماره سریال" FieldName="SerialNo" VisibleIndex="1">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="تاریخ صدور" FieldName="StartDate" VisibleIndex="2">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان اعتبار" FieldName="EndDate" VisibleIndex="3">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نوع درخواست" FieldName="CrtType" VisibleIndex="4">
                                <HeaderStyle Wrap="True" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="وضعیت پروانه" FieldName="IsConf" VisibleIndex="5">
                                <HeaderStyle Wrap="True" />
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="6" ShowClearFilterButton="true">
                            </dxwgv:GridViewCommandColumn>
                        </Columns>
                        <Settings ShowFilterRow="True" ShowGroupPanel="True" />
                        <SettingsDetail IsDetailGrid="True" />
                    </TSPControls:CustomAspxDevGridView>
                </DetailRow>
            </Templates>
            <SettingsDetail ExportMode="All" AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
            <ClientSideEvents FocusedRowChanged="function(s, e) {
  if(s.cpPrint==0)               
    GridViewInstitue.ExpandDetailRow(GridViewInstitue.GetFocusedRowIndex());
}" EndCallback="function(s, e) {
	 if(s.cpPrint==1)
        {
            window.open('../../Print.aspx');
           s.cpPrint=0;
        }
}" />
            <Images >
            </Images>
        </TSPControls:CustomAspxDevGridView>
        <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewInstitue"
            SessionName="SendBackDataTable_Insetitue" OnCallback="WFUserControl_Callback" />
        <br />
        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>
                                    <table >
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                        EnableTheming="False" ToolTip="جدید" ID="BtnNew2" EnableViewState="False" OnClick="BtnNew_Click">
                                                        <Image  Url="~/Images/icons/new.png">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                        Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit2" EnableViewState="False"
                                                        OnClick="btnEdit_Click">
                                                        <Image  Url="~/Images/icons/edit.png">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                        EnableTheming="False" ToolTip="مشاهده" ID="btnView2" EnableViewState="False"
                                                        OnClick="btnView_Click">
                                                        <Image  Url="~/Images/icons/view.png">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4">
                                                    </TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChangeCertificate2" runat="server" AutoPostBack="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnChangeCertificate_Click"
                                                        Text=" " ToolTip="درخواست تغییرات" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewInstitue.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/Change.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnRevival2" runat="server" AutoPostBack="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnRevival_Click" Text=" "
                                                        ToolTip="درخواست تمدید" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewInstitue.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/Revival.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="لغو درخواست"
                                                        Width="25px" ID="ASPxButton2" OnClick="btnDeleteReq_Click" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) {
if (GridViewInstitue.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{
    e.processOnServer=confirm('آیا مطمئن به لغو درخواست انتخاب شده هستید؟')		
	}
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                 <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیر فعال"
                                                        Width="25px" ID="btnInActive2" OnClick="btnInActive_Click" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) {
if (GridViewInstitue.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{
    e.processOnServer=confirm('آیا مطمئن به غیرفعال کردن موسسه انتخاب شده هستید؟')		
	}
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                 <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="فعال"
                                                        Width="25px" ID="btnActive2" OnClick="btnActive_Click" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) {
if (GridViewInstitue.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{
    e.processOnServer=confirm('آیا مطمئن به فعال کردن موسسه انتخاب شده هستید؟')		
	}
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/Active.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5">
                                                    </TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnReset1" runat="server" CausesValidation="False" 
                                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="بازیابی رمز عبور"
                                                        UseSubmitBehavior="False" OnClick="btnReset_Click">
                                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewInstitue.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/ChangePassword.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6">
                                                    </TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep2" runat="server" AutoPostBack="False" 
                                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewInstitue.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	ShowWf();
}
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/reload.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing2" runat="server" AutoPostBack="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnTracing_Click" Text=" "
                                                        ToolTip="پیگیری" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewInstitue.GetFocusedRowIndex()&lt;0)
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
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator7">
                                                    </TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                                        ID="btnPrint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False">
                                                        <ClientSideEvents Click="function(s,e){GridViewInstitue.PerformCallback('Print'); }">
                                                        </ClientSideEvents>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/Printers.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                                        ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                        OnClick="btnExportExcel_Click">
                                                        <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/ExportExcel.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                               
                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanelMenu>
        <asp:ObjectDataSource ID="OdbInstitue" runat="server" TypeName="TSP.DataManager.InstitueManager"
            SelectMethod="SelectInstitue" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter DefaultValue="1" Name="EndDateFrom" Type="String" />
                <asp:Parameter DefaultValue="2" Name="EndDateTo" Type="String" />
                <asp:Parameter DefaultValue="%" Name="InsRegNo" Type="String" />
                <asp:Parameter DefaultValue="%" Name="InsName" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
            TypeName="TSP.DataManager.WorkFlowTaskManager">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjdsInsCertificate" runat="server" SelectMethod="SelectByInstitue"
            TypeName="TSP.DataManager.InstitueCertificateManager">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="-1" Name="InsId" SessionField="InsId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>
