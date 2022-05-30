<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="TestSession.aspx.cs" Inherits="Employee_Amoozesh_TestSession"
    Title="صورت جلسه آزمون" %>

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
    <script language="javascript">

        function SetControlValuesAb() {
            gridAbsent.GetRowValues(gridAbsent.GetFocusedRowIndex(), 'MeId;FirstName;LastName;MeType;IsMember', SetValueAb);
        }
        function SetValueAb(values) {
            txtAbID.SetText(values[0]);
            AbFirstName.SetText(values[1]);
            AbLastName.SetText(values[2]);
            AbTypeName.SetText(values[3]);
            txtAbType.SetText(values[4]);

        }

        function SetControlValuesMo() {
            gridMo.GetRowValues(gridMo.GetFocusedRowIndex(), 'MeId;FirstName;LastName;MeType;IsMember', SetValueMo);
        }
        function SetValueMo(values) {
            txtMoID.SetText(values[0]);
            MoFirstName.SetText(values[1]);
            MoLastName.SetText(values[2]);
            MoTypeName.SetText(values[3]);
            txtMoType.SetText(values[4]);

        }
    </script>
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                width="100%">
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                align="right" cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                                ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                OnClick="btnSave_Click" ValidationGroup="TestSession">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/save.png">
                                                                </Image>
                                                                <ClientSideEvents Click="function(s, e) {
 if (ASPxClientEdit.ValidateGroup('TestSession') == false)
        return;
	  if(Grdv_Moragheb.GetVisibleRowsOnPage()==0)
    {
        alert('مراقبین جلسه را مشخص نمایید');
         e.processOnServer=false;
          return;
    } 
//  if(Grdv_Absent.GetVisibleRowsOnPage()!=txtAbCount.GetText())
 //   {
 //       alert(' تمامی غائبین جلسه مشخص نشده است ');
  //       e.processOnServer=false;
  //        return;
  //  }
