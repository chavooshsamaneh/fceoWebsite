<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="Delivery.aspx.cs" Inherits="Employee_Document_Delivery" %>


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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Src="~/UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
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
                                    OnClick="btnNew_Click">

                                    <Image Url="~/Images/icons/new.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>

                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                    ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnView_Click">

                                    <Image Url="~/Images/icons/view.png">
                                    </Image>
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewDocDelivery.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                </TSPControls:CustomAspxButton>

                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                    CausesValidation="False" ID="btnEdit" OnClick="btnEdit_Click" UseSubmitBehavior="False"
                                    EnableViewState="true" EnableTheming="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewDocDelivery.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>

                                    <Image Url="~/Images/icons/Edit.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار"
                                    ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewDocDelivery.GetFocusedRowIndex()&lt;0)
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
	if (GridViewDocDelivery.GetFocusedRowIndex()&lt;0)
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
                                    ID="btnCustomizationWindow" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    AutoPostBack="False" Visible="true">
                                    <ClientSideEvents Click="function(s, e) {
	if(!GridViewDocDelivery.IsCustomizationWindowVisible())
		GridViewDocDelivery.ShowCustomizationWindow();
	else
		GridViewDocDelivery.HideCustomizationWindow();
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
                        <td align="right" style="width: 15%">کد عضویت
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
                       <td valign="top" align="right" style="width: 15%">
                            <asp:Label runat="server" Text="نمایندگی" ID="lblAgent"></asp:Label>
                        </td>
                        <td valign="top" align="right" style="width: 35%">
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

                    </tr>
                    <tr>
                        <td align="right">تاریخ ثبت از
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox ID="txtCreateDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right">تاریخ ثبت تا
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox ID="txtCreateDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">دلیل بازگشت ظرفیت
                        </td>
                        <td align="right">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                ID="cmbReasonType" ClientInstanceName="cmbReasonType" ValueType="System.String" RightToLeft="True">
                                <ItemStyle HorizontalAlign="Right" />
                                <ValidationSettings Display="Dynamic" ValidationGroup="ValidAttach" ErrorTextPosition="bottom">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <RequiredField IsRequired="True" ErrorText="دلیل بازگشت را انتخاب نمایید"></RequiredField>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <Items>
                                    <dxe:ListEditItem Value="1" Text="پایان کار"></dxe:ListEditItem>
                                    <dxe:ListEditItem Value="2" Text="تعطیلی کارگاه"></dxe:ListEditItem>
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
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

    <TSPControls:CustomAspxDevGridView Width="100%" ID="GridViewDocDelivery" runat="server"
        DataSourceID="ObjectDataSourceDelivery"
        ClientInstanceName="GridViewDocDelivery" EnableViewState="False" KeyFieldName="CapRId" AutoGenerateColumns="False">
        <SettingsCustomizationWindow Enabled="True" />
        <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
        <SettingsText Title="مدیریت بازگشت ظرفیت نظارت" />
        <Settings ShowTitlePanel="true" ShowHorizontalScrollBar="true"></Settings>

        <Columns>
            <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="WFCurrentTaskId" Name="WFState"
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
            <dxwgv:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="CreateDate" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
         
            <dxwgv:GridViewDataTextColumn Caption="نمایندگی" FieldName="AgentName" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
          
            <dxtc:GridViewDataHyperLinkColumn Caption="تصویر رسید" FieldName="ImageURL" VisibleIndex="0" PropertiesHyperLinkEdit-Text="تصویر رسید" PropertiesHyperLinkEdit-Target="_blank">
            </dxtc:GridViewDataHyperLinkColumn>

            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>

    </TSPControls:CustomAspxDevGridView>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewDocDelivery"
        ExportEmptyDetailGrid="false">
    </dxwgv:ASPxGridViewExporter>
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
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>

                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                    ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnNew_Click">

                                    <Image Url="~/Images/icons/new.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>

                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                    ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnView_Click">

                                    <Image Url="~/Images/icons/view.png">
                                    </Image>
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewDocDelivery.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                </TSPControls:CustomAspxButton>

                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                    CausesValidation="False" ID="btnEdit2" OnClick="btnEdit_Click" UseSubmitBehavior="False"
                                    EnableViewState="true" EnableTheming="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewDocDelivery.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>

                                    <Image Url="~/Images/icons/Edit.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار"
                                    ID="btnSendNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewDocDelivery.GetFocusedRowIndex()&lt;0)
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
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing2" runat="server" AutoPostBack="true"
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="پیگیری" UseSubmitBehavior="False"
                                    CausesValidation="False" OnClick="btnTracing_Click">
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewDocDelivery.GetFocusedRowIndex()&lt;0)
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
                                    ID="btnCustomizationWindow2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    AutoPostBack="False" Visible="true">
                                    <ClientSideEvents Click="function(s, e) {
	if(!GridViewDocDelivery.IsCustomizationWindowVisible())
		GridViewDocDelivery.ShowCustomizationWindow();
	else
		GridViewDocDelivery.HideCustomizationWindow();
}" />
                                    <Image Url="~/Images/icons/cursor-hand.png" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False"
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

    <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewDocDelivery"
        SessionName="SendBackDataTable_EmpMeObsWork" OnCallback="WFUserControl_Callback" />
    <dxhf:ASPxHiddenField ID="HdPage" runat="server">
    </dxhf:ASPxHiddenField>

    <asp:ObjectDataSource ID="ObjectDataSourceDelivery" runat="server" TypeName="TSP.DataManager.DocDeliveryManager"
        SelectMethod="SelectDocDeliveryForEmpManagementPage" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="AgentId" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="CreateDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="CreateDateTo" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="TaskId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
        TypeName="TSP.DataManager.WorkFlowTaskManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

