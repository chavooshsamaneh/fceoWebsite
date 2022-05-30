<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Region.aspx.cs" Inherits="Employee_Management_Region"
    Title="مدیریت مناطق ماده 27" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]
                </div>
                  <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


 
                                    <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                        width="100%">
                                        <tr>
                                            <td style="vertical-align: top; text-align: right">
                                                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                    <tr>
                                                        <td style="width: 30px">
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" AutoPostBack="False" CausesValidation="False"
                                                                EnableTheming="False" EnableViewState="False"
                                                                Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                                                <ClientSideEvents Click="function(s, e) {
	grid.AddNewRow();
}" />
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" AutoPostBack="False" CausesValidation="False"
                                                                EnableTheming="False" EnableViewState="False"
                                                                Text=" " ToolTip="ویرایش" Width="25px" UseSubmitBehavior="False">
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
}
" />
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                                                EnableTheming="False" EnableViewState="False"
                                                                Height="25px" OnClick="btnDelete_Click" Text=" " ToolTip="حذف" UseSubmitBehavior="False">
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
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                    ClientInstanceName="grid"   Width="500px"
                    DataSourceID="ObjectDataSource1" EnableViewState="False" KeyFieldName="ReId"
                    OnRowInserting="CustomAspxDevGridView1_RowInserting" OnRowUpdating="CustomAspxDevGridView1_RowUpdating">
                     
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="ReId" Name="ReId" Visible="False" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام منطقه" FieldName="Name" Name="Name" VisibleIndex="0">
                            <PropertiesTextEdit Width="100px">
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                    <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="امتیاز" FieldName="Score" Name="Score" VisibleIndex="1">
                            <PropertiesTextEdit Width="100px">
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                    <RequiredField IsRequired="True" ErrorText="امتیاز را وارد نمایید"></RequiredField>
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="9" ShowClearFilterButton="true"> 
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="320px" />
                    
                </TSPControls:CustomAspxDevGridView>
                <br />   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                                                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server" CausesValidation="False"
                                                                EnableTheming="False" EnableViewState="False" Text=" "
                                                                ToolTip="جدید" UseSubmitBehavior="False">
                                                                <ClientSideEvents Click="function(s, e) {
	grid.AddNewRow();
}" />
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                </HoverStyle>
                                                                <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" CausesValidation="False"
                                                                EnableTheming="False" EnableViewState="False" Text=" "
                                                                ToolTip="ویرایش" Width="25px" UseSubmitBehavior="False">
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
                                                        <td>
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
                                                </table>
                                           </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
            TypeName="TSP.DataManager.RegionManager" OnInserting="ObjectDataSource1_Inserting"
            OnUpdating="ObjectDataSource1_Updating"></asp:ObjectDataSource>

        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                <img align="middle" src="../../Image/indicator.gif" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
 
</asp:Content>
