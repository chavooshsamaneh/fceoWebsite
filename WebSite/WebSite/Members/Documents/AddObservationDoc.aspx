<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddObservationDoc.aspx.cs" Inherits="Members_Documents_AddObservationDoc"
    Title="مشخصات مجوز ناظر حقیقی" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#"><span style="color: #000000">ب</span>ستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="جدید" ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="BtnNew_Click">
                                     
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right">
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="ویرایش" ToolTip="ویرایش"
                                            CausesValidation="False" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                  
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="ذخیره" ToolTip="ذخیره"
                                            Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click">
                                     
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="بازگشت" ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                          
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelImplement" HeaderText="مشاهده" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td style="width: 100%" dir="rtl" align="center">
                                        <dxe:ASPxLabel runat="server" Text="وضعیت درخواست:" Font-Bold="False" ID="lblWorkFlowState"
                                            ForeColor="Red">
                                        </dxe:ASPxLabel>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <fieldset>
                            <legend class="HelpUL">منطقه نظارت</legend>
                            <table id="tblCity" runat="server" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="شهر" ID="ASPxLabel9">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td valign="top" align="right">
                                                            <TSPControls:CustomTextBox runat="server" ID="txtCity" Width="170px" ReadOnly="True"
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
                                                        <td valign="top" align="right">
                                                            <TSPControls:CustomAspxButton runat="server" ToolTip="جستجو" CausesValidation="False"
                                                                ID="btnSearchCity" EnableClientSideAPI="True" AutoPostBack="False" UseSubmitBehavior="False"
                                                                EnableViewState="False" EnableTheming="False">
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
                                        <td valign="top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="شهرستان" ID="ASPxLabel12">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" width="35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtRegionOfCity" Width="100%"
                                                ReadOnly="True">
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
                                    </tr>
                                    <tr>
                                        <td valign="top" align="center" colspan="4">
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="&nbsp;&nbsp;اضافه به ليست"
                                                ID="btnAddCity" ValidationGroup="ValidCity"
                                                OnClick="btnAddCity_Click" UseSubmitBehavior="false">
                                                <Image Width="16px" Height="16px" Url="~/Images/AddToList.png" />
                                            </TSPControls:CustomAspxButton>
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <TSPControls:CustomAspxDevGridView runat="server" Width="100%"
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
                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowDeleteButton="true" ShowClearFilterButton="true">
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="CitCode" Caption="کد شهر">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="CitName" Caption="شهر">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="3" FieldName="Id">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView>
                        </fieldset>

                        <fieldset>
                            <legend class="HelpUL">مشخصات مجوز ناظر حقیقی</legend>

                            <table id="Table3" width="100%">
                                <tbody>
                                    <tr>
                                        <td style="vertical-align: top" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="شماره پروانه اشتغال" ID="ASPxLabel10" ClientInstanceName="lblDate">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top" dir="rtl" align="right" width="35%">
                                            <TSPControls:CustomTextBox runat="server" ID="lblMFNo" Width="100%" ReadOnly="True"
                                                Style="direction: ltr">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td style="vertical-align: top" dir="rtl" align="right" width="15%">
                                            <dxe:ASPxLabel runat="server" Text="شماره مجوز" ID="ASPxLabel3" ClientInstanceName="lblDate">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top" dir="rtl" align="right" width="35%">
                                            <TSPControls:CustomTextBox runat="server" ID="txtMfNoObs" Width="100%" ReadOnly="True"
                                                Style="direction: ltr">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="پایه" ID="ASPxLabel1" ClientInstanceName="lblFileNo">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top" dir="rtl" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="30px" ID="txtGradeObs" Width="100%"
                                                ReadOnly="True">
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="محل صدور مجوز" ID="ASPxLabel2" ClientInstanceName="lblFileNo">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top" dir="rtl" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtProvinceNameObs" Width="100%"
                                                ReadOnly="True">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ صدور" ID="ASPxLabel4" ClientInstanceName="lblFileNo">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top" dir="rtl" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtRegDateObs" Width="100%"
                                                ReadOnly="True" Style="direction: ltr">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="موقت/دائم" ID="ASPxLabel7">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top" dir="rtl" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                ID="cmbIsTemporary" ValueType="System.String">
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
                                        <td style="vertical-align: top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره سریال" ID="ASPxLabel8">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="vertical-align: top" dir="rtl" align="right">
                                            <TSPControls:CustomTextBox runat="server" ID="txtSerialNoObs" Width="100%"
                                                Style="direction: ltr">
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
                        </fieldset>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table  >
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="جدید" ToolTip="جدید"
                                            ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                      
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="ویرایش" ToolTip="ویرایش"
                                            CausesValidation="False" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                   
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td dir="ltr">
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="ذخیره" ToolTip="ذخیره"
                                            Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnSave_Click">
                                 
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="بازگشت" ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                     
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldDocMemberFile" ClientInstanceName="HiddenFieldDocMemberFile">
            </dxhf:ASPxHiddenField>

            <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl2" runat="server" Width="462px"
                HeaderText="جستجو" PopupVerticalAlign="WindowCenter"
                PopupHorizontalAlign="WindowCenter" CloseAction="CloseButton" PopupElementID="btnSearch1"
                Height="261px">
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

                        <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False"
                            Width="100%" ID="CustomAspxDevGridView1" DataSourceID="ObjdsCity" KeyFieldName="CitId"
                            AutoGenerateColumns="False" ClientInstanceName="gridCity">
                            <ClientSideEvents RowDblClick="function(s, e) {
	SetCityControlValues();
	popUpCity.Hide();
}"></ClientSideEvents>
                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                            <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                            <Columns>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="CitName" Caption="شهر"
                                    Name="CitName">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="AgentName" Caption="نام نمایندگی"
                                    Name="AgentName">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="AgentCode" Width="50px"
                                    Caption="کد نمایندگی" Name="AgentCode">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" Width="1px">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </TSPControls:CustomAspxDevGridView>
                        <asp:ObjectDataSource runat="server" SelectMethod="SelectByReCitId" ID="ObjdsCity"
                            TypeName="TSP.DataManager.CityManager" OldValuesParameterFormatString="original_{0}">
                            <SelectParameters>
                                <asp:Parameter Type="Int32" DefaultValue="-1" Name="ReCitId"></asp:Parameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
                <HeaderStyle>
                    <Paddings PaddingTop="1px" PaddingRight="6px" PaddingLeft="10px"></Paddings>
                </HeaderStyle>
                <SizeGripImage Height="12px" Width="12px">
                </SizeGripImage>
                <CloseButtonImage Height="17px" Width="17px">
                </CloseButtonImage>
            </TSPControls:CustomASPxPopupControl>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نماييد
                <img alt="" src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</asp:Content>
