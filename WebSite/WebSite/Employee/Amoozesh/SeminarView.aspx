<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="SeminarView.aspx.cs" Inherits="Employee_Amoozesh_SeminarView"
    Title="مشخصات سمینار" %>

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
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetControlValues() {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'Name;Family;Father;SSN;TeId;TypeName', SetValue);
        }
        function SetValue(values) {
            TextName.SetText(values[0]);
            TextFamily.SetText(values[1]);
            TextFather.SetText(values[2]);
            TextSSN.SetText(values[3]);
            TextID.SetText(values[4]);
            TextTypeName.SetText(values[5]);


        }
        function SetControlValuesGrade() {
            Gradegrid.GetRowValues(Gradegrid.GetFocusedRowIndex(), 'GMRId;GrdName;ResName;MjName', SetValueGrade);
        }
        function SetValueGrade(values) {
            TextMajorID.SetText(values[0]);
            TextGrd.SetText(values[1]);
            TextRes.SetText(values[2]);
            TextMj.SetText(values[3]);


        }
        function SetControlValuesTe() {
            gridTe.GetRowValues(gridTe.GetFocusedRowIndex(), 'Name;Family;Father;IdNo;SSN;Tel;MobileNo;Email;Licence;Major;BirthDate;TeId;Description;Salary;Type', SetValueTe);
        }
        function SetValueTe(values) {
            TextName.SetText(values[0]);
            TextFamily.SetText(values[1]);
            TextFather.SetText(values[2]);
            TextIdNo.SetText(values[3]);
            TextSSN.SetText(values[4]);
            TextTel.SetText(values[5]);
            TextMobileNo.SetText(values[6]);
            TextEmail.SetText(values[7]);
            cmbLicence.SetValue(values[8]);
            cmbMajor.SetValue(values[9]);

            TextID.SetText(values[11]);
            TextTeDesc.SetText(values[12]);
            TextSal.SetText(values[13]);
            combo.SetValue(values[14]);
            gridfile.PerformCallback(values[11]);
        }

        function SetEmpty() {
            TextName.SetText("");
            TextFamily.SetText("");
            TextFather.SetText("");
            TextIdNo.SetText("");
            TextSSN.SetText("");
            TextTel.SetText("");
            TextMobileNo.SetText("");
            TextEmail.SetText("");
            cmbLicence.SetSelectedIndex(-1);
            cmbMajor.SetSelectedIndex(-1);
            TextTeDesc.SetText("");
            TextSal.SetText("");
        }

        function Enable() {
            TextName.SetEnabled(true);
            TextFamily.SetEnabled(true);
            TextFather.SetEnabled(true);
            TextIdNo.SetEnabled(true);
            TextSSN.SetEnabled(true);
            TextTel.SetEnabled(true);
            TextMobileNo.SetEnabled(true);
            TextEmail.SetEnabled(true);
            cmbLicence.SetEnabled(true);
            cmbMajor.SetEnabled(true);
        }
        function Disable() {
            TextName.SetEnabled(false);
            TextFamily.SetEnabled(false);
            TextFather.SetEnabled(false);
            TextIdNo.SetEnabled(false);
            TextSSN.SetEnabled(false);
            TextTel.SetEnabled(false);
            TextMobileNo.SetEnabled(false);
            TextEmail.SetEnabled(false);
            cmbLicence.SetEnabled(false);
            cmbMajor.SetEnabled(false);
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
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="ASPxRoundPanel1" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False"
                                            Text=" " EnableTheming="False" ToolTip="جدید"
                                            ID="BtnNew" EnableViewState="False" OnClick="BtnNew_Click">
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" "
                                            EnableTheming="False" ToolTip="ویرایش" ID="btnEdit" EnableViewState="False" OnClick="btnEdit_Click">
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                            EnableTheming="False" ToolTip="ذخیره" ID="btnSave" EnableViewState="False" OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
	     
     if(gridTe.GetVisibleRowsOnPage()==0)
    {
        alert('مشخصات سخنران را وارد نمایید');
         e.processOnServer=false;
          return;
    }    
     e.processOnServer=true;
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False"
                                            Text=" " EnableTheming="False" ToolTip="بازگشت"
                                            ID="btnBack" EnableViewState="False" OnClick="btnBack_Click">
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomAspxMenuHorizontal Visible="false" ID="AspxMenu1" runat="server"
                OnItemClick="AspxMenu1_ItemClick">
                <Items>
                    <dxm:MenuItem Name="Seminar" Text="مشخصات سمینار" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Costs" Text="هزینه های متفرقه">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>


            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <fieldset runat="server" id="FieldSetBaseInfo">
                            <legend class="fieldset-legend" dir="rtl"><b>اطلاعات پایه</b>
                            </legend>
                            <dxp:ASPxPanel runat="server" ID="RoundPanelBaseInfo" ClientInstanceName="RoundPanelBaseInfo">
                                <PanelCollection>
                                    <dxp:PanelContent>
                                        <table dir="rtl" width="100%" align="right">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="right" style="width: 15%">
                                                        <dxe:ASPxLabel runat="server" Text="موضوع سمینار *" ID="ASPxLabel10">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3" style="width: 85%">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"
                                                            ID="txtSubject">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                                <RequiredField IsRequired="True" ErrorText="موضوع را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right" style="width: 15%">
                                                        <dxe:ASPxLabel runat="server" Text="موسسه برگزار کننده*" Width="100%" ID="ASPxLabel30">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomAspxComboBox runat="server" PopupVerticalAlign="NotSet" Width="100%"
                                                            IncrementalFilteringMode="StartsWith" TextField="InsName" ID="cmbInstitue"
                                                            DataSourceID="ODBInstitue" EnableClientSideAPI="True"
                                                            ValueType="System.String" ValueField="InsId" ClientInstanceName="cmbInstitue"
                                                            EnableIncrementalFiltering="True"
                                                            RightToLeft="True">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Width="14px" Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" ErrorText="موسسه را انتخاب نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <ButtonStyle Width="13px">
                                                            </ButtonStyle>
                                                        </TSPControls:CustomAspxComboBox>
                                                        <asp:ObjectDataSource runat="server" SelectMethod="GetData" ID="ODBInstitue" TypeName="TSP.DataManager.InstitueManager"></asp:ObjectDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right" style="width: 15%">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ شروع برگزاری " Width="100%" ID="ASPxLabel18">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" style="width: 35%">
                                                        <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                            PickerDirection="ToRight" ShowPickerOnTop="True" Width="245px" ID="txtDate" Style="direction: ltr; text-align: right;"
                                                            ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                                    </td>
                                                    <td valign="top" align="right" style="width: 15%">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ پایان برگزاری " Width="100%" ID="lblEndDate">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" style="width: 35%">
                                                        <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                            PickerDirection="ToRight" ShowPickerOnTop="True" Width="245px" ID="txtEndDate"
                                                            Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="ساعت برگزاری " Width="100%" ID="ASPxLabel2">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" MaxLength="5"
                                                            ID="txtTime">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                                <RegularExpression ErrorText="زمان را با فرمت(HH:MM)وارد نمایید  " ValidationExpression="\d{1,2}:\d{1,2}"></RegularExpression>
                                                                <%--  <RequiredField IsRequired="True" ErrorText="زمان را وارد نمایید"></RequiredField>--%>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="مدت زمان برگزاری(ساعت) *" Width="100%" ID="ASPxLabel29">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" MaxLength="5"
                                                            ID="txtDuration">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                                <RegularExpression ErrorText="مدت زمان را با فرمت(HH:MM)وارد نمایید  " ValidationExpression="\d{1,2}:\d{1,2}"></RegularExpression>
                                                                <RequiredField IsRequired="True" ErrorText="زمان را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="ظرفیت *" Width="100%" ID="ASPxLabel8">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"
                                                            ID="txtCapacity">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                                <RequiredField IsRequired="True" ErrorText="ظرفیت را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="هزینه شرکت در سمینار *" Width="100%" ID="ASPxLabel19">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"
                                                            ID="txtSeminarCost">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                                <RequiredField IsRequired="True" ErrorText="هزینه سمینار را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right" style="width: 15%">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ شروع ثبت نام *" Width="100%" ID="ASPxLabel5">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" style="width: 35%">
                                                        <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                            PickerDirection="ToRight" ShowPickerOnTop="True" Width="245px" ID="txtStartRegister" Style="direction: ltr; text-align: right;"
                                                            ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                                        <pdc:PersianDateValidator runat="server" ClientValidationFunction="PersianDateValidator"
                                                            ValidateEmptyText="True" ControlToValidate="txtStartRegister" ErrorMessage="تاریخ را وارد نمایید"
                                                            Display="Dynamic" ID="PersianDateValidator4"></pdc:PersianDateValidator>
                                                    </td>
                                                    <td valign="top" align="right" style="width: 15%">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ پایان ثبت نام *" Width="100%" ID="ASPxLabel24">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" style="width: 35%">
                                                        <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                            PickerDirection="ToRight" ShowPickerOnTop="True" Width="245px" ID="txtEndRegister"
                                                            Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                                        <pdc:PersianDateValidator runat="server" ClientValidationFunction="PersianDateValidator"
                                                            ValidateEmptyText="True" ControlToValidate="txtEndRegister" ErrorMessage="تاریخ را وارد نمایید"
                                                            Display="Dynamic" ID="PersianDateValidator5"></pdc:PersianDateValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="مطالب و سرفصل ها" Width="100%" ID="ASPxLabel20">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="68px" Width="100%"
                                                            ID="txtTopic">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="false" ErrorText="مطالب و سر فصل ها را به طور دقیق وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="محل برگزاری *" Width="100%" ID="ASPxLabel7">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%"
                                                            ID="txtPlace">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="True" ErrorText="محل برگزاری را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="توضیحات" Width="100%" ID="ASPxLabel15">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%"
                                                            ID="txtDesc">
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxp:ASPxPanel>
                        </fieldset>
                        <fieldset runat="server" id="ASPxRoundPanel8">
                            <legend class="fieldset-legend" dir="rtl"><b>زمان بندی برنامه های سمینار</b>
                            </legend>
                            <table runat="server" id="tblSchedule" dir="rtl" width="100%">
                                <tr runat="server" id="Tr1">
                                    <td runat="server" id="TD1" valign="top" align="right" style="width: 15%">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ" ID="ASPxLabel9">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="TD3" valign="top" align="right" style="width: 35%">
                                        <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" ShowPickerOnTop="True" Width="245px" ID="txtScheduleDate"
                                            Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                        <pdc:PersianDateValidator runat="server" ClientValidationFunction="PersianDateValidator"
                                            ControlToValidate="txtScheduleDate" ErrorMessage="تاریخ را  وارد نمایید" Display="Dynamic"
                                            ValidationGroup="Sch" ID="PersianDateValidator3"></pdc:PersianDateValidator>
                                    </td>
                                    <td style="width: 15%"></td>
                                    <td style="width: 35%"></td>
                                </tr>
                                <tr runat="server" id="Tr2">
                                    <td runat="server" id="TD5" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="از ساعت" ID="ASPxLabel22" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="TD6" valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"
                                            ID="txtSchTimeFrom">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Sch">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" ErrorText="زمان را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td runat="server" id="TD7" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تا ساعت" ID="ASPxLabel25" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="TD8" valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"
                                            ID="txtSchTimeTo">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Sch">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" ErrorText="زمان را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr3">
                                    <td runat="server" id="TD9" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="موضوع فعالیت" ID="ASPxLabel26" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="TD10" valign="top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%"
                                            ID="txtSchSubject">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Sch">
                                                <RequiredField IsRequired="True" ErrorText="فعالیت را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr4">
                                    <td runat="server" id="TD11" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" Width="100%" ID="ASPxLabel27">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="TD12" valign="top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%"
                                            ID="txtSchDesc">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText=""></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr5">
                                    <td runat="server" id="TD13" valign="top" align="center" colspan="4">
                                        <br />
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text="اضافه به لیست" ValidationGroup="Sch"
                                            ID="btnAddSchedule"
                                            OnClick="btnAddSchedule_Click">
                                            <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>

                            </table>
                            <TSPControls:CustomAspxDevGridView runat="server"
                                KeyFieldName="Id" AutoGenerateColumns="False" RightToLeft="True"
                                Width="100%" ID="AspxGridSchedule" OnRowDeleting="AspxGridSchedule_RowDeleting">
                                <Columns>
                                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="0" Name="Delete" ShowDeleteButton="true">
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Id" Name="Id" Visible="False" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Date" Caption="تاریخ" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TimeFrom" Caption="از ساعت" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TimeTo" Caption="تا ساعت" VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Subject" Caption="موضوع فعالیت" VisibleIndex="3">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Description" Caption="توضیحات" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>

                                </Columns>
                                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                <Settings ShowFilterRowMenu="True" ShowGroupPanel="True"></Settings>
                                <Images>
                                </Images>
                            </TSPControls:CustomAspxDevGridView>
                        </fieldset>
                        <fieldset runat="server" id="RoundPanelTeachers">
                            <legend class="fieldset-legend" dir="rtl"><b>* مشخصات سخنران</b>
                            </legend>
                            <table runat="server" id="tblTe" dir="rtl" width="100%">
                                <tr runat="server" id="Tr7">
                                    <td runat="server" id="Td15" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="ارائه دهنده" ID="ASPxLabel14" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td16" dir="ltr" valign="top" align="right" colspan="3">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" ClientInstanceName="btn"
                                            EnableClientSideAPI="True" CausesValidation="False"
                                            EnableTheming="False" ToolTip="جستجو" ID="ASPxButton2" EnableViewState="False">
                                            <ClientSideEvents Click="function(s, e) {
	popupTeacherSearch.Show();
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/Search.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ClientInstanceName="TextID" ClientVisible="False"
                                            ID="txtTeID">
                                        </TSPControls:CustomTextBox>
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ClientInstanceName="TextType" ClientVisible="False"
                                            ID="txtType">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr8">
                                    <td runat="server" id="Td17" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نوع" Enabled="False" ID="ASPxLabel3" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" valign="top" align="right" colspan="3">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ReadOnly="True"
                                            ClientInstanceName="TextTypeName" EnableClientSideAPI="True" ID="txtTypeName"
                                            __designer:wfdid="w61">
                                            <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom" ValidationGroup="Te">
                                                <RequiredField IsRequired="True" ErrorText="ارائه دهنده را انتخاب نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr9">
                                    <td runat="server" id="Td19" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام" ClientInstanceName="lblNameClient" Enabled="False"
                                            ID="lblName" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td20" valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ReadOnly="True"
                                            ClientInstanceName="TextName" EnableClientSideAPI="True" ID="txtTeName" __designer:wfdid="w63">
                                            <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom" ValidationGroup="Te">
                                                <RequiredField ErrorText="نام را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td runat="server" id="Td24" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ClientInstanceName="lblFamilyClient"
                                            Enabled="False" Width="100%" ID="lblFamily" __designer:wfdid="w64">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td25" valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ReadOnly="True"
                                            ClientInstanceName="TextFamily" EnableClientSideAPI="True" ID="txtTeFamily" __designer:wfdid="w65">
                                            <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom" ValidationGroup="Te">
                                                <RequiredField ErrorText="نام خانوادگی را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr10">
                                    <td runat="server" id="Td26" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نام پدر" Enabled="False" Width="100%" ID="ASPxLabel1">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td27" valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ReadOnly="True"
                                            ClientInstanceName="TextFather" EnableClientSideAPI="True" ID="txtTeFatherName">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Te">
                                                <RequiredField ErrorText="نام پدر را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td runat="server" id="Td28" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="کد ملی" Enabled="False" ID="ASPxLabel12" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td29" valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" MaxLength="10" ReadOnly="True"
                                            ClientInstanceName="TextSSN"
                                            EnableClientSideAPI="True" ID="txtTeSSN" __designer:wfdid="w69">
                                            <MaskSettings IncludeLiterals="DecimalSymbol"></MaskSettings>
                                            <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom" ValidationGroup="Te">
                                                <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="\d{10}"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr15">
                                    <td runat="server" id="Td30" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="دستمزد سخنران" ID="ASPxLabel28" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td31" valign="top" align="right" colspan="3">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"
                                            ClientInstanceName="TextSal" ID="txtTeSalary">
                                            <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom" ValidationGroup="Te">
                                                <ErrorFrameStyle Wrap="True">
                                                </ErrorFrameStyle>
                                                <RegularExpression ErrorText=""></RegularExpression>
                                                <RequiredField IsRequired="True" ErrorText="دستمزد را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr16">
                                    <td runat="server" id="Td32" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" Width="100%" ID="ASPxLabel4" __designer:wfdid="w72">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td33" valign="top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%"
                                            ClientInstanceName="TextTeDesc" ID="txtTeDesc">
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr17">
                                    <td runat="server" id="Td34" valign="top" align="center" colspan="4">
                                        <br />
                                        <TSPControls:CustomAspxButton runat="server" UseSubmitBehavior="False" Text="اضافه به لیست" ValidationGroup="Te"
                                            ID="btnAddTe"
                                            OnClick="btnAddTe_Click">
                                            <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                            <TSPControls:CustomAspxDevGridView runat="server" ClientInstanceName="gridTe"
                                KeyFieldName="Id" AutoGenerateColumns="False" RightToLeft="True"
                                Width="100%" ID="Grdv_Teacher" OnRowDeleting="Grdv_Teacher_RowDeleting">
                                <Columns>
                                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="0" Name="DeleteTeacher" ShowDeleteButton="true">
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Id" Visible="False" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Name" Caption="نام" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Family" Caption="نام خانوادگی" VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Father" Caption="نام پدر" VisibleIndex="3">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SSN" Caption="کد ملی" Visible="False" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Salary" Caption="دستمزد" VisibleIndex="4">
                                        <PropertiesTextEdit Width="100px" DisplayFormatString="#,#">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Type" Visible="False" VisibleIndex="6">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Description" Caption="توضیحات" VisibleIndex="5">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TeId" Visible="False" VisibleIndex="6">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TypeName" Name="TypeName" Caption="ارائه دهنده"
                                        VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                            </TSPControls:CustomAspxDevGridView>
                        </fieldset>
                        <fieldset runat="server" id="FieldsetAttachments">
                            <legend class="fieldset-legend" dir="rtl"><b>فایل های پیوست</b>
                            </legend>
                            <table runat="server" id="tblFile" dir="rtl" width="100%">
                                <tr runat="server" id="Tr12">
                                    <td runat="server" id="TD21" style="vertical-align: top; text-align: right" colspan="2">
                                        <dxe:ASPxLabel runat="server" Text="پیوست خلاصه ای از مطالب سمینار  ,فایل تصویری ارائه و جزوات قابل ارائه در سمینار الزامی می باشد"
                                            ForeColor="DarkBlue" ID="ASPxLabel21">
                                        </dxe:ASPxLabel>
                                        <br />
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr13">
                                    <td runat="server" id="TD22" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="فایل" ID="ASPxLabel6" __designer:wfdid="w48">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="TD23" valign="top" align="right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" InputType="Files" ClientInstanceName="flpc"
                                                            ShowProgressPanel="True" UploadWhenFileChoosed="true" ID="flp" OnFileUploadComplete="flp_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
	imgEndUploadImgClient.SetVisible(true);
}"></ClientSideEvents>
                                                            <CancelButton Text="انصراف">
                                                            </CancelButton>
                                                        </TSPControls:CustomAspxUploadControl>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد"
                                                            ClientInstanceName="imgEndUploadImgClient" ClientVisible="False" ID="imgEndUploadImg"
                                                            __designer:wfdid="w50">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxLabel runat="server" Text="فایل را انتخاب نمایید" ClientInstanceName="lblAttachError"
                                            ClientVisible="False" ForeColor="Red" ID="lblAttachError" __designer:wfdid="w51">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr14">
                                    <td runat="server" id="TD36" valign="top" align="right" style="width: 15%">
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" Width="100%" ID="ASPxLabel17" __designer:wfdid="w52">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="TD37" valign="top" align="right">
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%"
                                            ID="txtDescImg" __designer:wfdid="w53">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText=""></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr18">
                                    <td runat="server" id="TD38" align="center" colspan="2">
                                        <br />
                                        <TSPControls:CustomAspxButton runat="server" UseSubmitBehavior="False" CausesValidation="False"
                                            Text="اضافه به لیست"
                                            ID="btnAddFlp" __designer:wfdid="w54" OnClick="btnAddFlp_Click">
                                            <ClientSideEvents Click="function(s, e) {
	//e.processOnServer=false;
