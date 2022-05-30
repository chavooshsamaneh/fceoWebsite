<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="ResearchActivity.aspx.cs" Inherits="Employee_Management_ResearchActivity"
    Title="مدیریت مقالات و تحقیقات" %>

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
     
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                
                    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                            href="#"><span style="color: #000000">ب</span>ستن</a>]</div>
                     <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


 
                                        <table style="border-collapse: collapse; background-color: transparent" dir="rtl"
                                            cellpadding="0">
                                            <tbody>
                                                <tr>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                                             EnableTheming="False" ToolTip="جدید" ID="BtnNew"
                                                            EnableViewState="False">
                                                            <ClientSideEvents Click="function(s, e) {
	GridViewResearchAct.AddNewRow();
}"></ClientSideEvents>
                                                            <Image  Url="~/Images/icons/new.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                                             Width="25px" EnableTheming="False" ToolTip="ویرایش"
                                                            ID="btnEdit" EnableViewState="False">
                                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewResearchAct.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	GridViewResearchAct.StartEditRow(GridViewResearchAct.GetFocusedRowIndex());
}	
}"></ClientSideEvents>
                                                            <Image  Url="~/Images/icons/edit.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                                            Text=" "  EnableTheming="False" ToolTip="حذف"
                                                            ID="btnDelete" EnableViewState="False" OnClick="btnDelete_Click">
                                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewResearchAct.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}
}"></ClientSideEvents>
                                                            <Image  Url="~/Images/icons/delete.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                            EnableTheming="False" ToolTip="بازگشت" ID="btnBack" EnableViewState="False" OnClick="btnBack_Click">
                                                            <Image  Url="~/Images/icons/Back.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table> </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                                 
                        <br />
                        <TSPControls:CustomAspxDevGridView ID="GridViewResearchAct" runat="server" Width="100%"
                              EnableViewState="False"
                            OnRowUpdating="GridViewResearchAct_RowUpdating" KeyFieldName="RaId" OnRowInserting="GridViewResearchAct_RowInserting"
                            DataSourceID="ObjdsResearchActivity" ClientInstanceName="GridViewResearchAct"
                            AutoGenerateColumns="False">
                           
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="سابقه پژوهشی" FieldName="RaName" VisibleIndex="0"
                                    Width="300px">
                                    <PropertiesTextEdit Width="300px">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom">
                                            <RequiredField IsRequired="True" ErrorText="نام سابقه پژوهشی راوارد نمایید."></RequiredField>
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="امتیاز مربوطه" FieldName="Grade" VisibleIndex="1">
                                    <PropertiesTextEdit Width="300px">
                                        <ValidationSettings Display="Dynamic">
                                            <RequiredField ErrorText=""></RequiredField>
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn VisibleIndex="2" Width="1px">
                                    <EditFormSettings Visible="False"></EditFormSettings>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataMemoColumn Caption="توضیحات" FieldName="Description" VisibleIndex="3"
                                    Width="200px">
                                    <PropertiesMemoEdit Width="300px">
                                    </PropertiesMemoEdit>
                                </dxwgv:GridViewDataMemoColumn>
                            </Columns>
                            <SettingsEditing Mode="PopupEditForm" PopupEditFormModal="True" EditFormColumnCount="1"
                                PopupEditFormHorizontalAlign="WindowCenter" PopupEditFormVerticalAlign="WindowCenter" />
                            
                        </TSPControls:CustomAspxDevGridView>
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
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                                             EnableTheming="False" ToolTip="جدید" ID="btnNew2"
                                                            EnableViewState="False">
                                                            <ClientSideEvents Click="function(s, e) {
	GridViewResearchAct.AddNewRow();
}"></ClientSideEvents>
                                                            <Image  Url="~/Images/icons/new.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text=" "
                                                             Width="25px" EnableTheming="False" ToolTip="ویرایش"
                                                            ID="btnEdit2" EnableViewState="False">
                                                            <ClientSideEvents Click="function(s, e) {
			if (GridViewResearchAct.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	GridViewResearchAct.StartEditRow(GridViewResearchAct.GetFocusedRowIndex());
}	
}"></ClientSideEvents>
                                                            <Image  Url="~/Images/icons/edit.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" EnableClientSideAPI="True"
                                                            Text=" "  EnableTheming="False" ToolTip="حذف"
                                                            ID="btnDelete2" EnableViewState="False" OnClick="btnDelete_Click">
                                                            <ClientSideEvents Click="function(s, e) {
		if (GridViewResearchAct.GetFocusedRowIndex()&lt;0)
 	{
   		e.processOnServer=false;
   		alert(&quot;ردیفی انتخاب نشده است&quot;);
 	}
else
{
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}
}"></ClientSideEvents>
                                                            <Image  Url="~/Images/icons/delete.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" UseSubmitBehavior="False" Text=" " 
                                                            EnableTheming="False" ToolTip="بازگشت" ID="btnBack2" EnableViewState="False"
                                                            OnClick="btnBack_Click">
                                                            <Image  Url="~/Images/icons/Back.png">
                                                            </Image>
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"></Border>
                                                            </HoverStyle>
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                   </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                <asp:ObjectDataSource ID="ObjdsResearchActivity" runat="server" UpdateMethod="Update"
                    TypeName="TSP.DataManager.ResearchActivityManager" SelectMethod="GetData" InsertMethod="Insert"
                    DeleteMethod="Delete">
                   
                </asp:ObjectDataSource>
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
