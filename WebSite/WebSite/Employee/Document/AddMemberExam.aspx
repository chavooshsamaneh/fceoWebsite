<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddMemberExam.aspx.cs" Inherits="Employee_Document_AddMemberExam"
    Title="مشخصات آزمون" %>

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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Src="../../UserControl/MemberInfoUserControl.ascx" TagName="MemberInfoUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetControlValues() {

            grid.GetRowValues(grid.GetFocusedRowIndex(), 'TCondId;Title;MjName;ExpireDate', SetValue);
            PanelPeriodImg.SetVisible(false);
            lblPeriod.SetVisible(false);
        }
        function SetValue(values) {
            HiddenFieldExam.Set('TCondId', values[0]);
            txtExamDate1.SetText(values[1]);
            cmbTstType.ClearItems();
            cmbTstType.PerformCallback(values[0]);
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#"><span style="color: #000000">ب</span>بستن</a>]
            </div>

            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                            <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">

                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click" CausesValidation="true">

                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s,e){
                                                         if (ASPxClientEdit.ValidateGroup('ValidationExam') == false)
                                                         {
                                                                e.processOnServer=false;
                                                                return;
                                                          }                                                         
                                                        else
	                                                            e.processOnServer=true;
                                                        }" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
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

            <br />
            <uc2:MemberInfoUserControl ID="MemberInfoUserControl1" runat="server" />
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelExam" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <dxe:ASPxPanel ID="RoundPanelMeExam" runat="server">
                            <PanelCollection>
                                <dxe:PanelContent>
                                    <fieldset>
                                        <legend class="HelpUL">آزمون</legend>
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="right" rowspan="1" width="15%">
                                                        <dxe:ASPxLabel runat="server" Width="100%" Text="رشته*" ID="ASPxLabel8">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" dir="ltr" align="right" width="35%">
                                                        <TSPControls:CustomAspxComboBox RightToLeft="True" runat="server" Width="100%" ValueType="System.String"
                                                            DataSourceID="ObjdsMemberLicence" TextField="MjName" ValueField="MjId" AutoPostBack="True"
                                                            ID="cmbMajor" ClientInstanceName="cmbMajor" OnSelectedIndexChanged="cmbMajor_SelectedIndexChanged">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="ValidationExam">

                                                                <RequiredField ErrorText="رشته را انتخاب نمایید" IsRequired="true" />
                                                            </ValidationSettings>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                    <td valign="top" align="right" rowspan="1" width="15%">
                                                        <dxe:ASPxLabel runat="server" Width="100%" Text="آزمون*" ID="ASPxLabel9">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ObjdsTestCondition" AutoPostBack="true" OnSelectedIndexChanged="ComboTestCondition_SelectedIndexChanged"
                                                            TextField="Title" ValueField="TCondId" Width="100%"
                                                            ID="ComboTestCondition" ClientInstanceName="ComboTestCondition">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="ValidationExam">
                                                                <RequiredField ErrorText="آزمون را انتخاب نمایید" IsRequired="true" />
                                                            </ValidationSettings>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <asp:Label runat="server" Text="تصویر تاییدیه آزمون" ID="Label1"></asp:Label>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxUploadControl ID="flpAttachConf" ClientVisible="false"
                                                                            runat="server" ClientInstanceName="flpAttachConf" UploadWhenFileChoosed="true" OnFileUploadComplete="flpAttachImgConf_FileUploadComplete">
                                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClientflpAttachConf.SetVisible(true);
  	HiddenFieldExam.Set('imgConf',1);
	lblImageWarningConf.SetVisible(false);
	HyperLinkExamConfirmImageURL.SetVisible(true);
	HyperLinkExamConfirmImageURL.SetImageUrl('../../Image/DocMeFile/ExamsConfirm/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientflpAttachConf.SetVisible(false);
	lblImageWarningConf.SetVisible(true);
	HyperLinkExamConfirmImageURL.SetVisible(false);
	HyperLinkExamConfirmImageURL.SetImageUrl('');    
  	HiddenFieldExam.Set('imgConf',0);
	}
}" />
                                                                        </TSPControls:CustomAspxUploadControl>
                                                                        <dxe:ASPxLabel ID="lblImageWarningConf" runat="server" ClientInstanceName="lblImageWarningConf"
                                                                            ClientVisible="False" ForeColor="Red" Text="تصویر تاییدیه آزمون راانتخاب نمایید">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxImage ID="imgEndUploadImgClientflpAttachConf" runat="server" ClientInstanceName="imgEndUploadImgClientflpAttachConf"
                                                                            ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویرانتخاب شد">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <dxe:ASPxImage ID="HyperLinkExamConfirmImageURL" runat="server" ClientInstanceName="HyperLinkExamConfirmImageURL"
                                                            Width="75px" Height="75px" Target="_blank" Text="تصویر تاییدیه آزمون">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </fieldset>
                                </dxe:PanelContent>
                            </PanelCollection>
                        </dxe:ASPxPanel>
                        <fieldset id="RoundPanelPrj" runat="server">
                            <legend class="HelpUL">زمینه آزمون</legend>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" dir="rtl" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Width="100%" Text="نمره آزمون*" ID="ASPxLabel4">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <TSPControls:CustomTextBox runat="server" Width="100%"
                                                ID="txtPoint">

                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="ValidationExam">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RegularExpression ErrorText="نمره قبولی را با فرمت صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                                    <RequiredField IsRequired="True" ErrorText="نمره گرفته شده در آزمون را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" dir="rtl" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Width="100%" Text="زمینه آزمون*" ID="ASPxLabel7">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" TextField="TTypeName"
                                                ValueField="TCondDId" Width="100%" ClientInstanceName="cmbTstType"
                                                ID="cmbTestType" OnSelectedIndexChanged="cmbTestType_SelectedIndexChanged" AutoPostBack="true" DataSourceID="ObjectDataSourceTestType">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="ValidationExam">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="True" ErrorText="زمینه آزمون را انتخاب نمایید"></RequiredField>
                                                </ValidationSettings>
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="ValidationExam">
                                                    <RequiredField ErrorText="زمینه آزمون را انتخاب نمایید" IsRequired="true" />
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Width="100%" Text="پایه" ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" DataSourceID="ObjdsGrade"
                                                TextField="GrdName" ValueField="GrdId" Width="100%"
                                                ID="cmbGrade">
                                                <ValidationSettings>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right"></td>
                                        <td valign="top" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد کاربری" Width="100%" ID="ASPxLabel6">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="100%"
                                                ID="txtUserCode">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="ValidationExam">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="false" ErrorText="کد کاربری خود در آزمون را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Width="100%" Text="شماره داوطلبی" ID="ASPxLabel3">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="100%"
                                                ID="txtEntrantCode">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="ValidationExam">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="false" ErrorText="شماره داوطلبی خود در آزمون را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="تصویر کارنامه قبولی*" ID="Label50"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxUploadControl ID="flpAttach" ClientVisible="false" runat="server"
                                                                ClientInstanceName="flpi" UploadWhenFileChoosed="true" OnFileUploadComplete="flpAttachImgExam_FileUploadComplete">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClientflpAttach.SetVisible(true);
  	HiddenFieldExam.Set('imgExam',1);
	lblImageWarning.SetVisible(false);
	HyperLinkExamFileURL.SetVisible(true);
	HyperLinkExamFileURL.SetImageUrl('../../Image/DocMeFile/Exams/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientflpAttach.SetVisible(false);
	lblImageWarning.SetVisible(true);
	HyperLinkExamFileURL.SetVisible(false);
	HyperLinkExamFileURL.SetImageUrl('');    
  	HiddenFieldExam.Set('imgExam',0);
	}
}" />
                                                            </TSPControls:CustomAspxUploadControl>
                                                            <dxe:ASPxLabel ID="lblImageWarning" runat="server" ClientInstanceName="lblImageWarning"
                                                                ClientVisible="False" ForeColor="Red" Text="تصویر کارنامه قبولی راانتخاب نمایید">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxImage ID="imgEndUploadImgflpAttach" runat="server" ClientInstanceName="imgEndUploadImgClientflpAttach"
                                                                ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویرانتخاب شد">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <dxe:ASPxImage ID="HyperLinkExamFileURL" runat="server" ClientInstanceName="HyperLinkExamFileURL" Width="75px"
                                                Height="75px" Target="_blank" Text="تصویر کارنامه قبولی">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تصویر گواهینامه آموزشی*" ID="lblPeriod" ClientInstanceName="lblPeriod" ClientVisible="false"></dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxp:ASPxPanel runat="server" ID="PanelPeriodImg" ClientInstanceName="PanelPeriodImg" ClientVisible="false">
                                                <PanelCollection>
                                                    <dxp:PanelContent>

                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxUploadControl ID="UploadControlPeriodImgURL" runat="server" ClientInstanceName="UploadControlPeriodImgURL"
                                                                            UploadWhenFileChoosed="true" OnFileUploadComplete="UploadControlPeriodImgURL_FileUploadComplete">
                                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadPeriodImgURL.SetVisible(true);
  	 HiddenFieldExam.Set('PeriodImg',1);
	lblImagePeriodWarning.SetVisible(false);
	HyperLinkPeriodImg.SetVisible(true);
	HyperLinkPeriodImg.SetImageUrl('../../Image/DocMeFile/ImplementPeriod/'+e.callbackData);
	}
	else{
	imgEndUploadPeriodImgURL.SetVisible(false);
	lblImagePeriodWarning.SetVisible(true);
	HyperLinkPeriodImg.SetVisible(false);
	HyperLinkPeriodImg.SetImageUrl('');    
  	HiddenFieldExam.Set('PeriodImg',0);
	}
}" />
                                                                        </TSPControls:CustomAspxUploadControl>
                                                                        <dxe:ASPxLabel ID="lblImagePeriodWarning" runat="server" ClientInstanceName="lblImagePeriodWarning"
                                                                            ClientVisible="False" ForeColor="Red" Text="تصویر گواهینامه آموزشی راانتخاب نمایید">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxImage ID="imgEndUploadPeriodImgURL" runat="server" ClientInstanceName="imgEndUploadPeriodImgURL"
                                                                            ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویرانتخاب شد">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <dxe:ASPxImage ID="HyperLinkPeriodImg" runat="server" ClientInstanceName="HyperLinkPeriodImg" Width="75px"
                                                            Height="75px"
                                                            Target="_blank" Text="تصویر گواهینامه دوره آموزشی ورود به حرفه اجرا">
                                                        </dxe:ASPxImage>
                                                    </dxp:PanelContent>

                                                </PanelCollection>
                                            </dxp:ASPxPanel>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            Width="25px" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click" CausesValidation="true">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s,e){
                                                         if (ASPxClientEdit.ValidateGroup('ValidationExam') == false)
                                                         {
                                                                e.processOnServer=false;
                                                                return;
                                                          }
                                                        else
	                                                            e.processOnServer=true;
                                                        }" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnBack_Click">
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
                        <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldExam" ClientInstanceName="HiddenFieldExam">
                        </dxhf:ASPxHiddenField>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ObjectDataSource ID="ObjdsGrade" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsMajor" runat="server" SelectMethod="FindMjParents"
                TypeName="TSP.DataManager.MajorManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsMemberLicence" runat="server" SelectMethod="SelectMajorParents"
                TypeName="TSP.DataManager.MemberLicenceManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="MemberId"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsTestCondition" runat="server" SelectMethod="SelectByMajor"
                TypeName="TSP.DataManager.DocTestConditionManager" OldValuesParameterFormatString="original_{0}"
                CacheKeyDependency="CachePersonMember">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="0" Name="Inactive"></asp:Parameter>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="MjId"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsTestType" runat="server" SelectMethod="SelectByMajor"
                TypeName="TSP.DataManager.DocTestTypeManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="MjId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>

            <asp:ObjectDataSource ID="ObjectDataSourceTestType" runat="server" SelectMethod="SelectByTestConditionForExam"
                TypeName="TSP.DataManager.DocTestConditionDetailManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="TCondId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>

            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
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
