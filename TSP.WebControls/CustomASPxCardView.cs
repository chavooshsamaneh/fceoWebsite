using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("TSP.WebControls", "TSPControls")]
namespace TSP.WebControls
{
    public class CustomASPxCardView : DevExpress.Web.ASPxCardView
    {
        public CustomASPxCardView()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.Images.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            this.Width = Unit.Percentage(100);
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            CardLayoutProperties.SettingsAdaptivity.AdaptivityMode = DevExpress.Web.FormLayoutAdaptivityMode.SingleColumnWindowLimit;
            CardLayoutProperties.SettingsAdaptivity.SwitchToSingleColumnAtWindowInnerWidth = 600;
            Settings.LayoutMode = DevExpress.Web.Layout.Flow;
            this.Styles.FlowCard.BackColor = this.Styles.FlowCard.Border.BorderColor = System.Drawing.Color.Transparent;
            this.Styles.FlowCard.HorizontalAlign = HorizontalAlign.Center;
            this.Styles.FlowCard.Height = Unit.Percentage(100);
            this.Styles.FlowCard.HorizontalAlign = HorizontalAlign.Center;
            this.Border.BorderColor = System.Drawing.Color.White;
        }
    }
}
