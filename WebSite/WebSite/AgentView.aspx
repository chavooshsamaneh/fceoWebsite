<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="AgentView.aspx.cs" Inherits="AgentView" Title="مشخصات نمایندگی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="false">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
               
                <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server"
                    OnItemClick="ASPxMenu1_ItemClick">

                    <Items>
                        <dxm:MenuItem Selected="true" Name="Agent" Text="مشخصات نمایندگی">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="News" Text="اخبار نمایندگی">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="AgentList" Text="لیست نمایندگی ها">
                        </dxm:MenuItem>
                    </Items>

                </TSPControls:CustomAspxMenuHorizontal>

                <br />
                <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>

                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td width="15%" valign="top" align="right">
                                            <asp:Label runat="server" Text="نام نمایندگی" Width="100%" ID="Label10"></asp:Label>
                                        </td>
                                        <td width="30%" valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtName" Width="100%">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                    <RequiredField IsRequired="True" ErrorText="نام نمایندگی را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText=""></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td width="15%" valign="top" align="right">
                                          
   <asp:Label runat="server" Text="آدرس پست الکترونیکی" Width="100%" ID="lblEmail"></asp:Label>
                                        </td>
                                        <td width="30%" valign="top" align="left">
                                       <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtEmail" Width="100%">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره تلفن" Width="100%" ID="lblTel"></asp:Label>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtTel" MaxLength="12" Width="100%">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="فکس" Width="100%" ID="lblFax"></asp:Label>
                                        </td>
                                        <td dir="ltr" valign="top" align="left">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFax" MaxLength="12" Width="100%">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره همراه" Width="100%" ID="lblMobile"></asp:Label>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMobileNo" MaxLength="11"
                                                Width="100%">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                      <td valign="top" align="right">
                                            <asp:Label runat="server" Text="آدرس وب سایت" Width="100%" ID="lblWebsite"></asp:Label>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtWebsite" Width="100%">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="آدرس" Width="100%" ID="lblAddress"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="100%" ID="txtAddress" Width="100%">
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" width="100%">
                                              <asp:Label runat="server" Text="توضیحات" Width="100%" ID="lblDescription"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" width="100%">

                                            <TSPControls:CustomASPxHtmlEditor ID="txtDescription" ActiveView="Preview" runat="server"
                                                Width="100%" Height="100px">
                                                <Settings AllowContextMenu="False" AllowHtmlView="false" AllowDesignView="false"
                                                    AllowPreview="true" />
                                            </TSPControls:CustomASPxHtmlEditor>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
              
          
    <asp:HiddenField ID="PkAgentId" runat="server" Visible="False"></asp:HiddenField>
    <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
    <asp:HiddenField ID="AgentCode" runat="server" Visible="False"></asp:HiddenField>
</asp:Content>
