<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddObservationDoc.aspx.cs" Inherits="Employee_Document_AddObservationDoc"
    Title="مشخصات مجوز ناظر حقیقی" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetCityControlValues() {           
            gridCity.GetRowValues(gridCity.GetFocusedRowIndex(), 'CitName;CitId;CitCode;AgentName;AgentCode;AgentAddress', SetCityValue);
        }

        function SetCityValue(values) {
            txtCity.SetText(values[0]);
            HiddenFieldDocMemberFile.Set('CitId', values[1]);
            HiddenFieldDocMemberFile.Set('CitCode', values[2]);
        }

        function SetMeDocDefualtExpireDateJS() {
            CallbackPanelDoRegDate.PerformCallback(cmbIsTemporary.GetValue());
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
                CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
                FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
            </pdc:PersianDateScriptManager>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#"><span style="color: #000000">ب</span>ستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                            <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click">

                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
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
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelMemberFile" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td style="width: 100%" dir="rtl" align="center">
                                        <dxe:ASPxLabel runat="server" Text="وضعیت درخواست:" Font-Bold="False" ID="lblWorkFlowState"
                                            ForeColor="Red">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <fieldset id="RoundPanelTransfer"
                            runat="server">
                            <legend class="HelpUL">مشخصات عضو</legend>

                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td width="15%" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد پیگیری" ID="lblFollowCode">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td width="35%" valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtFollowCode" Width="100%"
                                                Enabled="false">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td width="15%" valign="top" align="right"></td>
                                        <td width="35%" valign="top" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td width="15%" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="ASPxLabel2" ClientInstanceName="lblPr">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td width="35%" valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMeId" Width="100%" AutoPostBack="True"
                                                OnTextChanged="txtMeId_TextChanged">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="کد عضویت را با فرمت صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td style="vertical-align: middle" dir="rtl" align="center" colspan="2" rowspan="4">
                                            <dxe:ASPxImage runat="server" Height="75px" Width="75px" ImageUrl="~/Images/Person.png"
                                                ID="ImgMember">
                                            </dxe:ASPxImage>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel16">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="lblMeName" Width="100%" Enabled="false">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right"></td>
                                        <td valign="top" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="ASPxLabel18">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="lblMeLastName" Width="100%"
                                                Enabled="false">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right"></td>
                                        <td valign="top" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره پروانه" ID="ASPxLabel10" ClientInstanceName="lblDate">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="lblMFNo" Width="100%" Enabled="false"
                                                Style="direction: ltr">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="رشته موضوع پروانه" ID="ASPxLabel11">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMemberFileMajor" Width="100%"
                                                Enabled="false">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right"></td>
                                        <td valign="top" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" width="15%" align="right">
                                            <dxe:ASPxLabel runat="server" Text="پایه نظارت" ID="ASPxLabel9">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" width="35%" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtObsName" Width="100%" Enabled="false">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" width="15%" align="right">
                                            <dxe:ASPxLabel ID="ASPxLabel21" runat="server" Text="تاریخ اعتبار پروانه" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" width="35%" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Style="direction: ltr" Width="100%" Enabled="false"
                                                ID="txtExpireDateMember">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="پایه اجرا" ID="ASPxLabel1221">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtImpName" Width="100%" Enabled="false">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="پایه طراحی" ID="ASPxLabel12">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtDesName" Width="100%" Enabled="false">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="پایه شهرسازی" ID="ASPxLabel23">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtUrbenismName" Width="100%"
                                                Enabled="false">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="پایه نقشه برداری" ID="ASPxLabel24">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMappingName" Width="100%"
                                                Enabled="false">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="پایه ترافیک" ID="ASPxLabel25">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtTrafficName" Width="100%"
                                                Enabled="false">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right"></td>
                                        <td valign="top" align="right"></td>
                                    </tr>
                                </tbody>
                            </table>
                            <table runat="server" id="TblTransfer" width="100%" dir="rtl">
                                <tr>
                                    <td style="width: 15%" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="استان قبلی" ClientInstanceName="lblPr" Width="100%"
                                            ID="ASPxLabel13">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="width: 35%" valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"
                                            ID="lblPreProvince">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" style="width: 15%" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ انتقالی" Width="100%" ID="ASPxLabel14">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="width: 35%" valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"
                                            ID="lblTransferDate" Style="direction: ltr">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="شماره پروانه" Width="100%" ID="ASPxLabel20">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"
                                            ClientInstanceName="lblFileNo" ID="lblFileNo" Style="direction: ltr" RightToLeft="True">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="شماره عضویت" ClientInstanceName="lblMeNo" ID="ASPxLabel22">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="100%" Enabled="false"
                                            ID="lblPreMeNo">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="lblDocPr" runat="server" ClientInstanceName="lblDocPr" Text="استان صدور پروانه"
                                            Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox ID="ComboDocPr" runat="server" ClientInstanceName="ComboDocPr"
                                            DataSourceID="OdbProvince"
                                            TextField="PrName" ValueField="PrId" ValueType="System.String"
                                            Width="100%" RightToLeft="True" Enabled="false">
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                        <asp:ObjectDataSource ID="OdbProvince" runat="server" TypeName="TSP.DataManager.ProvinceManager"
                                            SelectMethod="GetData" CacheDuration="30" FilterExpression="NezamCode<>{0}">
                                            <FilterParameters>
                                                <asp:Parameter Name="newparameter" />
                                            </FilterParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel runat="server" Text="تصویر نامه انتقالی" ID="ASPxLabel38">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <dxe:ASPxHyperLink ID="HyperLinkTransfer" runat="server" Target="_blank" Text="تصویر نامه انتقالی">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </fieldset>
                        <fieldset id="RoundPanelCity"
                            runat="server">
                            <legend class="HelpUL">منطقه نظارت</legend>

                            <table id="tblRegion" runat="server" width="100%">
                                <tbody>
                                    <tr>
                                        <td width="15%" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شهرستان" ID="ASPxLabel8">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td width="35%" valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtRegionOfCity" Width="100%"
                                                Enabled="false">
                                                <ValidationSettings Display="Dynamic" ValidationGroup="ValidCity" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="شهرستان محل اقامت را وارد نمایید"></RequiredField>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td width="15%" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شهر" ID="ASPxLabel7">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td width="35%" valign="top" align="right">
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td width="80%" valign="top" align="right">
                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtCity" Width="100%" ClientEnabled="false"
                                                                ClientInstanceName="txtCity">
                                                                <ValidationSettings Display="Dynamic" ValidationGroup="ValidCity" ErrorTextPosition="Bottom">
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <RequiredField IsRequired="True" ErrorText="شهر را وارد نمایید"></RequiredField>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                        <td width="20%" valign="top" align="right">
                                                            <TSPControls:CustomAspxButton  runat="server" ToolTip="جستجو" CausesValidation="False"
                                                                ID="btnSearchCity" EnableClientSideAPI="True" AutoPostBack="False" UseSubmitBehavior="False"
                                                                EnableViewState="False" EnableTheming="False" Width="30px">
                                                                <ClientSideEvents Click="function(s, e) {
	popUpCity.Show();
}"></ClientSideEvents>
                                                                <Image Url="~/Images/icons/Search.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="center" colspan="4">
                                            <TSPControls:CustomAspxButton runat="server" Text="&nbsp;&nbsp;اضافه به لیست"
                                                ValidationGroup="ValidCity" ID="btnAddCity"
                                                OnClick="btnAddCity_Click" UseSubmitBehavior="false">
                                                <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td style="width: 100%" valign="top" align="center">
                                            <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                                                ID="GridViewCity" KeyFieldName="Id" AutoGenerateColumns="False"
                                                OnRowDeleting="GridViewCity_RowDeleting">
                                                <Styles>
                                                    <GroupPanel ForeColor="Black">
                                                    </GroupPanel>
                                                    <Header HorizontalAlign="Center">
                                                    </Header>
                                                </Styles>
                                                <Settings ShowGroupPanel="True" ShowFilterRowMenu="True" ShowFilterRow="True"></Settings>
                                                <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="CitCode" Caption="کد شهر">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="CitName" Caption="شهر">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowClearFilterButton="true" ShowDeleteButton="true">
                                                    </dxwgv:GridViewCommandColumn>
                                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="Id">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                            </TSPControls:CustomAspxDevGridView2>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
                        <dxcp:ASPxPanel ID="RoundPanelObsDoc"
                            runat="server">
                            <PanelCollection>
                                <dxcp:PanelContent>
                                    <fieldset>
                                        <legend class="HelpUL">مشخصات مجوز نظارت</legend>

                                        <TSPControls:CustomAspxCallbackPanel runat="server"
                                            ClientInstanceName="CallbackPanelDoRegDate" Width="100%" ID="CallbackPanelDoRegDate"
                                            OnCallback="CallbackPanelDoRegDate_Callback">
                                            <PanelCollection>
                                                <dxp:PanelContent ID="PanelContent11" runat="server">
                                                    <table id="Table1" width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td width="15%" valign="top" align="right">
                                                                    <dxe:ASPxLabel runat="server" Text="شماره مجوز" ID="ASPxLabel3" ClientInstanceName="lblDate">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td width="35%" valign="top" align="right">
                                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtMfNoObs" Width="100%" Enabled="false"
                                                                        Style="direction: ltr">
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td width="15%" valign="top" align="right">
                                                                    <dxe:ASPxLabel runat="server" Text="محل صدور مجوز" ID="ASPxLabel4" ClientInstanceName="lblFileNo">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td width="35%" valign="top" align="right">
                                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtProvinceNameObs" Width="100%"
                                                                        Enabled="false">
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="15%" valign="top" align="right">
                                                                    <dxe:ASPxLabel runat="server" Text="پایه" ID="ASPxLabel19" ClientInstanceName="lblFileNo">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td width="15%" valign="top" align="right" colspan="3">
                                                                    <TSPControls:CustomASPXMemo runat="server" Height="37px" ID="txtGradeObs" Width="100%"
                                                                        Enabled="false">
                                                                    </TSPControls:CustomASPXMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="15%" valign="top" align="right">
                                                                    <dxe:ASPxLabel runat="server" Visible="false" Text="تاریخ صدور" ID="ASPxLabel5" ClientInstanceName="lblFileNo">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td width="35%" valign="top" align="right">
                                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Visible="false" ID="txtRegDateObs"
                                                                        Width="100%" Enabled="false">
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td width="15%" valign="top" align="right"></td>
                                                                <td width="35%" valign="top" align="right"></td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="right">
                                                                    <dxe:ASPxLabel runat="server" Text="موقت/دائم" ID="ASPxLabel1">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td valign="top" align="right">
                                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                        ID="cmbIsTemporary" ClientInstanceName="cmbIsTemporary" ValueType="System.String">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                            <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                            </ErrorImage>
                                                                            <RequiredField IsRequired="True" ErrorText="نوع مجوز را انتخاب نمایید"></RequiredField>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                        <Items>
                                                                            <dxe:ListEditItem Value="0" Text="پروانه دائم"></dxe:ListEditItem>
                                                                            <dxe:ListEditItem Value="1" Text="پروانه موقت"></dxe:ListEditItem>
                                                                        </Items>
                                                                        <ButtonStyle Width="13px">
                                                                        </ButtonStyle>
                                                                    </TSPControls:CustomAspxComboBox>
                                                                </td>
                                                                <td valign="top" align="right">
                                                                    <dxe:ASPxLabel runat="server" Text="شماره سریال" ID="ASPxLabel6">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td valign="top" align="right">
                                                                    <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtSerialNo" Width="100%"
                                                                        MaxLength="7">
                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                            </ErrorImage>
                                                                            <RegularExpression ErrorText="شماره سریال را با فرمت صحیح وارد نمایید" ValidationExpression="\d*"></RegularExpression>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="right">
                                                                    <dxe:ASPxLabel runat="server" Text="تاریخ صدور" ID="ASPxLabel15">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td valign="top" align="right">
                                                                    <table>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td valign="top" align="right">
                                                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="230px" ShowPickerOnTop="True"
                                                                                        ID="txtLastRegDateObs" PickerDirection="ToRight" ShowPickerOnEvent="OnClick"
                                                                                        IconUrl="~/Image/Calendar.gif" onchange="return SetMeDocDefualtExpireDateJS();"></pdc:PersianDateTextBox>
                                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastRegDateObs"
                                                                                        ID="RequiredFieldValidator1">تاریخ شروع را وارد نمایید</asp:RequiredFieldValidator>
                                                                                </td>
                                                                                <td valign="top" align="right">
                                                                                    <dxe:ASPxImage ID="btnSetRegDate" ClientInstanceName="btnSetRegDate" ToolTip="تنظیم تاریخ اعتبار"
                                                                                        runat="server" Text="" Height="13px" Border-BorderWidth="1px" Border-BorderColor="LightBlue"
                                                                                        Width="13px" Image-Height="13px" Image-Width="13px" ImageUrl="~/Images/ResetDate.png">
                                                                                        <ClientSideEvents Click="function(s, e) { SetMeDocDefualtExpireDateJS(); }" />
                                                                                    </dxe:ASPxImage>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                                <td valign="top" align="right">
                                                                    <dxe:ASPxLabel runat="server" Text="تاریخ پایان اعتبار" ID="ASPxLabel17">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td valign="top" align="right">
                                                                    <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick" Width="230px"
                                                                        ShowPickerOnTop="True" ID="txtExpDateObs" PickerDirection="ToRight" RightToLeft="False"
                                                                        IconUrl="~/Image/Calendar.gif" Style="direction: ltr;"></pdc:PersianDateTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </TSPControls:CustomAspxCallbackPanel>
                                    </fieldset>
                                </dxcp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxPanel>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldDocMemberFile" ClientInstanceName="HiddenFieldDocMemberFile">
            </dxhf:ASPxHiddenField>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click" CausesValidation="False">

                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td dir="ltr">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click">

                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" OnClick="btnBack_Click" runat="server" Text=" " EnableTheming="False"
                                            EnableViewState="False" UseSubmitBehavior="False" CausesValidation="False" ToolTip="بازگشت">
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
            <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl2" runat="server" ClientInstanceName="popUpCity"
                HeaderText="جستجو" Height="40%" PopupElementID="btnSearch1" Width="40%" PopupHorizontalAlign="Center" PopupVerticalAlign="WindowCenter">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">
                        <div dir="rtl">
                            <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                                ClientInstanceName="gridCity"
                                DataSourceID="ObjdsCity" EnableViewState="False" KeyFieldName="CitId"
                                Width="100%">
                                <ClientSideEvents RowDblClick="function(s, e) {
	SetCityControlValues();
	popUpCity.Hide();
}" />
                                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="شهر" FieldName="CitName" Name="CitName" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="نام نمایندگی" FieldName="AgentName" Name="AgentName"
                                        VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="کد نمایندگی" FieldName="AgentCode" Name="AgentCode"
                                        VisibleIndex="2" Width="150px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" Width="1px">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView>
                        </div>
                        <asp:ObjectDataSource ID="ObjdsCity" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
                            OldValuesParameterFormatString="original_{0}" SelectMethod="SelectByReCitId"
                            TypeName="TSP.DataManager.CityManager" UpdateMethod="Update">
                            <InsertParameters>
                                <asp:Parameter Name="CitId" Type="Int32" />
                                <asp:Parameter Name="CitCode" Type="String" />
                                <asp:Parameter Name="CitName" Type="String" />
                                <asp:Parameter Name="PrId" Type="Int32" />
                                <asp:Parameter Name="UserId" Type="Int32" />
                                <asp:Parameter Name="ModifiedDate" Type="DateTime" />
                            </InsertParameters>
                            <SelectParameters>
                                <asp:Parameter DefaultValue="-1" Name="ReCitId" Type="Int32" />
                            </SelectParameters>
                            <DeleteParameters>
                                <asp:Parameter Name="Original_CitId" Type="Int32" />
                                <asp:Parameter Name="Original_LastTimeStamp" Type="Object" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="CitId" Type="Int32" />
                                <asp:Parameter Name="CitCode" Type="String" />
                                <asp:Parameter Name="CitName" Type="String" />
                                <asp:Parameter Name="PrId" Type="Int32" />
                                <asp:Parameter Name="UserId" Type="Int32" />
                                <asp:Parameter Name="ModifiedDate" Type="DateTime" />
                                <asp:Parameter Name="Original_CitId" Type="Int32" />
                                <asp:Parameter Name="Original_LastTimeStamp" Type="Object" />
                            </UpdateParameters>
                        </asp:ObjectDataSource>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>

            </TSPControls:CustomASPxPopupControl>
            <asp:ObjectDataSource ID="ObjdsMajor" runat="server" TypeName="TSP.DataManager.MajorManager"
                SelectMethod="FindMajorParent">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="MjId"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsProvince" runat="server" TypeName="TSP.DataManager.ProvinceManager"
                SelectMethod="GetData"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsMemberLicence" runat="server" TypeName="TSP.DataManager.MemberLicenceManager"
                SelectMethod="SelectByMemberId" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="MemberId"></asp:Parameter>
                    <asp:Parameter DefaultValue="0" Name="InActive" Type="Int32" />
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
