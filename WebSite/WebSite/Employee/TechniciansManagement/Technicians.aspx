<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Technicians.aspx.cs" Inherits="Employee_TechniciansManagement_Technicians"
    Title="مدیریت کاردان ها و معماران تجربی" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Content" runat="server" style="width: 100%; display: block" align="center">
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <%-- <asp:updatepanel id="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
            [<a class="closeLink" href="#">بستن</a>]
        </div>
        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>
                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                        cellpadding="0">
                        <tbody>
                            <tr>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                        ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        OnClick="BtnNew_Click">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/new.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                        Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        OnClick="btnEdit_Click">
                                        <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/edit.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                        ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        OnClick="btnView_Click">
                                        <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/view.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server"  EnableTheming="False"
                                        EnableViewState="False" AutoPostBack="false" Text=" " ToolTip="حذف درخواست" OnClick="btnDelete_Click"
                                        UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
{
  if(!confirm('آیا مطمئن به حذف این درخواست هستید؟'))
  {
        e.processOnServer=false;
        //CallbackPanelPage.PerformCallback('btnDelete');
  }
}
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیر فعال"
                                        ID="btnActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnActive_Click">
                                        <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
//else
	 //e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/disactive.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تغییرات"
                                        ID="btnChangeReq" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnChangeReq_Click">
                                        <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/Change.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گردش کار"
                                        ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False">
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
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/reload.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td style="width: 30px">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="پیگیری"
                                        ID="btnTracing" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnTracing_Click">
                                        <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/Cheque Status ReChange.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                        ID="btnPrint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False">
                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	grid.PerformCallback('Print');
	//window.open(&quot;../../Print.aspx&quot;);	
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/printers.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px" 
                                        ToolTip="خروجی Excel" CausesValidation="False" ID="btnExportExcel" EnableClientSideAPI="True"
                                        UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnExportExcel"
                                        AutoPostBack="false" OnClick="btnExportExcel_Click">
                                        <ClientSideEvents Click="function(s,e){ }" />
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

        <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewTechnician">
        </dxwgv:ASPxGridViewExporter>
        <TSPControls:CustomAspxDevGridView ID="GridViewTechnician" runat="server" DataSourceID="ObjdsOtherPerson"
            Width="100%"  
            OnHtmlDataCellPrepared="GridViewTechnician_HtmlDataCellPrepared" OnDataBinding="GridViewTechnician_DataBinding"
            ClientInstanceName="grid" AutoGenerateColumns="False" KeyFieldName="OtpId" OnHtmlRowPrepared="GridViewTechnician_HtmlRowPrepared"
            OnCustomCallback="GridViewTechnician_CustomCallback"
            OnAutoFilterCellEditorInitialize="GridViewTechnician_AutoFilterCellEditorInitialize">
            <Templates>
                <DetailRow>
                    <TSPControls:CustomAspxDevGridView ID="GridViewTechnicianRequest" runat="server"
                        DataSourceID="ObjdsTechnicianRequest" Width="100%"  
                        OnHtmlDataCellPrepared="GridViewTechnicianRequest_HtmlDataCellPrepared" ClientInstanceName="gridReq"
                        AutoGenerateColumns="False" KeyFieldName="TnReId" OnBeforePerformDataSelect="GridViewTechnicianRequest_BeforePerformDataSelect"
                        OnAutoFilterCellEditorInitialize="GridViewTechnicianRequest_AutoFilterCellEditorInitialize">
                        <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                        <Columns>
                            <dxwgv:GridViewDataComboBoxColumn FieldName="TaskId" Caption="مرحله" Name="WFState"
                                VisibleIndex="0">
                                <DataItemTemplate>
                                    <div align="center">
                                        <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl="~/Images/WFStart.png">
                                        </dxe:ASPxImage>
                                    </div>
                                </DataItemTemplate>
                                <PropertiesComboBox ValueType="System.String" TextField="TaskName" DataSourceID="ObjdsWorkFlowTask"
                                    ValueField="TaskId">
                                </PropertiesComboBox>
                            </dxwgv:GridViewDataComboBoxColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="OtpCode" Caption="کد کانون کاردان ها">
                                <HeaderStyle Wrap="True"></HeaderStyle>
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FirstName" Caption="نام">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="LastName" Caption="نام خانوادگی">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataComboBoxColumn FieldName="MjId" Name="MjId" Caption="رشته" VisibleIndex="3">
                                <PropertiesComboBox ValueType="System.String" TextField="MjName" DataSourceID="ObjdsMajor"
                                    ValueField="MjId">
                                </PropertiesComboBox>
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataComboBoxColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="FileNo" Caption="شماره پروانه">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="StatusType" Caption="نوع درخواست">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>

                            <dxwgv:GridViewCommandColumn VisibleIndex="7" Caption=" " ShowClearFilterButton="true">
                            
                            </dxwgv:GridViewCommandColumn>
                        </Columns>
                        <Settings ShowGroupPanel="True" ShowFilterRowMenu="True" ShowFilterRow="True"></Settings>
                        <SettingsDetail IsDetailGrid="True"></SettingsDetail>
                    </TSPControls:CustomAspxDevGridView>
                </DetailRow>
            </Templates>
            <Columns>
                <dxwgv:GridViewDataComboBoxColumn FieldName="TaskId" Caption="مرحله" Name="WFState"
                    VisibleIndex="0">
                    <DataItemTemplate>
                        <div align="center">
                            <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl="~/Images/WFStart.png">
                            </dxe:ASPxImage>
                        </div>
                    </DataItemTemplate>
                    <PropertiesComboBox ValueType="System.String" TextField="TaskName" DataSourceID="ObjdsWorkFlowTask"
                        ValueField="TaskId">
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="OtpId"
                    Caption="کد شناسایی" Name="OtpId">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="OtpCode" Caption="کد کانون کاردان ها">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="TypeName" Caption="نوع">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="FirstName" Caption="نام">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="LastName" Caption="نام خانوادگی">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="SSN" Caption="کد ملی">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="4" FieldName="BirthDate"
                    Caption="تاریخ تولد">
                    <HeaderStyle Wrap="False"></HeaderStyle>
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="6" Visible="False" FieldName="MobileNo"
                    Caption="شماره همراه">
                    <HeaderStyle Wrap="False"></HeaderStyle>
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="MjId" Name="MjId" Caption="رشته" VisibleIndex="5">
                    <PropertiesComboBox ValueType="System.String" TextField="MjName" DataSourceID="ObjdsMajor"
                        ValueField="MjId">
                    </PropertiesComboBox>
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="MjName" Caption="عنوان رشته">
                    <CellStyle Wrap="False" HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="AgentName" Caption="نمایندگی">
                    <CellStyle Wrap="False" HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                
                <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="FileNo" Caption="شماره پروانه">
                    <CellStyle Wrap="False" HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="InActiveName" Caption="وضعیت">
                </dxwgv:GridViewDataTextColumn>

                <dxwgv:GridViewCommandColumn VisibleIndex="9" Caption=" " Width="30px" ShowClearFilterButton="true">
                </dxwgv:GridViewCommandColumn>
            </Columns>
            <Settings ShowHorizontalScrollBar="True"></Settings>
            <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
            <ClientSideEvents DetailRowExpanding="function(s, e) {
		grid.SetFocusedRowIndex(grid.cpSelectedIndex);
}"
                EndCallback="function(s, e) {
