<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="OfficeMemberShow.aspx.cs" Inherits="Members_Office_OfficeMemberShow" Title="مشخصات عضو" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
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

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetMember() {
            lMeNo.SetVisible(true);
            MeNo.SetVisible(true);
            lOtpCode.SetVisible(false);
            OtpCode.SetVisible(false);


            lblMjId.SetVisible(false);
            lblMjName.SetVisible(false);
            MjId.SetVisible(false);
            MjName.SetVisible(false);
            rdpGrade.SetVisible(false);


        }
        function SetKardanMemar() {
            lMeNo.SetVisible(false);
            MeNo.SetVisible(false);
            lOtpCode.SetVisible(true);
            OtpCode.SetVisible(true);


            lblMjId.SetVisible(true);
            lblMjName.SetVisible(true);
            MjId.SetVisible(true);
            MjName.SetVisible(true);
            rdpGrade.SetVisible(true);

        }
        function SetOther() {
            lMeNo.SetVisible(false);
            MeNo.SetVisible(false);
            lOtpCode.SetVisible(false);
            OtpCode.SetVisible(false);


            lblMjId.SetVisible(true);
            lblMjName.SetVisible(true);
            MjId.SetVisible(true);
            MjName.SetVisible(true);
            rdpGrade.SetVisible(false);

        }

        function ClearForm() {
            MeNo.SetText("");
            FirstName.SetText("");
            LastName.SetText("");
            FatherName.SetText("");
            document.getElementById('<%=txtBirthDate.ClientID%>').value = "";
            BirthPlace.SetText("");
            SSN.SetText("");
            IdNo.SetText("");
            FileNo.SetText("");
            lblE.SetVisible(false);

            img.SetImageUrl('../../Images/person.gif');

            chb.SetChecked(false);
            imgEmza.SetVisible(false);
            imgEmza.SetImageUrl("");

            document.getElementById('<%=txtStartDate.ClientID%>').value = "";

            position.SetSelectedIndex(-1);
            time.SetSelectedIndex(1);
            Desc.SetText("");

            FileNo.SetText("");
            Tel.SetText("");
            Tel_pre.SetText("");
            MobileNo.SetText("");
            Address.SetText("");
            OtpCode.SetText("");

            MjId.SetSelectedIndex(-1);
            MjName.SetText("");

        }
    </script>

    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" WorkDayCSS="PickerWorkDayCSS" WeekDayCSS="PickerWeekDayCSS" SelectedCSS="PickerSelectedCSS" HeaderCSS="PickerHeaderCSS" FrameCSS="PickerCSS" ForbidenCSS="PickerForbidenCSS" FooterCSS="PickerFooterCSS" CalendarDayWidth="50" CalendarCSS="PickerCalendarCSS">
    </pdc:PersianDateScriptManager>

    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server" visible="true">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#"><span style="color: #000000">بس</span>تن</a>]<br />
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table>
                    <tbody>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">


                                    <Image Url="~/Images/icons/Back.png"></Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <div style="width: 100%; text-align: right">
        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="شرکت :">
        </dxe:ASPxLabel>
        <dxe:ASPxLabel ID="lblOfName" runat="server">
        </dxe:ASPxLabel>
    </div>
    <br />
    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشخصات عضو" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
                <div class="row">
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="نوع عضو" ID="ASPxLabel1"></dxe:ASPxLabel>
                    </div>
                    <div class="col-md-9">
                        <TSPControls:CustomAspxComboBox runat="server"  ID="ComboType" ValueType="System.String" SelectedIndex="0" ClientInstanceName="cmbType" ReadOnly="True" ClientEnabled="False">
                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
if(cmbType.GetValue() == 0)
{
ClearForm();
  SetMember();
  }
