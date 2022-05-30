<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dxwsc" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxp" %>
	<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpellCheckForm.ascx.cs" Inherits="SpellCheckForm" %>
<table id="mainSpellCheckFormTable" cellpadding="0" cellspacing="0" class="mainSpellCheckFormTable"> 
    <tr>
        <td class="contentSCFormContainer">
            <table id="spellCheckForm" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2">
						<% =DevExpress.Web.ASPxSpellChecker.Localization.ASPxSpellCheckerLocalizer.GetString(DevExpress.Web.ASPxSpellChecker.Localization.ASPxSpellCheckerStringId.NotInDictionary)%>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="checkedDivContainer">
                        <div id="SCCheckedDiv" runat="server">
                        </div>
                    </td>
                    <td valign="top" class="buttonTableContainer">
                        <table id="topButtonsTable" class="buttonsTable" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnIgnore" runat="server" Width="100%" AutoPostback="false" UseSubmitBehavior="false">
                                    <ClientSideEvents Click="function(s, e) {aspxSCIgnore();}"/>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="verticalSeparator">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnIgnoreAll" runat="server" Width="100%" AutoPostback="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s, e) {aspxSCIgnoreAll();}"/>
                                    </TSPControls:CustomAspxButton>                    
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="verticalSeparator">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnAddToDictionary" runat="server" Width="100%" AutoPostback="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s, e) {aspxSCAddToDictionary();}"/>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="changeToText">
                        <% =DevExpress.Web.ASPxSpellChecker.Localization.ASPxSpellCheckerLocalizer.GetString(DevExpress.Web.ASPxSpellChecker.Localization.ASPxSpellCheckerStringId.ChangeTo)%>
                    </td>
                </tr>
                <tr>
                    <td valign="top" style="width:380px;">
                        <dxp:ASPxPanel ID="ChangeToPanel" runat="server" Width="100%" DefaultButton="btnChange">
                            <PanelCollection>
                                <dxp:PanelContent ID="PanelContent1" runat="server">
                                    <table style="width:100%" cellpadding="0" cellspacing="0px">
                                        <tr>
                                            <td valign="top" style="width:100%" class="verticalSeparator">
                                                <TSPControls:CustomTextBox IsMenuButton="true" ID="txtChangeTo" runat="server" Width="100%" ClientInstanceName="_dxeSCTxtChangeTo">
                                                    <ClientSideEvents 
                                                        KeyPress="function(s, e) {aspxSCTextBoxKeyPress(s, e);}"
                                                        KeyDown="function(s, e) {aspxSCTextBoxKeyDown(s,e);}"
                                                    />
                                                </TSPControls:CustomTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" class="listBoxContainer">
                                                <TSPControls:CustomASPxListBox runat="server" ID="SCSuggestionsListBox" ClientInstanceName="_dxeSCSuggestionsListBox" width="100%" Height="100px">
                                                    <ClientSideEvents 
                                                    ItemDoubleClick="function(s, e) {aspxSCListBoxItemDoubleClick(s, e);}"
                                                    SelectedIndexChanged="function(s, e) {aspxSCListBoxItemChanged(s, e);}"
                                                    />
                                                </TSPControls:CustomASPxListBox> 
                                            </td>
                                        </tr>
                                    </table>                                
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxp:ASPxPanel>
                    </td>
                    <td valign="top" class="buttonTableContainer">
                        <table id="bottomButtonsTable" class="buttonsTable" cellpadding="0px" cellspacing="0">
                            <tr>
                                <td valign="top">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChange" runat="server" Width="100%" ClientInstanceName="_dxeSCBtnChange" AutoPostback="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s, e) { aspxSCChange();}"/>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="verticalSeparator">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnChangeAll" runat="server" Width="100%" ClientInstanceName="_dxeSCBtnChangeAll" AutoPostback="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s, e) { aspxSCChangeAll();}"/>
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </td>
        </tr>
        <tr class="footerBackground">
            <td>
                <table cellpadding="0" cellspacing="0" style="width:100%;">
                    <tr>
                        <td align="right" class="leftBottomButton">
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnOptions" runat="server" Width="100px" AutoPostback="false" UseSubmitBehavior="false">
                                <ClientSideEvents Click="function(s, e) {aspxSCShowOptionsForm(true);}"/>
                            </TSPControls:CustomAspxButton>
                        </td>
                        <td align="left" class="rightBottomButton">
                            <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnClose" runat="server" Width="100%" AutoPostback="false" UseSubmitBehavior="false">
                                <ClientSideEvents Click="function(s, e) {aspxSCDialogComplete(false);}"/>
                            </TSPControls:CustomAspxButton>
                        </td>                
                    </tr>
                </table>
            </td>
        </tr>
</table>
