<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPagePortals.master"
    CodeFile="MemberFile.aspx.cs" Inherits="Employee_Document_MemberFile" Title="مدیریت پروانه اشتغال به کار" %>

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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>




<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>


<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script type="text/javascript" src="../Script/Utility.js">  
    </script>
    <script language="javascript">

        function ShowWFDesc(s, e) {
            GridViewMemberFile.GetRowValues(GridViewMemberFile.GetFocusedRowIndex(), 'wfDescription', OnGetSelectedFieldValues);

        }
        function OnGetSelectedFieldValues(selectedValues) {

            txtWFDesc.SetText(selectedValues);
            PopUpWFDesc.Show();
        }
    </script>
    <TSPControls:CustomAspxCallbackPanel ID="CallbackMeDoc" runat="server" ClientInstanceName="CallbackMeDoc"
        HideContentOnCallback="False"
        OnCallback="CallbackMeDoc_Callback" Width="100%">
        <SettingsLoadingPanel Text="در حال بارگذاری" />
        <LoadingPanelImage Url="~/Image/indicator.gif" />
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent1" runat="server">
                <div style="width: 100%" align="center">
                    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                        [<a class="closeLink" href="#">بستن</a>]
                    </div>
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu3" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <table dir="rtl" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                    ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    CausesValidation="False" AutoPostBack="False">

                                                    <Image Url="~/Images/icons/new.png">
                                                    </Image>
                                                    <ClientSideEvents Click="function(s, e) {
	CallbackMeDoc.PerformCallback('New');
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                    ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{
		CallbackMeDoc.PerformCallback('Edit');
	}
}"></ClientSideEvents>

                                                    <Image Url="~/Images/icons/edit.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                    ID="btnView" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{
	CallbackMeDoc.PerformCallback('View');
	}
}"
                                                        GotFocus="function(s, e) {	
}"></ClientSideEvents>

                                                    <Image Url="~/Images/icons/view.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="لغو درخواست"
                                                    ID="btnDelete" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{
    if(confirm('آیا مطمئن به لغو درخواست انتخاب شده هستید؟'))
		CallbackMeDoc.PerformCallback('Delete');
	}
}" />

                                                    <Image Url="~/Images/icons/delete.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست ارتقاء پایه"
                                                    ID="btnUpGrade" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False" AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {
                                                                if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
                                                                {
   		                                                                e.processOnServer=false;
   		                                                                alert(&quot;ردیفی انتخاب نشده است&quot;);
                                                                }
                                                                else
                                                                {
	                                                                CallbackMeDoc.PerformCallback('Upgrade');
                                                                }

                                                                }" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/UpGrade.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست تمدید"
                                                    ID="btnRevival" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
else
{
	CallbackMeDoc.PerformCallback('Revival');
}
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/Revival.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست درج صلاحیت جدید"
                                                    ID="btnQualification" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
else
{
	CallbackMeDoc.PerformCallback('Qualification');
}
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/Qualification.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست صدور المثنی"
                                                    ID="btnReDuplicate" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
else
{
	CallbackMeDoc.PerformCallback('ReDuplicate');
}

}" />

                                                    <Image Url="~/Images/icons/copy.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست تغییرات"
                                                    ID="btnChange" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
else
{
	CallbackMeDoc.PerformCallback('Change');
}
}" />

                                                    <Image Url="~/Images/icons/Change.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="صدور مجدد"
                                                    ID="btnReissues" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
else
{
	CallbackMeDoc.PerformCallback('Reissues');
}
}" />

                                                    <Image Url="~/Images/Reissues.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="تخصیص شماره پروانه عضو انتقالی"
                                                    ID="btnTransferedMemberRequest" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
