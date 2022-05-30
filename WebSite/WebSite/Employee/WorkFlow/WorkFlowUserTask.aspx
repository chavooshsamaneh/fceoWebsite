<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="WorkFlowUserTask.aspx.cs" Inherits="Employee_WorkFlow_WorkFlowUserTask"
    Title="کار تابل گردش کار" %>

<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>

    <script language="javascript" type="text/javascript">
    function ShowMessage(Message)
    {
           document.getElementById("<%=DivReport.ClientID%>").style.visibility='visible';
           document.getElementById("<%=DivReport.ClientID%>").style.display='inline';
           document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
    }
    </script>
        <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
            <asp:label id="LabelWarning" runat="server" text="Label"></asp:label>
            [<a class="closeLink" href="#">بستن</a>]</div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                                                <table >
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="ویرایش"
                                                                    Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="btnEdit_Click" Visible="False">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewWFState.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                                                    
                                                                    <Image  Url="~/Images/icons/edit.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="مشاهده"
                                                                    ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    AutoPostBack="false">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewWFState.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	else
	{
		GridViewWFState.PerformCallback('View');
	}
}"></ClientSideEvents>
                                                                   
                                                                    <Image  Url="~/Images/icons/view.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="حذف درخواست"
                                                                    Width="25px" ID="btnDeleteReq" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnDeleteReq_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewWFState.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                                   
                                                                    <Image  Url="~/Images/icons/delete.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="گردش کار"
                                                                    ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewWFState.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	

   // TextDesc.SetText('');
	//CallbackPanelWorkFlow.PerformCallback('');	
	//PopupWorkFlow.Show();
	ShowWf();
}
}"></ClientSideEvents>
                                                                   
                                                                    <Image  Url="~/Images/icons/reload.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td style="width: 30px">
                                                                <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="پیگیری گردش کار"
                                                                    ID="btnTracing" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnTracing_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
            
	if (GridViewWFState.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                                                   
                                                                    <Image  Url="~/Images/icons/Cheque Status ReChange.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td style="width: 30px">
                                                                <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="نمایش همه"
                                                                    ID="btnShowAllTask" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnShowAllTask_Click">
                                                                   
                                                                    <Image Height="25px" Url="~/Images/AllWFTask.png" Width="25px" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                          </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <TSPControls:CustomAspxDevGridView ID="GridViewWFState" runat="server" DataSourceID="ObjdsWfState"
                    Width="100%"  
                    OnHtmlRowPrepared="GridViewWFState_HtmlRowPrepared" KeyFieldName="StateId" AutoGenerateColumns="False"
                    ClientInstanceName="GridViewWFState" OnCustomCallback="GridViewWFState_CustomCallback"
                    OnFocusedRowChanged="GridViewWFState_FocusedRowChanged" OnHtmlDataCellPrepared="GridViewWFState_HtmlDataCellPrepared" RightToLeft="True">
                    <ClientSideEvents EndCallback="function(s,e){
                    // alert(e.result);
                     //GridViewWFState.GetValuesOnCustomCallback(e.visibleIndex,ShowMessage);
                     //window.open(s.cpURL);
                     //alert(s.cpURL);
                     if(s.cpError==1)
                     {
                        ShowMessage(s.cpErrorMsg);
                        s.cpError=0;
                     }
                     else
                     {
                        if(s.cpReType=='View')
                        {
                            window.open(s.cpURL);
                            s.cpReType='';
                         }
                     }
                    }" />
                  
                    <Columns>                                               
                         <dxwgv:GridViewDataComboBoxColumn Caption=" " FieldName="PriorityId" Name="Priority"
                                            VisibleIndex="0" Width="30px">
                                            <dataitemtemplate>
                        <DIV align=center><dxe:ASPxImage id="btnPriority" runat="server" Width="16px" Height="16px" ImageUrl="~/Images/Secret.png">
                        </dxe:ASPxImage></DIV>
                        
                                
</dataitemtemplate>
                                <propertiescombobox valuetype="System.String" datasourceid="ObjectDataSource_Priority" textfield="PName" valuefield="PId" ></propertiescombobox>
                                        </dxwgv:GridViewDataComboBoxColumn>
                                        
                        <dxwgv:GridViewDataComboBoxColumn Caption="نوع مسئولیت" FieldName="ResType" VisibleIndex="0">
                            <propertiescombobox valuetype="System.String"><Items>
