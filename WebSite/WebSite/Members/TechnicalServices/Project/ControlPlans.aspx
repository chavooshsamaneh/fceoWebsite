<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="ControlPlans.aspx.cs" Inherits="Members_TechnicalServices_Project_ControlPlans"
    Title="مدیریت بازبینی نقشه ها" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../../UserControl/WFUserControl.ascx" TagName="WFUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ثبت بازبینی" ToolTip="ثبت بازبینی"
                                            Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnEdit_Click">
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مشاهده" ToolTip="مشاهده"
                                            ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td></td>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="گردش کار" ToolTip="گردش کار"
                                            ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" Visible="false">
                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewPlans.GetFocusedRowIndex()&lt;0)
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
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <ul runat="server" id="ULAlarm" class="HelpUL">
                <li>در صورتی که شما به عنوان بازبین نقشه انتخاب شده اید،  نقشه های مربوطه را در این لیست مشاهده می نمائید</li>
                <li>جهت آگاهی از عنوان مراحل تایید نقشه و آیکن مربوطه، به راهنمای مراحل در پایین همین صفحه مراجعه نمایید</li>
                <li><b>تنها در صورتی قادر به ثبت بازبینی نقشه می باشید که مرحله نقشه "بررسی نقشه توسط بازبین" (  
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/WF/TS/TS_CheckerCheckMaps.png" Height="25px"
                        Width="25px" />) باشد </b></li>

            </ul>
            <TSPControls:CustomAspxDevGridView ID="GridViewPlans" runat="server" DataSourceID="ObjdsPlans"
                Width="100%"
                ClientInstanceName="GridViewPlans"
                AutoGenerateColumns="False" KeyFieldName="PlansId">
                <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                
                <Columns>


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
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="PlansId" Caption="PlansId"
                        Visible="False">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="PrjReId" Caption="PrjReId"
                        Visible="False">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectId" Caption="کد پروژه">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectName" Width="150px"
                        Caption="نام پروژه">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                        <CellStyle Wrap="False" HorizontalAlign="Right">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="No" Caption="شماره نقشه">
                        <HeaderStyle Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="PlanVersion" Caption="ورژن">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="PlansTypeId" Caption="نوع  نقشه" VisibleIndex="3" Width="150px">
                        <PropertiesComboBox ValueType="System.String" TextField="Title" DataSourceID="ObjdsPlansType"
                            ValueField="PlansTypeId">
                        </PropertiesComboBox>
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="PlanDate" Caption="تاریخ ثبت">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="FileNo" Caption="شماره پرونده پروژه"
                        Name="FileNo">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="RegisteredNo" Caption="پلاک ثبتی پروژه"
                        Name="RegisteredNo">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="LicenseNo" Caption="شماره پروانه ساخت"
                        Name="LicenseNo">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="Foundation" Caption="متراژ">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="InActives" Caption="وضعیت">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="10" FieldName="ConfirmeStatus" Caption="تایید بازبین">
                        <HeaderStyle Wrap="False" />
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="12" Width="1px" Caption=" " ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <Settings ShowHorizontalScrollBar="true"></Settings>
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
            <br />
            <dxhf:ASPxHiddenField runat="server" ID="HiddenPage" ClientInstanceName="HiddenPage">
            </dxhf:ASPxHiddenField>
            <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewPlans" SessionName="SendBackDataTable_ControlPlans"
                OnCallback="CallbackPanelWorkFlow_Callback" />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ثبت بازبینی" ToolTip="ثبت بازبینی"
                                            Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnEdit_Click">
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مشاهده" ToolTip="مشاهده"
                                            ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="گردش کار" ToolTip="گردش کار"
                                            ID="btnSendToNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" Visible="false">
                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewPlans.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	

    //TextDesc.SetText('');
	//CallbackPanelWorkFlow.PerformCallback('');	
	//PopupWorkFlow.Show();
	ShowWf();
}
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
                TypeName="TSP.DataManager.WorkFlowTaskManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsPlans" runat="server" SelectMethod="SelectTSPlansForControler"
                TypeName="TSP.DataManager.TechnicalServices.PlansManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="MeId"></asp:Parameter>
                    <asp:Parameter Type="Int32" DefaultValue="0" Name="InActiveControler"></asp:Parameter>

                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsPlansType" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.TechnicalServices.PlansTypeManager"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
