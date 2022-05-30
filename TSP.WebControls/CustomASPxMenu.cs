using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

namespace TSP.WebControls
{

    public class CustomASPxMenu : DevExpress.Web.ASPxMenu
    {

        public CustomASPxMenu()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";

            this.EnableViewState = false;
            this.GutterWidth = Unit.Pixel(0);
            this.AllowSelectItem = true;
            // this.BackColor = System.Drawing.ColorTranslator.FromHtml("#184377");
            this.Font.Bold = false;
            this.Font.Name = "Tahoma";
            this.Font.Size = FontUnit.Point(8);
            //this.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0077C6");// System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.PowderBlue);
            this.HorizontalAlign = HorizontalAlign.Right;
            this.ItemAutoWidth = false;
            this.SettingsLoadingPanel.Text = "در حال بارگذاری&hellip;";
            this.Orientation = Orientation.Vertical;
            this.SelectParentItem = true;

            this.ItemStyle.Height = Unit.Pixel(22);
            this.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            this.ItemStyle.Width = Unit.Percentage(100);
            //this.ItemStyle.SelectedStyle.BackgroundImage.ImageUrl = "~/flash/Home/nbHeaderHBack2.png";
            //this.ItemStyle.SelectedStyle.BackgroundImage.Repeat = DevExpress.Web.BackgroundImageRepeat.RepeatX;
            //this.ItemStyle.HoverStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#184377");
            //this.ItemStyle.HoverStyle.BackgroundImage.ImageUrl = "~/flash/Home/nbHeaderHBack2.png";
            //this.ItemStyle.HoverStyle.BackgroundImage.Repeat = DevExpress.Web.BackgroundImageRepeat.RepeatX;
            this.ItemStyle.HoverStyle.BackgroundImage.VerticalPosition = "center";
            //this.ItemStyle.HoverStyle.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#19577B");
            this.ItemStyle.Paddings.Padding = Unit.Pixel(0);
            this.ItemStyle.Paddings.PaddingLeft = Unit.Pixel(0);
            this.ItemStyle.Paddings.PaddingRight = Unit.Pixel(0);
            //this.ItemStyle.BorderBottom.BorderColor = System.Drawing.Color.MidnightBlue;
            this.ItemStyle.BorderBottom.BorderWidth = Unit.Pixel(1);


            ////////////////////
            //this.SubMenuItemStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#184377");
            this.SubMenuItemStyle.Font.Bold = false;
            this.SubMenuItemStyle.Font.Name = "Tahoma";
            this.SubMenuItemStyle.Font.Size = FontUnit.Point(8);
            //this.SubMenuItemStyle.ForeColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.PowderBlue);
            this.SubMenuItemStyle.HorizontalAlign = HorizontalAlign.Right;

            this.SubMenuItemStyle.Height = Unit.Pixel(22);
            this.SubMenuItemStyle.Width = Unit.Percentage(100);
            //this.SubMenuItemStyle.SelectedStyle.BackgroundImage.ImageUrl = "~/flash/Home/nbHeaderHBack2.png";
            //this.SubMenuItemStyle.SelectedStyle.BackgroundImage.Repeat = DevExpress.Web.BackgroundImageRepeat.RepeatX;
            //this.SubMenuItemStyle.HoverStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#184377");
            //this.SubMenuItemStyle.HoverStyle.BackgroundImage.ImageUrl = "~/flash/Home/nbHeaderHBack2.png";
            //this.SubMenuItemStyle.HoverStyle.BackgroundImage.Repeat = DevExpress.Web.BackgroundImageRepeat.RepeatX;
            this.SubMenuItemStyle.HoverStyle.BackgroundImage.VerticalPosition = "center";
            //this.SubMenuItemStyle.HoverStyle.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#19577B");
            this.SubMenuItemStyle.Paddings.Padding = Unit.Pixel(0);
            this.SubMenuItemStyle.Paddings.PaddingLeft = Unit.Pixel(0);
            this.SubMenuItemStyle.Paddings.PaddingRight = Unit.Pixel(0);
            //this.SubMenuItemStyle.BorderBottom.BorderColor = System.Drawing.Color.MidnightBlue;
            this.SubMenuItemStyle.BorderBottom.BorderWidth = Unit.Pixel(0);
            //this.SubMenuItemStyle.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#557798");
            this.SubMenuItemStyle.Border.BorderWidth = Unit.Pixel(0);
            //////////////////     
            this.LinkStyle.HoverFont.Bold = true;

            //this.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#557798");
            this.Border.BorderWidth = Unit.Pixel(2);
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
        }
    }
}
