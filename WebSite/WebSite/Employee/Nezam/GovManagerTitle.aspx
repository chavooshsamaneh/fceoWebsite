<%@ Page Title="مدیریت سمت های استانی/کشوری" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="GovManagerTitle.aspx.cs" Inherits="Employee_Nezam_GovManagerTitle" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls.FormCreatorComponents"
    TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#"><span style="color: #000000">بستن</span></a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
        <PanelCollection>
            <dx:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="false" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    CausesValidation="False" Text=" " EnableTheming="False"
                                    ToolTip="جدید" ID="btnnew" EnableViewState="False">
                                    <ClientSideEvents Click="function(s, e) {
	grid.AddNewRow();
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/new.png">
                                    </Image>

                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="false" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    CausesValidation="False" Text=" " Width="25px"
                                    EnableTheming="False" ToolTip="ویرایش" ID="btnedit" EnableViewState="False">
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
                                    <Image Url="~/Images/icons/edit.png">
                                    </Image>

                                </TSPControls:CustomAspxButton>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btndel" runat="server" EnableTheming="False" AutoPostBack="false"
                                    EnableViewState="False" ToolTip="حذف" OnClick="btndel_Click">
                                    <ClientSideEvents Click="function(s, e) {
                                                if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 {
	 if(confirm('آیا مطمئن به حذف این ردیف هستید؟'))
       grid.PerformCallback('delete');
 }
}" />
                                    <Image Url="~/Images/icons/delete.png">
                                    </Image>

                                </TSPControls:CustomAspxButton>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True" AutoPostBack="false"
                                    CausesValidation="False" Text=" " EnableTheming="False"
                                    ToolTip="غیر فعال" ID="btninactive" EnableViewState="False" OnClick="btnInActive_Click">
                                    <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 {
  if(confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟'))
  grid.PerformCallback('inactive');
  }

}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/disactive.png">
                                    </Image>

                                </TSPControls:CustomAspxButton>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                    ID="btnprint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False">
                                    <ClientSideEvents Click="function(s, e) {	
	grid.PerformCallback('print');
}"></ClientSideEvents>

                                    <Image Url="~/Images/icons/printers.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی Excel"
                                    ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnExportExcel_Click">
                                    <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>

                                    <Image Url="~/Images/icons/ExportExcel.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSPControls:CustomAspxDevGridView ID="GridViewGovManagerTitle" runat="server" AutoGenerateColumns="False"
        ClientInstanceName="grid"
        DataSourceID="ObjectDataSource1" EnableViewState="False" Width="100%" RightToLeft="True"
        KeyFieldName="GmtId" OnRowInserting="GridViewGovManagerTitle_RowInserting" OnRowUpdating="GridViewGovManagerTitle_RowUpdating"
        OnRowValidating="GridViewGovManagerTitle_RowValidating"
        OnCustomCallback="GridViewGovManagerTitle_CustomCallback">
        <ClientSideEvents EndCallback="function(s, e) {
    if(s.cpError==1)
     {
       ShowMessage(s.cpMsg);
       s.cpError=0;
       s.cpMsg = '';
     }
       if(s.cpDoPrint==1)
            {
               s.cpDoPrint = 0;
	           window.open('../../Print.aspx');
            }
}" />

        <Columns>
            <dxwgv:GridViewDataTextColumn Caption="نام" Width="300px" FieldName="GmtName" VisibleIndex="0">
                <PropertiesTextEdit Width="250px">
                    <ValidationSettings ErrorDisplayMode="ImageWithText" Display="Dynamic" ErrorTextPosition="Bottom">
                        <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditCellStyle HorizontalAlign="Right">
                </EditCellStyle>
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ ایجاد" Width="75px" FieldName="CreateDate"
                VisibleIndex="1">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ غیرفعال شدن" Width="75px" FieldName="InActiveDate"
                VisibleIndex="2">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataMemoColumn Caption="توضیحات" FieldName="Description" VisibleIndex="3"
                Width="150px">
                <PropertiesMemoEdit Height="37px" Width="250px">
                </PropertiesMemoEdit>
            </dxwgv:GridViewDataMemoColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="4"
                Width="50px">
                <EditFormSettings Visible="False"></EditFormSettings>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="5" Width="50px" ShowClearFilterButton="true">
            </dxwgv:GridViewCommandColumn>
        </Columns>
    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
        <PanelCollection>
            <dx:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="false" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    CausesValidation="False" Text=" " EnableTheming="False"
                                    ToolTip="جدید" ID="btnnew2" EnableViewState="False">
                                    <ClientSideEvents Click="function(s, e) {
	grid.AddNewRow();
}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/new.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="false" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                    CausesValidation="False" Text=" " Width="25px"
                                    EnableTheming="False" ToolTip="ویرایش" ID="btnedit2" EnableViewState="False">
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
                                    <Image Url="~/Images/icons/edit.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btndel2" runat="server" EnableTheming="False" AutoPostBack="false"
                                    EnableViewState="False" ToolTip="حذف" OnClick="btndel_Click">
                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                    <Image Url="~/Images/icons/delete.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True" AutoPostBack="false"
                                    CausesValidation="False" Text=" " EnableTheming="False"
                                    ToolTip="غیر فعال" ID="btninactive2" EnableViewState="False" OnClick="btnInActive_Click">
                                    <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');

}"></ClientSideEvents>
                                    <Image Url="~/Images/icons/disactive.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                    ID="btnprint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False">
                                    <ClientSideEvents Click="function(s, e) {	
	grid.PerformCallback('print');
}"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/printers.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td align="right" valign="top">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی Excel"
                                    ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnExportExcel_Click">
                                    <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/ExportExcel.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.GovManagerTitleManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewGovManagerTitle">
    </dxwgv:ASPxGridViewExporter>
</asp:Content>
