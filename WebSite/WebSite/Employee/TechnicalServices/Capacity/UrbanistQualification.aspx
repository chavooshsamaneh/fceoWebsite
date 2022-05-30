<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="UrbanistQualification.aspx.cs" Inherits="Employee_TechnicalServices_Capacity_UrbanistQualification" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
	GridViewUrbQul.GetValuesOnCustomCallback(GridViewUrbQul.GetFocusedRowIndex()+ ';'+'Edit',SetTaskOrderError);
	GridViewUrbQul.AddNewRow();
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                              
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px"
                                            ToolTip="غیر فعال" CausesValidation="False" ID="btnDelete" EnableClientSideAPI="True"
                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
	 
if (GridViewUrbQul.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else{
     if(confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟')){
                                              
          GridViewUrbQul.DeleteRow(GridViewUrbQul.GetFocusedRowIndex());
                                           
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
            <TSPControls:CustomAspxDevGridView ID="GridViewUrbQul" Width="100%" runat="server"
                DataSourceID="ObjdsUrbQul"
                AutoGenerateColumns="False" KeyFieldName="UrbQulId" OnRowInserting="GridViewUrbQul_RowInserting"
                OnRowUpdating="GridViewUrbQul_RowUpdating" OnRowDeleting="GridViewUrbQul_RowDeleting" ClientInstanceName="GridViewUrbQul"
                EnableViewState="False">
                <ClientSideEvents EndCallback="function(s, e) {
	if(GridViewUrbQul.cpError==1)
	{
		SetTaskOrderError(GridViewUrbQul.cpMsg);
		GridViewUrbQul.cpError=2;
	}
}"
                    BeginCallback="function(s, e) {
	GridViewUrbQul.cpError=2;
}"></ClientSideEvents>

                <Columns>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="UrbQulId"
                        Name="UrbQulId">
                    </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewDataComboBoxColumn FieldName="Grade" Caption="پایه"
                        Name="Grade" VisibleIndex="0">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <PropertiesComboBox DataSourceID="ObjdGrade" TextField="GrdName"
                            ValueField="GrdId" ValueType="System.String">
                        </PropertiesComboBox>
                        <EditItemTemplate>
                            <TSPControls:CustomAspxComboBox DataSourceID="ObjdGrade" ID="comboGrd" ClientInstanceName="comboGrd" runat="server"
                                ValueField="GrdId" TextField="GrdName" ValueType="System.Int32"
                                Value='<%# Bind("Grade") %>'>
                            </TSPControls:CustomAspxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataComboBoxColumn>
                  
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="QualificationMeter"
                        Caption="صلاحیت(متر)" Name="QualificationMeter">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <PropertiesTextEdit>
                        </PropertiesTextEdit>
                        <EditItemTemplate>
                            <TSPControls:CustomTextBox ID="txQualificationMeter" ClientInstanceName="txtQualificationMeter" runat="server"
                                ValueField="QualificationMeter" TextField="QualificationMeter"
                                Value='<%# Bind("QualificationMeter") %>'>
                            </TSPControls:CustomTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>


                      <dxwgv:GridViewDataComboBoxColumn FieldName="QualificationTypeName" Caption="عنوان"
                        Name="QualificationTypeName" VisibleIndex="0">
                          
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <EditItemTemplate>
                            <TSPControls:CustomAspxComboBox ID="comboQualificationType" ClientInstanceName="comboQualificationType" runat="server"
                                ValueField="QualificationType" TextField="QualificationType" ValueType="System.Int32"
                                Value='<%# Bind("QualificationType") %>'>
                                <Items>
                                    <dxe:ListEditItem Value="1" Text="آماده سازی اراضی"></dxe:ListEditItem>
                                    <dxe:ListEditItem Value="2" Text="تفکیک اراضی"></dxe:ListEditItem>
                                    <dxe:ListEditItem Value="3" Text="انطباق کاربری اراضی"></dxe:ListEditItem>
                                    <dxe:ListEditItem Value="4" Text="انطباق شهری ساختمان"></dxe:ListEditItem>
                                    <dxe:ListEditItem Value="5" Text="مجموع"></dxe:ListEditItem>
                                </Items>
                            </TSPControls:CustomAspxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataComboBoxColumn>
                      <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Count"
                        Caption="مجموع کار" Name="Count">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <PropertiesTextEdit>
                        </PropertiesTextEdit>
                        <EditItemTemplate>
                            <TSPControls:CustomTextBox ID="txtCount" ClientInstanceName="txtCount" runat="server"
                                ValueField="Count" TextField="Count"
                                Value='<%# Bind("Count") %>'>
                            </TSPControls:CustomTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Limit"
                        Caption="حجم کار تا" Name="Limit">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <PropertiesTextEdit>
                        </PropertiesTextEdit>
                        <EditItemTemplate>
                            <TSPControls:CustomTextBox ID="txtLimit" ClientInstanceName="txtLimit" runat="server"
                                ValueField="Limit" TextField="Limit"
                                Value='<%# Bind("Limit") %>'>
                            </TSPControls:CustomTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                        <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="CreateDate"
                        Caption="تاریخ ایجاد" >
                          <EditFormSettings Visible="False"/>
                    </dxuc:GridViewDataColumn>
                       <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="InActiveName"
                        Caption="وضعیت" >
                          <EditFormSettings Visible="False"/>
                    </dxuc:GridViewDataColumn>
                     <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="InActiveDate"
                        Caption="تاریخ غیرفعال" >
                          <EditFormSettings Visible="False"/>
                    </dxuc:GridViewDataColumn>

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
	GridViewUrbQul.AddNewRow();
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                  
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                            CausesValidation="False" ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
		 
if (GridViewUrbQul.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else{
     if(confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟')){
                                              
          GridViewUrbQul.DeleteRow(GridViewUrbQul.GetFocusedRowIndex());
                                           
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
            <asp:ObjectDataSource ID="ObjdsUrbQul" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.TechnicalServices.UrbanistQualificationManager"></asp:ObjectDataSource>
                 <asp:ObjectDataSource ID="ObjdGrade" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="TSP.DataManager.GradeManager"></asp:ObjectDataSource>

                    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

