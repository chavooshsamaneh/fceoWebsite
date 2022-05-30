<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="Links.aspx.cs" Inherits="Links" Title="لینک های مرتبط" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="جستجو" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <div class="row">
                            <div class="col-md-2">
                                نام لینک
                            </div>
                            <div class="col-md-4">

                                <TSPControls:CustomTextBox runat="server" ID="txtName">
                                </TSPControls:CustomTextBox>
                            </div>
                            <div class="col-md-2">نوع لینک
                            </div>
                            <div class="col-md-4">    <TSPControls:CustomAspxComboBox runat="server" DropDownStyle="DropDown"
                                            TextField="Name" ID="ComboType"
                                            DataSourceID="ObjectDataSource2" ValueType="System.String" ValueField="LiTId"
                                            RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                            </div>
                        </div>
                        <div class="Item-center">
                                        <TSPControls:CustomAspxButton runat="server" Text="نمایش" ID="btnSearch" UseSubmitBehavior="false"
                                            OnClick="btnSearch_Click">
                                        </TSPControls:CustomAspxButton>
                                  </div>
                        <asp:ObjectDataSource runat="server" SelectMethod="Search" ID="ObjectDataSource1"
                            TypeName="TSP.DataManager.LinksManager" OldValuesParameterFormatString="original_{0}">
                            <SelectParameters>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="%" Name="LiName"
                                    ControlID="txtName"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Value" Type="Int16" DefaultValue="-1" Name="TypeId"
                                    ControlID="ComboType"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource runat="server" SelectMethod="GetData" ID="ObjectDataSource2"
                            TypeName="TSP.DataManager.LinkTypeManager"></asp:ObjectDataSource>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomAspxDevDataView runat="server"
               EnableCallbackCompression="True" SettingsTableLayout-ColumnCount="1"
              ID="ASPxDataView1" EnableViewState="False" PagerSettings-EndlessPagingMode="OnClick">

                <ItemTemplate>
                    <table  class="DataViewOneColumn" width="100%">
                        <tbody>
                            <tr>
                                <td style="vertical-align: middle; padding: 5px 5px 5px 5px; text-align: right" class="TableTitle">
                                    <dxe:ASPxLabel ID="lblTitle" Font-Bold="True" runat="server" Text='<%# Bind("LiName") %>'>
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: right; padding: 5px 5px 5px 5px;">
                                    <table cellpadding="7px" width="100%">
                                        <tbody>
                                            <tr>
                                                <td width="15%" align="right" valign="top" rowspan="3">
                                                    <asp:Image ID="Image2" runat="server" Height="100px"
                                                        ImageUrl='<%# Eval("ImageUrl").ToString().Replace("~/","") %>'
                                                        OnDataBinding="Image_DataBinding" Width="100px" />
                                                </td>
                                                <td align="right" valign="top" width="15%">
                                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="نوع لینک :">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td width="75%" align="right" valign="top">
                                                    <dxe:ASPxLabel ID="lblGroup" runat="server" Text='<%# Bind("TypeName") %>'>
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top" width="15%">
                                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="آدرس لینک :">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td width="75%" align="right" valign="top">
                                                    <dxe:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text='<%# Bind("LinkAddress") %>'
                                                        NavigateUrl='<%# Bind("LinkAddress") %>' Target="_blank">
                                                    </dxe:ASPxHyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top" width="15%">
                                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="توضیحات :">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td width="75%" align="right" valign="top">
                                                    <dxe:ASPxLabel ID="lblDesc" runat="server" Text='<%# Bind("Description") %>'>
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ItemTemplate>
            </TSPControls:CustomAspxDevDataView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                <img id="IMG1" src="Image/indicator.gif" align="middle" />
                لطفا صبر نمایید ...
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
