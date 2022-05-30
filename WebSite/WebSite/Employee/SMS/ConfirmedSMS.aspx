<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="ConfirmedSMS.aspx.cs" Inherits="Employee_SMS_ConfirmedSMS" Title="مدیریت پیام های کوتاه" %>

<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxt" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
                    document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
                    document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
                }

                function HideSendSMSPanels() {
                    PanelConfirmSendSMS.SetVisible(false);
                    PanelConfirmDeliveryReport.SetVisible(false);
                    PanelSendFinish.SetVisible(false);
                }

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

    <div align="right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
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
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                    ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="BtnNew_Click">
                                   
                                    <Image  Url="~/Images/icons/new.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                     ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnEdit_Click">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewOutBox.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                   
                                    <Image  Url="~/Images/icons/edit.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                     ID="btnView" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnView_Click" CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewOutBox.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                 
                                    <Image  Url="~/Images/icons/view.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال"
                                     ID="ASPxButton1" AutoPostBack="False" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False" OnClick="btnInActive_Click">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewOutBox.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
else
{
 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}
}" />
                                   
                                    <Image  Url="~/Images/icons/disactive.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گیرندگان پیام کوتاه"
                                     ID="btnSMSRecievers" AutoPostBack="False" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False" OnClick="btnSMSRecievers_Click"
                                    CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewOutBox.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                  
                                    <Image  Url="~/Images/Icons/SMSReciever.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گردش کار"
                                    ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewOutBox.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
	ShowWf();
}
}"></ClientSideEvents>
                                  
                                    <Image  Url="~/Images/icons/reload.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing" runat="server" AutoPostBack="False" 
                                    EnableTheming="False" EnableViewState="False" OnClick="btnTracing_Click" Text=" "
                                    ToolTip="پیگیری" UseSubmitBehavior="False" CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
            
	if (GridViewOutBox.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />
                                   
                                    <Image  Url="~/Images/icons/Cheque Status ReChange.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendSMS" runat="server"  EnableTheming="False"
                                    EnableViewState="False" Text=" " ToolTip="ارسال پیام کوتاه" UseSubmitBehavior="False"
                                    CausesValidation="False" OnClick="btnSendSMS_Click">
                                    <ClientSideEvents Click="function(s, e) {
   
	if (GridViewOutBox.GetFocusedRowIndex()&lt;0)
                                       {
        e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
                                        }
 
}" />
                                   
                                    <Image  Url="~/Images/icons/SendSMS.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator7">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False" 
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                    UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                    <ClientSideEvents Click="function(s,e){  }" />
                                   
                                    <Image  Url="~/Images/icons/ExportExcel.png"  />
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
                        <td align="right" valign="top" width="15%">عنوان
                        </td>
                        <td  colspan="3" align="right" valign="top" >
                             <TSPControls:CustomTextBox ID="txtSmsSubject" runat="server" 
                                 Width="100%" ClientInstanceName="txtSmsSubject">
                            </TSPControls:CustomTextBox>
                        </td>
                    
                    </tr>
                    <tr>
                        <td align="right" valign="top" width="15%">تاریخ ثبت از
                        </td>
                        <td align="right" valign="top" width="35%">
                            <pdc:PersianDateTextBox ID="txtStartDate" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" ShowPickerOnEvent="OnClick" ShowPickerOnTop="True"
                                Width="300px" Style="direction: ltr;" RightToLeft="False"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right" valign="top" width="15%">تاریخ ثبت تا
                        </td>
                        <td align="right" valign="top" width="35%">
                            <pdc:PersianDateTextBox ID="txtEndDate" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" ShowPickerOnTop="True" ShowPickerOnEvent="OnClick"
                                Width="300px" Style="direction: ltr;" RightToLeft="False"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" width="15%">شروع اعتبار از
                        </td>
                        <td align="right" valign="top" width="35%">
                            <pdc:PersianDateTextBox ID="txtSMSDotoDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" ShowPickerOnEvent="OnClick" ShowPickerOnTop="True"
                                Width="300px" Style="direction: ltr;" RightToLeft="False"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right" valign="top" width="15%">شروع اعتبار تا
                        </td>
                        <td align="right" valign="top" width="35%">
                            <pdc:PersianDateTextBox ID="txtSMSDotoDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" ShowPickerOnTop="True" ShowPickerOnEvent="OnClick"
                                Width="300px" Style="direction: ltr;" RightToLeft="False"></pdc:PersianDateTextBox>
                        </td>
                    </tr>


                    <tr>
                        <td align="right" valign="top" width="15%">پایان اعتبار از
                        </td>
                        <td align="right" valign="top" width="35%">
                            <pdc:PersianDateTextBox ID="txtExpireDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" ShowPickerOnEvent="OnClick" ShowPickerOnTop="True"
                                Width="300px" Style="direction: ltr;" RightToLeft="False"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right" valign="top" width="15%">پایان اعتبار تا
                        </td>
                        <td align="right" valign="top" width="35%">
                            <pdc:PersianDateTextBox ID="txtExpireDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" ShowPickerOnTop="True" ShowPickerOnEvent="OnClick"
                                Width="300px" Style="direction: ltr;" RightToLeft="False"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="vertical-align: top">نوع پیام
                        </td>
                        <td align="right" style="vertical-align: top">
                            <TSPControls:CustomAspxComboBox ID="cmbSMSType" runat="server" 
                                 DataSourceID="ObjdsSmsType" ClientInstanceName="cmbSMSType"
                                 TextField="SmsTypeName" ValueField="SmsTypeId"
                                RightToLeft="True" ValueType="System.String" Width="100%">
                                <ItemStyle HorizontalAlign="Right" />
                                <ValidationSettings>
                                    
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td align="right" style="vertical-align: top">زبان
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxComboBox ID="cmbLanguage" ClientInstanceName="cmbLanguage" runat="server"
                                  
                                RightToLeft="True" ValueType="System.String" Width="100%">
                                <ItemStyle HorizontalAlign="Right" />
                                <Items>
                                    <dxe:ListEditItem Text="انگلیسی" Value="0" />
                                    <dxe:ListEditItem Text="فارسی" Value="1" />
                                </Items>
                                <ValidationSettings>
                                    
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="vertical-align: top">کد عضویت گیرنده
                        </td>
                        <td align="right" style="vertical-align: top" dir="rtl">
                            <TSPControls:CustomTextBox ID="txtRecieverId" runat="server" 
                                 Width="100%" ClientInstanceName="txtRecieverId">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RegularExpression ErrorText="کد عضویت را با فرمت صحیح وارد نمایید" ValidationExpression="\d*" />
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right" style="vertical-align: top">شماره همراه گیرنده
                        </td>
                        <td align="right" style="vertical-align: top" dir="rtl">
                            <TSPControls:CustomTextBox ID="txtRecieverMobile" ClientInstanceName="txtRecieverMobile" runat="server"
                                  MaxLength="11"
                                Width="100%">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                    <RegularExpression ErrorText="شماره همراه را با فرمت صحیح وارد نمایید" ValidationExpression="\d{11}" />
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" style="vertical-align: top">
                            <TSPControls:CustomAspxButton  ID="btnSearch" runat="server" 
                                 Text="جستجو" Width="100px" AutoPostBack="true" OnClick="btnSearch_OnClick">
                                <ClientSideEvents Click="function(s, e) {
 e.processOnServer=false;
	   if(CheckSearch()==0)
        {
          alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.'); 
          return; 
        }
        else
         e.processOnServer=true;
}" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="right" colspan="2" style="vertical-align: top">
                            <TSPControls:CustomAspxButton ID="btnClear" runat="server" AutoPostBack="true" OnClick="btnSearch_OnClick" 
                                 Width="100px" Text="پاک کردن فرم" UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {	

   	 ClearSearch(); 
}"></ClientSideEvents>
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <dx:ASPxGridViewExporter ID="GridViewExporter" GridViewID="GridViewOutBox" runat="server">
    </dx:ASPxGridViewExporter>
    <TSPControls:CustomAspxDevGridView ID="GridViewOutBox" runat="server" DataSourceID="objdsSMS"
        Width="100%"
        KeyFieldName="SmsId" AutoGenerateColumns="False" OnCustomCallback="GridViewOutBox_CustomCallback"
        ClientInstanceName="GridViewOutBox" OnHtmlRowPrepared="GridViewOutBox_HtmlRowPrepared"
        OnHtmlDataCellPrepared="GridViewOutBox_HtmlDataCellPrepared">
        <TotalSummary>
            <dxwgv:ASPxSummaryItem FieldName="TotalCost" SummaryType="Sum" />
        </TotalSummary>
        <SettingsCookies Enabled="false" />
        <Columns>
            <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                VisibleIndex="0">
                <DataItemTemplate>
                    <div align="center">
                        <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl="~/Images/WFStart.png">
                        </dxe:ASPxImage>
                    </div>
                </DataItemTemplate>
                <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                    ValueType="System.String">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataTextColumn Caption="عنوان" FieldName="SmsSubject" VisibleIndex="0"
                Width="200px">
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="وضعیت ارسال" FieldName="DeliverReport" VisibleIndex="0">
                <HeaderStyle Wrap="True" />
                <CellStyle HorizontalAlign="Right" Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ" FieldName="SMSDate" VisibleIndex="1">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شروع اعتبار" FieldName="SMSDotoDate" VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پایان اعتبار" FieldName="ExpireDate" VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="زمان" FieldName="SMSTime" Width="70px" VisibleIndex="3">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="بخش" FieldName="PartName" Width="70px" VisibleIndex="3">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="ثبت کننده" FieldName="SaverName" VisibleIndex="3">
                <HeaderStyle Wrap="True" />
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="سمت ثبت کننده" Width="150px" FieldName="SaverNmcName"
                VisibleIndex="3">
                <HeaderStyle Wrap="True" />
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>


            <dxwgv:GridViewDataTextColumn Caption="ارسال کننده" FieldName="SenderName" VisibleIndex="3">
                <HeaderStyle Wrap="True" />
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="نوع پیام" FieldName="SmsTypeName" VisibleIndex="5">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="زبان" FieldName="Languages" VisibleIndex="6">
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تعداد گیرندگان" FieldName="RecieverCount"
                Visible="False" VisibleIndex="6">
                <HeaderStyle Wrap="True" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تعداد پیام" FieldName="SmsCount" VisibleIndex="7">
                <HeaderStyle Wrap="True" HorizontalAlign="Center" />
            </dxwgv:GridViewDataTextColumn>          
            <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveState" VisibleIndex="10">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="12" Width="50px" ShowClearFilterButton="true">
       
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowHorizontalScrollBar="True" ShowFooter="true"></Settings>
    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table dir="rtl">
                    <tbody>
                        <tr>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                    ID="BtnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="BtnNew_Click">                                   
                                    <Image  Url="~/Images/icons/new.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                     ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnEdit_Click">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewOutBox.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                    
                                    <Image  Url="~/Images/icons/edit.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                     ID="btnView2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnView_Click" CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewOutBox.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                   
                                    <Image  Url="~/Images/icons/view.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال"
                                     ID="btnInActive" AutoPostBack="False" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False" OnClick="btnInActive_Click">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewOutBox.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
