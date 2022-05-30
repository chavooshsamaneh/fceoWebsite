<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="ExGroupPeriod.aspx.cs" Inherits="Employee_ExGroup_ExGroupPeriod" Title="مدیریت دوره ها" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls.FormCreatorComponents"
    TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server"></asp:Label>[<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dx:PanelContent>


  
                            <table >
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnnew" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnnew_Click" ToolTip="جدید" CausesValidation="False">
                                            <Image  Url="~/Images/new.png" >
                                            </Image>
                                         
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnedit" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnedit_Click" ToolTip="ویرایش" CausesValidation="False">
                                            <Image  Url="~/Images/icons/edit.png" >
                                            </Image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnview" runat="server"  EnableTheming="False"
                                            EnableViewState="False" ToolTip="مشاهده" OnClick="btnview_Click">
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator8"></TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btndel" runat="server"  EnableTheming="False"
                                            EnableViewState="False" ToolTip="حذف" OnClick="btndel_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                            <Image Url="~/Images/icons/delete.png">
                                            </Image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                            AutoPostBack="false" CausesValidation="False" Text=" " 
                                            EnableTheming="False" ToolTip="غیر فعال" ID="btninactive" EnableViewState="False"
                                            OnClick="btnInActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
 {
  if(confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟'))
  grid.PerformCallback('inactive');
  }

}"></ClientSideEvents>
                                            <Image  Url="~/Images/icons/disactive.png">
                                            </Image>
                                            
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                            ID="btnprint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {	
	grid.PerformCallback('print');
}"></ClientSideEvents>
                                            
                                            <Image  Url="~/Images/icons/printers.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                            ID="btnExportExcel" AutoPostBack="false" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s,e){btnTempExport.DoClick(); }"></ClientSideEvents>
                                           
                                            <Image  Url="~/Images/icons/ExportExcel.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                       </dx:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>


            <br />
            <TSPControls:CustomAspxDevGridView ID="gridViewExGroupPeriod" runat="server" DataSourceID="ObjectDataSourceExGroupPeriod"
                Width="100%"  
                ClientInstanceName="grid" AutoGenerateColumns="False" KeyFieldName="ExGroupPeriodId"
                OnCustomCallback="gridViewExGroupPeriod_CustomCallback">
                <ClientSideEvents EndCallback="function(s, e) {
    if(s.cpError==1)
     {
       ShowMessage(s.cpMsg);
       s.cpError=0;
       s.cpMsg = '';
     }
       if(s.cpDoPrint==1)
            {
               s.cpDoPrint = 0;
	           window.open('../../Print.aspx');
            }
}" />
                <Templates>
                    <DetailRow>
                        <TSPControls:CustomAspxDevGridView ID="GridViewCandidate" runat="server" DataSourceID="ObjectDataSourceCandidate"
                              AutoGenerateColumns="False"
                            KeyFieldName="CandidateId" OnBeforePerformDataSelect="GridViewCandidate_BeforePerformDataSelect">

                            <Columns>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FirstName" Caption="نام">
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="LastName" Caption="نام خانوادگی">
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="IsManagerName" Caption="نوع نامزد">
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="StatusName" Caption="وضعیت نامزد">
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="VoteCount" Caption="تعداد آرا">
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="Position" Caption="سمت">
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="InActiveName" Caption="وضعیت فعالیت">
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>

                            <SettingsDetail IsDetailGrid="True"></SettingsDetail>
                        </TSPControls:CustomAspxDevGridView>
                    </DetailRow>
                </Templates>
                <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>

                <Columns>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="ExGroupName" Caption="نام گروه/کمیته">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="150px" FieldName="PeriodName" Caption="نام دوره">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="StartDate" Name="Date"
                        Width="80px" Caption="تاریخ شروع">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="EndDate" Name="Date" Width="80px"
                        Caption="تاریخ پایان">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="Status" Caption="وضعیت">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataMemoColumn VisibleIndex="4" FieldName="Description" Width="200px"
                        Caption="توضیحات">
                    </dxwgv:GridViewDataMemoColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="5" Caption=" " ShowClearFilterButton="true">
         
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm"></SettingsEditing>

                <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dx:PanelContent>
                            <table >
                                <tr>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnnew2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnnew_Click" ToolTip="جدید" CausesValidation="False">
                                            <Image  Url="~/Images/new.png" >
                                            </Image>
                                            
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnedit2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" OnClick="btnedit_Click" ToolTip="ویرایش" CausesValidation="False">
                                            <Image  Url="~/Images/icons/edit.png" >
                                            </Image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnview2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" ToolTip="مشاهده" OnClick="btnview_Click">
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btndel2" runat="server"  EnableTheming="False"
                                            EnableViewState="False" ToolTip="حذف" OnClick="btndel_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                            <Image Url="~/Images/icons/delete.png">
                                            </Image>
                                           
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                            AutoPostBack="false" CausesValidation="False" Text=" " 
                                            EnableTheming="False" ToolTip="غیر فعال" ID="btninactive2" EnableViewState="False"
                                            OnClick="btnInActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
		 
if (grid.GetFocusedRowIndex()&lt;0)
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
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                            ID="btnprint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {	
	grid.PerformCallback('print');
}"></ClientSideEvents>
                                           
                                            <Image  Url="~/Images/icons/printers.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td align="right" valign="top">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                            ID="btnExportExcel2" AutoPostBack="false" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s,e){btnTempExport.DoClick(); }"></ClientSideEvents>
                                           
                                            <Image  Url="~/Images/icons/ExportExcel.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </table>
                        </dx:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
           
            <asp:ObjectDataSource ID="ObjectDataSourceExGroupPeriod" runat="server" TypeName="TSP.DataManager.ExGroupPeriodManager"
                SelectMethod="GetData"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceCandidate" runat="server" TypeName="TSP.DataManager.CandidateManager"
                SelectMethod="FindByExGroupPeriodId" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="-1" Name="ExGroupPeriodId" SessionField="ExGroupPeriodId"
                        Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="gridViewExGroupPeriod">
    </dxwgv:ASPxGridViewExporter>
    <TSPControls:CustomAspxButton ID="btnTempExport" ClientVisible="false" ClientInstanceName="btnTempExport"
        runat="server" OnClick="btntemp_Click">
    </TSPControls:CustomAspxButton>
</asp:Content>
