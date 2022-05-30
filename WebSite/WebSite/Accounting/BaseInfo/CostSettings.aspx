<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="CostSettings.aspx.cs" Inherits="Accounting_BaseInfo_CostSettings" Title="مدیریت تنظیم هزینه های دریافتی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
        visible="false">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " EnableTheming="False"
                                    ToolTip="ذخیره" ID="btnSave" EnableViewState="False" OnClick="btnSave_Click"
                                    UseSubmitBehavior="False">
                                    <Image Url="~/Images/icons/save.png">
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
    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

                <table align="right" width="100%">
                    <tbody>
                        <tr>
                            <td align="right" valign="top" style="width: 20%">
                                <asp:Label ID="Label4" runat="server" Text="ورودیه عضویت  برای اعضای حقیقی (ريال)"></asp:Label>
                            </td>
                            <td align="right" valign="top" style="width: 80%">
                                <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxFirstMembershipCost">
                                    <%--             <MaskSettings Mask="&lt;0..1000000000000000000000000g&gt;" />--%>
                                    <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">

                                        <RequiredField ErrorText=" " />
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <asp:Label runat="server" Text="حق عضویت سالیانه برای اعضای حقیقی (ريال)" ID="Label2"></asp:Label>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxYearlyMembershipCost">
                                    <%--                <MaskSettings Mask="&lt;0..1000000000000000000000000g&gt;"></MaskSettings>--%>
                                    <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">

                                        <RequiredField ErrorText=" "></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <asp:Label ID="Label5" runat="server" Text="ورودیه عضویت  برای اعضای حقوقی (ريال)"></asp:Label>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxFirstMembershipCostOffice">
                                 <%--   <MaskSettings Mask="&lt;0..1000000000000000000000000g&gt;" />--%>
                                    <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">

                                        <RequiredField ErrorText=" " />
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <asp:Label ID="Label1" runat="server" Text="حق عضویت سالیانه برای اعضای حقوقی (ريال)"></asp:Label>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxYearlyMembershipCostOffice">
                                    <%--<MaskSettings Mask="&lt;0..1000000000000000000000000g&gt;" />--%>
                                    <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">

                                        <RequiredField ErrorText=" " />
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <asp:Label ID="Label6" runat="server" Text="حق عضویت سالیانه برای اعضای حقوقی(مجریان) (ريال)"></asp:Label>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomTextBox runat="server" ID="TextBoxYearlyMembershipCostOfficeImp">
                                    <%--<MaskSettings Mask="&lt;0..1000000000000000000000000g&gt;" />--%>
                                    <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">

                                        <RequiredField ErrorText=" " />
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <dxe:ASPxLabel runat="server" Text="هزینه تمدید و تغییر پروانه  (ريال)"
                                    ID="ASPxLabel10">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomTextBox runat="server" TabIndex="6" ID="ASPxTextBoxMemberFileModifiedCost">
                                    <%--<MaskSettings Mask="&lt;0..1000000000000000000000000g&gt;"></MaskSettings>--%>
                                    <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">

                                        <RequiredField ErrorText=""></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <asp:Label runat="server" Text="هزینه صدور پروانه (ريال)" ID="Label3"></asp:Label>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxMemberFileRegistrationCost">
                                    <%--<MaskSettings Mask="&lt;0..1000000000000000000000000g&gt;"></MaskSettings>--%>
                                    <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">

                                        <RequiredField ErrorText=" "></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <asp:Label runat="server" Text="هزینه صدور/تمدید مجوز اجرا (ريال)" ID="Label7"></asp:Label>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomTextBox runat="server" ID="TextBoxImplentDoc" ViewStateMode="Enabled">
                                    <%--<MaskSettings Mask="&lt;0..1000000000000000000000000g&gt;"></MaskSettings>--%>
                                    <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">

                                        <RequiredField ErrorText=" "></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <dxe:ASPxLabel runat="server" Text="علی الحساب ماده 27 (ريال)" ID="ASPxLabel5">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxExpertPlace27sOnAccount">
                                    <%--<MaskSettings Mask="&lt;0..1000000000000000000000000g&gt;"></MaskSettings>--%>
                                    <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">

                                        <RequiredField ErrorText=" "></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " EnableTheming="False"
                                    ToolTip="ذخیره" ID="btnSave2" EnableViewState="False" OnClick="btnSave_Click"
                                    UseSubmitBehavior="False">
                                    <Image Url="~/Images/icons/save.png">
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
    <%--        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                <img id="IMG1" src="../../Image/indicator.gif" align="middle" />
                لطفا صبر نمایید ...
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>--%>
</asp:Content>
