<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="NezamChart.aspx.cs" Inherits="NezamChart" Title="چارت سازمانی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1" Namespace="DevExpress.Web.ASPxTreeList"
    TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.v17.1" Namespace="DevExpress.Web"
    TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1" Namespace="DevExpress.Web.ASPxTreeList"
    TagPrefix="dxwtl" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelChart" HeaderText="چارت سازمانی"
                    runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <TSPControls:CustomAspxDevTreeList runat="server" KeyFieldName="Id" ParentFieldName="ParentId" AutoGenerateColumns="False"
                                RightToLeft="True" ClientInstanceName="TreeNmChart" 
                                 DataSourceID="ObjdsNezamChart" Width="100%" ID="TreeListNmChart"
                                OnHtmlRowPrepared="TreeListNmChart_HtmlRowPrepared">
                                <Columns>
                                    <dxwtl:TreeListTextColumn FieldName="NcName" Caption="پست سازمانی" VisibleIndex="0">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwtl:TreeListTextColumn>
                                    <dxwtl:TreeListTextColumn FieldName="FullName" Caption="نام و نام خانوادگی" VisibleIndex="1">
                                        <DataCellTemplate>
                                            <dxe:ASPxLabel ID="lblFullName" runat="server" Width="189px" Text='<%# Bind("FullName") %>'>
                                            </dxe:ASPxLabel>
                                            <input id="chart<%# Container.NodeKey %>" type="hidden" value='<%# Eval("NodeType") %>' />
                                        </DataCellTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwtl:TreeListTextColumn>
                                    <dxwtl:TreeListTextColumn FieldName="FirstName" Caption="نام" Visible="False" VisibleIndex="2">
                                    </dxwtl:TreeListTextColumn>
                                    <dxwtl:TreeListTextColumn FieldName="LastName" Caption="نام خانوادگی" Visible="False"
                                        VisibleIndex="2">
                                        <DataCellTemplate>
                                            <dxe:ASPxLabel ID="lblLastName" runat="server" Width="189px" Text='<%# Bind("LastName") %>'>
                                            </dxe:ASPxLabel>
                                            <input id="chart<%# Container.NodeKey %>" type="hidden" value='<%# Eval("NodeType") %>' />
                                        </DataCellTemplate>
                                    </dxwtl:TreeListTextColumn>
                                    <dxwtl:TreeListTextColumn FieldName="NcId" Visible="False" VisibleIndex="4">
                                    </dxwtl:TreeListTextColumn>
                                    <dxwtl:TreeListTextColumn FieldName="NodeType" Name="NodeType" Visible="False" VisibleIndex="5">
                                        <DataCellTemplate>
                                        </DataCellTemplate>
                                    </dxwtl:TreeListTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevTreeList>
                            <br />
                            <div dir="rtl" align="right">
                                <fieldset style="width: 98%">
                                    <legend>راهنما</legend>
                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td valign="middle" align="right">
                                                    <asp:Label ID="Label1" runat="server" Width="16px" BackColor="Wheat" Height="16px"></asp:Label>
                                                </td>
                                                <td valign="middle" align="right">
                                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Width="108px" Text="ارشد پست سازمانی">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="middle" align="right">
                                                    <asp:Label ID="Label4" runat="server" Width="16px" BackColor="Blue" Height="16px"></asp:Label>
                                                </td>
                                                <td valign="middle" align="right">
                                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Width="131px" Text="پست سازمانی اصلی فرد">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </fieldset>
                            </div>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                    <asp:ObjectDataSource ID="ObjdsNezamChart" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="SelectNezamMemberChart" TypeName="TSP.DataManager.NezamMemberChartManager">
                        <SelectParameters>
                            <asp:Parameter Type="Int32" DefaultValue="0" Name="IsExternal"></asp:Parameter>
                            <asp:Parameter Type="Int32" DefaultValue="0" Name="InActive"></asp:Parameter>
                        </SelectParameters>
                    </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
 
</asp:Content>
