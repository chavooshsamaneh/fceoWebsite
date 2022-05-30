<%@ Page Async="true"  Title="درخواست تمدید پروانه اشتغال-ثبت نهایی" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="WizardrevivalDocFinish.aspx.cs" Inherits="Members_Documents_WizardrevivalDocFinish" %>



<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../UserControl/EPaymentUserControl.ascx" TagName="EPaymentUserControl"
    TagPrefix="TspUserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div align="right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>

            <TSPControls:CustomAspxMenuHorizontal ID="MenuSteps" runat="server">

                <Items>
                    <dxm:MenuItem Text="مدارک لازم" Name="Oath">
                        <Image Width="15px" Height="15px" />
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Kardan" Text="استعلام ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="JobConfirm" Text="تاییدیه سابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Summary" Text="خلاصه اطلاعات">
                        <Image Width="15px" Height="15px" />
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="End" Text="ثبت نهایی" Selected="true">
                        <Image Width="15px" Height="15px" />
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>

            <TSPControls:CustomASPxRoundPanel ID="RoundPanelSummary" HeaderText="ثبت نهایی" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <div dir="rtl" align="right" style="line-height: 15pt">
                            <p class="HelpUL">
                                <b>توجه!</b>
                            </p>
                            <ol dir="rtl" align="right">
                                <li>جهت تمدید پروانه نیازی به مراجعه و حضور در سازمان نمی باشد.</li>
                                <li>نحوه پرداخت هزینه تمدید پروانه فقط به صورت الکترونیکی و از طریق درگاه الکترونیکی
                                                بانک می باشد. بعد از انتخاب دکمه تایید و پرداخت به درگاه بانک هدایت خواهید شد
                                </li>
                            </ol>
                        </div>
                        <br />
                        <p class="HelpUL">
                            قابل ذکر است جهت پیگیری روند درخواست تمدید پروانه خود از طریق همین سامانه و با استفاده از
                                        نام کاربری و رمز عبور خود اقدام نمایید.در صورت عدم پیگیری شما از این طریق و در صورت
                                        ناقص بودن اطلاعات کارشناسان سازمان هیچگونه مسئولیتی در قبال تائید به موقع پروانه
                                        اشتغال به کار شما و یا عدم تائید آن ندارند.
                        </p>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <br />
            <TspUserControl:EPaymentUserControl ID="EPaymentUC" runat="server" />
            <div class="Item-center">
                <TSPControls:CustomAspxButton CssClass="ButtonMenue" ID="btnPre" OnClick="btnPre_Click" runat="server" Text="بازگشت"
                    ToolTip="بازگشت" EnableViewState="False" EnableTheming="False" UseSubmitBehavior="False"
                    CausesValidation="False">
                </TSPControls:CustomAspxButton>

                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" UseSubmitBehavior="False" CausesValidation="False"
                    Text="انصراف" EnableTheming="False" ToolTip="انصراف"
                    ID="btnCancel" EnableViewState="False" OnClick="btnCancel_Click" Visible="true">
                </TSPControls:CustomAspxButton>

                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" UseSubmitBehavior="False" CausesValidation="False"
                    Text="ثبت نهایی و پرداخت الکترونیک" EnableTheming="False" ToolTip="ثبت نهایی و پرداخت الکترونیک"
                    ID="btnSave" EnableViewState="False" OnClick="btnSave_Click" Visible="true">
                </TSPControls:CustomAspxButton>

                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="چاپ خلاصه اطلاعات" Width="200px" ID="btnPrint"
                    EncodeHtml="false" AutoPostBack="False"
                    UseSubmitBehavior="false" CausesValidation="False" EnableViewState="False"
                    Visible="false">
                    <ClientSideEvents Click="function(s, e) {
  window.open(HiddenFieldDocMemberFile.Get('PrintdocMeFile'));
}"></ClientSideEvents>
                </TSPControls:CustomAspxButton>

                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="چاپ نسخه اولیه گواهینامه" Width="200px" ID="btnPrePrint"
                    EncodeHtml="false" AutoPostBack="False"
                    UseSubmitBehavior="false" CausesValidation="False" EnableViewState="False"
                    Visible="false">
                    <ClientSideEvents Click="function(s, e) {
  window.open(HiddenFieldDocMemberFile.Get('PrePrintdocMeFile'));
}"></ClientSideEvents>
                </TSPControls:CustomAspxButton>
                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مدیریت پروانه اشتغال " Width="200px" ID="btnDocMemberFile"
                    EncodeHtml="false" OnClick="btnDocMemberFile_Click"
                    UseSubmitBehavior="false" CausesValidation="False" EnableViewState="False"
                    Visible="false">
                </TSPControls:CustomAspxButton>
            </div>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldDocMemberFile" ClientInstanceName="HiddenFieldDocMemberFile">
            </dxhf:ASPxHiddenField>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
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
