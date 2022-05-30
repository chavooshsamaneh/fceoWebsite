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
using DevExpress.Web;
using System.IO;


public partial class Employee_Amoozesh_SeminarView : System.Web.UI.Page
{
    #region Comments
    //int _SeId
    //{
    //    get
    //    {
    //        return Convert.ToInt32(SeminarId.Value);
    //    }
    //    set
    //    {
    //        SeminarId.Value = value.ToString();
    //    }
    //}

    //int _SeReqId
    //{
    //    get
    //    {
    //        return Convert.ToInt32(SeminarRequestId.Value);
    //    }
    //    set
    //    {
    //        SeminarRequestId.Value = value.ToString();
    //    }
    //}

    //string _PageMode
    //{
    //    get { return PgMode.Value; }
    //    set { PgMode.Value = value; }
    //}
    //#region Events
    //protected void Page_Load(object sender, EventArgs e)
    //{

    //    this.DivReport.Visible = false;
    //    this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
    //    this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

    //    if (!IsPostBack)
    //    {
    //        try
    //        {
    //            Session["IsEdited_SeminarView"] = false;

    //            if (string.IsNullOrEmpty(Request.QueryString["SeId"]) || string.IsNullOrEmpty(Request.QueryString["PageMode"]))
    //            {
    //                Response.Redirect("Seminar.aspx");
    //                return;
    //            }

    //            _SeReqId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["SeReqId"]));
    //            _SeId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["SeId"]));
    //            _PageMode = Utility.DecryptQS(Request.QueryString["PageMode"]).ToString();


    //            if (_SeId == null)
    //            {
    //                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
    //                return;
    //            }

    //            if (string.IsNullOrEmpty(_PageMode))
    //            {
    //                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
    //                return;
    //            }


    //            CheckWorkFlowPermission();

    //            this.ViewState["BtnSave"] = btnSave.Enabled;
    //            this.ViewState["BtnJudge"] = btnJudge.Enabled;
    //        }
    //        catch (Exception err)
    //        {
    //            Utility.SaveWebsiteError(err);
    //            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
    //            return;
    //        }
    //    }
    //    if (this.ViewState["BtnSave"] != null)
    //        this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
    //    if (this.ViewState["BtnJudge"] != null)
    //        this.btnJudge.Enabled = this.btnJudge2.Enabled = (bool)this.ViewState["BtnJudge"];
    //}

    //protected void btnBack_Click(object sender, EventArgs e)
    //{
    //    if (!string.IsNullOrEmpty(_PageMode))
    //    {
    //        if (_PageMode == "PRView")
    //            Response.Redirect("MemberLicence.aspx");

    //        else
    //        {
    //            if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && _SeId != null)
    //            {
    //                string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
    //                Response.Redirect("Seminar.aspx?PostId=" + Utility.EncryptQS(_SeId.ToString()) + "&GrdFlt=" + GrdFlt);
    //            }
    //            else
    //            {
    //                Response.Redirect("Seminar.aspx");
    //            }
    //        }

    //    }
    //    else
    //        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

    //}

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    if (_PageMode == "New")
    //        Insert(_SeId);
    //    else if (_PageMode == "EditReq")
    //    {
    //        EditRequest(_SeReqId);
    //    }
    //    else if (_PageMode == "Change")
    //    {
    //        InsertRequest(_SeReqId);
    //    }
    //    else if (_PageMode == "Edit")
    //    {
    //        int SeJudgeId = int.Parse(Utility.DecryptQS(JudgeId.Value));
    //        Edit(SeJudgeId, _SeId);
    //    }
    //}   

    //protected void AspxGridSchedule_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    //{
    //    if (e.DataColumn.FieldName == "Date")
    //        e.Cell.Style["direction"] = "ltr";
    //}

    //#endregion

    //#region Methods
    //private void SetMode()
    //{
    //    if (_PageMode == "View")
    //    {
    //        InsertWorkFlowStateView((int)TSP.DataManager.TableCodes.Seminar, _SeReqId);
    //        FillFormByRequest(_SeReqId);
    //        btnSave.Enabled = false;
    //        btnSave2.Enabled = false;
    //    }
    //    else if (_PageMode == "PRView")
    //    {
    //        ASPxRoundPanel5.Visible = false;
    //        ASPxRoundPanel8.Visible = false;
    //        ASPxRoundPanel9.Visible = false;
    //        btnSave.Visible = false;
    //        btnSave2.Visible = false;
    //        FillView(_SeId);

    //    }

    //}
    //private void EditRequest(int SeReqId)
    //{

    //}

    //private void InsertRequest(int SeReqId)
    //{

    //}

    //protected void Edit(int SeJudgeId, int SeId)
    //{
    //    TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
    //    TSP.DataManager.TrainingJudgmentManager JudgeManager = new TSP.DataManager.TrainingJudgmentManager();
    //    TSP.DataManager.SeminarManager SeminarManager = new TSP.DataManager.SeminarManager();
    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

    //    trans.Add(JudgeManager);
    //    trans.Add(SeminarManager);
    //    trans.Add(WorkFlowStateManager);

    //    try
    //    {
    //        JudgeManager.FindByCode(SeJudgeId);
    //        if (JudgeManager.Count <= 0)
    //        {
    //            trans.CancelSave();

    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "خطایی در ویرایش اطلاعات رخ داده است";
    //            return;
    //        }
    //        JudgeManager[0].BeginEdit();
    //        JudgeManager[0]["JudgeViewPoint"] = txtJudgeView.Text;
    //        JudgeManager[0]["MeetingId"] = txtMeeting.Text;
    //        JudgeManager[0]["IsConfirmed"] = rbtnGrade.Value;
    //        JudgeManager[0]["JudgeGrade"] = txtPoint.Text;
    //        JudgeManager[0]["JudgeGradeTime"] = txtTimePoint.Text;
    //        JudgeManager[0]["UltId"] = Utility.GetCurrentUser_LoginType();
    //        JudgeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //        JudgeManager[0]["ModifiedDate"] = DateTime.Now;

    //        JudgeManager[0].EndEdit();

    //        trans.BeginSave();
    //        if (JudgeManager.Save() > 0)
    //        {

    //            SeminarManager.FindByCode(SeId);
    //            SeminarManager[0].BeginEdit();
    //            SeminarManager[0]["Point"] = txtPoint.Text;
    //            SeminarManager[0]["TimePoint"] = txtTimePoint.Text;
    //            SeminarManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //            SeminarManager[0].EndEdit();
    //            SeminarManager.Save();

    //            int UpdateState = -1;
    //            if (!(Convert.ToBoolean(Session["IsEdited_SeminarView"].ToString())))
    //            {
    //                int TableType = (int)TSP.DataManager.TableCodes.Seminar;
    //                int UpdateTableType = (int)TSP.DataManager.TableCodes.TrainingJudgment;
    //                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, SeId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
    //            }
    //            if (UpdateState == -4)
    //            {
    //                trans.CancelSave();
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
    //                return;
    //            }
    //            else
    //            {

    //                Session["IsEdited_SeminarView"] = true;
    //                trans.EndSave();

    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "ذخیره انجام شد";
    //            }


    //        }
    //        else
    //        {
    //            trans.CancelSave();

    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
    //        }

    //    }
    //    catch (Exception err)
    //    {
    //        trans.CancelSave();
    //        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
    //        {
    //            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
    //            if (se.Number == 2601)
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
    //            }

    //            else
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //                this.LabelWarning.Text = err.Message;
    //            }
    //        }
    //        else
    //        {
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //            this.LabelWarning.Text = err.Message;
    //        }
    //    }
    //}

    //protected void Insert(int SeId)
    //{
    //    TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
    //    TSP.DataManager.TrainingJudgmentManager SemJudgeManager = new TSP.DataManager.TrainingJudgmentManager();
    //    TSP.DataManager.SeminarManager SeminarManager = new TSP.DataManager.SeminarManager();
    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
    //    TSP.DataManager.NezamChartManager NmcManager = new TSP.DataManager.NezamChartManager();
    //    TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();

    //    trans.Add(SemJudgeManager);
    //    trans.Add(SeminarManager);
    //    trans.Add(WorkFlowStateManager);

    //    try
    //    {
    //        DataRow drJudge = SemJudgeManager.NewRow();
    //        drJudge["PkId"] = SeId;
    //        drJudge["JudgeViewPoint"] = txtJudgeView.Text;
    //        if (!string.IsNullOrEmpty(txtMeeting.Text))
    //            drJudge["MeetingId"] = int.Parse(txtMeeting.Text);
    //        drJudge["IsConfirmed"] = rbtnGrade.Value;
    //        // drJudge["UltId"] = Utility.GetCurrentUser_LoginType();
    //        drJudge["UserId"] = Utility.GetCurrentUser_UserId();
    //        drJudge["Type"] = 1;//seminar
    //        drJudge["JudgeGrade"] = txtPoint.Text;
    //        drJudge["JudgeGradeTime"] = txtTimePoint.Text;
    //        drJudge["ModifiedDate"] = DateTime.Now;
    //        drJudge["CreateDate"] = Utility.GetDateOfToday();
    //        drJudge["NmcId"] = NmcManager.FindNmcId(Utility.GetCurrentUser_UserId(), LogManager);
    //        SemJudgeManager.AddRow(drJudge);

    //        trans.BeginSave();
    //        if (SemJudgeManager.Save() > 0)
    //        {

    //            if (!string.IsNullOrEmpty(txtPoint.Text))
    //            {
    //                SeminarManager.FindByCode(SeId);
    //                if (SeminarManager.Count == 1)
    //                {
    //                    SeminarManager[0].BeginEdit();
    //                    SeminarManager[0]["Point"] = txtPoint.Text;
    //                    SeminarManager[0]["TimePoint"] = txtTimePoint.Text;
    //                    SeminarManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
    //                    SeminarManager[0].EndEdit();
    //                    SeminarManager.Save();
    //                }
    //            }

    //            int UpdateState = -1;
    //            if (!(Convert.ToBoolean(Session["IsEdited_SeminarView"].ToString())))
    //            {
    //                int TableType = (int)TSP.DataManager.TableCodes.Seminar;
    //                int UpdateTableType = (int)TSP.DataManager.TableCodes.TrainingJudgment;
    //                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, SeId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId());
    //            }
    //            if (UpdateState == -4)
    //            {
    //                trans.CancelSave();
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
    //                return;
    //            }
    //            else
    //            {
    //                Session["IsEdited_SeminarView"] = true;
    //                trans.EndSave();

    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "ذخیره انجام شد";
    //            }


    //        }
    //        else
    //        {
    //            trans.CancelSave();
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        trans.CancelSave();
    //        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
    //        {
    //            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
    //            if (se.Number == 2601)
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
    //            }

    //            else
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //                this.LabelWarning.Text = err.Message;
    //            }
    //        }
    //        else
    //        {
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //            this.LabelWarning.Text = err.Message;
    //        }
    //    }
    //}

    //private void InsertWorkFlowStateView(int TableType, int TableId)
    //{
    //    TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
    //    try
    //    {
    //        int ViewState = WorkFlowStateManager.InsertWorkFlowViewState(TableType, TableId, "مشاهده سمینار توسط کارمند", Utility.GetCurrentUser_UserId());
    //        if (ViewState == -4)
    //        {
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
    //        {
    //            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
    //            if (se.Number == 2601)
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "اطلاعات تکراری می باشد";
    //            }
    //            else
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //            }
    //        }
    //        else
    //        {
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //        }
    //    }
    //}

    //protected void FillView(int SeId)
    //{
    //    OdbGrades.SelectParameters["PkId"].DefaultValue = _SeId.ToString();
    //    OdbGrades.SelectParameters["Type"].DefaultValue = "1";
    //    CustomAspxDevGridViewGrade.DataBind();
    //    OdbSchedule.FilterParameters[0].DefaultValue = _SeId.ToString();
    //    TSP.DataManager.SeminarManager SeManager = new TSP.DataManager.SeminarManager();
    //    TSP.DataManager.TrainingTeachersManager SemTeachManager = new TSP.DataManager.TrainingTeachersManager();

