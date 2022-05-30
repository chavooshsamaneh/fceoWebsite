<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WorkRequestInsertInfoUserControl.ascx.cs" Inherits="UserControl_WorkRequestInsertInfoUserControl" %>


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

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Src="~/UserControl/MeEngOfficeInfoUserControl.ascx" TagPrefix="TSP" TagName="MeEngOfficeInfoUserControl" %>
<%@ Register Src="~/UserControl/MeOfficeInfoUserControl.ascx" TagPrefix="TSP" TagName="MeOfficeInfoUserControlUserControl" %>

<script language="javascript">   

    var DesignMunicipalityChange = 0;
    var DesignBonyadMaskan = 0;
    var ObsMunicipalityChange = 0;
    var ObsBonyadMaskan = 0;
    var MunicipulityUrbEntebaghShahri = 0;
    var MunicipulityUrbTarh = 0;
    HiddenFieldPage.Set('MustHasAttach', false);
    function SetComperObslbl() {
        //اگر اختلاف عدد شهرداری مثبت باشد باید فایل نامه را آپلود کند        
        lblComperChangesObsShirazMunicipality.SetText(TextResult(HiddenFieldPage.Get('LastObsShirazMunicipality'), txtObsShirazMunicipality.GetValue()));
        lblComperChangesObsBonyadMaskan.SetText(TextResult(HiddenFieldPage.Get('LastObsBonyadMaskan'), txtBonyadMaskan.GetValue()));
        ObsMunicipalityChange = txtObsShirazMunicipality.GetValue() - HiddenFieldPage.Get('LastObsShirazMunicipality');
        ObsBonyadMaskan = txtBonyadMaskan.GetValue() - HiddenFieldPage.Get('LastObsBonyadMaskan');
        SetVisibleUploadControl();
    }
    function SetComperDesignlbl() {

        //اگر اختلاف عدد شهرداری مثبت باشد باید فایل نامه را آپلود کند
        lblComperChangesDesignShirazMunicipality.SetText(TextResult(HiddenFieldPage.Get('LastDesignShirazMunicipality'), txtDesignShirazMunicipality.GetValue()));
        lblComperChangesDesignBonyadMaskan.SetText(TextResult(HiddenFieldPage.Get('LastDesignBonyadMaskan'), txtDesignBonyadMaskan.GetValue()));
        DesignMunicipalityChange = txtDesignShirazMunicipality.GetValue() - HiddenFieldPage.Get('LastDesignShirazMunicipality');
        DesignBonyadMaskan = txtDesignBonyadMaskan.GetValue() - HiddenFieldPage.Get('LastDesignBonyadMaskan');
        SetVisibleUploadControl();
    }
    function SetComperUrbenismlbl() {

        lblComperChangesShirazMunicipulityUrbenismTarh.SetText(TextResult(HiddenFieldPage.Get('LastMunicipulityUrbTarh'), txtDesignShirazMunicipality.GetValue()));
        lblComperChangesShirazMunicipulityUrbenismEntebaghShahri.SetText(TextResult(HiddenFieldPage.Get('LastMunicipulityUrbEntebaghShahri'), txtShirazMunicipulityUrbenismEntebaghShahri.GetValue()));
        MunicipulityUrbEntebaghShahri = txtShirazMunicipulityUrbenismEntebaghShahri.GetValue() - HiddenFieldPage.Get('LastMunicipulityUrbEntebaghShahri');
        MunicipulityUrbTarh = txtShirazMunicipulityUrbenismTarh.GetValue() - HiddenFieldPage.Get('LastMunicipulityUrbTarh');
        SetVisibleUploadControl();
    }
    function TextResult(LastValue, CurrentValue) {
        var t = "مقدار قبلی:" + LastValue + "   تفاوت:" + (Math.abs(CurrentValue - LastValue))
        return t;
    }
    function SetVisibleUploadControl() {
        if (HiddenFieldPage.Get('StopmandatoryFileUploading') == 0 && HiddenFieldPage.Get('PgMd') != 'New' && (ObsMunicipalityChange < 0 || DesignMunicipalityChange < 0 || DesignBonyadMaskan < 0 || ObsBonyadMaskan < 0 || MunicipulityUrbEntebaghShahri < 0 || MunicipulityUrbTarh < 0)) {
            RoundPanelUploadControl.SetVisible(true);
            HiddenFieldPage.Set('MustHasAttach', true);
        }
        else {
            RoundPanelUploadControl.SetVisible(false);
            HiddenFieldPage.Set('MustHasAttach', false);
        }

    }
    function SetTextBoxDesignZero(SetBonyadZero) {
        txtDesignShirazMunicipality.SetText('0');
        if (SetBonyadZero == true)
            txtDesignBonyadMaskan.SetText('0');
        SetComperDesignlbl();
        SetVisibleUploadControl();
    }  
</script>
<pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
    CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
    FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
    WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
</pdc:PersianDateScriptManager>
<div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
    [<a class="closeLink" href="#"><span style="color: #000000"></span>بستن</a>]
