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
using System.IO;
using DevExpress.Web;

public partial class Employee_Amoozesh_TrainingQuestions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TrainingQuestionsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            GridViewQuestions.Visible = per.CanView;

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

            GridViewQuestions.JSProperties["cpReqType"] = "";
            GridViewQuestions.JSProperties["cpReqValue"] = "";

        }
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddTrainingQuestion.aspx?TrQuId=" + Utility.EncryptQS("") + "&PageMode=" + Utility.EncryptQS("New"));

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int TrQuId = -1;
        if (GridViewQuestions.FocusedRowIndex > -1)
        {
            DataRow row = GridViewQuestions.GetDataRow(GridViewQuestions.FocusedRowIndex);
            TrQuId = (int)row["TrQuId"];
        }
        if (TrQuId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("AddTrainingQuestion.aspx?TrQuId=" + Utility.EncryptQS(TrQuId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AmoozeshHome.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int TrQuId = -1;
        if (GridViewQuestions.FocusedRowIndex > -1)
        {
            DataRow row = GridViewQuestions.GetDataRow(GridViewQuestions.FocusedRowIndex);
            TrQuId = (int)row["TrQuId"];
        }
        if (TrQuId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Delete(TrQuId);
        }
    }    

    protected void btnView_Click(object sender, EventArgs e)
    {
        int TrQuId = -1;
        if (GridViewQuestions.FocusedRowIndex > -1)
        {
            DataRow row = GridViewQuestions.GetDataRow(GridViewQuestions.FocusedRowIndex);
            TrQuId = (int)row["TrQuId"];
        }
        if (TrQuId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("AddTrainingQuestion.aspx?TrQuId=" + Utility.EncryptQS(TrQuId.ToString()) + "&PageMode=" + Utility.EncryptQS("View"));
        }

    }

    protected void GridViewQuestions_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr";

    }

    protected void GridViewQuestions_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";
    }

    protected void GridViewQuestions_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        GridViewQuestions.JSProperties["cpPrint"] = 0;
        GridViewQuestions.JSProperties["cpReqType"] = e.Parameters;
        if (e.Parameters == "Print")
        {
            GridViewQuestions.JSProperties["cpPrint"] = 1;
            GridViewQuestions.JSProperties["cpReqValue"] = "../../ReportForms/Amoozesh/TrainingQuestionsReport.aspx?Qs=" + Utility.EncryptQS(GridViewQuestions.FilterExpression);
        }
    }

    protected void Delete(int TrQuId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TrainingQuestionsManager QuestionManager = new TSP.DataManager.TrainingQuestionsManager();
        TSP.DataManager.TrainingQuAnswersManager AnswerManager = new TSP.DataManager.TrainingQuAnswersManager();
        trans.Add(QuestionManager);
        trans.Add(AnswerManager);

        try
        {
            QuestionManager.FindByCode(TrQuId);
            if (QuestionManager.Count == 1)
            {
                string url = "";

                if (!string.IsNullOrEmpty(QuestionManager[0]["FileUrl"].ToString()))
                    url = QuestionManager[0]["FileUrl"].ToString();

                trans.BeginSave();

                AnswerManager.FindByQuestionCode(TrQuId);
                if (AnswerManager.Count > 0)
                {
                    int c = AnswerManager.Count;
                    for (int i = 0; i < c; i++)
                        AnswerManager[0].Delete();

                    AnswerManager.Save();
                }

                QuestionManager[0].Delete();

                int cn = QuestionManager.Save();
                if (cn == 1)
                {
                    trans.EndSave();

                    if ((!string.IsNullOrEmpty(url)) && (File.Exists(Server.MapPath(url))))
                        File.Delete(Server.MapPath(url));


                    GridViewQuestions.DataBind();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "حذف انجام شد";

                }
                else
                {
                    trans.CancelSave();

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                }

            }
        }
        catch (Exception err)
        {
            trans.CancelSave();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 547)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
            }
        }



    }
}
