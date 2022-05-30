<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="ImplementerInsert.aspx.cs" Inherits="Employee_TechnicalServices_Project_ImplementerInsert"
    Title="مشخصات مجری" %>

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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<%@ Register Src="~/UserControl/CapacityUserControl.ascx" TagPrefix="TSP" TagName="Capacity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <div id="content" style="width: 100%" align="center">
        <script language="javascript">
            function SetMemberKardan() {
                FirstName.SetVisible(true);
                LastName.SetVisible(true);
                FatherName.SetVisible(true);
                SSN.SetVisible(true);
                lblFirstName.SetVisible(true);
                lblLastName.SetVisible(true);
                lblFatherName.SetVisible(true);
                lblSSN.SetVisible(true);

                OrgName.SetVisible(false);
                Manager.SetVisible(false);
                lblOrgName.SetVisible(false);
                lblManager.SetVisible(false);

            }
            function SetOffice() {
                FirstName.SetVisible(false);
                LastName.SetVisible(false);
                FatherName.SetVisible(false);
                SSN.SetVisible(false);
                lblFirstName.SetVisible(false);
                lblLastName.SetVisible(false);
                lblFatherName.SetVisible(false);
                lblSSN.SetVisible(false);

                OrgName.SetVisible(true);
                Manager.SetVisible(true);
                lblOrgName.SetVisible(true);
                lblManager.SetVisible(true);
            }
            function SetClear() {
                FirstName.SetText("");
                LastName.SetText("");
                FatherName.SetText("");
                SSN.SetText("");
                OrgName.SetText("");
                Manager.SetText("");
                ID.SetText("");
                FileNo.SetText("");
                FileDate.SetText("");
                txtArchitectorCode.SetText("");
                chbm.SetChecked(false);
                chbo.SetChecked(false);
                HD.Set("name", 0);
                hp.SetVisible(false);
                hp.SetNavigateUrl("");
                img.SetVisible(false);
                ImpId.SetText("");
            }
            function SetFiche() {
                lblBank.SetVisible(false);
                lblBranchCode.SetVisible(false);
                lblBranchName.SetVisible(false);
                Bank.SetVisible(false);
                BranchCode.SetVisible(false);
                BranchName.SetVisible(false);
            }
            function SetCheque() {
                lblBank.SetVisible(true);
                lblBranchCode.SetVisible(true);
                lblBranchName.SetVisible(true);
                Bank.SetVisible(true);
                BranchCode.SetVisible(true);
                BranchName.SetVisible(true);
            }
        </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table >
                                <tbody>
                                    <tr>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="جدید"
                                                CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="BtnNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="ویرایش"
                                                CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="ذخیره"
                                                ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnSave_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if(HD.Get('name')!=1)
{
lbl.SetVisible(true);
e.processOnServer=false;
}
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/save.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator"></TSPControls:MenuSeprator>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxButton ID="btnShowPpcAttachPageToAutomationLetter" runat="server" AutoPostBack="False"
                                                CausesValidation="False"  EnableTheming="False"
                                                EnableViewState="False" Text=" " ToolTip="اضافه کردن صفحه به سند اتوماسیون اداری"
                                                UseSubmitBehavior="False">
                                                <ClientSideEvents Click="function(s,e){
                                                            ppcAttachPageToAutomationLetter.Show();
                                                            PanelAttachPageToAutomationLetterFinish.SetVisible(false);
                                                            PanelAttachPageToAutomationLetterInputData.SetVisible(true);                                                          
                                                            lblErrorInputAttachPageToAutomationLetter.SetVisible(false);
                                                            txtLetterNumber_AttachPageToAutomationLetter.SetText('');
                                                            txtLinkName_AttachPageToAutomationLetter.SetText('');
                                                            txtTimeOut_AttachPageToAutomationLetter.SetText('7');
                                                            txtLetterNumber_AttachPageToAutomationLetter.Focus();
                                                            }" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/AttachPage.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                        </td>
                                        <td align="right" valign="top">
                                            <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="بازگشت"
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
                <div align="right">
                    <TSPControls:CustomAspxMenuHorizontal ID="MenuImp" runat="server"  
                        OnItemClick="MenuImp_ItemClick"  AutoSeparators="RootOnly"
                        ItemSpacing="0px" SeparatorColor="#A5A6A8" SeparatorHeight="100%" SeparatorWidth="1px">
                        <Items>
                            <dxm:MenuItem Text="مشخصات مجری" Name="Imp" Selected="True">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="سوابق کاری" Name="Job">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="کیفیت اجرای پروژه ها" Name="Control">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="احکام شورای انتظامی" Name="Entezami">
                            </dxm:MenuItem>
                        </Items>
                        <RootItemSubMenuOffset X="-1" LastItemY="-2" LastItemX="-1" FirstItemY="-2" FirstItemX="-1"
                            Y="-2"></RootItemSubMenuOffset>
                        <Border BorderWidth="1px" BorderStyle="Solid" BorderColor="#A5A6A8"></Border>
                        <VerticalPopOutImage Height="8px" Width="4px">
                        </VerticalPopOutImage>
                        <ItemStyle VerticalAlign="Middle" ImageSpacing="5px" PopOutImageSpacing="7px"></ItemStyle>
                        <SubMenuItemStyle ImageSpacing="7px">
                        </SubMenuItemStyle>
                        <SubMenuStyle BackColor="#EDF3F4" GutterWidth="0px" SeparatorColor="#7EACB1"></SubMenuStyle>
                        <HorizontalPopOutImage Height="7px" Width="7px">
                        </HorizontalPopOutImage>
                    </TSPControls:CustomAspxMenuHorizontal>
                </div>
                <br />
                <TSP:ProjectInfo ID="prjInfo" runat="server" />
                <br />
                <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="نوع مجری" ID="ASPxLabel1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <TSPControls:CustomAspxComboBox runat="server"  Width="100%" 
                                                ID="CmbType" ValueType="System.String" SelectedIndex="0" ClientInstanceName="cmb"
                                                 RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
    SetClear();
	if(cmb.GetValue()=='2')
		SetOffice();
	else
		SetMemberKardan();


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
                                                    <dxe:ListEditItem Value="1" Text="شخص حقیقی" Selected="True"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Value="2" Text="شخص حقوقی"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Value="3" Text="کاردان"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Value="4" Text="معمار تجربی"></dxe:ListEditItem>
                                                </Items>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right" width="15%">
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="ASPxLabel10">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtID"  Width="100%" AutoPostBack="True"
                                                ClientInstanceName="ID"  OnTextChanged="txtID_TextChanged">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="\d*"></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="صلاحیت اجرا" ID="ASPxLabelImpId" ClientInstanceName="lblImpId">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtImpId"  Width="100%" ReadOnly="True"
                                                ClientInstanceName="ImpId" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabelFirstName" ClientInstanceName="lblFirstName">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtFirstName"  Width="100%"
                                                ReadOnly="True" ClientInstanceName="FirstName" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="ASPxLabelLastName" ClientInstanceName="lblLastName">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtLastName"  Width="100%"
                                                ReadOnly="True" ClientInstanceName="LastName" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام پدر" ID="ASPxLabelFatherName" ClientInstanceName="lblFatherName">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtFatherName"  Width="100%"
                                                ReadOnly="True" ClientInstanceName="FatherName" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد ملی" ID="ASPxLabelSSN" ClientInstanceName="lblSSN">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtSSN"  Width="100%" MaxLength="10"
                                                ReadOnly="True" ClientInstanceName="SSN" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره پروانه" ID="ASPxLabel13">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtFileNo"  Width="100%" ReadOnly="True"
                                                ClientInstanceName="FileNo"  Style="direction: ltr">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ اعتبار پروانه" ID="ASPxLabel15">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtFileDate"  Width="100%"
                                                ReadOnly="True" ClientInstanceName="FileDate" 
                                                Style="direction: ltr">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام سازمان" ClientVisible="False" ID="ASPxLabelOrgName"
                                                ClientInstanceName="lblOrgName">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtOrgName"  ClientVisible="False"
                                                Width="100%" ReadOnly="True" ClientInstanceName="OrgName" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="مدیر مسئول" ClientVisible="False" ID="ASPxLabelManager"
                                                ClientInstanceName="lblManager">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtManager"  ClientVisible="False"
                                                Width="100%" ReadOnly="True" ClientInstanceName="Manager" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تعهدها" ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxUploadControl runat="server" ShowProgressPanel="True" MaxSizeForUploadFile="0"
                                                                UploadWhenFileChoosed="True" ID="flpCommit" InputType="Files" ClientInstanceName="flp"
                                                                OnFileUploadComplete="flpCommit_FileUploadComplete">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
 if(e.isValid){
	img.SetVisible(true);
	HD.Set('name',1);
	lbl.SetVisible(false);
	hp.SetVisible(true);
	hp.SetNavigateUrl('../../../Image/Temp/'+e.callbackData);
    }
