<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="Project.aspx.cs" Inherits="Members_TechnicalServices_Project_Project"
    Title="مدیریت پروژه ها" %>

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
    <div id="divcontent" style="width: 100%" align="center" dir="rtl">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div dir="rtl" id="DivReport" class="DivErrors" align="right" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]
                </div>

                <TSPControls:CustomASPxRoundPanelMenu ID="ASPxRoundPanel1" runat="server" Width="100%">

                    <PanelCollection>
                        <dxp:PanelContent runat="server">

                            <table>
                                <tbody>
                                    <tr>
                                        <td align="right">
                                            <td>

                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مشاهده" ToolTip="مشاهده"
                                                    ID="btnSummary" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnSummary_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="چاپ" ToolTip="چاپ"
                                                    ID="btnPrint" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open('../../../Print.aspx');
}"></ClientSideEvents>
                                                </TSPControls:CustomAspxButton>
                                            </td>

                                            <td align="right">
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ثبت کار طراحی جدید" ToolTip="ثبت کار طراحی جدید"
                                                    ID="btnSubmitDesign" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnSubmitDesign_Click">
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right">
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مدیریت قراردادهای پروژها" ToolTip="مدیریت قراردادهای پروژها"
                                                    ID="btnContract" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnContract_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td align="right">
                                                <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ثبت قرارداد نظارت جدید" ToolTip="ثبت قرارداد نظارت جدید"
                                                    ID="btnNewObsContract" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnNewObsContract_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                    </tr>
                                </tbody>
                            </table>

                        </dxp:PanelContent>
                    </PanelCollection>

                </TSPControls:CustomASPxRoundPanelMenu>

          <ul runat="server" id="ULAlarm" class="HelpUL">
                <li>جهت ثبت قرارداد نظارت  پروژه ابتدا پروژه مورد نظر را در لیست موجود در این صفحه انتخاب نمایید و سپس گزینه "ثبت قرارداد نظارت جدید" را کلیک نمایید</li>
               <li>جهت ثبت قرارداد نظارت ، باید سمت شما در آن پروژه "ناظر" باشد</li>
                
            </ul>
                <TSPControls:CustomAspxDevGridView ID="GridViewProject" runat="server" DataSourceID="ObjectDataSourceProject"
                    Width="100%"
                    ClientInstanceName="mygrid" OnDetailRowExpandedChanged="GridViewProject_DetailRowExpandedChanged"
                    KeyFieldName="ProjectId" AutoGenerateColumns="False">

                    <Templates>
                        <DetailRow>
                            <TSPControls:CustomAspxDevGridView ID="GridViewProjectRequest" runat="server" DataSourceID="ObjdsProjectRequest"
                                AutoGenerateColumns="False"
                                KeyFieldName="PrjReId" OnBeforePerformDataSelect="GridViewProjectRequest_BeforePerformDataSelect">
                                <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                                <Styles>
                                    <GroupPanel ForeColor="Black">
                                    </GroupPanel>
                                    <Header HorizontalAlign="Center">
                                    </Header>
                                </Styles>
                                <Columns>
                                    
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="RequestDate" Caption="تاریخ درخواست">
                                        <CellStyle Wrap="False" HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="PrjReTypeTittle" Caption="نوع درخواست">
                                        <CellStyle Wrap="False" HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ConfirmState" Caption="وضعیت پروژه">
                                        <CellStyle Wrap="False" HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TaskName" Width="300px"
                                        Caption="وضعیت درخواست">
                                    </dxwgv:GridViewDataTextColumn>                              
                                </Columns>
                                <Settings ShowGroupPanel="True"></Settings>
                                <SettingsDetail IsDetailGrid="True"></SettingsDetail>
                            </TSPControls:CustomAspxDevGridView>
                        </DetailRow>
                    </Templates>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" Width="100px" FieldName="ProjectId"
                            Caption="کد پروژه" Name="ProjectId">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectIngridientType"
                            Caption="سمت" Name="ProjectIngridientType" Width="100px">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectStatus" Caption="وضعیت پروژه"
                            Name="ProjectStatus" Width="150px">
                            <PropertiesTextEdit EnableFocusedStyle="False">
                            </PropertiesTextEdit>
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" Width="150px" FieldName="ProjectName"
                            Caption="نام پروژه" Name="ProjectName">
                            <CellStyle Wrap="True" HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" Width="50px" FieldName="GroupName"
                            Caption="گروه ساختمان" Name="GroupName">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" Width="100px" FieldName="RegisteredNo"
                            Caption="پلاک ثبتی" Name="RegisteredNo">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نام مالک" FieldName="OwnerName" Name="OwnerName"
                            VisibleIndex="1" Width="150px">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>


                        <dxwgv:GridViewDataTextColumn Caption="ناحیه" FieldName="MainRegion" Name="MainRegion"
                            VisibleIndex="1" Width="150px">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="قطعه" FieldName="MainSection" Name="MainSection"
                            VisibleIndex="1" Width="150px">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="شهرداری" FieldName="MunName" Name="MunName"
                            VisibleIndex="1" Width="150px">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="نمایندگی" FieldName="AgentName" Name="AgentName"
                            VisibleIndex="1" Width="150px">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="زیربنا(متر)" FieldName="Foundation" Name="Foundation"
                            VisibleIndex="1" Width="70px">
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="بیشترین تعداد طبقات" FieldName="MaxStageNum"
                            Name="MaxStageNum" VisibleIndex="1" Width="100px">
                            <CellStyle HorizontalAlign="Center" Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataMemoColumn VisibleIndex="6" Width="150px" FieldName="PlansMethodNo"
                            Caption="شماره دستور نقشه" Name="PlansMethodNo">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataMemoColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="7" Width="70px" FieldName="Date" Caption="تاریخ ایجاد"
                            Name="Date">
                            <CellStyle Wrap="True" HorizontalAlign="Center">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="10" Caption=" ">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowHorizontalScrollBar="true"></Settings>
                    <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
                </TSPControls:CustomAspxDevGridView>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="ASPxRoundPanel2" runat="server" Width="100%">

                    <PanelCollection>
                        <dxp:PanelContent runat="server">
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مشاهده" ToolTip="مشاهده"
                                                ID="btnSummary2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnSummary_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="چاپ" ToolTip="چاپ"
                                                ID="btnPrint2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer=false;
	window.open('../../../Print.aspx');
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td align="right">
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ثبت کار طراحی جدید" ToolTip="ثبت کار طراحی جدید"
                                                ID="btnSubmitDesign2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnSubmitDesign_Click">
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td align="right">
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مدیریت قراردادهای پروژها" ToolTip="مدیریت قراردادهای پروژها"
                                                ID="btnContract2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnContract_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td align="right">
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="ثبت قرارداد نظارت جدید" ToolTip="ثبت قرارداد نظارت جدید"
                                                ID="btnNewObsContract2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnNewObsContract_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (mygrid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
	}
}" />
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
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
            AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
            <ProgressTemplate>
                <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                    <img id="IMG1" src="../../../Image/indicator.gif" align="middle" />
                    لطفا صبر نمایید ...
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
        <asp:ObjectDataSource ID="ObjectDataSourceProject" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="SelectProjectForMembers" TypeName="TSP.DataManager.TechnicalServices.ProjectManager">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkId"
            TypeName="TSP.DataManager.WorkFlowTaskManager">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="WorkFlowId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="TaskOrder" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjdsProjectRequest" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="SelectRequestByProject" TypeName="TSP.DataManager.TechnicalServices.ProjectRequestManager">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="-1" Name="ProjectId" SessionField="ProjectId"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>
