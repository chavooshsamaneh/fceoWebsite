<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxp" %>
	<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpellCheckOptionsForm.ascx.cs" Inherits="SpellCheckOptionsForm" %>
<table id="mainSpellCheckOptionsFormTable" cellpadding="0" cellspacing="0" class="mainSpellCheckOptionsFormTable">
    <tr>
        <td class="contentSCOptionsFormContainer">
            <table id="optionsForm" cellpadding="0px" cellspacing="0px" style="width:100%">
                <tr>
                    <td>
                        <dxrp:ASPxRoundPanel ID="pnlOptions" runat="server" Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomASPxCheckBox id="chkbUpperCase" ClientInstanceName="chkbUpperCase" runat="server"></TSPControls:CustomASPxCheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomASPxCheckBox id="chkbMixedCase" ClientInstanceName="chkbMixedCase" runat="server"></TSPControls:CustomASPxCheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomASPxCheckBox id="chkbNumbers" ClientInstanceName="chkbNumbers" runat="server"></TSPControls:CustomASPxCheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomASPxCheckBox id="chkbEmails" ClientInstanceName="chkbEmails" runat="server"></TSPControls:CustomASPxCheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <TSPControls:CustomASPxCheckBox id="chkbUrls" ClientInstanceName="chkbUrls" runat="server"></TSPControls:CustomASPxCheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                               <TSPControls:CustomASPxCheckBox id="chkbTags" ClientInstanceName="chkbTags" runat="server"></TSPControls:CustomASPxCheckBox>
                                            </td>
                                        </tr>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxrp:ASPxRoundPanel>
                    </td>
                </tr>
                <tr>
                    <td class="languagePanel">
                        <dxrp:ASPxRoundPanel ID="pnlLanguageSelection" runat="server" Width="100%">
                            <PanelCollection>
                                <dxp:PanelContent runat="server">
                                    <table style="width:100%;">
                                        <tr>
                                            <td colspan="2">
                                                <% =DevExpress.Web.ASPxSpellChecker.Localization.ASPxSpellCheckerLocalizer.GetString(DevExpress.Web.ASPxSpellChecker.Localization.ASPxSpellCheckerStringId.ChooseDictionary)%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <% =DevExpress.Web.ASPxSpellChecker.Localization.ASPxSpellCheckerLocalizer.GetString(DevExpress.Web.ASPxSpellChecker.Localization.ASPxSpellCheckerStringId.Language)%>
                                            </td>
                                            <td align="left" style="width:70%;">
                                                <TSPControls:CustomAspxComboBox ID="comboLanguage" ClientInstanceName="comboLanguage" runat="server" Width="100%"></TSPControls:CustomAspxComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxrp:ASPxRoundPanel>
                    </td>
                </tr>  
            </table>        
        </td>
    </tr>
    <tr class="footerBackground">
        <td>
            <table width="100%" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td class="leftBottomButton" align="right">
                        <TSPControls:CustomAspxButton IsMenuButton="true" id="btnOK" runat="server" AutoPostBack="false" Width="100px" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s, e) {aspxSCDialogComplete(true)}"/>
                        </TSPControls:CustomAspxButton>
                    </td>
                    <td class="rightBottomButton">
                        <TSPControls:CustomAspxButton IsMenuButton="true" id="btnCancel" runat="server" AutoPostBack="false" Width="100px" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s, e) {aspxSCDialogComplete(false)}"/>                        
                        </TSPControls:CustomAspxButton>
                    </td>
                </tr>
            </table>                
        </td>
    </tr>
</table>

