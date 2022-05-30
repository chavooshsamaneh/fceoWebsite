<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="Owner.aspx.cs" Inherits="Employee_TechnicalServices_Project_Owner"
    Title="مالکین پروژه" %>

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
<%@ Register Src="../../../UserControl/WFUserControl.ascx" TagName="WFUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divcontent" style="width: 100%" align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]
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
                                                ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="BtnNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnEdit_Click">
                                                <ClientSideEvents Click="function(s, e) {
	 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
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
                                                ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnView_Click">
                                                <ClientSideEvents Click="function(s, e) {
	 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/view.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6">
                                            </TSPControls:MenuSeprator>
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
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/disactive.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مالی مالکین"
                                                ID="btnAccOwner" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnAccOwner_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/TS/TSImpAcc.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار پروژه"
                                                ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/reload.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                ID="btnBack" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBack_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/Back.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت پروژه"
                                                CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
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
                <TSPControls:CustomAspxMenuHorizontal ID="MainMenu" runat="server" CssClass="ProjectMainMenuHorizontal" OnItemClick="MainMenu_ItemClick">
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
                                <%-- <dxm:MenuItem Text="مالی مالکان" Name="AccOwner">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="مالی ناظران" Name="AccObserver">
                                </dxm:MenuItem>--%>
                                <dxm:MenuItem Text="مالی طراحان" Name="AccDesigner" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                                <%--  <dxm:MenuItem Text="مالی مجریان" Name="AccImp">
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
                        <%--   <dxm:MenuItem Text="زمان بندی" Name="Timing">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="پروانه ساخت" Name="BuildingsLicense">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="اعلام وضعیت" Name="StatusAnnouncement">
                        </dxm:MenuItem>--%>
                    </Items>

                </TSPControls:CustomAspxMenuHorizontal>
                <br />
                <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenuOwner" Visible="false" runat="server"
                    OnItemClick="ASPxMenuOwner_ItemClick">
                    <Items>
                        <dxm:MenuItem Text="مالی مالکین" Name="Accounting">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="مدیریت مالکین" Name="Owner" Selected="True">
                        </dxm:MenuItem>
                    </Items>

                </TSPControls:CustomAspxMenuHorizontal>
                </div>
                <div dir="ltr">
                    <TSP:ProjectInfo ID="prjInfo" runat="server"></TSP:ProjectInfo>
                </div>
                <br />
                <TSPControls:CustomAspxDevGridView ID="GridViewOwner" Width="100%" runat="server"
                    DataSourceID="ObjectDataSourceOwner"
                    AutoGenerateColumns="False" KeyFieldName="OwnerId" ClientInstanceName="grid"
                    OnHtmlRowPrepared="GridViewOwner_HtmlRowPrepared" OnHtmlDataCellPrepared="GridViewOwner_HtmlDataCellPrepared"
                    OnAutoFilterCellEditorInitialize="GridViewOwner_AutoFilterCellEditorInitialize">
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="OwnerId">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataCheckColumn VisibleIndex="0" FieldName="IsAgent" Caption="نماینده مالکین">
                            <HeaderStyle Wrap="True"></HeaderStyle>
                        </dxwgv:GridViewDataCheckColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Name" Caption="نام مالک">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Tel" Caption="تلفن">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="MobileNo" Caption="شماره همراه">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="SSN" Caption="کد ملی مالک">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="MeId" Caption="کد عضویت مالک">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="CreateDate" Caption="تاریخ ایجاد">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="InActiveName" Caption="وضعیت">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="InactiveDate" Caption="تاریخ تغییر وضعیت">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="6" FieldName="PrjReId">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="14" Caption=" " ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <Settings ShowHorizontalScrollBar="true"></Settings>
                </TSPControls:CustomAspxDevGridView>
                <br />
                <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="grid" SessionName="SendBackDataTable_EmpPrjOwner"
                    OnCallback="CallbackPanelWorkFlow_Callback" />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="BtnNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnEdit_Click">
                                                <ClientSideEvents Click="function(s, e) {
	 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
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
                                                ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnView_Click">
                                                <ClientSideEvents Click="function(s, e) {
	 if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/view.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                            </TSPControls:MenuSeprator>
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
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/disactive.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مالی مالکین"
                                                ID="btnAccOwner2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnAccOwner_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/TS/TSImpAcc.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار پروژه"
                                                ID="btnSendNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/reload.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                ID="ASPxButton5" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBack_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/Back.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت پروژه"
                                                CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
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
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                    DisplayAfter="0">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                            <img src="../../../Image/indicator.gif" align="middle" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
                <asp:ObjectDataSource ID="ObjectDataSourceOwner" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="FindByProjectIdAndPrjReId" TypeName="TSP.DataManager.TechnicalServices.OwnerManager">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="ProjectId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="PrjReId" Type="Int32" />
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
    </div>
</asp:Content>
