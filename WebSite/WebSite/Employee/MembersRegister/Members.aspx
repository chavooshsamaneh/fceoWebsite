<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Members.aspx.cs" Inherits="Employee_MembersRegister_Members"
    Title="اعضای حقیقی" %>

<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
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
        function ShowWFDesc(s, e) {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'wfDescription', OnGetSelectedFieldValues);

        }
        function OnGetSelectedFieldValues(selectedValues) {
            txtWFDesc.SetText(selectedValues);
            PopUpWFDesc.Show();
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelPage" runat="server" ClientInstanceName="CallbackPanelPage"
        OnCallback="CallbackPanelPage_Callback" Width="100%" LoadingPanelImage-Url="../../Image/indicator.gif">
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent1" runat="server">
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="ASPxRoundPanel1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="BtnNew_Click">

                                                <Image Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                ID="btnReqEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqEdit_Click">

                                                <Image Url="~/Images/icons/edit.png" />
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                ID="btnReqView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqView_Click">


                                                <Image Url="~/Images/icons/view.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست جدید"
                                                ID="btnReqNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqNew_Click">

                                                <Image Url="~/Images/icons/new_Disabled.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست انتقال به استان دیگر"
                                                ID="btnTransfer" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqNew_Click">

                                                <Image Url="~/Images/icons/MeReqGoTootherProv.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست بازگشت به سازمان"
                                                ID="btnReturn" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqNew_Click">

                                                <Image Url="~/Images/icons/MeReqReturn.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست فوت"
                                                ID="btnDead" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqNew_Click">

                                                <Image Url="~/Images/icons/MeReqDead.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست اخراج از سازمان"
                                                ID="btnFired" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqNew_Click">

                                                <Image Url="~/Images/icons/MeReqFire.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست مدرک جعلی"
                                                ID="btnFakeLicense" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqNew_Click">

                                                <Image Url="~/Images/icons/MeReqFakeDoc.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست لغو عضویت"
                                                ID="btnCancleMembership" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/MeReqInActive.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف درخواست"
                                                ID="btnReqDelete" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqDelete_Click">

                                                <Image Url="~/Images/icons/delete.png" />
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }   
 e.processOnServer= confirm('آیا مطمئن به حذف این درخواست هستید؟');
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="فیش مالی"
                                                ID="btnAccFish" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnAccFish_Click">
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>


                                                <Image Url="~/Images/icons/AccFishList.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ فیش"
                                                ID="btnPrintAcc" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {		
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	else
 	    CallbackPanelPage.PerformCallback('PrintAcc;'+grid.GetFocusedRowIndex()); 
}"></ClientSideEvents>

                                                <Image Url="~/Images/TS/printorange.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator13"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازیابی رمز عبور"
                                                CausesValidation="False" ID="btnReset" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnResetSave_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
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
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="فرم هاي پرشده"
                                                ID="btnShowInputForm" EnableViewState="False" EnableTheming="False" UseSubmitBehavior="False"
                                                OnClick="btnShowInputForm_Click">

                                                <Image Url="~/Images/FormBuilder_View.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;رديفي انتخاب نشده است&quot;);
 }
}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator ID="MenuSeprator11" runat="server"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep" runat="server" AutoPostBack="False"
                                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                                <ClientSideEvents Click="function(s, e) {                                                          
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 
 {
 
 ShowWf();
 }
}" />

                                                <Image Url="~/Images/icons/reload.png" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing" runat="server" EnableTheming="False"
                                                EnableViewState="False" Text=" " AutoPostBack="true" ToolTip="پیگیری" UseSubmitBehavior="False" OnClick="btnTracing_Click">
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/Cheque Status ReChange.png" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton runat="server" ID="btnShowDetail" ButtonType="ShowDetail"
                                                ToolTip="مشاهده آخرین پانوشت پرونده" IsMenuButton="true" AutoPostBack="false">
                                                <ClientSideEvents Click="ShowWFDesc" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                                CausesValidation="False" ID="btnPrint" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" AutoPostBack="false">

                                                <Image Url="~/Images/icons/printers.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s,e){ 
	CallbackPanelPage.PerformCallback('Print');
}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrintCard" runat="server" CausesValidation="False"
                                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ کارت ورود به جلسه"
                                                UseSubmitBehavior="False" Visible="true" AutoPostBack="false">
                                                <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	else
 	    CallbackPanelPage.PerformCallback('PrintCard;'+grid.GetFocusedRowIndex());
 	
}" />

                                                <Image Url="~/Images/icons/Printers2.png" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrintCardRequest" runat="server" CausesValidation="False"
                                                EnableTheming="False" EnableViewState="False"
                                                Text=" " ToolTip="چاپ درخواست کارت عضویت" UseSubmitBehavior="False" Visible="true"
                                                AutoPostBack="false">
                                                <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	else
 	    CallbackPanelPage.PerformCallback('PrintRequestCard;'+grid.GetFocusedRowIndex());
 	
}" />

                                                <Image Url="~/Images/icons/printCardRequest.png" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False"
                                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                                UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                                <ClientSideEvents Click="function(s,e){  }" />

                                                <Image Url="~/Images/icons/ExportExcel.png" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتخاب ستون ها"
                                                ID="ASPxButton2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="False" Visible="true">
                                                <ClientSideEvents Click="function(s, e) {
	if(!grid.IsCustomizationWindowVisible())
		grid.ShowCustomizationWindow();
	else
		grid.HideCustomizationWindow();
}" />

                                                <Image Url="~/Images/icons/cursor-hand.png" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td align="left" style="width: 100%">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnHelp" runat="server" CausesValidation="False"
                                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                                Visible="true" AutoPostBack="false">
                                                <Image Url="~/Images/Help.png" />

                                                <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>

                <TSPControls:CustomAspxMenuHorizontal ID="SwitchMenu" ClientInstanceName="SwitchMenu" runat="server"
                    AutoPostBack="false">
                    <ClientSideEvents ItemClick="function(s, e) {                   
                              CallbackPanelPage.PerformCallback('SetGridData');              
                            }"></ClientSideEvents>

                    <Items>
                        <dxm:MenuItem Name="Member" Text="اعضای حقیقی" Selected="true">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="TempMember" Text=" اعضای موقت">
                        </dxm:MenuItem>
                    </Items>

                </TSPControls:CustomAspxMenuHorizontal>

                <div align="right">
                    <ul class="HelpUL">
                        <li>شماره پروانه نمایش داده شده در این صفحه برای هر عضو، مربوط به آخرین درخواست تایید
                                شده پروانه شخص در واحد پروانه اشتغال در استان فارس می باشد. </li>
                    </ul>

                </div>
                <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel3" HeaderText="جستجو" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="کد عضویت(دائم/موقت)" ID="ASPxLabel1" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtMeId" ClientInstanceName="txtMeId">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت را صحیح وارد نمایید" />
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="شماره پروانه" Width="100%" ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" dir="ltr" style="width: 35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtFileNo" ClientInstanceName="txtFileNo">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel3" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtFirstName"
                                                ClientInstanceName="txtFirstName">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="ASPxLabel5" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtLastName"
                                                ClientInstanceName="txtLastName">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره همراه" ID="ASPxLabel4" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtMobileNo"
                                                ClientInstanceName="txtMobileNo">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="رشته" ID="ASPxLabel6" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server"
                                                ClientInstanceName="drdMajor" TextField="MjName"
                                                ID="drdMajor" EnableIncrementalFiltering="true" DataSourceID="ODBMajor"
                                                ValueType="System.String" ValueField="MjId" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                    <RequiredField ErrorText="رشته را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ عضویت از" Width="100%" ID="ASPxLabel70">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="230px" ShowPickerOnTop="True"
                                                ID="txtFromDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" Style="direction: ltr"
                                                ShowPickerOnEvent="OnClick" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                            <dxe:ASPxLabel ID="ASPxLabel10" runat="server" ClientInstanceName="lblDateError"
                                                ClientVisible="False" ForeColor="Red" Text="محدوده تاریخ وارد شده صحیح نمی باشد">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ عضویت تا" ID="ASPxLabel8" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="230px" ShowPickerOnTop="True"
                                                ID="txtToDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" Style="direction: ltr"
                                                ShowPickerOnEvent="OnClick" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                        </td>
                                    </tr>



                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="ثبت آخرین درخواست از" Width="100%" ID="ASPxLabel16">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="230px" ShowPickerOnTop="True"
                                                ID="txtReqCreateDateFrom" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" Style="direction: ltr"
                                                ShowPickerOnEvent="OnClick" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                            <dxe:ASPxLabel ID="ASPxLabel17" runat="server" ClientInstanceName="lblDateErrorReq"
                                                ClientVisible="False" ForeColor="Red" Text="محدوده تاریخ وارد شده صحیح نمی باشد">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="ثبت آخرین درخواست تا" ID="ASPxLabel18" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="230px" ShowPickerOnTop="True"
                                                ID="txtReqCreateDateTo" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" Style="direction: ltr"
                                                ShowPickerOnEvent="OnClick" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="ثبت آخرین بررسی از" Width="100%" ID="ASPxLabel19">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="230px" ShowPickerOnTop="True"
                                                ID="txtDateEndAuditor" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" Style="direction: ltr"
                                                ShowPickerOnEvent="OnClick" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                            <dxe:ASPxLabel ID="ASPxLabel20" runat="server" ClientInstanceName="lblDateErrorEndAuditor"
                                                ClientVisible="False" ForeColor="Red" Text="محدوده تاریخ وارد شده صحیح نمی باشد">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="ثبت آخرین بررسی تا" ID="ASPxLabel21" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="230px" ShowPickerOnTop="True"
                                                ID="txtDateEndAuditorTo" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" Style="direction: ltr"
                                                ShowPickerOnEvent="OnClick" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                        </td>
                                    </tr>



                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                        <td align="center" colspan="4" valign="top"></td>
                                    </tr>
                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="وضعیت درخواست" ID="ASPxLabel9" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxComboBox runat="server"
                                                EnableIncrementalFiltering="true" TextField="MjName"
                                                ID="CmbReqType" ClientInstanceName="CmbReqType" ValueType="System.String"
                                                ValueField="MjId" RightToLeft="True"
                                                SelectedIndex="0">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                                <Items>
                                                    <dxe:ListEditItem Selected="True" Text="&lt;همه&gt;" Value="-1" />
                                                    <dxe:ListEditItem Text="در جریان" Value="0" />
                                                </Items>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="ثبت کننده عضو" ID="ASPxLabel15" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxComboBox runat="server"
                                                EnableIncrementalFiltering="true" TextField=""
                                                ID="CmbRequester" ClientInstanceName="CmbRequester" ValueType="System.String"
                                                ValueField="" RightToLeft="True"
                                                SelectedIndex="0">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                                <Items>
                                                    <dxe:ListEditItem Selected="True" Text="&lt;همه&gt;" Value="-1" />
                                                    <dxe:ListEditItem Text="عضو حقیقی" Value="0" />
                                                    <dxe:ListEditItem Text="کارمند" Value="1" />
                                                </Items>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="کد رهگیری" ID="ASPxLabel7" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtFollowCode"
                                                ClientInstanceName="txtFollowCode">
                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td>مرحله</td>
                                        <td>
                                            <TSPControls:CustomAspxComboBox ID="CmbTask" runat="server"
                                                ValueType="System.String"
                                                TextField="TaskName" ValueField="TaskId" RightToLeft="True" ClientInstanceName="CmbTask"
                                                DataSourceID="ObjdsWorkFlowTask">

                                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="center" colspan="4">
                                            <table>
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <TSPControls:CustomAspxButton runat="server" Text="جستجو" ID="btnSearch" AutoPostBack="true"
                                                            UseSubmitBehavior="False" Width="98px"
                                                            ClientInstanceName="btnSearch" OnClick="btnSearch_OnClick">
                                                            <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	if(CheckDate()==-1)
	  lblDateError.SetVisible(true); 
    else
    {
	     lblDateError.SetVisible(false);
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
                                                        <TSPControls:CustomAspxButton runat="server" Text="پاک کردن فرم" ID="btnClearSearch"
                                                            AutoPostBack="true" UseSubmitBehavior="False"
                                                            Width="98px" ClientInstanceName="btnClearSearch" OnClick="btnSearch_OnClick">
                                                            <ClientSideEvents Click="function(s, e) {

	 ClearSearch();
         e.processOnServer=true;
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
                <TSPControls:CustomAspxDevGridView SettingsDetail-ExportMode="Expanded" ID="GridViewMember"
                    runat="server" DataSourceID="ObjdsMembers" Width="100%"
                    ClientInstanceName="grid" OnHtmlRowPrepared="GridViewMember_HtmlRowPrepared"
                    OnPageIndexChanged="GridViewMember_PageIndexChanged">
                    <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
                    <SettingsCookies Enabled="true" StoreFiltering="true" StoreColumnsWidth="true" StoreColumnsVisiblePosition="true" />
                    <SettingsCustomizationWindow Enabled="True" />
                    <Columns>
                        <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                            VisibleIndex="0" Width="40px">
                            <DataItemTemplate>
                                <div align="center">
                                    <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>' ToolTip='<%# Bind("WfTaskFullName") %>'>
                                    </dxe:ASPxImage>
                                </div>
                            </DataItemTemplate>
                            <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                                ValueType="System.String">
                            </PropertiesComboBox>
                        </dxwgv:GridViewDataComboBoxColumn>

                        <dxwgv:GridViewDataTextColumn Caption="تاریخ آخرین درخواست" FieldName="ReqDate" VisibleIndex="0"
                            Width="15%" Visible="false">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نوع آخرین درخواست" FieldName="RequestType" VisibleIndex="0"
                            Width="15%" Visible="false">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ آخرین بررسی" FieldName="WFDate" VisibleIndex="0"
                            Width="15%" Visible="false">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="آخرین بررسی کننده" FieldName="WFDoerName" VisibleIndex="0"
                            Width="15%" Visible="false">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت انتقال" FieldName="TransferTypeName" VisibleIndex="0"
                            Width="15%" Visible="false">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewDataTextColumn Caption="نام پدر" FieldName="FatherName" VisibleIndex="0"
                            Width="15%" Visible="false">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره شناسنامه" FieldName="IdNo" VisibleIndex="0"
                            Width="10%" Visible="false">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="عنوان" FieldName="TiName" Visible="False"
                            VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="جنسیت" FieldName="SexName" Visible="False"
                            VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت تأهل" FieldName="MarName" Visible="False"
                            VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="محل تولد" FieldName="BirthPlace" Visible="False"
                            VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره همراه" FieldName="MobileNo" Visible="False"
                            VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="پست الکترونیکی" FieldName="Email" Visible="False"
                            VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="مقطع تحصیلی" FieldName="LiNames" Visible="False"
                            VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت سربازی" FieldName="SoName" Visible="False"
                            VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شهر اقامت" FieldName="CitName" Visible="False"
                            VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره حساب" FieldName="BankAccNo" Visible="False"
                            VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataImageColumn Caption="تصویر" FieldName="ImageUrl" Visible="False"
                            VisibleIndex="0">
                            <PropertiesImage ImageHeight="100px" ImageWidth="100px">
                            </PropertiesImage>
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataImageColumn Caption="تصویر شناسنامه" FieldName="IdNoPath" Visible="False"
                            VisibleIndex="0">
                            <PropertiesImage ImageHeight="100px" ImageWidth="100px">
                            </PropertiesImage>
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataImageColumn Caption="تصویر کارت ملی" FieldName="SSNPath" Visible="False"
                            VisibleIndex="0">
                            <PropertiesImage ImageHeight="100px" ImageWidth="100px">
                            </PropertiesImage>
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataImageColumn Caption="تصویر پایان خدمت" FieldName="SoldirPath"
                            Visible="False" VisibleIndex="0">
                            <PropertiesImage ImageHeight="100px" ImageWidth="100px">
                            </PropertiesImage>
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataImageColumn Caption="تصویر سکونت استان" FieldName="SelettelmentProofPath"
                            Visible="False" VisibleIndex="0">
                            <PropertiesImage ImageHeight="100px" ImageWidth="100px">
                            </PropertiesImage>
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataTextColumn Caption="آدرس محل سکونت" FieldName="HomeAdr" Name="HomeAdr"
                            Visible="False" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تلفن محل سکونت" FieldName="HomeTel" Name="HomeTel"
                            Visible="False" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="کدپستی محل سکونت" FieldName="HomePO" Name="HomePO"
                            Visible="False" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="آدرس محل کار" FieldName="WorkAdr" Name="WorkAdr"
                            Visible="False" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تلفن محل کار" FieldName="WorkTel" Name="WorkTel"
                            Visible="False" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="کدپستی محل کار" FieldName="WorkPO" Name="WorkPO"
                            Visible="False" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeCode" Name="MeCode"
                            VisibleIndex="0" Width="70px" Visible="true">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="کد عضویت موقت" FieldName="TMeId" Name="TMeId"
                            VisibleIndex="0" Width="90px">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره عضویت" FieldName="MeNo" Name="MeNo"
                            VisibleIndex="0" Width="90px">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نحوه پرداخت" FieldName="PaymentTypeName" Name="PaymentTypeName"
                            VisibleIndex="0" Width="90px">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت پرداخت" FieldName="PaymentStatusName"
                            Name="PaymentStatusName" VisibleIndex="0" Width="90px">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام کاربری" FieldName="UserName" Name="UserName"
                            VisibleIndex="0" Width="70px">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="کد ملی" FieldName="SSN" Visible="False" VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ تولد" FieldName="BirhtDate" Name="BirhtDate"
                            Visible="False" VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="رشته" FieldName="LastMjName" VisibleIndex="3"
                            Width="200px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره پروانه" FieldName="FileNo" Name="FileNo"
                            VisibleIndex="4" Width="150px">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ ثبت نام" FieldName="CreateDate" Name="CreateDate"
                            VisibleIndex="5">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ عضویت" FieldName="MembershipDate" Name="MembershipDate"
                            VisibleIndex="5">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataComboBoxColumn Caption="وضعیت عضویت" FieldName="MrsId" Name="MrsId"
                            VisibleIndex="6">
                            <PropertiesComboBox DataSourceID="ODBMrsId" EnableIncrementalFiltering="True" TextField="MrsName"
                                ValueField="MrsId" ValueType="System.String">
                            </PropertiesComboBox>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نمایندگی" FieldName="AgentName" Name="AgentName"
                            VisibleIndex="7" Width="100px">
                            <CellStyle Wrap="False" HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="9" Width="40px" ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <Settings ShowHorizontalScrollBar="True"></Settings>
                    <Templates>
                        <DetailRow>
                            <div align="center">
                                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridViewRequest" runat="server"
                                    ClientInstanceName="Reqgrid" KeyFieldName="MReId" Width="100%" OnBeforePerformDataSelect="CustomAspxDevGridViewRequest_BeforePerformDataSelect"
                                    DataSourceID="OdbRequest">
                                    <Columns>
                                        <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                                            VisibleIndex="0">
                                            <DataItemTemplate>
                                                <div align="center">
                                                    <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>' ToolTip='<%# Bind("WfTaskFullName") %>'>
                                                    </dxe:ASPxImage>
                                                </div>
                                            </DataItemTemplate>
                                            <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                                                ValueType="System.String">
                                            </PropertiesComboBox>
                                        </dxwgv:GridViewDataComboBoxColumn>
                                        <%--                   <dxwgv:GridViewDataTextColumn FieldName="MReId" Name="MReId" Visible="False" VisibleIndex="0">
                                            </dxwgv:GridViewDataTextColumn>--%>
                                        <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" Name="MeId" Visible="False"
                                            VisibleIndex="0">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="شماره عضویت" FieldName="MeNo" Name="MeNo"
                                            VisibleIndex="0" Width="120px">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="شماره همراه" FieldName="MobileNo" VisibleIndex="0">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="شماره حساب" FieldName="BankAccNo" Visible="False"
                                            VisibleIndex="0">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="CreateDate" Name="CreateDate"
                                            VisibleIndex="0">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نوع تأیید" FieldName="Confirm" VisibleIndex="1">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="تاریخ پاسخ" FieldName="AnswerDate" Name="AnswerDate"
                                            VisibleIndex="2">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نوع" FieldName="Created" VisibleIndex="3">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="Status" VisibleIndex="5"
                                            Visible="False">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="وضعیت عضو در نظام" FieldName="MrsName" VisibleIndex="4"
                                            Visible="False">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="درخواست دهنده" FieldName="TypeName" VisibleIndex="4">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="ارسال کننده" FieldName="RequesterName" VisibleIndex="5">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="WFRequesterType" VisibleIndex="6">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="wfDescriptionSummary" Caption="آخرین پانوشت پرونده">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                </TSPControls:CustomAspxDevGridView>
                            </div>
                        </DetailRow>
                    </Templates>
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" ExportMode="None" />
                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	if(grid.cpIsReturn!=1)
	{
		grid.cpSelectedIndex=grid.GetFocusedRowIndex();
			
	}
	else
	{
		grid.cpIsReturn=0;	
	}

	if(grid.cpIsPostBack!=1);
		//grid.ExpandDetailRow(grid.cpSelectedIndex);	
	else
		grid.cpIsPostBack=0;
}"
                        DetailRowExpanding="function(s, e) {
	grid.cpIsVisible=1;
	HDIsVisible.Set('IsVisible', 1);
	if(grid.cpIsReturn!=1)
	{
		grid.cpSelectedIndex=grid.GetFocusedRowIndex();
			
	}
	else
	{
		grid.cpIsReturn=0;	
	}				
		grid.SetFocusedRowIndex(grid.cpSelectedIndex);

}"
                        DetailRowCollapsing="function(s, e) {
	grid.cpIsVisible=0;
	HDIsVisible.Set('IsVisible', 0);
}" />
                </TSPControls:CustomAspxDevGridView>
                <br />

                <fieldset width="98%">
                    <legend>راهنما</legend>
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td valign="middle" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel36" runat="server" Text="تایید شده: فونت مشکی" ForeColor="Black">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="middle" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel37" runat="server" Text="تایید نشده: فونت خاکستری" ForeColor="Gray">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="middle" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel43" runat="server" Text="در جریان: فونت آبی" ForeColor="darkBlue">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="انتقال به استان دیگر: فونت سبز"
                                        ForeColor="green" Font-Bold="False">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="middle" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel12" runat="server" Text="بازگشت به سازمان: فونت مشکی"
                                        ForeColor="Black">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="middle" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel38" runat="server" Text="فوت شده: فونت نارنجی" ForeColor="darkOrange"
                                        Font-Bold="False">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel13" runat="server" Text="اخراج از سازمان: فونت قرمز"
                                        ForeColor="red" Font-Bold="False">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="middle" align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel14" runat="server" Text="لغو شده: فونت قهوه ای" ForeColor="Brown"
                                        Font-Bold="False">
                                    </dxe:ASPxLabel>
                                </td>
                                <td valign="middle" align="right"></td>
                            </tr>
                        </tbody>
                    </table>
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
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="BtnNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                ID="btnReqEdit1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqEdit_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/edit.png" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                ID="btnReqView1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqView_Click">

                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/view.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست جدید"
                                                ID="btnReqNew1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqNew_Click">

                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/new_Disabled.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست انتقال به استان دیگر"
                                                ID="btnTransfer2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/MeReqGoTootherProv.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست بازگشت به سازمان"
                                                ID="btnReturn2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/MeReqReturn.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست فوت"
                                                ID="btnDead2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/MeReqDead.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست اخراج از سازمان"
                                                ID="btnFired2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/MeReqFire.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست مدرک جعلی"
                                                ID="btnFakeLicense2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/MeReqFakeDoc.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="درخواست لغو عضویت"
                                                ID="btnCancleMembership2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/MeReqInActive.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف درخواست"
                                                ID="btnReqDelete1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnReqDelete_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/delete.png" />
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }   
 e.processOnServer= confirm('آیا مطمئن به حذف این درخواست هستید؟');
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator14"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="فیش مالی"
                                                ID="btnAccFish2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="true" OnClick="btnAccFish_Click">
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/AccFishList.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ فیش"
                                                ID="btnPrintAcc2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {		
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	else
 	    CallbackPanelPage.PerformCallback('PrintAcc;'+grid.GetFocusedRowIndex()); 
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/TS/printorange.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator7"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازیابی رمز عبور"
                                                CausesValidation="False" ID="btnReset1" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnResetSave_Click">
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/ChangePassword.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator8"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="فرم هاي پرشده"
                                                ID="btnShowInputForm2" EnableViewState="False" EnableTheming="False" UseSubmitBehavior="False"
                                                OnClick="btnShowInputForm_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/FormBuilder_View.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;رديفي انتخاب نشده است&quot;);
 }
}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator ID="MenuSeprator12" runat="server"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendToNexStep" runat="server" AutoPostBack="False"
                                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                                <ClientSideEvents Click="function(s, e) {                                                             
