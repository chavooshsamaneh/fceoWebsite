<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddFinancialStatus.aspx.cs" Inherits="Employee_ImplementDoc_AddFinancialStatus"
    Title="مشخصات توان مالی مجری حقیقی" %>

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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ImgTransfer ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">

                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ImgTransfer ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">

                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ImgTransfer ToolTip="ذخیره"
                                            ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">

                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ImgTransfer ToolTip="بازگشت"
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
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table dir="rtl" width="100%">
                            <tbody>
                                <tr>
                                    <td style="width: 90px" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نوع وضعیت مالی" Width="90px" ID="ASPxLabel2"></dxe:ASPxLabel>
                                    </td>
                                    <td  valign="top" align="right" colspan="3">
                                        <TSPControls:CustomAspxComboBox Width="50%" runat="server" ValueType="System.String" DataSourceID="ObjectDataSource1" TextField="Name" ValueField="OfdId"  ClientInstanceName="CmbName" ID="CmbName">
                                            <ButtonStyle Width="13px"></ButtonStyle>

                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>

                                                <RequiredField IsRequired="True" ErrorText="نوع وضعیت مالی را انتخاب نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                        &nbsp; </td>
                                    <td dir="ltr" valign="top" align="right" colspan="1">
                                        <dxe:ASPxLabel runat="server" Text="ضریب وضعیت مالی" Width="100px" ID="ASPxLabel8" Visible="False"></dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" valign="top" align="right" colspan="1">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ReadOnly="True" Visible="False" ID="txtNameValue">
                                            <validationsettings display="Dynamic" errortextposition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>

                                                    <RequiredField ErrorText=""></RequiredField>
                                                </validationsettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td  valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="مبلغ(ریال)" ID="ASPxLabel4"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="5">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtValue">
                                            <validationsettings display="Dynamic" errortextposition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>

                                                    <RequiredField IsRequired="True" ErrorText="مبلغ را وارد نمایید"></RequiredField>
                                                </validationsettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td  valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" Width="87px" ID="ASPxLabel3"></dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="5">
                                        <TSPControls:CustomASPXMemo runat="server" Height="35px"  ID="txtDesc">
                                            <ValidationSettings>
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <fieldset class="HelpUL">
                            <legend>مدارک پیوست</legend>

                            <table runat="server" id="TblFile" dir="rtl" width="100%">
                                <tr runat="server" id="Tr1">
                                    <td runat="server" id="Td1"  valign="top" align="right">
                                        <asp:Label runat="server" Text="فایل" Width="24px" ID="lblimg"></asp:Label>

                                    </td>
                                    <td runat="server" id="Td2" valign="top" align="right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" MaxSizeForUploadFile="0" InputType="Files" ClientInstanceName="flpc" ShowProgressPanel="True" ID="flp" OnFileUploadComplete="flp_FileUploadComplete">
                                                        
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
	imgEndUploadImgClient.SetVisible(true);
}" ></ClientSideEvents>

                                                            <CancelButton Text="انصراف"></CancelButton>
                                                        </TSPControls:CustomAspxUploadControl>

                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد" ClientInstanceName="imgEndUploadImgClient" ClientVisible="False" ID="imgEndUploadImg"></dxe:ASPxImage>

                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr3">
                                    <td runat="server" id="Td4"  valign="top" align="right">
                                        <asp:Label runat="server" Text="توضیحات" Width="24px" ID="Label10"></asp:Label>

                                    </td>
                                    <td runat="server" id="Td5" valign="top" align="right">
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px"  ID="txtDescImg">
                                            <ValidationSettings>
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>

                                    </td>
                                </tr>
                                <tr runat="server" id="Tr2">
                                    <td runat="server" id="Td3" align="center" colspan="2">
                                        <br />
                                        <TSPControls:CustomAspxButton  runat="server" UseSubmitBehavior="False" CausesValidation="False" Text="اضافه" Width="70px" ID="btnAddFlp" OnClick="btnAddFlp_Click"></TSPControls:CustomAspxButton>

                                    </td>
                                </tr>
                            </table>

                            <div dir="rtl">
                                <br />
                                <TSPControls:CustomAspxDevGridView2 runat="server" KeyFieldName="Id" AutoGenerateColumns="False" RightToLeft="True" Width="100%" ID="AspxGridFlp" EnableViewState="False" OnRowDeleting="AspxGridFlp_RowDeleting">
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn FieldName="FilePath" Name="FilePath" Caption="فایل" VisibleIndex="0">
                                            <DataItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" Text="آدرس فایل" Target="_blank" NavigateUrl='<%# Bind("TempImgUrl") %>'></asp:HyperLink>
                                            </DataItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
                                            </EditItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Description" Name="Description" Caption="توضیحات" VisibleIndex="1"></dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="2" ShowDeleteButton="true">
                                        </dxwgv:GridViewCommandColumn>
                                    </Columns>

                                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                </TSPControls:CustomAspxDevGridView2>

                            </div>
                        </fieldset>
                        </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ImgTransfer ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">

                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ImgTransfer ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">

                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ImgTransfer ToolTip="ذخیره"
                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">

                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ImgTransfer ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
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
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldFinancial">
            </dxhf:ASPxHiddenField>
            <asp:HiddenField ID="OfficeId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="FinancialId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="MemberFile" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="HDMode" runat="server" Visible="False"></asp:HiddenField>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.DocOffOfficeFactorDocumentsManager" FilterExpression="Type={0}">
                <FilterParameters>
                    <asp:Parameter Name="newparameter" />
                </FilterParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
