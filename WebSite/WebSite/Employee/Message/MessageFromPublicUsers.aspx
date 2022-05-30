<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="MessageFromPublicUsers.aspx.cs" Inherits="Employee_Message_MessageFromPublicUsers"
    Title="نامه های دریافتی از کاربران عمومی" %>

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
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <asp:UpdatePanel runat="server" ID="Test">
        <ContentTemplate>--%>
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

                function ShowPrintWindow() {
                    window.open(HiddenNewsData.Get("PrintAddress"), null, "height=500px, width=800px,resizable=yes, status=no, toolbar=no, menubar=no, location=no, scrollbars=1");
                }
            </script>
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]</div>
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                                        <table >
                                            <tbody>
                                                <tr>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="پاسخ به نامه"
                                                            ID="btnReply1" EnableViewState="False" EnableTheming="False" AutoPostBack="false"
                                                            UseSubmitBehavior="False">
                                                           
                                                            <Image  Url="~/Images/icons/ReplyOutputMessage.png">
                                                            </Image>
                                                            <ClientSideEvents Click="function(s, e) { 
if(grdMails.GetFocusedRowIndex()&gt;=0){
                     panelSendAnswer.SetVisible(true);
                     panelSendAnswerFinish.SetVisible(false); 
                     lblErrorInSendMessage.SetVisible(false); 
popupAnswer.Show();
}
else
alert('رکوردی انتخاب نشده است');
 }" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ارجاع"
                                                            ID="btnSend" EnableViewState="False" EnableTheming="False" AutoPostBack="false"
                                                            UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/LetterReference.png">
                                                            </Image>
                                                            <ClientSideEvents Click="function(s, e) { 
if(grdMails.GetFocusedRowIndex()&gt;=0){
  panelSend.SetVisible(true);
  panelSendFinish.SetVisible(false); 
  lblErrorInSendMessage1.SetVisible(false); 
  cmbMessageGroup.SetSelectedIndex(-1);
  PopupSend.Show();
}
else
alert('رکوردی انتخاب نشده است');
 }" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator">
                                                        </TSPControls:MenuSeprator>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف نامه"
                                                            ID="btnDelete1" EnableViewState="False" EnableTheming="False" OnClick="btnDelete_Click"
                                                            UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/Delete.png">
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
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator1">
                                                        </TSPControls:MenuSeprator>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ پیام"
                                                            ID="btnPrint" EnableViewState="False" EnableTheming="False" AutoPostBack="false"
                                                            UseSubmitBehavior="False">
                                                            <HoverStyle BackColor="#FFE0C0">
                                                                <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                                            </HoverStyle>
                                                            <Image  Url="~/Images/icons/printers.png">
                                                            </Image>
                                                            <ClientSideEvents Click="function(s,e){ ShowPrintWindow(); }" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                      <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  
                                                    ToolTip="خروجی Excel" CausesValidation="False" ID="btnExportExcel" EnableClientSideAPI="True"
                                                    UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnPrintClient"
                                                    AutoPostBack="false" OnClick="btnExportExcel_Click">
                                                    <ClientSideEvents Click="function(s,e){ }" />
                                                    <HoverStyle BackColor="#FFE0C0">
                                                        <Border BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" />
                                                    </HoverStyle>
                                                    <Image  Url="~/Images/icons/ExportExcel.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                                </tr>
                                            </tbody>
                                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <div align="right">
                
                    <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="grdMails">
                    </dxwgv:ASPxGridViewExporter>
                <TSPControls:CustomAspxDevGridView ID="grdMails" runat="server" DataSourceID="ObjectDataSource1"
                    Width="100%" OnCustomCallback="grdMails_CustomCallback" ClientInstanceName="grdMails"
                    KeyFieldName="MessageId" AutoGenerateColumns="False" Font-Size="8pt">
                    <ClientSideEvents FocusedRowChanged="grdMails_FocusedRowChanged"></ClientSideEvents>
                    <Settings ShowHorizontalScrollBar="true" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="MessageId" Width="4px" Caption="MessageId"
                            Visible="False" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="SenderName" Width="200px" Caption="نام"
                            VisibleIndex="0">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="SenderEmail" Width="150px" Caption="پست الکترونیکی"
                            VisibleIndex="1">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="SendDate" Width="80px" Caption="تاریخ ارسال"
                            VisibleIndex="2">
                            <CellStyle Wrap="false">
                            </CellStyle>
                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="SendMessageSubject" Width="150px" Caption="موضوع"
                            VisibleIndex="3">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="GroupName" Width="120px" Caption="بخش گیرنده"
                            VisibleIndex="3">
                            <HeaderStyle HorizontalAlign="Right" Wrap="true"></HeaderStyle>
                            <CellStyle HorizontalAlign="Right" Wrap="true">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="TypeName" Width="80px" Caption="نوع" VisibleIndex="3">
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
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SelectRecievedMessage"
                    TypeName="TSP.DataManager.PublicMessagesManager">
                    <SelectParameters>
                        <asp:Parameter Name="MeId" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <asp:Panel ID="panelViewMail" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="1px"
                    BorderColor="#7EACB1" CssClass="PanelViewMail" ScrollBars="Vertical" Height="400px">
                    <TSPControls:CustomAspxCallbackPanel runat="server"  ClientInstanceName="CallBackMails"
                      
                        ID="CallBackMails" OnCallback="CallBackMails_Callback">
                        <ClientSideEvents EndCallback="CallBackMails_OnEndCallback"></ClientSideEvents>
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <div align="right">
                                    <asp:Label ID="lblMail" runat="server" Width="97%" Font-Names="Tahoma" Font-Size="8pt"></asp:Label>
                                </div>
                                <dx:ASPxHiddenField ID="HiddenNewsData" runat="server" ClientInstanceName="HiddenNewsData">
                                </dx:ASPxHiddenField>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </TSPControls:CustomAspxCallbackPanel>
                </asp:Panel>
            </div>
            <br />
            <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                Width="100%">
                <PanelCollection>
                    <dxp:PanelContent>
                                        <table >
                                            <tbody>
                                                <tr>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="پاسخ به نامه"
                                                            ID="btnReply2" EnableViewState="False" EnableTheming="False" AutoPostBack="false"
                                                            UseSubmitBehavior="False">
                                                            <Image  Url="~/Images/icons/ReplyOutputMessage.png">
                                                            </Image>
                                                            <ClientSideEvents Click="function(s, e) { 
