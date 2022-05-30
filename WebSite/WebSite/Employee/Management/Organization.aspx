<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="Organization.aspx.cs" Inherits="Employee_Management_Organization" Title="مديريت ديگر سازمان ها" %>

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

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]</div>
                          <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>

                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/new.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش" Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/edit.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده" ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnView_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/view.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال" ID="btnInActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnInActive_Click">
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
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/disactive.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ" ID="ASPxButton2" AutoPostBack="False" UseSubmitBehavior="False" Visible="False" EnableViewState="False" EnableTheming="False">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../Print.aspx&quot;);	
}"></ClientSideEvents>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/printers.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                               

  </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" DataSourceID="ODbOrganization" Width="100%"   KeyFieldName="OrgId" AutoGenerateColumns="False" ClientInstanceName="grid"
                    OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize">
                    
                    <Columns>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="OrgName" Caption="نام سازمان" Width="250px">
                            <CellStyle Wrap="false"></CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="ManagerName" Caption="مدير سازمان" Width="150px">
                            <CellStyle Wrap="false"></CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Tel" Caption="تلفن">
                            <CellStyle Wrap="false"></CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="MobileNo" Visible="false" Caption="شماره همراه"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="CreateDate" Caption="تاريخ ايجاد">
                            <CellStyle Wrap="false"></CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="Status" Caption="وضعيت"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="6" Caption=" " Width="30px" ShowClearFilterButton="true">
                           
                        </dxwgv:GridViewCommandColumn>
                    </Columns>

                   
                    <Settings ShowGroupPanel="True" ShowFilterRowMenu="True" ShowFilterRow="True" ShowHorizontalScrollBar="true"></Settings>
                </TSPControls:CustomAspxDevGridView>
                <br />
                          <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


 
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/new.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش" Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/edit.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده" ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnView_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/view.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال" ID="btnInActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnInActive_Click">
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
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/disactive.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ" ID="ASPxButton3" AutoPostBack="False" UseSubmitBehavior="False" Visible="False" EnableViewState="False" EnableTheming="False">
                                                                        <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open(&quot;../../Print.aspx&quot;);	
}"></ClientSideEvents>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/printers.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table> </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                                                
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1"
            DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
        <asp:ObjectDataSource ID="ODbOrganization" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="TSP.DataManager.OrganizationManager"></asp:ObjectDataSource>
 
</asp:Content>



