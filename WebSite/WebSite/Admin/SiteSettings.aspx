<%@ Page Title="تنظیمات سایت" Language="C#" MasterPageFile="~/Admin/MasterPage.master"
    AutoEventWireup="true" CodeFile="SiteSettings.aspx.cs" Inherits="Admin_SiteSettings" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
            href="#">بستن</a>]
    </div>
    <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="100%"
        GroupBoxCaptionOffsetY="-24px" HeaderText="" RightToLeft="True"
        ShowHeader="False">
        <ContentPaddings PaddingBottom="2px" PaddingLeft="2px" PaddingTop="2px" PaddingRight="2px" />
        <HeaderStyle Height="23px">
            <Paddings PaddingBottom="0px" PaddingLeft="2px" PaddingTop="0px" />
        </HeaderStyle>
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <div style="width: 100%" dir="rtl">
                    <table>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true"  runat="server" UseSubmitBehavior="False" Text=" "
                                    EnableTheming="False" ToolTip="ذخیره" ID="BtnSave" EnableViewState="False" OnClick="BtnSave_Click">
                                    <Image Url="~/Images/icons/save.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>
    <br />
    <dx:ASPxRoundPanel ID="ASPxRoundPanel3" runat="server" Width="100%"
        GroupBoxCaptionOffsetY="-24px" HeaderText="تنظیمات سایت" RightToLeft="True">
        <ContentPaddings PaddingBottom="2px" PaddingLeft="2px" PaddingTop="2px" PaddingRight="2px" />
        <HeaderStyle Height="23px" HorizontalAlign="Right">
            <Paddings PaddingBottom="0px" PaddingLeft="2px" PaddingTop="0px" />
        </HeaderStyle>
        <PanelCollection>
            <dx:PanelContent ID="PanelContent3" runat="server">
                <table style="width: 100%" dir="rtl">
                    <tr>
                        <td width="20%" align="right">
                            <asp:Label ID="Label1" runat="server" Text="کد کاربری مدیریت سایت (Admin UserId)"></asp:Label>
                        </td>
                        <td width="40%" align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtAdminUserId">
                                <ValidationSettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td width="40%" align="right">
                            <asp:Label ID="Label2" runat="server" Text="با ',' جدا شوند" Font-Italic="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label3" runat="server" Text="حالت بروز رسانی سایت"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbWebsiteAccessible"
                                ValueType="System.String"
                                RightToLeft="True">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <Items>
                                    <dxe:ListEditItem Text="فعال" Value="1" />
                                    <dxe:ListEditItem Text="غیر فعال" Value="0" Selected="true" />
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label4" runat="server" Text="مدت زمان اعتبار Login Cookie"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtLoginCookieTimeOut">
                                <ValidationSettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label5" runat="server" Text="به دقیقه" Font-Italic="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label6" runat="server" Text="مدت زمان اعتبار Query"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtQueryTimeOut">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label7" runat="server" Text="به دقیقه" Font-Italic="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label8" runat="server" Text="مدت زمان اعتبار FileHandler"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtFileHandlerTimeOut">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label9" runat="server" Text="به دقیقه" Font-Italic="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label10" runat="server" Text="نمایش خطا(Exception) در پیغام ها"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbExceptionError"
                                ValueType="System.String"
                                RightToLeft="True">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <Items>
                                    <dxe:ListEditItem Text="فعال" Value="1" Selected="true" />
                                    <dxe:ListEditItem Text="غیر فعال" Value="0" />
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label11" runat="server" Text="حداکثر سایز فایل آپلودی"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtMaxSizeForUploadFile">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label12" runat="server" Text="به بایت" Font-Italic="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label13" runat="server" Text="نسخه سایت"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtSoftwareVersion">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label14" runat="server" Text="تاریخ ویرایش سایت"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtSoftwareEditedNo">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label15" runat="server" Text="آدرس سایت"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtNezamFarsWebsiteAddress" HorizontalAlign="Left" Style="direction: ltr">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label16" runat="server" Text="وضعیت دسترسی به جستجوی اعضا"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbMemberSearchAccessible"
                                ValueType="System.String"
                                RightToLeft="True">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <Items>
                                    <dxe:ListEditItem Text="فعال" Value="true" Selected="true" />
                                    <dxe:ListEditItem Text="غیر فعال" Value="false" />
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label33" runat="server" Text="چک شدن صفر بودن حساب اشخاص (InvoiceCheck)"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbIsZeroInvoiceCheck"
                                ValueType="System.String"
                                RightToLeft="True">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <Items>
                                    <dxe:ListEditItem Text="فعال" Value="true" Selected="true" />
                                    <dxe:ListEditItem Text="غیر فعال" Value="false" />
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label34" runat="server" Text="مهلت اقدام در اسناد اتوماسیون اداری"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtLetterActionDuration">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label35" runat="server" Text="مهلت انجام عملیات در گردش کار (WorkFlow)"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtWFExpireDateDuration">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label37" runat="server" Text="کشور پیش فرض"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbDefaultCountry"
                                DataSourceID="ObjectDataSourceCountry"
                                ValueType="System.String" RightToLeft="True"
                                ValueField="CounId" TextField="CounName">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label38" runat="server" Text="استان پیش فرض"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbDefaultProvince"
                                DataSourceID="ObjectDataSourceProvince"
                                ValueType="System.String" RightToLeft="True"
                                ValueField="PrId" TextField="PrName">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label36" runat="server" Text="شهر پیش فرض"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbDefaultCity"
                                DataSourceID="ObjectDataSourceCity" ValueType="System.String"
                                RightToLeft="True" ValueField="CitId"
                                TextField="CitName">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label39" runat="server" Text="کد پیش فرض نظام مهندسی استان"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtProvinceNezamCode">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label40" runat="server" Text="CheckMunPermission"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbCheckMunPermission"
                                ValueType="System.String"
                                RightToLeft="True">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <Items>
                                    <dxe:ListEditItem Text="فعال" Value="true" Selected="true" />
                                    <dxe:ListEditItem Text="غیر فعال" Value="false" />
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label41" runat="server" Text="ایجاد حساب مالی توسط سیستم"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbCreateAccount"
                                ValueType="System.String"
                                RightToLeft="True">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <Items>
                                    <dxe:ListEditItem Text="فعال" Value="true" Selected="true" />
                                    <dxe:ListEditItem Text="غیر فعال" Value="false" />
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label42" runat="server" Text="چک شدن Validation ها در واحد آموزش"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbIsAmoozeshConditionsChecked"
                                ValueType="System.String"
                                RightToLeft="True">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <Items>
                                    <dxe:ListEditItem Text="فعال" Value="true" Selected="true" />
                                    <dxe:ListEditItem Text="غیر فعال" Value="false" />
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label43" runat="server" Text="مدت پیش فرض انقضا تاریخ خبر"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtDefaultNewsExpireDate">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label44" runat="server" Text="به روز" Font-Italic="true"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td align="right">
                            <asp:Label ID="Label54" runat="server" Text="ConnectionString"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtConnectionString">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>
    <br />
    <dx:ASPxRoundPanel ID="ASPxRoundPanel7" runat="server" Width="100%"
        GroupBoxCaptionOffsetY="-24px" HeaderText="تنظیمات پیام کوتاه"
        RightToLeft="True">
        <ContentPaddings PaddingBottom="2px" PaddingLeft="2px" PaddingTop="2px" PaddingRight="2px" />
        <HeaderStyle Height="23px" HorizontalAlign="Right">
            <Paddings PaddingBottom="0px" PaddingLeft="2px" PaddingTop="0px" />
        </HeaderStyle>
        <PanelCollection>
            <dx:PanelContent ID="PanelContent7" runat="server">
                <table style="width: 100%" dir="rtl">
                    <tr>
                        <td align="right" width="20%">
                            <asp:Label ID="Label22" runat="server" Text="SMSPacketSize"></asp:Label>
                        </td>
                        <td align="right" width="40%">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtSMSPacketSize" HorizontalAlign="Left" Style="direction: ltr">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right" width="40%"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label45" runat="server" Text="تعداد SMSهای ارسالی برای Sleep Thread"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtNoOfSMSPacketSendBeforeThreadSleep" HorizontalAlign="Left" Style="direction: ltr">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label46" runat="server" Text="مدت زمان Sleep Thread"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtSMSThreadSleepTime" HorizontalAlign="Left" Style="direction: ltr">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label47" runat="server" Text="به میلی ثانیه" Font-Italic="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>
    <br />
    <dx:ASPxRoundPanel ID="ASPxRoundPanel4" runat="server" Width="100%"
        GroupBoxCaptionOffsetY="-24px" HeaderText="تنظیمات پیام کوتاه عصرفرا ارتباط"
        RightToLeft="True">
        <ContentPaddings PaddingBottom="2px" PaddingLeft="2px" PaddingTop="2px" PaddingRight="2px" />
        <HeaderStyle Height="23px" HorizontalAlign="Right">
            <Paddings PaddingBottom="0px" PaddingLeft="2px" PaddingTop="0px" />
        </HeaderStyle>
        <PanelCollection>
            <dx:PanelContent ID="PanelContent4" runat="server">
                <table style="width: 100%" dir="rtl">
                    <tr>
                        <td width="20%" align="right">
                            <asp:Label ID="Label17" runat="server" Text="WebService"></asp:Label>
                        </td>
                        <td width="40%" align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtWebService" HorizontalAlign="Left" Style="direction: ltr">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td width="40%" align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label18" runat="server" Text="BoxService"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtBoxService" HorizontalAlign="Left" Style="direction: ltr">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label19" runat="server" Text="SMSUsername"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtSMSUsername" HorizontalAlign="Left" Style="direction: ltr" AutoCompleteType="Disabled">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label20" runat="server" Text="SMSPassword"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtSMSPassword" HorizontalAlign="Left" Style="direction: ltr" AutoCompleteType="Disabled">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label21" runat="server" Text="SMSNumber"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtSMSNumber" HorizontalAlign="Left" Style="direction: ltr">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>
    <br />
    <dx:ASPxRoundPanel ID="ASPxRoundPanel6" runat="server" Width="100%"
        GroupBoxCaptionOffsetY="-24px" HeaderText="تنظیمات پیام کوتاه مگفا"
        RightToLeft="True">
        <ContentPaddings PaddingBottom="2px" PaddingLeft="2px" PaddingTop="2px" PaddingRight="2px" />
        <HeaderStyle Height="23px" HorizontalAlign="Right">
            <Paddings PaddingBottom="0px" PaddingLeft="2px" PaddingTop="0px" />
        </HeaderStyle>
        <PanelCollection>
            <dx:PanelContent ID="PanelContent6" runat="server">
                <table style="width: 100%" dir="rtl">
                    <tr>
                        <td width="20%" align="right">
                            <asp:Label ID="Label48" runat="server" Text="WebServiceURL"></asp:Label>
                        </td>
                        <td width="40%" align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtMagfaWebServiceURL" HorizontalAlign="Left" Style="direction: ltr">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td width="40%" align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label50" runat="server" Text="SMSUsername"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtMagfaSMSUserName" HorizontalAlign="Left" Style="direction: ltr" AutoCompleteType="Disabled">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label51" runat="server" Text="SMSPassword"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtMagfaSMSPassword" HorizontalAlign="Left" Style="direction: ltr" AutoCompleteType="Disabled">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label52" runat="server" Text="SMSNumber"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtMagfaSMSNumber" HorizontalAlign="Left" Style="direction: ltr">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label53" runat="server" Text="Domain"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtMagfaSMSDomain" HorizontalAlign="Left" Style="direction: ltr">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>
    <br />
    <dx:ASPxRoundPanel ID="ASPxRoundPanel5" runat="server" Width="100%"
        GroupBoxCaptionOffsetY="-24px" HeaderText="تنظیمات Email"
        RightToLeft="True">
        <ContentPaddings PaddingBottom="2px" PaddingLeft="2px" PaddingTop="2px" PaddingRight="2px" />
        <HeaderStyle Height="23px" HorizontalAlign="Right">
            <Paddings PaddingBottom="0px" PaddingLeft="2px" PaddingTop="0px" />
        </HeaderStyle>
        <PanelCollection>
            <dx:PanelContent ID="PanelContent5" runat="server">
                <table style="width: 100%" dir="rtl">
                    <tr>
                        <td width="20%" align="right">
                            <asp:Label ID="Label23" runat="server" Text="Email Address"></asp:Label>
                        </td>
                        <td width="40%" align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtEmail" HorizontalAlign="Left" Style="direction: ltr">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td width="40%" align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label24" runat="server" Text="Email Password"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtEmailPass" HorizontalAlign="Left" Style="direction: ltr" AutoCompleteType="Disabled">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label25" runat="server" Text="نام نمایشی در Email"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtEmailDisplayName">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label26" runat="server" Text="SMPT Name"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtSMPTName" HorizontalAlign="Left" Style="direction: ltr">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label27" runat="server" Text="Use Default Credentials"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbSMTPUseDefaultCredentials"
                                ValueType="System.String"
                                RightToLeft="True">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <Items>
                                    <dxe:ListEditItem Text="True" Value="1" Selected="true" />
                                    <dxe:ListEditItem Text="False" Value="0" />
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label28" runat="server" Text="SMTP SSL"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbSMPTEnableSsl"
                                ValueType="System.String"
                                RightToLeft="True">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <Items>
                                    <dxe:ListEditItem Text="فعال" Value="1" Selected="true" />
                                    <dxe:ListEditItem Text="غیر فعال" Value="0" />
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label29" runat="server" Text="SMPT Port"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtSMPTPort">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label31" runat="server" Text="برای حالت پیش فرض، -1 قرار داده شود"
                                Font-Italic="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label30" runat="server" Text="مدت اعتبار لینک ارسالی از طریق Email"></asp:Label>
                        </td>
                        <td align="right">
                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                ID="txtEmailLinkTimeOut">
                                <validationsettings>
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </validationsettings>
                            </TSPControls:CustomTextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label32" runat="server" Text="به دقیقه، برای حالت مدت اعتبار نامحدود، 0 قرار داده شود"
                                Font-Italic="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>
    <br />
    <dx:ASPxRoundPanel ID="ASPxRoundPanel8" runat="server" Width="100%"
        GroupBoxCaptionOffsetY="-24px" HeaderText="تنظیمات نمایش پیغام"
        RightToLeft="True">
        <ContentPaddings PaddingBottom="2px" PaddingLeft="2px" PaddingTop="2px" PaddingRight="2px" />
        <HeaderStyle Height="23px" HorizontalAlign="Right">
            <Paddings PaddingBottom="0px" PaddingLeft="2px" PaddingTop="0px" />
        </HeaderStyle>
        <PanelCollection>
            <dx:PanelContent ID="PanelContent8" runat="server">
                <table style="width: 100%" dir="rtl">

                    <tr>
                        <td align="right" width="20%">
                            <asp:Label ID="Label57" runat="server" Text="نمایش پیغام"></asp:Label>
                        </td>
                        <td align="right" width="40%">
                            <TSPControls:CustomAspxComboBox runat="server" Width="100%" ID="cmbShowMessage"
                                ValueType="System.String"
                                RightToLeft="True">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                    </ErrorImage>
                                    <ErrorFrameStyle ImageSpacing="4px">
                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                    </ErrorFrameStyle>
                                </ValidationSettings>
                                <Items>
                                    <dxe:ListEditItem Text="True" Value="1" Selected="true" />
                                    <dxe:ListEditItem Text="False" Value="0" />
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </td>
                        <td align="right" width="40%"></td>
                    </tr>
                    <tr>
                        <td align="right" width="20%">
                            <asp:Label ID="Label49" runat="server" Text="متن پیغام"></asp:Label>
                        </td>
                        <td align="right" width="40%">
                            <TSPControls:CustomASPXMemo ID="txtShowmsg" runat="server" Height="71px" Width="100%">
                            </TSPControls:CustomASPXMemo>
                        </td>
                        <td align="right" width="40%"></td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>
    <br />
    <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" Width="100%"
        GroupBoxCaptionOffsetY="-24px" HeaderText="" RightToLeft="True"
        ShowHeader="False">
        <ContentPaddings PaddingBottom="2px" PaddingLeft="2px" PaddingTop="2px" PaddingRight="2px" />
        <HeaderStyle Height="23px">
            <Paddings PaddingBottom="0px" PaddingLeft="2px" PaddingTop="0px" />
        </HeaderStyle>
        <PanelCollection>
            <dx:PanelContent ID="PanelContent2" runat="server">
                <div style="width: 100%" dir="rtl">
                    <table>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true"  runat="server" UseSubmitBehavior="False" Text=" "
                                    EnableTheming="False" ToolTip="ذخیره" ID="BtnSave2" EnableViewState="False" OnClick="BtnSave_Click">
                                    <Image Url="~/Images/icons/save.png">
                                    </Image>
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                    </HoverStyle>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>
    <asp:ObjectDataSource ID="ObjectDataSourceProvince" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.ProvinceManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceCountry" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.CountryManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceCity" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.CityManager"></asp:ObjectDataSource>
</asp:Content>
