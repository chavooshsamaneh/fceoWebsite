<%@ Page Language="C#" MasterPageFile="~/MasterPageWebsite.master" AutoEventWireup="true"
    CodeFile="WizardOfficeMember.aspx.cs" Inherits="NezamRegister_WizardOfficeMember"
    Title="عضویت حقوقی - اعضای شرکت" %>

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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script language="javascript">
        function SetMember() {
            lblImgWarning.SetVisible(false);
            lblFileNo.SetVisible(true);
            lblFileDate.SetVisible(true);
            FileNo.SetVisible(true);
            //FileDate.SetVisible(true);
            lMeNo.SetVisible(true);
            MeNo.SetVisible(true);
            lOtpCode.SetVisible(false);
            OtpCode.SetVisible(false);

            FirstName.SetEnabled(false);
            LastName.SetEnabled(false);
            FatherName.SetEnabled(false);
            document.getElementById('<%=txtBirthDate.ClientID%>').disabled = true;
            document.getElementById('<%=txtBirthDate.ClientID%>').setAttribute("usedatepicker", false);

            document.getElementById('<%=txtFileDate.ClientID%>').disabled = true;
            document.getElementById('<%=txtFileDate.ClientID%>').setAttribute("usedatepicker", false);

            BirthPlace.SetEnabled(false);
            SSN.SetEnabled(false);
            IdNo.SetEnabled(false);
            FileNo.SetEnabled(false);
            Tel_pre.SetEnabled(false);
            Tel.SetEnabled(false);
            MobileNo.SetEnabled(false);
            Address.SetEnabled(false);
            //FileDate.SetEnabled(false);
            flpimg.SetVisible(false);
            rdpGrade.SetVisible(true);

            PnlKardanMemarInfo.SetVisible(false);
            ValidatorEnable(document.getElementById('<%=PersianDateValidator1.ClientID%>'), false);
            PnlbasicInfo.SetVisible(false);
        }
        function SetKardanMemar() {
            lblImgWarning.SetVisible(false);
            lblFileNo.SetVisible(true);
            lblFileDate.SetVisible(true);
            FileNo.SetVisible(true);
            //FileDate.SetVisible(true);

            lMeNo.SetVisible(false);
            MeNo.SetVisible(false);
            lOtpCode.SetVisible(true);
            OtpCode.SetVisible(true);

            FirstName.SetEnabled(false);
            LastName.SetEnabled(false);
            FatherName.SetEnabled(false);
            document.getElementById('<%=txtBirthDate.ClientID%>').disabled = true;
            document.getElementById('<%=txtBirthDate.ClientID%>').setAttribute("usedatepicker", false);

            document.getElementById('<%=txtFileDate.ClientID%>').disabled = true;
            document.getElementById('<%=txtFileDate.ClientID%>').setAttribute("usedatepicker", false);
            BirthPlace.SetEnabled(false);
            SSN.SetEnabled(false);
            IdNo.SetEnabled(false);
            FileNo.SetEnabled(false);
            Tel_pre.SetEnabled(false);
            Tel.SetEnabled(false);
            MobileNo.SetEnabled(false);
            Address.SetEnabled(false);
            //FileDate.SetEnabled(false);
            flpimg.SetVisible(false);

            rdpGrade.SetVisible(true);

            PnlKardanMemarInfo.SetVisible(false);
            ValidatorEnable(document.getElementById('<%=PersianDateValidator1.ClientID%>'), false);
            PnlbasicInfo.SetVisible(true);
        }

        function SetNewKardanMemar() {
            lblImgWarning.SetVisible(true);
            lblFileNo.SetVisible(true);
            lblFileDate.SetVisible(true);
            FileNo.SetVisible(true);
            //FileDate.SetVisible(true);

            lMeNo.SetVisible(false);
            MeNo.SetVisible(false);
            lOtpCode.SetVisible(true);
            OtpCode.SetVisible(true);

            FirstName.SetEnabled(true);
            LastName.SetEnabled(true);
            FatherName.SetEnabled(true);
            document.getElementById('<%=txtBirthDate.ClientID%>').disabled = false;
            document.getElementById('<%=txtBirthDate.ClientID%>').setAttribute("usedatepicker", true);

            document.getElementById('<%=txtFileDate.ClientID%>').disabled = false;
            document.getElementById('<%=txtFileDate.ClientID%>').setAttribute("usedatepicker", true);
            BirthPlace.SetEnabled(true);
            SSN.SetEnabled(true);
            IdNo.SetEnabled(true);
            FileNo.SetEnabled(true);
            Tel_pre.SetEnabled(true);
            Tel.SetEnabled(true);
            MobileNo.SetEnabled(true);
            Address.SetEnabled(true);
            //FileDate.SetEnabled(true);

            flpimg.SetVisible(true);
            rdpGrade.SetVisible(true);

            PnlKardanMemarInfo.SetVisible(true);
            ValidatorEnable(document.getElementById('<%=PersianDateValidator1.ClientID%>'), true);
            PnlbasicInfo.SetVisible(true);
        }

        function SetOther() {
            lblImgWarning.SetVisible(true);
            lblFileNo.SetVisible(false);
            lblFileDate.SetVisible(false);
            FileNo.SetVisible(false);
            //FileDate.SetVisible(false);

            lMeNo.SetVisible(false);
            MeNo.SetVisible(false);
            lOtpCode.SetVisible(false);
            OtpCode.SetVisible(false);

            FirstName.SetEnabled(true);
            LastName.SetEnabled(true);
            FatherName.SetEnabled(true);
            document.getElementById('<%=txtBirthDate.ClientID%>').disabled = false;
            document.getElementById('<%=txtBirthDate.ClientID%>').setAttribute("usedatepicker", true);

            document.getElementById('<%=txtFileDate.ClientID%>').style.visibility = "hidden";
            //document.getElementById('<%=txtFileDate.ClientID%>').setAttribute("usedatepicker", false);
            BirthPlace.SetEnabled(true);
            SSN.SetEnabled(true);
            IdNo.SetEnabled(true);
            FileNo.SetEnabled(true);
            Tel_pre.SetEnabled(true);
            Tel.SetEnabled(true);
            MobileNo.SetEnabled(true);
            Address.SetEnabled(true);
            flpimg.SetVisible(true);
            ValidatorEnable(document.getElementById('<%=PersianDateValidator1.ClientID%>'), true);

            PnlKardanMemarInfo.SetVisible(false);
            rdpGrade.SetVisible(false);
            PnlbasicInfo.SetVisible(true);
        }
        function SetGridValues() {
            gridMember.GetRowValues(gridMember.GetFocusedRowIndex(), 'ID;OfmTypeId;MeId;OtpCode;FirstName;LastName;FatherName;IdNo;SSN;BirthDate;BirthPlace;ImageUrl;OfpId;HasSignRight;SignUrl;StartDate;FileNo;IsFullTime;Description;Tel;MobileNo;Address;Tel_pre;FileDate', SetTypeValue);
        }
        function SetTypeValue(values) {

            //  alert(values);
            //return;
            if (values[1].toString() == '1') {
                SetMember();
                cmbType.SetValue(0);
                MeNo.SetText(values[2]);
            }
            else if (values[1].toString() == '2') {
                SetKardanMemar();
                cmbType.SetValue(1);
                OtpCode.SetText(values[3]);
            }
            else if (values[1].toString() == '3') {
                cmbType.SetValue(3);
                SetOther();
            }
            else if (values[1].toString() == '4') {
                cmbType.SetValue(2);
                SetKardanMemar();
                OtpCode.SetText(values[3]);
            }
            //{

            FirstName.SetText(values[4]);
            LastName.SetText(values[5]);
            FatherName.SetText(values[6]);
            document.getElementById('<%=txtBirthDate.ClientID%>').value = values[9];
            BirthPlace.SetText(values[10]);
            SSN.SetText(values[8]);
            IdNo.SetText(values[7]);
            FileNo.SetText(values[16]);
            //FileDate.SetText(values[23]);
            document.getElementById('<%=txtFileDate.ClientID%>').value = values[23];
            //alert(values[11]);
            var d = values[11];

            if (d != null && d != '') {
                d = d.replace('~/', '');
                d = '../' + d;
            }
            else {
                d = '../Images/person.png';
            }
            img.SetVisible(true);
            img.SetImageUrl(d);

            chb.SetChecked(values[13]);

            if (values[13] == true) {

                imgEmza.SetVisible(true);
                //lblE.SetVisible(true);

                var d2 = values[14];

                if (d2 != null && d2 != '') {
                    d2 = d2.replace('~/', '');
                    d2 = '../' + d2;
                }

                imgEmza.SetImageUrl(d2);
            }
            document.getElementById('<%=txtStartDate.ClientID%>').value = values[15];

            position.SetValue(values[12]);
            time.SetValue(values[17]);
            Desc.SetText(values[18]);
            //}


            Tel.SetText(values[19]);
            MobileNo.SetText(values[20]);
            Address.SetText(values[21]);
            Tel_pre.SetText(values[22]);

        }
        function ClearForm() {
            //FileDate.SetText("");
            document.getElementById('<%=txtFileDate.ClientID%>').value = "";
            MeNo.SetText("");
            FirstName.SetText("");
            LastName.SetText("");
            FatherName.SetText("");
            document.getElementById('<%=txtBirthDate.ClientID%>').value = "";
            BirthPlace.SetText("");
            SSN.SetText("");
            IdNo.SetText("");
            FileNo.SetText("");
            // lblE.SetVisible(false);

            img.SetImageUrl('../Images/person.png');

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


            HDimg.Set("name", "0");
            imgtik.SetVisible(false);
            flpimg.SetVisible(false);

            //flpEmza.SetVisible(false);
            HDEmza.Set("name", "0");
            imgEmzatik.SetVisible(false);

            HDfile.Set("name", "0");
            imgtikcommit.SetVisible(false);
            hp.SetVisible(false);

            CmbAgent.SetSelectedIndex(-1);
            CmbCity.SetSelectedIndex(-1);
            MjId.SetText("");
            MjName.SetText("");

            btn.SetEnabled(true);
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                visible="true">
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
                    <dxm:MenuItem Text="اعضای شرکت" Name="Member" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="آگهی های رسمی" Name="Letter">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="خلاصه اطلاعات" Name="Summary">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="ثبت نهایی" Name="End">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>
            <div class="row">
                <ul class="HelpUL">
                    <li>پس از تکمیل اطلاعات هر یک از اعضای شرکت بر روی دکمه اضافه به لیست کلیک نمایید تا به لیست پایین صفحه اضافه گردد.
                    </li>
                    <li>مدیرعامل شرکت های اجرایی را تنها می توان از بین اعضای سازمان انتخاب کرد. </li>
                    <li>در ثبت <b>کاردان یا معمار تجربی</b> به عنوان عضو شرکت به نکات زیر توجه نمایید:
                                        <ul type="circle">
                                            <li>جهت ثبت کاردان یا معمارتجربی از لیست نوع عضو، گزینه کاردان یا معمار تجربی را انتخاب
                                                نمائید سپس کد عضویت کانون کاردانها را وارد نمائید. چنانچه این شخص از قبل در سیستم
                                                ثبت شده باشد اطلاعات آن را نمایش می دهد در غیر این صورت از لیست نوع عضو، گزینه کاردان
                                                جدید یا معمار تجربی جدید را انتخاب نمائید. </li>
                                        </ul>
                    </li>
                </ul>
            </div>
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="اعضای شرکت" runat="server"
                Width="100%">
                <HeaderTemplate>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 20%; height: 28px" valign="middle">
                                <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="اعضای شرکت">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" style="width: 80%; height: 28px" valign="middle">
                                <TSPControls:CustomAspxButton IsMenuButton="true" AutoPostBack="false" ID="btnHelp" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False"
                                    Text=" " ToolTip="راهنما" UseSubmitBehavior="False">

                                    <Image Height="25px" Url="~/Images/Help.png" Width="25px">
                                    </Image>
                                    <ClientSideEvents Click="function(s,e){ ShowHelpWindow(HiddenHelp.Get('HelpAddress')); }"></ClientSideEvents>
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <PanelCollection>
                    <dxp:PanelContent>
                        <div class="row">
                            <div class="col-md-3">نوع عضو</div>
                            <div class="col-md-3">
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                    ID="ComboType" ValueType="System.String"
                                    SelectedIndex="0" ClientInstanceName="cmbType"
                                    EnableIncrementalFiltering="True" RightToLeft="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
grid.PerformCallback('Clear');
 ClearForm();
if(cmbType.GetValue() == 0)
{
  SetMember();
}
if(cmbType.GetValue() == 1 || cmbType.GetValue() == 2)
{  
  SetKardanMemar();
}
if(cmbType.GetValue() == 3)
{  
  SetOther();
}
if(cmbType.GetValue() == 5 || cmbType.GetValue() == 6)
{  
  SetNewKardanMemar();
}
}"></ClientSideEvents>
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                        <RequiredField IsRequired="True" ErrorText="نوع عضو را انتخاب نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                    <Items>
                                        <dxe:ListEditItem Value="0" Text="عضو نظام" Selected="True"></dxe:ListEditItem>
                                        <dxe:ListEditItem Value="1" Text="کاردان"></dxe:ListEditItem>
                                        <dxe:ListEditItem Value="2" Text="معمار تجربی"></dxe:ListEditItem>
                                        <dxe:ListEditItem Value="3" Text="دیگر اشخاص"></dxe:ListEditItem>
                                   
                                    </Items>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                </TSPControls:CustomAspxComboBox>
                            </div>
                            <div class="col-md-3">
                                وضعیت امتیاز
                            </div>
                            <div class="col-md-3">
                                          <TSPControls:CustomAspxComboBox runat="server"  Width="100%"
                                                 ID="cmbHasEfficientGrade" ValueType="System.String"                                              
                                                SelectedIndex="0" ClientInstanceName="cmbHasEfficientGrade" 
                                                RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    
                                                    <RequiredField IsRequired="True" ErrorText="وضعیت امتیاز عضو را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <Items>
                                                    <dxe:ListEditItem Value="false" Text="فاقد امتیاز" ></dxe:ListEditItem>
                                                    <dxe:ListEditItem Value="true" Text="دارای امتیاز" Selected="True"></dxe:ListEditItem>
                                                </Items>
                                            </TSPControls:CustomAspxComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <dxe:ASPxLabel runat="server" Text="کد عضویت *" ID="lblMeNo" ClientInstanceName="lMeNo">
                                </dxe:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" ID="txtMeNo" Width="100%" AutoPostBack="True"
                                    ClientInstanceName="MeNo" OnTextChanged="txtMeNo_TextChanged">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                        <RegularExpression ErrorText="کد را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                    <ClientSideEvents TextChanged="function (s,e){  if(cmbType.GetValue() != 0)  e.processOnServer=false;}"></ClientSideEvents>
                                </TSPControls:CustomTextBox>
                            </div>
                            <div class="col-md-3"></div>
                            <div class="col-md-3"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <dxe:ASPxLabel runat="server" Text="کد عضویت كانون كاردان ها" ClientVisible="False"
                                    ID="lblOtpCode" ClientInstanceName="lOtpCode">
                                </dxe:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" ID="txtOtpCode" ClientVisible="False"
                                    Width="100%" AutoPostBack="True" ClientInstanceName="OtpCode"
                                    Style="direction: ltr" OnTextChanged="txtOtpCode_TextChanged">
                                    <ClientSideEvents TextChanged="function(s, e) {