    //    try
    //    {
    //        SeManager.FindByCode(SeId);
    //        if (SeManager.Count > 0)
    //        {
    //            txtDate.Text = SeManager[0]["StartDate"].ToString();
    //            txtEndDate.Text = SeManager[0]["EndDate"].ToString();
    //            txtDesc.Text = SeManager[0]["Description"].ToString();
    //            txtPlace.Text = SeManager[0]["Place"].ToString();
    //            txtTopic.Text = SeManager[0]["Topic"].ToString();
    //            txtDuration.Text = SeManager[0]["Duration"].ToString();

    //            decimal SeminarCost = Convert.ToDecimal(SeManager[0]["SeminarCost"].ToString());
    //            txtSeminarCost.Text = SeminarCost.ToString("#,#");
    //            //decimal TeacherSalary = Convert.ToDecimal(SeManager[0]["TeacherSalary"].ToString());
    //            //txtTeacherCost.Text = TeacherSalary.ToString("#,#");

    //            txtSubject.Text = SeManager[0]["Subject"].ToString();
    //            txtTime.Text = SeManager[0]["Time"].ToString();


    //            SemTeachManager.FindByPKCode(SeId, 1);
    //            if (SemTeachManager.Count > 0)
    //            {
    //                Grdv_Teacher.DataSource = SemTeachManager.DataTable;
    //                Grdv_Teacher.DataBind();
    //            }
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        this.DivReport.Visible = true;
    //        this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";

    //    }
    //}

    //protected void FillForm(int SeId)
    //{
    //    OdbGrades.SelectParameters["PkId"].DefaultValue = _SeId.ToString();
    //    OdbGrades.SelectParameters["Type"].DefaultValue = "1";
    //    CustomAspxDevGridViewGrade.DataBind();
    //    OdbSchedule.FilterParameters[0].DefaultValue = _SeId.ToString();
    //    TSP.DataManager.SeminarManager SeManager = new TSP.DataManager.SeminarManager();
    //    TSP.DataManager.TrainingTeachersManager SemTeachManager = new TSP.DataManager.TrainingTeachersManager();

    //    TSP.DataManager.TrainingJudgmentManager JudgeManager = new TSP.DataManager.TrainingJudgmentManager();

    //    try
    //    {
    //        SeManager.FindByCode(SeId);
    //        if (SeManager.Count > 0)
    //        {
    //            txtDate.Text = SeManager[0]["StartDate"].ToString();
    //            txtEndDate.Text = SeManager[0]["EndDate"].ToString();
    //            txtDesc.Text = SeManager[0]["Description"].ToString();
    //            txtPlace.Text = SeManager[0]["Place"].ToString();
    //            txtTopic.Text = SeManager[0]["Topic"].ToString();
    //            txtDuration.Text = SeManager[0]["Duration"].ToString();

    //            decimal SeminarCost = Convert.ToDecimal(SeManager[0]["SeminarCost"].ToString());
    //            txtSeminarCost.Text = SeminarCost.ToString("#,#");
    //            //decimal TeacherSalary = Convert.ToDecimal(SeManager[0]["TeacherSalary"].ToString());
    //            //txtTeacherCost.Text = TeacherSalary.ToString("#,#");

    //            txtSubject.Text = SeManager[0]["Subject"].ToString();
    //            txtTime.Text = SeManager[0]["Time"].ToString();


    //            SemTeachManager.FindByPKCode(SeId, 1);
    //            if (SemTeachManager.Count > 0)
    //            {
    //                Grdv_Teacher.DataSource = SemTeachManager.DataTable;
    //                Grdv_Teacher.DataBind();
    //            }
    //            // int TeId = int.Parse(SeManager[0]["TeId"].ToString());
    //            //TeManager.FindByCode(TeId);
    //            //if (TeManager.Count > 0)
    //            //{
    //            //    txtTeBirthDate.Text = TeManager[0]["BirthDate"].ToString();
    //            //    txtTeEmail.Text = TeManager[0]["Email"].ToString();
    //            //    txtTeFamily.Text = TeManager[0]["Family"].ToString();
    //            //    txtTeFatherName.Text = TeManager[0]["Father"].ToString();
    //            //    txtTeIdNo.Text = TeManager[0]["IdNo"].ToString();
    //            //    txtTeLicence.Text = TeManager[0]["LiName"].ToString();
    //            //    txtTeMajor.Text = TeManager[0]["MjName"].ToString();
    //            //    txtTeMobileNo.Text = TeManager[0]["MobileNo"].ToString();
    //            //    txtTeName.Text = TeManager[0]["Name"].ToString();
    //            //    txtTeSSN.Text = TeManager[0]["SSN"].ToString();
    //            //    txtTeTel.Text = TeManager[0]["Tel"].ToString();
    //            //    txtTeType.Text = TeManager[0]["TypeName"].ToString();

    //            //    if (TeManager[0]["Type"].ToString() == "1" || TeManager[0]["Type"].ToString() == "3")
    //            //        ASPxRoundPanel7.Visible = true;
    //            //    else
    //            //        ASPxRoundPanel7.Visible = false;


    //            TSP.DataManager.AttachmentsManager AttManager = new TSP.DataManager.AttachmentsManager();
    //            AspxGridFlp.DataSource = AttManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.Seminar, SeId, 2);
    //            AspxGridFlp.DataBind();

    //            string State = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["SeId"]).ToString());
    //            if (!string.IsNullOrEmpty(State))
    //            {
    //                if (State == "LearningGrade")
    //                {
    //                    lblPoint.Visible = true;
    //                    txtPoint.Visible = true;
    //                    lblMeetingId.Visible = false;
    //                    txtMeeting.Visible = false;
    //                    lblTiPoint.Visible = true;
    //                    txtTimePoint.Visible = true;
    //                }
    //                else if (State == "ExpertGroup")
    //                {
    //                    lblPoint.Visible = false;
    //                    txtPoint.Visible = false;
    //                    lblMeetingId.Visible = true;
    //                    txtMeeting.Visible = true;
    //                    lblTiPoint.Visible = false;
    //                    txtTimePoint.Visible = false;
    //                }
    //            }
    //            else
    //                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());


    //            JudgeManager.FindByPKCode(SeId, 1);
    //            if (JudgeManager.Count > 0)
    //            {
    //                JudgeId.Value = Utility.EncryptQS(JudgeManager[0]["JudgeId"].ToString());
    //             _PageMode="Edit";
    //                RoundPanelJudge.Visible = true;
    //                txtJudgeView.Text = JudgeManager[0]["JudgeViewPoint"].ToString();
    //                txtMeeting.Text = JudgeManager[0]["MeetingId"].ToString();
    //                if (txtPoint.Visible == true)
    //                    txtPoint.Text = SeManager[0]["Point"].ToString();
    //                if (txtTimePoint.Visible == true)
    //                    txtTimePoint.Text = SeManager[0]["TimePoint"].ToString();
    //                rbtnGrade.SelectedIndex = int.Parse(JudgeManager[0]["IsConfirmed"].ToString());

    //            }
    //            else
    //              _PageMode="New";


    //        }
    //        else
    //        {
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        this.DivReport.Visible = true;
    //        this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";

    //    }
    //}

    //protected void FillFormByRequest(int SeReqId)
    //{
    //    OdbGrades.SelectParameters["PkId"].DefaultValue = _SeId.ToString();
    //    OdbGrades.SelectParameters["Type"].DefaultValue = "1";
    //    CustomAspxDevGridViewGrade.DataBind();
    //    OdbSchedule.FilterParameters[0].DefaultValue = _SeId.ToString();
    //    TSP.DataManager.SeminarRequestManager SeminarRequestManager = new TSP.DataManager.SeminarRequestManager();
    //    TSP.DataManager.TrainingTeachersManager SemTeachManager = new TSP.DataManager.TrainingTeachersManager();

    //    TSP.DataManager.TrainingJudgmentManager JudgeManager = new TSP.DataManager.TrainingJudgmentManager();

    //    try
    //    {
    //        SeminarRequestManager.FindByCode(SeReqId);
    //        if (SeminarRequestManager.Count > 0)
    //        {
    //            txtDate.Text = SeminarRequestManager[0]["StartDate"].ToString();
    //            txtEndDate.Text = SeminarRequestManager[0]["EndDate"].ToString();
    //            txtDesc.Text = SeminarRequestManager[0]["Description"].ToString();
    //            txtPlace.Text = SeminarRequestManager[0]["Place"].ToString();
    //            txtTopic.Text = SeminarRequestManager[0]["Topic"].ToString();
    //            txtDuration.Text = SeminarRequestManager[0]["Duration"].ToString();

    //            decimal SeminarCost = Convert.ToDecimal(SeminarRequestManager[0]["SeminarCost"].ToString());
    //            txtSeminarCost.Text = SeminarCost.ToString("#,#");
    //            txtSubject.Text = SeminarRequestManager[0]["Subject"].ToString();
    //            txtTime.Text = SeminarRequestManager[0]["Time"].ToString();


    //            SemTeachManager.FindByPeriodRequestId(_SeId, _SeReqId, 1);
    //            if (SemTeachManager.Count > 0)
    //            {
    //                Grdv_Teacher.DataSource = SemTeachManager.DataTable;
    //                Grdv_Teacher.DataBind();
    //            }

    //            TSP.DataManager.AttachmentsManager AttManager = new TSP.DataManager.AttachmentsManager();
    //            AspxGridFlp.DataSource = AttManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.Seminar, _SeId, 2);
    //            AspxGridFlp.DataBind();

    //            //JudgeManager.FindByPKCode(_SeId, 1);
    //            //if (JudgeManager.Count > 0)
    //            //{
    //            //    JudgeId.Value = Utility.EncryptQS(JudgeManager[0]["JudgeId"].ToString());
    //            //    PgMode.Value = Utility.EncryptQS("Edit");
    //            //    RoundPanelJudge.Visible = true;
    //            //    txtJudgeView.Text = JudgeManager[0]["JudgeViewPoint"].ToString();
    //            //    txtMeeting.Text = JudgeManager[0]["MeetingId"].ToString();
    //            //    if (txtPoint.Visible == true)
    //            //        txtPoint.Text = SeminarRequestManager[0]["Point"].ToString();
    //            //    if (txtTimePoint.Visible == true)
    //            //        txtTimePoint.Text = SeminarRequestManager[0]["TimePoint"].ToString();
    //            //    rbtnGrade.SelectedIndex = int.Parse(JudgeManager[0]["IsConfirmed"].ToString());

    //            //}
    //            //else
    //            //    PgMode.Value = Utility.EncryptQS("New");


    //        }
    //        else
    //        {
    //            this.DivReport.Visible = true;
    //            this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        Utility.SaveWebsiteError(err);
    //        this.DivReport.Visible = true;
    //        this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";

    //    }
    //}

    //#region WF
    //private void CheckWorkFlowPermission()
    //{
    //    CheckWorkFlowPermissionForEdit(_PageMode);
    //}

    //private void CheckWorkFlowPermissionForEdit(string PageMode)
    //{
    //    //*****TableId

    //    //*******Editing Task Code
    //    int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveSeminarInfo;
    //    int SeminarCommitteeGradingTaskCode = (int)TSP.DataManager.WorkFlowTask.SeminarLearningCommitteeGrading;
    //    int SeminarExpertCommitteeConfirmingTaskCode = (int)TSP.DataManager.WorkFlowTask.SeminarExpertCommitteeConfirming;
    //    int WFCode = (int)TSP.DataManager.WorkFlows.SeminarConfirming;

    //    TSP.DataManager.WFPermission WFPerCommitteeGrading = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(SeminarCommitteeGradingTaskCode, WFCode, _SeReqId, Utility.GetCurrentUser_UserId(), PageMode);
    //    TSP.DataManager.WFPermission WFPerSeminarExpertCommitteeConfirming = TSP.DataManager.WorkFlowPermission.CheckPermissionForEdit(SeminarExpertCommitteeConfirmingTaskCode, WFCode, _SeReqId, Utility.GetCurrentUser_UserId(), PageMode);

    //    btnSave.Enabled = btnSave2.Enabled = WFPerCommitteeGrading.BtnSave || WFPerSeminarExpertCommitteeConfirming.BtnSave;
    //    btnJudge.Enabled = btnJudge2.Enabled = WFPerCommitteeGrading.BtnSave || WFPerSeminarExpertCommitteeConfirming.BtnSave;
    //    this.ViewState["BtnSave"] = btnSave.Enabled;
    //    this.ViewState["BtnJudge"] = btnJudge.Enabled;
    //}
    //#endregion
    //#endregion
    #endregion

