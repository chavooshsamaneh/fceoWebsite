<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberLicenceInsert.aspx.cs" Inherits="Members_MemberInfo_MemberLicenceInsert"
    Title="مشخصات مدرک تحصیلی" %>

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

        function CheckDate() {
            var StartDate = document.getElementById('<%=txtStartDate.ClientID%>').value;
            var EndDate = document.getElementById('<%=txtEndDate.ClientID%>').value;
            if (EndDate < StartDate && EndDate != "")
                return -1;
            else
                return 1;
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="btnNew" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                            <ClientSideEvents Click="function(s, e) {
	flpli.Set('name',0);
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {

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
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
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
            <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table align="right" dir="rtl" width="100%">
                            <tbody>
                                <tr>
                                    <td align="right" valign="top" width="15%">
                                        <asp:Label runat="server" Text="مقطع تحصیلی *" ID="Label37"></asp:Label>
                                    </td>
                                    <td align="right" valign="top" width="35%">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            RightToLeft="True" TextField="LiName" ID="drdLicence"
                                            EnableIncrementalFiltering="true" DataSourceID="ODBLicence"
                                            ValueType="System.String" ValueField="LiId">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="True" ErrorText="مقطع تحصیلی را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td align="right" valign="top" width="15%"></td>
                                    <td align="right" valign="top" width="35%"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" width="100%">
                                        <TSPControls:CustomAspxCallbackPanel runat="server"
                                            ClientInstanceName="CallbackPanelMajor" Width="100%" ID="CallbackPanelMajor"
                                            OnCallback="CallbackPanelMajor_Callback">
                                            <Paddings Padding="0" />
                                            <PanelCollection>
                                                <dxp:PanelContent>
                                                    <table width="100%">
                                                        <tr>
                                                            <td valign="top" align="right" width="15%">
                                                                <asp:Label runat="server" Text="گروه رشته*" ID="Label12"></asp:Label>
                                                            </td>
                                                            <td valign="top" align="right" width="35%">
                                                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                    TextField="MjName" ID="cmbParentMajor" DataSourceID="ODBParentMajor" RightToLeft="True"
                                                                    ValueType="System.String" ValueField="MjId" ClientInstanceName="cmbParentMajor"
                                                                    AutoPostBack="false">
                                                                    <ClientSideEvents SelectedIndexChanged="function(s,e){ 
                                                                                           CallbackPanelMajor.PerformCallback('cmbChange'+';'+ cmbParentMajor.GetValue());} " />
                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                        </ErrorImage>
                                                                        <RequiredField IsRequired="True" ErrorText="طبقه بندی رشته را انتخاب نمایید"></RequiredField>
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomAspxComboBox>
                                                                <asp:ObjectDataSource ID="ODBParentMajor" runat="server" SelectMethod="FindMjParents"
                                                                    TypeName="TSP.DataManager.MajorManager"></asp:ObjectDataSource>
                                                            </td>
                                                            <td valign="top" align="right" width="15%">
                                                                <asp:Label runat="server" Text="عنوان رشته*" ID="Label8"></asp:Label>
                                                            </td>
                                                            <td valign="top" align="right" width="35%">
                                                                <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                    TextField="MjName" ID="cmbMajor" RightToLeft="True" ValueType="System.String"
                                                                    DataSourceID="ODBMajor" ValueField="MjId" ClientInstanceName="cmbMajor">
                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                        <RequiredField ErrorText="عنوان رشته را وارد نمائید" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </TSPControls:CustomAspxComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </TSPControls:CustomAspxCallbackPanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="کشور" ID="Label6"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox ID="ComboCountry" runat="server" ClientInstanceName="ComboCountry"
                                            DataSourceID="OdbCountry" EnableClientSideAPI="True" EnableIncrementalFiltering="True"
                                            TextField="CounName"
                                            ValueField="CounId" ValueType="System.String" Width="100%" RightToLeft="True" AutoPostBack="true" OnSelectedIndexChanged="ComboCountry_SelectedIndexChanged" >                                           
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField ErrorText="کشور را انتخاب نمایید" IsRequired="True" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="شهر *" ID="Label42"></asp:Label>
                                    </td>
                                    <td dir="ltr" align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtCity" ClientInstanceName="City">
                                            <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField IsRequired="true" ErrorText="شهر را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText=""></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label9" runat="server" Text="دانشگاه *"></asp:Label>
                                    </td>
                                    <td align="right" colspan="3" width="100%">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                            RightToLeft="True"
                                            TextField="UnName" ID="ComboUniversity" EnableClientSideAPI="True" ValueType="System.String"
                                            ValueField="UnId" DataSourceID="ObjectDataSourceSearchUniversity" ClientInstanceName="ComboUniversity">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField IsRequired="true" ErrorText="دانشگاه را انتخاب نمایید"></RequiredField>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomAspxComboBox>

                                        <asp:ObjectDataSource ID="ObjectDataSourceSearchUniversity" runat="server" TypeName="TSP.DataManager.UniversityManager"
                                            SelectMethod="SelectConfirmedActiveUniversityByCounId" OldValuesParameterFormatString="original_{0}">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="-2" Name="CounId" Type="Int32" />
                                                <asp:Parameter DefaultValue="%" Name="UnName" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="تاریخ شروع" ID="Label1"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="245px" ShowPickerOnTop="True"
                                            ShowPickerOnEvent="OnClick" ID="txtStartDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" ClientInstanceName="lblDateError" ClientVisible="False"
                                            ForeColor="Red" Text="محدوده تاریخ وارد شده صحیح نمی باشد">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="تاریخ فارغ التحصیلی *" ID="Label44"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="245px" ShowPickerOnTop="True"
                                            ID="txtEndDate" ShowPickerOnEvent="OnClick" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                            ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtEndDate" ID="PersianDateValidator1">تاریخ نامعتبر</pdc:PersianDateValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="تعداد واحد" ID="Label2"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="3" ID="txtNumUnit">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RegularExpression ErrorText="تعداد واحد را با فرمت صحیح وارد نمایید" ValidationExpression="\d{2,3}"></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="معدل *" ID="Label3"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="5" ID="txtAvg">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="معدل را وارد نمایید"></RequiredField>
                                                <RegularExpression ErrorText="معدل را با 2 رقم اعشار وارد نمایید.مثلا 18.20" ValidationExpression="\d\d\.\d\d"></RegularExpression>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="موضوع پایان نامه" ID="Label7"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtThesis" ClientInstanceName="Thesis">
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RegularExpression ErrorText=""></RegularExpression>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label runat="server" Text="توضیحات" ID="Label41"></asp:Label>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <TSPControls:CustomASPXMemo runat="server" Height="40px" Width="100%" ID="txtDescription">
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="تصویر مدرک *">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" colspan="3" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl ID="flpLicense" runat="server" ClientInstanceName="flpi"
                                                            InputType="Images" UploadWhenFileChoosed="true" OnFileUploadComplete="flpLicense_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                if(e.isValid){
	imgEndUploadImgClientIdNo.SetVisible(true);
    flpli.Set('name',1);
	lbli.SetVisible(false);
	hpl.SetVisible(true);
	hpl.SetNavigateUrl('../../image/Members/License/'+e.callbackData);
	}
	else
	{
	imgEndUploadImgClientIdNo.SetVisible(false);
    flpli.Set('name',0);
	lbli.SetVisible(true);
	hpl.SetVisible(false);
	hpl.SetNavigateUrl('');
	}
}" />
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="ASPxLabel12" runat="server" ClientInstanceName="lbli" ClientVisible="False"
                                                            ForeColor="Red" Text="تصویر مدرک را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage ID="imgEndUploadImgIdNo" runat="server" ClientInstanceName="imgEndUploadImgClientIdNo"
                                                            ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink ID="HpLicense" runat="server" ClientInstanceName="hpl" ClientVisible="False"
                                            Target="_blank" Text="تصویر مدرک">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="نوع مدرک *" Width="78px">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%" ValueType="System.String" ID="cmbLicenceType"
                                            ClientInstanceName="cmbLicenceType">
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
                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                            <ClientSideEvents Click="function(s, e) {
	flpli.Set('name',0);
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
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
}" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
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
            <dxhf:ASPxHiddenField ID="HiddenFieldLicence" runat="server" ClientInstanceName="HiddenFieldLicence">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HiddenFieldUniValue" runat="server" ClientInstanceName="UniValue">
            </dxhf:ASPxHiddenField>
            <asp:HiddenField ID="HDMode" runat="server" Visible="False" />
            <dxhf:ASPxHiddenField ID="HDFlpLicense" runat="server" ClientInstanceName="flpli">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ODBLicence" runat="server"
                SelectMethod="GetData" TypeName="TSP.DataManager.LicenceManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBMajor" runat="server" SelectMethod="FindMajorAndChilds"
                TypeName="TSP.DataManager.MajorManager">
                <SelectParameters>
                    <asp:Parameter DbType="Int32" DefaultValue="-1" Name="MjId" />
                    <asp:Parameter DbType="Int32" DefaultValue="-1" Name="InActive" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbCountry" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.CountryManager">
                <FilterParameters>
                    <asp:Parameter Name="newparameter" />
                </FilterParameters>
            </asp:ObjectDataSource>
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
