<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="WorkFlowReport.aspx.cs" Inherits="Members_Documents_WorkFlowReport"
    Title="پیگیری جریان کار" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                            href="#">بستن</a>]</div>
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                    width="100%">
                                    <tr>
                                        <td style="vertical-align: top; width: 100%; text-align: right">
                                            <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                <tr>
                                                    <td >
                                                           <asp:LinkButton ID="LinkButton1" CssClass="ButtonMenue" OnClick="btnBack_Click" runat="server">مدیریت پروانه اشتغال به کار</asp:LinkButton>
                                                    
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>
                    <br />
                    <TSPControls:CustomAspxDevGridView runat="server"  ID="GridViewWFReport"
                        DataSourceID="ObjdsWfReport" KeyFieldName="StateId" AutoGenerateColumns="False"
                         Width="100%">
                        <Settings ShowHorizontalScrollBar="true"></Settings>
                        <Columns>
                            <dxwgv:GridViewDataComboBoxColumn FieldName="WorkFlowId" Width="150px" Caption="نام فرایند"
                                VisibleIndex="0">
                                <PropertiesComboBox ValueType="System.String" TextField="WorkFlowName" ValueField="WorkFlowId"
                                    DataSourceID="ObjdsWorkFlow">
                                </PropertiesComboBox>
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dxwgv:GridViewDataComboBoxColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="WFTaskName" Width="150px"
                                Caption="مرحله">
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="DocName" Caption="پرونده مربوطه">
                                <CellStyle HorizontalAlign="Right" Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="DoerName" Caption="انجام دهنده">
                                <CellStyle HorizontalAlign="Right" Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="StateTypeName" Caption="نوع عملیات">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="Date" Width="50px" Caption="تاریخ">
                                <CellStyle HorizontalAlign="Right" Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="StateTime" Caption="ساعت">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn VisibleIndex="7" Caption=" " ShowClearFilterButton="true"> 
                            </dxwgv:GridViewCommandColumn>
                        </Columns>
                    </TSPControls:CustomAspxDevGridView>
                    <br />
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                    width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top; width: 100%; text-align: right">
                                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                    cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td >
                                                              <asp:LinkButton ID="LinkButton2" CssClass="ButtonMenue" OnClick="btnBack_Click" runat="server">مدیریت پروانه اشتغال به کار</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldWFState">
                                                </dxhf:ASPxHiddenField>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>
                    <asp:ObjectDataSource ID="ObjdsWfReport" runat="server" TypeName="TSP.DataManager.WorkFlowStateManager"
                        SelectMethod="SelectStateReports" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:Parameter Type="Int32" DefaultValue="-1" Name="TableId"></asp:Parameter>
                            <asp:Parameter Type="Int32" DefaultValue="-1" Name="TableType"></asp:Parameter>
                            <asp:Parameter Type="Int32" DefaultValue="-1" Name="NmcId"></asp:Parameter>
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsWorkFlow" runat="server" TypeName="TSP.DataManager.WorkFlowManager"
                        SelectMethod="GetData"></asp:ObjectDataSource>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
