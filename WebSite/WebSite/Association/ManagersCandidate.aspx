<%@ Page Title="نامزدهای انتخابات" Language="C#" MasterPageFile="~/MasterPageWebsite.master"
    AutoEventWireup="true" CodeFile="ManagersCandidate.aspx.cs" Inherits="Association_ManagersCandidate" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript" src="../Script/Utility.js"></script>
    <script language="javascript" type="text/javascript">
        function contentPageLoad(sender, args) {
            location.href = "#Candidate";
        }
    </script>
    <div id="DivReport" runat="server" class="DivErrors" align="right">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]</div>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpe" runat="Server" TargetControlID="PanelInfo"
        ExpandControlID="PanelTitle" CollapseControlID="PanelTitle" Collapsed="true"
        CollapsedText="جهت مشاهده مشخصات تشکل کلیک نمایید" ExpandedText="جهت مخفی شدن مشخصات تشکل کلیک نمایید"
        ImageControlID="imgExpandedCollapsed" ExpandedImage="~/images/collapse_blue.png"
        CollapsedImage="~/images/expand_blue.png" SuppressPostBack="true">
    </ajaxToolkit:CollapsiblePanelExtender>
    <ajaxToolkit:RoundedCornersExtender ID="rce" runat="server" TargetControlID="PanelTitle"
        BorderColor="#7EACB1" Radius="6" Corners="Top" />
    <asp:Panel ID="PanelTitle" runat="server" CssClass="TableTitle" Style="padding: 5px"
        align="right" Direction="RightToLeft" BorderWidth="1px" BorderStyle="Solid">
        <table>
            <tr>
                <td>
                    <asp:Image ID="imgExpandedCollapsed" runat="server" ImageUrl="~/images/expand_blue.jpg" />
                </td>
                <td width="5px">
                </td>
                <td valign="middle">
                    <dxe:ASPxLabel ID="lblPeriodName" Font-Bold="true" runat="server" Text="">
                    </dxe:ASPxLabel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelInfo" runat="server">
        <TSPControls:CustomASPxRoundPanel ID="RoundPanelPeriod" HeaderText="مشخصات تشکل"
            runat="server" Width="100%" ShowHeader="false">
            <PanelCollection>
                <dx:PanelContent>
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td align="right" valign="top" width="15%">
                                    <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="نوع تشکل" Width="100%">
                                    </dx:ASPxLabel>
                                </td>
                                <td align="right" valign="top" width="35%">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtExGroupName"  Width="100%"
                                        Enabled="false" >
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td align="right" valign="top" width="15%">
                                </td>
                                <td align="left" valign="top" width="35%">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="15%">
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="تاریخ شروع تبلیغات" Width="100%">
                                    </dx:ASPxLabel>
                                </td>
                                <td align="right" dir="ltr" valign="top" width="35%">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtStartDatePropagation"  Width="100%"
                                        Enabled="false" RightToLeft="False" >
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td align="right" valign="top" width="15%">
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="تاریخ پایان تبلیغات" Width="100%">
                                    </dx:ASPxLabel>
                                </td>
                                <td align="left" dir="ltr" valign="top" width="35%">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtEndDatePropagation"  Width="100%"
                                        Enabled="false" RightToLeft="False" >
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" width="15%">
                                    <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="تاریخ شروع" Width="100%">
                                    </dx:ASPxLabel>
                                </td>
                                <td align="right" dir="ltr" valign="top" width="35%">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtStartDate"  Width="100%"
                                        Enabled="false" RightToLeft="False" >
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td align="right" valign="top" width="15%">
                                    <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="تاریخ پایان" Width="100%">
                                    </dx:ASPxLabel>
                                </td>
                                <td align="left" dir="ltr" valign="top" width="35%">
                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtEndDate"  Width="100%" Enabled="false"
                                        RightToLeft="False" >
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dx:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanel>
    </asp:Panel>
    <a name="Candidate"></a>
    
    <TSPControls:CustomAspxMenuHorizontal EnableViewState="true"  ID="MenueMajors" runat="server" OnItemClick="MenueMajors_ItemClick">
   
    </TSPControls:CustomAspxMenuHorizontal>
    <br />
    <TSPControls:CustomASPxRoundPanel ShowHeader="false" ID="RoundPanelHomePage" ClientInstanceName="RoundPanelHomePage"
        runat="server" Width="100%">
        <PanelCollection>
            <dx:PanelContent>
                <table width="100%">
                    <tr>
                        <td width="100%" align="center">
                            <dx:ASPxLabel runat="server" Font-Size="12px" Font-Bold="true" ID="lblDescription"
                                Text="جهت مشاهده کاندیدهای هر رشته بر روی رشته موردنظر کلیک نمایید">
                            </dx:ASPxLabel>
                            <br />
                            <br />
                            <dx:ASPxLabel runat="server" Font-Size="17px" Font-Bold="true" ID="lblHomePagePeriodName">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" align="center">
                            <br />
                            <dxe:ASPxImage ID="imgPeriodPic" runat="server" Width="255px" Height="160px">
                                <EmptyImage Height="160px" Width="255px" Url="~/Images/noImage.gif">
                                </EmptyImage>
                            </dxe:ASPxImage>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" align="center">
                        </td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <TSPControls:CustomAspxDevDataView ID="DataViewManagerCandidate" runat="server" ColumnCount="9"
        RowPerPage="25" Width="100%" 
         DataSourceID="ObjdsManagerCandidate" PagerSettings-EndlessPagingMode="OnClick">
      
        <ItemTemplate>
            <table width="80px" style="height: 80px" cellpadding="0" class="TableBorder">
                <tbody>
                    <tr>
                        <td style="padding: 0px 0px 0px 0px" align="center" rowspan="2" valign="middle">
                            <asp:Label ID="lblCancelStatus" runat="server" CssClass="VerticalText" Font-Bold="true"
                                ForeColor="Red" Text='<%# Eval("CancelStatus") %>'></asp:Label>
                        </td>
                        <td align="center" style="vertical-align: top; padding: 0px 0px 0px 0px">
                            <dxe:ASPxImage ID="ASPxImage1" runat="server" Height="50px" ImageUrl='<%# Bind("ImageUrl") %>'
                                Width="38px">
                                <EmptyImage Height="50px" Url="~/Images/person.png" Width="38px">
                                </EmptyImage>
                            </dxe:ASPxImage>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="vertical-align: top; height: 30px">
                           <%-- <asp:HyperLink ID="btnTitle" runat="server" CommandArgument='<%# Bind("CandidateId") %>' 
                                CssClass="LinkUnderlineAndBoldOnHover" Font-Size="XX-Small" ForeColor="RoyalBlue" PostBackUrl='<%#Bind("Attachment") %>'
                                Text='<%# Eval("MeFullName") %>' OnClick="btnTitle_Click"></asp:HyperLink>--%>
                               <asp:HyperLink ID="btnTitle" runat="server" 
                                CssClass="LinkUnderlineAndBoldOnHover" Font-Size="XX-Small" ForeColor="RoyalBlue" Target="_blank" NavigateUrl='<%#Bind("Attachment") %>'
                                Text='<%# Eval("MeFullName") %>' ></asp:HyperLink><%--OnClick="btnTitle_Click" --%>
                        </td>
                    </tr>
                </tbody>
            </table>
        </ItemTemplate>
        <PagerStyle ItemSpacing="10px" Spacing="10px"></PagerStyle>
    </TSPControls:CustomAspxDevDataView>
    <asp:ObjectDataSource ID="ObjdsManagerCandidate" runat="server" SelectMethod="FindByMajor"
        TypeName="TSP.DataManager.CandidateManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="MjId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="ExGroupPeriodId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="MjParentId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
</asp:Content>
