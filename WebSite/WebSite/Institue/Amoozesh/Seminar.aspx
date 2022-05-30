<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Seminar.aspx.cs" Inherits="Institue_Amoozesh_Seminar"
    Title="مدیریت سمینارها" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>


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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="DivReport" runat="server" class="DivErrors" dir="rtl" style="text-align: right"
        visible="true">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                    <tbody>
                        <tr>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False" 
                                    EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                    ToolTip="جدید">
                                    <ClientSideEvents Click="function(s, e) {
	
	
	
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" CausesValidation="False" 
                                    EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                    ToolTip="ویرایش">
                                    <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" CausesValidation="False" 
                                    EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                    ToolTip="مشاهده">
                                    <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="لغو درخواست"
                                    Width="25px" ID="btnDelete2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" CausesValidation="False" OnClick="btnDelete_Click">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                    <ClientSideEvents Click="function(s, e) {
if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{
    if(!confirm('آیا مطمئن به لغو درخواست انتخاب شده هستید؟'))
			e.processOnServer=false;
	}
}" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChange" runat="server"  EnableTheming="False"
                                    EnableViewState="False" OnClick="btnChange_Click" Text=" " ToolTip="درخواست تغییرات"
                                    UseSubmitBehavior="False">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/ChangeIns.png" Width="25px" />
                                    <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep" runat="server" AutoPostBack="False" CausesValidation="False"
                                     EnableTheming="False" EnableViewState="False"
                                    Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
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
                                    ToolTip="پیگیری" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
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
                        </tr>
                    </tbody>
                </table>

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSPControls:CustomAspxDevGridView ID="GridViewSeminar" runat="server" AutoGenerateColumns="False"
        DataSourceID="ObjectDataSource1" ClientInstanceName="gridview" KeyFieldName="SeId"
        Width="100%" OnHtmlDataCellPrepared="GridViewSeminar_HtmlDataCellPrepared"
        OnAutoFilterCellEditorInitialize="GridViewSeminar_AutoFilterCellEditorInitialize">
        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
        <SettingsDetail ExportMode="None" ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
        <Settings ShowHorizontalScrollBar="true" />
        <Columns>
            <dxwgv:GridViewDataTextColumn FieldName="SeId" Name="SeId" Visible="False" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="موضوع سمینار" FieldName="Subject" VisibleIndex="0"
                Width="250px">
                <CellStyle HorizontalAlign="Right" Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="StartDate" VisibleIndex="1"
                Width="80px">
                <CellStyle HorizontalAlign="Right" Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان" FieldName="EndDate" VisibleIndex="2"
                Width="80px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ساعت برگزاری" FieldName="Time" VisibleIndex="3">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                VisibleIndex="4">
                <DataItemTemplate>
                    <div align="center">
                        <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl="~/Images/WFStart.png">
                        </dxe:ASPxImage>
                    </div>
                </DataItemTemplate>
                <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                    EnableIncrementalFiltering="True" ValueType="System.String">
                </PropertiesComboBox>
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="5" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Templates>
            <DetailRow>
                <TSPControls:CustomAspxDevGridView runat="server"  EnableViewState="False"
                    DataSourceID="ObjectDataSourceRequest" RightToLeft="True" ID="GridViewRequest"
                    KeyFieldName="SeReqId" AutoGenerateColumns="False" ClientInstanceName="GridViewRequest"
                     Width="100%" OnBeforePerformDataSelect="GridViewRequest_BeforePerformDataSelect"
                    OnHtmlDataCellPrepared="GridViewRequest_HtmlDataCellPrepared">
                    <Settings ShowHorizontalScrollBar="true"></Settings>
                    <Columns>
                        <dxwgv:GridViewDataComboBoxColumn Width="50px" FieldName="TaskId" Caption="مرحله"
                            Name="WFState" VisibleIndex="0">
                            <PropertiesComboBox ValueType="System.String" TextField="TaskName" DataSourceID="ObjdsWorkFlowTask"
                                ValueField="TaskId">
                            </PropertiesComboBox>
                            <DataItemTemplate>
                                <div align="center">
                                    <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl="~/Images/WFStart.png">
                                    </dxe:ASPxImage>
                                </div>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="TypeName"
                            Caption="نام درخواست" Name="TypeName">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" Width="100px" FieldName="CreateDate"
                            Caption="تاریخ درخواست" Name="Date">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" Width="80px" FieldName="IsConfirmName"
                            Caption="وضعیت" Name="IsConfirmName">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" Width="150px" FieldName="RequesterName"
                            Caption="نام ارسال کننده" Name="RequesterName">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" Width="150px" FieldName="WFRequesterType"
                            Caption="سمت ارسال کننده" Name="WFRequesterType">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="50px" ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>

                </TSPControls:CustomAspxDevGridView>
            </DetailRow>
        </Templates>
    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                    width="100%">
                    <tbody>
                        <tr>
                            <td align="right">
                                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                    <tbody>
                                        <tr>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew1" runat="server" CausesValidation="False" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                                    ToolTip="جدید">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" CausesValidation="False" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                                    ToolTip="ویرایش">
                                                    <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView1" runat="server" CausesValidation="False" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                                    ToolTip="مشاهده">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                    <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="لغو درخواست"
                                                    Width="25px" ID="btnDelete" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False" OnClick="btnDelete_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                    <ClientSideEvents Click="function(s, e) {
