<%@ Page Title="فیش مالی" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MembersAccounting.aspx.cs" Inherits="Employee_MembersRegister_MembersAccounting" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function SetFiche() {
            lblFishNo.SetText('شماره فیش');
        }
        function SetPose() {
            lblFishNo.SetText('شناسه واریز');
        }

        function SetEpayment() {
            lblFishNo.SetText('شناسه پرداخت');
        }

    </script>
    <TSPControls:CustomAspxCallbackPanel ID="CallBackPage" ClientInstanceName="CallBackPage" HideContentOnCallback="false"
        Width="100%" runat="server" OnCallback="CallBackPage_Callback">
        <ClientSideEvents EndCallback="function(s, e) {                     
                     if(s.cpPrint==1)
                     {
                        window.open(s.cpPrintPath);
                        s.cpPrint=0;
                     }
                     if(s.cpShowMsg==1)
                     {
                        alert(s.cpMsg);
                        s.cpShowMsg=0;
                     }
                     }"
                     />
        <PanelCollection>
            <dxp:PanelContent>
                <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
                    CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
                    FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                    WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
                </pdc:PersianDateScriptManager>
                <div align="right" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tr>

                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server" EnableTheming="False"
                                            EnableViewState="False" AutoPostBack="true" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/save.png" />
                                          
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                            CausesValidation="False" ID="btnPrint" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s,e){
                                                                    CallBackPage.PerformCallback('Print');
                                                                    }" />

                                            <Image Url="~/Images/TS/printorange.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="تایید پرداخت فیش"
                                            ID="btnPaymentConfirm" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"  OnClick="btnPaymentConfirm_Click"
                                            AutoPostBack="true">
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/PaymentConfirm.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="عدم تایید پرداخت فیش"
                                            ID="btnPaymentReject" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="true" OnClick="btnPaymentReject_Click"> 
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/PaymentReject.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {                                                          
 ShowWf();

}" />

                                            <Image Url="~/Images/icons/reload.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                            ToolTip="بازگشت" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/Back.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت اعضا"
                                            CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBackToManagment_Click">

                                            <Image Url="../../Images/icons/BakToManagment.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <div style="width: 100%" align="right">
                    <TSPControls:CustomAspxMenuHorizontal ID="MenuTop" runat="server"
                        OnItemClick="MenuTop_ItemClick" ClientInstanceName="menu">
                        <Items>
                            <dxm:MenuItem Name="Request" Text="مشخصات عضو">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Job" Text="سوابق کاری">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Language" Text="زبان ها">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Activity" Text="فعالیت ها">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="مستندات" Name="Attach">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="گروه ها" Name="Group">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="مالی" Name="AccFish" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="گزارش تنظیمات" Name="PollAnswer">
                            </dxm:MenuItem>
                        </Items>

                    </TSPControls:CustomAspxMenuHorizontal>
                </div>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelMemberInfo" HeaderText="خلاصه اطلاعات عضو"
                    runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <fieldset runat="server" id="RoundPanelPage">
                                <legend class="fieldset-legend" dir="rtl">مشخصات عضو
                                </legend>
                                <table>
                                    <tr>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="شماره عضویت:" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtMeNo" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="وضعیت عضویت:" ID="lblStatus" Width="100%"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtMsName" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="نام: *" ID="Label14" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtFirstName" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 115px">
                                            <asp:Label runat="server" Text="نام خانوادگی: *" ID="Label26" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtLastName" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel ID="ASPxLabel26" runat="server" Text="نام(انگلیسی:) *" Width="100%"
                                                Wrap="False" RightToLeft="True">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtFirstNameEn" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="نام خانوادگی(انگلیسی): *" ID="Label27" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtLastNameEn" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره شناسنامه: *" ID="Label44" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtIdNo" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="کد ملی: *" ID="Label45" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtSSN" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="نام پدر: *" ID="Label46" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtFatherName" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="محل صدور: *" ID="Label47" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtIssuePlace" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="تاریخ تولد: *" ID="Label48"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtBirthDate" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="محل تولد:" ID="Label51"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtBirhtPlace" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="آدرس محل سکونت: *" Width="100%" ID="Label28"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtHomeAdr" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <%-- <td></td>
                                        <td></td>--%>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره تلفن محل سکونت: *" Width="100%" ID="Label29"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtHometel" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                            <%-- <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="vertical-align: top; text-align: right" >
                                                           
                                                        </td>
                                                        <td style="vertical-align: top; text-align: right" >
                                                            <asp:Label runat="server" Text="-" ID="Label65" Width="100%"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: top; text-align: right">
                                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtHometel_cityCode" Font-Bold="true"
                                                                Width="100%">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>--%>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره همراه: *" ID="Label30" Width="100%"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td26" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtMobileNo" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td runat="server" id="Td27" valign="top" align="right">
                                            <asp:Label runat="server" Text="آدرس محل کار:" ID="Label31" Width="100%"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td28" valign="top" align="right" colspan="3">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtWorkAdr" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td runat="server" id="Td29" valign="top" align="right">
                                            <asp:Label runat="server" Text="تلفن محل کار:" ID="Label32" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtWorkTel" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                            <%--  <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td align="right" style="width: 85%">
                                                      
                                                        </td>
                                                        <td style="vertical-align: top; text-align: right" style="width: 2%">
                                                            <asp:Label runat="server" Text="-" ID="Label33" Width="100%"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: top; text-align: right" style="width: 13%">
                                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtWorkTel_cityCode" Font-Bold="true"
                                                                Width="100%">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>--%>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره فکس:" ID="Label34" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtFaxNo" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                            <%-- <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="vertical-align: top; text-align: right" style="width: 85%">
                                                         
                                                        </td>
                                                        <td style="vertical-align: top; text-align: right" style="width: 2%">
                                                            <asp:Label runat="server" Text="-" ID="Label35" Width="100%"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: top; text-align: right" style="width: 13%">
                                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtFaxNo_cityCode" Font-Bold="true"
                                                                Width="100%">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>--%>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr11">
                                        <td runat="server" id="Td33" valign="top" align="right">
                                            <asp:Label runat="server" Text="کد پستی محل سکونت:" Width="100%" ID="Label36"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td34" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtHomePO" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td runat="server" id="Td35" valign="top" align="right">
                                            <asp:Label runat="server" Text="کد پستی محل کار:" ID="Label37" Width="100%"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td36" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtWorkPO" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr id="Tr12" runat="server">
                                        <td id="Td37" runat="server" align="right" valign="top">
                                            <asp:Label ID="Label59" runat="server" Text="جنسیت: *"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtSexName" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="ملیت:" ID="lblNationality" Width="100%"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtNationality" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr id="Tr13" runat="server">
                                        <td id="Td43" runat="server" align="right" valign="top" style="width: 115px">
                                            <dxe:ASPxLabel runat="server" Text="وضعیت سربازی: *" ID="lblSol" ClientInstanceName="lbl"
                                                Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtSoName" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <asp:Label ID="Label38" runat="server" Text="وضعیت تأهل: *" Width="100%"></asp:Label>
                                        </td>
                                        <td align="right" dir="ltr" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtMarName" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>

                                    <tr runat="server" id="Tr15454">
                                        <td runat="server" id="Td47" valign="top" align="right">
                                            <asp:Label runat="server" Text="محل اقامت: *" ID="Label39" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtCitName" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نمایندگی: *" ID="ASPxLabel12" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtAgentName" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label runat="server" Text="شماره حساب: " ID="Label49" Width="100%"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtBankAccNo" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد نظارت شهرداری:" ID="ASPxLabel29" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtArchitectorCode" Font-Bold="true"
                                                Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr10150">
                                        <td runat="server" id="Td3433" valign="top" align="right">
                                            <asp:Label runat="server" Text="آدرس وب سایت:" ID="Label40" Width="100%"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td35454" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtWebsite" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr runat="server" id="Tr16y671">
                                        <td runat="server" id="Td3565655" valign="top" align="right">
                                            <asp:Label runat="server" Text="آدرس پست الکترونیکی:" ID="lblEmail" Width="100%"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td332426" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtEmail" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="تصویر: *" ID="Label50" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgMember" ClientInstanceName="meImg"
                                                Border-BorderWidth="1px" Border-BorderStyle="Solid">
                                                <EmptyImage Height="75px" Width="75px" Url="~/Images/Person.png">
                                                </EmptyImage>
                                            </dxe:ASPxImage>
                                            <br />
                                            <dxe:ASPxLabel ID="ASPxLabelImgWarning" runat="server" ForeColor="#0000C0" Text="">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="تصویر امضا:" ID="Label42"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="ImgSign" ClientInstanceName="signImg"
                                                Border-BorderWidth="1px" Border-BorderStyle="Solid">
                                                <EmptyImage Url="~/Images/noimage.gif">
                                                </EmptyImage>
                                            </dxe:ASPxImage>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="تصویر صفحه اول شناسنامه: *">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxHyperLink ID="HpIdNo" ImageHeight="75px" ImageWidth="75px" runat="server" ClientInstanceName="hidno" Target="_blank"
                                                Text="تصویر صفحه اول شناسنامه">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="تصویر صفحه دوم شناسنامه:">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxHyperLink ID="HIdNoP2" ImageHeight="75px" ImageWidth="75px" runat="server" ClientInstanceName="HIdNoP2" Target="_blank"
                                                Text="تصویر صفحه دوم شناسنامه">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel ID="ASPxLabel19" runat="server" Text="تصویر صفحه توضیحات شناسنامه:">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxHyperLink ID="HIdNoPDes" ImageHeight="75px" ImageWidth="75px" runat="server" ClientInstanceName="HIdNoPDes" Target="_blank"
                                                Text="تصویر صفحه توضیحات شناسنامه">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel ID="ASPxLabel27" runat="server" Text="تصویر مدرک سکونت در استان: *">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxHyperLink ID="HypLinkResident" ImageHeight="75px" ImageWidth="75px" runat="server" ClientInstanceName="hRes"
                                                Target="_blank" Text="تصویر مدرک سکونت در استان">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel ID="ASPxLabel13" runat="server" Text="تصویر روی کارت ملی: *">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxHyperLink ID="HpSSN" runat="server" ImageHeight="75px" ImageWidth="75px" ClientInstanceName="hssn" Target="_blank"
                                                Text="تصویر روی کارت ملی">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="تصویر پشت کارت ملی: *">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxHyperLink ID="HssnBack" runat="server" ImageHeight="75px" ImageWidth="75px" ClientInstanceName="HssnBack" Target="_blank"
                                                Text="تصویر پشت کارت ملی">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel ID="lblSolFile" ClientVisible="false" runat="server" Text="تصویر روی کارت پایان خدمت:"
                                                ClientInstanceName="lblsolfile">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top" dir="rtl">
                                            <dxe:ASPxHyperLink ID="HpSoldier" runat="server" ImageHeight="75px" ImageWidth="75px" ClientInstanceName="hsol" ClientVisible="False"
                                                Target="_blank" Text="تصویر روی کارت پایان خدمت">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel ID="lblSoldireBack" ClientVisible="false" runat="server" Text="تصویر پشت کارت پایان خدمت:"
                                                ClientInstanceName="lblSoldireBack">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top" dir="rtl">
                                            <dxe:ASPxHyperLink ID="HpSoldierBack" runat="server" ImageHeight="75px" ImageWidth="75px" ClientInstanceName="HpSoldierBack" ClientVisible="False"
                                                Target="_blank" Text="تصویر پشت کارت پایان خدمت">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="شرح درخواست:" ID="Label60"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtReqDesc" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات:" ID="ASPxLabel10">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="3" align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtStDesc" Font-Bold="true" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <TSPControls:CustomASPxCheckBox runat="server" Text="اينجانب متعهد مي گردم درصورت قبولي در آزمون ورود به حرفه مهندسي و با علم به اينكه، براساس ابلاغ، اصلاحيه آيين نامه اجرايي قانون نظام مهندسي و كنترل ساختمان در تاريخ 94/12/20، ارائه كارت پايان خدمت يا كارت معافيت از خدمت جهت صدور/تمديد پروانه اشتغال الزامي مي باشد تصوير كارت مذكور را در سايت نظام مهندسي آپلود كرده و تصوير برابر اصل آن را تحويل واحد عضويت و پروانه اشتغال (حقيقي وحقوقي) بدهم در غيراينصورت امكان صدور پروانه اشتغال وجود نداشته و حق هرگونه اعتراضي را ازخود سلب مي نمايم" EnableClientSideAPI="True"
                                                ID="chbSoLdire" ClientInstanceName="chbSoLdire" Visible="false" Enabled="false">
                                            </TSPControls:CustomASPxCheckBox>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td colspan="4" align="right" valign="top">
                                            <TSPControls:CustomASPxCheckBox runat="server" Text="دارای مدرک تحصیلی کارشناسی ناپیوسته یا کاردانی می باشد."
                                                EnableClientSideAPI="True" ID="ChkBKardani" ClientInstanceName="ChkBKardani" ClientEnabled="false">
                                            </TSPControls:CustomASPxCheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right"  valign="top">
                                            <dxe:ASPxLabel runat="server" Text="تصویر استعلام عدم عضویت در نظام کاردانی*" ID="lblKardani">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" colspan="3" valign="top">
                                            <dxe:ASPxHyperLink ID="HpKardani" runat="server" ClientInstanceName="HpKardani"
                                                ClientVisible="False" Target="_blank" Text="تصویر استعلام عدم عضویت در نظام کاردانها">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td runat="server" align="right" colspan="4" valign="top" id="Td443y6">
                                            <TSPControls:CustomASPxCheckBox Enabled="false" ID="ChEnteghali" runat="server" ClientInstanceName="CHB"
                                                EnableClientSideAPI="True" Text="از استان دیگری به استان فارس منتقل شده است"
                                                Width="100%">
                                            </TSPControls:CustomASPxCheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="4" valign="top">
                                            <dxp:ASPxPanel ID="PanelTransferMember" runat="server" Width="100%">
                                                <PanelCollection>
                                                    <dxp:PanelContent>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 15%" align="right" valign="top">
                                                                    <dxe:ASPxLabel ID="ASPxLabel18" runat="server" ClientInstanceName="lblPr" Text="نوع انتقالی:"
                                                                        Width="100%">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="width: 35%" align="right" valign="top">
                                                                    <dxe:ASPxLabel runat="server" Text="- - -" ID="lblTransferStatus" Font-Bold="true" Width="100%">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="right" style="vertical-align: top" style="width: 15%">
                                                                    <dxe:ASPxLabel ID="ASPxLabel8" runat="server" ClientInstanceName="lblPr" Text="استان قبلی:"
                                                                        Width="100%">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="right" style="vertical-align: top" style="width: 35%">
                                                                    <dxe:ASPxLabel runat="server" Text="- - -" ID="txtTPr" Font-Bold="true" Width="100%">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="top">
                                                                    <dxe:ASPxLabel ID="ASPxLabel14" runat="server" ClientInstanceName="lblDate" Text="تاریخ انتقالی:"
                                                                        Width="100%">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td style="vertical-align: top" align="right" valign="top">
                                                                    <dxe:ASPxLabel runat="server" Text="- - -" ID="txtTDate" Font-Bold="true" Width="100%">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="center" style="vertical-align: top">
                                                                    <dxe:ASPxLabel ID="ASPxLabel15" runat="server" ClientInstanceName="lblMeNo" Text="شماره عضویت:"
                                                                        Width="100%">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxLabel runat="server" Text="- - -" ID="txtTMeNo" Font-Bold="true" Width="100%">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" enabled="false" colspan="4" style="vertical-align: top; text-align: right" valign="top">
                                                                    <TSPControls:CustomASPxCheckBox ID="ChbTCheckFileNo" runat="server" ClientInstanceName="chbtc"
                                                                        Text="داری پروانه اشتغال در سایر استان ها می باشد" Width="100%">
                                                                    </TSPControls:CustomASPxCheckBox>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="center">
                                                                    <dxp:ASPxPanel ID="PanelEntegaliDoc" ClientInstanceName="PanelEntegaliDoc" runat="server"
                                                                        Width="100%">
                                                                        <PanelCollection>
                                                                            <dxp:PanelContent>
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td colspan="4" align="center">
                                                                                            <dxe:ASPxLabel runat="server" Text="شماره پروانه و تاریخ ها مربوط به آخرین پروانه فرد در استان قبلی می باشد"
                                                                                                Font-Bold="true" ForeColor="DarkRed" ID="lblAlertPreDocDate">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 15%" align="right" valign="top">
                                                                                            <dxe:ASPxLabel ID="lblDocPr" runat="server" ClientInstanceName="lblDocPr" Text="استان صدور پروانه:"
                                                                                                Width="100%">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="width: 35%" align="right" valign="top">
                                                                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtDocPr" Font-Bold="true" Width="100%">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td></td>
                                                                                        <td></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right" valign="top">
                                                                                            <dxe:ASPxLabel ID="ASPxLabel16" runat="server" ClientInstanceName="lblFileNo" Text="شماره پروانه اشتغال:"
                                                                                                Width="100%">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td align="right" style="vertical-align: top" valign="top">
                                                                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtTFileNo" Font-Bold="true" Width="100%">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel runat="server" Text="تاریخ اولین صدور:" ID="ASPxLabel11" Width="100%"
                                                                                                ClientInstanceName="lblFirstDocRegDate">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtFirstDocRegDate" Font-Bold="true"
                                                                                                Width="100%">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel runat="server" Text="تاریخ آخرین صدور:" ID="ASPxLabel34" Width="100%"
                                                                                                ClientInstanceName="lblCurrentDocRegDate">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtCurrentDocRegDate" Font-Bold="true"
                                                                                                Width="100%">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel runat="server" Text="تاریخ اعتبار:" ID="ASPxLabel37" ClientInstanceName="lblCurrentDocExpDate">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                        <td>
                                                                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtCurrentDocExpDate" Font-Bold="true"
                                                                                                Width="100%">
                                                                                            </dxe:ASPxLabel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </dxp:PanelContent>
                                                                        </PanelCollection>
                                                                    </dxp:ASPxPanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 115px;" align="right" valign="top">
                                                                    <dxe:ASPxLabel ID="ASPxLabel17" runat="server" ClientInstanceName="lblImg" Text="تصویر نامه انتقالی:"
                                                                        Width="102px">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td colspan="3" align="right" valign="top">
                                                                    <dxe:ASPxImage ID="Timg" runat="server" ClientInstanceName="imgletter" Height="75px"
                                                                        Width="75px">
                                                                        <EmptyImage Height="75px" Url="~/Images/noimage.gif" Width="75px" />
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </dxp:ASPxPanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="استان" ID="lblOtherPr" Visible="False" Width="115px">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtOtherPr" Visible="False" Font-Bold="true"
                                                Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="شماره عضویت استان قبلی" ID="lblPrMeNo" Visible="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtPrMeNo" Font-Bold="true" Visible="false"
                                                Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="شماره پروانه" ID="lblPrFileNo" Visible="False"
                                                Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtPrFileNo" Visible="false" Font-Bold="true"
                                                Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="دلایل انتقال" ID="lblPrBody" Visible="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="3" align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="txtPrBody" Font-Bold="true" Visible="false"
                                                Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <br />
                            <fieldset runat="server" id="RoundPanelMemberLicence">
                                <legend class="fieldset-legend" dir="rtl">مدارک تحصیلی
                                </legend>
                                <TSPControls:CustomAspxDevGridView2 ID="GridViewMemberLicence" runat="server" Width="100%"
                                    OnHtmlRowPrepared="GridViewMemberLicence_HtmlRowPrepared"
                                    AutoGenerateColumns="False" ClientInstanceName="grid" OnAutoFilterCellEditorInitialize="GridViewMemberLicence_AutoFilterCellEditorInitialize"
                                    OnHtmlDataCellPrepared="GridViewMemberLicence_HtmlDataCellPrepared">
                                    <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
                                    <Columns>
                                        <dxwgv:GridViewDataImageColumn FieldName="ImageURL" Caption="تصویر" VisibleIndex="0"
                                            Width="150px">
                                            <EditCellStyle Wrap="False">
                                            </EditCellStyle>
                                            <PropertiesImage ImageHeight="150px" ImageWidth="150px">
                                            </PropertiesImage>
                                        </dxwgv:GridViewDataImageColumn>

                                        <dxwgv:GridViewDataImageColumn FieldName="InquiryImageURL" Caption="تصویر استعلام" VisibleIndex="0"
                                            Width="150px">
                                            <EditCellStyle Wrap="False">
                                            </EditCellStyle>
                                            <PropertiesImage ImageHeight="150px" ImageWidth="150px">
                                            </PropertiesImage>
                                        </dxwgv:GridViewDataImageColumn>

                                        <dxwgv:GridViewDataCheckColumn VisibleIndex="0" FieldName="DefaultValue" Width="60px"
                                            Caption="پیش فرض">
                                        </dxwgv:GridViewDataCheckColumn>
                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="MlId" Name="MlId">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="LiName" Width="150px" Caption="مقطع">
                                            <CellStyle Wrap="true">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjName" Width="150px" Caption="رشته">
                                            <CellStyle Wrap="true">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjCode" Width="80px" Caption="کد پروانه">
                                            <CellStyle Wrap="true" HorizontalAlign="Center">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="UnName" Width="200px" Caption="دانشگاه">
                                            <CellStyle Wrap="true">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="CitName"
                                            Caption="شهر">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="Avg" Width="60px" Caption="معدل">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="EndDate" Caption="تاریخ فارغ التحصیلی"
                                            Name="EndDate" Width="110px">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="Inquiry" Caption="استعلام">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="confirm" Caption="نوع تأیید">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="InActiveName" Width="60px"
                                            Caption="وضعیت">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="9" FieldName="MReId"
                                            Name="MReId">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewCommandColumn VisibleIndex="9" Width="30px" Caption=" " ShowClearFilterButton="true">
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="10" FieldName="MeId">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowHorizontalScrollBar="True"></Settings>
                                </TSPControls:CustomAspxDevGridView2>
                            </fieldset>
                            <%--  <TSPControls:CustomASPxRoundPanel ID="RoundPanelMemberLicence" HeaderText="مدارک تحصیلی"
                                runat="server" Width="100%">
                                <PanelCollection>
                                    <dxp:PanelContent>
                                       
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </TSPControls:CustomASPxRoundPanel>--%>

                            <fieldset runat="server" id="ASPxRoundPanelAccounting">
                                <legend class="fieldset-legend" dir="rtl">ثبت فیش
                                </legend>
                                <asp:Panel ID="PanelAccountingInserting" runat="server">
                                    <div align="center">
                                        <table id="tableAccounting" dir="rtl" runat="server" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td colspan="4" valign="top" align="center">
                                                        <ul class="HelpUL">
                                                            پیش از تایید فیش و ارسال پرونده عضو به مراحل بعدی گردش کار صحت تاریخ پرداخت به بانک
                                                        را بررسی نمایید.
                                                        </ul>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" valign="top" align="center">
                                                        <dxe:ASPxLabel runat="server" ID="lblRegEnter" ClientInstanceName="lblRegEnter" ForeColor="Blue">
                                                        </dxe:ASPxLabel>
                                                        <dxe:ASPxLabel runat="server" ID="lblReg" ClientInstanceName="lblReg" ClientVisible="false"
                                                            ForeColor="Blue">
                                                        </dxe:ASPxLabel>
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="top" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="نحوه پرداخت" ID="ASPxLabel4">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td align="right" dir="ltr" valign="top" width="35%">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                            ID="cmbaType" ValueType="System.String" SelectedIndex="0" ClientInstanceName="aType"
                                                            RightToLeft="True">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {    
	if(aType.GetValue()=='1')
		SetFiche();
	else
    {
	    if(aType.GetValue()=='3')
        	SetPose();
        else
            SetEpayment();
    }
}"></ClientSideEvents>
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" ErrorText="نوع را انتخاب نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <Items>
                                                                <dxe:ListEditItem Value="1" Text="فیش" Selected="True"></dxe:ListEditItem>
                                                                <dxe:ListEditItem Value="3" Text="دستگاه کارت خوان"></dxe:ListEditItem>
                                                                <dxe:ListEditItem Value="4" Text="پرداخت الکترونیکی"></dxe:ListEditItem>
                                                            </Items>
                                                            <ButtonStyle Width="13px">
                                                            </ButtonStyle>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                    <td width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ ثبت" ID="ASPxLabel5">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td width="35%">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtCreateDate" Width="100%"
                                                            ClientInstanceName="txtCreateDate">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right" style="width: 15%">
                                                        <dxe:ASPxLabel runat="server" Text="بابت" ID="ASPxLabel3">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" style="width: 35%">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbAccType"
                                                            RightToLeft="True" ValueType="System.Int32"
                                                            DataSourceID="ObjectDataSourceAccType"
                                                            TextField="TypeName" ValueField="AccTypeId" ClientInstanceName="cmbAccType" AutoPostBack="false">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ValidationSettings Display="Dynamic">
                                                                <RequiredField ErrorText=""></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <ClientSideEvents SelectedIndexChanged="function(s,e){CallBackPage.PerformCallback('AccTypeChange');}" />
                                                        </TSPControls:CustomAspxComboBox>
                                                        <asp:ObjectDataSource ID="ObjectDataSourceAccType" runat="server" SelectMethod="GetData"
                                                            TypeName="TSP.DataManager.TechnicalServices.AccTypeManager"></asp:ObjectDataSource>
                                                    </td>
                                                    <td valign="top" align="right" style="width: 15%">
                                                        <dxe:ASPxLabel runat="server" Text="مبلغ" ID="ASPxLabel33">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td dir="ltr" valign="top" align="right" style="width: 35%">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtaAmount" Width="100%"
                                                            ClientInstanceName="txtaAmount" ReadOnly="true">
                                                            <ValidationSettings Display="Dynamic" ValidationGroup="Acc" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" ErrorText="مبلغ را وارد نمایید"></RequiredField>
                                                                <RegularExpression ErrorText="نامعتبر" ValidationExpression="[1-9]\d*"></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="شماره فیش" ID="lblFishNo" ClientInstanceName="lblFishNo">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtaNumber" Width="100%"
                                                            Style="direction: ltr" ClientInstanceName="txtaNumber">
                                                            <ValidationSettings Display="Dynamic" ValidationGroup="Acc" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" ErrorText="شماره را وارد نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ پرداخت" ID="ASPxLabel32">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                            Width="220px" ShowPickerOnTop="True" ValidationGroup="Acc" ID="txtaDate" PickerDirection="ToRight"
                                                            RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                            ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtaDate" ValidationGroup="Acc"
                                                            ID="PersianDateValidator2">تاریخ نامعتبر</pdc:PersianDateValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="وضعیت پرداخت" ID="ASPxLabel1">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtStatus" Width="100%"
                                                            ClientEnabled="false">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel25">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="50px" ID="txtaDesc" Width="100%"
                                                            ClientInstanceName="txtaDesc">
                                                            <ValidationSettings>
                                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>


                                            </tbody>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </fieldset>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />

                <TSPControls:CustomASPxRoundPanelMenu ID="ASPxRoundPanel1" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server" EnableTheming="False"
                                            EnableViewState="False" AutoPostBack="true" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/save.png" />                                            
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                            CausesValidation="False" ID="btnPrint2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s,e){
                                                                    CallBackPage.PerformCallback('Print');
                                                                    }" />

                                            <Image Url="~/Images/TS/printorange.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="تایید پرداخت فیش"
                                            ID="btnPaymentConfirm2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnPaymentConfirm_Click"
                                            AutoPostBack="true">
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 } 
}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/PaymentConfirm.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="عدم تایید پرداخت فیش"
                                            ID="btnPaymentReject2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                             AutoPostBack="true" OnClick="btnPaymentReject_Click">
                                            <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/PaymentReject.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep2" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {                                                          
 ShowWf();

}" />

                                            <Image Url="~/Images/icons/reload.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                            ToolTip="بازگشت" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/Back.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت اعضا"
                                            CausesValidation="False" ID="btnBackToManagment2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBackToManagment_Click">

                                            <Image Url="../../Images/icons/BakToManagment.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <dxhf:ASPxHiddenField ID="HiddenFieldPage" ClientInstanceName="HiddenFieldPage" runat="server">
                </dxhf:ASPxHiddenField>
                <asp:ObjectDataSource ID="OdbCity" runat="server" CacheDuration="30" SelectMethod="GetData"
                    TypeName="TSP.DataManager.CityManager"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="OdbAgent" runat="server" CacheDuration="30" SelectMethod="GetData"
                    TypeName="TSP.DataManager.AccountingAgentManager"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODbAtType" runat="server" CacheDuration="30" EnableCaching="True"
                    SelectMethod="GetData" TypeName="TSP.DataManager.ActivityTypeManager"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODBMsId" runat="server" CacheDuration="30" EnableCaching="True"
                    SelectMethod="GetData" TypeName="TSP.DataManager.MemberStatusManager"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="OdbProvince" runat="server" TypeName="TSP.DataManager.ProvinceManager"
                    SelectMethod="GetData" CacheDuration="30" FilterExpression="NezamCode<>{0}">
                    <FilterParameters>
                        <asp:Parameter Name="newparameter" />
                    </FilterParameters>
                </asp:ObjectDataSource>
                <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="grid" SessionName="SendBackDataTable_MeConf"
                    OnCallback="WFUserControl_Callback" />
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomAspxCallbackPanel>
</asp:Content>
