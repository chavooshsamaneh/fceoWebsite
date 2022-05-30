<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddCourseRegister.aspx.cs" Inherits="Institue_Amoozesh_AddCourseRegister"
    Title="ثبت نام دوره های آموزشی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetControlValues() {

            grid.GetRowValues(grid.GetFocusedRowIndex(), 'MeId;FirstName;LastName;FatherName;IdNo;SSN;HomeTel;HomeAdr;MobileNo;BirhtDate;BirthPlace', SetValue);
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
            txtBirthPlaceClient.SetText(values[10]);
            document.getElementById('<%=txtBrithDate.ClientID%>').value = values[9];
            HiddenFieldCourseRegister.Set('MeId', values[0]);
        }


        function SetPeriodControlValues() {
            gridPeriod.GetRowValues(gridPeriod.GetFocusedRowIndex(), 'PPId;PName;Capacity;InsId;StartDate;EndDate;InsName;InActiveName;PeriodTitle;PeriodStatus;PPType;Address;CountRegister;PType;StartRegisterDate;EndRegisterDate;TestDate;PeriodCost;TestCost;PPCode', SetPeriodValue);
        }

        function SetPeriodValue(values) {
            txtPeriodTittle.SetText(values[8] + '(' + values[19] + ')');
            if (cmbRegisterType.GetSelectedIndex() == 0) {
                txtCost.SetText(values[17]);
            }
            else {
                txtCost.SetText(values[18]);
            }
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
            //alert(flag);
            txtMeNo.SetText('');
            txtSSN.SetText('');
            txtName.SetText('');
            txtLastName.SetText('');
            txtName.SetText('');
            txtFatherName.SetText('');
            txtIdNo.SetText('');
            txtTel.SetText('');
            txtMobileNo.SetText('');
            txtAddress.SetText('');
            txtBirthPlace.SetText('');
            txtDesc.SetText('');
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
            txtAddress.SetEnabled(flag);
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
    </script>   
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
               
                    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                        [<a class="closeLink" href="#">بستن</a>]</div>
                 <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                        <table >
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew" runat="server" AutoPostBack="False" CausesValidation="False"
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
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave" runat="server" AutoPostBack="False" 
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
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
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
                   	<TSPControls:CustomASPxRoundPanel ID="RoundPanelRegister" HeaderText="مشاهده" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
        
                                <table dir="rtl" id="Table2" cellpadding="1" width="100%">
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="right" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="وضعیت عضویت" Width="100%" ID="ASPxLabel17">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td dir="ltr" valign="top" align="right" width="35%">
                                                <TSPControls:CustomAspxComboBox runat="server"  Width="100%" 
                                                    ID="cmbMemberType" ValueType="System.String" Height="21px" ClientInstanceName="cmbMemberType"
                                                    >
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
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <Items>
                                                        <dxe:ListEditItem Value="0" Text="عضو نظام"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Value="1" Text="دیگر اشخاص"></dxe:ListEditItem>
                                                    </Items>
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td dir="ltr" valign="top" align="right" width="15%">
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="کد عضویت *" ID="lblMeId" ClientInstanceName="lblMeId"
                                                    Width="100%" Visible="False">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMeNo"  Width="100%" AutoPostBack="True"
                                                    ClientInstanceName="txtMeNo" 
                                                    Visible="False" OnTextChanged="txtMeNo_TextChanged">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                        <RegularExpression ErrorText="کد عضویت را با فرمت صحیح وارد نمایید" ValidationExpression="\d*" />
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                            </td>
                                            <td valign="top" align="right">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نام" ID="lblMeFirstName" Width="100%" ClientInstanceName="lblMname">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtName"  Width="100%" ClientInstanceName="txtName"
                                                    >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText="نام را وارد نمایید" IsRequired="True"></RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="lblMeLastName" Width="100%"
                                                    ClientInstanceName="lblMfamily">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtLastName"  Width="100%"
                                                    ClientInstanceName="txtLastName" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField ErrorText="نام خانوادگی را وارد نمایید" IsRequired="True"></RequiredField>
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
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFatherName"  EnableClientSideAPI="True"
                                                    Width="100%" ClientInstanceName="txtFatherName" >
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="تاریخ تولد" Width="100%" ID="ASPxLabel11">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="245px" ShowPickerOnTop="True"
                                                    ID="txtBrithDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                                    Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                <asp:RequiredFieldValidator runat="server" ErrorMessage="تاریخ تولد" ToolTip="لطفاً این فیلد را پر کنید."
                                                    ControlToValidate="txtBrithDate" ID="RequiredFieldValidator8" Display="Dynamic">لطفاً این فیلد را پر کنید.</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="محل تولد" Width="100%" ID="ASPxLabel4">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtBirthPlace"  EnableClientSideAPI="True"
                                                    Width="100%" ClientInstanceName="txtBirthPlace" >
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="شماره شناسنامه" Width="100%" ID="ASPxLabel8">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtIdNo"  EnableClientSideAPI="True"
                                                    Width="100%" ClientInstanceName="txtIdNo" >
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                        ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="\d{0,10}">
                                                        </RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td dir="ltr" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="* کد ملی" ID="ASPxLabel12" Width="100%">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtSSN"  EnableClientSideAPI="True"
                                                    Width="100%" MaxLength="10" ClientInstanceName="txtSSN" >
                                                    <MaskSettings IncludeLiterals="DecimalSymbol"></MaskSettings>
                                                    <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                        ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="\d{10}">
                                                        </RegularExpression>
                                                        <RequiredField ErrorText="کد ملی را وارد نمایید" IsRequired="True" />
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="شماره تلفن" Width="100%" ID="ASPxLabel9">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtTel"  EnableClientSideAPI="True"
                                                    Width="100%" MaxLength="12" ClientInstanceName="txtTel" >
                                                    <ValidationSettings Display="Dynamic" ErrorText="شماره تلفن به صورت چهار رقم پیش شماره و هفت رقم شماره تلفن می باشد(07116360332)"
                                                        ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="شماره تلفن به صورت چهار رقم پیش شماره و هفت رقم شماره تلفن می باشد(07116360332)"
                                                            ValidationExpression="0\d{8,11}"></RegularExpression>
                                                        <ErrorFrameStyle Wrap="True">
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td dir="ltr" valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="شماره تلفن همراه" Width="100px" ID="ASPxLabel13">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMobileNo"  EnableClientSideAPI="True"
                                                    Width="100%" MaxLength="11" ClientInstanceName="txtMobileNo" >
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                        ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید." ValidationExpression="0\d{10}">
                                                        </RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                            </td>
                                            <td dir="ltr" valign="top" align="right">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" dir="ltr" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="* استان" Width="100%" ID="ASPxLabel10">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" dir="ltr" valign="top">
                                                <TSPControls:CustomAspxComboBox ID="cmbProvince" runat="server" 
                                                     DataSourceID="ObjdsProvince" 
                                                    TextField="PrName" ValueField="PrId" ValueType="System.String" 
                                                    Width="100%" ClientInstanceName="cmbProvince">
                                                    <ValidationSettings ErrorTextPosition="Bottom">
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                        <RequiredField ErrorText="استان را انتخاب نمایید" IsRequired="True" />
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                                <asp:ObjectDataSource ID="ObjdsProvince" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
                                                    SelectMethod="GetData" TypeName="TSP.DataManager.ProvinceManager" UpdateMethod="Update">
                                                    <DeleteParameters>
                                                        <asp:Parameter Name="Original_PrId" Type="Int32" />
                                                        <asp:Parameter Name="Original_LastTimeStamp" Type="Object" />
                                                    </DeleteParameters>
                                                    <InsertParameters>
                                                        <asp:Parameter Name="NezamCode" Type="Int32" />
                                                        <asp:Parameter Name="PrName" Type="String" />
                                                        <asp:Parameter Name="CounId" Type="Int32" />
                                                        <asp:Parameter Name="UserId" Type="Int32" />
                                                        <asp:Parameter Name="ModifiedDate" Type="DateTime" />
                                                    </InsertParameters>
                                                    <UpdateParameters>
                                                        <asp:Parameter Name="NezamCode" Type="Int32" />
                                                        <asp:Parameter Name="PrName" Type="String" />
                                                        <asp:Parameter Name="CounId" Type="Int32" />
                                                        <asp:Parameter Name="UserId" Type="Int32" />
                                                        <asp:Parameter Name="ModifiedDate" Type="DateTime" />
                                                        <asp:Parameter Name="Original_PrId" Type="Int32" />
                                                        <asp:Parameter Name="Original_LastTimeStamp" Type="Object" />
                                                        <asp:Parameter Name="PrId" Type="Int32" />
                                                    </UpdateParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="شماره عضویت*" ID="lblMeNoOtp" ClientInstanceName="lblMeNoOtp"
                                                    Width="100%">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" dir="ltr" valign="top">
                                                <TSPControls:CustomTextBox IsMenuButton="true" ID="txtMeNoOtp" runat="server" 
                                                      Width="100%"
                                                    ClientInstanceName="txtMeNoOtp">
                                                    <ValidationSettings ErrorTextPosition="Bottom">
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                        <RequiredField ErrorText="کد عضویت را وارد نمایید" IsRequired="True" />
                                                        <RegularExpression ErrorText="کدعضویت عدد صحیح می باشد" ValidationExpression="\d*" />
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" dir="ltr" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="رشته تحصیلی*" Width="100%" ID="ASPxLabel16">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" dir="ltr" valign="top">
                                                <TSPControls:CustomAspxComboBox ID="cmbMajor" runat="server" 
                                                     DataSourceID="ObjdsMajor" 
                                                    TextField="MjName" ValueField="MjId" ValueType="System.String" 
                                                    Width="100%" ClientInstanceName="cmbMajor">
                                                    <ValidationSettings ErrorTextPosition="Bottom">
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                        <RequiredField ErrorText="رشته تحصیلی را انتخاب نمایید" IsRequired="True" />
                                                    </ValidationSettings>
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                                <asp:ObjectDataSource ID="ObjdsMajor" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager">
                                                </asp:ObjectDataSource>
                                            </td>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="شماره پروانه اشتغال" Width="100%" ID="ASPxLabel19">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" dir="ltr" valign="top">
                                                <TSPControls:CustomTextBox IsMenuButton="true" ID="txtFileNo" runat="server" 
                                                      Width="100%"
                                                    ClientInstanceName="txtFileNo">
                                                    <ValidationSettings>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
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
                                            <td align="right" dir="ltr" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="پایه و صلاحیت" Width="100px" ID="ASPxLabel18">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" dir="rtl" valign="top" colspan="3">
                                                <TSPControls:CustomTextBox IsMenuButton="true" ID="txtGrade" runat="server" 
                                                      Width="100%"
                                                    ClientInstanceName="txtGrade">
                                                    <ValidationSettings>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Width="100%" Text="آدرس" ClientVisible="false" ID="ASPxLabel3">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" colspan="3">
                                                <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtAddress"  ClientVisible="false" Width="100%"
                                                    ClientInstanceName="txtAddress" >
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
                                            <td valign="top" align="right" style="height: 46px">
                                                <dxe:ASPxLabel runat="server" Text="توضیحات" Width="100%" ID="ASPxLabel15">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" colspan="3" style="height: 46px">
                                                <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtDesc"  EnableClientSideAPI="True"
                                                    Width="100%" ClientInstanceName="txtDesc" >
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
                                            <td dir="rtl" valign="top" align="right" colspan="5">
                                                <table width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td align="right" valign="top" width="95%">
                                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtPeriodTittle"  Width="100%"
                                                                    ReadOnly="True" ClientInstanceName="txtPeriodTittle" >
                                                                    <ValidationSettings ErrorTextPosition="Bottom">
                                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                        <RequiredField ErrorText="دوره را انتخاب نمایید" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                            <td dir="ltr" width="5%">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server"  ToolTip="جستجو" CausesValidation="False"
                                                                    ID="btnSearchPeriod" EnableClientSideAPI="True" AutoPostBack="False" UseSubmitBehavior="False"
                                                                    EnableViewState="False" EnableTheming="False">
                                                                    <ClientSideEvents Click="function(s, e) {
	popUpPeriods.Show();
}"></ClientSideEvents>
                                                                    <Image  Url="~/Images/icons/Search.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <dxe:ASPxLabel runat="server" Text="نوع ثبت نام" Width="100%" ID="lblRegisterType"
                                                    ClientInstanceName="lblRegisterType">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" dir="ltr" valign="top">
                                                <TSPControls:CustomAspxComboBox runat="server"  Width="100%" 
                                                    ID="cmbRegisterType" ValueType="System.String" ClientInstanceName="cmbRegisterType"
                                                    >
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(cmbRegisterType.GetSelectedIndex()==0)
{
    txtCost.SetText(HiddenFieldCourseRegister.Get('PeriodCost'));

}
else
{
	 txtCost.SetText(HiddenFieldCourseRegister.Get('TestCost'));
}

}"></ClientSideEvents>
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <Items>
                                                        <dxe:ListEditItem Value="0" Text=" دوره و آزمون"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Value="1" Text="آزمون"></dxe:ListEditItem>
                                                    </Items>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td align="right" valign="top">
                                            </td>
                                            <td align="right" colspan="3" dir="ltr" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="هزینه دوره" Width="100%" ID="ASPxLabel5">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td dir="rtl" valign="top" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtCost"  EnableClientSideAPI="True"
                                                    Width="100%" MaxLength="11" ReadOnly="True" ClientInstanceName="txtCost" >
                                                    <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                        ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="هزینه دوره نا مشخص می باشد."></RequiredField>
                                                        <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."></RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="شماره فیش" Width="100%" ID="ASPxLabel6">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td dir="rtl" valign="top" align="right" colspan="3">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="ASPxTextBoxFicheCode"  EnableClientSideAPI="True"
                                                    Width="100%" MaxLength="11" >
                                                    <ValidationSettings Display="Dynamic" ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."
                                                        ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="شماره فیش را وارد نمایید."></RequiredField>
                                                        <RegularExpression ErrorText="لطفاً این فیلد را با فرمت صحیح وارد نمائید."></RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نحوه پرداخت" Width="100%" ID="ASPxLabel2" Visible="False">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td dir="ltr" valign="top" align="right" colspan="3">
                                                <TSPControls:CustomAspxComboBox runat="server" Visible="False"  Width="100%"
                                                     ID="cmbPaymentType" ValueType="System.String"
                                                    >
                                                    <ValidationSettings>
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <Items>
                                                        <dxe:ListEditItem Value="نقدی" Text="نقدی"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Value="فیش بانکی" Text="فیش بانکی"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Value="کارت" Text="کارت"></dxe:ListEditItem>
                                                    </Items>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                        </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel> 
                    <br />
                      <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
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
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
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
                                
                    <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl2" runat="server"  
                        HeaderText="جستجو" Height="269px"  ClientInstanceName="popUpPeriods" PopupElementID="btnSearch1" Width="600px">
                        <ContentCollection>
                            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                           
                                    <TSPControls:CustomAspxDevGridView ID="GridViewCourseRegister" runat="server" AutoGenerateColumns="False"
                                        ClientInstanceName="gridPeriod" 
                                         DataSourceID="ObjdsPeriodPresent" KeyFieldName="PPId" Width="100%"
                                        OnAutoFilterCellEditorInitialize="GridViewCourseRegister_AutoFilterCellEditorInitialize"
                                        OnHtmlDataCellPrepared="GridViewCourseRegister_HtmlDataCellPrepared" RightToLeft="True">
                                        <ClientSideEvents RowDblClick="function(s, e) {
	popUpPeriods.Hide();
	SetPeriodControlValues();
}" />                                       
                                        <Columns>
                                            <dxwgv:GridViewDataTextColumn FieldName="PPId" Visible="False" VisibleIndex="1">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="نام دوره" FieldName="PeriodTitle" VisibleIndex="0"
                                                Width="300px">
                                                <CellStyle Wrap="False">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="کد دوره" FieldName="PPCode" VisibleIndex="1">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="نحوه برگزاری" FieldName="PPType" VisibleIndex="2">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="موسسه" FieldName="InsName" VisibleIndex="2">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="تاریخ شروع دوره" FieldName="StartDate" VisibleIndex="3">
                                                <CellStyle HorizontalAlign="Right" Wrap="False">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان دوره" FieldName="EndDate" VisibleIndex="4">
                                                <CellStyle HorizontalAlign="Right" Wrap="False">
                                                </CellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="تاریخ امتحان" FieldName="TestDate" Visible="False"
                                                VisibleIndex="5">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="ثبت نام کنندگان" FieldName="CountRegister"
                                                VisibleIndex="5">
                                                <FooterCellStyle Wrap="True">
                                                </FooterCellStyle>
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="شروع ثبت نام" FieldName="StartRegisterDate"
                                                Visible="False" VisibleIndex="5">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="پایان ثبت نام" FieldName="EndRegisterDate"
                                                Visible="False" VisibleIndex="5">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="ظرفیت" FieldName="Capacity" VisibleIndex="6">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="هزینه دوره" FieldName="PeriodCost" VisibleIndex="7">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="هزینه آزمون" FieldName="TestCost" VisibleIndex="8">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="9" Width="30px" ShowClearFilterButton="true">
                                            </dxwgv:GridViewCommandColumn>
                                        </Columns>
                                    </TSPControls:CustomAspxDevGridView>
                                <asp:ObjectDataSource ID="ObjdsPeriodPresent" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="SelectPeriodsAndSeminars" TypeName="TSP.DataManager.PeriodPresentManager">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="-1" Name="PPId" Type="Int32" />
                                        <asp:Parameter DefaultValue="-1" Name="InsId" Type="Int32" />
                                        <asp:Parameter DefaultValue="%" Name="TodayDate" Type="String" />
                                        <asp:Parameter DefaultValue="-1" Name="ISOutTimePeriodReg" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </dxpc:PopupControlContentControl>
                        </ContentCollection>
                      
                    </TSPControls:CustomASPxPopupControl>
       
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
</asp:Content>
