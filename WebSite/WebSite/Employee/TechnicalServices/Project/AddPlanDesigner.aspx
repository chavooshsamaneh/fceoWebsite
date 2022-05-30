<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="AddPlanDesigner.aspx.cs" Inherits="Employee_TechnicalServices_Project_AddPlanDesigner"
    Title="مشخصات طراح نقشه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<%@ Register Src="~/UserControl/CapacityUserControl.ascx" TagPrefix="TSP" TagName="Capacity" %>
<%@ Register Src="~/UserControl/WFUserControl.ascx" TagName="WFUserControl"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/WorkRequestUserControl.ascx" TagPrefix="TSP" TagName="WorkRequestUserControl" %>
<%@ Register Src="~/UserControl/MeEngOfficeInfoUserControl.ascx" TagPrefix="TSP" TagName="MeEngOfficeInfoUserControl" %>
<%@ Register Src="~/UserControl/MeOfficeInfoUserControl.ascx" TagPrefix="TSP" TagName="MeOfficeInfoUserControlUserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetTaskOrderError(result) {
            //alert(result);
            if (result != null) {

                document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
                document.getElementById("<%=DivReport.ClientID%>").style.display = 'block';  //='visible';
                document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = result;
            }

        }

        function SetDivVisible() {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'hidden';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'none';
        }

        function CheckSaveConditions(value) {
            if (ASPxClientEdit.ValidateGroup() == false && GridViewMembers.GetVisibleRowsOnPage() <= 0) {
                value.processOnServer = false;
                return;
            }
            if (HiddenFieldPrjDes.Get('ShowAlert') == 1) {
                value.processOnServer = confirm(HiddenFieldPrjDes.Get('AlertMsg') + 'آیا با ذخیره اطلاعات موافق می باشید؟');
            }
        }

        function SetLbalesBsedOnMembershipType() {
            if (CmbMembershipType.GetSelectedIndex() == 0) {
                PanelDocumentFileNo.SetVisible(true);
                lbltxtMeIdSearchTitle.SetText('کد عضویت');
            }
            else if (CmbMembershipType.GetSelectedIndex() == 1) {
                PanelDocumentFileNo.SetVisible(false);
                lbltxtMeIdSearchTitle.SetText('كد عضويت كانون كاردان ها');
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
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s,e){
                                                                       CheckSaveConditions(e);
                                                                            }" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار پروژه"
                                            ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/reload.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مالی طراحان"
                                            ID="btnDesAcc" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnDesAcc_Click" CausesValidation="false">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/TS/TSImpAcc.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" Width="25px" ID="btnBack" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
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
            <br />
            <TSP:ProjectInfo ID="prjInfo" runat="server" />
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelDes" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <dxe:ASPxPanel runat="server" ID="RoundPanelSearch">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <fieldset>
                                        <legend class="HelpUL">انتخاب طراح</legend>


                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="right" colspan="4">
                                                        <ul class="HelpUL">
                                                            <li>اشخاص جهت طراحی نقشه بایستی عضو دفتر و یا شرکت باشند. </li>
                                                            <li>تنها اعضای دارای پروانه در رشته عمران می توانند بدون عضویت در دفتر و یا شرکت کار
                                                                                            طراحی معماری و زیر 600 متر انجام دهند. </li>
                                                            <li>با وارد نمودن کد عضویت شخص در صورت عضویت در شرکت و یا دفتر اطلاعات مربوط به شرکت
                                                                                            و یا دفتر وی تکمیل می گردد. </li>
                                                            <li><b>جهت جستجو بر اساس شماره پروانه می توانید ااز یکی از دو فرمت زیر استفاده نمایید.</b></li>
                                                            <li><b>در صورتی که از فرمت تفکیک شده شماره پروانه استفاده می نمایید نیازی به وارد کردن
                                                                                            صفر پیش از عدد در قسمت شماره سریال نمی باشد.</b></li>
                                                            <li><b>در صورتی که از فرمت پیوسته شماره پروانه استفاده می نمایید بایستی شماره پروانه
                                                                                            به صورت کامل و دقیق مطابق با آنچه در واحد پروانه اشتغال ثبت شده است وارد نمایید.(وارد
                                                                                            نمودن صفرهای پیش از عدد در قسمت شماره سریال شماره پروانه الزامی می باشد.)</b></li>
                                                        </ul>
                                                        <dxe:ASPxLabel Font-Bold="true" ForeColor="DarkRed" Visible="false" runat="server"
                                                            Text="" Width="100%" ID="lblWarningsearchOfEngInfo">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right" colspan="4">
                                                        <TSPControls:CustomASPxCheckBox runat="server" Text="با آگاهی کامل نسبت به قوانین سازمان، قصد ثبت اطلاعات بدون در نظر گرفتن کلیه پیش شرط ها را دارم"
                                                            Wrap="False" ID="CheckBoxSaveWithOutCondition">
                                                        </TSPControls:CustomASPxCheckBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right" width="15%">نوع عضویت</td>
                                                    <td valign="top" align="right" width="35%">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                            ID="CmbMembershipType" ClientInstanceName="CmbMembershipType" ValueType="System.String" SelectedIndex="0"
                                                            RightToLeft="True">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                                                SetLbalesBsedOnMembershipType();

}"></ClientSideEvents>
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                                <RequiredField IsRequired="True" ErrorText="نوع عضویت را انتخاب نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <Items>
                                                                <dxe:ListEditItem Value="1" Text="عضو سازمان نظام مهندسی" Selected="True"></dxe:ListEditItem>
                                                                <dxe:ListEditItem Value="4" Text="عضو نظام کاردان ها"></dxe:ListEditItem>
                                                            </Items>
                                                            <ButtonStyle Width="13px">
                                                            </ButtonStyle>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="lbltxtMeIdSearchTitle" ClientInstanceName="lbltxtMeIdSearchTitle">
                                                        </dxe:ASPxLabel>
                                                        :
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtMeIdSearch" OnTextChanged="txtMeIdSearch_TextChanged" AutoPostBack="true" Width="100%"
                                                            ClientInstanceName="txtMeIdSearch">
                                                            <%--<ClientSideEvents TextChanged="function(s, e) {
	
    CallBackDesigner.PerformCallback('FindAllInfo'+';'+txtMeIdSearch.GetText());
}"></ClientSideEvents>--%>
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                                <RegularExpression ErrorText="فیلد کد از جنس عدد صحیح می باشد" ValidationExpression="\d*"></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>

                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxcp:ASPxPanel ID="PanelDocumentFileNo" ClientInstanceName="PanelDocumentFileNo"
                                            runat="server">
                                            <PanelCollection>
                                                <dxcp:PanelContent>
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td valign="top" align="right" width="15%">شماره پروانه:
                                                                </td>
                                                                <td valign="top" align="right" width="35%">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td width="5%">استان
                                                                            </td>
                                                                            <td align="right" width="17%">
                                                                                <TSPControls:CustomTextBox runat="server" ID="txtDocProvCode" Width="100%"
                                                                                    ClientInstanceName="txtDocProvCode">
                                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                                                        <RegularExpression ErrorText="فیلد کد از جنس عدد صحیح می باشد" ValidationExpression="\d*"></RegularExpression>
                                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                        </ErrorFrameStyle>
                                                                                    </ValidationSettings>
                                                                                </TSPControls:CustomTextBox>
                                                                            </td>
                                                                            <td width="5%">رشته
                                                                            </td>
                                                                            <td align="right" width="17%">
                                                                                <TSPControls:CustomTextBox runat="server" ID="txtMjCode" Width="100%" ClientInstanceName="txtMjCode">
                                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                        </ErrorImage>
                                                                                        <RegularExpression ErrorText="فیلد کد از جنس عدد صحیح می باشد" ValidationExpression="\d*"></RegularExpression>
                                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                        </ErrorFrameStyle>
                                                                                    </ValidationSettings>
                                                                                </TSPControls:CustomTextBox>
                                                                            </td>
                                                                            <td width="5%">سریال
                                                                            </td>
                                                                            <td align="right" width="17%">
                                                                                <TSPControls:CustomTextBox runat="server" ID="txtDocSerialNo" OnTextChanged="txtDocSerialNo_TextChanged" AutoPostBack="true" Width="100%"
                                                                                    ClientInstanceName="txtDocSerialNo">
                                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                                                        <RegularExpression ErrorText="فیلد کد از جنس عدد صحیح می باشد" ValidationExpression="\d*"></RegularExpression>
                                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                        </ErrorFrameStyle>
                                                                                    </ValidationSettings>


                                                                                    <ClientSideEvents TextChanged="function(s, e) {
