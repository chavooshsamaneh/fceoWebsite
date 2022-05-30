<%@ Page Title="جستجو" Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript" language="javascript">
        function SearchKeyPress(e, Type) {
            if (Type == 1)//DevExpress controls
            {
                if (e.htmlEvent.keyCode == 13) {
                    btnSearch.DoClick();
                }
            }
            else if (Type == 2)//asp controls
            {
                if (e.keyCode == 13)
                    btnSearch.DoClick();
            }
        }
    </script>
    <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <div id="SearchSection">
        <br />
        <br />
        <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>

                    <table runat="server" id="tbl" style="width: 100%" dir="rtl">
                        <tr id="Tr1" runat="server">
                            <td id="Td1" runat="server" style="vertical-align: top; width: 15%" align="right">
                                <asp:Label runat="server" wrap='false' Text="عنوان" ID="Label5"></asp:Label>
                            </td>
                            <td id="Td10" runat="server" colspan="3" style="vertical-align: top; width: 75%" align="right">
                                <TSPControls:CustomTextBox ID="txtSearch" ClientInstanceName="txtSearch" runat="server">
                                    <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                    <ValidationSettings>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr id="Tr6" runat="server">
                            <td colspan="4" style="vertical-align: top;" align="center">

                                <TSPControls:CustomAspxButton ID="btnSearch" runat="server"
                                    ClientInstanceName="btnSearch" OnClick="btnSearch_Click" Text="جستجو" UseSubmitBehavior="False">
                                    <%--    <ClientSideEvents Click="function(s, e) {
	    e.processOnServer=false;
        if(CheckSearch()==0)
        {
          alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.'); 
          return; 
        }
        else
         e.processOnServer=true;
}" />--%>
                                </TSPControls:CustomAspxButton>

                            </td>
                        </tr>
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanel>
        <br />
    </div>
    <div class="row">
        <a runat="server" id="linkNewsSearch" href="#" class="SearchNews">خبرها</a>
        <a runat="server" id="linkPeriodsSearch" href="#" class="SearchPeriods">دوره های آموزشی</a>
        <a runat="server" id="linkRulesSearch" href="#" class="SearchRules">قوانین و بخشنامه ها</a>
        <a runat="server" id="linkFormsSearch" href="#" class="SearchForms">فرم ها</a>
        <a runat="server" id="linkTenderSearch" href="#" class="SearchTender">مناقصات</a>
        <br />
    </div>
    <div id="NewsSearchSection">
        <strong style="float: right">خبرها</strong>
        <TSPControls:CustomAspxDevDataView ID="DataViewNews" runat="server" Width="100%"
            RowPerPage="100" ColumnCount="1"
            DataSourceID="ObjdsNews" ItemSpacing="5px" RightToLeft="True" PagerStyle-ItemSpacing="10px" PagerSettings-EndlessPagingMode="OnScroll">
            <ItemStyle Height="1px" Width="1px" Paddings-Padding="1px" HorizontalAlign="Center">
                <Paddings Padding="0px"></Paddings>
            </ItemStyle>
            <ItemTemplate>
                <div class="row DataViewOneColumn NewsSearchSectionBorder">
                    <div class="row" style="text-align: right">
                        <span class="TitleOragne">
                            <h5><strong><%# Eval("Title") %></strong></h5>
                        </span>
                    </div>
                    <div class="row" align="right">
                        <div class="col-md-2 col-sm-2 col-xs-5">
                            <div class="row" style="text-align: center;">
                                <span style="font-size: 10px"><%# Eval("Name")+" "+Eval("ImpName") %></span>
                            </div>
                            <div class="row" align="center">
                                <img id="Image2" runat="server" ondatabinding="Image2_DataBinding" style="width: 100%; height: 100%; max-width: 150px; max-height: 150px; margin-bottom: 15px;"
                                    src='<%# Eval("url").ToString().Replace("~/","") %>' />
                            </div>
                        </div>
                        <div class="col-md-10 col-sm-10 col-xs-7">
                            <div class="row" style="text-align: left">

                                <span><strong><%# Eval("Date") %></strong>
                                </span><span><strong><%# Eval("StrTime").ToString().Substring(0,5) %></strong>
                                </span>

                            </div>
                            <div class="col-md-12" style="text-align: right">
                                <span>نمایندگی:<strong><%# Eval("AgentName") %></strong>
                                </span>
                            </div>
                            <div class="col-md-12" style="text-align: right">
                                <span><strong><%# Eval("Summary") %></strong>
                                </span>
                            </div>
                            <div class="col-md-12">
                                <asp:LinkButton class="continueLink" ID="LinkButtonNewsDetail" OnClick="LinkButtonNewsDetail_Click" runat="server" CommandArgument='<%# Bind("NewsId") %>'>مشروح خبر</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </TSPControls:CustomAspxDevDataView>
        <asp:ObjectDataSource ID="ObjdsNews" runat="server" SelectMethod="SearchNews"
            TypeName="TSP.DataManager.NewsManager" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="SubjectId"
                    Type="Int16" />
                <asp:Parameter DefaultValue="%" Name="Title"
                    Type="String" />
                <asp:Parameter DefaultValue="%" Name="Body"
                    Type="String" />
                <asp:Parameter DefaultValue="1" Name="FromDate"
                    Type="String" />
                <asp:Parameter DefaultValue="2" Name="ToDate"
                    Type="String" />
                <asp:Parameter DefaultValue="-1" Name="Importance"
                    Type="Int16" />
                <asp:Parameter DefaultValue="-1" Name="AgentId"
                    Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="ExGroupId"
                    Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="IsNotification"
                    Type="Int32" />
                <asp:Parameter DefaultValue="0" Name="InActive"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

    </div>
    <br />
    <div id="PeriodSearchSection">
        <strong>دوره های آموزشی</strong>
        <TSPControls:CustomAspxDevDataView ID="DataViewPeriods" runat="server" ColumnCount="1" PagerSettings-EndlessPagingMode="OnScroll"
            RowPerPage="10" Width="100%" DataSourceID="OdbPeriod">

            <ItemTemplate>
                <table class="DataViewOneColumn PeriodSearchSectionBorder" width="100%">
                    <tr>
                        <td align="right">

                            <span class="TitleOragne"><strong><%# Eval("PeriodTitle") %> </strong></span>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="right">
                            <table width="100%">
                                <tr>
                                    <td align="right" valign="middle" style="width: 80%">
                                        <table table width="100%" cellpadding="3" cellspacing="3">
                                            <tbody>
                                                <tr>
                                                    <td align="right" valign="middle">نام مؤسسه :
                                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="true" RightToLeft="True"
                                                            Text='<%# Bind("InsName") %>'>
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="middle" style="width: 50%">تاریخ شروع :
                                                        <dxe:ASPxLabel ID="lblstatus" runat="server" RightToLeft="True" Font-Bold="true"
                                                            Text='<%# Bind("StartDate") %>'>
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td align="right" valign="middle" style="width: 50%">تاریخ پایان :
                                                        <dxe:ASPxLabel ID="Label1" runat="server" Font-Bold="true" RightToLeft="True" Text='<%# Bind("EndDate") %>'>
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="middle">طول دوره(ساعت) :
                                                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Font-Bold="true" RightToLeft="True"
                                                            Text='<%# Bind("Duration") %>'>
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td align="right" valign="middle">امتیاز :
                                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" RightToLeft="True" Font-Bold="true"
                                                            Text='<%# Bind("Point") %>'>
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="middle">محل برگزاری :
                                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" RightToLeft="True" Font-Bold="true"
                                                            Text='<%# Bind("PPId") %>'>
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td valign="bottom" style="width: 100%">
                                        <asp:LinkButton ID="btnViewPeriods" OnClick="btnViewPeriods_Click" CssClass="continueLink" runat="server" CommandArgument='<%# Eval("PPId")+";"+ Eval("InsId") +";"+ Eval("PType") %>'>مشاهده جزییات</asp:LinkButton>
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
        <asp:ObjectDataSource ID="OdbPeriod" runat="server" SelectMethod="SearchPeriod"
            TypeName="TSP.DataManager.PeriodPresentManager" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter DefaultValue="%" Name="PeriodTitle" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>

    </div>
    <br />
    <div id="RulesSearchSection">
        <strong>قوانین و بخشنامه ها</strong>
        <TSPControls:CustomAspxDevDataView ID="DataViewRules" runat="server" ColumnCount="1"
            RowPerPage="10" Width="100%" RightToLeft="True" DataSourceID="ObjdsRules" PagerSettings-EndlessPagingMode="OnClick">

            <ItemTemplate>
                <table class="DataViewOneColumn RulesSearchSectionBorder" dir="rtl" width="100%">
                    <tbody>
                        <tr>
                            <td align="right" class="TableTitle" colspan="4" valign="middle">
                                <dxe:ASPxLabel ID="lblName" Font-Bold="true" runat="server" Text='<%# Bind("RuName") %>'>
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="4" valign="top">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td align="right" style="width: 15%" valign="top">
                                                <dxe:ASPxLabel ID="ASPxLabel1" Font-Bold="true" runat="server" Text="کد بخشنامه :">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" colspan="3" valign="top" style="width: 85%">
                                                <dxe:ASPxLabel ID="lblCode" runat="server" Text='<%# Bind("RuId") %>'>
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="نوع بخشنامه :" Font-Bold="true">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" colspan="3" valign="top">
                                                <dxe:ASPxLabel ID="lblGroup" runat="server" Text='<%# Bind("RuleTypeName") %>'>
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="توضیحات :" Font-Bold="true">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" style="text-align: justify" colspan="4" valign="top">
                                                <dxe:ASPxLabel ID="lblDesc" runat="server" Text='<%# Bind("Description") %>'>
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td valign="bottom">
                                                <dxe:ASPxHyperLink CssClass="continueLink" ID="HpLink" runat="server" NavigateUrl='<%# Bind("PdfUrl") %>'
                                                    Target="_blank" Text="دانلود">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ItemTemplate>
        </TSPControls:CustomAspxDevDataView>
        <asp:ObjectDataSource runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="Search" TypeName="TSP.DataManager.RulesManager" ID="ObjdsRules">
            <SelectParameters>
                <asp:Parameter DefaultValue="%" Name="RuName" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>

    </div>
    <br />
    <div id="FormSearchSection">
        <strong>فرم ها</strong>
        <TSPControls:CustomAspxDevDataView ID="DataViewForms" runat="server" ColumnCount="1"
            DataSourceID="ObjdsForms"
            PagerSettings-EndlessPagingMode="OnClick">

            <ItemTemplate>
                <table class="DataViewOneColumn FormSearchSectionBorder" dir="rtl" width="100%">
                    <tbody>
                        <tr>
                            <td align="right" class="TableTitle" colspan="4" valign="middle">
                                <dxe:ASPxLabel ID="lblName" Font-Bold="true" runat="server" Text='<%# Bind("FoName") %>'>
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="4" valign="top">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td align="right" style="width: 15%" valign="top">
                                                <dxe:ASPxLabel ID="ASPxLabel1" Font-Bold="true" runat="server" Text="کد فرم :">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" colspan="3" valign="top" style="width: 85%">
                                                <dxe:ASPxLabel ID="lblCode" runat="server" Text='<%# Bind("FoCode") %>'>
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="نوع فرم :" Font-Bold="true">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" colspan="3" valign="top">
                                                <dxe:ASPxLabel ID="lblGroup" runat="server" Text='<%# Bind("FormTypeName") %>'>
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="توضیحات :" Font-Bold="true">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" style="text-align: justify" colspan="4" valign="top">
                                                <dxe:ASPxLabel ID="lblDesc" runat="server" Text='<%# Bind("Description") %>'>
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <dxe:ASPxHyperLink CssClass="continueLink" ID="HpLink" runat="server" NavigateUrl='<%# Bind("PdfUrl") %>'
                                                    Target="_blank" Text="دانلود">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ItemTemplate>
        </TSPControls:CustomAspxDevDataView>
        <asp:ObjectDataSource runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="Search" TypeName="TSP.DataManager.FormsManager" ID="ObjdsForms">
            <SelectParameters>
                <asp:Parameter DefaultValue="%" Name="FoName" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>

    </div>
    <br />
    <div id="TenderSearchSection">
        <strong>مناقصات</strong>
        <TSPControls:CustomAspxDevDataView runat="server"
            AlwaysShowPager="True" PagerPanelSpacing="0px" RightToLeft="True"
            ID="DataViewTender" DataSourceID="ObjectDataSourceTender" EnableViewState="False"
            ColumnCount="1" Width="100%" PagerSettings-EndlessPagingMode="OnClick">
            <ItemTemplate>
                <table dir="rtl" width="100%" class="DataViewOneColumn TenderSearchSectionBorder">
                    <tbody>
                        <tr>
                            <td class="TableTitle" valign="middle" align="right" colspan="4">
                                <dxe:ASPxLabel ID="ASPxLabel3" Font-Bold="True" runat="server" Width="100px" Text="مناقصه شماره :">
                                </dxe:ASPxLabel>
                                <dxe:ASPxLabel ID="ASPxLabel7" Font-Bold="True" runat="server" Text='<%# Bind("TeId") %>'>
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" colspan="4">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 55px" valign="top" align="right">
                                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="نام ">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" colspan="3">
                                                <dxe:ASPxLabel ID="lblName" runat="server" Text='<%# Bind("TeName") %>'>
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="left" rowspan="2">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <a id="link1" href='<%# Bind("PdfUrl") %>' target="_blank" runat="server">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Download.png"></asp:Image>
                                                                </a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxHyperLink ID="HpLink" CssClass="continueLink" runat="server" Text="دانلود" NavigateUrl='<%# Bind("PdfUrl") %>'
                                                                    Target="_blank">
                                                                </dxe:ASPxHyperLink>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Width="55px" Text="تاریخ">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" dir="ltr" align="right" colspan="3">
                                                <dxe:ASPxLabel ID="lblDesc" runat="server" RightToLeft="False" Text='<%# Bind("Date") %>'>
                                                </dxe:ASPxLabel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ItemTemplate>
        </TSPControls:CustomAspxDevDataView>
        <asp:ObjectDataSource runat="server" SelectMethod="Search" ID="ObjectDataSourceTender"
            TypeName="TSP.DataManager.TenderManager" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter DefaultValue="%" Name="TeName" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>

