<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="PlanDesigner.aspx.cs" Inherits="Employee_TechnicalServices_Project_PlanDesigner"
    Title="مدیریت طراحان نقشه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
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
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مالی طراحان"
                                            ID="btnDesAcc" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnDesAcc_Click">
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
                                            <Image Url="~/Images/TS/TSImpAcc.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیرفعال"
                                            Width="25px" ID="btnInActive" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnInActive_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
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
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" Width="25px" ID="btnBack" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
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
                            <dxm:MenuItem Text="اطلاعات پایه" Name="BaseInfo" Selected="true" ItemStyle-CssClass="ProjectMainMenuItemStyle">
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
                            <%--             <dxm:MenuItem Text="مالی مالکان" Name="AccOwner">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="مالی طراحان" Name="AccDesigner">
                                </dxm:MenuItem>--%>
                            <dxm:MenuItem Text="مالی ناظران" Name="AccObserver" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                            </dxm:MenuItem>
                            <%-- <dxm:MenuItem Text="مالی مجریان" Name="AccImp">
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
                            <%-- <dxm:MenuItem Text="زمان بندی" Name="Timing">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="پروانه ساخت" Name="BuildingsLicense">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="اعلام وضعیت" Name="StatusAnnouncement">
                            </dxm:MenuItem>--%>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>
            <br />
            <TSPControls:CustomAspxMenuHorizontal ID="MenuPlan" runat="server"
                OnItemClick="MenuPlan_ItemClick" CssClass="ProjectSubMenuHorizontal">
                <ItemStyle HorizontalAlign="Right" Font-Size="X-Small" Font-Bold="true" />
                <Items>
                    <dxm:MenuItem Name="Plan" Text="مشخصات نقشه" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="PlanDes" Selected="True" Text="طراحان نقشه" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="ControlerViewPoint" Text="بازبین نقشه" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>

            <TSP:ProjectInfo ID="prjInfo" runat="server" />
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel3" HeaderText="مشخصات نقشه" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <div class="Item-center">
                            <dxe:ASPxLabel runat="server" Text="وضعیت درخواست: نامشخص" Font-Bold="False" ID="lblWorkFlowState"
                                ForeColor="Red">
                            </dxe:ASPxLabel>
                        </div>
                        <table width="100%">
                            <tr>
                                <td align="right" valign="top" width="15%">نوع نقشه:
                                </td>
                                <td align="right" valign="top" width="35%">
                                    <dxe:ASPxLabel runat="server" Font-Bold="true" Text="" ID="txtPlanType">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" width="15%">شماره نقشه:
                                </td>
                                <td align="right" valign="top" width="35%">
                                    <dxe:ASPxLabel runat="server" Font-Bold="true" Text="" ID="txtPlanNo" Style="direction: ltr">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">کد رهگیری نقشه</td>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel runat="server" Font-Bold="true" Text="" ID="txtFollowCode">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top">نسخه:
                                </td>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel runat="server" Font-Bold="true" Text="" ID="txtPlanVer">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewPlanDesigner" runat="server" DataSourceID="ObjdsPlanDesigner"
                Width="100%"
                KeyFieldName="PlansId" AutoGenerateColumns="False" ClientInstanceName="grid"
                OnHtmlDataCellPrepared="GridViewPlanDesigner_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="GridViewPlanDesigner_AutoFilterCellEditorInitialize">
                <Columns>
                    
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="DesignerInActiveName" Caption="وضعیت">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="PrjDesignerId" Caption="PrjDesignerId"
                        Visible="False" EditFormSettings-Visible="False">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="PrjDesignerReId" Caption="PrjReId"
                        Visible="False">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="DesignerPlansId" Caption="DesignerPlansId"
                        Visible="False">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MeType" Caption="نوع طراح">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="OfficeEngOId" Caption="کد دفتر/شرکت طراح">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MeId" Caption="کد عضویت طراح">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="CapacityDecrement" Caption="متراژ کسر ظرفیت">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Wage" Caption="متراژ دستمزد">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="YearName" Caption="تعرفه">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Year" Caption="سال کاری">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="فاقد پیش شرط ها" FieldName="SaveWithOutConditionName" VisibleIndex="0"
                        Width="100px">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="DesignerCreateDate" Caption="تاریخ ثبت طراح">
                        <CellStyle Wrap="False" HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <Settings ShowHorizontalScrollBar="true"></Settings>
            </TSPControls:CustomAspxDevGridView>
            <asp:ObjectDataSource ID="ObjdsPlanDesigner" runat="server" TypeName="TSP.DataManager.TechnicalServices.Designer_PlansManager"
                SelectMethod="SelectDesignerByPlansId" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="PlansId"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelMenuHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td>
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                            ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
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
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مالی طراحان"
                                                            ID="btnDesAcc2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnDesAcc_Click">
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
                                                            <Image Url="~/Images/TS/TSImpAcc.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیرفعال"
                                                            Width="25px" ID="btnInActive2" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnInActive_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
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
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                                    </td>
                                                    <td style="vertical-align: top">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                            CausesValidation="False" Width="25px" ID="btnBack2" AutoPostBack="False" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/Back.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldDesPlans">
                                        </dxhf:ASPxHiddenField>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img src="../../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>

</asp:Content>
