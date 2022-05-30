<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddTeacherJobHistory.aspx.cs" Inherits="Employee_Amoozesh_AddTeacherJobHistory"
    Title="مشخصات سابقه کار استاد" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 100%" align="center">
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server" visible="true">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                        [<a class="closeLink" href="#">بستن</a>]</div>
                      <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                            CausesValidation="False" ID="btnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False" OnClick="btnNew_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/new.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                            CausesValidation="False" Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	
	
	
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/edit.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                            Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnSave_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/save.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnBack_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
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
                	<TSPControls:CustomASPxRoundPanel ID="RoundPanelTeacherJob" HeaderText="سابقه کار" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

    
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="نوع فعالیت *"  ID="ASPxLabel8"></dxe:ASPxLabel>
                                                    </td>
                                                    <td  valign="top" align="right">
                                                        <TSPControls:CustomAspxComboBox runat="server" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" ValueType="System.String"     ID="cmbIsTeaching">
                                                            <Items>
                                                                <dxe:ListEditItem Text="سابقه تدریس" Value="1"></dxe:ListEditItem>
                                                                <dxe:ListEditItem Text="دیگر موارد" Value="0"></dxe:ListEditItem>
                                                            </Items>

                                                            <ButtonStyle Width="13px"></ButtonStyle>

                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>

                                                                <RequiredField IsRequired="True" ErrorText="نوع فعالیت را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                    <td valign="top" align="right"></td>
                                                    <td valign="top" align="right"></td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="نام موسسه *" Width="80px" ID="ASPxLabel1"></dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server"    ClientInstanceName="txtJobPlace" ID="txtJobPlace">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>

                                                                <RequiredField IsRequired="True" ErrorText="نام موسسه را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="نوع فعالیت آموزشی *" Width="115px" ID="ASPxLabel4"></dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Height="21px"   ClientInstanceName="txtJobName" ID="txtJobName">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>

                                                                <RequiredField IsRequired="True" ErrorText="نوع فعالیت آموزشی را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="کشور" ID="ASPxLabel3"></dxe:ASPxLabel>
                                                    </td>
                                                    <td  valign="top" align="right">
                                                        <TSPControls:CustomAspxComboBox runat="server" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" ValueType="System.String" DataSourceID="ObjdsCountry" TextField="CounName" ValueField="CounId"     ID="cmbCountry">
                                                            <ButtonStyle Width="13px"></ButtonStyle>

                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                    <td valign="top" align="right">&nbsp;<dxe:ASPxLabel runat="server" Text="شهر *" Width="47px" ID="ASPxLabel2"></dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Height="21px"   ClientInstanceName="txtJobName" ID="txtCity">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>

                                                                <RequiredField IsRequired="True" ErrorText="شهر را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ شروع *" Width="72px" ID="ASPxLabel5"></dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" Width="300px" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" ShowPickerOnTop="True" Height="21px"  ID="txtStartDate" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtStartDate" Width="126px" ID="RequiredFieldValidator8">تاریخ شروع را وارد نمایید</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ پایان *" Width="67px" ID="ASPxLabel6"></dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server"  Width="300px" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" ShowPickerOnTop="True" Height="21px"  ID="txtEndDate" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEndDate" Width="129px" ID="RequiredFieldValidator1">تاریخ پایان را وارد نمایید</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right" colspan="1">
                                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel7"></dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="4">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="38px"    ClientInstanceName="txtDescription" ID="txtDescription">
                                                            <ValidationSettings>
                                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right" colspan="1" rowspan="3">
                                                        <asp:Label runat="server" Text="فایل" Width="66px" ID="lblAttachment"></asp:Label>
                                                    </td>
                                                    <td valign="top" align="right" colspan="4">
                                                        <dxe:ASPxHyperLink runat="server" Text="ASPxHyperLink" Target="_blank" ID="linkAttachment"></dxe:ASPxHyperLink>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top" align="right" colspan="4">
                                                        <TSPControls:CustomAspxButton  runat="server" AutoPostBack="False" UseSubmitBehavior="False" CausesValidation="False" Text="انتخاب فایل"   Width="116px" ID="btnAttachTechearFile">
                                                            <ClientSideEvents Click="function(s, e) {
	ppcChooseImageClient.Show();
}"
                                                                CheckedChanged="function(s, e) {
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top" align="right" colspan="4">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False" Text="حذف فایل"   Width="113px" Height="25px" ID="btnDeleteAttachment" OnClick="btnDeleteAttachment_Click"></TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                     </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                        <br />
                	<TSPControls:CustomASPxRoundPanel ID="RoundPanelJudge" HeaderText="نظر کارشناسی" runat="server" Visible="False"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>


                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td style="vertical-align: top" align="right" colspan="1">
                                                        <asp:Label runat="server" Text="امتیاز" Width="78px" ID="Label1"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: top" align="right" colspan="2">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtGrade"  Width="68px" >
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
                                                        <asp:Label runat="server" Text="شماره جلسه" Width="78px" ID="Label9"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMeeting"  Width="68px" >
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
                                                        <asp:Label runat="server" Text="نظر کارشناسی" Width="90px" ID="Label8"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: top" align="right">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="38px" ID="txtJudgeView"  Width="520px"
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
                                          </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
            
                    <TSPControls:CustomASPxPopupControl ID="ppcChooseImage" runat="server" Width="404px"   HeaderText="انتخاب فایل" Height="77px" EnableAnimation="False" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" Modal="True" EnableViewState="False" EnableClientSideAPI="True" CloseAction="CloseButton" ClientInstanceName="ppcChooseImageClient" AutoUpdatePosition="True" AllowDragging="True" >
                        <SizeGripImage Height="12px" Width="12px"></SizeGripImage>
                        <ContentCollection>
                            <dxpc:PopupControlContentControl runat="server">
                                <div align="center">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: top; width: 100px" align="right">
                                                    <asp:Label runat="server" Text="فایل" Width="66px" ID="Label4"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; width: 100px" align="right">
                                                    <TSPControls:CustomAspxUploadControl runat="server" MaxSizeForUploadFile="0" InputType="Files" ClientInstanceName="uploader" ShowProgressPanel="True" Size="35" ID="flp" OnFileUploadComplete="UploaderImage_OnUploadComplete">
                                                        <ValidationSettings AllowedContentTypes="text/plain, text/html, text/xml, text/richtext, audio/wav, audio/mid, image/gif, image/pjpeg, image/png, image/bmp, video/avi, video/mpeg, application/pdf, application/x-zip-compressed, application/msword, application/vnd.openxmlformats-officedocument.wordprocessingml.document, application/vnd.ms-excel, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/xml, image/jpeg, image/x-png, image/x-xbitmap, application/zip" NotAllowedContentTypeErrorText="شما مجاز به آپلود این نوع فایل نیستید" FileDoesNotExistErrorText="فایل انتخابی وجود ندارد" MaxFileSizeErrorText="سایز فایل انتخابی از حداکثر مجاز (0 KB) بیشتر است" GeneralErrorText="خطایی در بارگزاری فایل در سرور انجام گرفته است"></ValidationSettings>

                                                        <ClientSideEvents FileUploadComplete="function(s, e) {  
