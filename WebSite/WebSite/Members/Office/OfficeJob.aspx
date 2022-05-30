<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="OfficeJob.aspx.cs" Inherits="Members_Office_OfficeJob" Title="سوابق کاری" %>

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

                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]</div>
                 <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                    <table >
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده" ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnView_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>

                                                                        <hoverstyle backcolor="#FFE0C0">
<Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
</hoverstyle>

                                                                        <image url="~/Images/icons/view.png"></image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                                        <hoverstyle backcolor="#FFE0C0">
<Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
</hoverstyle>

                                                                        <image url="~/Images/icons/Back.png"></image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                               </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                    <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server" AutoSeparators="RootOnly" ItemSpacing="0px" OnItemClick="ASPxMenu1_ItemClick" SeparatorColor="#A5A6A8" SeparatorHeight="100%" SeparatorWidth="1px">
                      
                        <Items>
                            <dxm:MenuItem Name="Office" Text="مشخصات شرکت">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Member" Text="اعضای شرکت">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Job" Text="سوابق کاری" Selected="true">
                            </dxm:MenuItem>
                        </Items>
                        
                    </TSPControls:CustomAspxMenuHorizontal>
                
                <br />
                <div style="width: 100%; text-align: right">
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="شرکت :">
                    </dxe:ASPxLabel>
                    <dxe:ASPxLabel ID="lblOfName" runat="server">
                    </dxe:ASPxLabel>
                </div>
                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" Width="100%" ClientInstanceName="jgrid" AutoGenerateColumns="False" KeyFieldName="JhId" DataSourceID="ObjectDataSource1"
                    OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize" OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared">
                    <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True" ColumnResizeMode="Control"></SettingsBehavior>

                   
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="JhId" Name="JhId"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="ProjectName" Caption="نام پروژه" Name="ProjectName" Width="200px"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="TypeName" Caption="نوع پروژه" Name="TypeName"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="ProjectPosition" Caption="سمت" Name="ProjectPosition"></dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="StartCorporateDate" Caption="تاریخ شروع همکاری" Name="StartCorporateDate">
                            <CellStyle Wrap="False"></CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" Visible="False" FieldName="EndCorporateDate" Caption="تاریخ پایان همکاری" Name="EndCorporateDate"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="Area" Caption="زیربنا" Name="Area"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="CorName" Caption="نحوه مشارکت" Name="CorName" Width="150px"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="CreateDate" Caption="تاریخ ایجاد" Name="CreateDate">
                            <CellStyle Wrap="False"></CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="InActiveName" Caption="وضعیت"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="CitName" Caption="شهر"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="TableId" Visible="False" VisibleIndex="8">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="10" Width="30px" ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                </TSPControls:CustomAspxDevGridView>
                <br />
                	<TSPControls:CustomASPxRoundPanel ID="ASPxRoundPanel3"  HeaderText="مجموع مطلوبیت کارهای اجرا شده" runat="server">
        <PanelCollection>
            <dxp:PanelContent>
 
                  
                                <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView2" runat="server" AutoGenerateColumns="False"
                                    DataSourceID="ObjectDataSourceFactorValues"
                                    KeyFieldName="OfdId" Width="450px">
                                   
                                   
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn Caption="امتیاز" FieldName="SumValue" VisibleIndex="0">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نوع مطلوبیت کار" FieldName="Name" VisibleIndex="1">
                                        </dxwgv:GridViewDataTextColumn>
                                    </Columns>
                                </TSPControls:CustomAspxDevGridView>
                               </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                <br />
              <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                    <table>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده" ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnView_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (jgrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>

                                                                        <hoverstyle backcolor="#FFE0C0">
<Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
</hoverstyle>

                                                                        <image url="~/Images/icons/view.png"></image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت" CausesValidation="False" ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                                        <hoverstyle backcolor="#FFE0C0">
<Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
</hoverstyle>

                                                                        <image url="~/Images/icons/Back.png"></image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                              </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
       
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
            SelectMethod="FindByMeRequest" TypeName="TSP.DataManager.ProjectJobHistoryManager" OldValuesParameterFormatString="original_{0}">
            <FilterParameters>
                <asp:Parameter Name="newparameter" />
            </FilterParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="MReId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="IsConfirm" Type="Int16" />
                <asp:Parameter DefaultValue="1" Name="Type" Type="Int16" />
                <asp:Parameter DefaultValue="-1" Name="TableType" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceFactorValues" runat="server" SelectMethod="SelectDocOffJobHistoryQualityValues"
            TypeName="TSP.DataManager.DocOffOfficeFactorDocumentsManager">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="OfficeId" runat="server" Visible="False" />
        <asp:HiddenField ID="PgMode" runat="server" />
        <asp:HiddenField ID="OfficeRequest" runat="server" Visible="False" />
        <asp:ObjectDataSource ID="ObjdsJudgment" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="SelectByJobHistory" TypeName="TSP.DataManager.TrainingJudgmentManager">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="-1" Name="PkId" SessionField="PKId" Type="Int32" />
                <asp:Parameter DefaultValue="-1" Name="JudgeId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
</asp:Content>





