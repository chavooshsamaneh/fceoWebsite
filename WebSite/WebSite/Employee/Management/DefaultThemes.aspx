<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="DefaultThemes.aspx.cs" Inherits="Employee_Management_DefaultThemes"
    Title="مدیریت طرح های پیش فرض" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls.FormCreatorComponents"
    TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
              <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dx:PanelContent>


 
                                        <table dir="rtl">
                                            <tr>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnnew" runat="server"  EnableTheming="False"
                                                        EnableViewState="False" ToolTip="جدید" OnClick="btnnew_Click">
                                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnedit" runat="server"  EnableTheming="False"
                                                        EnableViewState="False" ToolTip="ویرایش" OnClick="btnedit_Click">
                                                        <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnview" runat="server"  EnableTheming="False"
                                                        EnableViewState="False" ToolTip="مشاهده" OnClick="btnview_Click">
                                                        <Image Height="25px" Url="~/Images/icons/view.png" Width="25px">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btndel" runat="server"  EnableTheming="False"
                                                        EnableViewState="False" ToolTip="حذف" OnClick="btndel_Click">
                                                        <Image Height="25px" Url="~/Images/icons/Delete.png" Width="25px">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <ClientSideEvents Click="function(s, e) {
	if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;قالبی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به حذف این قالب هستید؟');
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </table>
                                   </dx:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                            <br />
                            <TSPControls:CustomAspxDevGridView ID="GridViewDefaultTheme" runat="server" AutoGenerateColumns="False"
                                ClientInstanceName="grid"  
                                DataSourceID="ObjectDataSource1" EnableViewState="False" KeyFieldName="DtId"
                                Width="100%" RightToLeft="True" OnAutoFilterCellEditorInitialize="GridViewDefaultTheme_AutoFilterCellEditorInitialize"
                                OnHtmlDataCellPrepared="GridViewDefaultTheme_HtmlDataCellPrepared">
                                
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="نوع قالب" FieldName="TypeName" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="نام قالب" FieldName="DtName" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="تاریخ" FieldName="Date" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="2" Width="30px" ShowClearFilterButton="true">
                                       
                                    </dxwgv:GridViewCommandColumn>
                                </Columns>
                                <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" PopupEditFormHorizontalAlign="WindowCenter"
                                    PopupEditFormModal="True" PopupEditFormVerticalAlign="WindowCenter" />
                                
                            </TSPControls:CustomAspxDevGridView>
                            <br />
                       <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dx:PanelContent>


 
                                        <table dir="rtl">
                                            <tr>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnnew2" runat="server"  EnableTheming="False"
                                                        EnableViewState="False" ToolTip="جدید" OnClick="btnnew_Click">
                                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnedit2" runat="server"  EnableTheming="False"
                                                        EnableViewState="False" ToolTip="ویرایش" OnClick="btnedit_Click">
                                                        <Image Height="25px" Url="~/Images/icons/edit.png" Width="25px">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnview2" runat="server"  EnableTheming="False"
                                                        EnableViewState="False" ToolTip="مشاهده" OnClick="btnview_Click">
                                                        <Image Height="25px" Url="~/Images/icons/view.png" Width="25px">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td align="right" valign="top">
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btndel2" runat="server"  EnableTheming="False"
                                                        EnableViewState="False" ToolTip="حذف" OnClick="btndel_Click">
                                                        <Image Height="25px" Url="~/Images/icons/Delete.png" Width="25px">
                                                        </Image>
                                                        <HoverStyle BackColor="#FFE0C0">
                                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                        </HoverStyle>
                                                        <ClientSideEvents Click="function(s, e) {
		if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;قالبی انتخاب نشده است&quot;);
 }
 else
  e.processOnServer= confirm('آیا مطمئن به حذف این قالب هستید؟');
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </table>
                                  </dx:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                            <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                BackgroundCssClass="modalProgressGreyBackground" DisplayAfter="0">
                                <ProgressTemplate>
                                    <div class="modalPopup">
                                        لطفا صبر نمایید
                                        <img alt="" align="middle" src="../../Image/indicator.gif" />
                                    </div>
                                </ProgressTemplate>
                            </asp:ModalUpdateProgress>
                       
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
        TypeName="TSP.DataManager.DefaultThemesManager" OldValuesParameterFormatString="original_{0}">
    </asp:ObjectDataSource>
</asp:Content>
