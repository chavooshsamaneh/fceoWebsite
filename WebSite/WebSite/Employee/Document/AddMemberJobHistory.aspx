<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddMemberJobHistory.aspx.cs" Inherits="Employee_Document_AddMemberJobHistory"
    Title="مشخصات سابقه کار" %>

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
<%@ Register Src="../../UserControl/MemberInfoUserControl.ascx" TagName="MemberInfoUserControl"
    TagPrefix="uc2" %>
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
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
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
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ValidationGroup="j" ID="btnSave" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
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
            <TSPControls:CustomAspxMenuHorizontal ID="MenuJob" runat="server" OnItemClick="MenuJob_ItemClick">
                <Items>
                    <dxm:MenuItem Name="Job" Selected="True" Text="مشخصات سابقه کار">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Qualification" Text="مطلوبیت">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>

            <br />
            <uc2:MemberInfoUserControl ID="MemberInfoUserControl1" runat="server" />
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelJobHistory" HeaderText="مشاهده" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>


                        <table dir="rtl" width="100%">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top" align="right" style="width: 15%">
                                        <dxe:ASPxLabel runat="server" Text="نام پروژه *" ID="ASPxLabel9" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top" dir="rtl" align="right" colspan="3" style="width: 35%">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtjPrName" ClientInstanceName="TextPrName">
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
                                    <td style="vertical-align: top;" align="right" style="width: 15%">
                                        <dxe:ASPxLabel runat="server" Text="نام کارفرما *" ID="ASPxLabel11" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top;" align="right" colspan="3" style="width: 35%">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtjEmployer"
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
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نوع پروژه *" ID="ASPxLabel8" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top;" dir="ltr" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="Name" ID="CombojPrType" DataSourceID="OdbPrType"
                                            ValueType="System.String" ValueField="PrtId" ClientInstanceName="CmbPrType"
                                            EnableIncrementalFiltering="True" Width="100%" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
 if(CmbPrType.GetValue() == '1')
	{
	TextArea.SetVisible(true);
	lbl11.SetVisible(true);
	TextFloor.SetVisible(true);
	lbl2.SetVisible(true);
	CmbSazeType.SetVisible(true);
	lbl3.SetVisible(true);
	}
	else
	{
	TextArea.SetVisible(false);
	lbl11.SetVisible(false);
	TextFloor.SetVisible(false);	
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
                                    <td style="vertical-align: top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نوع سازه *" ID="lblSazeType" ClientVisible="False"
                                            ClientInstanceName="lbl3" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top;" dir="ltr" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" ClientVisible="False"
                                            TextField="Name" ID="CombojSazeType"
                                            DataSourceID="OdbSazeType" ValueType="System.String" ValueField="SztId" ClientInstanceName="CmbSazeType"
                                            EnableIncrementalFiltering="True"
                                            Width="100%" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="نوع سازه را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="سمت *" ID="ASPxLabel14" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top;" dir="ltr" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="PName" ID="ComboPosition" DataSourceID="OdbJobPosition"
                                            ValueType="System.String" ValueField="PJPId" ClientInstanceName="CmbPosition"
                                            EnableIncrementalFiltering="True"
                                            Width="100%" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	//if(CmbPrType.GetValue() == '1')
	//{
	//TextArea.SetVisible(true);
	//TextFloor.SetVisible(true);
	//lbl1.SetVisible(true);
	//lbl2.SetVisible(true);
	//}
	//else
	//{
	//TextArea.SetVisible(false);
	//TextFloor.SetVisible(false);
	//lbl1.SetVisible(false);
	//lbl2.SetVisible(false);
	//}
 //if(CmbPosition.GetValue()=='8' || CmbPosition.GetValue()=='9')
	//	rdpJob.SetVisible(true);
	//else
	//	rdpJob.SetVisible(false);
}"></ClientSideEvents>
                                            <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="سمت را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td style="vertical-align: top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نحوه مشارکت *" ID="ASPxLabel24" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top;" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="CorName" ID="CombojIsCorporate" DataSourceID="OdbCorType"
                                            ValueType="System.String" ValueField="CortId" ClientInstanceName="CmbCorporate"
                                            EnableIncrementalFiltering="True"
                                            Width="100%" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="نحوه مشارکت را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="کشور *" ID="ASPxLabel12" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top;" dir="ltr" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="CounName" ID="CombojCountry" DataSourceID="ODBJobCountry"
                                            ValueType="System.String" ValueField="CounId" ClientInstanceName="CmbCountry"
                                            EnableIncrementalFiltering="True"
                                            Width="100%" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="کشور را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td style="vertical-align: top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="شهر *" ID="ASPxLabel13" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top;" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtjCity" ClientInstanceName="TextCity">
                                            <ValidationSettings ValidateOnLeave="False" Display="Dynamic" ValidationGroup="j"
                                                ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="شهر را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ شروع پروژه *" ID="ASPxLabel16" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top;" align="right">
                                        <pdc:PersianDateTextBox runat="server" Style="direction: ltr" DefaultDate="" Width="225px"
                                            ShowPickerOnTop="True" ID="txtjStartDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtjStartDate" ValidationGroup="j"
                                            ID="RequiredFieldValidator31" Display="Dynamic">تاریخ شروع پروژه را وارد نمایید</asp:RequiredFieldValidator>
                                    </td>
                                    <td style="vertical-align: top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="حجم پروژه" ID="ASPxLabel21" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top;" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtjPrVolume"
                                            ClientInstanceName="TextVolume">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField ErrorText=""></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ شروع همکاری *" ID="ASPxLabel17" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top;" align="right">
                                        <pdc:PersianDateTextBox runat="server" Style="direction: ltr" DefaultDate="" Width="225px"
                                            ShowPickerOnTop="True" ID="txtjCoStartDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtjCoStartDate" ValidationGroup="j"
                                            ID="RequiredFieldValidator32" Display="Dynamic">تاریخ شروع همکاری را وارد نمایید</asp:RequiredFieldValidator>
                                        <dxe:ASPxLabel ID="lblDateError" runat="server" ClientInstanceName="lblDateError"
                                            ClientVisible="False"
                                            ForeColor="Red" Text="تاریخ شروع بایستی قبل از تاریخ پایان باشد">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ پایان همکاری *" ID="ASPxLabel19" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top;" align="right">
                                        <pdc:PersianDateTextBox runat="server" Style="direction: ltr" DefaultDate="" Width="225px"
                                            ShowPickerOnTop="True" ID="txtjCoEndDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtjCoEndDate" ValidationGroup="j"
                                            ID="RequiredFieldValidator33" Display="Dynamic">تاریخ پایان همکاری را وارد نمایید</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="وضعیت پروژه در زمان شروع همکاری" ID="ASPxLabel18">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top;" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtjStartStatus"
                                            ClientInstanceName="TextSStatus">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td style="vertical-align: top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="وضعیت پروژه در زمان پایان همکاری" ID="ASPxLabel20"
                                            Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top;" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="txtjEndStatus" ClientInstanceName="TextEStatus"
                                            Width="100%">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="زیربنا *" ClientVisible="False" ID="ASPxLabel22"
                                            ClientInstanceName="lbl11" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top;" align="right">
                                        <TSPControls:CustomTextBox runat="server" ClientVisible="False" Width="170px"
                                            ID="txtjArea" ClientInstanceName="TextArea">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="زبر بنا را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="مقدار زیربنا را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td style="vertical-align: top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تعداد طبقات *" ClientVisible="False" ID="ASPxLabel23"
                                            ClientInstanceName="lbl2" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top;" align="right">
                                        <TSPControls:CustomTextBox runat="server" ClientVisible="False" Width="100%"
                                            ID="txtjFloor" ClientInstanceName="TextFloor">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="j" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="تعداد طبقات را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="تعداد طبقات را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel25" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top;" colspan="3" align="right">
                                        <TSPControls:CustomASPXMemo runat="server" Height="33px" Width="100%" ID="txtjDesc"
                                            ClientInstanceName="TextDesc">
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <dxe:ASPxPanel runat="server" id="ASPxRoundPanel4"><PanelCollection><dxe:PanelContent>
                        <fieldset >
                            <legend class="HelpUL">مطلوبیت کار انجام شده</legend>
                            <table runat="server" id="tbl" dir="rtl" width="100%">
                                <tr runat="server" id="Tr1">
                                    <td runat="server" id="Td1" style="vertical-align: top" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نوع مطلوبیت کار" Width="100%" ID="ASPxLabel4"
                                            >
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td2" style="vertical-align: top" dir="ltr" valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            TextField="Name" ID="CmbName" DataSourceID="OdbFactorDocuments" ValueType="System.String"
                                            ValueField="OfdId" EnableIncrementalFiltering="True"
                                            RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ValidationGroup="Job" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="نوع وضعیت مالی را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <Columns>
                                                <dxe:ListBoxColumn FieldName="Name" Caption="نام" Width="290px"></dxe:ListBoxColumn>
                                                <dxe:ListBoxColumn FieldName="Value" Caption="حداکثر نمره"></dxe:ListBoxColumn>
                                            </Columns>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr2">
                                    <td runat="server" id="Td5" style="vertical-align: top; width: 15%" valign="top"
                                        align="right">
                                        <dxe:ASPxLabel runat="server" Text="فایل" ID="ASPxLabel5" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td6" style="vertical-align: top; width: 85%" valign="top"
                                        align="right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" ShowProgressPanel="True" ID="flp"
                                                            InputType="Files" ClientInstanceName="flpc" OnFileUploadComplete="flp_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) { 

  if(e.isValid){
	imgEndUploadImgClient.SetVisible(true);
	flpme.Set('name',1);
	lblf.SetVisible(false);
    }
}"></ClientSideEvents>
                                                            <CancelButton Text="انصراف">
                                                            </CancelButton>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                            ID="ASPxLabel48" ForeColor="Red" ClientInstanceName="lblf" __designer:wfdid="w23">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="ASPxImage1" ClientInstanceName="imgEndUploadImgClient" __designer:wfdid="w24">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr3">
                                    <td runat="server" id="Td3" style="vertical-align: top" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel6" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td runat="server" id="Td4" style="vertical-align: top" valign="top" align="right">
                                        <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtJhDesc" Width="100%"
                                            __designer:wfdid="w26">
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr4">
                                    <td runat="server" id="Td7" style="vertical-align: top" align="center"  colspan="2">
                                          <br />
                                        <TSPControls:CustomAspxButton runat="server" Text="اضافه به لیست" ValidationGroup="Job"
                                            ID="btnAddFlp" UseSubmitBehavior="False"
                                            OnClick="btnAddFlp_Click">
                                            <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                            <ClientSideEvents Click="function(s, e) {

