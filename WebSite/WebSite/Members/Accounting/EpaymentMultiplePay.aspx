<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="EpaymentMultiplePay.aspx.cs" Inherits="Members_Accounting_EpaymentMultiplePay" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../UserControl/EPaymentUserControl.ascx" TagName="EPaymentUserControl"
    TagPrefix="TspUserControl" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>


            <ul style="font-family=tahoma; font-size: 8pt; line-height: 15pt; color: DarkRed">
                <%-- <li>اطلاعات ثبت نام شما در سیستم با موفقیت ثبت گردید.</li>--%>
                <li>در صورتی که به هر دلیل پس از اتصال به درگاه بانک  موفق به پرداخت الکترونیکی شهریه دوره/سمینار آموزشی نشدید،پیش از تکمیل ظرفیت دوره فرصت پرداخت شهریه از طریق همین سایت را خواهید
                                            داشت.جهت این امر پس از ورود به پرتال خود از طریق یکی از دو منوی زیر می توانید اقدام
                                            نمایید:</li>
                <ul type="disc">
                    <li>''واحد آموزش'' >> ''مدیریت فیش های پرداخت نشده پرداخت الکترونیکی
                    </li>
                    <li>''واحد امور مالی'' >> ''مدیریت فیش
                                            های پرداخت الکترونیکی
                    </li>
                </ul>
            </ul>
             <table>
                  <tbody>
                <tr>
                    <td >
                        <asp:LinkButton ID="btnSave1" CssClass="ButtonMenue" OnClick="btnSave_Click" runat="server">ذخیره</asp:LinkButton>
                    </td>
                    <td >
                        <asp:LinkButton ID="btnPre1" CssClass="ButtonMenue" OnClick="btnPre_Click" runat="server">بازگشت</asp:LinkButton>
                    </td>
                    <td >
                        <asp:LinkButton ID="btnCancel1" CssClass="ButtonMenue" OnClick="btnCancel_Click" runat="server">انصراف</asp:LinkButton>

                    </td>

                </tr>
                      </tbody>
            </table>
            <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
            <TspUserControl:EPaymentUserControl ID="EPaymentUC" runat="server" />

            <table>
                  <tbody>
                <tr>
                    <td >
                        <asp:LinkButton ID="btnSave" CssClass="ButtonMenue" OnClick="btnSave_Click" runat="server">ذخیره</asp:LinkButton>
                    </td>
                    <td >
                        <asp:LinkButton ID="btnPre" CssClass="ButtonMenue" OnClick="btnPre_Click" runat="server">بازگشت</asp:LinkButton>
                    </td>
                    <td >
                        <asp:LinkButton ID="btnCancel" CssClass="ButtonMenue" OnClick="btnCancel_Click" runat="server">انصراف</asp:LinkButton>

                    </td>

                </tr>
                      </tbody>
            </table>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                        <img src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
            <dx:ASPxHiddenField ID="HiddenFieldEpayment" runat="server">
            </dx:ASPxHiddenField>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
