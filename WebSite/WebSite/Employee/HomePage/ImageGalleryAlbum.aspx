<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ImageGalleryAlbum.aspx.cs" Inherits="Employee_HomePage_ImageGalleryAlbums"
    Title="مديريت گزارش تصویری" %>

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

    <script language="javascript" type="text/javascript">
        function ShowMessage(Message) {
            document.getElementById("<%=DivReport.ClientID%>").style.visibility = 'visible';
            document.getElementById("<%=DivReport.ClientID%>").style.display = 'inline';
            document.getElementById('<%=LabelWarning.ClientID%>').innerHTML = Message;
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                            EnableTheming="False" ToolTip="جدید" ID="BtnNew"
                                            EnableViewState="False">
                                            <ClientSideEvents Click="function(s, e) {
	GridViewAlbum.AddNewRow();
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                            Width="25px" EnableTheming="False" ToolTip="ویرایش"
                                            ID="btnEdit" EnableViewState="False">
                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewAlbum.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		ShowMessage(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	GridViewAlbum.StartEditRow(GridViewAlbum.GetFocusedRowIndex());
}	
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                            Text=" " EnableTheming="False" ToolTip="غیر فعال"
                                            ID="btnInActive" EnableViewState="False" OnClick="btnInActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewAlbum.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		ShowMessage(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/Disactive.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                            Text=" " EnableTheming="False" ToolTip="فعال"
                                            ID="btnActive" EnableViewState="False" OnClick="btnActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewAlbum.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		ShowMessage(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/Active.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                            Text=" " EnableTheming="False" ToolTip="تصاویر"
                                            ID="btnImages" EnableViewState="False" OnClick="btnImages_Click">
                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewAlbum.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		ShowMessage(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/preview.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <TSPControls:CustomAspxDevGridView ID="GridViewAlbum" Width="100%" runat="server"
                AutoGenerateColumns="False"
                DataSourceID="ObjdsAlbums" ClientInstanceName="GridViewAlbum" KeyFieldName="AlbumId"
                OnRowInserting="GridViewAlbum_RowInserting" OnRowUpdating="GridViewAlbum_RowUpdating"
                RightToLeft="True">
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="نام آلبوم" FieldName="AlbumName" VisibleIndex="0"
                        Width="65%">
                        <PropertiesTextEdit>
                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="نام وارد نشده است"></RequiredField>
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                       <dxwgv:GridViewDataTextColumn Caption="تاریخ اعتبار" FieldName="ValidDate" VisibleIndex="0"
                        Width="20%">
                        <PropertiesTextEdit>
                            <ValidationSettings Display="Dynamic" SetFocusOnError="True" CausesValidation="True" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                <RequiredField IsRequired="True" ErrorText="تاریخ اعتبار وارد نشده است"></RequiredField>
                                <RegularExpression ValidationExpression="\d{4}\/(0[1-9]|1[012])\/(0[1-9]|[12][0-9]|3[01])*" ErrorText="فرمت تاریخ درست نیست" />
                            </ValidationSettings>
                           <MaskSettings Mask="13--/--/--" />
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="1"
                        Width="9%">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " Name=" " VisibleIndex="3" Width="6%" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="AlbumId" Visible="False" VisibleIndex="2">
                        <EditFormSettings Visible="False"></EditFormSettings>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <ClientSideEvents EndCallback="function(s, e) {
if(s.cpMessage!='')
{
 ShowMessage(s.cpMessage);
}
}" />
                <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" PopupEditFormModal="True"
                    PopupEditFormVerticalAlign="WindowCenter" />

            </TSPControls:CustomAspxDevGridView>
             <asp:ObjectDataSource ID="ObjdsAlbums" runat="server" TypeName="TSP.DataManager.GalleryAlbumsManager"
                        SelectMethod="GetData"></asp:ObjectDataSource>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table >
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                            EnableTheming="False" ToolTip="جدید" ID="btnNew2"
                                            EnableViewState="False">
                                            <ClientSideEvents Click="function(s, e) {
		GridViewAlbum.AddNewRow();
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/new.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                            Width="25px" EnableTheming="False" ToolTip="ویرایش"
                                            ID="btnEdit2" EnableViewState="False">
                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewAlbum.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		ShowMessage(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	GridViewAlbum.StartEditRow(GridViewAlbum.GetFocusedRowIndex());
}	
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/edit.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                            Text=" " EnableTheming="False" ToolTip="غیر فعال"
                                            ID="btnInActive2" EnableViewState="False" OnClick="btnInActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewAlbum.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		ShowMessage(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به غیر فعال کردن این ردیف هستید؟');
}
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/Disactive.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                            Text=" " EnableTheming="False" ToolTip="فعال"
                                            ID="btnActive2" EnableViewState="False" OnClick="btnActive_Click">
                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewAlbum.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		ShowMessage(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به فعال کردن این ردیف هستید؟');
}
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/Active.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                            Text=" " EnableTheming="False" ToolTip="تصاویر"
                                            ID="btnImages2" EnableViewState="False" OnClick="btnImages_Click">
                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewAlbum.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		ShowMessage(&quot;ردیفی انتخاب نشده است&quot;);
 	}
}"></ClientSideEvents>
                                            <Image Url="~/Images/icons/preview.png">
                                            </Image>
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px"></border>
                                            </HoverStyle>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" DisplayAfter="0"
                BackgroundCssClass="modalProgressGreyBackground" AssociatedUpdatePanelID="UpdatePanel1">
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
