<%@ Page Title="تاییدیه سوابق کاری" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="WizardDocJobConfirm.aspx.cs" Inherits="Members_Documents_WizardDocJobConfirm" %>

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

        <script type="text/javascript" language="javascript">

            function SetConfimVisible() {
                if (cmbConfirmType.GetValue() == 0) {
                    lblDescription.SetVisible(true);
                    txtDescription.SetVisible(true);
                    lblOfficeName.SetVisible(true);
                    txtOfficeName.SetVisible(true);
                    lblOfficeMfNo.SetVisible(false);
                    txtOfficeMfNo.SetVisible(false);
                    lblHelpDes.SetVisible(true);
                    RoundPanelconfMember1.SetVisible(false);
                    lblGrid.SetVisible(true);
                    flpGrdAttach.SetVisible(true);
                    ImageGrd.SetVisible(true);
                    ImageGrd.SetImageUrl('../../Images/person.png');
                    lblProvince.SetVisible(false);
                    ComboProvince.SetVisible(false);
                }
                if (cmbConfirmType.GetValue() == 1) {
                    lblDescription.SetVisible(false);
                    txtDescription.SetVisible(false);
                    lblOfficeName.SetVisible(false);
                    txtOfficeName.SetVisible(false);
                    lblOfficeMfNo.SetVisible(false);
                    txtOfficeMfNo.SetVisible(false);
                    lblHelpDes.SetVisible(false);
                    RoundPanelconfMember1.SetVisible(true);
                    lblGrid.SetVisible(false);
                    flpGrdAttach.SetVisible(false);
                    imgEndUploadGrd.SetVisible(false);
                    lblValidationGrd.SetVisible(false);
                    ImageGrd.SetImageUrl('');
                    HiddenFieldJobConfirm.Set('Grdname', 0);
                    ImageGrd.SetVisible(false);
                    lblProvince.SetVisible(false);
                    ComboProvince.SetVisible(false);
                }
                if (cmbConfirmType.GetValue() == 2) {
                    lblDescription.SetVisible(true);
                    txtDescription.SetVisible(true);
                    lblOfficeName.SetVisible(true);
                    txtOfficeName.SetVisible(true);
                    lblOfficeMfNo.SetVisible(false);
                    txtOfficeMfNo.SetVisible(false);
                    lblHelpDes.SetVisible(true);
                    RoundPanelconfMember1.SetVisible(false);
                    lblGrid.SetVisible(false);
                    flpGrdAttach.SetVisible(false);
                    imgEndUploadGrd.SetVisible(false);
                    lblValidationGrd.SetVisible(false);
                    ImageGrd.SetImageUrl('');
                    HiddenFieldJobConfirm.Set('Grdname', 0);
                    ImageGrd.SetVisible(false);
                    lblProvince.SetVisible(false);
                    ComboProvince.SetVisible(false);
                }
                if (cmbConfirmType.GetValue() == 3) {

                    lblProvince.SetVisible(true);
                    ComboProvince.SetVisible(true);
                    lblHelpDes.SetVisible(false);
                    //lblHelpDes.SetText('توضیحات کاملی در ارتباط با نام استان تاریخ تکمیل فرم اضافه شود');
                    RoundPanelconfMember1.SetVisible(false);
                    lblGrid.SetVisible(false);
                    flpGrdAttach.SetVisible(false);
                    imgEndUploadGrd.SetVisible(false);
                    lblValidationGrd.SetVisible(false);
                    ImageGrd.SetImageUrl('');
                    HiddenFieldJobConfirm.Set('Grdname', 0);
                    ImageGrd.SetVisible(false);
                    lblDescription.SetVisible(false);
                    txtDescription.SetVisible(false);
                    lblOfficeName.SetVisible(false);
                    txtOfficeName.SetVisible(false);
                    lblOfficeMfNo.SetVisible(false);
                    txtOfficeMfNo.SetVisible(false);
                }


            }
        </script>
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div align="right" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
               
                            <TSPControls:CustomAspxMenuHorizontal ID="MenuSteps" runat="server" >
                                <Items>
                                    <dxm:MenuItem Text="سوگند نامه" Name="Oath">
                                        <Image Width="15px" Height="15px" />
                                    </dxm:MenuItem>  
                                    <dxm:MenuItem Name="Exams" Text="آزمون ها">
                                    </dxm:MenuItem>
                                    <dxm:MenuItem Name="AccConfirm"  Text="استعلام ها">
                                    </dxm:MenuItem>
                                  
                                    <dxm:MenuItem Name="JobConfirm" Text="تاییدیه سوابق کاری" Selected="true">
                                    </dxm:MenuItem>
                                    <dxm:MenuItem Name="Summary" Text="خلاصه اطلاعات">
                                    </dxm:MenuItem>
                                    <dxm:MenuItem Name="End" Text="ثبت نهایی">
                                    </dxm:MenuItem>
                                </Items>
                              
                            </TSPControls:CustomAspxMenuHorizontal>
                     
                            <ul class="HelpUL">
                                <li>بارگزاری تصویر فرم های تاییدیه سوابق کاری الزامی می باشد </li>
                                <li>تایید سابقه کاری بایستی به یکی از سه صورت زیر انجام پذیرد :</li>
                                <ol>
                                    <li>تائید سابقه کار توسط شرکت خصوصی:</li>
                                    <ol>
                                        <li>شرکت دارای پروانه اشتغال حقوقی از سازمان نظام مهندسی فارس می باشد، در این صورت بارگزاری
                                            تصویر پروانه شرکت حقوقی الزامی است. </li>
                                        <li>شرکت دارای پروانه اشتغال حقوقی از سازمان نظام مهندسی فارس نمی باشد، در این صورت
                                            بارگزاری کپی رتبه بندی شرکت الزامی است </li>
                                    </ol>
                                    <li>تایید دو نفر مهندس عضو سازمان دارای پروانه اشتغال معتبر و ده سال سابقه کار در حرفه
                                        مهندسی(یعنی از تاریخ مدرک تحصیلی کارشناسی ده سال گذشته باشد) <b>در صورتی که دو مهندس تایید کننده سابقه کار شما از اعضای استان دیگری (غیر از استان فارس) می باشند، تاریخ فارغ التحصیلی و تاریخ اعتبار پروانه اشتغال این دو شخص در فرم سابقه کار توسط سازمان نظام مهندسی همان استان تکمیل شده و با امضا و مهر مسئول مربوطه در آن سازمان تائید گردد. </b></li>
                                    <li>تایید شرکت یا نهاد یا سازمان دولتی</li>
                                </ol>
                                <li>در نگهداری اصل فرمها و مدارک دقت کنید که در صورت نیاز بتوانید آنها را به واحد صدور
                                    پروانه تحویل دهید.</li>
                                <li>جهت دریافت فرم خام سابقه کار می توانید از مسیر <u>صفحه اول >>آرشیو فرم ها</u> در
                                    همین سایت اقدام نمایید</li>
                                <li><b>تاريخ شروع و پايان همكاري (تاريخ شروع و پايان سابقه كار) دقيقاً مي بايستي همان تاريخ نوشته شده درفرم سابقه كارباشد درصورت هرگونه مغايرت ،اطلاعات شما عودت داده مي شود.لذاخواهشمنداست درتكميل فرمها دقت لازم بعمل آيد.</b></li>
                            </ul>
                        </td>
                  
                            <TSPControls:CustomASPxRoundPanel ID="RoundPanelJobConfirm" HeaderText="تاییدیه سابقه کار"
                                runat="server" Width="100%">
                                <PanelCollection>
                                    <dxp:PanelContent>
                                        <table width="100%">
                                            <tr>

                                                <td width="20%" align="right" valign="top"><strong>تاریخ همکاری از* :</strong>
                                                </td>
                                                <td width="30%" align="right" valign="top">
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="240px" ShowPickerOnTop="True"
                                                        ID="txtDateFrom" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                                        ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox> <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ValidationGroup="j" ClientValidationFunction="PersianDateValidator"
                                                        ErrorMessage="ابتدای تاریخ همکاری را وارد نمایید" ControlToValidate="txtDateFrom"
                                                        ID="PersianDateValidator1">ابتدای تاریخ همکاری را وارد نمایید</pdc:PersianDateValidator></td>
                                                <td width="20%" align="right" valign="top"><strong>تاریخ همکاری تا* :</strong>
                                                </td>
                                                <td width="30%" align="right" valign="top">
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="240px" ShowPickerOnTop="True"
                                                        ID="txtDateTo" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                                        ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox> <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ValidationGroup="j" ClientValidationFunction="PersianDateValidator"
                                                        ErrorMessage="انتهای تاریخ همکاری را وارد نمایید" ControlToValidate="txtDateTo"
                                                        ID="PersianDateValidator2">انتهای تاریخ همکاری را وارد نمایید</pdc:PersianDateValidator></td>
                                            </tr>
                                            <tr>

                                                <td align="right" valign="top">سمت* :
                                                </td>
                                                <td align="right" valign="top" colspan="3">
                                                    <TSPControls:CustomTextBox runat="server"  Width="100%" MaxLength="150" ID="txtPosition"
                                                        ClientInstanceName="txtPosition">
                                                        <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                            <RequiredField ErrorText="سمت را وارد نمایید" IsRequired="true"></RequiredField>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%" align="right" valign="top">نوع تایید کننده*
                                                </td>
                                                <td width="30%" align="right" valign="top">
                                                    <TSPControls:CustomAspxComboBox EnableIncrementalFiltering="true" runat="server" Width="100%" RightToLeft="True"
                                                         ID="cmbConfirmType"   ValueType="System.String"
                                                        ClientInstanceName="cmbConfirmType">
                                                        <Items>
                                                            <dxe:ListEditItem Text="شرکت خصوصی" Value="0" Selected="true" />
                                                            <dxe:ListEditItem Text="عضو حقیقی سازمان(تایید دو نفر مهندس از استان فارس)" Value="1" />
                                                            <dxe:ListEditItem Text="شرکت یا نهاد دولتی" Value="2" />
                                                            <dxe:ListEditItem Text="عضو حقیقی در دیگر استانها(تایید دو نفر مهندس از سایر استان ها)"
                                                                Value="3" />
                                                        </Items>
                                                        <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                            ErrorTextPosition="Bottom">
                                                          
                                                            <RequiredField IsRequired="True" ErrorText="نوع تایید کننده را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ClientSideEvents SelectedIndexChanged="function(s,e){
                                                        SetConfimVisible();
                                                        }" />
                                                    </TSPControls:CustomAspxComboBox>
                                                </td>
                                                <td width="20%" align="right" valign="top"></td>
                                                <td width="30%" align="right" valign="top"></td>
                                            </tr>

                                            <tr>
                                                <td width="20%" align="right" valign="top">
                                                    <dxe:ASPxLabel runat="server" Text="نام شرکت*" ID="lblOfficeName" ClientInstanceName="lblOfficeName"
                                                        Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td colspan="3" width="80%" align="right" valign="top">
                                                    <TSPControls:CustomTextBox runat="server"  Width="100%" MaxLength="30" ID="txtOfficeName"
                                                        ClientInstanceName="txtOfficeName">
                                                        <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                            <RequiredField ErrorText="نام شرکت را وارد نمایید" IsRequired="true"></RequiredField>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxLabel runat="server" Text="استان*" ID="lblProvince" ClientInstanceName="lblProvince"
                                                        Width="100%" ClientVisible="false">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxComboBox ID="ComboProvince" runat="server" ClientInstanceName="ComboProvince"
                                                          DataSourceID="OdbProvince"
                                                         TextField="PrName" ValueField="PrId" ValueType="System.String"
                                                        Width="100%" RightToLeft="True" ClientVisible="false">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="j">
                                                            
                                                            <RequiredField ErrorText="استان را انتخاب نمایید" IsRequired="True" />
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomAspxComboBox>
                                                    <asp:ObjectDataSource ID="OdbProvince" runat="server" TypeName="TSP.DataManager.ProvinceManager"
                                                        SelectMethod="GetData" CacheDuration="30" FilterExpression="NezamCode<>{0}">
                                                        <FilterParameters>
                                                            <asp:Parameter Name="newparameter" />
                                                        </FilterParameters>
                                                    </asp:ObjectDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <dxe:ASPxLabel runat="server" Text="شماره پروانه شرکت" ID="lblOfficeMfNo" ClientVisible="false"
                                                        ClientInstanceName="lblOfficeMfNo" Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomTextBox runat="server"  Width="100%" MaxLength="30" ClientVisible="false"
                                                        ID="txtOfficeMfNo" ClientInstanceName="txtOfficeMfNo">
                                                        <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                            <RequiredField ErrorText="شماره پروانه شرکت را وارد نمایید" IsRequired="true"></RequiredField>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td align="right" valign="top"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td width="20%" align="right" valign="top">
                                                    <dxe:ASPxLabel runat="server" Text="شرح فعالیت شرکت" ID="lblDescription" ClientInstanceName="lblDescription"
                                                        Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td colspan="3">
                                                    <TSPControls:CustomASPXMemo runat="server" Height="30px"  Width="100%" ID="txtDescription"
                                                        ClientInstanceName="txtDescription">
                                                    </TSPControls:CustomASPXMemo>
                                                    <dxe:ASPxLabel runat="server" Text="به صورت خلاصه رشته هایی که شرکت مورد نظر در آن زمینه ها فعالیت می کند را نام ببرید.(عمران/یک یا چند رشته از رشته های نقشه برداری،معماری،شهرسازی،تاسیسات و یا تاسیسات مکانیکی)  "
                                                        ID="lblHelpDes" ClientInstanceName="lblHelpDes" ForeColor="Red" Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="تصویر فرم تاییدیه سابقه کار *" ID="Label50"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" colspan="3">
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxUploadControl ID="flpConfAttach" runat="server" ClientInstanceName="flpConfAttach"
                                                                        UploadWhenFileChoosed="true" OnFileUploadComplete="flpConfAttach_FileUploadComplete">
                                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadConf.SetVisible(true);
  	 HiddenFieldJobConfirm.Set('Confname',1);
	lblValidationConf.SetVisible(false);
	
    ImageConf.SetVisible(true);
	ImageConf.SetImageUrl('../../Image/DocMeFile/JobConfirm/'+e.callbackData);
	}
	else{
	imgEndUploadConf.SetVisible(false);
	lblValidationConf.SetVisible(true);

    ImageConf.SetVisible(false);
	ImageConf.SetImageUrl('');
	}
}" />
                                                                    </TSPControls:CustomAspxUploadControl>
                                                                    <dxe:ASPxLabel ID="lblValidationConf" runat="server" ClientInstanceName="lblValidationConf"
                                                                        ClientVisible="False" ForeColor="Red" Text="تصویر  فرم سابقه کار را انتخاب نمایید">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxImage ID="imgEndUploadConf" runat="server" ClientInstanceName="imgEndUploadConf"
                                                                        ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="ImageConf" ClientInstanceName="ImageConf"
                                                        Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                                        <EmptyImage Height="75px" Width="75px" Url="~/Images/person.png">
                                                        </EmptyImage>
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="تصویر پروانه یا گواهی رتبه بندی*" ClientInstanceName="lblGrid"
                                                        ID="lblGrid">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" colspan="3">
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxUploadControl ID="flpGrdAttach" runat="server" ClientInstanceName="flpGrdAttach"
                                                                        UploadWhenFileChoosed="true" OnFileUploadComplete="flpConfAttach_FileUploadComplete">
                                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadGrd.SetVisible(true);
  	 HiddenFieldJobConfirm.Set('Grdname',1);
	lblValidationGrd.SetVisible(false);
    ImageGrd.SetVisible(true);
	ImageGrd.SetImageUrl('../../Image/DocMeFile/OfficeGrade/'+e.callbackData);
	}
	else{
	imgEndUploadGrd.SetVisible(false);
	lblValidationGrd.SetVisible(true);
    ImageGrd.SetVisible(false);
	ImageGrd.SetImageUrl('');
	}
}" />
                                                                    </TSPControls:CustomAspxUploadControl>
                                                                    <dxe:ASPxLabel ID="lblValidationGrd" runat="server" ClientInstanceName="lblValidationGrd"
                                                                        ClientVisible="False" ForeColor="Red" Text="تصویر  پروانه یا گواهی رتبه بندی را انتخاب نمایید">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxImage ID="imgEndUploadGrd" runat="server" ClientInstanceName="imgEndUploadGrd"
                                                                        ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="ImageGrd" ClientInstanceName="ImageGrd"
                                                        Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                                        <EmptyImage Height="75px" Width="75px" Url="~/Images/person.png">
                                                        </EmptyImage>
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <TSPControls:CustomASPxRoundPanel ID="RoundPanelconfMember1" ClientInstanceName="RoundPanelconfMember1"
                                                        ClientVisible="false" HeaderText="مشخصات تایید کننده " runat="server" Width="100%">
                                                        <PanelCollection>
                                                            <dxp:PanelContent>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td align="right" valign="top" colspan="4">
                                                                            <ul style="font-family=tahoma; font-size: 8pt; line-height: 15pt; color: DarkRed">
                                                                                <b>
                                                                                    <li>توجه! جهت اطلاع رسانی سیستم به صورت اتوماتیک پیام کوتاه و نامه الکترونیک به عضو
                                                                                        انتخاب شده ارسال می کند. همچنین جهت اطمینان بیشتر کارشناسان واحد پروانه با وی تماس
                                                                                        تلفنی حاصل می نمایند. بنابراین قبلا هماهنگی های لازم را انجام دهید </li>
                                                                                </b>
                                                                            </ul>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="30%" align="right" valign="top">کد عضویت:
                                                                        </td>
                                                                        <td width="30%" align="right" valign="top">
                                                                            <TSPControls:CustomTextBox runat="server"  Width="100%" MaxLength="30" ID="txtMeId1"
                                                                                OnTextChanged="txtMeId1_TextChanged" AutoPostBack="true">
                                                                                <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                                                    <RequiredField ErrorText="کد عضویت را وارد نمایید" IsRequired="true"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                        <td width="20%" align="right" valign="top"></td>
                                                                        <td width="20%" align="right" valign="top"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="top">نام و نام خوانوادگی:
                                                                        </td>
                                                                        <td align="right" valign="top">
                                                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="lblMeName1" Font-Bold="true" Width="100%">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td align="right" valign="top">شماره پروانه:
                                                                        </td>
                                                                        <td align="right" valign="top">
                                                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="lblMeFileNo1" Font-Bold="true" Width="100%">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" valign="top">تاریخ اخذ مدرک فارغ التحصیلی:
                                                                        </td>
                                                                        <td align="right" valign="top">
                                                                            <dxe:ASPxLabel runat="server" Text="- - -" ID="lblLicenseDate" Font-Bold="true" Width="100%">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td align="right" valign="top"></td>
                                                                        <td align="right" valign="top"></td>
                                                                    </tr>
                                                                </table>
                                                            </dxp:PanelContent>
                                                        </PanelCollection>
                                                    </TSPControls:CustomASPxRoundPanel>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="center" valign="top" colspan="4">
                                                    <br />
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td align="right" valign="top">
                                                                    <TSPControls:CustomAspxButton   CssClass="ButtonMenue"  runat="server" Text="&nbsp;&nbsp;&nbsp;اضافه به لیست" 
                                                                        ValidationGroup="j" ID="btnJob" UseSubmitBehavior="False" Wrap="False" OnClick="btnJob_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	 
