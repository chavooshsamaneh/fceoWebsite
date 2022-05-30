<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="Agents.aspx.cs" Inherits="Agents" Title="نمایندگی ها" %>

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
<%--<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>--%>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>

    <TSPControls:CustomAspxDevDataView ID="DataViewAgents" runat="server" DataSourceID="OdbAgent" Width="100%"
         ColumnCount="1" PagerPanelSpacing="0px" ItemSpacing="0px" PagerSettings-EndlessPagingMode="OnClick">
        <ItemStyle Width="100%" Height="20%"></ItemStyle>
        <ItemTemplate>

            <div class="col-lg-12 col-md-12 col-sm-6 col-xs-12 DataViewOneColumn ">
                <div class="row" style="min-height: 50px">
                    <a style="text-decoration:none" href='<%# "AgentView.aspx?AgentId=" + Utility.EncryptQS( Eval("AgentId").ToString() ) + "&PageMode=" + Utility.EncryptQS("View") %>' target="_blank">
                        <span class="TitleOragne">
                            <h3><%# Eval("Name") %></h3>
                        </span>
                    </a>
                </div>

                <div class="row">

                    <div class="col-lg-6" style="float: right">
                        <span>تلفن:<strong><%# Eval("Number") %></strong>
                        </span>
                    </div>

                    <div class="col-lg-6">
                        <span>پست الکترونیک:<strong><%# Eval("Email") %></strong>
                        </span>
                    </div>
                </div>
            <div class="col-lg-12">
              
                <span>آدرس:<strong><%# Eval("Address") %></strong>
                </span>

            </div>
            <div class="col-lg-12">
                <a id="btnView" href='<%# "AgentView.aspx?AgentId=" + Utility.EncryptQS( Eval("AgentId").ToString() ) + "&PageMode=" + Utility.EncryptQS("View") %>'>
                    <span class="continueLink">مشاهده</span>
                </a>
            </div>
            </div>
        </ItemTemplate>

    </TSPControls:CustomAspxDevDataView>


    <asp:ObjectDataSource ID="OdbAgent" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.AccountingAgentManager"
        OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>

</asp:Content>
