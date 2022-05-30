<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddSettlementAgent.aspx.cs" Inherits="Employee_Employee_AddSettlementAgent"
    Title="مشخصات عضو مسكن" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function SetControlValues() {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'MeId;FirstName;LastName;FatherName;IdNo;SSN;HomeTel;HomeAdr;MobileNo;Email;BirhtDate', SetValue);
        }
        function SetValue(values) {
            ID.SetText(values[0]);
            mFirstName.SetText(values[1]);
            mLastName.SetText(values[2]);
            txtFatherNameClient.SetText(values[3]);
            txtIdNoClient.SetText(values[4]);
            txtSSNClient.SetText(values[5]);
            txtTelClient.SetText(values[6]);
            txtAddressClient.SetText(values[7]);
            txtMobileNoClient.SetText(values[8]);
            txtEmailClient.SetText(values[9]);
            document.getElementById('<%=txtBrithDate.ClientID%>').value = values[10];
            HiddenFieldSettlementAgent.Set('MeId', values[0]);


        }
        function SetCombo() {
           
            if (cmbMemberType.GetValue() == '0') {
                SetEmpty();
                lblMe.SetVisible(true);
                ID.SetVisible(true);
                mFirstName.SetEnabled(false);
                mLastName.SetEnabled(false);
                txtFatherNameClient.SetEnabled(false);
                txtIdNoClient.SetEnabled(false);
                txtSSNClient.SetEnabled(false);
                txtTelClient.SetEnabled(false);
                txtAddressClient.SetEnabled(false);
                txtMobileNoClient.SetEnabled(false);
                txtEmailClient.SetEnabled(false);

                document.getElementById('<%=txtBrithDate.ClientID%>').disabled = true;
                document.getElementById('<%=txtBrithDate.ClientID%>').setAttribute("usedatepicker", false);
               
            }
            else {
                SetEmpty();
                lblMe.SetVisible(false);
                ID.SetVisible(false);
                mFirstName.SetEnabled(true);
                mLastName.SetEnabled(true);
                txtFatherNameClient.SetEnabled(true);
                txtIdNoClient.SetEnabled(true);
                txtSSNClient.SetEnabled(true);
                txtTelClient.SetEnabled(true);
                txtAddressClient.SetEnabled(true);
                txtMobileNoClient.SetEnabled(true);
                txtEmailClient.SetEnabled(true);

                document.getElementById('<%=txtBrithDate.ClientID%>').disabled = false;
                document.getElementById('<%=txtBrithDate.ClientID%>').setAttribute("usedatepicker", true);
               
            }
        }
        function SetEmpty() {
            ID.SetText("");
            mFirstName.SetText("");
            mLastName.SetText("");
            txtFatherNameClient.SetText("");
            txtIdNoClient.SetText("");
            txtSSNClient.SetText("");
            txtTelClient.SetText("");
            txtAddressClient.SetText("");
            txtMobileNoClient.SetText("");
            txtEmailClient.SetText("");
            document.getElementById('<%=txtBrithDate.ClientID%>').value = "";
        }
       
      //  window.onload = SetCombo;
       
    </script>

        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]
                </div>

                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0" width="100%">
                                <tbody>
                                    <tr>
                                        <td dir="ltr" valign="top" align="right">
                                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید" CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>

                                                                <Image  Url="~/Images/icons/new.png"></Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش" CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>

                                                                <Image  Url="~/Images/icons/edit.png"></Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره" ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnSave_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>

                                                                <Image  Url="~/Images/icons/save.png"></Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td >
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت" CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>

                                                                <Image  Url="~/Images/icons/Back.png"></Image>
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
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelSettelmentAgent" HeaderText="مشاهده" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="vertical-align: top; text-align: right" dir="rtl" id="Table2" cellpadding="1" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" Width="15%">
                                            <dxe:ASPxLabel runat="server" Text="وضعیت عضویت در نظام" Width="100%" ID="ASPxLabel17"></dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right" Width="35%" >
                                            <TSPControls:CustomAspxComboBox runat="server"   Width="100%" Enabled="False"  ID="cmbMemberType" ValueType="System.String" Height="21px" ClientInstanceName="cmbMemberType"  >
                                                <ClientSideEvents SelectedIndexChanged="function(s, e){