if( HiddenFieldJobConfirm.Get('Confname')==0){

	lblValidationConf.SetVisible(true);
	 e.processOnServer= false;
     if( cmbConfirmType.GetValue() == 0 && HiddenFieldJobConfirm.Get('Grdname')==0)
     {
     lblValidationGrd.SetVisible(true);
	 e.processOnServer= false;
     }
}
}"></ClientSideEvents>
                                                                    
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td align="right" valign="top">
                                                                    <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" Text="&nbsp;&nbsp;&nbsp;پاک کردن فرم" 
                                                                        CausesValidation="False" AutoPostBack="False" ID="btnJobRefresh" UseSubmitBehavior="False">
                                                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= false;
 if(confirm('آیا مطمئن به پاک کردن اطلاعات فرم هستید؟'))
	SetEmpty();
}"></ClientSideEvents>
                                                                      
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <TSPControls:CustomAspxDevGridView2 ID="GrdvJobCon" runat="server" Width="100%" EnableCallBacks="True"
                                                        OnRowDeleting="GrdvJobCon_RowDeleting" KeyFieldName="JobConfId" ClientInstanceName="jgrid"
                                                        AutoGenerateColumns="False">
                                                        <Settings ShowHorizontalScrollBar="true"></Settings>
                                                        <Columns>
                                                            <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowDeleteButton="true" Width="50px"> 
                                                            </dxwgv:GridViewCommandColumn>
                                                            <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="JobConfId"
                                                                Name="JobConfId">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="DateFrom" Caption="تاریخ همکاری از"
                                                                Name="DateFrom">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="DateTo" Caption="تاریخ همکاری تا"
                                                                Name="DateTo">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Position" Caption="سمت"
                                                                Name="Position">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ConfirmTypeName" Caption="نوع تایید کننده"
                                                                Name="ProjectName">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MeId" Caption="کد عضویت شخص حقیقی"
                                                                Name="Employer">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Name" Caption="نام" Name="PrTypeName">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="MFNo" Caption="شماره پروانه"
                                                                Name="StartCorporateDate">
                                                                <HeaderStyle Wrap="False" />
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="6" Width="200" FieldName="Description"
                                                                Caption="توضیحات" Name="EndCorporateDate">
                                                                <HeaderStyle Wrap="True" />
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="4" PropertiesHyperLinkEdit-Text="تصویر فرم تاییدیه "
                                                                FieldName="FileURL" Caption="تصویر فرم تاییدیه" Name="FileURL">
                                                            </dxwgv:GridViewDataHyperLinkColumn>
                                                            <dxwgv:GridViewDataHyperLinkColumn VisibleIndex="5" Width="130" PropertiesHyperLinkEdit-Text="تصویر پروانه/رتبه بندی "
                                                                FieldName="GradeURL" Caption="تصویر پروانه/رتبه بندی" Name="GradeURL">
                                                                <HeaderStyle Wrap="False" />
                                                                <CellStyle Wrap="False" />
                                                            </dxwgv:GridViewDataHyperLinkColumn>
                                                        </Columns>
                                                    </TSPControls:CustomAspxDevGridView2>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </TSPControls:CustomASPxRoundPanel>
                     <div class="Item-center">
                                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  ID="btnPre" OnClick="btnPre_Click" runat="server" Text="بازگشت" CausesValidation="False"
                                    UseSubmitBehavior="False" EnableTheming="False" EnableViewState="False" ToolTip="بازگشت"
                                    >
                                  
                                </TSPControls:CustomAspxButton>
                                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  ID="btnNext" OnClick="btnNext_Click" runat="server" Text="تایید و ادامه" UseSubmitBehavior="False"
                                    EnableTheming="False" EnableViewState="False" ToolTip="تایید و ادامه" >
                                   
                                </TSPControls:CustomAspxButton></div>
                           
                <dx:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
                </dx:ASPxHiddenField>
                <dx:ASPxHiddenField ID="HiddenFieldJobConfirm" runat="server" ClientInstanceName="HiddenFieldJobConfirm">
                </dx:ASPxHiddenField>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
