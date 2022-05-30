<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPagePortals.master"
    CodeFile="PeriodAttender.aspx.cs" Inherits="Employee_Amoozesh_Observer" Title="شرکت کنندگان در دوره" %>

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
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" WorkDayCSS="PickerWorkDayCSS"
        WeekDayCSS="PickerWeekDayCSS" SelectedCSS="PickerSelectedCSS" HeaderCSS="PickerHeaderCSS"
        FrameCSS="PickerCSS" ForbidenCSS="PickerForbidenCSS" FooterCSS="PickerFooterCSS"
        CalendarDayWidth="50" CalendarCSS="PickerCalendarCSS">
    </pdc:PersianDateScriptManager>
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]</div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                                <table >
                                    <tbody>
                                        <tr>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                    EnableTheming="False" ToolTip="ثبت نام جدید" ID="btnNew" EnableViewState="False"
                                                    OnClick="btnNew_Click">
                                                    <Image  Url="~/Images/icons/new.png">
                                                    </Image>
                                                 
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                    EnableTheming="False" ToolTip="ویرایش" ID="btnEdit" EnableViewState="False" OnClick="btnEdit_Click">
                                                    <Image  Url="~/Images/icons/edit.png">
                                                    </Image>
                                                  
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                    EnableTheming="False" ToolTip="مشاهده" ID="btnView" EnableViewState="False" OnClick="btnView_Click">
                                                    <Image  Url="~/Images/icons/view.png">
                                                    </Image>
                                                  
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                                </TSPControls:MenuSeprator>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                                    Text=" "  EnableTheming="False" ToolTip="لغو ثبت نام"
                                                    ID="btnInActive2" EnableViewState="False" OnClick="btnInActive_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به لغو ثبت نام این ردیف هستید؟');
}"></ClientSideEvents>
                                                    <Image  Url="~/Images/icons/delete.png">
                                                    </Image>
                                                   
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4">
                                                </TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                                    ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnExportExcel_Click">
                                                  
                                                    <Image  Url="~/Images/icons/ExportExcel.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                                </TSPControls:MenuSeprator>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                    EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click">
                                                    <Image  Url="~/Images/icons/Back.png">
                                                    </Image>
                                                   
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <div align="right" width="100%">
        <ul class="HelpUL">
            <li>پس از لغو ثبت نام امکان تغییر وضعیت مجدد وجود ندارد.لطفا در انجام این عملیات دقت
                لازم نمایید. </li>
            <li>در صورت لغو ثبت نام و درصورت تمایل مجدد عضو به شرکت در دوره بایستی مجددا ثبت نام
                نماید. </li>
            <li>لطفا در صورت لغو ثبت نام هر یک از اعضا اطلاعات پرداخت شخص شامل کد رهگیری و شناسه
                پرداخت را جهت برگشت هزینه پرداختی به واحد مالی اعلام نمایید. </li>
        </ul>
        <asp:Label runat="server" Text="" Font-Bold="true" ID="lblPeriodName"></asp:Label>
    </div>
    <TSPControls:CustomAspxDevGridView ID="GridViewPeriodRegister" runat="server" Width="100%"
        KeyFieldName="PRId" DataSourceID="ObjdsPeriodRegister" AutoGenerateColumns="False"  >
        <%--<SettingsCookies Enabled="false" />--%>
        <Settings ShowHorizontalScrollBar="true" ShowFooter="true"  />
        
        <TotalSummary>
            <dxwgv:ASPxSummaryItem FieldName="FishAmount" SummaryType="Sum" />
            <dxwgv:ASPxSummaryItem FieldName="Amount" SummaryType="Sum" />
            <dxwgv:ASPxSummaryItem FieldName="MeId" SummaryType="Count" />
        </TotalSummary>
        <Columns>
            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" Width="100px" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نوع عضویت" FieldName="MeType" VisibleIndex="0"
                Width="200px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1"
                Width="200px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2"
                Width="200px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نوع ثبت نام" FieldName="RgstType" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نحوه پرداخت" FieldName="PayType" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت ثبت نام" FieldName="RegInActiveName" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت پرداخت" FieldName="StatusName" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره فیش" FieldName="FishNumber" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ پرداخت" FieldName="PaymentDate" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="مبلغ فیش (ريال)" FieldName="FishAmount" VisibleIndex="7">
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="مبلغ فیش بابت دوره (ريال)" FieldName="Amount"
                VisibleIndex="7">
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="FollowNumber" Caption="شناسه پرداخت"
                Width="100px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ReferenceId" Caption="کدرهگیری بانکی"
                Width="100px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ساعات غیبت" FieldName="TotalTimePresent" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نمره" FieldName="FirstMark" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نمره نهایی" FieldName="LastMark" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت قبولی" FieldName="statusFinish" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="7" Width="30px" ShowClearFilterButton="true">
              
            </dxwgv:GridViewCommandColumn>
        </Columns>

    </TSPControls:CustomAspxDevGridView>
    <dx:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewPeriodRegister">
    </dx:ASPxGridViewExporter>
    <asp:ObjectDataSource ID="ObjdsPeriodRegister" runat="server" TypeName="TSP.DataManager.PeriodRegisterManager"
        SelectMethod="SelectPeriodRegisterForPeriod">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="PRId" />
            <asp:Parameter DefaultValue="-1" Name="PPId" />
            <asp:Parameter DefaultValue="-1" Name="MeId" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsEpaymentDetail" runat="server" SelectMethod="SelectAccDetailByTableId"
        TypeName="TSP.DataManager.TechnicalServices.AccountingDetailManager">
        <SelectParameters>
            <asp:SessionParameter DbType="Int32" Name="TableId" SessionField="TableId" DefaultValue="-1" />
            <asp:Parameter DbType="Int32" Name="TableId" DefaultValue="-1" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                                <table >
                                    <tbody>
                                        <tr>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                    EnableTheming="False" ToolTip="ثبت نام جدید" ID="btnNew2" EnableViewState="False"
                                                    OnClick="btnNew_Click">
                                                    <Image  Url="~/Images/icons/new.png">
                                                    </Image>
                                                  
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                    EnableTheming="False" ToolTip="ویرایش" ID="btnEdit2" EnableViewState="False"
                                                    OnClick="btnEdit_Click">
                                                    <Image  Url="~/Images/icons/edit.png">
                                                    </Image>
                                                   
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                    EnableTheming="False" ToolTip="مشاهده" ID="btnView2" EnableViewState="False"
                                                    OnClick="btnView_Click">
                                                    <Image  Url="~/Images/icons/view.png">
                                                    </Image>
                                                   
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2">
                                                </TSPControls:MenuSeprator>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                                    Text=" "  EnableTheming="False" ToolTip="لغو ثبت نام"
                                                    ID="btnInActive" EnableViewState="False" OnClick="btnInActive_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به لغو ثبت نام این ردیف هستید؟');
}"></ClientSideEvents>
                                                    <Image  Url="~/Images/icons/delete.png">
                                                    </Image>
                                                   
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5">
                                                </TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                                    ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnExportExcel_Click">
                                                    <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                                   
                                                    <Image  Url="~/Images/icons/ExportExcel.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3">
                                                </TSPControls:MenuSeprator>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                    EnableTheming="False" ToolTip="بازگشت" ID="ASPxButton6" EnableViewState="False"
                                                    OnClick="btnBack_Click">
                                                    <Image  Url="~/Images/icons/Back.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                          
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:HiddenField ID="HiddenPPId" runat="server" Visible="False"></asp:HiddenField>
</asp:Content>
