<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ObservationDoc.aspx.cs" Inherits="Employee_Document_ObservationDoc"
    Title="مدیریت مجوز فعالیت ناظر حقیقی" %>

<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
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

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script type="text/javascript" language="javascript">
        function SearchKeyPress(e, Type) {
            if (Type == 1)//DevExpress controls
            {
                if (e.htmlEvent.keyCode == 13) {
                    btnSearch.DoClick();
                }
            }
            else if (Type == 2)//asp controls
            {
                if (e.keyCode == 13)
                    btnSearch.DoClick();
            }
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
                CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
                FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
            </pdc:PersianDateScriptManager>  <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                            [<a class="closeLink" href="#">بستن</a>]</div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table >
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                            ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click" CausesValidation="False">
                                           
                                            <Image  Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top;">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                             ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnEdit_Click" CausesValidation="False">
                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                            
                                            <Image  Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top; width: 30px;">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                             ID="btnView" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnShow_Click" CausesValidation="False">
                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                         
                                            <Image  Url="~/Images/icons/view.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td style="vertical-align: top; width: 30px">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="لغو درخواست"
                                             ID="btnDelete" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnDelete_Click" CausesValidation="False">
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');	
}" />
                                          
                                            <Image  Url="~/Images/icons/delete.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top;">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست صدور المثنی"
                                             ID="btnReDuplicate" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" CausesValidation="False" OnClick="btnReDuplicate_Click"
                                            Visible="False">
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
//else
//{	
//	HiddenFieldRequest.Set('ReType','ReDuplicate');
//	txtRequestDes.SetText('');
//	CallbackPanelRequest.PerformCallback('');
//	PopupRequest.Show();
//}
}" />
                                          
                                            <Image  Url="~/Images/icons/copy.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تغییرات"
                                             ID="btnChange" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" CausesValidation="False" OnClick="btnChange_Click">
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}

}" />
                                          
                                            <Image  Url="~/Images/icons/Change.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تمدید"
                                             ID="btnRevival" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" CausesValidation="False" OnClick="btnRevival_Click">
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
}" />
                                           
                                            <Image  Url="~/Images/icons/Revival.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                      <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInvalid" runat="server"  EnableTheming="False"
                                                    EnableViewState="False" Text=" " ToolTip="درخواست ابطال عضویت حقوقی" UseSubmitBehavior="False"
                                                    OnClick="btnInvalid_Click">
                                                    <ClientSideEvents Click="function(s, e) {
		if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
  e.processOnServer= confirm('آیا مطمئن به باطل کردن این درخواست هستید؟');
}" />
                                                   
                                                    <Image  Url="~/Images/icons/button_cancel.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td style="vertical-align: top">
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
                                         
                                            <Image  Url="~/Images/icons/reload.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top;">
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
                                          
                                            <Image  Url="~/Images/icons/Cheque Status ReChange.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnprint" runat="server" AutoPostBack="False" 
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	GridViewMemberFile.PerformCallback('Print');	
}" />
                                           
                                            <Image  Url="~/Images/icons/printers.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnCommitment" runat="server" AutoPostBack="False" 
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ تعهدنامه نظارت"
                                            UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	CallbackPanelSearch.PerformCallback('PrintCom');	
}" />
                                          
                                            <Image  Url="~/Images/icons/printers2.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnAnnounce" runat="server" AutoPostBack="False" 
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ فرم اعلام همکاری"
                                            UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	CallbackPanelSearch.PerformCallback('PrintAnn');	
}" />
                                           
                                            <Image  Url="~/Images/icons/printers3.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                            ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnExportExcel_Click">
                                            <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                            
                                            <Image  Url="~/Images/icons/ExportExcel.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="left" style="width: 100%">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnHelp" runat="server" CausesValidation="False" 
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                            Visible="true" AutoPostBack="false">
                                            <Image  Url="~/Images/Help.png"  />
                                            
                                            <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }">
                                            </ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelSearch" runat="server"  
                HideContentOnCallback="false" ClientInstanceName="CallbackPanelSearch" OnCallback="CallbackPanelSearch_Callback"
                Width="100%">
                <ClientSideEvents EndCallback="function(s,e){
