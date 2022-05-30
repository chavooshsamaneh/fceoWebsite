<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="InstitueTeachers.aspx.cs" Inherits="Institue_Amoozesh_InstitueTeachers"
    Title="مدیریت اساتید" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>




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
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnNew_Click" Text=" "
                                            ToolTip="جدید" UseSubmitBehavior="False" Width="25px">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                            <image height="25px" url="~/Images/icons/new.png" width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                            ToolTip="ویرایش" UseSubmitBehavior="False" Width="25px">
                                            <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                            <image height="25px" url="~/Images/icons/edit.png" width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                            ToolTip="مشاهده" UseSubmitBehavior="False" Width="25px">
                                            <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                            <image height="25px" url="~/Images/icons/view.png" width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnActive2" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnActive_Click" Text=" "
                                            ToolTip="فعال" UseSubmitBehavior="False" Width="25px">
                                            <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
	else
	 e.processOnServer= confirm('آیا مطمئن به  فعال کردن این ردیف هستید؟');
}" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                            <image height="25px" url="~/Images/icons/acceptResignation.png" width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDisActive" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnDisActive_Click" Text=" "
                                            ToolTip="غیرفعال" UseSubmitBehavior="False" Width="25px">
                                            <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                            <image height="25px" url="~/Images/icons/disactive.png" width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewInsTeacher" runat="server" AutoGenerateColumns="False"
                DataSourceID="ObjdsTeacherInstitue"
                ClientInstanceName="gridview" EnableViewState="False" Width="100%" KeyFieldName="InsTeacherId"
                OnHtmlDataCellPrepared="GridViewInsTeacher_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="GridViewInsTeacher_AutoFilterCellEditorInitialize">

                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="Name" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="Family" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شماره همراه" FieldName="MobileNo" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ شروع همکاری" FieldName="StartDate" VisibleIndex="3"
                        Width="100px">
                        <CellStyle Wrap="false">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان" FieldName="EndDate" VisibleIndex="3"
                        Width="100px" Visible="false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="4"
                        Width="150px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="5">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="InActive" Visible="false" VisibleIndex="5">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="6" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>

            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew2" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnNew_Click" Text=" "
                                            ToolTip="جدید" UseSubmitBehavior="False" Width="25px">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                            <image height="25px" url="~/Images/icons/new.png" width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                            ToolTip="ویرایش" UseSubmitBehavior="False" Width="25px">
                                            <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                            <image height="25px" url="~/Images/icons/edit.png" width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td dir="ltr">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                            ToolTip="مشاهده" UseSubmitBehavior="False" Width="25px">
                                            <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                            <image height="25px" url="~/Images/icons/view.png" width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnActive" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnActive_Click" Text=" "
                                            ToolTip="فعال" UseSubmitBehavior="False" Width="25px">
                                            <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
	else
	 e.processOnServer= confirm('آیا مطمئن به  فعال کردن این ردیف هستید؟');
}" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                            <image height="25px" url="~/Images/icons/acceptResignation.png" width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDisActive2" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnDisActive_Click" Text=" "
                                            ToolTip="غیرفعال" UseSubmitBehavior="False" Width="25px">
                                            <ClientSideEvents Click="function(s, e) {
	if (gridview.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
	else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}" />
                                            <hoverstyle backcolor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </hoverstyle>
                                            <image height="25px" url="~/Images/icons/disactive.png" width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField ID="HiddenFieldInstitueTeacher" runat="server">
            </dxhf:ASPxHiddenField>

            <asp:ObjectDataSource ID="ObjdsTeacherInstitue" runat="server" TypeName="TSP.DataManager.InstitueTeachersManager"
                SelectMethod="SelectByInstitue" FilterExpression="InsId={0}" OldValuesParameterFormatString="original_{0}">
                <FilterParameters>
                    <asp:Parameter Name="newparameter" />
                </FilterParameters>
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="InsId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="InActive" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:HiddenField ID="InstitueId" runat="server" Visible="False"></asp:HiddenField>
            <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkId"
                TypeName="TSP.DataManager.WorkFlowTaskManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="WorkFlowId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="TaskOrder" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
   
        </ContentTemplate>
    </asp:UpdatePanel>
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
