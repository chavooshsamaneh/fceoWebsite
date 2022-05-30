<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ExpertFileInsert.aspx.cs" Inherits="Employee_27_ExpertFileInsert" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>


<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script  type="text/javascript" >
        function CheckDate() {
            var StartDate = document.getElementById('<%=txtexpIssueDate.ClientID%>').value;
            var EndDate = document.getElementById('<%=txtexpExpireDate.ClientID%>').value;
            if (EndDate < StartDate && EndDate != "")
                return -1;
            else
                return 1;
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
                CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
                FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
            </pdc:PersianDateScriptManager>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                        CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                        EnableTheming="False" OnClick="BtnNew_Click">
                                        <Image  Url="~/Images/icons/new.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                        CausesValidation="False"  ID="btnEdit" UseSubmitBehavior="False"
                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                        <Image  Url="~/Images/icons/edit.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>

                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server"  EnableTheming="False"
                                        EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                        <Image  Url="~/Images/icons/save.png"  />
                                         <ClientSideEvents Click="function(s, e) {
   
   if(  HiddenFieldPage.Get('ImgBackOld')==0)
   {
    lblValidationBImgOldDoc.SetVisible(true);
	e.processOnServer=false;
    return;
   }
  if(  HiddenFieldPage.Get('ImgFrontOld')==0)
   {
    lblValidationFImgOldDoc.SetVisible(true);
	e.processOnServer=false;
    return;
   }              
	if(CheckDate()==-1)
	{
		e.processOnServer=false;
		alert('بازه زمانی اعتبار پروانه کارشناس ماده 27 اشتباه می باشد' );
	}
	else
		e.processOnServer=true;
}" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False" 
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
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelPage" ClientInstanceName="RoundPanelPage"
                HeaderText="ویرایش" runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent>
                        <div align="center">
                        <dxe:ASPxLabel runat="server" Text="وضعیت درخواست:" Font-Bold="False" ForeColor="Red"
                            ID="lblWorkFlowState">
                        </dxe:ASPxLabel></div>
                        <fieldset runat="server" id="RoundPanelMajors">
                            <legend class="fieldset-legend" dir="rtl"><b>مشخصات عضو</b>
                            </legend>
                            <table width="100%" cellpadding="5">
                                <tr>
                                    <td align="right" valign="top" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="ASPxLabel2">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" width="35%">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Font-Bold="true" ID="lblMeId" AutoPostBack="true" OnTextChanged="lblMeId_TextChanged"  Width="100%">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                        <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                        <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت را صحیح وارد نمایید" />
                                                    </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td colspan="2" align="right">
                                        <dxe:ASPxImage ID="imgMember" Height="75px" Width="75px" runat="server">
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel4">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" width="35%">
                                        <dxe:ASPxLabel runat="server" Font-Bold="true" ID="lblFirstName"  Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="ASPxLabel1">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" width="35%">
                                        <dxe:ASPxLabel runat="server" Font-Bold="true" ID="lblLastName" Text="---"   Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <dxe:ASPxLabel runat="server" Text="کد ملی" ID="ASPxLabel3">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right">
                                        <dxe:ASPxLabel runat="server" Font-Bold="true" ID="lblSSN" Text="---"   Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام پدر" ID="ASPxLabel6">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right">
                                        <dxe:ASPxLabel runat="server" Font-Bold="true" ID="lblFatherName" Text="---"  Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <dxe:ASPxLabel runat="server" Text="شماره پروانه" ID="ASPxLabel7">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right">
                                        <dxe:ASPxLabel runat="server" Font-Bold="true" ID="lblFileNo" Text="---"  Width="100%">

                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ اعتبار پروانه" ID="ASPxLabel9">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right">
                                        <dxe:ASPxLabel runat="server" Font-Bold="true" ID="lblFileDate" Text="---"  Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset runat="server" id="Fieldset1">
                            <legend class="fieldset-legend" dir="rtl"><b>مشخصات پروانه کارشناسی ماده 27</b>
                            </legend>
                            <table width="100%">
                                <tr>
                                    <td align="right" valign="top" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="شماره پروانه" ID="ASPxLabel5">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" width="35%">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Font-Bold="true" ID="txtExpFileNo" Width="100%" >
                                             <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                        <RequiredField IsRequired="True" ErrorText="شماره پروانه را وارد نمایید"></RequiredField>
                                                    </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td>
                                      
                                    </td>
                                    <td>
                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td  align="right" valign="top" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ صدور پروانه" ID="ASPxLabel12" >
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td  align="right" valign="top" width="35%">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="240px" ShowPickerOnTop="True"
                                            ID="txtexpIssueDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                            ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                            ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtexpIssueDate" ID="PersianDateValidator1">تاریخ نامعتبر</pdc:PersianDateValidator>
                                    </td>
                                    <td  align="right" valign="top" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ اعتبار پروانه" ID="ASPxLabel34">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td  align="right" valign="top" width="35%">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="240px" ShowPickerOnTop="True"
                                            ID="txtexpExpireDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                            ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                            ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtexpExpireDate" ID="PersianDateValidator2">تاریخ نامعتبر</pdc:PersianDateValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="تصویر روی پروانه  ماده 27" ID="lblImgFrontOldDoc" ClientVisible="true">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <table dir="rtl">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" ID="flpFrontOldDoc" InputType="Images"
                                                            UploadWhenFileChoosed="true" ClientInstanceName="flpFrontOldDoc" OnFileUploadComplete="flpAttach_FileUploadComplete"
                                                            ClientVisible="false">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                        if(e.isValid){
	ImgEndFrontOldDoc.SetVisible(true);
	HiddenFieldPage.Set('ImgFrontOld',1);
	lblValidationFImgOldDoc.SetVisible(false);
    hpImgFrontOldDoc.SetVisible(true);
	hpImgFrontOldDoc.SetImageUrl('../../Image/27/FileImage/'+e.callbackData);
	}
	else{
    HiddenFieldPage.Set('ImgFrontOld',0);
	ImgEndFrontOldDoc.SetVisible(false);
	lblValidationFImgOldDoc.SetVisible(true);
	}
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                            ID="lblValidationFImgOldDoc" ForeColor="Red" ClientInstanceName="lblValidationFImgOldDoc">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="ImgEndFrontOldDoc" ClientInstanceName="ImgEndFrontOldDoc">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="hpImgFrontOldDoc" ClientInstanceName="hpImgFrontOldDoc"
                                            Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                            <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                            </EmptyImage>
                                        </dxe:ASPxImage>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="تصویر پشت پروانه ماده 27" ID="lblImgBackOldDoc" ClientVisible="true">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <table dir="rtl">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" ID="flpBackOldDoc" InputType="Images"
                                                            UploadWhenFileChoosed="true" ClientInstanceName="flpBackOldDoc" OnFileUploadComplete="flpAttach_FileUploadComplete"
                                                            ClientVisible="False">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
    if(e.isValid){
    HiddenFieldPage.Set('ImgBackOld',1);   
	ImgEndBackOldDoc.SetVisible(true);
	lblValidationBImgOldDoc.SetVisible(false);
    hpImgBackOldDoc.SetVisible(true);
    hpImgBackOldDoc.SetImageUrl('../../Image/27/FileImage/'+e.callbackData);
	}
	else{
    HiddenFieldPage.Set('ImgBackOld',0);
	ImgEndBackOldDoc.SetVisible(false);
	lblValidationBImgOldDoc.SetVisible(true);
	}
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                            ID="lblValidationBImgOldDoc" ForeColor="Red" ClientInstanceName="lblValidationBImgOldDoc">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="ImgEndBackOldDoc" ClientInstanceName="ImgEndBackOldDoc">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" Text="تصویر پشت پروانه  ماده 27"
                                            ID="hpImgBackOldDoc" ClientInstanceName="hpImgBackOldDoc" Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                            <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                            </EmptyImage>
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </dx:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />

            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldPage" ClientInstanceName="HiddenFieldPage">
            </dxhf:ASPxHiddenField>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader2" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tr>
                                <td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">
                                            <Image  Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                            CausesValidation="False"  ID="btnEdit2" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <Image  Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                            <Image  Url="~/Images/icons/save.png"  />
                                             <ClientSideEvents Click="function(s, e) {
   
   if(  HiddenFieldPage.Get('ImgBackOld')==0)
   {
    lblValidationBImgOldDoc.SetVisible(true);
	e.processOnServer=false;
    return;
   }
  if(  HiddenFieldPage.Get('ImgFrontOld')==0)
   {
    lblValidationFImgOldDoc.SetVisible(true);
	e.processOnServer=false;
    return;
   }              
	if(CheckDate()==-1)
	{
		e.processOnServer=false;
		alert('بازه زمانی اعتبار پروانه کارشناس ماده 27 اشتباه می باشد' );
	}
	else
		e.processOnServer=true;
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server" CausesValidation="False" 
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
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نماييد
                        <img alt="" src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