else
{
	CallbackMeDoc.PerformCallback('TransferedMemberReq');
}
}" />

                                                    <Image Url="~/Images/icons/TransferMeDocRequest.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار"
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

                                                    <Image Url="~/Images/icons/reload.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing" runat="server" AutoPostBack="False"
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="پیگیری" UseSubmitBehavior="False"
                                                    CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	else
	{
	CallbackMeDoc.PerformCallback('Tracing');
	}
}" />

                                                    <Image Url="~/Images/icons/Cheque Status ReChange.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" ID="btnShowDetail" ButtonType="ShowDetail"
                                                    ToolTip="مشاهده آخرین پانوشت پرونده" AutoPostBack="false">
                                                    <ClientSideEvents Click="ShowWFDesc" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint" runat="server" AutoPostBack="False"
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	CallbackMeDoc.PerformCallback('Print');
}" />

                                                    <Image Url="~/Images/icons/printers.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnReportAll" runat="server" AutoPostBack="False"
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ خلاصه پروانه"
                                                    UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
 {
	e.processOnServer=false;
	CallbackMeDoc.PerformCallback('ReportAll');
 }
}" />

                                                    <Image Url="~/Images/icons/printers2.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrePrint" runat="server" AutoPostBack="False"
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ نسخه اولیه گواهینامه اشتغال به کار"
                                                    UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
 {
	e.processOnServer=false;
	CallbackMeDoc.PerformCallback('PrePrint');
 }
}" />

                                                    <Image Url="~/Images/icons/printCardRequest.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrintBreif" runat="server" AutoPostBack="False"
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ خلاصه پرونده اشتغال"
                                                    UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
 {
	e.processOnServer=false;
	CallbackMeDoc.PerformCallback('ReportBreif');
 }
}" />

                                                    <Image Url="~/Images/icons/printred.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی Excel"
                                                    ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnExportExcel_Click">
                                                    <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>

                                                    <Image Url="~/Images/icons/ExportExcel.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتخاب ستون ها"
                                                    ID="btnChooseCln" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    AutoPostBack="False" Visible="true">
                                                    <ClientSideEvents Click="function(s, e) {
	if(!GridViewMemberFile.IsCustomizationWindowVisible())
		GridViewMemberFile.ShowCustomizationWindow();
	else
		GridViewMemberFile.HideCustomizationWindow();
}" />

                                                    <Image Url="~/Images/icons/cursor-hand.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="left" style="width: 100%">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnHelp2" runat="server" CausesValidation="False"
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                                    Visible="true" AutoPostBack="false">
                                                    <Image Url="~/Images/Help.png" />

                                                    <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>
                    <br />
                    <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server"
                        Width="100%">
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
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
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
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
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
                                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="نام" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" ID="txtName" runat="server" ClientInstanceName="txtName"
                                                Width="100%">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td align="right">
                                            <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="نام خانوادگی">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="left">
                                            <TSPControls:CustomTextBox IsMenuButton="true" ID="txtFamily" runat="server" ClientInstanceName="txtFamily"
                                                Width="100%">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 104px">
                                            <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Text="تاریخ اعتبار از" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right">
                                            <pdc:PersianDateTextBox ID="txtEndDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                                Width="300px" onkeypress="SearchKeyPress(event,2,btnSearch);"></pdc:PersianDateTextBox>
                                        </td>
                                        <td align="right">
                                            <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="تاریخ اعتبار تا" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right">
                                            <pdc:PersianDateTextBox ID="txtEndDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                                Width="300px" onkeypress="SearchKeyPress(event,2,btnSearch);"></pdc:PersianDateTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>نوع آخرین درخواست</td>
                                        <td>
                                            <TSPControls:CustomAspxComboBox ID="CmbLastReqType" runat="server"
                                                ValueType="System.String"
                                                RightToLeft="True" ClientInstanceName="CmbLastReqType" Width="100%" HorizontalAlign="Right"
                                                EnableIncrementalFiltering="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                                <Items>
                                                    <dxe:ListEditItem Text="همه موارد" Value="-1" Selected="true" />
                                                    <dxe:ListEditItem Text="صدور پروانه" Value="0" />
                                                    <dxe:ListEditItem Text="تمدید" Value="1" />
                                                    <dxe:ListEditItem Text="تغییرات" Value="2" />
                                                    <dxe:ListEditItem Text="ارتقا" Value="3" />
                                                    <dxe:ListEditItem Text="ابطال" Value="4" />
                                                    <dxe:ListEditItem Text="انتقال-صدور" Value="5" />
                                                    <dxe:ListEditItem Text="صلاحیت" Value="6" />
                                                    <dxe:ListEditItem Text="المثنی" Value="7" />
                                                    <dxe:ListEditItem Text="صدور سیستم قدیم" Value="8" />
                                                    <dxe:ListEditItem Text="ارتقاء/تمدید سیستم قدیم" Value="9" />
                                                    <dxe:ListEditItem Text="انتقال-تمدید" Value="10" />
                                                </Items>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td align="right">
                                            <dxe:ASPxLabel ID="ASPxLabel8" Wrap="False" runat="server" Text="نوع آخرین درخواست تایید شده "
                                                Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right">
                                            <TSPControls:CustomAspxComboBox ID="CmbReqType" runat="server"
                                                ValueType="System.String"
                                                RightToLeft="True" ClientInstanceName="CmbReqType" Width="100%" HorizontalAlign="Right"
                                                EnableIncrementalFiltering="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                                <Items>
                                                    <dxe:ListEditItem Text="همه موارد" Value="-1" Selected="true" />
                                                    <dxe:ListEditItem Text="صدور پروانه" Value="0" />
                                                    <dxe:ListEditItem Text="تمدید" Value="1" />
                                                    <dxe:ListEditItem Text="تغییرات" Value="2" />
                                                    <dxe:ListEditItem Text="ارتقا" Value="3" />
                                                    <dxe:ListEditItem Text="ابطال" Value="4" />
                                                    <dxe:ListEditItem Text="انتقال-صدور" Value="5" />
                                                    <dxe:ListEditItem Text="صلاحیت" Value="6" />
                                                    <dxe:ListEditItem Text="المثنی" Value="7" />
                                                    <dxe:ListEditItem Text="صدور سیستم قدیم" Value="8" />
                                                    <dxe:ListEditItem Text="ارتقاء/تمدید سیستم قدیم" Value="9" />
                                                    <dxe:ListEditItem Text="انتقال-تمدید" Value="10" />
                                                </Items>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>نحوه پرداخت
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxComboBox ID="CmbPaymentType" runat="server"
                                                ValueType="System.String"
                                                RightToLeft="True" ClientInstanceName="CmbPaymentType" Width="100%" HorizontalAlign="Right"
                                                EnableIncrementalFiltering="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                                <Items>
                                                    <dxe:ListEditItem Text="همه موارد" Value="-1" Selected="true" />
                                                    <dxe:ListEditItem Text="فیش" Value="1" />
                                                    <dxe:ListEditItem Text="پرداخت الکترونیک" Value="4" />
                                                </Items>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td>وضعیت پرداخت
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxComboBox ID="CmbPaymentStatus" runat="server"
                                                ValueType="System.String"
                                                RightToLeft="True" ClientInstanceName="CmbPaymentStatus" Width="100%" HorizontalAlign="Right"
                                                EnableIncrementalFiltering="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                                <Items>
                                                    <dxe:ListEditItem Text="همه موارد" Value="-1" Selected="true" />
                                                    <dxe:ListEditItem Text="پرداخت" Value="3" />
                                                    <dxe:ListEditItem Text="عدم پرداخت" Value="1" />
                                                </Items>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>نقص پرونده عضویت
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxComboBox ID="CmbFaultMemberRegister" runat="server"
                                                ValueType="System.String"
                                                RightToLeft="True" ClientInstanceName="CmbFaultMemberRegister" Width="100%"
                                                HorizontalAlign="Right" EnableIncrementalFiltering="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                                <Items>
                                                    <dxe:ListEditItem Text="همه موارد" Value="-1" Selected="true" />
                                                    <dxe:ListEditItem Text="فاقد نقص" Value="0" />
                                                    <dxe:ListEditItem Text="دارای نقص" Value="1" />
                                                </Items>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td>نقص پرونده پروانه
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxComboBox ID="CmbFaultDocument" runat="server"
                                                ValueType="System.String"
                                                RightToLeft="True" ClientInstanceName="CmbFaultDocument" Width="100%" HorizontalAlign="Right"
                                                EnableIncrementalFiltering="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                                <Items>
                                                    <dxe:ListEditItem Text="همه موارد" Value="-1" Selected="true" />
                                                    <dxe:ListEditItem Text="فاقد نقص" Value="0" />
                                                    <dxe:ListEditItem Text="دارای نقص" Value="1" />
                                                </Items>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>مرحله
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxComboBox ID="CmbTask" runat="server"
                                                ValueType="System.String"
                                                TextField="TaskName" ValueField="TaskId" RightToLeft="True" ClientInstanceName="CmbTask"
                                                DataSourceID="ObjdsWorkFlowTask" Width="100%" HorizontalAlign="Right" EnableIncrementalFiltering="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td>آخرین بررسی کننده
                                        </td>
                                        <td>
                                            <TSPControls:CustomTextBox IsMenuButton="true" ID="txtEndAuditor" runat="server" ClientInstanceName="txtEndAuditor"
                                                Width="100%">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                                <ValidationSettings>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>تاریخ آخرین بررسی از
                                        </td>
                                        <td>
                                            <pdc:PersianDateTextBox ID="txtDateEndAuditor" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                                Width="300px" onkeypress="SearchKeyPress(event,2,btnSearch);" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                        </td>
                                        <td>تاریخ آخرین بررسی تا                                       
                                        </td>
                                        <td>
                                            <pdc:PersianDateTextBox ID="txtDateEndAuditorTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"
                                                Width="300px" onkeypress="SearchKeyPress(event,2,btnSearch);"></pdc:PersianDateTextBox>

                                            <pdc:PersianDateTextBox ID="txtDateRequstRegister" runat="server" DefaultDate=""
                                                IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" RightToLeft="False"
                                                Visible="false" Style="direction: ltr; text-align: right;" Width="300px" onkeypress="SearchKeyPress(event,2,btnSearch);"></pdc:PersianDateTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>تاریخ آخرین درخواست از
                                        </td>
                                        <td>
                                            <pdc:PersianDateTextBox ID="txtLastRequestCreateDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                                Width="300px" onkeypress="SearchKeyPress(event,2,btnSearch);" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                        </td>
                                        <td>تاریخ آخرین درخواست تا                               
                                        </td>
                                        <td>
                                            <pdc:PersianDateTextBox ID="txtLastRequestCreateDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"
                                                Width="300px" onkeypress="SearchKeyPress(event,2,btnSearch);"></pdc:PersianDateTextBox>

                                            <pdc:PersianDateTextBox ID="PersianDateTextBox3" runat="server" DefaultDate=""
                                                IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" RightToLeft="False"
                                                Visible="false" Style="direction: ltr; text-align: right;" Width="300px" onkeypress="SearchKeyPress(event,2,btnSearch);"></pdc:PersianDateTextBox>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td>رشته موضوع پروانه</td>
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
                                        <td>پایه</td>
                                        <td>
                                            <TSPControls:CustomAspxComboBox ID="comboGrade" runat="server"
                                                ValueType="System.String"
                                                TextField="GrdName" ValueField="GrdId" RightToLeft="True" ClientInstanceName="comboGrade"
                                                DataSourceID="ObjectDataSourceDocGrade" Width="100%" HorizontalAlign="Right" EnableIncrementalFiltering="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                            </TSPControls:CustomAspxComboBox>

                                            <asp:ObjectDataSource ID="ObjectDataSourceDocGrade" runat="server" SelectMethod="GetData"
                                                TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">کد پیگیری
                                        </td>
                                        <td align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" ID="txtFollowCode" runat="server" ClientInstanceName="txtFollowCode"
                                                Width="100%">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                                <ValidationSettings>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td>درخواست دهنده</td>
                                        <td>
                                            <TSPControls:CustomAspxComboBox ID="ComboRequesterType" runat="server"
                                                ValueType="System.String"
                                                RightToLeft="True" ClientInstanceName="ComboRequesterType" Width="100%" HorizontalAlign="Right"
                                                EnableIncrementalFiltering="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                                <Items>
                                                    <dxe:ListEditItem Text="همه موارد" Value="-1" Selected="true" />
                                                    <dxe:ListEditItem Text="کارمند" Value="0" />
                                                    <dxe:ListEditItem Text="عضو" Value="1" />
                                                </Items>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4" dir="ltr" valign="top">
                                            <br />
                                            <table>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton ID="ASPxButton1" runat="server" AutoPostBack="true" OnClick="btnSearch_OnClick"
                                                            Text="پاک کردن فرم" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e) {
	   	 ClearSearch();
}" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton ID="btnSearch" runat="server" AutoPostBack="true" OnClick="btnSearch_OnClick"
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


                    <TSPControls:CustomASPxRoundPanel ID="RoundPanelMeInfo" runat="server" Width="100%"
                        HeaderText="نکات مهم در صفحه مدیریت پروانه اشتغال بکار" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Collapsed="true">


                        <PanelCollection>

                            <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">

                                <p class="HelpUL">
                                    لازم است هر کاربر تمامی نکات زیر را مطالعه کرده و به صورت کامل متوجه شود.
                                </p>
                                <dt>انواع درخواست ها</dt>
                                <dd>تنها درصورت اعلام مفقودی گواهینامه از درخواست المثنی استفاده نمایید.</dd>
                                <dd>تنها در صورتی از درخواست صدور مجدد استفاده نمایید که اولین درخواست گواهینامه شخص
                                                            تایید نشده و فاقد اعتبار باشد.</dd>
                                <dd>درصورتی که قصد تغییرات جزیی در گواهینامه بدون تمدید را دارید از درخواست تغییرات
                                                            استفاده نمایید</dd>
                                <dd>درصورتی که شخصی انتقالی از سایر استان ها می باشد،جهت جلوگیری از اتلاف وقت اعضا دارا
                                                            بودن پروانه اشتغال در استان قبلی وی به صورت دقیق بررسی گردد.</dd>
                                <dd>درصورتی که شخص دارای پروانه اشتغال از سایر استان ها بوده و قصد تمدید پروانه وی را
                                                            دارید در صفحه مشخصات پروانه در قسمت "نوع درخواست انتقال" گزینه انتقال وتمدید را
                                                            انتخاب نمایید.</dd>
                                <dd>اگر درخواست تمدیدی صادر شود تاریخ صدور و تاریخ تمدید بر اساس تاریخ صدور تمدید در
                                                            پروانه اشتغال بکار چاپ خواهد شد</dd>
                                <dt>ارسال برای رفع نقص</dt>
                                <dd>در صورتی که پرونده عضو ، تنها دارای نقص در مدارک عضویت می باشد ، گردش کار را به
                                                            مرحله '' رفع نقص مدارک عضویت توسط عضو حقیقی'' ارسال نمایید.
                                </dd>
                                <dd>در صورتی که پرونده عضو دارای ''نقص در مدارک پروانه'' <b>و یا</b> ''نقص در مدارک
                                                            پروانه و عضویت '' می باشد ، گردش کار را به مرحله'' درخواست صدور پروانه اشتغال به
                                                            کار'' ارسال نمایید
                                </dd>
                                <dt>عمومی</dt>
                                <dd>اطلاعات نمایش داده شده در ردیف اصلی گرید زیر مربوط به آخرین درخواست تایید شده برای
                                                            پروانه مربوطه می باشد.</dd>
                            </dx:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanel>



                    <br />
                    <TSPControls:CustomAspxDevGridView ID="GridViewMemberFile" Width="100%" runat="server"
                        AutoGenerateColumns="False" ClientInstanceName="GridViewMemberFile" KeyFieldName="MfId"
                        OnCustomCallback="GridViewMemberFile_CustomCallback" DataSourceID="ObjdsMemberFileMainRequest"
                        OnHtmlRowPrepared="GridViewMemberFile_HtmlRowPrepared" Font-Size="8pt">
                        <SettingsCookies CookiesID="MeCookieId" StoreColumnsVisiblePosition="true" />
                        <ClientSideEvents DetailRowExpanding="function(s, e) {	
                            GridViewMemberFile.SetFocusedRowIndex(GridViewMemberFile.cpSelectedIndex);
}"></ClientSideEvents>
                        <Columns>

                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MaxMfId" Visible="false" ShowInCustomizationForm="false">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn Caption=" " ShowSelectCheckbox="true" Name="CheckBox" VisibleIndex="0"
                                Width="30px">
                            </dxwgv:GridViewCommandColumn>
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
                            <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="HasMeDataComplete" Caption="پرونده عضویت آخرین درخواست"
                                Width="150px">
                                <CellStyle Wrap="False" CssClass="CellLeft">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="HasMeDocComplete" Caption="مدارک پروانه آخرین درخواست"
                                Width="150px">
                                <CellStyle Wrap="False" CssClass="CellLeft">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="LastRequesterTypeName" Caption="درخواست دهنده"
                                Width="80px">
                                <CellStyle Wrap="False" CssClass="CellLeft">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="WFDate" Caption="تاریخ آخرین بررسی"
                                Width="80px">
                                <CellStyle Wrap="False" CssClass="CellLeft">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="WFDoerName" Caption="آخرین بررسی کننده"
                                Width="80px">
                                <CellStyle Wrap="False" CssClass="CellLeft">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>

                            <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="LastRequestCreateDate" Name="LastRequestCreateDate"
                                Caption="تاریخ ثبت آخرین درخواست" Width="80px">
                                <CellStyle Wrap="False" CssClass="CellLeft">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="0"
                                Width="60px">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FirstName" Caption="نام">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="LastName" Caption="نام خانودگی">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="PaymentTypeName" Caption="نحوه پرداخت">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="PaymentStatusName" Caption="وضعیت پرداخت">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="2" FieldName="SerialNo"
                                Caption="سریال گواهینامه">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="LivertyDate"
                                Caption="تاریخ تحویل گواهینامه">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataImageColumn Caption="تصویر" FieldName="MeImg" Visible="False"
                                VisibleIndex="2">
                                <PropertiesImage ImageHeight="100px" ImageWidth="100px">
                                </PropertiesImage>
                            </dxwgv:GridViewDataImageColumn>
                            <dxwgv:GridViewDataTextColumn Caption="سریال پروانه آخرین درخواست" FieldName="LasMfSerialNo" Name="''"
                                VisibleIndex="3">
                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="FileNo" Caption="شماره پروانه">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="رشته موضوع پروانه" FieldName="MjName" VisibleIndex="4">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نظارت" FieldName="ObsGrade" VisibleIndex="5"
                                Width="50px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="اجرا" FieldName="ImpGrade" VisibleIndex="6"
                                Width="50px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="طراحی" FieldName="DesGrade" VisibleIndex="7"
                                Width="50px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="شهرسازی" FieldName="UrbanismGrade" VisibleIndex="7"
                                Width="50px" Visible="false">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نقشه برداری" FieldName="MappingGrade" VisibleIndex="7"
                                Width="50px" Visible="false">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="ترافیک" FieldName="TrafficGrade" VisibleIndex="7"
                                Width="50px" Visible="false">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="RegDate" Caption="تاریخ صدور"
                                Width="80px">
                                <CellStyle Wrap="False" CssClass="CellLeft">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="LastExpireDate" Caption="پایان اعتبار"
                                Width="80px">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="10" FieldName="InActives" Caption="وضعیت"
                                Width="50px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="کدملی" FieldName="SSN" Visible="False" VisibleIndex="12">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="تاریخ تولد" FieldName="BirhtDate" Visible="False"
                                VisibleIndex="6">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="شماره شناسنامه" FieldName="IdNo" VisibleIndex="12"
                                Width="100px">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نام پدر" FieldName="FatherName" VisibleIndex="12"
                                Width="100px">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="آخرین درخواست تایید شده" FieldName="LastConfirmReqName"
                                VisibleIndex="12" Width="130px">
                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn VisibleIndex="14" Caption=" " Width="50px" ShowClearFilterButton="true">
                            </dxwgv:GridViewCommandColumn>
                        </Columns>
                        <Settings ShowHorizontalScrollBar="True"></Settings>
                        <SettingsCustomizationWindow Enabled="True" />
                        <SettingsCookies CookiesID="MeDocCookieId" Enabled="True" StoreFiltering="False"
                            StorePaging="False" />
                        <SettingsDetail ExportMode="None" ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
                        <Templates>
                            <DetailRow>
                                <TSPControls:CustomAspxDevGridView ID="GridViewMeFileHistory" Width="100%" runat="server"
                                    DataSourceID="ObjdsMeFileSubRequest" KeyFieldName="MfId" ClientInstanceName="GridViewMemberFile1"
                                    AutoGenerateColumns="False" OnBeforePerformDataSelect="GridViewMeFileHistory_BeforePerformDataSelect">
                                    <Columns>
                                        <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                                            VisibleIndex="0" Width="40px">
                                            <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                                                ValueType="System.String">
                                            </PropertiesComboBox>
                                            <DataItemTemplate>
                                                <div align="center">
                                                    <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>'>
                                                    </dxe:ASPxImage>
                                                </div>
                                            </DataItemTemplate>
                                        </dxwgv:GridViewDataComboBoxColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TaskName" Caption="نام مرحله"
                                            Width="110px">
                                            <HeaderStyle Wrap="False" />
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="RequeterTypeName" Caption="ثبت کننده درخواست"
                                            Width="110px">
                                            <HeaderStyle Wrap="False" />
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="CreateDate" Caption="تاریخ ثبت درخواست"
                                            Width="110px">
                                            <HeaderStyle Wrap="True"></HeaderStyle>
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="SerialNo" Caption="شماره سریال">
                                            <HeaderStyle Wrap="True" />
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MFNo" Caption="شماره پروانه"
                                            Width="100px">
                                            <HeaderStyle Wrap="True"></HeaderStyle>
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="RegDate" Caption="تاریخ صدور"
                                            Width="80px">
                                            <HeaderStyle Wrap="True"></HeaderStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ExpireDate" Caption="پایان اعتبار"
                                            Width="80px">
                                            <HeaderStyle Wrap="True"></HeaderStyle>
                                            <CellStyle Wrap="False" CssClass="CellLeft">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="MFType" Caption="نوع درخواست"
                                            Width="130px">
                                            <HeaderStyle Wrap="False" />
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="FollowCode" Caption="کد پیگیری"
                                            Width="100px">
                                            <HeaderStyle Wrap="False" />
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="ارسال کننده" FieldName="RequesterName" VisibleIndex="7">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="WFRequesterType" VisibleIndex="8">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>

                                        <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="InActives" Caption="وضعیت">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="wfDescriptionSummary" Caption="آخرین پانوشت پرونده">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="LivertyDate"
                                            Caption="تاریخ تحویل گواهینامه">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="DescriptionLiverty"
                                            Caption="توضیحات تحویل">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewCommandColumn VisibleIndex="11" Caption=" " Width="50px" ShowClearFilterButton="true">
                                        </dxwgv:GridViewCommandColumn>
                                    </Columns>
                                    <SettingsDetail IsDetailGrid="True"></SettingsDetail>
                                </TSPControls:CustomAspxDevGridView>
                            </DetailRow>
                        </Templates>
                    </TSPControls:CustomAspxDevGridView>
                    <br />

                    <fieldset width="100%">
                        <legend>راهنما</legend>

                        <ul class="HelpWorkflowTasksImages">
                            <li class="col-sm-4">
                                <ul>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel36" runat="server" Text="صدور: فونت مشکی" ForeColor="Black">
                                        </dxe:ASPxLabel>
                                    </li>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel37" runat="server" Text="تمدید: فونت سبز" ForeColor="Green">
                                        </dxe:ASPxLabel>
                                    </li>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel43" runat="server" Text="ارتقاء پایه: فونت آبی تیره" ForeColor="DarkBlue">
                                        </dxe:ASPxLabel>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                        <ul class="HelpWorkflowTasksImages">
                            <li class="col-sm-4">
                                <ul>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="درج صلاحیت جدید: فونت بنفش"
                                            ForeColor="DarkMagenta" Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </li>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel12" runat="server" Text="المثنی: فونت صورتی" ForeColor="Magenta">
                                        </dxe:ASPxLabel>
                                    </li>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel38" runat="server" Text="تغییرات: فونت قهوه ای" ForeColor="Brown"
                                            Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                        <ul class="HelpWorkflowTasksImages">
                            <li class="col-sm-4">
                                <ul>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel13" runat="server" Text="انتقال از دیگر استان ها: فونت طلایی"
                                            ForeColor="Gold" Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </li>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel14" runat="server" Text="ابطال: فونت قرمز" ForeColor="Red"
                                            Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </li>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="درخواست درجریان: ردیف آبی" ForeColor="SteelBlue"
                                            Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                        <ul class="HelpWorkflowTasksImages">
                            <li class="col-sm-4">
                                <ul>
                                    <asp:Repeater runat="server" ID="RepeaterWfHepPrint1">
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
                                    <asp:Repeater runat="server" ID="RepeaterWfHepPrint2">
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
                                    <asp:Repeater runat="server" ID="RepeaterWfHepPrint3">
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
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <table dir="rtl" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                    ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/new.png">
                                                    </Image>
                                                    <ClientSideEvents Click="function(s, e) {

		CallbackMeDoc.PerformCallback('New');

}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                    ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	else
	{
		CallbackMeDoc.PerformCallback('Edit');
	}
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/edit.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                    ID="btnView2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{
		CallbackMeDoc.PerformCallback('View');
	}
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/view.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="لغو درخواست"
                                                    ID="btnDelete2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{    
    if(confirm('آیا مطمئن به لغو درخواست انتخاب شده هستید؟'))
		CallbackMeDoc.PerformCallback('Delete');
	}
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/delete.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست ارتقاء پایه"
                                                    ID="btnUpGrade2" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
                                                                if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
                                                                {
   		                                                                e.processOnServer=false;
   		                                                                alert(&quot;ردیفی انتخاب نشده است&quot;);
                                                                }
                                                                else
                                                                {
	                                                                CallbackMeDoc.PerformCallback('Upgrade');
                                                                }

                                                                }" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/UpGrade.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست تمدید"
                                                    ID="btnRevival2" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
