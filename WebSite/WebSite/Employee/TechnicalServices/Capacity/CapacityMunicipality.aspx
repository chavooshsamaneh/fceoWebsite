<%@ Page Title="مدیریت محدوده ی ظرفیت شهرداری شیراز" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="CapacityMunicipality.aspx.cs" Inherits="Employee_TechnicalServices_Capacity_CapacityMunicipality" %>


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

    <script lang="javascript">

        function SetTaskOrderError(result) {

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
                        <%--جدول منو--%>

                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	GridViewCapMunicipality.GetValuesOnCustomCallback(GridViewCapMunicipality.GetFocusedRowIndex()+ ';'+'Edit',SetTaskOrderError);
	GridViewCapMunicipality.AddNewRow();
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td></td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px"
                                            ToolTip="غیر فعال" CausesValidation="False" ID="btnDelete" EnableClientSideAPI="True"
                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewCapMunicipality.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else{
     if(confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟')){
                                              
          GridViewCapMunicipality.DeleteRow(GridViewCapMunicipality.GetFocusedRowIndex());
                                           
 }
  }

}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/disactive.png">
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
            <%--جدول اطلاعات--%>
            <TSPControls:CustomAspxDevGridView ID="GridViewCapMunicipality" Width="100%" runat="server"
                DataSourceID="ObjdsCapMunicipality" OnRowInserting="GridViewCapMunicipality_RowInserting"
                OnRowUpdating="GridViewCapMunicipality_RowUpdating" OnRowDeleting="GridViewCapMunicipality_RowDeleting" AutoGenerateColumns="False" KeyFieldName="MunCapacityId" ClientInstanceName="GridViewCapMunicipality"
                EnableViewState="False">
                <ClientSideEvents EndCallback="function(s, e) {
                 
	if(GridViewCapMunicipality.cpError==1)
	{
		SetTaskOrderError(GridViewCapMunicipality.cpMsg);
		GridViewCapMunicipality.cpError=2;
	}
}"
                    BeginCallback="function(s, e) {
                 
	GridViewCapMunicipality.cpError=2;
}"></ClientSideEvents>

                <Columns>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="MembeRestrictionId"
                        Name="MembeRestrictionId">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="MjId" Caption="رشته"
                        Name="MjId" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <PropertiesComboBox DataSourceID="ObjdsMajorParents" TextField="MjParentName"
                            ValueField="MjParentId" ValueType="System.String">
                        </PropertiesComboBox>
                        <EditItemTemplate>
                            <TSPControls:CustomAspxComboBox DataSourceID="ObjdsMajorParents" ID="comboMjParentId" ClientInstanceName="comboMjParentId" runat="server"
                                ValueField="MjId" TextField="MjParentName" ValueType="System.Int32"
                                Value='<%# Bind("MjId") %>'>
                            </TSPControls:CustomAspxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="GrdObsId" Caption="پایه نظارت"
                        Name="GrdIdObs" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <PropertiesComboBox DataSourceID="ObjdGrade" TextField="GrdName"
                            ValueField="GrdId" ValueType="System.String">
                        </PropertiesComboBox>
                        <EditItemTemplate>
                            <TSPControls:CustomAspxComboBox DataSourceID="ObjdGrade" ID="comboGrdObsId" ClientInstanceName="comboGrdObsId" runat="server"
                                ValueField="GrdId" TextField="GrdName" ValueType="System.Int32"
                                Value='<%# Bind("GrdObsId") %>'>
                            </TSPControls:CustomAspxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="GrdDesId" Caption="پایه طراحی"
                        Name="GrdIdDes" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <PropertiesComboBox DataSourceID="ObjdGrade" TextField="GrdName"
                            ValueField="GrdId" ValueType="System.String">
                        </PropertiesComboBox>
                        <EditItemTemplate>
                            <TSPControls:CustomAspxComboBox DataSourceID="ObjdGrade" ID="comboGrdDesId" ClientInstanceName="comboGrdDesId" runat="server"
                                ValueField="GrdId" TextField="GrdName" ValueType="System.Int32"
                                Value='<%# Bind("GrdDesId") %>'>
                            </TSPControls:CustomAspxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataComboBoxColumn>

                       <dxwgv:GridViewDataComboBoxColumn FieldName="GrdUrbanismId" Caption="پایه شهرسازی"
                        Name="GrdUrbanismId" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <PropertiesComboBox DataSourceID="ObjdGrade" TextField="GrdName"
                            ValueField="GrdId" ValueType="System.String">
                        </PropertiesComboBox>
                        <EditItemTemplate>
                            <TSPControls:CustomAspxComboBox DataSourceID="ObjdGrade" ID="comboGrdUrbanismId" ClientInstanceName="comboGrdUrbanismId" runat="server"
                                ValueField="GrdId" TextField="GrdName" ValueType="System.Int32"
                                Value='<%# Bind("GrdUrbanismId") %>'>
                            </TSPControls:CustomAspxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataComboBoxColumn>


                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MaxDesCapacity"
                        Caption="حداکثر ظرفیت طراحی" Name="MaxDesCapacity">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <PropertiesTextEdit>
                        </PropertiesTextEdit>
                        <EditItemTemplate>
                            <TSPControls:CustomTextBox ID="txMaxDesCapacity" ClientInstanceName="txtMaxDesCapacity" runat="server"
                                ValueField="MaxDesCapacity" TextField="MaxDesCapacity"
                                Value='<%# Bind("MaxDesCapacity") %>'>
                            </TSPControls:CustomTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MaxObsCapacity"
                        Caption="حداکثر ظرفیت نظارت" Name="MaxObsCapacity">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>

                        <EditItemTemplate>
                            <TSPControls:CustomTextBox ID="txtMaxObsCapacity" ClientInstanceName="txtMaxObsCapacity" runat="server"
                                ValueField="MaxObsCapacity" TextField="MaxObsCapacity"
                                Value='<%# Bind("MaxObsCapacity") %>'>
                            </TSPControls:CustomTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>

                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MaxUrbenismEntebaghShahri"
                        Caption="حداکثر ظرفیت انطباق شهری شهرسازی" Name="MaxUrbenismEntebaghShahri">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>

                        <EditItemTemplate>
                            <TSPControls:CustomTextBox ID="txtMaxUrbenismEntebaghShahri" ClientInstanceName="txtMaxUrbenismEntebaghShahri" runat="server"
                                ValueField="MaxUrbenismEntebaghShahri" TextField="MaxUrbenismEntebaghShahri"
                                Value='<%# Bind("MaxUrbenismEntebaghShahri") %>'>
                            </TSPControls:CustomTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MaxUrbenismTarhShahrsazi"
                        Caption="حداکثر ظرفیت طرح شهرسازی" Name="MaxUrbenismTarhShahrsazi">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>

                        <EditItemTemplate>
                            <TSPControls:CustomTextBox ID="txtMaxUrbenismTarhShahrsazi" ClientInstanceName="txtMaxUrbenismTarhShahrsazi" runat="server"
                                ValueField="MaxUrbenismTarhShahrsazi" TextField="MaxUrbenismTarhShahrsazi"
                                Value='<%# Bind("MaxUrbenismTarhShahrsazi") %>'>
                            </TSPControls:CustomTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>

                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="ActiveState"
                        Caption="وضعیت">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Description"
                        Caption="توضیحات" Name="Description">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <PropertiesTextEdit>
                        </PropertiesTextEdit>
                        <EditItemTemplate>
                            <TSPControls:CustomTextBox ID="txtDescription" ClientInstanceName="txtDescription" runat="server"
                                ValueField="Description" TextField="Description"
                                Value='<%# Bind("Description") %>'>
                            </TSPControls:CustomTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewCommandColumn Caption=" " Visible="true" VisibleIndex="3" Width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <SettingsEditing EditFormColumnCount="1" PopupEditFormModal="True" Mode="PopupEditForm">
                </SettingsEditing>

            </TSPControls:CustomAspxDevGridView>

            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <%--جدول منو--%>
                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	GridViewCapMunicipality.AddNewRow();
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td></td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                            CausesValidation="False" ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewCapMunicipality.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else{
     if(confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟')){
                                              
          GridViewCapMunicipality.DeleteRow(GridViewCapMunicipality.GetFocusedRowIndex());
                                           
 }
  }

}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/disactive.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ObjectDataSource ID="ObjdsCapMunicipality" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.TechnicalServices.CapacityInMunicipalityManager"></asp:ObjectDataSource>

            <asp:ObjectDataSource ID="ObjdsMajorParents" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.MajorParentsManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdGrade" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

