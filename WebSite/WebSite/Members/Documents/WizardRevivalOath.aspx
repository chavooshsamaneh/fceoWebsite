<%@ Page Title="درخواست تمدید پروانه اشتغال-مدارک لازم" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="WizardRevivalOath.aspx.cs" Inherits="Members_Documents_WizardRevivalOath" %>


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

                    <dxm:MenuItem Name="Kardan" Text="استعلام ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="JobConfirm" Text="تاییدیه سابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Summary" Text="خلاصه اطلاعات">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="End" Text="ثبت نهایی">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>

            <TSPControls:CustomASPxRoundPanel ID="RoundPanelMe" HeaderText="درخواست تمدید پروانه اشتغال بکار مهندسی"
                runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <fieldset>
                            <legend class="fieldset-legend" dir="rtl"><b>مدارک مورد نیاز جهت تمدید پروانه اشتغال به کار</b></legend>

                            <table width="100%">
                                <tr>
                                    <td align="right" valign="top" colspan="6">
                                        <p class="HelpUL">
                                            <b>قبل از شروع فرایند درخواست تمدید پروانه اشتغال به چه مدارکی نیاز است و چه مواردی را
                                                                        باید بررسی کرد</b>
                                        </p>
                                        <ul style="font-family=tahoma; font-size: 8pt; line-height: 15pt">
                                            <li>بررسی درستی تصویر متقاضی، تصاویر شناسنامه، مدرک پایان خدمت و شماره تلفن همراه که
                                                                        در همین صفحه نمایش داده شده اند(توضیحات بیشتر در پایین صفحه)</li>
                                            <li>بررسی درستی اطلاعات و تصویر مدارک تحصیلی(توضیحات بیشتر در پایین صفحه)</li>
                                            <li><b>سابقه کار:</b></li>

                                            <ul>
                                                
                                            <li>تهیه نامه سابقه کار از محل کار یا تکمیل فرم سابقه کار (جهت تکمیل فرم سابقه کار: از بالای صفحه، قسمت قوانین و بخشنامه ها ، فرم سابقه کار (کدفرم : 003/01 ) را انتخاب کرده و چاپ آن را تهیه کنید.
                                             (<a  target="_blank" class="blink TitleOragne" href="https://fceo.ir/Forms.aspx">لینک مستقیم آرشیو فرم ها جهت چاپ فرم سابقه کار</a> ) </li>
                                                <li>الف)در صورتی که دارای پروانه اشتغال در پایه یک می باشید ، ارائه سابقه کار نیاز نمی باشد.</li>
                                                <li>ب)در صورتی که دارای پروانه اشتغال در پایه دو یا پایه سه می باشید ، ارائه سابقه کار سه سال اخیر الزامی می باشد.</li>
                                                <li>ج)در صورتی که پروانه فعلی شما دارای اعتبار یک ساله بوده نیاز به ارائه سابقه کار نمی باشید
                                                                            <ul>
                                                                                <li>ج1)پروانه با اعتبار یکساله بدلیل عدم ارائه گواهی دوره جوش : بارگزاری گواهی دوره جوش الزامی می باشد
                                                                                </li>
                                                                                <li>ج2)پروانه اعتبار یکساله بدلیل عدم ارائه کارت پایان خدمت یا معافیت خدمت : آپلود کارت پایان خدمت یا معافیت خدمت الزامی می باشد.(جهت آپلود از طریق لینک درخواست تغییرات اطلاعات پایه عضویت اقدام نمایید)</li>
                                                                            </ul>
                                                </li>


                                            </ul>
                                            <li>در صورتی که رشته عمران می باشید ، آماده سازی تصویر گواهینامه شرکت در دوره جوش الزامی می باشد</li>
                                            <li>گذراندن دوره آموزشي HSE براي دارندگان پروانه اشتغال رشته عمران و معماري كه داراي صلاحيت اجرا مي باشند، الزامي گرديده است.براي رشته عمران تصویر آن فعلاً در قسمت مربوط به آپلود گواهي جوش ،آپلود شود اگر دوره جوش را آپلود كرده ايد اين تعهد كنار دوره جوش آپلود شود و در خصوص رشته معماري ، تصویر آن فعلاً در قسمت مربوط به آپلود مفاصاحساب مالياتي ،آپلود شود اگر مفاصاحساب مالياتي را آپلود كرده ايد اين تعهد كنار مفاصاحساب مالياتي آپلود شود.</li>                                            
                                            <li>تهیه معرفی نامه سازمان نظام مهندسی به اداره امور مالیاتی (به صفحه پرتال اعضا/لینک های پرکاربرد/چاپ نامه تسویه حساب امور مالیاتی مراجعه فرمایید)
                                            </li>
                                            <li>آماده کردن پاسخ نامه اداره امور مالیاتی جهت بارگذاری(آپلود) در سیستم</li>
                                            <li>آپلود پشت و روی اصل کارت پروانه اشتغال به کار</li>
                                        </ul>
                                        <br />
                                        <fieldset>
                                            <legend class="HelpUL" dir="rtl"><b>سابقه کار</b></legend>
                                            <p class="HelpUL">
                                                <b>ارائه سابقه کار به يکي از سه روش زير امکان پذیر است</b>
                                            </p>
                                            <ul style="font-family=tahoma; font-size: 7pt; line-height: 15pt">
                                                <li>شرکت خصوصي (سهامي خاص) داراي رتبه بندي ازسازمان مديريت و برنامه ريزي یا دارای پروانه
                                                                            حقوقی از سازمان نظام مهندسی:ارائه نامه روي سربرگ شرکت يا تکميل قسمت( الف) به همراه
                                                                            کپي رتبه بندي شرکت</li>
                                                <li>ارگان دولتي:ارائه نامه از ارگاني که در آن خدمت کرده ايد.</li>
                                                <li>درصورت عدم تهيه نامه ازمراکز عنوان شده يا فعاليت بصورت شخصي: تکميل قسمت (ب) فرم
                                                                            سابقه کار </li>
                                            </ul>
                                            <dl style="font-family=tahoma; font-size: 7pt; line-height: 15pt">
                                                <dt><b>روش تکمیل قسمت الف سابقه کار</b></dt>
                                                <dd>تکميل فرم توسط شما و امضاء و مهرشرکت به همراه کپي رتبه بندي شرکت.</dd>
                                                <dd>يا تکميل فرم توسط شما و مهر دفتر مهندسي طراحي (دفترطراحي زيرنظر نظام مهندسي) به
                                                                            همراه کپي پروانه دفترمهندسي معتبر(سابقه کار از دفتر طراحی فقط درخصوص سابقه بخش طراحی
                                                                            یا محاسبه قابل قبول می باشد)</dd>
                                                <dd>يا تکميل فرم توسط شما باامضا و مهرشرکت مهندسي داراي پروانه حقوقي ازسازمان به همراه
                                                                            کپي پروانه حقوقي که از نظام مهندسي دريافت کرده اند
                                                </dd>
                                            </dl>
                                            <dl style="font-family=tahoma; font-size: 7pt; line-height: 15pt">
                                                <dt><b>روش تکمیل قسمت ب سابقه کار</b></dt>
                                                <dd>تکميل فرم توسط شما وامضاء و مهرنظام مهندسي دو نفرمهندس با بيش ازده سال سابقه کار.
                                                                            (ده سال ازمدرک تحصيلي ليسانس تایید کنندگان گذشته باشد و حتماً داراي پروانه اشتغال
                                                                            معتبر باشند.) پايه پروانه اشتغال وهم رشته بودن دونفرمهندس تائيد کننده سابقه کار با
                                                                            رشته متقاضي مد نظر نمي باشد.</dd>
                                                <dd>
                                                    <b>در صورتی که دو مهندس تایید کننده سابقه کار شما از اعضای استان دیگری (غیر از استان فارس) می باشند، تاریخ فارغ التحصیلی و تاریخ اعتبار پروانه اشتغال این دو شخص در فرم سابقه کار توسط سازمان نظام مهندسی همان استان تکمیل شده و با امضا و مهر مسئول مربوطه در آن سازمان تائید گردد. </b>
                                                </dd>
                                            </dl>

                                            <p class="HelpUL">
                                                <b>نکاتی که در تهیه و ارائه سابقه کار باید رعایت نمود</b>
                                            </p>
                                            <ul style="font-family=tahoma; font-size: 7pt; line-height: 15pt">

                                                <li>نامه سابقه کار سه سال اخیر جهت افرادی که دارای پایه دو و یا سه می باشند،الزامی می باشد(تنها متقاضیان تمدید پروانه در پایه یک نیاز به ارائه سابقه کار ندارند). </li>
                                                <li>تاريخ شروع و پايان سابقه کار دقیقا ذکر گردد. </li>
                                                <li>سابقه کار مي بايستي به روز (حتی الامکان تاکنون) ارائه گردد</li>
                                                <%--  <li>نوع سابقه کاری و فعاليت، مربوط به رشته تحصيلي و بخش قبول شده درآزمون درنامه سابقه
                                                                            کار مشخص شود. برای نمونه: در صورت قبولي دربخش محاسبات عمران ارائه سابقه کار در زمينه
                                                                            محاسبات باشد، یا در صورت قبولي دربخش نظارت معماري ارائه سابقه کار در زمينه نظارت
                                                                            معماري باشد </li>--%>

                                                <li>ارائه بيمه نياز نمي باشد</li>
                                                <li>آپلود اصل نامه سابقه کار یا اصل فرم تکمیل شده سابقه کار (آپلود کپی نامه یا فرم مورد قبول نمی باشد)</li>
                                                <li>آپلود کپی رتبه بندی شرکت خصوصی(سهامی خاص) یا کپی پروانه دفتر مهندسی طراحی یا پروانه شرکت حقوقی(زیر تظر نظام مهندسی) بر اساس نوع سابقه کار ارائه شده</li>
                                            </ul>

                                        </fieldset>

                                        <p class="HelpUL">
                                            درصورت هرگونه کسری در مدارک روند تایید درخواست تمدید پروانه شما با تاخیر مواجه می
                                                                    گردد و سازمان در قبال تاخیر پیش آمده هیچگونه مسئولیتی نخواهد داشت
                                        </p>
                                        <p class="HelpUL">
                                            <b>توجه! </b>
                                        </p>
                                        <p style="font-family=tahoma; font-size: 8pt; line-height: 15pt">
                                            از آنجاییکه کلیه مراحل درخواست تمدید پروانه اشتغال و یا نواقص پروانه ازطریق پیامک به شما اطلاع
                                                                    داده می شود. نسبت به بروزرسانی شماره همراه خود به روش توضیح داده شده اقدام کنید
                                                                    درغیراینصورت این سازمان، هیچ گونه مسئولیتی درقبال عدم دریافت پیامکها، نخواهد شد.
                                                                    در ضمن در صورت غیر فعال کردن پیامک های تبلیغاتی، پیامک سازمان به تلفن همراه اعضا می رسد.
                                        </p>
                                        <p class="HelpUL">
                                            در صورت نیاز به تغییر تصویر متقاضی
                                        </p>
                                        <p style="font-family=tahoma; font-size: 8pt; line-height: 15pt">
                                            از داخل همین صفحه لینک تغییرات اطلاعات پایه را انتخاب کنید و در داخل صفحه ای که
                                                                    باز می شود عکس 4×3 <a style="color: DarkRed"><b>جدید </a><a style="color: DarkRed">رنگی(حداکثرمربوط
                                                                    به سه سال پیش و متفاوت از پروانه های قبلی </b></a>) آقایان : عکس با زمينه سفيد، پیراهن یقه دار (تی شرت مورد قبول نیست)،بدون
                                                                    عينک، کراوات و دیگر تزئینات خانمها: قرص صورت مشخص و با حجاب کامل (مقنعه)
                                        </p>
                                        <p class="HelpUL">
                                            درصورت نیاز به تغییرات در اطلاعات شناسنامه
                                        </p>
                                        <p style="font-family=tahoma; font-size: 8pt; line-height: 15pt">
                                            از درون همین صفحه لینک تغییرات اطلاعات پایه را انتخاب و در صفحه ایی که باز می شود
                                                                    صفحه توضیحات شناسنامه (در صورت وجود توضیحات) می بایست در قسمت پرتال شخصی شما درسایت نظام مهندسی بارگزاری شود.
                                        </p>
                                        <p class="HelpUL">
                                            در صورت نیاز به تغییر شماره تلفن همراه خود به صورت زیر عمل کنید
                                        </p>
                                        <p style="font-family=tahoma; font-size: 8pt; line-height: 15pt">
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
                                            <emptyimage height="75px" width="75px" url="~/Images/Person.png">
                                                                    </emptyimage>
                                        </dxe:ASPxImage>
                                    </td>
                                    <td align="center" valign="top" colspan="5" width="70%">
                                        <table width="100%">
                                            <tr>
                                                <td id="Td48" runat="server" align="right" valign="top" width="50%">تصویر شناسنامه:
                                                </td>
                                                <td align="right" valign="top" width="50%">
                                                    <dxe:ASPxHyperLink ID="HpIdNo" runat="server" ClientInstanceName="hidno" Target="_blank"
                                                        Text="تصویر شناسنامه">
                                                    </dxe:ASPxHyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">تصویر کارت ملی:
                                                </td>
                                                <td align="right" valign="top">
                                                    <dxe:ASPxHyperLink ID="HpSSN" runat="server" ClientInstanceName="hssn" Target="_blank"
                                                        Text="تصویر کارت ملی">
                                                    </dxe:ASPxHyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <br />
                                                    </td>
                                                </tr>
                                                <td align="right" valign="top">
                                                    <dxe:ASPxLabel ID="lblSoldire" ClientVisible="false" runat="server" Text="تصویر کارت پایان خدمت:"
                                                        ClientInstanceName="lblSoldire">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td align="right" valign="top" dir="rtl">
                                                    <dxe:ASPxHyperLink ID="HpSoldire" runat="server" ClientInstanceName="hssn" Target="_blank"
                                                        Text="تصویر کارت پایان خدمت">
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
                                        <p style="font-family=tahoma; font-size: 8pt; line-height: 15pt; color: DarkRed">
                                            <b style="color: darkorange">در صورتی که اطلاعات مدارک تحصیلی شما صحیح نمی باشد ،</b>
                                            از طریق لینک 'درخواست تغییرات
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
                                                <td align="left" valign="top" width="5%">
                                                    <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="درخواست تغییرات اطلاعات پایه" ToolTip="درخواست تغییرات اطلاعات پایه"
                                                        CausesValidation="False" ID="btnChangeBaseInfo" AutoPostBack="False" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" OnClick="btnChangeBaseInfo_Click">

                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td align="right" valign="middle" width="30%">
                                                </td>
                                                <td width="10%"></td>
                                                <td align="left" valign="top" width="5%">
                                                    <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="درخواست تغییرات مدرک تحصیلی" ToolTip="درخواست تغییرات مدرک تحصیلی"
                                                        CausesValidation="False" ID="btnLicenceRequest" AutoPostBack="False" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" OnClick="btnLicenceRequest_Click">
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td align="right" valign="middle" width="30%">
                                                </td>
                                            </tr>
                                        </table>
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
                                        <TSPControls:CustomASPxCheckBox  runat="server" Text="اینجانب اعلام می نمایم تعهد نامه را مطالعه نموده ام  و با اگاهی به همه موارد فوق موافقت خود را اعلام می دارم هچنین متعهد می گردم که چارچوب تعیین شده را در طول فعالیتهای حرفه ای خود مدنظر داشته و به آن عمل نمایم."
                                            EnableClientSideAPI="True" ID="chbIAgree" ClientInstanceName="chb">
                                            <ValidationSettings CausesValidation="true" Display="Dynamic" ErrorTextPosition="Right"
                                                ErrorDisplayMode="ImageWithTooltip" ErrorText="موارد اعلام شده مورد موافقت قرار نگرفته است">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
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
            <TSPControls:CustomAspxButton  CssClass="ButtonMenue" runat="server"  UseSubmitBehavior="False" CausesValidation="False"
                Text="انصراف" EnableTheming="False" ToolTip="انصراف"
                ID="btnCancel" EnableViewState="False" OnClick="btnCancel_Click" Visible="true">
            </TSPControls:CustomAspxButton>

            <TSPControls:CustomAspxButton  CssClass="ButtonMenue" ID="btnNext" OnClick="btnNext_Click" runat="server" Text="تایید و ادامه" UseSubmitBehavior="false"
                EnableTheming="False" EnableViewState="False" ToolTip="تایید و ادامه"
                ClientInstanceName="btnNext">

                <ClientSideEvents Click="function(s,e){
                                    if (chb.GetChecked()== false){
                                      e.processOnServer=false;
                                      chb.SetIsValid(false);
                                    }
                                    }" />
            </TSPControls:CustomAspxButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



