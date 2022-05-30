<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="Forms.aspx.cs" Inherits="Forms" Title="آرشیو فرم ها" %>

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
    <div id="divcontent" style="vertical-align: top; width: 100%;" align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
           	<TSPControls:CustomASPxRoundPanel ID="RoundPanelPage" HeaderText="جستجو" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
  
                                    <table style="width: 100%" dir="rtl" >
                                        <tbody>
                                            <tr>
                                                <td valign="top" align="right" width="15%">
                                                    <dxe:ASPxLabel runat="server" Text="کد فرم" ID="ASPxLabel1">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" width="35%">
                                                    <TSPControls:CustomTextBox runat="server" Width="100%"  
                                                        ID="txtCode">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right" width="15%">
                                                    <dxe:ASPxLabel runat="server" Text="نوع فرم" Width="100%" ID="ASPxLabel2">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" width="35%">
                                                    <TSPControls:CustomAspxComboBox runat="server"  Width="100%" TextField="FormTypeName"
                                                        ID="cmbFormType"  DataSourceID="ObjdsFormsType"
                                                        ValueType="System.String" RightToLeft="True" ValueField="FormTypeId" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="نوع فرم را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomAspxComboBox>
                                                    <asp:ObjectDataSource ID="ObjdsFormsType" runat="server" TypeName="TSP.DataManager.FormsTypeManager"
                                                        SelectMethod="SelectActiveType"></asp:ObjectDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right" width="15%">
                                                    <dxe:ASPxLabel runat="server" Text="نام فرم" Width="100%" ID="ASPxLabel3">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" colspan="3">
                                                    <TSPControls:CustomTextBox runat="server" Width="100%"  
                                                        ID="txtName">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4">
                                                
                                                    <TSPControls:CustomAspxButton runat="server" UseSubmitBehavior="False" Text="نمایش" 
                                                         ID="btnSearch" OnClick="btnSearch_Click">
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                     </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                <br />
                                    <TSPControls:CustomAspxDevDataView ID="DataViewForms" runat="server" ColumnCount="1"
                                        DataSourceID="ObjdsForms" 
                                       PagerSettings-EndlessPagingMode="OnClick">
                                   
                                        <ItemTemplate>
                                            <table class="DataViewOneColumn" dir="rtl" width="100%" >
                                                <tbody>
                                                    <tr>
                                                        <td align="right" class="TableTitle" colspan="4" valign="middle">
                                                            <dxe:ASPxLabel ID="lblName" Font-Bold="true" runat="server" Text='<%# Bind("FoName") %>'>
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="4" valign="top">
                                                            <table width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="right" style="width: 15%" valign="top">
                                                                            <dxe:ASPxLabel ID="ASPxLabel1" Font-Bold="true" runat="server" Text="کد فرم :">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td align="right" colspan="3" valign="top" style="width: 85%">
                                                                            <dxe:ASPxLabel ID="lblCode" runat="server" Text='<%# Bind("FoCode") %>'>
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="top">
                                                                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="نوع فرم :" Font-Bold="true">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td align="right" colspan="3" valign="top">
                                                                            <dxe:ASPxLabel ID="lblGroup" runat="server" Text='<%# Bind("FormTypeName") %>'>
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="top">
                                                                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="توضیحات :" Font-Bold="true">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td align="right" style="text-align: justify" colspan="4" valign="top">
                                                                            <dxe:ASPxLabel ID="lblDesc" runat="server" Text='<%# Bind("Description") %>'>
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td >
                                                                            <dxe:ASPxHyperLink CssClass="continueLink" ID="HpLink" runat="server" NavigateUrl='<%# Bind("PdfUrl") %>'
                                                                                Target="_blank" Text="دانلود">
                                                                            </dxe:ASPxHyperLink>
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
                                
                    <asp:ObjectDataSource runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="Search" TypeName="TSP.DataManager.FormsManager" ID="ObjdsForms">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="%" Name="FoCode" Type="String" />
                            <asp:Parameter DefaultValue="%" Name="FoName" Type="String" />
                            <asp:Parameter DefaultValue="-1" Name="FormTypeId" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
       
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
            AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
            <ProgressTemplate>
                <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                    <img id="IMG1" src="Image/indicator.gif" align="middle" />
                    لطفا صبر نمایید ...</div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>

</asp:Content>