if(txtDocProvCode.GetText()=='' || txtDocProvCode.GetText()==null ||txtMjCode.GetText()=='' || txtMjCode.GetText()==null)
{
alert('کد استان و رشته را به صورت کامل وارد نمایید');
e.ProsseccOnServer=false;
}
else
e.ProsseccOnServer=true;
}"></ClientSideEvents>
                                                                                </TSPControls:CustomTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td valign="top" align="right" width="15%">
                                                                    <b><font color="red">یا</font></b>شماره پروانه:
                                                                </td>
                                                                <td valign="top" align="right" width="35%">
                                                                    <TSPControls:CustomTextBox runat="server" ID="txtSearchFileNo" Width="100%"
                                                                        ClientInstanceName="txtSearchFileNo" OnTextChanged="txtSearchFileNo_TextChanged" AutoPostBack="true"
                                                                        Style="direction: ltr">
                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                            </ErrorImage>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                            <RegularExpression ErrorText="شماره پروانه به صورت *****-***-**  می باشد" ValidationExpression="\d{2}-\d{3}-\d{1,7}" />
                                                                        </ValidationSettings>

                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxcp:PanelContent>
                                            </PanelCollection>
                                        </dxcp:ASPxPanel>
                                    </fieldset>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dxe:ASPxPanel>
                        <TSP:WorkRequestUserControl runat="server" ID="WorkRequestUserControl" />
                        <dxcp:ASPxPanel ID="RoundPanelMemberEngOfficeInfo" ClientInstanceName="RoundPanelMemberEngOfficeInfo"
                            runat="server">
                            <PanelCollection>
                                <dxcp:PanelContent>
                                    <TSP:MeEngOfficeInfoUserControl runat="server" ID="UserControlMeEngOfficeInfoUserControl" />
                                    <TSP:MeOfficeInfoUserControlUserControl runat="server" ID="UserControlMeOfficeInfoUserControl" />
                                </dxcp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxPanel>
                        <fieldset>
                            <legend class="HelpUL" id="ASPxRoundPanel2" runat="server">مشخصات طراح</legend>

                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="نوع طراح" ID="ASPxLabel9">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <TSPControls:CustomAspxComboBox Enabled="false" runat="server"
                                                TextField="Title" ID="cmbDesMeType" DataSourceID="ObjectDataSourceMemberType"
                                                ValueType="System.String" ValueField="MemberTypeId" ClientInstanceName="cmbMeType"
                                                Width="100%" RightToLeft="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="نوع طراح را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                            <asp:ObjectDataSource ID="ObjectDataSourceMemberType" runat="server" TypeName="TSP.DataManager.TechnicalServices.MemberTypeManager"
                                                SelectMethod="GetData" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                                        </td>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="نوع نقشه" ID="lblcmbPlanType">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <TSPControls:CustomAspxComboBox Enabled="false" runat="server" Width="100%"
                                                TextField="Title" ID="cmbPlanType" DataSourceID="ObjdsPlansType"
                                                ValueType="System.String" ValueField="PlansTypeId" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                    <RequiredField IsRequired="True" ErrorText="نوع نقشه را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                            <asp:ObjectDataSource ID="ObjdsPlansType" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.TechnicalServices.PlansTypeManager"></asp:ObjectDataSource>
                                        </td>
                                    </tr>

                                    <td>
                                        <dxe:ASPxLabel runat="server" Text="تعرفه خدمات مهندسی *" ID="lblPriceArchive" ClientInstanceName="lblPriceArchive">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxComboBox runat="server"
                                            TextField="YearName" ID="cmbPriceArchive" DataSourceID="ObjectDataSource_PriceArchive"
                                            ValueType="System.String" ValueField="PriceArchiveId" ClientInstanceName="cmbPriceArchive"
                                            Width="100%" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="تعرفه را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                        <asp:ObjectDataSource ID="ObjectDataSource_PriceArchive" runat="server" TypeName="TSP.DataManager.TechnicalServices.PriceArchiveManager"
                                            SelectMethod="SelectActivePriceArchive"></asp:ObjectDataSource>
                                    </td>
                                    <td>سال کاری</td>
                                    <td>
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            TextField="Year" ID="comboYear" DataSourceID="ObjectCapacityAssignment"
                                            ValueType="System.String" RightToLeft="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="سال کاری را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                        <asp:ObjectDataSource ID="ObjectCapacityAssignment" runat="server" TypeName="TSP.DataManager.TechnicalServices.CapacityAssignmentManager"
                                            SelectMethod="SelectTSCapacityAssignmentYears">
                                            <SelectParameters>
                                                <asp:Parameter DbType="Int16" DefaultValue="-1" Name="IsMainAgent" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                    </tr>
                                      <tr>
                                          <td valign="top" align="right" colspan="4">
                                              <TSPControls:CustomASPxCheckBox runat="server" Text="نماینده طراحان می باشد"
                                                  Wrap="False" ID="chbIsMaster">
                                              </TSPControls:CustomASPxCheckBox>
                                          </td>
                                      </tr>
                                    <tr>
                                        <td valign="top" align="right" colspan="4">
                                            <TSPControls:CustomASPxCheckBox runat="server" Text="ثبت کارکرد به دلیل اضافه اشکوب می باشد"
                                                Wrap="False" ID="ChbIsExteraFloor" Visible="false">
                                            </TSPControls:CustomASPxCheckBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
                        <TSP:Capacity ID="CapacityUserControl" runat="server" />
                        <fieldset>
                            <legend class="HelpUL">مشخصات نقشه</legend>                          

                            <TSPControls:CustomAspxDevGridView2 ID="GridViewPlans" runat="server" DataSourceID="ObjdsPlans"
                                Width="100%"
                                KeyFieldName="DesignerPlansId" AutoGenerateColumns="False"
                                ClientInstanceName="GridViewPlans">
                                <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                                <Columns>
                                    <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                                        VisibleIndex="0" Width="40px">
                                        <DataItemTemplate>
                                            <div align="center">
                                                <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>'>
                                                </dxe:ASPxImage>
                                            </div>
                                        </DataItemTemplate>
                                        <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                                            ValueType="System.String">
                                        </PropertiesComboBox>
                                    </dxwgv:GridViewDataComboBoxColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" Visible="False" FieldName="Status"
                                        Width="150px" Caption="Status">
                                        <HeaderStyle Wrap="True"></HeaderStyle>
                                    </dxwgv:GridViewDataTextColumn>
             
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="No" Width="150px" Caption="شماره نقشه">
                                        <HeaderStyle Wrap="True"></HeaderStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="PlanVersion" Width="50px"
                                        Caption="نسخه">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataComboBoxColumn FieldName="PlansTypeId" Caption="نوع نقشه" VisibleIndex="3"
                                        Width="150px">
                                        <HeaderStyle Wrap="True"></HeaderStyle>
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                        <PropertiesComboBox ValueType="System.String" TextField="Title" DataSourceID="ObjdsPlansType"
                                            ValueField="PlansTypeId">
                                        </PropertiesComboBox>
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataComboBoxColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="PlanDate" Caption="تاریخ ثبت">
                                        <HeaderStyle Wrap="True"></HeaderStyle>
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                  
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="12" Visible="False" FieldName="TaskName"
                                        Width="150px" Caption="وضعیت درخواست">
                                        <HeaderStyle Wrap="True"></HeaderStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewCommandColumn VisibleIndex="14" Caption=" " ShowClearFilterButton="true">
                                    </dxwgv:GridViewCommandColumn>
                                </Columns>
                                <Settings ShowHorizontalScrollBar="true"></Settings>
                            </TSPControls:CustomAspxDevGridView2>

                        </fieldset>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="vertical-align: top" dir="ltr">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s,e){
                                                                       CheckSaveConditions(e);
                                                                            }" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator7">
                                        </TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار پروژه"
                                            ID="btnSendNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/reload.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>

                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مالی طراحان"
                                            ID="btnDesAcc2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnDesAcc_Click" CausesValidation="false">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/TS/TSImpAcc.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5"></TSPControls:MenuSeprator>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" Width="25px" ID="btnBack2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
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
            <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="grid" SessionName="SendBackDataTable_EmpPrjAddDes"
                OnCallback="CallbackPanelWorkFlow_Callback" />
            <dxhf:ASPxHiddenField ID="HiddenFieldPrjDes" runat="server" ClientInstanceName="HiddenFieldPrjDes">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjdsPlans" runat="server" TypeName="TSP.DataManager.TechnicalServices.Designer_PlansManager"
                SelectMethod="SelectTSDesignerPlansByProjectDesigner" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-2" Name="PrjDesignerId" Type="Int32" />
                    <asp:Parameter DefaultValue="-2" Name="ProjectId" Type="Int32" />
                    <asp:Parameter DefaultValue="-2" Name="PrjReId" Type="Int32" />
                    <asp:Parameter DefaultValue="-2" Name="PlansTypeId" Type="Int32" /> 
                    <asp:Parameter DefaultValue="-2" Name="PlansId" Type="Int32" />                   
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" TypeName="TSP.DataManager.WorkFlowTaskManager"
                SelectMethod="SelectByWorkCode">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img src="../../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>

</asp:Content>
