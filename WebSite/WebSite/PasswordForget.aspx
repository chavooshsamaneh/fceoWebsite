<%@ Page Language="C#"  Async="true"  MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="PasswordForget.aspx.cs" Inherits="PasswordForget" Title="فراموش کردن رمز عبور" %>

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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <dxe:ASPxPanel ID="PanelForgetPass" runat="server">
                <PanelCollection>
                    <dxe:PanelContent>
                        <div class="container-fluid-login LoginPage">
                            <div class="container-fluid-login-Header">
                                <h4>فراموش کردن رمز عبور</h4>
                            </div>
                            <div class="container-fluid-login-Body">
                                <div class="row">
                                    <asp:Label runat="server" ForeColor="Red" ID="lblError" Visible="False"></asp:Label>
                                </div>
                                <div class="row forgetPass fPassMessage" id="lblMessage" runat="server" visible="False">
                                </div>

                                <div class="row" style="margin-bottom: 5px;">
                                    <TSPControls:CustomTextBox ID="txtUserName" runat="server" NullText="نام کاربری"
                                        MaxLength="50" AutoCompleteType="Disabled">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Right" ErrorDisplayMode="ImageWithTooltip">

                                            <RequiredField ErrorText="نام کاربری را وارد نمایید" IsRequired="True" />

                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </div>
                                <div class="row forgetPass" id="QuestionPhone" runat="server" visible="false">
                                    <div class="row forgetPass" id="QuestionPhone1" runat="server" visible="true">
                                        آیا شما با نام کاربری  <span id="lblUserName" runat="server"></span>به شماره همراه زیر دسترسی دارید؟

                                    </div>
                                    <div class="row forgetPass" id="lblMobileNo" runat="server" visible="true">
                                        0917 ***** 60
                                    </div>
                                    <div class="row forgetPass" id="QuestionPhone2" runat="server" visible="true">
                                        اگر شما به شماره همراه با ساختار فوق دسترسی دارید؛ می توانیم برای شما کلید تایید را با پیام کوتاه ارسال کنیم
                                    </div>
                                </div>
                                <div class="row forgetPass" id="QuestionEmail" runat="server" visible="false">
                                    همچنین ما یک پست الکترونیکی با ساختار زیر از شما داریم که می توانیم لینک بازیابی رمز عبور را به آن ارسال کنیم
                                       <div class="row forgetPass" id="lblEmail" runat="server" visible="true">
                                           ni********@yahoo.com
                                       </div>
                                </div>
                                <div class="row forgetPass" id="lblValidationKey" runat="server" visible="false">
                                    بررسی یکتایی مالکیت تلفن همراه و کد کاربری
                                     <div class="row forgetPass valKey" runat="server">
                                         کلیدی را که به شماره تلفن زیر ارسال کردیم، وارد کنید
                                     </div>
                                    <div class="row forgetPass valKey" dir="ltr" id="lblMobileNo2" runat="server">
                                        0917 ***** 60
                                    </div>
                               
                                </div>

                                     <TSPControls:CustomTextBox ID="txtValidationKey" NullTextStyle-CssClass="txtValKey" runat="server" Visible="false" NullText="- - - - - - -"
                                        MaxLength="6" AutoCompleteType="Disabled">

                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Right" ErrorDisplayMode="ImageWithTooltip">
                                           
                                            <RequiredField IsRequired="True" ErrorText="E-mail is required" />
                                            <RequiredField ErrorText="کلید ارسال شده را وارد نمایید" IsRequired="True" />

                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                    <div class="row forgetPass valKey" id="lblMobileNo3" runat="server" visible="false">
                                        ممکن است مقداری طول بکشد تا کلید ارسال شده به دست شما برسد لطفا صبر کنید
                                    </div>

                                <div class="row">

                                    <TSPControls:CustomASPxCaptcha ID="Captcha" runat="server" CssClass="Logincaptcha" ChallengeImage-BackgroundColor="White">
                                        <RefreshButtonStyle CssClass="LoginCaptchaRefreshtext"></RefreshButtonStyle>
                                    </TSPControls:CustomASPxCaptcha>

                                </div>
                                <div class="row">
                                    <TSPControls:CustomAspxButton ID="btnNext" runat="server"
                                        Width="100%" Text="&nbsp;&nbsp;بعدی" OnClick="btnNext_Click"
                                        UseSubmitBehavior="true">
                                    </TSPControls:CustomAspxButton>
                                </div>
                                <div class="row">
                                    <TSPControls:CustomAspxButton ID="btnVerifyValKey" runat="server" Visible="false"
                                        Width="100%" Text="&nbsp;&nbsp;اعتبار سنجی" OnClick="btnVerifyValKey_Click"
                                        UseSubmitBehavior="true">
                                    </TSPControls:CustomAspxButton>
                                </div>
                                <div class="row btnMobileStyle">
                                    <TSPControls:CustomAspxButton ID="btnTextMessage" runat="server" Visible="false"
                                        Width="100%" Wrap="True" Text="کلید را با پیام کوتاه ارسال کنید" OnClick="btnSendSMS_Click"
                                        UseSubmitBehavior="true">
                                    </TSPControls:CustomAspxButton>
                                </div>
                                <div class="row btnEmailStyle">
                                    <TSPControls:CustomAspxButton ID="btnSendEmail" runat="server" Visible="false"
                                        Width="100%" Wrap="True" Text="لینک بازیابی را به ایمیل ام ارسال کنید" OnClick="btnSendEmail_Click"
                                        UseSubmitBehavior="true">
                                    </TSPControls:CustomAspxButton>
                                </div>

                            </div>
                        </div>
                    </dxe:PanelContent>
                </PanelCollection>
            </dxe:ASPxPanel>

            <dxhf:ASPxHiddenField ID="HiddenFieldInfo" ClientInstanceName="HiddenFieldInfo" runat="server">
            </dxhf:ASPxHiddenField>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div style="font-family: Tahoma; font-size: 9pt; text-align: center; padding-top: 25px; width: 300px; height: 41px; background-image: url(Images/UploadBg.png);">
                <img id="IMG1" src="Images/indicator.gif" align="middle" />
                لطفا صبر نمایید ...
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
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
