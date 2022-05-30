<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="TaskDoer.aspx.cs" Inherits="Employee_WorkFlow_TaskDoer"
    Title="مدیریت انجام دهندگان عملیات" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="DivReport" dir="rtl" runat="server" class="DivErrors" style="text-align: right">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnNew_Click" Text=" "
                                            ToolTip="جدید" UseSubmitBehavior="False">
                                          
                                            <image  url="~/Images/icons/new.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                            ToolTip="ویرایش"  UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewTaskDoer.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	
	
}" />
                                           
                                            <image  url="~/Images/icons/edit.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                            ToolTip="مشاهده"  UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewTaskDoer.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                            
                                            <image  url="~/Images/icons/view.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableClientSideAPI="True"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                            ToolTip="حذف" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewTaskDoer.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}
}" />
                                          
                                            <image  url="~/Images/icons/delete.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnBack_Click" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False">
                                         
                                            <image  url="~/Images/icons/Back.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            	<TSPControls:CustomASPxRoundPanel ID="RoundPanelTaskDoer" HeaderText="مشاهده" runat="server">
        <PanelCollection>
            <dxp:PanelContent>

                            <TSPControls:CustomAspxDevGridView ID="GridViewTaskDoer" runat="server" AutoGenerateColumns="False"
                                EnableViewState="False"
                                Width="100%" DataSourceID="ObjdsTaskDoer" KeyFieldName="DoerId" ClientInstanceName="GridViewTaskDoer">
                                <Settings ShowHorizontalScrollBar="true" />

                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="1"
                                        Width="200px">
                                        <CellStyle Wrap="False" HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="2"
                                        Width="200px">
                                        <CellStyle Wrap="False" HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataComboBoxColumn Caption="سمت" FieldName="NcId" VisibleIndex="3"
                                        Width="200px">
                                        <PropertiesComboBox DataSourceID="ObjdsNezamChart" TextField="NcName" ValueField="NcId"
                                            ValueType="System.String">
                                        </PropertiesComboBox>
                                        <CellStyle HorizontalAlign="Center" Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataComboBoxColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="اولویت" FieldName="DoerOrder" VisibleIndex="4"
                                        Width="50px">
                                        <CellStyle Wrap="False" HorizontalAlign="Center">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="5" Width="80px" ShowClearFilterButton="true">
                                    
                                    </dxwgv:GridViewCommandColumn>
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
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew2" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnNew_Click" Text=" "
                                            ToolTip="جدید" UseSubmitBehavior="False">
                                            
                                            <image  url="~/Images/icons/new.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                            ToolTip="ویرایش"  UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewTaskDoer.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	
	
}" />
                                          
                                            <image  url="~/Images/icons/edit.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server" AutoPostBack="False"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                            ToolTip="مشاهده"  UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewTaskDoer.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                           
                                            <image  url="~/Images/icons/view.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableClientSideAPI="True"
                                            EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                            ToolTip="حذف" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewTaskDoer.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}
}" />
                                         
                                            <image  url="~/Images/icons/delete.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server" EnableTheming="False"
                                            EnableViewState="False" OnClick="btnBack_Click" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False">
                                          
                                            <image  url="~/Images/icons/Back.png"  />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField ID="HiddenFieldTaskDoer" runat="server">
            </dxhf:ASPxHiddenField>

            <asp:ObjectDataSource ID="ObjdsTaskDoer" runat="server" SelectMethod="FindByDoerId"
                TypeName="TSP.DataManager.TaskDoerManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="TaskId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="DoerId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="EmpId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="UltId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="NmcId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsNezamChart" runat="server" DeleteMethod="Delete"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="TSP.DataManager.NezamChartManager"></asp:ObjectDataSource>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
        BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>

</asp:Content>
