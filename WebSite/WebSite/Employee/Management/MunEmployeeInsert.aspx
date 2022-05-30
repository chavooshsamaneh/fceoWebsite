<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MunEmployeeInsert.aspx.cs" Inherits="Employee_Management_MunEmployeeInsert"
    Title="مشخصات کارمند شهرداری" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
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
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]
                </div>
                   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                                    <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                        width="100%">
                                        <tbody>
                                            <tr>
                                                <td align="right">
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                        cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                                        CausesValidation="False" ID="btnNew" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                                        EnableViewState="False" EnableTheming="False" OnClick="btnNew_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	//SetNewMode();
}"></ClientSideEvents>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/new.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                                        CausesValidation="False" Width="25px" ID="btnEdit" EnableClientSideAPI="True"
                                                                        UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnEditClient"
                                                                        OnClick="btnEdit_Click">
                                                                        <ClientSideEvents Click="function(s, e) {

}
"></ClientSideEvents>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/edit.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                                                        ID="btnSave" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" ClientInstanceName="btnSaveClient" OnClick="btnSave_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	
if(CheckCharacterEncoding(txtFirstNameEnClient.GetText())==false)
 {
txtFirstNameEnClient.SetIsValid(false);
txtFirstNameEnClient.SetErrorText('حروف وارد شده نامعتبر است');
	e.processOnServer=false;
}
if(CheckCharacterEncoding(txtLastNameEnClient.GetText())==false)
 {
txtLastNameEnClient.SetIsValid(false);
txtLastNameEnClient.SetErrorText('حروف وارد شده نامعتبر است');
	e.processOnServer=false;
}


}
"></ClientSideEvents>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/save.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                                        CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnBack_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
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
                	<TSPControls:CustomASPxRoundPanel ID="RoundPanelEmlpoyee" HeaderText="مشاهده" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

        
                                    <table dir="rtl" id="Table2" width="100%">
                                        <tbody>
                                            <tr>
                                                <td valign="top" align="right" width="15%">
                                                    <asp:Label runat="server" Text="شهرداری" ID="Label1"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" width="35%">
                                                    <TSPControls:CustomAspxComboBox runat="server"  Width="100%" 
                                                        TextField="MunName" ID="cmbMun" DataSourceID="ObjectDataSourceMun" EnableClientSideAPI="True"
                                                        ValueType="System.String" ValueField="MunId" 
                                                        RightToLeft="True">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="شهرداری را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomAspxComboBox>
                                                </td>
                                                <td valign="top" align="right" width="15%"></td>
                                                <td valign="top" align="right" width="35%"></td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="نام" ID="Label2"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFirstName"  EnableClientSideAPI="True"
                                                        Width="100%" ClientInstanceName="txtFirstNameClient" >
                                                        <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right" colspan="1">
                                                    <asp:Label runat="server" Text="(انگلیسی)" ID="Label4"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFirstNameEn"  EnableClientSideAPI="True"
                                                        Width="100%" ClientInstanceName="txtFirstNameEnClient" >
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="نام خانوادگی" ID="Label3"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtLastName"  EnableClientSideAPI="True"
                                                        Width="100%" ClientInstanceName="txtLastNameClient" >
                                                        <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText=" نام خانوادگی را وارد نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="(انگلیسی)" ID="Label5"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtLastNameEn"  EnableClientSideAPI="True"
                                                        Width="100%" ClientInstanceName="txtLastNameEnClient" >
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="نام پدر" ID="Label6"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFatherName"  EnableClientSideAPI="True"
                                                        Width="100%" ClientInstanceName="txtFatherNameClient" >
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="نام پدر را وارد نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right"></td>
                                                <td valign="top" align="right"></td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="تاریخ تولد" ID="Label7"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                        Width="230px" ShowPickerOnTop="True" ID="txtBirthDate" PickerDirection="ToRight"
                                                        RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="محل تولد" ID="Label8"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtBirthPlace"  EnableClientSideAPI="True"
                                                        Width="100%" ClientInstanceName="txtBirthPlaceClient" >
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="شماره شناسنامه" ID="Label10"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtIdNo"  EnableClientSideAPI="True"
                                                        Width="100%" MaxLength="10" ClientInstanceName="txtIdNoClient" >
                                                        <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="شماره شناسنامه را وارد نمایید"></RequiredField>
                                                            <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,10}"></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="کد ملی" ID="Label9"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtSSN"  EnableClientSideAPI="True"
                                                        Width="100%" MaxLength="10" ClientInstanceName="txtSSNClient" >
                                                        <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="کد ملی را وارد نمایید"></RequiredField>
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
                                                    <asp:Label runat="server" Text="جنسیت" ID="Label11"></asp:Label>
                                                </td>
                                                <td dir="ltr" valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox runat="server"  Width="100%" 
                                                        TextField="SexName" ID="cmbSexId" DataSourceID="ObjectDataSourceDrpSex" EnableClientSideAPI="True"
                                                        ValueType="System.String" ValueField="SexId" ClientInstanceName="cmbSexIdClient"
                                                         RightToLeft="True">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField ErrorText="جنسیت را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomAspxComboBox>
                                                </td>
                                                <td dir="ltr" valign="top" align="right">
                                                    <asp:Label runat="server" Text="وضعیت تاهل" ID="Label12"></asp:Label>
                                                </td>
                                                <td dir="ltr" valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox runat="server"  Width="100%" 
                                                        TextField="MarName" ID="cmbMarId" DataSourceID="ObjectDataSourceDrpMar" EnableClientSideAPI="True"
                                                        ValueType="System.String" ValueField="MarId" ClientInstanceName="cmbMarIdClient"
                                                         RightToLeft="True">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </TSPControls:CustomAspxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="آدرس" ID="Label22"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" colspan="3">
                                                    <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtAddress"  EnableClientSideAPI="True"
                                                        Width="100%" ClientInstanceName="txtAddressClient" >
                                                        <ValidationSettings>
                                                            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomASPXMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="شماره تلفن" ID="Label13"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtTel"  EnableClientSideAPI="True"
                                                        Width="100%" ClientInstanceName="txtTelClient" >
                                                        <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RegularExpression ErrorText="شماره را با پیش شماره وارد نمایید" ValidationExpression="\d{11,12}"></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="شماره همراه" ID="Label14"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMobileNo"  EnableClientSideAPI="True"
                                                        Width="100%" MaxLength="11" ClientInstanceName="txtMobileNoClient" >
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField IsRequired="True" ErrorText="شماره همراه را وارد نمایید"></RequiredField>
                                                            <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="0\d{1,10}"></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="آدرس وب سایت" ID="Label15"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtWebSite"  EnableClientSideAPI="True"
                                                        Width="100%" ClientInstanceName="txtWebSiteClient" >
                                                        <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RegularExpression ErrorText="آدرس وب سایت را با فرمت http://www.Example.com وارد نمایید"
                                                                ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="آدرس پست الکترونیکی" Width="123px" ID="Label16"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtEmail"  EnableClientSideAPI="True"
                                                        Width="100%" ClientInstanceName="txtEmailClient" >
                                                        <ValidationSettings Display="Dynamic" ErrorText="" ErrorTextPosition="Bottom">
                                                            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <RequiredField ErrorText="آدرس پست الکترونیکی را وارد نمایید"></RequiredField>
                                                            <RegularExpression ErrorText="آدرس را با فرمت صحیح وارد نمایید" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="توضیحات" ID="Label21"></asp:Label>
                                                </td>
                                                <td dir="rtl" valign="top" align="right" colspan="5">
                                                    <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtDescription" 
                                                        EnableClientSideAPI="True" Width="100%" ClientInstanceName="txtDescriptionClient"
                                                        >
                                                        <ValidationSettings>
                                                            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                            </ErrorImage>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomASPXMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="تصویر"  EnableClientSideAPI="True"
                                                        ID="lblChooseImg" ClientInstanceName="lblChooseImgClient" >
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" colspan="5">
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxUploadControl runat="server" ShowProgressPanel="True" MaxSizeForUploadFile="0"
                                                                        UploadWhenFileChoosed="true" ID="flpImg" InputType="Images" ClientInstanceName="flpImgClient"
                                                                        OnFileUploadComplete="flpImg_FileUploadComplete">
                                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
 if(e.isValid){
	imgEndUploadImgClient.SetVisible(true);
	img1.SetVisible(true);
	img1.SetImageUrl('../../Image/Temp/'+e.callbackData);
    }
