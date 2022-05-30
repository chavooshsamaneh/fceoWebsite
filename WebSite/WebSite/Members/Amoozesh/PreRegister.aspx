<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="PreRegister.aspx.cs" Inherits="Members_Amoozesh_PreRegister"
    Title="پیش ثبت نام دوره" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <asp:updatepanel id="UpdatePanel1" runat="server">
    <ContentTemplate>        
            <div id="DivReport" runat="server" class="DivErrors" style="text-align: right" visible="true">
                <asp:label id="LabelWarning" runat="server" text="Label"></asp:label>
                [<a class="closeLink" href="#"><span style="color: #000000">ب</span>ستن</a>]</div>
                       <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                <table cellpadding="0" dir="rtl" style="border-collapse: collapse; background-color: transparent">
                                    <tbody>
                                        <tr>
                                            <td align="right">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnNew_Click" Text=" "
                                                    ToolTip="جدید" UseSubmitBehavior="False">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnEdit_Click" Text=" "
                                                    ToolTip="ویرایش" Width="25px" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewPreRegister.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
	
	
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnView_Click" Text=" "
                                                    ToolTip="مشاهده" Width="25px" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewPreRegister.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableClientSideAPI="True" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                                    ToolTip="حذف" UseSubmitBehavior="False">
                                                         <ClientSideEvents Click="function(s, e) {
if (GridViewPreRegister.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{
    e.processOnServer=confirm('آیا مطمئن به حذف این ردیف هستید؟')		
	}
}" />
                                                   
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                          </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewPreRegister" runat="server" AutoGenerateColumns="False"
                  EnableViewState="False"
                KeyFieldName="PRegisterId" Width="100%" ClientInstanceName="GridViewPreRegister"
                DataSourceID="ObjdsPreRegister" OnAutoFilterCellEditorInitialize="GridViewPreRegister_AutoFilterCellEditorInitialize"
                OnHtmlDataCellPrepared="GridViewPreRegister_HtmlDataCellPrepared">
                
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="واحد درسی" FieldName="CrsName" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="مؤسسه" FieldName="InsName" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="استاد" FieldName="TeName" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="تاریخ" FieldName="RegisteringDate" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="5" width="30px" ShowClearFilterButton="true">
                    </dxwgv:GridViewCommandColumn>
                </Columns>
              
            </TSPControls:CustomAspxDevGridView>
            <br />         
                      <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>
  
                                <table >
                                    <tbody>
                                        <tr>
                                            <td align="right">
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnNew2" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="جدید" UseSubmitBehavior="False" OnClick="btnNew_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnEdit2" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="ویرایش" Width="25px"
                                                    UseSubmitBehavior="False" OnClick="btnEdit_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewPreRegister.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server" AutoPostBack="False" 
                                                    EnableTheming="False" EnableViewState="False" Text=" " ToolTip="مشاهده" Width="25px"
                                                    UseSubmitBehavior="False" OnClick="btnView_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	if (GridViewPreRegister.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/view.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableClientSideAPI="True" 
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                                    ToolTip="حذف" UseSubmitBehavior="False">
                                                     <ClientSideEvents Click="function(s, e) {
if (GridViewPreRegister.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}	
	else
	{
    e.processOnServer=confirm('آیا مطمئن به حذف این ردیف هستید؟')		
	}
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                           </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            <asp:objectdatasource id="ObjdsPreRegister" runat="server" selectmethod="SelectByMeId"
                typename="TSP.DataManager.PreRegisterManager">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="MeId" Type="Int32" />
            </SelectParameters>
        </asp:objectdatasource>
        </div>
        </ContentTemplate> </asp:updatepanel>
        <asp:modalupdateprogress id="ModalUpdateProgress2" runat="server" associatedupdatepanelid="UpdatePanel1"
            backgroundcssclass="modalProgressGreyBackground" displayafter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                لطفا صبر نمایید
                <img align="middle" src="../../Image/indicator.gif" />
            </div>
        </ProgressTemplate>
    </asp:modalupdateprogress>
</asp:Content>
