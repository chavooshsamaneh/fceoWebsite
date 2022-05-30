<%@ Page Title="تعرفه خدمات مهندسی طراحی و نظارت" Language="C#" MasterPageFile="~/MasterPageWebsite.master"
    AutoEventWireup="true" CodeFile="PriceArchive.aspx.cs" Inherits="PriceArchive" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>


<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">

    <TSPControls:CustomAspxDevDataView ID="CustomAspxDevDataViewPriceArchive" runat="server"
        ColumnCount="1" RowPerPage="10" DataSourceID="ObjectDataSource_PriceArchive" PagerSettings-EndlessPagingMode="OnScroll">        
        <ItemTemplate>
            <div class="row DataViewOneColumn" style="width: 100%">
                <div class="row">
                    <asp:Label ID="lblTitle" class="TableTitle" runat="server" Text='<%# Bind("YearName") %>'></asp:Label>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-12">
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="true" RightToLeft="True"
                            Text=' سال تعرفه :'>
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-12">
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" RightToLeft="True" Text='<%# Bind("Year") %>' Width="100%">
                        </dxe:ASPxLabel>
                    </div>
                  <%--  <div class="col-lg-3 col-md-3 col-sm-12">
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Font-Bold="true" RightToLeft="True"
                            Text=''>
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-12">
                        <dxe:ASPxLabel ID="lblstatus" runat="server" RightToLeft="True" Text='<%# Bind("CreateDate") %>' Width="100%">
                        </dxe:ASPxLabel>
                    </div>--%>
                </div>
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-12">
                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Font-Bold="true" RightToLeft="True"
                            Text='توضیحات :'>
                        </dxe:ASPxLabel>
                    </div>
                    <div class="col-lg-10 col-md-10 col-sm-12">
                        <dxe:ASPxLabel ID="Label1" runat="server" Font-Bold="true" RightToLeft="True" Text='<%# Bind("Description") %>' Width="100%">
                        </dxe:ASPxLabel>
                    </div>
                </div>
                <div class="row " style="float: left">
                    <asp:LinkButton ID="btnPriceArchiveShow" OnClick="btnPriceArchiveShow_Click" runat="server" CssClass="continueLink"
                        CommandArgument='<%# Bind("PriceArchiveId") %>'>مشاهده تعرفه</asp:LinkButton>

                </div>
            </div>
        </ItemTemplate>
    </TSPControls:CustomAspxDevDataView>
    <asp:ObjectDataSource ID="ObjectDataSource_PriceArchive" runat="server" TypeName="TSP.DataManager.TechnicalServices.PriceArchiveManager"
        SelectMethod="SelectTSPriceArchiveForHomePage"></asp:ObjectDataSource>
</asp:Content>
