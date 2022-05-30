<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="WorkFlowReport.aspx.cs" Inherits="Members_MemberInfo_WorkFlowReport"
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

            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>




                        <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                            width="100%">
                            <tr>
                                <td align="right">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnShowDetail" CssClass="ButtonMenue"  runat="server" OnClientClick="
                                                ShowWFDesc();
   return false;                   ">مشاهده پانوشت مرحله</asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnBack" CssClass="ButtonMenue" OnClick="btnBack_Click" runat="server">مدیریت درخواست های عضویت</asp:LinkButton>

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
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelWFReport" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>

                        <TSPControls:CustomAspxDevGridView2 runat="server" ID="GridViewWFReport" ClientInstanceName="GridViewWFReport"
                            DataSourceID="ObjdsWfReport" KeyFieldName="StateId" AutoGenerateColumns="False"
                            Width="100%" OnAutoFilterCellEditorInitialize="GridViewWFReport_AutoFilterCellEditorInitialize"
                            OnHtmlDataCellPrepared="GridViewWFReport_HtmlDataCellPrepared">

                            <Settings ShowHorizontalScrollBar="true"></Settings>

                            <Columns>

                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="wfDescriptionSummary" Caption="پانوشت">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="WorkFlowId" Width="150px" Caption="نام فرایند"
                                    VisibleIndex="0">
                                    <PropertiesComboBox ValueType="System.String" TextField="WorkFlowName" ValueField="WorkFlowId"
                                        DataSourceID="ObjdsWorkFlow">
                                    </PropertiesComboBox>
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="WFTaskName" Width="200px"
                                    Caption="مرحله">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="DocName" Caption="پرونده مربوطه">
                                    <CellStyle HorizontalAlign="Right" Wrap="False"></CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="DoerName" Caption="انجام دهنده">
                                    <CellStyle HorizontalAlign="Center" Wrap="False"></CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="StateTypeName" Caption="نوع عملیات">
                                    <CellStyle HorizontalAlign="Center" Wrap="False"></CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" Width="200px" FieldName="Description" Caption="توضیحات">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="5" Name="Date" FieldName="Date" Width="80px" Caption="تاریخ">
                                    <CellStyle HorizontalAlign="Center" Wrap="False"></CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="StateTime" Caption="ساعت">
                                    <CellStyle HorizontalAlign="Center" Wrap="False"></CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewCommandColumn VisibleIndex="7" Caption=" " ShowClearFilterButton="true">
                                </dxwgv:GridViewCommandColumn>
                            </Columns>

                        </TSPControls:CustomAspxDevGridView2>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            </BR>
             <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                 Width="100%">
                 <PanelCollection>
                     <dxp:PanelContent>

                         <table>
                             <tbody>
                                 <tr>
                                     <td>

                                         <asp:LinkButton ID="btnShowDetail2" CssClass="ButtonMenue"  runat="server" OnClientClick="
                                                ShowWFDesc();
  return false;                   ">مشاهده پانوشت مرحله</asp:LinkButton>
                                     </td>
                                     <td>
                                         <asp:LinkButton ID="LinkButton1" CssClass="ButtonMenue" OnClick="btnBack_Click" runat="server">مدیریت درخواست های عضویت</asp:LinkButton>
                                     </td>
                                 </tr>
                             </tbody>
                         </table>

                     </dxp:PanelContent>
                 </PanelCollection>
             </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldWFState">
            </dxhf:ASPxHiddenField>
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
            <TSPControls:CustomASPxPopupControl ID="PopUpWFDesc" runat="server" Width="387px" Height="500px"
                ClientInstanceName="PopUpWFDesc"
                AllowDragging="True" CloseAction="CloseButton"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                HeaderText="پانوشت مرحله انتخاب شده">
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        <div width="100%" align="center">
                            <TSPControls:CustomASPXMemo runat="server" Height="500px" ID="txtWFDesc" Width="387px"
                                ClientInstanceName="txtWFDesc" ReadOnly="true">
                            </TSPControls:CustomASPXMemo>
                        </div>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </TSPControls:CustomASPxPopupControl>

        </ContentTemplate>
    </asp:UpdatePanel>
    <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl1" runat="server" Width="387px" Height="500px"
        ClientInstanceName="PopUpWFDesc"
        AllowDragging="True" CloseAction="CloseButton"
        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        HeaderText="پانوشت مرحله انتخاب شده">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <div width="100%" align="center">
                    <TSPControls:CustomASPXMemo runat="server" Height="500px" ID="ASPxMemo1" Width="387px"
                        ClientInstanceName="txtWFDesc" ReadOnly="true">
                    </TSPControls:CustomASPXMemo>
                </div>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </TSPControls:CustomASPxPopupControl>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                <img align="middle" src="../../Image/indicator.gif" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
