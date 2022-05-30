<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="Course.aspx.cs" Inherits="Members_Amoozesh_Course" Title="واحدهای درسی مورد تایید نظام مهندسی" %>

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
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <TSPControls:CustomAspxDevDataView ID="DataViewCourse" ClientInstanceName="DataViewAlbum"
        runat="server" DataSourceID="OdbCourse" Width="100%" PagerSettings-EndlessPagingMode="OnClick" SettingsTableLayout-ColumnCount="1">

        <ItemTemplate>
            <div class="DataViewOneColumn" style="width:100%;max-width:100%">
                <div class="row">
                    <span class="TitleOragne"><strong> <%# Eval("CrsName") %></strong></span>

                </div>
                <div class="row">
                    <span>
                        <strong>کد درس :</strong>
                        <%# Eval("CrsCode") %></span>
                </div>
                <div class="row">
                    <span>
                        <strong>امتیاز درس :</strong>
                        <%# Eval("Point") %></span>
                </div>
                <div class="row">
                    <span>
                        <strong>مدت زمان اعتبار :</strong>
                        <%# Eval("ValidDuration") %></span>
                </div>
                <div class="row">
                    <span>
                        <strong>مدت زمان دوره :</strong>
                        <%# Eval("ValidDuration") %></span>
                </div>
                <div class="row" style="float: left">
                    <asp:LinkButton CssClass="continueLink" ID="btnViewCourse" runat="server" OnDataBinding="btnViewCourse_DataBinding"
                        ToolTip='<%# Bind("CrsId")%>' Text="مشاهده جزئیات"></asp:LinkButton>
                </div>
            </div>
        </ItemTemplate>
    </TSPControls:CustomAspxDevDataView>
    <asp:ObjectDataSource ID="OdbCourse" runat="server" TypeName="TSP.DataManager.CourseManager"
        SelectMethod="GetData" FilterExpression="InActive={0}">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
    </asp:ObjectDataSource>


</asp:Content>
