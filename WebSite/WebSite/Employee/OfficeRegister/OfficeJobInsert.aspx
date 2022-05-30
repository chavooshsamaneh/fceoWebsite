<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeJobInsert.aspx.cs" Inherits="Employee_OfficeRegister_OfficeJobInsert"
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
<%@ Register Src="~/UserControl/OfficeInfoUserControl.ascx" TagName="OfficeInfoUserControl"
    TagPrefix="UserControlOfficeInfo" %>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">

                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">

                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ValidationGroup="j" ID="btnSave" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click">

                                            <Image Url="~/Images/icons/save.png">
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
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">

                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server"
                OnItemClick="ASPxMenu1_ItemClick">
                <Items>
                    <dxm:MenuItem Name="Job" Text="مشخصات سابقه کاری" Selected="True">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="JobQuality" Text="مطلوبیت کار">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>

            <br />
            <UserControlOfficeInfo:OfficeInfoUserControl ID="OfficeInfoUserControl" runat="server" />
            <div align="right">
                <ul class="HelpUL">
                    <li><b>مطلوبیت کار</b> تنها جهت سوابق کاری با سمت "مجری" و یا "نماینده مجری" قابل ثبت می باشد.</li>
                </ul>
            </div>
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>


                        <table dir="rtl" width="100%">
                            <tbody>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="نام پروژه *" ID="ASPxLabel9">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" colspan="3">
                                        <TSPControls:CustomTextBox runat="server"  ID="txtjPrName"
                                            ClientInstanceName="TextPrName">
                                            <ValidationSettings Display="Dynamic" ErrorText="" ValidationGroup="j" ErrorTextPosition="Bottom">
                                               
                                                <RequiredField IsRequired="True" ErrorText="نام پروژه را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="نام کارفرما *" ID="ASPxLabel11">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" colspan="3">
                                        <TSPControls:CustomTextBox runat="server"  ID="txtjEmployer"
                                            ClientInstanceName="TextEmployer">
                                            <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                ErrorTextPosition="Bottom">
                                                
                                                <RequiredField IsRequired="True" ErrorText="نام کارفرما را وارد نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="نوع پروژه *" ID="ASPxLabel8">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" dir="ltr">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="Name" ID="CombojPrType" DataSourceID="OdbPrType"
                                            ValueType="System.String" ValueField="PrtId" ClientInstanceName="CmbPrType"
                                            EnableIncrementalFiltering="True">
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
                                              
                                                <RequiredField IsRequired="True" ErrorText="نوع پروژه را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="نوع سازه *" ID="ASPxLabel10" ClientInstanceName="lbl3"
                                            ClientVisible="False">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" dir="ltr">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="Name" ID="CombojSazeType" DataSourceID="OdbSazeType"
                                            ValueType="System.String" ValueField="SztId" ClientInstanceName="CmbSazeType"
                                            ClientVisible="False" EnableIncrementalFiltering="True">
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
                                        <dxe:ASPxLabel runat="server" Text="سمت *" ID="ASPxLabel14">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" dir="ltr">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="PName" ID="ComboPosition" DataSourceID="OdbJobPosition"
                                            ValueType="System.String" ValueField="PJPId" ClientInstanceName="CmbPosition"
                                            EnableIncrementalFiltering="True">
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {

  // if(CmbPosition.GetValue()=='8' || CmbPosition.GetValue()=='9')
	//	rdpJob.SetVisible(true);
	//else
		//rdpJob.SetVisible(false);
}"></ClientSideEvents>
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
                                        <dxe:ASPxLabel runat="server" Text="نحوه مشارکت *" ID="ASPxLabel24">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" dir="ltr">
                                        
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="CorName" ID="CombojIsCorporate" DataSourceID="OdbCorType"
                                            ValueType="System.String" ValueField="CortId" ClientInstanceName="CmbCorporate"
                                            EnableIncrementalFiltering="True">
                                            <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="True" ErrorText="نحوه مشارکت را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <%--<Columns>
