<%@ Page Title="مدیریت مغایرت ها" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="ConflictManagment.aspx.cs" Inherits="Employee_Ticketing_ConflictManagment" %>

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

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>

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
                                                     <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {


	if (GridViewConflictManagment.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
{
	GridViewConflictManagment.StartEditRow(GridViewConflictManagment.GetFocusedRowIndex());
}
}
"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/ConfirmAccounting.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                           </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px"
                                            ToolTip="غیر فعال" CausesValidation="False" ID="btnDelete" EnableClientSideAPI="True"
                                            UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewConflictManagment.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else{
     if(confirm('آیا مطمئن به برگرداندن وضعیت این ردیف به حالت در دست بررسی هستید؟')){
                                               
          GridViewConflictManagment.DeleteRow(GridViewConflictManagment.GetFocusedRowIndex());
                                           
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
                                </tr>
                            </tbody>
                        </table>


                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />


            <%--جدول اطلاعات--%>
            <TSPControls:CustomAspxDevGridView ID="GridViewConflictManagment" Width="100%" runat="server"
                DataSourceID="ObjdsConflictManagment" OnRowUpdating="GridViewConflictManagment_RowUpdating"  OnRowDeleting="GridViewConflictManagment_RowDeleting" AutoGenerateColumns="False" KeyFieldName="ConfId" 
                ClientInstanceName="GridViewConflictManagment" EnableViewState="False">
                <ClientSideEvents EndCallback="function(s, e) {
               
	if(GridViewConflictManagment.cpError==1)
	{
		SetTaskOrderError(GridViewConflictManagment.cpMsg);
		GridViewConflictManagment.cpError=2;
	}
}"
                    BeginCallback="function(s, e) {
                   
	GridViewConflictManagment.cpError=2;
}"></ClientSideEvents>

                <Columns>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="ConfId"
                        Name="ConfId">
                    </dxwgv:GridViewDataTextColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="MeId" Caption="کد عضویت">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="FirstName" Caption="نام">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="LastName" Caption="نام خانوادگی">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="MobileNo" Caption="شماره موبایل">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                     <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="RegisterDate" Caption="تاریخ ثبت">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="TypeName" Caption="نوع عدم انطباق">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="TypeCodeName" Caption="حوزه مربوطه">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="SatisfaiedDate" Caption="تاریخ بسته شدن">
                        <EditFormSettings Visible="False" />
                    </dxuc:GridViewDataColumn>
                    <dxuc:GridViewDataColumn VisibleIndex="1" FieldName="SatisfaiedName"
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
                      
               <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            CausesValidation="False" Width="25px" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {


	if (GridViewConflictManagment.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
else
{
	GridViewConflictManagment.StartEditRow(GridViewConflictManagment.GetFocusedRowIndex());
}
}
"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/ConfirmAccounting.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                        </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                            CausesValidation="False" ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                            EnableViewState="False" EnableTheming="False" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewConflictManagment.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else{
     if(confirm('آیا مطمئن به برگرداندن وضعیت این ردیف به حالت در دست بررسی هستید؟')){
                                              
          GridViewConflictManagment.DeleteRow(GridViewConflictManagment.GetFocusedRowIndex());
                                           
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
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <asp:ObjectDataSource ID="ObjdsConflictManagment" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetAllForManagementPage" TypeName="TSP.DataManager.ConflictManager">
                <SelectParameters>
                    <asp:Parameter Name="MeId" Type="Int32" DefaultValue="-1" />
                </SelectParameters>

            </asp:ObjectDataSource>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

