<%@ Page Title="مدیریت اعلام آماده به کاری" Async="true" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ObserverWorkRequest.aspx.cs" Inherits="Employee_TechnicalServices_Capacity_ObserverWorkRequest" %>

<%@ Register Src="~/UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>

    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
        visible="true">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>

                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                    ID="btnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="BtnNew_Click">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/new.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>

                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                    ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnView_Click">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/view.png">
                                    </Image>
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست تغییرات"
                                    CausesValidation="False" ID="btnChangeRequest" OnClick="btnChangeRequest_Click" UseSubmitBehavior="False"
                                    EnableViewState="true" EnableTheming="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>

                                    <Image Url="~/Images/icons/Change.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>

                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                    ID="btnInActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnInActive_Click">
                                    <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');


}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/disactive.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار"
                                    ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
	ShowWf();
}
}"></ClientSideEvents>

                                    <Image Url="~/Images/icons/reload.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing" runat="server" AutoPostBack="true"
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="پیگیری" UseSubmitBehavior="False"
                                    CausesValidation="False" OnClick="btnTracing_Click">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />

                                    <Image Url="~/Images/icons/Cheque Status ReChange.png" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتخاب ستون ها"
                                    ID="ASPxButton2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    AutoPostBack="False" Visible="true">
                                    <ClientSideEvents Click="function(s, e) {
	if(!grid.IsCustomizationWindowVisible())
		grid.ShowCustomizationWindow();
	else
		grid.HideCustomizationWindow();
}" />
                                    <Image Url="~/Images/icons/cursor-hand.png" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                    UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">

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
    <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table width="100%">

                    <tr>
                        <td align="right" style="width: 15%">
                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="کد عضویت" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" style="width: 35%">
                            <TSPControls:CustomTextBox IsMenuButton="true" ID="txtMeId" runat="server" ClientInstanceName="txtMeId"
                                Width="100%">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="SearchValid">

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                    <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت را صحیح وارد نمایید" />
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right" style="width: 15%">
                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="شماره پروانه">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" style="width: 35%">
                            <TSPControls:CustomTextBox IsMenuButton="true" ID="txtMFNo" runat="server" ClientInstanceName="txtMFNo"
                                Width="100%"
                                RightToLeft="True" Style="direction: ltr">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="SearchValid">

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                    <RegularExpression ErrorText="شماره پروانه به صورت *****-***-**  می باشد" ValidationExpression="\d{2}-\d{3}-\d{1,5}" />
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Text="تاریخ ثبت از">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox ID="txtCreateDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="تاریخ ثبت تا">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox ID="txtCreateDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right">
                            <asp:Label runat="server" Text="نمایندگی" ID="Label1"></asp:Label>
                        </td>
                        <td valign="top" align="right">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                TextField="Name" ID="ComboAgent" ClientInstanceName="ComboAgent" DataSourceID="ObjectdatasourceAgent"
                                ValueType="System.Int32" ValueField="AgentId" RightToLeft="True"
                                EnableIncrementalFiltering="True">
                                <ItemStyle HorizontalAlign="Right" />
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />

                            </TSPControls:CustomAspxComboBox>

                            <asp:ObjectDataSource ID="ObjectdatasourceAgent" runat="server" SelectMethod="FindByCode"
                                TypeName="TSP.DataManager.AccountingAgentManager">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="-1" Name="AgentId" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                        <td>رشته آماده به کاری</td>
                        <td>
                            <TSPControls:CustomAspxComboBox ID="comboMjParent" runat="server"
                                ValueType="System.String"
                                TextField="MjName" ValueField="MjId" RightToLeft="True" ClientInstanceName="comboMjParent"
                                DataSourceID="ObjectDataSource_MajorParents" Width="100%" HorizontalAlign="Right" EnableIncrementalFiltering="True">
                                <ItemStyle HorizontalAlign="Right" />
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                            </TSPControls:CustomAspxComboBox>
                            <asp:ObjectDataSource ID="ObjectDataSource_MajorParents" runat="server" SelectMethod="FindMjParents"
                                TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>مرحله
                        </td>
                        <td>
                            <TSPControls:CustomAspxComboBox ID="CmbTask" ClientInstanceName="CmbTask" runat="server"
                                ValueType="System.String"
                                TextField="TaskName" ValueField="TaskId"
                                DataSourceID="ObjdsWorkFlowTask" HorizontalAlign="Right" EnableIncrementalFiltering="True">
                                <ItemStyle HorizontalAlign="Right" />
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4" dir="ltr" valign="top">
                            <br />
                            <table>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton ID="ASPxButton1" runat="server" AutoPostBack="true" OnClick="btnSearch_Click"
                                            Text="پاک کردن فرم" UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s, e) {
	   	 ClearSearch();
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton ID="btnSearch" runat="server" AutoPostBack="true" OnClick="btnSearch_Click"
                                            Text="جستجو" ClientInstanceName="btnSearch" Width="98px" UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s, e) {
 e.processOnServer=false;
 if (ASPxClientEdit.ValidateGroup('SearchValid')){
 
	   if(CheckSearch()==0)
        {
          alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.'); 
          return; 
        }
        else
         e.processOnServer=true;
  }
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
    <br />
    <TSPControls:CustomAspxDevGridView Width="100%" ID="GridViewObserverWorkRequest" runat="server"
        DataSourceID="ObjectDataSourceWorkRequest"
        ClientInstanceName="grid" EnableViewState="False" KeyFieldName="ObsWorkReqId" AutoGenerateColumns="False">
        <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
        <SettingsText Title="لیست اعلام آماده به کاری " />
        <SettingsCookies Enabled="true" StoreFiltering="true" StoreColumnsWidth="true" StoreColumnsVisiblePosition="true" />
        <SettingsCustomizationWindow Enabled="True" />
        <Settings ShowTitlePanel="true" ShowHorizontalScrollBar="true"></Settings>
        <Columns>
            <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="CurrentWfTasId" Name="WFState"
                VisibleIndex="0">
                <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                    ValueType="System.String">
                </PropertiesComboBox>
                <DataItemTemplate>
                    <div align="center">
                        <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>' ToolTip='<%# Bind("WfTaskFullName") %>'>
                        </dxe:ASPxImage>
                    </div>
                </DataItemTemplate>
            </dxwgv:GridViewDataComboBoxColumn>

            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="نام و نام خانوادگی" FieldName="MeFullName" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxcp:GridViewDataCheckColumn FieldName="Group1" Caption="گروه ساختمانی الف"
                Name="Group1" VisibleIndex="0">
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dxcp:GridViewDataCheckColumn>
            <dxcp:GridViewDataCheckColumn FieldName="Group2" Caption="گروه ساختمانی ب"
                Name="Group2" VisibleIndex="0">
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dxcp:GridViewDataCheckColumn>
            <dxcp:GridViewDataCheckColumn FieldName="Group3" Caption="گروه ساختمانی ج"
                Name="Group3" VisibleIndex="0">
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dxcp:GridViewDataCheckColumn>
            <dxcp:GridViewDataCheckColumn FieldName="Group4" Caption="گروه ساختمانی د"
                Name="Group4" VisibleIndex="0">
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dxcp:GridViewDataCheckColumn>
            <dxcp:GridViewDataCheckColumn FieldName="WantShahrakSanatiMeter" Caption="شهرک صنعتی"
                Name="WantShahrakSanatiMeter" VisibleIndex="0">
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dxcp:GridViewDataCheckColumn>
            <dxcp:GridViewDataCheckColumn FieldName="WantEghdamMeliMaskan" Caption="اقدام ملی مسکن"
                Name="WantEghdamMeliMaskan" VisibleIndex="0">
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dxcp:GridViewDataCheckColumn>
            <dxcp:GridViewDataCheckColumn FieldName="WantCharityWork" Caption="خیریه"
                Name="WantCharityWork" VisibleIndex="0" Visible="False">
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dxcp:GridViewDataCheckColumn>

            <dxwgv:GridViewDataTextColumn Caption="تاریخ عضویت" FieldName="MembershipDate" Name="MembershipDate" VisibleIndex="0" Visible="False">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ اخذ صلاحیت" FieldName="ObsDate" Name="ObsDate" VisibleIndex="0" Visible="False">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تقاضای کار نظارت" FieldName="IsObserverOffName" Name="IsObserverOffName" VisibleIndex="0" Visible="False">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="رشته آماده بکاری" FieldName="MjParentName" Name="MjParentName" VisibleIndex="0" Visible="False">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره پروانه" FieldName="FileNo" Name="FileNo" VisibleIndex="0" Visible="False">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="عضو دفتر گاز" FieldName="HasCertName" Name="HasGasCertName" VisibleIndex="0" Visible="False">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="پایه نظارت" FieldName="GrdName" Name="GrdName" VisibleIndex="0" Visible="False">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه طراحی" FieldName="DesGrdName" Name="DesGrdName" VisibleIndex="0" Visible="False">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ظرفیت باقی مانده واقعی نظارت" FieldName="RemainCapacityObsReal" Name="RemainCapacityObsReal" VisibleIndex="0" Visible="False">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نوع کار" FieldName="WantedWorkTypeName" Name="WantedWorkTypeName" VisibleIndex="0" Visible="False">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="StatusName" Name="StatusName" VisibleIndex="0" Visible="False">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ظرفیت کل نظارت" FieldName="CapacityObs" Name="CapacityObs" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="ظرفیت کل طراحی براساس پایه" FieldName="CapacityDesign" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نظارت شهرداری شیراز" FieldName="ShirazMunicipalityMeter" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="طراحی شهرداری شیراز" FieldName="ShirazMunicipalityDesignMeter" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="طراحی بنیاد مسکن" FieldName="BonyadMaskanDesignMeter" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نظارت بنیاد مسکن" FieldName="BonyadMaskan" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="طرح شهرسازی شهری شهرداری شیراز" FieldName="ShirazMunicipulityUrbenismTarh" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="طرح انطباق شهری شهرداری شیراز" FieldName="ShirazMunicipulityUrbenismEntebaghShahri" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataComboBoxColumn Caption="شهر انتخابی اول" FieldName="City1" Name="CitName1"
                VisibleIndex="0">
                <PropertiesComboBox DataSourceID="ObjectDataSourceCity" TextField="CitName" ValueField="CitId"
                    ValueType="System.String">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataComboBoxColumn Caption="شهر انتخابی دوم" FieldName="City2" Name="CitName2"
                VisibleIndex="0">
                <PropertiesComboBox DataSourceID="ObjectDataSourceCity" TextField="CitName" ValueField="CitId"
                    ValueType="System.String">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataTextColumn Caption="نمایندگی " FieldName="AgentName" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ارجاعات قبول کار 98 " FieldName="CountObsWorkSelect98" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ارجاعات قبول کار 99 " FieldName="CountObsWorkSelect99" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کل ارجاعات 98 " FieldName="CountObsWorkSelect98All" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کل ارجاعات 99 " FieldName="CountObsWorkSelect99All" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="درصد ظرفیت پر شده نظارت" FieldName="PercentOfCapacityUsage" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تعداد نظارت رد شده " FieldName="CountRejectByObs" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption=" تعداد کار اخذ شده" FieldName="CountInproccesWorks" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption=" تعداد کار اخذ شده زیر 400" FieldName="CountUnder400MeterWork" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شروع مرخصی" FieldName="StartOffDate" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایان مرخصی" FieldName="EndOffDate" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ ثبت" FieldName="CreateDate" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="زمان ثبت" FieldName="CreateTime" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="0" Width="30px" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>

        <SettingsDetail ExportMode="None" ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
        <Templates>
            <DetailRow>
                <TSPControls:CustomAspxDevGridView ID="GridViewObserverWorkRequestChange" Width="100%" runat="server"
                    DataSourceID="ObjectDataSourceObsWorkChange" KeyFieldName="ObsWorkReqChangeId" ClientInstanceName="GridViewObserverWorkRequestChange"
                    AutoGenerateColumns="False" OnBeforePerformDataSelect="GridViewObserverWorkRequestChange_BeforePerformDataSelect">
                    <Columns>
                        <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                            VisibleIndex="0">
                            <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                                ValueType="System.String">
                            </PropertiesComboBox>
                            <DataItemTemplate>
                                <div align="center">
                                    <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>' ToolTip='<%# Bind("WfTaskFullName") %>'>
                                    </dxe:ASPxImage>
                                </div>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataComboBoxColumn>

                        <dxcp:GridViewDataTextColumn FieldName="CreateDate" Caption="تاریخ درخواست"
                            Name="CreateDate" VisibleIndex="0">
                            <CellStyle Wrap="False" HorizontalAlign="Right">
                            </CellStyle>
                        </dxcp:GridViewDataTextColumn>

                        <dxcp:GridViewDataTextColumn FieldName="CreateTime" Caption="زمان ثبت درخواست"
                            Name="CreateTime" VisibleIndex="0">
                            <CellStyle Wrap="False" HorizontalAlign="Right">
                            </CellStyle>
                        </dxcp:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نوع درخواست" FieldName="RequestTypeName" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تقاضای کار نظارت" FieldName="IsObserverOffName" Name="IsObserverOffName" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxcp:GridViewDataTextColumn FieldName="IsConfirmName" Caption="وضعیت تایید"
                            Name="IsConfirmName" VisibleIndex="0">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxcp:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شروع مرخصی" FieldName="StartOffDate" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="پایان مرخصی" FieldName="EndOffDate" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxcp:GridViewDataCheckColumn FieldName="Group1" Caption="گروه ساختمانی الف"
                            Name="Group1" VisibleIndex="0">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxcp:GridViewDataCheckColumn>
                        <dxcp:GridViewDataCheckColumn FieldName="Group2" Caption="گروه ساختمانی ب"
                            Name="Group2" VisibleIndex="0">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxcp:GridViewDataCheckColumn>
                        <dxcp:GridViewDataCheckColumn FieldName="Group3" Caption="گروه ساختمانی ج"
                            Name="Group3" VisibleIndex="0">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxcp:GridViewDataCheckColumn>
                        <dxcp:GridViewDataCheckColumn FieldName="Group4" Caption="گروه ساختمانی د"
                            Name="Group4" VisibleIndex="0">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxcp:GridViewDataCheckColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نظارت شهرداری شیراز" FieldName="ShirazMunicipalityMeter" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="طراحی شهرداری شیراز" FieldName="ShirazMunicipalityDesignMeter" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="طراحی بنیاد مسکن" FieldName="BonyadMaskanDesignMeter" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نظارت بنیاد مسکن" FieldName="BonyadMaskan" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="طرح شهرسازی شهری شهرداری شیراز" FieldName="ShirazMunicipulityUrbenismTarh" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="طرح انطباق شهری شهرداری شیراز" FieldName="ShirazMunicipulityUrbenismEntebaghShahri" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                          <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="0" PropertiesHyperLinkEdit-Text="فرم تعهد نظارت"
                            FieldName="UrlObserverCommitmentForm" Caption="فرم تعهد نظارت" Name="UrlAttachment" Width="200px">
                        </dxwgv:GridViewDataHyperLinkColumn>
                        <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="0" PropertiesHyperLinkEdit-Text="فایل پیوست تاییدیه نهادهای مربوطه"
                            FieldName="UrlAttachment" Caption="فایل پیوست تاییدیه نهادهای مربوطه" Name="UrlAttachment" Width="200px">
                        </dxwgv:GridViewDataHyperLinkColumn>


                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="0" Width="30px" ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <SettingsDetail IsDetailGrid="True"></SettingsDetail>
                </TSPControls:CustomAspxDevGridView>
            </DetailRow>
        </Templates>
    </TSPControls:CustomAspxDevGridView>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewObserverWorkRequest"
        ExportEmptyDetailGrid="false">
    </dxwgv:ASPxGridViewExporter>
    <br />
    <fieldset width="100%">
        <legend>راهنما</legend>
        <ul class="HelpWorkflowTasksImages">
            <li class="col-sm-4">
                <ul>
                    <asp:Repeater runat="server" ID="RepeaterWfHelp1">
                        <ItemTemplate>
                            <li>
                                <asp:Image ID="Image2" Height="16px" Width="16px" runat="server" ImageUrl='<%# Eval("ImageURL") %> ' />
                                <a><%# Eval("TaskName") %> </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <li></li>
                </ul>
            </li>
            <li class="col-sm-4">
                <ul>
                    <li class="dropdown-header"></li>
                    <asp:Repeater runat="server" ID="RepeaterWfHelp2">
                        <ItemTemplate>
                            <li>
                                <asp:Image ID="Image2" Height="16px" Width="16px" runat="server" ImageUrl='<%# Eval("ImageURL") %> ' />
                                <a><%# Eval("TaskName") %> </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <li></li>
                </ul>
            </li>
            <li class="col-sm-4">
                <ul>
                    <li class="dropdown-header"></li>
                    <asp:Repeater runat="server" ID="RepeaterWfHelp3">
                        <ItemTemplate>
                            <li>
                                <asp:Image ID="Image2" Height="16px" Width="16px" runat="server" ImageUrl='<%# Eval("ImageURL") %> ' />
                                <a><%# Eval("TaskName") %> </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <li></li>
                </ul>
            </li>
        </ul>
    </fieldset>

    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                    ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="BtnNew_Click">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/new.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>

                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                    ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnView_Click">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/view.png">
                                    </Image>
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست تغییرات"
                                    CausesValidation="False" ID="btnChangeRequest2" OnClick="btnChangeRequest_Click" UseSubmitBehavior="False"
                                    EnableViewState="true" EnableTheming="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>

                                    <Image Url="~/Images/icons/Change.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                    ID="btnInActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnInActive_Click">
                                    <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');


}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/disactive.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار"
                                    ID="btnSendNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
	ShowWf();	
}
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/reload.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing2" runat="server" AutoPostBack="true"
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="پیگیری" UseSubmitBehavior="False"
                                    CausesValidation="False" OnClick="btnTracing_Click">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/Cheque Status ReChange.png" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتخاب ستون ها"
                                    ID="CustomAspxButton1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    AutoPostBack="False" Visible="true">
                                    <ClientSideEvents Click="function(s, e) {
	if(!grid.IsCustomizationWindowVisible())
		grid.ShowCustomizationWindow();
	else
		grid.HideCustomizationWindow();
}" />
                                    <Image Url="~/Images/icons/cursor-hand.png" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel" OnClick="btnExportExcel_Click"
                                    UseSubmitBehavior="False" Visible="true">
                                    <Image Height="25px" Url="~/Images/icons/ExportExcel.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewMemberFile"
        SessionName="SendBackDataTable_EmpMeObsWork" OnCallback="WFUserControl_Callback" />
    <dxhf:ASPxHiddenField ID="HDpage" runat="server">
    </dxhf:ASPxHiddenField>
    <asp:ObjectDataSource runat="server" SelectMethod="SelectByAgent" ID="ObjectDataSourceCity"
        TypeName="TSP.DataManager.CityManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Type="Int32" Name="AgentId" DefaultValue="-1"></asp:Parameter>
            <asp:Parameter Type="Int32" Name="ShowInTsWorkRequest" DefaultValue="1"></asp:Parameter>
            <asp:Parameter Type="Int32" Name="CitId" DefaultValue="-1"></asp:Parameter>
            <asp:Parameter Type="Int32" Name="CitIdExeption" DefaultValue="-1"></asp:Parameter>
            <asp:Parameter Type="String" Name="CitIdList" DefaultValue=""></asp:Parameter>
            <asp:Parameter Type="String" Name="CitIdExeptionList" DefaultValue=""></asp:Parameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceWorkRequest" runat="server" TypeName="TSP.DataManager.TechnicalServices.ObserverWorkRequestManager"
        SelectMethod="SearchForManagmentPage" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="CreateDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="CreateDateTo" Type="String" />
            <asp:Parameter DefaultValue="%" Name="MFNo" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="CurrentAgentCode" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="AgentId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="MJParentId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="TaskId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceObsWorkChange" runat="server" TypeName="TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager"
        SelectMethod="SearchForManagmentPage" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="-1" Name="ObsWorkReqId" SessionField="ObsWorkReqId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="ObsWorkReqChangeId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
        TypeName="TSP.DataManager.WorkFlowTaskManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>


</asp:Content>


