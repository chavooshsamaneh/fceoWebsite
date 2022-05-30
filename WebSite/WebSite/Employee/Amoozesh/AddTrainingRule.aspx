<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="AddTrainingRule.aspx.cs" Inherits="Employee_Amoozesh_AddTrainingRule" Title="مشخصات حکم آموزشی" %>


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
        function SetControlValues() {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'MeId;FirstName;LastName', SetValue);
        }
        function SetValue(values) {
            ID.SetText(values[0]);
            mFirstName.SetText(values[1]);
            mLastName.SetText(values[2]);
        }
    </script>
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]</div>
                  <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>



               
                                <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                    width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top; text-align: right">
                                                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                    <tbody>
                                                        <tr>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" CausesValidation="False" 
                                                                    EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                                                    ToolTip="جدید" UseSubmitBehavior="False">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server"  EnableTheming="False"
                                                                    EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False" 
                                                                    EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                                    ToolTip="بازگشت" UseSubmitBehavior="False">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
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
                	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

 
               
                                <table dir="rtl">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="عنوان" ID="ASPxLabel1"></dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top; text-align: right" colspan="3">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="258px" MaxLength="50" ID="txtSubject" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="عنوان را وارد نمایید"></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="lblMeId" ClientInstanceName="lblMe"></dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="145px" AutoPostBack="True" ID="txtMeNo" ClientInstanceName="ID"  OnTextChanged="txtMeNo_TextChanged">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField IsRequired="True" ErrorText=" کد عضویت را وارد نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server"  ToolTip="جستجو" CausesValidation="False" ID="btnSearch1" EnableClientSideAPI="True" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnMe">
                                                    <ClientSideEvents Click="function(s, e) {
	pop.Show();
}"></ClientSideEvents>

                                                    <Image  Url="~/Images/icons/Search.png"></Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td style="vertical-align: top; text-align: right"></td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="نام" ID="lblMeFirstName" ClientInstanceName="lblMname"></dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top; text-align: right" dir="ltr">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="145px" ReadOnly="True" ID="txtMeFirstName" ClientInstanceName="mFirstName" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText=""></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="lblMeLastName" ClientInstanceName="lblMfamily"></dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="145px" ReadOnly="True" ID="txtMeLastName" ClientInstanceName="mLastName" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText=""></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="دوره آموزشی" ID="ASPxLabel2"></dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top; text-align: right" dir="ltr">
                                                <TSPControls:CustomAspxComboBox runat="server" Width="145px"   TextField="PeriodTitle" ID="cmbPeriod"   DataSourceID="ObjectDataSourcePeriod" ValueType="System.String" ValueField="PPId"  >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField IsRequired="True" ErrorText="دوره آموزشی را انتخاب نمایید"></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>

                                                    <ButtonStyle Width="13px"></ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="نوع حکم" ID="ASPxLabel5"></dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top; text-align: right" dir="ltr">
                                                <TSPControls:CustomAspxComboBox runat="server" Width="145px"   ID="cmbRuleType"  ValueType="System.String"  >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                        <RequiredField ErrorText=""></RequiredField>

                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>

                                                    <ButtonStyle Width="13px"></ButtonStyle>
                                                    <Items>
                                                        <dxe:ListEditItem Text="حکم 1" Value="حکم 1" />
                                                        <dxe:ListEditItem Text="حکم2" Value="حکم2" />
                                                    </Items>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ" Width="81px" ID="ASPxLabel3"></dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="145px" ShowPickerOnTop="True" ID="txtDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDate" ID="RequiredFieldValidator1" Display="Dynamic">تاریخ را وارد نمایید</asp:RequiredFieldValidator>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <dxe:ASPxLabel runat="server" Text="مطرح کننده" Width="80px" ID="ASPxLabel4"></dxe:ASPxLabel>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="145px" MaxLength="50" ID="txtBroacher" >
                                                    <ValidationSettings>
                                                        <RequiredField ErrorText=""></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; text-align: right">
                                                <asp:Label runat="server" Text="شرح موضوع" Width="66px" ID="Label3"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top; text-align: right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="32px"  Width="389px" ID="txtDesc" ></TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; text-align: right">
                                                <asp:Label runat="server" Text="تاریخ بررسی کمیته آموزش" Width="73px" ID="lbl1"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="145px" MaxLength="50" ReadOnly="True" ID="txtReviewDate" >
                                                    <ValidationSettings ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText=""></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top; text-align: right"></td>
                                            <td style="vertical-align: top; text-align: right"></td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; text-align: right">
                                                <asp:Label runat="server" Text="تاریخ صورت جلسه" Width="102px" ID="lbl2"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="145px" MaxLength="50" ReadOnly="True" ID="txtSessionDate" >
                                                    <ValidationSettings ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText=""></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <asp:Label runat="server" Text="شماره صورت جلسه" Width="102px" ID="lbl3"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top; text-align: right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="145px" MaxLength="50" ReadOnly="True" ID="txtSessionNo" >
                                                    <ValidationSettings ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText=""></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; text-align: right">
                                                <asp:Label runat="server" Text="شرح نتیجه بررسی" Width="98px" ID="lbl4"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top; text-align: right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="32px"  Width="389px" ReadOnly="True" ID="txtResultDesc" ></TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <br />
                       <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel4"  HeaderText="فایل های پیوست" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                                  
                                                <table runat="server" id="TblFile" style="width: 378px" dir="rtl">
                                                    <tr runat="server" id="Tr1">
                                                        <td runat="server" id="Td1" style="vertical-align: top; text-align: right">
                                                            <asp:Label runat="server" Text="فایل" Width="24px" ID="lblimg"></asp:Label>

                                                        </td>
                                                        <td runat="server" id="Td2" style="text-align: right">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <TSPControls:CustomAspxUploadControl runat="server" ShowProgressPanel="True" MaxSizeForUploadFile="0" ID="flp" InputType="Files" ClientInstanceName="flpc" OnFileUploadComplete="flp_FileUploadComplete">
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
	   if(thisext == extension[i]) {flpc.Upload(); return; }
	}
