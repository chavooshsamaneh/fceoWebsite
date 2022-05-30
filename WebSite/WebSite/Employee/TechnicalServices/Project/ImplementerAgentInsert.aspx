<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="ImplementerAgentInsert.aspx.cs" Inherits="Employee_TechnicalServices_Project_ImplementerAgentInsert"
    Title="مشخصات نماینده مجری" %>

<%@ Register Assembly="DevExpress.XtraCharts.v17.1.Web, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts" TagPrefix="cc2" %>
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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
        <script language="javascript">
            function SetMember() {
                flpjob.SetVisible(false);
                LabelJob.SetVisible(false);
                imgtik.SetVisible(false);
                hpjob.SetVisible(false);

                //lMeNo.SetVisible(true);
                //MeNo.SetVisible(true);
                //lOtpCode.SetVisible(false);
                //OtpCode.SetVisible(false);

                //FirstName.SetEnabled(false);
                //LastName.SetEnabled(false);
                //FatherName.SetEnabled(false);

                //BirthPlace.SetEnabled(false);
                //SSN.SetEnabled(false);
                //IdNo.SetEnabled(false);
                //FileNo.SetEnabled(false);
                //FileNoDate.SetEnabled(false);
                //Tel.SetEnabled(false);
                //MobileNo.SetEnabled(false);
                //Address.SetEnabled(false);


                //lblMjId.SetVisible(false);
                //lblMjName.SetVisible(false);
                //MjId.SetVisible(false);
                //MjName.SetVisible(false);


            }
            function SetKardan() {
                flpjob.SetVisible(true);
                LabelJob.SetVisible(true);

            }
            function ClearForm() {
                MeNo.SetText("");
                FirstName.SetText("");
                LastName.SetText("");
                FatherName.SetText("");
                SSN.SetText("");
                FileNo.SetText("");
                FileNoDate.SetText("");

            }
        </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                    <table  >
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                                        ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnSave_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if(cmbType.GetValue() == 1)
	{
		if(HDjob.Get('name')!= 1)
		{
		lbljob.SetVisible(true);
		e.processOnServer=false;
		}
	
	}
}"></ClientSideEvents>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/save.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnShowPpcAttachPageToAutomationLetter" runat="server" AutoPostBack="False"
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
                <TSP:ProjectInfo ID="prjInfo" runat="server" />
                <br />
                	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanelImp" HeaderText="مشخصات مجری" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
    
                                <table dir="rtl" width="100%">
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="نوع مجری" ID="ASPxLabel2">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtImpType"  Width="100%" ReadOnly="True"
                                                    >
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
                                            <td valign="top" align="right">
                                            </td>
                                            <td valign="top" align="right">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="ASPxLabel10">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtImpId"  Width="100%" ReadOnly="True"
                                                    >
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
                                                <dxe:ASPxLabel runat="server" Text="نام مجری" ID="ASPxLabelFirstName" ClientInstanceName="lblFirstName">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtImpName"  Width="100%" ReadOnly="True"
                                                    >
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
                                    </tbody>
                                </table>
                             </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                    <br />
                	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="جدید" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                                <table dir="rtl" id="Table1" width="100%">
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="right" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="نوع نماینده" ID="ASPxLabel1">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td dir="ltr" valign="top" align="right" width="35%">
                                                <TSPControls:CustomAspxComboBox runat="server"  Width="100%" 
                                                 ID="ComboType" ValueType="System.String"
                                                    SelectedIndex="0" ClientInstanceName="cmbType" RightToLeft="True" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
if(cmbType.GetValue() == 0)
{
 ClearForm();
  SetMember();
  }
