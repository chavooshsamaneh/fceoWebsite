<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeFinancialStatus.aspx.cs" Inherits="Settlement_OfficeDocument_OfficeFinancialStatus"
    Title="وضعیت مالی" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                    ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnView_Click">
                                    <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                    <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </hoverstyle>
                                    <image url="~/Images/icons/view.png">
                                                                        </image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                    CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnBack_Click">
                                 
                                    <image url="~/Images/icons/Back.png">
                                                                        </image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server" OnItemClick="ASPxMenu1_ItemClick">
        <Items>
            <dxm:MenuItem Name="Office" Text="مشخصات شرکت">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Member" Text="اعضای شرکت">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Agent" Text="شعبه ها">
            </dxm:MenuItem>
            <%--     <dxm:MenuItem Name="Job" Text="سوابق کاری">
                            </dxm:MenuItem>--%>
            <dxm:MenuItem Name="Letters" Text="روزنامه های رسمی">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Financial" Text="وضعیت مالی" Selected="true">
            </dxm:MenuItem>
        </Items>
    </TSPControls:CustomAspxMenuHorizontal>
    <br />
    <div style="width: 100%; text-align: right">
        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="شرکت :">
        </dxe:ASPxLabel>
        <dxe:ASPxLabel ID="lblOfName" runat="server">
        </dxe:ASPxLabel>
    </div>
    <br />
    <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" DataSourceID="ObjectDataSource1"
        Width="100%"
        OnDetailRowExpandedChanged="CustomAspxDevGridView1_DetailRowExpandedChanged"
        KeyFieldName="OfsId" ClientInstanceName="jgrid" AutoGenerateColumns="False" OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize"
        OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared">
        <ClientSideEvents FocusedRowChanged="function(s, e) {
	//jgrid.ExpandDetailRow(jgrid.GetFocusedRowIndex());
}"></ClientSideEvents>
        <Templates>
            <DetailRow>
                <TSPControls:CustomAspxDevGridView ID="GridViewJudge" runat="server" DataSourceID="ObjdsJudgment"
                    Width="100%"
                    KeyFieldName="JudgeId" AutoGenerateColumns="False" EnableViewState="False" OnAutoFilterCellEditorInitialize="GridViewJudge_AutoFilterCellEditorInitialize"
                    OnHtmlDataCellPrepared="GridViewJudge_HtmlDataCellPrepared" OnBeforePerformDataSelect="GridViewJudge_BeforePerformDataSelect"
                    __designer:wfdid="w5">

                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="0">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="1">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="NcName" VisibleIndex="2">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره جلسه" FieldName="MeetingId" VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ جلسه" FieldName="MeetingDate" VisibleIndex="4">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="امتیاز" FieldName="FactValue" VisibleIndex="5">
                            <PropertiesTextEdit DisplayFormatString="#.###">
                                <Style HorizontalAlign="Right"></Style>
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="7" Width="1px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نظر کارشناسی" FieldName="JudgeViewPoint" VisibleIndex="8"
                            Width="200px">
                            <CellStyle Wrap="True">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="Confirm" VisibleIndex="6">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>

                    <SettingsDetail IsDetailGrid="True" />
                </TSPControls:CustomAspxDevGridView>

            </DetailRow>
        </Templates>
        <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>

        <Columns>
            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="OfsId"
                Name="OfsId">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Name" Caption="نام وضعیت"
                Name="Name">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Factor" Width="60px" Caption="ضریب">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="FactorValue" Width="60px"
                Caption="امتیاز">
                <CellStyle Wrap="False">
                </CellStyle>
                <PropertiesTextEdit DisplayFormatString="#.###">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="CreateDate" Width="80px"
                Caption="تاریخ ایجاد">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="InActiveName" Width="50px"
                Caption="وضعیت">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn VisibleIndex="5" Caption=" " Width="50px" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="OfReId">
            </dxwgv:GridViewDataTextColumn>
        </Columns>

        <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                    ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnView_Click">
                                    <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                    <hoverstyle backcolor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </hoverstyle>
                                    <image url="~/Images/icons/view.png">
                                                                        </image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                    CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnBack_Click">
                                   
                                    <image url="~/Images/icons/Back.png">
                                                                        </image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="FindByOffRequest"
        TypeName="TSP.DataManager.DocOffOfficeFinancialStatusManager">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="OfId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="OfReId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="IsConfirm" Type="Int16" />
            <asp:Parameter DefaultValue="-1" Name="JustActive" Type="Int16" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="OfficeId" runat="server" Visible="False" />
    <asp:HiddenField ID="PgMode" runat="server" />
    <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False" />
    <asp:ObjectDataSource ID="ObjdsJudgment" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="SelectByFinancial" TypeName="TSP.DataManager.TrainingJudgmentManager">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="-1" Name="PkId" SessionField="PKId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="JudgeId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
