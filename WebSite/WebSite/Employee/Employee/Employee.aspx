<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Employee.aspx.cs" Inherits="Employee_Employee_Employee"
    Title="کارمندان" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
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

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="idcontent" style="width: 100%;" align="center">
  
        <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelPage" runat="server" ClientInstanceName="CallbackPanelPage"
            HideContentOnCallback="false" OnCallback="CallbackPanelPage_Callback"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent ID="PanelContent1" runat="server">
                    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                            href="#">بستن</a>]</div>
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <table >
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                    CausesValidation="False" ID="BtnNew" AutoPostBack="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="BtnNew_Click">
                                                  
                                                    <Image  Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                    CausesValidation="False"  ID="btnEdit" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnEdit_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 	{
  		 e.processOnServer=false;
  		 alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}
"></ClientSideEvents>
                                                   
                                                    <Image  Url="~/Images/icons/edit.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                    ID="btnView" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnViewClient"
                                                    OnClick="btnView_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 	{
  		 e.processOnServer=false;
  		 alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                                  
                                                    <Image  Url="~/Images/icons/view.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تغییرات"
                                                    ID="btnChangeReq" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnChangeReq_Click">
                                                    <ClientSideEvents Click="function(s, e) {
            
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />
                                                   
                                                    <Image  Url="~/Images/icons/Change.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف درخواست"
                                                    ID="btnReqDelete" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnReqDelete_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
	 e.processOnServer= confirm('آیا مطمئن به حذف این درخواست هستید؟');
}" />
                                                    
                                                    <Image  Url="~/Images/icons/delete.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                                </TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  
                                                    Visible="false" ToolTip="غیر فعال" CausesValidation="False" ID="btnDisActive"
                                                    EnableClientSideAPI="True" EnableViewState="False" EnableTheming="False" OnClick="btnDisActive_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	 
if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                   
                                                    <Image  Url="~/Images/icons/disactive.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="تعیین سطح دسترسی"
                                                    CausesValidation="False" ID="btnUserRight" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnUserRight_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 	{
  		 e.processOnServer=false;
  		 alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                                    
                                                    <Image  Url="~/Images/icons/ChartMember.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازیابی رمز عبور"
                                                    CausesValidation="False" ID="btnReset1" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnResetSave_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>

                                                    <Image  Url="~/Images/ChangePassword.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6">
                                                </TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  
                                                    Visible="true" ToolTip="رمز یکبار عبور فعال" CausesValidation="False" ID="btnActiveTempPass"
                                                    EnableClientSideAPI="True" EnableViewState="False" EnableTheming="False" OnClick="btnTempPass_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	 
if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به فعال کردن رمز یکبار عبور این ردیف هستید؟');
}"></ClientSideEvents>
                                                    
                                                    <Image  Url="~/Images/icons/ActiveTempPass.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                                                                        <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  
                                                    Visible="true" ToolTip="رمز یکبار عبور غیر فعال" CausesValidation="False" ID="btnInActiveTempPass"
                                                    EnableClientSideAPI="True" EnableViewState="False" EnableTheming="False" OnClick="btnTempPass_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	 
