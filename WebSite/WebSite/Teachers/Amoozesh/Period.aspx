<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Period.aspx.cs" Inherits="Teachers_Amoozesh_Period"
    Title="دوره های آموزشی" %>

<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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
    <div id="Content" runat="server" style="width: 100%" align="center">
        <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
            visible="true">
            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
            [<a class="closeLink" href="#">بستن</a>]
        </div>
        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>
                    <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                        width="100%">
                        <tbody>
                            <tr>
                                <td style="vertical-align: top" align="right">
                                    <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                                        ToolTip="مشاهده">
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>

                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnMark" runat="server" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnMark_Click" Text=" "
                                                        ToolTip="ثبت نمرات آزمون">
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/Tamdid.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <%--     <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnObjection" runat="server" CausesValidation="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnObjection_Click" Text=" "
                                                        ToolTip="اعتراضات">
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/Paper2.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>--%>
                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep" runat="server" AutoPostBack="False" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False"
                                                        Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
	ShowWf();
}
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/reload.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <%--                                                <td dir="ltr">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing" runat="server" AutoPostBack="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnTracing_Click" Text=" "
                                                        ToolTip="پیگیری" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/Cheque Status ReChange.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>--%>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanelMenu>
        <br />
        <TSPControls:CustomAspxDevGridView ID="GridViewPeriods" runat="server" Width="100%"
            KeyFieldName="PkId" EnableViewState="False" DataSourceID="OdbPeriod" AutoGenerateColumns="False"
            ClientInstanceName="gridview" OnHtmlDataCellPrepared="GridViewPeriods_HtmlDataCellPrepared"
            OnAutoFilterCellEditorInitialize="GridViewPeriods_AutoFilterCellEditorInitialize">
            <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
            <Settings ShowHorizontalScrollBar="true"></Settings>

            <SettingsDetail ExportMode="None" ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
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
                <dxwgv:GridViewDataTextColumn Caption="کد" FieldName="InsId" Visible="False" VisibleIndex="0">
                    <HeaderStyle Wrap="True" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="عنوان دوره" FieldName="PeriodTitle" VisibleIndex="0"
                    Width="350px">
                    <CellStyle Wrap="false">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="کد دوره" FieldName="PPCode" VisibleIndex="1"  Width="200px">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="نام مؤسسه" FieldName="InsName" VisibleIndex="4" Width="200px">
                    <CellStyle Wrap="false">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="نام مدیر مؤسسه" FieldName="Manager" VisibleIndex="5"  Width="200px">
                    <CellStyle Wrap="false">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="StartDate" VisibleIndex="2"
                    Width="80px">
                    <CellStyle HorizontalAlign="Right" Wrap="false">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان" FieldName="EndDate" VisibleIndex="3"
                    Width="80px">
                    <CellStyle HorizontalAlign="Right" Wrap="false">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="امتیاز" FieldName="Point" Visible="False"
                    VisibleIndex="6" Width="40px">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="PeriodStatus" VisibleIndex="6">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Status" Visible="false" VisibleIndex="6">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="PkId" Name="PkId" Visible="False" VisibleIndex="7">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="7" Width="30px" ShowClearFilterButton="true">
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn FieldName="InsId" Visible="False" VisibleIndex="8">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <Templates>
                <DetailRow>
                    <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False"
                        DataSourceID="ObjectDataSourceRequest" RightToLeft="True" ID="GridViewRequest"
                        KeyFieldName="PPRId" AutoGenerateColumns="False" ClientInstanceName="GridViewRequest"
                        Width="100%" OnBeforePerformDataSelect="GridViewRequest_BeforePerformDataSelect">
                        <Settings ShowHorizontalScrollBar="true"></Settings>
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
                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="TypeName"
                                Caption="نام درخواست" Name="TypeName">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" Width="100px" FieldName="CreateDate"
                                Caption="تاریخ درخواست" Name="Date">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" Width="80px" FieldName="IsConfirmName"
                                Caption="وضعیت" Name="IsConfirmName">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" Width="150px" FieldName="LetterNo"
                                Caption="شماره نامه" Name="LetterNo">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" Width="80px" FieldName="LetterDate"
                                Caption="تاریخ نامه" Name="Date">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="5" Width="150px" FieldName="RequesterName"
                                Caption="نام ارسال کننده" Name="RequesterName">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="6" Width="150px" FieldName="WFRequesterType"
                                Caption="سمت ارسال کننده" Name="WFRequesterType">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>

                            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="50px" ShowClearFilterButton="true">
                            </dxwgv:GridViewCommandColumn>
                        </Columns>

                    </TSPControls:CustomAspxDevGridView>
                </DetailRow>
            </Templates>
        </TSPControls:CustomAspxDevGridView>
        <br />
        <fieldset width="100%">
            <legend>راهنما</legend>
            <ul class="HelpWorkflowTasksImages">
                <li class="col-sm-4">
                    <ul>
                        <asp:Repeater runat="server" ID="RepeaterWfHelp1">
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
                        <asp:Repeater runat="server" ID="RepeaterWfHelp2">
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
                        <asp:Repeater runat="server" ID="RepeaterWfHelp3">
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
            <ul class="HelpWorkflowTasksImages">
                *راهنمای گردش کار درخواست چاپ گواهینامه آموزشی
            </ul>
            <ul class="HelpWorkflowTasksImages">
                <li class="col-sm-4">
                    <ul>
                        <asp:Repeater runat="server" ID="RepeaterWfHepPrint1">
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
                        <asp:Repeater runat="server" ID="RepeaterWfHepPrint2">
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
                        <asp:Repeater runat="server" ID="RepeaterWfHepPrint3">
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

        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>



                    <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                        width="100%">
                        <tbody>
                            <tr>
                                <td style="vertical-align: top" align="right">
                                    <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView1" runat="server" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                                        ToolTip="مشاهده">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>

                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnMark2" runat="server" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="ثبت نمرات آزمون"
                                                        OnClick="btnMark_Click">
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/Tamdid.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <%--     <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnObjection2" runat="server" CausesValidation="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnObjection_Click" Text=" "
                                                        ToolTip="اعتراضات">
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/Paper2.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>--%>


                                                <td width="10px" align="center">
                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSendNextStep1" runat="server" AutoPostBack="False" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False"
                                                        Text=" " ToolTip="گردش کار" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
	ShowWf();
}
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/reload.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <%-- <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTracing2" runat="server" AutoPostBack="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnTracing_Click" Text=" "
                                                        ToolTip="پیگیری" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/Cheque Status ReChange.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>--%>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanelMenu>

        <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="gridview" SessionName="SendBackDataTable_PP"
            OnCallback="WFUserControl_Callback" />
        <asp:ObjectDataSource ID="OdbPeriod" runat="server" TypeName="TSP.DataManager.TrainingTeachersManager"
            SelectMethod="SelectPeriodTeachers" FilterExpression="InsId={0}">
            <FilterParameters>
                <asp:Parameter Name="newparameter" />
            </FilterParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="TeId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="PkId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceRequest" runat="server" SelectMethod="FindByPeriodId"
            TypeName="TSP.DataManager.PeriodPresentRequestManager" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="-1" Name="PPId" SessionField="PPId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="InstitueId" runat="server" Visible="False"></asp:HiddenField>

        <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkCode"
            TypeName="TSP.DataManager.WorkFlowTaskManager">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>
