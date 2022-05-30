<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminConfirm.aspx.cs" Inherits="Admin_AdminConfirm"
    MasterPageFile="~/Admin/MasterPage.master" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


 
                <br />
                <div dir="ltr" align="left">
                    <table>
                        <tr>
                            <td width="60px">
                                Username
                            </td>
                            <td>
                                <asp:TextBox AutoCompleteType="Disabled" ID="txtUsername" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table>
                        <tr>
                            <td width="60px">
                                Password
                            </td>
                            <td>
                                <asp:TextBox AutoCompleteType="Disabled" ID="txtPass" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnGenerate" runat="server" OnClick="btnGenerate_Click" Text="Generate" />
                            </td>
                            <td>
                                <asp:Button ID="btnGenerateDefault" runat="server" OnClick="btnGenerateDefault_Click"
                                    Text="Generate with Default values" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
                    <br />
                    <br />
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </div>
            </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
</asp:Content>
