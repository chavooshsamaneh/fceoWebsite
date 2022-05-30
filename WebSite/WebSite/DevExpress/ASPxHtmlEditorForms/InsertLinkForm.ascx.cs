/*
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
*/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.ASPxHtmlEditor.Localization;
using DevExpress.Web;

public partial class InsertLinkFrom : HtmlEditorUserControl {
	protected void Page_Load(object sender, EventArgs e) {
		Localize();
	}
	void Localize() {
		rblLinkType.Items.Add(ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.InsertLink_Url), "URL");
		rblLinkType.Items.Add(ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.InsertLink_Email), "Email");
		lblUrl.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.InsertLink_Url) + ":";
		txbURL.ValidationSettings.RequiredField.ErrorText = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.RequiredFieldError);
		lblEmailTo.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.InsertLink_EmailTo) + ":";
		txbEmailTo.ValidationSettings.RequiredField.ErrorText = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.RequiredFieldError);
		lblSubject.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.InsertLink_Subject) + ":";
		lblLinkDisplay.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.InsertLink_DisplayProperties);
		lblText.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.InsertLink_Text) + ":";
		lblToolTip.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.InsertLink_ToolTip) + ":";
		ckbOpenInNewWindow.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.InsertLink_OpenInNewWindow);
		btnOk.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.ButtonOk);
		btnChange.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.ButtonChange);
		btnCancel.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.ButtonCancel);
	}

    protected override ASPxEditBase[] GetChildDxEdits() {
        return new ASPxEditBase[] { 
			rblLinkType, 
			lblUrl, txbURL,
			lblEmailTo, txbEmailTo,
			lblSubject, txbSubject,
            lblLinkDisplay, lblText,
            txbText, lblToolTip,
            txbToolTip, ckbOpenInNewWindow
        };
    }
    protected override ASPxButton[] GetChildDxButtons() {
        return new ASPxButton[] { btnOk, btnCancel, btnChange };
    }
    protected override ASPxHtmlEditorRoundPanel GetChildDxHtmlEditorRoundPanel() {
        return rpInsertLink;
    }
}