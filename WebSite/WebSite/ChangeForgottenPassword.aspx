<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="ChangeForgottenPassword.aspx.cs" Inherits="ChangeForgottenPassword"
    Title="تغییر رمز عبور" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div runat="server" id="PanelMessage" visible="false">
        <div class="container-fluid-login LoginPage">
            <div class="container-fluid-login-Header">
                <h4>تغییر رمز عبور</h4>
            </div>
            <div class="container-fluid-login-Body">
                <div class="Item-center">
                  <h4>     <span id="lblMessage" runat="server"  style="color:white;">
                    </span></h4>
                </div>
            </div>
            <div class="container-fluid-login-Footer">
                <div class="row">
                     <div class="col-md-6">
                        <a id="A1" runat="server" href="LoginGuide.aspx" target="_blank" style="color: #fff !important; text-decoration: none;"><span class="glyphicon-user">راهنمای نام کاربری و ورود به سامانه</span>
                        </a>
                    </div>
                    <div class="col-md-6">
                        <a id="A2" runat="server" href="/Login.aspx" style="color: #fff !important; text-decoration: none;"><span class="glyphicon-lock">ورود به سیستم</span></a>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div runat="server" id="PanelChangePassword">
        <div class="container-fluid-login LoginPage">
            <div class="container-fluid-login-Header">
                <span>تغییر رمز عبور</span>
            </div>
            <div class="container-fluid-login-Body">
                <div class="row" style="text-align: center">
                    <asp:Label runat="server" ForeColor="Red" ID="lblError" Visible="False"></asp:Label>
                </div>
                <div class="row" style="margin-bottom: 5px; text-align: center">
                    <asp:Label runat="server" Style="color: #fff !important;" ID="Label1" Visible="False"></asp:Label>
                </div>
                <div class="row" style="margin-bottom: 5px;">
                    <TSPControls:CustomTextBox runat="server" Width="100%" ClientInstanceName="txtUserName" EnableClientSideAPI="True" ID="txtUserName" Enabled="False"
                        MaxLength="50" NullText="نام کاربری" RightToLeft="True">
                        <ValidationSettings Display="Dynamic">

                            <RequiredField IsRequired="True" ErrorText="نام کاربری وارد نشده است"></RequiredField>
                        </ValidationSettings>
                    </TSPControls:CustomTextBox>

                </div>
                <div class="row">

                    <TSPControls:CustomTextBox IsMenuButton="true" ID="txtPassword" runat="server" ClientInstanceName="txtPassword1" NullText="رمز عبور"
                        Password="True">
                        <ValidationSettings Display="Dynamic">

                            <RequiredField ErrorText="رمز عبور را وارد نمایید" IsRequired="True" />
                            <RegularExpression ErrorText="رمز عبور باید بین 6 تا 15 رقم باشد"  ValidationExpression="\w{6,15}" />

                        </ValidationSettings>
                    </TSPControls:CustomTextBox>
                </div>
                <div class="row">

                    <TSPControls:CustomTextBox IsMenuButton="true" ID="txtPassword2" runat="server" ClientInstanceName="txtPassword2" NullText="تکرار رمز عبور"
                        Password="True">
                        <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="تکرار رمز عبور را اشتباه وارد کرده اید"
                            ErrorTextPosition="Bottom">

                            <RequiredField ErrorText="تکرار رمز عبور را وارد نمایید" IsRequired="True" />
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px" />
                            </ErrorFrameStyle>
                        </ValidationSettings>
                        <ClientSideEvents Validation="function(s, e) {
	if(txtPassword1.GetText()!=txtPassword2.GetText())
		{
			e.isValid =false;
			txtPassword2.SetErrorText(&quot;تکرار کلمه عبور را اشتباه وارد کرده اید&quot;);
		}
}" />
                    </TSPControls:CustomTextBox>
                </div>
                <div class="row">

                    <asp:Panel ID="panelSecurityCode" runat="server">

                        <asp:Panel ID="Panel1" runat="server" align="center">
                            <TSPControls:CustomASPxCaptcha ID="Captcha" runat="server" CssClass="Logincaptcha" ChallengeImage-BackgroundColor="White" Width="100%">
                                <RefreshButtonStyle CssClass="LoginCaptchaRefreshtext"></RefreshButtonStyle>
                            </TSPControls:CustomASPxCaptcha>
                        </asp:Panel>

                    </asp:Panel>
                </div>
                <div class="row">
                    <TSPControls:CustomAspxButton ID="btnSave"  EncodeHtml="False"  runat="server" AutoPostBack="true"
                        OnClick="btnSave_Click" Text="ذخیره"  CausesValidation="true" Width="100%">
                    </TSPControls:CustomAspxButton>
                </div>
            </div>
            <div class="container-fluid-login-Footer">
                <div class="row">
                    <div class="col-md-6">
                        <a id="ASPxHyperLink3" runat="server" href="LoginGuide.aspx" target="_blank" style="color: #fff !important; text-decoration: none;"><span class="glyphicon-user">راهنمای نام کاربری و ورود به سامانه</span>
                        </a>
                    </div>
                    <div class="col-md-6">
                        <a id="btnForgotPass" runat="server" href="/Login.aspx" style="color: #fff !important; text-decoration: none;"><span class="glyphicon-lock">ورود به سیستم</span></a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
