using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Association_ManagersCandidateView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DisableWarningDiv();
            int ExGroupPeriodId = -1;
            int CandidateId = -1;
            int MjId = -1;
            try
            {
                if (Request.QueryString.Count != 0)
                {
                    ExGroupPeriodId = int.Parse(Utility.DecryptQS(Request.QueryString["ExGgPrId"].ToString()));
                    CandidateId = int.Parse(Utility.DecryptQS(Request.QueryString["CandidateId"].ToString()));
                    MjId = int.Parse(Utility.DecryptQS(Request.QueryString["MjId"].ToString()));
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
            LoadExGroupPeriod(ExGroupPeriodId);
            LoadCandidateInfo(CandidateId);
            btnBack.PostBackUrl = btnBack2.PostBackUrl = "ManagersCandidate.aspx?ExGgPrId=" + Request.QueryString["ExGgPrId"].ToString() + "&MjId=" + Utility.EncryptQS(MjId.ToString());
        }
    }

    void LoadExGroupPeriod(int ExGroupPeriodId)
    {
        TSP.DataManager.ExGroupPeriodManager epgManager = new TSP.DataManager.ExGroupPeriodManager();
        epgManager.FindByCode(ExGroupPeriodId);
        if (epgManager.Count == 1)
        {
            txtExGroupName.Text = epgManager[0]["ExGroupName"].ToString();
            txtPeriodName.Text = epgManager[0]["PeriodName"].ToString();
            txtStartDate.Text = epgManager[0]["StartDate"].ToString();
            txtEndDate.Text = epgManager[0]["EndDate"].ToString();
            txtStartDatePropagation.Text = epgManager[0]["StartDatePropagation"].ToString();
            txtEndDatePropagation.Text = epgManager[0]["EndDatePropagation"].ToString();
        }
    }

    void LoadCandidateInfo(int CandidateId)
    {
        TSP.DataManager.CandidateManager CandidateManager = new TSP.DataManager.CandidateManager();
        CandidateManager.FindByCode(CandidateId);
        if (CandidateManager.Count == 1)
        {

            txtMeId.Text = CandidateManager[0]["MeId"].ToString();
            if (!Utility.IsDBNullOrNullValue(CandidateManager[0]["Resume"]))
            {
                txtResume.Html = CandidateManager[0]["Resume"].ToString();
                RoundPanelResume.Visible = true;
            }
            if (!Utility.IsDBNullOrNullValue(CandidateManager[0]["CandidateCode"]))
            {
                txtCandidateCode.Text = CandidateManager[0]["CandidateCode"].ToString();
            }
            if (!Utility.IsDBNullOrNullValue(CandidateManager[0]["Attachment"]))
            {
                HyperLinkAttachment.Visible = true;
                HyperLinkAttachment.NavigateUrl = CandidateManager[0]["Attachment"].ToString();
            }
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
            MemberManager.FindByCode(Convert.ToInt32(CandidateManager[0]["MeId"]));
            if (MemberManager.Count != 1)
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                return;
            }
            txtFirstName.Text = MemberManager[0]["FirstName"].ToString();
            txtLastName.Text = MemberManager[0]["LastName"].ToString();
            txtImpName.Text = MemberManager[0]["ImpGrdName"].ToString();
            txtDesName.Text = MemberManager[0]["DesGrdName"].ToString();
            txtObsName.Text = MemberManager[0]["ObsGrdName"].ToString();
            txtUrbonism.Text = MemberManager[0]["UrbanismGrdName"].ToString();
            txtTraffic.Text = MemberManager[0]["TrafficGrdName"].ToString();
            txtMapping.Text = MemberManager[0]["MappingGrdName"].ToString();
            MemberRequestManager.FindByMemberId(Convert.ToInt32(CandidateManager[0]["MeId"]), -1, -1);
            if (MemberRequestManager.Count > 0)
                ImgMember.ImageUrl = MemberRequestManager[0]["ImageUrl"].ToString();
            //******در دوره انتخابات ششم هیئت مدیره مجتبی زوربخش برخلاف قانون با رشته پروانه خود(عمران) کاندید نشده و به صورت فرمایشی در کدها از رشته پیش فرض آن در عضویت (ترافیک)استفاده شده
            if (Convert.ToInt32(CandidateManager[0]["ExGroupPeriodId"]) == 1)
            {
                TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
                if (Convert.ToInt32(CandidateManager[0]["MeId"]) == 4442
                   || Convert.ToInt32(CandidateManager[0]["MeId"]) == 2540)
                {
                    if (!Utility.IsDBNullOrNullValue(CandidateManager[0]["LastMjName"]))
                        txtMajor.Text = CandidateManager[0]["LastMjName"].ToString();
                    if (!Utility.IsDBNullOrNullValue(CandidateManager[0]["LastUNName"]))
                        txtUniversity.Text = CandidateManager[0]["LastUNName"].ToString();
                }
                else if (Convert.ToInt32(CandidateManager[0]["MeId"]) == 1824)
                {
                    MemberLicenceManager.FindByCode(107747);
                    if (MemberLicenceManager.Count == 1)
                    {
                        if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["MeLicenceNamertl"]))
                            txtMajor.Text = MemberLicenceManager[0]["MeLicenceNamertl"].ToString();
                        if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["UnName"]))
                            txtUniversity.Text = MemberLicenceManager[0]["UnName"].ToString();
                    }
                }
                else if (Convert.ToInt32(CandidateManager[0]["MeId"]) == 3860)
                {
                    MemberLicenceManager.FindByCode(107748);
                    if (MemberLicenceManager.Count == 1)
                    {
                        if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["MeLicenceNamertl"]))
                            txtMajor.Text = MemberLicenceManager[0]["MeLicenceNamertl"].ToString();
                        if (!Utility.IsDBNullOrNullValue(MemberLicenceManager[0]["UnName"]))
                            txtUniversity.Text = MemberLicenceManager[0]["UnName"].ToString();
                    }
                }
                else
                {
                    TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
                    DocMemberFileMajorManager.SelectMemberMasterMajor(Convert.ToInt32(CandidateManager[0]["MeId"]));
                    if (DocMemberFileMajorManager.Count > 0)
                    {
                        if (!Utility.IsDBNullOrNullValue(DocMemberFileMajorManager[0]["FMjFullName"]))
                            txtMajor.Text = DocMemberFileMajorManager[0]["FMjFullName"].ToString();
                        if (!Utility.IsDBNullOrNullValue(DocMemberFileMajorManager[0]["UnName"]))
                            txtUniversity.Text = DocMemberFileMajorManager[0]["UnName"].ToString();
                    }
                }
            }
            else
            {
                TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager = new TSP.DataManager.DocMemberFileMajorManager();
                DocMemberFileMajorManager.SelectMemberMasterMajor(Convert.ToInt32(CandidateManager[0]["MeId"]));
                if (DocMemberFileMajorManager.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(DocMemberFileMajorManager[0]["FMjFullName"]))
                        txtMajor.Text = DocMemberFileMajorManager[0]["FMjFullName"].ToString();
                    if (!Utility.IsDBNullOrNullValue(DocMemberFileMajorManager[0]["UnName"]))
                        txtUniversity.Text = DocMemberFileMajorManager[0]["UnName"].ToString();
                }
            }

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
}