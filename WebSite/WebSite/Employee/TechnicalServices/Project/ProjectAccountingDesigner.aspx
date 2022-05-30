<%@ Page Title="مالی طراحان" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ProjectAccountingDesigner.aspx.cs" Inherits="Employee_TechnicalServices_Project_ProjectAccountingDesigner" %>

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
<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Src="../../../UserControl/WFUserControl.ascx" TagName="WFUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

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

        function ChangeSelectionItem(listBox, ItemsDifferentFromOther, SelectionStatus) {
            for (var i = 0; i < listBox.GetItemCount(); i++)
                if (CheckIndexIsInItemsDifferentFromOther(ItemsDifferentFromOther, i) == false) {
                    if (SelectionStatus == true)
                        listBox.SelectIndices([i]);
                    else
                        listBox.UnselectIndices([i])
                }
        }

        function UpdateSelectAllItemState(listBox, indexAll, ItemsDifferentFromOther) {
            IsAllSelected(listBox, indexAll, ItemsDifferentFromOther) ? listBox.SelectIndices([indexAll]) : listBox.UnselectIndices([indexAll]);
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

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="BtnNew_Click">
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">

                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                            ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف"
                                            ID="btnDelete" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnDelete_Click">
                                            <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
	 e.processOnServer= confirm('آیا مطمئن به حذف کردن این ردیف هستید؟');


}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/delete.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ارسال پیامک"
                                            ID="btnSendSms" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSendSms_Click">
                                            <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
	 e.processOnServer= confirm('آیا از ارسال پیامک برای صاحب این فیش مطمئن هستید؟');


}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/SendSMS.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="پرداخت فیش"
                                            ID="btnPaying" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnPaying_Click">
                                            <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
	 e.processOnServer= confirm('آیا از پرداخت این فیش مطمئن هستید؟');


}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/ConfirmAccounting.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش تاریخ و شماره فیش پرداخت شده"
                                            ID="btnEditFishNo" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnEditFishNo_Click">
                                            <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }


}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/EditPayedFish.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به وضعیت ثبت"
                                            ID="btnCancelPayed" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="CancelPayment_Click">
                                            <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }else
	 e.processOnServer= confirm('آیا از تغییر وضعیت پرداخت فیش انتخاب شده مطمئن می باشید؟');


}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/CancelPayment.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6"></TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " AutoPostBack="false"
                                            ToolTip="چاپ فیش" ID="btnPrintFish" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
                                                                         if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
  grid.PerformCallback('PrintFish'); 
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/printers.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ نامه شهرداری - تایید طراح"
                                            ID="btnDesPermit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
    drdPlanType.SetText('');
    ListBoxPlanType.UnselectAll();    		
	popupChoosePlanType.Show(); 
}"></ClientSideEvents>
                                            <Image Url="~/Images/TS/TSDesPermitt.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>

                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار پروژه"
                                            ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/reload.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            ID="btnBack" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <ClientSideEvents Click="function(s, e) {


}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت پروژه"
                                            CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBackToManagment_Click">
                                            <Image Url="../../../Images/icons/BakToManagment.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomAspxMenuHorizontal ID="MainMenu" runat="server" OnItemClick="MainMenu_ItemClick" CssClass="ProjectMainMenuHorizontal">
                <Items>
                    <dxm:MenuItem Text="مشخصات پروژه" Name="Project" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                        <Items>
                            <dxm:MenuItem Text="اطلاعات پایه" Name="BaseInfo" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="پلاک ثبتی" Name="RegisteredNo" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="دستور نقشه" Name="PlansMethod" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="بلوک" Name="Block" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="بیمه" Name="Insurance" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                            </dxm:MenuItem>
                        </Items>
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مالک" Name="Owner" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مالی پروژه" Name="Accounting" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                        <Items>
                            <%--  <dxm:MenuItem Text="مالی مالکان" Name="AccOwner">
                                </dxm:MenuItem>--%>
                            <dxm:MenuItem Text="مالی طراحان" Name="AccDesigner" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                            </dxm:MenuItem>
                            <%--   <dxm:MenuItem Text="مالی ناظران" Name="AccObserver">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="مالی مجریان" Name="AccImp">
                                </dxm:MenuItem>--%>
                        </Items>
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="نقشه" Name="Plans" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="طراح" Name="Designer" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="ناظر" Name="Observers" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                   <dxm:MenuItem Text="مجری" Name="Implementer"  ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="قرارداد" Name="Contract" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                     <%-- <dxm:MenuItem Text="زمان بندی" Name="Timing">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="پروانه ساخت" Name="BuildingsLicense">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="اعلام وضعیت" Name="StatusAnnouncement">
                    </dxm:MenuItem>--%>
                </Items>
                <RootItemSubMenuOffset X="-1" LastItemY="-2" LastItemX="-1" FirstItemY="-2" FirstItemX="-1"
                    Y="-2"></RootItemSubMenuOffset>
                <Border BorderWidth="1px" BorderStyle="Solid" BorderColor="#A5A6A8"></Border>
                <VerticalPopOutImage Height="8px" Width="4px">
                </VerticalPopOutImage>
                <ItemStyle VerticalAlign="Middle" ImageSpacing="5px" PopOutImageSpacing="7px"></ItemStyle>
                <SubMenuItemStyle ImageSpacing="7px">
                </SubMenuItemStyle>
                <SubMenuStyle BackColor="#EDF3F4" GutterWidth="0px" SeparatorColor="#7EACB1"></SubMenuStyle>
                <HorizontalPopOutImage Height="7px" Width="7px">
                </HorizontalPopOutImage>
            </TSPControls:CustomAspxMenuHorizontal>
            <br />
            <TSP:ProjectInfo ID="prjInfo" runat="server"></TSP:ProjectInfo>
            <ul class="HelpUL">
                <li>در صفحه مالی طراحان امکان اعمال هرگونه تغییر تنها در مرحله گردش کار "ثبت طراح" وجود دارد و منو های مربوطه در این مرحله فعال می گردند
                </li>

            </ul>
            <br />

            <TSPControls:CustomAspxDevGridView ClientInstanceName="grid" Width="100%" ID="GridViewProjectAccounting" Caption="مالی طراحان"
                runat="server" DataSourceID="ObjectDataSourceTsAcc" AutoGenerateColumns="False"
                KeyFieldName="AccountingId" OnCustomCallback="GridViewProjectAccounting_CustomCallback">
                <ClientSideEvents EndCallback="function(e,s){                            
                            if(grid.cpMessage!='')
                            {
                            alert(grid.cpMessage);
                            grid.cpMessage='';
                            }
 if(grid.cpPrintFish == 1)
    {
        grid.cpPrintFish = 0;
        window.open(grid.cpPrintFishPath);
        grid.cpPrintFishPath='';
    }

     if(grid.cpPrintDesPermit == 1)
    {
        grid.cpPrintDesPermit = 0;
        window.open(grid.cpPrintDesPermitPath);
        grid.cpPrintDesPermitPath='';
        popupChoosePlanType.Hide();
    }
    	        }" />
                <SettingsCookies Enabled="false" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="AccountingId" Visible="false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TypeName" Caption="نحوه پرداخت"
                        Width="80px">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="StatusName" Caption="وضعیت"
                        Width="80px">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="IsSMSSentName" Caption="درخواست پیامک"
                        >
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="SendSMSDate" Caption="تاریخ درخواست پیامک"
                        Width="80px">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FishPayerNameWithDetail"
                        Caption="نام پرداخت کننده" Width="170px">
                        <CellStyle Wrap="False" HorizontalAlign="Right">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" Width="230px" FieldName="AccTypeName"
                        Caption="بابت">
                        <CellStyle Wrap="False" HorizontalAlign="Right">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Number" Caption="شماره">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Date" Caption="تاریخ فیش"
                        Width="90px">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Width="200px" VisibleIndex="4" FieldName="Amount" Caption="مبلغ">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                        <PropertiesTextEdit DisplayFormatString="#,#">
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="CreateDate" Caption="تاریخ ایجاد"
                        Width="90px">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="Bank" Caption="بانک">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="BranchCode" Caption="کد شعبه">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="BranchName" Caption="نام شعبه">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="6" Caption=" " ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <Settings ShowHorizontalScrollBar="true" ShowFooter="true"></Settings>
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="Amount" SummaryType="Sum" />
                </TotalSummary>
            </TSPControls:CustomAspxDevGridView>

            <br />
            <TSPControls:CustomASPxPopupControl ID="popupChoosePlanType" runat="server" Width="400px"
                ShowPageScrollbarWhenModal="true"
                AutoUpdatePosition="true" ClientInstanceName="popupChoosePlanType" PopupVerticalAlign="WindowCenter"
                PopupHorizontalAlign="WindowCenter" Modal="True" CloseAction="CloseButton" AllowDragging="True"
                HeaderText="انتخاب نوع نقشه">
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        <table width="100%">
                            <tr>
                                <td colspan="2" align="center">نوع نقشه را مشخص نمائید
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td width="35%" align="right">نوع طراح
                                </td>
                                <td width="65%" align="right">
                                    <TSPControls:CustomASPXDropDownEdit ID="drdPlanType" RightToLeft="True" runat="server"
                                        ClientInstanceName="drdPlanType"
                                        Width="300px">
                                        <ValidationSettings ValidationGroup="ListType" ErrorTextPosition="Bottom" Display="Dynamic">

                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DropDownWindowTemplate>
                                            <TSPControls:CustomASPxListBox RightToLeft="True" ID="ListBoxPlanType" runat="server" DataSourceID="ObjectDataSourcePlanType"
                                                SelectionMode="CheckColumn" TextField="Title" ValueField="PlansTypeId" Width="300px"
                                                ClientInstanceName="ListBoxPlanType">
                                                <ClientSideEvents SelectedIndexChanged="function(s,e){ OnListBoxSelectionChanged(s,drdPlanType,0,'',e); }" />
                                            </TSPControls:CustomASPxListBox>
                                        </DropDownWindowTemplate>
                                        <ButtonStyle Width="13px">
                                        </ButtonStyle>
                                    </TSPControls:CustomASPXDropDownEdit>
                                    <asp:ObjectDataSource ID="ObjectDataSourcePlanType" runat="server" SelectMethod="GetData"
                                        TypeName="TSP.DataManager.TechnicalServices.PlansTypeManager"></asp:ObjectDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <br />
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="&nbsp;&nbsp;تایید"
                                        ID="btnSetList" ValidationGroup="ListType"
                                        UseSubmitBehavior="false" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e){
                                            if (ASPxClientEdit.ValidateGroup('ListType') == false)
                                                return;
                                                  popupChoosePlanType.Hide(); 
                                            grid.PerformCallback('DesPermitPrint');  
                                          }"></ClientSideEvents>
                                        <Image Width="16px" Height="16px" Url="~/Images/ok.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
                <HeaderStyle>
                    <Paddings PaddingTop="1px" PaddingRight="6px" PaddingLeft="10px"></Paddings>
                </HeaderStyle>
                <SizeGripImage Height="12px" Width="12px">
                </SizeGripImage>
                <CloseButtonImage Height="17px" Width="17px">
                </CloseButtonImage>
            </TSPControls:CustomASPxPopupControl>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                            ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف"
                                            ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnDelete_Click">
                                            <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
	 e.processOnServer= confirm('آیا مطمئن به حذف کردن این ردیف هستید؟');


}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/delete.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ارسال پیامک"
                                            ID="btnSendSms1" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSendSms_Click">
                                            <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
	 e.processOnServer= confirm('آیا از ارسال پیامک برای صاحب این فیش مطمئن هستید؟');


}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/SendSMS.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="پرداخت فیش"
                                            ID="btnPaying2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnPaying_Click">
                                            <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
	 e.processOnServer= confirm('آیا از پرداخت این فیش مطمئن هستید؟');


}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/ConfirmAccounting.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش تاریخ و شماره فیش پرداخت شده"
                                            ID="btnEditFishNo2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnEditFishNo_Click">
                                            <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }


}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/EditPayedFish.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به وضعیت ثبت"
                                            ID="btnCancelPayed2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="CancelPayment_Click">
                                            <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }else
	 e.processOnServer= confirm('آیا از تغییر وضعیت پرداخت فیش انتخاب شده مطمئن می باشید؟');


}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/CancelPayment.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator7"></TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ فیش"
                                            ID="btnPrintFish2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
                                                                     if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
  grid.PerformCallback('PrintFish'); 
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/printers.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ نامه شهرداری - تایید طراح"
                                            ID="btnDesPermit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
    drdPlanType.SetText('');
    ListBoxPlanType.UnselectAll();    		
	popupChoosePlanType.Show(); 
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/TS/TSDesPermitt.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>

                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار پروژه"
                                            ID="btnSendNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/reload.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator"></TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            ID="btnBack2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <ClientSideEvents Click="function(s, e) {


}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت پروژه"
                                            CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBackToManagment_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="../../../Images/icons/BakToManagment.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField ID="HiddenFieldAcc" runat="server">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjectDataSourceTsAcc" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="SelectAccountingForProjectForProjectObserverPage" TypeName="TSP.DataManager.TechnicalServices.AccountingManager">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="ProjectId"></asp:Parameter>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="PrjReId"></asp:Parameter>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="ProjectIngridientTypeId"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:HiddenField ID="HDProjectId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="ProjectPgMode" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="RequestId" runat="server" Visible="False"></asp:HiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="mygrid" SessionName="SendBackDataTable_EmpAccPrj"
        OnCallback="CallbackPanelWorkFlow_Callback" />
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                <img src="../../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
