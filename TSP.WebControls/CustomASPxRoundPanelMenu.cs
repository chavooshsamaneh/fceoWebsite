using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace TSP.WebControls
{
    public class CustomASPxRoundPanelMenu : DevExpress.Web.ASPxRoundPanel
    {
        public CustomASPxRoundPanelMenu()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            this.CssClass = "AspxRoundPanel-Border";       

            this.ContentPaddings.PaddingBottom = Unit.Pixel(0);
            this.ContentPaddings.PaddingLeft = Unit.Pixel(0);
            this.ContentPaddings.PaddingRight = Unit.Pixel(0);
            this.ContentPaddings.PaddingTop = Unit.Pixel(0);

            //this.Height = Unit.Pixel(20);
            //this.HeaderStyle.Paddings.PaddingBottom = Unit.Pixel(0);
            //this.HeaderStyle.Paddings.PaddingLeft = Unit.Pixel(0);
            //this.HeaderStyle.Paddings.PaddingTop = Unit.Pixel(0);
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            this.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            this.ShowHeader = false;
            this.ID = "RoundPanelMenu";
            this.View = DevExpress.Web.View.GroupBox;
            Width = Unit.Percentage(100);
        }
    }
}
