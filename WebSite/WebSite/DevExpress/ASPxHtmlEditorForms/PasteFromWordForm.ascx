<%--
{************************************************************************************}
{                                                                                    }
{   DO NOT MODIFY THIS FILE!                                                         }
{                                                                                    }
{   It will be overwritten without prompting when a new version becomes              }
{   available. All your changes will be lost.                                        }
{                                                                                    }
{   This file contains the default template and is required for the form             }
{   rendering. Improper modifications may result in incorrect behavior of            }
{   dialog forms.                                                                    }
{                                                                                    }
{************************************************************************************}
--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PasteFromWordForm.ascx.cs" Inherits="PasteFromWordForm" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dxhe" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxp" %>
	<%@ Register Assembly="TSP.WebControls" Namespace="TSP.WebControls" TagPrefix="TSPControls" %>
<dxp:ASPxPanel ID="MainPanel" runat="server" Width="100%" DefaultButton="btnOk" RenderMode="Table">
    <PanelCollection>
        <dxp:PanelContent ID="PanelContent1" runat="server">
            <table cellpadding="0" cellspacing="0" id="pasteFromWordForm">
                <tr>
                    <td class="contentCell">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="lblDescription" runat="server">
                                    </dxe:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td class="pasteContainerCell">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <iframe id='<% =HtmlEditor.ClientID + "_dxePasteFromWordContainer"%>' class="pasteContainer">
                                                </iframe>                                            
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="checkBoxCell">
                                    <TSPControls:CustomASPxCheckBox ID="ckbStripFont" runat="server" ClientInstanceName="_dxeCkbStripFont">
                                    </TSPControls:CustomASPxCheckBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="buttonsCell" align="right">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnOk" runat="server" AutoPostBack="False" Height="23px"
                                        Width="74px" ClientInstanceName="_dxeBtnOk" CausesValidation="False" >
                                        <clientsideevents click="function(s, e) {OnOkButtonClick_PasteFromWordForm();}" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                                <td class="cancelButton">
                                    <TSPControls:CustomAspxButton IsMenuButton="true" ID="btnCancel" runat="server" AutoPostBack="False"
                                        Height="23px" Width="74px" CausesValidation="False" >
                                        <clientsideevents click="function(s, e) {OnCancelButtonClick_PasteFromWordForm();}" />
                                    </TSPControls:CustomAspxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>        
        </dxp:PanelContent>
    </PanelCollection>
</dxp:ASPxPanel>
<script type="text/javascript" id="dxss_PasteFromWordForm">
    function OnOkButtonClick_PasteFromWordForm() {
        var res = {
            html: _aspxIFrameDocumentBody('<% =HtmlEditor.ClientID + "_dxePasteFromWordContainer"%>').innerHTML,
            stripFontFamily: _dxeCkbStripFont.GetChecked()
        };
        aspxDialogComplete(1, res);
    }
    function OnCancelButtonClick_PasteFromWordForm() {
        aspxDialogComplete(0, null);
    }
</script>