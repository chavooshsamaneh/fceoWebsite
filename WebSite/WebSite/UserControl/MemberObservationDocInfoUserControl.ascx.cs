using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_MemberObservationDocInfoUserControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void FillInfo(int MeId)
    {
        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
        System.Data.DataTable dtObsWorkReq = ObserverWorkRequestChangesManager.SelectLastRequest(-1, MeId);

        if (dtObsWorkReq.Rows.Count == 0)
            lblHasReqSaved.Text = "ثبت نشده";
        else
        {
            lblHasReqSaved.Text = "ثبت شده";
            lblRequestDate.Text = dtObsWorkReq.Rows[0]["CreateDate"].ToString(); 
        }
    }

    public void Clear()
    {
        lblRequestDate.Text = lblHasReqSaved.Text = "- - -";
    }
}