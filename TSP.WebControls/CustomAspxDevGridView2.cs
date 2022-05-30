using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Web;


[assembly: TagPrefix("TSP.WebControls", "TSPControls")]
namespace TSP.WebControls
{
    //this.CssFilePath = "~/App_Themes/MetropolisBlue/{0}/styles.css";
    // this.CssPostfix = "MetropolisBlue";
    public class CustomAspxDevGridView2 : DevExpress.Web.ASPxGridView
    {
        public CustomAspxDevGridView2()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.Images.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            //this.Styles.CssPostfix = "MetropolisBlue";
            this.SettingsText.CommandCancel = "انصراف";
            this.SettingsText.CommandClearFilter = "پاک کردن فیلتر";
            this.SettingsText.CommandDelete = "حذف";
            this.SettingsText.CommandEdit = "ویرایش";
            this.SettingsText.CommandNew = "جدید";
            this.SettingsText.CommandSelect = "انتخاب";
            this.SettingsText.CommandUpdate = "ذخیره";
            this.SettingsText.ConfirmDelete = "آیا مطمئن به حذف این ردیف هستید؟";
            this.SettingsText.GroupPanel = "برای گروه بندی از این قسمت استفاده کنید";
            this.SettingsText.PopupEditFormCaption = "تغییر رکورد";
            this.SettingsText.EmptyDataRow = "هیچ داده ای وجود ندارد";
            this.SettingsPager.AllButton.Text = "همه رکوردها";
            this.SettingsPager.FirstPageButton.Text = "اولین صفحه";
            this.SettingsPager.LastPageButton.Text = "آخرین صفحه";
            this.SettingsPager.NextPageButton.Text = "صفحه بعد";
            this.SettingsPager.PrevPageButton.Text = "صفحه قبل";
            //this.SettingsPager.Summary.AllPagesText = "صفحات: {0} - {1} ({2} مورد)";//"Pages: {0} - {1} ({2} items)";
            this.SettingsPager.Summary.Text = "صفحه: {0} از {1} (تعداد کل:{2})";//"Pages: {0} - {1} ({2} items)";
            this.Settings.ShowFilterRow = false;
            this.SettingsBehavior.AllowDragDrop = false;
            this.SettingsBehavior.AllowSort = false;
            this.Settings.ShowGroupPanel = false;
            this.SettingsBehavior.AllowFocusedRow = true;
            this.SettingsBehavior.AllowGroup = false;
            this.SettingsBehavior.ConfirmDelete = true;
            this.SettingsBehavior.ProcessFocusedRowChangedOnServer = false;
           this.SettingsResizing.ColumnResizeMode = ColumnResizeMode.Control;
            this.SettingsLoadingPanel.Text = "در حال بارگذاری";
            this.EnableViewState = false;

            //this.Styles.GroupPanel.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Styles.GroupPanel.ForeColor = System.Drawing.Color.Black;
            //this.Styles.GroupPanel.ForeColor = System.Drawing.Color.White;

            this.Styles.Header.HorizontalAlign = HorizontalAlign.Center;
           // this.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            //this.Styles.SelectedRow.BackColor = System.Drawing.Color.White;
            //this.Styles.SelectedRow.ForeColor = System.Drawing.Color.Black;

            this.SettingsEditing.PopupEditFormHorizontalAlign = DevExpress.Web.PopupHorizontalAlign.WindowCenter;
            this.SettingsEditing.PopupEditFormVerticalAlign = DevExpress.Web.PopupVerticalAlign.WindowCenter;
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            //SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;
        }
    }
}
