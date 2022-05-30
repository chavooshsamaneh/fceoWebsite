<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="TestCondition.aspx.cs" Inherits="Employee_Document_TestCondition"
    Title="مدیریت شرایط آزمون" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="center" style="width: 100%" dir="ltr">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
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
                                                ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="BtnNew_Click" CausesValidation="false">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td style="width: 30px">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnEdit_Click" CausesValidation="false">
                                                <ClientSideEvents Click="function(s, e) {
			if (GridViewTestCondition.GetFocusedRowIndex()&lt;0)
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
                                        <td style="width: 30px">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="مشاهده"
                                                Width="25px" ID="btnView" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnView_Click" CausesValidation="false">
                                                <ClientSideEvents Click="function(s, e) {
			if (GridViewTestCondition.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/view.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیرفعال"
                                                ID="btnDelete" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnDelete_Click">
                                                <ClientSideEvents Click="function(s, e) {
		if (GridViewTestCondition.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}
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
                                                ID="btnActive" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnActive_Click">
                                                <ClientSideEvents Click="function(s, e) {
		if (GridViewTestCondition.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/Active.png">
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
                <TSPControls:CustomAspxDevGridView ID="GridViewTestCondition" runat="server" DataSourceID="ObjdsTestCondition"
                    OnBeforePerformDataSelect="GridViewTestCondition_BeforePerformDataSelect" KeyFieldName="TCondId"
                    AutoGenerateColumns="False" ClientInstanceName="GridViewTestCondition" OnAutoFilterCellEditorInitialize="GridViewTestCondition_AutoFilterCellEditorInitialize"
                    OnHtmlDataCellPrepared="GridViewTestCondition_HtmlDataCellPrepared" Width="100%"
                    RightToLeft="True">
                    <Templates>
                        <DetailRow>
                            <TSPControls:CustomAspxDevGridView ID="GridViewTCondDetail" runat="server" DataSourceID="ObjdsTConditionDetail"
                                KeyFieldName="TCondDId" OnBeforePerformDataSelect="GridViewTCondDetail_BeforePerformDataSelect"
                                Width="100%" OnAutoFilterCellEditorInitialize="GridViewTCondDetail_AutoFilterCellEditorInitialize"
                                OnHtmlDataCellPrepared="GridViewTCondDetail_HtmlDataCellPrepared">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TTypeName" Caption="زمینه آزمون">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="GrdName" Caption="پایه">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="AcceptGrade" Caption="نمره قبولی">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" Width="1px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="DeductionGrade" Caption="حد کسر نمره">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="TestDate" Caption="تاریخ آزمون">
                                        <CellStyle Wrap="False">
                                        </CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataCheckColumn FieldName="NeedFileUpload" Caption="اجباری بودن بارگذاری تصویر دوره"
                                        Name="NeedFileUpload" VisibleIndex="0">
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dxwgv:GridViewDataCheckColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView>
                        </DetailRow>
                    </Templates>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Title" Caption="عنوان آزمون"
                            Name="Title" Width="300px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataComboBoxColumn FieldName="MjId" Caption="رشته" VisibleIndex="0"
                            Width="150px">
                            <PropertiesComboBox ValueType="System.String" TextField="MjName" DataSourceID="ObjdsMajor"
                                ValueField="MjId">
                            </PropertiesComboBox>
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="TestValidDate" Caption="اعتبار آزمون"
                            Name="TestValidDate" Width="80px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="ExpireDate" Caption="مهلت تکمیل سوابق کاری"
                            Name="ExpireDate" Width="80px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                            <HeaderStyle Wrap="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="InActives" Caption="وضعیت"
                            Width="60px">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn VisibleIndex="4" Caption=" " Width="30px" ShowClearFilterButton="true">
                        </dxwgv:GridViewCommandColumn>

                    </Columns>
                    <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
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
                                                ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="BtnNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ویرایش"
                                                Width="25px" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnEdit_Click">
                                                <ClientSideEvents Click="function(s, e) {
			if (GridViewTestCondition.GetFocusedRowIndex()&lt;0)
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
                                                Width="25px" ID="btnView2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnView_Click">
                                                <ClientSideEvents Click="function(s, e) {
			if (GridViewTestCondition.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/view.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                            </TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="غیرفعال"
                                                ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnDelete_Click">
                                                <ClientSideEvents Click="function(s, e) {
		if (GridViewTestCondition.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}
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
		if (GridViewTestCondition.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/Active.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ObjectDataSource ID="ObjdsMajor" runat="server" SelectMethod="FindMjParents"
                    TypeName="TSP.DataManager.MajorManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjdsTestCondition" runat="server" SelectMethod="GetData"
                    TypeName="TSP.DataManager.DocTestConditionManager" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjdsTConditionDetail" runat="server" SelectMethod="SelectByTestCondition"
                    TypeName="TSP.DataManager.DocTestConditionDetailManager" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:SessionParameter SessionField="TCondId" Type="Int32" Name="TCondId"></asp:SessionParameter>
                    </SelectParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
