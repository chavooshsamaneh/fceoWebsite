using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TSP.WebControls
{
    public partial class BaseXtraReport : DevExpress.XtraReports.UI.XtraReport
    {
        public BaseXtraReport()
        {
            InitializeComponent();
        }
        virtual protected void SetDataBinding(System.Data.DataTable dtSource)
        {
            this.DataSource = dtSource;
        }
       
    }
}
