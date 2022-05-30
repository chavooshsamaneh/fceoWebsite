<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="AddTestObservers.aspx.cs" Inherits="Employee_Amoozesh_AddTestObservers" Title="مشخصات ناظرین آزمون" %>


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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" WorkDayCSS="PickerWorkDayCSS" WeekDayCSS="PickerWeekDayCSS" SelectedCSS="PickerSelectedCSS" HeaderCSS="PickerHeaderCSS" FrameCSS="PickerCSS" ForbidenCSS="PickerForbidenCSS" FooterCSS="PickerFooterCSS" CalendarDayWidth="50" CalendarCSS="PickerCalendarCSS">
            </pdc:PersianDateScriptManager>

            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>


                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0" width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False" Text=" "  EnableTheming="False" ToolTip="جدید" ID="BtnNew" EnableViewState="False" OnClick="BtnNew_Click">
                                                            <Image  Url="~/Images/icons/new.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False" Text=" "  Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit" EnableViewState="False" OnClick="btnEdit_Click">
                                                            <Image  Url="~/Images/icons/edit.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "  EnableTheming="False" ToolTip="ذخیره" ID="btnSave" EnableViewState="False" OnClick="btnSave_Click">
                                                            <Image  Url="~/Images/icons/save.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True" CausesValidation="False" Text=" "  EnableTheming="False" ToolTip="حذف" ID="btnDelete" EnableViewState="False" OnClick="btnDelete_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>

                                                            <Image  Url="~/Images/icons/delete.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False" Text=" "  EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click">
                                                            <Image  Url="~/Images/icons/Back.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>


                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table style="vertical-align: top; text-align: right" cellpadding="1" dir="rtl">
                            <tr>
                                <td style="vertical-align: top; text-align: right" colspan="4">
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="لطفآ ناظرین مورد نظر را انتخاب نمائید و سپس بر روی دکمه ذخیره کلیک نمائید." Visible="False">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: center" colspan="4">
                                    <br />
                                    <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                                        DataSourceID="ODBObserver" KeyFieldName="ObId">
                                        <Columns>
                                            <dxwgv:GridViewCommandColumn Caption="انتخاب" ShowSelectCheckbox="True" VisibleIndex="0">
                                            </dxwgv:GridViewCommandColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="کد ناظر" FieldName="ObId" Visible="False"
                                                VisibleIndex="1">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="نام و نام خانوادگی" FieldName="ObserverName"
                                                VisibleIndex="1">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="نام پدر" FieldName="Father" VisibleIndex="2">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="نوع عضویت" FieldName="MeTypeName" VisibleIndex="3">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="4">
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                        <SettingsPager>
                                            <AllButton Text="همه رکوردها">
                                            </AllButton>
                                            <FirstPageButton Text="اولین صفحه">
                                            </FirstPageButton>
                                            <LastPageButton Text="آخرین صفحه">
                                            </LastPageButton>
                                            <NextPageButton Text="صفحه بعد">
                                            </NextPageButton>
                                            <PrevPageButton Text="صفحه قبل">
                                            </PrevPageButton>
                                            <Summary Text="صفحه: {0} از {1} (تعداد کل:{2})" />
                                        </SettingsPager>
                                        <Settings ShowGroupPanel="True" />
                                        <SettingsText CommandCancel="انصراف" CommandClearFilter="پاک کردن فیلتر" CommandDelete="حذف"
                                            CommandEdit="ویرایش" CommandNew="جدید" CommandSelect="انتخاب" CommandUpdate="ذخیره"
                                            ConfirmDelete="آیا مطمئن به حذف این ردیف هستید؟" EmptyDataRow="هیچ داده ای وجود ندارد"
                                            GroupPanel="برای گروه بندی از این قسمت استفاده کنید" />
                                        <SettingsLoadingPanel Text="در حال بارگذاری" />
                                        <Styles>
                                            <Header HorizontalAlign="Center">
                                            </Header>
                                            <SelectedRow BackColor="White" ForeColor="Black">
                                            </SelectedRow>
                                            <GroupPanel BackColor="CornflowerBlue" ForeColor="Black">
                                            </GroupPanel>
                                        </Styles>
                                    </TSPControls:CustomAspxDevGridView>
                                    <asp:ObjectDataSource ID="ODBObserver" runat="server"
                                        SelectMethod="GetData" TypeName="TSP.DataManager.ObserverManager" OldValuesParameterFormatString="original_{0}" FilterExpression="InActive={0}">
                                        <FilterParameters>
                                            <asp:Parameter DefaultValue="0" Name="InActive" />
                                        </FilterParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                        </table>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0" width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False" Text=" "  EnableTheming="False" ToolTip="جدید" ID="BtnNew2" EnableViewState="False" OnClick="BtnNew_Click">
                                                            <Image  Url="~/Images/icons/new.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False" Text=" "  Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit2" EnableViewState="False" OnClick="btnEdit_Click">
                                                            <Image  Url="~/Images/icons/edit.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "  EnableTheming="False" ToolTip="ذخیره" ID="btnSave2" EnableViewState="False" OnClick="btnSave_Click">
                                                            <Image  Url="~/Images/icons/save.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True" CausesValidation="False" Text=" "  EnableTheming="False" ToolTip="حذف" ID="btnDelete2" EnableViewState="False" OnClick="btnDelete_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>

                                                            <Image  Url="~/Images/icons/delete.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False" Text=" "  EnableTheming="False" ToolTip="بازگشت" ID="ASPxButton6" EnableViewState="False" OnClick="btnBack_Click">
                                                            <Image  Url="~/Images/icons/Back.png"></Image>

                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <asp:HiddenField ID="PeriodId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="TestID" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0" BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>
