<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="CourseAttachments.aspx.cs" Inherits="Employee_Amoozesh_CourseAttachments"
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
    <script language="javascript">

        function SetControlValues() {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'FilePath;Description', SetValue);
        }
        function SetValue(values) {
            var d = values[0];
            if (d != null && d != '') {
                d = d.replace('~/', '');
                d = '../' + d;
            }

            img.SetImageUrl(d);

            desc.SetText(values[1]);

        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server" visible="true">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                [<a class="closeLink" href="#">بستن</a>]</div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table class="TableMenu">
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server"  EnableTheming="False"
                                        EnableViewState="False" Text=" " ToolTip="جدید" AutoPostBack="False" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                        <ClientSideEvents Click="function(s, e) {
	pop.Show();
}" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableClientSideAPI="True" 
                                        EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                        ToolTip="حذف" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                    </TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False" 
                                        EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                        ToolTip="بازگشت" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                            </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <div width="100%">
                <TSPControls:CustomAspxMenuHorizontal ID="MenuCourseDetail" runat="server"  OnItemClick="MenuCourseDetail_ItemClick">
                    <Items>
                        <dxm:MenuItem Name="Course" Text="مشخصات درس">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="CourseDetail" Text="ارتباط با پروانه">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Refrences" Text="منابع درس">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Prerequisite" Text="پیشنیاز ها">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Group" Text="گروه بندی">
                        </dxm:MenuItem>
                        <dxm:MenuItem Name="Attachment" Text="فایل های پیوست" Selected="true">
                        </dxm:MenuItem>
                    </Items>
                </TSPControls:CustomAspxMenuHorizontal>
            </div>
            <br />
            <TSPControls:CustomAspxDevGridView ID="GridViewAttachment" runat="server" 
                 OnPageIndexChanged="GridViewAttachment_PageIndexChanged"
                AutoGenerateColumns="False" KeyFieldName="AttachId" ClientInstanceName="grid"
                Width="100%">
                <Columns>
                    <dxwgv:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="AttachId"
                        Name="AttachId">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataHyperLinkColumn Visible="False" VisibleIndex="0" FieldName="FilePath"
                        Caption="فایل" Name="FilePath">
                        <PropertiesHyperLinkEdit Target="_blank">
                        </PropertiesHyperLinkEdit>
                    </dxwgv:GridViewDataHyperLinkColumn>
                    <dxwgv:GridViewDataImageColumn Visible="False" VisibleIndex="0" FieldName="FilePath"
                        Caption="تصویر" Name="FilePath">
                        <PropertiesImage ImageWidth="75px" ImageHeight="75px">
                        </PropertiesImage>
                    </dxwgv:GridViewDataImageColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="0" Caption="فایل">
                        <DataItemTemplate>
                            <dxe:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="ASPxHyperLink" OnDataBinding="ASPxHyperLink1_DataBinding"
                                NavigateUrl='<%# Bind("FilePath") %>' Target="_blank">
                            </dxe:ASPxHyperLink>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="Description" Width="300px"
                        Caption="توضیحات" Name="Description">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </TSPControls:CustomAspxDevGridView>
            <br />
            <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl1" runat="server" Modal="true" 
                  ClientInstanceName="pop"
                CloseAction="CloseButton" HeaderText="جدید" PopupElementID="btnSearch1" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter">
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        <table>
                            <tbody>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <dxe:ASPxLabel runat="server" Text="فایل" ID="ASPxLabel1">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; text-align: right">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <TSPControls:CustomAspxUploadControl runat="server"
                                                            ID="flp" InputType="Files"    UploadWhenFileChoosed="true" ClientInstanceName="flpc" OnFileUploadComplete="flp_FileUploadComplete">
                                                            <ClientSideEvents  FileUploadComplete="function(s, e) {
	imgEndUploadImgClient.SetVisible(true);
}"></ClientSideEvents>
                                                            
                                                        </TSPControls:CustomAspxUploadControl>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxImage runat="server" ClientVisible="False" ToolTip="تصویر انتخاب شد" ImageUrl="~/Images/icons/button_ok.png"
                                                            ID="imgEndUploadImg" ClientInstanceName="imgEndUploadImgClient">
                                                        </dxe:ASPxImage>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; text-align: right">
                                        <dxe:ASPxLabel runat="server" Text="توضیحات" ID="ASPxLabel2">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td style="vertical-align: top; text-align: right">
                                        <TSPControls:CustomASPXMemo runat="server" Height="26px"  Width="328px" ID="txtDesc"
                                            >
                                            <ValidationSettings>
                                                <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
                                                </ErrorImage>
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </TSPControls:CustomASPXMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <br />
                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text="ذخیره"  ID="btnSave" UseSubmitBehavior="False"
                                             OnClick="btnSave_Click">
                                            <ClientSideEvents Click="function(s, e) {
	pop.Hide();
}"></ClientSideEvents>
                                        </TSPControls:CustomAspxButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
                <HeaderStyle>
                    <Paddings PaddingTop="1px" PaddingRight="6px" PaddingLeft="10px"></Paddings>
                </HeaderStyle>
                <SizeGripImage Height="12px" Width="12px">
                </SizeGripImage>
                <CloseButtonImage Height="17px" Width="17px">
                </CloseButtonImage>
            </TSPControls:CustomASPxPopupControl>
            <asp:HiddenField ID="CourseId" runat="server" Visible="False"></asp:HiddenField>
            <asp:HiddenField ID="PgMode" runat="server" Visible="False"></asp:HiddenField>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                        <table class="TableMenu">
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server"  EnableTheming="False"
                                        EnableViewState="False" Text=" " ToolTip="جدید" AutoPostBack="False" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/new.png" Width="25px" />
                                        <ClientSideEvents Click="function(s, e) {
	pop.Show();
}" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableClientSideAPI="True" 
                                        EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                        ToolTip="حذف" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/delete.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td width="10px" align="center">
                                    <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                    </TSPControls:MenuSeprator>
                                </td>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server" CausesValidation="False" 
                                        EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                        ToolTip="بازگشت" UseSubmitBehavior="False">
                                        <HoverStyle BackColor="#FFE0C0">
                                            <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                        </HoverStyle>
                                        <Image Height="25px" Url="~/Images/icons/Back.png" Width="25px" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
