<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="OwnerInsert.aspx.cs" Inherits="Employee_TechnicalServices_Project_OwnerInsert"
    Title="مشخصات مالک" %>

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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<%@ Register Src="../../../UserControl/WFUserControl.ascx" TagName="WFUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Content" runat="server" style="width: 100%; display: block;" align="center">
        <script language="javascript">
            function SetOtherPerson() {
                oFirstName.SetVisible(true);
                oLastName.SetVisible(true);
                oFatherName.SetVisible(true);
                oSSN.SetVisible(true);
                oIdNo.SetVisible(true);
                oBirthPlace.SetVisible(true);

                oOrgName.SetVisible(false);
                oManager.SetVisible(false);

                lbloFirstName.SetVisible(true);
                lbloLastName.SetVisible(true);
                lbloFatherName.SetVisible(true);
                lbloSSN.SetVisible(true);
                lbloIdNo.SetVisible(true);
                lbloBirthPlace.SetVisible(true);

                lbloOrgName.SetVisible(false);
                lbloManager.SetVisible(false);
            }
            function SetOrganization() {
                oFirstName.SetVisible(false);
                oLastName.SetVisible(false);
                oFatherName.SetVisible(false);
                oSSN.SetVisible(false);
                oIdNo.SetVisible(false);
                oBirthPlace.SetVisible(false);

                oOrgName.SetVisible(true);
                oManager.SetVisible(true);

                lbloFirstName.SetVisible(false);
                lbloLastName.SetVisible(false);
                lbloFatherName.SetVisible(false);
                lbloSSN.SetVisible(false);
                lbloIdNo.SetVisible(false);
                lbloBirthPlace.SetVisible(false);

                lbloOrgName.SetVisible(true);
                lbloManager.SetVisible(true);
            }
            function SetLawyerTrue() {
                lFirstName.SetVisible(true);
                lLastName.SetVisible(true);
                lFatherName.SetVisible(true);
                lSSN.SetVisible(true);
                lIdNo.SetVisible(true);
                lBirthPlace.SetVisible(true);
                lTel.SetVisible(true);
                lMobileNo.SetVisible(true);
                lAddress.SetVisible(true);

                lblFirstName.SetVisible(true);
                lblLastName.SetVisible(true);
                lblFatherName.SetVisible(true);
                lblSSN.SetVisible(true);
                lblIdNo.SetVisible(true);
                lblBirthPlace.SetVisible(true);
                lblTel.SetVisible(true);
                lblMobileNo.SetVisible(true);
                lblAddress.SetVisible(true);
            }
            function SetLawyerFalse() {
                lFirstName.SetVisible(false);
                lLastName.SetVisible(false);
                lFatherName.SetVisible(false);
                lSSN.SetVisible(false);
                lIdNo.SetVisible(false);
                lBirthPlace.SetVisible(false);
                lTel.SetVisible(false);
                lMobileNo.SetVisible(false);
                lAddress.SetVisible(false);

                lblFirstName.SetVisible(false);
                lblLastName.SetVisible(false);
                lblFatherName.SetVisible(false);
                lblSSN.SetVisible(false);
                lblIdNo.SetVisible(false);
                lblBirthPlace.SetVisible(false);
                lblTel.SetVisible(false);
                lblMobileNo.SetVisible(false);
                lblAddress.SetVisible(false);
            }

        </script>
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayHeight="15" CalendarDayWidth="33" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
             
                                  <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                            <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                width="100%">
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: top" align="right">
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
                                                                ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                OnClick="btnSave_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/save.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>   <td width="10px" align="center">
                                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator6">
                                                            </TSPControls:MenuSeprator>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ناظر جدید"
                                                                ID="btnObservers" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                OnClick="btnObservers_Click" CausesValidation="false">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/TS/Observers.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="طراح جدید"
                                                                ID="btnDesigners" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                OnClick="btnDesigners_Click" CausesValidation="false">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/TS/Designers.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                     
                                                        <td width="10px" align="center">
                                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                                            </TSPControls:MenuSeprator>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گردش کار پروژه"
                                                                ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" CausesValidation="false">
                                                                <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/reload.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td width="10px" align="center">
                                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4">
                                                            </TSPControls:MenuSeprator>
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
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                       </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                <TSP:ProjectInfo ID="prjInfo" runat="server"></TSP:ProjectInfo>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelInfo" HeaderText="مشاهده" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table dir="rtl" width="100%">
                                <tbody>
                                      <tr>
                                                                                    <td align="right" valign="top" colspan="4">
                                                                                        <ul class="HelpUL">

                                                                                            <li>ثبت و ویرایش مشخصات پایه ی نماینده مالکین تنها از صفحه مشخصات پروژه امکان پذیر است. </li>
                                                                                     

                                                                                        </ul>
                                                                                    </td>
                                                                                </tr>
                                    <tr>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="نوع مالک" ID="ASPxLabel1" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomAspxComboBox runat="server"  Width="100%" 
                                                ID="CmbType" ValueType="System.String" SelectedIndex="0" ClientInstanceName="cmb"
                                                 RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(cmb.GetValue() == '1')
		SetOtherPerson();
	else
		SetOrganization();
}"></ClientSideEvents>
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="مالک را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <Items>
                                                    <dxe:ListEditItem Value="1" Text="دیگر اشخاص" Selected="True"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Value="2" Text="سازمان"></dxe:ListEditItem>
                                                </Items>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 35%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" colspan="4">
                                            <TSPControls:CustomASPxCheckBox runat="server" Text="نماینده مالکین می باشد" Checked="False" ID="ChbAgent">
                                                <ClientSideEvents CheckedChanged="function(s, e) {
