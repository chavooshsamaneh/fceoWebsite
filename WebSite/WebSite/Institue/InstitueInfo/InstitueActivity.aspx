<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="InstitueActivity.aspx.cs" Inherits="Institue_InstitueInfo_InstitueActivity"
    Title="زمینه های فعالیت موسسه" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#"><span style="color: #000000">ب</span>ستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                    ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnBack_Click">
                                    <hoverstyle backcolor="#FFE0C0">
                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                    </hoverstyle>
                                    <image url="~/Images/icons/Back.png">
                                                    </image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>

    <TSPControls:CustomAspxMenuHorizontal ID="MenuInstitue" runat="server" OnItemClick="ASPxMenu1_ItemClick">
        <Items>
            <dxm:MenuItem Name="MainInfo" Text="اطلاعات پایه">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Manager" Text="هیئت اجرایی">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Activity" Text="زمینه های فعالیت" Selected="true">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Facility" Text="امکانات و تجهیزات">
            </dxm:MenuItem>
            <dxm:MenuItem Name="InsTeacher" Text="اساتید مؤسسه">
            </dxm:MenuItem>
        </Items>
    </TSPControls:CustomAspxMenuHorizontal>
    </div>
                <br />
    <TSPControls:CustomASPxRoundPanel ID="RoundPanelInsActivity" HeaderText="زمینه های فعالیت مؤسسه" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <TSPControls:CustomAspxDevGridView Width="100%" runat="server"
                    EnableViewState="False" ID="GridViewInsActivity" DataSourceID="ObjdsInsActivity"
                    KeyFieldName="InsActId" AutoGenerateColumns="False" ClientInstanceName="GridViewInsActivity">
                    <Columns>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="InsActName" Width="200px"
                            Caption="زمینه فعالیت">
                            <PropertiesTextEdit Width="200px">
                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                    <RequiredField ErrorText="زمینه فعالیت موسسه را وارد نمایید"></RequiredField>
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataMemoColumn VisibleIndex="2" FieldName="Description" Width="200px"
                            Caption="توضیحات">
                            <PropertiesMemoEdit Width="200px"></PropertiesMemoEdit>
                        </dxwgv:GridViewDataMemoColumn>
                    </Columns>
                </TSPControls:CustomAspxDevGridView>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                    ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                    OnClick="btnBack_Click">
                                    <hoverstyle backcolor="#FFE0C0">
                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                    </hoverstyle>
                                    <image url="~/Images/icons/Back.png">
                                                    </image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldInsActivity">
    </dxhf:ASPxHiddenField>
    <asp:ObjectDataSource ID="ObjdsInsActivity" runat="server" SelectMethod="SelectByCertificate"
        TypeName="TSP.DataManager.InstitueActivityManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="InsCId"></asp:Parameter>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="InsId"></asp:Parameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
