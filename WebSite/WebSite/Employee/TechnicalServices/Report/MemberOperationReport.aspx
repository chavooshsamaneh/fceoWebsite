<%@ Page Title="گزارش ریزکارکرد اعضا" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberOperationReport.aspx.cs" Inherits="Employee_TechnicalServices_Report_MemberOperationReport" %>

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

<%@ Register Src="~/UserControl/MeMembershipLicenceInfoUserControl.ascx" TagPrefix="TSP"
    TagName="MeMembershipLicenceInfoUserControl" %>
<%@ Register Src="~/UserControl/MeOfficeInfoUserControl.ascx" TagPrefix="TSP" TagName="MeOfficeInfoUserControlUserControl" %>
<%@ Register Src="~/UserControl/MeEngOfficeInfoUserControl.ascx" TagPrefix="TSP"
    TagName="MeEngOfficeInfoUserControl" %>
<%@ Register Src="~/UserControl/MemberImplementDocInfoUserControl.ascx" TagPrefix="TSP"
    TagName="MemberImplementDocInfoUserControl" %>
<%@ Register Src="~/UserControl/MemberObservationDocInfoUserControl.ascx" TagPrefix="TSP"
    TagName="MemberObservationDocInfoUserControl" %>

<%@ Register Src="~/UserControl/WorkRequestUserControl.ascx" TagPrefix="TSP" TagName="WorkRequestUserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                    ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    AutoPostBack="false">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/printers.png">
                                    </Image>
                                    <ClientSideEvents Click="function(s,e){
                                         CallbackPanelMain.PerformCallback('Print');
                                            }" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی اکسل"
                                    ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnExportExcel_Click">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/ExportExcel.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>

                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتخاب ستون ها"
                                    ID="btnChoosecolumn" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    AutoPostBack="False" Visible="true">
                                    <ClientSideEvents Click="function(s, e) {
	if(!GridViewProject.IsCustomizationWindowVisible())
		GridViewProject.ShowCustomizationWindow();
	else
		GridViewProject.HideCustomizationWindow();
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
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

    <TSPControls:CustomASPxRoundPanel ID="RoundPanelMeInfo" ShowCollapseButton="true" AllowCollapsingByHeaderClick="true" Collapsed="true" runat="server" Width="100%"
        HeaderText="راهنما">

        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <table width="100%">
                    <tr>
                        <td width="100%" align="right">
                            <table runat="server" id="TblHelp" width="100%">
                                <tr>
                                    <td colspan="4" align="right" valign="top" dir="rtl">
                                        <b>
                                            <ul style="font-family=tahoma; font-size: 8pt; line-height: 15pt; color: DarkRed">
                                                <li>در صورتی که پروانه اشتغال به کار شخص دارای رشته موضوع پروانه نقشه برداری و صلاحیت
                                                                رشته نقشه برداری باشد ، صلاحیت نظارت وی بر اساس پایه صلاحیت نقشه برداری وی محاسبه
                                                                می گردد.این شخص از ظرفیت نظارت خود به عنوان ناظر نقشه بردار می تواند استفاده نماید..
                                                </li>
                                                <li>داشتن صلاحیت در پروانه بدون ثبت آماده به کاری در سیستم به مفهوم توانایی شخص جهت فعالیت به عنوان ناظر و طراح نمی باشد. در صورتی که قصد
                                                                انجام کار نظارت یا طراحی را دارند، بایستی اقدام به ثبت آماده به کاری در سیستم نمایند </li>
                                                <li>در هنگام ثبت یک عضو حقیقی به عنوان ناظر 'ظرفیت موثر دفتر/ شرکت' در نظر گرفته نمی
                                                                شود.در این حالت ظرفیت نظارت به صورت زیر محاسبه می گردد:
                                                                <br />
                                                    ظرفیت کل نظارت = حداکثر ظرفیت مجاز بر اساس پایه شخص
                                                            + مقدار افزایش/کاهش ظرفیت نظارت </li>
                                                <li>داشتن ظرفیت اجرا به مفهوم توانایی شخص جهت فعالیت اجرا نمی باشد. در صورتی که قصد
                                                                انجام کار اجرا به عنوان شخص حقیقی دارند، بایستی دارای <u>مجوز اجرا</u> باشد و یا
                                                                اینکه به عنوان <u>عضو شرکت (مجری حقوقی)</u> کار را دریافت نماید و از ظرفیت شرکت
                                                                استفاده نماید. </li>
                                            </ul>
                                        </b>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>

    <br />
    <TSPControls:CustomAspxCallbackPanel runat="server"
        RightToLeft="True" ClientInstanceName="CallbackPanelMain"
        Width="100%" ID="CallbackPanelMain" OnCallback="CallbackPanelMain_Callback">

        <ClientSideEvents EndCallback="function(s, e) {
    if(s.cpError==1)
     {
       ShowMessage(s.cpMsg);
       s.cpError=0;
       s.cpMsg = '';
     }
     if(s.cpPrint==1)
     {
          if(s.cpCanPrint==1)
           {
                window.open('../../../Print.aspx');                      
           }
           else
           {
             alert('ابتدا اطلاعات شخص را جسجو نمایید');    
           }
     }
}" />
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent3" runat="server">
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
                                        <TSPControls:CustomTextBox runat="server" ID="txtMeIdSearch" Width="100%">
                                            <ClientSideEvents KeyPress="function(s,e) { SearchKeyPress(e, 1);}" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText="* نامعتبر" ValidationExpression="\d*"></RegularExpression>

                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="شماره پروانه" ID="ASPxLabel3">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="35%">
                                        <TSPControls:CustomTextBox ID="txtFileNo" runat="server" ClientInstanceName="txtFileNo" Width="100%"
                                            RightToLeft="True" Style="direction: ltr">
                                            <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RegularExpression ErrorText="شماره پروانه به صورت *****-***-**  می باشد" ValidationExpression="\d{2}-\d{3}-\d{1,5}" />
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
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
                                            Width="230px" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="تاریخ پروژه تا" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox ID="txtDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                            Width="230px" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
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
                                    <td>
                                        <asp:Label runat="server" Text="وضعیت کسر ظرفیت" ID="Label48"></asp:Label>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxComboBox runat="server" IncrementalFilteringMode="StartsWith"
                                            ID="cmbIsFree" ClientInstanceName="cmbIsFree"
                                            EnableIncrementalFiltering="True"
                                            RightToLeft="True">
                                            <Items>
                                                <dx:ListEditItem Text="کسر" Value="0" />
                                                <dx:ListEditItem Text="بازگشت" Value="1" />
                                            </Items>
                                            <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <br />
                                        <table>
                                            <tr>
                                                <td align="left">
                                                    <TSPControls:CustomAspxButton ID="btnSearch" runat="server" AutoPostBack="true" OnClick="btnSearch_Click"
                                                        Text="جستجو" ClientInstanceName="btnSearch" Width="98px" UseSubmitBehavior="true">
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td align="right">
                                                    <TSPControls:CustomAspxButton ID="ASPxButton3" runat="server" AutoPostBack="false"
                                                        Text="پاک کردن فرم" Width="100px" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {	
CallbackPanelMain.PerformCallback('clear');
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
                <TSP:WorkRequestUserControl runat="server" ID="WorkRequestUserControl" />


                <TSP:MemberCapacity ID="MemberCapacityUCDesign" runat="server" />
                <br />

                <TSPControls:CustomAspxDevGridView Caption="لیست کل پروژه ها" ID="GridViewProject" runat="server" Width="100%"
                    ClientInstanceName="GridViewProject"
                    KeyFieldName="ProjectId" AutoGenerateColumns="False" DataSourceID="ObjdProject">
                    <SettingsCookies Enabled="true" />
                    <SettingsCustomizationWindow Enabled="True" />
                    <Settings ShowHorizontalScrollBar="true" ShowFooter="true"></Settings>
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem FieldName="CapacityDecrement" SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem FieldName="Wage" SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem FieldName="NezamShare" SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem FieldName="ProjectId" SummaryType="Count" />
                    </TotalSummary>
                    <Columns>

                        <dxwgv:GridViewDataTextColumn Caption=" وضعیت کسر ظرفیت" FieldName="IsFreeName" Name="IsFreeName"
                            VisibleIndex="0" Width="150px">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ بازگشت ظرفیت" FieldName="FreeDate" Name="FreeDate"
                            VisibleIndex="0" Width="150px">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption=" وضعیت کسر کار" FieldName="IsWorkFreeName" Name="IsWorkFreeName"
                            VisibleIndex="0" Width="150px">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ بازگشت کار" FieldName="WorkFreeDate" Name="WorkFreeDate"
                            VisibleIndex="0" Width="150px">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت جریمه" FieldName="IsFineName" Name="IsFineName"
                            VisibleIndex="0" Width="150px">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان جریمه" FieldName="FineExpireDate" Name="FineExpireDate"
                            VisibleIndex="0" Width="150px">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" Name="InActiveName"
                            VisibleIndex="0" Width="150px">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="70px" FieldName="ProjectId"
                            Caption="کد پروژه" Name="ProjectId">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="DiscountPercent"
                            Caption="نوع پروژه" Name="DiscountPercent">
                            <CellStyle Wrap="True" HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="ProjectName"
                            Caption="نام پروژه" Name="ProjectName">
                            <CellStyle Wrap="True" HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="AgentName"
                            Caption="نمایندگی پروژه" Name="OwnerName">
                            <CellStyle Wrap="True" HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="OwnerName"
                            Caption="مالک" Name="OwnerName">
                            <CellStyle Wrap="True" HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="OwnerType"
                            Caption="نوع مالکیت" Name="OwnerType">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="75px" FieldName="Date" Caption="تاریخ پروژه"
                            Name="Date">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="75px" FieldName="WorkYear"
                            Caption="سال کاری" Name="WorkYear">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="75px" FieldName="DecreasedDate"
                            Caption="تاریخ کسر ظرفیت" Name="DecreasedDate">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewDataTextColumn Caption="فاقد پیش شرط ها" FieldName="SaveWithOutConditionName" VisibleIndex="0"
                            Width="100px">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectIngridientType"
                            Caption="سمت" Name="ProjectIngridientType" Width="80px">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectIngridientResName"
                            Caption="مسئولیت" Name="ProjectIngridientResName" Width="80px">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectIngridientDecreaseTypeName"
                            Caption="زمینه کسر ظرفیت" Name="ProjectIngridientDecreaseTypeName">
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
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MaxStage" Caption="حداکثر تعداد طبقات"
                            Name="MaxStage" Width="80px">
                            <PropertiesTextEdit EnableFocusedStyle="False">
                            </PropertiesTextEdit>
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                            <HeaderStyle Wrap="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Foundation" Caption="متراژ پروژه"
                            Name="Foundation" Width="100px">
                            <PropertiesTextEdit EnableFocusedStyle="False">
                            </PropertiesTextEdit>
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                            <HeaderStyle Wrap="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="50px" FieldName="GroupName"
                            Caption="گروه ساختمان" Name="GroupName">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="100px" FieldName="RegisteredNo"
                            Caption="پلاک ثبتی" Name="RegisteredNo">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
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
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="ArchiveNo"
                            Caption="کد بایگانی پروژه" Name="ProjectName">
                            <CellStyle Wrap="True" HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="100px" FieldName="FileNo"
                            Caption="شماره پرونده" Name="FileNo">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="پروانه ساخت" FieldName="LicenseNo" Name="LicenseNo"
                            VisibleIndex="0" Width="100px">
                            <CellStyle Wrap="false" CssClass="CellLeft" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نوع کاربری" FieldName="Usage" Name="Usage"
                            VisibleIndex="11" Width="100px">
                            <CellStyle Wrap="false" CssClass="CellLeft" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataMemoColumn VisibleIndex="0" Width="150px" FieldName="PlansMethodNo"
                            Caption="شماره دستور نقشه" Name="PlansMethodNo">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataMemoColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="100px" FieldName="ComputerCode"
                            Caption="کد کامپیوتری" Name="ComputerCode">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="13" FieldName="ProjectStatus" Caption="وضعیت پروژه"
                            Name="ProjectStatus" Width="300px">
                            <PropertiesTextEdit EnableFocusedStyle="False">
                            </PropertiesTextEdit>
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="100px" FieldName="CoordinatorObserver"
                            Caption="ناظر هماهنگ کننده" Name="CoordinatorObserver">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="13" Width="100px" FieldName="ObserverPayedShare"
                            Caption="سهم نظارت" Name="ObserverPayedShare">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ObserverShare" Caption="سهم محاسبه شده نظارت">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="100px" FieldName="NezamShare"
                            Caption="سهم سازمان" Name="NezamShare">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="100px" FieldName="InsuranceShare"
                            Caption="سهم بیمه" Name="InsuranceShare">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="100px" FieldName="Year"
                            Caption="سال تعرفه" Name="Year">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="13" Width="100px" FieldName="CeateDateObsInAccList"
                            Caption="ثبت در لیست حق الزحمه ناظرین" Name="CeateDateObsInAccList">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewDataTextColumn Caption="ناحیه" FieldName="MainRegion" Name="MainRegion"
                            VisibleIndex="0" Width="150px">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="قطعه" FieldName="MainSection" Name="MainSection"
                            VisibleIndex="0" Width="150px">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>

                        <%--    <dxwgv:GridViewDataTextColumn Caption="وضعیت سهم ناظر" FieldName="PayStatus" VisibleIndex="13"
                                                Width="100px">
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="تاریخ پرداخت" FieldName="PayeDate" VisibleIndex="13"
                                                Width="150px">
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>--%>
                        <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                </TSPControls:CustomAspxDevGridView>
                <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewProject"
                    ExportedRowType="All">
                </dxwgv:ASPxGridViewExporter>
                <asp:ObjectDataSource ID="ObjdProject" runat="server" SelectMethod="SelectProjectMembersReport"
                    TypeName="TSP.DataManager.TechnicalServices.ProjectManager" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter Name="MeId" DefaultValue="-1" Type="Int32" />
                        <asp:Parameter Name="MemberFileNo" DefaultValue="%" Type="String" />
                        <asp:Parameter Name="FromDate" DefaultValue="1" Type="String" />
                        <asp:Parameter Name="ToDate" DefaultValue="2" Type="String" />
                        <asp:Parameter Name="FromDateDecreased" DefaultValue="1" Type="String" />
                        <asp:Parameter Name="ToDateDecreased" DefaultValue="2" Type="String" />
                        <asp:Parameter Name="AgentId" DefaultValue="-1" Type="Int32" />
                        <asp:Parameter Name="IsFree" DefaultValue="-1" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <TSP:MeMembershipLicenceInfoUserControl runat="server" ID="UserControlMeMembershipLicenceInfo"
                    MeId="-2" />
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomAspxCallbackPanel>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                    ID="btnPrint2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    AutoPostBack="false">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/printers.png">
                                    </Image>
                                    <ClientSideEvents Click="function(s,e){
                                          CallbackPanelMain.PerformCallback('Print');
                                            }" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی اکسل"
                                    ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnExportExcel_Click">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/ExportExcel.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتخاب ستون ها"
                                    ID="btnChoosecolumn2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    AutoPostBack="False" Visible="true">
                                    <ClientSideEvents Click="function(s, e) {
	if(!GridViewProject.IsCustomizationWindowVisible())
		GridViewProject.ShowCustomizationWindow();
	else
		GridViewProject.HideCustomizationWindow();
}" />
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
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
    <asp:HiddenField ID="HiddenFieldMeId" runat="server" />
    <dx:ASPxHiddenField ID="HiddenFieldReport" ClientInstanceName="HiddenFieldReport"
        runat="server">
    </dx:ASPxHiddenField>
</asp:Content>
