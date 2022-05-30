using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP.WebControls
{
    public class CustomASPxCheckBoxList : DevExpress.Web.ASPxCheckBoxList
    {
        public CustomASPxCheckBoxList()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.ImagesInternal.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            this.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
        }
    }
}
