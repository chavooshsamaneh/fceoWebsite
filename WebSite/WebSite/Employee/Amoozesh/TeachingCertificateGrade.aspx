<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="TeachingCertificateGrade.aspx.cs" Inherits="Employee_Amoozesh_TeachingCertificateGrade"
    Title="امتیازات استاد" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="btnNew" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnNew_Click" UseSubmitBehavior="False">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            Width="25px" ID="btnEdit" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {

	
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                            ID="btnView" EnableClientSideAPI="True" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>

                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive" runat="server" CausesValidation="False" ClientInstanceName="btnInActive"
                                            EnableClientSideAPI="True" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnInActive_Click" Text=" " ToolTip="غیرفعال"
                                            UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	                                                     e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
                                                    }" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnActive" runat="server" CausesValidation="False" ClientInstanceName="btnInActive"
                                            EnableClientSideAPI="True" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnActive_Click" Text=" " ToolTip="فعال"
                                            UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	                                                     e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
                                                    }" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="../../Images/icons/Active.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewTeachingGrade" runat="server" AutoGenerateColumns="False"
                DataSourceID="ObjdsTeachingGrade"
                EnableViewState="False" Width="100%" KeyFieldName="TGradeId" OnHtmlDataCellPrepared="GridViewTeachingGrade_HtmlDataCellPrepared"
                OnAutoFilterCellEditorInitialize="GridViewTeachingGrade_AutoFilterCellEditorInitialize" RightToLeft="True">
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ" FieldName="TGradeDate" VisibleIndex="0" Width="80px">
                        <HeaderStyle HorizontalAlign="center" />
                        <CellStyle HorizontalAlign="Center"></CellStyle>

                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="حداقل امتیاز لازم" FieldName="MinGrade" VisibleIndex="1"
                        Width="100px">
                        <HeaderStyle HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="وضعیت" FieldName="InActiveName"
                        VisibleIndex="3" Width="50px">
                        <HeaderStyle HorizontalAlign="Center" />
                        <CellStyle HorizontalAlign="Center"></CellStyle>

                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="3" Width="30px" Caption=" " ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>

            </TSPControls:CustomAspxDevGridView>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table >
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew2" runat="server" AutoPostBack="False"
                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="جدید" UseSubmitBehavior="False" OnClick="btnNew_Click">
                                        <ClientSideEvents Click="function(s, e) {
	}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" AutoPostBack="False"
                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="ویرایش" Width="25px"
                                        UseSubmitBehavior="False" OnClick="btnEdit_Click">
                                        <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server" EnableClientSideAPI="True"
                                        EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                        ToolTip="مشاهده" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>

                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive2" runat="server" CausesValidation="False" ClientInstanceName="btnInActive"
                                        EnableClientSideAPI="True" EnableTheming="False"
                                        EnableViewState="False" OnClick="btnInActive_Click" Text=" " ToolTip="غیرفعال"
                                        UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	                                                     e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
                                                    }" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnActive2" runat="server" CausesValidation="False" ClientInstanceName="btnInActive"
                                        EnableClientSideAPI="True" EnableTheming="False"
                                        EnableViewState="False" OnClick="btnActive_Click" Text=" " ToolTip="فعال"
                                        UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	                                                     e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
                                                    }" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="../../Images/icons/Active.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField ID="HiddenFieldTeachingGrade" runat="server">
            </dxhf:ASPxHiddenField>

            <asp:ObjectDataSource ID="ObjdsTeachingGrade" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.TeachingGradeManager"></asp:ObjectDataSource>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>

</asp:Content>