if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن رمز یکبار عبوراین ردیف هستید؟');
}"></ClientSideEvents>
                                                   
                                                    <Image  Url="~/Images/icons/InActiveTempPass.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>


                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                                </TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گردش کار"
                                                    ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {                                                          
if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 
 {
 ShowWf();
 }
}" />
                                                   
                                                    <Image  Url="~/Images/icons/reload.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="width: 30px">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="پیگیری گردش کار"
                                                    ID="btnTracing" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnTracing_Click">
                                                    <ClientSideEvents Click="function(s, e) {
            
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                                   
                                                    <Image  Url="~/Images/icons/Cheque Status ReChange.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2">
                                                </TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  
                                                    ToolTip="چاپ" CausesValidation="False" ID="btnPrint" EnableClientSideAPI="True"
                                                    UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnPrintClient"
                                                    AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s,e){ 
	CallbackPanelPage.PerformCallback('Print');
}" />
                                                   
                                                    <Image  Url="~/Images/icons/printers.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  
                                                    ToolTip="خروجی Excel" CausesValidation="False" ID="btnExportExcel" EnableClientSideAPI="True"
                                                    UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnPrintClient"
                                                    AutoPostBack="false" OnClick="btnExportExcel_Click">
                                                    <ClientSideEvents Click="function(s,e){ }" />
                                                   
                                                    <Image  Url="~/Images/icons/ExportExcel.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>
                    <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="GridViewEmployee">
                    </dxwgv:ASPxGridViewExporter>
                    <br />
                    <TSPControls:CustomAspxDevGridView SettingsDetail-ExportMode="Expanded" ID="GridViewEmployee"
                        runat="server" ClientInstanceName="GridViewEmployeeClient" DataSourceID="ObjdsEmployee"
                        Width="100%" OnAutoFilterCellEditorInitialize="GridViewEmployee_AutoFilterCellEditorInitialize"
                        OnHtmlDataCellPrepared="GridViewEmployee_HtmlDataCellPrepared" KeyFieldName="EmpId"
                        OnFocusedRowChanged="GridViewEmployee_FocusedRowChanged" OnHtmlRowPrepared="GridViewEmployee_HtmlRowPrepared"
                        OnPageIndexChanged="GridViewEmployee_PageIndexChanged">
                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	if(GridViewEmployeeClient.cpIsReturn!=1)
	{
		GridViewEmployeeClient.cpSelectedIndex=GridViewEmployeeClient.GetFocusedRowIndex();
			
	}
	else
	{
		GridViewEmployeeClient.cpIsReturn=0;	
	}

	if(GridViewEmployeeClient.cpIsPostBack!=1);
		//GridViewEmployeeClient.ExpandDetailRow(GridViewEmployeeClient.cpSelectedIndex);	
	else
		GridViewEmployeeClient.cpIsPostBack=0;
}" DetailRowExpanding="function(s, e) {
	GridViewEmployeeClient.cpIsVisible=1;	
	if(GridViewEmployeeClient.cpIsReturn!=1)
	{
		GridViewEmployeeClient.cpSelectedIndex=GridViewEmployeeClient.GetFocusedRowIndex();
			
	}
	else
	{
		GridViewEmployeeClient.cpIsReturn=0;	
	}				
		GridViewEmployeeClient.SetFocusedRowIndex(GridViewEmployeeClient.cpSelectedIndex);

}" DetailRowCollapsing="function(s, e) {
	GridViewEmployeeClient.cpIsVisible=0;
}" />
                        <Settings ShowHorizontalScrollBar="True" />
                        <SettingsDetail ExportMode="All" ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True">
                        </SettingsDetail>
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
                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="EmpCode" Caption="کدپرسنلی"
                                Width="50px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FirstName" Caption="نام">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="LastName" Caption="نام خانوادگی">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="UserName" Caption="نام کاربری">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="PartName" Caption="بخش">
                            </dxwgv:GridViewDataTextColumn>
                            
                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="AgentName" Caption="نمایندگی">
                               <CellStyle Wrap="False" HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="FatherName" Caption="نام پدر">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="IdNo" Caption="شماره شناسنامه">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="SSN" Caption="کد ملی">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="CreateDate" Caption="تاریخ ایجاد">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn VisibleIndex="10" Caption=" " Width="30px" ShowClearFilterButton="true">
                       
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="StatusType" Caption="وضعیت">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                              <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="TempPass" Caption="رمز یکبار عبور">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                          
                        </Columns>
                        <Templates>
                            <DetailRow>
                                <TSPControls:CustomAspxDevGridView runat="server" DataSourceID="ObjdsEmployeeReq"
                                    ID="GridViewRequest" KeyFieldName="EmpId" ClientInstanceName="GridViewRequest"
                                    Width="100%" OnBeforePerformDataSelect="GridViewRequest_BeforePerformDataSelect"
                                    OnAutoFilterCellEditorInitialize="GridViewRequest_AutoFilterCellEditorInitialize"
                                    OnHtmlDataCellPrepared="GridViewRequest_HtmlDataCellPrepared">
                                    <Settings ShowHorizontalScrollBar="true"></Settings>
                                    <Columns>
                                            <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                                            VisibleIndex="0">
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
                                        <dxwgv:GridViewDataTextColumn Caption="کدپرسنلی" FieldName="EmpCode" VisibleIndex="0">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نام پدر" FieldName="FatherName" VisibleIndex="3">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="کد ملی" FieldName="SSN" VisibleIndex="3">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="تاریخ ایجاد" FieldName="CreateDate" VisibleIndex="4">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                    
                                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="6" ShowClearFilterButton="true">
                                     
                                        </dxwgv:GridViewCommandColumn>
                                    </Columns>
                                </TSPControls:CustomAspxDevGridView>
                            </DetailRow>
                        </Templates>
                    </TSPControls:CustomAspxDevGridView>
                    <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewEmployeeClient"
                        SessionName="SendBackDataTable_EmpWF" OnCallback="WFUserControl_Callback" />
                    <br />
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                    width="100%">
                                    <tbody>
                                        <tr>
                                            <td align="right">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                    CausesValidation="False" ID="btnNew1" AutoPostBack="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="BtnNew_Click">
                                                                    <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>
                                                                   
                                                                    <Image  Url="~/Images/icons/new.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                    CausesValidation="False"  ID="btnEdit1" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="btnEdit_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 	{
  		 e.processOnServer=false;
  		 alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}
