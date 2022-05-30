<%@ Page Title="درخواست ارتقاء پایه پروانه اشتغال-سوگند نامه" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="WizardUpgradeOath.aspx.cs" Inherits="Members_Documents_WizardUpgradeOath" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxlp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1" Namespace="DevExpress.Web"
    TagPrefix="dxe" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelNezamRegister" runat="server">
        <ContentTemplate>
            <div align="right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>

            <TSPControls:CustomAspxMenuHorizontal ID="MenuSteps" runat="server">
                <Items>
                    <dxm:MenuItem Text="مدارک لازم" Name="Oath" Selected="true">
                        <Image Width="15px" Height="15px" />
                    </dxm:MenuItem>

                    <dxm:MenuItem Name="Documents" Text="استعلام ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="JobConfirm" Text="تاییدیه سابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Summary" Text="خلاصه اطلاعات">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="End" Text="ثبت نهایی">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>

            <TSPControls:CustomASPxRoundPanel ID="RoundPanelMe" HeaderText="درخواست ارتقاء پایه پروانه اشتغال بکار مهندسی"
                runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <fieldset>
                            <legend class="fieldset-legend" dir="rtl"><b>پیش نیاز ها</b></legend>

                            <table width="100%">
                                <tr>
                                    <td align="right" valign="top" colspan="6">

                                        <p class="HelpUL">
                                            <b>قبل از شروع فرایند درخواست ارتقاء پایه پروانه اشتغال به چه مدارکی نیاز است و چه مواردی را
                                                                        باید بررسی کرد</b>
                                        </p>
                                        <ul style=" font-size: 8pt; line-height: 15pt">
                                            <li>بررسی درستی تصویر متقاضی، تصاویر شناسنامه،تصاویر کارت ملی، مدرک پایان خدمت و شماره تلفن همراه و آدرس منزل که
                                                                        در همین صفحه نمایش داده شده اند</li>
                                            <li>بررسی درستی اطلاعات و تصویر مدارک تحصیلی</li>
                                            <li>تهیه نامه سابقه کار از محل کار یا تکمیل فرم سابقه کار (جهت تکمیل فرم سابقه کار: از بالای صفحه، قسمت قوانین و بخشنامه ها ، فرم سابقه کار (کدفرم : 003/01 ) را انتخاب کرده و چاپ آن را تهیه کنید.
                                             (<a  target="_blank" class="blink TitleOragne" href="https://fceo.ir/Forms.aspx">لینک مستقیم آرشیو فرم ها جهت چاپ فرم سابقه کار</a> ) </li>
                                            <li>پاسخ نامه تسویه حساب امور مالیاتی (گواهی موضوع ماده 186 قانون مالیات های مستقیم) جهت تهیه معرفی نامه سازمان نظام مهندسی به اداره امور مالیاتی به صفحه پورتال اعضا/درخواست های واحد پروانه اشتغال /چاپ نامه تسویه حساب امور مالیاتی مراجعه فرمایید.در صورت پایان اعتبار پروانه اشتغال (یا در صورتی که تنها دو ماه به پایان اعتبار پروانه اشتغال شما باقی مانده باشد)، ارائه مفاصا حساب مالیاتی الزامی می باشد.
                                            </li>                                          
                                        </ul>
                                        <br />
                                        <fieldset>
                                            <legend class="HelpUL" dir="rtl"><b>سابقه کار</b></legend>
                                            <p class="HelpUL">
                                                نکاتی که در تهیه و ارائه سابقه کار باید رعایت نمود
                                            </p>
                                            <ul style=" font-size: 7pt; line-height: 15pt">
                                                <li>تاریخ شروع و پایان سابقه کاری مشخص باشد. </li>
                                                <li>سابقه کار می بایستی به روز (حتی الامکان تاکنون) ارائه گردد</li>
                                                <li>نوع سابقه کاری و فعالیت، مربوط به رشته تحصیلی و صلاحیت مد نظر جهت ارتقا پایه باشد.(به عنوان مثال در صورت ارتقا پایه محاسبه یا طراحی ، سابقه کار در زمینه محاسبه یا طراحی ارائه گردد)</li>

                                                <li>ارائه بیمه نیاز نمی باشد</li>
                                                <li>آپلود اصل نامه سابقه کار یا اصل فرم تکمیل شده سابقه کار (آپلود کپی نامه یا فرم مورد قبول نمی باشد)</li>
                                                 <li>گذراندن دوره آموزشي HSE براي دارندگان پروانه اشتغال رشته عمران و معماري كه داراي صلاحيت اجرا مي باشند، الزامي گرديده است.</li>
                                       
                                            </ul>
                                            <p class="HelpUL">
                                                ارائه سابقه کار به یکی از سه روش زیر امکان پذیر است
                                            </p>
                                            <ul style=" font-size: 7pt; line-height: 15pt">
                                                <li>شرکت خصوصی (سهامی خاص) دارای رتبه بندی ازسازمان مدیریت و برنامه ریزی یا دارای پروانه
                                                                            حقوقی از سازمان نظام مهندسی:ارائه نامه روی سربرگ شرکت یا تکمیل قسمت( الف) به همراه
                                                                            کپی رتبه بندی شرکت</li>
                                                <li>ارگان دولتی:ارائه نامه از ارگانی که در آن خدمت کرده اید.</li>
                                                <li>درصورت عدم تهیه نامه ازمراکز عنوان شده یا فعالیت بصورت شخصی: تکمیل قسمت (ب) فرم
                                                                            سابقه کار </li>
                                            </ul>
                                            <dl style=" font-size: 7pt; line-height: 15pt">
                                                <dt><b>روش تکمیل قسمت الف سابقه کار</b></dt>
                                                <dd>تکمیل فرم توسط شما و امضاء و مهرشرکت به همراه کپی رتبه بندی شرکت.</dd>
                                                <dd>یا تکمیل فرم توسط شما و مهر دفتر مهندسی طراحی (دفترطراحی زیرنظر نظام مهندسی) به
                                                                            همراه کپی پروانه دفترمهندسی معتبر(سابقه کار از دفتر طراحی فقط درخصوص سابقه بخش طراحی
                                                                            یا محاسبه قابل قبول می باشد)</dd>
                                                <dd>یا تکمیل فرم توسط شما باامضا و مهرشرکت مهندسی دارای پروانه حقوقی ازسازمان به همراه
                                                                            کپی پروانه حقوقی که از نظام مهندسی دریافت کرده اند
                                                </dd>
                                            </dl>
                                            <dl style=" font-size: 7pt; line-height: 15pt">
                                                <dt><b>روش تکمیل قسمت ب سابقه کار</b></dt>
                                                <dd>تکمیل فرم توسط شما وامضاء و مهرنظام مهندسی دو نفرمهندس با بیش ازده سال سابقه کار.
                                                                            (ده سال ازمدرک تحصیلی لیسانس تایید کنندگان گذشته باشد و حتماً دارای پروانه اشتغال
                                                                            معتبر باشند.) پایه پروانه اشتغال وهم رشته بودن دونفرمهندس تائید کننده سابقه کار با
                                                                            رشته متقاضی مد نظر نمی باشد.</dd>
                                                <dd>
                                                    <b>در صورتی که دو مهندس تایید کننده سابقه کار شما از اعضای استان دیگری (غیر از استان فارس) می باشند، تاریخ فارغ التحصیلی و تاریخ اعتبار پروانه اشتغال این دو شخص در فرم سابقه کار توسط سازمان نظام مهندسی همان استان تکمیل شده و با امضا و مهر مسئول مربوطه در آن سازمان تائید گردد. </b>
                                                </dd>
                                            </dl>
                                            <dl style=" font-size: 7pt; line-height: 15pt">
                                                <dt>مدت زمان مورد نیاز سابقه کار بعد از فراغت از تحصیل برای متقاضیان پایه سه با توجه
                                                                            به مقطع تحصیلی به صورت زیر می باشد</dt>
                                                <dd>ارتقا پایه سه به دو و دارای مدرک کارشناسی: 7 سال کامل بعد از فارغ التحصیلی كارشناسی (درصورتی كه قبلاً 7 سال سابقه كار را ارائه داده اید ، مقداری سابقه کار بعد از آخرین تاریخ سابقه کاری که روی سایت آپلودکرده اید تا کنون (جدید) ارائه گردد. ( عنوان تاریخ پایان سابقه کار را تاکنون نزنید تاریخ دقیق وارد کنید)</dd>
                                                <dd>ارتقا پایه سه به دو و دارای مدرک کارشناسی ارشد: 6 سال کامل بعد از فارغ التحصیلی كارشناسی نیاز می باشد(درصورتی كه قبلاً 6 سال سابقه كار را ارائه داده اید، مقداری سابقه کار بعد از آخرین تاریخ سابقه کاری که روی سایت آپلودکرده اید تا کنون (جدید) ارائه گردد. ( عنوان تاریخ پایان سابقه کار را تاکنون نزنید تاریخ دقیق وارد کنید)</dd>
                                                <dd>ارتقا پایه سه به دو و دارای مدرک دکترا: 5 سال کامل بعد از فارغ التحصیلی كارشناسی نیاز می باشد(درصورتی كه قبلاً 5 سال سابقه كار را ارائه داده اید، مقداری سابقه کار بعد از آخرین تاریخ سابقه کاری که روی سایت آپلودکرده اید تا کنون (جدید) ارائه گردد. ( عنوان تاریخ پایان سابقه کار را تاکنون نزنید تاریخ دقیق وارد کنید)</dd>

                                                <dd>ارتقا پایه دو به یک و دارای مدرک کارشناسی: 12 سال کامل بعد از فارغ التحصیلی كارشناسی نیاز می باشد(درصورتی كه قبلاً 12 سال سابقه كار را ارائه داده اید، مقداری سابقه کار بعد از آخرین تاریخ سابقه کاری که روی سایت آپلودکرده اید تا کنون (جدید) ارائه گردد. ( عنوان تاریخ پایان سابقه کار را تاکنون نزنید تاریخ دقیق وارد کنید)</dd>
                                                <dd>ارتقا پایه دو به یک و دارای مدرک کارشناسی ارشد: 11 سال کامل  بعد از فارغ التحصیلی كارشناسی نیاز می باشد(درصورتی كه قبلاً 11 سال سابقه كار را ارائه داده اید، مقداری سابقه کار بعد از آخرین تاریخ سابقه کاری که روی سایت آپلودکرده اید تا کنون (جدید) ارائه گردد. ( عنوان تاریخ پایان سابقه کار را تاکنون نزنید تاریخ دقیق وارد کنید)</dd>
                                                <dd>ارتقا پایه دو به یک و دارای مدرک دکترا: 10 سال کامل بعد از فارغ التحصیلی كارشناسی نیاز می باشد(درصورتی كه قبلاً 10 سال سابقه كار را ارائه داده اید، مقداری سابقه کار بعد از آخرین تاریخ سابقه کاری که روی سایت آپلودکرده اید تا کنون (جدید) ارائه گردد. ( عنوان تاریخ پایان سابقه کار را تاکنون نزنید تاریخ دقیق وارد کنید)</dd>
                                            </dl>
                                            <ul style=" font-size: 7pt; line-height: 15pt">
                                                <li>سوابق کار ارائه شده قبلی شما ، در سایت نشان داده می شود ( درصورتی که قبلاً درخواست سیستمی در پروانه اشتغال داده باشید) ، درصورتی که سابقه کار های ارائه شده برای ارتقا پایه کافی باشد ، دیگر نیازی به ارائه سابقه کار نیست ، در غیراینصورت می بایستی سابقه کار جدید ارائه گردد که تاریخ آنها به سوابق ارائه شده قبلی متفاوت بوده و هم پوشانی نداشته باشد.</li>
                                                <li>تاریخ شروع و پایان سابقه کار ، که در نامه یا فرم سابقه کار نوشته شده است ،می بایستی دقیقاً با تاریخ شروع و پایان کار که در سایت در قسمت سابقه کار وارد می کنید یکسان باشد. </li>
                                                <li>در صورتي كه پايه نظارت شما دو مي باشد ، جهت ارتقا پايه سه به دو اجرا ، ارائه سابقه كار در زمينه اجرا الزامي مي باشد و برعكس</li>
                                                <li>در صورتي كه پايه نظارت شما يك مي باشد ، جهت ارتقا پايه دو به يك اجرا ، ارائه سابقه كار در زمينه اجرا الزامي مي باشد و برعكس</li>

                                               </ul>
                                        </fieldset>
                                        <p class="HelpUL">
                                            درصورت هرگونه کسری در مدارک روند تایید درخواست صدور پروانه شمابا تاخیر مواجه می
                                                                    گردد و سازمان در قبال تاخیر پیش آمده هیچگونه مسئولیتی نخواهد داشت
                                        </p>
                                        <p class="HelpUL">
                                            <b>توجه! </b>
                                        </p>
                                        <p style=" font-size: 8pt; line-height: 15pt">
                                            از آنجاییکه کلیه مراحل درخواست ارتقاء پایه پروانه اشتغال و یا نواقص پروانه ازطریق پیامک به شما اطلاع
                                                                    داده می شود. نسبت به بروزرسانی شماره همراه خود به روش توضیح داده شده اقدام کنید
                                                                    درغیراینصورت این سازمان، هیچ گونه مسئولیتی درقبال عدم دریافت پیامکها، نخواهد شد.
                                                                    در ضمن در صورت غیر فعال کردن پیامک های تبلیغاتی، پیامک سازمان به تلفن همراه اعضا می رسد.
                                        </p>
                                        <p class="HelpUL">
                                            در صورت نیاز به تغییر تصویر متقاضی
                                        </p>
                                        <p style=" font-size: 8pt; line-height: 15pt">
                                            از درون همین صفحه لینک تغییرات اطلاعات پایه را انتخاب کنید و در داخل صفحه ای که
                                                                    باز می شود عکس 4×3 <a style="color: DarkRed">جدید </a><a style="color: DarkRed">رنگی</a>(حداکثرمربوط
                                                                    به سه سال پیش) آقایان : عکس با زمینه سفید، پیراهن یقه دار (تی شرت مورد قبول نیست)،بدون
                                                                    عینک، کراوات و دیگر تزئینات خانمها: قرص صورت مشخص و با حجاب کامل (مقنعه)
                                        </p>
                                        <p class="HelpUL">
                                            درصورت نیاز به تغییرات در اطلاعات شناسنامه،کارت ملی و کارت پایان خدمت
                                        </p>
                                        <p style=" font-size: 8pt; line-height: 15pt">
                                            از درون همین صفحه لینک تغییرات اطلاعات پایه را انتخاب و در صفحه ایی که باز می شود
                                                                    تصاویر شناسنامه، کارت ملی و کارت پایان خدمت می بایست در قسمت پرتال شخصی شما درسایت نظام مهندسی بارگزاری شود.
                                        </p>

                                        <p class="HelpUL">
                                            در صورت نیاز به تغییر شماره تلفن همراه خود به صورت زیر عمل کنید
                                        </p>
                                        <p style=" font-size: 8pt; line-height: 15pt">
                                            از درون همین صفحه لینک تغییرات اطلاعات پایه را انتخاب و در صفحه ایی که باز می شود
                                                                    اطلاعات و شماره همراه جدید خود را تغییر دهید
                                        </p>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="center" align="center" width="30%">
                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgMember" ClientInstanceName="meImg"
                                            Border-BorderWidth="1px" Border-BorderStyle="Solid">
                                            <EmptyImage Height="75px" Width="75px" Url="~/Images/Person.png">
                                            </EmptyImage>
                                        </dxe:ASPxImage>
                                    </td>
                                    <td align="center" valign="top" colspan="5" width="70%">
                                        <table width="100%">
                                            <tr>
                                                <td id="Td48" runat="server" align="right" valign="top" width="50%">تصویر صفحه اول شناسنامه:
                                                </td>
                                                <td align="right" valign="top" width="50%">
                                                    <dxe:ASPxHyperLink ID="HpIdNo" runat="server" ClientInstanceName="hidno" Target="_blank"
                                                        Text="تصویر صفحه اول شناسنامه">
                                                    </dxe:ASPxHyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Td2" runat="server" align="right" valign="top" width="50%">تصویر صفحه دوم شناسنامه:
                                                </td>
                                                <td align="right" valign="top" width="50%">
                                                    <dxe:ASPxHyperLink ID="HIdNoP2" runat="server" ClientInstanceName="HIdNoP2" Target="_blank"
                                                        Text="تصویر صفحه دوم شناسنامه">
                                                    </dxe:ASPxHyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Td3" runat="server" align="right" valign="top" width="50%">تصویر صفحه توضیحات شناسنامه:
                                                </td>
                                                <td align="right" valign="top" width="50%">
                                                    <dxe:ASPxHyperLink ID="HIdNoPDes" runat="server" ClientInstanceName="HIdNoPDes" Target="_blank"
                                                        Text="تصویر صفحه توضیحات شناسنامه">
                                                    </dxe:ASPxHyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">تصویر روی کارت ملی:
                                                </td>
                                                <td align="right" valign="top">
                                                    <dxe:ASPxHyperLink ID="HpSSN" runat="server" ClientInstanceName="hssn" Target="_blank"
                                                        Text="تصویر روی کارت ملی">
                                                    </dxe:ASPxHyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">تصویر پشت کارت ملی:
                                                </td>
                                                <td align="right" valign="top">
                                                    <dxe:ASPxHyperLink ID="HssnBack" runat="server" ClientInstanceName="HssnBack" Target="_blank"
                                                        Text="تصویر پشت کارت ملی">
                                                    </dxe:ASPxHyperLink>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="2">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <dxe:ASPxLabel ID="lblSoldire" ClientVisible="false" runat="server" Text="تصویر روی کارت پایان خدمت:"
                                                        ClientInstanceName="lblSoldire">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td align="right" valign="top" dir="rtl">
                                                    <dxe:ASPxHyperLink ID="HpSoldire" runat="server" ClientVisible="false" ClientInstanceName="hssn" Target="_blank"
                                                        Text="تصویر روی کارت پایان خدمت">
                                                    </dxe:ASPxHyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <dxe:ASPxLabel ID="lblSoldireBack" ClientVisible="false" runat="server" Text="تصویر پشت کارت پایان خدمت:"
                                                        ClientInstanceName="lblSoldireBack">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td align="right" valign="top" dir="rtl">
                                                    <dxe:ASPxHyperLink ID="HpSoldierBack" ClientVisible="false" runat="server" ClientInstanceName="HpSoldierBack" Target="_blank"
                                                        Text="تصویر پشت کارت پایان خدمت">
                                                    </dxe:ASPxHyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">تصویر استعلام عدم عضویت در نظام کاردانی:
                                                </td>
                                                <td align="right" valign="top" dir="rtl">
                                                    <dxe:ASPxHyperLink ID="HpKardani" runat="server" ClientInstanceName="HpKardani" Target="_blank"
                                                        Text="تصویر استعلام عدم عضویت در نظام کاردانی">
                                                    </dxe:ASPxHyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Td1" runat="server" align="right" valign="top" width="50%">شماره همراه:
                                                </td>
                                                <td align="right" valign="top" width="50%">
                                                    <dxe:ASPxLabel runat="server" Text="--------" ID="lblMobileNo" Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%">نشانی محل سکونت
                                    </td>
                                    <td align="right" valign="top" colspan="5" width="70%">
                                        <dxe:ASPxLabel runat="server" Text="--------" ID="lblAddress" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="right" valign="top">
                                        <br />
                                        <p class="HelpUL">
                                            در صورتی که اطلاعات هر یک از تصاویر نمایش داده شده بالا با اصل مدرک شما مغایرت دارد،
                                                                    به وسیله لینک 'درخواست تغییرات اطلاعات پایه' اقدام به تصحیح آنها نمایید و پس از
                                                                    تایید واحد عضویت اقدام به ثبت درخواست صدور پروانه نمایید.درصورت مشاهده مغایرت توسط
                                                                    واحد پروانه درخواست صدور پروانه شما توسط این واحد تایید نمی گردد
                                        </p>
                                        <br />
                                        <TSPControls:CustomAspxDevGridView2 runat="server" Caption="مدارک تحصیلی تایید شده در واحد عضویت"
                                            KeyFieldName="MlId"
                                            AutoGenerateColumns="False" RightToLeft="True" Width="100%" ID="GridViewMajor"
                                            DataSourceID="ObjdsMemberLicence">
                                            <Columns>
                                                <dxwgv:GridViewDataImageColumn FieldName="FilePath" Caption="تصویر مدرک تحصیلی" VisibleIndex="3"
                                                    Width="150px">
                                                    <EditCellStyle Wrap="False">
                                                    </EditCellStyle>
                                                    <PropertiesImage ImageHeight="150px" ImageWidth="150px">
                                                    </PropertiesImage>
                                                </dxwgv:GridViewDataImageColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="MeLicenceNamertl" Caption="رشته" VisibleIndex="3"
                                                    Width="250px">
                                                    <EditCellStyle Wrap="False">
                                                    </EditCellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="UnName" Width="200px" Caption="دانشگاه"
                                                    VisibleIndex="3">
                                                    <EditCellStyle Wrap="False">
                                                    </EditCellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="EndDate" Caption="فارغ التحصیل" VisibleIndex="3">
                                                    <EditCellStyle Wrap="False" HorizontalAlign="Center">
                                                    </EditCellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="Avg" Caption="معدل" VisibleIndex="3">
                                                    <EditCellStyle Wrap="False">
                                                    </EditCellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="CounName" Caption="کشور" VisibleIndex="3">
                                                    <EditCellStyle Wrap="False">
                                                    </EditCellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" ConfirmDelete="True"></SettingsBehavior>
                                            <Settings ShowHorizontalScrollBar="True"></Settings>
                                        </TSPControls:CustomAspxDevGridView2>
                                        <asp:ObjectDataSource ID="ObjdsMemberLicence" runat="server" TypeName="TSP.DataManager.MemberLicenceManager"
                                            SelectMethod="SelectByMemberId" OldValuesParameterFormatString="original_{0}">
                                            <SelectParameters>
                                                <asp:Parameter Type="Int32" DefaultValue="-1" Name="MemberId"></asp:Parameter>
                                                <asp:Parameter DefaultValue="0" Name="InActive" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <br />
                                        <p style=" font-size: 8pt; line-height: 15pt; color: DarkRed">
                                            در صورتی که اطلاعات مدارک تحصیلی شما صحیح نمی باشد، از طریق لینک 'درخواست تغییرات
                                                                    مدرک تحصیلی' اقدام به تصحیح آنها نمایید.سپس به واحد عضویت مراجعه کنید ومدرک جدید
                                                                    خود را ارائه داده تا واحد عضویت استعلام مدرک شما راانجام دهند. بعد از انجام این
                                                                    فرایند پی گیری های لازم را انجام دهید تا هرزمان پاسخ نامه استعلام شما به واحد عضویت
                                                                    ارسال شد بتوانید جهت درج مدرک جدید درپروانه خود اقدام کنید. درواقع از تایید واحد
                                                                    عضویت اقدام به ثبت درخواست صدور پروانه نمایید.
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <table width="100%">
                                            <tr>
                                                <td width="10%"></td>
                                                <td  width="30%">
                                                    <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="درخواست تغییرات اطلاعات پایه" ToolTip="درخواست تغییرات اطلاعات پایه"
                                                        CausesValidation="False" ID="btnChangeBaseInfo" AutoPostBack="False" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" OnClick="btnChangeBaseInfo_Click">
                                                     
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="5%"></td>
                                                <td width="5%"></td>
                                                <td   width="30%">
                                                    <TSPControls:CustomAspxButton  CssClass="ButtonMenue" runat="server" Text="درخواست تغییرات مدرک تحصیلی" ToolTip="درخواست تغییرات مدرک تحصیلی"
                                                        CausesValidation="False" ID="btnLicenceRequest" AutoPostBack="False" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" OnClick="btnLicenceRequest_Click">
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                
                                                <td width="10%"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="6" align="right" valign="top">
                                        <br />

                                        <TSPControls:CustomAspxDevGridView2 runat="server" Caption="پایه و صلاحیت های تایید شده در پروانه"
                                            KeyFieldName="MfdId"
                                            AutoGenerateColumns="False" RightToLeft="True" Width="100%" ID="GridViewResponsibility"
                                            DataSourceID="ObjdsMemberFileDetail">
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MjName" Width="250px" Caption="رشته">
                                                    <CellStyle HorizontalAlign="Center" Wrap="False">
                                                    </CellStyle>
                                                    <EditFormSettings Visible="False"></EditFormSettings>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GMRName" Width="100px"
                                                    Caption="پایه - صلاحیت">
                                                    <CellStyle HorizontalAlign="Center" Wrap="False">
                                                    </CellStyle>
                                                    <EditFormSettings Visible="False"></EditFormSettings>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Date" Name="Date" Width="120px"
                                                    Caption="تاریخ اخذ">
                                                    <CellStyle HorizontalAlign="Center" Wrap="False">
                                                    </CellStyle>
                                                    <EditFormSettings Visible="False"></EditFormSettings>
                                                </dxwgv:GridViewDataTextColumn>
                                                
                                                  <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="RespDateDiff" Caption="تعداد سال"
                                                    Name="JobDateDiff">
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" ConfirmDelete="True"></SettingsBehavior>
                                            <Settings ShowHorizontalScrollBar="True"></Settings>
                                        </TSPControls:CustomAspxDevGridView2>
                                        <asp:ObjectDataSource ID="ObjdsMemberFileDetail" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="SelectById" TypeName="TSP.DataManager.DocMemberFileDetailManager">
                                            <SelectParameters>
                                                <asp:Parameter Type="Int32" DefaultValue="-1" Name="MFId"></asp:Parameter>
                                                <asp:Parameter Type="Int32" DefaultValue="-1" Name="MeId"></asp:Parameter>
                                                <asp:Parameter Type="Int32" DefaultValue="-1" Name="InActive"></asp:Parameter>
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <br />
                                        <p style=" font-size: 8pt; line-height: 15pt; color: DarkRed">
                                            در صورتی که اطلاعات پایه و صلاحیت شما صحیح نمی باشد ، با واحد پروانه اشتغال سازمان تماس حاصل نمایید
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="right" valign="top">
                                        <br />
                                        <TSPControls:CustomAspxDevGridView2 runat="server" Caption="دوره های آموزشی گذرانده شده"
                                            KeyFieldName="PRId"
                                            AutoGenerateColumns="False" RightToLeft="True" Width="100%" ID="GridViewPeriods"
                                            DataSourceID="objdsPeriods">
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn FieldName="CrsNameAndHour" Caption="عنوان دوره/سمینار" VisibleIndex="1" Width="300px">
                                                </dxwgv:GridViewDataTextColumn>

                                                <dxwgv:GridViewDataTextColumn FieldName="GrdName" Width="100px" Caption="ارتقاء پایه"
                                                    VisibleIndex="1">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjName" Caption="رشته" Width="200px">
                                                    <CellStyle Wrap="False"></CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ResName" Caption="صلاحیت">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="TestDate" Caption="تاریخ آزمون">
                                                </dxwgv:GridViewDataTextColumn>

                                            </Columns>
                                            <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" ConfirmDelete="True"></SettingsBehavior>
                                            <Settings ShowHorizontalScrollBar="True"></Settings>
                                        </TSPControls:CustomAspxDevGridView2>
                                        <asp:ObjectDataSource ID="objdsPeriods" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="selectPeriodRegisterForMemberReport" TypeName="TSP.DataManager.PeriodRegisterManager">
                                            <SelectParameters>
                                                <asp:Parameter Type="Int32" DefaultValue="-2" Name="MeId"></asp:Parameter>
                                                <asp:Parameter Type="Int32" DefaultValue="-1" Name="MjId"></asp:Parameter>
                                                <asp:Parameter Type="Int32" DefaultValue="-1" Name="ResId"></asp:Parameter>
                                                <asp:Parameter Type="Int32" DefaultValue="-1" Name="CurrentGrdId"></asp:Parameter>
                                                <asp:Parameter Type="Int32" DefaultValue="1" Name="IsMemberPeriod"></asp:Parameter>
                                                <asp:Parameter Type="String" DefaultValue="" Name="RequestDate"></asp:Parameter>
                                                
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <ul style=" font-size: 8pt; line-height: 15pt; color: DarkRed">
                                            <li>در صورت گذراندن دوره آموزشی ارتقاء پایه در سایر استان های کشور از طریق لینک زیر اطلاعات و تصویر مدرک دوره را ثبت نمایید</li>
                                            <li>در صورت شرکت در همایش در سایر استان های کشور از طریق لینک زیر اطلاعات و تصویر مدرک دوره را ثبت نمایید</li>
                                          
                                        </ul>
                                         <table width="100%">
                                            <tr>
                                                <td width="5%"></td>
                                                <td  valign="top" width="20%">
                                                    <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="لیست کلیه دوره های مورد نیاز جهت ارتقاء پایه" ToolTip="لیست کلیه دوره های مورد نیاز جهت ارتقاء پایه"
                                                        CausesValidation="False" ID="btnUpGradePeriods"  UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" AutoPostBack="false" >
                                                     <ClientSideEvents Click="function(s,e){window.open('/Members/Amoozesh/UpGradePeriods.aspx');}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td   valign="middle" width="10%">
                                                </td>
                                                <td   valign="top" width="20%">
                                                 
                                                    <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ثبت گواهینامه همایش" ToolTip="ثبت گواهینامه همایش"
                                                        CausesValidation="False" ID="btnMadrakForUpGrade2"   UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False"  AutoPostBack="false">
                                                     <ClientSideEvents Click="function(s,e){window.open('/Members/Amoozesh/MadrakForUpGrade.aspx');}" />
                                                     
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td width="10%"></td>
                                                <td   width="20%">
                                                 
                                                    <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ثبت گواهینامه دوره آموزشی خارج از استان فارس" ToolTip="ثبت گواهینامه دوره آموزشی  ارتقاء پایه خارج از استان فارس"
                                                        CausesValidation="False" ID="btnMadrakForUpGrade" AutoPostBack="False" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False"  >
                                                     <ClientSideEvents Click="function(s,e){window.open('/Members/Amoozesh/MadrakForUpGrade.aspx');}" />
                                                     
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                  <td width="5%"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="right" valign="top">
                                        <br />
                                        <TSPControls:CustomAspxDevGridView2 ID="GridViewJobCon" Caption="سابقه کارهای ثبت شده در پروانه" ClientInstanceName="GridViewJobCon" runat="server" DataSourceID="ObjectDataSourceJobConfirm"
                                            Width="100%" EnableCallBacks="True" KeyFieldName="JobConfId" AutoGenerateColumns="False">
                                            <Settings ShowHorizontalScrollBar="true"></Settings>
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Position" Caption="سمت"
                                                    Name="Position">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FromDate" Caption="از تاریخ"
                                                    Name="FromDate">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ToDate" Caption="تا تاریخ"
                                                    Name="ToDate">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="JobDateDiff" Caption="تعداد سال"
                                                    Name="JobDateDiff">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="تصویر فرم تاییدیه "
                                                    FieldName="FileURL" Caption="تصویر فرم تاییدیه" Name="FileURL">
                                                </dxwgv:GridViewDataHyperLinkColumn>
                                            </Columns>
                                        </TSPControls:CustomAspxDevGridView2>
                                        <asp:ObjectDataSource ID="ObjectDataSourceJobConfirm" runat="server" SelectMethod="SelctActiveJob"
                                            TypeName="TSP.DataManager.DocMemberFileJobConfirmationManager">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="-2" DbType="Int32" Name="MeId" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                    <div align="center" dir="rtl">
                                        <asp:Label runat="server" ForeColor="Red" ID="lblError" Visible="False" Font-Size="10pt"></asp:Label>
                                    </div>
                                    <div align="right" dir="rtl"> 
                                        <br />
                                        <blink id="blkImgHasObserverWorkReq"><dxe:ASPxImage CssClass="blink" ID="ImgHasObserverWorkReq" ClientVisible="false" Width="25px" Height="25px" runat="server" ImageUrl="~/Images/Errors-64.png">
                                    </dxe:ASPxImage></blink>
                                          <asp:Label ID="lblHasObserverWorkReq" Font-Bold="true" ForeColor="DarkRed" Visible="false" runat="server">با توجه به اینکه شما قبلا آماده بکاری خود برای اخذ و انجام کار نظارت اعلام
                                             و در سامانه ثبت کرده اید در صورت تغییر در پایه و صلاحیت و تایید و صدور پروانه لازم است فرم اعلام آماده بکاری را دوباره تکمیل نمایید
                                              در غیر اینصورت ملاک اختصاص کار نظارت به شما بر اساس حدود پروانه قبل است
                                          </asp:Label>
                                        <br />
                                    </div>
                                    <div align="right" dir="rtl">
                                        <TSPControls:CustomASPxCheckBox runat="server" Text="اینجانب اعلام می نمایم سوگند نامه را مطالعه نموده ام  و با اگاهی به همه موارد فوق موافقت خود را اعلام می دارم هچنین متعهد می گردم که چارچوب تعیین شده را در طول فعالیتهای حرفه ای خود مدنظر داشته و به آن عمل نمایم."
                                            EnableClientSideAPI="True" ID="chbIAgree" ClientInstanceName="chb">
                                            <ValidationSettings CausesValidation="true" Display="Dynamic" ErrorTextPosition="Right"
                                                ErrorDisplayMode="ImageWithTooltip" ErrorText="موارد اعلام شده مورد موافقت قرار نگرفته است">
                                              
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" ErrorText="موارد اعلام شده مورد موافقت قرار نگرفته است" />
                                            </ValidationSettings>
                                        </TSPControls:CustomASPxCheckBox>
                                    </div>
                                </td>
                            </tr>

                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <div class="Item-center">
                <asp:LinkButton ID="LinkButton1" CssClass="ButtonMenue" OnClick="btnCancel_Click" runat="server" CausesValidation="false">انصراف</asp:LinkButton>
                <asp:LinkButton ID="btnSave" CssClass="ButtonMenue" OnClick="btnNext_Click" runat="server" OnClientClick="function(s,e){
                                    if (chb.GetChecked()== false){
                                      chb.SetIsValid(false);
                                        return false;
                                    }
                                    }">تایید و ادامه</asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>




