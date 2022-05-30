<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="TeacherAttachment.aspx.cs" Inherits="Teachers_TeacherInfo_TeacherAttachment"
    Title="مستندات" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                    width="100%">
                                    <tr>
                                        <td style="vertical-align: top" align="right">
                                            <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                <tr>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False" 
                                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                            ToolTip="بازگشت" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                           </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <TSPControls:CustomAspxMenuHorizontal ID="MenuTeacherInfo" runat="server" 
                      ItemSpacing="0px" OnItemClick="ASPxMenu1_ItemClick">
                    <Items>
                        <dxm:MenuItem Text="مشخصات فردی" Name="BasicInfo">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Job" Text="سوابق آموزشی">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Research" Text="تالیفات و تحقیقات">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Atachment" Text="مستندات" Selected="True">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Course" Text="دروس" Visible="false">
                        </dxm:MenuItem>
                    </Items>
                  
                </TSPControls:CustomAspxMenuHorizontal>
            </div>
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewAttachment" runat="server" AutoGenerateColumns="False"
                  Width="100%"
                KeyFieldName="AttachId" ClientInstanceName="GridViewAttachment">
                
                <Columns>
                    <dxwgv:GridViewDataImageColumn Caption="مستندات" FieldName="FilePath" VisibleIndex="0"
                        Visible="False">
                        <propertiesimage imageheight="24px" imagewidth="24px"></propertiesimage>
                    </dxwgv:GridViewDataImageColumn>
                    <dxwgv:GridViewDataTextColumn Caption="فایل" FieldName="FilePath" Name="FilePath"
                        VisibleIndex="0">
                        <dataitemtemplate>
<dxe:ASPxHyperLink id="ASPxHyperLink1" runat="server" Text="ASPxHyperLink" OnDataBinding="ASPxHyperLink1_DataBinding" NavigateUrl='<%# Bind("FilePath") %>' Target="_blank"></dxe:ASPxHyperLink>
</dataitemtemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" Width="1px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="توضیحات" VisibleIndex="2" FieldName="Description">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>
           <br />
         <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                            <table >
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server" CausesValidation="False" 
                                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                            ToolTip="بازگشت" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </table></dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                                            <dxhf:ASPxHiddenField ID="HiddenFieldTeCertificate" runat="server">
                                            </dxhf:ASPxHiddenField>
</asp:Content>
