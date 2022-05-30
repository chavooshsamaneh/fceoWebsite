<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="AnsweredMessageForPublicUsers.aspx.cs" Inherits="Employee_Message_AnsweredMessageForPublicUsers"
    Title="نامه های پاسخ داده شده به کاربران عمومی" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        var postponedCallBackMailsValue = null;

        function contentPageLoad(sender, args) {
            if (grdMails.GetVisibleRowsOnPage() > 0) {
                grdMails.SetFocusedRowIndex(0);
                CallBackMails.PerformCallback(grdMails.GetFocusedRowIndex());
            }
        }

        function grdMails_FocusedRowChanged(s, e) {
            var item = grdMails.GetFocusedRowIndex();
            if (CallBackMails.InCallback())
                postponedCallBackMailsValue = item;
            else
                CallBackMails.PerformCallback(item);
        }
        function CallBackMails_OnEndCallback(s, e) {
            if (postponedCallBackMailsValue != null) {
                CallBackMails.PerformCallback(postponedCallBackMailsValue);
                postponedCallBackMailsValue = null;
            }
        }

        function ShowHideMessageBox() {
            var MessageHeader = document.getElementById('MessageHeader');
            var MessageBox = document.getElementById('MessageBox');
            if (MessageBox.style.display == 'none') {
                MessageBox.style.display = 'block';
                MessageHeader.innerText = '- شرح پیغام :';
            }
            else if (MessageBox.style.display == 'block') {
                MessageBox.style.display = 'none';
                MessageHeader.innerText = '+ شرح پیغام :';
            }
        }
    </script>
    <%--    <asp:UpdatePanel runat="server" ID="Test">
        <ContentTemplate>--%>
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
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
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف نامه"
                                    ID="btnDelete1" EnableViewState="False" EnableTheming="False" OnClick="btnDelete_Click"
                                    UseSubmitBehavior="False">
                                    <Image Url="~/Images/icons/Delete.png">
                                    </Image>
                                    <ClientSideEvents Click="function(s, e) { 
                            if(grdMails.GetFocusedRowIndex()&gt;=0){
                             e.processOnServer=confirm('آیا مطمئن از حذف این نامه هستید؟');
                            }
else{
alert('رکوردی انتخاب نشده است');
                            e.processOnServer=false; }
                            }" />
                                </TSPControls:CustomAspxButton>
                            </td>
                            <td>
                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "
                                    ToolTip="خروجی Excel" CausesValidation="False" ID="btnExportExcel" EnableClientSideAPI="True"
                                    UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnPrintClient"
                                    AutoPostBack="false" OnClick="btnExportExcel_Click">
                                    <ClientSideEvents Click="function(s,e){ }" />
                                    <Image Url="~/Images/icons/ExportExcel.png" />
                                </TSPControls:CustomAspxButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
                </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
        <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="grdMails">
        </dxwgv:ASPxGridViewExporter>
        <TSPControls:CustomAspxDevGridView ID="grdMails" runat="server" DataSourceID="ObjectDataSource1"
            Width="100%" ClientInstanceName="grdMails" KeyFieldName="MessageId" AutoGenerateColumns="False"
            Font-Size="8pt">
            <ClientSideEvents FocusedRowChanged="grdMails_FocusedRowChanged"></ClientSideEvents>
            <Columns>
                <dxwgv:GridViewDataTextColumn FieldName="MessageId" Width="4px" Caption="MessageId"
                    Visible="False" VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="SenderName" Width="150px" Caption="نام"
                    VisibleIndex="0">
                    <CellStyle HorizontalAlign="Right">
                    </CellStyle>
                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="SenderEmail" Width="150px" Caption="پست الکترونیک"
                    VisibleIndex="1">
                    <CellStyle HorizontalAlign="Right">
                    </CellStyle>
                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="SendDate" Width="70px" Caption="تاریخ ارسال"
                    VisibleIndex="2">
                    <CellStyle Wrap="false">
                    </CellStyle>
                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="SendMessageSubject" Width="250px" Caption="موضوع"
                    VisibleIndex="3">
                    <CellStyle HorizontalAlign="Right">
                    </CellStyle>
                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="GroupName" Width="100px" Caption="بخش گیرنده"
                    VisibleIndex="3">
                    <HeaderStyle HorizontalAlign="Right" Wrap="true"></HeaderStyle>
                    <CellStyle HorizontalAlign="Right" Wrap="true">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="TypeName" Width="100px" Caption="نوع" VisibleIndex="3">
                    <HeaderStyle HorizontalAlign="Right" Wrap="true"></HeaderStyle>
                    <CellStyle HorizontalAlign="Right" Wrap="true">
                    </CellStyle>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="SendMessageBody" Caption="Body" Visible="False"
                    VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Width="4px" VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </TSPControls:CustomAspxDevGridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SelectAnsweredMessage"
            TypeName="TSP.DataManager.PublicMessagesManager">
            <SelectParameters>
                <asp:Parameter Name="MeId" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <br />
        <asp:Panel ID="panelViewMail" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="1px"
            BorderColor="#7EACB1" CssClass="PanelViewMail" ScrollBars="Vertical" Height="400px">
            <TSPControls:CustomAspxCallbackPanel ID="CallBackMails" runat="server"
                ClientInstanceName="CallBackMails" OnCallback="CallBackMails_Callback">
                <PanelCollection>
                    <dxp:PanelContent runat="server">
                        <div align="right">
                            <asp:Label runat="server" Font-Names="Tahoma" Font-Size="8pt" Width="97%" ID="lblMail"></asp:Label>
                        </div>
                    </dxp:PanelContent>
                </PanelCollection>
                <ClientSideEvents EndCallback="CallBackMails_OnEndCallback"></ClientSideEvents>
            </TSPControls:CustomAspxCallbackPanel>
        </asp:Panel>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="حذف نامه"
                                                    ID="btnDelete2" EnableViewState="False" EnableTheming="False" OnClick="btnDelete_Click"
                                                    UseSubmitBehavior="False">                                          
                                                    <image url="~/Images/icons/Delete.png">
                                                            </image>
                                                    <ClientSideEvents Click="function(s, e) { 
                            if(grdMails.GetFocusedRowIndex()&gt;=0){
                             e.processOnServer=confirm('آیا مطمئن از حذف این نامه هستید؟');
                            }
else{
alert('رکوردی انتخاب نشده است');
                            e.processOnServer=false; }
                            }" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                            <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "
                                                    ToolTip="خروجی Excel" CausesValidation="False" ID="btnExportExcel1" EnableClientSideAPI="True"
                                                    UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnPrintClient"
                                                    AutoPostBack="false" OnClick="btnExportExcel_Click">
                                                    <ClientSideEvents Click="function(s,e){ }" />
                                                    <image url="~/Images/icons/ExportExcel.png" />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
</asp:Content>
