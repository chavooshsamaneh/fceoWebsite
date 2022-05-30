<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberFileBasicInfo.aspx.cs" Inherits="Settlement_MemberDocument_MemberFileBasicInfo"
    Title="مشخصات پروانه اشتغال به کار" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
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
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                    Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnSave_Click">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/save.png">
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
    <%-- <TSPControls:CustomAspxMenuHorizontal ID="MenuMemberFile" runat="server"                            
                            OnItemClick="MenuMemberFile_ItemClick">
                            <Items>
                                <dxm:MenuItem Selected="True" Text="مشخصات پروانه">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="سابقه کار" Name="JobHistory">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="آزمون ها" Name="Exam">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Periods" Text="دوره">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="MeDetail" Text="صلاحیت ها">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="مدارک پیوست" Name="Attachment">
                                </dxm:MenuItem>
                            </Items>
                        </TSPControls:CustomAspxMenuHorizontal>--%>
    <br />
    <TSPControls:CustomASPxRoundPanel ID="RoundPanelMemberFile" HeaderText="مشاهده" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                <table id="Table4" width="100%">
                    <tbody>
                        <tr>
                            <td style="vertical-align: top; width: 15%" align="right">
                                <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="ASPxLabel2" ClientInstanceName="lblPr">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="vertical-align: top; width: 35%" align="right">
                                <TSPControls:CustomTextBox runat="server" ID="txtMeId" Width="100%" AutoPostBack="True"
                                    ClientEnabled="False">
                                    <ValidationSettings>
                                        <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                      
                            </td>
                            <td dir="rtl" style="width: 50%" valign="middle" align="right" colspan="2" rowspan="3">
                                <dxe:ASPxImage runat="server" Height="75px" Width="75px" ImageUrl="../../Images/Person.png"
                                    ID="ImgMember" __designer:wfdid="w8">
                                </dxe:ASPxImage>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel16">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right">
                                <TSPControls:CustomTextBox runat="server" ID="lblMeName" Width="100%" ClientEnabled="False"
                                    ReadOnly="True">
                                    <ValidationSettings>
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="ASPxLabel18">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="width: 35%" valign="top" align="right">
                                <TSPControls:CustomTextBox runat="server" ID="lblMeLastName" Width="100%"
                                    ClientEnabled="False" ReadOnly="True">
                                    <ValidationSettings>
                                        <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                        </ErrorImage>
                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomTextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table runat="server" id="tblProvince" width="100%">
                    <tr runat="server" id="Tr1">
                        <td runat="server" valign="top" style="width: 15%" align="right">
                            <dxe:ASPxLabel runat="server" Text="استان قبلی" ID="ASPxLabel6" ClientInstanceName="lblPr">
                            </dxe:ASPxLabel>
                        </td>
                        <td runat="server" valign="top" style="width: 35%" align="right">
                            <TSPControls:CustomTextBox runat="server" ID="lblPreProvince" Width="100%"
                                ClientEnabled="False" ReadOnly="True">
                            </TSPControls:CustomTextBox>
                        </td>
                        <td runat="server" valign="top" style="width: 15%" align="right">
                            <dxe:ASPxLabel runat="server" Text="تاریخ انتقالی" Width="100%" ID="ASPxLabel7" ClientInstanceName="lblDate">
                            </dxe:ASPxLabel>
                        </td>
                        <td runat="server" valign="top" style="width: 35%" align="right">
                            <TSPControls:CustomTextBox runat="server" ID="lblTransferDate" Width="100%"
                                ClientEnabled="False" ReadOnly="True">
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="Tr2">
                        <td runat="server" valign="top" dir="ltr" align="right">
                            <dxe:ASPxLabel runat="server" Text="شماره پروانه" ID="ASPxLabel8" ClientInstanceName="lblFileNo">
                            </dxe:ASPxLabel>
                        </td>
                        <td runat="server" id="TD6" valign="top" align="right">
                            <TSPControls:CustomTextBox runat="server" ID="lblFileNo" Width="100%" ClientEnabled="False"
                                ReadOnly="True" ClientInstanceName="lblFileNo">
                            </TSPControls:CustomTextBox>
                        </td>
                        <td runat="server" id="TD7" style="vertical-align: top" align="right">
                            <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="ASPxLabel9" ClientInstanceName="lblMeNo">
                            </dxe:ASPxLabel>
                        </td>
                        <td runat="server" valign="top" dir="rtl" align="right">
                            <TSPControls:CustomTextBox runat="server" ID="lblPreMeNo" Width="100%" ClientEnabled="False"
                                ReadOnly="True">
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="Tr3">
                        <td id="Td1" runat="server" valign="top" style="width: 15%" align="right">
                            <dxe:ASPxLabel runat="server" Text="استان صدور پروانه" ID="ASPxLabel1">
                            </dxe:ASPxLabel>
                        </td>
                        <td id="Td2" runat="server" valign="top" style="width: 35%" align="right">
                            <TSPControls:CustomTextBox runat="server" ID="txtDocPrName" Width="100%"
                                ClientEnabled="False" ReadOnly="True">
                            </TSPControls:CustomTextBox>
                        </td>
                        <td id="Td3" runat="server" valign="top" style="width: 15%" align="right">
                            <dxe:ASPxLabel runat="server" Text="تاریخ اولین صدور" ID="ASPxLabel4">
                            </dxe:ASPxLabel>
                        </td>
                        <td id="Td4" runat="server" valign="top" style="width: 35%" align="right">
                            <TSPControls:CustomTextBox runat="server" ID="txtFirstDocRegDate" Width="100%"
                                ClientEnabled="False" ReadOnly="True">
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="Tr4">
                        <td id="Td5" runat="server" valign="top" dir="ltr" align="right">
                            <dxe:ASPxLabel runat="server" Text="تاریخ آخرین تمدید" ID="ASPxLabel34">
                            </dxe:ASPxLabel>
                        </td>
                        <td runat="server" id="TD8" valign="top" align="right">
                            <TSPControls:CustomTextBox runat="server" ID="txtCurrentDocRegDate" Width="100%"
                                ClientEnabled="False" ReadOnly="True" ClientInstanceName="txtCurrentDocRegDate">
                            </TSPControls:CustomTextBox>
                        </td>
                        <td runat="server" id="TD9" style="vertical-align: top" align="right">
                            <dxe:ASPxLabel runat="server" Text="تاریخ اعتبار" ID="ASPxLabel37">
                            </dxe:ASPxLabel>
                        </td>
                        <td id="Td10" runat="server" valign="top" align="right">
                            <TSPControls:CustomTextBox runat="server" ID="txtCurrentDocExpDate" Width="100%"
                                ClientEnabled="False" ReadOnly="True" ClientInstanceName="txtCurrentDocExpDate">
                            </TSPControls:CustomTextBox>
                        </td>
                    </tr>
                </table>
                <TSPControls:CustomAspxCallbackPanel runat="server"
                    ClientInstanceName="CallbackPanelDoRegDate"  ID="CallbackPanelDoRegDate"
                    OnCallback="CallbackPanelDoRegDate_Callback">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent11" runat="server">
                            <table id="Table3" width="100%">
                                <tbody>
                                    <tr>
                                        <td style="width: 15%" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره پروانه" Width="72px" ID="ASPxLabel10" ClientInstanceName="lblDate">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="width: 35%" valign="top" dir="ltr" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="100%" ClientEnabled="False"
                                                ReadOnly="True" ID="lblMFNo">
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td style="width: 15%" valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ صدور اولین پروانه" Width="109px" ID="ASPxLabel12"
                                                ClientInstanceName="lblFileNo">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td style="width: 35%" valign="top" dir="rtl" align="right">
                                            <TSPControls:CustomTextBox Style="direction: ltr" runat="server" Width="100%"
                                                ClientEnabled="False" ReadOnly="True" ID="lblRegDate">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="موقت/دائم" ID="ASPxLabel3" ClientInstanceName="lblPr">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                ID="cmbIsTemporary" ClientInstanceName="cmbIsTemporary" ValueType="System.String"
                                                RightToLeft="True">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ClientSideEvents SelectedIndexChanged="function(s,e){CallbackPanelDoRegDate.PerformCallback(cmbIsTemporary.GetValue());}" />
                                                <Items>
                                                    <dxe:ListEditItem Value="0" Text="پروانه دائم"></dxe:ListEditItem>
                                                    <dxe:ListEditItem Value="1" Text="پروانه موقت"></dxe:ListEditItem>
                                                </Items>
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                                <ButtonStyle Width="13px">
                                                </ButtonStyle>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="شماره سریال" ID="ASPxLabel11" ClientInstanceName="lblPr">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox Style="direction: ltr" runat="server" Width="100%"
                                                ID="txtSerialNo" ClientInstanceName="txtSerialNo">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <RequiredField IsRequired="True" ErrorText="شماره سریال را وارد نمایید"></RequiredField>
                                                    <RegularExpression ErrorText="شماره سریال را با فرمت صحیح وارد نمائید" ValidationExpression="\d{1,10}"></RegularExpression>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ آخرین تمدید" ID="ASPxLabel15" ClientInstanceName="lblDate">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="245px" ShowPickerOnTop="True"
                                                ID="txtRegDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                            <asp:RequiredFieldValidator runat="server" ForeColor="Red" ControlToValidate="txtRegDate"
                                                ID="RequiredFieldValidator8">تاریخ آخرین تمدید را وارد نمایید</asp:RequiredFieldValidator>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="تاریخ پایان اعتبار" Width="88px" ID="ASPxLabel17"
                                                ClientInstanceName="lblDate">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="245px" ShowPickerOnTop="True"
                                                ID="txtExpDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                            <asp:RequiredFieldValidator runat="server" ForeColor="Red" ControlToValidate="txtExpDate"
                                                ID="RequiredFieldValidator1">تاریخ پایان اعتبار را وارد نمایید</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomAspxCallbackPanel >
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
                            <td dir="ltr">
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره"
                                    Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                    EnableTheming="False" OnClick="btnSave_Click">
                                    <HoverStyle BackColor="#FFE0C0">
                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                    </HoverStyle>
                                    <Image Url="~/Images/icons/save.png">
                                    </Image>
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td dir="ltr">
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" OnClick="btnBack_Click" runat="server" Text=" "
                                    EnableTheming="False" EnableViewState="False" UseSubmitBehavior="False" CausesValidation="False"
                                    ToolTip="بازگشت">
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
    <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldDocMemberFile">
    </dxhf:ASPxHiddenField>
    <asp:ObjectDataSource ID="ObjdsDocMeMajor" runat="server" SelectMethod="SelectMemberFileById"
        TypeName="TSP.DataManager.DocMemberFileMajorManager" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Type="Int32" DefaultValue="-1" Name="MFId"></asp:Parameter>
            <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="InActive" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
