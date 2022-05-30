<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="Plans.aspx.cs" Inherits="Employee_TechnicalServices_Project_Plans"
    Title="نقشه های پروژه" %>

<%@ Register Src="../../../UserControl/WFUserControl.ascx" TagName="WFUserControl"
    TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ProjectInfoUserControl.ascx" TagPrefix="TSP" TagName="ProjectInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function ResetReqControl() {
            txtMailTitle.SetText('');
            txtMailDate.SetText('');
            txtMailNo.SetText('');
            cmbPlanType.SetSelectedIndex(-1);
            callbackReset.cpIsValid = 0;
        }

        function CheckValidation() {
            if (IsDateValid(txtMailDate.GetIsValid() && txtMailNo.GetIsValid() && cmbPlanType.GetIsValid())) {
                callbackReset.cpIsValid = 1;
            }
            else {
                callbackReset.cpIsValid = 0;
            }
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
                        <table width="100%">
                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                                ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="BtnNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnEdit_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/edit.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
            
	if (GridViewPlans.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnView_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/view.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
            
	if (GridViewPlans.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف درخواست"
                                                Width="25px" ID="btnDeleteReq" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnDeleteReq_Click">
                                                <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/delete.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازبین نقشه"
                                                Width="25px" ID="btnControler" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnControler_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/TaskDoer.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
            
	if (GridViewPlans.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator9"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="طراحان نقشه"
                                                Width="25px" ID="btnDesigner" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnDesigner_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/TS/TSDesigner.png">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
            
	if (GridViewPlans.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	
}"></ClientSideEvents>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مالی طراحان"
                                                ID="btnDesAcc" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnDesAcc_Click">
                                                <ClientSideEvents Click="function(s, e) {
	 if (GridViewPlans.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/TS/TSImpAcc.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار نقشه"
                                                ID="btnSendNextStep" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
	if (GridViewPlans.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	
	ShowWf();
}
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/reload.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="پیگیری"
                                                ID="btnTracing" AutoPostBack="True" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnTracing_Click">
                                                <ClientSideEvents Click="function(s, e) {
            
	if (GridViewPlans.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/Cheque Status ReChange.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ نامه شهرداری - تایید طراح"
                                                ID="btnDesPermit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {		
	callbackReset.PerformCallback('DesPermitPrint');  
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/TS/TSDesPermitt.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator11"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                                ID="btnback" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnback_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/Back.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت پروژه"
                                                CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="../../../Images/icons/BakToManagment.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <div align="right">
                <TSPControls:CustomAspxMenuHorizontal ID="MainMenu" runat="server" CssClass="ProjectMainMenuHorizontal"
                    OnItemClick="MainMenu_ItemClick">
                    <Items>
                        <dxm:MenuItem Text="مشخصات پروژه" Name="Project" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                            <Items>
                                <dxm:MenuItem Text="اطلاعات پایه" Name="BaseInfo" Selected="true" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="پلاک ثبتی" Name="RegisteredNo" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="دستور نقشه" Name="PlansMethod" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="بلوک" Name="Block" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                                <dxm:MenuItem Text="بیمه" Name="Insurance" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                            </Items>
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="مالک" Name="Owner" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="مالی پروژه" Name="Accounting" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                            <Items>
                                <%--      <dxm:MenuItem Text="مالی مالکان" Name="AccOwner">
                                    </dxm:MenuItem>--%>
                                <dxm:MenuItem Text="مالی طراحان" Name="AccDesigner" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                                </dxm:MenuItem>
                                <%--<dxm:MenuItem Text="مالی ناظران" Name="AccObserver">
                                    </dxm:MenuItem>
                                    <dxm:MenuItem Text="مالی مجریان" Name="AccImp">
                                    </dxm:MenuItem>--%>
                            </Items>

                        </dxm:MenuItem>
                        <dxm:MenuItem Text="نقشه" Name="Plans" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="طراح" Name="Designer" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="ناظر" Name="Observers" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="مجری" Name="Implementer" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="قرارداد" Name="Contract" ItemStyle-CssClass="ProjectMainMenuItemStyle">
                        </dxm:MenuItem>
                        <%--   <dxm:MenuItem Text="زمان بندی" Name="Timing">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="پروانه ساخت" Name="BuildingsLicense">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="اعلام وضعیت" Name="StatusAnnouncement">
                        </dxm:MenuItem>--%>
                    </Items>
                </TSPControls:CustomAspxMenuHorizontal>
            </div>
            <div align="right">
                <ul class="HelpUL">
                    <li>در صورتی که روند تایید و کنترل نقشه مدنظر شما می باشد، طراحان را در این قسمت وارد
                            کنید. در غیر اینصورت از صفحه مدیریت طراحان پروژه استفاده نمائید. </li>
                </ul>
            </div>
            <TSP:ProjectInfo ID="prjInfo" runat="server"></TSP:ProjectInfo>
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewPlans" runat="server" DataSourceID="ObjdsPlans"
                Width="100%"
                KeyFieldName="PlansId" AutoGenerateColumns="False"
                ClientInstanceName="GridViewPlans" OnHtmlRowPrepared="GridViewPlans_HtmlRowPrepared"
                OnCustomCallback="GridViewPlans_CustomCallback">
                <Templates>
                    <DetailRow>
                        <TSPControls:CustomAspxDevGridView ID="GridViewPlanSubRe" runat="server" DataSourceID="ObjdsPlanDesigner"
                            Width="100%"
                            KeyFieldName="PlansId" AutoGenerateColumns="False" OnBeforePerformDataSelect="GridViewPlanSubRe_BeforePerformDataSelect"
                            OnHtmlDataCellPrepared="GridViewPlanSubRe_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="GridViewPlanSubRe_AutoFilterCellEditorInitialize">
                            <Columns>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="OfficeEngOId" Caption="کد دفتر/شرکت طراح">
                                    <CellStyle Wrap="False" HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="DesignerMeId" Caption="کد عضویت طراح">
                                    <CellStyle Wrap="False" HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="DesignerName" Caption="نام و نام خانوادگی"
                                    Width="300px">
                                    <CellStyle Wrap="False">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="DesignerCreateDate" Caption="تاریخ ثبت طراح">
                                    <CellStyle Wrap="False" HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="9" FieldName="Date" Caption="تاریخ ثبت نقشه">
                                    <CellStyle Wrap="False" HorizontalAlign="Center">
                                    </CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <SettingsDetail IsDetailGrid="True"></SettingsDetail>
                        </TSPControls:CustomAspxDevGridView>
                    </DetailRow>
                </Templates>
                <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                <Columns>
                    <dxwgv:GridViewDataComboBoxColumn Caption="مرحله" FieldName="TaskId" Name="WFState"
                        VisibleIndex="0" Width="40px">
                        <DataItemTemplate>
                            <div align="center">
                                <dxe:ASPxImage ID="btnWFState" runat="server" Width="16px" Height="16px" ImageUrl='<%# Bind("WFImageURL") %>'>
                                </dxe:ASPxImage>
                            </div>
                        </DataItemTemplate>
                        <PropertiesComboBox DataSourceID="ObjdsWorkFlowTask" TextField="TaskName" ValueField="TaskId"
                            ValueType="System.String">
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" Visible="False" FieldName="Status"
                        Width="150px" Caption="Status">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="WorkFlowName" Width="200px"
                        Caption="نوع درخواست">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                        <CellStyle Wrap="True">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="No" Width="150px" Caption="شماره نقشه">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="PlanVersion2" Width="50px"
                        Caption="نسخه">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="PlansTypeId" Caption="نوع نقشه" VisibleIndex="3"
                        Width="150px">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <PropertiesComboBox ValueType="System.String" TextField="Title" DataSourceID="ObjdsPlansType"
                            ValueField="PlansTypeId">
                        </PropertiesComboBox>
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="PlanDate" Caption="تاریخ ثبت">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="FileNo" Caption="شماره پرونده پروژه"
                        Name="FileNo">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                        <CellStyle Wrap="False">
                        </CellStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="RegisteredNo" Caption="پلاک ثبتی پروژه"
                        Name="RegisteredNo">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="7" FieldName="LicenseNo" Caption="شماره پروانه ساخت"
                        Name="LicenseNo">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="11" Width="100px" FieldName="ConfirmeStatus"
                        Caption="وضعیت تایید">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="12" FieldName="InActives" Caption="وضعیت">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="12" Visible="False" FieldName="TaskName"
                        Width="150px" Caption="وضعیت درخواست">
                        <HeaderStyle Wrap="True"></HeaderStyle>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="14" Caption=" " ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <Settings ShowHorizontalScrollBar="true"></Settings>
                <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
            </TSPControls:CustomAspxDevGridView>
            <fieldset width="100%">
                <legend>راهنما گردش کار نقشه</legend>
                <ul class="HelpWorkflowTasksImages">
                    <li class="col-sm-4">
                        <ul>
                            <asp:Repeater runat="server" ID="RepeaterWfHep1">
                                <ItemTemplate>
                                    <li>
                                        <asp:Image ID="Image2" Height="16px" Width="16px" runat="server" ImageUrl='<%# Eval("ImageURL") %> ' />
                                        <a><%# Eval("TaskName") %> </a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                            <li></li>
                        </ul>
                    </li>
                    <li class="col-sm-4">
                        <ul>
                            <li class="dropdown-header"></li>
                            <asp:Repeater runat="server" ID="RepeaterWfHep2">
                                <ItemTemplate>
                                    <li>
                                        <asp:Image ID="Image2" Height="16px" Width="16px" runat="server" ImageUrl='<%# Eval("ImageURL") %> ' />
                                        <a><%# Eval("TaskName") %> </a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                            <li></li>
                        </ul>
                    </li>
                    <li class="col-sm-4">
                        <ul>
                            <li class="dropdown-header"></li>
                            <asp:Repeater runat="server" ID="RepeaterWfHep3">
                                <ItemTemplate>
                                    <li>
                                        <asp:Image ID="Image2" Height="16px" Width="16px" runat="server" ImageUrl='<%# Eval("ImageURL") %> ' />
                                        <a><%# Eval("TaskName") %> </a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                            <li></li>
                        </ul>
                    </li>
                </ul>
            </fieldset>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="BtnNew_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnEdit_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
            
	if (GridViewPlans.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                            ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/view.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
            
	if (GridViewPlans.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف درخواست"
                                            Width="25px" ID="btnDeleteReq2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnDeleteReq_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/delete.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator4"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازبین نقشه"
                                            Width="25px" ID="btnControler2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnControler_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/TaskDoer.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
            
	if (GridViewPlans.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator10"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="طراحان نقشه"
                                            Width="25px" ID="btnDesigner2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnDesigner_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/TS/TSDesigner.png">
                                            </Image>
                                            <ClientSideEvents Click="function(s, e) {
            
	if (GridViewPlans.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مالی طراحان"
                                            ID="btnDesAcc2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnDesAcc_Click">
                                            <ClientSideEvents Click="function(s, e) {
	 if (GridViewPlans.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/TS/TSImpAcc.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="گردش کار نقشه"
                                            ID="btnSendToNextStep2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewPlans.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{	

    //TextDesc.SetText('');
	//CallbackPanelWorkFlow.PerformCallback('');	
	//PopupWorkFlow.Show();
	ShowWf();
}
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/reload.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="پیگیری"
                                            ID="btnTracing2" AutoPostBack="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnTracing_Click">
                                            <ClientSideEvents Click="function(s, e) {
            
	if (GridViewPlans.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
 	
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Cheque Status ReChange.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator8"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="چاپ نامه شهرداری - تایید طراح"
                                            ID="btnDesPermit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False">
                                            <ClientSideEvents Click="function(s, e) {		
	callbackReset.PerformCallback('DesPermitPrint');  
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/TS/TSDesPermitt.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator12"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            ID="ASPxButton1" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnback_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت پروژه"
                                            CausesValidation="False" ID="ASPxButton2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBackToManagment_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="../../../Images/icons/BakToManagment.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldPlan" ClientInstanceName="HDPlan">
                        </dxhf:ASPxHiddenField>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <uc1:WFUserControl ID="WFUserControl" runat="server" GridName="GridViewPlans" SessionName="SendBackDataTable_EmpPln"
                OnCallback="CallbackPanelWorkFlow_Callback" />
            <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" TypeName="TSP.DataManager.WorkFlowTaskManager"
                SelectMethod="SelectByWorkCode">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="WorkFlowCode" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsPlans" runat="server" TypeName="TSP.DataManager.TechnicalServices.PlansManager"
                SelectMethod="SelectById" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="PlansId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="ProjectId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsPlanDesigner" runat="server" TypeName="TSP.DataManager.TechnicalServices.Designer_PlansManager"
                SelectMethod="SelectTSDesignerPlansForByPlanId" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:SessionParameter SessionField="PlansId" Type="Int32" DefaultValue="-1" Name="PlansId"></asp:SessionParameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsPlansType" runat="server" TypeName="TSP.DataManager.TechnicalServices.PlansTypeManager"
                SelectMethod="GetData"></asp:ObjectDataSource>
            <dxhf:ASPxHiddenField ID="HiddenFieldPage" ClientInstanceName="HiddenFieldPage"
                runat="server">
            </dxhf:ASPxHiddenField>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
