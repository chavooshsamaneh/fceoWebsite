<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddMemberFile.aspx.cs" Inherits="Members_Documents_AddMemberFile"
    Title="صدور پروانه اشتغال به کار" %>

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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">




        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
        function SetMeDocDefualtExpireDateJS() {
            CallbackPanelDoRegDate.PerformCallback(cmbIsTemporary.GetValue());
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
                CalendarDayWidth="33" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
                FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
            </pdc:PersianDateScriptManager>
            <div align="right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>

            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent> 
                            <table >
                                <tbody>
                                    <tr>

                                        <td > 
                                              <asp:LinkButton ID="btnEdit" CssClass="ButtonMenue" OnClick="btnEdit_Click" runat="server">ویرایش درخواست</asp:LinkButton>
                                        </td>
                                        <td>
                                             <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="ذخیره"  ToolTip="ذخیره"
                                                Width="25px" ID="btnSave" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnSave_Click">
                                                   <ClientSideEvents Click="function(s, e) {                                                  
                                                 
                                                      return e.processOnServer= confirm('پس از ذخیره اطلاعات و اطمینان از تکمیل تمام اطلاعات بایستی در صفحه مدیریت درخواست های پروانه  گزينه گردش كار را انتخاب كرده و درخواست خود را به كارمند واحد عضويت و پروانه ارسال كنيد. در غیر این صورت روند بررسی پرونده شما شروع نمی شود');
                                                      btnSave(e); 
                                                      }"></ClientSideEvents>


                                             </TSPControls:CustomAspxButton>                                         
                                        </td>                                     
                                        <td>
                                             <asp:LinkButton ID="btnBack" CssClass="ButtonMenue" OnClick="btnBack_Click" runat="server">مدیریت پروانه اشتغال</asp:LinkButton>     
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                      
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomAspxMenuHorizontal ID="MenuMemberFile" runat="server"
                OnItemClick="MenuMemberFile_ItemClick" CssClass="ProjectMainMenuHorizontal">
                <Items>
                    <dxm:MenuItem Text="مشخصات پروانه" Selected="True" Name="BaseInfo"  ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="سابقه کار" Name="JobHistory" Visible="false" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="تاییدکنندگان سابقه کار" Name="JobConfirmition" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="آزمون ها" Name="Exam" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="دوره" Name="Periods" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="پایه - صلاحیت" Name="MeDetail" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مدارک پیوست" Name="Attachment" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Capacity" Text="ظرفیت اشتغال" Visible="false" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>

            <br />
            <table dir="rtl" width="100%" align="center">
                <td width="10%" align="left">
                    <blink id="bkImgWarningMsg"><dxe:ASPxImage ID="ImgWarningMsg" ClientVisible="false" Width="25px" Height="25px" runat="server" ImageUrl="~/Images/Errors-64.png">
                                    </dxe:ASPxImage></blink>
                </td>
                <td width="90%" align="right">
                    <asp:Label ID="lblWarningText" Font-Bold="true" ForeColor="DarkRed" runat="server"></asp:Label>
                </td>
            </table>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelMemberFile" HeaderText="مشاهده" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <div align="center">
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td style="width: 100%" dir="rtl" align="center">
                                            <dxe:ASPxLabel runat="server" Text="وضعیت درخواست:" Font-Bold="False" ForeColor="Red"
                                                ID="lblWorkFlowState">
                                            </dxe:ASPxLabel>
                                            <dxe:ASPxLabel runat="server" Text="" Font-Bold="False" ForeColor="Red" ID="lblRequestType">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <span runat="server" id="txtRequestComment" style="font-weight: bold" class="HelpUL" visible="false"></span>
                            <%--    <dxe:ASPxLabel runat="server" Font-Bold="true" Text="دستورالعمل درخواست" ID=""
                              >
                            </dxe:ASPxLabel>--%>
                        </div>
                        <br />
                        <fieldset runat="server" id="RoundPanelTransfer">
                            <legend class="fieldset-legend" dir="rtl"><b>مشخصات عضو</b>
                            </legend>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" align="right" style="width: 15%">
                                            <dxe:ASPxLabel runat="server" Text="کد عضویت *" ID="ASPxLabel2" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" style="width: 35%">
                                            <TSPControls:CustomTextBox runat="server" Width="100%" AutoPostBack="True"
                                                ID="txtMeId">
                                                <ClientSideEvents TextChanged="function(s, e) {
}"></ClientSideEvents>
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="True" ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                    <RegularExpression ValidationExpression="\d*" ErrorText="کد عضویت را صحیح وارد نمایید" />
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td width="15%"></td>
                                        <td width="35%"></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabel16" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="100%" Enabled="false"
                                                ID="lblMeName">
                                                <ValidationSettings>
                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomTextBox>
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="ASPxLabel18" Width="100%">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">
                                            <TSPControls:CustomTextBox runat="server" Width="100%" Enabled="false"
                                                ID="lblMeLastName">
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
                                        <td valign="top" align="right">آخرین تصویر تایید شده عضو
                                        </td>
                                        <td valign="middle" align="right">
                                            <dxe:ASPxHyperLink runat="server" NavigateUrl="~/Images/Person.png" ImageUrl="~/Images/Person.png"
                                                Width="75px" Height="75px" ImageWidth="75px" ImageHeight="75px" ID="ImgMember"
                                                Target="_blank">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                        <td id="Td48" runat="server" align="right" valign="top">آخرین تصویر تایید شده شناسنامه
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxHyperLink ID="HpIdNo" runat="server" ClientInstanceName="hidno" Target="_blank"
                                                ImageWidth="75px" ImageHeight="75px" Text="تصویر شناسنامه">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">آخرین تصویر کارت ملی تایید شده
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxHyperLink ID="HpSSN" runat="server" Target="_blank" ImageWidth="75px" ImageHeight="75px"
                                                Text="تصویر کارت ملی">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                        <td align="right" valign="top">
                                            <dxe:ASPxLabel ID="lblSoldire" ClientVisible="true" runat="server" Text="تصویر کارت پایان خدمت"
                                                ClientInstanceName="lblSoldire">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="right" valign="top" dir="rtl">
                                            <dxe:ASPxHyperLink ID="HpSoldire" runat="server" Target="_blank" ImageWidth="75px"
                                                ImageHeight="75px" Text="تصویر کارت پایان خدمت">
                                            </dxe:ASPxHyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <asp:Label runat="server" Text="تصویر نامه عدم عضویت در سازمان نظام کاردانی" ID="lblKardanAttach"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td valign="top" align="right">
                                             <a id="AImageKardan" runat="server" target="_blank">
                                            <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="ImageKardan" ClientInstanceName="ImageKardan"
                                                Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                                <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                                </EmptyImage>
                                            </dxe:ASPxImage>
                                                 </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel30">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right" colspan="3">
                                            <TSPControls:CustomASPXMemo runat="server" Height="34px" Width="100%"
                                                ClientInstanceName="txtDescription" ID="txtDescription" RightToLeft="True">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText="دلایل درخواست خود را به صورت مختصر بیان نمایید" IsRequired="false" />
                                                    <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                    </ErrorImage>
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPXMemo>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <table runat="server" id="TblTransfer" width="100%" dir="rtl">
                                <tr runat="server" id="Tr1">
                                    <td runat="server" id="Td1" style="width: 15%" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="استان قبلی" ClientInstanceName="lblPr" Width="100%"
                                            ID="ASPxLabel6">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="width: 35%" valign="top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" Enabled="false"
                                            ID="lblPreProvince">
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td valign="top" style="width: 15%" dir="rtl" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تاریخ انتقالی" Width="100%" ID="ASPxLabel7">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="width: 35%" valign="top" dir="ltr" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" Enabled="false"
                                            ID="lblTransferDate">
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
                                    <td runat="server" id="Td5" dir="ltr" valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="شماره پروانه" Width="100%" ID="ASPxLabel8">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" Enabled="false"
                                            ClientInstanceName="lblFileNo" ID="lblFileNo" Style="direction: ltr" RightToLeft="True">
                                            <ValidationSettings>
                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomTextBox>
                                    </td>
                                    <td style="vertical-align: top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="شماره عضویت" ClientInstanceName="lblMeNo" ID="ASPxLabel9">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top" dir="rtl" align="right">
                                        <TSPControls:CustomTextBox runat="server" Width="100%" Enabled="false"
                                            ID="lblPreMeNo">
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
                                <tr>
                                    <td colspan="4" align="justify">
                                        <br />
                                        <dxe:ASPxLabel runat="server" Text="تاریخ ها مربوط به آخرین پروانه فرد در استان قبلی می باشند.همچنین استان صدور پروانه ، محل صدور اولین پروانه وی می باشد"
                                            Font-Bold="true" ForeColor="DarkRed" ID="lblAlertPreDocDate">
                                        </dxe:ASPxLabel>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%" align="right" valign="top">
                                        <dxe:ASPxLabel ID="lblDocPr" runat="server" ClientInstanceName="lblDocPr" Text="استان صدور پروانه"
                                            Width="100%">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="width: 35%" align="right" valign="top">
                                        <TSPControls:CustomAspxComboBox ID="ComboDocPr" runat="server" ClientInstanceName="ComboDocPr"
                                            DataSourceID="OdbProvince"
                                            TextField="PrName" ValueField="PrId" ValueType="System.String"
                                            Width="100%" RightToLeft="True">

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
                                    <td style="width: 15%"></td>
                                    <td style="width: 35%"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel runat="server" Text="تاریخ اولین صدور" ID="ASPxLabel12">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <pdc:PersianDateTextBox runat="server" DefaultDate=""  ShowPickerOnTop="True"
                                            ID="txtFirstDocRegDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                            ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel runat="server" Text="تاریخ آخرین تمدید" ID="ASPxLabel34">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <pdc:PersianDateTextBox runat="server" DefaultDate=""  ShowPickerOnTop="True"
                                            ID="txtCurrentDocRegDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                            ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dxe:ASPxLabel runat="server" Text="تاریخ اعتبار" ID="ASPxLabel37">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td>
                                        <pdc:PersianDateTextBox runat="server" DefaultDate=""  ShowPickerOnTop="True"
                                            ID="txtCurrentDocExpDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"
                                            ShowPickerOnEvent="OnClick"></pdc:PersianDateTextBox>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />

                        <fieldset runat="server" id="RoundPanelMajors">
                            <legend class="fieldset-legend" dir="rtl"><b>*مدارک تحصیلی پروانه اشتغال</b>
                            </legend>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td dir="rtl" align="center">
                                            <TSPControls:CustomAspxDevGridView2 runat="server"
                                                KeyFieldName="Id" AutoGenerateColumns="False" RightToLeft="True"
                                                Width="100%" ID="GridViewMajor" ClientInstanceName="GridViewMajor" OnHtmlRowPrepared="GridViewMajor_HtmlRowPrepared"
                                                OnCommandButtonInitialize="GridViewMajor_CommandButtonInitialize"
                                                OnAutoFilterCellEditorInitialize="GridViewMajor_AutoFilterCellEditorInitialize"
                                                OnHtmlDataCellPrepared="GridViewMajor_HtmlDataCellPrepared">

                                                <Columns>

                                                    <dxwgv:GridViewDataTextColumn FieldName="Id" Caption="Id" Visible="False" VisibleIndex="0">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="InActives" Caption="وضعیت" VisibleIndex="0">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="MfId" Caption="MfId" Visible="False" VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="MlName" Width="200px" Caption="رشته تحصیلی"
                                                        VisibleIndex="0">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="FMjName" Width="200px" Caption="رشته موضوع پروانه"
                                                        VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="MjType" Caption="موضوع پروانه" VisibleIndex="2">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="MailNo" Caption="شماره نامه" VisibleIndex="3"
                                                        Width="100px">
                                                        <EditCellStyle Wrap="False">
                                                        </EditCellStyle>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="MailDate" Caption="تاریخ نامه" VisibleIndex="3">
                                                        <EditCellStyle Wrap="False">
                                                        </EditCellStyle>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="UnCount" Caption="کشور" VisibleIndex="3">
                                                        <EditCellStyle Wrap="False">
                                                        </EditCellStyle>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="UnName" Width="200px" Caption="دانشگاه"
                                                        VisibleIndex="3">
                                                        <EditCellStyle Wrap="False">
                                                        </EditCellStyle>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="UnEndDate" Caption="فارغ التحصیل" VisibleIndex="3">
                                                        <EditCellStyle Wrap="False">
                                                        </EditCellStyle>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="UnGrade" Caption="معدل" VisibleIndex="3">
                                                        <EditCellStyle Wrap="False">
                                                        </EditCellStyle>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="IsPrintedName" Caption="وضعیت چاپ" VisibleIndex="3">
                                                        <EditCellStyle Wrap="False">
                                                        </EditCellStyle>
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" ConfirmDelete="True"></SettingsBehavior>
                                                <Settings ShowFilterRowMenu="True" ShowGroupPanel="True" ShowHorizontalScrollBar="True"></Settings>
                                            </TSPControls:CustomAspxDevGridView2>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
                        <br />

                        <fieldset runat="server" id="RoundPanelBasicInfo">
                            <legend class="fieldset-legend" dir="rtl"><b>مشخصات پروانه</b>
                            </legend>
                            <TSPControls:CustomAspxCallbackPanel runat="server"
                                ClientInstanceName="CallbackPanelDoRegDate" Width="100%" ID="CallbackPanelDoRegDate"
                                OnCallback="CallbackPanelDoRegDate_Callback">
                                <PanelCollection>
                                    <dxp:PanelContent ID="PanelContent11" runat="server">
                                        <table id="Table3" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 100%" colspan="4" align="justify">
                                                        <dxe:ASPxLabel runat="server" Font-Bold="true" Text="نکات تاریخ صدور" ID="lblRegDateComment"
                                                            ForeColor="DarkRed" Visible="false">
                                                        </dxe:ASPxLabel>
                                                        <br />
                                                        <br />
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="نوع درخواست انتقال" ID="lblTransferType" Width="100%"
                                                            ClientInstanceName="lblTransferType">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="width: 30%">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                            ID="cmbTransferType" ClientInstanceName="cmbTransferType"
                                                            ValueType="System.String" AutoPostBack="false"
                                                            RightToLeft="True">
                                                            <Items>
                                                                <dxe:ListEditItem Value="5" Text="انتقال و صدور" Selected="true"></dxe:ListEditItem>
                                                                <dxe:ListEditItem Value="10" Text="انتقال و تمدید"></dxe:ListEditItem>
                                                            </Items>
                                                            <ItemStyle HorizontalAlign="Right" />
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
                                                    <td style="width: 15%"></td>
                                                    <td style="width: 30%"></td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="موقت/دائم" ID="ASPxLabel22">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="vertical-align: top" dir="rtl" align="right">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                            ID="cmbIsTemporary" ClientInstanceName="cmbIsTemporary" ValueType="System.String"
                                                            AutoPostBack="false" RightToLeft="True">
                                                            <ClientSideEvents SelectedIndexChanged="function(s,e){CallbackPanelDoRegDate.PerformCallback(cmbIsTemporary.GetValue());}" />
                                                            <Items>
                                                                <dxe:ListEditItem Value="0" Text="پروانه دائم"></dxe:ListEditItem>
                                                                <dxe:ListEditItem Value="1" Text="پروانه موقت"></dxe:ListEditItem>
                                                            </Items>
                                                            <ItemStyle HorizontalAlign="Right" />
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
                                                        <dxe:ASPxLabel runat="server" Text="شماره سریال گواهینامه" ID="ASPxLabel1">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" Width="100%"
                                                            ID="txtSerialNo">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                                <RegularExpression ValidationExpression="\d*" ErrorText="شماره سریال را صحیح وارد نمایید" />
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ صدور" ID="lblRegDate">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                                        <pdc:PersianDateTextBox runat="server" DefaultDate=""  ShowPickerOnTop="True"
                                                                            ID="txtRegDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" onchange="return SetMeDocDefualtExpireDateJS();"></pdc:PersianDateTextBox>
                                                     
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ پایان اعتبار" ID="ASPxLabel14">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <pdc:PersianDateTextBox runat="server" DefaultDate=""  ShowPickerOnTop="True"
                                                            ID="txtExpDate" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" align="center" valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="پایه طراحی" ID="ASPxLabel21">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" Width="100%" Enabled="false"
                                                            ID="txtGradeDes">
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="پایه نظارت" ID="ASPxLabel23">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" Width="100%" Enabled="false"
                                                            ID="txtGradeObs">
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
                                                        <dxe:ASPxLabel runat="server" Text="پایه اجرا" ID="ASPxLabel24">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" Width="100%" Enabled="false"
                                                            ID="txtGradeImp">
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="پایه شهرسازی" ID="ASPxLabel10">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" Width="100%" Enabled="false"
                                                            ID="txtGradeUrbanism">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="پایه نقشه برداری" ID="ASPxLabel36">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" Width="100%" Enabled="false"
                                                            ID="txtGradeMapping">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="پایه ترافیک" ID="ASPxLabel39">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" Width="100%" Enabled="false"
                                                            ID="txtGradeTraffic">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="شماره پروانه" ID="ASPxLabel40">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" Width="100%" Enabled="false" Style="direction: ltr"
                                                            RightToLeft="True" ID="lblMFNo">
                                                            <ValidationSettings>
                                                                <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                </ErrorImage>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ تحویل گواهینامه" ID="lblLivertyDate">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <TSPControls:CustomTextBox runat="server" Width="100%" ClientEnabled="false"
                                                            Style="direction: ltr" RightToLeft="True"
                                                            ID="txtLivertyDate" ClientInstanceName="txtLivertyDate">
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                </tr>




                                                <tr>
                                                    <td align="right" valign="top">
                                                        <dxe:ASPxLabel runat="server" Text="تصویر روی پروانه پیشین" ID="lblImgFrontOldDoc" ClientVisible="true">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td colspan="3" align="right" valign="top">
                                                        <table dir="rtl">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxUploadControl runat="server" ID="flpFrontOldDoc" InputType="Images"
                                                                            UploadWhenFileChoosed="true" ClientInstanceName="flpFrontOldDoc" OnFileUploadComplete="flpAttach_FileUploadComplete"
                                                                            ClientVisible="false">
                                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                        if(e.isValid){
	ImgEndFrontOldDoc.SetVisible(true);
	HiddenFieldDocMemberFile.Set('ImgFrontOld',1);
	lblValidationFImgOldDoc.SetVisible(false);
    hpImgFrontOldDoc.SetVisible(true);
	hpImgFrontOldDoc.SetImageUrl('../../Image/DocMeFile/OldDoc/'+e.callbackData);
	}
	else{
    HiddenFieldDocMemberFile.Set('ImgFrontOld',0);
	ImgEndFrontOldDoc.SetVisible(false);
	lblValidationFImgOldDoc.SetVisible(true);
	}
}"></ClientSideEvents>
                                                                        </TSPControls:CustomAspxUploadControl>
                                                                        <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                                            ID="lblValidationFImgOldDoc" ForeColor="Red" ClientInstanceName="lblValidationFImgOldDoc">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                            ID="ImgEndFrontOldDoc" ClientInstanceName="ImgEndFrontOldDoc">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="hpImgFrontOldDoc" ClientInstanceName="hpImgFrontOldDoc"
                                                            Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                                            <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                                            </EmptyImage>
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td align="right" valign="top">
                                                        <dxe:ASPxLabel runat="server" Text="تصویر پشت پروانه پیشین" ID="lblImgBackOldDoc" ClientVisible="true">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td colspan="3" align="right" valign="top">
                                                        <table dir="rtl">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxUploadControl runat="server" ID="flpBackOldDoc" InputType="Images"
                                                                            UploadWhenFileChoosed="true" ClientInstanceName="flpBackOldDoc" OnFileUploadComplete="flpAttach_FileUploadComplete"
                                                                            ClientVisible="False">
                                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
    if(e.isValid){
    HiddenFieldDocMemberFile.Set('ImgBackOld',1);   
	ImgEndBackOldDoc.SetVisible(true);
	lblValidationBImgOldDoc.SetVisible(false);
    hpImgBackOldDoc.SetVisible(true);
    hpImgBackOldDoc.SetImageUrl('../../Image/DocMeFile/OldDoc/'+e.callbackData);
	}
	else{
    HiddenFieldDocMemberFile.Set('ImgBackOld',0);
	ImgEndBackOldDoc.SetVisible(false);
	lblValidationBImgOldDoc.SetVisible(true);
	}
}"></ClientSideEvents>
                                                                        </TSPControls:CustomAspxUploadControl>
                                                                        <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                                            ID="lblValidationBImgOldDoc" ForeColor="Red" ClientInstanceName="lblValidationBImgOldDoc">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                            ID="ImgEndBackOldDoc" ClientInstanceName="ImgEndBackOldDoc">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" Text="تصویر پشت پروانه پیشین"
                                                            ID="hpImgBackOldDoc" ClientInstanceName="hpImgBackOldDoc" Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                                            <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                                            </EmptyImage>
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="top">
                                                        <dxe:ASPxLabel runat="server" Text="تصویر استعلام از اداره امور مالیاتی" ID="lblImgTaxOfficeLetter" ClientVisible="true">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td colspan="3" align="right" valign="top">
                                                        <table dir="rtl">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxUploadControl runat="server" ID="flpTaxOfficeLetter" InputType="Images"
                                                                            UploadWhenFileChoosed="true" ClientInstanceName="flpTaxOfficeLetter" OnFileUploadComplete="flpAttach_FileUploadComplete"
                                                                            ClientVisible="False">
                                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
    if(e.isValid){
    HiddenFieldDocMemberFile.Set('ImgTaxOfficeLetter',1);   
	ImgEndTaxOfficeLetter.SetVisible(true);
	lblValidationTaxOfficeLetter.SetVisible(false);
    hpImgTaxOfficeLetter.SetVisible(true);
    hpImgTaxOfficeLetter.SetImageUrl('../../Image/DocMeFile/TaxOffice/'+e.callbackData);
	}
	else{
    HiddenFieldDocMemberFile.Set('ImgTaxOfficeLetter',0);
	ImgEndTaxOfficeLetter.SetVisible(false);
	lblValidationTaxOfficeLetter.SetVisible(true);
	}
}"></ClientSideEvents>
                                                                        </TSPControls:CustomAspxUploadControl>
                                                                        <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                                            ID="lblValidationTaxOfficeLetter" ForeColor="Red" ClientInstanceName="lblValidationTaxOfficeLetter">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                            ID="ImgEndTaxOfficeLetter" ClientInstanceName="ImgEndTaxOfficeLetter">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" Text="تصویر استعلام اداره امور مالیاتی"
                                                            ID="hpImgTaxOfficeLetter" ClientInstanceName="hpImgTaxOfficeLetter" Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                                            <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                                            </EmptyImage>
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td align="right" valign="top">
                                                        <dxe:ASPxLabel runat="server" Text="تصویر گواهینامه دوره جوش" ID="lblJooshPeriod" ClientVisible="true">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td colspan="3" align="right" valign="top">
                                                        <table dir="rtl">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxUploadControl runat="server" ID="flpJooshPeriod" InputType="Images"
                                                                            UploadWhenFileChoosed="true" ClientInstanceName="flpJooshPeriod" OnFileUploadComplete="flpAttach_FileUploadComplete"
                                                                            ClientVisible="False">
                                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
    if(e.isValid){
    HiddenFieldDocMemberFile.Set('JooshPeriod',1);                                                                                                             
	ImgEndJooshPeriod.SetVisible(true);  
	lblValidationJooshPeriod.SetVisible(false);  
    hpImgJooshPeriod.SetVisible(true);  
    hpImgJooshPeriod.SetImageUrl('../../Image/DocMeFile/JooshPeriod/'+e.callbackData);  

	}
	else{
    HiddenFieldDocMemberFile.Set('JooshPeriod',0);
	ImgEndJooshPeriod.SetVisible(false);
	lblValidationJooshPeriod.SetVisible(true);
	}
           
}"></ClientSideEvents>
                                                                        </TSPControls:CustomAspxUploadControl>
                                                                        <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                                            ID="lblValidationJooshPeriod" ForeColor="Red" ClientInstanceName="lblValidationJooshPeriod">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                            ID="ImgEndJooshPeriod" ClientInstanceName="ImgEndJooshPeriod">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" Text="تصویر گواهینامه دوره جوش"
                                                            ID="hpImgJooshPeriod" ClientInstanceName="hpImgJooshPeriod" Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                                            <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                                            </EmptyImage>
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>


                                                      <tr>
                                                    <td align="right" valign="top">
                                                        <dxe:ASPxLabel runat="server" Text="تصویر HSE" ID="lblHse" ClientVisible="true">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td colspan="3" align="right" valign="top">
                                                        <table dir="rtl">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxUploadControl runat="server" ID="flpHse" InputType="Images"
                                                                            UploadWhenFileChoosed="true" ClientInstanceName="flpHse" OnFileUploadComplete="flpAttach_FileUploadComplete"
                                                                            ClientVisible="False">
                                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
    if(e.isValid){
                                                                               
    HiddenFieldDocMemberFile.Set('Hse',1);                                                                                                             
	ImgEndHse.SetVisible(true);  
	lblValidationHse.SetVisible(false);  
    hpImgHse.SetVisible(true);  
    hpImgHse.SetImageUrl('../../Image/DocMeFile/Hse/'+e.callbackData);  

	}
	else{
    HiddenFieldDocMemberFile.Set('Hse',0);
	ImgEndHse.SetVisible(false);
	lblValidationHse.SetVisible(true);
	}
           
}"></ClientSideEvents>
                                                                        </TSPControls:CustomAspxUploadControl>
                                                                        <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                                            ID="lblValidationHse" ForeColor="Red" ClientInstanceName="lblValidationHse">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                            ID="ImgEndHse" ClientInstanceName="ImgEndHse">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" Text="تصویر HSE"
                                                            ID="hpImgHse" ClientInstanceName="hpImgHse" Border-BorderWidth="1px" Border-BorderStyle="Solid" EnableClientSideAPI="True">
                                                            <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                                            </EmptyImage>
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </TSPControls:CustomAspxCallbackPanel>
                        </fieldset>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>

            <br />
            <fieldset runat="server" id="RoundPanelAccounting">
                <legend class="fieldset-legend" dir="rtl"><b>مشخصات فیش</b>
                </legend>


                <TSPControls:CustomAspxDevGridView2 runat="server" Width="100%"
                    ID="GridViewAccounting" KeyFieldName="AccountingId" AutoGenerateColumns="False"
                    OnHtmlRowPrepared="GridViewAccounting_HtmlRowPrepared"
                    ClientInstanceName="grid">
                    <Settings ShowHorizontalScrollBar="true" />
                    <Columns>

                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="AccType"
                            Caption="بابت">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="AccTypeName" Caption="بابت"
                            Width="250px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="StatusName" Caption="وضعیت پرداخت"
                            Width="80px">
                        </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="ReferenceId" Caption="شماره رهگیری بانک"
                            Width="150px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Number" Caption="شماره"
                            Width="200px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Date" Caption="تاریخ" Width="90px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Amount" Caption="مبلغ"
                            Width="100px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                            <PropertiesTextEdit DisplayFormatString="#,#">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5"  FieldName="Description" Caption="توضیحات"
                            Width="150px">
                            <CellStyle Wrap="True">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <ClientSideEvents EndCallback="function(s,e){
                                        if(s.cpSaveComplete=='1'){
                                         ClearAccounting();
                                         s.cpSaveComplete='0';
                                         }
                                        else if(s.cpMessage!='')
                                        {
                                         ShowMessage(s.cpMessage);
                                         s.cpMessage='';
                                        }
                                        }" />
                </TSPControls:CustomAspxDevGridView2>

            </fieldset>

            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelFooter" runat="server" Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        
                            <table >
                                <tbody>
                                    <tr>

                                        <td > 
                                              <asp:LinkButton ID="btnEdit2" CssClass="ButtonMenue" OnClick="btnEdit_Click" runat="server">ویرایش درخواست</asp:LinkButton>
                                        </td>
                                        <td>
                                           
                                             <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="ذخیره"  ToolTip="ذخیره"
                                                Width="25px" ID="btnSave2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnSave_Click">
                                                   <ClientSideEvents Click="function(s, e) {                                                  
                                                 
                                                      return e.processOnServer= confirm('پس از ذخیره اطلاعات و اطمینان از تکمیل تمام اطلاعات بایستی در صفحه مدیریت درخواست های پروانه  گزينه گردش كار را انتخاب كرده و درخواست خود را به كارمند واحد عضويت و پروانه ارسال كنيد. در غیر این صورت روند بررسی پرونده شما شروع نمی شود');
                                                      btnSave(e); 
                                                      }"></ClientSideEvents>


                                             </TSPControls:CustomAspxButton>   
                                        </td>                                     
                                        <td>
                                             <asp:LinkButton ID="btnBack2" CssClass="ButtonMenue" OnClick="btnBack_Click" runat="server">مدیریت پروانه اشتغال</asp:LinkButton>     
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldDocMemberFile" ClientInstanceName="HiddenFieldDocMemberFile">
            </dxhf:ASPxHiddenField>

            <asp:ObjectDataSource ID="ObjdsMajor" runat="server" TypeName="TSP.DataManager.MajorManager"
                SelectMethod="FindMajorParent">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="MjId"></asp:Parameter>
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
