<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PeriodDocBalanceBalance8Columns.aspx.cs" Inherits="ReportForms_Accounting_PeriodDocBalanceBalance8Columns" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.XtraReports.v17.1.Web, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dxxr" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="DivContent" style="width: 100%" align= "center">
            <br />
            <tspcontrols:customprinttoolbar id="CustomPrintToolbar1" runat="server" ReportViewer="<%# ReportViewer1 %>"></tspcontrols:customprinttoolbar>
            <br />
            <dxxr:reportviewer id="ReportViewer1" runat="server"></dxxr:reportviewer>
            <br />
        </div>
    
    </div>
    </form>
</body>
</html>
