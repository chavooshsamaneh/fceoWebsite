<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeDocument.aspx.cs" Inherits="Employee_OfficeRegister_OfficeDocument"
    Title="مديريت پروانه شرکت های حقوقی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
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
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
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
    <div id="DivReport" runat="server" class="DivErrors" align="right">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table cellpadding="0" dir="rtl" align="right" width="100%">
                    <tr>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDocNew" runat="server" EnableTheming="False"
                                EnableViewState="False" Text="" ToolTip="درخواست صدور پروانه" UseSubmitBehavior="False"
                                OnClick="btnDocNew_Click">
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/Write Document.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" EnableTheming="False"
                                EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" Width="25px"
                                UseSubmitBehavior="False">
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" EnableTheming="False"
                                EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td width="10px" align="center">
                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableTheming="False"
                                EnableViewState="False" OnClick="btnDelete_Click" Text=" " ToolTip="حذف درخواست"
                                UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
  e.processOnServer= confirm('آیا مطمئن به حذف این درخواست هستید؟');
}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChange" runat="server" EnableTheming="False"
                                EnableViewState="False" Text=" " ToolTip="درخواست تغییرات" UseSubmitBehavior="False"
                                OnClick="btnChange_Click">
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
                                <Image Height="25px" Url="~/Images/icons/Change.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>

                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnRevival" runat="server" EnableTheming="False"
                                EnableViewState="False" Text=" " ToolTip="درخواست تمدید" UseSubmitBehavior="False"
                                OnClick="btnRevival_Click">
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
                                <Image Height="25px" Url="~/Images/icons/Revival.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnReduplicate" runat="server"
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="درخواست المثنی"
                                UseSubmitBehavior="False" OnClick="btnReduplicate_Click">
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
                                <Image Height="25px" Url="~/Images/icons/Copy2.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDocumentInvalid" runat="server"
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="درخواست ابطال پروانه"
                                UseSubmitBehavior="False" OnClick="btnDocumentInvalid_Click">
                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
  e.processOnServer= confirm('آیا مطمئن به باطل کردن این پروانه هستید؟');
}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/button_cancel.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td width="10px" align="center">
                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep" runat="server" AutoPostBack="False"
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
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
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/reload.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing" runat="server" AutoPostBack="False"
                                EnableTheming="False" EnableViewState="False" OnClick="btnTracing_Click" Text=" "
                                ToolTip="پیگیری" UseSubmitBehavior="False">
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
                                <Image Height="25px" Url="~/Images/icons/Cheque Status ReChange.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td width="10px" align="center">
                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint" runat="server" AutoPostBack="False"
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	grid.PerformCallback('Print');	
}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/printers.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی Excel"
                                ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                OnClick="btnExportExcel_Click">
                                <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                <HoverStyle BackColor="#FFE0C0">
                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                </HoverStyle>
                                <Image Url="~/Images/icons/ExportExcel.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="left" style="width: 100%">
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnHelp" runat="server" CausesValidation="False"
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                Visible="true" AutoPostBack="false">
                                <Image Height="25px" Url="~/Images/Help.png" Width="25px" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
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
                                Width="230px" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right" valign="top">
                            <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="تاریخ اعتبار تا">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox ID="txtEndDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                Width="230px" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            <dxe:ASPxLabel ID="ASPxLabel13" runat="server" Text="تاریخ ثبت از">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox ID="txtCreateDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                Width="230px" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right" valign="top">
                            <dxe:ASPxLabel ID="ASPxLabel14" runat="server" Text="تاریخ ثبت تا">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox ID="txtCreateDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                Width="230px" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                      <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel31" runat="server" Text="تاریخ آخرین درخواست از">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox ID="txtCreateDateLastReqFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                             onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel32" runat="server" Text="تاریخ آخرین درخواست تا">
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
                        <td align="right" valign="top">نوع پروانه
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                ID="cmbMFType" ClientInstanceName="cmbMFType"
                                ValueType="System.String" AutoPostBack="false"
                                RightToLeft="True">
                                <Items>
                                    <dxe:ListEditItem Selected="true" Value="-1" Text="---------------"></dxe:ListEditItem>
                                    <dxe:ListEditItem Value="1" Text="طراح و ناظر"></dxe:ListEditItem>
                                    <dxe:ListEditItem Value="2" Text="مجری"></dxe:ListEditItem>
                                    <dxe:ListEditItem Value="3" Text="طراح و ناظر و مجری"></dxe:ListEditItem>
                                </Items>
                                <ItemStyle HorizontalAlign="Right" />

                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </TSPControls:CustomAspxComboBox>


                        </td>
                    </tr>
                    <tr>


                        <td align="right" valign="top">وضعیت پروانه
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                ID="cmbDocStatus" ClientInstanceName="cmbDocStatus"
                                ValueType="System.String" AutoPostBack="false"
                                RightToLeft="True">
                                <Items>
                                    <dxe:ListEditItem Selected="true" Value="-1" Text="---------------"></dxe:ListEditItem>
                                    <dxe:ListEditItem Value="0" Text="فاقد پروانه"></dxe:ListEditItem>
                                    <dxe:ListEditItem Value="1" Text="در جریان"></dxe:ListEditItem>
                                    <dxe:ListEditItem Value="2" Text="تایید شده"></dxe:ListEditItem>
                                    <dxe:ListEditItem Value="3" Text="عدم تایید"></dxe:ListEditItem>
                                    <dxe:ListEditItem Value="4" Text="ابطال پروانه"></dxe:ListEditItem>
                                </Items>
                                <ItemStyle HorizontalAlign="Right" />

                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </TSPControls:CustomAspxComboBox>


                        </td>
                        <td>مرحله(پروانه حقوقی)
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
                        <td align="center" colspan="4" valign="top">
                            <br />
                            <table>
                                <tr>
                                    <td style="float: right;" class="SearchButton-Portal">
                                        <TSPControls:CustomAspxButton Width="100px" ID="btnSearch" runat="server"
                                            Text="جستجو"
                                            ClientInstanceName="btnSearch" UseSubmitBehavior="false" OnClick="btnSearch_Click">
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
                                    <td style="float: left;">
                                        <TSPControls:CustomAspxButton ID="btnClearSearch" runat="server" OnClick="btnSearch_Click"
                                            Text="پاک کردن فرم"
                                            UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s, e) {
	   	 Clear();
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
        <li>درصورتی که درخواست از سمت واحد عضویت برای شرکتی ثبت شده باشد،امکان انجام عملیات
            برروی آن شرکت توسط واحد پروانه وجود ندارد.</li>
        <li>جهت ابطال فقط عضویت حقوقی یک شرکت از دکمه ابطال عضویت در صفحه مدیریت اعضای حقوقی
            استفاده نمایید و جهت ابطال پروانه شخص حقوقی از ابطال پروانه در همین صفحه استفاده
            نمایید.</li>
        <li><b>در صورتی که وضعیت عضویت شرکت در حالت ''تایید مشروط'' باشد ، تنها اعضای غیرفعال شده
            شرکت قادر به عضویت در سایر شرکت و یا دفاتر در سازمان می باشند و فعالیت سایر اعضای
            شرکت تا مشخص شدن وضعیت شرکت و تایید مجدد آن منع می گردد </b></li>
        <li>گزینه "نوع پروانه" در پنل جستجوی بر اساس آخرین درخواست شرکت جستجو می نماید
        </li>
        <li>ستون "نوع پروانه" در لیست زیر اطلاعات آخرین درخواست تایید شده شرکت را نمایش می دهد.</li>
        <li>درخواست هایی که نیاز به تایید کارشناس مسکن ندارند عبارتند از:"درخواست تغییر اطلاعات پایه"،"درخواست ابطال عضویت"،"درخواست ابطال پروانه حقوقی"</li>
        <li><b>"درخواست هایی که اعتبارسنجی وضعیت پروانه اعضای  سهامدارا شرکت در آنها انجام نمی شود عبارتند از:"درخواست تغییرات اطلاعات پایه"،"درخواست ابطال عضویت"،"درخواست ابطال پروانه"،"درخواست تغییرات اطلاعات پایه و سهامدار"،"درخواست شامل تایید مشروط"</b></li>
    </ul>
    <TSPControls:CustomAspxDevGridView ID="GridViewOffice" runat="server" AutoGenerateColumns="False"
        KeyFieldName="OfId"
        DataSourceID="ObjdsOfficeDocument" Width="100%" EnableViewState="False" ClientInstanceName="grid"
        OnHtmlRowPrepared="GridViewOffice_HtmlRowPrepared" OnHtmlDataCellPrepared="GridViewOffice_HtmlDataCellPrepared"
        OnAutoFilterCellEditorInitialize="GridViewOffice_AutoFilterCellEditorInitialize"
        OnPageIndexChanged="GridViewOffice_PageIndexChanged" OnCustomCallback="GridViewOffice_CustomCallback"
        Font-Size="8pt">
        <SettingsCookies Enabled="true" />
        <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowFilterRowMenu="True" ShowHorizontalScrollBar="True" />
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
            <dxwgv:GridViewDataTextColumn Name="ExpireState" VisibleIndex="0" Width="30px">
                <DataItemTemplate>
                    <div align="center">
                        <dxe:ASPxImage ID="ImgExpireState" runat="server" Width="16px" Height="16px">
                        </dxe:ASPxImage>
                    </div>
                </DataItemTemplate>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Width="80px" Caption="کد عضویت" FieldName="OfId" Name="OfId"
                VisibleIndex="0">
                <HeaderStyle Wrap="True"></HeaderStyle>
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام شرکت" FieldName="OfName" VisibleIndex="1"
                Width="150px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نوع شرکت" FieldName="OtName" Visible="true"
                VisibleIndex="2">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نوع پروانه" FieldName="MFTypeName" Name="MFTypeName"
                VisibleIndex="2" Width="130px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه شرکت" FieldName="GrdName" Name="GrdName"
                VisibleIndex="2" Width="130px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewDataTextColumn Caption="نوع فعالیت" FieldName="ActivityTypeName" Name="ActivityTypeName"
                VisibleIndex="2" Width="130px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره پروانه" FieldName="FileNo" Name="FileNo"
                VisibleIndex="2" Width="130px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام کاربری" Visible="False" FieldName="UserName"
                VisibleIndex="3">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="مدیر عامل" FieldName="MName" VisibleIndex="5">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره پروانه مدیر عامل" FieldName="MeFileNo" VisibleIndex="5">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="تعداد اعضا" FieldName="MemberCount" VisibleIndex="5">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره تماس" FieldName="Tel1" VisibleIndex="6"
                Visible="False">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره ثبت" FieldName="RegOfNo" Name="RegOfNo"
                VisibleIndex="7" Width="80px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="محل ثبت" FieldName="RegPlace" VisibleIndex="8"
                Visible="False">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت در عضویت" FieldName="OffStatus" VisibleIndex="8"
                Width="100px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت پروانه" FieldName="DocumentStatusName" VisibleIndex="8"
                Width="100px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="تاریخ عضویت" FieldName="CreateDate" VisibleIndex="10"
                Width="80px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="11" FieldName="RegDate" Caption="تاریخ صدور"
                Width="80px">
                <CellStyle Wrap="False" CssClass="CellLeft">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="12" FieldName="LastExpireDate" Caption="پایان اعتبارآخرین درخواست"
                Width="80px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="14" Width="30px" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <ClientSideEvents FocusedRowChanged="function(s, e) {
            if(s.cpDoPrint==1) return;
	if(grid.cpIsReturn!=1)
	{
		grid.cpSelectedIndex=grid.GetFocusedRowIndex();			
	}
	else
	{
		grid.cpIsReturn=0;	
	}
	//if(grid.cpIsPostBack!=1)
	//{
	//	grid.ExpandDetailRow(grid.cpSelectedIndex);
	//}
	//else
	//{
	//	grid.cpIsPostBack=0;
	//}
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
}"
            EndCallback="function(s, e) {
            if(s.cpDoPrint==1)
            {
                s.cpDoPrint = 0;
                window.open('../../Print.aspx');
            }}" />
        <Templates>
            <DetailRow>

                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridViewRequest" runat="server"
                    AutoGenerateColumns="False" ClientInstanceName="Reqgrid"
                    DataSourceID="OdbRequest" KeyFieldName="OfReId" OnBeforePerformDataSelect="CustomAspxDevGridViewRequest_BeforePerformDataSelect"
                    Width="100%"
                    OnAutoFilterCellEditorInitialize="CustomAspxDevGridViewRequest_AutoFilterCellEditorInitialize"
                    Font-Size="8pt">
                    <SettingsCookies Enabled="false" />
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
                        <dxwgv:GridViewDataTextColumn Caption="نوع" FieldName="TypeName" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MFTypeName" Caption="نوع پروانه"
                            Width="80px">
                            <HeaderStyle Wrap="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره پروانه" FieldName="MFNo" VisibleIndex="2"
                            Width="130px">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="درخواست دهنده" FieldName="RequesterType" VisibleIndex="3">
                            <CellStyle Wrap="True">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ارسال کننده" FieldName="RequesterName" VisibleIndex="5">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="WFRequesterType" VisibleIndex="6">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="CreateDate" VisibleIndex="7"
                            Width="80px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="SerialNo" Caption="شماره سریال"
                            Width="80px">
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نوع تأیید" FieldName="Confirm" Visible="False"
                            VisibleIndex="11">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ پاسخ" FieldName="AnswerDate" VisibleIndex="12">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="کد رهگیری" FieldName="FollowCode" VisibleIndex="12"
                            Width="60px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت تایید مشروط" FieldName="ConditionalApprovalName"
                            VisibleIndex="12" Width="100px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="OfId" Visible="False"
                            VisibleIndex="14">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Type" Visible="False" VisibleIndex="15">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="16" FieldName="RegDate" Caption="تاریخ صدور"
                            >
                            <HeaderStyle Wrap="True"></HeaderStyle>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="17" FieldName="ExpireDate" Caption="پایان اعتبار"
                            >
                            <HeaderStyle Wrap="True"></HeaderStyle>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="18" FieldName="InActive" Caption="وضعیت"
                            Visible="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="19" FieldName="LetterNo" Caption="شماره نامه"
                            Visible="False">
                            <HeaderStyle Wrap="True" />
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="20" FieldName="LetterDate" Caption="تاریخ نامه"
                            Visible="False">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="21" Caption=" " Width="30px" ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowHorizontalScrollBar="True"
                        ShowFilterRowMenu="True" />
                </TSPControls:CustomAspxDevGridView>

            </DetailRow>
        </Templates>
    </TSPControls:CustomAspxDevGridView>
    <br />

    <fieldset width="98%">
        <legend>راهنما</legend>
        <table width="100%">
            <tbody>
                <tr>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel36" runat="server" Text="درخواست صدور پروانه: فونت مشکی" ForeColor="Black">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel37" runat="server" Text="درخواست تمدید: فونت سبز" ForeColor="Green">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel43" runat="server" Text="درخواست تغییرات: فونت آبی تیره" ForeColor="DarkBlue">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="درخواست المثنی: فونت بنفش" ForeColor="DarkMagenta"
                            Font-Bold="False">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel12" runat="server" Text="درخواست ابطال پروانه اشتغال: فونت قرمز"
                            ForeColor="Red">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel38" runat="server" Text="پروانه باطل شده: فونت قهوه ای"
                            ForeColor="Brown" Font-Bold="False">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="درخواست تغییرات عضویت: فونت زیتونی" ForeColor="Olive">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="درخواست تایید شده: فونت مشکی" ForeColor="Black">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="درخواست تغییرات اطلاعات پایه : فونت صورتی"
                            ForeColor="DeepPink">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="right">
                        <dxe:ASPxLabel ID="ASPxLabel30" runat="server" Text="تایید مشروط : آبی آسمانی کمرنگ" ForeColor="LightSkyBlue">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="middle" align="right"></td>
                    <td valign="middle" align="right"></td>
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
                <tr>
                    <td colspan="3">
                        <strong>*راهنمای گردش کار عضویت حقوقی</strong>
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
            </tbody>
        </table>
    </fieldset>

    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table cellpadding="0" dir="rtl" width="100%" align="right">
                    <tr>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDocNew1" runat="server" EnableTheming="False"
                                EnableViewState="False" Text=" " ToolTip="درخواست صدور پروانه" UseSubmitBehavior="False"
                                OnClick="btnDocNew_Click">
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/Write Document.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" EnableTheming="False"
                                EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" Width="25px"
                                UseSubmitBehavior="False">
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server" EnableTheming="False"
                                EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td width="10px" align="center">
                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableTheming="False"
                                EnableViewState="False" OnClick="btnDelete_Click" Text=" " ToolTip="حذف درخواست"
                                UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
  e.processOnServer= confirm('آیا مطمئن به حذف این درخواست هستید؟');
}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChange1" runat="server" EnableTheming="False"
                                EnableViewState="False" Text=" " ToolTip="درخواست تغییرات" UseSubmitBehavior="False"
                                OnClick="btnChange_Click">
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
                                <Image Height="25px" Url="~/Images/icons/Change.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnRevival1" runat="server" EnableTheming="False"
                                EnableViewState="False" Text=" " ToolTip="درخواست تمدید" UseSubmitBehavior="False"
                                OnClick="btnRevival_Click">
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
                                <Image Height="25px" Url="~/Images/icons/Revival.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnReduplicate2" runat="server"
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="درخواست المثنی"
                                UseSubmitBehavior="False" OnClick="btnReduplicate_Click">
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
                                <Image Height="25px" Url="~/Images/icons/Copy2.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDocumentInvalid2" runat="server"
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="درخواست ابطال پروانه"
                                UseSubmitBehavior="False" OnClick="btnDocumentInvalid_Click">
                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
  e.processOnServer= confirm('آیا مطمئن به باطل کردن این پروانه هستید؟');
}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/button_cancel.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <%--  <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChangeBaseInfo2" runat="server" 
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="درخواست تغییرات اطلاعات پایه"
                                UseSubmitBehavior="False" OnClick="btnChangeBaseInfo_Click">
                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/ChangeBaseInfo.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>--%>
                        <td width="10px" align="center">
                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendToNexStep" runat="server" AutoPostBack="False"
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
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
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/reload.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing2" runat="server" AutoPostBack="False"
                                EnableTheming="False" EnableViewState="False" OnClick="btnTracing_Click" Text=" "
                                ToolTip="پیگیری" UseSubmitBehavior="False">
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
                                <Image Height="25px" Url="~/Images/icons/Cheque Status ReChange.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td width="10px" align="center">
                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6"></TSPControls:MenuSeprator>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint2" runat="server" AutoPostBack="False"
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	grid.PerformCallback('Print');		
}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/printers.png" Width="25px" />
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
                        <td align="left" style="width: 100%">
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnHelp2" runat="server" CausesValidation="False"
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                Visible="true" AutoPostBack="false">
                                <Image Height="25px" Url="~/Images/Help.png" Width="25px" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
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
    <asp:ObjectDataSource ID="ObjdsOfficeDocument" runat="server" SelectMethod="SelectOfficeForOfficeDocManagmentPage"
        TypeName="TSP.DataManager.OfficeManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="%" Name="FollowCode" Type="String" />
            <asp:Parameter DefaultValue="1" Name="EndDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="EndDateTo" Type="String" />
            <asp:Parameter DefaultValue="%" Name="OfName" Type="String" />
            <asp:Parameter DefaultValue="%" Name="FileNo" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="OfId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="TaskCode" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="MFType" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="CreateDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="CreateDateTo" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="DocStatus" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="TaskId" Type="Int32" />
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
    <dx:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
    </dx:ASPxHiddenField>
    <uc1:WFUserControl ID="WFUserControl" runat="server" OnCallback="WFUserControl_Callback"
        GridName="grid" SessionName="SendBackDataTable_OfConf" />
</asp:Content>
