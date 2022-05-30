using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP.WebControls
{
    public class CustomASPXMemo : DevExpress.Web.ASPxMemo
    {
        public CustomASPXMemo()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";

            this.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            this.ValidationSettings.Display = DevExpress.Web.Display.Dynamic;
            this.ValidationSettings.ErrorTextPosition = DevExpress.Web.ErrorTextPosition.Bottom;
            this.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithText;
        }
    }
}
