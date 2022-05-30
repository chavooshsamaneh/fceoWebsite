<%@ Page Title="شرکت کنندگان سمینار" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="SeminarAttender.aspx.cs" Inherits="Employee_Amoozesh_SeminarAttender" %>


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
<%@ Register Src="../../UserControl/SeminarInfoUserControl.ascx" TagName="SeminarInfoUserControl"
    TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" WorkDayCSS="PickerWorkDayCSS"
        WeekDayCSS="PickerWeekDayCSS" SelectedCSS="PickerSelectedCSS" HeaderCSS="PickerHeaderCSS"
        FrameCSS="PickerCSS" ForbidenCSS="PickerForbidenCSS" FooterCSS="PickerFooterCSS"
        CalendarDayWidth="50" CalendarCSS="PickerCalendarCSS">
    </pdc:PersianDateScriptManager>
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                    EnableTheming="False" ToolTip="ثبت نام جدید" ID="btnNew" EnableViewState="False"
                                    OnClick="btnNew_Click" Visible="false">
                                    <Image Url="~/Images/icons/new.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                    EnableTheming="False" ToolTip="ویرایش" ID="btnEdit" EnableViewState="False" OnClick="btnEdit_Click" Visible="false">
                                    <Image Url="~/Images/icons/edit.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                    EnableTheming="False" ToolTip="مشاهده" ID="btnView" EnableViewState="False" OnClick="btnView_Click">
                                    <Image Url="~/Images/icons/view.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>

                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    Text=" " EnableTheming="False" ToolTip="لغو ثبت نام"
                                    ID="btnInActive2" EnableViewState="False" OnClick="btnInActive_Click">
                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به لغو ثبت نام این ردیف هستید؟');
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/delete.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>


                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    Text=" " EnableTheming="False" ToolTip="حضور"
                                    ID="btnPresent" EnableViewState="False" OnClick="btnPresent_Click">
                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حضور این فرد هستید؟');
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/Present.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    Text=" " EnableTheming="False" ToolTip="غیبت"
                                    ID="btnAbcent" EnableViewState="False" OnClick="btnPresent_Click">
                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به غیبت این فرد هستید؟');
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/abcent.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>


                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی Excel"
                                    ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnExportExcel_Click">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/ExcelDownload.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>


                            <td>

                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " AutoPostBack="false" ToolTip="حضار Excel"
                                    ID="btnPresentExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False">

                                    <ClientSideEvents Click="function(s, e) {
                                     lblResult.SetText('');
	                                 PopupPresentExcel.Show();
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/ExcelUpload.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>

                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                </TSPControls:MenuSeprator>
                            </td>

                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                    EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click">
                                    <Image Url="~/Images/icons/Back.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <uc1:SeminarInfoUserControl ID="SeminarInfoUserControl" runat="server" />
    <br />
    <div align="right" width="100%">
        <ul class="HelpUL">
            <li>پس از لغو ثبت نام امکان تغییر وضعیت مجدد وجود ندارد.لطفا در انجام این عملیات دقت
                لازم نمایید. </li>
            <li>در صورت لغو ثبت نام و درصورت تمایل مجدد عضو به شرکت در دوره بایستی مجددا ثبت نام
                نماید. </li>
            <li>لطفا در صورت لغو ثبت نام هر یک از اعضا اطلاعات پرداخت شخص شامل کد رهگیری و شناسه
                پرداخت را جهت برگشت هزینه پرداختی به واحد مالی اعلام نمایید. </li>
        </ul>
    </div>
    <TSPControls:CustomAspxDevGridView ID="GridViewPeriodRegister" runat="server" Width="100%"
        KeyFieldName="PRId" DataSourceID="ObjdsPeriodRegister" AutoGenerateColumns="False">
        <Settings ShowHorizontalScrollBar="true" ShowFooter="true" />
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
            <dxwgv:GridViewDataTextColumn Caption="حضور" FieldName="RegIsPresent" VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تعداد جلسات حضور" FieldName="TotalTimePresent" VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="هزینه سمینار" FieldName="SeminarCostType" VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نحوه پرداخت" FieldName="PayType" VisibleIndex="7">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="RegInActiveName" VisibleIndex="7">
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
            <dxwgv:GridViewCommandColumn Caption=" " ShowClearFilterButton="true" VisibleIndex="7" Width="30px">
            </dxwgv:GridViewCommandColumn>
        </Columns>

    </TSPControls:CustomAspxDevGridView>
    <dx:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewPeriodRegister">
    </dx:ASPxGridViewExporter>
    <asp:ObjectDataSource ID="ObjdsPeriodRegister" runat="server" TypeName="TSP.DataManager.PeriodRegisterManager"
        SelectMethod="SelectPeriodRegisterForSeminar">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="PRId" />
            <asp:Parameter DefaultValue="-1" Name="PPId" />
            <asp:Parameter DefaultValue="-1" Name="MeId" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                    cellpadding="0">
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                    EnableTheming="False" ToolTip="ثبت نام جدید" ID="btnNew2" EnableViewState="False"
                                    OnClick="btnNew_Click" Visible="false">
                                    <Image Url="~/Images/icons/new.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                    EnableTheming="False" ToolTip="ویرایش" ID="btnEdit2" EnableViewState="False"
                                    OnClick="btnEdit_Click" Visible="false">
                                    <Image Url="~/Images/icons/edit.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                    EnableTheming="False" ToolTip="مشاهده" ID="btnView2" EnableViewState="False"
                                    OnClick="btnView_Click">
                                    <Image Url="~/Images/icons/view.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>

                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    Text=" " EnableTheming="False" ToolTip="لغو ثبت نام"
                                    ID="btnInActive" EnableViewState="False" OnClick="btnInActive_Click">
                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به لغو ثبت نام این ردیف هستید؟');
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/delete.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    Text=" " EnableTheming="False" ToolTip="حضور"
                                    ID="btnPresent2" EnableViewState="False" OnClick="btnPresent_Click">
                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حضور این فرد هستید؟');
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/Present.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    Text=" " EnableTheming="False" ToolTip="غیبت"
                                    ID="btnAbcent2" EnableViewState="False" OnClick="btnPresent_Click">
                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به غیبت این فرد هستید؟');
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/abcent.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی Excel"
                                    ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnExportExcel_Click">
                                    <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/ExcelDownload.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>

                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " AutoPostBack="false" ToolTip="حضار Excel"
                                    ID="btnPresentExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False">

                                    <ClientSideEvents Click="function(s, e) {
                                    lblResult.SetText('');
	                                PopupPresentExcel.Show();
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/ExcelUpload.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>

                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                    EnableTheming="False" ToolTip="بازگشت" ID="ASPxButton6" EnableViewState="False"
                                    OnClick="btnBack_Click">
                                    <Image Url="~/Images/icons/Back.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:HiddenField ID="HiddenSeId" runat="server" Visible="False"></asp:HiddenField>


    <TSPControls:CustomASPxPopupControl ID="PopupPresentExcel" runat="server" ClientInstanceName="PopupPresentExcel"
     HeaderText="بارگذاری فایل حضار"  Width="435px">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <div dir="rtl">
                    <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelPresentExcel" runat="server" ClientInstanceName="CallbackPanelPresentExcel"
                        OnCallback="CallbackPanelPresentExcel_Callback" Width="100%"
                        RightToLeft="True">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td width="100%" colspan="2">
                                                <ul class="HelpUL">

                                                    <li>فایل اکسل باید شامل یک شیت با نام Sheet1 و آن شیت شامل یک ستون که محتوی کدهای عضویت هست با سر ستونی با نام  meid وستون دیگر حاوی تعداد جلسات حضور با سر ستونی با نام Sessions باشد </li>
                                                    <li>پس از انتخاب دکمه ذخیره تنهااثرات نتیجه پردازش فایل اکسل در پایگاه داده ذخیره می شود و فایل اکسل و خلاصه نتیجه پردازش توسط کاربران قابل بازیابی نمی باشد</li>
                                                </ul>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%">فایل اکسل حضار
                                            </td>
                                            <td width="85%">

                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                    ID="flpExcel" InputType="Files" ClientInstanceName="flpExcel" OnFileUploadComplete="flpExcel_FileUploadComplete">
                                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	    imgEndUploadExcelPresent.SetVisible(true);
	    HiddenFieldInfo.Set('Check',1);
      
	    lblExcelPresentImagevalidation.SetVisible(false);
		hplExcel.SetNavigateUrl('../../Image/Amoozesh/ExcelPresent/'+e.callbackData);
        HiddenFieldInfo.Set('FileUpload','~/Image/Amoozesh/ExcelPresent/'+e.callbackData);
                                                            
        HiddenFieldInfo.Set('ExcelMeList',   flpExcel.cpExcelMeList);
                                                                                                           
	}
	else{
	   imgEndUploadExcelPresent.SetVisible(false);
	   lblExcelPresentImagevalidation.SetVisible(true);
	   hplExcel.SetNavigateUrl('');
	}
     lblResult.SetText(flpExcel.cpRMessage);
}"></ClientSideEvents>
                                                                    <ValidationSettings AllowedFileExtensions=".xlsx"></ValidationSettings>
                                                                </TSPControls:CustomAspxUploadControl>
                                                                <dxe:ASPxLabel ID="ASPxLabel19" runat="server" ClientInstanceName="lblExcelPresentImagevalidation" ClientVisible="False"
                                                                    ForeColor="Red" Text="فایل را انتخاب نمایید">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="فایل انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                    ID="ASPxImage3" ClientInstanceName="imgEndUploadExcelPresent">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <dxe:ASPxHyperLink runat="server" Text="فایل حضار" ID="hplExcel" ClientInstanceName="hplExcel">
                                                </dxe:ASPxHyperLink>
                                                <br />
                                                <dxe:ASPxLabel ID="ASPxLabelImgWarning" runat="server" ForeColor="#0000C0" Text="">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">خلاصه نتیجه پردازش
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomASPXMemo runat="server" Text="" Height="120px" ID="lblResult" ClientInstanceName="lblResult" ReadOnly="true" Width="100%"></TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table width="100%">
                                                    <tr>
                                                        <td width="50%" align="center">
                                                            <TSPControls:CustomAspxButton runat="server" Text="&nbsp;&nbsp;ذخیره"
                                                                ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False"
                                                                Width="120px" ClientInstanceName="btnSave">
                                                                <ClientSideEvents Click="function(s, e) {	

