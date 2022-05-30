<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="ImplementerAgent.aspx.cs" Inherits="Employee_TechnicalServices_Project_ImplementerAgent"
    Title="نماینده مجری" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
               
                                                    <table >
                                                        <tbody>
                                                            <tr>
                                                                <td>
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
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
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
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/view.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیر فعال"
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
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/disactive.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                        ID="btnBack" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnBack_Click">
                                                                        <ClientSideEvents Click="function(s, e) {


}"></ClientSideEvents>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/Back.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                <TSP:ProjectInfo ID="prjInfo" runat="server" />                    
                <br />
                <TSPControls:CustomAspxDevGridView Width="100%" ID="CustomAspxDevGridView1" runat="server"
                    DataSourceID="ObjectDataSource1"  
                    ClientInstanceName="grid" KeyFieldName="ImplementerAgentId" AutoGenerateColumns="False"
                    OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared"
                    OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize">
                    <Columns>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MemberTypeTitle" Caption="نوع نماینده">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Name" Caption="نام نماینده">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Code" Caption="کد عضویت">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="No" Caption="شماره پروانه">
                            <cellstyle wrap="False"></cellstyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="CreateDate" Caption="تاریخ ایجاد">
                            <cellstyle wrap="False"></cellstyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="InActiveName" Caption="وضعیت">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="InActiveDate" Caption="تاریخ تغییر وضعیت">
                            <cellstyle wrap="False"></cellstyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="PrjReId">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="9" ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>                  
                </TSPControls:CustomAspxDevGridView>
                <br />
                     <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                    <table  >
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                        ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="BtnNew_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/new.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
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
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/view.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیر فعال"
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
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/disactive.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                        ID="ASPxButton5" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnBack_Click">
                                                                        <ClientSideEvents Click="function(s, e) {


}"></ClientSideEvents>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/Back.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                              </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                    BackgroundCssClass="modalProgressGreyBackground">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                            <img src="../../../Image/indicator.gif" align="middle" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="TSP.DataManager.TechnicalServices.ImplementerAgentManager"
                    SelectMethod="FindByProjectIdPrjImpIdPrjReId" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="ProjectId"></asp:Parameter>
                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="PrjImpId"></asp:Parameter>
                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="PrjReId"></asp:Parameter>
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:HiddenField ID="HDProjectId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="RequestId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="HDMode" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="HDImpId" runat="server" Visible="False"></asp:HiddenField>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
