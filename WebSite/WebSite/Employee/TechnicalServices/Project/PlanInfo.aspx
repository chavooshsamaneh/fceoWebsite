<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="PlanInfo.aspx.cs" Inherits="Members_TechnicalServices_Project_PlanInfo"
    Title="مشخصات نقشه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>




<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>


<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID='UpdatePanel1' runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" Width="25px" ID="btnBack" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
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
            <TSPControls:CustomAspxMenuHorizontal ID="MenuPlan" runat="server"
                OnItemClick="MenuPlan_ItemClick">
                <Items>
                    <dxm:MenuItem Name="Plan" Text="مشخصات نقشه">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="PlanDes" Text="طراحان نقشه">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="ControlerViewPoint" Text="بازبین نقشه" Selected="True">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>
            <br />
            <TSP:ProjectInfo ID="prjInfo" runat="server"></TSP:ProjectInfo>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelPlans" HeaderText="مشاهده" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <fieldset>
                            <legend class="HelpUL">اطلاعات پایه نقشه</legend>

                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="نوع نقشه" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtPlanType" Width="100%"
                                                ReadOnly="True" __designer:wfdid="w95">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="شماره نقشه را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="شماره نقشه" ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right" width="35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtPlanNo" Width="100%" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="شماره نقشه را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel5">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtPlanDes" Width="100%"
                                                ReadOnly="True">
                                                <ValidationSettings>
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            <TSPControls:CustomAspxDevGridView runat="server" Width="100%"
                                                ID="GridViewAttachment" DataSourceID="ObjectDataSourceAttachments" KeyFieldName="AttachmentId"
                                                AutoGenerateColumns="False" ClientInstanceName="GridViewAttachment">
                                                <Settings ShowGroupPanel="True" ShowFilterRowMenu="True" ShowHorizontalScrollBar="true"></Settings>
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Title" Caption="نوع فایل">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FileName" Caption="نام فایل">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="FileSize" Caption="حجم فایل (KB)">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="FilePath" Caption=" لینک"
                                                        Name="FilePath">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxHyperLink ID="ASPxHyperLink2" runat="server" Text="ASPxHyperLink" Target="_blank"
                                                                NavigateUrl='<%# Bind("FilePath") %>' OnDataBinding="ASPxHyperLink1_DataBinding">
                                                            </dxe:ASPxHyperLink>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsLoadingPanel Text="در حال بارگذاری"></SettingsLoadingPanel>
                                            </TSPControls:CustomAspxDevGridView>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>

                        <fieldset>
                            <legend class="HelpUL">طراحان نقشه</legend>

                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="center" width="100%" colspan="4">
                                            <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                                                ID="GridViewDesigner" DataSourceID="ObjectDataSourceDesignerPlans" KeyFieldName="DesignerPlansId"
                                                AutoGenerateColumns="False" ClientInstanceName="GridViewDesigner"
                                                OnHtmlDataCellPrepared="GridViewDesigner_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="GridViewDesigner_AutoFilterCellEditorInitialize">
                                                <Settings ShowGroupPanel="True" ShowFilterRowMenu="True" ShowHorizontalScrollBar="true"></Settings>
                                                <Columns>
                                                    <dxwgv:GridViewDataCheckColumn VisibleIndex="0" FieldName="IsMaster" Caption="نماینده اصلی"
                                                        Name="IsMaster">
                                                    </dxwgv:GridViewDataCheckColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MeType" Caption="نوع طراح">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="OfficeEngOId" Caption="کد طراح">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <%--        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="DesignerName" Caption="نام">
                                                            </dxwgv:GridViewDataTextColumn>--%>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Date" Caption="تاریخ">
                                                        <CellStyle Wrap="False">
                                                        </CellStyle>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="CapacityDecrement" Caption="متراژ برای ظرفیت">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="Wage" Caption="متراژ برای دستمزد">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="8" FieldName="DesignerPlansId"
                                                        Caption="DesignerPlansId">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                            </TSPControls:CustomAspxDevGridView2>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
                        <fieldset>
                            <legend class="HelpUL">نواقص نقشه</legend>

                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" width="100%" align="center" colspan="4">
                                            <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                                                ID="GridViewViewPoint" DataSourceID="ObjectDataSourcePlansControlerViewPoint"
                                                KeyFieldName="ViewPointId" AutoGenerateColumns="False" ClientInstanceName="GridViewDesigner">
                                                <Settings ShowGroupPanel="True" ShowFilterRowMenu="True" ShowHorizontalScrollBar="true"></Settings>
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="RowNo" Width="30px" Caption="ردیف">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Subject" Caption="موضوع">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="SheetNo" Caption="شماره برگ نقشه">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ViewPoint" Width="300px"
                                                        Caption="توضیحات بازبینی">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="ControllerName" Caption="بازبین"
                                                        Name="ControllerName" Width="150px">
                                                        <CellStyle HorizontalAlign="Right" Wrap="False">
                                                        </CellStyle>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="Id" Caption="Id">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                            </TSPControls:CustomAspxDevGridView2>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" Width="25px" ID="btnBack2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                            <ClientSideEvents Click="function(s, e) {
		
}"></ClientSideEvents>
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
            <dxhf:ASPxHiddenField ID="HiddenFieldPrjDes" runat="server">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjectDataSourceAttachments" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="FindByTableTypeId" TypeName="TSP.DataManager.TechnicalServices.AttachmentsManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="TableTypeId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="TableType" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="AttachTypeId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceDesignerPlans" runat="server" SelectMethod="FindActivesByPlansId"
                TypeName="TSP.DataManager.TechnicalServices.Designer_PlansManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="PlansId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourcePlansControlerViewPoint" runat="server"
                OldValuesParameterFormatString="original_{0}" SelectMethod="FindByPlansId" TypeName="TSP.DataManager.TechnicalServices.PlansControlerViewPointManager">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="PlansId"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            </div>
                </div>
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                    BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                            <img src="../../../Image/indicator.gif" align="middle" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
