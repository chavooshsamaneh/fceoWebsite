<%@ Page Title="مشخصات پرداخت الکترونیکی" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPagePortals.master"   CodeFile="EpaymentVerifyParsian.aspx.cs" Inherits="EPayment_EpaymentVerifyParsian" %>
<%@ Register Src="../UserControl/EPaymentUserControl.ascx" TagName="EPaymentUserControl"
    TagPrefix="TspUserControl" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <div id="view" dir="rtl">
   
            <div id="PageContent" align="center">
                <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
                <TspUserControl:EPaymentUserControl ID="EPaymentUC" runat="server" />
                <dx:ASPxHiddenField ID="HiddenFieldEpayment" runat="server">
                </dx:ASPxHiddenField>
            </div>
        </div>

 <%--   </form>--%>

</asp:Content>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>سازمان نظام مهندسی ساختمان استان فارس</title>   
    <link rel="stylesheet" href="/StyleSheet/bootstrap.css" type="text/css" />
    <link rel="stylesheet" href="/StyleSheet/Style.css" type="text/css" />
    <style type="text/css">
        img, div, td {
            behavior: url(Script/iepngfix.htc);
        }
    </style>
</head>
<body>
    <form id="formEPaymentverify" runat="server">
        <div id="view" dir="rtl">
   
            <div id="PageContent" align="center">
                <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
                <TspUserControl:EPaymentUserControl ID="EPaymentUC" runat="server" />
                <dx:ASPxHiddenField ID="HiddenFieldEpayment" runat="server">
                </dx:ASPxHiddenField>
            </div>
        </div>

    </form>
</body>
</html>--%>
