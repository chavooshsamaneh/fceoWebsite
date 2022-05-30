<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Language.aspx.cs" Inherits="Employee_Management_Language"
    Title="مدیریت زبان ها" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divcontent" style="width: 100%" align="center">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div
                    style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]
                </div>
                 <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                                    <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                        width="100%">
                                        <tbody>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" AutoPostBack="False" CausesValidation="False"
                                                                        EnableClientSideAPI="True" EnableTheming="False"
                                                                        EnableViewState="False" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                                                        <ClientSideEvents Click="function(s, e) {
	grid.AddNewRow();
}" />
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" AutoPostBack="False" CausesValidation="False"
                                                                        EnableClientSideAPI="True" EnableTheming="False"
                                                                        EnableViewState="False" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False" Width="25px">
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
}" />
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                                                        EnableTheming="False" EnableViewState="False"
                                                                        OnClick="btnDelete_Click" Text=" " ToolTip="حذف" UseSubmitBehavior="False">
                                                                        <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');

}" />
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
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
                <TSPControls:CustomAspxDevGridView ID="Customaspxdevgridview1" runat="server" AutoGenerateColumns="False"
                    ClientInstanceName="grid"  
                    Width="100%" DataSourceID="ObjectDataSource1" EnableViewState="False" KeyFieldName="LanId"
                    OnRowInserting="Customaspxdevgridview1_RowInserting" OnRowUpdating="Customaspxdevgridview1_RowUpdating">
                     
                    <Columns>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="LanName" Caption="نام زبان"
                            Name="LanName">
                            <EditCellStyle HorizontalAlign="Right">
                            </EditCellStyle>
                            <PropertiesTextEdit Width="150px">
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                    <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataMemoColumn VisibleIndex="1" FieldName="Description" Caption="توضیحات"
                            Name="Description">
                            <PropertiesMemoEdit Width="250px">
                            </PropertiesMemoEdit>
                            <EditCellStyle HorizontalAlign="Right">
                            </EditCellStyle>
                        </dxwgv:GridViewDataMemoColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " Visible="true" VisibleIndex="3" Width="30px" ShowClearFilterButton="true">
                   
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <SettingsEditing EditFormColumnCount="1" PopupEditFormHorizontalAlign="WindowCenter"
                        PopupEditFormModal="True" PopupEditFormVerticalAlign="WindowCenter" Mode="PopupEditForm">
                    </SettingsEditing>
                     
                </TSPControls:CustomAspxDevGridView>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                                    <table cellpadding="0" dir="rtl" style="display: block; overflow: hidden; border-collapse: collapse"
                                        width="100%">
                                        <tbody>
                                            <tr>
                                                <td dir="ltr" align="right" valign="top">
                                                    <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew2" runat="server" AutoPostBack="False" CausesValidation="False"
                                                                        EnableClientSideAPI="True" EnableTheming="False"
                                                                        EnableViewState="False" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                                                        <ClientSideEvents Click="function(s, e) {
	grid.AddNewRow();
}" />
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" AutoPostBack="False" CausesValidation="False"
                                                                        EnableClientSideAPI="True" EnableTheming="False"
                                                                        EnableViewState="False" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False" Width="25px">
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
}" />
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                                                        EnableTheming="False" EnableViewState="False"
                                                                        OnClick="btnDelete_Click" Text=" " ToolTip="حذف" UseSubmitBehavior="False">
                                                                        <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');

}" />
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
            AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
            TypeName="TSP.DataManager.LanguageManager" ConflictDetection="CompareAllValues"
            OldValuesParameterFormatString="original_{0}" DeleteMethod="Delete" OnUpdating="ObjectDataSource1_Updating"
            UpdateMethod="Update" OnDeleting="ObjectDataSource1_Deleting" InsertMethod="Insert"
            OnInserting="ObjectDataSource1_Inserting">
            
        </asp:ObjectDataSource>
 
</asp:Content>
