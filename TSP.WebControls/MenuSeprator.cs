using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace TSP.WebControls
{
    public class MenuSeprator : Label
    {
        public MenuSeprator()
        {
            this.BorderColor = System.Drawing.Color.White;
            this.BorderStyle = BorderStyle.Inset;
            this.BorderWidth = Unit.Pixel(1);
            this.Width = Unit.Parse("1");
            this.Height = Unit.Pixel(20);
        }
    }
}