SetCombo();         
}"></ClientSideEvents>

                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <RequiredField IsRequired="True" ErrorText="نوع عضو را انتخاب نمایید"></RequiredField>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <Items>
                                                    <dxe:ListEditItem Value="0" Text="عضو نظام"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Value="1" Text="شخص جدید"></dxe:ListEditItem>
                                                </Items>

                                                <ButtonStyle Width="13px"></ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right" Width="15%"></td>
                                        <td valign="top" align="right"  Width="35%"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="lblMeId" ClientInstanceName="lblMe"></dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMeNo"  Width="100%" AutoPostBack="True" ClientInstanceName="ID"  OnTextChanged="txtMeID_TextChanged">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right"></td>
                                        <td valign="top" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام" ID="lblMeFirstName" ClientInstanceName="lblMname"></dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtName"  Width="100%" ClientInstanceName="mFirstName" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="lblMeLastName" ClientInstanceName="lblMfamily"></dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFamily"  Width="100%" ClientInstanceName="mLastName" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="نام خانوادگی را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام پدر" Width="100%" ID="ASPxLabel7"></dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFatherName"  EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtFatherNameClient" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="نام پدر را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ تولد" ID="ASPxLabel11"></dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="240px" ShowPickerOnTop="True" ID="txtBrithDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr;"></pdc:PersianDateTextBox>
                                            <asp:RequiredFieldValidator runat="server" ErrorMessage="تاریخ تولد" ToolTip="تاریخ را وارد نمایید" ControlToValidate="txtBrithDate" ID="RequiredFieldValidator8" Display="Dynamic">تاریخ را وارد نمایید</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره شناسنامه" Width="100%" ID="ASPxLabel8"></dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtIdNo"  EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtIdNoClient" >
                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="شماره شناسنامه را وارد نمایید"></RequiredField>

                                                    <RegularExpression ErrorText=""></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد ملی" ID="ASPxLabel12"></dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtSSN"  EnableClientSideAPI="True" Width="100%" MaxLength="10" ClientInstanceName="txtSSNClient" >
                                                <MaskSettings IncludeLiterals="DecimalSymbol"></MaskSettings>

                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="کد ملی را وارد نمایید"></RequiredField>

                                                    <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="\d{10}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره تلفن" Width="100%" ID="ASPxLabel9"></dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtTell"  EnableClientSideAPI="True" Width="100%" MaxLength="12" ClientInstanceName="txtTelClient" >
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="شماره تلفن به صورت چهار رقم پیش شماره و هفت رقم شماره تلفن می باشد(07116360332)" ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="شماره تلفن به صورت چهار رقم پیش شماره و هفت رقم شماره تلفن می باشد(07116360332)"></RegularExpression>

                                                    <ErrorFrameStyle Wrap="True"></ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره  همراه" Width="100%" ID="ASPxLabel13"></dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMobile"  EnableClientSideAPI="True" Width="100%" MaxLength="11" ClientInstanceName="txtMobileNoClient" >
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="0\d{10}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="آدرس پست الکترونیکی" Width="100%" ID="ASPxLabel16"></dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" >
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtEmail"  EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtEmailClient" >
                                                <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="آدرس پست الکترونیکی را وارد نمایید."></RequiredField>

                                                    <RegularExpression ErrorText="این آدرس صحیح نیست" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="آدرس" ID="ASPxLabel3"></dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtAddress"  Width="100%" ClientInstanceName="txtAddressClient" >
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
                                    <td align="right" valign="top">
                                       تصویر امضا
                                    </td>
                                    <td align="right" colspan="3" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                            ID="flpSign" InputType="Images" ClientInstanceName="flpSign" OnFileUploadComplete="flpSign_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadSign.SetVisible(true);
	lblSign.SetVisible(false);
	HpSign.SetVisible(true);
	HpSign.SetImageUrl('../../image/SettlmentSign/'+e.callbackData);
    HiddenFieldSettlementAgent.Set('SignImage','~/image/SettlmentSign/'+e.callbackData);
	}
	else{
	imgEndUploadSign.SetVisible(false);
   	lblSign.SetVisible(true);
	HpSign.SetVisible(false);
	HpSign.SetImageUrl('');
    HiddenFieldSettlementAgent.Set('SignImage','');
	}
}" />
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="lblSign" runat="server" ClientInstanceName="lblSign" ClientVisible="False"
                                                            ForeColor="Red" Text="تصویر امضا را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="false" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="imgEndUploadSign" ClientInstanceName="imgEndUploadSign">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxImage ID="HpSign" runat="server" Height="75px" Width="75px" ClientInstanceName="HpSign"
                                            Target="_blank" Text="تصویر امضا" ClientVisible="true" Border-BorderWidth="1px" Border-BorderStyle="Solid">
                                            <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                            </EmptyImage>
                                        </dxe:ASPxImage>

                                    </td>
                                </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" Width="100%" ID="ASPxLabel15"></dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtDescription"  EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtDescClient" >
                                                <ValidationSettings>
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

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
                <br />
                
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                    <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldSettlementAgent" ClientInstanceName="HiddenFieldSettlementAgent"></dxhf:ASPxHiddenField>
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید" CausesValidation="False" ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/new.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش" CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/edit.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره" ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnSave_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/save.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت" CausesValidation="False" ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/Back.png"></Image>
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

               
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
            </ContentTemplate>
        </asp:UpdatePanel>

</asp:Content>
