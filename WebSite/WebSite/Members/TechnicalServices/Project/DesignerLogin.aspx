<%@ Page Title="ثبت کار طراحی جدید" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="DesignerLogin.aspx.cs" Inherits="Members_TechnicalServices_Project_DesignerLogin" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <div>
        <div class="container-fluid-login LoginPage">
            <div class="container-fluid-login-Header">
                <span>ورود طراح</span>
            </div>
            <div class="container-fluid-login-Body">
                <div class="row" style="text-align: center">
                </div>
                <div class="row" style="margin-bottom: 5px; text-align: center">
                    <asp:Label runat="server" Style="color: #fff !important;" ID="lblError" Visible="False"></asp:Label>
                </div>
                <div class="row" style="margin-bottom: 5px;">
                    <TSPControls:CustomTextBox runat="server" Width="100%" ClientInstanceName="txtProjectId" EnableClientSideAPI="True" ID="txtProjectId"
                        MaxLength="10" NullText="کد پروژه" RightToLeft="True">
                        <ValidationSettings Display="Dynamic">
                            <RegularExpression ErrorText="کد پروژه را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                            <RequiredField IsRequired="True" ErrorText="کد پروژه وارد نشده است"></RequiredField>
                        </ValidationSettings>
                    </TSPControls:CustomTextBox>
                </div>
                <div class="row" style="margin-bottom: 5px;">

                    <TSPControls:CustomTextBox runat="server" Width="100%" AutoCompleteType="Disabled"
                        Password="True"
                        ClientInstanceName="txtDesingerCode" EnableClientSideAPI="True" ID="txtDesingerCode" NullText="شناسه طراح" MaxLength="10">
                        <ValidationSettings Display="Dynamic">
                            <RegularExpression ErrorText="شناسه طراح را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                            <RequiredField IsRequired="True" ErrorText="شناسه طراح وارد نشده است"></RequiredField>
                        </ValidationSettings>
                    </TSPControls:CustomTextBox>
                </div>
                <div class="row" style="margin-bottom: 5px;">
                    <fieldset>
                        <legend style="color: white; font-size: 12px; font-weight: bold">تعهد نامه</legend>
                        <p style="color: white; font-weight: 600; font-size: 12px; line-height: 22px; text-align: justify">بدینوسیله متعهد می گردم؛ مطابق با ضوابط تعیین شده در کنترل الکترونیکی خدمات مهندسی، نسبت به ثبت زیربنای پروژه‌ای که مسئولیت طراحی آن را به عهده داشته‌ام به صورت دقیق با عنوان کسر ظرفیت طراحی اقدام نمایم. همچنین در صورت هرگونه تغییر در زیر بنای پروژه در هر مقطع زمانی، موظف به اطلاع رسانی به سازمان و اصلاح در پرتال خود در سایت سازمان هستم.در صورت کشف هرگونه مغایرت، مسئولیت تمام عواقب قانونی بر عهده اینجانب بوده و سازمان مجاز به برخورد برابر ضوابط و مقررات می باشد.</p>
                        <TSPControls:CustomASPxCheckBox runat="server" ClientInstanceName="checkboxAgeement" ID="checkboxAgeement" ForeColor="White" Text="تعهد نامه به صورت کامل مطالعه شد و نسبت به آن متعهد می باشم">
                            <ValidationSettings Display="Dynamic" ErrorFrameStyle-ForeColor="Red">

                                <RequiredField IsRequired="True" ErrorText="گزینه مطالعه تعهد نامه را انتخاب نمایید"></RequiredField>
                            </ValidationSettings>
                        </TSPControls:CustomASPxCheckBox>
                    </fieldset>
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
                    <TSPControls:CustomAspxButton ID="btnLogin" runat="server" EncodeHtml="False" OnClick="btnLogin_Click" Text="اعتبار سنجی" Width="100%" UseSubmitBehavior="true">
                    </TSPControls:CustomAspxButton>
                </div>
            </div>
            <div class="container-fluid-login-Footer">
                <div class="row">
                    <div class="col-md-6">
                    </div>
                    <div class="col-md-6">
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

