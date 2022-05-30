<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="MemberPrivateInfoSetting.aspx.cs" Inherits="Users_MemberPrivateInfoSetting"
    Title="نحوه ارائه اطلاعت شخصی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Content" runat="server" align="center">
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
                <div dir="ltr">
                    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="100%" 
                        GroupBoxCaptionOffsetY="-24px" 
                         ShowHeader="true" HeaderText="نحوه ارائه اطلاعت شخصی">
                        <PanelCollection>
            <dx:PanelContent>
                                <div align="right" dir="rtl">
                                    <p style="text-align: justify; line-height: 15pt">
                                        با توجه به درخواست برخی شرکت های تولیدی و خدماتی جهت تبلیغ محصولات خود و مصوبه هیئت
                                        رئیسه سازمان در رابطه با اخذ مجوز از همکاران جهت ارائه شماره تلفن همراه و یا آدرس
                                        محل سکونت به شرکت های فوق الذکر، خواهشمند است گزینه مورد نظر جهت نحوه ارائه اطلاعات
                                        را انتخاب نمایید :
                                    </p>
                                    <br />
                                    <TSPControls:CustomASPxRadioButtonList runat="server" RightToLeft="True" SelectedIndex="0" 
                                          Width="100%"
                                         ID="rbtListUserInfoType" __designer:wfdid="w2" Border-BorderWidth="0px">
                                        <Items>
                                            <dx:ListEditItem Text="با ارائه آدرس و شماره تلفن همراه موافق می باشم" Value="1"
                                                Selected="True"></dx:ListEditItem>
                                            <dx:ListEditItem Text="تنها با ارائه شماره تلفن همراه موافق می باشم" Value="2"></dx:ListEditItem>
                                            <dx:ListEditItem Text="تنها با ارائه آدرس محل سکونت موافق می باشم" Value="3"></dx:ListEditItem>
                                            <dx:ListEditItem Text="با ارائه هر گونه اطلاعات مخالف می باشم" Value="4"></dx:ListEditItem>
                                        </Items>
                                    </TSPControls:CustomASPxRadioButtonList>
                                    <br />
                                     <div class="Item-center">
                                <TSPControls:CustomAspxButton runat="server"  CssClass="ButtonMenue" 
                                            Text="&nbsp;&nbsp;&nbsp;ذخیره"  
                                            ID="btnSave" OnClick="btnSave_Click" ImagePosition="Left">
                                        </TSPControls:CustomAspxButton>
                                    </div>
                                </div>
                              </dx:PanelContent>
        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanel>
                    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                        AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
                        <ProgressTemplate>
                            <div class="modalPopup">
                                لطفا صبر نمایید
                                <img src="../Image/indicator.gif" align="middle" />
                            </div>
                        </ProgressTemplate>
                    </asp:ModalUpdateProgress>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
