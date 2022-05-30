<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="Summary.aspx.cs" Inherits="Employee_TechnicalServices_Report_Summary"
    Title="خلاصه پرونده" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" style="display: block" align="center">
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <div dir="ltr" style="display: block; overflow: hidden">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                        visible="false">
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
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="چاپ" ToolTip="چاپ"
                                                    CausesValidation="True" Width="25px" ID="btnPrint" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnPrint_Click">
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="بازگشت" ToolTip="بازگشت"
                                                    CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnBack_Click">
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>
                    <br />
                    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanelMain" HeaderText="خلاصه پرونده" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>

                                <table dir="rtl">
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="right">
                                                <asp:Label runat="server" Text="کد پروژه" ID="LabelProjectId" Visible="false"></asp:Label>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxProjectId"
                                                    Enabled="True" Visible="false">
                                                </dxcp:ASPxLabel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <fieldset>
                                    <legend class="HelpUL">اطلاعات پایه</legend>

                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="کد پروژه" ID="Label2" __designer:wfdid="w60"></asp:Label>

                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxCode" ReadOnly="True" __designer:wfdid="w61">
                                                    </dxcp:ASPxLabel>

                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="تاریخ ثبت" ID="Label47"></asp:Label>

                                                </td>
                                                <td dir="ltr" valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="RegDate" ReadOnly="True">
                                                    </dxcp:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="نام پروژه" ID="Label3"></asp:Label>

                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxProjectName">
                                                    </dxcp:ASPxLabel>

                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="وضعیت پروژه" ID="Label51"></asp:Label>

                                                </td>
                                                <td dir="ltr" valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox Enabled="false" runat="server" TextField="Status" ID="ASPxComboBoxProjectStatus" DataSourceID="ObjectDataSourceProjectStatus" ReadOnly="True" ValueType="System.Int32" ValueField="ProjectStatusId" EnableIncrementalFiltering="True" __designer:wfdid="w68">
                                                    </TSPControls:CustomAspxComboBox>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="شماره پرونده" ID="Label12"></asp:Label>

                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxFileNo" ReadOnly="True">
                                                    </dxcp:ASPxLabel>

                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="تاریخ پرونده" ID="Label13"></asp:Label>

                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="FileDate">
                                                    </dxcp:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="شهر" ID="Label35"></asp:Label>

                                                </td>
                                                <td dir="ltr" valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox Enabled="false" runat="server" TextField="CitName" ID="ASPxComboBoxCity" DataSourceID="ObjectDataSourceCity" ReadOnly="True" ValueType="System.Int32" ValueField="CitId" EnableIncrementalFiltering="True" __designer:wfdid="w75">
                                                    </TSPControls:CustomAspxComboBox>

                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="شهرداری" ID="Label9" __designer:wfdid="w76"></asp:Label>

                                                </td>
                                                <td dir="ltr" valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox Enabled="false" runat="server" TextField="MunName" ID="ASPxComboBoxMunicipality" DataSourceID="ObjectdatasourceMunicipality" ReadOnly="True" ValueType="System.Int32" ValueField="MunId" EnableIncrementalFiltering="True" __designer:wfdid="w77">


                                                        <ButtonStyle Width="13px"></ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>

                                                    <asp:ObjectDataSource runat="server" SelectMethod="SelectByCity" ID="ObjectdatasourceMunicipality" TypeName="TSP.DataManager.TechnicalServices.MunicipalityManager" OldValuesParameterFormatString="original_{0}" __designer:wfdid="w78">
                                                        <SelectParameters>
                                                            <asp:ControlParameter PropertyName="Value" Type="Int32" DefaultValue="0" Name="CitId" ControlID="ASPxComboBoxCity"></asp:ControlParameter>
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="کد نوسازی شهرداری" ID="Label11" __designer:wfdid="w79"></asp:Label>

                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxReconstructionCode" ReadOnly="True" __designer:wfdid="w80">
                                                    </dxcp:ASPxLabel>

                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="کد کامپیوتری" ID="Label10" __designer:wfdid="w81"></asp:Label>

                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxComputerCode" ReadOnly="True" __designer:wfdid="w82">
                                                    </dxcp:ASPxLabel>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="کاربری" ID="Label19"></asp:Label>

                                                </td>
                                                <td dir="ltr" valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox Enabled="false" runat="server" TextField="Title" ID="ASPxComboBoxUsage" DataSourceID="ObjectdatasourceUsage" ReadOnly="True" ValueType="System.Int32" ValueField="UsageId" EnableIncrementalFiltering="True">


                                                        <ButtonStyle Width="13px"></ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>

                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="گروه ساختمانی" ID="LblGroup"></asp:Label>

                                                </td>
                                                <td dir="ltr" valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox Enabled="false" runat="server" TextField="GroupName" ID="ASPxComboBoxStructureGroups" DataSourceID="ObjectdatasourceStructureGroups" ReadOnly="True" ValueType="System.Int32" ValueField="GroupId" EnableIncrementalFiltering="True">


                                                        <ButtonStyle Width="13px"></ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label5" runat="server" Text="مساحت سند"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxDocumentArea">
                                                    </dxcp:ASPxLabel>
                                                </td>

                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="مساحت زمین" ID="Label20"></asp:Label>

                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxArea">
                                                    </dxcp:ASPxLabel>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="مساحت عقب نشینی" ID="Label21"></asp:Label>

                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxRecessArea" __designer:wfdid="w88">
                                                    </dxcp:ASPxLabel>

                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="مساحت باقی مانده" ID="Label22"></asp:Label>

                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxRemainArea">
                                                    </dxcp:ASPxLabel>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="زیر بنا (متر مربع) " ID="Label4"></asp:Label>

                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxFoundation">
                                                    </dxcp:ASPxLabel>

                                                </td>

                                            </tr>


                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="آدرس" ID="Label8"></asp:Label>

                                                </td>
                                                <td valign="top" align="right" colspan="3">

                                                    <dxcp:ASPxLabel runat="server" ID="TextBoxAddress">
                                                    </dxcp:ASPxLabel>

                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </fieldset>
                                <fieldset>
                                    <legend class="HelpUL">پلاک ثبتی</legend>

                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td dir="rtl">
                                                    <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%" ID="CustomAspxDevGridViewRegisteredNo" DataSourceID="ObjectDataSourceRegisteredNo" KeyFieldName="RegisteredNoId" AutoGenerateColumns="False">
                                                        <SettingsText CommandClearFilter="پاک کردن فیلتر" ConfirmDelete="آیا مطمئن به حذف این ردیف هستید؟" EmptyDataRow="هیچ داده ای وجود ندارد" GroupPanel="برای گروه بندی از این قسمت استفاده کنید" CommandEdit="ویرایش" CommandDelete="حذف" CommandSelect="انتخاب" CommandNew="جدید" CommandUpdate="ذخیره" CommandCancel="انصراف"></SettingsText>

                                                        <Styles>
                                                            <GroupPanel ForeColor="Black"></GroupPanel>

                                                            <AlternatingRow BackColor="White"></AlternatingRow>

                                                            <Header HorizontalAlign="Center"></Header>

                                                            <SelectedRow BackColor="White" ForeColor="Black"></SelectedRow>

                                                            <FilterBar BackColor="White"></FilterBar>
                                                        </Styles>

                                                        <Settings ShowGroupPanel="False"></Settings>

                                                        <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>

                                                        <SettingsPager>
                                                            <AllButton Text="همه رکوردها"></AllButton>

                                                            <FirstPageButton Text="اولین صفحه"></FirstPageButton>

                                                            <LastPageButton Text="آخرین صفحه"></LastPageButton>

                                                            <Summary Text="صفحه: {0} از {1} (تعداد کل:{2})"></Summary>

                                                            <NextPageButton Text="صفحه بعد"></NextPageButton>

                                                            <PrevPageButton Text="صفحه قبل"></PrevPageButton>
                                                        </SettingsPager>
                                                        <Columns>
                                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="RegisteredNoId" Caption="کد پلاک ثبتی" Name="RegisteredNoId"></dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="ProjectId" Caption="کد پروژه" Name="ProjectId">
                                                                <EditFormSettings Visible="False"></EditFormSettings>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="RegisteredNo" Caption="شماره پلاک ثبتی" Name="RegisteredNo"></dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="PostalCode" Caption="کد پستی" Name="PostalCode"></dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Type" Caption="نوع پلاک" Name="Type">
                                                                <PropertiesTextEdit EnableFocusedStyle="False"></PropertiesTextEdit>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Division" Caption="بخش ثبتی"
                                                                Name="Type">
                                                                <PropertiesTextEdit EnableFocusedStyle="False"></PropertiesTextEdit>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="Status" Name="Status" VisibleIndex="4">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="5">
                                                            </dxwgv:GridViewDataTextColumn>
                                                        </Columns>

                                                        <SettingsLoadingPanel Text="در حال بارگذاری"></SettingsLoadingPanel>
                                                    </TSPControls:CustomAspxDevGridView2>

                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </fieldset>
                                <fieldset>
                                    <legend class="HelpUL">دستور نقشه</legend>

                                    <table dir="rtl" width="100%">
                                        <tbody>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="شماره فرم دستور نقشه" ID="Label14"></asp:Label>

                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxPlansMethodNo" ReadOnly="True">
                                                    </dxcp:ASPxLabel>

                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="تاریخ صدور دستور نقشه" ID="Label15"></asp:Label>

                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="PlansMethodDate" ReadOnly="True">
                                                    </dxcp:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="تراکم ساختمانی (درصد)" ID="Label16"></asp:Label>

                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxTarakom" ReadOnly="True" __designer:wfdid="w102">
                                                    </dxcp:ASPxLabel>

                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="سطح اشغال (درصد)" ID="Label17" __designer:wfdid="w103"></asp:Label>

                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxEshghalSurface" ReadOnly="True" __designer:wfdid="w104">
                                                    </dxcp:ASPxLabel>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="حداکثر ارتفاع مجاز (متر)" ID="Label26" __designer:wfdid="w105"></asp:Label>

                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxAllowableHeight" ReadOnly="True" __designer:wfdid="w106">
                                                    </dxcp:ASPxLabel>

                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="عمق حریم تجاری (متر)" ID="Label27" __designer:wfdid="w107"></asp:Label>

                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxCommercialLimitation" ReadOnly="True" __designer:wfdid="w108">
                                                    </dxcp:ASPxLabel>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="محل احداث بنا" ID="Label18" __designer:wfdid="w109"></asp:Label>

                                                </td>
                                                <td dir="ltr" valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox runat="server" Enabled="false" TextField="Title" ID="ASPxComboBoxStructureBuiltPlace" DataSourceID="ObjectDataSourceStructureBuiltPlace" ReadOnly="True" ValueType="System.Int32" ValueField="StructureBuiltPlaceId" EnableIncrementalFiltering="True" __designer:wfdid="w110">


                                                        <ButtonStyle Width="13px"></ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>

                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="تعداد بلوک" ID="Label1" __designer:wfdid="w111"></asp:Label>

                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxBlockNum" ReadOnly="True" __designer:wfdid="w112">
                                                    </dxcp:ASPxLabel>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label6" runat="server" Text="ضابطه محل"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxLocationCriterion"
                                                        ReadOnly="True">
                                                    </dxcp:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label ID="Label7" runat="server" Text="جان پناه (cm)"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <dxcp:ASPxLabel runat="server" ID="ASPxTextBoxMantelet"
                                                        ReadOnly="True">
                                                    </dxcp:ASPxLabel>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </fieldset>
                                <fieldset>
                                    <legend class="HelpUL">بلوک</legend>
                                    <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%" ID="CustomAspxDevGridViewBlock" DataSourceID="ObjectDataSourceBlock" KeyFieldName="BlockId" AutoGenerateColumns="False" OnFocusedRowChanged="CustomAspxDevGridViewBlock_FocusedRowChanged" OnDetailRowExpandedChanged="CustomAspxDevGridViewBlock_DetailRowExpandedChanged">
                                        <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>

                                        <Templates>
                                            <DetailRow>
                                                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridViewWall" runat="server" DataSourceID="ObjectDataSourceWalls" Width="100%" __designer:wfdid="w7" AutoGenerateColumns="False" KeyFieldName="WallsId" Caption="مشخصات دیوارها" OnBeforePerformDataSelect="CustomAspxDevGridViewWall_BeforePerformDataSelect">
                                                    <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>

                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="EntranceId" Caption="EntranceId" Name="EntranceId"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="BlockId" Caption="BlockId" Name="BlockId"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="MainDirectionsId" Caption="جهت" VisibleIndex="0">
                                                            <PropertiesComboBox ValueType="System.String" TextField="Title" DataSourceID="ObjectdatasourceMainDirections" ValueField="MainDirectionsId"></PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Length" Caption="طول (متر)" Name="Length"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Height" Caption="ارتفاع (متر)" Name="Height"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>

                                                    <Settings ShowGroupPanel="False"></Settings>
                                                </TSPControls:CustomAspxDevGridView>
                                                <br />
                                                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridViewEntrance" runat="server" DataSourceID="ObjectDataSourceEntrance" Width="100%" __designer:wfdid="w8" AutoGenerateColumns="False" KeyFieldName="EntranceId" Caption="مشخصات درب ها" OnBeforePerformDataSelect="CustomAspxDevGridViewEntrance_BeforePerformDataSelect">
                                                    <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>

                                                    <Styles>
                                                        <GroupPanel ForeColor="Black"></GroupPanel>

                                                        <Header HorizontalAlign="Center"></Header>
                                                    </Styles>

                                                    <SettingsPager>
                                                        <AllButton Text="همه رکوردها"></AllButton>

                                                        <FirstPageButton Text="اولین صفحه"></FirstPageButton>

                                                        <LastPageButton Text="آخرین صفحه"></LastPageButton>

                                                        <Summary Text="صفحه: {0} از {1} (تعداد کل:{2})"></Summary>

                                                        <NextPageButton Text="صفحه بعد"></NextPageButton>

                                                        <PrevPageButton Text="صفحه قبل"></PrevPageButton>
                                                    </SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="EntranceId" Caption="EntranceId" Name="EntranceId"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="BlockId" Caption="BlockId" Name="BlockId"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="EntranceTypeId" Caption="نوع" VisibleIndex="0">
                                                            <PropertiesComboBox ValueType="System.String" TextField="Title" DataSourceID="ObjectdatasourceEntranceType" ValueField="EntranceTypeId"></PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="MainDirectionsId" Caption="جهت" VisibleIndex="1">
                                                            <PropertiesComboBox ValueType="System.String" TextField="Title" DataSourceID="ObjectdatasourceMainDirections" ValueField="MainDirectionsId"></PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Num" Caption="تعداد" Name="Num"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" Width="2px">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>

                                                    <SettingsText CommandClearFilter="پاک کردن فیلتر" ConfirmDelete="آیا مطمئن به حذف این ردیف هستید؟" EmptyDataRow="هیچ داده ای وجود ندارد" GroupPanel="جهت گروه بندی ستون مربوطه را به این قسمت بکشید" CommandEdit="ویرایش" CommandDelete="حذف" CommandSelect="انتخاب" CommandNew="جدید" CommandUpdate="ذخیره" CommandCancel="انصراف"></SettingsText>

                                                    <SettingsLoadingPanel Text="در حال بارگذاری"></SettingsLoadingPanel>

                                                    <Settings ShowGroupPanel="False"></Settings>
                                                </TSPControls:CustomAspxDevGridView>
                                                <br />
                                                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridViewFoundation" runat="server" DataSourceID="ObjectDataSourceFoundation" Width="100%" __designer:wfdid="w9" AutoGenerateColumns="False" KeyFieldName="FoundationId" OnBeforePerformDataSelect="CustomAspxDevGridViewFoundation_BeforePerformDataSelect" Caption="زیربنا">
                                                    <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>

                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="FoundationId" Caption="FoundationId" Name="FoundationId"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="1" FieldName="BlockId" Caption="BlockId" Name="BlockId"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="ProjectId" Caption="ProjectId" Name="ProjectId"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="StageTitle" Caption="عنوان طبقه" Name="StageTitle"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="EshghalSurface" Caption="سطح اشغال (%)" Name="EshghalSurface"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Area" Caption="مساحت (مترمربع)" Name="Area"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Height" Caption="ارتفاع (متر)" Name="Height"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Flat" Caption="تعداد واحد" Name="Flat"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="UsageId" Caption="کاربری" VisibleIndex="5">
                                                            <PropertiesComboBox ValueType="System.Int32" TextField="Title" DataSourceID="ObjectdatasourceUsage" ValueField="UsageId"></PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="SecondaryUsageId" Caption="کاربری فرعی" VisibleIndex="6">
                                                            <PropertiesComboBox ValueType="System.String" TextField="Title" DataSourceID="ObjectdatasourceSecondaryUsage" ValueField="SecondaryUsageId"></PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="CloseYard" Caption="بالکن بسته حیاط" Name="Yard">
                                                            <HeaderStyle Wrap="True"></HeaderStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="ClosePathway" Caption="بالکن بسته معبر" Name="Pathway">
                                                            <HeaderStyle Wrap="True"></HeaderStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="OpenYard" Caption="بالکن باز حیاط" Name="Yard">
                                                            <HeaderStyle Wrap="True"></HeaderStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="10" FieldName="OpenPathway" Caption="بالکن باز معبر" Name="Pathway">
                                                            <HeaderStyle Wrap="True"></HeaderStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <%--<dxwgv:GridViewDataTextColumn VisibleIndex="11"></dxwgv:GridViewDataTextColumn>--%>
                                                    </Columns>
                                                </TSPControls:CustomAspxDevGridView>
                                            </DetailRow>
                                        </Templates>
                                        <Settings ShowGroupPanel="False"></Settings>

                             
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="BlockId" Caption="BlockId" Name="BlockId"></dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="FoundationId" Caption="FoundationId" Name="FoundationId"></dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Foundation" Caption="زیربنا" Name="Foundation"></dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="StageNum" Caption="تعداد طبقات" Name="StageNum">
                                                <CellStyle Wrap="False"></CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="StructureSystem" Caption="سیستم سازه ای" Name="StructureSystem"></dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="StructureSkeleton" Caption="اسکلت ساختمان" Name="StructureSkeleton"></dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="RoofType" Caption="نوع سقف" Name="RoofType"></dxwgv:GridViewDataTextColumn>
                                            <%--<dxwgv:GridViewDataTextColumn VisibleIndex="5"></dxwgv:GridViewDataTextColumn>--%>
                                        </Columns>

                                    </TSPControls:CustomAspxDevGridView2>

                                   

                <asp:ObjectDataSource ID="ObjectDataSourceBlock" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="FindByProjectAndPrjReId" TypeName="TSP.DataManager.TechnicalServices.BlockManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="ProjectId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="PrjReId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>



                                </fieldset>
                                <fieldset>
                                    <legend class="HelpUL">مالک</legend>

                                    <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%" ID="CustomAspxDevGridViewOwner" DataSourceID="ObjectDataSourceOwner" KeyFieldName="OwnerId" AutoGenerateColumns="False">

                                        <Settings ShowGroupPanel="False"></Settings>

                                        <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>


                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="OwnerId"></dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="1" FieldName="PrjReId"></dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataCheckColumn VisibleIndex="0" FieldName="IsAgent" Caption="نماینده مالکین">
                                                <HeaderStyle Wrap="True"></HeaderStyle>
                                            </dxwgv:GridViewDataCheckColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Name" Caption="نام مالک"></dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Tel" Caption="تلفن"></dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="MobileNo" Caption="شماره همراه"></dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="LName" Caption="نام وکیل">
                                                <CellStyle Wrap="False"></CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="LTel" Caption="تلفن وکیل"></dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="LMobileNo" Caption="شماره همراه وکیل">
                                                <CellStyle Wrap="False"></CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="7"></dxwgv:GridViewDataTextColumn>
                                        </Columns>


                                    </TSPControls:CustomAspxDevGridView2>

                                </fieldset>
                                <fieldset>
                                    <legend class="HelpUL">طراح-نقشه</legend>

                                    <TSPControls:CustomAspxDevGridView2 ID="GridViewPlanSubRe" runat="server" DataSourceID="ObjectDataSourceDesignerPlans"
                                        Width="100%"
                                        KeyFieldName="PlansId" AutoGenerateColumns="False">
                                        <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="OfficeEngOId" Caption="کد دفتر/شرکت طراح">
                                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="DesignerMeId" Caption="کد عضویت طراح">
                                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="DesignerName" Caption="نام و نام خانوادگی"
                                                Width="300px">
                                                <CellStyle Wrap="False">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="CapacityDecrement" Caption="متراژ کسر ظرفیت">
                                                <CellStyle HorizontalAlign="Center"></CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="Wage" Caption="متراژ دستمزد">
                                                <CellStyle HorizontalAlign="Center"></CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="YearName" Caption="تعرفه">
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="Year" Caption="سال کاری">
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="DesignerCreateDate" Caption="تاریخ ثبت طراح">
                                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="Date" Caption="تاریخ ثبت نقشه">
                                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                        <Settings ShowGroupPanel="False" ShowFilterRow="False"></Settings>
                                        <SettingsDetail IsDetailGrid="True"></SettingsDetail>
                                    </TSPControls:CustomAspxDevGridView2>
                                    <br />
                                    <TSPControls:CustomAspxDevGridView2 ID="GridViewPlans"  Caption="نقشه"  runat="server" DataSourceID="ObjdsPlans"
                                        Width="100%"
                                        KeyFieldName="PlansId" AutoGenerateColumns="False"
                                        ClientInstanceName="GridViewPlans">


                                        <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Visible="False" FieldName="Status"
                                                Width="150px" Caption="Status">
                                                <HeaderStyle Wrap="True"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="WorkFlowName" Width="200px"
                                                Caption="نوع درخواست">
                                                <HeaderStyle Wrap="True"></HeaderStyle>
                                                <CellStyle Wrap="True">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="No" Width="150px" Caption="شماره نقشه">
                                                <HeaderStyle Wrap="True"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                        <%--    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="PlanVersion2" Width="50px"
                                                Caption="ورژن">
                                            </dxwgv:GridViewDataTextColumn>--%>
                                            <dxwgv:GridViewDataComboBoxColumn FieldName="PlansTypeId" Caption="نوع نقشه" VisibleIndex="3"
                                                Width="150px">
                                                <HeaderStyle Wrap="True"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Right">
                                                </CellStyle>
                                                <PropertiesComboBox ValueType="System.String" TextField="Title" DataSourceID="ObjdsPlansType"
                                                    ValueField="PlansTypeId">
                                                </PropertiesComboBox>
                                                <CellStyle Wrap="False">
                                                </CellStyle>
                                            </dxwgv:GridViewDataComboBoxColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="PlanDate" Caption="تاریخ ثبت">
                                                <HeaderStyle Wrap="True"></HeaderStyle>
                                                <CellStyle Wrap="False">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="FileNo" Caption="شماره پرونده پروژه"
                                                Name="FileNo">
                                                <HeaderStyle Wrap="True"></HeaderStyle>
                                                <CellStyle Wrap="False">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="RegisteredNo" Caption="پلاک ثبتی پروژه"
                                                Name="RegisteredNo">
                                                <HeaderStyle Wrap="True"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="LicenseNo" Caption="شماره پروانه ساخت"
                                                Name="LicenseNo">
                                                <HeaderStyle Wrap="True"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="Foundation" Caption="متراژ پروژه">
                                                <HeaderStyle Wrap="True"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="11" Width="100px" FieldName="DesAcceptPlan"
                                                Caption="وضعیت تایید">
                                                <HeaderStyle Wrap="True"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="12" FieldName="InActives" Caption="وضعیت">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="12" Visible="False" FieldName="TaskName"
                                                Width="150px" Caption="وضعیت درخواست">
                                                <HeaderStyle Wrap="True"></HeaderStyle>
                                            </dxwgv:GridViewDataTextColumn>
                               <%--             <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                                                Width="50px" VisibleIndex="13">
                                                <DataItemTemplate>
                                                    <div align="center">
                                                        <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl="~/Images/WFStart.png">
                                                        </dxe:ASPxImage>
                                                    </div>
                                                </DataItemTemplate>
                                                <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                                                    ValueType="System.String">
                                                </PropertiesComboBox>
                                            </dxwgv:GridViewDataComboBoxColumn>--%>
                                            <dxwgv:GridViewCommandColumn VisibleIndex="14" Caption=" " ShowClearFilterButton="true">
                                            </dxwgv:GridViewCommandColumn>
                                        </Columns>
                                        <Settings ShowHorizontalScrollBar="true"></Settings>
                                    </TSPControls:CustomAspxDevGridView2><br />
                <asp:ObjectDataSource ID="ObjdsPlans" runat="server" TypeName="TSP.DataManager.TechnicalServices.PlansManager"
                    SelectMethod="SelectById" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="PlansId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="ProjectId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <asp:ObjectDataSource ID="ObjdsPlansType" runat="server" TypeName="TSP.DataManager.TechnicalServices.PlansTypeManager"
                    SelectMethod="GetData"></asp:ObjectDataSource>
            </ContentTemplate>
                                    <TSPControls:CustomAspxDevGridView2 Caption="نظرات بازبینی نقشه" runat="server" Width="100%"
                                        ID="GridViewViewPoint" DataSourceID="ObjectDataSourcePlansControlerViewPoint"
                                        KeyFieldName="ViewPointId" AutoGenerateColumns="False" ClientInstanceName="GridViewDesigner">
                                        <Settings ShowGroupPanel="True" ShowFilterRowMenu="True" ShowHorizontalScrollBar="true"></Settings>
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="RowNo" Width="30px" Caption="ردیف">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Subject" Caption="موضوع">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="SheetNo" Caption="شماره برگ نقشه">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="مشاهده"
                                                FieldName="FileUrl" Caption="فایل پیوست" Name="ControlerFilePath" PropertiesHyperLinkEdit-Target="_blank">
                                            </dxwgv:GridViewDataHyperLinkColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ViewPoint" Width="300px"
                                                Caption="توضیحات بازبینی">
                                            </dxwgv:GridViewDataTextColumn>


                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="Id" Caption="Id">
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                    </TSPControls:CustomAspxDevGridView2>
                                    <asp:ObjectDataSource ID="ObjectDataSourcePlansControlerViewPoint" runat="server"
                                        OldValuesParameterFormatString="original_{0}" SelectMethod="FindByPlansId" TypeName="TSP.DataManager.TechnicalServices.PlansControlerViewPointManager">
                                        <SelectParameters>
                                            <asp:Parameter Type="Int32" DefaultValue="-1" Name="PlansId"></asp:Parameter>
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </fieldset>
                                <fieldset>
                                    <legend class="HelpUL">ناظر</legend>
                                    <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridViewProjectObservers" runat="server" DataSourceID="ObjectDataSourceObserver"
                                        AutoGenerateColumns="False"
                                        KeyFieldName="ProjectObserversId" Width="100%">

                                        <Columns>
                                            <dxwgv:GridViewDataCheckColumn VisibleIndex="0" FieldName="IsMother" Caption="ناظر هماهنگ کننده">
                                                <HeaderStyle Wrap="True"></HeaderStyle>
                                            </dxwgv:GridViewDataCheckColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="ID" Caption="کد عضویت">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Name" Caption="نام">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="MemberTypeTitle" Caption="نوع ناظر">
                                            </dxwgv:GridViewDataTextColumn>

                                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Manager" Caption="مدیر مسئول">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="CapacityDecrement" Caption="متراژ کسر ظرفیت">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="Wage" Caption="متراژ دستمزد">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="7">
                                                <EditFormSettings Visible="False"></EditFormSettings>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="8" FieldName="PrjReId">
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                        <Settings ShowGroupPanel="False" ShowFilterRow="False"></Settings>
                                    </TSPControls:CustomAspxDevGridView>

                                </fieldset>

                                <fieldset>
                                    <legend class="HelpUL">مجری</legend>

                                    <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridViewImplementer" runat="server" DataSourceID="ObjectDataSourceProjectImplementer"
                                        OnFocusedRowChanged="CustomAspxDevGridViewImplementer_FocusedRowChanged"
                                        OnDetailRowExpandedChanged="CustomAspxDevGridViewImplementer_DetailRowExpandedChanged"
                                        AutoGenerateColumns="False" KeyFieldName="PrjImpId" Width="100%">
                                        <Templates>
                                            <DetailRow>
                                                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridViewImplementerAgent" runat="server" DataSourceID="ObjectDataSourceImplementerAgent"
                                                    Width="100%"
                                                    KeyFieldName="ImplementerAgentId" AutoGenerateColumns="False" OnBeforePerformDataSelect="CustomAspxDevGridViewImplementerAgent_BeforePerformDataSelect">
                                                    <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>

                                                    <Columns>

                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MeOPersonId" Caption="کد عضویت">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Name" Caption="نام نماینده">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MemberTypeTitle" Caption="نوع نماینده">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="No" Caption="شماره پروانه">
                                                            <CellStyle Wrap="False"></CellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="4">
                                                            <EditFormSettings Visible="False"></EditFormSettings>
                                                        </dxwgv:GridViewDataTextColumn>

                                                    </Columns>
                                                    <Settings ShowGroupPanel="False" ShowFilterRow="False"></Settings>
                                                </TSPControls:CustomAspxDevGridView>
                                            </DetailRow>
                                        </Templates>
                                        <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                                        <Styles>
                                            <GroupPanel ForeColor="Black">
                                            </GroupPanel>
                                            <Header HorizontalAlign="Center">
                                            </Header>
                                        </Styles>
                                        <SettingsPager>
                                            <AllButton Text="همه رکوردها">
                                            </AllButton>
                                            <FirstPageButton Text="اولین صفحه">
                                            </FirstPageButton>
                                            <LastPageButton Text="آخرین صفحه">
                                            </LastPageButton>
                                            <Summary Text="صفحه: {0} از {1} (تعداد کل:{2})"></Summary>
                                            <NextPageButton Text="صفحه بعد">
                                            </NextPageButton>
                                            <PrevPageButton Text="صفحه قبل">
                                            </PrevPageButton>
                                        </SettingsPager>
                                        <Columns>
                                            <dxwgv:GridViewDataCheckColumn Caption="نماینده مجری" FieldName="IsMother" VisibleIndex="0">
                                                <HeaderStyle Wrap="True" />
                                            </dxwgv:GridViewDataCheckColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ID" Caption="کد عضویت">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Name" Caption="نام">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MemberTypeTitle" Caption="نوع مجری">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Manager" Caption="مدیر مسئول">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="CapacityDecrement" Caption="متراژ کسر ظرفیت">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="Wage" Caption="متراژ دستمزد">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="6">
                                                <EditFormSettings Visible="False"></EditFormSettings>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="PrjReId">
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsText CommandClearFilter="پاک کردن فیلتر" ConfirmDelete="آیا مطمئن به حذف این ردیف هستید؟"
                                            EmptyDataRow="هیچ داده ای وجود ندارد" GroupPanel="برای گروه بندی از این قسمت استفاده کنید"
                                            CommandEdit="ویرایش" CommandDelete="حذف" CommandSelect="انتخاب" CommandNew="جدید"
                                            CommandUpdate="ذخیره" CommandCancel="انصراف"></SettingsText>
                                        <SettingsLoadingPanel Text="در حال بارگذاری"></SettingsLoadingPanel>
                                        <Settings ShowGroupPanel="False" ShowFilterRow="False"></Settings>
                                        <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
                                    </TSPControls:CustomAspxDevGridView>

                                </fieldset>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanel>
                    <br />

                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server" Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="چاپ" ToolTip="چاپ"
                                                    CausesValidation="True" Width="25px" ID="btnPrint2" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnPrint_Click">
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="بازگشت" ToolTip="بازگشت"
                                                    CausesValidation="False" ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnBack_Click">
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>
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
            <asp:ObjectDataSource ID="ObjectDataSourceProjectStatus" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.TechnicalServices.ProjectStatusManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceCity" runat="server"
                SelectMethod="SelectByPrId" TypeName="TSP.DataManager.CityManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="PrId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectdatasourceUsage" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.TechnicalServices.UsageManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectdatasourceSecondaryUsage" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.TechnicalServices.SecondaryUsageManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectdatasourceStructureGroups" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.TechnicalServices.StructureGroupsManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceRegisteredNo" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="FindConfirmedByProjectId" TypeName="TSP.DataManager.TechnicalServices.RegisteredNoManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-2" Name="ProjectId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceStructureBuiltPlace" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.TechnicalServices.StructureBuiltPlaceManager"></asp:ObjectDataSource>
          
            <asp:ObjectDataSource ID="ObjectDataSourceFoundation" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="FindByBlockId" TypeName="TSP.DataManager.TechnicalServices.FoundationManager">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="-1" Name="BlockId" SessionField="FoundationId" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceWalls" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="FindByBlockId" TypeName="TSP.DataManager.TechnicalServices.WallsManager">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="-1" Name="BlockId" SessionField="WallsId" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceEntrance" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="FindByBlockId" TypeName="TSP.DataManager.TechnicalServices.EntranceManager">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="-1" Name="BlockId" SessionField="EntranceId" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectdatasourceEntranceType" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.TechnicalServices.EntranceTypeManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectdatasourceMainDirections" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.TechnicalServices.MainDirectionsManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceOwner" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="FindConfirmedByProjectId" TypeName="TSP.DataManager.TechnicalServices.OwnerManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-2" Name="ProjectId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceDesignerPlans" runat="server" TypeName="TSP.DataManager.TechnicalServices.Designer_PlansManager"
                SelectMethod="SelectActiveTSDesignerPlansForByProjectId" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:SessionParameter SessionField="ProjectId" Type="Int32" DefaultValue="-1" Name="ProjectId"></asp:SessionParameter>
                    <asp:SessionParameter SessionField="ProjectId" Type="Int32" DefaultValue="-1" Name="MeId"></asp:SessionParameter>
                    <asp:SessionParameter SessionField="ProjectId" Type="Int32" DefaultValue="-1" Name="PrjReId"></asp:SessionParameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceObserver" runat="server" SelectMethod="FindConfirmedByProjectId"
                TypeName="TSP.DataManager.TechnicalServices.Project_ObserversManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-2" Name="ProjectId"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceProjectImplementer" runat="server" SelectMethod="FindConfirmedByProjectId"
                TypeName="TSP.DataManager.TechnicalServices.Project_ImplementerManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-2" Name="ProjectId"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceImplementerAgent" runat="server" SelectMethod="FindByProjectIdPrjImpIdPrjReId"
                TypeName="TSP.DataManager.TechnicalServices.ImplementerAgentManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="ProjectId"></asp:Parameter>
                    <asp:SessionParameter SessionField="ImpId" Type="Int32" DefaultValue="-1" Name="PrjImpId"></asp:SessionParameter>
                    <asp:Parameter DefaultValue="-1" Name="PrjReId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
</asp:Content>
