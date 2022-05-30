<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeJob.aspx.cs" Inherits="Employee_OfficeRegister_OfficeJob"
    Title="سوابق کاری" %>

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
<%@ Register Src="~/UserControl/OfficeInfoUserControl.ascx" TagName="OfficeInfoUserControl"
    TagPrefix="UserControlOfficeInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                            width="100%">
                            <tbody>
                                <tr>
                                    <td valign="top" align="right">
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                                            EnableTheming="False" ToolTip="جدید" ID="BtnNew" EnableViewState="False" OnClick="BtnNew_Click">
                                                            <image  url="~/Images/icons/new.png">
                                                                        </image>
                                                            
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                                            Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit" EnableViewState="False"
                                                            OnClick="btnEdit_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                                            <image  url="~/Images/icons/edit.png">
                                                                        </image>
                                                         
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                                            EnableTheming="False" ToolTip="مشاهده" ID="btnView" EnableViewState="False" OnClick="btnView_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                                            <image  url="~/Images/icons/view.png">
                                                                        </image>
                                                       
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                                            Text=" " EnableTheming="False" ToolTip="غیر فعال"
                                                            ID="btnInActive" EnableViewState="False" OnClick="btnInActive_Click">
                                                            <ClientSideEvents Click="function(s, e) {
if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                            <image  url="~/Images/icons/disactive.png">
                                                                        </image>
                                                          
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                  
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False"
                                                            Text=" " EnableTheming="False" ToolTip="بازگشت"
                                                            ID="btnBack" EnableViewState="False" OnClick="btnBack_Click">
                                                            <image  url="~/Images/icons/Back.png">
                                                                        </image>
                                                           
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت اعضای حقوقی"
                                                            CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                            
                                                            <image  url="~/Images/icons/BakToManagment.png">
                                                                        </image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server"
                OnItemClick="ASPxMenu1_ItemClick">
                <Items>
                    <dxm:MenuItem Text="مشخصات شرکت" Name="Office">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="اعضا" Name="Member">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="شعبه ها" Name="Agent">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="سوابق کاری" Name="Job" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="روزنامه های رسمی" Name="Letters">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Financial" Text="وضعیت مالی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مستندات" Name="Attach">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Group" Text="گروه ها">
                    </dxm:MenuItem>
                </Items>

            </TSPControls:CustomAspxMenuHorizontal>

            <br />
            <UserControlOfficeInfo:OfficeInfoUserControl ID="OfficeInfoUserControl" runat="server" />
            <br />
            <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server"
                DataSourceID="ObjectDataSource1" Width="100%"
                OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize"
                OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared" OnDetailRowExpandedChanged="CustomAspxDevGridView1_DetailRowExpandedChanged"
                OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared" KeyFieldName="JhId"
                AutoGenerateColumns="False" ClientInstanceName="jgrid">

                <Columns>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="JhId" Name="JhId">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectName" Caption="نام پروژه"
                        Name="ProjectName" Width="200px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="TypeName" Caption="نوع پروژه"
                        Name="TypeName">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="ProjectPosition" Caption="سمت"
                        Name="ProjectPosition">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="StartCorporateDate" Caption="تاریخ شروع همکاری"
                        Name="StartCorporateDate">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" Visible="False" FieldName="EndCorporateDate"
                        Caption="تاریخ پایان همکاری" Name="EndCorporateDate">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="Area" Caption="زیربنا"
                        Name="Area">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="CorName" Caption="نحوه مشارکت"
                        Name="CorName" Width="150px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="CreateDate" Caption="تاریخ ایجاد"
                        Name="CreateDate">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="InActiveName" Caption="وضعیت">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="CitName" Caption="شهر">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="TableId" Visible="False" VisibleIndex="8">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="11" Width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>


                <Templates>
                    <DetailRow>
                        <div align="center">
                            <TSPControls:CustomAspxDevGridView ID="GridViewJudge" runat="server" AutoGenerateColumns="False"
                                DataSourceID="ObjdsJudgment"
                                EnableViewState="False" KeyFieldName="JudgeId" OnBeforePerformDataSelect="GridViewJudge_BeforePerformDataSelect"
                                OnHtmlDataCellPrepared="GridViewJudge_HtmlDataCellPrepared" Width="100%">
                                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" ColumnResizeMode="Control" />

                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="0">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="1">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="NcName" VisibleIndex="2">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="شماره جلسه" FieldName="MeetingId" VisibleIndex="3">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="امتیاز" FieldName="FactValue" VisibleIndex="5">
                                        <PropertiesTextEdit DisplayFormatString="#.###">
                                            <Style HorizontalAlign="Right"></Style>
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="نظر کارشناسی" FieldName="JudgeViewPoint" VisibleIndex="8"
                                        Width="300px">
                                        <CellStyle Wrap="True">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="تاریخ جلسه" FieldName="MeetingDate" VisibleIndex="4">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="Confirm" VisibleIndex="6">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>

                                <SettingsDetail IsDetailGrid="True" />
                            </TSPControls:CustomAspxDevGridView>
                        </div>
                    </DetailRow>
                </Templates>
                <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
            </TSPControls:CustomAspxDevGridView>
      
     
                        <fieldset id="ASPxRoundPanelValues"  runat="server"><legend class="HelpUL">مجموع مطلوبیت کارهای اجرا شده</legend>  <TSPControls:CustomAspxDevGridView runat="server"
                            KeyFieldName="OfdId" AutoGenerateColumns="False" RightToLeft="True"
                            DataSourceID="ObjectDataSourceFactorValues" Width="100%" ID="CustomAspxDevGridView2"
                            >
                            <Columns>
                                <dxwgv:GridViewDataTextColumn FieldName="SumValue" Caption="امتیاز" VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="Name" Caption="نوع مطلوبیت کار" VisibleIndex="1">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>

                        </TSPControls:CustomAspxDevGridView></fieldset>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                            EnableTheming="False" ToolTip="جدید" ID="BtnNew2" EnableViewState="False" OnClick="BtnNew_Click">
                                            <image  url="~/Images/icons/new.png">
                                                                        </image>
                                            
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                            Width="25px" EnableTheming="False" ToolTip="ویرایش" ID="btnEdit2" EnableViewState="False"
                                            OnClick="btnEdit_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                            <image  url="~/Images/icons/edit.png">
                                                                        </image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" "
                                            EnableTheming="False" ToolTip="مشاهده" ID="btnView2" EnableViewState="False"
                                            OnClick="btnView_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                            <image  url="~/Images/icons/view.png">
                                                                        </image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                            Text=" " EnableTheming="False" ToolTip="غیر فعال"
                                            ID="btnInActive2" EnableViewState="False" OnClick="btnInActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                            <image  url="~/Images/icons/disactive.png">
                                                                        </image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                   
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" CausesValidation="False"
                                            Text=" " EnableTheming="False" ToolTip="بازگشت"
                                            ID="ASPxButton1" EnableViewState="False" OnClick="btnBack_Click">
                                            <image  url="~/Images/icons/Back.png">
                                                                        </image>
                                         
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت اعضای حقوقی"
                                            CausesValidation="False" ID="ASPxButton2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBackToManagment_Click">
                                           
                                            <image  url="~/Images/icons/BakToManagment.png">
                                                                        </image>
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
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="FindByMeRequest"
        TypeName="TSP.DataManager.ProjectJobHistoryManager" OldValuesParameterFormatString="original_{0}">
        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="MReId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="IsConfirm" Type="Int16" />
            <asp:Parameter DefaultValue="1" Name="Type" Type="Int16" />
            <asp:Parameter DefaultValue="-1" Name="TableType" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="JustActive" Type="Int16" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="OfficeId" runat="server" Visible="False" />
    <asp:HiddenField ID="PgMode" runat="server" />
    <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False" />
    <asp:HiddenField ID="HDMode" runat="server" Visible="False" />
    <dxhf:ASPxHiddenField ID="HiddenFieldOffice" runat="server">
    </dxhf:ASPxHiddenField>
    <asp:ObjectDataSource ID="ObjdsJudgment" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="SelectByJobHistory" TypeName="TSP.DataManager.TrainingJudgmentManager">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="-1" Name="PkId" SessionField="PKId" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="JudgeId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceFactorValues" runat="server" SelectMethod="SelectDocOffJobHistoryQualityValues"
        TypeName="TSP.DataManager.DocOffOfficeFactorDocumentsManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
