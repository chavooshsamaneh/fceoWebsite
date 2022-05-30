using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP.WebControls
{
    public class CustomASPxPopupControl : DevExpress.Web.ASPxPopupControl
    {
        public CustomASPxPopupControl()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            Modal = true;
            CloseAction = DevExpress.Web.CloseAction.CloseButton;
            AllowDragging = true;
            AutoUpdatePosition = true;
            PopupVerticalAlign = DevExpress.Web.PopupVerticalAlign.WindowCenter;
            PopupHorizontalAlign = DevExpress.Web.PopupHorizontalAlign.WindowCenter;
            RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
    }
}
