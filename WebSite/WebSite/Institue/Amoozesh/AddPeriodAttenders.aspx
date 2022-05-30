<%@ Page Title="مشخصات شرکت کننده در دوره" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="AddPeriodAttenders.aspx.cs" Inherits="Institue_Amoozesh_AddPeriodAttenders" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetPeriodControlValues() {
            gridPeriod.GetRowValues(gridPeriod.GetFocusedRowIndex(), 'PPId;PName;Capacity;InsId;StartDate;EndDate;InsName;InActiveName;PeriodTitle;PeriodStatus;PPType;Address;CountRegister;PType;StartRegisterDate;EndRegisterDate;TestDate;PeriodCost;TestCost;PPCode', SetPeriodValue);
        }

        function SetPeriodValue(values) {
            txtPeriodTittle.SetText(values[8] + '(' + values[19] + ')');
            HiddenFieldCourseRegister.Set('PPId', values[0]);
            HiddenFieldCourseRegister.Set('PType', values[13]);
            HiddenFieldCourseRegister.Set('PeriodCost', values[17]);
            HiddenFieldCourseRegister.Set('TestCost', values[18]);
            HiddenFieldCourseRegister.Set('StartDate', values[4]);
            if (values[13] == 0) {
                cmbRegisterType.SetEnabled(true);
                lblRegisterType.SetEnabled(true);
            }
            if (values[13] == 1) {
                cmbRegisterType.SetEnabled(false);
                lblRegisterType.SetEnabled(false);
            }
        }

        function SetEnable(flag) {

            txtMeNo.SetText('');
            txtSSN.SetText('');
            txtName.SetText('');
            txtLastName.SetText('');
            txtName.SetText('');
            txtFatherName.SetText('');
            txtIdNo.SetText('');
            txtTel.SetText('');
            txtMobileNo.SetText('');
            //txtAddress.SetText('');
            txtBirthPlace.SetText('');
            txtDesc.SetText('');
            cmbProvince.SetSelectedIndex(-1);
            cmbMajor.SetSelectedIndex(-1);
            txtFileNo.SetText('');
            txtGrade.SetText('');
            //txtAddress.SetText('');

            document.getElementById('<%=txtBrithDate.ClientID%>').value = '';

            lblMeId.SetVisible(!flag);
            txtMeNo.SetVisible(!flag);
            txtSSN.SetEnabled(flag);
            txtName.SetEnabled(flag);
            txtLastName.SetEnabled(flag);
            txtName.SetEnabled(flag);
            txtFatherName.SetEnabled(flag);
            txtIdNo.SetEnabled(flag);
            txtTel.SetEnabled(flag);
            txtMobileNo.SetEnabled(flag);
            //txtAddress.SetEnabled(flag);
            txtBirthPlace.SetEnabled(flag);
            txtDesc.SetEnabled(flag);
            document.getElementById('<%=txtBrithDate.ClientID%>').disabled = !flag;
            cmbProvince.SetEnabled(flag);
            txtMeNoOtp.SetVisible(flag);
            lblMeNoOtp.SetVisible(flag);

            txtFileNo.SetEnabled(flag);
            cmbMajor.SetEnabled(flag);
            txtGrade.SetEnabled(flag);
            lblflpCon.SetVisible(flag);

            flpi.SetVisible(flag);



            if (!flag) {
                hpConfAttach.SetNavigateUrl('');
                HiddenFieldCourseRegister.Set('Confletter', 0);
                imgEndUploadImgClientflpConfAttach.SetVisible(flag);
                lblImageWarning.SetVisible(flag);
                hpConfAttach.SetVisible(flag);
            }


        }

        function SetFiche() {
            lblFishNo.SetText('شماره فیش');
        }
        function SetPose() {
            lblFishNo.SetText('شناسه واریز');
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>

    <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelMain" runat="server" ClientInstanceName="CallbackPanelMain"
        OnCallback="CallbackPanelMain_Callback">
        <ClientSideEvents EndCallback="function(s, e) {  }" />
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent1" runat="server">
                <div id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew" runat="server" AutoPostBack="False" CausesValidation="False"
                                                 EnableTheming="False" EnableViewState="False"
                                                OnClick="btnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False" Width="25px">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" AutoPostBack="False" CausesValidation="False"
                                                 EnableTheming="False" EnableViewState="False"
                                                OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False" Width="25px">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server" AutoPostBack="False" 
                                                EnableTheming="False" EnableViewState="False" OnClick="btSave_Click" Text=" "
                                                ToolTip="ذخیره" UseSubmitBehavior="False" Width="25px">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                      
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False" 
                                                EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                ToolTip="بازگشت" UseSubmitBehavior="False">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                  <fieldset>
            <legend class="HelpUL" dir="rtl"><b>اطلاعات دوره</b></legend>
            <table width="100%">
                <tr>
                    <td valign="top" align="right" width="15%">
                        <dxe:ASPxLabel runat="server" Text="عنوان دوره:" Width="100%" ID="ASPxLabel5">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="top" colspan="3" align="right" width="75%">
                        <dxe:ASPxLabel runat="server" Text="" Width="100%" ID="lblPeriodTitle">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right" width="15%">
                        <dxe:ASPxLabel runat="server" Text="کد دوره:" Width="100%" ID="ASPxLabel14">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="top" align="right" width="35%">
                        <dxe:ASPxLabel runat="server" Text="" Width="100%" ID="lblPPCode">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="top" align="right" width="15%">
                        <dxe:ASPxLabel runat="server" Text="ظرفیت دوره:" Width="100%" ID="ASPxLabel20">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="top" align="right" width="35%">
                        <dxe:ASPxLabel runat="server" Text="" Width="100%" ID="lblCapacity">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right" width="15%">
                        <dxe:ASPxLabel runat="server" Text="تاریخ شروع:" Width="100%" ID="ASPxLabel21">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="top" align="right" width="35%">
                        <dxe:ASPxLabel runat="server" Text="" Width="100%" ID="lblStartDate">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="top" align="right" width="15%">
                        <dxe:ASPxLabel runat="server" Text="تاریخ پایان:" Width="100%" ID="ASPxLabel22">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="top" align="right" width="35%">
                        <dxe:ASPxLabel runat="server" Text="" Width="100%" ID="lblEndDate">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right" width="15%">
                        <dxe:ASPxLabel runat="server" Text="تاریخ شروع ثبت نام:" Width="100%" ID="ASPxLabel23">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="top" align="right" width="35%">
                        <dxe:ASPxLabel runat="server" Text="" Width="100%" ID="lblStartRegisterDate">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="top" align="right" width="15%">
                        <dxe:ASPxLabel runat="server" Text="تاریخ پایان ثبت نام:" Width="100%" ID="ASPxLabel24">
                        </dxe:ASPxLabel>
                    </td>
                    <td valign="top" align="right" width="35%">
                        <dxe:ASPxLabel runat="server" Text="" Width="100%" ID="lblEndRegisterDate">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
            </table>
        </fieldset>
       <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelRegister" HeaderText="مشاهده" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table dir="rtl" id="Table2" cellpadding="1" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="center" colspan="6">
                                            <dxe:ASPxLabel runat="server" Text="وضعیت درخواست : نامشخص" ForeColor="Red" ID="lblWorkFlowState"
                                                Wrap="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="وضعیت عضویت" Width="100%" ID="ASPxLabel17">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right" width="35%">
                                            <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" Width="100%" 
                                                  ClientInstanceName="cmbMemberType"
                                                RightToLeft="True" ID="cmbMemberType">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
if(cmbMemberType.GetSelectedIndex()==0)
{
	SetEnable(false);
}
else
{
	SetEnable(true);
}
}"></ClientSideEvents>
                                                <Items>
                                                    <dxe:ListEditItem Text="عضو نظام" Value="0"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Text="دیگر اشخاص" Value="1"></dxe:ListEditItem>
                                                </Items>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td dir="ltr" valign="top" align="right" width="15%"></td>
                                        <td valign="top" align="right" width="35%"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد عضویت *" ClientInstanceName="lblMeId" Width="100%"
                                                ID="lblMeId" ClientVisible="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="100%" AutoPostBack="True" ClientVisible="False"
                                                  ClientInstanceName="txtMeNo"
                                                ID="txtMeNo" OnTextChanged="txtMeNo_TextChanged">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ErrorText="کد عضویت را با فرمت صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                                    <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right"></td>
                                        <td valign="top" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام" ClientInstanceName="lblMname" ID="lblMeFirstName">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="100%"  
                                                ClientInstanceName="txtName" ID="txtName">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ClientInstanceName="lblMfamily"
                                                ID="lblMeLastName">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="100%"  
                                                ClientInstanceName="txtLastName" ID="txtLastName">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="نام خانوادگی را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام پدر" Width="100%" ID="ASPxLabel7">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="100%"  
                                                ClientInstanceName="txtFatherName" EnableClientSideAPI="True" ID="txtFatherName">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ تولد*" ID="ASPxLabel11">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                PickerDirection="ToRight" ShowPickerOnTop="True" Width="245px" ID="txtBrithDate"
                                                Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBrithDate" ErrorMessage="تاریخ تولد"
                                                Display="Dynamic" ToolTip="لطفاً تاریخ تولد را وارد نمایید." ID="RequiredFieldValidator8">لطفاً تاریخ تولد را وارد نمایید.
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="محل تولد" Width="100%" ID="ASPxLabel4">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="100%"  
                                                ClientInstanceName="txtBirthPlace" EnableClientSideAPI="True" ID="txtBirthPlace">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره شناسنامه" Width="100%" ID="ASPxLabel8">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="100%"  
                                                ClientInstanceName="txtIdNo" EnableClientSideAPI="True" ID="txtIdNo">
                                                <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="\d{0,10}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td dir="ltr" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="* کد ملی" Width="100%" ID="ASPxLabel12">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="10"  
                                                ClientInstanceName="txtSSN" EnableClientSideAPI="True" ID="txtSSN">
                                                <MaskSettings IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="\d{10}"></RegularExpression>
                                                    <RequiredField IsRequired="True" ErrorText="کد ملی را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره تلفن" Width="100%" ID="ASPxLabel9">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="12"  
                                                ClientInstanceName="txtTel" EnableClientSideAPI="True" ID="txtTel">
                                                <ValidationSettings Display="Dynamic" ErrorText="شماره تلفن به صورت چهار رقم پیش شماره و هفت رقم شماره تلفن می باشد(07116360332)"
                                                    ErrorTextPosition="Bottom">
                                                    <ErrorFrameStyle Wrap="True">
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ErrorText="شماره تلفن به صورت چهار رقم پیش شماره و هفت رقم شماره تلفن می باشد(07116360332)"
                                                        ValidationExpression="0\d{8,11}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td dir="ltr" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره تلفن همراه" Width="100%" ID="ASPxLabel13">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="11"  
                                                ClientInstanceName="txtMobileNo" EnableClientSideAPI="True" ID="txtMobileNo">
                                                <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                    ErrorTextPosition="Bottom">
                                                    <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="0\d{10}"></RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right"></td>
                                        <td dir="ltr" valign="top" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td dir="ltr" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="* استان" Width="100%" ID="ASPxLabel10">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ObjdsProvince"
                                                TextField="PrName" ValueField="PrId" Width="100%" 
                                                  
                                                ClientInstanceName="cmbProvince" ID="cmbProvince" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings ErrorTextPosition="Bottom">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="True" ErrorText="استان را انتخاب نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                            <asp:ObjectDataSource runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.ProvinceManager"
                                                ID="ObjdsProvince"></asp:ObjectDataSource>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره عضویت *" ClientInstanceName="lblMeNoOtp"
                                                Width="100%" ID="lblMeNoOtp">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="100%" 
                                                  ClientInstanceName="txtMeNoOtp"
                                                ID="txtMeNoOtp">
                                                <ValidationSettings ErrorTextPosition="Bottom">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ErrorText="کدعضویت عدد صحیح می باشد" ValidationExpression="\d*"></RegularExpression>
                                                    <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td dir="ltr" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="* رشته تحصیلی" Width="100%" ID="ASPxLabel16">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ObjdsMajor"
                                                TextField="MjName" ValueField="MjId" Width="100%" 
                                                  
                                                ClientInstanceName="cmbMajor" ID="cmbMajor" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                <ValidationSettings ErrorTextPosition="Bottom">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="True" ErrorText="رشته تحصیلی را انتخاب نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                            <asp:ObjectDataSource runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager"
                                                ID="ObjdsMajor"></asp:ObjectDataSource>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره پروانه اشتغال" Width="100%" ID="ASPxLabel19">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="100%" 
                                                  ClientInstanceName="txtFileNo"
                                                ID="txtFileNo">
                                                <ValidationSettings>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" dir="ltr" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="تصویر معرفی نامه" Width="100px" ID="lblflpCon" ClientInstanceName="lblflpCon">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" dir="rtl" valign="top" colspan="3">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxUploadControl ID="flpConfAttach" runat="server" ClientInstanceName="flpi"
                                                                UploadWhenFileChoosed="true" OnFileUploadComplete="flpConfAttach_FileUploadComplete">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClientflpConfAttach.SetVisible(true);
  	HiddenFieldCourseRegister.Set('Confletter',1);
	lblImageWarning.SetVisible(false);
	hpConfAttach.SetVisible(true);
	hpConfAttach.SetNavigateUrl('../../Image/Amoozesh/Period/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientflpConfAttach.SetVisible(false);
	lblImageWarning.SetVisible(true);
	hpConfAttach.SetVisible(false);
	hpConfAttach.SetNavigateUrl('');    
  	HiddenFieldCourseRegister.Set('Confletter',0);
	}
}" />
                                                            </TSPControls:CustomAspxUploadControl>
                                                            <dxe:ASPxLabel ID="lblImageWarning" runat="server" ClientInstanceName="lblImageWarning"
                                                                ClientVisible="False" ForeColor="Red" Text="تصویر معرفی نامه راانتخاب نمایید">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxImage ID="imgEndUploadImgflpConfAttach" runat="server" ClientInstanceName="imgEndUploadImgClientflpConfAttach"
                                                                ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویرانتخاب شد">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <dxe:ASPxHyperLink ID="HpflpConfAttach" runat="server" ClientInstanceName="hpConfAttach"
                                                Target="_blank" Text="تصویر معرفی نامه">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td dir="ltr" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="پایه و صلاحیت" Width="100%" ID="ASPxLabel18">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="rtl" valign="top" align="right" colspan="3">
                                            <TSPControls:CustomTextBox runat="server" Width="100%" 
                                                  ClientInstanceName="txtGrade"
                                                ID="txtGrade">
                                                <ValidationSettings>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                 <%--   <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="آدرس" ID="ASPxLabel3">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%"  
                                                ClientInstanceName="txtAddress" ID="txtAddress">
                                                <ValidationSettings>
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td style="height: 46px" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" Width="100%" ID="ASPxLabel15">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="height: 46px" valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%"  
                                                ClientInstanceName="txtDesc" EnableClientSideAPI="True" ID="txtDesc">
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
                                            <dxe:ASPxLabel runat="server" Text="دوره *" Width="100%" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="rtl" valign="top" align="right" colspan="3">
                                            <TSPControls:CustomTextBox runat="server" Width="100%" ReadOnly="True"  
                                                ClientInstanceName="txtPeriodTittle" ID="txtPeriodTittle">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نوع ثبت نام" ClientInstanceName="lblRegisterType"
                                                Width="100%" ID="lblRegisterType">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" SelectedIndex="2" ValueType="System.String" Width="100%"
                                                ClientEnabled="False"  
                                                 ClientInstanceName="cmbRegisterType"
                                                ID="cmbRegisterType" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                                                CallbackPanelMain.PerformCallback('RegisterTypeChange');}"></ClientSideEvents>
                                                <Items>
                                                    <dxe:ListEditItem Text=" دوره و آزمون" Value="0"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Text="آزمون" Value="1"></dxe:ListEditItem>

                                                </Items>
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right"></td>
                                        <td dir="ltr" valign="top" align="right" colspan="3"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" valign="top" align="center">
                                            <br />
                                            <TSPControls:CustomASPxRoundPanel ID="RoundPanelAccounting" ClientInstanceName="RoundPanelAccounting"
                                                HeaderText="مشخصات پرداخت" runat="server" Width="100%">
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <table width="100%">
                                                            <tr>
                                                                <td colspan="4" align="center">
                                                                    <dxe:ASPxLabel runat="server" Font-Bold="true" Width="100%" Font-Size="10px" Text=""
                                                                        ID="lblWarningPrice" ForeColor="DarkRed">
                                                                    </dxe:ASPxLabel>
                                                                    <dxe:ASPxLabel runat="server" ID="lblPrice" ForeColor="Blue">
                                                                    </dxe:ASPxLabel>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" valign="top" colspan="4">
                                                                    <dxe:ASPxLabel runat="server" ID="lblCost" ClientInstanceName="lblCost" Text="مبلغ قابل پرداخت: ---"
                                                                        ForeColor="Blue">
                                                                    </dxe:ASPxLabel>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="top" width="15%">
                                                                    <dxe:ASPxLabel runat="server" Text="نحوه پرداخت" ID="ASPxLabel27">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="right" dir="ltr" valign="top" width="35%">
                                                                    <TSPControls:CustomAspxComboBox runat="server"  Width="100%" 
                                                                        ID="cmbaType" ValueType="System.String" SelectedIndex="0" ClientInstanceName="aType"
                                                                         RightToLeft="True">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {    
	if(aType.GetValue()=='1')
		SetFiche();
	else
		SetPose();
}"></ClientSideEvents>
                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                            <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                            </ErrorImage>
                                                                            <RequiredField IsRequired="True" ErrorText="نوع را انتخاب نمایید"></RequiredField>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                        <Items>
                                                                            <dxe:ListEditItem Value="5" Text="ثبت نام الکترونیک  از طریق موسسه" Selected="True"></dxe:ListEditItem>                                                                            
                                                                            <dxe:ListEditItem Value="1" Text="فیش" Selected="True"></dxe:ListEditItem>
                                                                            <dxe:ListEditItem Value="3" Text="دستگاه کارت خوان"></dxe:ListEditItem>
                                                                        </Items>
                                                                        <ButtonStyle Width="13px">
                                                                        </ButtonStyle>
                                                                    </TSPControls:CustomAspxComboBox>
                                                                </td>
                                                                <td align="right" valign="top" width="15%">
                                                                    <dxe:ASPxLabel runat="server" Text="بابت" ID="ASPxLabel2">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="right" dir="ltr" valign="top" width="35%">
                                                                    <TSPControls:CustomAspxComboBox runat="server"  Width="100%" Enabled="False"
                                                                         ID="cmbAccType" ValueType="System.String"
                                                                        SelectedIndex="0"  RightToLeft="True">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                            <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                            </ErrorImage>
                                                                            <RequiredField ErrorText=""></RequiredField>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                        <Items>
                                                                            <dxe:ListEditItem Value="12" Text="هزینه ثبت نام دوره آموزشی" Selected="true"></dxe:ListEditItem>
                                                                        </Items>
                                                                        <ButtonStyle Width="13px">
                                                                        </ButtonStyle>
                                                                    </TSPControls:CustomAspxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="top">
                                                                    <dxe:ASPxLabel runat="server" Text="شماره فیش" ID="lblFishNo" ClientInstanceName="lblFishNo">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="right" valign="top" dir="ltr">
                                                                    <TSPControls:CustomTextBox runat="server" ID="txtaNumber"  Width="100%" >
                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                            </ErrorImage>
                                                                            <RequiredField IsRequired="false" ErrorText="شماره را وارد نمایید"></RequiredField>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td align="right" valign="top">
                                                                    <dxe:ASPxLabel runat="server" Text="تاریخ" ID="ASPxLabel32">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="right" valign="top">
                                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                                        Width="245px" ShowPickerOnTop="True" ID="txtaDate" PickerDirection="ToRight"
                                                                        IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right" RightToLeft="False"></pdc:PersianDateTextBox>
                                                                    <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                                        ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtaDate" ID="PersianDateValidator2">تاریخ را انتخاب نمایید</pdc:PersianDateValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="top">
                                                                    <dxe:ASPxLabel runat="server" Text="مبلغ(ريال)" ID="ASPxLabel33">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="right" valign="top">
                                                                    <TSPControls:CustomTextBox runat="server" ID="txtaAmount" ClientInstanceName="txtaAmount" Enabled="false"
                                                                         Width="100%" >
                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                            </ErrorImage>
                                                                            <RequiredField IsRequired="True" ErrorText="مبلغ را وارد نمایید"></RequiredField>
                                                                            <RegularExpression ErrorText="مبلغ را صحیح وارد نمایید" ValidationExpression="[1-9]\d*"></RegularExpression>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td align="right" valign="top"></td>
                                                                <td align="right" valign="top"></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" valign="top">
                                                                    <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel6">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td align="right" colspan="3" valign="top">
                                                                    <TSPControls:CustomASPXMemo runat="server" Height="30px" ID="txtaDesc"  Width="100%"
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
                                                        </table>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </TSPControls:CustomASPxRoundPanel>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="ASPxRoundPanel1" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew2" runat="server" AutoPostBack="False" CausesValidation="False"
                                                 EnableTheming="False" EnableViewState="False"
                                                OnClick="btnNew_Click" Text=" " ToolTip="جدید" UseSubmitBehavior="False" Width="25px">
                                                <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" AutoPostBack="False" CausesValidation="False"
                                                 EnableTheming="False" EnableViewState="False"
                                                OnClick="btnEdit_Click" Text=" " ToolTip="ویرایش" UseSubmitBehavior="False" Width="25px">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server" AutoPostBack="False" 
                                                EnableTheming="False" EnableViewState="False" OnClick="btSave_Click" Text=" "
                                                ToolTip="ذخیره" UseSubmitBehavior="False" Width="25px">
                                                <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>

                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server" CausesValidation="False" 
                                                EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                ToolTip="بازگشت" UseSubmitBehavior="False">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>


                <dxhf:ASPxHiddenField ID="HiddenFieldCourseRegister" runat="server" ClientInstanceName="HiddenFieldCourseRegister">
                </dxhf:ASPxHiddenField>
          
            </dxp:PanelContent>

        </PanelCollection>
    </TSPControls:CustomAspxCallbackPanel>

</asp:Content>
