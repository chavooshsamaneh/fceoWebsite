<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="EngOfficeRegister.aspx.cs" Inherits="Members_EngOffice_EngOfficeRegister"
    Title="مشخصات دفتر" %>

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
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="Content" runat="server" style="width: 100%; display: block;" align="center">
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                width="100%">
                                <tbody>
                                    <tr>
                                        <td align="right">
                                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 27px; height: 27px;">
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server"  EnableTheming="False"
                                                                ToolTip="ذخیره" ID="btnSave" EnableViewState="False" OnClick="btnSave_Click"
                                                                Text=" " UseSubmitBehavior="False">
                                                                <Image  Url="~/Images/icons/save.png">
                                                                </Image>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                </HoverStyle>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td style="width: 27px; height: 27px;">
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server"  EnableTheming="False"
                                                                ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click"
                                                                CausesValidation="False" Text=" " UseSubmitBehavior="False">
                                                                <Image  Url="~/Images/icons/Back.png">
                                                                </Image>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                </HoverStyle>
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
                <div align="right">
                    <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server" 
                         SeparatorWidth="1px" SeparatorHeight="100%" SeparatorColor="#A5A6A8"
                        OnItemClick="ASPxMenu1_ItemClick" ItemSpacing="0px" 
                        AutoSeparators="RootOnly" RightToLeft="True">
                        <Items>
                            <dxm:MenuItem Name="EngOffice" Text="مشخصات دفتر" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Member" Text="اعضای دفتر">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Job" Text="سوابق کاری">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Attach" Text="مستندات">
                            </dxm:MenuItem>
                        </Items>
                    </TSPControls:CustomAspxMenuHorizontal>
                </div>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" ClientInstanceName="RoundPanel1"
                    HeaderText="" runat="server" Width="100%">
                    <PanelCollection>
                        <dx:PanelContent>
                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td align="right" valign="top" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="نام دفتر" ID="ASPxLabel5" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="3" align="right" valign="top" style="width: 85%">
                                            <TSPControls:CustomTextBox  runat="server" Width="100%" ID="txtEngOffName" 
                                                >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                    </ErrorFrameStyle>
                                                    <RequiredField ErrorText="نام دفتر را وارد نمایید" IsRequired="True" />
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="شماره  مشارکت نامه" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <TSPControls:CustomTextBox  runat="server"  Style="direction: ltr" Width="100%"
                                                ID="txtLetterNo" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="شماره نامه را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ مشارکت نامه" ID="ASPxLabel4">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="230px" ShowPickerOnTop="True"
                                                ID="txtLetterDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                            <asp:RequiredFieldValidator runat="server" ErrorMessage="تاریخ تولد" ToolTip="تاریخ نامه را وارد نمایید"
                                                ControlToValidate="txtLetterDate" ID="RequiredFieldValidator8" Display="Dynamic">تاریخ نامه را وارد نمایید</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نوع دفتر*" ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server"  TextField="Name"
                                                ID="ComboEOfTId"  DataSourceID="OdbEOffType" ValueType="System.String"
                                                ValueField="EOfTId" RightToLeft="True" 
                                                Width="100%">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نوع دفتر را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره دفتر اسناد رسمی" ID="ASPxLabel7">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox  runat="server"  Width="100%" MaxLength="20" ID="txtDaftarNo"
                                                >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="شماره دفتر را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="محل دفتر اسناد رسمی" ID="ASPxLabel9">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px"  Width="100%" ID="txtDaftarLoc"
                                                >
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تلفن*" ID="ASPxLabel10">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox  runat="server"  MaxLength="12" Width="100%" ID="txtTel"
                                                >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="تلفن را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="شماره تلفن را صحیح وارد نمایید" ValidationExpression="\d*">
                                                    </RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="فکس" ID="ASPxLabel11">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox  runat="server" MaxLength="12"  Width="100%" ID="txtFax"
                                                >
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RegularExpression ErrorText="شماره فکس را صحیح وارد نمایید" ValidationExpression="\d*">
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
                                            <asp:Label runat="server" Text="شماره همراه *" ID="Label30" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox  runat="server" ID="txtMobileNo"  Width="100%"
                                                MaxLength="11" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="شماره همراه را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="شماره همراه را صحیح وارد نمایید" ValidationExpression="0\d{1,10}">
                                                    </RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="آدرس*" ID="ASPxLabel3">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px"  Width="100%" ID="txtAddress"
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
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel6">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px"  Width="100%" ID="txtDesc"
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
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره پروانه" Enabled="False" ID="lblFileNo">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox  runat="server"  Width="100%" Enabled="False" ID="txtFileNo"
                                                Style="direction: ltr" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                        </td>
                                        <td valign="top" align="right">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره نامه" ID="ASPxLabeldno" Visible="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox  runat="server"  Width="100%" ClientEnabled="False"
                                                ID="txtFileLetterNo"  Visible="False">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ نامه" ID="ASPxLabelddate" ClientInstanceName="ASPxLabel8"
                                                Visible="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="230px" Enabled="False"
                                                Visible="False" ShowPickerOnTop="True" ID="txtFileLetterDate" PickerDirection="ToRight"
                                                IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="موقت/دائم" ID="ASPxLabeld1" Visible="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" Visible="False" Width="100%" Enabled="False" 
                                                ID="cmbdIsTemporary"  RightToLeft="True" ValueType="System.String"
                                                >
                                                <ItemStyle HorizontalAlign="Right" />
                                                <Items>
                                                    <dxe:ListEditItem Value="0" Text="پروانه دائم"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Value="1" Text="پروانه موقت"></dxe:ListEditItem>
                                                </Items>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره سریال" ID="ASPxLabeld2" Visible="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox  runat="server"  Width="100%" Enabled="False" ID="txtdSerialNo"
                                                 Visible="False">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ آخرین تمدید" ID="ASPxLabeld3" ClientInstanceName="lblDate"
                                                Visible="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="230px" Enabled="False"
                                                Visible="False" ShowPickerOnTop="True" ID="txtdLastRegDate" PickerDirection="ToRight"
                                                IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ پایان اعتبار" ID="ASPxLabeld4" ClientInstanceName="lblDate"
                                                Visible="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="230px" Enabled="False"
                                                Visible="False" ShowPickerOnTop="True" ID="txtdExpDate" PickerDirection="ToRight"
                                                IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%" Visible="false">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="شماره فیش" Width="65px" ID="ASPxLabel8" ClientInstanceName="ASPxLabel6">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox  runat="server"  Width="170px" ID="ASPxTextBoxFicheCode"
                                                ClientInstanceName="txtFicheCode" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="شماره فیش را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="مبلغ" Enabled="False" ID="ASPxLabel12" ClientInstanceName="ASPxLabel8">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server"  Width="170px" ClientEnabled="False"
                                                ID="ASPxTextBoxAmount" ClientInstanceName="txtAmount" >
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                <asp:HiddenField ID="EngOfficeId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="EngFileId" runat="server" Visible="False"></asp:HiddenField>
                <asp:ObjectDataSource ID="OdbEOffType" runat="server" TypeName="TSP.DataManager.EngOfficeTypeManager"
                    SelectMethod="GetData" FilterExpression="EOfTId={0}">
                    <FilterParameters>
                        <asp:Parameter Name="newparameter" />
                    </FilterParameters>
                </asp:ObjectDataSource>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                width="100%">
                                <tbody>
                                    <tr>
                                        <td align="right">
                                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 27px; height: 27px;">
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server"  EnableTheming="False"
                                                                ToolTip="ذخیره" ID="btnSave2" EnableViewState="False" OnClick="btnSave_Click"
                                                                Text=" " UseSubmitBehavior="False">
                                                                <Image  Url="~/Images/icons/save.png">
                                                                </Image>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                </HoverStyle>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td style="width: 27px; height: 27px;">
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server"  EnableTheming="False"
                                                                ToolTip="بازگشت" ID="ASPxButton6" EnableViewState="False" OnClick="btnBack_Click"
                                                                CausesValidation="False" Text=" " UseSubmitBehavior="False">
                                                                <Image  Url="~/Images/icons/Back.png">
                                                                </Image>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                                </HoverStyle>
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
            DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="..\..\Image\indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
    </div>
</asp:Content>
