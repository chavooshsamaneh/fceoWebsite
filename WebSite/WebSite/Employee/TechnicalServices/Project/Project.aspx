<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="Project.aspx.cs" Inherits="Employee_TechnicalServices_Project_Project"
    Title="مدیریت پروژه ها" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dxpgw" %>
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
<%@ Register Src="../../../UserControl/WFUserControl.ascx" TagName="WFUserControl"
    TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
        function SetControlValues() {
            GridViewProject.GetRowValues(GridViewProject.GetFocusedRowIndex(), 'ArchiveNo', SetValue);
        }
        function SetValue(values) {
            if (values != '' || values != null)
                txtArchiveNo.SetText(values);
        }

        function PopupArchiveNoShow() {
            txtArchiveNo.SetText('');
            SetControlValues()
            PopupArchiveNo.Show();
        }

        function SaveArchiveNo() {
            PopupArchiveNo.Hide();
            CallbackProject.PerformCallback('SaveArchiveNo');
        }
    </script>
    <div id="divcontent" style="width: 100%" align="center" dir="rtl">
        <TSPControls:CustomAspxCallbackPanel ID="CallbackProject" runat="server" ClientInstanceName="CallbackProject"
            OnCallback="CallbackProject_Callback" Width="100%">
            <ClientSideEvents EndCallback="function(e,s){
            if(CallbackProject.cpPrint==1)
            {
    	        CallbackProject.cpPrint=0;
                e.processOnServer=false;
    	        window.open('../../../Print.aspx');
    	    }
    	    else
    	        CallbackProject.cpPrint=0;

 if(CallbackProject.cpPrintSummary == 1)
    {
        CallbackProject.cpPrintSummary = 0;
        window.open(CallbackProject.cpPrintSummaryPath);
        CallbackProject.cpPrintSummaryPath='';
    }

 if(CallbackProject.cpPrintDesPermit == 1)
    {
        CallbackProject.cpPrintDesPermit = 0;
        window.open(CallbackProject.cpPrintDesPermitPath);
        CallbackProject.cpPrintDesPermitPath='';
        popupChoosePlanType.Hide();
    }
    	        }" />
            <LoadingPanelImage Url="~/Image/indicator.gif" />
            <PanelCollection>
                <dxp:PanelContent runat="server">
                    <div dir="rtl" id="DivReport" class="DivErrors" align="right" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                            href="#">بستن</a>]
                    </div>
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="right">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                    ID="btnNew" UseSubmitBehavior="False" EnableViewState="true" EnableTheming="False"
                                                    AutoPostBack="false">
                                                    <Image Url="~/Images/icons/new.png">
                                                    </Image>
                                                    <ClientSideEvents Click="function(s, e) {
	CallbackProject.PerformCallback('New');
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                    Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="true" EnableTheming="False"
                                                    AutoPostBack="false">

                                                    <Image Url="~/Images/icons/edit.png">
                                                    </Image>
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
	    CallbackProject.PerformCallback('Edit');
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                    ID="btnView" UseSubmitBehavior="False" EnableViewState="true" EnableTheming="False"
                                                    AutoPostBack="false">

                                                    <Image Url="~/Images/icons/view.png">
                                                    </Image>
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
	    CallbackProject.PerformCallback('View');
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بایگانی"
                                                    ID="btnArchive" UseSubmitBehavior="False" EnableViewState="true" EnableTheming="False"
                                                    AutoPostBack="false">

                                                    <Image Url="~/Images/EnteghalArchive.png">
                                                    </Image>
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
    PopupArchiveNoShow();
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ناظر جدید"
                                                    ID="btnObservers" UseSubmitBehavior="False" EnableViewState="true" EnableTheming="False"
                                                    OnClick="btnObservers_Click">

                                                    <Image Url="~/Images/TS/Observers.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="طراح جدید"
                                                    ID="btnDesigners" UseSubmitBehavior="False" EnableViewState="true" EnableTheming="False"
                                                    OnClick="btnDesigners_Click">

                                                    <Image Url="~/Images/TS/Designers.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مجری جدید"
                                                    ID="btnImplementers" UseSubmitBehavior="False" EnableViewState="true" EnableTheming="False"
                                                    OnClick="btnImplementers_Click">

                                                    <Image Url="~/Images/TS/Implementers.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>

                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="لغو درخواست"
                                                    ID="btnDelete" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False" AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s, e) {
	 if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
		if(confirm('آیا مطمئن به حذف این ردیف هستید؟'))
		    CallbackProject.PerformCallback('Delete');
}"></ClientSideEvents>
                                                    <Image Url="~/Images/icons/delete.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>


                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف درخواست و سابقه"
                                                    ID="btnDeleteAll" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False" AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s, e) {
	 if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
		if(confirm('آیا مطمئن به حذف درخواست و سابقه این ردیف هستید؟'))
		    CallbackProject.PerformCallback('DeleteAll');
}"></ClientSideEvents>

                                                    <Image Url="~/Images/icons/DeleteAll.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="تعلیق درخواست تایید شده"
                                                    ID="btnDeleteConfirmedWFState" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False" AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s, e) {
	 if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
		if(confirm('آیا مطمئن به تعلیق درخواست این ردیف هستید؟'))
		    CallbackProject.PerformCallback('DeleteConfirmedWFState');
}"></ClientSideEvents>
                                                    <Image Url="~/Images/icons/DeleteConfirmedWFState.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست تغییرات"
                                                    CausesValidation="False" ID="btnChangeProject" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="true" EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
{
     CallbackProject.PerformCallback('ChangeProjectRequest');
}
}"></ClientSideEvents>
                                                    <Image Url="~/Images/icons/Change.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست صدور پروانه پایان کار"
                                                    CausesValidation="False" ID="btnEndPrjCertificate" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="true" EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
{ 
CallbackProject.PerformCallback('EndPrj');
		
}
}" />
                                                    <Image Height="25px" Url="~/Images/Certificate.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton runat="server" IsMenuButton="true" Text=" " ToolTip="درخواست پروانه ساخت"
                                                    CausesValidation="False" ID="btnChangeRequestLicence" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
	    CallbackProject.PerformCallback('ChangeRequestLicence');
}"></ClientSideEvents>
                                                    <Image Height="25px" Width="25px" Url="~/Images/icons/NewObsFish.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton runat="server" IsMenuButton="true" Text=" " ToolTip="درخواست اضافه اشکوب"
                                                    CausesValidation="False" ID="btnAdditionalStageRequest" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
                                                    
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
	    CallbackProject.PerformCallback('AdditionalStageRequest');
                                                     
}"></ClientSideEvents>
                                                    <Image Height="25px" Width="25px" Url="~/Images/icons/UpGrade.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton runat="server" IsMenuButton="true" Text=" " ToolTip="درخواست توسعه بنا"
                                                    CausesValidation="False" ID="btnIncreaseBuildingAreaRequest" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
	    CallbackProject.PerformCallback('IncreaseBuildingAreaRequest');
}"></ClientSideEvents>
                                                    <Image Height="25px" Width="25px" Url="~/Images/icons/Revival.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton runat="server" IsMenuButton="true" Text=" " ToolTip="درخواست اعلام شروع نشدن ساخت و ساز"
                                                    CausesValidation="False" ID="btnBuildingNotStartedRequest" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
	    CallbackProject.PerformCallback('BuildingNotStarted');
}"></ClientSideEvents>
                                                    <Image Height="25px" Width="25px" Url="~/Images/icons/TransferMeDocRequest.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازیابی رمز عبور"
                                                    CausesValidation="False" ID="btnReset" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnResetSave_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>

                                                    <Image Url="~/Images/ChangePassword.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار"
                                                    ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	ShowWf();
}
}"></ClientSideEvents>
                                                    <Image Url="~/Images/icons/reload.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="پیگیری گردش کار"
                                                    ID="btnTracing" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
            
	if (GridViewProjectRequest.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	else
	    CallbackProject.PerformCallback('Tracing');
}"></ClientSideEvents>

                                                    <Image Url="~/Images/icons/Cheque Status ReChange.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                                    ID="btnPrint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {		
	CallbackProject.PerformCallback('Print');  
}"></ClientSideEvents>

                                                    <Image Url="~/Images/icons/printers.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ خلاصه پروژه"
                                                    ID="btnPrintSummary" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	else		
    	CallbackProject.PerformCallback('PrintSummary');  
}"></ClientSideEvents>

                                                    <Image Url="~/Images/icons/printers2.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ نامه شهرداری - تایید طراح"
                                                    ID="btnDesPermit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="true"
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
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ نامه شهرداری - تایید ناظر"
                                                    ID="btnObsPermit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {		
	CallbackProject.PerformCallback('ObsPermitPrint');  
}"></ClientSideEvents>

                                                    <Image Url="~/Images/TS/printorange.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ نامه شهرداری - تایید ناظر(همراه با تعهد)"
                                                    ID="btnObsPermitWithText" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {		
	CallbackProject.PerformCallback('ObsPermitPrint2');  
}"></ClientSideEvents>

                                                    <Image Url="~/Images/TS/printorange.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False"
                                                    EnableTheming="False" EnableViewState="true" Text=" " ToolTip="خروجی Excel"
                                                    UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">

                                                    <Image Height="25px" Url="~/Images/icons/ExportExcel.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>
                    <br />
                    <ul runat="server" id="ULAlarm" visible="false" class="HelpUL">
                        <li>کاربر گرامی سطح دسترسی مشاهده پروژه ها بر اساس شهرداری برای شما تنظیم نشده است</li>
                    </ul>
                    <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" ClientInstanceName="RoundPanel1"
                        HeaderText="جستجو" runat="server" Width="100%">
                        <PanelCollection>
                            <dx:PanelContent>
                                <table width="100%" dir="rtl">
                                    <tr>
                                        <td valign="top" align="right" width="20%">
                                            <asp:Label runat="server" Text="کد پروژه" ID="Label4"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" width="30%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtCode" ClientInstanceName="txtCode"
                                                Width="100%">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RegularExpression ErrorText="کد پروژه را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right" width="20%">
                                            <asp:Label runat="server" Text="نام پروژه" ID="Label47"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" width="30%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtName" ClientInstanceName="txtName"
                                                Width="100%">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" width="20%">
                                            <asp:Label runat="server" Text="پلاک ثبتی" ID="Label5"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" width="30%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtPelak" ClientInstanceName="txtPelak"
                                                Width="100%">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right" width="20%">
                                            <asp:Label runat="server" Text="شماره پرونده" ID="Label6"></asp:Label>
                                        </td>
                                        <td dir="ltr" valign="top" align="right" width="30%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtFileNumber" ClientInstanceName="txtFileNumber"
                                                Width="100%">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" width="20%">
                                            <asp:Label runat="server" Text="شماره دستور نقشه" ID="Label7"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" width="30%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtMapNumer" ClientInstanceName="txtMapNumer"
                                                Width="100%">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right" width="20%">
                                            <asp:Label runat="server" Text="گروه ساختمان" ID="Label8"></asp:Label>
                                        </td>
                                        <td dir="ltr" valign="top" align="right" width="30%">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                TextField="GroupName" ID="ASPxComboBoxStructureGroups" ClientInstanceName="ASPxComboBoxStructureGroups"
                                                DataSourceID="ObjectdatasourceStructureGroups" ValueType="System.Int32" ValueField="GroupId"
                                                RightToLeft="True" EnableIncrementalFiltering="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </TSPControls:CustomAspxComboBox>
                                            <asp:ObjectDataSource ID="ObjectdatasourceStructureGroups" runat="server" SelectMethod="GetData"
                                                TypeName="TSP.DataManager.TechnicalServices.StructureGroupsManager"></asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="شهر" ID="Label35"></asp:Label>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server"
                                                ID="ASPxComboBoxCity" ClientInstanceName="ASPxComboBoxCity"
                                                AutoPostBack="True" DataSourceID="ObjectDataSourceCity"
                                                ValueField="CitId" TextField="CitName" EnableIncrementalFiltering="True"
                                                RightToLeft="True" OnSelectedIndexChanged="ASPxComboBoxCity_SelectedIndexChanged">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </TSPControls:CustomAspxComboBox>
                                            <asp:ObjectDataSource ID="ObjectDataSourceCity" runat="server" SelectMethod="GetData"
                                                TypeName="TSP.DataManager.CityManager"></asp:ObjectDataSource>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="شهرداری" ID="Label11"></asp:Label>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server"
                                                TextField="MunName" ValueField="MunId" ID="ASPxComboBoxMunicipality" ClientInstanceName="ASPxComboBoxMunicipality"
                                                DataSourceID="ObjectdatasourceMunicipality"
                                                EnableIncrementalFiltering="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </TSPControls:CustomAspxComboBox>

                                            <asp:ObjectDataSource ID="ObjectdatasourceMunicipality" runat="server" SelectMethod="SelectByCity"
                                                TypeName="TSP.DataManager.TechnicalServices.MunicipalityManager">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="-1" Name="CitId" Type="Int32" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" width="20%">
                                            <asp:Label runat="server" Text="تاریخ ایجاد از" ID="Label9"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" width="30%">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="300px" ShowPickerOnTop="True"
                                                ID="txtDateFrom" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                                ShowPickerOnEvent="OnClick" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                        </td>
                                        <td valign="top" align="right" width="20%">
                                            <asp:Label runat="server" Text="تاریخ ایجاد تا" ID="Label10"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" width="30%">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="300px" ShowPickerOnTop="True"
                                                ID="txtDateTo" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                                ShowPickerOnEvent="OnClick" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" width="20%">
                                            <asp:Label runat="server" Text="شماره پروانه ساختمان" ID="Label12"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" width="30%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtLicenceNo" ClientInstanceName="txtLicenceNo"
                                                Width="100%">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right" width="20%">
                                            <asp:Label runat="server" Text="نام مالک" ID="Label1"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" width="30%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtOwnerName" ClientInstanceName="txtOwnerName"
                                                Width="100%">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" width="20%">
                                            <asp:Label runat="server" Text="کد بایگانی" ID="Label2"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" width="30%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtSearchArchiveNo" ClientInstanceName="txtSearchArchiveNo"
                                                Width="100%">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td>شناسه پروژه</td>
                                        <td>
                                            <TSPControls:CustomTextBox runat="server" ID="txtProjectNo" ClientInstanceName="txtProjectNo"
                                                Width="100%">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td valign="top" align="right" width="20%">ناحیه
                                        </td>
                                        <td valign="top" align="right" width="30%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtMainRegion" ClientInstanceName="txtMainRegion"
                                                Width="100%">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td>قطعه</td>
                                        <td>
                                            <TSPControls:CustomTextBox runat="server" ID="txtMainSection" ClientInstanceName="txtMainSection"
                                                Width="100%">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>مرحله
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxComboBox ID="CmbTask" ClientInstanceName="CmbTask" runat="server"
                                                ValueType="System.String"
                                                TextField="TaskName" ValueField="TaskId"
                                                DataSourceID="ObjdsWorkFlowTaskProject" HorizontalAlign="Right" EnableIncrementalFiltering="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" Text="نوع پروژه" ID="Label48"></asp:Label>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxComboBox runat="server"
                                                TextField="NameDiscountPercent" ID="cmbDiscountPercent" ClientInstanceName="cmbDiscountPercent" DataSourceID="ObjectDataSourceDiscountPercent"
                                                ValueType="System.Int32" Width="100%" ValueField="DiscountPercentId"
                                                EnableIncrementalFiltering="True" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />

                                            </TSPControls:CustomAspxComboBox>

                                            <asp:ObjectDataSource ID="ObjectDataSourceDiscountPercent" runat="server" SelectMethod="FindByInActive"
                                                TypeName="TSP.DataManager.TechnicalServices.DiscountPercentManager">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="-1" Name="InActive" Type="Int32" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" width="100%">
                                            <br />
                                            <table width="100%">
                                                <tr>
                                                    <td align="left" width="50%">
                                                        <TSPControls:CustomAspxButton ID="btnSearch" runat="server" AutoPostBack="true" OnClick="btnSearch_OnClick"
                                                            Width="100px" Text="جستجو" ClientInstanceName="btnSearch"
                                                            UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
