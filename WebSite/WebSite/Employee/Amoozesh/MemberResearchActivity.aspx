<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="MemberResearchActivity.aspx.cs" Inherits="Employee_Amoozesh_MemberResearchActivity"
    Title="مدیریت تالیفات و تحقیقات" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
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

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
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



                <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                    width="100%">
                    <tbody>
                        <tr>
                            <td style="vertical-align: top; text-align: right">
                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                    cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                    CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="BtnNew_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                    CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	
if (grid2.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
	
	
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/edit.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                    CausesValidation="False" ID="btnView" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnView_Click">
                                                    <ClientSideEvents Click="function(s, e) {
		 
if (grid2.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
	
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/view.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیر فعال"
                                                    ID="btnInActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnInActive_Click" Visible="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (grid2.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/disactive.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست بررسی مجدد"
                                                    CausesValidation="False" ID="btnChange" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnChange_Click">
                                                    <ClientSideEvents Click="function(s, e) {
		 
if (grid2.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به ثبت درخواست بررسی مجدد می باشید؟');
	
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/Change.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گردش کار"
                                                    ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (grid2.GetFocusedRowIndex()&lt;0)
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
                                                    <Image  Url="~/Images/icons/reload.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="پیگیری"
                                                    ID="btnTracing" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnTracing_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (grid2.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/Cheque Status ReChange.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>

                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                                    ID="btnPrint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s,e){grid2.PerformCallback('Print'); }"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/Printers.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                                    ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnExportExcel_Click">
                                                    <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/ExportExcel.png">
                                                    </Image>
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
    <br />
    <dx:ASPxGridViewExporter ID="GridViewExporter" GridViewID="GridViewResearchAct" runat="server">
    </dx:ASPxGridViewExporter>
    <TSPControls:CustomAspxDevGridView ID="GridViewResearchAct" runat="server" DataSourceID="OdbMemResearch"
        Width="100%"
        ClientInstanceName="grid2" OnCustomCallback="GridViewResearchAct_CustomCallback"
        KeyFieldName="MraId" AutoGenerateColumns="False" OnDetailRowExpandedChanged="GridViewResearchAct_DetailRowExpandedChanged" RightToLeft="True">
        <Templates>
            <DetailRow>
                <TSPControls:CustomAspxDevGridView ID="GridViewTrainingJudgment" runat="server"
                    Width="100%" KeyFieldName="JudgeId"
                    AutoGenerateColumns="False" OnBeforePerformDataSelect="GridViewTrainingJudgment_BeforePerformDataSelect"
                    DataSourceID="ObjdsTrainingJudgment" OnHtmlDataCellPrepared="GridViewTrainingJudgment_HtmlDataCellPrepared"
                    OnAutoFilterCellEditorInitialize="GridViewTrainingJudgment_AutoFilterCellEditorInitialize">

                    <Columns>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Visible="false" FieldName="MeetingId"
                            Caption="شماره جلسه">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" Visible="false" FieldName="MeetingDate"
                            Caption="تاریخ جلسه">
                            <CellStyle Wrap="True"></CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="JudgeViewPoint" Caption="نظر کارشناسی">
                            <CellStyle Wrap="True"></CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="JudgeGrade" Caption="امتیاز">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="ConfirmType" Caption="وضعیت تایید">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="CreateDate" Caption="تاریخ درخواست">
                            <CellStyle Wrap="True"></CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" Visible="false" FieldName="TaskName"
                            Caption="وضعیت درخواست">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                            VisibleIndex="6" Width="40px">
                            <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                                ValueType="System.String">
                            </PropertiesComboBox>
                            <DataItemTemplate>
                                <div align="center">
                                    <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl="~/Images/WFStart.png">
                                    </dxe:ASPxImage>
                                </div>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="7" Caption=" " ShowClearFilterButton="true"> 
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <SettingsDetail IsDetailGrid="True"></SettingsDetail>
                </TSPControls:CustomAspxDevGridView>
            </DetailRow>
        </Templates>

        <Columns>
            <dxwgv:GridViewDataTextColumn FieldName="MraId" Name="MraId" Visible="False" VisibleIndex="0">
                <HeaderTemplate>
                </HeaderTemplate>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1">
                <CellStyle Wrap="False"></CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2">
                <CellStyle Wrap="False"></CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نوع مقاله / فعالیت آموزشی" FieldName="RaName"
                VisibleIndex="3" Width="150px">
                <CellStyle Wrap="False"></CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام مقاله / فعالیت آموزشی" FieldName="Name"
                Width="150px" VisibleIndex="4">
                <CellStyle Wrap="False"></CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="5"
                Width="150px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="6">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="7" Width="30px" ShowClearFilterButton="true">
               
            </dxwgv:GridViewCommandColumn>
        </Columns>

        <Settings ShowHorizontalScrollBar="True"></Settings>
        <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" ExportMode="All"></SettingsDetail>
        <ClientSideEvents FocusedRowChanged="function(s, e) {
	  if(s.cpPrint==0)               
            grid2.ExpandDetailRow(grid2.GetFocusedRowIndex());
}"
            EndCallback="function(s, e) {
	 if(s.cpPrint==1)
        {
            window.open('../../Print.aspx');
           s.cpPrint=0;
        }
}" />

    </TSPControls:CustomAspxDevGridView>

    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>



                <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                    width="100%">
                    <tbody>
                        <tr>
                            <td style="vertical-align: top; text-align: right">
                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                    cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                    ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="BtnNew_Click">
                                                    <ClientSideEvents Click="function(s, e) {
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                    Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnEdit_Click">
                                                    <ClientSideEvents Click="function(s, e) {
		 
if (grid2.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
}
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/edit.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                    ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnView_Click">
                                                    <ClientSideEvents Click="function(s, e) {
		 
if (grid2.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/view.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیر فعال"
                                                    ID="btnInActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnInActive_Click" Visible="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (grid2.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/disactive.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست بررسی مجدد"
                                                    CausesValidation="False" ID="btnChange2" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnChange_Click">
                                                    <ClientSideEvents Click="function(s, e) {
		 
if (grid2.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به ثبت درخواست بررسی مجدد می باشید؟');
	
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/Change.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گردش کار"
                                                    ID="btnSendNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (grid2.GetFocusedRowIndex()&lt;0)
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
                                                    <Image  Url="~/Images/icons/reload.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="پیگیری"
                                                    ID="btnTracing2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnTracing_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (grid2.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/Cheque Status ReChange.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>

                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                                    ID="btnPrint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s,e){grid2.PerformCallback('Print'); }"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/Printers.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                                    ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnExportExcel_Click">
                                                    <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/ExportExcel.png">
                                                    </Image>
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
    <uc1:WFUserControl ID="WFUserControl" runat="server" SessionName="SendBackDataTable_Research"
        GridName="grid2" OnCallback="WFUserControl_Callback" />
    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkId"
        TypeName="TSP.DataManager.WorkFlowTaskManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="WorkFlowId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="TaskOrder" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODBResearch" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.ResearchActivityManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsTrainingJudgment" runat="server" CacheDuration="30"
        EnableCaching="True" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectByResearchAct"
        TypeName="TSP.DataManager.TrainingJudgmentManager">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="-1" Name="PkId" SessionField="MraId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="JudgeId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbMemResearch" runat="server" DeleteMethod="Delete" FilterExpression="MeId={0}"
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
        TypeName="TSP.DataManager.MemberResearchActivityManager" UpdateMethod="Update">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>

    </asp:ObjectDataSource>
    <asp:HiddenField ID="MemberId" runat="server" />
    <asp:HiddenField ID="MemberRequest" runat="server" Visible="False" />
    <asp:HiddenField ID="HDMode" runat="server" Visible="False" />
</asp:Content>
