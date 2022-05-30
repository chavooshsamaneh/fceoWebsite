<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="MemberImpCapacityInsert.aspx.cs" Inherits="Employee_TechnicalServices_BaseInfo_MemberImpCapacityInsert"
    Title="ظرفیت اشتغال اجرا شخص حقیقی" %>

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

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                            <hoverstyle backcolor="#FFE0C0">
                                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                            </hoverstyle>
                                            <image url="~/Images/icons/new.png">
                                                                            </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <hoverstyle backcolor="#FFE0C0">
                                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                            </hoverstyle>
                                            <image url="~/Images/icons/save.png">
                                                                            </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <hoverstyle backcolor="#FFE0C0">
                                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                            </hoverstyle>
                                            <image url="~/Images/icons/Back.png">
                                                                            </image>
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
                        <table  width="100%">
                            <tbody>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label runat="server" Text="پایه مهندسی" ID="Label24"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxComboBox runat="server" 
                                            TextField="GrdName" ID="ASPxComboBoxGrde" DataSourceID="ObjectdatasourceGrade"
                                            ValueType="System.Int32" ValueField="GrdId"
                                            EnableIncrementalFiltering="True" RightToLeft="True" ClientInstanceName="ComboBoxStructureSkeleton"
                                            AutoPostBack="False">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                <RequiredField ErrorText="لطفاً پایه مهندسی را انتخاب نمایید" IsRequired="True" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td valign="top" align="right">
                                        <asp:Label ID="Label31" runat="server" Text="حداکثر تعداد طبقات از روی شالوده"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxMaxFloor" >
                                            <MaskSettings Mask="&lt;0..10000000000&gt;" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField ErrorText="" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:Label ID="Label1" runat="server" Text="حداکثر ظرفیت اشتغال"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxMaxJobCapacity"
                                            >
                                            <MaskSettings Mask="&lt;0..10000000000&gt;" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField ErrorText="" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" align="right">&nbsp;<asp:Label runat="server" Text="حداکثر تعداد واحد ساختمانی همزمان" ID="Label29"></asp:Label>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxMaxUnitCount" >
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField ErrorText="لطفاً فیلد را وارد نمایید" IsRequired="True" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RegularExpression ErrorText="" />
                                            </ValidationSettings>
                                            <MaskSettings Mask="&lt;0..10000000000&gt;" />
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <asp:Label ID="Label30" runat="server" Text="تاریخ تعریف"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <pdc:PersianDateTextBox runat="server" Enabled="False" DefaultDate="" Width="183px"
                                            ShowPickerOnTop="True" ID="CreateDate" PickerDirection="ToRight" RightToLeft="False"
                                            IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:Label ID="Label2" runat="server" Text="وضعیت"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomTextBox runat="server" ID="ASPxTextBoxInActivStatus"
                                             Enabled="False">
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <RequiredField ErrorText="" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            </div>
            <br />

            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0" align="right">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnNew_Click">
                                            <hoverstyle backcolor="#FFE0C0">
                                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                            </hoverstyle>
                                            <image url="~/Images/icons/new.png">
                                                                            </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                            ID="btnSave2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <hoverstyle backcolor="#FFE0C0">
                                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                            </hoverstyle>
                                            <image url="~/Images/icons/save.png">
                                                                            </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton6" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBack_Click">
                                            <hoverstyle backcolor="#FFE0C0">
                                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                            </hoverstyle>
                                            <image url="~/Images/icons/Back.png">
                                                                            </image>
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
    <asp:ObjectDataSource ID="ObjectdatasourceGrade" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.GradeManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
    <asp:HiddenField ID="PkEnIqId" runat="server" Visible="False" />
    </div>
    </div>
</asp:Content>
