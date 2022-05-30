<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WorkRequestUserControl.ascx.cs" Inherits="UserControl_WorkRequestUserControl" %>


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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

            <fieldset id="RoundPanelMeInfo"
                runat="server">
                <legend class="HelpUL">مشخصات عضو</legend>
                <div class="row">
                      <h5>   <b>
                <a runat="server" class="HelpUL" text="" font-bold="False" id="lblWarning" visible="false"   ></a></b></h5>   
                </div>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="نام" ID="ASPxLabelFirstName" ClientInstanceName="lblFirstName">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel Text="---" runat="server" ID="txtFirstName" Width="100%"
                                    ClientInstanceName="txtFirstName">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="نام خانوادگی" ID="ASPxLabelLastName" ClientInstanceName="lblLastName">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel Text="---" runat="server" ID="txtLastName" Width="100%"
                                    ClientInstanceName="txtLastName">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="نام پدر" ID="ASPxLabelFatherName" ClientInstanceName="lblFatherName">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel Text="---" runat="server" ID="txtFatherName" Width="100%"
                                    ClientInstanceName="txtFatherName">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="کد ملی" ID="ASPxLabelSSN" ClientInstanceName="lblSSN">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel Text="---" runat="server" ID="txtSSN" Width="100%" MaxLength="10"
                                    ClientInstanceName="txtSSN">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="تاریخ اعتبار پروانه" ID="ASPxLabel15">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel Text="---" runat="server" ID="txtFileDate" Width="100%"
                                    ClientInstanceName="txtFileDate">
                                </dxe:ASPxLabel>
                            </td>
                              <td valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="شماره پروانه" ID="ASPxLabel" >
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel Text="---" runat="server" ID="txtFileNo" Width="100%" 
                                    ClientInstanceName="txtFileNo">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>

                        <tr>
                            <td valign="top" width="15%" align="right">
                                <dxe:ASPxLabel runat="server" Text="پایه نظارت" ID="lblObsId">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" width="35%" align="right">
                                <dxe:ASPxLabel runat="server" ID="txtObsId" ClientInstanceName="txtObsId" Text="---" Width="100%">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="پایه طراحی" ID="lblDesignId" ClientInstanceName="lblDesignId">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel Text="---" runat="server" ID="txtDesign" Width="100%"
                                    ClientInstanceName="txtDesign">
                                </dxe:ASPxLabel>
                            </td>
                            <%-- <td>تاریخ اخذ صلاحیت نظارت</td>
                            <td>
                                <dxe:ASPxLabel runat="server" ID="txtObsDate" Text="---" Width="100%">
                                </dxe:ASPxLabel>
                            </td>--%>
                        </tr>
                        <tr>

                            <td valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="پایه نقشه برداری" ID="lblMappingId" ClientInstanceName="lblMappingId">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel Text="---" runat="server" ID="txtMapping" Width="100%"
                                    ClientInstanceName="txtMappingId">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel runat="server" Text="پایه شهرسازی" ID="lblUrbenism" ClientInstanceName="lblUrbenism">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="top" align="right">
                                <dxe:ASPxLabel Text="---" runat="server" ID="txtUrbenism" Width="100%"
                                    ClientInstanceName="txtUrbenism">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxLabel runat="server" Text="رشته آماده به کاری" ID="lblMemberFileMajor" ClientInstanceName="lblMemberFileMajor">
                                </dxe:ASPxLabel>
                            </td>
                            <td>
                                <dxe:ASPxLabel runat="server" ID="txtMemberFileMajor" ClientInstanceName="txtMemberFileMajor" Width="100%" Text="---">
                                </dxe:ASPxLabel>
                            </td>
                            <td class="TdAlignment">عضو در دفتر گاز
                            </td>
                            <td class="TdAlignment">
                                <dxe:ASPxLabel runat="server" Text="---" ID="lblHasGasCert" ClientInstanceName="lblHasGasCert" Width="100%">
                                </dxe:ASPxLabel>
                            </td>
                         
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxLabel runat="server" Text="نمایندگی" Width="100%" ID="ASPxLabel2">
                                </dxe:ASPxLabel>
                            </td>
                            <td>
                                <dxe:ASPxLabel runat="server" Text="---" Width="100%" 
                                    ID="txtAgent" ClientInstanceName="txtAgent">
                                </dxe:ASPxLabel>

                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </tbody>
                </table>
            </fieldset>

