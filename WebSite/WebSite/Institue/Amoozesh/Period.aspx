<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Period.aspx.cs" Inherits="Institue_Amoozesh_Period"
    Title="مدیریت دوره های آموزشی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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
<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
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
    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
        visible="true">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>

    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <div dir="rtl">
                    <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                        width="100%">
                        <tbody>
                            <tr>
                                <td align="right">
                                    <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                                        ToolTip="جدید">
                                                        <ClientSideEvents Click="function(s, e) {
	
	
	
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                                        ToolTip="ویرایش" UseSubmitBehavior="False" Width="25px">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                                        ToolTip="مشاهده">
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChange" runat="server" EnableTheming="False"
                                                        EnableViewState="False" OnClick="btnChange_Click" Text=" " ToolTip="درخواست تغییرات"
                                                        UseSubmitBehavior="False">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/ChangeIns.png" Width="25px" />
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelReq" runat="server" EnableTheming="False"
                                                        EnableViewState="False" OnClick="btnDelReq_Click" Text=" " ToolTip="حذف درخواست"
                                                        UseSubmitBehavior="False">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
    e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');	
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="لغو دوره"
                                                        ID="btnInActive" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                        OnClick="btnInActive_Click" CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) {
		 e.processOnServer= confirm('آیا مطمئن به لغو این دوره هستید؟');
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPeriodAttender" runat="server"
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnPeriodAttender_Click"
                                                        Text=" " ToolTip="شرکت کنندگان دوره" UseSubmitBehavior="False">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/PeriodAttender.png" Width="25px" />
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPresent" runat="server" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnPresent_Click" Text=" "
                                                        ToolTip="ثبت ساعات حضور">
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/Tamdid.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep" runat="server" AutoPostBack="False" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False"
                                                        Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
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
	if (gridview.GetFocusedRowIndex()&lt;0)
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
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                                        ID="btnPrint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False">
                                                        <ClientSideEvents Click="function(s,e){gridview.PerformCallback('Print'); }"></ClientSideEvents>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                        </HoverStyle>
                                                        <Image Url="~/Images/icons/Printers.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>


                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ شرکت کنندگان دوره"
                                                        ID="btnPrintAttenders" AutoPostBack="false">
                                                        <ClientSideEvents Click="function(s, e) {
if (gridview.GetFocusedRowIndex()&lt;0)
 {
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
        return;
 }
 else
 {                                                                
  gridview.PerformCallback('PrintAttender');
                                                                
 }
}" />

                                                        <Image Url="~/Images/icons/printers2.png">
                                                        </Image>
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
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>

    <br />
    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel3" runat="server" HeaderText="جستجو"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table dir="rtl" width="100%">
                    <tbody>
                        <tr>
                            <td valign="top" align="right" style="vertical-align: top"  width="20%">کد دوره
                            </td>
                            <td valign="top" align="right" style="vertical-align: top"  width="30%">
                                <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtPPCode"
                                    ClientInstanceName="txtPPCode">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ValidationSettings>
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                            <td style="vertical-align: top;" valign="top" align="right" width="20%">
                                <dxe:ASPxLabel runat="server" Text="عنوان درس" ID="ASPxLabel6">
                                </dxe:ASPxLabel>
                            </td>
                            <td dir="ltr" valign="top" align="right" style="vertical-align: top" width="30%">
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                    TextField="CrsName" ID="cmbCourse"
                                    DataSourceID="odbCourseName" ValueType="System.String" ValueField="CrsId"
                                    EnableIncrementalFiltering="True" ClientInstanceName="cmbCourse" RightToLeft="True">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </TSPControls:CustomAspxComboBox>

                                <asp:ObjectDataSource ID="odbCourseName" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetData" TypeName="TSP.DataManager.CourseManager"></asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" style="vertical-align: top">تاریخ شروع دوره از</td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                    Width="200px" ShowPickerOnTop="True" ID="txtStartDateFrom" PickerDirection="ToRight"
                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="false" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ را با فرمت صحیح وارد نمایید" ControlToValidate="txtStartDateFrom" ID="PersianDateValidator2"
                                    Display="Dynamic"></pdc:PersianDateValidator></td>
                            <td valign="top" align="right" style="vertical-align: top">تاریخ شروع دوره تا</td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                    Width="200px" ShowPickerOnTop="True" ID="txtStartDateTo" PickerDirection="ToRight"
                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="false" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ را با فرمت صحیح وارد نمایید" ControlToValidate="txtStartDateTo" ID="PersianDateValidator3"
                                    Display="Dynamic"></pdc:PersianDateValidator></td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" style="vertical-align: top">تاریخ پایان دوره از</td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                    Width="200px" ShowPickerOnTop="True" ID="txtEndDateFrom" PickerDirection="ToRight"
                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="false" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ را با فرمت صحیح وارد نمایید" ControlToValidate="txtEndDateFrom" ID="PersianDateValidator4"
                                    Display="Dynamic"></pdc:PersianDateValidator></td>
                            <td valign="top" align="right" style="vertical-align: top">تاریخ پایان دوره تا</td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                    Width="200px" ShowPickerOnTop="True" ID="txtEndDateTo" PickerDirection="ToRight"
                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="false" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ را با فرمت صحیح وارد نمایید" ControlToValidate="txtEndDateTo" ID="PersianDateValidator5"
                                    Display="Dynamic"></pdc:PersianDateValidator></td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" style="vertical-align: top">تاریخ آزمون از</td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                    Width="200px" ShowPickerOnTop="True" ID="txtTestDateFrom" PickerDirection="ToRight"
                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="false" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ را با فرمت صحیح وارد نمایید" ControlToValidate="txtTestDateFrom" ID="PersianDateValidator1"
                                    Display="Dynamic"></pdc:PersianDateValidator></td>
                            <td valign="top" align="right" style="vertical-align: top">تاریخ آزمون تا</td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                    Width="200px" ShowPickerOnTop="True" ID="txtTestDateTo" PickerDirection="ToRight"
                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="false" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ را با فرمت صحیح وارد نمایید" ControlToValidate="txtTestDateTo" ID="PersianDateValidator7"
                                    Display="Dynamic"></pdc:PersianDateValidator></td>
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
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td valign="top" align="center" colspan="4">
                                <br />
                                <table>
                                    <tr>
                                        <td align="left" valign="top">
                                            <TSPControls:CustomAspxButton runat="server" Text="جستجو" ID="btnSearch" ClientInstanceName="btnSearch" OnClick="btnSearch_Click"
                                                AutoPostBack="False" UseSubmitBehavior="False"
                                                Height="25px" Width="92px">
                                                <ClientSideEvents Click="function(s, e) {
