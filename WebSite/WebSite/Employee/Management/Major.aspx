<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Major.aspx.cs" Inherits="Employee_Management_Major"
    Title="مدیریت رشته های تحصیلی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew" AutoPostBack="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image Width="25px" Height="25px" Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                            CausesValidation="False" ID="btnEdit" EnableViewState="False" Width="25px" EnableTheming="False"
                                            OnClick="btnEdit_Click">
                                            <ClientSideEvents Click="function(s, e) {

}
"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image Width="25px" Height="25px" Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                            CausesValidation="False" ID="btnView" AutoPostBack="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnView_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image Width="25px" Height="25px" Url="~/Images/icons/view.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                            CausesValidation="False" EnableClientSideAPI="True" ID="btnDelete" AutoPostBack="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnDelete_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
}
 else
//SetControlValues('Delete');
  e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');

}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image Width="25px" Height="25px" Url="~/Images/icons/delete.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                        </TSPControls:MenuSeprator>
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
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                             EnableTheming="False" EnableViewState="False"
                                            OnClick="btnActive_Click" Text=" " ToolTip="فعال" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');

}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/active.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2">
                                        </TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px" 
                                            ToolTip="خروجی Excel" CausesValidation="False" ID="btnExportExcel" EnableClientSideAPI="True"
                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnPrintClient"
                                            AutoPostBack="false" OnClick="btnExportExcel_Click">
                                            <ClientSideEvents Click="function(s,e){ }" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/ExportExcel.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        </td>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            
                    <dxwgv:ASPxGridViewExporter ID="GridViewExporterMajor" runat="server" GridViewID="DevGridViewMajor">
                    </dxwgv:ASPxGridViewExporter>
            <TSPControls:CustomAspxDevGridView ID="DevGridViewMajor" runat="server" Width="100%"
                KeyFieldName="MjId" EnableViewState="False" DataSourceID="ObjdsMajor" 
                 ClientInstanceName="grid" AutoGenerateColumns="False"
                OnRowUpdating="DevGridViewMajor_RowUpdating"> 

                <Columns>
                       <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MjId" Caption="کد"
                        Name="MjId">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نوع پروانه" FieldName="MasterType" Name="MasterType"
                        VisibleIndex="0" Width="60px">
                        <EditCellStyle HorizontalAlign="Center">
                        </EditCellStyle>
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MjCode" Caption="کد پروانه"
                        Name="MjCode">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjName" Width="200px" Caption="نام رشته"
                        Name="MjName">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="ParentId" Width="150px" Caption="زیر شاخه"
                        Name="PName" VisibleIndex="3">
                        <PropertiesComboBox Width="250px" TextField="MjParentName" DataSourceID="ObjectDataSourceDropDown"
                            ValueType="System.String" ValueField="MjParentId">
                        </PropertiesComboBox>
                        <CellStyle HorizontalAlign="Center" Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="InActive" Caption="وضعیت"
                        Name="MjCode">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataMemoColumn VisibleIndex="5" FieldName="Description" Width="250px"
                        Caption="توضیحات" Name="Description">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxwgv:GridViewDataMemoColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="6" Width="40px" Caption=" " ShowClearFilterButton="true">
                   
                    </dxwgv:GridViewCommandColumn>
                </Columns>

                <SettingsEditing EditFormColumnCount="1" PopupEditFormHorizontalAlign="WindowCenter"
                    PopupEditFormModal="True" PopupEditFormVerticalAlign="WindowCenter" Mode="PopupEditForm"></SettingsEditing>

                <Settings ShowGroupPanel="True" ShowFilterRowMenu="True" ShowHorizontalScrollBar="True"></Settings>
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
                                            CausesValidation="False" ID="btnNew2" AutoPostBack="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image Width="25px" Height="25px" Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                            CausesValidation="False" ID="btnEdit2" EnableViewState="False" Width="25px" EnableTheming="False"
                                            OnClick="btnEdit_Click">
                                            <ClientSideEvents Click="function(s, e) {

}
"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image Width="25px" Height="25px" Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                            CausesValidation="False" ID="btnView2" AutoPostBack="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnView_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image Width="25px" Height="25px" Url="~/Images/icons/view.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                            CausesValidation="False" EnableClientSideAPI="True" ID="btnDelete2" AutoPostBack="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnDelete_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
}
 else
//SetControlValues('Delete');
  e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');

}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image Width="25px" Height="25px" Url="~/Images/icons/delete.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                        </TSPControls:MenuSeprator>
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
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton2" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                             EnableTheming="False" EnableViewState="False"
                                            OnClick="btnActive_Click" Text=" " ToolTip="فعال" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');

}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/Active.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3">
                                        </TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px" 
                                            ToolTip="خروجی Excel" CausesValidation="False" ID="btnExportExcel2" EnableClientSideAPI="True"
                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnPrintClient"
                                            AutoPostBack="false" OnClick="btnExportExcel_Click">
                                            <ClientSideEvents Click="function(s,e){ }" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/ExportExcel.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
           
            <asp:ObjectDataSource ID="ObjectDataSourceDropDown" runat="server" SelectMethod="FindMjParents"
                TypeName="TSP.DataManager.MajorManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsMajor" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager"
                ConflictDetection="CompareAllValues" OldValuesParameterFormatString="Original_{0}"></asp:ObjectDataSource>
 
</asp:Content>