if(s.cpPrintCom==1)
    {
        s.cpPrintCom = 0;
        window.open(s.cpPrintComPath);
    }

if(s.cpPrintAnn==1)
    {
        s.cpPrintAnn = 0;
        window.open(s.cpPrintAnnPath);
    }
            }" />
                <PanelCollection>
                    <dxp:PanelContent runat="server">
                      	<TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                                            <table width="100%">
                                                <tr>
                                                    <td align="right" style="width: 104px">
                                                        <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Text="تاریخ اعتبار از">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td align="right" >
                                                        <pdc:PersianDateTextBox ID="txtEndDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                            PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                                            Width="300px" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                                    </td>
                                                    <td align="right">
                                                        <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="تاریخ اعتبار تا">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td align="right">
                                                        <pdc:PersianDateTextBox ID="txtEndDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                            PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                                            Width="300px" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="کد پیگیری">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" ID="txtFollowCode" runat="server" ClientInstanceName="txtFollowCode"
                                                              
                                                            >
                                                            <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                                            <ValidationSettings>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="4" dir="ltr" valign="top">
                                                        <br />
                                                        <table>
                                                            <tr>
                                                                <td style="width: 100px">
                                                                    <TSPControls:CustomAspxButton   ID="ASPxButton1" runat="server" AutoPostBack="False" 
                                                                          Text="پاک کردن فرم"
                                                                        UseSubmitBehavior="false">
                                                                        <ClientSideEvents Click="function(s, e) {
		CallbackPanelSearch.PerformCallback('Clear');
		GridViewMemberFile.PerformCallback('Clear');
}" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td style="width: 100px">
                                                                    <TSPControls:CustomAspxButton  ID="btnSearch" runat="server" AutoPostBack="False" 
                                                                          Text="جستجو"
                                                                        ClientInstanceName="btnSearch" Width="98px" UseSubmitBehavior="false">
                                                                        <ClientSideEvents Click="function(s, e) {
	GridViewMemberFile.PerformCallback('Search');
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
                            <TSPControls:CustomAspxDevGridView  ID="GridViewMemberFile" Width="100%" runat="server"
                                DataSourceID="ObjdsMemberFileMainRequest" 
                                 AutoGenerateColumns="False" ClientInstanceName="GridViewMemberFile"
                                KeyFieldName="MfId" OnCustomCallback="GridViewMemberFile_CustomCallback" OnHtmlRowPrepared="GridViewMemberFile_HtmlRowPrepared"
                                OnAutoFilterCellEditorInitialize="GridViewMemberFile_AutoFilterCellEditorInitialize"
                                OnHtmlDataCellPrepared="GridViewMemberFile_HtmlDataCellPrepared" OnDetailsChanged="GridViewMemberFile_DetailsChanged">
                                <ClientSideEvents  DetailRowExpanding="function(s, e) {
	       			
		        GridViewMemberFile.SetFocusedRowIndex(GridViewMemberFile.cpSelectedIndex);
        }"
                                     EndCallback="function(s, e) {
            if(s.cpDoPrint==1)
            {
                s.cpDoPrint = 0;
                window.open('../../Print.aspx');
            }       
}"></ClientSideEvents>
                                <Templates>
                                    <DetailRow>
                                        <TSPControls:CustomAspxDevGridView ID="GridViewMeFileHistory" Width="100%" runat="server"
                                            DataSourceID="ObjdsMeFileSubRequest"  
                                            KeyFieldName="MfId" ClientInstanceName="GridViewMemberFile1" AutoGenerateColumns="False"
                                            OnBeforePerformDataSelect="GridViewMeFileHistory_BeforePerformDataSelect" OnAutoFilterCellEditorInitialize="GridViewMeFileHistory_AutoFilterCellEditorInitialize"
                                            OnHtmlDataCellPrepared="GridViewMeFileHistory_HtmlDataCellPrepared">
                                            <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True" ColumnResizeMode="Control">
                                            </SettingsBehavior>
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
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="SerialNo" Caption="شماره سریال"
                                                    Width="80px">
                                                    <HeaderStyle Wrap="False" />
                                                    <CellStyle Wrap="False">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FollowCode" Caption="کد پیگیری"
                                                    Width="80px">
                                                    <HeaderStyle Wrap="False" />
                                                    <CellStyle Wrap="False">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MFNo" Caption="شماره مجوز"
                                                    Width="80px">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <CellStyle Wrap="False">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="RegDate" Caption="تاریخ صدور"
                                                    Width="70px">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <CellStyle Wrap="False">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ExpireDate" Caption="تاریخ پایان اعتبار"
                                                    Width="80px">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <CellStyle Wrap="False">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="MailNo" Caption="شماره نامه"
                                                    Width="100px">
                                                    <HeaderStyle Wrap="False" />
                                                    <CellStyle Wrap="False">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="MailDate" Caption="تاریخ نامه"
                                                    Width="70px">
                                                    <CellStyle Wrap="False">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="MFType" Caption="وضعیت پروانه"
                                                    Width="70px">
                                                    <HeaderStyle Wrap="False" />
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="InActives" Caption="وضعیت"
                                                    Width="60px">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="ارسال کننده" FieldName="RequesterName" VisibleIndex="8"
                                                    Width="100px">
                                                    <CellStyle Wrap="False">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="WFRequesterType" VisibleIndex="9"
                                                    Width="100px">
                                                    <CellStyle Wrap="False">
                                                    </CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewCommandColumn Width="30px" VisibleIndex="11" Caption=" "  ShowClearFilterButton="true">
                                           
                                                </dxwgv:GridViewCommandColumn>
                                            </Columns>
                                            
                                            <SettingsDetail IsDetailGrid="True"></SettingsDetail>
                                        </TSPControls:CustomAspxDevGridView>
                                    </DetailRow>
                                </Templates>
                             
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="" Name="ExpireState" VisibleIndex="0" Width="20px">
                                        <DataItemTemplate>
                                            <div align="center">
                                                <dxe:ASPxImage ID="ImgExpireState" runat="server" Width="16px" Height="16px">
                                                </dxe:ASPxImage>
                                            </div>
                                        </DataItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MemberId" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1"
                                        Width="150px">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="نام خانودگی" FieldName="LastName" VisibleIndex="2"
                                        Width="150px">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="شماره مجوز ناظر" FieldName="MFNo" Name="''"
                                        VisibleIndex="3">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="پایه نظارت" FieldName="ObsGrade" Name="''"
                                        VisibleIndex="4">
                                        <CellStyle Wrap="False" HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="شماره سریال" FieldName="SerialNo" Visible="False"
                                        VisibleIndex="5">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="تاریخ صدور" FieldName="RegDate" VisibleIndex="6"
                                        Width="80px">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان اعتبار" FieldName="LastExpireDate"
                                        VisibleIndex="7" Width="80px">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="وضعیت پروانه" FieldName="MFType" Visible="False"
                                        VisibleIndex="8">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActives" VisibleIndex="9">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="وضعیت درخواست" FieldName="TaskName" Visible="False"
                                        VisibleIndex="10">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                                        VisibleIndex="11" Width="40px">
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
                                    <dxwgv:GridViewCommandColumn Width="30px" Caption=" " VisibleIndex="6" ShowClearFilterButton="true">
                               
                                    </dxwgv:GridViewCommandColumn>
                                </Columns>
                               
                                <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" ExportMode="All">
                                </SettingsDetail>
                            </TSPControls:CustomAspxDevGridView>
                            <br />
                        </div>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomAspxCallbackPanel>
              <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                                <table dir="rtl" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                    ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False" OnClick="BtnNew_Click">
                                                   
                                                    <Image  Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                     ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnEdit_Click" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                                   
                                                    <Image  Url="~/Images/icons/edit.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                     ID="btnView2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnShow_Click" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                                  
                                                    <Image  Url="~/Images/icons/view.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                            </td>
                                            <td style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="لغو درخواست"
                                                     ID="btnDelete2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnDelete_Click" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                                   
                                                    <Image  Url="~/Images/icons/delete.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست صدور المثنی"
                                                     ID="btnReDuplicate2" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False" OnClick="btnReDuplicate_Click"
                                                    Visible="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}

}" />
                                                  
                                                    <Image  Url="~/Images/icons/copy.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تغییرات"
                                                     ID="btnChange2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False" OnClick="btnChange_Click">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
}" />
                                                  
                                                    <Image  Url="~/Images/icons/Change.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تمدید"
                                                     ID="btnRevival2" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" CausesValidation="False" OnClick="btnRevival_Click">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
}" />
                                                  
                                                    <Image  Url="~/Images/icons/Revival.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td> <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInvalid2" runat="server"  EnableTheming="False"
                                                    EnableViewState="False" Text=" " ToolTip="درخواست ابطال عضویت حقوقی" UseSubmitBehavior="False"
                                                    OnClick="btnInvalid_Click">
                                                    <ClientSideEvents Click="function(s, e) {
		if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
  e.processOnServer= confirm('آیا مطمئن به باطل کردن این درخواست هستید؟');
}" />
                                                  
                                                    <Image  Url="~/Images/icons/button_cancel.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
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
                                                  
                                                    <Image  Url="~/Images/icons/Cheque Status ReChange.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6"></TSPControls:MenuSeprator>
                                            </td>
                                            <td style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnprint2" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	GridViewMemberFile.PerformCallback('Print');	
}" />
                                                   
                                                    <Image  Url="~/Images/icons/printers.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnCommitment2" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ تعهدنامه نظارت"
                                                    UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	CallbackPanelSearch.PerformCallback('PrintCom');	
}" />
                                                    
                                                    <Image  Url="~/Images/icons/printers2.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnAnnounce2" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ فرم اعلام همکاری"
                                                    UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	CallbackPanelSearch.PerformCallback('PrintAnn');	
}" />
                                                    
                                                    <Image  Url="~/Images/icons/printers3.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="vertical-align: top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                                    ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnExportExcel_Click">
                                                    <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                                   
                                                    <Image  Url="~/Images/icons/ExportExcel.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="left" style="width: 100%">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnHelp2" runat="server" CausesValidation="False" 
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                                    Visible="true" AutoPostBack="false">
                                                    <Image  Url="~/Images/Help.png"  />
                                                   
                                                    <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }">
                                                    </ClientSideEvents>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table></dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>

                                <dxhf:ASPxHiddenField ID="HiddenFieldRequest" runat="server" ClientInstanceName="HiddenFieldRequest">
                                </dxhf:ASPxHiddenField>
                            
            <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
                TypeName="TSP.DataManager.WorkFlowTaskManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsMemberFileMainRequest" runat="server" 
                 SelectMethod="SelectObsDocMainRequest" TypeName="TSP.DataManager.DocMemberFileManager"
               OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                    <asp:Parameter DefaultValue="%" Name="FollowCode" Type="String" />
                    <asp:Parameter DefaultValue="1" Name="EndDateFrom" Type="String" />
                    <asp:Parameter DefaultValue="2" Name="EndDateTo" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsMeFileSubRequest" runat="server" SelectMethod="SelectObsDocSubRequest"
                TypeName="TSP.DataManager.DocMemberFileManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="-1" Name="MeId" SessionField="MeId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="TaskCode" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBMemberFile" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
                SelectMethod="GetData" TypeName="TSP.DataManager.MemberFileManager" UpdateMethod="Update"
                OldValuesParameterFormatString="original_{0}" FilterExpression="MeId={0}">
                <FilterParameters>
                    <asp:SessionParameter DefaultValue="-1" Name="MeId" SessionField="MeId" />
                </FilterParameters>
            </asp:ObjectDataSource>
            <asp:HiddenField ID="hfPageMode" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hfMfId" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hfMeId" runat="server"></asp:HiddenField>
            <dxhf:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
            </dxhf:ASPxHiddenField>
            <dxwgv:ASPxGridViewExporter ID="GridViewExporter" GridViewID="GridViewMemberFile"
                runat="server">
            </dxwgv:ASPxGridViewExporter>
            <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewMemberFile"
                SessionName="SendBackDataTable_EmpObsDoc" OnCallback="WFUserControl_Callback" />
            </div>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نماييد
                        <img alt="" src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
