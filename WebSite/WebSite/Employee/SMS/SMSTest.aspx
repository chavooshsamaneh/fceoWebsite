<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="SMSTest.aspx.cs" Inherits="Employee_SMS_SMSTest" %>
	<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <dx:ASPxButton ID="ASPxButton2" runat="server" OnClick="btnTestSending15000SMS_Click"
        Text="btnTestSending15000SMS">
    </dx:ASPxButton>
    <dx:ASPxButton ID="SendNotification" runat="server" OnClick="btnSendNotification_Click" Text="SendNotification">
    </dx:ASPxButton>
    <TSPControls:CustomASPXMemo ID="txtBody" runat="server" Height="71px" Text="test SMS" Width="170px">
    </TSPControls:CustomASPXMemo>
    <dx:ASPxTextBox ID="txtReceiver" runat="server" Text="09177029545" Width="170px">
    </dx:ASPxTextBox>
    <dx:ASPxLabel runat="server" ID="lblWarning" Text="Pending.....">
    </dx:ASPxLabel>
    <br />
    <dx:ASPxButton ID="btnGetDelivery" runat="server" 
        OnClick="btnGetDelivery_Click" Text="Get Delivery">
    </dx:ASPxButton>
    <br />
    <br />
    <dx:ASPxLabel runat="server" ID="lblWarningDelivery" Text="Delivery Report">
    </dx:ASPxLabel>
    <br />
</asp:Content>
