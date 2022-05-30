<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="CourseRefrences.aspx.cs" Inherits="Employee_Amoozesh_CourseRefrences"
    Title="منابع درسی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                    <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                    [<a class="closeLink" href="#">بستن</a>]</div>
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
	GridViewCourseRefrences.AddNewRow();
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
                                                ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                Width="25px" EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
		if (GridViewCourseRefrences.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	GridViewCourseRefrences.StartEditRow(GridViewCourseRefrences.GetFocusedRowIndex());
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
	if (GridViewCourseRefrences.GetFocusedRowIndex()&lt;0)
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
                                        <td>
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
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
                <div align="right">
                    <TSPControls:CustomAspxMenuHorizontal ID="MenuCourseDetail" runat="server" 
                       OnItemClick="MenuCourseDetail_ItemClick" >
                        <Items>
                            <dxm:MenuItem Name="Course" Text="مشخصات درس">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="CourseDetail" Text="ارتباط با پروانه">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="CourseReferences" Text="منابع درسی" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="پیشنیاز ها" Name="Prerequisite">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Group" Text="گروه بندی">
                            </dxm:MenuItem>
                            <dxm:MenuItem Name="Attachment" Text="فایل های پیوست">
                            </dxm:MenuItem>
                        </Items>
                    </TSPControls:CustomAspxMenuHorizontal>
                </div>
                <br />
                <TSPControls:CustomASPxRoundPanel ID="RoundPanelCourseRefrence" HeaderText="منابع درس"
                    runat="server" Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                            <TSPControls:CustomAspxDevGridView runat="server"  Width="100%"
                                RightToLeft="True" ID="GridViewCourseRefrences" DataSourceID="ObjdsCourseRefrences"
                                KeyFieldName="RefrenceId" AutoGenerateColumns="False" ClientInstanceName="GridViewCourseRefrences"
                                 OnRowUpdating="GridViewCourseRefrences_RowUpdating"
                                OnRowInserting="GridViewCourseRefrences_RowInserting">
                                <Images >
                                </Images>
                                <SettingsEditing PopupEditFormHeight="150px" PopupEditFormModal="True" PopupEditFormVerticalAlign="WindowCenter"
                                    Mode="PopupEditForm"></SettingsEditing>
                                <Settings ShowHorizontalScrollBar="True"></Settings>
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="RefName" Width="200px"
                                        Caption="نام کتاب">
                                        <PropertiesTextEdit Width="200px">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="نام کتاب را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="RefAuthor" Width="200px"
                                        Caption="مولف">
                                        <PropertiesTextEdit Width="200px">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                <RequiredField IsRequired="True" ErrorText="نام مولف را وارد نمایید"></RequiredField>
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataMemoColumn VisibleIndex="1" FieldName="RefTopics" Width="300px"
                                        Caption="سرفصل ها">
                                        <PropertiesMemoEdit Width="200px" Height="70px">
                                        </PropertiesMemoEdit>
                                    </dxwgv:GridViewDataMemoColumn>
                                    <dxwgv:GridViewDataMemoColumn VisibleIndex="3" FieldName="Description" Width="300px"
                                        Caption="توضیحات">
                                        <PropertiesMemoEdit Width="200px" Height="70px">
                                        </PropertiesMemoEdit>
                                    </dxwgv:GridViewDataMemoColumn>
                                    <dxwgv:GridViewCommandColumn VisibleIndex="4" Caption=" " ShowClearFilterButton="true">
                                      
                                    </dxwgv:GridViewCommandColumn>
                                </Columns>
                            </TSPControls:CustomAspxDevGridView>
                            <asp:ObjectDataSource runat="server" SelectMethod="SelectByCourseId" ID="ObjdsCourseRefrences"
                                TypeName="TSP.DataManager.CourseRefrenceManager">
                                <SelectParameters>
                                    <asp:Parameter Type="Int32" DefaultValue="-1" Name="CourseId"></asp:Parameter>
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
                            <table class="TableMenu">
                                <tbody>
                                    <tr>
                                        <td>
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
	GridViewCourseRefrences.AddNewRow();
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
                                                ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                Width="25px" EnableTheming="False">
                                                <ClientSideEvents Click="function(s, e) {
		if (GridViewCourseRefrences.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	GridViewCourseRefrences.StartEditRow(GridViewCourseRefrences.GetFocusedRowIndex());
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
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}"></ClientSideEvents>
                                                <HoverStyle BackColor="#FFE0C0">
                                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                </HoverStyle>
                                                <Image Width="25px" Height="25px" Url="~/Images/icons/delete.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td>
                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
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
                <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldCourseRef">
                </dxhf:ASPxHiddenField>
                <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                    AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
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
