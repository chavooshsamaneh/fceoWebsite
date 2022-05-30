<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberLicenceInsert.aspx.cs" Inherits="Employee_MembersRegister_MemberLicenceInsert"
    Title="مشخصات مدرک" %>

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


<%@ Register Src="../../UserControl/MemberInfoUserControl.ascx" TagName="MemberInfoUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SearchUniKeyPress(e, Type) {
            if (Type == 1)//DevExpress controls
            {
                if (e.htmlEvent.keyCode == 13) {
                    btnSearchUni.DoClick();
                }
            }
            else if (Type == 2)//asp controls
            {
                if (e.keyCode == 13)
                    btnSearchUni.DoClick();
            }
        }
        function SetControlValuesUni() {
            uniName.SetText('در حال بارگذاری ...');
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'UnId;UnCode;UnName', SetValueUni);
        }
        function SetValueUni(values) {
            uniName.SetText(values[2]);
            UniValue.Set("Id", values[0]);
        }


        function SetControlValues() {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'UnId;UnCode;UnName', SetValue);
        }
        function SetValue(values) {
            uniName.SetText(values[2]);
            UniValue.Set("Id", values[0]);
        }

        function CheckDate() {
            var StartDate = document.getElementById('<%=txtStartDate.ClientID%>').value;
            var EndDate = document.getElementById('<%=txtEndDate.ClientID%>').value;
            if (EndDate < StartDate && EndDate != "")
                return -1;
            else
                return 1;
        }

        function CheckUniBeforeSave() {
            if (txtUniName.GetText() == '' && uniName.GetText() == '') {
                uniName.SetIsValid(false);
                return false;
            }
            else {
                uniName.SetIsValid(true);
                return true;
            }
        }

        function GetImage(Values) {
            var ImageUrl = Values.GetImageUrl();
            window.open(ImageUrl, '_blank');
        }

    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="DivReport" class="DivErrors" align="right" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table >
                            <tbody>
                                <tr>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">                                        
                                            <Image  Url="~/Images/icons/new.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
	flpli.Set('name',0);
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                  
                                            <Image  Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                            ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if(HiddenFieldLicence.Get('IsConfMode')==0)
	{	       
	    if(CheckDate()==-1)
	    {
		    e.processOnServer=false;
		    lblDateError.SetVisible(true);
	    }
	    else
		    lblDateError.SetVisible(false);
    	  
	    if(flpli.Get('name')!=1)
	    {
		    lbli.SetVisible(true);
		    e.processOnServer=false;
	    } 
	}
    else if(flpli.Get('InquiryImage')!=1)
    {
		    InquiryImageValidation.SetVisible(true);
		    e.processOnServer=false;
    }


}"></ClientSideEvents>
                                      
                                            <Image  Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td >
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
            <div style="width: 100%; text-align: right; display: none">
                <dxe:ASPxLabel ID="lblSex" runat="server">
                </dxe:ASPxLabel>
                <dxe:ASPxLabel ID="lblT" runat="server">
                </dxe:ASPxLabel>
                <dxe:ASPxLabel ID="lblOfName" runat="server">
                </dxe:ASPxLabel>
            </div>
            <uc2:MemberInfoUserControl ID="MemberInfoUserControl" runat="server" />
            <br />
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table align="right" dir="rtl" width="100%">
                            <tbody>
                                <tr>
                                    <td valign="top" align="right" style="width: 15%">
                                        <asp:Label runat="server" Text=" مقطع تحصیلی *" ID="Label37" Width="100%"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" style="width: 35%">
                                        <TSPControls:CustomAspxComboBox runat="server" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                            ValueType="System.String" DataSourceID="ODBLicence" TextField="LiName" ValueField="LiId"
                                            RightToLeft="True"  Width="100%"  ID="drdLicence">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                            
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" ErrorText="مقطع تحصیلی را انتخاب نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right" style="width: 15%"></td>
                                    <td valign="top" align="right" style="width: 35%"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" Text="رشته *" ID="Label38" Width="100%"></asp:Label></td>
                                    <td colspan="3">
                                        <TSPControls:CustomAspxComboBox runat="server" EnableIncrementalFiltering="True" IncrementalFilteringMode="Contains"
                                            ValueType="System.String" DataSourceID="ODBMajor" TextField="MjNameCode" ValueField="MjId"
                                            RightToLeft="True" Width="100%" 
                                           ID="drdMajor" >
                                           
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                           
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" ErrorText="رشته را انتخاب نمایید"></RequiredField>
                                            </ValidationSettings>
                                            <Columns>
                                                <dxe:ListBoxColumn FieldName="MjNameCode" Caption="عنوان" Width="80%"  />
                                                <dxe:ListBoxColumn FieldName="InActive" Caption="وضعیت" Width="20%" />
                                            </Columns>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td valign="top" align="right" style="width: 15%">
                                        <asp:Label runat="server" Text="کشور *" ID="Label6" Width="100%"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" style="width: 35%">
                                        <TSPControls:CustomAspxComboBox runat="server" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                            ValueType="System.String" DataSourceID="OdbCountry" TextField="CounName" ValueField="CounId"
                                             Width="100%" 
                                            RightToLeft="True" 
                                            ClientInstanceName="cmbCoun" EnableClientSideAPI="True" ID="ComboCountry">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {	
	grid.PerformCallback(cmbCoun.GetValue().toString()+';'+txtUniNameSearch.GetText()+';'+'IndexChange');
}"></ClientSideEvents>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                             
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="True" ErrorText="کشور را انتخاب نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right" style="width: 15%">
                                        <asp:Label runat="server" Text="شهر" Width="100%" ID="Label5"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" style="width: 35%">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"  
                                            ClientInstanceName="City" ID="txtCity">
                                            <ClientSideEvents Validation="function(s, e) {
}"></ClientSideEvents>
                                            <ValidationSettings EnableCustomValidation="True" Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RegularExpression ErrorText=""></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="دانشگاه *" ID="Label40" Width="100%"></asp:Label>
                                    </td>
                                    <td dir="rtl" valign="top" align="right">
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" align="right" style="width: 90%">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" ReadOnly="True"  
                                                            ClientInstanceName="uniName" ID="txtdrdUniName">
                                                            <ClientSideEvents Validation="function(s, e) {

}"></ClientSideEvents>
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ErrorText="دانشگاه را انتخاب نمایید">
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                                <RegularExpression ErrorText=""></RegularExpression>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right" style="width: 10%">
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" CausesValidation="False"
                                                            Text=" "  EnableTheming="False" ToolTip="جستجو"
                                                            ID="btnSearch1" EnableViewState="False">
                                                            <ClientSideEvents Click="function(s, e) {
