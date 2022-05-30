<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="TeacherJobHistory.aspx.cs" Inherits="Employee_Amoozesh_TeacherJobHistory"
    Title="سابقه کاری استاد" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true"
                    dir="rtl">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table >
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                ID="btnNew" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnNew_Click" UseSubmitBehavior="False">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/new.png"></Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                Width="25px" ID="btnEdit" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                                <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacherJob.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	
	
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/edit.png"></Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                Width="25px" ID="btnView" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnView_Click" UseSubmitBehavior="False">
                                                <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacherJob.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/view.png"></Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnJudge" runat="server" EnableClientSideAPI="True" 
                                                EnableTheming="False" EnableViewState="False" OnClick="btnJudge_Click" Text=" "
                                                ToolTip="نظر کارشناسی" UseSubmitBehavior="False">
                                                <ClientSideEvents Click="function(s, e) {
		if (GridViewTeacherJob.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}

}" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="../../Images/icons/User comment.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال"
                                                ID="btnDelete" EnableClientSideAPI="True" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnDelete_Click" UseSubmitBehavior="False">
                                                <ClientSideEvents Click="function(s, e) {
			if (GridViewTeacherJob.GetFocusedRowIndex()&lt;0)
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
                                                <Image  Url="~/Images/icons/disactive.png"></Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                ID="btnBack" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click"
                                                UseSubmitBehavior="False">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/Back.png"></Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <TSPControls:CustomAspxMenuHorizontal ID="MenuTeacherInfo"  runat="server" OnItemClick="MenuTeacherInfo_ItemClick"  Enabled="False">
                    <Items>
                        <dxm:MenuItem Text="مشخصات فردی" Name="BasicInfo">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="مدارک تحصیلی" Name="licence">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="سوابق آموزشی" Name="Job" Selected="true">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="تالیفات و تحقیقات" Name="ResearchAct">
                        </dxm:MenuItem>
                        <dxm:MenuItem Text="مستندات" Name="Attach">
                        </dxm:MenuItem>
                    </Items>
                </TSPControls:CustomAspxMenuHorizontal>
                    <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelTeacherJob" HeaderText="سابقه کار" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


                            <TSPControls:CustomAspxDevGridView runat="server"  EnableViewState="False"
                                Width="100%" ID="GridViewTeacherJob" DataSourceID="ObjdsTeacherJobHistory" AutoGenerateColumns="False"
                                ClientInstanceName="GridViewTeacherJob" 
                                KeyFieldName="TJobHistoryId"
                                OnHtmlDataCellPrepared="GridViewTeacherJob_HtmlDataCellPrepared"
                                OnAutoFilterCellEditorInitialize="GridViewTeacherJob_AutoFilterCellEditorInitialize">

                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="TJobPlace" Caption="نام موسسه">
                                        <CellStyle Wrap="false"></CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="شهر" FieldName="CitName" VisibleIndex="1"
                                        Width="100px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="TJobName" Caption="نوع فعالیت آموزشی">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="StartDate" Caption="تاریخ شروع"
                                        Width="150px">
                                        <CellStyle HorizontalAlign="Right" Wrap="false"></CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="EndDate" Caption="تاریخ پایان"
                                        Width="150px">
                                        <CellStyle HorizontalAlign="Right" Wrap="false"></CellStyle>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="5" Width="50px" ShowClearFilterButton="true">
                                    </dxwgv:GridViewCommandColumn>
                                </Columns>
                                <Templates>
                                    <DetailRow>
                                        <TSPControls:CustomAspxDevGridView ID="GridViewJudge" runat="server" AutoGenerateColumns="False"
                                              DataSourceID="ObjdsJudgment"
                                            EnableViewState="False" KeyFieldName="JudgeId" OnBeforePerformDataSelect="GridViewJudge_BeforePerformDataSelect"
                                            Width="100%">

                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="0">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="1">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="سمت" FieldName="NcName" VisibleIndex="2">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="شماره جلسه" VisibleIndex="3">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="امتیاز" FieldName="JudgeGrade" VisibleIndex="4">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="نظر کارشناسی" FieldName="JudgeViewPoint" VisibleIndex="6"
                                                    Width="200px">
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>

                                            <SettingsDetail IsDetailGrid="True" />
                                        </TSPControls:CustomAspxDevGridView>
                                    </DetailRow>
                                </Templates>
                                <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                            </TSPControls:CustomAspxDevGridView>
                            <asp:ObjectDataSource runat="server" SelectMethod="SelectByTeacherId" ID="ObjdsTeacherJobHistory"
                                TypeName="TSP.DataManager.TeacherJobHistoryManager" OldValuesParameterFormatString="original_{0}">
                                <SelectParameters>
                                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="TeacherId"></asp:Parameter>
                                    <asp:Parameter DefaultValue="-1" Name="TcId" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="ObjdsJudgment" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="FindByTableType" TypeName="TSP.DataManager.TeacherJudgmentManager">
                                <SelectParameters>
                                    <asp:SessionParameter DefaultValue="-1" Name="TeId" SessionField="TeId" Type="Int32" />
                                    <asp:SessionParameter DefaultValue="-1" Name="TableId" SessionField="TableId" Type="Int32" />
                                    <asp:SessionParameter DefaultValue="-1" Name="TableType" SessionField="TableType"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                ID="btnNew2" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                                UseSubmitBehavior="False" OnClick="btnNew_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image Url="~/Images/icons/new.png" ></Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                Width="25px" ID="btnEdit2" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                                UseSubmitBehavior="False" OnClick="btnEdit_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacherJob.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/edit.png"></Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                Width="25px" ID="btnView2" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                                UseSubmitBehavior="False" OnClick="btnView_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacherJob.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/view.png"></Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnJudge2" runat="server" EnableClientSideAPI="True" 
                                                EnableTheming="False" EnableViewState="False" OnClick="btnJudge_Click" Text=" "
                                                ToolTip="نظر کارشناسی" UseSubmitBehavior="False">
                                                <ClientSideEvents Click="function(s, e) {
		if (GridViewTeacherJob.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}

}" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="../../Images/icons/User comment.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال"
                                                ID="btnDelete2" EnableClientSideAPI="True" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnDelete_Click" UseSubmitBehavior="False">
                                                <ClientSideEvents Click="function(s, e) {
			if (GridViewTeacherJob.GetFocusedRowIndex()&lt;0)
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
                                                <Image  Url="~/Images/icons/disactive.png"></Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                ID="btnBack2" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click"
                                                UseSubmitBehavior="False">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/Back.png"></Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldTeacher">
                </dxhf:ASPxHiddenField>

            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
            <ProgressTemplate>
                <div class="modalPopup">
                    لطفا صبر نمایید
                    <img align="middle" src="../../Image/indicator.gif" />
                </div>
            </ProgressTemplate>
        </asp:ModalUpdateProgress>
</asp:Content>
