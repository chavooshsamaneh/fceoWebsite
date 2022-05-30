<%@ Page Title="گزارش ناظران پروژه" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ProjectObserverReport.aspx.cs" Inherits="Employee_TechnicalServices_Report_ProjectObserverReport" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent1" runat="server">
                <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                    width="100%">
                    <tr>
                        <td align="right">
                            <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی اکسل"
                                            ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnExportExcel_Click">
                                            <Image Url="~/Images/icons/ExportExcel.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتخاب ستون ها"
                                                ID="btnChoosecolumn" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="False" Visible="true">
                                                <ClientSideEvents Click="function(s, e) {
	if(!GridViewObserverReport.IsCustomizationWindowVisible())
		GridViewObserverReport.ShowCustomizationWindow();
	else
		GridViewObserverReport.HideCustomizationWindow();
}" />

                                                <Image Height="25px" Url="~/Images/icons/cursor-hand.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
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
    <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table width="100%">
                    <tr>
                        <td align="right" valign="top">
                            <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="تاریخ کسرظرفیت از" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox ID="txtDateFromDecreased" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                Width="230px" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right" valign="top">
                            <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="تاریخ کسرظرفیت تا" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox ID="txtDateToDecreased" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                Width="230px" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right">
                            <asp:Label runat="server" Text="پلاک ثبتی" ID="Label5"></asp:Label>
                        </td>
                        <td valign="top" align="right">
                            <TSPControls:CustomTextBox runat="server" ID="txtRegNo" ClientInstanceName="txtRegNo">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                            </TSPControls:CustomTextBox>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="کد پروژه" ID="Label4"></asp:Label>
                        </td>
                        <td>
                            <TSPControls:CustomTextBox runat="server" ID="txtProjectId" ClientInstanceName="txtProjectId">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <RegularExpression ErrorText="کد پروژه را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="کد عضویت" ID="Label1"></asp:Label>
                        </td>
                        <td>
                            <TSPControls:CustomTextBox runat="server" ID="txtMeId" ClientInstanceName="txtMeId">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                            </TSPControls:CustomTextBox>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="شماره لیست حق الزحمه" ID="Label3"></asp:Label>
                        </td>
                        <td>
                            <TSPControls:CustomTextBox runat="server" ID="txtLisNo" ClientInstanceName="txtLisNo">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <br />
                            <table>
                                <tr>
                                    <td align="left">
                                        <TSPControls:CustomAspxButton ID="btnSearch" runat="server"
                                            Text="جستجو" ClientInstanceName="btnSearch" Width="98px" UseSubmitBehavior="true" OnClick="btnSearch_Onclick">
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
                                    <td align="right">
                                        <TSPControls:CustomAspxButton ID="ASPxButton3" runat="server" AutoPostBack="false"
                                            Text="پاک کردن فرم" Width="100px" UseSubmitBehavior="False" OnClick="btnSearch_Onclick">
                                            <ClientSideEvents Click="function(s, e) {	
	   	 ClearSearch();
}"></ClientSideEvents>
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
    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewObserverReport"
        ExportedRowType="All">
    </dxwgv:ASPxGridViewExporter>
    <TSPControls:CustomAspxDevGridView ID="GridViewObserverReport" KeyFieldName="ProjectObserversId"
        runat="server" DataSourceID="ObjdObserveReport" Width="100%" ClientInstanceName="GridViewObserverReport">
        <Settings ShowHorizontalScrollBar="true" ShowFooter="true" />
        <SettingsCustomizationWindow Enabled="True" />
        <TotalSummary>
            <dxwgv:ASPxSummaryItem FieldName="CapacityDecrement" SummaryType="Sum" />
            <dxwgv:ASPxSummaryItem FieldName="Wage" SummaryType="Sum" />
            <dxwgv:ASPxSummaryItem FieldName="ObserverInsurancePrice" SummaryType="Sum" />
            <dxwgv:ASPxSummaryItem FieldName="InsurancePrice" SummaryType="Sum" />
            <dxwgv:ASPxSummaryItem FieldName="ObserverWagePrice" SummaryType="Sum" />
            <dxwgv:ASPxSummaryItem FieldName="NezamPrice" SummaryType="Sum" />
            <dxwgv:ASPxSummaryItem FieldName="ProjectId" SummaryType="Count" />
        </TotalSummary>
        <Columns>


            <%--       <dxwgv:GridViewDataTextColumn Visible="false" FieldName="AccountingId" VisibleIndex="0" ShowInCustomizationForm="false"
                Width="150px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>

            </dxwgv:GridViewDataTextColumn>--%>
            <dxwgv:GridViewDataTextColumn Caption="کد پروژه" FieldName="ProjectId" VisibleIndex="0"
                Width="70px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="شماره لیست حق الزحمه" FieldName="ListNo" VisibleIndex="0"
                Width="150px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ لیست حق الزحمه" FieldName="ListDate" VisibleIndex="0"
                Width="150px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ناظر" FieldName="ObserverName" VisibleIndex="0"
                Width="150px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="0"
                Width="70px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره شناسنامه ناظر" FieldName="IdNo" VisibleIndex="0">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد ملی ناظر" FieldName="SSN" VisibleIndex="0">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="شماره همراه ناظر" FieldName="MobileNo" VisibleIndex="0">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ تولد ناظر" FieldName="BirhtDate" VisibleIndex="0">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="ماهیت ناظر" FieldName="MeType" VisibleIndex="0">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="شماره حساب عضو" FieldName="BankAccNo" VisibleIndex="0"
                Width="100px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نوع نظارت" FieldName="ObserversTypeName" VisibleIndex="0"
                Width="100px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="متراژ کسر ظرفیت" FieldName="CapacityDecrement"
                VisibleIndex="0" Width="100px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="متراژ دستمزد" FieldName="Wage" VisibleIndex="0"
                Width="100px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>


            <dxwgv:GridViewDataTextColumn Caption="سال تعرفه" Visible="false" FieldName="priceYear" VisibleIndex="0"
                Width="150px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="75px" FieldName="WorkYear"
                Caption="سال کاری" Name="WorkYear">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="مبلغ تعرفه" Visible="false" FieldName="PriceArchivePrice" VisibleIndex="0"
                Width="150px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شهر" Visible="false" FieldName="CitName" VisibleIndex="0"
                Width="150px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نمایندگی پروژه" Visible="false" FieldName="AgentName" VisibleIndex="0"
                Width="150px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="هزینه پایه بیمه(ریال)" FieldName="InsurancePrice"
                VisibleIndex="0" Width="200px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تعرفه بیمه(ریال)" FieldName="ObserverInsurancePrice"
                VisibleIndex="0" Width="200px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="سهم سازمان(ریال)" FieldName="NezamPrice" VisibleIndex="0"
                Width="200px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="سهم ناظر(ریال)" FieldName="ObserverWagePrice"
                VisibleIndex="0" Width="200px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ کسر ظرفیت" FieldName="DecreasedDate" VisibleIndex="0"
                Width="80px" Visible="false">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Foundation" Caption="متراژ پروژه"
                Name="Foundation" Width="100px">
                <PropertiesTextEdit EnableFocusedStyle="False">
                </PropertiesTextEdit>
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
                <HeaderStyle Wrap="True" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ ثبت پروژه" FieldName="ProjectDate" VisibleIndex="0"
                Width="80px" Visible="false">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Width="100px" VisibleIndex="0" FieldName="IsObserverConfirmedName"
                Caption="وضعیت پذیرش ارجاع توسط ناظر">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پلاک ثبتی" FieldName="RegisteredNo" VisibleIndex="0"
                Width="70px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="GroupName"
                Caption="گروه ساختمانی" Name="GroupName">
                <CellStyle Wrap="True" HorizontalAlign="Right">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="مالک" FieldName="OwnerName" VisibleIndex="0"
                Width="300px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <%--   <dxwgv:GridViewDataTextColumn Caption="تاریخ صدور پروانه ساختمان" FieldName="BuildingCertificateStartDate"
                VisibleIndex="0" Width="200px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان اعتبار پروانه ساختمان" FieldName="BuildingCertificateExpirDate"
                VisibleIndex="0" Width="200px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>--%>
            <dxwgv:GridViewDataTextColumn Caption="ثبت کننده ناظر" Visible="false" FieldName="UserNameInsertRecord" VisibleIndex="0"
                Width="150px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="فاقد پیش شرط ها" FieldName="SaveWithOutConditionName" VisibleIndex="0"
                Width="100px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="0" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>
    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent2" runat="server">
                <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                    width="100%">
                    <tr>
                        <td align="right">
                            <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی اکسل"
                                            ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnExportExcel_Click">
                                            <Image Url="~/Images/icons/ExportExcel.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتخاب ستون ها"
                                            ID="btnChoosecolumn1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            AutoPostBack="False" Visible="true">
                                            <ClientSideEvents Click="function(s, e) {
	if(!GridViewObserverReport.IsCustomizationWindowVisible())
		GridViewObserverReport.ShowCustomizationWindow();
	else
		GridViewObserverReport.HideCustomizationWindow();
}" />
                                            <Image Height="25px" Url="~/Images/icons/cursor-hand.png" Width="25px" />
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
    <asp:ObjectDataSource ID="ObjdObserveReport" runat="server" SelectMethod="SelectProjectObserverWageReport"
        TypeName="TSP.DataManager.TechnicalServices.Project_ObserversManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DbType="Int32" DefaultValue="-1" Name="projectId" />
            <asp:Parameter DbType="String" DefaultValue="%" Name="RegisteredNo" />
            <asp:Parameter DbType="Int32" DefaultValue="-1" Name="MeId" />
            <asp:Parameter DbType="Int32" DefaultValue="-1" Name="ListNo" />
            <asp:Parameter DbType="String" DefaultValue="1" Name="FromDateDecreased" />
            <asp:Parameter DbType="String" DefaultValue="2" Name="ToDateDecreased" />
            <asp:Parameter DbType="Int32" DefaultValue="-1" Name="AgentId" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dx:ASPxHiddenField ID="HiddenFieldReport" runat="server">
    </dx:ASPxHiddenField>
</asp:Content>