e.processOnServer=false;
 if (ASPxClientEdit.ValidateGroup('SearchValid'))
{

	   if(CheckSearch()==0)
        {
          alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.'); 
          return; 
        }
        else
         e.processOnServer=true;
 }}
" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxButton runat="server" Text="پاک کردن فرم" ID="btnClear" OnClick="btnSearch_Click"
                                                AutoPostBack="False" UseSubmitBehavior="False">
                                                <ClientSideEvents Click="function(s, e) {

        ClearSearch();
}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <div align="right">
        <ul class="HelpUL">
            <li>جهت تغییر و یا تکمیل اطلاعات دوره هایی که پیش از این در سیستم ثبت و تایید شده اند
                        و یا انتقال یافته از سیستم قدیم می باشند مراحل زیر را طی نمایید:
                        <ul type="circle">
                            <li>برای دوره مورد نظر ''درخواست تغییرات'' ثبت نمایید. </li>
                            <li>اطلاعات مورد نظر را تکمیل نمایید. </li>
                            <li>گردش کار درخواست ثبت شده را تایید نمایید. </li>
                        </ul>
            </li>
        </ul>
    </div>
    <br />
    <TSPControls:CustomAspxDevGridView ID="GridViewPeriods" runat="server" Width="100%"
        KeyFieldName="PPId"
        EnableViewState="False" DataSourceID="OdbPeriod" AutoGenerateColumns="False"
        ClientInstanceName="gridview" OnCustomCallback="GridViewPeriods_CustomCallback"
        OnAutoFilterCellEditorInitialize="GridViewPeriods_AutoFilterCellEditorInitialize"
        OnHtmlRowPrepared="GridViewPeriods_HtmlRowPrepared">
        <Columns>
            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="InsId"
                Caption="کد">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="TaskCode" Name="TaskCode">
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
            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="PeriodTitle" Caption="عنوان دوره"
                Width="220px">
                <CellStyle Wrap="false">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="PPCode" Caption="کد دوره">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="StartDate" Caption="تاریخ شروع"
                Width="80px">
                <CellStyle HorizontalAlign="Right" Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="EndDate" Caption="تاریخ پایان"
                Width="80px">
                <CellStyle HorizontalAlign="Right" Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="6" FieldName="Point"
                Width="40px" Caption="امتیاز">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Visible="true" VisibleIndex="2" FieldName="Duration"
                Width="60px" Caption="طول دوره(ساعت)">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="4" FieldName="Place"
                Width="100px" Caption="محل برگزاری">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>


            <dxwgv:GridViewDataTextColumn Caption="ظرفیت دوره" FieldName="Capacity" VisibleIndex="4">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تعداد کل شرکت کنندگان" FieldName="CountRegister"
                VisibleIndex="4">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تعداد ثبت نام در آزمون" FieldName="CountRegisterTest"
                VisibleIndex="4">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تعداد ثبت نام دوره و آزمون" FieldName="CountRegisterPeriod"
                VisibleIndex="4">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تعداد ثبت خارج از نوبت" FieldName="CountRegisterOutOfTime"
                VisibleIndex="4">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ظرفیت باقیمانده" FieldName="RemainCapacity"
                VisibleIndex="4">
                <CellStyle Wrap="True">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>


            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="PeriodStatus" Caption="وضعیت"
                Width="150px">
                <CellStyle Wrap="false">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="InActiveName" Caption="وضعیت دوره"
                Width="80px">
                <CellStyle Wrap="false">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="PPId" Name="PPId">
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="6" Width="30px" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowHorizontalScrollBar="true"></Settings>
        <SettingsDetail ExportMode="All" ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
                <ClientSideEvents EndCallback="function(s, e) {
	 if(s.cpPrint==1)
        {
            window.open('../../Print.aspx');
           s.cpPrint=0;
        }
        else if(s.cpPrint==2)
        {
           window.open(s.cpURL);
           s.cpURL='';
           s.cpPrint=0;
        }
}" />
        <Templates>
            <DetailRow>
                <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False"
                    DataSourceID="ObjectDataSourceRequest" RightToLeft="True" ID="GridViewRequest"
                    KeyFieldName="PPRId" AutoGenerateColumns="False" ClientInstanceName="GridViewRequest"
                    Width="100%" OnBeforePerformDataSelect="GridViewRequest_BeforePerformDataSelect"
                    OnAutoFilterCellEditorInitialize="GridViewRequest_AutoFilterCellEditorInitialize">
                    <Styles>
                        <GroupPanel ForeColor="Black">
                        </GroupPanel>
                        <Header HorizontalAlign="Center">
                        </Header>
                    </Styles>
                    <Settings ShowGroupPanel="True" ShowHorizontalScrollBar="true" ShowFilterRowMenu="True"
                        ShowFilterRow="True"></Settings>
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
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="TypeName"
                            Caption="نام درخواست" Name="TypeName">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" Width="100px" FieldName="CreateDate"
                            Caption="تاریخ درخواست" Name="Date">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" Width="80px" FieldName="IsConfirmName"
                            Caption="وضعیت" Name="IsConfirmName">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" Width="150px" FieldName="LetterNo"
                            Caption="شماره نامه" Name="LetterNo">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" Width="80px" FieldName="LetterDate"
                            Caption="تاریخ نامه" Name="Date">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" Width="150px" FieldName="RequesterName"
                            Caption="نام ارسال کننده" Name="RequesterName">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" Width="150px" FieldName="WFRequesterType"
                            Caption="سمت ارسال کننده" Name="WFRequesterType">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="50px" ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <SettingsLoadingPanel Text="در حال بارگذاری"></SettingsLoadingPanel>
                </TSPControls:CustomAspxDevGridView>
            </DetailRow>
        </Templates>
        <ClientSideEvents FocusedRowChanged="function(s, e) {
