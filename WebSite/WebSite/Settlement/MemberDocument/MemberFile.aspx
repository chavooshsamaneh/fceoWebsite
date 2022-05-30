<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberFile.aspx.cs" Inherits="Settlement_MemberDocument_MemberFile"
    Title="مدیریت پروانه اشتغال به کار" %>

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>




<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>


<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <dxhf:ASPxHiddenField ID="HiddenFieldMeFile" ClientInstanceName="HiddenFieldMeFile"
        runat="server">
    </dxhf:ASPxHiddenField>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                    ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnEdit_Click" CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
			                                                        if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	                                                        {
   		                                                        e.processOnServer=false;
   		                                                        alert(&quot;ردیفی انتخاب نشده است&quot;);
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
                                    ID="btnView" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnShow_Click" CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
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
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td style="vertical-align: top">
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
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/reload.png">
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
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/Cheque Status ReChange.png" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint" runat="server" AutoPostBack="False"
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../Print.aspx&quot;);
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
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
	GridViewMemberFile.PerformCallback('ReportAll');
 }
}" />
                                    <Image Url="~/Images/icons/printers2.png" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrePrint1" OnClick="btnPrePrint_Click" runat="server" AutoPostBack="true"
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ نسخه اولیه گواهینامه اشتغال به کار"
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}" />
                                    <Image Url="~/Images/icons/printCardRequest.png" />
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
                            <TSPControls:CustomTextBox ID="txtMeId" runat="server" ClientInstanceName="txtMeId"
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
                            <TSPControls:CustomTextBox ID="txtMFNo" runat="server" ClientInstanceName="txtMFNo"
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
                            <TSPControls:CustomTextBox ID="txtName" runat="server" ClientInstanceName="txtName"
                                Width="100%">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="نام خانوادگی">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="left">
                            <TSPControls:CustomTextBox ID="txtFamily" runat="server" ClientInstanceName="txtFamily"
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
                                Width="230px" onkeypress="SearchKeyPress(event,2,btnSearch);"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="تاریخ اعتبار تا" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox ID="txtEndDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                Width="230px" onkeypress="SearchKeyPress(event,2,btnSearch);"></pdc:PersianDateTextBox>
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
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="کد پیگیری" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox ID="txtFollowCode" runat="server" ClientInstanceName="txtFollowCode"
                                Width="100%">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <ValidationSettings>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel8" Wrap="False" runat="server" Text="نوع آخرین درخواست"
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
                        <td align="right">مرحله گردش کار</td>
                        <td align="right">
                            <TSPControls:CustomAspxComboBox ID="ComboBoxWorkFlow" runat="server"
                                ValueType="System.String"
                                RightToLeft="True" ClientInstanceName="ComboBoxWorkFlow" Width="100%" HorizontalAlign="Right"
                                EnableIncrementalFiltering="True" DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskCode">
                                <ItemStyle HorizontalAlign="Right" />
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
                        <td align="center" colspan="4" dir="ltr" valign="top">
                            <br />
                            <table>
                                <tr>
                                    <td style="width: 100px">
                                        <TSPControls:CustomAspxButton ID="btnClearSearch" runat="server" AutoPostBack="true" OnClick="btnSearch_OnClick"
                                            Text="پاک کردن فرم"
                                            UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s, e) {
	   	 ClearSearch();
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="width: 100px">
                                        <TSPControls:CustomAspxButton ID="btnSearch" runat="server" AutoPostBack="True"
                                            Text="جستجو"
                                            ClientInstanceName="btnSearch" Width="98px" UseSubmitBehavior="false" OnClick="btnSearch_OnClick">
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
    <%--  کدهای عضویت انتخاب شده :
                    <TSPControls:CustomASPXMemo runat="server" ID="txtMeList" ClientInstanceName="txtMeList" Width="100%" ssFilePath="~/App_Themes/Glass/{0}/styles.css"
                     Height="80px" ReadOnly="True"></TSPControls:CustomASPXMemo>
                    <br />--%>
    <TSPControls:CustomAspxDevGridView ID="GridViewMemberFile" runat="server" DataSourceID="ObjdsMemberFileMainRequest"
        Width="100%" AutoGenerateColumns="False" ClientInstanceName="GridViewMemberFile"
        KeyFieldName="MfId" OnCustomCallback="GridViewMemberFile_CustomCallback" OnHtmlRowPrepared="GridViewMemberFile_HtmlRowPrepared"
        OnAutoFilterCellEditorInitialize="GridViewMemberFile_AutoFilterCellEditorInitialize"
        OnHtmlDataCellPrepared="GridViewMemberFile_HtmlDataCellPrepared">
        <SettingsCustomizationWindow Enabled="True" />
        <ClientSideEvents EndCallback="function(s,e){
                  if(s.cpDoPrintAll==1)
            {
               s.cpDoPrintAll = 0;
               window.open(s.cpPrintAllPath);
               s.cpPrintAllPath='';
            }
            else if(s.cpDoPrePrint==1)            
            {
              s.cpDoPrePrint = 0;
               window.open(s.cpPrePrintPath);
               s.cpPrePrintPath='';
            }
        
        }" />
        <Templates>
            <DetailRow>
                <TSPControls:CustomAspxDevGridView ID="GridViewMeFileHistory" runat="server" Width="100%"
                    KeyFieldName="MfId" ClientInstanceName="GridViewMemberFile1" OnBeforePerformDataSelect="GridViewMeFileHistory_BeforePerformDataSelect"
                    DataSourceID="ObjdsMeFileSubRequest" OnAutoFilterCellEditorInitialize="GridViewMeFileHistory_AutoFilterCellEditorInitialize"
                    OnHtmlDataCellPrepared="GridViewMeFileHistory_HtmlDataCellPrepared">
                    <SettingsCookies Enabled="false" />
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
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MaxMfId" Visible="false">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ارسال کننده" FieldName="RequesterName" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="WFRequesterType" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="SerialNo" Caption="شماره سریال">
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MFNo" Caption="پروانه اشتغال">
                            <HeaderStyle Wrap="false" />
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="RegDate" Caption="تاریخ صدور">
                            <HeaderStyle Wrap="false" />
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ExpireDate" Caption="پایان اعتبار">
                            <HeaderStyle Wrap="false" />
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="MFType" Caption="وضعیت پروانه">
                            <HeaderStyle Wrap="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="LivertyDate"
                            Caption="تاریخ تحویل گواهینامه">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="wfDescriptionSummary" Caption="آخرین پانوشت پرونده">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="FollowCode" Caption="کد پیگیری"
                            Width="100px">
                            <HeaderStyle Wrap="False" />
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="InActives" Caption="وضعیت">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="9" Caption=" " ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <Settings ShowHorizontalScrollBar="True"></Settings>
                    <SettingsDetail IsDetailGrid="True"></SettingsDetail>
                </TSPControls:CustomAspxDevGridView>
            </DetailRow>
        </Templates>
        <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
        <Settings ShowHorizontalScrollBar="True"></Settings>
        <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" ExportMode="None"></SettingsDetail>
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
            <dxwgv:GridViewDataTextColumn Caption="آخرین درخواست" FieldName="LastRequsetTypeName"
                VisibleIndex="0" Width="130px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="0"
                Width="80px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1">
                <CellStyle HorizontalAlign="Right" Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام خانودگی" FieldName="LastName" VisibleIndex="2">
                <CellStyle HorizontalAlign="Right" Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره سریال" FieldName="SerialNo" VisibleIndex="2"
                Visible="False">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره پروانه" FieldName="MFNo" VisibleIndex="3">
                <HeaderStyle Wrap="True" />
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="رشته اصلی" FieldName="MjName" VisibleIndex="4"
                Width="100px">
                <CellStyle Wrap="False" HorizontalAlign="Right">
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
                Width="50px" Visible="true">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نقشه برداری" FieldName="MappingGrade" VisibleIndex="7"
                Width="50px" Visible="true">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ترافیک" FieldName="TrafficGrade" VisibleIndex="7"
                Width="50px" Visible="true">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="RegDate" Caption="تاریخ صدور"
                Width="80px">
                <FilterCellStyle CssClass="CellLeft">
                </FilterCellStyle>
                <CellStyle Wrap="False" CssClass="CellLeft">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="LastExpireDate" Caption="پایان اعتبار"
                Width="80px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت پروانه" FieldName="MFType" VisibleIndex="9"
                Visible="False">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActives" VisibleIndex="10">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="11" Width="30px" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>
    </TSPControls:CustomAspxDevGridView>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewMemberFile">
    </dxwgv:ASPxGridViewExporter>
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
                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="درخواست درجریان: ردیف آبی" ForeColor="SteelBlue"
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
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                    cellpadding="0">
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                    ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnEdit_Click" CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>

                                    <Image Url="~/Images/icons/edit.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                    ID="btnView2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnShow_Click" CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/view.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td style="vertical-align: top">
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

                                    <Image Url="~/Images/icons/reload.png">
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

                                    <Image Url="~/Images/icons/Cheque Status ReChange.png" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server" AutoPostBack="False"
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../Print.aspx&quot;);	
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
	GridViewMemberFile.PerformCallback('ReportAll');
 }
}" />

                                    <Image Url="~/Images/icons/printers2.png" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrePrint" OnClick="btnPrePrint_Click" runat="server" AutoPostBack="true"
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ نسخه اولیه گواهینامه اشتغال به کار"
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}" />
                                    <Image Url="~/Images/icons/printCardRequest.png" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی Excel"
                                    ID="ASPxButton2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnExportExcel_Click">
                                    <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>

                                    <Image Url="~/Images/icons/ExportExcel.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتخاب ستون ها"
                                    ID="btnChooseCln2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
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
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
        TypeName="TSP.DataManager.WorkFlowTaskManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsMemberFileMainRequest" runat="server" SelectMethod="SelectMainRequestForSettelment"
        TypeName="TSP.DataManager.DocMemberFileManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
            <asp:Parameter DefaultValue="%" Name="FollowCode" Type="String" />
            <asp:Parameter DefaultValue="1" Name="EndDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="EndDateTo" Type="String" />
            <asp:Parameter DefaultValue="%" Name="FirstName" Type="String" />
            <asp:Parameter DefaultValue="%" Name="LastName" Type="String" />
            <asp:Parameter DefaultValue="%" Name="MFNo" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="LastConfirmReqType" Type="String" />
            <asp:Parameter DefaultValue="%" Name="MFNoWithOutSerial" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="MFSerialNo" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="TaskCodeAccConf" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="TaskCode" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="WFDate" Type="String" />
            <asp:Parameter DefaultValue="2" Name="WFDateTo" Type="String" />
            <asp:Parameter DefaultValue="%" Name="WFDoerName" Type="String" />



        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsMeFileSubRequest" runat="server" SelectMethod="SelectSubRequest"
        TypeName="TSP.DataManager.DocMemberFileManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="-1" Name="MeId" SessionField="MeId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="TaskCode" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="DocType" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewMemberFile"
        SessionName="SendBackDataTable_StlMeFile" OnCallback="WFUserControl_Callback" />
</asp:Content>
