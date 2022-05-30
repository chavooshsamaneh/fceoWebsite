<%@ Page Title="درخواست تغییرات اطلاعات پایه" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberInsertBaseInfo.aspx.cs" Inherits="Members_MemberInfo_MemberInsertBaseInfo" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<%@ Register Src="~/UserControl/PhotoUploadHelp.ascx" TagName="PhotoUploadHelp" TagPrefix="UcUploadHelp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function CheckImage() {
            var Image = true;

            if (HiddenFieldPage.Get('FlpMe') == 0) {
                lblFlpMe.SetVisible(true);
                Image = false;

            }

            if (HiddenFieldPage.Get('MeIdNo') == 0) {
                lbli.SetVisible(true);
                Image = false;

            }
            if (HiddenFieldPage.Get('MeSSN') == 0) {
                lblss.SetVisible(true);
                Image = false;

            }
            if (HiddenFieldPage.Get('SSNBack') == 0) {
                lblssBack.SetVisible(true);
                Image = false;

            }
            if (HiddenFieldPage.Get('Resident') == 0) {
                lblResidentImageValidation.SetVisible(true);
                Image = false;

            }

            if (HiddenFieldPage.Get('MeSol') == 0) {
                lblsol.SetVisible(true);
                Image = false;

            }
            return Image;
        }


    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div align="right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <table>
                <tr>
                    <td>
                        <asp:LinkButton ID="btnSave" CssClass="ButtonMenue" OnClick="btnSave_Click" runat="server" OnClientClick="
                                          if (CheckImage()==false)
                                           {
                                                return false;                                       
                                           }
                                           if (ASPxClientEdit.ValidateGroup() == false)
                                           {
                                                return false;                                       
                                            }
                                         return confirm('آیا از ذخیره و ارسال درخواست به واحد عضویت مطمئن می باشید؟');
                                        ">ذخیره درخواست و ارسال به واحد عضویت</asp:LinkButton>
                    </td>

                    <td>
                        <asp:LinkButton ID="btnBack" CssClass="ButtonMenue" OnClick="btnBack_Click" runat="server">بازگشت</asp:LinkButton>

                    </td>
                </tr>
            </table>

            <br />
            <TSPControls:CustomASPxRoundPanel ID="CustomASPxRoundPanel1" ClientInstanceName="RoundPanelMain"
                HeaderText="توجه" runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent>
                        <table width="100%" cellpadding="5" align="center">
                            <tr>
                                <td align="right">
                                    <ul class="HelpUL">
                                        <li>این فرم امکان تغییر سریع اطلاعات پایه عضویت شما را فراهم می نماید. </li>
                                        <li>به منظور تغییر سایر اطلاعات عضویت خود می توانید از منوی 'واحد عضویت > مدیریت درخواست
                                            ها' اقدام نمائید. </li>
                                        <li>پس از انجام تغییرات مورد نظر جهت ثبت تغییرات و ارسال به مسئول مربوطه جهت بررسی و
                                            تایید بر روی دکمه "ذخیره درخواست و ارسال به واحد عضویت" واقع در منوی بالا/پایین صفحه کلیک نمایید. </li>
                                    </ul>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelMain" ClientInstanceName="RoundPanelMain"
                HeaderText="" runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent>
                        <table width="100%">
                            <tr>
                                <td align="right" valign="top" width="15%">
                                    <asp:Label runat="server" Text="تلفن همراه " ID="Label49"></asp:Label>
                                </td>
                                <td align="right" valign="top" width="35%">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="11" ID="txtMobile"
                                        ClientInstanceName="txtMobile">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RegularExpression ErrorText="تلفن همراه را با فرمت صحیح وارد نمائید" ValidationExpression="\d{1,16}"></RegularExpression>
                                            <RequiredField IsRequired="true" ErrorText="تلفن همراه را وارد نمایید" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td align="right" valign="top" width="15%">
                                    <asp:Label runat="server" Text="پست الکترونیک " ID="Label2"></asp:Label>
                                    <%-- <asp:Label runat="server" Text="شماره حساب " ID="Label1"></asp:Label>--%>
                                </td>
                                <td align="right" valign="top" width="35%">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtEmail"
                                        ClientInstanceName="txtEmail">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RegularExpression ErrorText="پست الکترونیکی را با فرمت صحیح وارد نمایید" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                            <RequiredField IsRequired="true" ErrorText="پست الکترونیک را وارد نمایید" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <asp:Label runat="server" Text="کد ملی " ID="Label3"></asp:Label>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="10" ID="txtSSN"
                                        ClientInstanceName="txtSSN">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RegularExpression ErrorText="کد ملی را با فرمت صحیح وارد نمائید" ValidationExpression="\d{1,16}" />
                                            <RequiredField IsRequired="true" ErrorText="کد ملی را وارد نمایید" />
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </td>
                                <td align="right" valign="top"></td>
                                <td align="right" valign="top"></td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    <asp:Label runat="server" Text="آدرس محل سکونت*" Width="110px" ID="Label43"></asp:Label>
                                </td>
                                <td valign="top" align="right" colspan="3">
                                    <TSPControls:CustomASPXMemo runat="server" Height="50px" ID="txtHomeAdr" Width="100%"
                                        ClientInstanceName="txtHomeAdr">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                            </ErrorImage>
                                            <RequiredField IsRequired="True" ErrorText="آدرس را وارد نمایید"></RequiredField>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomASPXMemo>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right" colspan="4">
                                    <UcUploadHelp:PhotoUploadHelp ID="PhotoHelp" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right">
                                    <asp:Label runat="server" Text="تصویر*" ID="Label50"></asp:Label>
                                </td>
                                <td valign="top" align="right" colspan="3">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                        ID="flpImage" InputType="Images" ClientInstanceName="flpimg" OnFileUploadComplete="flpImage_FileUploadComplete">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClient2.SetVisible(true);
	HiddenFieldPage.Set('FlpMe',1);
	lblFlpMe.SetVisible(false);
	meImg.SetImageUrl('../../Image/Members/Person/Request/'+e.callbackData);
    HiddenFieldPage.Set('MeImgUpload','~/Image/Members/Person/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClient2.SetVisible(false);
    HiddenFieldPage.Set('FlpMe',0);
	lblFlpMe.SetVisible(true);
	meImg.SetImageUrl('');
	}
}"></ClientSideEvents>
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel ID="ASPxLabel19" runat="server" ClientInstanceName="lblFlpMe" ClientVisible="False"
                                                        ForeColor="Red" Text="تصویر را انتخاب نمایید">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                        ID="ASPxImage3" ClientInstanceName="imgEndUploadImgClient2">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgMember" ClientInstanceName="meImg"
                                        Border-BorderWidth="1px" Border-BorderStyle="Solid">
                                        <EmptyImage Height="75px" Width="75px" Url="~/Images/Person.png">
                                        </EmptyImage>
                                    </dxe:ASPxImage>
                                    <br />
                                    <dxe:ASPxLabel ID="ASPxLabelImgWarning" runat="server" ForeColor="#0000C0" Text="">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td id="Td48" runat="server" align="right" valign="top" style="width: 115px">
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="تصویر صفحه اول شناسنامه*">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" colspan="3" valign="top">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl ID="flpIdNo" runat="server" ClientInstanceName="flpi"
                                                        UploadWhenFileChoosed="true" OnFileUploadComplete="flpIdNo_FileUploadComplete">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClientIdNo.SetVisible(true);
  	HiddenFieldPage.Set('MeIdNo',1);
	lbli.SetVisible(false);
	hidno.SetNavigateUrl('../../Image/Members/IdNo/Request/'+e.callbackData);
    HiddenFieldPage.Set('MeImageIdNo','~/Image/Members/IdNo/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientIdNo.SetVisible(false);
    HiddenFieldPage.Set('MeIdNo',0);
	lbli.SetVisible(true);
	hidno.SetNavigateUrl('');
	}
}" />
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel ID="ASPxLabel21" runat="server" ClientInstanceName="lbli" ClientVisible="False"
                                                        ForeColor="Red" Text="تصویر صفحه اول شناسنامه را انتخاب نمایید">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage ID="imgEndUploadImgIdNo" runat="server" ClientInstanceName="imgEndUploadImgClientIdNo"
                                                        ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxe:ASPxHyperLink ID="HpIdNo" runat="server" ClientInstanceName="hidno" Target="_blank"
                                        Text="تصویر شناسنامه">
                                    </dxe:ASPxHyperLink>
                                </td>
                            </tr>

                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel ID="ASPxLabel17" runat="server" Text="تصویر صفحه دوم شناسنامه">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" colspan="3" valign="top">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl ID="flpIdNoP2" runat="server" ClientInstanceName="flpIdNoP2"
                                                        InputType="Images" UploadWhenFileChoosed="true" OnFileUploadComplete="flpIdNo_FileUploadComplete">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientIdNoP2.SetVisible(true);
    HiddenFieldPage.Set('IdNoP2',1);
	HIdNoP2.SetNavigateUrl('../../Image/Members/IdNo/Request/'+e.callbackData);
    HiddenFieldPage.Set('MeImageIdNoP2','~/Image/Members/IdNo/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientIdNoP2.SetVisible(false);
    HiddenFieldPage.Set('IdNoP2',0);
	HIdNoP2.SetNavigateUrl('');
	}
}" />
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel ID="lblIdNoP2" runat="server" ClientInstanceName="lblIdNoP2" ClientVisible="False"
                                                        ForeColor="Red" Text="تصویر صفحه دوم شناسنامه را انتخاب نمایید">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage ID="imgEndUploadImgClientIdNoP2" runat="server" ClientInstanceName="imgEndUploadImgClientIdNoP2"
                                                        ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxe:ASPxHyperLink ID="HIdNoP2" runat="server" ClientInstanceName="HIdNoP2"
                                        Target="_blank" Text="تصویر صفحه دوم شناسنامه">
                                    </dxe:ASPxHyperLink>
                                </td>
                            </tr>

                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="تصویر صفحه توضیحات شناسنامه*">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" colspan="3" valign="top">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl ID="flpIdNoPDes" runat="server" ClientInstanceName="flpIdNoPDes"
                                                        InputType="Images" UploadWhenFileChoosed="true" OnFileUploadComplete="flpIdNo_FileUploadComplete">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientIdNoPDes.SetVisible(true);
    HiddenFieldPage.Set('IdNoPDes',1);
	lblIdNoPDes.SetVisible(false);
	HIdNoPDes.SetNavigateUrl('../../Image/Members/IdNo/Request/'+e.callbackData);
    HiddenFieldPage.Set('MeImageIdNoDes','~/Image/Members/IdNo/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientIdNoPDes.SetVisible(false);
    HiddenFieldPage.Set('IdNoPDes',0);
	lblIdNoPDes.SetVisible(true);
	HIdNoPDes.SetNavigateUrl('');
	}
}" />
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel ID="lblIdNoPDes" runat="server" ClientInstanceName="lblIdNoPDes" ClientVisible="False"
                                                        ForeColor="Red" Text="تصویر صفحه توضیحات شناسنامه را انتخاب نمایید">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage ID="imgEndUploadImgClientIdNoPDes" runat="server" ClientInstanceName="imgEndUploadImgClientIdNoPDes"
                                                        ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxe:ASPxHyperLink ID="HIdNoPDes" runat="server" ClientInstanceName="HIdNoPDes"
                                        Target="_blank" Text="تصویر صفحه توضیحات شناسنامه">
                                    </dxe:ASPxHyperLink>
                                </td>
                            </tr>


                            <tr>
                                <td align="right" valign="top" style="width: 115px">
                                    <dxe:ASPxLabel ID="ASPxLabel13" runat="server" Text="تصویر روی کارت ملی*">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" colspan="3" valign="top">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td align="right" dir="rtl">
                                                    <TSPControls:CustomAspxUploadControl ID="flpSSN" runat="server" ClientInstanceName="flpss"
                                                        OnFileUploadComplete="flpSSN_FileUploadComplete" UploadWhenFileChoosed="true">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                     if(e.isValid){
	imgEndUploadImgClientSSN.SetVisible(true);
    HiddenFieldPage.Set('MeSSN',1);
	lblss.SetVisible(false);
	hssn.SetNavigateUrl('../../image/Members/SSN/Request/'+e.callbackData);
    HiddenFieldPage.Set('FileOfSSN','~/Image/Members/SSN/Request/'+e.callbackData);
	}
	else
	{
	imgEndUploadImgClientSSN.SetVisible(false);
    HiddenFieldPage.Set('MeSSN',0);
	lblss.SetVisible(true);
	hssn.SetNavigateUrl('');
	}
}" />
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel ID="lblss" runat="server" ClientInstanceName="lblss" ClientVisible="False"
                                                        ForeColor="Red" Text="تصویر کارت ملی را انتخاب نمایید">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage ID="imgEndUploadImgSSN" runat="server" ClientInstanceName="imgEndUploadImgClientSSN"
                                                        ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxe:ASPxHyperLink ID="HpSSN" runat="server" ClientInstanceName="hssn" Target="_blank"
                                        Text="تصویر روی کارت ملی">
                                    </dxe:ASPxHyperLink>
                                </td>
                            </tr>

                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel ID="ASPxLabel18" runat="server" Text="تصویر پشت کارت ملی*">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" colspan="3" valign="top">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl ID="flpSSNBack" runat="server" ClientInstanceName="flpSSNBack"
                                                        InputType="Images" UploadWhenFileChoosed="true" OnFileUploadComplete="flpSSN_FileUploadComplete">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientSSNBack.SetVisible(true);
    HiddenFieldPage.Set('SSNBack',1);
	lblssBack.SetVisible(false);
	hssnBack.SetNavigateUrl('../../image/Members/SSN/Request/'+e.callbackData);
    HiddenFieldPage.Set('FileOfSSNBack','~/Image/Members/SSN/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientSSNBack.SetVisible(false);
    HiddenFieldPage.Set('SSNBack',0);
	lblssBack.SetVisible(true);
	hssnBack.SetNavigateUrl('');
	}
}" />
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel ID="ASPxLabel20" runat="server" ClientInstanceName="lblssBack" ClientVisible="False"
                                                        ForeColor="Red" Text="تصویر پشت کارت ملی را انتخاب نمایید">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage ID="ASPxImage2" runat="server" ClientInstanceName="imgEndUploadImgClientSSNBack"
                                                        ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxe:ASPxHyperLink ID="hssnBack" runat="server" ClientInstanceName="hssnBack"
                                        Target="_blank" Text="تصویر پشت کارت ملی">
                                    </dxe:ASPxHyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel ID="ASPxLabel27" runat="server" Text="تصویر مدرک سکونت در استان*">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" colspan="3" valign="top">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                        ID="flpResident" InputType="Images" ClientInstanceName="flpResident" OnFileUploadComplete="flpResident_FileUploadComplete">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientRes.SetVisible(true);
    HiddenFieldPage.Set('Resident',1);
	lblResidentImageValidation.SetVisible(false);
	HpResident.SetVisible(true);
	HpResident.SetNavigateUrl('../../image/Members/Resident/Request/'+e.callbackData);
    HiddenFieldPage.Set('FileOfResident','~/Image/Members/Resident/Request/'+e.callbackData);
	}

	else
    {
	imgEndUploadImgClientRes.SetVisible(false);
   
    HiddenFieldPage.Set('Resident',0);
	lblResidentImageValidation.SetVisible(true);
	HpResident.SetVisible(false);
	HpResident.SetNavigateUrl('');
	}
}" />
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel ID="lblResidentImageValidation" runat="server" ClientInstanceName="lblResidentImageValidation" ClientVisible="False"
                                                        ForeColor="Red" Text="تصویر مدرک سکونت در استان را انتخاب نمایید">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                        ID="imgEndUploadImgClientRes" ClientInstanceName="imgEndUploadImgClientRes">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxe:ASPxHyperLink ID="HpResident" runat="server" ClientInstanceName="HpResident"
                                        Target="_blank" Text="تصویر مدرک سکونت در استان">
                                    </dxe:ASPxHyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" style="width: 115px">
                                    <dxe:ASPxLabel ID="lblSolFile" ClientVisible="false" runat="server" Text="تصویر روی کارت پایان خدمت*"
                                        ClientInstanceName="lblsolfile">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" colspan="3" valign="top" dir="rtl">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl runat="server" ID="flpSoldier" UploadWhenFileChoosed="true"
                                                        ClientInstanceName="flpsol" ClientVisible="false" OnFileUploadComplete="flpSoldier_FileUploadComplete">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClientSol.SetVisible(true);
    HiddenFieldPage.Set('MeSol',1);
    //lblsolfile.SetVisible(false);
	lblsol.SetVisible(false);
	hsol.SetNavigateUrl('../../image/Members/Soldier/Request/'+e.callbackData);
    HiddenFieldPage.Set('FileOfSol','~/Image/Members/Soldier/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientSol.SetVisible(false);
    HiddenFieldPage.Set('MeSol',0);
	lblsol.SetVisible(true);
	hsol.SetNavigateUrl('');
	}
}" />
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel runat="server" Text="تصویر روی کارت پایان خدمت را انتخاب نمایید" ClientVisible="False"
                                                        ID="lblsol" ForeColor="Red" ClientInstanceName="lblsol">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                        ID="imgEndUploadImgSol" ClientInstanceName="imgEndUploadImgClientSol">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxe:ASPxHyperLink ID="HpSoldier" runat="server" ClientInstanceName="hsol"
                                        Target="_blank" Text="تصویر روی کارت پایان خدمت">
                                    </dxe:ASPxHyperLink>
                                    <TSPControls:CustomTextBox runat="server" Text="click" CausesValidation="False" ClientVisible="False"
                                        Width="62px" ID="ASPxButton3" EnableClientSideAPI="True" AutoPostBack="False"
                                        UseSubmitBehavior="False" ClientInstanceName="but">
                                    </TSPControls:CustomTextBox>
                                </td>
                            </tr>


                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel ID="lblSolFileBack" runat="server" Text="تصویر پشت کارت پایان خدمت" ClientInstanceName="lblSolFileBack">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" colspan="3" valign="top">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                        ID="flpSoldierBack" InputType="Images" ClientInstanceName="flpSoldierBack" OnFileUploadComplete="flpSoldier_FileUploadComplete">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgSolBack.SetVisible(true);
   
	HiddenFieldPage.Set('SolBack',1);
	lblSolBack.SetVisible(false);
	HpSoldierBack.SetNavigateUrl('../../image/Members/Soldier/Request/'+e.callbackData);
    HiddenFieldPage.Set('FileOfSolBack','~/Image/Members/Soldier/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgSolBack.SetVisible(false);
   
	HiddenFieldPage.Set('SolBack',0);
	lblSolBack.SetVisible(true);
	HpSoldierBack.SetNavigateUrl('');
	}
}" />
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel ID="lblSolBack" runat="server" ClientInstanceName="lblSolBack" ClientVisible="False"
                                                        ForeColor="Red" Text="تصویر پشت کارت پایان خدمت را انتخاب نمایید">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                        ID="imgEndUploadImgSolBack" ClientInstanceName="imgEndUploadImgSolBack">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxe:ASPxHyperLink ID="HpSoldierBack" runat="server" ClientInstanceName="HpSoldierBack"
                                        Target="_blank" Text="تصویر پشت کارت پایان خدمت">
                                    </dxe:ASPxHyperLink>

                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />

            <table>
                <tr>
                    <td>
                        <asp:LinkButton ID="btnSave2" CssClass="ButtonMenue" OnClick="btnSave_Click" runat="server" OnClientClick="
                                          if (CheckImage()==false)
                                           {
                                                return false;                                       
                                           }
                                           if (ASPxClientEdit.ValidateGroup() == false)
                                           {
                                                return false;                                       
                                            }
                                         return confirm('آیا از ذخیره و ارسال درخواست به واحد عضویت مطمئن می باشید؟');
                                        ">ذخیره درخواست و ارسال به واحد عضویت</asp:LinkButton>
                    </td>

                    <td>
                        <asp:LinkButton ID="btnBack2" CssClass="ButtonMenue" OnClick="btnBack_Click" runat="server">بازگشت</asp:LinkButton>

                    </td>
                </tr>
            </table>

            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <dxhf:ASPxHiddenField ID="HiddenFieldPage" runat="server" ClientInstanceName="HiddenFieldPage">
            </dxhf:ASPxHiddenField>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نماييد
                <img alt="" src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