CallbackPanelPresentExcel.PerformCallback();

}"></ClientSideEvents>
                                                                <Image Width="16px" Height="16px" Url="~/Images/WorkFlow_Save.png" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td width="50%" align="center">
                                                            <TSPControls:CustomAspxButton  runat="server" Text="&nbsp;&nbsp;خروج"
                                                                ID="btnClose" AutoPostBack="False" UseSubmitBehavior="False"
                                                                Width="120px" ClientInstanceName="btnSenNextStep">
                                                                <ClientSideEvents Click="function(s, e) {		                                    
	                                    PopupPresentExcel.Hide();
                                       
                                    }"></ClientSideEvents>
                                                                <Image Width="16px" Height="16px" Url="~/Images/Close-box-red.png" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomAspxCallbackPanel>
                </div>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
        <HeaderStyle HorizontalAlign="Center" Wrap="False">
            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
        </HeaderStyle>
        <SizeGripImage Height="12px" Width="12px" />
        <CloseButtonImage Height="17px" Width="17px" />
    </TSPControls:CustomASPxPopupControl>

    <dxhf:ASPxHiddenField ID="HiddenFieldInfo" ClientInstanceName="HiddenFieldInfo" runat="server">
    </dxhf:ASPxHiddenField>

</asp:Content>



