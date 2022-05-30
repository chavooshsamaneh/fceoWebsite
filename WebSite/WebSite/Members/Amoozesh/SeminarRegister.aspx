<%@ Page Title="ثبت نام دوره های غیر مصوب و سمینارهای آموزشی" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="SeminarRegister.aspx.cs" Inherits="Members_Amoozesh_SeminarRegister" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
<%@ Register Src="../../UserControl/EPaymentUserControl.ascx" TagName="EPaymentUserControl"
    TagPrefix="TspUserControl" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
 <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>

          <TSPControls:CustomAspxDevDataView ID="DataViewSeminar" runat="server" ColumnCount="1"
                 Width="100%" DataSourceID="OdbSeminar"      RowPerPage="10">
    <PagerSettings ShowNumericButtons="false">
            <AllButton Visible="False" />
             <LastPageButton Visible="false"></LastPageButton>
             <FirstPageButton Visible="false"></FirstPageButton>
            <Summary Visible="false" />
            <PageSizeItemSettings Visible="false" ShowAllItem="false" Caption="" />
        </PagerSettings>
                <ItemTemplate>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                    </div>

                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <div class="col-md-3 col-sm-6  col-xs-12">
                            تاریخ شروع :
                        </div>
                        <div class="col-md-3 col-sm-6  col-xs-12">
                            <dxe:ASPxLabel ID="lblstatus" runat="server" RightToLeft="True" Font-Bold="true"
                                Text='<%# Bind("Startdate") %>'>
                            </dxe:ASPxLabel>
                        </div>
                        <div class="col-md-3 col-sm-6  col-xs-12">
                            تاریخ پایان :
                        </div>
                        <div class="col-md-3 col-sm-6  col-xs-12">
                            <dxe:ASPxLabel ID="Label1" runat="server" Font-Bold="true" RightToLeft="True" Text='<%# Bind("EndDate") %>'>
                            </dxe:ASPxLabel>
                        </div>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <div class="col-md-3 col-sm-6  col-xs-12">
                            ساعت برگزاری :
                        </div>
                        <div class="col-md-3 col-sm-6  col-xs-12">
                            <dxe:ASPxLabel ID="ASPxLabel6" runat="server" RightToLeft="True" Font-Bold="true"
                                Text='<%# Bind("Time") %>'>
                            </dxe:ASPxLabel>
                        </div>
                        <div class="col-md-3 col-sm-6  col-xs-12">
                            مدت زمان برگزاری(ساعت) :
                        </div>
                        <div class="col-md-3 col-sm-6  col-xs-12">
                            <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Font-Bold="true" RightToLeft="True"
                                Text='<%# Bind("Duration") %>'>
                            </dxe:ASPxLabel>
                        </div>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <div class="col-md-3 col-sm-6  col-xs-12">
                            نام مؤسسه :
                        </div>
                        <div class="col-md-3 col-sm-6  col-xs-12">
                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="true" RightToLeft="True"
                                Text='<%# Bind("InsName") %>'>
                            </dxe:ASPxLabel>
                        </div>
                        <div class="col-md-3 col-sm-6  col-xs-12">
                            محل برگزاری :
                        </div>
                        <div class="col-md-3 col-sm-6  col-xs-12">
                            <dxe:ASPxLabel ID="ASPxLabel4" runat="server" RightToLeft="True" Font-Bold="true"
                                Text='<%# Bind("Place") %>'>
                            </dxe:ASPxLabel>
                        </div>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <div class="col-md-3 col-sm-6  col-xs-12">
                            ظرفیت سمینار :
                        </div>
                        <div class="col-md-3 col-sm-6  col-xs-12">
                            <dxe:ASPxLabel ID="ASPxLabel51" runat="server" Font-Bold="true" RightToLeft="True"
                                Text='<%# Bind("Capacity") %>'>
                            </dxe:ASPxLabel>
                        </div>
                        <div class="col-md-3 col-sm-6  col-xs-12">
                            ظرفیت باقیمانده :
                        </div>
                        <div class="col-md-3 col-sm-6  col-xs-12">
                            <dxe:ASPxLabel ID="ASPxLabel8" runat="server" RightToLeft="True" Font-Bold="true"
                                Text='<%# Bind("RemainCapacity") %>'>
                            </dxe:ASPxLabel>
                        </div>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12">
                        <div class="col-md-3 col-sm-6  col-xs-12">
                            هزینه سمینار :
                        </div>
                        <div class="col-md-3 col-sm-6  col-xs-12">
                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="true" RightToLeft="True"
                                Text='<%# Bind("SeminarCost","{0:#,#}") %>'>
                            </dxe:ASPxLabel>
                        </div>
                        <div class="col-md-3 col-sm-6  col-xs-12">
                        </div>
                        <div class="col-md-3 col-sm-6  col-xs-12">
                        </div>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12">

                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:LinkButton ID="LinkButton1" CssClass="continueLink" OnClick="btnView_Click" runat="server" CommandArgument='<%# Eval("SeId")+";"+ Eval("InsId") %>'>مشاهده جزییات</asp:LinkButton>
                        </div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:LinkButton ID="btnRegister" CssClass="continueLink" OnClick="btnRegister_Click" runat="server" CommandArgument='<%# Bind("SeId") %>'>ثبت نام</asp:LinkButton>
                        </div>

                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12 footer-divider"></div>
                </ItemTemplate>
            </TSPControls:CustomAspxDevDataView>

            <asp:ObjectDataSource ID="OdbSeminar" runat="server" SelectMethod="SelectSeminarForRegister"
                TypeName="TSP.DataManager.SeminarManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
            <dx:ASPxHiddenField ID="HiddenFieldEpayment" runat="server">
            </dx:ASPxHiddenField>
        <%--    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>

        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
