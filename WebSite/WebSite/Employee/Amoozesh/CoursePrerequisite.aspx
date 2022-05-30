<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="CoursePrerequisite.aspx.cs" Inherits="Employee_Amoozesh_CoursePrerequisite"
    Title="پیشنیاز دروس" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div dir="ltr" style="width: 100%" align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                        href="#">بستن</a>]</div>
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table class="TableMenu">
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
	GridViewPrerequisite.AddNewRow();
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False"  EnableViewState="False"
                                                Width="25px" EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
			if (GridViewPrerequisite.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	GridViewPrerequisite.StartEditRow(GridViewPrerequisite.GetFocusedRowIndex());
}	
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="~/Images/icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                                EnableClientSideAPI="True" ID="btnDelete" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnDelete_Click">
                                                <ClientSideEvents Click="function(s, e) {
		if (GridViewPrerequisite.GetFocusedRowIndex()&lt;0)
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
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="~/Images/icons/delete.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>  <td width="10px" align="center">
<TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
</TSPControls:MenuSeprator>
</td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnBack_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="~/Images/icons/Back.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                
                    <TSPControls:CustomAspxMenuHorizontal ID="MenuCourseDetail" runat="server" 
                        OnItemClick="MenuCourseDetail_ItemClick" >
                        <Items>
                            <dxm:MenuItem Name="Course" Text="مشخصات درس">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="CourseDetail" Text="ارتباط با پروانه">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Refrences" Text="منابع درس">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="CoursePrerequisite" Text="پیشنیازها" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Group" Text="گروه بندی">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Attachment" Text="فایل های پیوست">
                            </dxm:MenuItem>
                        </Items>
                    </TSPControls:CustomAspxMenuHorizontal>
                </div>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelPrerequisite" HeaderText="پیشنیاز"
                    runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <TSPControls:CustomAspxDevGridView ID="GridViewPrerequisite" Width="100%" runat="server"
                                  AutoGenerateColumns="False"
                                DataSourceID="ObjdsPrerequisite" ClientInstanceName="GridViewPrerequisite" KeyFieldName="PrerequisiteId"
                                OnRowInserting="GridViewPrerequisite_RowInserting" OnRowUpdating="GridViewPrerequisite_RowUpdating">
                             
                                <Columns>
                                    <dxwgv:GridViewDataComboBoxColumn Caption="پیشنیاز" FieldName="ParentCrsId" VisibleIndex="0"
                                        Width="200px">
                                        <EditCellStyle HorizontalAlign="Right">
                                        </EditCellStyle>
                                        <PropertiesComboBox DataSourceID="ObjdsCourse" TextField="CrsName" ValueField="CrsId"
                                            ValueType="System.String" Width="300px">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="پیشنیاز را انتخاب نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </PropertiesComboBox>
                                    </dxwgv:GridViewDataComboBoxColumn>
                                    <dxwgv:GridViewDataMemoColumn Caption="توضیحات" FieldName="Description" VisibleIndex="1">
                                        <PropertiesMemoEdit Height="70px" Width="300px">
                                        </PropertiesMemoEdit>
                                    </dxwgv:GridViewDataMemoColumn>
                                    <dxwgv:GridViewCommandColumn Caption="" Name="" VisibleIndex="2" ShowClearFilterButton="true">
                                    </dxwgv:GridViewCommandColumn>
                                </Columns>
                                <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" PopupEditFormModal="True"
                                    PopupEditFormVerticalAlign="WindowCenter" />
                            </TSPControls:CustomAspxDevGridView>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanel>
                <br />
                <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <table class="TableMenu">
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
		GridViewPrerequisite.AddNewRow();
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="~/Images/icons/new.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False"
                                                EnableViewState="False" Width="25px" EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
			if (GridViewPrerequisite.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	GridViewPrerequisite.StartEditRow(GridViewPrerequisite.GetFocusedRowIndex());
}	
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="~/Images/icons/edit.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                                EnableClientSideAPI="True" ID="btnDelete2" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False" OnClick="btnDelete_Click">
                                                <ClientSideEvents Click="function(s, e) {
	if (GridViewPrerequisite.GetFocusedRowIndex()&lt;0)
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
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="~/Images/icons/delete.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                          <td width="10px" align="center">
<TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
</TSPControls:MenuSeprator>
</td>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                OnClick="btnBack_Click">
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="~/Images/icons/Back.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldPre">
                </dxhf:ASPxHiddenField>
                <asp:ObjectDataSource ID="ObjdsPrerequisite" runat="server" SelectMethod="SelectByCourseId"
                    TypeName="TSP.DataManager.CoursePrerequisiteManager">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="-1" Name="CourseId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjdsCourse" runat="server" SelectMethod="GetData" TypeName="TSP.DataManager.CourseManager">
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
