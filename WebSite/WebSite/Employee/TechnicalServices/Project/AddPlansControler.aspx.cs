using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DevExpress.Web;
using System.Data;
public partial class Employee_TechnicalServices_Project_AddPlansControler : System.Web.UI.Page
{
    string PageMode
    {
        get
        {
            return HiddenFieldPrjDes["PageMode"].ToString();
        }
        set
        {
            HiddenFieldPrjDes["PageMode"] = value;
        }
    }
    int ProjectId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["ProjectId"]);
        }
        set
        {
            HiddenFieldPrjDes["ProjectId"] = value;
        }
    }
    int PlansControlerId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["PlansControlerId"]);
        }
        set
        {
            HiddenFieldPrjDes["PlansControlerId"] = value;
        }
    }

    int PrjReId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["PrjReId"]);
        }
        set
        {
            HiddenFieldPrjDes["PrjReId"] = value;
        }
    }
    int PlansId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["PlansId"]);
        }
        set
        {
            HiddenFieldPrjDes["PlansId"] = value;
        }
    }

    private int GroupId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["GroupId"]);
        }
        set
        {
            HiddenFieldPrjDes["GroupId"] = value.ToString();
        }
    }
    private int StructureSkeletonId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["StructureSkeletonId"]);
        }
        set
        {
            HiddenFieldPrjDes["StructureSkeletonId"] = value.ToString();
        }
    }
    private int _PlansTypeId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldPrjDes["PlansTypeId"]);
        }
        set
        {
            HiddenFieldPrjDes["PlansTypeId"] = value.ToString();
        }
    }

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
            if (string.IsNullOrEmpty(Request.QueryString["ProjectId"]) || string.IsNullOrEmpty(Request.QueryString["PrjReId"]) || string.IsNullOrEmpty(Request.QueryString["PrjPgMd"]) || Request.QueryString["PlnPgMd"] == null || Request.QueryString["PlnId"] == null)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }
        if (this.ViewState["BtnSave"] != null)
            btnSaveAndSend.Enabled = btnSaveAndSend2.Enabled = this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

    }
    #region Btn Clicks
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();
        string QS = "PrjId=" + Utility.EncryptQS(ProjectId.ToString()) +
                    "&PrjReId=" + Utility.EncryptQS(PrjReId.ToString()) +
                    "&PrePgMd=" + Request.QueryString["PrjPgMd"] +
                    "&PlnPgMd=" + Request.QueryString["PlnPgMd"] +
                    "&PlnId=" + Utility.EncryptQS(PlansId.ToString())
                       + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
        Response.Redirect("PlanControlers.aspx?" + QS);
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //پیرو باز خورد98.02.31 اعلام به انتخاب بازبین تمام گروه ها شدند
        //////if (Utility.GetCurrentUser_AgentId() != Utility.GetCurrentAgentCode() && GroupId != (int)TSP.DataManager.TSStructureGroups.A)
        //////{
        //////    SetLabelWarning("شما تنها قادر به انتخاب بازبین جهت پروژه های گروه ساختمانی الف می باشید.");
        //////    return;
        //////}
        PageMode = "New";
        PlansControlerId = -1;
        SetNewModeKeys();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        PageMode = "Edit";
        SetEditModeKeys();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        switch (PageMode)
        {
            case "New":
                Insert(false);
                break;
        }
    }

    protected void btnSaveAndSend_Click(object sender, EventArgs e)
    {

        switch (PageMode)
        {
            case "New":
                Insert(true);
                break;
        }
    }
    #endregion 

    protected void cmbPlanControler_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbPlanControler.SelectedIndex <= -1)
        {
            SetLabelWarning("بازبین نقشه را انتخاب نمایید.");
            return;
        }
        int ControlerId = int.Parse(cmbPlanControler.SelectedItem.Value.ToString());
        TSP.DataManager.TechnicalServices.ControlerManager ControlerManager = new TSP.DataManager.TechnicalServices.ControlerManager();
        ControlerManager.FindByControlerId(ControlerId);
        if (ControlerManager.Count != 1)
        {
            SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        txtDesGrade.Text = ControlerManager[0]["DesGrdName"].ToString();
        txtImpGrade.Text = ControlerManager[0]["ImpGrdName"].ToString();
        txtMappingGrade.Text = ControlerManager[0]["MappingGrdName"].ToString();
        txtObsGrade.Text = ControlerManager[0]["ObsGrdName"].ToString();
        txtTrafficGrade.Text = ControlerManager[0]["TrafficGrdName"].ToString();
        txtUrbanismGrade.Text = ControlerManager[0]["UrbanismGrdName"].ToString();
    }

    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        int ProjectReTableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSPlans);
        int WfCode = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;
        WFUserControl.PerformCallback(PlansId, ProjectReTableType, WfCode, e);


        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        DataTable dt = WorkFlowStateManager.SelectLastStateByWfCode((int)TSP.DataManager.WorkFlows.TSPlansConfirming, PlansId);
        if (dt.Rows.Count > 0)
            WfCode = Convert.ToInt32(dt.Rows[0]["WorkFlowCode"]);

        if (WfCode == -1)
        {
            WFUserControl.SetMsgText("گردش کار درخواست جاری نامشخص است");
            return;
        }
        string GrdFlt = Request.QueryString["GrdFlt"].ToString();
        string SrchFlt = Request.QueryString["SrchFlt"].ToString();

        string QS = "PrjReId=" + Utility.EncryptQS(PrjReId.ToString()) +
                    "&PlnId=" + Utility.EncryptQS(PlansId.ToString())
                    + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
        QS = "PlanControlers.aspx?" + QS + "&PrjId=" + Utility.EncryptQS(ProjectId.ToString()) + "&PrePgMd=" + Request.QueryString["PrjPgMd"] + "&PlnPgMd=" + Request.QueryString["PlnPgMd"];

        WFUserControl.QueryStringForRedirect = QS;
        WFUserControl.PerformCallback(PlansId, ProjectReTableType, WfCode, e);
    }
    #endregion
    #region  Methods

    private void SetKeys()
    {
        try
        {
            PlansControlerId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PlnCId"].ToString()));
            PrjReId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PrjReId"].ToString()));
            PlansId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["PlnId"].ToString()));
            ProjectId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["ProjectId"].ToString()));
            PageMode = Utility.DecryptQS(Request.QueryString["PgMd"].ToString());

            SetMode(PageMode);
            CheckWorkFlowPermission();
        }
        catch (Exception Err)
        {
            Utility.SaveWebsiteError(Err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }
    }
    private void SetMode(string PageMode)
    {
        switch (PageMode)
        {
            case "View":
                SetViewModeKeys();
                break;

            case "New":
                SetNewModeKeys();
                break;

            case "Edit":
                SetEditModeKeys();
                break;
        }
    }

    private void SetNewModeKeys()
    {
        FillProjectInfo(PrjReId);
        FillForm(PlansControlerId);
        BtnNew.Enabled = true;
        BtnNew2.Enabled = true;
        btnSaveAndSend.Enabled = btnSaveAndSend2.Enabled = btnSave.Enabled = btnSave2.Enabled = true;
        CheckAccess();

        ClearForm();
        SetEnable(true);
        RoundPanelPlans.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        FillProjectInfo(PrjReId);
        FillForm(PlansControlerId);
        BtnNew.Enabled = true;
        BtnNew2.Enabled = true;
        btnSaveAndSend.Enabled = btnSaveAndSend2.Enabled = btnSave.Enabled = btnSave2.Enabled = true;
        //btnEdit.Enabled = false;
        //btnEdit2.Enabled = false;
        CheckAccess();

        SetEnable(true);

        RoundPanelPlans.HeaderText = "ویرایش";
    }

    private void SetViewModeKeys()
    {
        BtnNew.Enabled = true;
        BtnNew2.Enabled = true;
        btnSaveAndSend.Enabled = btnSaveAndSend2.Enabled = btnSave.Enabled = btnSave2.Enabled = false;
        //btnEdit.Enabled = true;
        //btnEdit2.Enabled = true;
        CheckAccess();

        SetEnable(false);
        RoundPanelPlans.HeaderText = "مشاهده";

        FillProjectInfo(PrjReId);
        FillForm(PlansControlerId);
    }
    private void CheckAccess()
    {
        TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.Plans_ControlerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        if (BtnNew.Enabled == true)
        {
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
        }

        //if (btnEdit.Enabled == true)
        //{
        //    btnEdit.Enabled = per.CanEdit;
        //    btnEdit2.Enabled = per.CanEdit;
        //}

        if (PageMode == "New" && btnSave.Enabled == true)
        {
            btnSaveAndSend.Enabled = btnSaveAndSend2.Enabled = btnSave.Enabled = btnSave2.Enabled = per.CanNew;
        }
        if (PageMode == "Edit" && btnSave.Enabled == true)
        {
            btnSaveAndSend.Enabled = btnSaveAndSend2.Enabled = btnSave.Enabled = btnSave2.Enabled = per.CanEdit;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        //this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }
    private void ClearForm()
    {
        txtDesGrade.Text = "";
        txtImpGrade.Text = "";
        txtMappingGrade.Text = "";
        txtObsGrade.Text = "";
        txtTrafficGrade.Text = "";
        txtUrbanismGrade.Text = "";
    }

    private void SetEnable(bool Enable)
    {
        cmbPlanControler.Enabled =
        txtDesGrade.Enabled =
        txtImpGrade.Enabled =
        txtMappingGrade.Enabled =
        txtObsGrade.Enabled =
        txtTrafficGrade.Enabled =
        txtUrbanismGrade.Enabled = Enable;
    }
    #region  FillForms
    private void FillForm(int PlansControlerId)
    {
        FillPlan(PlansId);
        FillPlanAttachment(PlansId);
        FillDesignerPlans(PlansId);
        FillPlansControlerViewPoint(PlansId);
        if (PageMode != "New")
        {
            TSP.DataManager.TechnicalServices.Plans_ControlerManager Plans_ControlerManager = new TSP.DataManager.TechnicalServices.Plans_ControlerManager();
            Plans_ControlerManager.FindByPlansControlerId(PlansControlerId);
            if (Plans_ControlerManager.Count != 0)
            {
                cmbPlanControler.DataBind();
                cmbPlanControler.SelectedIndex = cmbPlanControler.Items.FindByValue(Plans_ControlerManager[0]["ControlerId"].ToString()).Index;
                TSP.DataManager.TechnicalServices.ControlerManager ControlerManager = new TSP.DataManager.TechnicalServices.ControlerManager();
                ControlerManager.FindByControlerId(Convert.ToInt32(Plans_ControlerManager[0]["ControlerId"]));
                if (ControlerManager.Count != 1)
                {
                    SetLabelWarning(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
                    return;
                }
                txtDesGrade.Text = ControlerManager[0]["DesGrdName"].ToString();
                txtImpGrade.Text = ControlerManager[0]["ImpGrdName"].ToString();
                txtMappingGrade.Text = ControlerManager[0]["MappingGrdName"].ToString();
                txtObsGrade.Text = ControlerManager[0]["ObsGrdName"].ToString();
                txtTrafficGrade.Text = ControlerManager[0]["TrafficGrdName"].ToString();
                txtUrbanismGrade.Text = ControlerManager[0]["UrbanismGrdName"].ToString();
            }
        }
    }

    protected void FillProjectInfo(int Id)
    {
        prjInfo.Fill(Id);
        GroupId = prjInfo.GroupId;
        StructureSkeletonId = prjInfo.StructureSkeletonId;
    }

    private void FillPlan(int PlansId)
    {
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        PlansManager.FindByPlansId(PlansId);
        if (PlansManager.Count == 1)
        {
            txtPlanDes.Text = PlansManager[0]["Description"].ToString();
            txtPlanNo.Text = PlansManager[0]["No"].ToString();
            txtPlanType.Text = PlansManager[0]["Title"].ToString();
            string MajorList = "";
            _PlansTypeId = Convert.ToInt32(PlansManager[0]["PlansTypeId"]);
            switch (_PlansTypeId)
            {
                case (int)TSP.DataManager.TSPlansType.Memari:
                    if (GroupId == (int)TSP.DataManager.TSStructureGroups.A && StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Ajory)
                        MajorList = ((int)TSP.DataManager.MainMajors.Architecture).ToString() + "," + ((int)TSP.DataManager.MainMajors.Civil).ToString();
                    else
                        MajorList = ((int)TSP.DataManager.MainMajors.Architecture).ToString();
                    break;
                case (int)TSP.DataManager.TSPlansType.Sazeh:
                    if (GroupId == (int)TSP.DataManager.TSStructureGroups.A && StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Ajory)
                        MajorList = ((int)TSP.DataManager.MainMajors.Architecture).ToString() + "," + ((int)TSP.DataManager.MainMajors.Civil).ToString();
                    else
                        MajorList = ((int)TSP.DataManager.MainMajors.Civil).ToString();
                    break;
                case (int)TSP.DataManager.TSPlansType.Shahrsazi:
                    MajorList = ((int)TSP.DataManager.MainMajors.Urbanism).ToString();
                    break;
                case (int)TSP.DataManager.TSPlansType.TasisatBargh:
                    if (GroupId == (int)TSP.DataManager.TSStructureGroups.A && StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Ajory)
                        MajorList = ((int)TSP.DataManager.MainMajors.Electronic).ToString() + "," + ((int)TSP.DataManager.MainMajors.Mechanic).ToString();
                    else
                        MajorList = ((int)TSP.DataManager.MainMajors.Electronic).ToString();
                    break;
                case (int)TSP.DataManager.TSPlansType.TasisatMechanic:
                    if (GroupId == (int)TSP.DataManager.TSStructureGroups.A && StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Ajory)
                        MajorList = ((int)TSP.DataManager.MainMajors.Electronic).ToString() + "," + ((int)TSP.DataManager.MainMajors.Mechanic).ToString();
                    else
                        MajorList = ((int)TSP.DataManager.MainMajors.Mechanic).ToString();
                    break;

            }
            if (Utility.GetCurrentUser_AgentId() != Utility.GetCurrentAgentCode())
                ObjdsControler.SelectParameters["AgentId"].DefaultValue = Utility.GetCurrentUser_AgentId().ToString();
            else
                ObjdsControler.SelectParameters["AgentId"].DefaultValue = "-1";
            ObjdsControler.SelectParameters["AgentIdShiraz"].DefaultValue = Utility.GetCurrentAgentCode().ToString();
            ObjdsControler.SelectParameters["MajorIdList"].DefaultValue = MajorList;
            ObjdsControler.SelectParameters["InActive"].DefaultValue = "0";
        }
    }

    private void FillPlanAttachment(int PlansId)
    {
        ObjectDataSourceAttachments.SelectParameters["TableTypeId"].DefaultValue = PlansId.ToString();
        ObjectDataSourceAttachments.SelectParameters["TableType"].DefaultValue = ((int)TSP.DataManager.TableCodes.TSPlans).ToString();
        ObjectDataSourceAttachments.SelectParameters["AttachTypeId"].DefaultValue = "-1";
    }

    private void FillDesignerPlans(int PlansId)
    {
        ObjectDataSourceDesignerPlans.SelectParameters["PlansId"].DefaultValue = PlansId.ToString();
    }

    private void FillPlansControlerViewPoint(int PlansId)
    {
        ObjectDataSourcePlansControlerViewPoint.SelectParameters["PlansId"].DefaultValue = PlansId.ToString();
    }
    #endregion
    #region Insert-Update  
    private void Insert(Boolean AutoWf)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.TechnicalServices.Plans_ControlerManager PlansControlerManager = new TSP.DataManager.TechnicalServices.Plans_ControlerManager();
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        TSP.DataManager.TransactionManager transact = new TSP.DataManager.TransactionManager();
        try
        {
            if (AutoWf)
            {
                transact.Add(PlansControlerManager);
                transact.Add(WorkFlowStateManager);
                transact.Add(WorkFlowTaskManager);
                transact.Add(PlansManager);
            }
            int MeId = Convert.ToInt32(cmbPlanControler.SelectedItem.GetValue("MeId"));
            PlansControlerManager.FindActiveControlerByPlansId(PlansId);
            if (!(GroupId == (int)TSP.DataManager.TSStructureGroups.A && StructureSkeletonId == (int)TSP.DataManager.TSStructureSkeleton.Ajory && _PlansTypeId == (int)TSP.DataManager.TSPlansType.Memari))
            {
                if (PlansControlerManager.Count > 0)
                {
                    SetLabelWarning("برای این نقشه بازبین انتخاب شده است.");
                    return;
                }
            }
            PlansControlerManager.FindPlanOfControler(MeId, PlansId, 0);
            if (PlansControlerManager.Count > 0)
            {
                SetLabelWarning("بازبین انتخاب شده برای این نقشه تکراری می باشد.");
                return;
            }
            if (CheckIfIsDesigner())
            {
                SetLabelWarning("بازبین نقشه نمی تواند از طراحان همان نقشه باشد.");
                return;
            }

            if (!CheckGrade())
                return;
            if (AutoWf)
                transact.BeginSave();
            DataRow PlanControlerRow = PlansControlerManager.NewRow();


            PlanControlerRow["PlansId"] = PlansId;
            PlanControlerRow["ControlerId"] = Convert.ToInt32(cmbPlanControler.Value);
            PlanControlerRow["InActive"] = 0;
            PlanControlerRow["IsPlanConfirmed"] = 0;
            PlanControlerRow["ProjectId"] = ProjectId;
            PlanControlerRow["PrjReqId"] = PrjReId;

            PlanControlerRow["Date"] = Utility.GetDateOfToday();
            PlanControlerRow["UserId"] = Utility.GetCurrentUser_UserId();
            PlanControlerRow["ModifiedDate"] = DateTime.Now;
            PlansControlerManager.AddRow(PlanControlerRow);
            if (PlansControlerManager.Save() <= 0)
            {
                SetLabelWarning("خطا در ذخیره ایجاد شده است");
                return;
            }
            string SMSError = "";
            if (AutoWf)
            {
                if (!InsertWF(WorkFlowStateManager, WorkFlowTaskManager, PlansManager))
                {
                    transact.CancelSave();
                    SetLabelWarning("خطا در ذخیره ایجاد شده است");

                }
                transact.EndSave();
                #region SendSMS
                try
                {
                    TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
                    DataTable dtExtraInfo = WorkFlowPermission.GetMemberInfoForSMSByWFCode((int)TSP.DataManager.WorkFlows.TSPlansConfirming, Convert.ToInt32(PlansId), (int)TSP.DataManager.WorkFlowTask.ControlerConfirmingPlan, Utility.GetCurrentUser_LoginType());
                    if (dtExtraInfo.Rows.Count == 0)
                        SMSError = "اطلاعات جهت ارسال پیامک یافت نشد";

                    for (int i = 0; i < dtExtraInfo.Rows.Count; i++)
                    {
                        string SMSBody = dtExtraInfo.Rows[i]["SMSBody"].ToString();
                        SendSMSNotification(Utility.Notifications.NotificationTypes.TSPlanConfirming, SMSBody, dtExtraInfo.Rows[i]["SMSMobileNo"].ToString(), dtExtraInfo.Rows[i]["SMSMeId"].ToString(), (TSP.DataManager.UserType)(Convert.ToInt32(dtExtraInfo.Rows[i]["SMSUltId"])));
                    }
                }
                catch (Exception ex)
                {
                    Utility.SaveWebsiteError(ex);
                    SMSError = "خطا در ارسال پیامک رخ داده است";
                }
                #endregion
            }
            SetViewModeKeys();
            PageMode = "View";
            SetLabelWarning("ذخیره انجام شد" + SMSError);
        }
        catch (Exception ex)
        {
            if (AutoWf)
                transact.CancelSave();
            SetLabelWarning("خطا در ذخیره ایجاد شده است");
            Utility.SaveWebsiteError(ex);
        }
    }

    private bool InsertWF(TSP.DataManager.WorkFlowStateManager WorkFlowStateManager, TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager, TSP.DataManager.TechnicalServices.PlansManager PlansManager)
    {
        int TaskId = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.AssignControlerToPlan;
        int TableId = PlansId;

        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count != 1)
        {
            SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است.");
            return false;
        }
        TaskId = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);

        int CurrentNmcId = FindNmcId(TaskId);
        if (CurrentNmcId == -1)
        {
            SetLabelWarning("دسترسی شما در گردش کار تنظیم نشده است.");
            return false;
        }

        int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, (int)TSP.DataManager.WorkFlowTask.ControlerConfirmingPlan, CurrentNmcId, Utility.GetCurrentUser_UserId(), 0);
        if (WfStart > 0)
        {
            WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.ControlerConfirmingPlan);
            if (WorkFlowTaskManager.Count != 1)
            {
                SetLabelWarning("خطایی در بازخوانی اطلاعات رخ داده است.");
                return false;
            }
            TaskId = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);

            if (PlansManager.UpdateRequestTaskId(TableId, TaskId, WfStart) == 0)
                return true;
        }

        SetLabelWarning("خطایی در ذخیره انجام گرفته است.");
        return false;
    }

    private int FindNmcId(int TaskId)
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int NmcId = -1;
        NmcId = NezamChartManager.FindNmcId(UserId, TaskId, LoginManager);
        if (NmcId > 0)
        {
            return NmcId;
        }
        else
        {
            SetLabelWarning("اطلاعات شما به عنوان انجام دهنده عملیات انتخاب شده در گردش کار ثبت نشده است.");
            return (-1);
        }
    }
    #endregion
    /****************************************************************************************************************************************/
    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
            if (se.Number == 2601)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                SetLabelWarning("کد تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                SetLabelWarning("اطلاعات وابسته معتبر نمی باشد");
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            SetLabelWarning("خطایی در ذخیره انجام گرفته است");
        }
    }

    private bool CheckIfIsDesigner()
    {
        int MeId = Convert.ToInt32(cmbPlanControler.SelectedItem.GetValue("MeId"));
        string listDesignerMeId = "";
        TSP.DataManager.TechnicalServices.Project_DesignerManager ProjectDesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
        DataTable dtDesignerList = ProjectDesignerManager.GetTSProjectDesignerListNezamMembers(ProjectId, PrjReId);
        if (dtDesignerList.Rows.Count != 0)
        {
            listDesignerMeId = dtDesignerList.Rows[0]["Melist"].ToString();
            string[] DesList = listDesignerMeId.Split(',');
            for (int i = 0; i < DesList.Length; i++)
            {
                if (DesList[i] == MeId.ToString())
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool CheckGrade()
    {
        int MeId = Convert.ToInt32(cmbPlanControler.SelectedItem.GetValue("MeId"));
        TSP.DataManager.MemberManager memberManager = new TSP.DataManager.MemberManager();
        DataTable dtMemberGradeId = memberManager.SelectMemberGradeIdsFromtblMember(MeId);

        if (CheckIfConsultantDesigners())
            return true;

        int ControllerGradeId = 0;
        if (!Utility.IsDBNullOrNullValue(dtMemberGradeId.Rows[0]["dsId"]))
            ControllerGradeId = Convert.ToInt32(dtMemberGradeId.Rows[0]["dsId"]);
        DataTable dt = GetDesignersMaxGrade();
        int DesignersGradeId =Convert.ToInt32( dt.Rows[0]["GradeId"]);
        Boolean HasDesignId = Convert.ToBoolean(Convert.ToInt32( dt.Rows[0]["HasDesignId"]));

        if (ControllerGradeId <= 0)
        {
            SetLabelWarning("بازبین انتخاب شده دارای پایه و صلاحیت در رشته مربوط به این نقشه نمی باشد.");
            return false;
        }

        if (DesignersGradeId <= 0)
        {
            SetLabelWarning("خطایی در بازخوانی اطلاعات پایه و صلاحیت طراحان نقشه رخ داده است.");
            return false;
        }

        if (ControllerGradeId > DesignersGradeId && HasDesignId)
        {
            SetLabelWarning("پایه بازبین نقشه باید برابر یا بیشتر از پایه طراحان باشد.");
            return false;
        }

        if (ControllerGradeId <= DesignersGradeId || !HasDesignId)
            return true;

        return false;
    }

    private DataTable GetDesignersMaxGrade()
    {
        int GradeId = 0;
        DataTable dt = new DataTable();
        dt.Columns.Add("Id");
        dt.Columns["Id"].AutoIncrement = true;
        dt.Columns["Id"].AutoIncrementSeed = 1;
        dt.Constraints.Add("PK_ID", dt.Columns["Id"], true);
        dt.Columns.Add("GradeId");
        dt.Columns.Add("HasDesignId");
        DataRow dr = dt.NewRow();
        int HasDesignId = 1;
        TSP.DataManager.TechnicalServices.Designer_PlansManager DesignerPlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
        DesignerPlansManager.FindActivesByPlansId(PlansId);
        for (int i = 0; i < DesignerPlansManager.Count; i++)
        {
            int MeId = Convert.ToInt32(DesignerPlansManager[i]["MeId"]);
            int DesId = Convert.ToInt32(DesignerPlansManager[i]["DesId"]);
            GradeId = (DesId > 0 && (DesId < GradeId || GradeId == 0)) ? DesId : GradeId;
            if (Convert.ToInt32(DesignerPlansManager[i]["HasDesignId"]) == 0)
                HasDesignId = Convert.ToInt32(DesignerPlansManager[i]["HasDesignId"]);
        }
        dr["GradeId"] = GradeId;
        dr["HasDesignId"] = HasDesignId;
        dt.Rows.Add(dr);
        return dt;
    }

    private bool CheckIfConsultantDesigners()
    {
        TSP.DataManager.TechnicalServices.Designer_PlansManager DesignerPlansManager = new TSP.DataManager.TechnicalServices.Designer_PlansManager();
        DesignerPlansManager.FindActivesByPlansId(PlansId);
        for (int i = 0; i < DesignerPlansManager.Count; i++)
        {
            int PrjDesignerId = Convert.ToInt32(DesignerPlansManager[i]["PrjDesignerId"]);
            TSP.DataManager.TechnicalServices.Project_DesignerManager ProjectDesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
            ProjectDesignerManager.FindByPrjDesignerId(PrjDesignerId);
            if (ProjectDesignerManager.Count > 0 && Convert.ToInt32(ProjectDesignerManager[0]["MemberTypeId"]) != (int)TSP.DataManager.TSMemberType.ConsultantCompany)
                return false;
        }
        return true;
    }

    #region WF Permissions
    private void CheckWorkFlowPermission()
    {
        CheckWorkFlowPermissionForEdit(PageMode);
    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        //*******Editing Task Code
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.AssignControlerToPlan;
        int WFCode = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;

        TSP.DataManager.WFPermission WFPer = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(TaskCode, WFCode, PlansId, Utility.GetCurrentUser_UserId(), PageMode);
        //btnEdit.Enabled = WFPer.BtnEdit;
        //btnEdit2.Enabled = WFPer.BtnEdit;
        btnSaveAndSend.Enabled = btnSaveAndSend2.Enabled = btnSave.Enabled = btnSave2.Enabled = WFPer.BtnSave;
        BtnNew.Enabled = WFPer.BtnNew;
        BtnNew2.Enabled = WFPer.BtnNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;
    }
    #endregion
    private void SendSMSNotification(Utility.Notifications.NotificationTypes NotificationType, string SMSBody, string SMSMobileNo, string SMSMeId, TSP.DataManager.UserType SMSUltId)
    {

        if (String.IsNullOrWhiteSpace(SMSMobileNo) == false)
        {

            DataTable dtRecivers = new DataTable();
            dtRecivers.Columns.Add("SMSMobileNo");
            dtRecivers.Columns.Add("SMSMeId");
            dtRecivers.Columns.Add("SMSUltId");
            dtRecivers.Columns.Add("Description");

            DataRow dr = dtRecivers.NewRow();
            dr["SMSMobileNo"] = SMSMobileNo;
            dr["SMSMeId"] = SMSMeId;
            dr["SMSUltId"] = ((int)SMSUltId).ToString();
            dr["Description"] = SMSBody;

            dtRecivers.Rows.Add(dr);
            dtRecivers.AcceptChanges();

            //send sms
            SendSMSNotification SMSNotifications = new SendSMSNotification(NotificationType);
            SMSNotifications.SendSMSNote(dtRecivers, SMSBody);
        }
    }
    #endregion


}