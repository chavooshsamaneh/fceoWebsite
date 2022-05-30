<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="WorkFlowReport.aspx.cs" Inherits="Institue_InstitueInfo_WorkFlowReport"
    Title="پیگیری گردش کار" %>

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
     <script language="javascript">

        function ShowWFDesc(s, e) {
            GridViewWFReport.GetRowValues(GridViewWFReport.GetFocusedRowIndex(), 'Description', OnGetSelectedFieldValues);

        }
        function OnGetSelectedFieldValues(selectedValues) {

            txtWFDesc.SetText(selectedValues);
            PopUpWFDesc.Show();
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton runat="server" ID="btnShowDetail" ButtonType="ShowDetail"
                                        ToolTip="پانوشت مرحله" IsMenuButton="true" AutoPostBack="false">
                                        <ClientSideEvents Click="ShowWFDesc" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False"
                                        EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                        ToolTip="بازگشت" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <div align="right">
                <ul class="HelpUL">
                    <li>جهت مشاهده <b>پانوشت هر مرحله</b> پس از انتخاب آن در جدول زیر  برروی ''دکمه مشاهده پانوشت مرحله'' (
                            <asp:Image ID="Image10" runat="server" ImageUrl="~/Images/icons/ShowDetail.png" />
                        ) واقع در منوی بالا/پایین صفحه کلیک نمایید. </li>

                </ul>
            </div>
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewWFReport" ClientInstanceName="GridViewWFReport" runat="server" DataSourceID="ObjdsWfReport"
                Width="100%" OnAutoFilterCellEditorInitialize="GridViewWFReport_AutoFilterCellEditorInitialize"
                OnHtmlDataCellPrepared="GridViewWFReport_HtmlDataCellPrepared" 
                AutoGenerateColumns="False" KeyFieldName="StateId" RightToLeft="True">
                <Settings ShowHorizontalScrollBar="true" />
                <Columns>



                    <dxwgv:GridViewDataImageColumn VisibleIndex="0" FieldName="SignUrl" Caption="تصویر امضاء">
                        <PropertiesImage ImageHeight="50px" ImageWidth="50px"></PropertiesImage>
                    </dxwgv:GridViewDataImageColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="wfDescriptionSummary" Caption="پانوشت">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="WorkFlowId" Caption="نام فرایند" VisibleIndex="0" Width="200px">
                        <PropertiesComboBox TextField="WorkFlowName" DataSourceID="ObjdsWorkFlow" ValueType="System.String"
                            ValueField="WorkFlowId">
                        </PropertiesComboBox>
                        <CellStyle Wrap="false" HorizontalAlign="Right"></CellStyle>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="WFTaskName" Caption="مرحله" Width="200px">
                        <CellStyle Wrap="false" HorizontalAlign="Right"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="DocName" Caption="پرونده مربوطه">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="DoerName" Caption="انجام دهنده">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="ExpireDate" Caption="مهلت">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="Date" Caption="تاریخ">
                        <CellStyle Wrap="False" HorizontalAlign="Right"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="StateTime" Caption="ساعت">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="StateTypeName" Caption="نوع عملیات">
                    </dxwgv:GridViewDataTextColumn>


                    <dxwgv:GridViewCommandColumn VisibleIndex="8" Caption=" " Width="50px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>

            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton runat="server" ID="btnShowDetail2" ButtonType="ShowDetail"
                                            ToolTip="پانوشت مرحله" IsMenuButton="true" AutoPostBack="false">
                                            <ClientSideEvents Click="ShowWFDesc" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton5" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldWFState">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjdsWfReport" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="SelectStateReportsById" TypeName="TSP.DataManager.WorkFlowStateManager">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="TableId"></asp:Parameter>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="TableType"></asp:Parameter>
                    <asp:Parameter DefaultValue="-1" Name="WfCode" Type="Int32" />
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="NmcId"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsWorkFlow" runat="server" TypeName="TSP.DataManager.WorkFlowManager"
                SelectMethod="GetData"></asp:ObjectDataSource>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
