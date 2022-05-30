<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="Inbox.aspx.cs" Inherits="Members_Message_Inbox" Title="نامه های رسیده" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxdv" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                 
                    <div style="text-align: right" dir="rtl" id="DivReport" class="DivErrors" runat="server">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                            href="#">بستن</a>]</div>
                    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    >
                    <PanelCollection>
                        <dxp:PanelContent>


  
                                <table  >
                                    <tr>
                                        <td style="vertical-align: top" dir="ltr" align="right">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                                CausesValidation="False" Width="25px" ID="btnDelete" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnDelete_Click">
                                                <ClientSideEvents Click="function(s, e) {	
e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}
"></ClientSideEvents>
                                               
                                                <Image  Url="~/Images/icons/delete.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td style="vertical-align: top" dir="ltr" align="right">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ارسال به دیگران"
                                                CausesValidation="False" Width="25px" ID="btnForward" UseSubmitBehavior="False"
                                                Visible="False" EnableViewState="False" EnableTheming="False" OnClick="ASPxButton2_Click">
                                               
                                                <Image  Url="~/Images/MailReply.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td style="vertical-align: top" dir="ltr" align="right">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px" 
                                                ToolTip="پاسخ" Width="25px" ID="btnReply" UseSubmitBehavior="False" Visible="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="ASPxButton1_Click">
                                               
                                                <Image  Url="~/Images/Mail-Reply1.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </table>
                          </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                    <br />
                    <TSPControls:CustomAspxDevGridView ID="GridViewMsg" runat="server" DataSourceID="ObjectDataSource1"
                        Width="100%" RightToLeft="True" ClientInstanceName="grid1" OnHtmlRowPrepared="GridViewMsg_HtmlRowPrepared"
                        KeyFieldName="MsgId" OnHtmlDataCellPrepared="GridViewMsg_HtmlDataCellPrepared"
                        OnAutoFilterCellEditorInitialize="GridViewMsg_AutoFilterCellEditorInitialize">
                        <ClientSideEvents FocusedRowChanged="function(s, e) {
	//SetMemoValue();
	CallbackMsg.PerformCallback('');
}"></ClientSideEvents>
                        <Columns>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="0" FieldName="LastName" Caption="فرستنده"
                                Width="20%">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="1" FieldName="MsgSubject" Caption="موضوع"
                                Width="55%">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn VisibleIndex="2" FieldName="Date" Caption="تاریخ" Width="15%">
                                <CellStyle Wrap="False">
                                </CellStyle>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn VisibleIndex="3" Caption=" " Width="10%" ShowClearFilterButton="true">
                         
                            </dxwgv:GridViewCommandColumn>
                        </Columns>
                        <Settings ShowFilterBar="Hidden" />
                    </TSPControls:CustomAspxDevGridView>
                    <br />
                    <table class="TableBorder" width="100%">
                        <tbody>
                            <tr>
                                <td class="TableTitle" colspan="2" align="right">
                                    <asp:Label ID="Label3" runat="server" Text="متن پیام:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 20%; background-color: whitesmoke;" align="center">
                                    <TSPControls:CustomAspxCallbackPanel ID="CallbackPanelMsg" runat="server" Width="100%" ClientInstanceName="CallbackMsg"
                                        OnCallback="CallbackPanelMsg_Callback"  RightToLeft="True"
                                        >
                                        <PanelCollection>
                                            <dxp:PanelContent runat="server">
                                                <div align="right">
                                                    <asp:Panel runat="server" Width="100%" Direction="RightToLeft" Height="150px" ScrollBars="Auto"
                                                        ID="Panel1" Style="padding: 5px 5px 5px 5px">
                                                        <asp:Label runat="server" Height="100%" Width="100%" ID="lblMessageContent" ClientInstanceName="label1">
                                                        </asp:Label>
                                                    </asp:Panel>
                                                </div>
                                            </dxp:PanelContent>
                                        </PanelCollection>
                                       
                                    </TSPControls:CustomAspxCallbackPanel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />  <asp:HiddenField runat="server" ID="MsgId"></asp:HiddenField>
                 <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    >
                    <PanelCollection>
                        <dxp:PanelContent>


   
                                <table >
                                    <tr>
                                        <td style="vertical-align: top" dir="ltr" align="right">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="حذف"
                                                CausesValidation="False" Width="25px" ID="btnDelete2" UseSubmitBehavior="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="btnDelete_Click">
                                                <ClientSideEvents Click="function(s, e) {	
e.processOnServer= confirm('آیا مطمئن به حذف این ردیف هستید؟');
}
"></ClientSideEvents>
                                              
                                                <Image  Url="~/Images/icons/delete.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td style="vertical-align: top" dir="ltr" align="right">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="ارسال به دیگران"
                                                CausesValidation="False" Width="25px" ID="btnForward2" UseSubmitBehavior="False"
                                                Visible="False" EnableViewState="False" EnableTheming="False" OnClick="ASPxButton2_Click">
                                               
                                                <Image  Url="~/Images/MailReply.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                        <td style="vertical-align: top" dir="ltr" align="right">
                                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " Height="25px" 
                                                ToolTip="پاسخ" Width="25px" ID="btnReply2" UseSubmitBehavior="False" Visible="False"
                                                EnableViewState="False" EnableTheming="False" OnClick="ASPxButton1_Click">
                                                
                                                <Image  Url="~/Images/Mail-Reply1.png">
                                                </Image>
                                            </TSPControls:CustomAspxButton>
                                        </td>
                                    </tr>
                                </table>
                              
                           </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                        FilterExpression="NeedConfirm={0}" TypeName="TSP.DataManager.MessageManager"
                        SelectMethod="SelectMessageOFInbox">
                        <SelectParameters>
                            <asp:Parameter Name="ReceiverId" Type="Int32" DefaultValue="-1" />
                            <asp:Parameter Name="ReceiverType" Type="Int32" DefaultValue="-1" />
                        </SelectParameters>
                        <FilterParameters>
                            <asp:Parameter DefaultValue="0" Name="NeedConfirm" />
                        </FilterParameters>
                    </asp:ObjectDataSource>
                    <dxhf:ASPxHiddenField ID="ASPxHiddenField1" runat="server" ClientInstanceName="hidden1">
                    </dxhf:ASPxHiddenField>
                    <asp:HiddenField ID="IsSelectAll" runat="server" Value="false"></asp:HiddenField>
                    <asp:ModalUpdateProgress ID="ModalUpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                        DisplayAfter="0" BackgroundCssClass="modalProgressGreyBackground">
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
