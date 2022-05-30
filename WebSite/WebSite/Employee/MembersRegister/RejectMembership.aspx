<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePortals.master" AutoEventWireup="true" CodeFile="RejectMembership.aspx.cs" Inherits="Employee_MembersRegister_RejectMembership" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
        <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>
        [<a class="closeLink" href="#">بستن</a>]
    </div>
    <TSPControls:CustomASPxRoundPanelMenu ID="RoundPanelHeader" runat="server" Width="100%">
        <PanelCollection>
            <dxp:PanelContent>
                <table>
                    <td>
                        <TSPControls:CustomAspxButton CssClass="ButtonMenue" IsMenuButton="true" ID="btnSave" runat="server" EnableTheming="False"
                            EnableViewState="False" OnClick="btnSave_Click" Text="ذخیره" ToolTip="ذخیره" UseSubmitBehavior="False">
                        </TSPControls:CustomAspxButton>
                    </td>

                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanelMenu>
    <br />
    <TSPControls:CustomASPXMemo runat="server" Height="71px" ID="txtMeNo" Caption="لیست اعضا">
        <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
            <ErrorImage Url="~/App_Themes/Glass/Editors/edtError.png">
            </ErrorImage>
            <ErrorFrameStyle ImageSpacing="4px">
                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
            </ErrorFrameStyle>
            <RequiredField ErrorText="اعضا را وارد نمایید" IsRequired="True" />
        </ValidationSettings>
    </TSPControls:CustomASPXMemo>

    <TSPControls:CustomASPXMemo runat="server" Height="71px" ID="txtError" Caption="لیست اعضا ذخیره ناموفق">
       
    </TSPControls:CustomASPXMemo>
    <TSPControls:CustomASPXMemo runat="server" Height="71px" ID="txtComplete" Caption="لیست اعضا ذخیره موفق">
      
    </TSPControls:CustomASPXMemo>
</asp:Content>

