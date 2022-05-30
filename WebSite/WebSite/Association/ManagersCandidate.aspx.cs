using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
public partial class Association_ManagersCandidate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DisableWarningDiv();
        if (!IsPostBack)
        {
            LoadExGroupPeriod();
            FindIsGrouping();
            if (!IsGrouping)
            {
                RoundPanelHomePage.Visible = false;
                DataViewManagerCandidate.Visible = true;
                LoadCandidates("-1", "-1");
                return;
            }

            TSP.DataManager.MajorManager MajorManager = new TSP.DataManager.MajorManager();
            DataTable dtMajors = MajorManager.FindMjParents();
            for (int i = 0; i < dtMajors.Rows.Count; i++)
            {
                MenueMajors.Items.Add(dtMajors.Rows[i]["MjName"].ToString(), dtMajors.Rows[i]["MjId"].ToString());
            }
            RoundPanelHomePage.Visible = true;
            DataViewManagerCandidate.Visible = false;
            ObjdsManagerCandidate.SelectParameters["MjId"].DefaultValue = null;
            ObjdsManagerCandidate.SelectParameters["MjParentId"].DefaultValue = null;
            ObjdsManagerCandidate.SelectParameters["ExGroupPeriodId"].DefaultValue = null;

            if (Request.QueryString["MjId"] != null)
            {
                RoundPanelHomePage.Visible = false;
                DataViewManagerCandidate.Visible = true;
                string MjParentId = Utility.DecryptQS(Request.QueryString["MjId"].ToString());
                LoadCandidates("-1", MjParentId);
                MenueMajors.Items.FindByName(MjParentId).Selected = true;
            }
        }
       
    }

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

    protected void MenueMajors_ItemClick(object source, MenuItemEventArgs e)
    {
        e.Item.Selected = true;
        LoadCandidates("-1", e.Item.Name);
        RoundPanelHomePage.Visible = false;
        DataViewManagerCandidate.Visible = true;
    }

    protected void btnTitle_Click(object sender, EventArgs e)
    {
        
        //DevExpress.Web.ASPxHyperLink hp = (DevExpress.Web.ASPxHyperLink)sender;
        //hp.NavigateUrl = "ManagersCandidateView.aspx?CandidateId=" + Utility.EncryptQS(hp.ToolTip) + "&ExGgPrId=" + Request.QueryString["ExGgPrId"].ToString();
        //hp.ToolTip = "";
        LinkButton lb = (LinkButton)sender;
        TSP.DataManager.CandidateManager CandidateManager = new TSP.DataManager.CandidateManager();
        CandidateManager.FindByCode(Convert.ToInt32(lb.CommandArgument));
        if (CandidateManager.Count == 1)
        {
            if (Convert.ToInt32(CandidateManager[0]["CandidateStatus"]) == (int)TSP.DataManager.CandidateStatus.FirstCancel)
            {
                ShowMessage("امکان مشاهده اطلاعات بیشتر کاندید انتخاب شده وجود ندارد.وضعیت کاندید مورد نظر انصراف اولیه می باشد");
                return;
            }
            string MjId = Utility.EncryptQS("-1");
          //  if (IsGrouping) MjId = Utility.EncryptQS(MenueMajors.SelectedItem.Name.ToString());
            Response.Redirect(CandidateManager[0]["Attachment"].ToString());
            //Response.Redirect("ManagersCandidateView.aspx?CandidateId=" + Utility.EncryptQS(lb.CommandArgument)
            //    + "&ExGgPrId=" + Request.QueryString["ExGgPrId"].ToString() + "&MjId=" + MjId);
        }
        else
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
        }
        lb.ToolTip = "";
    }

    void LoadExGroupPeriod()
    {
        int ExGroupPeriodId = -1;
        try
        {
            if (Request.QueryString.Count != 0)
            {
                ExGroupPeriodId = int.Parse(Utility.DecryptQS(Request.QueryString["ExGgPrId"].ToString()));
            }
            else
            {
                Response.Redirect("~/Default.aspx");
                return;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            Response.Redirect("~/Default.aspx");
            return;
        }
        TSP.DataManager.ExGroupPeriodManager epgManager = new TSP.DataManager.ExGroupPeriodManager();
        epgManager.FindByCode(ExGroupPeriodId);
        if (epgManager.Count == 1)
        {
            lblPeriodName.Text = epgManager[0]["PeriodName"].ToString();
            txtExGroupName.Text = epgManager[0]["ExGroupName"].ToString();
            txtStartDate.Text = epgManager[0]["StartDate"].ToString();
            txtEndDate.Text = epgManager[0]["EndDate"].ToString();
            txtStartDatePropagation.Text = epgManager[0]["StartDatePropagation"].ToString();
            txtEndDatePropagation.Text = epgManager[0]["EndDatePropagation"].ToString();

            //---------homepage panel----
            if (!Utility.IsDBNullOrNullValue(epgManager[0]["Attachment"]))
                imgPeriodPic.ImageUrl = epgManager[0]["Attachment"].ToString();
            lblHomePagePeriodName.Text = epgManager[0]["PeriodName"].ToString();
        }
    }

    void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    void DisableWarningDiv()
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    void LoadCandidates(string MjId, string MjParentId)
    {
        ObjdsManagerCandidate.SelectParameters["MjParentId"].DefaultValue = MjParentId;
        ObjdsManagerCandidate.SelectParameters["MjId"].DefaultValue = MjId;
        ObjdsManagerCandidate.SelectParameters["ExGroupPeriodId"].DefaultValue = Utility.DecryptQS(Request.QueryString["ExGgPrId"].ToString());
        DataViewManagerCandidate.DataBind();
    }

    void FindIsGrouping()
    {
        string ExGroupPeriodId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["ExGgPrId"]).ToString());
        TSP.DataManager.ExGroupPeriodManager ExGroupPeriodManager = new TSP.DataManager.ExGroupPeriodManager();
        ExGroupPeriodManager.FindByCode(Convert.ToInt32(ExGroupPeriodId));
        if (ExGroupPeriodManager.Count == 1)
            _IsGrouping = Convert.ToBoolean(ExGroupPeriodManager[0]["IsGrouping"]);
    }
}