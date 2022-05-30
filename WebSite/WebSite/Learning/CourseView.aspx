<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master"
    AutoEventWireup="true" CodeFile="CourseView.aspx.cs" Inherits="Members_Amoozesh_CourseView"
    Title="مشخصات درس" %>

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
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.1" Namespace="DevExpress.Web.ASPxTreeList"
    TagPrefix="dxwtl" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" "
                                                            EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click"
                                                            UseSubmitBehavior="False">
                                                            <image url="~/Images/icons/Back.png">
                                                                </image>
                                                            <hoverstyle backcolor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                </hoverstyle>
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
            <TSPControls:CustomAspxMenuHorizontal ID="MenuCourseDetails" runat="server"
                OnItemClick="MenuCourseDetails_ItemClick">
                <Items>
                    <dxm:MenuItem Name="CourseDetail" Text="مشخصات درس" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="CourseDetail" Text="ارتباط با پروانه">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="CourseRefrence" Text="منابع درس">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Prerequisite" Text="پیشنیاز ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Group" Text="گروه بندی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Attachment" Text="فایل های پیوست">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>

            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="ASPxRoundPanel2"  runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="نوع واحد:" Width="47px" ID="ASPxLabel7" Visible="False">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td dir="ltr" align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox runat="server" Visible="False"
                                            ID="cmbType" ReadOnly="True"
                                            ValueType="System.String">
                                            <Items>
                                                <dxe:ListEditItem Value="1" Text="تئوری"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="عملی" Text="عملی"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="عملی-تئوری" Text="عملی-تئوری"></dxe:ListEditItem>
                                            </Items>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="نوع واحد را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td align="right" valign="top"></td>
                                    <td align="right" valign="top"></td>
                                </tr>
                                <tr>
                                    <td width="15%" align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="کد:" ID="ASPxLabel1">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td width="35%" align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="170px" ReadOnly="True"
                                            ID="txtCourseId">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="فیلد کد را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td width="15%" align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="مدت زمان اعتبار(ماه):" Width="110px" ID="ASPxLabel4">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td width="35%" align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="170px" MaxLength="3" ReadOnly="True"
                                            ID="txtValidDiuration">
                                            <MaskSettings Mask="&lt;0..999&gt;"></MaskSettings>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RegularExpression ErrorText="لطفاً این فیلد را به صورت صحیح کامل نمائید." ValidationExpression="\d{0,3}"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="نام:" ID="ASPxLabel2">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="170px" ReadOnly="True"
                                            ID="txtCourseName">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="فیلد نام را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="طول دوره(ساعت):" Width="101px" ID="ASPxLabel5">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="170px" ReadOnly="True"
                                            ID="txtDuration">
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ErrorTextPosition="Bottom">
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="طول دوره را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="فیلد را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="مدت زمان عملی(ساعت):" Width="134px" ID="ASPxLabel8">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="170px" ReadOnly="True"
                                            ID="txtbPracticalDuration">
                                            <MaskSettings Mask="&lt;0..999999&gt;"></MaskSettings>
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ErrorTextPosition="Bottom">
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="مدت زمان عملی را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="فیلد را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="مدت زمان تئوری(ساعت):" Width="130px" ID="ASPxLabel6">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="170px" ReadOnly="True"
                                            ID="txtbNonPracticalDuration">
                                            <MaskSettings Mask="&lt;0..999999&gt;"></MaskSettings>
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ErrorTextPosition="Bottom">
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="مدت زمان بازدید از کارگاه را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="فیلد را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="مدت زمان بازدید از کارگاه(ساعت):" Width="180px"
                                            ID="ASPxLabel9">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="170px" ReadOnly="True"
                                            ID="txtbWorkroomDuration">
                                            <MaskSettings Mask="&lt;0..999999&gt;"></MaskSettings>
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ErrorTextPosition="Bottom">
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="مدت زمان بازدید از کارگاه را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="فیلد را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="امتیاز:" ID="ASPxLabel3">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="170px" MaxLength="6" ReadOnly="True"
                                            ID="txtPoint">
                                            <MaskSettings Mask="&lt;0..999999&gt;"></MaskSettings>
                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ErrorTextPosition="Bottom">
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="امتیاز را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="لطفاً این فیلد را به صورت صحیح کامل نمائید." ValidationExpression="\d{0,6}"></RegularExpression>
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
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <asp:HiddenField ID="CourseId" runat="server" Visible="False"></asp:HiddenField>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" "
                                                            EnableTheming="False" ToolTip="بازگشت" ID="ASPxButton6" EnableViewState="False"
                                                            OnClick="btnBack_Click" UseSubmitBehavior="False">
                                                            <image url="~/Images/icons/Back.png">
                                                                </image>
                                                            <hoverstyle backcolor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                </hoverstyle>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
