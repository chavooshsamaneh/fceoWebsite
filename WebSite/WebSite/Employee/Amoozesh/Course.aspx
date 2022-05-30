<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPagePortals.master"
    CodeFile="Course.aspx.cs" Inherits="Employee_Amoozesh_Course" Title="مدیریت دروس مورد تایید نظام مهندسی" %>

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
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
                        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
                        FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS" SelectedCSS="PickerSelectedCSS"
                        WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
                    </pdc:PersianDateScriptManager>
                    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                            href="#">بستن</a>]</div>
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
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                    ID="BtnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="BtnNew_Click">
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/new.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                    Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="btnEdit_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt; 0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}	
}"></ClientSideEvents>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/edit.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                                    ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                    OnClick="btnView_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt; 0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}	
}"></ClientSideEvents>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/view.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>   <td width="10px" align="center">
<TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
</TSPControls:MenuSeprator>
</td>
                                                            <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال"
                                                                    CausesValidation="False" ID="btnDisActive" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                                    EnableViewState="False" EnableTheming="False" OnClick="btnDisActive_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt; 0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}	
else
	 e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="~/Images/icons/disactive.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                                <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="فعال"
                                                                    CausesValidation="False" ID="btnActive" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                                    EnableViewState="False" EnableTheming="False" OnClick="btnActive_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt; 0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}	
else
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="../../Images/icons/Active.png">
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
                    <TSPControls:CustomAspxDevGridView Width="100%" ID="GridViewCourse" runat="server"
                        DataSourceID="OdbCourse" 
                        ClientInstanceName="grid"  KeyFieldName="CrsId" OnFocusedRowChanged="GridViewCourse_FocusedRowChanged">
                        <SettingsBehavior  ColumnResizeMode="Control">
                        </SettingsBehavior>
                        <Columns>
                            <dxwgv:GridViewDataTextColumn  VisibleIndex="0" FieldName="CrsId"
                                Caption="شماره درس">
                            </dxwgv:GridViewDataTextColumn>
                            
                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="CrsCode" Width="60px" Caption="کد! ">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="CrsName" Width="170px"
                                Caption="نام ">
                                <CellStyle Wrap="false">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Point" Width="30px" Caption="امتیاز">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="3" FieldName="ValidDuration" Width="80px"
                                Caption="مدت زمان اعتبار">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="Duration" Width="80px"
                                Caption="مدت زمان دوره">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="6" Width="1px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="InActiveName" Width="40px"
                                Caption="وضعیت">
                            </dxwgv:GridViewDataTextColumn>
                        </Columns>
                    </TSPControls:CustomAspxDevGridView>
                    <br />
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>
                                <table cellpadding="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td align="right">
                                                <div dir="rtl">
                                                    <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                                        cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                                        ID="BtnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="BtnNew_Click">
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/new.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                                        Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                                                        EnableTheming="False" OnClick="btnEdit_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt; 0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}	
}"></ClientSideEvents>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/edit.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                                        ID="btnView2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                                        OnClick="btnView_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt; 0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}	
}"></ClientSideEvents>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/view.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>   <td width="10px" align="center">
<TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
</TSPControls:MenuSeprator>
</td>
                                                                <td>
                                                                    <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال"
                                                                        CausesValidation="False" ID="btnDisActive2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                                        EnableViewState="False" EnableTheming="False" OnClick="btnDisActive_Click">
                                                                        <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt; 0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}		
e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                                        <HoverStyle BackColor="#FFE0C0">
                                                                            <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                        </HoverStyle>
                                                                        <Image  Url="~/Images/icons/disactive.png">
                                                                        </Image>
                                                                    </TSPControls:CustomAspxButton>
                                                                </td>   <td>
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="فعال"
                                                                    CausesValidation="False" ID="btnActive2" EnableClientSideAPI="True" UseSubmitBehavior="False"
                                                                    EnableViewState="False" EnableTheming="False" OnClick="btnActive_Click">
                                                                    <ClientSideEvents Click="function(s, e) {
if (grid.GetFocusedRowIndex()&lt; 0)
 	{
   		e.processOnServer=false;
   		alert('ردیفی انتخاب نشده است');
 	}	
else
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}"></ClientSideEvents>
                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                                    </HoverStyle>
                                                                    <Image  Url="../../Images/icons/Active.png">
                                                                    </Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomASPxRoundPanelMenu>
                    <asp:ObjectDataSource ID="OdbCourse" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.CourseManager">
                    </asp:ObjectDataSource>         
                    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                        BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                        <ProgressTemplate>
                            <div class="modalPopup">
                                لطفا صبر نمایید
                                <img src="../../Image/indicator.gif" align="middle" />
                            </div>
                        </ProgressTemplate>
                    </asp:ModalUpdateProgress>
     
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
