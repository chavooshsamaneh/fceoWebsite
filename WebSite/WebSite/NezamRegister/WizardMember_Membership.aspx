<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="WizardMember_Membership.aspx.cs" Inherits="NezamRegister_WizardMember_Membership"
    Title="عضویت حقیقی - چهارچوب شئون حرفه ای مهندسی" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server">
        <Items>
            <dxm:MenuItem Text="چهارچوب شئون حرفه ای" Name="Membership" Selected="true">
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
            <dxm:MenuItem Name="End" Text="ثبت نهایی">
            </dxm:MenuItem>
        </Items>

    </TSPControls:CustomAspxMenuHorizontal>
    <br />

    <TSPControls:CustomASPxRoundPanel ID="RoundPanelMe" HeaderText="عضویت حقیقی" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <fieldset>
                    <legend class="HelpUL">چهارچوب شئون حرفه ای مهندسی</legend>
                    <div align="right" dir="rtl">
                        <ol style="font-family=tahoma; font-size: 8pt; line-height: 15pt">
                            <li><b>در رابطه با حرفه ی مهندسان</b>
                                <br />
                                <ul type="disc">
                                    <li>آشنایی با دستاوردهای علمی، فنی و حرفه ای روز</li>
                                    <li>رعایت ضوابط و مقررات ساختمانی</li>
                                    <li>رعایت ضوابط و مقررات طرح های جامع و هادی</li>
                                    <li>انتقال دانش فنی و تجربیات حرفه ای به همکاران و مشارکت در فعالیت های اجتماعی حرفه
                                                                        ای</li>
                                    <li>استفاده از تجربیات و دستاوردهای دیگران با ذکر ماخذ</li>
                                    <li>رعایت مصالح همکاران و رقابت سالم حرفه ای </li>
                                    <li>رعایت بی طرفی کامل درصورت ارجاع حکمیت یا کارشناسی حرفه ای</li>
                                    <li>عدم اشتغال در حرفه های مهندسی خارج از صلاحیت تعیین شده بر اساس پروانه ی اشتغال</li>
                                </ul>
                                <br />
                            </li>
                            <li><b>در رابطه با کارفرما</b>
                                <br />
                                <ul type="disc">
                                    <li>حفظ اسرار کارفرما در رابطه با خدمات مهندسی مورد درخواست</li>
                                    <li>اجرای تعهدات قراردادی</li>
                                    <li>راهنمایی کارفرما در مواردی که منافع او با اجتماع و حرفه در تضاد است.</li>
                                </ul>
                                <br />
                            </li>
                            <li><b>در رابطه با جامعه</b>
                                <br />
                                <ul type="disc">
                                    <li>رعایت مصالح جامعه بر اساس قوانین دولت ایران</li>
                                    <li>رعایت حفظ محیط زیست</li>
                                </ul>
                            </li>
                        </ol>
                    </div>
                </fieldset>
                <fieldset>
                    <legend class="HelpUL">عضویت</legend>
                    <div dir="rtl" align="right" style="line-height: 15pt; font-family: Tahoma; font-size: 8pt">
                        <b>مدارک مورد نیاز برای ثبت نام آنلاین (بارگذاری برروی سایت) :</b>
                        <br />
                        <b style="color: darkviolet">اصل شناسنامه ، اصل كارت ملي ، اصل كارت پايان خدمت  ، (به صورت رنگي ) اسكن شود.(عكس برداري توسط تلفن همراه قابل قبول نمی باشد.)</b>
                        <ol dir="rtl" align="right">
                            <li>تصویر شخص
                                                                <div align="right" dir="rtl">
                                                                    <asp:Label runat="server" ID="lblImg">
                                                                    </asp:Label>
                                                                </div>
                            </li>
                            <li>تصویر رنگی شناسنامه و در صورت لزوم توضیحات </li>
                            <li>تصویر رنگی پشت و روی کارت ملی</li>
                            <li>تصویر رنگی پشت و روی کارت پایان خدمت یا هر نامه یا مدرکی که وضعیت خدمت را مشخص می کند(برای آقایان)</li>
                            <li>نامه عدم عضویت در نظام کاردانی در صورت داشتن مدرک کارشناسی ناپیوسته</li>
                            <li>تصویر نامه انتقالی (در صورتیکه از استان دیگری منتقل شده اید)</li>
                            <li>تصویر رنگی مدرک تحصیلی</li>
                            <%--<li>يك مدرك دال بر اينكه حداقل شش ماه ساكن شهر موردنظر خود (شهري كه به عنوان محل عضويت انتخاب كرده ايد) در استان فارس بوده ايد مانند ( تصوير سند منزل يا اجاره نامه رسمي يا سابقه بيمه تامين اجتماعي اسكن شود در غيراينصورت مداركي مانند تائيديه شوراي شهر يا شوراي محل) :--%>
                            <li>يك مدرك دال بر اينكه شش ماه ساكن شهر منتخب خود بعنوان محل عضويت بوده اید (شهري كه بعنوان محل عضويت در سايت انتخاب كرده ايد- دفتر نماندگی) اسكن نمائيدکه يكي از موارد زير می باشد:
                                <ul>
                                    <li>الف - سند مالكيت (بنام خودتان يا كسي كه نام او در شناسنامه شما مي باشد)</li>
                                    <li>ب- اجاره نامه رسمي معتبر (محضري با كد رهگيري) (بنام خودتان يا كسي كه نام او در شناسنامه شما مي باشد)</li>
                                    <li>ج- نامه اي تهيه گردد و در آن آدرس محل سكونت خود را قيد نموده و پس از تائيد چند نفر از كساني كه شما را مي شناسند، اين نامه به تائيد مسجد محله خود رسانده و مهر شود
                                    </li>
                                </ul>                             
                            </li>
                        </ol>
                        <br />
                        <b>مراحل ثبت نام :</b>
                        <ol dir="rtl" align="right">
                            <li>ثبت مشخصات فردی</li>
                            <li>ثبت مدارک تحصیلی</li>
                            <li>ثبت سوابق کاری (در صورت وجود)</li>
                            <li>ثبت فعالیت ها (در صورت وجود)</li>
                            <li>ثبت زبان های خارجی (در صورت آشنایی با زبان های خارجی)</li>
                            <li>مشاهده خلاصه اطلاعات و اطمینان از صحت اطلاعات وارد نموده</li>
                            <li>ثبت نهایی و <b>پرداخت حق عضویت با استفاده از درگاه الکترونیکی 
                            </b></li>
                        </ol>
                        <b>نکات مهم هنگام ثبت نام :</b>
                        <ul>

                            <li>هنگام ثبت اطلاعات توجه نمایید درصورت ورود اطلاعات ناقص و یا نادرست (بخصوص عدم مطابقت
                                                                عنوان مدرک تحصیلی انتخاب شده با عنوان مدرک درج شده بر روی دانشنامه شما) عواقب بعدی
                                                                این عمل به عهده ی شما می باشد و مبلغ واریز شده به حساب به هیچ عنوان قابل بازگشت
                                                                نمی باشد. </li>
                            <li>در روند ثبت نام به هیچ عنوان از دکمه بازگشت مرورگر استفاده ننمایید.
                            </li>
                            <li>پیش از شروع به ثبت نام از آنجا که  پرداخت هزينه ثبت نام به مبلغ
                                  <asp:Label runat="server" ID="lblMemberShipCost"></asp:Label>
                                ،صرفاً در همین سامانه از طریق درگاه الکترونیکی بانک و داشتن رمز پويا  
                               
                                میسر است، از در دسترس بودن اطلاعات کارت بانک عضو شتاب خود مطمئن گردید.
                            </li>

                        </ul>
                        <br />
                        <p><b>نکات مهم بعد از ثبت نام :</b></p>
                        <ul>
                            <li>پس از پايان عمليات ثبت نام عضويت بصورت آنلاين، درخواست شما ظرف مدت 48 ساعت كاري توسط کارشناسان واحد عضویت و پروانه سازمان بررسي مي شود و پس از آن پيامكی از طرف سازمان به مضمون زیر برای شما ارسال می گردد.
                                <br />
                                <i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