<dxcp:ASPxPanel ID="PanelMainWorkReqUserControl" ClientInstanceName="PanelMainWorkReqUserControl"
    runat="server">
    <PanelCollection>
        <dxcp:PanelContent>

            <dxcp:ASPxPanel ID="RoundPanelCity" ClientInstanceName="RoundPanelCity"
                runat="server">
                <PanelCollection>
                    <dxcp:PanelContent>
                        <fieldset id="RoundPanel3"
                            runat="server">
                            <legend class="HelpUL">منطقه نظارت</legend>

                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td width="15%">
                                            <dxe:ASPxLabel runat="server" Text="شهر انتخابی اول" ID="ASPxLabel8">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td width="35%">
                                            <dxe:ASPxLabel runat="server" Text="---" Width="100%"
                                                ID="txtCity1">
                                            </dxe:ASPxLabel>

                                        </td>
                                        <td width="15%">
                                            <dxcp:ASPxLabel runat="server" Text="شهر انتخابی دوم" ClientInstanceName="lblCity2" ID="lblCity2">
                                            </dxcp:ASPxLabel>
                                        </td>
                                        <td width="35%">

                                            <dxe:ASPxLabel runat="server" Text="---" Width="100%"
                                                ID="txtCity2">
                                            </dxe:ASPxLabel>
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
                        <fieldset id="RoundPanel2"
                            runat="server">
                            <legend class="HelpUL">نوع پروژه های مورد تقاضا</legend>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomASPxCheckBox ReadOnly="true" ID="CheckBoxIsFullTimeWorker" ClientInstanceName="CheckBoxIsFullTimeWorker" runat="server" Text="اینجانب شاغل تمام وقت نمی باشم">
                                            </TSPControls:CustomASPxCheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomASPxCheckBoxList  ReadOnly="true" Caption="گروه ساختمانی مورد درخواست جهت ارجاع کار" runat="server" TextField="GroupName" ValueField="GroupId"
                                                DataSourceID="ObjdsStructureGroups" ID="CheckListStructureGroups" ClientInstanceName="CheckListStructureGroups" RepeatDirection="Horizontal" RepeatLayout="Flow">
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
                                            <TSPControls:CustomASPxCheckBox  ReadOnly="true" ID="CheckBoxWantCharity" runat="server" Text="تمایل به انجام پروژه‌های خیریه دارم. (بر اساس بخشنامه شماره 400/57865 مورخ 20/10/1394 ابلاغی وزارت راه و شهرسازی)"></TSPControls:CustomASPxCheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomASPxCheckBox ReadOnly="true" ID="CheckBoxWantShahrakSanati" runat="server" Text="تمایل به انجام پروژه‌های شهرک صنعتی دارم. (بر اساس ضوابط و مقررات شرکت شهرک‌های صنعتی و لیست اعلامی از طرف آن شرکت)"></TSPControls:CustomASPxCheckBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
                    </dxcp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxPanel>
            <dxcp:ASPxPanel ID="RoundPanelBasicCapacityInfo" ClientInstanceName="RoundPanelBasicCapacityInfo"
                runat="server">
                <PanelCollection>
                    <dxcp:PanelContent>
                        <fieldset id="Fieldset1"
                            runat="server">
                            <legend class="HelpUL">زیربنا کل </legend>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td width="30%">حداکثر ظرفیت اشتغال نظارت در برش زمانی(براساس پایه)</td>
                                        <td>
                                            <dxcp:ASPxLabel runat="server" EnableViewState="true" ID="lblMaxJobObsCapacity" ClientInstanceName="lblMaxJobObsCapacity" Text="---"></dxcp:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>حداکثر ظرفیت اشتغال طراحی در مدت یک سال(براساس پایه در دفتر/شرکت)</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblMaxDesignCapacity" Text="---"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>حداکثر مجموع ظرفیت طراحی و نظارت در برش زمانی</td>
                                        <td>
                                            <dxcp:ASPxLabel runat="server" EnableViewState="true" ID="lblMaxTotalCapacity" Text="---" ClientInstanceName="lblMaxTotalCapacity"></dxcp:ASPxLabel>
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
            <dxcp:ASPxPanel ID="RoundPanelObserveCapacity" ClientInstanceName="RoundPanelObserveCapacity"
                runat="server">
                <PanelCollection>
                    <dxcp:PanelContent>

                        <fieldset id="RoundPanel1"
                            runat="server">
                            <legend class="HelpUL">حداکثر زیربنا نظارت</legend>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td width="30%">
                                            <dxcp:ASPxLabel ClientInstanceName="lblObsShirazMunicipality" runat="server" ID="lblObsShirazMunicipality" Text="زیربنا نظارت شهرداری شیراز">
                                            </dxcp:ASPxLabel>
                                            <dxcp:ASPxLabel runat="server" ID="lblObsShirazMunicipalityLimitation" ClientInstanceName="lblObsShirazMunicipalityLimitation" Style="color: red; text-decoration: none"></dxcp:ASPxLabel>

                                        </td>
                                        <td>
                                            <dxcp:ASPxLabel runat="server" ID="txtObsShirazMunicipality" ClientInstanceName="txtObsShirazMunicipality" Text="---" Width="100%" NullText="0">
                                            </dxcp:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxcp:ASPxLabel ClientInstanceName="lblKhanZenyanObserveMeter" runat="server" ID="lblKhanZenyanObserveMeter" Text="زیربنا نظارت خان زنیان"></dxcp:ASPxLabel>

                                        </td>
                                        <td>
                                            <dxcp:ASPxLabel runat="server" ID="txtKhanZenyanObserveMeter" ClientInstanceName="txtKhanZenyanObserveMeter" Text="---" Width="100%" NullText="0">
                                            </dxcp:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxcp:ASPxLabel ClientInstanceName="lblLapooyObserveMeter" runat="server" ID="lblLapooyObserveMeter" Text="زیربنا نظارت لپویی"></dxcp:ASPxLabel>

                                        </td>
                                        <td>
                                            <dxcp:ASPxLabel runat="server" ID="txtLapooyObserveMeter" ClientInstanceName="txtLapooyObserveMeter" Text="---" Width="100%" NullText="0">
                                            </dxcp:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxcp:ASPxLabel ClientInstanceName="lblZarghanObserveMeter" runat="server" ID="lblZarghanObserveMeter" Text="زیربنا نظارت زرقان"></dxcp:ASPxLabel>

                                        </td>
                                        <td>
                                            <dxcp:ASPxLabel runat="server" ID="txtZarghanObserveMeter" ClientInstanceName="txtZarghanObserveMeter" Text="---" Width="100%" NullText="0">
                                            </dxcp:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxcp:ASPxLabel ClientInstanceName="lblDareyonObserveMeter" runat="server" ID="lblDareyonObserveMeter" Text="زیربنا نظارت داریون"></dxcp:ASPxLabel>

                                        </td>
                                        <td>
                                            <dxcp:ASPxLabel runat="server" ID="txtDareyonObserveMeter" ClientInstanceName="txtDareyonObserveMeter" Text="---" Width="100%" NullText="0">
                                            </dxcp:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dxcp:ASPxLabel ClientInstanceName="lblBonyadMaskan" runat="server" ID="lblBonyadMaskan" Text="زیربنا نظارت بنیاد مسکن"></dxcp:ASPxLabel>
                                        </td>
                                        <td>
                                            <dxcp:ASPxLabel runat="server" ID="txtBonyadMaskan" Width="100%" Text="---">
                                            </dxcp:ASPxLabel>
                                        </td>
                                    </tr>

                                    <%--  <tr>
                                        <td colspan="4">
                                            <dxcp:ASPxLabel CssClass="HelpUL" Font-Bold="true" runat="server" Width="100%" ID="ASPxLabel7" Text="مابقی ظرفیت نظارت جزء ظرفیت نظارت شهرستان محسوب و در سامانه خدمات مهندسی ثبت می گردد.">
                                            </dxcp:ASPxLabel>
                                        </td>

                                    </tr>--%>
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
                        <fieldset id="Fieldset2"
                            runat="server">
                            <legend class="HelpUL" id="TitleDesignCapacity" runat="server">حداکثر زیربنا طراحی</legend>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td width="30%">
                                            <dxcp:ASPxLabel runat="server" ID="lblDesignShirazMunicipality" ClientInstanceName="lblDesignShirazMunicipality" Text="زیربنا طراحی شهرداری شیراز"></dxcp:ASPxLabel>
                                            <dxcp:ASPxLabel runat="server" ID="lblDesignShirazMunicipalityLimitation" ClientInstanceName="lblDesignShirazMunicipalityLimitation" Style="color: red; text-decoration: none"></dxcp:ASPxLabel>

                                        </td>
                                        <td>
                                            <dxcp:ASPxLabel runat="server" ID="txtDesignShirazMunicipality" ClientInstanceName="txtDesignShirazMunicipality" Width="100%" Text="---">
                                            </dxcp:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>زیربنا طراحی بنیاد مسکن</td>
                                        <td>
                                            <dxcp:ASPxLabel runat="server" ID="txtDesignBonyadMaskan" Width="100%" Text="---">
                                            </dxcp:ASPxLabel>
                                        </td>
                                    </tr>
                                    <%--    <tr>
                                        <td colspan="4">
                                            <dxcp:ASPxLabel CssClass="HelpUL" Font-Bold="true" runat="server" Width="100%" ID="ASPxLabel12" Text="مابقی ظرفیت طراحی جزء ظرفیت استان محسوب شده و در سامانه خدمات مهندسی ثبت می گردد.">
                                            </dxcp:ASPxLabel>
                                        </td>

                                    </tr>--%>
                                </tbody>
                            </table>
                        </fieldset>
                    </dxcp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxPanel>

            <dxcp:ASPxPanel ID="RoundPanelUrbenismCapacity" ClientInstanceName="RoundPanelUrbenismCapacity"
                runat="server">
                <PanelCollection>
                    <dxcp:PanelContent>
                        <fieldset id="Fieldset3"
                            runat="server">
                            <legend class="HelpUL" id="Legend1" runat="server">حداکثر ظرفیت شهرسازی</legend>
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
                                            <dxcp:ASPxLabel runat="server" ID="lblShirazMunicipulityUrbenismTarh" ClientInstanceName="lblShirazMunicipulityUrbenismTarh" Text="حداکثر تهیه طرح های شهرسازی شهرداری شیراز"></dxcp:ASPxLabel>                                           

                                        </td>
                                        <td>
                                            <dxcp:ASPxLabel runat="server" ID="txtShirazMunicipulityUrbenismTarh" ClientInstanceName="txtShirazMunicipulityUrbenismTarh" Width="100%" Text="---">
                                            </dxcp:ASPxLabel>
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
                                            <dxcp:ASPxLabel runat="server" ID="lblShirazMunicipulityUrbenismEntebaghShahri" ClientInstanceName="lblShirazMunicipulityUrbenismEntebaghShahri" Text="حداکثر تهیه طرح انطباق شهری شهرداری شیراز"></dxcp:ASPxLabel>                                            
                                        </td>
                                        <td>
                                            <dxcp:ASPxLabel runat="server" ID="txtShirazMunicipulityUrbenismEntebaghShahri" ClientInstanceName="txtShirazMunicipulityUrbenismEntebaghShahri" Width="100%" Text="---">
                                            </dxcp:ASPxLabel>
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
