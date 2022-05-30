<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="PlanInfo.aspx.cs" Inherits="Members_TechnicalServices_Project_PlanInfo"
    Title="مشخصات نقشه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <div align="center">
        <asp:UpdatePanel ID='UpdatePanel1' runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ویرایش" ToolTip="ویرایش"
                                                CausesValidation="False" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ذخیره" ToolTip="ذخیره"
                                                Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnSave_Click">
                                                <ClientSideEvents Click="function(s, e) {
		                                    e.processOnServer= confirm(HiddenFieldPrjDes.Get('MsgAgrrement'));}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ذخیره و ارسال به طراح" ToolTip="ذخیره و ارسال به طراح"
                                                Width="25px" ID="btnSaveAndSendToDesigner" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnSaveAndSendToDesigner_Click">
                                                <ClientSideEvents Click="function(s,e){
                                                    if(confirm(HiddenFieldPrjDes.Get('MsgAgrrement'))){
                                                    e.processOnServer= confirm('پس از ذخیره و ارسال نقشه به طراح جهت تصحیح امکان تغییرات وجود ندارد.آیا مطمئن به ذخیره و ارسال نقشه به طراح جهت تصحیح می باشید؟');
                                                    }else
                                                    e.processOnServer=false;
                                                    }" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ذخیره و تایید نهایی نقشه" ToolTip="ذخیره و تایید نهایی نقشه"
                                                Width="25px" ID="btnSaveAndonfirm" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnSaveAndonfirm_Click">
                                                <ClientSideEvents Click="function(s,e){ if(confirm(HiddenFieldPrjDes.Get('MsgAgrrement'))){
                                                  e.processOnServer= confirm('با توجه به اینکه پس از تایید نهایی امکان باز گشت و ویرایش وجود ندارد،آیا از تایید نهایی مطمئن می باشید؟');
                                                    }else
                                                    e.processOnServer=false;
                                                    }" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مدیریت بازبینی نقشه ها" ToolTip="مدیریت بازبینی نقشه ها"
                                                CausesValidation="False" Width="25px" ID="btnBack" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                <TSP:ProjectInfo ID="prjInfo" runat="server" />
                <div>
                    <dxcp:ASPxLabel CssClass="HelpUL" Font-Bold="true" runat="server" Width="100%" Font-Size="15pt" ClientInstanceName="lblWarning" ID="lblWarning" Text="توجه!!">
                    </dxcp:ASPxLabel>
                </div>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelPlans" HeaderText="ویرایش" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <fieldset>
                                <legend class="HelpUL">اطلاعات نقشه</legend>
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="right" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="نوع نقشه:" ID="ASPxLabel1">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                                <dxe:ASPxLabel runat="server" Text="" ID="txtPlanType" Width="100%" Font-Bold="true" />
                                            </td>
                                            <td dir="rtl" valign="top" align="right" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="شماره نقشه:" ID="ASPxLabel2">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td dir="ltr" valign="top" align="right" width="35%">
                                                <dxe:ASPxLabel runat="server" Text="" ID="txtPlanNo" Width="100%" Font-Bold="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="توضیحات:" ID="ASPxLabel5">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" colspan="3">
                                                <dxe:ASPxLabel runat="server" Text="" ID="txtPlanDes" Width="100%" Font-Bold="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4">
                                                <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                                                    ID="GridViewAttachment" DataSourceID="ObjectDataSourceAttachments" KeyFieldName="AttachmentId"
                                                    AutoGenerateColumns="False" ClientInstanceName="GridViewAttachment">
                                                    <Settings ShowHorizontalScrollBar="true"></Settings>
                                                    <SettingsCookies Enabled="false" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Title" Caption="نوع فایل"
                                                            Width="150px">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FileName" Caption="نام فایل"
                                                            Width="200px">
                                                            <CellStyle Wrap="False">
                                                            </CellStyle>
                                                        </dxwgv:GridViewDataTextColumn>

                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="CreateDate" Caption="تاریخ ثبت">
                                                            <CellStyle Wrap="False" HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dxwgv:GridViewDataTextColumn>

                                                        <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="مشاهده"
                                                            FieldName="FilePath" Caption="لینک" Name="PlanFilePath" PropertiesHyperLinkEdit-Target="_blank">
                                                        </dxwgv:GridViewDataHyperLinkColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="4">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </TSPControls:CustomAspxDevGridView2>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>
                            <fieldset>
                                <legend class="HelpUL">نواقص نقشه</legend>
                                <table dir="rtl" width="100%">
                                    <tbody>
                                        <tr runat="server" id="tblTrViewPoint">
                                            <td runat="server" valign="top" align="right" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="موضوع" ID="ASPxLabel3">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td runat="server" valign="top" align="right" width="35%">
                                                <TSPControls:CustomTextBox runat="server" ID="txtSubject" Width="100%" MaxLength="150">
                                                    <ValidationSettings Display="Dynamic" ValidationGroup="validateViewPoint" ErrorTextPosition="Bottom">

                                                        <RequiredField IsRequired="True" ErrorText="موضوع را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td runat="server" valign="top" align="right" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="شماره برگ نقشه" Width="100%" ID="ASPxLabel4">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td runat="server" valign="top" align="right" width="35%">
                                                <TSPControls:CustomTextBox runat="server" ID="txtSheetNo" Width="100%">
                                                    <ValidationSettings Display="Dynamic" ValidationGroup="validateViewPoint" ErrorTextPosition="Bottom">

                                                        <RequiredField IsRequired="True" ErrorText="شماره برگ نقشه را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="tblTRControlerDes">
                                            <td runat="server" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="توضیحات بازبینی" ID="ASPxLabel17">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td runat="server" valign="top" align="right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" ID="txtViewPoint" Width="100%">
                                                    <ValidationSettings Display="Dynamic" ValidationGroup="validateViewPoint" ErrorTextPosition="Bottom">
                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="توضیحات بازبینی را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="tblflpViewPoint">
                                            <td>فایل پیوست</td>
                                            <td colspan="3" align="right" valign="top">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxUploadControl ID="flpControlerAttach" runat="server" ClientInstanceName="flpControlerAttach"
                                                                    MaxSizeForUploadFile="100000000" UploadWhenFileChoosed="true" InputType="Files" OnFileUploadComplete="flpControlerAttach_FileUploadComplete">
                                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
    if(e.isValid){
	imgEndUploadControler.SetVisible(true);
  	HiddenFieldPrjDes.Set('Controlername',1);
	lblControlerValidation.SetVisible(false);
	HyperLinkControlerFile.SetVisible(true);
	HyperLinkControlerFile.SetNavigateUrl('../../../Image/TechnicalServices/Plans/PlanControler/'+e.callbackData);                                                                  
	}
	else{
    
  	HiddenFieldPrjDes.Set('Controlername',0);
	imgEndUploadControler.SetVisible(false);
	lblControlerValidation.SetVisible(true);
	HyperLinkControlerFile.SetVisible(false);
	HyperLinkControlerFile.SetNavigateUrl('');
	}
}" />
                                                                </TSPControls:CustomAspxUploadControl>
                                                                <dxe:ASPxLabel ID="lblControlerValidation" runat="server" ClientInstanceName="lblControlerValidation"
                                                                    ClientVisible="False" ForeColor="Red" Text="فایل پیوست">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxImage ID="imgEndUploadControler" runat="server" ClientInstanceName="imgEndUploadControler"
                                                                    ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <dxe:ASPxHyperLink runat="server" Text="فایل پیوست" Target="_blank" ID="HyperLinkControlerFile"
                                                    ClientInstanceName="HyperLinkControlerFile">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td runat="server" id="tblTdAddViewPoint" valign="middle" align="center" colspan="4">
                                                <div class="Item-center">
                                                    <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="&nbsp;&nbsp;اضافه به ليست"
                                                        ID="btnAddViewPoint" ValidationGroup="validateViewPoint"
                                                        OnClick="btnAddViewPoint_Click" UseSubmitBehavior="false">
                                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به افزودن این اطلاعات به لیست زیر هستید؟ در صورت افزودن اطلاعات قادر به حذف یا ویرایش نیستید');
}"></ClientSideEvents>
                                                    </TSPControls:CustomAspxButton>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center" colspan="4">
                                                <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                                                    ID="GridViewViewPoint" KeyFieldName="Id" AutoGenerateColumns="False"
                                                    ClientInstanceName="GridViewDesigner">
                                                    <Settings ShowHorizontalScrollBar="true"></Settings>
                                                    <SettingsCookies Enabled="false" />
                                                    <Columns>

                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Subject" Caption="موضوع"
                                                            Width="150px">
                                                            <CellStyle Wrap="False">
                                                            </CellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="SheetNo" Caption="شماره برگ نقشه"
                                                            Width="100px">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="مشاهده"
                                                            FieldName="FileUrl" Caption="لینک" Name="FileUrl" PropertiesHyperLinkEdit-Target="_blank">
                                                        </dxwgv:GridViewDataHyperLinkColumn>
                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ViewPoint" Width="400px"
                                                            Caption="توضیحات بازبینی">
                                                        </dxwgv:GridViewDataTextColumn>

                                                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="ControllerName" Caption="نام بازبین"
                                                            Name="ControllerName" Width="150px">
                                                            <CellStyle HorizontalAlign="Right" Wrap="False">
                                                            </CellStyle>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="Id" Caption="Id">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewCommandColumn VisibleIndex="4" Caption=" " ShowClearFilterButton="true">
                                                        </dxwgv:GridViewCommandColumn>
                                                    </Columns>
                                                </TSPControls:CustomAspxDevGridView2>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ویرایش" ToolTip="ویرایش"
                                                CausesValidation="False" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ذخیره" ToolTip="ذخیره"
                                                Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnSave_Click">
                                                <ClientSideEvents Click="function(s, e) {
		                                    e.processOnServer= confirm(HiddenFieldPrjDes.Get('MsgAgrrement'));}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ذخیره و ارسال به طراح" ToolTip="ذخیره و ارسال به طراح"
                                                Width="25px" ID="btnSaveAndSendToDesigner2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnSaveAndSendToDesigner_Click">
                                                <ClientSideEvents Click="function(s,e){
                                                    if(confirm(HiddenFieldPrjDes.Get('MsgAgrrement'))){
                                                    e.processOnServer= confirm('پس از ذخیره و ارسال نقشه به طراح جهت تصحیح امکان تغییرات وجود ندارد.آیا مطمئن به ذخیره و ارسال نقشه به طراح جهت تصحیح می باشید؟');
                                                    }else
                                                    e.processOnServer=false;
                                                    }" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ذخیره و تایید نهایی نقشه" ToolTip="ذخیره و تایید نهایی نقشه"
                                                Width="25px" ID="btnSaveAndonfirm2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnSaveAndonfirm_Click">
                                                <ClientSideEvents Click="function(s,e){ if(confirm(HiddenFieldPrjDes.Get('MsgAgrrement'))){
                                                    e.processOnServer= confirm('با توجه به اینکه پس از تایید نهایی امکان باز گشت و ویرایش وجود ندارد،آیا از تایید نهایی مطمئن می باشید؟');
                                                    }else
                                                    e.processOnServer=false;
                                                    }" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مدیریت بازبینی نقشه ها" ToolTip="مدیریت بازبینی نقشه ها"
                                                CausesValidation="False" Width="25px" ID="btnBack2" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                <ClientSideEvents Click="function(s, e) { }"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldPrjDes" ClientInstanceName="HiddenFieldPrjDes">
                </dxhf:ASPxHiddenField>
                <asp:ObjectDataSource ID="ObjectDataSourceAttachments" runat="server" TypeName="TSP.DataManager.TechnicalServices.AttachmentsManager"
                    SelectMethod="SelectTSAttachmentsForPlans" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="TableTypeId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="AttachTypeId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                </div> </div>
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                    BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                            <img src="../../../Image/indicator.gif" align="middle" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