alert(&quot;شما مجاز به آپلود این فایل نیستید&quot;);
s.ClearText();
}"
                                                                                    FileUploadComplete="function(s, e) {
	imgEndUploadImgClient.SetVisible(true);
}"></ClientSideEvents>

                                                                                <ValidationSettings AllowedContentTypes="text/plain, text/html, text/xml, text/richtext, audio/wav, audio/mid, image/gif, image/pjpeg, image/png, image/bmp, video/avi, video/mpeg, application/pdf, application/x-zip-compressed, application/msword, application/vnd.openxmlformats-officedocument.wordprocessingml.document, application/vnd.ms-excel, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/xml, image/jpeg, image/x-png, image/x-xbitmap, application/zip" FileDoesNotExistErrorText="فایل انتخابی وجود ندارد" NotAllowedContentTypeErrorText="شما مجاز به آپلود این نوع فایل نیستید" GeneralErrorText="خطایی در بارگزاری فایل در سرور انجام گرفته است" MaxFileSizeErrorText="سایز فایل انتخابی از حداکثر مجاز (0 KB) بیشتر است"></ValidationSettings>

                                                                                <CancelButton Text="انصراف"></CancelButton>
                                                                            </TSPControls:CustomAspxUploadControl>

                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png" ID="imgEndUploadImg" ClientInstanceName="imgEndUploadImgClient"></dxe:ASPxImage>

                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="Tr3">
                                                        <td runat="server" id="Td4" style="vertical-align: top; text-align: right">
                                                            <asp:Label runat="server" Text="توضیحات" Width="86px" ID="Label10"></asp:Label>

                                                        </td>
                                                        <td runat="server" id="Td5" style="text-align: right">
                                                            <TSPControls:CustomASPXMemo runat="server" Height="32px"  Width="396px" ID="txtDescImg" >
                                                                <ValidationSettings>
                                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomASPXMemo>

                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="Tr2">
                                                        <td runat="server" id="Td3" style="text-align: center" colspan="2">
                                                            <br />
                                                            <TSPControls:CustomAspxButton  runat="server" Text="اضافه"  CausesValidation="False" Width="70px" ID="btnAddFlp" UseSubmitBehavior="False"  OnClick="btnAddFlp_Click"></TSPControls:CustomAspxButton>

                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>

                                                <br />
                                                <div dir="rtl">
                                                    <TSPControls:CustomAspxDevGridView runat="server"  EnableViewState="False" Width="379px" ID="AspxGridFlp" KeyFieldName="Id" AutoGenerateColumns="False"  OnRowDeleting="AspxGridFlp_RowDeleting">
                                                        <SettingsText CommandClearFilter="پاک کردن فیلتر" ConfirmDelete="آیا مطمئن به حذف این ردیف هستید؟" EmptyDataRow="هیچ داده ای وجود ندارد" GroupPanel="برای گروه بندی از این قسمت استفاده کنید" CommandEdit="ویرایش" CommandDelete="حذف" CommandSelect="انتخاب" CommandNew="جدید" CommandUpdate="ذخیره" CommandCancel="انصراف"></SettingsText>

                                                        <Styles  >
                                                            <GroupPanel BackColor="CornflowerBlue" ForeColor="White"></GroupPanel>

                                                            <Header HorizontalAlign="Center"></Header>

                                                            <SelectedRow BackColor="White" ForeColor="Black"></SelectedRow>
                                                        </Styles>

                                                        <Settings ShowGroupPanel="True"></Settings>

                                                        <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>

                                                        <SettingsPager Mode="ShowAllRecords">
                                                            <AllButton Text="همه رکوردها"></AllButton>

                                                            <FirstPageButton Text="اولین صفحه"></FirstPageButton>

                                                            <LastPageButton Text="آخرین صفحه"></LastPageButton>

                                                            <Summary Text="صفحه: {0} از {1} (تعداد کل:{2})"></Summary>

                                                            <NextPageButton Text="صفحه بعد"></NextPageButton>

                                                            <PrevPageButton Text="صفحه قبل"></PrevPageButton>
                                                        </SettingsPager>
                                                        <Columns>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FilePath" Caption="فایل" Name="FilePath">
                                                                <DataItemTemplate>
                                                                    <asp:HyperLink ID="HyperLink1" runat="server" Text="آدرس فایل" Target="_blank" NavigateUrl='<%# Bind("TempImgUrl") %>'></asp:HyperLink>
                                                                </DataItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
                                                                </EditItemTemplate>
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Description" Caption="توضیحات" Name="Description"></dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewCommandColumn VisibleIndex="2" Caption=" " ShowDeleteButton="true">
                                                            </dxwgv:GridViewCommandColumn>
                                                        </Columns>

                                                        <SettingsLoadingPanel Text="در حال بارگذاری"></SettingsLoadingPanel>
                                                    </TSPControls:CustomAspxDevGridView>

                                                </div>
                                           </dxp:PanelContent>
                                </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                          </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                <br />

                   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


 
                                <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                                    width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top; text-align: right">
                                                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                                    <tbody>
                                                        <tr>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server" CausesValidation="False" 
                                                                    EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                                                    ToolTip="جدید" UseSubmitBehavior="False">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server"  EnableTheming="False"
                                                                    EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton6" runat="server" CausesValidation="False" 
                                                                    EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                                    ToolTip="بازگشت" UseSubmitBehavior="False">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                    </HoverStyle>
                                                                    <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
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
                <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl1" runat="server" Height="269px"    HeaderText="جستجو" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" PopupElementID="btnSearch1" CloseAction="CloseButton" ClientInstanceName="pop">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <TSPControls:CustomAspxDevGridView runat="server"  EnableViewState="False" Width="544px" ID="GridMember" DataSourceID="ObjectDataSource1" KeyFieldName="MeId" AutoGenerateColumns="False" ClientInstanceName="grid"  OnCustomCallback="GridMember_CustomCallback">
                                <ClientSideEvents RowDblClick="function(s, e) {