else
{  
  ClearForm();
  SetKardan();
}

}"></ClientSideEvents>
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="نوع عضو را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <Items>
                                                        <dxe:ListEditItem Value="0" Text="عضو حقیقی" Selected="True"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Value="1" Text="کاردان"></dxe:ListEditItem>
                                                        <dxe:ListEditItem Text="معمار تجربی" Value="2" />
                                                    </Items>
                                                    <ButtonStyle Width="13px">
                                                    </ButtonStyle>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td valign="top" align="right" width="15%">
                                                <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="lblMeNo" ClientInstanceName="lMeNo">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" width="35%">
                                                <TSPControls:CustomTextBox runat="server" ID="txtMeNo"  Width="100%" AutoPostBack="True"
                                                    ClientInstanceName="MeNo"  OnTextChanged="txtMeNo_TextChanged">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText="کد را صحیح وارد نمایید" ValidationExpression="\d*">
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
                                                <asp:Label runat="server" Text="نام" ID="Label4"></asp:Label>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtFirstName"  Width="100%"
                                                    ReadOnly="True" ClientInstanceName="FirstName" >
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <asp:Label runat="server" Text="نام خانوادگی" ID="Label5"></asp:Label>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtLastName"  Width="100%"
                                                    ReadOnly="True" ClientInstanceName="LastName" >
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <asp:Label runat="server" Text="نام پدر" ID="Label11"></asp:Label>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtFatherName"  Width="100%"
                                                    ReadOnly="True" ClientInstanceName="FatherName" >
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <asp:Label runat="server" Text="کد ملی" ID="Label15"></asp:Label>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtSSN"  Width="100%" MaxLength="10"
                                                    ReadOnly="True" ClientInstanceName="SSN" >
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <asp:Label runat="server" Text="شماره پروانه اشتغال" ID="Label22"></asp:Label>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtFileNo"  Width="100%" ReadOnly="True"
                                                    ClientInstanceName="FileNo"  Style="direction: ltr">
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                                <asp:Label runat="server" Text="تاریخ اعتبار پروانه" ID="Label1"></asp:Label>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtFileNoDate"  Width="100%"
                                                    ReadOnly="True" ClientInstanceName="FileNoDate" 
                                                    Style="direction: ltr">
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <asp:Label runat="server" Text="صلاحیت اجرا" ID="Label2"></asp:Label>
                                            </td>
                                            <td valign="top" align="right">
                                                <TSPControls:CustomTextBox runat="server" ID="txtImpRes"  Width="100%" ReadOnly="True"
                                                    ClientInstanceName="txtImpRes" >
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right">
                                            </td>
                                            <td valign="top" align="right">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                <dxe:ASPxLabel runat="server" Text="سوابق تجربه کاری" ClientVisible="False" ID="ASPxLabelJob"
                                                    ClientInstanceName="LabelJob">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td valign="top" align="right" colspan="3">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <TSPControls:CustomAspxUploadControl runat="server" ShowProgressPanel="True" MaxSizeForUploadFile="0"
                                                                    ID="flp_Job" InputType="Images" ClientInstanceName="flpjob" OnFileUploadComplete="flp_Job_FileUploadComplete">
                                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
	imgtik.SetVisible(true);
	HDjob.Set('name',1);
	lbljob.SetVisible(false);
	hpjob.SetVisible(true);
	hpjob.SetNavigateUrl('../../../Image/Temp/'+e.callbackData);
}"></ClientSideEvents>
                                                                </TSPControls:CustomAspxUploadControl>
                                                                <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                                    ID="ASPxLabel101" ForeColor="Red" ClientInstanceName="lbljob">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                    ID="ASPxImage1" ClientInstanceName="imgtik">
                                                                </dxe:ASPxImage>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <dxe:ASPxHyperLink runat="server" Text="آدرس فایل" ClientVisible="False" Target="_blank"
                                                    ID="Hp_Job" ClientInstanceName="hpjob">
                                                </dxe:ASPxHyperLink>
                                                <TSPControls:CustomAspxButton runat="server" Text="click" CausesValidation="False" ClientVisible="False"
                                                    Width="62px" ID="ASPxButton1" EnableClientSideAPI="True" AutoPostBack="False"
                                                    UseSubmitBehavior="False" ClientInstanceName="ibut">
                                                    <ClientSideEvents Click="function(s, e) {
flpjob.SetVisible(false);
}"></ClientSideEvents>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right" colspan="4">
                                                <TSPControls:CustomASPxCheckBox runat="server" Text="شخص مورد نظر در پروژه دیگری مشغول به کار می باشد, آیا پلاک های ثبتی همجوار است؟"
                                                    ID="ChbCheck" ForeColor="Red" Visible="False">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RequiredField IsRequired="True" ErrorText="تایید همجواری پلاک ها الزامی می باشد">
                                                        </RequiredField>
                                                    </ValidationSettings>
                                                </TSPControls:CustomASPxCheckBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                  </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                    <br />
                  <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                        <table >
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                            OnClick="btnSave_Click">
                                                                            <ClientSideEvents Click="function(s, e) {
	if(cmbType.GetValue() == 1)
	{
		if(HDjob.Get('name')!= 1)
		{
		lbljob.SetVisible(true);
		e.processOnServer=false;
		}
		
	}
}"></ClientSideEvents>
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                            </HoverStyle>
                                                                            <Image  Url="~/Images/icons/save.png">
                                                                            </Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td >
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnShowPpcAttachPageToAutomationLetter2" runat="server" AutoPostBack="False"
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
                <asp:HiddenField ID="HDProjectId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="HDImpId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="HDImpAgentId" runat="server" Visible="False"></asp:HiddenField>
                <dxhf:ASPxHiddenField ID="HD_Job" runat="server" ClientInstanceName="HDjob">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="HD_File" runat="server" ClientInstanceName="HDfile">
                </dxhf:ASPxHiddenField>
                <asp:ObjectDataSource ID="OdbMajor" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
                    SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager" UpdateMethod="Update">
                </asp:ObjectDataSource>
                <asp:HiddenField ID="RequestId" runat="server" Visible="False"></asp:HiddenField>
                <asp:HiddenField ID="HDMode" runat="server" Visible="False"></asp:HiddenField>
                <TSPControls:CustomASPxPopupControl ID="ppcAttachPageToAutomationLetter" runat="server" Width="404px"
                      HeaderText="اضافه کردن صفحه به سند اتوماسیون اداری"
                    PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" Modal="True"
                     Height="77px" EnableViewState="False" EnableClientSideAPI="True"
                    EnableAnimation="False" CloseAction="CloseButton" ClientInstanceName="ppcAttachPageToAutomationLetter"
                    AutoUpdatePosition="True" AllowDragging="True">
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
                                                                                        <TSPControls:CustomAspxButton  runat="server" Text="&#160;ذخیره"  CausesValidation="False"
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
                                                        <TSPControls:CustomAspxButton  runat="server" Text="&#160;خروج"  CausesValidation="False"
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
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
