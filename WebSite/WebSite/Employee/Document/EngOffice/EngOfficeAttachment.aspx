<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="EngOfficeAttachment.aspx.cs" Inherits="Employee_Document_EngOffice_EngOfficeAttachment"
    Title="فایل های پیوست" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
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
            <div id="DivReport" runat="server" class="DivErrors" style="text-align: right" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                                    <table >
                                        <tr>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" EnableTheming="False"
                                                    EnableViewState="False" Text=" " ToolTip="جدید" AutoPostBack="False" UseSubmitBehavior="False">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/new.png"  />
                                                    <ClientSideEvents Click="function(s, e) {
	pop.Show();
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableClientSideAPI="True"
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                                    ToolTip="حذف" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
	 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/delete.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1"></TSPControls:MenuSeprator>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False"
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                    ToolTip="بازگشت" UseSubmitBehavior="False">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/Back.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت دفاتر"
                                                    CausesValidation="False" ID="btnBackToManagment" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/BakToManagment.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </table>
                              
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomAspxMenuHorizontal ID="ASPxMenu1" runat="server" OnItemClick="ASPxMenu1_ItemClick"
               >
                <Items>
                    <dxm:MenuItem Name="EngOffice" Text="مشخصات دفتر">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Member" Text="اعضای دفتر">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Job" Text="سوابق کاری">
                    </dxm:MenuItem>
                    <dxm:MenuItem Name="Attach" Text="مستندات" Selected="true">
                    </dxm:MenuItem>
                </Items>
              
            </TSPControls:CustomAspxMenuHorizontal>

            <br />
            <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
                Width="100%"  
                KeyFieldName="AttachId" ClientInstanceName="grid" DataSourceID="ObjectDataSource1"
                OnHtmlRowPrepared="CustomAspxDevGridView1_HtmlRowPrepared">

                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="AttachId" Name="AttachId" Visible="False"
                        VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="فایل" VisibleIndex="0">
                        <DataItemTemplate>
                            <dxe:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="ASPxHyperLink" 
                                Target="_blank" NavigateUrl='<%# Bind("FilePath") %>' OnDataBinding="ASPxHyperLink1_DataBinding">
                            </dxe:ASPxHyperLink>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataImageColumn Caption="تصویر" FieldName="FilePath" Name="FilePath"
                        Visible="False" VisibleIndex="0">
                        <PropertiesImage ImageHeight="75px" ImageWidth="75px">
                        </PropertiesImage>
                    </dxwgv:GridViewDataImageColumn>
                    <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" Name="Description"
                        VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="RefTable" Visible="False" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>

            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl1" runat="server" ClientInstanceName="pop"
                HeaderText="جدید"  PopupElementID="btnSearch1"
                >
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        <table>
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="فایل">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxUploadControl ID="flp" runat="server" ClientInstanceName="flpc"
                                                        InputType="Files" OnFileUploadComplete="flp_FileUploadComplete" UploadWhenFileChoosed="true">
                                                        <ClientSideEvents FileUploadComplete="function(s, e) {
	imgEndUploadImgClient.SetVisible(true);
}" />
                                                    </TSPControls:CustomAspxUploadControl>
                                                </td>
                                                <td>
                                                    <dxe:ASPxImage ID="imgEndUploadImg" runat="server" ClientInstanceName="imgEndUploadImgClient"
                                                        ClientVisible="False" ImageUrl="~/Images/icons/button_ok.png" ToolTip="تصویر انتخاب شد">
                                                    </dxe:ASPxImage>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="توضیحات">
                                    </dxe:ASPxLabel>
                                </td>
                                <td align="right" valign="top">
                                    <TSPControls:CustomASPXMemo ID="txtDesc" runat="server" Height="26px" 
                                        >
                                        <ValidationSettings>
                                            
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </TSPControls:CustomASPXMemo>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <br />
                                    <TSPControls:CustomAspxButton  ID="btnSave" runat="server" Text="ذخیره" 
                                         OnClick="btnSave_Click" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	pop.Hide();
}" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
                <HeaderStyle>
                    <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                </HeaderStyle>
                <SizeGripImage Height="12px" Width="12px" />
                <CloseButtonImage Height="17px" Width="17px" />
            </TSPControls:CustomASPxPopupControl>
            <asp:HiddenField ID="EngOfficeId" runat="server" Visible="False" />
            <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
            <asp:HiddenField ID="EngFileId" runat="server" Visible="False" />
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
                SelectMethod="FindByTablePrimaryKey" TypeName="TSP.DataManager.AttachmentsManager">

                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="ttid" Type="Int32" />
                    <asp:Parameter DefaultValue="-1" Name="code" Type="Int32" />
                </SelectParameters>

            </asp:ObjectDataSource>

            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                                    <table >
                                        <tr>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server" EnableTheming="False"
                                                    EnableViewState="False" Text=" " ToolTip="جدید" AutoPostBack="False" UseSubmitBehavior="False">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/new.png"  />
                                                    <ClientSideEvents Click="function(s, e) {
	pop.Show();
}" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableClientSideAPI="True"
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                                    ToolTip="حذف" UseSubmitBehavior="False">
                                                    <ClientSideEvents Click="function(s, e) {
	 
if (grid.GetFocusedRowIndex()&lt;0)
 {
   e.processOnServer=false;
   alert(&quot;ردیفی انتخاب نشده است&quot;);
 }
 else
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/delete.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td width="10px" align="center">
                                                <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2"></TSPControls:MenuSeprator>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server" CausesValidation="False"
                                                    EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                    ToolTip="بازگشت" UseSubmitBehavior="False">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border bordercolor="Gray" borderstyle="Outset" borderwidth="1px" />
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/Back.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td >
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="بازگشت به مدیریت دفاتر"
                                                    CausesValidation="False" ID="ASPxButton2" UseSubmitBehavior="False" EnableViewState="False"
                                                    EnableTheming="False" OnClick="btnBackToManagment_Click">
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/BakToManagment.png">
                                                    </Image>
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
</asp:Content>
