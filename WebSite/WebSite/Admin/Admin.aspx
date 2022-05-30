<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin_Admin" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>پنل مدیریت سایت</title>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <br />
    <div align="center">
                    <TSPControls:CustomASPxRoundPanel ID="PanelMenu" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

       
       
                    <div align="right" width="100%">
                        <b style="color: DarkBlue">پرتال</b>
                        <hr style="color: DarkBlue" width="50%" align="right"  />
                    </div>
                    <table width="100%" cellspacing="20px">
                        <tr>
                            <td width="33%" align="center">
                                <a href="AdminConfirm.aspx" style="text-decoration: none">
                                    <asp:Image runat="server" ID="Image1" ImageUrl="~/Images/Password-64.png" Width="64px"
                                        Height="64px" /><br />
                                    تغییر رمز عبور </a>
                            </td>
                            <td width="34%" align="center">
                                <a href="../Login.aspx" style="text-decoration: none">
                                    <asp:Image runat="server" ID="Image2" ImageUrl="~/Images/x-office-contact.png" Width="64px"
                                        Height="64px" /><br />
                                    ورورد به پرتال </a>
                            </td>
                            <td width="33%" align="center">
                                <a href="../Default.aspx" style="text-decoration: none">
                                    <asp:Image runat="server" ID="Image3" ImageUrl="~/Images/Home-64.png" Width="64px"
                                        Height="64px" /><br />
                                    صفحه اصلی </a>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <div align="right" width="100%">
                        <b style="color: DarkBlue">مدیریت سایت</b>
                        <hr style="color: DarkBlue" width="50%" align="right"  />
                    </div>
                    <table width="100%" cellspacing="20px">
                        <tr>
                            <td width="33%" align="center">
                                <a href="WebSiteError.aspx" style="text-decoration: none">
                                    <asp:Image runat="server" ID="Image4" ImageUrl="~/Images/Errors-64.png" Width="64px"
                                        Height="64px" /><br />
                                    خطاهای سایت </a>
                            </td>
                            <td width="34%" align="center">
                                <a href="DBAdmin.aspx" style="text-decoration: none">
                                    <asp:Image runat="server" ID="Image5" ImageUrl="~/Images/DatabaseAdmin-64.png" Width="64px"
                                        Height="64px" /><br />
                                    مدیریت دیتابیس </a>
                            </td>
                            <td width="33%" align="center">
                                <a href="SiteSettings.aspx" style="text-decoration: none">
                                    <asp:Image runat="server" ID="Image6" ImageUrl="~/Images/Settings-64.png" Width="64px"
                                        Height="64px" /><br />
                                    تنظیمات سایت </a>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <a href="StringEncryptDecrypt.aspx" style="text-decoration: none">
                                    <asp:Image runat="server" ID="Image7" ImageUrl="~/Images/Half Encrypted-64.png" Width="64px"
                                        Height="64px" /><br />
                                    رمزگذاری و رمزگشایی </a>
                            </td>
                        </tr>
                    </table>
                    <br />
               </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    </div>
    </form>
</body>
</html>