pop.Show();
}"></ClientSideEvents>
                                                            <Image  Url="~/Images/icons/Search.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                            </HoverStyle>
                                                            <FocusRectBorder BorderStyle="Solid" BorderWidth="1px"></FocusRectBorder>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="دیگر موارد" Width="100%" ID="Label4" Visible="False"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Visible="False"  
                                            ID="txtUniName" ClientInstanceName="txtUniName">
                                            <ClientSideEvents Validation="function(s, e) {
	//if(Uni.GetSelectedIndex()==0 &amp;&amp; e.value==null)
//{
//e.errorText=&quot;دانشگاه را وارد نمایید&quot;;
//e.isValid=false;
//}
 // uniName.SetErrorText(&quot;دانشگاه را وارد نمایید&quot;);

}"></ClientSideEvents>
                                            <ValidationSettings EnableCustomValidation="True" Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RegularExpression ErrorText=""></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="center" colspan="4">
                                        <asp:Label runat="server" Text="(دانشگاه وارد شده  جزء دانشگاه های مورد تأیید نظام نمی باشد)"
                                            ForeColor="Crimson" Width="100%" ID="lblUniwar" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="تاریخ شروع" ID="Label1" Width="100%"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" ShowPickerOnTop="True" ShowPickerOnEvent="OnClick"
                                            Width="225px" ID="txtStartDate" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                        <dxe:ASPxLabel runat="server" Text="محدوده تاریخ وارد شده صحیح نمی باشد" ClientInstanceName="lblDateError"
                                            ClientVisible="False" ForeColor="Red" ID="ASPxLabel2">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="تاریخ فارغ التحصیلی *" Width="100%" ID="Label44"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                            PickerDirection="ToRight" ShowPickerOnTop="True" ShowPickerOnEvent="OnClick"
                                            Width="225px" ID="txtEndDate" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                        <pdc:PersianDateValidator runat="server" ClientValidationFunction="PersianDateValidator"
                                            ValidateEmptyText="True" ControlToValidate="txtEndDate" ErrorMessage="تاریخ را وارد نمایید"
                                            ID="PersianDateValidator1">تاریخ نامعتبر</pdc:PersianDateValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="تعداد واحد" ID="Label2" Width="100%"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" MaxLength="3"  
                                            ID="txtNumUnit">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RegularExpression ErrorText="تعداد واحد را با فرمت صحیح وارد نمایید" ValidationExpression="\d{2,3}"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="معدل *" ID="Label3" Width="100%"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" MaxLength="5"  
                                            ID="txtAvg">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText="معدل را با فرمت 2 رقم اعشار وارد نمایید.مثلا 18.20"
                                                    ValidationExpression="\d\d\.\d\d"></RegularExpression>
                                                <RequiredField ErrorText="معدل را وارد نمایید" IsRequired="true"></RequiredField>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="موضوع پایان نامه" Width="100%" ID="Label7"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%"  
                                            ClientInstanceName="Thesis" ID="txtThesis">
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RegularExpression ErrorText=""></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="توضیحات" ID="Label41" Width="100%"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" Height="40px" Width="100%"  
                                            ID="txtDescription">
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="نوع مدرک *" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxComboBox runat="server"  Width="100%"
                                            ValueType="System.String" ID="cmbLicenceType"
                                            ClientInstanceName="cmbLicenceType" >
                                            <Items>
                                                <dxe:ListEditItem Text="مدرک پیش فرض می باشد" Value="1" />
                                                <dxe:ListEditItem Text="مدرک پیش فرض نمی باشد" Value="0" />
                                            </Items>
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                             
                                                <RequiredField IsRequired="True" ErrorText="نوع مدرک را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تصویر مدرک" ID="ASPxLabel11" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" InputType="Images" UploadWhenFileChoosed="True"
                                                            ClientInstanceName="flpi" ID="flpLicense" OnFileUploadComplete="flpLicense_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClientIdNo.SetVisible(true);
    flpli.Set('name',1);
	lbli.SetVisible(false);
	hpl.SetVisible(true);
	hpl.SetImageUrl('../../image/Members/License/'+e.callbackData);
   
	}
	else{
	imgEndUploadImgClientIdNo.SetVisible(false);
    flpli.Set('name',0);
	lbli.SetVisible(true);
	hpl.SetVisible(false);
	hpl.SetImageUrl('');
	}
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="تصویر مدرک را انتخاب نمایید" ClientInstanceName="lbli"
                                                            ClientVisible="False" ForeColor="Red" ID="ASPxLabel12">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد"
                                                            ClientInstanceName="imgEndUploadImgClientIdNo" ClientVisible="False" ID="imgEndUploadImgIdNo">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <div id="divLicense" onclick="GetImage(hpl)">
                                            <dxe:ASPxImage runat="server" ToolTip="تصویر مدرک" Height="150px" Width="150px"
                                                ID="HpLicense" ClientInstanceName="hpl"
                                                Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True" ClientSideEvents-Click="">
                                                <EmptyImage Height="150px" Width="150px" Url="../../Images/noimage.gif">
                                                </EmptyImage>
                                            </dxe:ASPxImage>
                                        </div>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="ریز نمرات" ID="ASPxLabel1" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" InputType="Files" UploadWhenFileChoosed="True"
                                                            ClientInstanceName="FileUploadScoreImage" ID="FileUploadScoreImage" OnFileUploadComplete="FileUploadScoreImage_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClientScore.SetVisible(true);
    flpli.Set('Score',1);
	lblValidScoreImage.SetVisible(false);
	HyperLinkScore.SetVisible(true);
	HyperLinkScore.SetNavigateUrl('../../image/Members/Scores/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientScore.SetVisible(false);
    flpli.Set('Score',0);
	lblValidScoreImage.SetVisible(true);
	HyperLinkScore.SetVisible(false);
	HyperLinkScore.SetNavigateUrl('');
	}
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="ریز نمرات را انتخاب نمایید" ClientInstanceName="lblValidScoreImage"
                                                            ClientVisible="False" ForeColor="Red" ID="lblValidScoreImage">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/Images/icons/button_ok.png" ToolTip="فایل انتخاب شد"
                                                            ClientInstanceName="imgEndUploadImgClientScore" ClientVisible="False" ID="imgEndUploadImgClientScore">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink runat="server" Text="ریز نمرات" Target="_blank" ClientInstanceName="HyperLinkScore"
                                            ClientVisible="False" ID="HyperLinkScore" ImageHeight="150px" ImageWidth="150px">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="جذب از طریق آزمون" ID="ASPxLabel5" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" InputType="Images" UploadWhenFileChoosed="True"
                                                            ClientInstanceName="FileUploadEntranceExamConfImageURL" ID="FileUploadEntranceExamConfImageURL"
                                                            OnFileUploadComplete="FileUploadEntranceExamConfImageURL_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClientEntranceExamConfImage.SetVisible(true);
    flpli.Set('EntranceExamConfImageURL',1);
	lblValiEntranceExamConfImage.SetVisible(false);
	HyperLinkEntranceExamConf.SetVisible(true);
	HyperLinkEntranceExamConf.SetImageUrl('../../image/Members/EntranceExamConfirmation/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientEntranceExamConfImage.SetVisible(false);
    flpli.Set('EntranceExamConfImageURL',0);
	lblValiEntranceExamConfImage.SetVisible(true);
	HyperLinkEntranceExamConf.SetVisible(false);
	HyperLinkEntranceExamConf.SetImageUrl('');
	}
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientInstanceName="lblValiEntranceExamConfImage"
                                                            ClientVisible="False" ForeColor="Red" ID="lblValiEntranceExamConfImage">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد"
                                                            ClientInstanceName="imgEndUploadImgClientEntranceExamConfImage" ClientVisible="False"
                                                            ID="imgEndUploadImgClientEntranceExamConfImage">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <div id="divHyperLinkEntranceExamConf" onclick="GetImage(HyperLinkEntranceExamConf)">
                                            <dxe:ASPxImage runat="server" ToolTip="جذب از طریق آزمون" Height="150px" Width="150px"
                                                ID="HyperLinkEntranceExamConf" ClientInstanceName="HyperLinkEntranceExamConf"
                                                Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                                <EmptyImage Height="150px" Width="150px" Url="../../Images/noimage.gif">
                                                </EmptyImage>
                                            </dxe:ASPxImage>
                                        </div>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="نامه معادلسازی" ID="ASPxLabel7" Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" InputType="Images" UploadWhenFileChoosed="True"
                                                            ClientInstanceName="FileUploadEquivalentImageURL" ID="FileUploadEquivalentImageURL"
                                                            OnFileUploadComplete="FileUploadEquivalentImageURL_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	imgEndUploadImgClientEquivalent.SetVisible(true);
    flpli.Set('Equivalent',1);
	lblValidEquivalentImage.SetVisible(false);
	HyperLinkEquivalent.SetVisible(true);
	HyperLinkEquivalent.SetImageUrl('../../image/Members/Equivalent/'+e.callbackData);
    
	}
	else{
	imgEndUploadImgClientEquivalent.SetVisible(false);
    flpli.Set('Equivalent',0);
	lblValidEquivalentImage.SetVisible(true);
	HyperLinkEquivalent.SetVisible(false);
	HyperLinkEquivalent.SetImageUrl('');
	}
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="نامه معادلسازی را انتخاب نمایید" ClientInstanceName="lblValidEquivalentImage"
                                                            ClientVisible="False" ForeColor="Red" ID="lblValidEquivalentImage">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد"
                                                            ClientInstanceName="imgEndUploadImgClientEquivalent" ClientVisible="False" ID="imgEndUploadImgClientEquivalent">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <%--<dxe:ASPxHyperLink runat="server" Text="نامه معادلسازی" Target="_blank" ClientInstanceName="HyperLinkEquivalent"
                                            ClientVisible="False" ID="HyperLinkEquivalent" ImageHeight="150px" ImageWidth="150px">
                                        </dxe:ASPxHyperLink>--%>
                                        <div id="divHyperLinkEquivalent" onclick="GetImage(HyperLinkEquivalent)">
                                            <dxe:ASPxImage runat="server" ToolTip="نامه معادلسازی" Height="150px" Width="150px"
                                                ID="HyperLinkEquivalent" ClientInstanceName="HyperLinkEquivalent" Border-BorderWidth="1px"
                                                Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                                <EmptyImage Height="150px" Width="150px" Url="../../Images/noimage.gif">
                                                </EmptyImage>
                                            </dxe:ASPxImage>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right" colspan="4">
                                        <dxp:ASPxPanel ID="PanelInQuiry" ClientInstanceName="PanelInQuiry" runat="server">
                                            <PanelCollection>
                                                <dxp:PanelContent>
                                                    <fieldset>
                                                        <dl style="font-family=tahoma; font-size: 7pt; line-height: 15pt">
                                                            <table dir="rtl" width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td valign="top" align="right" colspan="4">
                                                                            <TSPControls:CustomASPxCheckBox runat="server" Text="مدرک فوق استعلام شده است" ClientInstanceName="chbs"
                                                                                ID="ChbEstelam">
                                                                            </TSPControls:CustomASPxCheckBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" align="right" width="15%">
                                                                            <dxe:ASPxLabel runat="server" Text="نوع تایید" Width="100%" ClientInstanceName="lblConfirm"
                                                                                ID="lblConfirm">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td valign="top" align="right" width="35%">
                                                                            <TSPControls:CustomAspxComboBox runat="server"  Width="100%"
                                                                                RightToLeft="True" 
                                                                                ClientInstanceName="cmbConfirm" EnableClientSideAPI="True" ID="cmbConfirm" SelectedIndex="0"
                                                                                ValueType="System.String" HorizontalAlign="Right">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Value="0" Text="عدم تایید"></dxe:ListEditItem>
                                                                                    <dxe:ListEditItem Value="1" Text="تایید شده"></dxe:ListEditItem>
                                                                                    <dxe:ListEditItem Value="2" Text="جعلی"></dxe:ListEditItem>
                                                                                    <dxe:ListEditItem Value="3" Text="استعلام سایر استان ها-انتقالی"></dxe:ListEditItem>
                                                                                </Items>
                                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                  
                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                    <RequiredField IsRequired="True" ErrorText="نوع تایید را انتخاب نمایید"></RequiredField>
                                                                                </ValidationSettings>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </TSPControls:CustomAspxComboBox>
                                                                        </td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4">
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td valign="top" align="right" width="15%">
                                                                                        <dxe:ASPxLabel runat="server" Text="تصویر استعلام" Width="100%" ClientInstanceName="lblInqImage"
                                                                                            ID="lblInqImage">
                                                                                        </dxe:ASPxLabel>
                                                                                    </td>
                                                                                    <td>
                                                                                        <table>
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <TSPControls:CustomAspxUploadControl runat="server" InputType="Images" UploadWhenFileChoosed="True"
                                                                                                            ClientInstanceName="UploadControlInquiry" ID="UploadControlInquiry" OnFileUploadComplete="UploadControlInquiry_FileUploadComplete">
                                                                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	ImageInquiryImageUploded.SetVisible(true);
    flpli.Set('InquiryImage',1);
	InquiryImageValidation.SetVisible(false);
	InquiryImageLink.SetVisible(true);
	InquiryImageLink.SetImageUrl('../../image/Members/LicenseInquery/'+e.callbackData);
	}
	else{
	ImageInquiryImageUploded.SetVisible(false);
    flpli.Set('InquiryImage',0);
	InquiryImageValidation.SetVisible(true);
	InquiryImageLink.SetVisible(false);
	InquiryImageLink.SetImageUrl('');
	}
}"></ClientSideEvents>
                                                                                                        </TSPControls:CustomAspxUploadControl>
                                                                                                        <dxe:ASPxLabel runat="server" Text="تصویر استعلام را انتخاب نمایید" ClientInstanceName="InquiryImageValidation"
                                                                                                            ClientVisible="False" ForeColor="Red" ID="InquiryImageValidation">
                                                                                                        </dxe:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dxe:ASPxImage runat="server" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد"
                                                                                                            ClientInstanceName="ImageInquiryImageUploded" ClientVisible="False" ID="ImageInquiryImageUploded">
                                                                                                        </dxe:ASPxImage>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                        <div id="divInquiryImageLink" onclick="GetImage(InquiryImageLink)">
                                                                                            <dxe:ASPxImage runat="server" ToolTip="تصویر استعلام" Height="150px" Width="150px"
                                                                                                ID="InquiryImageLink" ClientInstanceName="InquiryImageLink" Border-BorderWidth="1px"
                                                                                                Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                                                                                <EmptyImage Height="150px" Width="150px" Url="../../Images/noimage.gif">
                                                                                                </EmptyImage>
                                                                                            </dxe:ASPxImage>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </dl>
                                                    </fieldset>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table >
                            <tbody>
                                <tr>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">
                                        
                                            <Image  Url="~/Images/icons/new.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
	flpli.Set('name',0);
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                  
                                            <Image  Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ذخیره"
                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
