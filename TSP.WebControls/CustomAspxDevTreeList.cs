using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Web.ASPxTreeList ;


[assembly: TagPrefix("TSP.WebControls", "TSPControls")]
namespace TSP.WebControls
{
    public class CustomAspxDevTreeList : DevExpress.Web.ASPxTreeList.ASPxTreeList
    {
        public CustomAspxDevTreeList()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.Images.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
         
            this.Images.CollapsedButton.Height = Unit.Pixel(11);
            this.Images.CollapsedButton.Width = Unit.Pixel(11);
            //this.Images.CollapsedButton.Url = "~/App_Themes/MetropolisBlue/TreeList/CollapsedButton.png";
            this.Images.ExpandedButton.Height = Unit.Pixel(11);
            this.Images.ExpandedButton.Width = Unit.Pixel(11);
            //this.Images.ExpandedButton.Url = "~/App_Themes/MetropolisBlue/TreeList/ExpandedButton.png";
            this.Images.CustomizationWindowClose.Height = Unit.Pixel(17);
            this.Images.CustomizationWindowClose.Width = Unit.Pixel(17);

            this.SettingsText.CommandCancel = "انصراف";
            this.SettingsText.CommandDelete = "حذف";
            this.SettingsText.CommandEdit = "ویرایش";
            this.SettingsText.CommandNew = "جدید";
            this.SettingsText.CommandUpdate = "ذخیره";
            this.SettingsText.ConfirmDelete = "آیا مطمئن به حذف این ردیف هستید؟";


            this.SettingsPager.AllButton.Text = "همه رکوردها";
            this.SettingsPager.FirstPageButton.Text = "اولین صفحه";
            this.SettingsPager.LastPageButton.Text = "آخرین صفحه";
            this.SettingsPager.NextPageButton.Text = "صفحه بعد";
            this.SettingsPager.PrevPageButton.Text = "صفحه قبل";
           // this.SettingsPager.Summary.AllPagesText = "صفحات: {0} - {1} ({2} مورد)";//"Pages: {0} - {1} ({2} items)";
            this.SettingsPager.Summary.Text = "صفحه: {0} از {1} (تعداد کل:{2})";//"Pages: {0} - {1} ({2} items)";
          


            this.SettingsLoadingPanel.Text = "در حال بارگذاری";


            this.Styles.Header.HorizontalAlign = HorizontalAlign.Center;

        }

    }
}
