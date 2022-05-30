<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ContactUsSettings.aspx.cs" Inherits="Employee_Management_ContactUsSettings"
    Title="تنظیمات تماس با ما" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="false">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]</div>
              <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


 
                                <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                    width="100%">
                                    <tr>
                                        <td style="vertical-align: top" align="right">
                                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                cellpadding="0">
                                                <tr>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  EnableTheming="False"
                                                            ToolTip="ذخیره" ID="btnSave" EnableViewState="False" OnClick="btnSave_Click"
                                                            UseSubmitBehavior="False">
                                                            <Image  Url="~/Images/icons/save.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            <br />
                <TSPControls:CustomAspxDevTreeList runat="server" KeyFieldName="NcId" ParentFieldName="ParentId"
                    AutoGenerateColumns="False"  
                    DataSourceID="ObjectDataSource1" ID="TreeListMemberChart" EnableViewState="False"
                    Width="100%" RightToLeft="True">
                    <Columns>
                        <dxwtl:TreeListTextColumn FieldName="NcName" Caption="نام بخش" VisibleIndex="0" Width="50%">
                        </dxwtl:TreeListTextColumn>
                        <dxwtl:TreeListTextColumn FieldName="EmployeeName" Caption="مسئول" VisibleIndex="1"
                            Width="50%">
                        </dxwtl:TreeListTextColumn>
                        <dxwtl:TreeListTextColumn FieldName="NcId" Caption="NcId" Visible="False" VisibleIndex="2">
                        </dxwtl:TreeListTextColumn>
                    </Columns>
                    <SettingsBehavior AllowDragDrop="False" ExpandCollapseAction="NodeDblClick"></SettingsBehavior>
                    <SettingsSelection Enabled="True" AllowSelectAll="True"></SettingsSelection>
                </TSPControls:CustomAspxDevTreeList>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SelectNezamMemberChartForContactUs"
                    TypeName="TSP.DataManager.NezamMemberChartManager"></asp:ObjectDataSource>
           
            <br />
              <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                             
                                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                cellpadding="0">
                                                <tr>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  EnableTheming="False"
                                                            ToolTip="ذخیره" ID="btnSave2" EnableViewState="False" OnClick="btnSave_Click"
                                                            UseSubmitBehavior="False">
                                                            <Image  Url="~/Images/icons/save.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </table></dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                                       
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                <img alt="" id="IMG2" src="../../Image/indicator.gif" align="middle" />
                لطفا صبر نمایید ...</div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
