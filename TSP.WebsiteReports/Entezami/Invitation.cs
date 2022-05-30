using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TSP.WebsiteReports.Entezami
{
    public partial class Invitation : DevExpress.XtraReports.UI.XtraReport
    {
        public Invitation(int ClnId)
        {
            InitializeComponent();

            //----------complain info----------
            TSP.DataManager.EntezamiComplainManager ComplainManager = new DataManager.EntezamiComplainManager();
            TSP.DataManager.EntezamiMoteshakiManager MoteshakiManager = new DataManager.EntezamiMoteshakiManager();
            string StrMoteshaki = "";
            ComplainManager.FindByCode(ClnId);
            if (ComplainManager.Count == 1)
            {
                lblSubject.Text = ComplainManager[0]["Subject"].ToString();
                lblClnCode.Text = ComplainManager[0]["ClnCode"].ToString();
              //  lblName.Styles.Style.TextAlignment=DevExpress.XtraPrinting.TextAlignment.
                MoteshakiManager.FindByComplainCode(ClnId);
                if (MoteshakiManager.Count != 0)
                {
                    StrMoteshaki =  MoteshakiManager[0]["Name"].ToString();
                    for (int i = 1; i < MoteshakiManager.Count; i++)
                    {
                        StrMoteshaki = StrMoteshaki + " و " + MoteshakiManager[i]["Name"].ToString();
                    }
                    lblMoteshaki.Text = StrMoteshaki;
                }

                this.DataSource = MoteshakiManager.DataTable;
                lblName.DataBindings.Add("Text", MoteshakiManager.DataTable, "Name");
            }

            //------session info-------
            TSP.DataManager.Session.AgendaManager AgendaManager = new TSP.DataManager.Session.AgendaManager();
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.Complain);
            System.Data.DataTable dtAgenda = AgendaManager.SelectLastAgendaForEntezami(TableType, ClnId,
                (int)TSP.DataManager.Session.AgendaTypesManager.Types.Entezami_Invite);
            if (dtAgenda.Rows.Count == 1)
            {
                DateTime dt = Convert.ToDateTime(dtAgenda.Rows[0]["StartDate"]);
                lblDay.Text = Get_WeekDay(dt.DayOfWeek);
                lblDate.Text = dtAgenda.Rows[0]["StartDate"].ToString();
                lblHour.Text = dtAgenda.Rows[0]["StartTime"].ToString();
            }
        }

        private String Get_WeekDay(DayOfWeek Weekday)
        {
            String day = "";
            switch (Weekday)
            {
                case DayOfWeek.Saturday:
                    day = "شنبه";
                    break;
                case DayOfWeek.Sunday:
                    day = "یکشنبه";
                    break;
                case DayOfWeek.Monday:
                    day = "دوشنبه";
                    break;
                case DayOfWeek.Tuesday:
                    day = "سه شنبه";
                    break;
                case DayOfWeek.Wednesday:
                    day = "چهارشنبه";
                    break;
                case DayOfWeek.Thursday:
                    day = "پنج شنبه";
                    break;
                case DayOfWeek.Friday:
                    day = "جمعه";
                    break;
            }
            return day;
        }

    }
}
