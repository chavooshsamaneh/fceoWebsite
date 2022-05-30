using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

[assembly: TagPrefix("TSP.WebControls", "TSPControls")]
namespace TSP.WebControls
{
    public class CustomASPxCaptcha : DevExpress.Web.ASPxCaptcha
    {
        public CustomASPxCaptcha()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            //this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            //this.CharacterSet = "abcdefghijkmnpqrstuvwxyz23456789ABDEFGHJLMNQRT";
            //this.CodeLength = 8;
            this.CharacterSet = "1234567890";
            this.TextBox.LabelText = "کد امنیتی :";
            this.TextBox.Position = DevExpress.Web.Captcha.ControlPosition.Bottom;
            
            this.RefreshButton.Text = "کد جدید";
            this.RefreshButton.Image.Height = System.Web.UI.WebControls.Unit.Pixel(20);
            this.RefreshButton.Image.Width = System.Web.UI.WebControls.Unit.Pixel(20);            

            this.ChallengeImage.BackgroundColor = System.Drawing.Color.Transparent;
            this.ChallengeImage.BorderWidth = 1;            

            this.ValidationSettings.Display = DevExpress.Web.Display.Dynamic;
            this.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithText;
            this.ValidationSettings.ErrorText = "کد امنیتی وارد شده اشتباه است";
            this.ValidationSettings.RequiredField.ErrorText = "کد امنیتی وارد نشده است";
            this.ValidationSettings.RequiredField.IsRequired = true;
        }

        public static Boolean Check(String CaptchaCode, String InputCode)
        {
            return CaptchaCode == InputCode;
        }
    }
}