if(cmbType.GetValue() == 1 || cmbType.GetValue() == 2)
{  
  ClearForm();
  SetKardanMemar();
}
if(cmbType.GetValue() == 3)
{  
  ClearForm();
  SetOther();
}
}"></ClientSideEvents>
                            <Items>
                                <dxe:ListEditItem Value="0" Text="عضو نظام"></dxe:ListEditItem>
                                <dxe:ListEditItem Value="1" Text="کاردان"></dxe:ListEditItem>
                                <dxe:ListEditItem Value="2" Text="معمار تجربی"></dxe:ListEditItem>
                                <dxe:ListEditItem Value="3" Text="دیگر اشخاص"></dxe:ListEditItem>
                            </Items>

                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                <RequiredField IsRequired="True" ErrorText="نوع عضو را انتخاب نمایید"></RequiredField>

                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>

                            <ButtonStyle Width="13px"></ButtonStyle>
                        </TSPControls:CustomAspxComboBox>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="lblMeNo" ClientInstanceName="lMeNo"></dxe:ASPxLabel>
                    </div>
                    <div class="col-md-9">
                        <TSPControls:CustomTextBox runat="server"  AutoPostBack="True" ID="txtMeNo" ClientInstanceName="MeNo" ReadOnly="True" ClientEnabled="False">
                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>

                                <RegularExpression ErrorText="کد را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>

                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>
                        </TSPControls:CustomTextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="کد عضویت" ClientVisible="False" ID="lblOtpCode" ClientInstanceName="lOtpCode"></dxe:ASPxLabel>
                    </div>
                    <div class="col-md-9">
                        <TSPControls:CustomTextBox runat="server" ClientVisible="False"  ID="txtOtpCode" ClientInstanceName="OtpCode" ReadOnly="True">
                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                <RequiredField ErrorText=""></RequiredField>

                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>
                        </TSPControls:CustomTextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label runat="server" Text="نام" ID="Label4"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <TSPControls:CustomTextBox runat="server"  ClientEnabled="False" ID="txtFirstName" ClientInstanceName="FirstName" ReadOnly="True">
                            <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="نام را وارد نمایید" ErrorTextPosition="Bottom">
                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                <RequiredField ErrorText=""></RequiredField>

                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>

                            <ClientSideEvents Validation="function(s, e) {
		if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                        </TSPControls:CustomTextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" Text="نام خانوادگی" ID="Label5"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <TSPControls:CustomTextBox runat="server"  ClientEnabled="False" ID="txtLastName" ClientInstanceName="LastName" ReadOnly="True">
                            <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="نام خانوادگی را وارد نمایید" ErrorTextPosition="Bottom">
                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                <RequiredField ErrorText=""></RequiredField>

                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>

                            <ClientSideEvents Validation="function(s, e) {
		if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                        </TSPControls:CustomTextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label runat="server" Text="نام پدر" ID="Label11"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <TSPControls:CustomTextBox runat="server"  ClientEnabled="False" ID="txtFatherName" ClientInstanceName="FatherName" ReadOnly="True">
                            <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="نام پدر را وارد نمایید" ErrorTextPosition="Bottom">
                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                <RequiredField ErrorText=""></RequiredField>

                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>

                            <ClientSideEvents Validation="function(s, e) {
	if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                        </TSPControls:CustomTextBox>
                    </div>
                    <div class="col-md-3"></div>
                    <div class="col-md-3"></div>
                </div>

                <div class="row">
                    <div class="col-md-3">
                        <asp:Label runat="server" Text="شماره پروانه اشتغال" Width="115px" ID="Label22"></asp:Label></div>
                    <div class="col-md-3">
                        <TSPControls:CustomTextBox runat="server"  ClientEnabled="False" ID="txtFileNo" ClientInstanceName="FileNo" ReadOnly="True">
                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                <RegularExpression ErrorText=""></RegularExpression>

                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>
                        </TSPControls:CustomTextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label2" runat="server" Text="تاریخ اعتبار پروانه" Width="115px"></asp:Label></div>
                    <div class="col-md-3">
                        <TSPControls:CustomTextBox ID="txtFileNoDate" runat="server" ClientEnabled="False" ClientInstanceName="FileNoDate"
                             ReadOnly="True">
                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                <RegularExpression ErrorText="" />
                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px" />
                                </ErrorFrameStyle>
                            </ValidationSettings>
                        </TSPControls:CustomTextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label runat="server" Text="تاریخ تولد" ID="Label12"></asp:Label></div>
                    <div class="col-md-3">
                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="160px" Enabled="False" ShowPickerOnTop="True" ID="txtBirthDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" ReadOnly="True"></pdc:PersianDateTextBox></div>
                    <div class="col-md-3">
                        <asp:Label runat="server" Text="محل تولد" ID="Label13"></asp:Label></div>
                    <div class="col-md-3">
                        <TSPControls:CustomTextBox runat="server"  ClientEnabled="False" ID="txtBirthPlace" ClientInstanceName="BirthPlace" ReadOnly="True">
                            <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="محل تولد را وارد نمایید" ErrorTextPosition="Bottom">
                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                <RequiredField ErrorText=""></RequiredField>

                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>

                            <ClientSideEvents Validation="function(s, e) {
	if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                        </TSPControls:CustomTextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label runat="server" Text="شماره شناسنامه" ID="Label14"></asp:Label></div>
                    <div class="col-md-3">
                        <TSPControls:CustomTextBox runat="server"  MaxLength="10" ClientEnabled="False" ID="txtIdNo" ClientInstanceName="IdNo" ReadOnly="True">
                            <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="شماره شناسنامه را وارد نمایید" ErrorTextPosition="Bottom">
                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                <RequiredField ErrorText=""></RequiredField>

                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,10}"></RegularExpression>

                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>

                            <ClientSideEvents Validation="function(s, e) {
	if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                        </TSPControls:CustomTextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" Text="کد ملی" ID="Label15"></asp:Label></div>
                    <div class="col-md-3">
                        <TSPControls:CustomTextBox runat="server"  MaxLength="10" AutoPostBack="True" ClientEnabled="False" ID="txtSSN" ClientInstanceName="SSN" ReadOnly="True">
                            <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="کد ملی را وارد نمایید" ErrorTextPosition="Bottom">
                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                <RequiredField ErrorText=""></RequiredField>

                                <RegularExpression ErrorText="این کد صحیح نیست" ValidationExpression="\d{10}"></RegularExpression>

                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>

                            <ClientSideEvents Validation="function(s, e) {
	if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                        </TSPControls:CustomTextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label runat="server" Text="تلفن" ID="Lhe69"></asp:Label></div>
                    <div class="col-md-3">
                        <table>
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomTextBox runat="server" Width="80px" MaxLength="8" ClientEnabled="False" ID="txtTel" ClientInstanceName="Tel" ReadOnly="True">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <RequiredField ErrorText=""></RequiredField>

                                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{5,8}"></RegularExpression>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td style="vertical-align: top">
                                        <asp:Label runat="server" Text="-" Width="13px" ID="Lbjjje71"></asp:Label>
                                    </td>
                                    <td style="vertical-align: top">
                                        <TSPControls:CustomTextBox runat="server" Width="40px" MaxLength="4" ClientEnabled="False" ID="txtTel_pre" ClientInstanceName="Tel_pre" ReadOnly="True">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                <RequiredField ErrorText=""></RequiredField>

                                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{2,3}"></RegularExpression>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" Text="شماره همراه" ID="Lal75"></asp:Label></div>
                    <div class="col-md-3">
                        <TSPControls:CustomTextBox runat="server"  MaxLength="11" ClientEnabled="False" ID="txtMobile" ClientInstanceName="MobileNo" ReadOnly="True">
                            <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="شماره همراه را وارد نمایید" ErrorTextPosition="Bottom">
                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                <RequiredField ErrorText=""></RequiredField>

                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{10}"></RegularExpression>

                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>

                            <ClientSideEvents Validation="function(s, e) {
	if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                        </TSPControls:CustomTextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label runat="server" Text="آدرس" ID="Lbه76"></asp:Label></div>
                    <div class="col-md-9">
                        <TSPControls:CustomASPXMemo runat="server" Height="26px"  ClientEnabled="False" ID="txtAddress" ClientInstanceName="Address" ReadOnly="True">
                            <ValidationSettings>
                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>
                        </TSPControls:CustomASPXMemo>
                    </div>
                  
                </div>
                <div class="row">
                      <div class="col-md-9">
                        <asp:Label runat="server" Text="تصویر" ID="Label17"></asp:Label></div>
                    <div class="col-md-3">
                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgMember" ClientInstanceName="img">
                            <EmptyImage Url="~/Images/person.gif"></EmptyImage>
                        </dxe:ASPxImage>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="رشته" ClientVisible="False" ID="ASPxLabel5" ClientInstanceName="lblMjId"></dxe:ASPxLabel>
                    </div>
                    <div class="col-md-3">
                        <TSPControls:CustomAspxComboBox runat="server" ClientVisible="False"  TextField="MjName" ID="ComboMjId" DataSourceID="OdbMajor" ValueType="System.String" ValueField="MjId" ClientInstanceName="MjId" ReadOnly="True">
                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                <RequiredField IsRequired="True" ErrorText="رشته را انتخاب نمایید"></RequiredField>

                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>

                            <ButtonStyle Width="13px"></ButtonStyle>
                        </TSPControls:CustomAspxComboBox>
                    </div>
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="مدرک" ClientVisible="False" ID="ASPxLabel6" ClientInstanceName="lblMjName"></dxe:ASPxLabel>
                    </div>
                    <div class="col-md-3">
                        <TSPControls:CustomTextBox runat="server" ClientVisible="False"  ID="txtMjName" ClientInstanceName="MjName" ReadOnly="True">
                            <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                <RequiredField IsRequired="True" ErrorText="مدرک را وارد نمایید"></RequiredField>

                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>

                            <ClientSideEvents Validation="function(s, e) {
		if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                        </TSPControls:CustomTextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label runat="server" Text="سمت" ID="Label32"></asp:Label></div>
                    <div class="col-md-3">
                        <TSPControls:CustomAspxComboBox runat="server"  TextField="OfpName" ID="drdPosition" EnableIncrementalFiltering="True" DataSourceID="ODBPosition" ValueType="System.String" ValueField="OfpId" ClientInstanceName="position" ReadOnly="True">
                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                <RequiredField IsRequired="True" ErrorText="سمت را انتخاب نمایید"></RequiredField>

                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>

                            <ButtonStyle Width="13px"></ButtonStyle>
                        </TSPControls:CustomAspxComboBox>
                    </div>
                    <div class="col-md-3"></div>
                    <div class="col-md-3"></div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label runat="server" Text="نوع همکاری" Width="73px" ID="Label53"></asp:Label></div>
                    <div class="col-md-3">
                        <TSPControls:CustomAspxComboBox runat="server"  ID="ComboTime" ValueType="System.String" SelectedIndex="1" ClientInstanceName="time" ReadOnly="True">
                            <Items>
                                <dxe:ListEditItem Value="0" Text="پاره وقت"></dxe:ListEditItem>
                                <dxe:ListEditItem Value="1" Text="تمام وقت"></dxe:ListEditItem>
                            </Items>

                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                <RequiredField IsRequired="True" ErrorText="نوع همکاری را انتخاب نمایید"></RequiredField>

                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>

                            <ButtonStyle Width="13px"></ButtonStyle>
                        </TSPControls:CustomAspxComboBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" Text="تاریخ شروع همکاری" Width="117px" ID="Label42"></asp:Label></div>
                    <div class="col-md-3">
                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="160px" ShowPickerOnTop="True" ID="txtStartDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" ReadOnly="True"></pdc:PersianDateTextBox></div>
                </div>
                <div class="row">
                    <TSPControls:CustomASPxCheckBox runat="server" Text="دارای حق امضا" EnableClientSideAPI="True" Width="105px" ID="chbHaghEmza" ClientInstanceName="chb">
                    </TSPControls:CustomASPxCheckBox>

                </div>
                <div class="row">
                    <div class="col-md-3">
                        <dxe:ASPxLabel runat="server" Text="تصویر امضا" ClientVisible="False" ID="lbEmza" ClientInstanceName="lblE"></dxe:ASPxLabel>
                    </div>
                    <div class="col-md-9">
                        <dxe:ASPxImage runat="server" Height="75px" ClientVisible="False" Width="75px" ID="imgEmza" ClientInstanceName="imgEmza">
                            <EmptyImage Url="~/Images/noimage.gif" />
                        </dxe:ASPxImage>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label runat="server" Text="توضیحات" ID="Label45"></asp:Label></div>
                    <div class="col-md-9">
                        <TSPControls:CustomASPXMemo runat="server" Height="26px" Width="510px" ID="txtDesc" ClientInstanceName="Desc" ReadOnly="True">
                            <ValidationSettings>
                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                <ErrorFrameStyle ImageSpacing="4px">
                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                </ErrorFrameStyle>
                            </ValidationSettings>
                        </TSPControls:CustomASPXMemo>
                    </div>

                </div>

            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
    <br />
    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanelGrade" HeaderText="صلاحیت ها" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <TSPControls:CustomAspxDevGridView runat="server" ID="CustomAspxDevGridView1" KeyFieldName="MGId" AutoGenerateColumns="False">

                    <Columns>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GrdName" Caption="پایه"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="ResName" Caption="صلاحیت"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="MjName" Caption="رشته"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2"></dxwgv:GridViewDataTextColumn>
                    </Columns>
                </TSPControls:CustomAspxDevGridView>


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
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">


                                    <Image Url="~/Images/icons/Back.png"></Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>


    <asp:HiddenField ID="OfficeId" runat="server" Visible="False" />
    <asp:HiddenField ID="OfPersonId" runat="server" Visible="False" />
    <asp:HiddenField ID="OffMeType" runat="server" Visible="False" />
    <asp:HiddenField ID="OffMemberId" runat="server" Visible="False" />
    <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
    <asp:ObjectDataSource ID="ODBPosition" runat="server" DeleteMethod="Delete" FilterExpression="OfType={0}"
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
        TypeName="TSP.DataManager.OfficePositionManager" UpdateMethod="Update">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False" />
    <asp:ObjectDataSource ID="OdbMajor" runat="server" DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager" UpdateMethod="Update"></asp:ObjectDataSource>
    <asp:HiddenField ID="HDOtpId" runat="server" Visible="False" />
</asp:Content>






