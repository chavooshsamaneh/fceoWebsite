<%@ Page Title="مدیریت سخنران" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="Lecturer.aspx.cs" Inherits="Employee_Amoozesh_Lecturer" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>

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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Content" runat="server" style="width: 100%; text-align: center;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server"
                    visible="true">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]
                </div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>



                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                Width="25px" ID="btnNew" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnNew_Click">
                                                <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                Width="25px" ID="btnEdit" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnEdit_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex &lt; 0)
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
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                Width="25px" ID="btnView" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnView_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex &lt; 0)
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
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnActive" runat="server" AutoPostBack="False" 
                                                EnableTheming="False" EnableViewState="False" OnClick="btnActive_Click" Text=" "
                                                ToolTip="فعال" UseSubmitBehavior="False" Width="25px">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
	else
	 e.processOnServer= confirm('آیا مطمئن به  فعال کردن این ردیف هستید؟');
}" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/acceptResignation.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال"
                                                Width="25px" ID="btnDisActive" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnDisActive_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex &lt; 0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
 else
  e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
	
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/disactive.png">
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
                <TSPControls:CustomAspxDevGridView ID="GridViewLecturer" runat="server" 
                     DataSourceID="ObjectDataSource1"
                    AutoGenerateColumns="False" KeyFieldName="TeId" ClientInstanceName="grid" Width="100%">
                    <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="TeId">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="Name" Caption="نام">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Family" Caption="نام خانوادگی">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Father" Caption="نام پدر"
                            Visible="false">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="4" FieldName="MobileNo" Caption="شماره همراه">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="5" FieldName="LiName" Caption="مدرک تحصیلی">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn VisibleIndex="6" FieldName="InActiveName" Caption="وضعیت">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="7" Width="30px" ShowClearFilterButton="true">
                         
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                </TSPControls:CustomAspxDevGridView>

                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                Width="25px" ID="btnNew2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnNew_Click">
                                                <ClientSideEvents Click="function(s, e) {
	
	
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                Width="25px" ID="btnEdit2" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnEdit_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex &lt; 0)
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
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="مشاهده"
                                                Width="25px" ID="btnView2" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnView_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex &lt; 0)
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
                                        </td>
                                        <td width="10px" align="center">
                                            <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnActive2" runat="server" AutoPostBack="False" 
                                                EnableTheming="False" EnableViewState="False" OnClick="btnActive_Click" Text=" "
                                                ToolTip="فعال" UseSubmitBehavior="False" Width="25px">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
	else
	 e.processOnServer= confirm('آیا مطمئن به  فعال کردن این ردیف هستید؟');
}" />
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                </HoverStyle>
                                                <Image Height="25px" Url="~/Images/icons/acceptResignation.png" Width="25px" />
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td >
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال"
                                                Width="25px" ID="btnDisActive2" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnDisActive_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex &lt; 0)
 {
   e.processOnServer=false;
   alert('ردیفی انتخاب نشده است');
 }
 else
   e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');

}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image  Url="~/Images/icons/disactive.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" FilterExpression="InsId={0}"
                    SelectMethod="SelectLecturer" TypeName="TSP.DataManager.TeacherManager">
                    <FilterParameters>
                        <asp:Parameter Name="newparameter"></asp:Parameter>
                    </FilterParameters>
                    <SelectParameters>
                        <asp:Parameter Type="Int32" DefaultValue="-1" Name="TeId"></asp:Parameter>
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:HiddenField ID="InstitueId" runat="server" Visible="False"></asp:HiddenField>
                <asp:ObjectDataSource ID="ObjdsWorkFlowTask" runat="server" SelectMethod="SelectByWorkId"
                    TypeName="TSP.DataManager.WorkFlowTaskManager">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="WorkFlowId" Type="Int32" />
                        <asp:Parameter DefaultValue="-1" Name="TaskOrder" Type="Int32" />
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
    </div>
</asp:Content>


