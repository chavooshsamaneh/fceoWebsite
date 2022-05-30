<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="WizardOffice.aspx.cs" Inherits="NezamRegister_WizardOffice" Title="عضویت حقوقی - مشخصات شرکت" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript" language="javascript">

        function SetEmpty() {
            Name.SetText("");
            txt1.SetText("");
            Subject.SetText("");
            RegNo.SetText("");
            RegPlace.SetText("");
            //FileNo.SetText("");
            Value.SetText("");
            MobileNo.SetText("");
            Stock.SetText("");
            Address.SetText("");
            Tel1.SetText("");
            Tel1_pre.SetText("");
            Tel2.SetText("");
            Tel2_pre.SetText("");
            Fax.SetText("");
            Fax_pre.SetText("");
            Website.SetText("");
            Email.SetText("");
            Desc.SetText("");
            Code.SetText("");

            cmbMembershipRequstType.SetSelectedIndex(-1);
            cmbType.SetSelectedIndex(-1);
            //cmbAttype.SetSelectedIndex(-1);

            document.getElementById('<%=txtOfRegDate.ClientID%>').value = "";
            flpArm2.Set("name", "0");
            flpSign2.Set("name", "0");
            ImageArm.SetVisible(false);
            ImageSign.SetVisible(false);
            imgArm.SetVisible(false);
            imgSign.SetVisible(false);

        }
    </script>
    <asp:UpdatePanel ID="UpdatePanelNezamRegister" runat="server">
        <ContentTemplate>
            <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
                CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
                FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
            </pdc:PersianDateScriptManager>
            <div id="DivReport" runat="server" class="DivErrors" style="text-align: right" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>

            <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server">
                <Items>
                    <dxm:MenuItem Text="چهارچوب شئون حرفه ای" Name="Membership">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Office" Text="مشخصات شرکت" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Agent" Text="شعبه ها">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Member" Text="اعضای شرکت">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Letter" Text="آگهی های رسمی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Summary" Text="خلاصه اطلاعات">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="End" Text="ثبت نهایی">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشخصات شرکت" runat="server"
                Width="100%">
                <HeaderTemplate>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 20%; height: 28px" valign="middle">
                                <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="مشخصات شرکت">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="width: 80%; height: 28px" valign="middle">
                                <TSPControls:CustomAspxButton IsMenuButton="true" AutoPostBack="false" ID="btnHelp" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False"
                                    Text=" " ToolTip="راهنما" UseSubmitBehavior="False">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                    </HoverStyle>
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
                            <div class="col-md-3">نام شرکت *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox ID="txtOfName" runat="server"
                                    Width="100%" ClientInstanceName="Name" MaxLength="80">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RequiredField ErrorText="نام شرکت را وارد نمایید" IsRequired="True" />

                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                            <div class="col-md-3">نام شرکت(انگلیسی) *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox ID="txtOfNameEn" runat="server"
                                    Width="100%" ClientInstanceName="txt1" MaxLength="80">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                        <RequiredField ErrorText="نام (انگلیسی) را وارد نمایید" IsRequired="True" />
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">موضوع شرکت *</div>
                            <div class="col-md-9">
                                <TSPControls:CustomTextBox ID="txtOfSubject" runat="server"
                                    Width="100%" ClientInstanceName="Subject" MaxLength="100">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RequiredField ErrorText="موضوع شرکت را وارد نمایید" IsRequired="True" />

                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">زمینه موضوعی شرکت*</div>
                            <div class="col-md-3">
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                    ID="cmbMembershipRequstType" ClientInstanceName="cmbMembershipRequstType"
                                    ValueType="System.String" AutoPostBack="false"
                                    RightToLeft="True">
                                    <Items>
                                        <dxe:ListEditItem Value="1" Text="انبوه ساز"></dxe:ListEditItem>
                                        <dxe:ListEditItem Value="2" Text="سازندگان مسکن و ساختمان"></dxe:ListEditItem>
                                        <dxe:ListEditItem Value="3" Text="شرکت خدمات فنی آزمایشگاهی"></dxe:ListEditItem>
                                        <dxe:ListEditItem Value="4" Text="شرکت کنترل نظارت ساختمان"></dxe:ListEditItem>
                                        <dxe:ListEditItem Value="5" Text="شرکت طراحی و نظارت"></dxe:ListEditItem>
                                        <dxe:ListEditItem Value="6" Text="مجری لوله کشی گاز"></dxe:ListEditItem>
                                    </Items>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="True" ErrorText="زمینه موضوعی شرکت را انتخاب نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                </TSPControls:CustomAspxComboBox>
                            </div>
                            <div class="col-md-3">نوع شرکت *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomAspxComboBox ID="drdOfType" runat="server"
                                    EnableIncrementalFiltering="true" DataSourceID="OdbOfType"
                                    TextField="OtName"
                                    ValueField="OtId" RightToLeft="True" ValueType="System.String" Width="100%" ClientInstanceName="cmbType">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                        <RequiredField ErrorText="نوع شرکت را انتخاب نمایید" IsRequired="True" />
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                </TSPControls:CustomAspxComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">شماره ثبت شرکت *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox ID="txtOfRegNo" runat="server"
                                    Width="100%" ClientInstanceName="RegNo" MaxLength="20">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RequiredField ErrorText="شماره ثبت را وارد نمایید" IsRequired="True" />
                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,10}" />

                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                            <div class="col-md-3">تاریخ ثبت شرکت *</div>
                            <div class="col-md-3">
                                <pdc:PersianDateTextBox ID="txtOfRegDate" ShowPickerOnEvent="OnClick" runat="server"
                                    DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" ShowPickerOnTop="True"
                                    Width="240px"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtOfRegDate" ID="PersianDateValidator1">تاریخ نامعتبر</pdc:PersianDateValidator>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">محل ثبت شرکت *</div>
                            <div class="col-md-9">
                                <TSPControls:CustomTextBox ID="txtOfRegPlace" runat="server"
                                    Width="100%" ClientInstanceName="RegPlace" MaxLength="50">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RequiredField ErrorText="محل ثبت را وارد نمایید" IsRequired="True" />

                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                  
                        <div class="row">
                            <div class="col-md-3">سرمایه شرکت (ریال) *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox ID="txtOfValue" runat="server"
                                    Width="100%" ClientInstanceName="Value">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RequiredField ErrorText="سرمایه شرکت را وارد نمایید" IsRequired="True" />
                                        <RegularExpression ErrorText="سرمایه را صحیح وارد نمایید" ValidationExpression="\d{1,11}" />

                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>

                            </div>
                            <div class="col-md-3">تعداد سهام شرکت *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox ID="txtOfStock" runat="server"
                                    Width="100%" ClientInstanceName="Stock">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RequiredField ErrorText="تعداد سهام شرکت را وارد نمایید" IsRequired="True" />
                                        <RegularExpression ErrorText="تعداد سهام را صحیح وارد نمایید" ValidationExpression="\d{1,11}" />

                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                        <div class="row">                           
                            <div class="col-md-3">آدرس شرکت*</div>
                            <div class="col-md-9">
                                <TSPControls:CustomASPXMemo ID="txtOfAddress" runat="server"
                                    Height="40px" Width="100%" ClientInstanceName="Address">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RequiredField ErrorText="آدرس شرکت را وارد نمایید" IsRequired="True" />

                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomASPXMemo>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">تلفن 1*</div>
                            <div class="col-md-3">
                                <table width="100%">
                                    <tr>
                                        <td style="vertical-align: top; text-align: right" width="80%">
                                            <TSPControls:CustomTextBox ID="txtOfTel1" runat="server"
                                                MaxLength="8" Width="100%" ClientInstanceName="Tel1">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText="شماره تلفن را وارد نمایید" IsRequired="True" />
                                                    <RegularExpression ErrorText="شماره تلفن را صحیح وارد نمایید" ValidationExpression="\d{5,8}" />

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td style="vertical-align: top; text-align: right" width="5%">
                                            <asp:Label ID="Labe71" runat="server" Text="-" Width="13px"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; text-align: right" width="15%">
                                            <TSPControls:CustomTextBox ID="txtOfTel1_pre" runat="server"
                                                MaxLength="4" Width="100%" ClientInstanceName="Tel1_pre">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText="پیش شماره تلفن را وارد نمایید" />
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}" />

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-3">تلفن 2</div>
                            <div class="col-md-3">
                                <table width="100%">
                                    <tr>
                                        <td align="right" valign="top" width="80%">
                                            <TSPControls:CustomTextBox ID="txtOfTel2" runat="server"
                                                MaxLength="8" Width="100%" ClientInstanceName="Tel2">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="شماره تلفن را صحیح وارد نمایید" ValidationExpression="\d{5,8}" />

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td align="right" valign="top" width="5%">
                                            <asp:Label ID="Label72" runat="server" Text="-" Width="100%"></asp:Label>
                                        </td>
                                        <td align="right" valign="top" width="15%">
                                            <TSPControls:CustomTextBox ID="txtOfTel2_pre" runat="server"
                                                MaxLength="4" Width="100%" ClientInstanceName="Tel2_pre">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}" />

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">فکس</div>
                            <div class="col-md-3">
                                <table>
                                    <tr>
                                        <td style="vertical-align: top; text-align: right" width="80%">
                                            <TSPControls:CustomTextBox ID="txtOfFax" runat="server"
                                                MaxLength="8" Width="100%" ClientInstanceName="Fax">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="شماره فکس را صحیح وارد نمایید" ValidationExpression="\d{5,8}" />

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td style="vertical-align: top; text-align: right" width="5%">
                                            <asp:Label ID="Labe74" runat="server" Text="-" Width="100%"></asp:Label>
                                        </td>
                                        <td style="vertical-align: top; text-align: right" width="15%">
                                            <TSPControls:CustomTextBox ID="txtOfFax_pre" runat="server"
                                                MaxLength="4" Width="100%" ClientInstanceName="Fax_pre">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}" />

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-3">شماره همراه *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox ID="txtOfMobile" runat="server"
                                    MaxLength="11" Width="100%" ClientInstanceName="MobileNo">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RequiredField ErrorText="شماره همراه را وارد نمایید" IsRequired="True" />
                                        <RegularExpression ErrorText="شماره تلفن همراه را صحیح وارد نمایید (به فرمت 09xxxxxxxxx)"
                                            ValidationExpression="09\d{9}" />
                                        <%--"(0)\d{1,10}"--%>

                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">آدرس وب سایت</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox ID="txtOfWebsite" runat="server"
                                    Width="100%" ClientInstanceName="Website" MaxLength="60">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RegularExpression ErrorText="آدرس وب سایت را با فرمت http://www.Example.com وارد نمایید"
                                            ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?" />

                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                            <div class="col-md-3">آدرس پست الکترونیکی</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox ID="txtOfEmail" runat="server"
                                    Width="100%" ClientInstanceName="Email" MaxLength="50">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RequiredField ErrorText="آدرس پست الکترونیکی را وارد نمایید" IsRequired="False" />
                                        <RegularExpression ErrorText="آدرس پست الکترونیکی را با فرمت صحیح وارد نمایید" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />

                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">تصویر آرم شرکت *</div>
                            <div class="col-md-3">
                                <table>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxUploadControl ID="flpOfArm" runat="server" UploadWhenFileChoosed="true"
                                                InputType="Images" ClientInstanceName="flpArm" OnFileUploadComplete="flpOfArm_FileUploadComplete">
                                                <ClientSideEvents FileUploadComplete="function(s, e) {
	if(e.isValid){
	imgArm.SetVisible(true);
	flpArm2.Set('name',1);
	lblArm.SetVisible(false);
	ImageArm.SetVisible(true);
	ImageArm.SetImageUrl('../Image/Temp/'+e.callbackData);
	}
	else
	{
	imgArm.SetVisible(false);
	flpArm2.Set('name',0);
	lblArm.SetVisible(true);
	ImageArm.SetVisible(false);
	ImageArm.SetImageUrl('');
	}
}" />
                                            </TSPControls:CustomAspxUploadControl>
                                            <dxe:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="lblArm" ClientVisible="False"
                                                ForeColor="Red" Text="تصویر آرم شرکت را انتخاب نمایید">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td>
                                            <dxe:ASPxImage ID="imgEndUploadImg" runat="server" ClientInstanceName="imgArm" ClientVisible="False"
                                                ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                            </dxe:ASPxImage>
                                        </td>
                                    </tr>
                                </table>
                                <dxe:ASPxImage ID="imgOfArm" Border-BorderWidth="1px" Border-BorderStyle="Solid"
                                    runat="server" Height="75px" Width="75px" ClientInstanceName="ImageArm" ClientVisible="False">
                                </dxe:ASPxImage>
                            </div>
                            <div class="col-md-3">تصویر مهر شرکت *</div>
                            <div class="col-md-3">
                                <table>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxUploadControl ID="flpOfSign" runat="server" UploadWhenFileChoosed="true"
                                                ClientInstanceName="flpSign" OnFileUploadComplete="flpOfSign_FileUploadComplete">
                                                <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgSign.SetVisible(true);
	flpSign2.Set('name',1);
	lblSign.SetVisible(false);
	ImageSign.SetVisible(true);
	ImageSign.SetImageUrl('../Image/Temp/'+e.callbackData);
	}
	else
	{
	imgSign.SetVisible(false);
	flpSign2.Set('name',0);
	lblSign.SetVisible(true);
	ImageSign.SetVisible(false);
	ImageSign.SetImageUrl('');
	}
}" />
                                            </TSPControls:CustomAspxUploadControl>
                                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" ClientInstanceName="lblSign" ClientVisible="False"
                                                ForeColor="Red" Text="تصویر مهر شرکت را انتخاب نمایید">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td>
                                            <dxe:ASPxImage ID="ASPxImage1" runat="server" ClientInstanceName="imgSign" ClientVisible="False"
                                                ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                            </dxe:ASPxImage>
                                        </td>
                                    </tr>
                                </table>
                                <dxe:ASPxImage ID="imgOfSign" Border-BorderWidth="1px" Border-BorderStyle="Solid"
                                    runat="server" Height="75px" Width="75px" ClientInstanceName="ImageSign" ClientVisible="False">
                                </dxe:ASPxImage>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">توضیحات(حداکثر255کاراکتر)</div>
                            <div class="col-md-9">
                                <TSPControls:CustomASPXMemo ID="txtOfDesc" runat="server"
                                    Height="45px" Width="100%" ClientInstanceName="Desc">
                                    <ClientSideEvents KeyPress="function(s,e){ CheckDevExpressTextboxLengthOnKeyPress(s,e,255); }" />
                                </TSPControls:CustomASPXMemo>
                            </div>
                        </div>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <div class="Item-center">
                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  ID="btnContinue" runat="server" EnableTheming="False"
                    EnableViewState="False" OnClick="btnContinue_Click" Text="تایید و ادامه" ToolTip=""
                    UseSubmitBehavior="False">
                    <ClientSideEvents Click="function(s, e) {
	
	if(CheckCharacterEncoding(txt1.GetText())==false)
 {
txt1.SetIsValid(false);
txt1.SetErrorText('حروف وارد شده نامعتبر است');
	e.processOnServer=false;
}
if(flpArm2.Get('name')!=1)
{
lblArm.SetVisible(true);
e.processOnServer=false;
}

