<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="PeriodsView.aspx.cs" Inherits="Employee_Amoozesh_PeriodsView"
    Title="مشخصات دوره" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
    <div id="Content" runat="server" style="width: 100%" dir="ltr">
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
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
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                                ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnSave2_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/save.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="نظر سنجی"
                                                ID="btnOpinion" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnOpinion_Click" CausesValidation="False" Visible="False">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/User comment.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="لغو دوره"
                                                ID="btnInActive" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnInActive_Click" CausesValidation="False">
                                                <ClientSideEvents Click="function(s, e) {
		 e.processOnServer= confirm('آیا مطمئن به لغو این دوره هستید؟');
}" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint" Visible="false" ClientInstanceName="btnEnvelopePrint" runat="server"
                                                Text=" " UseSubmitBehavior="False" ToolTip="چاپ" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="False">
                                                <Image Height="25px" Url="~/Images/icons/printers.png" Width="25px">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
                                                                        CallbackPanelPeriod.PerformCallback('Print')
                                                                     }" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                      
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBack_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
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

                            <fieldset runat="server" id="ASPxRoundPanel2">
                                <legend class="fieldset-legend" dir="rtl"><b>اطلاعات پایه</b>
                                </legend>

                                <table dir="rtl" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top; width: 137px; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="عنوان دوره" ID="ASPxLabel10"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top; text-align: right" colspan="3">
                                                <TSPControls:CustomTextBox runat="server" Width="548px" ReadOnly="True" ID="txtCrsId">
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 137px; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="طول دوره(ساعت)" Width="101px" ID="ASPxLabel6"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomTextBox runat="server" Width="145px" ReadOnly="True" ID="txtPDuration" ClientInstanceName="TextDur">
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="امتیاز" ID="ASPxLabel13"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomTextBox runat="server" Width="145px" ReadOnly="True" ID="txtPoint">
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 137px; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="مدت زمان اعتبار(ماه)" Width="110px" ID="ASPxLabel17"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomTextBox runat="server" Width="145px" ReadOnly="True" ID="txtValidDuration">
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="مدت زمان عملی(ساعت)" Width="134px" ID="ASPxLabel24"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomTextBox runat="server" Width="145px" ReadOnly="True" ID="txtbPracticalDuration">
                                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ErrorTextPosition="Bottom">
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

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
                                            <td style="vertical-align: top; width: 137px; height: 37px; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="مدت زمان تئوری(ساعت)" Width="100px" ID="ASPxLabel30"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top; height: 37px; text-align: right">
                                                <TSPControls:CustomTextBox runat="server" Width="145px" ReadOnly="True" ID="txtbNonPracticalDuration">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="لطفاً این فیلد را به صورت صحیح کامل نمائید."></RequiredField>

                                                        <RegularExpression ErrorText=""></RegularExpression>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                            <td style="vertical-align: top; height: 37px; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="مدت زمان بازدید از کارگاه(ساعت)" Width="100px" ID="ASPxLabel25"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top; height: 37px; text-align: right">
                                                <TSPControls:CustomTextBox runat="server" Width="145px" ReadOnly="True" ID="txtbWorkroomDuration">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

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
                                            <td style="vertical-align: top; width: 137px; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="کد دوره" ID="ASPxLabel18"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomTextBox runat="server" Width="145px" ReadOnly="True" ID="txtPPCode">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="ظرفیت دوره" ID="ASPxLabel2"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomTextBox runat="server" Width="145px" ReadOnly="True" ID="txtCapacity">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="ظرفیت دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 137px; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="هزینه دوره(بدون تخفیف)" Width="124px" ID="ASPxLabel19"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomTextBox runat="server" Width="145px" ReadOnly="True" ID="txtPeriodCost">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="تخفیف" ID="ASPxLabel20" Visible="False"></dxe:ASPxLabel>

                                                <dxe:ASPxLabel runat="server" Text="هزینه آزمون" ID="ASPxLabel8"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomTextBox runat="server" Width="145px" ReadOnly="True" ID="txtTestCost">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                                <TSPControls:CustomTextBox runat="server" Width="145px" ReadOnly="True" ID="txtDiscount" Visible="False">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 137px; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ شروع" ID="ASPxLabel12"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomTextBox runat="server" Style="direction: ltr" Width="145px" ReadOnly="True" ID="txtStartDate">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ پایان" Width="56px" ID="ASPxLabel11"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomTextBox runat="server" Style="direction: ltr" Width="145px" ReadOnly="True" ID="txtEndDate">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 137px; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ آزمون" ID="ASPxLabel4"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomTextBox runat="server" Style="direction: ltr" Width="145px" ReadOnly="True" ID="txtTestDate">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="ساعت شروع آزمون" Width="101px" ID="ASPxLabel5"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomTextBox runat="server" Width="145px" ReadOnly="True" ID="txtTestHour">
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 137px; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="محل برگزاری آزمون" ID="ASPxLabel26"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top; text-align: right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="26px" Width="548px" ReadOnly="True" ID="txtTestPlace">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText="محل برگزاری را وارد نمایید"></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPXMemo>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 137px; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="محل برگزاری دوره" ID="ASPxLabel7"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top; text-align: right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="26px" Width="548px" ReadOnly="True" ID="txtPlace">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText="محل برگزاری را وارد نمایید"></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPXMemo>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 137px; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="توضیحات" Width="100px" ID="ASPxLabel15"></dxe:ASPxLabel>

                                                <br />
                                            </td>
                                            <td style="vertical-align: top; text-align: right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="71px" Width="548px" ReadOnly="True" ID="txtDesc"></TSPControls:CustomASPXMemo>

                                                <br />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>
                            <br />
                            <fieldset runat="server" id="ASPxRoundPanelPPRegister">
                                <legend class="fieldset-legend" dir="rtl"><b>اطلاعات  ثبت نام</b>
                                </legend>
                                <table dir="rtl" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نوع ثبت نام" ID="ASPxLabel16"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtsRgstType" Width="145px" ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                            <td style="vertical-align: top" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ ثبت نام" ID="ASPxLabel21"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtsRegisterDate" Width="145px" ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نوع پرداخت" ID="ASPxLabel22"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtsPayType" Width="145px" ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                            <td style="vertical-align: top" valign="top" align="right"></td>
                                            <td style="vertical-align: top" valign="top" align="right"></td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نمره اولیه" ID="ASPxLabel23"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtsFirstMark" Width="145px" ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                            <td style="vertical-align: top" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نمره نهایی" ID="ASPxLabel31"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtsLastMark" Width="145px" ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ اعتراض" ID="ASPxLabel32"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtsMeObjectionDate" Width="145px" ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                            <td style="vertical-align: top" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ پاسخ اعتراض" Width="125px" ID="ASPxLabel33"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtsTeObjectionDate" Width="145px" ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="متن اعتراض" ID="ASPxLabel35"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" valign="top" align="right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtsMeObjectionText" Width="510px" ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText="محل برگزاری را وارد نمایید"></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPXMemo>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="متن پاسخ اعتراض" Width="128px" ID="ASPxLabel36"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" valign="top" align="right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtsTeObjectionText" Width="510px" ReadOnly="True">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText="محل برگزاری را وارد نمایید"></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPXMemo>

                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>
                            <br />
                            <fieldset runat="server" id="ASPxRoundPanel4">
                                <legend class="fieldset-legend" dir="rtl"><b>مشخصات مدرس</b>
                                </legend>

                                <TSPControls:CustomAspxDevGridView runat="server" Width="100%" ID="CustomAspxDevGridView2" KeyFieldName="TrTeId" AutoGenerateColumns="False" ClientInstanceName="grid">
                                    <SettingsEditing Mode="PopupEditForm"></SettingsEditing>

                                    <ClientSideEvents RowClick="function(s, e) {
	SetControlValues();
	btn.SetEnabled(false);
}"></ClientSideEvents>

                                    <SettingsText CommandClearFilter="پاک کردن فیلتر" ConfirmDelete="آیا مطمئن به حذف این ردیف هستید؟" EmptyDataRow="هیچ داده ای وجود ندارد" GroupPanel="جهت گروه بندی ستون مربوطه را به این قسمت بکشید" CommandEdit="ویرایش" PopupEditFormCaption="تغییر رکورد" CommandDelete="حذف" CommandSelect="انتخاب" CommandNew="جدید" CommandUpdate="ذخیره" CommandCancel="انصراف"></SettingsText>

                                    <Styles>
                                        <GroupPanel ForeColor="Black"></GroupPanel>

                                        <Header HorizontalAlign="Center"></Header>
                                    </Styles>

                                    <Settings ShowGroupPanel="True"></Settings>

                                    <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>

                                    <SettingsPager>
                                        <AllButton Text="همه رکوردها"></AllButton>

                                        <FirstPageButton Text="اولین صفحه"></FirstPageButton>

                                        <LastPageButton Text="آخرین صفحه"></LastPageButton>

                                        <Summary Text="صفحه: {0} از {1} (تعداد کل:{2})"></Summary>

                                        <NextPageButton Text="صفحه بعد"></NextPageButton>

                                        <PrevPageButton Text="صفحه قبل"></PrevPageButton>
                                    </SettingsPager>
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="TeId"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TeName" Caption="نام و نام خانوادگی">
                                            <EditFormSettings Visible="False"></EditFormSettings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="LiName" Caption="مدرک تحصیلی"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="NonPracticalSalary" Caption="حق الزحمه تئوری">
                                            <PropertiesTextEdit DisplayFormatString="#,#" Width="100px"></PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="PracticalSalary" Caption="حق الزحمه عملی">
                                            <PropertiesTextEdit DisplayFormatString="#,#" Width="100px"></PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="WorkroomSalary" Caption="حق الزحمه کارگاه">
                                            <PropertiesTextEdit DisplayFormatString="#,#" Width="100px"></PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="NonPracticalHour" Caption="زمان تئوری">
                                            <PropertiesTextEdit Width="100px"></PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="PracticalHour" Caption="زمان عملی">
                                            <PropertiesTextEdit Width="100px"></PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="WorkroomHour" Caption="زمان کارگاه">
                                            <PropertiesTextEdit Width="100px"></PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="8" FieldName="Id">
                                            <EditFormSettings Visible="False"></EditFormSettings>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="Description" Caption="توضیحات"></dxwgv:GridViewDataTextColumn>
                                    </Columns>

                                    <SettingsLoadingPanel Text="در حال بارگذاری"></SettingsLoadingPanel>
                                </TSPControls:CustomAspxDevGridView>


                            </fieldset>
                            <br />
                            <fieldset runat="server" id="ASPxRoundPanel5">
                                <legend class="fieldset-legend" dir="rtl"><b>زمان برگزاری جلسات</b>
                                </legend>

                                <div dir="rtl">
                                    <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False" EnableCallBacks="False" ID="CustomAspxDevGridView1" KeyFieldName="SchId" AutoGenerateColumns="False"
                                        Width="100%" OnHtmlDataCellPrepared="CustomAspxDevGridView1_OnHtmlDataCellPrepared">
                                        <SettingsEditing Mode="PopupEditForm"></SettingsEditing>

                                        <SettingsText CommandClearFilter="پاک کردن فیلتر" ConfirmDelete="آیا مطمئن به حذف این ردیف هستید؟" EmptyDataRow="هیچ داده ای وجود ندارد" GroupPanel="جهت گروه بندی ستون مربوطه را به این قسمت بکشید" CommandEdit="ویرایش" PopupEditFormCaption="تغییر رکورد" CommandDelete="حذف" CommandSelect="انتخاب" CommandNew="جدید" CommandUpdate="ذخیره" CommandCancel="انصراف"></SettingsText>

                                        <Styles>
                                            <GroupPanel ForeColor="Black"></GroupPanel>

                                            <Header HorizontalAlign="Center"></Header>
                                        </Styles>

                                        <Settings ShowGroupPanel="True"></Settings>

                                        <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>

                                        <SettingsPager>
                                            <AllButton Text="همه رکوردها"></AllButton>

                                            <FirstPageButton Text="اولین صفحه"></FirstPageButton>

                                            <LastPageButton Text="آخرین صفحه"></LastPageButton>

                                            <Summary Text="صفحه: {0} از {1} (تعداد کل:{2})"></Summary>

                                            <NextPageButton Text="صفحه بعد"></NextPageButton>

                                            <PrevPageButton Text="صفحه قبل"></PrevPageButton>
                                        </SettingsPager>
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="SchId"></dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Number" Caption="شماره جلسه">
                                                <EditCellStyle HorizontalAlign="Right"></EditCellStyle>

                                                <PropertiesTextEdit Width="100px">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d*"></RegularExpression>
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Date" Caption="تاریخ">
                                                <EditItemTemplate>
                                                    <br />
                                                    <pdc:PersianDateTextBox ID="txtDate" runat="server" Width="100px" Text='<%# Bind("Date") %>' IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" ShowPickerOnTop="True" DefaultDate></pdc:PersianDateTextBox>
                                                    <pdc:PersianDateValidator ID="PersianDateValidator4" runat="server" ControlToValidate="txtDate" ErrorMessage="تاریخ را صحیح وارد نمایید" Display="Dynamic" ValidateEmptyText="True"></pdc:PersianDateValidator>
                                                </EditItemTemplate>

                                                <PropertiesTextEdit>
                                                    <ValidationSettings CausesValidation="True" ErrorText="hi">
                                                        <RequiredField IsRequired="True"></RequiredField>
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="StartTime" Caption="ساعت شروع">
                                                <EditCellStyle HorizontalAlign="Right"></EditCellStyle>

                                                <PropertiesTextEdit Width="100px">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="ساعت شروع را وارد نمایید "></RequiredField>
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="EndTime" Caption="ساعت پایان">
                                                <EditCellStyle HorizontalAlign="Right"></EditCellStyle>

                                                <PropertiesTextEdit Width="100px">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="ساعت پایان را وارد نمایید"></RequiredField>
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataMemoColumn VisibleIndex="4" FieldName="Description" Caption="توضیحات">
                                                <PropertiesMemoEdit Width="280px"></PropertiesMemoEdit>

                                                <EditCellStyle HorizontalAlign="Right"></EditCellStyle>

                                                <EditFormSettings ColumnSpan="2"></EditFormSettings>
                                            </dxwgv:GridViewDataMemoColumn>
                                        </Columns>

                                        <SettingsLoadingPanel Text="در حال بارگذاری"></SettingsLoadingPanel>
                                    </TSPControls:CustomAspxDevGridView>

                                </div>
                            </fieldset>
                            <br />
                            <fieldset runat="server" id="ASPxRoundPanel9">
                                <legend class="fieldset-legend" dir="rtl"><b>امتیاز بندی برای رشته های مختلف</b>
                                </legend>

                                <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False" Width="411px" ID="CustomAspxDevGridViewGrade" DataSourceID="OdbGrades" KeyFieldName="TrGrId" AutoGenerateColumns="False">
                                    <SettingsEditing EditFormColumnCount="1" PopupEditFormModal="True" Mode="PopupEditForm"></SettingsEditing>

                                    <ClientSideEvents RowDblClick="function(s, e) {
	Gradepop.Hide();

	SetControlValuesGrade();
}"></ClientSideEvents>

                                    <SettingsText CommandClearFilter="پاک کردن فیلتر" ConfirmDelete="آیا مطمئن به حذف این ردیف هستید؟" EmptyDataRow="هیچ داده ای وجود ندارد" GroupPanel="جهت گروه بندی ستون مربوطه را به این قسمت بکشید" CommandEdit="ویرایش" PopupEditFormCaption="تغییر رکورد" CommandDelete="حذف" CommandSelect="انتخاب" CommandNew="جدید" CommandUpdate="ذخیره" CommandCancel="انصراف"></SettingsText>

                                    <Styles>
                                        <GroupPanel ForeColor="Black"></GroupPanel>

                                        <Header HorizontalAlign="Center"></Header>
                                    </Styles>

                                    <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>

                                    <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>

                                    <SettingsPager>
                                        <AllButton Text="همه رکوردها"></AllButton>

                                        <FirstPageButton Text="اولین صفحه"></FirstPageButton>

                                        <LastPageButton Text="آخرین صفحه"></LastPageButton>

                                        <Summary Text="صفحه: {0} از {1} (تعداد کل:{2})"></Summary>

                                        <NextPageButton Text="صفحه بعد"></NextPageButton>

                                        <PrevPageButton Text="صفحه قبل"></PrevPageButton>
                                    </SettingsPager>
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GrdName" Caption="پایه"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="ResName" Caption="صلاحیت"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjName" Caption="رشته"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Description" Caption="توضیحات"></dxwgv:GridViewDataTextColumn>
                                    </Columns>

                                    <SettingsLoadingPanel Text="در حال بارگذاری"></SettingsLoadingPanel>
                                </TSPControls:CustomAspxDevGridView>

                            </fieldset>
                            <br />
                            <fieldset runat="server" id="ASPxRoundPanel7">
                                <legend class="fieldset-legend" dir="rtl"><b>سؤالات نظر سنجی شرکت کنندگان دوره</b>
                                </legend>
                                <table dir="rtl" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="کد سری سؤالات" ID="ASPxLabel27"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" dir="ltr" align="right">
                                                <TSPControls:CustomAspxComboBox runat="server" Width="150px" TextField="QuCode" ID="CmbQucode" DataSourceID="ObjectDataSource1" ValueType="System.String" ValueField="QuCode">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField IsRequired="True" ErrorText="کد سؤال را انتخاب نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>

                                                    <ButtonStyle Width="13px"></ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>

                                            </td>
                                            <td style="vertical-align: top" align="right"></td>
                                            <td style="vertical-align: top" align="right"></td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ شروع" ID="ASPxLabel28"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="160px" ShowPickerOnTop="True" ID="txtOpStartDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr;"></pdc:PersianDateTextBox>

                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOpStartDate" ID="RequiredFieldValidator1" Display="Dynamic">تاریخ شروع را وارد نمایید</asp:RequiredFieldValidator>

                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ پایان" ID="ASPxLabel29"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="160px" ShowPickerOnTop="True" ID="txtOpExpDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr;"></pdc:PersianDateTextBox>

                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOpExpDate" ID="RequiredFieldValidator2" Display="Dynamic">تاریخ پایان را وارد نمایید</asp:RequiredFieldValidator>

                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>
                            <br />
                            <fieldset runat="server" id="ASPxRoundPanel8">
                                <legend class="fieldset-legend" dir="rtl"><b>اطلاعات بازرس دوره</b>
                                </legend>

                                <table dir="rtl" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top" align="right"></td>
                                            <td style="vertical-align: top" dir="ltr" align="right">
                                                <TSPControls:CustomAspxComboBox runat="server" ClientVisible="False" Width="150px" TextField="NmcName" ID="cmbMemberChart" DataSourceID="OdbNezamMember" EnableClientSideAPI="True" ValueType="System.String" ValueField="UltId" ClientInstanceName="cmb">
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
//alert(cmb.GetSelectedItem().GetColumnText(&quot;UltId&quot;))	;
ult.Set(&quot;UltId&quot;,cmb.GetSelectedItem().GetColumnText(&quot;EmpId&quot;));
}"></ClientSideEvents>

                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField IsRequired="True" ErrorText="بازرس را انتخاب نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <Columns>
                                                        <dxe:ListBoxColumn FieldName="UltName" Caption="نوع"></dxe:ListBoxColumn>
                                                        <dxe:ListBoxColumn FieldName="NmcName" Caption="نام و نام خانوادگی" Name="NmcName"></dxe:ListBoxColumn>
                                                        <dxe:ListBoxColumn FieldName="EmpId" Caption="کد" Name="EmpId"></dxe:ListBoxColumn>
                                                    </Columns>

                                                    <ButtonStyle Width="13px"></ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>

                                                <TSPControls:CustomTextBox runat="server" ID="txtBzName" Width="150px" Enabled="False" Visible="False">
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                            <td style="vertical-align: top" align="right"></td>
                                            <td style="vertical-align: top" dir="ltr" align="right"></td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="بازرس" ID="ASPxLabel14"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" dir="ltr" align="right">
                                                <TSPControls:CustomAspxComboBox runat="server" Width="150px" ID="CmbBzType" ValueType="System.String" SelectedIndex="0" ClientInstanceName="CmbBz">
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	TextBzCode.SetText('');
	TextBzFirstName.SetText('');
	TextBzLastName.SetText('');

	if(CmbBz.GetValue()==&quot;1&quot;)
	{
		lblMe.SetVisible(true);
		lblEmp.SetVisible(false);
	}
	else if (CmbBz.GetValue()==&quot;4&quot;)
	{	
		lblMe.SetVisible(false);
		lblEmp.SetVisible(true);
	}
	
}"></ClientSideEvents>

                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField IsRequired="True" ErrorText="کد سؤال را انتخاب نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <Items>
                                                        <dxe:ListEditItem Value="4" Text="کارمند" Selected="True"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Value="1" Text="عضو حقیقی"></dxe:ListEditItem>
                                                    </Items>

                                                    <ButtonStyle Width="13px"></ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>

                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="کد عضویت" ClientVisible="False" ID="ASPxLabel34" ClientInstanceName="lblMe"></dxe:ASPxLabel>

                                                <dxe:ASPxLabel runat="server" Text="کد کارمندی" ID="ASPxLabel37" ClientInstanceName="lblEmp"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtBzCode" Width="150px" AutoPostBack="True" ClientInstanceName="TextBzCode" OnTextChanged="txtBzCode_TextChanged">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                                <TSPControls:CustomTextBox runat="server" ID="txtEmpId" ClientVisible="False" Width="100px">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel38"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtBzFirstName" Width="150px" ReadOnly="True" ClientInstanceName="TextBzFirstName">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="ASPxLabel39"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtBzLastName" Width="150px" ReadOnly="True" ClientInstanceName="TextBzLastName">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="کد سری سؤالات" ID="ASPxLabel1"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" dir="ltr" align="right">
                                                <TSPControls:CustomAspxComboBox runat="server" Width="150px" TextField="QuCode" ID="cmbBzQuCode" DataSourceID="ObjectDataSource1" ValueType="System.String" ValueField="QuCode">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField IsRequired="True" ErrorText="کد سؤال را انتخاب نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>

                                                    <ButtonStyle Width="13px"></ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>

                                            </td>
                                            <td style="vertical-align: top" align="right"></td>
                                            <td style="vertical-align: top" dir="ltr" align="right"></td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ شروع" Width="95px" ID="ASPxLabel3"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="160px" ShowPickerOnTop="True" ID="txtBzStartDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr;"></pdc:PersianDateTextBox>

                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBzStartDate" ID="RequiredFieldValidator3" Display="Dynamic">تاریخ شروع را وارد نمایید</asp:RequiredFieldValidator>

                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ پایان" ID="ASPxLabel9"></dxe:ASPxLabel>

                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="160px" ShowPickerOnTop="True" ID="txtBzExpDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr;"></pdc:PersianDateTextBox>

                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBzExpDate" ID="RequiredFieldValidator4" Display="Dynamic">تاریخ پایان را وارد نمایید</asp:RequiredFieldValidator>

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
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                                ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnSave2_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/save.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="نظر سنجی"
                                                ID="btnOpinion1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnOpinion_Click" CausesValidation="False" Visible="False">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/User comment.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="لغو دوره"
                                                ID="btnInactive2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnInActive_Click" CausesValidation="False">
                                                <ClientSideEvents Click="function(s, e) {
		 e.processOnServer= confirm('آیا مطمئن به لغو این دوره هستید؟');
}" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint2" Visible="false" ClientInstanceName="btnEnvelopePrint" runat="server"
                                                Text=" " UseSubmitBehavior="False" ToolTip="چاپ" EnableViewState="False" EnableTheming="False"
                                                AutoPostBack="False">
                                                <Image Height="25px" Url="~/Images/icons/printers.png" Width="25px">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
                                                                        CallbackPanelPeriod.PerformCallback('Print')
                                                                     }" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                            </TSPControls:CustomAspxButton>
                                        </td>                                   
                                        <td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                    CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnBack_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
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
                <asp:HiddenField ID="PeriodId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="MemberId" runat="server" Visible="False"></asp:HiddenField>
                <asp:ObjectDataSource ID="OdbGrades" runat="server" SelectMethod="FindByPKCode" TypeName="TSP.DataManager.TrainingAcceptedGradeManager">
                    <SelectParameters>
                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="PkId"></asp:Parameter>
                        <asp:Parameter Type="Byte" DefaultValue="10" Name="Type"></asp:Parameter>
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:HiddenField ID="HDQuCode" runat="server" Visible="False"></asp:HiddenField>
                <asp:ObjectDataSource ID="OdbQuestion" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="SelectActiveQuestionSet" TypeName="TSP.DataManager.QuestionSetManager"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="SelectActiveQuestionSet" TypeName="TSP.DataManager.QuestionSetManager"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="OdbNezamMember" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="SelectMemberChartMeEmp" TypeName="TSP.DataManager.NezamMemberChartManager"
                    UpdateMethod="Update" InsertMethod="Insert" DeleteMethod="Delete"></asp:ObjectDataSource>
                <dxhf:ASPxHiddenField ID="HDUltId" runat="server" ClientInstanceName="ult">
                </dxhf:ASPxHiddenField>
                <dx:ASPxCallbackPanel ID="CallbackPanelPeriod" runat="server" ClientInstanceName="CallbackPanelPeriod"
                    HideContentOnCallback="True" Width="100%" OnCallback="CallbackPanelPeriod_OnCallback">
                    <ClientSideEvents EndCallback="function(s, e) {              
                if(s.cpPrintPgName!='')                    
                        window.open(s.cpPrintPgName);
                }" />
                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                        </dxp:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                    BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                        <img align="middle" src="../../Image/indicator.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