//else
	//// e.processOnServer=true;
}" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="btnBack_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/Back.png">
                                                                </Image>
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
                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelPage" HeaderText="مشاهده" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <fieldset runat="server" id="FieldSetBaseInfo">
                                <legend class="fieldset-legend" dir="rtl"><b>مشخصات آزمون</b>
                                </legend>

                                <table dir="rtl" id="Table1" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="عنوان دوره" Width="100%" ID="ASPxLabel17">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right" colspan="3">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtPPTitle"  Width="100%"
                                                    ReadOnly="True" >
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
                                            <td style="vertical-align: top" align="right" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ آزمون" ID="ASPxLabel18">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right" width="30%">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtTestDate" Style="direction: ltr" 
                                                    Width="100%" ReadOnly="True" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top" align="right" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="ساعت شروع آزمون" Width="100%" ID="ASPxLabel19">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right" width="30%">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtTestHour"  Width="100%"
                                                    ReadOnly="True" >
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
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="محل برگزاری آزمون" Width="120px" ID="ASPxLabel26">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="40px" ID="txtTestPlace"  Width="100%"
                                                    ReadOnly="True" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText="محل برگزاری را وارد نمایید"></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>


                            <fieldset runat="server" id="FieldSet1">
                                <legend class="fieldset-legend" dir="rtl"><b>صورت جلسه</b>
                                </legend>
                                <table dir="rtl" id="Table2" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top" align="right" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="ساعت شروع *" Width="100%" ID="ASPxLabel1">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right" width="30%">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtSeStartTime"  MaxLength="5"
                                                    Width="100%" >
                                                    <ValidationSettings Display="Dynamic" ValidationGroup="TestSession" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="ساعت شروع را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top" align="right" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="ساعت پایان *" Width="100%" ID="ASPxLabel4">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right" width="30%">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtSeEndTime"  MaxLength="5"
                                                    Width="100%" >
                                                    <ValidationSettings Display="Dynamic" ValidationGroup="TestSession" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="ساعت پایان را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تعداد شرکت کنندگان *" Width="100%" ID="ASPxLabel2">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtParCount"  Width="100%"
                                                    >
                                                    <ValidationSettings Display="Dynamic" ValidationGroup="TestSession" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="تعداد را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تعداد حاضرین *" Width="100%" ID="ASPxLabel5">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtAttCount"  Width="100%"
                                                    >
                                                    <ValidationSettings Display="Dynamic" ValidationGroup="TestSession" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="تعداد را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تعداد غائبین *" Width="100%" ID="ASPxLabel3">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtAbCount"  Width="100%"
                                                    ClientInstanceName="txtAbCount" >
                                                    <ValidationSettings Display="Dynamic" ValidationGroup="TestSession" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="تعداد را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top" align="right"></td>
                                            <td style="vertical-align: top" align="right"></td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="شماره صورت جلسه" Width="100%" ID="ASPxLabel6">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtSessionNo"  Width="100%"
                                                    >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ صورت جلسه *" Width="100%" ID="ASPxLabel7">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="220px" ShowPickerOnTop="True"
                                                    ValidationGroup="TestSession" ID="txtSessionDate" PickerDirection="ToRight" RightToLeft="False"
                                                    IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                <asp:RequiredFieldValidator runat="server" ErrorMessage="تاریخ را وارد نمایید" ControlToValidate="txtSessionDate"
                                                    ID="RequiredFieldValidator2"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تصویر صورت جلسه" ID="lblFileNew">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right" colspan="3">
                                                <table dir="rtl">
                                                    <tbody>
                                                        <tr>
                                                            <td style="vertical-align: top; text-align: right">
                                                                <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                    ID="flp" InputType="Files" ClientInstanceName="flpc" OnFileUploadComplete="flp_FileUploadComplete">
                                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
                                                             
	imgEndUploadImg.SetVisible(true);
                                                            
	
	HpLinkFile.SetVisible(true);
                                                            
		HpLinkFile.SetNavigateUrl('../../Image/Employee/Amoozesh/TestSession/'+e.callbackData);
                                                              
       // HiddenFieldInfo.Set('MeImgUpload','~/Image/Members/Person/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImg.SetVisible(false);
	//lblPersonalImagevalidation.SetVisible(true);
	HpLinkFile.SetVisible(false);
	HpLinkFile.SetNavigateUrl('');
	}
}"></ClientSideEvents>
                                                                </TSPControls:CustomAspxUploadControl>

                                                            </td>
                                                            <td style="vertical-align: top; text-align: right">
                                                                <dxe:ASPxImage runat="server" ID="imgEndUploadImg" ToolTip="تصویر انتخاب شد" ClientVisible="False"
                                                                    ImageUrl="~/Images/icons/button_ok.png" ClientInstanceName="imgEndUploadImg">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" align="right" colspan="4">
                                                <dxe:ASPxHyperLink runat="server" Text="تصویر صورت جلسه" ID="HpLinkFile" ClientInstanceName="HpLinkFile" Target="_blank"
                                                    ClientVisible="False">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel8">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top" align="right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="40px" ID="txtSeDesc"  Width="100%"
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
                            </fieldset>

                            <fieldset runat="server" id="FieldSet2">
                                <legend class="fieldset-legend" dir="rtl"><b>* مراقبین</b>
                                </legend>
                                <table runat="server" id="tblMor" dir="rtl" width="100%">
                                    <tr id="Tr1" runat="server">
                                        <td id="TD1" runat="server" style="vertical-align: top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="سمت مراقب" Width="100%" ID="ASPxLabel9" __designer:wfdid="w15">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td id="TD2" runat="server" style="vertical-align: top" dir="ltr" align="right" width="30%">
                                            <TSPControls:CustomAspxComboBox runat="server"  Width="100%" ID="ComboType"
                                                 EnableClientSideAPI="True" ValueType="System.String"
                                                SelectedIndex="1" ClientInstanceName="cmbType" 
                                                __designer:wfdid="w16">
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(cmbType.GetValue() == 1)
     {
		lblMe.SetVisible(true);
		lblEmp.SetVisible(false);

		
	 }
	else if(cmbType.GetValue() == 0)
	{
		lblMe.SetVisible(false);
		lblEmp.SetVisible(true);

	}
}"></ClientSideEvents>
                                                <ValidationSettings Display="Dynamic" ValidationGroup="Mor" ErrorTextPosition="Bottom">

                                                    <RequiredField IsRequired="True" ErrorText="مراقب را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <Items>
                                                    <dxe:ListEditItem Value="0" Text="کارمند"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Value="1" Text="عضو حقیقی" Selected="True"></dxe:ListEditItem>
                                                </Items>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td id="TD3" runat="server" style="vertical-align: top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="کد عضویت *" Width="100%" ID="lblMeId" ClientInstanceName="lblMe">
                                            </dxe:ASPxLabel>
                                            <dxe:ASPxLabel runat="server" Text="کد کارمند" ClientVisible="False" ID="lblEmpId"
                                                ClientInstanceName="lblEmp">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td id="TD4" runat="server" style="vertical-align: top" align="right" width="30%">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMeNo"  AutoPostBack="True"
                                                Width="100%" ClientInstanceName="ID" 
                                                OnTextChanged="txtMeNo_TextChanged">
                                                <ValidationSettings Display="Dynamic" ValidationGroup="Mor" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="کد را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtEmpId"  Visible="False"
                                                Width="100%"  __designer:wfdid="w20">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=""></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr id="Tr2" runat="server">
                                        <td id="TD5" runat="server" style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام" ID="lblMeFirstName" ClientInstanceName="lblMname">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td id="TD6" runat="server" style="vertical-align: top" dir="ltr" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMeFirstName"  ReadOnly="True"
                                                Width="100%" ClientInstanceName="mFirstName" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=""></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td id="TD7" runat="server" style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام خانوادگی" Width="100%" ID="lblMeLastName"
                                                ClientInstanceName="lblMfamily">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td id="TD8" runat="server" style="vertical-align: top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMeLastName"  ReadOnly="True"
                                                Width="100%" ClientInstanceName="mLastName" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=""></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr id="Tr3" runat="server">
                                        <td id="TD9" runat="server" style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel10">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td id="TD10" runat="server" style="vertical-align: top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="40px" ID="txtMorDesc"  Width="100%"
                                                 __designer:wfdid="w26">
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
                                    <tr id="Tr4" runat="server">
                                        <td id="TD11" runat="server" style="vertical-align: top" align="center" colspan="4">
                                            <br />
                                            <TSPControls:CustomAspxButton  runat="server" Text="اضافه به لیست"  ID="btnAddMoragheb"
                                                ValidationGroup="Mor" 
                                                OnClick="btnAddMoragheb_Click" Width="120px">
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </table>
                                <TSPControls:CustomAspxDevGridView runat="server"  Width="100%"
                                    RightToLeft="True" ID="Grdv_Moragheb" KeyFieldName="Id" AutoGenerateColumns="False"
                                    ClientInstanceName="Grdv_Moragheb"  OnRowDeleting="Grdv_Moragheb_RowDeleting">
                                    <ClientSideEvents
                                        EndCallback="function(s, e) { 
    if(s.cpError==1)
     {
       ShowMessage(s.cpMsg);
       s.cpError=0;
       s.cpMsg = '';
     }
}"></ClientSideEvents>
                                    <Settings ShowGroupPanel="True" ShowFilterRowMenu="True"></Settings>
                                    <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>

                                    <Columns>

                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="Id">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="0" ShowDeleteButton="true">
                                         
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Code" Caption="کد">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FirstName" Caption="نام">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="LastName" Caption="نام خانوادگی">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="TypeName" Caption="سمت">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Description" Caption="توضیحات">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>

                                </TSPControls:CustomAspxDevGridView>
                            </fieldset>

                            <fieldset runat="server" id="FieldSet3">
                                <legend class="fieldset-legend" dir="rtl"><b>غائبین</b>
                                </legend>
                                <table runat="server" id="tblAbsents" dir="rtl" width="100%">
                                    <tr id="Tr6" runat="server">
                                        <td id="TD13" runat="server" style="vertical-align: top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="جستجو" ID="ASPxLabel15">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td id="TD14" runat="server" style="vertical-align: top" align="right" width="30%">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server"  ToolTip="جستجو" CausesValidation="False"
                                                EnableClientSideAPI="True" ID="ASPxButton4" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
	popAbsent.Show();
}"></ClientSideEvents>
                                                <Image Width="25px" Height="25px" Url="~/Images/icons/Search.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="TextAbId" ClientVisible="False" Width="15%"
                                                ClientInstanceName="txtAbID">
                                            </TSPControls:CustomTextBox>
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="TextAbType" ClientVisible="False" Width="30%"
                                                ClientInstanceName="txtAbType">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td id="TD15" runat="server" style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="وضعیت عضویت" ID="ASPxLabel23">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td id="TD16" runat="server" style="vertical-align: top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtAbTypeName"  ReadOnly="True"
                                                Width="100%" ClientInstanceName="AbTypeName" >
                                                <ValidationSettings Display="Dynamic" ValidationGroup="Mo" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=""></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr id="Tr7" runat="server">
                                        <td id="TD17" runat="server" style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel16">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td id="TD18" runat="server" style="vertical-align: top" dir="ltr" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtAbFirstName"  ReadOnly="True"
                                                Width="100%" ClientInstanceName="AbFirstName" >
                                                <ValidationSettings Display="Dynamic" ValidationGroup="Ab" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="شخص غائب را انتخاب نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td id="TD19" runat="server" style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام خانوادگی" Width="100%" ID="ASPxLabel11">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td id="TD20" runat="server" style="vertical-align: top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtAbLastName"  ReadOnly="True"
                                                Width="100%" ClientInstanceName="AbLastName" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=""></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr id="Tr8" runat="server">
                                        <td id="TD21" runat="server" style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" Width="100%" ID="ASPxLabel21">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td id="TD22" runat="server" style="vertical-align: top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="40px" ID="txtAbDesc"  Width="100%"
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
                                    <tr id="Tr9" runat="server">
                                        <td id="TD23" runat="server" style="vertical-align: top" align="center" colspan="4">
                                            <br />
                                            <TSPControls:CustomAspxButton  runat="server" Text="اضافه به لیست"  ID="btnAddAbsent"
                                                ValidationGroup="Ab" Width="120px"  OnClick="btnAddAbsent_Click">
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>

                                </table>
                                <TSPControls:CustomAspxDevGridView runat="server"  Width="100%"
                                    RightToLeft="True" ID="Grdv_Absent" KeyFieldName="Id" AutoGenerateColumns="False"
                                    ClientInstanceName="Grdv_Absent" OnPageIndexChanged="OnPageIndexChanged_Grdv_Absent"  OnRowDeleting="Grdv_Absent_RowDeleting">
                                    <ClientSideEvents
                                        EndCallback="function(s, e) { 
    if(s.cpError==1)
     {
       ShowMessage(s.cpMsg);
       s.cpError=0;
       s.cpMsg = '';
     }
}"></ClientSideEvents>
                                    <Settings ShowGroupPanel="True" ShowFilterRowMenu="True"></Settings>
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="Id">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="0" ShowDeleteButton="true">
                                       
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Code" Caption="کد">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FirstName" Caption="نام">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="LastName" Caption="نام خانوادگی">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="TypeName" Caption="وضعیت عضویت">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Description" Caption="توضیحات">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                </TSPControls:CustomAspxDevGridView>
                            </fieldset>


                            <fieldset runat="server" id="FieldSet4">
                                <legend class="fieldset-legend" dir="rtl"><b>متخلفین</b>
                                </legend>
                                <table runat="server" id="tblMot" dir="rtl" width="100%">
                                    <tr id="Tr11" runat="server">
                                        <td id="TD25" runat="server" style="vertical-align: top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="جستجو" ID="ASPxLabel12">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td id="TD26" runat="server" style="vertical-align: top" align="right" widht="30%">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server"  ToolTip="جستجو" CausesValidation="False"
                                                EnableClientSideAPI="True" ID="ASPxButton3" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" __designer:wfdid="w66">
                                                <ClientSideEvents Click="function(s, e) {
	popMo.Show();
}"></ClientSideEvents>
                                                <Image Width="25px" Height="25px" Url="~/Images/icons/Search.png">
                                                </Image>
                                           </TSPControls:CustomAspxButton>
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="TextMoId" ClientVisible="False" Width="100%"
                                                ClientInstanceName="txtMoID">
                                            </TSPControls:CustomTextBox>
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="TextMoType" ClientVisible="False" Width="100%"
                                                ClientInstanceName="txtMoType">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td id="TD27" runat="server" style="vertical-align: top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="وضعیت عضویت" ID="ASPxLabel22">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td id="TD28" runat="server" style="vertical-align: top" align="right" width="30%">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMoTypeName"  ReadOnly="True"
                                                Width="100%" ClientInstanceName="MoTypeName" 
                                                __designer:wfdid="w70">
                                                <ValidationSettings Display="Dynamic" ValidationGroup="Mo" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=""></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr id="Tr12" runat="server">
                                        <td id="TD29" runat="server" style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel13">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td id="TD30" runat="server" style="vertical-align: top" dir="ltr" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMoFirstName"  ReadOnly="True"
                                                Width="100%" ClientInstanceName="MoFirstName" >
                                                <ValidationSettings Display="Dynamic" ValidationGroup="Mo" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="شخص متخلف را انتخاب نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td id="TD31" runat="server" style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام خانوادگی" Width="100%" ID="ASPxLabel14">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td id="TD32" runat="server" style="vertical-align: top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMoLastName"  ReadOnly="True"
                                                Width="100%" ClientInstanceName="MoLastName" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=""></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr id="Tr13" runat="server">
                                        <td id="TD33" runat="server" style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" Width="100%" ID="ASPxLabel20">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td id="TD34" runat="server" style="vertical-align: top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="40px" ID="txtMoDesc"  Width="100%"
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
                                    <tr id="Tr14" runat="server">
                                        <td id="TD35" runat="server" style="vertical-align: top" align="center" colspan="4">
                                            <br />
                                            <TSPControls:CustomAspxButton runat="server" Text="اضافه به لیست"  ID="btnAddMo" ValidationGroup="Mo"
                                                 Width="120px" OnClick="btnAddMo_Click">
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>

                                </table>
                                <TSPControls:CustomAspxDevGridView runat="server"  Width="100%"
                                    RightToLeft="True" ID="Grdv_Mo" KeyFieldName="Id" AutoGenerateColumns="False"
                                     OnRowDeleting="Grdv_Mo_RowDeleting">
                                    <ClientSideEvents
                                        EndCallback="function(s, e) { 
    if(s.cpError==1)
     {
       ShowMessage(s.cpMsg);
       s.cpError=0;
       s.cpMsg = '';
     }
}"></ClientSideEvents>
                                    <Images >
                                    </Images>

                                    <Settings ShowGroupPanel="True" ShowFilterRowMenu="True"></Settings>
                                    <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>

                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="Id">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="0" ShowDeleteButton="true">
                                       
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Code" Caption="کد">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FirstName" Caption="نام">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="LastName" Caption="نام خانوادگی">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="TypeName" Caption="وضعیت عضویت">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Description" Caption="توضیحات">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>

                                </TSPControls:CustomAspxDevGridView>
                            </fieldset>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                width="100%">
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                                ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                OnClick="btnSave_Click" ValidationGroup="TestSession">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/save.png">
                                                                </Image>
                                                                <ClientSideEvents Click="function(s, e) {
 if (ASPxClientEdit.ValidateGroup('TestSession') == false)
        return;
	  if(Grdv_Moragheb.GetVisibleRowsOnPage()==0)
    {
        alert('مراقبین جلسه را مشخص نمایید');
         e.processOnServer=false;
          return;
    } 
