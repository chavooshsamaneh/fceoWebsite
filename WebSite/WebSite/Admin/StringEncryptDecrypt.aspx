<%@ Page Title="رمزگذاری و رمزگشایی" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="StringEncryptDecrypt.aspx.cs" Inherits="Admin_StringEncryptDecrypt" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
         <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                    <table Width="100%">
                        <tr>
                            <td width="15px">
                                String
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtStr" AutoCompleteType="Disabled" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnEncrypt" runat="server" Text="Encrypt" OnClick="btnEncrypt_Click" />
                            </td>
                            <td width="150px">
                            </td>
                            <td>
                                <asp:Button ID="btnDecrypt" runat="server" Text="Decrypt" OnClick="btnDecrypt_Click"  />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelResult" runat="server" Visible="false">
                        <br />
                        <br />
                        <asp:Label ID="lblResult" runat="server"></asp:Label></asp:Panel>
                    <br />
              </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>

</asp:Content>
