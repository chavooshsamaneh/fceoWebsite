<%@ Page Title="درخواست درج صلاحیت جدید پروانه اشتغال-استعلام ها" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="WizardQualificationKardan.aspx.cs" Inherits="Members_Documents_WizardQualificationKardan" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div align="right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>

            <TSPControls:CustomAspxMenuHorizontal ID="MenuSteps" runat="server">

                <Items>
                    <dxm:MenuItem Text="سوگند نامه" Name="Oath">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Kardan" Text="استعلام ها" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Exams" Text="آزمون ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="JobConfirm" Text="تاییدیه سابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Summary" Text="خلاصه اطلاعات">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="End" Text="ثبت نهایی">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>

            <ul class="HelpUL">
                <li>بارگذاری تصویر پاسخ نامه یا استعلام اداره امور مالیاتی جهت تحویل گرفتن پروانه اشتغال بکار الزامی می باشد 
                </li>
                <li>در این مرحله یا تصویر استعلام را بارگذاری نمایید یا در صورت عدم پاسخ اداره امور مالیاتی تاکنون تعهد بارگذاری ان را بعد از پاسخ انتخاب نمایید </li>
                <li>لینک چاپ فرم استعلام از اداره امور مالیاتی را از قسمت الف-لینک های پرکاربرد در صفحه خانه ب-لینک های سمت راست پرتال..واحد صدور پروانه.. مدیریت پروانه اشتغال بکار..منوی صفحه مدیریت..آیکن چاپ نامه استعلام </li>
                <li>محل بارگذاری تصویر پاسخ استعلام در الف-ویزارد صدور پروانه اشتغال بکار همین صفحه ب-لینک های سمت راست پرتال..واحد صدور پروانه.. مدیریت پروانه اشتغال بکار..منوی صفحه مدیریت..آیکن بارگذاری پاسخ استعلام اداره امور مالیاتی </li>

            </ul>
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelTaxLetter" HeaderText="استعلام ها"
                runat="server" Visible="true" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tr>
                                <td valign="top" align="right" style="width: 30%">
                                    <asp:Label runat="server" Text="تصویر پاسخ استعلام اداره امور مالیاتی" ID="lblTaxOfficeLetter"></asp:Label>
                                </td>
                                <td valign="top" align="right" colspan="3" style="width: 70%">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl ID="flpTaxOfficeLetter" runat="server" ClientInstanceName="flpTaxOfficeLetter"
                                                        UploadWhenFileChoosed="true" OnFileUploadComplete="flpAttach_FileUploadComplete">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndTaxOfficeLetter.SetVisible(true);
  	HiddenFieldAccConfirm.Set('TaxOfficeLetter',1);
	lblValidationTaxOfficeLetter.SetVisible(false);
	imgTaxOfficeLetter.SetVisible(true);
	imgTaxOfficeLetter.SetImageUrl('../../Image/DocMeFile/TaxOffice/'+e.callbackData);
	}
	else{
    
  	HiddenFieldAccConfirm.Set('TaxOfficeLetter',0);
	imgEndTaxOfficeLetter.SetVisible(false);
	
	imgTaxOfficeLetter.SetVisible(false);
	imgTaxOfficeLetter.SetImageUrl('');
	}
}" />
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel ID="lblValidationTaxOfficeLetter" runat="server" ClientInstanceName="lblValidationTaxOfficeLetter"
                                                        ClientVisible="False" ForeColor="Red" Text="تصویر پاسخ استعلام اداره امور مالیاتی بارگذاری نمایید">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage ID="imgEndTaxOfficeLetter" runat="server" ClientInstanceName="imgEndTaxOfficeLetter"
                                                        ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgTaxOfficeLetter" ClientInstanceName="imgTaxOfficeLetter"
                                        Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                        <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                        </EmptyImage>
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
                <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelPeriodJoosh" HeaderText="گواهینامه دوره جوش"
                runat="server" Visible="true" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tr>
                                <td valign="top" align="right" style="width: 30%">
                                    <asp:Label runat="server" Text="تصویر گواهینامه دوره جوش" ID="lblPeriodImage"></asp:Label>
                                </td>
                                <td valign="top" align="right" colspan="3" style="width: 70%">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl ID="flplblPeriodImage" runat="server" ClientInstanceName="flplblPeriodImage"
                                                        UploadWhenFileChoosed="true" OnFileUploadComplete="flpAttach_FileUploadComplete">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndPeriodImage.SetVisible(true);
  	HiddenFieldAccConfirm.Set('PeriodImage',1);
	lblValidationPeriodImage.SetVisible(false);
	imgPeriodImage.SetVisible(true);
	imgPeriodImage.SetImageUrl('../../Image/DocMeFile/JooshPeriod/'+e.callbackData);
	}
	else{
    
  	HiddenFieldAccConfirm.Set('PeriodImage',0);
	imgEndPeriodImage.SetVisible(false);
	
	
	imgPeriodImage.SetImageUrl('../../Images/noimage.gif');
	}
}" />
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel ID="lblValidationPeriodImage" runat="server" ClientInstanceName="lblValidationPeriodImage"
                                                        ClientVisible="False" ForeColor="Red" Text="تصویر گواهینامه دوره جوش را بارگذاری نمایید">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage ID="imgEndPeriodImage" runat="server" ClientInstanceName="imgEndPeriodImage"
                                                        ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgPeriodImage" ClientInstanceName="imgPeriodImage"
                                        Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                        <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                        </EmptyImage>
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="CustomASPxRoundPanel1" HeaderText="تصاویر پروانه اشتغال بکار"
                runat="server" Visible="true" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tr>
                                <td valign="top" align="right" style="width: 30%">
                                    <asp:Label runat="server" Text="تصویر روی پروانه قبلی" ID="Label1"></asp:Label>
                                </td>
                                <td valign="top" align="right" colspan="3" style="width: 70%">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl ID="flpFrontDoc" runat="server" ClientInstanceName="flpFrontDoc"
                                                        UploadWhenFileChoosed="true" OnFileUploadComplete="flpAttach_FileUploadComplete">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
                                                                           
	imgEndFrontDoc.SetVisible(true);
  	HiddenFieldAccConfirm.Set('FrontDoc',1);
	lblValidationFrontDoc.SetVisible(false);
	imgFrontDoc.SetVisible(true);
                                                                            
	imgFrontDoc.SetImageUrl('../../Image/DocMeFile/OldDoc/'+e.callbackData);
	}
	else{
    
  	HiddenFieldAccConfirm.Set('FrontDoc',0);
	imgEndFrontDoc.SetVisible(false);
	
	imgFrontDoc.SetVisible(false);
	imgFrontDoc.SetImageUrl('');
	}
}" />
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel ID="lblValidationFrontDoc" runat="server" ClientInstanceName="lblValidationFrontDoc"
                                                        ClientVisible="False" ForeColor="Red" Text="تصویر روی آخرین پروانه اشتغال بکار را بارگذاری نمایید ">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage ID="imgEndFrontDoc" runat="server" ClientInstanceName="imgEndFrontDoc"
                                                        ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgFrontDoc" ClientInstanceName="imgFrontDoc"
                                        Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                        <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                        </EmptyImage>
                                    </dxe:ASPxImage>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right" style="width: 30%">
                                    <asp:Label runat="server" Text="تصویر پشت پروانه قبلی" ID="Label2"></asp:Label>
                                </td>
                                <td valign="top" align="right" colspan="3" style="width: 70%">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl ID="flpBackDoc" runat="server" ClientInstanceName="flpBackDoc"
                                                        UploadWhenFileChoosed="true" OnFileUploadComplete="flpAttach_FileUploadComplete">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndBackDoc.SetVisible(true);
  	HiddenFieldAccConfirm.Set('backDoc',1);
	lblValidationBackDoc.SetVisible(false);
	imgBackDoc.SetVisible(true);
	imgBackDoc.SetImageUrl('../../Image/DocMeFile/OldDoc/'+e.callbackData);
	}
	else{
    
  	HiddenFieldAccConfirm.Set('backDoc',0);
	imgEndBackDoc.SetVisible(false);
	
	imgBackDoc.SetVisible(false);
	imgBackDoc.SetImageUrl('');
	}
}" />
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel ID="lblValidationBackDoc" runat="server" ClientInstanceName="lblValidationBackDoc"
                                                        ClientVisible="False" ForeColor="Red" Text="تصویر پشت آخرین پروانه اشتغال بکار را بارگذاری نمایید ">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage ID="imgEndBackDoc" runat="server" ClientInstanceName="imgEndBackDoc"
                                                        ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgBackDoc" ClientInstanceName="imgBackDoc"
                                        Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                        <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                        </EmptyImage>
                                    </dxe:ASPxImage>
                                </td>
                            </tr>

                            <tr>
                                <td valign="top" align="right">
                                    <dxe:ASPxLabel runat="server" Text="تصویر HSE*" ID="lblHSE" ClientInstanceName="lblHSE" ClientVisible="false"></dxe:ASPxLabel>
                                </td>
                                <td valign="top" align="right">
                                    <dxp:ASPxPanel runat="server" ID="PanelflpHSE" ClientInstanceName="PanelflpHSE" ClientVisible="false">
                                        <PanelCollection>
                                            <dxp:PanelContent>

                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxUploadControl ID="flpHSE" runat="server" ClientInstanceName="flpHSE"
                                                                    UploadWhenFileChoosed="true" InputType="Files" OnFileUploadComplete="flpAttach_FileUploadComplete">
                                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadHSEImgURL.SetVisible(true);
  	HiddenFieldAccConfirm.Set('HSEImg',1);
	lblImageHSEWarning.SetVisible(false);
	HeyperLinkHSEImg.SetVisible(true);
	HeyperLinkHSEImg.SetNavigateUrl('../../Image/DocMeFile/HSE/'+e.callbackData);
	}
	else{
	imgEndUploadHSEImgURL.SetVisible(false);
	lblImageHSEWarning.SetVisible(true);
	HeyperLinkHSEImg.SetVisible(false);
	HeyperLinkHSEImg.SetNavigateUrl('');    
  	HiddenFieldAccConfirm.Set('HSEImg',0);
	}
}" />
                                                                </TSPControls:CustomAspxUploadControl>
                                                                <dxe:ASPxLabel ID="lblImageHSEWarning" runat="server" ClientInstanceName="lblImageHSEWarning"
                                                                    ClientVisible="False" ForeColor="Red" Text="تصویر گواهینامه آموزشی راانتخاب نمایید">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxImage ID="imgEndUploadHSEImgURL" runat="server" ClientInstanceName="imgEndUploadHSEImgURL"
                                                                    ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویرانتخاب شد">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <dxe:ASPxHyperLink ID="HeyperLinkHSEImg" runat="server" ClientInstanceName="HeyperLinkHSEImg"
                                                    Target="_blank" Text="تصویر HSE">
                                                </dxe:ASPxHyperLink>
                                            </dxp:PanelContent>

                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <div class="Item-center">
                <TSPControls:CustomAspxButton CssClass="ButtonMenue" ID="btnPre" OnClick="btnPre_Click" runat="server" Text="بازگشت"
                    ToolTip="بازگشت" EnableViewState="False" EnableTheming="False" UseSubmitBehavior="False"
                    CausesValidation="False">
                </TSPControls:CustomAspxButton>
                <TSPControls:CustomAspxButton CssClass="ButtonMenue" ID="btnNext" OnClick="btnNext_Click" runat="server" Text="تایید و ادامه" CausesValidation="false"
                    UseSubmitBehavior="False" EnableTheming="False" EnableViewState="False" ToolTip="تایید و ادامه">
                </TSPControls:CustomAspxButton>
            </div>
            <dx:ASPxHiddenField ID="HiddenFieldAccConfirm" runat="server" ClientInstanceName="HiddenFieldAccConfirm">
            </dx:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


