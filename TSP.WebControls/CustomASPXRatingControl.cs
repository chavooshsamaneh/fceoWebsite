using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP.WebControls
{
    public class CustomASPXRatingControl :DevExpress.Web.ASPxRatingControl
    {
        public CustomASPXRatingControl()
        {

            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
   
            //this.CssPostfix = "Metropolis";
            //this.CssClass = "~/App_Themes/Metropolis/{0}/styles.css";
            //this.ImageMapUrl = "~/App_Themes/Metropolis/{0}/sprite.css";
          //  this.ImagesInternal.SpriteCssFilePath
        }
    }
}
