<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="FinancialStatus.aspx.cs" Inherits="Members_ImplementDoc_FinancialStatus"
    Title="توان مالی" %>

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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]</div>
                        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 27px; height: 27px">
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image Height="25px" Width="25px" Url="~/Images/icons/new.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td style="width: 27px; height: 27px">
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش" Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image Height="25px" Width="25px" Url="~/Images/icons/edit.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td style="width: 27px; height: 27px">
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده" ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnView_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image Height="25px" Width="25px" Url="~/Images/icons/view.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td style="width: 27px; height: 27px">
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت" CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image Height="25px" Width="25px" Url="~/Images/icons/Back.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                              </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <div style="width: 100%" dir="rtl" align="right">
                    <TSPControls:CustomAspxMenuHorizontal ID="MenuMemberFile" runat="server">
                        <Items>
                            <dxm:MenuItem Text="مشخصات مجوز" Name="ImplDoc"></dxm:MenuItem>
                            <dxm:MenuItem Text="سابقه کار" Name="JobHistory"></dxm:MenuItem>
                            <dxm:MenuItem Selected="True" Text="توان مالی">
                            </dxm:MenuItem>
                        </Items>

                    </TSPControls:CustomAspxMenuHorizontal>
                </div>
                <br />
                <TSPControls:CustomAspxDevGridView ID="GridViewFinancialStatus" runat="server" DataSourceID="ObjdsFinancialStatus" Width="100%" CssFilePath="~/App_Themes/Glass/{0}/styles.css" CssPostfix="Glass" KeyFieldName="OfsId" AutoGenerateColumns="False" ClientInstanceName="jgrid" OnAutoFilterCellEditorInitialize="GridViewFinancialStatus_AutoFilterCellEditorInitialize" OnHtmlDataCellPrepared="GridViewFinancialStatus_HtmlDataCellPrepared">
                  
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="OfsId" Name="OfsId"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Name" Caption="نام وضعیت" Name="Name"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Factor" Caption="ضریب"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="FactorValue" Caption="امتیاز">
                            <CellStyle Wrap="False"></CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="CreateDate" Caption="تاریخ ایجاد">
                            <CellStyle Wrap="False"></CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="4" Caption=" " ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>

                </TSPControls:CustomAspxDevGridView>
                <br />
                                                    <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldFinantialStatus"></dxhf:ASPxHiddenField>
                         <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 27px; height: 27px">
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image Height="25px" Width="25px" Url="~/Images/icons/new.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td style="width: 27px; height: 27px">
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش" Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image Height="25px" Width="25px" Url="~/Images/icons/edit.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td style="width: 27px; height: 27px">
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده" ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnView_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image Height="25px" Width="25px" Url="~/Images/icons/view.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td style="width: 27px; height: 27px">
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت" CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image Height="25px" Width="25px" Url="~/Images/icons/Back.png"></Image>
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
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
        <asp:ObjectDataSource ID="ObjdsFinancialStatus" runat="server" FilterExpression="MeId={0}"
            SelectMethod="SelectForImplementDoc" TypeName="TSP.DataManager.DocOffOfficeFinancialStatusManager">
            <FilterParameters>
                <asp:Parameter Name="newparameter" />
            </FilterParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="OfReId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

</asp:Content>
