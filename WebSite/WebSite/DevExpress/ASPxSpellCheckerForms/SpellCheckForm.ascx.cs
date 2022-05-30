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
using DevExpress.Web.ASPxSpellChecker;
using DevExpress.Web;
using DevExpress.Web.ASPxSpellChecker.Localization;

public partial class SpellCheckForm : SpellCheckerUserControl {
    protected void Page_Load(object sender, EventArgs e) {
        PrepareCheckedDiv();
        PrepareAddToDictionaryButton();
		Localize();
    }
    protected override ASPxEditBase[] GetChildDxEdits() {
        return new ASPxEditBase[] { txtChangeTo, SCSuggestionsListBox };
    }

    protected override ASPxButton[] GetChildDxButtons() {
        return new ASPxButton[] {
            btnAddToDictionary,
            btnChange,
            btnChangeAll,
            btnClose,
            btnIgnore,
            btnIgnoreAll,
            btnOptions
        };
    }

    protected void PrepareAddToDictionaryButton() {
        //btnAddToDictionary.Enabled = SpellChecker.GetCustomDictionary() != null;
    }
    protected void PrepareCheckedDiv() {
        CheckedTextContainerStyle style = SpellChecker.GetCheckedTextContainerStyle();
        SCCheckedDiv.Attributes.Add("class", style.CssClass);
        CssStyleCollection styles = style.GetStyleAttributes(Page);
        foreach (string key in styles.Keys)
            SCCheckedDiv.Style.Add(key, styles[key]);
    }
	protected void Localize() {
		btnIgnore.Text = ASPxSpellCheckerLocalizer.GetString(ASPxSpellCheckerStringId.IgnoreOnceButton);
		btnIgnoreAll.Text = ASPxSpellCheckerLocalizer.GetString(ASPxSpellCheckerStringId.IgnoreAllButton);
		btnAddToDictionary.Text = ASPxSpellCheckerLocalizer.GetString(ASPxSpellCheckerStringId.AddToDictionaryButton);
		btnChange.Text = ASPxSpellCheckerLocalizer.GetString(ASPxSpellCheckerStringId.ChangeButton);
		btnChangeAll.Text = ASPxSpellCheckerLocalizer.GetString(ASPxSpellCheckerStringId.ChangeAllButton);
		btnOptions.Text = ASPxSpellCheckerLocalizer.GetString(ASPxSpellCheckerStringId.OptionsButton);
		btnClose.Text = ASPxSpellCheckerLocalizer.GetString(ASPxSpellCheckerStringId.CloseButton);
	}
}