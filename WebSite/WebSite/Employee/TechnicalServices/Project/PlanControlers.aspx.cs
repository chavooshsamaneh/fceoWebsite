using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
public partial class Employee_TechnicalServices_Project_PlanControlers : System.Web.UI.Page
{

    #region Properties
    private string PlansId
    {
        get
        {
            return HiddenFieldDesPlans["PlnId"].ToString();
        }
        set
        {
            HiddenFieldDesPlans["PlnId"] = value;
        }
    }

    private string PrjReId
    {
        get
        {
            return HiddenFieldDesPlans["PrjReId"].ToString();
        }
        set
        {
            HiddenFieldDesPlans["PrjReId"] = value;
        }
    }

    private string ProjectId
    {
        get
        {
            return HiddenFieldDesPlans["PrjId"].ToString();
        }
        set
        {
            HiddenFieldDesPlans["PrjId"] = value.ToString();
        }
    }


    private int GroupId
    {
        get
        {
            return Convert.ToInt32( HiddenFieldDesPlans["GroupId"]);
        }
        set
        {
            HiddenFieldDesPlans["GroupId"] = value.ToString();
        }
    }

    private Boolean _CanEditProjectInfoInThisRequest
    {
        get
        {
            return Convert.ToBoolean(HiddenFieldDesPlans["CanEditProjectInfoInThisRequest"]);
        }
        set
        {
            HiddenFieldDesPlans["CanEditProjectInfoInThisRequest"] = value.ToString();
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Request.QueryString["SrchFlt"].ToString();
            if (string.IsNullOrEmpty(Request.QueryString["PrjId"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]))
            {
                Response.Redirect("Project.aspx?" + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt);
            }

            if (Request.QueryString["PlnPgMd"] == null || Request.QueryString["PlnId"] == null)
            {
                string QS = "ProjectId=" + Request.QueryString["PrjId"].ToString() +
                    "&PrjReId=" + Request.QueryString["PrjReId"].ToString() +
                    "&PageMode=" + Request.QueryString["PageMode"].ToString() +
                    "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
                Response.Redirect("Plans.aspx?" + QS);
            }

            TSP.DataManager.Permission perDes = TSP.DataManager.TechnicalServices.Plans_ControlerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            BtnNew.Enabled = perDes.CanNew;
            BtnNew2.Enabled = perDes.CanNew;
            btnView.Enabled = perDes.CanView;
            btnView2.Enabled = perDes.CanView;
            btnInActive.Enabled = perDes.CanDelete;
            btnInActive2.Enabled = perDes.CanDelete;
            GridViewControler.Visible = perDes.CanView;
            btnDesAcc.Enabled = btnDesAcc2.Enabled = perDes.CanView;

            SetKey();
            CheckWorkFlowPermission();
            if(!_CanEditProjectInfoInThisRequest)
                BtnNew.Enabled =BtnNew2.Enabled =btnInActive.Enabled = btnInActive2.Enabled =  btnDesAcc.Enabled = btnDesAcc2.Enabled = false;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnDelete"] = btnInActive.Enabled;
            this.ViewState["btnDesAcc"] = btnDesAcc.Enabled;
        }

        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["btnDesAcc"] != null)
            this.btnDesAcc.Enabled = this.btnDesAcc2.Enabled = (bool)this.ViewState["btnDesAcc"];
    }

    #region btn Click
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        Delete_Inactive();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string QS = "ProjectId=" + Utility.EncryptQS(ProjectId) +
                    "&PrjReId=" + Utility.EncryptQS(PrjReId) +
                    "&PageMode=" + HiddenFieldDesPlans["PrjPgMd"].ToString() +
                    "&PlnPgMd=" + Request.QueryString["PlnPgMd"] +
                    "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
        Response.Redirect("Plans.aspx?" + QS);
    }

    protected void btnDesAcc_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string QS = "&ProjectId=" + HiddenFieldDesPlans["PrjId"].ToString() +
                "&PrjReId=" + HiddenFieldDesPlans["PrjReId"].ToString() +
                "&IngT=" + Utility.EncryptQS(((int)TSP.DataManager.TSProjectIngridientType.Designer).ToString()) +
                "&PageMode=" + HiddenFieldDesPlans["PrjPgMd"].ToString() +
                "&PlnPgMd=" + Request.QueryString["PlnPgMd"] +
                "&UrlReferrer=" + Utility.EncryptQS("PlanDesigner.aspx") +
                "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

        Response.Redirect("ProjectAccounting.aspx?" + QS);

    }
    #endregion

    protected void MainMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string PageMode = HiddenFieldDesPlans["PrjPgMd"].ToString();

        PrjMainMenu MainMenu = new PrjMainMenu("Project", Convert.ToInt32(ProjectId));
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        Response.Redirect(MainMenu.GetRedirectLink(e.Item.Name, Utility.EncryptQS(PrjReId), PageMode, GrdFlt, SrchFlt));
    }
    protected void MenuPlan_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();

        string QS = "PrjReId=" + Utility.EncryptQS(PrjReId) +
                    "&PlnId=" + Utility.EncryptQS(PlansId)
                    + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

        switch (e.Item.Name)
        {
            case "Plan":
                QS = QS +"&PrjId=" + Utility.EncryptQS(ProjectId) + "&PrePgMd=" + HiddenFieldDesPlans["PrjPgMd"].ToString() + "&PlnPgMd=" + Request.QueryString["PlnPgMd"];
                Response.Redirect("AddPlans.aspx?" + QS);
                break;

            case "PlanDes":
                QS = QS + "&ProjectId=" + Utility.EncryptQS(ProjectId) + "&PlnPgMd=" + Request.QueryString["PlnPgMd"]
                    + "&PlanPageMode=" + Request.QueryString["PlnPgMd"] + "&PageMode=" + HiddenFieldDesPlans["PrjPgMd"].ToString();
                Response.Redirect("PlanDesigner.aspx?" + QS);
                break;

        }
    }

    #endregion

    #region Methods
    /**************************************************************************************************************************************************/
    private void SetKey()
    {
        try
        {
            PrjReId = Utility.DecryptQS(Request.QueryString["PrjReId"].ToString());
            PlansId = Utility.DecryptQS(Request.QueryString["PlnId"].ToString());
            ProjectId = Utility.DecryptQS(Request.QueryString["PrjId"].ToString());

            HiddenFieldDesPlans["PrjPgMd"] = Request.QueryString["PrePgMd"].ToString();
            HiddenFieldDesPlans["PlnPgMd"] = Request.QueryString["PlnPgMd"].ToString();
            HiddenFieldDesPlans["PlansTypeId"] = "";

            if (string.IsNullOrEmpty(PlansId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            ObjdsPlansControler.SelectParameters["PlansId"].DefaultValue = PlansId;
            ObjdsPlansControler.SelectParameters["ProjectId"].DefaultValue = ProjectId;
            ObjdsPlansControler.SelectParameters["PrjReId"].DefaultValue = PrjReId;

            FillProjectInfo(int.Parse(PrjReId));
            FillPlanInfo();

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    protected void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
        GroupId = prjInfo.GroupId;
        _CanEditProjectInfoInThisRequest = prjInfo.CanEditProjectInfoInThisRequest;
    }

    private void FillPlanInfo()
    {
        if (string.IsNullOrEmpty(PlansId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();

        DataTable dtPlan = PlansManager.SelectById(Convert.ToInt32(PlansId), -1);

        if (dtPlan.Rows.Count == 1)
        {

            if (!Utility.IsDBNullOrNullValue(dtPlan.Rows[0]["No"]))
                txtPlanNo.Text = dtPlan.Rows[0]["No"].ToString();
            if (!Utility.IsDBNullOrNullValue(dtPlan.Rows[0]["PlansType"]))
                txtPlanType.Text = dtPlan.Rows[0]["PlansType"].ToString();
            if (!Utility.IsDBNullOrNullValue(dtPlan.Rows[0]["PlanVersion"]))
                txtPlanVer.Text = dtPlan.Rows[0]["PlanVersion"].ToString();

            HiddenFieldDesPlans["PlansTypeId"] = dtPlan.Rows[0]["PlansTypeId"];

            txtFollowCode.Text = dtPlan.Rows[0]["FollowCode"].ToString();


            if (!Utility.IsDBNullOrNullValue(dtPlan.Rows[0]["TaskName"]))
                lblWorkFlowState.Text = "وضعیت درخواست ثبت طراح و نقشه مربوطه: " + dtPlan.Rows[0]["TaskName"].ToString();
            else
                lblWorkFlowState.Text = "وضعیت درخواست ثبت طراح و نقشه مربوطه: " + "نامشخص";
        }
    }

    private void NextPage(string Mode)
    {
        int PlansControlerId = -1;
        int focucedIndex = GridViewControler.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewControler.GetDataRow(focucedIndex);
            PlansControlerId = (int)row["PlansControlerId"];

        }
        if (PlansControlerId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                PlansControlerId = -1;
            }
            string GrdFlt = Request.QueryString["GrdFlt"].ToString();
            string SrchFlt = Request.QueryString["SrchFlt"].ToString();

            string QS = "PlnCId=" + Utility.EncryptQS(PlansControlerId.ToString()) +
                "&PgMd=" + Utility.EncryptQS(Mode) +
                "&ProjectId=" + Utility.EncryptQS(ProjectId) +
                "&PrjReId=" + Utility.EncryptQS(PrjReId) +
                "&PlnPgMd=" + Request.QueryString["PlnPgMd"] +
                "&PlnId=" + Utility.EncryptQS(PlansId) +
                "&PrjPgMd=" + HiddenFieldDesPlans["PrjPgMd"].ToString() +
                //"&PrjDesignerId=" + Utility.EncryptQS(PrjDesignerId.ToString()) +
                //"&PageSender=" + Utility.EncryptQS("PlanDesigner") +
                "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

            Response.Redirect("AddPlansControler.aspx?" + QS);
        }
    }

    private void Delete_Inactive()
    {
        int PlansControlerId = -1;
        int ProjectId = -1;
        int PrjReqId = -1;

        if (GridViewControler.FocusedRowIndex > -1)
        {
            DataRow row = GridViewControler.GetDataRow(GridViewControler.FocusedRowIndex);
            PlansControlerId = (int)row["PlansControlerId"];
            ProjectId = (int)row["ProjectId"];
            PrjReqId = (int)row["PrjReqId"];
        }

        if (PrjReqId == -1 || ProjectId == -1 || PlansControlerId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاًابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            //if (PrjReqId == Convert.ToInt32(PrjReId))
            //    Delete(PlansControlerId);
            //else
                InActive(PlansControlerId);
        }
    }

    private void InActive(int TableId)
    {
        try
        {
            InsertInActive(TableId, Convert.ToInt32(PrjReId), (int)TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSPlans_Controler), (int)TSP.DataManager.TableType.TSProjectRequest);

            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد.";

            GridViewControler.DataBind();
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    protected void InsertInActive(int TableId, int ReqId, int TableType, int ReTableType)
    {
        TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();

        DataRow dr = Manager.NewRow();

        dr.BeginEdit();
        dr["TableId"] = TableId;
        dr["TableType"] = TableType;
        dr["ReqId"] = ReqId;
        dr["ReqType"] = ReTableType;
        dr["InActive"] = 1;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        dr.EndEdit();

        Manager.AddRow(dr);
        Manager.Save();
    }

    private void Delete(int PlansControlerId)
    {
        TSP.DataManager.TechnicalServices.Plans_ControlerManager Plans_ControlerManager = new TSP.DataManager.TechnicalServices.Plans_ControlerManager();

        try
        {
            Plans_ControlerManager.FindByPlansControlerId(PlansControlerId);
            if (Plans_ControlerManager.Count <= 0)
            {
                ShowMessage("خطا در بازیابی اطلاعات ایجاد شده است.");
                return;
            }
            Plans_ControlerManager[0].Delete();
            Plans_ControlerManager.Save();
            ShowMessage("ذخیره انجام شد.");
            GridViewControler.DataBind();
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }    

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
            }
            else if (se.Number == 2627)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
            }
            else if (se.Number == 547)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        }
    }

    #region WorkFlow Methods
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForEdit();
    }

    private void CheckWorkFlowPermissionForEdit()
    {
        TSP.DataManager.WFPermission WFPer = CheckPlanWorkFlowPermissionForEdit();
        BtnNew.Enabled = WFPer.BtnNew;
        BtnNew2.Enabled = WFPer.BtnNew;
        btnInActive.Enabled = WFPer.BtnInactive;
        btnInActive2.Enabled = WFPer.BtnInactive;

        this.ViewState["BtnNew"] = BtnNew.Enabled;
        this.ViewState["BtnDelete"] = btnInActive.Enabled;
    }

    private TSP.DataManager.WFPermission CheckPlanWorkFlowPermissionForEdit()
    {

        int WFCode = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;

        //*******Editing Task Code
        int PlanTaskCode = (int)TSP.DataManager.WorkFlowTask.AssignControlerToPlan;

        TSP.DataManager.WFPermission PerPlan = TSP.DataManager.WorkFlowPermission.CheckWFPermissionForEditForManagementPage(PlanTaskCode, WFCode, Convert.ToInt32(PlansId), Utility.GetCurrentUser_UserId());

        return PerPlan;
    }

    private int GetCurrentTaskCode(int WfCode, int TableId)
    {
        int CurrentTaskOrder = -2;
        int CurrentTaskCode = -2;

        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dtWorkFlowState = WorkFlowStateManager.SelectLastStateByWfCode(WfCode, TableId);
        if (dtWorkFlowState.Rows.Count > 0)
        {
            CurrentTaskOrder = int.Parse(dtWorkFlowState.Rows[0]["TaskOrder"].ToString());
            CurrentTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
        }

        return CurrentTaskCode;
    }

    #endregion

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
}