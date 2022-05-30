<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="InstitueManager.aspx.cs" Inherits="Institue_InstitueInfo_InstitueManager" Title="هیئت اجرایی مؤسسه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click">
                                    <image url="~/Images/icons/Back.png"></image>

                                    <hoverstyle backcolor="#FFE0C0">
<Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
</hoverstyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <TSPControls:CustomAspxMenuHorizontal ID="MenuInstitue" runat="server"
        OnItemClick="ASPxMenu1_ItemClick">
        <Items>
            <dxm:MenuItem Name="BasicInfo" Text="اطلاعات پایه">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Manager" Text="هیئت اجرایی" Selected="true">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Activity" Text="زمینه های فعالیت">
            </dxm:MenuItem>
            <dxm:MenuItem Name="Facility" Text="امکانات و تجهیزات">
            </dxm:MenuItem>
            <dxm:MenuItem Name="InsTeacher" Text="اساتید مؤسسه">
            </dxm:MenuItem>
        </Items>
    </TSPControls:CustomAspxMenuHorizontal>
    <TSPControls:CustomASPxRoundPanel ID="RoundPanelInsManager" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <TSPControls:CustomAspxDevGridView runat="server" ClientInstanceName="GridViewInsManager" KeyFieldName="InsMngId" AutoGenerateColumns="False" DataSourceID="ObjdsInsManager" Width="100%" ID="GridViewInsManager" EnableViewState="False">
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="Name" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="Family" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره شناسنامه" FieldName="IdNo" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام پدر" FieldName="Father" VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="رشته تحصیلی" FieldName="MjName" VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="مدرک تحصیلی" FieldName="LiName" VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="میزان سهم(درصد)" FieldName="InsShares" VisibleIndex="6">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="7" ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
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
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " EnableTheming="False" ToolTip="بازگشت" ID="btnBack2" EnableViewState="False" OnClick="btnBack_Click">
                                    <image url="~/Images/icons/Back.png"></image>

                                    <hoverstyle backcolor="#FFE0C0">
<Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
</hoverstyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldInsManager"></dxhf:ASPxHiddenField>
    <asp:ObjectDataSource ID="ObjdsInsManager" runat="server" SelectMethod="SelectByCertificate" TypeName="TSP.DataManager.InstitueManagerManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="InsCId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="InsId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>


