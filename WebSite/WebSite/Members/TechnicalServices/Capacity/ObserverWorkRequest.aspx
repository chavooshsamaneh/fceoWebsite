<%@ Page Title="مدیریت اعلام آماده به کاری" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ObserverWorkRequest.aspx.cs" Inherits="Members_TechnicalServices_Capacity_ObserverWorkRequest" %>


<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                visible="true">
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
                                    <td>
                                        <asp:LinkButton ID="BtnNew" CssClass="ButtonMenue" OnClick="BtnNew_Click" runat="server">اعلام آماده به کاری</asp:LinkButton>


                                    </td>
                                    <td>

                                        <asp:LinkButton ID="btnView" CssClass="ButtonMenue" OnClick="btnView_Click" runat="server" OnClientClick="
                                                           	if (grid.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
  alert(&quot;ردیفی انتخاب نشده است&quot;);}">مشاهده اطلاعات آماده به کاری</asp:LinkButton>
                                    </td>

                                    <td>
                                        <td>
                                            <asp:LinkButton ID="btnOffDate" CssClass="ButtonMenue" OnClick="btnOffDate_Click" runat="server">اعلام مرخصی</asp:LinkButton>

                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btnChangeRequest" CssClass="ButtonMenue" OnClick="btnChangeRequest_Click" runat="server">درخواست تغییرات</asp:LinkButton>

                                        </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <%--       <div align="right">
                <ul class="HelpUL">
                    <li></li>
            </div>--%>
            <br />
            <TSPControls:CustomAspxDevGridView Width="100%" ID="GridViewObserverWorkRequest" runat="server"
                DataSourceID="ObjectDataSourceWorkRequest"
                ClientInstanceName="grid" EnableViewState="False" KeyFieldName="ObsWorkReqId" AutoGenerateColumns="False">
                <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
                <SettingsText Title="لیست اعلام آماده به کاری" />
                <Settings ShowTitlePanel="true" ShowHorizontalScrollBar="true"></Settings>
                <Columns>
                    <%--                    <dxwgv:GridViewDataTextColumn FieldName="ObsWorkReqId" Name="ObsWorkReqId" Visible="False" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                        VisibleIndex="0">
                        <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                            ValueType="System.String">
                        </PropertiesComboBox>
                        <DataItemTemplate>
                            <div align="center">
                                <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>' ToolTip='<%# Bind("WfTaskFullName") %>'>
                                </dxe:ASPxImage>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="CreateDate" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <%--   <dxwgv:GridViewDataTextColumn Caption="سال افتتاح ظرفیت" FieldName="CapacityAssignmentYear" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxcp:GridViewDataCheckColumn FieldName="Group1" Caption="گروه ساختمانی الف"
                        Name="Group1" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxcp:GridViewDataCheckColumn>
                    <dxcp:GridViewDataCheckColumn FieldName="Group2" Caption="گروه ساختمانی ب"
                        Name="Group2" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxcp:GridViewDataCheckColumn>
                    <dxcp:GridViewDataCheckColumn FieldName="Group3" Caption="گروه ساختمانی ج"
                        Name="Group3" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxcp:GridViewDataCheckColumn>
                    <dxcp:GridViewDataCheckColumn FieldName="Group4" Caption="گروه ساختمانی د"
                        Name="Group4" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxcp:GridViewDataCheckColumn>
                    <dxcp:GridViewDataCheckColumn FieldName="WantShahrakSanatiMeter" Caption="شهرک صنعتی"
                        Name="WantCharityWork" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxcp:GridViewDataCheckColumn>
                    <dxcp:GridViewDataCheckColumn FieldName="WantEghdamMeliMaskan" Caption="اقدام ملی مسکن"
                        Name="WantEghdamMeliMaskan" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxcp:GridViewDataCheckColumn>
                    <dxcp:GridViewDataCheckColumn FieldName="WantCharityWork" Caption="خیریه"
                        Name="WantCharityWork" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dxcp:GridViewDataCheckColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نوع درخواست" FieldName="RequestTypeName" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تقاضای کار نظارت صدرا/شهرستان" FieldName="IsObserverOffName" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نظارت شهرداری شیراز" FieldName="ShirazMunicipalityMeter" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="طراحی شهرداری شیراز" FieldName="ShirazMunicipalityDesignMeter" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="طراحی بنیاد مسکن" FieldName="BonyadMaskanDesignMeter" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="نظارت بنیاد مسکن" FieldName="BonyadMaskan" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="طرح شهرسازی شهری شهرداری شیراز" FieldName="ShirazMunicipulityUrbenismTarh" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="طرح انطباق شهری شهرداری شیراز" FieldName="ShirazMunicipulityUrbenismEntebaghShahri" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn Caption="خان زنیان" FieldName="KhanZenyanObserveMeter" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="لپویی" FieldName="LapooyObserveMeter" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="زرقان" FieldName="ZarghanObserveMeter" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="داریون" FieldName="DareyonObserveMeter" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شهر انتخابی اول" FieldName="citName1" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="شهر انتخابی دوم" FieldName="citName2" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn Caption="شروع مرخصی" FieldName="StartOffDate" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="پایان مرخصی" FieldName="EndOffDate" VisibleIndex="0">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>

            </TSPControls:CustomAspxDevGridView>
            <%-- <br />

            <fieldset width="98%">
                <legend>راهنما</legend>
                <table width="100%">
                    <tbody>

                        <tr>
                            <td valign="middle" align="right">
                                <asp:Image ID="Image5" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/MeDoc_WFStart.png" />
                                <dxe:ASPxLabel ID="ASPxLabel19" runat="server" Text="درخواست ثبت اطلاعات عضو حقیقی"
                                    ForeColor="Black" Font-Bold="False">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="middle" align="right">
                                <asp:Image ID="Image6" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WF/Member-MembershipUnitConfirmingMember.png" />
                                <dxe:ASPxLabel ID="ASPxLabel21" runat="server" Text="تایید کارمند واحد عضویت"
                                    ForeColor="Black" Font-Bold="False">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="middle" align="right">

                                <asp:Image ID="Image11" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WFConfirmAndEnd.png" />
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="تایید و پایان بررسی درخواست عضو حقیقی"
                                    ForeColor="Black" Font-Bold="False">
                                </dxe:ASPxLabel>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Image ID="Image10" Height="16px" Width="16px" runat="server" ImageUrl="~/Images/WFREjectAndEnd.png" />
                                <dxe:ASPxLabel ID="ASPxLabel29" runat="server" Text="عدم تایید و پایان بررسی درخواست عضو حقیقی"
                                    ForeColor="Black" Font-Bold="False">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="middle" align="right">
                                <asp:Image ID="Image7" Height="16px" Width="16px" runat="server" Visible="false" ImageUrl="~/Images/WF/Member-ExecutiveManagerConfirmingMember.png" />
                                <dxe:ASPxLabel ID="ASPxLabel23" runat="server" Text="تایید مدیر اجرایی" Visible="false" ForeColor="Black"
                                    Font-Bold="False">
                                </dxe:ASPxLabel>
                            </td>
                            <td valign="middle" align="right">
                                <asp:Image ID="Image8" Height="16px" Width="16px" runat="server" Visible="false" ImageUrl="~/Images/WF/Member-AccountingManagerConfirmingMember.png" />
                                <dxe:ASPxLabel ID="ASPxLabel24" runat="server" Text="تایید مدیر امور مالی" Visible="false"
                                    ForeColor="Black" Font-Bold="False">
                                </dxe:ASPxLabel>
                            </td>

                        </tr>

                    </tbody>
                </table>
            </fieldset>--%>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>

                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnNew2" CssClass="ButtonMenue" OnClick="BtnNew_Click" runat="server">اعلام آماده به کاری</asp:LinkButton>


                                    </td>
                                    <td>
                                        <asp:LinkButton ID="btnView2" CssClass="ButtonMenue" OnClick="btnView_Click" runat="server" OnClientClick="
                                                           	if (grid.GetFocusedRowIndex()&lt;0)
 {
   return false;                   
  alert(&quot;ردیفی انتخاب نشده است&quot;);}">مشاهده اطلاعات آماده به کاری</asp:LinkButton>
                                    </td>

                                    <td>
                                        <asp:LinkButton ID="btnOffDate2" CssClass="ButtonMenue" OnClick="btnOffDate_Click" runat="server">اعلام مرخصی</asp:LinkButton>
                                    </td>

                                    <td>
                                        <asp:LinkButton ID="btnChangeRequest2" CssClass="ButtonMenue" OnClick="btnChangeRequest_Click" runat="server">درخواست تغییرات</asp:LinkButton>

                                    </td>
                                    <%--  <td>
                                        <asp:LinkButton ID="btnTracing2" CssClass="ButtonMenue" OnClick="btnTracing_Click" runat="server" OnClientClick="
                                                           	if (grid.GetFocusedRowIndex()&lt;0)
 {
   return false;                   

  alert(&quot;ردیفی انتخاب نشده است&quot;);}">پیگیری گردش کار</asp:LinkButton>
                                    </td>--%>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField ID="HDpage" runat="server">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjectDataSourceWorkRequest" runat="server" TypeName="TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager"
                SelectMethod="SearchForManagmentPage" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="ObsWorkReqId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="ObsWorkReqChangeId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="modalPopup">
                        لطفا صبر نمایید
                            <img src="../../../Image/indicator.gif" align="middle" />
                    </div>
                </ProgressTemplate>
            </asp:ModalUpdateProgress>
            <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" TypeName="TSP.DataManager.WorkFlowTaskManager"
                SelectMethod="SelectByWorkId">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="WorkFlowId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="TaskOrder" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

