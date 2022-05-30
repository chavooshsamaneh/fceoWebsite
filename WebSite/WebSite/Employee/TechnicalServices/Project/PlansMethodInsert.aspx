<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="PlansMethodInsert.aspx.cs" Inherits="Employee_TechnicalServices_Project_PlansMethodInsert"
    Title="دستور نقشه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
      
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                        visible="false">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                        [<a class="closeLink" href="#">بستن</a>]</div>
                              <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                    <table  >
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                        CausesValidation="False" ID="btnNew" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnNew_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/new.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                        CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/edit.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                                        ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnSave_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/save.png">
                                                                        </Image>
                                                                        <ClientSideEvents Click="function(s, e) {
	if(HFPlansMethod.Get('name')!=1)
	{
		lblPlansMethod.SetVisible(true);
		e.processOnServer=false;
	}
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
                                              </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                    <br />
                    <TSP:ProjectInfo ID="prjInfo" runat="server"></TSP:ProjectInfo>
                    <br />
                    	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
        
                                <table dir="rtl" width="100%">
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="right" style="width: 20%">
                                                <dxe:ASPxLabel runat="server" Text="شماره فرم دستور نقشه" ID="ASPxLabel5" Wrap="False">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" style="width: 30%">
                                                <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxPlansMethodNo" 
                                                    Width="100%" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="شماره دستور نقشه را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right" style="width: 20%">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ صدور دستور نقشه" ID="ASPxLabel1" Wrap="False">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" style="width: 30%">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="240px" ShowPickerOnTop="True"
                                                    ID="PlansMethodDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                                    ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="PlansMethodDate" ID="RequiredFieldValidatorPlansMethodDate">تاریخ صدور دستور نقشه  را وارد نمایید</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تراکم ساختمانی (درصد)" ID="ASPxLabel2" Wrap="False">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxTarakom"  Width="100%"
                                                    >
                                                    <MaskSettings Mask="&lt;0..100&gt;"></MaskSettings>
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="لطفاً نتراکم ساختمانی را وارد نمایید">
                                                        </RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="سطح اشغال (درصد)" ID="ASPxLabel3" Wrap="False">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxEshghalSurface" 
                                                    Width="100%" >
                                                    <MaskSettings Mask="&lt;0..100&gt;"></MaskSettings>
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="لطفاً سطح اشغال را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="حداکثر ارتفاع مجاز (متر)" ID="ASPxLabel4" Wrap="False">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxAllowableHeight" 
                                                    Width="100%" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="لطفاً حداکثر ارتفاع مجاز را وارد نمایید">
                                                        </RequiredField>
                                                        <RegularExpression ErrorText="لطفا عدد تا 2 رقم اعشار وارد کنید" ValidationExpression="(\d)+((.\d{1,2}))?">
                                                        </RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="عمق حریم تجاری (متر)" ID="ASPxLabel6" Wrap="False">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxCommercialLimitation" 
                                                    Width="100%" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField ErrorText="لطفاً عمق حریم تجاری را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="لطفا عدد تا 2 رقم اعشار وارد کنید" ValidationExpression="(\d)+((.\d{1,2}))?">
                                                        </RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="محل احداث بنا" ID="ASPxLabel7" Wrap="False">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td dir="ltr" valign="top" align="right">
                                                <TSPControls:CustomAspxComboBox runat="server"  Width="100%" 
                                                    TextField="Title" ID="ASPxComboBoxStructureBuiltPlace" DataSourceID="ObjectDataSourceStructureBuiltPlace"
                                                    ValueType="System.Int32" ValueField="StructureBuiltPlaceId" 
                                                    EnableIncrementalFiltering="True" RightToLeft="True">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="لطفاً محل احداث بنا را انتخاب نمایید">
                                                        </RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تعداد بلوک" ID="ASPxLabel8" Wrap="False">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxBlockNum"  Width="100%"
                                                    >
                                                    <MaskSettings Mask="&lt;1..100&gt;"></MaskSettings>
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField ErrorText="لطفاً عمق حریم تجاری را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="لطفا عدد تا 2 رقم اعشار وارد کنید" ValidationExpression="(\d)+((.\d{1,2}))?">
                                                        </RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="ضابطه محل" ID="ASPxLabel9" Wrap="False">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxLocationCriterion" 
                                                    Width="100%" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        
                                                        <RequiredField ErrorText="لطفاً ضابطه محل را وارد نمایید" IsRequired="True" />
                                                        <RegularExpression ErrorText="" />
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="جان پناه (cm)" ID="ASPxLabel10" Wrap="False">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxMantelet"  Width="100%"
                                                    >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        
                                                        <RequiredField ErrorText="لطفاً جان پناه را وارد نمایید" />
                                                        <RegularExpression ErrorText="لطفا عدد  وارد کنید" ValidationExpression="\d*" />
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="دستور نقشه" ID="ASPxLabel11" Wrap="False">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" colspan="3" valign="top">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxUploadControl ID="flpOfPlansMethod" runat="server" ClientInstanceName="flpPlansMethod"
                                                                InputType="Files" UploadWhenFileChoosed="True" OnFileUploadComplete="flpOfPlansMethod_FileUploadComplete"
                                                                Width="240px">
                                                                <ValidationSettings GeneralErrorText="خطایی در بارگزاری فایل در سرور انجام گرفته است"
                                                                    MaxFileSizeErrorText="سایز فایل انتخابی از حداکثر مجاز (0 KB) بیشتر است" MultiSelectionErrorText="Attention! 

