<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="SMSInbox.aspx.cs" Inherits="Employee_SMS_SMSInbox" Title="پیام های دریافت شده" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
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
    <div align="right" id="DivReport" class="DivErrors" runat="server" dir="rtl">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False" 
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                    UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">

                                   
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
                        <td align="right" valign="top" width="15%">تاریخ دریافت از
                        </td>
                        <td align="right" valign="top" width="35%">
                            <pdc:PersianDateTextBox ID="txtSMSDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" ShowPickerOnEvent="OnClick" ShowPickerOnTop="True"
                                Width="256px" Style="direction: ltr;" RightToLeft="False"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right" valign="top" width="15%">تاریخ دریافت تا
                        </td>
                        <td align="right" valign="top" width="35%">
                            <pdc:PersianDateTextBox ID="txtSMSDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" ShowPickerOnTop="True" ShowPickerOnEvent="OnClick"
                                Width="256px" Style="direction: ltr;" RightToLeft="False"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" style="vertical-align: top">
                            <TSPControls:CustomAspxButton ID="btnSearch" runat="server" 
                                 Text="جستجو" Width="100px" AutoPostBack="true" OnClick="btnSearch_Click">
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
                            <TSPControls:CustomAspxButton ID="btnClear" runat="server" AutoPostBack="true" OnClick="btnSearch_Click" 
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
    <TSPControls:CustomAspxButton ID="ASPxButton1" runat="server" OnClick="ASPxButton1_Click" Text="ASPxButton"
        Visible="false">
    </TSPControls:CustomAspxButton>
    <br />
    <dx:ASPxGridViewExporter ID="GridViewExporter" GridViewID="GridViewInbox" runat="server">
    </dx:ASPxGridViewExporter>
    <TSPControls:CustomAspxDevGridView ID="GridViewInbox" runat="server" DataSourceID="objdsSMS"
        Width="100%" KeyFieldName="SmsId" AutoGenerateColumns="False">
        <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowHorizontalScrollBar="True"
            ShowFilterRowMenu="True"></Settings>
        <Columns>
            <dxwgv:GridViewDataTextColumn Caption="ارسال کننده" FieldName="SenderName" VisibleIndex="0">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="SenderId" VisibleIndex="1">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره همراه" FieldName="MobileNo" VisibleIndex="2">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="SMSDate" Caption="تاریخ">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="SMSTime" Caption="زمان">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="SmsBody" Caption="عنوان" Width="120px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <%--   <dxwgv:GridViewDataColumn VisibleIndex="6" FieldName="SmsBody" Caption="متن">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataColumn>--%>

            <%--  <dxwgv:GridViewDataTextColumn Caption="متن" FieldName="SmsBody" VisibleIndex="6"
                    Width="300px">
                </dxwgv:GridViewDataTextColumn>--%>
            <%--  <dxwgv:GridViewCommandColumn VisibleIndex="7" Caption=" " Width="30px">
                    <ClearFilterButton Visible="True">
                    </ClearFilterButton>
                </dxwgv:GridViewCommandColumn>--%>
        </Columns>

    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table >
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False" 
                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                    UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                    <Image  Url="~/Images/icons/ExportExcel.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:ObjectDataSource ID="objdsSMS" runat="server" SelectMethod="ReceivedSMS" TypeName="TSP.DataManager.SmsManager"
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DbType="String" Name="SMSDateFrom" DefaultValue="1" />
            <asp:Parameter DbType="String" Name="SMSDateTo" DefaultValue="2" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
