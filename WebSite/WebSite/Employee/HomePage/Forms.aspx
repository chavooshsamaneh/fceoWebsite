<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="Forms.aspx.cs" Inherits="Employee_HomePage_Forms" Title="فرم ها" %>

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

    <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>


                <table>
                    <tr>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" EnableTheming="False"
                                EnableViewState="False" OnClick="BtnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">

                                <Image Url="~/Images/icons/new.png" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" EnableTheming="False"
                                EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش"
                                UseSubmitBehavior="False">

                                <Image Url="~/Images/icons/edit.png" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" EnableTheming="False"
                                EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">

                                <Image Url="~/Images/icons/view.png" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableClientSideAPI="True"
                                EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                ToolTip="حذف" UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />

                                <Image Url="~/Images/icons/delete.png" />
                            </TSPControls:CustomAspxButton>
                        </td>

                    </tr>
                </table>

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" DataSourceID="ObjectDataSource1"
        KeyFieldName="FoId" Width="100%">
        <Columns>
            <dxwgv:GridViewDataTextColumn Caption="نوع فرم" FieldName="FormTypeName" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="کد فرم" FieldName="FoCode" VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="عنوان فرم" FieldName="FoName" VisibleIndex="1">
            </dxwgv:GridViewDataTextColumn>

        </Columns>
    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                    <tr>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server" EnableTheming="False"
                                EnableViewState="False" OnClick="BtnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">

                                <Image Url="~/Images/icons/new.png" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" EnableTheming="False"
                                EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش"
                                UseSubmitBehavior="False">

                                <Image Url="~/Images/icons/edit.png" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server" EnableTheming="False"
                                EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">

                                <Image Url="~/Images/icons/view.png" />
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableClientSideAPI="True"
                                EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                ToolTip="حذف" UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />

                                <Image Url="~/Images/icons/delete.png" />
                            </TSPControls:CustomAspxButton>
                        </td>

                    </tr>
                </table>

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.FormsManager"></asp:ObjectDataSource>

</asp:Content>
