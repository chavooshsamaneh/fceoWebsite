<%@ Page Title="مشخصات نظرسنجی" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="PollInsert.aspx.cs" Inherits="Employee_Poll_PollInsert" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
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
            UpdateText(listBox, DropDown, indexAll, ItemsDifferentFromOther);
        }
        function UpdateSelectAllItemState(listBox, indexAll, ItemsDifferentFromOther) {
            IsAllSelected(listBox, indexAll, ItemsDifferentFromOther) ? listBox.SelectIndices([indexAll]) : listBox.UnselectIndices([indexAll]);
        }
        function IsAllSelected(listBox, indexAll, ItemsDifferentFromOther) {
            for (var i = 0; i < listBox.GetItemCount(); i++) {
                if (i != indexAll) {
                    if (CheckIndexIsInItemsDifferentFromOther(ItemsDifferentFromOther, i) == false)
                        if (!listBox.GetItem(i).selected)
                            return false;
                }
            }
            return true;
        }
        function UpdateText(listBox, DropDown, indexAll, ItemsDifferentFromOther) {
            var selectedItems = listBox.GetSelectedItems();
            IsAllSelected(listBox, indexAll, ItemsDifferentFromOther) ? cmbAgent.SetText('<همه>') : DropDown.SetText(GetSelectedItemsText(selectedItems, indexAll));
        }
        function GetSelectedItemsText(items, indexAll) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                if (items[i].index != indexAll)
                    texts.push(items[i].text);
            return texts.join(',');
        }
        function ChangeSelectionItem(listBox, ItemsDifferentFromOther, SelectionStatus) {
            for (var i = 0; i < listBox.GetItemCount(); i++)
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
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
                CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
                FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
            </pdc:PersianDateScriptManager>
            <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]</div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent1" runat="server">
                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                        ID="btnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        CausesValidation="False" AutoPostBack="True" OnClick="btnNew_Click">
                                  
                                        <Image  Url="~/Images/icons/new.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                         ID="btnEdit" AutoPostBack="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" CausesValidation="False" OnClick="btnEdit_Click">
                                        
                                        <Image  Url="~/Images/icons/edit.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server"  EnableTheming="False"
                                            EnableViewState="False" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False" OnClick="btnSave_Click">
                                           
                                            <Image  Url="~/Images/icons/save.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server"  EnableTheming="False"
                                        EnableViewState="False" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False" OnClick="btnBack_Click"
                                        CausesValidation="false">
                                       
                                        <Image  Url="~/Images/icons/back.png"  />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomAspxMenuHorizontal ID="MenuDetail" runat="server"  
                OnItemClick="MenuDetail_ItemClick">
                <Items>
                    <dxm:MenuItem Text="اطلاعات پایه" Name="BaseInfo" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="سوالات" Name="Questions">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>
            <ul class="HelpUL">
                در صورتی که سوالی برای نظرسنجی تعریف نشود، نظرسنجی به کاربران جهت پاسخ دهی نمایش
                داده نخواهد شد.
            </ul>
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelMain" ClientInstanceName="RoundPanelMain"
                HeaderText="مشاهده" runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent>
                        <table width="100%">
                            <tr>
                                <td width="15%" valign="top" align="right">
                                    عنوان نظرسنجی
                                </td>
                                <td width="85%" valign="top" align="right" colspan="3">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtTittle"  Width="100%" >
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            
                                            <RequiredField IsRequired="true" ErrorText="عنوان را وارد نمایید"></RequiredField>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%" valign="top" align="right">
                                    تاریخ شروع نمایش
                                </td>
                                <td width="35%" valign="top" align="right">
                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="225px" ShowPickerOnTop="True"
                                        ID="txtStartDate" PickerDirection="ToRight" ShowPickerOnEvent="OnClick" IconUrl="~/Image/Calendar.gif"
                                        Style="direction: ltr"></pdc:PersianDateTextBox>
                                    <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                        ErrorMessage="تاریخ را انتخاب نمایید" ControlToValidate="txtStartDate" ID="DateValidatorStart">تاریخ را انتخاب نمایید</pdc:PersianDateValidator>
                                </td>
                                <td width="15%" valign="top" align="right">
                                    تاریخ اعتبار
                                </td>
                                <td width="35%" valign="top" align="right">
                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="225px" ShowPickerOnTop="True"
                                        ID="txtValidatDate" PickerDirection="ToRight" ShowPickerOnEvent="OnClick" IconUrl="~/Image/Calendar.gif"
                                        Style="direction: ltr"></pdc:PersianDateTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    سطح دسترسی نتایج
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%" 
                                        RightToLeft="True"  ID="cmbIsResultPublic"
                                         ValueType="System.String" 
                                        EnableIncrementalFiltering="True">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            
                                            <RequiredField ErrorText="سطح دسترسی نمایش را انتخاب نمایید" IsRequired="true" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <Items>
                                            <dxe:ListEditItem Text="کارمندان" Value="0" Selected="true" />
                                            <dxe:ListEditItem Text="کارمندان-اعضا" Value="1" />
                                            <dxe:ListEditItem Text="کارمندان-اعضا-کاربر مهمان" Value="2" />
                                        </Items>
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                    </TSPControls:CustomAspxComboBox>
                                </td>
                                <td valign="top" align="right">
                                    نوع نظر سنجی
                                </td>
                                <td valign="top" align="right">
                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%" 
                                        RightToLeft="True"  ID="cmbType" 
                                        ValueType="System.String"  EnableIncrementalFiltering="True">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                          
                                            <RequiredField ErrorText="تعداد سوالات نظر سنجی را تعیین کنید" IsRequired="true" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <Items>
                                            <dxe:ListEditItem Text="تک سوالی" Value="0" Selected="true" />
                                            <dxe:ListEditItem Text="چند سوالی" Value="1" />
                                        </Items>
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                    </TSPControls:CustomAspxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    دفتر نمایندگی
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomASPXDropDownEdit RightToLeft="True" ID="cmbAgent" runat="server" 
                                         Width="100%"  ClientInstanceName="cmbAgent">
                                        <DropDownWindowTemplate>
                                            <TSPControls:CustomASPxListBox RightToLeft="True" ID="ListBoxAgent" runat="server" DataSourceID="ObjectDataSourceAgent"
                                                SelectionMode="CheckColumn"  TextField="Name" ValueField="AgentId"
                                                ClientInstanceName="ListBoxAgent">
                                                <ClientSideEvents SelectedIndexChanged="function(s,e){ OnListBoxSelectionChanged(s,cmbAgent,0,'',e); }" />
                                            </TSPControls:CustomASPxListBox>
                                        </DropDownWindowTemplate>
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                    </TSPControls:CustomASPXDropDownEdit>
                                    <asp:ObjectDataSource ID="ObjectDataSourceAgent" runat="server" SelectMethod="GetData"
                                        TypeName="TSP.DataManager.AccountingAgentManager"></asp:ObjectDataSource>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    محل نمایش نظرسنجی
                                </td>
                                <td colspan="4">
                                    <TSPControls:CustomASPxCheckBoxList ID="chkDisplayLocations" runat="server"  
                                         DataSourceID="ObjectDataSourceDisplayLocationTypes" ValueField="TypeId"
                                        ClientInstanceName="chkDisplayLocations" RepeatColumns="2" RightToLeft="True"
                                        TextField="TypeName"  Width="100%">
                                    </TSPControls:CustomASPxCheckBoxList>
                                    <asp:ObjectDataSource ID="ObjectDataSourceDisplayLocationTypes" runat="server" SelectMethod="SelectDisplayLocationForPoll"
                                        TypeName="TSP.DataManager.FormBuilder.DisplayLocationTypesManager"></asp:ObjectDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    <asp:Label runat="server" Text="تصویر" ID="Label9"></asp:Label>
                                </td>
                                <td valign="top" align="right">
                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td dir="rtl" style="width: 80%">
                                                    <TSPControls:CustomAspxUploadControl runat="server" ID="flpImage" InputType="Images"
                                                        ClientInstanceName="flpImage" OnFileUploadComplete="flpImage_FileUploadComplete"
                                                        MaxSizeForUploadFile="3000000" UploadWhenFileChoosed="true" Width="100%">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
