<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="InstitueActivity.aspx.cs" Inherits="Employee_Amoozesh_InstitueActivity"
    Title="زمینه های فعالیت موسسه" %>

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
    <div style="width: 100%" align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="width: 100%" align="center">
                    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                        [<a class="closeLink" href="#"><span style="color: #000000">ب</span>ستن</a>]
                    </div>

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
                                                    ID="BtnNew" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	GridViewInsActivity.AddNewRow();
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                    Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewInsActivity.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	GridViewInsActivity.StartEditRow(GridViewInsActivity.GetFocusedRowIndex());
}	
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/edit.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال"
                                                    ID="btnDelete" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnDelete_Click">
                                                    <ClientSideEvents Click="function(s, e) {
		if (GridViewInsActivity.GetFocusedRowIndex()&lt;0)
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
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                    ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnBack_Click">
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
                    <TSPControls:CustomAspxMenuHorizontal ID="MenuInstitue" runat="server"   OnItemClick="ASPxMenu1_ItemClick">
                        <Items>
                            <dxm:MenuItem Text="اطلاعات پایه" Name="MainInfo">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="هیئت اجرایی" Name="Manager">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="زمینه های فعالیت" Name="Activity" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="امکانات و تجهیزات" Name="Facility">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="اساتید" Name="InsTeacher">
                            </dxm:MenuItem>
                        </Items>
                    </TSPControls:CustomAspxMenuHorizontal>

                    <br />
                    <TSPControls:CustomASPxRoundPanel ID="RoundPanelInsActivity" HeaderText="زمینه های فعالیت مؤسسه" runat="server"
                        Width="100%">
                        <PanelCollection>
                            <dxp:PanelContent>


                                <TSPControls:CustomAspxDevGridView Width="100%" runat="server"
                                    EnableViewState="False" ID="GridViewInsActivity" DataSourceID="ObjdsInsActivity"
                                    KeyFieldName="InsActId" AutoGenerateColumns="False" ClientInstanceName="GridViewInsActivity"
                                    OnRowInserting="GridViewInsActivity_RowInserting"
                                    OnRowUpdating="GridViewInsActivity_RowUpdating" OnRowValidating="GridViewInsActivity_RowValidating">
                                    <SettingsEditing EditFormColumnCount="1" PopupEditFormModal="True" Mode="PopupEditForm">
                                    </SettingsEditing>

                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="InsActName" Width="200px"
                                            Caption="زمینه فعالیت">
                                            <PropertiesTextEdit Width="200px">
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                                    <RequiredField ErrorText="زمینه فعالیت موسسه را وارد نمایید"></RequiredField>
                                                </ValidationSettings>
                                            </PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataMemoColumn VisibleIndex="2" FieldName="Description" Width="200px"
                                            Caption="توضیحات">
                                            <PropertiesMemoEdit Width="200px"></PropertiesMemoEdit>
                                        </dxwgv:GridViewDataMemoColumn>
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
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید"
                                                    ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
	GridViewInsActivity.AddNewRow();
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/new.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش"
                                                    Width="25px" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False">
                                                    <ClientSideEvents Click="function(s, e) {
			if (GridViewInsActivity.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	GridViewInsActivity.StartEditRow(GridViewInsActivity.GetFocusedRowIndex());
}	
}"></ClientSideEvents>
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/edit.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال"
                                                    ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnDelete_Click">
                                                    <ClientSideEvents Click="function(s, e) {
		if (GridViewInsActivity.GetFocusedRowIndex()&lt;0)
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
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت"
                                                    ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                                    OnClick="btnBack_Click">
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
                    <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldInsActivity">
                    </dxhf:ASPxHiddenField>

                    <asp:ObjectDataSource ID="ObjdsInsActivity" runat="server" SelectMethod="SelectByInstitue"
                        TypeName="TSP.DataManager.InstitueActivityManager" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:Parameter Type="Int32" DefaultValue="-1" Name="InsId"></asp:Parameter>
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
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
