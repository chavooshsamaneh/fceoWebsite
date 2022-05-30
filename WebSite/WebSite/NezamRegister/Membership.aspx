<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPageWebsite.master"
    CodeFile="Membership.aspx.cs" Inherits="NezamRegister_Membership" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
            <div align="right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
    <TSPControls:CustomASPxRoundPanel ID="RoundPanelPrj" HeaderText="نحوه عضویت در سازمان نظام مهندسی استان فارس" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table style="  width: 100%;"">
                    <tbody>
                        <tr>
                            <td style="margin: 15px 15pt 15px 15px">
                              
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal">
                                    <b>
                                        <span style="font-size: 9pt; ">چه اشخاصی می توانند به عضویت سازمان نظام مهندسی ساختمان استان فارس درآیند؟<o:p></o:p></span>
                                    </b>
                                </p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal">
                                    <span style="font-size: 9pt; ">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal"><span style="font-size: 9pt; ">اشخاص حقیقی و حقوقی که در حوزه استان فارس در زمینه ساختمان فعالیت دارند می توانند به عضویت این سازمان درآیند.<o:p></o:p></span></p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal">
                                    <span style="font-size: 9pt; ">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal"><span style="font-size: 9pt; ">الف- اشخاص حقیقی باید در یکی از <b>رشته های اصلی مهندسی</b> که شامل معماری، عمران، تاسیسات مکانیکی، تاسیسات برقی، شهرسازی، نقشه برداری و ترافیک و یا در یکی از <b>رشته های مرتبط با مهندسی ساختمان</b> دارای مدرک تحصیلی حداقل کارشناسی باشند.<o:p></o:p></span></p>
                                <p style="margin: 0cm 36pt 0pt 0cm; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal"><span style="font-size: 9pt; ">تبصره 1- مهندس حوزه هر استان در این قانون به شخصی اطلاق می شود که حداقل متولد آن استان یا 6 ماه ممتد پیش از تاریخ تسلیم درخواست عضویت، در آن استان مقیم باشد.<o:p></o:p></span></p>
                                <p style="margin: 0cm 36pt 0pt 0cm; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal">
                                    <span style="font-size: 9pt; ">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                                <p style="margin: 0cm 36pt 0pt 0cm; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal"><span style="font-size: 9pt; ">تبصره 2- هر یک از مهندسان در بیش از یک سازمان نمی توانند عضویت یابند.<o:p></o:p></span></p>
                                <p style="margin: 0cm 36pt 0pt 0cm; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal">
                                    <span style="font-size: 9pt; ">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                                <p style="margin: 0cm 36pt 0pt 0cm; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal"><span style="font-size: 9pt; ">تبصره 3- رشته های مرتبط با مهندسی ساختمان به کلیه رشته هایی اطلاق می شود که عنوان آنها با رشته های اصلی یاد شده متفاوت بوده ولی محتوای علمی و آموزشی آنها با رشته های اصلی بیش از 70% در ارتباط باشد و فارغ التحصیلان اینگونه رشته ها خدمات فنی معینی را در زمینه های طراحی, محاسبه, اجرا, نگهداری, کنترل, آموزش, تحقیق, و نظایر آن به بخشهای ساختمان و شهرسازی عرضه می کنند اما این خدمات از حیث حجم, اهمیت و میزان تاثیر عرفا همطراز خدمات رشته های اصلی مهندسی ساختمان نباشد.<o:p></o:p></span></p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal"><span style="font-size: 9pt; ">ب- عضویت <b>اشخاص حقوقی</b> شاغل به کار مهندسی در رشته های اصلی و اشخاص حقیقی در رشته های مرتبط با مهندسی ساختمان بلا مانع است.<o:p></o:p></span></p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal">
                                    <span style="font-size: 9pt; ">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal">
                                    <span style="font-size: 9pt; ">
                                        <o:p>&nbsp;</o:p>
                                    </span><span style="font-size: 9pt; ">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal">
                                    <b>
                                        <span style="font-size: 9pt; ">روند ثبت عضویت اشخاص حقیقی در سازمان نظام مهندسی چگونه است؟<o:p></o:p></span></b>
                                </p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal">
                                    <span style="font-size: 9pt; ">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal">
                                    <span style="font-size: 9pt; ">در ابتدا اطلاعات مربوط به خود را در صفحاتی که از طریق لینک
                                        <dxe:ASPxHyperLink runat="server" Text="عضویت حقیقی الکترونیکی در سازمان" ID="ASPxHyperLink1" NavigateUrl="~/NezamRegister/WizardMember_Membership.aspx"></dxe:ASPxHyperLink>
                                        &nbsp;در دسترس است تکمیل کرده و سپس در تاریخ معین اصل<span> مدارک مورد لزوم عضویت حقیقی</span> و فیش بانکی را به سازمان آورده تا مسئول عضویت با کنترل آنها کارت عضویت شما در سازمان نظام مهندسی را به شما تحویل دهد.<o:p></o:p></span>
                                </p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal"><span style="font-size: 9pt; ">در روش اینترنتی مهلتی برای مراجعه به سازمان و ارائه اصل مدارک و گرفتن کارت عضویت تعیین می شود. اگر در مهلت زمان تعیین شده به سازمان مراجعه نکنید باید تمامی مراحل عضویت اینترنتی را از نو انجام دهید.<o:p></o:p></span></p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal">
                                    <span style="font-size: 9pt; ">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal"><b><span style="font-size: 9pt; ">روند ثبت عضویت اشخاص حقوقی در سازمان نظام مهندسی چگونه است؟<o:p></o:p></span></b></p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal">
                                    <span style="font-size: 9pt; ">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal"><span style="font-size: 9pt; ">به دو صورت اینترنتی و حضور در سازمان می توان عضو حقوقی سازمان نظام مهندسی ساختمان استان فارس شد.<o:p></o:p></span></p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal">
                                    <span style="font-size: 9pt; color: #0000ff; ">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal"><span style="font-size: 9pt; ">روش اینترنتی:<o:p></o:p></span></p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal">
                                    <span style="font-size: 9pt; ">روش اینترنتی سریع ترین روش برای ثبت عضویت در سازمان می باشد. در ابتدا نماینده شرکت اطلاعات مربوط به شرکت خود را در صفحاتی که از طریق لینک&nbsp;<span style="color: blue"><span style="color: #000000"> </span>
                                        <dxe:ASPxHyperLink runat="server" Text="عضویت حقوقی الکترونیکی در سازمان" ID="ASPxHyperLink2" NavigateUrl="~/NezamRegister/WizardOffice_Membership.aspx"></dxe:ASPxHyperLink>
                                    </span>&nbsp;در دسترس است تکمیل کرده و سپس در تاریخ معین شخصا اصل <span>مدارک مورد لزوم عضویت حقوقی</span> و فیش های بانکی را به سازمان آورده تا مسئول عضویت با کنترل آنها کارت عضویت شرکت در سازمان نظام مهندسی را به شخص نماینده تحویل دهد.<o:p></o:p></span>
                                </p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal"><span style="font-size: 9pt; ">در روش اینترنتی مهلتی برای مراجعه به سازمان و ارائه اصل مدارک و گرفتن کارت عضویت تعیین می شود. اگر در مهلت زمان تعیین شده به سازمان مراجعه نکنید باید تمامی مراحل عضویت اینترنتی را از نو انجام دهید.<o:p></o:p></span></p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal">
                                    <span style="font-size: 9pt; ">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal">
                                    <span style="font-size: 9pt; ">حضور در سازمان و در خواست عضویت:
                                    <o:p></o:p>
                                    </span>
                                </p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal"><span style="font-size: 9pt; ">برای انجام ثبت عضویت با این روش در ابتدا باید فرمهای عضویت حقوقی را از طریق لینک <span>فرمهای عضویت حقوقی</span> در وب سایت سازمان دریافت کرد. بعد از چاپ و تکمیل فرمها آنها را همراه با اصل <span>مدارک مورد لزوم عضویت حقوقی</span> به سازمان تحویل داد. مسئول دفتر تاریخی را برای پاسخ گویی و مراجعه بعدی نماینده شرکت تعیین می کند. شخص با مراجعه در تاریخ مقرر می تواند پاسخ مسئول دفتر را دریافت کند. چون پاسخ مسئول دفتر ممکن است دال بر وجود نواقصی در اطلاعات و یا مشکلات دیگر باشد که انجام عضویت را با مشکل مواجه می کند برگزیدن این روش ممکن است با مراجعات پیاپی به سازمان همراه باشد!<o:p></o:p></span></p>
                                <p style="margin: 0cm 0cm 0pt; direction: rtl; unicode-bidi: embed; text-align: justify" dir="rtl" class="MsoNormal">
                                    <span style="font-size: 9pt; ; mso-bidi-language: FA" dir="ltr">
                                        <o:p>&nbsp;</o:p>
                                    </span>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" >
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width: 101px">
                                                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" Text="عضویت حقیقی" Width="145px" ID="ASPxButton2" OnClick="ASPxButton2_Click"></TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="width: 100px">
                                                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" Text="عضویت حقوقی (شرکت)" ID="ASPxButton1" Wrap="False" OnClick="ASPxButton1_Click"></TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
</asp:Content>