else
 {
 	imgEndUploadImgClient.SetVisible(false);
	img1.SetVisible(false);
	img1.SetImageUrl('');
 }
}"></ClientSideEvents>
                                                                    </TSPControls:CustomAspxUploadControl>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                        ID="imgEndUploadImg" ClientInstanceName="imgEndUploadImgClient">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <dxe:ASPxLabel runat="server" ID="lblImg" ForeColor="Blue">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <br />
                                                    <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgPic" ClientInstanceName="img1"
                                                        Border-BorderWidth="1px" Border-BorderStyle="Solid">
                                                        <EmptyImage Height="100px" Width="100px" Url="~/Images/person.gif">
                                                        </EmptyImage>
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="تصویر امضا"  EnableClientSideAPI="True"
                                                        UploadWhenFileChoosed="true" ID="ASPxLabel1" ClientInstanceName="lblChooseImgClient"
                                                        >
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right" colspan="5">
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxUploadControl runat="server" ShowProgressPanel="True" MaxSizeForUploadFile="0"
                                                                        UploadWhenFileChoosed="true" ID="flpSign" InputType="Images" ClientInstanceName="flpSignClient"
                                                                        OnFileUploadComplete="flpSign_FileUploadComplete">
                                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
 if(e.isValid){
	imgEndUploadImgClientSign.SetVisible(true);
	hp.SetVisible(true);
	hp.SetNavigateUrl('../../Image/Temp/'+e.callbackData);
    }
