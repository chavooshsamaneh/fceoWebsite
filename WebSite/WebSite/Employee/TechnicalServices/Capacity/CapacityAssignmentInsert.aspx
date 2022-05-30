<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="CapacityAssignmentInsert.aspx.cs" Inherits="Employee_TechnicalServices_Capacity_CapacityAssignmentInsert"
    Title="اختصاص ظرفیت" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" style="display: block" align="center">
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <div dir="ltr" style="display: block; overflow: hidden">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                        visible="false">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                        [<a class="closeLink" href="#">بستن</a>]
                    </div>
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                        <PanelCollection>
                            <dxp:PanelContent>

                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                    CausesValidation="False" ID="btnNew" UseSubmitBehavior="False" EnableViewState="False"
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
                                                    CausesValidation="False" Width="25px" ID="btnEdit" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/edit.png">
                                                    </Image>
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
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                                </TSPControls:MenuSeprator>
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
                    <TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel2" HeaderText="مشاهده" runat="server">
                        <PanelCollection>
                            <dxp:PanelContent>

                                <table dir="rtl" width="100%">
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="right">
                                                <asp:Label runat="server" Text="سال" ID="Label24" Style="width: 15%"></asp:Label>
                                            </td>
                                            <td valign="top" align="right" style="width: 35%">
                                                <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxYear"
                                                    Enabled="False">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                        <RequiredField IsRequired="True" ErrorText="لطفاً سال را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText=""></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                </TSPControls:CustomTextBox>
                                            </td>
                                            <td valign="top" align="right" style="width: 15%">
                                                <asp:Label runat="server" Text="N(%):" ID="Label29"></asp:Label>
                                            </td>
                                            <td valign="top" align="right" style="width: 35%">
                                                <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxCapacityPrcnt">
                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                        <RequiredField IsRequired="True" ErrorText="لطفاً N را وارد نمایید"></RequiredField>
                                                        <RegularExpression ErrorText=""></RegularExpression>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                        </ErrorFrameStyle>
                                                    </ValidationSettings>
                                                    <MaskSettings Mask="&lt;0..100&gt;" />
                                                    <ClientSideEvents TextChanged="function(s, e) {
	lblCapacityPrcnt.SetVisible(false);
}" />
                                                </TSPControls:CustomTextBox>
                                            </td>

                                        </tr>
                                    </tbody>
                                </table>
                                <fieldset runat="server" id="RoundPanelMainAgent">
                                    <legend class="fieldset-legend" dir="rtl"><b>مشخصات سال کاری نمایندگی شیراز</b>
                                    </legend>
                                    <table dir="rtl" width="100%">
                                        <tbody>

                                            <tr>
                                                <td align="right" valign="top" style="width: 15%">تاریخ شروع سال کاری نمایندگی شیراز</td>
                                                <td align="right" valign="top" style="width: 35%">
                                                    <pdc:PersianDateTextBox ID="txtAssignmentDate" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                        PickerDirection="ToRight" ShowPickerOnTop="True" Width="250px" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txtAssignmentDate" ErrorMessage="تاریخ را وارد نمایید"></asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right" valign="top" style="width: 15%">تاریخ پایان سال کاری نمایندگی شیراز</td>
                                                <td align="right" valign="top" style="width: 35%">
                                                    <pdc:PersianDateTextBox ID="txtEndDate" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                        PickerDirection="ToRight" ShowPickerOnTop="True" Width="250px" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPlansMethodDate" runat="server"
                                                        ControlToValidate="txtEndDate" ErrorMessage="تاریخ را وارد نمایید"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right" style="width: 15%">
                                                    <asp:Label runat="server" Text="تعداد کار مجاز :" ID="Label1"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtWorkCountMainAgent">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="لطفاً تعداد کار مجاز را وارد نمایید"></RequiredField>
                                                            <RegularExpression ErrorText=""></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <MaskSettings Mask="&lt;0..1000&gt;" />
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right" style="width: 15%">
                                                    <asp:Label runat="server" Text="تعداد کار زیر 400 معادل یک کار :" ID="Label2"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtWorkCountUnder400MainAgent">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="لطفاً تعداد کار زیر 400 معادل یک کار  را وارد نمایید"></RequiredField>
                                                            <RegularExpression ErrorText=""></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <MaskSettings Mask="&lt;0..1000&gt;" />
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <tr>
                                                    <td align="right" valign="top" style="width: 15%">عدم اجباری آپلود نامه شهرداری و بنیاد مسکن شیراز</td>
                                                    <td align="right" valign="top" style="width: 35%">
                                                        <pdc:PersianDateTextBox ID="txtStopmandatoryFileUploadingMainAgent" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                            PickerDirection="ToRight" ShowPickerOnTop="True" Width="250px" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="2" valign="top">
                                                        <TSPControls:CustomASPxCheckBox ID="CheckBoxFreeObsCapacityMainAgent" runat="server" Text="کارکرد نظارت سال قبل آزاد شود">
                                                        </TSPControls:CustomASPxCheckBox>
                                                    </td>
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="2" valign="top">
                                                        <TSPControls:CustomASPxCheckBox ID="CheckBoxFreeDesCapacityMainAgent" runat="server" Text="کارکرد طراحی سال قبل آزاد شود">
                                                        </TSPControls:CustomASPxCheckBox>
                                                    </td>
                                                    <td colspan="2"></td>
                                                </tr>
                                        </tbody>
                                    </table>
                                </fieldset>
                                <fieldset runat="server" id="RoundPanelOtherAgents">
                                    <legend class="fieldset-legend" dir="rtl"><b>مشخصات سال کاری سایر نمایندگی ها</b>
                                    </legend>
                                    <table dir="rtl" width="100%">
                                        <tbody>

                                            <tr>
                                                <td align="right" valign="top" style="width: 15%">تاریخ شروع سال کاری سایر نمایندگی</td>
                                                <td align="right" valign="top" style="width: 35%">
                                                    <pdc:PersianDateTextBox ID="txtAssignmentDateOtherAgent" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                        PickerDirection="ToRight" ShowPickerOnTop="True" Width="250px" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                        ControlToValidate="txtAssignmentDateOtherAgent" ErrorMessage="تاریخ را وارد نمایید"></asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right" valign="top" style="width: 15%">تاریخ پایان سال کاری  سایر  نمایندگی</td>
                                                <td align="right" valign="top" style="width: 35%">
                                                    <pdc:PersianDateTextBox ID="txtEndDateOtherAgents" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                        PickerDirection="ToRight" ShowPickerOnTop="True" Width="250px" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                        ControlToValidate="txtEndDateOtherAgents" ErrorMessage="تاریخ را وارد نمایید"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right" style="width: 15%">
                                                    <asp:Label runat="server" Text="تعداد کار مجاز :" ID="Label3"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtWorkCountOtherAgents">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="لطفاً تعداد کار مجاز را وارد نمایید"></RequiredField>
                                                            <RegularExpression ErrorText=""></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <MaskSettings Mask="&lt;0..1000&gt;" />
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right" style="width: 15%">
                                                    <asp:Label runat="server" Text="تعداد کار زیر 400 معادل یک کار :" ID="Label4"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" style="width: 35%">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtWorkCountUnder400OtherAgents">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="لطفاً تعداد کار زیر 400 معادل یک کار  را وارد نمایید"></RequiredField>
                                                            <RegularExpression ErrorText=""></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <MaskSettings Mask="&lt;0..1000&gt;" />
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <tr>
                                                    <td align="right" valign="top" style="width: 15%">عدم اجباری آپلود نامه شهرداری و بنیاد مسکن شهرستان ها</td>
                                                    <td align="right" valign="top" style="width: 35%">
                                                        <pdc:PersianDateTextBox ID="txtStopmandatoryFileUploadingOtherAgent" runat="server" DefaultDate="" IconUrl="~/Image/Calendar.gif"
                                                            PickerDirection="ToRight" ShowPickerOnTop="True" Width="250px" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="2" valign="top">
                                                        <TSPControls:CustomASPxCheckBox ID="CheckBoxFreeObsCapacityOtherAgents" runat="server" Text="کارکرد نظارت سال قبل آزاد شود">
                                                        </TSPControls:CustomASPxCheckBox>
                                                    </td>
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="2" valign="top">
                                                        <TSPControls:CustomASPxCheckBox ID="CheckBoxFreeDesCapacityOtherAgents" runat="server" Text="کارکرد طراحی سال قبل آزاد شود">
                                                        </TSPControls:CustomASPxCheckBox>
                                                    </td>
                                                    <td colspan="2"></td>
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

                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                    CausesValidation="False" ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False"
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
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image Url="~/Images/icons/edit.png">
                                                    </Image>
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
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2">
                                                </TSPControls:MenuSeprator>
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
            <asp:HiddenField ID="PkCapacityAssignmentId" runat="server" Visible="False" />
            <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
        </div>
    </div>
</asp:Content>
