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

public partial class SpellCheckOptionsForm : SpellCheckerUserControl {
    protected void Page_Load(object sender, EventArgs e) {
        PrepareFormCheckBoxes();
        PrepareLanguagesCombo();
		Localize();
    }

    protected override ASPxEditBase[] GetChildDxEdits() {
        return new ASPxEditBase[] { chkbEmails, 
                                    chkbMixedCase, 
                                    chkbNumbers, 
                                    chkbUpperCase, 
                                    chkbUrls, 
                                    comboLanguage, 
                                    chkbTags 
        };
    }

    protected override ASPxButton[] GetChildDxButtons() {
        return new ASPxButton[] {
            btnCancel,
            btnOK
        };
    }

    protected void PrepareFormCheckBoxes() {
        ASPxSpellChecker spellChecker = ((ASPxSpellChecker)NamingContainer);
        chkbEmails.Checked = spellChecker.SettingsSpelling.IgnoreEmails;
        chkbMixedCase.Checked = spellChecker.SettingsSpelling.IgnoreMixedCaseWords;
        chkbNumbers.Checked = spellChecker.SettingsSpelling.IgnoreWordsWithNumber;
        chkbUpperCase.Checked = spellChecker.SettingsSpelling.IgnoreUpperCaseWords;
        chkbUrls.Checked = spellChecker.SettingsSpelling.IgnoreUrls;
        chkbTags.Checked = spellChecker.SettingsSpelling.IgnoreMarkupTags;
    }
    protected void PrepareLanguagesCombo() {
        comboLanguage.Items.Clear();
        for (int i = 0; i < SpellChecker.Dictionaries.Count; i++) {
            if (comboLanguage.Items.FindByText(SpellChecker.Dictionaries[i].GetCulture().DisplayName) == null)
                comboLanguage.Items.Add(SpellChecker.Dictionaries[i].GetCulture().DisplayName);
        }
        comboLanguage.SelectedItem = comboLanguage.Items.FindByText(SpellChecker.GetCulture().DisplayName);
    }
	protected void Localize() {
		pnlOptions.HeaderText = ASPxSpellCheckerLocalizer.GetString(ASPxSpellCheckerStringId.GeneralOptions);
		chkbUpperCase.Text = ASPxSpellCheckerLocalizer.GetString(ASPxSpellCheckerStringId.IgnoreUppercase);
        chkbMixedCase.Text = ASPxSpellCheckerLocalizer.GetString(ASPxSpellCheckerStringId.IgnoreMixedCase);
        chkbNumbers.Text = ASPxSpellCheckerLocalizer.GetString(ASPxSpellCheckerStringId.IgnoreNumbers);
		chkbEmails.Text = ASPxSpellCheckerLocalizer.GetString(ASPxSpellCheckerStringId.IgnoreEmails);
        chkbUrls.Text = ASPxSpellCheckerLocalizer.GetString(ASPxSpellCheckerStringId.IgnoreUrls);
		chkbTags.Text = ASPxSpellCheckerLocalizer.GetString(ASPxSpellCheckerStringId.IgnoreTags);
		pnlLanguageSelection.HeaderText = ASPxSpellCheckerLocalizer.GetString(ASPxSpellCheckerStringId.InternationalDictionaries);
		btnOK.Text = ASPxSpellCheckerLocalizer.GetString(ASPxSpellCheckerStringId.OkButton);
		btnCancel.Text = ASPxSpellCheckerLocalizer.GetString(ASPxSpellCheckerStringId.CancelButton);
	}
}
