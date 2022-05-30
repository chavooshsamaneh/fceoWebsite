<%@ Page Title="آرشیو گزارشات صوتی" Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true" CodeFile="Podcasts.aspx.cs" Inherits="Podcasts" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <TSPControls:CustomAspxDevDataView ID="DataViewPodcast" runat="server" Width="100%"
        RowPerPage="10" 
        DataSourceID="ObjectDataSourcePodcast" ColumnCount="3" ItemSpacing="5px" RightToLeft="True" PagerStyle-ItemSpacing="10px" PagerSettings-EndlessPagingMode="OnScroll">
       
        <ItemTemplate>

            <div class="box-owl-scroll">
                                                <a class="link-row" target="_blank" runat="server">
                                                    <div class="overlab img-one">
                                                        <%# Eval("ImageURL") %>
                                                    </div>
                                                    <div class="box-contnt">
                                                        <div class="date-scroll"><%# Eval("CreateDate") %></div>
                                                        <div class="title-scroll"><%# Eval("Description") %></div>
                                                    </div>
                                                </a>
                                            </div>
        </ItemTemplate>

    </TSPControls:CustomAspxDevDataView>
    <asp:ObjectDataSource ID="ObjectDataSourcePodcast" runat="server" SelectMethod="FindImageType"
        TypeName="TSP.DataManager.SiteImageManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DbType="Int32" DefaultValue="3" Name="ImageType" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

