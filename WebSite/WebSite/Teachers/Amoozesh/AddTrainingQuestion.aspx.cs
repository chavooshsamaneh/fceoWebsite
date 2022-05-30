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

public partial class Teachers_Amoozesh_AddTrainingQuestion : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            Session["TeQuFileUpload"] = null;

            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["TrQuId"]))
            {
                Response.Redirect("TrainingQuestions.aspx");
                return;
            }


            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                QuestionId.Value = Server.HtmlDecode(Request.QueryString["TrQuId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string TrQuId = Utility.DecryptQS(QuestionId.Value);

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            switch (PageMode)
            {
                case "View":
                    Disable();

                    if (string.IsNullOrEmpty(TrQuId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    lblFileNew.Visible = false;
                    flp.Visible = false;

                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    FillForm(int.Parse(TrQuId));
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    break;


                case "New":
                    Enable();
                    ASPxRoundPanel2.HeaderText = "جدید";
                    cmbCourse.DataBind();
                    cmbCourse.SelectedIndex = -1;
                    ClearForm();
                    break;

                case "Edit":
                    Enable();
                    HpLinkFile.Visible = true;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    if (string.IsNullOrEmpty(TrQuId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                        return;
                    }
                    FillForm(int.Parse(TrQuId));
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    break;
            }

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;


        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        lblFileNew.Visible = true;
        flp.Visible = true;
        QuestionId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
        Enable();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        lblFileNew.Visible = true;
        flp.Visible = true;
        HpLinkFile.Visible = true;

        string pageMode = Utility.DecryptQS(PgMode.Value);
        string TrQuId = Utility.DecryptQS(QuestionId.Value);

        if (string.IsNullOrEmpty(TrQuId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (string.IsNullOrEmpty(pageMode) && pageMode != "View")
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            else
            {
                Enable();
                TSP.DataManager.Permission per = TSP.DataManager.TrainingQuestionsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                this.ViewState["BtnSave"] = btnSave.Enabled;

                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";

            }

        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string TrQuId = Utility.DecryptQS(QuestionId.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        if (PageMode == "New")
        {
            Insert();
        }
        else if (PageMode == "Edit")
        {

            if (string.IsNullOrEmpty(TrQuId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            else
            {
                Edit(int.Parse(TrQuId));
            }

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["TeQuFileUpload"] = null;
        Response.Redirect("TrainingQuestions.aspx");

    }

    protected void flp_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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
    #endregion

    #region Methods
    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new System.IO.FileInfo(uploadedFile.PostedFile.FileName);
                ret = System.IO.Path.GetRandomFileName() + ImageType.Extension;
            } while (System.IO.File.Exists(MapPath("~/Image/Employee/Amoozesh/TrainingQuestions/") + ret) == true || System.IO.File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["TeQuFileUpload"] = tempFileName;
        }
        return ret;
    }

    protected void ClearForm()
    {
        for (int i = 0; i < ASPxRoundPanel2.Controls.Count; i++)
        {
            if (ASPxRoundPanel2.Controls[i] is DevExpress.Web.ASPxTextBox)
            {
                DevExpress.Web.ASPxTextBox co = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel2.Controls[i];
                co.Text = "";
            }

        }
        txtDesc.Text = "";
        txtQuText.Text = "";
        //    cmbCourse.DataBind();
        //   cmbCourse.SelectedIndex = -1;
        rdbAnswer.SelectedIndex = -1;
        rdbType.SelectedIndex = -1;
        rdbEnableView.Checked = false;
        Session["TeQuFileUpload"] = null;
    }

    protected void Enable()
    {
        txtAnsw1.Enabled = true;
        txtAnsw2.Enabled = true;
        txtAnsw3.Enabled = true;
        txtAnsw4.Enabled = true;
        txtDesc.Enabled = true;
        txtQuText.Enabled = true;
        cmbCourse.Enabled = true;
        rdbAnswer.Enabled = true;
        rdbEnableView.Enabled = true;
        rdbType.Enabled = true;
    }

    protected void Disable()
    {
        txtAnsw1.Enabled = false;
        txtAnsw2.Enabled = false;
        txtAnsw3.Enabled = false;
        txtAnsw4.Enabled = false;
        txtDesc.Enabled = false;
        txtQuText.Enabled = false;
        cmbCourse.Enabled = false;
        rdbAnswer.Enabled = false;
        rdbEnableView.Enabled = false;
        rdbType.Enabled = false;

    }

    protected void Insert()
    {

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TrainingQuestionsManager QuestionManager = new TSP.DataManager.TrainingQuestionsManager();
        TSP.DataManager.TrainingQuAnswersManager AnswerManager = new TSP.DataManager.TrainingQuAnswersManager();
        trans.Add(QuestionManager);
        trans.Add(AnswerManager);

        try
        {
            DataRow drQu = QuestionManager.NewRow();
            if (cmbCourse.Value != null)
                drQu["CrsId"] = cmbCourse.Value;
            drQu["QuText"] = txtQuText.Text;
            drQu["PkId"] = Utility.GetCurrentUser_MeId();
            drQu["UltId"] = 5;
            if (rdbType.SelectedIndex != -1)
                drQu["Type"] = rdbType.Value;
            else
                drQu["Type"] = DBNull.Value;
            drQu["EnableView"] = rdbEnableView.Checked;
            drQu["Description"] = txtDesc.Text;
            drQu["CreateDate"] = Utility.GetDateOfToday();
            drQu["UserId"] = Utility.GetCurrentUser_UserId();
            drQu["ModifiedDate"] = DateTime.Now;

            if (Session["TeQuFileUpload"] != null)
            {
                drQu["FileUrl"] = "~/Image/Employee/Amoozesh/TrainingQuestions/" + System.IO.Path.GetFileName(Session["TeQuFileUpload"].ToString());
            }

            QuestionManager.AddRow(drQu);
            trans.BeginSave();
            int cnt = QuestionManager.Save();

            if (cnt > 0)
            {
                int TrQuId = int.Parse(QuestionManager[0]["TrQuId"].ToString());

                if (Session["TeQuFileUpload"] != null)
                {
                    string ImgSoource = Server.MapPath("~/image/Temp/") + System.IO.Path.GetFileName(Session["TeQuFileUpload"].ToString());
                    string ImgTarget = Server.MapPath(QuestionManager[0]["FileUrl"].ToString());
                    System.IO.File.Copy(ImgSoource, ImgTarget, true);

                }



                DataRow drAns1 = AnswerManager.NewRow();
                drAns1["TrQuId"] = TrQuId;
                drAns1["Number"] = 1;
                drAns1["AnsText"] = txtAnsw1.Text;
                if (rdbAnswer.Value != null)
                    drAns1["AnswerNo"] = rdbAnswer.Value;
                drAns1["UserId"] = Utility.GetCurrentUser_UserId();
                drAns1["ModifiedDate"] = DateTime.Now;
                AnswerManager.AddRow(drAns1);

                DataRow drAns2 = AnswerManager.NewRow();
                drAns2["TrQuId"] = TrQuId;
                drAns2["Number"] = 2;
                drAns2["AnsText"] = txtAnsw2.Text;
                if (rdbAnswer.Value != null)
                    drAns2["AnswerNo"] = rdbAnswer.Value;
                drAns2["UserId"] = Utility.GetCurrentUser_UserId();
                drAns2["ModifiedDate"] = DateTime.Now;
                AnswerManager.AddRow(drAns2);

                DataRow drAns3 = AnswerManager.NewRow();
                drAns3["TrQuId"] = TrQuId;
                drAns3["Number"] = 3;
                drAns3["AnsText"] = txtAnsw3.Text;
                if (rdbAnswer.Value != null)
                    drAns3["AnswerNo"] = rdbAnswer.Value;
                drAns3["UserId"] = Utility.GetCurrentUser_UserId();
                drAns3["ModifiedDate"] = DateTime.Now;
                AnswerManager.AddRow(drAns3);


                DataRow drAns4 = AnswerManager.NewRow();
                drAns4["TrQuId"] = TrQuId;
                drAns4["Number"] = 4;
                drAns4["AnsText"] = txtAnsw4.Text;
                if (rdbAnswer.Value != null)
                    drAns4["AnswerNo"] = rdbAnswer.Value;
                drAns4["UserId"] = Utility.GetCurrentUser_UserId();
                drAns4["ModifiedDate"] = DateTime.Now;
                AnswerManager.AddRow(drAns4);

                if (AnswerManager.Save() > 0)
                {
                    QuestionId.Value = Utility.EncryptQS(AnswerManager[AnswerManager.Count - 1]["TrQuId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    trans.EndSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }


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

    protected void Edit(int TrQuId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TrainingQuestionsManager QuestionManager = new TSP.DataManager.TrainingQuestionsManager();
        TSP.DataManager.TrainingQuAnswersManager AnswerManager = new TSP.DataManager.TrainingQuAnswersManager();
        trans.Add(QuestionManager);
        trans.Add(AnswerManager);

        try
        {
            QuestionManager.FindByCode(TrQuId);
            QuestionManager[0].BeginEdit();
            if (cmbCourse.Value != null)
                QuestionManager[0]["CrsId"] = cmbCourse.Value;
            QuestionManager[0]["QuText"] = txtQuText.Text;

            if (rdbType.SelectedIndex != -1)
                QuestionManager[0]["Type"] = rdbType.Value;

            QuestionManager[0]["EnableView"] = rdbEnableView.Checked;
            QuestionManager[0]["Description"] = txtDesc.Text;

            QuestionManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            QuestionManager[0]["ModifiedDate"] = DateTime.Now;

            if (Session["QuFileUpload"] != null)
            {
                QuestionManager[0]["FileUrl"] = "~/Image/Employee/Amoozesh/TrainingQuestions/" + System.IO.Path.GetFileName(Session["QuFileUpload"].ToString());
            }
            QuestionManager[0].EndEdit();
            trans.BeginSave();

            QuestionManager.Save();

            if (Session["QuFileUpload"] != null)
            {
                string ImgSoource = Server.MapPath("~/image/Temp/") + System.IO.Path.GetFileName(Session["QuFileUpload"].ToString());
                string ImgTarget = Server.MapPath(QuestionManager[0]["FileUrl"].ToString());
                System.IO.File.Copy(ImgSoource, ImgTarget, true);

            }


            AnswerManager.FindByQuestionCode(TrQuId);
            AnswerManager[0].BeginEdit();
            AnswerManager[0]["AnsText"] = txtAnsw1.Text;
            if (rdbAnswer.Value != null)
                AnswerManager[0]["AnswerNo"] = rdbAnswer.Value;
            AnswerManager[0].EndEdit();

            AnswerManager[1].BeginEdit();
            AnswerManager[1]["AnsText"] = txtAnsw2.Text;
            if (rdbAnswer.Value != null)
                AnswerManager[1]["AnswerNo"] = rdbAnswer.Value;
            AnswerManager[1].EndEdit();

            AnswerManager[2].BeginEdit();
            AnswerManager[2]["AnsText"] = txtAnsw3.Text;
            if (rdbAnswer.Value != null)
                AnswerManager[2]["AnswerNo"] = rdbAnswer.Value;
            AnswerManager[2].EndEdit();

            AnswerManager[3].BeginEdit();
            AnswerManager[3]["AnsText"] = txtAnsw4.Text;
            if (rdbAnswer.Value != null)
                AnswerManager[3]["AnswerNo"] = rdbAnswer.Value;
            AnswerManager[3].EndEdit();

            AnswerManager.Save();

            trans.EndSave();

            QuestionId.Value = Utility.EncryptQS(QuestionManager[0]["TrQuId"].ToString());
            PgMode.Value = Utility.EncryptQS("Edit");
            ASPxRoundPanel2.HeaderText = "ویرایش";
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

    protected void FillForm(int TrQuId)
    {
        TSP.DataManager.TrainingQuestionsManager QuestionManager = new TSP.DataManager.TrainingQuestionsManager();
        TSP.DataManager.TrainingQuAnswersManager AnswerManager = new TSP.DataManager.TrainingQuAnswersManager();

        try
        {
            QuestionManager.FindByCode(TrQuId);
            if (QuestionManager.Count == 1)
            {
                txtDesc.Text = QuestionManager[0]["Description"].ToString();
                txtQuText.Text = QuestionManager[0]["QuText"].ToString();
                cmbCourse.DataBind();
                cmbCourse.SelectedIndex = cmbCourse.Items.IndexOfValue(QuestionManager[0]["CrsId"].ToString());
                if (!string.IsNullOrEmpty(QuestionManager[0]["Type"].ToString()))
                    rdbType.Value = QuestionManager[0]["Type"].ToString();
                rdbEnableView.Checked = Convert.ToBoolean(QuestionManager[0]["EnableView"]);
                if (!string.IsNullOrEmpty(QuestionManager[0]["FileUrl"].ToString()))
                    HpLinkFile.NavigateUrl = QuestionManager[0]["FileUrl"].ToString();

                AnswerManager.FindByQuestionCode(TrQuId);
                if (AnswerManager.Count > 0)
                {
                    txtAnsw1.Text = AnswerManager[0]["AnsText"].ToString();
                    txtAnsw2.Text = AnswerManager[1]["AnsText"].ToString();
                    txtAnsw3.Text = AnswerManager[2]["AnsText"].ToString();
                    txtAnsw4.Text = AnswerManager[3]["AnsText"].ToString();
                    rdbAnswer.SelectedIndex = rdbAnswer.Items.IndexOfValue(AnswerManager[0]["AnswerNo"].ToString());

                }

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";
                return;
            }

        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
    }
    #endregion
}
