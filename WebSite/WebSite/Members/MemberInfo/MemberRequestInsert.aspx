<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MemberRequestInsert.aspx.cs" Inherits="Members_MemberInfo_MemberRequestInsert"
    Title="درخواست تغییرات" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <script language="javascript" type="text/javascript">
        function SetKardani(Visible) {

            panelKardani.SetVisible(Visible);
        }

        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
        function ClearAttachments() {
            imgEndUploadImgClient.SetVisible(false);
            txtDescImg.SetText("");
        }
        function SetSexMen() {
            lblSolStatus.SetVisible(true);
            drdSoldier.SetVisible(true);
            drdSoldier.SetSelectedIndex(-1);

            lblsolfile.SetVisible(true);
            flpSoldier.SetVisible(true);

            lblSolFileBack.SetVisible(true);
            flpSoldierBack.SetVisible(true);

            lblWarnSolImage.SetVisible(true);
            HpSoldier.SetVisible(true);
            HpSoldierBack.SetVisible(true);
        }
        function SetSexWomen() {
            lblSolStatus.SetVisible(false);
            drdSoldier.SetVisible(false);
            drdSoldier.SetSelectedIndex(-1);
            chbSoLdire.SetVisible(false);
            chbSoLdire.SetChecked(false);

            lblsolfile.SetVisible(false);
            flpSoldier.SetVisible(false);

            lblSolFileBack.SetVisible(false);
            flpSoldierBack.SetVisible(false);

            lblWarnSolImage.SetVisible(false);
            HpSoldier.SetVisible(false);
            HpSoldierBack.SetVisible(false);
        }

        function btnSave(e) {
            var birthdate = document.getElementById('<%=txtBirthDate.ClientID%>').value;
            if (birthdate == '') {
                alert('تاریخ تولد را وارد نمایید');
            }
            if (CheckCharacterEncoding(txtFirstNameEn.GetText()) == false) {
                txtFirstNameEn.SetIsValid(false);
                txtFirstNameEn.SetErrorText('حروف وارد شده نامعتبر است');
                alert('حروف وارد شده در نام انگلیسی نامعتبر است');
                e.processOnServer = false;
                return;
            }
            if (CheckCharacterEncoding(txtLastNameEn.GetText()) == false) {
                txtLastNameEn.SetIsValid(false);
                txtLastNameEn.SetErrorText('حروف وارد شده نامعتبر است');
                alert('حروف وارد شده در نام خانوادگی انگلیسی نامعتبر است');
                e.processOnServer = false;
                return;
            }
            if (flpme.Get('name') != 1 && flpme.Get('name') != 2) {
                lblPersonImageValidation.SetVisible(true);
                lblme2.SetVisible(true);
                alert('تصویر را انتخاب نمایید');
                e.processOnServer = false;
                return;
            }
            else {
                lblPersonImageValidation.SetVisible(false);
                lblme2.SetVisible(false);
            }
            if (flpmeidno.Get('name') != 1 && flpmeidno.Get('name') != 2) {
                lblIdNoImageValidation.SetVisible(true);
                lbli2.SetVisible(true);
                alert('تصویر صفحه اول شناسنامه را انتخاب نمایید');
                e.processOnServer = false;
                return;
            }
            else {
                lblIdNoImageValidation.SetVisible(false);
                lbli2.SetVisible(false);
            }

            if (flpmessn.Get('name') != 1 && flpmessn.Get('name') != 2) {
                lblSSNImagevalidation.SetVisible(true);
                lblSSN2.SetVisible(true);
                alert('تصویر روی کارت ملی را انتخاب نمایید');
                e.processOnServer = false;
                return;
            }
            else {
                lblSSNImagevalidation.SetVisible(false);
                lblSSN2.SetVisible(false);
            }

            if (flpmessn.Get('SSNBack') != 1 && flpmessn.Get('SSNBack') != 2) {
                lblValidationssBack.SetVisible(true);
                lblSSNBack.SetVisible(true);
                alert('تصویر پشت کارت ملی را انتخاب نمایید');
                e.processOnServer = false;
                return;
            }
            else {
                lblValidationssBack.SetVisible(false);
                lblSSNBack.SetVisible(false);
            }

            if (HDFlpResident.Get('name') != 1 && HDFlpResident.Get('name') != 2) {
                lblResidentImageValidation.SetVisible(true);
                lblResident.SetVisible(true);
                alert('تصویر مدرک سکونت در استان را انتخاب نمایید');
                e.processOnServer = false;
                return;
            }
            else {
                lblResidentImageValidation.SetVisible(false);
                lblResident.SetVisible(false);
            }

            if (ChEnteghali.GetChecked() == true) {
                if (flpletter.Get('name') != 1 && flpletter.Get('name') != 2) {
                    lblletter.SetVisible(true);
                    lblletter2.SetVisible(true);
                    alert('تصویر نامه را انتخاب نمایید');
                    e.processOnServer = false;
                    return;
                }
                else {
                    lblletter.SetVisible(false);
                    lblletter2.SetVisible(false);
                }
            }

            if (flpmesol.Get('name') != 1 && drdSoldier.GetVisible()) {
                lblsol.SetVisible(true);
                lblsol2.SetVisible(true);
                alert('تصویر روی کارت پایان خدمت را انتخاب نمایید');
                e.processOnServer = false;
                return;
            }
            else {
                lblsol.SetVisible(false);
                lblsol2.SetVisible(false);
            }
        }

        function DisableDocInfoControl(value) {
            if (!value) {//Disable Controls
                PanelEntegaliDoc.SetVisible(false);
            } else {//Enable Controls
                PanelEntegaliDoc.SetVisible(true);
            }
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div align="right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" Text="ذخیره" ToolTip="ذخیره"
                                            ID="btnSave" EnableViewState="False" ValidationGroup="Member" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {                                                  
                                                    if (ASPxClientEdit.ValidateGroup('Member') == false)
                                                     {
                                                       alert('لطفا موارد ذکر شده در بالای صفحه را تکمیل نمائید');
                                                       return;
                                                     }
                                                      return e.processOnServer= confirm('پس از ذخیره اطلاعات و اطمینان از تکمیل تمام اطلاعات بایستی در صفحه مدیریت درخواست های عضویت  گزينه گردش كار را انتخاب كرده و درخواست خود را به كارمند واحد عضويت ارسال كنيد. در غیر این صورت روند بررسی پرونده شما شروع نمی شود');
                                                      btnSave(e); 
                                                      }"></ClientSideEvents>
                                      
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td valign="top" align="right">
                                        <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" Text="بازگشت" ToolTip="بازگشت"
                                            CausesValidation="False" ID="btnBack" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnBack_Click">
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server"
                CssClass="ProjectMainMenuHorizontal"
                OnItemClick="ASPxMenu1_ItemClick">
                <Items>
                    <dxm:MenuItem Name="Member" Text="مشخصات عضو" Selected="true" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مدارک تحصیلی" Name="Madrak" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="سوابق کاری" Name="Job" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="فعالیت ها" Name="Activity" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="زبان ها" Name="Language" ItemStyle-CssClass="HorizontalMainMenuItemStyle">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>
            </div>
            <div align="right">
                <ul class="HelpUL">
                    <li>جهت تغییر نام/نام خانوادگی/نمایندگی و یا شماره حساب با در دست داشتن مدارک لازم به
                        سازمان مراجعه نمایید. </li>
                    <li>پس از تغییر اطلاعات خود در این صفحه جهت اعمال تغییرات انجام شده بر روی دکمه ''ذخیره''
                       واقع در منوی بالا/پایین صفحه کلیک نمایید. </li>
                    <li>جهت بازگشت به صفحه قبل بر روی دکمه ''بازگشت''  واقع در منوی بالا/پایین صفحه کلیک نمایید. </li>
                    <li><b>توجه!!!!
                        پس از ذخیره اطلاعات و اطمینان از تکمیل تمام اطلاعات بایستی در ''صفحه مدیریت درخواست های عضویت '' گزينه گردش كار را انتخاب كرده و درخواست خود را به كارمند واحد عضويت ارسال كنيد. در غیر این صورت روند بررسی پرونده شما شروع نمی شود.
                    </b></li>
                </ul>
            </div>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelPage" HeaderText="مشخصات عضو" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <fieldset runat="server" id="RoundPanel">
                            <legend class="fieldset-legend" dir="rtl"><b>اطلاعات پایه</b>
                            </legend>
                            <dxp:ASPxPanel runat="server" ID="RoundPanelBaseInfo">
                                <PanelCollection>
                                    <dxp:PanelContent>
                                        <table width="100%">
                                            <tr>
                                                <td valign="top" align="center" colspan="4">
                                                    <dxe:ASPxValidationSummary ID="ASPxValidationSummary1" runat="server" ShowErrorAsLink="true"
                                                        ShowErrorsInEditors="true" RenderMode="BulletedList" HorizontalAlign="Right"
                                                        ValidationGroup="Member" RightToLeft="True">
                                                    </dxe:ASPxValidationSummary>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="center" colspan="4">
                                                    <dxe:ASPxLabel runat="server" Text="تصویر صفحه اول شناسنامه را انتخاب نمایید" ClientVisible="False"
                                                        ID="lbli2" ForeColor="Red" ClientInstanceName="lbli2">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="center" colspan="4">
                                                    <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                        ID="ASPxLabel6" ForeColor="Red" ClientInstanceName="lblme2">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="center" colspan="4">
                                                    <dxe:ASPxLabel runat="server" Text="تصویر امضا را انتخاب نمایید" ClientVisible="False"
                                                        ID="ASPxLabel7" ForeColor="Red" ClientInstanceName="lblsign2">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="center" colspan="4">
                                                    <dxe:ASPxLabel runat="server" Text="تصویر روی کارت ملی را انتخاب نمایید" ClientVisible="False"
                                                        ID="ASPxLabel9" ForeColor="Red" ClientInstanceName="lblSSN2">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="center" colspan="4">
                                                    <dxe:ASPxLabel runat="server" Text="تصویر پشت کارت ملی را انتخاب نمایید" ClientVisible="False"
                                                        ID="ASPxLabel5" ForeColor="Red" ClientInstanceName="lblSSNBack">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="center" colspan="4">
                                                    <dxe:ASPxLabel runat="server" Text="تصویر مدرک سکونت در استان را انتخاب نمایید" ClientVisible="False"
                                                        ID="lblResident" ForeColor="Red" ClientInstanceName="lblResident">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="center" colspan="4">
                                                    <dxe:ASPxLabel runat="server" Text="تصویر نامه را انتخاب نمایید" ClientVisible="False"
                                                        ID="ASPxLabel20" ForeColor="Red" ClientInstanceName="lblletter2">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="center" colspan="4">
                                                    <dxe:ASPxLabel runat="server" Text="تصویر روی کارت پایان خدمت را انتخاب نمایید" ClientVisible="False"
                                                        ID="ASPxLabel19" ForeColor="Red" ClientInstanceName="lblsol2">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="center" colspan="4">
                                                    <dxe:ASPxLabel runat="server" Text="وضعیت درخواست:" Font-Bold="false" ID="lblWorkFlowState"
                                                        ForeColor="Red" Font-Size="9pt">
                                                    </dxe:ASPxLabel>
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="شرح درخواست" ID="Label60"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" colspan="3">
                                                    <TSPControls:CustomASPXMemo runat="server" Height="70px" ID="txtDesc" Width="100%"
                                                        ClientInstanceName="txtDesc">
                                                        <ValidationSettings ValidationGroup="Member">

                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomASPXMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right" width="15%">
                                                    <asp:Label runat="server" Text="شماره عضویت" ID="Label13"></asp:Label>
                                                </td>
                                                <td valign="top" dir="ltr" align="right" width="35%">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtMeNo" Width="100%" Enabled="False">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="شماره عضویت را وارد نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right" width="15%"></td>
                                                <td valign="top" dir="ltr" align="right" width="35%"></td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="نام *" ID="Label1"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtFirstName" Width="100%"
                                                        ClientInstanceName="txtFirstName" Enabled="false">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="نام خانوادگی*" ID="Label22"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtLastName" Width="100%"
                                                        ClientInstanceName="txtLastName" Enabled="false">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

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
                                                    <asp:Label runat="server" Text="نام(انگلیسی)*" ID="Label61"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtFirstNameEn" Width="100%"
                                                        ClientInstanceName="txtFirstNameEn">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="نام (انگلیسی) را وارد نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="نام خانوادگی(انگلیسی) *" ID="Label62"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtLastNameEn" Width="100%"
                                                        ClientInstanceName="txtLastNameEn">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="نام خانوادگی (انگلیسی) را وارد نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td runat="server" id="Td7" valign="top" align="right">
                                                    <asp:Label runat="server" Text="شماره شناسنامه *" ID="Label6"></asp:Label>
                                                </td>
                                                <td runat="server" valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtIdNo" Width="100%" MaxLength="10"
                                                        ClientInstanceName="txtIdNo">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="شماره شناسنامه را وارد نمایید"></RequiredField>
                                                            <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,10}"></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td runat="server" valign="top" align="right">
                                                    <asp:Label runat="server" Text="کد ملی*" ID="Label7"></asp:Label>
                                                </td>
                                                <td runat="server" valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtSSN" Width="100%" MaxLength="10"
                                                        ClientInstanceName="txtSSN">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

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
                                                <td runat="server" valign="top" align="right">
                                                    <asp:Label runat="server" Text="نام پدر*" ID="Label8"></asp:Label>
                                                </td>
                                                <td runat="server" valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtFatherName" Width="100%"
                                                        ClientInstanceName="txtFatherName">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="نام پدر را وارد نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td runat="server" valign="top" align="right">
                                                    <asp:Label runat="server" Text="محل صدور*" ID="Label9"></asp:Label>
                                                </td>
                                                <td runat="server" valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtIssuePlace" Width="100%"
                                                        ClientInstanceName="txtIssuePlace">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="محل صدور را وارد نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td runat="server" valign="top" align="right">
                                                    <asp:Label runat="server" Text="تاریخ تولد*" ID="Label11"></asp:Label>
                                                </td>
                                                <td runat="server" valign="top" align="right">
                                                    <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                        Width="254px" ShowPickerOnTop="True" ID="txtBirthDate" PickerDirection="ToRight"
                                                        IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right" RightToLeft="False"></pdc:PersianDateTextBox>
                                                    <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                        ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtBirthDate" ValidationGroup="Member"
                                                        ID="PersianDateValidator1">تاریخ نامعتبر</pdc:PersianDateValidator>
                                                </td>
                                                <td runat="server" valign="top" align="right">
                                                    <asp:Label runat="server" Text="محل تولد *" ID="Label12"></asp:Label>
                                                </td>
                                                <td runat="server" valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtBirhtPlace" Width="100%"
                                                        ClientInstanceName="txtBirhtPlace">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="False" ErrorText="محل تولد را وارد نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="آدرس محل سکونت*" Width="100%" ID="Label43"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" colspan="3">
                                                    <TSPControls:CustomASPXMemo runat="server" Height="50px" ID="txtHomeAdr" Width="100%"
                                                        ClientInstanceName="txtHomeAdr">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="آدرس را وارد نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomASPXMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="شماره تلفن محل سکونت*" Width="100%" ID="Label41"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td style="vertical-align: top; text-align: right" width="70%">
                                                                    <TSPControls:CustomTextBox runat="server" ID="txtHometel" Width="100%"
                                                                        MaxLength="8" ClientInstanceName="txtHometel">
                                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                                            <RequiredField IsRequired="True" ErrorText="شماره تلفن را وارد نمایید"></RequiredField>
                                                                            <RegularExpression ErrorText="شماره تلفن را صحیح وارد نمایید" ValidationExpression="\d{5,8}"></RegularExpression>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: right" width="5%">
                                                                    <asp:Label runat="server" Text="-" ID="Label65"></asp:Label>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: right" width="25%">
                                                                    <TSPControls:CustomTextBox runat="server" ID="txtHometel_cityCode" Width="100%"
                                                                        MaxLength="4" ClientInstanceName="txtHometel_cityCode">
                                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                                            <RegularExpression ErrorText="پیش شماره تلفن را صحیح وارد نمایید" ValidationExpression="(0)\d{2,3}"></RegularExpression>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="شماره همراه*" ID="Label42"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtMobileNo" Width="100%"
                                                        MaxLength="11" ClientInstanceName="txtMobileNo">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="شماره همراه را وارد نمایید"></RequiredField>
                                                            <RegularExpression ErrorText="شماره همراه را صحیح وارد نمایید" ValidationExpression="0\d{1,10}"></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="آدرس محل کار" ID="Label47"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" colspan="3">
                                                    <TSPControls:CustomASPXMemo runat="server" Height="50px" ID="txtWorkAdr" Width="100%"
                                                        ClientInstanceName="txtWorkAdr">
                                                        <ValidationSettings ValidationGroup="Member">

                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomASPXMemo>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="تلفن محل کار" ID="Label45"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td style="vertical-align: top; text-align: right" width="70%">
                                                                    <TSPControls:CustomTextBox runat="server" ID="txtWorkTel" Width="100%"
                                                                        MaxLength="8" ClientInstanceName="txtWorkTel">
                                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                                            <RequiredField ErrorText="شماره تلفن را وارد نمایید"></RequiredField>
                                                                            <RegularExpression ErrorText="شماره تلفن را صحیح وارد نمایید" ValidationExpression="\d{5,8}"></RegularExpression>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: right" width="5%">
                                                                    <asp:Label runat="server" Text="-" ID="Label2"></asp:Label>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: right" width="25%">
                                                                    <TSPControls:CustomTextBox runat="server" ID="txtWorkTel_cityCode" Width="100%"
                                                                        MaxLength="4" ClientInstanceName="txtWorkTel_cityCode">
                                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                                            <RegularExpression ErrorText="پیش شماره تلفن را صحیح وارد نمایید" ValidationExpression="(0)\d{2,3}"></RegularExpression>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="شماره فکس" ID="Label46"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td style="vertical-align: top; text-align: right" width="70%">
                                                                    <TSPControls:CustomTextBox runat="server" ID="txtFaxNo" Width="100%" MaxLength="8"
                                                                        ClientInstanceName="txtFaxNo">
                                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                                            <RequiredField ErrorText=""></RequiredField>
                                                                            <RegularExpression ErrorText="شماره فکس را صحیح وارد نمایید" ValidationExpression="\d{5,8}"></RegularExpression>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: right" width="5%">
                                                                    <asp:Label runat="server" Text="-" ID="Label3"></asp:Label>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: right" width="25%">
                                                                    <TSPControls:CustomTextBox runat="server" ID="txtFaxNo_cityCode" Width="100%"
                                                                        MaxLength="4" ClientInstanceName="txtFaxNo_cityCode">
                                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                                            <RegularExpression ErrorText="پیش شماره فکس را صحیح وارد نمایید" ValidationExpression="(0)\d{2,3}"></RegularExpression>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </TSPControls:CustomTextBox>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="کد پستی محل سکونت" Width="100%" ID="Label44"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtHomePO" Width="100%" MaxLength="10"
                                                        ClientInstanceName="txtHomePO">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RegularExpression ErrorText="کد پستی را صحیح وارد نمایید" ValidationExpression="^\d{10}$"></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="کد پستی محل کار" Width="100%" ID="Label48"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtWorkPO" Width="100%" MaxLength="10"
                                                        ClientInstanceName="txtWorkPO">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RegularExpression ErrorText="کد پستی را صحیح وارد نمایید" ValidationExpression="^\d{10}$"></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="جنسیت*" ID="Label15"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%" TextField="SexName" ID="drdSexId" DataSourceID="ODBSex"
                                                        RightToLeft="True" EnableClientSideAPI="True" ValueType="System.String" ValueField="SexId"
                                                        ClientInstanceName="chbSex" EnableIncrementalFiltering="True">
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
		if (chbSex.GetSelectedIndex()== 0)
        {
           SetSexWomen();
        }
        else
        { 
			SetSexMen();
        }
      
}"></ClientSideEvents>
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="جنسیت را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="وضعیت سربازی*" ID="lblSolStatus" ClientInstanceName="lblSolStatus">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%" TextField="SoName" ID="drdSoId" DataSourceID="ODBSo"
                                                        RightToLeft="True" ValueType="System.String" ValueField="SoId" ClientInstanceName="drdSoldier"
                                                        EnableIncrementalFiltering="True">
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
   if (drdSoldier.GetSelectedIndex() == 3 || drdSoldier.GetSelectedIndex() == 4 || drdSoldier.GetSelectedIndex() == 5)
                {
                   chbSoLdire.SetChecked(false);
                   chbSoLdire.SetVisible(true);
                }
                
            else
            { 
              chbSoLdire.SetVisible(false);
              chbSoLdire.SetChecked(false);
            }
      
}"></ClientSideEvents>
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="وضعیت سربازی را انتخاب نمایید"></RequiredField>
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
                                                <td colspan="4">
                                                    <TSPControls:CustomASPxCheckBox runat="server" Text="اينجانب متعهد مي گردم درصورت قبولي در آزمون ورود به حرفه مهندسي و با علم به اينكه، براساس ابلاغ، اصلاحيه آيين نامه اجرايي قانون نظام مهندسي و كنترل ساختمان در تاريخ 94/12/20، ارائه كارت پايان خدمت يا كارت معافيت از خدمت جهت صدور/تمديد پروانه اشتغال الزامي مي باشد تصوير كارت مذكور را در سايت نظام مهندسي آپلود كرده و تصوير برابر اصل آن را تحويل واحد عضويت و پروانه اشتغال (حقيقي وحقوقي) بدهم در غيراينصورت امكان صدور پروانه اشتغال وجود نداشته و حق هرگونه اعتراضي را ازخود سلب مي نمايم" EnableClientSideAPI="True"
                                                        ID="chbSoLdire" ClientInstanceName="chbSoLdire" ClientVisible="false">
                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom"
                                                            ErrorDisplayMode="ImageWithTooltip">

                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                            <RequiredField IsRequired="true" ErrorText="تعهد مربوط به وضعیت سربازی مورد موافقت قرار نگرفته است" />
                                                        </ValidationSettings>
                                                    </TSPControls:CustomASPxCheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="وضعیت تأهل*" ID="Label51"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                        TextField="MarName" ID="drdMarId" DataSourceID="ODBMar"
                                                        RightToLeft="True" ValueType="System.String" ValueField="MarId" ClientInstanceName="drdMarId"
                                                        EnableIncrementalFiltering="True">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="وضعیت تاهل را انتخاب نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>
                                                </td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="محل اقامت *" ID="Label4"></asp:Label>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                        TextField="CitName" ID="drdCitId" DataSourceID="OdbCity"
                                                        RightToLeft="True" ValueType="System.String" ValueField="CitId" ClientInstanceName="drdCitId"
                                                        EnableIncrementalFiltering="True">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="محل اقامت را وارد نمایید"></RequiredField>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                        <ButtonStyle Width="13px">
                                                        </ButtonStyle>
                                                    </TSPControls:CustomAspxComboBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="نمایندگی *" ID="ASPxLabel1">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomAspxComboBox runat="server" Width="100%" TextField="Name" ID="drdAgent" DataSourceID="OdbAgent"
                                                        RightToLeft="True" ValueType="System.String" ValueField="AgentId" ClientInstanceName="drdAgent"
                                                        Enabled="false" EnableIncrementalFiltering="True">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField IsRequired="True" ErrorText="نمایندگی را انتخاب نمایید"></RequiredField>
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
                                                <td align="right" valign="top">
                                                    <asp:Label runat="server" Text="شماره حساب " ID="Label49"></asp:Label>
                                                </td>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomTextBox runat="server" Enabled="false" Width="100%"
                                                        MaxLength="16" ID="txtBankAccNo"
                                                        ClientInstanceName="txtBankAccNo">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="\d{1,16}"></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                                <td valign="top" align="right">
                                                    <dxe:ASPxLabel runat="server" Text="کد نظارت شهرداری" ID="ASPxLabel29" Width="100%">
                                                    </dxe:ASPxLabel>
                                                </td>
                                                <td valign="top" align="right">
                                                    <TSPControls:CustomTextBox runat="server" Width="100%" MaxLength="20" ID="txtArchitectorCode"
                                                        ClientInstanceName="txtArchitectorCode">
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="آدرس وب سایت" ID="Label57"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" colspan="3">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtWebsite" Width="100%"
                                                        ClientInstanceName="txtWebsite">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RegularExpression ErrorText="آدرس وب سایت را با فرمت http://www.Example.com وارد نمایید"
                                                                ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="right">
                                                    <asp:Label runat="server" Text="آدرس پست الکترونیکی *" ID="Label58"></asp:Label>
                                                </td>
                                                <td valign="top" align="right" colspan="3">
                                                    <TSPControls:CustomTextBox runat="server" ID="txtEmail" Width="100%" ClientInstanceName="txtEmail">
                                                        <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                            <RequiredField ErrorText="آدرس پست الکترونیکی را وارد نمایید"></RequiredField>
                                                            <RegularExpression ErrorText="آدرس پست الکترونیکی را با فرمت صحیح وارد نمایید" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></RegularExpression>
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </TSPControls:CustomTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxp:ASPxPanel>
                        </fieldset>
                        <fieldset runat="server" id="RoundPanelImages">
                            <legend class="fieldset-legend" dir="rtl"><b>تصاویر</b>
                            </legend>
                            <table>
                                <tr>
                                    <td runat="server" id="Td37" valign="top" align="right">
                                        <asp:Label runat="server" Text="تصویر *" ID="Label50"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td38" valign="top" align="right" colspan="3">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" ID="flpImage" InputType="Images"
                                                            UploadWhenFileChoosed="true" ClientInstanceName="flpimg" OnFileUploadComplete="flpImage_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                if(e.isValid){
	imgEndUploadImgClient2.SetVisible(true);
	 flpme.Set('name',1);
	lblPersonImageValidation.SetVisible(false);
    lblme2.SetVisible(false);
	meImg.SetVisible(true);
	meImg.SetImageUrl('../../Image/Members/Person/Request/'+e.callbackData);
    }
    else{
	imgEndUploadImgClient2.SetVisible(false);
	 flpme.Set('name',0);
	lblPersonImageValidation.SetVisible(true);
    lblme2.SetVisible(true);
	meImg.SetVisible(false);
	meImg.SetImageUrl('');
    }
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="تصویر را انتخاب نمایید" ClientVisible="False"
                                                            ID="ASPxLabel2" ForeColor="Red" ClientInstanceName="lblPersonImageValidation">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="ASPxImage3" ClientInstanceName="imgEndUploadImgClient2">
                                                        </dxe:ASPxImage>
                                                        <br />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxLabel runat="server" ID="ASPxLabelImgWarning" ForeColor="#0000C0">
                                        </dxe:ASPxLabel>
                                        <br />
                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgMember" ClientInstanceName="meImg">
                                            <Border BorderWidth="1px" BorderStyle="Solid"></Border>
                                            <EmptyImage Height="75px" Width="75px" Url="~/Images/person.gif">
                                            </EmptyImage>
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" id="Td6" valign="top" align="right">
                                        <asp:Label runat="server" Text="تصویر امضا" ID="Label5"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td8" valign="top" align="right" colspan="3">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                            ID="flpSign" InputType="images" ClientInstanceName="flpSign" OnFileUploadComplete="flpSign_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                            if(e.isValid){
	imgEndUploadImgClientSign.SetVisible(true);
	 flpmesign.Set('name',1);
	lblsignImageValidation.SetVisible(false);
    lblsign2.SetVisible(false);
	signImg.SetVisible(true);
	signImg.SetImageUrl('../../Image/Members/Sign/Request/'+e.callbackData);
	}
	else
	{
	imgEndUploadImgClientSign.SetVisible(false);
	 flpmesign.Set('name',0);
	lblsignImageValidation.SetVisible(true);
    lblsign2.SetVisible(true);
	signImg.SetVisible(false);
	signImg.SetImageUrl('');
	}
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="تصویر امضا را انتخاب نمایید" ClientVisible="False"
                                                            ID="ASPxLabel10" ForeColor="Red" ClientInstanceName="lblsignImageValidation">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="ASPxImage4" ClientInstanceName="imgEndUploadImgClientSign">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="ImgSign" ClientInstanceName="signImg">
                                            <Border BorderWidth="1px" BorderStyle="Solid"></Border>
                                            <EmptyImage Url="~/Images/noimage.gif">
                                            </EmptyImage>
                                        </dxe:ASPxImage>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تصویر صفحه اول شناسنامه *" ID="ASPxLabel11">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                            ID="flpIdNo" InputType="Images" ClientInstanceName="flpIdNo" OnFileUploadComplete="flpIdNo_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                            if(e.isValid){
	imgEndUploadImgClientIdNo.SetVisible(true);
    flpmeidno.Set('name',1);
	lblIdNoImageValidation.SetVisible(false);
	hidno.SetVisible(true);
	hidno.SetNavigateUrl('../../image/Members/IdNo/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientIdNo.SetVisible(false);
    flpmeidno.Set('name',0);
    
	lblIdNoImageValidation.SetVisible(true);
	hidno.SetVisible(false);
	hidno.SetNavigateUrl('');
	}
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="تصویر صفحه اول شناسنامه را انتخاب نمایید" ClientVisible="False"
                                                            ID="ASPxLabel12" ForeColor="Red" ClientInstanceName="lblIdNoImageValidation">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="imgEndUploadImgIdNo" ClientInstanceName="imgEndUploadImgClientIdNo">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink runat="server" Text="تصویر صفحه اول شناسنامه" Target="_blank" ID="HpIdNo"
                                            ClientInstanceName="hidno">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="تصویر صفحه دوم شناسنامه">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" colspan="3" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl ID="flpIdNoP2" runat="server" ClientInstanceName="flpIdNoP2"
                                                            InputType="Images" UploadWhenFileChoosed="true" OnFileUploadComplete="flpIdNo_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientIdNoP2.SetVisible(true);
    flpmeidno.Set('IdNoP2',1);
	lblIdNoP2.SetVisible(false);
	HIdNoP2.SetVisible(true);
	HIdNoP2.SetNavigateUrl('../../image/Members/IdNo/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientIdNoP2.SetVisible(false);
    flpmeidno.Set('IdNoP2',0);
	lblIdNoP2.SetVisible(true);
	HIdNoP2.SetVisible(false);
	HIdNoP2.SetNavigateUrl('');
	}
}" />
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="lblIdNoP2" runat="server" ClientInstanceName="lblIdNoP2" ClientVisible="False"
                                                            ForeColor="Red" Text="تصویر صفحه دوم شناسنامه را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage ID="imgEndUploadImgClientIdNoP2" runat="server" ClientInstanceName="imgEndUploadImgClientIdNoP2"
                                                            ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink ID="HIdNoP2" runat="server" ClientInstanceName="HIdNoP2" ClientVisible="False"
                                            Target="_blank" Text="تصویر صفحه دوم شناسنامه">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel21" runat="server" Text="تصویر صفحه توضیحات شناسنامه">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" colspan="3" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl ID="flpIdNoPDes" runat="server" ClientInstanceName="flpIdNoPDes"
                                                            InputType="Images" UploadWhenFileChoosed="true" OnFileUploadComplete="flpIdNo_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientIdNoPDes.SetVisible(true);
    flpmeidno.Set('IdNoPDes',1);
	lblIdNoPDes.SetVisible(false);
	HIdNoPDes.SetVisible(true);
	HIdNoPDes.SetNavigateUrl('../../image/Members/IdNo/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientIdNoPDes.SetVisible(false);
    flpmeidno.Set('IdNoPDes',0);
	lblIdNoPDes.SetVisible(true);
	HIdNoPDes.SetVisible(false);
	HIdNoPDes.SetNavigateUrl('');
	}
}" />
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="lblIdNoPDes" runat="server" ClientInstanceName="lblIdNoPDes" ClientVisible="False"
                                                            ForeColor="Red" Text="تصویر صفحه توضیحات شناسنامه را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage ID="imgEndUploadImgClientIdNoPDes" runat="server" ClientInstanceName="imgEndUploadImgClientIdNoPDes"
                                                            ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink ID="HIdNoPDes" runat="server" ClientInstanceName="HIdNoPDes" ClientVisible="False"
                                            Target="_blank" Text="تصویر صفحه توضیحات شناسنامه">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>

                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تصویر روی کارت ملی*" ID="ASPxLabel13">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                            ID="flpSSN" InputType="Images" ClientInstanceName="flpSSN" OnFileUploadComplete="flpSSN_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                            if(e.isValid){
	imgEndUploadImgClientSSN.SetVisible(true);
    flpmessn.Set('name',1);
	lblSSNImagevalidation.SetVisible(false);
    lblSSN2.SetVisible(false);
	HpSSN.SetVisible(true);
	HpSSN.SetNavigateUrl('../../image/Members/SSN/Request/'+e.callbackData);
	}
	else
	{
	imgEndUploadImgClientSSN.SetVisible(false);
    flpmessn.Set('name',0);
	lblSSNImagevalidation.SetVisible(true);
    lblSSN2.SetVisible(true);
	HpSSN.SetVisible(false);
	HpSSN.SetNavigateUrl('');
	}
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="تصویر روی کارت ملی را انتخاب نمایید" ClientVisible="False"
                                                            ID="ASPxLabel14" ForeColor="Red" ClientInstanceName="lblSSNImagevalidation">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویرانتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="imgEndUploadImgSSN" ClientInstanceName="imgEndUploadImgClientSSN">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink runat="server" Text="تصویر روی کارت ملی" Target="_blank" ID="HpSSN"
                                            ClientInstanceName="HpSSN">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel22" runat="server" Text="تصویر پشت کارت ملی*">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" colspan="3" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl ID="flpSSNBack" runat="server" ClientInstanceName="flpSSNBack"
                                                            InputType="Images" UploadWhenFileChoosed="true" OnFileUploadComplete="flpSSN_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientSSNBack.SetVisible(true);
    flpmessn.Set('SSNBack',1);
	lblValidationssBack.SetVisible(false);
	hssnBack.SetVisible(true);
    lblSSNBack.SetVisible(true);
	hssnBack.SetNavigateUrl('../../image/Members/SSN/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientSSNBack.SetVisible(false);
    flpmessn.Set('SSNBack',0);
	lblValidationssBack.SetVisible(true);
	hssnBack.SetVisible(false);
	hssnBack.SetNavigateUrl('');
	}
}" />
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="lblValidationssBack" runat="server" ClientInstanceName="lblValidationssBack" ClientVisible="False"
                                                            ForeColor="Red" Text="تصویر پشت کارت ملی را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage ID="ASPxImage2" runat="server" ClientInstanceName="imgEndUploadImgClientSSNBack"
                                                            ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink ID="hssnBack" runat="server" ClientInstanceName="hssnBack" ClientVisible="False"
                                            Target="_blank" Text="تصویر پشت کارت ملی">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>


                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="ASPxLabel27" runat="server" Text="تصویر مدرک سکونت در استان*">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td colspan="3" align="right" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                            ID="flpResident" InputType="Images" ClientInstanceName="flpResident" OnFileUploadComplete="flpResident_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientRes.SetVisible(true);
   
	HDFlpResident.Set('name',1);
	lblResidentImageValidation.SetVisible(false);
	HypLinkResident.SetVisible(true);
	HypLinkResident.SetNavigateUrl('../../image/Members/Resident/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientRes.SetVisible(false);
   
	HDFlpResident.Set('name',0);
	lblResidentImageValidation.SetVisible(true);
	HypLinkResident.SetVisible(false);
	HypLinkResident.SetNavigateUrl('');
	}
}" />
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="ASPxLabel28" runat="server" ClientInstanceName="lblResidentImageValidation" ClientVisible="False"
                                                            ForeColor="Red" Text="تصویر مدرک سکونت در استان را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="ASPxImage1" ClientInstanceName="imgEndUploadImgClientRes">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink ID="HypLinkResident" runat="server" ClientInstanceName="HypLinkResident"
                                            Target="_blank" Text="تصویر مدرک سکونت در استان">
                                        </dxe:ASPxHyperLink>
                                    </td>
                                </tr>

                                   <tr>
                                    <td colspan="4" align="right" valign="top">
                                        <TSPControls:CustomASPxCheckBox runat="server" Text="دارای مدرک تحصیلی کارشناسی ناپیوسته یا کاردانی می باشم."
                                            EnableClientSideAPI="True" ID="ChkBKardani" ClientInstanceName="ChkBKardani">
                                            <ClientSideEvents CheckedChanged="function(s, e) {
	if (ChkBKardani.GetChecked()== true)
        {
		SetKardani(true);
        }
        else
        {
		SetKardani(false);
       }
}"></ClientSideEvents>
                                        </TSPControls:CustomASPxCheckBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="4" align="right" valign="top">
                                        <dxp:ASPxPanel ID="panelKardani" runat="server" ClientVisible="false" ClientInstanceName="panelKardani">
                                            <PanelCollection>
                                                <dxp:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td colspan="2" width="100%" align="right" valign="top">
                                                                    <b style="color: blue">چنانچه دارای مدرک کارشناسی ناپیوسته یا مدرک کاردانی می باشید باید تصویر استعلام عدم عضویت در نظام کاردانی ساختمان استان فارس بارگذاری شود .</b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" width="15%" valign="top">تصویر استعلام عدم عضویت در نظام کاردانی*
                                                                </td>
                                                                <td align="right" width="85%" valign="top">
                                                                    <table>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                                        ID="flpKardani" InputType="Images" ClientInstanceName="flpKardani" OnFileUploadComplete="flpKardani_FileUploadComplete">
                                                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientKardani.SetVisible(true);
	HDFlpResident.Set('Kardani',1);
	lblKardani.SetVisible(false);
	HpKardani.SetVisible(true);
	HpKardani.SetNavigateUrl('../../image/Members/NezamKardani/Request/'+e.callbackData);	                                                                                                                                                                                 
    HiddenFieldMember.Set('MeImageKardan','~/image/Members/NezamKardani/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientKardani.SetVisible(false);
	HDFlpResident.Set('Kardani',0);
	lblKardani.SetVisible(true);
	HpKardani.SetVisible(false);
	HpKardani.SetNavigateUrl('');
	}
}" />
                                                                                    </TSPControls:CustomAspxUploadControl>
                                                                                    <dxe:ASPxLabel ID="lblKardani" runat="server" ClientInstanceName="lblKardani" ClientVisible="False"
                                                                                        ForeColor="Red" Text="تصویر استعلام عدم عضویت در نظام کاردانها را انتخاب نمایید">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                                        ID="imgEndUploadImgClientKardani" ClientInstanceName="imgEndUploadImgClientKardani">
                                                                                    </dxe:ASPxImage>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <dxe:ASPxHyperLink ID="HpKardani" runat="server" ClientInstanceName="HpKardani"
                                                                        ClientVisible="False" Target="_blank" Text="تصویر استعلام عدم عضویت در نظام کاردانها">
                                                                    </dxe:ASPxHyperLink>
                                                                </td>
                                                            </tr>


                                                        </tbody>
                                                    </table>
                                                </dxp:PanelContent>
                                            </PanelCollection>
                                        </dxp:ASPxPanel>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td colspan="4" align="right">
                                        <dxe:ASPxLabel ClientInstanceName="lblWarnSolImage" Font-Bold="true" Font-Size="8pt" runat="server" Text="جهت دریافت پروانه اشتغال به کار ثبت تصویر کارت پایان خدمت الزامی می باشد"
                                            ID="lblWarnSolImage" ForeColor="Blue">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <dxe:ASPxLabel runat="server" Text="تصویر روی کارت پایان خدمت*" ID="lblSolFile" ClientInstanceName="lblsolfile">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td valign="top" align="right" colspan="3">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                            ID="flpSoldier" InputType="Images" ClientInstanceName="flpSoldier" OnFileUploadComplete="flpSoldier_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                            if(e.isValid){
	imgEndUploadImgClientSol.SetVisible(true);
    flpmesol.Set('name',1);
	lblsol.SetVisible(false);	
    lblsol2.SetVisible(false);
	HpSoldier.SetVisible(true);
	HpSoldier.SetNavigateUrl('../../image/Members/Soldier/Request/'+e.callbackData);
	}
	else
	{
	imgEndUploadImgClientSol.SetVisible(false);
    flpmesol.Set('name',0);
	lblsol.SetVisible(true);	
    lblsol2.SetVisible(true);	
	HpSoldier.SetVisible(false);
	HpSoldier.SetNavigateUrl('');
	}
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel runat="server" Text="تصویر روی کارت پایان خدمت را انتخاب نمایید" ClientVisible="False"
                                                            ID="ASPxLabel24" ForeColor="Red" ClientInstanceName="lblsol">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویرانتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="imgEndUploadImgSol" ClientInstanceName="imgEndUploadImgClientSol">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink runat="server" Text="تصویر روی کارت پایان خدمت" ClientVisible="False"
                                            Target="_blank" ID="HpSoldier" ClientInstanceName="HpSoldier">
                                        </dxe:ASPxHyperLink>





                                    </td>
                                </tr>

                                <tr>
                                    <td align="right" valign="top">
                                        <dxe:ASPxLabel ID="lblSolFileBack" runat="server" Text="تصویر پشت کارت پایان خدمت" ClientInstanceName="lblSolFileBack">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td align="right" colspan="3" valign="top">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                            ID="flpSoldierBack" InputType="Images" ClientInstanceName="flpSoldierBack" OnFileUploadComplete="flpSoldier_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgSolBack.SetVisible(true);
   
	flpmesol.Set('SolBack',1);
	lblSolBack.SetVisible(false);
	HpSoldierBack.SetVisible(true);
	HpSoldierBack.SetNavigateUrl('../../image/Members/Soldier/Request/'+e.callbackData);
	}
	else{
	imgEndUploadImgSolBack.SetVisible(false);
   
	flpmesol.Set('SolBack',0);
	lblSolBack.SetVisible(true);
	HpSoldierBack.SetVisible(false);
	HpSoldierBack.SetNavigateUrl('');
	}
}" />
                                                        </TSPControls:CustomAspxUploadControl>
                                                        <dxe:ASPxLabel ID="lblSolBack" runat="server" ClientInstanceName="lblSolBack" ClientVisible="False"
                                                            ForeColor="Red" Text="تصویر پشت کارت پایان خدمت را انتخاب نمایید">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="imgEndUploadImgSolBack" ClientInstanceName="imgEndUploadImgSolBack">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <dxe:ASPxHyperLink ID="HpSoldierBack" runat="server" ClientInstanceName="HpSoldierBack" ClientVisible="False"
                                            Target="_blank" Text="تصویر پشت کارت پایان خدمت">
                                        </dxe:ASPxHyperLink>

                                    </td>
                                </tr>

                            </table>
                        </fieldset>
                        <br />
                        <fieldset>
                            <legend class="HelpUL" dir="rtl">
                                <TSPControls:CustomASPxCheckBox runat="server" Text="از استان دیگری به استان فارس منتقل شده ام."
                                    EnableClientSideAPI="True" ID="ChEnteghali" ClientInstanceName="ChEnteghali" ClientVisible="true">
                                    <ClientSideEvents CheckedChanged="function(s, e) {
	if (ChEnteghali.GetChecked()== true)
        {
		   PanelEnteghali.SetVisible(true);
       // CallbackPanelEnteghali.PerformCallback('true');
        }
        else
        {
		   PanelEnteghali.SetVisible(false);
      //  CallbackPanelEnteghali.PerformCallback('false');
       }
    
}"></ClientSideEvents>
                                </TSPControls:CustomASPxCheckBox>
                            </legend>

                            <dxp:ASPxPanel ID="PanelEnteghali" ClientInstanceName="PanelEnteghali" runat="server"
                                Width="100%">
                                <PanelCollection>
                                    <dxp:PanelContent>
                                        <table id="Table1" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 15%" align="right" valign="top">نوع انتقالی
                                                    </td>
                                                    <td style="width: 35%" align="right" valign="top">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                            RightToLeft="True" ID="CmbTransferStatus" ValueType="System.String"
                                                            ClientInstanceName="CmbTransferStatus"
                                                            SelectedIndex="-1" TextField="" ValueField="" EnableIncrementalFiltering="true">
                                                            <Items>
                                                                <dxe:ListEditItem Text="انتقال از دیگر استان به استان فارس" Value="1" />
                                                                <dxe:ListEditItem Text="انتقال از فارس به استان دیگر" Value="2" />
                                                                <dxe:ListEditItem Text="بازگشت به استان فارس" Value="3" />
                                                            </Items>
                                                            <ItemStyle HorizontalAlign="Right" />

                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                                <RequiredField ErrorText="نوع انتقالی را انتخاب نمایید" IsRequired="True" />
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <ButtonStyle Width="13px">
                                                            </ButtonStyle>

                                                        </TSPControls:CustomAspxComboBox>


                                                    </td>
                                                    <td style="width: 15%" align="right" valign="top"></td>
                                                    <td style="width: 35%" align="right" valign="top"></td>

                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="دیگراستان" ID="ASPxLabel8" ClientInstanceName="lblPr">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td width="35%">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                            TextField="PrName" ID="ComboTPr" DataSourceID="OdbProvince" ValueType="System.String"
                                                            RightToLeft="True" ValueField="PrId" ClientInstanceName="ComboPr">
                                                            <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                                <RequiredField ErrorText="دیگراستان را انتخاب نمایید"></RequiredField>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <ButtonStyle Width="13px">
                                                            </ButtonStyle>
                                                        </TSPControls:CustomAspxComboBox>
                                                    </td>
                                                    <td style="vertical-align: top" dir="ltr" align="center" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ انتقالی" ID="ASPxLabel3"
                                                            ClientInstanceName="lblDate">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="vertical-align: top" dir="ltr" width="35%">
                                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnEvent="OnClick"
                                                            Width="245px" ShowPickerOnTop="True" ID="txtTDate" PickerDirection="ToRight"
                                                            IconUrl="~/Image/Calendar.gif" Style="direction: ltr; text-align: right" RightToLeft="False"></pdc:PersianDateTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top; text-align: right">
                                                        <dxe:ASPxLabel runat="server" Text="کد عضویت" ID="ASPxLabel15" ClientInstanceName="lblMeNo">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td style="vertical-align: top">
                                                        <TSPControls:CustomTextBox runat="server" ID="txtTransferMeNo" Width="100%" ClientInstanceName="MeNo">
                                                            <ValidationSettings ValidationGroup="Member" Display="Dynamic" ErrorTextPosition="Bottom">

                                                                <RequiredField ErrorText="کد عضویت را وارد نمایید"></RequiredField>
                                                                <RegularExpression ErrorText="کد عضویت فقط از ارقام تشکیل شده است و ارقام آخر شماره عضویت شما می باشد." ValidationExpression="\d*"></RegularExpression>
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                    </td>
                                                    <td style="vertical-align: top" align="center"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="right" width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="دلایل انتقال" ClientVisible="False" Width="105px"
                                                            ID="ASPxLabelPrDesc" ClientInstanceName="lblPrDesc">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td valign="top" align="right" colspan="3">
                                                        <TSPControls:CustomASPXMemo runat="server" Height="70px" ID="txtTransferBodyResone"
                                                            Width="550px" ClientInstanceName="TextPr">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="True" ErrorText="دلایل انتقال را وارد نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPXMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="4">
                                                        <TSPControls:CustomASPxCheckBox runat="server" Text="داری پروانه اشتغال به کار می باشم" ID="ChbTCheckFileNo"
                                                            ClientInstanceName="chbtc">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
	

    if(chbtc.GetChecked()==true)
	{
        DisableDocInfoControl(true);
	}
	else
	{
        DisableDocInfoControl(false);
	}
    FileNo.SetText('');
}"></ClientSideEvents>
                                                        </TSPControls:CustomASPxCheckBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" align="center">
                                                        <dxp:ASPxPanel ID="PanelEntegaliDoc" ClientInstanceName="PanelEntegaliDoc" runat="server"
                                                            Width="100%" ClientVisible="false">
                                                            <PanelCollection>
                                                                <dxp:PanelContent>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td style="width: 15%" valign="top" align="right">
                                                                                <dxe:ASPxLabel runat="server" Text="شماره پروانه" Width="135px" ID="ASPxLabel16"
                                                                                    ClientInstanceName="lblFileNo">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 35%" valign="top" align="right">
                                                                                <TSPControls:CustomTextBox runat="server" ID="txtTFileNo" Width="170px"
                                                                                    ClientInstanceName="FileNo" Style="direction: ltr">
                                                                                    <ValidationSettings ValidationGroup="Member" Display="Dynamic" EnableCustomValidation="True"
                                                                                        ErrorText="شماره پروانه را وارد نمایید" ErrorTextPosition="Bottom">

                                                                                        <RequiredField ErrorText="شماره پروانه را وارد نمایید"></RequiredField>
                                                                                        <%--   <RegularExpression ErrorText="این شماره صحیح نیست" ValidationExpression="^\d{2}-\d{3}-\d{3,5}"></RegularExpression>--%>
                                                                                        <RegularExpression ErrorText="یا ساختار کد صحیح نیست یا ابتدای کد نباید 17 باشد" ValidationExpression="^(?!17)\d\d\-\d\d\d\-\d+$" />
                                                                                        <ErrorFrameStyle Wrap="True" ImageSpacing="4px">
                                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                                        </ErrorFrameStyle>
                                                                                    </ValidationSettings>
                                                                                    <ClientSideEvents Validation="function(s, e) {
	if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}"></ClientSideEvents>
                                                                                </TSPControls:CustomTextBox>
                                                                            </td>
                                                                            <td style="width: 15%" valign="top" align="right">
                                                                                <dxe:ASPxLabel ID="lblDocPr" runat="server" ClientInstanceName="lblDocPr" Text="استان صدور پروانه"
                                                                                    Width="100%">
                                                                                </dxe:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 35%" valign="top" align="right">
                                                                                <TSPControls:CustomAspxComboBox ID="ComboDocPreProvince" runat="server" ClientInstanceName="ComboDocPreProvince"
                                                                                    DataSourceID="OdbProvince"
                                                                                    TextField="PrName" ValueField="PrId" ValueType="System.String"
                                                                                    Width="100%" RightToLeft="True">
                                                                                    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">

                                                                                        <RequiredField ErrorText="استان صدور پروانه را انتخاب نمایید" IsRequired="True" />
                                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                                                        </ErrorFrameStyle>
                                                                                    </ValidationSettings>
                                                                                    <ButtonStyle Width="13px">
                                                                                    </ButtonStyle>
                                                                                    <ClientSideEvents Validation="function(s, e) {
	if(s.GetEnabled()==true)
	{
		if(s.GetText()==null || s.GetText()=='')
			e.isValid=false;
	}
	else 
		e.isValid=true;
}" />
                                                                                </TSPControls:CustomAspxComboBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxp:ASPxPanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top">
                                                        <dxe:ASPxLabel runat="server" Text="تصویر نامه انتقالی" Width="102px" ID="ASPxLabel17"
                                                            ClientInstanceName="lblImg">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td colspan="3">
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                            ID="FlpTLetter" InputType="Images" ClientInstanceName="Img" OnFileUploadComplete="FlpTLetter_FileUploadComplete">
                                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                            if(e.isValid){
	imgEndUploadImgClientLetter.SetVisible(true);
	flpletter.Set('name',1);
	lblletter.SetVisible(false);
    lblletter2.SetVisible(false);
	imgletter.SetVisible(true);
	imgletter.SetImageUrl('../../Image/Temp/'+e.callbackData);
	}
	else{
	imgEndUploadImgClientLetter.SetVisible(false);
	flpletter.Set('name',0);
	lblletter.SetVisible(true);
    lblletter2.SetVisible(true);
	imgletter.SetVisible(false);
	imgletter.SetImageUrl('');
	}
}"></ClientSideEvents>
                                                                        </TSPControls:CustomAspxUploadControl>
                                                                        <dxe:ASPxLabel runat="server" Text="تصویر نامه را انتخاب نمایید" ClientVisible="False"
                                                                            ID="ASPxLabel18" ForeColor="Red" ClientInstanceName="lblletter">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                            ID="imgEndUploadImgLetter" ClientInstanceName="imgEndUploadImgClientLetter">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <dxe:ASPxImage runat="server" Height="75px" Width="75px" ID="imgletter" ClientInstanceName="imgletter">
                                                            <EmptyImage Height="75px" Width="75px" Url="~/Images/noimage.gif">
                                                            </EmptyImage>
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxp:ASPxPanel>
                        </fieldset>



                        <br />
                        <fieldset runat="server" id="RoundPanelCommissions">
                            <legend class="fieldset-legend" dir="rtl"><b>کمیسیون های همکاری</b>
                            </legend>

                            <asp:CheckBoxList ID="chbComId" runat="server" DataSourceID="ODBCom" DataTextField="ComName"
                                DataValueField="ComId" RepeatColumns="3" Width="100%">
                            </asp:CheckBoxList>
                        </fieldset>
                        <br />
                        <fieldset runat="server" id="RoundPanelAttachments">
                            <legend class="fieldset-legend" dir="rtl"><b>فایل های پیوست</b>
                            </legend>

                            <table runat="server" id="TblFile" style="width: 100%" dir="rtl">
                                <tr runat="server" id="Tr1">
                                    <td runat="server" id="Td1" valign="top" align="right">
                                        <asp:Label runat="server" Text="فایل" Width="24px" ID="lblimg"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td2" valign="top" align="right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                            ID="flp" InputType="Files" ClientInstanceName="flpc" OnFileUploadComplete="flp_FileUploadComplete">
                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                        if(e.isValid)
	imgEndUploadImgClient.SetVisible(true);
	else
	imgEndUploadImgClient.SetVisible(false);
}"></ClientSideEvents>
                                                        </TSPControls:CustomAspxUploadControl>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="imgEndUploadImg" ClientInstanceName="imgEndUploadImgClient">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr3">
                                    <td runat="server" id="Td4" valign="top" align="right">
                                        <asp:Label runat="server" Text="توضیحات" Width="82px" ID="Label10"></asp:Label>
                                    </td>
                                    <td runat="server" id="Td5" valign="top" align="right">
                                        <TSPControls:CustomASPXMemo runat="server" Height="30px" ID="txtDescImg"
                                            ClientInstanceName="txtDescImg">
                                            <ValidationSettings>

                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr2">
                                    <td runat="server" id="Td3" align="center" colspan="2">
                                        <div  class="Item-center" />
                                        <TSPControls:CustomAspxButton  CssClass="ButtonMenue" runat="server" Text="&nbsp;&nbsp;&nbsp;اضافه به لیست" Width="150px"
                                            ID="btnAddFlp" CausesValidation="false" EncodeHtml="false" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s,e){ AspxGridFlp.PerformCallback('Insert$'+txtDescImg.GetText()); }" />
                                         
                                        </TSPControls:CustomAspxButton>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                            <TSPControls:CustomAspxDevGridView runat="server" Width="100%"
                                RightToLeft="True" ClientInstanceName="AspxGridFlp" ID="AspxGridFlp" KeyFieldName="Id"
                                OnRowDeleting="AspxGridFlp_RowDeleting" OnCustomCallback="AspxGridFlp_CustomCallback">
                                <Columns>
                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption=" " Width="5%"
                                        Name="Command" ShowDeleteButton="true">
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="fileName" Caption="فایل"
                                        Name="fileName">
                                        <DataItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" Text="آدرس فایل" Width="30%" Target="_blank"
                                                NavigateUrl='<%# Bind("TempImgUrl") %>'></asp:HyperLink>
                                        </DataItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Description" Caption="توضیحات"
                                        Width="65%" Name="Description">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <Settings ShowFilterBar="Hidden" />
                                <ClientSideEvents EndCallback="function(s,e){
                                    if(s.cpState=='1')
                                       ClearAttachments();
                                    else if(s.cpMessage!='')
                                       ShowMessage(s.cpMessage);

                                    s.cpMessage='';
                                    s.cpState=-1;
                                    }" />
                            </TSPControls:CustomAspxDevGridView>
                        </fieldset>
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
                                        <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" Text="ذخیره" ToolTip="ذخیره"
                                            ID="btnSave2" EnableViewState="False" ValidationGroup="Member" EnableTheming="False"
                                            OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
                                                    if (ASPxClientEdit.ValidateGroup('Member') == false)
                                                     {
                                                       alert('لطفا موارد ذکر شده در بالای صفحه را تکمیل نمائید');
                                                       return;
                                                     }
                                                return e.processOnServer= confirm('پس از ذخیره اطلاعات و اطمینان از تکمیل تمام اطلاعات بایستی در صفحه مدیریت درخواست های عضویت  گزينه گردش كار را انتخاب كرده و درخواست خود را به كارمند واحد عضويت ارسال كنيد. در غیر این صورت روند بررسی پرونده شما شروع نمی شود');
                                                         btnSave(e); }"></ClientSideEvents>
                                      
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton  CssClass="ButtonMenue"  runat="server" Text="بازگشت" ToolTip="بازگشت"
                                            CausesValidation="False" ID="ASPxButton6" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnBack_Click">                                         
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ObjectDataSource ID="ODBSo" runat="server" CacheDuration="30" EnableCaching="True"
                SelectMethod="GetData" TypeName="TSP.DataManager.SoldierManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBMar" runat="server" CacheDuration="30" EnableCaching="True"
                SelectMethod="GetData" TypeName="TSP.DataManager.MaritalStatusManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbCity" runat="server" CacheDuration="30" SelectMethod="GetData"
                TypeName="TSP.DataManager.CityManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbAgent" runat="server" CacheDuration="30" SelectMethod="GetData"
                TypeName="TSP.DataManager.AccountingAgentManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="OdbProvince" runat="server" CacheDuration="30" SelectMethod="GetData"
                TypeName="TSP.DataManager.ProvinceManager" FilterExpression="NezamCode<>{0}">
                <FilterParameters>
                    <asp:Parameter Name="newparameter" />
                </FilterParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBCom" runat="server" TypeName="TSP.DataManager.CommissionManager"
                SelectMethod="GetData" EnableCaching="True" CacheDuration="30"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODBSex" runat="server" TypeName="TSP.DataManager.SexManager"
                SelectMethod="GetData" EnableCaching="True" CacheDuration="30"></asp:ObjectDataSource>            
            <dxhf:ASPxHiddenField ID="HiddenFieldMember" runat="server" ClientInstanceName="HiddenFieldMember">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpIdNo" runat="server" ClientInstanceName="flpmeidno">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpSSN" runat="server" ClientInstanceName="flpmessn">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpMember" runat="server" ClientInstanceName="flpme">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpSign" runat="server" ClientInstanceName="flpmesign">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpLetter" runat="server" ClientInstanceName="flpletter">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpSol" runat="server" ClientInstanceName="flpmesol">
            </dxhf:ASPxHiddenField>
            <dxhf:ASPxHiddenField ID="HDFlpResident" runat="server" ClientInstanceName="HDFlpResident">
            </dxhf:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
