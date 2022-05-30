<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="WorkFlowTask.aspx.cs" Inherits="Employee_WorkFlow_WorkFlowTask"
    Title="مدیریت گردش کار" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetTaskOrderError(result) {
            //alert(result);
            if (result != null) {

                document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
                document.getElementById("<%=DivReport.ClientID%>").style.display = 'block';  //='visible';
                document.getElementById('<%=LabelWarning.ClientID%>').innerText = result;
            }

        }

        function SetDivVisible() {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'hidden';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'none';
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 100%" dir="rtl" align="center">
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" Width="25px" EnableTheming="False"
                                                OnClick="btnEdit_Click">
                                                <ClientSideEvents Click="function(s, e) {
                                                    if (GridViewWorkFlowTask.GetFocusedRowIndex()&lt;0)
 	                                                    {
   		                                                    e.processOnServer=false;
   		                                                    alert(&quot;ردیفی انتخاب نشده است&quot;);
 	                                                    }
	                                                    //else
	                                                  //  {
		                                              //      GridViewWorkFlowTask.StartEditRow(GridViewWorkFlowTask.GetFocusedRowIndex());
	                                                   // }
                                                    }
                                                        "></ClientSideEvents>

                                                <Image Width="25px" Height="25px" Url="~/Images/icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                ID="btnView" UseSubmitBehavior="False" EnableViewState="False" Width="25px" EnableTheming="False"
                                                OnClick="btnView_Click">
                                                <ClientSideEvents Click="function(s, e) {
                                                    if (GridViewWorkFlowTask.GetFocusedRowIndex()&lt;0)
 	                                                    {
   		                                                    e.processOnServer=false;
   		                                                    alert(&quot;ردیفی انتخاب نشده است&quot;);
 	                                                    }
                                                    }
                                                        "></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="../../Images/icons/view.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مسئول انجام عملیات"
                                                ID="btnTaskDoer" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnTaskDoer_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="~/Images/TaskDoer.png">
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
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelSearch" HeaderText="جستجو" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="گردش کار" Width="100%" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%" IncrementalFilteringMode="StartsWith"
                                                TextField="WorkFlowName" ID="cmbWorkFlow"
                                                DataSourceID="ObjdsWorkFlow" ValueType="System.String" ValueField="WorkFlowId"
                                                ClientInstanceName="cmbWorkFlow"
                                                EnableIncrementalFiltering="True" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td dir="ltr" valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="سمت" ID="ASPxLabel3" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%" IncrementalFilteringMode="Contains"
                                                TextField="FullName" ID="cmbNezamChart" DataSourceID="ObjdsNezamChart"
                                                ValueType="System.String" ValueField="NcId"
                                                EnableIncrementalFiltering="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="center" colspan="4">
                                            <br />
                                            <TSPControls:CustomAspxButton runat="server" Text="جستجو"
                                                ID="btnSearch" AutoPostBack="False">
                                                <ClientSideEvents Click="function(s, e) {
GridViewWorkFlowTask.cpError=1;
GridViewWorkFlowTask.PerformCallback('');
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomAspxDevGridView ID="GridViewWorkFlowTask" runat="server" DataSourceID="ObjdsWorkFlowTask"
                    Width="100%"
                    OnCustomDataCallback="GridViewWorkFlowTask_CustomDataCallback" KeyFieldName="TaskId"
                    OnRowUpdating="GridViewWorkFlowTask_RowUpdating" EnableViewState="False" ClientInstanceName="GridViewWorkFlowTask"
                    AutoGenerateColumns="False" OnStartRowEditing="GridViewWorkFlowTask_StartRowEditing"
                    OnRowValidating="GridViewWorkFlowTask_RowValidating" OnCustomCallback="GridViewWorkFlowTask_CustomCallback"
                    RightToLeft="True">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	if(GridViewWorkFlowTask.cpIsReturn!=1)
	{
		GridViewWorkFlowTask.cpSelectedIndex=GridViewWorkFlowTask.GetFocusedRowIndex();
			
	}
	else
	{
		GridViewWorkFlowTask.cpIsReturn=0;	
	}
}"
                        EndCallback="function(s, e) {
if( GridViewWorkFlowTask.cpError==1)
	SetDivVisible();
}"
                        CustomButtonClick="function(s, e) {
    GridViewWorkFlowTask.cpError=0;
	e.processOnServer=false;
	GridViewWorkFlowTask.GetValuesOnCustomCallback(e.visibleIndex + ';'+e.buttonID,SetTaskOrderError);
