<%@ Page Title="لیست ارجاع کار ناظران" Language="C#" Async="true" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ObserverSelected.aspx.cs" Inherits="Employee_TechnicalServices_Project_ObserverSelected" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتخاب ناظر"
                                            ID="btnObserverSelect" ClientInstanceName="btnObserverSelect" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnObserverSelect_Click">
                                            <ClientSideEvents Click="function(s, e) {
                        if (ASPxClientEdit.ValidateGroup() == false)
                             {
                               e.processOnServer= false;                                       
                             }
}" />

                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/AutoFishPayment.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                  
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
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
            <br />
            <TSP:ProjectInfo ID="prjInfo" runat="server" />
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanelObsType" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <div>


                            <table width="100%">

                                <tr>
                                    <td colspan="4">
                                        <TSPControls:CustomAspxDevGridView2 ID="GridViewObsTypeByProjectInfo" Visible="false" runat="server" DataSourceID="ObjectDataSourceObserverMajorByProjectInfo"
                                            AutoGenerateColumns="False"
                                            KeyFieldName="MajorIdList" Width="100%" ClientInstanceName="GridViewObsTypeByProjectInfo">
                                            <SettingsText Title="لیست ناظران مورد نیاز پروژه" />
                                            <Settings ShowTitlePanel="true" ShowHorizontalScrollBar="true"></Settings>
                                            <Columns>
                                                <%--   <dxwgv:GridViewDataTextColumn Width="100px" VisibleIndex="0" FieldName="InActiveName"
                                        Caption="وضعیت">
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>--%>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" Width="500px" FieldName="MajorList" Caption="نوع ناظر">
                                                </dxwgv:GridViewDataTextColumn>

                                                <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="10" ShowClearFilterButton="true">
                                                </dxwgv:GridViewCommandColumn>
                                            </Columns>

                                        </TSPControls:CustomAspxDevGridView2>
                                    </td>

                                </tr>

                                <dxcp:ASPxPanel ID="RoundPanelCmbObsType" Visible="false" runat="server">
                                    <PanelCollection>
                                        <dxcp:PanelContent>
                                            <br />
                                            <tr>
                                                <td width="15%">نوع ناظر* </td>
                                                <td width="35%">
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                        TextField="MajorList" ID="comboObsTypeByProjectInfo" DataSourceID="ObjectDataSourceObserverMajorByProjectInfo"
                                                        ValueType="System.String" RightToLeft="True" ValueField="MajorIdList">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="true" ErrorText="نوع ناظر را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomAspxComboBox>

                                                </td>
                                                <td width="15%"></td>
                                                <td width="35%"></td>
                                            </tr>
                                        </dxcp:PanelContent>
                                    </PanelCollection>
                                </dxcp:ASPxPanel>

                            </table>

                            <asp:ObjectDataSource ID="ObjectDataSourceObserverMajorByProjectInfo" runat="server" SelectMethod="SelectTSProjectObserverMajorByProjectInfo"
                                TypeName="TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="-1" Name="GroupId" Type="Int32" />
                                    <asp:Parameter DefaultValue="-1" Name="ProjectFoundation" Type="Int32" />
                                    <asp:Parameter DefaultValue="-1" Name="StructureSkeletonId" Type="Int32" />
                                    <asp:Parameter DefaultValue="" Name="ExecptionMajorIdList" Type="String" />                                    
                                </SelectParameters>
                            </asp:ObjectDataSource>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomAspxDevGridView Width="100%" ID="GridViewProjectSelectedObserver" runat="server"
                DataSourceID="ObjectDataSourceSelectObs"
                ClientInstanceName="GridViewProjectSelectedObserver" EnableViewState="False" KeyFieldName="ProjectObserverSelectedId" AutoGenerateColumns="False">
                <SettingsText Title="لیست ارجاع کار ناظران پروژه" />
                <SettingsCookies Enabled="true" StoreFiltering="true" StoreColumnsWidth="true" StoreColumnsVisiblePosition="true" />
                <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
                <SettingsCustomizationWindow Enabled="True" />
                <Settings ShowTitlePanel="true" ShowHorizontalScrollBar="true"></Settings>
                <Columns>

                    <%-- <dxwgv:GridViewDataTextColumn Caption="سال افتتاح ظرفیت" FieldName="CapacityAssignmentYear" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn Caption="کد پروژه" FieldName="ProjectId" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="MeId" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام عضو" FieldName="MeFullName" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شماره پروانه اشتغال بکار" FieldName="FileNo" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn Caption="رشته" FieldName="MjParentName" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="پایه نظارت" FieldName="GrdName" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn Caption="متراژ کسر ظرفیت" FieldName="CapacityDecrement" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxcp:GridViewDataTextColumn FieldName="IsObserverConfirmedName" Caption="وضعیت تایید"
                        Name="IsConfirmName" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxcp:GridViewDataTextColumn>



                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>

            </TSPControls:CustomAspxDevGridView>
            <br />


            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="انتخاب ناظر"
                                            ID="btnObserverSelect2" ClientInstanceName="btnObserverSelect2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnObserverSelect_Click">
                                            <ClientSideEvents Click="function(s, e) {
                        if (ASPxClientEdit.ValidateGroup() == false)
                             {
                               e.processOnServer= false;                                       
                             }
}" />

                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/AutoFishPayment.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
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

            <dxhf:ASPxHiddenField ID="HDpage" runat="server">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjectDataSourceSelectObs" runat="server" TypeName="TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager"
                SelectMethod="SearchForManagmentPage" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>

                    <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                    <asp:Parameter DefaultValue="-2" Name="ProjectId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="AgentId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
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

