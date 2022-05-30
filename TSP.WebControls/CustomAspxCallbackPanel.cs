using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;

[assembly: TagPrefix("TSP.WebControls", "TSPControls")]
namespace TSP.WebControls
{
    public class CustomAspxCallbackPanel :DevExpress.Web.ASPxCallbackPanel
    {

        public CustomAspxCallbackPanel()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.Images.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            this.Width = Unit.Percentage(100);
            this.SettingsLoadingPanel.Text = "در حال بارگذاری لطفا صبر نمایید...";
            //this.LoadingPanelImage.Url = "Image/indicator.gif";
            this.HideContentOnCallback = false;
            this.SettingsLoadingPanel.ShowImage = true;
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
           //this.ShowLoadingPanel = true;
            
        }
    }
}
