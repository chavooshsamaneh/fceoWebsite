<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="TeachersLicence.aspx.cs" Inherits="Employee_Amoozesh_TeachersLicence"
    Title="مدارک تحصیلی استاد" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true"
                dir="rtl">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table>
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew" runat="server" AutoPostBack="False"
                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="جدید" OnClick="btnNew_Click"
                                        UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/new.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" AutoPostBack="False"
                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="ویرایش"
                                        OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
			if (GridViewTeacherLicence.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/edit.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" EnableClientSideAPI="True"
                                        EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                        ToolTip="مشاهده" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
		if (GridViewTeacherLicence.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}

}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/view.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnJudge" runat="server" EnableClientSideAPI="True"
                                        EnableTheming="False" EnableViewState="False" OnClick="btnJudge_Click" Text=" "
                                        ToolTip="نظر کارشناسی" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
		if (GridViewTeacherLicence.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}

}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Url="../../Images/icons/User comment.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableClientSideAPI="True"
                                        EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                        ToolTip="غیرفعال" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
		if (GridViewTeacherLicence.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/disactive.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" EnableTheming="False"
                                        EnableViewState="False" OnClick="btnBack_Click" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/Back.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomAspxMenuHorizontal ID="MenuTeacherInfo" runat="server"
                 OnItemClick="MenuTeacherInfo_ItemClick"
                Enabled="False">
                <Items>
                    <dxm:MenuItem Name="BasicInfo" Text="مشخصات فردی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Licence" Text="مدارک تحصیلی" Selected="true">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق آموزشی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Research" Text="تالیفات و تحقیقات">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Attachment" Text="مستندات">
                    </dxm:MenuItem>
                </Items>
            </TSPControls:CustomAspxMenuHorizontal>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelTeacherLicence" HeaderText="مدارک تحصیلی" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>

                        <TSPControls:CustomAspxDevGridView ID="GridViewTeacherLicence" runat="server" Width="100%"
                            AutoGenerateColumns="False"
                            KeyFieldName="MlId" ClientInstanceName="GridViewTeacherLicence" EnableCallBacks="False"
                            EnableViewState="False">
                            <ClientSideEvents RowClick="function(s, e) {
	//btn.SetEnabled(false);
	//SetControlValues();
}"></ClientSideEvents>

                            <Columns>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="MlId" Name="MlId">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="LiName" Caption="مدرک"
                                    Width="200px" Name="LiName">
                                    <CellStyle Wrap="false"></CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MjName" Caption="رشته"
                                    Name="MjName">
                                    <CellStyle Wrap="false"></CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="UnName" Caption="دانشگاه"
                                    Width="180px" Name="UnName">
                                    <CellStyle Wrap="false"></CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="CitName" Caption="شهر"
                                    Name="CitName">
                                    <CellStyle Wrap="false"></CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Avg" Caption="معدل" Name="Avg"
                                    Width="50px">
                                    <CellStyle Wrap="false"></CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="امتیاز" FieldName="Grade" VisibleIndex="5"
                                    Width="40px">
                                    <CellStyle Wrap="false"></CellStyle>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="7" Width="30px" ShowClearFilterButton="true">
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="LiId" Name="LiId">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="MjId" Name="MjId">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="UnId" Name="UnId">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="NumUnit"
                                    Name="NumUnit">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="5" FieldName="Description"
                                    Name="Description">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="6" FieldName="StartDate"
                                    Name="StartDate">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="6" FieldName="EndDate"
                                    Name="EndDate">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>

                            <Settings ShowHorizontalScrollBar="true"></Settings>
                            <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
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
                        </TSPControls:CustomAspxDevGridView>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table >
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server" AutoPostBack="False"
                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="جدید" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/new.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" AutoPostBack="False"
                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="ویرایش"
                                        UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
			if (GridViewTeacherLicence.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/edit.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server" EnableClientSideAPI="True"
                                        EnableTheming="False" EnableViewState="False" Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
		if (GridViewTeacherLicence.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}

}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/view.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnJudge2" runat="server" EnableClientSideAPI="True"
                                        EnableTheming="False" EnableViewState="False" OnClick="btnJudge_Click" Text=" "
                                        ToolTip="نظر کارشناسی" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
		if (GridViewTeacherLicence.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}

}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Url="../../Images/icons/User comment.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableClientSideAPI="True"
                                        EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                        ToolTip="غیرفعال" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
		if (GridViewTeacherLicence.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/disactive.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server" EnableTheming="False"
                                        EnableViewState="False" OnClick="btnBack_Click" Text=" " ToolTip="بازگشت" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                        </HoverStyle>
                                        <Image Url="~/Images/icons/Back.png" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField ID="HiddenFieldTeacherLicnce" runat="server">
            </dxhf:ASPxHiddenField>

            <asp:ObjectDataSource ID="ObjdsTeacherLicence" runat="server" SelectMethod="SelectByTeacherId"
                TypeName="TSP.DataManager.TeacherLicenceManger" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="TeacherId" Type="Int32" />
                    <asp:Parameter DefaultValue="0" Name="InActive" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsMemberLicence" runat="server" SelectMethod="SelectByMemberId"
                TypeName="TSP.DataManager.MemberLicenceManager" DeleteMethod="Delete" InsertMethod="Insert"
                OldValuesParameterFormatString="original_{0}" UpdateMethod="Update">

                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="MemberId" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="InActive" Type="Int32" />
                </SelectParameters>

            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsJudgment" runat="server" SelectMethod="FindByTableType"
                TypeName="TSP.DataManager.TeacherJudgmentManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="-1" Name="TeId" SessionField="TeId" Type="Int32" />
                    <asp:SessionParameter DefaultValue="-1" Name="TableId" SessionField="TableId" Type="Int32" />
                    <asp:SessionParameter DefaultValue="-1" Name="TableType" SessionField="TableType"
                        Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
    
  
    <asp:ObjectDataSource ID="ODBMadrak" runat="server" DeleteMethod="Delete" FilterExpression="MeId={0}"
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
        TypeName="TSP.DataManager.MemberLicenceManager" UpdateMethod="Update">

        <FilterParameters>
            <asp:Parameter Name="newparameter" />
        </FilterParameters>

    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODBLicence" runat="server"
        SelectMethod="GetData" TypeName="TSP.DataManager.LicenceManager"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODBMajor" runat="server"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
        TypeName="TSP.DataManager.MajorManager" UpdateMethod="Update"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODBUniversity" runat="server"
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="TSP.DataManager.UniversityManager" UpdateMethod="Update"></asp:ObjectDataSource>

</asp:Content>
