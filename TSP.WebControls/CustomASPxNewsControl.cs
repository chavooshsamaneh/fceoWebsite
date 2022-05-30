using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using DevExpress.Web;
[assembly: TagPrefix("TSP.WebControls", "TSPControls")]
namespace TSP.WebControls
{
    public class CustomASPxNewsControl :ASPxNewsControl
    {
         public CustomASPxNewsControl()
         {
             this.Theme = "Metropolis";
             this.ApplyTheme("Metropolis");
             this.CssPostfix = "Metropolis";
             this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
             this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";

             this.RowPerPage = 10;
            // this.ContentStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#EDF3F4");

             this.SettingsLoadingPanel.Text = "در حال بارگذاری";
             this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
             this.Font.Size = 9;
             this.EmptyDataText = "هیچ داده ای وجود ندارد";
             this.BackToTopText = "بازگشت به بالا";

             this.PagerSettings.Summary.Text = "صفحه: {0} از {1} (تعداد کل:{2})";
             this.PagerSettings.EllipsisMode = DevExpress.Web.PagerEllipsisMode.InsideNumeric;
             this.PagerSettings.NumericButtonCount = 5;
             this.PagerSettings.ShowDefaultImages = true;
             this.PagerSettings.ShowNumericButtons = true;
             this.PagerSettings.Position = System.Web.UI.WebControls.PagerPosition.TopAndBottom;
            // this.PagerSettings.ShowDisabledButtons = false;
             this.PagerSettings.FirstPageButton.Text = "صفحه اول";
             this.PagerSettings.LastPageButton.Text = "صفحه آخر";
             this.PagerSettings.NextPageButton.Text = "بعدی";
             this.PagerSettings.PrevPageButton.Text = "قبلی";
             this.PagerSettings.PrevPageButton.Visible = true;
             this.PagerSettings.FirstPageButton.Visible = false;
             this.PagerSettings.LastPageButton.Visible = false;
             this.PagerSettings.NextPageButton.Visible = true;

             this.ItemSettings.TailText = "ادامه خبر";
             this.ItemSettings.DateHorizontalPosition = DevExpress.Web.DateHorizontalPosition.OutsideRight;
             this.ItemSettings.DateVerticalPosition = DevExpress.Web.DateVerticalPosition.Top;
             this.ItemSettings.TailPosition = DevExpress.Web.TailPosition.KeepWithLastWord;
             //this.ItemSettings.ImagePosition = DevExpress.Web.HeadlineImagePosition.Right;
             
             this.ItemSettings.DateHorizontalPosition = DevExpress.Web.DateHorizontalPosition.Left;
             this.ItemSettings.DateVerticalPosition = DevExpress.Web.DateVerticalPosition.Bottom;
             this.ItemDateStyle.ForeColor = System.Drawing.Color.DarkBlue;

             this.ItemImage.Height=System.Web.UI.WebControls.Unit.Pixel(100);
             this.ItemImage.Width = System.Web.UI.WebControls.Unit.Pixel(100);
             this.ItemImage.Url = "~/Images/NoImage3DPeople.png";

             this.ItemStyle.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Middle;

             this.ItemHeaderStyle.Font.Size = 8;
             this.ItemHeaderStyle.ForeColor = System.Drawing.Color.DarkBlue;
             
         }
    }
}