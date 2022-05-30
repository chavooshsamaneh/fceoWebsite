<%@ Page Title="مدیریت فیش های پروژه ساختمانی" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ProjectAccounting.aspx.cs" Inherits="Owner_ProjectAccounting" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
        visible="true">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent1" runat="server">

                <table>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnPayment" CssClass="ButtonMenue" OnClick="btnPayment_Click" runat="server" OnClientClick="
if (GridViewProjectAccounting.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
   alert(&quot;ردیفی انتخاب نشده است&quot;);}                                               
 
   return confirm(HiddenPage.Get('MsgOwner'));">پرداخت الکترونیکی</asp:LinkButton>
                        </td>
                    </tr>
                </table>

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSP:ProjectInfo ID="prjInfo" runat="server"></TSP:ProjectInfo>
    <br />
    <TSPControls:CustomAspxDevGridView2 ClientInstanceName="GridViewProjectAccounting" Width="100%" ID="GridViewProjectAccounting"
        runat="server" DataSourceID="ObjectDataSourceTsAcc"
        AutoGenerateColumns="False" KeyFieldName="AccountingId">

        <SettingsCookies Enabled="false" />
        <Columns>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="AccountingId" Visible="false">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TypeName" Caption="نحوه پرداخت"
                Width="150px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="StatusName" Caption="وضعیت"
                Width="80px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FishPayerNameWithDetail"
                Caption="پرداخت کننده" Width="170px">
                <CellStyle Wrap="False" HorizontalAlign="Right">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="2" Width="230px" FieldName="AccTypeName"
                Caption="بابت">
                <CellStyle Wrap="False" HorizontalAlign="Right">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Number" Caption="شماره">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Date" Caption="تاریخ فیش"
                Width="90px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="PaymentDate" Caption="تاریخ پرداخت"
                Width="90px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Width="200px" VisibleIndex="4" FieldName="Amount" Caption="مبلغ">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="CreateDate" Caption="تاریخ ایجاد"
                Width="90px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
           
            <dxwgv:GridViewCommandColumn VisibleIndex="6" Caption=" " ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowHorizontalScrollBar="true" ShowFooter="true"></Settings>
        <TotalSummary>
            <dxwgv:ASPxSummaryItem FieldName="Amount" SummaryType="Sum" />
        </TotalSummary>
    </TSPControls:CustomAspxDevGridView2>
    <asp:ObjectDataSource ID="ObjectDataSourceTsAcc" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="SelectAccountingForProject" TypeName="TSP.DataManager.TechnicalServices.AccountingManager">
        <SelectParameters>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="ProjectId"></asp:Parameter>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="TableTypeId"></asp:Parameter>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="ProjectIngridientTypeId"></asp:Parameter>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="AccType"></asp:Parameter>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="Status"></asp:Parameter>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="FishPayerId"></asp:Parameter>
            <asp:Parameter Type="Int32" DefaultValue="4" Name="Type"></asp:Parameter>
            <asp:Parameter Type="String" DefaultValue="1" Name="FromDate"></asp:Parameter>
            <asp:Parameter Type="String" DefaultValue="2" Name="ToDate"></asp:Parameter>
            <asp:Parameter Type="String" DefaultValue="" Name="AccTypeList"></asp:Parameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server" Width="100%">
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent2" runat="server">

                <table>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnPayment2" CssClass="ButtonMenue" OnClick="btnPayment_Click" runat="server" OnClientClick="
if (GridViewProjectAccounting.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
   alert(&quot;ردیفی انتخاب نشده است&quot;);}                                               
 
   return confirm(HiddenPage.Get('MsgOwner'));">پرداخت الکترونیکی</asp:LinkButton>

                        </td>
                    </tr>
                </table>

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <dx:ASPxHiddenField ID="HiddenPage" runat="server" ClientInstanceName="HiddenPage">
    </dx:ASPxHiddenField>
</asp:Content>

