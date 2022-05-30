<%@ Page Title="مشخصات پنل تسلیت" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddCondolence.aspx.cs" Inherits="Employee_HomePage_AddCondolence" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>


<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls.FormCreatorComponents"
    TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls.FormCreatorComponents"
    TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>

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
                    href="#"><span style="color: #000000">بستن</span></a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tr>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnnew" runat="server" EnableTheming="False"
                                        EnableViewState="False" OnClick="btnnew_Click" ToolTip="جدید" CausesValidation="False">
                                        <Image  Url="~/Images/icons/new.png" >
                                        </Image>

                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnedit" runat="server" EnableTheming="False"
                                        EnableViewState="False" OnClick="btnedit_Click" ToolTip="ویرایش" CausesValidation="False">
                                        <Image  Url="~/Images/icons/edit.png" >
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnsave" runat="server" EnableTheming="False"
                                        EnableViewState="False" ToolTip="ذخیره" OnClick="btnsave_Click">
                                        <Image Url="~/Images/icons/save.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnback" runat="server" EnableTheming="False"
                                        EnableViewState="False" ToolTip="بازگشت" CausesValidation="False" PostBackUrl="Condolence.aspx">
                                        <Image Url="~/Images/icons/Back.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
       

                            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelMain" HeaderText="جدید" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td align="right" valign="top" width="15%">
                                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="نوع">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" width="35%">
                                        <TSPControls:CustomAspxComboBox ID="cmbType" RightToLeft="True" 
                                            runat="server">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <Items>
                                                <dx:ListEditItem Selected="true" Text="تبریک" Value="1" />
                                                <dx:ListEditItem Text="تسلیت" Value="2" />
                                            </Items>
                                        </TSPControls:CustomAspxComboBox>
                                    </td>
                                    <td align="right" valign="top" width="15%"></td>
                                    <td align="right" valign="top" width="35%"></td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" width="15%">
                                        <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="خلاصه متن* (حداکثر 127 کاراکتر)">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" ClientInstanceName="memo" 
                                            Height="37px" ID="MemoSummery"  Width="100%" >
                                            <ClientSideEvents KeyPress="function(s,e){ CheckDevExpressTextboxLengthOnKeyPress(s,e,127); }" />
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                                <RequiredField IsRequired="true" ErrorText="خلاصه متن را وارد نمایید" />
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="right" valign="top" width="15%">
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="از طرف">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" 
                                            Height="37px" ID="MemoFrom"  Width="100%" >
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" width="15%">
                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="توضیحات">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td colspan="3">
                                        <TSPControls:CustomASPXMemo runat="server" 
                                            Height="37px" ID="MemoDescription"  Width="100%" >
                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" width="15%">
                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="تاریخ شروع نمایش*">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" width="35%">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="193px" ValidationGroup="Complain"
                                            ID="txtStartDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                            Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="تاریخ شروع را وارد نمایید"
                                            ControlToValidate="txtStartDate" ID="RequiredFieldValidator2"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="right" valign="top" width="15%">
                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="تاریخ پایان نمایش*">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td align="right" valign="top" width="35%">
                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="193px" ValidationGroup="Complain"
                                            ID="txtEndDate" PickerDirection="ToRight" RightToLeft="False" IconUrl="~/Image/Calendar.gif"
                                            Style="direction: ltr; text-align: right;"></pdc:PersianDateTextBox>
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="تاریخ پایان را وارد نمایید"
                                            ControlToValidate="txtEndDate" ID="RequiredFieldValidator1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right" width="15%">
                                        <dx:ASPxLabel runat="server" Text="تصویر" ID="ASPxLabel4">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" width="35%">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl ID="FileUploadDocument" runat="server" ClientInstanceName="flp"
                                                        InputType="Images" UploadWhenFileChoosed="True" OnFileUploadComplete="FileUploadDocument_FileUploadComplete"
                                                        Width="200px">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                        if(e.isValid){
	imgEndUploadImgClient.SetVisible(true);
    lblerrorupload.SetVisible(false); 
    imgCondolence.SetVisible(true);   
    imgCondolence.SetImageUrl('../../Image/Temp/' + e.callbackData);
	}
	else{
	imgEndUploadImgClient.SetVisible(false);
    lblerrorupload.SetVisible(false); 
    imgCondolence.SetVisible(false);   
    imgCondolence.SetImageUrl('');
	}
}" />
                                                    </TSPControls:CustomAspxUploadControl>
                                                </td>
                                                <td>
                                                    <dx:ASPxImage ID="ASPxImage1" runat="server" ClientInstanceName="imgEndUploadImgClient"
                                                        ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="فایل انتخاب شد">
                                                    </dx:ASPxImage>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <dx:ASPxLabel ID="lblerrorupload" runat="server" ClientInstanceName="lblerrorupload"
                                                        ClientVisible="False" Font-Size="8pt" ForeColor="Red" Text="فایل را انتخاب نمایید">
                                                    </dx:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="right" valign="top" width="15%"></td>
                                    <td align="left" valign="top" width="35%"></td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" width="15%"></td>
                                    <td align="right" valign="top" width="35%">
                                        <dx:ASPxImage ID="imgCondolence" ClientVisible="false" runat="server" ClientInstanceName="imgCondolence"
                                            Height="200px" Width="150px">
                                        </dx:ASPxImage>
                                    </td>
                                    <td align="right" valign="top" width="15%"></td>
                                    <td align="right" valign="top" width="35%"></td>
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
                        <table>
                            <tr>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnnew2" runat="server" EnableTheming="False"
                                        EnableViewState="False" OnClick="btnnew_Click" ToolTip="جدید" CausesValidation="False">
                                        <Image  Url="~/Images/icons/new.png" >
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnedit2" runat="server" EnableTheming="False"
                                        EnableViewState="False" OnClick="btnedit_Click" ToolTip="ویرایش" CausesValidation="False">
                                        <Image  Url="~/Images/icons/edit.png" >
                                        </Image>
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnsave2" runat="server" EnableTheming="False"
                                        EnableViewState="False" ToolTip="ذخیره" OnClick="btnsave_Click">
                                        <Image Url="~/Images/icons/save.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnback2" runat="server" EnableTheming="False"
                                        EnableViewState="False" ToolTip="بازگشت" CausesValidation="False" PostBackUrl="Condolence.aspx">
                                        <Image Url="~/Images/icons/Back.png">
                                        </Image>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
                AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                        <img alt="" id="IMG2" src="../../Image/indicator.gif" align="middle" />
                        لطفا صبر نمایید ...
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>

            <asp:ObjectDataSource ID="ObjectDataSourceGovTitle" runat="server" OldValuesParameterFormatString="original_{0}"
                TypeName="TSP.DataManager.GovManagerTitleManager" SelectMethod="GetData"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourcePrintType" runat="server" OldValuesParameterFormatString="original_{0}"
                TypeName="TSP.DataManager.PrintTypeManager" SelectMethod="GetData"></asp:ObjectDataSource>
            <dx:ASPxHiddenField ID="HiddenFieldModeID" runat="server">
            </dx:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
