<%@ Page Title="مشخصات انواع بخشنامه ها" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="AddRulesType.aspx.cs" Inherits="Employee_HomePage_AddRulesType" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                            <table >
                                <tr>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                            ToolTip="جدید" UseSubmitBehavior="False">
                                          
                                            <Image  Url="~/Images/icons/new.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                            ToolTip="ویرایش"  UseSubmitBehavior="False">
                                          
                                            <Image  Url="~/Images/icons/edit.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                           
                                            <Image  Url="~/Images/icons/save.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                            ToolTip="بازگشت" UseSubmitBehavior="False">
                                            
                                            <Image  Url="~/Images/icons/Back.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    	<TSPControls:CustomASPxRoundPanel ID="RoundPanelFormType" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

      
                    <table dir="rtl" width="100%">
                        <tr>
                            <td align="right" valign="top" style="width: 15%">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="عنوان بخشنامه *">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="right" valign="top" style="width: 35%">
                                <TSPControls:CustomTextBox IsMenuButton="true" ID="txtRoleTypeName" runat="server" Width="100%" 
                                    >
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RequiredField ErrorText="عنوان را وارد نمایید" IsRequired="True" />
                                        
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                            <td align="right" valign="top" style="width: 15%"></td>
                            <td align="right" valign="top" style="width: 35%"></td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="توضیحات">
                                </dxe:ASPxLabel>
                            </td>
                            <td colspan="3" align="right" valign="top">
                                <TSPControls:CustomASPXMemo ID="txtDescription" runat="server" Height="37px" Width="100%" 
                                    >
                                    <ValidationSettings>
                                        
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomASPXMemo>
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
                                    <table>
                                        <tr>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server" CausesValidation="False"
                                                    EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                                    ToolTip="جدید" UseSubmitBehavior="False">
                                                   
                                                    <Image  Url="~/Images/icons/new.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" CausesValidation="False"
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                                    ToolTip="ویرایش"  UseSubmitBehavior="False">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/edit.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server" EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                                   
                                                    <Image  Url="~/Images/icons/save.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton6" runat="server" CausesValidation="False"
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                    ToolTip="بازگشت" UseSubmitBehavior="False">
                                                    
                                                    <Image  Url="~/Images/icons/Back.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </table>
                   </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
    <dx:ASPxHiddenField ID="HiddenFieldFormType" runat="server">
    </dx:ASPxHiddenField>
 
</asp:Content>