if(grid.cpDoPrint == 1)
{
    grid.cpDoPrint = 0;
    window.open('../../Print.aspx');
}

}" />
        </TSPControls:CustomAspxDevGridView>
        <br />
        <TSPControls:CustomASPxPopupControl ID="PopupWorkFlow" runat="server" Width="390px" 
             ClientInstanceName="PopupWorkFlow"
            PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" Modal="True"
             HeaderText="" CloseAction="CloseButton"
            AllowDragging="True" AllowResize="True">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <div dir="rtl">
                        <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelWorkFlow" runat="server" ClientInstanceName="CallbackPanelWorkFlow"
                             OnCallback="CallbackPanelWorkFlow_Callback" Width="100%">
                            <ClientSideEvents EndCallback="function(s, e) {
	PopupWorkFlow.SetHeaderText(CallbackPanelWorkFlow.cpWfName);
	lblWfState.SetText(CallbackPanelWorkFlow.cpWfStateName);
}"></ClientSideEvents>
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <dxp:ASPxPanel runat="server" Width="100%" ID="PanelMain" ClientInstanceName="PanelMain">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <table width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td colspan="2">
                                                                <dxe:ASPxLabel runat="server" Text="ASPxLabel" Font-Size="X-Small" ID="lblError"
                                                                    ForeColor="Red" Visible="False" >
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 25px" valign="top" align="center" colspan="2">
                                                                <dxe:ASPxLabel runat="server" Text="وضعیت جاری:" ID="lblWfState" ForeColor="Red"
                                                                    ClientInstanceName="lblWfState" >
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" align="right" colspan="2">
                                                                <TSPControls:CustomASPxCheckBox runat="server" Text="ارسال  همزمان پیام"  ID="chbIsSendMail"
                                                                   >
                                                                </TSPControls:CustomASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" align="right">
                                                                <dxe:ASPxLabel runat="server" Text="مرحله" Font-Size="X-Small" ID="lblSenBack" __designer:wfdid="w32">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td  valign="top" align="right">
                                                                <TSPControls:CustomAspxComboBox runat="server"  
                                                                    ID="cmbSendBackTask" ValueType="System.String" 
                                                                   >
                                                                    <ValidationSettings>
                                                                      
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
                                                                <dxe:ASPxLabel runat="server" Text="توضیحات" Font-Size="X-Small" ID="ASPxLabel7"
                                                                    >
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td dir="rtl" valign="top" align="right">
                                                                <TSPControls:CustomASPXMemo runat="server" Height="71px" ID="txtDescription" 
                                                                  ClientInstanceName="TextDesc" 
                                                                    >
                                                                    <ValidationSettings>
                                                                       
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomASPXMemo>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td dir="ltr" align="center" colspan="2">
                                                                <TSPControls:CustomAspxButton  runat="server" Text="ارسال"  Width="93px" ID="btnSendNextWorkStep"
                                                                    AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep"
                                                                     __designer:wfdid="w36">
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
                                    <dxp:ASPxPanel runat="server" Width="100%" ID="PanelSaveSuccessfully" ClientInstanceName="PanelSaveSuccessfully">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <div align="center">
                                                    <br />
                                                    <dxe:ASPxLabel runat="server" Text="عملیات مورد نظر با موفقیت انجام شد." Font-Size="X-Small"
                                                        ID="lblWFWarning" ForeColor="Red" __designer:wfdid="w38">
                                                    </dxe:ASPxLabel>
                                                    <br />
                                                    <br />
                                                    <TSPControls:CustomAspxButton runat="server" Text="خروج"  Width="93px" ID="btnClose"
                                                        AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnSenNextStep"
                                                         __designer:wfdid="w39">
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
            <HeaderStyle HorizontalAlign="Center" Wrap="False">
                <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
            </HeaderStyle>
            <SizeGripImage Height="12px" Width="12px" />
            <CloseButtonImage Height="17px" Width="17px" />
        </TSPControls:CustomASPxPopupControl>
        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>
                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                        cellpadding="0">
                        <tbody>
                            <tr>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                        ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        OnClick="BtnNew_Click">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/new.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                        Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnEdit_Click">
                                        <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/edit.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                        ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        OnClick="btnView_Click">
                                        <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/view.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server"  EnableTheming="False"
                                        EnableViewState="False" AutoPostBack="false" Text=" " OnClick="btnDelete_Click"
                                        ToolTip="حذف درخواست" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
    if(!confirm('آیا مطمئن به حذف این درخواست هستید؟'))
        e.processOnServer=false;
        //CallbackPanelPage.PerformCallback('btnDelete');
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیر فعال"
                                        ID="btnActive1" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnActive_Click">
                                        <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
