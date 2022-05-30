<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddPeriodRegister.aspx.cs" Inherits="Employee_Amoozesh_AddPeriodRegister"
    Title="ثبت نام دوره" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]</div>
      
                            <TSPControls:CustomASPxRoundPanel ID="RoundPanelPeriodRegister" HeaderText="دوره آموزشی"
                                runat="server" Width="100%">
                                <PanelCollection>
                                    <dxp:PanelContent>
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="کد دوره:" Width="100%" ID="ASPxLabel3">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <dxe:ASPxLabel runat="server" Text="- - -" Width="100%" ID="lblPPCode" Font-Bold="true">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="عنوان دوره:" Width="100%" ID="ASPxLabel4">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <dxe:ASPxLabel runat="server" Text="- - -" Width="100%" ID="lblPeriodTittle" Font-Bold="true">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                              
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="موسسه:" Width="100%" ID="ASPxLabel9">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="- - -" Width="100%" ID="lblInsName" Font-Bold="true">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="مکان برگزاری:" Width="100%" ID="ASPxLabel2">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="- - -" Width="100%" ID="lblPeriodPlace" Font-Bold="true">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                  <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ شروع دوره:" Width="100%" ID="ASPxLabel1">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="- - -" Width="100%" ID="lblStartDate" Font-Bold="true">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ پایان دوره:" Width="100%" ID="ASPxLabel5">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="- - -" Width="100%" ID="lblEndDate" Font-Bold="true">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ امتحان:" Width="100%" ID="ASPxLabel6">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="- - -" Width="100%" ID="lblTestDate" Font-Bold="true">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="ساعت امتحان:" Width="100%" ID="ASPxLabel7">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="- - -" Width="98px" ID="lblTestHourse" Font-Bold="true">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="ظرفیت دوره:" Width="100%" ID="ASPxLabel10">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="- - -" Width="100%" ID="lblCapacity" Font-Bold="true">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="ظرفیت باقیمانده:" Width="100%" ID="ASPxLabel12">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="- - -" Width="100%" ID="lblRemainCapacity" Font-Bold="true">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="هزینه دوره:" Width="100%" ID="ASPxLabel8">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="- - -" Width="100%" ID="lblPeriodCost" Font-Bold="true">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="نحوه برگزاری:" Width="100%" ID="ASPxLabel11">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="- - -" Width="100%" ID="lblPeriodType" Font-Bold="true">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </TSPControls:CustomASPxRoundPanel>
                        
                            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشخصات ثبت نام"
                                runat="server" Width="100%">
                                <PanelCollection>
                                    <dxp:PanelContent>
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="نوع ثبت نام:" Width="80px" ID="ASPxLabel14">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="145px" 
                                                            ID="cmbRegisterType"  AutoPostBack="True" ValueType="System.String"
                                                             OnSelectedIndexChanged="cmbRegisterType_SelectedIndexChanged">
                                                            <Items>
                                                                <dxe:ListEditItem Value="0" Text=" دوره و آزمون"></dxe:ListEditItem>
                                                                <dxe:ListEditItem Value="1" Text="آزمون"></dxe:ListEditItem>
                                                                <dxe:ListEditItem Value="2" Text="دوره"></dxe:ListEditItem>
                                                            </Items>
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="نحوه پرداخت:" Width="80px" ID="ASPxLabel13">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="145px" 
                                                            ID="cmbPaymentType"  ValueType="System.String" 
                                                            SelectedIndex="3" Enabled="false">
                                                            <Items>
                                                                <dxe:ListEditItem Value="1" Text="نقدی"></dxe:ListEditItem>
                                                                <dxe:ListEditItem Value="2" Text="فیش بانکی"></dxe:ListEditItem>
                                                                <dxe:ListEditItem Value="3" Text="کارت"></dxe:ListEditItem>
                                                            </Items>
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" align="center">
                                                        <br />
                                                        <table>
                                                            <tr>
                                                                <td valign="top" align="right">
                                                                    <TSPControls:CustomAspxButton runat="server" Text="&nbsp;&nbsp;&nbsp;پرداخت" Width="122px" ID="btnFinish"
                                                                        EncodeHtml="false" OnClick="btnFinish_Click" 
                                                                         UseSubmitBehavior="false" CausesValidation="False">
                                                                        <Image Width="20px" Height="20px" Url="~/Images/icons/Payment.png" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td valign="top" align="left">
                                                                    <TSPControls:CustomAspxButton runat="server" Text="&nbsp;&nbsp;&nbsp;بازگشت" Width="122px" ID="btnBack"
                                                                        EncodeHtml="false" OnClick="btnBack_Click" 
                                                                         UseSubmitBehavior="false" CausesValidation="False">
                                                                        <Image Width="20px" Height="20px" Url="~/Images/icons/Back.png" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </TSPControls:CustomASPxRoundPanel>
                   
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldCourseRegister">
            </dxhf:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                <img align="middle" src="../../Image/indicator.gif" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
