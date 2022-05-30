<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="WizardOffice_Membership.aspx.cs" Inherits="NezamRegister_WizardOffice_Membership"
    Title="عضویت حقوقی - چهارچوب شئون حرفه ای مهندسی" %>

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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1" Namespace="DevExpress.Web"
    TagPrefix="dxe" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelNezamRegister" runat="server">
        <ContentTemplate>
            <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server">
                <Items>
                    <dxm:MenuItem Text="چهارچوب شئون حرفه ای" Name="Membership" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مشخصات شرکت" Name="Office">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="شعبه ها" Name="Agent">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="اعضای شرکت" Name="Member">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="آگهی های رسمی" Name="Letter">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="خلاصه اطلاعات" Name="Summary">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="ثبت نهایی" Name="End">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>

            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="عضویت حقوقی" runat="server">
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
                                <ol dir="rtl" align="right">
                                    <li>تصویر آرم شرکت</li>
                                    <li>تصویر مهر شرکت</li>
                                    <li>تصویر اعضا (در صورتیکه عضو سازمان نباشند)</li>
                                </ol>
                                <br />
                                <b>مراحل ثبت نام :</b>
                                <ol dir="rtl" align="right">
                                    <li>ثبت مشخصات شرکت </li>
                                    <li>ثبت شعبه های شرکت (در صورت وجود)</li>
                                    <li>ثبت اعضای شرکت (اعضای شرکت حداقل باید 3 نفر باشند که شامل یک نفر به عنوان مدیر عامل
                                                                و 2 نفر عضو هیئت مدیره)</li>
                                    <li>ثبت سوابق کاری شرکت (در صورت وجود)</li>
                                    <li>ثبت آگهی های درج شده در روزنامه رسمی مرتبط با شرکت</li>
                                </ol>
                                <br />
                                <b>پس از پایان عملیات عضویت به صورت آنلاین، فقط 10 روز مهلت خواهید داشت تا مدارک زیر
                                                            را به سازمان تحویل دهید :</b>


                                <asp:Label Visible="false" runat="server" ID="lblMemberShipCost"></asp:Label>

                                <asp:Label runat="server" ID="lblYearMemberShipCost" Visible="false"></asp:Label>
                                <ol dir="rtl" align="right">

                                    <li>با ورود به سايت سازمان www.fceo.ir ، قسمت قوانين و بخشنامه ها ، آرشيو فرمها ، فرم عضويت حقوقي به شماره كد 00/008 را از سايت پرينت گرفته و با دردست داشتن مدارك به واحد عضويت سازمان مراجعه كنند.
                                    </li>
                                </ol>
                            </div>
                            <div class="Item-center">
                                <asp:Label runat="server" ForeColor="Red" ID="lblError" Visible="False" Font-Size="10pt"></asp:Label>
                            </div>
                            <br />
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
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" ID="btnNext" OnClick="btnNext_Click" runat="server" Text="تایید و ادامه" UseSubmitBehavior="true"
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

                        </fieldset>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        AssociatedUpdatePanelID="UpdatePanelNezamRegister" BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div style="font-family: Tahoma; font-size: 9pt; text-align: center; padding-top: 25px; width: 300px; height: 41px; background-image: url(../Images/UploadBg.png);">
                <img id="IMG1" src="../Images/indicator.gif" align="middle" />
                لطفا صبر نمایید ...
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