if(cmbType.GetValue() == 5 || cmbType.GetValue() == 6)
{  
  e.processOnServer=false;
}
}"></ClientSideEvents>
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                        <RegularExpression ErrorText="کد را صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                            <div class="col-md-3"></div>
                            <div class="col-md-3"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">نام</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" ID="txtFirstName" Width="100%"
                                    ClientEnabled="False" ClientInstanceName="FirstName">
                                    <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="نام را وارد نمایید"
                                        ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
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
                            <div class="col-md-3">نام خانوادگی</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" ID="txtLastName" Width="100%"
                                    ClientEnabled="False" ClientInstanceName="LastName">
                                    <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="نام خانوادگی را وارد نمایید"
                                        ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
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
                            <div class="col-md-3">نام پدر</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" ID="txtFatherName" Width="100%"
                                    ClientEnabled="False" ClientInstanceName="FatherName">
                                    <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="نام پدر را وارد نمایید"
                                        ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
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
                                <dxe:ASPxLabel runat="server" Text="شماره پروانه اشتغال" ID="lblFileNo" ClientInstanceName="lblFileNo">
                                </dxe:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" ID="txtFileNo" Width="100%" ClientEnabled="False"
                                    ClientInstanceName="FileNo" Style="direction: ltr">
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RegularExpression ErrorText=""></RegularExpression>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </div>
                            <div class="col-md-3">
                                <dxe:ASPxLabel runat="server" Text="تاریخ اعتبار پروانه" ID="lblFileDate" ClientInstanceName="lblFileDate">
                                </dxe:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                    Width="245px" Enabled="False" ShowPickerOnTop="True" ID="txtFileDate" PickerDirection="ToRight"
                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                            </div>
                        </div>

                                 <dxp:ASPxPanel ID="PnlbasicInfo" Width="100%" ClientInstanceName="PnlbasicInfo"
                            runat="server" ClientVisible="false">
                            <PanelCollection>
                                <dxp:PanelContent>
                        <div class="row">
                            <div class="col-md-3">تاریخ تولد</div>
                            <div class="col-md-3">
                                <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                    Width="245px" Enabled="False" ShowPickerOnTop="True" ID="txtBirthDate" PickerDirection="ToRight"
                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtBirthDate" ID="PersianDateValidator1">تاریخ نامعتبر</pdc:PersianDateValidator>
                            </div>
                            <div class="col-md-3">محل تولد</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" ID="txtBirthPlace" Width="100%"
                                    ClientEnabled="False" ClientInstanceName="BirthPlace">
                                    <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="محل تولد را وارد نمایید"
                                        ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
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
                            <div class="col-md-3">شماره شناسنامه</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" ID="txtIdNo" Width="100%" MaxLength="10"
                                    ClientEnabled="False" ClientInstanceName="IdNo">
                                    <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="شماره شناسنامه را وارد نمایید"
                                        ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
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
                            <div class="col-md-3">کد ملی</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" ID="txtSSN" Width="100%" MaxLength="10"
                                    ClientEnabled="False" ClientInstanceName="SSN">
                                    <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="کد ملی را وارد نمایید"
                                        ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
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
                            <div class="col-md-3">تلفن</div>
                            <div class="col-md-3">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td valign="top" width="80%">
                                                <TSPControls:CustomTextBox runat="server" ID="txtTel" Width="100%" MaxLength="8"
                                                    ClientEnabled="False" ClientInstanceName="Tel">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RequiredField ErrorText=""></RequiredField>
                                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{5,8}"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" width="5%">
                                                <asp:Label runat="server" Text="-" Width="13px" ID="Lbjjje71"></asp:Label>
                                            </td>
                                            <td valign="top" width="15%">
                                                <TSPControls:CustomTextBox runat="server" ID="txtTel_pre" Width="100%" MaxLength="4"
                                                    ClientEnabled="False" ClientInstanceName="Tel_pre">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
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
                            <div class="col-md-3">شماره همراه</div>
                            <div class="col-md-3">
                                <TSPControls:CustomTextBox runat="server" ID="txtMobile" Width="100%" MaxLength="11"
                                    ClientEnabled="False" ClientInstanceName="MobileNo">
                                    <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="شماره همراه را وارد نمایید"
                                        ErrorTextPosition="Bottom">
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <RequiredField ErrorText=""></RequiredField>
                                        <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="09\d{9}"></RegularExpression>
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
                            <div class="col-md-3">آدرس</div>
                            <div class="col-md-9">
                                <TSPControls:CustomASPXMemo runat="server" Height="40px" ID="txtAddress" Width="100%"
                                    ClientEnabled="False" ClientInstanceName="Address">
                                    <ValidationSettings>
                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomASPXMemo>
                            </div>
                        </div>
                                      </dxp:PanelContent>
                            </PanelCollection>
                        </dxp:ASPxPanel>
                        <dxp:ASPxPanel ID="PnlKardanMemarInfo" Width="100%" ClientInstanceName="PnlKardanMemarInfo"
                            runat="server" ClientVisible="false">
                            <PanelCollection>
                                <dxp:PanelContent>

                                    <div class="row">
                                        <div class="col-md-3">نمایندگی</div>
                                        <div class="col-md-3">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                TextField="Name" ID="CmbAgent" ClientInstanceName="CmbAgent" DataSourceID="ObjectDataSourceAgent"
                                                ValueType="System.String" ValueField="AgentId" RightToLeft="True"
                                                EnableIncrementalFiltering="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="نمایندگی را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                            <asp:ObjectDataSource ID="ObjectDataSourceAgent" runat="server" SelectMethod="GetData"
                                                TypeName="TSP.DataManager.AccountingAgentManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                                        </div>
                                        <div class="col-md-3">شهر محل اقامت</div>
                                        <div class="col-md-3">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                TextField="CitName" ID="CmbCity" DataSourceID="ObjectDataSourceCity" ValueType="System.String"
                                                ValueField="CitId" ClientInstanceName="CmbCity" RightToLeft="True"
                                                EnableIncrementalFiltering="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="شهر را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                            <asp:ObjectDataSource ID="ObjectDataSourceCity" runat="server" SelectMethod="GetData"
                                                TypeName="TSP.DataManager.CityManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <dxe:ASPxLabel runat="server" Text="رشته" ID="ASPxLabel5" ClientInstanceName="lblMjId">
                                            </dxe:ASPxLabel>
                                        </div>
                                        <div class="col-md-3">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                TextField="MjName" ID="ComboMjId" DataSourceID="OdbMajor"
                                                ValueType="System.String" ValueField="MjId" ClientInstanceName="MjId"
                                                EnableIncrementalFiltering="True" RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="True" ErrorText="رشته را انتخاب نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="FindMjParents"
                                                TypeName="TSP.DataManager.MajorManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                                        </div>
                                        <div class="col-md-3">
                                            <dxe:ASPxLabel runat="server" Text="عنوان رشته" ID="ASPxLabel6" ClientInstanceName="lblMjName">
                                            </dxe:ASPxLabel>
                                        </div>
                                        <div class="col-md-3">
                                            <TSPControls:CustomTextBox runat="server" ID="txtMjName" Width="100%" ClientInstanceName="MjName">
                                                <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="مدرک را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ClientSideEvents Validation="function(s, e) {
		
}"></ClientSideEvents>
                                            </TSPControls:CustomTextBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <dxe:ASPxLabel runat="server" Text="تصویر پروانه اشتغال" ID="ASPxLabel2">
                                            </dxe:ASPxLabel>
                                        </div>
                                        <div class="col-md-3">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxUploadControl ID="flpCommit" runat="server" ClientInstanceName="flp"
                                                                InputType="Images" UploadWhenFileChoosed="True" OnFileUploadComplete="flpCommit_FileUploadComplete"
                                                                Width="100%">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
