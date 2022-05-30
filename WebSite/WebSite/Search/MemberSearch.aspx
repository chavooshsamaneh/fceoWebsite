<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    EnableViewStateMac="false" CodeFile="MemberSearch.aspx.cs" Inherits="Search_MemberSearch"
    Title="جستجوی اعضای حقیقی" %>

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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Src="~/UserControl/PrintEnvelopeDetailsUserControl.ascx" TagPrefix="TSP"
    TagName="EnvelopeDetails" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../Script/Utility.js">  
    </script>
    <script language="javascript" type="text/javascript">
        function MemberSearch(s, e) {
            e.processOnServer = false;
            if (ASPxClientEdit.ValidateGroup('Member') == false)
                return;
            if (CheckCreateDate() == -1) {
                lblErrorCreateDate.SetVisible(true);
                return;
            }
            else
                lblErrorCreateDate.SetVisible(false);
            if (CheckBirthDate() == -1) {
                lblErrorBirthDate.SetVisible(true);
                return;
            }
            else
                lblErrorBirthDate.SetVisible(false);
            if (CheckFileDate() == -1) {
                lblErrorFileDate.SetVisible(true);
                return;
            }
            else
                lblErrorFileDate.SetVisible(false);
            if (CheckMembershipDate() == -1) {
                lblErrortxtMembershipDateDate.SetVisible(true);
                return;
            }
            else
                lblErrortxtMembershipDateDate.SetVisible(false);
            //  grdMembers.PerformCallback('Search');
            if (CheckSearch() == 0) {
                alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.');
                return;
            }
            e.processOnServer = true;
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
            //if(chkMultiSelect.GetChecked()==true){
            if (grdMembers.GetSelectedRowCount() > 0) {
                txtSelectedMeId.SetText('در حال بارگذاری...');
                grdMembers.GetSelectedFieldValues('MeId', OnGetSelectedFieldValues);
            }
            else
                txtSelectedMeId.SetText('');
            // }
        }
        function OnGridEndCallback(s, e) {
            if (s.cpPrint == 1) {
                s.cpPrint = 0;
                window.open(s.cpURL);
            }
            _selectNumber = s.cpSelectedRowsOnPage;
            if (grdMembers.cpDoPrint == 1) {
                grdMembers.cpDoPrint = 0;
                window.open('../Print.aspx');
            }
        }
        function OnGetSelectedFieldValues(selectedValues) {
            if (selectedValues.length == 0) return;
            var MeId = '';
            for (i = 0; i < selectedValues.length; i++) {
                if (MeId != '') MeId += ';';
                MeId += selectedValues[i];
            }
            txtSelectedMeId.SetText(MeId);
        }

        /*******************************************************/
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
        /*************************************************************/
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <TSPControls:CustomASPxRoundPanelMenu ID="ASPxRoundPanel1" runat="server" Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table dir="rtl" align="right">
                    <tr>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEnvelopePrint" runat="server" AutoPostBack="False"
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ پاکت نامه"
                                UseSubmitBehavior="False">
                                <Image Url="~/Images/icons/printers2.png" />
                                <ClientSideEvents Click="function(s, e) {

if(txtSelectedMeId.GetText()=='')
{
alert('ردیفی انتخاب نشده است');
}
else
    popupChooseDetails.Show();		

}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint" runat="server" AutoPostBack="False"
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                <Image Url="../Images/icons/printers.png" />
                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	grdMembers.PerformCallback('Print');
}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrintMeDocReport" runat="server" AutoPostBack="False"
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ گزارش پروانه اشتغال شخص حقیقی به تفکیک رشته" UseSubmitBehavior="False">
                                <Image Url="../Images/icons/printorange.png" />
                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	grdMembers.PerformCallback('PrintMeDocReport');
}" />
                                <HoverStyle BackColor="#FFE0C0">
                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                </HoverStyle>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False"
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                UseSubmitBehavior="False" OnClick="btnExportExcel_Click">
                                <ClientSideEvents Click="function(s,e){ 
