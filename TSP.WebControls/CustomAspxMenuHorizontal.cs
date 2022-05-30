using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

using DevExpress.Web;
namespace TSP.WebControls
{
    public class CustomAspxMenuHorizontal : DevExpress.Web.ASPxMenu
    {
        public CustomAspxMenuHorizontal()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";

            this.AutoSeparators = AutoSeparatorMode.RootOnly;
            this.EnableViewState = false;
            this.GutterWidth = Unit.Pixel(0);
            this.AllowSelectItem = true;
            //this.Font.Bold = false;
            //this.Font.Size = FontUnit.Point(8);
            this.HorizontalAlign = HorizontalAlign.Right;
            this.SettingsLoadingPanel.Text = "در حال بارگذاری";
            this.Orientation = Orientation.Horizontal;
            this.SelectParentItem = true;
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            this.EnableAdaptivity = true;
            this.ItemStyle.Height = Unit.Pixel(30);            
            //    this.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            //    this.ItemStyle.Paddings.Padding = Unit.Pixel(0);
            //    this.ItemStyle.Paddings.PaddingLeft = Unit.Pixel(0);
            //    this.ItemStyle.Paddings.PaddingRight = Unit.Pixel(0);
            //    this.ItemStyle.BorderBottom.BorderWidth = Unit.Pixel(1);
            //    this.ItemImage.Height = Unit.Pixel(30);
            //    this.ItemImage.Width =Unit.Pixel(30);
        }
    }
}
