<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="AgentAttachments.aspx.cs" Inherits="Accounting_Users_AgentAttachments" Title="پیوست" %>


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
        <div id="DivReport" runat="server" class="DivErrors" style="text-align: right" visible="true">
            <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                href="#">بستن</a>]
        </div>
        <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server">
            <PanelCollection>
                <dxp:PanelContent>

                    <table>
                        <tr>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew" runat="server" EnableTheming="False"
                                    EnableViewState="False" Text=" " ToolTip="جدید" AutoPostBack="False" UseSubmitBehavior="False">
                       
                                    <image  url="~/Images/icons/new.png"  />
                                    <ClientSideEvents Click="function(s, e) {
	pop.Show();
}" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView" runat="server" AutoPostBack="False" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False"
                                    Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	popview.Show();
	SetControlValues();
		
}" />
                              
                                    <image  url="~/Images/icons/view.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete" runat="server" EnableClientSideAPI="True"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                    ToolTip="حذف" UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                 
                                    <image  url="~/Images/icons/delete.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnBack" runat="server" CausesValidation="False"
                                    EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                    ToolTip="بازگشت" UseSubmitBehavior="False">

                                    <image  url="~/Images/icons/Back.png"  />
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </table>
                </dxp:PanelContent>
            </PanelCollection>
        </TSPControls:CustomASPxRoundPanelMenu>
        <br />
        <TSPControls:CustomAspxDevGridView ID="CustomAspxDevGridView1" runat="server" AutoGenerateColumns="False"
            KeyFieldName="AttachId" ClientInstanceName="grid">

            <Columns>
                <dxwgv:GridViewDataTextColumn FieldName="AttachId" Name="AttachId" Visible="False"
                    VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataImageColumn Caption="تصویر" FieldName="FilePath" VisibleIndex="0" Name="FilePath">
                    <PropertiesImage ImageHeight="75px" ImageWidth="75px"></PropertiesImage>
                </dxwgv:GridViewDataImageColumn>
                <dxwgv:GridViewDataTextColumn Caption="توضیحات" VisibleIndex="1" Width="350px" FieldName="Description" Name="Description">
                </dxwgv:GridViewDataTextColumn>
            </Columns>

        </TSPControls:CustomAspxDevGridView>
        <br />
        <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl1" runat="server"
            HeaderText="جدید" PopupElementID="btnSearch1" ClientInstanceName="pop" Width="350px">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table>
                        <tr>
                            <td style="vertical-align: top; text-align: right">
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="تصویر">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="vertical-align: top; text-align: right">
                                <TSPControls:CustomAspxUploadControl ID="flp" runat="server"
                                    MaxSizeForUploadFile="0" ShowProgressPanel="True">
                                </TSPControls:CustomAspxUploadControl>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; text-align: right">
                                <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="توضیحات">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="vertical-align: top; text-align: right">
                                <TSPControls:CustomASPXMemo ID="txtDesc" runat="server" Height="26px">
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
                                <TSPControls:CustomAspxButton  ID="btnSave" runat="server" Text="ذخیره" OnClick="btnSave_Click" UseSubmitBehavior="False">
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
        <TSPControls:CustomASPxPopupControl ID="ASPxPopupControl2" runat="server" ClientInstanceName="popview"
            CloseAction="CloseButton"
            HeaderText="مشاهده" PopupElementID="btnSearch1"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    <table>
                        <tr>
                            <td style="vertical-align: top; text-align: right">
                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="تصویر">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="vertical-align: top; text-align: right">
                                <dxe:ASPxImage ID="ASPxImage1" runat="server" ClientInstanceName="img" Height="550px"
                                    Width="550px">
                                </dxe:ASPxImage>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; text-align: right">
                                <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="توضیحات">
                                </dxe:ASPxLabel>
                            </td>
                            <td style="vertical-align: top; text-align: right">
                                <TSPControls:CustomASPXMemo ID="ASPxMemo1" runat="server" Height="26px" Width="550px" ClientInstanceName="desc">
                                    <ValidationSettings>

                                        <ErrorFrameStyle ImageSpacing="4px">
                                            <ErrorTextPaddings PaddingLeft="4px" />
                                        </ErrorFrameStyle>
                                    </ValidationSettings>
                                </TSPControls:CustomASPXMemo>
                            </td>
                        </tr>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
           
        </TSPControls:CustomASPxPopupControl>
        <asp:HiddenField ID="HDAgentId" runat="server" Visible="False" />
        <asp:HiddenField ID="PgMode" runat="server" Visible="False" />
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
      <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server">
                    <PanelCollection>
                        <dxp:PanelContent>

                                        <table>
                                            <tr>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="BtnNew2" runat="server" EnableTheming="False"
                                                        EnableViewState="False" Text=" " ToolTip="جدید" AutoPostBack="False" UseSubmitBehavior="False">
                                                        
                                                        <image  url="~/Images/icons/new.png"  />
                                                        <ClientSideEvents Click="function(s, e) {
	pop.Show();
}" />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnView2" runat="server" AutoPostBack="False" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False"
                                                        Text=" " ToolTip="مشاهده" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	popview.Show();
	SetControlValues();
		
}" />
                                                       
                                                        <image  url="~/Images/icons/view.png"  />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnDelete2" runat="server" EnableClientSideAPI="True"
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnDelete_Click" Text=" "
                                                        ToolTip="حذف" UseSubmitBehavior="False">
                                                        <ClientSideEvents Click="function(s, e) {
	 e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}" />
                                                     
                                                        <image  url="~/Images/icons/delete.png"  />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                                <td>
                                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="ASPxButton1" runat="server" CausesValidation="False"
                                                        EnableTheming="False" EnableViewState="False" OnClick="btnBack_Click" Text=" "
                                                        ToolTip="بازگشت" UseSubmitBehavior="False">
                                                      
                                                        <image  url="~/Images/icons/Back.png"  />
                                                    </TSPControls:CustomAspxButton>
                                                </td>
                                            </tr>
                                        </table>
                                   </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
</asp:Content>



