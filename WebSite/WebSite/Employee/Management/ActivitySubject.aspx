<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ActivitySubject.aspx.cs" Inherits="Employee_Management_ActivitySubject"
    Title="مدیریت زمینه های فعالیت" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1" Namespace="DevExpress.Web"
    TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1" Namespace="DevExpress.Web"
    TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>

<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]</div>
                   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxrp:PanelContent>

                            
  
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
                                                                            <clientsideevents click="function(s, e) {
	grid.AddNewRow();
}" />
                                                                            <hoverstyle backcolor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                </hoverstyle>
                                                                            <image height="25px" url="~/Images/icons/new.png" width="25px" />
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td >
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" AutoPostBack="False" CausesValidation="False"
                                                                            EnableClientSideAPI="True" EnableTheming="False"
                                                                            EnableViewState="False" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False" Width="25px">
                                                                            <clientsideevents click="function(s, e) {
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
                                                                            <hoverstyle backcolor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                </hoverstyle>
                                                                            <image height="25px" url="~/Images/icons/edit.png" width="25px" />
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td >
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                                                            EnableTheming="False" EnableViewState="False"
                                                                            OnClick="btnDelete_Click" Text=" " ToolTip="حذف" UseSubmitBehavior="False">
                                                                            <clientsideevents click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');

}" />
                                                                            <hoverstyle backcolor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                </hoverstyle>
                                                                            <image height="25px" url="~/Images/icons/delete.png" width="25px" />
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                  </dxrp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                    <br />
                    <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                        ClientInstanceName="grid"  
                        DataSourceID="ObjectDataSource1" EnableViewState="False" KeyFieldName="AsId"
                        OnRowInserting="CustomAspxDevGridView1_RowInserting" OnRowUpdating="CustomAspxDevGridView1_RowUpdating">
                       
                        <Columns>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="AsName" Caption="نام فعالیت" Name="AsName">
                                <EditCellStyle HorizontalAlign="Right"></EditCellStyle>

                                <PropertiesTextEdit Width="150px">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                        <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                    </ValidationSettings>
                                </PropertiesTextEdit>
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>

                        <SettingsEditing EditFormColumnCount="1" PopupEditFormHorizontalAlign="WindowCenter" PopupEditFormModal="True" PopupEditFormVerticalAlign="WindowCenter" Mode="PopupEditForm"></SettingsEditing>

                       
                    </TSPControls:CustomAspxDevGridView>
                    <br />
                  <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxrp:PanelContent>


  
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
                                                                            <clientsideevents click="function(s, e) {
	grid.AddNewRow();
}" />
                                                                            <hoverstyle backcolor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                </hoverstyle>
                                                                            <image height="25px" url="~/Images/icons/new.png" width="25px" />
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td >
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" AutoPostBack="False" CausesValidation="False"
                                                                            EnableClientSideAPI="True" EnableTheming="False"
                                                                            EnableViewState="False" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False" Width="25px">
                                                                            <clientsideevents click="function(s, e) {
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
                                                                            <hoverstyle backcolor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                </hoverstyle>
                                                                            <image height="25px" url="~/Images/icons/edit.png" width="25px" />
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td >
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                                                            EnableTheming="False" EnableViewState="False"
                                                                            OnClick="btnDelete_Click" Text=" " ToolTip="حذف" UseSubmitBehavior="False">
                                                                            <clientsideevents click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');

}" />
                                                                            <hoverstyle backcolor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                </hoverstyle>
                                                                            <image height="25px" url="~/Images/icons/delete.png" width="25px" />
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                   </dxrp:PanelContent>
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
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
             OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
            TypeName="TSP.DataManager.ActivitySubjectManager" UpdateMethod="Update" OnDeleting="ObjectDataSource1_Deleting"
            OnUpdating="ObjectDataSource1_Updating" OnInserting="ObjectDataSource1_Inserting">
           
        </asp:ObjectDataSource>
</asp:Content>