if(e.isValid){
	imgtikcommit.SetVisible(true);
	HDfile.Set('name',1);
	lbl.SetVisible(false);
	hp.SetVisible(true);
	hp.SetNavigateUrl('../Image/Temp/'+e.callbackData);  
	}
	else{
    imgtikcommit.SetVisible(false);
	HDfile.Set('name',0);
	lbl.SetVisible(true);
	hp.SetVisible(false);
	hp.SetNavigateUrl('');
	}
}" />
                                                            </TSPControls:CustomAspxUploadControl>
                                                            <dxhf:ASPxHiddenField ID="HDFlpCommit" runat="server" ClientInstanceName="HDfile">
                                                            </dxhf:ASPxHiddenField>
                                                            <dxe:ASPxLabel runat="server" Text="تصویر پروانه اشتغال را انتخاب نمایید" ClientVisible="False"
                                                                ID="lblerr" ForeColor="Red" ClientInstanceName="lbl">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                ID="imgEndUploadImg" ClientInstanceName="imgtikcommit">
                                                            </dxe:ASPxImage>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <dxe:ASPxHyperLink runat="server" Text="آدرس تصویر پروانه اشتغال" ClientVisible="False"
                                                Target="_blank" ID="HpCommit" ClientInstanceName="hp">
                                            </dxe:ASPxHyperLink>
                                            <TSPControls:CustomAspxButton  runat="server" Text="click" CausesValidation="False" ClientVisible="False"
                                                Width="62px" ID="ASPxButton2" EnableClientSideAPI="True" AutoPostBack="False"
                                                UseSubmitBehavior="False" ClientInstanceName="filebut">
                                                <ClientSideEvents Click="function(s, e) {
