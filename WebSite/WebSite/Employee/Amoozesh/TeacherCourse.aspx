<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="TeacherCourse.aspx.cs" Inherits="Employee_Amoozesh_TeacherCourse"
    Title="دروس استاد" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                 
                    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#"><span style="color: #000000">ب</span>ستن</a>]</div>
                      <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                                    <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: top; width: 100%;">
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید" ID="btnNew" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False">
                                                                        <ClientSideEvents Click="function(s, e) {
	GridViewTeacherCourse.AddNewRow();
}"></ClientSideEvents>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/new.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" AutoPostBack="False">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacherCourse.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	else
	{
		GridViewTeacherCourse.StartEditRow(GridViewTeacherCourse.GetFocusedRowIndex());
	}
}"></ClientSideEvents>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/edit.png"></Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال" ID="btnDisActive" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" AutoPostBack="False" OnClick="btnDisActive_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacherCourse.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	
}" />
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت" CausesValidation="False" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
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
                    <br /> 
                        <TSPControls:CustomAspxMenuHorizontal ID="MenuTeacherInfo" RightToLeft="True" runat="server" SeparatorHeight="100%" SeparatorColor="#A5A6A8" AutoSeparators="RootOnly" OnItemClick="MenuTeacherInfo_ItemClick"    SeparatorWidth="1px" ItemSpacing="0px">
                            <Items>
                                <dxm:MenuItem Name="BasicInfo" Text="مشخصات فردی">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="licence" Text="مدارک تحصیلی">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Job" Text="سوابق آموزشی">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Attachment" Text="مستندات">
                                </dxm:MenuItem>
                                <dxm:MenuItem Name="Judge" Text="نظر کارشناس">
                                </dxm:MenuItem>
                            </Items>
                            <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-2" LastItemX="-1" LastItemY="-2"
                                X="-1" Y="-2" />
                            <Border BorderColor="#A5A6A8" BorderStyle="Solid" BorderWidth="1px" />
                            <VerticalPopOutImage Height="8px" Width="4px" />
                            <ItemStyle ImageSpacing="5px" PopOutImageSpacing="7px" VerticalAlign="Middle"></ItemStyle>
                            <SubMenuItemStyle ImageSpacing="7px">
                            </SubMenuItemStyle>
                            <SubMenuStyle BackColor="#EDF3F4" GutterWidth="0px" SeparatorColor="#7EACB1" />
                            <HorizontalPopOutImage Height="7px" Width="7px" />
                        </TSPControls:CustomAspxMenuHorizontal>
                     
                    <br />
                    <TSPControls:CustomAspxDevGridView ID="GridViewTeacherCourse" runat="server"   OnRowUpdating="GridViewTeacherCourse_RowUpdating" OnRowInserting="GridViewTeacherCourse_RowInserting" KeyFieldName="TCrsId" DataSourceID="ObjdsTeacherCourse" ClientInstanceName="GridViewTeacherCourse" AutoGenerateColumns="False" OnRowValidating="GridViewTeacherCourse_RowValidating" Width="100%">
                        <Templates>
                            <DetailRow>
                                <TSPControls:CustomAspxDevGridView ID="GridViewTeacherCourseJudgment" runat="server"
                                    AutoGenerateColumns="False"  
                                    DataSourceID="ObjdsTeacherCourseJudgment" KeyFieldName="TCrsJId" OnBeforePerformDataSelect="GridViewTeacherCourseJudgment_BeforePerformDataSelect">
                                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                     
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="FirstName" VisibleIndex="0">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="LastName" VisibleIndex="1">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="وضعیت تایید" FieldName="IsConfirme" VisibleIndex="2">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="شماره نامه" FieldName="MailNo" VisibleIndex="3">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="دلایل" FieldName="ViewPoint" VisibleIndex="4">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="5" ShowClearFilterButton="false">
                                            
                                        </dxwgv:GridViewCommandColumn>
                                    </Columns>
                                  
                                    <SettingsDetail IsDetailGrid="True" />
                                </TSPControls:CustomAspxDevGridView>

                            </DetailRow>
                        </Templates>

                        
                        <Columns>
                            <dxwgv:GridViewDataComboBoxColumn FieldName="CrsId" Width="100px" Caption="درس" VisibleIndex="0">
                                <EditCellStyle HorizontalAlign="Right"></EditCellStyle>

                                <PropertiesComboBox Width="150px" ValueType="System.String" TextField="CrsName" ValueField="CrsId" DataSourceID="ObjdsCourse"></PropertiesComboBox>
                            </dxwgv:GridViewDataComboBoxColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="CrsCode" Caption="کد درس">
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="RequestDate" Caption="تاریخ درخواست">
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataMemoColumn VisibleIndex="3" FieldName="Description" Width="150px" Caption="توضیحات">
                                <PropertiesMemoEdit Height="50px" Width="300px"></PropertiesMemoEdit>
                            </dxwgv:GridViewDataMemoColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="IsConfirme" Caption="وضعیت تایید">
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn VisibleIndex="5" Caption=" " ShowClearFilterButton="true">
                               
                            </dxwgv:GridViewCommandColumn>
                        </Columns>

                        
                        <SettingsDetail ShowDetailRow="True"></SettingsDetail>
                    </TSPControls:CustomAspxDevGridView>
                    <br />
                     <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


   
                                    <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: top; width: 100%;">
                                                    <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldTeacherCourse"></dxhf:ASPxHiddenField>
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید" ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" AutoPostBack="False">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/new.png"></Image>
                                                                        <ClientSideEvents Click="function(s, e) {
	GridViewTeacherCourse.AddNewRow();
}" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" AutoPostBack="False">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/edit.png"></Image>
                                                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacherCourse.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	else
	{
		GridViewTeacherCourse.StartEditRow(GridViewTeacherCourse.GetFocusedRowIndex());
	}
}" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال" ID="btnDisActive2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" AutoPostBack="False" OnClick="btnDisActive_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacherCourse.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}" />
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                                        </HoverStyle>
                                                                        <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت" CausesValidation="False" ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/Back.png"></Image>
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
                    <asp:ObjectDataSource ID="ObjdsTeacherCourse" runat="server" TypeName="TSP.DataManager.TeachersCourseManager" SelectMethod="SelectTeachersCourse">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="-1" Name="TeId" Type="Int32" />
                            <asp:Parameter DefaultValue="-1" Name="CrsId" Type="Int32" />
                            <asp:Parameter DefaultValue="-1" Name="IsConfirmed" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsCourse" runat="server" TypeName="TSP.DataManager.CourseManager" SelectMethod="GetData"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsTeacherCourseJudgment" runat="server" TypeName="TSP.DataManager.TeacherCourseJudgmentManager" SelectMethod="SelectByEmpId">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="-1" Name="TableId" SessionField="TableId" Type="Int32" />
                            <asp:Parameter DefaultValue="-1" Name="EmpId" Type="Int32" />
                            <asp:SessionParameter DefaultValue="-1" Name="TableType" SessionField="TableType"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
          
            </ContentTemplate>
        </asp:UpdatePanel>
 
</asp:Content>
