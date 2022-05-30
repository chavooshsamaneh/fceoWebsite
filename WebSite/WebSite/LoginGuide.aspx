<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginGuide.aspx.cs" Inherits="LoginGuide" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>راهنمای نام کاربری</title>
</head>
<body dir="rtl" align="right">
    <form id="form1" runat="server" style="font-family: tahoma; font-size: 9pt">
        <div align="center">
            <table>
                <tr>
                    <td>
                        <asp:Image ID="imgTitle" runat="server" ImageUrl="~/Images/LoginGuide/Help.jpg" />
                    </td>
                    <td valign="middle">
                        <h1 style="font-family: tahoma; font-size: 10pt">راهنمای نام کاربری و ورود به سیستم</h1>
                    </td>
                </tr>
            </table>
        </div>
        <hr />
        <ul type="disc">
            <li><b>اعضا حقیقی</b><br />
                <p>
                    نام کاربری برای اعضای حقیقی، کد عضویت آن ها می باشد.
            <br />
                    کد عضویت را می توان از روی کارت عضویت بدست آورد، بدین صورت که شماره عضویت بطور مثال بصورت
            <span dir="ltr">"17-XX-<span><u>کد عضویت</u></span>"</span>
                    می باشد.
                </p>
                <br />
                <div align="center">
                    <asp:Image ID="imgMemberCardSample" runat="server" ImageUrl="~/Images/LoginGuide/MemberCardSample.jpg" />
                </div>
            </li>
            <li><b>کارمندان</b>
                <p>
                    کاربرانی که رمز یکبار عبور برای آنها الزامی می باشد:
                     <br />
                    <ol type="1">
                        <li>نام کاربری و رمز عبور خودرا وارد نمایید و سپس برروی "ارسال پیامک" کلیک نمایید</li>
                        <li>صبر نمایید تا رمز یکبار عبور برروی گوشی شما ارسال شود</li>
                        <li>با استفاده از نام کاربری و رمز عبور و رمز یکبار عبور ارسال شده بر روی گوشی همراه خود می توانید وارد سیستم شوید
                        </li>
                    </ol>
                </p>
            </li>
        </ul>
    </form>
</body>
</html>
