<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="City.aspx.cs" Inherits="City" Title="مدیریت شهرها" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
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

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td align="right" dir="rtl">
                                        <table dir="rtl" cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td align="right" valign="top">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                            CausesValidation="False" ID="BtnNew" AutoPostBack="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="BtnNew_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/new.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td align="right" valign="top">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                            CausesValidation="False" Width="25px" ID="btnEdit" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnEdit_Click">
                                                            <ClientSideEvents Click="function(s, e) {

}
"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/edit.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td align="right" valign="top">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                            CausesValidation="False" ID="btnView" AutoPostBack="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnView_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/view.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td align="right" valign="top">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                                            EnableTheming="False" EnableViewState="False"
                                                            Text=" " ToolTip="حذف" OnClick="btnDelete_Click" AutoPostBack="False">
                                                            <ClientSideEvents Click="function(s, e) {
if (GridViewCity.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                            <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel2" runat="server" CausesValidation="False"
                                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                                            UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                                            <ClientSideEvents Click="function(s,e){  }" />

                                                            <Image Url="~/Images/icons/ExportExcel.png" />
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
            <ul class="HelpUL">
                <li>منظور از امکان ثبت طراح فاقد دفتر، در ثبت طراحان پروژه های این شهر، امکان ثبت طراحی معماری توسط رشته عمران و معماری که فاقد دفتر طراحی وجود داشته باشد</li>
            </ul>
            <TSPControls:CustomAspxDevGridView ID="GridViewCity" runat="server" AutoGenerateColumns="False"
                DataSourceID="ObjectDataSource1"
                ClientInstanceName="GridViewCity" KeyFieldName="CitId" Width="100%">
                <Columns>

                    <dxwgv:GridViewDataTextColumn Caption="کد شهر" FieldName="CitCode" VisibleIndex="0">
                        <CellStyle Wrap="False" HorizontalAlign="Center" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نام شهر" FieldName="CitName" Name="CitName"
                        VisibleIndex="0" Width="150px">
                        <CellStyle Wrap="False" HorizontalAlign="Center" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شهرستان" FieldName="ReCitName" VisibleIndex="0" Width="150px">
                        <CellStyle Wrap="False" HorizontalAlign="Center" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نمایندگی" FieldName="AgentName" VisibleIndex="0" Width="150px">
                        <CellStyle Wrap="False" HorizontalAlign="Center" />
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn Caption="استان" FieldName="PrName" VisibleIndex="0" Width="150px">
                        <CellStyle Wrap="False" HorizontalAlign="Center" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="کشور" FieldName="CounName" VisibleIndex="0" Width="150px">
                        <CellStyle Wrap="False" HorizontalAlign="Center" />
                    </dxwgv:GridViewDataTextColumn>

                    <dxcp:GridViewDataCheckColumn FieldName="ShowInTsWorkRequest" Caption="نمایش در آماده به کاری"
                        Name="ShowInTsWorkRequest" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxcp:GridViewDataCheckColumn>                    
                    <dxcp:GridViewDataCheckColumn FieldName="IsPopulationUnder25000" Caption="جمعیت زیر 25000"
                        Name="IsPopulationUnder25000" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxcp:GridViewDataCheckColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شماره حساب 5درصد طراحی" FieldName="AccountNumberDesign" VisibleIndex="0">
                        <CellStyle Wrap="False" HorizontalAlign="Center" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شماره حساب صددرصد حق الزحمه ناظرین" FieldName="AccountNmberObserving" VisibleIndex="0">
                        <CellStyle Wrap="False" HorizontalAlign="Center" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شماره حساب 5درصد حق الزحمه ناظر ساختمان و تاسیسات" FieldName="AccountNmberObserving5Percent" VisibleIndex="0">
                        <CellStyle Wrap="False" HorizontalAlign="Center" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شماره حساب هزینه دفترچه فنی ملکی (پنج در هزار)" FieldName="AccountNmber5In1000" VisibleIndex="0">
                        <CellStyle Wrap="False" HorizontalAlign="Center" />
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn Caption="پین کد درگاه پارسیان  حساب طراحی" FieldName="PINCodeDesign" VisibleIndex="0">
                        <CellStyle Wrap="False" HorizontalAlign="Center" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ترمینال درگاه پارسیان  حساب طراحی" FieldName="TerminalDesign" VisibleIndex="0">
                        <CellStyle Wrap="False" HorizontalAlign="Center" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="پین کد درگاه پارسیان  حساب نظارت" FieldName="PINCodeObserver" VisibleIndex="0">
                        <CellStyle Wrap="False" HorizontalAlign="Center" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ترمینال درگاه پارسیان  حساب نظارت" FieldName="TerminalObserver" VisibleIndex="0">
                        <CellStyle Wrap="False" HorizontalAlign="Center" />
                    </dxwgv:GridViewDataTextColumn>


                    <dxwgv:GridViewDataTextColumn Caption="امکان ثبت طراح فاقد دفتر" FieldName="CanObserverBeDesignerName" VisibleIndex="0">
                        <CellStyle Wrap="False" HorizontalAlign="Center" />
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn Caption="N در تابع ارجاع کار نظارت" FieldName="NValueInFunctionA" VisibleIndex="0">
                        <CellStyle Wrap="False" HorizontalAlign="Center" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="0" ShowClearFilterButton="true">

                        <CellStyle Wrap="False" HorizontalAlign="Center" />
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <Settings ShowHorizontalScrollBar="true" />
            </TSPControls:CustomAspxDevGridView>
            <br />
            <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewCity"
                ExportedRowType="All">
            </dxwgv:ASPxGridViewExporter>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.CityManager"></asp:ObjectDataSource>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table dir="rtl">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="btnNew2" AutoPostBack="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit2" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnEdit_Click">
                                            <ClientSideEvents Click="function(s, e) {

}
"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                            CausesValidation="False" ID="btnView2" AutoPostBack="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnView_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" CausesValidation="False" EnableClientSideAPI="True"
                                            EnableTheming="False" EnableViewState="False"
                                            Text=" " ToolTip="حذف" OnClick="btnDelete_Click" AutoPostBack="False">
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewCity.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                            <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnExportExcel" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" Text=" " ToolTip="خروجی Excel"
                                            UseSubmitBehavior="False" Visible="true" OnClick="btnExportExcel_Click">
                                            <ClientSideEvents Click="function(s,e){  }" />

                                            <Image Url="~/Images/icons/ExportExcel.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>          
</asp:Content>
