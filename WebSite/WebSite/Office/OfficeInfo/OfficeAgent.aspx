<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeAgent.aspx.cs" Inherits="Office_OfficeInfo_OfficeAgent"
    Title="شعبه ها" %>

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
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
     
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                 <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>

                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                    ID="btnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="btnNew_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/new.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                    ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="btnEdit_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/edit.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                                    ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="btnView_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/view.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیر فعال"
                                                                    ID="btnInActive" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="btnInActive_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/disactive.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                    CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnBack_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/Back.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت اعضای حقوقی"
                                                                    CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/BakToManagment.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                         
  </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                    <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" ItemImagePosition="right" runat="server" 
                        OnItemClick="ASPxMenu1_ItemClick" >
                        <Items>
                            <dxm:MenuItem Text="مشخصات شرکت" Name="Office">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Member" Text="اعضا">
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
                       
                        </Items>
                    </TSPControls:CustomAspxMenuHorizontal>
              
                <br />
               
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" DataSourceID="ODbOfAgent"
                    Width="100%"  
                    KeyFieldName="OagId" AutoGenerateColumns="False" ClientInstanceName="grid" OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared"
                    OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize">
                    <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="OagId" Caption="کد" Name="OagId"
                            Width="40px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="OagName" Caption="نام شعبه"
                            Name="OagName" Width="150px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Width="150px" VisibleIndex="2" FieldName="Responsible"
                            Caption="مدیر مسئول">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Tel" Caption="تلفن">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Fax" Caption="فکس" Visible="false">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="InActiveName" Caption="وضعیت"
                            Width="60px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="OfReId">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="10" ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowFilterRowMenu="true" ShowHorizontalScrollBar="true">
                    </Settings>
                </TSPControls:CustomAspxDevGridView>
                <br />
                  <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>

                                                <table >
                                                    <tbody>
                                                        <tr>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                    ID="btnNew1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="btnNew_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/new.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                    ID="btnEdit1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="btnEdit_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/edit.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                                    ID="btnView1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="btnView_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/view.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیر فعال"
                                                                    ID="btnInActive2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    __designer:wfdid="w2" OnClick="btnInActive_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
}"></ClientSideEvents>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/disactive.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                    CausesValidation="False" ID="ASPxButton2" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnBack_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/Back.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت اعضای حقوقی"
                                                                    CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/BakToManagment.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                               
  </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
      
        <asp:ObjectDataSource ID="ODbOfAgent" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="FindByOffRequest" TypeName="TSP.DataManager.OfficeAgentManager">
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
        <asp:HiddenField ID="Mode" runat="server" Visible="False" />

</asp:Content>