"></ClientSideEvents>
                                                                   
                                                                    <Image  Url="~/Images/icons/edit.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                                    ID="btnView2" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnViewClient2"
                                                                    OnClick="btnView_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 	{
  		 e.processOnServer=false;
  		 alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                                                  
                                                                    <Image  Url="~/Images/icons/view.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تغییرات"
                                                                    ID="btnChangeReq2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnChangeReq_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
            
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />
                                                                   
                                                                    <Image  Url="~/Images/icons/Change.png"  />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف درخواست"
                                                                    ID="btnReqDelete2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="btnReqDelete_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
	 e.processOnServer= confirm('آیا مطمئن به حذف این درخواست هستید؟');
}" />
                                                                   
                                                                    <Image  Url="~/Images/icons/delete.png"  />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  
                                                                    ToolTip="غیر فعال" CausesValidation="False" ID="btnDisActive2" EnableClientSideAPI="True"
                                                                    Visible="false" EnableViewState="False" EnableTheming="False" OnClick="btnDisActive_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
	 
if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                                  
                                                                    <Image  Url="~/Images/icons/disactive.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td width="10px" align="center">
                                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3">
                                                                </TSPControls:MenuSeprator>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="تعیین سطح دسترسی"
                                                                    CausesValidation="False" ID="btnUserRight1" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="btnUserRight_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 	{
  		 e.processOnServer=false;
  		 alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                                                   
                                                                    <Image  Url="~/Images/icons/ChartMember.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازیابی رمز عبور"
                                                                    CausesValidation="False" ID="btnReset" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnResetSave_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                                                 
                                                                    <Image  Url="~/Images/ChangePassword.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>


                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator7">
                                                </TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  
                                                    Visible="true" ToolTip="رمز یکبار عبور فعال" CausesValidation="False" ID="btnActiveTempPass2"
                                                    EnableClientSideAPI="True" EnableViewState="False" EnableTheming="False" OnClick="btnTempPass_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	 
if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به فعال کردن رمز یکبار عبور این ردیف هستید؟');
}"></ClientSideEvents>
                                                   
                                                    <Image  Url="~/Images/icons/ActiveTempPass.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                                                                        <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  
                                                    Visible="true" ToolTip="رمز یکبار عبور غیر فعال" CausesValidation="False" ID="btnInActiveTempPass2"
                                                    EnableClientSideAPI="True" EnableViewState="False" EnableTheming="False" OnClick="btnTempPass_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	 
if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن رمز یکبار عبوراین ردیف هستید؟');
}"></ClientSideEvents>
                                                  
                                                    <Image  Url="~/Images/icons/InActiveTempPass.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>


                                                            <td width="10px" align="center">
                                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4">
                                                                </TSPControls:MenuSeprator>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گردش کار"
                                                                    ID="btnSendNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False">
                                                                    <ClientSideEvents Click="function(s, e) {                                                          
if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 
 {
 ShowWf();
 }
}" />
                                                                  
                                                                    <Image  Url="~/Images/icons/reload.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td style="width: 30px">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="پیگیری گردش کار"
                                                                    ID="btnTracing2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnTracing_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
            
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                                                 
                                                                    <Image  Url="~/Images/icons/Cheque Status ReChange.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td width="10px" align="center">
                                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5">
                                                                </TSPControls:MenuSeprator>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  
                                                                    ToolTip="چاپ" CausesValidation="False" ID="btnPrint2" EnableClientSideAPI="True"
                                                                    UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnPrintClient2"
                                                                    AutoPostBack="false">
                                                                    <ClientSideEvents Click="function(s,e){ 
	CallbackPanelPage.PerformCallback('Print');
}" />
                                                                  
                                                                    <Image  Url="~/Images/icons/printers.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  
                                                                    ToolTip="خروجی Excel" CausesValidation="False" ID="btnExportExcel2" EnableClientSideAPI="True"
                                                                    UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnPrintClient"
                                                                    AutoPostBack="false" OnClick="btnExportExcel_Click">
                                                                    <ClientSideEvents Click="function(s,e){ }" />
                                                                  
                                                                    <Image  Url="~/Images/icons/ExportExcel.png"  />
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
                    <asp:ObjectDataSource ID="ObjdsEmployeeReq" runat="server" SelectMethod="FindByEmpolyee"
                        TypeName="TSP.DataManager.EmployeeRequestManager" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="-1" Name="EmpId" SessionField="EmpId" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
                        TypeName="TSP.DataManager.WorkFlowTaskManager">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsEmployee" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.EmployeeManager"
                        CacheExpirationPolicy="Sliding"
                        OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                </dxp:PanelContent>
            </PanelCollection>
            <ClientSideEvents EndCallback="function(s, e) {
if(CallbackPanelPage.cpDoPrint == 1)
{
    CallbackPanelPage.cpDoPrint = 0;
    window.open('../../Print.aspx');
}
}" />
        </TSPControls:CustomAspxCallbackPanel>
    </div>
</asp:Content>
