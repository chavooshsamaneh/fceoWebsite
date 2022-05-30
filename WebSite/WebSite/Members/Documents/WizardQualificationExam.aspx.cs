using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DevExpress.Web;
using System.Data;
using System.Collections;

public partial class Members_Documents_WizardQualificationExam : System.Web.UI.Page
{
    DataTable dtExamDetail = null;
    int _TTypeId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldExam["TTypeId"]);
        }
        set
        {
            HiddenFieldExam["TTypeId"] = value;
        }
    }
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ObjdsMajor.SelectParameters["MeId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
        SetWarningLableDisable();
        CheckTsTimeOut();
        HpflpConfAttach.ClientVisible = false;

        if (!IsPostBack)
        {
            HiddenFieldDocMemberFile["name"] = 0;
            HiddenFieldDocMemberFile["PeriodImg"] = 0;
            cmbGrade.DataBind();
            cmbGrade.SelectedIndex = cmbGrade.Items.FindByValue(((int)TSP.DataManager.DocumentGrads.Grade3).ToString()).Index;
            Session["ExamFileURL"] = Session["ImplementPeriodFileURL"] = null;
            SetMenueImage();
            CreateExamDataTable();
            ObjdsTestCondition.SelectParameters["MjId"].DefaultValue = "-2";
            if (Session["WizardDocQualificationExam"] != null)
            {
                dtExamDetail = (DataTable)Session["WizardDocQualificationExam"];
                GridViewExam.DataSource = dtExamDetail;
                GridViewExam.DataBind();
            }
        }

    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {

            if (Session["WizardDocQualificationExam"] == null)
            {
                SetMessage("اطلاعات آزمون ثبت نشده است.");
                return;
            }
            if (Session["WizardDocQualificationExam"] != null && !(((DataTable)Session["WizardDocQualificationExam"]).Rows.Count > 0))
            {
                SetMessage("ورود اطلاعات آزمون اجباری می باشد.");
                return;
            }
            Response.Redirect("WizardQualificationKardan.aspx");
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            return;
        }
    }


    #region Exam
    protected void cmbMajor_SelectedIndexChanged(object sender, EventArgs e)
    {
        string MjId = "";
        cmbMajor.DataBind();
        if (cmbMajor.SelectedItem != null)
        {
            MjId = cmbMajor.SelectedItem.Value.ToString();
        }
        ObjdsTestCondition.SelectParameters[1].DefaultValue = MjId;

        ObjectDataSourceTestType.SelectParameters["TCondId"].DefaultValue = "-2";
        cmbTestType.DataBind();
        ComboTestCondition.SelectedIndex = -1;
    }
    protected void ComboTestCondition_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ComboTestCondition.SelectedItem == null || ComboTestCondition.SelectedItem.Value == null)
            return;
        int TCondId = Convert.ToInt32(ComboTestCondition.SelectedItem.Value);
        ObjectDataSourceTestType.SelectParameters["TCondId"].DefaultValue = TCondId.ToString();
        cmbTestType.DataBind();
        cmbTestType.SelectedIndex = -1;
    }
    protected void cmbTestType_SelectedIndexChanged(object sender, EventArgs e)
    {
        _TTypeId = -2;
        if (cmbTestType.SelectedItem != null && cmbTestType.SelectedItem.Value != null)
        {
            int TCondDId = Convert.ToInt32(cmbTestType.SelectedItem.Value);
            TSP.DataManager.DocTestConditionDetailManager DocTestConditionDetailManager = new TSP.DataManager.DocTestConditionDetailManager();
            DocTestConditionDetailManager.FindByCode(TCondDId);
            if (DocTestConditionDetailManager.Count == 1)
            {
                lblPeriod.ClientVisible = PanelPeriodImg.ClientVisible = Convert.ToBoolean(DocTestConditionDetailManager[0]["NeedFileUpload"]);
                _TTypeId = Convert.ToInt32(DocTestConditionDetailManager[0]["TTypeId"]);
            }
        }
        else
        {
            PanelPeriodImg.ClientVisible = false;
        }
    }

    protected void GridViewExam_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        GridViewExam.JSProperties["cpSaveComplete"] = "0";
        GridViewExam.JSProperties["cpMessage"] = "";

        if (e.Parameters == "Add")
        {
            try
            {
                RowInserting();
                cmbGrade.DataBind();
                cmbGrade.SelectedIndex = cmbGrade.Items.FindByValue(((int)TSP.DataManager.DocumentGrads.Grade3).ToString()).Index;
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                if (Utility.ShowExceptionError())
                    GridViewExam.JSProperties["cpMessage"] = "خطایی در اضافه کردن رخ داده است" + err.Message;
                else
                    GridViewExam.JSProperties["cpMessage"] = "خطایی در اضافه کردن رخ داده است";
            }
        }
    }

    protected void GridViewTestCondition_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;
        try
        {
            GridViewExam.JSProperties["cpSaveComplete"] = "0";
            GridViewExam.JSProperties["cpMessage"] = "";
            GridViewExam.DataSource = (DataTable)Session["WizardDocQualificationExam"];

            if (Session["WizardDocQualificationExam"] == null)
            {
                GridViewExam.JSProperties["cpMessage"] = Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SessionHasBeenExpired);
                return;
            }
            if (GridViewExam.FocusedRowIndex <= -1)
            {
                GridViewExam.JSProperties["cpMessage"] = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
                return;
            }
            dtExamDetail = (DataTable)Session["WizardDocQualificationExam"];
            dtExamDetail.Rows.Find(e.Keys["Id"]).Delete();
            Session["WizardDocQualificationExam"] = dtExamDetail;
            GridViewExam.DataSource = dtExamDetail;
            GridViewExam.DataBind();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            GridViewExam.JSProperties["cpMessage"] = "خطایی در حذف ایجاد شد";
        }
    }

    protected void flpConfAttach_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageConfAttach(e.UploadedFile, "Exam");
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void UploadControlPeriodImgURL_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            // if (Convert.ToInt32(HiddenFieldExam["TCondId"]) > 84)
            //if (Convert.ToInt32(ViewState["TCondId"]) > 84)
            e.CallbackData = SaveImageConfAttach(e.UploadedFile, "ImplementPeriod");
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    #endregion

    #endregion

    #region Methods
    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    private void SetWarningLableDisable()
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }
    private Boolean CheckTsTimeOut()
    {
        if (Session["WizardDocQualificationExam"] == null && Session["WizardDocQualificationSummary"] == null && Session["WizardDocQualificationOath"] == null
         && Session["WizardQualificationJobConfirm"] == null
                 )
        {
            SetMessage("مدت زمان اعتبار صفحه به پایان رسیده است");
            return true;
        }

        if (Session["WizardDocQualificationOath"] == null)
        {
            SetMessage("سوگند نامه و تعهدات را تایید ننموده اید");
            return true;
        }
        return false;
    }

    #region Exam

    private void SetMenueImage()
    {
        if (Session["WizardDocQualificationOath"] != null && (Boolean)Session["WizardDocQualificationOath"] == true)
        {
            MenuSteps.Items.FindByName("Oath").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Oath").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Oath").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardDocQualificationExam"] != null && ((DataTable)Session["WizardDocQualificationExam"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("Exams").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Exams").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Exams").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardQualificationJobConfirm"] != null && ((DataTable)Session["WizardQualificationJobConfirm"]).Rows.Count > 0)
        {
            MenuSteps.Items.FindByName("JobConfirm").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("JobConfirm").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("JobConfirm").Image.Height = Unit.Pixel(15);
        }

        if (Session["WizardDocQualificationSummary"] != null && (Boolean)Session["WizardDocQualificationSummary"] == true)
        {
            MenuSteps.Items.FindByName("Summary").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Summary").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Summary").Image.Height = Unit.Pixel(15);
        }
        if (Session["WizardQualificationImgfrontDoc"] != null)
        {
            MenuSteps.Items.FindByName("Kardan").Image.Url = "~/Images/icons/button_ok.png";
            MenuSteps.Items.FindByName("Kardan").Image.Width = Unit.Pixel(15);
            MenuSteps.Items.FindByName("Kardan").Image.Height = Unit.Pixel(15);
        }


    }

    private void CreateExamDataTable()
    {
        if (Session["WizardDocQualificationExam"] == null)
        {
            dtExamDetail = new DataTable();
            dtExamDetail.Columns.Add("Id");
            dtExamDetail.Columns["Id"].AutoIncrement = true;
            dtExamDetail.Columns["Id"].AutoIncrementSeed = 1;
            dtExamDetail.Constraints.Add("PK_ID", dtExamDetail.Columns["Id"], true);
            dtExamDetail.Columns.Add("Point");
            dtExamDetail.Columns.Add("TTypeName");
            dtExamDetail.Columns.Add("GrdName");
            dtExamDetail.Columns.Add("TTypeId");
            dtExamDetail.Columns.Add("GrdId");
            dtExamDetail.Columns.Add("MajorId");
            dtExamDetail.Columns.Add("MajorName");
            dtExamDetail.Columns.Add("TCondId");
            dtExamDetail.Columns.Add("ExamTitle");
            dtExamDetail.Columns.Add("EntranceCode");
            dtExamDetail.Columns.Add("UserCode");
            dtExamDetail.Columns.Add("FileURL");
            dtExamDetail.Columns.Add("PeriodImgURL");
            dtExamDetail.Columns.Add("NeedFileUpload");

            Session["WizardDocQualificationExam"] = dtExamDetail;
        }
        else
            dtExamDetail = (DataTable)Session["WizardDocQualificationExam"];

        GridViewExam.DataSource = dtExamDetail;
        GridViewExam.DataBind();
    }

    protected string SaveImageConfAttach(UploadedFile uploadedFile, string RequestType)
    {
        string ret = "";

        if (uploadedFile.IsValid)
        {
            switch (RequestType)
            {
                case "Exam":
                    do
                    {
                        System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                        ret = "MeId_" + Utility.GetCurrentUser_MeId().ToString() + "_" + Path.GetRandomFileName() + ImageType.Extension;

                    } while (File.Exists(MapPath("~/image/DocMeFile/Exams/") + ret) == true);
                    string tempFileName = MapPath("~/image/DocMeFile/Exams/") + ret;
                    uploadedFile.SaveAs(tempFileName, true);
                    Session["ExamFileURL"] = "~/image/DocMeFile/Exams/" + ret;
                    break;
                case "ImplementPeriod":
                    do
                    {
                        System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                        ret = "MeId_" + Utility.GetCurrentUser_MeId().ToString() + "_" + Path.GetRandomFileName() + ImageType.Extension;

                    } while (File.Exists(MapPath("~/image/DocMeFile/ImplementPeriod/") + ret) == true);
                    string tempFileName1 = MapPath("~/image/DocMeFile/ImplementPeriod/") + ret;
                    uploadedFile.SaveAs(tempFileName1, true);
                    Session["ImplementPeriodFileURL"] = "~/image/DocMeFile/ImplementPeriod/" + ret;
                    break;
            }

        }
        return ret;
        //
    }

    private void RowInserting()
    {
        try
        {
            if (cmbTestType.Value == null)
            {
                GridViewExam.JSProperties["cpMessage"] = "زمینه آزمون را انتخاب نمایید";
                return;
            }

            if (Session["WizardDocQualificationExam"] != null)
            {
                dtExamDetail = (DataTable)Session["WizardDocQualificationExam"];

                TSP.DataManager.DocTestConditionDetailManager DocTestConditionDetailManager = new TSP.DataManager.DocTestConditionDetailManager();
                DocTestConditionDetailManager.FindByCode(Convert.ToInt32(cmbTestType.Value));
                Boolean NeedFileUpload = false;
                int TTypeId = -2;
                if (DocTestConditionDetailManager.Count == 0)
                {
                    GridViewExam.JSProperties["cpMessage"] = "خطا در بازخوانی اطلاعات آزمون ایجاد شده است.";
                    return;
                }
                NeedFileUpload = Convert.ToBoolean(DocTestConditionDetailManager[0]["NeedFileUpload"]);
                TTypeId = Convert.ToInt32(DocTestConditionDetailManager[0]["TTypeId"]);
                if (DocTestConditionDetailManager.Count > 0 && Convert.ToBoolean(DocTestConditionDetailManager[0]["NeedFileUpload"]) && Session["ImplementPeriodFileURL"] == null)
                {
                    GridViewExam.JSProperties["cpMessage"] = "آپلود تصویر گواهینامه آموزشی اجباری می باشد.";
                    return;
                }
                dtExamDetail.DefaultView.RowFilter = "TCondId=" + ComboTestCondition.SelectedItem.Value.ToString() + " and TTypeId=" + TTypeId.ToString();
                if (dtExamDetail.DefaultView.Count > 0)
                {
                    GridViewExam.JSProperties["cpMessage"] = "آزمون و زمینه آزمون انتخاب شده تکراری می باشد.";

                    dtExamDetail.DefaultView.RowFilter = "";
                    return;
                }


                dtExamDetail.DefaultView.RowFilter = "";

                DataRow dr = dtExamDetail.NewRow();

                dr["Point"] = txtPoint.Text;
                cmbTestType.DataBind();
                dr["TTypeName"] = cmbTestType.Text;
                dr["TTypeId"] = TTypeId;
                dr["GrdName"] = cmbGrade.Text;
                if (cmbGrade.Value != null)
                    dr["GrdId"] = cmbGrade.Value;
                dr["TCondId"] = Convert.ToInt32(ComboTestCondition.SelectedItem.Value);
                dr["ExamTitle"] =ComboTestCondition.SelectedItem.Text.ToString();
                dr["EntranceCode"] = txtEntrantCode.Text;
                dr["UserCode"] = txtUserCode.Text;
                dr["MajorName"] = cmbMajor.Text;
                dr["MajorId"] = cmbMajor.Value;
                if (Session["ExamFileURL"] != null)
                {
                    dr["FileURL"] = Session["ExamFileURL"].ToString();
                    Session.Remove("ExamFileURL");
                }
                if (Session["ImplementPeriodFileURL"] != null)
                {
                    dr["PeriodImgURL"] = Session["ImplementPeriodFileURL"].ToString();
                    Session.Remove("ImplementPeriodFileURL");
                }
                dtExamDetail.Rows.Add(dr);
                Session["WizardDocQualificationExam"] = dtExamDetail;
                GridViewExam.DataSource = dtExamDetail;
                GridViewExam.DataBind();
                GridViewExam.JSProperties["cpSaveComplete"] = "1";
                Session["ExamFileURL"] = Session["ImplementPeriodFileURL"] = null;


            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            if (Utility.ShowExceptionError())
                GridViewExam.JSProperties["cpMessage"] = "خطایی در اضافه کردن رخ داده است" + err.Message;
            else
                GridViewExam.JSProperties["cpMessage"] = "خطایی در اضافه کردن رخ داده است";
        }
    }

    private Boolean CbIAgree()
    {
        if (Session["WizardQualificationchbIAgree"] != null)
        {
            if (Convert.ToBoolean(Session["WizardQualificationchbIAgree"]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    #endregion
    #endregion
}