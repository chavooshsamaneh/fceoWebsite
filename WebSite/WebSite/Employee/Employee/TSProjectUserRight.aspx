<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="TSProjectUserRight.aspx.cs" Inherits="Employee_Employee_TSProjectUserRight" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                    ID="btnSave" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" ClientInstanceName="btnSaveClient" OnClick="btnSave_Click">
                                   
                                    <Image Url="~/Images/icons/save.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                    CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnBack_Click">
                                  
                                    <Image Url="~/Images/icons/Back.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>

    <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server" OnItemClick="ASPxMenu1_ItemClick">
        <Items>
            <dxm:MenuItem Name="Pages" Text="دسترسی صفحات">
            </dxm:MenuItem>

            <dxm:MenuItem Name="TS" Text="دسترسی خدمات مهندسی" Selected="true">
            </dxm:MenuItem>
        </Items>

    </TSPControls:CustomAspxMenuHorizontal>
    <br />

    <TSPControls:CustomASPxRoundPanel ID="RoundPanelPermission" HeaderText="سطح دسترسی جلسات" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <TSPControls:CustomAspxDevGridView runat="server" Width="100%"
                    ID="gridMunicipality" DataSourceID="ObjectMunicipality" KeyFieldName="MunId"
                    AutoGenerateColumns="False" 
                    OnCustomButtonCallback="gridMunicipality_CustomButtonCallback">
                    <Settings ShowGroupPanel="True" ShowFilterRowMenu="True" ShowFilterRow="True"></Settings>
                    <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="false"></SettingsBehavior>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MunName" Caption="نام شهرداری">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="CitName" Caption="شهر">
                        </dxwgv:GridViewDataTextColumn>
                           <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MunName" Caption="شهرداری مرکز">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" انتخاب" ShowSelectCheckbox="True"
                            Width="50px">
                            <CustomButtons>
                                <dxwgv:GridViewCommandColumnCustomButton Visibility="FilterRow" ID="SelectButtonTypes"
                                    Text="انتخاب همه">
                                </dxwgv:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="3" Caption=" " Width="30px" ShowClearFilterButton="true">
                    
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
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                    ID="btnSave2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" ClientInstanceName="btnSaveClient2" OnClick="btnSave_Click">
                                  
                                    <Image Url="~/Images/icons/save.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                    CausesValidation="False" ID="btnBack2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                    EnableViewState="False" EnableTheming="False" ClientInstanceName="btnBackClient2"
                                    OnClick="btnBack_Click">
                                   
                                    <Image Url="~/Images/icons/Back.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:ObjectDataSource ID="ObjectMunicipality" runat="server" SelectMethod="SelectTSMunicipalityParent"
        TypeName="TSP.DataManager.TechnicalServices.MunicipalityManager"></asp:ObjectDataSource>
    <dxhf:ASPxHiddenField ID="HiddenFieldQueryString" runat="server">
    </dxhf:ASPxHiddenField>

</asp:Content>

