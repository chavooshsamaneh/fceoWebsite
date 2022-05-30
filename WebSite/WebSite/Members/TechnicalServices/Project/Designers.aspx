<%@ Page Title="طراحان پروژه" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="Designers.aspx.cs" Inherits="Members_TechnicalServices_Project_Designers" %>

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
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]</div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table width="100%">
                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مشاهده"  ToolTip="مشاهده"
                                                ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnView_Click">
                                                <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="بازگشت"  ToolTip="بازگشت"
                                                ID="btnback" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnback_Click">
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <div align="right" style="width: 100%">
                <TSPControls:CustomAspxMenuHorizontal ID="MainMenu" runat="server" 
                     SeparatorWidth="1px" SeparatorHeight="100%" SeparatorColor="#A5A6A8"
                    OnItemClick="MainMenu_ItemClick" ItemSpacing="0px" AutoSeparators="RootOnly"
                     Font-Size="11px" RightToLeft="True">
                    <Items>
                        <dxm:MenuItem Name="Project" Text="مشخصات پروژه">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="مالک" Name="Owner">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="مالی پروژه" Name="Accounting">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="نقشه" Name="Plans">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="طراح" Name="Designer" Selected="true">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="ناظر" Name="Observers">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="مجری" Name="Implementer">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="قرارداد" Name="Contract">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="زمان بندی" Name="Timing">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="پروانه ساخت" Name="BuildingsLicense">
                        </dxm:MenuItem>
                    </Items>
                </TSPControls:CustomAspxMenuHorizontal>
            </div>
            <br />
            <TSP:ProjectInfo ID="prjInfo" runat="server"></TSP:ProjectInfo>
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewDesigner" runat="server" DataSourceID="ObjdsDesigner"
                Width="100%"  
                KeyFieldName="DesignerPlansId" AutoGenerateColumns="False" ClientInstanceName="grid">
                <Columns>
                    <dxwgv:GridViewDataCheckColumn VisibleIndex="0" FieldName="IsMaster" Caption="نماینده اصلی"
                        Name="IsMaster">
                    </dxwgv:GridViewDataCheckColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="OfficeEngOId" Caption="کد طراح">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" Width="170px" FieldName="DesignerName" Caption="نام">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="No" Width="150px" Caption="شماره نقشه">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="PlansTypeId" Caption="نوع نقشه" VisibleIndex="4"
                        Width="120px">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                        <PropertiesComboBox ValueType="System.String" TextField="Title" DataSourceID="ObjdsPlansType"
                            ValueField="PlansTypeId">
                        </PropertiesComboBox>
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="MeType" Caption="نوع طراح">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="Date" Caption="تاریخ">
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="CapacityDecrement" Caption="متراژ کسر ظرفیت">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="8" FieldName="Wage" Caption="متراژ دستمزد">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="9" Caption=" " ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <Settings ShowHorizontalScrollBar="true"></Settings>
            </TSPControls:CustomAspxDevGridView>
            <asp:ObjectDataSource ID="ObjdsDesigner" runat="server" TypeName="TSP.DataManager.TechnicalServices.Designer_PlansManager"
                SelectMethod="FindByRequest" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="ProjectId"></asp:Parameter>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="PrjReId"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsPlansType" runat="server" TypeName="TSP.DataManager.TechnicalServices.PlansTypeManager"
                SelectMethod="GetData"></asp:ObjectDataSource>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldDesPlans">
            </dxhf:ASPxHiddenField>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="مشاهده"  ToolTip="مشاهده"
                                            ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">
                                            <ClientSideEvents Click="function(s, e) {
            
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" runat="server" Text="بازگشت"  ToolTip="بازگشت"
                                            ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnback_Click">
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
                   <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
            AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
            <ProgressTemplate>
                <div class="modalPopup" style="font-family: Tahoma; font-size: 9pt">
                    <img id="IMG1" src="../../../Image/indicator.gif" align="middle" />
                    لطفا صبر نمایید ...</div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
