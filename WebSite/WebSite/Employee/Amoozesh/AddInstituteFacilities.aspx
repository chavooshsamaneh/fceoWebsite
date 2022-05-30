<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddInstituteFacilities.aspx.cs" Inherits="Employee_Amoozesh_AddInstituteFacilities"
    Title="مشخصات امکانات و تجهیزات مؤسسه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="width: 100%" dir="ltr" align="center">
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                visible="true">
                <asp:label id="LabelWarning" runat="server" text="Label"></asp:label>
                [<a class="closeLink" href="#">بستن</a>]</div>
               <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                    <tbody>
                                        <tr>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="جدید" UseSubmitBehavior="False"
                                                    OnClick="btnNew_Click">
                                                    <ClientSideEvents Click="function(s, e) {

}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False"
                                                    Width="25px" OnClick="btnEdit_Click">
                                                    <ClientSideEvents Click="function(s, e) {
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False"
                                                    Width="25px" OnClick="btnSave_Click">
                                                    <ClientSideEvents Click="function(s, e) {
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server"  EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnBack_Click" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                         </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
         
                <br />
            	
                <div style="vertical-align: top; width: 100%; height: 1px; text-align: right">
                    <dxe:ASPxLabel ID="lblInsName" runat="server" Text="ASPxLabel">
                    </dxe:ASPxLabel>
                </div>
                <br /><TSPControls:CustomASPxRoundPanel ID="RoundPanelFacility" HeaderText="ویرایش" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

       
                                <table style="width: 100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top;" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نوع امکانات" ID="ASPxLabel1">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top;" colspan="3" align="right">
                                                <div dir="ltr">
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="184px" 
                                                        ID="cmbEquipmentType"  AutoPostBack="True" ValueType="System.String"
                                                         OnSelectedIndexChanged="cmbEquipmentType_SelectedIndexChanged">
                                                        <Items>
                                                            <dxe:ListEditItem Value="0" Text="تجهیزات"></dxe:ListEditItem>
                                                            <dxe:ListEditItem Value="1" Text="فضای آموزشی"></dxe:ListEditItem>
                                                        </Items>
                                                        <ValidationSettings>
                                                            <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نام تجهیزات" ID="lblEquipName">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top;" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="184px" ID="txtEquipment"
                                                    >
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top;" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تعداد" ID="lblEquipCount">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top;" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="184px" ID="txtEquipmentCount"
                                                    >
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;" align="right">
                                                <dxe:ASPxLabel runat="server" Text="فضای آموزشی" Width="78px" ID="lblFacilityName">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top;" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="184px" ID="txtFacilityName"
                                                    >
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top;" align="right">
                                                <dxe:ASPxLabel runat="server" Text="ظرفیت" ID="lblCapacity">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top;" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="184px" ID="txtCapacity"
                                                    >
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;" align="right">
                                                <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel3">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top;" colspan="3" align="right">
                                                <TSPControls:CustomASPXMemo runat="server" Height="37px"  Width="520px" ID="txtDescription"
                                                    >
                                                    <ValidationSettings>
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>



                                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                    <tbody>
                                        <tr>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew2" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="جدید" UseSubmitBehavior="False"
                                                    OnClick="btnNew_Click">
                                                    <ClientSideEvents Click="function(s, e) {
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False"
                                                    Width="25px" OnClick="btnEdit_Click">
                                                    <ClientSideEvents Click="function(s, e) {	
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False"
                                                    Width="25px" OnClick="btnSave_Click">
                                                    <ClientSideEvents Click="function(s, e) {
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server"  EnableTheming="False"
                                                    EnableViewState="False" OnClick="btnBack_Click" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                              </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                                <dxhf:ASPxHiddenField ID="HiddenFieldFacility" runat="server">
                                </dxhf:ASPxHiddenField>
                           
        <asp:modalupdateprogress id="ModalUpdateProgress2" runat="server" backgroundcssclass="modalProgressGreyBackground"
            displayafter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img align="middle" src="../../Image/indicator.gif" />
                </div>
            </ProgressTemplate>
        </asp:modalupdateprogress>
        </ContentTemplate> </asp:UpdatePanel>
</asp:Content>
