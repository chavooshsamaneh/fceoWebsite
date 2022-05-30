<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Teachers.aspx.cs" Inherits="Employee_Amoozesh_Teachers"
    Title="اساتید" %>

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
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <div dir="rtl">
        <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
            [<a class="closeLink" href="#">بستن</a>]
        </div>

        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>
                    <table>
                        <tbody>
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                        ID="BtnNew" EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click"
                                        UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/new.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                        Width="25px" ID="btnEdit" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click"
                                        UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
			if (GridViewTeacher.GetFocusedRowIndex()&lt;0)
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
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                        ID="btnView" EnableViewState="False" EnableTheming="False" OnClick="btnView_Click"
                                        UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
		if (GridViewTeacher.GetFocusedRowIndex()&lt;0)
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
                                <td style="width: 30px">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال"
                                        CausesValidation="False" ID="btnInActive" EnableClientSideAPI="True" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnInActive_Click" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td style="width: 30px">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="فعال"
                                        CausesValidation="False" ID="btnActive" EnableClientSideAPI="True" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnActive_Click" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="../../Images/icons/Active.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>                               
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnRevival" runat="server" AutoPostBack="False" 
                                        EnableTheming="False" EnableViewState="False" OnClick="btnRevival_Click" Text=" "
                                        ToolTip="درخواست تمدید" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacher.GetFocusedRowIndex()&lt;0)
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
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnCertificateChange" runat="server" AutoPostBack="False" 
                                        EnableTheming="False" EnableViewState="False" OnClick="btnCertificateChange_Click"
                                        Text=" " ToolTip="درخواست تغییرات" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacher.GetFocusedRowIndex()&lt;0)
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
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnReset" runat="server" CausesValidation="False" 
                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="بازیابی رمز عبور"
                                        UseSubmitBehavior="False" OnClick="btnReset_Click">
                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacher.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/ChangePassword.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گردش کار"
                                        ID="btnSendNextStep" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                        UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacher.GetFocusedRowIndex()&lt;0)
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
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="پیگیری"
                                        ID="btnTracing" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                        UseSubmitBehavior="False" OnClick="btnTracing_Click">
                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacher.GetFocusedRowIndex()&lt;0)
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
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                        ID="btnPrint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False">
                                        <ClientSideEvents Click="function(s,e){GridViewTeacher.PerformCallback('Print'); }"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/Printers.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
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
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="راهنما"
                                        ID="btnHelp" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e){ ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/Help.png">
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
        <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>


                    <table width="100%">
                        <tr>
                            <td align="right" valign="top" width="15%">
                                <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="نام">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" valign="top" width="35%">
                                <TSPControls:CustomTextBox IsMenuButton="true" ID="txtName" runat="server" ClientInstanceName="txtName" 
                                     Width="100%" >
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                </TSPControls:CustomTextBox>
                            </td>
                            <td align="right" valign="top" width="15%">
                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="نام خانوادگی">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" valign="top" width="35%">
                                <TSPControls:CustomTextBox IsMenuButton="true" ID="txtFamily" runat="server" ClientInstanceName="txtFamily" 
                                     Width="100%" >
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="رشته">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomAspxComboBox ID="cmbMajor" runat="server" 
                                     DataSourceID="ODBMajor" 
                                    TextField="MjName" Width="100%" ValueField="MjId" ValueType="System.String">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                </TSPControls:CustomAspxComboBox>
                            </td>
                            <td align="right" valign="top">
                                <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="مقطع تحصیلی">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomAspxComboBox ID="cmbLicence" runat="server" 
                                     DataSourceID="ODBLicence" 
                                    TextField="LiName" ValueField="LiId" Width="100%" ValueType="System.String">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                </TSPControls:CustomAspxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="تاریخ اعتبار از">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" valign="top">
                                <pdc:PersianDateTextBox ID="txtEndDateFrom" runat="server" onkeypress="SearchKeyPress(event,2);" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                    PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                    Width="300px"></pdc:PersianDateTextBox>
                            </td>
                            <td align="right" style="width: 104px" valign="top">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="تاریخ اعتبار تا">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" valign="top">
                                <pdc:PersianDateTextBox ID="txtEndDateTo" runat="server" onkeypress="SearchKeyPress(event,2);" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                    PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                    Width="300px"></pdc:PersianDateTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4" valign="top">
                                <br />
                                <table>
                                    <tr>
                                        <td style="width: 100px">
                                            <TSPControls:CustomAspxButton  ID="btnSearch" ClientInstanceName="btnSearch" runat="server" 
                                                  Text="جستجو"
                                                Width="98px" OnClick="btnSearch_Click">
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td style="width: 100px">
                                            <TSPControls:CustomAspxButton  ID="btnClearsearch" runat="server" 
                                                  Text="پاک کردن فرم" OnClick="btnClearsearch_Click">
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
        <dx:ASPxGridViewExporter ID="GridViewExporter" GridViewID="GridViewTeacher" runat="server">
        </dx:ASPxGridViewExporter>
        <TSPControls:CustomAspxDevGridView ID="GridViewTeacher" runat="server" 
            Width="100%" ClientInstanceName="GridViewTeacher"
            EnableViewState="False" AutoGenerateColumns="False" DataSourceID="OdbTeacher"
            KeyFieldName="TeId" OnCustomCallback="GridViewTeacher_CustomCallback" OnDetailRowExpandedChanged="GridViewTeacher_DetailRowExpandedChanged"
            OnHtmlRowPrepared="GridViewTeacher_HtmlRowPrepared" OnHtmlDataCellPrepared="GridViewTeacher_HtmlDataCellPrepared"
            RightToLeft="True">
            <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True" ColumnResizeMode="Control"></SettingsBehavior>

            <Columns>
                <dxwgv:GridViewDataComboBoxColumn Caption=" " FieldName="IsExpired" Name="IsExpired"
                    VisibleIndex="0" Width="30px">
                    <PropertiesComboBox ValueType="System.String">
                        <Items>
                            <dxe:ListEditItem Text="پایان اعتبار" Value="1"></dxe:ListEditItem>
                            <dxe:ListEditItem Text="دارای اعتبار" Value="0"></dxe:ListEditItem>
                        </Items>
                    </PropertiesComboBox>
                    <DataItemTemplate>
                        <div align="center">
                            <dxe:ASPxImage ID="btnIsExpired" runat="server" Width="16px" ImageUrl="~/Images/CertificateValid.png"
                                Height="16px">
                            </dxe:ASPxImage>
                        </div>
                    </DataItemTemplate>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                    VisibleIndex="0">
                    <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                        ValueType="System.String" IncrementalFilteringMode="StartsWith">
                    </PropertiesComboBox>
                    <DataItemTemplate>
                        <div align="center">
                            <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl="~/Images/WFStart.png">
                            </dxe:ASPxImage>
                        </div>
                    </DataItemTemplate>
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataTextColumn Caption="کد" FieldName="TiId" Visible="False" VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" Visible="False"
                    VisibleIndex="3">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="شماره پرونده" FieldName="FileNo" Visible="False"
                    VisibleIndex="0">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="Name" VisibleIndex="0">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="Family" VisibleIndex="1">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="نام کاربری" FieldName="UserName" VisibleIndex="2">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="نام پدر" FieldName="Father" VisibleIndex="3">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="تاریخ تولد" FieldName="BirthDate" Visible="False"
                    VisibleIndex="3">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="شماره شناسنامه" FieldName="IdNo" Visible="False"
                    VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="مدرک تحصیلی" FieldName="LiName" VisibleIndex="4">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="رشته" FieldName="MjName" VisibleIndex="5">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="شماره همراه" FieldName="MobileNo" Visible="False"
                    VisibleIndex="5">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="آدرس پست الکترونیکی" FieldName="Email" Visible="False"
                    VisibleIndex="6">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="6"
                    Width="60px">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="تاریخ اعتبار" FieldName="EndDate" Name="EndDate"
                    VisibleIndex="6">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="وضعیت پرونده" FieldName="TaskId" Visible="False"
                    VisibleIndex="6" Width="300px">
                    <CellStyle HorizontalAlign="Right">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>

                <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="7" Width="30px" ShowClearFilterButton="true">
                 
                </dxwgv:GridViewCommandColumn>
            </Columns>

            <Settings ShowHorizontalScrollBar="True"></Settings>
            <ClientSideEvents
                EndCallback="function(s, e) {
	 if(s.cpPrint==1)
        {
         s.cpPrint=0;
            window.open('../../Print.aspx');
        }
}" />
            <Templates>
                <DetailRow>
                    <div align="right">
                        <TSPControls:CustomAspxDevGridView ID="GridViewTeacherCertificate" runat="server" AutoGenerateColumns="False"
                            DataSourceID="ObjdsTeacherCertificate"
                            KeyFieldName="TcId" OnBeforePerformDataSelect="GridViewTeacherCertificate_BeforePerformDataSelect"
                            OnHtmlDataCellPrepared="GridViewTeacherCertificate_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="GridViewTeacherCertificate_AutoFilterCellEditorInitialize">
                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />


                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="نوع درخواست" FieldName="CertificateType" VisibleIndex="0">
                                    <HeaderStyle Wrap="False" />
                                </dxwgv:GridViewDataTextColumn>
                                
                  <dxwgv:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="Date" VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="وضعیت درخواست" FieldName="IsConf" VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="شماره مجوز تدریس" FieldName="FileNo" VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="شماره سریال" FieldName="SerialNo" VisibleIndex="1">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="تاریخ صدور" FieldName="StartDate" VisibleIndex="2">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان اعتبار" FieldName="EndDate" VisibleIndex="3">
                                </dxwgv:GridViewDataTextColumn>


                                <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="6" ShowClearFilterButton="true">
                                 
                                </dxwgv:GridViewCommandColumn>
                            </Columns>

                            <SettingsDetail IsDetailGrid="True" />
                        </TSPControls:CustomAspxDevGridView>
                    </div>
                </DetailRow>
            </Templates>
            <SettingsDetail ExportMode="None" AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />

        </TSPControls:CustomAspxDevGridView>
        <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewTeacher" SessionName="SendBackDataTable_Teacher"
            OnCallback="WFUserControl_Callback" />
        <br />
        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>

                    <table>
                        <tbody>
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                        ID="BtnNew2" EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click"
                                        UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/new.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                        Width="25px" ID="btnEdit2" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click"
                                        UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
		if (GridViewTeacher.GetFocusedRowIndex()&lt;0)
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
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                        ID="btnView2" EnableViewState="False" EnableTheming="False" OnClick="btnView_Click"
                                        UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
		if (GridViewTeacher.GetFocusedRowIndex()&lt;0)
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
                                <td style="width: 30px">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال"
                                        CausesValidation="False" ID="btnInActive2" EnableClientSideAPI="True" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnInActive_Click" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td style="width: 30px">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="فعال"
                                        CausesValidation="False" ID="btnActive2" EnableClientSideAPI="True" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnActive_Click" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="../../Images/icons/Active.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                             
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnRevival2" runat="server" AutoPostBack="False" 
                                        EnableTheming="False" EnableViewState="False" OnClick="btnRevival_Click" Text=" "
                                        ToolTip="درخواست تمدید" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacher.GetFocusedRowIndex()&lt;0)
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
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnCertificateChange2" runat="server" AutoPostBack="False" 
                                        EnableTheming="False" EnableViewState="False" OnClick="btnCertificateChange_Click"
                                        Text=" " ToolTip="درخواست تغییرات" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacher.GetFocusedRowIndex()&lt;0)
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
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnReset1" runat="server" CausesValidation="False" 
                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="بازیابی رمز عبور"
                                        UseSubmitBehavior="False" OnClick="btnReset_Click">
                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacher.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/ChangePassword.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گردش کار"
                                        ID="btnSendNextStep2" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                        UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
		if (GridViewTeacher.GetFocusedRowIndex()&lt;0)
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
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="پیگیری"
                                        ID="btnTracing2" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                        UseSubmitBehavior="False" OnClick="btnTracing_Click">
                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacher.GetFocusedRowIndex()&lt;0)
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
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                        ID="btnPrint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False">
                                        <ClientSideEvents Click="function(s,e){GridViewTeacher.PerformCallback('Print'); }"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/Printers.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                        ID="btnExportExel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        OnClick="btnExportExcel_Click">
                                        <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/ExportExcel.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="راهنما"
                                        ID="btnHelp2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e){ ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/Help.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>

                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanelMenu>
        <asp:ObjectDataSource ID="OdbTeacher" runat="server" SelectMethod="SelectTeacher"
            TypeName="TSP.DataManager.TeacherManager" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="LiId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="MjId" Type="Int32" />
                <asp:Parameter DefaultValue="%" Name="Name" Type="String" />
                <asp:Parameter DefaultValue="%" Name="Family" Type="String" />
                <asp:Parameter DefaultValue="1" Name="EndDateFrom" Type="String" />
                <asp:Parameter DefaultValue="2" Name="EndDateTo" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ODBMajor" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ODBLicence" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.LicenceManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkId"
            TypeName="TSP.DataManager.WorkFlowTaskManager" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="WorkFlowId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="TaskOrder" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjdsTeacherCertificate" runat="server" SelectMethod="SelectTeachersCertificate"
            TypeName="TSP.DataManager.TeacherCertificateManager" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="TcId" Type="Int32" />
                <asp:SessionParameter DefaultValue="-1" Name="TeId" SessionField="TeId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="Type" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>

    <dx:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
    </dx:ASPxHiddenField>
</asp:Content>
