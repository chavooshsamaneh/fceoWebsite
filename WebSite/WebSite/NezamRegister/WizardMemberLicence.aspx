<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="WizardMemberLicence.aspx.cs" Inherits="NezamRegister_WizardMemberLicence"
    Title="عضویت حقیقی - مدارک تحصیلی" %>

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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript" language="javascript">
        window.onbeforeunload = function () { return; };
        function SearchUniKeyPress(e, Type) {
            if (Type == 1)//DevExpress controls
            {
                if (e.htmlEvent.keyCode == 13) {
                    btnSearchUni.DoClick();
                }
            }
            else if (Type == 2)//asp controls
            {
                if (e.keyCode == 13)
                    btnSearchUni.DoClick();
            }
        }

        function SetControlValues() {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'CityName;Avg;LiId;MajorId;UniId;NumUnit;Description;StartDate;EndDate;UniName;CitId;CounId;Thesis;DefaultValue;LicenseUrl', SetValue);
        }
        function SetValue(values) {
            Avg.SetText(values[1]);
            Num.SetText(values[5]);
            Desc.SetText(values[6]);


            document.getElementById('<%=txtStartDate.ClientID%>').value = values[7];
            document.getElementById('<%=txtEndDate.ClientID%>').value = values[8];

            ComboCountry.SetValue(values[11]);

            Licence.SetValue(values[2]);
            cmbMajor.SetValue(values[3]);
            Uni.SetValue(values[4]);
            if (values[4] == null)
                uniName.SetText(values[9]);
            uniName2.SetText(values[9]);
            // cmbCity.SetValue(values[10]);
            if (values[10] == null)
                City.SetText(values[0]);

            Thesis.SetText(values[12]);
            if (values[13] == "True")
                cmbLicenceType.SetSelectedIndex(0);
            else if (values[13] == "False")
                cmbLicenceType.SetSelectedIndex(1);
            hpl.SetVisible(true);
            hpl.SetNavigateUrl('../image/Members/License/' + values[14]);
            flpi.SetVisible(false);
            if (values[10] != null) {
                //******values[11] : CountId ;values[10] : CitId******
                //cmbCity.PerformCallback(values[11]+';'+values[10]);
            }
        }
        function SetEmpty() {
            Avg.SetText("");
            Num.SetText("");
            Desc.SetText("");

            document.getElementById('<%=txtStartDate.ClientID%>').value = "";
            document.getElementById('<%=txtEndDate.ClientID%>').value = "";

            Licence.SetSelectedIndex(-1);
            cmbMajor.SetSelectedIndex(-1);
            cmbParentMajor.SetSelectedIndex(-1);
            ComboUniversity.SetSelectedIndex(-1);
            City.SetText("");
            Thesis.SetText("");
            hpl.SetVisible(false);
            hpl.SetNavigateUrl("");
            flpi.SetVisible(true);
            flpli.Set("name", 0);
            imgEndUploadImgClientIdNo.SetVisible(false);

            btn.SetEnabled(true);
        }
        function CheckDate() {
            var StartDate = document.getElementById('<%=txtStartDate.ClientID%>').value;
            var EndDate = document.getElementById('<%=txtEndDate.ClientID%>').value;
            if (EndDate < StartDate && EndDate != "")
                return -1;
            else
                return 1;
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <dxp:PanelContent ID="PanelContent3" runat="server">
                <div align="right" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server">
                    <Items>
                        <dxm:MenuItem Text="چهارچوب شئون حرفه ای" Name="Membership">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Member" Text="مشخصات فردی">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Madrak" Text="مدارک تحصیلی" Selected="true">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Job" Text="سوابق کاری">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Activity" Text="فعالیت ها">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Language" Text="زبان ها">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Summary" Text="خلاصه اطلاعات">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="End" Text="ثبت نهایی">
                        </dxm:MenuItem>
                    </Items>
                </TSPControls:CustomAspxMenuHorizontal>
                <ul class="HelpUL" style="text-align: center;">توجه تکمیل دقیق این فرم اجباری می باشد!</ul>

                <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مدارک تحصیلی"
                    runat="server" Width="100%">
                    <HeaderTemplate>
                        <table width="100%">
                            <tr>
                                <td align="right" style="width: 20%; height: 28px" valign="middle">
                                    <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="مدارک تحصیلی" Font-Bold="true">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="left" style="width: 80%; height: 30px" valign="middle">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" AutoPostBack="false" ID="btnHelp" runat="server" CausesValidation="False"
                                        EnableTheming="False" EnableViewState="False"
                                        Text=" " ToolTip="راهنما" UseSubmitBehavior="False">
                                        <Image Height="25px" Url="~/Images/Help.png" Width="25px">
                                        </Image>
                                        <ClientSideEvents Click="function(s,e){ ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <PanelCollection>
                        <dxp:PanelContent>
                            <div class="row">
                                <ul class="HelpUL">
                                    <li>چنانچه دانشگاه محل تحصیل شما، دانشگاه آزاد اسلامی واحد یاسوج می باشد تصویر مدرک تحصیلی را به صورت پشت و رو آپلود فرمایید.</li>
                                    <li><u>عنوان رشته </u>و <u>نام دانشگاه</u> بایستی کاملا مطابق با عنوان درج شده در مدرک
                                                                    تحصیلی شما باشد.درغیر این صورت روند تایید درخواست عضویت شما دچار مشکل خواهد شد.
                                    </li>
                                    <li>پس از تکمیل اطلاعات مربوط به هر یک از مدارک تحصیلی، بر روی <u>دکمه ''اضافه به لیست''</u>
                                        کلیک نمایید </li>
                                    <li>در صورتی که دارای مقطع تحصیلی <u>کارشناسی ناپیوسته</u> می باشید ، ثبت اطلاعات کاردانی
                                                                    نیز الزامی می باشد و در فرم مشخصات فردی باید تصویر استعلام عدم عضویت در کانون کاردان ها را بارگذاری کنید</li>
                                </ul>
                                <asp:Label runat="server" Font-Bold="true" Text="" ID="Label11" ForeColor="DarkRed"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-md-3">مقطع تحصیلی *</div>
                                <div class="col-md-3">
                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                        RightToLeft="True" TextField="LiName" ID="drdLicence"
                                        DataSourceID="ODBLicence" ValueType="System.String" ValueField="LiId" ClientInstanceName="Licence">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                            <RequiredField IsRequired="True" ErrorText="مقطع تحصیلی را انتخاب نمایید"></RequiredField>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomAspxComboBox>
                                    <asp:ObjectDataSource ID="ODBLicence" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.LicenceManager"></asp:ObjectDataSource>
                                </div>
                                <div class="col-md-3"></div>
                                <div class="col-md-3"></div>
                            </div>
                            <div class="row">
                                <TSPControls:CustomAspxCallbackPanel runat="server" ClientInstanceName="CallbackPanelMajor"
                                    Width="100%" ID="CallbackPanelMajor" OnCallback="CallbackPanelMajor_Callback">
                                    <Paddings Padding="0" />
                                    <PanelCollection>
                                        <dxp:PanelContent>
                                            <div class="row">

                                                <div class="col-md-3">گروه رشته*</div>
                                                <div class="col-md-3">
                                                    <TSPControls:CustomAspxComboBox runat="server"
                                                        TextField="MjName" ID="cmbParentMajor" DataSourceID="ODBParentMajor" RightToLeft="True"
                                                        ValueType="System.String" ValueField="MjId" ClientInstanceName="cmbParentMajor"
                                                        AutoPostBack="false">
                                                        <ClientSideEvents SelectedIndexChanged="function(s,e){ 
                                                                                           CallbackPanelMajor.PerformCallback('cmbChange'+';'+ cmbParentMajor.GetValue());} " />
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="طبقه بندی رشته را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomAspxComboBox>
                                                    <asp:ObjectDataSource ID="ODBParentMajor" runat="server" SelectMethod="FindMjParents"
                                                        TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
                                                </div>
                                                <div class="col-md-3">عنوان رشته*</div>
                                                <div class="col-md-3">
                                                    <TSPControls:CustomAspxComboBox runat="server"
                                                        TextField="MjName" ID="cmbMajor" RightToLeft="True" ValueType="System.String"
                                                        DataSourceID="ODBMajor" ValueField="MjId" ClientInstanceName="cmbMajor">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField ErrorText="عنوان رشته را انتخاب نمائید" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </TSPControls:CustomAspxComboBox>
                                                    <asp:ObjectDataSource ID="ODBMajor" runat="server" SelectMethod="FindMajorAndChilds"
                                                        TypeName="TSP.DataManager.MajorManager">
                                                        <SelectParameters>
                                                            <asp:Parameter DbType="Int32" DefaultValue="-1" Name="MjId" />
                                                            <asp:Parameter DbType="Int32" DefaultValue="0" Name="InActive" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">کشور</div>
                                                <div class="col-md-3">
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%" TextField="CounName" ID="ComboCountry"
                                                        RightToLeft="True" DataSourceID="OdbCountry" EnableClientSideAPI="True" ValueType="System.String"
                                                        ValueField="CounId" ClientInstanceName="ComboCountry"
                                                        EnableIncrementalFiltering="True">
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                                             CallbackPanelMajor.PerformCallback('ComboCountry'+';'+ ComboCountry.GetValue());
}"></ClientSideEvents>
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="کشور را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomAspxComboBox>
                                                </div>
                                                <div class="col-md-3">شهر *</div>
                                                <div class="col-md-3">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtCity" Width="100%" MaxLength="30" ClientInstanceName="City">
                                                        <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="true" ErrorText="شهر را وارد نمایید"></RequiredField>
                                                            <RegularExpression ErrorText=""></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">دانشگاه*</div>
                                                <div class="col-md-9">
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                        RightToLeft="True"
                                                        TextField="UnName" ID="ComboUniversity" EnableClientSideAPI="True" ValueType="System.String"
                                                        ValueField="UnId" DataSourceID="ObjectDataSourceSearchUniversity" ClientInstanceName="ComboUniversity">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="true" ErrorText="دانشگاه را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomAspxComboBox>

                                                    <asp:ObjectDataSource ID="ObjectDataSourceSearchUniversity" runat="server" TypeName="TSP.DataManager.UniversityManager"
                                                        SelectMethod="SelectConfirmedActiveUniversityByCounId" OldValuesParameterFormatString="original_{0}">
                                                        <SelectParameters>
                                                            <asp:Parameter DefaultValue="-2" Name="CounId" Type="Int32" />
                                                            <asp:Parameter DefaultValue="%" Name="UnName" Type="String" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </div>
                                            </div>

                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </TSPControls:CustomAspxCallbackPanel>
                            </div>
                            <div class="row">
                                <div class="col-md-3">تاریخ شروع</div>
                                <div class="col-md-3">
                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                        Width="240px" ShowPickerOnTop="True" ID="txtStartDate" PickerDirection="ToRight"
                                        IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                    <dxe:ASPxLabel runat="server" Text="محدوده تاریخ وارد شده صحیح نمی باشد" ClientVisible="False"
                                        ID="ASPxLabel2" ForeColor="Red" ClientInstanceName="lblDateError">
                                    </dxe:ASPxLabel>
                                </div>
                                <div class="col-md-3">تاریخ فارغ التحصیلی*</div>
                                <div class="col-md-3">
                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                        Width="240px" ShowPickerOnTop="True" ID="txtEndDate" PickerDirection="ToRight"
                                        IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                    <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                        ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtEndDate" ID="PersianDateValidator1">تاریخ را انتخاب نمایید</pdc:PersianDateValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">تعداد واحد</div>
                                <div class="col-md-3">
                                    <TSPControls:CustomTextBox runat="server" ID="txtNumUnit" Width="100%" MaxLength="3"
                                        ClientInstanceName="Num">
                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            <ErrorImage Height="14px">
                                            </ErrorImage>
                                            <RegularExpression ErrorText="تعداد واحد صحیح نیست" ValidationExpression="\d{2,3}"></RegularExpression>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </div>
                                <div class="col-md-3">معدل*</div>
                                <div class="col-md-3">
                                    <TSPControls:CustomTextBox runat="server" ID="txtAvg" Width="100%" MaxLength="5"
                                        ClientInstanceName="Avg">
                                        <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">
                                            <RequiredField IsRequired="True" ErrorText="معدل را وارد نمایید"></RequiredField>
                                            <RegularExpression ErrorText="معدل را با 2 رقم اعشار وارد نماییدمثلا 18.20" ValidationExpression="\d\d\.\d\d"></RegularExpression>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">موضوع پایان نامه</div>
                                <div class="col-md-9">
                                    <TSPControls:CustomTextBox runat="server" ID="txtThesis" Width="100%" MaxLength="255"
                                        ClientInstanceName="Thesis">
                                        <ValidationSettings>
                                            <ErrorImage Height="14px">
                                            </ErrorImage>
                                            <RegularExpression ErrorText=""></RegularExpression>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-3">توضیحات (حداکثر255کاراکتر)</div>
                                <div class="col-md-9">
                                    <TSPControls:CustomASPXMemo runat="server" Height="45px" ID="txtDescription"
                                        Width="100%" ClientInstanceName="Desc">
                                        <ClientSideEvents KeyPress="function(s,e){ CheckDevExpressTextboxLengthOnKeyPress(s,e,255); }" />
                                    </TSPControls:CustomASPXMemo>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">تصویر مدرک*</div>
                                <div class="col-md-9">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl ID="flpLicense" runat="server" ClientInstanceName="flpi"
                                                        UploadWhenFileChoosed="true" OnFileUploadComplete="flpLicense_FileUploadComplete">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
	if(e.isValid){
	imgEndUploadImgClientIdNo.SetVisible(true);
    flpli.Set('name',1);
	lbli.SetVisible(false);
	hpl.SetVisible(true);
	hpl.SetNavigateUrl('../Image/Members/License/'+e.callbackData);
	}
	else
	{
	imgEndUploadImgClientIdNo.SetVisible(false);
    flpli.Set('name',0);
	lbli.SetVisible(true);
	hpl.SetVisible(false);
	hpl.SetNavigateUrl('');
	}
}" />
                                                    </TSPControls:CustomAspxUploadControl>
                                                    <dxe:ASPxLabel ID="lblImgErr" runat="server" ClientInstanceName="lbli" ClientVisible="False"
                                                        ForeColor="Red" Text="تصویر مدرک را انتخاب نمایید">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage ID="imgEndUploadImgIdNo" runat="server" ClientInstanceName="imgEndUploadImgClientIdNo"
                                                        ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <dxe:ASPxHyperLink ID="HpLicense" runat="server" ClientInstanceName="hpl" ClientVisible="False"
                                        Target="_blank" Text="تصویر مدرک">
                                    </dxe:ASPxHyperLink>
                                </div>
                            </div>
                            <br />
                            <div class="Item-center">
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="&nbsp;&nbsp;&nbsp;اضافه به لیست"
                                    ID="btnAdd" UseSubmitBehavior="False" ClientInstanceName="btn"
                                    CausesValidation="true" OnClick="btnAdd_Click">
                                    <ClientSideEvents Click="function(s, e) {


    if(CheckDate()==-1)
	{
		e.processOnServer=false;
		lblDateError.SetVisible(true);
         return;
	}
	else
		lblDateError.SetVisible(false);

	if(flpli.Get('name')!=1)
	{
		lbli.SetVisible(true);
		e.processOnServer=false;
        return;
	}
    else lbli.SetVisible(false);  
}"></ClientSideEvents>
                                </TSPControls:CustomAspxButton>
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="&nbsp;&nbsp;&nbsp;پاک کردن فرم"
                                    CausesValidation="False" AutoPostBack="False" ID="btnRefresh" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