else {
	img.SetVisible(false);
	HD.Set('name',0);
	lbl.SetVisible(true);
	hp.SetVisible(false);
	hp.SetNavigateUrl('');
}
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxUploadControl>
                                                            <dxe:ASPxLabel runat="server" Text="تصویر تعهد را انتخاب نمایید" ClientVisible="False"
                                                                ID="ASPxLabel3" ForeColor="Red" ClientInstanceName="lbl">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                ID="imgEndUploadImg" ClientInstanceName="img">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <dxe:ASPxHyperLink runat="server" Text="آدرس فایل" ClientVisible="False" Target="_blank"
                                                ID="HpCommit" ClientInstanceName="hp">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد نظارت شهرسازی" ID="lblArchitectorCode" ClientInstanceName="lblArchitectorCode">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtArchitectorCode"  Width="100%"
                                                ClientInstanceName="txtArchitectorCode" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomASPxCheckBox runat="server" Text="مجری مالک پروژه نیز می باشد" ID="ChbOwner"
                                                ClientInstanceName="chbo">
                                            </TSPControls:CustomASPxCheckBox>
                                        </td>
                                        <td valign="top" align="right">
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomASPxCheckBox runat="server" Text="مجری مادر" ID="ChbMother" ClientInstanceName="chbm">
                                            </TSPControls:CustomASPxCheckBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSP:Capacity ID="CapacityUserControl" runat="server" />
                <%--<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanelCapacity" HeaderText="ظرفیت"
                    runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="متراژ کسر ظرفیت (%)" ID="ASPxLabel23">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtcDecrementPercent"  Width="100%"
                                                ReadOnly="True" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="متراژ دستمزد (%)" ID="ASPxLabel24">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtcWagePercent"  Width="100%"
                                                ReadOnly="True" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="متراژ کل پروژه" ID="ASPxLabel18">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtcFoundation"  Width="100%"
                                                ReadOnly="True" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="ظرفیت کل" ID="ASPxLabeTotalCapacity">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtcTotalCapacity"  Width="100%"
                                                ReadOnly="True" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="ظرفیت باقیمانده" ID="ASPxLabeRemainCapacity">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtcRemainCapacity"  Width="100%"
                                                ReadOnly="True" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کل کارکرد" ID="ASPxLabeTotalFunction">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtcTotalFunction"  Width="100%"
                                                ReadOnly="True" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تعداد پروژه" ID="ASPxLabeProjectCount">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtcProjectCount"  Width="100%"
                                                ReadOnly="True" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کل رزرو شده" ID="ASPxLabeReserve">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtcReserve"  Width="100%"
                                                ReadOnly="True" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="متراژ کسر ظرفیت مجری" ID="ASPxLabel17">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtcCapacityDecrement"  Width="100%"
                                                >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="متراژ کسر ظرفیت را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="متراژ دستمزد مجری" ID="ASPxLabel19">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtcWage"  Width="100%" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="متراژ دستمزد را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>--%>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0">
                                <tr>
                                    <td >
                                        <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="جدید"
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
                                        <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="ویرایش"
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
                                        <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="ذخیره"
                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if(HD.Get('name')!=1)
{
lbl.SetVisible(true);
e.processOnServer=false;
}
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="اضافه کردن صفحه به سند اتوماسیون اداری"
                                            CausesValidation="False" ID="btnShowPpcAttachPageToAutomationLetter2" AutoPostBack="False"
                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s,e){
                                                            ppcAttachPageToAutomationLetter.Show();
                                                            PanelAttachPageToAutomationLetterFinish.SetVisible(false);
                                                            PanelAttachPageToAutomationLetterInputData.SetVisible(true);                                                          
                                                            lblErrorInputAttachPageToAutomationLetter.SetVisible(false);
                                                            txtLetterNumber_AttachPageToAutomationLetter.SetText('');
                                                            txtLinkName_AttachPageToAutomationLetter.SetText('');
                                                            txtTimeOut_AttachPageToAutomationLetter.SetText('7');
                                                            txtLetterNumber_AttachPageToAutomationLetter.Focus();
                                                            }"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/AttachPage.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton runat="server" Text=" "  ToolTip="بازگشت"
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
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:HiddenField ID="HDProjectId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="HDImpId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="RequestId" runat="server" Visible="False"></asp:HiddenField>
                <dxhf:ASPxHiddenField ID="HDFlpCommit" runat="server" ClientInstanceName="HD">
                </dxhf:ASPxHiddenField>
                <asp:HiddenField ID="HDCitId" runat="server" Visible="False" __designer:wfdid="w6">
                </asp:HiddenField>
                <TSPControls:CustomASPxPopupControl ID="ppcAttachPageToAutomationLetter" runat="server" Width="404px"
                      
                    HeaderText="اضافه کردن صفحه به سند اتوماسیون اداری" PopupVerticalAlign="WindowCenter"
                    PopupHorizontalAlign="WindowCenter" Modal="True" Height="77px" EnableViewState="False"
                    EnableClientSideAPI="True" EnableAnimation="False" CloseAction="CloseButton"
                    ClientInstanceName="ppcAttachPageToAutomationLetter" AutoUpdatePosition="True"
                    AllowDragging="True">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <TSPControls:CustomAspxCallbackPanel runat="server"  ID="CallbackAttachPageToAutomationLetter"
                                ClientInstanceName="CallbackAttachPageToAutomationLetter" OnCallback="CallbackAttachPageToAutomationLetter_Callback">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server">
                                        <dxp:ASPxPanel runat="server" ID="PanelAttachPageToAutomationLetterInputData" ClientInstanceName="PanelAttachPageToAutomationLetterInputData">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <div align="center">
                                                        <table width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        <dxe:ASPxLabel runat="server" ClientVisible="False" ID="lblErrorInputAttachPageToAutomationLetter"
                                                                            ForeColor="Red" ClientInstanceName="lblErrorInputAttachPageToAutomationLetter">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px" align="right">
                                                                        شماره سند
                                                                    </td>
                                                                    <td style="width: 200px" align="right">
                                                                        <TSPControls:CustomTextBox runat="server" ID="txtLetterNumber_AttachPageToAutomationLetter"
                                                                             Width="170px" ClientInstanceName="txtLetterNumber_AttachPageToAutomationLetter"
                                                                            >
                                                                            <ValidationSettings Display="Dynamic" ValidationGroup="AttachPageToAutomationLetter"
                                                                                ErrorTextPosition="Bottom">
                                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                                </ErrorImage>
                                                                                <RequiredField IsRequired="True" ErrorText="شماره سند وارد نشده است"></RequiredField>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        نام لینک
                                                                    </td>
                                                                    <td align="right">
                                                                        <TSPControls:CustomTextBox runat="server" ID="txtLinkName_AttachPageToAutomationLetter" 
                                                                            Width="170px" ClientInstanceName="txtLinkName_AttachPageToAutomationLetter" >
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
                                                                    <td align="right">
                                                                        مدت اعتبار (روز)
                                                                    </td>
                                                                    <td align="right">
                                                                        <dxe:ASPxSpinEdit runat="server" MaxValue="3650" Height="21px" ID="txtTimeOut_AttachPageToAutomationLetter"
                                                                             Width="170px" AllowNull="False" 
                                                                            Number="7" NumberType="Integer" MinValue="1" ClientInstanceName="txtTimeOut_AttachPageToAutomationLetter"
                                                                            >
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        <br />
                                                                        <table>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <TSPControls:CustomAspxButton runat="server" Text="&#160;ذخیره"  CausesValidation="False"
                                                                                            Width="80px" ID="btnSaveAttachPageToAutomationLetter" AutoPostBack="False" UseSubmitBehavior="False"
                                                                                            >
                                                                                            <ClientSideEvents Click="function(s, e){ 
                                                if(ASPxClientEdit.ValidateGroup('AttachPageToAutomationLetter')==true)
                                                CallbackAttachPageToAutomationLetter.PerformCallback('');
                                                }"></ClientSideEvents>
                                                                                            <Image Height="20px" Width="20px" Url="~/Images/Icons/Save.png">
                                                                                            </Image>
                                                                                        </TSPControls:CustomAspxButton>
                                                                                    </td>
                                                                                    <td style="width: 15px">
                                                                                    </td>
                                                                                    <td>
                                                                                        <TSPControls:CustomAspxButton runat="server" Text="&#160;انصراف"  CausesValidation="False"
                                                                                            Width="80px" ID="btnClosePpcAttachPageToAutomationLetter" AutoPostBack="False"
                                                                                            UseSubmitBehavior="False" >
                                                                                            <ClientSideEvents Click="function(s, e) { ppcAttachPageToAutomationLetter.Hide(); }">
                                                                                            </ClientSideEvents>
                                                                                            <Image Height="20px" Width="20px" Url="~/Images/Stop.png">
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
                                                    </div>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                        <dxp:ASPxPanel runat="server" ClientVisible="False" ID="PanelAttachPageToAutomationLetterFinish"
                                            ClientInstanceName="PanelAttachPageToAutomationLetterFinish">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server">
                                                    <div align="center">
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <dxe:ASPxLabel runat="server" ID="lblMessageAttachPageToAutomationLetter" ForeColor="Green"
                                                            ClientInstanceName="lblMessageAttachPageToAutomationLetter">
                                                        </dxe:ASPxLabel>
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <TSPControls:CustomAspxButton runat="server" Text="&#160;خروج"  CausesValidation="False"
                                                            Width="80px" ID="btnClosePpcAttachPageToAutomationLetter2" AutoPostBack="False"
                                                            UseSubmitBehavior="False" >
                                                            <ClientSideEvents Click="function(s, e) { ppcAttachPageToAutomationLetter.Hide(); }">
                                                            </ClientSideEvents>
                                                            <Image Height="20px" Width="20px" Url="~/Images/Stop.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </div>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </TSPControls:CustomAspxCallbackPanel>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                    <HeaderStyle HorizontalAlign="Right">
                        <Paddings PaddingTop="1px" PaddingRight="6px" PaddingLeft="10px"></Paddings>
                    </HeaderStyle>
                    <SizeGripImage Height="12px" Width="12px">
                    </SizeGripImage>
                    <CloseButtonImage Height="17px" Width="17px">
                    </CloseButtonImage>
                </TSPControls:CustomASPxPopupControl>
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                    BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                            <img src="../../../Image/indicator.gif" align="middle" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