if(txtSelectedMeId.GetText()=='')
{
	alert('ردیفی انتخاب نشده است');
	e.processOnServer=false;
}
 }" />

                                <Image Url="~/Images/icons/ExportExcel.png" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton2" runat="server" AutoPostBack="False"
                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="انتخاب ستون ها"
                                UseSubmitBehavior="False">
                                <Image Url="~/Images/icons/cursor-hand.png" />
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
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSPControls:CustomASPxRoundPanel ID="SearchPanel" HeaderText="جستجو" runat="server" ShowCollapseButton="true">
        <PanelCollection>
            <dxp:PanelContent>

                <table width="100%">
                    <tr>
                        <td align="right" valign="top" colspan="4">
                            <ul class="HelpUL">
                                <li><b>رشته در عضویت</b> بیانگر رشته <u>پیش فرض</u> شخص در واحد عضویت می باشد. </li>
                                <li><b>مقطع تحصیلی</b> بر اساس مقطع مدرک تحصیلی<u> پیش فرض</u> شخص در عضویت می باشد.                                    
                                </li>
                                <li>
                                    <b>پایه پروانه</b> در کلیه صلاحیت های شخص جستجو می نماید.
                                </li>
                                <li>ستون "درخواست واحد پروانه" بیانگر وجود/عدم وجود درخواست درجریان در واحد پرانه می باشد و بیانگر دارا بودن/نبودن پروانه <b>نمی باشد</b></li>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" width="20%">کد عضویت از
                        </td>
                        <td align="right" valign="top" width="30%">
                            <TSPControls:CustomTextBox ID="txtMeIdFrom" runat="server"
                                ClientInstanceName="TextMeIdFrom">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearchMember);}" />
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Member">

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت نامعتبر است" />
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right" valign="top" width="20%">کد عضویت تا
                        </td>
                        <td align="right" valign="top" width="30%">
                            <TSPControls:CustomTextBox ID="txtMeIdTo" runat="server"
                                ClientInstanceName="TextMeIdTo">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearchMember);}" />
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Member">

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت نامعتبر است" />
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">نام
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomTextBox ID="txtFName" runat="server"
                                ClientInstanceName="TextFName">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearchMember);}" />
                                <ValidationSettings>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right" valign="top">نام خانوادگی
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomTextBox ID="txtLName" runat="server"
                                ClientInstanceName="TextLName">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearchMember);}" />
                                <ValidationSettings>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">تاریخ تولد از
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox  runat="server" DefaultDate="" ShowPickerOnTop="True"
                                ID="txtBirthDateFrom" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick" RightToLeft="False"
                                onkeypress="SearchKeyPress(event,2,btnSearchMember);"></pdc:PersianDateTextBox>
                            <dxe:ASPxLabel runat="server" Text="تاریخ شروع بایستی قبل از تاریخ پایان باشد"
                                ClientVisible="False" ID="lblErrorBirthDate" ForeColor="Red" ClientInstanceName="lblErrorBirthDate">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top">تاریخ تولد تا
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox  runat="server" DefaultDate="" ShowPickerOnTop="True"
                                ID="txtBirthDateTo" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick" RightToLeft="False"
                                onkeypress="SearchKeyPress(event,2,btnSearchMember);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">تاریخ ثبت نام از
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox  runat="server" DefaultDate="" ShowPickerOnTop="True"
                                ID="txtCreateDateFrom" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick" RightToLeft="False"
                                onkeypress="SearchKeyPress(event,2,btnSearchMember);"></pdc:PersianDateTextBox>
                            <dxe:ASPxLabel runat="server" Text="تاریخ شروع بایستی قبل از تاریخ پایان باشد"
                                ClientVisible="False" ID="lblErrorCreateDate" ForeColor="Red" ClientInstanceName="lblErrorCreateDate">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top">تاریخ ثبت نام تا
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox  runat="server" DefaultDate="" ShowPickerOnTop="True"
                                ID="txtCreateDateTo" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick" RightToLeft="False"
                                onkeypress="SearchKeyPress(event,2,btnSearchMember);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">تاریخ عضویت از
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox  runat="server" DefaultDate="" ShowPickerOnTop="True"
                                ID="txtMembershipDateFrom" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick" RightToLeft="False"
                                onkeypress="SearchKeyPress(event,2,btnSearchMember);"></pdc:PersianDateTextBox>
                            <dxe:ASPxLabel runat="server" Text="تاریخ شروع بایستی قبل از تاریخ پایان باشد"
                                ClientVisible="False" ID="lblErrortxtMembershipDateDate" ForeColor="Red" ClientInstanceName="lblErrortxtMembershipDateDate">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top">تاریخ عضویت تا
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox  runat="server" DefaultDate="" ShowPickerOnTop="True"
                                ID="txtMembershipDateTo" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick" RightToLeft="False"
                                onkeypress="SearchKeyPress(event,2,btnSearchMember);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">تاریخ صدور پروانه از
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox  ID="txtFirstDocRegDateFrom" runat="server" DefaultDate=""
                                IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" ShowPickerOnEvent="OnClick"
                                ShowPickerOnTop="True" Style="direction: ltr; text-align: right;"
                                RightToLeft="False" onkeypress="SearchKeyPress(event,2,btnSearchMember);"></pdc:PersianDateTextBox>
                            <dxe:ASPxLabel runat="server" Text="تاریخ شروع بایستی قبل از تاریخ پایان باشد"
                                ClientVisible="False" ID="lblErrorFirstDocRegDateFrom" ForeColor="Red" ClientInstanceName="lblErrorFirstDocRegDateFrom">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top">تاریخ صدور پروانه تا
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox  ID="txtFirstDocRegDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" ShowPickerOnEvent="OnClick" ShowPickerOnTop="True"
                                Style="direction: ltr; text-align: right;" RightToLeft="False"
                                onkeypress="SearchKeyPress(event,2,btnSearchMember);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">تاریخ تمدید پروانه از
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox  ID="txtRevivalDocRegDateFrom" runat="server" DefaultDate=""
                                IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" ShowPickerOnEvent="OnClick"
                                ShowPickerOnTop="True" Style="direction: ltr; text-align: right;"
                                RightToLeft="False" onkeypress="SearchKeyPress(event,2,btnSearchMember);"></pdc:PersianDateTextBox>
                            <dxe:ASPxLabel runat="server" Text="تاریخ شروع بایستی قبل از تاریخ پایان باشد"
                                ClientVisible="False" ID="lblErrorRevivalDocRegDateFrom" ForeColor="Red" ClientInstanceName="lblErrorRevivalDocRegDateFrom">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top">تاریخ تمدید پروانه تا
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox  ID="txtRevivalDocRegDateTo" runat="server" DefaultDate=""
                                IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" ShowPickerOnEvent="OnClick"
                                ShowPickerOnTop="True" Style="direction: ltr; text-align: right;"
                                RightToLeft="False" onkeypress="SearchKeyPress(event,2,btnSearchMember);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">تاریخ اعتبار پروانه از
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox  ID="txtFileDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" ShowPickerOnEvent="OnClick" ShowPickerOnTop="True"
                                Style="direction: ltr; text-align: right;" RightToLeft="False"
                                onkeypress="SearchKeyPress(event,2,btnSearchMember);"></pdc:PersianDateTextBox>
                            <dxe:ASPxLabel runat="server" Text="تاریخ شروع بایستی قبل از تاریخ پایان باشد"
                                ClientVisible="False" ID="lblErrorFileDate" ForeColor="Red" ClientInstanceName="lblErrorFileDate">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top">تاریخ اعتبار پروانه تا
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox  ID="txtFileDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" ShowPickerOnEvent="OnClick" ShowPickerOnTop="True"
                                Style="direction: ltr; text-align: right;" RightToLeft="False"
                                onkeypress="SearchKeyPress(event,2,btnSearchMember);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">رشته در عضویت
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomASPXDropDownEdit ID="drdMajor" RightToLeft="True" runat="server"
                                ClientInstanceName="drdMj">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearchMember);}" />
                                <ValidationSettings>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <DropDownWindowTemplate>
                                    <TSPControls:CustomASPxListBox RightToLeft="True" ID="ListBoxMajor" runat="server" DataSourceID="ObjectDataSource_MajorParents"
                                        SelectionMode="CheckColumn" TextField="MjName" ValueField="MjId"
                                        ClientInstanceName="ListMj">
                                        <ClientSideEvents SelectedIndexChanged="function(s,e){ OnListBoxSelectionChanged(s,drdMj,0,'',e); }" />
                                    </TSPControls:CustomASPxListBox>
                                </DropDownWindowTemplate>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </TSPControls:CustomASPXDropDownEdit>
                        </td>
                        <td align="right" valign="top">مقطع تحصیلی
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomASPXDropDownEdit ID="drdLicense" RightToLeft="True" runat="server"
                                ClientInstanceName="drdLicense">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearchMember);}" />
                                <ValidationSettings>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <DropDownWindowTemplate>
                                    <TSPControls:CustomASPxListBox RightToLeft="True" ID="ListBoxLicense" runat="server" DataSourceID="ObjectDataSourceLicense"
                                        SelectionMode="CheckColumn" TextField="LiName" ValueField="LiId"
                                        ClientInstanceName="ListBoxLicense">
                                        <ClientSideEvents SelectedIndexChanged="function(s,e){ OnListBoxSelectionChanged(s,drdLicense,0,'',e); }" />
                                    </TSPControls:CustomASPxListBox>
                                </DropDownWindowTemplate>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </TSPControls:CustomASPXDropDownEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">وضعیت عضویت
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomASPXDropDownEdit ID="drdRegistrationStatus" RightToLeft="True" runat="server"
                                ClientInstanceName="drdRegistrationStatus">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearchMember);}" />
                                <ValidationSettings>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <DropDownWindowTemplate>
                                    <TSPControls:CustomASPxListBox RightToLeft="True" ID="ListBoxRegistrationStatus" runat="server"
                                        DataSourceID="ObjectDataSourceRegistrationStatus" SelectionMode="CheckColumn"
                                        TextField="MrsName" ValueField="MrsId" ClientInstanceName="ListBoxRegistrationStatus">
                                        <ClientSideEvents SelectedIndexChanged="function(s,e){ OnListBoxSelectionChanged(s,drdRegistrationStatus,0,'',e); }" />
                                    </TSPControls:CustomASPxListBox>
                                </DropDownWindowTemplate>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </TSPControls:CustomASPXDropDownEdit>
                        </td>
                        <td align="right" valign="top">وضعیت مدارک تحصیلی
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String"
                                ID="cmbLicenseInquiryStatus"
                                RightToLeft="True" ClientInstanceName="cmbLicenseInquiryStatus">
                                <Items>
                                    <dxe:ListEditItem Text="همه" Value="-1" />
                                    <dxe:ListEditItem Text="استعلام نشده" Value="0" />
                                    <dxe:ListEditItem Text="استعلام شده" Value="1" />
                                </Items>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearchMember);}" />
                            </TSPControls:CustomAspxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <TSPControls:CustomAspxCallbackPanel runat="server"
                                ClientInstanceName="CallbackPanelParvane" Width="100%" ID="CallbackPanelParvane"
                                OnCallback="CallbackPanelParvane_Callback">
                                <Paddings Padding="0" />
                                <ClientSideEvents EndCallback="function(s,e){ OnListBoxSelectionChanged(ListBoxFileMjName,drdFileMjName,0,'',e); }" />
                                <PanelCollection>
                                    <dxp:PanelContent ID="PanelContent3" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td align="right" valign="top" width="20%">رشته پروانه
                                                </td>
                                                <td align="right" valign="top" width="30%">
                                                    <TSPControls:CustomAspxComboBox ID="drdParentFileMajor" runat="server"
                                                        ClientInstanceName="drdParentFileMajor"
                                                        DataSourceID="ObjectDataSource_MajorParents" TextField="MjName" ValueField="MjId">
                                                        <ClientSideEvents SelectedIndexChanged="function(s,e){ 
                                                                            CallbackPanelParvane.PerformCallback('cmbChange'+';'+ drdParentFileMajor.GetValue());} " />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </TSPControls:CustomAspxComboBox>
                                                </td>
                                                <td align="right" valign="top" width="20%">رشته موضوع پروانه
                                                </td>
                                                <td align="right" valign="top" width="30%">
                                                    <TSPControls:CustomASPXDropDownEdit RightToLeft="True" ID="drdFileMjName" runat="server"
                                                        ClientInstanceName="drdFileMjName">
                                                        <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearchMember);}" />
                                                        <ValidationSettings>

                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <DropDownWindowTemplate>
                                                            <TSPControls:CustomASPxListBox RightToLeft="True" ID="ListBoxFileMjName" runat="server" Width="400"
                                                                SelectionMode="CheckColumn" TextField="MjName" ValueField="MjId" ClientInstanceName="ListBoxFileMjName">
                                                                <ClientSideEvents SelectedIndexChanged="function(s,e){ OnListBoxSelectionChanged(s,drdFileMjName,0,'',e); }" />
                                                            </TSPControls:CustomASPxListBox>
                                                        </DropDownWindowTemplate>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                    </TSPControls:CustomASPXDropDownEdit>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </TSPControls:CustomAspxCallbackPanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">پایه پروانه
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomASPXDropDownEdit RightToLeft="True" ID="drdDocGrade" runat="server"
                                ClientInstanceName="drdDocGrade">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearchMember);}" />
                                <ValidationSettings>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <DropDownWindowTemplate>
                                    <TSPControls:CustomASPxListBox RightToLeft="True" ID="ListBoxDocGrade" runat="server" DataSourceID="ObjectDataSourceDocGrade"
                                        SelectionMode="CheckColumn" TextField="GrdName" ValueField="GrdId"
                                        ClientInstanceName="ListBoxDocGrade">
                                        <ClientSideEvents SelectedIndexChanged="function(s,e){ OnListBoxSelectionChanged(s,drdDocGrade,1,'0',e); }" />
                                    </TSPControls:CustomASPxListBox>
                                </DropDownWindowTemplate>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </TSPControls:CustomASPXDropDownEdit>
                        </td>
                        <td align="right" valign="top">گروه
                        </td>
                        <td align="right" valign="top">

                              <TSPControls:CustomAspxComboBox ID="drdGroup" runat="server"
                                                        ClientInstanceName="drdGr"
                                                        DataSourceID="ObjectDataSourceGroup" TextField="GrName" ValueField="GrId">
                                                    
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </TSPControls:CustomAspxComboBox>


                         <%--   <TSPControls:CustomASPXDropDownEdit RightToLeft="True" ID="drdGroup" runat="server"
                                ClientInstanceName="drdGr">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearchMember);}" />
                                <ValidationSettings>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <DropDownWindowTemplate>
                                    <TSPControls:CustomASPxListBox RightToLeft="True" ID="ListBoxGroup" runat="server" DataSourceID="ObjectDataSourceGroup"
                                        SelectionMode="CheckColumn" TextField="GrName" ValueField="GrId"
                                        ClientInstanceName="ListGr">
                                        <ClientSideEvents SelectedIndexChanged="function(s,e){ OnListBoxSelectionChanged(s,drdGr,0,'',e); }" />
                                    </TSPControls:CustomASPxListBox>
                                </DropDownWindowTemplate>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </TSPControls:CustomASPXDropDownEdit>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">کمیسیون های همکاری
                        </td>
                        <td align="right" valign="top">

                              <TSPControls:CustomAspxComboBox ID="drdCommision" runat="server"
                                                        ClientInstanceName="drdCom"
                                                        DataSourceID="ObjectDataSourceCommision" TextField="ComName" ValueField="ComId">
                                                    
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </TSPControls:CustomAspxComboBox>
                          <%--  <TSPControls:CustomASPXDropDownEdit RightToLeft="True" ID="drdCommision" runat="server"
                                ClientInstanceName="drdCom">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearchMember);}" />
                                <ValidationSettings>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <DropDownWindowTemplate>
                                    <TSPControls:CustomASPxListBox RightToLeft="True" ID="ListBoxCom" runat="server" DataSourceID="ObjectDataSourceCommision"
                                        SelectionMode="CheckColumn" TextField="ComName" ValueField="ComId"
                                        ClientInstanceName="ListCom">
                                        <ClientSideEvents SelectedIndexChanged="function(s,e){ OnListBoxSelectionChanged(s,drdCom,0,'',e); }" />
                                    </TSPControls:CustomASPxListBox>
                                </DropDownWindowTemplate>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </TSPControls:CustomASPXDropDownEdit>--%>
                        </td>
                        <td align="right" valign="top">دفتر نمایندگی
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomASPXDropDownEdit RightToLeft="True" ID="drdAgent" runat="server"
                                ClientInstanceName="drdAgent">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearchMember);}" />
                                <ValidationSettings>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <DropDownWindowTemplate>
                                    <TSPControls:CustomASPxListBox RightToLeft="True" ID="ListBoxAgent" runat="server" DataSourceID="ObjectDataSourceAgent"
                                        SelectionMode="CheckColumn" TextField="Name" ValueField="AgentId"
                                        ClientInstanceName="ListBoxAgent">
                                        <ClientSideEvents SelectedIndexChanged="function(s,e){ OnListBoxSelectionChanged(s,drdAgent,0,'',e); }" />
                                    </TSPControls:CustomASPxListBox>
                                </DropDownWindowTemplate>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </TSPControls:CustomASPXDropDownEdit>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <TSPControls:CustomAspxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" CausesValidation="False"
                                Text="&nbsp;جستجو"
                                Width="130px" ID="btnSearchMember" ClientInstanceName="btnSearchMember" OnClick="btnSearchMember_Click">

                                <ClientSideEvents Click="MemberSearch" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td colspan="2">
                            <TSPControls:CustomAspxButton runat="server" Text="&nbsp;پاک کردن فرم" CausesValidation="False"
                                ID="btnMeRefresh" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                AutoPostBack="False"
                                Width="130px">

                                <ClientSideEvents Click="function(s, e) {
 e.processOnServer= false;
 if(confirm('آیا مطمئن به پاک کردن اطلاعات فرم هستید؟'))
	SetEmpty();
}" />
                            </TSPControls:CustomAspxButton>
                        </td>

                    </tr>

                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <TSPControls:CustomAspxDevGridView runat="server" Font-Size="7.5pt" Width="100%"
        ID="grdMembers" DataSourceID="ObjectDataSourceGrid" ClientInstanceName="grdMembers"
        KeyFieldName="MeId" AutoGenerateColumns="False" OnCustomJSProperties="grdMembers_CustomJSProperties"
        OnCustomCallback="grdMembers_CustomCallback" OnHtmlRowPrepared="grdMembers_HtmlRowPrepared"
        OnHtmlDataCellPrepared="grdMembers_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="grdMembers_AutoFilterCellEditorInitialize"
        RightToLeft="True">
        <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowFilterRowMenu="True"></Settings>
        <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True" ColumnResizeMode="NextColumn"></SettingsBehavior>
        <ClientSideEvents SelectionChanged="OnGridSelectionChanged" EndCallback="OnGridEndCallback"
            RowDblClick="function(s,e){                
                s.SelectRowOnPage(e.visibleIndex);}" />
        <Columns>
            <dxwgv:GridViewCommandColumn Name="SelectMember" ShowSelectCheckbox="True" VisibleIndex="1"
                Width="50px" AllowDragDrop="False">
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
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="40" Width="30px" AllowDragDrop="False" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="1"
                Width="100px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد عضویت موقت" FieldName="TMeId" VisibleIndex="1"
                Width="100px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="2"
                Width="100px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2"
                Width="100px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام پدر" FieldName="FatherName" VisibleIndex="3"
                Width="100px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره شناسنامه" FieldName="IdNo" VisibleIndex="4"
                Width="100px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="رشته پیش فرض عضویت" FieldName="LastMjName" VisibleIndex="5">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="عنوان گروه رشته" FieldName="MajorParentName" VisibleIndex="5">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewDataTextColumn Caption="کدملی" FieldName="SSN" Visible="False" VisibleIndex="7">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="عنوان" FieldName="TiName" Visible="False"
                VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="جنسیت" FieldName="SexName" Visible="False"
                VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت تأهل" FieldName="MarName" Visible="False"
                VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ تولد" FieldName="BirhtDate" Visible="False"
                VisibleIndex="6">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="محل تولد" FieldName="BirthPlace" Visible="False"
                VisibleIndex="6">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره همراه" FieldName="MobileNo" Visible="False"
                VisibleIndex="6">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پست الکترونیکی" FieldName="Email" Visible="False"
                VisibleIndex="6">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="مقطع تحصیلی" FieldName="LastLiName" Visible="False"
                VisibleIndex="6">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت سربازی" FieldName="SoName" Visible="False"
                VisibleIndex="7">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شهر اقامت" FieldName="CitName" Visible="False"
                VisibleIndex="7">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره حساب" FieldName="BankAccNo" Visible="False"
                VisibleIndex="7">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نمایندگی" FieldName="AgentName" Visible="False"
                VisibleIndex="7">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره عضویت" FieldName="MeNo" Visible="False"
                VisibleIndex="9">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ ثبت نام" FieldName="CreateDate" Visible="False"
                VisibleIndex="6" Width="80px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ عضویت" FieldName="MembershipDate" Visible="False"
                VisibleIndex="6" Width="80px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت عضویت" FieldName="MrsName" Visible="False"
                VisibleIndex="6">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="وضعیت انتقال" FieldName="TransferTypeName" VisibleIndex="6"
                Width="15%" Visible="false">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="درخواست واحد پروانه" FieldName="MeDocStatus" Visible="False"
                VisibleIndex="8">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره پروانه" FieldName="FileNo" Visible="False"
                VisibleIndex="8">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ اعتبار پروانه" FieldName="FileDate"
                Visible="False" VisibleIndex="8" Width="80px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ صدور پروانه" FieldName="FirstDocRegDate"
                Visible="False" VisibleIndex="8" Width="80px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ تمدید پروانه" FieldName="RevivalDocRegDate"
                Visible="False" VisibleIndex="8" Width="80px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="بالاترین پایه" FieldName="MaxGradeName" Visible="False"
                VisibleIndex="8">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه طراحی" FieldName="DesGrdName" Visible="False"
                VisibleIndex="8">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه نظارت" FieldName="ObsGrdName" Visible="False"
                VisibleIndex="8">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه اجرا" FieldName="ImpGrdName" Visible="False"
                VisibleIndex="8">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="رشته پروانه" FieldName="FileMjName" Visible="False"
                VisibleIndex="8">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه شهرسازی" FieldName="UrbanismGrade" VisibleIndex="8"
                Width="50px" Visible="false">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه نقشه برداری" FieldName="MappingGrade"
                VisibleIndex="8" Width="50px" Visible="false">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایه ترافیک" FieldName="TrafficGrade" VisibleIndex="8"
                Width="50px" Visible="false">
            </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="پایه گاز" FieldName="GasGrade" VisibleIndex="8"
                Width="50px" Visible="false">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataImageColumn Caption="تصویر" FieldName="ImageUrl" Visible="False"
                VisibleIndex="6">
                <PropertiesImage ImageHeight="100px" ImageWidth="100px">
                </PropertiesImage>
            </dxwgv:GridViewDataImageColumn>
            <dxwgv:GridViewDataTextColumn Caption="گروه رشته" FieldName="MjMaster" Visible="False"
                VisibleIndex="6">
            </dxwgv:GridViewDataTextColumn>
         <%--   <dxwgv:GridViewDataTextColumn Caption="کمیسیون های همکاری" FieldName="MeCommissions"
                Visible="False" VisibleIndex="6">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>--%>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ فارغ التحصیلی" FieldName="LastLiEndDate"
                Visible="False" VisibleIndex="6">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="آدرس محل سکونت" FieldName="HomeAdr" Name="HomeAdr"
                Visible="False" VisibleIndex="6">
                <CellStyle Wrap="true">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تلفن محل سکونت" FieldName="HomeTel" Name="HomeTel"
                Visible="False" VisibleIndex="6">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کدپستی محل سکونت" FieldName="HomePO" Name="HomePO"
                Visible="False" VisibleIndex="6">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="آدرس محل کار" FieldName="WorkAdr" Name="WorkAdr"
                Visible="False" VisibleIndex="6">
                <CellStyle Wrap="true">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تلفن محل کار" FieldName="WorkTel" Name="WorkTel"
                Visible="False" VisibleIndex="6">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کدپستی محل کار" FieldName="WorkPO" Name="WorkPO"
                Visible="False" VisibleIndex="6">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="دانشگاه محل تحصیل" FieldName="LastUnName"
                Name="LastUnName" Visible="False" VisibleIndex="6">
                <CellStyle Wrap="true">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataComboBoxColumn Caption="نحوه ارائه اطلاعات" FieldName="UserInfoType"
                VisibleIndex="6" Width="100px">
                <PropertiesComboBox TextField="UserInfoTypeName" ValueField="UserInfoType" ValueType="System.String">
                    <Items>
                        <dxe:ListEditItem Text="انتخاب نشده" Value="0"></dxe:ListEditItem>
                        <dxe:ListEditItem Text="آدرس و شماره همراه" Value="1"></dxe:ListEditItem>
                        <dxe:ListEditItem Text="شماره همراه" Value="2"></dxe:ListEditItem>
                        <dxe:ListEditItem Text="آدرس" Value="3"></dxe:ListEditItem>
                        <dxe:ListEditItem Text="مخالف با ارائه اطلاعات" Value="4"></dxe:ListEditItem>
                    </Items>
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataComboBoxColumn Caption="دریافت فصل نامه" FieldName="RecieveMagazine"
                VisibleIndex="6" Width="100px">
                <PropertiesComboBox TextField="RecieveMagazineName" ValueField="RecieveMagazine"
                    ValueType="System.String">
                    <Items>
                        <dxe:ListEditItem Text="انتخاب نشده" Value="0"></dxe:ListEditItem>
                        <dxe:ListEditItem Text="بلی" Value="1"></dxe:ListEditItem>
                        <dxe:ListEditItem Text="خیر" Value="2"></dxe:ListEditItem>
                    </Items>
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
        </Columns>
        <SettingsPager Position="TopAndBottom">
        </SettingsPager>
        <SettingsCustomizationWindow Enabled="True" PopupHorizontalAlign="Center" PopupVerticalAlign="Middle" />
        <SettingsCookies CookiesID="MeSearchCookieId" Enabled="True" StoreFiltering="False"
            StorePaging="False" />
    </TSPControls:CustomAspxDevGridView>
    <br />
    <dxp:ASPxPanel ID="PanelSelectedMeId" runat="server" ClientInstanceName="PanelSelectedMeId">
        <PanelCollection>
            <dxp:PanelContent runat="server">
           
                    <div >کدهای عضویت انتخاب شده :</div>
                 <div style="float: left">
                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="کپی"
                            ID="btnCopy" EnableViewState="False" EnableTheming="False" AutoPostBack="False"
                            UseSubmitBehavior="False">
                            <Image Url="~/Images/icons/Copy2.png">
                            </Image>
                            <ClientSideEvents Click="function(s,e){copyToClipboard(txtSelectedMeId.GetText());}" />
                        </TSPControls:CustomAspxButton>
                    </div>
                    <div  style="float:left">
                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف انتخاب ها"
                            ID="btnClear" EnableViewState="False" EnableTheming="False" AutoPostBack="False"
                            UseSubmitBehavior="False">
                            <Image Url="~/Images/icons/Clear-Form.png">
                            </Image>
                            <ClientSideEvents Click="function(s,e){grdMembers.UnselectRows();}" />
                        </TSPControls:CustomAspxButton>
                    </div>
                <TSPControls:CustomASPXMemo ID="txtSelectedMeId" runat="server"
                    Height="80px" Width="100%" ReadOnly="True" ClientInstanceName="txtSelectedMeId">
                    <ValidationSettings>

                        <ErrorFrameStyle ImageSpacing="4px">
                            <ErrorTextPaddings PaddingLeft="4px" />
                        </ErrorFrameStyle>
                    </ValidationSettings>
                </TSPControls:CustomASPXMemo>
             
                     
             
            </dxp:PanelContent>
        </PanelCollection>
    </dxp:ASPxPanel>
   <%-- <TSP:EnvelopeDetails ID="envDetails" runat="server" ClientInstanceName="envDetails"
        CallbackName="grdMembers" CallbackParam="EnvelopePrint" />--%>


      <TSPControls:CustomASPxPopupControl ID="popupChooseDetails" runat="server"
        ShowPageScrollbarWhenModal="True"
        AutoUpdatePosition="True" ClientInstanceName="popupChooseDetails" PopupVerticalAlign="WindowCenter"
        PopupHorizontalAlign="WindowCenter" Modal="True" CloseAction="CloseButton" AllowDragging="True"
        HeaderText="انتخاب جزئیات">
 <ClientSideEvents Closing="function(s,e){grdMembers.PerformCallback('EnvelopePrint');}" />
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <table style="width: 300px; text-align: right;">
                    <tr>
                        <td valign="top" style="width: 89px">
                            <dxe:ASPxLabel runat="server" Text="انتخاب آدرس" ID="ASPxLabel1">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top">
                            <TSPControls:CustomAspxComboBox runat="server" Width="180px"
                                ID="comboAddress"
                                ClientInstanceName="comboAddress"
                                EnableIncrementalFiltering="True" HorizontalAlign="Right" SelectedIndex="0" ValueType="System.String">
                                <Items>
                                    <dxe:ListEditItem Text="آدرس محل سکونت" Value="0" Selected="True" />
                                    <dxe:ListEditItem Text="آدرس محل کار" Value="1" />
                                </Items>
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="LetterInputs">
                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 89px">
                            <dxe:ASPxLabel runat="server" Text="انتخاب نوع چاپ" ID="ASPxLabel2">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top">
                            <TSPControls:CustomAspxComboBox runat="server" Width="180px"
                                ID="comboPrintType"
                                ClientInstanceName="comboPrintType"
                                EnableIncrementalFiltering="True" HorizontalAlign="Right" SelectedIndex="0" ValueType="System.String">
                                <Items>
                                    <dxe:ListEditItem Text="چاپ پشت سر هم" Value="0" Selected="True" />
                                    <dxe:ListEditItem Text="چاپ صفحه به صفحه" Value="1" />
                                </Items>
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="LetterInputs">

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <dxe:ASPxLabel runat="server" Text="دبیرخانه" ID="ASPxLabel3">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <TSPControls:CustomAspxComboBox runat="server" Width="180px"
                                ID="comboSecretariat" SelectedIndex="0"
                                ClientInstanceName="comboSecretariat"
                                EnableIncrementalFiltering="True" HorizontalAlign="Right" ValueType="System.Int32" DataSourceID="ObjectDataSourceSecretariat" TextField="SName" ValueField="SId">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="LetterInputs">

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomAspxComboBox>
                            <asp:ObjectDataSource ID="ObjectDataSourceSecretariat" runat="server" SelectMethod="SelectAutomationSecretariatByEmId"
                                TypeName="TSP.DataManager.Automation.SecretariatManager">
                                <SelectParameters>
                                    <asp:Parameter Name="EmpId" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <br />
                            <TSPControls:CustomAspxButton runat="server" Text="تایید" ToolTip="تایید"
                                CausesValidation="False" ID="btnClosePopupChooseSender" AutoPostBack="False"
                                UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnClosePopup">
                                <ClientSideEvents Click="function(s,e){popupChooseDetails.Hide();}"></ClientSideEvents>
                                <Image Height="10px" Width="10px" Url="~/Images/icons/Check.png"></Image>
                            </TSPControls:CustomAspxButton>
                        </td>

                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>

    </TSPControls:CustomASPxPopupControl>
    <asp:ObjectDataSource ID="ObjectDataSource_MajorParents" runat="server" SelectMethod="FindMjParents"
        TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceLicense" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.LicenceManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceRegistrationStatus" runat="server" SelectMethod="GetData" FilterExpression="MrsId=1 or MrsId=2 or MrsId=3 or MrsId=4 or MrsId=5 or MrsId=7 or MrsId=10 or MrsId=8"
        TypeName="TSP.DataManager.MembershipRegistrationStatusManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceAgent" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.AccountingAgentManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceGroup" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.GroupManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceDocGrade" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceGrid" runat="server" SelectMethod="SelectMemberForSearchByExec"
        TypeName="TSP.DataManager.MemberManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="MeIdFrom" Type="Int32" DefaultValue="-2"  />
            <asp:Parameter Name="MeIdTo" Type="Int32" DefaultValue="-1"  />
            <asp:Parameter Name="MjParam" Type="String"  DefaultValue="" />
            <asp:Parameter DefaultValue="%" Name="FirstName" Type="String" />
            <asp:Parameter DefaultValue="%" Name="LastName" Type="String" />
            <asp:Parameter Name="GrParam" Type="Int32"  DefaultValue="-1" />
            <asp:Parameter Name="ComId" Type="Int32" DefaultValue="-1" />
            <asp:Parameter Name="CreateDateFrom" Type="String"  DefaultValue="1" />
            <asp:Parameter Name="CreateDateTo" Type="String"  DefaultValue="2" />
            <asp:Parameter Name="FileDateFrom" Type="String" DefaultValue="1"  />
            <asp:Parameter Name="FileDateTo" Type="String"  DefaultValue="2" />
            <asp:Parameter Name="MembershipDateFrom" Type="String"  DefaultValue="1" />
            <asp:Parameter Name="MembershipDateTo" Type="String"  DefaultValue="2" />
            <asp:Parameter Name="BirthDateFrom" Type="String"  DefaultValue="1" />
            <asp:Parameter Name="BirthDateTo" Type="String" DefaultValue="2"  />
            <asp:Parameter Name="LicenseParam" Type="String"  DefaultValue="0" />
            <asp:Parameter Name="RegistrationStatusParam" Type="String"  DefaultValue="0" />
            <asp:Parameter Name="AgentParam" Type="String"  DefaultValue="0" />
            <asp:Parameter Name="DocGradeParam" Type="String"  DefaultValue="-1" />
            <asp:Parameter Name="FirstDocRegDateFrom" Type="String"  DefaultValue="1" />
            <asp:Parameter Name="FirstDocRegDateTo" Type="String"  DefaultValue="2" />
            <asp:Parameter Name="RevivalDocRegDateFrom" Type="String"  DefaultValue="1" />
            <asp:Parameter Name="RevivalDocRegDateTo" Type="String"  DefaultValue="2" />
            <asp:Parameter Name="FileMjIdParam" Type="String"  DefaultValue="0" />
            <asp:Parameter Name="FileMjParentIdParam" Type="String" DefaultValue="0" />
            <asp:Parameter Name="LicenseInquiryStatus" Type="Int32" DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceCommision" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.CommissionManager"></asp:ObjectDataSource>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" ExportEmptyDetailGrid="True"
        GridViewID="grdMembers" ExportedRowType="Selected">
    </dxwgv:ASPxGridViewExporter>
</asp:Content>
