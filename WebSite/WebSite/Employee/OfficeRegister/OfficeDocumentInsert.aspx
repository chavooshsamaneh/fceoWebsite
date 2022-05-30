
<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeDocumentInsert.aspx.cs" Inherits="Employee_OfficeRegister_OfficeDocumentInsert"
    Title="مشخصات پروانه عضو حقوقی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%" >
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td align="right">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ButtonType="New" runat="server" Text=" "  ToolTip="جدید"
                                                                CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="BtnNew_Click">
                                                             
                                                                <Image  Url="~/Images/icons/new.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton ButtonType="Edit" IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                                                EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                              
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton ButtonType="Save" IsMenuButton="true"  runat="server" Text=" "  ToolTip="ذخیره"
                                                                ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                OnClick="btnSave_Click">
                                                                <ClientSideEvents Click="function(s, e) {
if(CheckCharacterEncoding(txt1.GetText())==false)
    {
     txt1.SetIsValid(false);
     txt1.SetErrorText('حروف وارد شده نامعتبر است');
	 e.processOnServer=false;
    }
//if(flpArm2.Get('name')!=1)
//{
//lblArm.SetVisible(true);
//e.processOnServer=false;
//}

//if(flpSign2.Get('name')!=1)
//{
//lblSign.SetVisible(true);
//e.processOnServer=false;
//}
}"></ClientSideEvents>
                                                               
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td width="10px" align="center">
                                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton ButtonType="PrintGrid" IsMenuButton="true" ID="btnPrint" runat="server" CausesValidation="False" 
                                                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False"
                                                                AutoPostBack="False">
                                                               
                                                                <ClientSideEvents Click="function(s, e) {
	Callback.PerformCallback('Print');
}" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td width="10px" align="center">
                                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6"></TSPControls:MenuSeprator>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton  IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="btnBack_Click">
                                                               
                                                                <Image  Url="~/Images/icons/Back.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <dx:ASPxCallback ID="Callback" runat="server" ClientInstanceName="Callback" OnCallback="Callback_Callback">
                                                <ClientSideEvents EndCallback="function(s, e) {
	if(s.cpPrint==1 &amp;&amp; s.cpURL!='')
{
window.open(s.cpURL);
}
}" />
                                            </dx:ASPxCallback>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
        
                    <TSPControls:CustomAspxMenuHorizontal ID="MenuDetails" runat="server" 
                         SeparatorHeight="100%"   ItemImagePosition="right"
                        OnItemClick="MenuDetails_ItemClick"  > 
                        <Items>
                            <dxm:MenuItem Name="Office" Text="مشخصات شرکت" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Member" Text="اعضا">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Agent" Text="شعبه ها">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Job" Text="سوابق کاری">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Letters" Text="روزنامه های رسمی">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Financial" Text="وضعیت مالی">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Attach" Text="مستندات">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Group" Text="گروه ها">
                            </dxm:MenuItem>
                        </Items>
                    </TSPControls:CustomAspxMenuHorizontal>
        
                <div align="right">
                    <ul class="HelpUL">
                        <li>در صورتی که وضعیت عضویت شرکت در حالت ''تایید مشروط'' باشد ، تنها اعضای غیرفعال شده
                            شرکت قادر به عضویت در سایر شرکت و یا دفاتر در سازمان می باشند و فعالیت سایر اعضای
                            شرکت تا مشخص شدن وضعیت شرکت و تایید مجدد آن منع می گردد </li>
                    </ul>
                </div>
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelOffice" HeaderText="مشاهده" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>                           
                            <fieldset>
                                <legend class="fieldset-legend">مشخصات عضو حقوقی
                                </legend>


                                <table style="text-align: right" dir="rtl" width="100%">
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="center" colspan="4">
                                                <dxe:ASPxLabel runat="server" Text="وضعیت درخواست:" Font-Bold="False" ID="lblWorkFlowState"
                                                    ForeColor="Red">
                                                </dxe:ASPxLabel>
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">کد عضویت*
                                            </td>
                                            <td align="right" valign="top" width="30%">
                                                <TSPControls:CustomTextBox runat="server" ID="txtOfId"  Width="100%" 
                                                    Enabled="false" AutoPostBack="true" OnTextChanged="txtOfId_TextChanged">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="فرمت کد وارد شده صحیح نمی باشد" ValidationExpression="\d{1,10}"></RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td align="right" valign="top" width="20%">شماره عضویت
                                            </td>
                                            <td align="right" valign="top" width="30%">
                                                <TSPControls:CustomTextBox runat="server" ID="txtMeNo"  Width="100%" 
                                                    Enabled="false">
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">نام شرکت
                                            </td>
                                            <td colspan="3" align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server" ID="txtOfName"  Width="100%" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                       
                                                        <RequiredField IsRequired="True" ErrorText="نام شرکت را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">نام شرکت(انگلیسی)
                                            </td>
                                            <td colspan="3" align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server" ID="txtOfNameEn"  Width="100%"
                                                    ClientInstanceName="txt1" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="false" ErrorText="نام شرکت را وارد نمایید"></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">موضوع شرکت
                                            </td>
                                            <td colspan="3" align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server" ID="txtOfSubject"  Width="100%"
                                                    >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="موضوع شرکت را وارد نمایید"></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">
                                                <dxe:ASPxLabel runat="server" Text="زمینه موضوعی شرکت*" ID="lblMembershipRequstType"
                                                    ClientInstanceName="lblMembershipRequstType">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" width="30%">
                                                <TSPControls:CustomAspxComboBox runat="server" Width="100%" 
                                                    ID="cmbMembershipRequstType" ClientInstanceName="cmbMembershipRequstType" 
                                                    ValueType="System.String"  AutoPostBack="false"
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
                                                      
                                                        <RequiredField IsRequired="True" ErrorText="زمینه موضوعی شرکت را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" style="width: 20%">نوع شرکت
                                            </td>
                                            <td dir="ltr" align="right" valign="top" width="30%">
                                                <TSPControls:CustomAspxComboBox runat="server" 
                                                     TextField="OtName" ID="drdOfType" DataSourceID="OdbOfType"
                                                    ValueType="System.String" ValueField="OtId" 
                                                    EnableIncrementalFiltering="True" RightToLeft="True" Width="100%">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                     
                                                        <RequiredField IsRequired="True" ErrorText="نوع شرکت را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td align="right" valign="top" width="20%">
                                                <asp:Label runat="server" Text="شماره ثبت شرکت" ID="Label62"></asp:Label>
                                            </td>
                                            <td align="right" valign="top" width="30%">
                                                <TSPControls:CustomTextBox runat="server" ID="txtOfRegNo"  Width="100%" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="شماره ثبت را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,10}"></RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">محل ثبت شرکت
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server" ID="txtOfRegPlace"  Width="100%"
                                                    >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="محل ثبت شرکت را وارد نمایید"></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td align="right" valign="top">تاریخ ثبت شرکت
                                            </td>
                                            <td align="right" valign="top">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                    Width="230px" ShowPickerOnTop="True" ID="txtOfRegDate" PickerDirection="ToRight"
                                                    IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                    ErrorMessage="تاریخ را انتخاب نمایید" ControlToValidate="txtOfRegDate" ID="PersianDateValidator2">تاریخ نامعتبر</pdc:PersianDateValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">سرمایه شرکت(ریال)
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server" ID="txtOfValue"  Width="100%" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="سرمایه شرکت را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,11}"></RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td align="right" valign="top">تعداد سهام شرکت
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server" ID="txtOfStock"  Width="100%" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="تعداد سهام شرکت را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,11}"></RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label runat="server" Text="نوع فعالیت" ID="Label68" Visible="False"></asp:Label>
                                            </td>
                                            <td dir="ltr" align="right" valign="top">
                                                <TSPControls:CustomAspxComboBox runat="server" Visible="False" 
                                                     TextField="OatName" ID="aspxcmbAttype"
                                                    DataSourceID="OdbOfAtType" ValueType="System.String" ValueField="OatId" 
                                                     EnableIncrementalFiltering="True"
                                                    Width="100%">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        
                                                        <RequiredField IsRequired="True" ErrorText="نوع فعالیت را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td align="right" valign="top">
                                                <asp:Label runat="server" Text="نوع تأئید" ID="lblTaeed" Visible="False"></asp:Label>
                                            </td>
                                            <td dir="ltr" align="right" valign="top">
                                                <TSPControls:CustomAspxComboBox runat="server" Visible="False" 
                                                     TextField="MrsName" ID="drdMrsId" DataSourceID="ODBMrsId"
                                                    ValueType="System.String" ValueField="MrsId" 
                                                    EnableIncrementalFiltering="True" Width="100%">
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">آدرس شرکت
                                            </td>
                                            <td colspan="3" align="right" valign="top">
                                                <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtOfAddress"  Width="100%"
                                                    >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="آدرس شرکت را وارد نمایید"></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label runat="server" Text="تلفن 1" ID="Labe69"></asp:Label>
                                            </td>
                                            <td align="right" valign="top">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="right" valign="top" width="70%">
                                                            <TSPControls:CustomTextBox runat="server" ID="txtOfTel1"  Width="100%" MaxLength="8"
                                                                >
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <RequiredField IsRequired="True" ErrorText="شماره تلفن را وارد نمایید"></RequiredField>
                                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{5,8}"></RegularExpression>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                        <td align="right" valign="top" width="10%">
                                                            <asp:Label runat="server" Text="-" Width="13px" ID="Labe71"></asp:Label>
                                                        </td>
                                                        <td align="right" valign="top" width="20%">
                                                            <TSPControls:CustomTextBox runat="server" ID="txtOfTel1_pre"  Width="100%"
                                                                MaxLength="4" >
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <RequiredField ErrorText="پیش شماره تلفن را وارد نمایید"></RequiredField>
                                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}"></RegularExpression>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right" valign="top">
                                                <asp:Label runat="server" Text="تلفن 2" ID="Label70"></asp:Label>
                                            </td>
                                            <td align="right" valign="top">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="right" valign="top" width="70%">
                                                            <TSPControls:CustomTextBox runat="server" ID="txtOfTel2"  Width="100%" MaxLength="8"
                                                                >
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{5,8}"></RegularExpression>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                        <td align="right" valign="top" width="10%">
                                                            <asp:Label runat="server" Text="-" Width="13px" ID="Label72"></asp:Label>
                                                        </td>
                                                        <td align="right" valign="top" width="20%">
                                                            <TSPControls:CustomTextBox runat="server" ID="txtOfTel2_pre"  Width="100%"
                                                                MaxLength="4" >
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}"></RegularExpression>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label runat="server" Text="فکس" ID="Labe73"></asp:Label>
                                            </td>
                                            <td align="right" valign="top">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="right" valign="top" width="70%">
                                                            <TSPControls:CustomTextBox runat="server" ID="txtOfFax"  Width="100%" MaxLength="8"
                                                                >
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{5,8}"></RegularExpression>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                        <td align="right" valign="top" width="10%">
                                                            <asp:Label runat="server" Text="-" Width="13px" ID="Labe74"></asp:Label>
                                                        </td>
                                                        <td align="right" valign="top" width="20%">
                                                            <TSPControls:CustomTextBox runat="server" ID="txtOfFax_pre"  Width="100%"
                                                                MaxLength="4" >
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}"></RegularExpression>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right" valign="top">
                                                <asp:Label runat="server" Text="شماره همراه مدیر عامل" ID="Label75"></asp:Label>
                                            </td>
                                            <td align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server" ID="txtOfMobile"  Width="100%"
                                                    MaxLength="11" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="شماره همراه را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{1,10}"></RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label runat="server" Text="آدرس وب سایت" ID="Labe77"></asp:Label>
                                            </td>
                                            <td colspan="3" align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server" ID="txtOfWebsite"  Width="300px"
                                                    >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="آدرس وب سایت را با فرمت http://www.Example.com وارد نمایید"
                                                            ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label runat="server" Text="آدرس پست الکترونیکی" ID="Labe82"></asp:Label>
                                            </td>
                                            <td colspan="3" align="right" valign="top">
                                                <TSPControls:CustomTextBox runat="server" ID="txtOfEmail"  Width="300px"
                                                    >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText="آدرس پست الکترونیکی را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText=" آدرس پست الکترونیکی را با فرمت صحیح وارد نمایید" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label runat="server" Text="تصویر آرم شرکت" ID="Label79"></asp:Label>
                                            </td>
                                            <td colspan="3" align="right" valign="top">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                    ID="flpOfArm" InputType="Images" ClientInstanceName="flpArm" OnFileUploadComplete="flpOfArm_FileUploadComplete">
                                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
                                                            if(e.isValid){
	imgArm.SetVisible(true);
	flpArm2.Set('name',1);
	ImageArm.SetVisible(true);
	ImageArm.SetImageUrl('../../Image/Temp/'+e.callbackData);
    }
    else
    {
    imgArm.SetVisible(false);
	flpArm2.Set('name',0);
	ImageArm.SetVisible(false);
	ImageArm.SetImageUrl('');
    }
}"></ClientSideEvents>
                                                                </TSPControls:CustomAspxUploadControl>
                                                                <dxe:ASPxLabel runat="server" Text="تصویر آرم شرکت را انتخاب نمایید" ClientVisible="False"
                                                                    ID="ASPxLabel1" ForeColor="Red" ClientInstanceName="lblArm">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                    ID="imgEndUploadImg" ClientInstanceName="imgArm">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <dxe:ASPxImage runat="server" Height="75px" ClientVisible="False" Width="75px" ID="imgOfArm"
                                                    ClientInstanceName="ImageArm">
                                                    <Border BorderWidth="1px" BorderStyle="Solid"></Border>
                                                </dxe:ASPxImage>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label runat="server" Text="تصویر مهر شرکت" ID="Label80"></asp:Label>
                                            </td>
                                            <td colspan="3" align="right" valign="top">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                    ID="flpOfSign" InputType="Images" ClientInstanceName="flpSign" OnFileUploadComplete="flpOfSign_FileUploadComplete">
                                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
                                                            if(e.isValid){
	imgSign.SetVisible(true);
	flpSign2.Set('name',1);
	ImageSign.SetVisible(true);
	ImageSign.SetImageUrl('../../Image/Temp/'+e.callbackData);
    }
    else
    {
    imgSign.SetVisible(false);
	flpSign2.Set('name',0);
	ImageSign.SetVisible(false);
	ImageSign.SetImageUrl('');
    }
}"></ClientSideEvents>
                                                                </TSPControls:CustomAspxUploadControl>
                                                                <dxe:ASPxLabel runat="server" Text="تصویر مهر شرکت را انتخاب نمایید" ClientVisible="False"
                                                                    ID="ASPxLabel2" ForeColor="Red" ClientInstanceName="lblSign">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                    ID="ASPxImage1" ClientInstanceName="imgSign">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <dxe:ASPxImage runat="server" Height="75px" ClientVisible="False" Width="75px" ID="imgOfSign"
                                                    ClientInstanceName="ImageSign">
                                                    <Border BorderWidth="1px" BorderStyle="Solid"></Border>
                                                </dxe:ASPxImage>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label runat="server" Text="توضیحات شرکت" ID="Label81"></asp:Label>
                                            </td>
                                            <td colspan="3" align="right" valign="top">
                                                <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtOfDesc"  Width="100%"
                                                    >
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right" style="width: 15%">
                                                <asp:Label runat="server" Text="شرح درخواست" Width="100%" ID="Label19"></asp:Label>
                                            </td>
                                            <td colspan="3" align="right" valign="top">
                                                <TSPControls:CustomASPXMemo runat="server" Height="40px" ID="txtReRequestDesc" 
                                                    Width="100%" >
                                                    <ClientSideEvents KeyPress="function(s,e){ CheckDevExpressTextboxLengthOnKeyPress(s,e,255); }" />
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="کد پیگیری" Width="100%" ID="ASPxLabel3">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td dir="rtl" valign="top" align="right" style="width: 30%">
                                                <TSPControls:CustomTextBox runat="server" Text="0000000000" ID="txtFollowCode" 
                                                    Width="100%" Enabled="False" ReadOnly="True" >
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">
                                                <dxe:ASPxLabel ClientVisible="false" runat="server" Text="نوع فعالیت" ID="lblActivityType"
                                                    ClientInstanceName="lblActivityType">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" valign="top" width="30%">
                                                <TSPControls:CustomAspxComboBox runat="server" Width="100%" 
                                                    ID="cmbActivityType" ClientInstanceName="cmbActivityType" 
                                                    ValueType="System.String"  AutoPostBack="false"
                                                    RightToLeft="True" ClientVisible="false">
                                                    <Items>
                                                        <dxe:ListEditItem Value="0" Text="پیمان مدیریت"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Value="1" Text="پیمانکاری یا پیمان مدیریت"></dxe:ListEditItem>
                                                    </Items>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        
                                                        <RequiredField IsRequired="True" ErrorText="نوع فعالیت را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td align="right" valign="top" width="20%"></td>
                                            <td align="right" valign="top" width="30%"></td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" colspan="4">
                                                <TSPControls:CustomASPxCheckBox ForeColor="DarkViolet" Font-Bold="true" ID="CheckBoxConditionalApprove"
                                                     Width="100%" 
                                                    RightToLeft="True" Text="با آگاهی کامل از قوانین سازمان نظام مهندسی و مبحث دوم قصد تایید مشروط این عضویت حقوقی را دارا می باشم و هرگونه عواقب آن را به عهده می گیرم"
                                                    runat="server">
                                                </TSPControls:CustomASPxCheckBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>
                            <br />
                            <fieldset id="RoundPanelDocumentBasicInfo" runat="server">
                                <legend class="fieldset-legend">مشخصات پروانه
                                </legend>

                                <TSPControls:CustomAspxCallbackPanel runat="server"
                                    ClientInstanceName="CallbackPanelDoRegDate" Width="100%" ID="CallbackPanelDoRegDate"
                                    OnCallback="CallbackPanelDoRegDate_Callback">
                                    <SettingsLoadingPanel Text="در حال بارگذاری" />
                                    <PanelCollection>
                                        <dxp:PanelContent ID="PanelContent11" runat="server">
                                            <table id="Table3" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 100%" colspan="4" align="center">
                                                            <dxe:ASPxLabel runat="server" Width="100%" Font-Bold="true" Text="نکات تاریخ صدور"
                                                                ID="lblRegDateComment" ForeColor="DarkRed" Visible="false">
                                                            </dxe:ASPxLabel>
                                                            <br />
                                                        </td>
                                                        <br />
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 15%" valign="top" align="right">
                                                            <dxe:ASPxLabel Width="100%" runat="server" Text="نوع پروانه *" ID="ASPxLabel5">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td style="width: 35%" valign="top" align="right">
                                                            <TSPControls:CustomAspxComboBox runat="server"  Width="100%" 
                                                                 ID="ComboDocType" ValueType="System.String"
                                                                 EnableIncrementalFiltering="True"
                                                                IncrementalFilteringMode="StartsWith" RightToLeft="True">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                  
                                                                    <RequiredField IsRequired="True" ErrorText="نوع پروانه را انتخاب نمایید"></RequiredField>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                                <Items>
                                                                    <dxe:ListEditItem Value="1" Text="طراح و ناظر"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Value="2" Text="مجری"></dxe:ListEditItem>
                                                                </Items>
                                                                <ButtonStyle Width="13px">
                                                                </ButtonStyle>
                                                                <ClientSideEvents SelectedIndexChanged="
                                                                        function(s,e){                                                                        
                                                                            if(s.GetValue()==1)
                                                                            {
                                                                                lblActivityType.SetVisible(false);
                                                                                cmbActivityType.SetVisible(false);
                                                                                cmbActivityType.SetSelectedIndex(-1);
                                                                                ComboBoxGrade.SetVisible(false);
                                                                                lblGrade.SetVisible(false);
                                                                            }
                                                                            else 
                                                                            {                                                                                
                                                                                lblActivityType.SetVisible(true);
                                                                                cmbActivityType.SetVisible(true);
                                                                                cmbActivityType.SetSelectedIndex(-1);
                                                                                ComboBoxGrade.SetVisible(true);
                                                                                lblGrade.SetVisible(true);
                                                                            }
                                                                        }
                                                                        " />
                                                            </TSPControls:CustomAspxComboBox>
                                                        </td>
                                                        <td style="width: 15%" valign="top" align="right">
                                                            <dxe:ASPxLabel runat="server" Text="شماره پروانه" Width="100%" ID="ASPxLabel8">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td style="width: 35%" valign="top" align="left">
                                                            <TSPControls:CustomTextBox runat="server" ID="txtFileNo"  Width="100%" Enabled="False"
                                                                 Style="direction: ltr">
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 15%" style="vertical-align: top" align="right">
                                                            <dxe:ASPxLabel Width="100%" runat="server" Text="موقت/دائم" ID="ASPxLabel22">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td style="width: 35%" style="vertical-align: top" dir="rtl" align="right">
                                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%" 
                                                                ID="cmbdIsTemporary" ClientInstanceName="cmbIsTemporary"  ValueType="System.String"
                                                                 AutoPostBack="false" RightToLeft="True">
                                                                <ClientSideEvents SelectedIndexChanged="function(s,e){CallbackPanelDoRegDate.PerformCallback(cmbIsTemporary.GetValue());}" />
                                                                <Items>
                                                                    <dxe:ListEditItem Value="0" Text="پروانه دائم"></dxe:ListEditItem>
                                                                    <dxe:ListEditItem Value="1" Text="پروانه موقت"></dxe:ListEditItem>
                                                                </Items>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ValidationSettings>
                                                                   
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                                <ButtonStyle Width="13px">
                                                                </ButtonStyle>
                                                            </TSPControls:CustomAspxComboBox>
                                                        </td>
                                                        <td style="width: 15%" valign="top" align="right">
                                                            <dxe:ASPxLabel Width="100%" runat="server" Text="شماره سریال گواهینامه" ID="ASPxLabel4">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td style="width: 35%" valign="top" align="right">
                                                            <TSPControls:CustomTextBox runat="server" Width="100%"  
                                                                ID="txtdSerialNo">
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                   
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                    <RegularExpression ValidationExpression="\d*" ErrorText="شماره سریال را صحیح وارد نمایید" />
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">
                                                            <dxe:ASPxLabel runat="server" Text="تاریخ صدور" ID="lblRegDate">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="240px" ShowPickerOnTop="True"
                                                                                ID="txtdLastRegDate" PickerDirection="ToRight" ShowPickerOnEvent="OnClick" IconUrl="~/Image/Calendar.gif"
                                                                                onchange="return SetMeDocDefualtExpireDateJS();"></pdc:PersianDateTextBox>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxImage ID="btnSetRegDate" ClientInstanceName="btnSetRegDate" ToolTip="تنظیم تاریخ اعتبار"
                                                                                runat="server" Text="" Height="13px" Border-BorderWidth="1px" Border-BorderColor="LightBlue"
                                                                                Width="13px" Image-Height="13px" Image-Width="13px" ImageUrl="~/Images/ResetDate.png">
                                                                                <ClientSideEvents Click="function(s, e) { SetMeDocDefualtExpireDateJS(); }" />
                                                                            </dxe:ASPxImage>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <dxe:ASPxLabel runat="server" Text="تاریخ پایان اعتبار" ID="ASPxLabel14">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <pdc:PersianDateTextBox ShowPickerOnEvent="OnClick" runat="server" DefaultDate=""
                                                                Width="240px" ShowPickerOnTop="True" ID="txtdExpDate" PickerDirection="ToRight"
                                                                IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">
                                                              <dxe:ASPxLabel runat="server" Text="پایه" ID="lblGrade" ClientInstanceName="lblGrade">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <TSPControls:CustomAspxComboBox runat="server"  Width="100%"  
                                                                 ID="ComboBoxGrade" ClientInstanceName="ComboBoxGrade" ValueType="System.String"
                                                                 EnableIncrementalFiltering="True"
                                                                IncrementalFilteringMode="StartsWith" RightToLeft="True" DataSourceID="ObjectDataSourceGrade"
                                                                TextField="GrdName" ValueField="GrdId">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                   
                                                                    <RequiredField IsRequired="True" ErrorText="پایه را انتخاب نمایید"></RequiredField>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                                <ButtonStyle Width="13px">
                                                                </ButtonStyle>
                                                            </TSPControls:CustomAspxComboBox>
                                                            <asp:ObjectDataSource ID="ObjectDataSourceGrade" runat="server" SelectMethod="GetData"
                                                                TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>
                                                        </td>
                                                        <td valign="top" align="right"></td>
                                                        <td valign="top" align="right"></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </TSPControls:CustomAspxCallbackPanel>
                            </fieldset>
                            <br />
                            <fieldset id="RoundPanelAccounting" runat="server">
                                <legend class="fieldset-legend">ثبت فیش
                                </legend>
                                <asp:Panel ID="PanelAccountingInserting" runat="server">
                                    <div align="center">
                                        <table id="tableAccounting" dir="rtl" runat="server" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td colspan="4" valign="top" align="center">
                                                        <dxe:ASPxLabel runat="server" ID="lblRegEnter" ClientInstanceName="lblRegEnter" ForeColor="Blue">
                                                        </dxe:ASPxLabel>
                                                        <dxe:ASPxLabel runat="server" ID="lblReg" ClientInstanceName="lblReg" ClientVisible="false"
                                                            ForeColor="Blue">
                                                        </dxe:ASPxLabel>
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right" style="width: 15%">
                                                        <dxe:ASPxLabel runat="server" Text="بابت" ID="ASPxLabel17">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td dir="rtl" valign="top" align="right" style="width: 35%">
                                                        <TSPControls:CustomAspxComboBox runat="server"  Width="100%" ID="cmbAccType"
                                                             ValueType="System.Int32" SelectedIndex="0"
                                                             DataSourceID="ObjectDataSourceAccType"
                                                            TextField="TypeName" ValueField="AccTypeId" ClientInstanceName="cmbAccType">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ValidationSettings Display="Dynamic">
                                                                <RequiredField ErrorText=""></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <ButtonStyle Width="13px">
                                                            </ButtonStyle>
                                                            
                                                        </TSPControls:CustomAspxComboBox>
                                                        <asp:ObjectDataSource ID="ObjectDataSourceAccType" runat="server" SelectMethod="GetData"
                                                            TypeName="TSP.DataManager.TechnicalServices.AccTypeManager"></asp:ObjectDataSource>
                                                    </td>
                                                    <td valign="top" align="right" style="width: 15%">
                                                        <dxe:ASPxLabel runat="server" Text="مبلغ" ID="ASPxLabel33">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td dir="ltr" valign="top" align="right" style="width: 35%">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtaAmount"  Width="100%" 
                                                            ClientInstanceName="txtaAmount">
                                                            <ValidationSettings Display="Dynamic" ValidationGroup="Acc" ErrorTextPosition="Bottom">
                                                                
                                                                <RequiredField IsRequired="True" ErrorText="مبلغ را وارد نمایید"></RequiredField>
                                                                <RegularExpression ErrorText="نامعتبر" ValidationExpression="[1-9]\d*"></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="شماره" ID="ASPxLabel31">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtaNumber"  Width="100%" 
                                                            Style="direction: ltr" ClientInstanceName="txtaNumber">
                                                            <ValidationSettings Display="Dynamic" ValidationGroup="Acc" ErrorTextPosition="Bottom">
                                                                
                                                                <RequiredField IsRequired="True" ErrorText="شماره را وارد نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ" ID="ASPxLabel32">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                            Width="240px" ShowPickerOnTop="True" ValidationGroup="Acc" ID="txtaDate" PickerDirection="ToRight"
                                                            RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                            ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtaDate" ValidationGroup="Acc"
                                                            ID="PersianDateValidator1">تاریخ نامعتبر</pdc:PersianDateValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel25">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="30px" ID="txtaDesc"  Width="100%"
                                                             ClientInstanceName="txtaDesc">
                                                            <ValidationSettings>
                                                              
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="middle" align="center" colspan="4">
                                                        <br />
                                                        <TSPControls:CustomAspxButton   runat="server" ImagePosition="left" Text="اضافه به لیست" 
                                                            ID="btnAddAccounting" ValidationGroup="Acc" 
                                                            AutoPostBack="False" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) { btnAdd_Click(); }" />
                                                            <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                                        </TSPControls:CustomAspxButton>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="center" colspan="4"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <TSPControls:CustomAspxDevGridView2 runat="server"   Width="100%"
                                    ID="GridViewAccounting" KeyFieldName="AccountingId" AutoGenerateColumns="False"
                                    OnHtmlRowPrepared="GridViewAccounting_HtmlRowPrepared"
                                    OnRowDeleting="GridViewAccounting_RowDeleting" ClientInstanceName="grid" OnCustomCallback="GridViewAccounting_CustomCallback">
                                    
                                    <SettingsCookies Enabled="false" />
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn  VisibleIndex="0" Caption=" " ButtonType="Image" Name="Delete"
                                            Width="25px" ShowDeleteButton="true" >
                                           <%-- <DeleteButton Visible="True" Image-Url="~/Images/DeleteFromGrid.png">
                                            </DeleteButton>--%>
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="AccType"
                                            Caption="بابت">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="AccTypeName" Caption="بابت">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Number" Caption="شماره">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Date" Caption="تاریخ">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Amount" Caption="مبلغ">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                            <PropertiesTextEdit DisplayFormatString="#,#">
                                            </PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <ClientSideEvents EndCallback="function(s,e){                                    
                                        if(s.cpSaveComplete=='1'){
                                         ClearAccounting();
                                         s.cpSaveComplete='0';
                                         }
                                        else if(s.cpMessage!='')
                                        {
                                         alert(s.cpMessage);
                                         s.cpMessage='';
                                        }
                                        }" />
                                </TSPControls:CustomAspxDevGridView2>
                            </fieldset>
                            <br />
                            <fieldset id="RoundPanelFileAttachment" runat="server">
                                <legend class="fieldset-legend">فایل های پیوست
                                </legend>

                                <table runat="server" id="TblFile" dir="rtl" width="100%">
                                    <tr runat="server" id="Tr14">
                                        <td runat="server" id="Td41" style="vertical-align: top; text-align: right">
                                            <asp:Label runat="server" Text="فایل" Width="24px" ID="lblimg"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td42" align="right">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                ID="flp" InputType="Files" ClientInstanceName="flpc" OnFileUploadComplete="flp_FileUploadComplete">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
                                                        if(e.isValid){
	imgEndUploadImgClient3.SetVisible(true);
flpme.Set('name',1);
	lbl1.SetVisible(false);
    }
    else{
	imgEndUploadImgClient3.SetVisible(false);
flpme.Set('name',0);
	lbl1.SetVisible(false);
    }
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxUploadControl>
                                                            <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                                ID="ASPxLabel6" ForeColor="Red" ClientInstanceName="lbl1">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                ID="ASPxImage2" ClientInstanceName="imgEndUploadImgClient3">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr15">
                                        <td runat="server" id="Td43" style="vertical-align: top; text-align: right">
                                            <asp:Label runat="server" Text="توضیحات" Width="82px" ID="Label52"></asp:Label>
                                        </td>
                                        <td runat="server" id="Td44" style="text-align: right">
                                            <TSPControls:CustomASPXMemo runat="server" Height="30px" ID="txtDescImg"  Width="530px"
                                                >
                                                <ValidationSettings>
                                                    
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="Tr16">
                                        <td runat="server" id="Td45" align="center" colspan="2">
                                            <br />
                                            <TSPControls:CustomAspxButton  runat="server" Text="اضافه"  CausesValidation="False"
                                                Width="70px" ID="btnAddFlp" UseSubmitBehavior="False" 
                                                OnClick="btnAddFlp_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if(flpme.Get('name')!=1)
{
lbl1.SetVisible(true);

e.processOnServer=false;
}
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <div dir="rtl" align="center">
                                    <TSPControls:CustomAspxDevGridView runat="server"  EnableViewState="False"
                                        Width="100%" ID="AspxGridFlp" KeyFieldName="Id" AutoGenerateColumns="False"  >
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FilePath" Caption="فایل"
                                                Name="FilePath">
                                                <DataItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" Text="آدرس فایل" NavigateUrl='<%# Bind("TempImgUrl") %>'
                                                        Target="_blank"></asp:HyperLink>
                                                </DataItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
                                                </EditItemTemplate>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Description" Caption="توضیحات"
                                                Name="Description">
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>
                                    </TSPControls:CustomAspxDevGridView>
                                </div>

                            </fieldset>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
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
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="BtnNew_Click">
                                                              
                                                                <Image  Url="~/Images/icons/new.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                                                EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                               
                                                                <Image  Url="~/Images/icons/edit.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ButtonType="Save" runat="server" Text=" "  ToolTip="ذخیره"
                                                                ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                OnClick="btnSave_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	if(CheckCharacterEncoding(txt1.GetText())==false)
 {
txt1.SetIsValid(false);
txt1.SetErrorText('حروف وارد شده نامعتبر است');
	e.processOnServer=false;
}
//if(flpArm2.Get('name')!=1)
//{
//lblArm.SetVisible(true);
//e.processOnServer=false;
//}

