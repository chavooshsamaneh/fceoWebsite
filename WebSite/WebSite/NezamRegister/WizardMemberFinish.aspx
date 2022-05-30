<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="WizardMemberFinish.aspx.cs" Inherits="NezamRegister_WizardMemberFinish"
    Title="عضویت حقیقی - ثبت نهایی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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
<%@ Register Src="../UserControl/EPaymentUserControl.ascx" TagName="EPaymentUserControl"
    TagPrefix="TspUserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script language="javascript" type="text/javascript">
        function SetPaymentType() {
            if (RadiobtnPaymentType.GetValue() == 0) {
                PanelEpayment.SetVisible(true);
            }
            else {
                PanelEpayment.SetVisible(false);
            }
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div align="right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>

            <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server">
                <Items>
                    <dxm:MenuItem Text="چهارچوب شئون حرفه ای" Name="Membership">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Member" Text="مشخصات فردی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Activity" Text="فعالیت ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Language" Text="زبان ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Summary" Text="خلاصه اطلاعات">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="End" Text="ثبت نهایی" Selected="true">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>

            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="ثبت عضویت حقیقی"
                runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <div class="row">
                            <p align="justify" dir="rtl" style="line-height: 15pt">
                                لطفا ظرف مدت 90 روز از این تاریخ با در دست داشتن <b>مدارک ذیل و کد رهگیری</b> به
                                                        واحد عضویت نظام مهندسی ساختمان استان فارس واقع در ساختمان مرکزی به نشانی شیراز بلوار
                                                        ستارخان انتهای کوچه نمازی مراجعه نمایید.
                                                        <br>
                                بعد از مهلت تعیین شده درخواست به صورت خودکار از سیستم حذف می گردد.
                            </p>
                            <p align="justify" class="HelpUL">
                                ** توجه نمایید درصورت ورود اطلاعات ناقص و یا نادرست (بخصوص عدم مطابقت عنوان مدرک
                                                        تحصیلی انتخاب شده با عنوان مدرک درج شده بر روی دانشنامه شما) عواقب بعدی این عمل
                                                        به عهده ی شما می باشد و <u>مبلغ واریز شده به حساب به هیچ عنوان قابل بازگشت نمی باشد</u>.
                                                        
                            </p>
                            <p align="justify" class="HelpUL">
                                **توجه نمایید درصورتی که پیش از این در سایر استان ها عضو بوده اید بایستی در صفحه ثبت اطلاعات شخصی تیک ''
	از استان دیگری به استان فارس منتقل شده ام'' را انتخاب کرده باشید.در صورت هرگونه اشتباه و یا عدم اعلام انتقال از سایر استان ها در روند ثبت نام مبلغ واریز شده به حساب به هیچ عنوان قابل بازگشت نمی باشد.
                            </p>
                            <div dir="rtl" align="right" style="line-height: 18pt">
                                <b>مدارک مورد نیاز جهت ارائه به سازمان پس از ثبت درخواست در سیستم :</b>
                                <ol dir="rtl" align="right">
                                    <li>دو نسخه تصوير برابر اصل شده (كارداني ، كارشناسي و ،كارشناسي ارشد ،دكترا )در دفتر اسناد رسمي </li>
                                    <li>يك نسخه تصوير برابر اصل شده شناسنامه (صفحه اول ،درصورت داشتن توضيحات ، صفحه توضيحات نيز الزامي مي باشد) در دفتر اسناد رسمي)</li>
                                    <li>يك نسخه تصوير برابر اصل شده كارت ملي در دفتر اسناد رسمي</li>
                                    <li>يك نسخه تصوير برابر اصل شده كارت پايان خدمت يا هر نامه يا مدركي كه وضعيت خدمت را مشخص مي كند (درصورتي كه عنوان نامه به نظام مهندسي مي باشد فقط ارائه اصل نامه الزامي است )در دفتر اسناد رسمي</li>
                                    <li>اصل نامه عدم عضويت در نظام كارداني (در صورت داشتن مدرك كارشناسي ناپيوسته)</li>
                                    <li>یک مدرک دال بر اینکه حداقل شش ماه ساکن استان فارس بوده اید مانند :
                                                                <ul>
                                                                    <li>گواهی معتبر اشتغال به کار</li>
                                                                    <li>سوابق پرداخت بیمه</li>
                                                                    <li>اجاره نامه</li>
                                                                    <li>تاییدیه شورای محل</li>
                                                                    <li>و ...</li>
                                                                </ul>
                                    </li>
                                </ol>
                            </div>
                            <p align="justify" class="HelpUL">
                                قابل ذکر است اطلاعات شما ظرف مدت 48 ساعت كاری كنترل می شود درصورت دريافت پيام (درخواست اطلاعات عضویت شما مورد بررسی قرارگرفت.لطفاً جهت مشاهده نتیجه بررسی و اقدام لازم، پس از 48 ساعت کاری، ازپورتال شخصی خود،منوی اصلی،مدیریت درخواستها،درنوار ابزار،گزینه پی گیری گردش کار، برروی آیکون آخرین پانوشت کلیک نمائید.) با دردست داشتن مدارك زير به سازمان نظام مهندسي واحد عضويت و پروانه اشتغال مراجعه نمائيد
                                                       <%-- قابل ذکر است جهت پیگیری روند ثبت نام خود از طریق همین سامانه و با استفاده از نام
                                                        کاربری و رمز عبور خود اقدام نمایید.در صورت عدم پیگیری شما از این طریق و در صورت
                                                        ناقص بودن اطلاعات کارمندان سازمان هیچگونه مسئولیتی در قبال تائید به موقع عضویت شما
                                                        و یا عدم تائید عضویت شما ندارند.--%>
                            </p>
                        </div>
                        <fieldset>
                            <legend>نحوه پرداخت حق عضویت</legend>
                            <ul class="HelpUL">
                          <%--      <li>در انتخاب روش پرداخت دقت نمایید.پس از کلیک برروی دکمه ذخیره امکان تغییر روش وجود
                                                                        ندارد. </li>--%>
                                <li runat="server" id="liPaymentComGen">در صورت وارد شدن به صفحه بانک به هیچ عنوان از دکمه بازگشت مرورگر استفاده ننمایید.
                                </li>
                                 <li runat="server" id="liPaymentComEnt" visible="false">پرداخت حق عضویت اعضای انتقالی با حضور در سازمان و پرداخت فیش صورت می پذیرد.
                                </li>
                            </ul>
                            <dxe:ASPxRadioButtonList ID="RadiobtnPaymentType" ClientInstanceName="RadiobtnPaymentType"
                                runat="server"
                                Border-BorderColor="Transparent" >
                                <Items>
                                    <dxe:ListEditItem Selected="true" Text="پرداخت حق عضویت به صورت الکترونیک و از طریق درگاه بانک تجارت"
                                        Value="0"  />
                                  <%--  <dxe:ListEditItem   Text="پرداخت حق عضویت به صورت دریافت فیش بانکی از سازمان و پرداخت حضوری در بانک"
                                        Value="1" />--%>
                                </Items>
                                <ClientSideEvents SelectedIndexChanged="function(s,e){
                                                      SetPaymentType();
                                                        }" />
                            </dxe:ASPxRadioButtonList>
                        </fieldset>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <TSPControls:CustomASPxRoundPanel ID="PanelUserInfo" HeaderText="مشخصات کاربری" runat="server"
                Width="100%" Visible="false">
                <PanelCollection>
                    <dxp:PanelContent>
                        <div class="row">
                            <div class="col-md-3">نام کاربری:</div>
                            <div class="col-md-3">
                                <dxe:ASPxLabel runat="server" ID="ASUserName" ForeColor="DarkBlue">
                                </dxe:ASPxLabel>
                            </div>
                            <div class="col-md-3">رمز عبور:</div>
                            <div class="col-md-3">
                                <dxe:ASPxLabel runat="server" ID="ASPassword" ForeColor="DarkBlue">
                                </dxe:ASPxLabel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">پست الکترونیکی:</div>
                            <div class="col-md-3">
                                <dxe:ASPxLabel runat="server" ID="ASEmailUser" ForeColor="DarkBlue">
                                </dxe:ASPxLabel>
                            </div>
                            <div class="col-md-3">کد رهگیری:</div>
                            <div class="col-md-3">
                                <dxe:ASPxLabel runat="server" ID="lblFollowCode" ForeColor="DarkBlue">
                                </dxe:ASPxLabel>
                            </div>
                        </div>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <dxp:ASPxPanel ID="PanelEpayment" ClientInstanceName="PanelEpayment" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <TspUserControl:EPaymentUserControl ID="EPaymentUC" runat="server" />
                    </dxp:PanelContent>
                </PanelCollection>
            </dxp:ASPxPanel>

            <div class="Item-center">
                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" UseSubmitBehavior="False" CausesValidation="False"
                    Text="&nbsp;&nbsp;&nbsp;بازگشت&nbsp;&nbsp;&nbsp;" EnableTheming="False" ToolTip="بازگشت"
                    ID="btnPre" EnableViewState="False" OnClick="btnPre_Click" Visible="true">
                </TSPControls:CustomAspxButton>
                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" UseSubmitBehavior="False" CausesValidation="False"
                    Text="&nbsp;&nbsp;&nbsp;انصراف&nbsp;&nbsp;&nbsp;" EnableTheming="False" ToolTip="انصراف"
                    ID="btnCancel" EnableViewState="False" OnClick="btnCancel_Click" Visible="true">
                </TSPControls:CustomAspxButton>

                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" UseSubmitBehavior="False" CausesValidation="False"
                    Text="&nbsp;&nbsp;&nbsp;ذخیره&nbsp;&nbsp;&nbsp;" EnableTheming="False" ToolTip="ذخیره"
                    ID="btnSave" EnableViewState="False" OnClick="btnSave_Click" Visible="true">


                    <ClientSideEvents Click="function(s,e){
                                                if(RadiobtnPaymentType.GetEnabled())
                                                {
                                                 if (RadiobtnPaymentType.GetValue() == 0)
                                                {
                                                 e.processOnServer= confirm('آیا از ثبت نهایی اطلاعات مطمئن می باشید؟.')
                                                 }
                                                 else
                                                 {
                                                  e.processOnServer= confirm('آیا از ثبت نهایی اطلاعات مطمئن می باشید؟')
                                                 }        
                                                 }                                        
                                                }" />
                </TSPControls:CustomAspxButton>
                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" Text="پرداخت الکترونیکی" Width="170px" ID="btnEpayment"
                    EncodeHtml="false"
                    UseSubmitBehavior="false" CausesValidation="False" Visible="false" OnClick="btnEpayment_Click">
                </TSPControls:CustomAspxButton>

                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" Text="&nbsp;&nbsp;&nbsp;چاپ" Width="150px" ID="btnPrint"
                    EncodeHtml="false" AutoPostBack="False"
                    UseSubmitBehavior="false" CausesValidation="False" EnableViewState="False"
                    Visible="false">
                    <ClientSideEvents Click="function(s, e) {
  window.open(HiddenPrintDetial.Get('PrintUserData'));
}"></ClientSideEvents>
                </TSPControls:CustomAspxButton>
                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" Text="&nbsp;&nbsp;&nbsp;چاپ اطلاعات کاربری" Width="170px"
                    ID="btnPrintUserInfo" EncodeHtml="false" AutoPostBack="False"
                    UseSubmitBehavior="false" CausesValidation="False" EnableViewState="False"
                    Visible="false">
                    <ClientSideEvents Click="function(s, e) {
  window.open(HiddenPrintDetial.Get('PrintUserInfo'));
}"></ClientSideEvents>
                </TSPControls:CustomAspxButton>
                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" Text="&nbsp;&nbsp;&nbsp;ورود به پرتال" Width="150px"
                    ID="btnContinue" EncodeHtml="false" OnClick="btnContinue_Click"
                    UseSubmitBehavior="false" CausesValidation="False" EnableViewState="False"
                    Visible="false">
                </TSPControls:CustomAspxButton>
            </div>
            <dx:ASPxHiddenField ID="HiddenPrintDetial" runat="server" ClientInstanceName="HiddenPrintDetial">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="HiddenFieldWizardMe" runat="server">
            </dx:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="HDMemberId" runat="server" Visible="False" />
    <asp:HiddenField ID="HDRequestId" runat="server" Visible="False" />
    <input id="merchantId" name="merchantId" type="hidden" value="<%= TSP.Utility.OnlinePayment.GetNezamMerchantId() %>" />
    <input id="paymentId" name="paymentId" type="hidden" value="<%=  Session["MePaymentId"]%>" />
    <input id="amount" name="amount" type="hidden" value='<%= (Session["MePrice"]!=null)?Convert.ToInt64(Decimal.Parse(Session["MePrice"].ToString())):-1%>' />
    <input id="revertURL" name="revertURL" type="hidden" value="<%= this.Request.Url.AbsoluteUri %>" />
    <input id="customerId" name="customerId" type="hidden" />
</asp:Content>
