<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="Partition.aspx.cs" Inherits="Employee_Management_Partition" Title="مدیریت بخش" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" EnableClientSideAPI="True" CausesValidation="False" Text=" " EnableTheming="False" ToolTip="جدید" ID="BtnNew" EnableViewState="False">
                                    <ClientSideEvents Click="function(s, e) {
	grid.AddNewRow();
}"></ClientSideEvents>

                                    <Image Url="~/Images/icons/new.png"></Image>

                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" EnableClientSideAPI="True" CausesValidation="False" Text=" " Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit" EnableViewState="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	grid.StartEditRow(grid.GetFocusedRowIndex());
}	
}"></ClientSideEvents>

                                    <Image Url="~/Images/icons/edit.png"></Image>

                                  
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True" CausesValidation="False" Text=" " EnableTheming="False" ToolTip="غیر فعال" ID="btnInActive" EnableViewState="False" OnClick="btnInActive_Click">
                                    <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');

}"></ClientSideEvents>

                                    <Image Url="~/Images/icons/disactive.png"></Image>

                                   
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%" RightToLeft="True"
        OnRowUpdating="CustomAspxDevGridView1_RowUpdating" OnRowInserting="CustomAspxDevGridView1_RowInserting"
        OnRowValidating="CustomAspxDevGridView1_OnRowValidating"
        KeyFieldName="PartId" EnableViewState="False" ClientInstanceName="grid" AutoGenerateColumns="False">
        <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="300px" PopupEditFormHorizontalAlign="WindowCenter" PopupEditFormVerticalAlign="WindowCenter" PopupEditFormModal="True" EditFormColumnCount="1"></SettingsEditing>
        <Columns>
            <dxwgv:GridViewDataTextColumn FieldName="PartName" Name="PartName" Caption="نام بخش" VisibleIndex="0">
                <PropertiesTextEdit Width="150px">
                    <ValidationSettings ErrorDisplayMode="ImageWithText" Display="Dynamic" ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                    </ValidationSettings>
                </PropertiesTextEdit>

                <EditCellStyle HorizontalAlign="Right"></EditCellStyle>
                <CellStyle HorizontalAlign="Right"></CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataComboBoxColumn FieldName="AgentId" Caption="نمایندگی" VisibleIndex="1">

                <PropertiesComboBox DataSourceID="ObjdsAgent" TextField="Name" ValueField="AgentId" ValueType="System.String" Width="150px">
                    <ValidationSettings ErrorDisplayMode="ImageWithText" Display="Dynamic" ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="نمایندگی را انتخاب نمایید"></RequiredField>
                    </ValidationSettings>
                </PropertiesComboBox>

                <EditFormSettings Visible="True"></EditFormSettings>
                <CellStyle HorizontalAlign="Right"></CellStyle>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataTextColumn FieldName="ActiveType" Caption="وضعیت" VisibleIndex="2" Width="100px">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="9" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>

        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowGroupPanel="True"></Settings>
    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" EnableClientSideAPI="True" CausesValidation="False" Text=" " EnableTheming="False" ToolTip="جدید" ID="btnNew2" EnableViewState="False">
                                    <ClientSideEvents Click="function(s, e) {
	grid.AddNewRow();
}"></ClientSideEvents>

                                    <Image Url="~/Images/icons/new.png"></Image>

                                
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" EnableClientSideAPI="True" CausesValidation="False" Text=" " Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit2" EnableViewState="False">
                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	grid.StartEditRow(grid.GetFocusedRowIndex());
}	
}"></ClientSideEvents>

                                    <Image Url="~/Images/icons/edit.png"></Image>

                                  
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True" CausesValidation="False" Text=" " EnableTheming="False" ToolTip="غیر فعال" ID="btnInActive1" EnableViewState="False" OnClick="btnInActive_Click">
                                    <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');

}"></ClientSideEvents>

                                    <Image Url="~/Images/icons/disactive.png"></Image>

                                 
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:ObjectDataSource ID="ObjdsAgent" runat="server" TypeName="TSP.DataManager.AccountingAgentManager" SelectMethod="GetData"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete"
        InsertMethod="Insert" SelectMethod="GetData" TypeName="TSP.DataManager.PartitionManager"
        UpdateMethod="Update" OnDeleting="ObjectDataSource1_Deleting" OnUpdating="ObjectDataSource1_Updating"
        OldValuesParameterFormatString="original_{0}" OnInserting="ObjectDataSource1_Inserting"></asp:ObjectDataSource>
</asp:Content>