pop.Hide();
//loadPanel1.Show();
	SetControlValues();
	
}"></ClientSideEvents>

                               
                             
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MeId" Caption="کد عضویت" Name="MeId"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FirstName" Caption="نام" Name="FirstName"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="LastName" Caption="نام خانوادگی" Name="LastName"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="FatherName" Caption="نام پدر" Name="FatherName"></dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="SSN" Caption="کد ملی" Name="SSN"></dxwgv:GridViewDataTextColumn>
                                </Columns>

                                <SettingsLoadingPanel Text="در حال بارگذاری"></SettingsLoadingPanel>
                            </TSPControls:CustomAspxDevGridView>
                            <asp:ObjectDataSource runat="server" DeleteMethod="Delete" EnableCaching="True" InsertMethod="Insert" CacheExpirationPolicy="Sliding" SelectMethod="SearchMemberByMjId" ID="ObjectDataSource1" CacheDuration="3600" UpdateMethod="Update" TypeName="TSP.DataManager.MemberManager" OldValuesParameterFormatString="original_{0}">                            
                                <SelectParameters>
                                    <asp:Parameter Type="Int16" DefaultValue="-1" Name="MeId"></asp:Parameter>
                                    <asp:Parameter Type="String" DefaultValue="%" Name="FirstName"></asp:Parameter>
                                    <asp:Parameter Type="String" DefaultValue="%" Name="LastName"></asp:Parameter>
                                    <asp:Parameter Type="String" DefaultValue="%" Name="MobileNo"></asp:Parameter>
                                    <asp:Parameter Type="Int16" DefaultValue="-1" Name="MjId"></asp:Parameter>
                                </SelectParameters>                               
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource runat="server" CacheKeyDependency="CachePersonMember" DeleteMethod="Delete" EnableCaching="True" InsertMethod="Insert" CacheExpirationPolicy="Sliding" SelectMethod="GetData" ID="ObjectDataSource2" CacheDuration="3600" UpdateMethod="Update" TypeName="TSP.DataManager.MemberManager" OldValuesParameterFormatString="original_{0}">
                               
                            </asp:ObjectDataSource>
                            &nbsp;&nbsp;
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>

                    <HeaderStyle>
                        <Paddings PaddingTop="1px" PaddingRight="6px" PaddingLeft="10px"></Paddings>
                    </HeaderStyle>

                    <SizeGripImage Height="12px" Width="12px"></SizeGripImage>

                    <CloseButtonImage Height="17px" Width="17px"></CloseButtonImage>
                </TSPControls:CustomASPxPopupControl>
                <asp:HiddenField ID="RuleId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
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
        <asp:ObjectDataSource ID="ObjectDataSourcePeriod" runat="server"
            CacheExpirationPolicy="Sliding" CacheKeyDependency="CachePersonMember" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="TSP.DataManager.PeriodPresentManager"></asp:ObjectDataSource>
    </div>
</asp:Content>


