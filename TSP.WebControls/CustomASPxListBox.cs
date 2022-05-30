using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP.WebControls
{
    public class CustomASPxListBox : DevExpress.Web.ASPxListBox
    {
        public CustomASPxListBox()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            this.Width = System.Web.UI.WebControls.Unit.Percentage(100);
        }
    }
}
