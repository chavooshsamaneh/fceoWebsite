<%@ Page Title="سوابق عضویت در دفاتر" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="EngOffice.aspx.cs" Inherits="Members_EngOfficeView_EngOffice" %>

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
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="Content" runat="server" style="width: 100%" align="center">
        <div id="DivReport" runat="server" class="DivErrors" style="text-align: right">
            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                href="#">بستن</a>]</div>
        <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
            <PanelCollection>
                <dxp:PanelContent>
                    <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                        width="100%">
                        <tr>
                            <td align="right">
                                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                    <tr>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server"  EnableTheming="False"
                                                EnableViewState="False" OnClick="btnView_Click" ToolTip="مشاهده" Text=" " UseSubmitBehavior="False">
                                                <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanelMenu>
        <br />
        <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
            Width="100%" KeyFieldName="EngOfId" 
             DataSourceID="OdbEngOffice" ClientInstanceName="grid" OnDetailRowExpandedChanged="CustomAspxDevGridView1_DetailRowExpandedChanged"
            OnFocusedRowChanged="CustomAspxDevGridView1_FocusedRowChanged" OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared"
            OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize">
            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" ColumnResizeMode="Control" />
            <Styles  >
                <Header HorizontalAlign="Center">
                </Header>
                <GroupPanel ForeColor="Black">
                </GroupPanel>
            </Styles>
            <Settings ShowGroupPanel="True" ShowHorizontalScrollBar="true" ShowFilterRow="true"
                ShowFilterRowMenu="true" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="کد دفتر" FieldName="EngOfId" Name="EngOfId"
                    Visible="true" Width="50px" VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="نوع دفتر" FieldName="OfTName" VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="OfpName" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="وضعیت عضو" FieldName="MeInActive" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="شماره مشارکت نامه" FieldName="ParticipateLetterNo"
                    VisibleIndex="3">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="تاریخ مشارکت نامه" FieldName="ParticipateLetterDate"
                    VisibleIndex="4">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="شماره دفتر اسناد رسمی" FieldName="EngOffNo"
                    VisibleIndex="5">
                    <CellStyle Wrap="False">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="تعداد اعضا" FieldName="MeCount" Name="MeCount"
                    VisibleIndex="6">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="وضعیت دفتر" FieldName="InActiveName" VisibleIndex="7">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                    VisibleIndex="8">
                    <DataItemTemplate>
                        <div align="center">
                            <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl="~/Images/WFStart.png">
                            </dxe:ASPxImage>
                        </div>
                    </DataItemTemplate>
                    <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                        ValueType="System.String">
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="10" Width="30px" ShowClearFilterButton="true">          
                </dxwgv:GridViewCommandColumn>
            </Columns>
            <Templates>
                <DetailRow>
                    <div align="center">
                        <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridViewRequest" runat="server"
                            AutoGenerateColumns="False"  
                            DataSourceID="OdbRequest" KeyFieldName="EOfId" OnBeforePerformDataSelect="CustomAspxDevGridViewRequest_BeforePerformDataSelect"
                            OnHtmlDataCellPrepared="CustomAspxDevGridViewRequest_HtmlDataCellPrepared" Width="100%"
                            OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize">
                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                            <Styles  >
                                <GroupPanel ForeColor="Black">
                                </GroupPanel>
                                <Header HorizontalAlign="Center">
                                </Header>
                            </Styles>
                            <Columns>
                                <dxwgv:GridViewDataTextColumn FieldName="EOfId" Name="EOfId" Visible="False" VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="تاریخ درخواست" FieldName="CreateDate" VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="نوع تأیید" FieldName="Confirm" VisibleIndex="6">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="تاریخ پاسخ" FieldName="AnswerDate" Visible="False"
                                    VisibleIndex="2">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="نوع درخواست" FieldName="TypeName" VisibleIndex="2">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="درخواست دهنده" FieldName="RequesterType" VisibleIndex="1">
                                    <CellStyle Wrap="True">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Type" Visible="False" VisibleIndex="5">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="شماره سریال" FieldName="SerialNo" VisibleIndex="4">
                                    <HeaderStyle Wrap="True" />
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="شماره پروانه اشتغال" FieldName="FileNo" VisibleIndex="3">
                                    <HeaderStyle Wrap="True" />
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="تاریخ صدور" FieldName="RegDate" Visible="False"
                                    VisibleIndex="2">
                                    <HeaderStyle Wrap="True" />
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان اعتبار" FieldName="ExpireDate"
                                    Visible="False" VisibleIndex="3">
                                    <HeaderStyle Wrap="True" />
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="وضعیت درخواست" FieldName="TaskName" VisibleIndex="7"
                                    Visible="false">
                                    <CellStyle Wrap="True">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="EngOfId" Visible="False" VisibleIndex="9">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                                    VisibleIndex="9">
                                    <DataItemTemplate>
                                        <div align="center">
                                            <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl="~/Images/WFStart.png">
                                            </dxe:ASPxImage>
                                        </div>
                                    </DataItemTemplate>
                                    <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                                        ValueType="System.String">
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="11" Width="30px" ShowClearFilterButton="true">
                                </dxwgv:GridViewCommandColumn>
                            </Columns>
                            <Settings ShowFilterRow="True" ShowGroupPanel="True" />
                        </TSPControls:CustomAspxDevGridView>
                    </div>
                </DetailRow>
            </Templates>
            <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
        </TSPControls:CustomAspxDevGridView>
        <br />
        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
            Width="100%">
            <PanelCollection>
                <dxp:PanelContent>
                    <table cellpadding="0" style="display: block; overflow: hidden; border-collapse: collapse"
                        width="100%">
                        <tr>
                            <td align="right">
                                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                    <tr>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server"  EnableTheming="False"
                                                EnableViewState="False" OnClick="btnView_Click" ToolTip="مشاهده" Text=" " UseSubmitBehavior="False">
                                                <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                </HoverStyle>
                                                <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanelMenu>
        <asp:ObjectDataSource ID="OdbEngOffice" runat="server" SelectMethod="SelectEngOfficeByMeId"
            TypeName="TSP.DataManager.EngOfficeManager" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="OdbRequest" runat="server" SelectMethod="FindByEngOfficeId"
            TypeName="TSP.DataManager.EngOffFileManager" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="" Name="EngOfId" SessionField="EngOfficeId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkId"
            TypeName="TSP.DataManager.WorkFlowTaskManager">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="WorkFlowId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="TaskOrder" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
            DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
    </div>
</asp:Content>
