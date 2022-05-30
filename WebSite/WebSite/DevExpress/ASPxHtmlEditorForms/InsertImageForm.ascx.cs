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
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.ASPxHtmlEditor.Localization;
using DevExpress.Web;

public partial class InsertImageForm : HtmlEditorUserControl {

    protected void Page_Load(object sender, EventArgs e) {
        ckbSaveToServer.ClientVisible = HtmlEditor.Settings.AllowInsertDirectImageUrls && !string.IsNullOrEmpty(HtmlEditor.SettingsImageUpload.UploadImageFolder);
        rblFromThisComputer.ClientEnabled = !string.IsNullOrEmpty(HtmlEditor.SettingsImageUpload.UploadImageFolder);
		Localize();
    }
	void Localize() {
		rblFromTheWeb.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.InsertImage_FromWeb);
		rblFromThisComputer.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.InsertImage_FromLocal);
		lblImageUrl.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.InsertImage_EnterUrl) + ":";
		txbInsertImageUrl.ValidationSettings.RequiredField.ErrorText = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.RequiredFieldError);
		ckbSaveToServer.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.InsertImage_SaveToServer);
		lblBrowse.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.InsertImage_UploadInstructions) + ":";
		ckbMoreImageOptions.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.InsertImage_MoreOptions);
		btnInsertImage.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.ButtonInsert);
		btnChangeImage.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.ButtonChange);
		btnInsertImageCancel.Text = ASPxHtmlEditorLocalizer.GetString(ASPxHtmlEditorStringId.ButtonCancel);
	}
    protected override ASPxEditBase[] GetChildDxEdits() {
        return new ASPxEditBase[] {
			rblFromTheWeb, rblFromThisComputer,
			txbInsertImageUrl, 
            ckbSaveToServer, 
            ckbMoreImageOptions
        };
    }
    protected override ASPxButton[] GetChildDxButtons() {
        return new ASPxButton[] { btnInsertImage, btnChangeImage, btnInsertImage, btnInsertImageCancel };
    }
    protected override ASPxHtmlEditorRoundPanel GetChildDxHtmlEditorRoundPanel() {
        return rpInsertImage;
    }

    protected string SaveUploadFile() {
        string fileName = "";
        if (uplImage.HasFile) {
            string uploadFolder = HtmlEditor.SettingsImageUpload.UploadImageFolder;
            fileName = MapPath(uploadFolder) + uplImage.UploadedFiles[0].FileName;
            try {
                uplImage.UploadedFiles[0].SaveAs(fileName, false);
            } catch (IOException) {
                fileName = MapPath(uploadFolder) + uplImage.GetRandomFileName();
                uplImage.UploadedFiles[0].SaveAs(fileName);
            }
        }
        return Path.GetFileName(fileName);
    }
    protected void uplImage_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs args) {
        try {
            args.CallbackData = SaveUploadFile();
        } catch (Exception e) {
            args.IsValid = false;
            args.ErrorText = e.Message;
        }
    }
}