//	if(flpc.GetText()=='')
	//{
	//	lblAttachError.SetVisible(true);
	//	e.processOnServer=false;
	//}
	//else
	//{
	//	lblAttachError.SetVisible(false);
	//	e.processOnServer=true;
//	}
}"></ClientSideEvents>
                                            <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>

                            </table>
                            <TSPControls:CustomAspxDevGridView runat="server"
                                KeyFieldName="Id" AutoGenerateColumns="False" RightToLeft="True"
                                Width="100%" ID="AspxGridFlp" ClientInstanceName="AspxGridFlp" EnableViewState="False"
                                OnRowDeleting="AspxGridFlp_RowDeleting">
                                <Columns>
                                    <dxwgv:GridViewCommandColumn Width="80px" Caption=" " VisibleIndex="0" Name="Delete" ShowDeleteButton="true">
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="FilePath" Name="FilePath" Caption="فایل"
                                        VisibleIndex="0">
                                        <DataItemTemplate>
                                            <asp:HyperLink ID="HyperLink2" runat="server" Text="آدرس فایل" Target="_blank" NavigateUrl='<%# Bind("TempImgUrl") %>'></asp:HyperLink>
                                        </DataItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButton2" runat="server">LinkButton</asp:LinkButton>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Description" Name="Description" Caption="توضیحات"
                                        VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>

                                </Columns>
                                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                            </TSPControls:CustomAspxDevGridView>
                        </fieldset>
               
                        <fieldset runat="server" id="Fieldset2">
                            <legend class="fieldset-legend" dir="rtl"><b>امتیاز بندی برای رشته های مختلف</b>
                            </legend>
                            <table runat="server" id="TableGrade" dir="rtl" width="100%">
                                <tr runat="server" id="Tr21">
                                    <td runat="server" id="TD43" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="رشته" Width="100%" ID="ASPxLabel13">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="TD44" dir="ltr" valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ObjdsMajor"
                                            TextField="MjName" ValueField="MjId" AutoPostBack="True"
                                            ID="CmbMajor"
                                            OnSelectedIndexChanged="CmbMajor_SelectedIndexChanged" RightToLeft="True" Width="100%">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="ValidateGrade">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" ErrorText="رشته را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td runat="server" id="TD45" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="پایه" ID="ASPxLabel16" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="TD46" dir="ltr" valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ObjdsUpGrade"
                                            TextField="UpGrdName" ValueField="UpGrdId" AutoPostBack="True"
                                            ID="cmbUpGrade"
                                            OnSelectedIndexChanged="cmbUpGrade_SelectedIndexChanged" RightToLeft="True" Width="100%">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="ValidateGrade">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" ErrorText="پایه را انتخاب نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr22">
                                    <td runat="server" id="TD47" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="صلاحیت" ID="ASPxLabel11" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="TD48" dir="ltr" valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ObjdsAcceptGrade"
                                            TextField="ResName" ValueField="GMRId"
                                            ID="cmbResponsibility"
                                            RightToLeft="True" Width="100%">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="ValidateGrade">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" ErrorText=" صلاحیت را انتخاب نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td runat="server" id="TD49" valign="top" align="right"></td>
                                    <td runat="server" id="TD50" valign="top" align="right"></td>
                                </tr>
                                <tr runat="server" id="Tr23">
                                    <td runat="server" id="TD51" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel23">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="TD52" valign="top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%"
                                            ID="txtMjDesc">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText=""></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr24">
                                    <td runat="server" id="TD53" valign="top" align="center" colspan="4">
                                        <br />
                                        <TSPControls:CustomAspxButton  runat="server" UseSubmitBehavior="False" Text="اضافه به لیست" ValidationGroup="ValidateGrade"
                                            ID="btnAddMajor"
                                            OnClick="btnAddMajor_Click">
                                            <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>

                            </table>
                            <TSPControls:CustomAspxDevGridView runat="server"
                                KeyFieldName="Id" AutoGenerateColumns="False" RightToLeft="True"
                                Width="100%" ID="CustomAspxDevGridViewGrade" EnableViewState="False" OnRowDeleting="CustomAspxDevGridViewGrade_RowDeleting">
                                <ClientSideEvents RowDblClick="function(s, e) {
	Gradepop.Hide();

	SetControlValuesGrade();
}"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="0" Name="Delete" ShowDeleteButton="true">
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="GrdName" Caption="پایه" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="ResName" Caption="صلاحیت" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="MjName" Caption="رشته" VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Description" Caption="توضیحات" VisibleIndex="3">
                                    </dxwgv:GridViewDataTextColumn>

                                </Columns>
                                <SettingsEditing Mode="PopupEditForm" PopupEditFormModal="True" EditFormColumnCount="1"></SettingsEditing>
                                <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowGroupPanel="True"></Settings>
                            </TSPControls:CustomAspxDevGridView>
                        </fieldset>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="ASPxRoundPanel3" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                                        <table >
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                            CausesValidation="False" ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="BtnNew_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/new.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                            CausesValidation="False" ID="btnEdit2" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnEdit_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/edit.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnSave_Click">
                                                            <ClientSideEvents Click="function(s,e){                                                                   
                                                                     if(gridTe.GetVisibleRowsOnPage()==0)
                                                                    {
                                                                        alert('مشخصات سخنران را وارد نمایید');
                                                                         e.processOnServer=false;
                                                                          return;
                                                                    }    
                                                                    // if(AspxGridFlp.GetVisibleRowsOnPage()==0)
                                                                   // {
                                                                   //     alert('فایل های پیوست را وارد نمایید');
                                                                   //      e.processOnServer=false;
                                                                     //    return;
                                                                  //  }
                                                                     e.processOnServer=true;
                                                                }" />
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/save.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                            CausesValidation="False" ID="ASPxButton4" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnBack_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
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
            <TSPControls:CustomASPxPopupControl ID="popupTeacherSearch" runat="server"
                HeaderText="جستجو" ClientInstanceName="popupTeacherSearch">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">
                        <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False"
                            Width="544px" ID="GridMember" DataSourceID="ObjectDataSource1" KeyFieldName="MeId"
                            AutoGenerateColumns="False" ClientInstanceName="grid"
                            OnCustomCallback="GridMember_CustomCallback">
                            <ClientSideEvents RowDblClick="function(s, e) {
