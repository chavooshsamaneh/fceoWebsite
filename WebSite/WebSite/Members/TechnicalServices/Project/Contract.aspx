<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Contract.aspx.cs" Inherits="Members_TechnicalServices_Project_Contract"
    Title="مدیریت قرارداد" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ثبت قرارداد جدید" ToolTip="ثبت قرارداد جدید"
                                    ID="btnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnNew_Click">
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مشاهده" ToolTip="مشاهده"
                                    ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnView_Click">
                                    <ClientSideEvents Click="function(s, e) {
	 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                </TSPControls:CustomAspxButton>


                            </td>
                            <td>
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مدیریت پروژه ها" ToolTip="مدیریت پروژه ها"
                                    ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnBack_Click">
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <div dir="ltr">
        <TSP:ProjectInfo ID="prjInfo" runat="server"></TSP:ProjectInfo>
    </div>
    <br />
    <TSPControls:CustomAspxDevGridView ID="GridViewContract" Width="100%" runat="server"
        DataSourceID="ObjdContract"
        AutoGenerateColumns="False" KeyFieldName="ContractId" ClientInstanceName="grid">
        <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
        <Columns>
            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Title" Caption="نوع قرارداد">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Duration" Caption="مدت زمان(ماه)">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Amount" Caption="مبلغ قرارداد">
                <PropertiesTextEdit DisplayFormatString="#,#">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="ContractDate" Caption="تاریخ انعقاد">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="CreateDate" Caption="تاریخ ایجاد">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="مشاهده"
                FieldName="FileUrl" Caption="فایل قرارداد" Name="PlanFilePath" PropertiesHyperLinkEdit-Target="_blank">
            </dxwgv:GridViewDataHyperLinkColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="InActiveName" Caption="وضعیت">
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="10" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowHorizontalScrollBar="true"></Settings>
    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ثبت قرارداد جدید" ToolTip="ثبت قرارداد جدید"
                                    ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnNew_Click">
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مشاهده" ToolTip="مشاهده"
                                    ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnView_Click">
                                    <ClientSideEvents Click="function(s, e) {
	 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                </TSPControls:CustomAspxButton>


                            </td>
                            <td>
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مدیریت پروژه ها" ToolTip="مدیریت پروژه ها"
                                    ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnBack_Click">
                                </TSPControls:CustomAspxButton>
                            </td>

                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>


    <asp:ObjectDataSource ID="ObjdContract" runat="server" SelectMethod="SelectTSContractForMembers"
        TypeName="TSP.DataManager.TechnicalServices.ContractManager">
        <SelectParameters>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="PrjImpObsDsgnId"></asp:Parameter>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="ProjectIngridientTypeId"></asp:Parameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    <dxhf:ASPxHiddenField ID="HiddenFieldPage" runat="server" ClientInstanceName="HiddenFieldPage">
    </dxhf:ASPxHiddenField>
</asp:Content>
