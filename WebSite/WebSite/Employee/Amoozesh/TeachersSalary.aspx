<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="TeachersSalary.aspx.cs" Inherits="Employee_Amoozesh_TecheasSalary"
    Title="مدیریت حق الزحمه اساتید" %>

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
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#"><span style="color: #000000">ب</span>ستن</a>]
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
                                            ID="BtnNew" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                            UseSubmitBehavior="False" OnClick="BtnNew_Click">
                                            <ClientSideEvents Click="function(s, e) {
	//GridViewSalary.AddNewRow();
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                            Width="25px" ID="btnEdit" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                            UseSubmitBehavior="False" OnClick="btnEdit_Click">
                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewSalary.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                            Width="25px" ID="btnView" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                            UseSubmitBehavior="False" OnClick="btnView_Click">
                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewSalary.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                            ID="btnDelete" EnableClientSideAPI="True" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnDelete_Click" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewSalary.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/delete.png">
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
            <TSPControls:CustomAspxDevGridView ID="GridViewSalary" runat="server" 
                 KeyFieldName="SalaryId" OnRowUpdating="GridViewSalary_RowUpdating"
                OnRowInserting="GridViewSalary_RowInserting" ClientInstanceName="GridViewSalary"
                AutoGenerateColumns="False" DataSourceID="ObjdsTeacherSalary" Width="100%" OnHtmlDataCellPrepared="GridViewSalary_HtmlDataCellPrepared"
                OnAutoFilterCellEditorInitialize="GridViewSalary_AutoFilterCellEditorInitialize" RightToLeft="True">

                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="حق الزحمه عملی(ریال)" FieldName="SalaryPractical"
                        VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="حق الزحمه تئوری(ریال)" FieldName="SalaryNonPractical"
                        VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="حق الزحمه بازدید از کارگاه(ریال)" FieldName="SalaryWorkroom"
                        VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="مدرک تحصیلی" FieldName="LiId" Name="Licence"
                        VisibleIndex="4">
                        <PropertiesComboBox DataSourceID="ObjdsLicence" TextField="LiName" ValueField="LiId"
                            ValueType="System.String" Width="150px">
                        </PropertiesComboBox>
                        <EditItemTemplate>
                            <div style="width: 100px; height: 24px" dir="ltr" id="DIV1" onclick="return DIV1_onclick()">
                                <TSPControls:CustomAspxComboBox ID="cmbLicence" runat="server" Text='<%# Bind("LiName") %>'    DataSourceID="ObjdsLicence" __designer:wfdid="w7" ValueType="System.String" TextField="LiName" ValueField="LiId">
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
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ" FieldName="StartDate" Name="StartDate"
                        VisibleIndex="5" Width="100px">
                        <EditItemTemplate>
                            <pdc:PersianDateTextBox ID="txtStartDate" Text='<%#Bind("StartDate") %>' runat="server" Width="145px" DefaultDate="" ShowPickerOnTop="True" PickerDirection="ToRight" IconUrl="~/Image/Calendar.gif" __designer:wfdid="w6"></pdc:PersianDateTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>

                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="5" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <SettingsEditing PopupEditFormModal="True" Mode="PopupEditForm" EditFormColumnCount="1"
                    NewItemRowPosition="Bottom">
                </SettingsEditing>

            </TSPControls:CustomAspxDevGridView>
            <asp:ObjectDataSource ID="ObjdsTeacherSalary" runat="server" SelectMethod="GetData"
                TypeName="TSP.DataManager.TeachersSalaryManager"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjdsLicence" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.LicenceManager"></asp:ObjectDataSource>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table >
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                            ID="btnNew2" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                            UseSubmitBehavior="False" OnClick="BtnNew_Click">
                                            <ClientSideEvents Click="function(s, e) {
	//GridViewSalary.AddNewRow();
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/new.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                            Width="25px" ID="btnEdit2" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                            UseSubmitBehavior="False" OnClick="btnEdit_Click">
                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewSalary.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/edit.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                            Width="25px" ID="btnView2" AutoPostBack="False" EnableViewState="False" EnableTheming="False"
                                            UseSubmitBehavior="False" OnClick="btnView_Click">
                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewSalary.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                            ID="btnDelete2" EnableClientSideAPI="True" EnableViewState="False" EnableTheming="False"
                                            OnClick="btnDelete_Click" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewSalary.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}
}"></ClientSideEvents>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>
                                            <Image  Url="~/Images/icons/delete.png">
                                            </Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>

</asp:Content>
