<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="PriceArchive.aspx.cs" Inherits="TechnicalServices_PriceArchive" Title="مدیریت تعرفه خدمات مهندسی طراحی و نظارت" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                                <table ">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top;" align="Right">
                                                <table dir="rtl" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                                    ID="btnNew" EnableViewState="False" EnableTheming="False" OnClick="btnNew_Click"
                                                                    UseSubmitBehavior="False">
                                                                    <hoverstyle backcolor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </hoverstyle>
                                                                    <image url="~/Images/icons/new.png">
                                                                    </image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                                    Width="25px" ID="btnEdit" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click"
                                                                    UseSubmitBehavior="False">
                                                                    <hoverstyle backcolor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </hoverstyle>
                                                                    <image url="~/Images/icons/edit.png">
                                                                    </image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                                    ID="btnView" EnableViewState="False" EnableTheming="False" OnClick="btnView_Click"
                                                                    UseSubmitBehavior="False">
                                                                    <hoverstyle backcolor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </hoverstyle>
                                                                    <image url="~/Images/icons/view.png">
                                                                    </image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td width="10px" align="center">
                                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف"
                                                                    ID="btnDelete" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    AutoPostBack="false" OnClick="btnDelete_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
if (grdPriceArchive.GetFocusedRowIndex()&gt;-1)
{
	e.ProsseOnServer=confirm('آیا مطمئن به حذف این ردیف هستید؟');	 		
}
else
	alert(&quot;ردیفی انتخاب نشده است&quot;);
}" />
                                                                    <hoverstyle backcolor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </hoverstyle>
                                                                    <image height="25px" url="~/Images/icons/delete.png" width="25px" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                                                    ID="btnInActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnInActive_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
if (grdPriceArchive.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                                    <hoverstyle backcolor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </hoverstyle>
                                                                    <image url="~/Images/icons/disactive.png">
                                                                    </image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="فعال"
                                                                    ID="btnActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnActive_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
if (grdPriceArchive.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                                    <hoverstyle backcolor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </hoverstyle>
                                                                    <image url="~/Images/icons/active.png">
                                                                    </image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                           </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            <br />
                <TSPControls:CustomAspxDevGridView ID="grdPriceArchive" runat="server" Width="100%"
                    ClientInstanceName="grdPriceArchive"
                    EnableViewState="False" DataSourceID="ObjectDataSource_PriceArchive" KeyFieldName="PriceArchiveId"
                    OnAutoFilterCellEditorInitialize="grdPriceArchive_AutoFilterCellEditorInitialize"
                    OnHtmlDataCellPrepared="grdPriceArchive_HtmlDataCellPrepared">
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="PriceArchiveId" Width="10%" Caption="کد ثبت"
                            VisibleIndex="0" Visible="false">
                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Year" Caption="سال" VisibleIndex="1" Width="180px">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CreateDate" Caption="تاریخ ثبت" VisibleIndex="1"
                            Width="180px">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="DocumentNo" Caption="شماره نامه" VisibleIndex="2"
                            Width="150px">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="DocumentDate" Caption="تاریخ نامه" VisibleIndex="3"
                            Width="150px">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="ActiveStatus" Caption="وضعیت" VisibleIndex="3"
                            Width="80px">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="14" Caption=" " Width="80px" ShowClearFilterButton="true">
                     
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <SettingsCookies Enabled="false" />
                    <Settings ShowHorizontalScrollBar="true" ShowFilterRow="True" ShowGroupPanel="True"></Settings>
                </TSPControls:CustomAspxDevGridView>
                <asp:ObjectDataSource ID="ObjectDataSource_PriceArchive" runat="server" TypeName="TSP.DataManager.TechnicalServices.PriceArchiveManager"
                    SelectMethod="GetData"></asp:ObjectDataSource>
     
            <br />
             <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>  
                                <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                    width="100%">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                    ID="btnNew2" EnableViewState="False" EnableTheming="False" OnClick="btnNew_Click"
                                                    UseSubmitBehavior="False">
                                                    <hoverstyle backcolor="#FFE0C0">
                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                    </hoverstyle>
                                                    <image url="~/Images/icons/new.png">
                                                    </image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                    Width="25px" ID="btnEdit2" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click"
                                                    UseSubmitBehavior="False">
                                                    <hoverstyle backcolor="#FFE0C0">
                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                    </hoverstyle>
                                                    <image url="~/Images/icons/edit.png">
                                                    </image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                    ID="btnView2" EnableViewState="False" EnableTheming="False" OnClick="btnView_Click"
                                                    UseSubmitBehavior="False">
                                                    <hoverstyle backcolor="#FFE0C0">
                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                    </hoverstyle>
                                                    <image url="~/Images/icons/view.png">
                                                    </image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف"
                                                    ID="btnDelete2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    AutoPostBack="false" OnClick="btnDelete_Click">
                                                    <ClientSideEvents Click="function(s, e) {
if (grdPriceArchive.GetFocusedRowIndex()&gt;-1)
{
	e.ProsseOnServer=confirm('آیا مطمئن به حذف این ردیف هستید؟');	 		
}
else
	alert(&quot;ردیفی انتخاب نشده است&quot;);
}" />
                                                    <hoverstyle backcolor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </hoverstyle>
                                                    <image height="25px" url="~/Images/icons/delete.png" width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                                    ID="btnInActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnInActive_Click">
                                                    <ClientSideEvents Click="function(s, e) {
if (grdPriceArchive.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                    <hoverstyle backcolor="#FFE0C0">
                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                    </hoverstyle>
                                                    <image url="~/Images/icons/disactive.png">
                                                    </image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="فعال"
                                                    ID="btnActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnActive_Click">
                                                    <ClientSideEvents Click="function(s, e) {
if (grdPriceArchive.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                    <hoverstyle backcolor="#FFE0C0">
                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                    </hoverstyle>
                                                    <image url="~/Images/icons/active.png">
                                                    </image>
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
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                <img id="IMG1" src="../../../Image/indicator.gif" align="middle" />
                لطفا صبر نمایید ...
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