e.processOnServer=false;
	   if(CheckSearch()==0)
        {
          alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.'); 
          return; 
        }
        else
         e.processOnServer=true;
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td align="right" width="50%">
                                                        <TSPControls:CustomAspxButton ID="btnClear" runat="server" AutoPostBack="true" OnClick="btnClear_OnClick"
                                                            Width="100px" Text="پاک کردن فرم" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
	   	 ClearSearch();
}" />

                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </dx:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanel>
                    <div align="right" width="100%">
                        <ul class="HelpUL">
                            <li>جهت اعمال هرگونه تغییرات در پروژه های انتقال یافته از سیستم قدیم از درخواست تغییرات
                                استفاده نمایید. </li>
                            <li>درصورتی که تمایل به انتصاب کدی دلخواه و در عین حال یکتا به هر پروژه دارید از کد
                                بایگانی استفاده نمایید. </li>
                            <li>نام پروژه فیلد اطلاعاتی است که مفهوم مربوط به خود را دارد و یکتا نمی باشد. </li>
                            <li><b>جددا پیشنهاد می گردد جهت سرعت هرچه بیشتر در جستجو و پیگیری پروژه ها و پرونده
                                های مربوطه، از کد پروژه استفاده نمایید و در هنگام زونکن بندی پرونده ها کد هر پروژه
                                را برای پرونده پروژه مربوطه یادداشت نمایید.</b> </li>
                        </ul>
                    </div>
                    <TSPControls:CustomAspxDevGridView ID="GridViewProject" runat="server" DataSourceID="ObjectDataSourceProject"
                        Width="100%" ClientInstanceName="GridViewProject" OnDetailRowExpandedChanged="GridViewProject_DetailRowExpandedChanged"
                        KeyFieldName="ProjectId" AutoGenerateColumns="False" OnHtmlRowPrepared="GridViewProject_HtmlRowPrepared"
                        OnAutoFilterCellEditorInitialize="GridViewProject_AutoFilterCellEditorInitialize"
                        OnCustomCallback="GridViewProject_CustomCallback">
                        <SettingsCookies Enabled="true" />
                        <Settings ShowHorizontalScrollBar="true" />
                        <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" ExportMode="None"></SettingsDetail>
                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	if(GridViewProject.cpIsReturn!=1)
	{
		GridViewProject.cpSelectedIndex=GridViewProject.GetFocusedRowIndex();
	}
	else
	{
		GridViewProject.cpIsReturn=0;	
	}
}"
                            DetailRowExpanding="function(s, e) {
	if(GridViewProject.cpIsReturn!=1)
	{
		GridViewProject.cpSelectedIndex=GridViewProject.GetFocusedRowIndex();
			
	}
	else
	{
		GridViewProject.cpIsReturn=0;	
	}				
		GridViewProject.SetFocusedRowIndex(GridViewProject.cpSelectedIndex);

}" />
                        <Templates>
                            <DetailRow>

                                <TSPControls:CustomAspxDevGridView ID="GridViewProjectRequest" ClientInstanceName="GridViewProjectRequest" runat="server" DataSourceID="ObjdsProjectRequest"
                                    KeyFieldName="PrjReId" OnBeforePerformDataSelect="GridViewProjectRequest_BeforePerformDataSelect"
                                    OnAutoFilterCellEditorInitialize="GridViewProjectRequest_AutoFilterCellEditorInitialize"
                                    Width="100%">
                                    <SettingsDetail IsDetailGrid="True"></SettingsDetail>
                                    <Settings ShowHorizontalScrollBar="true" />
                                    <Columns>

                                        <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                                            VisibleIndex="0" Width="40px">
                                            <DataItemTemplate>
                                                <div align="center">
                                                    <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>'>
                                                    </dxe:ASPxImage>
                                                </div>
                                            </DataItemTemplate>
                                            <PropertiesComboBox DataSourceID="ObjdsWorkFlowTaskProject" TextField="TaskName" ValueField="TaskId"
                                                ValueType="System.String">
                                            </PropertiesComboBox>
                                        </dxwgv:GridViewDataComboBoxColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="250px" FieldName="PrjReTypeTittle"
                                            Caption="نوع درخواست">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="RequestDate" Caption="تاریخ درخواست"
                                            Width="100px">
                                            <CellStyle Wrap="False" HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" Width="130px" FieldName="ConfirmState"
                                            Caption="وضعیت">
                                            <CellStyle Wrap="False" HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="TaskName" Width="300px"
                                            Caption="وضعیت درخواست" Visible="False">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="زیربنا(متر)" FieldName="ReqFoundation" Name="Foundation"
                                            VisibleIndex="3" Width="70px">
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="بیشترین تعداد طبقات" FieldName="MaxStageNum"
                                            Name="MaxStageNum" VisibleIndex="3" Width="100px">
                                            <CellStyle HorizontalAlign="Center" Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نوع اسکلت" FieldName="StructureSkeletonTitle" Name="StructureSkeletonTitle"
                                            VisibleIndex="1" Width="150px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectRequestStatusTitle"
                                            Caption="وضعیت پروژه در درخواست">
                                             <CellStyle HorizontalAlign="Center" Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="120px" FieldName="RequesterName"
                                            Caption="ارسال کننده">
                                            <CellStyle Wrap="False" HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="120px" FieldName="WFRequesterType"
                                            Caption="سمت">
                                            <CellStyle Wrap="False" HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                </TSPControls:CustomAspxDevGridView>
                            </DetailRow>
                        </Templates>
                        <Columns>
                            <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                                VisibleIndex="0" Width="40px">
                                <DataItemTemplate>
                                    <div align="center">
                                        <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>'>
                                        </dxe:ASPxImage>
                                    </div>
                                </DataItemTemplate>
                                <PropertiesComboBox DataSourceID="ObjdsWorkFlowTaskProject" TextField="TaskName" ValueField="TaskId"
                                    ValueType="System.String">
                                </PropertiesComboBox>
                            </dxwgv:GridViewDataComboBoxColumn>
                            <dxwgv:GridViewDataTextColumn Caption="کد پروژه" FieldName="ProjectId" Name="ProjectId"
                                VisibleIndex="0" Width="50px">
                                <CellStyle Wrap="false" HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نوع پروژه" FieldName="DiscountPercent" Name="DiscountPercent"
                                VisibleIndex="0" Width="50px">
                                <CellStyle Wrap="false" HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="شناسه پروژه" FieldName="ProjectNo" Name="ProjectNo"
                                VisibleIndex="0" Width="50px">
                                <CellStyle Wrap="false" HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نام پروژه" FieldName="ProjectName" Name="ProjectName"
                                VisibleIndex="1" Width="150px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="کد بایگانی" FieldName="ArchiveNo" Name="ArchiveNo"
                                VisibleIndex="1" Width="150px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نام مالک" FieldName="OwnerName" Name="OwnerName"
                                VisibleIndex="1" Width="150px">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نوع اسکلت" FieldName="StructureSkeletonTitle" Name="StructureSkeletonTitle"
                                VisibleIndex="1" Width="150px">
                            </dxwgv:GridViewDataTextColumn>

                            <dxwgv:GridViewDataTextColumn Caption="ناحیه" FieldName="MainRegion" Name="MainRegion"
                                VisibleIndex="1" Width="150px">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="قطعه" FieldName="MainSection" Name="MainSection"
                                VisibleIndex="1" Width="150px">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="شهرداری" FieldName="MunName" Name="MunName"
                                VisibleIndex="1" Width="150px">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نمایندگی" FieldName="AgentName" Name="AgentName"
                                VisibleIndex="1" Width="150px">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="زیربنا(متر)" FieldName="Foundation" Name="Foundation"
                                VisibleIndex="1" Width="70px">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="بیشترین تعداد طبقات" FieldName="MaxStageNum"
                                Name="MaxStageNum" VisibleIndex="1" Width="100px">
                                <CellStyle HorizontalAlign="Center" Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="پلاک ثبتی" FieldName="RegisteredNo" Name="RegisteredNo"
                                VisibleIndex="2" Width="100px">
                                <CellStyle Wrap="false" CssClass="CellLeft" HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="شماره پروانه ساختمان" FieldName="LicenseNo" Name="LicenseNo"
                                VisibleIndex="3" Width="100px">
                                <CellStyle Wrap="false" CssClass="CellLeft" HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="گروه ساختمان" FieldName="GroupName" Name="GroupName"
                                VisibleIndex="4" Width="100px">
                                <HeaderStyle Wrap="true"></HeaderStyle>
                                <CellStyle Wrap="false" HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="تاریخ ایجاد" FieldName="Date" Name="Date"
                                VisibleIndex="5" Width="100px">
                                <CellStyle Wrap="false" HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="وضعیت پروژه" FieldName="ProjectStatus" Name="ProjectStatus"
                                VisibleIndex="6" Width="150px">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                                <PropertiesTextEdit EnableFocusedStyle="False">
                                </PropertiesTextEdit>
                            </dxwgv:GridViewDataTextColumn>

                            <dxwgv:GridViewDataTextColumn Caption="شماره پرونده" FieldName="FileNo" Name="FileNo"
                                VisibleIndex="8">
                                <HeaderStyle Wrap="true"></HeaderStyle>
                                <CellStyle Wrap="false" HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataMemoColumn Caption="شماره دستور نقشه" FieldName="PlansMethodNo"
                                Name="PlansMethodNo" VisibleIndex="9" Width="130px">
                                <CellStyle Wrap="false" HorizontalAlign="Center">
                                </CellStyle>
                                <HeaderStyle Wrap="true"></HeaderStyle>
                            </dxwgv:GridViewDataMemoColumn>
                            <dxwgv:GridViewDataMemoColumn Caption="وضعیت ثبت طراح" FieldName="DesignerSavedName"
                                Name="DesignerSavedName" VisibleIndex="9" Width="130px">
                                <CellStyle Wrap="false" HorizontalAlign="Center">
                                </CellStyle>
                                <HeaderStyle Wrap="true"></HeaderStyle>
                            </dxwgv:GridViewDataMemoColumn>
                            <dxwgv:GridViewCommandColumn VisibleIndex="14" Caption=" " Width="30px" ShowClearFilterButton="true">
                            </dxwgv:GridViewCommandColumn>
                        </Columns>
                    </TSPControls:CustomAspxDevGridView>
                    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewProject">
                    </dxwgv:ASPxGridViewExporter>
                    <br />

                    <fieldset width="100%">
                        <legend>راهنما</legend>
                        <ul class="HelpWorkflowTasksImages">
                            <li class="col-sm-4">
                                <ul>
                                    <%--    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel36" runat="server" Text="تغییرات ناظر: فونت زیتونی" ForeColor="Olive">
                                        </dxe:ASPxLabel>
                                    </li>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel37" runat="server" Text="تغییرات مجری: فونت صورتی" ForeColor="DeepPink">
                                        </dxe:ASPxLabel>
                                    </li>--%>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel43" runat="server" Text="تغییرات: فونت آبی" ForeColor="DarkBlue">
                                        </dxe:ASPxLabel>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                        <ul class="HelpWorkflowTasksImages">
                            <li class="col-sm-4">
                                <ul>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="تغییرات دستور نقشه : فونت قهوه ای"
                                            ForeColor="Brown" Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                        <ul class="HelpWorkflowTasksImages">
                            <li class="col-sm-4">
                                <ul>
                                    <li>
                                        <dxe:ASPxLabel ID="ASPxLabel13" runat="server" Text="صدور پروانه پایان کار: فونت بنفش"
                                            ForeColor="Purple" Font-Bold="False">
                                        </dxe:ASPxLabel>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                        *راهنمای گردش کار درخواست ثبت پروژه ساختمانی
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
                        *راهنمای گردش کار ثبت طراح و تاييد نقشه مربوطه
                        <ul class="HelpWorkflowTasksImages">
                            <li class="col-sm-4">
                                <ul>
                                    <asp:Repeater runat="server" ID="RepeaterWfPLnaHelp1">
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
                                    <asp:Repeater runat="server" ID="RepeaterWfPLnaHelp2">
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
                                    <asp:Repeater runat="server" ID="RepeaterWfPLnaHelp3">
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
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                    ID="btnNew2" UseSubmitBehavior="False" EnableViewState="true" EnableTheming="False"
                                                    AutoPostBack="false">

                                                    <Image Url="~/Images/icons/new.png">
                                                    </Image>
                                                    <ClientSideEvents Click="function(s, e) {
	CallbackProject.PerformCallback('New');
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                    Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False" AutoPostBack="false">

                                                    <Image Url="~/Images/icons/edit.png">
                                                    </Image>
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
	    CallbackProject.PerformCallback('Edit');
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                    ID="btnView2" UseSubmitBehavior="False" EnableViewState="true" EnableTheming="False"
                                                    AutoPostBack="false">

                                                    <Image Url="~/Images/icons/view.png">
                                                    </Image>
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
	    CallbackProject.PerformCallback('View');
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بایگانی"
                                                    ID="btnArchive2" UseSubmitBehavior="False" EnableViewState="true" EnableTheming="False"
                                                    AutoPostBack="false">

                                                    <Image Url="~/Images/EnteghalArchive.png">
                                                    </Image>
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
    PopupArchiveNoShow();
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ناظر جدید"
                                                    ID="btnObservers2" UseSubmitBehavior="False" EnableViewState="true" EnableTheming="False"
                                                    OnClick="btnObservers_Click">

                                                    <Image Url="~/Images/TS/Observers.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="طراح جدید"
                                                    ID="btnDesigners2" UseSubmitBehavior="False" EnableViewState="true" EnableTheming="False"
                                                    OnClick="btnDesigners_Click">
                                                    <Image Url="~/Images/TS/Designers.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مجری جدید"
                                                    ID="btnImplementers2" UseSubmitBehavior="False" EnableViewState="true" EnableTheming="False"
                                                    OnClick="btnImplementers_Click">

                                                    <Image Url="~/Images/TS/Implementers.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="لغو درخواست"
                                                    ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False" AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s, e) {
	 if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
		if(confirm('آیا مطمئن به حذف این ردیف هستید؟'))
		    CallbackProject.PerformCallback('Delete');
}"></ClientSideEvents>

                                                    <Image Url="~/Images/icons/delete.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف درخواست و سابقه"
                                                    ID="btnDeleteAll2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False" AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s, e) {
	 if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
		if(confirm('آیا مطمئن به حذف درخواست و سابقه این ردیف هستید؟'))
		    CallbackProject.PerformCallback('DeleteAll');
}"></ClientSideEvents>

                                                    <Image Url="~/Images/icons/DeleteAll.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="تعلیق درخواست تایید شده"
                                                    ID="btnDeleteConfirmedWFState2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False" AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s, e) {
	 if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
		if(confirm('آیا مطمئن به تعلیق درخواست این ردیف هستید؟'))
		    CallbackProject.PerformCallback('DeleteConfirmedWFState');
}"></ClientSideEvents>

                                                    <Image Url="~/Images/icons/DeleteConfirmedWFState.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست تغییرات"
                                                    CausesValidation="False" ID="btnChangeProject2" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="true" EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
{
	   CallbackProject.PerformCallback('ChangeProjectRequest');
}
}"></ClientSideEvents>
                                                    <Image Url="~/Images/icons/Change.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست صدور پروانه پایان کار"
                                                    CausesValidation="False" ID="btnEndPrjCertificate2" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="true" EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
{
	CallbackProject.PerformCallback('EndPrj');	
}
}" />

                                                    <Image Height="25px" Url="~/Images/Certificate.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton runat="server" IsMenuButton="true" Text=" " ToolTip="درخواست پروانه ساخت"
                                                    CausesValidation="False" ID="btnChangeRequestLicence2" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
	    CallbackProject.PerformCallback('ChangeRequestLicence');
}"></ClientSideEvents>
                                                    <Image Height="25px" Width="25px" Url="~/Images/icons/NewObsFish.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton runat="server" IsMenuButton="true" Text=" " ToolTip="درخواست اضافه اشکوب"
                                                    CausesValidation="False" ID="btnAdditionalStageRequest2" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
	    CallbackProject.PerformCallback('AdditionalStageRequest');
}"></ClientSideEvents>
                                                    <Image Height="25px" Width="25px" Url="~/Images/icons/UpGrade.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton runat="server" IsMenuButton="true" Text=" " ToolTip="درخواست توسعه بنا"
                                                    CausesValidation="False" ID="btnIncreaseBuildingAreaRequest2" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
	    CallbackProject.PerformCallback('IncreaseBuildingAreaRequest');
}"></ClientSideEvents>
                                                    <Image Height="25px" Width="25px" Url="~/Images/icons/Revival.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton runat="server" IsMenuButton="true" Text=" " ToolTip="درخواست اعلام شروع نشدن ساخت و ساز"
                                                    CausesValidation="False" ID="btnBuildingNotStartedRequest2" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
	    CallbackProject.PerformCallback('BuildingNotStarted');
}"></ClientSideEvents>
                                                    <Image Height="25px" Width="25px" Url="~/Images/icons/TransferMeDocRequest.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازیابی رمز عبور"
                                                    CausesValidation="False" ID="btnReset1" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnResetSave_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>

                                                    <Image Url="~/Images/ChangePassword.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار"
                                                    ID="btnSendNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	ShowWf();
}
}"></ClientSideEvents>

                                                    <Image Url="~/Images/icons/reload.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="پیگیری جریان کار"
                                                    ID="btnTracing2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
            
	if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	else
	    CallbackProject.PerformCallback('Tracing');
}"></ClientSideEvents>

                                                    <Image Url="~/Images/icons/Cheque Status ReChange.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                                    ID="btnPrint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {		
	CallbackProject.PerformCallback('Print');  
}"></ClientSideEvents>

                                                    <Image Url="~/Images/icons/printers.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ خلاصه پروژه"
                                                    ID="btnPrintSummary2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewProject.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	else		
    	CallbackProject.PerformCallback('PrintSummary');  
}"></ClientSideEvents>

                                                    <Image Url="~/Images/icons/printers2.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ نامه شهرداری - تایید طراح"
                                                    ID="btnDesPermit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="true"
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
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ نامه شهرداری - تایید ناظر"
                                                    ID="btnObsPermit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {		
	CallbackProject.PerformCallback('ObsPermitPrint');  
}"></ClientSideEvents>

                                                    <Image Url="~/Images/TS/printorange.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ نامه شهرداری - تایید ناظر2"
                                                    ID="btnObsPermitWithText2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="true"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {		
	CallbackProject.PerformCallback('ObsPermitPrint2');  
}"></ClientSideEvents>

                                                    <Image Url="~/Images/TS/printorange.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False"
                                                    EnableTheming="False" EnableViewState="true" Text=" " ToolTip="خروجی Excel"
                                                    UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                                    <Image Height="25px" Url="~/Images/icons/ExportExcel.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>
                    <TSPControls:CustomASPxPopupControl ID="PopupArchiveNo" runat="server" AllowDragging="True" ClientInstanceName="PopupArchiveNo"
                        CloseAction="CloseButton"
                        HeaderText="ثبت کد بایگانی" RightToLeft="True"
                        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        Width="320px">
                        <ContentCollection>
                            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                <table width="100%" align="center">
                                    <tr>
                                        <td>کد بایگانی
                                        </td>
                                        <td align="center">
                                            <TSPControls:CustomTextBox IsMenuButton="true" ID="txtArchiveNo" ClientInstanceName="txtArchiveNo" runat="server"
                                                Width="100%">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="ArchiveNoValidation">
                                                    <RequiredField ErrorText="کد بایگانی را وارد نمایید" IsRequired="True" />
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <br />
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="&nbsp;&nbsp;ذخیره"
                                                ID="btnSaveObsSharePayed" UseSubmitBehavior="False" AutoPostBack="false"
                                                CausesValidation="true" Width="120px" ClientInstanceName="btnSaveObsSharePayed"
                                                ValidationGroup="ArchiveNoValidation">
                                                <Image Width="16px" Height="16px" Url="~/Images/WorkFlow_Save.png" />
                                                <ClientSideEvents Click="function(s, e) {	
    if (ASPxClientEdit.ValidateGroup('ArchiveNoValidation') == false)
      return;   
    SaveArchiveNo();
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </table>
                            </dxpc:PopupControlContentControl>
                        </ContentCollection>
                    </TSPControls:CustomASPxPopupControl>
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
                                                    <%--<RequiredField IsRequired="true" ErrorText="نوع نقشه را انتخاب نمایید" />--%>
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
                                            CallbackProject.PerformCallback('DesPermitPrint');  
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
                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomAspxCallbackPanel>
        <%--        OldValuesParameterFormatString="original_{0}"--%>
        <asp:ObjectDataSource ID="ObjectDataSourceProject" runat="server" SelectMethod="SearchForManagmentPage"
            TypeName="TSP.DataManager.TechnicalServices.ProjectManager">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="ProjectId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="GroupId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="CitId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="MunId" Type="Int32" />
                <asp:Parameter DefaultValue="%" Name="ProjectName" Type="String" />
                <asp:Parameter DefaultValue="%" Name="RegisteredNo" Type="String" />
                <asp:Parameter DefaultValue="%" Name="FileNo" Type="String" />
                <asp:Parameter DefaultValue="%" Name="PlansMethodNo" Type="String" />
                <asp:Parameter DefaultValue="1" Name="FromDate" Type="String" />
                <asp:Parameter DefaultValue="2" Name="ToDate" Type="String" />
                <asp:Parameter DefaultValue="%" Name="LicenseNo" Type="String" />
                <%--LicenseNo همانBuildingCertificateNum--%>
                <asp:Parameter DefaultValue="%" Name="OwnerName" Type="String" />
                <asp:Parameter DefaultValue="%" Name="ArchiveNo" Type="String" />
                <asp:Parameter DefaultValue="-1" Name="AgentId" Type="Int32" />
                <asp:Parameter DefaultValue="(0)" Name="MunParentIdList" Type="String" />
                <asp:Parameter DefaultValue="-1" Name="ProjectNo" Type="Int32" />
                <asp:Parameter DefaultValue="%" Name="MainRegion" Type="String" />
                <asp:Parameter DefaultValue="%" Name="MainSection" Type="String" />
                <asp:Parameter DefaultValue="-1" Name="TaskId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="DiscountPercentId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjdsWorkFlowTaskProject" runat="server" SelectMethod="SelectByWorkCode"
            TypeName="TSP.DataManager.WorkFlowTaskManager">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjdsProjectRequest" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="SelectRequestByProject" TypeName="TSP.DataManager.TechnicalServices.ProjectRequestManager">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="-1" Name="ProjectId" SessionField="ProjectId"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewProject" SessionName="SendBackDataTable_EmpPrj"
            OnCallback="CallbackPanelWorkFlow_Callback" />
        <TSPControls:CustomASPxPopupControl ID="popupWait" runat="server" HeaderText="" EnableViewState="true"
            ClientInstanceName="popupWait" AutoUpdatePosition="True" EnableAnimation="False"
            AllowDragging="false" AllowResize="false" BackColor="Transparent" PopupVerticalAlign="WindowCenter"
            PopupHorizontalAlign="WindowCenter" Modal="True" CloseAction="CloseButton" ShowHeader="False"
            ShowShadow="False">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl5420542" runat="server">
                    <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                        <asp:Image ID="IMG1" ImageUrl="~/Image/indicator.gif" align="middle" runat="server" />
                        لطفا صبر نمایید ...
                    </div>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
            <ContentStyle>
                <Paddings Padding="0px" />
            </ContentStyle>

            <Border BorderWidth="0px" />
        </TSPControls:CustomASPxPopupControl>
        <%-- <dxhf:ASPxHiddenField ID="hiddenFieldIsVisible" ClientInstanceName="HDIsVisible"
            runat="server">
        </dxhf:ASPxHiddenField>--%>
    </div>
</asp:Content>
