<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="PeriodView.aspx.cs" Inherits="Teachers_Amoozesh_PeriodView"
    Title="مشخصات دوره آموزشی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
                CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
                FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
            </pdc:PersianDateScriptManager>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
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
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">

                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="دوره آموزشی" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <fieldset>
                            <legend class="HelpUL">اطلاعات پایه</legend>

                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="عنوان دوره" ID="ASPxLabel10" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3" style="width: 85%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtCrsId" Width="100%" ReadOnly="True">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="طول دوره(ساعت)" Width="100%" ID="ASPxLabel6">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtPDuration" Width="100%"
                                                ReadOnly="True" ClientInstanceName="TextDur">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="امتیاز" ID="ASPxLabel13" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtPoint" Width="100%" ReadOnly="True">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="مدت زمان اعتبار(ماه)" Width="100%" ID="ASPxLabel17">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtValidDuration" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="مدت زمان عملی(ساعت)" Width="100%" ID="ASPxLabel24">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtbPracticalDuration" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" SetFocusOnError="True" ErrorTextPosition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="لطفاً این فیلد را به صورت صحیح کامل نمائید."></RequiredField>
                                                    <RegularExpression ErrorText=""></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="مدت زمان تئوری(ساعت)" Width="100%" ID="ASPxLabel30">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtbNonPracticalDuration"
                                                Width="100%" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="لطفاً این فیلد را به صورت صحیح کامل نمائید."></RequiredField>
                                                    <RegularExpression ErrorText=""></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="مدت زمان بازدید از کارگاه(ساعت)" Width="100%"
                                                ID="ASPxLabel25">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtbWorkroomDuration" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="لطفاً این فیلد را به صورت صحیح کامل نمائید."></RequiredField>
                                                    <RegularExpression ErrorText=""></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد دوره" ID="ASPxLabel18" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtPPCode" Width="100%" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="ظرفیت دوره" ID="ASPxLabel2" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtCapacity" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="ظرفیت دوره را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="هزینه دوره(بدون تخفیف)" Width="100%" ID="ASPxLabel19">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtPeriodCost" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تخفیف" ID="ASPxLabel20" Visible="False" Width="100%">
                                            </dxe:ASPxLabel>
                                            <dxe:ASPxLabel runat="server" Text="هزینه آزمون" ID="ASPxLabel8" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtDiscount" Width="100%"
                                                ReadOnly="True" Visible="False">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                            <TSPControls:CustomTextBox runat="server" ID="txtTestCost" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ شروع" ID="ASPxLabel12" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtStartDate" Style="direction: ltr"
                                                Width="100%" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ پایان" Width="100%" ID="ASPxLabel11">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtEndDate" Style="direction: ltr"
                                                Width="100%" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ آزمون" ID="ASPxLabel4" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtTestDate" Style="direction: ltr"
                                                Width="100%" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="ساعت شروع آزمون" Width="100%" ID="ASPxLabel5">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtTestHour" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="محل برگزاری آزمون" ID="ASPxLabel26" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtTestPlace" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText="محل برگزاری را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="محل برگزاری دوره" ID="ASPxLabel7" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtPlace" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText="محل برگزاری را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" Width="100%" ID="ASPxLabel15">
                                            </dxe:ASPxLabel>
                                            <br />
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="35px" ID="txtDesc" Width="100%"
                                                ReadOnly="True">
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>

                        <fieldset>
                            <legend class="HelpUL">مشخصات مؤسسه</legend>

                            <table style="vertical-align: top; text-align: right" dir="rtl" cellpadding="1" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="نام مؤسسه" ID="ASPxLabel16" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsName" Width="100%" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText=" "></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="نام مدیر مؤسسه" Width="100%" ID="ASPxLabel21">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsManager" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح تکمیل نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=" "></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره مجوز آموزشی" ID="ASPxLabel22" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Style="direction: ltr" ID="txtInsFileNo"
                                                Width="100%" MaxLength="15" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح تکمیل نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=" "></RequiredField>
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح تکمیل نمائید." ValidationExpression="\d{0,15}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره ثبت مؤسسه" Width="100%" ID="ASPxLabel23">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsRegNo" Style="direction: ltr"
                                                Width="100%" MaxLength="15" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح تکمیل نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=" "></RequiredField>
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح تکمیل نمائید." ValidationExpression="\d{0,15}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ ثبت مؤسسه" ID="ASPxLabel31" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsRegDate" Style="direction: ltr"
                                                Width="100%" MaxLength="15" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح تکمیل نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=" "></RequiredField>
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح تکمیل نمائید." ValidationExpression="\d{0,15}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="محل ثبت مؤسسه" ID="ASPxLabel32">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsRegPlace" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح تکمیل نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=" "></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شهر" ID="ASPxLabel41">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsCitName" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=" "></RequiredField>
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="0\d{10}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره تلفن همراه" Width="100%" ID="ASPxLabel35">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsMobileNo" Width="100%"
                                                MaxLength="11" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=" "></RequiredField>
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="0\d{10}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره تلفن1" ID="ASPxLabel33" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsTel1" Width="100%" MaxLength="12"
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=" "></RequiredField>
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="0\d{8,11}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره تلفن2" ID="ASPxLabel34" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsTel2" Width="100%" MaxLength="12"
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=" "></RequiredField>
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="0\d{8,11}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="آدرس پست الکترونیکی" Width="100%" ID="ASPxLabel37">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="2">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsEmail" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=" "></RequiredField>
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="آدرس وب سایت" Width="100%" ID="ASPxLabel38">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="2">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsWebSite" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=" "></RequiredField>
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="آدرس" ID="ASPxLabel36" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="25px" ID="txtInsAddress"
                                                Width="100%" ReadOnly="True">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText=" "></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel39" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="30px" ID="txtInsDesc" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText=" "></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
                        <fieldset>
                            <legend class="HelpUL">مشخصات مدرس</legend>

                            <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                                ID="CustomAspxDevGridView2" KeyFieldName="TrTeId" AutoGenerateColumns="False"
                                ClientInstanceName="grid">
                                <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
                                <Settings ShowHorizontalScrollBar="true"></Settings>
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="TeId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TeName" Caption="نام و نام خانوادگی">
                                        <EditFormSettings Visible="False"></EditFormSettings>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="LiName" Caption="مدرک تحصیلی">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="NonPracticalSalary" Caption="حق الزحمه تئوری">
                                        <PropertiesTextEdit Width="100px" DisplayFormatString="#,#">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="PracticalSalary" Caption="حق الزحمه عملی">
                                        <PropertiesTextEdit Width="100px" DisplayFormatString="#,#">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="WorkroomSalary" Caption="حق الزحمه کارگاه">
                                        <PropertiesTextEdit Width="100px" DisplayFormatString="#,#">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="NonPracticalHour" Caption="زمان تئوری">
                                        <PropertiesTextEdit Width="100px">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="PracticalHour" Caption="زمان عملی">
                                        <PropertiesTextEdit Width="100px">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="WorkroomHour" Caption="زمان کارگاه">
                                        <PropertiesTextEdit Width="100px">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="8" FieldName="Id">
                                        <EditFormSettings Visible="False"></EditFormSettings>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="Description" Caption="توضیحات">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView2>
                        </fieldset>
                        <fieldset>
                            <legend class="HelpUL">زمان برگزاری جلسات</legend>

                            <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                                EnableViewState="False" EnableCallBacks="False" ID="CustomAspxDevGridView1" KeyFieldName="SchId"
                                AutoGenerateColumns="False" OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared">
                                <Settings ShowHorizontalScrollBar="true"></Settings>
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="SchId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Number" Caption="شماره جلسه">
                                        <EditCellStyle HorizontalAlign="Right">
                                        </EditCellStyle>
                                        <PropertiesTextEdit Width="100px">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d*"></RegularExpression>
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Date" Caption="تاریخ">
                                        <EditItemTemplate>
                                            <br />
                                            <pdc:PersianDateTextBox ID="txtDate" runat="server" Width="100px" Text='<%# Bind("Date") %>'
                                                IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" ShowPickerOnTop="True"
                                                DefaultDate></pdc:PersianDateTextBox>
                                            <pdc:PersianDateValidator ID="PersianDateValidator4" runat="server" ControlToValidate="txtDate"
                                                ErrorMessage="تاریخ را صحیح وارد نمایید" Display="Dynamic" ValidateEmptyText="True"></pdc:PersianDateValidator>
                                        </EditItemTemplate>
                                        <PropertiesTextEdit>
                                            <ValidationSettings CausesValidation="True" ErrorText="hi">
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="StartTime" Caption="ساعت شروع">
                                        <EditCellStyle HorizontalAlign="Right">
                                        </EditCellStyle>
                                        <PropertiesTextEdit Width="100px">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="ساعت شروع را وارد نمایید "></RequiredField>
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="EndTime" Caption="ساعت پایان">
                                        <EditCellStyle HorizontalAlign="Right">
                                        </EditCellStyle>
                                        <PropertiesTextEdit Width="100px">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="ساعت پایان را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataMemoColumn VisibleIndex="5" FieldName="Description" Caption="توضیحات">
                                        <PropertiesMemoEdit Width="280px">
                                        </PropertiesMemoEdit>
                                        <EditCellStyle HorizontalAlign="Right">
                                        </EditCellStyle>
                                        <EditFormSettings ColumnSpan="2"></EditFormSettings>
                                    </dxwgv:GridViewDataMemoColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView2>
                        </fieldset>
                        <fieldset>
                            <legend class="HelpUL">سؤالات نظر سنجی شرکت کنندگان دوره</legend>

                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="کد سری سؤالات" Width="100%" ID="ASPxLabel27">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtAttQuCode" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right" style="width: 15%"></td>
                                        <td valign="top" align="right" style="width: 35%"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ شروع" ID="ASPxLabel28" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtAttStartDate" Style="direction: ltr"
                                                Width="100%" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ پایان" Width="100%" ID="ASPxLabel29">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtAttEndDate" Style="direction: ltr"
                                                Width="100%" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>

                        <fieldset>
                            <legend class="HelpUL">اطلاعات بازرس دوره</legend>

                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="بازرس" ID="ASPxLabel14" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtBazName" Width="100%" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="سمت" ID="ASPxLabel40" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtBazSemat" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد سری سؤالات" ID="ASPxLabel1" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtBazQuCode" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right"></td>
                                        <td dir="ltr" valign="top" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ شروع" ID="ASPxLabel3" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtBazStartDate" Style="direction: ltr"
                                                Width="100%" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ پایان" Width="100%" ID="ASPxLabel9">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtBazEndDate" Style="direction: ltr"
                                                Width="100%" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>

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
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">

                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:HiddenField ID="InstitueId" runat="server" Visible="False" />
            <asp:HiddenField ID="PeriodId" runat="server" Visible="False"></asp:HiddenField>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                        <img align="middle" src="../../Image/indicator.gif" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
