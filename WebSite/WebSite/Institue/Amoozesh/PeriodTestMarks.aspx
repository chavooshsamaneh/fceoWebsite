<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="PeriodTestMarks.aspx.cs" Inherits="Institue_Amoozesh_PeriodTestMarks"
    Title="ثبت نمرات" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>


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

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="DivReport" runat="server" class="DivErrors" dir="rtl" style="text-align: right"
                visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave1" runat="server" EnableTheming="False"
                                                            EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                                            <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </hoverstyle>
                                                            <image height="25px" url="~/Images/icons/save.png" width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnConfirm1" runat="server" EnableTheming="False"
                                                            EnableViewState="False" OnClick="btnConfirm_Click" Text=" " ToolTip="ثبت نهایی"
                                                            UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به ثبت نهایی نمرات هستید؟');
}" />
                                                            <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </hoverstyle>
                                                            <image height="25px" url="~/Images/icons/dvd_unmount.png" width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="ButtonRet1" runat="server" CausesValidation="False"
                                                            EnableTheming="False" EnableViewState="False" OnClick="ButtonRet_Click" Text=" "
                                                            ToolTip="بازگشت" UseSubmitBehavior="False">
                                                            <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </hoverstyle>
                                                            <image height="25px" url="~/Images/icons/Back.png" width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomAspxMenuHorizontal ID="AspxMenu1" runat="server" OnItemClick="AspxMenu1_ItemClick">
                <Items>
                    <dxm:MenuItem Name="InValid" Text="لغو دوره">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="TestMarks" Text="ثبت نمرات آزمون" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Costs" Text="هزینه های متفرقه">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Period" Text="مشخصات دوره">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>

            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel4" HeaderText="مشخصات مؤسسه" runat="server" ShowCollapseButton="true">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table dir="rtl" width="100%">
                            <tr>
                                <td align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="نام مؤسسه :">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right">
                                    <dxe:ASPxLabel ID="lblInsName" runat="server">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right">
                                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="مدیر مؤسسه :">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right">
                                    <dxe:ASPxLabel ID="lblManager" runat="server">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel3" HeaderText="اطلاعات پایه" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table dir="rtl" width="100%">
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="عنوان دوره">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" colspan="3">
                                    <TSPControls:CustomTextBox ID="txtPPTitle" runat="server"
                                        Width="264px" ReadOnly="True">
                                        <ValidationSettings>

                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="تاریخ اعتراض">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top">
                                    <pdc:PersianDateTextBox ID="txtDate" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                        PickerDirection="ToRight" ShowPickerOnTop="True" Width="130px"></pdc:PersianDateTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                        ErrorMessage="تاریخ را وارد نمایید"></asp:RequiredFieldValidator>
                                </td>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="نمره کل">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomTextBox ID="txtTotalMark" runat="server"
                                        Width="100px">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                            <RequiredField ErrorText="نمره کل را وارد نمایید" IsRequired="True" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="" />
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel ID="lblPStatus" runat="server" Text="وضعیت دوره" Visible="False">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomTextBox ID="txtPPStatus" runat="server"
                                        ReadOnly="True" Text="تأیید نهایی" Visible="False" Width="132px">
                                        <ValidationSettings>

                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td style="vertical-align: top; text-align: right"></td>
                                <td style="vertical-align: top; text-align: right"></td>
                            </tr>
                        </table>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                KeyFieldName="PRId"
                DataSourceID="ObjectDataSource1">
                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="PRId" Visible="False" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نمره" FieldName="LastMark" VisibleIndex="3">
                        <DataItemTemplate>
                            <TSPControls:CustomTextBox ID="txtMark" runat="server" Width="70px" Text='<%# Bind("LastMark") %>'>
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                    <RequiredField IsRequired="True" ErrorText="نمره را وارد نمایید"></RequiredField>
                                    <RegularExpression ErrorText=""></RegularExpression>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="5">
                        <DataItemTemplate>
                            <TSPControls:CustomASPXMemo ID="txtDesc" Text='<%# Bind("Description") %>' runat="server" Width="170px"                                
                                Height="22px">
                                <ValidationSettings>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomASPXMemo>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" FilterExpression="PPId={0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.PeriodRegisterManager">
                <FilterParameters>
                    <asp:Parameter Name="newparameter" />
                </FilterParameters>
            </asp:ObjectDataSource>
            <asp:HiddenField ID="PeriodId" runat="server" Visible="False" />
            <asp:HiddenField ID="InstitueId" runat="server" Visible="False" />
            <asp:HiddenField ID="HDPageMode" runat="server" Visible="False" />
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                            <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </hoverstyle>
                                            <image height="25px" url="~/Images/icons/save.png" width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnConfirm" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnConfirm_Click" Text=" " ToolTip="ثبت نهایی"
                                            UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به ثبت نهایی نمرات هستید؟');
}" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </hoverstyle>
                                            <image height="25px" url="~/Images/icons/dvd_unmount.png" width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton3" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="ButtonRet_Click" Text=" "
                                            ToolTip="بازگشت" UseSubmitBehavior="False">
                                            <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                        </hoverstyle>
                                            <image height="25px" url="~/Images/icons/Back.png" width="25px" />
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
</asp:Content>
