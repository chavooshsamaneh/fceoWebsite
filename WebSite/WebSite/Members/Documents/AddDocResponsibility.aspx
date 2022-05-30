<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AddDocResponsibility.aspx.cs" Inherits="Members_Documents_AddDocResponsibility"
    Title="مشخصات پایه - صلاحیت" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Src="../../UserControl/MemberInfoUserControl.ascx" TagName="MemberInfoUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 100%" align="center" dir="ltr">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                 <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                                <table  >
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="جدید"  ToolTip="جدید"
                                                    CausesValidation="False" Width="25px" ID="btnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnNew_Click">
                                                
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ویرایش"  ToolTip="ویرایش"
                                                    CausesValidation="False" Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="ذخیره"  ToolTip="ذخیره"
                                                    Width="25px" ID="btnSave" AutoPostBack="true" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" CausesValidation="true" OnClick="btnSave_Click">
                                               
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="بازگشت"  ToolTip="بازگشت"
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
                <br />
                <uc2:MemberInfoUserControl ID="MemberInfoUserControl1" runat="server" />
                <br />
                <TSPControls:CustomAspxCallbackPanel ID="CallbackRes" runat="server" Width="100%" OnCallback="CallbackRes_Callback"
                    ClientInstanceName="CallbackRes" HideContentOnCallback="False" >
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent2" runat="server">
                            <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
                                CalendarDayWidth="31" CalendarDayHeight="15" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
                                FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                                WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
                            </pdc:PersianDateScriptManager>
                            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                                    href="#"><span style="color: #000000">ب</span>ستن</a>]</div>
                            <table>
                                <tr>
                                    <td style="width: 100%" align="center">
                                        <dxe:ASPxLabel runat="server" Font-Bold="true" Text="دستورالعمل درخواست" ID="txtRequestComment"
                                            ForeColor="DarkRed" Visible="false">
                                        </dxe:ASPxLabel>
                                        <dxe:ASPxLabel runat="server" Font-Bold="true" Text="هشدار" ID="txtComment" ForeColor="DarkRed"
                                            Visible="false">
                                        </dxe:ASPxLabel>
                                    </td>
                            </table>
                            <br />
                            <TSPControls:CustomASPxRoundPanel ID="RoundPanelMeFileDetail" HeaderText="مشاهده"
                                runat="server" Width="100%">
                                <PanelCollection>
                                    <dxp:PanelContent>
                                        <div dir="rtl">
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td valign="top" align="right" style="width: 15%">
                                                            رشته موضوع پروانه *
                                                        </td>
                                                        <td valign="top" align="right" style="width: 35%">
                                                            <TSPControls:CustomAspxComboBox RightToLeft="True" runat="server"  Width="100%"
                                                                 TextField="FMjName" ID="cmbMajor" DataSourceID="ObjdsMemberFileMajor"
                                                                ValueType="System.String" ValueField="MFMjId" ClientInstanceName="cmbMajor" >
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {                                                           
	                                                                    CallbackRes.PerformCallback('Major'+';'+cmbMajor.GetValue());
                                                                    }" />
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                                    </ErrorFrameStyle>
                                                                    <RequiredField ErrorText="رشته را انتخاب نمایید" IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </TSPControls:CustomAspxComboBox>
                                                            <dxe:ASPxLabel ID="lblIsMasterMajor" runat="server" ClientInstanceName="lblIsMasterMajor"
                                                                ClientVisible="False" ForeColor="Red" Text="رشته موضوع پروانه می باشد">
                                                            </dxe:ASPxLabel>
                                                            <asp:ObjectDataSource ID="ObjdsMemberFileMajor" runat="server" TypeName="TSP.DataManager.DocMemberFileMajorManager"
                                                                SelectMethod="SelectMemberFileById" OldValuesParameterFormatString="original_{0}">
                                                                <SelectParameters>
                                                                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="MFId"></asp:Parameter>
                                                                    <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                                                                    <asp:Parameter DefaultValue="0" Name="InActive" Type="Int32" />
                                                                </SelectParameters>
                                                            </asp:ObjectDataSource>
                                                        </td>
                                                        <td style="vertical-align: top" align="right" style="width: 15%">
                                                            رشته و مقطع تحصیلی
                                                        </td>
                                                        <td style="vertical-align: top" align="right" style="width: 35%">
                                                            <TSPControls:CustomTextBox runat="server" ID="txtMeLicence"  Width="100%"
                                                                EnableClientSideAPI="false" >
                                                                <ValidationSettings>
                                                                    
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            شیوه اخذ صلاحیت *
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <TSPControls:CustomAspxComboBox Width="100%" runat="server" ValueType="System.String" DataSourceID="ObjdsMeDocAcceptType"
                                                                TextField="ActTypeName" ValueField="ActTypeId" AutoPostBack="false" 
                                                                  ID="cmbDocMeAcceptType"
                                                                RightToLeft="True" ClientInstanceName="cmbDocMeAcceptType">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png">
                                                                    </ErrorImage>
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                    <RequiredField IsRequired="True" ErrorText="شیوه اخذ صلاحیت را انتخاب نمایید"></RequiredField>
                                                                </ValidationSettings>
                                                                <ClientSideEvents ValueChanged="function(s, e) {
	                                                                    CallbackRes.PerformCallback('ActType');
                                                                    }" />
                                                            </TSPControls:CustomAspxComboBox>
                                                            <asp:ObjectDataSource ID="ObjdsMeDocAcceptType" runat="server" UpdateMethod="Update"
                                                                TypeName="TSP.DataManager.DocMemberFileAcceptTypeManager" SelectMethod="GetData">
                                                            </asp:ObjectDataSource>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            پایه - صلاحیت *
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <TSPControls:CustomAspxComboBox RightToLeft="True" runat="server"  Width="100%"
                                                                 TextField="GrdResName" ID="cmbAcceptedGrd"
                                                                DataSourceID="ObjdsAcceptGrad" ValueType="System.String" ValueField="GMRId" ClientInstanceName="cmbAcceptedGrd"
                                                                 AutoPostBack="false">
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                                    </ErrorFrameStyle>
                                                                    <RequiredField ErrorText="پایه و صلاحیت را انتخاب نمایید" IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </TSPControls:CustomAspxComboBox>
                                                            <asp:ObjectDataSource ID="ObjdsAcceptGrad" runat="server" TypeName="TSP.DataManager.DocAcceptedGradeManager"
                                                                SelectMethod="SelectGradForUpgrade">
                                                                <SelectParameters>
                                                                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="MjId"></asp:Parameter>
                                                                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="ObsGrdId"></asp:Parameter>
                                                                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="DesGrdId"></asp:Parameter>
                                                                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="ImpGrdId"></asp:Parameter>
                                                                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="MappingGrdId"></asp:Parameter>
                                                                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="TrafficGrdId"></asp:Parameter>
                                                                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="UrbanismGrdId"></asp:Parameter>
                                                                </SelectParameters>
                                                            </asp:ObjectDataSource>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">
                                                            تاریخ اخذ صلاحیت *
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <pdc:PersianDateTextBox runat="server" DefaultDate="" Width="240px" ShowPickerOnTop="True"
                                                                ID="txtResDate" PickerDirection="ToRight" ShowPickerOnEvent="OnClick" IconUrl="~/Image/Calendar.gif"
                                                                Style="direction: ltr"></pdc:PersianDateTextBox>
                                                            <pdc:PersianDateValidator runat="server" ValidateEmptyText="True" ClientValidationFunction="PersianDateValidator"
                                                                ErrorMessage="تاریخ نامعتبر" ControlToValidate="txtResDate" ID="PersianDateValidator1">تاریخ را انتخاب نمایید</pdc:PersianDateValidator>
                                                        </td>
                                                        <td valign="top" align="right">
                                                            حدود صلاحیت*
                                                        </td>
                                                        <td valign="top" align="right">
                                                            <TSPControls:CustomAspxComboBox RightToLeft="True" runat="server"  Width="100%"
                                                                 TextField="ResRangeName" ID="cmbDocResRange"
                                                                DataSourceID="ObjectDataSourceResRange" ValueType="System.String" ValueField="ResRangeId"
                                                                ClientInstanceName="cmbDocResRange" 
                                                                AutoPostBack="false">
                                                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                    
                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px" />
                                                                    </ErrorFrameStyle>
                                                                    <RequiredField ErrorText="حدود صلاحیت را انتخاب نمایید" IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </TSPControls:CustomAspxComboBox>
                                                            <asp:ObjectDataSource ID="ObjectDataSourceResRange" runat="server" TypeName="TSP.DataManager.DocResponsibilityRangeManager"
                                                                SelectMethod="GetData"></asp:ObjectDataSource>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </TSPControls:CustomASPxRoundPanel>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomAspxCallbackPanel>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>


   
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="جدید"  ToolTip="جدید"
                                                    CausesValidation="False" Width="25px" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                                    EnableViewState="False" EnableTheming="False" OnClick="btnNew_Click">
                                               
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="ویرایش"  ToolTip="ویرایش"
                                                    Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnEdit_Click">
                                              
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue"  runat="server" Text="ذخیره"  ToolTip="ذخیره"
                                                    Width="25px" ID="btnSave2" AutoPostBack="true" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnSave_Click" CausesValidation="true">
                                               
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton CausesValidation="false" runat="server" Text="بازگشت" 
                                                    ToolTip="بازگشت" ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnBack_Click">
                                              
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                                <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldMeFileDetail" ClientInstanceName="HiddenFieldMeFileDetail">
                                </dxhf:ASPxHiddenField>
                             
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
 
</asp:Content>