flpCommit.SetVisible(false);
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        
                                        </div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-3"></div>
                                    </div>

                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxp:ASPxPanel>

                        <div class="row">
                            <div class="col-md-3">تصویر</div>
                            <div class="col-md-3">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                    ID="flp_Image" InputType="Images" ClientInstanceName="flpimg" OnFileUploadComplete="flp_Image_FileUploadComplete">
                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
	if(e.isValid){
	imgtik.SetVisible(true);
	HDimg.Set('name',1);
	lblimg.SetVisible(false);
	
img.SetImageUrl('../Image/Temp/'+e.callbackData);
}
else
{
imgtik.SetVisible(false);
	HDimg.Set('name',0);
	lblimg.SetVisible(true);
	
img.SetImageUrl('');
}
}"></ClientSideEvents>
                                                </TSPControls:CustomAspxUploadControl>
                                                <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                    ID="ASPxLabel101" ForeColor="Red" ClientInstanceName="lblimg">
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
                                <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgMember" ClientInstanceName="img">
                                    <EmptyImage Url="~/Images/person.png">
                                    </EmptyImage>
                                </dxe:ASPxImage>
                                <br />
                                <dxe:ASPxLabel runat="server" ID="lblImgWarning" ForeColor="Blue" ClientInstanceName="lblImgWarning"
                                    ClientVisible="false">
                                </dxe:ASPxLabel>
                                <TSPControls:CustomAspxButton runat="server" Text="click" CausesValidation="False" ClientVisible="False"
                                    Width="62px" ID="ASPxButton1" EnableClientSideAPI="True" AutoPostBack="False"
                                    UseSubmitBehavior="False" ClientInstanceName="ibut">
                                    <ClientSideEvents Click="function(s, e) {
