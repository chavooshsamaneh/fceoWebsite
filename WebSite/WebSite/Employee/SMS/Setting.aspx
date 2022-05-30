<%@ Page Title="تنظیمات پیام کوتاه" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Setting.aspx.cs" Inherits="Employee_SMS_Setting" %>

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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]</div>

    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                <table >
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                    ID="btnSave" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnSave_Click" UseSubmitBehavior="False">
                                    
                                    <Image  Url="~/Images/icons/save.png">
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
    <TSPControls:CustomASPxRoundPanel ID="RoundPanelMain" HeaderText="مگفا" runat="server"
        Width="100%">
        <PanelCollection>
            <dx:PanelContent>
                <table width="100%">
                    <tr>
                        <td colspan="4" align="right">
                            <ul class="HelpUL">
                                <li>در صورت مشاهده خطا در ارتباط با وب سرویس مگفا مدت زمانی تامل نمایید تا مشکل از جانب
                                    شرکت مگفا برطرف گردد.برای تماس مستقيم با بخش پشتيبانی فنی شرکت مگفا، می‌توانيد با شماره تلفن‌های
                                    (۰۲۱) ۸۸۵۰۶۰۸۹ و (۰۲۱) ۸۸۵۱۰۸۸۷ ارتباط برقرار نماييد. </li>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="اعتبار باقیمانده در مگفا:" ID="ASPxLabel1">
                            </dxe:ASPxLabel>
                        </td>
                        <td width="25%" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" RightToLeft="True" ForeColor="Blue" Font-Bold="true"
                                ID="lblMagfaCreditInfo">
                            </dxe:ASPxLabel>
                        </td>
                        <td width="20%" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="شماره اختصاصی:" ID="ASPxLabel2">
                            </dxe:ASPxLabel>
                        </td>
                        <td width="30%" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" RightToLeft="True" ForeColor="Blue" Font-Bold="true"
                                ID="lblMagfaNumber">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <TSPControls:CustomASPxRoundPanel ID="CustomASPxRoundPanel2" HeaderText="عصر فرا ارتباط"
        runat="server" Width="100%">
        <PanelCollection>
            <dx:PanelContent>
                <table width="100%">
                    <tr>
                        <td width="25%" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="اعتبار باقیمانده در عصر فرا ارتباط:" ID="ASPxLabel6">
                            </dxe:ASPxLabel>
                        </td>
                        <td width="25%" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Font-Bold="true" RightToLeft="True" ForeColor="Blue"
                                ID="lblAFECreditInfo">
                            </dxe:ASPxLabel>
                        </td>
                        <td width="20%" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="شماره اختصاصی:" ID="ASPxLabel4">
                            </dxe:ASPxLabel>
                        </td>
                        <td width="30%" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" RightToLeft="True" ForeColor="Blue" Font-Bold="true"
                                ID="lblAFENumber">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
     <br />
    <TSPControls:CustomASPxRoundPanel ID="CustomASPxRoundPanel3" HeaderText="پویا رایانه دنا"
        runat="server" Width="100%">
        <PanelCollection>
            <dx:PanelContent>
                <table width="100%">
                    <tr>
                        <td width="25%" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="اعتبار باقیمانده در پویا رایانه دنا:" ID="ASPxLabel7">
                            </dxe:ASPxLabel>
                        </td>
                        <td width="25%" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Font-Bold="true" RightToLeft="True" ForeColor="Blue"
                                ID="lblPrdcoCreditInfo">
                            </dxe:ASPxLabel>
                        </td>
                        <td width="20%" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="شماره اختصاصی:" ID="ASPxLabel9">
                            </dxe:ASPxLabel>
                        </td>
                        <td width="30%" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" RightToLeft="True" ForeColor="Blue" Font-Bold="true"
                                ID="lblPrdcoNumber">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <TSPControls:CustomASPxRoundPanel ID="CustomASPxRoundPanel1" ClientInstanceName="RoundPanelMain"
        HeaderText="تنظیمات پیام کوتاه" runat="server" Width="100%">
        <PanelCollection>
            <dx:PanelContent>
                <table width="100%">
                    <tr>
                        <td width="20%" valign="top" align="right">
                            <dxe:ASPxLabel runat="server" Text="وب سرویس جاری" ID="ASPxLabel3">
                            </dxe:ASPxLabel>
                        </td>
                        <td width="30%" valign="top" align="right">
                            <TSPControls:CustomAspxComboBox runat="server"  ID="cmbWebServiceSMSType"
                                 RightToLeft="True" >
                                <ItemStyle HorizontalAlign="Right" />
                                <Items>
                                    <dxe:ListEditItem Selected="true" Text="مگفا" Value="1" />
                                    <dxe:ListEditItem Text="عصرفراارتباط" Value="0" />
                                    <dxe:ListEditItem Text="پویا رایانه دنا" Value="2" />
                                </Items>
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <RequiredField IsRequired="true" ErrorText="لطفا نوع وب سرویس را انتخاب نمائید" />
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td width="25%" valign="top" align="right">
                        </td>
                        <td width="25%" valign="top" align="right">
                        </td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                <table >
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                    ID="btnSave2" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnSave_Click" UseSubmitBehavior="False">
                                  
                                  
                                    <Image  Url="~/Images/icons/save.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
                              </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>

</asp:Content>
