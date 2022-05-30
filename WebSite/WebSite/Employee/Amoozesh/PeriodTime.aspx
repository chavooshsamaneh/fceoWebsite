<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PeriodTime.aspx.cs" MasterPageFile="~/MasterPagePortals.master" Inherits="Employee_Amoozesh_PeriodTime" %>

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

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>   <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
                 <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  

                                    <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: top; text-align: right">
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "  EnableTheming="False" ToolTip="جدید" ID="BtnNew" EnableViewState="False" OnClick="BtnNew_Click">
                                                                        <Image  Url="~/Images/icons/new.png"></Image>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "  Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit" EnableViewState="False" OnClick="btnEdit_Click">
                                                                        <Image  Url="~/Images/icons/edit.png"></Image>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "  EnableTheming="False" ToolTip="مشاهده" ID="btnView" EnableViewState="False" OnClick="btnView_Click">
                                                                        <Image  Url="~/Images/icons/view.png"></Image>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True" Text=" "  EnableTheming="False" ToolTip="حذف" ID="btnDelete" EnableViewState="False" OnClick="btnDelete_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>

                                                                        <Image  Url="~/Images/icons/delete.png"></Image>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "  EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click">
                                                                        <Image  Url="~/Images/icons/Back.png"></Image>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                                        </HoverStyle>
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
                    <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" Width="368px"   KeyFieldName="PTTId" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False">
                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                        <Styles  >
                            <Header HorizontalAlign="Center">
                            </Header>
                            <GroupPanel ForeColor="Black">
                            </GroupPanel>
                        </Styles>
                          
                        <Columns>
                            <dxwgv:GridViewDataTextColumn FieldName="PTTId" Name="PTTId" Visible="False" VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="عنوان" FieldName="PeriodName" Name="PeriodName"
                                VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="روز" FieldName="DayName" Name="DayName" VisibleIndex="1">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="ساعت شروع" FieldName="StartTime" Name="StartTime"
                                VisibleIndex="2">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="ساعت پایان" FieldName="EndTime" Name="EndTime"
                                VisibleIndex="3">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                      
                    </TSPControls:CustomAspxDevGridView>
                    <asp:HiddenField ID="PPId" runat="server" Visible="False"></asp:HiddenField>
                    <br />
                  <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


   
                                    <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: top; text-align: right">
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "  EnableTheming="False" ToolTip="جدید" ID="BtnNew2" EnableViewState="False" OnClick="BtnNew_Click">
                                                                        <Image  Url="~/Images/icons/new.png"></Image>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "  Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit2" EnableViewState="False" OnClick="btnEdit_Click">
                                                                        <Image  Url="~/Images/icons/edit.png"></Image>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "  EnableTheming="False" ToolTip="مشاهده" ID="btnView2" EnableViewState="False" OnClick="btnView_Click">
                                                                        <Image  Url="~/Images/icons/view.png"></Image>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True" Text=" "  EnableTheming="False" ToolTip="حذف" ID="btnDelete2" EnableViewState="False" OnClick="btnDelete_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>

                                                                        <Image  Url="~/Images/icons/delete.png"></Image>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "  EnableTheming="False" ToolTip="بازگشت" ID="ASPxButton6" EnableViewState="False" OnClick="btnBack_Click">
                                                                        <Image  Url="~/Images/icons/Back.png"></Image>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                                        </HoverStyle>
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
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="TSP.DataManager.PeriodTimeTableManager" SelectMethod="GetData" FilterExpression="PPId={0}">
                        <FilterParameters>
                            <asp:Parameter Name="newparameter" />
                        </FilterParameters>
                    </asp:ObjectDataSource>
                    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0" BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                        <ProgressTemplate>
                            <div class="modalPopup">
                                لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                            </div>
                        </ProgressTemplate>
                    </asp:ModalUpdateProgress>
          
            </ContentTemplate>
        </asp:UpdatePanel>

</asp:Content>