</div>
<br />
<dxcp:ASPxPanel ID="RoundPanelPage" runat="server">
    <PanelCollection>
        <dxcp:PanelContent>
            <dxcp:ASPxPanel ID="RoundPanelMajor" runat="server">
                <PanelCollection>
                    <dxcp:PanelContent>
                        <fieldset>
                            <legend class="legendTitle">لیست رشته های پروانه اشتغال به کار</legend>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td valign="top" width="15%">رشته آماده به کاری</td>
                                        <td>
                                            <TSPControls:CustomAspxComboBox Width="100%" runat="server" ValueType="System.String"
                                                TextField="MajorParentName" ValueField="MajorParentId" AutoPostBack="true" OnSelectedIndexChanged="cmbMajor_SelectedIndexChanged"
                                                ID="cmbMajor"
                                                ClientInstanceName="cmbMajor">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                    </ErrorFrameStyle>
                                                    <RequiredField IsRequired="True" ErrorText="رشته را انتخاب نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomAspxComboBox>
                                        </td>
                                    </tr>
                            </table>
                        </fieldset>
                    </dxcp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxPanel>
            <dxcp:ASPxPanel ID="RoundPanelMeInfo" runat="server">
                <PanelCollection>
                    <dxcp:PanelContent>
                        <fieldset>
                            <legend class="legendTitle">مشخصات عضو</legend>

                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td style="width: 100%" colspan="4" align="center">
                                            <b>
                                                <a runat="server" class="HelpUL" text="" font-bold="False" id="lblWarning" style="text-decoration: none"></a></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="right">نام
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" ID="lblMeName" CssClass="lblShowInfo">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td valign="top" align="right">نام خانوادگی
                                        </td>
                                        <td valign="top" align="right">
                                            <dxe:ASPxLabel runat="server" ID="lblMeLastName" CssClass="lblShowInfo">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>تاریخ عضویت در سازمان</td>
                                        <td>
                                            <dxe:ASPxLabel IsMenuButton="true" runat="server" ID="txtMembershipDate" Text="---" CssClass="lblShowInfo">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td>شماره پروانه
                                          
                                        </td>
                                        <td>
                                            <dxe:ASPxLabel IsMenuButton="true" runat="server" ID="txtMFNo" Text="---" CssClass="lblShowInfo">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>رشته آماده به کاری
                                        </td>
                                        <td>
                                            <dxe:ASPxLabel IsMenuButton="true" runat="server" ID="txtMemberFileMajor" Text="---" CssClass="lblShowInfo">
                                            </dxe:ASPxLabel>

                                        </td>
                                        <td valign="top" width="15%" align="right">تاریخ اعتبار پروانه
                                        </td>
                                        <td valign="top" width="35%" align="right">
                                            <dxe:ASPxLabel IsMenuButton="true" runat="server" Text="---" CssClass="lblShowInfo"
                                                ID="txtExpireDateMember">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" width="15%" align="right">پایه نظارت
                                        </td>
                                        <td valign="top" width="35%" align="right">
                                            <dxe:ASPxLabel IsMenuButton="true" runat="server" ID="txtObsName" Text="---" CssClass="lblShowInfo">
                                            </dxe:ASPxLabel>
                                        </td>

                                        <td>تاریخ اخذ آخرین پایه نظارت</td>
                                        <td>
                                            <dxe:ASPxLabel IsMenuButton="true" runat="server" ID="txtObsDate" Text="---" CssClass="lblShowInfo">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>پایه نقشه برداری
                                        </td>
                                        <td>
                                            <dxe:ASPxLabel IsMenuButton="true" runat="server" ID="txtMappingName" Text="---" CssClass="lblShowInfo">
                                            </dxe:ASPxLabel>
                                        </td>
                                        </td>
                                        <td>تاریخ اخذ آخرین پایه نقشه برداری</td>
                                        <td>
                                            <dxe:ASPxLabel IsMenuButton="true" Text="---" runat="server" ID="txtMappingDate" CssClass="lblShowInfo">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" width="15%" align="right">پایه طراحی
                                        </td>
                                        <td valign="top" width="35%" align="right">
                                            <dxe:ASPxLabel IsMenuButton="true" Text="---" runat="server" ID="txtDesignName" CssClass="lblShowInfo">
                                            </dxe:ASPxLabel>
                                        </td>

                                        <td>تاریخ اخذ آخرین پایه طراحی</td>
                                        <td>
                                            <dxe:ASPxLabel IsMenuButton="true" Text="---" runat="server" ID="txtDesignDate" CssClass="lblShowInfo">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" width="15%" align="right">پایه شهرسازی
                                        </td>
                                        <td valign="top" width="35%" align="right">
                                            <dxe:ASPxLabel IsMenuButton="true" Text="---" runat="server" ID="txtUrbenismName" CssClass="lblShowInfo">
                                            </dxe:ASPxLabel>
                                        </td>

                                        <td>تاریخ اخذ آخرین پایه شهرسازی</td>
                                        <td>
                                            <dxe:ASPxLabel IsMenuButton="true" Text="---" runat="server" ID="txtUrbenismDate" CssClass="lblShowInfo">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TdAlignment">عضو در دفتر گاز:
                                        </td>
                                        <td class="TdAlignment" colspan="3">
                                            <dxe:ASPxLabel runat="server" Text="---" ID="lblHasGasCert" CssClass="lblShowInfo">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
                    </dxcp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxPanel>
            <dxcp:ASPxPanel ID="RoundPanelMemberEngOfficeInfo" ClientInstanceName="RoundPanelMemberEngOfficeInfo" ClientVisible="false"
                runat="server">
                <PanelCollection>
                    <dxcp:PanelContent>
                        <TSP:MeEngOfficeInfoUserControl runat="server" ID="UserControlMeEngOfficeInfoUserControl" />
                        <TSP:MeOfficeInfoUserControlUserControl runat="server" ID="UserControlMeOfficeInfoUserControl" />
                    </dxcp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxPanel>
            <dxcp:ASPxPanel ID="PanelMain" ClientInstanceName="PanelMain"
                runat="server">
                <PanelCollection>
                    <dxcp:PanelContent>
                        <dxcp:ASPxPanel ID="RoundPanelBasicCapacityInfo"
                            runat="server">
                            <PanelCollection>
                                <dxcp:PanelContent>
                                    <fieldset id="Fieldset1"
                                        runat="server">
                                        <legend class="legendTitle">زیربنا کل </legend>
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td colspan="2" style="align-content: center">
                                                        <dxcp:ASPxLabel CssClass="HelpUL" Font-Bold="true" runat="server" Width="100%" Font-Size="Large" ClientInstanceName="lblWarningCapacity" ID="lblWarningCapacity" Text="توجه!!">
                                                        </dxcp:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="30%">حداکثر ظرفیت اشتغال نظارت در برش زمانی(براساس پایه)</td>
                                                    <td>
                                                        <dxcp:ASPxLabel runat="server" EnableViewState="true" ID="lblMaxJobObsCapacity" ClientInstanceName="lblMaxJobObsCapacity" CssClass="lblShowInfo" Text="---"></dxcp:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>حداکثر ظرفیت اشتغال طراحی در مدت یک سال(براساس پایه در دفتر/شرکت)</td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblMaxDesignCapacity" CssClass="lblShowInfo" Text="---"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>حداکثر مجموع ظرفیت طراحی و نظارت در برش زمانی</td>
                                                    <td>
                                                        <dxcp:ASPxLabel runat="server" EnableViewState="true" ID="lblMaxTotalCapacity" Text="---" CssClass="lblShowInfo" ClientInstanceName="lblMaxTotalCapacity"></dxcp:ASPxLabel>
                                                    </td>


                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <dxcp:ASPxLabel ClientVisible="false" EnableViewState="true" runat="server" ID="lblMaxJobCount" ClientInstanceName="lblMaxJobCount" Text="---"></dxcp:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </fieldset>
                                </dxcp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxPanel>
                        <dxcp:ASPxPanel ID="RoundPanelDesignCapacity" ClientInstanceName="RoundPanelDesignCapacity"
                            runat="server">
                            <PanelCollection>
                                <dxcp:PanelContent>
                                    <fieldset>
                                        <legend class="legendTitle" id="TitleDesignCapacity" runat="server">تعیین زیربنا طراحی</legend>
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td width="30%">
                                                        <dxcp:ASPxLabel runat="server" ID="lblDesignShirazMunicipality" ClientInstanceName="lblDesignShirazMunicipality" Text="زیربنا طراحی شهرداری شیراز"></dxcp:ASPxLabel>
                                                        <dxcp:ASPxLabel runat="server" ID="lblDesignShirazMunicipalityLimitation" ClientInstanceName="lblDesignShirazMunicipalityLimitation" Style="color: red; text-decoration: none"></dxcp:ASPxLabel>

                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtDesignShirazMunicipality" ClientSideEvents-KeyUp="SetComperDesignlbl" ClientInstanceName="txtDesignShirazMunicipality" Width="100%" NullText="0">
                                                            <MaskSettings ShowHints="true" ErrorText="حداکثر زیربنا رعایت شود" AllowMouseWheel="false" />
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField ErrorText="زیربنا را وارد نمایید"></RequiredField>
                                                                <RegularExpression ValidationExpression="\d*" ErrorText="زیربنا را صحیح وارد نمایید" />
                                                            </ValidationSettings>

                                                        </TSPControls:CustomTextBox>
                                                        <dxcp:ASPxLabel runat="server" ID="lblComperChangesDesignShirazMunicipality" ClientInstanceName="lblComperChangesDesignShirazMunicipality" CssClass="ComperObserverRequest"></dxcp:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxcp:ASPxLabel runat="server" Text="زیربنا طراحی بنیاد مسکن"></dxcp:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtDesignBonyadMaskan" ClientInstanceName="txtDesignBonyadMaskan" ClientSideEvents-KeyUp="SetComperDesignlbl" Width="100%" NullText="0">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="true" ErrorText="زیربنا را وارد نمایید"></RequiredField>
                                                                <RegularExpression ValidationExpression="\d*" ErrorText="زیربنا را صحیح وارد نمایید" />
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                        <dxcp:ASPxLabel runat="server" ID="lblComperChangesDesignBonyadMaskan" ClientInstanceName="lblComperChangesDesignBonyadMaskan" CssClass="ComperObserverRequest"></dxcp:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <dxcp:ASPxLabel CssClass="HelpUL" Font-Bold="true" runat="server" Width="100%" ID="ASPxLabel12" Text="مابقی ظرفیت طراحی جزء ظرفیت استان محسوب شده و در سامانه خدمات مهندسی ثبت می گردد.">
                                                        </dxcp:ASPxLabel>
                                                    </td>

                                                </tr>

                                            </tbody>
                                        </table>
                                    </fieldset>
                                </dxcp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxPanel>
                        <dxcp:ASPxPanel ID="RounPanelUrbenismCapacity"
                            runat="server">
                            <PanelCollection>
                                <dxcp:PanelContent>
                                    <fieldset id="Fieldset3"
                                        runat="server">
                                        <legend class="legendTitle" id="Legend1" runat="server">تعیین ظرفیت شهرسازی</legend>
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td width="30%">کل ظرفیت تهیه طرح های شهرسازی در استان(مترمربع)</td>
                                                    <td>
                                                        <dxcp:ASPxLabel runat="server" EnableViewState="true" ID="lblMaxJobUrbenismCapacityUrbenismTarh" ClientInstanceName="lblMaxJobUrbenismCapacityUrbenismTarh" Text="---"></dxcp:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="30%">
                                                        <dxcp:ASPxLabel runat="server" ID="lblShirazMunicipulityUrbenismTarh" ClientInstanceName="lblShirazMunicipulityUrbenismTarh" Text="تهیه طرح های شهرسازی شهرداری شیراز"></dxcp:ASPxLabel>
                                                        <dxcp:ASPxLabel runat="server" ID="lblShirazMunicipulityUrbenismTarhLimitation" ClientInstanceName="lblShirazMunicipulityUrbenismTarhLimitation" Style="color: red; text-decoration: none"></dxcp:ASPxLabel>

                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtShirazMunicipulityUrbenismTarh" ClientSideEvents-KeyUp="SetComperUrbenismlbl" ClientInstanceName="txtShirazMunicipulityUrbenismTarh" Width="100%" NullText="0">
                                                            <MaskSettings ShowHints="true" ErrorText="حداکثر زیربنا رعایت شود" />
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField ErrorText="زیربنا را وارد نمایید"></RequiredField>
                                                                <RegularExpression ValidationExpression="\d*" ErrorText="زیربنا را صحیح وارد نمایید" />
                                                            </ValidationSettings>

                                                        </TSPControls:CustomTextBox>
                                                        <dxcp:ASPxLabel runat="server" ID="lblComperChangesShirazMunicipulityUrbenismTarh" ClientInstanceName="lblComperChangesShirazMunicipulityUrbenismTarh" CssClass="ComperObserverRequest"></dxcp:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="30%">کل ظرفیت تهیه طرح انطباق شهری در استان(مترمربع)</td>
                                                    <td>
                                                        <dxcp:ASPxLabel runat="server" EnableViewState="true" ID="lblMaxJobUrbenismCapacityEntebaghShahri" ClientInstanceName="lblMaxJobUrbenismCapacityEntebaghShahri" Text="---"></dxcp:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="30%">
                                                        <dxcp:ASPxLabel runat="server" ID="lblShirazMunicipulityUrbenismEntebaghShahri" ClientInstanceName="lblShirazMunicipulityUrbenismEntebaghShahri" Text="تهیه طرح انطباق شهری شهرداری شیراز"></dxcp:ASPxLabel>
                                                        <dxcp:ASPxLabel runat="server" ID="lblShirazMunicipulityUrbenismEntebaghShahriLimitation" ClientInstanceName="lblShirazMunicipulityUrbenismEntebaghShahriLimitation" Style="color: red; text-decoration: none"></dxcp:ASPxLabel>

                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtShirazMunicipulityUrbenismEntebaghShahri" ClientSideEvents-KeyUp="SetComperUrbenismlbl" ClientInstanceName="txtShirazMunicipulityUrbenismEntebaghShahri" Width="100%" NullText="0">
                                                            <MaskSettings ShowHints="true" ErrorText="حداکثر زیربنا رعایت شود" />
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField ErrorText="زیربنا را وارد نمایید"></RequiredField>
                                                                <RegularExpression ValidationExpression="\d*" ErrorText="زیربنا را صحیح وارد نمایید" />
                                                            </ValidationSettings>

                                                        </TSPControls:CustomTextBox>
                                                        <dxcp:ASPxLabel runat="server" ID="lblComperChangesShirazMunicipulityUrbenismEntebaghShahri" ClientInstanceName="lblComperChangesShirazMunicipulityUrbenismEntebaghShahri" CssClass="ComperObserverRequest"></dxcp:ASPxLabel>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <dxcp:ASPxLabel CssClass="HelpUL" Font-Bold="true" runat="server" Width="100%" ClientInstanceName="lblSadraDescription" ID="lblSadraDescription" Text="نکته: مابقی ظرفیت تخصیص یافته به شهرداري شیراز، جزء ظرفیت استان محسوب و در سامانه خدمات مهندسی ثبت میگردد">
                                                        </dxcp:ASPxLabel>
                                                    </td>

                                                </tr>
                                            </tbody>
                                        </table>
                                    </fieldset>
                                </dxcp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxPanel>
                        <dxcp:ASPxPanel ID="RoundPanelCity" ClientInstanceName="RoundPanelCity"
                            runat="server">
                            <PanelCollection>
                                <dxcp:PanelContent>
                                    <fieldset id="RoundPanel3"
                                        runat="server">
                                        <legend class="legendTitle">منطقه نظارت</legend>

                                        <table width="100%">
                                            <tbody>

                                                <tr>
                                                    <td>
                                                        <dxe:ASPxLabel runat="server" Text="نمایندگی" Width="100%" ID="ASPxLabel2">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxLabel IsMenuButton="true" runat="server" CssClass="lblShowInfo"
                                                            ID="txtAgent">
                                                        </dxe:ASPxLabel>

                                                    </td>


                                                </tr>
                                                <tr>
                                                    <td width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="شهر انتخابی اول" ID="ASPxLabel8">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td width="35%">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                            TextField="CitName" ID="comboCity1" ClientInstanceName="comboCity1" DataSourceID="ObjectDataSourceCity1"
                                                            ValueType="System.Int32" ValueField="CitId"
                                                            EnableIncrementalFiltering="True" RightToLeft="True" AutoPostBack="true" OnSelectedIndexChanged="comboCity1_SelectedIndexChanged">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ButtonStyle Width="13px">
                                                            </ButtonStyle>
                                                        </TSPControls:CustomAspxComboBox>
                                                        <asp:ObjectDataSource runat="server" SelectMethod="SelectByAgent" ID="ObjectDataSourceCity1"
                                                            TypeName="TSP.DataManager.CityManager" OldValuesParameterFormatString="original_{0}">
                                                            <SelectParameters>
                                                                <asp:Parameter Type="Int32" Name="AgentId" DefaultValue="-1"></asp:Parameter>
                                                                <asp:Parameter Type="Int32" Name="ShowInTsWorkRequest" DefaultValue="1"></asp:Parameter>
                                                                <asp:Parameter Type="Int32" Name="CitId" DefaultValue="-1"></asp:Parameter>
                                                                <asp:Parameter Type="Int32" Name="CitIdExeption" DefaultValue="-1"></asp:Parameter>
                                                                <asp:Parameter Type="String" Name="CitIdList" DefaultValue=""></asp:Parameter>
                                                                <asp:Parameter Type="String" Name="CitIdExeptionList" DefaultValue=""></asp:Parameter>
                                                            </SelectParameters>
                                                        </asp:ObjectDataSource>
                                                    </td>
                                                    <td width="15%">
                                                        <dxcp:ASPxLabel runat="server" Text="شهر انتخابی دوم" ClientInstanceName="lblCity2" ID="lblCity2">
                                                        </dxcp:ASPxLabel>
                                                    </td>
                                                    <td width="35%">
                                                        <TSPControls:CustomAspxComboBox runat="server" Width="100%"
                                                            TextField="CitName" ID="comboCity2" ClientInstanceName="comboCity2" DataSourceID="ObjectDataSourceCity2"
                                                            ValueType="System.Int32" ValueField="CitId"
                                                            EnableIncrementalFiltering="True" RightToLeft="True" AutoPostBack="true" OnSelectedIndexChanged="comboCity2_SelectedIndexChanged">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <%--        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="true" ErrorText="شهر دوم را انتخاب نمایید"></RequiredField>

                                                            </ValidationSettings>--%>
                                                            <ButtonStyle Width="13px">
                                                            </ButtonStyle>
                                                            <ClientSideEvents SelectedIndexChanged="function(s,e){
                                                                if(comboCity2.GetValue()==null && comboCity1.GetValue()==null)
                                                                {
                                                                    RoundPanelRules.SetVisible(false);
                                                                    CheckBoxWantObserverSelect.SetChecked(false);
                                                                    checkboxRulls.SetChecked(false);
                                                                }
                                                                 if(comboCity2.GetValue()==comboCity1.GetValue())
                                                                {
                                                                alert('شهر انتخابی اول و دوم نمی توانند یکسان باشند');
                                                                comboCity2.SetSelectedIndex(0);
                                                                return;
                                                                }                                                                                                           
                                                            }" />
                                                        </TSPControls:CustomAspxComboBox>
                                                        <asp:ObjectDataSource runat="server" SelectMethod="SelectByAgent" ID="ObjectDataSourceCity2"
                                                            TypeName="TSP.DataManager.CityManager" OldValuesParameterFormatString="original_{0}">
                                                            <SelectParameters>
                                                                <asp:Parameter Type="Int32" Name="AgentId" DefaultValue="-1"></asp:Parameter>
                                                                <asp:Parameter Type="Int32" Name="ShowInTsWorkRequest" DefaultValue="-1"></asp:Parameter>
                                                                <asp:Parameter Type="Int32" Name="CitId" DefaultValue="-1"></asp:Parameter>
                                                                <asp:Parameter Type="Int32" Name="CitIdExeption" DefaultValue="-1"></asp:Parameter>
                                                                <asp:Parameter Type="String" Name="CitIdList" DefaultValue=""></asp:Parameter>
                                                                <asp:Parameter Type="String" Name="CitIdExeptionList" DefaultValue=""></asp:Parameter>

                                                            </SelectParameters>
                                                        </asp:ObjectDataSource>

                                                    </td>

                                                </tr>

                                            </tbody>
                                        </table>
                                    </fieldset>
                                </dxcp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxPanel>
                        <dxcp:ASPxPanel ID="RoundPanelObserveCapacity" ClientInstanceName="RoundPanelObserveCapacity"
                            runat="server">
                            <PanelCollection>
                                <dxcp:PanelContent>

                                    <fieldset>
                                        <legend class="legendTitle">تعیین زیربنا نظارت</legend>

                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td width="30%">
                                                        <dxcp:ASPxLabel ClientInstanceName="lblObsShirazMunicipality" runat="server" ID="lblObsShirazMunicipality" Text="زیربنا نظارت شهرداری شیراز">
                                                        </dxcp:ASPxLabel>
                                                        <dxcp:ASPxLabel runat="server" ID="lblObsShirazMunicipalityLimitation" ClientInstanceName="lblObsShirazMunicipalityLimitation" Style="color: red; text-decoration: none"></dxcp:ASPxLabel>

                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ID="txtObsShirazMunicipality" ClientSideEvents-KeyUp="SetComperObslbl" ClientInstanceName="txtObsShirazMunicipality" Width="100%" NullText="0">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField ErrorText="زیربنا را وارد نمایید"></RequiredField>

                                                                <RegularExpression ValidationExpression="\d*" ErrorText="زیربنا را صحیح وارد نمایید" />
                                                            </ValidationSettings>
                                                            <MaskSettings ShowHints="true" ErrorText="حداکثر زیربنا رعایت شود" AllowMouseWheel="false" />
                                                        </TSPControls:CustomTextBox>
                                                        <dxcp:ASPxLabel runat="server" ID="lblComperChangesObsShirazMunicipality" ClientInstanceName="lblComperChangesObsShirazMunicipality" CssClass="ComperObserverRequest"></dxcp:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dxcp:ASPxLabel ClientInstanceName="lblBonyadMaskan" runat="server" ID="lblBonyadMaskan" Text="زیربنا نظارت بنیاد مسکن"></dxcp:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomTextBox IsMenuButton="true" runat="server" ClientSideEvents-KeyUp="SetComperObslbl" ClientInstanceName="txtBonyadMaskan" ID="txtBonyadMaskan" Width="100%" NullText="0">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="true" ErrorText="زیربنا را وارد نمایید"></RequiredField>
                                                                <RegularExpression ValidationExpression="\d*" ErrorText="زیربنا را صحیح وارد نمایید" />
                                                            </ValidationSettings>
                                                        </TSPControls:CustomTextBox>
                                                        <dxcp:ASPxLabel runat="server" ID="lblComperChangesObsBonyadMaskan" ClientInstanceName="lblComperChangesObsBonyadMaskan" CssClass="ComperObserverRequest"></dxcp:ASPxLabel>
                                                    </td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </fieldset>
                                </dxcp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxPanel>
                        <dxcp:ASPxPanel ID="RoundPanelRules" ClientInstanceName="RoundPanelRules"
                            runat="server">
                            <PanelCollection>
                                <dxcp:PanelContent>
                                    <fieldset>
                                        <legend class="legendTitle">قوانین ارجاع کار نظارت</legend>

                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td width="40%"></td>
                                                    <td width="20%">
                                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مشاهده نظام نامه اولویت بندی ارجاع کار نظارت ساختمان(مربوط به ارجاع نظارت الکترونیکی در شهر صدرا و دفاتر نمایندگی شهرستان ها)" ToolTip="دریافت نظام نامه اولویت بندی ارجاع کار نظارت ساختمان"
                                                            CausesValidation="False" ID="btnMadrakForUpGrade" AutoPostBack="False" UseSubmitBehavior="False"
                                                            EnableViewState="False" EnableTheming="False">
                                                            <ClientSideEvents Click="function(s,e){window.open('TsObserverWorkRulls.aspx');}" />

                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="40%"></td>

                                                </tr>

                                                <tr>

                                                    <td colspan="3">
                                                        <TSPControls:CustomASPxCheckBox ID="checkboxRulls" ClientInstanceName="checkboxRulls" runat="server" Text="کلیه قوانین درج شده در ''نظام نامه اولویت بندی ارجاع کار نظارت ساختمان'' (لینک موجود در همین صفحه) همچنین ضوابط و مقررات مربوط به ارجاع نظارت براساس مبحث دوم مقررات ملی ساختمان (نظامات اداری) را مطالعه کرده و می‌پذیرم.">
                                                            <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="true" ErrorText="گزینه پذیرش قوانین را انتخاب نمایید"></RequiredField>
                                                            </ValidationSettings>
                                                        </TSPControls:CustomASPxCheckBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <dxcp:ASPxLabel CssClass="HelpUL" Font-Size="Large" Font-Bold="true" runat="server" Width="100%" ID="ASPxLabel1" Text="توجه!!در صورت تمایل به ارجاع کار نظارت بایستی تیک زیر را انتخاب نمایید">
                                                        </dxcp:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>

                                                    <td colspan="3">
                                                        <TSPControls:CustomASPxCheckBox Font-Bold="true" ForeColor="#006600" ID="CheckBoxWantObserverSelect" ClientInstanceName="CheckBoxWantObserverSelect" runat="server" Text="با آگاهی کامل تمایل به ارجاع کار نظارت  دارم">
                                                        </TSPControls:CustomASPxCheckBox>
                                                        <br />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </fieldset>
                                </dxcp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxPanel>
                        <dxcp:ASPxPanel ID="RoundPanelPrjTypes" ClientInstanceName="RoundPanelPrjTypes"
                            runat="server">
                            <PanelCollection>
                                <dxcp:PanelContent>
                                    <fieldset>
                                        <legend class="legendTitle">نوع پروژه های مورد تقاضا</legend>
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomASPxCheckBox ID="CheckBoxIsFullTimeWorker" ClientInstanceName="CheckBoxIsFullTimeWorker" runat="server" Text="اینجانب شاغل تمام وقت نمی باشم">
                                                        </TSPControls:CustomASPxCheckBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomASPxCheckBoxList Caption="گروه ساختمانی مورد درخواست جهت ارجاع کار" runat="server" TextField="GroupName" ValueField="GroupId"
                                                            DataSourceID="ObjdsStructureGroups" ID="CheckListStructureGroups" ClientInstanceName="CheckListStructureGroups">
                                                            <%--    <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                <RequiredField IsRequired="true" ErrorText="گروه ساختمانی مورد درخواست جهت ارجاع کار را انتخاب نمایید"></RequiredField>

                                                            </ValidationSettings>--%>
                                                        </TSPControls:CustomASPxCheckBoxList>

                                                        <asp:ObjectDataSource ID="ObjdsStructureGroups" runat="server" TypeName="TSP.DataManager.TechnicalServices.StructureGroupsManager"
                                                            SelectMethod="FindByMeGradeId">
                                                            <SelectParameters>
                                                                <asp:Parameter Type="Int32" DefaultValue="-1" Name="MeGradeId"></asp:Parameter>
                                                            </SelectParameters>
                                                        </asp:ObjectDataSource>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomASPxCheckBox ID="CheckBoxWantCharity" runat="server" Text="تمایل به انجام پروژه‌های خیریه دارم. (بر اساس بخشنامه شماره 400/57865 مورخ 20/10/1394 ابلاغی وزارت راه و شهرسازی)"></TSPControls:CustomASPxCheckBox>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomASPxCheckBox ID="CheckBoxWantEghdamMeliMaskan" runat="server" Text="تمايل به انجام پروژه هاي اقدام ملي مسكن دارم"></TSPControls:CustomASPxCheckBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomASPxCheckBox ID="CheckBoxWantShahrakSanati" runat="server" Text="از اعضای لیست اعلامی از طرف شرکت شهرک صنعتی می باشد"></TSPControls:CustomASPxCheckBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </fieldset>
                                </dxcp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxPanel>
                        <dxcp:ASPxPanel ID="RoundPanelUploadControlObsCommitmentForm" ClientInstanceName="RoundPanelUploadControlObsCommitmentForm" ClientVisible="false"
                            runat="server">
                            <PanelCollection>
                                <dxcp:PanelContent>
                                    <fieldset id="Fieldset2"
                                        runat="server">
                                        <legend class="legendTitle" id="Legend3" runat="server">تعهد نظارت</legend>

                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td id="Td1" runat="server" valign="top" align="right" style="width: 30%">
                                                        <asp:Label runat="server" Text="فرم تعهد نظارت" ID="Label1"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                            ID="flpObsCommitmentForm" InputType="Files" ClientInstanceName="flpObsCommitmentForm" OnFileUploadComplete="flpObsCommitmentForm_FileUploadComplete">
                                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	
	imgEndUploadObsCommitmentForm.SetVisible(true);
	lblvalidationUploadAttachObsCommitmentForm.SetVisible(false);
	hplPreviewObsCommitmentForm.SetVisible(true);
	hplPreviewObsCommitmentForm.SetNavigateUrl('../../../Image/Members/RequestWork/ObsCommitmentForm/'+e.callbackData);
    HiddenFieldPage.Set('FullFilePathObsCommitmentForm','~/image/Members/RequestWork/ObsCommitmentForm/'+e.callbackData);
    HiddenFieldPage.Set('flpObsCommitmentFormValidation',true);
	}
	else{
	imgEndUploadObsCommitmentForm.SetVisible(false);
	lblvalidationUploadAttachObsCommitmentForm.SetVisible(true);
	hplPreviewObsCommitmentForm.SetVisible(false);
	hplPreviewObsCommitmentForm.SetNavigateUrl('');
    HiddenFieldPage.Set('flpObsCommitmentFormValidation',false);
	}
}"></ClientSideEvents>
                                                                        </TSPControls:CustomAspxUploadControl>
                                                                        <dxe:ASPxLabel ID="lblvalidationUploadAttachObsCommitmentForm" runat="server" ClientInstanceName="lblvalidationUploadAttachObsCommitmentForm" ClientVisible="False"
                                                                            ForeColor="Red" Text="پیوست را انتخاب نمایید">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="پیوست انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                            ID="imgEndUploadObsCommitmentForm" ClientInstanceName="imgEndUploadObsCommitmentForm">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <dxe:ASPxHyperLink runat="server" Target="_blank" Text="فایل بارگذاری شده" ID="hplPreviewObsCommitmentForm" ClientInstanceName="hplPreviewObsCommitmentForm">
                                                        </dxe:ASPxHyperLink>
                                                    </td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </fieldset>
                                </dxcp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxPanel>
                        <dxcp:ASPxPanel ID="RoundPanelUploadControl" ClientInstanceName="RoundPanelUploadControl" ClientVisible="false"
                            runat="server">
                            <PanelCollection>
                                <dxcp:PanelContent>
                                    <fieldset id="Fieldset4"
                                        runat="server">
                                        <legend class="legendTitle" id="Legend2" runat="server">پیوست</legend>
                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td id="Td41" runat="server" valign="top" align="right" style="width: 30%">
                                                        <asp:Label runat="server" Text="فایل پیوست تاییدیه نهادهای مربوطه" ID="Label42"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <TSPControls:CustomAspxUploadControl runat="server" UploadWhenFileChoosed="true"
                                                                            ID="flpAttach" InputType="Files" ClientInstanceName="flpAttach" OnFileUploadComplete="flpAttach_FileUploadComplete">
                                                                            <ClientSideEvents FileUploadComplete="function(s, e) {
                                                                    if(e.isValid){
	
	imgEndUploadClient.SetVisible(true);
	lblvalidationUploadAttach.SetVisible(false);
	hplPreview.SetVisible(true);
	hplPreview.SetNavigateUrl('../../../Image/Members/RequestWork/'+e.callbackData);
    HiddenFieldPage.Set('FullFilePath','~/image/Members/RequestWork/'+e.callbackData);
    HiddenFieldPage.Set('flpAttachValidation',true);
	}
	else{
	imgEndUploadClient.SetVisible(false);
	lblvalidationUploadAttach.SetVisible(true);
	hplPreview.SetVisible(false);
	hplPreview.SetNavigateUrl('');
    HiddenFieldPage.Set('flpAttachValidation',false);
	}
}"></ClientSideEvents>
                                                                        </TSPControls:CustomAspxUploadControl>
                                                                        <dxe:ASPxLabel ID="ASPxLabel20" runat="server" ClientInstanceName="lblvalidationUploadAttach" ClientVisible="False"
                                                                            ForeColor="Red" Text="پیوست را انتخاب نمایید">
                                                                        </dxe:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="پیوست انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                                            ID="ASPxImage4" ClientInstanceName="imgEndUploadClient">
                                                                        </dxe:ASPxImage>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <dxe:ASPxHyperLink runat="server" Target="_blank" Text="فایل بارگذاری شده" ID="hplPreview" ClientInstanceName="hplPreview">
                                                        </dxe:ASPxHyperLink>
                                                    </td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </fieldset>
                                </dxcp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxPanel>
                        <dxcp:ASPxPanel ID="RoundPanelOffRequest" Visible="false"
                            runat="server">
                            <PanelCollection>
                                <dxcp:PanelContent>
                                    <fieldset>
                                        <legend class="legendTitle">تنظیم تاریخ مرخصی</legend>

                                        <table width="100%">
                                            <tbody>
                                                <tr>
                                                    <td width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ شروع مرخصی" ID="ASPxLabel15">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <pdc:PersianDateTextBox runat="server" DefaultDate="" ShowPickerOnTop="True"
                                                            ID="txtStartOffDate" PickerDirection="ToRight" ShowPickerOnEvent="OnClick"
                                                            IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="true" ClientValidationFunction="PersianDateValidator"
                                                            ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtStartOffDate" ID="PersianDateValidator1">تاریخ نامعتبر</pdc:PersianDateValidator>
                                                    </td>
                                                    <td width="15%">
                                                        <dxe:ASPxLabel runat="server" Text="تاریخ پایان مرخصی" ID="ASPxLabel17">
                                                        </dxe:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <pdc:PersianDateTextBox runat="server" ShowPickerOnEvent="OnClick"
                                                            ShowPickerOnTop="True" ID="txtEndOffDate" PickerDirection="ToRight"
                                                            IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                        <pdc:PersianDateValidator runat="server" ValidateEmptyText="true" ClientValidationFunction="PersianDateValidator"
                                                            ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtEndOffDate" ID="PersianDateValidator2">تاریخ نامعتبر</pdc:PersianDateValidator>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </fieldset>
                                </dxcp:PanelContent>
                            </PanelCollection>
                        </dxcp:ASPxPanel>
                    </dxcp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxPanel>
            <dxcp:ASPxPanel ID="RoundPanelCommitMuniciToll" ClientVisible="false" ClientEnabled="false"
                runat="server">
                <PanelCollection>
                    <dxcp:PanelContent>
                        <fieldset>
                            <legend class="legendTitle">تعهد پرداخت عوارض شهرداری شیراز</legend>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td>

                                            <TSPControls:CustomASPxCheckBox ID="CheckBoxCommitMuniciToll" ClientInstanceName="CheckBoxCommitMuniciToll" runat="server" Text="اینجانب متعهد می گردم با اعلام سازمان نسبت به پرداخت عوارض شهرداری اقدام نمایم.">
                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                    <RequiredField IsRequired="true" ErrorText="برای ثبت آماده بکاری تعهد به پرداخت عوارض شهرداری در صورت اعلام سازمان باید مورد پذیرش قرار بگیرد"></RequiredField>
                                                </ValidationSettings>
                                            </TSPControls:CustomASPxCheckBox>

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
                    </dxcp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxPanel>


            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldPage" ClientInstanceName="HiddenFieldPage">
            </dxhf:ASPxHiddenField>
        </dxcp:PanelContent>
    </PanelCollection>
</dxcp:ASPxPanel>
