<%@ Page Title=" مدیریت اعضای دفترگاز " Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="GasOfficeMembersManagement.aspx.cs" Inherits="Employee_Document_GasOfficeMembersManagement" %>

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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script lang="javascript">

        function SetTaskOrderError(result) {

            if (result != null) {

                document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
                document.getElementById("<%=DivReport.ClientID%>").style.display = 'block';
                document.getElementById('<%=LabelWarning.ClientID%>').innerText = result;
            }

        }

        function SetDivVisible() {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'hidden';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'none';
        }
    </script>
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
                                            CausesValidation="False" ID="btnNew" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	GridViewGasOfficeMembersManagment.GetValuesOnCustomCallback(GridViewGasOfficeMembersManagment.GetFocusedRowIndex()+ ';'+'Edit',SetTaskOrderError);
	GridViewGasOfficeMembersManagment.AddNewRow();
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>

                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px"
                                            ToolTip="غیر فعال" CausesValidation="False" ID="btnInActive" EnableClientSideAPI="True"
                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewGasOfficeMembersManagment.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else{
     if(confirm('آیا مطمئن به برگرداندن وضعیت این عضو به حالت غیرفعال هستید؟')){
           hiddenFieldPage.Set('btnInActive',1);  
          hiddenFieldPage.Set('btnOff',0);                       
          GridViewGasOfficeMembersManagment.DeleteRow(GridViewGasOfficeMembersManagment.GetFocusedRowIndex());                                                                      
 }
  }

}"></ClientSideEvents>

                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/cancelResignation.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px"
                                            ToolTip="مرخصی" CausesValidation="False" ID="btnOff" EnableClientSideAPI="True"
                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewGasOfficeMembersManagment.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else{
     if(confirm('آیا مطمئن به برگرداندن وضعیت این عضو به حالت مرخصی هستید؟')){
        hiddenFieldPage.Set('btnOff',1);
          hiddenFieldPage.Set('btnInActive',0);           
          GridViewGasOfficeMembersManagment.DeleteRow(GridViewGasOfficeMembersManagment.GetFocusedRowIndex());
                                              
                                           
 }
  }

}"></ClientSideEvents>

                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/OffTime.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی Excel"
                                            ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnExportExcel_Click">
                                            <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>

                                            <Image Url="~/Images/icons/ExportExcel.png">
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
            <TSPControls:CustomAspxDevGridView ID="GridViewGasOfficeMembersManagment" Width="100%" runat="server"
                DataSourceID="ObjdsGasOfficeMembersManagment" AutoGenerateColumns="False" KeyFieldName="GasMeId"
                ClientInstanceName="GridViewGasOfficeMembersManagment" OnRowDeleting="GridViewGasOfficeMembersManagment_RowDeleting" OnRowInserting="GridViewGasOfficeMembersManagment_RowInserting" EnableViewState="False">
                <ClientSideEvents EndCallback="function(s, e) {
                 
	if(GridViewGasOfficeMembersManagment.cpError==1)
	{
		SetTaskOrderError(GridViewGasOfficeMembersManagment.cpMsg);
		GridViewGasOfficeMembersManagment.cpError=2;
	}
}"
                    BeginCallback="function(s, e) {
                 
	GridViewGasOfficeMembersManagment.cpError=2;
}"></ClientSideEvents>

                <Columns>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="GasMeId"
                        Name="GasMeId">
                    </dxwgv:GridViewDataTextColumn>
                    <dxuc:GridViewDataTextColumn VisibleIndex="1" FieldName="MeId" Caption="کد عضویت">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <PropertiesTextEdit>
                        </PropertiesTextEdit>
                        <EditItemTemplate>
                            <TSPControls:CustomTextBox ID="txtMeId" ClientInstanceName="txtMeId" runat="server"
                                ValueField="MeId" TextField="MeId"
                                Value='<%# Bind("MeId") %>'>
                            </TSPControls:CustomTextBox>
                        </EditItemTemplate>
                    </dxuc:GridViewDataTextColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="FirstName" Caption="نام">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="LastName" Caption="نام خانوادگی">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="MobileNo" Caption="شماره موبایل">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="CreateDate" Caption="تاریخ ثبت">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="StatusName" Caption="وضعیت">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="StatusChangeDate" Caption="تاریخ تغییر وضعیت">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="FileNo"
                        Caption="شماره پروانه">
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
            
                    <dxwgv:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewGasOfficeMembersManagment">
                    </dxwgv:ASPxGridViewExporter>

            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <%--جدول منو--%>
                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	GridViewGasOfficeMembersManagment.GetValuesOnCustomCallback(GridViewGasOfficeMembersManagment.GetFocusedRowIndex()+ ';'+'Edit',SetTaskOrderError);
	GridViewGasOfficeMembersManagment.AddNewRow();
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>

                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                            CausesValidation="False" ID="btnInActive2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewGasOfficeMembersManagment.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else{
          
     if(confirm('آیا مطمئن به برگرداندن وضعیت این عضو به حالت غیرفعال هستید؟')){
        hiddenFieldPage.Set('btnInActive',1);  
          hiddenFieldPage.Set('btnOff',0);
       
          GridViewGasOfficeMembersManagment.DeleteRow(GridViewGasOfficeMembersManagment.GetFocusedRowIndex());                                                                      
 }
  }

}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/cancelResignation.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>


                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px"
                                            ToolTip="مرخصی" CausesValidation="False" ID="btnOff2" EnableClientSideAPI="True"
                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewGasOfficeMembersManagment.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else{
       
     if(confirm('آیا مطمئن به برگرداندن وضعیت این عضو به حالت مرخصی هستید؟')){
          hiddenFieldPage.Set('btnOff',1);
          hiddenFieldPage.Set('btnInActive',0);  
        
          GridViewGasOfficeMembersManagment.DeleteRow(GridViewGasOfficeMembersManagment.GetFocusedRowIndex());                      
 }
  }

}"></ClientSideEvents>

                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/OffTime.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>


                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="خروجی Excel"
                                            ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnExportExcel_Click">
                                            <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>

                                            <Image Url="~/Images/icons/ExportExcel.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <dxhf:ASPxHiddenField ID="hiddenFieldPage" runat="server" ClientInstanceName="hiddenFieldPage">
            </dxhf:ASPxHiddenField>
            <asp:ObjectDataSource ID="ObjdsGasOfficeMembersManagment" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetAllForManagementPage" TypeName="TSP.DataManager.DocGasOfficeMembersManager"></asp:ObjectDataSource>

    
</asp:Content>