if(chb.GetChecked()==true)
{
	SetLawyerTrue();
}
else
{
 SetLawyerFalse();
}
}"></ClientSideEvents>
                                            </TSPControls:CustomASPxCheckBox>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxlbloFirstName" ClientInstanceName="lbloFirstName"
                                                Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtoFirstName"  Width="100%"
                                                ClientInstanceName="oFirstName" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام خانوادگی" Width="100%" ID="ASPxlbloLastName"
                                                ClientInstanceName="lbloLastName">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtoLastName"  Width="100%"
                                                ClientInstanceName="oLastName" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نام خانوادگی را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام سازمان" ClientVisible="False" Width="100%"
                                                ID="ASPxlbloOrgName" ClientInstanceName="lbloOrgName">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtoOrgName"  ClientVisible="False"
                                                Width="100%" ClientInstanceName="oOrgName" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نام سازمان را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="مدیر مسئول" ClientVisible="False" ID="ASPxlbloManager"
                                                ClientInstanceName="lbloManager" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtoManager"  ClientVisible="False"
                                                Width="100%" ClientInstanceName="oManager" >
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
                                            <dxe:ASPxLabel runat="server" Text="نام پدر" ID="ASPxlbloFatherName" ClientInstanceName="lbloFatherName"
                                                Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtoFatherName"  Width="100%"
                                                ClientInstanceName="oFatherName" >
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
                                            <dxe:ASPxLabel runat="server" Text="کد ملی" ID="ASPxlbloSSN" ClientInstanceName="lbloSSN"
                                                Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtoSSN"  Width="100%" MaxLength="10"
                                                ClientInstanceName="oSSN" >
                                                <ValidationSettings Display="Dynamic" ErrorText="ساختار کد ملی وارد شده صحیح نمی باشد" EnableCustomValidation="True" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="true" ErrorText="ورود کد ملی مالک الزامی می باشد"></RequiredField>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                 <ClientSideEvents Validation="onIranianNationalCodeValidation" />
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره شناسنامه" Width="100%" ID="ASPxlbloIdNo"
                                                ClientInstanceName="lbloIdNo">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtoIdNo"  Width="100%" MaxLength="10"
                                                ClientInstanceName="oIdNo" >
                                                <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <%--  <RequiredField ErrorText="" IsRequired="false"></RequiredField>--%>
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,10}">
                                                    </RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="محل صدور" ID="ASPxlbloBirthPlace" ClientInstanceName="lbloBirthPlace"
                                                Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtoBirthPlace"  Width="100%"
                                                ClientInstanceName="oBirthPlace" >
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
                                            <dxe:ASPxLabel runat="server" Text="تلفن" ID="ASPxLabel6" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtoTel"  Width="100%" ClientInstanceName="oTel"
                                                >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <%--  <RequiredField  IsRequired="false" ErrorText="تلفن را وارد نمایید"></RequiredField>--%>
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d*"></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره همراه" ID="ASPxLabel7" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtoMobileNo"  Width="100%"
                                                MaxLength="11" ClientInstanceName="oMobileNo" >
                                                <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <%-- <RequiredField  IsRequired="false" ErrorText="شماره همراه را وارد نمایید"></RequiredField>--%>
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{10}">
                                                    </RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="آدرس" ID="ASPxLabel8" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtoAddress"  Width="100%"
                                                ClientInstanceName="oAddress" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <%-- <RequiredField IsRequired="True" ErrorText="آدرس را وارد نمایید"></RequiredField>--%>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" colspan="4">
                                            <br />
                                            <TSPControls:CustomASPxCheckBox runat="server" Text="وکیل قانونی دارد" ID="ChbHaveLawyer" ClientInstanceName="chb">
                                                <ClientSideEvents CheckedChanged="function(s, e) {