GridViewWorkFlowTask.PerformCallback();
	
}"
                        DetailRowExpanding="function(s, e) {
	if(GridViewWorkFlowTask.cpIsReturn!=1)
	{
		GridViewWorkFlowTask.cpSelectedIndex=GridViewWorkFlowTask.GetFocusedRowIndex();
			
	}
	else
	{
		GridViewWorkFlowTask.cpIsReturn=0;	
	}				
		GridViewWorkFlowTask.SetFocusedRowIndex(GridViewWorkFlowTask.cpSelectedIndex);

	
}"></ClientSideEvents>
                    <Templates>
                        <DetailRow>
                            <div align="center">
                                <TSPControls:CustomAspxDevGridView ID="GridViewTaskDoer" runat="server" AutoGenerateColumns="False"
                                    ClientInstanceName="GridViewTaskDoer"
                                    DataSourceID="ObjdsTaskDoer" EnableViewState="False" KeyFieldName="DoerId"
                                    OnBeforePerformDataSelect="GridViewTaskDoer_BeforePerformDataSelect" Width="100%">
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn Caption="اولویت" FieldName="DoerOrder" VisibleIndex="0">
                                            <CellStyle Font-Size="8pt">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataComboBoxColumn Caption="سمت" FieldName="NcName" VisibleIndex="1">
                                            <PropertiesComboBox DataSourceID="ObjdsNezamChart" TextField="NcName" ValueField="NcId"
                                                ValueType="System.String">
                                            </PropertiesComboBox>
                                            <CellStyle Font-Size="8pt" HorizontalAlign="Right" Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataComboBoxColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="2">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="3">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="4" ShowClearFilterButton="true">
                                        </dxwgv:GridViewCommandColumn>
                                    </Columns>
                                    <SettingsDetail IsDetailGrid="True" />
                                </TSPControls:CustomAspxDevGridView>
                            </div>
                        </DetailRow>
                    </Templates>
                    <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                    <Columns>

                        <dxwgv:GridViewDataBinaryImageColumn Caption="" FieldName="" Name="ImageURL"
                            VisibleIndex="0" Width="40px">
                            <DataItemTemplate>
                                <div align="center">
                                    <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("ImageURL") %>'>
                                    </dxe:ASPxImage>
                                </div>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataBinaryImageColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" Width="10px" Caption="ردیف">
                            <EditFormSettings Visible="False"></EditFormSettings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="WorkFlowName" Width="300px" Caption="عنوان گردش كار">
                            <EditFormSettings Visible="False"></EditFormSettings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="TaskName" Caption="مرحله گردش کار" Width="300px">
                            <EditFormSettings Visible="False"></EditFormSettings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="SmsBody" Caption="متن پیامک ورود به مرحله" Width="300px">
                            <EditFormSettings Visible="False"></EditFormSettings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataCheckColumn VisibleIndex="1" FieldName="IsSmsSend" Caption="امکان اننخاب ارسال همزمان پیامک" Width="300px">
                            <EditFormSettings Visible="False"></EditFormSettings>
                        </dxwgv:GridViewDataCheckColumn>
                        
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="TaskOrder" Width="80px"
                            Caption="اولویت">
                            <PropertiesTextEdit Width="150px">
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                    <RequiredField IsRequired="True" ErrorText="اولویت انجام عملیات را وارد نمایید"></RequiredField>
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <CellStyle HorizontalAlign="Center"></CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="2" FieldName="IsVital">
                            <EditFormSettings Visible="False"></EditFormSettings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="WorkFlowId">
                            <EditFormSettings Visible="False"></EditFormSettings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="TaskCode">
                            <EditFormSettings Visible="False"></EditFormSettings>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="4" Width="80px" Caption="تغییر اولویت"
                            ButtonType="Image" Name="OrderChange">
                            <CustomButtons>
                                <dxwgv:GridViewCommandColumnCustomButton ID="Up" Text="اولویت بالاتر">
                                    <Image Width="16px" Height="16px" Url="~/Images/icons/up-32.png">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                                <dxwgv:GridViewCommandColumnCustomButton ID="Down" Text="اولویت پایین تر">
                                    <Image Width="16px" Height="16px" Url="~/Images/icons/down-32.png">
                                    </Image>
                                </dxwgv:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="4" Width="25px" Caption=" " ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>

                    <SettingsEditing EditFormColumnCount="1" PopupEditFormModal="True" Mode="PopupEditForm">
                    </SettingsEditing>
                    <Settings ShowHorizontalScrollBar="true"></Settings>
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
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False" Width="25px"
                                                EnableTheming="False" OnClick="btnEdit_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (GridViewWorkFlowTask.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	//else
//	{
//		GridViewWorkFlowTask.StartEditRow(GridViewWorkFlowTask.GetFocusedRowIndex());
//	}
	
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="~/Images/icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" Width="25px"
                                                EnableTheming="False" OnClick="btnView_Click">
                                                <ClientSideEvents Click="function(s, e) {
                                                    if (GridViewWorkFlowTask.GetFocusedRowIndex()&lt;0)
 	                                                    {
   		                                                    e.processOnServer=false;
   		                                                    alert(&quot;ردیفی انتخاب نشده است&quot;);
 	                                                    }
                                                    }
                                                        "></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="../../Images/icons/view.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مسئول انجام عملیات"
                                                ID="btnTaskDoer2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnTaskDoer_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="~/Images/TaskDoer.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ObjectDataSource ID="ObjdsWorkFlow" runat="server" OldValuesParameterFormatString="original_{0}"
                    TypeName="TSP.DataManager.WorkFlowManager" SelectMethod="GetData"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" OldValuesParameterFormatString="original_{0}"
                    TypeName="TSP.DataManager.WorkFlowTaskManager" SelectMethod="SelectByWorkId">
                    <SelectParameters>
                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="WorkFlowId"></asp:Parameter>
                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="TaskOrder"></asp:Parameter>
                        <asp:Parameter DefaultValue="-1" Name="NcId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="ShowEndProcess" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjdsNezamChart" runat="server" OldValuesParameterFormatString="original_{0}"
                    TypeName="TSP.DataManager.NezamChartManager" SelectMethod="GetData"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjdsTaskDoer" runat="server" OldValuesParameterFormatString="original_{0}"
                    TypeName="TSP.DataManager.TaskDoerManager" SelectMethod="FindByDoerId">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="-1" Name="TaskId" SessionField="TaskId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="DoerId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="EmpId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="UltId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="NmcId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img align="middle" src="../../Image/indicator.gif" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>

</asp:Content>
