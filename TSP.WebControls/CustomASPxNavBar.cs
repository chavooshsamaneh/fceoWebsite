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
    public class CustomASPxNavBar : DevExpress.Web.ASPxNavBar
    {
        public CustomASPxNavBar()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";

            this.EnableAnimation = true;
            this.SettingsLoadingPanel.Text = "در حال بارگذاری&hellip;";
            this.AutoCollapse = true;
           // this.BackColor = System.Drawing.ColorTranslator.FromHtml("#184377");
            //this.CssPostfix = "MetropolisBlue";
            //this.ImageFolder = "~/App_Themes/MetropolisBlue/{0}/";
            this.GroupSpacing = Unit.Pixel(0);

            //this.GroupHeaderStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0077C6");
            this.GroupHeaderStyle.Height = Unit.Pixel(20);
            this.GroupHeaderStyle.HorizontalAlign = HorizontalAlign.Right;
           // this.GroupHeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#184377");
            this.GroupHeaderStyle.Font.Bold = true;
            this.GroupHeaderStyle.Font.Underline = false;
          //  this.GroupHeaderStyle.BackgroundImage.ImageUrl = "~/flash/Home/nbHeaderHBack2.png";
          //  this.GroupHeaderStyle.BackgroundImage.Repeat = DevExpress.Web.BackgroundImageRepeat.RepeatX;           
           // this.GroupHeaderStyle.HoverStyle.BackgroundImage.ImageUrl = "~/flash/Home/nbHeaderHBack2.png";
           // this.GroupHeaderStyle.HoverStyle.BackgroundImage.Repeat = DevExpress.Web.BackgroundImageRepeat.RepeatX;
          //  this.GroupHeaderStyle.HoverStyle.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#19577B");
            this.GroupHeaderStyle.HoverStyle.Border.BorderWidth = Unit.Pixel(1);
            this.GroupHeaderStyle.HoverStyle.Border.BorderStyle = BorderStyle.Solid;
            this.GroupHeaderStyle.Paddings.Padding = Unit.Pixel(0);
            this.GroupHeaderStyle.Paddings.PaddingRight = Unit.Pixel(5);
            //this.GroupHeaderStyle.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#19577B");
            this.GroupHeaderStyle.Border.BorderStyle = BorderStyle.Solid;
            this.GroupHeaderStyle.Border.BorderWidth = Unit.Pixel(1);

            this.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            this.ItemStyle.Height = Unit.Pixel(20);
            //this.ItemStyle.SelectedStyle.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.HotTrack);
            //this.ItemStyle.SelectedStyle.BackgroundImage.Repeat = DevExpress.Web.BackgroundImageRepeat.RepeatX;
            //this.ItemStyle.HoverStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#0077C6");
            this.ItemStyle.HoverStyle.Font.Bold = true;
            this.ItemStyle.Paddings.Padding = Unit.Pixel(0);
            this.ItemStyle.Paddings.PaddingRight = Unit.Pixel(25);

            //this.GroupHeaderStyleCollapsed.BackColor = System.Drawing.ColorTranslator.FromHtml("#184377");
            this.GroupHeaderStyleCollapsed.Font.Bold = false;
            this.GroupHeaderStyleCollapsed.HoverStyle.Font.Bold = true;
            //this.GroupHeaderStyleCollapsed.HoverStyle.BackgroundImage.ImageUrl = "~/flash/Home/nbHeaderHBack2.png";
            //this.GroupHeaderStyleCollapsed.HoverStyle.BackgroundImage.Repeat = DevExpress.Web.BackgroundImageRepeat.RepeatX;
            //this.GroupHeaderStyleCollapsed.HoverStyle.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#19577B");
            this.GroupHeaderStyleCollapsed.HoverStyle.Border.BorderStyle = BorderStyle.Solid;
            this.GroupHeaderStyleCollapsed.HoverStyle.Border.BorderWidth = Unit.Pixel(1);
            //this.GroupHeaderStyleCollapsed.BackgroundImage.ImageUrl = "~/flash/Home/nbHeaderBack3.png";
            //this.GroupHeaderStyleCollapsed.BackgroundImage.Repeat = DevExpress.Web.BackgroundImageRepeat.RepeatX;
            //this.GroupHeaderStyleCollapsed.Border.BorderColor = System.Drawing.Color.Silver;
            this.GroupHeaderStyleCollapsed.Border.BorderStyle = BorderStyle.Solid;
            this.GroupHeaderStyleCollapsed.Border.BorderWidth = Unit.Pixel(0);

            //this.Border.BorderColor = System.Drawing.ColorTranslator.FromHtml("#557798");
            this.Border.BorderStyle = BorderStyle.Solid;
            this.Border.BorderWidth = Unit.Pixel(1);

            //this.GroupContentStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#184377");
            this.GroupContentStyle.Font.Size = FontUnit.Point(7);
            this.GroupContentStyle.Font.Underline = false;
            //this.GroupContentStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0077C6");// System.Drawing.Color.White;
            //this.GroupContentStyle.HorizontalAlign = HorizontalAlign.Right;
            this.GroupContentStyle.Paddings.Padding = Unit.Pixel(0);
            this.GroupContentStyle.Paddings.PaddingBottom = Unit.Pixel(0);
            this.GroupContentStyle.Paddings.PaddingLeft = Unit.Pixel(0);
            this.GroupContentStyle.Paddings.PaddingRight = Unit.Pixel(0);
            this.GroupContentStyle.Paddings.PaddingTop = Unit.Pixel(0);
            //this.GroupContentStyle.BackgroundImage.ImageUrl = "~/flash/Home/nbHeaderHBack3.png";
            //this.GroupContentStyle.BackgroundImage.Repeat = DevExpress.Web.BackgroundImageRepeat.RepeatX;
            this.GroupContentStyle.BackgroundImage.VerticalPosition = "bottom";
            //this.GroupContentStyle.BorderTop.BorderColor = System.Drawing.ColorTranslator.FromHtml("#19577B");
            this.GroupContentStyle.BorderTop.BorderStyle = BorderStyle.Solid;
            this.GroupContentStyle.BorderTop.BorderWidth = Unit.Pixel(1);
            //this.GroupContentStyle.BorderBottom.BorderColor = System.Drawing.Color.Black;
            this.GroupContentStyle.BorderBottom.BorderStyle = BorderStyle.Solid;
            this.GroupContentStyle.BorderBottom.BorderWidth = Unit.Pixel(1);

            this.CollapseImage.Height = Unit.Pixel(16);
            this.CollapseImage.Width = Unit.Pixel(16);

            this.ExpandImage.Height = Unit.Pixel(16);
            this.ExpandImage.Width = Unit.Pixel(16);
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
        }
    }
}
