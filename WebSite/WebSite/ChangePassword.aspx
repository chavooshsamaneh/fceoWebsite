<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Members_ChengeUserName" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]
    </div>
    <div>
        <div class="container-fluid-login LoginPage">
            <div class="container-fluid-login-Header">
                <h4>تغییر رمز عبور</h4>
            </div>
            <div class="container-fluid-login-Body">
                <div class="row" style="text-align: center">
                </div>
                <div class="row" style="margin-bottom: 5px; text-align: center">
                    <asp:Label runat="server" Style="color: #fff !important;" ID="lblError" Visible="False"></asp:Label>
                </div>
                <div class="row" style="margin-bottom: 5px;">
                    <TSPControls:CustomTextBox runat="server" Width="100%" ClientInstanceName="UserName" EnableClientSideAPI="True" ID="UserName" Enabled="False"
                        MaxLength="50" NullText="نام کاربری" RightToLeft="True">
                        <ValidationSettings Display="Dynamic" ErrorFrameStyle-Font-Size="16px">

                            <RequiredField IsRequired="True" ErrorText="نام کاربری وارد نشده است"></RequiredField>
                        </ValidationSettings>
                    </TSPControls:CustomTextBox>

                </div>
                <div class="row" style="margin-bottom: 5px;">

                    <TSPControls:CustomTextBox runat="server" Width="100%" AutoCompleteType="Disabled"
                        Password="True"
                        ClientInstanceName="OldPassword" EnableClientSideAPI="True" ID="OldPassword" NullText="رمز عبور قدیمی">
                        <ValidationSettings Display="Dynamic" ErrorFrameStyle-Font-Size="16px">


                            <RequiredField IsRequired="True" ErrorText="رمز عبور قدیمی وارد نشده است"></RequiredField>
                        </ValidationSettings>
                    </TSPControls:CustomTextBox>
                </div>
                <div class="row">

                    <TSPControls:CustomTextBox runat="server" Width="100%" AutoCompleteType="Disabled"
                        Password="True"
                        ClientInstanceName="Password" EnableClientSideAPI="True" ID="Password" NullText="رمز عبور جدید">
                        <ValidationSettings Display="Dynamic" ErrorFrameStyle-Font-Size="16px">


                            <RequiredField IsRequired="True" ErrorText="رمز عبور جدید وارد نشده است"></RequiredField>
                            <RegularExpression ValidationExpression="(\w+|\W+)(\w+|\W+)(\w+|\W+)(\w+|\W+)(\w+|\W+)(\w+|\W+)" ErrorText="طول رمزعبور باید حداقل 6 کاراکتر باشد" />
                        </ValidationSettings>
                    </TSPControls:CustomTextBox>
                </div>
                <div class="row">
                    <TSPControls:CustomTextBox runat="server" Width="100%" AutoCompleteType="Disabled"
                        Password="True"
                        ClientInstanceName="ConfirmPassword" EnableClientSideAPI="True" ID="ConfirmPassword" NullText="تأئید رمز عبور جدید">
                        <ValidationSettings Display="Dynamic" ErrorFrameStyle-Font-Size="16px">


                            <RequiredField IsRequired="True" ErrorText="تأئید رمز عبور جدید وارد نشده است"></RequiredField>
                            <RegularExpression ValidationExpression="(\w+|\W+)(\w+|\W+)(\w+|\W+)(\w+|\W+)(\w+|\W+)(\w+|\W+)" ErrorText="طول رمزعبور باید حداقل 6 کاراکتر باشد" />
                        </ValidationSettings>
                    </TSPControls:CustomTextBox>
                </div>
                <div style="text-align: center">
                    <br />
                    <asp:CompareValidator ID="PasswordCompare" runat="server" Width="183px" ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="رمز عبور و تأئید آن باید یکسان باشد." ControlToCompare="Password" ValidationGroup="Wizard1"></asp:CompareValidator>&nbsp;<br />
                    <br />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Width="237px" Height="20px" HeaderText="لطفاً فیلدهای زیر را تکمیل نمائید."></asp:ValidationSummary>
                </div>               
                <div class="Item-center">
                    <TSPControls:CustomAspxButton ID="btnSave" OnClick="btnOfUNRefresh_Click" runat="server" Text="ذخیره" CausesValidation="False" Width="55px">
                    </TSPControls:CustomAspxButton>
                    <TSPControls:CustomAspxButton  ID="btnOfUNRefresh" OnClick="btnOfUNRefresh_Click" runat="server" Text="پاک کردن فرم" CausesValidation="False" Wrap="False">
                    </TSPControls:CustomAspxButton>
                    <TSPControls:CustomAspxButton ID="BtnBack" OnClick="BtnBack_Click" runat="server" Text="بازگشت" CausesValidation="False" Width="55px">
                    </TSPControls:CustomAspxButton>
                </div>
            </div>
            <div class="container-fluid-login-Footer">
                <div class="row">
                    <%-- <div class="col-md-6">
                                <a id="ASPxHyperLink3" runat="server" href="LoginGuide.aspx" target="_blank" style="color: #fff !important; text-decoration: none;"><span class="glyphicon-user">راهنمای نام کاربری و ورود به سامانه</span>
                                </a>
                            </div>
                            <div class="col-md-6">
                                <a id="btnForgotPass" runat="server" href="~/PasswordForget.aspx" style="color: #fff !important; text-decoration: none;"><span class="glyphicon-lock">رمز عبور را فراموش کرده ام</span></a>
                            </div>--%>
                </div>
            </div>
        </div>
    </div>

    <%--  <div id="Content" runat="server" style="text-align: center;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
               
                <table style="width: 409px; height: 226px" class="TableBorder" border="0">
                    <tbody>
                        <tr>
                            <td style="vertical-align: top" class="TableTitle" colspan="2"></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 212px; text-align: right" align="right">&nbsp;<asp:Label ID="Label49" runat="server" Text="نام کاربری"></asp:Label>
                            </td>
                            <td style="vertical-align: top; width: 212px; text-align: right">
                             
                                &nbsp; </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 212px; text-align: right" align="right">
                                <asp:Label ID="Label47" runat="server" Text="رمز عبور قدیمی"></asp:Label></td>
                            <td style="vertical-align: top; width: 212px; text-align: right">
                            <asp:TextBox ID="OldPassword" runat="server" Width="180px" MaxLength="40" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="OldPassword" Display="Dynamic" ErrorMessage="کلمه رمز را وارد کنید." ToolTip="کلمه رمز را وارد کنید.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 212px; text-align: right" align="right">
                                <asp:Label ID="Label1" runat="server" Text="رمز عبور جدید"></asp:Label></td>
                            <td style="vertical-align: top; width: 212px; text-align: right">
                             <asp:TextBox ID="Password" runat="server" Width="180px" MaxLength="40" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" Display="Dynamic" ErrorMessage="کلمه رمز را وارد کنید." ToolTip="کلمه رمز را وارد کنید.">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Width="290px" ControlToValidate="Password" Display="Dynamic" ValidationExpression="(\w+|\W+)(\w+|\W+)(\w+|\W+)(\w+|\W+)(\w+|\W+)(\w+|\W+)">طول رمزعبور باید حداقل 6 کاراکتر باشد.</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 212px; text-align: right" align="right">
                                <asp:Label ID="Label48" runat="server" Width="111px" Text="تأئید رمز عبور جدید"></asp:Label>
                            </td>
                            <td style="vertical-align: top; width: 212px; text-align: right">
                              <asp:TextBox ID="ConfirmPassword" runat="server" Width="180px" MaxLength="40" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="تأئید کلمه رمز را وارد کنید." ToolTip="تأئید کلمه رمز را وارد کنید.">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Width="286px" ControlToValidate="ConfirmPassword" Display="Dynamic" ValidationExpression="(\w+|\W+)(\w+|\W+)(\w+|\W+)(\w+|\W+)(\w+|\W+)(\w+|\W+)">طول رمزعبور باید حداقل 6 کاراکتر باشد.</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top" align="center" colspan="2">
                              
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top" align="center" colspan="2">&nbsp;<table style="width: 122px">
                                <tbody>
                                    <tr>
                                        <td style="width: 100px">
                                           </td>
                                        <td style="width: 100px">
                                            </td>
                                        <td style="width: 100px">
                                           </td>
                                    </tr>
                                </tbody>
                            </table>
                                &nbsp; </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
            AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                            <img src="Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>

    </div>--%>
</asp:Content>

