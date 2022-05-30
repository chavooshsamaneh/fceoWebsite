<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MunEmployee.aspx.cs" Inherits="Employee_Management_MunEmployee"
    Title="کارمندان شهرداری" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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

<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="idcontent" style="width: 100%;" align="center">
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



                            <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0"
                                width="100%">
                                <tbody>
                                    <tr>
                                        <td align="right">
                                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                                CausesValidation="False" ID="BtnNew" AutoPostBack="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="BtnNew_Click">
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/new.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                                CausesValidation="False" Width="25px" ID="btnEdit" EnableViewState="False" EnableTheming="False"
                                                                OnClick="btnEdit_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 	{
  		 e.processOnServer=false;
  		 alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}
"></ClientSideEvents>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/edit.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                                ID="btnView" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnViewClient"
                                                                OnClick="btnView_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 	{
  		 e.processOnServer=false;
  		 alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/view.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px"
                                                                ToolTip="غیر فعال" CausesValidation="False" ID="btnDisActive" EnableClientSideAPI="True"
                                                                EnableViewState="False" EnableTheming="False" OnClick="btnInActive_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	 
if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/disactive.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="تعیین سطح دسترسی"
                                                                CausesValidation="False" ID="btnUserRight" Visible="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="btnUserRight_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 	{
  		 e.processOnServer=false;
  		 alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/ChartMember.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازیابی رمز عبور"
                                                                CausesValidation="False" ID="btnReset1" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="btnResetSave_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/ChangePassword.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px"
                                                                ToolTip="چاپ" CausesValidation="False" ID="btnPrint" EnableClientSideAPI="True"
                                                                AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                ClientInstanceName="btnPrintClient">
                                                                <ClientSideEvents Click="function(s,e){ window.open('../../Print.aspx'); }"></ClientSideEvents>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/printers.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <br />
                <TSPControls:CustomAspxDevGridView ID="GridViewEmployee" runat="server" DataSourceID="ObjdsEmployee"
                    Width="100%"  
                    AutoGenerateColumns="False" ClientInstanceName="GridViewEmployeeClient" KeyFieldName="EmpId"
                    EnableViewState="true" OnHtmlDataCellPrepared="GridViewEmployee_HtmlDataCellPrepared"
                    OnAutoFilterCellEditorInitialize="GridViewEmployee_AutoFilterCellEditorInitialize">
                    <ClientSideEvents FocusedRowChanged="function(s, e) {

}"></ClientSideEvents>

                    <Columns>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MunName" Caption="شهرداری"
                            Width="150px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FirstName" Caption="نام">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="LastName" Caption="نام خانوادگی">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="UserName" Caption="نام کاربری">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="FatherName" Caption="نام پدر"
                            Visible="false">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="IdNo" Caption="شماره شناسنامه">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="SSN" Caption="کد ملی" Visible="false">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="CreateDate" Caption="تاریخ ایجاد">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="8" Caption=" " Width="30px" ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="8" FieldName="LoginUserId">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="StatusType" Caption="وضعیت">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>

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
                                                                CausesValidation="False" ID="btnNew1" AutoPostBack="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="BtnNew_Click">
                                                                <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/new.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                                CausesValidation="False" Width="25px" ID="btnEdit1" EnableViewState="False" EnableTheming="False"
                                                                OnClick="btnEdit_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 	{
  		 e.processOnServer=false;
  		 alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}
"></ClientSideEvents>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/edit.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                                ID="btnView2" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnViewClient2"
                                                                OnClick="btnView_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 	{
  		 e.processOnServer=false;
  		 alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/view.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px"
                                                                ToolTip="غیر فعال" CausesValidation="False" ID="btnDisActive2" EnableClientSideAPI="True"
                                                                EnableViewState="False" EnableTheming="False" OnClick="btnInActive_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	 
if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/disactive.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="تعیین سطح دسترسی"
                                                                CausesValidation="False" ID="btnUserRight1" Visible="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="btnUserRight_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 	{
  		 e.processOnServer=false;
  		 alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/ChartMember.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازیابی رمز عبور"
                                                                CausesValidation="False" ID="btnReset" UseSubmitBehavior="False" EnableViewState="False"
                                                                EnableTheming="False" OnClick="btnResetSave_Click">
                                                                <ClientSideEvents Click="function(s, e) {
	if (GridViewEmployeeClient.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }

}"></ClientSideEvents>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/ChangePassword.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                        <td>
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px"
                                                                ToolTip="چاپ" CausesValidation="False" ID="btnPrint2" EnableClientSideAPI="True"
                                                                AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                ClientInstanceName="btnPrintClient2">
                                                                <ClientSideEvents Click="function(s,e){ window.open('../../Print.aspx'); }"></ClientSideEvents>
                                                                <HoverStyle BackColor="#FFE0C0">
                                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                </HoverStyle>
                                                                <Image  Url="~/Images/icons/printers.png">
                                                                </Image>
                                                            </TSPControls:CustomAspxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                  
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ObjectDataSource ID="ObjdsEmployee" runat="server" SelectMethod="SelectMunicipalityEmployee"
                    TypeName="TSP.DataManager.EmployeeManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
            AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img src="../../Image/indicator.gif" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
</asp:Content>
