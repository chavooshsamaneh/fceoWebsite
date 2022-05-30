<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="MemberHome.aspx.cs" Inherits="Members_MemberHome" Title="پرتال اعضای حقیقی" %>

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

<%--<%@ Register Src="../UserControl/FormBuilder_ActiveFormsUserControl.ascx" TagName="FormBuilder_ActiveFormsUserControl"
    TagPrefix="TSP" %>--%>
<%@ Register Src="../UserControl/Poll_ValidPollListUserControl.ascx" TagName="Poll_ValidPollListUserControl"
    TagPrefix="TSP" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script type="text/javascript" language="javascript">
        function ShowHelp() {
            ShowHelpWindow(HiddenHelp.Get('HelpAddress'));
        }
        function confirmLetterTax() {
            if (confirm("پاسخ دريافت شده از اداره ماليات از تاريخ درج شده در آن فقط به مدت سه ماه اعتبار دارد") == true)
            window.open(HiddenHelp.Get('LetterReport'));
                }
    </script>
    <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <div class="row">
        <asp:Label ID="lblError" runat="server" Text="" Font-Size="Small" Visible="False"
            ForeColor="Red">
        </asp:Label>
    </div>
    <fieldset id="Fieldset1" runat="server">
        <legend class="HelpUL" style="color: #cb368a">درخواست های واحد مالی</legend>
        <div class="row">
            <div id="panelMeDebt" runat="server" class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #4D4C78;">
                <a onclick="ShowHelpWindow('https://debt.fceo.ir')">
                    <div class="Inside">
                        <div id="txtMeDebt" style="text-align: center" runat="server" class="col-md-9 QuickMenuText"><span>پرداخت آنلاین بدهی</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/card-in-use-48.png" />
                        </div>
                    </div>
                </a>
            </div>
            <div id="panelMeLoan" runat="server" class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #b886ab;">
                <a onclick="ShowHelpWindow('https://loan.fceo.ir/')">
                    <div class="Inside">
                        <div id="txtMeLoan" style="text-align: center" runat="server" class="col-md-9 QuickMenuText"><span>پرداخت آنلاین اقساط وام</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/card-in-use-48.png" />
                        </div>
                    </div>
                </a>
            </div>
            <div id="Div3" runat="server" class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #d80202;">
                <a href="Accounting/EpaymentFishes.aspx?P=N">
                    <div class="Inside">
                        <div id="Div4" style="text-align: center" runat="server" class="col-md-9 QuickMenuText"><span>مدیریت فیش های پرداخت نشده</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/card-in-use-48.png" />
                        </div>
                    </div>
                </a>
            </div>
        </div>
    </fieldset>

    <fieldset id="PanelTechnicalService" runat="server">
        <legend class="HelpUL" style="color: #cb368a">درخواست های واحد خدمات مهندسی</legend>

        <div class="row">
            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #6653ff;">
                <a runat="server" id="btnWorkRequestNew" onserverclick="btnWorkRequestNew_ServerClick">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>اعلام آماده بکاری</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/TsNewWorkRequest.png" />
                        </div>
                    </div>
                </a>
            </div>


            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #be3399;">
                <a runat="server" id="btnWorkRequest" onserverclick="btnWorkRequest_ServerClick">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>مدیریت آماده به کاری</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/TsWorkRequestChange.png" />
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #a7414a;">
                <a runat="server" id="btnWorkRequestChangeRequest" onserverclick="btnWorkRequestChangeRequest_ServerClick">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>درخواست تغییرات اطلاعات آماده بکاری</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/TSWorRequestChange.png" />
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #009688;">
                <a runat="server" id="btnWorkRequestOffeRequest" onserverclick="btnWorkRequestOffeRequest_ServerClick">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>اعلام مرخصی نظارت</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/TsVacationRequest.png" />
                        </div>
                    </div>
                </a>
            </div>

        </div>
        <div class="row">
            <div id="Div1" runat="server" class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #1512bd;">
                <a runat="server" id="btnControler" onserverclick="btnControler_ServerClick">
                    <div class="Inside">
                        <div id="Div2" style="text-align: center" runat="server" class="col-md-9 QuickMenuText"><span>بازبینی نقشه</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/Controler.png" />
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #b886ab;">
                <a runat="server" id="btnNewDesign" onserverclick="btnNewDesign_ServerClick">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>ثبت کار طراحی جدید</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/NewDesignAndPlan.png" />
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #f57951;">
                <a runat="server" id="btnMapManagmentPage" onserverclick="btnMapManagmentPage_ServerClick">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>مدیریت نقشه ها</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/MapManagment.png" />
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #21a235;">
                <a runat="server" id="btnProjectManagmentPage" onserverclick="btnProjectManagmentPage_ServerClick">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>مدیریت پروژه ها</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/ProjectManagment.png" />
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <div id="Div6" runat="server" class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #79aefe;">
            <a runat="server" id="A10" href="TechnicalServices/Project/ObserverSelected.aspx">
                <div class="Inside">
                    <div class="col-md-9 QuickMenuText"><span>لیست ارجاع کارنظارت صدرا/شهرستان</span></div>
                    <div class="col-md-3 QuickMenuIcon">
                        <img src="../Images/HomePage/ObsSelect.png" height="48" width="48" />
                    </div>
                </div>
            </a>
        </div>
        <div class="row">
            <div id="Div5" runat="server" class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #eabede;">
                <a runat="server" id="A9" href="TechnicalServices/Report/MemberOperationReport.aspx">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>گزارش کارکرد خدمات مهندسی</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/MemberOperationReport.png" height="48" width="48" />
                        </div>
                    </div>
                </a>
            </div>
            <div id="btnQueueListMunRequest" runat="server" class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #d85202;">
                <a runat="server" id="btnQueueListMunRequestA" href="http://esup.fceo.ir" target="_blank">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>دریافت اولویت ارجاع کار نظارت شهرداری شیراز</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/TSQueueListMun.png" height="48" width="48" />
                        </div>
                    </div>
                </a>
            </div>
              <div runat="server" id="div9" class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #a5f0b0;">
                <a onclick="ShowHelpWindow('../../Help/EghdamMeliMaskan.pdf')">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>تفاهم نامه همكاري در اجراي پروژه اقدام ملي تامين مسكن</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/HelpDocs.png" />
                        </div>
                    </div>
                </a>

            </div>

        </div>
    </fieldset>


    <fieldset>
        <legend class="HelpUL" style="color: #cb368a">درخواست های واحد عضویت</legend>


        <div class="row">
            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #403df3;">
                <a runat="server" id="test" onserverclick="btnChangeBaseInfo_Click">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>درخواست تغییرات اطلاعات پایه</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/edit-user-48.png" />
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #ff5394;">
                <a runat="server" id="A1" onserverclick="btnLicenceRequest_Click">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>درخواست تغییرات مدرک تحصیلی</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/LicenceRequest.png" />
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #f7bc48;">
                <a runat="server" id="A2" onserverclick="btnNewMemberReq_Click">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>درخواست تغییرات پرونده عضویت</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/MeChangeRequest.png" />
                        </div>
                    </div>
                </a>
            </div>



            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #184b46;">
                <a runat="server" id="A3" onserverclick="btnEditMemberInfo_Click">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>ویرایش اطلاعات پرونده عضویت</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/editMeRequest.png" />
                        </div>
                    </div>
                </a>
            </div>

        </div>



    </fieldset>

    <fieldset runat="server" id="RoundPanelDocumentRequest2">
        <legend class="HelpUL" style="color: #1A6E74">درخواست های واحد پروانه</legend>
        <div class="row">

            <div runat="server" id="btnNewDoc" class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #D2B4DE;">
                <a runat="server" id="A4" onserverclick="btnNewDoc_Click">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>درخواست صدور پروانه اشتغال به کار</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/NeweDoc.png" />
                        </div>
                    </div>
                </a>
            </div>


            <div runat="server" id="btnRevival" class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #79aefe;">
                <a runat="server" id="A6" onserverclick="btnRevival_Click">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>درخواست تمدید پروانه اشتغال به کار</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/Revival.png" />
                        </div>
                    </div>
                </a>
            </div>



            <div runat="server" id="btnQualification" class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #d918bb;">
                <a runat="server" id="A5" onserverclick="btnQualification_Click">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>درخواست درج صلاحیت جدید در پروانه اشتغال به کار</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/Qualification.png" />
                        </div>
                    </div>
                </a>
            </div>



            <div runat="server" id="btnUpgrade" class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #f0941e;">
                <a runat="server" id="btnUpgrade1" onserverclick="btnUpgrade_ServerClick">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>درخواست ارتقا پایه پروانه اشتغال به کار</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/UpgradeDocument.png" />
                        </div>
                    </div>
                </a>
            </div>

        </div>
        <div class="row">
               <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #6f84a2;">
                <a runat="server" id="A11"  href="Documents/MemberFiles.aspx">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>مدیریت درخواست های پروانه اشتغال</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/LicenceRequest.png" />
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #42B4BC;">
                <a runat="server" id="A7" onclick="confirmLetterTax();">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>چاپ نامه تسویه حساب امور مالیاتی</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/PintTaxLetter.png" />
                        </div>
                    </div>
                </a>
            </div>


            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #8a4980;">
                <a runat="server" id="A8" onclick="window.open('../../Help/TaxOfficeAddress.pdf');">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>آدرس ادارات امورمالیاتی شیراز براساس محدوده جغرافیایی</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <i class="fas fa-file-pdf" style="font-size: 48px; color: #a7414a"></i>
                        </div>
                    </div>
                </a>
            </div>

        </div>
    </fieldset>
    <fieldset>
        <legend class="HelpUL" style="color: #f57951">راهنمای درخواست ها</legend>

        <div class="row">
            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #dae0ea;">
                <a onclick="ShowHelpWindow('../../Help/DocumentFiles/NewDocHelp.pdf')">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>راهنمای صدور پروانه اشتغال به کار</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/HelpDocs.png" />
                        </div>
                    </div>
                </a>
            </div>


            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #9b7792;">
                <a onclick="ShowHelpWindow('../../Help/DocumentFiles/HelpdocRevival.pdf')">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>راهنمای تمدید اشتغال به کار</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/HelpDocs.png" />
                        </div>
                    </div>
                </a>

            </div>


            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #f74848;">
                <a onclick="ShowHelpWindow('../../Help/DocumentFiles/DocQualificationHelp.pdf')">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>راهنمای درخواست درج صلاحیت جدید در پروانه اشتغال به کار</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/HelpDocs.png" />
                        </div>
                    </div>
                </a>

            </div>


            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #8fe824;">
                <a onclick="function(s,e){ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>راهنمای استفاده از پرتال</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/HelpDocs.png" />
                        </div>
                    </div>
                </a>

            </div>
        </div>
        <div class="row">
            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #D2B4DE;">
                <a onclick="ShowHelpWindow('../../Help/DocumentFiles/HelpEditDocument.pdf')">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>راهنمای رفع نقص درخواست پروانه اشتغال</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/HelpDocs.png" />
                        </div>
                    </div>
                </a>

            </div>

            <div class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #f20eb7;">
                <a onclick="ShowHelpWindow('../../Help/DocumentFiles/HelpEditMember.pdf')">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>راهنمای رفع نقص درخواست عضویت</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/HelpDocs.png" />
                        </div>
                    </div>
                </a>

            </div>
            <div runat="server" id="divheplWorkRequest" class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #f2190e;">
                <a onclick="ShowHelpWindow('../../Help/HelpWorkRequest.pdf')">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>راهنمای آماده بکاری</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/HelpDocs.png" />
                        </div>
                    </div>
                </a>

            </div>


            <div runat="server" id="div7" class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #a5f0b0;">
                <a onclick="ShowHelpWindow('../../Help/HelpPassForget.pdf')">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>راهنمای فراموشی رمز عبور</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/HelpDocs.png" />
                        </div>
                    </div>
                </a>

            </div>
        </div>

        <div class="row">

            <div runat="server" id="div8" class="col-md-2 Divborder QuickMenu" style="border-bottom-color: #6653ff;">
                <a onclick="ShowHelpWindow('../../Help/HelpChangePass.pdf')">
                    <div class="Inside">
                        <div class="col-md-9 QuickMenuText"><span>راهنمای تغییر رمز عبور</span></div>
                        <div class="col-md-3 QuickMenuIcon">
                            <img src="../Images/HomePage/HelpDocs.png" />
                        </div>
                    </div>
                </a>

            </div>
        </div>
    </fieldset>
    <fieldset>
        <legend class="HelpUL" style="color: #3619AD">نکات درخواست های واحد عضویت</legend>
        <div class="row">
            <ul class="HelpULGray">
                <p>
                    <b>توجه نکته بسیار مهم:</b>در صورت ثبت هر کدام از درخواست های عضویت بالا تا زمان تایید واحد عضویت امکان ثبت درخواست دیگری وجود ندارد بنابراین در صورت نیاز به تغییر اطلاعات پایه و مدارک تحصیلی <b>به صورت هم زمان</b> از منوی سمت راست مدیریت درخواست هااستفاده کنید و سپس از با استفاده از دکمه گردش کار پرونده را برای واحد عضویت ارسال نمایید
                </p>
                <li><b>ویرایش اطلاعات پرونده عضویت:</b>در صورت وجود درخواست فعالی که در مرحله ثبت درخواست قرار دارد</li>
                <li><b>درخواست تغییرات اطلاعات پایه:</b>در صورتی که قصد دارید<b>تنها</b>اطلاعات شناسایی مربوط به خود را تغییر دهید</li>
                <li><b>درخواست تغییرات مدرک تحصیلی:</b>در صورتی که قصد دارید<b>تنها</b>مدارک تحصیلی یا تصاویر مربوط به آنها را تغییر دهید</li>
                <li><b>درخواست تغییرات پرونده عضویت:</b>در صورتی که قصد دارید<b>به صورت هم زمان</b>اطلاعات شناسایی مربوط به خود و مدارک تحصیلی یا تصاویر مربوط به آنها را تغییر دهید</li>
            </ul>
        </div>
    </fieldset>
    <fieldset runat="server" id="RoundPanelDocumentRequest">
        <legend class="HelpUL">نکات درخواست های واحد پروانه</legend>
        <ul class="HelpULGray">
            <li><b>درخواست صدور پروانه اشتغال:</b>    صرفا مربوط به متقاضیانی است که برای بار نخست درخواست صدور پروانه اشتغال دارند.
            </li>
            <li><b>درخواست درج صلاحیت جدید در پروانه اشتغال:</b>این درخواست صرفا مربوط به کسانی است که دارای پروانه اشتغال بوده و صلاحیت جدیدی در آزمون ورود به حرفه مهندسی پذیرفته شده اند و متقاضی درج آن در پروانه اشتغال می باشند.د.</li>
        </ul>
    </fieldset>
    <br />

    <TSP:Poll_ValidPollListUserControl ID="Poll_ValidPollListUserControl" DisplayLocationType="MembersPortal"
        DataviewColumnCount="4" DataviewItemHeight="150" DataviewItemWidth="150" DataviewItemSpacing="-1"
        runat="server" QuestionCountType="-1" />
    <%--<TSP:FormBuilder_ActiveFormsUserControl ID="FormBuilder_ActiveFormsUserControl" runat="server"
        DisplayLocationType="MembersPortal" />--%>
    <dx:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
    </dx:ASPxHiddenField>

</asp:Content>
