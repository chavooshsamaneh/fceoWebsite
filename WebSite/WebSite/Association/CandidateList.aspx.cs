using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;

public partial class CandidateList : System.Web.UI.Page
{
    private static bool _IsGrouping;
    private static bool IsGrouping
    {
        get
        {
            return _IsGrouping;
        }
        set
        {
            _IsGrouping = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string MjId = "";
        if (Request.QueryString["ExGroupPeriodId"] == null)
        {
            Response.Redirect("ExGroupPeriods.aspx");
        }
        string ExGroupPeriodId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["ExGroupPeriodId"]).ToString());
        if (string.IsNullOrEmpty(ExGroupPeriodId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        if (!IsPostBack)
        {
            LoadExGroupPeriod(Convert.ToInt32(ExGroupPeriodId));
            FindIsGrouping();

        }

        if (IsGrouping)
        {
            TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();
            DataTable dtMajors = MajorManager.FindMjParents();
            MjId = dtMajors.Rows[0]["MjId"].ToString();
            for (int i = 0; i < dtMajors.Rows.Count; i++)
            {
                MenueMajors.Items.Add(dtMajors.Rows[i]["MjName"].ToString(), dtMajors.Rows[i]["MjId"].ToString());
            }

            OdbCandid.SelectParameters["MjId"].DefaultValue = MjId;
        }
        else OdbCandid.SelectParameters["MjId"].DefaultValue = "-1";
        if (!string.IsNullOrEmpty(MjId))
        {
            MenueMajors.Items.FindByName(MjId).Selected = true;
        }
        OdbCandid.SelectParameters["ExGroupPeriodId"].DefaultValue = ExGroupPeriodId;
        OdbCandid.SelectParameters["InActive"].DefaultValue = "0";
    }


    protected void MenueMajors_ItemClick(object source, MenuItemEventArgs e)
    {
        e.Item.Selected = true;
        if (IsGrouping)
            LoadCandidates(e.Item.Name);
        else LoadCandidates("-1");
        //RoundPanelHomePage.Visible = false;
        //DataViewManagerCandidate.Visible = true;
    }

    void LoadExGroupPeriod(int ExGroupPeriodId)
    {
        TSP.DataManager.ExGroupPeriodManager epgManager = new TSP.DataManager.ExGroupPeriodManager();
        epgManager.FindByCode(ExGroupPeriodId);
        if (epgManager.Count == 1)
        {
            RoundPanelPeriod.HeaderText = epgManager[0]["PeriodName"].ToString();
            txtExGroupName.Text = epgManager[0]["ExGroupName"].ToString();
            txtStartDate.Text = epgManager[0]["StartDate"].ToString();
            txtEndDate.Text = epgManager[0]["EndDate"].ToString();
            txtStartDatePropagation.Text = epgManager[0]["StartDatePropagation"].ToString();
            txtEndDatePropagation.Text = epgManager[0]["EndDatePropagation"].ToString();
        }
    }

    void LoadCandidates(string MjId)
    {
        OdbCandid.SelectParameters["MjId"].DefaultValue = IsGrouping ? MjId : "-1";
        OdbCandid.SelectParameters["ExGroupPeriodId"].DefaultValue = Utility.DecryptQS(Request.QueryString["ExGroupPeriodId"].ToString());
        DataViewCandidate.DataBind();
    }

    void FindIsGrouping()
    {
        string ExGroupPeriodId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["ExGroupPeriodId"]).ToString());
        TSP.DataManager.ExGroupPeriodManager ExGroupPeriodManager = new TSP.DataManager.ExGroupPeriodManager();
        ExGroupPeriodManager.FindByCode(Convert.ToInt32(ExGroupPeriodId));
        if (ExGroupPeriodManager.Count == 1)
            _IsGrouping = Convert.ToBoolean(ExGroupPeriodManager[0]["IsGrouping"]);
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ExGrpCode"] != null)
            Response.Redirect("ExGroupPeriods.aspx?ExGrpCode=" + Request.QueryString["ExGrpCode"]);
        else
            Response.Redirect("ExGroupPeriods.aspx");
    }
}