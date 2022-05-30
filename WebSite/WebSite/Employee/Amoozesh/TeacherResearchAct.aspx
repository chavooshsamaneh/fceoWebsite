<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="TeacherResearchAct.aspx.cs" Inherits="Employee_Amoozesh_TeacherResearchAct"
    Title="تالیفات و تحقیقات استاد" %>

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
 
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
            CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
            FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
            WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
        </pdc:PersianDateScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate> 
                    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                        [<a class="closeLink" href="#">بستن</a>]
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
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="" 
                                                            ToolTip="جدید" Width="" ID="btnNew" AutoPostBack="False" EnableViewState="False"
                                                            EnableTheming="False" SkinID="" OnClick="btnNew_Click" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
	//GridViewTeacherResearch.AddNewRow();
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/new.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="" 
                                                            ToolTip="ویرایش" Width="25px" ID="btnEdit" AutoPostBack="False" EnableViewState="False"
                                                            EnableTheming="False" SkinID="" OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacherResearch.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	//else
	//{
		//GridViewTeacherResearch.StartEditRow(GridViewTeacherResearch.GetFocusedRowIndex());
	//}
	
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/edit.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnJudge" runat="server" EnableClientSideAPI="True" 
                                                            EnableTheming="False" EnableViewState="False" OnClick="btnJudge_Click" Text=" "
                                                            ToolTip="نظر کارشناسی" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewTeacherResearch.GetFocusedRowIndex()&lt;0)
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
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="" 
                                                            ToolTip="غیرفعال" Width="" ID="btnDelete" EnableClientSideAPI="True" EnableViewState="False"
                                                            EnableTheming="False" SkinID="" OnClick="btnDelete_Click" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewTeacherResearch.GetFocusedRowIndex()&lt;0)
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
                                                            <Image  Url="~/Images/icons/disactive.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="" 
                                                            ToolTip="بازگشت" Width="" ID="btnBack" EnableViewState="False" EnableTheming="False"
                                                            SkinID="" OnClick="btnBack_Click" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/Back.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                  </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu> 
                        <TSPControls:CustomAspxMenuHorizontal ID="MenuTeacherInfo" runat="server"  OnItemClick="MenuTeacherInfo_ItemClick" >
                            <Items>
                                <dxm:MenuItem Name="BasicInfo" Text="مشخصات فردی">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="licence" Text="مدارک تحصیلی">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Job" Text="سوابق آموزشی">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Research" Text="تالیفات و تحقیقات" Selected="True">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Attachment" Text="مستندات">
                                </dxm:MenuItem>
                            </Items>
                        </TSPControls:CustomAspxMenuHorizontal>
                  
                        <br /> 	<TSPControls:CustomASPxRoundPanel ID="RoundPanelTeacherResearch"  HeaderText="تحقیقات و تالیفات" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>

       
                                        <TSPControls:CustomAspxDevGridView runat="server"  EnableViewState="False"
                                            ID="GridViewTeacherResearch" KeyFieldName="TResearchId" AutoGenerateColumns="False"
                                            ClientInstanceName="GridViewTeacherResearch" 
                                            OnRowUpdating="GridViewTeacherResearch_RowUpdating" OnRowInserting="GridViewTeacherResearch_RowInserting"
                                            OnBeforePerformDataSelect="GridViewTeacherResearch_BeforePerformDataSelect" Width="100%"
                                            OnHtmlDataCellPrepared="GridViewTeacherResearch_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="GridViewTeacherResearch_AutoFilterCellEditorInitialize"
                                            RightToLeft="True">
                                            <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>
                                            <Templates>
                                                <DetailRow>
                                                    <TSPControls:CustomAspxDevGridView ID="GridViewJudge" runat="server" Width="100%"
                                                          EnableViewState="False"
                                                        AutoGenerateColumns="False" KeyFieldName="JudgeId" DataSourceID="ObjdsJudgment"
                                                        OnBeforePerformDataSelect="GridViewJudge_BeforePerformDataSelect">
                                                       
                                                        <Columns>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="FirstName" Caption="نام">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="LastName" Caption="نام خانوادگی">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="NcName" Caption="سمت">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" Caption="شماره جلسه">
                                                            </dxwgv:GridViewDataTextColumn>
                                                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="JudgeGrade" Caption="امتیاز">
                                                            </dxwgv:GridViewDataTextColumn>
                                                        </Columns>
                                                        
                                                        <SettingsDetail IsDetailGrid="True"></SettingsDetail>
                                                    </TSPControls:CustomAspxDevGridView>
                                                </DetailRow>
                                            </Templates>
                                            <SettingsEditing EditFormColumnCount="1" PopupEditFormHorizontalAlign="WindowCenter"
                                                PopupEditFormModal="True" Mode="PopupEditForm">
                                            </SettingsEditing>
                                             
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn Caption="نام مقاله" FieldName="Name" VisibleIndex="0"
                                                    Width="100px">
                                                    <PropertiesTextEdit Width="150px"></PropertiesTextEdit>
                                                    <EditCellStyle HorizontalAlign="Right"></EditCellStyle>
                                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataComboBoxColumn Caption="نوع مقاله" FieldName="RaId" Name="ResearchType"
                                                    VisibleIndex="1" Width="300px">
                                                    <PropertiesComboBox DataSourceID="ObjdsResearchActivity" TextField="RaName" ValueField="RaId"
                                                        ValueType="System.String" Width="100px">
                                                    </PropertiesComboBox>
                                                    <EditCellStyle HorizontalAlign="Right"></EditCellStyle>
                                                    <EditItemTemplate>
                                                        <div style="width: 100px; height: 23px" dir="ltr">
                                                            <TSPControls:CustomAspxComboBox ID="cmbResearchActivity" runat="server" Width="150px"    __designer:wfdid="w15" DataSourceID="ObjdsResearchActivity" TextField="RaName" ValueField="RaId" ValueType="System.String">
                                                                <ButtonStyle Width="13px"></ButtonStyle>

                                                                <ValidationSettings>
                                                                    <ErrorImage Height="14px" Width="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomAspxComboBox>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                                </dxwgv:GridViewDataComboBoxColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="تاریخ مقاله" FieldName="ResearchDate" Name="ResearchDate"
                                                    VisibleIndex="2" Width="100px">
                                                    <PropertiesTextEdit Width="100px"></PropertiesTextEdit>
                                                    <EditCellStyle HorizontalAlign="Right"></EditCellStyle>
                                                    <EditItemTemplate>
                                                        <pdc:PersianDateTextBox ID="txtResearchDate" runat="server" Width="150px" Text='<%#Bind("ResearchDate") %>' __designer:dtid="844424930132101" __designer:wfdid="w14" DefaultDate="" ShowPickerOnTop="True" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif"></pdc:PersianDateTextBox>
                                                    </EditItemTemplate>
                                                    <CellStyle HorizontalAlign="Right" Wrap="False"></CellStyle>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataMemoColumn Caption="توضیحات" FieldName="Description" VisibleIndex="3"
                                                    Width="150px">
                                                    <PropertiesMemoEdit Width="150px"></PropertiesMemoEdit>
                                                    <EditCellStyle HorizontalAlign="Right"></EditCellStyle>
                                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                                </dxwgv:GridViewDataMemoColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="امتیاز" FieldName="Grade" VisibleIndex="4"
                                                    Width="50px">
                                                    <EditFormSettings Visible="False"></EditFormSettings>
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="6" Width="50px" ShowClearFilterButton="true">
                                                 
                                                </dxwgv:GridViewCommandColumn>
                                            </Columns>
                                             
                                        </TSPControls:CustomAspxDevGridView>
                                   </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
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
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="" 
                                                            ToolTip="جدید" Width="" ID="btnNew2" AutoPostBack="False" EnableViewState="False"
                                                            EnableTheming="False" SkinID="" OnClick="btnNew_Click" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
	//GridViewTeacherResearch.AddNewRow();
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/new.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="" 
                                                            ToolTip="ویرایش" Width="25px" ID="btnEdit2" AutoPostBack="False" EnableViewState="False"
                                                            EnableTheming="False" SkinID="" OnClick="btnEdit_Click" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacherResearch.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
