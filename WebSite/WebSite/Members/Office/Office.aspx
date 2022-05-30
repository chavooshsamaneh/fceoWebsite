<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Office.aspx.cs" Inherits="Members_Office_Office"
    Title="شرکت ها" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                  <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                    <table  >
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                                        CausesValidation="False" ID="btnView" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnView_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
	
}"></ClientSideEvents>
                                                                   
                                                                        <Image  Url="~/Images/icons/view.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                             
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                              </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <div style="width: 100%; text-align: right">
                    <dxe:ASPxLabel ID="lblSex" runat="server">
                    </dxe:ASPxLabel>
                    <dxe:ASPxLabel ID="lblT" runat="server">
                    </dxe:ASPxLabel>
                    <dxe:ASPxLabel ID="lblOfName" runat="server">
                    </dxe:ASPxLabel>
                </div>
                <br />
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                      KeyFieldName="OfId"
                    DataSourceID="ObjectDataSource1" Width="100%" EnableViewState="False" ClientInstanceName="grid"
                    OnDetailRowExpandedChanged="CustomAspxDevGridView1_DetailRowExpandedChanged"
                    OnFocusedRowChanged="CustomAspxDevGridView1_FocusedRowChanged" OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize"
                    OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared">
                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" ColumnResizeMode="Control" />
                   
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="شماره عضویت" FieldName="OfId" Name="OfId"
                            Visible="False" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام شرکت" FieldName="OfName" VisibleIndex="0"
                            Width="150px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نوع شرکت" FieldName="OtName" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="OfpName" VisibleIndex="1"
                            Width="150px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره تماس" FieldName="Tel1" Visible="false"
                            VisibleIndex="4">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره ثبت" FieldName="RegOfNo" Name="RegOfNo"
                            VisibleIndex="3">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="محل ثبت" FieldName="RegPlace" VisibleIndex="5"
                            Visible="false">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شماره پروانه" FieldName="FileNo" VisibleIndex="4"
                            Visible="False">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataComboBoxColumn Caption="وضعیت" FieldName="MrsId" VisibleIndex="6">
                            <PropertiesComboBox DataSourceID="ODBMrsId" TextField="MrsName" ValueField="MrsId"
                                ValueType="System.String">
                            </PropertiesComboBox>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ ایجاد" FieldName="CreateDate" VisibleIndex="7">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowFilterRowMenu="True" ShowHorizontalScrollBar="true" />
                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	//grid.ExpandDetailRow(grid.GetFocusedRowIndex());
}" />
                    <Templates>
                        <DetailRow>
                                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridViewRequest" runat="server"
                                    AutoGenerateColumns="False" ClientInstanceName="Reqgrid" 
                                     DataSourceID="OdbRequest" KeyFieldName="OfReId" OnBeforePerformDataSelect="CustomAspxDevGridViewRequest_BeforePerformDataSelect"
                                    OnAutoFilterCellEditorInitialize="CustomAspxDevGridViewRequest_AutoFilterCellEditorInitialize"
                                    OnHtmlDataCellPrepared="CustomAspxDevGridViewRequest_HtmlDataCellPrepared" Width="100%">
                                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" ColumnResizeMode="Control" />
                                   
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn FieldName="OfReId" Name="OfReId" Visible="False" VisibleIndex="0">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="کد عضویت" FieldName="OfId" Visible="False"
                                            VisibleIndex="0">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="CreateDate" VisibleIndex="0">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نوع تأیید" FieldName="Confirm" VisibleIndex="6">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="تاریخ پاسخ" FieldName="AnswerDate" VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نوع درخواست" FieldName="TypeName" VisibleIndex="0">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="درخواست دهنده" FieldName="RequesterType" VisibleIndex="1">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="Type" Visible="False" VisibleIndex="5">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="SerialNo" Caption="شماره سریال">
                                            <HeaderStyle Wrap="True" />
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="MFNo" Caption="شماره پروانه"
                                            Width="130px">
                                            <CellStyle Wrap="False">
                                            </CellStyle>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="MFTypeName" Caption="نوع پروانه">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowHorizontalScrollBar="true" />
                                </TSPControls:CustomAspxDevGridView>
                            </div>
                        </DetailRow>
                    </Templates>
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                </TSPControls:CustomAspxDevGridView>
                <br />
                  <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                    <table >
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                                        CausesValidation="False" ID="ASPxButton1" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnView_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
	
}"></ClientSideEvents>
                                                                       
                                                                        <Image  Url="~/Images/icons/view.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                              
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                              </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SelectOfficeMemberByMeId"
                    TypeName="TSP.DataManager.OfficeMemberManager">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="PersonId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODBMrsId" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.MembershipRegistrationStatusManager"
                    FilterExpression="MrsId<{0}">
                    <FilterParameters>
                        <asp:Parameter Name="newparameter" />
                    </FilterParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="OdbRequest" runat="server" SelectMethod="FindByOfficeId"
                    TypeName="TSP.DataManager.OfficeRequestManager" CacheDuration="3600" CacheKeyDependency="CacheOffice">
                    <SelectParameters>
                        <asp:SessionParameter Name="OfId" SessionField="OfficeId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="IsConfirm" Type="Int16" />
                        <asp:Parameter DefaultValue="-1" Name="Type" Type="Int16" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
            DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
 
</asp:Content>
