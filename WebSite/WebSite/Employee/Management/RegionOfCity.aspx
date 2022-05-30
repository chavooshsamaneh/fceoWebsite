<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="RegionOfCity.aspx.cs" Inherits="Employee_Management_RegionOfCity"
    Title="مدیریت شهرستان ها" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxnc" %>
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
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">

        function SetTaskOrderError(result) {
            //alert(result);
            if (result != null) {

                document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
                document.getElementById("<%=DivReport.ClientID%>").style.display = 'block';  //='visible';
                document.getElementById('<%=LabelWarning.ClientID%>').innerText = result;
            }

        }

        function SetDivVisible() {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'hidden';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'none';
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>


                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	CityGrid.GetValuesOnCustomCallback(CityGrid.GetFocusedRowIndex()+ ';'+'Edit',SetTaskOrderError);
	CityGrid.AddNewRow();
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {


	if (CityGrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
{
	CityGrid.StartEditRow(CityGrid.GetFocusedRowIndex());
}
}
"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px"
                                            ToolTip="حذف" CausesValidation="False" ID="btnDelete" EnableClientSideAPI="True"
                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnDelete_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 
if (CityGrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/delete.png">
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
            <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" Width="100%" runat="server"
                DataSourceID="ObjdsRegionOfCity"
                AutoGenerateColumns="False" KeyFieldName="ReCitId" OnRowInserting="CustomAspxDevGridView1_RowInserting"
                OnRowUpdating="CustomAspxDevGridView1_RowUpdating" ClientInstanceName="CityGrid"
                EnableViewState="False" OnAutoFilterCellEditorInitialize="CustomAspxDevGridView1_AutoFilterCellEditorInitialize"
                OnCellEditorInitialize="CustomAspxDevGridView1_CellEditorInitialize">
                <ClientSideEvents EndCallback="function(s, e) {
	if(CityGrid.cpError==1)
	{
		SetTaskOrderError(CityGrid.cpMsg);
		CityGrid.cpError=2;
	}
}"
                    BeginCallback="function(s, e) {
	CityGrid.cpError=2;
}"></ClientSideEvents>

                <Columns>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="ReCitId"
                        Name="ReCitId">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="PrId"  Caption="استان"
                        Name="PrId" VisibleIndex="0">
                        <PropertiesComboBox ValueType="System.Int32" TextField="PrName" DataSourceID="ObjectDataSourceDropDown"
                            ValueField="PrId">
                        </PropertiesComboBox>
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <EditItemTemplate>
                            <TSPControls:CustomAspxComboBox ID="comboProvince" ClientInstanceName="comboProvince"  runat="server" DataSourceID="ObjectDataSourceDropDown"
                                
                                ValueField="PrId" TextField="PrName" ValueType="System.Int32"
                                Value='<%# Bind("PrId") %>' >
                            <ClientSideEvents  SelectedIndexChanged="function(s,e){if(comboProvince.GetValue()==16){comboAgent.SetEnabled(true); comboAgent.SetSelectedvalue(-1);  comboAgent.SetValue(''); }
                                else{comboAgent.SetEnabled(false);comboAgent.SetSelectedvalue(-1); comboAgent.SetValue('');} }" />
                            </TSPControls:CustomAspxComboBox>                            
                        </EditItemTemplate>
                        
                    </dxwgv:GridViewDataComboBoxColumn>

                    <dxwgv:GridViewDataComboBoxColumn FieldName="AgentName"  Caption="نمایندگی"
                        Name="AgentId" VisibleIndex="0">
                        <PropertiesComboBox ValueType="System.Int32" TextField="Name" DataSourceID="ObjectDataSourceAgent"
                            ValueField="AgentId">
                        </PropertiesComboBox>
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <EditItemTemplate>
                            <TSPControls:CustomAspxComboBox ID="comboAgent" ClientInstanceName="comboAgent" runat="server" DataSourceID="ObjectDataSourceAgent"
                                
                                ValueField="AgentId" TextField="Name" ValueType="System.Int32"
                                Value='<%# Bind("AgentId") %>'>
                            </TSPControls:CustomAspxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="ReCitName" 
                        Caption="نام شهرستان" Name="ReCitName">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <PropertiesTextEdit >
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorText=""
                                ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="نام را وارد نمایید"></RequiredField>
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="ReCitCode" 
                        Caption="کدشهرستان" Name="ReCitCode">
                        <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                        <PropertiesTextEdit >
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorText=""
                                ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="کد را وارد نمایید"></RequiredField>
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " Visible="true" VisibleIndex="3" Width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <SettingsEditing EditFormColumnCount="1" PopupEditFormModal="True" Mode="PopupEditForm"
                    >
                </SettingsEditing>

            </TSPControls:CustomAspxDevGridView>

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
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	CityGrid.AddNewRow();
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	
	if (CityGrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
{
	CityGrid.StartEditRow(CityGrid.GetFocusedRowIndex());
}
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف"
                                            CausesValidation="False" ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" OnClick="btnDelete_Click">
                                            <ClientSideEvents Click="function(s, e) {
		 
if (CityGrid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/delete.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ObjectDataSource ID="ObjdsRegionOfCity" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.RegionOfCityManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceDropDown" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.ProvinceManager"></asp:ObjectDataSource>

            <asp:ObjectDataSource ID="ObjectDataSourceAgent" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="FindByCurrentProvince" TypeName="TSP.DataManager.AccountingAgentManager">
                <SelectParameters>
                    <asp:Parameter Name="PrId" DefaultValue="16"
                        Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
