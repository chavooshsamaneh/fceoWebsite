<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="EngOfficeSearch.aspx.cs" Inherits="Search_EngOfficeSearch" Title="جستجوی دفاتر" %>

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
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
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
            //  if (chkMultiSelect.GetChecked() == true) {
            if (grdMembers.GetSelectedRowCount() > 0) {
                txtSelectedMeId.SetText('در حال بارگذاری...');
                grdMembers.GetSelectedFieldValues('EngOfId', OnGetSelectedFieldValues);
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
            var EngOfId = '';
            for (i = 0; i < selectedValues.length; i++) {
                if (EngOfId != '') EngOfId += ';';
                EngOfId += selectedValues[i];
            }
            txtSelectedMeId.SetText(EngOfId);
        }

        function OnListBoxSelectionChanged(listBox, DropDown, indexAll, ItemsDifferentFromOther, args) {
            if (indexAll != -1 && args.index == indexAll) {
                if (args.isSelected) {
                    ChangeSelectionItem(listBox, ItemsDifferentFromOther, true);
                }
                else {
                    ChangeSelectionItem(listBox, ItemsDifferentFromOther, false);
                }
            }
            UpdateSelectAllItemState(listBox, indexAll, ItemsDifferentFromOther);
            UpdateText(listBox, DropDown, indexAll);
        }
        function UpdateSelectAllItemState(listBox, indexAll, ItemsDifferentFromOther) {
            IsAllSelected(listBox, indexAll, ItemsDifferentFromOther) ? listBox.SelectIndices([indexAll]) : listBox.UnselectIndices([indexAll]);
        }
        function IsAllSelected(listBox, indexAll, ItemsDifferentFromOther) {
            for (var i = 0; i < listBox.GetItemCount() ; i++) {
                if (i != indexAll) {
                    if (CheckIndexIsInItemsDifferentFromOther(ItemsDifferentFromOther, i) == false)
                        if (!listBox.GetItem(i).selected)
                            return false;
                }
            }
            return true;
        }
        function UpdateText(listBox, DropDown, indexAll) {
            var selectedItems = listBox.GetSelectedItems();
            DropDown.SetText(GetSelectedItemsText(selectedItems, indexAll));
        }
        function GetSelectedItemsText(items, indexAll) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                if (items[i].index != indexAll)
                    texts.push(items[i].text);
            return texts.join(',');
        }
        function ChangeSelectionItem(listBox, ItemsDifferentFromOther, SelectionStatus) {
            for (var i = 0; i < listBox.GetItemCount() ; i++)
                if (CheckIndexIsInItemsDifferentFromOther(ItemsDifferentFromOther, i) == false) {
                    if (SelectionStatus == true)
                        listBox.SelectIndices([i]);
                    else
                        listBox.UnselectIndices([i])
                }
        }
        function CheckIndexIsInItemsDifferentFromOther(ItemsDifferentFromOther, Index) {
            var Items = ItemsDifferentFromOther.split(';');
            for (var i = 0; i < Items.length; i++)
                if (Items[i] != '' && Items[i] == Index.toString())
                    return true;
            return false;
        }
        /*******************************************************/
    </script>
 <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu"   runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                <table dir="rtl" width="100%" align="right">
                    <tr>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                            <Image  Url="~/Images/icons/printers.png"  />
                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../Print.aspx&quot;);		

}" />
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                            UseSubmitBehavior="False" OnClick="btnExportExcel_Click">
                                            <ClientSideEvents Click="function(s,e){  }" />
                                           
                                            <Image  Url="~/Images/icons/ExportExcel.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton2" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="انتخاب ستون ها"
                                            UseSubmitBehavior="False">
                                            <Image  Url="~/Images/icons/cursor-hand.png"  />
                                            <ClientSideEvents Click="function(s, e) {
	if(!grdMembers.IsCustomizationWindowVisible())
		grdMembers.ShowCustomizationWindow();
	else
		grdMembers.HideCustomizationWindow();
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
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
   <TSPControls:CustomASPxRoundPanel ID="SearchPanel" HeaderText="جستجو" runat="server" ShowCollapseButton="true">
        <PanelCollection>
            <dxp:PanelContent>
                <table width="100%">
                    <tr>
                        <td align="right" valign="top" width="15%">کد دفتر
                        </td>
                        <td align="right" valign="top" width="35%">
                            <TSPControls:CustomTextBox ID="txtEngOfId" runat="server"
                                ClientInstanceName="TextEngOfId">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom"
                                    ValidationGroup="Member">

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت نامعتبر است" />
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right" valign="top" width="15%">نوع دفتر
                        </td>
                        <td align="right" valign="top" width="35%">
                            <TSPControls:CustomAspxComboBox ID="CmbEngType" runat="server"
                                ValueType="System.String"
                                RightToLeft="True" ClientInstanceName="ComboEngType" HorizontalAlign="Right"
                                EnableIncrementalFiltering="True" TextField="Name" ValueField="EOfTId" DataSourceID="OdbEngOfficeType">
                                <ItemStyle HorizontalAlign="Right" />
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <ValidationSettings>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">تاریخ درخواست از
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox  Width="250px" runat="server" DefaultDate="" ShowPickerOnTop="True"
                                onkeypress="SearchKeyPress(event,2,btnSearch);" ID="txtFromDate" ShowPickerOnEvent="OnClick"
                                PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                            <dxe:ASPxLabel ID="ASPxLabel10" runat="server" ClientInstanceName="lblDateError"
                                ClientVisible="False" ForeColor="Red" Text="محدوده تاریخ وارد شده صحیح نمی باشد">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top">تاریخ درخواست تا
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox  Width="250px" ID="txtToDate" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                ShowPickerOnEvent="OnClick" PickerDirection="ToRight" ShowPickerOnTop="True"
                                onkeypress="SearchKeyPress(event,2,btnSearch);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel1" Wrap="False" runat="server" Text="تاریخ اعتبار از"
                                Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right"  >
                            <pdc:PersianDateTextBox  Width="250px"  ID="txtEndDateFrom" ShowPickerOnEvent="OnClick" runat="server"
                                DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" RightToLeft="False"
                                Style="direction: ltr; text-align: right;" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel9" Wrap="False" runat="server" Text="تاریخ اعتبار تا"
                                Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox  Width="250px"  ID="txtEndDateTo" ShowPickerOnEvent="OnClick" runat="server"
                                DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" RightToLeft="False"
                                Style="direction: ltr; text-align: right;" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">کد عضویت
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
                        <td align="right" valign="top">مدیر مسئول
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomTextBox ID="txtManagerName" runat="server"
                                ClientInstanceName="txtManagerName">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">تلفن
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomTextBox ID="txtTel" runat="server"
                                ClientInstanceName="txtTel">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right" valign="top">نام دفتر
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomTextBox ID="txtName" runat="server"
                                ClientInstanceName="txtName">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">وضعیت عضویت
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomASPXDropDownEdit ID="drdEngOfficeStatus" RightToLeft="True" runat="server"
                                ClientInstanceName="drdEngOfficeStatus"
                               >
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <DropDownWindowTemplate>
                                    <TSPControls:CustomASPxListBox RightToLeft="True" ID="ListBoxEngOfficeStatus" runat="server" SelectionMode="CheckColumn"
                                        ClientInstanceName="ListBoxEngOfficeStatus">
                                        <Items>
                                            <dxe:ListEditItem Text="در جریان" Value="0" />
                                            <dxe:ListEditItem Selected="true" Text="تایید شده" Value="1" />
                                            <dxe:ListEditItem Text="تایید نشده" Value="2" />
                                            <dxe:ListEditItem Text="باطل شده" Value="3" />
                                        </Items>
                                        <ClientSideEvents SelectedIndexChanged="function(s,e){ OnListBoxSelectionChanged(s,drdEngOfficeStatus,0,'',e); }" />
                                    </TSPControls:CustomASPxListBox>
                                </DropDownWindowTemplate>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </TSPControls:CustomASPXDropDownEdit>
                        </td>
                        <td align="right" valign="top">
                            <dxe:ASPxLabel ID="ASPxLabel8" Wrap="False" runat="server" Text="نوع درخواست" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxComboBox ID="CmbReqType" runat="server"
                                ValueType="System.String"
                                RightToLeft="True" ClientInstanceName="CmbReqType" HorizontalAlign="Right"
                                EnableIncrementalFiltering="True">
                                <ItemStyle HorizontalAlign="Right" />
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                <Items>
                                    <dxe:ListEditItem Text="صدور پروانه" Value="0" />
                                    <dxe:ListEditItem Text="تمدید" Value="1" />
                                    <dxe:ListEditItem Text="تغییرات" Value="2" />
                                    <dxe:ListEditItem Text="المثنی" Value="3" />
                                    <dxe:ListEditItem Text="ابطال" Value="4" />
                                    <dxe:ListEditItem Text="صدور سیستم قدیم" Value="5" />
                                    <dxe:ListEditItem Text="تغییرات اطلاعات پایه" Value="6" />
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel2" Wrap="False" runat="server" Text="تاریخ صدور از" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" >
                            <pdc:PersianDateTextBox  Width="250px"  ID="txtFirstRegDateFrom" ShowPickerOnEvent="OnClick" runat="server"
                                DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" RightToLeft="False"
                                Style="direction: ltr; text-align: right;" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel3" Wrap="False" runat="server" Text="تاریخ صدور تا" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox  Width="250px"  ID="txtFirstRegDateTo" ShowPickerOnEvent="OnClick" runat="server"
                                DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" RightToLeft="False"
                                Style="direction: ltr; text-align: right;" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel4" Wrap="False" runat="server" Text="تاریخ تمدید از"
                                Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" >
                            <pdc:PersianDateTextBox  Width="250px"  ID="txtLastRegDateFrom" ShowPickerOnEvent="OnClick" runat="server"
                                DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" RightToLeft="False"
                                Style="direction: ltr; text-align: right;" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right">
                            <dxe:ASPxLabel ID="ASPxLabel5" Wrap="False" runat="server" Text="تاریخ تمدید تا"
                                Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right">
                            <pdc:PersianDateTextBox  Width="250px"  ID="txtLastRegDateTo" ShowPickerOnEvent="OnClick" runat="server"
                                DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" RightToLeft="False"
                                Style="direction: ltr; text-align: right;" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <br />
                            <table>
                                <tr>
                                    <td style="width: 100px">
                                        <TSPControls:CustomAspxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" CausesValidation="False"
                                            Text="&nbsp;جستجو"
                                            Width="126px" ID="ASPxButton10" ClientInstanceName="btnSearch">
                                            <Image Width="20px" Height="20px" Url="~/Images/icons/Search.png" />
                                            <ClientSideEvents Click="MemberSearch" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="width: 100px">
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

    <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
        ClientInstanceName="grdMembers"
        RightToLeft="True" DataSourceID="ObjectDataSource1" 
        KeyFieldName="EngOfId" OnCustomCallback="CustomAspxDevGridView1_CustomCallback"
        OnCustomJSProperties="CustomAspxDevGridView1_CustomJSProperties" 
        Width="100%">
        <ClientSideEvents EndCallback="OnGridEndCallback" RowDblClick="function(s,e){s.SelectRowOnPage(e.visibleIndex);}"
            SelectionChanged="OnGridSelectionChanged" />
        <Columns>

            <dxwgv:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="50px"
                AllowDragDrop="False">
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
            
                     <dxwgv:GridViewDataTextColumn Caption="وضعیت عضو در دفتر" FieldName="MemberActiveStatus" VisibleIndex="0">
                <CellStyle Wrap="false" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد دفتر" FieldName="EngOfId" VisibleIndex="0">
                <CellStyle Wrap="false" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام دفتر" Width="200px" FieldName="EngOffName"
                VisibleIndex="0">
                <CellStyle Wrap="false">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نوع دفتر" FieldName="OfTName" VisibleIndex="1">
                <CellStyle Wrap="false" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>


            <dxwgv:GridViewDataTextColumn Caption="کد عضویت مدیر مسئول" Width="100px" FieldName="ManagerMeId"
                VisibleIndex="3">
                <CellStyle Wrap="false">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام مدیر مسئول" Width="100px" FieldName="MeFirstName"
                VisibleIndex="3">
                <CellStyle Wrap="false">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی مدیر مسئول" Width="170px" FieldName="MeLastName"
                VisibleIndex="3">
                <CellStyle Wrap="false">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تعداد اعضا" FieldName="MeCount" VisibleIndex="4">
                <CellStyle Wrap="false" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره مشارکت نامه" FieldName="ParticipateLetterNo"
                VisibleIndex="2" Visible="false">
                <CellStyle Wrap="false" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ مشارکت نامه" FieldName="ParticipateLetterDate"
                VisibleIndex="5" Visible="false">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره دفتر اسناد رسمی" FieldName="EngOffNo"
                VisibleIndex="7" Visible="False">
                <CellStyle Wrap="false" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره پروانه" FieldName="FileNo" VisibleIndex="6">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ ثبت در سیستم" FieldName="CreateDate"
                VisibleIndex="7">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="محل دفتر اسناد رسمی" FieldName="EngOffLoc"
                VisibleIndex="8" Visible="false">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره تلفن" FieldName="TellNo" VisibleIndex="9">
                <CellStyle Wrap="false" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره فکس" FieldName="FaxNo" VisibleIndex="9"
                Visible="false">
                <CellStyle Wrap="false" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره تلفن همراه" FieldName="MobileNo" VisibleIndex="9"
                Visible="false">
                <CellStyle Wrap="false">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پست الکترونیکی" FieldName="Email" VisibleIndex="9"
                Visible="false">
                <CellStyle Wrap="false" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="آدرس" FieldName="Address" VisibleIndex="9">
                <CellStyle Wrap="false">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ اولین صدور" FieldName="FirstRegDate"
                VisibleIndex="9" Visible="false">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ آخرین تمدید" FieldName="LastRegDate"
                VisibleIndex="9" Visible="false">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان اعتبار" FieldName="LastExpireDate"
                VisibleIndex="9" Visible="false">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataComboBoxColumn Caption="نوع درخواست" FieldName="ReqType" Name="ReqType"
                VisibleIndex="9" Visible="false">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
                <PropertiesComboBox>
                    <Items>
                        <dxe:ListEditItem Text="صدور پروانه" Value="0" />
                        <dxe:ListEditItem Text="تمدید" Value="1" />
                        <dxe:ListEditItem Text="تغییرات" Value="2" />
                        <dxe:ListEditItem Text="المثنی" Value="3" />
                        <dxe:ListEditItem Text="ابطال" Value="4" />
                        <dxe:ListEditItem Text="صدور سیستم قدیم" Value="5" />
                        <dxe:ListEditItem Text="تغییرات اطلاعات پایه" Value="6" />
                    </Items>
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت عضویت" FieldName="ConfirmStatus" Name="ConfirmStatus"
                VisibleIndex="10" Width="100px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
        </Columns>
        <Settings ShowHorizontalScrollBar="true" ShowGroupPanel="True" ShowFilterRow="True" />
        <SettingsCustomizationWindow Enabled="True" PopupHorizontalAlign="Center" PopupVerticalAlign="Middle" />
        <SettingsPager Position="TopAndBottom">
        </SettingsPager>
    </TSPControls:CustomAspxDevGridView>
    <br />
    <dxp:ASPxPanel ID="PanelSelectedMeId" runat="server" ClientInstanceName="PanelSelectedMeId">
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent1" runat="server">
                <table width="100%">
                    <tr>
                        <td align="right">کدهای عضویت انتخاب شده :
                        </td>
                        <td align="left">
                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                cellpadding="0">
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="کپی"
                                            ID="btnCopy" EnableViewState="False" EnableTheming="False" AutoPostBack="false"
                                            UseSubmitBehavior="False">
                                            <Image Url="~/Images/icons/Copy2.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s,e){copyToClipboard(txtSelectedMeId.GetText());}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف انتخاب ها"
                                            ID="btnClear" EnableViewState="False" EnableTheming="False" AutoPostBack="false"
                                            UseSubmitBehavior="False">
                                            <Image Url="~/Images/icons/Clear-Form.png">
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
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SelectEngOfficeForSearch"
        TypeName="TSP.DataManager.EngOfficeManager"  OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="EngOfId" Type="Int32" />
            <asp:Parameter Name="MeId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="EOfTId" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="FromDate" Type="String" />
            <asp:Parameter DefaultValue="2" Name="ToDate" Type="String" />
            <asp:Parameter DefaultValue="%" Name="MeName" Type="String" />
            <asp:Parameter DefaultValue="%" Name="TellNo" Type="String" />
            <asp:Parameter DefaultValue="%" Name="EngOffName" Type="String" />
            <asp:Parameter DefaultValue="(1)" Name="IsConfirm" Type="String" />
            <asp:Parameter DefaultValue="1" Name="EndDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="EndDateTo" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="ReqType" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="FirstRegDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="FirstRegDateTo" Type="String" />
            <asp:Parameter DefaultValue="1" Name="LastRegDateFrom" Type="String" />
            <asp:Parameter DefaultValue="2" Name="LastRegDateTo" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbEngOfficeType" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.EngOfficeTypeManager" CacheDuration="600" CacheExpirationPolicy="Sliding"
        OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" ExportEmptyDetailGrid="True"
        GridViewID="CustomAspxDevGridView1" ExportedRowType="Selected">
    </dxwgv:ASPxGridViewExporter>
</asp:Content>
