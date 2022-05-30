<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeJobShow.aspx.cs" Inherits="Office_OfficeInfo_OfficeJobShow"
    Title="مشخصات سابقه کاری" %>

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
    <script language="javascript">
        function CheckDate() {
            var StartDate = document.getElementById('<%=txtjCoStartDate.ClientID%>').value;
            var EndDate = document.getElementById('<%=txtjCoEndDate.ClientID%>').value;
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
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                    cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                    CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="BtnNew_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/new.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                    CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                                                    EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/edit.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                                    ValidationGroup="j" ID="btnSave" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnSave_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/save.png">
                                                                    </Image>
                                                                    <ClientSideEvents Click="function(s, e) {
	if(CheckDate()==-1)
	{
		e.processOnServer=false;
		lblDateError.SetVisible(true);
	}
	else
		lblDateError.SetVisible(false);
}" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                    CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnBack_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/Back.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                               
  </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>           
                <br />
             <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                 
                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td align="right" valign="top" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="نام پروژه" ID="ASPxLabel9">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="3" align="right" valign="top">
                                            <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtjPrName" ClientInstanceName="TextPrName"
                                                >
                                                <ValidationSettings Display="Dynamic" ErrorText="" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نام پروژه را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="نام کارفرما" ID="ASPxLabel11">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="3" align="right" valign="top">
                                            <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtjEmployer"
                                                ClientInstanceName="TextEmployer" >
                                                <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                    ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نام کارفرما را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="نوع پروژه" ID="ASPxLabel8">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top" width="35%">
                                            <TSPControls:CustomAspxComboBox runat="server" 
                                                TextField="Name" ID="CombojPrType"  DataSourceID="OdbPrType"
                                                Width="100%" ValueType="System.String" ValueField="PrtId" ClientInstanceName="CmbPrType"
                                                RightToLeft="True" >
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
   if(CmbPrType.GetValue() == '1')
	{
	TextArea.SetVisible(true);
	TextFloor.SetVisible(true);
	lbl1.SetVisible(true);
	lbl2.SetVisible(true);
	CmbSazeType.SetVisible(true);
	lbl3.SetVisible(true);
	}
	else
	{
	TextArea.SetVisible(false);
	TextFloor.SetVisible(false);
	lbl1.SetVisible(false);
	lbl2.SetVisible(false);
	CmbSazeType.SetVisible(false);
	lbl3.SetVisible(false);
	}
}"></ClientSideEvents>
                                                <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                    ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نوع پروژه را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td align="right" valign="top" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="نوع سازه" ID="ASPxLabel10" ClientInstanceName="lbl3"
                                                ClientVisible="False">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top" width="35%">
                                            <TSPControls:CustomAspxComboBox runat="server" 
                                                TextField="Name" ID="CombojSazeType"  DataSourceID="OdbSazeType"
                                                ValueType="System.String" ValueField="SztId" ClientInstanceName="CmbSazeType"
                                                 ClientVisible="False" Width="100%">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                    ErrorTextPosition="Bottom">
                                                  
                                                    <RequiredField IsRequired="True" ErrorText="نوع سازه را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="سمت" ID="ASPxLabel14">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxComboBox runat="server" 
                                                TextField="PName" ID="ComboPosition"  DataSourceID="OdbJobPosition"
                                                ValueType="System.String" ValueField="PJPId" ClientInstanceName="CmbPosition"
                                                Width="100%">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                    ErrorTextPosition="Bottom">
                                                  
                                                    <RequiredField IsRequired="True" ErrorText="سمت را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="نحوه مشارکت" ID="ASPxLabel24">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxComboBox runat="server" 
                                                TextField="CorName" ID="CombojIsCorporate"  DataSourceID="OdbCorType"
                                                ValueType="System.String" ValueField="CortId" ClientInstanceName="CmbCorporate"
                                                Width="100%">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                    ErrorTextPosition="Bottom">
                                                   
                                                    <RequiredField IsRequired="True" ErrorText="نحوه مشارکت را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="کشور" ID="ASPxLabel12">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxComboBox runat="server" 
                                                TextField="CounName" ID="CombojCountry"  DataSourceID="ODBJobCountry"
                                                ValueType="System.String" ValueField="CounId" ClientInstanceName="CmbCountry"
                                                 Width="100%">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                    ErrorTextPosition="Bottom">
                                                    
                                                    <RequiredField IsRequired="True" ErrorText="کشور را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="شهر" ID="ASPxLabel13">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtjCity" ClientInstanceName="TextCity"
                                                >
                                                <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                    ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="شهر را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ شروع پروژه" ID="ASPxLabel16">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                Width="230px" ShowPickerOnTop="True" ID="txtjStartDate" PickerDirection="ToRight"
                                                IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                            <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ValidationGroup="j"
                                                ClientValidationFunction="PersianDateValidator" ErrorMessage="تاریخ نامعتبر"
                                                ControlToValidate="txtjStartDate" ID="PersianDateValidator1">تاریخ نامعتبر</pdc:PersianDateValidator>
                                        </td>
                                        <td align="right" valign="top">
                                            &nbsp;<dxe:ASPxLabel runat="server" Text="حجم پروژه" ID="ASPxLabel21">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtjPrVolume"
                                                ClientInstanceName="TextVolume" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=""></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ شروع همکاری" ID="ASPxLabel17">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                Width="230px" ShowPickerOnTop="True" ID="txtjCoStartDate" PickerDirection="ToRight"
                                                IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                            <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ValidationGroup="j"
                                                ClientValidationFunction="PersianDateValidator" ErrorMessage="تاریخ نامعتبر"
                                                ControlToValidate="txtjCoStartDate" ID="PersianDateValidator2">تاریخ نامعتبر</pdc:PersianDateValidator>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ پایان همکاری" ShowPickerOnEvent="OnClick"
                                                ID="ASPxLabel19">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="230px" ShowPickerOnTop="True"
                                                ID="txtjCoEndDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                            <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ValidationGroup="j"
                                                ClientValidationFunction="PersianDateValidator" ErrorMessage="تاریخ نامعتبر"
                                                ControlToValidate="txtjCoEndDate" ID="PersianDateValidator3">تاریخ نامعتبر</pdc:PersianDateValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                        </td>
                                        <td align="right" colspan="2" valign="top">
                                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" ClientInstanceName="lblDateError" ClientVisible="False"
                                                ForeColor="Red" Text="محدوده تاریخ وارد شده صحیح نمی باشد">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="وضعیت پروژه در زمان شروع همکاری" Width="110px"
                                                ID="ASPxLabel18">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtjStartStatus"
                                                ClientInstanceName="TextSStatus" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="وضعیت پروژه در زمان پایان همکاری" Width="110px"
                                                ID="ASPxLabel20">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox runat="server"  Width="100%" ID="txtjEndStatus"
                                                ClientInstanceName="TextEStatus" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="زیربنا" ClientVisible="False" ID="ASPxLabel22"
                                                ClientInstanceName="lbl1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox runat="server"  ClientVisible="False" Width="100%"
                                                ID="txtjArea" ClientInstanceName="TextArea" >
                                                <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="زبر بنا را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="مقدار زیربنا را صحیح وارد نمایید" ValidationExpression="\d*">
                                                    </RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="تعداد طبقات" ClientVisible="False" ID="ASPxLabel23"
                                                ClientInstanceName="lbl2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomTextBox runat="server"  ClientVisible="False" Width="100%"
                                                ID="txtjFloor" ClientInstanceName="TextFloor" >
                                                <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="تعداد طبقات را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="تعداد طبقات را صحیح وارد نمایید" ValidationExpression="\d*">
                                                    </RegularExpression>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel25">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td colspan="3" align="right" valign="top">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px"  Width="100%" ID="txtjDesc"
                                                ClientInstanceName="TextDesc" >
                                            </TSPControls:CustomASPXMemo>
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
  
                                                <table >
                                                    <tbody>
                                                        <tr>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                    CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="BtnNew_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/new.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                    CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                                                    EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/edit.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                                    ValidationGroup="j" ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnSave_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/save.png">
                                                                    </Image>
                                                                    <ClientSideEvents Click="function(s, e) {
	if(CheckDate()==-1)
	{
		e.processOnServer=false;
		lblDateError.SetVisible(true);
	}
	else
		lblDateError.SetVisible(false);
}" />
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td >
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                    CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                                                    EnableTheming="False" OnClick="btnBack_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/Back.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                           </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
        <asp:HiddenField ID="OfficeId" runat="server" Visible="False" />
        <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False" />
        <asp:HiddenField ID="JobId" runat="server" Visible="False" />
        <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
        <dxhf:ASPxHiddenField ID="HDFlpMember" runat="server" ClientInstanceName="flpme">
        </dxhf:ASPxHiddenField>
        <asp:ObjectDataSource ID="ODBJobCountry" runat="server" CacheExpirationPolicy="Sliding"
            OldValuesParameterFormatString="original_{0}" SqlCacheDependency="NezamFars:tblCountry"
            EnableCaching="True" SelectMethod="GetData" TypeName="TSP.DataManager.CountryManager">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="OdbPrType" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="TSP.DataManager.ProjectTypeManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="OdbSazeType" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="TSP.DataManager.SazeTypeManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="OdbCorType" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="TSP.DataManager.CorporationTypeManager"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="OdbJobPosition" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="TSP.DataManager.ProjectJobPositionManager">
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HDMode" runat="server" Visible="False" />
        <asp:ObjectDataSource ID="OdbFactorDocuments" runat="server" FilterExpression="Type={0}"
            SelectMethod="GetData" TypeName="TSP.DataManager.DocOffOfficeFactorDocumentsManager">
            <FilterParameters>
                <asp:Parameter Name="newparameter" />
            </FilterParameters>
        </asp:ObjectDataSource>
</asp:Content>
