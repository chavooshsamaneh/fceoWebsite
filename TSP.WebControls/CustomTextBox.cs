using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSP.WebControls
{
    public class CustomTextBox :  DevExpress.Web.ASPxTextBox//DevExpress.Web.Bootstrap.BootstrapTextBox//
    {
        public CustomTextBox()
        {

            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            
            this.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            //this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            //1px solid #7A7B80
            // this.Height = System.Web.UI.WebControls.Unit.Pixel(20);
            // System.Drawing.ColorTranslator.FromHtml("#184377");
            // Width="100%" Border-BorderStyle="Solid"  Border-BorderColor="#A5045A" Border-BorderWidth="1px"
            this.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            //this.Border.BorderColor = EshopBusiness.WebsiteColors._ControlBorder;
            this.Border.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1);
            this.Paddings.PaddingLeft = System.Web.UI.WebControls.Unit.Percentage(5);
            this.ValidationSettings.Display = DevExpress.Web.Display.Dynamic;
            this.ValidationSettings.ErrorTextPosition = DevExpress.Web.ErrorTextPosition.Bottom;
            this.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithText;
            //this.ValidationSettings.ErrorFrameStyle.ForeColor = EshopBusiness.WebsiteColors._ValidationError;//"#666F9A");
          //  this.ValidationSettings.ErrorFrameStyle.Font.Bold = true;
        }
    }
}