flpimg.SetVisible(false);
}"></ClientSideEvents>
                                </TSPControls:CustomAspxButton>
                            </div>
                            <div class="col-md-3"></div>
                            <div class="col-md-3"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">سمت *</div>
                            <div class="col-md-3">
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                    TextField="OfpName" ID="drdPosition" DataSourceID="ODBPosition"
                                    ValueType="System.String" ValueField="OfpId" RightToLeft="True" ClientInstanceName="position">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                        <RequiredField IsRequired="True" ErrorText="سمت را انتخاب نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                </TSPControls:CustomAspxComboBox>
                            </div>
                            <div class="col-md-3">نوع همکاری</div>
                            <div class="col-md-3">
                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                    ID="ComboTime" ValueType="System.String"
                                    SelectedIndex="1" ClientInstanceName="time"
                                    EnableIncrementalFiltering="True" RightToLeft="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                        <RequiredField IsRequired="True" ErrorText="نوع همکاری را انتخاب نمایید"></RequiredField>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                    <Items>
                                        <dxe:ListEditItem Value="0" Text="پاره وقت"></dxe:ListEditItem>
                                        <dxe:ListEditItem Value="1" Text="تمام وقت" Selected="True"></dxe:ListEditItem>
                                    </Items>
                                    <ButtonStyle Width="13px">
                                    </ButtonStyle>
                                </TSPControls:CustomAspxComboBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">تاریخ شروع همکاری *</div>
                            <div class="col-md-3">
                                <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                    Width="245px" ShowPickerOnTop="True" ID="txtStartDate" PickerDirection="ToRight"
                                    RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                    ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtStartDate" ID="PersianDateValidator2">تاریخ نامعتبر</pdc:PersianDateValidator>
                            </div>
                            <div class="col-md-3">
                                <TSPControls:CustomASPxCheckBox runat="server" Text="دارای حق امضا" EnableClientSideAPI="True"
                                    Width="100%" ID="chbHaghEmza" ClientInstanceName="chb">
                                    <ClientSideEvents CheckedChanged="function(s, e) {
	//if(chb.GetChecked()==true)
