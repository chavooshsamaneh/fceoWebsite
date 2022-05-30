<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="WizardOfficeFinish.aspx.cs" Inherits="NezamRegister_WizardOfficeFinish"
    Title="عضویت حقوقی - ثبت نهایی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
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
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="false">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server">
                <Items>
                    <dxm:MenuItem Text="چهارچوب شئون حرفه ای" Name="Membership">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Office" Text="مشخصات شرکت">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Agent" Text="شعبه ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Member" Text="اعضای شرکت">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Letter" Text="آگهی های رسمی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Summary" Text="خلاصه اطلاعات">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="End" Text="ثبت نهایی" Selected="true">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>

            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="ثبت عضویت حقوقی" runat="server">


                <HeaderTemplate>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 20%; height: 28px" valign="middle">
                                <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="ثبت عضویت حقوقی">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="width: 80%; height: 28px" valign="middle">
                                <TSPControls:CustomAspxButton IsMenuButton="true" AutoPostBack="false" ID="btnHelp" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False"
                                    Text=" " ToolTip="راهنما" UseSubmitBehavior="False">
                                    <image height="25px" url="~/Images/Help.png" width="25px">
                                                            </image>
                                    <ClientSideEvents Click="function(s,e){ ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <PanelCollection>
                    <dxp:PanelContent>

                        <dxe:ASPxPanel runat="server" Visible="False" ID="tblUser">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <div class="row">
                                        <div class="col-md-3">نام کاربری :</div>
                                        <div class="col-md-3">
                                            <dxe:ASPxLabel runat="server" ID="ASUserName">
                                            </dxe:ASPxLabel>
                                        </div>
                                        <div class="col-md-3">رمز عبور :</div>
                                        <div class="col-md-3">
                                            <dxe:ASPxLabel runat="server" ID="ASPassword">
                                            </dxe:ASPxLabel>
                                        </div>
                                    </div>
                                       <div class="row">
                                <div class="col-md-3">پست الکترونیکی :</div>
                                <div class="col-md-3">    <dxe:ASPxLabel runat="server" ID="ASEmailUser">
                                    </dxe:ASPxLabel></div>
                                <div class="col-md-3">کد رهگیری :</div>
                                <div class="col-md-3">      <dxe:ASPxLabel runat="server" ID="lblFollowCode">
                                    </dxe:ASPxLabel></div>
                            </div>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dxe:ASPxPanel>
                        <br />
                        <div class="row">
                        <p align="justify" dir="rtl" style="line-height: 15pt">
                            لطفا ظرف مدت 10 روز از این تاریخ با در دست داشتن <b>مدارک ذیل و کد رهگیری</b> به
                                                    واحد عضویت نظام مهندسی ساختمان استان فارس واقع در ساختمان مرکزی به نشانی شیراز بلوار
                                                    ستارخان انتهای کوچه نمازی مراجعه نمایید.
                                                    <br>
                            در غیر اینصورت باید تمامی مراحل ثبت عضویت را از ابتدا انجام دهید.
                                                    <br />
                            <br />
                        </p>
                        <div dir="rtl" align="right" style="line-height: 15pt">
                            <b>مدارک مورد نیاز :</b>
                            <ol dir="rtl" align="right">
                                <li>اساسنامه شرکت</li>
                                <li>اظهار نامه شرکت</li>
                                <li>روزنامه رسمی شرکت حاوی آگهی تاسیس</li>
                                <li>روزنامه های رسمی شرکت حاوی تغییرات مهم</li>
                                <li>آخرین روزنامه رسمی شرکت</li>
                                <li>آخرین صورت جلسه مربوط به انتخاب مدیران و دارندگان حق امضا</li>
                                <li>
                                    <asp:Label Font-Bold="true" ForeColor="DarkBlue" runat="server" ID="lblMemberShipCost"></asp:Label></li>
                                <li>
                                    <asp:Label Font-Bold="true" ForeColor="DarkBlue" runat="server" ID="lblYearMemberShipCost"></asp:Label></li>
                                <%--<li>فیش بانکی مربوط به پرداخت ورودیه به مبلغ پانصد هزار ریال به حساب جاری  10009/57840 بانک تجارت شعبه نظام مهندسی</li>
                                                    <li>فیش بانکی مربوط به پرداخت حق عضویت سالانه به مبلغ پانصد هزار ریال به حساب جاری 10009/57840 بانک تجارت شعبه نظام مهندسی</li>--%>
                                <li>دو قطعه عکس مدیر عامل شرکت</li>
                                <li>شناسنامه مدیران</li>
                                <li>پروانه های اشتغال مدیر عامل, اعضای هیئت مدیره و شاغلین تمام وقت (مهندسین, کاردانها
                                                            و معماران تجربی) </li>
                                <li>نامه درخواست ثبت عضویت در سازمان نظام مهندسی بر روی سربرگ شرکت با مهر و امضای مدیر عامل مطابق متنی که در صفحه چارچوب شئون حرفه ای آمده است</li>
                            </ol>
                            <b>*توجه: بایستی اصل کلیه مدارک ذکر شده را به همراه داشته باشید. </b>
                        </div>
                        <p class="HelpUL">
                            قابل ذکر است جهت پیگیری روند ثبت نام خود از طریق همین سامانه و با استفاده از نام
                                                    کاربری و رمز عبور خود اقدام نمایید.در صورت عدم پیگیری شما از این طریق و در صورت
                                                    ناقص بودن اطلاعات کارمندان سازمان هیچگونه مسئولیتی در قبال تائید به موقع عضویت شما
                                                    و یا عدم تائید عضویت شما ندارند.
                        </p>
                       </div>
                        <br />
                        <div class="Item-center">
                                        <TSPControls:CustomAspxButton  CssClass="ButtonMenue" runat="server" Text="&nbsp;&nbsp;&nbsp;بازگشت&nbsp;&nbsp;&nbsp;" ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnPre" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnPre_Click">
                                        </TSPControls:CustomAspxButton>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="&nbsp;&nbsp;&nbsp;انصراف&nbsp;&nbsp;&nbsp;" ToolTip="انصراف"
                                            CausesValidation="False" ID="btnCancel" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnCancel_Click">
                                        </TSPControls:CustomAspxButton>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" UseSubmitBehavior="False" CausesValidation="False"
                                           Text="&nbsp;&nbsp;&nbsp;ذخیره&nbsp;&nbsp;&nbsp;" EnableTheming="False" ToolTip="ذخیره"
                                            ID="btnSave" EnableViewState="False" OnClick="btnSave_Click" AutoPostBack="true">
                            
                                        </TSPControls:CustomAspxButton>
                                      
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="&nbsp;&nbsp;&nbsp;چاپ" Width="150px" ID="btnPrint"
                                            EncodeHtml="false" AutoPostBack="False"
                                            UseSubmitBehavior="false" CausesValidation="False" EnableViewState="False"
                                            Visible="false">
                                            <ClientSideEvents Click="function(s, e) {
  window.open(HiddenPrintDetial.Get('PrintUserData'));
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="&nbsp;&nbsp;&nbsp;چاپ اطلاعات کاربری" Width="170px"
                                            ID="btnPrintUserInfo" EncodeHtml="false" AutoPostBack="False"
                                            UseSubmitBehavior="false" CausesValidation="False" EnableViewState="False"
                                            Visible="false">
                                            <ClientSideEvents Click="function(s, e) {
  window.open(HiddenPrintDetial.Get('PrintUserInfo'));
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="&nbsp;&nbsp;&nbsp;ورود به پرتال" Width="150px"
                                            ID="btnContinue" EncodeHtml="false" OnClick="btnContinue_Click"
                                            UseSubmitBehavior="false" CausesValidation="False" EnableViewState="False"
                                            Visible="false">                                            
                                        </TSPControls:CustomAspxButton>
                               
                              </div>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <dx:ASPxHiddenField ID="HiddenPrintDetial" runat="server" ClientInstanceName="HiddenPrintDetial">
            </dx:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img align="middle" src="../Image/indicator.gif" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
    <asp:HiddenField ID="HDOfficeId" runat="server" Visible="False" />
    <input id="merchantId" name="merchantId" type="hidden" value="<%= TSP.Utility.OnlinePayment.GetNezamMerchantId() %>" />
    <input id="paymentId" name="paymentId" type="hidden" value="<%=  Session["OfPaymentId"]%>" />
    <input id="amount" name="amount" type="hidden" value="<%=(Session["OfPrice"]!=null)?Convert.ToInt64(Decimal.Parse(Session["OfPrice"].ToString())):-1%>" />
    <input id="revertURL" name="revertURL" type="hidden" value="<%= this.Request.Url.AbsoluteUri %>" />
    <input id="customerId" name="customerId" type="hidden" />
    <dx:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
    </dx:ASPxHiddenField>

</asp:Content>
