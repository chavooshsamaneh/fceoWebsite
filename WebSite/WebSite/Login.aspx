<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" MasterPageFile="~/MasterPageWebsite.master"
    Inherits="login" Title="ورود به سامانه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">

            <div>
                <div class="container-fluid-login LoginPage">
                    <div class="container-fluid-login-Header">
                        <span>ورود کاربر</span>
                    </div>
                    <div class="container-fluid-login-Body">
                        <div class="row" style="text-align: center">
                        </div>
                        <div class="row" style="margin-bottom: 5px; text-align: center">
                            <asp:Label runat="server" Style="color: #fff !important;" ID="lblError" Visible="False"></asp:Label>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <TSPControls:CustomTextBox runat="server" Width="100%" ClientInstanceName="txtUsername" EnableClientSideAPI="True" ID="txtUsername"
                                MaxLength="50" NullText="نام کاربری" RightToLeft="True">
                                <ValidationSettings Display="Dynamic" ErrorFrameStyle-Font-Size="16px">

                                    <RequiredField IsRequired="True" ErrorText="نام کاربری وارد نشده است"></RequiredField>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">

                            <TSPControls:CustomTextBox runat="server" Width="100%" AutoCompleteType="Disabled"
                                Password="True"
                                ClientInstanceName="txtPass" EnableClientSideAPI="True" ID="txtPass" NullText="رمز عبور">
                                <ValidationSettings Display="Dynamic" ErrorFrameStyle-Font-Size="16px">


                                    <RequiredField IsRequired="True" ErrorText="رمز عبور وارد نشده است"></RequiredField>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </div>
                        <div class="row">
                            <div class="col-md-8" style="padding: 0;">
                                <TSPControls:CustomTextBox runat="server" Width="100%" AutoCompleteType="Disabled"
                                    Password="True"
                                    ClientInstanceName="txtTempPass" EnableClientSideAPI="True" ID="txtTempPass" NullText="رمز یکبار">
                                    <ValidationSettings Display="Dynamic">

                                        <RequiredField IsRequired="False" ErrorText="رمز یکبار عبور وارد نشده است"></RequiredField>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                            <div class="col-md-4" style="padding: 2px 2px 0px 0px;">
                                <TSPControls:CustomAspxButton runat="server" Text="ارسال پیامک" Wrap="False" ID="btnTempPass"
                                    EncodeHtml="false" UseSubmitBehavior="False" OnClick="btnTempPass_Click" Width="100%">
                                </TSPControls:CustomAspxButton>
                            </div>

                        </div>
                        <div style="text-align: center">
                            <span style="color: #fff !important; text-decoration: none; font-size: 12px;">*رمز یکبار عبور برای اعضا الزامی نمی باشد</span>
                        </div>
                        <div class="row ">
                            <asp:Panel ID="panelSecurityCode" runat="server">

                                <asp:Panel ID="Panel1" runat="server" align="center">
                                    <TSPControls:CustomASPxCaptcha ID="Captcha" CssClass="Logincaptcha" runat="server" Width="100%" ChallengeImage-BackgroundColor="White">
                                        <RefreshButtonStyle CssClass="LoginCaptchaRefreshtext"></RefreshButtonStyle>



                                    </TSPControls:CustomASPxCaptcha>
                                </asp:Panel>

                            </asp:Panel>
                        </div>
                        <div class="row">
                            <TSPControls:CustomAspxButton ID="btnLogin" runat="server" EncodeHtml="False" OnClick="btnLogin_Click" Text="ورود به سامانه" Width="100%" UseSubmitBehavior="true">
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
                                <a id="btnForgotPass" runat="server" href="~/PasswordForget.aspx" style="color: #fff !important; text-decoration: none;"><span class="glyphicon-lock">رمز عبور را فراموش کرده ام</span></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
       
    <script type="text/javascript">

        // # of images
        var imgCount = 42;
        // image directory
        var dir = 'images/Login/';
        // random the images
        var randomCount = Math.round(Math.random() * (imgCount - 1)) + 1;
        // array of images & file name

        var images = new Array
        images[1] = "1.jpg",
        images[2] = "2.jpg",
        images[3] = "3.jpg",
        images[4] = "4.jpg";
        images[5] = "5.jpg";
        images[6] = "6.jpg";
        images[7] = "7.jpg";
        images[8] = "8.jpg";
        images[9] = "9.jpg";
        images[10] = "10.jpg";
        images[11] = "11.jpg";
        images[12] = "12.jpg";
        images[13] = "13.jpg";
        images[14] = "14.jpg";
        images[15] = "15.jpg";
        images[16] = "16.jpg";
        images[17] = "17.jpg";
        images[18] = "18.jpg";
        images[19] = "19.jpg";
        images[20] = "20.jpg";
        images[21] = "21.jpg";
        images[22] = "22.jpg";
        images[23] = "23.jpg";
        images[24] = "24.jpg";
        images[25] = "25.jpg";
        images[26] = "26.jpg";
        images[27] = "27.jpg";
        images[28] = "28.jpg";
        images[29] = "29.jpg";
        images[30] = "30.jpg";
        images[31] = "31.jpg";
        images[32] = "32.jpg";
        images[33] = "33.jpg";
        images[34] = "34.jpg";
        images[35] = "35.jpg";
        images[36] = "36.jpg";
        images[37] = "37.jpg";
        images[38] = "38.jpg";
        images[39] = "39.jpg";
        images[40] = "40.jpg";
        images[41] = "41.jpg";
        images[42] = "42.jpg";

        document.body.style.cssText = "background:url(" + dir + images[randomCount] + ") no-repeat center center fixed;-webkit-background-size: cover;-moz-background-size: cover;-o-background-size: cover;background-size: cover;";
       

    </script>
</asp:Content>