else
    {
    imgEndUploadImgClientSign.SetVisible(false);
	hp.SetVisible(false);
	hp.SetNavigateUrl('');
    }
}" />
                                                                    </TSPControls:CustomAspxUploadControl>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                        ID="ASPxImage1" ClientInstanceName="imgEndUploadImgClientSign">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <dxe:ASPxHyperLink ID="HpSign" ClientVisible="false" runat="server" ClientInstanceName="hp"
                                                                        Text="تصویر" Target="_blank">
                                                                    </dxe:ASPxHyperLink>
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
                                                                            CausesValidation="False" ID="btnNew2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                                            EnableViewState="False" EnableTheming="False" ClientInstanceName="btnNewClient2"
                                                                            OnClick="btnNew_Click">
                                                                            <ClientSideEvents Click="function(s, e) {
	//SetNewMode();
}"></ClientSideEvents>
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                            </HoverStyle>
                                                                            <Image  Url="~/Images/icons/new.png">
                                                                            </Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                                            CausesValidation="False" Width="25px" ID="btnEdit2" EnableClientSideAPI="True"
                                                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnEditClient2"
                                                                            OnClick="btnEdit_Click">
                                                                            <ClientSideEvents Click="function(s, e) {
	
}
"></ClientSideEvents>
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                            </HoverStyle>
                                                                            <Image  Url="~/Images/icons/edit.png">
                                                                            </Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                                                            ID="btnSave2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                                            EnableTheming="False" ClientInstanceName="btnSaveClient2" OnClick="btnSave_Click">
                                                                            <ClientSideEvents Click="function(s, e) {

if(CheckCharacterEncoding(txtFirstNameEnClient.GetText())==false)
 {
txtFirstNameEnClient.SetIsValid(false);
txtFirstNameEnClient.SetErrorText('حروف وارد شده نامعتبر است');
	e.processOnServer=false;
}
if(CheckCharacterEncoding(txtLastNameEnClient.GetText())==false)
 {
txtLastNameEnClient.SetIsValid(false);
txtLastNameEnClient.SetErrorText('حروف وارد شده نامعتبر است');
	e.processOnServer=false;
}


}"></ClientSideEvents>
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                            </HoverStyle>
                                                                            <Image  Url="~/Images/icons/save.png">
                                                                            </Image>
                                                                        </TSPControls:CustomAspxButton>
                                                                    </td>
                                                                    <td>
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                                            CausesValidation="False" ID="btnBack2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                                            EnableViewState="False" EnableTheming="False" ClientInstanceName="btnBackClient2"
                                                                            OnClick="btnBack_Click">
                                                                            <HoverStyle BackColor="#FFE0C0">
                                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
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
                    <asp:ObjectDataSource ID="ObjectDataSourceDrpSex" runat="server" InsertMethod="Insert"
                        SelectMethod="GetData" TypeName="TSP.DataManager.SexManager" CacheDuration="30">
                      
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSourceDrpMar" runat="server" 
                        SelectMethod="GetData" TypeName="TSP.DataManager.MaritalStatusManager" 
                        OldValuesParameterFormatString="original_{0}">
                       
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSourceMun" runat="server" SelectMethod="GetData"
                        TypeName="TSP.DataManager.TechnicalServices.MunicipalityManager" CacheDuration="30"
                        OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                    <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
                    <asp:HiddenField ID="HDEmpId" runat="server" Visible="False"></asp:HiddenField>
       
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
            DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>

</asp:Content>
