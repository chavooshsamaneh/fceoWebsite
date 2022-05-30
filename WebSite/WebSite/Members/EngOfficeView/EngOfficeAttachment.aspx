<%@ Page Title="مستندات" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="EngOfficeAttachment.aspx.cs" Inherits="Members_EngOfficeView_EngOfficeAttachment" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
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
    <div id="Content" runat="server" style="width: 100%; display: block;" align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="DivReport" runat="server" class="DivErrors" style="text-align: right" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                width="100%">
                                <tr>
                                    <td align="right">
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
                <div dir="ltr" style="width: 100%" align="right">
                    <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server"  OnItemClick="ASPxMenu1_ItemClick">
                        <Items>
                            <dxm:MenuItem Name="EngOffice" Text="مشخصات دفتر">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Member" Text="اعضای دفتر">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Job" Text="سوابق کاری">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Attach" Text="مستندات" Selected="true">
                            </dxm:MenuItem>
                        </Items>
                        
                    </TSPControls:CustomAspxMenuHorizontal>
                </div>
                <br />
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                      Width="100%"
                    KeyFieldName="AttachId" ClientInstanceName="grid" DataSourceID="ObjectDataSource1"
                    OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared">
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="AttachId" Name="AttachId" Visible="False"
                            VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="فایل" VisibleIndex="0">
                            <DataItemTemplate>
                                <dxe:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="ASPxHyperLink" Target="_blank"
                                    NavigateUrl='<%# Bind("FilePath") %>' OnDataBinding="ASPxHyperLink1_DataBinding">
                                </dxe:ASPxHyperLink>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataImageColumn Caption="تصویر" FieldName="FilePath" Name="FilePath"
                            Visible="False" VisibleIndex="0">
                            <PropertiesImage ImageHeight="75px" ImageWidth="75px">
                            </PropertiesImage>
                        </dxwgv:GridViewDataImageColumn>
                        <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" Name="Description"
                            VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="RefTable" Visible="False" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                </TSPControls:CustomAspxDevGridView>
                <br />
                <asp:HiddenField ID="EngOfficeId" runat="server" Visible="False" />
                <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
                <asp:HiddenField ID="EngFileId" runat="server" Visible="False" />
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="FindByTablePrimaryKey"
                    TypeName="TSP.DataManager.AttachmentsManager">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="ttid" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="code" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                    BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                            <img align="middle" src="../../Image/indicator.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                width="100%">
                                <tr>
                                    <td align="right">
                                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                            <tr>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server" CausesValidation="False" 
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
