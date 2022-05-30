<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeAgentShow.aspx.cs" Inherits="Settlement_OfficeDocument_OfficeAgentShow"
    Title="مشخصات شعبه" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>

    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table>
                    <tr>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False"
                                EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                ToolTip="بازگشت" UseSubmitBehavior="False">
                                <Image Url="~/Images/icons/Back.png" />
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <div style="width: 100%; text-align: right">
        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="شرکت :">
        </dxe:ASPxLabel>
        <dxe:ASPxLabel ID="lblOfName" runat="server">
        </dxe:ASPxLabel>
    </div>
    <br />
    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table style="text-align: right" dir="rtl" width="100%">
                    <tbody>
                        <tr>
                            <td align="right" valign="top">
                                <asp:Label runat="server" Text="نام شعبه" Width="76px" ID="Label59"></asp:Label>
                            </td>
                            <td colspan="3" align="right" valign="top">
                                <TSPControls:CustomTextBox runat="server" Width="250px" ID="txtOfAgName"
                                    ReadOnly="True">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="True" ErrorText="نام نمایندگی را وارد نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <asp:Label runat="server" Text="مدیر مسئول" Width="100px" ID="Label84"></asp:Label>
                            </td>
                            <td colspan="3" align="right" valign="top">
                                <TSPControls:CustomTextBox runat="server" Width="250px" ID="txtOfAgResponsible"
                                    ReadOnly="True">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RequiredField IsRequired="True" ErrorText="نام مدیر مسئول را وارد نمایید"></RequiredField>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <asp:Label runat="server" Text="تلفن " Width="36px" ID="Label69"></asp:Label>
                            </td>
                            <td colspan="3" align="right" valign="top">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomTextBox runat="server" Width="105px" MaxLength="8" ID="txtOfAgTel"
                                                    ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="شماره تلفن را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{5,8}"></RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top">
                                                <asp:Label runat="server" Text="-" Width="13px" ID="Label71"></asp:Label>
                                            </td>
                                            <td>
                                                <TSPControls:CustomTextBox runat="server" Width="40px" MaxLength="4" ID="txtOfAgTel_pre"
                                                    ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}"></RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <asp:Label runat="server" Text="فکس" ID="Label73"></asp:Label>
                            </td>
                            <td colspan="3" align="right" valign="top">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomTextBox runat="server" Width="105px" MaxLength="8" ID="txtOfAgFax"
                                                    ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{5,8}"></RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top">
                                                <asp:Label runat="server" Text="-" Width="13px" ID="Label74"></asp:Label>
                                            </td>
                                            <td>
                                                <TSPControls:CustomTextBox runat="server" Width="40px" MaxLength="4" ID="txtOfAgFax_pre"
                                                    ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}"></RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <asp:Label runat="server" Text="آدرس وب سایت" Width="93px" ID="Label77"></asp:Label>
                            </td>
                            <td colspan="3" align="right" valign="top">
                                <TSPControls:CustomTextBox runat="server" Width="250px" ID="txtOfAgWebsite"
                                    ReadOnly="True">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RegularExpression ErrorText="آدرس وب سایت را صحیح وارد نمایید" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></RegularExpression>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <asp:Label runat="server" Text="آدرس پست الکترونیکی" Width="135px" ID="Label82"></asp:Label>
                            </td>
                            <td colspan="3" align="right" valign="top">
                                <TSPControls:CustomTextBox runat="server" Width="250px" ID="txtOfAgEmail1"
                                    ReadOnly="True">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RegularExpression ErrorText="آدرس پست الکترونیکی را صحیح وارد نمایید" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <asp:Label runat="server" Text="آدرس" ID="Label76"></asp:Label>
                            </td>
                            <td colspan="3" align="right" valign="top">
                                <TSPControls:CustomASPXMemo runat="server" Height="26px" Width="500px" ID="txtOfAgAddress"
                                    ReadOnly="True">
                                </TSPControls:CustomASPXMemo>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tr>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton2" runat="server" CausesValidation="False"
                                EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                ToolTip="بازگشت" UseSubmitBehavior="False">

                                <Image Url="~/Images/icons/Back.png" />
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:HiddenField ID="OfficeId" runat="server" Visible="False"></asp:HiddenField>
    <asp:HiddenField ID="AgentId" runat="server" Visible="False"></asp:HiddenField>
    <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
    <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False"></asp:HiddenField>

</asp:Content>