//{
	//flpEmza.SetVisible(true);
	//lblE.SetVisible(true);
//}
//else
//{
//flpEmza.SetVisible(false);
//lblE.SetVisible(false);
//}
}"></ClientSideEvents>
                                </TSPControls:CustomASPxCheckBox>
                            </div>
                            <div class="col-md-3"></div>
                        </div>


                        <div class="row">
                            <div class="col-md-3">
                                <dxe:ASPxLabel runat="server" Text="تصویر امضا" ID="lbEmza" ClientInstanceName="lblE">
                                </dxe:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                    ID="flp_Emza" InputType="Images" ClientInstanceName="flpEmza" OnFileUploadComplete="flp_Emza_FileUploadComplete">
                                                    <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                                    if(e.isValid){
	imgEmzatik.SetVisible(true);
    btnRemoveFile.SetVisible(true);
	HDEmza.Set('name',1);
	lblEmza.SetVisible(false);
	imgEmza.SetVisible(true);
	imgEmza.SetImageUrl('../Image/Temp/'+e.callbackData);
	}
	else
	{
	imgEmzatik.SetVisible(false);
    btnRemoveFile.SetVisible(false);
	HDEmza.Set('name',0);
	lblEmza.SetVisible(true);
	imgEmza.SetVisible(false);
	imgEmza.SetImageUrl('');
	}
}"></ClientSideEvents>
                                                </TSPControls:CustomAspxUploadControl>
                                                <dxe:ASPxLabel runat="server" Text="تصویر امضا را انتخاب نمایید" ClientVisible="False"
                                                    ID="ASPxLabel166" ForeColor="Red" ClientInstanceName="lblEmza">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnRemoveFile" runat="server" CausesValidation="False"
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnRemoveFile_Click" Text=" "
                                                    ToolTip="حذف فایل" UseSubmitBehavior="False" ClientInstanceName="btnRemoveFile"
                                                    ClientVisible="false">
                                                    <Image Height="16px" Url="~/Images/icons/DeleteFile.png" Width="16px" />
                                                    <ClientSideEvents Click="function(s,e){	 e.processOnServer= confirm('آیا مطمئن به حذف فایل هستید؟');}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                    ID="imgEndUploadImg123" ClientInstanceName="imgEmzatik">
                                                </dxe:ASPxImage>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <dxe:ASPxImage runat="server" Height="75px" ClientVisible="False" Width="75px" ID="imgEmza"
                                    ClientInstanceName="imgEmza">
                                </dxe:ASPxImage>
                                <TSPControls:CustomAspxButton runat="server" Text="click" CausesValidation="False" ClientVisible="False"
                                    Width="62px" ID="ASPxButton3" EnableClientSideAPI="True" AutoPostBack="False"
                                    UseSubmitBehavior="False" ClientInstanceName="but">
                                    <ClientSideEvents Click="function(s, e) {
