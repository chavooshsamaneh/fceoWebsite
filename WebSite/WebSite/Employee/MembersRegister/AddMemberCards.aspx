<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddMemberCards.aspx.cs" Inherits="Employee_MembersRegister_AddMemberCards"
    Title="مشخصات کارت عضویت" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div align="right" id="DivReport" class="DivErrors" runat="server" dir="rtl">
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
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="btnNew" EnableClientSideAPI="True" AutoPostBack="False"
                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnNew_Click">

                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" EnableClientSideAPI="True"
                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnEditClient"
                                            OnClick="btnEdit_Click">

                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" ClientInstanceName="btnSaveClient" OnClick="btnSave_Click">

                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره و تایید اتوماتیک گردش کار"
                                            ID="btnSaveAndAutoConfirm" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSaveAndAutoConfirm_Click">
                                            <ClientSideEvents Click="function(s,e){ e.processOnServer= confirm('پس از ثبت کارت عضویت با وضعیت تایید شده امکان ویرایش آن وجود ندارد.آیا مطمئن به ثبت  به صورت تایید شده می باشید؟');}" />

                                            <Image Url="~/Images/icons/SaveMeCardAndAutoConfirm.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
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

            <div width="100%" align="center">

                <dxe:ASPxLabel runat="server" Text="وضعیت درخواست:" Font-Bold="False" ID="lblWorkFlowState"
                    ForeColor="Red">
                </dxe:ASPxLabel>

            </div>
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelMeCards" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <div align="right">
                            <asp:Label ID="lblErrorInMemberData" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                        </div>
                        <table width="100%">
                            <tbody>
                           
                                <tr>
                                    <td valign="top" align="right" style="width: 15%">
                                        <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="ASPxLabel1" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" style="width: 35%">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMeId" Width="100%" AutoPostBack="True"
                                            OnTextChanged="txtMeId_TextChanged">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="کد عضویت را با فرمت صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right" style="width: 15%">
                                        <dxe:ASPxLabel runat="server" Text="علت درخواست" ID="ASPxLabel8">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" style="width: 35%">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            RightToLeft="True" ID="cmbCardType" ValueType="System.String"
                                            Width="100%">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="علت درخواست را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <Items>
                                                <dxe:ListEditItem Value="-1" Text="------------"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="0" Text="صدور کارت جدید"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="1" Text="مخدوش شدن کارت"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="2" Text="عیب فنی کارت"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="3" Text="تغییر اطلاعات عضو"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="4" Text="مفقود شدن کارت"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="5" Text="کارت موقت"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="6" Text="دریافت پروانه اشتغال"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="7" Text="کارشناس ماده 27"></dxe:ListEditItem>
                                            </Items>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel2" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td dir="rtl" valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtName" Width="100%" ReadOnly="True">
                                            <ReadOnlyStyle BackColor="Snow">
                                            </ReadOnlyStyle>
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                       <td align="center" colspan="2" rowspan="3" valign="middle" style="width: 50%">
                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="ImgMember">
                                            <EmptyImage Height="75px" Url="~/Images/Person.png" Width="75px" />
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="ASPxLabel4" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td dir="rtl" valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFamily" Width="100%" ReadOnly="True">
                                            <ReadOnlyStyle BackColor="Snow">
                                            </ReadOnlyStyle>
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
                                    <td valign="top" align="right" style="width: 15%">
                                        <dxe:ASPxLabel runat="server" Text="نام انگلیسی" ID="ASPxLabel7" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td dir="rtl" valign="top" align="right" style="width: 35%">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFirstNameEn" Width="100%"
                                            ReadOnly="True">
                                            <ReadOnlyStyle BackColor="Snow">
                                            </ReadOnlyStyle>
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
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام خانوادگی انگلیسی" ID="ASPxLabel12" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td dir="rtl" valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFamilyNameEn" Width="100%"
                                            ReadOnly="True">
                                            <ReadOnlyStyle BackColor="Snow">
                                            </ReadOnlyStyle>
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right" style="width: 15%">
                                        <dxe:ASPxLabel runat="server" Text="کد ملی" ID="ASPxLabel3" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" dir="rtl" align="right" style="width: 35%">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtSSN" Width="100%" ReadOnly="True">
                                            <ReadOnlyStyle BackColor="Snow">
                                            </ReadOnlyStyle>
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="رشته" ID="ASPxLabel11" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" dir="rtl">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMajor" Width="100%" ReadOnly="True">
                                            <ReadOnlyStyle BackColor="Snow">
                                            </ReadOnlyStyle>
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="شماره عضویت" Width="100%" ID="ASPxLabel5">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMeNo" Width="100%" ReadOnly="True">
                                            <ReadOnlyStyle BackColor="Snow">
                                            </ReadOnlyStyle>
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
                                         <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="شماره پروانه" Width="100%" ID="ASPxLabel6">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFileNo" Width="100%" ReadOnly="True">
                                            <ReadOnlyStyle BackColor="Snow">
                                            </ReadOnlyStyle>
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="شماره پروانه ماده 27" Width="100%" ID="ASPxLabel9">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtExertFilNo" Width="100%" ReadOnly="True">
                                            <ReadOnlyStyle BackColor="Snow">
                                            </ReadOnlyStyle>
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
                            </tbody>
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
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="btnNew2" EnableClientSideAPI="True" AutoPostBack="False"
                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnNewClient2"
                                            OnClick="btnNew_Click">
                                           
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit2" EnableClientSideAPI="True"
                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnEditClient2"
                                            OnClick="btnEdit_Click">
                                           
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" ClientInstanceName="btnSaveClient2" OnClick="btnSave_Click">
                                          
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره و تایید اتوماتیک گردش کار"
                                            ID="btnSaveAndAutoConfirm2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSaveAndAutoConfirm_Click">
                                            <ClientSideEvents Click="function(s,e){ e.processOnServer= confirm('پس از ثبت کارت عضویت با وضعیت تایید شده امکان ویرایش آن وجود ندارد.آیا مطمئن به ثبت  به صورت تایید شده می باشید؟');}" />
                                           
                                            <Image Url="~/Images/icons/SaveMeCardAndAutoConfirm.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
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
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldMemberCards" ClientInstanceName="HiddenFieldMemberCards">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjectDataSourceDrpSex" runat="server" CacheDuration="30"
                TypeName="TSP.DataManager.SexManager" SelectMethod="GetData"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceDrpMar" runat="server" CacheDuration="30"
                TypeName="TSP.DataManager.MaritalStatusManager" SelectMethod="GetData" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceDrpPart" runat="server" CacheDuration="30"
                TypeName="TSP.DataManager.PartitionManager" SelectMethod="GetData"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceDrpRel" runat="server" CacheDuration="30"
                TypeName="TSP.DataManager.ReligionManager" SelectMethod="GetData"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceAgent" runat="server" CacheDuration="30"
                TypeName="TSP.DataManager.AccountingAgentManager" SelectMethod="GetData"></asp:ObjectDataSource>
            <dxhf:ASPxHiddenField ID="HDFlpMember" runat="server" ClientInstanceName="flpme">
            </dxhf:ASPxHiddenField>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
        DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>

</asp:Content>
