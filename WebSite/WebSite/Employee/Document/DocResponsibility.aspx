<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="DocResponsibility.aspx.cs" Inherits="Employee_Document_DocResponsibility"
    Title="مدیریت پایه - صلاحیت های پروانه" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../UserControl/MemberInfoUserControl.ascx" TagName="MemberInfoUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function SetError(result) {
            //alert(result);
            if (result != null) {

                document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
                document.getElementById("<%=DivReport.ClientID%>").style.display = 'block';
                document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = result;
            }

        }

        function SetDivVisible() {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'hidden';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'none';
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#"><span style="color: #000000"></span>بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="جدید"
                                            ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="BtnNew_Click">
                                            <ClientSideEvents Click="function(s, e) {	
	//GridViewMeFiledetail.AddNewRow();
}"></ClientSideEvents>

                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="width: 30px">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                            Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnEdit_Click">
                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewMeFiledetail.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
//else
//{
//GridViewMeFiledetail.StartEditRow(GridViewMeFiledetail.GetFocusedRowIndex());
//}
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="width: 30px">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                            Width="25px" ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnView_Click">
                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewMeFiledetail.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
//else
//{
//GridViewMeFiledetail.StartEditRow(GridViewMeFiledetail.GetFocusedRowIndex());
//}
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td style="width: 30px">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                            Width="25px" ID="btnInActive" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnInActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewMeFiledetail.GetFocusedRowIndex()&lt;0)
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
                                            <Image Url="~/Images/icons/disactive.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="فعال"
                                                            ID="btnActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                            EnableTheming="False" OnClick="btnActive_Click">
                                                            <ClientSideEvents Click="function(s, e) {
if (GridViewMeFiledetail.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image Url="~/Images/icons/active.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت پروانه"
                                            CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBackToManagment_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="../../Images/icons/BakToManagment.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <TSPControls:CustomAspxMenuHorizontal ID="MenuMemberFile" runat="server"
                OnItemClick="MenuMemberFile_ItemClick">
                <Items>
                    <dxm:MenuItem Text="مشخصات پروانه" Name="Major">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="تاییدکنندگان سابقه کار" Name="JobConfirmition">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="آزمون ها" Name="Exam">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Periods" Text="دوره های آموزشی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="MeDetail" Text="پایه-صلاحیت" Selected="True">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="مدارک پیوست" Name="Attachment">
                    </dxm:MenuItem>
                    <dxm:MenuItem Text="ظرفیت اشتغال" Name="Capacity">
                    </dxm:MenuItem>

                </Items>

            </TSPControls:CustomAspxMenuHorizontal>

            <table>
                <tr>
                    <td style="width: 100%" align="center">
                        <br />
                        <dxe:ASPxLabel runat="server" Font-Bold="true" Text="دستورالعمل درخواست" ID="txtRequestComment"
                            ForeColor="DarkRed" Visible="false">
                        </dxe:ASPxLabel>
                        <br />
                    </td>
            </table>
            <uc2:MemberInfoUserControl ID="MemberInfoUserControl1" runat="server" />
            <br />
            <TSPControls:CustomAspxDevGridView runat="server" EnableViewState="False"
                ID="GridViewMeFiledetail" DataSourceID="ObjdsMemberFileDetail" KeyFieldName="MfdId"
                AutoGenerateColumns="False" ClientInstanceName="GridViewMeFiledetail"
                OnHtmlRowPrepared="GridViewMeFiledetail_HtmlRowPrepared" Width="100%" OnAutoFilterCellEditorInitialize="GridViewMeFiledetail_AutoFilterCellEditorInitialize"
                OnHtmlDataCellPrepared="GridViewMeFiledetail_HtmlDataCellPrepared">
                <SettingsEditing EditFormColumnCount="1" PopupEditFormModal="True" Mode="PopupEditForm">
                </SettingsEditing>

                <Columns>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MjNameAndStatus" Width="250px" Caption="رشته">
                        <CellStyle HorizontalAlign="Center" Wrap="False">
                        </CellStyle>
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="MjCode" Width="250px" Caption="کد رشته">
                        <CellStyle HorizontalAlign="Center" Wrap="False">
                        </CellStyle>
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="GMRName" Width="100px"
                        Caption="پایه - صلاحیت">
                        <CellStyle HorizontalAlign="Center" Wrap="False">
                        </CellStyle>
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="ActTypeName" Width="200px"
                        Caption="شیوه اخذ">
                        <CellStyle HorizontalAlign="Center" Wrap="False">
                        </CellStyle>
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Date" Name="Date" Width="120px"
                        Caption="تاریخ اخذ">
                        <CellStyle HorizontalAlign="Center" Wrap="False">
                        </CellStyle>
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="ResRangeName" Name="ResRangeName"
                        Width="150px" Caption="محدوده صلاحیت">
                        <CellStyle HorizontalAlign="Center" Wrap="False">
                        </CellStyle>
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="RespDateDiff" Caption="تعداد سال تا امروز"
                        Name="JobDateDiff">
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="CanUpgrade" Caption="تعداد سال مورد نیاز ارتقاء"
                        Name="JobDateDiff">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="InActives" Width="80px"
                        Caption="وضعیت">
                        <CellStyle HorizontalAlign="Center" Wrap="False">
                        </CellStyle>
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="3" Caption=" " Width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <ClientSideEvents EndCallback="function(s, e) {
	//if(GridViewMeFiledetail.cpError==1)
	//{
	//	SetError(GridViewMeFiledetail.cpErrorMsg);
	//}

}" />
            </TSPControls:CustomAspxDevGridView>
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
                                            ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="BtnNew_Click">
                                            <ClientSideEvents Click="function(s, e) {
		//GridViewMeFiledetail.AddNewRow();
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
                                            Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnEdit_Click">
                                            <ClientSideEvents Click="function(s, e) {
				if (GridViewMeFiledetail.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                            Width="25px" ID="btnView2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnView_Click">
                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewMeFiledetail.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
//else
//{
//GridViewMeFiledetail.StartEditRow(GridViewMeFiledetail.GetFocusedRowIndex());
//}
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیر فعال"
                                            Width="25px" ID="btnInActive2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnInActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewMeFiledetail.GetFocusedRowIndex()&lt;0)
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
                                            <Image Url="~/Images/icons/disactive.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>

                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="فعال"
                                            ID="btnActive2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
if (GridViewMeFiledetail.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/active.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت"
                                            ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnBack_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="~/Images/icons/Back.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td width="10px" align="center">
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت پروانه"
                                            CausesValidation="False" ID="btnBackToManagment2" UseSubmitBehavior="False" EnableViewState="False"
                                            EnableTheming="False" OnClick="btnBackToManagment_Click">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image Url="../../Images/icons/BakToManagment.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldMeFileDetail">
            </dxhf:ASPxHiddenField>

            <asp:ObjectDataSource ID="ObjdsMemberFileDetail" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="spSelectDocMemberFileDetailForManagementPage" TypeName="TSP.DataManager.DocMemberFileDetailManager">
                <SelectParameters>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="MFId"></asp:Parameter>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="MeId"></asp:Parameter>
                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="InActive"></asp:Parameter>
                </SelectParameters>
            </asp:ObjectDataSource>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