// gridview.ExpandDetailRow(gridview.GetFocusedRowIndex());
}
" />
    </TSPControls:CustomAspxDevGridView>
    <dx:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="CustomAspxDevGridView1">
    </dx:ASPxGridViewExporter>

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
        *راهنمای گردش کار درخواست چاپ گواهینامه آموزشی
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




    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <div dir="rtl">
                    <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                        width="100%">
                        <tbody>
                            <tr>
                                <td align="right">
                                    <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew1" runat="server" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                                        ToolTip="جدید">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit1" runat="server" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                                        ToolTip="ویرایش" UseSubmitBehavior="False" Width="25px">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView1" runat="server" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                                        ToolTip="مشاهده">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator8"></TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChange2" runat="server" EnableTheming="False"
                                                        EnableViewState="False" OnClick="btnChange_Click" Text=" " ToolTip="درخواست تغییرات"
                                                        UseSubmitBehavior="False">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/ChangeIns.png" Width="25px" />
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelReq2" runat="server" EnableTheming="False"
                                                        EnableViewState="False" OnClick="btnDelReq_Click" Text=" " ToolTip="حذف درخواست"
                                                        UseSubmitBehavior="False">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
    e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');	
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="لغو دوره"
                                                        ID="btnInActive2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                        OnClick="btnInActive_Click" CausesValidation="False">
                                                        <ClientSideEvents Click="function(s, e) {
		 e.processOnServer= confirm('آیا مطمئن به لغو این دوره هستید؟');
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator7"></TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPeriodAttender2" runat="server"
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnPeriodAttender_Click"
                                                        Text=" " ToolTip="شرکت کنندگان دوره" UseSubmitBehavior="False">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/PeriodAttender.png" Width="25px" />
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPresent2" runat="server" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnPresent_Click" Text=" "
                                                        ToolTip="ثبت ساعات حضور">
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/Tamdid.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep1" runat="server" AutoPostBack="False" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False"
                                                        Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
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
	if (gridview.GetFocusedRowIndex()&lt;0)
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
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                                        ID="btnPrint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False">
                                                        <ClientSideEvents Click="function(s,e){gridview.PerformCallback('Print'); }"></ClientSideEvents>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                        </HoverStyle>
                                                        <Image Url="~/Images/icons/Printers.png">
                                                        </Image>
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
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>


    <asp:ObjectDataSource ID="OdbPeriod" runat="server" TypeName="TSP.DataManager.PeriodPresentManager"
        SelectMethod="SelectPeriodPresentForManagmentPage" FilterExpression="InsId={0}">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="PPId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="PPRId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="InsId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="Status" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="CrsId" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="StartDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="StartDateTo" Type="String" />
            <asp:Parameter DefaultValue="1" Name="EndDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="EndDateTo" Type="String" />
            <asp:Parameter DefaultValue="1" Name="TestDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="TestDateTo" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="TaskId" Type="Int32" />
            <asp:Parameter DefaultValue="%" Name="PPCode" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceRequest" runat="server" SelectMethod="FindByPeriodId"
        TypeName="TSP.DataManager.PeriodPresentRequestManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="-1" Name="PPId" SessionField="PPId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="InstitueId" runat="server" Visible="False"></asp:HiddenField>
    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCodeList"
        TypeName="TSP.DataManager.WorkFlowTaskManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="WorkFlowCodeList" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="gridview" SessionName="SendBackDataTable_Period"
        OnCallback="WFUserControl_Callback" />
</asp:Content>
