<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Seminar.aspx.cs" Inherits="Employee_Amoozesh_Seminar"
    Title="مدیریت دوره های غیر مصوب و سمینارهای آموزشی" %>

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
    </script>
    <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>

    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                    ToolTip="جدید">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" EnableTheming="False"
                                    EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False">
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
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" EnableTheming="False"
                                    EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
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
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSeminarAttender" runat="server"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnSeminarAttender_Click"
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
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6"></TSPControls:MenuSeprator>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" Visible="false" ID="btnJudge" runat="server"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnJudge_Click" Text=" "
                                    ToolTip="نظر کارشناس" UseSubmitBehavior="False">
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
                                    <Image Height="25px" Url="~/Images/icons/User comment.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
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

                            <td align="right" valign="top">
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
                            <td align="right" valign="top">
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

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" runat="server" HeaderText="جستجو"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table dir="rtl" width="100%">
                    <tbody>
                        <tr>

                            <td style="vertical-align: top;" valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="موضوع سمینار" ID="ASPxLabel6">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" colspan="3" align="right" style="vertical-align: top">
                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ID="txtSeminarSubject"
                                    ClientInstanceName="txtSeminarSubject">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />

                                </TSPControls:CustomTextBox>



                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="موسسه برگزار کننده" Width="100%" ID="ASPxLabel8">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right">
                                <TSPControls:CustomAspxComboBox runat="server" PopupVerticalAlign="NotSet" Width="100%"
                                    IncrementalFilteringMode="StartsWith" TextField="InsName" ID="cmbInstitue" AutoPostBack="false"
                                    DataSourceID="ODBInstitue" EnableClientSideAPI="True"
                                    ValueType="System.String" ValueField="InsId" ClientInstanceName="cmbInstitue"
                                    EnableIncrementalFiltering="True"
                                    RightToLeft="True">
                                    <ItemStyle HorizontalAlign="Right" />

                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                </TSPControls:CustomAspxComboBox>
                                <asp:ObjectDataSource runat="server" SelectMethod="GetData" ID="ODBInstitue" TypeName="TSP.DataManager.InstitueManager"></asp:ObjectDataSource>
                            </td>
                            <td>مرحله
                            </td>
                            <td>
                                <TSPControls:CustomAspxComboBox ID="CmbTask" runat="server"
                                    ValueType="System.String"
                                    TextField="TaskName" ValueField="TaskCode" RightToLeft="True" ClientInstanceName="CmbTask"
                                    DataSourceID="ObjdsWorkFlowTask" Width="100%" HorizontalAlign="Right" EnableIncrementalFiltering="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                </TSPControls:CustomAspxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" style="vertical-align: top">تاریخ شروع از</td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                     ShowPickerOnTop="True" ID="txtStartDateFrom" PickerDirection="ToRight"
                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="false" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ را با فرمت صحیح وارد نمایید" ControlToValidate="txtStartDateFrom" ID="PersianDateValidator2"
                                    Display="Dynamic"></pdc:PersianDateValidator></td>
                            <td valign="top" align="right" style="vertical-align: top">تاریخ شروع تا</td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                     ShowPickerOnTop="True" ID="txtStartDateTo" PickerDirection="ToRight"
                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="false" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ را با فرمت صحیح وارد نمایید" ControlToValidate="txtStartDateTo" ID="PersianDateValidator3"
                                    Display="Dynamic"></pdc:PersianDateValidator></td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" style="vertical-align: top">تاریخ پایان از</td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                     ShowPickerOnTop="True" ID="txtEndDateFrom" PickerDirection="ToRight"
                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="false" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ را با فرمت صحیح وارد نمایید" ControlToValidate="txtEndDateFrom" ID="PersianDateValidator4"
                                    Display="Dynamic"></pdc:PersianDateValidator></td>
                            <td valign="top" align="right" style="vertical-align: top">تاریخ پایان تا</td>
                            <td valign="top" align="right" style="vertical-align: top">
                                <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                     ShowPickerOnTop="True" ID="txtEndDateTo" PickerDirection="ToRight"
                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="false" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ را با فرمت صحیح وارد نمایید" ControlToValidate="txtEndDateTo" ID="PersianDateValidator5"
                                    Display="Dynamic"></pdc:PersianDateValidator></td>
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
 }
}" />
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
    <dx:ASPxGridViewExporter ID="GridViewExporter" GridViewID="GridViewSeminar" runat="server">
    </dx:ASPxGridViewExporter>
    <TSPControls:CustomAspxDevGridView ID="GridViewSeminar" runat="server" AutoGenerateColumns="False"
        ClientInstanceName="gridview"
        Width="100%" DataSourceID="OdbSeminar" KeyFieldName="SeId" RightToLeft="True"
        OnCustomCallback="GridViewSeminar_CustomCallback" OnHtmlDataCellPrepared="GridViewSeminar_HtmlDataCellPrepared"
        OnFocusedRowChanged="GridViewSeminar_FocusedRowChanged" OnAutoFilterCellEditorInitialize="GridViewSeminar_AutoFilterCellEditorInitialize">
        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
        <ClientSideEvents EndCallback="function(s, e) {
	 if(s.cpPrint==1)
        {
            window.open('../../Print.aspx');
           s.cpPrint=0;
        }
}" />
        <SettingsDetail ExportMode="None" ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
        <Settings ShowHorizontalScrollBar="true" />
        <Columns>
            <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                VisibleIndex="0">
                <DataItemTemplate>
                    <div align="center">
                        <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl="~/Images/WFStart.png">
                        </dxe:ASPxImage>
                    </div>
                </DataItemTemplate>
                <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                    EnableIncrementalFiltering="True" ValueType="System.String">
                </PropertiesComboBox>
                <CellStyle HorizontalAlign="Center"></CellStyle>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataTextColumn Caption="موضوع" FieldName="Subject" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام مؤسسه" FieldName="InsName" VisibleIndex="1">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="StartDate" VisibleIndex="2">
                <CellStyle Wrap="false"></CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان" FieldName="EndDate" VisibleIndex="3">
                <CellStyle Wrap="false"></CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="زمان برگزاری" FieldName="Time" VisibleIndex="4">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="SeminarStatus" VisibleIndex="4">
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewDataTextColumn Caption="تعداد ثبت نام" FieldName="CountRegister" 
                VisibleIndex="4">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تعداد شرکت کننده" FieldName="CountPresent" 
                VisibleIndex="4">
            </dxwgv:GridViewDataTextColumn>
         <%--   <dxwgv:GridViewDataComboBoxColumn Caption="وضعیت پرونده" Visible="false" FieldName="TaskId"
                VisibleIndex="5" Width="150px">
                <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                    ValueType="System.String">
                </PropertiesComboBox>
                <CellStyle HorizontalAlign="Right"></CellStyle>
            </dxwgv:GridViewDataComboBoxColumn>--%>

            <dxwgv:GridViewCommandColumn Caption=" " ShowClearFilterButton="true" VisibleIndex="6">
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Templates>
            <DetailRow>
                <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False"
                    DataSourceID="ObjectDataSourceRequest" RightToLeft="True" ID="GridViewRequest"
                    KeyFieldName="SeReqId" AutoGenerateColumns="False" ClientInstanceName="GridViewRequest"
                    Width="100%" OnBeforePerformDataSelect="GridViewRequest_BeforePerformDataSelect"
                    OnHtmlDataCellPrepared="GridViewRequest_HtmlDataCellPrepared">
                    <Settings ShowHorizontalScrollBar="true"></Settings>
                    <Columns>
                        <dxwgv:GridViewDataComboBoxColumn Width="50px" FieldName="TaskId" Caption="مرحله"
                            Name="WFState" VisibleIndex="0">
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

                        <dxwgv:GridViewCommandColumn Caption=" " ShowClearFilterButton="true" VisibleIndex="8" Width="50px">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>

                </TSPControls:CustomAspxDevGridView>
            </DetailRow>
        </Templates>
    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                    ToolTip="جدید">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" EnableTheming="False"
                                    EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False">
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
                                    ToolTip="مشاهده" UseSubmitBehavior="False">
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
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator7"></TSPControls:MenuSeprator>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSeminarAttender2" runat="server"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnSeminarAttender_Click"
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
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator8"></TSPControls:MenuSeprator>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" Visible="false" ID="ASPxButton2" runat="server"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnJudge_Click" Text=" "
                                    ToolTip="نظر کارشناس" UseSubmitBehavior="False">
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
                                    <Image Height="25px" Url="~/Images/icons/User comment.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
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

                            <td align="right" valign="top">
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
                            <td align="right" valign="top">
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

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:ObjectDataSource ID="OdbSeminar" runat="server" SelectMethod="SelectSeminarForManagmentPage" TypeName="TSP.DataManager.SeminarManager">

        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="SeId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="TaskCode" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="InsId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="StartDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="StartDateTo" Type="String" />
            <asp:Parameter DefaultValue="1" Name="EndDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="EndDateTo" Type="String" />
            <asp:Parameter DefaultValue="%" Name="SeminarSubject" Type="String" />

        </SelectParameters>




    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceRequest" runat="server" SelectMethod="FindBySeminarId"
        TypeName="TSP.DataManager.SeminarRequestManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="-1" Name="SeId" SessionField="SeId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="SelectByWorkCode"
        TypeName="TSP.DataManager.WorkFlowTaskManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                  <img align="middle" src="../../Image/indicator.gif" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
    <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="gridview" SessionName="SendBackDataTable_PP"
        OnCallback="WFUserControl_Callback" />
    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkId"
        TypeName="TSP.DataManager.WorkFlowTaskManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="WorkFlowId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="TaskOrder" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SelectByWorkId"
        TypeName="TSP.DataManager.WorkFlowTaskManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="WorkFlowId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="TaskOrder" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="HDState" runat="server" Visible="False" />
</asp:Content>
