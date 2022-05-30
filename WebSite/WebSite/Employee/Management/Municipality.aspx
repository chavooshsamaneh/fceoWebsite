<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="Municipality.aspx.cs" Inherits="Employee_Management_Municipality" Title="مدیریت شهرداری ها" %>

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
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]</div>
                         <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


 
                                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                            </HoverStyle>

                                                                            <Image  Url="~/Images/icons/new.png"></Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش" Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                            </HoverStyle>

                                                                            <Image  Url="~/Images/icons/edit.png"></Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td></td>
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده" ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnView_Click">
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                            </HoverStyle>

                                                                            <Image  Url="~/Images/icons/view.png"></Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                   </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                    <br />
                    <TSPControls:CustomAspxDevGridView ID="GridViewMunicipality" runat="server" DataSourceID="ObjdsMunicipality" Width="100%"   KeyFieldName="MunId" AutoGenerateColumns="False">
                         
                        <Columns>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MunName" Caption="نام شهرداری"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="ReCitName" Caption="شهرستان"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="CitName" Caption="شهر"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="ParentName" Caption="شهرداری مرکزی"></dxwgv:GridViewDataTextColumn>                            
                            <dxwgv:GridViewCommandColumn VisibleIndex="5" Caption=" " ShowClearFilterButton="true">
                              
                            </dxwgv:GridViewCommandColumn>
                        </Columns>

                        
                    </TSPControls:CustomAspxDevGridView>
                    <asp:ObjectDataSource ID="ObjdsMunicipality" runat="server" TypeName="TSP.DataManager.TechnicalServices.MunicipalityManager" SelectMethod="GetData"></asp:ObjectDataSource>
                    <br />
                              <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                            </HoverStyle>

                                                                            <Image  Url="~/Images/icons/new.png"></Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش" Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" __designer:wfdid="w1" OnClick="btnEdit_Click">
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                            </HoverStyle>

                                                                            <Image  Url="~/Images/icons/edit.png"></Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده" ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnView_Click">
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                            </HoverStyle>

                                                                            <Image  Url="~/Images/icons/view.png"></Image>
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

</asp:Content>

