<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficeFinancialStatus.aspx.cs" Inherits="Employee_OfficeRegister_OfficeFinancialStatus"
    Title="وضعیت مالی" %>

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
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>
                      <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    >
                    <PanelCollection>
                        <dxp:PanelContent>


  
                                                    <table >
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                        ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="BtnNew_Click">                                                                        
                                                                        <image  Url="~/Images/icons/new.png">
                                                                        </image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                        Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnEdit_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                                                     
                                                                        <Image  Url="~/Images/icons/edit.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                                        ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnView_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                                                       
                                                                        <Image  Url="~/Images/icons/view.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیر فعال"
                                                                        ID="btnInActive" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnInActive_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                                       
                                                                        <Image  Url="~/Images/icons/disactive.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="نظر کارشناسی"
                                                                        ID="btnJudgment" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnJudgment_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                                                      
                                                                        <Image  Url="~/Images/icons/User comment.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td width="10px" align="center">
                                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                        CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnBack_Click">
                                                                       
                                                                        <Image  Url="~/Images/icons/Back.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                   <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت اعضای حقوقی"
                                                                        CausesValidation="False" ID="ASPxButton4" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                                       
                                                                        <Image  Url="~/Images/icons/BakToManagment.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                              </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                    <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server" 
                      
                        OnItemClick="ASPxMenu1_ItemClick" >
                        <Items>
                            <dxm:MenuItem Text="مشخصات شرکت" Name="Office">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="اعضا" Name="Member">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="شعبه ها" Name="Agent">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Job" Text="سوابق کاری">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="روزنامه های رسمی" Name="Letters">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Financial" Text="وضعیت مالی" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="مستندات" Name="Attach">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Group" Text="گروه ها">
                            </dxm:MenuItem>
                        </Items>
                       
                    </TSPControls:CustomAspxMenuHorizontal>
                </div>
                    <br />
                <UserControlOfficeInfo:OfficeInfoUserControl ID="OfficeInfoUserControl" runat="server" />
<br />
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" Width="100%" runat="server"
                    DataSourceID="ObjectDataSource1" 
                     KeyFieldName="OfsId" AutoGenerateColumns="False" ClientInstanceName="jgrid"
                    OnDetailRowExpandedChanged="CustomAspxDevGridView1_DetailRowExpandedChanged"
                    OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared" OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared"
                    OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	//jgrid.ExpandDetailRow(jgrid.GetFocusedRowIndex());
}"></ClientSideEvents>
                    <Templates>
                        <DetailRow>
                            <TSPControls:CustomAspxDevGridView ID="GridViewJudge" runat="server" DataSourceID="ObjdsJudgment"
                                Width="100%"  
                                EnableViewState="False" AutoGenerateColumns="False" KeyFieldName="JudgeId" OnBeforePerformDataSelect="GridViewJudge_BeforePerformDataSelect"
                                OnHtmlDataCellPrepared="GridViewJudge_HtmlDataCellPrepared">
                                <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                             
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FirstName" Caption="نام">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="LastName" Caption="نام خانوادگی">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="NcName" Caption="سمت">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="MeetingId" Caption="شماره جلسه">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="FactValue" Caption="امتیاز">
                                        <PropertiesTextEdit DisplayFormatString="#.###">
                                            <Style HorizontalAlign="Right"></Style>
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="JudgeViewPoint" Width="200px"
                                        Caption="نظر کارشناسی">
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
                             
                                <SettingsDetail IsDetailGrid="True"></SettingsDetail>
                            </TSPControls:CustomAspxDevGridView>
                        </DetailRow>
                    </Templates>
                  
                 
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="OfsId" Name="OfsId" Visible="False" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام وضعیت" FieldName="Name" Name="Name" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ضریب" FieldName="Factor" VisibleIndex="1"
                            Width="60px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="امتیاز" FieldName="FactorValue" VisibleIndex="2"
                            Width="60px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                            <PropertiesTextEdit DisplayFormatString="#.###">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="تاریخ ایجاد" FieldName="CreateDate" Name="CreateDate"
                            VisibleIndex="3" Width="80px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="3"
                            Width="50px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="4" Width="30px" ShowClearFilterButton="true">
                       
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="OfReId" Visible="False" VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
               
                    <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
                </TSPControls:CustomAspxDevGridView>
                <br />
                       <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    >
                    <PanelCollection>
                        <dxp:PanelContent>


  
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                        ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="BtnNew_Click">
                                                                       
                                                                        <Image  Url="~/Images/icons/new.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                        Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnEdit_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                                                        
                                                                        <Image  Url="~/Images/icons/edit.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                                        ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnView_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                                                      
                                                                        <Image  Url="~/Images/icons/view.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیر فعال"
                                                                        ID="btnInActive2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnInActive_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                                        
                                                                        <Image  Url="~/Images/icons/disactive.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="نظر کارشناسی"
                                                                        ID="btnJudgment2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnJudgment_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                                                       
                                                                        <Image  Url="~/Images/icons/User comment.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td width="10px" align="center">
                                                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                                        CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnBack_Click">
                                                                      
                                                                        <Image  Url="~/Images/icons/Back.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                   <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت به مدیریت اعضای حقوقی"
                                                                        CausesValidation="False" ID="ASPxButton2" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                                        
                                                                        <Image  Url="~/Images/icons/BakToManagment.png">
                                                                        </Image>
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
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="FindByOffRequest"
            TypeName="TSP.DataManager.DocOffOfficeFinancialStatusManager" OldValuesParameterFormatString="original_{0}">
            <FilterParameters>
                <asp:Parameter Name="newparameter" />
            </FilterParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="OfId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="OfReId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="IsConfirm" Type="Int16" />
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
            SelectMethod="SelectByFinancial" TypeName="TSP.DataManager.TrainingJudgmentManager">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="-1" Name="PkId" SessionField="PKId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="JudgeId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

</asp:Content>
