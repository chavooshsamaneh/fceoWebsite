<%@ Page Title="مدیریت اسناد پرداخت الکترونیکی" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="EpaymentFishes.aspx.cs" Inherits="EPayment_EpaymentFishes" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

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
    </script>
    <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelPage" runat="server" ClientInstanceName="CallbackPanelPage"
        OnCallback="CallbackPanelPage_Callback" LoadingPanelImage-Url="~/Image/indicator.gif">
        <ClientSideEvents EndCallback="function(s, e) {
                                     if(s.cpPrint==1)
                                     {
                                        s.cpPrint=0;
                                        window.open(s.cpPrintUrl);
                                     }
                                     }" />
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent3" runat="server">
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                    visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent1" runat="server">
                                        <table >
                                            <tr>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server"  EnableTheming="False"
                                                        EnableViewState="False" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False" AutoPostBack="false">
                                                        <Image  Url="~/Images/icons/view.png" Width="25px" />
                                                        <ClientSideEvents Click="function(s,e){CallbackPanelPage.PerformCallback('View');}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                                        CausesValidation="False" ID="btnPrint" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" AutoPostBack="false">
                                                        
                                                        <Image  Url="~/Images/icons/printers.png">
                                                        </Image>
                                                        <ClientSideEvents Click="function(s,e){ 
	CallbackPanelPage.PerformCallback('Print');
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False" 
                                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                                        UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/ExportExcel.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
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
                                    <td align="right" valign="top" width="15%">
                                        کد عضویت
                                    </td>
                                    <td align="right" valign="top" width="35%">
                                        <TSPControls:CustomTextBox ID="txtMeId" runat="server" ClientInstanceName="txtMeId" 
                                              Width="100%">
                                            <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1,btnSearch);}" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="SearchValid">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت را صحیح وارد نمایید" />
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top" width="15%">
                                        وضعیت پرداخت
                                    </td>
                                    <td align="right" valign="top" width="35%">
                                        <TSPControls:CustomAspxComboBox runat="server" PopupVerticalAlign="Below"  Width="100%"
                                            IncrementalFilteringMode="StartsWith" TextField="StatusName" ID="cmbPaymentStatus"
                                             EnableClientSideAPI="True" ValueType="System.String"
                                            ValueField="Status" ClientInstanceName="cmbPaymentStatus" 
                                            EnableIncrementalFiltering="True" RightToLeft="True">
                                            <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <Items>
                                                <dxe:ListEditItem Text="-------------------" Selected="true" />
                                                <dxe:ListEditItem Text="ثبت در سیستم" Value="1" />
                                                <dxe:ListEditItem Text="پرداخت" Value="3" />
                                            </Items>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <dxp:ASPxPanel ID="PanelSearchPeriodReg" runat="server" Width="100%">
                                            <PanelCollection>
                                                <dxp:PanelContent>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="right" valign="top" width="15%">
                                                                دوره
                                                            </td>
                                                            <td align="right" valign="top" width="35%">
                                                                <TSPControls:CustomAspxComboBox runat="server" PopupVerticalAlign="Below"  Width="100%"
                                                                    IncrementalFilteringMode="Contains" TextField="PPCode" ID="cmbPeriods" 
                                                                    DataSourceID="OdbPeriod" EnableClientSideAPI="True" ValueType="System.String"
                                                                    ValueField="PPId" ClientInstanceName="cmbPeriods" 
                                                                    EnableIncrementalFiltering="True" RightToLeft="True">
                                                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ButtonStyle Width="13px">
                                                                    </ButtonStyle>
                                                                    <Columns>
                                                                        <dxe:ListBoxColumn FieldName="PPCode" Caption="کد دوره" Width="100px" />
                                                                        <dxe:ListBoxColumn FieldName="PeriodTitle" Caption="نام دوره" Width="300px" />
                                                                    </Columns>
                                                                </TSPControls:CustomAspxComboBox>
                                                                <asp:ObjectDataSource ID="OdbPeriod" runat="server" SelectMethod="SelectPeriodPresentHasEpayment"
                                                                    TypeName="TSP.DataManager.PeriodPresentManager">
                                                                    <SelectParameters>
                                                                        <asp:Parameter DefaultValue="false" Name="IsFill" Type="Boolean" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                            <td align="right" valign="top" width="15%">
                                                            </td>
                                                            <td align="right" valign="top" width="35%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top" colspan="2">
                                        <TSPControls:CustomAspxButton runat="server" Text="جستجو"  ID="btnSearch" AutoPostBack="False"
                                            UseSubmitBehavior="False"  Width="98px"
                                            ClientInstanceName="btnSearch">
                                            <ClientSideEvents Click="function(s, e) {
		CallbackPanelPage.PerformCallback('Search');
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top" colspan="2">
                                        <TSPControls:CustomAspxButton runat="server" Text="پاک کردن فرم"  ID="ASPxButton1"
                                            AutoPostBack="False" UseSubmitBehavior="False" 
                                            >
                                            <ClientSideEvents Click="function(s, e) {
	CallbackPanelPage.PerformCallback('Clear');
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewAccounting"
                    ExportedRowType="All">
                </dxwgv:ASPxGridViewExporter>
                <TSPControls:CustomAspxDevGridView runat="server"  Width="100%"
                    ID="GridViewAccounting" KeyFieldName="AccountingId" AutoGenerateColumns="False"
                     ClientInstanceName="grid" DataSourceID="ObjdsEpayment"
                    OnHtmlDataCellPrepared="GridViewAccounting_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="GridViewAccounting_AutoFilterCellEditorInitialize">
                    <Settings ShowHorizontalScrollBar="true" ShowFooter="true" />
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem FieldName="Amount" SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem FieldName="FishPayerId" SummaryType="Count" />
                    </TotalSummary>
                    <SettingsCookies Enabled="false" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FishPayerId" Name="FishPayerId"
                            Caption="کد پرداخت کننده پرداخت کننده">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FishPayerMembershipType"
                            Name="FishPayerMembershipType" Caption="نوع کد عضویت">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TMeId" Name="TMeId" Caption="کد عضویت موقت"
                            Visible="false">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FishPayerName" Caption="پرداخت کننده">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="AccTypeName" Caption="بابت"
                            Width="350px">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="StatusName" Caption="وضعیت پرداخت"
                            Width="100px">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Amount" Caption="مبلغ (ريال)"
                            Width="200px">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                            <PropertiesTextEdit DisplayFormatString="#,#">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Date" Name="Date" Caption="تاریخ"
                            Width="80px">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="FollowNumber" Caption="کد رهگیری در سامانه"
                            Width="100px">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ReferenceId" Caption="کدرهگیری بانکی"
                            Width="100px">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TypeName" Caption="نحوه پرداخت"
                            Width="100px">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="InActiveName" Caption="وضعیت ثبت"
                            Width="100px">
                            <CellStyle Wrap="False" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" ExportMode="None" />
                    <Templates>
                        <DetailRow>
                            <TSPControls:CustomAspxDevGridView ID="GridViewAccountingDetails" runat="server"
                                ClientInstanceName="GridViewAccountingDetails" AutoGenerateColumns="False" 
                                 KeyFieldName="AccDetailId" Width="100%" OnBeforePerformDataSelect="GridViewAccountingDetails_BeforePerformDataSelect"
                                DataSourceID="ObjdsEpaymentDetail">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="PayTypeName" Name="PayTypeName"
                                        Caption="عنوان دوره" Width="300px">
                                        <CellStyle Wrap="False" HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Amount" Name="Amount" Caption="مبلغ"
                                        Width="100px">
                                        <PropertiesTextEdit DisplayFormatString="#,#">
                                        </PropertiesTextEdit>
                                        <CellStyle Wrap="False" HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="InActiveName" Caption="وضعیت ثبت"
                                        Width="100px">
                                        <CellStyle Wrap="False" HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView>
                        </DetailRow>
                    </Templates>
                </TSPControls:CustomAspxDevGridView>
              
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelMenu2" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent2" runat="server">
                                        <table >
                                            <tr>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnViwe2" runat="server"  EnableTheming="False"
                                                        EnableViewState="False" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False" AutoPostBack="false">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/view.png" Width="25px" />
                                                        <ClientSideEvents Click="function(s,e){CallbackPanelPage.PerformCallback('View');}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                                        CausesValidation="False" ID="btnPrint2" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" AutoPostBack="false">
                                                       
                                                        <Image  Url="~/Images/icons/printers.png">
                                                        </Image>
                                                        <ClientSideEvents Click="function(s,e){ 
	CallbackPanelPage.PerformCallback('Print');
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False" 
                                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                                        UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                                        
                                                        <Image  Url="~/Images/icons/ExportExcel.png"  />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                                </td>
                                            </tr>
                                        </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ObjectDataSource ID="ObjdsEpayment" runat="server" SelectMethod="SelectAccountingForEpayemnt"
                    TypeName="TSP.DataManager.TechnicalServices.AccountingManager">
                    <SelectParameters>
                        <asp:Parameter DbType="Int32" Name="TableTypeId" DefaultValue="-1" />
                        <asp:Parameter DbType="Int32" Name="AccType" DefaultValue="-1" />
                        <asp:Parameter DbType="Int32" Name="Status" DefaultValue="-1" />
                        <asp:Parameter DbType="Int32" Name="FishPayerId" DefaultValue="-1" />
                        <asp:Parameter DbType="Int32" Name="PPId" DefaultValue="-1" />
                        <asp:Parameter DbType="Int32" Name="CrsId" DefaultValue="-1" />
                        <asp:Parameter DbType="String" Name="AccTypeList" DefaultValue="0" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjdsEpaymentDetail" runat="server" SelectMethod="SelectAccDetailByAccountingId"
                    TypeName="TSP.DataManager.TechnicalServices.AccountingDetailManager">
                    <SelectParameters>
                        <asp:SessionParameter DbType="Int32" Name="AccountingId" SessionField="AccountingId"
                            DefaultValue="-1" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <dx:ASPxHiddenField ID="HiddenFieldEpayment" runat="server">
                </dx:ASPxHiddenField>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomAspxCallbackPanel>
</asp:Content>
