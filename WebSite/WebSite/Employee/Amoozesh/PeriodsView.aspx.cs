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
public partial class Employee_Amoozesh_PeriodsView : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.PeriodOpinionManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnSave.Enabled = per.CanNew;
            btnSave2.Enabled = per.CanNew;


            HDUltId["UltId"] = "";

            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["PPId"]))
            {
                Response.Redirect("Periods.aspx");
                return;
            }

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                PeriodId.Value = Server.HtmlDecode(Request.QueryString["PPId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string PPId = Utility.DecryptQS(PeriodId.Value);

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            OdbGrades.SelectParameters["PkId"].DefaultValue = PPId;
            OdbGrades.SelectParameters["Type"].DefaultValue = "0";

            CustomAspxDevGridViewGrade.DataBind();


            switch (PageMode)
            {

                case "View":

                    if (string.IsNullOrEmpty(PPId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    InsertWorkFlowStateView((int)TSP.DataManager.TableCodes.PeriodPresent, int.Parse(PPId));
                    FillForm(int.Parse(PPId));
                    FillOpinion(int.Parse(PPId));
                    TSP.DataManager.ScheduleManager SchManager = new TSP.DataManager.ScheduleManager();

                    SchManager.FindByTtIdTableType(int.Parse(PPId), (int)TSP.DataManager.TableCodes.PeriodPresent);
                    if (SchManager.Count > 0)
                    {
                        CustomAspxDevGridView1.DataSource = SchManager.DataTable;
                        CustomAspxDevGridView1.DataBind();

                    }
                    break;

                case "PRView":

                    if (string.IsNullOrEmpty(PPId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    string MeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["MeId"].ToString()));
                    MemberId.Value = Server.HtmlDecode(Request.QueryString["MeId"].ToString());
                    try
                    {
                        MeId = Utility.DecryptQS(MemberId.Value);
                    }
                    catch
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    }
                    if (string.IsNullOrEmpty(MeId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    //InsertWorkFlowStServer.HtmlDecode(Request.QueryString["PageMode"].ToString()ateView((int)TSP.DataManager.TableCodes.PeriodPresent, int.Parse(PPId));
                    ASPxRoundPanelPPRegister.Visible = true;
                    ASPxRoundPanel5.Visible = false;
                    ASPxRoundPanel7.Visible = false;
                    ASPxRoundPanel8.Visible = false;
                    ASPxRoundPanel9.Visible = false;
                    btnOpinion.Visible = false;
                    btnOpinion1.Visible = false;
                    btnInActive.Visible = false;
                    btnInactive2.Visible = false;
                    btnSave.Visible = false;
                    btnSave2.Visible = false;

                    FillForm(int.Parse(PPId));
                    FillPPRegister(int.Parse(PPId), int.Parse(MeId));
                    break;
            }
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInactive2.Enabled = (bool)this.ViewState["BtnInActive"];

        //btnBack.PostBackUrl = "Periods.aspx";
        //ASPxButton1.PostBackUrl = "Periods.aspx";

    }

    protected void FillForm(int PPId)
    {
        //string PageMode = Utility.DecryptQS(PgMode.Value);

        TSP.DataManager.PeriodPresentManager manager = new TSP.DataManager.PeriodPresentManager();
        try
        {
            manager.FindByCode(PPId);
            if (manager.Count == 1)
            {
                decimal discount = 0;
                decimal TestCost = 0;

                txtCapacity.Text = manager[0]["Capacity"].ToString();
                //if (!Utility.IsDBNullOrNullValue(manager[0]["Discount"]))
                //{
                //    discount = Convert.ToDecimal(manager[0]["Discount"].ToString());
                //    txtDiscount.Text = discount.ToString("#,#");
                //}
                if (!Utility.IsDBNullOrNullValue(manager[0]["TestCost"]))
                {
                    TestCost = Convert.ToDecimal(manager[0]["TestCost"].ToString());
                    txtTestCost.Text = TestCost.ToString("#,#");
                }
                txtEndDate.Text = manager[0]["EndDate"].ToString();
                decimal PeriodCost = Convert.ToDecimal(manager[0]["PeriodCost"].ToString());
                txtPeriodCost.Text = PeriodCost.ToString("#,#");
                txtPPCode.Text = manager[0]["PPCode"].ToString();
                txtPlace.Text = manager[0]["Place"].ToString();

                txtTestDate.Text = manager[0]["TestDate"].ToString();
                txtTestHour.Text = manager[0]["TestHour"].ToString();
                txtTestPlace.Text = manager[0]["TestPlace"].ToString();

                txtStartDate.Text = manager[0]["StartDate"].ToString();

                txtDesc.Text = manager[0]["Description"].ToString();
                txtCrsId.Text = manager[0]["CrsName"].ToString();
                // txtTeId.Text = manager[0]["InsName"].ToString();
                FillCourse(int.Parse(manager[0]["CrsId"].ToString()));
                // FillTeacher(int.Parse(manager[0]["TeId"].ToString()));
                FillTeacher(PPId);

                //decimal SalaryNonPractical = Convert.ToDecimal(manager[0]["SalaryNonPractical"].ToString());
                //txtSalNonpractical.Text = SalaryNonPractical.ToString("#,#");
                //decimal SalaryPractical = Convert.ToDecimal(manager[0]["SalaryPractical"].ToString());
                //txtSalPractical.Text = SalaryPractical.ToString("#,#");
                //decimal SalaryWorkroom = Convert.ToDecimal(manager[0]["SalaryWorkroom"].ToString());
                //txtSalWorkroom.Text = SalaryWorkroom.ToString("#,#");


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

    protected void FillPPRegister(int PPId, int MeId)
    {
        TSP.DataManager.PeriodRegisterManager PRManager = new TSP.DataManager.PeriodRegisterManager();
        try
        {
            DataTable dt = PRManager.SelectPeriodRegister(MeId, PPId, 0);
            if (dt.Rows.Count > 0)
            {
                txtsFirstMark.Text = dt.Rows[0]["FirstMark"].ToString();
                txtsLastMark.Text = dt.Rows[0]["LastMark"].ToString();
                txtsMeObjectionDate.Text = dt.Rows[0]["MeObjectionDate"].ToString();
                txtsMeObjectionText.Text = dt.Rows[0]["MeObjectionText"].ToString();
                txtsPayType.Text = dt.Rows[0]["PayType"].ToString();
                txtsRegisterDate.Text = dt.Rows[0]["RegisterDate"].ToString();
                txtsRgstType.Text = dt.Rows[0]["RgstType"].ToString();
                txtsTeObjectionDate.Text = dt.Rows[0]["MeObjectionDate"].ToString();
                txtsTeObjectionText.Text = dt.Rows[0]["TeObjectionText"].ToString();

            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
    }

    protected void FillCourse(int CrsId)
    {
        TSP.DataManager.CourseManager CrsManager = new TSP.DataManager.CourseManager();
        try
        {

            CrsManager.FindByCode(CrsId);
            if (CrsManager.Count > 0)
            {
                txtPDuration.Text = CrsManager[0]["Duration"].ToString();
                txtPoint.Text = CrsManager[0]["Point"].ToString();
                //txtPTypeName.Text = CrsManager[0]["TypeName"].ToString();
                txtValidDuration.Text = CrsManager[0]["ValidDuration"].ToString();
                txtbPracticalDuration.Text = CrsManager[0]["PracticalDuration"].ToString();
                txtbNonPracticalDuration.Text = CrsManager[0]["NonPracticalDuration"].ToString();
                txtbWorkroomDuration.Text = CrsManager[0]["WorkroomDuration"].ToString();

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";
            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
    }

    protected void FillTeacher(int PPId)
    {
        // TSP.DataManager.TeacherManager TeManager = new TSP.DataManager.TeacherManager();
        try
        {
            TSP.DataManager.TrainingTeachersManager PPTeManager = new TSP.DataManager.TrainingTeachersManager();
            //CustomAspxDevGridView2.DataSource = PPTeManager.FindByPPCode(PPId);
            CustomAspxDevGridView2.DataSource = PPTeManager.FindByPKCode(PPId, 0);
            CustomAspxDevGridView2.DataBind();
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }


        //try
        //{

        //TeManager.FindByCode(TeId);
        //if (TeManager.Count > 0)
        //{
        //    if (string.IsNullOrEmpty(TeManager[0]["MeId"].ToString()))
        //    {
        //        txtTMeId.Text = "";
        //        txtTPaye.Text = "";
        //        lblMeId.Enabled = false;
        //        lblPaye.Enabled = false;
        //        txtTPaye.Enabled = false;
        //        txtTMeId.Enabled = false;
        //        this.lblInsError.Visible = true;
        //        this.lblInsError.Text = "استاد مورد نظر عضو حقیقی نظام مهندسی نمی باشد";
        //    }
        //    else
        //    {
        //        lblMeId.Enabled = true;
        //        lblPaye.Enabled = true;
        //        txtTPaye.Enabled = true;
        //        txtTMeId.Enabled = true;
        //        this.lblInsError.Visible = false;
        //        txtTMeId.Text = TeManager[0]["MeId"].ToString();
        //        //txtTPaye.Text=?????????

        //    }

        //    txtTFileNo.Text = TeManager[0]["FileNo"].ToString();
        //    txtTLicence.Text = TeManager[0]["LiName"].ToString();
        //    txtTMajor.Text = TeManager[0]["MjName"].ToString();
        //    txtTTeName.Text = TeManager[0]["TeName"].ToString();
        //}
        //else
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";
        //}
        //}
        //catch (Exception err)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        //}
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        // string name = cmbMemberChart.Columns[0].FieldName;
        //string id = cmbMemberChart.Value.ToString();
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string PPId = Utility.DecryptQS(PeriodId.Value);

        if (!string.IsNullOrEmpty(PageMode))
        {
            if (PageMode == "View")
            {
                if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(PPId))
                {
                    string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
                    Response.Redirect("Periods.aspx?PostId=" + PeriodId.Value + "&GrdFlt=" + GrdFlt);
                }
                else
                {
                    Response.Redirect("Periods.aspx");
                }

            }

            else
                Response.Redirect("MemberLicence.aspx");

        }
        else
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

    }

    protected void FillOpinion(int PPId)
    {
        TSP.DataManager.PeriodOpinionManager OpManager = new TSP.DataManager.PeriodOpinionManager();
        try
        {
            //****PeriodOpinio ==> Type ==> 1: شرکت کنندگان 
            //****PeriodOpinio ==> Type ==> 2:بازرسان
            OpManager.FindByPeriodId(PPId, 1);
            if (OpManager.Count > 0)
            {
                //CmbQucode.SelectedIndex = CmbQucode.Items.IndexOfText(OpManager[0]["QuCode"].ToString());

                CmbQucode.Value = OpManager[0]["QuCode"].ToString();
                txtOpExpDate.Text = OpManager[0]["ExpiredDate"].ToString();
                txtOpStartDate.Text = OpManager[0]["StartDate"].ToString();
            }

            OpManager.FindByPeriodId(PPId, 2);
            if (OpManager.Count > 0)
            {
                ASPxRoundPanel8.Disabled = true;
                //cmbMemberChart.Visible = false;
                //txtBzName.Visible = true;
                //txtBzName.Text = OpManager[0]["VName"].ToString();
                CmbBzType.Value = OpManager[0]["UltId"].ToString();
                if (CmbBzType.Value.ToString() == "1")
                {
                    ASPxLabel34.ClientVisible = true;
                    ASPxLabel37.ClientVisible = false;

                }
                txtBzCode.Text = OpManager[0]["EmpId"].ToString();
                txtBzFirstName.Text = OpManager[0]["FirstName"].ToString();
                txtBzLastName.Text = OpManager[0]["LastName"].ToString();


                cmbBzQuCode.Value = OpManager[0]["QuCode"].ToString();
                txtBzExpDate.Text = OpManager[0]["ExpiredDate"].ToString();
                txtBzStartDate.Text = OpManager[0]["StartDate"].ToString();

                if (Convert.ToInt32(OpManager[0]["UltId"]) == (int)TSP.DataManager.UserType.Employee)
                {
                    int EmpId = Utility.GetCurrentUser_UserId();
                    if (Convert.ToInt32(OpManager[0]["EmpId"]) == EmpId)
                    {
                        HDQuCode.Value = Utility.EncryptQS(OpManager[0]["QuCode"].ToString());
                        btnOpinion.Visible = true;
                        btnOpinion1.Visible = true;
                    }
                    //cmbMemberChart                   

                }
            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
    }

    protected void btnSave2_Click(object sender, EventArgs e)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.PeriodOpinionManager OpManager = new TSP.DataManager.PeriodOpinionManager();
        // TSP.DataManager.MemberOpinionManager MeOpManager = new TSP.DataManager.MemberOpinionManager();
        TSP.DataManager.OpinionsManager OpinionManager = new TSP.DataManager.OpinionsManager();
        trans.Add(OpManager);
        OpManager.ClearBeforeFill = true;

        try
        {
            int PPId = int.Parse(Utility.DecryptQS(PeriodId.Value));

            //MeOpManager.FindByPeriodId(PPId);
            //if (MeOpManager.Count > 0)
            //{
            //    this.DivReport.Visible = true;
            //    this.LabelWarning.Text = "امکان ویرایش سری سؤالات وجود ندارد";
            //    return;
            //}
            OpinionManager.FindByPeriodId(PPId);
            if (OpinionManager.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل شروع نظر سنجی امکان ویرایش سری سؤالات وجود ندارد";
                return;
            }
            trans.BeginSave();

            OpManager.FindByPeriodId(PPId, 1);//شرکت کنندگان
            if (OpManager.Count > 0)
            {
                OpManager[0].BeginEdit();
                OpManager[0]["QuCode"] = CmbQucode.Text;
                OpManager[0]["StartDate"] = txtOpStartDate.Text;
                OpManager[0]["ExpiredDate"] = txtOpExpDate.Text;
                OpManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                OpManager[0].EndEdit();
                OpManager.Save();
                OpManager.DataTable.AcceptChanges();

            }

            OpManager.FindByPeriodId(PPId, 2);//بازرس
            if (OpManager.Count > 0)
            {
                OpManager[0].BeginEdit();
                OpManager[0]["QuCode"] = cmbBzQuCode.Text;
                OpManager[0]["StartDate"] = txtBzStartDate.Text;
                OpManager[0]["ExpiredDate"] = txtBzExpDate.Text;
                OpManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                OpManager[0].EndEdit();
                OpManager.Save();
                OpManager.DataTable.AcceptChanges();

            }
            else
            {
                DataRow dr = OpManager.NewRow();
                dr["PeriodId"] = PPId;
                dr["QuCode"] = cmbBzQuCode.Value.ToString();
                //if (cmbMemberChart.Value != null)
                //    dr["EmpId"] = Convert.ToInt32(HDUltId["UltId"].ToString());
                //dr["EmpId"] = cmbMemberChart.Value.ToString();
                dr["EmpId"] = int.Parse(txtBzCode.Text);
                dr["StartDate"] = txtBzStartDate.Text;
                dr["ExpiredDate"] = txtBzExpDate.Text;
                //dr["UltId"] = Convert.ToInt16(cmbMemberChart.Value.ToString());
                dr["UltId"] = CmbBzType.Value;
                dr["Type"] = 2;//بازرس
                dr["UserId"] = Utility.GetCurrentUser_UserId();
                dr["ModifiedDate"] = DateTime.Now;
                OpManager.AddRow(dr);
                if (OpManager.Save() > 0)
                {
                    OpManager.DataTable.AcceptChanges();

                }
                else
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }

            trans.EndSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";
        }
        catch (Exception err)
        {
            trans.CancelSave();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";

        }
    }

    protected void btnOpinion_Click(object sender, EventArgs e)
    {
        Response.Redirect("PeriodOpinionForm.aspx?PPId=" + PeriodId.Value + "&QuCode=" + HDQuCode.Value + "&PageMode=" + PgMode.Value);
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

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty(PeriodId.Value))
        //{
        //    int PPId = int.Parse(Utility.DecryptQS(PeriodId.Value));
        //    if (!IsInActive(PPId))
        //    {
        //        Response.Redirect("InActivePeriod.aspx?PPId=" + PeriodId.Value + "&PgMd=" +Utility.EncryptQS("New"));
        //    }
        //    else
        //    {
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "دوره انتخاب شده لغو شده است.";
        //    }
        //}
        if (!string.IsNullOrEmpty(PeriodId.Value))
        {
            int PSCId = -1;
            int TableType = (int)TSP.DataManager.TableCodes.PeriodPresent;
            int PPId = int.Parse(Utility.DecryptQS(PeriodId.Value));
            if (!IsInActive(PPId))
            {
                Response.Redirect("InActivePeriod.aspx?PPId=" + PeriodId.Value + "&PageMode=" + PgMode.Value + "&PSCId=" + Utility.DecryptQS(PSCId.ToString()) + "&PgMd=" + Utility.EncryptQS("New"));
            }
            else
            {
                TSP.DataManager.TrainingStatusChangeManager TrainingStatusChangeManager = new TSP.DataManager.TrainingStatusChangeManager();

                TrainingStatusChangeManager.FindByTableType(TableType, PPId, 0);
                if (TrainingStatusChangeManager.Count > 0)
                {
                    PSCId = int.Parse(TrainingStatusChangeManager[0]["PSCId"].ToString());
                    Response.Redirect("InActivePeriod.aspx?PPId=" + PeriodId.Value + "&PSCId=" + Utility.EncryptQS(PSCId.ToString()) + "&PgMd=" + Utility.EncryptQS("View"));
                }
            }
        }
    }

    private Boolean IsInActive(int PPId)
    {
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        PeriodPresentManager.FindByCode(PPId);
        if (PeriodPresentManager.Count == 1)
        {
            if (Convert.ToInt32(PeriodPresentManager[0]["Status"]) == (int)TSP.DataManager.PeriodPresentStatus.InvalidPeriod)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    protected void txtBzCode_TextChanged(object sender, EventArgs e)
    {
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.EmployeeManager EmpManager = new TSP.DataManager.EmployeeManager();

        try
        {
            if (CmbBzType.Value != null)
            {
                if (CmbBzType.Value.ToString() == "1")//Member
                {
                    ASPxLabel34.ClientVisible = true;
                    ASPxLabel37.ClientVisible = false;

                    if (!string.IsNullOrEmpty(txtBzCode.Text))
                    {
                        MeManager.FindByCode(int.Parse(txtBzCode.Text));
                        if (MeManager.Count == 1)
                        {
                            txtBzFirstName.Text = MeManager[0]["FirstName"].ToString();
                            txtBzLastName.Text = MeManager[0]["LastName"].ToString();
                            txtEmpId.Text = txtBzCode.Text;
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "چنین کد عضویتی وجود ندارد.مجدداً وارد نمایید";
                            return;
                        }

                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "کد عضویت را وارد نمایید";
                        return;
                    }
                }
                else //Employee
                {
                    ASPxLabel34.ClientVisible = false;
                    ASPxLabel37.ClientVisible = true;

                    if (!string.IsNullOrEmpty(txtBzCode.Text))
                    {
                        EmpManager.FindByEmpCode(txtBzCode.Text);
                        if (EmpManager.Count == 1)
                        {
                            txtBzFirstName.Text = EmpManager[0]["FirstName"].ToString();
                            txtBzLastName.Text = EmpManager[0]["LastName"].ToString();
                            txtEmpId.Text = EmpManager[0]["EmpId"].ToString();
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "چنین کد کارمندی وجود ندارد.مجدداً وارد نمایید";
                            return;
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "کد کارمندی را وارد نمایید";
                        return;
                    }
                }

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "نوع بازرس را انتخاب نمایید";
            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی رخ داده است.بازرس را مجدداً انتخاب نمایید";
        }
    }

    protected void CustomAspxDevGridView1_OnHtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Date")
        {
            e.Cell.Style["direction"] = "ltr";
        }
    }

    protected void CallbackPanelPeriod_OnCallback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        CallbackPanelPeriod.JSProperties["cpPrintPgName"] = "";
        string Parameters = e.Parameter;
        switch (Parameters)
        {
            case "Print":

                CallbackPanelPeriod.JSProperties["cpPrintPgName"] = "../../ReportForms/Amoozesh/CourseReport.aspx?PeId=" + PeriodId.Value + "&MeId=" + MemberId.Value;

                break;
        }
    }

}


