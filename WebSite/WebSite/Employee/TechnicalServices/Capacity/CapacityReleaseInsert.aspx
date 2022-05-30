<%@ Page Title="بازگشت ظرفیت نظارت" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="CapacityReleaseInsert.aspx.cs" Inherits="Employee_TechnicalServices_Capacity_CapacityReleaseInsert" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#"><span style="color: #000000"></span>بستن</a>]
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
                                            ID="btnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnNew_Click">


                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" ID="btnEdit" OnClick="btnEdit_Click" UseSubmitBehavior="False"
                                            EnableViewState="true" EnableTheming="False">
                                            <Image Url="~/Images/icons/Edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                            <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                            <ClientSideEvents Click="function(s, e) {
                     if (ASPxClientEdit.ValidateGroup() == false ||  HiddenFieldPage.Get('Letter') == 0 || HiddenFieldPage.Get('Form') == 0)
                        {
                        e.processOnServer= false;   
                        alert('تکمیل موارد لازم و بارکذاری تصاویر اجباری است');
                        return;
                        }
	                    e.processOnServer= confirm('آیا از ذخیره بازگشت ظرفیت در این پروژه مطمئن می باشید؟');
                        }" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                            ToolTip="بازگشت" UseSubmitBehavior="False">
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
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelProjectSearch" HeaderText="جستجو پروژه"
                runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td width="15%">کد پروژه*
                                    </td>
                                    <td width="35%">
                                        <TSPControls:CustomTextBox runat="server" ID="txtProjectCode" ClientInstanceName="txtProjectCode" AutoPostBack="true" OnTextChanged="txtProjectCode_TextChanged"
                                            Width="100%">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="ValidAttach" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="ورود کد پروژه اجباری است"></RequiredField>
                                                <RegularExpression ErrorText="کد پروژه را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td width="15%">ناظر پروژه
                                    </td>
                                    <td width="35%">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            ID="comboProjectObserver" ValueType="System.String" SelectedIndex="0" DataSourceID="ObjectDataSourceObserver" ClientInstanceName="comboProjectObserver"
                                            RightToLeft="True" AutoPostBack="true" TextField="MemberFullName" ValueField="MeId">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {

}"></ClientSideEvents>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="ناظر را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                        <asp:ObjectDataSource ID="ObjectDataSourceObserver" runat="server" SelectMethod="SelectTSProjectObserversForCapacityRelease"
                                            TypeName="TSP.DataManager.TechnicalServices.Project_ObserversManager" OldValuesParameterFormatString="original_{0}">
                                            <SelectParameters>
                                                <asp:Parameter Type="Int32" DefaultValue="-2" Name="ProjectId"></asp:Parameter>
                                                <asp:Parameter Type="Int32" DefaultValue="-1" Name="MeOfficeOthPEngOId"></asp:Parameter>
                                                <asp:Parameter Type="Int32" DefaultValue="-1" Name="InActive"></asp:Parameter>
                                                
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSP:ProjectInfo ID="prjInfo" runat="server" />
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelPage" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td width="15%">دلیل بازگشت ظرفیت*</td>
                                    <td width="35%">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            ID="cmbReasonType" ClientInstanceName="cmbReasonType" ValueType="System.String" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){
                                            
                                               if (cmbReasonType.GetValue()== 0)//***----
                                                { 
                                               
                                                }

                                               if (cmbReasonType.GetValue()==1)//***پایان کار
                                                {
                                                lblLetterCode.SetText('شماره گزارش پایانی صادره از شهرداری*');
                                                lblLetterDateMun.SetText('تاریخ گزارش پایانی صادره از شهرداری*');                                                    
                                                lblTrFileUplodeLetter.SetText('تصویر نامه تعطیلی کارگاه ثبت شده در سازمان*');
                                                }

                                                if (cmbReasonType.GetValue()==2)//****تعطیلی کارگاه
                                                {
                                                lblLetterCode.SetText('شماره نامه تعطیلی کارگاه وارده به نظام*');
                                                lblLetterDateMun.SetText('تاریخ نامه تعطیلی کارگاه وارده به نظام*');   
                                                lblTrFileUplodeLetter.SetText('تصویر گزارش پایانی ثبت شده در شهرداری*');                                      
                                                }
                                        
                                                
                                            }" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="bottom">

                                                <RequiredField IsRequired="True" ErrorText="دلیل بازگشت ظرفیت را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>

                                            <Items>
                                                <dxe:ListEditItem Value="0" Text="--------"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="1" Text="پایان کار"></dxe:ListEditItem>
                                                <dxe:ListEditItem Value="2" Text="تعطیلی کارگاه"></dxe:ListEditItem>
                                            </Items>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td width="15%"></td>
                                    <td width="35%"></td>
                                </tr>
                                <tr>
                                    <td width="15%">
                                        <dxe:ASPxLabel runat="server" Text="شماره گزارش پایانی صادره از شهرداری*"
                                            ID="lblLetterCode" ClientInstanceName="lblLetterCode">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td width="35%">
                                        <TSPControls:CustomTextBox runat="server" ID="txtLetterCode" ClientInstanceName="txtLetterCode" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="شماره نامه صادره از شهرداری را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>

                                    </td>
                                    <td width="15%">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ گزارش پایانی صادره از شهرداری*"
                                            ID="lblLetterDateMun" ClientInstanceName="lblLetterDateMun">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td width="35%" runat="server" id="tdLetterDateMun">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="225px" ShowPickerOnTop="True"
                                            ID="txtLetterDateMun" PickerDirection="ToRight" ShowPickerOnEvent="OnClick" IconUrl="~/Image/Calendar.gif" Style="direction: ltr">
                                        </pdc:PersianDateTextBox>
                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True"
                                            ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtLetterDateMun" ID="PersianDateValidatorLetterDateMun">تاریخ نامعتبر</pdc:PersianDateValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <dxe:ASPxLabel runat="server" Text="تصویر نامه تعطیلی کارگاه ثبت شده در سازمان*"
                                            ID="lblTrFileUplodeLetter" ClientInstanceName="lblTrFileUplodeLetter">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <table dir="rtl" visible="true">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomAspxUploadControl runat="server" Width="258px" ShowProgressPanel="True"
                                                            UploadWhenFileChoosed="True" ID="flpLetter" InputType="Files"
                                                            ClientInstanceName="flpLetter" OnFileUploadComplete="flpAttach_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
  if(e.isValid){
	imgEndUploadLetter.SetVisible(true);
	HiddenFieldPage.Set('Letter',1);
	lblValidationLetter.SetVisible(false);
	hpFilePathLetter.SetVisible(true);
	hpFilePathLetter.SetNavigateUrl('../../../Image/TechnicalServices/Release/'+e.callbackData);
    HiddenFieldPage.Set('LetterUrl','../../../Image/TechnicalServices/Release/'+e.callbackData);
	}
	else{
	imgEndUploadLetter.SetVisible(false);
	HiddenFieldPage.Set('Letter',0);
	lblValidationLetter.SetVisible(true);
	hpFilePathLetter.SetVisible(false);
	hpFilePathLetter.SetNavigateUrl('');
    HiddenFieldPage.Set('LetterUrl','');
	}
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                            ID="lblValidationLetter" ForeColor="Red" ClientInstanceName="lblValidationLetter">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="imgEndUploadLetter" ClientInstanceName="imgEndUploadLetter">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink runat="server" Text="آدرس فایل" ClientVisible="False" Target="_blank"
                                            ID="hpFilePathLetter" NavigateUrl='<%# Bind("FilePathLetter") %>' ClientInstanceName="hpFilePathLetter">
                                        </dxe:ASPxHyperLink>

                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>توضیحات</td>
                                    <td colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtDes" Width="100%">
                                            <ValidationSettings>

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
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnNew_Click">
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" ID="btnEdit2" OnClick="btnEdit_Click" UseSubmitBehavior="False"
                                            EnableViewState="true" EnableTheming="False">
                                            <Image Url="~/Images/icons/Edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
                     if (ASPxClientEdit.ValidateGroup() == false ||  HiddenFieldPage.Get('Letter') == 0 || HiddenFieldPage.Get('Form') == 0)
                        {
                        e.processOnServer= false;   
                         alert('تکمیل موارد لازم و بارگذاری تصاویر اجباری است');
                        return;
                        }
	                    e.processOnServer= confirm('آیا از ذخیره بازگشت ظرفیت در این پروژه مطمئن می باشید؟');
                        }" />
                                            <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server" CausesValidation="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                            ToolTip="بازگشت" UseSubmitBehavior="False">
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
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldPage" ClientInstanceName="HiddenFieldPage">
            </dxhf:ASPxHiddenField>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

