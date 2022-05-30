<%@ Page Title="مدیریت نقشه ها" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="MemberPlans.aspx.cs" Inherits="Members_TechnicalServices_Project_MemberPlans" %>


<%@ Register Src="~/UserControl/WFUserControl.ascx" TagName="WFUserControl"
    TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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
<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tr>
                                    <td align="right">
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ثبت کار طراحی جدید" ToolTip="ثبت کار طراحی جدید"
                                            ID="btnSubmitDesign" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSubmitDesign_Click">
                                        </TSPControls:CustomAspxButton>

                                    </td>
                                    <td align="right">
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ویرایش اطلاعات نقشه" ToolTip="ویرایش اطلاعات نقشه"
                                            ID="btnEditDesign" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnEditDesign_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                        </TSPControls:CustomAspxButton>

                                    </td>
                                    <td align="right">
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مشاهده اطلاعات نقشه" ToolTip="مشاهده اطلاعات نقشه"
                                            ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                        </TSPControls:CustomAspxButton>

                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="گردش کار نقشه" ToolTip="گردش کار نقشه"
                                            ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
	ShowWf();
}
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                <TSPControls:CustomAspxDevGridView ID="GridViewPlans" ClientInstanceName="mygrid" runat="server" DataSourceID="ObjdsPlans"
                    Width="100%"
                    KeyFieldName="PlansId" AutoGenerateColumns="False">
                    <Templates>
                        <DetailRow>
                            <TSPControls:CustomAspxDevGridView ID="GridViewPlanSubRe" runat="server" DataSourceID="ObjdsPlanDesigner"
                                Width="100%"
                                KeyFieldName="PlansId" AutoGenerateColumns="False" OnBeforePerformDataSelect="GridViewPlanSubRe_BeforePerformDataSelect">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="OfficeEngOId" Caption="کد دفتر/شرکت طراح">
                                        <CellStyle Wrap="False" HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="DesignerMeId" Caption="کد عضویت طراح">
                                        <CellStyle Wrap="False" HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="DesignerName" Caption="نام و نام خانوادگی"
                                        Width="300px">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="CapacityDecrement" Caption="متراژ کسر ظرفیت">
                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="Wage" Caption="متراژ دستمزد">
                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="YearName" Caption="تعرفه">
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="Year" Caption="سال کاری">
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="DesignerCreateDate" Caption="تاریخ ثبت طراح">
                                        <CellStyle Wrap="False" HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="Date" Caption="تاریخ ثبت نقشه">
                                        <CellStyle Wrap="False" HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsDetail IsDetailGrid="True"></SettingsDetail>
                            </TSPControls:CustomAspxDevGridView>
                        </DetailRow>
                    </Templates>
                    <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Visible="False" FieldName="Status"
                            Width="150px" Caption="Status">
                            <HeaderStyle Wrap="True"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                            VisibleIndex="0" Width="40px">
                            <DataItemTemplate>
                                <div align="center">
                                    <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>'>
                                    </dxe:ASPxImage>
                                </div>
                            </DataItemTemplate>
                            <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                                ValueType="System.String">
                            </PropertiesComboBox>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectId" Caption="کد پروژه"
                            Name="ProjectId">
                            <HeaderStyle Wrap="True"></HeaderStyle>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="WorkFlowName" Width="200px"
                            Caption="نوع درخواست">
                            <HeaderStyle Wrap="True"></HeaderStyle>
                            <CellStyle Wrap="True">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="No" Width="150px" Caption="شماره نقشه">
                            <HeaderStyle Wrap="True"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <%--       <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="PlanVersion2" Width="50px"
                            Caption="ورژن">
                        </dxwgv:GridViewDataTextColumn>--%>
                        <dxwgv:GridViewDataComboBoxColumn FieldName="PlansTypeId" Caption="نوع نقشه" VisibleIndex="3"
                            Width="150px">
                            <HeaderStyle Wrap="True"></HeaderStyle>
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                            <PropertiesComboBox ValueType="System.String" TextField="Title" DataSourceID="ObjdsPlansType"
                                ValueField="PlansTypeId">
                            </PropertiesComboBox>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="PlanDate" Caption="تاریخ ثبت">
                            <HeaderStyle Wrap="True"></HeaderStyle>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="RegisteredNo" Caption="پلاک ثبتی پروژه"
                            Name="RegisteredNo">
                            <HeaderStyle Wrap="True"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="LicenseNo" Caption="شماره پروانه ساخت"
                            Name="LicenseNo">
                            <HeaderStyle Wrap="True"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="Foundation" Caption="متراژ پروژه">
                            <HeaderStyle Wrap="True"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>                        
                        <dxwgv:GridViewDataTextColumn VisibleIndex="12" FieldName="InActives" Caption="وضعیت">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="12" Visible="False" FieldName="TaskName"
                            Width="150px" Caption="وضعیت درخواست">
                            <HeaderStyle Wrap="True"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>                      
                        <dxwgv:GridViewCommandColumn VisibleIndex="14" Caption=" " ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <Settings ShowHorizontalScrollBar="true"></Settings>
                    <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
                </TSPControls:CustomAspxDevGridView>
                <fieldset width="100%">
                    <legend>راهنما</legend>
                    <ul class="HelpWorkflowTasksImages">
                        <li class="col-sm-4">
                            <ul>
                                <asp:Repeater runat="server" ID="RepeaterWfHep1">
                                    <ItemTemplate>
                                        <li>
                                            <asp:Image ID="Image2" Height="16px" Width="16px" runat="server" ImageUrl='<%# Eval("ImageURL") %> ' />
                                            <a><%# Eval("TaskName") %> </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <li></li>
                            </ul>
                        </li>
                        <li class="col-sm-4">
                            <ul>
                                <li class="dropdown-header"></li>
                                <asp:Repeater runat="server" ID="RepeaterWfHep2">
                                    <ItemTemplate>
                                        <li>
                                            <asp:Image ID="Image2" Height="16px" Width="16px" runat="server" ImageUrl='<%# Eval("ImageURL") %> ' />
                                            <a><%# Eval("TaskName") %> </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <li></li>
                            </ul>
                        </li>
                        <li class="col-sm-4">
                            <ul>
                                <li class="dropdown-header"></li>
                                <asp:Repeater runat="server" ID="RepeaterWfHep3">
                                    <ItemTemplate>
                                        <li>
                                            <asp:Image ID="Image2" Height="16px" Width="16px" runat="server" ImageUrl='<%# Eval("ImageURL") %> ' />
                                            <a><%# Eval("TaskName") %> </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <li></li>
                            </ul>
                        </li>
                    </ul>
                </fieldset>
                <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewPlans" SessionName="SendBackDataTable_EmpPln"
                    OnCallback="CallbackPanelWorkFlow_Callback" />
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tr>
                                    <td align="right">
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ثبت کار طراحی جدید" ToolTip="ثبت کار طراحی جدید"
                                            ID="btnSubmitDesign2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSubmitDesign_Click">
                                        </TSPControls:CustomAspxButton>

                                    </td>
                                    <td align="right">
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ویرایش اطلاعات نقشه" ToolTip="ویرایش اطلاعات نقشه"
                                            ID="btnEditDesign2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnEditDesign_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                        </TSPControls:CustomAspxButton>
                                        <td>
                                            <td align="right">
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مشاهده اطلاعات نقشه" ToolTip="مشاهده اطلاعات نقشه"
                                                    ID="CustomAspxButton1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnView_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                                </TSPControls:CustomAspxButton>

                                            </td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="گردش کار نقشه" ToolTip="گردش کار نقشه"
                                                ID="btnSendNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
	ShowWf();
}
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldPlan" ClientInstanceName="HDPlan">
                </dxhf:ASPxHiddenField>
                <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" TypeName="TSP.DataManager.WorkFlowTaskManager"
                    SelectMethod="SelectByWorkCode">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjdsPlans" runat="server" TypeName="TSP.DataManager.TechnicalServices.PlansManager"
                    SelectMethod="SelectTSPlansForMember" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                        <asp:Parameter DefaultValue="0" Name="DesignerInAcive" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjdsPlanDesigner" runat="server" TypeName="TSP.DataManager.TechnicalServices.Designer_PlansManager"
                    SelectMethod="SelectTSDesignerPlansForByPlanId" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:SessionParameter SessionField="PlansId" Type="Int32" DefaultValue="-1" Name="PlansId"></asp:SessionParameter>
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjdsPlansType" runat="server" TypeName="TSP.DataManager.TechnicalServices.PlansTypeManager"
                    SelectMethod="GetData"></asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

