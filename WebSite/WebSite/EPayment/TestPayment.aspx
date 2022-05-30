<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true"
    CodeFile="TestPayment.aspx.cs" Inherits="EPayment_TestPayment" %>

<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="center" width="100%">
        <table>
            <tr>
                <td>
                merchantId
                </td>
                <td>
                    <TSPControls:CustomTextBox runat="server" ID="txtMerchantId">
                    </TSPControls:CustomTextBox>
                </td>
            </tr>
            <tr>
                <td>
                ReferenceId
                </td>
                <td>
                    <TSPControls:CustomTextBox runat="server" ID="txtReferenceId">
                    </TSPControls:CustomTextBox>
                </td>
            </tr>
            <tr>
            <td>
            <dxe:ASPxLabel runat="server" ID="lblMessage" Text="---"></dxe:ASPxLabel>
            </td>
            </tr>
        </table>
        <TSPControls:CustomAspxButton  runat="server" Text="پاسخ بانک" Width="160px" ID="btnEPayment" EncodeHtml="false"
            OnClick="btnBankReply_Click" 
             UseSubmitBehavior="false" CausesValidation="False" Visible="true">
            <Image Width="20px" Height="20px" Url="~/Images/icons/Empayment.png" />
        </TSPControls:CustomAspxButton>
    </div>
</asp:Content>
