<%@ Page Title="گزارش طراحان پروژه" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ReportDesignersCity.aspx.cs" Inherits="Employee_TechnicalServices_Report_ReportDesignersCity" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/MemberCapacityUserControl.ascx" TagPrefix="TSP" TagName="MemberCapacity" %>
<%@ Register Src="~/UserControl/MeMembershipInfoUserControl.ascx" TagPrefix="TSP"
    TagName="MeMembershipInfoUserControl" %>
<%@ Register Src="~/UserControl/MeDocumentInfoUserControl.ascx" TagPrefix="TSP" TagName="MeDocumentInfoUserControl" %>
<%@ Register Src="~/UserControl/MeMembershipLicenceInfoUserControl.ascx" TagPrefix="TSP"
    TagName="MeMembershipLicenceInfoUserControl" %>
<%@ Register Src="~/UserControl/MeOfficeInfoUserControl.ascx" TagPrefix="TSP" TagName="MeOfficeInfoUserControlUserControl" %>
<%@ Register Src="~/UserControl/MeEngOfficeInfoUserControl.ascx" TagPrefix="TSP"
    TagName="MeEngOfficeInfoUserControl" %>
<%@ Register Src="~/UserControl/MemberImplementDocInfoUserControl.ascx" TagPrefix="TSP"
    TagName="MemberImplementDocInfoUserControl" %>
