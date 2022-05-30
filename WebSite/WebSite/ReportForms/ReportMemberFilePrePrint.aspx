<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportMemberFilePrePrint.aspx.cs"
    Inherits="ReportForms_ReportMemberFilePrePrint" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.XtraReports.v17.1.Web, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dxxr" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>نسخه اولیه گواهینامه اشتغال به کار</title>
     <style type="text/css">
   
       @font-face {
            font-family: 'Raspoutine Medium';
            src: url(http://www.fceo.ir/fonts/BNazanin.ttf);
        }
      
    </style>
</head>
     
     

<body>
    <form id="form1" runat="server">
    <div>
        <TSPControls:CustomAspxCallbackPanel runat="server"   Width="100%"
            ID="CallbackPanelReport" ClientInstanceName="CallbackPanelReport" OnCallback="CallbackPanelReport_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
    if(s.cpError==1)
     {
       alert(s.cpMsg);
       s.cpError=0;
       s.cpMsg = '';
     }
}" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent5" runat="server">
                    <div id="DivContent" style="width: 100%" align="center">
                        <TSPControls:CustomPrintToolbar ID="CustomPrintToolbar1" runat="server" ReportViewer="<%# RptMemberFile %>">
                            <ClientSideEvents ItemClick="function(s, e) {
 if(e.item.name == &quot;PrintReport&quot;)  {
   CallbackPanelReport.PerformCallback();
  }
}" />
                        </TSPControls:CustomPrintToolbar>
                        <br />
                        <dxxr:ReportViewer ID="RptMemberFile" PrintUsingAdobePlugIn="false" runat="server"
                            LoadingPanelText="لطفاً صبر نمایید">
                        </dxxr:ReportViewer>
                        <br />
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </TSPControls:CustomAspxCallbackPanel>
    </div>
    </form>
</body>
</html>
