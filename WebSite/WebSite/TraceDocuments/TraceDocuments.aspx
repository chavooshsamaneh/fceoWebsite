<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="TraceDocuments.aspx.cs" Inherits="TraceDocuments_TraceDocuments" Title="بررسی اصالت اسناد" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <div>
        <div class="container-fluid-login LoginPage">
            <div class="container-fluid-login-Header">
                <span>بررسی اصالت اسناد</span>
            </div>
            <div class="container-fluid-login-Body">
                <div class="row" style="text-align: center">
                </div>
                <div class="row" style="margin-bottom: 5px; text-align: center">
                    <asp:Label runat="server" ForeColor="White" ID="lblError" Visible="False"></asp:Label>
                </div>
                <div class="row" style="margin-bottom: 5px;">
                    <TSPControls:CustomTextBox runat="server"  AutoCompleteType="Disabled"
                        
                        ClientInstanceName="txtDocumentNo" NullText="کد ده رقمی سند را وارد نمایید" EnableClientSideAPI="True" ID="txtDocumentNo"
                        MaxLength="10">
                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Right" ErrorDisplayMode="ImageWithTooltip">
                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                            </ErrorImage>
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                            </ErrorFrameStyle>
                            <RequiredField IsRequired="True" ErrorText="کد ده رقمی وارد نشده است"></RequiredField>
                        </ValidationSettings>
                    </TSPControls:CustomTextBox>
                </div>
                <div class="row " align="center">
                  <%--  <TSPControls:CustomASPxCaptcha ID="Captcha" runat="server" TextBox-Visible="false"
                        Width="200px">
                        <ClientSideEvents BeginCallback="function(s,e){ txtSecurityCode.SetText(''); }" />
                    </TSPControls:CustomASPxCaptcha>--%>
                       <TSPControls:CustomASPxCaptcha ID="Captcha" CssClass="Logincaptcha" runat="server" Width="100%" ChallengeImage-BackgroundColor="White">
                                        <RefreshButtonStyle CssClass="LoginCaptchaRefreshtext"></RefreshButtonStyle>



                                    </TSPControls:CustomASPxCaptcha>
                </div>
                <div class="row">
                    <TSPControls:CustomAspxButton runat="server" Text="بررسی اصالت سند" Width="100%" ID="btnTraceDocument"
                        OnClick="btnTraceDocument_Click">
                        <Image Width="20px" Height="20px" Url="~/Images/viewmag.png" />
                        <ClientSideEvents Click="function(s,e){ e.processOnServer=ASPxClientEdit.ValidateGroup('TraceDocument'); }" />
                    </TSPControls:CustomAspxButton>
                </div>
            </div>
            <div class="container-fluid-login-Footer">
                <%--   <div class="row">
                            <div class="col-md-6">
                                <a id="ASPxHyperLink3" runat="server" href="LoginGuide.aspx" target="_blank" style="color: #fff !important; text-decoration: none;"><span class="glyphicon-user">راهنمای نام کاربری و ورود به سامانه</span>
                                </a>
                            </div>
                            <div class="col-md-6">
                                <a id="btnForgotPass" runat="server" href="~/PasswordForget.aspx" style="color: #fff !important; text-decoration: none;"><span class="glyphicon-lock">رمز عبور را فراموش کرده ام</span></a>
                            </div>
                        </div>--%>
            </div>
        </div>
    </div>
</asp:Content>