else
{
 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}
}" />
                                    
                                    <Image  Url="~/Images/icons/disactive.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گیرندگان پیام کوتاه"
                                     ID="btnSMSRecievers2" AutoPostBack="False" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False" OnClick="btnSMSRecievers_Click"
                                    CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewOutBox.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                    
                                    <Image  Url="~/Images/Icons/SMSReciever.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گردش کار"
                                    ID="btnSendToNexStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewOutBox.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
	ShowWf();
}
}"></ClientSideEvents>
                                   
                                    <Image  Url="~/Images/icons/reload.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing2" runat="server" AutoPostBack="False" 
                                    EnableTheming="False" EnableViewState="False" OnClick="btnTracing_Click" Text=" "
                                    ToolTip="پیگیری" UseSubmitBehavior="False" CausesValidation="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewOutBox.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />
                                   
                                    <Image  Url="~/Images/icons/Cheque Status ReChange.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendSMS2" runat="server"  EnableTheming="False"
                                    EnableViewState="False" Text=" " ToolTip="ارسال پیام کوتاه" UseSubmitBehavior="False"
                                    CausesValidation="False" OnClick="btnSendSMS_Click">
                                    <ClientSideEvents Click="function(s, e) {
            
	if (GridViewOutBox.GetFocusedRowIndex()&lt;0)
                                     {
        e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
                                        }

}" />
                                   
                                    <Image  Url="~/Images/icons/SendSMS.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td  align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False" 
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                    UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                    <ClientSideEvents Click="function(s,e){  }" />
                                  
                                    <Image  Url="~/Images/icons/ExportExcel.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkId"
        TypeName="TSP.DataManager.WorkFlowTaskManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="WorkFlowId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="TaskOrder" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewOutBox" SessionName="SendBackDataTable_SMSConf"
        OnCallback="WFUserControl_Callback" GridHasCallback="true" />
    <asp:ObjectDataSource ID="objdsSMS" runat="server" SelectMethod="SearchSMS" TypeName="TSP.DataManager.SmsManager"
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="SaverEmpId"></asp:Parameter>            
            <asp:Parameter DefaultValue="9999/99/99" Name="SMSStartDate" Type="String" />
            <asp:Parameter DefaultValue="9999/99/99" Name="SMSEndDate" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="RecieverId" Type="Int32" />
            <asp:Parameter DefaultValue="%" Name="RecieverCellPhone" Type="String" /> 
             <asp:Parameter DefaultValue="%" Name="SmsSubject" Type="String" />
            <asp:Parameter DefaultValue="9999/99/99" Name="ExpireDateFrom" Type="String" />
            <asp:Parameter DefaultValue="9999/99/99" Name="ExpireDateTo" Type="String" />
            <asp:Parameter DefaultValue="9999/99/99" Name="SMSDotoDateTo" Type="String" />
            <asp:Parameter DefaultValue="9999/99/99" Name="SMSDotoDateFrom" Type="String" />
            <asp:Parameter DefaultValue="-1" Name="IsFarsi" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="SmsTypeId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsSmsType" runat="server" SelectMethod="SelectByLastModified"
        TypeName="TSP.DataManager.SmsTypeModifiedManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="SmstypeId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
   
</asp:Content>