popupTeacherSearch.Hide();
SetControlValues();
	
}"></ClientSideEvents>

                            <Columns>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Name" Caption="نام" Name="Name">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Family" Caption="نام خانوادگی"
                                    Name="Family">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Father" Caption="نام پدر"
                                    Name="Father">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="4" FieldName="Type" Name="Type">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="SSN" Caption="کد ملی"
                                    Name="SSN">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="TeId" Name="TeId">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="TypeName" Caption=" " Name="TypeName">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>

                        </TSPControls:CustomAspxDevGridView>
                        <asp:ObjectDataSource runat="server" CacheExpirationPolicy="Sliding"
                            SelectMethod="SelectTeacherAndLecturer" ID="ObjectDataSource1" CacheDuration="3600" TypeName="TSP.DataManager.TeacherManager"
                            OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
                <HeaderStyle>
                    <Paddings PaddingTop="1px" PaddingRight="6px" PaddingLeft="10px"></Paddings>
                </HeaderStyle>
                <SizeGripImage Height="12px" Width="12px">
                </SizeGripImage>
                <CloseButtonImage Height="17px" Width="17px">
                </CloseButtonImage>
            </TSPControls:CustomASPxPopupControl>
            <asp:ObjectDataSource ID="ObjdsMajor" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsUpGrade" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.DocAcceptedUpGradeManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsAcceptGrade" runat="server" SelectMethod="SelectByMajor"
                TypeName="TSP.DataManager.DocAcceptedGradeManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="MjId"></asp:Parameter>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="GrdId"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBTeacher" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.TeacherManager"
                FilterExpression="InActive={0}">
                <FilterParameters>
                    <asp:Parameter DefaultValue="False" Name="InActive" />
                </FilterParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBMajor" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager"
                OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBLicence" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.LicenceManager"></asp:ObjectDataSource>
            <dxhf:ASPxHiddenField ID="HiddenFieldInfo" ClientInstanceName="HiddenFieldInfo" runat="server">
            </dxhf:ASPxHiddenField>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                            <img align="middle" src="../../Image/indicator.gif" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
