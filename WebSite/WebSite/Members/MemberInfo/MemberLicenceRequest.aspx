<%@ Page Title="درخواست تغییرات مدرک تحصیلی" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberLicenceRequest.aspx.cs" Inherits="Members_MemberInfo_MemberLicenceRequest" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
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
            hpl.SetNavigateUrl('../image/temp/' + values[14]);
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
            Uni.SetSelectedIndex(0);
            uniName.SetText("");
            uniName2.SetText("");
            City.SetText("");
            gridUni.PerformCallback(ComboCountry.GetValue().toString() + ';' + txtUniNameSearch.GetText() + ';' + 'btnRefresh');
            Thesis.SetText("");
            cmbLicenceType.SetSelectedIndex(-1);

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
        function SetControlValuesUni() {
            uniName2.SetText('در حال بارگذاری ...');
            gridUni.GetRowValues(gridUni.GetFocusedRowIndex(), 'UnId;UnCode;UnName', SetValueUni);
        }
        function SetValueUni(values) {
            uniName2.SetText(values[2]);
            UniValue.Set("Id", values[0]);
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

                <table>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnSave" CssClass="ButtonMenue" OnClick="btnSave_Click" runat="server" CausesValidation="false" OnClientClick="                                 
                                if(grid.GetVisibleRowsOnPage()==0)
	                            {
	                                alert(&quot;ثبت حداقل یک مدرک تحصیلی الزامی می باشد&quot;)
	                                    return false;                                                            
	                            }                                    
                                return confirm('آیا از ذخیره و ارسال درخواست به واحد عضویت مطمئن می باشید؟');">ذخیره درخواست و ارسال به واحد عضویت</asp:LinkButton>
                        </td>

                        <td>
                            <asp:LinkButton ID="btnBack" CssClass="ButtonMenue" OnClick="btnBack_Click" runat="server" CausesValidation="false">بازگشت</asp:LinkButton>

                        </td>
                    </tr>
                </table>

                <br />
                <TSPControls:CustomASPxRoundPanel ID="CustomASPxRoundPanel1" ClientInstanceName="RoundPanelMain"
                    HeaderText="توجه" runat="server" Width="100%">
                    <PanelCollection>
                        <dx:PanelContent>
                            <table align="center" width="100%">
                                <tr>
                                    <td align="right">
                                        <ul class="HelpUL">
                                            <li><b>نحوه وارد کردن مدرک جدید</b>
                                                <ul type="circle" class="HelpUL">
                                                    <li>پس از تکمیل اطلاعات مربوط به هر یک از مدارک تحصیلی، بر روی دکمه ''اضافه به لیست''
                                                            کلیک نمایید.</li>
                                                </ul>
                                            </li>
                                            <li><b>انتخاب دانشگاه</b>
                                                <ul type="circle" class="HelpUL">
                                                    <li>جهت وارد کردن نام دانشگاه خود از علامت ذره بین کنار آن استفاده نمائید </li>
                                                    <li>در صورتی که دانشگاه شما در لیست موجود نمی باشد، نام دانشگاه را در قسمت سایر دانشگاه
                                                            ها وارد نمایید.</li>
                                                </ul>
                                            </li>
                                            <li><b>انتخاب نوع مدرک</b>
                                                <ul type="circle" class="HelpUL">
                                                    <li>شماره عضویت شما بر اساس ''کد رشته و مقطع مدرک پیش فرض'' می باشد.</li>
                                                    <li>مدرک کاردانی نمی تواند به عنوان مدرک پیش فرض انتخاب شود.</li>
                                                </ul>
                                            </li>
                                            <li>پس از انجام تغییرات مورد نظر جهت ثبت تغییرات و ارسال به مسئول مربوطه جهت بررسی و
                                                    تایید بر روی دکمه "ذخیره درخواست و ارسال به واحد عضویت" واقع در منوی بالا/پایین صفحه کلیک نمایید. </li>
                                            <li>در صورتی که قصد تغییر مدرک ازقبل ثبت شده را دارید ابتدا با استفاده از دکمه حذف واقع در لیست مدارک پایین صفحه،مدرک مورد نظر را حذف نموده و سپس مدرک با اطلاعات صحیح را با استفاده از روش ذکر شده در بالا به لیست اضافه نمایید.. </li>
                                        </ul>
                                    </td>
                                </tr>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <table width="100%">
                    <tr>
                        <td width="100%" valign="top">
                            <TSPControls:CustomASPxRoundPanel ID="RoundPanelMain" HeaderText="مشاهده" runat="server">


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
                                                    Text="" ToolTip="راهنما" UseSubmitBehavior="False">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image Url="~/Images/Help.png">
                                                    </Image>
                                                    <ClientSideEvents Click="function(s,e){ ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <PanelCollection>
                                    <dxp:PanelContent>

                                        <table dir="rtl" width="100%" align="right">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="right" width="18%">
                                                        <asp:Label runat="server" Text=" مقطع تحصیلی *" ID="Label37"></asp:Label>
                                                    </td>
                                                    <td valign="top" align="right" width="32%">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                            RightToLeft="True" TextField="LiName" ID="drdLicence"
                                                            DataSourceID="ODBLicence" ValueType="System.String" ValueField="LiId" ClientInstanceName="Licence">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" ErrorText="مقطع تحصیلی را انتخاب نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                    <td valign="top" align="right" width="20%"></td>
                                                    <td valign="top" align="right" width="30%"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" width="100%">
                                                        <TSPControls:CustomAspxCallbackPanel runat="server"
                                                            ClientInstanceName="CallbackPanelMajor" Width="100%" ID="CallbackPanelMajor"
                                                            OnCallback="CallbackPanelMajor_Callback">
                                                            <Paddings Padding="0" />
                                                            <PanelCollection>
                                                                <dxp:PanelContent>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td valign="top" align="right" width="18%">
                                                                                <asp:Label runat="server" Text="گروه رشته *" ID="Label12"></asp:Label>
                                                                            </td>
                                                                            <td valign="top" align="right" width="32%">
                                                                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                                    TextField="MjName" ID="cmbParentMajor" DataSourceID="ODBParentMajor" RightToLeft="True"
                                                                                    ValueType="System.String" ValueField="MjId" ClientInstanceName="cmbParentMajor"
                                                                                    AutoPostBack="false">
                                                                                    <ClientSideEvents SelectedIndexChanged="function(s,e){ 
                                                                                           CallbackPanelMajor.PerformCallback('cmbChange'+';'+ cmbParentMajor.GetValue());} " />
                                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                        </ErrorImage>
                                                                                        <RequiredField IsRequired="True" ErrorText="طبقه بندی رشته را انتخاب نمایید"></RequiredField>
                                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                        </ErrorFrameStyle>
                                                                                    </ValidationSettings>
                                                                                </TSPControls:CustomAspxComboBox>
                                                                                <asp:ObjectDataSource ID="ODBParentMajor" runat="server" SelectMethod="FindMjParents"
                                                                                    TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
                                                                            </td>
                                                                            <td valign="top" align="right" width="20%">
                                                                                <asp:Label runat="server" Text="عنوان رشته *" ID="Label38"></asp:Label>
                                                                            </td>
                                                                            <td valign="top" align="right" width="30%">
                                                                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                                    TextField="MjName" ID="cmbMajor" RightToLeft="True" ValueType="System.String"
                                                                                    DataSourceID="ODBMajor" ValueField="MjId" ClientInstanceName="cmbMajor">
                                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                        <RequiredField ErrorText="عنوان رشته را وارد نمائید" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </TSPControls:CustomAspxComboBox>
                                                                                <asp:ObjectDataSource ID="ODBMajor" runat="server" SelectMethod="FindMajorAndChilds"
                                                                                    TypeName="TSP.DataManager.MajorManager">
                                                                                    <SelectParameters>
                                                                                        <asp:Parameter DbType="Int32" DefaultValue="-1" Name="MjId" />
                                                                                        <asp:Parameter DbType="Int32" DefaultValue="0" Name="InActive" />
                                                                                    </SelectParameters>
                                                                                </asp:ObjectDataSource>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </TSPControls:CustomAspxCallbackPanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <asp:Label runat="server" Text="کشور" ID="Label6"></asp:Label>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                            TextField="CounName" ID="ComboCountry"
                                                            RightToLeft="True" DataSourceID="OdbCountry" EnableClientSideAPI="True" ValueType="System.String" AutoPostBack="true" OnSelectedIndexChanged="ComboCountry_SelectedIndexChanged" 
                                                            ValueField="CounId" ClientInstanceName="ComboCountry"
                                                            EnableIncrementalFiltering="True">
                                                          
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" ErrorText="کشور را انتخاب نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <asp:Label runat="server" Text="شهر *" ID="Label42"></asp:Label>
                                                        <asp:Label runat="server" Text="دیگر موارد" ID="Label5" Visible="False"></asp:Label>
                                                    </td>
                                                    <td dir="rtl" valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtCity" Width="100%" ClientInstanceName="City">
                                                            <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="true" ErrorText="شهر را وارد نمایید"></RequiredField>
                                                                <RegularExpression ErrorText=""></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <asp:Label runat="server" Text="دانشگاه *" ID="Label9"></asp:Label>
                                                    </td>
                                                    <td valign="top" align="right" dir="rtl" colspan="3">
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
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <asp:Label runat="server" Text="تاریخ شروع" ID="Label1"></asp:Label>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                            Width="165px" ShowPickerOnTop="True" ID="txtStartDate" PickerDirection="ToRight"
                                                            IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                        <dxe:ASPxLabel runat="server" Text="محدوده تاریخ وارد شده صحیح نمی باشد" ClientVisible="False"
                                                            ID="ASPxLabel2" ForeColor="Red" ClientInstanceName="lblDateError">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="left">
                                                        <asp:Label runat="server" Text="تاریخ فارغ التحصیلی *" ID="Label44"></asp:Label>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                            Width="165px" ShowPickerOnTop="True" ID="txtEndDate" PickerDirection="ToRight"
                                                            IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                            ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtEndDate" ID="PersianDateValidator1">تاریخ را انتخاب نمایید</pdc:PersianDateValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <asp:Label runat="server" Text="تعداد واحد" ID="Label2"></asp:Label>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtNumUnit" Width="100%" MaxLength="3"
                                                            ClientInstanceName="Num">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RegularExpression ErrorText="تعداد واحد صحیح نیست" ValidationExpression="\d{2,3}"></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <asp:Label runat="server" Text="معدل *" ID="Label3"></asp:Label>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtAvg" Width="100%" MaxLength="5"
                                                            ClientInstanceName="Avg">
                                                            <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="True" ErrorText="معدل را وارد نمایید"></RequiredField>
                                                                <RegularExpression ErrorText="معدل را با 2 رقم اعشار وارد نماییدمثلا 18.20" ValidationExpression="\d\d\.\d\d"></RegularExpression>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <asp:Label runat="server" Text="موضوع پایان نامه" ID="Label7"></asp:Label>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtThesis" Width="100%" MaxLength="255"
                                                            ClientInstanceName="Thesis">
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RegularExpression ErrorText=""></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <tr>
                                                        <td valign="top" align="right">
                                                            <asp:Label runat="server" Text="توضیحات" ID="Label41"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="right" colspan="3">
                                                            <TSPControls:CustomASPXMemo runat="server" Height="45px" ID="txtDescription"
                                                                Width="100%" ClientInstanceName="Desc">
                                                            </TSPControls:CustomASPXMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="top">
                                                            <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="تصویر مدرک *">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td align="right" valign="top" colspan="3">
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
	hpl.SetNavigateUrl('../../image/Members/License/'+e.callbackData);
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
                                                                                <BrowseButton Text=" انتخاب تصویر">
                                                                                    <Image Height="16px" Url="~/Images/Icons/image-upload.png" Width="16px">
                                                                                    </Image>
                                                                                </BrowseButton>
                                                                                <CancelButton Text="انصراف">
                                                                                </CancelButton>
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
                                                        </td>
                                                    </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </TSPControls:CustomASPxRoundPanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td width="90%" valign="top" align="center">
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
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
                                        </td>
                                        <td>
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
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td width="90%" valign="top">
                            <TSPControls:CustomAspxDevGridView2 ID="CustomAspxDevGridView1" runat="server" Width="100%"
                                Font-Size="8pt" AutoGenerateColumns="False" KeyFieldName="Id" ClientInstanceName="grid"
                                OnRowDeleting="CustomAspxDevGridView1_RowDeleting" EnableCallBacks="False">
                                <ClientSideEvents RowClick="function(s, e) {
	//btn.SetEnabled(false);
	//SetControlValues();
}"
                                    SelectionChanged="function(s, e) {
	btn.SetEnabled(false);
	SetControlValues();
}"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowDeleteButton="true" Width="50px">
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataImageColumn FieldName="LicenseUrl" Caption="تصویر مدرک تحصیلی" VisibleIndex="0"
                                        Width="150px">
                                        <EditCellStyle Wrap="False">
                                        </EditCellStyle>
                                        <PropertiesImage ImageHeight="150px" ImageWidth="150px">
                                        </PropertiesImage>
                                    </dxwgv:GridViewDataImageColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="LiName" Caption="مقطع"
                                        Name="LiName" Width="120px">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MjName" Caption="رشته"
                                        Name="MjName" Width="120px">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="UnName" Caption="دانشگاه"
                                        Name="UnName" Width="150px">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="CitName" Caption="شهر"
                                        Name="CitName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Avg" Caption="معدل" Name="Avg">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="StartDate" Caption="تاریخ شروع"
                                        Name="StartDate">
                                        <CellStyle Wrap="False" HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="EndDate" Caption="تاریخ پایان"
                                        Name="EndDate">
                                        <CellStyle Wrap="False" HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataCheckColumn VisibleIndex="0" FieldName="DefaultValue" Caption="پیش فرض">
                                    </dxwgv:GridViewDataCheckColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="LicenseUrl" Visible="False" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowDragDrop="False" AllowGroup="False" AllowSort="False" ConfirmDelete="True"
                                    ColumnResizeMode="Control" />
                                <Settings ShowHorizontalScrollBar="true" />
                            </TSPControls:CustomAspxDevGridView2>
                        </td>
                    </tr>
                </table>
                <br />

                <table>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnSave2" CssClass="ButtonMenue" OnClick="btnSave_Click" runat="server" CausesValidation="false" OnClientClick="  if(grid.GetVisibleRowsOnPage()==0)
	                                                    {
	                                                        alert(&quot;ثبت حداقل یک مدرک تحصیلی الزامی می باشد&quot;)
	                                                         return false;                                                            
	                                                    }                                    
                                                        return confirm('آیا از ذخیره و ارسال درخواست به واحد عضویت مطمئن می باشید؟');">ذخیره درخواست و ارسال به واحد عضویت</asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnBack2" CssClass="ButtonMenue" OnClick="btnBack_Click" runat="server" CausesValidation="false">بازگشت</asp:LinkButton>

                        </td>
                    </tr>
                </table>

                <dxhf:ASPxHiddenField ID="HiddenFieldUniValue" runat="server" ClientInstanceName="UniValue">
                </dxhf:ASPxHiddenField>
                <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>

                <asp:ObjectDataSource ID="ODBLicence" runat="server" CacheDuration="30" DeleteMethod="Delete"
                    EnableCaching="True" InsertMethod="Insert" SelectMethod="GetData" TypeName="TSP.DataManager.LicenceManager"
                    UpdateMethod="Update"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODBUniversity" runat="server" DeleteMethod="Delete" SqlCacheDependency="NezamFars:tblUniversity"
                    CacheExpirationPolicy="Sliding" EnableCaching="True" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="SelectConfirmedUniversity" TypeName="TSP.DataManager.UniversityManager"
                    UpdateMethod="Update"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="OdbCity" runat="server" SelectMethod="SelectByCounId" TypeName="TSP.DataManager.CityManager"
                    DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update">
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
