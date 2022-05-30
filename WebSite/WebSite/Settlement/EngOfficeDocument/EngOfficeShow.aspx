<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="EngOfficeShow.aspx.cs" Inherits="Settlement_EngOfficeDocument_EngOfficeShow"
    Title="مشخصات دفتر" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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

    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>

    <div align="right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table align="right">
                    <tbody>
                        <tr>
                            <td style="width: 27px; height: 27px;">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" EnableTheming="False"
                                    ToolTip="ذخیره" ID="btnSave" EnableViewState="False" OnClick="btnSave_Click"
                                    Text=" " UseSubmitBehavior="False">
                                    <Image Url="~/Images/icons/save.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                            </td>
                            <td style="width: 27px; height: 27px;">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" EnableTheming="False"
                                    ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click"
                                    CausesValidation="False" Text=" " UseSubmitBehavior="False">
                                    <Image Url="~/Images/icons/Back.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server" OnItemClick="ASPxMenu1_ItemClick">
        <Items>
            <dxm:MenuItem Name="Member" Text="مشخصات دفتر" Selected="true">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Member" Text="اعضای دفتر">
            </dxm:MenuItem>
            <%-- <dxm:MenuItem Name="Job" Text="سوابق کاری">
                                </dxm:MenuItem>--%>
            <dxm:MenuItem Name="Attach" Text="مستندات">
            </dxm:MenuItem>
        </Items>
    </TSPControls:CustomAspxMenuHorizontal>

    <br />

    <TSPControls:CustomASPxRoundPanel ID="RoundPanelMemberFile" HeaderText="مشاهده" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                <fieldset>
                    <legend class="HelpUL">مشخصات دفتر</legend>

                    <table dir="rtl" width="100%">
                        <tr>
                            <td align="right" valign="top" width="20%">
                                <dxe:ASPxLabel runat="server" Text="نام دفتر" ID="ASPxLabel5" Width="100%">
                                </dxe:ASPxLabel>
                            </td>
                            <td colspan="3" align="right" valign="top">
                                <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtEngOffName">
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="20%">
                                <dxe:ASPxLabel runat="server" Text="شماره مشارکت نامه" ID="ASPxLabel100" Width="100%">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" valign="top" width="30%">
                                <TSPControls:CustomTextBox runat="server" Width="100%" Style="direction: ltr" ID="txtLetterNo">
                                </TSPControls:CustomTextBox>
                            </td>
                            <td align="right" width="20%" valign="top">
                                <dxe:ASPxLabel runat="server" Text="تاریخ مشارکت نامه" ID="ASPxLabel4" Width="100%">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" width="30%" valign="top">
                                <pdc:PersianDateTextBox runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                    ShowPickerOnEvent="OnClick" PickerDirection="ToRight" ShowPickerOnTop="True"
                                    Width="200px" ID="txtLetterDate"></pdc:PersianDateTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <dxe:ASPxLabel runat="server" Text="نوع دفتر *" ID="ASPxLabel2" Width="100%">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="OdbEOffType"
                                    TextField="Name" ValueField="EOfTId"
                                    ID="ComboEOfTId" RightToLeft="True"
                                    Width="100%">
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>

                                </TSPControls:CustomAspxComboBox>
                            </td>
                            <td align="right" valign="top">
                                <dxe:ASPxLabel runat="server" Text="شماره دفتر اسناد رسمی" ID="ASPxLabel7" Width="100%">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtDaftarNo"
                                    MaxLength="20">
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <dxe:ASPxLabel runat="server" Text="تلفن *" ID="ASPxLabel10" Width="100%">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtTel">
                                </TSPControls:CustomTextBox>
                            </td>
                            <td align="right" valign="top">
                                <dxe:ASPxLabel runat="server" Text="فکس" ID="ASPxLabel1" Width="100%">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtFax">
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right">
                                <asp:Label runat="server" Text="شماره همراه *" ID="Label30" Width="100%"></asp:Label>
                            </td>
                            <td valign="top" align="right">
                                <TSPControls:CustomTextBox runat="server" ID="txtMobileNo" Width="100%"
                                    MaxLength="11">
                                </TSPControls:CustomTextBox>
                            </td>
                            <td valign="top" align="right">
                                <asp:Label runat="server" ID="Label301" Text="آدرس پست الکترونیکی" Width="100%"></asp:Label>
                            </td>
                            <td valign="top" align="right">
                                <TSPControls:CustomTextBox runat="server" ID="txtEmail" Width="100%">
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <dxe:ASPxLabel runat="server" Text="محل دفتر اسناد رسمی *" ID="ASPxLabel9" Width="100%">
                                </dxe:ASPxLabel>
                            </td>
                            <td colspan="3" align="right" valign="top">
                                <TSPControls:CustomASPXMemo ID="txtDaftarLoc" runat="server"
                                    Height="40px" Width="100%">
                                </TSPControls:CustomASPXMemo>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <dxe:ASPxLabel runat="server" Text="آدرس *" ID="ASPxLabel3" Width="100%">
                                </dxe:ASPxLabel>
                            </td>
                            <td colspan="3" align="right" valign="top">
                                <TSPControls:CustomASPXMemo ID="txtAddress" runat="server"
                                    Height="40px" Width="100%">
                                </TSPControls:CustomASPXMemo>
                            </td>
                        </tr>
                        <tr id="Tr1" runat="server">
                            <td align="right" valign="top">
                                <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel6" Width="100%">
                                </dxe:ASPxLabel>
                            </td>
                            <td colspan="3" align="right" valign="top">
                                <TSPControls:CustomASPXMemo ID="txtDesc" runat="server"
                                    Height="40px" Width="100%">
                                </TSPControls:CustomASPXMemo>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset>
                    <legend class="HelpUL">مشخصات مجوز</legend>

                    <TSPControls:CustomAspxCallbackPanel runat="server" 
                        ClientInstanceName="CallbackPanelDoRegDate" Width="100%" ID="CallbackPanelDoRegDate"
                        OnCallback="CallbackPanelDoRegDate_Callback">
                        <PanelCollection>
                            <dxp:PanelContent ID="PanelContent11" runat="server">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td align="right" valign="top" style="width: 15%">
                                                <dxe:ASPxLabel ID="lblFileNo" runat="server" Text="شماره پروانه">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" dir="rtl" style="width: 35%">
                                                <TSPControls:CustomTextBox ID="txtFileNo" runat="server"
                                                    Enabled="False" Width="100%" Style="direction: ltr" RightToLeft="True">
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td align="right" valign="top" style="width: 15%"></td>
                                            <td align="right" valign="top" style="width: 35%"></td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: top">
                                                <dxe:ASPxLabel ID="ASPxLabeld1" runat="server" Text="موقت/دائم" Width="100%">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td id="Td2" runat="server" align="right" style="vertical-align: top">
                                                <TSPControls:CustomAspxComboBox ID="cmbdIsTemporary" ClientInstanceName="cmbIsTemporary" runat="server"
                                                    ValueType="System.String" Width="100%" RightToLeft="True">
                                                    <ClientSideEvents SelectedIndexChanged="function(s,e){CallbackPanelDoRegDate.PerformCallback(cmbIsTemporary.GetValue());}" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <Items>
                                                        <dxe:ListEditItem Text="پروانه دائم" Value="0" />
                                                        <dxe:ListEditItem Text="پروانه موقت" Value="1" />
                                                    </Items>
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td id="Td3" runat="server" align="right" dir="rtl" valign="top">
                                                <dxe:ASPxLabel ID="ASPxLabeld2" runat="server" Text="شماره سریال" Width="100%">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td id="Td4" runat="server" align="right" dir="rtl" style="vertical-align: top">
                                                <TSPControls:CustomTextBox ID="txtdSerialNo" runat="server"
                                                    Width="100%">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="true" ErrorText="شماره سریال را وارد نمایید" />
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr id="Tr2" runat="server">
                                            <td id="Td5" runat="server" align="right" valign="top">
                                                <dxe:ASPxLabel ID="ASPxLabeld3" runat="server" ClientInstanceName="lblDate" Text="تاریخ آخرین تمدید"
                                                    Width="100%">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td id="Td6" runat="server" align="right" dir="rtl" valign="top">
                                                <pdc:PersianDateTextBox ID="txtLastRegDate" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                    PickerDirection="ToRight" ShowPickerOnTop="True" ShowPickerOnEvent="OnClick"
                                                    Width="230px"></pdc:PersianDateTextBox>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastRegDate" ErrorMessage="تاریخ آخرین تمدید"
                                                    Display="Dynamic" ToolTip="تاریخ آخرین تمدید را وارد نمایید" ID="RequiredFieldValidator1">تاریخ آخرین تمدید را وارد نمایید</asp:RequiredFieldValidator>
                                            </td>
                                            <td id="Td7" runat="server" align="right" dir="rtl" valign="top">
                                                <dxe:ASPxLabel ID="ASPxLabeld4" runat="server" ClientInstanceName="lblDate" Text="تاریخ پایان اعتبار"
                                                    Width="100%">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td id="Td8" runat="server" align="right" dir="rtl" valign="top">
                                                <pdc:PersianDateTextBox ID="txtExpDate" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                    PickerDirection="ToRight" ShowPickerOnTop="True" Width="230px"></pdc:PersianDateTextBox>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastRegDate" ErrorMessage="تاریخ پایان اعتبار"
                                                    Display="Dynamic" ToolTip="تاریخ پایان اعتبار را وارد نمایید" ID="RequiredFieldValidator2">تاریخ پایان اعتبار را وارد نمایید</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomAspxCallbackPanel>
                </fieldset>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>

    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                <table align="right">
                    <tbody>
                        <tr>
                            <td style="width: 27px; height: 27px;">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" EnableTheming="False"
                                    ToolTip="ذخیره" ID="btnSave2" EnableViewState="False" OnClick="btnSave_Click"
                                    Text=" " UseSubmitBehavior="False">
                                    <Image Url="~/Images/icons/save.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                            </td>
                            <td style="width: 27px; height: 27px;">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" EnableTheming="False"
                                    ToolTip="بازگشت" ID="ASPxButton6" EnableViewState="False" OnClick="btnBack_Click"
                                    CausesValidation="False" Text=" " UseSubmitBehavior="False">
                                    <Image Url="~/Images/icons/Back.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:HiddenField ID="EngOfficeId" runat="server" Visible="False"></asp:HiddenField>
    <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
    <asp:HiddenField ID="EngFileId" runat="server" Visible="False" />
    <asp:ObjectDataSource ID="OdbEOffType" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.EngOfficeTypeManager"></asp:ObjectDataSource>
</asp:Content>
