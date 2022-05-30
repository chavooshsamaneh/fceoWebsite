using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

[assembly: TagPrefix("TSP.WebControls", "TSPControls")]
namespace TSP.WebControls
{
    public class CustomAspxUploadControl : DevExpress.Web.ASPxUploadControl
    {
        #region Properties
        private int _MaxSizeForUploadFile; // In Byte

        /// <summary>
        /// Gets or Sets the Maximum size (In Byte) of file for Upload
        /// </summary>
        [Category("TSP-Properties")]
        [Description("سایز فایل آپلودی")]
        public int MaxSizeForUploadFile
        {
            get { return _MaxSizeForUploadFile; }
            set
            {
                _MaxSizeForUploadFile = value;
                this.ValidationSettings.MaxFileSize = value;
                this.ValidationSettings.MaxFileSizeErrorText = GetMaxFileSizeErrorText(value);
            }
        }

        public enum InputTypes
        {
            Images, Files, Custom
        }

        private InputTypes _InputType;

        /// <summary>
        /// Gets or Sets the UploadFile ContentType 
        /// </summary>
        [Category("TSP-Properties")]
        [Description("نوع فایل ورودی")]
        public InputTypes InputType
        {
            get { return _InputType; }
            set
            {
                _InputType = value;
                SetContentTypes();
            }
        }

        private Boolean _UploadWhenFileChoosed;

        /// <summary>
        /// Upload the file when user choosed a file 
        /// </summary>
        [Category("TSP-Properties")]
        [Description("آپلود فایل در همان زمانی که فایل انتخاب شود")]
        public Boolean UploadWhenFileChoosed
        {
            get { return _UploadWhenFileChoosed; }
            set
            {
                _UploadWhenFileChoosed = value;
                SetContentTypes();
            }
        }

        private String _CustomAllowedFileTypes;

        /// <summary>
        /// Allowed File Extensions - Format: .*,ie. .jpg (Seprated with ',')
        /// </summary>
        [Category("TSP-Properties")]
        [Description("نوع فایل های آپلودی - به صورت .* مثلا .jpg - با ',' جدا شوند)")]
        public String CustomAllowedFileTypes
        {
            get { return _CustomAllowedFileTypes; }
            set
            {
                _CustomAllowedFileTypes = value;
                SetContentTypes();
            }
        }
        #endregion

        public CustomAspxUploadControl()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";

            _InputType = InputTypes.Images;
            _UploadWhenFileChoosed = false;
            _CustomAllowedFileTypes = "";
            this.UploadMode = UploadControlUploadMode.Standard;
            this.ShowClearFileSelectionButton = false;
            SetContentTypes();
            SetProperties();
            Width = Unit.Percentage(100);
        }

        void SetProperties()
        {
            try
            {
                _MaxSizeForUploadFile = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MaxSizeForUploadFile"]);
            }
            catch (Exception)
            {
                _MaxSizeForUploadFile = 100000; // Default 100 KB
            }
            this.ValidationSettings.MaxFileSize = _MaxSizeForUploadFile;
            this.ValidationSettings.MaxFileSizeErrorText = GetMaxFileSizeErrorText(_MaxSizeForUploadFile);
            this.ShowProgressPanel = true;
            this.CancelButton.Text = "انصراف";
            this.ValidationSettings.GeneralErrorText = "خطایی در بارگزاری فایل در سرور انجام گرفته است";
            this.ValidationSettings.NotAllowedFileExtensionErrorText = "شما مجاز به آپلود این نوع فایل نیستید";
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
        }

        void SetBrowsButton()
        {
            this.BrowseButton.ImagePosition = DevExpress.Web.ImagePosition.Left;
            this.BrowseButton.Image.Height = Unit.Pixel(16);
            this.BrowseButton.Image.Width = Unit.Pixel(16);

            switch (_InputType)
            {
                case InputTypes.Images:
                    this.BrowseButton.Text = " انتخاب تصویر";
                    this.BrowseButton.Image.Url = "~/Images/Icons/image-upload.png";
                    break;
                case InputTypes.Files:
                    this.BrowseButton.Text = " انتخاب فایل";
                    this.BrowseButton.Image.Url = "~/Images/Icons/file-upload.png";
                    break;
                case InputTypes.Custom:
                    this.BrowseButton.Text = " انتخاب فایل";
                    this.BrowseButton.Image.Url = "~/Images/Icons/file-upload.png";
                    break;
            }
        }

