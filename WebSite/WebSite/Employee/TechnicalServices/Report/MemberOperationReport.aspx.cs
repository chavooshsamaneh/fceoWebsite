using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Employee_TechnicalServices_Report_MemberOperationReport : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Attributes.Add("style", "display:block");
        this.DivReport.Attributes.Add("style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        //txtFileNo.Attributes["onkeyup"] = "return ltr_override(event)";
        if (!IsPostBack)
        {
            MemberCapacityUCDesign.ProjectIngridientTypeId = TSP.DataManager.TSProjectIngridientType.Designer;
            Session["DataTable"] = null;
            Session["DataSource"] = null;
            cmbIsFree.DataBind();
            cmbIsFree.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));            
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }
    protected void CallbackPanelMain_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        CallbackPanelMain.JSProperties["cpPrint"] = 0;
        CallbackPanelMain.JSProperties["cpCanPrint"] = 0;
        switch (e.Parameter)
        {
            case "search":
                Search();
                break;
            case "clear":
                Clear();
                break;
            case "Print":
                Search();
                if (!Utility.IsDBNullOrNullValue(txtMeIdSearch.Text))
                {
                    CallbackPanelMain.JSProperties["cpPrint"] = 1;
                    CallbackPanelMain.JSProperties["cpCanPrint"] = 1;
                }
                else
                {
                    CallbackPanelMain.JSProperties["cpPrint"] = 1;
                    CallbackPanelMain.JSProperties["cpCanPrint"] = 0;
                }
                break;
            case "PrintMe":
                if (!Utility.IsDBNullOrNullValue(txtMeIdSearch.Text))
                {
                    CallbackPanelMain.JSProperties["cpPrintMe"] = 1;
                    CallbackPanelMain.JSProperties["cpPrintUL"] = "~";
                }
                else
                {
                    CallbackPanelMain.JSProperties["cpPrintMe"] = 0;
                    CallbackPanelMain.JSProperties["cpPrintUL"] = "";
                }
                break;
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {

        GridViewProject.DataBind();
        GridViewExporter.FileName = "Report";
        GridViewExporter.WriteXlsToResponse(true);
    }
    #endregion

    #region Methods
    private void Search()
    {
        Session["DataTable"] = null;
        Session["DataSource"] = null;
        if (string.IsNullOrEmpty(txtMeIdSearch.Text) && string.IsNullOrEmpty(txtFileNo.Text))
        {
            SetLabelWarning("لطفا کد عضویت یا شماره پروانه را وارد نمایید");
            return;
        }
        int MemberId;
        if (!int.TryParse(txtMeIdSearch.Text, out MemberId))
        {
            SetLabelWarning("کد عضویت را با فرمت صحیح وارد نمایید");
            return;
        }
        string MfNo = txtFileNo.Text.Trim();

        try
        {
            if (string.IsNullOrEmpty(txtMeIdSearch.Text.Trim()) && !string.IsNullOrEmpty(MfNo))
            {
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DocMemberFileManager.SelectMainRequestByMfNo(MfNo, 0);
                txtMeIdSearch.Text = txtMeIdSearch.Text = DocMemberFileManager[0]["MeId"].ToString();
            }

            int MeId = int.Parse(txtMeIdSearch.Text.Trim());
            TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
            MeManager.FindByCode(MeId);
            if (MeManager.Count != 1)
            {
                ShowCallBackMessage("چنین کد عضویتی وجود ندارد.مجدداً وارد نمایید");
                return;
            }
            if (Utility.GetCurrentUser_AgentId() != Utility.GetCurrentAgentCode() && Convert.ToInt32(MeManager[0]["AgentId"]) != Utility.GetCurrentUser_AgentId())
            {
                ShowCallBackMessage("شما تنها قادر به جستجوی اعضای نمایندگی خود می باشید. " + (Utility.IsDBNullOrNullValue(MeManager[0]["AgentName"]) ? "نمایندگی عضو انتخاب شده" + MeManager[0]["AgentName"].ToString() + "می باشد" : ""));
                return;
            }

            WorkRequestUserControl.SetUserControlVisible(true);
            WorkRequestUserControl.UserControlvisible = true;
            WorkRequestUserControl.FillForm(MeId.ToString(), TSP.DataManager.TSMemberType.Member);
            WorkRequestUserControl.MeId = MeId;
            UserControlMeMembershipLicenceInfo.FillMemberLicence(MeId);
            FillDesignCapacity(MeId);
            FillGridProjects();

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetLabelWarning("اطلاعات وارد شده معتبر نمی باشد");
            return;
        }
    }


    private void FillDesignCapacity(int MeId)
    {
        MemberCapacityUCDesign.ProjectIngridientTypeId = TSP.DataManager.TSProjectIngridientType.Designer;
        MemberCapacityUCDesign.FillMemberCapacityInfo(MeId, 1);
    }

    private void FillGridProjects()
    {
        int MeId = -2;
        string MemberFileNo = "%";
        string FromDate = "1";
        string ToDate = "2";
        string FromDateDecreased = "1";
        string ToDateDecreased = "2";
        string ReportHeader = "";
        if (!Utility.IsDBNullOrNullValue(txtMeIdSearch.Text))
            if (!int.TryParse(txtMeIdSearch.Text.Trim(), out MeId))
                return;
        if (!Utility.IsDBNullOrNullValue(txtFileNo.Text))
        {
            if (MeId == -2)
                MeId = -1;
            MemberFileNo = txtFileNo.Text.Trim();
        }

        if (!Utility.IsDBNullOrNullValue(txtDateFrom.Text))
        {
            FromDate = txtDateFrom.Text.Trim();
            ReportHeader += "از تاریخ پروژه " + FromDate;
        }

        if (!Utility.IsDBNullOrNullValue(txtDateTo.Text))
        {
            ToDate = txtDateTo.Text.Trim();
            ReportHeader += " تا تاریخ پروژه " + ToDate;
        }
        if (!Utility.IsDBNullOrNullValue(txtDateFromDecreased.Text))
        {
            FromDateDecreased = txtDateFromDecreased.Text.Trim();
            ReportHeader += " از تاریخ کسرظرفیت " + FromDateDecreased;
        }
        if (!Utility.IsDBNullOrNullValue(txtDateToDecreased.Text))
        {
            ToDateDecreased = txtDateToDecreased.Text.Trim();
            ReportHeader += " تا تاریخ کسرظرفیت " + ToDateDecreased;
        }      
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        int AgentId = -1;
        if (!Utility.IsCurrentUserFromMainAgentId())
            AgentId = Utility.GetCurrentUser_AgentId();
        ObjdProject.SelectParameters["MeId"].DefaultValue = MeId.ToString();
        ObjdProject.SelectParameters["MemberFileNo"].DefaultValue = MemberFileNo;
        ObjdProject.SelectParameters["FromDate"].DefaultValue = FromDate;
        ObjdProject.SelectParameters["ToDate"].DefaultValue = ToDate;
        ObjdProject.SelectParameters["FromDateDecreased"].DefaultValue = FromDateDecreased;
        ObjdProject.SelectParameters["ToDateDecreased"].DefaultValue = ToDateDecreased;
        ObjdProject.SelectParameters["AgentId"].DefaultValue = AgentId.ToString();
        if (Utility.IsDBNullOrNullValue(cmbIsFree.Value))
            ObjdProject.SelectParameters["IsFree"].DefaultValue = "-1"; 
        else
            ObjdProject.SelectParameters["IsFree"].DefaultValue = cmbIsFree.Value .ToString();
        GridViewProject.DataBind();
        if (!Utility.IsDBNullOrNullValue(txtMeIdSearch.Text))
        {
            MemberManager.FindByCode(MeId);
            string ReportTitle = "";
            string MeName = "";
            string FileNo = "";
            if (MemberManager.Count == 1)
            {
                MeName = MemberManager[0]["MeName"].ToString();
                FileNo = MemberManager[0]["FileNo"].ToString();
            }

            ReportTitle = "ریز کارکرد " + MeName
                  + "\n" + "\n" + "پروانه اشتغال:" + FileNo.ToString()
            + "\n" + "\n" + "شماره عضویت:" + MeId.ToString();
            Session["DataTable"] = GridViewProject.Columns;
            Session["DataSource"] = ObjdProject;
            Session["Title"] = ReportTitle;
            CallbackPanelMain.JSProperties["cpCanPrint"] = 1;
            Session["Header"] = ReportHeader;
        }
    }

    private void Clear()
    {
        txtMeIdSearch.Text = "";
        txtFileNo.Text = "";
        txtDateFrom.Text = "- - -";
        txtDateTo.Text = "- - -";
        txtDateFromDecreased.Text = "- - -";
        txtDateToDecreased.Text = "- - -";
        cmbIsFree.SelectedIndex = 0;
        UserControlMeMembershipLicenceInfo.Clear();
        WorkRequestUserControl.ClearForm();
        FillGridProjects();
    }

    private string GetGrdName(int GrdId)
    {
        TSP.DataManager.GradeManager GradeManager = new TSP.DataManager.GradeManager();
        GradeManager.FindByCode(GrdId);
        if (GradeManager.Count > 0)
            return GradeManager[0]["GrdName"].ToString();
        else
            return "";
    }

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }

    void ShowCallBackMessage(string Msg)
    {
        CallbackPanelMain.JSProperties["cpMsg"] = Msg;
        CallbackPanelMain.JSProperties["cpError"] = 1;
    }
    #endregion

}