//	else
//	{
	//	GridViewTeacherResearch.StartEditRow(GridViewTeacherResearch.GetFocusedRowIndex());
//	}
	
}"></ClientSideEvents>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/edit.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnJudge2" runat="server" EnableClientSideAPI="True" 
                                                            EnableTheming="False" EnableViewState="False" OnClick="btnJudge_Click" Text=" "
                                                            ToolTip="نظر کارشناسی" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewTeacherResearch.GetFocusedRowIndex()&lt;0)
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
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="" 
                                                            ToolTip="غیرفعال" Width="" ID="btnDelete2" EnableClientSideAPI="True" EnableViewState="False"
                                                            EnableTheming="False" SkinID="" OnClick="btnDelete_Click" UseSubmitBehavior="False">
                                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewTeacherResearch.GetFocusedRowIndex()&lt;0)
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
                                                            <Image  Url="~/Images/icons/disactive.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td>
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="" 
                                                            ToolTip="بازگشت" Width="" ID="btnBack2" EnableViewState="False" EnableTheming="False"
                                                            SkinID="" OnClick="btnBack_Click" UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/Back.png">
                                                            </Image>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table> </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                                        <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldTeacherResearchAct">
                                        </dxhf:ASPxHiddenField>
                                     
                    <asp:ObjectDataSource ID="ObjdsResearchActivity" runat="server" UpdateMethod="Update"
                        TypeName="TSP.DataManager.ResearchActivityManager" SelectMethod="GetData" InsertMethod="Insert"
                        DeleteMethod="Delete">
                         
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsTeacherResearch" runat="server" TypeName="TSP.DataManager.TeacherResearchActivityManager"
                        SelectMethod="SelectByTeacher" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="-1" Name="TecherId" Type="Int32" />
                            <asp:Parameter DefaultValue="-1" Name="TcId" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsMemberResearchActivity" runat="server" UpdateMethod="Update"
                        TypeName="TSP.DataManager.MemberResearchActivityManager" SelectMethod="SelectByMember"
                        InsertMethod="Insert" DeleteMethod="Delete">
                       
                        <SelectParameters>
                            <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
                        </SelectParameters>
                         
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsJudgment" runat="server" TypeName="TSP.DataManager.TeacherJudgmentManager"
                        SelectMethod="FindByResearchActivity">
                        <SelectParameters>
                            <asp:SessionParameter SessionField="TeId" Type="Int32" DefaultValue="-1" Name="TeId"></asp:SessionParameter>
                            <asp:SessionParameter DefaultValue="-1" Name="TResearchId" SessionField="TResearchId"
                                Type="Int32" />
                            <asp:SessionParameter DefaultValue="-1" Name="TableType" SessionField="TableType"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
           
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
