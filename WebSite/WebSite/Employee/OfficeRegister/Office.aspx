<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Office.aspx.cs" Inherits="Employee_OfficeRegister_Office"
    Title="مديريت اعضای حقوقی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
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

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelPage" runat="server" ClientInstanceName="CallbackPanelPage"
        OnCallback="CallbackPanelPage_Callback" Width="100%">
        <PanelCollection>
            <dxp:PanelContent runat="server">
                <div id="DivReport" runat="server" class="DivErrors" align="right">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" EnableTheming="False"
                                            EnableViewState="true" OnClick="BtnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/new.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" EnableTheming="False"
                                            EnableViewState="true" AutoPostBack="false" Text=" " ToolTip="ویرایش"
                                            UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/edit.png" />
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
    CallbackPanelPage.PerformCallback('btnEdit');
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" EnableTheming="False"
                                            EnableViewState="true" AutoPostBack="false" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/view.png" />
                                            <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
    CallbackPanelPage.PerformCallback('btnView');
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableTheming="False"
                                            EnableViewState="true" AutoPostBack="false" Text=" " ToolTip="حذف درخواست" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
{
  if(confirm('آیا مطمئن به حذف این درخواست هستید؟'))
  {
        CallbackPanelPage.PerformCallback('btnDelete');
  }
}
}" />

                                            <Image Url="~/Images/icons/delete.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNewReq" runat="server" EnableTheming="False"
                                            EnableViewState="true" Text=" " ToolTip="درخواست تغییرات" UseSubmitBehavior="False"
                                            AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
    CallbackPanelPage.PerformCallback('btnNewReq');
}" />

                                            <Image Url="~/Images/icons/Write Document.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChangeMambers" runat="server" EnableTheming="False"
                                            EnableViewState="False" Text=" " ToolTip="درخواست اطلاعات پایه و سهامداران" UseSubmitBehavior="False"
                                            OnClick="btnChangeMambers_Click">
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
                                            <Image Url="~/Images/icons/Change.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChangeBaseInfo" runat="server"
                                            EnableTheming="False" EnableViewState="true" Text=" " ToolTip="درخواست تغییرات اطلاعات پایه"
                                            UseSubmitBehavior="False" OnClick="btnChangeBaseInfo_Click">
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
                                            <Image Url="~/Images/icons/ChangeBaseInfo.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInvalid" runat="server" EnableTheming="False"
                                            EnableViewState="true" Text=" " ToolTip="درخواست ابطال عضویت حقوقی" UseSubmitBehavior="False"
                                            OnClick="btnInvalid_Click">
                                            <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
  e.processOnServer= confirm('آیا مطمئن به باطل کردن این درخواست هستید؟');
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/button_cancel.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnReset1" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="true" Text=" " ToolTip="بازیابی رمز عبور"
                                            UseSubmitBehavior="False" OnClick="btnResetSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}" />

                                            <Image Url="~/Images/ChangePassword.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendToNextStep" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="true" Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
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
}" />

                                            <Image Url="~/Images/icons/reload.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="true" Text=" " ToolTip="پیگیری" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	else
 	    CallbackPanelPage.PerformCallback('btnTracing');
 	
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Cheque Status ReChange.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="true" AutoPostBack="false" Text=" " ToolTip="چاپ"
                                            UseSubmitBehavior="False" Visible="true">

                                            <Image Url="~/Images/icons/printers.png" />
                                            <ClientSideEvents Click="function(s,e){ 
