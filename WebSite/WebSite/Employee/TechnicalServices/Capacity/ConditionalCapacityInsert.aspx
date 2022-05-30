<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="ConditionalCapacityInsert.aspx.cs" Inherits="Employee_TechnicalServices_Capacity_ConditionalCapacityInsert"
    Title="کاهش/افزایش ظرفیت" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/UserControl/MemberCapacityUserControl.ascx" TagPrefix="TSP" TagName="MemberCapacity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function CheckDate() {
            var StartDate = document.getElementById('<%=FromDate.ClientID%>').value;
            var EndDate = document.getElementById('<%=ToDate.ClientID%>').value;       
            if (EndDate < StartDate && EndDate != "")
                return -1;
            else
                return 1;
        }

        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }

    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#"><span style="color: #000000">بستن</span></a>]</div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                            CausesValidation="False" ID="btnNew" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnNew_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/new.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
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
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                            ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnSave_Click">
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
    if(ASPxComboBoxMemberType.GetValue() == 4)
       e.processOnServer= confirm('آیا مطمئن به ورود اطلاعات ظرفیت به صورت گروهی هستید؟');  
}" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4">
                                                        </TSPControls:MenuSeprator>
                                                    </td>
                                                    <td align="left" valign="top">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گزارش ریز کارکرد اعضا"
                                                            CausesValidation="False" ID="btnMemberOperationReport" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False">
                                                            <ClientSideEvents Click="function(s,e){
                                                              window.open(HiddenFieldCapacity.Get('ReportURL'));
                                                            }" />
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/Report.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                 
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2">
                                                        </TSPControls:MenuSeprator>
                                                    </td>
                                                    <td>
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
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelGroupSaving" HeaderText="" ShowHeader="false"
                runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tr>
                                <td colspan="2" width="50px" align="center" valign="top">
                                    <dxe:ASPxLabel runat="server" Text="افزایش/کاهش با موفقیت انجام شد" ID="lblMeGroupSaveMessage"
                                        Font-Bold="true" Width="100%" ForeColor="Green">
                                    </dxe:ASPxLabel>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td width="50px" align="left" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False"
                                        ClientInstanceName="btnBack3" Text="&nbsp;&nbsp;&nbsp;بازگشت به مدیریت کاهش/افزایش"
                                        Width="240px" ID="btnBack3"  
                                        PostBackUrl="ConditionalCapacity.aspx">
                                        <Image Width="20px" Height="20px" Url="~/Images/icons/back.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="50px" align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="&nbsp;&nbsp;&nbsp;افزایش/ کاهش جدید" Width="240px"
                                        ID="btnNewCondition" OnClick="btnNewCondition_Click" 
                                         AutoPostBack="true" CausesValidation="false">
                                        <Image Width="20px" Height="20px" Url="~/Images/icons/New.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomAspxCallbackPanel ID="CallbackCapacity" runat="server" ClientInstanceName="CallbackCapacity"
                OnCallback="CallbackCapacity_Callback" Width="100%">
                <ClientSideEvents EndCallback="function(s, e) {
    if(s.cpError==1)
     {
       ShowMessage(s.cpMsg);
       s.cpError=0;
       s.cpMsg = '';
     }
}" />
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent1" runat="server">
                        <TSPControls:CustomASPxRoundPanel ID="RoundPanelMain" HeaderText="مشاهده" runat="server"
                            Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent>
                                                <fieldset><legend class="HelpUL">اطلاعات عضو
                                                          </legend>
                                                <table dir="rtl" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td valign="top" align="right" width="15%">
                                                                <asp:Label runat="server" Text="نوع عضو" ID="Label4"></asp:Label>
                                                            </td>
                                                            <td valign="top" align="right" width="35%">
                                                                <TSPControls:CustomAspxComboBox runat="server"  
                                                                    ID="ASPxComboBoxMemberType" ClientInstanceName="ASPxComboBoxMemberType" ValueType="System.String"
                                                                     RightToLeft="True" Width="100%" >
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <Items>
                                                                        <dxe:ListEditItem Text="شخص حقیقی" Value="1" />
                                                                        <dxe:ListEditItem Text="شخص حقوقی" Value="2" />
                                                                        <dxe:ListEditItem Text="دفتر" Value="3" />
                                                                        <dxe:ListEditItem Text="انتخاب گروهی اعضا" Value="4" />
                                                                    </Items>
                                                                    <ValidationSettings ValidationGroup="MemberValidation" Display="Dynamic" ErrorTextPosition="Bottom">
                                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                        <RequiredField ErrorText="نوع عضو را انتخاب نمایید" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <ButtonStyle Width="13px">
                                                                    </ButtonStyle>
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
CallbackCapacity.PerformCallback('ChangeMe'+';'+ ASPxComboBoxMemberType.GetValue());
}" />
                                                                </TSPControls:CustomAspxComboBox>
                                                            </td>
                                                            <td valign="top" align="right" width="15%">
                                                                <asp:Label runat="server" Text="کد عضویت" ID="lblId"></asp:Label>
                                                            </td>
                                                            <td valign="top" align="right" width="35%">
                                                                <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxMeOfficeEngOId" 
                                                                    Width="100%"  ClientInstanceName="txtMeId">
                                                                    <ClientSideEvents TextChanged="function(s, e) {
