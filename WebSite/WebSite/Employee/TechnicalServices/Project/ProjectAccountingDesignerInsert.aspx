<%@ Page Title="مشخصات مالی طراح پروژه" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ProjectAccountingDesignerInsert.aspx.cs" Inherits="Employee_TechnicalServices_Project_ProjectAccountingDesignerInsert" %>



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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Src="../../../UserControl/WFUserControl.ascx" TagName="WFUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">

        function SetFiche() {
            lblBank.SetVisible(false);
            lblBranchCode.SetVisible(false);
            lblBranchName.SetVisible(false);
            Bank.SetVisible(false);
            BranchCode.SetVisible(false);
            BranchName.SetVisible(false);
            btnPos.SetVisible(false);
            btnPos2.SetVisible(false);
            txtPCPosResponce.SetVisible(false);
            lblFishNo.SetText('شماره فیش');
            txtaNumber.SetEnabled(true);
            txtaAmount.SetEnabled(false)
            txtaAmount.SetText(HiddenFieldAcc.Get('Price'));
        }
        function SetCheque() {
            lblBank.SetVisible(true);
            lblBranchCode.SetVisible(true);
            lblBranchName.SetVisible(true);
            Bank.SetVisible(true);
            BranchCode.SetVisible(true);
            BranchName.SetVisible(true);
            btnPos.SetVisible(false);
            btnPos2.SetVisible(false);
            lblPCPosResponce.SetVisible(false);
            txtPCPosResponce.SetVisible(false);
            lblFishNo.SetText('شماره چک');
            txtaNumber.SetEnabled(true);
            txtaAmount.SetEnabled(true);
        }

        function SetPose() {
            lblBank.SetVisible(false);
            lblBranchCode.SetVisible(false);
            lblBranchName.SetVisible(false);
            Bank.SetVisible(false);
            BranchCode.SetVisible(false);
            BranchName.SetVisible(false);
            btnPos.SetVisible(true);
            btnPos2.SetVisible(true);

            txtPCPosResponce.SetVisible(true);
            lblFishNo.SetText('شناسه واریز');
            txtaNumber.SetEnabled(false);
            txtaAmount.SetEnabled(false);
            txtaAmount.SetText(HiddenFieldAcc.Get('Price'));
        }

        function SetEpay() {
            lblBank.SetVisible(false);
            lblBranchCode.SetVisible(false);
            lblBranchName.SetVisible(false);
            Bank.SetVisible(false);
            BranchCode.SetVisible(false);
            BranchName.SetVisible(false);
            btnPos.SetVisible(false);
            btnPos2.SetVisible(false);

            txtPCPosResponce.SetVisible(false);
            lblFishNo.SetText('شناسه پیگیری');
            txtaNumber.SetEnabled(false);
            if (parseInt(HiddenFieldAcc.Get('Price')) < 500000000) {
                txtaAmount.SetEnabled(false);
                txtaAmount.SetText(HiddenFieldAcc.Get('Price'));
            }
            else
                txtaAmount.SetEnabled(true);
        }
        function SetTaskOrderError(result) {
            if (result != null) {

                document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
                document.getElementById("<%=DivReport.ClientID%>").style.display = 'block';  //='visible';
                document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = result;
            }
        }
        //************Observer List******************//
        var textSeparatorObserver = ";";
        function UpdateTextObserver() {
            var selectedItems = ListBoxGroupObserver.GetSelectedItems();
            cmbObserver.SetText(GetSelectedItemsTextObserver(selectedItems));
        }

        function GetSelectedItemsTextObserver(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                texts.push(items[i].text);
            return texts.join(textSeparatorObserver);
        }
        //*******************************************//
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
            IsAllSelected(listBox, indexAll, ItemsDifferentFromOther) ? cmbObserver.SetText('<همه>') : DropDown.SetText(GetSelectedItemsText(selectedItems, indexAll));
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
        //******************************//


        function saleByPaymentId() {
            var PcPosType = HiddenFieldAcc.Get("PcPosType").toString();
            var PcPosJSON = "";
            if (PcPosType == "1") {
                PcPosJSON = PcPosJSONCon;
            }
            else if (PcPosType == "2") {
                PcPosJSON = PcPosJSONCon2;
            }
            console.log(PcPosJSON);

            $.ajax({

                type: "POST",
                contentType: "application/json",
                data: null,
                url: "http://localhost:50000/SerialServiceHost/InitiateService1",

                dataType: 'json',
                data: PcPosJSON,
                success: function (result) {
                    console.log("ok");
                }
                , error: function (xhr, ajaxOptions, thrownError) {
                    console.log(xhr.status);
                    console.log(thrownError);
                }
            });
            var salesJSON = '{ "Amount":"' + txtaAmount.GetValue() + '", "PaymentId":"' + HiddenFieldAcc.Get("PaymentIdPOS").toString() + '"}';

            console.log(salesJSON);
            $.ajax({
                type: "POST",
                contentType: "application/json",

                url: "http://localhost:50000/SerialServiceHost/SaleWithPaymentId",
                dataType: 'json',
                data: salesJSON,
                crossDomain: true,
                success: function (result) {

                    getResult(result);
                }
                , error: function (xhr, ajaxOptions, thrownError) {
                    console.log(xhr.status);
                    console.log(thrownError);
                }

            });
        }
        function getResult(xmlParam) {

            //var RespCode = 'RespCode:'+ $(xmlParam).find('RespCode').text();
            //var SerialNo = ',SerialNo:'+$(xmlParam).find('SerialNo').text();
            //var TransactionDate =',TransactionDate:'+ $(xmlParam).find('TransactionDate').text();
            //var TransactionTime =',TransactionTime:' +$(xmlParam).find('TransactionTime').text();
            //var Amount =',Amount:'+ $(xmlParam).find('Amount').text();
            //var TerminalId =',TerminalId:'+ $(xmlParam).find('TerminalId').text();
            //var TraceNo = ',TraceNo:'+$(xmlParam).find('TraceNo').text();
            //var Pan = ',Pan:' + $(xmlParam).find('Pan').text();

            console.log(xmlParam);
            if ($(xmlParam).find('RespCode').text() == "00") {
                txtaNumber.SetText($(xmlParam).find('TraceNo').text());
                document.getElementById("<%= txtPaymentDate.ClientID %>").value = $(xmlParam).find('TransactionDate').text();
            }
            else
                SetTaskOrderError("تراکنش با دستگاه ناموفق بود");


        }

    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayHeight="15" CalendarDayWidth="33" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0">
                            <tr>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                        CausesValidation="False" ID="btnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                        EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/new.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                        CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/edit.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                        ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        OnClick="btnSave_Click" ValidationGroup="Acc">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/save.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره فیش پرداخت شده"
                                        ID="btnSavePayed2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        OnClick="btnSavePayed_Click" ValidationGroup="Acc">
                                        <ClientSideEvents Click="function(s,e){ e.processOnServer= confirm('پس از ثبت فیش با وضعیت پرداخت شده امکان ویرایش آن وجود ندارد.آیا مطمئن به ثبت فیش به صورت پرداخت شده می باشید؟');}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/AutoFishPayment.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="پرداخت با دستگاه"
                                        ID="btnPos" ClientInstanceName="btnPos" ClientVisible="false" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" AutoPostBack="false" ClientSideEvents-Click="saleByPaymentId">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/pos.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center" valign="top">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " AutoPostBack="false"
                                        ToolTip="چاپ فیش" ID="btnPrintFish" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                        EnableViewState="False" EnableTheming="False">
                                        <ClientSideEvents Click="function(s, e) {
  CallbackPanelMain.PerformCallback('PrintFish'); 
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/printers.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>

                                <td width="10px" align="center" valign="top">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار پروژه"
                                        ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
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
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                        CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnBack_Click">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/Back.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <div align="center">
                <table dir="rtl" width="100%">
                    <td width="5%" align="left">
                        <blink id="bkImgWarningMsg"><dxe:ASPxImage ID="ImgWarningMsg" ClientVisible="false" Width="20px" Height="20px" runat="server" ImageUrl="~/Images/Errors-64.png">
                                    </dxe:ASPxImage></blink>
                    </td>
                    <td width="95%" align="right">
                        <asp:Label ID="lblWarning" Font-Bold="true" ForeColor="DarkRed" runat="server" Font-Size="11px"></asp:Label>
                    </td>
                </table>
            </div>
            <br />
            <TSP:ProjectInfo ID="prjInfo" runat="server"></TSP:ProjectInfo>
            <ul class="HelpUL">
                <li>در صورت مشخص نبودن تعرفه استفاده شده توسط ناظر/طراح مبلغ دستمزد صفر محاسبه خواهد
                    شد. </li>
                <li>در صورت پرداخت توسط دستگاه کارت خوان (POS) نحوه پرداخت را دستگاه کارت خوان انتخاب
                    نمایید. </li>
                <li>در هنگام استفاده از دکمه گردش کار <b>در این صفحه</b> در صورتی که شرایط ثبت فیش در
                    مرحله گردش کار انتخاب شده وجود نداشته باشد ، پس از کلیک برروی دکمه خروج در پنجره
                    گردش کار، مرورگر شمارا به صفحه مدیریت مالی پروژه هدایت می نماید.</li>
                <li>در هنگام محاسبه دستمزدها ،براساس نوع پروژه مبلغ محاسبه شده در درصد تخفیف برای پروژه
                    های خاص ضرب می گردد. </li>
                <li>در صورتی که قصد فیشی را دارید که پیش از ثبت آن در سیستم پرداخت آن انجام پذیرفته
                    است ، می توانید از دکمه ذخیره فیش پرداخت شده اقدام نمایید.توجه نمایید این دکمه جهت
                    ثبت فیش می باشد و در هنگام ویرایش و یا مشاهده فیش غیرفعال می باشد. </li>
            </ul>
            <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelMain" runat="server" ClientInstanceName="CallbackPanelMain"
                OnCallback="CallbackPanelMain_Callback" LoadingPanelImage-Url="../../../Image/indicator.gif">
                <ClientSideEvents EndCallback="function(e,s){
               
                if(CallbackPanelMain.cpPrintMsg!='')
                {
                    alert(CallbackPanelMain.cpPrintMsg);
                    CallbackPanelMain.cpPrintMsg='';
                }
                if(CallbackPanelMain.cpPrintFish == 1)
                {                 
                    CallbackPanelMain.cpPrintFish = 0;
                    window.open(CallbackPanelMain.cpPrintFishPath);
                    CallbackPanelMain.cpPrintFishPath='';
                }
                       if(CallbackPanelMain.cpPaymentIdPOS!='')
                    {
                      HiddenFieldAcc.Set('PaymentIdPOS',CallbackPanelMain.cpPaymentIdPOS)
                    }
    	        }" />
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent1" runat="server">
                        <TSPControls:CustomASPxRoundPanel ID="RoundPanelAccounting" ClientInstanceName="RoundPanelAccounting"
                            HeaderText="مشاهده" runat="server" Width="100%">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="4" align="center">
                                                <dxe:ASPxLabel runat="server" Font-Bold="true" Width="100%" Font-Size="10px" Text=""
                                                    ID="lblWarningPrice" ForeColor="DarkRed">
                                                </dxe:ASPxLabel>
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="center">
                                                <table dir="rtl" width="100%" runat="server" id="tblDesAndImp">
                                                    <tbody>
                                                        <tr>
                                                            <td valign="top" align="center">
                                                                <dxe:ASPxLabel runat="server" Text="کل مبلغ قابل پرداخت بر اساس متراژ دستمزد و تعرفه سال"
                                                                    ID="lblPriceDesc" ForeColor="Blue">
                                                                </dxe:ASPxLabel>
                                                                <dxe:ASPxLabel runat="server" ID="lblPrice" ForeColor="Blue">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="نوع نقشه" ID="lblPlanType" ClientVisible="true">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" width="35%">
                                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                    ID="cmbPlanType" ValueType="System.String" ClientInstanceName="cmbPlanType"
                                                    DataSourceID="ObjectDataSourcePlanType" RightToLeft="True" TextField="Title"
                                                    ValueField="PlansTypeId" ClientVisible="true" AutoPostBack="true" OnSelectedIndexChanged="cmbPlanType_SelectedIndexChanged">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Acc">
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="نوع نقشه را انتخاب نمایید"></RequiredField>
                                                    </ValidationSettings>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </TSPControls:CustomAspxComboBox>
                                                <asp:ObjectDataSource ID="ObjectDataSourcePlanType" runat="server" SelectMethod="SelectTSPlansTypeForAccounting"
                                                    TypeName="TSP.DataManager.TechnicalServices.PlansTypeManager">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="-1" Name="ProjectId" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                            <td align="right" valign="top" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="نام طراح" ID="lblDesigner" ClientVisible="true">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" width="35%">
                                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                    ID="cmbDesigner" ValueType="System.String" ClientInstanceName="cmbDesigner"
                                                    DataSourceID="ObjdDesigner" RightToLeft="True" TextField="DesignerMemberName"
                                                    ValueField="DesignerPlansId" ClientVisible="true" AutoPostBack="true" OnSelectedIndexChanged="cmbDesigner_SelectedIndexChanged">
                                                    <ClientSideEvents EndCallback="function(s,e){                                                        
                                                         if(cmbDesigner.cpPaymentIdPOS!='')
                                                        {
                                                            HiddenFieldAcc.Set('PaymentIdPOS',cmbDesigner.cpPaymentIdPOS)
                                                        }
                                                        }" />

                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Acc">                                                        
                                                        <RequiredField IsRequired="True" ErrorText="نام/نوع طراح را انتخاب نمایید"></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                                <asp:ObjectDataSource ID="ObjdDesigner" runat="server" SelectMethod="SelectTSDesignerPlansForDesignerFish"
                                                    TypeName="TSP.DataManager.TechnicalServices.Designer_PlansManager">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="-2" Name="ProjectId" Type="Int32" />
                                                        <asp:Parameter DefaultValue="-2" Name="PlansTypeId" Type="Int32" />
                                                        <asp:Parameter DefaultValue="-" Name="PlansTypeIdList" Type="String" />                                                        
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="متراژ دستمزد" ID="lblWage" ClientVisible="true">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server" Enabled="false" ID="txtWage" Width="100%"
                                                    ClientVisible="true">
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="تعرفه" ID="lblPriceArchive" ClientVisible="true">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server" Enabled="false" ID="txtPriceArchive"
                                                    Width="100%" ClientVisible="true">
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="نحوه پرداخت" ID="ASPxLabel27">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" dir="ltr" valign="top">
                                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                    ID="cmbPaymentType" ValueType="System.String" SelectedIndex="0" ClientInstanceName="cmbPaymentType"
                                                    RightToLeft="True">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {

	if(cmbPaymentType.GetValue()=='1')
		SetFiche();
	else
    {
		if(cmbPaymentType.GetValue()=='2')
        SetCheque();
        else
        {
            if(cmbPaymentType.GetValue()=='3')
            SetPose();
              else
                  { 
                     if(cmbPaymentType.GetValue()=='4')
                     SetEpay();
                  }
        }
    }
}"></ClientSideEvents>
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Acc">
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="نوع را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <Items>
                                                        <dxe:ListEditItem Value="1" Text="فیش" Selected="True"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Value="2" Text="چک"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Value="3" Text="دستگاه کارت خوان"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Value="4" Text="پرداخت الکترونیکی"></dxe:ListEditItem>
                                                    </Items>
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="بابت" ID="ASPxLabel1">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbAccType"
                                                    ValueType="System.Int32" SelectedIndex="0"
                                                    DataSourceID="ObjectDataSourceAccType"
                                                    TextField="TypeName" ValueField="AccTypeId" ClientInstanceName="cmbAccType">
                                                    <%--OnSelectedIndexChanged="cmbAccType_SelectedIndexChanged"
                                        AutoPostBack="true">--%>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ValidationSettings Display="Dynamic">
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <ClientSideEvents SelectedIndexChanged="function(s,e){CallbackPanelMain.PerformCallback('AccTypeChange');}" />
                                                    <Columns>
                                                        <dxe:ListBoxColumn Caption="عنوان" FieldName="TypeName" Width="200px" />
                                                        <dxe:ListBoxColumn Caption="کد معین/موضوع" FieldName="AccCode" />
                                                    </Columns>
                                                </TSPControls:CustomAspxComboBox>
                                                <asp:ObjectDataSource ID="ObjectDataSourceAccType" runat="server" SelectMethod="GetData"
                                                    TypeName="TSP.DataManager.TechnicalServices.AccTypeManager"></asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ" ID="ASPxLabel32">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                    Width="245px" ShowPickerOnTop="True" ValidationGroup="Acc" ID="txtaDate" PickerDirection="ToRight"
                                                    IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right" RightToLeft="False"></pdc:PersianDateTextBox>
                                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                    ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtaDate" ID="PersianDateValidator2"
                                                    ValidationGroup="Acc">تاریخ را انتخاب نمایید</pdc:PersianDateValidator>
                                            </td>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="شماره" ID="lblFishNo" ClientInstanceName="lblFishNo">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" dir="ltr">
                                                <TSPControls:CustomTextBox runat="server" ID="txtaNumber" ClientInstanceName="txtaNumber" Width="100%">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Acc">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="شماره را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="مبلغ(ريال)" ID="ASPxLabel33">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server" ID="txtaAmount" ClientInstanceName="txtaAmount" Width="100%"
                                                    ClientEnabled="false">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Acc">
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
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="بانک" ClientVisible="False" ID="ASPxLabelBank"
                                                    ClientInstanceName="lblBank">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server" ID="txtaBank" ClientVisible="False"
                                                    Width="100%" ClientInstanceName="Bank">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Acc">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="نام بانک را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="کد شعبه" ClientVisible="False" ID="ASPxLabelBranchCode"
                                                    ClientInstanceName="lblBranchCode">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server" ID="txtaBranchCode" ClientVisible="False"
                                                    Width="100%" ClientInstanceName="BranchCode">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Acc">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="کد شعبه را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="نام شعبه" ClientVisible="False" ID="ASPxLabelBranchName"
                                                    ClientInstanceName="lblBranchName">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server" ID="txtaBranchName" ClientVisible="False"
                                                    Width="100%" ClientInstanceName="BranchName">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Acc">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="نام شعبه را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ پرداخت" ID="ASPxLabel3">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                    Width="245px" ShowPickerOnTop="True" ID="txtPaymentDate" PickerDirection="ToRight"
                                                    IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right" RightToLeft="False"></pdc:PersianDateTextBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel6">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" colspan="3" valign="top">
                                                <TSPControls:CustomASPXMemo runat="server" Height="30px" ClientInstanceName="txtaDesc" ID="txtaDesc" Width="100%">
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
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="پاسخ کارت خوان" ID="lblPCPosResponce" ClientInstanceName="lblPCPosResponce" ClientVisible="false">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" colspan="3" valign="top">
                                                <TSPControls:CustomASPXMemo Height="30px" runat="server" Width="100%" Wrap="True" Text="" ClientInstanceName="txtPCPosResponce" ID="txtPCPosResponce" ClientVisible="false">
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </TSPControls:CustomASPxRoundPanel>
                        <dxhf:ASPxHiddenField ID="HiddenFieldObserver" runat="server">
                        </dxhf:ASPxHiddenField>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomAspxCallbackPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0">
                            <tr>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                        CausesValidation="False" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                        EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
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
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                        ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        OnClick="btnSave_Click" ValidationGroup="Acc">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/save.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره فیش پرداخت شده"
                                        ID="btnSavePayed" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        OnClick="btnSavePayed_Click" ValidationGroup="Acc">
                                        <ClientSideEvents Click="function(s,e){ e.processOnServer= confirm('پس از ثبت فیش با وضعیت پرداخت شده امکان ویرایش آن وجود ندارد.آیا مطمئن به ثبت فیش به صورت پرداخت شده می باشید؟');}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/AutoFishPayment.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="پرداخت با دستگاه"
                                        ID="btnPos2" ClientInstanceName="btnPos2" ClientVisible="false" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" AutoPostBack="false" ClientSideEvents-Click="saleByPaymentId">

                                        <HoverStyle BackColor="#FFE0C0">
                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/pos.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center" valign="top">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator"></TSPControls:MenuSeprator>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " AutoPostBack="false"
                                        ToolTip="چاپ فیش" ID="btnPrintFish2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                        EnableViewState="False" EnableTheming="False">
                                        <ClientSideEvents Click="function(s, e) {
  CallbackPanelMain.PerformCallback('PrintFish'); 
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/printers.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>

                                <td width="10px" align="center" valign="top">
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
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                        CausesValidation="False" ID="btnback2" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnBack_Click">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/Back.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:HiddenField ID="HDProjectId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="HDAccountingId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="RequestId" runat="server" Visible="False"></asp:HiddenField>
            <dxhf:ASPxHiddenField ID="HiddenFieldAcc" ClientInstanceName="HiddenFieldAcc" runat="server">
            </dxhf:ASPxHiddenField>
            <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="mygrid" SessionName="SendBackDataTable_EmpAccPrj"
                OnCallback="CallbackPanelWorkFlow_Callback" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                <img id="IMG1" src="../../../Image/indicator.gif" align="middle" />
                لطفا صبر نمایید ...
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
    <script> 
        var PcPosJSONCon = '{ "SerialNo":"' + HiddenFieldAcc.Get("SerialNoPcPos").toString() + '", "AcceptorId":"' + HiddenFieldAcc.Get("AcceptorIdPcPos").toString() + '","TerminalId":"' + HiddenFieldAcc.Get("TerminalIdPcPos").toString() + '","ComPort":"' + HiddenFieldAcc.Get("ComPortPcPos").toString() + '"}';
        var PcPosJSONCon2 = '{ "SerialNo":"' + HiddenFieldAcc.Get("SerialNoPcPos2").toString() + '", "AcceptorId":"' + HiddenFieldAcc.Get("AcceptorIdPcPos2").toString() + '","TerminalId":"' + HiddenFieldAcc.Get("TerminalIdPcPos2").toString() + '","ComPort":"' + HiddenFieldAcc.Get("ComPortPcPos2").toString() + '"}';
        console.log(PcPosJSONCon);
        console.log(PcPosJSONCon2);

    </script>
</asp:Content>

