<%@ Page Title="مدیریت دوره های آموزشی" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="Periods.aspx.cs" Inherits="Settlement_Amoozesh_Periods" %>

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
        <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
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
                                    <td style="vertical-align: top; text-align: right">
                                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                            <tbody>
                                                <tr>

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
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/reload.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td dir="ltr">
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
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/Cheque Status ReChange.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                    </td>

                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ شرکت کنندگان دوره"
                                                            ID="btnPrintAttenders" AutoPostBack="false" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False">
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
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/printers2.png">
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
                                <td valign="top" align="right" style="vertical-align: top">کد دوره
                                </td>
                                <td valign="top" align="right" style="vertical-align: top">
                                    <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtPPCode"
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
                                <td style="vertical-align: top;" valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="عنوان درس" ID="ASPxLabel6">
                                    </dxe:ASPxLabel>
                                </td>
                                <td dir="ltr" valign="top" align="right" style="vertical-align: top">
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
                                <td valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="موسسه برگزار کننده*" Width="100%" ID="ASPxLabel8">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomAspxComboBox runat="server" PopupVerticalAlign="NotSet"  Width="100%"
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
                                        TextField="TaskName" ValueField="TaskId" RightToLeft="True" ClientInstanceName="CmbTask"
                                        DataSourceID="ObjdsWorkFlowTask" Width="100%" HorizontalAlign="Right" EnableIncrementalFiltering="True">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                    </TSPControls:CustomAspxComboBox>
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
                                <td valign="top" align="center" colspan="4">
                                    <br />
                                    <table>
                                        <tr>
                                            <td align="left" valign="top">
                                                <TSPControls:CustomAspxButton runat="server" Text="جستجو"  ID="btnSearch" ClientInstanceName="btnSearch" OnClick="btnSearch_Click"
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
                                                <TSPControls:CustomAspxButton  runat="server" Text="پاک کردن فرم"  ID="btnClear" OnClick="btnSearch_Click"
                                                    AutoPostBack="False" UseSubmitBehavior="False" >
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
        <TSPControls:CustomAspxDevGridView ID="GridViewPeriods" runat="server" AutoGenerateColumns="False"
            ClientInstanceName="gridview" DataSourceID="OdbPeriod" KeyFieldName="PPId" 
             OnCustomCallback="GridViewPeriods_CustomCallback" Width="100%"
            OnAutoFilterCellEditorInitialize="GridViewPeriods_AutoFilterCellEditorInitialize"
             RightToLeft="True"
            OnHtmlRowPrepared="GridViewPeriods_HtmlRowPrepared">
            <SettingsBehavior ColumnResizeMode="Control" />
            <SettingsDetail ExportMode="None" ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
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
                <dxwgv:GridViewDataTextColumn Caption="عنوان" FieldName="PeriodTitle" VisibleIndex="1"
                    Width="160px">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="کد دوره" FieldName="PPCode" VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="نام مؤسسه" FieldName="InsName" VisibleIndex="2"
                    Width="160px">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>

                <dxwgv:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="StartDate" VisibleIndex="3">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان" FieldName="EndDate" VisibleIndex="4">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="تاریخ آزمون" FieldName="TestDate" VisibleIndex="4">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="طول دوره(ساعت)" FieldName="Duration" VisibleIndex="4">
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
                <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="PeriodStatus" VisibleIndex="5">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>

                <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="7" Width="30px" ShowClearFilterButton="true">
                </dxwgv:GridViewCommandColumn>
            </Columns>
            <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFilterRowMenu="True" ShowHorizontalScrollBar="True" />
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
}"
                FocusedRowChanged="function(s, e) {
          
 //gridview.ExpandDetailRow(gridview.GetFocusedRowIndex());
          
}
" />
            <Templates>
                <DetailRow>
                    <TSPControls:CustomAspxDevGridView runat="server"  EnableViewState="False"
                        DataSourceID="ObjectDataSourceRequest" RightToLeft="True" ID="GridViewRequest"
                        KeyFieldName="PPRId" AutoGenerateColumns="False" ClientInstanceName="GridViewRequest"
                         Width="100%" OnBeforePerformDataSelect="GridViewRequest_BeforePerformDataSelect">
                        <Settings ShowHorizontalScrollBar="true"></Settings>
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

                    </TSPControls:CustomAspxDevGridView>
                </DetailRow>
            </Templates>
        </TSPControls:CustomAspxDevGridView>
        <dx:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewPeriods">
        </dx:ASPxGridViewExporter>
        <br />
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
    </fieldset>
                   
        <br />
        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>
                                        <table >
                                            <tbody>
                                                <tr>
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
                                                           
                                                            <Image Height="25px" Url="~/Images/icons/Cheque Status ReChange.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                                                    </td>

                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ شرکت کنندگان دوره"
                                                            ID="btnPrintAttenders2" AutoPostBack="false" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False">
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
                                                           
                                                            <Image  Url="~/Images/icons/printers2.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>

                                                </tr>
                                            </tbody>
                                       
                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanelMenu>

        <asp:ObjectDataSource ID="OdbPeriod" runat="server" SelectMethod="SelectPeriodPresentForManagmentPageForSettlement"
            TypeName="TSP.DataManager.PeriodPresentManager">
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
        <asp:ObjectDataSource ID="ObjectDataSourceRequest" runat="server" SelectMethod="FindByPeriodId"
            TypeName="TSP.DataManager.PeriodPresentRequestManager" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="-1" Name="PPId" SessionField="PPId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
            TypeName="TSP.DataManager.WorkFlowTaskManager">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
   </asp:Content>


