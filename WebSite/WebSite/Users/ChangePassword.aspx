<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="Members_ChengeUserName" Title="تغییر رمز عبور" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
 
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                 <TSPControls:CustomAspxMenuHorizontal ID="MenuUserSettings" runat="server"  
                            AutoSeparators="RootOnly">
                            <Items>
                                <dxm:MenuItem Name="ChangePassword" Text="تغییر رمز عبور" NavigateUrl="ChangePassword.aspx">
                                    <Image Url="~/Images/password.png" Height="20px" Width="20px">
                                    </Image>
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="MemberPrivateInfoSetting" Text="نحوه ارائه اطلاعت شخصی" NavigateUrl="MemberPrivateInfoSetting.aspx">
                                    <Image Url="~/Images/PrivateSettings.png" Height="20px" Width="20px">
                                    </Image>
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="RecieveMagazineSetting" Text="دریافت فصلنامه" NavigateUrl="RecieveMagazineSetting.aspx">
                                    <Image Url="~/Images/Magazine.png" Height="20px" Width="20px">
                                    </Image>
                                </dxm:MenuItem>
                            </Items>
                         
                        </TSPControls:CustomAspxMenuHorizontal>
                 <br />
               <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel3" HeaderText="تغییر رمز عبور" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

       
                                <table width="380px" dir="rtl">
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label ID="Label49" runat="server" Text="نام کاربری"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox ID="txtUserName" runat="server" 
                                                 Width="170px" ReadOnly="true">
                                                <ReadOnlyStyle BackColor="Snow">
                                                </ReadOnlyStyle>
                                                <ValidationSettings>
                                                    
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label ID="Label2" runat="server" Text="رمز عبور فعلی"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox ID="txtOldPass" runat="server" 
                                                 Width="170px" Password="True">
                                                <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">
                                                    
                                                    <RequiredField ErrorText="رمز عبور فعلی را وارد نمایید" IsRequired="True" />
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label ID="Label3" runat="server" Text="رمز عبور جدید"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox ID="txtPassword" runat="server" ClientInstanceName="p1" 
                                                 Password="True" Width="170px">
                                                <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">
                                                    
                                                    <RequiredField ErrorText="رمز عبور جدید را وارد نمایید" IsRequired="True" />
                                                    <RegularExpression ErrorText="رمز عبور باید بین 6 تا 15 رقم باشد" ValidationExpression="\w{6,15}" />
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label ID="Label4" runat="server" Width="73px" Text="تکرار رمز عبور"></asp:Label>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox ID="txtPassword2" runat="server" ClientInstanceName="p2" 
                                                 Password="True" Width="170px">
                                                <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="تکرار رمز عبور را اشتباه وارد کرده اید"
                                                    ErrorTextPosition="Bottom">
                                                    
                                                    <RequiredField ErrorText="تکرار رمز عبور را وارد نمایید" IsRequired="True" />
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents Validation="function(s, e) {
	if(p1.GetText()!=p2.GetText())
		{
			e.isValid =false;
			p2.SetErrorText(&quot;تکرار کلمه عبور را اشتباه وارد کرده اید&quot;);
		}
}" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td style="vertical-align: top; text-align: right">
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword"
                                                ControlToValidate="txtPassword2" Display="Dynamic" Visible="False" Width="187px">تکرار رمز عبور را اشتباه وارد کرده اید</asp:CompareValidator>
                                        </td>
                                    </tr>--%>
                                </table>
                            
                            <br />
                            <div class="Item-center">
                                <TSPControls:CustomAspxButton runat="server"  CssClass="ButtonMenue" 
                                    Text="&nbsp;&nbsp;&nbsp;ذخیره"  
                                    ID="btnSave" OnClick="btnSave_Click" ImagePosition="Left">
                                </TSPControls:CustomAspxButton>
                            </div>
                        </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
            AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
 
</asp:Content>