if(chb.GetChecked()==true)
{
	SetLawyerTrue();
}
else
{
 SetLawyerFalse();
}
}"></ClientSideEvents>
                                            </TSPControls:CustomASPxCheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام" ClientVisible="False" ID="ASPxlblFirstName"
                                                ClientInstanceName="lblFirstName" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtlFirstName"  ClientVisible="False"
                                                Width="100%" ClientInstanceName="lFirstName" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ClientVisible="False" ID="ASPxlblLastName"
                                                ClientInstanceName="lblLastName" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtlLastName"  ClientVisible="False"
                                                Width="100%" ClientInstanceName="lLastName" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نام خانوادگی را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام پدر" ClientVisible="False" ID="ASPxlblFatherName"
                                                ClientInstanceName="lblFatherName" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtlFatherName"  ClientVisible="False"
                                                Width="100%" ClientInstanceName="lFatherName" >
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
                                            <dxe:ASPxLabel runat="server" Text="کد ملی" ClientVisible="False" ID="ASPxlblSSN"
                                                ClientInstanceName="lblSSN" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtlSSN"  ClientVisible="False"
                                                Width="100%" MaxLength="10" ClientInstanceName="lSSN" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="\d{10}"></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره شناسنامه" ClientVisible="False" ID="ASPxlblIdNo"
                                                ClientInstanceName="lblIdNo" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtlIdNo"  ClientVisible="False"
                                                Width="100%" MaxLength="10" ClientInstanceName="lIdNo" >
                                                <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <%--   <RequiredField ErrorText=""></RequiredField>--%>
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,10}">
                                                    </RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="محل صدور" ClientVisible="False" ID="ASPxlblBirthPlace"
                                                ClientInstanceName="lblBirthPlace" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtlBirthPlace"  ClientVisible="False"
                                                Width="100%" ClientInstanceName="lBirthPlace" >
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
                                            <dxe:ASPxLabel runat="server" Text="تلفن" ClientVisible="False" ID="ASPxlblTel" ClientInstanceName="lblTel"
                                                Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtlTel"  ClientVisible="False"
                                                Width="100%" ClientInstanceName="lTel" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <%-- <RequiredField IsRequired="True" ErrorText="تلفن را وارد نمایید"></RequiredField>--%>
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d*"></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره همراه" ClientVisible="False" ID="ASPxlblMobileNo"
                                                ClientInstanceName="lblMobileNo" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtlMobileNo"  ClientVisible="False"
                                                Width="100%" MaxLength="11" ClientInstanceName="lMobileNo" >
                                                <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{10}">
                                                    </RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="آدرس" ClientVisible="False" ID="ASPxlblAddess"
                                                ClientInstanceName="lblAddress" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="26px" ID="txtlAddess"  ClientVisible="False"
                                                Width="100%" ClientInstanceName="lAddress" >
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                          <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                
                            <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                width="100%">
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: top" align="right">
                                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                cellpadding="0">
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
                                                                ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                OnClick="btnSave_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/save.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td width="10px" align="center">
                                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2">
                                                            </TSPControls:MenuSeprator>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ناظر جدید"
                                                                ID="btnObservers2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                OnClick="btnObservers_Click" CausesValidation="false">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/TS/Observers.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="طراح جدید"
                                                                ID="btnDesigners2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                OnClick="btnDesigners_Click" CausesValidation="false">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/TS/Designers.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td width="10px" align="center">
                                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5">
                                                            </TSPControls:MenuSeprator>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="گردش کار پروژه"
                                                                ID="btnSendNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" CausesValidation="false">
                                                                <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/reload.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td width="10px" align="center">
                                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3">
                                                            </TSPControls:MenuSeprator>
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
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                       </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:HiddenField ID="HDProjectId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="HDOwnerId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="RequestId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="HFOtherPersOrgId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="HFLawyerId" runat="server" Visible="False"></asp:HiddenField>
                
                <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="grid" SessionName="SendBackDataTable_EmpPrjOwnerIns"
                    OnCallback="CallbackPanelWorkFlow_Callback" />
              
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
            DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
    </div>
</asp:Content>
