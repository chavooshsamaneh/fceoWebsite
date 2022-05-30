<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="PeriodRegister.aspx.cs" Inherits="Employee_Amoozesh_PeriodRegister"
    Title="ثبت نام دوره های آموزشی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
<%@ Register Src="../../UserControl/EPaymentUserControl.ascx" TagName="EPaymentUserControl"
    TagPrefix="TspUserControl" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="Content" runat="server" style="width: 100%" align="center">
                <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <table width="100%">
                    <tr>
                        <td>
                            <ul class="HelpUL">
                                <li>جهت ثبت نام در هریک از دوره های آموزشی به صورت کامل (شرکت در کلاس ها و آزمون) بر
                                    روی لینک ''ثبت نام در کلاس های دوره و آزمون دوره'' همان دوره کلیک نمایید. </li>
                                <li>جهت ثبت نام در آزمون هر یک از دوره های آموزشی بر روی لینک '' ثبت نام فقط درآزمون
                                    دوره'' همان دوره کلیک نمایید. </li>
                                <%--<li>در صورتی که از ثبت نام در دوره انتخاب شده پشیمان شده اید ، بر روی علامت ضربدر (حذف)
                                    در لیست دوره های انتخابی و مربوط به دوره مورد نظر خورد خود کلیک نمایید. </li>
                                <li>پس از انتخاب کلیه دوره های آموزشی مورد نظر،جهت تکمیل روند ثبت نام بایستی برروی <u>
                                    دکمه تایید و ادامه</u> کلیک نموده.به این ترتیب شما به صفحه ارتباط با درگاه الکترونیکی جهت پرداخت هزینه
                                    ثبت نام دوره هدایت می شود. </li>
                                <li>در صورت عدم کلیک برروی دکمه ''تایید و ادامه'' و تکمیل روند ثبت نام و پرداخت الکترونیکی هیچگونه اطلاعات از ثبت نام شما در دوره های آموزشی در سیستم ثبت نخواهد شد.</li>--%>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            <TSPControls:CustomAspxDevDataView ID="DataViewPeriods" runat="server" ColumnCount="1"
                                Width="100%" DataSourceID="OdbPeriod" PagerSettings-EndlessPagingMode="OnScroll" >
                                <ItemTemplate>
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <asp:Label ID="lblTitle"  Font-Bold="true"  runat="server" Text='<%# Bind("PeriodTitle") %>'></asp:Label>

                                    </div>
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-3 col-sm-6  col-xs-12">کد دوره :</div>
                                        <div class="col-md-9">
                                            <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Font-Bold="true" RightToLeft="True"
                                                Text='<%# Bind("PPCode") %>'>
                                            </dxe:ASPxLabel>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-3 col-sm-6  col-xs-12">تاریخ شروع :</div>
                                        <div class="col-md-3 col-sm-6  col-xs-12">
                                            <dxe:ASPxLabel ID="lblstatus" runat="server" RightToLeft="True" Font-Bold="true"
                                                Text='<%# Bind("StartDate") %>'>
                                            </dxe:ASPxLabel>
                                        </div>
                                        <div class="col-md-3 col-sm-6  col-xs-12">تاریخ پایان :</div>
                                        <div class="col-md-3 col-sm-6  col-xs-12">
                                            <dxe:ASPxLabel ID="Label1" runat="server" Font-Bold="true" RightToLeft="True" Text='<%# Bind("EndDate") %>'>
                                            </dxe:ASPxLabel>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-3 col-sm-6  col-xs-12">تاریخ امتحان :</div>
                                        <div class="col-md-3 col-sm-6  col-xs-12">
                                            <dxe:ASPxLabel ID="ASPxLabel6" runat="server" RightToLeft="True" Font-Bold="true"
                                                Text='<%# Bind("TestDate") %>'>
                                            </dxe:ASPxLabel>
                                        </div>
                                        <div class="col-md-3 col-sm-6  col-xs-12">ساعت امتحان :</div>
                                        <div class="col-md-3 col-sm-6  col-xs-12">
                                            <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Font-Bold="true" RightToLeft="True"
                                                Text='<%# Bind("TestHour") %>'>
                                            </dxe:ASPxLabel>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-3 col-sm-6  col-xs-12">طول دوره(ساعت) :</div>
                                        <div class="col-md-3 col-sm-6  col-xs-12">
                                            <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Font-Bold="true" RightToLeft="True"
                                                Text='<%# Bind("Duration") %>'>
                                            </dxe:ASPxLabel>
                                        </div>
                                        <div class="col-md-3 col-sm-6  col-xs-12">امتیاز :</div>
                                        <div class="col-md-3 col-sm-6  col-xs-12">
                                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" RightToLeft="True" Font-Bold="true"
                                                Text='<%# Bind("Point") %>'>
                                            </dxe:ASPxLabel>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-3 col-sm-6  col-xs-12">ظرفیت دوره :</div>
                                        <div class="col-md-3 col-sm-6  col-xs-12">
                                            <dxe:ASPxLabel ID="ASPxLabel51" runat="server" Font-Bold="true" RightToLeft="True"
                                                Text='<%# Bind("Capacity") %>'>
                                            </dxe:ASPxLabel>
                                        </div>
                                        <div class="col-md-3 col-sm-6  col-xs-12">ظرفیت باقیمانده :</div>
                                        <div class="col-md-3 col-sm-6  col-xs-12">
                                            <dxe:ASPxLabel ID="ASPxLabel8" runat="server" RightToLeft="True" Font-Bold="true"
                                                Text='<%# Bind("RemainCapacity") %>'>
                                            </dxe:ASPxLabel>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-3 col-sm-6  col-xs-12">هزینه دوره و آزمون(ریال) :</div>
                                        <div class="col-md-3 col-sm-6  col-xs-12">
                                            <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Font-Bold="true" RightToLeft="True"
                                                Text='<%# Bind("PeriodCost","{0:#,#}") %>'>
                                            </dxe:ASPxLabel>
                                        </div>
                                        <div class="col-md-3 col-sm-6  col-xs-12">هزینه آزمون به تنهایی(ریال) :</div>
                                        <div class="col-md-3 col-sm-6  col-xs-12">
                                            <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Font-Bold="true" RightToLeft="True"
                                                Text='<%# Bind("TestCost","{0:#,#}") %>'>
                                            </dxe:ASPxLabel>
                                        </div>
                                    </div>
                                      <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-3 col-sm-6  col-xs-12">نحوه برگزاری :</div>
                                        <div class="col-md-3 col-sm-6  col-xs-12">
                                            <dxe:ASPxLabel ID="ASPxLabel10" runat="server" RightToLeft="True" Font-Bold="true"
                                                Text='<%# Bind("PPType") %>'>
                                            </dxe:ASPxLabel>
                                        </div>
                                        <div class="col-md-3 col-sm-6  col-xs-12">نام مؤسسه :</div>
                                        <div class="col-md-3 col-sm-6  col-xs-12">
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="true" RightToLeft="True"
                                                Text='<%# Bind("InsName") %>'>
                                            </dxe:ASPxLabel>
                                        </div>
                                    </div>
                                     <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-3 col-sm-6 col-xs-12">محل برگزاری :</div>
                                        <div class="col-md-9 col-sm-6 col-xs-12">
                                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" RightToLeft="True" Font-Bold="true"
                                                Text='<%# Bind("Place") %>'>
                                            </dxe:ASPxLabel>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="col-md-3 col-sm-6 col-xs-12">
                                            <asp:LinkButton ID="LinkButton1" CssClass="continueLink" OnClick="btnView_Click" runat="server" CommandArgument='<%# Eval("PPId")+";"+ Eval("InsId") %>'>مشاهده جزییات</asp:LinkButton></div>
                                        <div class="col-md-3 col-sm-6 col-xs-12">
                                            <asp:LinkButton ID="btnRegister" CssClass="continueLink" OnClick="btnRegister_Click" runat="server" CommandArgument='<%# Bind("PPId") %>'> ثبت نام در دوره و آزمون </asp:LinkButton></div>
                                        <div class="col-md-3 col-sm-6 col-xs-12">
                                            <asp:LinkButton ID="btnRegisterTest" CssClass="continueLink" OnClick="btnRegisterTest_Click" runat="server"
                                                CommandArgument='<%# Bind("PPId") %>'>ثبت نام فقط درآزمون دوره</asp:LinkButton>
                                        </div>
                                        <div class="col-md-3 col-sm-6 col-xs-12">
                                            <asp:LinkButton ID="btnPeriodOnly" CssClass="continueLink" OnClick="btnPeriodOnly_Click" runat="server"
                                                CommandArgument='<%# Bind("PPId") %>'>ثبت نام فقط در دوره</asp:LinkButton>
                                        </div>
                                    </div>      
                                        <div class="col-md-12 col-sm-12 col-xs-12 footer-divider"></div>
                                </ItemTemplate>
                            </TSPControls:CustomAspxDevDataView>
                        </td>
                    </tr>
                </table>
                <asp:ObjectDataSource ID="OdbPeriod" runat="server" SelectMethod="FindPeriodPresentByDateForMemberRegister"
                    TypeName="TSP.DataManager.PeriodPresentManager" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter DbType="Int32" DefaultValue="0" Name="PType" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <dx:ASPxHiddenField ID="HiddenFieldEpayment" runat="server">
                </dx:ASPxHiddenField>
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                    DisplayAfter="0">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
