<%@ Page Language="C#" MasterPageFile="~//MasterPageWebsite.master" AutoEventWireup="true" CodeFile="LoginPass.aspx.cs" Inherits="LoginPass" Title="Untitled Page" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="MemberPass" />
    <br /><asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="EmployeePass" /><br />
    <br /><br /><br />
    <asp:Button ID="btnOfficePass" runat="server" OnClick="btnOfficePass_Click" Text="OfficePass" /><br />
    <dx:aspxlabel id="lblMsg" runat="server" text="ASPxLabel"></dx:aspxlabel>
    
</asp:Content>