//else
  //e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/disactive.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تغییرات"
                                        ID="btnChangeReq2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnChangeReq_Click">
                                        <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/Change.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گردش کار"
                                        ID="btnSendNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False">
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
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/reload.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td style="width: 30px">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="پیگیری"
                                        ID="btnTracing2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnTracing_Click">
                                        <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/Cheque Status ReChange.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6"></TSPControls:MenuSeprator>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                        ID="btnPrint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False">
                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	grid.PerformCallback('Print');
	//window.open(&quot;../../Print.aspx&quot;);	
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/printers.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px" 
                                        ToolTip="خروجی Excel" CausesValidation="False" ID="btnExportExcel2" EnableClientSideAPI="True"
                                        UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnExportExcel2"
                                        AutoPostBack="false" OnClick="btnExportExcel_Click">
                                        <ClientSideEvents Click="function(s,e){ }" />
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
        <%--</ContentTemplate>
        </asp:updatepanel>--%>
        <asp:ObjectDataSource ID="ObjdsOtherPerson" runat="server" SelectMethod="SelectOtherPersonKardanAndMemarForManagmentPage"
            TypeName="TSP.DataManager.OtherPersonManager">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="OtpId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjdsTechnicianRequest" runat="server" SelectMethod="SelectForManagementPage"
            TypeName="TSP.DataManager.TechnicianRequestManager" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="-1" Name="OtpId" SessionField="OtpId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        
           
            <asp:ObjectDataSource ID="ObjdsMajor" runat="server" SelectMethod="FindMjParents"
                TypeName="TSP.DataManager.MajorManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
            TypeName="TSP.DataManager.WorkFlowTaskManager">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>