The following {0} files are invalid because they exceed the allowed file size ({1}) or their extensions are not allowed. These files have been removed from selection, so they will not be uploaded. 

{2}" NotAllowedFileExtensionErrorText="شما مجاز به آپلود این نوع فایل نیستید">
                                                                </ValidationSettings>
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                        if(e.isValid){
	imgPlansMethod.SetVisible(true);
	HFPlansMethod.Set('name',1);
	lblPlansMethod.SetVisible(false);
	PlansMethod.SetVisible(true);
	PlansMethod.SetNavigateUrl('../../../Image/Temp/'+e.callbackData);  
	}
	else{
	imgPlansMethod.SetVisible(false);

	lblPlansMethod.SetVisible(true);
	PlansMethod.SetVisible(true);
	PlansMethod.SetNavigateUrl(''); 
	}
}" />
                                                                <CancelButton Text="انصراف">
                                                                </CancelButton>
                                                            </TSPControls:CustomAspxUploadControl>
                                                            
                                                            <dxe:ASPxLabel ID="ASPxLabelPlansMethod" runat="server" ClientInstanceName="lblPlansMethod"
                                                                ClientVisible="False" ForeColor="Red" Text="تصویر دستور نقشه را انتخاب نمایید">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxImage ID="ASPxImagePlansMethod" runat="server" ClientInstanceName="imgPlansMethod"
                                                                ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxe:ASPxHyperLink ID="ASPxHyperLinkPlansMethod" runat="server" ClientInstanceName="PlansMethod"
                                                    ClientVisible="False" Text="مسیر فایل" Target="_blank" dir="ltr">
                                                </dxe:ASPxHyperLink>
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

                                                    <table  >
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                        CausesValidation="False" ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnNew_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/new.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                        CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/edit.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                                        ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnSave_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/save.png">
                                                                        </Image>
                                                                        <ClientSideEvents Click="function(s, e) {
	if(HFPlansMethod.Get('name')!=1)
	{
		lblPlansMethod.SetVisible(true);
		e.processOnServer=false;
	}
}" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                        CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
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
                                                 </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                        <img id="IMG1" src="../../../Image/indicator.gif" align="middle" />
                        لطفا صبر نمایید ...</div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
            <asp:ObjectDataSource ID="ObjectDataSourceStructureBuiltPlace" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.TechnicalServices.StructureBuiltPlaceManager"></asp:ObjectDataSource>
            <asp:HiddenField ID="PkPlansMethodId" runat="server" Visible="False" />
            <asp:HiddenField ID="PkProjectId" runat="server" Visible="False" />
            <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
            <asp:HiddenField ID="PkPrjReId" runat="server" Visible="False" />
            <asp:HiddenField ID="MPgMode" runat="server" Visible="False" />
            <dxhf:ASPxHiddenField ID="ASPxHiddenFieldPlansMethod" runat="server" ClientInstanceName="HFPlansMethod">
            </dxhf:ASPxHiddenField>
</asp:Content>
