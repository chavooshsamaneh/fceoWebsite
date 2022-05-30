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

public partial class PasteFromWordForm : HtmlEditorUserControl {
	protected void Page_Load(object sender, EventArgs e) {
		Localize();
	}
	void Localize() {
		lblDescription.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.PasteRtf_Instructions);
		ckbStripFont.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.PasteRtf_StripFont);
		btnOk.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.ButtonOk);
		btnCancel.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.ButtonCancel);
	}
    protected override ASPxEditBase[] GetChildDxEdits() {
        return new ASPxEditBase[] { 
			lblDescription, ckbStripFont
        };
    }
    protected override ASPxButton[] GetChildDxButtons() {
        return new ASPxButton[] { btnOk, btnCancel };
    }
}