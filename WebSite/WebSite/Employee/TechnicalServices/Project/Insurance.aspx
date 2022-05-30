<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="Insurance.aspx.cs" Inherits="Employee_TechnicalServices_Project_Insurance"
    Title="بیمه" %>

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
  
                                                    <table >
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
                                                                        <ClientSideEvents Click="function(s, e) {
	if(HFInsurance.Get('name')!=1)
	{
		lblInsurance.SetVisible(true);
		e.processOnServer=false;
	}
}"></ClientSideEvents>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/save.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                                                        CausesValidation="False" ID="btnDelete" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                                        EnableViewState="False" EnableTheming="False" OnClick="btnDelete_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/delete.png">
                                                                        </Image>
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
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت پروژه"
                                                                        CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="../../../Images/icons/BakToManagment.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table></dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                        <TSPControls:CustomAspxMenuHorizontal ID="MainMenu" runat="server" CssClass="ProjectMainMenuHorizontal"   OnItemClick="MainMenu_ItemClick" >
                            <Items>
                                <dxm:MenuItem Text="مشخصات پروژه" Name="Project"  ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="مالک" Name="Owner" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="مالی پروژه" Name="Accounting" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                     <Items>                                
                          <%--      <dxm:MenuItem Text="مالی مالکان" Name="AccOwner">
                                </dxm:MenuItem>--%>
                                <dxm:MenuItem Text="مالی طراحان" Name="AccDesigner" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                               <%-- <dxm:MenuItem Text="مالی ناظران" Name="AccObserver">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="مالی مجریان" Name="AccImp">
                                </dxm:MenuItem>--%>
                            </Items>
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="نقشه" Name="Plans" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="طراح" Name="Designer" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="ناظر" Name="Observers" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                             <dxm:MenuItem Text="مجری" Name="Implementer"  ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="قرارداد" Name="Contract" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                                  <%-- <dxm:MenuItem Text="زمان بندی" Name="Timing">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="پروانه ساخت" Name="BuildingsLicense">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="اعلام وضعیت" Name="StatusAnnouncement">
                                </dxm:MenuItem>--%>
                            </Items>
                          
                        </TSPControls:CustomAspxMenuHorizontal>
                        <br />
                        <TSPControls:CustomAspxMenuHorizontal ID="ProjectMenu" runat="server" OnItemClick="ProjectMenu_ItemClick" CssClass="ProjectSubMenuHorizontal">
                            <Items>
                                <dxm:MenuItem Text="اطلاعات پایه" Name="BaseInfo" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="پلاک ثبتی" Name="RegisteredNo" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="دستور نقشه" Name="PlansMethod" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="بلوک" Name="Block" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="بیمه" Name="Insurance" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                                </dxm:MenuItem>
                            </Items>
                          
                        </TSPControls:CustomAspxMenuHorizontal>
              	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
        
                                    <table dir="rtl" width="100%">
                                        <tbody>
                                            <tr>
                                                <td valign="top" align="right" width="15%">
                                                    <asp:Label runat="server" Text="تاریخ ایجاد" ID="Label47"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" width="35%">
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="230px" Enabled="False"
                                                        ShowPickerOnTop="True" ID="CreateDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="CreateDate" ID="RequiredFieldValidatorRegDate">تاریخ ایجاد  را وارد نمایید</asp:RequiredFieldValidator>
                                                </td>
                                                <td valign="top" align="right" width="15%">
                                                    <asp:Label runat="server" Text="تاریخ صدور" ID="Label13"></asp:Label>
                                                </td>
                                                <td dir="ltr" valign="top" align="right" width="35%">
                                                    <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                                        Width="230px" ShowPickerOnTop="True" ID="RegDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RegDate" ID="RequiredFieldValidatorFileDate">تاریخ صدور  را وارد نمایید</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="شماره بیمه نامه" ID="Label49"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxInsuranceNo"  Width="100%"
                                                        Enabled="False" >
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField ErrorText="لطفاً شماره بیمه نامه را وارد نمایید" IsRequired="True">
                                                            </RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="نام شرکت بیمه" ID="Label50"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxInsName"  Width="100%"
                                                        Enabled="False" >
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField ErrorText="لطفاً نام شرکت بیمه را وارد نمایید" IsRequired="True">
                                                            </RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="کد نماینده بیمه" ID="Label12"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxInsCode"  Width="100%"
                                                        >
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="لطفاً کد نماینده بیمه را وارد نمایید">
                                                            </RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="مبلغ (ریال)" ID="Label10"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxAmount"  Width="100%"
                                                        >
                                                        <MaskSettings ErrorText="لطفا عدد وارد کنید" Mask="&lt;0..10000000000000000000000000000000000000&gt;">
                                                        </MaskSettings>
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="لطفاً مبلغ  را وارد نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="آدرس نماینده بیمه" ID="Label8"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" colspan="3">
                                                    <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="TextBoxAddress" 
                                                        Width="100%" >
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="لطفاً آدرس را وارد نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomASPXMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="توضیحات" ID="Label1"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" colspan="3">
                                                    <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtDescription" 
                                                        Width="100%" >
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField ErrorText=""></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomASPXMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="بیمه نامه" ID="Label54"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" colspan="3">
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                        ID="flpOfInsurance" InputType="Files" ClientInstanceName="flpInsurance" OnFileUploadComplete="flpOfInsurance_FileUploadComplete">
                                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                        if(e.isValid){
	imgInsurance.SetVisible(true);
	HFInsurance.Set('name',1);
	lblInsurance.SetVisible(false);
	Insurance.SetVisible(true);
	Insurance.SetNavigateUrl('../../../Image/Temp/'+e.callbackData);
    }
    else{
	imgInsurance.SetVisible(false);
	HFInsurance.Set('name',0);
	lblInsurance.SetVisible(true);
	Insurance.SetVisible(false);
	Insurance.SetNavigateUrl('');
    }
}"></ClientSideEvents>
                                                                    </TSPControls:CustomAspxUploadControl>
                                                                    <%--<TSPControls:CustomAspxUploadControl runat="server" ShowProgressPanel="True" MaxSizeForUploadFile="0"
                                                                        ID="flpOfInsurance" InputType="Files" ClientInstanceName="flpInsurance" OnFileUploadComplete="flpOfInsurance_FileUploadComplete">
                                                                        <ClientSideEvents TextChanged="function (s,e) {
	var InputFile=s.GetText();
