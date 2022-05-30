<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="InActivePeriod.aspx.cs" Inherits="Employee_Amoozesh_InActivePeriod" Title="لغو دوره آموزشی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <div style="width: 100%" align="center" dir="ltr">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>



                            <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                width="100%">
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: top; text-align: right">
                                            <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server"  EnableTheming="False"
                                                                EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False" CausesValidation="true">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False" 
                                                                EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                                ToolTip="بازگشت" UseSubmitBehavior="False">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
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
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelInActivePeriod" HeaderText="مشاهده" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>

                            <table width="100%">
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="دلیل لغو">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" dir="ltr">
                                        <TSPControls:CustomAspxComboBox ID="cmbInActiveReason" runat="server" 
                                              ValueType="System.String">
                                            <Items>
                                                <dxe:ListEditItem Text="حد نصاب" Value="0" />
                                                <dxe:ListEditItem Text="مؤسسه" Value="1" />
                                                <dxe:ListEditItem Text="استاد" Value="2" />
                                                <dxe:ListEditItem Text="دیگر موارد" Value="4" />
                                            </Items>
                                            <ValidationSettings>
                                                
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td style="vertical-align: top; text-align: right"></td>
                                    <td dir="ltr"></td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="شماره نامه" Width="61px">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="width: 213px">
                                        <TSPControls:CustomTextBox IsMenuButton="true" ID="txtMailNo" runat="server" 
                                             Width="170px">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="شماره نامه را وارد نمایید" IsRequired="True" />
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td style="vertical-align: top; text-align: right">
                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="تاریخ نامه" Width="51px">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" ShowPickerOnTop="True" Width="145px" ID="txtMailDate"></pdc:PersianDateTextBox>
                                        <pdc:PersianDateValidator ID="ValidatorDate" runat="server" ClientValidationFunction="PersianDateValidator"
                                            ControlToValidate="txtMailDate" ErrorMessage="تاریخ نامه را وارد نمایید"
                                            ValidateEmptyText="True" Width="135px"></pdc:PersianDateValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="توضیحات">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td colspan="3">
                                        <TSPControls:CustomASPXMemo ID="txtDescription" runat="server" 
                                             Height="37px" Width="100%">
                                            <ValidationSettings>
                                                
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
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



                            <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                width="100%">
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: top; text-align: right">
                                            <dxhf:ASPxHiddenField ID="HiddenFieldPeriod" runat="server"></dxhf:ASPxHiddenField>
                                            <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server"  EnableTheming="False"
                                                                EnableViewState="False" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False" CausesValidation="true">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton4" runat="server" CausesValidation="False" 
                                                                EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                                ToolTip="بازگشت" UseSubmitBehavior="False">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
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

            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