if(grdMails.GetFocusedRowIndex()&gt;=0){
                     panelSendAnswer.SetVisible(true);
                     panelSendAnswerFinish.SetVisible(false); 
                     lblErrorInSendMessage.SetVisible(false); 
popupAnswer.Show();
}
else
alert('رکوردی انتخاب نشده است');
 }" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ارجاع"
                                                            ID="ASPxButton2" EnableViewState="False" EnableTheming="False" AutoPostBack="false"
                                                            UseSubmitBehavior="False">
                                                            <Image  Url="~/Images/LetterReference.png">
                                                            </Image>
                                                            <ClientSideEvents Click="function(s, e) { 
if(grdMails.GetFocusedRowIndex()&gt;=0){
  panelSend.SetVisible(true);
  panelSendFinish.SetVisible(false); 
  lblErrorInSendMessage1.SetVisible(false); 
   cmbMessageGroup.SetSelectedIndex(-1);
  PopupSend.Show();
}
else
alert('رکوردی انتخاب نشده است');
 }" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator2">
                                                        </TSPControls:MenuSeprator>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف نامه"
                                                            ID="btnDelete2" EnableViewState="False" EnableTheming="False" OnClick="btnDelete_Click"
                                                            UseSubmitBehavior="False">
                                                            <Image  Url="~/Images/icons/Delete.png">
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
                                                    <td width="10px" align="center">
                                                        <TSPControls:MenuSeprator runat="server" ID="MenuSeprator3">
                                                        </TSPControls:MenuSeprator>
                                                    </td>
                                                    <td >
                                                        <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ پیام"
                                                            ID="ASPxButton3" EnableViewState="False" EnableTheming="False" AutoPostBack="false"
                                                            UseSubmitBehavior="False">
                                                            <Image  Url="~/Images/icons/printers.png">
                                                            </Image>
                                                            <ClientSideEvents Click="function(s,e){ ShowPrintWindow(); }" />
                                                        </TSPControls:CustomAspxButton>
                                                    </td>  <td>
                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  
                                                    ToolTip="خروجی Excel" CausesValidation="False" ID="btnExportExcel2" EnableClientSideAPI="True"
                                                    UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnPrintClient"
                                                    AutoPostBack="false" OnClick="btnExportExcel_Click">
                                                    <ClientSideEvents Click="function(s,e){ }" />
                                                    <Image  Url="~/Images/icons/ExportExcel.png"  />
                                                </TSPControls:CustomAspxButton>
                                            </td>
                                                </tr>
                                            </tbody>
                                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </TSPControls:CustomASPxRoundPanelMenu>
            <TSPControls:CustomASPxPopupControl ID="popupAnswer" runat="server" Width="510px" ClientInstanceName="popupAnswer"
                AllowDragging="True" AutoUpdatePosition="True" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
                 HeaderText="پاسخ به نامه" >
                <SizeGripImage Height="12px" Width="12px">
                </SizeGripImage>
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">
                        <TSPControls:CustomAspxCallbackPanel runat="server"  ClientInstanceName="callBackAnswer"
                           
                            ID="callBackAnswer" OnCallback="callBackAnswer_Callback">
                            <ClientSideEvents EndCallback="function(s,e){ grdMails.PerformCallback(''); }"></ClientSideEvents>
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <dxp:ASPxPanel runat="server" ClientInstanceName="panelSendAnswer" ID="panelSendAnswer">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <div dir="rtl" align="right">
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxLabel runat="server" Text="خطایی در ارسال پیغام انجام گرفته است" ClientInstanceName="lblErrorInSendMessage"
                                                                        ClientVisible="False" ForeColor="Red" ID="lblErrorInSendMessage">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    متن پاسخ :
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <TSPControls:CustomASPXMemo runat="server" Height="200px"  
                                                                        Width="500px" ClientInstanceName="txtAnswerBody" ID="txtAnswerBody">
                                                                        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                                                            <RequiredField IsRequired="True" ErrorText="متن پاسخ وارد نشده است"></RequiredField>
                                                                        </ValidationSettings>
                                                                    </TSPControls:CustomASPXMemo>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table style="table-layout: fixed" width="100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <TSPControls:CustomAspxButton runat="server" AutoPostBack="False"  
                                                                                        Text="&#160;ارسال" ID="btnAnswer">
                                                                                        <ClientSideEvents Click="function(s,e){ 
                                                                        txtAnswerBody.Validate();
                                                                        if(txtAnswerBody.GetIsValid())
                                                                          callBackAnswer.PerformCallback(txtAnswerBody.GetText());
                                                                         }"></ClientSideEvents>
                                                                                        <Image Height="20px" Width="20px" Url="~/Images/E-Mail.png">
                                                                                        </Image>
                                                                                    </TSPControls:CustomAspxButton>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <TSPControls:CustomAspxButton runat="server" AutoPostBack="False"  
                                                                                        CausesValidation="False" Text="&#160;انصراف" ID="btnCancel">
                                                                                        <ClientSideEvents Click="function(s,e){ popupAnswer.Hide(); }"></ClientSideEvents>
                                                                                        <Image Height="20px" Width="20px" Url="~/Images/Stop.png">
                                                                                        </Image>
                                                                                    </TSPControls:CustomAspxButton>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                    <dxp:ASPxPanel runat="server" ClientInstanceName="panelSendAnswerFinish" ClientVisible="False"
                                        ID="panelSendAnswerFinish">
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <div dir="rtl" align="center">
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <dxe:ASPxLabel runat="server" Text="پاسخ شما ارسال شد" Font-Size="11pt" ForeColor="Green"
                                                        ID="ASPxLabel1">
                                                    </dxe:ASPxLabel>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <TSPControls:CustomAspxButton runat="server" AutoPostBack="False" CausesValidation="False" Text="&#160;خروج"
                                                        ID="ASPxButton1">
                                                        <ClientSideEvents Click="function(s,e){ popupAnswer.Hide(); }"></ClientSideEvents>
                                                        <Image Height="20px" Width="20px" Url="~/Images/Stop.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                    <br />
                                                </div>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </TSPControls:CustomAspxCallbackPanel>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
                <CloseButtonImage Height="17px" Width="17px">
                </CloseButtonImage>
                <HeaderStyle>
                    <Paddings PaddingLeft="10px" PaddingTop="1px" PaddingRight="6px"></Paddings>
                </HeaderStyle>
            </TSPControls:CustomASPxPopupControl>
            <TSPControls:CustomASPxPopupControl ID="PopupSend" runat="server" Width="300px" ClientInstanceName="PopupSend"
                AllowDragging="True" AutoUpdatePosition="True" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
                 HeaderText="ارجاع به دیگران" >
                <SizeGripImage Height="12px" Width="12px">
                </SizeGripImage>
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        <TSPControls:CustomAspxCallbackPanel runat="server"  ClientInstanceName="callBackSend"
                          
                            ID="callBackSend" OnCallback="callBackSend_Callback">
                            <ClientSideEvents EndCallback="function(s,e){ grdMails.PerformCallback(''); }"></ClientSideEvents>
                            <PanelCollection>
                                <dxp:PanelContent ID="PanelContent1" runat="server">
                                    <dxp:ASPxPanel runat="server" ClientInstanceName="panelSend" ID="panelSend">
                                        <PanelCollection>
                                            <dxp:PanelContent ID="PanelContent2" runat="server">
                                                <div dir="rtl" align="right">
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <dxe:ASPxLabel runat="server" Text="خطایی در ارجاع پیغام انجام گرفته است" ClientInstanceName="lblErrorInSendMessage1"
                                                                        ClientVisible="False" ForeColor="Red" ID="lblErrorInSendMessage1">
                                                                    </dxe:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" width="15%" valign="top">
                                                                    گیرنده
                                                                </td>
                                                                <td align="right" width="85%" valign="top">
                                                                    <TSPControls:CustomAspxComboBox ID="cmbMessageGroup" ClientInstanceName="cmbMessageGroup" DataSourceID="ObjectDataSource2"
                                                                        ValueField="NcId" TextField="NcName" runat="server" 
                                                                          ValueType="System.String"
                                                                        HorizontalAlign="Right" Width="100%" RightToLeft="True">
                                                                        <Columns>
                                                                              <dxe:ListBoxColumn FieldName="NcName" Caption="نام بخش" Width="100px"></dxe:ListBoxColumn>
                                                                            <dxe:ListBoxColumn FieldName="EmployeeName" Caption="مسئول" Width="150px"></dxe:ListBoxColumn>
                                                                        </Columns>
                                                                        <ButtonStyle Width="13px">
                                                                        </ButtonStyle>
                                                                        <ValidationSettings ErrorTextPosition="Bottom">
                                                                            <RequiredField IsRequired="true" ErrorText="لطفا گیرنده را انتخاب نمائید" />
                                                                            
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </TSPControls:CustomAspxComboBox>
                                                                 <%--   <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetData"
                                                                        TypeName="TSP.DataManager.PublicMessageGroupsManager"></asp:ObjectDataSource>--%>
                                                                       <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="SelectNezamMemberChartForContactUs"
                    TypeName="TSP.DataManager.NezamMemberChartManager"></asp:ObjectDataSource>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <table style="table-layout: fixed" width="100%">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <TSPControls:CustomAspxButton runat="server" AutoPostBack="False"  
                                                                                        Text="&#160;ذخیره" ID="btnSendToOthers">
                                                                                        <ClientSideEvents Click="function(s,e){ 
                                                                        cmbMessageGroup.Validate();
                                                                        if(cmbMessageGroup.GetIsValid())
                                                                          callBackSend.PerformCallback(cmbMessageGroup.GetValue());
                                                                         }"></ClientSideEvents>
                                                                                        <Image Height="20px" Width="20px" Url="~/Images/icons/save.png">
                                                                                        </Image>
                                                                                    </TSPControls:CustomAspxButton>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <TSPControls:CustomAspxButton runat="server" AutoPostBack="False"  
                                                                                        CausesValidation="False" Text="&#160;انصراف" ID="btnCancelToOthers">
                                                                                        <ClientSideEvents Click="function(s,e){ PopupSend.Hide(); }"></ClientSideEvents>
                                                                                        <Image Height="20px" Width="20px" Url="~/Images/Stop.png">
                                                                                        </Image>
                                                                                    </TSPControls:CustomAspxButton>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                    <dxp:ASPxPanel runat="server" ClientInstanceName="panelSendFinish" ClientVisible="False"
                                        ID="panelSendFinish">
                                        <PanelCollection>
                                            <dxp:PanelContent ID="PanelContent3" runat="server">
                                                <div dir="rtl" align="center">
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <dxe:ASPxLabel runat="server" Text="پیام به شخص مورد نظر ارجاع شد" Font-Size="11pt"
                                                        ForeColor="Green" ID="ASPxLabel3">
                                                    </dxe:ASPxLabel>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <TSPControls:CustomAspxButton runat="server" AutoPostBack="False" CausesValidation="False" Text="&#160;خروج"
                                                        ID="ASPxButton6">
                                                        <ClientSideEvents Click="function(s,e){ PopupSend.Hide(); }"></ClientSideEvents>
                                                        <Image Height="20px" Width="20px" Url="~/Images/Stop.png">
                                                        </Image>
                                                    </TSPControls:CustomAspxButton>
                                                    <br />
                                                </div>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                    </dxp:ASPxPanel>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </TSPControls:CustomAspxCallbackPanel>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
              
            </TSPControls:CustomASPxPopupControl>
</asp:Content>
