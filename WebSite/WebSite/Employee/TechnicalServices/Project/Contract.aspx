<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="Contract.aspx.cs" Inherits="Employee_TechnicalServices_Project_Contract"
    Title="مدیریت قرارداد" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="BtnNew_Click">
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnEdit_Click">
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                            ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                            ID="btnInActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnInActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');


}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/disactive.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            ID="btnBack" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <ClientSideEvents Click="function(s, e) {


}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت پروژه"
                                            CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBackToManagment_Click">
                                            <Image Url="../../../Images/icons/BakToManagment.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomAspxMenuHorizontal ID="MainMenu" runat="server" CssClass="ProjectMainMenuHorizontal"
                OnItemClick="MainMenu_ItemClick">
                <Items>
                    <dxm:MenuItem Text="مشخصات پروژه" Name="Project" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                        <Items>
                            <dxm:MenuItem Text="اطلاعات پایه" Name="BaseInfo" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="پلاک ثبتی" Name="RegisteredNo" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="دستور نقشه" Name="PlansMethod" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="بلوک" Name="Block" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="بیمه" Name="Insurance" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                            </dxm:MenuItem>
                        </Items>
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مالک" Name="Owner" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مالی پروژه" Name="Accounting" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                        <Items>
                            <%--  <dxm:MenuItem Text="مالی مالکان" Name="AccOwner">
                                </dxm:MenuItem>--%>
                            <dxm:MenuItem Text="مالی طراحان" Name="AccDesigner" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                            </dxm:MenuItem>
                            <%-- <dxm:MenuItem Text="مالی ناظران" Name="AccObserver">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="مالی مجریان" Name="AccImp">
                                </dxm:MenuItem>--%>
                        </Items>
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="نقشه" Name="Plans" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="طراح" Name="Designer" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="ناظر" Name="Observers" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مجری" Name="Implementer" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="قرارداد" Name="Contract" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                    <%--  <dxm:MenuItem Text="زمان بندی" Name="Timing">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="پروانه ساخت" Name="BuildingsLicense">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="اعلام وضعیت" Name="StatusAnnouncement">
                            </dxm:MenuItem>--%>
                        </Items>
                    </TSPControls:CustomAspxMenuHorizontal>
           
                <br />
                <TSP:ProjectInfo ID="prjInfo" runat="server" />
                <br />
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" Width="100%" runat="server"
                    DataSourceID="ObjectDataSourceContractManager" 
                     AutoGenerateColumns="False" KeyFieldName="ContractId" ClientInstanceName="grid"
                    OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared" OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared"
                    OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize">
                    <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                 
                    <Columns>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ContractId" Caption="کد قرارداد">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Title" Caption="نوع قرارداد">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Duration" Caption="مدت زمان(ماه)">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Amount" Caption="مبلغ قرارداد">
                            <PropertiesTextEdit DisplayFormatString="#,#">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="ContractDate" Caption="تاریخ انعقاد">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="CreateDate" Caption="تاریخ ایجاد">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="InActiveName" Caption="وضعیت">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="InActiveDate" Caption="تاریخ تغییر وضعیت">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="9" ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                </TSPControls:CustomAspxDevGridView>
                <br />
                 <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                                                    <table >
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true"  runat="server" Text=" "  ToolTip="جدید"
                                                                        ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="BtnNew_Click">
                                                                        <Image  Url="~/Images/icons/new.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true"  runat="server" Text=" "  ToolTip="ویرایش"
                                                                        ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnEdit_Click">
                                                                        <Image  Url="~/Images/icons/edit.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton  IsMenuButton="true"  runat="server" Text=" "  ToolTip="مشاهده"
                                                                        ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnView_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                            ID="btnInActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnInActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');


}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/disactive.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            ID="ASPxButton5" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <ClientSideEvents Click="function(s, e) {


}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت پروژه"
                                            CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBackToManagment_Click">
                                            <Image Url="../../../Images/icons/BakToManagment.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                            <img alt="" src="../../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
            <asp:ObjectDataSource ID="ObjectDataSourceContractManager" runat="server" SelectMethod="FindByProjectIdAndPrjReId"
                TypeName="TSP.DataManager.TechnicalServices.ContractManager">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="ProjectId"></asp:Parameter>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="PrjReId"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:HiddenField ID="HDProjectId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="RequestId" runat="server" Visible="False"></asp:HiddenField>
            <dxhf:ASPxHiddenField ID="HiddenFieldPage" ClientInstanceName="HiddenFieldPage"
                runat="server">
            </dxhf:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