CallbackPanelPage.PerformCallback('Print');
//window.open('../../Print.aspx'); 
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrintCard" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="true" Text=" " ToolTip="چاپ کارت موقت عضویت"
                                            UseSubmitBehavior="False" Visible="true" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	else
 	    CallbackPanelPage.PerformCallback('PrintCard');
 	
}" />

                                            <Image Url="~/Images/icons/Printers2.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="true" Text=" " ToolTip="خروجی Excel" UseSubmitBehavior="False"
                                            Visible="true" OnClick="btnExportExcel_Click">

                                            <Image Url="~/Images/icons/ExportExcel.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="left" style="width: 100%">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnHelp2" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="true" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                            Visible="true" AutoPostBack="false">
                                            <Image Url="~/Images/Help.png" />

                                            <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewOffice">
                </dxwgv:ASPxGridViewExporter>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table width="100%">
                                <tr>
                                    <td align="right" valign="top" width="15%">
                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="کد عضویت شرکت">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" width="35%">
                                        <TSPControls:CustomTextBox ID="txtOfId" runat="server" ClientInstanceName="txtOfId"
                                            Width="100%">
                                            <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="SearchValid">

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت را صحیح وارد نمایید" />
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top" width="15%">
                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="نام شرکت">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" width="35%">
                                        <TSPControls:CustomTextBox ID="txtOfName" runat="server" ClientInstanceName="txtOfName"
                                            Width="100%">
                                            <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="شماره پروانه">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox ID="txtFileNo" runat="server" ClientInstanceName="txtFileNo"
                                            Width="100%">
                                            <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="کد پیگیری">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox ID="txtFollowCode" runat="server" ClientInstanceName="txtFollowCode"
                                            Width="100%">
                                            <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Text="تاریخ اعتبار از">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox ID="txtEndDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                             onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="تاریخ اعتبار تا">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox ID="txtEndDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                             onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel13" runat="server" Text="تاریخ ثبت درخواست از">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox ID="txtCreateDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                             onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel14" runat="server" Text="تاریخ ثبت درخواست تا">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox ID="txtCreateDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                             onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                    </td>
                                </tr>


                                  <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="تاریخ آخرین درخواست از">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox ID="txtCreateDateLastReqFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                             onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel12" runat="server" Text="تاریخ آخرین درخواست تا">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox ID="txtCreateDateLastReqTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                             onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="کد عضویت شرکا">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox ID="txtMeId" runat="server" ClientInstanceName="txtMeId"
                                            Width="100%">
                                            <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="SearchValid">

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت را صحیح وارد نمایید" />
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>

                                    <td>مرحله(عضویت حقوقی)
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxComboBox ID="CmbTask" runat="server"
                                            ValueType="System.String"
                                            TextField="TaskName" ValueField="TaskId" RightToLeft="True" ClientInstanceName="CmbTask"
                                            DataSourceID="ObjdsWorkFlowTask" Width="100%" HorizontalAlign="Right" EnableIncrementalFiltering="True" SelectedIndex="0">
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
                                                <td style="width: 100px">
                                                    <TSPControls:CustomAspxButton ID="btnClearSearch" runat="server" OnClick="btnClearSearch_Click"
                                                        Text="پاک کردن فرم"
                                                        UseSubmitBehavior="false">
                                                        <ClientSideEvents Click="function(s, e) {
        Clear();
	//	grid.PerformCallback('Clear');
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td style="width: 100px">
                                                    <TSPControls:CustomAspxButton ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                                                        Text="جستجو"
                                                        ClientInstanceName="btnSearch" Width="98px" UseSubmitBehavior="false">
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
        // grid.PerformCallback('Search');
    }
}" />

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

                <ul class="HelpUL">
                    <li>درصورتی که درخواست از سمت واحد پروانه برای شرکتی ثبت شده باشد،امکان انجام عملیات
                        برروی آن شرکت توسط واحد عضویت وجود ندارد.</li>
                    <li>جهت ابطال فقط عضویت حقوقی یک شرکت از دکمه ابطال عضویت در همین صفحه استفاده نمایید
                        و جهت ابطال پروانه شخص حقوقی از ابطال پروانه در صفحه مدیریت پروانه شخص حقوقی استفاده
                        نمایید.</li>
                    <li>در درخواست تغییرات اطلاعات پایه تنها قادر به تغییر اطلاعات پایه شخص حقوقی می باشید.با
                        توجه به این که تغییر اطلاعاتی که در گواهینامه اشخاص ثبت می گردد نیازمند تایید مسکن
                        می باشد و "درخواست تغییرات اطلاعات پایه" شامل تاییدیه مسکن نمی باشد قادر به تغییر
                        این اطلاعات از قبیل <b>اعضای شرکت</b> نمی باشید. </li>
                    <li>در صورتی که وضعیت عضویت شرکت در حالت ''تایید مشروط'' باشد ، تنها اعضای غیرفعال شده
                        شرکت قادر به عضویت در سایر شرکت و یا دفاتر در سازمان می باشند و فعالیت سایر اعضای
                        شرکت تا مشخص شدن وضعیت شرکت و تایید مجدد آن منع می گردد </li>
                    <li>درخواست هایی که نیاز به تایید کارشناس مسکن ندارند عبارتند از:"درخواست تغییر اطلاعات پایه"،"درخواست ابطال عضویت"،"درخواست ابطال پروانه حقوقی"</li>
        <li>درخواست هایی که اعتبارسنجی وضعیت پروانه اعضای شرکت در آنها انجام نمی شود عبارتند از:"درخواست تغییرات اطلاعات پایه"،"درخواست ابطال عضویت"،"درخواست ابطال پروانه"،"درخواست تغییرات اطلاعات پایه و سهامدار"درخواست شامل تایید مشروط"</li>
                </ul>
                <TSPControls:CustomAspxDevGridView ID="GridViewOffice" runat="server" KeyFieldName="OfId"
                    DataSourceID="ObjdsOffice" Width="100%" ClientInstanceName="grid" OnHtmlRowPrepared="GridViewOffice_HtmlRowPrepared"
                    OnHtmlDataCellPrepared="GridViewOffice_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="GridViewOffice_AutoFilterCellEditorInitialize"
                    OnPageIndexChanged="GridViewOffice_PageIndexChanged" OnProcessColumnAutoFilter="GridViewOffice_ProcessColumnAutoFilter">
                    <SettingsCookies StoreColumnsVisiblePosition="true" StoreColumnsWidth="true" />
                    <Settings ShowHorizontalScrollBar="True" />
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" ExportMode="None" />
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
                        <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="OfId" Name="OfId" VisibleIndex="0">
                            <HeaderStyle Wrap="True"></HeaderStyle>
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام شرکت" FieldName="OfName" VisibleIndex="1"
                            Width="250px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام کاربری" FieldName="UserName" VisibleIndex="2">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="مدیر عامل" FieldName="MName" VisibleIndex="3">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تعداد اعضا" FieldName="MemberCount" VisibleIndex="3">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="زمینه موضوعی" FieldName="MembershipRequstTypeName" VisibleIndex="3">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره تماس" FieldName="Tel1" VisibleIndex="3"
                            Visible="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نوع شرکت" FieldName="OtName" Visible="true"
                            VisibleIndex="3">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نوع پروانه" FieldName="MFTypeName" Name="MFTypeName"
                            VisibleIndex="3" Width="130px">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نوع فعالیت" FieldName="ActivityTypeName" Name="ActivityTypeName"
                            VisibleIndex="3" Width="130px">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره ثبت" FieldName="RegOfNo" Name="RegOfNo"
                            VisibleIndex="4">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="محل ثبت" FieldName="RegPlace" VisibleIndex="5"
                            Visible="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت عضویت" FieldName="OffStatus" VisibleIndex="8"
                            Width="100px">
                            <CellStyle HorizontalAlign="Center" Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت پروانه" FieldName="DocumentStatusName"
                            VisibleIndex="8" Width="100px">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ عضویت" FieldName="CreateDate" VisibleIndex="5"
                            Name="CreateDate">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
                            <%--    <ClearFilterButton Visible="True">
                            </ClearFilterButton>--%>
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	if(grid.cpIsReturn!=1)
	{
		grid.cpSelectedIndex=grid.GetFocusedRowIndex();			
	}
	else
	{
		grid.cpIsReturn=0;	
	}
	    if(grid.cpIsPostBack==0)
		    grid.ExpandDetailRow(grid.cpSelectedIndex);
		else
		    grid.cpIsPostBack=0
}"
                        DetailRowExpanding="function(s, e) {
	if(grid.cpIsReturn!=1)
	{
		grid.cpSelectedIndex=grid.GetFocusedRowIndex();
	}
	else
	{
		grid.cpIsReturn=0;	
	}		
		grid.SetFocusedRowIndex(grid.cpSelectedIndex);
}" />
                    <Templates>
                        <DetailRow>
                            <div align="center">
                                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridViewRequest" runat="server"
                                    ClientInstanceName="Reqgrid" DataSourceID="OdbRequest" KeyFieldName="OfReId"
                                    OnBeforePerformDataSelect="CustomAspxDevGridViewRequest_BeforePerformDataSelect"
                                    Width="100%" OnHtmlDataCellPrepared="CustomAspxDevGridViewRequest_HtmlDataCellPrepared"
                                    OnAutoFilterCellEditorInitialize="CustomAspxDevGridViewRequest_AutoFilterCellEditorInitialize">
                                    <Settings ShowHorizontalScrollBar="false" />
                                    <SettingsCookies StoreColumnsVisiblePosition="true" StoreColumnsWidth="true" />
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
                                        <dxwgv:GridViewDataTextColumn FieldName="OfReId" Name="OfReId" Visible="False" VisibleIndex="0">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نوع" FieldName="TypeName" VisibleIndex="0">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="درخواست دهنده" FieldName="RequesterType" VisibleIndex="1">
                                            <CellStyle Wrap="True">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="ارسال کننده" FieldName="RequesterName" VisibleIndex="2">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="WFRequesterType" VisibleIndex="3">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="CreateDate" VisibleIndex="4"
                                            Name="CreateDate">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="SerialNo" Caption="شماره سریال">
                                            <HeaderStyle Wrap="True" />
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="MFNo" Caption="شماره پروانه "
                                            Width="150px" Name="MFNo">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="MFTypeName" Caption="نوع پروانه">
                                            <HeaderStyle Wrap="True" />
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نوع تأیید" FieldName="Confirm" Visible="False"
                                            VisibleIndex="8">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="تاریخ پاسخ" FieldName="AnswerDate" VisibleIndex="8"
                                            Name="AnswerDate">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="OfId" Visible="False"
                                            VisibleIndex="12">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Type" Visible="False" VisibleIndex="12">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="12" FieldName="RegDate" Caption="تاریخ صدور"
                                            Visible="False">
                                            <HeaderStyle Wrap="True"></HeaderStyle>
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="12" FieldName="ExpireDate" Caption="تاریخ پایان اعتبار"
                                            Visible="False">
                                            <HeaderStyle Wrap="True"></HeaderStyle>
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="11" FieldName="InActive" Caption="وضعیت"
                                            Visible="False">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewCommandColumn VisibleIndex="9" Caption=" " Width="30px" ShowClearFilterButton="true">
                                            <%--<ClearFilterButton Visible="True">
                                            </ClearFilterButton>--%>
                                        </dxwgv:GridViewCommandColumn>
                                    </Columns>
                                </TSPControls:CustomAspxDevGridView>
                            </div>
                        </DetailRow>
                    </Templates>
                </TSPControls:CustomAspxDevGridView>
        

                <fieldset width="98%">
                    <legend>راهنما</legend>
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td valign="middle" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel43" runat="server" Text="درخواست تغییرات عضویت: فونت زیتونی"
                                        ForeColor="Olive">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="middle" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="درخواست تغییرات پروانه : فونت آبی تیره"
                                        ForeColor="DarkBlue">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel38" runat="server" Text="عضویت باطل شده: فونت قهوه ای"
                                        ForeColor="Brown">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="middle" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="درخواست ابطال عضویت : فونت قرمز"
                                        ForeColor="Red">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="درخواست تغییرات اطلاعات پایه : فونت صورتی"
                                        ForeColor="DeepPink">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="middle" align="right">
                                           <dxe:ASPxLabel ID="ASPxLabel30" runat="server" Text="تایید مشروط : آبی آسمانی کمرنگ" ForeColor="LightSkyBlue">
                        </dxe:ASPxLabel>
                                        </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Image ID="Image3" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/OffMe_WFStart.png" />
                                    <dxe:ASPxLabel ID="ASPxLabel17" runat="server" Text="درخواست ثبت اطلاعات شخص حقوقی"
                                        ForeColor="Black" Font-Bold="False">
                                    </dxe:ASPxLabel>
                                </td>
                                <td>
                                    <asp:Image ID="Image6" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/OffMe_MembershipEmployeeConfirmating.png" />
                                    <dxe:ASPxLabel ID="ASPxLabel20" runat="server" Text="تایید کارمند واحد عضویت"
                                        ForeColor="Black" Font-Bold="False">
                                    </dxe:ASPxLabel>
                                </td>
                                <td>
                                    <asp:Image ID="Image10" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/OffMe_TaeedModirOmorEdari.png" />
                                    <dxe:ASPxLabel ID="ASPxLabel22" runat="server" Text="تایید مدیر امور اداری"
                                        ForeColor="Black" Font-Bold="False">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Image ID="Image13" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/OffMe_TaeedModirEjraee.png" />
                                    <dxe:ASPxLabel ID="ASPxLabel27" runat="server" Text="تایید مدیر اجرایی سازمان"
                                        ForeColor="Black" Font-Bold="False">
                                    </dxe:ASPxLabel>
                                </td>
                                <td>
                                    <asp:Image ID="Image14" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WFConfirmAndEnd.png" />
                                    <dxe:ASPxLabel ID="ASPxLabel28" runat="server" Text="تایید و پایان بررسی درخواست ثبت شخص حقوقی"
                                        ForeColor="Black" Font-Bold="False">
                                    </dxe:ASPxLabel>
                                </td>
                                <td>
                                    <asp:Image ID="Image15" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WFREjectAndEnd.png" />
                                    <dxe:ASPxLabel ID="ASPxLabel29" runat="server" Text="عدم تایید و پایان بررسی درخواست ثبت شخص حقوقی"
                                        ForeColor="Black" Font-Bold="False">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <strong>*راهنمای گردش کار پروانه حقوقی</strong>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle" align="right">
                                    <asp:Image ID="Image2" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/MeDoc_WFStart.png" />
                                    <dxe:ASPxLabel ID="ASPxLabel15" runat="server" Text="درخواست صدور پروانه شخص حقوقی"
                                        ForeColor="Black" Font-Bold="False">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="middle" align="right">
                                    <asp:Image ID="Image1" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/OffDoc_DocumentUnitEmployee.png" />
                                    <dxe:ASPxLabel ID="ASPxLabel16" runat="server" Text="بررسی و تایید درخواست توسط مسئول واحد پروانه"
                                        ForeColor="Black" Font-Bold="False">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="middle" align="right">
                                    <asp:Image ID="Image4" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/OffDoc_settlementAgentConfirming.png" />
                                    <dxe:ASPxLabel ID="ASPxLabel18" runat="server" Text="تایید کارشناس مسکن" ForeColor="Black"
                                        Font-Bold="False">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle" align="right">
                                    <asp:Image ID="Image5" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/OffDoc_ NezamEmployeeInSettlement.png" />
                                    <dxe:ASPxLabel ID="ASPxLabel19" runat="server" Text="تایید رئیس اداره توسعه مهندسی و نظارت بر مقررات ملی و کیفیت ساخت"
                                        ForeColor="Black" Font-Bold="False">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="middle" align="right">
                                    <asp:Image ID="Image12" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/OffDoc_RoadAndurbanism.png" />
                                    <dxe:ASPxLabel ID="ASPxLabel23" runat="server" Text="تایید معاون شهرسازی و معماری اداره کل راه و شهرسازی"
                                        ForeColor="Black" Font-Bold="False">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="middle" align="right">
                                    <asp:Image ID="Image7" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/OffDoc_PrintDoc.png" />
                                    <dxe:ASPxLabel ID="ASPxLabel21" runat="server" Text="چاپ گواهینامه توسط کارشناس واحد پروانه"
                                        ForeColor="Black" Font-Bold="False">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle" align="right">

                                    <asp:Image ID="Image11" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/OffDoc_PrintAndWaitForConfirm.png" />
                                    <dxe:ASPxLabel ID="ASPxLabel24" runat="server" Text="چاپ شده و منتظر تایید نهایی"
                                        ForeColor="Black" Font-Bold="False">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="middle" align="right">
                                    <asp:Image ID="Image8" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WFConfirmAndEnd.png" />
                                    <dxe:ASPxLabel ID="ASPxLabel25" runat="server" Text="تایید و پایان بررسی صدور پروانه شخص حقوقی"
                                        ForeColor="Black" Font-Bold="False">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="middle" align="right">
                                    <asp:Image ID="Image9" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WFREjectAndEnd.png" />
                                    <dxe:ASPxLabel ID="ASPxLabel26" runat="server" Text="عدم تایید وپایان بررسی صدور پروانه شخص حقوقی"
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
                            <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server" EnableTheming="False"
                                            EnableViewState="true" OnClick="BtnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/new.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" EnableTheming="False"
                                            EnableViewState="true" AutoPostBack="false" Text=" " ToolTip="ویرایش"
                                            UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/edit.png" />
                                            <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else   
    CallbackPanelPage.PerformCallback('btnEdit');
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server" EnableTheming="False"
                                            EnableViewState="true" AutoPostBack="false" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/view.png" />
                                            <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
    CallbackPanelPage.PerformCallback('btnView');
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableTheming="False"
                                            EnableViewState="true" AutoPostBack="false" Text=" " ToolTip="حذف درخواست" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
    if(confirm('آیا مطمئن به حذف این درخواست هستید؟'))
        CallbackPanelPage.PerformCallback('btnDelete');
}" />

                                            <Image Url="~/Images/icons/delete.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNewReq1" runat="server" EnableTheming="False"
                                            EnableViewState="true" Text=" " ToolTip="درخواست تغییرات" UseSubmitBehavior="False"
                                            AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
    CallbackPanelPage.PerformCallback('btnNewReq');
}" />

                                            <Image Url="~/Images/icons/Write Document.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChangeMambers2" runat="server" EnableTheming="False"
                                            EnableViewState="False" Text=" " ToolTip="درخواست اطلاعات پایه و سهامداران" UseSubmitBehavior="False"
                                            OnClick="btnChangeMambers_Click">
                                            <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />

                                            <Image Url="~/Images/icons/Change.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChangeBaseInfo2" runat="server"
                                            EnableTheming="False" EnableViewState="true" Text=" " ToolTip="درخواست تغییرات اطلاعات پایه"
                                            UseSubmitBehavior="False" OnClick="btnChangeBaseInfo_Click">
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />

                                            <Image Url="~/Images/icons/ChangeBaseInfo.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInvalid2" runat="server" EnableTheming="False"
                                            EnableViewState="true" Text=" " ToolTip="درخواست ابطال عضویت حقوقی" UseSubmitBehavior="False"
                                            OnClick="btnInvalid_Click">
                                            <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
  e.processOnServer= confirm('آیا مطمئن به باطل کردن این درخواست هستید؟');
}" />

                                            <Image Url="~/Images/icons/button_cancel.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnReset" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="true" Text=" " ToolTip="بازیابی رمز عبور"
                                            UseSubmitBehavior="False" OnClick="btnResetSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}" />
                                            <Image Url="~/Images/ChangePassword.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator7"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendToNextStep2" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="true" Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
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
}" />

                                            <Image Url="~/Images/icons/reload.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing2" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="true" Text=" " ToolTip="پیگیری" UseSubmitBehavior="False">
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
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator8"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint2" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="true" AutoPostBack="false" Text=" " ToolTip="چاپ"
                                            UseSubmitBehavior="False" Visible="true">

                                            <Image Url="~/Images/icons/printers.png" />
                                            <ClientSideEvents Click="function(s,e){ 
