<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="Groups.aspx.cs" Inherits="Employee_Groups" Title="گروه ها" %>

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
    <div id="divcontent" style="width: 100%;" align="center" dir="rtl">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>

                            <table>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                            EnableTheming="False" EnableViewState="False"
                                            OnClick="BtnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/new.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="width: 7px">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                            EnableTheming="False" EnableViewState="False"
                                            OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/edit.png" />
                                            <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }

}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                            EnableTheming="False" EnableViewState="False"
                                            OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/view.png" />
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                            EnableTheming="False" EnableViewState="False"
                                            OnClick="btnDelete_Click" ToolTip="حذف" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/delete.png" />
                                            <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
 else
  e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');

}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                    Width="100%" DataSourceID="ObjectDataSource1" KeyFieldName="GrId"
                    ClientInstanceName="grid" OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared"
                    OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize"
                    RightToLeft="True">
                    <Columns>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="3" Width="50px" ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn> 
                        <dxwgv:GridViewDataTextColumn  FieldName="GrId" Caption="کد گروه"
                            VisibleIndex="0">   </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewDataTextColumn Caption="نام گروه" FieldName="GrName" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                       
                        <dxwgv:GridViewDataTextColumn Caption="UserId" FieldName="UserId" Visible="False"
                            VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ ایجاد" VisibleIndex="2"
                            FieldName="CreateDate">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="3"
                            Visible="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ایجاد کننده" FieldName="EmpName" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowGroupPanel="True" ShowFilterRow="True" />
                </TSPControls:CustomAspxDevGridView>

                <br />
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete"
                    InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
                    TypeName="TSP.DataManager.GroupManager"></asp:ObjectDataSource>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew2" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                            EnableTheming="False" EnableViewState="False"
                                            OnClick="BtnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/new.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="width: 7px">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                            EnableTheming="False" EnableViewState="False"
                                            OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/edit.png" />
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                            EnableTheming="False" EnableViewState="False"
                                            OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/view.png" />
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                            EnableTheming="False" EnableViewState="False"
                                            OnClick="btnDelete_Click" ToolTip="حذف" UseSubmitBehavior="False">

                                            <Image Url="~/Images/icons/delete.png" />
                                            <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
 else
  e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');

}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>

                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img align="middle" src="../Image/indicator.gif" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
</asp:Content>
