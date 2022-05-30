<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="PeriodsView.aspx.cs" Inherits="PeriodsView" Title="مشخصات دوره آموزشی" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxe:PanelContent>
                        <table>
                            <tbody>
                                <tr>
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
                    </dxe:PanelContent>

                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
        <ul class="HelpUL">
                    <li><b>توجه!</b>جهت ثبت نام بخش توضیحات به طور دقیق مطالعه شود</li>
                </ul>
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="دوره آموزشی" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <fieldset>
                            <legend class="HelpUL">اطلاعات پایه</legend>

                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="عنوان دوره" ID="ASPxLabel10">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3" style="width: 85%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtCrsId" ReadOnly="True"
                                                Width="100%">
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
                                            <dxe:ASPxLabel runat="server" Text="طول دوره(ساعت)" Width="101px" ID="ASPxLabel6">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtPDuration" ReadOnly="True"
                                                Width="100%" ClientInstanceName="TextDur">
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
                                            <dxe:ASPxLabel runat="server" Text="امتیاز" ID="ASPxLabel13">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtPoint" ReadOnly="True"
                                                Width="100%">
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
                                            <dxe:ASPxLabel runat="server" Text="مدت زمان اعتبار(ماه)" Width="110px" ID="ASPxLabel17">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtValidDuration" ReadOnly="True"
                                                Width="100%">
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
                                            <dxe:ASPxLabel runat="server" Text="مدت زمان عملی(ساعت)" Width="134px" ID="ASPxLabel24">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtbPracticalDuration" ReadOnly="True"
                                                Width="100%">
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
                                            <dxe:ASPxLabel runat="server" Text="مدت زمان تئوری(ساعت)" ID="ASPxLabel30">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtbNonPracticalDuration"
                                                ReadOnly="True" Width="100%">
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
                                            <dxe:ASPxLabel runat="server" Text="مدت زمان بازدید از کارگاه(ساعت)" Width="100px"
                                                ID="ASPxLabel25">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtbWorkroomDuration" ReadOnly="True"
                                                Width="100%">
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
                                            <dxe:ASPxLabel runat="server" Text="کد دوره" ID="ASPxLabel18">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtPPCode" ReadOnly="True"
                                                Width="100%">
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
                                            <dxe:ASPxLabel runat="server" Text="ظرفیت دوره" ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtCapacity" ReadOnly="True"
                                                Width="100%">
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
                                            <dxe:ASPxLabel runat="server" Text="هزینه دوره(بدون تخفیف)" Width="124px" ID="ASPxLabel19">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtPeriodCost" ReadOnly="True"
                                                Width="100%">
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
                                            <dxe:ASPxLabel runat="server" Text="تخفیف" ID="ASPxLabel20">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtDiscount" ReadOnly="True"
                                                Width="100%">
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
                                            <dxe:ASPxLabel runat="server" Text="تاریخ شروع" ID="ASPxLabel12">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtStartDate" ReadOnly="True"
                                                Width="100%" Style="direction: ltr">
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
                                            <dxe:ASPxLabel runat="server" Text="تاریخ پایان" Width="56px" ID="ASPxLabel11">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtEndDate" ReadOnly="True"
                                                Width="100%" Style="direction: ltr">
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
                                            <dxe:ASPxLabel runat="server" Text="تاریخ آزمون" ID="ASPxLabel4">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtTestDate" ReadOnly="True"
                                                Width="100%" Style="direction: ltr">
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
                                            <dxe:ASPxLabel runat="server" Text="ساعت شروع آزمون" Width="101px" ID="ASPxLabel5">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtTestHour" ReadOnly="True"
                                                Width="100%">
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
                                            <dxe:ASPxLabel runat="server" Text="محل برگزاری آزمون" ID="ASPxLabel26">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtTestPlace" ReadOnly="True"
                                                Width="100%">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText="محل برگزاری را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="محل برگزاری دوره" ID="ASPxLabel7">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtPlace" ReadOnly="True"
                                                Width="100%">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText="محل برگزاری را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel15">
                                            </dxe:ASPxLabel>
                                            <br />
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="35px" ID="txtDesc" ReadOnly="True"
                                                Width="100%">
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>

                        <fieldset>
                            <legend class="HelpUL">مشخصات مؤسسه</legend>

                            <table dir="rtl" cellpadding="1" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="نام مؤسسه" ID="ASPxLabel16">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 85%" colspan="3">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsName" ReadOnly="True"
                                                Width="100%">
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
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="نام مدیر مؤسسه" Width="86px" ID="ASPxLabel21">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 85%" colspan="3">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsManager" ReadOnly="True"
                                                Width="100%">
                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح تکمیل نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=" "></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="شهر" ID="ASPxLabel41">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsCitName" ReadOnly="True"
                                                Width="100%">
                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=" "></RequiredField>
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="0\d{10}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="شماره تلفن همراه" Width="100px" ID="ASPxLabel35">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsMobileNo" MaxLength="11"
                                                ReadOnly="True" Width="100%">
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
                                            <dxe:ASPxLabel runat="server" Text="شماره تلفن1" ID="ASPxLabel33">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsTel1" MaxLength="12"
                                                ReadOnly="True" Width="100%"
                                                Style="direction: ltr">
                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=" "></RequiredField>
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="0\d{8,11}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره تلفن2" ID="ASPxLabel34">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsTel2" MaxLength="12"
                                                ReadOnly="True" Width="100%"
                                                Style="direction: ltr">
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
                                            <dxe:ASPxLabel runat="server" Text="آدرس پست الکترونیکی" Width="120px" ID="ASPxLabel37">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsEmail" ReadOnly="True"
                                                Width="100%">
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
                                            <dxe:ASPxLabel runat="server" Text="آدرس وب سایت" Width="120px" ID="ASPxLabel38">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomTextBox runat="server" ID="txtInsWebSite" ReadOnly="True"
                                                Width="100%">
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
                                            <dxe:ASPxLabel runat="server" Text="آدرس" ID="ASPxLabel36">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="25px" ID="txtInsAddress"
                                                ReadOnly="True" Width="100%">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText=" "></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel39">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="30px" ID="txtInsDesc" ReadOnly="True"
                                                Width="100%">
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
                                RightToLeft="True" ID="CustomAspxDevGridView2" KeyFieldName="TrTeId" AutoGenerateColumns="False"
                                ClientInstanceName="grid">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="TeId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TeName" Width="150px" Caption="نام و نام خانوادگی">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                        <EditFormSettings Visible="False"></EditFormSettings>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="LiName" Caption="مدرک تحصیلی">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="PracticalHour" Caption="زمان عملی">
                                        <PropertiesTextEdit Width="100px">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="WorkroomHour" Caption="زمان کارگاه">
                                        <PropertiesTextEdit Width="100px">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="Id">
                                        <EditFormSettings Visible="False"></EditFormSettings>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewCommandColumn VisibleIndex="11" Caption=" " ShowClearFilterButton="true">
                                    </dxwgv:GridViewCommandColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView2>
                        </fieldset>
                        <fieldset>
                            <legend class="HelpUL">زمان برگزاری جلسات</legend>

                            <TSPControls:CustomAspxDevGridView2 runat="server" EnableViewState="False"
                                Width="100%" RightToLeft="True" EnableCallBacks="False" ID="CustomAspxDevGridView1"
                                KeyFieldName="SchId" AutoGenerateColumns="False"
                                OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize"
                                OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared">
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
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Date" Caption="تاریخ">
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
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="StartTime" Caption="ساعت شروع">
                                        <EditCellStyle HorizontalAlign="Right">
                                        </EditCellStyle>
                                        <PropertiesTextEdit Width="100px">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="ساعت شروع را وارد نمایید "></RequiredField>
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="EndTime" Caption="ساعت پایان">
                                        <EditCellStyle HorizontalAlign="Right">
                                        </EditCellStyle>
                                        <PropertiesTextEdit Width="100px">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="ساعت پایان را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataMemoColumn VisibleIndex="3" FieldName="Description" Caption="توضیحات">
                                        <PropertiesMemoEdit Width="280px">
                                        </PropertiesMemoEdit>
                                        <EditCellStyle HorizontalAlign="Right">
                                        </EditCellStyle>
                                        <EditFormSettings ColumnSpan="2"></EditFormSettings>
                                    </dxwgv:GridViewDataMemoColumn>
                                    <dxwgv:GridViewCommandColumn VisibleIndex="6" Caption=" " ShowClearFilterButton="true">
                                    </dxwgv:GridViewCommandColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView2>
                        </fieldset>
                        <fieldset>
                            <legend class="HelpUL">امتیاز بندی برای رشته های مختلف</legend>

                            <TSPControls:CustomAspxDevGridView2 runat="server" EnableViewState="False"
                                Width="100%" RightToLeft="True" ID="CustomAspxDevGridViewGrade" DataSourceID="OdbGrades"
                                KeyFieldName="TrGrId" AutoGenerateColumns="False">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GrdName" Caption="پایه">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="ResName" Caption="صلاحیت">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjName" Caption="رشته">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Description" Caption="توضیحات">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView2>
                        </fieldset>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>


                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                            ToolTip="بازگشت" UseSubmitBehavior="False">
                                           
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
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img align="middle" src="Image/indicator.gif" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
    <asp:ObjectDataSource ID="OdbGrades" runat="server" SelectMethod="FindByPKCode" TypeName="TSP.DataManager.TrainingAcceptedGradeManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="PkId" Type="Int32" />
            <asp:Parameter DefaultValue="10" Name="Type" Type="Byte" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
