<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="SMSCostInsert.aspx.cs" Inherits="Employee_SMS_SMSCostInsert" Title="مشخصات هزینه پیام کوتاه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="jscript">
        function SetEmpty() {
            txtCostEn.SetText("");
            txtbCostFr.SetText("");
            txtbMailNo.SetText("");
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td align="right" dir="ltr" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave" EnableViewState="False" EnableTheming="False" OnClick="btnSave_Click"
                                            UseSubmitBehavior="False">
                                            
                                            <image url="~/Images/icons/save.png">
                                                        </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" dir="ltr" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" AutoPostBack="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click" UseSubmitBehavior="False">
                                           
                                            <image url="~/Images/icons/Back.png">
                                                        </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelSMSCost" HeaderText="مشاهده" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td valign="top" align="right" width="20%">
                                        <dxe:ASPxLabel runat="server" Text="هزینه فارسی(ریال)" ID="ASPxLabel3">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="30%">
                                        <TSPControls:CustomTextBox runat="server" EnableClientSideAPI="True" Width="100%" ID="txtbCostFr"
                                            ClientInstanceName="txtbCostFr">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText="هزینه پیام فارسی را وارد نمایید" IsRequired="True" />
                                                <RegularExpression ErrorText="هزینه به صورت عدد صحیح و یا عدد اعشاری باشد(به عنوان مثال 12.5)"
                                                    ValidationExpression="\d*([.]\d*)?" />

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right" width="20%">
                                        <dxe:ASPxLabel runat="server" Text="هزینه انگلیسی(ریال)" ID="ASPxLabel4">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="30%">
                                        <TSPControls:CustomTextBox runat="server" EnableClientSideAPI="True" Width="100%" ID="txtCostEn"
                                            ClientInstanceName="txtCostEn">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText="هزینه پیام انگلسی را وارد نمایید" IsRequired="True" />
                                                <RegularExpression ErrorText="هزینه به صورت عدد صحیح و یا عدد اعشاری باشد(به عنوان مثال 12.5)"
                                                    ValidationExpression="\d*([.]\d*)?" />

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right" width="20%">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ" ID="ASPxLabel5">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="30%">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="230px" ShowPickerOnTop="True"
                                            ID="txtbDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="تاریخ را وارد نمایید" ToolTip="تاریخ را وارد نمایید"
                                            ControlToValidate="txtbDate" Width="128px" ID="RequiredFieldValidatorDate" Display="Dynamic">تاریخ را وارد نمایید</asp:RequiredFieldValidator>
                                    </td>
                                    <td valign="top" align="right" width="20%">
                                        <dxe:ASPxLabel runat="server" Text="نوع وب سرویس" ID="ASPxLabel1">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="30%">
                                        <TSPControls:CustomAspxComboBox ID="cmbWebService" runat="server"
                                            Width="100%" RightToLeft="True">
                                            <Items>
                                                <dxe:ListEditItem Text="عصر فرا ارتباط" Value="0" />
                                                <dxe:ListEditItem Text="مگفا" Value="1" />
                                                <dxe:ListEditItem Text="پویا رایانه" Value="2" />
                                            </Items>
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="true" ErrorText="نوع وب سرویس را انتخاب نمایید" />
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
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
                            <tbody>
                                <tr>
                                    <td align="right" dir="ltr" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave2" EnableViewState="False" EnableTheming="False" UseSubmitBehavior="False"
                                            OnClick="btnSave_Click">
                                           
                                            <image url="~/Images/icons/save.png">
                                                        </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" dir="ltr" style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack2" AutoPostBack="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click" UseSubmitBehavior="False">
                                            
                                            <image url="~/Images/icons/Back.png">
                                                        </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table></dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                        <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldSMSCost">
                        </dxhf:ASPxHiddenField>                  
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                DisplayAfter="0" BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                                    <img alt="" src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
