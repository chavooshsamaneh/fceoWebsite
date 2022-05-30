<%@ Page Title="گزارش چاپ کارت های پروانه شرکت های حقوقی" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="OfficePrintingHistory.aspx.cs" Inherits="Employee_Document_Reports_OfficePrintingHistory" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
        Width="100%">
        <PanelCollection>
       <dx:PanelContent>


 
                <table cellpadding="0">
                    <tr>
                        <td align="right" valign="top">
                             <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                ID="btnPrint2" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False">
                                <ClientSideEvents Click="function(s,e){grid.PerformCallback('Print'); }"></ClientSideEvents>
                                <HoverStyle BackColor="#FFE0C0">
                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                </HoverStyle>
                                <Image  Url="~/Images/icons/Printers.png">
                                </Image>
                            </TSPControls:CustomAspxButton >
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
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSPControls:CustomAspxDevGridView ID="GridViewMemberRequest" runat="server" AutoGenerateColumns="False"
        ClientInstanceName="grid" DataSourceID="objdsPrintingHistory" KeyFieldName="PrtHId"
        OnCustomCallback="GridViewMemberRequest_CustomCallback" RightToLeft="True" Width="100%">
        <Columns>
            <dxwgv:GridViewDataTextColumn Caption="کد شرکت" FieldName="Id" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام شرکت" FieldName="Name" VisibleIndex="1"
                Width="200px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="شماره سریال کارت" FieldName="PrtSerialNo"
                VisibleIndex="2" Width="150px">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataComboBoxColumn Caption="وضعیت اعتبار" FieldName="IsValid" Name="IsValid"
                VisibleIndex="3" Width="70px">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="معتبر" Value="0" />
                        <dx:ListEditItem Text="نامعتبر" Value="1" />
                        <dx:ListEditItem Text="باطل شده" Value="2" />
                    </Items>
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataTextColumn Caption="نام کاربر" FieldName="UserFullName" VisibleIndex="3"
                Width="150px">
                <CellStyle Wrap="false">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Width="80px" Caption="تاریخ چاپ" FieldName="CreateDate"
                VisibleIndex="4">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Width="100px" Caption="ساعت چاپ" FieldName="CreateTime"
                VisibleIndex="5">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" VisibleIndex="5"
                Width="150px">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="6" Width="50px" ShowClearFilterButton="true">
            
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowHorizontalScrollBar="True" />
        <ClientSideEvents EndCallback="function(s, e) {
	 if(s.cpPrint==1)
        {
            window.open('../../../Print.aspx');
           s.cpPrint=0;
        }
}" />
    </TSPControls:CustomAspxDevGridView>
    <br />
    <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
        Width="100%">
        <PanelCollection>
           <dx:PanelContent>


   
                <table cellpadding="0">
                    <tr>
                        <td align="right" valign="top">
                           <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="چاپ"
                                ID="btnPrint" AutoPostBack="False" UseSubmitBehavior="False" EnableViewState="False"
                                EnableTheming="False">
                                <ClientSideEvents Click="function(s,e){grid.PerformCallback('Print'); }"></ClientSideEvents>
                                <HoverStyle BackColor="#FFE0C0">
                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                </HoverStyle>
                                <Image  Url="~/Images/icons/Printers.png">
                                </Image>
                            </TSPControls:CustomAspxButton  >
                        </td>
                        <td align="right" valign="top">
                            <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" "  ToolTip="خروجی Excel"
                                ID="btnExportExcel" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False"
                                OnClick="btnExportExcel_Click">
                                <ClientSideEvents Click="function(s,e){ }"></ClientSideEvents>
                                <HoverStyle BackColor="#FFE0C0">
                                    <Border BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></Border>
                                </HoverStyle>
                                <Image  Url="~/Images/icons/ExportExcel.png">
                                </Image>
                            </TSPControls:CustomAspxButton  >
                        </td>
                    </tr>
                </table>
        </dx:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <asp:ObjectDataSource ID="objdsPrintingHistory" runat="server" TypeName="TSP.DataManager.PrintingHistoryManager"
        SelectMethod="FindByTableType">
        <SelectParameters>
            <asp:Parameter DbType="Int32" DefaultValue="417" Name="TableType" />
            <asp:Parameter DbType="Int32" DefaultValue="-1" Name="TableTypeMe" />
            <asp:Parameter DbType="Int32" DefaultValue="-1" Name="TableTypeEngOff" />
            <asp:Parameter DbType="Int32" DefaultValue="-1" Name="TableTypeOffice" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <dx:ASPxGridViewExporter ID="GridViewExporter" runat="server" GridViewID="GridViewMemberRequest">
    </dx:ASPxGridViewExporter>
</asp:Content>
