using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP.WebControls
{
    public class CustomAspxComboBox : DevExpress.Web.ASPxComboBox
    {
        public CustomAspxComboBox()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            this.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            ItemStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            Width = System.Web.UI.WebControls.Unit.Percentage(100);
            this.EnableIncrementalFiltering = true;
            IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
            this.ValueType = typeof(String);
            this.ValidationSettings.ErrorImage.Height = System.Web.UI.WebControls.Unit.Pixel(14);
            //this.ValidationSettings.ErrorImage.Url = "~/App_Themes/Metropolis/Editors/edtError.png";
            this.ValidationSettings.ErrorFrameStyle.ImageSpacing = System.Web.UI.WebControls.Unit.Pixel(4);
            this.ValidationSettings.ErrorFrameStyle.ErrorTextPaddings.PaddingLeft = System.Web.UI.WebControls.Unit.Pixel(4);
        }
    }
}
