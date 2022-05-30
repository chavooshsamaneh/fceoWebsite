<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="GovManagerName.aspx.cs" Inherits="Employee_Document_GovManagerName"
    Title="مدیریت مديران استانی/كشوری" %>

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
                    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                            href="#"><span style="color: #000000">بستن</span></a>]</div>
                            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                                    <table >
                                                        <tr>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server"  EnableTheming="False"
                                                                    EnableViewState="False" OnClick="BtnNew_Click" ToolTip="جدید" Text=" " UseSubmitBehavior="False">
                                                                    <Image  Url="~/Images/icons/new.png"  />
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server"  EnableTheming="False"
                                                                    EnableViewState="False" OnClick="btnEdit_Click" ToolTip="ویرایش" 
                                                                    Text=" " UseSubmitBehavior="False">
                                                                    <Image  Url="~/Images/icons/edit.png"  />
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server"  EnableTheming="False"
                                                                    EnableViewState="False" OnClick="btnView_Click" ToolTip="مشاهده" Text=" " UseSubmitBehavior="False">
                                                                    <Image  Url="~/Images/icons/view.png"  />
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableClientSideAPI="True" 
                                                                    EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" ToolTip="حذف"
                                                                    Text=" " UseSubmitBehavior="False">
                                                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                                    <Image  Url="~/Images/icons/delete.png"  />
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive2" runat="server" EnableClientSideAPI="True" 
                                                                    EnableTheming="False" EnableViewState="False" OnClick="btnInActive_Click" ToolTip="غیرفعال"
                                                                    Text=" " UseSubmitBehavior="False">
                                                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}" />
                                                                    <Image  Url="~/Images/icons/disactive.png"  />
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
  </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" Width="100%"
                      DataSourceID="ObjectDataSource1"
                    AutoGenerateColumns="False" KeyFieldName="GmnId" OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize"
                    OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared">
                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                  
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="GmnId" Name="GmnId" Visible="False" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="GmtName" Name="GmtName" Width="300px" Caption="عنوان"
                            VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="GmnName" Name="GmnName" Width="150px" Caption="نام"
                            VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="InActiveName" Name="InActiveName" Caption="وضعیت"
                            VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CreateDate" Name="CreateDate" Caption="تاریخ ایجاد"
                            VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="6" ShowClearFilterButton="true">
                         
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <Settings ShowHorizontalScrollBar="true" ShowGroupPanel="True" ShowFilterRow="true"
                        ShowFilterRowMenu="true"></Settings>
                </TSPControls:CustomAspxDevGridView>
                <br />
                          <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                                <table >
                                                    <tr>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server"  EnableTheming="False"
                                                                EnableViewState="False" OnClick="BtnNew_Click" ToolTip="جدید" Text=" " UseSubmitBehavior="False">
                                                                <Image  Url="~/Images/icons/new.png"  />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server"  EnableTheming="False"
                                                                EnableViewState="False" OnClick="btnEdit_Click" ToolTip="ویرایش" 
                                                                Text=" " UseSubmitBehavior="False">
                                                                <Image  Url="~/Images/icons/edit.png"  />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server"  EnableTheming="False"
                                                                EnableViewState="False" OnClick="btnView_Click" ToolTip="مشاهده" Text=" " UseSubmitBehavior="False">
                                                                <Image  Url="~/Images/icons/view.png"  />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableClientSideAPI="True" 
                                                                EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" ToolTip="حذف"
                                                                Text=" " UseSubmitBehavior="False">
                                                                <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                                <Image  Url="~/Images/icons/delete.png"  />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive" runat="server" EnableClientSideAPI="True" 
                                                                EnableTheming="False" EnableViewState="False" OnClick="btnInActive_Click" ToolTip="غیرفعال"
                                                                Text=" " UseSubmitBehavior="False">
                                                                <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}" />
                                                                <Image  Url="~/Images/icons/disactive.png"  />
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
            TypeName="TSP.DataManager.GovManagerNameManager"></asp:ObjectDataSource>
</asp:Content>