CallbackPanelPage.PerformCallback('Print');
//window.open('../../Print.aspx'); 
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrintCard2" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="true" Text=" " ToolTip="چاپ کارت موقت عضویت"
                                            UseSubmitBehavior="False" Visible="true" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	else
 	    CallbackPanelPage.PerformCallback('PrintCard');
 	
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Printers2.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="true" Text=" " ToolTip="خروجی Excel" UseSubmitBehavior="False"
                                            Visible="true" OnClick="btnExportExcel_Click">
                                            <ClientSideEvents Click="function(s,e){  }" />

                                            <Image Url="~/Images/icons/ExportExcel.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="left" style="width: 100%">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnHelp" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="true" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                            Visible="true" AutoPostBack="false">
                                            <Image Url="~/Images/Help.png" />

                                            <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <dx:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
                </dx:ASPxHiddenField>
                <asp:ObjectDataSource ID="ObjdsOffice" runat="server" SelectMethod="SelectOfficeForOfficeManagmentPage"
                    TypeName="TSP.DataManager.OfficeManager">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="%" Name="FollowCode" Type="String" />
                        <asp:Parameter DefaultValue="1" Name="EndDateFrom" Type="String" />
                        <asp:Parameter DefaultValue="2" Name="EndDateTo" Type="String" />
                        <asp:Parameter DefaultValue="%" Name="OfName" Type="String" />
                        <asp:Parameter DefaultValue="%" Name="FileNo" Type="String" />
                        <asp:Parameter DefaultValue="-1" Name="OfId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="TaskId" Type="Int32" />
                        <asp:Parameter DefaultValue="1" Name="CreateDateFrom" Type="String" />
                        <asp:Parameter DefaultValue="2" Name="CreateDateTo" Type="String" />
                        <asp:Parameter DefaultValue="1" Name="CreateDateLastReqFrom" Type="String" />
                        <asp:Parameter DefaultValue="2" Name="CreateDateLastReqTo" Type="String" />
                        
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="OdbRequest" runat="server" SelectMethod="FindByOfficeId"
                    TypeName="TSP.DataManager.OfficeRequestManager">
                    <SelectParameters>
                        <asp:SessionParameter Name="OfId" SessionField="OfficeId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="IsConfirm" Type="Int16" />
                        <asp:Parameter DefaultValue="-1" Name="Type" Type="Int16" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCodeList"
                    TypeName="TSP.DataManager.WorkFlowTaskManager">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="0" Name="WorkFlowCodeList" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <uc1:WFUserControl ID="WFUserControl" runat="server" OnCallback="WFUserControl_Callback"
                    GridName="grid" SessionName="SendBackDataTable_Off" />
            </dxp:PanelContent>
        </PanelCollection>
        <ClientSideEvents EndCallback="function(s, e) {
            if(CallbackPanelPage.cpDoPrint==1)
            {
                CallbackPanelPage.cpDoPrint = 0;
                window.open('../../Print.aspx');
            }
            if(s.cpReqType=='PrintCard'){
              if(s.cpReqvalue!='')
                window.open(s.cpReqValue);  
                }
                else if(s.cpReqType=='Message')  {
                  if(s.cpReqvalue!='')
                    alert(s.cpReqvalue);
                } 
            //if(CallbackPanelPage.cpCall==1)
            //{
                //alert(CallbackPanelPage.cpRedirectUrl);
                //CallbackPanelPage.cpCall=0;
                //window.location.href = CallbackPanelPage.cpRedirectUrl;
            //}
}"
            CallbackError="function(s, e) {
}" />
    </TSPControls:CustomAspxCallbackPanel>
</asp:Content>
