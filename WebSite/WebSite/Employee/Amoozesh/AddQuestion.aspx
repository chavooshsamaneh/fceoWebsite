<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddQuestion.aspx.cs" Inherits="Employee_Amoozesh_AddQuestion"
    Title="مشخصات سؤال نظر سنجی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" WorkDayCSS="PickerWorkDayCSS" WeekDayCSS="PickerWeekDayCSS" SelectedCSS="PickerSelectedCSS" HeaderCSS="PickerHeaderCSS" FrameCSS="PickerCSS" ForbidenCSS="PickerForbidenCSS" FooterCSS="PickerFooterCSS" CalendarDayWidth="50" CalendarCSS="PickerCalendarCSS">
            </pdc:PersianDateScriptManager>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">
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
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                            CausesValidation="False" ID="btnBack" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
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
                        <fieldset id="ASPxRoundPanel4" runat="server">
                            <legend class="HelpUL">اطلاعات پایه></legend>
                            <table dir="rtl" width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد سؤال"  ID="lblCodeNew" ></dxe:ASPxLabel>

                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtQuCode" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>

                                                    <RequiredField IsRequired="True" ErrorText="کد سؤال را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>

                                        </td>
                                        <td valign="top" align="right"></td>
                                        <td valign="top" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="کد سؤال" ID="lblCodeView" Visible="False" __designer:wfdid="w13"></dxe:ASPxLabel>

                                        </td>
                                        <td dir="ltr" valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" ValueType="System.String" TextField="QuCode" ValueField="QuCode"  AutoPostBack="True" Visible="False" ID="CmbQucode" __designer:wfdid="w14" OnSelectedIndexChanged="CmbQucode_SelectedIndexChanged">
                                                <ButtonStyle Width="13px"></ButtonStyle>

                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>

                                        </td>
                                        <td valign="top" align="right"></td>
                                        <td valign="top" align="right"></td>
                                    </tr>
                                    <tr>
                                        <td  valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="طراح سؤال"  ID="ASPxLabel1" ></dxe:ASPxLabel>

                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtDesigner" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>

                                                    <RequiredField IsRequired="True" ErrorText="طراح سؤال را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>

                                        </td>
                                        <td  valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره ویرایش" Width="74px" ID="ASPxLabel3" ></dxe:ASPxLabel>

                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  ID="txtEditNo" >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>

                                                    <RequiredField ErrorText=""></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right" colspan="4">
                                            <dx:ASPxCallbackPanel runat="server" ClientInstanceName="CallbackPanelReq" Width="100%" ID="CallbackPanelReq" OnCallback="CallbackPanelReq_Callback">
                                                <PanelCollection>
                                                    <dxp:PanelContent runat="server">
                                                        <table id="Table5" width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: 122px" valign="top" align="right">
                                                                        <dxe:ASPxLabel runat="server" Text="شماره نامه" ClientInstanceName="lblPr" ID="ASPxLabel11" __designer:wfdid="w44"></dxe:ASPxLabel>


                                                                    </td>
                                                                    <td valign="top" align="right">
                                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  ClientInstanceName="txtLetterNo" ID="txtLetterNo" Style="direction: ltr" >
                                                                            <ClientSideEvents TextChanged="function(s, e) {
	        CallbackPanelReq.PerformCallback('CheckLetter'+';'+txtLetterNo.GetText());
        }"></ClientSideEvents>

                                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom" ValidationGroup="ValidateDuplicate">
                                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                </ErrorFrameStyle>

                                                                                <RequiredField IsRequired="True" ErrorText="شماره نامه را وارد نمایید"></RequiredField>
                                                                            </ValidationSettings>
                                                                        </TSPControls:CustomTextBox>


                                                                        <dxe:ASPxLabel runat="server" Text="شماره نامه معتبر نمی باشد" ClientVisible="False" BackColor="White" ForeColor="Red" ID="lblErrorMail" __designer:wfdid="w46"></dxe:ASPxLabel>
                                                                    </td>
                                                                    <td  dir="ltr" valign="top" align="right">
                                                                        <dxe:ASPxLabel runat="server" Text="تاریخ نامه" ClientInstanceName="lblDate" ID="ASPxLabel13" ></dxe:ASPxLabel>


                                                                    </td>
                                                                    <td dir="rtl" valign="top" align="right">
                                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  ReadOnly="True" ID="txtLetterDate" >
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
                                                                    <td  valign="top" align="right">موضوع نامه</td>
                                                                    <td dir="rtl" valign="top" align="right" colspan="3">
                                                                        <TSPControls:CustomASPXMemo runat="server" Height="30px" ReadOnly="True" ClientInstanceName="txtMailTitle" ID="txtMailTitle" __designer:wfdid="w49">

                                                                        </TSPControls:CustomASPXMemo>


                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxCallbackPanel>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره صورت جلسه" Width="105px" ID="ASPxLabel5" Visible="False" __designer:wfdid="w24"></dxe:ASPxLabel>

                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Visible="False" ID="txtSessionNo" Style="direction: ltr"  >
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>

                                                    <RequiredField ErrorText=""></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>

                                        </td>
                                        <td  valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ صورت جلسه" Width="94px" ID="ASPxLabel6" Visible="False" __designer:wfdid="w26"></dxe:ASPxLabel>

                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" RightToLeft="False" DefaultDate="" IconUrl="~/Image/Calendar.gif" PickerDirection="ToRight" ShowPickerOnTop="True" Width="165px" ID="txtSessionDate" Visible="False" Style="direction: ltr; text-align: right;" __designer:wfdid="w27"></pdc:PersianDateTextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td  valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" Width="62px" ID="ASPxLabel7" __designer:wfdid="w28"></dxe:ASPxLabel>

                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="30px" ID="txtDesc" __designer:wfdid="w29">
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
                                        <td style="width: 105px" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="وضعیت" Width="62px" ID="lblStatus" __designer:wfdid="w30"></dxe:ASPxLabel>

                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server" Width="150px" ReadOnly="True" ID="txtStatus" __designer:wfdid="w31">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>

                                                    <RequiredField IsRequired="True" ErrorText="کد سؤال را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>

                                        </td>
                                        <td style="width: 94px" valign="top" align="right"></td>
                                        <td valign="top" align="right"></td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>

                        <fieldset id="ASPxRoundPanel5" runat="server">
                            <legend class="HelpUL">سری سؤالات></legend>
                            <TSPControls:CustomAspxDevGridView runat="server" KeyFieldName="QuId" ID="CustomAspxDevGridView1" OnRowUpdating="CustomAspxDevGridView1_RowUpdating" OnRowInserting="CustomAspxDevGridView1_RowInserting" OnRowDeleting="CustomAspxDevGridView1_RowDeleting" OnCommandButtonInitialize="CustomAspxDevGridView1_CommandButtonInitialize">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn FieldName="QuNo" Caption="شماره" VisibleIndex="0">
                                        <PropertiesTextEdit Width="100px">
                                            <ValidationSettings ErrorDisplayMode="ImageWithText" Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d*"></RegularExpression>

                                                <RequiredField IsRequired="True" ErrorText="شماره را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </PropertiesTextEdit>

                                        <EditCellStyle HorizontalAlign="Right"></EditCellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataMemoColumn FieldName="Question" Caption="سؤال" VisibleIndex="1">
                                        <PropertiesMemoEdit Width="250px">
                                            <ValidationSettings ErrorDisplayMode="ImageWithText" Display="Dynamic" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="سؤال را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </PropertiesMemoEdit>

                                        <EditCellStyle HorizontalAlign="Right"></EditCellStyle>
                                    </dxwgv:GridViewDataMemoColumn>
                                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="2" ShowEditButton="true" ShowNewButton="true" ShowDeleteButton="true">
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="QuId" Visible="False" VisibleIndex="3"></dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
                            </TSPControls:CustomAspxDevGridView>
                        </fieldset>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <dx:ASPxHiddenField ID="HiddenFieldLetter" ClientInstanceName="HiddenFieldLetter" runat="server"></dx:ASPxHiddenField>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="BtnNew_Click">
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
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                            CausesValidation="False" ID="btnBack2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
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
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                   <img align="middle" src="../../Image/indicator.gif" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
    <asp:HiddenField ID="HDQuId" runat="server" Visible="False" />
    <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
    <asp:ObjectDataSource ID="OdbQuestion" runat="server" SelectMethod="SelectUniqueCode"
        TypeName="TSP.DataManager.QuestionsManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdbGrid" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.QuestionsManager"></asp:ObjectDataSource>

</asp:Content>