if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{
    if(!confirm('آیا مطمئن به لغو درخواست انتخاب شده هستید؟'))
			e.processOnServer=false;
	}
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChange2" runat="server"  EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnChange_Click" Text=" " ToolTip="درخواست تغییرات"
                                                    UseSubmitBehavior="False">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/ChangeIns.png" Width="25px" />
                                                    <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep1" runat="server" AutoPostBack="False" CausesValidation="False"
                                                     EnableTheming="False" EnableViewState="False"
                                                    Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
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
                                                    ToolTip="پیگیری" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
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
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:HiddenField ID="InstitueId" runat="server" Visible="False" />
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" FilterExpression="InsId={0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.SeminarManager" OldValuesParameterFormatString="original_{0}">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceRequest" runat="server" SelectMethod="FindBySeminarId"
        TypeName="TSP.DataManager.SeminarRequestManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="-1" Name="SeId" SessionField="SeId" Type="Int32" />
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
        PopupVerticalAlign="WindowCenter" Width="500px" RightToLeft="True">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelWorkFlow" runat="server" ClientInstanceName="CallbackPanelWorkFlow"
                    OnCallback="CallbackPanelWorkFlow_Callback" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <dxp:ASPxPanel ID="PanelMain" runat="server" ClientInstanceName="PanelMain" Width="100%">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td colspan="2">
                                                        <dxe:ASPxLabel runat="server" Text="ASPxLabel" Font-Size="X-Small" ID="lblError"
                                                            ForeColor="Red" Visible="False">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="top"  width="20%" >
                                                        <dxe:ASPxLabel runat="server" Text="ارسال به مرحله:" Font-Size="X-Small" ID="lblSenBack">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td align="right"   width="80%">
                                                        <TSPControls:CustomAspxComboBox runat="server"  ID="cmbSendBackTask"
                                                             ValueType="System.String"  RightToLeft="True" Width="100%">
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <ButtonStyle Width="13px">
                                                            </ButtonStyle>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="توضیحات:" Font-Size="X-Small" Width="56px" ID="ASPxLabel1">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td dir="rtl">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="71px"  Width="100%" ID="txtDescription"
                                                            >
                                                            <ValidationSettings>
                                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td  align="center"  colspan="2">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="ارسال"  Width="93px" ID="btnSendNextWorkStep"
                                                            AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep"
                                                            >
                                                            <ClientSideEvents Click="function(s, e) {	
	CallbackPanelWorkFlow.PerformCallback('Send');
	gridview.PerformCallback('');
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
                                        <div align="center">
                                            <br />
                                            <dxe:ASPxLabel runat="server" Text="ذخیره با موفقیت انجام شد." Font-Size="X-Small"
                                                ID="lblPeriodWarning" ForeColor="Red" __designer:wfdid="w24">
                                            </dxe:ASPxLabel>
                                            <br />
                                            <br />
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="خروج"  Width="93px" ID="btnClose"
                                                AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep"
                                                 __designer:wfdid="w25">
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
            </dxpc:PopupControlContentControl>
        </ContentCollection>
        <HeaderStyle>
            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
        </HeaderStyle>
        <SizeGripImage Height="12px" Width="12px" />
        <CloseButtonImage Height="17px" Width="17px" />
    </TSPControls:CustomASPxPopupControl>

</asp:Content>
