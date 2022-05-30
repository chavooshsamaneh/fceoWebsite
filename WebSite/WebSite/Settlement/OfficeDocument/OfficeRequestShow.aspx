<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeRequestShow.aspx.cs" Inherits="Settlement_OfficeDocument_OfficeRequestShow"
    Title="مشخصات پروانه شخص حقوقی" %>

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
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table class="TableMenu">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
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

            <TSPControls:CustomAspxMenuHorizontal  runat="server" ID="ASPxMenu1" OnItemClick="ASPxMenu1_ItemClick">
                <Items>
                    <dxm:MenuItem Name="Office" Text="مشخصات شرکت" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Member" Text="اعضای شرکت">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Agent" Text="شعبه ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Letters" Text="روزنامه های رسمی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Financial" Text="وضعیت مالی">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>

            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                         <fieldset><legend class="HelpUL">مشخصات شرکت</legend>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td align="right" valign="top" style="width: 15%">
                                        <asp:Label runat="server" Text="نام شرکت" ID="Labe59" Width="100%"></asp:Label>
                                    </td>
                                    <td align="right" valign="top" style="width: 85%">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" ReadOnly="True" ID="txtOfName">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="نام شرکت(انگلیسی)" Width="100%" ID="Label67"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" ReadOnly="True" ID="txtOfNameEn"
                                            ClientInstanceName="txt1">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="موضوع شرکت" ID="Label61" Width="100%"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" ReadOnly="True" ID="txtOfSubject">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="نوع شرکت" ID="Label60" Width="100%"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="50%" ReadOnly="True" ID="txtOfType">
                                        </TSPControls:CustomTextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="شماره ثبت شرکت" Width="100%" ID="Label62"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="50%" ReadOnly="True" ID="txtOfRegNo">
                                        </TSPControls:CustomTextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="محل ثبت شرکت" Width="100%" ID="Label63"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="50%" ReadOnly="True" ID="txtOfRegPlace">
                                        </TSPControls:CustomTextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="تاریخ ثبت شرکت" ID="Label64" Width="100%"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox runat="server" ReadOnly="True" DefaultDate="" Width="225px"
                                            ShowPickerOnTop="True" ID="txtOfRegDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="سرمایه شرکت" ID="Label65" Width="100%"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="50%" ReadOnly="True" ID="txtOfValue">
                                        </TSPControls:CustomTextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="تعداد سهام شرکت" Width="100%" ID="Label66"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="50%" ReadOnly="True" ID="txtOfStock">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="نوع فعالیت" ID="Label68" Visible="false" Width="100%"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="50%" ReadOnly="True" ID="txtOfAttType"
                                            Visible="false">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td style="vertical-align: top"></td>
                                    <td style="vertical-align: top" dir="ltr"></td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="آدرس شرکت" ID="Labe76" Width="100%"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%" ReadOnly="True"
                                            ID="txtOfAddress">
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="تلفن 1" ID="Labe69" Width="100%"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td style="vertical-align: top; text-align: right">
                                                        <TSPControls:CustomTextBox runat="server" Width="108px" MaxLength="8" ReadOnly="True"
                                                            ID="txtOfTel1">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td style="vertical-align: top; text-align: right">
                                                        <asp:Label runat="server" Text="-" Width="13px" ID="Labe71"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: top; text-align: right">
                                                        <TSPControls:CustomTextBox runat="server" Width="38px" MaxLength="4" ReadOnly="True"
                                                            ID="txtOfTel1_pre">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="تلفن 2" ID="Label70"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td style="vertical-align: top; text-align: right">
                                                        <TSPControls:CustomTextBox runat="server" Width="108px" MaxLength="8" ReadOnly="True"
                                                            ID="txtOfTel2">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td style="vertical-align: top; text-align: right">
                                                        <asp:Label runat="server" Text="-" Width="13px" ID="Label72"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: top; text-align: right">
                                                        <TSPControls:CustomTextBox runat="server" Width="38px" MaxLength="4" ReadOnly="True"
                                                            ID="txtOfTel2_pre">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="فکس" ID="Labe73"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td style="vertical-align: top; text-align: right">
                                                        <TSPControls:CustomTextBox runat="server" Width="108px" MaxLength="8" ReadOnly="True"
                                                            ID="txtOfFax">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td style="vertical-align: top; text-align: right">
                                                        <asp:Label runat="server" Text="-" Width="13px" ID="Labe74"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: top; text-align: right">
                                                        <TSPControls:CustomTextBox runat="server" Width="38px" MaxLength="4" ReadOnly="True"
                                                            ID="txtOfFax_pre">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="شماره همراه" ID="Label75" Width="100%"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="50%" MaxLength="11" ReadOnly="True"
                                            ID="txtOfMobile">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="آدرس وب سایت" ID="Labe77" Width="100%"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="50%" ReadOnly="True" ID="txtOfWebsite">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="آدرس پست الکترونیکی" Width="100%" ID="Labe82"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="50%" ReadOnly="True" ID="txtOfEmail">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="تصویر آرم شرکت" ID="Label79"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <dxe:ASPxImage runat="server" Height="75px" ClientVisible="False" Width="75px" ID="imgOfArm"
                                            ClientInstanceName="ImageArm">
                                            <EmptyImage Url="~/Images/noimage.gif" />
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="تصویر مهر شرکت" ID="Label80" Width="100%"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <dxe:ASPxImage runat="server" Height="75px" ClientVisible="False" Width="75px" ID="imgOfSign"
                                            ClientInstanceName="ImageSign">
                                            <EmptyImage Url="~/Images/noimage.gif" />
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="توضیحات" ID="Label81" Width="100%"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%" ReadOnly="True"
                                            ID="txtOfDesc">
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="شرح درخواست" Width="100%" ID="Label19"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <TSPControls:CustomASPXMemo runat="server" Height="30px" Width="100%" ReadOnly="True"
                                            ID="txtReRequestDesc">
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                            </tbody>
                        </table></fieldset>
                        <fieldset><legend class="HelpUL">مشخصات پروانه</legend>
 <table id="Table1" width="100%">
                            <tbody>                             
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="نوع پروانه" Width="100%" ID="ASPxLabel2" ClientInstanceName="lblDate">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" ClientEnabled="False"
                                            ReadOnly="True" ID="txtdMFType">
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td dir="rtl" align="right" valign="top"></td>
                                    <td dir="rtl" align="right" valign="top"></td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="شماره پروانه" Width="100%" ID="ASPxLabel10" ClientInstanceName="lblDate">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" ClientEnabled="False"
                                            Style="direction: ltr" ReadOnly="True" ID="txtdMFNo">
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td dir="rtl" align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ صدور اولین پروانه" Width="100%" ID="ASPxLabel12"
                                            ClientInstanceName="lblFileNo">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td dir="rtl" align="right" valign="top">
                                        <TSPControls:CustomTextBox Style="direction: ltr" runat="server" Width="100%"
                                            ClientEnabled="False" ReadOnly="True" ID="txtdRegDate">
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
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="موقت/دائم" ID="ASPxLabel1" ClientInstanceName="lblPr"
                                            Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            ID="cmbdIsTemporary" ValueType="System.String"
                                            RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <Items>
                                                <dxe:ListEditItem Value="0" Text="پروانه دائم"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="1" Text="پروانه موقت"></dxe:ListEditItem>
                                            </Items>
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td dir="rtl" align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="شماره سریال" ID="ASPxLabel11" ClientInstanceName="lblPr"
                                            Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td dir="rtl" align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" Style="direction: ltr"
                                            ID="txtdSerialNo">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="شماره سریال را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d*"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ آخرین تمدید" ID="ASPxLabel15" ClientInstanceName="lblDate"
                                            Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td dir="rtl" align="right" valign="top">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="225px" ShowPickerOnTop="True"
                                            ID="txtdLastRegDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtdLastRegDate" ID="RequiredFieldValidator8"
                                            Display="Dynamic">تاریخ آخرین تمدید را وارد نمایید</asp:RequiredFieldValidator>
                                    </td>
                                    <td dir="rtl" align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ پایان اعتبار" Width="100%" ID="ASPxLabel17"
                                            ClientInstanceName="lblDate">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td dir="rtl" align="right" valign="top">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="225px" ShowPickerOnTop="True"
                                            ID="txtdExpDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                        <asp:RequiredFieldValidator runat="server" BorderStyle="None" ControlToValidate="txtdExpDate"
                                            ID="RequiredFieldValidator1" Display="Dynamic">تاریخ پایان اعتبار را وارد نمایید</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        </fieldset>
                        <fieldset><legend class="HelpUL">فایل های پیوست</legend>
                        <TSPControls:CustomAspxDevGridView2 runat="server" EnableViewState="False" Width="100%"
                            ID="AspxGridFlp" KeyFieldName="AttachId">
                            <Columns>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FilePath" Caption="فایل"
                                    Name="FilePath">
                                    <DataItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text="آدرس فایل" NavigateUrl='<%# Bind("FilePath") %>'
                                            Target="_blank"></asp:HyperLink>
                                    </DataItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
                                    </EditItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Description" Caption="توضیحات"
                                    Name="Description">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </TSPControls:CustomAspxDevGridView2></fieldset>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />           
         
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table class="TableMenu">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
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
            <asp:HiddenField ID="OfficeId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <asp:ObjectDataSource ID="OdbOfType" runat="server" UpdateMethod="Update" TypeName="TSP.DataManager.OfficeTypeManager"
                SelectMethod="GetData" OldValuesParameterFormatString="original_{0}" InsertMethod="Insert"
                DeleteMethod="Delete">
                <InsertParameters>
                    <asp:Parameter Name="OtName" Type="String" />
                    <asp:Parameter Name="ModifiedDate" Type="DateTime" />
                </InsertParameters>
                <DeleteParameters>
                    <asp:Parameter Name="Original_OtId" Type="Int16" />
                    <asp:Parameter Name="Original_LastTimeStamp" Type="Object" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="OtName" Type="String" />
                    <asp:Parameter Name="ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="Original_OtId" Type="Int16" />
                    <asp:Parameter Name="Original_LastTimeStamp" Type="Object" />
                    <asp:Parameter Name="OtId" Type="Int32" />
                </UpdateParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbOfAtType" runat="server" UpdateMethod="Update" TypeName="TSP.DataManager.OfficeActivityTypeManager"
                SelectMethod="GetData" InsertMethod="Insert" DeleteMethod="Delete">
                <InsertParameters>
                    <asp:Parameter Name="OatId" Type="Int32" />
                    <asp:Parameter Name="OatName" Type="String" />
                    <asp:Parameter Name="ModifiedDate" Type="DateTime" />
                </InsertParameters>
                <DeleteParameters>
                    <asp:Parameter Name="Original_OatId" Type="Int32" />
                    <asp:Parameter Name="Original_LastTimeStamp" Type="Object" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="OatId" Type="Int32" />
                    <asp:Parameter Name="OatName" Type="String" />
                    <asp:Parameter Name="ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="Original_OatId" Type="Int32" />
                    <asp:Parameter Name="Original_LastTimeStamp" Type="Object" />
                </UpdateParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBMrsId" runat="server" TypeName="TSP.DataManager.MembershipRegistrationStatusManager"
                SelectMethod="GetData" EnableCaching="True" CacheDuration="30"></asp:ObjectDataSource>
            <dxhf:ASPxHiddenField ID="HDFlpArm" runat="server" ClientInstanceName="flpArm2">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpSign" runat="server" ClientInstanceName="flpSign2">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpMember" runat="server" ClientInstanceName="flpme">
            </dxhf:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