//flpEmza.SetVisible(false);
}"></ClientSideEvents>
                                </TSPControls:CustomAspxButton>
                            </div>
                            <div class="col-md-3"></div>
                            <div class="col-md-3"></div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">توضیحات</div>
                            <div class="col-md-9">
                                <TSPControls:CustomASPXMemo runat="server" Height="45px" ID="txtDesc" Width="100%"
                                    ClientInstanceName="Desc">
                                    <ValidationSettings>
                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomASPXMemo>
                            </div>
                        </div>
                        <dxe:ASPxPanel ID="ASPxRoundPanelGrade" ClientInstanceName="rdpGrade" HeaderText=""
                            runat="server">
                            <PanelCollection>
                                <dx:PanelContent>


                                    <fieldset>
                                        <legend class="HelpUL">پایه-صلاحیت های پروانه اشتغال</legend>
                                        <TSPControls:CustomAspxDevGridView2 runat="server" ID="GridViewResponsblity"
                                            KeyFieldName="Id" Width="100%" ClientInstanceName="grid" OnCustomCallback="GridViewResponsblity_CustomCallback">
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GrdName" Caption="پایه">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="ResName" Caption="صلاحیت">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="MjName" Caption="رشته">
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                        </TSPControls:CustomAspxDevGridView2>
                                    </fieldset>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dxe:ASPxPanel>

                        <br />
                        <div class="Item-center">
                            <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="&nbsp;&nbsp;اضافه به لیست"
                                ID="btnAdd" UseSubmitBehavior="False" Wrap="False" ClientInstanceName="btn"
                                OnClick="btnAdd_Click">
                                <ClientSideEvents Click="function(s, e) {
var flag=0;

if(cmbType.GetValue()==&quot;3&quot;)
{
if(HDimg.Get('name')!=1)
{
lblimg.SetVisible(true);
e.processOnServer=false;
flag=1;
}
}
else 
 lblimg.SetVisible(false);
if (chb.GetChecked()== true)
{
if(HDEmza.Get('name')!=1)
{
lblEmza.SetVisible(true);
e.processOnServer=false;
flag=1;
}
}

}"></ClientSideEvents>
                            </TSPControls:CustomAspxButton>
                            <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="&nbsp;&nbsp;&nbsp;پاک کردن فرم"
                                CausesValidation="False" AutoPostBack="False" ID="btnRefresh" UseSubmitBehavior="False">
                                <ClientSideEvents Click="function(s, e) {
 e.processOnServer= false;
 if(confirm('آیا مطمئن به پاک کردن اطلاعات فرم هستید؟'))
 {
 cmbType.SetSelectedIndex(0);
	ClearForm();
  SetMember();
	}
}"></ClientSideEvents>
                            </TSPControls:CustomAspxButton>
                        </div>
                        <br />
                        <TSPControls:CustomAspxDevGridView2 ID="GvMembers" runat="server" Width="100%"
                            ClientInstanceName="gridMember" OnRowDeleting="GvMembers_RowDeleting"
                            KeyFieldName="ID" AutoGenerateColumns="False" EnableCallBacks="False">
                            <SettingsText Title="لیست اعضای شرکت" />
                            <ClientSideEvents RowDblClick="function(s, e) {
	//SetGridValues();
}"
                                SelectionChanged="function(s, e) {
    