if (grid.GetFocusedRowIndex()&lt;0)
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
                                                <Image Url="~/Images/icons/reload.png" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing2" runat="server" AutoPostBack="true"
                                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="پیگیری" UseSubmitBehavior="False" OnClick="btnTracing_Click">
                                                <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
   return;
 }
   e.processOnServer=true;
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/Cheque Status ReChange.png" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" ID="btnShowDetail2" ButtonType="ShowDetail"
                                                ToolTip="مشاهده آخرین پانوشت پرونده" AutoPostBack="false">
                                                <ClientSideEvents Click="ShowWFDesc" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator9"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                                CausesValidation="False" ID="btnPrint2" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" AutoPostBack="false">
                                                <ClientSideEvents Click="function(s,e){ 
	CallbackPanelPage.PerformCallback('Print');
}" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/printers.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrintCard2" runat="server" CausesValidation="False"
                                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ کارت ورود به جلسه"
                                                UseSubmitBehavior="False" Visible="true" AutoPostBack="false">
                                                <ClientSideEvents Click="function(s, e) { 	     	    
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	else
 	    CallbackPanelPage.PerformCallback('PrintCard;'+grid.GetFocusedRowIndex());
 	
}" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/Printers2.png" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrintCardRequest2" runat="server" CausesValidation="False"
                                                EnableTheming="False" EnableViewState="False"
                                                Text=" " ToolTip="چاپ درخواست کارت عضویت" UseSubmitBehavior="False" Visible="true"
                                                AutoPostBack="false">
                                                <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	else
 	    CallbackPanelPage.PerformCallback('PrintRequestCard;'+grid.GetFocusedRowIndex());
 	
}" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/printCardRequest.png" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False"
                                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                                UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                                <ClientSideEvents Click="function(s,e){  }" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/ExportExcel.png" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator10"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتخاب ستون ها"
                                                ID="btnChooseColumn2" AutoPostBack="false" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" Visible="true">
                                                <ClientSideEvents Click="function(s, e) {
	if(!grid.IsCustomizationWindowVisible())
		grid.ShowCustomizationWindow();
	else
		grid.HideCustomizationWindow();
}" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/cursor-hand.png" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td align="left" style="width: 100%">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnhelp2" runat="server" CausesValidation="False"
                                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="راهنما" UseSubmitBehavior="False"
                                                Visible="true" AutoPostBack="false">
                                                <Image Url="~/Images/Help.png" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <ClientSideEvents Click="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>

                <TSPControls:CustomASPxPopupControl ID="PopUpWFDesc" runat="server" Width="387px" Height="500px"
                    ClientInstanceName="PopUpWFDesc"
                    AllowDragging="True" CloseAction="CloseButton"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    HeaderText="آخرین پانوشت پرونده">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <div width="100%" align="center">
                                <TSPControls:CustomASPXMemo runat="server" Height="500px" ID="txtWFDesc" Width="387px"
                                    ClientInstanceName="txtWFDesc" ReadOnly="true">
                                </TSPControls:CustomASPXMemo>
                            </div>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </TSPControls:CustomASPxPopupControl>
            </dxp:PanelContent>
        </PanelCollection>
        <ClientSideEvents EndCallback="function(s, e) {
