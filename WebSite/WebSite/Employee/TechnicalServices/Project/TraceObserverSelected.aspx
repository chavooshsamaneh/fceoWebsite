<%@ Page Title="پیگیری ارجاع کار به ناظران" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="TraceObserverSelected.aspx.cs" Inherits="Employee_TechnicalServices_Project_TraceObserverSelected" %>

<%@ Register Src="~/UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <script>
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
        function ClearSearch() {
            txtProjectNo.SetText("");
            ASPxComboBoxAgent.SetSelectedIndex(0);
            txtCreateDateFrom.SetText("");
            txtCreateDateTo.SetText("");

        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%--  <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>--%>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>

                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                            UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
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
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <fieldset id="Fieldset1">
                            <legend class="legendTitle" id="Legend1">حدود جستجو</legend>
                            <table width="100%">

                                <tr>
                                    <td align="right" style="width: 15%">
                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="کد پروژه">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" style="width: 35%">
                                        <TSPControls:CustomTextBox IsMenuButton="true" ID="txtProjectNo" runat="server" ClientInstanceName="txtProjectNo" Width="100%">
                                            <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="SearchValid">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RegularExpression ErrorText="کد پروژه را صحیح وارد نمایید" ValidationExpression="\d*" />
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" style="width: 15%">
                                        <asp:Label runat="server" Text="نمایندگی" ID="Label1"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 35%">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            TextField="Name" ID="ASPxComboBoxAgent" ClientInstanceName="ASPxComboBoxAgent" AutoPostBack="True" DataSourceID="ObjectdatasourceAgent"
                                            ValueType="System.Int32" ValueField="AgentId" RightToLeft="True"
                                            EnableIncrementalFiltering="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>

                                    </td>

                                </tr>
                                <tr>
                                    <td align="right" style="width: 104px">
                                        <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Text="تاریخ ثبت از">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right">
                                        <pdc:PersianDateTextBox ID="txtCreateDateFrom" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                            Width="300px" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                    </td>
                                    <td align="right">
                                        <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="تاریخ ثبت تا">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right">
                                        <pdc:PersianDateTextBox ID="txtCreateDateTo" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" RightToLeft="False" Style="direction: ltr; text-align: right;"
                                            Width="300px" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4" dir="ltr" valign="top">
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton ID="ASPxButton1" runat="server" AutoPostBack="true"
                                                        Text="پاک کردن فرم" UseSubmitBehavior="false">
                                                        <ClientSideEvents Click="function(s, e) {
	   	 ClearSearch();
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton ID="btnSearch" runat="server" AutoPostBack="true" OnClick="btnSearch_Click"
                                                        Text="جستجو" ClientInstanceName="btnSearch" Width="98px" UseSubmitBehavior="false">
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                        <fieldset id="Fieldset2">
                            <legend class="legendTitle" id="Legend2">دسته ها</legend>
                            <table width="100%">
                                <tr>
                                    <td align="right" style="width: 15%">اندازه دسته</td>
                                    <td align="right" style="width: 35%">
                                        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="PageSize_Changed">
                                            <asp:ListItem Text="5" Value="5" />
                                            <asp:ListItem Text="10" Value="10" />
                                            <asp:ListItem Text="20" Value="20" />
                                            <asp:ListItem Text="30" Value="30" />
                                        </asp:DropDownList></td>
                                    <td align="right" style="width: 15%">شماره دسته ی انتخابی</td>
                                    <td align="right" style="width: 35%"> <dxe:ASPxLabel ID="lblPageNo" runat="server" Text="">
                                        </dxe:ASPxLabel></td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">شماره دسته</td>
                                    <td align="right" style="width: 35%"></td>
                                    <td align="right" style="width: 15%"></td>
                                    <td align="right" style="width: 35%"></td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4">
                                        <asp:Repeater ID="rptPager" runat="server">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <%--          <div align="right">
                <ul class="HelpUL">
                    <li></li>
            </div>--%>
            <br />
            <TSPControls:CustomAspxDevGridView Width="100%" ID="GridViewTraceObserverSelected" runat="server"
                ClientInstanceName="grid" EnableViewState="False" KeyFieldName="Id"  OnPageIndexChanged="GridViewTraceObserverSelected_PageIndexChanged" AutoGenerateColumns="false">
                <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
                <SettingsText Title="لیست پیگیری ارجاع کار به ناظران-فیلتر ها بر روی هر دسته جداگانه اعمال می شوند" />
                <SettingsCookies Enabled="true" StoreFiltering="true" StoreColumnsWidth="true" StoreColumnsVisiblePosition="true" />
                <SettingsCustomizationWindow Enabled="True" />
                <Settings ShowTitlePanel="true" ShowHorizontalScrollBar="true" VerticalScrollBarMode="Visible" VerticalScrollBarStyle="VirtualSmooth" VerticalScrollableHeight="700" />
                <SettingsPager Mode="EndlessPaging">
                </SettingsPager>

                <Columns>
                     <dxwgv:GridViewDataTextColumn Caption="شماره ردیف" FieldName="RowNum" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="کد پروژه" FieldName="ProjectId" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="رشته" FieldName="IngridiantMajor" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ ارجاع" FieldName="Date" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="کد مرحله " FieldName="StageId" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تعداد ناظر در هر مرحله" FieldName="Count" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ عضویت" FieldName="MembershipDate" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ صلاحیت نظارت" FieldName="ObsDate" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="زمینه کاری" FieldName="WantedWorkType" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="گروه ساختمانی الف" FieldName="Group1" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="گروه ساختمانی ب" FieldName="Group2" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="گروه ساختمانی ج" FieldName="Group3" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="گروه ساختمانی د" FieldName="Group4" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شهر 1" FieldName="City1" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شهر 2" FieldName="City2" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تعداد کار باقیمانده" FieldName="CountRemainWorkCount" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ظرفیت کل" FieldName="TotalCapacity" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ظرفیت نظارت" FieldName="CapacityObs" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ظرفیت طراحی" FieldName="CapacityDesign" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ظرفیت پر شده" FieldName="UsedCapacity" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                       <dxwgv:GridViewDataTextColumn Caption="در صد ظرفیت پر شده نظارت" FieldName="PercentOfCapacityUsage" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    
                    <dxwgv:GridViewDataTextColumn Caption="ظرفیت باقی مانده" FieldName="RemainCapacity" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ظرفیت باقی مانده نظارت" FieldName="RemainCapacityObs" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ظرفیت واقعی باقی مانده نظارت" FieldName="RemainCapacityObsReal" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>
            <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewTraceObserverSelected">
            </dxwgv:ASPxGridViewExporter>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>


                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                            UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/ExportExcel.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>


                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <%--  <asp:ObjectDataSource ID="ObjdsFunctionALogs" runat="server" TypeName="TSP.DataManager.TechnicalServices.TSFunctionALogsManager"
                SelectMethod="GetData"></asp:ObjectDataSource>--%>
            <asp:ObjectDataSource ID="ObjectdatasourceAgent" runat="server" SelectMethod="FindByCode"
                TypeName="TSP.DataManager.AccountingAgentManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="AgentId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <dxhf:ASPxHiddenField ID="HiddenFieldPage" runat="server" ClientInstanceName="HiddenFieldPage">
            </dxhf:ASPxHiddenField>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