btn.SetEnabled(false);
	SetGridValues();
}"></ClientSideEvents>
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="ID" Name="ID">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="OfmTypeId"
                                    Name="OfmTypeId">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="1" FieldName="OthType"
                                    Name="OthType">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" "  Width="25px" ShowClearFilterButton="true" ShowSelectButton="true">

                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" "  Width="25px" ShowDeleteButton="true">
                               
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FirstName" Caption="نام"
                                    Name="FirstName">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="LastName" Caption="نام خانوادگی"
                                    Name="LastName">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="IdNo" Caption="شماره شناسنامه"
                                    Name="IdNo">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="SSN" Caption="کد ملی"
                                    Name="SSN">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="BirthDate"
                                    Caption="تاریخ تولد" Name="BirthDate">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="OfPosition" Caption="سمت"
                                    Name="OfPosition">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="MobileNo"
                                    Caption="شماره همراه" Name="MobileNo">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="FatherName" Caption="نام پدر"
                                    Name="FatherName">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="StartDate" Caption="شروع همکاری"
                                    Name="StartDate">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="10" FieldName="BirthPlace"
                                    Name="BirthPlace">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="10" FieldName="OfpId"
                                    Name="OfpId">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="9" FieldName="HasSignRight"
                                    Name="HasSignRight">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="9" FieldName="ImageUrl"
                                    Name="ImageUrl">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="9" FieldName="SignUrl"
                                    Name="SignUrl">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="10" FieldName="FileNo"
                                    Name="FileNo">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="10" FieldName="IsFullTime"
                                    Name="IsFullTime">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="10" FieldName="Description"
                                    Name="Description">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="10" FieldName="Tel" Name="Tel">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="10" FieldName="Address"
                                    Name="Address">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="9" FieldName="OtpCode"
                                    Name="OtpCode">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="OfmTypeName" Caption="نوع"
                                    Name="OfmTypeName">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="8" FieldName="MeId" Name="MeId">
                                </dxwgv:GridViewDataTextColumn>
                                <%-- <dxwgv:GridViewCommandColumn VisibleIndex="7" Caption=" " Width="30px">
                                            <DeleteButton Visible="True">
                                            </DeleteButton>
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewCommandColumn VisibleIndex="6" Caption=" " Width="30px">
                                            <ClearFilterButton Visible="True">
                                            </ClearFilterButton>
                                            <SelectButton Visible="True" Text="مشاهده">
                                            </SelectButton>
                                        </dxwgv:GridViewCommandColumn>--%>
                                <dxwgv:GridViewDataTextColumn FieldName="Tel_pre" Visible="False" VisibleIndex="8">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </TSPControls:CustomAspxDevGridView2>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <div class="Item-center">

                <TSPControls:CustomAspxButton CssClass="ButtonMenue"  ID="btnPre" OnClick="btnPre_Click" runat="server" Text="&nbsp;&nbsp;&nbsp;بازگشت&nbsp;&nbsp;&nbsp;" UseSubmitBehavior="False"
                    EnableTheming="False" EnableViewState="False" CausesValidation="False" ToolTip="بازگشت">
                </TSPControls:CustomAspxButton>
                <TSPControls:CustomAspxButton CssClass="ButtonMenue"  ID="btnNext" OnClick="btnNext_Click" runat="server" Text="تایید و ادامه" UseSubmitBehavior="False"
                    EnableTheming="False" EnableViewState="False" CausesValidation="False" ToolTip="تایید و ادامه">

                    <ClientSideEvents Click="function(s, e) {
	if(gridMember.GetVisibleRowsOnPage()==0)
	{
	   alert(&quot;ثبت اعضای شرکت الزامی می باشد&quot;)
	   e.processOnServer=false;
	}
}" />
                </TSPControls:CustomAspxButton>
            </div>
            <asp:ObjectDataSource ID="ODBPosition" runat="server" FilterExpression="OfType={0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.OfficePositionManager" OldValuesParameterFormatString="original_{0}">
                <FilterParameters>
                    <asp:Parameter Name="newparameter"></asp:Parameter>
                </FilterParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbMajor" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
            <asp:HiddenField ID="HDOtpId" runat="server" Visible="False"></asp:HiddenField>
            <dxhf:ASPxHiddenField ID="HD_img" runat="server" ClientInstanceName="HDimg">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HD_Emza" runat="server" ClientInstanceName="HDEmza">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HiddenHelp" runat="server" ClientInstanceName="HiddenHelp">
            </dxhf:ASPxHiddenField>
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
</asp:Content>
