<%@ Page Language="C#" MasterPageFile="~/MasterPagePortals.master"
    AutoEventWireup="true" CodeFile="IntroductionInsert.aspx.cs"
    Inherits="Employee_Management_IntroductionInsert" Title="مدیریت معرفی سازمان" %>

<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

	<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
            <div style="text-align: right" id="DivReport" class="DivErrors" runat="server">
                <asp:Label ID="LabelWarning" runat="server" Text="Label"></asp:Label>[<a class="closeLink"
                    href="#">بستن</a>]
            </div>
               <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu1" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>


  
                                <table style="display: block; vertical-align: middle; overflow: hidden; border-collapse: collapse" id="tableCallback" cellpadding="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td align="right">
                                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 30px">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره" CausesValidation="False" ID="btnSave" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnSaveClient2" OnClick="btnSave_Click">
                                                                    <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>

                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                    </HoverStyle>

                                                                    <Image  Url="~/Images/icons/save.png"></Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                         </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
            <br />
            <TSPControls:CustomASPxRoundPanel ID="RoundPanelMemberInfo" HeaderText="معرفی سازمان" runat="server"
        Width="100%">
        <PanelCollection>
            <dxp:PanelContent>


            
                            <table style="width: 100%; direction: rtl">
                                <tbody>
                                    <tr>
                                        <td align="right" colspan="4" valign="top">
                                            <dxe:ASPxLabel runat="server" Text="متن معرفی" ID="labelMeId"></dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4" valign="top">&nbsp;
                                            <TSPControls:CustomASPxHtmlEditor runat="server" ID="txtIntText"></TSPControls:CustomASPxHtmlEditor>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>        </dxp:PanelContent>
        </PanelCollection>
    </TSPControls:CustomASPxRoundPanel>
                      
            <br />
                                   <TSPControls:CustomASPxRoundPanelMenu ID="CustomASPxRoundPanelMenu2" runat="server"
                    Width="100%">
                    <PanelCollection>
                        <dxp:PanelContent>
                              
                                                <table style="border-collapse: collapse; background-color: transparent" dir="rtl" cellpadding="0">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 30px">
                                                                <TSPControls:CustomAspxButton IsMenuButton="true" runat="server" Text=" " ToolTip="ذخیره" CausesValidation="False" ID="btnSave2" EnableClientSideAPI="True" UseSubmitBehavior="False" EnableViewState="False" EnableTheming="False" ClientInstanceName="btnSaveClient2" OnClick="btnSave_Click">
                                                                    <ClientSideEvents Click="function(s, e) {

}"></ClientSideEvents>

                                                                    <HoverStyle BackColor="#FFE0C0">
                                                                        <border borderwidth="1px" borderstyle="Outset" bordercolor="Gray"></border>
                                                                    </HoverStyle>

                                                                    <Image  Url="~/Images/icons/save.png"></Image>
                                                                </TSPControls:CustomAspxButton>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                   


  </dxp:PanelContent>
                    </PanelCollection>
                </TSPControls:CustomASPxRoundPanelMenu>
          
</asp:Content>

