<%@ Page Title="پیام های کوتاه" Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="RecievedSms.aspx.cs" Inherits="Members_Message_RecievedSms" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <TSPControls:CustomAspxDevGridView ID="GridViewSmsRecived" runat="server" DataSourceID="objdsSMS"
        Width="100%" KeyFieldName="SmsId" AutoGenerateColumns="False" ClientInstanceName="GridViewSmsRecived">
        <Columns>
            <dxwgv:GridViewDataTextColumn Caption="عنوان" FieldName="SmsSubject" VisibleIndex="0"
                Width="200px">
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت تحویل مخابرات" FieldName="DeliverReport" VisibleIndex="0">
                <HeaderStyle Wrap="True" />
                <CellStyle HorizontalAlign="Right" Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="تاریخ" FieldName="SMSDate" VisibleIndex="1">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="زمان" FieldName="SMSTime" Width="70px" VisibleIndex="3">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="بخش" FieldName="PartName" Width="70px" VisibleIndex="4">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
                
            <dxwgv:GridViewDataTextColumn Caption="متن پیام" FieldName="SmsBody" VisibleIndex="5"  Width="400px">
                <HeaderStyle Wrap="True" HorizontalAlign="Center" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="زبان" FieldName="Languages" VisibleIndex="6">
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
                  </dxwgv:GridViewDataTextColumn>
                  <dxwgv:GridViewDataTextColumn Caption="نوع پیام" FieldName="SmsTypeName" VisibleIndex="7">
                <CellStyle Wrap="False">
                </CellStyle>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="وضعیت" FieldName="InActiveState" VisibleIndex="10">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Caption=" " VisibleIndex="12" Width="50px" ShowClearFilterButton="true">
         
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <Settings ShowHorizontalScrollBar="True" ShowFooter="true"></Settings>
    </TSPControls:CustomAspxDevGridView>
    <asp:ObjectDataSource ID="objdsSMS" runat="server" SelectMethod="SelectSMSByRecieverId"
        TypeName="TSP.DataManager.SmsManager">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="RecieverId" Type="Int32" />
             <asp:Parameter DefaultValue="1" Name="IsDelivered" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