if(e.isValid){
//imgEndUploadImgClient.SetVisible(true);
ppcChooseImageClient.Hide();
}
}"></ClientSideEvents>

                                                       
                                                    </TSPControls:CustomAspxUploadControl>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; width: 100px" align="right">
                                                    <asp:Label runat="server" Text="توضیحات" Width="66px" ID="Label5"></asp:Label>
                                                </td>
                                                <td style="vertical-align: top; width: 100px" align="right">
                                                    <TSPControls:CustomASPXMemo runat="server" Height="38px" Width="310px"   ClientInstanceName="txtAttachDescrip" ID="ASPxMemo1"></TSPControls:CustomASPXMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <TSPControls:CustomAspxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btnImageUploadClient" CausesValidation="False" Text="ذخیره"   Width="118px" ID="btnImageUpload">
                                                        <ClientSideEvents Click="function(s, e) { 
uploader.Upload();
}"></ClientSideEvents>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </dxpc:PopupControlContentControl>
                        </ContentCollection>

                        <CloseButtonImage Height="17px" Width="17px"></CloseButtonImage>

                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingTop="1px" PaddingRight="6px"></Paddings>
                        </HeaderStyle>
                    </TSPControls:CustomASPxPopupControl>
                    <br />
                                        <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldTeacherJob">
                                        </dxhf:ASPxHiddenField>
                           <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                            CausesValidation="False" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False" OnClick="btnNew_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/new.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                            CausesValidation="False" Width="25px" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/edit.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                            Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnSave_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/save.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                            CausesValidation="False" ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnBack_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
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
                    <asp:ObjectDataSource ID="ObjdsCountry" runat="server" DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetData" TypeName="TSP.DataManager.CountryManager" UpdateMethod="Update">
                       
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsCity" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.CityManager"></asp:ObjectDataSource>
         
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img align="middle" src="../../Image/indicator.gif" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>

</asp:Content>
