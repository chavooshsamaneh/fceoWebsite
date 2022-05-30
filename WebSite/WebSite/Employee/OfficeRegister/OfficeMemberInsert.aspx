<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeMemberInsert.aspx.cs" Inherits="Employee_OfficeRegister_OfficeMemberInsert"
    Title="اعضای شرکت" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
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
<%@ Register Src="~/UserControl/OfficeInfoUserControl.ascx" TagName="OfficeInfoUserControl"
    TagPrefix="UserControlOfficeInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Content" runat="server" style="width: 100%; display: block;" align="center">
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div dir="rtl" id="DivReport" class="DivErrors" align="right" runat="server" visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#"><span style="color: #000000">بستن</span></a>]<br />
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelMenu1" runat="server" >
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table >
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="BtnNew_Click">
                                              
                                                <Image   Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                CausesValidation="False"  ID="btnEdit" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                              
                                                <Image   Url="~/Images/icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                ClientInstanceName="save" OnClick="btnSave_Click">
                                                <ClientSideEvents Click="function(s, e) {

var flag=0;
//if(cmbType.GetValue()==&quot;3&quot;)
//{
//if(HDimg.Get('name')!=1)
//{
//lblimg.SetVisible(true);
//e.processOnServer=false;
//flag=1;
//}
//}
//else
// lblimg.SetVisible(false);
if (flpEmza.GetVisible() && chb.GetChecked()== true)
{
if(HDEmza.Get('name')!=1)
{
lblEmza.SetVisible(true);
e.processOnServer=false;
flag=1;
}
}
else
 lblEmza.SetVisible(false);
}"></ClientSideEvents>
                                                
                                                <Image   Url="~/Images/icons/save.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBack_Click">
                                             
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
                <UserControlOfficeInfo:OfficeInfoUserControl ID="OfficeInfoUserControl" runat="server" />
                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelOfficeMember" HeaderText="مشاهده"
                    runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table dir="rtl" id="Table2" width="100%">
                                <tbody>
                                    <tr>
                                        <td colspan="4">
                                            <ul class="HelpUL">
                                                <li>ثبت مدیر عامل شرکت الزامی می باشد </li>
                                                <li>درصورتی که درخواست ثبت شده شما "درخواست تغییر اطلاعات پایه" باشد با توجه به این
                                                    که در این نوع درخواست تاییدیه مسکن وجود ندارد، و همچنین با توجه به این که اطلاعات
                                                    اعضای شرکت درگواهینامه چاپ شده و تغییر آن نیاز به چاپ مجدد و در نتیجه تایید مسکن
                                                    دارد، امکان تغییر اطلاعات اعضای شرکت وجود ندارد. </li>
                                                <li>درصورتی که قصد تغییر اطلاعات اعضای شرکت را دارید از درخواست تغییر اطلاعات پایه استفاده
                                                    ننمایید. </li>
                                                <li>بر اساس ماده 6 بند 6-1-1 و ماده 9 بند 9-1-1 کتاب مبحث دوم و طبق قانون تجارت ذکر
                                                    شده توسط اداره ثبت شرکت ها و مالکیت صنعتی ، مجموع تعداد سهامداران و اعضای هیئت مدیره
                                                    شرکت های سهامی خاص بایستی حداقل سه نفر باشد. </li>
                                                <li>بر اساس ماده 6 بند 6-1-1 و ماده 9 بند 9-1-1 کتاب مبحث دوم و طبق قانون تجارت ذکر
                                                    شده توسط اداره ثبت شرکت ها و مالکیت صنعتی ، مجموع تعداد سهامداران و اعضای هیئت مدیره
                                                    شرکت های مسئولیت محدود بایستی حداقل دو نفر باشد. </li>
                                                <li>طبق ماده 6 بند 6-1-4 کتاب مبحث دوم حداقل دو نفر از اعضای شرکت طراح و ناظر باید دارای
                                                    پروانه اشتغال به کار باشند </li>
                                                <li>براساس ماده 6 بند 6-5-1 بایستی مدیرعامل شرکت طراح و ناظر دارای پروانه اشتغال به
                                                    کار طراحی باشد و بنابراین بایستی عضو نظام مهندسی باشد. </li>
                                                <li>طبق ماده 9 بند 9-1-4 کتاب مبحث دوم حداقل دو نفر از اعضای شرکت اجرایی باید دارای
                                                    پروانه اشتغال به کار باشند </li>
                                            </ul>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" width="20%">
                                            <dxe:ASPxLabel runat="server" Text="نوع عضو" ID="ASPxLabel1" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td dir="ltr" valign="top" align="right" width="30%">
                                            <TSPControls:CustomAspxComboBox runat="server"  Width="100%" 
                                                 ID="ComboType" ValueType="System.String"
                                               AutoPostBack="true" OnSelectedIndexChanged="ComboType_IndexChenge"
                                                SelectedIndex="0" ClientInstanceName="cmbType" 
                                                RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
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
                                        </td>
                                        <td valign="top" align="right" width="15%">
                                        وضعیت امتیاز
                                        </td>
                                        <td valign="top" align="right" width="35%">
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
                                                    <dxe:ListEditItem Value="0" Text="فاقد امتیاز" ></dxe:ListEditItem>
                                                    <dxe:ListEditItem Value="1" Text="دارای امتیاز" Selected="True"></dxe:ListEditItem>
                                                </Items>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد عضویت *" ID="lblMeNo" ClientInstanceName="lMeNo"
                                                Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtMeNo"  Width="100%" AutoPostBack="True"
                                                ClientInstanceName="MeNo"  OnTextChanged="txtMeNo_TextChanged">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                   
                                                    <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="کد را صحیح وارد نمایید" ValidationExpression="\d*">
                                                    </RegularExpression>
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
                                            <dxe:ASPxLabel runat="server" Text="كد عضويت كانون كاردان ها" ClientVisible="False"
                                                ID="lblOtpCode" ClientInstanceName="lOtpCode" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtOtpCode"  ClientVisible="False"
                                                Width="100%" AutoPostBack="True" ClientInstanceName="OtpCode" 
                                                OnTextChanged="txtOtpCode_TextChanged">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                   
                                                    <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="کد را صحیح وارد نمایید" ValidationExpression="\d*">
                                                    </RegularExpression>
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
                                            <dxe:ASPxLabel ID="lblFileNo" Width="100%" runat="server" ClientInstanceName="lblFileNo"
                                                Text="شماره پروانه اشتغال">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtFileNo"  Width="100%" ClientEnabled="False"
                                                ClientInstanceName="FileNo" AutoPostBack="True" OnTextChanged="txtFileNo_TextChanged"
                                                 Style="direction: ltr">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    
                                                    <RequiredField IsRequired="True" ErrorText="شماره پروانه را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="شماره را با فرمت *****-***-**-* وارد نمایید" ValidationExpression="^\d{1}-\d{2}-\d{3}-\d{3,5}">
                                                    </RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel ID="lblFileDate" Width="100%" runat="server" ClientInstanceName="lblFileDate"
                                                Text="تاریخ اعتبار پروانه">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                Width="220px" Enabled="False" ShowPickerOnTop="True" ID="txtFileNoDate" PickerDirection="ToRight"
                                                RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                            <%--                                                <TSPControls:CustomTextBox runat="server" ID="txtFileNoDate"  Width="100%"
                                                    ClientEnabled="False" ClientInstanceName="FileNoDate" 
                                                    Style="direction: ltr">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                        </ErrorImage>
                                                        <RegularExpression ErrorText=""></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="نام" ID="Label4"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtFirstName"  Width="100%"
                                                ClientEnabled="False" ClientInstanceName="FirstName" >
                                                <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="نام را وارد نمایید"
                                                    ErrorTextPosition="Bottom">
                                                 
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
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="نام خانوادگی" ID="Label5" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtLastName"  Width="100%"
                                                ClientEnabled="False" ClientInstanceName="LastName" >
                                                <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="نام خانوادگی را وارد نمایید"
                                                    ErrorTextPosition="Bottom">
                                                
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="نام پدر" ID="Label11" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtFatherName"  Width="100%"
                                                ClientEnabled="False" ClientInstanceName="FatherName" >
                                               <%-- <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="نام پدر را وارد نمایید"
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
}"></ClientSideEvents>--%>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                        </td>
                                        <td valign="top" align="right">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="تاریخ تولد" ID="Label12" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                Width="220px" Enabled="False" ShowPickerOnTop="True" ID="txtBirthDate" PickerDirection="ToRight"
                                                RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>                                          
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="محل تولد" ID="Label13"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtBirthPlace"  Width="100%"
                                                ClientEnabled="False" ClientInstanceName="BirthPlace" >
                                               <%-- <ValidationSettings Display="Dynamic" EnableCustomValidation="True"
                                                    ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
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
}"></ClientSideEvents>--%>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره شناسنامه" ID="Label14"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtIdNo"  Width="100%" MaxLength="10"
                                                ClientEnabled="False" ClientInstanceName="IdNo" >
                                                <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorText="شماره شناسنامه را وارد نمایید"
                                                    ErrorTextPosition="Bottom">
                                                  
                                                    <RequiredField ErrorText=""></RequiredField>
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,10}">
                                                    </RegularExpression>
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
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="کد ملی" ID="Label15" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtSSN"  Width="100%" MaxLength="10"
                                                ClientEnabled="False" ClientInstanceName="SSN" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom"  ErrorText="کد ملی را وارد نمایید">
                                                 
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="تلفن" ID="Lhe69"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <%--                                            <TSPControls:CustomAspxCallbackPanel runat="server" Width="100%" HideContentOnCallback="False"
                                                    ShowLoadingPanel="False" ID="CallbackMeType" ClientInstanceName="CallbackMeType"
                                                    OnCallback="CallbackMeType_Callback1">
                                                    <PanelCollection>
                                                        <dxp:PanelContent runat="server">--%>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="vertical-align: top" width="75%">
                                                            <TSPControls:CustomTextBox runat="server" ID="txtTel"  Width="100%" MaxLength="8"
                                                                ClientEnabled="False" ClientInstanceName="Tel" >
                                                                
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                        <td style="vertical-align: top" width="5%">
                                                            <asp:Label runat="server" Text="-" Width="100%" ID="Lbjjje71"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: top" width="20%">
                                                            <TSPControls:CustomTextBox runat="server" ID="txtTel_pre"  Width="100%" MaxLength="4"
                                                                ClientEnabled="False" ClientInstanceName="Tel_pre" >
                                                            
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <%--   </dxp:PanelContent>
                                                    </PanelCollection>
                                                    <ClientSideEvents EndCallback="function(s, e) {
	gridRes.PerformCallback('');
}" />
                                                </TSPControls:CustomAspxCallbackPanel>--%>
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="شماره همراه" ID="Lal75"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtMobile"  Width="100%" MaxLength="11"
                                                ClientEnabled="False" ClientInstanceName="MobileNo" >
                                                <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorText="شماره همراه را وارد نمایید">
                                                 
                                                    <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{10}">
                                                    </RegularExpression>
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="آدرس" Width="100%" ID="Lbه76"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtAddress"  Width="100%"
                                                ClientEnabled="False" ClientInstanceName="Address" >
                                                <ValidationSettings>
                                                 
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="تصویر" ID="Label17" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
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
    img.SetImageUrl('../../Image/Temp/'+e.callbackData);
}
	else{
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
                                                <EmptyImage Url="~/Images/Person.png">
                                                </EmptyImage>
                                            </dxe:ASPxImage>
                                            <br />
                                            <dxe:ASPxLabel runat="server" ID="lblImg" ForeColor="Blue">
                                            </dxe:ASPxLabel>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="click" CausesValidation="False" ClientVisible="False"
                                                Width="62px" ID="ASPxButton1" EnableClientSideAPI="True" AutoPostBack="False"
                                                UseSubmitBehavior="False" ClientInstanceName="ibut">
                                                <ClientSideEvents Click="function(s, e) {
flpimg.SetVisible(false);
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="رشته" ClientVisible="False" Width="100%" ID="ASPxLabel5"
                                                ClientInstanceName="lblMjId">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" ClientVisible="False"  Width="100%"
                                                  TextField="MjName"
                                                ID="ComboMjId" DataSourceID="OdbMajor" ValueType="System.String" ValueField="MjId"
                                                ClientInstanceName="MjId"  RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Width="100%" Text="مدرک" ClientVisible="False" ID="ASPxLabel6"
                                                ClientInstanceName="lblMjName">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtMjName"  ClientVisible="False"
                                                Width="100%" ClientInstanceName="MjName" >
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="سمت *" ID="Label32"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server"  Width="100%" 
                                                 TextField="OfpName" ID="drdPosition" DataSourceID="ODBPosition"
                                                ValueType="System.String" ValueField="OfpId" ClientInstanceName="position" 
                                                EnableIncrementalFiltering="True" RightToLeft="True">
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
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="نوع همکاری" Width="100%" ID="Label53"></asp:Label>
                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server"  Width="100%"  
                                                 ID="ComboTime" ValueType="System.String"
                                                SelectedIndex="1" ClientInstanceName="time" RightToLeft="True" >
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
                                        </td>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="تاریخ شروع همکاری" Width="100%" ID="Label42"></asp:Label>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                Width="220px" ShowPickerOnTop="True" ID="txtStartDate" PickerDirection="ToRight"
                                                RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
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
                                        </td>
                                        <td valign="top" align="right">
                                        </td>
                                        <td valign="top" align="right">
                                        </td>
                                        <td valign="top" align="right">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تصویر امضا" ID="lbEmza" ClientInstanceName="lblE">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                ID="flp_Emza" InputType="Images" ClientInstanceName="flpEmza" OnFileUploadComplete="flp_Emza_FileUploadComplete">
                                                                <ClientSideEvents FileUploadComplete="function(s, e) {
                                                            if(e.isValid){
	imgEmzatik.SetVisible(true);
	HDEmza.Set('name',1);
	lblEmza.SetVisible(false);
	imgEmza.SetVisible(true);
	imgEmza.SetImageUrl('../../Image/Temp/'+e.callbackData);
	}
	else
	{
	imgEmzatik.SetVisible(false);
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
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="click" CausesValidation="False" ClientVisible="False"
                                                Width="62px" ID="ASPxButton3" EnableClientSideAPI="True" AutoPostBack="False"
                                                UseSubmitBehavior="False" ClientInstanceName="but">
                                                <ClientSideEvents Click="function(s, e) {
flpEmza.SetVisible(false);
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="توضیحات" ID="Label45" Width="100%"></asp:Label>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtDesc"  Width="100%"
                                                ClientInstanceName="Desc" >
                                                <ValidationSettings>
                                                   
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanelGrade" HeaderText="صلاحیت ها"
                    runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <br />
                            <div dir="rtl">
                                <TSPControls:CustomAspxDevGridView2 Width="100%" runat="server" 
                                    ID="GridViewGrade" AutoGenerateColumns="False" 
                                    ClientInstanceName="gridRes">
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GrdName" Caption="پایه">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="ResName" Caption="صلاحیت">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MjName" Caption="رشته">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                </TSPControls:CustomAspxDevGridView2>
                            </div>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table  >
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="BtnNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image   Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                CausesValidation="False"  ID="btnEdit2" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image   Url="~/Images/icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                ID="btnSave1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                ClientInstanceName="save1" OnClick="btnSave_Click">
                                                <ClientSideEvents Click="function(s, e) {
		var flag=0;
//if(cmbType.GetValue()==&quot;3&quot;)
//{
//if(HDimg.Get('name')!=1)
//{
//lblimg.SetVisible(true);
//e.processOnServer=false;
//flag=1;
//}
//}
//else
// lblimg.SetVisible(false);
if (chb.GetChecked()== true)
{
if(flpEmza.GetVisible() && HDEmza.Get('name')!=1)
{
lblEmza.SetVisible(true);
e.processOnServer=false;
flag=1;
}
}
else
 lblEmza.SetVisible(false);
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image   Url="~/Images/icons/save.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBack_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image   Url="~/Images/icons/Back.png">
                                                </Image>
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
                <dxhf:ASPxHiddenField ID="HD_img" runat="server" ClientInstanceName="HDimg">
                </dxhf:ASPxHiddenField>
                <dxhf:ASPxHiddenField ID="HD_Emza" runat="server" ClientInstanceName="HDEmza">
                </dxhf:ASPxHiddenField>
                <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False" />
                <asp:HiddenField ID="HDMode" runat="server" Visible="False" />
                <asp:HiddenField ID="HDOtpId" runat="server" Visible="False" />
                <dxhf:ASPxHiddenField ID="HiddenFieldOffice" runat="server">
                </dxhf:ASPxHiddenField>
                <asp:ObjectDataSource ID="ODBPosition" runat="server" DeleteMethod="Delete" FilterExpression="OfType={0}"
                    InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
                    TypeName="TSP.DataManager.OfficePositionManager" UpdateMethod="Update">
                    <FilterParameters>
                        <asp:Parameter Name="newparameter" />
                    </FilterParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="OdbMajor" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.MajorManager">
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
            DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img align="middle" src="../../Image/indicator.gif" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
    </div>
</asp:Content>