if(ASPxClientEdit.ValidateGroup('MemberValidation'))
   CallbackCapacity.PerformCallback('SearchMe'+';'+ txtMeId.GetText());
}" />
                                                                    <ValidationSettings ValidationGroup="MemberValidation" Display="Dynamic" ErrorTextPosition="Bottom">
                                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <RequiredField IsRequired="True" ErrorText="لطفاً کد عضویت را وارد کنید"></RequiredField>
                                                                        <RegularExpression ErrorText="کد عضویت را با فرمت صحیح وارد نمایید" ValidationExpression="\d*" />
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" valign="top" align="right" width="100%">
                                                                <table runat="server" width="100%" id="tblMemberInfo">
                                                                    <tr>
                                                                        <td valign="top" align="right" width="15%">
                                                                            <asp:Label runat="server" Text="نام" ID="Label3"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="right" width="35%">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtMemberName"  Width="100%"
                                                                                ClientEnabled="false"  ClientInstanceName="txtMemberName">
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                        <td valign="top" align="right" width="15%">
                                                                            <asp:Label runat="server" Text="شماره عضویت" ID="Label9"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" dir="ltr" align="right" width="35%">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtMeNo"  Width="100%" ClientEnabled="false"
                                                                                 ClientInstanceName="txtMeNo">
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" align="right">
                                                                            <asp:Label runat="server" Text="شماره پروانه" ID="Label10"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" dir="ltr" align="right">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtFileNo"  Width="100%" ClientEnabled="false"
                                                                                 ClientInstanceName="txtFileNo">
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <asp:Label runat="server" Text="تاریخ اعتبار پروانه" ID="Label11"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" dir="ltr" align="right">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtFileDate"  Width="100%"
                                                                                ClientEnabled="false"  ClientInstanceName="txtMeNo">
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" align="right">
                                                                            <asp:Label runat="server" Text="پایه نظارت" ID="Label12"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtObs"  Width="100%" ClientEnabled="false"
                                                                                 ClientInstanceName="txtObs">
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <asp:Label runat="server" Text="پایه اجرا" ID="Label13"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtImp"  Width="100%" ClientEnabled="false"
                                                                                 ClientInstanceName="txtImp">
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" align="right">
                                                                            <asp:Label runat="server" Text="پایه طراحی" ID="Label14"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtDesign"  Width="100%" ClientEnabled="false"
                                                                                 ClientInstanceName="txtDesign">
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <asp:Label runat="server" Text="پایه شهرسازی" ID="Label15"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtUrbanism"  Width="100%"
                                                                                ClientEnabled="false"  ClientInstanceName="txtUrbanism">
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" align="right">
                                                                            <asp:Label runat="server" Text="پایه ترافیک" ID="Label16"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtTraffic"  Width="100%" ClientEnabled="false"
                                                                                 ClientInstanceName="txtTraffic">
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <asp:Label runat="server" Text="پایه نقشه برداری" ID="Label17"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtMapping"  Width="100%" ClientEnabled="false"
                                                                                 ClientInstanceName="txtMapping">
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table runat="server" width="100%" id="tblOfficeInfo">
                                                                    <tr>
                                                                        <td valign="top" align="right" width="15%">
                                                                            <asp:Label runat="server" Text="نام شرکت" ID="Label18"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="right" width="35%">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtOfficeName"  Width="100%"
                                                                                ClientEnabled="false"  ClientInstanceName="txtOfficeName">
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                        <td valign="top" align="right" width="15%">
                                                                            <asp:Label runat="server" Text="موضوع شرکت" ID="Label19"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="right" width="35%">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtOfficeSubject"  Width="100%"
                                                                                ClientEnabled="false"  ClientInstanceName="txtOfficeSubject">
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" align="right">
                                                                            <asp:Label runat="server" Text="نام مدیرعامل" ID="Label20"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtOfficeManager"  Width="100%"
                                                                                ClientEnabled="false"  ClientInstanceName="txtOfficeManager">
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <asp:Label runat="server" Text="تعداد اعضا" ID="Label21"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtMembersCount"  Width="100%"
                                                                                ClientEnabled="false"  ClientInstanceName="txtMembersCount">
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table runat="server" width="100%" id="tblEngOfficeInfo">
                                                                    <tr>
                                                                        <td valign="top" align="right" width="15%">
                                                                            <asp:Label runat="server" Text="نام دفتر" ID="Label22"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="right" width="35%">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtEngOffName"  Width="100%"
                                                                                ClientEnabled="false"  ClientInstanceName="txtEngOffName">
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                        <td valign="top" align="right" width="15%">
                                                                            <asp:Label runat="server" Text="نوع دفتر" ID="Label23"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="right" width="35%">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtEngOffType"  Width="100%"
                                                                                ClientEnabled="false"  ClientInstanceName="txtEngOffType">
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" align="right">
                                                                            <asp:Label runat="server" Text="نام مدیرمسئول" ID="Label24"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtEngOffManager"  Width="100%"
                                                                                ClientEnabled="false"  ClientInstanceName="txtEngOffManager">
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table runat="server" width="100%" id="TblGroupMemberInfo">
                                                                    <tr>
                                                                        <td valign="top" align="right">
                                                                            <asp:Label runat="server" Text="رشته" ID="Label27"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomAspxComboBox runat="server"  Width="100%" 
                                                                                TextField="MjName" ID="cmbMajor" DataSourceID="ODBParentMajor" RightToLeft="True"
                                                                                ValueType="System.String" ValueField="MjId" ClientInstanceName="cmbMajor" 
                                                                                AutoPostBack="false">
                                                                                <ClientSideEvents SelectedIndexChanged="function(s,e){
                                                                                CallbackCapacity.PerformCallback('MeGroupMajorChange');
                                                                                }" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" >
                                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                    </ErrorImage>
                                                                                    <RequiredField IsRequired="True" ErrorText="رشته را انتخاب نمایید"></RequiredField>
                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>
                                                                            </TSPControls:CustomAspxComboBox>
                                                                            <asp:ObjectDataSource ID="ODBParentMajor" runat="server" SelectMethod="FindMjParents"
                                                                                TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" align="right" width="15%">
                                                                            <dxe:ASPxLabel ID="lblGradeObs" ClientInstanceName="lblGradeObs" runat="server" Text="پایه نظارت">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td valign="top" align="right" width="35%">
                                                                            <TSPControls:CustomAspxComboBox runat="server"  
                                                                                ID="cmbGradeObs" ClientInstanceName="cmbGradeObs" ValueType="System.String" 
                                                                                DataSourceID="ObjdGrade" RightToLeft="True" TextField="GrdName" ValueField="GrdId"
                                                                                Width="100%">
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" >
                                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                    </ErrorImage>
                                                                                    <RequiredField IsRequired="True" ErrorText="پایه را انتخاب نمایید"></RequiredField>
                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>
                                                                            </TSPControls:CustomAspxComboBox>
                                                                            <asp:ObjectDataSource ID="ObjdGrade" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.GradeManager">
                                                                            </asp:ObjectDataSource>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <asp:Label runat="server" Text="پایه طراحی" ID="lblGradeDes"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomAspxComboBox runat="server"  
                                                                                ID="cmbGradeDesign" ClientInstanceName="cmbGradeDesign" ValueType="System.String"
                                                                                 DataSourceID="ObjdGrade" RightToLeft="True"
                                                                                TextField="GrdName" ValueField="GrdId" Width="100%">
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" >
                                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                    </ErrorImage>
                                                                                    <RequiredField IsRequired="True" ErrorText="پایه را انتخاب نمایید"></RequiredField>
                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>
                                                                            </TSPControls:CustomAspxComboBox>
                                                                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
                                                                                TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>                                                                       
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table></fieldset>                                                                             
                                    <dx:ASPxPanel runat="server" ID="RoundPanelCapacity">
                                        <PanelCollection><dx:PanelContent>

                                                      
                                     <fieldset><legend class="HelpUL">اطلاعات ظرفیت دوره جاری
                                                          </legend>
                                                <table dir="rtl" width="100%">
                                                    <tr>
                                                        <td colspan="4" align="right" valign="top" dir="rtl">
                                                            <b>
                                                                <ul style="font-family=tahoma; font-size: 8pt; line-height: 15pt; color: DarkRed">
                                                                    <li>ظرفیت نظارت =ظرفیت طراحی * ضریب تبدیل طراحی به نظارت </li>
                                                                    <li>ظرفیت کل طراحی = حداکثر ظرفیت مجاز بر اساس پایه شخص + ظرفیت موثر دفتر/ شرکت +مقدار
                                                                        افزایش/کاهش ظرفیت طراحی + (مقدار افزایش/کاهش ظرفیت نظارت ÷ ضریب تبدیل نظارت به طراحی)
                                                                    </li>
                                                                    <li>تبدیل ظرفیت نظارت و طراحی در صورتی که شخص فاقد یکی از صلاحیت های نظارت و یا طراحی
                                                                        باشد ، قابل استفاده نمی باشد. </li>
                                                                    <li>در صورتی که پروانه اشتغال به کار شخص دارای رشته موضوع پروانه نقشه برداری و صلاحیت
                                                                        رشته نقشه برداری باشد ، صلاحیت نظارت وی بر اساس پایه صلاحیت نقشه برداری وی محاسبه
                                                                        می گردد.این شخص از ظرفیت نظارت خود به عنوان ناظر نقشه بردار می تواند استفاده نماید.
                                                                    </li>
                                                                    <li>داشتن ظرفیت نظارت به مفهوم توانایی شخص جهت فعالیت نظارت نمی باشد. در صورتی که قصد
                                                                        انجام کار نظارت به عنوان شخص حقیقی دارند، بایستی دارای مجوز فعالیت ناظر حقیقی(مجوز
                                                                        نظارت) باشند </li>
                                                                    <li>در هنگام ثبت یک عضو حقیقی به عنوان ناظر 'ظرفیت موثر دفتر/ شرکت' در نظر گرفته نمی
                                                                        شود.در این حالت ظرفیت نظارت به صورت زیر محاسبه می گردد:
                                                                        <br />
                                                                        ظرفیت کل نظارت = حداکثر ظرفیت مجاز بر اساس پایه شخص + (مقدار افزایش/کاهش ظرفیت طراحی
                                                                        × ضریب تبدیل طراحی به نظارت) + (مقدار افزایش/کاهش ظرفیت نظارت) </li>
                                                                    <li>داشتن ظرفیت اجرا به مفهوم توانایی شخص جهت فعالیت اجرا نمی باشد. در صورتی که قصد
                                                                        انجام کار اجرا به عنوان شخص حقیقی دارند، بایستی دارای <u>مجوز اجرا</u> باشد و یا
                                                                        اینکه به عنوان <u>عضو شرکت (مجری حقوقی)</u> کار را دریافت نماید و از ظرفیت شرکت
                                                                        استفاده نماید. </li>
                                                                </ul>
                                                            </b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <TSP:MemberCapacity ID="MemberCapacityUCDesign" runat="server" />
                                                        </td>
                                                    </tr>
                                                  <%--  <tr>
                                                        <td colspan="4" align="center" valign="top" dir="rtl">
                                                            <TSP:MemberCapacity ID="MemberCapacityUCImplement" runat="server" />
                                                        </td>
                                                    </tr>--%>
                                                </table>
                                            </fieldset>   </dx:PanelContent></PanelCollection>
                                    </dx:ASPxPanel>
                                  <fieldset><legend class="HelpUL">کاهش/افزایش ظرفیت
                                                          </legend>
                                 
                                                <table dir="rtl" width="100%">
                                                    <tr>
                                                        <td valign="top" align="right">
                                                            <asp:Label runat="server" Text="کد پروژه" ID="lblProjectId"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxProjectId" ClientInstanceName="ASPxTextBoxProjectId"
                                                                 Width="100%" >
                                                                <ClientSideEvents TextChanged="function(s, e) {
