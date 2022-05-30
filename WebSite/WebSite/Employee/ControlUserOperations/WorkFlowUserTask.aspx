<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="WorkFlowUserTask.aspx.cs" Inherits="Employee_WorkFlow_WorkFlowUserTask"
    Title="کنترل گردش پرونده ها" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
    
        <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
            <asp:label id="LabelWarning" runat="server" text="Label"></asp:label>
            [<a class="closeLink" href="#">بستن</a>]</div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
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
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                                                        CausesValidation="False" ID="btnPrint2" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" AutoPostBack="false">
                                                                       
                                                                        <Image  Url="~/Images/icons/printers.png">
                                                                        </Image>
                                                                        <ClientSideEvents Click="function(s,e){ GridViewWFState.PerformCallback('Print'); }" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False" 
                                                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                                                        UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                                                      
                                                                        <Image  Url="~/Images/icons/ExportExcel.png"  />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            
                           </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <dx:ASPxGridViewExporter ID="GridViewExporter" GridViewID="GridViewWFState" runat="server">
            </dx:ASPxGridViewExporter>
      
                <TSPControls:CustomAspxDevGridView ID="GridViewWFState" runat="server" Width="100%"
                      OnHtmlRowPrepared="GridViewWFState_HtmlRowPrepared"
                    AutoGenerateColumns="False" ClientInstanceName="GridViewWFState" OnCustomCallback="GridViewWFState_CustomCallback"
                    OnFocusedRowChanged="GridViewWFState_FocusedRowChanged" OnHtmlDataCellPrepared="GridViewWFState_HtmlDataCellPrepared"
                    RightToLeft="True">
                    <ClientSideEvents EndCallback="function(s,e){                  
                     if(s.cpError==1)
                     {
                        //ShowMessage(s.cpErrorMsg);
                        alert(s.cpErrorMsg);
                        s.cpError=0;
                     }
                     else
                     {
                        if(s.cpReType=='View')
                        {
                            window.open(s.cpURL);
                            s.cpReType='';
                         }
                          if(s.cpPrint==1)
                            {
                             s.cpPrint=0;
                                window.open('../../Printdt.aspx');
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
                            <propertiescombobox valuetype="System.String" datasourceid="ObjectDataSource_Priority"
                                textfield="PName" valuefield="PId"></propertiescombobox>
                        </dxwgv:GridViewDataComboBoxColumn>
                      
                        <dxwgv:GridViewDataTextColumn Caption="فرآیند" FieldName="WorkFlowName" VisibleIndex="0">
                            <headerstyle horizontalalign="Center" />
                            <cellstyle wrap="False" horizontalalign="Right"></cellstyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="عملیات" FieldName="TaskName" VisibleIndex="0">
                            <headerstyle horizontalalign="Center" />
                            <cellstyle wrap="False" horizontalalign="Right"></cellstyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شرح پرونده" FieldName="DocName" VisibleIndex="3">
                            <headerstyle horizontalalign="Center" />
                            <cellstyle wrap="False" horizontalalign="Right"></cellstyle>
                        </dxwgv:GridViewDataTextColumn>
                          <dxwgv:GridViewDataTextColumn Caption="نوع مسئولیت" FieldName="ResType" VisibleIndex="3">
                            <headerstyle horizontalalign="Center" />
                            <cellstyle wrap="False" horizontalalign="Right"></cellstyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption=" نام دریافت کننده" FieldName="DoerName" VisibleIndex="3">
                            <headerstyle horizontalalign="Center" wrap="false" />
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
                <br />
           <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                                <table >
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
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
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                                                    CausesValidation="False" ID="btnPrint" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" AutoPostBack="false">
                                                                
                                                                    <Image  Url="~/Images/icons/printers.png">
                                                                    </Image>
                                                                    <ClientSideEvents Click="function(s,e){ GridViewWFState.PerformCallback('Print'); }" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False" 
                                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                                                    UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                                                
                                                                    <Image  Url="~/Images/icons/ExportExcel.png"  />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                           </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                                                <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldWF">
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
        <asp:objectdatasource id="ObjectDataSource_Priority" runat="server" selectmethod="GetData"
            typename="TSP.DataManager.Automation.PriorityManager"></asp:objectdatasource>
  
</asp:Content>