if(HiddenFieldLicence.Get('IsConfMode')==0)
{
	    if(CheckDate()==-1)
	    {
		    e.processOnServer=false;
		    lblDateError.SetVisible(true);
	    }
	    else
		    lblDateError.SetVisible(false);

	    if(flpli.Get('name')!=1)
	    {
		    lbli.SetVisible(true);
		    e.processOnServer=false;
	    }
}
 else if(flpli.Get('InquiryImage')!=1)
    {
		    InquiryImageValidation.SetVisible(true);
		    e.processOnServer=false;
    }
}"></ClientSideEvents>
                                         
                                            <Image  Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
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
            <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl1" runat="server" Width="600px" 
                  HeaderText="جستجو"
                ClientInstanceName="pop" 
                PopupElementID="btnSearch1" >
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">
                        <table class="TableBorder" width="100%">
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <table>
                                            <tr>
                                                <td style="height: 28px">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="انتخاب دانشگاه"
                                                        CausesValidation="False" ID="btnSearchUniversity" AutoPostBack="False" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False">
                                                        <ClientSideEvents Click="function(s,e){
if (grid.GetFocusedRowIndex()&lt;0)
{
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
}
else{
       pop.Hide();
       SetControlValuesUni();
}
                                                            }"></ClientSideEvents>
                                                        
                                                        <Image   Url="~/Images/icons/button_ok.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروج"
                                                        CausesValidation="False" ID="btnClosePopupReciever" AutoPostBack="False" UseSubmitBehavior="False"
                                                        EnableViewState="False" EnableTheming="False">
                                                        <ClientSideEvents Click="function(s,e){pop.Hide()}"></ClientSideEvents>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                        </HoverStyle>
                                                        <Image   Url="~/Images/Close-box-red.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                      
                        <fieldset><legend class="HelpUL">جستجو</legend>
                        
                                    <table dir="rtl" width="100%">
                                        <tbody>
                                            <tr>
                                                <td valign="top" align="right" width="100px">
                                                    <dxe:ASPxLabel runat="server" Text="نام دانشگاه" ID="ASPxLabel3">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="100%" ID="txtUniNameSearch"
                                                        ClientInstanceName="txtUniNameSearch" >
                                                        <ClientSideEvents KeyPress="function(s,e){SearchUniKeyPress(e,1);}" />
                                                       
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr style="font-size: 12pt; font-family: Times New Roman">
                                                <td valign="top" align="center" colspan="2">
                                                    <br />
                                                    <TSPControls:CustomAspxButton   runat="server" Text="جستجو"  ID="btnSearchUni"
                                                        AutoPostBack="False" UseSubmitBehavior="False" 
                                                        Width="98px" ClientInstanceName="btnSearchUni" CausesValidation="false">
                                                        <ClientSideEvents Click="function(s, e) {
	 if(!grid.InCallback())
		grid.PerformCallback('Search;'+txtUniNameSearch.GetText()+';'+cmbCoun.GetValue().toString());
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                             </fieldset>
                        <TSPControls:CustomAspxDevGridView runat="server"  Width="600px"
                            ID="CustomAspxDevGridView1" DataSourceID="ObjectDataSourceSearchUniversity" KeyFieldName="UnId"
                            ClientInstanceName="grid" OnCustomCallback="CustomAspxDevGridView1_CustomCallback">
                            <ClientSideEvents RowDblClick="function(s, e) {
		pop.Hide();
		SetControlValuesUni();
}"></ClientSideEvents>
                            <Settings ShowGroupPanel="True" ShowFilterRowMenu="True" ShowFilterRow="True"></Settings>
                            <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                            <Columns>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="CounName" Caption="کشور"
                                    Width="17%">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="UnCode" Caption="کد دانشگاه"
                                    Width="10%">
                                    <HeaderStyle Wrap="true" />
                                    <CellStyle Wrap="false">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="UnName" Caption="نام دانشگاه"
                                    Width="50%">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="UnTypeName" Caption="نوع"
                                    Width="16%">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="2" FieldName="UnId" Width="7%">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="5" ShowClearFilterButton="true">
                                  
                                </dxwgv:GridViewCommandColumn>
                            </Columns>
                        </TSPControls:CustomAspxDevGridView>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </TSPControls:CustomASPxPopupControl>
            <asp:ObjectDataSource ID="ObjectDataSourceSearchUniversity" runat="server" TypeName="TSP.DataManager.UniversityManager"
                SelectMethod="SelectConfirmedActiveUniversityByCounId" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-2" Name="CounId" Type="Int32" />
                    <asp:Parameter DefaultValue="%" Name="UnName" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBLicence" runat="server"
                SelectMethod="GetData" TypeName="TSP.DataManager.LicenceManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBMajor" runat="server"
                OldValuesParameterFormatString="original_{0}" SelectMethod="MajorInActive" TypeName="TSP.DataManager.MajorManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="InActiveMajor" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBUniversity" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.UniversityManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBCity" runat="server"
                SelectMethod="SelectByCounId"
                TypeName="TSP.DataManager.CityManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-2" Name="CounId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbCountry" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.CountryManager">
                <FilterParameters>
                    <asp:Parameter Name="newparameter" />
                </FilterParameters>
            </asp:ObjectDataSource>
            <asp:HiddenField ID="MemberRequest" runat="server" Visible="False" />
            <asp:HiddenField ID="HDMode" runat="server" Visible="False" />
            <dxhf:ASPxHiddenField ID="HiddenFieldUniValue" runat="server" ClientInstanceName="UniValue">
            </dxhf:ASPxHiddenField>
            <asp:HiddenField ID="HDEditMode" runat="server" />
            <dxhf:ASPxHiddenField ID="HDFlpLicense" runat="server" ClientInstanceName="flpli">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HiddenFieldLicence" runat="server" ClientInstanceName="HiddenFieldLicence">
            </dxhf:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
