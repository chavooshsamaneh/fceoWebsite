<%@ Page Title="آرشیو گزارشات ویدیویی" Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true" CodeFile="Videos.aspx.cs" Inherits="Videos" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
<style>.h_iframe-aparat_embed_frame{position:relative;} .h_iframe-aparat_embed_frame .ratio {display:block;width:100%;height:auto;} .h_iframe-aparat_embed_frame iframe {position:absolute;top:0;left:0;width:100%; height:100%;}</style>
    
    <TSPControls:CustomAspxDevDataView ID="DataViewPodcast" runat="server" Width="100%"
        RowPerPage="10" 
        DataSourceID="ObjectDataSourcePodcast" ColumnCount="3" ItemSpacing="5px" RightToLeft="True" PagerStyle-ItemSpacing="10px" PagerSettings-EndlessPagingMode="OnScroll">
       
        <ItemTemplate>

           <div style="width:320px; height:100%">
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



