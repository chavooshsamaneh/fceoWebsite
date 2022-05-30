<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddPeriods.aspx.cs" Inherits="Employee_Amoozesh_AddPeriods"
    Title="مشخصات دوره" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="../../UserControl/WFUserControl.ascx" TagName="WFUserControl" TagPrefix="uc1" %>
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

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script language="javascript">

        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }

        function SetControlValues() {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'TeId;NonPracticalSalary;NonPracticalHour;PracticalSalary;PracticalHour;WorkroomSalary;WorkroomHour;TeName;MjName;LiName;FileNo;MeId;Description', SetValue);
        }
        function SetValue(values) {
            combobox.SetValue(values[0]);
            TextSNP.SetText(values[1]);
            TextHNP.SetText(values[2]);
            TextSP.SetText(values[3]);
            TextHP.SetText(values[4]);
            TextSW.SetText(values[5]);
            TextHW.SetText(values[6]);
            TextName.SetText(values[7]);
            TextMj.SetText(values[8]);
            TextLi.SetText(values[9]);
            TextFile.SetText(values[10]);
            TextMeId.SetText(values[11]);
            TextDesc.SetText(values[12]);

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

        function SetEmpty() {
            combobox.SetSelectedIndex(-1);
            TextSNP.SetText("");
            TextHNP.SetText("");
            TextSP.SetText("");
            TextHP.SetText("");
            TextSW.SetText("");
            TextHW.SetText("");
            TextName.SetText("");
            TextMj.SetText("");
            TextLi.SetText("");
            TextFile.SetText("");
            TextMeId.SetText("");
            TextDesc.SetText("");

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
                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0">
                                            <tbody>
                                                <tr>

                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" "
                                                            Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit" EnableViewState="False"
                                                            OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                                            <Image Url="~/Images/icons/edit.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " EnableTheming="False"
                                                            ToolTip="ذخیره" ID="btnSave" EnableViewState="False" OnClick="btnSave_Click"
                                                            UseSubmitBehavior="False">
                                                            <Image Url="~/Images/icons/save.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار"
                                                            ID="btnWF" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" CausesValidation="False">
                                                            <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>

                                                            <Image Url="~/Images/icons/reload.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" "
                                                            EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click"
                                                            UseSubmitBehavior="False">
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
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <div>
                <TSPControls:CustomAspxMenuHorizontal ID="AspxMenu1" runat="server" SeparatorHeight="100%" SeparatorColor="#A5A6A8"
                    AutoSeparators="RootOnly" OnItemClick="AspxMenu1_ItemClick"
                    SeparatorWidth="1px"
                    ItemSpacing="0px" ClientVisible="false">
                    <Items>
                        <dxm:MenuItem Name="InValid" Text="لغو دوره" Visible="false">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="TestMarks" Text="نتایج آزمون" Selected="true">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Costs" Text="هزینه های متفرقه">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Period" Text="مشخصات دوره" Selected="true">
                        </dxm:MenuItem>
                    </Items>
                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-2" LastItemX="-1" LastItemY="-2"
                        X="-1" Y="-2" />
                    <Border BorderColor="#A5A6A8" BorderStyle="Solid" BorderWidth="1px" />
                    <VerticalPopOutImage Height="8px" Width="4px" />
                    <ItemStyle ImageSpacing="5px" PopOutImageSpacing="7px" VerticalAlign="Middle" />
                    <SubMenuItemStyle ImageSpacing="7px">
                    </SubMenuItemStyle>
                    <SubMenuStyle BackColor="#EDF3F4" GutterWidth="0px" SeparatorColor="#7EACB1" />
                    <HorizontalPopOutImage Height="7px" Width="7px" />
                </TSPControls:CustomAspxMenuHorizontal>
            </div>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <div align="right" dir="rtl" width="100%" runat="server" id="DivAlert">
                            <ul class="HelpUL">
                                <li>توجه: بدلیل ثبت نام اعضا در این دوره امکان تغییر هزینه دوره و هزینه آزمون وجود ندارد.
                                </li>
                            </ul>
                        </div>
                        <fieldset runat="server" id="FieldSetBaseInfo">
                            <legend class="fieldset-legend" dir="rtl"><b>اطلاعات پایه</b>
                            </legend>

                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" style="width: 20%">
                                            <dxe:ASPxLabel runat="server" Text="عنوان دوره(درس) *" Width="100%" ID="ASPxLabel10">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 30%">
                                            <TSPControls:CustomAspxComboBox runat="server" PopupVerticalAlign="NotSet" Width="100%"
                                                IncrementalFilteringMode="StartsWith" TextField="CrsTitle" ID="cmbCrsId" AutoPostBack="True"
                                                DataSourceID="ODBCourse" EnableClientSideAPI="True"
                                                ValueType="System.String" ValueField="CrsId" ClientInstanceName="combobox"
                                                EnableIncrementalFiltering="True" OnSelectedIndexChanged="cmbCrsId_SelectedIndexChanged"
                                                RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Width="14px" Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="دوره مرتبط را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right" style="width: 20%">
                                            <dxe:ASPxLabel runat="server" Text="موسسه برگزار کننده*" Width="100%" ID="ASPxLabel8">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 30%">
                                            <TSPControls:CustomAspxComboBox runat="server" PopupVerticalAlign="NotSet" Width="100%"
                                                IncrementalFilteringMode="StartsWith" TextField="InsName" ID="cmbInstitue" AutoPostBack="True"
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td valign="top" align="right" style="width: 20%">
                                                        <dxe:ASPxLabel runat="server" Text="طول دوره(ساعت)" Width="100%" Enabled="False"
                                                            ID="ASPxLabel6">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" style="width: 30%">
                                                        <TSPControls:CustomTextBox  runat="server" ID="txtPDuration" ReadOnly="True"
                                                            Width="100%" ClientInstanceName="TextDur">
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right" style="width: 20%">
                                                        <dxe:ASPxLabel runat="server" Text="امتیاز" Enabled="False" ID="ASPxLabel13" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" style="width: 30%">
                                                        <TSPControls:CustomTextBox  runat="server" ID="txtPoint" ReadOnly="True"
                                                            Width="100%">
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="مدت زمان اعتبار(ماه)" Width="100%" Enabled="False"
                                                            ID="ASPxLabel17">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox  runat="server" ID="txtValidDuration" ReadOnly="True"
                                                            Width="100%">
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="مدت زمان عملی(ساعت)" Width="100%" Enabled="False"
                                                            ID="ASPxLabel24">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox  runat="server" ID="txtbPracticalDuration" ReadOnly="True"
                                                            Width="100%">
                                                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" ErrorTextPosition="Bottom">
                                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RegularExpression ErrorText=""></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="مدت زمان تئوری(ساعت)" Width="100%" Enabled="False"
                                                            ID="ASPxLabel27">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox  runat="server" ID="txtbNonPracticalDuration"
                                                            ReadOnly="True" Width="100%">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RegularExpression ErrorText=""></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="مدت زمان بازدید از کارگاه(ساعت)" Width="100%"
                                                            Enabled="False" ID="ASPxLabel25">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox  runat="server" ID="txtbWorkroomDuration" ReadOnly="True"
                                                            Width="100%">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RegularExpression ErrorText=""></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="کد دوره *" Width="100%" ID="ASPxLabel18">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox  runat="server" ID="txtPPCode" Width="100%">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="ظرفیت دوره *" Width="100%" ID="ASPxLabel2">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox  runat="server" ID="txtCapacity" Width="100%">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" ErrorText="ظرفیت دوره را وارد نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="هزینه دوره(بدون تخفیف) *" Width="100%" ID="ASPxLabel19">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox  runat="server" ID="txtPeriodCost" Width="100%">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" ErrorText="هزینه دوره را وارد نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="هزینه آزمون" ID="ASPxLabel39" Width="100%">
                                                        </dxe:ASPxLabel>
                                                        <dxe:ASPxLabel runat="server" Text="تخفیف" ID="ASPxLabel20" Visible="False" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox  runat="server" ID="txtDiscount" Visible="False"
                                                            Width="100%">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField ErrorText=""></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                        <TSPControls:CustomTextBox  runat="server" ID="txtTestCost" Width="100%">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField ErrorText=""></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ شروع *" Width="100%" ID="ASPxLabel12">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                                            Width="200px" ShowPickerOnTop="True" ID="txtStartDate" PickerDirection="ToRight"
                                                            RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                            ErrorMessage="تاریخ را وارد نمایید" ControlToValidate="txtStartDate" ID="PersianDateValidator1"
                                                            Display="Dynamic"></pdc:PersianDateValidator>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ پایان *" Width="100%" ID="ASPxLabel11">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                                            Width="200px" ShowPickerOnTop="True" ID="txtEndDate" PickerDirection="ToRight"
                                                            RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                            ErrorMessage="تاریخ را وارد نمایید" ControlToValidate="txtEndDate" ID="PersianDateValidator2"
                                                            Display="Dynamic"></pdc:PersianDateValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ آزمون *" Width="100%" ID="ASPxLabel4">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                                            Width="200px" ShowPickerOnTop="True" ID="txtTestDate" PickerDirection="ToRight"
                                                            RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                            ErrorMessage="تاریخ را وارد نمایید" ControlToValidate="txtTestDate" ID="PersianDateValidator3"
                                                            Display="Dynamic"></pdc:PersianDateValidator>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="ساعت شروع آزمون" Width="100%" ID="ASPxLabel5">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox  runat="server" ID="txtTestHour" Width="100%">
                                                            <MaskSettings Mask="HH:mm"></MaskSettings>
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ شروع ثبت نام *" Width="100%" ID="ASPxLabel32">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                                            Width="200px" ShowPickerOnTop="True" ID="txtStartRegisterDate" PickerDirection="ToRight"
                                                            RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                            ErrorMessage="تاریخ را وارد نمایید" ControlToValidate="txtStartRegisterDate"
                                                            ID="PersianDateValidator5" Display="Dynamic"></pdc:PersianDateValidator>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ پایان ثبت نام *" Width="100%" ID="ASPxLabel33">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" DefaultDate=""
                                                            Width="200px" ShowPickerOnTop="True" ID="txtEndRegisterDate" PickerDirection="ToRight"
                                                            RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                            ErrorMessage="تاریخ را وارد نمایید" ControlToValidate="txtEndRegisterDate" ID="PersianDateValidator6"
                                                            Display="Dynamic"></pdc:PersianDateValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="نوع دوره" ID="lblPPType" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td dir="ltr" valign="top" align="right">
                                                        <TSPControls:CustomAspxComboBox runat="server" PopupVerticalAlign="NotSet" Width="100%"
                                                            IncrementalFilteringMode="StartsWith" ID="ComboPPType"
                                                            EnableClientSideAPI="True" ValueType="System.String"
                                                            SelectedIndex="0" EnableIncrementalFiltering="True"
                                                            RightToLeft="True">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Width="14px" Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <RequiredField IsRequired="True" ErrorText="نوع دوره را انتخاب نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <Items>
                                                                <dxe:ListEditItem Value="0" Text="دوره کامل" Selected="True"></dxe:ListEditItem>
                                                                <dxe:ListEditItem Value="1" Text="دوره آزمون"></dxe:ListEditItem>
                                                            </Items>
                                                            <ButtonStyle Width="13px">
                                                            </ButtonStyle>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                         <td valign="top" align="right" style="width: 20%">
                                            <dxe:ASPxLabel runat="server" Text="نظر سنجی دوره*" Width="100%" ID="ASPxLabel34">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 30%">
                                            <TSPControls:CustomAspxComboBox runat="server" PopupVerticalAlign="NotSet" Width="100%"
                                                IncrementalFilteringMode="StartsWith" TextField="Tittle" ID="cmbPoll" AutoPostBack="false"
                                                DataSourceID="ODBPoll" EnableClientSideAPI="True"
                                                ValueType="System.String" ValueField="PollId" ClientInstanceName="cmbPoll"
                                                EnableIncrementalFiltering="True"
                                                RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Width="14px" Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نظرسنجی مرتبط با دوره را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="محل برگزاری دوره *" ID="ASPxLabel7" Width="100%">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtPlace" Width="100%">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="True" ErrorText="محل برگزاری دوره را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="محل برگزاری آزمون *" Width="100%" ID="ASPxLabel26">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtTestPlace" Width="100%">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="True" ErrorText="محل برگزاری آزمون را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="توضیحات" Width="100%" ID="ASPxLabel15">
                                                        </dxe:ASPxLabel>
                                                        <br />
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtDesc" Width="100%">
                                                        </TSPControls:CustomASPXMemo>
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>


                        <fieldset runat="server" id="FieldSet1">
                            <legend class="fieldset-legend" dir="rtl"><b>* مشخصات مدرس</b>
                            </legend>
                            <table dir="rtl" runat="server" id="tblTeacher" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" style="width: 20%">
                                            <dxe:ASPxLabel runat="server" Text="ارائه دهنده" ID="ASPxLabel14" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right" colspan="3">
                                            <TSPControls:CustomAspxComboBox runat="server" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                                ValueType="System.String" DataSourceID="ODBTeacher" TextField="TeName" ValueField="TeId"
                                                AutoResizeWithContainer="True" Width="100%"
                                                AutoPostBack="True"
                                                ClientInstanceName="combobox"
                                                EnableClientSideAPI="True" ID="cmbTeId" OnSelectedIndexChanged="cmbTeId_SelectedIndexChanged"
                                                RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="Te">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="True" ErrorText="ارائه دهنده را انتخاب نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" style="width: 20%">
                                            <dxe:ASPxLabel runat="server" Text="حق الزحمه تئوری(ساعت)" ID="ASPxLabel21">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 30%">
                                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                                ClientInstanceName="TextSNP" ID="txtSalNonpractical">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right" style="width: 20%">
                                            <dxe:ASPxLabel runat="server" Text="مدت زمان تئوری(ساعت)" ID="ASPxLabel29">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 30%">
                                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                                ClientInstanceName="TextHNP" ID="txtHoNonPractical">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="حق الزحمه عملی(ساعت)" ID="ASPxLabel22">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                                ClientInstanceName="TextSP" ID="txtSalPractical">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="مدت زمان عملی(ساعت)" ID="ASPxLabel30">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                                ClientInstanceName="TextHP" ID="txtHoPractical">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="حق الزحمه بازدید از کارگاه(ساعت)" ID="ASPxLabel23">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                                ClientInstanceName="TextSW" ID="txtSalWorkroom">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="مدت زمان بازدید از کارگاه(ساعت)" ID="ASPxLabel28">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox  runat="server" Width="100%"
                                                ClientInstanceName="TextHW" ID="txtHoWorkroom">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام مدرس" Enabled="False" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox  runat="server" Width="100%" ReadOnly="True"
                                                ClientInstanceName="TextName" ID="txtTTeName">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="رشته" Enabled="False" ID="ASPxLabel3">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox  runat="server" Width="100%" ReadOnly="True"
                                                ClientInstanceName="TextMj" ID="txtTMajor">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="آخرین مدرک تحصیلی" Enabled="False" ID="ASPxLabel9">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox  runat="server" Width="100%" ReadOnly="True"
                                                ClientInstanceName="TextLi" ID="txtTLicence">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره پروانه اشتغال" Enabled="False" ID="ASPxLabel16">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox  runat="server" Width="100%" ReadOnly="True"
                                                ClientInstanceName="TextFile" ID="txtTFileNo" Style="direction: ltr">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره عضویت" Enabled="False" ID="lblMeId">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox  runat="server" Width="100%" ReadOnly="True"
                                                ClientInstanceName="TextMeId" ID="txtTMeId">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="پایه" Enabled="False" ID="lblPaye">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox  runat="server" Width="100%" ReadOnly="True"
                                                ClientInstanceName="TextPaye" ID="txtTPaye">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="center" colspan="4">
                                            <dxe:ASPxLabel runat="server" ForeColor="#0000C0" ID="lblInsError" Visible="False"
                                                Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel31">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px" Width="100%"
                                                ClientInstanceName="TextDesc" ID="txtTeDesc">
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            <br />
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxButton runat="server" UseSubmitBehavior="False" ClientInstanceName="btn"
                                                                Text="اضافه به لیست" ValidationGroup="Te"
                                                                ID="btnAddTeacher" OnClick="btnAddTeacher_Click">
                                                                <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton runat="server" AutoPostBack="False" UseSubmitBehavior="False" CausesValidation="False"
                                                                Text="پاک کردن فرم"
                                                                ID="btnTeRefresh" EnableViewState="False">
                                                                <ClientSideEvents Click="function(s, e) {
	SetEmpty();
	btn.SetEnabled(true);
}"></ClientSideEvents>
                                                                <Image Height="16px" Width="16px" Url="~/Images/icons/Clear-Form.png">
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
                            <TSPControls:CustomAspxDevGridView2 runat="server" ClientInstanceName="grid"
                                KeyFieldName="Id" AutoGenerateColumns="False" RightToLeft="True"
                                Width="100%" ID="GridViewTeacher" OnCommandButtonInitialize="GridViewTeacher_CommandButtonInitialize"
                                OnRowUpdating="GridViewTeacher_RowUpdating" OnRowDeleting="GridViewTeacher_RowDeleting"
                                OnHtmlRowPrepared="GridViewTeacher_HtmlRowPrepared">
                                <ClientSideEvents RowClick="function(s, e) {
    SetControlValues();
	btn.SetEnabled(false);
}"
                                    EndCallback="function(s, e) { 
    if(s.cpError==1)
     {
       ShowMessage(s.cpMsg);
       s.cpError=0;
       s.cpMsg = '';
     }
}"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn FieldName="TeId" Visible="False" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TeName" Caption="نام و نام خانوادگی" VisibleIndex="0">
                                        <EditFormSettings Visible="False"></EditFormSettings>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NonPracticalSalary" Caption="حق الزحمه تئوری"
                                        VisibleIndex="1">
                                        <PropertiesTextEdit Width="100px" DisplayFormatString="#,#">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="PracticalSalary" Caption="حق الزحمه عملی"
                                        VisibleIndex="2">
                                        <PropertiesTextEdit Width="100px" DisplayFormatString="#,#">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="WorkroomSalary" Caption="حق الزحمه کارگاه"
                                        VisibleIndex="3">
                                        <PropertiesTextEdit Width="100px" DisplayFormatString="#,#">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NonPracticalHour" Caption="زمان تئوری" VisibleIndex="4">
                                        <PropertiesTextEdit Width="100px">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="PracticalHour" Caption="زمان عملی" VisibleIndex="5">
                                        <PropertiesTextEdit Width="100px">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="WorkroomHour" Caption="زمان کارگاه" VisibleIndex="6">
                                        <PropertiesTextEdit Width="100px">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true" Caption=" " VisibleIndex="7">
                                        <%--  <EditButton Visible="True">
                                        </EditButton>
                                        <DeleteButton Visible="True">
                                        </DeleteButton>--%>
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Id" Visible="False" VisibleIndex="8">
                                        <EditFormSettings Visible="False"></EditFormSettings>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="MeId" Visible="False" VisibleIndex="8">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="FileNo" Visible="False" VisibleIndex="8">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Description" Visible="False" VisibleIndex="8">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="MjName" Visible="False" VisibleIndex="8">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="LiName" Visible="False" VisibleIndex="8">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
                                <Settings ShowFilterRowMenu="True" ShowGroupPanel="True"></Settings>
                            </TSPControls:CustomAspxDevGridView2>
                        </fieldset>

                        <fieldset runat="server" id="FieldSet2">
                            <legend class="fieldset-legend" dir="rtl"><b>زمان برگزاری جلسات</b>
                            </legend>

                            <TSPControls:CustomAspxDevGridView2 runat="server"
                                EnableCallBacks="False" KeyFieldName="Id" AutoGenerateColumns="False"
                                RightToLeft="True" Width="100%" ID="GridViewSchedule" EnableViewState="False"
                                OnCommandButtonInitialize="GridViewSchedule_CommandButtonInitialize" OnRowUpdating="GridViewSchedule_RowUpdating"
                                OnRowDeleting="GridViewSchedule_RowDeleting" OnRowValidating="GridViewSchedule_RowValidating"
                                OnRowInserting="GridViewSchedule_RowInserting" OnHtmlRowPrepared="GridViewSchedule_HtmlRowPrepared">
                                <ClientSideEvents EndCallback="function(s,e) { 
   if(s.cpError==1)
     {
       ShowMessage(s.cpMsg);
       s.cpError=0;
       s.cpMsg = '';
     }
}"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn FieldName="SchId" Visible="False" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Number" Caption="شماره جلسه" VisibleIndex="0">
                                        <PropertiesTextEdit Width="100px">
                                            <ValidationSettings ErrorDisplayMode="ImageWithText" Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d*"></RegularExpression>
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <EditCellStyle HorizontalAlign="Right">
                                        </EditCellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Date" Caption="تاریخ" VisibleIndex="1">
                                        <PropertiesTextEdit>
                                            <ValidationSettings CausesValidation="True" ErrorText="hi">
                                                <RequiredField IsRequired="True"></RequiredField>
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <EditItemTemplate>
                                            <br />
                                            <pdc:PersianDateTextBox ID="txtDate" runat="server" Width="100px" Text='<%# Bind("Date") %>'
                                                IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" ShowPickerOnTop="True"></pdc:PersianDateTextBox>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="StartTime" Caption="ساعت شروع" VisibleIndex="2">
                                        <PropertiesTextEdit Width="100px">
                                            <ValidationSettings ErrorDisplayMode="ImageWithText" Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="ساعت شروع را وارد نمایید "></RequiredField>
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <EditCellStyle HorizontalAlign="Right">
                                        </EditCellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="EndTime" Caption="ساعت پایان" VisibleIndex="3">
                                        <PropertiesTextEdit Width="100px">
                                            <ValidationSettings ErrorDisplayMode="ImageWithText" Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="ساعت پایان را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <EditCellStyle HorizontalAlign="Right">
                                        </EditCellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataMemoColumn FieldName="Description" Caption="توضیحات" VisibleIndex="4">
                                        <PropertiesMemoEdit Width="280px">
                                        </PropertiesMemoEdit>
                                        <EditFormSettings ColumnSpan="2"></EditFormSettings>
                                        <EditCellStyle HorizontalAlign="Right">
                                        </EditCellStyle>
                                    </dxwgv:GridViewDataMemoColumn>
                                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="5" ShowEditButton="true" ShowNewButton="true" ShowDeleteButton="true">
                                        <%--<EditButton Visible="True">
                                        </EditButton>
                                        <NewButton Visible="True">
                                        </NewButton>
                                        <DeleteButton Visible="True">
                                        </DeleteButton>--%>
                                    </dxwgv:GridViewCommandColumn>
                                </Columns>
                                <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
                            </TSPControls:CustomAspxDevGridView2>
                        </fieldset>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="ASPxRoundPanel3" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                            <tbody>
                                                <tr>
                                                    <%-- <td>
                                                        <TSPControls:CustomAspxButton ID="BtnNew2" runat="server" CausesValidation="False" 
                                                            EnableTheming="False" EnableViewState="False" OnClick="BtnNew_Click" Text=" "
                                                            ToolTip="جدید" UseSubmitBehavior="False" ClientVisible="false">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>--%>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" CausesValidation="False" Text=" "
                                                            Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit2" EnableViewState="False"
                                                            OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnSave2" runat="server" EnableTheming="False"
                                                            EnableViewState="False" OnClick="btnSave_Click" Text=" " ToolTip="ذخیره" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/save.png" Width="25px" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار"
                                                            ID="btnWF2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" CausesValidation="False">
                                                            <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>

                                                            <Image Url="~/Images/icons/reload.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton4" runat="server" CausesValidation="False"
                                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                            ToolTip="بازگشت" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                            </HoverStyle>
                                                            <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
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
            
                
            <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="mygrid" SessionName="SendBackDataTable_SMSIns"
                OnCallback="WFUserControl_Callback" GridHasCallback="true" />
            <dx:ASPxHiddenField ID="HiddenFieldPage" ClientInstanceName="HiddenFieldPage" runat="server">
            </dx:ASPxHiddenField>
            <asp:HiddenField ID="PeriodId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PeriodRequestId" runat="server" Visible="False"></asp:HiddenField>
            <asp:ObjectDataSource runat="server" FilterExpression="InActive={0}" SelectMethod="GetData"
                ID="ODBCourse" TypeName="TSP.DataManager.CourseManager" OldValuesParameterFormatString="original_{0}">
                <FilterParameters>
                    <asp:Parameter DefaultValue="False" Name="InActive"></asp:Parameter>
                </FilterParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource runat="server" SelectMethod="GetData" ID="ODBInstitue" TypeName="TSP.DataManager.InstitueManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource runat="server" FilterExpression="InActive={0}" SelectMethod="SelectConfirmedTeacher"
                ID="ODBTeacher" TypeName="TSP.DataManager.TeacherManager" OldValuesParameterFormatString="original_{0}">
                <FilterParameters>
                    <asp:Parameter DefaultValue="False" Name="InActive"></asp:Parameter>
                </FilterParameters>
            </asp:ObjectDataSource>
                <asp:ObjectDataSource runat="server"  SelectMethod="SelectPollForPeriodInsert"
                ID="ODBPoll" TypeName="TSP.DataManager.PollPollManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter  Name="DateOfToday" DbType="String" DefaultValue="%"></asp:Parameter>
                     <asp:Parameter  Name="PollId" DbType="Int32" DefaultValue="-1" ></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
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