    DataTable dtOfImg = null;
    DataTable dtOfSchedule = null;
    DataTable dtOfTe = null;
    DataTable dtOfGrade = null;
    int _SeId
    {
        get
        {
            try
            {
                return Convert.ToInt32(HiddenFieldInfo["SeId"]);
            }
            catch
            {
                return Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["SeId"].ToString())));
            }
        }
        set
        {
            HiddenFieldInfo["SeId"] = value;
        }
    }

    //int _InsId
    //{
    //    get
    //    {
    //        try
    //        {
    //            return Convert.ToInt32(HiddenFieldInfo["InsId"]);
    //        }
    //        catch
    //        {
    //            return Utility.GetCurrentUser_MeId();
    //        }
    //    }
    //    set
    //    {
    //        HiddenFieldInfo["InsId"] = value;
    //    }
    //}

    string _PageMode
    {
        get
        {
            return HiddenFieldInfo["PageMode"].ToString();
        }
        set
        {
            HiddenFieldInfo["PageMode"] = value;
        }
    }

    int _SeReqId
    {
        get
        {
            try
            {
                return Convert.ToInt32(HiddenFieldInfo["SeReqId"]);
            }
            catch
            {
                return Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["SeReqId"].ToString())));
            }
        }
        set
        {
            HiddenFieldInfo["SeReqId"] = value;
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

            Session["IsEdited_Seminar"] = false;
            Session["SeminarUpload"] = null;
            Session["SemTeacher"] = null;
            Session["TblOfImg6"] = null;
            Session["SeminarSchedule"] = null;
            Session["SeGrade"] = null;
            #region Create Datatable
            if (Session["SemTeacher"] == null)
            {
                dtOfTe = new DataTable();
                dtOfTe.Columns.Add("TeId");
                dtOfTe.Columns.Add("SeTeId");
                dtOfTe.Columns.Add("TypeName");
                dtOfTe.Columns.Add("Name");
                dtOfTe.Columns.Add("Family");
                dtOfTe.Columns.Add("Father");
                dtOfTe.Columns.Add("SSN");
                dtOfTe.Columns.Add("Salary");
                dtOfTe.Columns.Add("Description");
                dtOfTe.Columns.Add("Id");
                dtOfTe.Columns["Id"].AutoIncrement = true;
                dtOfTe.Columns["Id"].AutoIncrementSeed = 1;

                Session["SemTeacher"] = dtOfTe;
            }


            if (Session["TblOfImg6"] == null)
            {
                dtOfImg = new DataTable();
                dtOfImg.Columns.Add("Image", typeof(byte[]));
                dtOfImg.Columns.Add("ImgUrl");
                dtOfImg.Columns.Add("TempImgUrl");
                dtOfImg.Columns.Add("fileName");
                dtOfImg.Columns.Add("Mode");
                dtOfImg.Columns.Add("Code");
                dtOfImg.Columns.Add("Description");
                dtOfImg.Columns.Add("Id");
                dtOfImg.Columns["Id"].AutoIncrement = true;
                dtOfImg.Columns["Id"].AutoIncrementSeed = 1;

                Session["TblOfImg6"] = dtOfImg;
            }


            if (Session["SeminarSchedule"] == null)
            {
                dtOfSchedule = new DataTable();
                dtOfSchedule.Columns.Add("TimeFrom");
                dtOfSchedule.Columns.Add("TimeTo");
                dtOfSchedule.Columns.Add("Subject");
                dtOfSchedule.Columns.Add("Date");
                dtOfSchedule.Columns.Add("Description");
                dtOfSchedule.Columns.Add("Id");
                dtOfSchedule.Columns["Id"].AutoIncrement = true;
                dtOfSchedule.Columns["Id"].AutoIncrementSeed = 1;

                Session["SeminarSchedule"] = dtOfSchedule;
            }


            if (Session["SeGrade"] == null)
            {
                dtOfGrade = new DataTable();
                dtOfGrade.Columns.Add("GMRId");
                dtOfGrade.Columns.Add("UpGrdPId");
                dtOfGrade.Columns.Add("GrdName");
                dtOfGrade.Columns.Add("ResName");
                dtOfGrade.Columns.Add("MjName");
                dtOfGrade.Columns.Add("Description");
                dtOfGrade.Columns.Add("Id");
                dtOfGrade.Columns["Id"].AutoIncrement = true;
                dtOfGrade.Columns["Id"].AutoIncrementSeed = 1;
                dtOfGrade.Constraints.Add("PK_ID", dtOfGrade.Columns["Id"], true);

                Session["SeGrade"] = dtOfGrade;
            }

            #endregion

            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]))
            {
                Response.Redirect("Seminar.aspx");
                return;
            }
            TSP.DataManager.Permission per = TSP.DataManager.SeminarManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            //btnDisActive.Enabled = per.CanEdit;
            //btnDisActive2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            try
            {
                _PageMode = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"].ToString()));
                //  _InsId = Utility.GetCurrentUser_MeId();
                _SeId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["SeId"]).ToString()));
                _SeReqId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["SeReqId"]).ToString()));
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            #region SetPageMode
            if (string.IsNullOrEmpty(_PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            switch (_PageMode)
            {
                case "View":

                    SetViewMode();
                    break;
                case "New":
                    SetNewMode();
                    break;
                case "Edit":
                    SetEditMode();
                    break;
                case "Change":
                    SetChangeMode();
                    break;
            }
            #endregion
            CheckWorkFlowPermission();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }

        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];



        if (!Utility.IsDBNullOrNullValue(Session["TblOfImg6"]))
        {
            AspxGridFlp.DataSource = (DataTable)Session["TblOfImg6"];
            AspxGridFlp.DataBind();
        }

        if (!Utility.IsDBNullOrNullValue(Session["SemTeacher"]))
        {
            Grdv_Teacher.DataSource = (DataTable)Session["SemTeacher"];
            Grdv_Teacher.DataBind();
        }

        if (!Utility.IsDBNullOrNullValue(Session["SeminarSchedule"]))
        {
            AspxGridSchedule.DataSource = (DataTable)Session["SeminarSchedule"];
            AspxGridSchedule.DataBind();
        }

        if (!Utility.IsDBNullOrNullValue(Session["SeGrade"]))
        {
            CustomAspxDevGridViewGrade.DataSource = (DataTable)Session["SeGrade"];
            CustomAspxDevGridViewGrade.DataBind();
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session.Remove("SeminarSchedule");
        Session.Remove("TblOfImg6");
        Session.Remove("SemTeacher");
        Session.Remove("SeGrade");

        Response.Redirect("Seminar.aspx?GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);

    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        SetNewMode();
        this.ViewState["BtnSave"] = btnSave.Enabled;
        //this.ViewState["BtnEdit"] = btnEdit.Enabled;

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        SetEditMode();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Grdv_Teacher.VisibleRowCount == 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات برای ذخیره کافی نمی باشد.مشخصات سخنران را وارد نمایید";
            return;
        }

        if (string.IsNullOrEmpty(_PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        if (_PageMode == "New")
        {
            Insert();

        }
        else if (_PageMode == "Edit")
        {

            if (_SeReqId == null)
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            Edit(_SeReqId);
        }
        else if (_PageMode == "Change")
        {
            InsertRequest();
        }
    }

    protected void btnAddFlp_Click(object sender, EventArgs e)
    {
        // string fileName = "", pathAx = "", extension = "";
        byte[] img = null;

        if (Session["TblOfImg6"] != null)
        {
            dtOfImg = (DataTable)Session["TblOfImg6"];

            DataRow dr = dtOfImg.NewRow();

            try
            {
                if (Session["SeminarUpload"] != null)
                {
                    //extension = Path.GetExtension(flp.FileName);
                    //extension = extension.ToLower();

                    //fileName = Utility.GenerateName(Path.GetExtension(flp.FileName));
                    //pathAx = Server.MapPath("~/image/Temp/");
                    //flp.SaveAs(pathAx + fileName);
                    dr[0] = img;
                    dr[1] = "~/Image/Seminar/" + System.IO.Path.GetFileName(Session["SeminarUpload"].ToString());
                    dr[3] = System.IO.Path.GetFileName(Session["SeminarUpload"].ToString());
                    dr[2] = "~/Image/temp/" + System.IO.Path.GetFileName(Session["SeminarUpload"].ToString());
                    dr[6] = txtDescImg.Text;
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "فایل مورد نظر را انتخاب نمایید";
                    return;
                }

                dr[4] = 0;
                dtOfImg.Rows.Add(dr);
                AspxGridFlp.DataSource = dtOfImg;
                AspxGridFlp.DataBind();

                Session["SeminarUpload"] = null;

                txtDescImg.Text = "";
                imgEndUploadImg.ClientVisible = false;

            }
            catch
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
            }

        }

    }

    protected void btnAddSchedule_Click(object sender, EventArgs e)
    {
        if (Session["SeminarSchedule"] != null)
        {
            dtOfSchedule = (DataTable)Session["SeminarSchedule"];

            DataRow dr = dtOfSchedule.NewRow();

            try
            {
                dr["TimeFrom"] = txtSchTimeFrom.Text;
                dr["TimeTo"] = txtSchTimeTo.Text;
                dr["Subject"] = txtSchSubject.Text;
                dr["Description"] = txtSchDesc.Text;
                dr["Date"] = txtScheduleDate.Text;


                dtOfSchedule.Rows.Add(dr);
                AspxGridSchedule.DataSource = dtOfSchedule;
                AspxGridSchedule.DataBind();

                // Session["TeacherUpload"] = null;
                txtSchDesc.Text = "";
                txtSchSubject.Text = "";
                txtSchTimeFrom.Text = "";
                txtSchTimeTo.Text = "";
                txtScheduleDate.Text = "";
                // txtShetualeDate.Text = "";

            }
            catch
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
            }
        }
    }

    protected void btnAddTe_Click(object sender, EventArgs e)
    {
        if (Session["SemTeacher"] != null)
        {
            dtOfTe = (DataTable)Session["SemTeacher"];

            DataRow dr = dtOfTe.NewRow();

            try
            {
                if (string.IsNullOrEmpty(txtTeID.Text))
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
                    return;
                }
                if (dtOfTe.Rows.Count > 0)
                {
                    for (int i = 0; i < dtOfTe.Rows.Count; i++)
                    {
                        //if (dtOfTe.Rows[i]["Type"].ToString() == "2")//Teacher
                        //{
                        if (dtOfTe.Rows[i].RowState != DataRowState.Deleted && dtOfTe.Rows[i]["TeId"].ToString() == txtTeID.Text)
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "شخص مورد نظر قبلاً انتخاب شده است";
                            return;
                        }
                        // }
                        //else if (dtOfTe.Rows[i]["Name"].ToString() == txtTeName.Text && dtOfTe.Rows[i]["Family"].ToString() == txtTeFamily.Text && dtOfTe.Rows[i]["SSN"].ToString() == txtTeSSN.Text)
                        //{
                        //    this.DivReport.Visible = true;
                        //    this.LabelWarning.Text = "سخنران مورد نظر قبلاً وارد شده است";
                        //    return;
                        //}
                    }
                }

                //if (comboType.Value.ToString() == "1")//other
                //{
                //    //if (dtOfTe.Rows.Count == 0)
                //    //    dr["TeId"] = 1;
                //    //else
                //    //    dr["TeId"] = int.Parse(dtOfTe.Rows[dtOfTe.Rows.Count - 1]["Id"].ToString()) + 1;
                //    dr["Type"] = 1;
                //    //dtOfImgTeacher = (DataTable)Session["TblOfImg7"];

                //    dr["Te"] = 0;

                //}
                //else if (comboType.Value.ToString() == "2")//teacher
                //{
                //    dr["TeId"] = txtTeID.Text;
                //    dr["Type"] = 2;

                //}
                dr["TeId"] = txtTeID.Text;
                dr["TypeName"] = txtTypeName.Text;
                dr["Family"] = txtTeFamily.Text;
                dr["Father"] = txtTeFatherName.Text;
                dr["Name"] = txtTeName.Text;
                dr["SSN"] = txtTeSSN.Text;
                dr["Description"] = txtTeDesc.Text;
                dr["Salary"] = txtTeSalary.Text;


                dtOfTe.Rows.Add(dr);
                Grdv_Teacher.DataSource = dtOfTe;
                Grdv_Teacher.DataBind();

                //for (int i = 0; i < ASPxRoundPanel4.Controls.Count; i++)
                //{
                //    if (ASPxRoundPanel4.Controls[i] is DevExpress.Web.ASPxTextBox)
                //    {
                //        DevExpress.Web.ASPxTextBox txt = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel4.Controls[i];
                //        txt.Text = "";
                //    }

                //}
                txtTeDesc.Text = "";
                txtTypeName.Text = "";
                txtTeDesc.Text = "";
                txtTeName.Text = "";
                txtTeFamily.Text = "";
                txtTeSSN.Text = "";
                txtTeSalary.Text = "";
                txtTeFatherName.Text = "";

            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
            }

        }
    }

    protected void btnAddMajor_Click(object sender, EventArgs e)
    {
        TSP.DataManager.DocUpGradePointManager DocUpGradePointManager = new TSP.DataManager.DocUpGradePointManager();
        string GMRId = "";

        if (cmbResponsibility.SelectedItem != null)
            GMRId = cmbResponsibility.SelectedItem.Value.ToString();
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "صلاحیت مورد نظر را انتخاب نمایید";
            return;
        }

        DataTable dtUpGradePoint = DocUpGradePointManager.SelectLastVersionByGMRId(int.Parse(GMRId));
        if (dtUpGradePoint.Rows.Count > 0)
        {
            // string GMRId = dtUpGradePoint.Rows[0]["GMRId"].ToString();
            if (Session["SeGrade"] != null)
            {
                dtOfGrade = (DataTable)Session["SeGrade"];

                DataRow dr = dtOfGrade.NewRow();

                try
                {
                    //if (string.IsNullOrEmpty(txtMajorId.Text))
                    //{
                    //    this.DivReport.Visible = true;
                    //    this.LabelWarning.Text = "پایه مورد نظر را انتخاب نمایید";
                    //    return;
                    //}

                    if (dtOfGrade.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtOfGrade.Rows.Count; i++)
                        {
                            if ((dtOfGrade.Rows[i].RowState != DataRowState.Deleted) && (dtOfGrade.Rows[i]["GMRId"].ToString() == GMRId))// && dtOfGrade.Rows[i]["Type"].ToString()=="1")
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "ترکیب رشته،پایه و صلاحیت انتخاب شده تکراری می باشد";
                                return;
                            }
                        }
                    }

                    dr["UpGrdPId"] = dtUpGradePoint.Rows[dtUpGradePoint.Rows.Count - 1]["UpGrdPId"].ToString();
                    dr["GMRId"] = GMRId;
                    dr["GrdName"] = cmbUpGrade.SelectedItem.Text;
                    dr["ResName"] = cmbResponsibility.SelectedItem.Text;
                    dr["MjName"] = CmbMajor.SelectedItem.Text;
                    dr["Description"] = txtMjDesc.Text;

                    dtOfGrade.Rows.Add(dr);
                    CustomAspxDevGridViewGrade.DataSource = dtOfGrade;
                    CustomAspxDevGridViewGrade.DataBind();

                    //txtGrdName.Text = "";
                    //txtResName.Text = "";
                    //txtMjName.Text = "";
                    txtMjDesc.Text = "";

                }
                catch
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
                }
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "وضعیت انتخاب شده (رشته ، صلاحیت و پایه) برای امتیازدهی تعریف نشده است.";
            return;
        }
    }

    protected void GridMember_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        GridMember.DataSource = ObjectDataSource1;
    }

    protected void Grdv_Teacher_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        Grdv_Teacher.DataSource = (DataTable)Session["SemTeacher"];
        Grdv_Teacher.DataBind();

        int Id = -1;
        if (Grdv_Teacher.FocusedRowIndex > -1)
        {
            Id = Grdv_Teacher.FocusedRowIndex;
        }
        if (Id == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            return;

        }
        else
        {

            dtOfTe = (DataTable)Session["SemTeacher"];
            dtOfTe.Rows[Id].Delete();
            Session["SemTeacher"] = dtOfTe;
            Grdv_Teacher.DataSource = (DataTable)Session["SemTeacher"];
            Grdv_Teacher.DataBind();
            dtOfTe = (DataTable)Session["SemTeacher"];


        }
    }

    protected void AspxGridFlp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        AspxGridFlp.DataSource = (DataTable)Session["TblOfImg6"];
        AspxGridFlp.DataBind();

        int Id = -1;
        if (AspxGridFlp.FocusedRowIndex > -1)
        {
            Id = AspxGridFlp.FocusedRowIndex;
        }
        if (Id == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            return;

        }
        else
        {

            dtOfImg = (DataTable)Session["TblOfImg6"];
            dtOfImg.Rows[Id].Delete();
            Session["TblOfImg6"] = dtOfImg;
            AspxGridFlp.DataSource = (DataTable)Session["TblOfImg6"];
            AspxGridFlp.DataBind();
            dtOfImg = (DataTable)Session["TblOfImg6"];


        }
    }

    protected void flp_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void AspxGridSchedule_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        AspxGridSchedule.DataSource = (DataTable)Session["SeminarSchedule"];
        AspxGridSchedule.DataBind();

        int Id = -1;
        if (AspxGridSchedule.FocusedRowIndex > -1)
        {
            Id = AspxGridSchedule.FocusedRowIndex;
        }
        if (Id == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            return;

        }
        else
        {

            dtOfSchedule = (DataTable)Session["SeminarSchedule"];
            dtOfSchedule.Rows[Id].Delete();
            Session["SeminarSchedule"] = dtOfSchedule;
            AspxGridSchedule.DataSource = (DataTable)Session["SeminarSchedule"];
            AspxGridSchedule.DataBind();
            dtOfSchedule = (DataTable)Session["SeminarSchedule"];

        }
    }

    protected void AspxGridFlpTeacher_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        string TeId = e.Parameters;
        //TSP.DataManager.SeminarTeachersManager SemTeachManager = new TSP.DataManager.SeminarTeachersManager();
        //SemTeachManager.FindBySeminarCode(int.Parse(Utility.DecryptQS(SeminarId.Value)));
        //SemTeachManager.CurrentFilter = "TeId=" + TeId
        //AspxGridFlpTeacher.DataSource = SemTeachManager.DataTable;
        //AspxGridFlpTeacher.DataBind();


        //dtOfImgTeacher = (DataTable)Session["SemTeacher"];
        //dtOfImgTeacher.DefaultView.RowFilter = "TeId=" + TeId;
        //AspxGridFlpTeacher.DataSource = dtOfImgTeacher.DefaultView;
        //AspxGridFlpTeacher.DataBind();


    }

    protected void CustomAspxDevGridViewGrade_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        CustomAspxDevGridViewGrade.DataSource = (DataTable)Session["SeGrade"];
        CustomAspxDevGridViewGrade.DataBind();

        int Id = -1;
        if (CustomAspxDevGridViewGrade.FocusedRowIndex > -1)
        {
            Id = CustomAspxDevGridViewGrade.FocusedRowIndex;
        }
        if (Id == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            return;

        }
        else
        {

            dtOfGrade = (DataTable)Session["SeGrade"];
            //dtOfGrade.Rows[Id].Delete();
            dtOfGrade.Rows.Find(e.Keys[0]).Delete();

            Session["SeGrade"] = dtOfGrade;
            CustomAspxDevGridViewGrade.DataSource = (DataTable)Session["SeGrade"];
            CustomAspxDevGridViewGrade.DataBind();
            dtOfGrade = (DataTable)Session["SeGrade"];

        }
    }

    protected void CmbMajor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmbMajor.SelectedIndex > -1 && cmbUpGrade.SelectedIndex > -1)
        {
            TSP.DataManager.DocAcceptedUpGradeManager DocAcceptedUpGradeManager = new TSP.DataManager.DocAcceptedUpGradeManager();
            CmbMajor.DataBind();
            cmbUpGrade.DataBind();
            int UpGrdId = int.Parse(cmbUpGrade.SelectedItem.Value.ToString());
            string MjId = CmbMajor.SelectedItem.Value.ToString();
            DocAcceptedUpGradeManager.FindByCode(UpGrdId);
            if (DocAcceptedUpGradeManager.Count == 1)
            {
                cmbResponsibility.Text = "";
                string GrdId = DocAcceptedUpGradeManager[0]["OriginGradeId"].ToString();
                ObjdsAcceptGrade.SelectParameters[0].DefaultValue = MjId;
                ObjdsAcceptGrade.SelectParameters[1].DefaultValue = GrdId;
                cmbResponsibility.DataBind();
                cmbResponsibility.SelectedIndex = 0;
            }
        }
    }

    protected void cmbUpGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmbMajor.SelectedIndex > -1 && cmbUpGrade.SelectedIndex > -1)
        {
            TSP.DataManager.DocAcceptedUpGradeManager DocAcceptedUpGradeManager = new TSP.DataManager.DocAcceptedUpGradeManager();
            CmbMajor.DataBind();
            cmbUpGrade.DataBind();
            int UpGrdId = int.Parse(cmbUpGrade.SelectedItem.Value.ToString());
            string MjId = CmbMajor.SelectedItem.Value.ToString();
            DocAcceptedUpGradeManager.FindByCode(UpGrdId);
            if (DocAcceptedUpGradeManager.Count == 1)
            {
                cmbResponsibility.Text = "";
                string GrdId = DocAcceptedUpGradeManager[0]["OriginGradeId"].ToString();
                ObjdsAcceptGrade.SelectParameters[0].DefaultValue = MjId;
                ObjdsAcceptGrade.SelectParameters[1].DefaultValue = GrdId;
                cmbResponsibility.DataBind();
                cmbResponsibility.SelectedIndex = 0;
            }

        }
    }

    protected void AspxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {

        //switch (e.Item.Name)
        //{
        //    case "Costs":
        //        Response.Redirect("SeminarCosts.aspx?SeId=" + Utility.EncryptQS(_SeId.ToString()) + "&InsId=" + Utility.EncryptQS(_InsId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&SeReqId=" + Utility.EncryptQS(_SeReqId.ToString()) + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
        //        break;
        //}
    }
    #endregion

    #region Methods
    private void SetNewMode()
    {
        AspxMenu1.Enabled = false;
        btnSave.Enabled = true;
        btnSave2.Enabled = true;
        SetEnable(true);
        AspxMenu1.Enabled = false;
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        _PageMode = "New";
        _SeId = -1;
        CheckWorkFlowPermission();
    }

    private void SetViewMode()
    {
        if (_SeId == null)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        FillForm(_SeReqId);
        SetEnable(false);
        InsertWorkFlowStateView((int)TSP.DataManager.TableCodes.Seminar, _SeReqId);
        ASPxRoundPanel2.HeaderText = "مشاهده";
    }

    private void SetEditMode()
    {
        if (!CheckPermitionForEdit(_SeReqId))
        {
            SetMessage("تنها در مرحله تقاضای برگزاری سمینار و در صورت داشتن دسترسی گردش کار قادر به ویرایش اطلاعات  می باشید ");
            return;
        }
        if (_SeId == null)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        if (string.IsNullOrEmpty(_PageMode) && _PageMode != "View")
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetEnable(true);
        CmbMajor.SelectedIndex = 0;
        CmbMajor_SelectedIndexChanged(this, new EventArgs());
        FillForm(_SeReqId);
        InsertWorkFlowStateView((int)TSP.DataManager.TableCodes.Seminar, _SeReqId);
        _PageMode = "Edit";
        ASPxRoundPanel2.HeaderText = "ویرایش";
        btnSave.Enabled = btnSave2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;

    }

    private void SetChangeMode()
    {
        if (_SeReqId == null)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        if (string.IsNullOrEmpty(_PageMode) && _PageMode != "View")
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetEnable(true);
        CmbMajor.SelectedIndex = 0;
        CmbMajor_SelectedIndexChanged(this, new EventArgs());
        FillForm(_SeReqId);
        _PageMode = "Change";
        ASPxRoundPanel2.HeaderText = "ثبت درخواست تغییرات سمینار";
        btnSave.Enabled = btnSave2.Enabled = true;
        this.ViewState["BtnSave"] = btnSave.Enabled;

    }

    protected void SetEnable(Boolean Enable)
    {
        AspxGridSchedule.Columns["Delete"].Visible =
        AspxGridFlp.Columns["Delete"].Visible =
        CustomAspxDevGridViewGrade.Columns["Delete"].Visible =
        Grdv_Teacher.Columns["DeleteTeacher"].Visible =
        tblSchedule.Visible =
        tblFile.Visible =
        TableGrade.Visible =
        txtDate.Enabled =
        txtEndDate.Enabled =
        cmbResponsibility.Enabled =
        cmbUpGrade.Enabled =
        CmbMajor.Enabled =
        txtStartRegister.Enabled =
        txtEndRegister.Enabled =
        tblTe.Visible =
        RoundPanelBaseInfo.Enabled = Enable;

    }

    protected void ClearForm()
    {
        txtSubject.Text =
        txtDate.Text =
        txtTime.Text =
        txtDuration.Text =
        txtCapacity.Text =
        txtSeminarCost.Text =
        txtTopic.Text =
        txtDesc.Text =
        txtPlace.Text =
        txtEndDate.Text =
        txtTeDesc.Text =
        txtTeFamily.Text =
        txtTeFatherName.Text =
        txtTeID.Text =
        txtTeName.Text =
        txtTeSalary.Text =
        txtTeSSN.Text =
        txtEndRegister.Text =
        txtStartRegister.Text =
        txtTypeName.Text = "";

        dtOfImg = (DataTable)Session["TblOfImg6"];
        dtOfImg.Rows.Clear();
        AspxGridFlp.DataSource = dtOfImg;
        AspxGridFlp.DataBind();

        dtOfTe = (DataTable)Session["SemTeacher"];
        dtOfTe.Rows.Clear();
        Grdv_Teacher.DataSource = dtOfTe;
        Grdv_Teacher.DataBind();

        dtOfSchedule = (DataTable)Session["SeminarSchedule"];
        dtOfSchedule.Rows.Clear();
        AspxGridSchedule.DataSource = dtOfSchedule;
        AspxGridSchedule.DataBind();

        dtOfGrade = (DataTable)Session["SeGrade"];
        dtOfGrade.Rows.Clear();
        CustomAspxDevGridViewGrade.DataSource = dtOfGrade;
        CustomAspxDevGridViewGrade.DataBind();

        CmbMajor.SelectedIndex = 0;
        CmbMajor_SelectedIndexChanged(this, new EventArgs());
        cmbInstitue.SelectedIndex = -1;
    }

    protected void FillForm(int SeReqId)
    {
        TSP.DataManager.SeminarRequestManager SeManager = new TSP.DataManager.SeminarRequestManager();
        TSP.DataManager.TrainingTeachersManager SemTeachManager = new TSP.DataManager.TrainingTeachersManager();

        try
        {
            SeManager.FindByCode(SeReqId);
            if (SeManager.Count == 1)
            {
                if (!Utility.IsDBNullOrNullValue(SeManager[0]["StartDate"]))
                    txtDate.Text = SeManager[0]["StartDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(SeManager[0]["EndDate"]))
                    txtEndDate.Text = SeManager[0]["EndDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(SeManager[0]["Description"]))
                    txtDesc.Text = SeManager[0]["Description"].ToString();
                if (!Utility.IsDBNullOrNullValue(SeManager[0]["Place"]))
                    txtPlace.Text = SeManager[0]["Place"].ToString();
                if (!Utility.IsDBNullOrNullValue(SeManager[0]["Subject"]))
                    txtSubject.Text = SeManager[0]["Subject"].ToString();
                if (!Utility.IsDBNullOrNullValue(SeManager[0]["Time"]))
                    txtTime.Text = SeManager[0]["Time"].ToString();
                if (!Utility.IsDBNullOrNullValue(SeManager[0]["Duration"]))
                    txtDuration.Text = SeManager[0]["Duration"].ToString().Trim();
                if (!Utility.IsDBNullOrNullValue(SeManager[0]["Topic"]))
                    txtTopic.Text = SeManager[0]["Topic"].ToString();
                if (!Utility.IsDBNullOrNullValue(SeManager[0]["Capacity"]))
                    txtCapacity.Text = SeManager[0]["Capacity"].ToString();
                if (!Utility.IsDBNullOrNullValue(SeManager[0]["StartRegisterDate"]))
                    txtStartRegister.Text = SeManager[0]["StartRegisterDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(SeManager[0]["EndRegisterDate"]))
                    txtEndRegister.Text = SeManager[0]["EndRegisterDate"].ToString();
                if (!Utility.IsDBNullOrNullValue(SeManager[0]["SeminarCost"]))
                {
                    decimal SeminarCost = Convert.ToDecimal(SeManager[0]["SeminarCost"].ToString());
                    txtSeminarCost.Text = SeminarCost.ToString("#,#");
                }
                if (Utility.IsDBNullOrNullValue(txtSeminarCost.Text))
                    txtSeminarCost.Text = "0";
                if (!Utility.IsDBNullOrNullValue(SeManager[0]["InsId"]))
                {
                    cmbInstitue.DataBind();
                    cmbInstitue.SelectedIndex = cmbInstitue.Items.FindByValue(SeManager[0]["InsId"].ToString()).Index;
                }
                #region Teachers
                //if (Session["SemTeacher"] == null || Session["SemTeacher"]=="")
                //{
                SemTeachManager.FindByPeriodRequestId(_SeId, _SeReqId, 1);
                for (int i = 0; i < SemTeachManager.Count; i++)
                {
                    DataRow dr = dtOfTe.NewRow();
                    dr["TeId"] = SemTeachManager[i]["TeId"];
                    dr["Id"] = SemTeachManager[i]["TrTeId"];

                    if (!Utility.IsDBNullOrNullValue(SemTeachManager[i]["Name"]))
                        dr["Name"] = SemTeachManager[i]["Name"].ToString();
                    if (!Utility.IsDBNullOrNullValue(SemTeachManager[i]["Family"]))
                        dr["Family"] = SemTeachManager[i]["Family"].ToString();
                    if (!Utility.IsDBNullOrNullValue(SemTeachManager[i]["Father"]))
                        dr["Father"] = SemTeachManager[i]["Father"].ToString();
                    if (!Utility.IsDBNullOrNullValue(SemTeachManager[i]["PracticalSalary"]))
                        dr["Salary"] = SemTeachManager[i]["PracticalSalary"].ToString();
                    if (!Utility.IsDBNullOrNullValue(SemTeachManager[i]["Description"]))
                        dr["Description"] = SemTeachManager[i]["Description"].ToString();
                    if (!Utility.IsDBNullOrNullValue(SemTeachManager[i]["TypeName"]))
                        dr["TypeName"] = SemTeachManager[i]["TypeName"].ToString();

                    dtOfTe.Rows.Add(dr);

                }
                dtOfTe.AcceptChanges();
                Session["SemTeacher"] = dtOfTe;
                //}
                Grdv_Teacher.DataSource = (DataTable)Session["SemTeacher"];
                Grdv_Teacher.DataBind();

                #endregion

                #region AttachmentFile


                if (Session["TblOfImg6"] == null)
                {
                    TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
                    attachManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.Seminar, _SeId, 2);

                    for (int i = 0; i < attachManager.Count; i++)
                    {
                        DataRow dr = dtOfImg.NewRow();

                        dr[0] = attachManager[i]["AtContent"];
                        if (!Utility.IsDBNullOrNullValue(attachManager[i]["FilePath"]))
                            dr[1] = attachManager[i]["FilePath"].ToString();
                        if (!Utility.IsDBNullOrNullValue(attachManager[i]["FilePath"]))
                            dr[2] = attachManager[i]["FilePath"].ToString();
                        if (!Utility.IsDBNullOrNullValue(attachManager[i]["Description"]))
                            dr[6] = attachManager[i]["Description"].ToString();

                        if (!Utility.IsDBNullOrNullValue(attachManager[i]["FilePath"]))
                        {
                            string fileName = Path.GetFileName(attachManager[i]["FilePath"].ToString());
                            dr[3] = fileName;
                        }
                        dr[4] = 1;
                        dr[5] = attachManager[i][0];
                        dtOfImg.Rows.Add(dr);

                    }
                    dtOfImg.AcceptChanges();
                    Session["TblOfImg6"] = dtOfImg;
                }

                AspxGridFlp.DataSource = (DataTable)Session["TblOfImg6"];
                AspxGridFlp.DataBind();
                #endregion

                #region Schedule

                //if (Session["SeminarSchedule"] == null)
                //{
                TSP.DataManager.ScheduleManager ScheduleManager = new TSP.DataManager.ScheduleManager();
                //ScheduleManager.FindByTtIdTableType(_SeId, (int)TSP.DataManager.TableCodes.Seminar);
                ScheduleManager.FindBySminarRequest(_SeId, _SeReqId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.Seminar));
                for (int i = 0; i < ScheduleManager.Count; i++)
                {
                    DataRow dr = dtOfSchedule.NewRow();
                    dr["TimeFrom"] = ScheduleManager[i]["StartTime"];
                    if (!Utility.IsDBNullOrNullValue(ScheduleManager[i]["EndTime"]))
                        dr["TimeTo"] = ScheduleManager[i]["EndTime"].ToString();
                    if (!Utility.IsDBNullOrNullValue(ScheduleManager[i]["Subject"]))
                        dr["Subject"] = ScheduleManager[i]["Subject"].ToString();
                    if (!Utility.IsDBNullOrNullValue(ScheduleManager[i]["Description"]))
                        dr["Description"] = ScheduleManager[i]["Description"].ToString();
                    dr["Date"] = ScheduleManager[i]["Date"];
                    dr["Id"] = ScheduleManager[i][0];
                    dtOfSchedule.Rows.Add(dr);
                }
                dtOfSchedule.AcceptChanges();
                Session["SeminarSchedule"] = dtOfSchedule;
                //}
                AspxGridSchedule.DataSource = (DataTable)Session["SeminarSchedule"];
                AspxGridSchedule.DataBind();
                #endregion

                #region Grade

                if (Session["SeGrade"] == null)
                {
                    TSP.DataManager.TrainingAcceptedGradeManager GradeManager = new TSP.DataManager.TrainingAcceptedGradeManager();
                    GradeManager.FindByPKCode(_SeId, 1);
                    for (int i = 0; i < GradeManager.Count; i++)
                    {
                        DataRow dr = dtOfGrade.NewRow();

                        dr["UpGrdPId"] = GradeManager[i]["UpGrdPId"];
                        if (!Utility.IsDBNullOrNullValue(GradeManager[i]["GrdName"]))
                            dr["GrdName"] = GradeManager[i]["GrdName"].ToString();
                        if (!Utility.IsDBNullOrNullValue(GradeManager[i]["ResName"]))
                            dr["ResName"] = GradeManager[i]["ResName"].ToString();
                        if (!Utility.IsDBNullOrNullValue(GradeManager[i]["MjName"]))
                            dr["MjName"] = GradeManager[i]["MjName"].ToString();
                        if (!Utility.IsDBNullOrNullValue(GradeManager[i]["Description"]))
                            dr["Description"] = GradeManager[i]["Description"].ToString();
                        dr[6] = GradeManager[i][0];

                        dtOfGrade.Rows.Add(dr);
                    }
                    dtOfGrade.AcceptChanges();
                    Session["SeGrade"] = dtOfGrade;
                }

                CustomAspxDevGridViewGrade.DataSource = (DataTable)Session["SeGrade"];
                CustomAspxDevGridViewGrade.DataBind();
                #endregion
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
    }

    protected void Insert()
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.SeminarManager SeManager = new TSP.DataManager.SeminarManager();
        TSP.DataManager.SeminarRequestManager SeminarRequestManager = new TSP.DataManager.SeminarRequestManager();
        TSP.DataManager.TeacherManager TeManager = new TSP.DataManager.TeacherManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.ScheduleManager ScheduleManager = new TSP.DataManager.ScheduleManager();
        TSP.DataManager.TrainingTeachersManager SemTeachManager = new TSP.DataManager.TrainingTeachersManager();
        TSP.DataManager.TrainingAcceptedGradeManager GradeManager = new TSP.DataManager.TrainingAcceptedGradeManager();

        trans.Add(SeManager);
        trans.Add(SeminarRequestManager);
        trans.Add(TeManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(attachManager);
        trans.Add(ScheduleManager);
        trans.Add(SemTeachManager);
        trans.Add(GradeManager);


        try
        {


            string StartDate = txtDate.Text;
            string EndDate = txtEndDate.Text;
            int IsDateCurrect = string.Compare(EndDate, StartDate);
            if (IsDateCurrect < 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "تاریخ پایان برگزاری را صحیح وارد نمایید.";
                return;
            }
            IsDateCurrect = string.Compare(txtEndRegister.Text, txtStartRegister.Text);
            if (IsDateCurrect < 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "تاریخ پایان ثبت نام را صحیح وارد نمایید.";
                return;
            }
            trans.BeginSave();
            DataRow drP = SeManager.NewRow();
            drP["InsId"] = cmbInstitue.SelectedItem.Value;
            drP["Subject"] = txtSubject.Text;
            drP["Status"] = 0;
            drP["StartDate"] = txtDate.Text;
            drP["EndDate"] = txtEndDate.Text;
            drP["Time"] = txtTime.Text;
            drP["Duration"] = txtDuration.Text;
            drP["Place"] = txtPlace.Text;
            drP["Topic"] = txtTopic.Text;
            drP["Description"] = txtDesc.Text;
            drP["SeminarCost"] = txtSeminarCost.Text;
            drP["StartRegisterDate"] = txtStartRegister.Text;
            drP["EndRegisterDate"] = txtEndRegister.Text;
            drP["CreateDate"] = Utility.GetDateOfToday();
            drP["Capacity"] = txtCapacity.Text;
            // drP["TeacherSalary"] = txtTeacherCost.Text;
            drP["InActive"] = 0;
            drP["UserId"] = Utility.GetCurrentUser_UserId();
            drP["ModifiedDate"] = DateTime.Now;

            SeManager.AddRow(drP);
            int cnt = SeManager.Save();
            SeManager.DataTable.AcceptChanges();

            if (cnt <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }

            int SemId = int.Parse(SeManager[0]["SeId"].ToString());

            DataRow drReq = SeminarRequestManager.NewRow();
            drReq["SeId"] = SemId;
            drReq["InsId"] = cmbInstitue.SelectedItem.Value;
            drReq["Subject"] = txtSubject.Text;
            drReq["Type"] = (int)TSP.DataManager.SeminarRequestType.SaveInfo;
            drReq["StartDate"] = txtDate.Text;
            drReq["EndDate"] = txtEndDate.Text;
            drReq["Time"] = txtTime.Text;
            drReq["Duration"] = txtDuration.Text;
            drReq["Place"] = txtPlace.Text;
            drReq["Topic"] = txtTopic.Text;
            drReq["Description"] = txtDesc.Text;
            drReq["SeminarCost"] = txtSeminarCost.Text;
            drReq["StartRegisterDate"] = txtStartRegister.Text;
            drReq["EndRegisterDate"] = txtEndRegister.Text;
            drReq["CreateDate"] = Utility.GetDateOfToday();
            drReq["Capacity"] = txtCapacity.Text;
            drReq["InActive"] = 0;
            drReq["IsConfirm"] = 0;
            drReq["UserId"] = Utility.GetCurrentUser_UserId();
            drReq["ModifiedDate"] = DateTime.Now;

            SeminarRequestManager.AddRow(drReq);

            if (SeminarRequestManager.Save() <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            SeminarRequestManager.DataTable.AcceptChanges();
            int SeReqId = int.Parse(SeminarRequestManager[0]["SeReqId"].ToString());

            #region SeminarTeacher
            dtOfTe = (DataTable)Session["SemTeacher"];
            for (int i = 0; i < dtOfTe.DefaultView.Count; i++)
            {
                if (dtOfTe.Rows[i]["TypeName"].ToString() == "استاد")
                {
                    int TeacherId = int.Parse(dtOfTe.Rows[i]["TeId"].ToString());
                    TeManager.FindByCode(TeacherId);
                    if (TeManager.Count == 1)
                    {
                        TeManager[0].BeginEdit();
                        TeManager[0]["Type"] = 2;//Teacher-Lecturer
                        TeManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        TeManager[0]["ModifiedDate"] = DateTime.Now;
                        TeManager[0].EndEdit();
                        TeManager.Save();
                    }
                }
                DataRow drSm = SemTeachManager.NewRow();
                drSm["PkId"] = SemId;
                drSm["PPRId"] = SeReqId;
                drSm["TeId"] = dtOfTe.Rows[i]["TeId"].ToString();
                drSm["PracticalSalary"] = dtOfTe.Rows[i]["Salary"].ToString();

                drSm["PracticalHour"] = DBNull.Value;
                drSm["NonPracticalHour"] = DBNull.Value;
                drSm["WorkroomHour"] = DBNull.Value;
                drSm["NonPracticalSalary"] = DBNull.Value;
                drSm["WorkroomSalary"] = DBNull.Value;
                drSm["PollId"] = -1;

                drSm["Description"] = dtOfTe.Rows[i]["Description"].ToString();
                drSm["Type"] = 1;//seminar
                drSm["InActive"] = 0;
                drSm["UserId"] = Utility.GetCurrentUser_UserId();
                drSm["ModifiedDate"] = DateTime.Now;
                SemTeachManager.AddRow(drSm);
                SemTeachManager.Save();
                SemTeachManager.DataTable.AcceptChanges();




             



            }

            #endregion

            #region WorkFlow
            ////*****Check is User In TaskDoer*****
            // int TableId = int.Parse(SeManager[0]["SeId"].ToString());

            TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveSeminarInfo;
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                TaskDoerManager.FindByTaskId(TaskId);
                if (TaskDoerManager.Count > 0)
                {
                    TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
                    int NmcId = NezamChartManager.FindNmcId(Utility.GetCurrentUser_UserId(), LoginManager);
                    if (NmcId > -1)
                    {
                        int StartWorkFlow = WorkFlowStateManager.StartWorkFlow(SeReqId, TaskCode, NmcId, Utility.GetCurrentUser_UserId());
                        if (StartWorkFlow < 0)
                        {
                            trans.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                        }
                    }
                }
            }


            #endregion

            #region AttachmentFile
            dtOfImg = (DataTable)Session["TblOfImg6"];
            //int EpId = int.Parse(ExPManager[0]["EpId"].ToString());
            if (dtOfImg.DefaultView.Count > 0)
            {
                for (int i = 0; i < dtOfImg.DefaultView.Count; i++)
                {
                    DataRow drImg = attachManager.NewRow();
                    drImg["TtId"] = (int)TSP.DataManager.TableCodes.Seminar;
                    drImg["RefTable"] = SemId;// TableId;//
                    drImg["AttId"] = 2;
                    drImg["FilePath"] = dtOfImg.Rows[i]["ImgUrl"].ToString();
                    drImg["IsValid"] = 1;
                    drImg["Description"] = dtOfImg.Rows[i]["Description"].ToString();
                    drImg["UserId"] = Utility.GetCurrentUser_UserId();
                    drImg["ModfiedDate"] = DateTime.Now;
                    attachManager.AddRow(drImg);
                    int imgcnt = attachManager.Save();
                    attachManager.DataTable.AcceptChanges();
                    if (imgcnt == 1)
                    {
                        dtOfImg.Rows[i].BeginEdit();
                        dtOfImg.Rows[i]["Code"] = attachManager[attachManager.Count - 1]["AttachId"].ToString();
                        dtOfImg.Rows[i].EndEdit();

                        if (!string.IsNullOrEmpty(dtOfImg.Rows[i]["ImgUrl"].ToString()))
                        {

                            string ImgSoource = Server.MapPath("~/image/Temp/") + dtOfImg.Rows[i]["fileName"].ToString();
                            string ImgTarget = Server.MapPath(dtOfImg.Rows[i]["ImgUrl"].ToString());
                            File.Copy(ImgSoource, ImgTarget, true);
                            // grdv_Img.Columns[1].Visible = true;
                        }


                    }
                }

            }
            #endregion

            #region Schedule

            dtOfSchedule = (DataTable)Session["SeminarSchedule"];

            if (dtOfSchedule.DefaultView.Count > 0)
            {
                for (int i = 0; i < dtOfSchedule.DefaultView.Count; i++)
                {
                    DataRow drSchedule = ScheduleManager.NewRow();
                    drSchedule["TtId"] = SemId;
                    drSchedule["PPRId"] = SeReqId;
                    drSchedule["TableType"] = (int)TSP.DataManager.TableCodes.Seminar;
                    drSchedule["StartTime"] = dtOfSchedule.Rows[i]["TimeFrom"].ToString();
                    drSchedule["EndTime"] = dtOfSchedule.Rows[i]["TimeTo"].ToString();
                    drSchedule["Date"] = dtOfSchedule.Rows[i]["Date"].ToString();
                    drSchedule["Subject"] = dtOfSchedule.Rows[i]["Subject"].ToString();
                    drSchedule["Description"] = dtOfSchedule.Rows[i]["Description"].ToString();
                    drSchedule["UserId"] = Utility.GetCurrentUser_UserId();
                    drSchedule["Type"] = 1;//seminar
                    drSchedule["ModifiedDate"] = DateTime.Now;
                    ScheduleManager.AddRow(drSchedule);

                    ScheduleManager.Save();
                    ScheduleManager.DataTable.AcceptChanges();

                }

            }
            #endregion

            #region Grades

            dtOfGrade = (DataTable)Session["SeGrade"];

            if (dtOfGrade.DefaultView.Count > 0)
            {
                for (int i = 0; i < dtOfGrade.DefaultView.Count; i++)
                {
                    DataRow drGrade = GradeManager.NewRow();
                    drGrade["UpGrdPId"] = dtOfGrade.Rows[i]["UpGrdPId"].ToString();
                    drGrade["PkId"] = SemId;
                    drGrade["Type"] = 1;//Seminar
                    drGrade["Description"] = dtOfGrade.Rows[i]["Description"].ToString();
                    drGrade["CreateDate"] = Utility.GetDateOfToday();
                    drGrade["UserId"] = Utility.GetCurrentUser_UserId();
                    drGrade["ModifiedDate"] = DateTime.Now;
                    GradeManager.AddRow(drGrade);

                    //GradeManager.DataTable.AcceptChanges();

                }

                GradeManager.Save();
            }
            #endregion

            trans.EndSave();
            _SeId = Convert.ToInt32(SeManager[0]["SeId"]);
            _SeReqId = SeReqId;
            _PageMode = "Edit";
            ASPxRoundPanel2.HeaderText = "ویرایش";
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";
            Session["IsEdited_Seminar"] = true;
            AspxMenu1.Enabled = true;
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
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
    }

    private void InsertRequest()
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.SeminarRequestManager SeminarRequestManager = new TSP.DataManager.SeminarRequestManager();
        TSP.DataManager.ScheduleManager ScheduleManager = new TSP.DataManager.ScheduleManager();
        TSP.DataManager.TrainingTeachersManager SemTeachManager = new TSP.DataManager.TrainingTeachersManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.TrainingAcceptedGradeManager GradeManager = new TSP.DataManager.TrainingAcceptedGradeManager();

        trans.Add(SeminarRequestManager);
        trans.Add(ScheduleManager);
        trans.Add(SemTeachManager);
        trans.Add(attachManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(GradeManager);

        try
        {
            trans.BeginSave();
            DataRow drReq = SeminarRequestManager.NewRow();
            drReq["SeId"] = _SeId;
            drReq["InsId"] = cmbInstitue.SelectedItem.Value;
            drReq["Subject"] = txtSubject.Text;
            drReq["Type"] = (int)TSP.DataManager.SeminarRequestType.Change;
            drReq["StartDate"] = txtDate.Text;
            drReq["EndDate"] = txtEndDate.Text;
            drReq["Time"] = txtTime.Text;
            drReq["Duration"] = txtDuration.Text;
            drReq["Place"] = txtPlace.Text;
            drReq["Topic"] = txtTopic.Text;
            drReq["Description"] = txtDesc.Text;
            drReq["SeminarCost"] = txtSeminarCost.Text;
            drReq["StartRegisterDate"] = txtStartRegister.Text;
            drReq["EndRegisterDate"] = txtEndRegister.Text;
            drReq["CreateDate"] = Utility.GetDateOfToday();
            drReq["Capacity"] = txtCapacity.Text;
            drReq["InActive"] = 0;
            drReq["IsConfirm"] = 0;
            drReq["UserId"] = Utility.GetCurrentUser_UserId();
            drReq["ModifiedDate"] = DateTime.Now;

            SeminarRequestManager.AddRow(drReq);

            if (SeminarRequestManager.Save() <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            SeminarRequestManager.DataTable.AcceptChanges();
            int SeReqId = int.Parse(SeminarRequestManager[0]["SeReqId"].ToString());
            #region WF
            ////*****Check is User In TaskDoer*****
            // int TableId = int.Parse(SeManager[0]["SeId"].ToString());

            TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveSeminarInfo;
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count > 0)
            {
                int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                TaskDoerManager.FindByTaskId(TaskId);
                if (TaskDoerManager.Count > 0)
                {
                    TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
                    int NmcId = NezamChartManager.FindNmcId(Utility.GetCurrentUser_UserId(), LoginManager);
                    if (NmcId > -1)
                    {
                        int StartWorkFlow = WorkFlowStateManager.StartWorkFlow(SeReqId, TaskCode, NmcId, Utility.GetCurrentUser_UserId());
                        if (StartWorkFlow < 0)
                        {
                            trans.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                        }
                    }
                }
            }
            #endregion
            #region SeminarTeacher

            dtOfTe = (DataTable)Session["SemTeacher"];
            if (dtOfTe.GetChanges() != null)
            {
                DataRow[] insRows = dtOfTe.Select(null, null, DataViewRowState.Added);
                DataRow[] delRows = dtOfTe.Select(null, null, DataViewRowState.Deleted);
                if (delRows.Length > 0)
                {
                    for (int i = 0; i < delRows.Length; i++)
                    {
                        SemTeachManager.FindByCode(int.Parse(delRows[i]["Id", DataRowVersion.Original].ToString()));
                        SemTeachManager[0].Delete();
                        SemTeachManager.Save();
                        SemTeachManager.DataTable.AcceptChanges();
                    }
                }

                if (insRows.Length > 0)
                {
                    for (int i = 0; i < insRows.Length; i++)
                    {
                        DataRow drSm = SemTeachManager.NewRow();
                        drSm["PkId"] = _SeId;
                        drSm["PPRId"] = SeReqId;
                        drSm["TeId"] = insRows[i]["TeId"].ToString();
                        drSm["PracticalSalary"] = insRows[i]["Salary"].ToString();

                        drSm["PracticalHour"] = DBNull.Value;
                        drSm["NonPracticalHour"] = DBNull.Value;
                        drSm["WorkroomHour"] = DBNull.Value;
                        drSm["NonPracticalSalary"] = DBNull.Value;
                        drSm["WorkroomSalary"] = DBNull.Value;

                        drSm["Description"] = insRows[i]["Description"].ToString();
                        drSm["Type"] = 1;
                        drSm["UserId"] = Utility.GetCurrentUser_UserId();
                        drSm["ModifiedDate"] = DateTime.Now;

                        SemTeachManager.AddRow(drSm);

                        SemTeachManager.Save();
                        insRows[i].BeginEdit();
                        insRows[i]["SeTeId"] = SemTeachManager[SemTeachManager.Count - 1]["TrTeId"];
                        insRows[i].EndEdit();

                        SemTeachManager.DataTable.AcceptChanges();

                    }

                }

            }
            #endregion

            #region AttachmentFile
            dtOfImg = (DataTable)Session["TblOfImg6"];

            if (dtOfImg.GetChanges() != null)
            {

                DataRow[] delRows = dtOfImg.Select(null, null, DataViewRowState.Deleted);
                DataRow[] insRows = dtOfImg.Select(null, null, DataViewRowState.Added);

                if (delRows.Length > 0)
                {
                    for (int i = 0; i < delRows.Length; i++)
                    {
                        attachManager.FindByCode(int.Parse(delRows[i]["Code", DataRowVersion.Original].ToString()));
                        attachManager[0].Delete();

                        attachManager.Save();
                        attachManager.DataTable.AcceptChanges();

                    }
                }


                if (insRows.Length > 0)
                {
                    for (int i = 0; i < insRows.Length; i++)
                    {
                        DataRow drImg = attachManager.NewRow();
                        drImg["TtId"] = (int)TSP.DataManager.TableCodes.Seminar;
                        drImg["RefTable"] = _SeId;
                        drImg["AttId"] = 2;
                        drImg["FilePath"] = insRows[i]["ImgUrl"].ToString();
                        drImg["IsValid"] = 1;
                        drImg["Description"] = insRows[i]["Description"].ToString();
                        drImg["UserId"] = Utility.GetCurrentUser_UserId();
                        drImg["ModfiedDate"] = DateTime.Now;

                        attachManager.AddRow(drImg);

                        attachManager.Save();
                        //insRows[i].BeginEdit();
                        //insRows[i]["PPTeId"] = attachManager[attachManager.Count - 1]["PPTeId"];
                        //insRows[i].EndEdit();

                        if (!string.IsNullOrEmpty(insRows[i]["ImgUrl"].ToString()))
                        {

                            string ImgSoource = Server.MapPath("~/image/Temp/") + insRows[i]["fileName"].ToString();
                            string ImgTarget = Server.MapPath(insRows[i]["ImgUrl"].ToString());
                            File.Copy(ImgSoource, ImgTarget, true);
                        }

                        attachManager.DataTable.AcceptChanges();

                    }
                }
            }


            #endregion

            #region Schedule

            dtOfSchedule = (DataTable)Session["SeminarSchedule"];

            if (dtOfSchedule.GetChanges() != null)
            {

                DataRow[] delRows = dtOfSchedule.Select(null, null, DataViewRowState.Deleted);
                DataRow[] insRows = dtOfSchedule.Select(null, null, DataViewRowState.Added);

                if (delRows.Length > 0)
                {
                    for (int i = 0; i < delRows.Length; i++)
                    {
                        ScheduleManager.FindByCode(int.Parse(delRows[i]["Id", DataRowVersion.Original].ToString()));
                        ScheduleManager[0].Delete();

                        ScheduleManager.Save();
                        ScheduleManager.DataTable.AcceptChanges();

                    }
                }


                if (insRows.Length > 0)
                {
                    for (int i = 0; i < insRows.Length; i++)
                    {
                        DataRow drSchedule = ScheduleManager.NewRow();
                        drSchedule["TtId"] = _SeId;
                        drSchedule["PPRId"] = SeReqId;
                        drSchedule["TableType"] = (int)TSP.DataManager.TableCodes.Seminar;
                        drSchedule["StartTime"] = insRows[i]["TimeFrom"].ToString();
                        drSchedule["EndTime"] = insRows[i]["TimeTo"].ToString();
                        drSchedule["Date"] = insRows[i]["Date"].ToString();
                        drSchedule["Subject"] = insRows[i]["Subject"].ToString();
                        drSchedule["Description"] = insRows[i]["Description"].ToString();
                        drSchedule["UserId"] = Utility.GetCurrentUser_UserId();
                        drSchedule["Type"] = 1;//seminar
                        drSchedule["UserId"] = Utility.GetCurrentUser_UserId();
                        drSchedule["ModifiedDate"] = DateTime.Now;
                        //
                        ScheduleManager.AddRow(drSchedule);

                        ScheduleManager.Save();

                        ScheduleManager.DataTable.AcceptChanges();

                    }

                }

                // dtOfCost.AcceptChanges();
            }


            #endregion

            #region Grade

            dtOfGrade = (DataTable)Session["SeGrade"];

            if (dtOfGrade.GetChanges() != null)
            {

                DataRow[] delRows = dtOfGrade.Select(null, null, DataViewRowState.Deleted);
                DataRow[] insRows = dtOfGrade.Select(null, null, DataViewRowState.Added);

                if (delRows.Length > 0)
                {
                    for (int i = 0; i < delRows.Length; i++)
                    {
                        GradeManager.FindByCode(int.Parse(delRows[i]["Id", DataRowVersion.Original].ToString()));
                        GradeManager[0].Delete();

                        GradeManager.Save();
                        GradeManager.DataTable.AcceptChanges();

                    }
                }


                if (insRows.Length > 0)
                {
                    for (int i = 0; i < insRows.Length; i++)
                    {
                        DataRow drGrade = GradeManager.NewRow();
                        drGrade["UpGrdPId"] = insRows[i]["UpGrdPId"].ToString();
                        drGrade["PkId"] = _SeId;
                        drGrade["Type"] = 1;//Seminar
                        drGrade["Description"] = insRows[i]["Description"].ToString();
                        drGrade["CreateDate"] = Utility.GetDateOfToday();
                        drGrade["UserId"] = Utility.GetCurrentUser_UserId();
                        drGrade["ModifiedDate"] = DateTime.Now;

                        GradeManager.AddRow(drGrade);

                        //insRows[i].BeginEdit();
                        //insRows[i]["PPTeId"] = attachManager[attachManager.Count - 1]["PPTeId"];
                        //insRows[i].EndEdit();

                        //ScheduleManager.DataTable.AcceptChanges();

                    }
                    GradeManager.Save();


                }

                // dtOfCost.AcceptChanges();
            }


            #endregion

            trans.EndSave();
            ASPxRoundPanel2.HeaderText = "ویرایش";
            _PageMode = "Edit";
            _SeReqId = SeReqId;
            Session["IsEdited_Seminar"] = true;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";



        }
        catch (Exception err)
        {
            trans.CancelSave();

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
                    this.LabelWarning.Text = "کد سمینار تکراری می باشد";
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
    }

    protected void Edit(int SeReqId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.SeminarRequestManager SeminarRequestManager = new TSP.DataManager.SeminarRequestManager();
        TSP.DataManager.ScheduleManager ScheduleManager = new TSP.DataManager.ScheduleManager();
        TSP.DataManager.TrainingTeachersManager SemTeachManager = new TSP.DataManager.TrainingTeachersManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.TrainingAcceptedGradeManager GradeManager = new TSP.DataManager.TrainingAcceptedGradeManager();

        trans.Add(SeminarRequestManager);
        trans.Add(ScheduleManager);
        trans.Add(SemTeachManager);
        trans.Add(attachManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(GradeManager);

        try
        {
            trans.BeginSave();
            SeminarRequestManager.FindByCode(SeReqId);
            int SeId = Convert.ToInt32(SeminarRequestManager[0]["SeId"]);
            if (SeminarRequestManager.Count != 1)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ویرایش اطلاعات رخ داده است";
                return;
            }
            SeminarRequestManager[0].BeginEdit();
            SeminarRequestManager[0]["InsId"] = cmbInstitue.SelectedItem.Value;
            SeminarRequestManager[0]["Subject"] = txtSubject.Text;
            SeminarRequestManager[0]["StartDate"] = txtDate.Text;
            SeminarRequestManager[0]["EndDate"] = txtEndDate.Text;
            SeminarRequestManager[0]["Capacity"] = txtCapacity.Text;
            SeminarRequestManager[0]["Time"] = txtTime.Text;
            SeminarRequestManager[0]["Duration"] = txtDuration.Text;
            SeminarRequestManager[0]["Place"] = txtPlace.Text;
            SeminarRequestManager[0]["Description"] = txtDesc.Text;
            SeminarRequestManager[0]["SeminarCost"] = txtSeminarCost.Text;
            SeminarRequestManager[0]["StartRegisterDate"] = txtStartRegister.Text;
            SeminarRequestManager[0]["EndRegisterDate"] = txtEndRegister.Text;
            SeminarRequestManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            SeminarRequestManager[0]["ModifiedDate"] = DateTime.Now;

            SeminarRequestManager[0].EndEdit();
            if (SeminarRequestManager.Save() <= 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
                return;
            }
            int UpdateState = -1;
            if (!(Convert.ToBoolean(Session["IsEdited_Seminar"].ToString())))
            {
                int TableType = (int)TSP.DataManager.TableCodes.Seminar;
                UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, SeReqId, TableType, "ویرایش اطلاعات سمینار توسط موسسه", Utility.GetCurrentUser_UserId());
            }
            if (UpdateState == -4)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                return;
            }

            #region SeminarTeacher

            dtOfTe = (DataTable)Session["SemTeacher"];
            if (dtOfTe.GetChanges() != null)
            {
                DataRow[] insRows = dtOfTe.Select(null, null, DataViewRowState.Added);
                DataRow[] delRows = dtOfTe.Select(null, null, DataViewRowState.Deleted);
                if (delRows.Length > 0)
                {
                    for (int i = 0; i < delRows.Length; i++)
                    {
                        SemTeachManager.FindByCode(int.Parse(delRows[i]["Id", DataRowVersion.Original].ToString()));
                        SemTeachManager[0].Delete();
                        SemTeachManager.Save();
                        SemTeachManager.DataTable.AcceptChanges();
                    }
                }

                if (insRows.Length > 0)
                {
                    for (int i = 0; i < insRows.Length; i++)
                    {
                        DataRow drSm = SemTeachManager.NewRow();
                        drSm["PkId"] = SeId;
                        drSm["PPRId"] = SeReqId;
                        drSm["TeId"] = insRows[i]["TeId"].ToString();
                        drSm["PracticalSalary"] = insRows[i]["Salary"].ToString();
                        drSm["Description"] = insRows[i]["Description"].ToString();
                        drSm["Type"] = 1;
                        drSm["UserId"] = Utility.GetCurrentUser_UserId();
                        drSm["ModifiedDate"] = DateTime.Now;

                        SemTeachManager.AddRow(drSm);

                        SemTeachManager.Save();
                        insRows[i].BeginEdit();
                        insRows[i]["SeTeId"] = SemTeachManager[SemTeachManager.Count - 1]["TrTeId"];
                        insRows[i].EndEdit();

                        SemTeachManager.DataTable.AcceptChanges();

                    }

                }

            }
            #endregion

            #region AttachmentFile
            dtOfImg = (DataTable)Session["TblOfImg6"];

            if (dtOfImg.GetChanges() != null)
            {

                DataRow[] delRows = dtOfImg.Select(null, null, DataViewRowState.Deleted);
                DataRow[] insRows = dtOfImg.Select(null, null, DataViewRowState.Added);

                if (delRows.Length > 0)
                {
                    for (int i = 0; i < delRows.Length; i++)
                    {
                        attachManager.FindByCode(int.Parse(delRows[i]["Code", DataRowVersion.Original].ToString()));
                        attachManager[0].Delete();

                        attachManager.Save();
                        attachManager.DataTable.AcceptChanges();

                    }
                }


                if (insRows.Length > 0)
                {
                    for (int i = 0; i < insRows.Length; i++)
                    {
                        DataRow drImg = attachManager.NewRow();
                        drImg["TtId"] = (int)TSP.DataManager.TableCodes.Seminar;
                        drImg["RefTable"] = SeId;
                        drImg["AttId"] = 2;
                        drImg["FilePath"] = insRows[i]["ImgUrl"].ToString();
                        drImg["IsValid"] = 1;
                        drImg["Description"] = insRows[i]["Description"].ToString();
                        drImg["UserId"] = Utility.GetCurrentUser_UserId();
                        drImg["ModfiedDate"] = DateTime.Now;

                        attachManager.AddRow(drImg);

                        attachManager.Save();
                        //insRows[i].BeginEdit();
                        //insRows[i]["PPTeId"] = attachManager[attachManager.Count - 1]["PPTeId"];
                        //insRows[i].EndEdit();

                        if (!string.IsNullOrEmpty(insRows[i]["ImgUrl"].ToString()))
                        {

                            string ImgSoource = Server.MapPath("~/image/Temp/") + insRows[i]["fileName"].ToString();
                            string ImgTarget = Server.MapPath(insRows[i]["ImgUrl"].ToString());
                            File.Copy(ImgSoource, ImgTarget, true);
                        }

                        attachManager.DataTable.AcceptChanges();

                    }
                }
            }


            #endregion

            #region Schedule

            dtOfSchedule = (DataTable)Session["SeminarSchedule"];

            if (dtOfSchedule.GetChanges() != null)
            {

                DataRow[] delRows = dtOfSchedule.Select(null, null, DataViewRowState.Deleted);
                DataRow[] insRows = dtOfSchedule.Select(null, null, DataViewRowState.Added);

                if (delRows.Length > 0)
                {
                    for (int i = 0; i < delRows.Length; i++)
                    {
                        ScheduleManager.FindByCode(int.Parse(delRows[i]["Id", DataRowVersion.Original].ToString()));
                        ScheduleManager[0].Delete();

                        ScheduleManager.Save();
                        ScheduleManager.DataTable.AcceptChanges();

                    }
                }


                if (insRows.Length > 0)
                {
                    for (int i = 0; i < insRows.Length; i++)
                    {
                        DataRow drSchedule = ScheduleManager.NewRow();
                        drSchedule["TtId"] = SeId;
                        drSchedule["PPRId"] = SeReqId;
                        drSchedule["TableType"] = (int)TSP.DataManager.TableCodes.Seminar;
                        drSchedule["StartTime"] = insRows[i]["TimeFrom"].ToString();
                        drSchedule["EndTime"] = insRows[i]["TimeTo"].ToString();
                        drSchedule["Date"] = insRows[i]["Date"].ToString();
                        drSchedule["Subject"] = insRows[i]["Subject"].ToString();
                        drSchedule["Description"] = insRows[i]["Description"].ToString();
                        drSchedule["UserId"] = Utility.GetCurrentUser_UserId();
                        drSchedule["Type"] = 1;//seminar
                        drSchedule["UserId"] = Utility.GetCurrentUser_UserId();
                        drSchedule["ModifiedDate"] = DateTime.Now;
                        //
                        ScheduleManager.AddRow(drSchedule);

                        ScheduleManager.Save();

                        ScheduleManager.DataTable.AcceptChanges();

                    }

                }

                // dtOfCost.AcceptChanges();
            }


            #endregion

            #region Grade

            dtOfGrade = (DataTable)Session["SeGrade"];

            if (dtOfGrade.GetChanges() != null)
            {

                DataRow[] delRows = dtOfGrade.Select(null, null, DataViewRowState.Deleted);
                DataRow[] insRows = dtOfGrade.Select(null, null, DataViewRowState.Added);

                if (delRows.Length > 0)
                {
                    for (int i = 0; i < delRows.Length; i++)
                    {
                        GradeManager.FindByCode(int.Parse(delRows[i]["Id", DataRowVersion.Original].ToString()));
                        GradeManager[0].Delete();

                        GradeManager.Save();
                        GradeManager.DataTable.AcceptChanges();

                    }
                }


                if (insRows.Length > 0)
                {
                    for (int i = 0; i < insRows.Length; i++)
                    {
                        DataRow drGrade = GradeManager.NewRow();
                        drGrade["UpGrdPId"] = insRows[i]["UpGrdPId"].ToString();
                        drGrade["PkId"] = SeId;
                        drGrade["Type"] = 1;//Seminar
                        drGrade["Description"] = insRows[i]["Description"].ToString();
                        drGrade["CreateDate"] = Utility.GetDateOfToday();
                        drGrade["UserId"] = Utility.GetCurrentUser_UserId();
                        drGrade["ModifiedDate"] = DateTime.Now;

                        GradeManager.AddRow(drGrade);

                        //insRows[i].BeginEdit();
                        //insRows[i]["PPTeId"] = attachManager[attachManager.Count - 1]["PPTeId"];
                        //insRows[i].EndEdit();

                        //ScheduleManager.DataTable.AcceptChanges();

                    }
                    GradeManager.Save();


                }

                // dtOfCost.AcceptChanges();
            }


            #endregion

            trans.EndSave();
            ASPxRoundPanel2.HeaderText = "ویرایش";
            _PageMode = "Edit";
            Session["IsEdited_Seminar"] = true;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";
        }
        catch (Exception err)
        {
            trans.CancelSave();

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
                    this.LabelWarning.Text = "کد سمینار تکراری می باشد";
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
    }

    private void StartWorkFlow(int TableId, int NmcId, TSP.DataManager.WorkFlowStateManager WorkFlowStateManager, TSP.DataManager.TransactionManager TransactionManager)
    {
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveSeminarInfo;
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            int TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());

            DataRow StateRow = WorkFlowStateManager.NewRow();
            StateRow["TaskId"] = TaskId;
            StateRow["TableId"] = TableId;
            StateRow["NmcId"] = NmcId;
            StateRow["SubOrder"] = 1;
            //StateRow["Description"] = "";
            StateRow["UserId"] = Utility.GetCurrentUser_UserId();
            StateRow["ModifiedDate"] = DateTime.Now;

            WorkFlowStateManager.AddRow(StateRow);
            int cn = WorkFlowStateManager.Save();
            if (cn > 0)
            {
                DivReport.Visible = true;
                LabelWarning.Text = "ذخیره انجام شد.";
            }
            else
            {
                TransactionManager.CancelSave();
                DivReport.Visible = true;
                LabelWarning.Text = "خطایی در ذخیره انجام شد.";
                return;
            }
        }
        else
        {
            TransactionManager.CancelSave();
            DivReport.Visible = true;
            LabelWarning.Text = "خطایی در ذخیره انجام شد.";
            return;
        }

    }

    private int FindNmcId(TSP.DataManager.LoginManager LoginManager, TSP.DataManager.TransactionManager TransactionManager)
    {
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        LoginManager.FindByCode(UserId);
        int NmcId = -1;
        if (LoginManager.Count > 0)
        {
            int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
            int UltId = int.Parse(LoginManager[0]["UltId"].ToString());
            NezamChartManager.FindByEmpId(EmpId, UltId);
            if (NezamChartManager.Count > 0)
            {
                NmcId = int.Parse(NezamChartManager[0]["NmcId"].ToString());
            }
            else
            {
                TransactionManager.CancelSave();
                DivReport.Visible = true;
                LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
            }
        }
        else
        {
            TransactionManager.CancelSave();
            Response.Redirect("../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            return (-1);
        }
        return (NmcId);
    }

    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/Seminar/") + ret) == true || System.IO.File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["SeminarUpload"] = tempFileName;
        }
        return ret;
    }

    private void CheckWorkFlowPermission()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveSeminarInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            CheckWorkFlowPermissionForSave(_PageMode);
            if (_PageMode != "New")
                CheckWorkFlowPermissionForEdit(_PageMode);
        }
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {

        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.SaveSeminarInfo;
        int Permission = -1;
        int TableType = (int)TSP.DataManager.TableCodes.Seminar;
        Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());
        if (Permission > 0)
        {
            BtnNew.Enabled = true;
            btnNew2.Enabled = true;
            switch (PageMode)
            {
                case "New":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
                case "View":
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
            }
        }
        else
        {
            switch (Permission)
            {
                case -1:
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "شما قادر به تعریف سمینار جدید نمی باشید";
                    break;
                case -6:
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "شما قادر به تعریف سمینار جدید نمی باشید";
                    break;
                case -7:
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "نام موسسه شما در چارت سازمانی وارد نشده است.شما قادر به تعریف سمینار جدید نمی باشید";
                    break;
                default:
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    break;
            }

            BtnNew.Enabled = false;
            btnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = BtnNew.Enabled;

    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        int TableType = (int)TSP.DataManager.TableCodes.Seminar;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveSeminarInfo;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, _SeReqId, TaskCode, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0)
        {
            switch (PageMode)
            {
                case "Edit":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
                case "View":
                    btnEdit.Enabled = true;
                    btnEdit2.Enabled = true;
                    //btnSave.Enabled = true;
                    //btnSave2.Enabled = true;
                    break;
            }
        }
        else
        {
            switch (PageMode)
            {
                case "View":
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    break;
            }
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
        }
        //if (Permisssion < 0)
        //{
        //    btnEdit.Enabled = false;
        //    btnEdit2.Enabled = false;
        //    btnSave.Enabled = false;
        //    btnSave2.Enabled = false;

        //}

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        int WfCode = (int)TSP.DataManager.WorkFlows.SeminarConfirming;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveSeminarInfo;
        TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
        return (WorkFlowPermission.CheckPermissionForEditByUser(TableId, WfCode, TaskCode, Utility.GetCurrentUser_UserId(), Utility.GetCurrentUser_NmcIdType()));
    }

    private void InsertWorkFlowStateView(int TableType, int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        try
        {
            int ViewState = WorkFlowStateManager.InsertWorkFlowViewState(TableType, TableId, "View", Utility.GetCurrentUser_UserId());
            if (ViewState == -4)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
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
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    #endregion
}
