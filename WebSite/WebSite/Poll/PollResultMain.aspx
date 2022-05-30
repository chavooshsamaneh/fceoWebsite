<%@ Page Title="نتایج نظر سنجی" Language="C#" MasterPageFile="~/MasterPageWebsite.master"
    AutoEventWireup="true" CodeFile="PollResultMain.aspx.cs" Inherits="Poll_PollResultMain" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.1.Web, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.1.Web, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]</div>
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelPoll" HeaderText="نتایج نظرسنجی"
                runat="server" Width="100%"  Font-Size="X-Small" HeaderStyle-Wrap="True">
                <ContentPaddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
                <PanelCollection>
                    <dxp:PanelContent>
                        <div style="width: 100%" align="center">
                            <dxe:ASPxLabel runat="server" ID="lblTotalAnswer" Text="تعداد کل پاسخ ها:---" Font-Bold="true"
                                ForeColor="DarkViolet">
                            </dxe:ASPxLabel>
                            <br />
                            <TSPControls:CustomAspxDevDataView ID="DataViewQuestion" ClientInstanceName="DataViewPoll"
                                runat="server" ColumnCount="1" RowPerPage="25" Width="100%" 
                         
                                RightToLeft="True" ItemSpacing="0px" PagerStyle-ItemSpacing="0px" Border-BorderStyle="None">
                                <ItemStyle Height="1px" Width="1px" Paddings-Padding="0px" />
                                <ItemTemplate>
                                    <table class="TableBorder" width="100%">
                                        <tbody>
                                            <tr>
                                                <td class="TableTitle" align="right" colspan="2">
                                                    <dx:ASPxLabel ID="lblQuestion" runat="server" Text='<%# Bind("Question") %>' Font-Bold="True"
                                                        Width="100%">
                                                    </dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dx:ASPxLabel ID="lblQustion" runat="server" Width="100%" Text='<%# Bind("QuestionId") %>'
                                                        Wrap="True" Visible="false">
                                                    </dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top">
                                                    <TSPControls:CustomAspxDevDataView ID="DataViewChoise" ClientInstanceName="DataViewChoise"
                                                        DataSourceID="objdsReport" runat="server" ColumnCount="1" RowPerPage="25" Width="100%"
                                                          EmptyDataText="هیچ داده ای وجود ندارد"
                                                        RightToLeft="True" ItemSpacing="0px" PagerStyle-ItemSpacing="0px"
                                                        Border-BorderStyle="None">
                                                        <ItemStyle Height="1px" Width="1px" Paddings-Padding="0px" />
                                                        <ItemTemplate>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td colspan="2" align="right">
                                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Width="100%" Text='<%# Eval("ChoiseName")+" - تعدادپاسخ:"+Eval("CountAnswer") %>'
                                                                            Wrap="false" Font-Bold="true" ForeColor="DarkViolet">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="50%" align="right">
                                                                        <dxe:ASPxProgressBar  
                                                                            RightToLeft="True" ID="ASPxProgressBar1" runat="server" Height="11px" Position="50"
                                                                            Value='<%# Bind("ChoicePercent") %>' Width="50%">
                                                                        </dxe:ASPxProgressBar>
                                                                    </td>
                                                                    <td align="right" width="50%">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <PagerSettings Visible="false">
                                                        </PagerSettings>
                                                    </TSPControls:CustomAspxDevDataView>
                                                    <asp:ObjectDataSource ID="objdsReport" runat="server" SelectMethod="SelectAnswerReport"
                                                        TypeName="TSP.DataManager.PollAnswerManager" OldValuesParameterFormatString="original_{0}">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="lblQustion" DefaultValue="-1" Name="QuestionId"
                                                                PropertyName="Text" Type="Int32" />
                                                            <asp:Parameter Name="PollId" Type="Int32" DefaultValue="-1" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </ItemTemplate>
                               
                            </TSPControls:CustomAspxDevDataView>
                        </div>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <dx:ASPxHiddenField ID="HiddenFieldPage" runat="server">
            </dx:ASPxHiddenField>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                        <img src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
