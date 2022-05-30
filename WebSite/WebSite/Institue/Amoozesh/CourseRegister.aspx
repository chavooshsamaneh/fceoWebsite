<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="CourseRegister.aspx.cs" Inherits="Institue_Amoozesh_CourseRegister"
    Title="مدیریت ثبت نام دوره" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="center" style="width: 100%">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="width: 100%" dir="rtl" align="center">
                    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                        [<a class="closeLink" href="#">بستن</a>]
                    </div>
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelHeader" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <div dir="rtl" style="width: 100%">
                                    <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew" runat="server" AutoPostBack="False" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False"
                                                        OnClick="btnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False" Width="25px">
                                                        <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" AutoPostBack="False"
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                                        ToolTip="مشاهده" UseSubmitBehavior="False" Width="25px">
                                                        <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" AutoPostBack="False"
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                                        ToolTip="حذف" UseSubmitBehavior="False" Width="25px">
                                                        <ClientSideEvents Click="function(s, e) {e.processOnServer=confirm('آیا از حذف رکورد انتخاب شده مطمئن هستید؟');}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="../../Images/icons/delete.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>
                    <br />
                    <TSPControls:CustomAspxDevGridView ID="GridViewCourseRegister" runat="server"
                        AutoGenerateColumns="False" DataSourceID="ObjdsPeriodRegister"
                        KeyFieldName="PRId" OnHtmlDataCellPrepared="GridViewCourseRegister_HtmlDataCellPrepared"
                        OnAutoFilterCellEditorInitialize="GridViewCourseRegister_AutoFilterCellEditorInitialize">
                        <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True" ColumnResizeMode="Control"></SettingsBehavior>
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="نوع عضویت" FieldName="MeType" VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeNo" VisibleIndex="1"
                                Width="80px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="2">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="3">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="عنوان دوره" FieldName="PeriodTitle" VisibleIndex="4"
                                Width="200px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="کد دوره" FieldName="PPCode" VisibleIndex="5">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="تاریخ ثبت نام" FieldName="RegisterDate" VisibleIndex="6"
                                Width="100px">
                                <CellStyle HorizontalAlign="Right" Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="تاریخ شروع دوره" FieldName="StartDate" VisibleIndex="7"
                                Width="100px">
                                <CellStyle HorizontalAlign="Right" Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان دوره" FieldName="EndDate" VisibleIndex="8"
                                Width="100px">
                                <CellStyle HorizontalAlign="Right" Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="20" FieldName="FirstMark" Caption="نمره اولیه"
                                Width="100px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="21" FieldName="LastMark" Caption="نمره نهایی"
                                Width="100px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="22" FieldName="statusName" Caption="نتیجه پایانی"
                                Width="100px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="23" Width="30px" ShowClearFilterButton="true">
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نوع ثبت نام" FieldName="RgstType" VisibleIndex="7">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نحوه پرداخت" FieldName="PayType" VisibleIndex="7">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="RegInActiveName" VisibleIndex="7">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="وضعیت پرداخت" FieldName="StatusAcount" VisibleIndex="7">
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
                            <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="FollowNumber" Caption="شناسه پرداخت"
                                Width="100px">
                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="ReferenceId" Caption="کدرهگیری بانکی"
                                Width="100px">
                                <CellStyle Wrap="False" HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                    </TSPControls:CustomAspxDevGridView>
                    <br />
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelFooter" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <div dir="rtl" style="width: 100%">
                                    <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew2" runat="server" AutoPostBack="False" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False"
                                                        OnClick="btnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False" Width="25px">
                                                        <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server" AutoPostBack="False"
                                                        EnableTheming="False" EnableViewState="False" Text=" " OnClick="btnView_Click"
                                                        ToolTip="مشاهده" UseSubmitBehavior="False" Width="25px">
                                                        <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" AutoPostBack="False"
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                                        ToolTip="حذف" UseSubmitBehavior="False" Width="25px">
                                                        <ClientSideEvents Click="function(s, e) {e.processOnServer=confirm('آیا از حذف رکورد انتخاب شده مطمئن هستید؟');}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="../../Images/icons/delete.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxhf:ASPxHiddenField ID="HiddenFieldCourseRegister" runat="server">
                                    </dxhf:ASPxHiddenField>
                                </div>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>

                    <asp:ObjectDataSource ID="ObjdsPeriodRegister" runat="server" TypeName="TSP.DataManager.PeriodRegisterManager"
                        SelectMethod="SelectPeriodRegister" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:Parameter Type="Int32" Name="PRId" DefaultValue="-1"></asp:Parameter>
                            <asp:Parameter Type="Int32" Name="MeId" DefaultValue="-1"></asp:Parameter>
                            <asp:Parameter Type="Int32" Name="PPId" DefaultValue="-1"></asp:Parameter>
                            <asp:Parameter Type="Int32" Name="InsId" DefaultValue="-1"></asp:Parameter>
                            <asp:Parameter DefaultValue="-1" Name="IsSeminar" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
