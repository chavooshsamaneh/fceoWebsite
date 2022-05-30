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

public partial class Employee_Amoozesh_AddTeacherCourse : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (string.IsNullOrEmpty(Request.QueryString["PgMd"]) || string.IsNullOrEmpty(Request.QueryString["TCrsJId"]))
        {
            Response.Redirect("TeacherCourse.aspx");
            return;
        }
        if (!IsPostBack)
        {
            SetKeys();
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {

    }


    protected void btnDisActive_Click(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(HiddenFieldTeacherCourse["PageMode"].ToString());
        if (PageMode == "New")
        {
            InsertTeacherCourseJudgment();
        }
        else
        {
            if (PageMode == "Edit")
            {
                string TCrsJId = Utility.DecryptQS(HiddenFieldTeacherCourse["TCrsJId"].ToString());
                EditTeacherCourseJudgment(int.Parse(TCrsJId));
            }
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TeacherCourse.aspx");
    }
    #endregion

    #region Methods
    private void InsertTeacherCourseJudgment()
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TeacherCourseJudgmentManager TeacherCourseJudgmentManager= new TSP.DataManager.TeacherCourseJudgmentManager();
        TSP.DataManager.TeachersCourseManager TeachersCourseManager = new TSP.DataManager.TeachersCourseManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TransactionManager.Add(TeacherCourseJudgmentManager);
        TransactionManager.Add(TeachersCourseManager);
        try
        {
            TransactionManager.BeginSave();
            LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
            int EmpId= int.Parse(LoginManager[0]["MeId"].ToString());
            DataRow TCourseJudgmentRow = TeacherCourseJudgmentManager.NewRow();
            int TableType = (int)TSP.DataManager.TableCodes.TeacherCourseJudgment;
            int TCrsId=int.Parse(Utility.DecryptQS(HiddenFieldTeacherCourse["TCrsId"].ToString()));
            TCourseJudgmentRow["TableType"] = TableType;
            TCourseJudgmentRow["TableId"] = TCrsId;
            TCourseJudgmentRow["EmpId"] = EmpId;
            if(rdbtnIsConfirm.SelectedIndex==0)//Not Confirme
            TCourseJudgmentRow["IsConfirmed"] =2;
            if (rdbtnIsConfirm.SelectedIndex == 1)//Confirm
            TCourseJudgmentRow["IsConfirmed"] = 1;
            TCourseJudgmentRow["ViewPoint"] = txtDescription.Text;
            TCourseJudgmentRow["MailNo"] = txtMailNo.Text;
            TCourseJudgmentRow["UserId"] =Utility.GetCurrentUser_UserId();
            TCourseJudgmentRow["ModifiedDate"] = DateTime.Now;

            TeacherCourseJudgmentManager.AddRow(TCourseJudgmentRow);
            int cn = TeacherCourseJudgmentManager.Save();
            if (cn > 0)
            {
                Boolean IsConfirmed = true;
                DataTable dtTeacherCourseJudgment= TeacherCourseJudgmentManager.SelectByEmpId(TCrsId,-1, (int)TSP.DataManager.TableCodes.TeacherCourseJudgment);
                if (dtTeacherCourseJudgment.Rows.Count > 0)
                {
                    int JudgeCount= dtTeacherCourseJudgment.Rows.Count;
                    for (int i = 0; i < JudgeCount; i++)
                    {
                        IsConfirmed= Convert.ToBoolean(int.Parse(dtTeacherCourseJudgment.Rows[i]["IsConfirmed"].ToString()));
                        if (!IsConfirmed)
                        {
                            TeachersCourseManager.FindByCode(TCrsId);
                            if (TeachersCourseManager.Count > 0)
                            {
                                TeachersCourseManager[0].BeginEdit();
                                TeachersCourseManager[0]["IsConfirmed"] = 0;
                                TeachersCourseManager[0].EndEdit();
                                int cnt = TeachersCourseManager.Save();
                                if(cn>0)
                                {
                                    TransactionManager.EndSave();
                                    HiddenFieldTeacherCourse["PageMode"] = Utility.EncryptQS("Edit");
                                    HiddenFieldTeacherCourse["TCrsJId"] = Utility.EncryptQS(TeacherCourseJudgmentManager[0]["TCrsJId"].ToString());
                                    RoundPanelTeacherCourse.HeaderText = "مشاهده";
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "ذخیره انجام شد.";
                                }
                                else
                                {
                                    TransactionManager.CancelSave();
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                                }
                            }
                            else
                            {
                                TransactionManager.CancelSave();
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                            }
                            break;
                        }
                    }
                    if (IsConfirmed)
                    {
                        TeachersCourseManager.FindByCode(TCrsId);
                        if (TeachersCourseManager.Count > 0)
                        {
                            TeachersCourseManager[0].BeginEdit();
                            TeachersCourseManager[0]["IsConfirmed"] = 1;
                            TeachersCourseManager[0].EndEdit();
                            int cnt = TeachersCourseManager.Save();
                            if (cn > 0)
                            {
                                TransactionManager.EndSave();
                                HiddenFieldTeacherCourse["PageMode"] = Utility.EncryptQS("Edit");
                                HiddenFieldTeacherCourse["TCrsJId"] = Utility.EncryptQS(TeacherCourseJudgmentManager[0]["TCrsJId"].ToString());
                                RoundPanelTeacherCourse.HeaderText = "مشاهده";
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "ذخیره انجام شد.";
                            }
                            else
                            {
                                TransactionManager.CancelSave();
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                            }
                        }
                        else
                        {
                            TransactionManager.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                        }
                    }
                }             
            }
            else
            {
                TransactionManager.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
            }
        }
        catch(Exception err)
        {
            TransactionManager.CancelSave();
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

    private void EditTeacherCourseJudgment(int TCrsJId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TeachersCourseManager TeachersCourseManager = new TSP.DataManager.TeachersCourseManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.TeacherCourseJudgmentManager TeacherCourseJudgmentManager = new TSP.DataManager.TeacherCourseJudgmentManager();
        TransactionManager.Add(TeachersCourseManager);
        TransactionManager.Add(TeacherCourseJudgmentManager);
        try
        {
            TransactionManager.BeginSave();
            TeacherCourseJudgmentManager.FindByCode(TCrsJId);
            if (TeacherCourseJudgmentManager.Count == 1)
            {
                LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
                int EmpId = int.Parse(LoginManager[0]["MeId"].ToString());
                int TCrsId = int.Parse(Utility.DecryptQS(HiddenFieldTeacherCourse["TCrsId"].ToString()));
                TeacherCourseJudgmentManager[0].BeginEdit();
                TeacherCourseJudgmentManager[0]["TableId"] = TCrsId;
                TeacherCourseJudgmentManager[0]["EmpId"] = EmpId;
                if(rdbtnIsConfirm.SelectedIndex==0)//Not Confirm
                TeacherCourseJudgmentManager[0]["IsConfirmed"] = 2;
                if (rdbtnIsConfirm.SelectedIndex == 0)//Confirm
                TeacherCourseJudgmentManager[0]["IsConfirmed"] = 1;
                TeacherCourseJudgmentManager[0]["ViewPoint"] = txtDescription.Text;
                TeacherCourseJudgmentManager[0]["MailNo"] = txtMailNo.Text;
                TeacherCourseJudgmentManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                TeacherCourseJudgmentManager[0]["ModifiedDate"] = DateTime.Now;

                TeacherCourseJudgmentManager[0].EndEdit();
                int cn = TeacherCourseJudgmentManager.Save();
                if (cn > 0)
                {
                    Boolean IsConfirmed = true;
                    DataTable dtTeacherCourseJudgment = TeacherCourseJudgmentManager.SelectByEmpId(TCrsId, -1, (int)TSP.DataManager.TableCodes.TeacherCourseJudgment);
                    if (dtTeacherCourseJudgment.Rows.Count > 0)
                    {
                        int JudgeCount = dtTeacherCourseJudgment.Rows.Count;
                        for (int i = 0; i < JudgeCount; i++)
                        {
                            IsConfirmed = Convert.ToBoolean(int.Parse(dtTeacherCourseJudgment.Rows[i]["IsConfirmed"].ToString()));
                            if (!IsConfirmed)
                            {
                                TeachersCourseManager.FindByCode(TCrsId);
                                if (TeachersCourseManager.Count > 0)
                                {
                                    TeachersCourseManager[0].BeginEdit();
                                    TeachersCourseManager[0]["IsConfirmed"] = 0;
                                    TeachersCourseManager[0].EndEdit();
                                    int cnt = TeachersCourseManager.Save();
                                    if (cn > 0)
                                    {
                                        TransactionManager.EndSave();
                                        this.DivReport.Visible = true;
                                        this.LabelWarning.Text = "ذخیره انجام شد.";
                                    }
                                    else
                                    {
                                        TransactionManager.CancelSave();
                                        this.DivReport.Visible = true;
                                        this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                                    }
                                }
                                else
                                {
                                    TransactionManager.CancelSave();
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                                }
                                break;
                            }
                        }
                        if (IsConfirmed)
                        {
                            TeachersCourseManager.FindByCode(TCrsId);
                            if (TeachersCourseManager.Count > 0)
                            {
                                TeachersCourseManager[0].BeginEdit();
                                TeachersCourseManager[0]["IsConfirmed"] = 1;
                                TeachersCourseManager[0].EndEdit();
                                int cnt = TeachersCourseManager.Save();
                                if (cn > 0)
                                {
                                    TransactionManager.EndSave();
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = "ذخیره انجام شد.";
                                }
                                else
                                {
                                    TransactionManager.CancelSave();
                                    this.DivReport.Visible = true;
                                    this.LabelWarning.Text = ".خطایی در ذخیره انجام گرفته است";
                                }
                            }
                            else
                            {
                                TransactionManager.CancelSave();
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                            }
                        }
                    }
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
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            TransactionManager.CancelSave();
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

    private void SetKeys()
    {
        HiddenFieldTeacherCourse["TCrsJId"] = Request.QueryString["TCrsJId"];
        HiddenFieldTeacherCourse["PageMode"] = Request.QueryString["PgMd"];
        HiddenFieldTeacherCourse["TCrsId"] = Request.QueryString["TCrsId"];
        string PageMode = Utility.DecryptQS(HiddenFieldTeacherCourse["PageMode"].ToString());
        string TCrsId = Utility.DecryptQS(HiddenFieldTeacherCourse["TCrsId"].ToString());
        FillTeacherCourse(int.Parse(TCrsId));
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetMode(PageMode);
    }

    private void SetMode(string PageMode)
    {
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        switch (PageMode)
        {
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
        ClearForm();

       // RoundPanelTeacherJob.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        string TCrsJId = Utility.DecryptQS(HiddenFieldTeacherCourse["TCrsJId"].ToString());
        if (string.IsNullOrEmpty(TCrsJId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        FillForm(int.Parse(TCrsJId));

     //   RoundPanelTeacherJob.HeaderText = "ویرایش";
    }

    private void ClearForm()
    {
        txtDescription.Text = "";
        txtMailNo.Text = "";
        rdbtnIsConfirm.SelectedIndex = 0;
    }

    private void FillForm(int TCrsJId)
    {
        TSP.DataManager.TeacherCourseJudgmentManager TeacherCourseJudgmentManager = new TSP.DataManager.TeacherCourseJudgmentManager();
        TeacherCourseJudgmentManager.FindByCode(TCrsJId);
        if (TeacherCourseJudgmentManager.Count==1)
        {
            if(int.Parse(TeacherCourseJudgmentManager[0]["IsConfirmed"].ToString())==1)
            rdbtnIsConfirm.SelectedIndex =1;
        if (int.Parse(TeacherCourseJudgmentManager[0]["IsConfirmed"].ToString()) == 2)
            rdbtnIsConfirm.SelectedIndex =0;
            txtMailNo.Text = TeacherCourseJudgmentManager[0]["MailNo"].ToString();
            txtDescription.Text = TeacherCourseJudgmentManager[0]["ViewPoint"].ToString();
        }
    }

    private void FillTeacherCourse(int TCrsId)
    {
        TSP.DataManager.TeachersCourseManager TeachersCourseManager = new TSP.DataManager.TeachersCourseManager();
        TeachersCourseManager.FindByCode(TCrsId);
        if (TeachersCourseManager.Count == 1)
        {
            cmbTeacherCourse.DataBind();
            cmbTeacherCourse.SelectedIndex = cmbTeacherCourse.Items.FindByValue(TeachersCourseManager[0]["CrsId"].ToString()).Index;
            txtTCrsDescription.Text = TeachersCourseManager[0]["Description"].ToString();
        }
        else
        {
            Response.Redirect("Teachers.aspx");
        }
    }

    #endregion
}
