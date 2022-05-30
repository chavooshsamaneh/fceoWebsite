using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP.WebControls
{

    public class CustomAspxDevDataView : DevExpress.Web.ASPxDataView
    {
        public CustomAspxDevDataView()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath= "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";

            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            this.EmptyDataText = "هیچ داده ای وجود ندارد";
            this.PagerSettings.AllButton.Text = "همه رکوردها";
            this.PagerSettings.FirstPageButton.Text = "اولین صفحه";
            this.PagerSettings.LastPageButton.Text = "آخرین صفحه";
            this.PagerSettings.NextPageButton.Text = "صفحه بعد";
            this.PagerSettings.PrevPageButton.Text = "صفحه قبل";
            this.PagerSettings.ShowMoreItemsText = "نمایش بیشتر";
            this.AlwaysShowPager = true;
            this.PagerSettings.Summary.Text = "صفحه: {0} از {1} (تعداد کل:{2})";
            PagerSettings.PageSizeItemSettings.Items = new String[] { "10", "20", "50", "100" };
            PagerSettings.PageSizeItemSettings.Visible = true;
            this.SettingsLoadingPanel.Text = "در حال بارگذاری";                                    
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            Width = System.Web.UI.WebControls.Unit.Percentage(100);
            ItemStyle.Paddings.Padding = System.Web.UI.WebControls.Unit.Pixel(0);
            //ItemStyle.Height = System.Web.UI.WebControls.Unit.Pixel(1);
           // Paddings = 0;
          /////////////////////////////////////////////////////////////  
            //this.Styles.GroupPanel.ForeColor = System.Drawing.Color.Black;
            //this.Styles.Header.HorizontalAlign = HorizontalAlign.Center;
            //this.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            //this.Settings.ShowFilterRowMenu = true;
            //this.Settings.ShowFilterRow = true;    
            // this.PagerSettings.ShowGroupPanel = true;
            //this.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.Control;
            //this.SettingsBehavior.AllowFocusedRow = true;
            //this.SettingsBehavior.AllowGroup = true;
            //this.SettingsBehavior.ConfirmDelete = true;        
        }
    }
}