//if(flpSign2.Get('name')!=1)
//{
//lblSign.SetVisible(true);
//e.processOnServer=false;
//}
}"></ClientSideEvents>
                                                             
                                                             
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td width="10px" align="center">
                                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnPrint2" runat="server" CausesValidation="False" 
                                                                EnableTheming="False" EnableViewState="False" Text=" " ToolTip="چاپ" UseSubmitBehavior="False"
                                                                AutoPostBack="False">
                                                               
                                                                <Image Height="25px" Url="~/Images/icons/printers.png" Width="25px" />
                                                                <ClientSideEvents Click="function(s, e) {
		Callback.PerformCallback('Print');
}" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td width="10px" align="center">
                                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="btnBack_Click">
                                                               
                                                                <Image  Url="~/Images/icons/Back.png">
                                                                </Image>
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
                <asp:HiddenField ID="OfficeId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="HDReType" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
                <asp:ObjectDataSource ID="OdbOfType" runat="server" UpdateMethod="Update" TypeName="TSP.DataManager.OfficeTypeManager"
                    SelectMethod="GetData" OldValuesParameterFormatString="original_{0}" InsertMethod="Insert"
                    DeleteMethod="Delete"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="OdbOfAtType" runat="server" UpdateMethod="Update" TypeName="TSP.DataManager.OfficeActivityTypeManager"
                    SelectMethod="GetData" InsertMethod="Insert" DeleteMethod="Delete"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODBMrsId" runat="server" TypeName="TSP.DataManager.MembershipRegistrationStatusManager"
                    SelectMethod="GetData" EnableCaching="True" CacheDuration="30"></asp:ObjectDataSource>
                <dxhf:ASPxHiddenField ID="HDFlpArm" runat="server" ClientInstanceName="flpArm2">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="HDFlpSign" runat="server" ClientInstanceName="flpSign2">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="HDFlpMember" runat="server" ClientInstanceName="flpme">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="HiddenFieldOffice" runat="server" ClientInstanceName="HiddenFieldOffice">
                </dxhf:ASPxHiddenField>
                <asp:HiddenField ID="HDMFType" runat="server" Visible="False"></asp:HiddenField>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
   
    <script language="javascript" type="text/javascript">
        function btnAdd_Click() {
            var txtDate = document.getElementById('<%= txtaDate.ClientID %>');

            if (txtaNumber.GetIsValid() && txtaAmount.GetIsValid()) {
                grid.PerformCallback('Add');
            }
        }

        function ClearAccounting() {
            cmbAccType.SetSelectedIndex(0);
            var txtDate = document.getElementById('<%= txtaDate.ClientID %>');
            txtDate.value = "";
            txtaNumber.SetText("");
            txtaAmount.SetText(HiddenFieldOffice.Get('FishAmount'));
            txtaDesc.SetText("");
        }
        function SetMeDocDefualtExpireDateJS() {
            CallbackPanelDoRegDate.PerformCallback(cmbIsTemporary.GetValue());
        }
    </script>
</asp:Content>
