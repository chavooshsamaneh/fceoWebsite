<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="Nezam.aspx.cs" Inherits="Employee_Nezam_Nezam" Title="مشخصات سازمان" %>

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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if(flpmesign.Get('name')!=1)
	{
		lble.SetVisible(true);
		e.processOnServer=false;
	}
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/save.png">
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
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="جدید" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel1">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomTextBox runat="server"
                                            ID="txtName">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" ErrorText="نام سازمان را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="شهر" ID="ASPxLabel2">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ReadOnly="True"
                                            ID="txtCitName">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" ErrorText="نام مدير سازمان را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right"></td>
                                    <td valign="top" align="right"></td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تلفن 1" ID="ASPxLabel3">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server"
                                            ID="txtTel1">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" ErrorText="تلفن را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تلفن 2" ID="ASPxLabel4">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server"
                                            ID="txtTel2">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText="تلفن را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="فكس" ID="ASPxLabel6">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server"
                                            ID="txtFax">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField ErrorText=""></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="کد پستی" Width="57px" ID="ASPxLabel11">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" MaxLength="20"
                                            ID="txtCodePO" Style="direction: ltr">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText=""></RegularExpression>
                                                <RequiredField ErrorText=""></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="آدرس وب سايت" ID="ASPxLabel7">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomTextBox runat="server"
                                            ID="txtWebsite">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText="آدرس وب سایت را با فرمت http://www.Example.com وارد نمایید"
                                                    ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="آدرس پست الکترونیکى" Width="118px" ID="ASPxLabel8">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomTextBox runat="server"
                                            ID="txtEmail">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText="این آدرس صحیح نیست" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="آدرس" ID="ASPxLabel9">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="26px"
                                            ID="txtAddress">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="آدرس را وارد نماييد"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" id="Td1" valign="top" align="right">
                                        <asp:Label runat="server" Text="تصویر مهر سازمان" ID="Label42"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td2" valign="top" align="right" colspan="3">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" InputType="Files"
                                                            ClientInstanceName="flps" ShowProgressPanel="True" ID="flpSign" OnFileUploadComplete="flpSign_FileUploadComplete">

                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
	
	imgEndUploadImgClientSign.SetVisible(true);
	 flpmesign.Set('name',1);
	lble.SetVisible(false);
	hSign.SetVisible(true);
	hSign.SetNavigateUrl('../../Image/Temp/'+e.callbackData);
}"></ClientSideEvents>

                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="تصویر مهر را انتخاب نمایید" ClientInstanceName="lble"
                                                            ClientVisible="False" ForeColor="Red" ID="ASPxLabel20">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد"
                                                            ClientInstanceName="imgEndUploadImgClientSign" ClientVisible="False" ID="ASPxImage4">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink runat="server" Text="تصویر مهر سازمان" Target="_blank" ClientInstanceName="hSign"
                                            ClientVisible="False" ID="HpSign">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" id="Td3" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تصویر سربرگ" ID="ASPxLabel5">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td4" valign="top" align="right" colspan="3">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" InputType="Files"
                                                            ClientInstanceName="flpi" ShowProgressPanel="True" ID="flpHeader" OnFileUploadComplete="flpHeader_FileUploadComplete">

                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
	imgEndUploadImgClientIdNo.SetVisible(true);

	hHeader.SetVisible(true);
	hHeader.SetNavigateUrl('../../Image/Temp/'+e.callbackData);
}"></ClientSideEvents>

                                                        </TSPControls:CustomAspxUploadControl>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد"
                                                            ClientInstanceName="imgEndUploadImgClientIdNo" ClientVisible="False" ID="imgEndUploadImgIdNo">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink runat="server" Text="تصویر سربرگ" Target="_blank" ClientInstanceName="hHeader"
                                            ClientVisible="False" ID="HpHeader">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" valign="top" align="right" colspan="4">
                                        <br />
                                        <dxe:ASPxLabel runat="server" Text="اطلاعات تماس(جهت نمایش در صفحه تماس با ما)" ID="ASPxLabel12">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" valign="top" align="right" colspan="4">                                 
                                        <TSPControls:CustomASPxHtmlEditor  ID="txtDesc" runat="server"></TSPControls:CustomASPxHtmlEditor>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
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
                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if(flpmesign.Get('name')!=1)
	{
		lble.SetVisible(true);
		e.processOnServer=false;
	}
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpSign" runat="server"
                ClientInstanceName="flpmesign">
            </dxhf:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
