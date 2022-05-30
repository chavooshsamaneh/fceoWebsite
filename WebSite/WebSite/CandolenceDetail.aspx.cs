using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CandolenceDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["CdlId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        string s = Utility.DecryptQS(Request.QueryString["CdlId"]).ToString();

        if (string.IsNullOrEmpty(s))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        Session["CdlId"] = s;
        FillCondolence();
    }

    private void FillCondolence()
    {
        TSP.DataManager.CondolenceManager CondolenceManager = new TSP.DataManager.CondolenceManager();
        int id = Convert.ToInt32(Session["CdlId"].ToString());
        CondolenceManager.FindByCode(id);
        if (CondolenceManager.Count != 0)
        {
            if (Convert.ToInt32(CondolenceManager[0]["Type"]) == 1)
                lblTitle.Text = "پیام تبریک";
            else lblTitle.Text = "پیام تسلیت";
            lblDate.Text = CondolenceManager[0]["StartDate"].ToString();
            lblSummary.Text = CondolenceManager[0]["Summary"].ToString();
            lblBody.Text = CondolenceManager[0]["Description"].ToString();
            if (!Utility.IsDBNullOrNullValue(CondolenceManager[0]["CdlFrom"]))
                lblFrom.Text = "از طرف " + CondolenceManager[0]["CdlFrom"].ToString();
            if (!Utility.IsDBNullOrNullValue(CondolenceManager[0]["CdlImage"]))
            {
                imgImage.Visible = true;
                imgImage.Src = CondolenceManager[0]["CdlImage"].ToString();
            }
            else
                imgImage.Visible = false;
        }
    }
}