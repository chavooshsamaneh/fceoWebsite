<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TraceDocMemberFile.aspx.cs" Inherits="TraceDocuments_TraceDocMemberFile" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.XtraReports.v17.1.Web, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dxxr" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>مشخصات پروانه اشتغال به کار</title>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center" width="100%">
            <dxe:ASPxLabel runat="server" ID="lblWarning" Text="" ForeColor="Red" Font-Bold="true">
            </dxe:ASPxLabel>
        </div>
        <%--   <div align="center" width="100%" style="border-bottom-color: Black; border-bottom-width:medium">
        <dxe:ASPxImage runat="server" ID="ImgMeURL"  Height="100px" Width="100px">
        </dxe:ASPxImage>
    </div>--%>
        <div align="center" width="100%">
            <dxxr:ReportViewer ID="RptMemberFile" PrintUsingAdobePlugIn="false" runat="server"
                LoadingPanelText="لطفاً صبر نمایید">
            </dxxr:ReportViewer>
        </div>
    </form>
</body>
</html>