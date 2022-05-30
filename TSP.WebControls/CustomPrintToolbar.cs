using System;
using System.Collections.Generic;
using System.Text;

namespace TSP.WebControls
{
    public class CustomPrintToolbar:DevExpress.XtraReports.Web.ReportToolbar
    {
        public CustomPrintToolbar()
        {
            this.Theme = "Metropolis";
            this.ApplyTheme("Metropolis");
            this.CssPostfix = "Metropolis";
            this.CssFilePath = "~/App_Themes/Metropolis/{0}/styles.css";
            this.Images.SpriteCssFilePath = "~/App_Themes/Metropolis/{0}/sprite.css";
            this.Paddings.PaddingRight = System.Web.UI.WebControls.Unit.Percentage(15);
            this.Paddings.PaddingLeft = System.Web.UI.WebControls.Unit.Percentage(15);  
            DevExpress.XtraReports.Web.ReportToolbarButton b = null;
             // new DevExpress.XtraReports.Web.ReportToolbarButton();
            //b.ItemKind = DevExpress.XtraReports.Web.ReportToolbarItemKind.Search;
           // b.ToolTip = "جستجو";
            
           // this.Items.Add(b);
            RightToLeftInternal = DevExpress.Utils.DefaultBoolean.True;
            DevExpress.XtraReports.Web.ReportToolbarSeparator sep = new DevExpress.XtraReports.Web.ReportToolbarSeparator();
            sep.Width = 3;
            this.Items.Add(sep);
            Width = System.Web.UI.WebControls.Unit.Percentage(100);  
            
            b = new DevExpress.XtraReports.Web.ReportToolbarButton();
            b.ItemKind = DevExpress.XtraReports.Web.ReportToolbarItemKind.PrintReport;
            b.ToolTip = "چاپ گزارش"; ;
            this.Items.Add(b);
            b = new DevExpress.XtraReports.Web.ReportToolbarButton();
            b.ItemKind = DevExpress.XtraReports.Web.ReportToolbarItemKind.PrintPage;
            b.ToolTip = "چاپ صفحه"; ;
            this.Items.Add(b);

            sep = new DevExpress.XtraReports.Web.ReportToolbarSeparator();
            sep.Width = 3;
            this.Items.Add(sep);

            b = new DevExpress.XtraReports.Web.ReportToolbarButton();
            b.ItemKind = DevExpress.XtraReports.Web.ReportToolbarItemKind.FirstPage;
            b.Enabled = false;
            b.ToolTip = "صفحه اول"; ;
            this.Items.Add(b);

            b = new DevExpress.XtraReports.Web.ReportToolbarButton();
            b.ItemKind = DevExpress.XtraReports.Web.ReportToolbarItemKind.PreviousPage;
            b.Enabled = false;
            b.ToolTip = "صفحه قبل"; ;
            this.Items.Add(b);

            DevExpress.XtraReports.Web.ReportToolbarLabel lab = new DevExpress.XtraReports.Web.ReportToolbarLabel();

            lab.Text = "صفحه"; ;
            this.Items.Add(lab);

            DevExpress.XtraReports.Web.ReportToolbarComboBox com = new DevExpress.XtraReports.Web.ReportToolbarComboBox();
            com.ItemKind = DevExpress.XtraReports.Web.ReportToolbarItemKind.PageNumber;
            com.Width = new System.Web.UI.WebControls.Unit("60px");
            this.Items.Add(com);

            lab = new DevExpress.XtraReports.Web.ReportToolbarLabel();
            lab.Text = "از"; ;
            this.Items.Add(lab);

            DevExpress.XtraReports.Web.ReportToolbarTextBox tb = new DevExpress.XtraReports.Web.ReportToolbarTextBox();
            tb.ItemKind = DevExpress.XtraReports.Web.ReportToolbarItemKind.PageCount;
            tb.IsReadOnly = true;
            this.Items.Add(tb);

            b = new DevExpress.XtraReports.Web.ReportToolbarButton();
            b.ItemKind = DevExpress.XtraReports.Web.ReportToolbarItemKind.NextPage;
            b.ToolTip = "صفحه بعد"; ;
            this.Items.Add(b);

            b = new DevExpress.XtraReports.Web.ReportToolbarButton();
            b.ItemKind = DevExpress.XtraReports.Web.ReportToolbarItemKind.LastPage;
            b.ToolTip = "صفحه آخر"; ;
            this.Items.Add(b);

            sep = new DevExpress.XtraReports.Web.ReportToolbarSeparator();
            sep.Width = 3;
            this.Items.Add(sep);

            b = new DevExpress.XtraReports.Web.ReportToolbarButton();
            b.ItemKind = DevExpress.XtraReports.Web.ReportToolbarItemKind.SaveToDisk;
            b.ToolTip = "ذخیره";
            this.Items.Add(b);

            b = new DevExpress.XtraReports.Web.ReportToolbarButton();
            b.ItemKind = DevExpress.XtraReports.Web.ReportToolbarItemKind.SaveToWindow;
            b.ToolTip = "نمایش";
            this.Items.Add(b);

            com = new DevExpress.XtraReports.Web.ReportToolbarComboBox();
            com.ItemKind = DevExpress.XtraReports.Web.ReportToolbarItemKind.SaveFormat;
            com.Width = new System.Web.UI.WebControls.Unit("70px");
            com.Elements.Add(new DevExpress.XtraReports.Web.ListElement("png"));
            com.Elements.Add(new DevExpress.XtraReports.Web.ListElement("pdf"));
            com.Elements.Add(new DevExpress.XtraReports.Web.ListElement("xls"));
            com.Elements.Add(new DevExpress.XtraReports.Web.ListElement("xlsx"));
            //com.Elements.Add(new DevExpress.XtraReports.Web.ListElement("rtf"));
            //com.Elements.Add(new DevExpress.XtraReports.Web.ListElement("mht"));
           // com.Elements.Add(new DevExpress.XtraReports.Web.ListElement("txt"));
            //com.Elements.Add(new DevExpress.XtraReports.Web.ListElement("csv"));
            

            this.Items.Add(com);
           
        }
    }
}
