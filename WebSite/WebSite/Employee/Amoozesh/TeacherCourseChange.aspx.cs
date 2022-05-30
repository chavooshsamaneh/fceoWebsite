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

public partial class Employee_Amoozesh_TeacherCourseChange : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {        if (string.IsNullOrEmpty(Request.QueryString["TCId"]) || string.IsNullOrEmpty(Request.QueryString["TeId"]))
        {
            Response.Redirect("Teachers.aspx");
            return;
        }
        if (!IsPostBack)
        {

            HiddenFieldTeacherCourse["TeId"] = Request.QueryString["TeId"];
            HiddenFieldTeacherCourse["TCId"] = Request.QueryString["TCId"];
            string TeId = Utility.DecryptQS(HiddenFieldTeacherCourse["TeId"].ToString());
            ObjdsTeacherCourse.SelectParameters[0].DefaultValue = TeId;
            TSP.DataManager.Permission per = TSP.DataManager.TeachersCourseManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            GridViewTeacherCourse.Visible = per.CanView;            
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
            this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
        }
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDisActive"] != null)
            this.btnDisActive.Enabled = this.btnDisActive.Enabled = (bool)this.ViewState["BtnDisActive"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Teachers.aspx");
    }

    protected void GridViewTeacherCourse_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
    }

    protected void GridViewTeacherCourse_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        InsertTeacherCourse(e);
    }

    protected void GridViewTeacherCourse_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

    }

    protected void GridViewTeacherCourseJudgment_BeforePerformDataSelect(object sender, EventArgs e)
    {

    }

    protected void GridViewTeacherCourse_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        TSP.DataManager.TeachersCourseManager TeachersCourseManager = new TSP.DataManager.TeachersCourseManager();
        int CrsId = int.Parse(e.NewValues["CrsId"].ToString());
        int TeId = int.Parse(Utility.DecryptQS(HiddenFieldTeacherCourse["TeId"].ToString()));
        int TCId = int.Parse(Utility.DecryptQS(HiddenFieldTeacherCourse["TCId"].ToString()));
        DataTable dtTCourse = TeachersCourseManager.SelecteachersCourseLastVesion(TeId, CrsId);
        if (dtTCourse.Rows.Count == 1)
        {
            int Type = int.Parse(dtTCourse.Rows[0]["Type"].ToString());
            int IsConfirm = int.Parse(dtTCourse.Rows[0]["IsConfirmed"].ToString());
            if (IsConfirm == 0)//******نامشخص
            {               
                e.RowError="درس انتخاب شده تکراری می باشد.";
            }
        }
    }

    protected void btnDisActive_Click(object sender, EventArgs e)
    {

    }

    protected void CallbackPanelDeActive_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (GridViewTeacherCourse.FocusedRowIndex>-1)
        {
            int TCrsId = -1;
            DataRow TCrsRow = GridViewTeacherCourse.GetDataRow(GridViewTeacherCourse.FocusedRowIndex);
            TCrsId = int.Parse(TCrsRow["TCrsId"].ToString());
            TeacherCourseDeActiveRequest(TCrsId);
        }
    }
    #endregion
 
    #region Methods
    private void InsertTeacherCourse(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TeacherCertificateManager TeacherCertificateManager = new TSP.DataManager.TeacherCertificateManager();
        TSP.DataManager.TeachersCourseManager TeachersCourseManager = new TSP.DataManager.TeachersCourseManager();
        TransactionManager.Add(TeacherCertificateManager);
        TransactionManager.Add(TeachersCourseManager);
        int CrsId = int.Parse(e.NewValues["CrsId"].ToString());
        int TeId = int.Parse(Utility.DecryptQS(HiddenFieldTeacherCourse["TeId"].ToString()));
        int TCId = int.Parse(Utility.DecryptQS(HiddenFieldTeacherCourse["TCId"].ToString()));
        try
        {
            TransactionManager.BeginSave();
            TeacherCertificateManager.FindByCode(TCId);
            if (TeacherCertificateManager.Count == 1)
            {
                DataRow TeacherCertificateRow = TeacherCertificateManager.NewRow();
                TeacherCertificateRow["Type"] = 2;
                TeacherCertificateRow["TeId"] = TeId;
                TeacherCertificateRow["FileNo"] = TeacherCertificateManager[0]["FileNo"].ToString();
                TeacherCertificateRow["SerialNo"] = TeacherCertificateManager[0]["SerialNo"].ToString();
                TeacherCertificateRow["StartDate"] = TeacherCertificateManager[0]["StartDate"].ToString();
                TeacherCertificateRow["EndDate"] = TeacherCertificateManager[0]["EndDate"].ToString();
                TeacherCertificateRow["UserId"] = Utility.GetCurrentUser_UserId();
                TeacherCertificateRow["ModifiedDate"] = DateTime.Now;

                TeacherCertificateManager.AddRow(TeacherCertificateRow);
                int cnt = TeacherCertificateManager.Save();
                if (cnt > 0)
                {

                    DataTable dtTCourse = TeachersCourseManager.SelecteachersCourseLastVesion(TeId, CrsId);
                    if (dtTCourse.Rows.Count == 1)
                    {
                        int Type = int.Parse(dtTCourse.Rows[0]["Type"].ToString());
                        int IsConfirm = int.Parse(dtTCourse.Rows[0]["IsConfirmed"].ToString());
                        if (IsConfirm == 0)//******نامشخص
                        {
                            TransactionManager.CancelSave();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "درس انتخاب شده تکراری می باشد.";
                        }
                        else//******تایید یا عدم تایید
                        {
                            DataRow TCourseRow = TeachersCourseManager.NewRow();
                            if ((HiddenFieldTeacherCourse["TeId"] == null) && (string.IsNullOrEmpty(HiddenFieldTeacherCourse["TeId"].ToString())))
                            {
                                Response.Redirect("Teachers.aspx");
                            }
                            TCourseRow["TeId"] = TeId;
                            TCourseRow["CrsId"] = e.NewValues["CrsId"];
                            TCourseRow["RequestDate"] = Utility.GetDateOfToday();
                            TCourseRow["Type"] = 1;
                            TCourseRow["IsConfirmed"] = 0;
                            TCourseRow["Description"] = e.NewValues["CrsId"];
                            TCourseRow["InActive"] = 0;
                            TCourseRow["UserId"] = Utility.GetCurrentUser_UserId();
                            TCourseRow["ModifiedDate"] = DateTime.Now;

                            TeachersCourseManager.AddRow(TCourseRow);
                            int cn = TeachersCourseManager.Save();
                            if (cn > 0)
                            {
                                TransactionManager.EndSave();
                                GridViewTeacherCourse.DataBind();
                                GridViewTeacherCourse.CancelEdit();
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "ذخیره انجام شد.";
                            }
                            else
                            {
                                TransactionManager.CancelSave();
                                GridViewTeacherCourse.CancelEdit();
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "خطایی در ذخیره انجام شد.";
                            }
                        }                      

                    }
                    else
                    {
                        DataRow TCourseRow = TeachersCourseManager.NewRow();
                        if ((HiddenFieldTeacherCourse["TeId"] == null) && (string.IsNullOrEmpty(HiddenFieldTeacherCourse["TeId"].ToString())))
                        {
                            Response.Redirect("Teachers.aspx");
                        }
                        TCourseRow["TeId"] = TeId;
                        TCourseRow["CrsId"] = e.NewValues["CrsId"];
                        TCourseRow["RequestDate"] = Utility.GetDateOfToday();
                        TCourseRow["Type"] = 0;
                        TCourseRow["IsConfirmed"] = 0;
                        TCourseRow["Description"] = e.NewValues["CrsId"];
                        TCourseRow["InActive"] = 0;
                        TCourseRow["UserId"] =Utility.GetCurrentUser_UserId();
                        TCourseRow["ModifiedDate"] = DateTime.Now;

                        TeachersCourseManager.AddRow(TCourseRow);
                        int cn = TeachersCourseManager.Save();
                        if (cn > 0)
                        {
                            TransactionManager.EndSave();
                            GridViewTeacherCourse.DataBind();
                            GridViewTeacherCourse.CancelEdit();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "ذخیره انجام شد.";
                        }
                        else
                        {
                            TransactionManager.CancelSave();
                            GridViewTeacherCourse.CancelEdit();
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در ذخیره انجام شد.";
                        }
                    }
                }
                else
                {
                    TransactionManager.CancelSave();
                    GridViewTeacherCourse.CancelEdit();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام شد.";
                }
            }
            else
            {
                TransactionManager.CancelSave();
                GridViewTeacherCourse.CancelEdit();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            GridViewTeacherCourse.CancelEdit();

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

    private void TeacherCourseDeActiveRequest(int TCrsId)
    {
        try
        {
            TSP.DataManager.TeachersCourseManager TeachersCourseManager = new TSP.DataManager.TeachersCourseManager();
            TeachersCourseManager.FindByCode(TCrsId);
            if (TeachersCourseManager.Count == 1)
            {
                DataRow TCourseRow = TeachersCourseManager.NewRow();
                int TeId = int.Parse(Utility.DecryptQS(HiddenFieldTeacherCourse["TeId"].ToString()));
                TCourseRow["TeId"] = TeId;
                TCourseRow["CrsId"] = int.Parse(TeachersCourseManager[0]["CrsId"].ToString());
                TCourseRow["RequestDate"] = Utility.GetDateOfToday();
                TCourseRow["Type"] = 2;
                TCourseRow["IsConfirmed"] = 0;
                TCourseRow["MailNo"] = txtMailNo.Text;
                TCourseRow["Description"] = TeachersCourseManager[0]["Description"].ToString();
                TCourseRow["InActive"] = 0;
                TCourseRow["UserId"] =Utility.GetCurrentUser_UserId();
                TCourseRow["ModifiedDate"] =DateTime.Now;

                TeachersCourseManager.AddRow(TCourseRow);
                int cn = TeachersCourseManager.Save();
                if (cn > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
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
    #endregion 

}
