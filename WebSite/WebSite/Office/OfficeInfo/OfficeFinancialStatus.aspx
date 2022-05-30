<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeFinancialStatus.aspx.cs" Inherits="Office_OfficeInfo_OfficeFinancialStatus"
    Title="وضعیت مالی" %>

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
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
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
                                                                    ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="BtnNew_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/new.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                    Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="btnEdit_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
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
	if (jgrid.GetFocusedRowIndex()&lt;0)
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
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}" />
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
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
                                                                    CausesValidation="False" ID="ASPxButton2" UseSubmitBehavior="False" EnableViewState="False"
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
                         SeparatorHeight="100%" SeparatorColor="#A5A6A8"
                        AutoSeparators="RootOnly" OnItemClick="ASPxMenu1_ItemClick" 
                        ItemSpacing="0px" SeparatorWidth="1px" RightToLeft="True">
                        <Items>
                            <dxm:MenuItem Text="مشخصات شرکت" Name="Office">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Member" Text="اعضا">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="شعبه ها" Name="Agent">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="سوابق کاری" Name="Job">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="روزنامه های رسمی" Name="Letters">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Financial" Text="وضعیت مالی" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="مستندات" Name="Attach">
                            </dxm:MenuItem>
                        </Items>
                    </TSPControls:CustomAspxMenuHorizontal>
              
                <br />
             
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                    ClientInstanceName="jgrid"  
                    DataSourceID="ObjectDataSource1" KeyFieldName="OfsId" OnDetailRowExpandedChanged="CustomAspxDevGridView1_DetailRowExpandedChanged"
                    OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize"
                    Width="100%">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	//jgrid.ExpandDetailRow(jgrid.GetFocusedRowIndex());
}" />
                    <Templates>
                        <DetailRow>
                            <div align="center">
                                <TSPControls:CustomAspxDevGridView ID="GridViewJudge" runat="server" AutoGenerateColumns="False"
                                      DataSourceID="ObjdsJudgment"
                                    EnableViewState="False" KeyFieldName="JudgeId" OnBeforePerformDataSelect="GridViewJudge_BeforePerformDataSelect"
                                    OnHtmlDataCellPrepared="GridViewJudge_HtmlDataCellPrepared" Width="100%">
                                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" ColumnResizeMode="control" />
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="0">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="1">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="NcName" VisibleIndex="2">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="شماره جلسه" FieldName="MeetingId" VisibleIndex="3">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="تاریخ جلسه" FieldName="MeetingDate" VisibleIndex="4">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="امتیاز" FieldName="FactValue" VisibleIndex="5">
                                            <PropertiesTextEdit DisplayFormatString="#.###">
                                                <Style HorizontalAlign="Right"></Style>
                                            </PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="7" Width="1px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نظر کارشناسی" FieldName="JudgeViewPoint" VisibleIndex="8"
                                            Width="200px">
                                            <CellStyle Wrap="True">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="Confirm" VisibleIndex="6">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowGroupPanel="True" ShowHorizontalScrollBar="true" />
                                    <SettingsDetail IsDetailGrid="True" />
                                </TSPControls:CustomAspxDevGridView>
                            </div>
                        </DetailRow>
                    </Templates>
                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="OfsId" Name="OfsId" Visible="False" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام وضعیت" Width="300px" FieldName="Name"
                            Name="Name" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ضریب" FieldName="Factor" VisibleIndex="1"
                            Width="60px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="امتیاز" FieldName="FactorValue" VisibleIndex="2"
                            Width="60px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                            <PropertiesTextEdit DisplayFormatString="#.###">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ ایجاد" FieldName="CreateDate" VisibleIndex="3">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="4"
                            Width="60px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="4" ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="OfReId" Visible="False" VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFilterRowMenu="true" ShowHorizontalScrollBar="true" />
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                </TSPControls:CustomAspxDevGridView>
                <br />
                  <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                    ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="BtnNew_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/new.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                    Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnEdit_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
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
                                                                    ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="btnView_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
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
                                                                    OnClick="btnInActive_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}" />
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                    CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
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
                                                                    CausesValidation="False" ID="ASPxButton3" UseSubmitBehavior="False" EnableViewState="False"
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
       
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="FindByOffRequest"
            TypeName="TSP.DataManager.DocOffOfficeFinancialStatusManager">
            <FilterParameters>
                <asp:Parameter Name="newparameter" />
            </FilterParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="OfId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="OfReId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="IsConfirm" Type="Int16" />
                <asp:Parameter DefaultValue="-1" Name="JustActive" Type="Int16" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="OfficeId" runat="server" Visible="False" />
        <asp:HiddenField ID="PgMode" runat="server" />
        <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False" />
        <asp:HiddenField ID="HDMode" runat="server" Visible="False" />
        <asp:ObjectDataSource ID="ObjdsJudgment" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="SelectByFinancial" TypeName="TSP.DataManager.TrainingJudgmentManager">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="-1" Name="PkId" SessionField="PKId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="JudgeId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

</asp:Content>
