<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="PlanControler.aspx.cs" Inherits="Employee_TechnicalServices_Plan_PlanControler"
    Title="مدیریت بازبین نقشه های ساختمانی" %>

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



                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                            width="100%" align="right">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0" align="right">
                                            <tbody>
                                                <tr>
                                                    <td align="right">
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
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                            ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnView_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/view.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیرفعال"
                                                            Width="25px" ID="btnInActive" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnInActive_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/disactive.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="فعال"
                                                            Width="25px" ID="btnActive" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnActive_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/Active.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                                            ID="btnPrint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False">
                                                            <ClientSideEvents Click="function(s, e) {	
	mygrid.PerformCallback('Print');	
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/printers.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False"
                                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                                            UseSubmitBehavior="False" Visible="true">
                                                            <ClientSideEvents Click="function(s,e){ btnTempExport.DoClick(); }"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/ExportExcel.png" Width="25px" />
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
            <TSPControls:CustomAspxDevGridView ID="GridViewControler" runat="server" DataSourceID="ObjdsContrler"
                KeyFieldName="ControlerId" Width="100%" AutoGenerateColumns="False" ClientInstanceName="mygrid" OnHtmlDataCellPrepared="GridViewControler_HtmlDataCellPrepared"
                OnAutoFilterCellEditorInitialize="GridViewControler_AutoFilterCellEditorInitialize"
                OnCustomCallback="GridViewControler_CustomCallback">
                <Settings ShowHorizontalScrollBar="true" ShowFooter="true" />
              <%--  <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="Foundation" SummaryType="Sum" />
                </TotalSummary>--%>
                <ClientSideEvents EndCallback="function(s, e) {
            if(s.cpDoPrint==1)
            {
                s.cpDoPrint = 0;
	window.open('../../../Print.aspx');
            }
}" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ControlerId" Caption="ControlerId"
                        Visible="False">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MeId" Caption="کد عضویت">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FirstName" Caption="نام">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" Width="150px" FieldName="LastName"
                        Caption="نام خانوادگی">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="FileNo" Caption="شماره پروانه">
                        <CellStyle CssClass="CellLeft">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="FileMjName" Caption="رشته">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="DesGrdName" Caption="پایه طراحی">
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="UrbanismGrdName" Caption="پایه شهرسازی">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="TrafficGrdName" Caption="پایه ترافیک">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="MappingGrdName" Caption="پایه نقشه برداری">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="ImpGrdName" Caption="پایه اجرا">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="ObsGrdName" Caption="پایه نظارت">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Date" Caption="تاریخ">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="InActives" Caption="وضعیت">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="AgentName" Caption="نمایندگی">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="6" Caption=" " ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                            width="100%" align="right">
                            <tbody>
                                <tr>
                                    <td>
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0" align="right">
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
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                            ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnView_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/view.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیرفعال"
                                                            Width="25px" ID="btnInActive2" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnInActive_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/disactive.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="فعال"
                                                            Width="25px" ID="btnActive2" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnActive_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/Active.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ"
                                                            ID="btnPrint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False">
                                                            <ClientSideEvents Click="function(s, e) {
	mygrid.PerformCallback('Print');
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/printers.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False"
                                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                                            UseSubmitBehavior="False" Visible="true">
                                                            <ClientSideEvents Click="function(s,e){ btnTempExport.DoClick(); }"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/ExportExcel.png" Width="25px" />
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
    <asp:ObjectDataSource ID="ObjdsContrler" runat="server" OldValuesParameterFormatString="original_{0}"
        TypeName="TSP.DataManager.TechnicalServices.ControlerManager" SelectMethod="SelectAllControlerByType">
        <SelectParameters>
            <asp:Parameter DefaultValue="1" Name="Type" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="MunId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewControler">
    </dxwgv:ASPxGridViewExporter>
    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnTempExport" ClientVisible="false" ClientInstanceName="btnTempExport"
        runat="server" OnClick="btntemp_Click">
    </TSPControls:CustomAspxButton>
</asp:Content>