if(CallbackPanelPage.cpDoPrint == 1)
{
    CallbackPanelPage.cpDoPrint = 0;
    window.open('../../Print.aspx');
}
if(CallbackPanelPage.cpDoPrintCard == 1)
{
    CallbackPanelPage.cpDoPrintCard = 0;
    window.open(CallbackPanelPage.cpPrintCardPath);
    CallbackPanelPage.cpPrintCardPath='';
}
if(CallbackPanelPage.cpDoPrintRequestCard == 1)
{
    CallbackPanelPage.cpDoPrintRequestCard = 0;
    window.open(CallbackPanelPage.cpPrintRequestCardPath);
    CallbackPanelPage.cpPrintRequestCardPath='';
}


}" />
    </TSPControls:CustomAspxCallbackPanel>
    <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="grid" SessionName="SendBackDataTable_MeConf"
        OnCallback="WFUserControl_Callback" />
    <asp:ObjectDataSource ID="ODBMrsId" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.MembershipRegistrationStatusManager" FilterExpression="MrsId=1 or MrsId=2 or MrsId=3 or MrsId=4 or MrsId=5 or MrsId=7 or MrsId=10 or MrsId=8"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODBMajor" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsMembers" runat="server" TypeName="TSP.DataManager.MemberManager"
        SelectMethod="SelectMemberForManagmentPage" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="MeId" Type="Int32" DefaultValue="-1" />
            <asp:Parameter DefaultValue="%" Name="FirstName" Type="String" />
            <asp:Parameter DefaultValue="%" Name="LastName" Type="String" />
            <asp:Parameter DefaultValue="%" Name="MobileNo" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="MjId" Type="Int32" />
            <asp:Parameter DefaultValue="%" Name="FileNo" Type="String" />
            <asp:Parameter DefaultValue="1" Name="DateFrom" Type="String" />
            <asp:Parameter DefaultValue="3" Name="DateTo" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="IsConfirm" Type="Int32" />
            <asp:Parameter DefaultValue="%" Name="FollowCode" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="Requester" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="AgentId" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="ReqCreateDateFrom" Type="String" />
            <asp:Parameter DefaultValue="3" Name="ReqCreateDateTo" Type="String" />
            <asp:Parameter DefaultValue="1" Name="WFDate" Type="String" />
            <asp:Parameter DefaultValue="2" Name="WFDateTo" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="TaskId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsTempMembers" runat="server" TypeName="TSP.DataManager.TempMemberManager"
        SelectMethod="SelectTemporaryMemberForManagmentPage" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="MeId" Type="Int32" DefaultValue="-1" />
            <asp:Parameter DefaultValue="%" Name="FirstName" Type="String" />
            <asp:Parameter DefaultValue="%" Name="LastName" Type="String" />
            <asp:Parameter DefaultValue="%" Name="MobileNo" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="MjId" Type="Int32" />
            <asp:Parameter DefaultValue="%" Name="FileNo" Type="String" />
            <asp:Parameter DefaultValue="1" Name="DateFrom" Type="String" />
            <asp:Parameter DefaultValue="3" Name="DateTo" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="IsConfirm" Type="Int32" />
            <asp:Parameter DefaultValue="%" Name="FollowCode" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="Requester" Type="Int32" />
            <asp:Parameter DefaultValue="1" Name="ReqCreateDateFrom" Type="String" />
            <asp:Parameter DefaultValue="3" Name="ReqCreateDateTo" Type="String" />
            <asp:Parameter DefaultValue="1" Name="WFDate" Type="String" />
            <asp:Parameter DefaultValue="2" Name="WFDateTo" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="AgentId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="TaskId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbRequest" runat="server" SelectMethod="SelectMemberRequestForManagmentPage"
        TypeName="TSP.DataManager.MemberRequestManager">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="-1" Name="MeId" SessionField="MemberId" Type="Int32" />
            <asp:SessionParameter DefaultValue="-1" Name="IsMeTemp" SessionField="IsMeTemp" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
        TypeName="TSP.DataManager.WorkFlowTaskManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewMember"
        ExportedRowType="All">
    </dxwgv:ASPxGridViewExporter>
    <dxhf:ASPxHiddenField ID="hiddenFieldIsVisible" ClientInstanceName="HDIsVisible"
        runat="server">
    </dxhf:ASPxHiddenField>
    <dxhf:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
    </dxhf:ASPxHiddenField>

</asp:Content>
