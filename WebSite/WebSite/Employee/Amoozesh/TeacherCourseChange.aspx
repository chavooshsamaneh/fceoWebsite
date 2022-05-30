<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="TeacherCourseChange.aspx.cs" Inherits="Employee_Amoozesh_TeacherCourseChange" Title="Untitled Page" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

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
                                                <td style="vertical-align: top; width: 100%; text-align: right">
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
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False">
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
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال" ID="btnDisActive" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacherCourse.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	else
	{
		PopUpInActive.Show();
	}
}"></ClientSideEvents>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/disactive.png"></Image>
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
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table> </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                                
                    <br />
                    <TSPControls:CustomAspxDevGridView ID="GridViewTeacherCourse" runat="server"   OnCustomCallback="GridViewTeacherCourse_CustomCallback" AutoGenerateColumns="False" ClientInstanceName="GridViewTeacherCourse" DataSourceID="ObjdsTeacherCourse" KeyFieldName="TCrsId" OnRowInserting="GridViewTeacherCourse_RowInserting" OnRowUpdating="GridViewTeacherCourse_RowUpdating" OnRowValidating="GridViewTeacherCourse_RowValidating">
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
                                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="5" ShowClearFilterButton="true"> 
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

                        <SettingsEditing EditFormColumnCount="1" PopupEditFormModal="True" Mode="PopupEditForm"></SettingsEditing>
 

                        <SettingsDetail ShowDetailRow="True"></SettingsDetail>
                    </TSPControls:CustomAspxDevGridView>
          
                    <TSPControls:CustomASPxPopupControl ID="PopUpInActive" runat="server" Width="338px"    ClientInstanceName="PopUpInActive" __designer:wfdid="w1" HeaderText="درخواست حذف" CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" AllowDragging="True" ShowPageScrollbarWhenModal="True">
                        <ContentCollection>
                            <dxpc:PopupControlContentControl runat="server">
                                <TSPControls:CustomAspxCallbackPanel runat="server" Width="200px" ID="CallbackPanelDeActive" ClientInstanceName="CallbackPanelDeActive" __designer:wfdid="w21" OnCallback="CallbackPanelDeActive_Callback">
                                    <PanelCollection>
                                        <dxp:PanelContent runat="server">
                                            <table style="width: 157%">
                                                <tbody>
                                                    <tr>
                                                        <td style="vertical-align: top; width: 67px; text-align: right">
                                                            <dxe:ASPxLabel runat="server" Text="شماره نامه:" ID="ASPxLabel1" __designer:wfdid="w49"></dxe:ASPxLabel>

                                                        </td>
                                                        <td style="vertical-align: top; text-align: right" colspan="3">
                                                            <TSPControls:CustomTextBox IsMenuButton="true" runat="server"  Width="142px" ID="txtMailNo"  __designer:wfdid="w50">
                                                                <ValidationSettings>
                                                                    <ErrorImage Height="14px" Url="~/App_Themes/Glass/Editors/edtError.png"></ErrorImage>

                                                                    <ErrorFrameStyle ImageSpacing="4px">
                                                                        <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                    </ErrorFrameStyle>
                                                                </ValidationSettings>
                                                            </TSPControls:CustomTextBox>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top; text-align: center" dir="ltr" colspan="4">
                                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="ذخیره"  Width="63px" ID="btnSave"  __designer:wfdid="w51">
                                                                <ClientSideEvents Click="function(s, e) {
	CallbackPanelDeActive.PerformCallback('');
	PopUpInActive.Hide();
}"></ClientSideEvents>
                                                            </TSPControls:CustomAspxButton>

                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </dxp:PanelContent>
                                    </PanelCollection>
                                </TSPControls:CustomAspxCallbackPanel>
                            </dxpc:PopupControlContentControl>
                        </ContentCollection>

                        <HeaderStyle>
                            <Paddings PaddingTop="1px" PaddingRight="6px" PaddingLeft="10px"></Paddings>
                        </HeaderStyle>

                        <SizeGripImage Height="12px" Width="12px"></SizeGripImage>

                        <CloseButtonImage Height="17px" Width="17px"></CloseButtonImage>
                    </TSPControls:CustomASPxPopupControl>
                    <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


   
                                    <table style="display: block; overflow: hidden; border-collapse: collapse" cellpadding="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="vertical-align: top; width: 100%; text-align: right">
                                                    <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldTeacherCourse"></dxhf:ASPxHiddenField>
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td >
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False">
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
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False">
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
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال" ID="btnDisActive2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (GridViewTeacherCourse.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>

                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                        </HoverStyle>

                                                                        <Image  Url="~/Images/icons/disactive.png"></Image>
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
                    <asp:ObjectDataSource ID="ObjdsTeacherCourse" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectTeachersCourse" TypeName="TSP.DataManager.TeachersCourseManager">
                        <SelectParameters>
                            <asp:Parameter Type="Int32" DefaultValue="-1" Name="TeId"></asp:Parameter>
                            <asp:Parameter Type="Int32" DefaultValue="-1" Name="CrsId"></asp:Parameter>
                            <asp:Parameter Type="Int32" DefaultValue="1" Name="IsConfirmed"></asp:Parameter>
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsCourse" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.CourseManager"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjdsTeacherCourseJudgment" runat="server" SelectMethod="SelectByEmpId" TypeName="TSP.DataManager.TeacherCourseJudgmentManager">
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

