<%@ Page Title="تنظیمات رمز یکبار عبور" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="TempPassSetting.aspx.cs" Inherits="Employee_ControlUserOperations_TempPassSetting" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]</div>

         <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>

                                            <table >
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                    ID="btnSave" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnSave_Click" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>
                                 
                                    <Image  Url="~/Images/icons/save.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
  </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>

    <br />
       <TSPControls:CustomASPxRoundPanel ID="RoundPanelTempPass" HeaderText="تنظیمات رمز یکبار عبور" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                 <fieldset>
                                                    <dl style="font-family:tahoma; font-size: 7pt; line-height: 15pt">
                                                        <p class="HelpUL">
                                                            لازم است هر کاربر تمامی نکات زیر را مطالعه کرده و به صورت کامل متوجه شود.
                                                        </p>
                                                        <dt>مربوط به تنظیمات این صفحه</dt>
                                                        <dd>با فعال بودن وضعیت رمز یکبار عبور از میان افراد هر گروه آن کاربرانی که رمز یکبار عبور برای آنها اجباری شده است ملزم به وارد کردن رمز یکبار عبور می شوند.</dd>
                                                     <dd>با استفاده از تنظیمات زیر می توانید موتور تولید رمز یکبار عبور و اعتبار سنجی آن را برای گروه های کاربری مختلف راه اندازی نمود</dd>
                                                        <dt>سیاست های کلی رمز یکبار عبور</dt>
                                                        <dd>جهت استفاده از رمز یکبار عبور باید سرویس ارسال پیامک فعال باشد و کاربر نیز شماره فعال تلفن همراه داشته باشد.</dd>
                                                         <dd>اگر در عرض دو دقیقه یک کاربر معتبر و مشخص بیش از سه  بار برای دریافت رمز یکبار عبور اقدام نماید سیستم تا ده دقیقه رمز یکبار عبور برای آن کاربر تولید نخواهد کرد.</dd>
                                                          <dt>سیاستهای کلی ورود</dt>
                                                         <dd>اگر در عرض سه دقیقه بیش از پنج تلاش ناموفق از طرف یک نشانی پروتکل اینترنت مشخص صورت گیرد آن نشانی تا بیست دقیقه مسدود می شود و اجازه ورود نخواهد داشت</dd>
                                                        <dd>تلاش های ناموفق از طرف یک نشانی پروتکل اینترنت مشخص جهت دریافت رمز عبور موقت نیز جز سهمیه پنج بار تلاش ناموفق محسوب می شوند</dd>
                                                    </dl>
                                                </fieldset>

                    <table width="100%">
                    <tr style=" padding-bottom: 1em">
                        <td width="30%" valign="top" align="right">
                           رمز یکبار عبور کارمندان
                        </td>
                        <td width="30%" valign="top" align="right">
                            <TSPControls:CustomAspxComboBox runat="server"  ID="cmbEmpTempPass"
                                 Width="100%" RightToLeft="True" >
                                <ItemStyle HorizontalAlign="Right" />
                                <Items>
                                     <dxe:ListEditItem Selected="true" Text="غیرفعال" Value="false" />
                                    <dxe:ListEditItem  Text="فعال" Value="true" />
                                </Items>
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <RequiredField IsRequired="true" ErrorText="وضعیت رمز یکبار عبور کارمندان را مشخص کنید" />
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td width="40%" valign="top" align="right">
                        </td>
                    </tr>
                             <tr style=" border-spacing:15em" >
                        <td   valign="top" align="right">
                           رمز یکبار عبور اعضای مسکن
                        </td>
                        <td valign="top" align="right">
                            <TSPControls:CustomAspxComboBox runat="server"  ID="cmbStlTempPass"
                                 Width="100%" RightToLeft="True" >
                                <ItemStyle HorizontalAlign="Right" />
                                <Items>
                                     <dxe:ListEditItem Selected="true" Text="غیرفعال" Value="false" />
                                    <dxe:ListEditItem  Text="فعال" Value="true" />
                                </Items>
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <RequiredField IsRequired="true" ErrorText="وضعیت رمز یکبار عبور اعضای مسکن را مشخص کنید" />
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td valign="top" align="right">
                        </td>
                    </tr>
                               <tr >
                        <td  valign="top" align="right">
                           رمز یکبار عبور اعضای حقیقی
                        </td>
                        <td  valign="top" align="right">
                            <TSPControls:CustomAspxComboBox runat="server"  ID="cmbMeTempPass"
                                 Width="100%" RightToLeft="True" >
                                <ItemStyle HorizontalAlign="Right" />
                                <Items>
                                     <dxe:ListEditItem Selected="true" Text="غیرفعال" Value="false" />
                                    <dxe:ListEditItem  Text="فعال" Value="true" />
                                </Items>
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <RequiredField IsRequired="true" ErrorText="وضعیت رمز یکبار عبور اعضای حقیقی را مشخص کنید" />
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td  valign="top" align="right">
                        </td>
                    </tr>
                               <tr>
                        <td  valign="top" align="right">
                           رمز یکبار عبور اعضای حقیقی موقت
                        </td>
                        <td  valign="top" align="right">
                            <TSPControls:CustomAspxComboBox runat="server"  ID="cmbTempMeTempPass"
                                 Width="100%" RightToLeft="True" >
                                <ItemStyle HorizontalAlign="Right" />
                                <Items>
                                     <dxe:ListEditItem Selected="true" Text="غیرفعال" Value="false" />
                                    <dxe:ListEditItem  Text="فعال" Value="true" />
                                </Items>
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <RequiredField IsRequired="true" ErrorText="وضعیت رمز یکبار عبور اعضای حقیقی موقت را مشخص کنید" />
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td  valign="top" align="right">
                        </td>
                    </tr>
                </table>


        </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>

    <br />

         <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                <table >
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                    ID="btnSave2" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnSave_Click" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>
                                 
                                    <Image  Url="~/Images/icons/save.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>

  </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>


</asp:Content>

