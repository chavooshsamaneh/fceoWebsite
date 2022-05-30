<%@ Page Title="تنظیمات گروه ساختمانی و پایه ناظران" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="StructureGroupsAndObserverGrade.aspx.cs" Inherits="Employee_TechnicalServices_BaseInfo_StructureGroupsAndObserverGrade" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxnc" %>
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
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
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
    </script>
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
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {


	if (GridViewStructureGroups.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
{
	GridViewStructureGroups.StartEditRow(GridViewStructureGroups.GetFocusedRowIndex());
}
}
"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
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
            <TSPControls:CustomAspxDevGridView ID="GridViewStructureGroups" Width="100%" runat="server"
                DataSourceID="ObjdsStructureGroups"
                AutoGenerateColumns="False" KeyFieldName="GroupId"
                OnRowUpdating="GridViewStructureGroups_RowUpdating" ClientInstanceName="GridViewStructureGroups"
                EnableViewState="False">
                <ClientSideEvents EndCallback="function(s, e) {
	if(GridViewStructureGroups.cpError==1)
	{
		SetTaskOrderError(GridViewStructureGroups.cpMsg);
		GridViewStructureGroups.cpError=2;
	}
}"
                    BeginCallback="function(s, e) {
	GridViewStructureGroups.cpError=2;
}"></ClientSideEvents>

                <Columns>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="ReCitId"
                        Name="ReCitId">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="GroupName" Caption="گروه ساختمانی"
                        Name="GroupName" VisibleIndex="0">
                        <EditFormSettings Visible="False" />
                   
                    </dxwgv:GridViewDataTextColumn>

                    <dxuc:GridViewDataCheckColumn FieldName="Grade1" Caption="پایه یک"
                        Name="Grade1" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxuc:GridViewDataCheckColumn>

                    <dxuc:GridViewDataCheckColumn FieldName="Grade2" Caption="پایه دو"
                        Name="Grade1" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxuc:GridViewDataCheckColumn>

                    <dxuc:GridViewDataCheckColumn FieldName="Grade3" Caption="پایه سه"
                        Name="Grade1" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxuc:GridViewDataCheckColumn>

                    <dxuc:GridViewDataCheckColumn FieldName="Grade4" Caption="پایه ارشد"
                        Name="Grade1" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxuc:GridViewDataCheckColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " Visible="true" VisibleIndex="3" Width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <SettingsEditing EditFormColumnCount="1" PopupEditFormModal="True" Mode="PopupEditForm">
                </SettingsEditing>

            </TSPControls:CustomAspxDevGridView>

            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                     <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {


	if (GridViewStructureGroups.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
{
	GridViewStructureGroups.StartEditRow(GridViewStructureGroups.GetFocusedRowIndex());
}
}
"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ObjectDataSource ID="ObjdsStructureGroups" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.TechnicalServices.StructureGroupsManager"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

