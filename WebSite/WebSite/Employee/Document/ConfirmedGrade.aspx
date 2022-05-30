<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ConfirmedGrade.aspx.cs" Inherits="Employee_Document_ConfirmedGrade"
    Title="مدیریت پایه های تایید شده" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;

        }
    </script>
 
        <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
            [<a class="closeLink" href="#">بستن</a>]
        </div>

        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>



                    <table >
                        <tbody>
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                        ID="BtnNew"  OnClick="BtnNew_Click" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False">
                                        <ClientSideEvents Click="function(s, e) {
	//GridViewGradeMjResponsibility.AddNewRow();
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/new.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                        Width="25px" ID="btnEdit" OnClick="btnEdit_Click" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False">
                                        <ClientSideEvents Click="function(s, e) {
			if (GridViewGradeMjResponsibility.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
//else
//{
//	GridViewGradeMjResponsibility.StartEditRow(GridViewGradeMjResponsibility.GetFocusedRowIndex());
//}	
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/edit.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                        ID="btnDelete" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" AutoPostBack="False">
                                        <ClientSideEvents Click="function(s, e) {
		if (GridViewGradeMjResponsibility.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	if(confirm('آیا مطمئن به حذف این ردیف هستید؟'))
{
	GridViewGradeMjResponsibility.PerformCallback('Delete');
}
}
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/delete.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال"
                                        ID="btnInActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" AutoPostBack="False">
                                        <ClientSideEvents Click="function(s, e) {
		if (GridViewGradeMjResponsibility.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	if(confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟'))
{
	GridViewGradeMjResponsibility.PerformCallback('InActive');
}
}
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/disactive.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td style="width: 30px">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="فعال"
                                        ID="btnActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" AutoPostBack="False">
                                        <ClientSideEvents Click="function(s, e) {
		if (GridViewGradeMjResponsibility.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
if(confirm('آیا مطمئن به فعال کردن این ردیف هستید؟'))
{
	GridViewGradeMjResponsibility.PerformCallback('Active');
}
}
}"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/Active.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                </td>
                                <td >
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                        ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                        OnClick="btnExportExcel_Click">
                                        <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                        </HoverStyle>
                                        <Image  Url="~/Images/icons/ExportExcel.png">
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

    <dx:ASPxGridViewExporter ID="GridViewExporter" GridViewID="GridViewConfirmedGrade"
        runat="server">
    </dx:ASPxGridViewExporter>
    <TSPControls:CustomAspxDevGridView ID="GridViewConfirmedGrade" runat="server" DataSourceID="ObjdsAcceptedGrade"
        Width="100%"  
        KeyFieldName="GMRId" AutoGenerateColumns="False"
        ClientInstanceName="GridViewGradeMjResponsibility" OnRowInserting="GridViewGradeMjResponsibility_RowInserting"
        OnRowUpdating="GridViewGradeMjResponsibility_RowUpdating" RightToLeft="True"
        OnCustomCallback="GridViewConfirmedGrade_CustomCallback">
        <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True" ColumnResizeMode="NextColumn"></SettingsBehavior>
        <ClientSideEvents EndCallback="function(s,e){
            if(s.cpError==1)
            {
                ShowMessage(s.cpErrorMsg);
                s.cpError=0;
            }
            }" />

        <Columns>
            <dxwgv:GridViewDataComboBoxColumn Caption="پایه" Width="120px" FieldName="GrdId"
                VisibleIndex="1">
                <PropertiesComboBox ValueType="System.String" TextField="GrdName" DataSourceID="ObjdsGrade"
                    ValueField="GrdId">
                </PropertiesComboBox>
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataComboBoxColumn Caption="صلاحیت" Width="120px" FieldName="ResId"
                VisibleIndex="2">
                <PropertiesComboBox ValueType="System.String" TextField="ResName" DataSourceID="ObjdsResponsibilityType"
                    ValueField="ResId">
                </PropertiesComboBox>
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataComboBoxColumn Caption="رشته" Width="320px" FieldName="MjId" VisibleIndex="3">
                <PropertiesComboBox ValueType="System.String" TextField="MJName" DataSourceID="ObjdsMajor"
                    ValueField="MjId">
                </PropertiesComboBox>
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد رشته" FieldName="MjCode" VisibleIndex="4"
                Width="120px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>

            <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActives" VisibleIndex="5"
                Width="120px">
                <CellStyle Wrap="False" HorizontalAlign="Center">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn VisibleIndex="6" Caption=" " Width="50px"  ShowClearFilterButton="true">
              
            </dxwgv:GridViewCommandColumn>
        </Columns>

        <Settings ShowHorizontalScrollBar="True"></Settings>
    </TSPControls:CustomAspxDevGridView>
    <br />

    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>



                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                    cellpadding="0">
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                    ID="btnNew2"  UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False"  OnClick="BtnNew_Click">
                                    <ClientSideEvents Click="function(s, e) {
	//GridViewGradeMjResponsibility.AddNewRow();
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image  Url="~/Images/icons/new.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                    Width="25px" ID="btnEdit2" OnClick="btnEdit_Click"  UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False">
                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewGradeMjResponsibility.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
//else
//{
	//GridViewGradeMjResponsibility.StartEditRow(GridViewGradeMjResponsibility.GetFocusedRowIndex());
//}	
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image  Url="~/Images/icons/edit.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                    ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) {
		if (GridViewGradeMjResponsibility.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	if(confirm('آیا مطمئن به حذف این ردیف هستید؟'))
{
	GridViewGradeMjResponsibility.PerformCallback('Delete');
}
}
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image  Url="~/Images/icons/delete.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال"
                                    ID="btnInActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) {
		if (GridViewGradeMjResponsibility.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	if(confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟'))
{
	GridViewGradeMjResponsibility.PerformCallback('InActive');
}
}
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image  Url="~/Images/icons/disactive.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="فعال"
                                    ID="btnActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) {
		if (GridViewGradeMjResponsibility.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	if(confirm('آیا مطمئن به فعال کردن این ردیف هستید؟'))
{
	GridViewGradeMjResponsibility.PerformCallback('Active');
}
}
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image  Url="~/Images/icons/Active.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                    ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnExportExcel_Click">
                                    <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                    </HoverStyle>
                                    <Image  Url="~/Images/icons/ExportExcel.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>

    <asp:ObjectDataSource ID="ObjdsGrade" runat="server"
        SelectMethod="GetData" TypeName="TSP.DataManager.GradeManager" UpdateMethod="Update"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsMajor" runat="server"
        SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsResponsibilityType" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.ResponcibilityTypeManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjdsAcceptedGrade" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.DocAcceptedGradeManager"></asp:ObjectDataSource>

</asp:Content>