var extension = new Array();

extension[0] = &quot;.txt&quot;;
extension[1] = &quot;.html&quot;;
extension[1] = &quot;.htm&quot;;
extension[2] = &quot;.xml&quot;;
extension[3] = &quot;.rtf&quot;;
extension[4] = &quot;.wav&quot;;
extension[5] = &quot;.mid&quot;;
extension[6] = &quot;.gif&quot;;
extension[7] = &quot;.jpg&quot;;
extension[8] = &quot;.jpeg&quot;;
extension[9] = &quot;.png&quot;;
extension[10] = &quot;.bmp&quot;;
extension[11] = &quot;.avi&quot;;
extension[12] = &quot;.mpeg&quot;;
extension[13] = &quot;.mpg&quot;;
extension[14] = &quot;.zip&quot;;
extension[15] = &quot;.doc&quot;;
extension[16] = &quot;.docx&quot;;
extension[17] = &quot;.xls&quot;;
extension[18] = &quot;.xlsx&quot;;
extension[19] = &quot;.pdf&quot;;


var thisext = InputFile.substr(InputFile.lastIndexOf('.')).toLowerCase();
for(var i = 0; i &lt; extension.length; i++) 
   {
	   if(thisext == extension[i]) {flpInsurance.Upload(); return; }
	}
alert(&quot;شما مجاز به آپلود این فایل نیستید&quot;);
s.ClearText();
}" FileUploadComplete="function(s, e) {
	imgInsurance.SetVisible(true);
	HFInsurance.Set('name',1);
	lblInsurance.SetVisible(false);
	Insurance.SetVisible(true);
	Insurance.SetText(s.GetText());
	Insurance.SetNavigateUrl('../../../Image/Temp/'+e.callbackData);
}"></ClientSideEvents>
                                                                        <ValidationSettings AllowedContentTypes="text/plain, text/html, text/xml, text/richtext, audio/wav, audio/mid, image/gif, image/pjpeg, image/png, image/bmp, video/avi, video/mpeg, application/pdf, application/x-zip-compressed, application/msword, application/vnd.openxmlformats-officedocument.wordprocessingml.document, application/vnd.ms-excel, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/xml, image/jpeg, image/x-png, image/x-xbitmap, application/zip"
                                                                            FileDoesNotExistErrorText="فایل انتخابی وجود ندارد" NotAllowedContentTypeErrorText="شما مجاز به آپلود این نوع فایل نیستید"
                                                                            GeneralErrorText="خطایی در بارگزاری فایل در سرور انجام گرفته است" MaxFileSizeErrorText="سایز فایل انتخابی از حداکثر مجاز (0 KB) بیشتر است">
                                                                        </ValidationSettings>
                                                                        <CancelButton Text="انصراف">
                                                                        </CancelButton>
                                                                    </TSPControls:CustomAspxUploadControl>--%>
                                                                    <dxe:ASPxLabel runat="server" Text="تصویر بیمه نامه را انتخاب نمایید" ClientVisible="False"
                                                                        ID="ASPxLabelInsurance" ForeColor="Red" ClientInstanceName="lblInsurance">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                        ID="ASPxImageInsurance" ClientInstanceName="imgInsurance">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <dxe:ASPxHyperLink runat="server" Text="مسیر فایل" ClientVisible="False" Target="_blank"
                                                        ID="ASPxHyperLinkInsurance" ClientInstanceName="Insurance" dir="ltr">
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
  
                                                        <table >
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
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true"  runat="server" Text=" "  ToolTip="ویرایش"
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
                                                                            <ClientSideEvents Click="function(s, e) {
	if(HFInsurance.Get('name')!=1)
	{
		lblInsurance.SetVisible(true);
		e.processOnServer=false;
	}
}"></ClientSideEvents>
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                            </HoverStyle>
                                                                            <Image  Url="~/Images/icons/save.png">
                                                                            </Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                                                            CausesValidation="False" ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                                            EnableViewState="False" EnableTheming="False" OnClick="btnDelete_Click">
                                                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                            </HoverStyle>
                                                                            <Image  Url="~/Images/icons/delete.png">
                                                                            </Image>
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
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت پروژه"
                                                                            CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                                                            EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                            </HoverStyle>
                                                                            <Image  Url="../../../Images/icons/BakToManagment.png">
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
            <asp:HiddenField ID="PkProjectId" runat="server" Visible="False" />
            <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
            <asp:HiddenField ID="PkInsuranceId" runat="server" Visible="False" />
            <asp:HiddenField ID="PkPrjReId" runat="server" Visible="False" />
            <asp:HiddenField ID="MPgMode" runat="server" Visible="False" />
    <dxhf:ASPxHiddenField ID="ASPxHiddenFieldInsurance" runat="server" ClientInstanceName="HFInsurance">
    </dxhf:ASPxHiddenField>
</asp:Content>
