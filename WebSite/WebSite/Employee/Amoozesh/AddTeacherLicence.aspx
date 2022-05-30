<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="AddTeacherLicence.aspx.cs" Inherits="Employee_Amoozesh_AddTeacherLicence"
    Title="مشخصات مدرک تحصیلی استاد" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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

    <script type="text/javascript" language="javascript">

function SetControlValues()
{
  grid.GetRowValues(grid.GetFocusedRowIndex(),'CityName;Avg;LiId;MajorId;UniId;NumUnit;Description;StartDate;EndDate',SetValue);
}
function ClearFormValue()
{
txtAvg.SetText('');
txtNumUnit.SetText('');
txtDescription.SetText('');


document.getElementById('<%=txtStartDate.ClientID%>').value="";
document.getElementById('<%=txtEndDate.ClientID%>').value="";

cmbCity.SetSelectedIndex(0);
CmbLicence.SetSelectedIndex(0);
CmbMajor.SetSelectedIndex(0);
CmbUniversity.SetSelectedIndex(0);

}
function EnabledControl()
{
txtAvg.SetEnabled(true);
txtNumUnit.SetEnabled(true);
txtDescription.SetEnabled(true);


document.getElementById('<#%DivStartDate%>').disabled=false;
document.getElementById('<#%DivEndDate%>').disabled=false;

cmbCity.SetEnabled(true);
CmbLicence.SetEnabled(true);
CmbMajor.SetEnabled(true);
CmbUniversity.SetEnabled(true);
HiddenFieldTecherLicence.Set("",HiddenFieldTecherLicence.Get("New"));
}

    </script>

        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true"
                    dir="rtl">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                    <table >
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                        CausesValidation="False" ID="BtnNew" AutoPostBack="False" EnableViewState="False"
                                                        EnableTheming="False" UseSubmitBehavior="False" OnClick="BtnNew_Click">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/new.png"></Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                        CausesValidation="False" Width="25px" ID="btnEdit" AutoPostBack="False" EnableViewState="False"
                                                        EnableTheming="False" OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/edit.png"></Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                        Width="25px" ID="btnSave" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                                        OnClick="btnSave_Click" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/save.png"></Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                        CausesValidation="False" ID="btnBack" EnableViewState="False" EnableTheming="False"
                                                        OnClick="btnBack_Click" UseSubmitBehavior="False">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/Back.png"></Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
              
                                <TSPControls:CustomAspxMenuHorizontal ID="MenuTeacherInfo" Visible="false" runat="server" 
                                   Enabled="False" 
                                    OnItemClick="MenuTeacherInfo_ItemClick" 
                                    SeparatorHeight="100%">
                                    <Items>
                                        <dxm:MenuItem Text="مشخصات فردی" Name="BasicInfo">
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Text="سوابق آموزشی" Name="Job">
                                        </dxm:MenuItem>
                                        <dxm:MenuItem Text="تالیفات و تحقیقات" Name="Research">
                                        </dxm:MenuItem>
                                    </Items>
                                </TSPControls:CustomAspxMenuHorizontal>
                    <br />
                	<TSPControls:CustomASPxRoundPanel ID="RoundPanelTeacherLicence" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

                                <table dir="rtl" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top;" align="right">
                                                <asp:Label runat="server" Text=" مدرک تحصیلی"  ID="Label37"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;"  align="right">
                                                <TSPControls:CustomAspxComboBox runat="server"   TextField="LiName" ID="CmbLicence" 
                                                    DataSourceID="ODBLicence" ValueType="System.String" ValueField="LiId" ClientInstanceName="CmbLicence"
                                                   >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="مدرک تحصیلی را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td style="vertical-align: top;" align="right">
                                                <asp:Label runat="server" Text="رشته" ID="Label38"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;"  align="right">
                                                <TSPControls:CustomAspxComboBox runat="server"  
                                                  TextField="MjName" ID="CmbMajor" 
                                                    DataSourceID="ODBMajor" ValueType="System.String" ValueField="MjId" ClientInstanceName="CmbMajor"
                                                  >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="رشته را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;" align="right">
                                                <asp:Label runat="server" Text="دانشگاه" ID="Label40"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;"  align="right">
                                                <TSPControls:CustomAspxComboBox runat="server" EnableClientSideAPI="True"  
                                                 TextField="UnName" ID="CmbUniversity" 
                                                    DataSourceID="ODBUniversity" ValueType="System.String" ValueField="UnId" ClientInstanceName="CmbUniversity"
                                                    AutoPostBack="True" OnSelectedIndexChanged="CmbUniversity_SelectedIndexChanged">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="دانشگاه را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                            <td style="vertical-align: top;" align="right">
                                                <asp:Label runat="server" Text="شهر *" Width="40px" ID="Label42"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;"  align="right">
                                                <TSPControls:CustomAspxComboBox runat="server" 
                                                     TextField="CitName" ID="cmbCity"
                                                    DataSourceID="ObjdsCity" ValueType="System.String" ValueField="CitId" ClientInstanceName="cmbCity"
                                                   >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>
                                                        <RequiredField IsRequired="True" ErrorText="شهر را انتخاب نمایید"></RequiredField>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomAspxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;" align="right">
                                                <asp:Label runat="server" Text="تاریخ شروع *" ID="Label1"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;" align="right">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="167px" ShowPickerOnTop="True"
                                                    ID="txtStartDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" style="direction:ltr;" RightToLeft="False"></pdc:PersianDateTextBox>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtStartDate" ID="RequiredFieldValidator8">تاریخ شروع را وارد نمایید</asp:RequiredFieldValidator>
                                            </td>
                                            <td style="vertical-align: top;" align="right">
                                                <asp:Label runat="server" Text="تاریخ پایان *" Width="99px" ID="Label44"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;" align="right">
                                                <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="169px" ShowPickerOnTop="True"
                                                    ID="txtEndDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" style="direction:ltr;" RightToLeft="False"></pdc:PersianDateTextBox>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEndDate" ID="RequiredFieldValidator1">تاریخ فارغ التحصیلی را وارد نمایید</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;"  align="right">
                                                <asp:Label runat="server" Text="تعداد واحد" ID="Label2"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="80px" MaxLength="3" ID="txtNumUnit"
                                                    ClientInstanceName="txtNumUnit" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>
                                                        <RegularExpression ErrorText="تعداد واحد صحیح نیست" ValidationExpression="\d{2,3}"></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td style="vertical-align: top;" align="right">
                                                <asp:Label runat="server" Text="معدل" ID="Label3"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;" align="right">
                                                <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="80px" MaxLength="5" ID="txtAvg"
                                                    ClientInstanceName="txtAvg" >
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                        <RegularExpression ErrorText="معدل را با 2 رقم اعشار وارد نمایید" ValidationExpression="\d\d\.\d\d">
                                                        </RegularExpression>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;" align="right">
                                                <asp:Label runat="server" Text="توضیحات" ID="Label41"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;" colspan="3" align="right">
                                                <TSPControls:CustomASPXMemo runat="server" Height="37px"  Width="560px" ID="txtDescription"
                                                    ClientInstanceName="txtDescription" >
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;" rowspan="3" align="right" >
                                                <asp:Label runat="server" Text="فایل" Width="66px" ID="Label6"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;" colspan="3" align="right">
                                                <dxe:ASPxHyperLink runat="server" Text="ASPxHyperLink" Target="_blank" ID="linkAttachment">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;" colspan="3" align="right">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="انتخاب فایل"  CausesValidation="False"
                                                    Width="116px" ID="btnAttachTechearLicenceFile" AutoPostBack="False" UseSubmitBehavior="False"
                                                    >
                                                    <ClientSideEvents CheckedChanged="function(s, e) {
}" Click="function(s, e) {
	ppcChooseImageClient.Show();
}"></ClientSideEvents>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;" colspan="3" align="right">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="حذف فایل" Height="25px"  Width="113px"
                                                    ID="btnDeleteAttachment" UseSubmitBehavior="False" 
                                                    OnClick="btnDeleteAttachment_Click" CausesValidation="False">
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                           
        </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                    <br />
                	<%--<TSPControls:CustomASPxRoundPanel ID="RoundPanelJudge" HeaderText="نظر کارشناسی" runat="server"
        Width="100%" Visible="False">
        <PanelCollection>
            <dxp:PanelContent>

                   
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td style="vertical-align: top;" align="right">
                                                        <asp:Label runat="server" Text="شماره جلسه" Width="78px" ID="Label9"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="68px" ID="txtMeeting" >
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top;" align="right">
                                                        <asp:Label runat="server" Text="نظر کارشناسی" Width="90px" ID="Label8"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: top" align="right">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="38px"  Width="484px" ID="txtJudgeView"
                                                            >
                                                            <ValidationSettings>
                                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" dir="rtl" style="vertical-align: top" align="right">
                                                        <dxe:ASPxRadioButtonList runat="server" Width="172px" ID="rbtnGrade">
                                                            <Items>
                                                                <dxe:ListEditItem Value="0" Text="مورد تایید می باشد"></dxe:ListEditItem>
                                                                <dxe:ListEditItem Value="1" Text="مورد تایید نمی باشد"></dxe:ListEditItem>
                                                            </Items>
                                                            <Border BorderStyle="None" BorderWidth="0px" />
                                                            <BorderRight BorderStyle="None" BorderWidth="0px" />
                                                            <BorderLeft BorderStyle="None" BorderWidth="0px" />
                                                            <BorderTop BorderStyle="None" BorderWidth="0px" />
                                                            <BorderBottom BorderStyle="None" BorderWidth="0px" />
                                                        </dxe:ASPxRadioButtonList>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                
                                </dxp:PanelContent>      
        </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>--%>
           
                <TSPControls:CustomASPxPopupControl ID="ppcChooseImage" runat="server" Width="404px" 
                      HeaderText="انتخاب فایل"
                    ClientInstanceName="ppcChooseImageClient"
                    EnableClientSideAPI="True" EnableViewState="False"  Height="77px">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <div align="center" dir="rtl">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top;" align="right">
                                                <asp:Label runat="server" Text="فایل" Width="26px" ID="Label4"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;" align="right">
                                                <TSPControls:CustomAspxUploadControl runat="server" Size="35" ShowProgressPanel="True"
                                                    MaxSizeForUploadFile="0" ID="flp" InputType="Files" ClientInstanceName="uploader"
                                                    OnFileUploadComplete="UploaderImage_OnUploadComplete">
                                                    <ClientSideEvents  FileUploadComplete="function(s, e) {  
