<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GlobalError.aspx.cs" Inherits="GlobalError" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
    <%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
	<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>سازمان نظام مهندسی ساختمان استان فارس</title>
    <link href="StyleSheet/MainStyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="StyleSheet/Default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        img, div, td
        {
            behavior: url(Script/iepngfix.htc);
        }
    </style>
</head>
<body dir="rtl">
    <form id="form1" runat="server">
    <div id="view" dir="rtl">
        <div id="head">
            <div id="mainMenu">
                <table width="100%">
                    <tr>
                        <td style="width: 75%" align="right">
                            <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu_Top" runat="server" AutoSeparators="All" 
                                font-names="Tahoma" font-size="11px" font-underline="False" ForeColor="White"
                                ItemLinkMode="TextAndImage" SeparatorColor="White"
                                SeparatorHeight="20px" SkinID="None" Width="100%">
                                <SettingsLoadingPanel Text="لطفاً صبر نمائيد" />
                                <%--SeparatorColor="#5386CB" ForeColor="#162436"--%>
                                <Items>
                                    <dxm:MenuItem Name="home" NavigateUrl="~/Default.aspx" Text="صفحه اول">
                                        <Image Url="~/Images/Icons/Home-32.png" Height="20px" Width="20px" ToolTip="صفحه اول">
                                        </Image>
                                    </dxm:MenuItem>
                                    <dxm:MenuItem NavigateUrl="~/Introduction.aspx" Text="معرفي سازمان">
                                        <Image Url="~/flash/Home/Entire Network.png" Height="20px" Width="20px" ToolTip="معرفي سازمان">
                                        </Image>
                                    </dxm:MenuItem>
                                    <dxm:MenuItem NavigateUrl="~/MembersInfo/Members.aspx" Text="اعضاي سازمان">
                                        <Image Url="~/Images/Icons/businessmen.png" Height="20px" Width="20px" ToolTip="اعضاي سازمان">
                                        </Image>
                                    </dxm:MenuItem>
                                    <dxm:MenuItem NavigateUrl="~/MembersInfo/Office.aspx" Text="اعضاي حقوقي">
                                        <Image Url="~/flash/Home/Office.png" Height="20px" Width="20px" ToolTip="اعضاي حقوقي">
                                        </Image>
                                    </dxm:MenuItem>
                                    <dxm:MenuItem NavigateUrl="~/News.aspx" Text="اخبار سازمان">
                                        <Image Url="~/flash/Home/knewsticker.png" Height="20px" Width="20px" ToolTip="اخبار سازمان">
                                        </Image>
                                    </dxm:MenuItem>
                                    <dxm:MenuItem NavigateUrl="~/Period.aspx" Text="آموزش">
                                        <Image Url="~/flash/Home/tutorials.png" Height="20px" Width="20px" ToolTip="آموزش">
                                        </Image>
                                    </dxm:MenuItem>
                                    <dxm:MenuItem Text="تماس با ما" NavigateUrl="~/ContactUs.aspx">
                                        <Image Url="~/Images/Icons/Contact-32.png" Height="20px" Width="20px" ToolTip="تماس با ما">
                                        </Image>
                                    </dxm:MenuItem>
                                    <%--<dxm:MenuItem Text="راهنما" NavigateUrl="~/ContactUs.aspx" Name="Help">
                                                <Image Url="~/Images/Icons/Help-32.png" Height="20px" Width="20px" ToolTip="راهنما">
                                                </Image>
                                            </dxm:MenuItem>--%>
                                </Items>
                                <ItemStyle HorizontalAlign="Center" Font-Bold="False" ForeColor="White" Wrap="False" />
                                <LinkStyle>
                                    <HoverFont Underline="True" />
                                </LinkStyle>
                                <Paddings Padding="0px" />
                                <SeparatorPaddings PaddingLeft="14px" PaddingRight="14px" PaddingTop="1px" />
                            </TSPControls:CustomAspxMenuHorizontal>
                            
                        </td>
                        <td style="width: 25%" align="left">
                            <%--<table>
                                        <tr>
                                            <td>
                                                <asp:Image runat="server" ID="ImageDate" ImageUrl="~/Images/Icons/Calendar.png" Height="20px"
                                                    Width="20px" ToolTip="تاريخ امروز" /></td>
                                            <td>--%>
                            <dxe:ASPxLabel id="lblDate" runat="server" text="" font-names="tahoma" font-size="8pt"
                                forecolor="white">
                            </dxe:ASPxLabel>
                            <%--</td>
                                        </tr>
                                    </table>--%>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="logo">
            </div>
        </div>
        <div id="PageContent" align="center">
        <table style="background-image: url(Images/GlobalError/GlobalError.png); display: block;
            width: 969px;  height: 408px">
            <tr>
                <td width="600px" valign="top" align="center">
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="شما در یکی از صفحات دچار این خطا شده اید :"
                        Font-Bold="true" ForeColor="darkred" Font-Size="10pt">
                    </asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblError" runat="server" Text="چنین صفحه ای در سایت وجود ندارد" Font-Bold="true"
                        ForeColor="darkblue" Font-Size="12pt">
                    </asp:Label>
                    <div style="height: 90px">
                    </div>
                    <table width="80%" align="left">
                        <tr>
                            <td>
                                <asp:LinkButton runat="server" ID="lnkHomePage" PostBackUrl="~/Default.aspx">
                                    <table>
                                        <tr>
                                            <%--<td>
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/GlobalError/Arrow.png" Height="5px"
                                                                    Width="8px" />
                                                            </td>--%>
                                            <td>
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Icons/Home-32.png" Height="20px"
                                                    Width="20px" ToolTip="صفحه اصلی" />
                                            </td>
                                            <td>
                                                صفحه اصلی
                                            </td>
                                        </tr>
                                    </table>
                                </asp:LinkButton>
                                <br />
                                <asp:LinkButton runat="server" ID="lnkIntroduction" PostBackUrl="~/Introduction.aspx">
                                    <table>
                                        <tr>
                                            <%--<td>
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/GlobalError/Arrow.png" Height="5px"
                                                                    Width="8px" />
                                                            </td>--%>
                                            <td>
                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/flash/Home/Entire Network.png"
                                                    Height="20px" Width="20px" ToolTip="معرفی سازمان" />
                                            </td>
                                            <td>
                                                معرفی سازمان
                                            </td>
                                        </tr>
                                    </table>
                                </asp:LinkButton>
                                <br />
                                <asp:LinkButton runat="server" ID="lnkMembers" PostBackUrl="~/MembersInfo/Members.aspx">
                                    <table>
                                        <tr>
                                            <%--<td>
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/GlobalError/Arrow.png" Height="5px"
                                                                    Width="8px" />
                                                            </td>--%>
                                            <td>
                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Icons/businessmen.png" Height="20px"
                                                    Width="20px" ToolTip="اعضای سازمان" />
                                            </td>
                                            <td>
                                                اعضای سازمان
                                            </td>
                                        </tr>
                                    </table>
                                </asp:LinkButton>
                                <br />
                                <asp:LinkButton runat="server" ID="lnkOffice" PostBackUrl="~/MembersInfo/Office.aspx">
                                    <table>
                                        <tr>
                                            <%--<td>
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/GlobalError/Arrow.png" Height="5px"
                                                                    Width="8px" />
                                                            </td>--%>
                                            <td>
                                                <asp:Image ID="Image4" runat="server" ImageUrl="~/flash/Home/Office.png" Height="20px"
                                                    Width="20px" ToolTip="اعضای حقوقی" />
                                            </td>
                                            <td>
                                                اعضای حقوقی
                                            </td>
                                        </tr>
                                    </table>
                                </asp:LinkButton>
                            </td>
                            <td valign="top">
                                <asp:LinkButton runat="server" ID="lnkLogin" PostBackUrl="~/Login.aspx">
                                    <table>
                                        <tr>
                                            <%--<td>
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/GlobalError/Arrow.png" Height="5px"
                                                                    Width="8px" />
                                                            </td>--%>
                                            <td>
                                                <asp:Image ID="Image11" runat="server" ImageUrl="~/Images/User.png" Height="20px"
                                                    Width="20px" ToolTip="ورود کاربر" />
                                            </td>
                                            <td>
                                                ورود کاربر
                                            </td>
                                        </tr>
                                    </table>
                                </asp:LinkButton>
                                <br />
                                <asp:LinkButton runat="server" ID="lnkNews" PostBackUrl="~/News.aspx">
                                    <table>
                                        <tr>
                                            <%--<td>
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/GlobalError/Arrow.png" Height="5px"
                                                                    Width="8px" />
                                                            </td>--%>
                                            <td>
                                                <asp:Image ID="Image5" runat="server" ImageUrl="~/flash/Home/knewsticker.png" Height="20px"
                                                    Width="20px" ToolTip="اخبار سازمان" />
                                            </td>
                                            <td>
                                                اخبار سازمان
                                            </td>
                                        </tr>
                                    </table>
                                </asp:LinkButton>
                                <br />
                                <asp:LinkButton runat="server" ID="lnkPeriod" PostBackUrl="~/Period.aspx">
                                    <table>
                                        <tr>
                                            <%--<td>
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/GlobalError/Arrow.png" Height="5px"
                                                                    Width="8px" />
                                                            </td>--%>
                                            <td>
                                                <asp:Image ID="Image6" runat="server" ImageUrl="~/flash/Home/tutorials.png" Height="20px"
                                                    Width="20px" ToolTip="آموزش" />
                                            </td>
                                            <td>
                                                آموزش
                                            </td>
                                        </tr>
                                    </table>
                                </asp:LinkButton>
                                <br />
                                <asp:LinkButton runat="server" ID="lnkContactUs" PostBackUrl="~/ContactUs.aspx">
                                    <table>
                                        <tr>
                                            <%--<td>
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/GlobalError/Arrow.png" Height="5px"
                                                                    Width="8px" />
                                                            </td>--%>
                                            <td>
                                                <asp:Image ID="Image9" runat="server" ImageUrl="~/Images/Icons/Contact-32.png" Height="20px"
                                                    Width="20px" ToolTip="تماس با ما" />
                                            </td>
                                            <td>
                                                تماس با ما
                                            </td>
                                        </tr>
                                    </table>
                                </asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <br />
                                <asp:LinkButton runat="server" ID="lnkBack" PostBackUrl="javascript:history.go(-1)">
                                    <table>
                                        <tr>
                                            <%--<td>
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/GlobalError/Arrow.png" Height="5px"
                                                                    Width="8px" />
                                                            </td>--%>
                                            <td>
                                                <asp:Image ID="Image10" runat="server" ImageUrl="~/Images/back.png" Height="20px"
                                                    Width="20px" ToolTip="بازگشت" />
                                            </td>
                                            <td>
                                                بازگشت
                                            </td>
                                        </tr>
                                    </table>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="45%" valign="top" align="center">
                    <br />
                    <asp:Panel runat="server" ID="PanelSpaceImgError">
                        <br />
                        <br />
                        <br />
                        <br />
                    </asp:Panel>
                    <asp:Image ID="ImgError" runat="server" ImageUrl="~/Images/GlobalError/404.png" />
                </td>
            </tr>
        </table>
        </div>
        <div id="foot">
            <table dir="rtl" style="width: 100%; font-size: 8pt; font-family: tahoma;">
                <tr>
                    <td align="right" style="width: 30%">
                        اين نرم افزار شامل قوانين کپي رايت مي باشد.
                    </td>
                    <td align="center" style="width: 55%">
                        تمام حقوق مادي و معنوي اين سايت متعلق به سازمان نظام مهندسي ساختمان استان فارس مي
                        باشد.
                    </td>
                    <td align="right" style="width: 5%">
                    </td>
                    <td align="right" style="width: 10%">
                        <asp:Label ID="lblSoftwareVersion" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="color: White; font-family: Tahoma; font-size: 8pt" align="right">
                        <table>
                            <tr>
                                <td valign="middle">
                                    مرورگرهاي پشتياني کننده :
                                </td>
                                <td>
                                    <asp:Image ID="imgIE" AlternateText="IE" ToolTip="Internet Explorer" Width="16px"
                                        Height="16px" runat="server" ImageUrl="~/Images/Icons/Browsers/IE.png" />
                                </td>
                                <td>
                                    <asp:Image ID="imgFireFox" AlternateText="Firefox" ToolTip="Mozilla Firefox" Width="16px"
                                        Height="16px" runat="server" ImageUrl="~/Images/Icons/Browsers/Firefox.png" />
                                </td>
                                <td>
                                    <asp:Image ID="imgChrome" AlternateText="Chrome" ToolTip="Google Chrome" Width="16px"
                                        Height="16px" runat="server" ImageUrl="~/Images/Icons/Browsers/Chrome.png" />
                                </td>
                                <td>
                                    <asp:Image ID="imgOpera" AlternateText="Opera" ToolTip="Opera" Width="16px" Height="16px"
                                        runat="server" ImageUrl="~/Images/Icons/Browsers/Opera.png" />
                                </td>
                                <td>
                                    <asp:Image ID="imgSafari" AlternateText="Safari" ToolTip="Safari" Width="16px" Height="16px"
                                        runat="server" ImageUrl="~/Images/Icons/Browsers/Safari.png" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="center">
                        پشتيباني توسط &nbsp; <a href="http://www.tarrahsamaneh.com" style="color: white;
                            text-decoration: none" target="_blank">شرکت طراح سامانه پارسيان</a> شماره تماس:
                        8324579
                    </td>
                    <td style="width: 5%">
                    </td>
                    <td align="right">
                        <asp:Label ID="lblSoftwareEditNo" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
