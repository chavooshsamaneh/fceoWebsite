<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPageWebsite.master"
    CodeFile="News.aspx.cs" Inherits="News" Title="آرشیو اخبار" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
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
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <div align="right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>

    <TSPControls:CustomAspxMenuHorizontal ID="MenuAgent" runat="server"
        OnItemClick="MenuAgent_ItemClick"
        Visible="false">

        <Items>
            <dxm:MenuItem Name="Agent" Text="مشخصات نمایندگی">
            </dxm:MenuItem>
            <dxm:MenuItem Selected="true" Name="News" Text="اخبار نمایندگی">
            </dxm:MenuItem>
        </Items>
    </TSPControls:CustomAspxMenuHorizontal>
    <TSPControls:CustomAspxMenuHorizontal ID="MenuExpGroup" runat="server"
        OnItemClick="MenuExpGroup_ItemClick"
        Visible="false">

        <Items>
            <dxm:MenuItem Name="News" Enabled="false" Selected="true" Text="اخبار گروه تخصصی">
            </dxm:MenuItem>
            <dxm:MenuItem Name="ExpInfo" Text="مشخصات گروه تخصصی">
            </dxm:MenuItem>
        </Items>
    </TSPControls:CustomAspxMenuHorizontal>
    <ul class="HelpUL">
        <li>جهت نمایش لیست اخبار حداقل یکی از فیلد های جستجو را تکمیل نموده و سپس بر روی دکمه جستجو کلیک نمایید
               
        </li>
    </ul>

    <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table runat="server" id="tbl" style="width: 100%" dir="rtl">
                    <tr id="Tr1" runat="server">
                        <td id="Td1" runat="server" style="vertical-align: top; width: 15%" align="right">
                            <asp:Label runat="server" wrap='false' Text="متن خبر" ID="Label5"></asp:Label>
                        </td>
                        <td id="Td10" runat="server" colspan="3" style="vertical-align: top; width: 75%" align="right">
                            <TSPControls:CustomTextBox ID="txtSearchBody" ClientInstanceName="txtSearchBody" runat="server">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                <ValidationSettings>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr id="Tr2" runat="server">
                        <td id="Td2" runat="server" style="vertical-align: top;" align="right">
                            <asp:Label runat="server" Text="عنوان" ID="Label3"></asp:Label>
                        </td>
                        <td id="Td3" runat="server" style="vertical-align: top;" align="right" colspan="3">
                            <TSPControls:CustomTextBox ID="txtSearchTitle" ClientInstanceName="txtSearchTitle" runat="server">
                                <ClientSideEvents KeyPress="function(s,e){SearchKeyPress(e,1);}" />
                                <ValidationSettings>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr id="Tr4" runat="server">
                        <td id="Td11" runat="server" style="vertical-align: top; width: 15%;" align="right">
                            <asp:Label runat="server" Text="از تاریخ" Width="100%" ID="Label6"></asp:Label>
                        </td>
                        <td id="Td12" runat="server" colspan="1" style="vertical-align: top; width: 35%" align="right">
                            <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                Width="230px" ShowPickerOnTop="True" ID="txtSearchFromDate" PickerDirection="ToRight"
                                IconUrl="~/Images/Calendar.gif" Style="direction: ltr" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                        <td style="vertical-align: top; width: 15%;" align="right">
                            <asp:Label runat="server" Text=" تا تاریخ" Width="100%" ID="Label11"></asp:Label>
                        </td>
                        <td style="vertical-align: top; width: 35%">
                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="230px" ShowPickerOnTop="True"
                                ShowPickerOnEvent="OnClick" ID="txtSearchToDate" PickerDirection="ToRight" IconUrl="~/Images/Calendar.gif" onkeypress="SearchKeyPress(event,2);"></pdc:PersianDateTextBox>
                        </td>
                    </tr>
                    <tr id="Tr3" runat="server">
                        <td id="Td6" runat="server" style="vertical-align: top;" align="right">
                            <asp:Label runat="server" Text="موضوع " Width="100%"></asp:Label>
                        </td>
                        <td id="Td7" runat="server" style="vertical-align: top;" align="right">
                            <TSPControls:CustomAspxComboBox ID="cmbSub" ClientInstanceName="cmbSub" runat="server"
                                DataSourceID="ObjectDataSourceSub"
                                EnableIncrementalFiltering="True" RightToLeft="True" TextField="Name" ValueField="SubId"
                                ValueType="System.String" Width="100%">
                                <ItemStyle HorizontalAlign="Right" />
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td id="Td4" runat="server" style="vertical-align: top;" align="right">
                            <asp:Label runat="server" Text="درجه اهمیت" Width="100%" ID="Label1"></asp:Label>
                        </td>
                        <td id="Td5" runat="server" style="vertical-align: top;" align="right">
                            <TSPControls:CustomAspxComboBox ID="cmbImp" ClientInstanceName="cmbImp" runat="server"
                                EnableIncrementalFiltering="True" RightToLeft="True"
                                SelectedIndex="0" ValueType="System.String" Width="100%">
                                <ItemStyle HorizontalAlign="Right" />
                                <Items>
                                    <dxe:ListEditItem Text="------------" Value="-1" Selected="True" />
                                    <dxe:ListEditItem Text="معمولی" Value="0" />
                                    <%--   <dxe:ListEditItem Text="مهم" Value="1" />--%>
                                    <dxe:ListEditItem Text="بسیار مهم" Value="2" />
                                </Items>
                                <ValidationSettings>

                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px" />
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;" align="right">
                            <asp:Label runat="server" Text="نمایندگی" Width="100%" ID="Label8"></asp:Label>
                        </td>
                        <td style="vertical-align: top;" align="right">
                            <TSPControls:CustomAspxComboBox ID="cmbAgent" ClientInstanceName="cmbAgent" runat="server"
                                DataSourceID="ObjectDataSourceAgent"
                                EnableIncrementalFiltering="True" TextField="Name" ValueField="AgentId" ValueType="System.String"
                                Width="100%" RightToLeft="True">
                                <ItemStyle HorizontalAlign="Right" />
                            </TSPControls:CustomAspxComboBox>
                            <asp:ObjectDataSource ID="ObjectDataSourceAgent" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetData" TypeName="TSP.DataManager.AccountingAgentManager"></asp:ObjectDataSource>
                        </td>
                        <td style="vertical-align: top;" align="right">گروه تخصصی</td>
                        <td>
                            <TSPControls:CustomAspxComboBox ID="cmbExGroup" ClientInstanceName="cmbExGroup" runat="server"
                                DataSourceID="ObjectDataExGroup"
                                EnableIncrementalFiltering="True" TextField="ExGroupName" ValueField="ExGroupId" ValueType="System.String"
                                Width="100%" RightToLeft="True">
                                <ItemStyle HorizontalAlign="Right" />
                            </TSPControls:CustomAspxComboBox>
                            <asp:ObjectDataSource ID="ObjectDataExGroup" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="SelectExGroupForNews" TypeName="TSP.DataManager.ExGroupManager"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3">
                            <TSPControls:CustomASPxCheckBox runat="server" ID="CheckBoxIsNotification" Text="خبر از نوع اطلاعیه می باشد."></TSPControls:CustomASPxCheckBox>
                        </td>
                    </tr>
                    <tr id="Tr6" runat="server">
                        <td colspan="4" style="vertical-align: top;" align="center">

                            <TSPControls:CustomAspxButton ID="btnSearch" runat="server"
                                ClientInstanceName="btnSearch" OnClick="btnSearch_Click" Text="جستجو" UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
	    e.processOnServer=false;
        if(CheckSearch()==0)
        {
          alert('پر کردن حداقل یکی از فیلد های جستجو اجباری می باشد.'); 
          return; 
        }
        else
         e.processOnServer=true;
}" />
                            </TSPControls:CustomAspxButton>

                        </td>
                    </tr>
                </table>


            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>

    <br />
    <TSPControls:CustomAspxDevDataView ID="DataViewNews" runat="server" Width="100%"
        RowPerPage="10" ColumnCount="1"
        DataSourceID="ObjdsNews" ItemSpacing="5px" RightToLeft="True" PagerStyle-ItemSpacing="10px" >
         <PagerSettings ShowNumericButtons="false">
            <AllButton Visible="False" />
             <LastPageButton Visible="false"></LastPageButton>
             <FirstPageButton Visible="false"></FirstPageButton>
            <Summary Visible="false" />
            <PageSizeItemSettings Visible="false" ShowAllItem="false" Caption="" />
        </PagerSettings>

        <ItemStyle Height="1px" Width="1px" Paddings-Padding="1px" HorizontalAlign="Center">
            <Paddings Padding="0px"></Paddings>
        </ItemStyle>
        <ItemTemplate>

            <div class="row DataViewOneColumn">
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
                            <img id="Image2" runat="server" ondatabinding="Image2_DataBinding" style="width: 100%; height: 100%; max-width: 150px; max-height: 150px"
                                src='<%# Eval("url").ToString().Replace("~/","") %>' />
                        </div>
                        <div class="row" align="center">
                            <TSPControls:CustomASPXRatingControl ID="Rating1" runat="server" Value="0" FillPrecision="Full"
                                ReadOnly="true" ToolTip='<%# Bind("SumRate") %>' OnDataBinding="Rating1_DataBinding">
                            </TSPControls:CustomASPXRatingControl>
                        </div>
                        <div class="row" style="text-align: center">
                            <span>تعداد بازدید کنندگان:<strong><%# Eval("CountOfRead") %></strong>
                            </span>
                        </div>

                    </div>
                    <div class="col-md-10 col-sm-10 col-xs-7">
                        <div class="row" style="text-align: left">
                            
                            <span>تاریخ خبر<strong>:<%# Eval("Date") %></strong>
                             <span>تاریخ ثبت<strong>:<%# Eval("CreateDate") %></strong>
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
                            <asp:LinkButton class="continueLink" ID="LinkButton1" OnClick="LinkButton1_Click" runat="server" CommandArgument='<%# Bind("NewsId") %>'>مشروح خبر</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

        </ItemTemplate>

    </TSPControls:CustomAspxDevDataView>

    <dx:ASPxHiddenField ID="HdAgent" runat="server" ClientInstanceName="HdAgent">
    </dx:ASPxHiddenField>

    <asp:ObjectDataSource ID="ObjectDataSourceSub" runat="server" DeleteMethod="Delete"
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
        TypeName="TSP.DataManager.NewsSubjectManager"></asp:ObjectDataSource>

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
</asp:Content>
