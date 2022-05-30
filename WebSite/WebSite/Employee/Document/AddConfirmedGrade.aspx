<%@ Page Title="مشخصات پایه تایید شده" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="AddConfirmedGrade.aspx.cs" Inherits="Employee_Document_AddConfirmedGrade" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
    <div id="Content" runat="server" style="width: 100%; display: block;" align="center">
        <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
            [<a class="closeLink" href="#">بستن</a>]
        </div>

        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>
                    <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                        <tr>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False" 
                                    EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                    ToolTip="جدید" UseSubmitBehavior="False">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" CausesValidation="False" 
                                    EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                    ToolTip="ویرایش" Width="25px" UseSubmitBehavior="False">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server"  EnableTheming="False"
                                    EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                </TSPControls:MenuSeprator>
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

                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanelMenu>
        <br />

        <TSPControls:CustomASPxRoundPanel ID="RoundPanelMain" HeaderText="مشاهده" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>


                    <table dir="rtl" width="100%">
                        <tr>
                            <td align="right" valign="top">
                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="رشته">
                                </dxe:ASPxLabel>
                            </td>
                            <td colspan="3" align="right" valign="top">
                                <TSPControls:CustomAspxComboBox ID="comboMajor" runat="server" 
                                      ValueType="System.String"
                                    TextField="MjNameCode" ValueField="MjId" RightToLeft="True" ClientInstanceName="comboMajor"
                                    DataSourceID="ObjdsMajor"  HorizontalAlign="Right" EnableIncrementalFiltering="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="True" ErrorText="رشته را انتخاب نمایید"></RequiredField>
                                    </ValidationSettings>
                                </TSPControls:CustomAspxComboBox>
                                <asp:ObjectDataSource ID="ObjdsMajor" runat="server"
                                    SelectMethod="MajorInActive" TypeName="TSP.DataManager.MajorManager">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="InActiveMajor" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                            <tr>
                                <td align="right" valign="top" style="width: 15%">
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="پایه *">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" style="width: 35%">
                                    <TSPControls:CustomAspxComboBox ID="comboGrade" runat="server" 
                                          ValueType="System.String"
                                        TextField="GrdName" ValueField="GrdId" RightToLeft="True" ClientInstanceName="CmbTask"
                                        DataSourceID="ObjdsGrade"  HorizontalAlign="Right" EnableIncrementalFiltering="True">
                                        <ItemStyle HorizontalAlign="Right" />
                                         <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="True" ErrorText="پایه را انتخاب نمایید"></RequiredField>
                                    </ValidationSettings>
                                    </TSPControls:CustomAspxComboBox>
                                    <asp:ObjectDataSource ID="ObjdsGrade" runat="server"
                                        SelectMethod="GetData" TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>

                                </td>
                                <td align="right" valign="top" style="width: 15%">
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="صلاحیت">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" style="width: 35%">
                                    <TSPControls:CustomAspxComboBox ID="comboResponsblity" runat="server" 
                                          ValueType="System.String"
                                        TextField="ResName" ValueField="ResId" RightToLeft="True" ClientInstanceName="comboResponsblity"
                                        DataSourceID="ObjdsResponsibilityType"  HorizontalAlign="Right" EnableIncrementalFiltering="True">
                                        <ItemStyle HorizontalAlign="Right" />
                                         <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                        <RequiredField IsRequired="True" ErrorText="صلاحیت را انتخاب نمایید"></RequiredField>
                                    </ValidationSettings>
                                    </TSPControls:CustomAspxComboBox>
                                    <asp:ObjectDataSource ID="ObjdsResponsibilityType" runat="server" SelectMethod="GetData"
                                        TypeName="TSP.DataManager.ResponcibilityTypeManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                                </td>
                            </tr>


                        </tr>
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanel>
        <br />
        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>
                    <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                        <tr>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server" CausesValidation="False" 
                                    EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                    ToolTip="جدید" UseSubmitBehavior="False">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" CausesValidation="False" 
                                    EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                    ToolTip="ویرایش" Width="25px" UseSubmitBehavior="False">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server"  EnableTheming="False"
                                    EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                    </HoverStyle>
                                    <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td width="10px" align="center">
                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                </TSPControls:MenuSeprator>
                            </td>
                            <td >
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton6" runat="server" CausesValidation="False" 
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
        <dx:ASPxHiddenField ID="HiddenFieldPage" runat="server">
        </dx:ASPxHiddenField>
    </div>
</asp:Content>


