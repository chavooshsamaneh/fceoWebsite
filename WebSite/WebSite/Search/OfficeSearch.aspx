<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="OfficeSearch.aspx.cs" Inherits="Search_OfficeSearch" Title="جستجوی اعضای حقوقی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <script language="javascript" type="text/javascript">
        function MemberSearch(s, e) {
            if (ASPxClientEdit.ValidateGroup('Member') == false)
                return;
            grdMembers.PerformCallback('');

            if (CheckDate() == -1) {
                e.processOnServer = false;
                lblDateError.SetVisible(true);
            }
            else {
                lblDateError.SetVisible(false);
            }
        }

        /************** Grid Selection *********************/
        var _selectNumber = 0;
        var _handle = true;

        function cbSelectAllCheckedChanged(s, e) {
            if (s.GetChecked())
                grdMembers.SelectRows();
            else
                grdMembers.UnselectRows();
        }

        function OnGridSelectionChanged(s, e) {
            cbSelectAll.SetChecked(s.GetSelectedRowCount() == s.cpVisibleRowCount);

            if (e.isChangedOnServer == false) {
                if (e.isAllRecordsOnPage && e.isSelected)
                    _selectNumber = s.GetVisibleRowsOnPage();
                else if (e.isAllRecordsOnPage && !e.isSelected)
                    _selectNumber = 0;
                else if (!e.isAllRecordsOnPage && e.isSelected)
                    _selectNumber++;
                else if (!e.isAllRecordsOnPage && !e.isSelected)
                    _selectNumber--;

                _handle = true;
            }
            //  if (chkMultiSelect.GetChecked() == true) 
            //  {
            if (grdMembers.GetSelectedRowCount() > 0) {
                txtSelectedMeId.SetText('در حال بارگذاری...');
                grdMembers.GetSelectedFieldValues('OfId', OnGetSelectedFieldValues);
            }
            else
                txtSelectedMeId.SetText('');
            //  }
        }
        function OnGridEndCallback(s, e) {
            _selectNumber = s.cpSelectedRowsOnPage;
        }
        function OnGetSelectedFieldValues(selectedValues) {
            if (selectedValues.length == 0) return;
            var OfId = '';
            for (i = 0; i < selectedValues.length; i++) {
                if (OfId != '') OfId += ';';
                OfId += selectedValues[i];
            }
            txtSelectedMeId.SetText(OfId);
        }

        /*******************************************************/
    </script>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table dir="rtl" align="right">
                    <tr>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint" runat="server" AutoPostBack="False" 
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                <Image Height="25px" Url="~/Images/icons/printers.png" Width="25px" />
                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../Print.aspx&quot;);		

}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                </HoverStyle>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False" 
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                UseSubmitBehavior="False" OnClick="btnExportExcel_Click">
                                <ClientSideEvents Click="function(s,e){  }" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                </HoverStyle>
                                <Image Height="25px" Url="~/Images/icons/ExportExcel.png" Width="25px" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton2" runat="server" AutoPostBack="False" 
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="انتخاب ستون ها"
                                UseSubmitBehavior="False">
                                <Image Height="25px" Url="~/Images/icons/cursor-hand.png" Width="25px" />
                                <ClientSideEvents Click="function(s, e) {
	if(!grdMembers.IsCustomizationWindowVisible())
		grdMembers.ShowCustomizationWindow();
	else
		grdMembers.HideCustomizationWindow();
}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                </HoverStyle>
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSPControls:CustomASPxRoundPanel ID="CustomASPxRoundPanelMenu2" HeaderText="جستجو" runat="server" ShowCollapseButton="true"
        Width="100%">

        <PanelCollection>
            <dxp:PanelContent>
                <table width="100%">
                    <tr>
                        <td align="right" valign="top" width="15%">
                            کد شرکت
                        </td>
                        <td align="right" valign="top" width="35%">
                            <TSPControls:CustomTextBox ID="txtOfId" runat="server" 
                                 ClientInstanceName="TextOfId">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="bottom"
                                    ValidationGroup="Member">
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت نا معتبر است" />
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right" valign="top" width="15%">
                            نام شرکت
                        </td>
                        <td align="right" valign="top" width="35%">
                            <TSPControls:CustomTextBox ID="txtFName" runat="server" 
                                 ClientInstanceName="TextOfName">
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
                        <td align="right" valign="top">
                            تاریخ ثبت از
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox runat="server" DefaultDate=""  ShowPickerOnTop="True" Width="300px"
                                onkeypress="SearchKeyPress(event,2,btnSearch);" ID="txtFromDate" PickerDirection="ToRight"
                                IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                            <dxe:ASPxLabel ID="ASPxLabel10" runat="server" ClientInstanceName="lblDateError"
                                ClientVisible="False" ForeColor="Red" Text="محدوده تاریخ وارد شده صحیح نمی باشد">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top">
                            تاریخ ثبت تا
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox ID="txtToDate" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif" Width="300px"
                                onkeypress="SearchKeyPress(event,2,btnSearch);" PickerDirection="ToRight" ShowPickerOnTop="True"
                                ></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            نوع پروانه
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxComboBox ID="cmbMFType" runat="server" 
                                  ValueType="System.String"
                                RightToLeft="True"  HorizontalAlign="Right" EnableIncrementalFiltering="True"
                                ClientInstanceName="ComboMF">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <ValidationSettings>
                                    
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <Items>
                                    <dxe:ListEditItem Text="طراح و ناظر" Value="1" />
                                    <dxe:ListEditItem Text="مجری" Value="2" />
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td align="right" valign="top">
                            کد عضویت شرکا
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomTextBox ID="txtMeId" runat="server" 
                                  ClientInstanceName="TextMeId">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom"
                                    ValidationGroup="Member">
                                    
                                    <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت نامعتبر است" />
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  align="right" valign="top">
                            وضعیت پروانه
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxComboBox ID="comboBoxDocStatus" runat="server" 
                                  ValueType="System.String"
                                RightToLeft="True"  HorizontalAlign="Right" EnableIncrementalFiltering="True"
                                ClientInstanceName="comboBoxDocStatus" >
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <ItemStyle HorizontalAlign="Right" />
                                <Items>
                                    <dxe:ListEditItem Text="فاقد پروانه" Value="0" />
                                    <dxe:ListEditItem Text="درجریان" Value="1" />
                                    <dxe:ListEditItem Text="تایید شده" Value="2" />
                                    <dxe:ListEditItem Text="عدم تایید" Value="3" />
                                    <dxe:ListEditItem Text="ابطال پروانه" Value="4" />
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <br />
                            <table>
                                <tr>
                                    <td width="50%" align="left">
                                        <TSPControls:CustomAspxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" CausesValidation="False"
                                            Text="&nbsp;جستجو"  
                                            Width="126px" ID="ASPxButton3" ClientInstanceName="btnSearch">
                                            <Image Width="20px" Height="20px" Url="~/Images/icons/Search.png" />
                                            <ClientSideEvents Click="MemberSearch" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="50%" align="right">
                                        <TSPControls:CustomAspxButton runat="server" Text="&nbsp;پاک کردن فرم" CausesValidation="False"
                                            ID="btnMeRefresh" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="False"  
                                             Width="126px">
                                            <Image Height="20px" Width="20px" Url="~/Images/icons/Clear-Form.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
 e.processOnServer= false;
 if(confirm('آیا مطمئن به پاک کردن اطلاعات فرم هستید؟'))
	SetEmpty();
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
    <TSPControls:CustomAspxDevGridView ID="GridViewOffice" runat="server" AutoGenerateColumns="False"
        ClientInstanceName="grdMembers" RightToLeft="True" DataSourceID="ObjectDataSourceOffice"
       KeyFieldName="OfId" OnCustomCallback="GridViewOffice_CustomCallback"
        OnCustomJSProperties="GridViewOffice_CustomJSProperties" OnHtmlDataCellPrepared="GridViewOffice_HtmlDataCellPrepared"
        OnAutoFilterCellEditorInitialize="GridViewOffice_AutoFilterCellEditorInitialize"
        Width="100%">
        <ClientSideEvents EndCallback="OnGridEndCallback" RowDblClick="function(s,e){s.SelectRowOnPage(e.visibleIndex);}"
            SelectionChanged="OnGridSelectionChanged" />
        <SettingsCustomizationWindow Enabled="True" PopupHorizontalAlign="Center" PopupVerticalAlign="Middle" />
     <SettingsPager Position="TopAndBottom">
        </SettingsPager>
        <SettingsCookies Enabled="true" />
        <Columns>
            <dxwgv:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" AllowDragDrop="False"
                Width="50px">
                <HeaderStyle HorizontalAlign="Center">
                    <Paddings PaddingTop="1px" PaddingBottom="1px"></Paddings>
                </HeaderStyle>
                <HeaderTemplate>
                    <TSPControls:CustomASPxCheckBox ID="cbSelectAll" runat="server" ClientInstanceName="cbSelectAll"
                        OnInit="cbSelectAll_Init">
                        <ClientSideEvents CheckedChanged="cbSelectAllCheckedChanged" />
                    </TSPControls:CustomASPxCheckBox>
                </HeaderTemplate>
            </dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewCommandColumn Caption=" " Visible="true" VisibleIndex="40" Width="30px"
                AllowDragDrop="False" ShowClearFilterButton="true">
        
            </dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد شرکت" FieldName="OfId" VisibleIndex="0">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام شرکت" Width="250px" FieldName="OfName"
                VisibleIndex="1">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نوع شرکت" FieldName="OtName" VisibleIndex="2">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="مدیر مسئول" FieldName="Manager" VisibleIndex="2">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="موضوع" FieldName="Subject" Visible="False"
                VisibleIndex="3">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تعداد اعضا" FieldName="MemberCount" VisibleIndex="3">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره ثبت" FieldName="RegOfNo" VisibleIndex="3">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ ثبت شرکت" FieldName="RegDate" VisibleIndex="4">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره پروانه" FieldName="FileNo" VisibleIndex="5">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه شرکت" FieldName="GrdName" VisibleIndex="5">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewDataTextColumn Caption="نوع پروانه" FieldName="MFTypeName" VisibleIndex="6">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ ثبت در سیستم" FieldName="CreateDate"
                VisibleIndex="7">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره همراه" FieldName="MobileNo" Visible="False"
                VisibleIndex="8">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره تلفن 1" FieldName="Tel1" Visible="False"
                VisibleIndex="8">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره تلفن 2" FieldName="Tel2" Visible="False"
                VisibleIndex="8">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره فکس" FieldName="Fax" Visible="False"
                VisibleIndex="8">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره عضویت" FieldName="MeNo" Visible="False"
                VisibleIndex="8">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ اعتبار پروانه" FieldName="FileDate"
                Visible="False" VisibleIndex="8">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ آخرین تمدید" FieldName="LastRegDate"
                Visible="False" VisibleIndex="8">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت پروانه" FieldName="DocumentStatusName"
                VisibleIndex="8" Width="100px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت عضویت" FieldName="MrsName" Visible="False"
                VisibleIndex="8">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="آدرس" FieldName="Address" Visible="False"
                VisibleIndex="8">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            
        </Columns>
    </TSPControls:CustomAspxDevGridView>
    <br />
    <dxp:ASPxPanel ID="PanelSelectedMeId" runat="server" ClientInstanceName="PanelSelectedMeId">
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent1" runat="server">
                <table width="100%">
                    <tr>
                        <td align="right">
                            کدهای عضویت انتخاب شده :
                        </td>
                        <td align="left">
                            <table >
                                <tr>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="کپی"
                                            ID="btnCopy" EnableViewState="False" EnableTheming="False" AutoPostBack="false"
                                            UseSubmitBehavior="False">
                                           
                                            <Image  Url="~/Images/icons/Copy2.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s,e){copyToClipboard(txtSelectedMeId.GetText());}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف انتخاب ها"
                                            ID="btnClear" EnableViewState="False" EnableTheming="False" AutoPostBack="false"
                                            UseSubmitBehavior="False">
                                           
                                            <Image  Url="~/Images/icons/Clear-Form.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s,e){grdMembers.UnselectRows();}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <TSPControls:CustomASPXMemo ID="txtSelectedMeId" runat="server" 
                     Height="80px" Width="100%" ReadOnly="true" ClientInstanceName="txtSelectedMeId">
                    <ValidationSettings>
                        
                        <ErrorFrameStyle ImageSpacing="4px">
                            <ErrorTextPaddings PaddingLeft="4px" />
                        </ErrorFrameStyle>
                    </ValidationSettings>
                </TSPControls:CustomASPXMemo>
            </dxp:PanelContent>
        </PanelCollection>
    </dxp:ASPxPanel>
    <asp:ObjectDataSource ID="ObjectDataSourceOffice" runat="server" SelectMethod="SelectOfficeForSearch"
        TypeName="TSP.DataManager.OfficeManager" CacheExpirationPolicy="Sliding" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="OfId" Type="Int32" />
            <asp:Parameter Name="MeId" Type="Int32" />
            <asp:Parameter Name="OfName" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="MFType" Type="Int16" />
            <asp:Parameter DefaultValue="1" Name="FromDate" Type="String" />
            <asp:Parameter DefaultValue="2" Name="ToDate" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="DocumentStatus" Type="Int16" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" ExportEmptyDetailGrid="True"
        GridViewID="GridViewOffice" ExportedRowType="Selected">
    </dxwgv:ASPxGridViewExporter>
</asp:Content>
