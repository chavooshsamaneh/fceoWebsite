<%@ Page Language="C#" Title="مشخصات فاکتور پرداخت"   AutoEventWireup="true" CodeFile="Epayment.aspx.cs" Inherits="EPayment_Epayment" %>

<%@ Register Src="../UserControl/EPaymentUserControl.ascx" TagName="EPaymentUserControl"
    TagPrefix="TspUserControl" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
	<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>سازمان نظام مهندسی ساختمان استان فارس</title>
    <%--<link href="StyleSheet/MainStyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="StyleSheet/Default.css" rel="stylesheet" type="text/css" />--%>
    
    <link rel="stylesheet" href="/StyleSheet/bootstrap.css" type="text/css" />
    <link rel="stylesheet" href="/StyleSheet/Style.css" type="text/css" />
</head>
<body dir="rtl">
    <form id="formEPayment" runat="server"  target="TheWindow">
        <div id="view" dir="rtl">
            <div id="PageContent" align="center">
                <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <asp:HiddenField ID="merchantId" runat="server" />
                <asp:HiddenField ID="token" runat="server" />
                <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>
                <TspUserControl:EPaymentUserControl ID="EPaymentUC" runat="server" />

                <div align="center">
                    <table>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnPayment" CssClass="ButtonMenue" OnClick="btnPayment_Click" runat="server">پرداخت الکترونیکی</asp:LinkButton>

                               <%-- <TSPControls:CustomAspxButton ID="btnPayment" Width="140px" runat="server"  EnableViewState="False"
                                    OnClick="btnPayment_Click" Text="پرداخت الکترونیکی" Wrap="False" 
                                  >
                                    <Image Height="25px" Url="~/Images/icons/Empayment.png" Width="25px" />
                                </TSPControls:CustomAspxButton>--%>
                            </td>
                            <td>
                            
                            </td>
                        </tr>
                    </table>
                </div>
                <dx:ASPxHiddenField ID="HiddenFieldEpayment" runat="server">
                </dx:ASPxHiddenField>

            </div>
        </div>
    </form>
</body>
</html>


