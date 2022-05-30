using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TSP.WebControls
{
    public class CustomASPxHtmlEditor : DevExpress.Web.ASPxHtmlEditor.ASPxHtmlEditor
    {
        public CustomASPxHtmlEditor()
        {
            //this.Theme = "Metropolis";

            //this.ApplyTheme("Metropolis");
            //this.CssPostfix = "Metropolis";
            //this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            //this.Images.sprite = "~/App_Themes/Metropolis/{0}/sprite.css";

            //this.Theme = "Metropolis";
            //this.ApplyTheme("Metropolis");
            //this.CssPostfix = "Metropolis";
            //this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            //this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";

            RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            SettingsLoadingPanel.ImagePosition = DevExpress.Web.ImagePosition.Right;
            SettingsLoadingPanel.Text = "در حال بارگذاری";
            SettingsText.DesignViewTab = "طراحی";
            SettingsText.PreviewTab = "مشاهده";
            Settings.AllowHtmlView = true;
            SettingsHtmlEditing.AllowIFrames = true;
            Width = Unit.Percentage(100);
            #region Style   
            //this.Theme = ControlsStyle.WebSiteThemeCssPostfix;
            //this.ApplyTheme(ControlsStyle.WebSiteThemeCssPostfix);
            //CssFilePath = "~/App_Themes/MetropolisBlue/{0}/styles.css";
            //CssPostfix = "MetropolisBlue";
            //Styles.CssFilePath = "~/App_Themes/MetropolisBlue/{0}/styles.css";
            //Styles.CssPostfix = "MetropolisBlue";
            //Styles.ViewArea.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#4986A2");
            //Images.SpriteCssFilePath = "~/App_Themes/MetropolisBlue/{0}/sprite.css";
            //Images.LoadingPanel.Url = "~/App_Themes/MetropolisBlue/HtmlEditor/Loading.gif";
            //ImagesFileManager.FolderContainerNodeLoadingPanel.Url = "~/App_Themes/MetropolisBlue/Web/tvNodeLoading.gif";
            //ImagesFileManager.LoadingPanel.Url = "~/App_Themes/MetropolisBlue/Web/Loading.gif";
            #endregion

            #region ContextMenuItems
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("Cut", "cut"));
            ContextMenuItems["cut"].BeginGroup = true;
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("Copy", "copy"));
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("Paste", "paste"));
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("انتخاب همه", "selectall"));
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("تغییر لینک ...", "changelinkdialog"));
            ContextMenuItems["changelinkdialog"].BeginGroup = true;
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("تغییر تصویر ...", "changeimagedialog"));
            ContextMenuItems["changeimagedialog"].BeginGroup = true;
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("تنظیمات جدول ...", "tablepropertiesdialog"));
            ContextMenuItems["tablepropertiesdialog"].BeginGroup = true;
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("تنظیمات ستون ...", "tablecolumnpropertiesdialog"));
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("تنظیمات سلول ...", "tablecellpropertiesdialog"));
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("درج ردیف در بالا", "inserttablerowabove"));
            ContextMenuItems["inserttablerowabove"].BeginGroup = true;
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("درج ردیف در پایین", "inserttablerowbelow"));
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("درج ستون در چپ", "inserttablecolumntoleft"));
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("درج ستون در راست", "inserttablecolumntoright"));
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("تقسیم افقی", "splittablecellhorizontally"));
            ContextMenuItems["splittablecellhorizontally"].BeginGroup = true;
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("تقسیم عمودی", "splittablecellvertically"));
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("Merge Right", "mergetablecellright"));
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("Merge Down", "mergetablecelldown"));
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("حذف جدول", "deletetable"));
            ContextMenuItems["deletetable"].BeginGroup = true;
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("حذف ردیف", "deletetablerow"));
            ContextMenuItems.Add(new DevExpress.Web.ASPxHtmlEditor.HtmlEditorContextMenuItem("حذف ستون", "deletetablecolumn"));
            #endregion

            #region Toolbars
            #region StandardToolbar1
            DevExpress.Web.ASPxHtmlEditor.HtmlEditorToolbar StandardToolbar1 = new DevExpress.Web.ASPxHtmlEditor.HtmlEditorToolbar("StandardToolbar1");

            StandardToolbar1.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarCutButton());
            StandardToolbar1.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarCopyButton());
            StandardToolbar1.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarPasteButton());
            StandardToolbar1.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarPasteFromWordButton());
            StandardToolbar1.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarUndoButton(true));
            StandardToolbar1.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarRedoButton());
            StandardToolbar1.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarRemoveFormatButton(true));
            StandardToolbar1.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarSuperscriptButton(true));
            StandardToolbar1.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarSubscriptButton());
            StandardToolbar1.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarInsertOrderedListButton(true));
            StandardToolbar1.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarInsertUnorderedListButton());
            StandardToolbar1.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarIndentButton(true));
            StandardToolbar1.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarOutdentButton());
            StandardToolbar1.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarInsertLinkDialogButton(true));
            StandardToolbar1.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarUnlinkButton());
            StandardToolbar1.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarInsertImageDialogButton());

            DevExpress.Web.ASPxHtmlEditor.ToolbarTableOperationsDropDownButton ToolbarTableOperationsDropDownButton = new DevExpress.Web.ASPxHtmlEditor.ToolbarTableOperationsDropDownButton(true);
            ToolbarTableOperationsDropDownButton.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarInsertTableDialogButton(true));
            ToolbarTableOperationsDropDownButton.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarTablePropertiesDialogButton(true));
            ToolbarTableOperationsDropDownButton.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarTableRowPropertiesDialogButton());
            ToolbarTableOperationsDropDownButton.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarTableColumnPropertiesDialogButton());
            ToolbarTableOperationsDropDownButton.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarTableCellPropertiesDialogButton());
            ToolbarTableOperationsDropDownButton.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarInsertTableRowAboveButton(true));
            ToolbarTableOperationsDropDownButton.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarInsertTableRowBelowButton());
            ToolbarTableOperationsDropDownButton.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarInsertTableColumnToLeftButton());
            ToolbarTableOperationsDropDownButton.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarInsertTableColumnToRightButton());
            ToolbarTableOperationsDropDownButton.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarSplitTableCellHorizontallyButton(true));
            ToolbarTableOperationsDropDownButton.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarSplitTableCellVerticallyButton());
            ToolbarTableOperationsDropDownButton.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarMergeTableCellRightButton());
            ToolbarTableOperationsDropDownButton.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarMergeTableCellDownButton());
            ToolbarTableOperationsDropDownButton.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarDeleteTableButton(true));
            ToolbarTableOperationsDropDownButton.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarDeleteTableRowButton());
            ToolbarTableOperationsDropDownButton.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarDeleteTableColumnButton());
            StandardToolbar1.Items.Add(ToolbarTableOperationsDropDownButton);

            //StandardToolbar1.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarFullscreenButton(true));
            Toolbars.Add(StandardToolbar1);
            #endregion
            #region StandardToolbar2
            DevExpress.Web.ASPxHtmlEditor.HtmlEditorToolbar StandardToolbar2 = new DevExpress.Web.ASPxHtmlEditor.HtmlEditorToolbar("StandardToolbar2");

            DevExpress.Web.ASPxHtmlEditor.ToolbarParagraphFormattingEdit ToolbarParagraphFormattingEdit = new DevExpress.Web.ASPxHtmlEditor.ToolbarParagraphFormattingEdit();
            ToolbarParagraphFormattingEdit.Width = System.Web.UI.WebControls.Unit.Pixel(120);
            ToolbarParagraphFormattingEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("Normal", "p"));
            ToolbarParagraphFormattingEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("Heading  1", "h1"));
            ToolbarParagraphFormattingEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("Heading  2", "h2"));
            ToolbarParagraphFormattingEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("Heading  3", "h3"));
            ToolbarParagraphFormattingEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("Heading  4", "h4"));
            ToolbarParagraphFormattingEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("Heading  5", "h5"));
            ToolbarParagraphFormattingEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("Heading  6", "h6"));
            ToolbarParagraphFormattingEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("Address", "address"));
            //ToolbarParagraphFormattingEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("Normal (DIV)", "div"));
            StandardToolbar2.Items.Add(ToolbarParagraphFormattingEdit);

            DevExpress.Web.ASPxHtmlEditor.ToolbarFontNameEdit ToolbarFontNameEdit = new DevExpress.Web.ASPxHtmlEditor.ToolbarFontNameEdit();
            ToolbarFontNameEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("IRANSans", "IRANSans"));
            ToolbarFontNameEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("B Nazanin", "B Nazanin"));
            ToolbarFontNameEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("B Titr", "B Titr"));
            StandardToolbar2.Items.Add(ToolbarFontNameEdit);

            DevExpress.Web.ASPxHtmlEditor.ToolbarFontSizeEdit ToolbarFontSizeEdit = new DevExpress.Web.ASPxHtmlEditor.ToolbarFontSizeEdit();
            ToolbarFontSizeEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("1 (8pt)", "1"));
            ToolbarFontSizeEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("2 (10pt)", "2"));
            ToolbarFontSizeEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("3 (12pt)", "3"));
            ToolbarFontSizeEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("4 (14pt)", "4"));
            ToolbarFontSizeEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("5 (18pt)", "5"));
            ToolbarFontSizeEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("6 (24pt)", "6"));
            ToolbarFontSizeEdit.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarListEditItem("7 (36pt)", "7"));
            StandardToolbar2.Items.Add(ToolbarFontSizeEdit);

            StandardToolbar2.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarBoldButton(true));
            StandardToolbar2.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarItalicButton());
            StandardToolbar2.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarUnderlineButton());
            StandardToolbar2.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarStrikethroughButton());
            StandardToolbar2.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarJustifyRightButton(true));
            StandardToolbar2.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarJustifyCenterButton());
            StandardToolbar2.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarJustifyLeftButton());
            StandardToolbar2.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarBackColorButton(true));
            StandardToolbar2.Items.Add(new DevExpress.Web.ASPxHtmlEditor.ToolbarFontColorButton());
            Toolbars.Add(StandardToolbar2);
            #endregion
            #endregion

            this.SettingsDialogs.InsertImageDialog.SettingsImageUpload.UploadFolderUrlPath = "/Image/HtmlEditor/";
            this.SettingsDialogs.InsertImageDialog.SettingsImageUpload.FileSystemSettings.UploadFolder = "/Image/HtmlEditor/";
            this.SettingsDialogs.InsertImageDialog.SettingsImageSelector.CommonSettings.RootFolder = "/Image/HtmlEditor/";
        }
    }
}