if(e.isValid){
imgEndImageAttachment.SetVisible(true);
	AttachmentImage.SetImageUrl('../../Image/Temp/'+e.callbackData);
}
else {
 imgEndImageAttachment.SetVisible(false); 
	AttachmentImage.SetImageUrl('');
 }                                     
}"></ClientSideEvents>
                                                    </TSPControls:CustomAspxUploadControl>
                                                </td>
                                                <td style="width: 20%">
                                                    <dxe:ASPxImage runat="server" ID="imgEndImageAttachment" ToolTip="تصویر انتخاب شد" ClientVisible="False"
                                                        ImageUrl="~/Images/icons/button_ok.png" ClientInstanceName="imgEndImageAttachment">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top" colspan="2">
                                                    <asp:Label runat="server" Text="اندازه تصویر انتخابی بایستی 364*1140 باشد" ID="Label1"
                                                        ForeColor="Red" Width="100%"></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxe:ASPxImage runat="server" ID="AttachmentImage" ClientInstanceName="AttachmentImage"
                                        ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/noImage.gif" Height="160px" Width="225px">
                                        <EmptyImage Height="160px" Width="225px" Url="~/Images/noImage.gif">
                                        </EmptyImage>
                                    </dxe:ASPxImage>
                                </td>
                                <td valign="top" align="right">
                                </td>
                            </tr>
                            <td valign="top" align="right">
                                <asp:Label runat="server" Text="فایل" ID="Label2"></asp:Label>
                            </td>
                            <td valign="top" align="right">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td dir="rtl">
                                                <TSPControls:CustomAspxUploadControl runat="server" ID="flpcAttachment" InputType="Files"
                                                    ClientInstanceName="flpcAttachment" OnFileUploadComplete="flpcAttachment_FileUploadComplete"
                                                    MaxSizeForUploadFile="3000000" UploadWhenFileChoosed="true" Width="100%">
                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
