<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ImplementDoc.aspx.cs" Inherits="Employee_ImplementDoc_ImplementDoc"
    Title="مدیریت مجوز فعالیت مجری حقیقی" %>

<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
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
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table dir="rtl" cellpadding="0">
                    <tbody>
                        <tr>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                    ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="BtnNew_Click" CausesValidation="False">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image  Url="~/Images/icons/new.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top;">
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
                            <td style="vertical-align: top; width: 30px;">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                    Width="25px" ID="btnView" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnShow_Click" CausesValidation="False">
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
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                            </td>
                            <td style="vertical-align: top; width: 30px">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="لغو درخواست"
                                    Width="25px" ID="btnDelete" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnDelete_Click" CausesValidation="False">
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
                                    <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top;">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست صدور المثنی"
                                    Width="25px" ID="btnReDuplicate" AutoPostBack="False" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False" CausesValidation="False" OnClick="btnReDuplicate_Click">
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
                                    <Image Height="25px" Url="~/Images/icons/copy.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تمدید"
                                    Width="25px" ID="btnRevival" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" CausesValidation="False" OnClick="btnRevival_Click">
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
                                    <Image Height="25px" Url="~/Images/icons/Revival.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تغییرات"
                                    Width="25px" ID="btnChange" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" CausesValidation="False" OnClick="btnChange_Click">
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
                                    <Image Height="25px" Url="~/Images/icons/Change.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInvalid" runat="server"  EnableTheming="False"
                                    EnableViewState="False" AutoPostBack="false" Text=" " ToolTip="درخواست ابطال"
                                    UseSubmitBehavior="False" OnClick="btnInValid_Click">
                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
	e.processOnServer=confirm('آیا مطمئن به باطل کردن این مجوز هستید؟');
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/button_cancel.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>                  
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
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
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
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
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/Cheque Status ReChange.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint" runat="server" AutoPostBack="False" 
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
		GridViewMemberFile.PerformCallback('Print');	
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
                            <td align="left" style="width: 100%">
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnHelp" runat="server" CausesValidation="False" 
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                    Visible="true" AutoPostBack="false">
                                    <Image Height="25px" Url="~/Images/Help.png" Width="25px" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
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
    <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table width="100%">
                                <tr>
                                    <td align="right" valign="top" width="15%">
                                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="کد عضویت">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" width="35%">
                                        <TSPControls:CustomTextBox IsMenuButton="true" ID="txtMeId" runat="server" ClientInstanceName="txtMeId" 
                                              Width="100%">
                                            <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Search">
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RegularExpression ErrorText="کد عضویت را با فرمت صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top" width="15%">
                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="کد پیگیری">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" width="35%">
                                        <TSPControls:CustomTextBox IsMenuButton="true" ID="txtFollowCode" runat="server" ClientInstanceName="txtFollowCode"
                                              
                                            Width="100%">
                                            <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" width="15%">
                                        <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Text="تاریخ اعتبار از">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" width="35%">
                                        <pdc:PersianDateTextBox ID="txtEndDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                            Width="230px" onkeypress="SearchKeyPress(event,2);" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                    </td>
                                    <td align="right" valign="top" width="15%">
                                        <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="تاریخ اعتبار تا">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" width="35%">
                                        <pdc:PersianDateTextBox ID="txtEndDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                            Width="230px" onkeypress="SearchKeyPress(event,2);" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
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
                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="تاریخ آخرین درخواست تا">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox ID="txtCreateDateLastReqTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                             onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="center" colspan="4" dir="ltr" valign="top">
                                        <br />
                                        <table>
                                            <tr>
                                                <td >
                                                    <TSPControls:CustomAspxButton   ID="ASPxButton2" runat="server" AutoPostBack="False" 
                                                          Text="پاک کردن فرم"
                                                        UseSubmitBehavior="false" OnClick="btnSearch_Click">
                                                        <ClientSideEvents Click="function(s, e) {
	   	 ClearSearch();
}" />                                                     
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td >
                                                    <TSPControls:CustomAspxButton   ID="btnSearch" runat="server" AutoPostBack="False" 
                                                          Text="جستجو"
                                                        ClientInstanceName="btnSearch" Width="98px" UseSubmitBehavior="false" CausesValidation="true" ValidationGroup="Search" OnClick="btnSearch_Click">
                                                      <ClientSideEvents Click="function(s, e) {
 //e.processOnServer=false;
 //if (ASPxClientEdit.ValidateGroup('SearchValid')){
 