if(flpSign2.Get('name')!=1)
{
lblSign.SetVisible(true);
e.processOnServer=false;
}


}" />

                </TSPControls:CustomAspxButton>
                <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" Text="&nbsp;&nbsp;&nbsp;پاک کردن فرم"
                    CausesValidation="False" AutoPostBack="False" ID="btnMeRefresh" UseSubmitBehavior="False">

                    <ClientSideEvents Click="function(s, e) {
 e.processOnServer= false;
 if(confirm('آیا مطمئن به پاک کردن اطلاعات فرم هستید؟'))
	SetEmpty();
}" />
                </TSPControls:CustomAspxButton>
            </div>
            <asp:ObjectDataSource ID="OdbOfType" runat="server" CacheDuration="30" EnableCaching="True"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="TSP.DataManager.OfficeTypeManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbOfAtType" runat="server" CacheDuration="30" EnableCaching="True"
                SelectMethod="GetData" TypeName="TSP.DataManager.OfficeActivityTypeManager"></asp:ObjectDataSource>
            <dxhf:ASPxHiddenField ID="HDFlpArm" runat="server" ClientInstanceName="flpArm2">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpSign" runat="server" ClientInstanceName="flpSign2">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
            </dxhf:ASPxHiddenField>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        AssociatedUpdatePanelID="UpdatePanelNezamRegister" BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div style="font-family: Tahoma; font-size: 9pt; text-align: center; padding-top: 25px; width: 300px; height: 41px; background-image: url(../Images/UploadBg.png);">
                <img id="IMG1" src="../Images/indicator.gif" align="middle" />
                لطفا صبر نمایید ...
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
