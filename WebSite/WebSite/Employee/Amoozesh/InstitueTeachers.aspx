<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="InstitueTeachers.aspx.cs" Inherits="Employee_Amoozesh_InstitueTeachers" Title="اساتید مؤسسه" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxuc" %>

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

            <div id="DivReport" runat="server" class="DivErrors" dir="rtl" style="text-align: right"
                visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>


                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                            <tbody>
                                <tr>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew" runat="server" AutoPostBack="False" 
                                            EnableTheming="False" EnableViewState="False" OnClick="btnNew_Click" Text=" "
                                            ToolTip="جدید" UseSubmitBehavior="False" Width="25px">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" AutoPostBack="False" 
                                            EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                            ToolTip="ویرایش" UseSubmitBehavior="False" Width="25px">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" AutoPostBack="False" 
                                            EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                            ToolTip="مشاهده" UseSubmitBehavior="False" Width="25px">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDisActive" runat="server" AutoPostBack="False" 
                                            EnableTheming="False" EnableViewState="False" OnClick="btnDisActive_Click" Text=" "
                                            ToolTip="غیرفعال" UseSubmitBehavior="False" Width="25px">
                                            <ClientSideEvents Click="function(s, e) {
	if (GridViewInsTeacher.GetFocusedRowIndex()&amp;lt;0)
 	{
   		e.processOnServer=false;
   		alert(&amp;quot;ردیفی انتخاب نشده است&amp;quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}
	
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False" 
                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                            ToolTip="بازگشت" UseSubmitBehavior="False">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>

            <TSPControls:CustomAspxMenuHorizontal ID="MenuInstitue" runat="server"  OnItemClick="ASPxMenu1_ItemClick" >
                <Items>
                    <dxm:MenuItem Name="BasicInfo" Text="اطلاعات پایه">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Manager" Text="هیئت اجرایی">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Activity" Text="زمینه های فعالیت">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Facilities" Text="امکانات و تجهیزات">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="InsTeacher" Text="اساتید" Selected="true">
                    </dxm:MenuItem>
                </Items>
              
            </TSPControls:CustomAspxMenuHorizontal>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelInsTeacher" HHeaderText="اساتید موسسه" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <TSPControls:CustomAspxDevGridView ID="GridViewInsTeacher" runat="server" AutoGenerateColumns="False"
                            OnHtmlDataCellPrepared="GridViewInsTeacher_HtmlDataCellPrepared" OnAutoFilterCellEditorInitialize="GridViewInsTeacher_AutoFilterCellEditorInitialize"
                              EnableViewState="False"
                            Width="100%" DataSourceID="ObjdsInsTeacher" KeyFieldName="InsTeacherId" ClientInstanceName="GridViewInsTeacher">
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="نام" FieldName="Name" VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="نام خانوادگی" FieldName="Family" VisibleIndex="1">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="تاریخ شروع" FieldName="StartDate" VisibleIndex="2" Width="100px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="تاریخ پایان" FieldName="EndDate" VisibleIndex="3" Width="100px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="4" Width="150px">
                                </dxwgv:GridViewDataTextColumn>

                                <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveName" VisibleIndex="6">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="8" Width="30px" ShowClearFilterButton="true"> 
                                </dxwgv:GridViewCommandColumn>
                            </Columns>
                          
                        </TSPControls:CustomAspxDevGridView>


                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanel>
            <br />



            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu3" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                            <tbody>
                                <tr>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew2" runat="server" AutoPostBack="False" 
                                            EnableTheming="False" EnableViewState="False" OnClick="btnNew_Click" Text=" "
                                            ToolTip="جدید" UseSubmitBehavior="False" Width="25px">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" AutoPostBack="False" 
                                            EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                            ToolTip="ویرایش" UseSubmitBehavior="False" Width="25px">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td dir="ltr" >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server" AutoPostBack="False" 
                                            EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                            ToolTip="مشاهده" UseSubmitBehavior="False" Width="25px">
                                            <ClientSideEvents Click="function(s, e) {
	
	
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDisActive2" runat="server" AutoPostBack="False" 
                                            EnableTheming="False" EnableViewState="False" OnClick="btnDisActive_Click" Text=" "
                                            ToolTip="غیرفعال" UseSubmitBehavior="False" Width="25px">
                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewInsTeacher.GetFocusedRowIndex()&amp;lt;0)
 	{
   		e.processOnServer=false;
   		alert(&amp;quot;ردیفی انتخاب نشده است&amp;quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به غیرفعال کردن این ردیف هستید؟');
}
	
}" />
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/disactive.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                    <td >
                                        <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack2" runat="server" CausesValidation="False" 
                                            EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                            ToolTip="بازگشت" UseSubmitBehavior="False">
                                            <HoverStyle BackColor="#FFE0C0">
                                                <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                            </HoverStyle>
                                            <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <dxhf:ASPxHiddenField ID="HiddenFieldInsTeacher" runat="server">
            </dxhf:ASPxHiddenField>

            <asp:ObjectDataSource ID="ObjdsInsTeacher" runat="server" SelectMethod="SelectByInstitue"
                TypeName="TSP.DataManager.InstitueTeachersManager" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="InsId" Type="Int32" />
                    <asp:Parameter DefaultValue="0" Name="InActive" Type="Int32" />
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

</asp:Content>

