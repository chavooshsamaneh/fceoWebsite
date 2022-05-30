<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="University.aspx.cs" Inherits="Employee_Management_University"
    Title="مدیریت دانشگاه" %>

<%@ Register Assembly="DevExpress.Xpo.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Xpo" TagPrefix="dxxpo" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
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
                                                <td align="right" valign="top">
                                                    <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False" 
                                                                    EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                                                    ToolTip="جدید">
                                                                    <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" CausesValidation="False" 
                                                                    EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                                                    ToolTip="ویرایش" Width="25px">
                                                                    <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                    <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server"  EnableTheming="False"
                                                                    EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                                    <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td width="10px" align="center">
                                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                                                     EnableTheming="False" EnableViewState="False"
                                                                    OnClick="btnDelete_Click" Text=" " ToolTip="حذف">
                                                                    <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                                    <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td style="width: 30px" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیر فعال"
                                                                    Width="25px" ID="btnInActive" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnInActive_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
			if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else	
 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/disactive.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="فعال"
                                                                    ID="btnActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnActive_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/active.png">
                                                                    </Image>
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
                    <TSPControls:CustomAspxDevGridView ID="GridViewUniversity" runat="server" AutoGenerateColumns="False"
                       DataSourceID="ObjDataSourceUniversity" 
                        KeyFieldName="UnId" ClientInstanceName="grid" Width="100%" RightToLeft="True">
                        <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                        <Columns>
                            <dxwgv:GridViewDataTextColumn FieldName="CounId" Name="CounId" Visible="False" VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="کد دانشگاه" FieldName="UnCode" VisibleIndex="1">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataComboBoxColumn Width="250px" Caption="نام دانشگاه" FieldName="UnId"
                                VisibleIndex="2">
                                <PropertiesComboBox DataSourceID="ObjDataSourceUniversity" TextField="UnName" ValueField="UnId"
                                    ValueType="System.String" IncrementalFilteringMode="Contains">
                                </PropertiesComboBox>
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dxwgv:GridViewDataComboBoxColumn>
                            <dxwgv:GridViewDataComboBoxColumn Caption="کشور" Width="150px" FieldName="CounId"
                                VisibleIndex="3">
                                <PropertiesComboBox DataSourceID="ObjectDataSourceCoun" TextField="CounName" ValueField="CounId"
                                    ValueType="System.String" IncrementalFilteringMode="StartsWith">
                                </PropertiesComboBox>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataComboBoxColumn>
                            <dxwgv:GridViewDataTextColumn Caption="نوع دانشگاه" FieldName="UnTypeName" VisibleIndex="4">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="داخلی/خارجی" FieldName="UnInternal" VisibleIndex="5">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="وضعیت تایید" FieldName="ConfirmStatus" VisibleIndex="5">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="توضیحات" Width="200px" FieldName="Description"
                                VisibleIndex="6">
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="وضعیت" Width="80px" FieldName="InActiveStatus"
                                VisibleIndex="6">
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn Caption=" " Name="فیلتر" VisibleIndex="7" ShowClearFilterButton="true">
                         
                            </dxwgv:GridViewCommandColumn>
                        </Columns>
                        <SettingsCookies Enabled="false" />
                        <Settings ShowHorizontalScrollBar="true" ShowGroupPanel="True" ShowFilterRow="True"
                            ShowFilterRowMenu="True"></Settings>
                    </TSPControls:CustomAspxDevGridView>
                    <br />

                       <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                   
                                        <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                            width="100%">
                                            <tr>
                                                <td dir="ltr" align="right" valign="top">
                                                    <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                        <tr>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew2" runat="server" CausesValidation="False" 
                                                                    EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                                                    ToolTip="جدید">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" CausesValidation="False" 
                                                                    EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                                                    ToolTip="ویرایش" Width="25px">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server"  EnableTheming="False"
                                                                    EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td width="10px" align="center">
                                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                                            </td>
                                                            <td align="right" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                                                     EnableTheming="False" EnableViewState="False"
                                                                    Text=" " ToolTip="حذف" OnClick="btnDelete_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
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
                                                            <td style="width: 30px" valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیر فعال"
                                                                    Width="25px" ID="btnInActive2" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnInActive_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
			if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else	
 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/disactive.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td valign="top">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="فعال"
                                                                    ID="btnActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnActive_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/active.png">
                                                                    </Image>
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

                    <asp:ObjectDataSource ID="ObjDataSourceUniversity" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetData" TypeName="TSP.DataManager.UniversityManager"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSourceCoun" runat="server" SelectMethod="GetData"
                        TypeName="TSP.DataManager.CountryManager"></asp:ObjectDataSource>
                    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                        <ProgressTemplate>
                            <div class="modalPopup">
                                لطفا صبر نمایید
                                <img src="../../Image/indicator.gif" align="middle" />
                            </div>
                        </ProgressTemplate>
                    </asp:ModalUpdateProgress>

            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