<%@ Register Src="~/UserControl/MemberObservationDocInfoUserControl.ascx" TagPrefix="TSP"
    TagName="MemberObservationDocInfoUserControl" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
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
    <div align="right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table dir="rtl" cellpadding="0" align="right">
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی اکسل"
                                    ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnExportExcel_Click">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image  Url="~/Images/icons/ExportExcel.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="انتخاب ستون ها"
                                    ID="btnChoosecolumn" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    AutoPostBack="False" Visible="true">
                                    <ClientSideEvents Click="function(s, e) {
	if(!GridViewProjectDesigner.IsCustomizationWindowVisible())
		GridViewProjectDesigner.ShowCustomizationWindow();
	else
		GridViewProjectDesigner.HideCustomizationWindow();
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/cursor-hand.png" Width="25px" />
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
                        <td valign="top" align="right" width="15%">
                            <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="ASPxLabel13">
                            </dxe:ASPxLabel>
                        </td>
                        <td valign="top" align="right" width="35%">
                            <TSPControls:CustomTextBox runat="server" ID="txtMeIdSearch" ClientInstanceName="txtMeIdSearch"  Width="100%"
                                >
                                <ClientSideEvents KeyPress="function(s,e) { SearchKeyPress(e, 1);}" />
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <RegularExpression ErrorText="* نامعتبر" ValidationExpression="\d*"></RegularExpression>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td valign="top" align="right" width="15%">
                            <asp:Label runat="server" Text="شهر" ID="Label35"></asp:Label>
                        </td>
                        <td valign="top" align="right" width="35%">
                            <TSPControls:CustomAspxComboBox runat="server"  
                                TextField="CitName" ID="ComboBoxCity" ClientInstanceName="ComboBoxCity"
                                DataSourceID="ObjectDataSourceCity" ValueType="System.Int32"
                                ValueField="CitId"  EnableIncrementalFiltering="True"
                                Width="100%" RightToLeft="True">
                                <ItemStyle HorizontalAlign="Right" />
                            </TSPControls:CustomAspxComboBox>
                            <asp:ObjectDataSource ID="ObjectDataSourceCity" runat="server" SelectMethod="GetData"
                                TypeName="TSP.DataManager.CityManager"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Text="تاریخ پروژه از" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox ID="txtDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                Width="330px" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right" valign="top">
                            <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="تاریخ پروژه تا" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox ID="txtDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                Width="330px" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="تاریخ کسرظرفیت از" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox ID="txtDateFromDecreased" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                Width="330px" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                        </td>
                        <td align="right" valign="top">
                            <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="تاریخ کسرظرفیت تا" Width="100%">
                            </dxe:ASPxLabel>
                        </td>
                        <td align="right" valign="top">
                            <pdc:PersianDateTextBox ID="txtDateToDecreased" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                Width="330px" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <br />
                            <table>
                                <tr>
                                    <td align="left">
                                        <TSPControls:CustomAspxButton ID="btnSearch" runat="server" OnClick="btnSearch_Click" 
                                             Text="جستجو" ClientInstanceName="btnSearch" Width="98px" UseSubmitBehavior="true">
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
                                        <TSPControls:CustomAspxButton ID="ASPxButton3" runat="server" OnClick="btnSearch_Click" 
                                             Text="پاک کردن فرم" Width="100px" UseSubmitBehavior="False">
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
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />

    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewProjectDesigner"
        ExportedRowType="All">
    </dxwgv:ASPxGridViewExporter>
    <TSPControls:CustomAspxDevGridView ID="GridViewProjectDesigner" runat="server" Width="100%"
          ClientInstanceName="GridViewProjectDesigner"
        KeyFieldName="PrjDesignerId" DataSourceID="ObjectDataSourceReportDesigners" AutoGenerateColumns="False">
        <SettingsCookies Enabled="true" />
        <SettingsCustomizationWindow Enabled="True" />
        <TotalSummary>
            <dxwgv:ASPxSummaryItem FieldName="CapacityDecrement" SummaryType="Sum" />
            <dxwgv:ASPxSummaryItem FieldName="Wage" SummaryType="Sum" />
            <dxwgv:ASPxSummaryItem FieldName="PrjDesignerId" SummaryType="Count" />
        </TotalSummary>
        <Columns>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="70px" FieldName="MeId"
                Caption="کد عضویت" Name="MeId">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="70px" FieldName="MeName"
                Caption="نام طراح" Name="MeName">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="70px" FieldName="MjParentName"
                Caption="رشته پروانه" Name="MjParentName">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="70px" FieldName="ProjectId"
                Caption="کد پروژه" Name="ProjectId">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="OwnerName"
                Caption="مالک" Name="OwnerName">
                <CellStyle Wrap="True" HorizontalAlign="Right">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="GroupName"
                Caption="گروه ساختمانی" Name="GroupName">
                <CellStyle Wrap="True" HorizontalAlign="Right">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="75px" FieldName="Date" Caption="تاریخ پروژه"
                Name="Date">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="75px" FieldName="DecreasedDate"
                Caption="تاریخ کسر ظرفیت" Name="DecreasedDate">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="CapacityDecrement" Caption="متراژ کسر ظرفیت"
                Name="DecrementPercent" Width="100px">
                <PropertiesTextEdit EnableFocusedStyle="False">
                </PropertiesTextEdit>
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
                <HeaderStyle Wrap="False" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Wage" Caption="متراژ دستمزد"
                Name="WagePercent" Width="100px">
                <PropertiesTextEdit EnableFocusedStyle="False">
                </PropertiesTextEdit>
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>                 
                    <dxwgv:GridViewDataTextColumn Caption="فاقد پیش شرط ها"   FieldName="SaveWithOutConditionName" VisibleIndex="0"
                        Width="100px">
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
                  <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="AgentName" Caption="نمایندگی پروژه"
                    Name="AgentName" Width="100px">
                    <PropertiesTextEdit EnableFocusedStyle="False">
                    </PropertiesTextEdit>
                    <CellStyle Wrap="True" HorizontalAlign="Center">
                    </CellStyle>
                    <HeaderStyle Wrap="True" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MemberAgentName" Caption="نمایندگی طراح"
                    Name="AgentName" Width="100px">
                    <PropertiesTextEdit EnableFocusedStyle="False">
                    </PropertiesTextEdit>
                    <CellStyle Wrap="True" HorizontalAlign="Center">
                    </CellStyle>
                    <HeaderStyle Wrap="True" />
                </dxwgv:GridViewDataTextColumn>
               <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="DesName" Caption="پایه طراحی"
                    Name="DesName" Width="100px">
                    <PropertiesTextEdit EnableFocusedStyle="False">
                    </PropertiesTextEdit>
                    <CellStyle Wrap="True" HorizontalAlign="Center">
                    </CellStyle>
                    <HeaderStyle Wrap="True" />
                </dxwgv:GridViewDataTextColumn>
                   <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ObsName" Caption="پایه نظارت"
                    Name="ObsName" Width="100px">
                    <PropertiesTextEdit EnableFocusedStyle="False">
                    </PropertiesTextEdit>
                    <CellStyle Wrap="True" HorizontalAlign="Center">
                    </CellStyle>
                    <HeaderStyle Wrap="True" />
                </dxwgv:GridViewDataTextColumn>
                   <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectIngridientTypeTitle" Caption="زمینه کسر ظرفیت"
                    Name="ProjectIngridientTypeTitle" Width="100px">
                    <PropertiesTextEdit EnableFocusedStyle="False">
                    </PropertiesTextEdit>
                    <CellStyle Wrap="True" HorizontalAlign="Center">
                    </CellStyle>
                    <HeaderStyle Wrap="True" />
                </dxwgv:GridViewDataTextColumn>
                  
                   <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="DesignerTypeTitle" Caption="نوع طراح"
                    Name="DesignerTypeTitle" Width="100px">
                    <PropertiesTextEdit EnableFocusedStyle="False">
                    </PropertiesTextEdit>
                    <CellStyle Wrap="True" HorizontalAlign="Center">
                    </CellStyle>
                    <HeaderStyle Wrap="True" />
                </dxwgv:GridViewDataTextColumn>
                 <%-- <dxwgv:GridViewDataTextColumn VisibleIndex="9" Width="50px" FieldName="GroupName"
                    Caption="گروه ساختمان" Name="GroupName">
                    <CellStyle Wrap="True" HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn VisibleIndex="10" Width="100px" FieldName="RegisteredNo"
                    Caption="پلاک ثبتی" Name="RegisteredNo">
                    <CellStyle Wrap="True" HorizontalAlign="Center">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>--%>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="100px" FieldName="CitName"
                Caption="شهر" Name="CitName">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="100px" FieldName="MunName"
                Caption="شهرداری" Name="MunName">
                <CellStyle Wrap="True" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <%-- <dxwgv:GridViewDataTextColumn VisibleIndex="10" Width="150px" FieldName="ArchiveNo"
                    Caption="کد بایگانی پروژه" Name="ProjectName">
                    <CellStyle Wrap="True" HorizontalAlign="Right">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>--%>
            <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowClearFilterButton="true">
            
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowHorizontalScrollBar="true" ShowFooter="true"></Settings>
    </TSPControls:CustomAspxDevGridView>
    <asp:ObjectDataSource ID="ObjectDataSourceReportDesigners" runat="server" SelectMethod="ReportTSProjectDesignerByCity" TypeName="TSP.DataManager.TechnicalServices.Project_DesignerManager">
        <SelectParameters>
            <asp:Parameter DbType="Int32" DefaultValue="-1" Name="MeId" />
            <asp:Parameter DbType="String" DefaultValue="1" Name="FromDate" />
            <asp:Parameter DbType="String" DefaultValue="2" Name="ToDate" />
            <asp:Parameter DbType="String" DefaultValue="1" Name="FromDateDecreased" />
            <asp:Parameter DbType="String" DefaultValue="2" Name="ToDateDecreased" />
            <asp:Parameter DbType="Int32" DefaultValue="-1" Name="AgentId" />
            <asp:Parameter DbType="Int32" DefaultValue="-1" Name="CitId" />
        </SelectParameters>

    </asp:ObjectDataSource>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table dir="rtl" cellpadding="0" align="right">
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی اکسل"
                                    ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnExportExcel_Click">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image  Url="~/Images/icons/ExportExcel.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="انتخاب ستون ها"
                                    ID="btnChoosecolumn2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    AutoPostBack="False" Visible="true">
                                    <ClientSideEvents Click="function(s, e) {
	if(!GridViewProjectDesigner.IsCustomizationWindowVisible())
		GridViewProjectDesigner.ShowCustomizationWindow();
	else
		GridViewProjectDesigner.HideCustomizationWindow();
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/cursor-hand.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
</asp:Content>
