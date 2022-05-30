<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="ProjectInsert.aspx.cs" Inherits="Employee_TechnicalServices_Project_ProjectInsert"
    Title="مشخصات پروژه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="../../../UserControl/WFUserControl.ascx" TagName="WFUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetMaxStageNum() {
            var maxstagenum = parseInt(txtStage.GetText());
            var StageNum = parseInt(txtStageNum.GetText());
            if (maxstagenum < StageNum) {
                txtStage.SetText(txtStageNum.GetText());
            }
            else {

                txtStage.SetText(parseInt(HFCroquis.Get('MaxStageNum')));
            }

        }
        function SetCompareFundationAndSteplbl() {
            lblCompareChangesProjectFoundation.SetText(TextResult(HiddenFieldPage.Get('LastFundation'), txtProjectFoundation.GetValue()));
        }
        function SetCompareSteplbl() {
            lblCompareChangesStageNum.SetText(TextResult(HiddenFieldPage.Get('LastStageNum'), txtStageNum.GetValue()));
        }

        function TextResult(LastValue, CurrentValue) {
            var t = "مقدار قبلی:" + LastValue + "   تفاوت:" + (Math.abs(CurrentValue - LastValue))
            return t;
        }
    </script>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                visible="false">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0" align="right">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="btnNew" UseSubmitBehavior="False" EnableViewState="true"
                                            EnableTheming="False" OnClick="btnNew_Click">
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                            EnableViewState="true" EnableTheming="False" OnClick="btnEdit_Click">
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave" UseSubmitBehavior="False" EnableViewState="true" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if(HiddenFieldPage.Get('RequestType')==6 && HFCroquis.Get('BuildingLicence')!=1)
	{
		lblBuildingLicence.SetVisible(true);
		e.processOnServer=false;
    }
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/save.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                        </TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار پروژه"
                                            ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="true"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/reload.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4">
                                        </TSPControls:MenuSeprator>
                                    </td>

                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="true"
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
            <TSPControls:CustomAspxMenuHorizontal ID="MainMenu" runat="server" CssClass="ProjectMainMenuHorizontal" OnItemClick="MainMenu_ItemClick">
                <Items>
                    <dxm:MenuItem Text="مشخصات پروژه" Name="Project" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مالک" Name="Owner" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مالی پروژه" Name="Accounting" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                        <Items>
                            <%--  <dxm:MenuItem Text="مالی مالکان" Name="AccOwner">
                            </dxm:MenuItem>--%>
                            <dxm:MenuItem Text="مالی طراحان" Name="AccDesigner" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                            </dxm:MenuItem>
                            <%--           <dxm:MenuItem Text="مالی ناظران" Name="AccObserver">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="مالی مجریان" Name="AccImp">
                            </dxm:MenuItem>--%>
                        </Items>
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="نقشه" Name="Plans" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="طراح" Name="Designer" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="ناظر" Name="Observers" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مجری" Name="Implementer" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="قرارداد" Name="Contract" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                    </dxm:MenuItem>
                    <%--  <dxm:MenuItem Text="زمان بندی" Name="Timing">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="پروانه ساخت" Name="BuildingsLicense">
                    </dxm:MenuItem>
                     <dxm:MenuItem Text="اعلام وضعیت" Name="StatusAnnouncement" >
                    </dxm:MenuItem>--%>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>
            <br />
            <TSPControls:CustomAspxMenuHorizontal ID="ProjectMenu" runat="server" OnItemClick="ProjectMenu_ItemClick" CssClass="ProjectSubMenuHorizontal">
                <ItemStyle HorizontalAlign="Right" Font-Size="X-Small" Font-Bold="true" />
                <Items>
                    <dxm:MenuItem Text="اطلاعات پایه" Name="BaseInfo" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="پلاک ثبتی" Name="RegisteredNo" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="دستور نقشه" Name="PlansMethod" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="بلوک" Name="Block" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="بیمه" Name="Insurance" ItemStyle-CssClass="ProjectSubMenuItemStyle">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>

            <TSPControls:CustomASPxRoundPanel ID="RoundPanelProjectInfo" HeaderText="مشاهده"
                runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent>
                        <table width="100%" dir="rtl">
                            <tbody>
                                <tr>
                                    <td align="center" valign="top" colspan="4">
                                        <dxe:ASPxLabel runat="server" Text="وضعیت درخواست:" Font-Bold="False" ID="lblWorkFlowState"
                                            ForeColor="Red">
                                        </dxe:ASPxLabel>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table dir="rtl" width="100%">
                            <tr>
                                <td width="5%" align="left">
                                    <blink id="bkImgWarningMsg"><dxe:ASPxImage ID="ImgWarningMsg" ClientVisible="false" Width="20px" Height="20px" runat="server" ImageUrl="~/Images/Errors-64.png">
                                    </dxe:ASPxImage></blink>
                                </td>
                                <td width="95%" align="right">
                                    <asp:Label ID="lblWarningText" Font-Bold="true" CssClass="HelpUL" runat="server"
                                        Font-Size="11px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="5%" align="left">
                                    <blink id="bkImgWarningCountIngrediant"><dxe:ASPxImage ID="ImgWarningCountIngrediant" ClientVisible="false" Width="20px" Height="20px" runat="server" ImageUrl="~/Images/Errors-64.png">
                                    </dxe:ASPxImage></blink>
                                </td>
                                <td width="95%" align="right">

                                    <asp:Label ID="lblCountIngrediant" Font-Bold="true" CssClass="HelpUL" Visible="false" runat="server"
                                        Font-Size="11px"></asp:Label>
                                </td>

                            </tr>

                        </table>
                        <fieldset>
                            <legend class="HelpUL">مشخصات اصلی پروژه</legend>

                            <table width="100%">
                                <tr>
                                    <td align="center" valign="top" colspan="4">
                                        <ul class="HelpUL">

                                            <li>جهت تکمیل کلیه اطلاعات پروژه می توانید از ''دستور نقشه'' استفاده نمایید. </li>
                                            <li>در صورتی که پروژه فاقد پلاک ثبتی می باشد هیچ مقداری را ثبت ننمایید(از نوشتن واژه
                                                                فاقد پلاک ثبتی جددا خودداری نمایید.) </li>
                                            <li>در صورتی که پروژه دارای چند پلاک ثبتی می باشد ، پلاک ثبتی اصلی را در این صفحه ثبت
                                                                نموده و سایر پلاک های ثبتی را از طریق صفحه مدیریت پلاک های ثبتی ، ثبت نمایید.
                                            </li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right" width="15%">نوع درخواست</td>
                                    <td valign="top" align="right" width="35%">
                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%" Enabled="False"
                                            TextField="PrjReTypeTittle" ID="ComboBoxProjectRequestType"
                                            DataSourceID="ObjectDataSourceProjectRequestType" ValueType="System.Int32" ValueField="PrjReTypeId"
                                            RightToLeft="True" EnableIncrementalFiltering="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                        <asp:ObjectDataSource ID="ObjectDataSourceProjectRequestType" runat="server" SelectMethod="SelectProjectRequestTypeForProjectRequest"
                                            TypeName="TSP.DataManager.TechnicalServices.ProjectRequestTypeManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                                    </td>
                                    <td valign="top" align="right" width="15%"></td>
                                    <td valign="top" align="right" width="35%"></td>
                                </tr>
                            </table>
                            <dx:ASPxPanel runat="server" ID="pnlMainInformation">
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <table width="100%">
                                            <tr>
                                                <td valign="top" align="right" width="15%">شناسه دسترسی طراح
                                                </td>
                                                <td valign="top" align="right" width="35%">
                                                    <TSPControls:CustomTextBox runat="server" Enabled="false" ID="txtTraceCode" Width="100%">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right" width="15%">شناسه پروژه</td>
                                                <td valign="top" align="right" width="35%">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtProjectNo" Width="100%" Enabled="false">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right" width="15%">
                                                    <asp:Label runat="server" Text="کد پروژه" ID="Label2"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" width="35%">
                                                    <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxCode" Width="100%"
                                                        Enabled="False">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField ErrorText=""></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right" width="15%">
                                                    <asp:Label runat="server" Text="تاریخ ثبت درخواست" ID="Label47"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" width="35%">
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="300px" Enabled="False"
                                                        ShowPickerOnTop="True" ID="RegDate" PickerDirection="ToRight" RightToLeft="False"
                                                        IconUrl="~/Image/Calendar.gif" ShowPickerOnEvent="OnClick" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RegDate" ID="RequiredFieldValidatorRegDate">تاریخ ثبت  را وارد نمایید</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="وضعیت پروژه" ID="Label51"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%" Enabled="False"
                                                        TextField="Status" ID="ASPxComboBoxProjectStatus"
                                                        DataSourceID="ObjectDataSourceProjectStatus" ValueType="System.Int32" ValueField="ProjectStatusId"
                                                        RightToLeft="True" EnableIncrementalFiltering="True">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>
                                                    <asp:ObjectDataSource ID="ObjectDataSourceProjectStatus" runat="server" SelectMethod="GetData"
                                                        TypeName="TSP.DataManager.TechnicalServices.ProjectStatusManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="نوع پروژه" ID="Label48"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox runat="server"
                                                        TextField="NameDiscountPercent" ID="ASPxComboBoxDiscountPercent" AutoPostBack="True" DataSourceID="ObjectDataSourceDiscountPercent"
                                                        ValueType="System.Int32" Width="100%" ValueField="DiscountPercentId"
                                                        EnableIncrementalFiltering="True" RightToLeft="True" OnSelectedIndexChanged="ASPxComboBoxDiscountPercent_SelectedIndexChanged">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="لطفاً  نوع پروژه را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="کسر ظرفیت (%)" ID="Label49"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxDecrementPercent"
                                                        Width="100%" Enabled="False">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField ErrorText=""></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="دستمزد (%)" ID="Label50"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxWagePercent" Width="100%"
                                                        Enabled="False">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField ErrorText=""></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="نمایندگی" ID="Label1"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                        TextField="Name" ID="ASPxComboBoxAgent" AutoPostBack="True" DataSourceID="ObjectdatasourceAgent"
                                                        ValueType="System.Int32" ValueField="AgentId" RightToLeft="True"
                                                        EnableIncrementalFiltering="True">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="لطفاً نمایندگی را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>
                                                </td>
                                                <td valign="top" align="right" width="15%"></td>
                                                <td valign="top" align="right" width="35%"></td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="شهر*" ID="Label35"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox runat="server"
                                                        TextField="CitName" ID="ASPxComboBoxCity" AutoPostBack="True" DataSourceID="ObjectDataSourceCity"
                                                        ValueType="System.Int32" ValueField="CitId"
                                                        EnableIncrementalFiltering="True" Width="100%" RightToLeft="True" OnSelectedIndexChanged="ASPxComboBoxCity_SelectedIndexChanged">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="لطفاً شهر را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>
                                                    <asp:ObjectDataSource ID="ObjectDataSourceCity" runat="server" SelectMethod="SelectProjectUserRightDataTableByLoginId"
                                                        TypeName="TSP.DataManager.TechnicalServices.ProjectUserRightManager">
                                                        <SelectParameters>
                                                            <asp:Parameter DefaultValue="-1" Name="LoginId" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="شهرداری*" ID="Label9"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                        TextField="MunName" ID="ASPxComboBoxMunicipality" DataSourceID="ObjectdatasourceMunicipality"
                                                        ValueType="System.Int32" ValueField="MunId"
                                                        EnableIncrementalFiltering="True" RightToLeft="True">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <RequiredField IsRequired="True" ErrorText="لطفاً شهرداری را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>
                                                    <asp:ObjectDataSource runat="server" SelectMethod="SelectChilds" ID="ObjectdatasourceMunicipality"
                                                        TypeName="TSP.DataManager.TechnicalServices.MunicipalityManager" OldValuesParameterFormatString="original_{0}">
                                                        <SelectParameters>
                                                            <asp:ControlParameter PropertyName="Value" Type="Int32" DefaultValue="0" Name="CitId"
                                                                ControlID="ASPxComboBoxCity"></asp:ControlParameter>
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="آدرس" ID="Label8"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" colspan="3">
                                                    <TSPControls:CustomASPXMemo runat="server" Height="55px" ID="TextBoxAddress"
                                                        Width="100%">
                                                    </TSPControls:CustomASPXMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="نوع کاربری" ID="Label19"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                        TextField="Title" ID="ASPxComboBoxUsage" DataSourceID="ObjectdatasourceUsage"
                                                        ValueType="System.Int32" ValueField="UsageId"
                                                        EnableIncrementalFiltering="True" RightToLeft="True">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="نوع مالکیت" ID="Label5"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                        TextField="OwnershipTypeName" ID="cmbOwnerShipType" DataSourceID="ObjectOwnerShipType"
                                                        ValueType="System.Int32" ValueField="OwnershipTypeId"
                                                        EnableIncrementalFiltering="True" RightToLeft="True">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <%--<RequiredField IsRequired="True" ErrorText="لطفاً نوع مالکیت را انتخاب نمایید"></RequiredField>--%>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomAspxComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right" style="width: 15%">قطعه
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtMainSection" Width="100%"
                                                        RightToLeft="True">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right" style="width: 15%">ناحیه
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtMainRegion" Width="100%"
                                                        RightToLeft="True">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right" style="width: 15%">
                                                    <asp:Label runat="server" Text="شماره پلاک ثبتی اصلی" ID="Label6" Width="100%"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtRegisteredNo" Width="100%"
                                                        RightToLeft="True">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">

                                                    <fieldset>
                                                        <legend class="HelpUL">گروه ساختمانی</legend>
                                                        <asp:Panel runat="server" ID="pnlMaxBlock">
                                                            <table width="100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="right" valign="top" colspan="4">
                                                                            <ul class="HelpUL">

                                                                                <li>گروه ساختمانی بر اساس بیشترین تعداد طبقات ثبت شده در قسمت بلوک و متراژ زیربنا محاسبه
                                                                                                می گردد. </li>
                                                                                <li>در این صفحه اطلاعات اولین بلوک ثبت شده برای این پروژه را نمایش می دهد. </li>
                                                                                <li>در صورتی که پروژه دارای بیش از یک بلوک می باشد،از طریق صفحه مدیریت بلوک ها اقدام
                                                                                                نمایید. </li>
                                                                                <li><b>برای ساختمان های بالای دو طبقه نوع اسکلت نمی تواند بنایی باشد</b></li>

                                                                            </ul>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" align="right" width="15%">
                                                                            <dxe:ASPxLabel runat="server" Text="بیشترین تعداد طبقات" ID="ASPxLabel6" Width="100%"
                                                                                Wrap="False" ClientVisible="false">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td valign="top" align="right" width="35%">
                                                                            <TSPControls:CustomTextBox ReadOnly="true" runat="server" ID="txtStage" ClientInstanceName="txtStage"
                                                                                Width="100%"
                                                                                AutoPostBack="true" ClientVisible="false">
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                        <td width="15%"></td>
                                                                        <td width="35%"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" align="right">
                                                                            <asp:Label runat="server" Text="زیر بنای کل (متر مربع)*" ID="Label24"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtProjectFoundation" ClientInstanceName="txtProjectFoundation" Width="100%"
                                                                                AutoPostBack="true" OnTextChanged="txtProjectFoundation_TextChanged" ClientSideEvents-KeyUp="SetCompareFundationAndSteplbl">
                                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                    <RequiredField IsRequired="True" ErrorText="لطفاً زیر بنا را وارد نمایید"></RequiredField>
                                                                                    <RegularExpression ErrorText="لطفا عدد صحیحی را در نهایت تا 2 رقم اعشار وارد کنید" ValidationExpression="(([1-9])+([0-9])*)(\.\d{1,2})?|([0]{1})((\.\d{1,2}))"></RegularExpression>
                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>
                                                                            </TSPControls:CustomTextBox>
                                                                            <dxcp:ASPxLabel runat="server" ID="lblCompareChangesProjectFoundation" ClientInstanceName="lblCompareChangesProjectFoundation" CssClass="ComperObserverRequest"></dxcp:ASPxLabel>
                                                                        </td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>تعداد طبقات از روی شالوده
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomTextBox runat="server" ID="txtStageNum" ClientInstanceName="txtStageNum"
                                                                                Width="100%" OnTextChanged="txtProjectFoundation_TextChanged" ClientSideEvents-KeyUp="SetCompareSteplbl">
                                                                                <MaskSettings Mask="&lt;0..1000&gt;"></MaskSettings>
                                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                                                    <RequiredField IsRequired="True" ErrorText="لطفاً تعداد طبقات  را وارد نمایید"></RequiredField>
                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>
                                                                                <ClientSideEvents TextChanged="function(s,e){   
                                                                                            SetMaxStageNum();                                                                                         
                                                                                            if(txtBlockFoundation.GetText()=='')
                                                                                            {
                                                                                            e.processOnServer=false;
                                                                                            
                                                                                            }
                                                                                            else  e.processOnServer=true;
                                                                                            }" />
                                                                            </TSPControls:CustomTextBox>

                                                                            <dxcp:ASPxLabel runat="server" ID="lblCompareChangesStageNum" ClientInstanceName="lblCompareChangesStageNum" CssClass="ComperObserverRequest"></dxcp:ASPxLabel>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <asp:Label runat="server" Text="گروه ساختمانی*" ID="LblGroup"></asp:Label>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                                TextField="GroupName" ID="ASPxComboBoxStructureGroups" DataSourceID="ObjectdatasourceStructureGroups"
                                                                                ValueType="System.Int32" ValueField="GroupId"
                                                                                RightToLeft="True" EnableIncrementalFiltering="True">
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                    <RequiredField IsRequired="True" ErrorText="لطفاً گروه ساختمانی را انتخاب نمایید"></RequiredField>
                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>
                                                                                <ButtonStyle Width="13px">
                                                                                </ButtonStyle>
                                                                            </TSPControls:CustomAspxComboBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top" align="right">
                                                                            <dxe:ASPxLabel runat="server" Text="نوع اسکلت*" ID="Label31">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td dir="ltr" valign="top" align="right">
                                                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                                TextField="Title" ID="ComboBoxStructureSkeleton" AutoPostBack="True" DataSourceID="ObjectdatasourceStructureSkeleton"
                                                                                ValueType="System.Int32" ValueField="StructureSkeletonId" OnSelectedIndexChanged="ComboBoxStructureSkeleton_SelectedIndexChanged"
                                                                                EnableIncrementalFiltering="True"
                                                                                ClientInstanceName="ComboBoxStructureSkeleton" RightToLeft="True">
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                    <RequiredField IsRequired="True" ErrorText="لطفاً نوع اسکلت را انتخاب نمایید"></RequiredField>
                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>
                                                                            </TSPControls:CustomAspxComboBox>
                                                                            <asp:ObjectDataSource ID="ObjectdatasourceStructureSkeleton" runat="server" SelectMethod="GetData"
                                                                                TypeName="TSP.DataManager.TechnicalServices.StructureSkeletonManager"></asp:ObjectDataSource>
                                                                        </td>
                                                                        <td valign="top" align="right">
                                                                            <asp:Label runat="server" Text="نوع سقف" ID="Label33"></asp:Label>
                                                                        </td>
                                                                        <td dir="ltr" valign="top" align="right">
                                                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                                TextField="Title" ID="ComboBoxRoofType" DataSourceID="ObjectdatasourceRoofType"
                                                                                ValueType="System.Int32" ValueField="RoofTypeId"
                                                                                EnableIncrementalFiltering="True"
                                                                                ClientInstanceName="ComboBoxRoofType" RightToLeft="True">
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </TSPControls:CustomAspxComboBox>
                                                                            <asp:ObjectDataSource ID="ObjectdatasourceRoofType" runat="server" SelectMethod="GetData"
                                                                                TypeName="TSP.DataManager.TechnicalServices.RoofTypeManager"></asp:ObjectDataSource>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxLabel runat="server" Text="متراژ طراح سازه*" ClientVisible="false" ClientInstanceName="lblSazehTarkibiFoundation" ID="lblSazehTarkibiFoundation">
                                                                            </dxe:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <TSPControls:CustomTextBox runat="server" ClientVisible="false" ID="txtSazehTarkibiFoundation" ClientInstanceName="txtSazehTarkibiFoundation"
                                                                                Width="100%">
                                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                    <RequiredField IsRequired="True" ErrorText="لطفاً متراژ قسمت نیاز به سازه را وارد نمایید"></RequiredField>
                                                                                    <RegularExpression ErrorText="لطفا عدد صحیحی را در نهایت تا 2 رقم اعشار وارد کنید" ValidationExpression="(([1-9])+([0-9])*)(\.\d{1,2})?|([0]{1})((\.\d{1,2}))"></RegularExpression>
                                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                    </ErrorFrameStyle>
                                                                                </ValidationSettings>
                                                                            </TSPControls:CustomTextBox>
                                                                        </td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </fieldset>

                                                </td>
                                            </tr>
                                        </table>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxPanel>
                            <table width="100%">
                                <tr>
                                    <td colspan="4">
                                        <dx:ASPxPanel runat="server" ID="PanelOwnerInfo">
                                            <PanelCollection>
                                                <dx:PanelContent>
                                                    <fieldset>
                                                        <legend class="HelpUL">مشخصات مالک</legend>
                                                        <asp:Panel runat="server" ID="Panel1">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td valign="top" align="right" style="width: 15%">
                                                                        <dxe:ASPxLabel runat="server" Text="نوع مالک" ID="ASPxLabel3" Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td valign="top" align="right" style="width: 35%">
                                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                                            ID="CmbType" ValueType="System.String" SelectedIndex="0" ClientInstanceName="cmb"
                                                                            RightToLeft="True">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {

	if(cmb.GetValue() == '1')
		{
        
        lbloFirstName.SetText('نام نماینده مالکین');
        lblOwnerLastName.SetVisible(true);
        txtOwnerLastName.SetVisible(true);
        txtSSN.SetVisible(true);
        lblSSN.SetVisible(true);
        }
	else
		{
        lbloFirstName.SetText('نام سازمان');
        lblOwnerLastName.SetVisible(false);
        txtOwnerLastName.SetVisible(false);
        txtSSN.SetVisible(false);
        lblSSN.SetVisible(false);
        }
}"></ClientSideEvents>
                                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                <RequiredField IsRequired="True" ErrorText="نوع مالک را انتخاب نمایید"></RequiredField>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                            <Items>
                                                                                <dxe:ListEditItem Value="1" Text="دیگر اشخاص" Selected="True"></dxe:ListEditItem>
                                                                                <dxe:ListEditItem Value="2" Text="سازمان"></dxe:ListEditItem>
                                                                            </Items>
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                        </TSPControls:CustomAspxComboBox>
                                                                    </td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>

                                                                    <td valign="top" align="right" width="15%">
                                                                        <dxe:ASPxLabel runat="server" Text="نام نماینده مالکین" ID="lbloFirstName" ClientInstanceName="lbloFirstName"
                                                                            Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td valign="top" align="right" width="35%">
                                                                        <TSPControls:CustomTextBox runat="server" ID="txtOwnerFirstName" Width="100%"
                                                                            ClientInstanceName="oFirstName">

                                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>
                                                                    </td>
                                                                    <td valign="top" align="right" width="15%">
                                                                        <dxe:ASPxLabel runat="server" Text="نام خانوادگی  نماینده مالکین" Width="100%" ID="lblOwnerLastName"
                                                                            ClientInstanceName="lblOwnerLastName">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td valign="top" align="right" width="35%">
                                                                        <TSPControls:CustomTextBox runat="server" ID="txtOwnerLastName" Width="100%"
                                                                            ClientInstanceName="txtOwnerLastName">

                                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                                <RequiredField IsRequired="True" ErrorText="نام خانوادگی را وارد نمایید"></RequiredField>
                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="right">
                                                                        <dxe:ASPxLabel runat="server" Text="شماره همراه مالک" ID="ASPxLabel7" Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <TSPControls:CustomTextBox runat="server" ID="txtOwnerMobileNo" Width="100%"
                                                                            MaxLength="11" ClientInstanceName="txtOwnerMobileNo">

                                                                            <ValidationSettings Display="Dynamic" EnableCustomValidation="True" ErrorTextPosition="Bottom">
                                                                                <RequiredField IsRequired="true" ErrorText="شماره همراه مالک جهت بازیابی رمزعبور مالک و ارسال پیام کوتاه الزامی می باشد"></RequiredField>
                                                                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="(0)\d{10}"></RegularExpression>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <dxe:ASPxLabel runat="server" Text="کد ملی مالک" ID="lblSSN" ClientInstanceName="lblSSN" Width="100%">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <TSPControls:CustomTextBox runat="server" ID="txtSSN" Width="100%"
                                                                            MaxLength="10" ClientInstanceName="txtSSN">

                                                                            <ValidationSettings Display="Dynamic" ErrorText="ساختار کد ملی وارد شده صحیح نمی باشد" EnableCustomValidation="True" ErrorTextPosition="Bottom">
                                                                                <RequiredField IsRequired="true" ErrorText="ورود کد ملی مالک الزامی می باشد"></RequiredField>

                                                                            </ValidationSettings>
                                                                            <ClientSideEvents Validation="onIranianNationalCodeValidation" />
                                                                        </TSPControls:CustomTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>

                                                    </fieldset>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxPanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right" width="15%">کد بایگانی
                                    </td>
                                    <td valign="top" align="right" width="35%">
                                        <TSPControls:CustomTextBox runat="server" ID="txtArchiveNo" Width="100%">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right" width="15%">
                                        <asp:Label runat="server" Text="نام پروژه" ID="Label3"></asp:Label>
                                    </td>
                                    <td valign="top" align="right" width="35%">
                                        <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxProjectName" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>

                                </tr>

                                <tr>
                                    <td valign="top" align="right" width="15%">
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel2">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" colspan="3" valign="top">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" ID="txtReqDesc">
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right" colspan="4">
                                        <TSPControls:CustomASPxCheckBox runat="server" ID="chbHasDesigner" Width="100%"
                                            Text="با آگاهی کامل نسبت به قوانین سازمان و مبحث دوم مقررات ملی ساختمان درخواست ثبت طراحان و عوامل پروژه بدون ثبت طراح نقشه معماری را دارم." Checked="false">
                                        </TSPControls:CustomASPxCheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right" colspan="4">
                                        <TSPControls:CustomASPxCheckBox runat="server" ID="chbHasObserver" Width="100%"
                                            Text="با آگاهی کامل نسبت به قوانین سازمان درخواست ثبت پروژه بدون ناظران و فیش نظارت را دارم." Checked="false">
                                        </TSPControls:CustomASPxCheckBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <dx:ASPxPanel runat="server" ID="PanelSubInfo">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <fieldset>
                                        <legend class="HelpUL">مشخصات فرعی پروژه</legend>
                                        <table width="100%">
                                            <tr>
                                                <td valign="top" align="right" style="width: 15%">
                                                    <asp:Label runat="server" Text="شماره پرونده" ID="Label12"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxFileNo" Width="100%">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right" style="width: 15%">
                                                    <asp:Label runat="server" Text="تاریخ پرونده" ID="Label13"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="300px" ShowPickerOnTop="True"
                                                        ID="FileDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                                        Style="direction: ltr; text-align: right;" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="کد نوسازی شهرداری" ID="Label11"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxReconstructionCode"
                                                        Width="100%">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="کد کامپیوتری" ID="Label10"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxComputerCode" Width="100%">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="مساحت سند" ID="Label4"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxDocumentArea" Width="100%">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="false" ErrorText="لطفاً مساحت سند را وارد نمایید"></RequiredField>
                                                            <RegularExpression ErrorText="لطفا عدد صحیحی را در نهایت تا 2 رقم اعشار وارد کنید" ValidationExpression="(([1-9])+([0-9])*)(\.\d{1,2})?|([0]{1})((\.\d{1,2})|([0]{1})?)"></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="مساحت زمین" ID="Label20"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxArea" Width="100%">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="false" ErrorText="لطفاً مساحت زمین را وارد نمایید"></RequiredField>
                                                            <RegularExpression ErrorText="لطفا عدد صحیحی را در نهایت تا 2 رقم اعشار وارد کنید" ValidationExpression="(([1-9])+([0-9])*)(\.\d{1,2})?|([0]{1})((\.\d{1,2})|([0]{1})?)"></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="مساحت عقب نشینی" ID="Label21"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxRecessArea" Width="100%">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="false" ErrorText="لطفاً مساحت عقب نشینی را وارد نمایید"></RequiredField>
                                                            <RegularExpression ErrorText="لطفا عدد صحیحی را در نهایت تا 2 رقم اعشار وارد کنید" ValidationExpression="(([1-9])+([0-9])*)(\.\d{1,2})?|([0]{1})((\.\d{1,2})|([0]{1})?)"></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="مساحت باقی مانده" ID="Label22"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxRemainArea" Width="100%">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="false" ErrorText="لطفاً مساحت باقی مانده را وارد نمایید"></RequiredField>
                                                            <RegularExpression ErrorText="لطفا عدد صحیحی را در نهایت تا 2 رقم اعشار وارد کنید" ValidationExpression="(([1-9])+([0-9])*)(\.\d{1,2})?|([0]{1})((\.\d{1,2})|([0]{1})?)"></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="دفترچه فنی ملکی" ID="Label52"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" colspan="3">
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                        ID="flpOfCroquis" InputType="Files" ClientInstanceName="flpCroquis" OnFileUploadComplete="flpOfCroquis_FileUploadComplete" MaxSizeForUploadFile="1000000">

                                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                        if(e.isValid){
	
  
    imgCroquis.SetVisible(true);
	HFCroquis.Set('name',1);
	lblCroquis.SetVisible(false);
	ASPxHyperLinkCroquis.SetVisible(true);
	ASPxHyperLinkCroquis.SetNavigateUrl('../../../Image/TechnicalServices/TechnicalBooklet/'+e.callbackData);
    HiddenFieldPage.Set('TechnicalBookletAttachName', '~/Image/TechnicalServices/TechnicalBooklet/'+e.callbackData);  
 
    }
    else{
	imgCroquis.SetVisible(false);
	HFCroquis.Set('name',0);
	lblCroquis.SetVisible(true);
	ASPxHyperLinkCroquis.SetVisible(false);
	ASPxHyperLinkCroquis.SetNavigateUrl('');
    }
}"></ClientSideEvents>

                                                                    </TSPControls:CustomAspxUploadControl>
                                                                    <dxe:ASPxLabel runat="server" Text="فایل را انتخاب نمایید" ClientVisible="False"
                                                                        ID="ASPxLabel1" ForeColor="Red" ClientInstanceName="lblCroquis">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="فایل انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                        ID="imgEndUploadImg" ClientInstanceName="imgCroquis">
                                                                    </dxe:ASPxImage>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <dxe:ASPxHyperLink runat="server" Text="فایل دفترچه فنی ملکی" ClientVisible="False" Target="_blank"
                                                        ID="ASPxHyperLinkCroquis" ClientInstanceName="ASPxHyperLinkCroquis" dir="ltr">
                                                    </dxe:ASPxHyperLink>
                                                </td>
                                            </tr>

                                        </table>
                                    </fieldset>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>
                        <dx:ASPxPanel runat="server" ID="PanelBuldingCheck">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <fieldset>
                                        <legend class="HelpUL">مستندات شروع نشدن ساخت و ساز</legend>
                                        <table dir="rtl" width="100%">
                                            <tbody>

                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ بازرسی" ID="ASPxLabel16">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                            ID="txtBuldingCheckDate" RightToLeft="False" PickerDirection="ToRight"
                                                            IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>

                                                    </td>
                                                    <td valign="top" align="right" width="15%">
                                                        <asp:Label runat="server" Text="مستندات شروع نشدن ساخت و ساز" ID="Label16"></asp:Label>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                            ID="flpBuldingCheck" InputType="Files" ClientInstanceName="flpBuldingCheck" OnFileUploadComplete="flpBuldingCheck_FileUploadComplete" MaxSizeForUploadFile="1000000">

                                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                        if(e.isValid){
	imgBuldingCheck.SetVisible(true);
	HFCroquis.Set('BuldingCheck',1);
	lblBuldingCheck.SetVisible(false);
	HyperLinkBuldingCheck.SetVisible(true);
	HyperLinkBuldingCheck.SetNavigateUrl('../../../Image/TechnicalServices/BuldingCheck/'+e.callbackData);
    HiddenFieldPage.Set('BuldingCheckAttachName','~/Image/TechnicalServices/BuldingCheck/'+e.callbackData);                                                        
    }
    else{
	imgBuldingCheck.SetVisible(false);
	HFCroquis.Set('BuldingCheck',0);
	lblBuldingCheck.SetVisible(true);
	HyperLinkBuldingCheck.SetVisible(false);
	HyperLinkBuldingCheck.SetNavigateUrl('');
    }
}"></ClientSideEvents>
                                                                        </TSPControls:CustomAspxUploadControl>
                                                                        <dxe:ASPxLabel runat="server" Text="فایل را انتخاب نمایید" ClientVisible="False"
                                                                            ID="lblBuldingCheck" ForeColor="Red" ClientInstanceName="lblBuldingCheck">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="فایل انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                            ID="imgBuldingCheck" ClientInstanceName="imgBuldingCheck" MaxSizeForUploadFile="1000000">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <dxe:ASPxHyperLink runat="server" Text="فایل مستندات پروانه ساختمانی" ClientVisible="False" Target="_blank"
                                                            ID="HyperLinkBuldingCheck" ClientInstanceName="HyperLinkBuldingCheck" dir="ltr">
                                                        </dxe:ASPxHyperLink>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </fieldset>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>
                        <dx:ASPxPanel runat="server" ID="PanelBuildingCertificate">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <fieldset>
                                        <legend class="HelpUL">مشخصات پروانه ساختمانی</legend>
                                        <table dir="rtl" width="100%">
                                            <tbody>

                                                <tr>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="شماره پروانه ساختمان" ID="ASPxLabel5">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtBuildingCertificateNum" ClientInstanceName="txtBuildingCertificateNum" Width="100%">
                                                        </TSPControls:CustomTextBox>
                                                    </td>

                                                    <td valign="top" align="right" width="15%">
                                                        <asp:Label runat="server" Text="مستندات پروانه ساختمان" ID="Label14"></asp:Label>
                                                    </td>
                                                    <td valign="top" align="right" width="35%">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                            ID="flpBuildingCertificate" InputType="Files" ClientInstanceName="flpCroquis" OnFileUploadComplete="flpBuildingCertificate_FileUploadComplete" MaxSizeForUploadFile="1000000">

                                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                        if(e.isValid){
	imgBuildingCertificate.SetVisible(true);
	HFCroquis.Set('BuildingCertificate',1);
	lblBuildingCertificate.SetVisible(false);
	HyperLinkBuildingCertificate.SetVisible(true);
	HyperLinkBuildingCertificate.SetNavigateUrl('../../../Image/TechnicalServices/BuildingCertificate/'+e.callbackData);
    HiddenFieldPage.Set('BuildingCertificateAttachName','~/Image/TechnicalServices/BuildingCertificate/'+e.callbackData);                                                        
    }
    else{
	imgBuildingCertificate.SetVisible(false);
	HFCroquis.Set('BuildingCertificate',0);
	lblBuildingCertificate.SetVisible(true);
	HyperLinkBuildingCertificate.SetVisible(false);
	HyperLinkBuildingCertificate.SetNavigateUrl('');
    }
}"></ClientSideEvents>
                                                                        </TSPControls:CustomAspxUploadControl>
                                                                        <dxe:ASPxLabel runat="server" Text="فایل را انتخاب نمایید" ClientVisible="False"
                                                                            ID="lblBuildingCertificate" ForeColor="Red" ClientInstanceName="lblBuildingCertificate">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="فایل انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                            ID="imgBuildingCertificate" ClientInstanceName="imgBuildingCertificate" MaxSizeForUploadFile="1000000">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <dxe:ASPxHyperLink runat="server" Text="فایل مستندات پروانه ساختمانی" ClientVisible="False" Target="_blank"
                                                            ID="HyperLinkBuildingCertificate" ClientInstanceName="HyperLinkBuildingCertificate" dir="ltr">
                                                        </dxe:ASPxHyperLink>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ صدور پروانه ساختمان" ID="ASPxLabel11">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                            ID="txtBuildingCertificateStartDate" RightToLeft="False" PickerDirection="ToRight"
                                                            IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>

                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text=" تاریخ پایان اعتبار پروانه ساختمان" ID="ASPxLabel12">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                            PickerDirection="ToRight" ShowPickerOnTop="True" ID="txtBuildingCertificateExpirDate"
                                                            RightToLeft="False" IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                                        <dxe:ASPxLabel runat="server" Text="محدوده تاریخ وارد شده صحیح نمی باشد" ClientVisible="False"
                                                            ID="ASPxLabel13" ForeColor="Red" ClientInstanceName="lblDateError">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </fieldset>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>
                        <dx:ASPxPanel runat="server" ID="PanelEndProject">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <fieldset>
                                        <legend class="HelpUL">مشخصات پایان کار</legend>
                                        <table dir="rtl" width="100%">
                                            <tbody>

                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="شماره پایان کار ساختمان" ID="ASPxLabel4">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtEndProjectNum" ClientInstanceName="txtEndProjectNum" Width="100%">
                                                        </TSPControls:CustomTextBox>
                                                    </td>

                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ پایان کار" ID="ASPxLabel9">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick" Width="100%"
                                                            ID="txtEndProjectStartDate" RightToLeft="False" PickerDirection="ToRight"
                                                            IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>

                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <asp:Label runat="server" Text="مستندات پایان کار" ID="Label7"></asp:Label>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                            ID="flpBuildingLicence" InputType="Files" ClientInstanceName="flpCroquis" OnFileUploadComplete="flpBuildingLicence_FileUploadComplete" MaxSizeForUploadFile="1000000">

                                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                        if(e.isValid){
	imgBuildingLicence.SetVisible(true);
	HFCroquis.Set('BuildingLicence',1);
	lblBuildingLicence.SetVisible(false);
	HyperLinkBuildingLicence.SetVisible(true);
	HyperLinkBuildingLicence.SetNavigateUrl('../../../Image/TechnicalServices/BuildingLicence/'+e.callbackData);
    HiddenFieldPage.Set('BuildingLicenceAttachNameRnd', '~/Image/TechnicalServices/BuildingLicence/'+e.callbackData);  

    }
    else{
	imgBuildingLicence.SetVisible(false);
	HFCroquis.Set('BuildingLicence',0);
	lblBuildingLicence.SetVisible(true);
	HyperLinkBuildingLicence.SetVisible(false);
	HyperLinkBuildingLicence.SetNavigateUrl('');
    }
}"></ClientSideEvents>
                                                                        </TSPControls:CustomAspxUploadControl>
                                                                        <dxe:ASPxLabel runat="server" Text="فایل را انتخاب نمایید" ClientVisible="False"
                                                                            ID="lblBuildingLicence" ForeColor="Red" ClientInstanceName="lblBuildingLicence">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="فایل انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                            ID="imgBuildingLicence" ClientInstanceName="imgBuildingLicence" MaxSizeForUploadFile="1000000">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <dxe:ASPxHyperLink runat="server" Text="فایل مستندات پایان کار" ClientVisible="False" Target="_blank"
                                                            ID="HyperLinkBuildingLicence" ClientInstanceName="HyperLinkBuildingLicence" dir="ltr">
                                                        </dxe:ASPxHyperLink>
                                                    </td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </fieldset>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>

                    </dx:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table cellpadding="0" width="100%" align="right">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top" align="right">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0" align="right">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                            CausesValidation="False" ID="btnNew2" UseSubmitBehavior="False" EnableViewState="true"
                                                            EnableTheming="False" OnClick="btnNew_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/new.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                            CausesValidation="False" Width="25px" ID="btnEdit2" UseSubmitBehavior="False"
                                                            EnableViewState="true" EnableTheming="False" OnClick="btnEdit_Click">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/edit.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="true" EnableTheming="False"
                                                            OnClick="btnSave_Click">
                                                            <ClientSideEvents Click="function(s, e) {
