<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberCards.aspx.cs" Inherits="Employee_MembersRegister_MemberCards"
    Title="مدیریت صدور کارت عضویت" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
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
        function ShowWFDesc(s, e) {
            GridViewMemberFile.GetRowValues(GridViewMemberFile.GetFocusedRowIndex(), 'wfDescription', OnGetSelectedFieldValues);

        }
        function OnGetSelectedFieldValues(selectedValues) {

            txtWFDesc.SetText(selectedValues);
            PopUpWFDesc.Show();
        }
    </script>
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
            
                                <table >
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                    ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="BtnNew_Click">
                                               
                                                    <Image  Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>

                                            </td>
                                            <td>
                                             
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                    CausesValidation="False" AutoPostBack="true" UseSubmitBehavior="False" Width="25px" ID="btnEdit" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnEdit_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberCardClient.GetFocusedRowIndex()&lt;0)
 	{
  		 e.processOnServer=false;
  		 alert(&quot;ردیفی انتخاب نشده است&quot;);
  return;
                                                        
 }
   e.processOnServer=true;
}
"></ClientSideEvents>
                                                   
                                                    <Image  Url="~/Images/icons/edit.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                    ID="btnView" EnableViewState="False" UseSubmitBehavior="False" EnableTheming="False" ClientInstanceName="btnViewClient"
                                                    OnClick="btnView_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberCardClient.GetFocusedRowIndex()&lt;0)
 	{
  		 e.processOnServer=false;
  		 alert(&quot;ردیفی انتخاب نشده است&quot;);
 	 return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                                  
                                                    <Image  Url="~/Images/icons/view.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px" 
                                                    ToolTip="لغو درخواست" UseSubmitBehavior="False" CausesValidation="False" ID="btnDisActive" EnableClientSideAPI="True"
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnDisActive_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	 
if (GridViewMemberCardClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
  return;
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                   
                                                    <Image  Url="~/Images/icons/delete.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گردش کار"
                                                    ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberCardClient.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
      return;
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
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="پیگیری"
                                                    ID="btnTracing" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnTracing_Click">
                                                    <ClientSideEvents Click="function(s, e) {
            
	if (GridViewMemberCardClient.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	 return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                                  
                                                    <Image  Url="~/Images/icons/Cheque Status ReChange.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                                    ID="btnPrint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s,e){GridViewMemberCardClient.PerformCallback('Print'); }"></ClientSideEvents>
                                                   
                                                    <Image  Url="~/Images/icons/Printers.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                                    ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnExportExcel_Click">
                                                    <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                                   
                                                    <Image  Url="~/Images/icons/ExportExcel.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnHelp2" runat="server" CausesValidation="False" 
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                                    Visible="true" AutoPostBack="false">
                                                    <Image Height="25px" Url="~/Images/Help.png" Width="25px" />
                                                    
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
                        <td align="right" style="width: 15%"></td>
                        <td align="right" style="width: 35%"></td>
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
                        <td align="right" >
                            <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Text="تاریخ ثبت از" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" >
                            <pdc:PersianDateTextBox ID="txtCreateDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                Width="300px" onkeypress="SearchKeyPress(event,2,btnSearch);"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="تاریخ ثبت تا" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox ID="txtCreateDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                Width="300px" onkeypress="SearchKeyPress(event,2,btnSearch);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="تاریخ چاپ از" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox ID="txtPrintDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                Width="300px" onkeypress="SearchKeyPress(event,2,btnSearch);"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="تاریخ چاپ تا" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox ID="txtPrintDateto" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                Width="300px" onkeypress="SearchKeyPress(event,2,btnSearch);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>وضعیت چاپ
                        </td>
                        <td>
                            <TSPControls:CustomAspxComboBox ID="CmbIsPrinted" runat="server" 
                                  ValueType="System.String"
                                RightToLeft="True" ClientInstanceName="CmbIsPrinted" Width="100%" HorizontalAlign="Right"
                                EnableIncrementalFiltering="True">
                                <ItemStyle HorizontalAlign="Right" />
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <Items>
                                    <dxe:ListEditItem Text="همه موارد" Value="-1" Selected="true" />
                                    <dxe:ListEditItem Text="چاپ شده" Value="1" />
                                    <dxe:ListEditItem Text="چاپ نشده" Value="0" />
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td>علت درخواست
                        </td>
                        <td>
                            <TSPControls:CustomAspxComboBox ID="CmbMeCrdType" runat="server" 
                                  ValueType="System.String"
                                RightToLeft="True" ClientInstanceName="CmbMeCrdType" Width="100%" HorizontalAlign="Right"
                                EnableIncrementalFiltering="True">
                                <ItemStyle HorizontalAlign="Right" />
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <Items>
                                    <dxe:ListEditItem Text="همه موارد" Value="-1" Selected="true" />
                                    <dxe:ListEditItem Text="جدید" Value="0" />
                                    <dxe:ListEditItem Text="مخدوش" Value="1" />
                                    <dxe:ListEditItem Text="عیب فنی" Value="2" />
                                    <dxe:ListEditItem Text="تغییر" Value="3" />
                                    <dxe:ListEditItem Text="مفقود" Value="4" />
                                    <dxe:ListEditItem Text="موقت" Value="5" />
                                    <dxe:ListEditItem Text="دریافت پروانه اشتغال" Value="6" />
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                    </tr>

                    <tr>
                        <td align="center" colspan="4" dir="ltr" valign="top">
                            <br />
                            <table>
                                <tr>
                                    <td >
                                        <TSPControls:CustomAspxButton   ID="ASPxButton1" runat="server" AutoPostBack="true" OnClick="btnSearch_OnClick"
                                              
                                            Text="پاک کردن فرم" UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s, e) {
	   	 ClearSearch();
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
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
    <dx:ASPxGridViewExporter ID="GridViewExporter" GridViewID="GridViewMemberCard" runat="server">
    </dx:ASPxGridViewExporter>
    <TSPControls:CustomAspxDevGridView ID="GridViewMemberCard" runat="server" DataSourceID="ObjdsMeCards"
        Width="100%"  
        OnHtmlDataCellPrepared="GridViewMemberCard_HtmlDataCellPrepared" EnableViewState="False"
        KeyFieldName="MeCrdId" ClientInstanceName="GridViewMemberCardClient" AutoGenerateColumns="False"
        OnDetailRowExpandedChanged="GridViewMemberCard_DetailRowExpandedChanged" OnHtmlRowPrepared="GridViewMemberCard_HtmlRowPrepared"
        OnFocusedRowChanged="GridViewMemberCard_FocusedRowChanged" OnAutoFilterCellEditorInitialize="GridViewMemberCard_AutoFilterCellEditorInitialize"
        OnCustomCallback="GridViewMemberCard_CustomCallback1" RightToLeft="True">
        <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True" ColumnResizeMode="Control"></SettingsBehavior>
        <SettingsCookies Enabled="true" />
        <Columns>
            <dxwgv:GridViewDataComboBoxColumn FieldName="TaskId" Caption="مرحله" Name="WFState"
                VisibleIndex="0">
                <PropertiesComboBox ValueType="System.String" TextField="TaskName" DataSourceID="ObjdsWorkFlowTask"
                    ValueField="TaskId">
                </PropertiesComboBox>
                <DataItemTemplate>
                    <div align="center">
                        <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl="~/Images/WFStart.png">
                        </dxe:ASPxImage>
                    </div>
                </DataItemTemplate>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ چاپ" FieldName="PrintDate" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت چاپ" FieldName="PrintStatus" VisibleIndex="0">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="علت درخواست" FieldName="EventType" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="1">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="2">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="3">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره عضویت" FieldName="MeNo" VisibleIndex="4">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption=" شماره پروانه" FieldName="FileNo" VisibleIndex="5">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="CreateDate" VisibleIndex="6">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ارسال کننده" FieldName="RequesterName" VisibleIndex="7">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="WFRequesterType" VisibleIndex="9">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="مرحله" FieldName="TaskName" Visible="False"
                VisibleIndex="10">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " Width="30px" VisibleIndex="12" ShowClearFilterButton="true">
            
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowHorizontalScrollBar="True"></Settings>
        <SettingsDetail AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
        <ClientSideEvents EndCallback="function(s, e) {
	 if(s.cpPrint==1)
        {
            window.open('../../Print.aspx');
           s.cpPrint=0;
        }
}" />
        <Images >
        </Images>
    </TSPControls:CustomAspxDevGridView>
    <br />
    <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewMemberCardClient"
        SessionName="SendBackDataTable_MeCWF" OnCallback="WFUserControl_Callback" />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>

                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                    ID="btnNew1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="BtnNew_Click">
                                                  
                                                    <Image  Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>

                                            </td>
                                            <td>
                                                  <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                    CausesValidation="False" AutoPostBack="true" UseSubmitBehavior="False" Width="25px" ID="btnEdit1" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnEdit_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberCardClient.GetFocusedRowIndex()&lt;0)
 	{
  		 e.processOnServer=false;
  		 alert(&quot;ردیفی انتخاب نشده است&quot;);
  return;
                                                        
 }
   e.processOnServer=true;
}
"></ClientSideEvents>
                                                  
                                                    <Image  Url="~/Images/icons/edit.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" UseSubmitBehavior="False" runat="server" Text=" "  ToolTip="مشاهده"
                                                    ID="btnView2" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnViewClient2"
                                                    OnClick="btnView_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberCardClient.GetFocusedRowIndex()&lt;0)
 	{
  		 e.processOnServer=false;
  		 alert(&quot;ردیفی انتخاب نشده است&quot;);
 	 return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                                   
                                                    <Image  Url="~/Images/icons/view.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" UseSubmitBehavior="False" runat="server" Text=" " Height="25px" 
                                                    ToolTip="لغو درخواست" CausesValidation="False" ID="btnDisActive2" EnableClientSideAPI="True"
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnDisActive_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	 
if (GridViewMemberCardClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
                                                        return;
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                   
                                                    <Image  Url="~/Images/icons/delete.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                            </td>
                                            <td style="width: 30px">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گردش کار"
                                                    ID="btnSendNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewMemberCardClient.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
                                                        return;
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
                                            <td style="width: 30px">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="پیگیری"
                                                    ID="btnTracing2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnTracing_Click">
                                                    <ClientSideEvents Click="function(s, e) {
            
	if (GridViewMemberCardClient.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	 return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                                  
                                                    <Image  Url="~/Images/icons/Cheque Status ReChange.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                                    ID="btnPrint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s,e){GridViewMemberCardClient.PerformCallback('Print'); }"></ClientSideEvents>
                                                   
                                                    <Image  Url="~/Images/icons/Printers.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                                    ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
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
                                                    <Image Height="25px" Url="~/Images/Help.png" Width="25px" />
                                                  
                                                    <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
        TypeName="TSP.DataManager.WorkFlowTaskManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsMeCards" runat="server" SelectMethod="SearchForManagmentPage"
        TypeName="TSP.DataManager.MemberCardsManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="MeCrdType" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="IsPrinted" Type="Int32" />
            <asp:Parameter DefaultValue="%" Name="FirstName" Type="String" />
            <asp:Parameter DefaultValue="%" Name="LastName" Type="String" />
            <asp:Parameter DefaultValue="9999/99/99" Name="PrintDateFrom" Type="String" />
            <asp:Parameter DefaultValue="9999/99/99" Name="PrintDateTo" Type="String" />
            <asp:Parameter DefaultValue="9999/99/99" Name="CreateDateFrom" Type="String" />
            <asp:Parameter DefaultValue="9999/99/99" Name="CreateDateTo" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dx:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
    </dx:ASPxHiddenField>
</asp:Content>
