<%@ Page Title="مشخصات نامزد انتخابات" Language="C#" MasterPageFile="~/MasterPageWebsite.master"
    AutoEventWireup="true" CodeFile="ManagersCandidateView.aspx.cs" Inherits="Association_ManagersCandidateView" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
 <script type="text/javascript" src="../Script/Utility.js"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="DivReport" runat="server" class="DivErrors" align="right">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]</div>
               <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dx:PanelContent>


  
            
                        <table cellpadding="0" width="100%">
                            <tr>
                                <td align="right" valign="top">
                                    <dx:ASPxButton ID="btnBack" runat="server"  EnableTheming="False"
                                        EnableViewState="False" ToolTip="بازگشت" PostBackUrl="ManagersCandidate.aspx"
                                        CausesValidation="False">
                                        <Image Url="~/Images/icons/Back.png">
                                        </Image>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                   </dx:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="CustomASPxRoundPanel1" HeaderText="مشخصات تشکل"
                runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td align="right" valign="top" width="15%">
                                        <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="نوع تشکل" Width="100%">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" width="35%">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtExGroupName"  Width="100%"
                                            Enabled="false" >
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top" width="15%">
                                        <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="نام دوره انتخاباتی" Width="100%">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td align="left" valign="top" width="35%">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtPeriodName"  Width="100%"
                                            Enabled="false" >
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" width="15%">
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="تاریخ شروع تبلیغات" Width="100%">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td align="right" dir="ltr" valign="top" width="35%">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtStartDatePropagation"  Width="100%"
                                            Enabled="false" RightToLeft="False" >
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top" width="15%">
                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="تاریخ پایان تبلیغات" Width="100%">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td align="left" dir="ltr" valign="top" width="35%">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtEndDatePropagation"  Width="100%"
                                            Enabled="false" RightToLeft="False" >
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" width="15%">
                                        <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="تاریخ شروع" Width="100%">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td align="right" dir="ltr" valign="top" width="35%">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtStartDate"  Width="100%"
                                            Enabled="false" RightToLeft="False" >
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top" width="15%">
                                        <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="تاریخ پایان" Width="100%">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td align="left" dir="ltr" valign="top" width="35%">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtEndDate"  Width="100%" Enabled="false"
                                            RightToLeft="False" >
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="CustomASPxRoundPanel2" HeaderText="مشخصات نامزد انتخاباتی"
                runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td colspan="2">
                                        <TSPControls:CustomASPxRoundPanel ID="CustomASPxRoundPanel3" HeaderText="مشخصات فردی"
                                            runat="server" Width="100%">
                                            <PanelCollection>
                                                <dx:PanelContent>
                                                    <table style="width: 100%">
                                                        <tbody>
                                                            <tr>
                                                                <td valign="top" align="right" width="15%">
                                                                    <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="labelMeId">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td valign="top" align="right" width="50%">
                                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMeId"  Width="100%" ClientInstanceName="txtMeId"
                                                                        Enabled="false"  AutoPostBack="false">
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td valign="middle" align="center" width="35%" colspan="2" rowspan="11">
                                                                    <dx:ASPxImage ID="ImgMember" runat="server" ImageUrl="~/Images/Person.png" Width="100px"
                                                                        Height="120px">
                                                                    </dx:ASPxImage>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="right" width="15%">
                                                                    <dxe:ASPxLabel runat="server" Text="کد انتخاباتی" ID="ASPxLabel5">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td valign="top" align="right" width="35%">
                                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtCandidateCode"  Width="100%"
                                                                        Enabled="false"  AutoPostBack="false">
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td valign="middle" align="center" colspan="2">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="right" width="15%">
                                                                    <dxe:ASPxLabel runat="server" Text="نام" ID="labelFirstName" Width="100%">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td valign="top" align="right" width="35%">
                                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFirstName"  Width="100%"
                                                                        Enabled="false" ClientInstanceName="txtFirstName" >
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td valign="middle" align="center" colspan="2">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="right" width="15%">
                                                                    <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="labelLastName">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td valign="top" align="left" width="35%">
                                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtLastName"  Width="100%"
                                                                        Enabled="false" ClientInstanceName="txtLastName" >
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td valign="middle" align="center" colspan="2">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="right" width="15%">
                                                                    <dxe:ASPxLabel runat="server" Text="رشته تحصیلی" ID="ASPxLabel3">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td valign="top" align="right" width="35%">
                                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMajor"  Width="100%" Enabled="false"
                                                                        >
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td valign="middle" align="center" colspan="2">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="right" width="15%">
                                                                    <dxe:ASPxLabel runat="server" Text="دانشگاه" ID="ASPxLabel4">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td valign="top" align="right" width="35%">
                                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtUniversity"  Width="100%"
                                                                        Enabled="false" >
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td valign="middle" align="center" colspan="2">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="right">
                                                                    <dxe:ASPxLabel runat="server" Text="پایه اجرا" ID="ASPxLabel6">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td valign="top" align="right">
                                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtImpName"  Width="100%" Enabled="false"
                                                                        >
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td valign="middle" align="center" colspan="2">
                                                                </td>
                                                                <tr>
                                                                    <tr>
                                                                        <td valign="top" align="right">
                                                                            <dxe:ASPxLabel runat="server" Text="پایه نظارت" ID="ASPxLabel7">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtObsName"  Width="100%" Enabled="false"
                                                                                >
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                        <td valign="middle" align="center" colspan="2">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" align="right">
                                                                            <dxe:ASPxLabel runat="server" Text="پایه طراحی" ID="ASPxLabel8">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtDesName"  Width="100%" Enabled="false"
                                                                                >
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                        <td valign="middle" align="center" colspan="2">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" align="right">
                                                                            <dxe:ASPxLabel runat="server" Text="پایه شهرسازی" ID="ASPxLabel9">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtUrbonism"  Width="100%"
                                                                                Enabled="false" >
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                        <td valign="middle" align="center" colspan="2">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" align="right">
                                                                            <dxe:ASPxLabel runat="server" Text="پایه نقشه برداری" ID="ASPxLabel12">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMapping"  Width="100%" Enabled="false"
                                                                                >
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                        <td valign="middle" align="center" colspan="2">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" align="right">
                                                                            <dxe:ASPxLabel runat="server" Text="پایه ترافیک" ID="ASPxLabel13">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtTraffic"  Width="100%" Enabled="false"
                                                                                >
                                                                            </TSPControls:CustomTextBox>
                                                                            <td valign="middle" align="center" colspan="2">
                                                                            </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" align="center">
                                                                            <br />
                                                                            <dxe:ASPxHyperLink ID="HyperLinkAttachment" runat="server" Target="_blank" Text="دانلود اطلاعات بیشتر ...."
                                                                                Visible="False">
                                                                            </dxe:ASPxHyperLink>
                                                                        </td>
                                                                    </tr>
                                                        </tbody>
                                                    </table>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </TSPControls:CustomASPxRoundPanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <TSPControls:CustomASPxRoundPanel ID="RoundPanelResume" HeaderText="خلاصه رزومه"
                                            Visible="false" runat="server" Width="100%">
                                            <PanelCollection>
                                                <dx:PanelContent>
                                                    <table style="width: 100%">
                                                        <tbody>
                                                            <tr>
                                                                <td colspan="4" valign="top" align="right" width="100%" colspan="3">
                                                                    <TSPControls:CustomASPxHtmlEditor ID="txtResume" ActiveView="Preview" runat="server"
                                                                        Width="100%">
                                                                        <Settings AllowContextMenu="False" AllowHtmlView="false" AllowDesignView="false"
                                                                            AllowPreview="true" />
                                                                    </TSPControls:CustomASPxHtmlEditor>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </TSPControls:CustomASPxRoundPanel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
               <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dx:PanelContent>


  
                        <table cellpadding="0" width="100%">
                            <tr>
                                <td align="right" valign="top">
                                    <dx:ASPxButton ID="btnBack2" runat="server"  EnableTheming="False"
                                        EnableViewState="False" ToolTip="بازگشت" PostBackUrl="ManagersCandidate.aspx"
                                        CausesValidation="False">
                                        <Image Url="~/Images/icons/Back.png">
                                        </Image>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                 </dx:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                        <img src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