if(e.isValid){
//imgEndUploadImgClient.SetVisible(true);
ppcChooseImageClient.Hide();
}
}"></ClientSideEvents>
                                                   
                                                </TSPControls:CustomAspxUploadControl>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;" align="right">
                                                <asp:Label runat="server" Text="توضیحات" Width="28px" ID="Label5"></asp:Label>
                                            </td>
                                            <td style="vertical-align: top;" align="right">
                                                <TSPControls:CustomASPXMemo runat="server" Height="37px"  Width="310px" ID="ASPxMemo1"
                                                    ClientInstanceName="txtAttachDescrip" >
                                                </TSPControls:CustomASPXMemo>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2"  style="vertical-align: top" align="center">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="ذخیره"  CausesValidation="False"
                                                    Width="122px" ID="btnImageUpload" AutoPostBack="False" UseSubmitBehavior="False"
                                                    ClientInstanceName="btnImageUploadClient" >
                                                    <ClientSideEvents Click="function(s, e) { 
uploader.Upload();
}"></ClientSideEvents>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                    <HeaderStyle>
                        <Paddings PaddingTop="1px" PaddingRight="6px" PaddingLeft="10px"></Paddings>
                    </HeaderStyle>
                    <SizeGripImage Height="12px" Width="12px"></SizeGripImage>
                    <CloseButtonImage Height="17px" Width="17px"></CloseButtonImage>
                </TSPControls:CustomASPxPopupControl>
               <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                                    <table >
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                        CausesValidation="False" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/new.png"></Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                        CausesValidation="False" Width="25px" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/edit.png"></Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                                        Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" OnClick="btnSave_Click">
                                                        <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/save.png"></Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                        CausesValidation="False" ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False"
                                                        EnableTheming="False" OnClick="btnBack_Click">
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image  Url="~/Images/icons/Back.png"></Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                            </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                                    <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldTecherLicence" ClientInstanceName="HiddenFieldTecherLicence">
                                    </dxhf:ASPxHiddenField>
                               
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                    BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div class="modalPopup">
                            لطفا صبر نمایید
                            <img align="middle" src="../../Image/indicator.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:ModalUpdateProgress>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ObjectDataSource ID="ODBMadrak" runat="server" FilterExpression="MeId={0}"
          OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
            TypeName="TSP.DataManager.MemberLicenceManager">
           
            <FilterParameters>
                <asp:Parameter Name="newparameter" />
            </FilterParameters>
           
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ODBLicence" runat="server" 
          SelectMethod="GetData" TypeName="TSP.DataManager.LicenceManager"
           >
          
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ODBMajor" runat="server"  OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
            TypeName="TSP.DataManager.MajorManager" >
            
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjdsCity" runat="server"
            OldValuesParameterFormatString="original_{0}" SelectMethod="SelectByCountry"
            TypeName="TSP.DataManager.CityManager" >
           
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="CounId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ODBUniversity" runat="server"  OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="TSP.DataManager.UniversityManager" >
         
        </asp:ObjectDataSource>

</asp:Content>
