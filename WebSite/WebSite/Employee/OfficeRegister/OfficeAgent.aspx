<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeAgent.aspx.cs" Inherits="Employee_OfficeRegister_OfficeAgent"
    Title="مدیریت شعبه های شرکت" %>

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
<%@ Register Src="~/UserControl/OfficeInfoUserControl.ascx" TagName="OfficeInfoUserControl"
    TagPrefix="UserControlOfficeInfo" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
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
                                        <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" EnableTheming="False"
                                        EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">

                                        <Image Url="~/Images/icons/view.png" />
                                        <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive" runat="server" EnableClientSideAPI="True"
                                        EnableTheming="False" EnableViewState="False" OnClick="btnInActive_Click" Text=" "
                                        ToolTip="غیر فعال" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}" />

                                        <Image Url="~/Images/icons/disactive.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton2" runat="server" AutoPostBack="False"
                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	grid.PerformCallback('Print');
	//window.open(&quot;../../Print.aspx&quot;);	
}" />

                                        <Image Url="~/Images/icons/printers.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False"
                                        EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                        ToolTip="بازگشت" UseSubmitBehavior="False">

                                        <Image Url="~/Images/icons/Back.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت اعضای حقوقی"
                                        CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnBackToManagment_Click">

                                        <Image Url="~/Images/icons/BakToManagment.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server"
                OnItemClick="ASPxMenu1_ItemClick">
                <Items>
                    <dxm:MenuItem Text="مشخصات شرکت" Name="Office">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="اعضا" Name="Member">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="شعبه ها" Name="Agent" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="سوابق کاری" Name="Job">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="روزنامه های رسمی" Name="Letters">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Financial" Text="وضعیت مالی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مستندات" Name="Attach">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Group" Text="گروه ها">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>

            <br />
            <UserControlOfficeInfo:OfficeInfoUserControl ID="OfficeInfoUserControl" runat="server" />
            <br />
            <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" Width="100%" runat="server"
                KeyFieldName="OagId"
                AutoGenerateColumns="False" ClientInstanceName="grid" DataSourceID="ODbOfAgent"
                OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared" OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared"
                OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize"
                OnCustomCallback="CustomAspxDevGridView1_CustomCallback">

                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="کد" FieldName="OagId" Name="OagId" VisibleIndex="0"
                        Width="30px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام شعبه" FieldName="OagName" Name="OagName"
                        VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="مدیر مسئول" FieldName="Responsible" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تلفن" FieldName="Tel" VisibleIndex="3" Name="Tel">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="فکس" FieldName="Fax" VisibleIndex="4" Visible="False">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="4"
                        Width="50px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="OfReId" VisibleIndex="4" Visible="False">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="5" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <ClientSideEvents EndCallback="function(s, e) {
if(grid.cpDoPrint==1)
{
	grid.cpDoPrint = 0;
	window.open(&quot;../../Print.aspx&quot;);
}
}" />
            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>


                        <table>
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server" EnableTheming="False"
                                        EnableViewState="False" OnClick="BtnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/new.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" EnableTheming="False"
                                        EnableViewState="False" OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش"
                                        UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/edit.png" />
                                        <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server" EnableTheming="False"
                                        EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/view.png" />
                                        <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnInActive2" runat="server" EnableClientSideAPI="True"
                                        EnableTheming="False" EnableViewState="False" OnClick="btnInActive_Click" Text=" "
                                        ToolTip="غیر فعال" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/disactive.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton3" runat="server" AutoPostBack="False"
                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	grid.PerformCallback('Print');
	//window.open(&quot;../../Print.aspx&quot;);	
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/printers.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server" CausesValidation="False"
                                        EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                        ToolTip="بازگشت" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/Back.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت اعضای حقوقی"
                                        CausesValidation="False" ID="ASPxButton4" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="btnBackToManagment_Click">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/BakToManagment.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
        AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
    <asp:ObjectDataSource ID="ODbOfAgent" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
        OldValuesParameterFormatString="original_{0}" SelectMethod="FindByOffRequest"
        TypeName="TSP.DataManager.OfficeAgentManager" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="Original_OagId" Type="Int32" />
            <asp:Parameter Name="Original_LastTimeStamp" Type="Object" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="OfId" Type="Int32" />
            <asp:Parameter Name="OagName" Type="String" />
            <asp:Parameter Name="Tel" Type="String" />
            <asp:Parameter Name="Fax" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="Website" Type="String" />
            <asp:Parameter Name="Address" Type="String" />
            <asp:Parameter Name="Responsible" Type="String" />
            <asp:Parameter Name="UserId" Type="Int32" />
            <asp:Parameter Name="ModifiedDate" Type="DateTime" />
            <asp:Parameter Name="Original_OagId" Type="Int32" />
            <asp:Parameter Name="Original_LastTimeStamp" Type="Object" />
            <asp:Parameter Name="OagId" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="OfId" Type="Int32" />
            <asp:Parameter Name="OagName" Type="String" />
            <asp:Parameter Name="Tel" Type="String" />
            <asp:Parameter Name="Fax" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="Website" Type="String" />
            <asp:Parameter Name="Address" Type="String" />
            <asp:Parameter Name="Responsible" Type="String" />
            <asp:Parameter Name="UserId" Type="Int32" />
            <asp:Parameter Name="ModifiedDate" Type="DateTime" />
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="OfId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="OfReId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="IsConfirm" Type="Int16" />
            <asp:Parameter DefaultValue="-1" Name="JustActive" Type="Int16" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="OfficeId" runat="server" Visible="False" />
    <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
    <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False" />
    <asp:HiddenField ID="HDMode" runat="server" Visible="False" />
    <dxhf:ASPxHiddenField ID="HiddenFieldOffice" runat="server">
    </dxhf:ASPxHiddenField>

</asp:Content>