//	   if(CheckSearch()==0)
  //      {
    //      alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.'); 
      //    return; 
        //}
        //else
         e.processOnServer=true;
  //}
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
    <TSPControls:CustomAspxDevGridView ID="GridViewMemberFile" Width="100%" runat="server"
        DataSourceID="ObjdsMemberFileMainRequest"
        AutoGenerateColumns="False" ClientInstanceName="GridViewMemberFile"
        KeyFieldName="MfId" OnDetailRowExpandedChanged="GridViewMemberFile_DetailRowExpandedChanged"
        OnCustomCallback="GridViewMemberFile_CustomCallback" OnHtmlRowPrepared="GridViewMemberFile_HtmlRowPrepared"
        Font-Size="8pt" OnAutoFilterCellEditorInitialize="GridViewMemberFile_AutoFilterCellEditorInitialize"
        OnHtmlDataCellPrepared="GridViewMemberFile_HtmlDataCellPrepared">
        <ClientSideEvents EndCallback="function(s, e) {
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
                    OnBeforePerformDataSelect="GridViewMeFileHistory_BeforePerformDataSelect" Font-Size="8pt"
                    OnAutoFilterCellEditorInitialize="GridViewMeFileHistory_AutoFilterCellEditorInitialize"
                    OnHtmlDataCellPrepared="GridViewMeFileHistory_HtmlDataCellPrepared">
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
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="RequeterTypeName" Caption="ثبت کننده درخواست"
                            Width="100px">
                            <HeaderStyle Wrap="True"></HeaderStyle>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MFType" Caption="نوع درخواست">
                            <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="SerialNo" Caption="شماره سریال">
                            <HeaderStyle Wrap="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FollowCode" Caption="کد پیگیری">
                            <HeaderStyle />
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Width="150px" VisibleIndex="1" FieldName="MFNo" Caption="شماره مجوز">
                            <HeaderStyle></HeaderStyle>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="RegDate" Caption="تاریخ صدور">
                            <HeaderStyle></HeaderStyle>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ExpireDate" Caption="تاریخ پایان اعتبار">
                            <HeaderStyle></HeaderStyle>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="InActives" Caption="وضعیت">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Width="150px" VisibleIndex="6" FieldName="MailNo" Caption="شماره نامه">
                            <HeaderStyle Wrap="False" />
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
                        <dxwgv:GridViewDataTextColumn Width="150px" Caption="ارسال کننده" FieldName="RequesterName"
                            VisibleIndex="8">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Width="150px" Caption="سمت" FieldName="WFRequesterType"
                            VisibleIndex="8">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="9" Caption=" " ShowClearFilterButton="true">
                         
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

            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MemberId" VisibleIndex="0"
                Width="60px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام خانودگی" FieldName="LastName" VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
              <dxwgv:GridViewDataTextColumn Caption="وضعیت عضویت" FieldName="MrsName" 
                VisibleIndex="2">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
              <dxwgv:GridViewDataTextColumn Caption="شماره همراه" FieldName="MobileNo" 
                VisibleIndex="2">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewDataTextColumn Caption="شماره سریال" FieldName="SerialNo" Visible="False"
                VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد مجری" FieldName="MFSerialNo" Name="''"
                VisibleIndex="3">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره مجوز اجرا" FieldName="MFNo" Name="''"
                VisibleIndex="3">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه مجوز" FieldName="GrdName" VisibleIndex="4">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ صدور" FieldName="RegDate" VisibleIndex="4">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان اعتبار" FieldName="LastExpireDate"
                VisibleIndex="5">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت پروانه" FieldName="MFType" Visible="False"
                VisibleIndex="4">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="LastConfirmReqTypeName"
                Caption="نوع درخواست">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewCommandColumn Width="30px" Caption=" " VisibleIndex="7" ShowClearFilterButton="true">
      
            </dxwgv:GridViewCommandColumn>
        </Columns>
     
        <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" ExportMode="None"></SettingsDetail>
        <SettingsCookies Enabled="false" />
    </TSPControls:CustomAspxDevGridView>
    <br />
 
                    <fieldset width="98%">
                        <legend>راهنما</legend>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td valign="middle" align="right">
                                        <dxe:ASPxLabel ID="ASPxLabel36" runat="server" Text="صدور: فونت مشکی" ForeColor="Black">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="middle" align="right">
                                        <dxe:ASPxLabel ID="ASPxLabel37" runat="server" Text="تمدید: فونت سبز" ForeColor="Green">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="middle" align="right">
                                        <dxe:ASPxLabel ID="ASPxLabel12" runat="server" Text="المثنی: فونت صورتی" ForeColor="Magenta">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="right">
                                        <dxe:ASPxLabel ID="ASPxLabel38" runat="server" Text="تغییرات: فونت قهوه ای" ForeColor="Brown"
                                            Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="middle" align="right">
                                        <dxe:ASPxLabel ID="ASPxLabel14" runat="server" Text="ابطال: فونت قرمز" ForeColor="Red"
                                            Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="middle" align="right"></td>
                                </tr>

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
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu3" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table dir="rtl" cellpadding="0">
                    <tbody>
                        <tr>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                    ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" CausesValidation="False">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image  Url="~/Images/icons/new.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
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
                                    EnableTheming="False" OnClick="btnShow_Click" CausesValidation="False">
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
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="لغو درخواست"
                                    Width="25px" ID="btnDelete2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnDelete_Click" CausesValidation="False">
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
                                    <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست صدور المثنی"
                                    Width="25px" ID="btnReDuplicate2" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" CausesValidation="False" OnClick="btnReDuplicate_Click">
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
                                    <Image Height="25px" Url="~/Images/icons/copy.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تمدید"
                                    Width="25px" ID="btnRevival2" AutoPostBack="False" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False" CausesValidation="False" OnClick="btnRevival_Click">
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
                                    <Image Height="25px" Url="~/Images/icons/Revival.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="درخواست تغییرات"
                                    Width="25px" ID="btnChange2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" CausesValidation="False" OnClick="btnChange_Click">
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
                                    <Image Height="25px" Url="~/Images/icons/Change.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td style="vertical-align: top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInValid2" runat="server"  EnableTheming="False"
                                    EnableViewState="False" AutoPostBack="false" Text=" " ToolTip="درخواست ابطال"
                                    UseSubmitBehavior="False" OnClick="btnInValid_Click">
                                    <ClientSideEvents Click="function(s, e) {
