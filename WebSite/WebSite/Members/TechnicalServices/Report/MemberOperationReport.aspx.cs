using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Members_TechnicalServices_Report_MemberOperationReport : System.Web.UI.Page
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
            Session["DataTable"] = null;
            Session["DataSource"] = null;
            cmbIsFree.DataBind();
            cmbIsFree.Items.Insert(0, new DevExpress.Web.ListEditItem("همه موارد", null));
            cmbIsFree.SelectedIndex = cmbIsFree.Items.FindByValue("0").Index;
        }

        Search();
    }

    protected void CallbackPanelMain_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        CallbackPanelMain.JSProperties["cpPrint"] = 0;
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
                CallbackPanelMain.JSProperties["cpPrint"] = 1;
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
        CallbackPanelMain.JSProperties["cpPrint"] = 0;
        CallbackPanelMain.JSProperties["cpCanPrint"] = 0;
        Session["DataTable"] = null;
        Session["DataSource"] = null;

        try
        {
            FillMemberInfo(Utility.GetCurrentUser_MeId());
            FillGridProjects();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            Clear();
            ShowCallBackMessage("اطلاعات وارد شده معتبر نمی باشد");
            return;
        }
    }

    void FillMemberInfo(int MeId)
    {
        UserControlMeMembershipLicenceInfo.FillMemberLicence(MeId);
        UserControlMeMembershipInfo.FillMember(MeId);
        UserControlMeDocumentInfo.FillMember(MeId);
        UserControlMeOfficeInfoUserControl.FillInfo(MeId);
        UserControlMeEngOfficeInfoUserControl.FillInfo(MeId);
        UserControlMemberObservationDocInfo.FillInfo(MeId);
        UserControlMemberImplementDocInfo.FillInfo(MeId);
    }

    private void FillGridProjects()
    {
        int MeId = Utility.GetCurrentUser_MeId();
        string MemberFileNo = "%";
        string FromDate = "1";
        string ToDate = "2";
        string FromDateDecreased = "1";
        string ToDateDecreased = "2";
        string ReportHeader = "";

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
            ObjdProject.SelectParameters["IsFree"].DefaultValue = cmbIsFree.Value.ToString();
        GridViewProject.DataBind();
        MemberManager.FindByCode(Utility.GetCurrentUser_MeId());
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

    private void Clear()
    {
        txtDateFrom.Text = "- - -";
        txtDateTo.Text = "- - -";
        txtDateFromDecreased.Text = "- - -";
        txtDateToDecreased.Text = "- - -";
        cmbIsFree.SelectedIndex = cmbIsFree.Items.FindByValue("0").Index;
        UserControlMeMembershipLicenceInfo.Clear();
        UserControlMeMembershipInfo.Clear();
        UserControlMeDocumentInfo.Clear();
        UserControlMeEngOfficeInfoUserControl.Clear();
        UserControlMemberObservationDocInfo.Clear();
        UserControlMemberImplementDocInfo.Clear();
        GridViewProject.DataSource = null;
        GridViewProject.DataBind();
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