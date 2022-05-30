<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="PeriodsView.aspx.cs" Inherits="Members_Amoozesh_PeriodsView"
    Title="مشخصات دوره" %>

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
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>
                <div style="width: 100%" align="right" dir="ltr">
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelHeader" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <div dir="rtl">
                                    <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                        width="100%">
                                        <tbody>
                                            <tr>
                                                <td align="right">
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                        cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                        CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnBack_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/Back.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>
                </div>
                 <ul class="HelpUL">
                <li><b>توجه!</b>جهت ثبت نام بخش توضیحات به طور دقیق مطالعه شود</li>
            </ul>
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelPeriod" HeaderText="دوره آموزشی"
                    runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <fieldset><legend class="HelpUL">اطلاعات پایه</legend>
                           
                                        <table dir="rtl" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="عنوان دوره" ID="ASPxLabel10" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="85%" colspan="3">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtCrsId"  Width="100%" ReadOnly="True"
                                                            >
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
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="طول دوره(ساعت)" Width="100%" ID="ASPxLabel6">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtPDuration"  Width="100%"
                                                            ReadOnly="True" ClientInstanceName="TextDur" >
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="امتیاز" ID="ASPxLabel13" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtPoint"  Width="100%" ReadOnly="True"
                                                            >
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
                                                        <TSPControls:CustomTextBox runat="server" ID="txtValidDuration"  Width="100%"
                                                            ReadOnly="True" >
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
                                                        <TSPControls:CustomTextBox runat="server" ID="txtbPracticalDuration"  Width="100%"
                                                            ReadOnly="True" >
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
                                                            Width="100%" ReadOnly="True" >
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
                                                        <TSPControls:CustomTextBox runat="server" ID="txtbWorkroomDuration"  Width="100%"
                                                            ReadOnly="True" >
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
                                                        <TSPControls:CustomTextBox runat="server" ID="txtPPCode"  Width="100%" ReadOnly="True"
                                                             __designer:wfdid="w82">
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
                                                        <TSPControls:CustomTextBox runat="server" ID="txtCapacity"  Width="100%"
                                                            ReadOnly="True" >
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
                                                        <dxe:ASPxLabel runat="server" Text="هزینه دوره(بدون تخفیف) ریال" Width="100%" ID="ASPxLabel19"
                                                            __designer:wfdid="w85">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtPeriodCost"  Width="100%"
                                                            ReadOnly="True" >
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
                                                        <dxe:ASPxLabel runat="server" Text="هزینه آزمون ریال" ID="ASPxLabel20" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtTestCost" 
                                                            ReadOnly="True" >
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
                                                        <TSPControls:CustomTextBox runat="server" Style="direction: ltr" ID="txtStartDate" 
                                                            Width="100%" ReadOnly="True" >
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
                                                        <TSPControls:CustomTextBox runat="server" Style="direction: ltr" ID="txtEndDate" 
                                                            Width="100%" ReadOnly="True" 
                                                            __designer:wfdid="w92">
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
                                                        <TSPControls:CustomTextBox runat="server" Style="direction: ltr" ID="txtTestDate" 
                                                            Width="100%" ReadOnly="True" >
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
                                                        <TSPControls:CustomTextBox runat="server" Style="direction: ltr" ID="txtTestHour" 
                                                            Width="100%" ReadOnly="True" >
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
                                                        <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtTestPlace"  Width="100%"
                                                            ReadOnly="True" >
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
                                                        <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtPlace"  Width="100%"
                                                            ReadOnly="True" >
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField ErrorText="محل برگزاری را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel15" Width="100%">
                                                        </dxe:ASPxLabel>
                                                        <br />
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="71px" ID="txtDesc"  Width="100%"
                                                            ReadOnly="True" >
                                                        </TSPControls:CustomASPXMemo>
                                                        <br />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                   </fieldset>
                        <fieldset><legend class="HelpUL">مشخصات مؤسسه</legend>
                           
                                        <table align="right" dir="rtl" cellpadding="1" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="نام مؤسسه" ID="ASPxLabel16" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtInsName"  Width="100%" ReadOnly="True"
                                                            >
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
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="نام مدیر مؤسسه" Width="100%" ID="ASPxLabel21">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtInsManager"  Width="100%"
                                                            ReadOnly="True" >
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
                                                        <dxe:ASPxLabel runat="server" Text="شماره ثبت مؤسسه" Width="100%" ID="ASPxLabel23">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox Style="direction: ltr" runat="server" ID="txtIRegNo" 
                                                            Width="100%" ReadOnly="True" >
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
                                                        <dxe:ASPxLabel runat="server" Text="شماره همراه" ID="ASPxLabel35" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtIMobileNo"  Width="100%"
                                                            ReadOnly="True"  __designer:wfdid="w111">
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
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ ثبت مؤسسه" ID="ASPxLabel31" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" Style="direction: ltr" ID="txtIRegDate" 
                                                            Width="100%" ReadOnly="True" >
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
                                                        <dxe:ASPxLabel runat="server" Text="محل ثبت مؤسسه" ID="ASPxLabel32" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtIRegPlace"  Width="100%"
                                                            ReadOnly="True" >
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
                                                        <dxe:ASPxLabel runat="server" Text="شماره تلفن1" ID="ASPxLabel33" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtITel1"  Width="100%" ReadOnly="True"
                                                            >
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
                                                        <dxe:ASPxLabel runat="server" Text="شماره تلفن2" ID="ASPxLabel34" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtITel2"  Width="100%" ReadOnly="True"
                                                            >
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
                                                        <dxe:ASPxLabel runat="server" Text="شماره مجوز آموزشی" ID="ASPxLabel22" Visible="False">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" Style="direction: ltr" ID="txtIFileNo" 
                                                            Width="145px" ReadOnly="True" 
                                                            Visible="False">
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
                                                    </td>
                                                    <td valign="top" align="right">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="آدرس پست الکترونیکی" Width="100%" ID="lbl">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtIEmail"  Width="100%" ReadOnly="True"
                                                            >
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
                                                        <dxe:ASPxLabel runat="server" Text="آدرس وب سایت" ID="ASPxLabel36" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtIWebSite"  Width="100%"
                                                            ReadOnly="True" >
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
                                                        <dxe:ASPxLabel runat="server" Text="آدرس" ID="ASPxLabel37" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtIAddress"  Width="100%"
                                                            ReadOnly="True" >
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField ErrorText="محل برگزاری را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel38">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtIDesc"  Width="100%"
                                                            ReadOnly="True" >
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField ErrorText="محل برگزاری را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table></fieldset>
                                 
                        <fieldset><legend class="HelpUL">مشخصات مدرس</legend>
                           
                                            <TSPControls:CustomAspxDevGridView runat="server"  Width="100%"
                                                ID="CustomAspxDevGridView2" KeyFieldName="TrTeId" AutoGenerateColumns="False"
                                                ClientInstanceName="grid"  __designer:wfdid="w132">
                                                <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
                                                <ClientSideEvents RowClick="function(s, e) {
	SetControlValues();
	btn.SetEnabled(false);
}"></ClientSideEvents>
                                                <Settings ShowHorizontalScrollBar="true" />
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
                                            </TSPControls:CustomAspxDevGridView>
                                     </fieldset>  
                         <fieldset><legend class="HelpUL">زمان برگزاری جلسات</legend>
                                            <TSPControls:CustomAspxDevGridView runat="server"  EnableViewState="False"
                                                EnableCallBacks="False" ID="CustomAspxDevGridView1" KeyFieldName="SchId" AutoGenerateColumns="False"
                                                 __designer:wfdid="w134" Width="100%">
                                                <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
                                                <Settings ShowHorizontalScrollBar="true" />
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="SchId">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Number" Caption="شماره جلسه"
                                                        Width="100px">
                                                        <EditCellStyle HorizontalAlign="Right">
                                                        </EditCellStyle>
                                                        <PropertiesTextEdit Width="100px">
                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d*"></RegularExpression>
                                                            </ValidationSettings>
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Date" Caption="تاریخ" Width="100px">
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
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="StartTime" Caption="ساعت شروع"
                                                        Width="100px">
                                                        <EditCellStyle HorizontalAlign="Right">
                                                        </EditCellStyle>
                                                        <PropertiesTextEdit Width="100px">
                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="True" ErrorText="ساعت شروع را وارد نمایید "></RequiredField>
                                                            </ValidationSettings>
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="EndTime" Caption="ساعت پایان"
                                                        Width="100px">
                                                        <EditCellStyle HorizontalAlign="Right">
                                                        </EditCellStyle>
                                                        <PropertiesTextEdit Width="100px">
                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="True" ErrorText="ساعت پایان را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataMemoColumn VisibleIndex="4" FieldName="Description" Caption="توضیحات"
                                                        Width="350px">
                                                        <PropertiesMemoEdit Width="280px">
                                                        </PropertiesMemoEdit>
                                                        <EditCellStyle HorizontalAlign="Right">
                                                        </EditCellStyle>
                                                        <EditFormSettings ColumnSpan="2"></EditFormSettings>
                                                    </dxwgv:GridViewDataMemoColumn>
                                                </Columns>
                                            </TSPControls:CustomAspxDevGridView>
                                    </fieldset>  
                         <fieldset><legend class="HelpUL">نظر سنجی شرکت کنندگان دوره</legend>
                              
                                        <table dir="rtl" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="کد سری سؤالات" Width="100%" ID="ASPxLabel27">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtOpQucode"  Width="100%"
                                                            ReadOnly="True" >
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
                                                    <td valign="top" align="right" width="15%">
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ شروع" ID="ASPxLabel28" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" Style="direction: ltr" ID="txtOpStartDate" 
                                                            Width="100%" ReadOnly="True" >
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
                                                        <TSPControls:CustomTextBox runat="server" Style="direction: ltr" ID="txtOpEndDate" 
                                                            Width="100%" ReadOnly="True" >
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
                                        </table></fieldset>
                                   <fieldset><legend class="HelpUL">امتیاز بندی برای رشته های مختلف</legend>
                          
                                        <TSPControls:CustomAspxDevGridView2 runat="server"  EnableViewState="False"
                                            Width="100%" RightToLeft="True" ID="CustomAspxDevGridViewGrade" DataSourceID="OdbGrades"
                                            KeyFieldName="TrGrId" AutoGenerateColumns="False" >
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
                  <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                    <table  >
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                        CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnBack_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/Back.png">
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
                <asp:HiddenField ID="InstitueId" runat="server" Visible="False" />
                <asp:HiddenField ID="HDQuCode" runat="server" Visible="False" />
                <asp:ObjectDataSource ID="OdbQuestion" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="SelectActiveQuestionSet" TypeName="TSP.DataManager.QuestionSetManager">
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetData" TypeName="TSP.DataManager.QuestionSetManager"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="OdbGrades" runat="server" SelectMethod="FindByPKCode" TypeName="TSP.DataManager.TrainingAcceptedGradeManager">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="PkId" Type="Int32" />
                        <asp:Parameter DefaultValue="10" Name="Type" Type="Byte" />
                    </SelectParameters>
                </asp:ObjectDataSource>
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
</asp:Content>
