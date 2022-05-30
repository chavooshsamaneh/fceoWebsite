<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ImplementDoc.aspx.cs" Inherits="Settlement_ImplementDoc_ImplementDoc"
    Title="مدیریت مجوز فعالیت مجری حقیقی" %>

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

<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <div style="width: 100%" align="center">
        <div style="width: 100%" dir="rtl" id="Content" runat="server">
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]</div>

               <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                    cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                    Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnEdit_Click" CausesValidation="False">
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
                                                    <Image  Url="~/Images/icons/edit.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                    Width="25px" ID="btnView" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnView_Click" CausesValidation="False">
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
                                                    <Image  Url="~/Images/icons/view.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گردش کار"
                                                    ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
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
                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/reload.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnTracing_Click" Text=" "
                                                    ToolTip="پیگیری" UseSubmitBehavior="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
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
                                            <td >
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
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewMemberFile" Width="100%" runat="server"
                DataSourceID="ObjdsMemberFileMainRequest" 
                 AutoGenerateColumns="False" ClientInstanceName="GridViewMemberFile"
                KeyFieldName="MfId" OnCustomCallback="GridViewMemberFile_CustomCallback" OnHtmlRowPrepared="GridViewMemberFile_HtmlRowPrepared"
                Font-Size="8pt" OnAutoFilterCellEditorInitialize="GridViewMemberFile_AutoFilterCellEditorInitialize"
                OnHtmlDataCellPrepared="GridViewMemberFile_HtmlDataCellPrepared" OnPageIndexChanged="GridViewMemberFile_PageIndexChanged">
                <ClientSideEvents FocusedRowChanged="function(s, e) {

	                if(GridViewMemberFile.cpIsReturn!=1)
	                {
		                GridViewMemberFile.cpSelectedIndex=GridViewMemberFile.GetFocusedRowIndex();
                			
	                }
	                else
	                {
		                GridViewMemberFile.cpIsReturn=0;	
	                }

	                if(GridViewMemberFile.cpIsPostBack!=1)
		                GridViewMemberFile.ExpandDetailRow(GridViewMemberFile.cpSelectedIndex);	
	                else
		                GridViewMemberFile.cpIsPostBack=0;
                }" DetailRowExpanding="function(s, e) {
	                if(GridViewMemberFile.cpIsReturn!=1)
	                {
		                GridViewMemberFile.cpSelectedIndex=GridViewMemberFile.GetFocusedRowIndex();
                			
	                }
	                else
	                {
		                GridViewMemberFile.cpIsReturn=0;	
	                }				
		                GridViewMemberFile.SetFocusedRowIndex(GridViewMemberFile.cpSelectedIndex);
                }"></ClientSideEvents>
                <Templates>
                    <DetailRow>
                        <TSPControls:CustomAspxDevGridView ID="GridViewMeFileHistory" Width="100%" runat="server"
                            DataSourceID="ObjdsMeFileSubRequest"  
                            KeyFieldName="MfId" ClientInstanceName="GridViewMemberFile1" AutoGenerateColumns="False"
                            OnBeforePerformDataSelect="GridViewMeFileHistory_BeforePerformDataSelect" OnAutoFilterCellEditorInitialize="GridViewMeFileHistory_AutoFilterCellEditorInitialize"
                            OnHtmlDataCellPrepared="GridViewMeFileHistory_HtmlDataCellPrepared">
                            <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                            <Columns>
                                     <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                                VisibleIndex="0" Width="40px">
                                <DataItemTemplate>
                                    <div align="center">
                                        <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>'>
                                        </dxe:ASPxImage>
                                    </div>
                                </DataItemTemplate>
                                <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                                    ValueType="System.String">
                                </PropertiesComboBox>
                            </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="SerialNo" Caption="شماره سریال">
                                    <HeaderStyle Wrap="True" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MFNo" Caption="شماره مجوز">
                                    <HeaderStyle Wrap="True"></HeaderStyle>
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="RegDate" Caption="تاریخ صدور">
                                    <HeaderStyle Wrap="True"></HeaderStyle>
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ExpireDate" Caption="تاریخ پایان اعتبار">
                                    <HeaderStyle Wrap="True"></HeaderStyle>
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="InActives" Caption="وضعیت">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="MFType" Caption="وضعیت پروانه">
                                    <HeaderStyle Wrap="True" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="MailNo" Caption="شماره نامه">
                                    <HeaderStyle Wrap="True" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="MailDate" Caption="تاریخ نامه">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="8" Visible="false" FieldName="TaskName"
                                    Caption="وضعیت درخواست">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewCommandColumn VisibleIndex="9" Caption=" " ShowClearFilterButton="true">                               
                                </dxwgv:GridViewCommandColumn>
                            </Columns>
                            <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowHorizontalScrollBar="True">
                            </Settings>
                            <SettingsDetail IsDetailGrid="True"></SettingsDetail>
                        </TSPControls:CustomAspxDevGridView>
                    </DetailRow>
                </Templates>
                <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True" ColumnResizeMode="Control">
                </SettingsBehavior>
                <Columns>
                         <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                                VisibleIndex="0" Width="40px">
                                <DataItemTemplate>
                                    <div align="center">
                                        <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>'>
                                        </dxe:ASPxImage>
                                    </div>
                                </DataItemTemplate>
                                <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                                    ValueType="System.String">
                                </PropertiesComboBox>
                            </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MemberId" Caption="کد عضویت"
                        Width="50px">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FirstName" Caption="نام"
                        Width="150px">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="LastName" Caption="نام خانودگی"
                        Width="150px">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="SerialNo"
                        Caption="شماره سریال">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="کد مجری" FieldName="MFSerialNo" Name="''"
                        VisibleIndex="3">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="MFNo" Caption="شماره مجوز اجرا"
                        Width="150px" Name="''">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="RegDate" Caption="تاریخ صدور">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="ExpireDate" Caption="تاریخ پایان اعتبار">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="MFType"
                        Caption="وضعیت پروانه">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="InActives" Caption="وضعیت"
                        Width="50px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="9" FieldName="TaskName"
                        Caption="وضعیت درخواست">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="11" Caption=" " Width="30px" ShowClearFilterButton="true">                      
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowFilterRowMenu="True" ShowHorizontalScrollBar="True">
                </Settings>
                <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
            </TSPControls:CustomAspxDevGridView>
            <br />
                    <fieldset width="98%">
                        <legend>راهنما</legend>
                        <table width="100%">
                            <tbody>
                               

                                <tr>
                                    <td valign="middle" align="right">
                                        <asp:Image ID="Image2" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/ImplementDoc/MeDoc_WFStart.png" />
                                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="درخواست صدور مجوز فعالیت مجری حقیقی"
                                            ForeColor="Black" Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="middle" align="right">
                                          <asp:Image ID="Image3" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/ImplementDoc/MeDoc_DocumentUnitEmployee.png" />
                                        <dxe:ASPxLabel ID="ASPxLabel15" runat="server" Text="بررسی و تایید درخواست توسط مسئول واحد پروانه"
                                            ForeColor="Black" Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="middle" align="right">
                                        <asp:Image ID="Image4" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/ImplementDoc/MeDoc_settlementAgentConfirming.png" />
                                        <dxe:ASPxLabel ID="ASPxLabel16" runat="server" Text="تایید کارشناس مسکن" ForeColor="Black"
                                            Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="right">
                                       <asp:Image ID="Image5" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/ImplementDoc/MeDoc_ NezamEmployeeInSettlement.png" />
                                        <dxe:ASPxLabel ID="ASPxLabel17" runat="server" Text="تایید رئیس اداره توسعه مهندسی و نظارت بر مقررات ملی و کیفیت ساخت"
                                            ForeColor="Black" Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="middle" align="right">
                                        <asp:Image ID="Image6" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/ImplementDoc/MeDoc_RoadAndurbanism.png" />
                                        <dxe:ASPxLabel ID="ASPxLabel18" runat="server" Text="تایید مدیر کل اداره راه و شهرسازی استان فارس"
                                            ForeColor="Black" Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="middle" align="right">
                                       <asp:Image ID="Image7" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/ImplementDoc/MeDoc_PrintDoc.png" />
                                        <dxe:ASPxLabel ID="ASPxLabel19" runat="server" Text="چاپ گواهینامه توسط کارشناس واحد پروانه"
                                            ForeColor="Black" Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="right">
                                        <asp:Image ID="Image11" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/MeDoc_PrintAndWaitForConfirm.png" />
                                        <dxe:ASPxLabel ID="ASPxLabel24" runat="server" Text="چاپ شده و منتظر تایید نهایی"
                                            ForeColor="Black" Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="middle" align="right">
                                         <asp:Image ID="Image8" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WFConfirmAndEnd.png" />
                                        <dxe:ASPxLabel ID="ASPxLabel20" runat="server" Text="تایید و پایان بررسی صدور مجوز مجری حقیقی"
                                            ForeColor="Black" Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="middle" align="right">
                                       <asp:Image ID="Image9" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WFREjectAndEnd.png" />
                                        <dxe:ASPxLabel ID="ASPxLabel21" runat="server" Text="عدم تایید و پایان بررسی صدور مجوز مجری حقیقی"
                                            ForeColor="Black" Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                             
                            </tbody>
                        </table>
                    </fieldset>
               
            <br />
               <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
           
                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                    cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                    Width="25px" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnEdit_Click" CausesValidation="False">
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
                                                    <Image  Url="~/Images/icons/edit.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                    Width="25px" ID="btnView2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnView_Click" CausesValidation="False">
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
                                                    <Image  Url="~/Images/icons/view.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گردش کار"
                                                    ID="btnSendNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
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
                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/reload.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing2" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnTracing_Click" Text=" "
                                                    ToolTip="پیگیری" UseSubmitBehavior="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
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
                                            <td style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnprint2" runat="server" AutoPostBack="False" 
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
                                            <td >
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
            <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewMemberFile">
            </dxwgv:ASPxGridViewExporter>
                                <dxhf:ASPxHiddenField ID="HiddenFieldRequest" runat="server" ClientInstanceName="HiddenFieldRequest">
                                </dxhf:ASPxHiddenField>
           <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
                        TypeName="TSP.DataManager.WorkFlowTaskManager">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsMemberFileMainRequest" runat="server" SelectMethod="SelectImpDocMainRequest"
                TypeName="TSP.DataManager.DocMemberFileManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsMeFileSubRequest" runat="server" SelectMethod="SelectImpDocSubRequest"
                TypeName="TSP.DataManager.DocMemberFileManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="-1" Name="MeId" SessionField="MeId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="TaskCode" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBGrId" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.GradeManager"
                OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBActId" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.AcceptTypeManager"
                OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
            <asp:HiddenField ID="hfPageMode" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hfMfId" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hfMeId" runat="server"></asp:HiddenField>
            <uc1:WFUserControl ID="WFUserControl" runat="server" OnCallback="WFUserControl_Callback"
                GridName="grid" SessionName="SendBackDataTable_StlImpDoc" />
        </div>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
