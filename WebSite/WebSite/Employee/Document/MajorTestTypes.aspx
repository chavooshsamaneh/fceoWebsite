<%@ Page Title="مدیریت زمینه های آزمون براساس رشته" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MajorTestTypes.aspx.cs" Inherits="Employee_Document_MajorTestTypes" %>

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
    <script language="javascript" type="text/javascript">
        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div align="right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]</div>
                     <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


   
                        <table >
                            <tbody>
                                <tr>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" AutoPostBack="False" CausesValidation="False"
                                            EnableClientSideAPI="True"  EnableTheming="False"
                                            EnableViewState="False" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	grid.AddNewRow();
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" AutoPostBack="False" CausesValidation="False"
                                            EnableClientSideAPI="True"  EnableTheming="False"
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
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
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
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                             EnableTheming="False" EnableViewState="False"
                                            OnClick="btnInActive_Click" Text=" " ToolTip="غیرفعال" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');

}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                   </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomAspxDevGridView ID="grdTypes" runat="server" ClientInstanceName="grid"
                DataSourceID="ObjectDataSourceTypes" KeyFieldName="MjTTypeId" OnRowInserting="grdTypes_RowInserting"
                OnRowUpdating="grdTypes_RowUpdating" Width="100%" RightToLeft="True" OnAutoFilterCellEditorInitialize="grdTypes_AutoFilterCellEditorInitialize"
                OnHtmlDataCellPrepared="grdTypes_HtmlDataCellPrepared">
                <Columns>
                    <dxwgv:GridViewDataComboBoxColumn Caption="رشته" Width="38%" FieldName="MjId" VisibleIndex="0">
                        <PropertiesComboBox ValueType="System.String" TextField="MjName" DataSourceID="ObjectDataSourceMajor"
                            ValueField="MjId" Width="260px">
                        </PropertiesComboBox>
                        <CellStyle Wrap="False" HorizontalAlign="Right">
                        </CellStyle>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="زمینه آزمون" Width="38%" FieldName="TTypeId"
                        VisibleIndex="1">
                        <PropertiesComboBox ValueType="System.String" TextField="TTypeName" DataSourceID="ObjectDataSourceTestTypes"
                            ValueField="TTypeId" Width="260px">
                        </PropertiesComboBox>
                        <CellStyle Wrap="False" HorizontalAlign="Right">
                        </CellStyle>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="InActiveName" Caption="وضعیت"
                        Name="InActiveName" EditFormSettings-Visible="False" Width="10%">
                        <CellStyle Wrap="false">
                        </CellStyle>
                        <HeaderStyle HorizontalAlign="Right" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="InActiveDate" Caption="تاریخ غیرفعال"
                        Name="InActiveDate" EditFormSettings-Visible="False" Width="14%">
                        <CellStyle Wrap="false">
                        </CellStyle>
                        <HeaderStyle HorizontalAlign="Right" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " Visible="true" VisibleIndex="2" Width="30px"  ShowClearFilterButton="true">
                    
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <SettingsEditing EditFormColumnCount="1" PopupEditFormHorizontalAlign="WindowCenter"
                    PopupEditFormModal="True" PopupEditFormVerticalAlign="WindowCenter" Mode="PopupEditForm">
                </SettingsEditing>
                <SettingsText PopupEditFormCaption="تغییر رکورد" />
                <ClientSideEvents EndCallback="function(s,e){
                    if(s.cpMessage!='')
                    {
                         ShowMessage(s.cpMessage);
                        s.cpMessage='';
                    }
                }" />
            </TSPControls:CustomAspxDevGridView>
            <br />
                     <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                            <tbody>
                                <tr>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew2" runat="server" AutoPostBack="False" CausesValidation="False"
                                            EnableClientSideAPI="True"  EnableTheming="False"
                                            EnableViewState="False" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	grid.AddNewRow();
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" AutoPostBack="False" CausesValidation="False"
                                            EnableClientSideAPI="True"  EnableTheming="False"
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
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
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
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive2" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                             EnableTheming="False" EnableViewState="False"
                                            OnClick="btnInActive_Click" Text=" " ToolTip="غیرفعال" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');

}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
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
    <asp:ObjectDataSource ID="ObjectDataSourceTypes" runat="server" SelectMethod="GetDataForManage"
        TypeName="TSP.DataManager.DocMajorTestTypeManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceTestTypes" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.DocTestTypeManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceMajor" runat="server" SelectMethod="FindMjParents"
        TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
</asp:Content>
