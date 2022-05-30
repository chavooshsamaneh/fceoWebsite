using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Web;

namespace TSP.WebControls
{
    public class CustomAspxMenuManagmentPage : DevExpress.Web.ASPxMenu
    {
        public CustomAspxMenuManagmentPage()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";

            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            this.AutoSeparators=AutoSeparatorMode.All;
            this.ItemLinkMode=DevExpress.Web.ItemLinkMode.TextAndImage;
            this.AllowSelectItem = true;
            //this.Font-Names="Tahoma" Font-Size="11px"
            //Font-Underline="False" ForeColor="#162436" SeparatorColor="#5386CB" SeparatorHeight="10px"
            //SeparatorWidth="1px"
        }
    }
}
