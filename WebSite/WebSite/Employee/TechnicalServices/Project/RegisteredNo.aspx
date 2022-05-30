<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="RegisteredNo.aspx.cs" Inherits="Employee_TechnicalServices_Project_RegisteredNo"
    Title="مدیریت پلاکهای ثبتی" %>
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

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div dir="rtl" id="DivReport" class="DivErrors" align="right" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]<br />
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="btnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                            ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیرفعال"
                                            ID="btnDelete" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnDelete_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
		e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/disactive.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                        </TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                            ID="btnPrint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {	
	e.processOnServer=false;
	window.open('../../../Print.aspx');
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/printers.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                        </TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnBack_Click">
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
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مالک" Name="Owner" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مالی پروژه" Name="Accounting" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                        <Items>
                            <%-- <dxm:MenuItem Text="مالی مالکان" Name="AccOwner">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="مالی ناظران" Name="AccObserver">
                            </dxm:MenuItem>--%>
                            <dxm:MenuItem Text="مالی طراحان" Name="AccDesigner">
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
                    <dxm:MenuItem Text="مجری" Name="Implementer" ItemStyle-CssClass="ProjectMainMenuItemStyle">
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
            <TSPControls:CustomAspxMenuHorizontal ID="ProjectMenu" runat="server" OnItemClick="ProjectMenu_ItemClick" CssClass="ProjectSubMenuHorizontal">
                <Items>
                    <dxm:MenuItem Text="اطلاعات پایه" Name="BaseInfo" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="پلاک ثبتی" Name="RegisteredNo" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="دستور نقشه" Name="PlansMethod" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="بلوک" Name="Block" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="بیمه" Name="Insurance" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>

            <TSP:ProjectInfo ID="prjInfo" runat="server"></TSP:ProjectInfo>
            <div align="right" width="100%">
                <ul class="HelpUL">
                    <li>در صورت عدم ثبت پلاک ثبتی،پروژه فاقد پلاک ثبتی در نظر گرفته می شود.همچنین در گزارش
                            معرفی طراح و ناظر به شهرداری، عبارت فاقد پلاک ثبتی چاپ خواهد شد. </li>
                </ul>
            </div>
            <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" DataSourceID="ObjectDataSourceRegisteredNo"
                Width="100%"
                OnFocusedRowChanged="CustomAspxDevGridView1_FocusedRowChanged" OnDetailRowExpandedChanged="CustomAspxDevGridView1_DetailRowExpandedChanged"
                AutoGenerateColumns="False" KeyFieldName="RegisteredNoId" ClientInstanceName="mygrid"
                OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared" OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize">
                <Columns>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="RegisteredNoId"
                        Caption="کد پلاک ثبتی" Name="RegisteredNoId">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="ProjectId"
                        Caption="کد پروژه" Name="ProjectId">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="RegisteredNo" Caption="شماره پلاک ثبتی"
                        Name="RegisteredNo">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="PostalCode" Caption="کد پستی"
                        Name="PostalCode">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Type" Caption="نوع پلاک"
                        Name="Type">
                        <PropertiesTextEdit EnableFocusedStyle="False">
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Division" Caption="بخش ثبتی"
                        Name="Type">
                        <PropertiesTextEdit EnableFocusedStyle="False">
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="Status" Name="Status" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="14" Caption=" " ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <Templates>
                    <DetailRow>
                        <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridViewDirections" runat="server"
                            DataSourceID="ObjectdatasourceDirections" Width="100%"
                            ClientInstanceName="mygrid" KeyFieldName="DirectionsId" AutoGenerateColumns="False"
                            OnBeforePerformDataSelect="CustomAspxDevGridViewDirections_BeforePerformDataSelect">
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="DirectionsId"
                                    Caption="DirectionsId" Name="DirectionsId">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="RegisteredNoId"
                                    Caption="کد پلاک ثبتی" Name="RegisteredNoId">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="عنوان" FieldName="DirectionTypeTitle" Name="DirectionTypeTitle"
                                    VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="North" Caption="شمال" Name="North">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="East" Caption="شرق" Name="East">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="South" Caption="جنوب" Name="South">
                                    <PropertiesTextEdit EnableFocusedStyle="False">
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="غرب" FieldName="West" Name="West" VisibleIndex="4">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </TSPControls:CustomAspxDevGridView>
                    </DetailRow>
                </Templates>
                <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" />
            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                            ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیرفعال"
                                            ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnDelete_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
	else
		e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/disactive.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2">
                                        </TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                                ID="btnPrint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open('../../../Print.aspx');
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/printers.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                    ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnBack_Click">
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
    <asp:ObjectDataSource ID="ObjectDataSourceRegisteredNo" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="FindByProjectAndPrjReId" TypeName="TSP.DataManager.TechnicalServices.RegisteredNoManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="ProjectId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="PrjReId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectdatasourceDirections" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="FindByRegisteredNoId" TypeName="TSP.DataManager.TechnicalServices.DirectionsManager">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="-1" Name="RegisteredNoId" SessionField="RegisteredNoId"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="PkProjectId" runat="server" Visible="False" />
    <asp:HiddenField ID="PkPrjReId" runat="server" Visible="False" />
    <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
    <dxhf:aspxhiddenfield id="HiddenFieldPage" clientinstancename="HiddenFieldPage"
        runat="server">
    </dxhf:aspxhiddenfield>
</asp:Content>
