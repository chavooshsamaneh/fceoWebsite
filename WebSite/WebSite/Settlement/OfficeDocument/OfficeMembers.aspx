<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeMembers.aspx.cs" Inherits="Settlement_OfficeDocument_OfficeMembers"
    Title="اعضای شرکت" %>

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
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                width="100%">
                                <tr>
                                    <td align="right">
                                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                            <tr>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server"  EnableTheming="False"
                                                        EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                        ToolTip="بازگشت" UseSubmitBehavior="False">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
     
                    <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server"  
                         OnItemClick="ASPxMenu1_ItemClick">
                        <Items>
                            <dxm:MenuItem Name="Office" Text="مشخصات شرکت">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Member" Text="اعضای شرکت" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Agent" Text="شعبه ها">
                            </dxm:MenuItem>
                   <%--         <dxm:MenuItem Name="Job" Text="سوابق کاری">
                            
                            </dxm:MenuItem>  --%>
                              <dxm:MenuItem Name="Letters" Text="روزنامه های رسمی">
                                </dxm:MenuItem>
                            <dxm:MenuItem Name="Financial" Text="وضعیت مالی">
                            </dxm:MenuItem>
                        </Items>
                      
                    </TSPControls:CustomAspxMenuHorizontal>
                    <br />
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="شرکت :">
                    </dxe:ASPxLabel>
                    <dxe:ASPxLabel ID="lblOfName" runat="server">
                    </dxe:ASPxLabel>
    
                <br />
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" Width="100%" runat="server"
                    DataSourceID="ObjectDataSourceOffice" ClientInstanceName="grid" KeyFieldName="OfmId"
                    OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared" OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize"
                    OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared">
                    <SettingsBehavior ColumnResizeMode="Control" />
                    <SettingsCookies StoreColumnsWidth="true" StoreColumnsVisiblePosition="true" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="OfpName" VisibleIndex="0"
                            Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نوع" VisibleIndex="0" FieldName="TypeName">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="Active" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="OffMeId" Caption="کد عضویت"
                            Name="PersonId">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="OfmId" Name="OfmId" Visible="False" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="OfmType" Name="OfmType" Visible="False"
                            VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="PersonId" Name="PersonId" Visible="False"
                            VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره شناسنامه" FieldName="IdNo" VisibleIndex="2"
                            Visible="false">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="کد ملی" FieldName="SSN" VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ تولد" FieldName="BirthDate" VisibleIndex="4"
                            Visible="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="FileNo" Caption="شماره پروانه"
                            Name="FileNo">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="DesGrdName" Caption="پایه طراحی">
                            <HeaderStyle Wrap="True"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="ObsGrdName" Caption="پایه نظارت">
                            <HeaderStyle Wrap="True"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="ImpGrdName" Caption="پایه اجرا">
                            <HeaderStyle Wrap="True"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="OfReId" Visible="False" VisibleIndex="10">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="10" Width="30px" ShowClearFilterButton="true">
                         
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <Settings ShowHorizontalScrollBar="true"></Settings>
                </TSPControls:CustomAspxDevGridView>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                        <table >
                                            <tr>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView1" runat="server"  EnableTheming="False"
                                                        EnableViewState="False" OnClick="btnView_Click" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}" />
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton2" runat="server" CausesValidation="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                        ToolTip="بازگشت" UseSubmitBehavior="False">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>

        <asp:ObjectDataSource ID="ObjectDataSourceOffice" runat="server" SelectMethod="FindByOffRequest"
            TypeName="TSP.DataManager.OfficeMemberManager" FilterExpression="OfId={0}">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="OfId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="OfReId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="IsConfirm" Type="Int16" />
            </SelectParameters>
            <FilterParameters>
                <asp:Parameter Name="newparameter" />
            </FilterParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="OfficeId" runat="server" Visible="False" />
        <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
        <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False" />

</asp:Content>
