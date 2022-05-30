using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TSP.WebControls
{
    public class CustomAspxButton : DevExpress.Web.ASPxButton
    {
        public enum ButtonTypes
        {
            New,
            Save,
            Edit,
            ExportExcel,
            View, DeleteRequest, WorkFlow, TraceWorkFlow, PrintGrid, PrintRed, PrintViolent, PrintOrange,
            NewRequest, Help, ShowDetail


        }

        private ButtonTypes _ButtonType;

        /// <summary>
        /// Gets or Sets the ButtonType
        /// </summary>
        [Category("TSP-Properties")]
        [Description("نوع دکمه")]
        public ButtonTypes ButtonType
        {
            get { return _ButtonType; }
            set
            {
                _ButtonType = value;
                SetButtonPropertyBasedOnType();
            }
        }

        private Boolean _IsMenuButton;


        [Category("TSP-Properties")]
        //[Description("ابتدای مسیر تصویر بر اساس فلدر صفحه")]
        public Boolean IsMenuButton
        {
            get { return _IsMenuButton; }
            set
            {
                _IsMenuButton = value;
                SetPropertiesBasedOnMenu();
            }
        }

        public CustomAspxButton()
        {
            //this.CssFilePath = "~/App_Themes/MetropolisBlue/{0}/styles.css";
            //this.CssPostfix = "MetropolisBlue";
            //SpriteCssFilePath = "~/App_Themes/MetropolisBlue/{0}/sprite.css";
            //this.Theme = ControlsStyle.WebSiteThemeCssPostfix;
            //this.ApplyTheme(ControlsStyle.WebSiteThemeCssPostfix);

            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";

            this.EnableViewState = true;
            this.Image.Width = System.Web.UI.WebControls.Unit.Pixel(25);
            this.Image.Height = System.Web.UI.WebControls.Unit.Pixel(25);
            //this.Wrap = DevExpress.Utils.DefaultBoolean.False;
            this.ClientInstanceName = ID;
            UseSubmitBehavior = false;

        }

        private void SetPropertiesBasedOnMenu()
        {
            if (_IsMenuButton)
            {
                this.Text = "";
                this.HoverStyle.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1);
                this.HoverStyle.Border.BorderColor = System.Drawing.Color.Gray;
                this.HoverStyle.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.Outset;
                HoverStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFE0C0");
                //EnableDefaultAppearance = false;
                RenderMode = DevExpress.Web.ButtonRenderMode.Link;
                EnableTheming = false;
                Image.Height = System.Web.UI.WebControls.Unit.Pixel(25);
                Image.Width = System.Web.UI.WebControls.Unit.Pixel(25);
            }
        }

        private void SetButtonPropertyBasedOnType()
        {
            this.CausesValidation = false;
            switch (_ButtonType)
            {
                case ButtonTypes.New:
                    this.Image.Url = "~/Images/icons/new.png";
                    this.Text = this.ToolTip = "جدید";
                    break;
                case ButtonTypes.Edit:
                    this.Image.Url = "~/Images/icons/edit.png";
                    this.Text = this.ToolTip = "ویرایش";
                    break;
                case ButtonTypes.View:
                    this.Image.Url = "~/Images/icons/view.png";
                    this.Text = this.ToolTip = "مشاهده";
                    break;
                case ButtonTypes.WorkFlow:
                    this.Image.Url = "~/Images/icons/reload.png";
                    this.Text = this.ToolTip = "گردش کار";
                    break;
                case ButtonTypes.TraceWorkFlow:
                    this.Image.Url = "~/Images/icons/Cheque Status ReChange.png";
                    this.Text = this.ToolTip = "پیگیری";
                    break;
                case ButtonTypes.DeleteRequest:
                    this.Image.Url = "~/Images/icons/delete.png";
                    this.Text = this.ToolTip = "لغو درخواست";
                    break;
                case ButtonTypes.Save:
                    this.Image.Url = "~/Images/icons/save.png";
                    this.CausesValidation = true;
                    break;
                case ButtonTypes.PrintGrid:
                    this.Image.Url = "~/Images/icons/printers.png";
                    this.Text = this.ToolTip = "چاپ";
                    break;
                case ButtonTypes.ExportExcel:
                    this.Image.Url = "~/Images/icons/ExportExcel.png";
                    this.Text = this.ToolTip = "خروجی اکسل";
                    break;
                case ButtonTypes.Help:
                    this.Image.Url = "~/Images/Help.png";
                    this.Text = this.ToolTip = "راهنما";
                    break;
                case ButtonTypes.ShowDetail:
                    this.Image.Url = "~/Images/icons/ShowDetail.png";
                    this.Text = this.ToolTip = "مشاهده جزییات";
                    break;

            }
            if (_IsMenuButton)
            {
                this.Text = "";
            }
        }

    }
}
