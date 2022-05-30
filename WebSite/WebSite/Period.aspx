<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPageWebsite.master"
    CodeFile="Period.aspx.cs" Inherits="Period" Title="دوره های آموزشی" %>

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
    <div id="Content" runat="server" style="width: 100%" align="center">
        <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
            [<a class="closeLink" href="#">بستن</a>]</div>
      
        <TSPControls:CustomAspxDevDataView ID="DataViewPeriods" runat="server" ColumnCount="1" 
            RowPerPage="10" Width="100%" DataSourceID="OdbPeriod" 
            >
            <PagerSettings ShowNumericButtons="false">
            <AllButton Visible="False" />
             <LastPageButton Visible="false"></LastPageButton>
             <FirstPageButton Visible="false"></FirstPageButton>
            <Summary Visible="false" />
            <PageSizeItemSettings Visible="false" ShowAllItem="false" Caption="" />
        </PagerSettings>
            <ItemTemplate>
                <table  class="DataViewOneColumn" width="100%">
                    <tr>
                        <td align="right">
                       
                            <span  class="TitleOragne"><strong> <%# Eval("PeriodTitle") %> </strong></span>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="right">
                            <table width="100%"  >
                                <tr>
                                    <td align="right" valign="middle" style="width: 80%">
                                        <table table width="100%" cellpadding="3" cellspacing="3">
                                            <tbody>
                                                <tr>
                                                    <td align="right" valign="middle">
                                                        نام مؤسسه :
                                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="true" RightToLeft="True"
                                                            Text='<%# Bind("InsName") %>'>
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="middle" style="width: 50%">
                                                        تاریخ شروع :
                                                        <dxe:ASPxLabel ID="lblstatus" runat="server" RightToLeft="True" Font-Bold="true"
                                                            Text='<%# Bind("StartDate") %>'>
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td align="right" valign="middle" style="width: 50%">
                                                        تاریخ پایان :
                                                        <dxe:ASPxLabel ID="Label1" runat="server" Font-Bold="true" RightToLeft="True" Text='<%# Bind("EndDate") %>'>
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="middle">
                                                        طول دوره(ساعت) :
                                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Font-Bold="true" RightToLeft="True"
                                                            Text='<%# Bind("Duration") %>'>
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td align="right" valign="middle">
                                                        امتیاز :
                                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" RightToLeft="True" Font-Bold="true"
                                                            Text='<%# Bind("Point") %>'>
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="middle">
                                                        محل برگزاری :
                                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" RightToLeft="True" Font-Bold="true"
                                                            Text='<%# Bind("Place") %>'>
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td   valign="bottom" style="width: 100%">
                                        <asp:LinkButton ID="btnView" OnClick="btnView_Click" CssClass="continueLink" runat="server" CommandArgument='<%# Eval("PPId")+";"+ Eval("InsId") +";"+ Eval("PType") %>'>مشاهده جزییات</asp:LinkButton>
                                    </td>
                                   <%-- <td align="center" valign="bottom" style="width: 10%">
                                        <asp:LinkButton ID="btnRegister" OnClick="btnRegister_Click" runat="server" CommandArgument='<%# Bind("PPId") %>'>ثبت نام</asp:LinkButton>
                                    </td>--%>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </TSPControls:CustomAspxDevDataView>
        <asp:ObjectDataSource ID="OdbPeriod" runat="server" SelectMethod="FindMembersByDate"
            TypeName="TSP.DataManager.PeriodPresentManager" OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
            DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
    </div>
</asp:Content>
