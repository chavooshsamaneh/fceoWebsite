<%@ Page Title="ثبت اطلاعات" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ChangeMemberData.aspx.cs" Inherits="Members_ChangeMemberData" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]</div>
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="100%" 
                GroupBoxCaptionOffsetY="-24px" 
                 ShowHeader="true" HeaderText="ثبت اطلاعات" RightToLeft="True">               
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">
                        <div align="right" dir="rtl">
                            <asp:Label ID="lblSetMemberDataDescription" runat="server" Text="" ForeColor="DarkBlue"></asp:Label>
                            <br />
                            <br />
                            <asp:Panel ID="PanelMobileNo" runat="server">
                                <table>
                                    <tr>
                                        <td align="right" width="100px">
                                            <asp:Label runat="server" Text="شماره تلفن همراه" ID="Label42"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <TSPControls:CustomTextBox runat="server"  Width="250px" MaxLength="11" ID="txtMobileNo"
                                                >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="MemberData">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="شماره تلفن همراه را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="0\d{1,10}">
                                                    </RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents KeyPress="function(s,e){ if(CheckEnterKeyOnKeyPress(e,1)) btnSave.DoClick(); }" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="PanelEmail" runat="server">
                                <table>
                                    <tr>
                                        <td align="right" width="100px">
                                            <asp:Label runat="server" Text="پست الکترونیکی" ID="Label58"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <TSPControls:CustomTextBox runat="server"  Width="250px" ID="txtEmail" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="MemberData">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="پست الکترونیکی را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="پست الکترونیکی را با فرمت صحیح وارد نمایید" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                    </RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents KeyPress="function(s,e){ if(CheckEnterKeyOnKeyPress(e,1)) btnSave.DoClick(); }" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                            <div class="Item-center">
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server"
                                    Text="&nbsp;&nbsp;&nbsp;ذخیره"  
                                    ID="btnSave" ClientInstanceName="btnSave" OnClick="btnSave_Click" ImagePosition="Left">                                  
                                    <ClientSideEvents Click="function(s,e){ 
                                                             if (ASPxClientEdit.ValidateGroup('MemberData') == false)
                                                               e.processOnServer=false;
                                                             }" />
                                </TSPControls:CustomAspxButton>
                             
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
