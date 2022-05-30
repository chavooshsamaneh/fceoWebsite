<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Src="~/UserControl/Poll_ValidPollListUserControl.ascx" TagName="Poll_ValidPollListUserControl"
    TagPrefix="TSP" %>

<%--<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>

    <div class="panel panel-danger" runat="server" id="RoundPanelMessage">
        <div class="panel-heading">هشدار</div>
        <div class="panel-body">
            <asp:Label ID="lblMessage" runat="server" Text="سایت در حال بروز رسانی است" Font-Bold="true"
                ForeColor="DarkBlue"></asp:Label>
        </div>
    </div>
    <%--  <div class="AccessOldWebsite">
        <a href="http://fceo.ir:8210" target="_blank" id="linkoldWebsite" runat="server">دسترسی به نسخه قدیم وب سایت سازمان نظام مهندسی استان فارس</a>
    </div>--%>
    <div class="container top-content">
        <div class="col-md-9">
            <%--***Slider****--%>
            <div id="slider" class="row ">
                <div id="BanerCarousel" class="carousel slide" style="width: 100%" data-ride="carousel">

                    <!-- Wrapper for slides -->
                    <div class="carousel-inner" role="listbox">
                        <asp:Repeater runat="server" ID="Repeater1" DataSourceID="ObjectDataSourceHomeImage">
                            <ItemTemplate>
                                <div class='<%# Container.ItemIndex == 0 ? "item active" : "item" %>'>

                                    <div class="hovereffect2">
                                        <img class="img-responsive" id="ImageNews" runat="server" src='<%# Bind("Imageurl") %>' alt="" />
                                        <div class="overlay3">
                                            <h2 class="<%# Eval("classDes")%>"><%# Eval("Description")%></h2>
                                            <span class="<%# Eval("classLink")%>"><a id="linkURL" runat="server" href='<%# Bind("LinkURL") %>'>مشاهده</a> </span>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                    </div>

                    <!-- Left and right controls -->
                    <a class="left carousel-control" href="#BanerCarousel" data-slide="prev">
                        <span class="glyphicon glyphicon-chevron-left"></span>

                    </a>
                    <a class="right carousel-control" href="#BanerCarousel" data-slide="next">
                        <span class="glyphicon glyphicon-chevron-right"></span>

                    </a>
                </div>
            </div>
            <asp:ObjectDataSource ID="ObjectDataSourceHomeImage" runat="server" SelectMethod="SelectSiteSlider"
                TypeName="TSP.DataManager.SiteImageManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
        </div>
        <div class="col-md-3">
            <div class="row searchArea">
                <div class="row">
                    <div class="row tv-icon">
                        <a title="اخبار و رویدادها" target="_blank" href="https://www.aparat.com/user/video/user_list/userid/5874899/usercat/645607">
                            <img class="img-responsive img-center img-topRadius" src="/Image/HomePage/Icons/tv.png" /></a>
                    </div>
                    <div class="row searchNews">
                        <div class="flexsearch">
                            <div class="flexsearch--wrapper">

                                <div class="flexsearch--input-wrapper">
                                    <input id="txtSearchNews" runat="server" class="flexsearch--input" type="text" value="" title="با وارد کردن کلید واژه مناسب اطلاعات مورد نظر خود را بیابید" placeholder="واژه مورد نظر را جستجو کنید ..." onkeyup="SearchKeyPress(event)" />
                                    <input id="btnSearchNews" runat="server" onserverclick="btnSearchNews_ServerClick" class="flexsearch--submit" type="button" value="&#10140;" />
                                </div>


                            </div>
                        </div>
                    </div>
                    <div class="row titleSec">ما را در شبکه های اجتماعی دنبال کنید</div>
                    <div class="row headerLink">
                        <%--    <div class="LinkItemsSocial">
                            <a title="Telegram" target="_blank" href="https://t.me/joinchat/AAAAAER1KYYCbALhcU_7BA">
                                <img src="/Images/icons/social/telegram-36g.png" /></a>
                        </div>--%>
                        <div class="LinkItemsSocial">
                            <a title="اینستاگرام" target="_blank" href="https://instagram.com/fars_nezam">
                                <img src="/Images/icons/social/instagram-36g.png" /></a>
                        </div>
                        <div class="LinkItemsSocial">
                            <a title="آپارات" target="_blank" href="https://www.aparat.com/nezam_mohandesi">
                                <img src="/Images/icons/social/aparat-36g.png" /></a>
                        </div>

                        <div class="LinkItemsSocial">
                            <a title="اپلیکیشن اطلاع رسان مهندسی(ارم)" target="_blank" href="https://fceo.ir/NewsShow.Aspx?NewsId=usOviaPkGBmREjYB9Sp+NA==">
                                <img src="/Images/icons/social/Eram-36g.png" /></a>
                        </div>

                    </div>

                    <div class="row titleSec">با ما در تماس باشید</div>

                    <div class="row headerLink">
                        <div class="LinkItemsSocial">
                            <a title="فید خبری" href="/NewsRssList.aspx">
                                <img src="/Images/icons/social/rss-36g.png" /></a>
                        </div>

                        <div class="LinkItemsSocial">
                            <a title="موقعیت مکانی در نقشه" target="_blank" href="https://www.google.com/maps/d/viewer?mid=1_lBJJru1b6Fp6gntADjJ-rogG4s&ll=29.625399391052092%2C52.49301373958588&z=19">
                                <img src="/Images/icons/social/loca-36g.png" /></a>
                        </div>

                        <div class="LinkItemsSocial">
                            <a title="سوالات متداول" href="/Faq.aspx">
                                <img src="/Images/icons/social/faq-36g.png" /></a>
                        </div>
                        <div class="LinkItemsSocial">
                            <a title="آمار بازدید سایت" href="/WebsiteStatistics.aspx">
                                <img src="/Images/icons/social/statis-36g.png" /></a>
                        </div>
                        <div class="LinkItemsSocial">
                            <a title="پیوندها" href="/Links.aspx">
                                <img src="/Images/icons/social/share-36g.png" /></a>
                        </div>
                        <div class="LinkItemsSocial">
                            <a title="تماس با سازمان" href="/ContactUs.aspx">
                                <img src="/Images/icons/social/tel-36g.png" /></a>
                        </div>
                    </div>

                    <div class="row TraceDoc">
                        <div class="flexTrace">
                            <div class="flexTrace--wrapper">

                                <div class="flexTrace--input-wrapper">
                                    <input id="txtDocumentTrace" runat="server" class="flexTrace--input" type="text" title="اصالت اسناد صادره را بیازمایید" placeholder="اصالت اسناد صادره را بیازمایید ..." onkeyup="TraceKeyPress(event)" />
                                    <input class="flexTrace--submit" type="button" value="&#10140;" id="btnDocumentTrace" runat="server" onserverclick="btnDocumentTrace_ServerClick" />
                                </div>


                            </div>
                        </div>
                    </div>

                    <%--    <div class="row Period">
                      <div class="flexsearch">
                        <div class="flexsearch--wrapper">

                            <div class="flexsearch--input-wrapper">
                                <input class="flexsearch--input" type="search" placeholder="دوره های آموزشی را جستجو کنید ..." />
                            </div>
                            <input class="flexsearch--submit" type="submit" value="&#10140;" />

                        </div>
                    </div>
                </div>--%>
                </div>
            </div>

        </div>
    </div>
    <%--***Services****--%>
    <div class="container servicecontainer">
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="me-centerd flex-center box darkblue">
                <div class="service-wrapper bubbleMembership">
                    <img class="img-responsive img-center img-topRadius" src="/Image/HomePage/Icons/Membership.png" />
                    <span>خدمات عضویت</span>
                </div>
                <div class="darkblueline service-wrapper-text">
                    <ul class="serviceulcolor">
                        <li><a target="_blank" href="/NezamRegister/Membership.aspx" style="padding: 0px">ثبت نام عضویت حقیقی در سازمان</a></li>
                        <li><a target="_blank" href="/NezamRegister/Membership.aspx" style="padding: 0px">ثبت نام عضویت حقوقی در سازمان</a></li>
                        <li><a target="_blank" href="/MembersInfo/Members.aspx" style="padding: 0px">جستجو اعضای حقیقی</a></li>
                        <li><a target="_blank" href="/MembersInfo/Office.aspx" style="padding: 0px">جستجو اعضای حقوقی</a></li>
                        <li><a target="_blank" href="/Epayment/DebtPayment.aspx" style="padding: 0px">استعلام و پرداخت آنلاین بدهی قطع عضویت</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="me-centerd flex-center box darkGreen">
                <div class="service-wrapper bubbleAmoozesh">
                    <img class="img-responsive img-center img-topRadius" src="/Image/HomePage/Icons/Amoozesh.png" />
                    <span>خدمات آموزش</span>
                </div>
                <div class="darkGreenline service-wrapper-text">
                    <ul class="serviceulcolor">
                        <li><a href="#" runat="server" id="linkLMSHelp" target="_blank" style="padding: 0px">راهنمای ورود به صفحه وب کنفرانس</a></li>
                        <li><a href="#" runat="server" id="linkSchedule" target="_blank" style="padding: 0px">برنامه آموزشی دوره های ورود به حرفه و ارتقاء</a></li>
                        <li><a target="_blank" href="/Members/Amoozesh/PeriodRegister.aspx" style="padding: 0px">ثبت نام دوره های آموزشی</a></li>
                        <li><a target="_blank" href="/Members/Amoozesh/PeriodRegister.aspx" style="padding: 0px">ثبت نام دوره غیر مصوب و سمینار آموزشی</a></li>
                        <li><a target="_blank" href="/Period.aspx" style="padding: 0px">دوره ها وسمینار های آموزشی در حال برگزاری</a></li>
                        <li><a target="_blank" href="/Learning/Course.aspx" style="padding: 0px">واحدهای درسی مورد تایید نظام مهندسی</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="me-centerd flex-center box Violet">
                <div class="service-wrapper bubbleServices">
                    <img class="img-responsive img-center img-topRadius" src="/Image/HomePage/Icons/certificate.png" />
                    <span>خدمات پروانه اشتغال</span>
                </div>
                <div class="Violetline service-wrapper-text">
                    <ul class="serviceulcolor ">
                        <li><a runat="server" id="btnNewDoc" onserverclick="btnNewDoc_ServerClick" style="padding: 0px">صدور پروانه اشتغال به کار</a></li>
                        <li><a runat="server" id="btnQualification" onserverclick="btnQualification_ServerClick" style="padding: 0px">درج صلاحیت جدید در پروانه اشتغال به کار</a></li>
                        <li><a runat="server" id="btnRevival" onserverclick="btnRevival_ServerClick" style="padding: 0px">تمدید پروانه اشتغال به کار</a></li>
                        <li><a href="#" onclick="ShowHelpWindow('../../Help/DocumentFiles/NewDocHelp.pdf')" style="padding: 0px">راهنمای صدور پروانه اشتغال</a></li>
                        <li><a href="#" onclick="ShowHelpWindow('../../Help/DocumentFiles/DocQualificationHelp.pdf')" style="padding: 0px">راهنمای درج صلاحیت جدید در پروانه اشتغال</a></li>
                        <li><a href="#" onclick="ShowHelpWindow('../../Help/DocumentFiles/HelpdocRevival.pdf')" style="padding: 0px">راهنمای تمدید پروانه اشتغال</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="me-centerd flex-center box Pink">
                <div class="service-wrapper bubbleSubSystem">
                    <img class="img-responsive img-center img-topRadius" src="/Image/HomePage/Icons/SubSystem.png" />
                    <span>سایر خدمات</span>
                </div>
                <div class="Pinkline service-wrapper-text">
                    <ul class="serviceulcolor">
                        <li><a target="_blank" href="/PriceArchive.aspx" style="padding: 0px">تعرفه خدمات مهندسی طراحی و نظارت</a></li>
                        <%--<li><a target="_blank" href="https://esys.fceo.ir/" style="padding: 0px">سامانه افتتاح ظرفیت شیراز و صدرا</a></li>--%>
                        <li><a target="_blank" href="/Forms.aspx" style="padding: 0px">آرشیو فرم ها</a></li>

                        <li><a target="_blank" href="https://debt.fceo.ir" style="padding: 0px">پرداخت آنلاین بدهی عضویت حقیقی</a></li>
                        <li><a target="_blank" href="https://loan.fceo.ir" style="padding: 0px">پرداخت آنلاین بدهی وام</a></li>
                        <li><a target="_blank" id="linkWelfareSchedule" runat="server" href="#" style="padding: 0px">لیست برنامه‌ های فرهنگی، رفاهی و ورزشی</a></li>

                        <%--  <asp:Repeater runat="server" ID="RepeaterSubSystem" DataSourceID="ObjDataSourceSubSystem">
                            <ItemTemplate>
                                <li><a target="_blank" style="padding: 0px" href='<%# Eval("SubSysLink") %>'><%# Eval("SubSysName") %></a></li>
                            </ItemTemplate>
                        </asp:Repeater>--%>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <%-- <asp:ObjectDataSource ID="ObjDataSourceSubSystem" runat="server" SelectMethod="FindActiveSubSystem"
        TypeName="TSP.DataManager.SubSystemManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>--%>


    <%--***Association **Temporary_Item****--%>
    <div class="row-six" id="DivAssociation" runat="server">
        <div class="container">
            <div class="row">
                <TSPControls:CustomAspxDevDataView ID="DataViewAssociation" runat="server" AllowPaging="false">
                    <SettingsTableLayout RowsPerPage="50" ColumnCount="1" />
                    <ItemStyle BackColor="Transparent" Paddings-Padding="0px" HorizontalAlign="Right" VerticalAlign="Top" />
                    <ItemTemplate>
                        <div style="text-align: center; width: 100%">
                            <a target="_blank" href='<%# "Association/ManagersCandidate.aspx?ExGgPrId=" +Utility.EncryptQS(Eval("ExGroupPeriodId").ToString()) %>'>
                                <strong><%# Eval("PeriodName") %></strong>
                            </a>
                        </div>

                        <div class="row">
                            <a target="_blank" href='<%# "Association/ManagersCandidate.aspx?ExGgPrId=" + Utility.EncryptQS(Eval("ExGroupPeriodId").ToString()) %>'>
                                <dxe:ASPxImage Style="border: 4px solid #585a56;" CssClass="img-thumbnail img-responsive" ImageAlign="Middle" ID="ASPxImage1" ToolTip='' runat="server" ImageUrl='<%# Bind("Attachment") %>'>
                                    <EmptyImage Url="~/Images/NoImage3DPeople.png">
                                    </EmptyImage>
                                </dxe:ASPxImage>
                            </a>
                        </div>
                    </ItemTemplate>
                </TSPControls:CustomAspxDevDataView>
            </div>
        </div>
    </div>
    <%--***Association **Temporary_Item****--%>
    <div class="container">
        <div class="row commissions">
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="row">
                        <a class="" runat="server" id="linkExpConstruction">
                            <img src="/Image/HomePage/Icons/omran.png" /><span>گروه تخصصی عمران</span></a>
                    </div>
                    <div class="row">
                        <a class="" runat="server" id="linkExpElectronic">
                            <img src="/Image/HomePage/Icons/electrical.png" /><span>گروه تخصصی برق</span></a>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="row">
                        <a class="" runat="server" id="linkExpArchitecture">
                            <img src="/Image/HomePage/Icons/architect.png" /><span>گروه تخصصی معماری</span></a>
                    </div>
                    <div class="row">
                        <a class="" runat="server" id="linkExpMechanic">
                            <img src="/Image/HomePage/Icons/mecanical.png" /><span>گروه تخصصی مکانیک</span></a>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="row">
                        <a class="" runat="server" id="linkExpUrbenism">
                            <img src="/Image/HomePage/Icons/urban.png" /><span>گروه تخصصی شهرسازی</span></a>
                    </div>
                    <div class="row">
                        <a class="" runat="server" id="linkExpTraffic">
                            <img src="/Image/HomePage/Icons/trrafic.png" /><span>گروه تخصصی ترافیک</span></a>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="row">
                        <a class="" runat="server" id="linkExpMapping">
                            <img src="/Image/HomePage/Icons/topography.png" /><span>گروه تخصصی نقشه برداری</span></a>
                    </div>
                    <div class="row">
                        <a class="" runat="server" id="linkExpWelfare">
                            <img src="/Image/HomePage/Icons/climbing.png" /><span>فرهنگی رفاهی</span></a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Start Important News Html Containers with worldwide -->
    <link href="/StyleSheet/galleriffic-1.css" type="text/css" rel="stylesheet" />
    <%--<script type="text/javascript" src="/Script/jquery-1.11.2.min.js"></script>--%>
    <script type="text/javascript" defer="defer" src="/Script/jquery.galleriffic.js"></script>


    <div class="container ">
        <div class=" row box-thumbs-slider ">

            <!-- Start Minimal Gallery Html Containers -->
            <div class="col-md-6">
                <div class="row">
                    <div class="row">
                        <img class="img-responsive AttentionNews" alt="AttentionNews" src="/Image/HomePage/Icons/worldwide.png" /><span class="AttentionNewsTitle">سرخط مهمترین خبر های روز</span>
                    </div>
                    <div id="thumbs" class="navigation row">
                        <ul class="thumbs noscript">
                            <asp:Repeater runat="server" ID="RepeaterImportantNews">
                                <ItemTemplate>

                                    <li>
                                        <a class="thumb" href='<%# Eval("Imageurl") %>' phref='<%# Eval("NewsContentURL") %>' title='<%# Eval("NewsHeader") %>'>
                                            <div class="box-contnt-thumb">

                                                <div class="title-thumb"><%# Eval("NewsHeader") %></div>
                                                <div class="date-thumb" style="float: left"><%# Eval("Date") %></div>
                                            </div>
                                        </a>
                                        <a class="link" title="مشروح خبر" href='<%# Eval("NewsContentURL") %>'>مشروح خبر</a>

                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>

                        </ul>
                    </div>
                </div>
            </div>
            <div class="col-md-6 gal">
                <div class="row">
                    <div id="gallery" class="content">

                        <div class="slideshow-container">
                            <div id="loading" class="loader"></div>
                            <div id="slideshow" class="slideshow"></div>
                        </div>
                        <div id="controls" class="controls"></div>
                        <div id="nav" class="controls"></div>
                    </div>
                </div>
            </div>


            <!-- End Minimal Gallery Html Containers -->
        </div>
    </div>


    <script type="text/javascript">
        // We only want these styles applied when javascript is enabled
        $('div.navigation').css({ 'width': '100%', 'min-height': '300px', 'float': 'right' });
        $('div.content').css('display', 'block');

        $(document).ready(function () {
            // Initialize Minimal Galleriffic Gallery
            $('#thumbs').galleriffic({
                imageContainerSel: '#slideshow',
                ssControlsContainerSel: '#controls',
                navControlsContainerSel: '#nav'
            });
        });
    </script>
    <%-- ********آخرین اخبار و پادکست و....************ --%>
    <asp:ObjectDataSource ID="ObjdsAlbums" runat="server" TypeName="TSP.DataManager.GalleryAlbumsManager"
        SelectMethod="SelectViewGalleryAlbums">
        <SelectParameters>
            <asp:Parameter Name="AlbumName" Type="String" DefaultValue="%" />
            <asp:Parameter Name="FromDate" Type="String" DefaultValue="1" />
            <asp:Parameter Name="ToDate" Type="String" DefaultValue="2" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <link href="/StyleSheet/responsive-tabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/responsive-tabs.js"></script>


    <div class="row-six" id="NewsSection">
        <div class="container">
            <div class="row">
                <div class="responsive-tabs-wrapper">
                    <div class="part-tabs-one responsive-tabs col-xs-12 col-sm-12 col-md-12 ie12 fr ">

                        <h2 runat="server" id="tablist1Tab1" class="responsive-tabs__heading--active h2-tab1" tabindex="0">
                            <div class="tab-icon"></div>
                            <span>اخبار</span></h2>
                        <div runat="server" class="s-box-main " aria-hidden="false" role="tabpanel" aria-labelledby="tablist1-tab1" id="tablist1Panel1" style="display: block; opacity: 1;">
                            <div style="width: 100%">
                                <!--?xml version="1.0" encoding="utf-16"?-->
                                <link href="/StyleSheet/owl.carousel.min.css" type="text/css" rel="stylesheet" />
                                <link href="/StyleSheet/owl.theme.default.min.css" type="text/css" rel="stylesheet" />
                                <%-- <script type="text/javascript" src="/Script/jquery-1.11.2.min.js"></script>--%>
                                <script type="text/javascript" src="/Script/owl.carousel.min.js"></script>
                                <script type="text/javascript" src="/Script/lazyload.js"></script>
                                <div id="owlLatestNews" class="owl-carousel owl-theme  owl-drag">
                                    <asp:Repeater runat="server" ID="RepeaterLatestNews">
                                        <ItemTemplate>
                                            <div class="box-owl-scroll">
                                                <a class="link-row" target="_blank" runat="server" href='<%# Eval("NewsContentURL") %>'>
                                                    <div class="overlab img-one">
                                                        <img class="lazyload" id="ImageNews" runat="server" data-src='<%# Eval("Imageurl") %>' />
                                                    </div>
                                                    <div class="box-contnt">
                                                        <div class="date-scroll"><%# Eval("Date") %></div>
                                                        <div class="title-scroll"><%# Eval("NewsHeader") %></div>
                                                    </div>
                                                </a>
                                            </div>

                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <a class="More" runat="server" id="linkMoreNews">بيشتر</a>
                            </div>
                        </div>
                        <h2 runat="server" id="tablist1Tab2" class="h2-tab2" tabindex="0">
                            <div class="tab-icon"></div>
                            <span>اطلاعیه ها</span></h2>
                        <div runat="server" class="s-box-main " aria-hidden="true" role="tabpanel" aria-labelledby="tablist1-tab2" id="tablist1Panel2" style="display: block; opacity: 1;">

                            <div style="width: 100%">

                                <div id="owlNewsNotification" class="owl-carousel owl-theme  owl-drag">
                                    <asp:Repeater runat="server" ID="RepeaterNewsNotification">
                                        <ItemTemplate>
                                            <div class="box-owl-scroll">
                                                <a class="link-row" target="_blank" runat="server" href='<%# Eval("NewsContentURL") %>'>
                                                    <div class="overlab img-one">
                                                        <img class="lazyload" id="ImageNews" runat="server" data-src='<%# Eval("Imageurl") %>' />
                                                    </div>
                                                    <div class="box-contnt">
                                                        <div class="date-scroll"><%# Eval("Date") %></div>
                                                        <div class="title-scroll"><%# Eval("NewsHeader") %></div>
                                                    </div>
                                                </a>
                                            </div>

                                        </ItemTemplate>
                                    </asp:Repeater>



                                </div>
                                <a class="More" runat="server" id="linkMoreNotificationNews" href="#" style="">بيشتر</a>
                            </div>

                        </div>


                        <h2 runat="server" id="tablist1Tab6" class="h2-tab6" tabindex="0">
                            <div class="tab-icon"></div>
                            <span>هیئت مدیره</span></h2>
                        <div runat="server" class="s-box-main " aria-hidden="true" role="tabpanel" aria-labelledby="tablist1-tab6" id="tablist1Panel6" style="display: block; opacity: 1;">

                            <div style="width: 100%">

                                <div id="owlNewsBoardOfDirectors" class="owl-carousel owl-theme  owl-drag">
                                    <asp:Repeater runat="server" ID="RepeaterNewsBoardOfDirectors">
                                        <ItemTemplate>
                                            <div class="box-owl-scroll">
                                                <a class="link-row" target="_blank" runat="server" href='<%# Eval("NewsContentURL") %>'>
                                                    <div class="overlab img-one">
                                                        <img class="lazyload" id="ImageNews" runat="server" data-src='<%# Eval("Imageurl") %>' />
                                                    </div>
                                                    <div class="box-contnt">
                                                        <div class="date-scroll"><%# Eval("Date") %></div>
                                                        <div class="title-scroll"><%# Eval("NewsHeader") %></div>
                                                    </div>
                                                </a>
                                            </div>

                                        </ItemTemplate>
                                    </asp:Repeater>



                                </div>
                                <a class="More" runat="server" id="A5" href="#" style="">بيشتر</a>
                            </div>

                        </div>

                        <h2 runat="server" id="tablist1Tab3" class="h2-tab3" tabindex="0">
                            <div class="tab-icon"></div>
                            <span>گزارش تصویری</span></h2>
                        <div runat="server" class="s-box-main " aria-hidden="true" role="tabpanel" aria-labelledby="tablist1-tab3" id="tablist1Panel3" style="display: block; opacity: 1;">
                            <div style="width: 100%">
                                <div id="owlImageGallary" class="owl-carousel owl-theme  owl-drag">
                                    <asp:Repeater runat="server" ID="RepeaterImageGallary">
                                        <ItemTemplate>
                                            <div class="box-owl-scroll">
                                                <a class="link-row" runat="server" target="_blank" href='<%# "ImageGallery/ImageGalleryFrame.aspx?album" +Utility.EncryptQS(Eval("AlbumId").ToString()) %>'>
                                                    <div class="overlab img-one">
                                                        <img class="lazyload" id="ImageNews" runat="server" data-src='<%# Eval("ImgUrl").ToString().Replace("~/","") %>' />
                                                    </div>
                                                    <div class="box-contnt">
                                                        <div class="date-scroll"><%# Eval("UploadDate") %></div>
                                                        <div class="title-scroll"><%# Eval("AlbumName") %></div>
                                                    </div>
                                                </a>
                                            </div>

                                        </ItemTemplate>
                                    </asp:Repeater>



                                </div>
                                <a class="More" href="ImageGallery/ImageGallery.aspx" style="">بيشتر</a>
                            </div>
                        </div>
                        <h2 runat="server" id="tablist1Tab4" class="h2-tab4" tabindex="0">
                            <div class="tab-icon"></div>
                            <span>گزارش ویدیویی</span></h2>
                        <div runat="server" class="s-box-main " aria-hidden="true" role="tabpanel" aria-labelledby="tablist1-tab4" id="tablist1Panel4" style="display: block; opacity: 1;">
                            <div style="width: 100%">
                                <div id="owlVideos" class="owl-carousel owl-theme  owl-drag">
                                    <asp:Repeater runat="server" ID="RepeaterVideos">
                                        <ItemTemplate>
                                            <div class="box-owl-scroll">
                                                <a class="link-row" target="_blank" runat="server">
                                                    <div class="overlab img-one">
                                                        <%# Eval("ImageURL") %>
                                                    </div>
                                                    <div class="box-contnt">
                                                        <div class="date-scroll"><%# Eval("CreateDate") %></div>
                                                        <a href='<%# Eval("LinkURL") %>' style="text-decoration: none">
                                                            <div class="title-scroll"><%# Eval("Description") %></div>
                                                        </a>
                                                    </div>
                                                </a>
                                            </div>

                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <a class="More" href="/Videos.aspx" style="">بيشتر</a>
                        </div>
                        <h2 runat="server" id="tablist1Tab5" class="h2-tab5" tabindex="0">
                            <div class="tab-icon"></div>
                            <span>گزارش صوتی</span></h2>
                        <div runat="server" class="s-box-main " aria-hidden="true" role="tabpanel" aria-labelledby="tablist1-tab5" id="tablist1Panel5" style="display: block; opacity: 1;">
                            <div style="width: 100%">

                                <div id="owlPodcast" class="owl-carousel owl-theme  owl-drag">
                                    <asp:Repeater runat="server" ID="RepeaterPodcast">
                                        <ItemTemplate>
                                            <div class="box-owl-scroll">
                                                <a class="link-row" target="_blank" runat="server">
                                                    <div class="overlab img-one">
                                                        <%# Eval("ImageURL") %>
                                                    </div>
                                                    <div class="box-contnt">
                                                        <div class="date-scroll"><%# Eval("CreateDate") %></div>
                                                        <div class="title-scroll"><%# Eval("Description") %></div>
                                                    </div>
                                                </a>
                                            </div>

                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <a class="More" href="/Podcasts.aspx" style="">بيشتر</a>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--***Map - magazin - galery ****--%>
    <div class="container">
        <div class="row">
            <%--*******************نقشه**********************--%>
            <div id="divMap" runat="server" class="dyn-col ">
                <div class="map-title">دفاتر نمایندگی های استان فارس<span id="lblMap" class="map-lbl"></span><span id="lblMapText" style="display: none" class="map-lbl">صفحه ی نمایندگی شهرستان&nbsp</span></div>
                <div class="fars-map-panel">

                    <svg version="1.1" id="Layer_1" xmlns="https://www.w3.org/2000/svg" xmlns:xlink="https://www.w3.org/1999/xlink" width="299" x="0px" y="0px"
                        viewbox="0 0 496 505.6" style="enable-background: new 0 0 496 505.6;" xml:space="preserve" class="fars-map">
                        <style type="text/css">
                            .st0 {
                                fill: #D5BE8F;
                            }

                            .st1 {
                                fill: #80AF26;
                            }

                            .st2 {
                                fill: #CCFFB1;
                            }

                            .st3 {
                                fill: #F5C46A;
                            }

                            .st4 {
                                fill: #5FFFDC;
                            }

                            .st5 {
                                fill: #01C6FF;
                            }

                            .st6 {
                                fill: #64ABFE;
                            }

                            .st7 {
                                fill: #43AC94;
                            }

                            .st8 {
                                fill: #1B8077;
                            }

                            .st9 {
                                fill: #FFA900;
                            }

                            .st10 {
                                fill: #B8CFFE;
                            }

                            .st11 {
                                fill: #FE9A6F;
                            }

                            .st12 {
                                fill: #E8FFB2;
                            }

                            .st13 {
                                fill: #FDBB56;
                            }

                            .st14 {
                                fill: #AAFF01;
                            }

                            .st15 {
                                fill: #FFE8B3;
                            }

                            .st16 {
                                fill: #B0FFE2;
                            }

                            .st17 {
                                fill: #E7E600;
                            }

                            .st18 {
                                fill: #FCE79F;
                            }

                            .st19 {
                                fill: #E6FFB2;
                            }

                            .st20 {
                                fill: #FDFE5E;
                            }

                            .st21 {
                                fill: #FEE8A2;
                            }

                            .st22 {
                                fill: #FDB2B3;
                            }

                            .st23 {
                                fill: #FDFFB5;
                            }

                            .st24 {
                                fill: #EFC53F;
                            }

                            .st25 {
                                fill: #55FF00;
                            }

                            .st26 {
                                fill: #61D9FE;
                            }

                            .st27 {
                                fill: #FFB2E5;
                            }

                            .st28 {
                                fill: #CCFFB1;
                            }

                            .fars-map a:hover path {
                                fill: #dc3c00;
                            }
                        </style>
                        <g>
                            <a id="9" title="لارستان" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()" class="link-path">
                                <path class="st0" d="M491.3,402.1c1.6,0.3,2.6,1.2,3,2.6c0.2,0.9,1,1.5,1.4,2.3c0.5,1,0.4,1.3-0.6,1.8c-1.6,0.8-3.4,1.3-5.1,1.5
		c-2.4,0.3-4.6,0.9-6.7,2.2c-0.9,0.5-1.9,0.9-2.8,1.2c-1.3,0.4-2.2,1.1-2.7,2.3c-0.7,1.7-1.8,3.2-2.7,4.9c-0.6,1.1-1.5,1.9-2.8,2.3
		c-1.3,0.4-2.6,1.1-3.9,1.6c-1.1,0.5-2,1.2-2.6,2.2c-1.2,1.9-2.5,3.8-3.8,5.6c-1.2,1.6-1.8,3.4-2.1,5.3c-0.5,3.7-2.8,6.2-5.1,8.9
		c-0.6,0.7-1.2,1.2-2.1,1.4c-2.8,0.6-4.9,2.3-6.5,4.5c-1.9,2.7-4.3,3.4-7.4,2.9c-0.1,0-0.3-0.1-0.4,0c-3.9,0.2-7.7-0.5-11.6-0.4
		c-1.4,0-2.7,0.4-4,0.4c-3.2,0-6.4-0.4-9.6-0.7c-3.8-0.4-7.6-0.7-11.2-1.6c-4-0.9-7.9-0.5-11.9-0.7c-0.6,0-1.1,0-1.7,0.2
		c-1.4,0.5-2.9,0.6-4.4,0.6c-1.8-0.1-3.6,0.3-5.2,1.2c-2.7,1.5-5.5,1.6-8.4,0.9c-3.8-0.9-7.7-0.9-11.5-0.6c-2.4,0.2-4.8,0.5-7.1,1.5
		c-1.2,0.5-2.2,1.3-3.2,2.1c-1.6,1.3-3.4,2.4-5.1,3.5c-2.5,1.7-5.4,2.8-8.3,3.3c-1.5,0.3-3,1.1-4.7,0.5c-0.6-0.2-1.3,0.1-1.8,0.4
		c-2.1,1.4-4.5,1.6-6.9,2.1c-3.6,0.8-7.3,0.5-11,0.5c-3.1,0-6,0.8-8.6,2.3c-1.6,0.8-3.3,1.1-4.9,1.6c-3.3,0.9-6.2-0.2-9.2-1.2
		c-2.3-0.7-4.6-1.2-6.8-1.9c-1.7-0.5-3.4-1.1-5-2.1c-1.5-1-1.6-1.7-0.6-3.1c1.4-2.1,2.6-4.4,3.6-6.8c0.9-2.2,0.1-4.4-0.1-6.6
		c-0.1-0.9-0.8-1.4-1.8-1.5c-2.1-0.2-4.2-0.2-6.3,0.1c-0.6,0.1-1.1,0.1-1.7-0.1c-0.7-0.3-1.1-0.7-0.8-1.5c0.4-0.8,0.3-1.6,0.1-2.4
		c-0.2-0.6-0.2-1.1,0.2-1.7c0.4-0.5,0.8-0.8,1.4-0.3c1.9,1.2,4.1,1.5,6.4,1.7c1.4,0.1,2.6,1,4,1.4c0.5,0.1,0.6,0.8,0.6,1.3
		c0.1,1,0.5,1.4,1.6,1.6c1.3,0.2,2.7,0.5,3.9,1c0.3,0.1,0.7,0.3,0.9,0.2c1.2-0.4,2,0.4,2.7,1c2.3,2.1,4.9,3.7,7.4,5.5
		c0.9,0.6,1.8,1,2.8,1.2c2,0.5,4,0.9,6,1.8c-0.7-2.3-1.5-4.3-2.3-6.4c-0.5-1.4-1.3-2.5-2.8-3c-0.6-0.2-1.1-0.7-1.4-1.2
		c-0.2-0.3-0.4-0.6-0.2-0.9c0.7-1,0.7-2.2,1.1-3.3c0.3-0.8,0.7-0.8,1.3-0.6c1.8,0.8,3.7,1.1,5.6,1.3c0.3,0,0.6,0,0.7,0.2
		c1.4,1.1,2.3,0.1,3.2-0.8c0.3-0.3,0.5-0.6,0.7-0.9c1.1-1.3,2.2-1.9,4.2-1.7c3.2,0.4,6.5-0.1,9.8-0.4c1.7-0.2,3.2-0.8,4.7-1.4
		c2-0.8,4.1-1.2,6.3-0.9c2.3,0.3,4.5,1.1,6.7,1.5c1.2,0.2,1.9,0.1,2.4-1c1.1-2.5,2.3-5,3-7.7c0.1-0.5-0.1-0.9-0.2-1.3
		c-0.5-1.7-0.5-3.4,0.4-5c0-0.1,0.1-0.1,0.1-0.2c0.1-1.8,1.1-3,2.9-3.7c-0.4-0.9-1.2-0.9-1.9-0.9c-2.8,0.1-5.5-0.2-8.1-1.2
		c-1.3-0.5-1.5-0.3-1.9,1c-0.6,1.7-0.8,3.5-1.3,5.2c-0.4,1.6-0.6,1.5-2.2,1.6c-1.8,0-3.4-0.4-5-1.2c-2.7-1.4-5.6-1.9-8.6-1.9
		c-4.4,0.1-8.4-1.2-12.3-2.7c-2.3-0.8-4.3-1.2-6.6-0.2c-2.6,1.1-5.4,1.5-8.3,1.3c-1.9-0.1-3.8-0.6-5.5-1.7c-1.1-0.7-1.1-0.9-0.3-1.9
		c0.4-0.4,0.7-0.9,1.1-1.3c0.5-0.6,0.8-1.6,1.7-1.7c1.3,0,2.7-0.3,3.9,0.4c1.7,1,1.8,1,2.8-0.8c0.7-1.2,1.8-2,2.9-2.8
		c1.3-0.9,1.3-1.2,0-2.1c-3.2-2.1-6.7-3.9-9.6-6.4c-2.8-2.5-6.2-3.8-9.7-5c-1.2-0.4-2.3-0.9-3.4-1.4c-0.4-0.1-0.8-0.3-0.8-0.7
		c-0.2-1.9-1.6-3.7-0.3-5.7c0.1-0.2,0.2-0.5,0-0.7c-1.1-2.3-2.3-4.4-4.6-5.8c-2.9-1.7-5.9-3.1-9-4.5c-3.3-1.4-6.5-3.2-9.6-5
		c-0.8-0.5-0.9-1.2-0.6-1.8c1.1-2.2-0.3-3.3-1.9-4.3c-0.3-0.2-0.7-0.4-1-0.6c-0.2-0.2-0.3-0.4-0.2-0.7c0.1-0.3,0.3-0.5,0.6-0.5
		c2.2,0.6,4.1-0.7,6.1-1.1c2.6-0.5,5.2-1.4,7.8-2c1.2-0.3,2.5-0.1,3.8-0.2c2.6-0.1,5.1-0.1,7.7-0.1c2,0,3.9,0,5.9,0.3
		c0.3,0,0.5,0.1,0.8,0.1c0.6,0.1,1.1,1,1.7,0.5c0.4-0.3,0.5-1,0.9-1.5c0.8-1.1,1.7-2.2,2.8-3.2c0.8-0.7,1.5-0.8,2.4-0.3
		c1.7,1,3.6,1.6,5.5,2.2c1.6,0.5,3.2,0.4,4.7-0.2c2.4-1,4.8-1.7,7.1-3c1.5-0.8,3.2-1,4.8-1.2c0.5-0.1,0.9,0,1.3,0.2
		c2.7,1.5,5.7,1.6,8.6,1.3c2.6-0.2,4.5,0.5,6.1,2.4c1.9,2.3,4.4,3.9,7,5.4c1.2,0.7,2.4,1.2,3.7,1.6c2.2,0.5,3.8,1.8,4.4,4.1
		c0.4,1.4,1,2.8,1,4.3c0.1,1.2,0.8,2.1,0.7,3.4c0,0.4,0.4,0.8,0.8,1c0.5,0.4,1.1,0.8,1.5,1.2c2,2.4,4.4,4.3,6.8,6.3
		c2,1.7,4.3,2.8,6.6,3.9c3.2,1.6,6.6,2.7,9.8,4.6c2.6,1.6,5.7,1.7,8.7,2c0.3,0,0.5,0,0.8,0c3.5,0.9,6.9-0.2,10.2-1.1
		c1.4-0.3,2.8-0.6,4.2-0.5c3.2,0.2,6.3-0.3,9.4-0.5c1.5-0.1,2.8-0.8,4.2-1.4c1.5-0.7,3.2-1,4.7-1.5c1.3-0.4,1.5-0.7,1.2-2.3
		c-0.2-1.4-0.8-2.7-1.2-4c-0.4-1-0.6-2.1-0.7-3.2c-0.1-0.6,0.2-1,0.9-1c2.6,0,5.3-0.2,7.8,0.2c1.5,0.3,3.2,0.6,4.6,1.4
		c0.8,0.4,1.3,0.2,1.9-0.2c1.2-0.8,2.7-1.4,3.6-2.5c1.3-1.3,2.8-1.3,4.3-1.2c2.9,0.3,5.8,0.9,8.7,1.3c2.6,0.3,4.5,2,6.8,3.1
		c2.6,1.3,5.1,2.9,8.1,3.3c1.4,0.2,2.3,1.2,3.1,2.2c0.7,0.8,1.3,0.9,2.3,0.7c4.2-0.7,8.5-0.9,12.7-0.5c2.2,0.2,4.4,0.8,6.5,1.2
		c1.3,0.3,2.5,0.3,3.8,0.8c0.7,0.3,1.3,0.6,1.8,1.1c0.6,0.7-0.1,1.3-0.1,2C491.9,399.7,491.6,400.8,491.3,402.1z" />
                            </a>
                            <a id="14" title="نی ریز" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st1" d="M359.1,277c-1.2-0.3-2.3,0.3-3.5,0.2c-1.6-0.2-3-0.6-4.5-1.1c-0.7-0.2-1.5-0.5-2.2-0.6
		c-2.4-0.1-3.7-1.5-4.6-3.5c-0.9-2-2.9-3.1-5.1-2.9c-0.9,0.1-1.8,0.2-2.6-0.5c-1.5-1.4-3.3-1.9-5.1-2.6c-1.8-0.6-1.7-0.7-1.7-2.6
		c0.1-1.5-0.2-2.9-0.6-4.4c-0.4-1.8-1-3.6-1.4-5.4c-0.1-0.5-0.3-1-0.6-1.4c-0.8-0.9-1.3-1.1-2.2-0.2c-1.8,1.8-3.6,2.1-5.9,0.8
		c-2.2-1.2-4.6-1.8-7.1-2.4c-2.4-0.6-4.5-1.8-6.6-3c-3.7-2.1-7.3-4.4-10.2-7.5c-0.8-0.8-1.5-1.8-2.3-2.5c-3.3-2.9-7.1-4.9-11.4-6.2
		c-0.5-0.1-0.9-0.4-1.4-0.6c-1.6-0.5-3.1-1.3-4.6-1.9c-3.7-1.6-6.2-4.5-6.8-8.7c-0.1-0.9,0.3-1.6,0.5-2.4c0.8-3,2.8-4.8,6-5.2
		c1.1-0.1,2.1-0.7,3.2-0.3c0.5,0.2,0.8-0.1,1.1-0.5c1.5-2.1,2.8-4.3,4.8-6c0.7-0.7,0.3-1.6-0.2-2.3c-1-1.5-1.1-3.1-1.1-4.8
		c0-1.1,0.4-1.8,1.4-2.3c1.2-0.5,2.3-1.2,3.4-1.9c1.5-1,3.2-1.4,4.9-1.6c0.9-0.1,1.9-0.6,2.6-0.5c1.4,0.2,2.9-0.9,4.3,0.4
		c0.4,0.4,1,0.5,1.6,0.5c2.4-0.3,5,0.3,7.3-1c0.4-0.2,0.9-0.2,1.3,0.1c2.8,1.5,5.4,3.2,7.7,5.4c1.4,1.4,3,2.4,4.8,3.2
		c0.7,0.3,1.3,0.5,2,0.4c1.9-0.1,3.8,0.1,5.7,0.8c0.8,0.3,1.8,0.3,2.6,0.5c1.8,0.5,3.1,1.7,4.3,3.1c2,2.4,3.4,5.2,4.1,8.2
		c1,4.2,3.7,6.8,7.6,8.3c1,0.4,2,0.4,3,0c0.9-0.4,1.9-0.7,2.9-1.2c1.8-1,3.8-0.9,5.8-1c2.8-0.1,5.6-0.2,8.4-0.4
		c0.2,0,0.5-0.1,0.7-0.2c2.1-1.5,4.3-1,6.4-0.5c2.2,0.5,4.3,1.2,6.4,2.2c1.7,0.8,3.5,1.1,5.4,0.3c1.7-0.7,3.5-1.2,5.4-1.3
		c2.4-0.1,4.6,0.4,6.7,1.4c1.9,0.8,3.4,2.2,4.8,3.8c1.4,1.6,2.7,3.3,3.8,5.1c1.7,2.6,3.6,5,5.9,7.1c0.7,0.6,1.3,1.2,1.7,2
		c2.2,4.4,5.8,7.2,9.9,9.7c1.4,0.8,2.8,1.6,4.3,2.1c4,1.5,6.8,4.3,9.2,7.6c3.4,4.9,5.3,10.3,6.5,16.1c0.7,3.2,0.9,6.3,1.4,9.5
		c0.1,0.5,0.1,1-0.1,1.5c-0.2,0.9-0.3,1.8-0.3,2.7c0.1,2.9-0.5,5.8-0.5,8.6c-0.1,3.5-0.9,7-0.5,10.5c0.1,1.1,0.2,2.2,0.4,3.2
		c0.5,2.5,0.3,2.7-2.3,2.8c-2.6,0-5.2,0.2-7.7,1.3c-0.9,0.4-2,0.3-3,0.3c-0.5,0-1.1-0.2-1.4-0.5c-2-1.9-4.5-3.2-6.7-4.9
		c-2-1.4-3.4-3.3-4.8-5.2c-1.3-1.7-2.6-3.3-4.6-4.3c-2.4-1.3-4.4-3.2-6.4-4.9c-2.2-1.9-4.7-3.1-7.4-4.1c-2.2-0.8-4.1,0.4-5.6,1.9
		c-2,2.1-4.3,3.2-7.2,3c-2.4,1.3-5.1,0.4-7.6,1c-1.9,0.5-4,0.3-6,0.1c-1-0.1-2-0.6-2.8-1.2c-0.9-0.6-0.9-0.9-0.1-1.8
		c0.4-0.4,0.7-0.9,0.8-1.5c0.1-0.6,0.4-1.1,0.5-1.7c0.3-1.1,1.3-2,0.7-3.3c-0.8-1.8-1.7-3.6-3.6-4.3c-1.6-0.6-2.6-1.7-3.5-3.1
		C365.6,277.9,362.5,277.2,359.1,277z" />
                            </a>
                            <a id="2" title="آباده" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st2" d="M170.6,14.3c1,0.8,2.5,0.7,3.4,1.7c0.2,0.2,0.7,0.2,1.1,0.1c2.6-0.5,5.1-0.4,7.6,0c0.4,0.1,0.8,0.1,1.2,0.1
		c6.5,0.4,13,0.4,19.5,0.4c1.1,0,2.1,0.1,3.1,0.8c1.1,0.7,2.5,0.4,3.8,0.2c1.8-0.3,3.3,0.3,4.7,1.2c2.5,1.5,2.8,1.8,3.5,4.8
		c0.7,3,1.9,5.8,3.1,8.7c0.4,1,1,2.1,0.6,3.3c-0.1,0.3,0,0.7,0.3,0.9c0.3,0.3,0.4,0.6,0.6,1c0.9,2.5,1.8,5,2,7.7
		c0.2,1.7,0,3.5-0.9,5c-0.3,0.6-0.5,1.2-0.3,2c0.4,1.6-0.3,3.2-0.5,4.7c-0.1,1.2-0.4,2.4-0.9,3.5c-0.8,2-0.6,4.2-0.3,6.3
		c0.2,1.1,1.1,2.1,1.2,3.4c0,0.1,0,0.1,0.1,0.2c1.3,0.9,2.1,2.4,3.1,3.6c0.3,0.4,0.8,0.4,1.2,0.4c3-0.1,5.6,0.9,8.1,2.6
		c2.8,1.9,5.6,3.7,8.3,5.5c2.2,1.5,2.2,2.2,0.7,4.3c-2,2.7-4.3,5.1-6.7,7.4c-0.6,0.6-1.3,0.9-2,0.9c-2.1,0-4.2,0.2-6.3,0
		c-1-0.1-1.8-0.3-2.7-0.9c-0.8-0.5-1.8-0.6-2.7-1c-2.6-1.2-5.3-2.3-7.8-3.8c-0.2-0.1-0.4-0.3-0.7-0.4c-2.1-0.4-2.4-1.9-2.1-3.6
		c0.2-1.2,0.2-2.4,0.1-3.6c-0.1-1.1,0.1-2.3,0.9-3.3c0.7-0.8,0.6-1.9,0.2-2.8c-0.4-1.1-0.9-2.2-1.7-3c-1.4-1.4-2.9-2.8-4.4-4.1
		c-1.3-1.1-1.6-1.1-3-0.2c-1.3,0.8-2.7,0.7-4.1,0.9c-1,0.1-2,0.3-3,0.7c-0.9,0.5-1.9,0.6-3,0.6c-0.9,0-1.8,0-2.7,0
		c-3.9-0.2-7.8,0.4-11.8,0.3c-1.1,0-2.3,0.4-3.4,0.6c-3,0.4-6,0.9-8.3-2c-0.7-0.8-1.6-1.4-2.7-1.8c-2.6-1-4.8-2.8-7.2-4.1
		c-0.3-0.2-0.6-0.3-0.8-0.5c-0.8-0.5-1.3-0.2-1.7,0.6c-0.4,0.8-0.8,1.6-1.1,2.4c-0.9,2.9-2.9,5.3-4.2,7.9c-1.1,2.2-3,4-4.7,5.9
		c-1.6,1.8-2.9,3.9-4.2,6c-0.5,0.8-0.2,1.5,0.3,2.3c0.8,1.2,0.8,1.7-0.3,2.6c-1.7,1.4-3.5,2.7-5.9,2.5c-0.7,0-1.4,0.1-2.1,0.4
		c-1.2,0.5-2.4,0.7-3.7,0.4c-0.7-0.1-1.4-0.1-2.1,0.1c-3.2,1-6.3,0.5-9.3-1c-0.3-0.2-0.8-0.4-0.9-0.7c-0.8-1.8-1.8-3.7-1.6-5.7
		c0.3-2.3,1.2-4.5,3-6.2c1.3-1.2,2.6-2.4,3.7-3.9c0.9-1.3,1-1.6,0.2-2.9c-0.6-1-1.1-2-1.3-3.2c-0.2-1.2,0.1-2.1,0.9-2.9
		c2.1-2.5,3.6-5.5,5.3-8.2c0.6-1,0.8-2,0.7-3.1c-0.1-3.5-0.1-6.9-0.1-10.4c0-2.4-0.4-4.7-0.2-7.1c0-0.5-0.2-0.9-0.5-1.4
		c-0.7-1-1.3-2.1-1.3-3.5c0-1-1-1.7-1.2-2.8c-0.3-1.8-0.6-3.4,0.5-5.1c1.6-2.4,2.9-5,5.1-6.9c0.4-0.3,0.5-0.8,0.5-1.2
		c-0.3-1.7,0.5-3.1,0.7-4.7c0.4-2.6,2.2-4.6,4.1-6.1c2.1-1.7,3.9-3.7,5.7-5.7c1.3-1.3,2.7-2.6,4-3.8c0.9-0.8,1.8-0.8,2.8,0
		c1.7,1.4,3.4,2.8,5.1,4.1c1.2,1,2.1,2.2,2.9,3.6c1.1,2,2.6,3.6,4.5,4.8c2.1,1.3,4.1,2.6,6.4,3.6C169.8,14.3,170.3,14,170.6,14.3z" />
                            </a>
                            <a id="5" title="داراب" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st3" d="M354.2,302.6c2.8-0.2,5.2,1.1,7.8,2.1c0.5,0.2,1,0.4,1.4,0.6c1.8,0.8,2.6,0.6,3.4-1.2c0.9-1.8,2.2-3.3,3.2-5
		c0.5-0.8,1.1-1,2-0.8c0.1,0,0.1,0,0.2,0c5.4,2,10.9,1.4,16.4,0.7c3.3-0.5,6.5-1.8,9.1-4.3c1.7-1.7,3.8-1.9,5.8-0.6
		c0.9,0.6,1.9,0.8,2.7,1.4c1.6,1,3.2,2.1,4.5,3.6c0.9,0.9,1.9,1.7,3,2.4c3.3,1.8,5.8,4.5,7.5,7.8c0.5,0.9,1.1,1.6,2.1,1.9
		c0.7,0.2,1.3,0.6,1.8,1c1.8,1.8,4.1,2.9,6.1,4.4c1.6,1.1,3.4,1.2,5.1,0.4c4.9-2,9.7-1.6,14.3,0.7c5,2.5,9.1,6,12.3,10.6
		c1.1,1.6,2.4,3.1,3.2,4.8c0.5,1,0.9,2,0.4,3.1c-0.1,0.3-0.1,0.8-0.1,1.1c0,1.5-0.6,2.7-1.7,3.7c-0.5,0.5-1,1-1.4,1.6
		c-0.6,1-0.9,2-0.6,3.4c0.5,1.7,0.4,3.6,0,5.3c-0.2,0.8-0.2,1.7-0.5,2.4c-0.6,1.5-0.1,2.7,0.5,4c0.6,1.3,1,2.5-0.1,3.8
		c-0.3,0.4-0.4,1-0.2,1.5c0.4,1.2,0.4,2.6,1.1,3.7c1.6,2.7,1.2,5.4,0.4,8.2c-0.3,1-0.4,1.8,0.2,2.7c1,1.7,1.2,3.5,0.5,5.4
		c-0.6,1.8-1,3.7-2.1,5.4c-0.8,1.2-0.7,1.1-2.1,0.7c-2.2-0.6-4.2-1.4-6.2-2.4c-1-0.5-1.9-1.4-2.9-1.8c-3.1-1.3-6.2-2.4-9.6-2.6
		c-1.6-0.1-3.2-0.2-4.8-0.3c-1.1,0-2,0.2-2.9,0.9c-0.2,0.2-0.5,0.2-0.6,0.4c-2.7,2.6-5.8,2.5-9,1.3c-2.7-1.1-5.6-0.6-8.3-0.8
		c-0.3,0-0.7,0.1-0.8,0.4c-0.7,1.3-1.1,0.5-1.6-0.1c-1-1.2-1.6-2.8-2.7-4c-0.7-0.7-0.6-1.7-0.1-2.5c1.5-3,1.9-6.3,2.5-9.4
		c0.7-3.6,1.7-7.1,2.2-10.7c0.1-1,0.4-1.9,0.8-2.7c0.7-1.4,0.2-2.3-1.3-2.8c-0.4-0.1-0.9-0.3-1.3-0.3c-5.6-0.1-10.7-2.1-15.3-5.1
		c-5.1-3.2-9.6-7.3-13.5-12c-1-1.2-2.1-2.1-3.6-2.3c-2.1-0.4-3.8-1.6-5.6-2.4c-0.5-0.2-0.9-0.5-1.3-0.8c-1.4-1-2.9-0.8-4.1,0.2
		c-1.2,1-2.8,1.4-4.1,2.2c-0.7,0.4-1.4,0.4-2-0.1c-0.8-0.6-1.6-0.6-2.4,0.1c-1,0.8-2.4,0.9-3.3,1.8c-0.4,0.3-1,0.2-1.5-0.1
		c-2.5-1.6-5.3-2.2-8.1-2.9c-2.8-0.7-5.5-1.7-8.2-2.8c-0.9-0.4-1.2-0.9-1.4-1.8c-0.4-3.5,0.9-6.4,2.5-9.3c1.5-2.6,3.2-5.1,4.6-7.8
		c1.1-2,3-3.5,4.8-5C352,302.6,353,302.4,354.2,302.6z" />
                            </a>
                            <a id="1" title="شیراز" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st4" d="M234.5,234.2c-1.6,1.6-3.4,2.3-5.3,2.5c-0.7,0.1-1.4,0.2-2.1,0.3c-1.6,0.4-2.2,1.3-1.5,2.8
		c0.6,1.2,0.2,2.7,1,3.8c0.2,0.2,0,0.6-0.3,0.8c-0.3,0.2-0.7,0.1-0.9-0.1c-0.9-1.8-2.3-1.1-3.4-0.5c-1.9,0.9-3.5,0.7-5.2-0.5
		c-1.2-0.9-2.3-1.9-3.7-2.5c-2.1-0.9-3.8-0.8-5.4,0.9c-1.2,1.3-2.5,2.4-3.6,3.8c-1.6,2.2-4,3.6-6.3,5c-0.2,0.1-0.4,0.2-0.5,0.2
		c-3.1,0.4-6.2,0.8-9.2-0.8c-1.7-0.9-3.2-1.8-4.6-3c-0.7-0.6-1.6-0.8-2.5-1c-0.7,1.9-1.1,3.8-0.8,5.9c0.2,1.5-0.2,3.1-0.9,4.5
		c-1.6,3.1-3,6.2-4.1,9.5c-0.8,2.4-2,3.1-4.5,3c-0.3,0-0.7-0.1-1,0c-2.8,0.7-4.8-0.7-6.7-2.3c-1-0.9-2.2-1.6-3.5-2
		c-1.3-0.4-2.5-0.4-3.7,0.1c-1.2,0.5-2.3,1.1-3.6,1.3c-1.1,0.1-1.5,1.1-2.2,1.8c-1.1,1.2-1.4,1.2-2.2-0.3c-1.4-2.7-2.7-5.5-4.3-8.2
		c-1-1.7-0.7-3.7-0.3-5.5c0.3-1.5,0.3-3.1,0.8-4.5c0.2-0.5-0.1-0.9-0.2-1.3c-0.8-2.5-2-4.7-3.5-6.8c-0.7-1-1.2-2.3-1.8-3.4
		c-0.6-1.1-1.5-1.7-2.8-2.2c-3.8-1.4-7.5-3.2-8.6-7.8c-0.3-1.3-0.4-2.5,0.7-3.7c1.3-1.4,0.8-3.5-1-4.8c-3.5-2.4-5.5-5.8-7.5-9.4
		c-0.4-0.7-0.5-1.6-0.5-2.4c0-2.1,0.1-4.1-0.3-6.1c-0.4-1.9,1.3-3.1,3.2-2.3c0.6,0.2,1.2,0.4,1.9,0.4c1.9,0.1,3.6,0.5,5.3,1.2
		c1.5,0.6,3,0.9,4.5-0.3c1-0.8,2.3-0.7,3.5-0.6c3.8,0.3,6.2-1.7,8.1-4.8c1-1.6,1.6-3.4,2.1-5.1c0.3-0.8,0.7-1.2,1.5-1.2
		c2.8,0,5.6-0.3,8.4,0.6c3.2,1,6.5,1.9,9.8,2.9c2.6,0.8,4.8,2.4,6.6,4.5c0.5,0.6,0.9,1.1,1.5,1.5c1.7,1.1,3.5,2.2,5.2,3.3
		c0.6,0.4,1.2,0.7,2,0.8c1.1,0.2,2.1,0.4,3.2,0.7c0.9,0.3,1.9,0.2,2.8-0.1c2.3-0.8,4.6-1.5,7-2.1c3.5-0.9,7.1-0.7,10.7-0.6
		c1.1,0,2.1,0.4,3.1,1c1.1,0.6,2.2,0.7,3.4,0.4c0.9-0.3,1.7-0.1,2.4,0.4c2.6,1.8,5.6,2.6,8.4,3.7c2.6,1,5.2,2.1,7.6,3.6
		c1.4,0.9,3,1.5,4.4,2.4c1.2,0.8,1.5,1.7,0.8,2.9c-1.7,3-2.9,6.1-4,9.4c-0.2,0.6-0.3,1.3-0.7,1.8c-0.9,1-0.6,2.1-0.3,3.2
		C231.5,230.8,232.3,232.8,234.5,234.2z" />
                            </a>
                            <a id="12" title="اقلید" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st5" d="M120.2,95.7c3.3,0.8,6.2,1.4,9.3,0.6c4.7,0.5,9-0.7,13.1-2.8c0.9-0.4,1.7-0.9,2.5-1.4c1.3-0.8,1.9-1.8,1-3
		c-1.5-2.1-0.5-3.5,0.7-5.1c1.7-2.5,3.4-5,5.6-7.1c0.8-0.7,1.2-1.7,1.6-2.6c1.2-2.6,3.1-4.7,4-7.4c0.2-0.6,0.7-0.7,1.2-0.8
		c1.1-0.2,2.2,0,3.1,0.7c1.3,1,2.9,1.6,4.4,2.4c1.5,0.7,2.7,1.6,3.9,2.8c1.7,1.8,3.8,2.6,6.3,1.9c2.6-0.7,5.2-1,7.8-0.8
		c1.1,0.1,2.2-0.3,3.2-0.4c2.2-0.1,4.4-0.1,6.5-0.1c1.2,0,2.4-0.1,3.6-0.5c2-0.6,4-0.9,6-1.1c0.8-0.1,1.6-0.3,2.3-0.8
		c0.4-0.4,1-0.5,1.4-0.2c1.9,1.2,4,2.2,4.9,4.5c0.6,1.5,1.2,3,0.1,4.6c-0.3,0.4-0.3,1-0.4,1.5c-0.2,2.2-0.2,4.4-0.5,6.5
		c-0.2,1.4,0.5,2.7,1.5,3.7c1.8,2,2.5,4.2,2.5,6.9c0,1,0,2,0.4,2.8c0.8,1.6,0,2.8-0.9,3.8c-1.8,2.1-3.1,4.4-4.1,6.9
		c-1.1,2.6,0.1,5.4,2.7,6.5c1.3,0.6,2.6,1.4,3.8,2.2c0.8,0.5,1,1.4,0.4,2.2c-1.8,2.5-2.8,5.5-5.4,7.5c-0.9,0.6-1.3,1.8-1.8,2.7
		c-0.3,0.5-0.4,1-0.2,1.5c0.9,2.1,1.4,4.4,2.7,6.4c0.7,1.1,1.7,2.1,2.6,3.1c1.4,1.4,1.6,3.5,0.3,5c-1.1,1.3-2,2.6-3.1,3.9
		c-1.6,2-3.8,3.5-6.7,2.5c-1.4-0.5-2.5-1.5-3.6-2.5c-0.8-0.7-1.5-1.4-2.2-2.2c-1.4-1.5-2.9-1.5-4.7-0.9c-1.8,0.6-3.3,1.6-4.8,2.7
		c-0.6,0.4-1.1,0.9-1.8,1.1c-1.3,0.4-2.3,0.3-3.3-0.8c-3.3-3.3-7-6-10.9-8.5c-1.1-0.7-2.3-1.4-3.6-1.8c-1-0.3-1.8-1-2.7-1.5
		c-2-1.1-4.1-1.9-6.2-2.5c-3.2-0.8-5.6-2.8-8-4.7c-1.6-1.3-3-2.8-4.5-4.2c-0.9-0.8-1.5-1.8-1.8-2.9c-0.8-2.8-2.4-5.1-3.8-7.5
		c-1.3-2-3.2-3.5-5-5c-0.3-0.2-0.6-0.4-0.9-0.1c-1.2,1.1-3.1,1.3-3.5,3.3c-0.1,0.8-0.9,1.1-1.8,1.1c-2.9,0.1-5.8-0.1-8.6-0.8
		c-2.5-0.6-3.8-2.5-4.3-4.7c-0.4-2-1.6-3.3-3-4.5c-2.7-2.4-2.4-1.9-0.2-4.6c1.1-1.3,2.2-2.5,2.6-4.2C120.1,97.9,120.5,97,120.2,95.7
		z" />
                            </a>
                            <a id="4" title="جهرم" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st28" d="M325.6,342.6c-0.6,1.5-2,2.5-3,3.8c-1,1.3-1.8,2.7-2.1,4.3c-0.5,2.4,0.9,5,3.2,5.8c1.2,0.4,2.5,0.6,3.7,1.2
		c0.4,0.2,0.7,0.4,0.6,0.8c-0.1,0.4-0.5,0.5-0.9,0.5c-2.3-0.3-4.6,0-6.9-0.1c-0.9,0-1.6-0.3-2.4-0.6c-2.6-1.2-5.3-0.8-7.5,0.7
		c-2,1.3-4.1,2-6.3,2.9c-2.6,1-5.2,0.6-7.6-0.6c-1.6-0.8-3.1-1.5-4.7-2.2c-1.2-0.5-1.4-0.4-1.8,0.8c-0.5,1.3-1.4,2.3-2.1,3.4
		c-0.4,0.7-1.3,0.9-2.1,0.8c-1.7-0.1-3.4-0.2-5.1-0.4c-0.8-0.1-1-0.5-0.6-1.2c1.1-2.1,1.6-4.4,2.8-6.5c0.6-1,0.3-1.4-0.8-1.6
		c-0.6-0.1-1.3-0.1-1.9-0.1c-3.1-0.2-6.2,0.3-9.2-0.3c-0.9-0.2-1.6-0.7-2.5-1c-2.2-0.6-4.4-1.3-6.7-1.7c-1.1-0.2-2.2,0-3.4-0.1
		c-4.6-0.5-7.9-2.9-10.3-6.7c-0.7-1-1.4-2-2.3-2.8c-1.1-1.1-2.3-2.1-3.2-3.3c-1.3-1.8-3.2-2.7-5.1-3.3c-3.7-1.1-7.2-2.8-10.8-4.2
		c-1.9-0.7-3.5-2.1-5.2-3.2c-3.6-2.4-7.3-4.8-10.9-7.2c-0.9-0.6-1.6-1.3-1.4-2.5c0.2-1.2,0.8-1.9,2-2.3c1.8-0.6,3.6-1.3,5.2-2.5
		c0.5-0.4,1.1-0.5,1.8-0.6c1.3-0.1,2.5-0.3,3.8-0.5c0.9-0.1,1.4-0.7,1.7-1.4c0.5-1,1-2.1,1.4-3.2c0.2-0.5,0.3-1,0-1.5
		c-0.7-1.2-0.7-2.4-0.7-3.7c0-1.9-0.5-3.5-1.9-4.9c-0.7-0.7-1.3-1.1-2.3-1.1c-1.3,0-2.6,0-3.9-0.1c-0.8,0-1.4-0.5-1.9-1.1
		c-1.5-1.9-2.4-4.2-3.6-6.4c-0.7-1.3-0.6-1.8,0.6-2.5c2.4-1.4,4.8-2.9,7.8-3c1.6-0.1,1.6-0.1,2.1-1.8c0.5-1.7,1.4-3.1,2.9-4
		c1.9-1.1,4-0.6,5.9,0.3c0.7,0.4,1.3,0.6,2.1,0.5c2.4-0.4,4.7,0.1,6.7,1.7c1.6,1.2,3.5,2.2,5.2,3.3c1.5,1,3,1.9,4.4,3.2
		c1.4,1.3,3.2,2,5.3,2c3.7-0.1,7.3,0,11-0.1c1.2,0,2.5-0.1,3.6,0.3c1.7,0.6,3.4,0.2,5.1-0.2c1.2-0.3,1.6,0,1.7,1.1
		c0.1,1.3,0.5,2.7-0.2,3.9c-0.4,0.7-0.1,1.2,0.2,1.8c0.5,0.8,1.1,1.4,1.8,2c2.7,2.4,5.3,5,7.1,8.3c0.6,1.2,1.7,2.1,2.6,3.1
		c0.4,0.5,0.9,0.8,1.5,0.8c1,0,1.3,0.5,1.6,1.3c0.4,0.9,0.9,1.9,1.5,2.7c1.9,2.3,3.5,4.9,5.7,7c1.6,1.5,3.6,2.4,5.7,3.2
		c0.9,0.3,1.4,0.8,1.8,1.6c0.7,1.4,1.4,2.9,2.2,4.3c1,1.9,2.3,3.4,4.4,4.5c1.2,0.6,2.3,1.5,2.9,3c0.6,1.6,1.8,2.7,3.6,3.1
		c1.9,0.5,3.8,1.3,5.7,1.6c0.7,0.1,1.4,0.6,2.1,1C325.3,342.4,325.5,342.4,325.6,342.6z" />
                            </a>
                            <a id="13" title="ممسنی" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st6" d="M121,174.7c1.9-0.6,3.2,0.5,4.5,1.6c1.7,1.4,3.5,2.6,5.9,2.6c0.9,0,1.8-0.1,2.6-0.6c0.4-0.2,0.9-0.8,1.1-0.5
		c0.3,0.4,0.8,0.6,1.1,0.9c0.2,0.2,0.3,0.4,0.3,0.7c0.2,0.8,1.4,0.9,1.3,1.8c-0.1,1-1.2,0.8-1.8,1.2c-0.9,0.7-1.1,1.8-0.3,2.7
		c0,0,0.1,0.1,0.1,0.1c1.7,2.9,4,5.2,6.9,7c1.4,0.8,1.4,2.4,0.3,3.6c-1,1.1-2.2,1.8-3.7,1.7c-2.2-0.2-4.2,0.3-6.1,1.3
		c-0.9,0.4-1.8,0.5-2.8,0.3c-2.7-0.6-5.4-1.2-8-1.9c-1.1-0.3-2.1-0.4-3,0c-1.5,0.6-3,0.7-4.5,1c-2.7,0.5-4.9-0.8-6.8-2.5
		c-0.8-0.7-1.5-1.6-2.2-2.4c-0.4-0.5-0.8-0.9-1.3-1.1c-2.5-0.6-4.9-1.4-7.5-1.7c-1.8-0.2-3.2,0.8-4.7,1.4c-0.5,0.2-0.8,0.6-0.9,1.2
		c-0.2,1.4-1.1,2-2.4,1.9c-1.5-0.2-2,0.6-2.6,1.9c-1.4,3-3.6,5.2-6.9,6.3c-1.1,0.4-2,1.1-2.3,2.2c-0.4,1.3-1.2,2.4-2,3.5
		c-0.6,0.8-1.3,1.6-1.7,2.6c-0.3,0.7-0.7,1-1.5,0.9c-0.4-0.1-0.8,0-1.1,0c-1.8-0.2-3.4,0.4-4.7,1.7c-0.8,0.8-1.7,0.9-2.7,0.5
		c-2.4-1-5-0.7-7.5-0.9c-2-0.1-4.1-0.5-5.8-1.7c-2.3-1.7-4.8-3.2-7.4-4.6c-1.3-0.7-2.5-1-4-0.9c-3.3,0.2-6.7,0.2-10-0.2
		c-1.5-0.2-3,0-4.4,0.5c-0.9,0.3-1.7,0.4-2.6,0.1c-3.6-1.4-7.2-2.7-10.8-4c-1.2-0.5-2.3-1.1-3.1-2.2c-0.5-0.8-0.9-1.5-0.8-2.5
		c0.1-2.6-1-4.3-3.4-5.2c-1-0.4-2.1-0.7-3.1-1.1c-0.7-0.3-0.9-0.7-0.4-1.4c0.4-0.5,0.8-1,1.2-1.5c0.8-0.8,1.6-1.5,2.3-2.3
		c2.2-2.5,5.1-3,8.2-2.5c2.9,0.5,5.6,1.4,8.3,2.3c2.1,0.7,3.2,0.2,4.3-1.7c0.4-0.7,0.6-1.4,0.7-2.2c0.3-3.3,1.7-6.1,3.4-8.9
		c0.7-1.1,1.5-1.9,2.8-2.3c1.1-0.3,1.9-1,2.7-1.8c1.3-1.5,2.9-2.7,4.5-4c0.9-0.7,1.8-1.4,3.1-1.2c0.5,0.1,1-0.2,1.4-0.5
		c1.3-0.7,2.6-1,4.1-0.9c1.2,0.1,2.2-0.2,3.3-0.8c1.3-0.8,2.7-1.7,4.5-0.9c0.6,0.3,1.1-0.2,1.4-0.8c0.9-1.9,2.5-3.2,4.4-4.2
		c0.3-0.2,0.7-0.4,0.9-0.7c0.9-1.1,1.7-1.5,3-0.3c0.6,0.6,1.5,0,2.2-0.1c2.7-0.4,4.3,0.3,5.5,2.6c0.1,0.2,0.2,0.4,0.3,0.5
		c0.9,1.5,1.7,1.8,3.1,0.9c1.2-0.7,2.2-1.1,3.6-0.6c0.7,0.3,1.5,0.1,2.3-0.2c0.8-0.3,1.5-0.1,2,0.7c1,1.7,2.8,2.6,4,4.2
		c0.9,1.1,1.5,1,2.3-0.3c0.5-0.7,1.1-1.1,1.8-1.4c1.1-0.3,2.1-0.9,3.1-1.4c1.6-0.8,2-1.9,0.9-3.3c-1.5-1.9-2.5-4-3.6-6.1
		c-0.3-0.6-0.4-1.1,0-1.8c1.1-1.8,1.2-3.7,0.1-5.6c-1-1.6-2.1-3.2-3.1-4.9c-0.9-1.4-0.5-2.1,1.1-2.2c0.2,0,0.4-0.1,0.6,0
		c1.8,0.5,3.5,0.1,5.2-0.6c0.7-0.3,1.1,0.1,1.5,0.5c0.6,0.6,1,1.3,1.3,2.1c0.3,0.9,0.7,1.7,0.9,2.5c0.4,1.4,1.1,2.4,2.4,3.1
		c1,0.6,1.9,1.3,2.8,2c0.6,0.5,1.3,0.9,2.1,0.9c1.3,0.1,2.3,0.7,3.3,1.4c1.8,1.3,3.7,2.5,5.6,3.8c2.7,1.9,4.8,4.2,5.9,7.4
		c0.4,1.1,0.5,2.2,0.1,3.4c-0.2,0.8-0.6,1.4-1.5,1.5c-2.6,0.3-4.9,1.6-7.4,1.9c-0.6,0.1-1.1,0.3-1.6,0.7c-2.5,2-1.9,3-0.1,4.1
		c0.3,0.2,0.6,0.3,0.8,0.5C114.5,174.1,117.7,174.4,121,174.7z" />
                            </a>
                            <a id="22" title="فراشبند" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st7" d="M166.4,396c-4.4,0.2-8.2-1.2-11.7-3.4c-4.5-2.9-9-5.9-13.5-9c-2.2-1.5-2.4-3.7-0.4-5.1c3.6-2.5,4.7-6.1,5-10.1
		c0.2-2.3-0.5-4.3-1.5-6.4c-2.4-4.9-4.5-9.9-5.7-15.3c-0.2-0.9-0.6-1.7-0.9-2.5c-0.6-1.7-0.8-3.5-1-5.3c-0.2-2.2-0.5-4.5-0.5-6.7
		c0-1.9-0.3-3.7-0.9-5.5c-1.6-4-2.4-8.2-3.5-12.4c0-0.2-0.1-0.4-0.1-0.6c0.2-2.6-1.2-4.4-3-5.9c-2.1-1.8-2.7-4.4-3.6-6.9
		c-0.7-2-1.3-4-2.2-5.9c-1.1-2.3-3.3-3.6-4.9-5.4c-0.5-0.6-1.1-1.1-1.6-1.7c-0.8-1-0.8-2-0.5-3.1c0.4-1.1,0.9-2.1,1.7-3
		c0.8-0.8,1.4-1.9,2.2-2.7c2.2-2.2,4.8-3.9,7.6-5.3c1.2-0.6,2.3-1.1,3.6-1.5c1.5-0.4,3-0.7,4.6-0.3c0.8,0.2,1.6,0.2,2.5,0
		c1.8-0.5,3.6,0.1,5.3,0.3c1.1,0.1,1.9,1.2,2.4,2.1c1.7,2.8,3.9,5.2,5.6,8.1c1.1,1.9,2.6,3.7,4.3,5.2c0.6,0.5,1.2,1.2,1.8,1.8
		c1.8,1.8,2.7,4,3.5,6.3c0.8,2,1.2,4,2,6c0.4,1,1,1.9,1.8,2.7c1.8,1.7,3.1,3.8,3.6,6.3c0.2,0.8,0.5,1.4,1.1,2
		c1.6,1.6,3.2,3.2,4.8,4.8c0.7,0.7,1.5,1.3,2.5,1.4c1.3,0.1,2.5,0.9,4,0.6c1-0.2,2,0.9,2.8,1.8c0.8,0.8,1.3,1.6,0.9,2.9
		c-0.4,1.2-0.4,2.6,0.1,3.8c0.4,1.2,0.3,2.3-0.2,3.3c-0.8,1.6-0.9,3.2-0.2,5c1.4,3.6,1.3,7.4,0.7,11.2c-0.1,0.7-0.2,1.4-0.7,2
		c-0.7,0.7-0.4,1.3,0,2.1c0.9,1.7,0.9,1.8-0.8,2.8c-0.9,0.6-1.5,1.3-1.5,2.5c0,1.3-0.5,2.5-1,3.7c-0.8,1.9-1.6,3.9-2.2,5.9
		c-0.6,1.9-1.7,3.6-2.6,5.4c-0.4,0.9-0.7,1.8-0.7,2.8c0,3.9,0,7.8,0,11.8c0,2,0,4.1-0.3,6.1c-0.3,1.4-1.5,3.1-2.9,3.1
		C170.1,395.5,168.2,396.4,166.4,396z" />
                            </a>
                            <a id="02" title="بوانات" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st8" d="M276.5,175c-1.2-0.1-2.2,0-3.3,0.4c-0.8,0.2-1.6,0.1-2.1-0.6c-0.7-1-1.7-1.8-2.3-2.8c-1.3-2-3-3.7-4.8-5.2
		c-2.8-2.4-4.1-5.6-4.8-9.1c-0.2-1.2-0.5-2.4-0.9-3.5c-0.4-1-0.6-2-0.7-3c-0.2-2.5,0.2-3,2.7-3.5c1.8-0.3,2-0.5,2-2.3
		c0-2.7,0.1-5.4,0-8.1c-0.1-1.6,0.4-2.7,1.9-3.4c1.8-0.8,2.3-2.6,1.5-4.3c-0.5-1.1-1.2-2-2.2-2.6c-0.7-0.4-0.7-1-0.3-1.6
		c0.2-0.4,0.5-0.8,0.6-1.2c0.5-1.4,0.1-2.3-1-3.2c-1.2-0.9-2.7-1.1-4-1.8c-0.5-0.3-1.1-0.5-1.5-0.8c-1.1-0.8-1.5-1.9-0.9-3.1
		c0.5-1.1,1.1-2.3,1.9-3.1c2.2-2.2,3.9-4.8,6.1-7c1.5-1.5,3.3-2.6,4.9-4c0.5-0.4,1-0.4,1.6-0.2c1.2,0.4,2.4,0.9,3.5,1.6
		c1.8,1.2,3.8,1.8,6,1.7c0.6,0,1.3,0.1,1.9,0.2c1.5,0.3,3.1,0.4,4.6,0c0.6-0.2,1.3-0.2,1.9-0.2c3.6,0.4,7.3,0.5,10.8,1.6
		c4.6,1.5,8.7,3.8,10.5,8.9c1.2,3.3,2.5,6.6,3.9,9.8c0.7,1.7,0.8,3.2,0.3,4.9c-0.7,2.2-0.4,4.2,0.6,6.2c2.1,4.2,1.7,8.5,1,12.8
		c-0.7,4.6-1.8,9.2-1.3,13.9c0.3,2.9,0.3,6,2,8.7c2.3,3.6,5.4,6.3,9.1,8.5c0.5,0.3,1.1,0.4,1.6,0.5c1.8,0.3,3.5,0.9,5.2,1.5
		c1.3,0.5,1.9,1.3,1.9,2.8c-0.1,2.4,0,4.8-0.7,7.3c-0.5,1.8,0,3.7-0.4,5.6c-0.2,1-0.5,2-0.4,3c0,0.4-0.2,0.8-0.7,0.6
		c-2.1-1.1-4.4-1-6.7-1c-0.1,0-0.1,0-0.2,0c-1.6,0.1-3-0.4-4.2-1.5c-1.5-1.5-3.4-2.4-4.9-3.7c-1.6-1.3-3.4-2.3-5.1-3.5
		c-1.1-0.7-2.2-0.9-3.5-0.6c-2,0.5-4,1-6,1.1c-1.5,0-1.5,0-2-1.4c-0.6-1.5-0.9-3.1-1.3-4.6c-0.3-1-0.8-1.7-1.7-2.2
		c-1.3-0.8-2.6-1.4-4-1.9c-2.6-1.1-4.9-2.5-7.3-3.9c-1.7-1-3.5-1.8-5.5-2.3C278.4,175,277.4,174.8,276.5,175z" />
                            </a>
                            <a id="05" title="زرین دشت" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st9" d="M338.4,328c2.1,0.6,4.1,1.2,6,2c2.1,0.9,4.3,1.4,6.5,1.9c2.2,0.5,4.2,1.3,6,2.4c1,0.6,1.8,0.5,2.6-0.1
		c0.8-0.6,1.7-1,2.7-0.9c2.5,0.2,4.6-0.8,6.7-2c0.1,0,0.1-0.1,0.2-0.1c2.2-1.5,4.5-1.6,6.8-0.1c1.8,1.1,3.8,1.9,5.9,2.3
		c0.6,0.1,1.1,0.5,1.5,0.9c1.2,1.3,2.4,2.5,3.4,3.9c1.2,1.6,2.8,2.8,4.3,4.1c2.8,2.5,5.9,4.6,9.1,6.4c3.9,2.2,8,3.8,12.5,4
		c1.3,0.1,1.6,0.4,1.5,1.8c-0.1,1.3-0.3,2.7-0.6,4c-0.5,2-0.7,4-1.3,6c-0.5,1.8-0.4,3.7-1,5.5c-0.8,2.5-0.8,5.2-2.1,7.5
		c-0.4,0.8-0.3,1.6,0.3,2.3c3.1,3.7,4.7,8,5.7,12.7c0.3,1.3,0.1,1.7-1.2,2.2c-1.4,0.5-2.7,1.1-4.1,1.6c-1.3,0.5-2.5,1-3.9,0.7
		c-0.2-0.1-0.5,0-0.8,0c-1.9,0.5-3.8,0.7-5.7,0.6c-2.7-0.2-5.3,0.5-8,1.1c-3.8,0.9-7.7,0.6-11.5,0.1c-0.1,0-0.1-0.1-0.2-0.1
		c-3.2-0.1-5.9-1.5-8.7-2.9c-2.8-1.5-5.9-2.4-8.7-4c-3.6-2.1-7-4.6-9.8-7.7c-0.4-0.4-0.9-0.8-1.2-1.2c-1.2-1.2-2.1-2.6-2.2-4.3
		c-0.2-2.6-1.3-5.1-2-7.6c-0.1-0.5-0.6-0.8-1.1-1c-1.3-0.7-2.6-1.4-4.1-1.6c-0.6-0.1-1.1-0.3-1.6-0.6c-2.5-1.4-5-3-7-5
		c-0.5-0.5-0.9-0.9-1.4-1.3c-1.2-0.8-1.9-1.8-2.2-3.2c-0.3-1.4-1.3-2.1-2.6-2.6c-1.3-0.5-2.6-0.9-3.8-1.8c-1.4-1-2-2.5-1.1-3.9
		c1-1.6,1.8-3.4,3.5-4.5c1.6-1.1,3-2.4,4.5-3.6c0.5-0.4,0.9-0.7,1.2-1.2c0.6-0.9,1.3-1.8,2.2-2.4c2.7-1.8,3.5-4.6,4.3-7.5
		C338.1,330,338.2,329.1,338.4,328z" />
                            </a>
                            <a id="17" title="خنج" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st10" d="M302,413.1c-0.2,0.5-0.7,0.6-1.1,0.7c-0.4,0.1-0.9,0.2-1.1,0.4c-0.9,1.8-2.5,1.3-3.9,1
		c-2.5-0.4-4.2,0.4-5.2,2.8c-0.3,0.7-0.9,1.2-1.4,1.8c-0.7,0.8-1.7,0.9-2.7,0.6c-1.1-0.3-2.1-0.5-3.1-1c-0.5-0.3-1.1-0.5-1.6-0.6
		c-1.1-0.3-1.7,0-1.9,1c-0.7,3.1-2.5,5.8-2.9,8.9c-0.2,1.2-1.2,1.7-2.7,1.5c-1.6-0.3-3.3-0.5-4.8-1.2c-1.1-0.6-1.9-0.5-3,0.4
		c-1.4,1.2-3,2.2-4.5,3.2c-1.3,0.9-2.6,1.2-4.1,0.2c-1.7-1.2-3.7-1.8-5.6-2.6c-2.2-0.9-4.5-1.5-6.8-1.9c-3.4-0.5-6.6-1.7-9.8-2.6
		c-1-0.3-2-0.5-3-0.7c-5.2-0.9-9.9-2.7-13.4-6.8c-1.5-1.7-3.4-2.7-5.6-3.2c-3.8-0.8-7.1-2.6-10.2-4.8c-0.1-0.1-0.2-0.1-0.3-0.2
		c-1.6-1.4-1.7-1.5-3.3-0.3c-1.3,1-2.6,1-3.9,0.2c-1.7-1-3.4-2-5.1-3c-3.1-1.9-6.4-3.6-9.3-5.8c-2-1.5-3.7-3.4-5.4-5.2
		c-0.4-0.4-0.8-0.9,0-1.4c0.4-0.2,0.3-0.6,0.4-1c0.2-2,0.6-3.9,1-5.9c0.2-0.7,0.3-1.4,0.1-2.1c-0.7-2.6-0.8-5.2-0.4-7.8
		c0.2-1.3,0.1-2.6,0-3.8c-0.1-1.7,0.7-2.1,2.1-1.3c1.8,1.1,4,1.5,5.9,2.2c3.1,1.2,6.2,2.6,9.1,4.3c3.8,2.2,7.6,4.5,11.4,6.7
		c2.5,1.5,4.8,3.1,7.4,4.4c3.2,1.6,6.3,3.2,9.7,4.1c1.3,0.4,2.4,1.2,3.8,1.4c2.3,0.4,4.2-0.4,6-1.5c0.7-0.4,1.3-1,2-1.5
		c0.5-0.4,0.9-0.8,0.9-1.6c0.1-1.2,0.9-2.3,1.6-3.2c0.6-0.8,1-1.8,0.6-2.7c-0.6-1.3,0.1-1.9,0.9-2.6c0.6-0.5,1.3-0.8,2-1.1
		c1.9-0.9,2.5-1.7,2.1-3.8c-0.2-1.2-0.4-2.4-1.1-3.5c-1-1.7-0.7-2.4,0.9-3.7c2-1.7,4.3-2.2,6.8-1.9c0.1,0,0.1,0,0.2,0
		c1.4,0.1,4.7,3.5,4.6,4.9c-0.2,2.5,1.2,3.8,3.1,5c3.1,1.9,6.6,3.2,9.8,4.8c2.8,1.3,5.6,2.4,8.1,4.4c1.2,1,1.6,2.4,2.4,3.7
		c0.2,0.3,0,0.6-0.2,0.9c-0.8,1.2-0.6,2.4,0,3.6c0.2,0.4,0.7,0.8,0.4,1.4c-0.7,1,0,1.5,0.6,2c1,0.7,2,1.2,3.2,1.6
		c3.2,1,6.3,2.3,9.2,3.8c0.2,0.1,0.4,0.3,0.6,0.5c2.7,2.9,6.4,4.5,9.5,6.8c0.3,0.2,0.5,0.4,0.8,0.6C301.9,412.8,301.9,413,302,413.1
		z" />
                            </a>
                            <a id="6" title="فسا" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st11" d="M325.5,283.1c0.9-1.1,2-1.2,3.1-1.5c2.1-0.5,4.2,0,6.2,0c0.5,0,1,0,1.5-0.2c1.4-0.5,2.3-0.3,3.1,1
		c1.5,2.4,2.3,4.9,1.8,7.8c-0.4,2,1.1,3.2,2.3,4.4c0.7,0.7,1.5,1.3,2.3,1.7c1.8,1.1,3,2.7,4.2,4.4c0.5,0.7,0.4,1.3-0.4,1.5
		c-1.3,0.4-1.9,1.4-2.6,2.4c-2.3,3.7-4.7,7.3-6.8,11.2c-1.3,2.3-2.3,4.8-2.9,7.5c-0.6,2.6-0.7,5.2-1.2,7.8c-0.4,2.2-1.3,4.3-3.5,5.5
		c-1.3,0.7-1.9,2-2.7,3.2c-0.9,1.3-1.1,1.6-2.4,1.1c-3-1-6.1-1.2-9.1-2.1c-2-0.6-3.4-1.7-4.1-3.6c-0.5-1.4-1.2-2.2-2.7-2.6
		c-2-0.5-3.1-2.2-3.9-4c-0.6-1.2-1.2-2.4-1.8-3.6c-0.8-1.5-1.8-2.7-3.5-3.2c-3.3-1-5.6-3.3-7.5-6c-0.7-1-1.3-1.9-2.2-2.7
		c-0.8-0.7-1.4-1.5-1.5-2.6c-0.1-1.2-0.7-2-1.9-2.3c-1.9-0.4-3.1-1.7-4.1-3.3c-1.1-1.8-2.4-3.5-3.6-5.3c-0.6-0.9-1.3-1.6-2.2-2.1
		c-0.6-0.4-1.3-0.9-1.9-1.4c-0.8-0.6-1-1.4-1-2.5c0.2-2.4,0.5-4.9-0.3-7.3c-0.9-2.6-2.3-5-4.6-6.7c-0.9-0.7-1.5-1.7-2.3-2.6
		c-1.2-1.4-2.5-2.8-3.7-4.2c-2-2.4-2.2-5.1-1.9-8c0.2-2.1,0.4-4.2,0.1-6.3c-0.3-2.2-0.1-4.5-0.3-6.7c-0.1-1.7,0.2-1.9,1.8-2.4
		c2.1-0.6,4.1-1.3,6.2-1.9c3.8-1.1,7.6-2,11.3-3.1c1-0.3,2.1,0,2.9-0.7c0.4-0.3,0.9,0,1.2,0.3c3.1,2.2,5.4,5.1,7,8.6
		c0.2,0.5,0.2,1,0.1,1.5c-0.9,2.5-1.8,4.9-3.9,6.8c-1.6,1.5-3.2,3.2-4.8,4.8c-0.4,0.4-0.8,0.7-0.3,1.3c1.7,1.8,2.9,4,4.8,5.5
		c1.9,1.5,3.9,2,6.3,2.4c2.8,0.5,5.8,0.4,8.4,1.7c0.9,0.4,1.7,0.8,2.4,1.5c2.8,2.7,6.3,3.4,10,2.7c1.7-0.3,3.4-0.3,5.1-0.3
		C324.2,280.4,324.8,281.7,325.5,283.1z" />
                            </a>
                            <a id="8" title="کازرون" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st12" d="M122.7,223.6c-0.1,0.6,0.2,1.1,0.6,1.3c1.9,1.3,2.5,3.4,3.2,5.3c0.6,1.6,1.6,2.7,3,3.6
		c1.9,1.2,3.7,2.5,5.9,3.2c1,0.3,1.9,1.2,2.3,2.2c0.7,1.6,1.8,2.9,2.7,4.4c0.9,1.5,1.6,3.1,0.9,4.9c-0.2,0.5-0.2,1.1-0.1,1.5
		c0.5,1.8,0.3,3.7,0.2,5.5c-0.1,2.6,1,4.7,2.3,6.8c1.2,1.9,2.4,4,2.4,6.4c0,1.7-0.2,1.8-1.8,1.9c-1.2,0.1-2.3,0-3.4-0.3
		c-1.1-0.3-2.3-0.4-3.4-0.5c-3-0.2-5.8,0.6-8.6,1.8c-3.4,1.5-6.7,3-9.4,5.5c-0.7,0.7-1.4,1.3-2,2.1c-0.7,0.7-1.2,0.7-1.9-0.1
		c-1.4-1.6-2.9-3.1-4.2-4.7c-2.6-3-5.1-6.1-7.7-9.1c-1.8-2-2.9-4.6-4.4-6.8c-0.5-0.7-0.9-1.4-1.3-2.1c-1-1.7-1.9-3.5-2-5.6
		c0-1.3-0.5-2.5-1.9-3c-0.9-0.3-1.5-1-1.9-1.9c-0.6-1.2-1.4-2.1-2.6-2.7c-0.4-0.2-0.8-0.5-1.1-0.8c-1.1-1.1-2.5-1.8-4.1-2
		c-1.4-0.2-2.6-0.5-3.8-1.2c-0.6-0.3-1.1-0.2-1.6,0.1c-1.6,0.9-3.1,0.7-4.7-0.3c-1.3-0.9-2.5-1.8-3.4-3c-1.7-2.1-3.4-4.2-5.1-6.3
		c-0.4-0.5-0.9-1.1-1.2-1.7c-1.2-2-1.2-2.3,0.4-4c0.8-0.9,1.6-1.9,2.1-3c0.6-1.2,0.5-2.2-0.2-3.3c-0.4-0.5-0.9-1.1-0.4-1.7
		c0.6-0.6,0.9-1.6,2-1.7c1.1-0.1,2.2-0.3,3.2-0.2c1.7,0.1,2.8-0.7,3.4-2.1c1-2.3,2.6-4.1,3.6-6.3c0.3-0.7,0.8-1.1,1.5-1.4
		c3.3-1.2,5.7-3.5,7.4-6.4c0.5-0.8,1.2-1.4,2.2-1.4c1.2,0,1.8-0.7,2.4-1.6c0.9-1.5,2-2.6,3.9-2.8c0.2,0,0.5-0.2,0.7-0.1
		c1.8,0.8,3.7,0.7,5.6,1c0.9,0.1,1.6,0.4,2.2,1c0.5,0.5,0.9,0.9,1.4,1.4c1.7,2,3.7,3.3,6.3,3.9c0.8,0.2,1.5,0.3,2.3,0.3
		c1.6,0.1,2,0.3,2.4,1.9c0.5,1.7,0.6,3.5,0.6,5.3c0,0.9,0.2,1.8,0.5,2.6c1.6,4.3,4.5,7.6,7.8,10.6c0.3,0.3,0.6,0.5,0.9,0.8
		c0.8,1,0.3,2.4-1,2.6c-0.5,0.1-1,0-1.5,0.1C123.6,223.8,123.2,223.3,122.7,223.6z" />
                            </a>
                            <a id="10" title="لامرد" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st13" d="M197.5,413.4c0.7-0.9,1.4-1.3,2.3-1.3c0.9,0,1.1-1.4,2.1-1c0.8,0.3,1.3,1,2,1.4c2.1,1.2,4.1,2.4,6.3,3.4
		c1.4,0.6,3,0.8,4.4,1.2c1,0.3,1.9,0.7,2.6,1.5c2,2.2,4.2,4.2,6.9,5.5c2.6,1.3,5.4,2.3,8.4,2.8c3.2,0.5,6.4,1.5,9.5,2.5
		c1.8,0.6,3.8,0.5,5.6,1.4c0.3,0.2,0.7,0.3,1,0.5c0.7,0.5,0.9,1.4,0.4,2.2c-0.9,1.4-1.5,3-3.2,3.7c-0.4,0.2-0.8,0.5-1.1,0.8
		c-0.4,0.4-1.1,0.5-1,1.3c0.1,0.8,0.6,1.4,1.4,1.7c1.3,0.4,2.6,0.6,3.7,0c2.8-1.6,5.7-1.2,8.7-1.2c0.6,0,1.2,0.1,1.6,0.5
		c1.8,1.5,4.1,2.5,4.9,5c0.2,0.5,0.7,0.7,1.1,1c2.3,1.2,2.5,1.5,1.3,3.8c-0.1,0.1-0.1,0.2-0.1,0.4c0.1,0,0.1,0.1,0.2,0.1
		c2.7,0.7,5.4,1.7,8.3,1.1c0.5-0.1,1-0.3,1.5-0.2c0.5,0,0.9,0.2,1.1,0.7c1,2.2,1.3,4.3,0.1,6.6c-1.1,2.2-2.3,4.3-3.7,6.3
		c-1.1,1.5-0.7,2.8,1,3.5c3,1.2,5.7,2.9,9,3.4c2.9,0.4,5.7,1.5,8.5,2.5c1.9,0.7,3.5,0.7,5.4,0.1c2.9-0.9,5.6-2.5,8.6-3.4
		c1.3-0.4,2.6-0.7,4-0.7c2.8,0,5.7,0.1,8.5,0c2.7-0.1,5.4-0.1,7.9-1.2c0.6-0.3,1.2-0.5,1.6-1c1-1.1,2.9-1.1,3.9-0.2
		c0.4,0.4,0.4,0.9,0.4,1.4c0.1,1.5-0.3,2.9-0.6,4.4c-0.3,1.3-0.3,2.5,0,3.8c0.7,2.6,0.9,5.2,0.1,7.8c-0.3,1.2-0.8,2.1-1.8,2.9
		c-1.8,1.3-3.3,2.9-4.7,4.5c-1.2,1.5-2.7,2.5-4.6,3.1c-2.7,0.8-4.8,2.4-7,4c-0.3,0.2-0.5,0.4-0.7,0.6c-4.2,3.2-4.1,2.9-8.3,1.3
		c-4.3-1.6-8.6-2.8-13-4.1c-1.9-0.5-3.7-1.3-5.3-2.3c-1.9-1.1-3.9-1.3-5.9-1.2c-1.8,0.1-3.7,0.2-5.5-0.1c-2.5-0.5-4.1-2-5.2-4.1
		c-0.1-0.3-0.3-0.6-0.4-0.9c-0.5-2.8-2.5-3.9-4.9-4.8c-4-1.5-7.9-3.3-12-4.7c-1.6-0.6-3.3-0.9-5-1.1c-1.5-0.1-2.9-0.3-4.4-0.5
		c-1.8-0.3-3.5-0.5-5.3-0.6c-1.3-0.1-2.5-0.6-3.6-1.2c-1-0.5-1.1-1.4-0.3-2.2c0.7-0.6,0.9-1.4,1.1-2.2c0.5-2,1.2-3.9,2.8-5.4
		c0.7-0.6,1.2-1.5,1.6-2.4c0.9-2.1,2-2.5,4.1-1.5c0.7,0.3,1.3,0.8,2.1,1c2.8,0.7,5.4-1.3,5.1-4.2c-0.2-1.6,0.1-2.9,1.5-3.7
		c0.2-0.1,0.4-0.3,0.6-0.5c0.9-1.1,0.7-1.4-0.7-1.7c-2.1-0.6-4.3-0.2-6.4-0.7c-0.9-0.2-1.7-0.5-2.4-1c-2.6-1.9-5.4-3.5-8.3-4.8
		c-1.1-0.5-2.2-0.7-3.3-1.1c-2.6-1-5-2.6-7.3-4.3c-3.7-2.7-7.1-5.7-10.5-8.7c-2.3-2-4.8-3.8-7.2-5.8c-1.5-1.2-3.3-2-5.1-2.7
		c-2.1-0.8-2.1-0.7-2-3c0-1.7-0.1-3.3,0-5C199.6,414.7,199.3,413.7,197.5,413.4z" />
                            </a>
                            <a id="11" title="مرودشت" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st14" d="M180.2,179.2c-0.2-1.1,0.3-2,1.2-2.8c1.2-1.1,2.2-2.4,3.6-3.2c0.3-0.1,0.5-0.4,0.7-0.7
		c0.6-0.6,0.7-1.1-0.1-1.6c-1.1-0.7-1.8-1.6-2.5-2.6c-0.4-0.7-1.1-1.1-1.7-1.5c-1.9-1.2-3.7-2.6-5.7-3.6c-1.4-0.7-2.8-1.2-4.4-1.1
		c-1.4,0-2.1,0.5-2.5,1.9c-0.5,1.8-0.6,1.9-2.3,1.6c-2.2-0.4-4.4-1.1-6.4-2.1c-2.5-1.3-5.1-2.5-7.1-4.5c-0.6-0.6-1.4-1.1-2.4-1.2
		c-0.8-0.1-1.4-0.6-2.1-0.8c-2-0.7-2.7-2.4-3.4-4.2c-0.3-0.7-0.6-1.3-0.9-1.9c-0.3-0.6-0.7-0.9-1.4-0.6c-0.6,0.3-0.9-0.1-1.2-0.7
		c-0.9-2.1-0.3-4,0.6-5.9c0.7-1.4,2-2.3,3.1-3.4c1.5-1.4,1.5-1.9,0.2-3.5c-1.2-1.4-2.8-2.2-4.2-3.3c-0.5-0.4-1-0.8-1.6-1.1
		c-1.5-1-1.7-1.7-0.9-3.8c0.1-0.4,0.4-0.7,0.6-1c0.7-1.3,0.7-1.4-0.5-2.4c-0.4-0.4-0.9-0.8-1.3-1.1c-0.6-0.5-1-1.2-1-2
		c0-1.6,0-3.2,0.1-4.8c0.1-1.3,1.8-2.1,3.2-1.6c0.7,0.3,1.3,0.8,1.8,1.3c2.1,2.1,3.2,4.8,4.5,7.4c0.6,1.2,1.1,2.5,1.6,3.7
		c0.4,0.9,1.1,1.6,1.8,2.2c2.4,1.8,4.5,4.1,6.7,6c1.3,1.1,2.7,2,4.4,2.3c2,0.4,3.8,1.2,5.7,1.8c1.4,0.5,2.4,1.6,3.9,2.1
		c2.3,0.8,4.3,2.2,6.4,3.5c2.6,1.7,4.7,4,6.9,6.2c0.8,0.8,1.8,1.5,2.8,1.9c1.2,0.5,2.5,0.6,3.7-0.1c1-0.6,2.2-1,3-2
		c0.6-0.8,1.7-1.1,2.6-1.2c0.7-0.1,1.3-0.2,1.9-0.5c0.5-0.3,1-0.2,1.4,0.2c0.8,0.6,1.7,1.2,2.4,2c1.8,2.1,4.1,3.4,6.9,3.9
		c1,0.2,1.8,0.8,2.6,1.2c2.6,1.4,5.3,2.7,7.3,5c0.5,0.6,1,1.2,1.3,1.9c0.2,0.4,0.4,0.9,0.2,1.3c-1.2,1.9,0,3.3,1,4.5
		c2.7,3.4,6,6.1,9.4,8.7c2.5,1.9,5,3.8,7.5,5.7c1.6,1.1,2.5,2.7,3.5,4.3c1.3,2.1,3,3.9,4.9,5.4c1.5,1.1,2.8,2.5,4.1,3.8
		c0.8,0.8,0.7,1.1-0.4,1.5c-1,0.3-2,0.8-3.1,0.9c-2.2,0.2-4.1,1.4-5.8,2.8c-0.7,0.6-1.2,1.4-1.2,2.3c0.1,1.8-0.1,3.6,1,5.2
		c0.3,0.4,0.4,1,0,1.4c-0.4,0.3-0.8,0-1.2-0.3c-3.1-2.4-6.7-4-10.1-5.9c-2.4-1.3-5.1-2.1-7.6-3.1c-2.4-0.9-4.8-1.8-6.9-3.3
		c-1-0.7-1.9-0.9-3-0.4c-1.4,0.6-2.6,0.3-3.8-0.5c-0.6-0.4-1.2-0.6-2-0.7c-2.8-0.1-5.7-0.2-8.5,0c-2.6,0.2-4.5-1-6.3-2.7
		c-2.3-2.2-3.8-5-5.7-7.5c-0.3-0.5-0.5-1-0.6-1.6c-0.1-0.7-0.6-1-1.1-1.2C181.1,184.2,180.3,182,180.2,179.2z" />
                            </a>
                            <a id="7" title="فیروزآباد" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st15" d="M158.4,265.8c1.9-0.5,3,1.1,4.1,2.2c1.6,1.6,3.3,2.5,5.6,2.5c1.5,0,3.1,0,4.6,0c1,0,1.9-0.4,2.5-1.2
		c0.8-1.2,1.7-2.2,2-3.7c0.1-0.4,0-1,0.6-1.1c0.6-0.1,1.2,0.2,1.5,0.7c0.6,1,1.4,1.7,2.1,2.5c0.8,0.9,1.8,1.6,2.9,2.2
		c1.1,0.6,2.1,1.3,3.1,2c2.4,1.6,5.1,2.6,7.9,3.2c1,0.2,2,0.6,2.6,1.5c2.3,3,5.2,5.5,7.9,8.2c2.1,2.1,3.7,4.4,4.9,7
		c0.8,1.7,1.5,3.5,2.9,5c0.8,0.8,1.6,1.2,2.7,1.2c1.4,0,2.8-0.1,4.2,0.2c1.1,0.2,1.5,0.8,1.6,1.8c0.1,1.1,0,2.2,0.2,3.3
		c0.2,1.1-0.1,2.3,1,3.1c0.3,0.3,0.3,0.8,0.2,1.3c-0.2,1.2-2.4,3.4-3.6,3.3c-2.1-0.1-3.8,0.9-5.7,1.7c-1.2,0.5-2.3,1.1-3.5,1.5
		c-1.2,0.4-2,1.1-2.8,2c-1.2,1.2-1.2,2.7,0,4c0.7,0.8,1.4,1.5,2.3,2.1c4,2.6,7.9,5.2,11.9,7.8c0.9,0.6,1.8,1.3,2.6,2
		c0.5,0.5,0.5,0.9-0.1,1.3c-1,0.7-1.8,1.6-2.6,2.6c-0.7,0.9-1.8,1.1-3,0.6c-1.2-0.5-2.2-1.3-3.2-2.1c-1.5-1.1-3.1-1.6-4.9-1.3
		c-1.2,0.2-1.5,0.6-1.7,1.9c-0.3,2.2,0.3,4.3-0.3,6.5c-0.6,2.2-1.4,4.2-3.3,5.7c-0.4,0.3-0.6,0.3-1.1,0c-2.8-1.8-5.7-3.5-8.8-4.5
		c-2.3-0.8-4.7-1.4-7.2-1.2c-1.1,0.1-1.6-0.3-1.8-1.5c-0.3-1.5-0.8-3-1.3-4.4c-0.4-1.2-0.4-2.2,0.1-3.4c1.6-3.4,1.1-6.9,0.9-10.4
		c0-0.6-0.4-0.9-0.8-1.2c-1.5-1.1-3.1-1.9-4.7-2.7c-0.8-0.4-1.7-0.4-2.6-0.3c-1.4,0.1-2.5-0.3-3.4-1.4c-0.3-0.4-0.7-0.8-1.1-1.1
		c-2.3-2-4.2-4.3-4.9-7.4c-0.2-1.1-1.1-1.9-1.9-2.6c-1.6-1.4-2.5-3.1-3.2-5.1c-0.7-2.3-1.5-4.6-2.2-6.9c-0.5-1.5-1.3-2.8-2.5-3.8
		c-4.7-4.1-8.1-9.2-11.4-14.5c-0.1-0.2-0.3-0.4-0.5-0.6c-1.1-1.4-0.9-2.1,0.5-3.1c0.6-0.4,1.3-0.5,1.9-0.8c1.1-0.6,2.1-1.2,3-2.1
		C154.1,266.5,156.2,265.8,158.4,265.8z" />
                            </a>
                            <a id="18" title="قیر و کارزین" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st16" d="M273.7,364c-1.5,0-2.6,0.1-3.7-0.4c-0.6-0.3-1.1,0-1.7,0.2c-2.4,1-4.8,1.7-7.4,2.1c-1.4,0.2-2.7,0.6-4.1,0.9
		c-2.6,0.6-5.1,0.4-7.6,0.3c-3.7-0.1-6.7,1.3-9.3,3.8c-1.3,1.2-1.2,3,0.2,4.4c0.5,0.5,1,0.9,0.4,1.6c-0.4,0.6,0,0.9,0.3,1.3
		c0.6,0.8,0.3,1.4-0.5,1.9c-0.8,0.5-1.5,1-2.3,1.3c-0.9,0.4-1.5,1.2-2.2,1.8c-0.6,0.6-0.9,1.5-0.6,2.1c0.7,1.6,0.3,2.8-1.1,3.7
		c-0.2,0.1-0.1,0.3-0.1,0.5c-0.4,2.9-1.4,4-4.1,4.4c-1.9,0.3-3.8-0.1-5.7-0.5c-4-0.9-7.6-2.8-11.1-4.7c-3.6-1.9-7.1-4-10.6-6.3
		c-2.1-1.4-4.4-2.4-6.5-3.8c-3.8-2.5-7.9-4.3-12.2-5.6c-1.2-0.4-2.4-0.9-3.6-1.4c-1.3-0.6-1.5-1.3-1-2.6c0.9-2.4,1.8-4.8,2.6-7.2
		c0.5-1.6,1.6-3.1,1.6-4.9c0-0.5,0.5-0.7,0.9-0.9c0.9-0.5,1.9-1.1,2.8-1.6c1-0.6,1.1-1.1,0.3-2c-0.5-0.6-1.1-1.1-1.7-1.5
		c-0.6-0.3-0.7-0.8-0.1-1.2c0.6-0.3,0.6-0.9,0.7-1.5c0.1-1.1,0.2-2.2,0.4-3.2c0.3-1.5,0.3-1.6,1.9-1.8c1.6-0.2,3.2-0.2,4.8,0.3
		c2.2,0.7,4.4,1.2,6.4,2.6c1.5,1.1,3.5,1.6,4.9,3c0.7,0.6,1.5,0.3,2-0.4c2.3-3.1,4.1-6.3,4-10.4c0-0.8,0-1.7,0.1-2.5
		c0.1-0.6,0.4-1,1.3-1c1.9,0,3.3,0.7,4.6,2.1c0.3,0.3,0.5,0.6,0.8,0.8c1.7,1.5,2.9,1.5,4.5-0.1c1.2-1.2,2.5-2.4,3.9-3.4
		c1.1-0.8,2.2-1,3.4-0.5c2,0.7,4,1.4,5.9,2.2c0.7,0.3,1.4,0.5,2.2,0.8c1.6,0.6,3,1.4,4.1,2.8c0.9,1.3,1.9,2.5,3.3,3.5
		c0.7,0.5,1.1,1.2,1.5,2c0.5,1,1.4,1.8,2.1,2.8c2.3,3,5.7,3.9,9.1,5c2.5,0.7,5.1,0.6,7.6,1.2c1.6,0.4,3.1,0.7,4.6,1.3
		c1.1,0.5,2.2,0.7,3.4,0.7c1.9,0,3.7-0.1,5.6,0.3c1.5,0.3,1.4,0.2,1.3,1.7c-0.2,1.8-1.2,3.1-2.1,4.5
		C276.8,364,275.1,364.1,273.7,364z" />
                            </a>
                            <a id="01" title="سپیدان" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st17" d="M193.3,198.9c-3.5,2-6.9,2.8-10.9,1.4c-3.3-1.2-5.8-3.1-8.1-5.6c-2.2-2.3-5.1-3.6-8.2-4.5
		c-3.3-1-6.5-1.9-9.8-2.9c-1-0.3-2-0.5-3-0.5c-1.4,0-2.8,0-4.2,0c-2.3,0.1-3.5,1-4.1,3.2c0,0.1-0.1,0.2-0.1,0.4
		c-0.3,0.9-0.8,1.1-1.5,0.5c-1.8-1.7-3.8-3-5.1-5.2c-0.3-0.6-1.3-0.6-1.2-1.4c0.1-0.7,0.9-1,1.3-1.6c0.4-0.7,1.7-0.7,1.6-1.7
		c0-0.8-1-1.1-1.3-1.8c-0.5-1-1.2-1.8-1.8-2.7c-0.8-1.1-0.9-1.1-2.1-0.5c-0.7,0.4-1.4,0.7-2.1,0.9c-1.6,0.5-3.1,0.2-4.4-0.7
		c-1.3-0.8-2.6-1.7-3.8-2.7c-1-0.8-2-1.1-3.2-1.1c-1.6,0-3.2-0.1-4.8-0.3c-1.8-0.2-3.1-1.1-3.8-2.8c-0.5-1.1-0.2-1.6,1-2
		c3.2-1.2,6.6-1.7,9.7-3.1c0.3-0.2,0.7-0.3,1-0.5c0.2-0.1,0.5-0.3,0.3-0.6c-1-1.6-1-3.5-1.6-5.3c-1.1-3.2-3.4-5.4-6-7.3
		c-1.7-1.3-3.6-2.3-5-3.9c-1-1.1-1-1.1,0-2.1c0.3-0.3,0.6-0.5,0.8-0.8c1.3-1.3,1.3-1.3,0.2-2.7c-1-1.3-0.9-1.8,0.5-2.6
		c0.9-0.5,1.8-1,2.7-1.5c2.2-1.2,3.3-3.4,2.6-5.6c-1.3-3.8,0.3-6.9,2.1-9.9c0.6-0.9,1.3-1.7,1.6-2.8c0.1-0.7,0.7-1.2,1.4-0.4
		c0.3,0.3,0.6,0.4,1,0.4c2.3,0.2,4.6,0.5,6.9,0.4c0.4,0,0.8,0,1.2,0.1c1,0.1,1.7,0.4,1.8,1.6c0.2,2,1,3.7,2.7,4.8
		c0.5,0.3,0.8,0.8,0.2,1.2c-1.4,0.8-0.8,2-0.9,3.1c-0.1,1,0.2,1.8,1,2.5c1.8,1.6,3.6,3.1,5.5,4.5c1.6,1.2,1.6,2.1,0,3.2
		c-0.7,0.4-1.1,1-1.5,1.7c-1.1,1.7-1.6,3.8-2.9,5.4c-0.1,0.1-0.2,0.4-0.2,0.5c0.8,1.4,0.8,3.3,2.6,4c1.6,0.7,2.9,1.6,3.5,3.5
		c0.4,1.2,1.4,2.2,2.8,2.7c2.5,0.9,4.7,2.1,6.9,3.7c3.1,2.2,6.6,3.7,10.2,4.9c0.5,0.2,1,0.3,1.5,0.5c2,0.6,3.1,0.1,4.1-1.8
		c0.7-1.5,0.7-1.4,2.4-1.4c3.1,0,5,2,7.2,3.7c1.1,0.9,1.7,2.1,2.8,2.9c0.7,0.6,0.4,1.3-0.2,1.8c-1.1,1.1-2.2,2.2-3,3.5
		c-1.5,2.5-1,7.4,1.6,9.3c2.3,1.7,3.9,4.1,5.4,6.4C188.3,194.5,190.4,196.9,193.3,198.9z" />
                            </a>
                            <a id="20" title="خرم بید" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st18" d="M216.2,90.6c0.7,1,1.5,1.2,2.3,1.5c1.6,0.5,3,1.4,4.5,2.2c3.2,1.6,6.7,2.8,10.3,3c3.4,0.2,6.4-1.2,8.4-4.1
		c1.9-2.7,4.2-5,6.4-7.4c0.7-0.7,1.3-0.4,1.9-0.1c2,1,4.1,2,6,3c1.2,0.6,2.4,1.1,3.9,0.9c1.2-0.1,2.1,0.4,2.9,1.2
		c0.9,0.9,1.6,1.8,2.2,2.9c0.7,1.4,1.4,2.8,2.1,4.1c0.3,0.5,0.9,0.9,0.7,1.5c-0.3,0.8-0.6,1.7-1.4,2.1c-2.7,1.5-4.6,3.8-6.4,6.3
		c-0.8,1.1-1.8,2-2.7,3c-1.5,1.7-2.6,3.7-3.3,5.8c-0.6,1.8-0.3,2.3,1.5,3.1c1.5,0.6,2.9,1.4,4.4,1.9c1,0.3,1.6,1.2,2.4,2.3
		c-0.3,0.2-0.7,0.3-0.9,0.6c-0.9,1.1-0.9,2.3,0.1,3.4c0.3,0.3,0.7,0.6,1,0.9c2,1.9,1.9,2.4,0,4.4c-0.7,0.7-1.7,1-2.1,2
		c-0.3,0.6-0.5,1.1-0.3,1.8c0.8,2.7,0.3,5.5,0.3,8.2c0,0.8-0.4,1.5-1.4,1.5c-0.5,0-1,0.2-1.5,0.4c-1.9,0.7-2.3,1.4-1.9,3.4
		c0.1,0.7,0.3,1.4,0.3,2.1c0,0.9,0.1,1.7,0.7,2.5c0.6,1,0.4,1.1-0.8,1.4c-1.7,0.4-3.4-0.1-5.1,0.2c-0.8,0.1-1.3-0.4-1.6-1.2
		c-0.3-0.7-0.7-1-1.4-1.1c-1.5-0.4-1.7-0.8-1.4-2.4c0.3-1.6,0.6-3.1,1-4.7c0-0.1,0.1-0.2,0.1-0.4c0.6-2.4,0-3.2-2.5-3.3
		c-0.8,0-1.5-0.1-2.3-0.4c-0.8-0.3-1.7-0.5-2.6-0.2c-0.5,0.2-0.9-0.1-1.3-0.2c-1-0.3-1.9-0.4-3-0.2c-1.8,0.2-3.6-0.1-5.3-0.4
		c-2.2-0.3-4.2,0.2-6.2,0.6c-0.6,0.1-1.1,0.4-1.6,0.5c-2.3,0.6-4.4,0.3-5.9-1.7c-1.3-1.7-2.7-3.5-3.5-5.5c-1-2.5-0.9-3.1,1-4.9
		c1.5-1.4,2.7-2.9,3.2-4.9c0.4-1.3,1.4-2.3,2.1-3.4c1.4-2.4,1.4-2.6-1-4.1c-1.5-0.9-3-1.7-4.4-2.8c-1.9-1.6-2.3-3.5-1-5.6
		c1.2-1.9,2.3-3.8,3.9-5.5c1.4-1.5,2-3.2,1.3-5.3c-0.7-1.9-0.6-4-1.2-6C216.8,92.5,216.3,91.9,216.2,90.6z" />
                            </a>
                            <a id="010" title="مهر" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st19" d="M197.8,420c-0.1,1.2,0.2,2.4,0,3.6c-0.1,1,0.5,1.6,1.5,1.7c2.7,0.3,5,1.6,7.2,3c0.7,0.5,1.3,1.1,1.9,1.6
		c3,2.9,6.7,5.1,9.6,8c3,3,6.6,5.1,10.1,7.3c1.8,1.2,3.9,1.8,6,2.5c2.6,0.8,5.1,2.1,7.3,3.9c1.5,1.2,3,2.2,4.9,2.4
		c0.3,0,0.5,0.1,0.7,0.2c2.5,0.7,2.8,1.1,2,3.5c-0.4,1.1,0,2.4-1,3.4c-0.8,0.9-1.3,1.2-2.4,0.6c-1.3-0.7-2.7-1.5-4-2.3
		c-1.1-0.6-1.3-0.6-1.9,0.7c-0.6,1.3-1.6,2.5-1.9,4c-0.1,0.4-0.6,0.8-1,1.2c-1.4,1.4-2.3,3-2.9,4.9c-0.2,0.7-0.5,1.3-0.8,2
		c-0.6,1.5-1.6,1.8-3,0.8c-0.3-0.2-0.5-0.4-0.7-0.7c-1.4-2.1-3.3-3.9-4.9-6c-2.8-3.7-6.3-6.7-10.1-9.4c-1.4-0.9-2.8-1.9-4-3.1
		c-3.1-3-7-4.8-11-6.1c-2.6-0.8-4.7-2.1-6-4.5c-0.8-1.5-2-2.5-3.5-3.2c-1.4-0.6-2.5-1.6-3.5-2.7c-1.1-1.2-2.4-2.2-3.9-2.7
		c-1.1-0.4-1.9-1-2.6-2c-0.6-0.8-1.1-1.7-1.8-2.2c-1.9-1.5-2.1-3.5-1.7-5.6c0.6-2.6,0.1-5.1-0.3-7.6c-0.1-0.9-0.6-1.7-1.1-2.4
		c-2.5-3.9-5-7.8-7.6-11.6c-0.5-0.8-0.9-1.7-1.1-2.7c-0.3-1.8,0.6-3,2.4-3.1c1.7-0.1,3.5,0.2,5.1-0.4c0.5-0.2,1,0.1,1.4,0.5
		c0.8,0.7,1.8,1.3,2.5,2.1c3,3.5,7.1,5.5,10.9,7.8c1.4,0.8,2.8,1.8,4.2,2.6c0.9,0.5,1.5,1.3,1.3,2.3c-0.2,0.9,0.3,1.1,1,1.3
		c2.1,0.6,2.3,1,2.3,3.1C197.7,417.9,197.8,419,197.8,420z" />
                            </a>
                            <a id="15" title="استهبان" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st20" d="M326.1,253.8c0.6,2.9,1.8,5.4,1.4,8.3c-0.1,0.8-0.1,1.5,0,2.3c0.2,1.8,0.9,2.5,2.7,2.9c1.6,0.3,3.1,1,4.2,2.2
		c0.6,0.7,1.3,1.2,2.1,1.7c1,0.6,1.9,1.1,2.9-0.1c0.6-0.7,1.4-0.4,2,0.3c0.9,0.9,1.8,1.8,2.6,2.9c1,1.4,2.3,2.3,3.9,2.6
		c2.7,0.5,5.3,1.4,8,2.2c0.6,0.2,1.1,0.2,1.7,0c3.8-1.2,6.6,0.3,9,3.1c1.1,1.3,2.2,2.4,3.8,3.2c1.4,0.6,2,2.1,2.8,3.3
		c0.4,0.6,0.1,1.1-0.1,1.6c-1.5,3.4-3.1,6.6-5.4,9.5c-0.4,0.4-0.6,1-0.8,1.5c-0.3,0.8-0.8,1.4-1.4,2c-0.6,0.6-1,0.7-1.9,0.3
		c-2.2-1-4.5-1.8-6.8-2.5c-1.2-0.4-2.5-1-3.9-0.4c-0.7,0.3-1.1-0.3-1.4-0.8c-1.4-2.5-3.6-4.2-6-5.6c-0.3-0.2-0.6-0.4-1-0.6
		c-1.1-0.6-1.7-1.4-1.1-2.8c0.2-0.5,0.3-1.2,0.1-1.7c-0.8-2.6-1.3-5.3-2.9-7.5c-0.4-0.6-0.7-1.2-1.1-1.8c-0.8-1.3-1.8-1.4-3-0.5
		c-1.2,0.9-1.4,1-2.7,0.1c-0.9-0.6-1.5-0.6-2.4-0.1c-1.5,0.9-3.2,0.8-4.9,0.7c-0.2,0-0.4,0-0.6-0.1c-2.3-1.9-5.1-1.2-7.7-1.3
		c-2.1-0.1-4.2,0.1-6.3,0c-1.6-0.1-2.7-0.8-3.7-1.8c-1.9-1.9-4.5-2.4-7-2.9c-1.9-0.4-3.8-0.5-5.6-1.3c-0.2-0.1-0.5-0.3-0.7-0.2
		c-2,0.5-3.3-0.7-4.5-2c-0.7-0.7-1.3-1.4-1.9-2.1c-0.8-1-0.8-1.7-0.1-2.8c0.3-0.4,0.6-0.9,1-1.2c1.8-1.6,3.2-3.5,4.7-5.4
		c1.2-1.4,1.6-3.3,2.2-5.1c0.1-0.3-0.1-0.6-0.2-0.9c-1.1-3-2.7-5.8-5.2-8c-0.1-0.1-0.2-0.2-0.3-0.3c-2.6-2.6-2.5-2.6-0.6-5.7
		c0.4-0.7,0.9-0.8,1.6-0.7c0.8,0.1,1.2,0.7,1.7,1.2c1.4,1.5,2.8,3,4.4,4.3c3.1,2.7,6.8,4.4,10.2,6.5c2.2,1.3,4.5,2.1,7,2.5
		c1.9,0.3,3.5,1.3,5.1,2.2c1.6,0.9,3,0.8,4.5,0C324.8,254.7,325.3,254.3,326.1,253.8z" />
                            </a>
                            <a id="001" title="سروستان" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st21" d="M274.4,286.1c-2.1,0.5-3.9,0.7-5.9,0.6c-3.4-0.2-6.8-0.1-10.2-0.1c-1.7,0-3.5-0.1-5.2,0
		c-1.3,0.1-2.4-0.2-3.4-1.1c-3.2-2.8-7-4.8-10.6-7c-1-0.6-1.8-1.7-3.1-1.8c-0.1,0-0.3-0.2-0.4-0.1c-1.8,0.4-2.8-0.9-3.8-2.1
		c-1.6-1.8-3.5-3.2-5.2-4.8c-0.2-0.2-0.5-0.4-0.8-0.5c-2.1-0.6-3.6-2-4.9-3.6c-0.6-0.8-1.3-1.4-2.4-1.5c-0.9-0.1-1.5-0.6-2.1-1.3
		c-2.3-2.3-4.3-4.7-5.9-7.5c-1.1-2.1-2.4-4.1-4-5.9c-1.2-1.4-1.1-2.4-0.2-4c0.9-1.5,2.3-2.3,3.7-3.3c0.4-0.3,0.8-0.1,1.1,0
		c1.6,0.7,3.4,1.1,4.8,2.4c0.9,0.9,2.2,1.3,3.4,1.7c0.5,0.1,1.1,0.3,1.5,0.1c2.7-1.4,5.2,0.1,7.7,0.7c0.8,0.2,1.5,0.6,2.4,0.8
		c1.5,0.3,2.6,1.4,3.6,2.5c0.7,0.8,1.3,0.8,2.2,0.4c2.5-1.1,5-1.5,7.5,0.1c0.5,0.3,1.1,0.5,1.6,0.6c2.2,0.4,4.1,1.3,6,2.4
		c0.9,0.5,1.6,0.4,2.5-0.2c1.3-0.9,2.4-1.9,4-2.2c2.1-0.4,3.5,0.4,3.6,2.5c0.1,2.8-0.1,5.5,0,8.3c0.1,3.3-0.5,6.8,1.4,9.9
		c0.2,0.3,0.3,0.6,0.5,0.8c3.1,2.9,4.6,6.9,8,9.6C273,283.4,273.8,284.7,274.4,286.1z" />
                            </a>
                            <a id="16" title="گراش" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st22" d="M344.4,439.7c-1.6-0.1-2.9-0.3-4.3-0.8c-3.3-1.3-6.4-0.6-9.5,0.8c-2.4,1.2-5,1.6-7.7,1.6c-2.1,0-4.1,0-6.2,0
		c-1.9,0-3.7,0.4-4.9,2.1c-0.9,1.1-2,1.4-3.4,1.3c-1.8-0.2-3.5-0.6-5.2-1.2c-2.2-0.7-3.1-0.3-3.8,1.9c-0.4,1.3-1,2.5-1.5,3.7
		c-0.5,1.2-0.4,1.3,0.8,1.9c0.6,0.3,1.1,0.7,1.7,0.9c1.8,0.6,2.4,2.3,3.3,3.6c0.3,0.4,0.3,1,0,1.5c-0.3,0.5-0.8,0.5-1.4,0.4
		c-1.5-0.2-2.9-0.7-4.1-1.6c-2.7-2.1-5.9-3.7-8.1-6.5c-0.9-1.1-2.1-1.5-3.6-1.1c-2,0.5-2.9-0.1-3.9-1.9c-0.3-0.6-0.7-1.2-1.3-1.3
		c-2.2-0.6-4.2-1.8-6.6-1.8c-1.5,0-3.1-0.3-4.4-1.2c-1.2-0.8-2.7-0.3-3.1,1.3c-0.1,0.2-0.2,0.5-0.3,0.7c-0.3,0.6-0.9,0.9-1.3,0.2
		c-1-1.8-2.9-2.7-4.1-4.3c-0.9-1.2-2.3-1.7-3.7-1.8c-3.3-0.1-6.6-0.3-9.6,1.3c-0.3,0.2-0.7,0.3-1.1,0.1c-0.4-0.2-0.8-0.5-0.4-1
		c0.2-0.2,0.5-0.4,0.7-0.6c1.4-1.2,2.4-2.7,3.3-4.2c0.2-0.4,0.5-0.8,0.7-1.1c0.4-0.6,1-0.8,1.7-0.3c0.8,0.7,1.8,0.9,2.8,1.3
		c1.3,0.5,2.4,1.3,3.2,2.4c0.5,0.7,1,0.7,1.7,0c2-2,4.6-3.2,6.7-5.2c0.6-0.6,1.2-0.3,1.9,0c2.2,1,4.5,1.7,7,1.6c1.1,0,1.6-0.4,2-1.4
		c0.6-2,1.1-3.9,1.5-6c0.2-1.3,0.9-2.4,1.6-3.5c0.4-0.7,1-0.9,1.8-0.5c3.3,1.6,6.8,2.6,10.3,3.7c1.2,0.4,2.4,0.8,3.6,1.3
		c0.5,0.2,1,0.4,1.5,0.1c2.2-1,4.7-1.4,7-2.2c2.6-0.9,5.1-0.1,7.5,0.8c2.1,0.8,4.2,1.3,6.3,1.9c0.9,0.3,1.8,0.3,2.7,0.3
		c1.2,0,2.3,0.1,3.4,0.4c1.3,0.4,2.7,0.2,4.1,0.7c1.5,0.5,3.1,0.8,4.6,1.5c1.6,0.8,3.3,0.7,4.9,0.6c1,0,1.6-0.7,1.9-1.6
		c0.4-1.1,0.7-2.2,1.1-3.3c0.7-1.8,1.8-2.3,3.7-1.8c0.7,0.2,1.5,0.3,2.2,0.6c1,0.4,1.4,1.4,0.6,2.1c-0.9,0.9-0.9,1.9-0.9,3
		c-0.1,1.3,0,2.7,0,4.1c0,2.3-0.8,4.2-1.9,6.2C345.4,439.9,344.9,439.7,344.4,439.7z" />
                            </a>
                            <a id="011" title="پاسارگاد" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st23" d="M211.5,155.2c2-0.4,2.9-1.7,3.8-3c1-1.5,1.9-3.1,3.1-4.4c0.3-0.3,0.5-0.6,0.5-1c0-1.2,0.7-1.6,1.8-1.8
		c2.3-0.4,4.5-0.9,6.8-1.4c0.8-0.2,1.5-0.1,2.3,0c3.9,0.9,7.8,1.6,11.8,1.2c1.2-0.1,2.4,0,3.6,0.3c0.9,0.2,1,0.8,0.7,1.4
		c-0.7,1.2-0.8,2.5-1,3.9c-0.1,1.1-0.5,2.1-1.3,2.9c-0.4,0.4-0.4,0.8,0.2,1.1c0.9,0.4,1.7,0.9,2.6,1.3c0.4,0.2,0.9,0.3,1.1,0.7
		c0.5,1.2,1.6,1.3,2.7,1.3c1.7,0,3.5,0.2,5.2,0.1c1.5-0.1,2.2,0.5,2.7,1.9c0.4,1.2,0.9,2.3,1.4,3.4c1,2,2.2,3.9,3.9,5.4
		c1.9,1.7,3.6,3.6,5,5.8c1.3,2,3,3.1,5.5,2.9c1.2-0.1,1.9,0.6,2.4,1.6c1.4,2.7,0.3,6.2-2.6,7.7c-1.3,0.7-2.6,1.4-4,1.8
		c-1.1,0.2-2,0.2-2.8-0.7c-1.1-1.3-2.6-2-4.3-2.3c-1-0.1-1.8-0.6-2.5-1.3c-1-1.1-2-2.1-2.8-3.4c-1.1-1.9-2.8-2.3-5-1.9
		c-2.9,0.5-4.8,2.2-6.1,4.7c-1,1.9-2.2,3.6-4.3,4.6c-1.3-4-4.8-5.8-7.9-8c-3.9-2.8-7.9-5.5-11.1-9.2c-0.9-1-1.6-2-1-3.4
		c0.2-0.4,0-0.9-0.1-1.3c-0.4-1.4-1-2.8-1.7-4.1c-0.8-1.4-2.4-1.8-3.6-2.8c-1.3-1.1-3.1-1.6-4.2-3
		C212.1,156,211.9,155.7,211.5,155.2z" />
                            </a>
                            <a id="0001" title="خرامه" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st24" d="M228.2,242c0.2-1,0.1-1.6-0.5-2.2c-0.8-0.8-0.6-1.3,0.6-1.4c1.3-0.1,2.5-0.6,3.7-0.8c1.7-0.3,2.8-1.4,3.9-2.6
		c0.4-0.4,0.4-0.8,0-1.2c-0.5-0.5-1-1-1.5-1.5c-1.2-1.1-1.7-2.3-1.3-4c0.3-1.5-0.2-3,0.3-4.6c0.9-2.9,1.9-5.9,3.6-8.5
		c0.6-1,1.6-1.4,2.8-1.1c2.7,0.9,5.4,2,8.2,2.8c4,1.1,8,2.4,12.2,1.9c1.9-0.2,3.8,0,5.8,0c1,0,1.2,0.7,1.3,1.4
		c0.2,1.3,0.5,2.5,1.1,3.7c0.5,0.9,1,1.8,1.8,2.5c2.8,2.6,5.8,4.8,9.7,5.7c2.5,0.6,4.9,1.6,7.1,2.8c1.6,0.9,2,1.5,1.2,3.5
		c-0.9,2.1-2.4,3.7-4.7,4.4c-4.6,1.3-9.3,2.3-13.9,3.8c-1,0.3-2.1,0.7-3.1,1.1c-2.2,0.8-4.5,1.3-6.9,1.6c-2,0.3-3.7,1.1-5.3,2.2
		c-0.6,0.4-1,0.4-1.7,0.1c-2.2-1-4.6-1.7-6.9-2.3c-0.5-0.1-1-0.2-1.5-0.5c-2.2-1.6-4.7-1.1-7.1-0.8c-0.3,0-0.6,0.2-0.8,0.4
		c-0.3,0.4-0.6,0.2-1,0c-1.5-1.2-3.2-2.1-5-2.8C228.5,245.2,228.2,243.5,228.2,242z" />
                            </a>
                            <a id="3" title="ارسنجان" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st25" d="M264.5,217.5c-0.3,0-1,0.1-1.7-0.1c-2.4-0.7-4.8-0.6-7.3-0.6c-1.7,0-3.4-0.1-5.1-0.7c-2-0.7-4.1-0.9-6.1-1.7
		c-0.8-0.3-1.4-0.6-1.4-1.6c0-0.7-0.3-1.2-0.6-1.8c-0.7-1.4-1-2.9-1-4.5c0-1,0.3-1.8,1.2-2.2c1.2-0.6,2.4-1.3,3.8-1.7
		c1.9-0.4,3.8-1,5.3-2.5c1.1-1.2,1.2-2.2-0.1-3.4c-1.4-1.2-3-2-4.4-3.2c-0.8-0.7-1.4-1.5-2.1-2.3c-0.6-0.6-0.7-1.5-0.3-2.3
		c1.3-2.8,2.8-5.5,4.8-7.9c1-1.2,5.2-1.1,6.2,0.1c1,1,2.1,2,2.8,3.2c0.9,1.5,2.3,2.1,3.8,2.4c1.3,0.3,2.4,0.7,3.3,1.8
		c0.4,0.5,1.1,0.8,1.7,1.2c0.8,0.5,1.7,0.4,2.6,0.1c1.6-0.7,3.2-1.4,4.7-2.1c2.5-1.2,3.5-3.5,4-6c0.2-1.1-0.3-2-0.8-2.9
		c-0.4-0.8-0.1-1.1,0.8-1.3c1.4-0.2,2.7,0,3.9,0.7c2,1.1,4.1,2.1,6,3.4c1.3,0.9,3,1,4.4,1.9c1.5,1,3,1.9,3.4,3.9
		c0.1,0.6,0.6,1.1,0.8,1.7c0.3,1-0.1,1.6-1.1,1.3c-1-0.3-1.7,0.3-2.6,0.5c-3.4,0.7-6.4,2.1-9.3,4c-0.9,0.6-1.7,1.2-2.3,2
		c-0.9,1.1-1.5,2.2-0.7,3.7c0.5,1,0.6,2.2,1.3,3.2c0.4,0.6,0.3,1.4-0.2,2c-1,1.2-2,2.5-3.1,3.7c-0.7,0.7-1.7,1.1-2.7,1.3
		c-2.8,0.5-5.3,1.5-7.2,3.7c-0.3,0.3-0.7,0.7-0.8,1.1c-0.4,1.6-1.5,2.1-3.1,2C265.3,217.4,265.1,217.5,264.5,217.5z" />
                            </a>
                            <a id="19" title="کوار" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st26" d="M230.2,274.9c-1,0.6-1.7,0.5-2.4,0.1c-1-0.6-1.9-0.4-2.8,0.2c-1.8,1.2-3.7,2.3-4.8,4.3
		c-0.3,0.5-0.7,0.9-1.3,1.2c-2.1,1-3.9,2.6-6.4,3c-0.7,0.1-1.5,0.2-2,0.6c-1.6,1.1-2.6,0.4-3.7-0.8c-2.5-2.6-5.3-5-7.7-7.8
		c-1.5-1.8-3.5-2.7-5.6-3c-2.6-0.3-4.7-1.7-6.8-2.9c-0.6-0.4-1.2-0.9-1.9-1.3c-0.9-0.6-1.8-1.3-2.4-2.2c-0.8-1.3-1.7-2.3-2.8-3.4
		c-1-1-1.1-1.5-0.4-2.7c1.6-2.8,2.4-5.9,3.5-8.9c0.2-0.4,0-0.8-0.1-1.1c-0.1-0.3-0.4-0.5-0.5-0.8c-0.1-0.3-0.2-0.6,0.1-0.9
		c0.3-0.3,0.6-0.2,0.9,0.1c1.8,1.5,4,2.3,6,3.3c2.4,1.2,5,1.4,7.5,0.6c1.7-0.5,3.4-1.2,4.9-2.3c2.2-1.6,3.3-1.3,4.9,0.9
		c2.5,3.4,4.4,7,6.8,10.5c1.1,1.7,2.5,2.8,4.3,3.6c1.1,0.5,2.1,1.1,2.8,2.2c0.7,1.2,1.9,1.7,3.1,2.1
		C226.2,270.5,228,272.7,230.2,274.9z" />
                            </a>
                            <a id="013" title="رستم" onmouseover="ShowName(this)" onclick="PageRedirect(this)" onmouseout="ClearText()">
                                <path class="st27" d="M91.5,145.1c0,0.9-0.2,1.6-0.5,2.2c-0.9,1.5-0.6,2.8,0.1,4.2c0.9,1.7,1.6,3.5,3,4.9c0.8,0.8,0.5,2.1-0.6,2.6
		c-1.4,0.6-2.9,1-4.2,1.8c-0.6,0.4-1.2,0.1-1.6-0.2c-1-0.7-2.2-1.4-2.5-2.8c-0.3-1.1-1-1.4-2-1.4c-1.9,0-3.9,0-5.8,0.1
		c-0.6,0-1.2,0.1-1.7,0.7c-0.8,0.8-1.3,0.7-1.6-0.4c-0.4-1.8-1.5-2.6-3.1-3c-1.3-0.3-2.5-1-3.9-0.5c-0.4,0.1-0.9-0.1-1.3-0.2
		c-1.1-0.3-1.3-0.5-0.6-1.4c0.7-0.9,0.3-1.4-0.3-2c-0.6-0.6-1.4-1.2-2-1.8c-1.4-1.3-1.4-2.2,0-3.5c0.9-0.8,1.8-1.5,2.6-2.3
		c0.8-0.7,1-1.6,0.4-2.6c-0.5-0.8-0.9-1.5-1.5-2.2c-1.4-1.5-2.8-3-4.1-4.6c-1.2-1.3-2.1-2.7-2.5-4.5c-0.3-1.2-1.1-2.2-1.7-3.2
		c-1.2-1.9-2.1-4-3.4-5.8c-0.6-0.7-0.5-1.4,0.1-2.1c0.8-0.9,1.5-1.8,2.2-2.7c0.7-0.9,1.3-1.8,1.7-2.8c0.4-1.1,0.7-1.1,1.5-0.2
		c0.4,0.4,0.7,0.9,1,1.4c1.5,2.7,3.9,4.2,6.7,5.1c2,0.7,3.8,1.8,5.6,2.9c1.6,1,2,2.2,1,3.7c-1.2,1.7-0.7,3.1,0.2,4.7
		c1.2,2,2.5,3.5,5.1,3.5c1.2,0,2.6,0.2,3.7,0.6c1.9,0.7,3.7,1.7,5,3.4c1.7,2.2,2.8,4.7,4.6,6.9C91.4,144.1,91.5,144.8,91.5,145.1z" />
                            </a>
                        </g>
                    </svg>

                </div>
            </div>
            <%--*******************فصل نامه گزارش**********************--%>
            <div id="divMag" runat="server" class="dyn-col ">
                <div class="mag-title" runat="server" id="divMagazinTitle">
                    <span runat="server" id="lblMagazinTitle">فصل نامه گزارش</span>
                </div>
                <div class="mag-panel">

                    <div class="row">
                        <div class="col-md-6 col-sm-6">
                            <div class="row">
                                <div class="hovereffect">
                                    <img runat="server" class="img-responsive magImg" id="ImageMagazin">
                                    <div class="overlay2">
                                        <h2><span runat="server" id="lblMagazinTitle2">فصل نامه گزارش</span></h2>
                                        <a class="info" runat="server" id="LinkMagazin">مشاهده</a>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="hovereffect">
                                    <img runat="server" class="img-responsive magImg" id="ImageMagazinPre">
                                    <div class="overlay2">
                                        <h2><span runat="server" id="lblMagazinTitlePre">فصل نامه گزارش</span></h2>
                                        <a class="info" runat="server" id="LinkMagazinPre">مشاهده</a>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-md-6  col-sm-6">
                            <div class="hovereffect">
                                <img class="img-responsive magImg" src="/Images/HomePage/MagArchive.png" />
                                <div class="overlay2">
                                    <h2>آرشیو</h2>
                                    <a class="info" id="linkMagazinArchive" runat="server">مشاهده</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <a target="_blank" href="/help/Magazin.pdf" style="padding-top: 10px; text-decoration: none"><i class="fas fa-file-pdf" style="font-size: 24px; color: #a7414a"></i>شیوه نامه پذیرش مقاله ی فصل نامه گزارش</a>

                    </div>

                </div>
            </div>


            <%--************poll & Condolence **Temporary_Item*************--%>

            <%--*******************تبریک و تسلیت**********************--%>
            <div id="divCondolence" runat="server" class="dyn-col ">
                <div class="con-title" runat="server" id="div1">
                    <span runat="server" id="Span1">تبریک و تسلیت</span>
                </div>
                <div class="con-panel">

                    <div class="row">
                        <section id="rpPromo3" class="rp-row-promo">
                            <div id="CongCarousel" class="carousel slide" style="margin: 0px auto; width: 90%;" data-ride="carousel">
                                <%--Wrapper for slides --%>
                                <div class="carousel-inner" role="listbox">
                                    <ol class="carousel-indicators">
                                        <li data-target="#CongCarousel" data-slide-to="0" class="active"></li>
                                        <li data-target="#CongCarousel" data-slide-to="1"></li>
                                        <li data-target="#CongCarousel" data-slide-to="2"></li>
                                    </ol>
                                    <asp:Repeater runat="server" ID="RepeaterCondolence">
                                        <ItemTemplate>
                                            <div class='<%# Container.ItemIndex == 0 ? "item active" : "item" %>'>
                                                <img class="img-responsive" id="ImageNews" runat="server" src="/Images/HomePage/cond.jpg" alt="" />
                                                <div class="carousel-caption">
                                                    <h4><%#Eval("Summary") %> </h4>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                                <!-- Left and right controls -->
                                <a class="left carousel-control" href="#CongCarousel" data-slide="prev">
                                    <span class="glyphicon glyphicon-chevron-left"></span>
                                    <span class="sr-only">Previous</span>
                                </a>
                                <a class="right carousel-control" href="#CongCarousel" data-slide="next">
                                    <span class="glyphicon glyphicon-chevron-right"></span>
                                    <span class="sr-only">Next</span>
                                </a>
                            </div>
                        </section>
                    </div>
                </div>
            </div>
            <%--*******************نظرسنجی**********************--%>
            <div id="divPollUniq" runat="server" class="dyn-col ">
                <div class="mag-title" runat="server" id="div2">
                    <span runat="server" id="Span2">نظرسنجی</span>
                </div>
                <div class="mag-panel">

                    <div class="row">
                        <TSPControls:CustomAspxDevDataView ID="DataViewPoll" ClientInstanceName="DataViewPoll"
                            runat="server" ColumnCount="1" RowPerPage="25" Width="100%"
                            RightToLeft="True" ItemSpacing="0px" PagerStyle-ItemSpacing="0px" Border-BorderStyle="None"
                            Visible="false" OnCustomCallback="OnCustomCallback_DataViewPoll" PagerSettings-EndlessPagingMode="OnClick">

                            <ItemTemplate>
                                <table class="TableBorder" width="100%">
                                    <tbody>
                                        <tr>
                                            <td class="TableTitle" align="right" colspan="2">
                                                <dx:ASPxLabel ID="lblPollId" runat="server" Text='<%# Bind("PollId") %>' Visible="false">
                                                </dx:ASPxLabel>
                                                <dx:ASPxLabel ID="lblIsResualtPublic" runat="server" Text='<%# Bind("IsResultPublic") %>'
                                                    Visible="false">
                                                </dx:ASPxLabel>
                                                <dx:ASPxLabel ID="lblTitle" runat="server" Text='<%# Bind("Tittle") %>' Font-Bold="True"
                                                    Width="100%">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right" colspan="2">
                                                <dx:ASPxLabel ID="lblQustion" runat="server" Width="100%" Text='<%# Bind("Question") %>'
                                                    Wrap="True">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="top" colspan="2">
                                                <dx:ASPxRadioButtonList CssPostfix="Glass"
                                                    Border-BorderColor="Transparent" ID="rdbChoise" TextField="ChoiseName" ValueField="ChoiseId"
                                                    runat="server" Width="100%" DataSourceID="objdsChoise">
                                                </dx:ASPxRadioButtonList>
                                                <asp:ObjectDataSource ID="objdsChoise" runat="server" SelectMethod="FindByQuestionId"
                                                    TypeName="TSP.DataManager.PollChoiseManager" OldValuesParameterFormatString="original_{0}">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="btnSavePollAnswer" DefaultValue="-1" Name="QuestionId"
                                                            PropertyName="CommandArgument" Type="Int16" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top">
                                                <TSPControls:CustomAspxButton runat="server"
                                                    Text="ثبت" ImagePosition="left" Width="50%"
                                                    ID="btnSavePollAnswer" ClientInstanceName="btnSavePollAnswer" CommandArgument='<%# Bind("QuestionId") %>'
                                                    UseSubmitBehavior="False" AutoPostBack="false" CausesValidation="true" ValidationGroup="PollChoise"
                                                    OnLoad="btnSavePollAnswer_OnLoad">
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right" valign="top" width="50%">
                                                <TSPControls:CustomAspxButton runat="server"
                                                    Text="نتایج" ImagePosition="left" Width="50%"
                                                    ID="btnResultView" ClientInstanceName="btnResultView" CommandArgument='<%# Bind("PollId") %>'
                                                    UseSubmitBehavior="False" AutoPostBack="false" CausesValidation="true" OnLoad="btnResultView_OnLoad">
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="top" colspan="2">
                                                <dxe:ASPxLabel ID="lblPollReport" runat="server" Text="" ForeColor="Red">
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ItemTemplate>

                        </TSPControls:CustomAspxDevDataView>
                    </div>
                </div>
            </div>
        </div>


        <TSP:Poll_ValidPollListUserControl ID="Poll_ValidPollListUserControl" DisplayLocationType="HomePage"
            DataviewColumnCount="4" DataviewItemHeight="160" DataviewItemSpacing="1" runat="server"
            QuestionCountType="1" />
    </div>
    <%--***Periods****--%>
    <div class="row-six" id="PeriodsSection" runat="server">
        <div class="container">
            <div class="row">
                <div class="responsive-tabs-wrapper">
                    <div class="part-tabs-one responsive-tabs col-xs-12 col-sm-12 col-md-12 ie12 fr ">

                        <h2 runat="server" id="tablist2Tab1" class="responsive-tabs__heading--active h2-tab1" tabindex="0">
                            <div class="tab-icon"></div>
                            <span>دوره های آموزشی</span></h2>
                        <div runat="server" class="s-box-main " aria-hidden="false" role="tabpanel" aria-labelledby="tablist2-tab1" id="tablist2Panel1" style="display: block; opacity: 1;">
                            <div style="width: 100%">

                                <div id="owlPeriod" class="owl-carousel owl-theme owl-drag">
                                    <asp:Repeater runat="server" ID="DataViewPeriods">
                                        <ItemTemplate>
                                            <div class="box-owl-scroll">
                                                <a class="link-row" runat="server" href='<%# "/PeriodsView.Aspx?PPId="+ Utility.EncryptQS( Eval("PPId").ToString())+"&InsId="+ Utility.EncryptQS(Eval("InsId").ToString())+"&PrePg="+ Utility.EncryptQS("Home") %>' target="_blank">
                                                    <div class="overlab img-one">
                                                        <img class="lazyload" id="ImageNews" runat="server" data-src='/image/Periods/1.jpg' />
                                                    </div>
                                                    <div class="box-contnt PeriodList-Items">
                                                        <div class="date-scroll"><%# Eval("StartDate") %></div>
                                                        <div class="title-scroll"><%# Eval("PeriodTitle") %></div>
                                                        <div class="row">
                                                            <div class="col-1">
                                                                <span runat="server" id="A1">تاریخ شروع</span>
                                                            </div>
                                                            <div class="col-2">
                                                                <span>
                                                                    <%# Eval("StartDate") %>
                                                                </span>
                                                            </div>
                                                            <div class="col-3">
                                                                <span runat="server" id="A3">ظرفیت دوره:</span>
                                                            </div>

                                                            <div class="col-4">
                                                                <span>
                                                                    <%# Eval("Capacity") %>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="row">

                                                            <div class="col-1">
                                                                <span runat="server" id="A2">تاریخ پایان</span>
                                                            </div>
                                                            <div class="col-2">
                                                                <span>
                                                                    <%# Eval("EndDate") %>
                                                                </span>
                                                            </div>
                                                            <div class="col-3">
                                                                <span runat="server" id="A4" style="text-wrap: none">ظرفیت باقیمانده:</span>
                                                            </div>
                                                            <div class="col-4">
                                                                <span>
                                                                    <%# Eval("RemainCapacity") %>
                                                                </span>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </a>
                                            </div>

                                        </ItemTemplate>
                                    </asp:Repeater>



                                </div>
                                <a class="More" href="/Period.aspx" style="">بيشتر</a>
                            </div>
                        </div>
                        <h2 runat="server" id="tablist2Tab2" class="h2-tab2" tabindex="0">
                            <div class="tab-icon"></div>
                            <span>سمینار و دوره غیر مصوب</span></h2>
                        <div runat="server" class="s-box-main " aria-hidden="true" role="tabpanel" aria-labelledby="tablist2-tab2" id="tablist2Panel2" style="display: block; opacity: 1;">

                            <div style="width: 100%">

                                <div id="owlSeminar" class="owl-carousel owl-theme owl-drag">

                                    <asp:Repeater runat="server" ID="DataViewSeminar">
                                        <ItemTemplate>
                                            <div class="box-owl-scroll">
                                                <a class="link-row" runat="server" href='<%# "/SeminarView.Aspx?SeId="+ Utility.EncryptQS( Eval("PPId").ToString())+"&InsId="+ Utility.EncryptQS(Eval("InsId").ToString())+"&PrePg="+ Utility.EncryptQS("Home") %>' target="_blank">
                                                    <div class="overlab img-one">
                                                        <img class="lazyload" id="ImageNews" runat="server" data-src='/image/Periods/1.jpg' />
                                                    </div>
                                                    <div class="box-contnt PeriodList-Items">
                                                        <div class="date-scroll"><%# Eval("StartDate") %></div>
                                                        <div class="title-scroll"><%# Eval("PeriodTitle") %></div>
                                                        <div class="row">
                                                            <div class="col-1">
                                                                <span runat="server" id="A1">تاریخ شروع</span>
                                                            </div>
                                                            <div class="col-2">
                                                                <span>
                                                                    <%# Eval("StartDate") %>
                                                                </span>
                                                            </div>
                                                            <div class="col-3">
                                                                <span runat="server" id="A3">ظرفیت دوره:</span>
                                                            </div>

                                                            <div class="col-4">
                                                                <span>
                                                                    <%# Eval("Capacity") %>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div class="row">

                                                            <div class="col-1">
                                                                <span runat="server" id="A2">تاریخ پایان</span>
                                                            </div>
                                                            <div class="col-2">
                                                                <span>
                                                                    <%# Eval("EndDate") %>
                                                                </span>
                                                            </div>
                                                            <div class="col-3">
                                                                <span runat="server" id="A4" style="text-wrap: none">ظرفیت باقیمانده:</span>
                                                            </div>
                                                            <div class="col-4">
                                                                <span>
                                                                    <%# Eval("RemainCapacity") %>
                                                                </span>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </a>
                                            </div>

                                        </ItemTemplate>
                                    </asp:Repeater>



                                </div>
                                <a class="More" href="/Period.aspx" style="">بيشتر</a>
                            </div>

                        </div>

                    </div>
                </div>


            </div>
        </div>
    </div>
    <div width="100%" align="center">
        <%--<img id='wmcshwlanbpehwla' style='cursor: pointer' onclick='window.open("https://trustseal.enamad.ir/Verify.aspx?id=8424&p=aqgwodshwkynodsh", "Popup","toolbar=no, location=no, statusbar=no, menubar=no, scrollbars=1, resizable=0, width=580, height=600, top=30")' alt='' src='https://trustseal.enamad.ir/logo.aspx?id=8424&p=ukaqaodsqesgaods' />--%>
        <a referrerpolicy="origin" target="_blank" href="https://trustseal.enamad.ir/?id=201692&amp;Code=yveyxwRytFZ94AY0hHHA"><img referrerpolicy="origin" src="https://Trustseal.eNamad.ir/logo.aspx?id=201692&amp;Code=yveyxwRytFZ94AY0hHHA" alt="" style="cursor:pointer" id="yveyxwRytFZ94AY0hHHA"></a>

    </div>
    <script type="text/javascript">
        function SearchKeyPress(event) {
            // alert(1);
            if (event.keyCode === 13) {
<%= ClientScript.GetPostBackEventReference(btnSearchNews, string.Empty) %>;
            }
        }
        function TraceKeyPress(event) {
            // alert(1);
            if (event.keyCode === 13) {
<%= ClientScript.GetPostBackEventReference(btnDocumentTrace, string.Empty) %>;
            }
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.owl-carousel').owlCarousel({
                rtl: true,

                margin: 25,
                lazyLoad: true,
                responsiveClass: true,
                nav: true,
                responsive: {
                    0: {
                        items: 1,
                        nav: true
                    },
                    400: {
                        items: 1,
                        nav: true
                    },
                    480: {
                        items: 1,
                        nav: true
                    },
                    640: {
                        items: 2,
                        nav: true
                    },
                    768: {
                        items: 4,
                        nav: true
                    },
                    1200: {
                        items: 3,
                        nav: true
                    },
                    1900: {
                        items: 3,
                        nav: true,
                        loop: true,
                        margin: 25
                    }
                }

            });
            $('.owl-next').addClass('glyphicon glyphicon-chevron-right').text('');
            $('.owl-prev').addClass('glyphicon glyphicon-chevron-left').text('');
        });

    </script>

    <script>
        $("img.lazyload").lazyload();

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            //title
            for (var RecordCount = 0; RecordCount < $(".title-scroll").length; RecordCount++) {
                var TitleLengthslider = $(".title-scroll").eq(RecordCount).text().length
                if (TitleLengthslider > 75) {
                    var TitleLastSpaceslider = $(".title-scroll").eq(RecordCount).text().lastIndexOf(" ", 80)
                    var TitleTextslider = $(".title-scroll").eq(RecordCount).text()
                    var TitleTextslider = $(".title-scroll").eq(RecordCount).text().substring(0, TitleLastSpaceslider);
                    $(".title-scroll").eq(RecordCount).text(TitleTextslider + "...")
                }
            }
            //title
            for (var RecordCount = 0; RecordCount < $(".title-thumb").length; RecordCount++) {
                var TitleLengthslider = $(".title-thumb").eq(RecordCount).text().length
                if (TitleLengthslider > 70) {
                    var TitleLastSpaceslider = $(".title-thumb").eq(RecordCount).text().lastIndexOf(" ", 72)
                    var TitleTextslider = $(".title-thumb").eq(RecordCount).text()
                    var TitleTextslider = $(".title-thumb").eq(RecordCount).text().substring(0, TitleLastSpaceslider);
                    $(".title-thumb").eq(RecordCount).text(TitleTextslider + "...")
                }
            }
            //summary
            for (var RecordCount = 0; RecordCount < $(".summary-scroll").length; RecordCount++) {
                var TitleLengthslider = $(".summary-scroll").eq(RecordCount).text().length
                if (TitleLengthslider > 75) {
                    var TitleLastSpaceslider = $(".summary-scroll").eq(RecordCount).text().lastIndexOf(" ", 80)
                    var TitleTextslider = $(".summary-scroll").eq(RecordCount).text()
                    var TitleTextslider = $(".summary-scroll").eq(RecordCount).text().substring(0, TitleLastSpaceslider);
                    $(".summary-scroll").eq(RecordCount).text(TitleTextslider + "...")
                }
            }
        });
    </script>
    <script>
        $(".responsive-tabs__list__item").click(function () {
            $(".s-box-main").each(function () {
                if ($(this).attr('aria-hidden') == 'false') {
                    $(this).animate({ opacity: 1 }, 2000);
                } else if ($(this).attr('aria-hidden') == 'true') { $('.s-box-main').css('opacity', ''); }
            });
        });
        $("#tablist1-tab1").trigger("click");

        RESPONSIVEUI.responsiveTabs();



        //<!-- auto-generate carousel indicator html -->
        $(document).ready(function () {
            var myCarousel = $('#BanerCarousel');
            myCarousel.append("<ol class='carousel-indicators'></ol>");
            var indicators = $('.carousel-indicators');
            myCarousel.find('.carousel-inner').children('.item').each(function (index) {
                (index === 0) ?
                    indicators.append("<li data-target='#BanerCarousel' data-slide-to='" + index + "' class='active'></li>") :
                    indicators.append("<li data-target='#BanerCarousel' data-slide-to='" + index + "'></li>");
            });
            //<!-- then call carousel -->
            $('#BanerCarousel').carousel();
        });



        // Add smooth scrolling to all links .a2a_kit a
        $('#periodLeftLink').click(function () {

            // Make sure this.hash has a value before overriding default behavior
            if (this.hash !== "") {
                // Prevent default anchor click behavior
                event.preventDefault();

                // Store hash
                var hash = this.hash;

                // Using jQuery's animate() method to add smooth page scroll
                // The optional number (800) specifies the number of milliseconds it takes to scroll to the specified area
                $('html, body').animate({
                    scrollTop: $(hash).offset().top - 180
                }, 800, 'swing', function () {

                    // Add hash (#) to URL when done scrolling (default click behavior)
                    window.location.hash = hash;
                });
            } // End if
        });



        $(window).load(function () {
            var width = $(window).width();
            if (width > 991) {
                var Height = $('#slider').height()
                $(".searchArea").css({
                    'min-height': Height + "px"
                });
            }
        });
        $(window).resize(function () {
            var width = $(window).width();
            if (width > 991) {
                var Height = $('#slider').height()
                $(".searchArea").css({
                    'min-height': Height + "px"
                });
            }
        });

        $(document).ready(function () {
            //Enable swiping...
            $(".carousel").swipe({

                swipe: function (event, direction, distance, duration, fingerCount, fingerData) {

                    if (direction == 'left') $(this).carousel('prev');
                    if (direction == 'right') $(this).carousel('next');

                },
                allowPageScroll: "vertical"

            });
        });



        $(window).load(function () {
            // executes when complete page is fully loaded, including all frames, objects and images
            console.log("window is loaded");
            // window load  
        });


        function ClearText() {
            document.getElementById("lblMap").innerHTML = "";
            document.getElementById("lblMapText").style.display = 'none';
        }
        function ShowName(obj) {

            document.getElementById("lblMap").innerHTML = obj.getAttribute("title");
            document.getElementById("lblMapText").style.display = 'block';
        }


        function PageRedirect(obj) {
            var Cd = obj.getAttribute("id");
            switch (Cd) {
                case '01':
                case '001':
                case '0001':
                    Cd = '1'
                    break;
                case '02':
                    Cd = '2'
                    break;
                case '05':
                    Cd = '5'
                    break;
                case '07':
                    Cd = '7'
                    break;
                case '010':
                    Cd = '10'
                    break;
                case '011':
                    Cd = '11'
                    break;
                case '013':
                    Cd = '13'
                    break;
            }
            window.open("/AgentView.aspx?Cd=" + Cd)
        }

        //$(document).ready(function(){
        //    $('.link-path').hover(function () {
        //      //  alert(1);
        //        $('span.map-lbl').text($(this).attr('title'));
        //  //$("span.map-lbl").text($(this).attr("title"));
        //});
        //});
    </script>
</asp:Content>
