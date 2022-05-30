<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Licence.aspx.cs" Inherits="Members_Amoozesh_Licence"
    Title="گواهینامه های آموزشی" %>

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
              
                    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                        [<a class="closeLink" href="#">بستن</a>]</div>
                              <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                                        <table >
                                                            <tbody>
                                                                <tr>
                                                                    <td >
                                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                                            ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                            OnClick="btnView_Click">
                                                                            <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
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
                        <br />
                            <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" DataSourceID="OdbMadrak"
                                Width="100%"  
                                ClientInstanceName="grid" AutoGenerateColumns="False" KeyFieldName="ID;Type"
                                OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize"
                                OnHtmlDataCellPrepared="CustomAspxDevGridView1_HtmlDataCellPrepared" RightToLeft="True">
                                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" ColumnResizeMode="Control" />
                                <SettingsCookies Enabled="false" />
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" Visible="False"
                                        VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="عنوان مدرک" FieldName="CrsTitle" Name="CrsTitle"
                                        VisibleIndex="1" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="تاریخ ایجاد" FieldName="CreateDate" Name="CreateDate"
                                        VisibleIndex="8">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataComboBoxColumn Caption="نوع مدرک" FieldName="TypeName" VisibleIndex="0">
                                        <PropertiesComboBox ValueType="System.String">
                                            <Items>
                                                <dxe:ListEditItem Text="مدرک دوره" Value="مدرک دوره"></dxe:ListEditItem>
                                                <dxe:ListEditItem Text="مدرک سمینار" Value="مدرک سمینار"></dxe:ListEditItem>
                                                <dxe:ListEditItem Text="ثبت نام دوره" Value="ثبت نام دوره"></dxe:ListEditItem>
                                                <dxe:ListEditItem Text="ثبت نام سمینار" Value="ثبت نام سمینار"></dxe:ListEditItem>
                                            </Items>
                                        </PropertiesComboBox>
                                    </dxwgv:GridViewDataComboBoxColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Type" Visible="False" VisibleIndex="5">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="عنوان دوره" FieldName="CrsTitle" VisibleIndex="4"
                                        Width="250px">
                                    </dxwgv:GridViewDataTextColumn>
                                    
                                    <dxwgv:GridViewDataTextColumn FieldName="PPCode" Name="PPCode" Caption="کد"  VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="StartDate" VisibleIndex="5">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان" FieldName="EndDate" VisibleIndex="6">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="نمره نهایی" FieldName="Mark" VisibleIndex="7">
                                    </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="TestTotalMark" Caption="نمره کل آزمون" VisibleIndex="8">
                        </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="InsId" Visible="False" VisibleIndex="9">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewCommandColumn VisibleIndex="11" Caption=" " Width="30px" ShowClearFilterButton="true">
                                
                                    </dxwgv:GridViewCommandColumn>
                                </Columns>
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
                                                                                ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                                OnClick="btnView_Click">
                                                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}	
}"></ClientSideEvents>
                                                                               
                                                                                <Image  Url="~/Images/icons/view.png">
                                                                                </Image>
                                                                            </TSPControls:CustomAspxButton>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                      </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                        <asp:ObjectDataSource ID="OdbMadrak" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="SearchMadrakByMeId" TypeName="TSP.DataManager.MadrakManager">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                                <asp:Parameter DefaultValue="%" Name="FirstName" Type="String" />
                                <asp:Parameter DefaultValue="%" Name="LastName" Type="String" />
                                <asp:Parameter DefaultValue="-1" Name="CrsId" Type="Int32" />
                                <asp:Parameter DefaultValue="-1" Name="Type" Type="Int16" />
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
