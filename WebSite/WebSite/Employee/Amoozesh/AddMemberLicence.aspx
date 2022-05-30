<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddMemberLicence.aspx.cs" Inherits="Employee_Amoozesh_AddMemberLicence"
    Title="مشخصات مدرک" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script language="javascript">
        function contentPageLoad(sender, args) {
            if (cmb.GetSelectedIndex() == 0)
                SetPeriod();
            else
                SetSeminar();
        }
        function SetControlValues() {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'MeId;FirstName;LastName', SetValue);
        }
        function SetValue(values) {
            ID.SetText(values[0]);
            mFirstName.SetText(values[1]);
            mLastName.SetText(values[2]);
        }
        function SetPeriod() {

            document.getElementById('tr6').style.display = 'none';
            document.getElementById('tr7').style.display = 'none';
            document.getElementById('tr9').style.display = 'none';

            document.getElementById('tr5').style.display = 'inline';
            document.getElementById('tr10').style.display = 'inline';
            document.getElementById('tr11').style.display = 'inline';
            document.getElementById('tr13').style.display = 'inline';
            document.getElementById('tr14').style.display = 'inline';

        }
        function SetSeminar() {

            document.getElementById('tr6').style.display = 'inline';
            document.getElementById('tr7').style.display = 'inline';
            document.getElementById('tr9').style.display = 'inline';

            document.getElementById('tr5').style.display = 'none';
            document.getElementById('tr10').style.display = 'none';
            document.getElementById('tr11').style.display = 'none';
            document.getElementById('tr13').style.display = 'none';
            document.getElementById('tr14').style.display = 'none';
        }
        function SetEmpty() {
            ID.SetText("");
            mFirstName.SetText("");
            mLastName.SetText("");

            mdType.SetSelectedIndex(-1);
            LiNo.SetText("");
            document.getElementById('<%=txtLiDate.ClientID%>').value = "";
            crsId.SetSelectedIndex(-1);
            PPCode.SetText("");
            SeName.SetText("");
            SeTeName.SetText("");
            document.getElementById('<%=txtStartDate.ClientID%>').value = "";
            document.getElementById('<%=txtEndDate.ClientID%>').value = "";
            SeDuration.SetText("");
            TimeMark.SetText("");
            TotalMark.SetText("");
            PPDuration.SetText("");
            InsName.SetText("");
            InsRegNo.SetText("");
            PPTeName.SetText("");
            TeFileNo.SetText("");
            Desc.SetText("");
            PrId.SetSelectedIndex(-1);
            document.getElementById('<%=txtTestDate.ClientID%>').value = "";
            document.getElementById('<%=txtInsRegDate.ClientID%>').value = "";
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <div runat="server" id="DivReport" style="text-align: right" class="DivErrors">
                    <asp:Label runat="server" Text="Label" ID="LabelWarning"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>

                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>



                                    <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                        width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: top">
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                        cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False"
                                                                        Text=" "  EnableTheming="False" ToolTip="جدید"
                                                                        ID="BtnNew" EnableViewState="False" OnClick="BtnNew_Click">
                                                                        <Image  Url="~/Images/icons/new.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False"
                                                                        Text=" "  Width="25px" EnableTheming="False" ToolTip="ویرایش"
                                                                        ID="btnEdit" EnableViewState="False" OnClick="btnEdit_Click">
                                                                        <Image  Url="~/Images/icons/edit.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                                        EnableTheming="False" ToolTip="ذخیره" ID="btnSave" EnableViewState="False" OnClick="btnSave_Click">
                                                                        <Image  Url="~/Images/icons/save.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False"
                                                                        Text=" "  EnableTheming="False" ToolTip="بازگشت"
                                                                        ID="btnBack" EnableViewState="False" OnClick="btnBack_Click">
                                                                        <Image  Url="~/Images/icons/Back.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
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
                <br />
            	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

 
                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" width="20%">
                                            <dxe:ASPxLabel runat="server" Wrap="False" Text="کد عضویت *" ClientInstanceName="lblMe"
                                                ID="lblMeId">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="30%">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"  
                                                ClientInstanceName="ID" ID="txtMeNo" OnTextChanged="txtMeNo_TextChanged" AutoPostBack="true">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="True" ErrorText=" کد عضویت را وارد نمایید"></RequiredField>
                                                    <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت عدد صحیح می باشد" />
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right" width="20%">
                                        </td>
                                        <td valign="top" align="right" width="30%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام" ClientInstanceName="lblMname" ID="lblMeFirstName">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ReadOnly="True"  
                                                ClientInstanceName="mFirstName" ID="txtMeFirstName">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=""></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Wrap="False" Text="نام خانوادگی" ClientInstanceName="lblMfamily"
                                                ID="lblMeLastName">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ReadOnly="True"  
                                                ClientInstanceName="mLastName" ID="txtMeLastName">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=""></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نوع" ID="ASPxLabel5">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                                SelectedIndex="0" ValueType="System.String"
                                                Width="100%"   
                                                ClientInstanceName="cmb" ID="ComboType" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
     SetEmpty();
	if(cmb.GetSelectedIndex()==0)
	    
		SetPeriod();
	
	else

		SetSeminar();
		
}"></ClientSideEvents>
                                                <Items>
                                                    <dxe:ListEditItem Text="دوره" Value="0" Selected="True"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Text="سمینار" Value="1"></dxe:ListEditItem>
                                                </Items>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="True" ErrorText="نوع مدرک را انتخاب نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نوع مدرک *" ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                                ValueType="System.String" Width="100%" 
                                                  ClientInstanceName="mdType"
                                                ID="ComboMdType" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <Items>
                                                    <dxe:ListEditItem Text="اولیه" Value="0"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Text="انتقالی" Value="1"></dxe:ListEditItem>
                                                </Items>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="True" ErrorText="نوع مدرک را انتخاب نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Wrap="False" Text="شماره گواهینامه *" ID="ASPxLabel8">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"  
                                                ClientInstanceName="LiNo" ID="txtLiNo">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="شماره را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Wrap="False" Text="تاریخ گواهینامه *" ID="ASPxLabel6">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                PickerDirection="ToRight" ShowPickerOnTop="True" Width="230px" ID="txtLiDate"
                                                Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLiDate" ErrorMessage="تاریخ را وارد نمایید"
                                                Display="Dynamic" ID="RequiredFieldValidator4">تاریخ را وارد نمایید</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="عنوان *" ID="ASPxLabel7">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" DropDownWidth="100%" EnableIncrementalFiltering="True"
                                                IncrementalFilteringMode="StartsWith" ValueType="System.Int32" DataSourceID="odbCourseName"
                                                TextField="CrsTitle" ValueField="CrsId" TextFormatString="{2}" Width="100%"   
                                                ClientInstanceName="crsId" ID="ComboCrsName" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <Columns>
                                                    <dxe:ListBoxColumn FieldName="NonPracticalDuration" Width="80px" Caption="تعداد ساعات">
                                                    </dxe:ListBoxColumn>
                                                    <dxe:ListBoxColumn FieldName="CrsCode" Width="80px" Caption="کد درس"></dxe:ListBoxColumn>
                                                    <dxe:ListBoxColumn FieldName="CrsTitle" Width="420px" Caption="عنوان"></dxe:ListBoxColumn>
                                                </Columns>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="True" ErrorText="عنوان مدرک را انتخاب نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد دوره" ID="ASPxLabel9">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"  
                                                ClientInstanceName="PPCode" ID="txtPPCode">
                                                <ValidationSettings ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText="کد دوره را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Wrap="False" Text="عنوان سمینار *" ID="ASPxLabel21">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"  
                                                ClientInstanceName="SeName" ID="txtSeName">
                                                <ValidationSettings ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="عنوان را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Wrap="False" Text="ارائه دهنده *" ID="ASPxLabel23">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"  
                                                ClientInstanceName="SeTeName" ID="txtSeTeName">
                                                <ValidationSettings ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="ارائه دهنده را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Wrap="False" Text="تاریخ شروع *" ID="ASPxLabel10">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                PickerDirection="ToRight" ShowPickerOnTop="True" Width="230px" ID="txtStartDate"
                                                Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtStartDate" ErrorMessage="تاریخ را وارد نمایید"
                                                Display="Dynamic" ID="RequiredFieldValidator2">تاریخ را وارد نمایید</asp:RequiredFieldValidator>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Wrap="False" Text="تاریخ پایان *" ID="ASPxLabel12">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                PickerDirection="ToRight" ShowPickerOnTop="True" Width="230px" ID="txtEndDate"
                                                Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEndDate" ErrorMessage="تاریخ را وارد نمایید"
                                                Display="Dynamic" ID="RequiredFieldValidator3">تاریخ را وارد نمایید</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="مدت زمان سمینار(ساعت) *" Width="144px" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" MaxLength="6"  
                                                ClientInstanceName="SeDuration" ID="txtSeDuration">
                                                <ValidationSettings ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="مدت زمان را وارد نمایید"></RequiredField>
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
                                            <dxe:ASPxLabel runat="server" Wrap="False" Text="امتیاز اخذ شده(ساعت)" ID="ASPxLabel24">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"  
                                                ClientInstanceName="TimeMark" ID="txtTimeMark">
                                                <ValidationSettings ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText=""></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Wrap="False" Text="امتیاز کل *" ID="ASPxLabel25">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"  
                                                ClientInstanceName="TotalMark" ID="txtTotalMark">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="امتیاز کل را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Wrap="False" Text="مدت زمان دوره(ساعت) *" ID="ASPxLabel11">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" MaxLength="6"  
                                                ClientInstanceName="PPDuration" ID="txtPPDuration">
                                                <ValidationSettings ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="مدت زمان را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Wrap="False" Text="تاریخ آزمون" ID="ASPxLabel20">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                PickerDirection="ToRight" ShowPickerOnTop="True" Width="230px" ID="txtTestDate"
                                                Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام مؤسسه *" ID="ASPxLabel13">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"  
                                                ClientInstanceName="InsName" ID="txtInsName">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="مؤسسه را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="استان" ID="ASPxLabel16">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                                ValueType="System.String" DataSourceID="OdbProvince" TextField="PrName" ValueField="PrId"
                                                Width="100%" 
                                                  ClientInstanceName="PrId"
                                                ID="ComboPrId" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RequiredField ErrorText=""></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Wrap="False" Text="شماره مجوز فعالیت" ID="ASPxLabel14">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"  
                                                ClientInstanceName="InsRegNo" ID="txtInsRegNo" Style="direction: ltr">
                                                <ValidationSettings ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText="مکان را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Wrap="False" Text="تاریخ اخذ مجوز" ID="ASPxLabel15">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                PickerDirection="ToRight" ShowPickerOnEvent="OnClick" ShowPickerOnTop="True"
                                                Width="230px" ID="txtInsRegDate" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="مدرس دوره *" ID="ASPxLabel17">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"  
                                                ClientInstanceName="PPTeName" ID="txtPPTeName">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="مدرس را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Wrap="False" Text="پروانه اشتغال به کار" ID="ASPxLabel18">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"  
                                                ClientInstanceName="TeFileNo" ID="txtTeFileNo" Style="direction: ltr">
                                                <ValidationSettings ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText="مکان را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="توضیحات" ID="Label3"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="32px" Width="100%"  
                                                ClientInstanceName="Desc" ID="txtDesc">
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="فایل" ID="Label1"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td valign="top" align="right">
                                                           <%--  <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                ID="flp" InputType="Files" ClientInstanceName="flpc" OnFileUploadComplete="flp_FileUploadComplete">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid)
	imgEndUploadImgClient3.SetVisible(true);
	else
	imgEndUploadImgClient3.SetVisible(false);	
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxUploadControl>--%>

                                                            <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"  InputType="Files"
                                                                ClientInstanceName="flpc" ID="flpImage" OnFileUploadComplete="flpImage_FileUploadComplete">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
	if(e.isValid)
    {                                                                    
    imgEndUploadImgClient.SetVisible(true);                                                                       
	hp.SetVisible(true);                                                                    
	hp.SetNavigateUrl('../../Image/Amoozesh/Madrak/'+e.callbackData);                                                                    
    }
    else 
    imgEndUploadImgClient.SetVisible(false);
}"></ClientSideEvents>
                                                                <CancelButton Text="انصراف">
                                                                </CancelButton>
                                                            </TSPControls:CustomAspxUploadControl>
                                                        </td>
                                                        <td valign="middle" align="right">
                                                            <dxe:ASPxImage runat="server" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد"
                                                                ClientInstanceName="imgEndUploadImgClient" ClientVisible="False" ID="imgEndUploadImg">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <dxe:ASPxHyperLink runat="server" Text="آدرس فایل" Target="_blank" ClientInstanceName="hp"
                                                ClientVisible="False" ID="HpFile">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                              </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                <asp:HiddenField runat="server" ID="MadrakId" Visible="False"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="PgMode" Visible="False"></asp:HiddenField>
                <br />
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


 
                                    <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                        width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: top">
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                        cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False"
                                                                        Text=" "  EnableTheming="False" ToolTip="جدید"
                                                                        ID="BtnNew2" EnableViewState="False" OnClick="BtnNew_Click">
                                                                        <Image  Url="~/Images/icons/new.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False"
                                                                        Text=" "  Width="25px" EnableTheming="False" ToolTip="ویرایش"
                                                                        ID="btnEdit2" EnableViewState="False" OnClick="btnEdit_Click">
                                                                        <Image  Url="~/Images/icons/edit.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                                        EnableTheming="False" ToolTip="ذخیره" ID="btnSave2" EnableViewState="False" OnClick="btnSave_Click">
                                                                        <Image  Url="~/Images/icons/save.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                                        </HoverStyle>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False"
                                                                        Text=" "  EnableTheming="False" ToolTip="بازگشت"
                                                                        ID="ASPxButton6" EnableViewState="False" OnClick="btnBack_Click">
                                                                        <Image  Url="~/Images/icons/Back.png">
                                                                        </Image>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
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
                <asp:ObjectDataSource runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetData" TypeName="TSP.DataManager.CourseManager" ID="odbCourseName">
                </asp:ObjectDataSource>
                <asp:ObjectDataSource runat="server" CacheDuration="3600" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetData" TypeName="TSP.DataManager.ProvinceManager" ID="OdbProvince">
                    <FilterParameters>
                        <asp:Parameter Name="EpId"></asp:Parameter>
                    </FilterParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource runat="server" CacheDuration="30" DeleteMethod="Delete" EnableCaching="True"
                    InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectMemberByName"
                    TypeName="TSP.DataManager.MemberManager" UpdateMethod="Update" ID="OdbLastName">
                   
                    <SelectParameters>
                        <asp:Parameter DefaultValue="%" Name="FirstName" Type="String"></asp:Parameter>
                        <asp:Parameter DefaultValue="" Name="LastName" Type="String"></asp:Parameter>
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