//  if(Grdv_Absent.GetVisibleRowsOnPage()!=txtAbCount.GetText())
//    {
 //       alert(' تمامی غائبین جلسه مشخص نشده است ');
  //       e.processOnServer=false;
 //         return;
  //  }

else
	 e.processOnServer=true;
}" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                CausesValidation="False" ID="ASPxButton2" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="btnBack_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/Back.png">
                                                                </Image>
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
                <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl3" runat="server"  
                    HeaderText="جستجو" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter"
                    PopupElementID="ASPxButton2"  Height="269px"
                    CloseAction="CloseButton" ClientInstanceName="popAbsent">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <TSPControls:CustomAspxDevGridView runat="server"  EnableViewState="False"
                                Width="544px" ID="CustomAspxDevGridView1" DataSourceID="OdbPeriodRegister" KeyFieldName="PRId"
                                AutoGenerateColumns="False" ClientInstanceName="gridAbsent" >
                                <ClientSideEvents RowDblClick="function(s, e) {
	popAbsent.Hide();

	SetControlValuesAb();
	
}"></ClientSideEvents>
                                <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MeId" Caption="کد عضویت">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FirstName" Caption="نام"
                                        Name="FirstName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="LastName" Caption="نام خانوادگی"
                                        Name="LastName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="MeType" Caption="سمت">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="IsMember" Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </TSPControls:CustomASPxPopupControl>
                <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl4" runat="server"  
                    HeaderText="جستجو" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter"
                    PopupElementID="ASPxButton2"  Height="269px"
                    CloseAction="CloseButton" ClientInstanceName="popMo">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <TSPControls:CustomAspxDevGridView runat="server"  EnableViewState="False"
                                Width="544px" ID="CustomAspxDevGridView3" DataSourceID="OdbPeriodRegister" KeyFieldName="PRId"
                                AutoGenerateColumns="False" ClientInstanceName="gridMo" >
                                <ClientSideEvents RowDblClick="function(s, e) {
	popMo.Hide();

	SetControlValuesMo();
	
}"></ClientSideEvents>
                                <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MeId" Caption="کد عضویت">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FirstName" Caption="نام"
                                        Name="FirstName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="LastName" Caption="نام خانوادگی"
                                        Name="LastName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="MeType" Caption="وضعیت عضویت">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="IsMember" Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                    <HeaderStyle>
                        <Paddings PaddingTop="1px" PaddingRight="6px" PaddingLeft="10px"></Paddings>
                    </HeaderStyle>
                    <SizeGripImage Height="12px" Width="12px">
                    </SizeGripImage>
                    <CloseButtonImage Height="17px" Width="17px">
                    </CloseButtonImage>
                </TSPControls:CustomASPxPopupControl>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
            DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img align="middle" src="../../Image/indicator.gif" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
        <asp:HiddenField ID="PeriodId" runat="server" Visible="False" />
        <asp:HiddenField ID="SessionId" runat="server" Visible="False" />
        <asp:HiddenField ID="PageMode" runat="server" Visible="False" />


        <asp:ObjectDataSource ID="OdbPeriodRegister" runat="server" TypeName="TSP.DataManager.PeriodRegisterManager"
            SelectMethod="SelectPeriodRegister" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter Type="Int32" Name="PRId" DefaultValue="-1"></asp:Parameter>
                <asp:Parameter Type="Int32" Name="MeId" DefaultValue="-1"></asp:Parameter>
                <asp:Parameter Type="Int32" Name="PPId" DefaultValue="-1"></asp:Parameter>
                <asp:Parameter Type="Int32" Name="InsId" DefaultValue="-1"></asp:Parameter>
                <asp:Parameter DefaultValue="0" Name="IsSeminar" Type="Int32" />

                <asp:Parameter Type="Int32" Name="InActive" DefaultValue="0"></asp:Parameter>
            </SelectParameters>
        </asp:ObjectDataSource>

</asp:Content>
