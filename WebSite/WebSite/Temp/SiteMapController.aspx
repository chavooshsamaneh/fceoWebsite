<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SiteMapController.aspx.cs"
    Inherits="SiteMapController" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxsm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="direction: ltr">
            <dxsm:ASPxSiteMapDataSource ID="ASPxSiteMapDataSource1" runat="server" EnableRoles="True"
                ShowStartingNode="True" SiteMapFileName="~/web.sitemap" StartFromCurrentNode="True"
                StartingNodeOffset="1" />
        </div>
    </form>
</body>
</html>