if(flpme.Get('name')!=1)
{
lblf.SetVisible(true);

e.processOnServer=false;
}


}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr5">
                                    <td runat="server" id="Td8" style="vertical-align: top" valign="top" align="right"
                                        colspan="2">
                                        <br />
                                    </td>
                                </tr>
                            </table>
                            <div dir="rtl" id="Div1">
                                <TSPControls:CustomAspxDevGridView2 runat="server" EnableViewState="False"
                                    Width="100%" ID="AspxGridFlp" KeyFieldName="Id" AutoGenerateColumns="False"
                                    OnRowDeleting="AspxGridFlp_RowDeleting">
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowDeleteButton="true" Name="clmnDelete"
                                            Width="25px">                                                                                                         
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="OfdName" Caption="نوع">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="FilePath" Caption="فایل"
                                            Name="FilePath">
                                            <DataItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" Text="آدرس فایل" Target="_blank" NavigateUrl='<%# Bind("TempImgUrl") %>'></asp:HyperLink>
                                            </DataItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
                                            </EditItemTemplate>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Description" Caption="توضیحات"
                                            Name="Description">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                </TSPControls:CustomAspxDevGridView2>
                            </div>
                        </fieldset></dxe:PanelContent></PanelCollection></dxe:ASPxPanel>
                        <fieldset>
                            <legend class="HelpUL" id="RoundPanelAttach" runat="server">مدارک پیوست</legend>

                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: top; width: 15%"  align="right">
                                            <dxe:ASPxLabel runat="server" Text="فایل" ID="ASPxLabel1" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td  align="right" colspan="3" style="width: 85%">
                                            <table >
                                                <tbody>
                                                    <tr>
                                                        <td align="right">
                                                            <TSPControls:CustomAspxUploadControl runat="server" MaxSizeForUploadFile="0" InputType="Files"
                                                                UploadWhenFileChoosed="True" ClientInstanceName="flp" ShowProgressPanel="True"
                                                                Width="258px" ID="flpFile" OnFileUploadComplete="flpFile_FileUploadComplete">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
	if(e.isValid){
    imgEndUploadImgClient.SetVisible(true);
	HDflp.Set('name',1);
	lbl1.SetVisible(false);
    }
}"></ClientSideEvents>
                                                                <CancelButton Text="انصراف">
                                                                </CancelButton>
                                                            </TSPControls:CustomAspxUploadControl>
                                                            <dxe:ASPxLabel runat="server" Text="فایل را انتخاب نمایید" ClientInstanceName="lbl1"
                                                                ClientVisible="False" ForeColor="Red" ID="ASPxLabel3">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxImage runat="server" ImageUrl="~/Images/icons/button_ok.png" ToolTip="فایل انتخاب شد"
                                                                ClientInstanceName="imgEndUploadImgClient" ClientVisible="False" ID="imgEndUploadImg">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <dxe:ASPxHyperLink runat="server" NavigateUrl='<%# Bind("FilePath") %>' Text="آدرس فایل"
                                                Target="_blank" ClientInstanceName="hp" ClientVisible="False" ID="ASPxHyperLink1">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel2" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="31px" Width="100%"
                                                ID="txtDescription">
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
                                        <td style="vertical-align: top; width: 94px" align="right"></td>
                                        <td align="right" colspan="3"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                                   <br />
                                            <TSPControls:CustomAspxButton runat="server" UseSubmitBehavior="False"
                                                Text="اضافه به لیست"
                                                ID="btnSaveAttachment" OnClick="btnSaveAttachment_Click">
                                                <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                                <ClientSideEvents Click="function(s, e) {
	if(HDflp.Get('name')!=1)
{
lbl1.SetVisible(true);

e.processOnServer=false;
}

}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                                
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">   <br />
                                            <TSPControls:CustomAspxDevGridView2 runat="server" ClientInstanceName="GridViewAttachment"
                                                KeyFieldName="Id"
                                                AutoGenerateColumns="False" RightToLeft="True" Width="100%" ID="GridViewAttachment"
                                                OnRowDeleting="GridViewAttachment_RowDeleting">
                                                <Columns>
                                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" "  ShowDeleteButton="true" Name="clmnDelete"
                                                        Width="25px"> 
                                                    </dxwgv:GridViewCommandColumn>
                                                    <dxwgv:GridViewDataImageColumn FieldName="FilePath" Caption="مستندات" Visible="False"
                                                        VisibleIndex="0">
                                                        <PropertiesImage ImageHeight="24px" ImageWidth="24px">
                                                        </PropertiesImage>
                                                    </dxwgv:GridViewDataImageColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="FilePath" Name="FilePath" Caption="فایل"
                                                        VisibleIndex="0">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxHyperLink ID="ASPxHyperLink2" runat="server" Text="ASPxHyperLink" Target="_blank"
                                                                NavigateUrl='<%# Bind("FilePath") %>' OnDataBinding="ASPxHyperLink1_DataBinding">
                                                            </dxe:ASPxHyperLink>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="Description" Name="Description" Caption="توضیحات"
                                                        VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <%--               <dxwgv:GridViewCommandColumn Width="30px" Caption=" " VisibleIndex="2">
                                                                            <DeleteButton Visible="True">
                                                                            </DeleteButton>
                                                                        </dxwgv:GridViewCommandColumn>--%>
                                                </Columns>
                                               
                                            </TSPControls:CustomAspxDevGridView2>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
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
                                    <td style="vertical-align: top;">
                                        <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldJobHistory">
                                        </dxhf:ASPxHiddenField>
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                            CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
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
                                                            CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/edit.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                                            ValidationGroup="j" ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnSave_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
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
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                            CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
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
    <asp:HiddenField ID="MemberId" runat="server" Visible="False" />
    <asp:HiddenField ID="MemberRequest" runat="server" Visible="False" />
    <asp:HiddenField ID="JobId" runat="server" Visible="False" />
    <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
    <asp:ObjectDataSource ID="ODBJobCountry" runat="server"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.CountryManager" UpdateMethod="Update"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbPrType" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.ProjectTypeManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbSazeType" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.SazeTypeManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbCorType" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.CorporationTypeManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbJobPosition" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.ProjectJobPositionManager"></asp:ObjectDataSource>
    <dxhf:ASPxHiddenField ID="HD_Flp" runat="server" ClientInstanceName="HDflp">
    </dxhf:ASPxHiddenField>
    <dxhf:ASPxHiddenField ID="HDFlpMember" runat="server" ClientInstanceName="flpme">
    </dxhf:ASPxHiddenField>
    <asp:ObjectDataSource ID="OdbFactorDocuments" runat="server" FilterExpression="Type={0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.DocOffOfficeFactorDocumentsManager">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
    </asp:ObjectDataSource>

</asp:Content>