<dxe:ListEditItem Value="اصلی" Text="اصلی"></dxe:ListEditItem>
<dxe:ListEditItem Value="تفویض شده" Text="تفویض شده"></dxe:ListEditItem>
</Items>
</propertiescombobox>
                            <cellstyle horizontalalign="Right" wrap="False"></cellstyle>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataComboBoxColumn Caption="فرآیند" FieldName="WorkFlowId" Name="CmbWF"
                            VisibleIndex="1" Width="150px">
                            <propertiescombobox datasourceid="ObjdsWorkFlow" textfield="WorkFlowName" valuefield="WorkFlowId"
                                valuetype="System.String"></propertiescombobox>
                            <cellstyle horizontalalign="Right"></cellstyle>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataComboBoxColumn Caption="عملیات" FieldName="TaskId" VisibleIndex="2"
                            Width="150px">
                            <propertiescombobox datasourceid="ObjdsWorkFlowTask" textfield="TaskName" valuefield="TaskId"
                                valuetype="System.String"></propertiescombobox>
                            <cellstyle horizontalalign="Right"></cellstyle>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شرح پرونده" FieldName="DocName" VisibleIndex="3">
                            <headerstyle horizontalalign="Center" />
                            <cellstyle wrap="False" horizontalalign="Right"></cellstyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام ارسال کننده" FieldName="SenderName" VisibleIndex="4"
                            Width="100px">
                            <headerstyle wrap="True" />
                            <cellstyle wrap="False" horizontalalign="Right"></cellstyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="سمت ارسال کننده" FieldName="SenderNcName"
                            VisibleIndex="5">
                            <headerstyle wrap="False" />
                            <cellstyle wrap="False" horizontalalign="Right"></cellstyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ دریافت" FieldName="Date" VisibleIndex="6">
                            <cellstyle wrap="False"></cellstyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="زمان دریافت" FieldName="StateTime" VisibleIndex="7">
                            <cellstyle wrap="False"></cellstyle>
                        </dxwgv:GridViewDataTextColumn>
                          <dxwgv:GridViewDataTextColumn Caption="مهلت" FieldName="WFExpDate" VisibleIndex="6">
                            <cellstyle wrap="False"></cellstyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
                            <cellstyle wrap="False"></cellstyle>
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                  
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
                 
                </TSPControls:CustomAspxDevGridView>
                <uc1:WFUserControl ID="WFUserControl" runat="server" OnCallback="WFUserControl_Callback"
                    GridName="GridViewWFState" SessionName="SendBackDataTable_EmpWF" />
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                                                <table >
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="ویرایش"
                                                                    Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnEdit_Click" Visible="False">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewWFState.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                                             
                                                                    <Image  Url="~/Images/icons/edit.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="مشاهده"
                                                                    ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    AutoPostBack="false">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewWFState.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	else
	{
		GridViewWFState.PerformCallback('View');
	}
}"></ClientSideEvents>
                                                                  
                                                                    <Image  Url="~/Images/icons/view.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="حذف درخواست"
                                                                    Width="25px" ID="btnDeleteReq2" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnDeleteReq_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
if (GridViewWFState.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                                 
                                                                    <Image  Url="~/Images/icons/delete.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="گردش کار"
                                                                    ID="btnSendToNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewWFState.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	

   // TextDesc.SetText('');
	//CallbackPanelWorkFlow.PerformCallback('');	
	//PopupWorkFlow.Show();
	ShowWf();
}
}"></ClientSideEvents>
                                                                  
                                                                    <Image  Url="~/Images/icons/reload.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="پیگیری گردش کار"
                                                                    ID="btnTracing2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnTracing_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
     if (GridViewWFState.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                                              
                                                                    <Image  Url="~/Images/icons/Cheque Status ReChange.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="نمایش همه"
                                                                    ID="btnShowAllTask2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnShowAllTask_Click">
                                                             
                                                                    <Image Height="25px" Url="~/Images/AllWFTask.png" Width="25px" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                           </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu> <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldWF">
                                                </dxhf:ASPxHiddenField>
        <asp:objectdatasource id="ObjdsWorkFlowTask" runat="server" typename="TSP.DataManager.WorkFlowTaskManager"
            selectmethod="SelectByWorkId">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="WorkFlowId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="TaskOrder" Type="Int32" />
            </SelectParameters>
        </asp:objectdatasource>
        <asp:objectdatasource id="ObjdsWorkFlow" runat="server" typename="TSP.DataManager.WorkFlowManager"
            selectmethod="GetData"></asp:objectdatasource>
        <asp:objectdatasource id="ObjdsWfState" runat="server" typename="TSP.DataManager.WorkFlowStateManager"
            selectmethod="SelectUserTasks" oldvaluesparameterformatstring="original_{0}">
            <SelectParameters>
                <asp:Parameter Type="Int32" DefaultValue="-1" Name="UserId"></asp:Parameter>
                <asp:Parameter Type="Int32" DefaultValue="-1" Name="WfCode"></asp:Parameter>
                <asp:Parameter DefaultValue="0" Name="ShowAllTask" Type="Int32" />
            </SelectParameters>
        </asp:objectdatasource>
        <asp:ObjectDataSource ID="ObjectDataSource_Priority" runat="server" SelectMethod="GetData"
            TypeName="TSP.DataManager.Automation.PriorityManager"></asp:ObjectDataSource>
    
</asp:Content>
