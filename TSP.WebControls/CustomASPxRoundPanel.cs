using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TSP.WebControls
{
    public class CustomASPxRoundPanel : DevExpress.Web.ASPxRoundPanel
    {
        public CustomASPxRoundPanel()
        {
            //this.CssFilePath = "~/App_Themes/MetropolisBlue/{0}/styles.css";
            //this.CssPostfix = "MetropolisBlue";
            //SpriteCssFilePath = "~/App_Themes/MetropolisBlue/{0}/sprite.css";
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            this.CssClass = "AspxRoundPanel-Border PaddingRightDevRoundPanel";

            //this.GroupBoxCaptionOffsetY = Unit.Pixel(-24);
           
            //this.ContentPaddings.PaddingBottom = Unit.Pixel(10);
            //this.ContentPaddings.PaddingLeft = Unit.Pixel(4);
            //this.ContentPaddings.PaddingTop = Unit.Pixel(10);

            //this.HeaderStyle.Height = Unit.Pixel(23);
            //this.HeaderStyle.Paddings.PaddingBottom = Unit.Pixel(0);
            //this.HeaderStyle.Paddings.PaddingLeft = Unit.Pixel(2);
            //this.HeaderStyle.Paddings.PaddingTop = Unit.Pixel(0);
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            this.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            CollapseButtonStyle.CssClass = "dxrpCollapseButtonRtl_leftButton";
            Width = Unit.Percentage(100);
        }
    }
}