if(HiddenFieldPage.Get('RequestType')==6 && HFCroquis.Get('BuildingLicence')!=1)
	{
		lblBuildingLicence.SetVisible(true);
		e.processOnServer=false;
    }	
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/save.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2">
                                                        </TSPControls:MenuSeprator>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار پروژه"
                                                            ID="btnSendNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="true"
                                                            EnableTheming="False">
                                                            <ClientSideEvents Click="function(s, e) {
	ShowWf();
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/reload.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator5">
                                                        </TSPControls:MenuSeprator>
                                                    </td>


                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                            CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="true"
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
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="mygrid" SessionName="SendBackDataTable_EmpPrjIns"
                OnCallback="CallbackPanelWorkFlow_Callback" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
        <ProgressTemplate>
            <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                <img id="IMG1" src="../../../Image/indicator.gif" align="middle" />
                لطفا صبر نمایید ...
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
    <asp:ObjectDataSource ID="ObjectDataSourceDiscountPercent" runat="server" SelectMethod="FindByInActive"
        TypeName="TSP.DataManager.TechnicalServices.DiscountPercentManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="InActive" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectdatasourceAgent" runat="server" SelectMethod="FindByCode"
        TypeName="TSP.DataManager.AccountingAgentManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="AgentId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ObjectdatasourceUsage" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.TechnicalServices.UsageManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectOwnerShipType" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.TechnicalServices.OwnershipTypeManager"></asp:ObjectDataSource>
    <%--   <asp:ObjectDataSource ID="ObjectdatasourceStructureGroups" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.TechnicalServices.StructureGroupsManager"></asp:ObjectDataSource>--%>
    <asp:ObjectDataSource ID="ObjectdatasourceStructureGroups" runat="server" SelectMethod="FindArchiveStructureItemsForGroupMax"
        TypeName="TSP.DataManager.TechnicalServices.StructureGroupsManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="Step" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="Fundation" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="PkProjectId" runat="server" Visible="False" />
    <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
    <asp:HiddenField ID="PkPrjReId" runat="server" Visible="False" />
    <dxhf:ASPxHiddenField ID="ASPxHiddenFieldCroquis" runat="server" ClientInstanceName="HFCroquis">
    </dxhf:ASPxHiddenField>
    <dxhf:ASPxHiddenField ID="HiddenFieldPage" runat="server" ClientInstanceName="HiddenFieldPage">
    </dxhf:ASPxHiddenField>
</asp:Content>
