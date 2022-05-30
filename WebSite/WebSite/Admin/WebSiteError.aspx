<%@ Page Language="C#" AutoEventWireup="true" CodeFile="websiteError.aspx.cs" Inherits="Admin_websiteError"
    Title="خطاهای سایت" MasterPageFile="~/Admin/MasterPage.master" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <div align="right" id="DivReport" class="DivErrors" runat="server" visible="true">
                        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
                        [<a class="closeLink" href="#">بستن</a>]</div>
    <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="100%" 
         GroupBoxCaptionOffsetY="-24px" HeaderText="" RightToLeft="True"
         ShowHeader="False">
        <ContentPaddings PaddingBottom="2px" PaddingLeft="2px" PaddingTop="2px" PaddingRight="2px" />
        <HeaderStyle Height="23px">
            <Paddings PaddingBottom="0px" PaddingLeft="2px" PaddingTop="0px" />
        </HeaderStyle>
        <PanelCollection>
            <dx:PanelContent runat="server">
                <table>
                    <tr>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بررسی شده"
                                ID="btnIsValide" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                OnClick="btnIsValide_Click">
                                <ClientSideEvents Click="function(s, e) {
	                                if (grvWebsiteErrrs.GetFocusedRowIndex()&lt;0)
                                 {
                                   e.processOnServer=false;
                                   alert(&quot;ردیفی انتخاب نشده است&quot;);
                                 }
                                    else
	                                e.processOnServer= confirm('آیا مطمئن از بررسی شدن این  خطاهستید؟');
                                }"></ClientSideEvents>
                                <HoverStyle BackColor="#FFE0C0">
                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                </HoverStyle>
                                <Image  Url="~/Images/icons/Active.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                ID="btnPrint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False">
                                <ClientSideEvents Click="function(s,e){grvWebsiteErrrs.PerformCallback('Print'); }">
                                </ClientSideEvents>
                                <HoverStyle BackColor="#FFE0C0">
                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                </HoverStyle>
                                <Image  Url="~/Images/icons/Printers.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                ID="btnExportExcel2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                OnClick="btnExportExcel_Click">
                                <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                <HoverStyle BackColor="#FFE0C0">
                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                </HoverStyle>
                                <Image  Url="~/Images/icons/ExportExcel.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>
    <br />
    <TSPControls:CustomAspxDevGridView ID="grvWebsiteErrrs" runat="server" AutoGenerateColumns="False"
        ClientInstanceName="grvWebsiteErrrs" 
         DataSourceID="ObjectDataSource1" KeyFieldName="Id" OnCustomCallback="grvWebsiteErrrs_CustomCallback"
        RightToLeft="True" Width="100%">
        <Columns>
            <dxwgv:GridViewDataTextColumn Caption="نام کاربر" FieldName="FullName" VisibleIndex="0"
                Width="100px">
                <CellStyle Wrap="true">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نوع کاربر" FieldName="UltName" VisibleIndex="0"
                Width="100px">
                <CellStyle Wrap="true">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ" FieldName="Date" VisibleIndex="1" Width="80px">
                <CellStyle Wrap="false">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ساعت" FieldName="Time" VisibleIndex="2" Width="50px">
                <CellStyle Wrap="false">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="پیام خطا" FieldName="Message" VisibleIndex="3"
                Width="300px">
                <CellStyle Wrap="true" CssClass="LeftToRightDirection">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="مسیر خطا" FieldName="Url" VisibleIndex="4"
                Width="350px">
                <CellStyle Wrap="true" CssClass="LeftToRightDirection">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="جزییات خطا" FieldName="StackTrace" VisibleIndex="5"
                Width="350px">
                <CellStyle Wrap="True" CssClass="LeftToRightDirection">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="7" Width="2%" ShowClearFilterButton="true">
        
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowFilterRowMenu="True" ShowGroupPanel="True" ShowFilterRow="True" ShowHorizontalScrollBar="true" />
        <ClientSideEvents EndCallback="function(s, e) {
	 if(s.cpPrint==1)
        {
            window.open('../Print.aspx');
           s.cpPrint=0;
        }
}" />
    </TSPControls:CustomAspxDevGridView>
    <br />
    <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" Width="100%" 
         GroupBoxCaptionOffsetY="-24px" HeaderText="" RightToLeft="True"
         ShowHeader="False">
        <ContentPaddings PaddingBottom="2px" PaddingLeft="2px" PaddingTop="2px" PaddingRight="2px" />
        <HeaderStyle Height="23px">
            <Paddings PaddingBottom="0px" PaddingLeft="2px" PaddingTop="0px" />
        </HeaderStyle>
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <table>
                    <tr>
                     <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="بررسی شده"
                                ID="btnIsValide2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                OnClick="btnIsValide_Click">
                                <ClientSideEvents Click="function(s, e) {
	                                if (grvWebsiteErrrs.GetFocusedRowIndex()&lt;0)
                                 {
                                   e.processOnServer=false;
                                   alert(&quot;ردیفی انتخاب نشده است&quot;);
                                 }
                                    else
	                                e.processOnServer= confirm('آیا مطمئن از بررسی شدن این  خطاهستید؟');
                                }"></ClientSideEvents>
                                <HoverStyle BackColor="#FFE0C0">
                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                </HoverStyle>
                                <Image  Url="~/Images/icons/Active.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                ID="ASPxButton1" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False">
                                <ClientSideEvents Click="function(s,e){grvWebsiteErrrs.PerformCallback('Print'); }">
                                </ClientSideEvents>
                                <HoverStyle BackColor="#FFE0C0">
                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                </HoverStyle>
                                <Image  Url="~/Images/icons/Printers.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                ID="ASPxButton2" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                OnClick="btnExportExcel_Click">
                                <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                <HoverStyle BackColor="#FFE0C0">
                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                </HoverStyle>
                                <Image  Url="~/Images/icons/ExportExcel.png">
                                </Image>
                            </TSPControls:CustomAspxButton>
                        </td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="TSP.DataManager.WebsiteErrorsManager"
        SelectMethod="GetData"></asp:ObjectDataSource>
    <dx:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="grvWebsiteErrrs">
    </dx:ASPxGridViewExporter>
</asp:Content>