if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
	e.processOnServer=confirm('آیا مطمئن به باطل کردن این مجوز هستید؟');
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/button_cancel.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                          <%--  <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnActivate2" runat="server"  EnableTheming="False"
                                    EnableViewState="False" Text=" " ToolTip="درخواست احیا" OnClick="btnActivate_Click"
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
		if (GridViewMemberFile.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
 	e.processOnServer=confirm('آیا مطمئن به احیا کردن این مجوز هستید؟')
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/RevivalDoc.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>--%>
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
                            <td align="left" style="width: 100%">
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnHelp2" runat="server" CausesValidation="False" 
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                    Visible="true" AutoPostBack="false">
                                    <Image Height="25px" Url="~/Images/Help.png" Width="25px" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
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
    <dxhf:ASPxHiddenField ID="HiddenFieldRequest" runat="server" ClientInstanceName="HiddenFieldRequest">
    </dxhf:ASPxHiddenField>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewMemberFile">
    </dxwgv:ASPxGridViewExporter>
    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
        TypeName="TSP.DataManager.WorkFlowTaskManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsMemberFileMainRequest" runat="server" SelectMethod="SelectImpDocMainRequest" TypeName="TSP.DataManager.DocMemberFileManager"
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
            <asp:Parameter DefaultValue="%" Name="FollowCode" Type="String" />
            <asp:Parameter DefaultValue="1" Name="EndDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="EndDateTo" Type="String" />
            <asp:Parameter DefaultValue="1" Name="ReqCreateDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="ReqCreateDateTo" Type="String" />
            
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsMeFileSubRequest" runat="server"
        SelectMethod="SelectImpDocSubRequest" TypeName="TSP.DataManager.DocMemberFileManager"
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="-1" Name="MeId" SessionField="MeId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="TaskCode" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hfPageMode" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hfMfId" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hfMeId" runat="server"></asp:HiddenField>
    <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewMemberFile"
        SessionName="SendBackDataTable_EmpImpDoc" OnCallback="WFUserControl_Callback" />
    <dxhf:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
    </dxhf:ASPxHiddenField>
</asp:Content>