e.processOnServer= false;
 if(confirm('آیا مطمئن به پاک کردن اطلاعات فرم هستید؟'))
 {
	SetEmpty();	
  }
}"></ClientSideEvents>

                                </TSPControls:CustomAspxButton>
                            </div>
                            <br />
                            <TSPControls:CustomAspxDevGridView2 ID="GridViewMeLicence" runat="server" Width="100%"
                                Font-Size="8pt" AutoGenerateColumns="False" KeyFieldName="MlId" ClientInstanceName="grid"
                                OnRowDeleting="GridViewMeLicence_RowDeleting" EnableCallBacks="False">
                                <ClientSideEvents
                                    SelectionChanged="function(s, e) {
	btn.SetEnabled(false);
	SetControlValues();
}"></ClientSideEvents>
                                <Columns>
                                    
                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowClearFilterButton="true" ShowSelectButton="true" Width="50px">
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowDeleteButton="true" Width="50px">
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="MlId" Name="MlId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="LiName" Caption="مقطع"
                                        Name="LiName" Width="120px">
                                        <CellStyle Wrap="True">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MajorName" Caption="رشته"
                                        Name="MajorName" Width="120px">
                                        <CellStyle Wrap="True">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="UniName" Caption="دانشگاه"
                                        Name="UniName" Width="150px">
                                        <CellStyle Wrap="True">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="CityName"
                                        Caption="شهر" Name="CityName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Avg" Caption="معدل" Name="Avg">
                                        <CellStyle Wrap="false">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="LiId" Name="LiId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="MajorId"
                                        Name="MajorId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="UniId"
                                        Name="UniId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="NumUnit"
                                        Name="NumUnit">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="Description"
                                        Name="Description">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="تاریخ شروع" VisibleIndex="0" FieldName="StartDate"
                                        Name="StartDate">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="تاریخ فارغ التحصیلی" VisibleIndex="0" FieldName="EndDate"
                                        Name="EndDate">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="دانشگاه" VisibleIndex="0" FieldName="UniName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="CitId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="CounId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="Thesis">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="DefaultValue"
                                        Name="DefaultValue">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataCheckColumn Visible="False" FieldName="DefaultValue" Caption="پیش فرض">
                                    </dxwgv:GridViewDataCheckColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="LicenseUrl" Visible="False" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" ConfirmDelete="True" />
                            </TSPControls:CustomAspxDevGridView2>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <div class="Item-center">
                    <TSPControls:CustomAspxButton CssClass="ButtonMenue" ID="btnPre" OnClick="btnPre_Click" runat="server" Text="&nbsp;&nbsp;&nbsp;بازگشت&nbsp;&nbsp;&nbsp;"
                        ToolTip="بازگشت" EnableViewState="False" EnableTheming="False" UseSubmitBehavior="False"
                        CausesValidation="False">
                    </TSPControls:CustomAspxButton>
                    <TSPControls:CustomAspxButton CssClass="ButtonMenue" ID="btnNext" OnClick="btnNext_Click" runat="server" Text="تایید و ادامه"
                        ToolTip="تایید و ادامه" EnableViewState="False" EnableTheming="False" UseSubmitBehavior="False"
                        CausesValidation="False">

                        <ClientSideEvents Click="function(s, e) {
	if(grid.GetVisibleRowsOnPage()==0)
	{
	   alert(&quot;ثبت حداقل یک مدرک تحصیلی الزامی می باشد&quot;)
	   e.processOnServer=false;
	}
	else	
	    SetEmpty();
}"></ClientSideEvents>
                    </TSPControls:CustomAspxButton>

                </div>


                <dxhf:ASPxHiddenField ID="HiddenFieldUniValue" runat="server" ClientInstanceName="UniValue">
                </dxhf:ASPxHiddenField>
                <asp:ObjectDataSource ID="ODBUniversity" runat="server" SqlCacheDependency="NezamFars:tblUniversity"
                    CacheExpirationPolicy="Sliding" EnableCaching="True" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="SelectConfirmedUniversity" TypeName="TSP.DataManager.UniversityManager"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="OdbCity" runat="server" SelectMethod="SelectByCounId" TypeName="TSP.DataManager.CityManager">
                    <FilterParameters>
                        <asp:Parameter Name="newparameter" />
                    </FilterParameters>
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-2" Name="CounId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="OdbCountry" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.CountryManager">
                    <FilterParameters>
                        <asp:Parameter Name="newparameter" />
                    </FilterParameters>
                </asp:ObjectDataSource>
                <dxhf:ASPxHiddenField ID="HDFlpLicense" runat="server" ClientInstanceName="flpli">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
                </dxhf:ASPxHiddenField>
            </dxp:PanelContent>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