"مدارك آپلود شده در سيستم بررسي و مورد تاييد قرار گرفته است لطفا جهت ارائه مدارك عضويت (ليست زير) و دريافت شماره عضويت دائمي، به سازمان نظام مهندسي ساختمان شماره 1، واحد عضويت و پروانه اشتغال مراجعه فرمائيد" 
                                </i>
                            </li>
                            <li>پس از دریافت این پیامک شما حداكثر 90 روز مهلت داريد مدارك عضويت (ليست زير) را به واحد عضويت و پروانه اشتغال تحويل دهيد. درصورت گذشت بيش از 90 روز از درخواست و عدم مراجعه به سازمان، مي بايستي مجدداً درخواست عضويت الکترونیکی را ثبت نمایید.</li>
                            <li>در صورت عدم دریافت پیامک پس از 72 ساعت از ثبت درخواست با واحد عضویت سازمان تماس بگیرید</li>
                        </ul>
                        <ol dir="rtl" align="right">
                            <li>اصل مدرک تحصیلی و دو نسخه تصویر برابر اصل شده آن </li>
                            <li>اصل و تصویر برابر اصل شده شناسنامه (صفحه اول و توضیحات)</li>
                            <li>اصل و کپی برابر اصل شده کارت ملی</li>
                            <li>اصل و تصویر برابر اصل شده کارت پایان خدمت یا هر نامه یا مدرکی که وضعیت خدمت را مشخص می کند</li>
                            <%--<li>اصل نامه عدم عضویت در نظام کاردانی در صورت داشتن مدرک کارشناسی ناپیوسته</li>--%>              
                        </ol>

                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td align="center">
                                        <asp:LinkButton ID="btnHelp2" Font-Bold="true" runat="server" Font-Size="13px" ForeColor="DarkBlue"
                                            OnClientClick="ShowHelpWindow(HiddenHelp.Get('HelpAddress'))" CssClass="LinkUnderlineOnHover"
                                            Text="راهنمای ثبت نام کلیک نمایید" CausesValidation="false"></asp:LinkButton>
                                        <dx:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
                                        </dx:ASPxHiddenField>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div align="center" dir="rtl">
                        <asp:Label runat="server" ForeColor="Red" ID="lblError" Visible="False" Font-Size="10pt"></asp:Label>
                    </div>
                    <div class="row" align="right" dir="rtl">
                        <TSPControls:CustomASPxCheckBox runat="server" Text="راهنمای ثبت نام را مطالعه نموده ام" EnableClientSideAPI="True"
                            ID="chbIAgreeHelp" ClientInstanceName="chb">
                            <ValidationSettings CausesValidation="true" Display="Dynamic" ErrorTextPosition="Right"
                                ErrorDisplayMode="ImageWithTooltip" ErrorText="موارد اعلام شده مورد موافقت قرار نگرفته است">
                                <RequiredField IsRequired="true" ErrorText="موارد اعلام شده مورد موافقت قرار نگرفته است" />
                            </ValidationSettings>
                        </TSPControls:CustomASPxCheckBox>
                    </div>
                    <div class="row">
                        <TSPControls:CustomASPxCheckBox runat="server" Text="اینجانب با اگاهی به همه موارد فوق موافقت خود را اعلام می دارم هچنین متعهد می گردم که چارچوب تعیین شده را در طول فعالیتهای حرفه ای خود مدنظر داشته و به آن عمل نمایم."
                            EnableClientSideAPI="True" ID="chbIAgree" ClientInstanceName="chb">
                            <ValidationSettings CausesValidation="true" Display="Dynamic" ErrorTextPosition="Right"
                                ErrorDisplayMode="ImageWithTooltip" ErrorText="موارد اعلام شده مورد موافقت قرار نگرفته است">

                                <RequiredField IsRequired="true" ErrorText="موارد اعلام شده مورد موافقت قرار نگرفته است" />
                            </ValidationSettings>
                        </TSPControls:CustomASPxCheckBox>
                    </div>
                    <div class=" Item-center">

                        <TSPControls:CustomASPxCaptcha ID="Captcha" runat="server" Width="100%">
                        </TSPControls:CustomASPxCaptcha>

                    </div>
                    <div class="Item-center">
                        <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  ID="btnNext" OnClick="btnNext_Click" runat="server" Text="تایید و ادامه" UseSubmitBehavior="false"
                            EnableTheming="False" EnableViewState="False" ToolTip="تایید و ادامه"
                            ClientInstanceName="btnNext">
                        </TSPControls:CustomAspxButton>
                    </div>
                </fieldset>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>

</asp:Content>