else
{
	CallbackMeDoc.PerformCallback('Revival');
}
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/Revival.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست درج صلاحیت جدید"
                                                    ID="btnQualification2" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
else
{
	CallbackMeDoc.PerformCallback('Qualification');
}
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/Qualification.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست صدور المثنی"
                                                    ID="btnReDuplicate2" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False" AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
else
{
	CallbackMeDoc.PerformCallback('ReDuplicate');
}

}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/copy.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست تغییرات"
                                                    ID="btnChange2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
                                                                if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
                                                                {
   		                                                                e.processOnServer=false;
   		                                                                alert(&quot;ردیفی انتخاب نشده است&quot;);
                                                                }
                                                                else
                                                                {
	                                                                CallbackMeDoc.PerformCallback('Change');
                                                                }
                                                                }" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/Change.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="صدور مجدد"
                                                    ID="btnReissues2" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
else
{
	CallbackMeDoc.PerformCallback('Reissues');
}
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/Reissues.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="تخصیص شماره پروانه عضو انتقالی"
                                                    ID="btnTransferedMemberRequest2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
else
{
	CallbackMeDoc.PerformCallback('TransferedMemberReq');
}
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/TransferMeDocRequest.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار"
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
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/reload.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing2" runat="server" AutoPostBack="False"
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="پیگیری" UseSubmitBehavior="False"
                                                    CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
	{
		CallbackMeDoc.PerformCallback('Tracing');
	}
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/Cheque Status ReChange.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" ID="btnShowDetail2" ButtonType="ShowDetail"
                                                    ToolTip="مشاهده آخرین پانوشت پرونده" AutoPostBack="false">
                                                    <ClientSideEvents Click="ShowWFDesc" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator7"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnprint2" runat="server" AutoPostBack="False"
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	CallbackMeDoc.PerformCallback('Print');	
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/printers.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnReportAll2" runat="server" AutoPostBack="False"
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ خلاصه پروانه"
                                                    UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
 {
	e.processOnServer=false;
	CallbackMeDoc.PerformCallback('ReportAll');
 }
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/printers2.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrePrint2" runat="server" AutoPostBack="False"
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ نسخه اولیه گواهینامه اشتغال به کار"
                                                    UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
 {
	e.processOnServer=false;
	CallbackMeDoc.PerformCallback('PrePrint');
 }
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/printCardRequest.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrintBreif2" runat="server" AutoPostBack="False"
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ خلاصه پرونده اشتغال"
                                                    UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
 {
	e.processOnServer=false;
	CallbackMeDoc.PerformCallback('ReportBreif');
 }
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/printred.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی Excel"
                                                    ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnExportExcel_Click">
                                                    <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/ExportExcel.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator8"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتخاب ستون ها"
                                                    ID="ASPxButton2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    AutoPostBack="False" Visible="true">
                                                    <ClientSideEvents Click="function(s, e) {
	if(!GridViewMemberFile.IsCustomizationWindowVisible())
		GridViewMemberFile.ShowCustomizationWindow();
	else
		GridViewMemberFile.HideCustomizationWindow();
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/cursor-hand.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="left" style="width: 100%">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnHelp" runat="server" CausesValidation="False"
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                                    Visible="true" AutoPostBack="false">
                                                    <Image Url="~/Images/Help.png" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>
                    <dxwgv:ASPxGridViewExporter ID="GridViewExporterDocMe" runat="server" GridViewID="GridViewMemberFile">
                    </dxwgv:ASPxGridViewExporter>
                    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
                        TypeName="TSP.DataManager.WorkFlowTaskManager">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsMemberFileMainRequest" runat="server" SelectMethod="SelectMainRequest"
                        TypeName="TSP.DataManager.DocMemberFileManager" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                            <asp:Parameter DefaultValue="0" Name="DocType" Type="Int32" />
                            <asp:Parameter DefaultValue="%" Name="FollowCode" Type="String" />
                            <asp:Parameter DefaultValue="1" Name="EndDateFrom" Type="String" />
                            <asp:Parameter DefaultValue="2" Name="EndDateTo" Type="String" />
                            <asp:Parameter DefaultValue="%" Name="FirstName" Type="String" />
                            <asp:Parameter DefaultValue="%" Name="LastName" Type="String" />
                            <asp:Parameter DefaultValue="%" Name="MFNo" Type="String" />
                            <asp:Parameter DefaultValue="-1" Name="LastConfirmReqType" Type="String" />
                            <asp:Parameter DefaultValue="-1" Name="LastRequsetType" Type="Int32" />
                            <asp:Parameter DefaultValue="-1" Name="MeDataComplete" Type="Int16" />
                            <asp:Parameter DefaultValue="-1" Name="MeDocComplete" Type="Int16" />
                            <asp:Parameter DefaultValue="1" Name="WFDate" Type="String" />
                            <asp:Parameter DefaultValue="2" Name="WFDateTo" Type="String" />
                            <asp:Parameter DefaultValue="%" Name="WFDoerName" Type="String" />
                            <asp:Parameter DefaultValue="-1" Name="PaymentType" Type="Int16" />
                            <asp:Parameter DefaultValue="-1" Name="PaymentStatus" Type="Int16" />
                            <asp:Parameter DefaultValue="1" Name="CreateDateLastRequst" Type="String" />
                            <asp:Parameter DefaultValue="-1" Name="TaskId" Type="Int16" />
                            <asp:Parameter DefaultValue="1" Name="LastRequestCreateDateFrom" Type="String" />
                            <asp:Parameter DefaultValue="2" Name="LastRequestCreateDateTo" Type="String" />
                            <asp:Parameter DefaultValue="-1" Name="GradeId" Type="Int16" />
                            <asp:Parameter DefaultValue="-1" Name="MjParentId" Type="Int16" />
                            <asp:Parameter DefaultValue="-1" Name="RequesterType" Type="Int16" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsMeFileSubRequest" runat="server" DeleteMethod="Delete"
                        SelectMethod="SelectSubRequest" TypeName="TSP.DataManager.DocMemberFileManager"
                        OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="-1" Name="MeId" SessionField="MeId" Type="Int32" />
                            <asp:Parameter DefaultValue="-1" Name="TaskCode" Type="Int32" />
                            <asp:Parameter DefaultValue="0" Name="DocType" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <dxhf:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
                    </dxhf:ASPxHiddenField>
                    <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewMemberFile"
                        SessionName="SendBackDataTable_EmpMeFile" OnCallback="WFUserControl_Callback" />
                    <TSPControls:CustomASPxPopupControl ID="PopUpWFDesc" runat="server" Width="387px" Height="500px"
                        ClientInstanceName="PopUpWFDesc"
                        AllowDragging="True" CloseAction="CloseButton"
                        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        HeaderText="آخرین پانوشت پرونده">
                        <ContentCollection>
                            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                <div width="100%" align="center">
                                    <TSPControls:CustomASPXMemo runat="server" Height="500px" ID="txtWFDesc" Width="387px"
                                        ClientInstanceName="txtWFDesc" ReadOnly="true">
                                    </TSPControls:CustomASPXMemo>
                                </div>
                            </dxpc:PopupControlContentControl>
                        </ContentCollection>
                    </TSPControls:CustomASPxPopupControl>
                </div>
            </dxp:PanelContent>
        </PanelCollection>
        <LoadingPanelStyle>
            <border borderstyle="Double" />
        </LoadingPanelStyle>
        <ClientSideEvents EndCallback="function(s, e) {
            if(s.cpDoPrint==1)
            {
                s.cpDoPrint = 0;
                window.open('../../Print.aspx');
            }

           if(s.cpDoPrintAll==1)
            {
               s.cpDoPrintAll = 0;
               window.open(s.cpPrintAllPath);
               s.cpPrintAllPath='';
            }

           if(s.cpDoPrePrint==1)
            {
               s.cpDoPrePrint = 0;
               window.open(s.cpPrePrintPath);
               s.cpPrePrintPath='';
            }
            
               }" />
    </TSPControls:CustomAspxCallbackPanel>
</asp:Content>