if(e.isValid){
imgEndAttachment.SetVisible(true);
	HyperLinkAttachment.SetNavigateUrl('../../Image/Temp/'+e.callbackData);
}
else {
 imgEndAttachment.SetVisible(false); 
 HyperLinkAttachment.SetNavigateUrl('');
 }                                     
}"></ClientSideEvents>
                                                    <BrowseButton Text=" انتخاب فایل">
                                                        <Image Height="16px" Url="~/Images/Icons/file-upload.png" Width="16px">
                                                        </Image>
                                                    </BrowseButton>
                                                    <CancelButton Text="انصراف">
                                                    </CancelButton>
                                                </TSPControls:CustomAspxUploadControl>
                                            </td>
                                            <td align="right">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnRemoveFile" runat="server" CausesValidation="False" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnRemoveFile_Click" Text=" "
                                                    ToolTip="حذف فایل" UseSubmitBehavior="False">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="16px" Url="~/Images/icons/DeleteFile.png" Width="16px" />
                                                    <ClientSideEvents Click="function(s,e){	 e.processOnServer= confirm('آیا مطمئن به حذف فایل هستید؟');}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxImage runat="server" ID="ASPxImage1" ToolTip="تصویر انتخاب شد" ClientVisible="False"
                                                    ImageUrl="~/Images/icons/button_ok.png" ClientInstanceName="imgEndAttachment">
                                                </dxe:ASPxImage>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <dxe:ASPxHyperLink ID="HyperLinkAttachment" runat="server" ClientInstanceName="HyperLinkAttachment"
                                    Target="_blank" Text="آدرس فایل">
                                </dxe:ASPxHyperLink>
                            </td>
                            <td valign="top" align="right">
                            </td>
                            <tr>
                                <td valign="top" align="right">
                                    توضیحات(حداکثر255کاراکتر)
                                </td>
                                <td valign="top" align="right" colspan="3">
                                    <TSPControls:CustomASPXMemo runat="server" ID="txtDescription"  Width="100% "
                                        >
                                        <ClientSideEvents KeyPress="function(s,e){ CheckDevExpressTextboxLengthOnKeyPress(s,e,255); }" />
                                    </TSPControls:CustomASPXMemo>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            </br>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent2" runat="server">
                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                        ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        CausesValidation="False" AutoPostBack="True" OnClick="btnNew_Click">
                                        
                                        <Image  Url="~/Images/icons/new.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                         ID="btnEdit2" AutoPostBack="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" CausesValidation="False" OnClick="btnEdit_Click">
                                        
                                        <Image  Url="~/Images/icons/edit.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False" OnClick="btnSave_Click">
                                            
                                            <Image  Url="~/Images/icons/save.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server"  EnableTheming="False"
                                        EnableViewState="False" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False" OnClick="btnBack_Click"
                                        CausesValidation="false">
                                        
                                        <Image  Url="~/Images/icons/back.png"  />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ObjectDataSource ID="ObjdsbserverPollPoll" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.PollPollManager"></asp:ObjectDataSource>
            <dx:ASPxHiddenField ID="HiddenFieldPoll" runat="server">
            </dx:ASPxHiddenField>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                        <img src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
