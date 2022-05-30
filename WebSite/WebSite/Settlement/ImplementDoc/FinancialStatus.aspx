<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="FinancialStatus.aspx.cs" Inherits="Settlement_ImplementDoc_FinancialStatus"
    Title="توان مالی مجری حقیقی" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                            <Image Url="~/Images/icons/Back.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                            </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomAspxMenuHorizontal ID="MenuMemberFile" runat="server" OnItemClick="MenuMemberFile_ItemClick">
                <Items>
                    <dxm:MenuItem Text="مشخصات مجوز" Name="ImplDoc"></dxm:MenuItem>
                <%--    <dxm:MenuItem Text="سابقه کار" Name="JobHistory"></dxm:MenuItem>--%>
                    <dxm:MenuItem Text="توان مالی" Selected="True"></dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>

            <br />
                            <TSPControls:CustomAspxDevGridView runat="server" Width="100%" ID="GridViewFinancialStatus" DataSourceID="ObjdsFinancialStatus" KeyFieldName="OfsId" AutoGenerateColumns="False" ClientInstanceName="jgrid" OnAutoFilterCellEditorInitialize="GridViewFinancialStatus_AutoFilterCellEditorInitialize" OnHtmlDataCellPrepared="GridViewFinancialStatus_HtmlDataCellPrepared">
                                <SettingsDetail ShowDetailRow="True"></SettingsDetail>
                                <Templates>
                                    <DetailRow>
                                        <TSPControls:CustomAspxDevGridView ID="GridViewJudge" runat="server" DataSourceID="ObjdsJudgment" Width="419px" EnableViewState="False" AutoGenerateColumns="False" KeyFieldName="JudgeId" OnAutoFilterCellEditorInitialize="GridViewJudge_AutoFilterCellEditorInitialize" OnHtmlDataCellPrepared="GridViewJudge_HtmlDataCellPrepared" __designer:wfdid="w1" OnBeforePerformDataSelect="GridViewJudge_BeforePerformDataSelect">
                                            <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>

                                          
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FirstName" Caption="نام">
                                                    <CellStyle Wrap="False"></CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="LastName" Caption="نام خانوادگی">
                                                    <CellStyle Wrap="False"></CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MeetingId" Caption="شماره جلسه"></dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="MeetingDate" Caption="تاریخ جلسه">
                                                    <CellStyle Wrap="False"></CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="FactValue" Caption="امتیاز">
                                                    <PropertiesTextEdit DisplayFormatString="#.###"></PropertiesTextEdit>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="5" Width="1px"></dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="JudgeViewPoint" Width="200px" Caption="نظر کارشناسی"></dxwgv:GridViewDataTextColumn>
                                            </Columns>

                                        </TSPControls:CustomAspxDevGridView>
                                    </DetailRow>
                                </Templates>
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="OfsId" Name="OfsId"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Name" Caption="نام وضعیت" Name="Name"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Factor" Caption="ضریب"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="FactorValue" Caption="امتیاز">
                                        <CellStyle Wrap="False"></CellStyle>

                                        <PropertiesTextEdit DisplayFormatString="#.###"></PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="CreateDate" Caption="تاریخ ایجاد">
                                        <CellStyle Wrap="False"></CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewCommandColumn VisibleIndex="4" Caption=" " ShowClearFilterButton="true">
                                    </dxwgv:GridViewCommandColumn>
                                </Columns>                                
                            </TSPControls:CustomAspxDevGridView>
            
              
            <br />

            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldFinantialStatus"></dxhf:ASPxHiddenField>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>

                                            <Image Url="~/Images/icons/Back.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

    <asp:ObjectDataSource ID="ObjdsFinancialStatus" runat="server" FilterExpression="MeId={0}"
        SelectMethod="SelectForImplementDoc" TypeName="TSP.DataManager.DocOffOfficeFinancialStatusManager">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="OfReId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsJudgment" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="SelectByFinancial" TypeName="TSP.DataManager.TrainingJudgmentManager">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="-1" Name="PkId" SessionField="PKId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="JudgeId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
