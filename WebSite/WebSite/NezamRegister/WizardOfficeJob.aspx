<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="WizardOfficeJob.aspx.cs" Inherits="NezamRegister_WizardOfficeJob" Title="عضویت حقوقی - سوابق کاری" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript" language="javascript">

        function SetControlValues() {
            jgrid.GetRowValues(jgrid.GetFocusedRowIndex(), 'ProjectName;PJPId;Employer;CitName;StatusOfStartDate;StatusOfEndDate;Area;Floors;Description;StartOriginalDate;StartCorporateDate;EndCorporateDate;PrTypeId;SazeTypeId;CorTypeId;CounId;ProjectVolume', SetValue);
        }
        function SetValue(values) {
            TextPrName.SetText(values[0]);
            CmbPosition.SetValue(values[1]);
            TextEmployer.SetText(values[2]);
            TextCity.SetText(values[3]);
            TextSStatus.SetText(values[4]);
            TextEStatus.SetText(values[5]);

            TextDesc.SetText(values[8]);

            document.getElementById('<%=txtjStartDate.ClientID%>').value = values[9];
            document.getElementById('<%=txtjCoStartDate.ClientID%>').value = values[10];
            document.getElementById('<%=txtjCoEndDate.ClientID%>').value = values[11];

            CmbPrType.SetValue(values[12]);

            if (values[12] == '1') {
                TextArea.SetVisible(true);
                TextFloor.SetVisible(true);
                CmbSazeType.SetVisible(true);
                lbl1.SetVisible(true);
                lbl2.SetVisible(true);
                lbl3.SetVisible(true);
                TextArea.SetText(values[6]);
                TextFloor.SetText(values[7]);
                CmbSazeType.SetValue(values[13]);

            }
            else {
                TextArea.SetVisible(false);
                TextFloor.SetVisible(false);
                CmbSazeType.SetVisible(false);
                lbl1.SetVisible(false);
                lbl2.SetVisible(false);
                lbl3.SetVisible(false);

            }

            CmbCorporate.SetValue(values[14]);
            CmbCountry.SetValue(values[15]);

            TextVolume.SetText(values[16]);

        }
        function SetEmpty() {
            TextPrName.SetText("");
            TextEmployer.SetText("");
            TextCity.SetText("");
            TextSStatus.SetText("");
            TextEStatus.SetText("");
            TextArea.SetText("");
            TextFloor.SetText("");
            TextDesc.SetText("");

            document.getElementById('<%=txtjStartDate.ClientID%>').value = "";
            document.getElementById('<%=txtjCoStartDate.ClientID%>').value = "";
            document.getElementById('<%=txtjCoEndDate.ClientID%>').value = "";

            CmbPrType.SetSelectedIndex(-1);
            CmbSazeType.SetSelectedIndex(-1);
            CmbCorporate.SetSelectedIndex(-1);
            CmbCountry.SetSelectedIndex(-1);
            CmbPosition.SetSelectedIndex(-1);

            TextVolume.SetText("");
            bjob.SetEnabled(true);

            TextArea.SetVisible(false);
            TextFloor.SetVisible(false);
            CmbSazeType.SetVisible(false);
            lbl1.SetVisible(false);
            lbl2.SetVisible(false);
            lbl3.SetVisible(false);
        }
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
            <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server">
                <Items>
                    <dxm:MenuItem Text="چهارچوب شئون حرفه ای" Name="Membership">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مشخصات شرکت" Name="Office">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="شعبه ها" Name="Agent">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="اعضای شرکت" Name="Member">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق کاری" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="آگهی های رسمی" Name="Letter">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="خلاصه اطلاعات" Name="Summary">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="ثبت نهایی" Name="End">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel1" HeaderText="سوابق کاری" runat="server">


                <HeaderTemplate>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 20%; height: 28px" valign="middle">
                                <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="سوابق کاری">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="width: 80%; height: 28px" valign="middle">
                                <TSPControls:CustomAspxButton IsMenuButton="true" AutoPostBack="false" ID="btnHelp" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False"
                                    Text=" " ToolTip="راهنما" UseSubmitBehavior="False">
                                  
                                    <image height="25px" url="~/Images/Help.png" width="25px">
                                                        </image>
                                    <ClientSideEvents Click="function(s,e){ ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <PanelCollection>
                    <dxp:PanelContent>
                        <div class="row">
                            <div class="col-md-3">نام پروژه *</div>
                            <div class="col-md-9">
                                <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="100" ID="txtjPrName"
                                    ClientInstanceName="TextPrName">
                                    <ValidationSettings Display="Dynamic" ErrorText="" ValidationGroup="j" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="True" ErrorText="نام پروژه را وارد نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">نام کارفرما *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="80" ID="txtjEmployer"
                                    ClientInstanceName="TextEmployer">
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
                            </div>
                            <div class="col-md-3">نوع پروژه *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%" TextField="Name" ID="CombojPrType"
                                    DataSourceID="OdbPrType" ValueType="System.String" ValueField="PrtId" EnableIncrementalFiltering="true"
                                    ClientInstanceName="CmbPrType" RightToLeft="True">
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
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <dxe:ASPxLabel runat="server" Text="نوع سازه *" ID="ASPxLabel10" ClientInstanceName="lbl3"
                                    ClientVisible="False">
                                </dxe:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%" EnableIncrementalFiltering="true" TextField="Name" ID="CombojSazeType"
                                    DataSourceID="OdbSazeType" ValueType="System.String" ValueField="SztId" ClientInstanceName="CmbSazeType"
                                    ClientVisible="False" RightToLeft="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                        ErrorTextPosition="Bottom">

                                        <RequiredField IsRequired="True" ErrorText="نوع سازه را انتخاب نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomAspxComboBox>
                            </div>
                            <div class="col-md-3"></div>
                            <div class="col-md-3"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">سمت *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                    EnableIncrementalFiltering="true" TextField="PName"
                                    ID="ComboPosition" DataSourceID="OdbJobPosition" ValueType="System.String"
                                    ValueField="PJPId" RightToLeft="True" ClientInstanceName="CmbPosition">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(CmbPrType.GetValue() == '1')
	{
	TextArea.SetVisible(true);
	TextFloor.SetVisible(true);
	lbl1.SetVisible(true);
	lbl2.SetVisible(true);
	}
	else
	{
	TextArea.SetVisible(false);
	TextFloor.SetVisible(false);
	lbl1.SetVisible(false);
	lbl2.SetVisible(false);
	}
}"></ClientSideEvents>
                                    <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                        ErrorTextPosition="Bottom">

                                        <RequiredField IsRequired="True" ErrorText="سمت را انتخاب نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomAspxComboBox>
                            </div>
                            <div class="col-md-3">نحوه مشارکت *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                    EnableIncrementalFiltering="true" TextField="CorName"
                                    ID="CombojIsCorporate" DataSourceID="OdbCorType" ValueType="System.String"
                                    ValueField="CortId" ClientInstanceName="CmbCorporate" RightToLeft="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                        ErrorTextPosition="Bottom">

                                        <RequiredField IsRequired="True" ErrorText="مشارکت را انتخاب نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomAspxComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">کشور *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomAspxComboBox EnableIncrementalFiltering="true" runat="server" Width="100%"
                                    TextField="CounName" ID="CombojCountry"
                                    DataSourceID="ODBJobCountry" ValueType="System.String" ValueField="CounId"
                                    ClientInstanceName="CmbCountry" RightToLeft="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                        ErrorTextPosition="Bottom">

                                        <RequiredField IsRequired="True" ErrorText="کشور را انتخاب نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomAspxComboBox>
                            </div>
                            <div class="col-md-3">شهر *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="30" ID="txtjCity"
                                    ClientInstanceName="TextCity">
                                    <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                        ErrorTextPosition="Bottom">
                                        <RequiredField IsRequired="True" ErrorText="شهر را وارد نمایید"></RequiredField>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">تاریخ شروع پروژه</div>
                            <div class="col-md-3">
                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="245px" ShowPickerOnTop="True"
                                    ID="txtjStartDate" PickerDirection="ToRight" ShowPickerOnEvent="OnClick" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ نامعتبر" ValidationGroup="j" ControlToValidate="txtjStartDate"
                                    ID="PersianDateValidator1">تاریخ نامعتبر</pdc:PersianDateValidator>
                            </div>
                            <div class="col-md-3">حجم پروژه</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="30" ID="txtjPrVolume"
                                    ClientInstanceName="TextVolume">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RequiredField ErrorText=""></RequiredField>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">تاریخ شروع همکاری</div>
                            <div class="col-md-3">
                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="245px" ShowPickerOnTop="True"
                                    ID="txtjCoStartDate" ShowPickerOnEvent="OnClick" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ نامعتبر" ValidationGroup="j" ControlToValidate="txtjCoStartDate"
                                    ID="PersianDateValidator2">تاریخ نامعتبر</pdc:PersianDateValidator>
                                <br />
                                <dxe:ASPxLabel runat="server" Text="محدوده تاریخ وارد شده صحیح نمی باشد" ClientVisible="False"
                                    ID="ASPxLabel2" ForeColor="Red" ClientInstanceName="lblDateError">
                                </dxe:ASPxLabel>
                            </div>
                            <div class="col-md-3">تاریخ پایان همکاری</div>
                            <div class="col-md-3">
                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="245px" ShowPickerOnTop="True"
                                    ID="txtjCoEndDate" ShowPickerOnEvent="OnClick" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ نامعتبر" ValidationGroup="j" ControlToValidate="txtjCoEndDate"
                                    ID="PersianDateValidator3">تاریخ نامعتبر</pdc:PersianDateValidator>
                            </div>

                            <div class="row">
                                <div class="col-md-3">وضعیت پروژه در زمان شروع همکاری</div>
                                <div class="col-md-3">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="50" ID="txtjStartStatus"
                                        ClientInstanceName="TextSStatus">
                                    </TSPControls:CustomTextBox>
                                </div>
                                <div class="col-md-3">وضعیت پروژه در زمان پایان همکاری</div>
                                <div class="col-md-3">
                                    <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="50" ID="txtjEndStatus"
                                        ClientInstanceName="TextEStatus">
                                    </TSPControls:CustomTextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <dxe:ASPxLabel runat="server" Text="زیربنا *" ClientVisible="False" ID="ASPxLabel22"
                                        ClientInstanceName="lbl1">
                                    </dxe:ASPxLabel>
                                </div>
                                <div class="col-md-3">
                                    <TSPControls:CustomTextBox runat="server" ClientVisible="False" Width="100%"
                                        ID="txtjArea" ClientInstanceName="TextArea">
                                        <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                            <RequiredField IsRequired="True" ErrorText="زیر بنا را وارد نمایید"></RequiredField>
                                            <RegularExpression ErrorText=" زیربنا را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </div>
                                <div class="col-md-3">
                                    <dxe:ASPxLabel runat="server" Text="تعداد طبقات *" ClientVisible="False" ID="ASPxLabel23"
                                        ClientInstanceName="lbl2">
                                    </dxe:ASPxLabel>
                                </div>
                                <div class="col-md-3">
                                    <TSPControls:CustomTextBox runat="server" ClientVisible="False" Width="100%"
                                        ID="txtjFloor" ClientInstanceName="TextFloor">
                                        <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                            <RequiredField IsRequired="True" ErrorText="تعداد طبقات را وارد نمایید"></RequiredField>
                                            <RegularExpression ErrorText="تعداد  را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                        </ValidationSettings>
                                    </TSPControls:CustomTextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">توضیحات(حداکثر255کاراکتر)</div>
                                <div class="col-md-9">
                                    <TSPControls:CustomASPXMemo runat="server" Height="45px" Width="100%" ID="txtjDesc"
                                        ClientInstanceName="TextDesc">
                                        <ClientSideEvents KeyPress="function(s,e){ CheckDevExpressTextboxLengthOnKeyPress(s,e,255); }" />
                                    </TSPControls:CustomASPXMemo>
                                </div>
                            </div>
                            <br />
                            <div class="Item-center">
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="&nbsp;&nbsp;اضافه به لیست"
                                    ValidationGroup="j" ID="btnJob" UseSubmitBehavior="False" Wrap="False" ClientInstanceName="bjob"
                                    OnClick="btnJob_Click">
                                    <ClientSideEvents Click="function(s, e) {

	if(CheckDate()==-1)
	{
		e.processOnServer=false;
		lblDateError.SetVisible(true);
	}
	else
		lblDateError.SetVisible(false);
}"></ClientSideEvents>
                                </TSPControls:CustomAspxButton>
                                <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="&nbsp;&nbsp;&nbsp;پاک کردن فرم"
                                    CausesValidation="False" AutoPostBack="False" ID="btnJobRefresh" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= false;
 if(confirm('آیا مطمئن به پاک کردن اطلاعات فرم هستید؟'))
	SetEmpty();
}"></ClientSideEvents>
                                </TSPControls:CustomAspxButton>
                            </div>
                            <br />
                            <TSPControls:CustomAspxDevGridView2 ID="GrdvJob" runat="server" Width="100%"
                                EnableCallBacks="False" OnRowDeleting="GrdvJob_RowDeleting"
                                KeyFieldName="JhId" ClientInstanceName="jgrid" AutoGenerateColumns="False">
                                <ClientSideEvents RowClick="function(s, e) {
	//bjob.SetEnabled(false);
	//SetControlValues();
}"
                                    SelectionChanged="function(s, e) {
	bjob.SetEnabled(false);
	SetControlValues();
}"></ClientSideEvents>
                                <Columns>
                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " Width="25px" ShowClearFilterButton="true" ShowSelectButton="true">
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " Width="25px" ShowDeleteButton="true">
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="JhId" Name="JhId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectName" Caption="نام پروژه"
                                        Name="ProjectName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Employer" Caption="کارفرما"
                                        Name="Employer" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="PrTypeName" Caption="نوع پروژه"
                                        Name="PrTypeName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="StartCorporateDate" Caption="تاریخ شروع همکاری"
                                        Name="StartCorporateDate">
                                        <HeaderStyle Wrap="True" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="EndCorporateDate" Caption="تاریخ پایان همکاری"
                                        Name="EndCorporateDate">
                                        <HeaderStyle Wrap="True" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="Area" Caption="زیربنا"
                                        Name="Area">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="CorTypeName" Caption="نحوه مشارکت"
                                        Name="CorTypeName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="PrTypeId"
                                        Name="PrTypeId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="SazeTypeId"
                                        Name="SazeTypeId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="SazeTypeName"
                                        Name="SazeTypeName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="CitName" Caption="شهر"
                                        Name="CitName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="CounId"
                                        Name="CounId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="CounName"
                                        Name="CounName">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="ProjectPosition"
                                        Name="ProjectPosition">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="StartOriginalDate"
                                        Name="StartOriginalDate">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="StatusOfStartDate"
                                        Name="StatusOfStartDate">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="StatusOfEndDate"
                                        Name="StatusOfEndDate">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="ProjectVolume"
                                        Name="ProjectVolume">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="Floors"
                                        Name="Floors">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="CorTypeId"
                                        Name="CorTypeId">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="7" FieldName="Description"
                                        Name="Description">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="9" FieldName="PJPId"
                                        Name="PJPId">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView2>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <div class="Item-center">
                <TSPControls:CustomAspxButton CssClass="ButtonMenue"  ID="btnPre" OnClick="btnPre_Click" runat="server" Text="&nbsp;&nbsp;&nbsp;بازگشت&nbsp;&nbsp;&nbsp;" CausesValidation="False"
                    UseSubmitBehavior="False" EnableTheming="False" EnableViewState="False" ToolTip="بازگشت">
                </TSPControls:CustomAspxButton>
                <TSPControls:CustomAspxButton CssClass="ButtonMenue"  ID="btnNext" OnClick="btnNext_Click" runat="server" Text="تایید و ادامه" CausesValidation="False"
                    UseSubmitBehavior="False" EnableTheming="False" EnableViewState="False" ToolTip="تایید و ادامه">
                </TSPControls:CustomAspxButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img align="middle" src="../Image/indicator.gif" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
    <asp:ObjectDataSource ID="ODBJobName" runat="server"
        SelectMethod="GetData" TypeName="TSP.DataManager.JobSubjectManager"></asp:ObjectDataSource>
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
    <dx:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
    </dx:ASPxHiddenField>
</asp:Content>
