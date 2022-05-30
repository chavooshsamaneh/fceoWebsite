<%@ Page Title="مدیریت اعضای محروم از نظارت" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ObserverRestriction.aspx.cs" Inherits="Employee_TechnicalServices_Capacity_ObserverRestriction" %>


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
	GridViewObsRestriction.GetValuesOnCustomCallback(GridViewObsRestriction.GetFocusedRowIndex()+ ';'+'Edit',SetTaskOrderError);
	GridViewObsRestriction.AddNewRow();
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>                   
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px"
                                            ToolTip="غیر فعال" CausesValidation="False" ID="btnDelete" EnableClientSideAPI="True"
                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
	 
if (GridViewObsRestriction.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else{
     if(confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟')){
                                              
          GridViewObsRestriction.DeleteRow(GridViewObsRestriction.GetFocusedRowIndex());
                                           
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
            <TSPControls:CustomAspxDevGridView ID="GridViewObsRestriction" Width="100%" runat="server"
                DataSourceID="ObjdsObsRestriction"
                AutoGenerateColumns="False" KeyFieldName="MembeRestrictionId" OnRowInserting="GridViewObsRestriction_RowInserting"
                 OnRowDeleting="GridViewObsRestriction_RowDeleting" ClientInstanceName="GridViewObsRestriction"
                EnableViewState="False">
                <ClientSideEvents EndCallback="function(s, e) {
	if(GridViewObsRestriction.cpError==1)
	{
		SetTaskOrderError(GridViewObsRestriction.cpMsg);
		GridViewObsRestriction.cpError=2;
	}
}"
                    BeginCallback="function(s, e) {
	GridViewObsRestriction.cpError=2;
}"></ClientSideEvents>

                <Columns>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="MembeRestrictionId"
                        Name="MembeRestrictionId">
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataComboBoxColumn FieldName="TypeName" Caption="نوع محدودیت"
                        Name="TypeName" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <EditItemTemplate>
                            <TSPControls:CustomAspxComboBox ID="comboAgent" ClientInstanceName="comboAgent" runat="server"
                                ValueField="TypeName" TextField="TypeName" ValueType="System.Int32"
                                Value='<%# Bind("Type") %>'>
                                <Items>
                                    <dxe:ListEditItem Value="1" Text="انبوه سازان"></dxe:ListEditItem>
                                    <%--     <dxe:ListEditItem Value="2" Text="مجری حقیقی"></dxe:ListEditItem>   --%>
                                    <dxe:ListEditItem Value="3" Text="پیمانکار"></dxe:ListEditItem>
                                    <%--    <dxe:ListEditItem Value="4" Text="دفتر طراحی"></dxe:ListEditItem>
                                    <dxe:ListEditItem Value="5" Text="بازرسی گاز"></dxe:ListEditItem>--%>
                                    <dxe:ListEditItem Value="6" Text="دارای حکم شورای انتظامی"></dxe:ListEditItem>
                                    <%--   <dxe:ListEditItem Value="7" Text="شرکت های مجری"></dxe:ListEditItem>--%>
                                    <dxe:ListEditItem Value="8" Text="شرکت های آزمایشگاهی"></dxe:ListEditItem>
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MeId"
                        Caption="کد عضویت" Name="MeId">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <PropertiesTextEdit>
                        </PropertiesTextEdit>
                        <EditItemTemplate>
                            <TSPControls:CustomTextBox ID="txMeId" ClientInstanceName="txtMeId" runat="server"
                                ValueField="MeId" TextField="MeId"
                                Value='<%# Bind("MeId") %>'>
                            </TSPControls:CustomTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MeFullName"
                        Caption="نام و نام خانوادگی" Name="MeFullName" >
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <EditFormSettings Visible="False" />
                        <EditItemTemplate>
                            <TSPControls:CustomTextBox ID="txtMeFullName" ClientInstanceName="txtMeFullName" runat="server" ClientEnabled="false"
                                ValueField="MeFullName" TextField="MeFullName"
                                Value='<%# Bind("MeFullName") %>'>
                            </TSPControls:CustomTextBox>
                        </EditItemTemplate> 
                    </dxwgv:GridViewDataTextColumn>
                      <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="InActiveName"
                        Caption="وضعیت" >
                          <EditFormSettings Visible="False"/>
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



                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                            cellpadding="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            CausesValidation="False" ID="BtnNew2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	GridViewObsRestriction.AddNewRow();
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>          
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                            CausesValidation="False" ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
		 
if (GridViewObsRestriction.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else{
     if(confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟')){
                                              
          GridViewObsRestriction.DeleteRow(GridViewObsRestriction.GetFocusedRowIndex());
                                           
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
            <asp:ObjectDataSource ID="ObjdsObsRestriction" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.TechnicalServices.MemberRestrictionForObserverWorkRequestManager"></asp:ObjectDataSource>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

