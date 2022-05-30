using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TSP.WebsiteReports.ReportBuilder
{
    public partial class XtraReport : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport(int ReportId)
        {
            InitializeComponent();
            CreateReport(ReportId);
        }

        private void CreateReport(int ReportId)
        {
            TSP.DataManager.ReportBuilderManager ReportBuilderManager = new DataManager.ReportBuilderManager();
            ReportBuilderManager.FindById(ReportId);
            this.PaperKind = (System.Drawing.Printing.PaperKind)Convert.ToInt32(ReportBuilderManager[0]["PaperKind"]);
            this.Landscape = Convert.ToBoolean(ReportBuilderManager[0]["IsLandscape"]);

            DataTable dtReport = ReportBuilderManager.ExecuteQuery(ReportBuilderManager[0]["Query"].ToString());

            XRLabel label = new XRLabel();
            label.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            label.Font = new System.Drawing.Font("B Titr", 14F, FontStyle.Bold);
            PageHeader.Controls.Add(label);

            if (dtReport.Rows.Count > 0)
            {
                int padding = 10;
                int tableWidth = this.PageWidth - this.Margins.Left - this.Margins.Right - padding * 2;

                XRTable dynamicTable = XRTable.CreateTable(
                                    new Rectangle(padding,    // rect X
                                                    0,          // rect Y
                                                    tableWidth, // width
                                                    40),        // height
                                                    1,          // table row count
                                                    0);         // table column count
                XRTable tableHeaders = XRTable.CreateTable(
                                     new Rectangle(padding,
                                         50,
                                         tableWidth,
                                         40),
                                         1,
                                         0);

                dynamicTable.Width = tableWidth;
                dynamicTable.Rows.FirstRow.Width = tableWidth;
                dynamicTable.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) | DevExpress.XtraPrinting.BorderSide.Bottom)));
                //dynamicTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
                dynamicTable.BorderWidth = 1;
                tableHeaders.Width = tableWidth;
                tableHeaders.Rows.FirstRow.Width = tableWidth;
                tableHeaders.Borders = DevExpress.XtraPrinting.BorderSide.All;
                tableHeaders.BorderWidth = 1;
                tableHeaders.Font = new System.Drawing.Font("Tahoma", 9F, FontStyle.Bold);

                int i = 0;
                dynamicTable.BeginInit();
                tableHeaders.BeginInit();
                foreach (DataColumn dc in dtReport.Columns)
                {
                    XRTableCell cell = new XRTableCell();
                    XRTableCell header = new XRTableCell();

                    XRBinding binding = new XRBinding("Text", dtReport, dtReport.Columns[i].ColumnName);
                    cell.DataBindings.Add(binding);
                    cell.CanGrow = false;
                    cell.Width = 100;
                    cell.Text = dc.ColumnName;
                    dynamicTable.Rows.FirstRow.Cells.Add(cell);

                    header.CanGrow = false;
                    header.Width = 100;
                    header.Text = dc.ColumnName;
                    tableHeaders.Rows.FirstRow.Cells.Add(header);

                    i++;
                }
                dynamicTable.Font = new System.Drawing.Font("Tahoma", 8F);

                Detail.Controls.Add(dynamicTable);
                PageHeader.Controls.Add(tableHeaders);

                label.Text = ReportBuilderManager[0]["Title"].ToString();
                //this.DisplayName = ReportBuilderManager[0]["Title"].ToString();
                label.Width = tableWidth;

                XRPageInfo PageInfo = new XRPageInfo();
                PageInfo.Format = "صفحه {0:#}";
                PageInfo.WidthF = (float)tableWidth;
                PageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
                PageInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                PageInfo.PageInfo = DevExpress.XtraPrinting.PageInfo.Number;
                PageFooter.Controls.Add(PageInfo);

                this.DataSource = dtReport;
                dynamicTable.EndInit();
                tableHeaders.EndInit();
            }

            else
            {
                label.Text = string.Format("داده ای برای نمایش وجود ندارد");
            }
        }
    }
}
