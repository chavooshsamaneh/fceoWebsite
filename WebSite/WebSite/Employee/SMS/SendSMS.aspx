<%@ Page Title="ارسال پیام کوتاه" Language="C#" Async="true" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="SendSMS.aspx.cs" Inherits="Employee_SMS_SendSMS"
    EnableViewState="true" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="مشخصات پیام کوتاه" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                <table width="100%">
                    <tr>
                        <td colspan="4" align="right">
                            <ul class="HelpUL">
                                <li>در صورت مشاهده خطا در ارتباط با وب سرویس مگفا مدت زمانی تامل نمایید تا مشکل از جانب
                                        شرکت مگفا برطرف گردد.برای تماس مستقيم با بخش پشتيبانی فنی شرکت مگفا، می‌توانيد با
                                        شماره تلفن‌های (۰۲۱) ۸۸۵۰۶۰۸۹ و (۰۲۱) ۸۸۵۱۰۸۸۷ ارتباط برقرار نماييد. </li>
                                  
                            </ul>
                        </td>
                    </tr>
                    <tr>
                         <td width="15%">وب سرویس پیش فرض :

                         </td>
                        <td width="35%">
                              <asp:Label ID="lblCurrentWebService" runat="server"></asp:Label>
                        </td>
                         <td width="15%"></td>
                         <td width="35%"></td>
                    </tr>
                    <tr>
                        <td width="15%">هزینه پیام کوتاه :
                        </td>
                        <td width="35%">
                            <asp:Label runat="server" ID="lblSmsCost" Font-Bold="true" ></asp:Label>
                        </td>
                        <td width="15%">اعتبار باقیمانده :
                        </td>
                        <td width="35%">
                            <asp:Label runat="server" ID="txtRemainingCredit" Font-Bold="true" ></asp:Label>
                        </td>
                    </tr>
                     </table>
                <br />
                <br />
                     <table width="100%">

                    <tr>
                        <td width="15%">عنوان پیام کوتاه :
                        </td>
                          <td width="85%">
                            <asp:Label runat="server" ID="lblSmsSubject" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>متن پیام کوتاه :
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblSmsBody" ></asp:Label>
                        </td>
                    </tr>
                </table>



            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelSendSMS" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table width="100%">
                    <tr>
                        <td colspan="3"  align="center">
                            <br />
                            <asp:Label ID="lblConfirmSendSMS" runat="server"></asp:Label>
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <TSPControls:CustomAspxButton runat="server" Text="&nbsp;&nbsp;&nbsp;ارسال" Width="122px" ID="btnSend"
                                OnClick="btnSend_Click">
                                <Image Width="20px" Height="20px" Url="~/Images/Mobile.png" />
                                <ClientSideEvents Click="function(s,e){ setTimeout(function () { s.SetEnabled(false); btnCancel.SetEnabled(false); }, 1); }" />
                            </TSPControls:CustomAspxButton>
                            <TSPControls:CustomAspxButton  runat="server" Text="&nbsp;&nbsp;&nbsp;ذخیره" Width="122px" ID="btnSave"
                                OnClick="btnSave_Click">
                                <Image Width="20px" Height="20px" Url="~/Images/icons/save.png" />
                                <ClientSideEvents Click="function(s,e){ setTimeout(function () { s.SetEnabled(false); btnCancel.SetEnabled(false); }, 1); }" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td width="20px"></td>
                        <td align="right">
                            <TSPControls:CustomAspxButton runat="server" UseSubmitBehavior="False" 
                                ClientInstanceName="btnCancel" Text="&nbsp;&nbsp;&nbsp;بازگشت" Width="122px"
                                ID="btnCancel" PostBackUrl="ConfirmedSMS.aspx">
                                <Image Width="20px" Height="20px" Url="~/Images/icons/back.png" />
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelMessage" runat="server" Visible="false">
        <PanelCollection>
            <dxp:PanelContent>

                <div align="center">
                    <br />
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False"
                        Text="&nbsp;&nbsp;&nbsp;بازگشت" Width="122px" ID="ASPxButton1"
                        PostBackUrl="ConfirmedSMS.aspx">
                        <Image Width="20px" Height="20px" Url="~/Images/icons/back.png" />
                    </TSPControls:CustomAspxButton>
                </div>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
</asp:Content>