if(ASPxClientEdit.ValidateGroup('ProjectVaildation'))
   CallbackCapacity.PerformCallback('SearchProj'+';'+ ASPxTextBoxProjectId.GetText());
}" />
                                                                <ValidationSettings ValidationGroup="ProjectVaildation" Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <RegularExpression ErrorText="عدد صحیح وارد کنید" ValidationExpression="(-)?(\d)+">
                                                                    </RegularExpression>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                        <td valign="top" align="right" width="15%">
                                                            <asp:Label runat="server" Text="نام پروژه" ID="lblProjectName"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="right" width="35%">
                                                            <TSPControls:CustomTextBox runat="server" ID="txtProjectName" ClientEnabled="false" 
                                                                Width="100%" >
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right" width="15%">
                                                            <asp:Label runat="server" Text="دلیل" ID="Label1"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="right" colspan="3">
                                                            <TSPControls:CustomAspxComboBox runat="server"  
                                                                ID="ASPxComboBoxReason" ClientInstanceName="ASPxComboBoxReason" ValueType="System.String"
                                                                 DataSourceID="ObjectDataSourceReason"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ASPxComboBoxReason_SelectedIndexChanged"
                                                                TextField="Title" ValueField="ReasonId" RightToLeft="True" Width="100%">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom"  >
                                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                    <RequiredField ErrorText="دلیل را انتخاب نمایید" IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ButtonStyle Width="13px">
                                                                </ButtonStyle>
                                                            </TSPControls:CustomAspxComboBox>
                                                            <dxe:ASPxLabel ID="LabelReasonErr" runat="server" ForeColor="Red" Text="مقدار نامعتبر *"
                                                                Visible="False">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right" width="15%">
                                                            <asp:Label runat="server" Text="ظرفیت" ID="Label29"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="right" width="35%">
                                                            <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxCapacity"  Width="100%"
                                                                >
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom"  >
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <RequiredField IsRequired="True" ErrorText="لطفاً ظرفیت را وارد نمایید"></RequiredField>
                                                                    <RegularExpression ErrorText="عدد صحیح وارد کنید" ValidationExpression="(-)?(\d)+">
                                                                    </RegularExpression>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                        <td valign="top" align="right" width="15%">
                                                            <asp:Label runat="server" Text="صلاحیت" ID="Label5"></asp:Label>
                                                        </td>
                                                        <td dir="ltr" valign="top" align="right" width="35%">
                                                            <TSPControls:CustomAspxComboBox runat="server"  
                                                                ID="ASPxComboBoxProjectIngridientType" ValueType="System.String" 
                                                                DataSourceID="ObjectDataSourceProjectIngridientType" RightToLeft="True" TextField="Type"
                                                                ValueField="ProjectIngridientTypeId" Width="100%">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom"  >
                                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                    <RequiredField ErrorText="صلاحیت را انتخاب نمایید" IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ButtonStyle Width="13px">
                                                                </ButtonStyle>
                                                            </TSPControls:CustomAspxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">
                                                            <asp:Label runat="server" Text="از تاریخ" ID="Label2"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <pdc:PersianDateTextBox ID="FromDate" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                                PickerDirection="ToRight" ShowPickerOnTop="True" Width="250px" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPlansMethodDate" runat="server"
                                                                ControlToValidate="FromDate" ErrorMessage="تاریخ را وارد نمایید"  ></asp:RequiredFieldValidator>
                                                            <br />
                                                            <dxe:ASPxLabel ID="ASPxLabel2" runat="server" ClientInstanceName="lblDateError" ClientVisible="False"
                                                                ForeColor="Red" Text="محدوده تاریخ وارد شده صحیح نمی باشد">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <asp:Label runat="server" Text="تا تاریخ" ID="LabelToDate"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <pdc:PersianDateTextBox ID="ToDate" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                                PickerDirection="ToRight" ShowPickerOnTop="True" Width="250px" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTodate" runat="server" ControlToValidate="ToDate"
                                                                ErrorMessage="تاریخ را وارد نمایید"  ></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">
                                                            <asp:Label runat="server" Text="توضیحات" ID="Label7"></asp:Label>
                                                        </td>
                                                        <td valign="top" align="right" colspan="3">
                                                            <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="ASPxMemoDescription" 
                                                                Width="100%" >
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
                                                        <td style="height: 36px" valign="top" align="right" colspan="2">
                                                            <TSPControls:CustomASPxCheckBox Visible="false" runat="server" Text="تایید شده" ID="ASPxCheckBoxIsConfirmed"
                                                                ReadOnly="True">
                                                            </TSPControls:CustomASPxCheckBox>
                                                        </td>
                                                        <td style="height: 36px" valign="top" align="right" colspan="2">
                                                        </td>
                                                    </tr>
                                                </table>
                                   </fieldset>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </TSPControls:CustomASPxRoundPanel>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomAspxCallbackPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                            CausesValidation="False" ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnNew_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/new.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
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
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                            OnClick="btnSave_Click">
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
 //if(ASPxClientEdit.ValidateGroup('ConditionalCap')==true)
    if(ASPxComboBoxMemberType.GetValue() == 4)
       e.processOnServer= confirm('آیا مطمئن به ورود اطلاعات ظرفیت به صورت گروهی هستید؟');  
}" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5">
                                                        </TSPControls:MenuSeprator>
                                                    </td>
                                                    <td align="left" valign="top">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گزارش ریز کارکرد اعضا"
                                                            CausesValidation="False" ID="btnMemberOperationReport2" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False">
                                                            <ClientSideEvents Click="function(s,e){
                                                              window.open(HiddenFieldCapacity.Get('ReportURL'));
                                                            }" />
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/Report.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                  
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                                        </TSPControls:MenuSeprator>
                                                    </td>
                                                    <td>
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
            <asp:ObjectDataSource ID="ObjectDataSourceProjectIngridientType" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.TechnicalServices.ProjectIngridientTypeManager" FilterExpression="ProjectIngridientTypeId<{0}">
                <FilterParameters>
                    <asp:Parameter Name="newparameter" />
                </FilterParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceReason" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.TechnicalServices.ReasonManager">
                <FilterParameters>
                    <asp:Parameter Name="newparameter" />
                </FilterParameters>
            </asp:ObjectDataSource>
            <asp:HiddenField ID="PkConditionalCapacityId" runat="server" Visible="False" />
            <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
            <dxhf:ASPxHiddenField ID="HiddenFieldCapacity" ClientInstanceName="HiddenFieldCapacity"
                runat="server">
            </dxhf:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                <img id="IMG1" src="../../../Image/indicator.gif" align="middle" />
                صبر نمایید ...</div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
