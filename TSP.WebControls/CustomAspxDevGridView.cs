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

    public class CustomAspxDevGridView :  DevExpress.Web.ASPxGridView//DevExpress.Web.Bootstrap.BootstrapGridView//
    {
        public CustomAspxDevGridView()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.Images.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            this.SettingsText.CommandCancel = "انصراف";
            this.SettingsText.CommandClearFilter = "پاک کردن فیلتر";
            this.SettingsText.CommandDelete = "حذف";
            this.SettingsText.CommandEdit = "ویرایش";
            this.SettingsText.CommandNew = "جدید";
            this.SettingsText.CommandSelect = "انتخاب";
            this.SettingsText.CommandUpdate = "ذخیره";
            this.SettingsText.ConfirmDelete = "آیا مطمئن به حذف این ردیف هستید؟";
            this.SettingsText.GroupPanel = "جهت گروه بندی ستون مربوطه را به این قسمت بکشید";//"برای گروه بندی از این قسمت استفاده کنید";
            this.SettingsText.PopupEditFormCaption = "تغییر رکورد";
            this.SettingsText.EmptyDataRow = "هیچ داده ای وجود ندارد";
            this.SettingsPager.AllButton.Text = "همه رکوردها";
            this.SettingsPager.FirstPageButton.Text = "اولین صفحه";
            this.SettingsPager.LastPageButton.Text = "آخرین صفحه";
            this.SettingsPager.NextPageButton.Text = "صفحه بعد";
            this.SettingsPager.PrevPageButton.Text = "صفحه قبل";
            this.SettingsPager.AlwaysShowPager = true;
            this.SettingsPager.Summary.Text = "صفحه: {0} از {1} (تعداد کل:{2})";
            this.StylesPager.PageNumber.HorizontalAlign = HorizontalAlign.Center;
            this.StylesPager.PageNumber.VerticalAlign = VerticalAlign.Middle;

            this.Settings.ShowGroupPanel = true;
            this.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.Control;
            this.SettingsBehavior.AllowFocusedRow = true;
            this.SettingsBehavior.AllowGroup = true;
            this.SettingsBehavior.ConfirmDelete = true;
            this.SettingsLoadingPanel.Text = "در حال بارگذاری";
            this.EnableViewState = false;

            this.Styles.Header.HorizontalAlign = HorizontalAlign.Center;
            this.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            this.Settings.ShowFilterRowMenu = true;
            this.Settings.ShowFilterRow = true;
            Settings.HorizontalScrollBarMode = ScrollBarMode.Auto;

            this.SettingsCookies.StoreFiltering = false;
            this.SettingsCookies.StorePaging = false;
            this.SettingsCookies.StoreGroupingAndSorting = false;
            SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;            
            SettingsPager.PageSizeItemSettings.Items = new String[] { "10", "20", "50","100" };
            SettingsPager.PageSizeItemSettings.Visible = true;
            SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;
            
        }
    }
}