        String GetMaxFileSizeErrorText(int MaxSizeForUploadFile)
        {
            return "سایز فایل انتخابی از حداکثر مجاز " + "(" + (MaxSizeForUploadFile / 1000) + " KB)" + " بیشتر است";
        }

        private void SetContentTypes()
        {
            switch (_InputType)
            {
                case InputTypes.Images:
                    this.ValidationSettings.AllowedFileExtensions = GetAllowedExtensionTypesForImage();
                    break;
                case InputTypes.Files:
                    this.ValidationSettings.AllowedFileExtensions = GetAllowedExtensionTypesForFile();
                    break;
                case InputTypes.Custom:
                    this.ValidationSettings.AllowedFileExtensions = _CustomAllowedFileTypes.Split(',');
                    break;
            }
            this.ClientSideEvents.TextChanged = GetJavaScript();
            SetBrowsButton();
        }

        private String GetJavaScript()
        {
            try
            {
                switch (_InputType)
                {
                    case InputTypes.Images:
                        if (_UploadWhenFileChoosed)
                            return Properties.Resources.CheckFileUploadForImages_UploadWhenOK;
                        else
                            return Properties.Resources.CheckFileUploadForImages;
                        break;
                    case InputTypes.Files:
                        if (_UploadWhenFileChoosed)
                            return Properties.Resources.CheckFileUploadForFiles_UploadWhenOK;
                        else
                            return Properties.Resources.CheckFileUploadForFiles;
                        break;
                    case InputTypes.Custom:
                        return CreateJavaScriptForCustomFileTypes();
                        break;
                }
            }
            catch (Exception) { }
            return "";
        }

        private String[] GetAllowedExtensionTypesForImage()
        {
            String[] Types = new String[5];
            Types[0] = ".gif";
            Types[1] = ".jpeg";
            Types[2] = ".png";
            Types[3] = ".bmp";
            Types[4] = ".jpg";
            return Types;
        }

        private String[] GetAllowedExtensionTypesForFile()
        {
            String[] Types = new String[23];
            Types[0] = ".gif";
            Types[1] = ".jpeg";
            Types[2] = ".jpg";
            Types[3] = ".png";
            Types[4] = ".bmp";
            Types[5] = ".zip";
            Types[6] = ".rar";
            Types[7] = ".txt";
            Types[8] = ".pdf";
            Types[9] = ".doc";
            Types[10] = ".docx";
            Types[11] = ".xls";
            Types[12] = ".xlsx";
            Types[13] = ".html";
            Types[14] = ".htm";
            Types[15] = ".rtf";
            Types[16] = ".swf";
            Types[17] = ".repx";
            Types[18] = ".dwg";
            Types[19] = ".edb";
            Types[20] = ".e2k";
            Types[21] = ".fdb";
            Types[22] = ".f2k";
            return Types;
        }

        private String CreateJavaScriptForCustomFileTypes()
        {
            String Js = "function (s, e) { if (s.GetText() == '') return; ";
            Js += "var InputFile = s.GetText(); var extension = new Array(); ";
            String[] AllowedFileType = _CustomAllowedFileTypes.Split(',');
            for (int i = 0; i < AllowedFileType.Length; i++)
                Js += "extension[" + i + "] = '" + AllowedFileType[i] + "'; ";
            Js += "var thisext = InputFile.substr(InputFile.lastIndexOf('.')).toLowerCase(); ";
            Js += "for (var i = 0; i < extension.length; i++) { if (thisext == extension[i]) { ";
            if (_UploadWhenFileChoosed)
                Js += "s.Upload(); ";
            Js += " return; }} alert(\"شما مجاز به آپلود این فایل نیستید\"); s.ClearText(); }";

            return Js;
        }
    }
}
