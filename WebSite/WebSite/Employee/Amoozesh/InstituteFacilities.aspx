<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="InstituteFacilities.aspx.cs" Inherits="Employee_Amoozesh_InstituteFacilities"
    Title="امکانات و تجهیزات موسسه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink" href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>



                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید" ID="btnNew" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnNew_Click">
                                            <ClientSideEvents Click="function(s, e) {
	//GridViewFacility.AddNewRow();
}"></ClientSideEvents>

                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>

                                            <Image  Url="~/Images/icons/new.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش" Width="25px" ID="btnEdit" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewFacility.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	
}"></ClientSideEvents>

                                            <HoverStyle BackColor="#FFE0C0">
                                                <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                            </HoverStyle>

                                            <Image  Url="~/Images/icons/edit.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال" ID="btnDelete" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnDelete_Click">
                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewFacility.GetFocusedRowIndex()&lt;0)
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

                                            <Image  Url="~/Images/icons/disactive.png"></Image>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td>
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت" ID="btnBack" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
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

             <TSPControls:CustomAspxMenuHorizontal ID="MenuInstitue" runat="server"   OnItemClick="ASPxMenu1_ItemClick">
                        <Items>
                            <dxm:MenuItem Text="اطلاعات پایه" Name="MainInfo">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="هیئت اجرایی" Name="Manager">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="زمینه های فعالیت" Name="Activity">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="امکانات و تجهیزات" Name="Facility" Selected="true">
                            </dxm:MenuItem>
                            <dxm:MenuItem Text="اساتید" Name="InsTeacher">
                            </dxm:MenuItem>
                        </Items>
                    </TSPControls:CustomAspxMenuHorizontal>

            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelFacility" HeaderText="امکانات و تجهیزات" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>



                        <TSPControls:CustomAspxDevGridView ID="GridViewFacility" Width="100%" runat="server" 
                             EnableViewState="False" AutoGenerateColumns="False" ClientInstanceName="GridViewFacility" DataSourceID="ObjdsInstitueFacility" KeyFieldName="InsFacilityId" OnRowInserting="GridViewFacility_RowInserting" OnRowUpdating="GridViewFacility_RowUpdating" OnHtmlRowPrepared="GridViewFacility_HtmlRowPrepared">

                            <Columns>
                                <dxwgv:GridViewDataCheckColumn Caption="نوع" FieldName="IsEquipment" VisibleIndex="0"
                                    Width="150px">
                                    <DataItemTemplate>
                                        <dxe:ASPxLabel ID="lblFacilityType" runat="server" Text="ASPxLabel" ></dxe:ASPxLabel>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataCheckColumn>
                                <dxwgv:GridViewDataTextColumn Caption="تجهیزات/فضای آموزشی" FieldName="FacilityName" VisibleIndex="1" Width="200px">
                                    <PropertiesTextEdit Width="200px">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                            <RequiredField IsRequired="True" ErrorText="نام فضای آموزشی را وارد نمایید"></RequiredField>
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="تعداد/ظرفیت" FieldName="Capacity" VisibleIndex="2" Width="50px">
                                    <PropertiesTextEdit Width="200px">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                            <RequiredField IsRequired="True" ErrorText="ظرفیت فضای آموزشی را وارد نمایید"></RequiredField>

                                            <RegularExpression ErrorText="ظرفیت فضای آموزشی عدد صحیح مثبت می باشد."></RegularExpression>
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>

                                <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="4" Width="200px">
                                    <PropertiesTextEdit Width="200px"></PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true">
                                </dxwgv:GridViewCommandColumn>
                            </Columns>
                            <SettingsEditing Mode="PopupEditForm" PopupEditFormModal="True" />
                        </TSPControls:CustomAspxDevGridView>

                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />

            <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                <tbody>
                    <tr>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="جدید" ID="btnNew2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnNew_Click">
                                <ClientSideEvents Click="function(s, e) {
	//GridViewFacility.AddNewRow();
}"></ClientSideEvents>

                                <HoverStyle BackColor="#FFE0C0">
                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                </HoverStyle>

                                <Image  Url="~/Images/icons/new.png"></Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ویرایش" Width="25px" ID="btnEdit2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnEdit_Click">
                                <ClientSideEvents Click="function(s, e) {
			if (GridViewTeacherLicence.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	
}"></ClientSideEvents>

                                <HoverStyle BackColor="#FFE0C0">
                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                </HoverStyle>

                                <Image  Url="~/Images/icons/edit.png"></Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="غیرفعال" ID="btnDelete2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnDelete_Click">
                                <ClientSideEvents Click="function(s, e) {
		if (GridViewFacility.GetFocusedRowIndex()&lt;0)
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

                                <Image  Url="~/Images/icons/disactive.png"></Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td>
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بازگشت" ID="btnBack2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" OnClick="btnBack_Click">
                                <HoverStyle BackColor="#FFE0C0">
                                    <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                </HoverStyle>

                                <Image  Url="~/Images/icons/Back.png"></Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </tbody>
            </table>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField runat="server" ID="HiddenFieldFacility"></dxhf:ASPxHiddenField>

            <asp:ObjectDataSource ID="ObjdsInstitueFacility" runat="server" SelectMethod="FindByInstitueId" TypeName="TSP.DataManager.InstitueFacilityManager">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="InsId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" BackgroundCssClass="modalProgressGreyBackground"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                            <img src="../../Image/indicator.gif" align="middle" />
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>

</asp:Content>
