<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddForm.aspx.cs" Inherits="Employee_HomePage_AddForm"
    Title="مشخصات فرم" %>

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
        <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
            [<a class="closeLink" href="#">بستن</a>]</div>
         <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                        <table>
                                            <tr>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                                        ToolTip="جدید" UseSubmitBehavior="False">
                                                     
                                                        <Image  Url="~/Images/icons/new.png"  />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" CausesValidation="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                                        ToolTip="ویرایش"  UseSubmitBehavior="False">
                                                      
                                                        <Image  Url="~/Images/icons/edit.png"  />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server"  EnableTheming="False"
                                                        EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                                        
                                                        <Image  Url="~/Images/icons/save.png"  />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td >
                                                  <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnback" runat="server" CausesValidation="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                        ToolTip="بازگشت" UseSubmitBehavior="False">
                                                       
                                                        <Image  Url="~/Images/icons/Back.png"  />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </table>
                      </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
        <br />
    	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

                        <table dir="rtl" width="100%">
                            <tr>
                                <td align="right" valign="top" style="width: 15%">
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="کد فرم *">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" style="width: 35%">
                                    <TSPControls:CustomTextBox IsMenuButton="true" ID="txtFoCode" runat="server" Width="100%" 
                                        >
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <RequiredField ErrorText="کد فرم را وارد نمایید" IsRequired="True" />
                                            
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td align="right" valign="top" style="width: 15%">
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="نوع فرم *">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top" style="width: 35%">                                
                                    <TSPControls:CustomAspxComboBox runat="server"  Width="100%" TextField="FormTypeName"
                                        ID="cmbFormType"  DataSourceID="ObjdsFormsType"
                                        ValueType="System.String" RightToLeft="True" ValueField="FormTypeId" >
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RequiredField IsRequired="True" ErrorText="نوع فرم را انتخاب نمایید"></RequiredField>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomAspxComboBox>
                                    <asp:ObjectDataSource ID="ObjdsFormsType" runat="server"  TypeName="TSP.DataManager.FormsTypeManager"
                                        SelectMethod="SelectActiveType"></asp:ObjectDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="عنوان فرم *">
                                    </dxe:ASPxLabel>
                                </td>
                                <td colspan="3" align="right" valign="top">
                                    <TSPControls:CustomTextBox IsMenuButton="true" ID="txtFoName" runat="server" Width="100%" 
                                        >
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <RequiredField ErrorText="نام فرم را وارد نمایید" IsRequired="True" />
                                            
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="توضیحات">
                                    </dxe:ASPxLabel>
                                </td>
                                <td colspan="3" align="right" valign="top">
                                    <TSPControls:CustomASPXMemo ID="txtDesc" runat="server" Height="37px" Width="100%" 
                                        >
                                        <ValidationSettings>
                                            
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomASPXMemo>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="فایل">
                                    </dxe:ASPxLabel>
                                </td>
                                <td colspan="3" align="right" valign="top">
                                    <TSPControls:CustomAspxUploadControl ID="flp" runat="server" InputType="Files" MaxSizeForUploadFile="0"
                                        ShowProgressPanel="True">
                                    </TSPControls:CustomAspxUploadControl>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="right" valign="top" dir="ltr">
                                    <dxe:ASPxHyperLink ID="HpLink" runat="server" Target="_blank" Text="آدرس فایل">
                                    </dxe:ASPxHyperLink>
                                </td>
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
                                        <table >
                                            <tr>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server" CausesValidation="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                                        ToolTip="جدید" UseSubmitBehavior="False">
                                                        <Image  Url="~/Images/icons/new.png"  />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" CausesValidation="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                                        ToolTip="ویرایش"  UseSubmitBehavior="False">
                                                        <Image  Url="~/Images/icons/edit.png"  />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server"  EnableTheming="False"
                                                        EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                                        <Image  Url="~/Images/icons/save.png"  />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td >
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnback2" runat="server" CausesValidation="False" 
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                        ToolTip="بازگشت" UseSubmitBehavior="False">
                                                        <Image  Url="~/Images/icons/Back.png"  />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </table>
                       </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
        <asp:HiddenField ID="FormId" runat="server" Visible="False" />
        <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
</asp:Content>