<dxe:ListBoxColumn  Caption="نام" FieldName="CorName"/>
</Columns>--%>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="کشور *" ID="ASPxLabel12" Width="70">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" dir="ltr">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="CounName" ID="CombojCountry" DataSourceID="ODBJobCountry"
                                            ValueType="System.String" ValueField="CounId" ClientInstanceName="CmbCountry"
                                            EnableIncrementalFiltering="True">
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
                                        <dxe:ASPxLabel runat="server" Text="شهر *" ID="ASPxLabel13">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server"   ID="txtjCity" ClientInstanceName="TextCity">
                                            <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="شهر را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ شروع پروژه *" ID="ASPxLabel16">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="185px" ShowPickerOnTop="True"
                                            ID="txtjStartDate" ShowPickerOnEvent="OnClick" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                            Style="direction: ltr" RightToLeft="False"></pdc:PersianDateTextBox>
                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                            ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtjStartDate" ID="PersianDateValidator1"
                                            ValidationGroup="j">تاریخ نامعتبر</pdc:PersianDateValidator>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="حجم پروژه" ID="ASPxLabel21">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server"  ID="txtjPrVolume"
                                            ClientInstanceName="TextVolume">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText=""></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ شروع همکاری *" ID="ASPxLabel17">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="185px" ShowPickerOnTop="True"
                                            ID="txtjCoStartDate" ShowPickerOnEvent="OnClick" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                            Style="direction: ltr" RightToLeft="False"></pdc:PersianDateTextBox>
                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                            ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtjCoStartDate" ID="PersianDateValidator2"
                                            ValidationGroup="j">تاریخ نامعتبر</pdc:PersianDateValidator>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ پایان همکاری *" ID="ASPxLabel19">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="185px" ShowPickerOnTop="True"
                                            ID="txtjCoEndDate" ShowPickerOnEvent="OnClick" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                            Style="direction: ltr" RightToLeft="False"></pdc:PersianDateTextBox>
                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                            ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtjCoEndDate" ID="PersianDateValidator3"
                                            ValidationGroup="j">تاریخ نامعتبر</pdc:PersianDateValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top"></td>
                                    <td align="right" colspan="2" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" ClientInstanceName="lblDateError" ClientVisible="False"
                                            ForeColor="Red" Text="محدوده تاریخ وارد شده صحیح نمی باشد">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top"></td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="وضعیت پروژه در زمان شروع همکاری" Width="110px"
                                            ID="ASPxLabel18">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server"   ID="txtjStartStatus"
                                            ClientInstanceName="TextSStatus">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="وضعیت پروژه در زمان پایان همکاری" Width="110px"
                                            ID="ASPxLabel20">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server"   ID="txtjEndStatus"
                                            ClientInstanceName="TextEStatus">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="زیربنا *" ClientVisible="False" ID="ASPxLabel22"
                                            ClientInstanceName="lbl1">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" ClientVisible="False"  
                                            ID="txtjArea" ClientInstanceName="TextArea">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="زبر بنا را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="مقدار زیربنا را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="تعداد طبقات *" ClientVisible="False" ID="ASPxLabel23"
                                            ClientInstanceName="lbl2">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" ClientVisible="False" 
                                            ID="txtjFloor" ClientInstanceName="TextFloor">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="تعداد طبقات را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="تعداد طبقات را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel25">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" colspan="3">
                                        <TSPControls:CustomASPXMemo  runat="server" Height="33px"   ID="txtjDesc"
                                            ClientInstanceName="TextDesc">
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <TSPControls:CustomASPxRoundPanel ID="RoundPanelJudge" HeaderText="نظر کارشناسی" runat="server"
                            Visible="False">
                            <PanelCollection>
                                <dxp:PanelContent>



                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="4">
                                                    <dxe:ASPxRadioButtonList runat="server"  ID="rdbtnIsConfirm">
                                                        <Border BorderWidth="0px"></Border>
                                                        <Items>
                                                            <dxe:ListEditItem Value="0" Text="مورد تایید نمی باشد"></dxe:ListEditItem>
                                                            <dxe:ListEditItem Value="1" Text="مورد تایید می باشد"></dxe:ListEditItem>
                                                        </Items>
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="وضعیت را مشخص نمایید"></RequiredField>
                                                        </ValidationSettings>
                                                    </dxe:ASPxRadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="3">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtMeetingId" Width="76px">
                                                        <ValidationSettings>
                                                            
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td style="vertical-align: top" dir="rtl" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="شماره جلسه" Width="69px" ID="ASPxLabel6">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="3">
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="180px" ShowPickerOnTop="True"
                                                        ID="txtMeetingDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                                        Style="direction: ltr;"></pdc:PersianDateTextBox>
                                                </td>
                                                <td style="vertical-align: top" dir="rtl" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="تاریخ جلسه" ID="ASPxLabel7">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="3">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtGrade" >
                                                        <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                            
                                                            <RequiredField IsRequired="True" ErrorText="مبلغ مورد قبول کارشناس را وارد نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td style="vertical-align: top" dir="rtl" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="متراژ" ID="ASPxLabel2">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top" dir="rtl" align="right" colspan="3">
                                                    <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtViewPoint">
                                                        <ValidationSettings>
                                                            
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomASPXMemo>
                                                </td>
                                                <td style="vertical-align: top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="نظر کارشناسی" Width="82px" ID="ASPxLabel3">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </TSPControls:CustomASPxRoundPanel>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">
                                          
                                            <image url="~/Images/icons/new.png">
                                                                            </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                           
                                            <image url="~/Images/icons/edit.png">
                                                                            </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ValidationGroup="j" ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click">
                                          
                                            <image url="~/Images/icons/save.png">
                                                                            </image>
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
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                           
                                            <image url="~/Images/icons/Back.png">
                                                                            </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
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
    <asp:HiddenField ID="OfficeId" runat="server" Visible="False" />
    <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False" />
    <asp:HiddenField ID="JobId" runat="server" Visible="False" />
    <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
    <dxhf:ASPxHiddenField ID="HDFlpMember" runat="server" ClientInstanceName="flpme">
    </dxhf:ASPxHiddenField>
    <asp:ObjectDataSource ID="ODBJobCountry" runat="server"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.CountryManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbPrType" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.ProjectTypeManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbSazeType" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.SazeTypeManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbCorType" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.CorporationTypeManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbJobPosition" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.ProjectJobPositionManager"></asp:ObjectDataSource>
    <asp:HiddenField ID="HDMode" runat="server" Visible="False" />
    <asp:HiddenField ID="HDJudgeId" runat="server" Visible="False" />
    <dxhf:ASPxHiddenField ID="HiddenFieldOffice" runat="server">
    </dxhf:ASPxHiddenField>

</asp:Content>
