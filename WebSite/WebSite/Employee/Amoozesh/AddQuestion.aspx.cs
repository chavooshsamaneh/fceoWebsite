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

public partial class Employee_Amoozesh_AddQuestion : System.Web.UI.Page
{
    DataTable dtOfQu = new DataTable();
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        txtLetterNo.Attributes["onkeyup"] = "return ltr_override(event)";

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");


        if (!IsPostBack)
        {
            Session["question"] = null;

            TSP.DataManager.Permission per = TSP.DataManager.QuestionsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;


            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                HDQuId.Value = Server.HtmlDecode(Request.QueryString["QuSetId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string QuSetId = Utility.DecryptQS(HDQuId.Value);


            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            switch (PageMode)
            {
                case "View":
                    SetViewMode();


                    if (string.IsNullOrEmpty(QuSetId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    btnEdit.Enabled = per.CanEdit;
                    btnEdit2.Enabled = per.CanEdit;


                    FillForm(int.Parse(QuSetId));
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    break;


                case "New":
                    Enable();
                    //if (!this.Page.IsStartupScriptRegistered("Key"))
                    //    this.Page.RegisterStartupScript("Key", "<script>SetPeriod();</script>");


                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    ASPxRoundPanel2.HeaderText = "جدید";
                    lblStatus.Visible = false;
                    txtStatus.Visible = false;

                    TSP.DataManager.QuestionsManager Manager = new TSP.DataManager.QuestionsManager();
                    Session["question"] = Manager;
                    CustomAspxDevGridView1.DataSource = Manager.DataTable;
                    CustomAspxDevGridView1.DataBind();

                    ClearForm();
                    break;
                case "Edit":
                    Enable();

                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;

                    if (string.IsNullOrEmpty(QuSetId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    FillForm(int.Parse(QuSetId));
                    ASPxRoundPanel2.Enabled = true;
                    ASPxRoundPanel2.HeaderText = "ویرایش";

                    break;

            }

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;


        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        btnBack.PostBackUrl = "Questions.aspx";
        btnBack2.PostBackUrl = "Questions.aspx";
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {

        TSP.DataManager.QuestionsManager Manager = new TSP.DataManager.QuestionsManager();
        Session["question"] = Manager;
        CustomAspxDevGridView1.DataSource = Manager.DataTable;
        CustomAspxDevGridView1.DataBind();

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;


        HDQuId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";

        //CmbQucode.Visible = false;
        //lblCodeView.Visible = false;
        //lblCodeNew.Visible = true;
        //txtQuCode.Visible = true;
        txtStatus.Visible = false;
        lblStatus.Visible = false;

        Enable();
        ClearForm();
        TSP.DataManager.Permission per = TSP.DataManager.QuestionsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string QuSetId = Utility.DecryptQS(HDQuId.Value);
        //string QuCode = CmbQucode.Text;
        if (!string.IsNullOrEmpty(QuSetId))
        {
            TSP.DataManager.QuestionSetManager QuManager = new TSP.DataManager.QuestionSetManager();
            //QuManager.FindByQuCode(QuCode);
            QuManager.FindByCode(int.Parse(QuSetId));
            if (Convert.ToBoolean(QuManager[0]["InActive"]) == true)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان ویرایش برای سری سؤالات غیر فعال وجود ندارد";
                return;
            }
            else
            {
                //txtQuCode.Visible = false;
                //CmbQucode.Visible = true;
                //txtStatus.Visible = false;
                //lblStatus.Visible = false;

                Enable();

                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";

                TSP.DataManager.QuestionsManager Manager = (TSP.DataManager.QuestionsManager)Session["question"];
                CustomAspxDevGridView1.DataSource = Manager.DataTable;
                CustomAspxDevGridView1.DataBind();
                // Load_Grid();
                //CustomAspxDevGridView1.DataSource = (DataTable)Session["question"];
                //CustomAspxDevGridView1.DataBind();

                TSP.DataManager.Permission per = TSP.DataManager.QuestionsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;

                this.ViewState["BtnSave"] = btnSave.Enabled;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;

            }
        }

        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "برای ویرایش اطلاعات ابتدا یک سری از سؤالات را انتخاب نمایید";
        }


    }


    protected void CustomAspxDevGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        DateTime dt = new DateTime();
        dt = DateTime.Now;
        System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
        string PerDate = string.Format("{0}/{1}/{2}", pDate.GetYear(dt).ToString().PadLeft(4, '0'), pDate.GetMonth(dt).ToString().PadLeft(2, '0'), pDate.GetDayOfMonth(dt).ToString().PadLeft(2, '0'));

        // DataTable dt = (DataTable)Session["question"];
        TSP.DataManager.QuestionsManager QuManager = (TSP.DataManager.QuestionsManager)Session["question"];
        DataRow dr = QuManager.NewRow();

        try
        {
            dr["QuNo"] = e.NewValues["QuNo"];
            dr["Question"] = e.NewValues["Question"];

            dr["CreateDate"] = PerDate;
            if (txtQuCode.Visible)
                dr["QuCode"] = txtQuCode.Text;
            else
                dr["QuCode"] = CmbQucode.Text;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            QuManager.AddRow(dr);

            CustomAspxDevGridView1.CancelEdit();

            CustomAspxDevGridView1.DataSource = QuManager.DataTable;
            CustomAspxDevGridView1.DataBind();
            // Load_Grid();

        }
        catch (Exception err)
        {
            CustomAspxDevGridView1.CancelEdit();

            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
        }


        e.Cancel = true;
    }
    protected void CustomAspxDevGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        TSP.DataManager.QuestionsManager QuManager = (TSP.DataManager.QuestionsManager)Session["question"];

        CustomAspxDevGridView1.DataSource = QuManager.DataTable;
        CustomAspxDevGridView1.DataBind();

        int Id = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            Id = CustomAspxDevGridView1.FocusedRowIndex;
        }
        if (Id == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            return;

        }
        else
        {

            QuManager.DataTable.Rows[Id].Delete();
            // Session["question"] = QuManager;
            // Load_Grid();
            CustomAspxDevGridView1.DataSource = QuManager.DataTable;
            CustomAspxDevGridView1.DataBind();


        }
    }
    protected void CustomAspxDevGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        TSP.DataManager.QuestionsManager QuManager = (TSP.DataManager.QuestionsManager)Session["question"];

        try
        {
            DataRow dr = QuManager.DataTable.Rows.Find(e.Keys[0]);
            dr.BeginEdit();
            dr["QuNo"] = e.NewValues["QuNo"];
            dr["Question"] = e.NewValues["Question"];
            dr.EndEdit();
            CustomAspxDevGridView1.CancelEdit();
            //  Load_Grid();
            CustomAspxDevGridView1.DataSource = QuManager.DataTable;
            CustomAspxDevGridView1.DataBind();

        }
        catch (Exception err)
        {
            CustomAspxDevGridView1.CancelEdit();

            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
        }

        e.Cancel = true;
    }

    protected void CmbQucode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //TSP.DataManager.QuestionsManager Manager = new TSP.DataManager.QuestionsManager();
            //FillForm();

            //**********************//
            //Manager.FindByQuCode(CmbQucode.Text);
            //txtStatus.Text = Manager[0]["ActiveName"].ToString();
            //Session["question"] = Manager;
            //CustomAspxDevGridView1.DataSource = Manager.DataTable;
            //CustomAspxDevGridView1.DataBind();

            //string QuCode = CmbQucode.Text;
            //Session["question"] = Manager.FindByQuCode(QuCode);
            //CustomAspxDevGridView1.DataSource = (DataTable)Session["question"];
            //CustomAspxDevGridView1.DataBind();
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
    }
    protected void CustomAspxDevGridView1_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        //string PageMode = Utility.DecryptQS(PgMode.Value);
        //if (PageMode == "View")
        //{
        //    if (e.Button.ButtonType == DevExpress.Web.ColumnCommandButtonType.New)
        //        e.Visible = false;
        //    if (e.Button.ButtonType == DevExpress.Web.ColumnCommandButtonType.Edit)
        //        e.Visible = false;
        //    if (e.Button.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
        //        e.Visible = false;
        //}
        //else if (PageMode == "New" || PageMode == "Edit")
        //{
        //    if (e.Button.ButtonType == DevExpress.Web.ColumnCommandButtonType.New)
        //        e.Visible = true;
        //    if (e.Button.ButtonType == DevExpress.Web.ColumnCommandButtonType.Edit)
        //        e.Visible = true;
        //    if (e.Button.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
        //        e.Visible = true;
        //}
    }
    protected void Load_Grid()
    {
        //TSP.DataManager.QuestionsManager Manager = new TSP.DataManager.QuestionsManager();
        //string QuCode = CmbQucode.Text;
        //Manager.FindByQuCode(QuCode);
        //Session["question"] = Manager;
        //CustomAspxDevGridView1.DataSource = (TSP.DataManager.QuestionsManager)Session["question"];
        //CustomAspxDevGridView1.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CustomAspxDevGridView1.VisibleRowCount == 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات برای ذخیره کامل نمی باشد";
            return;
        }
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string QuSetId = Utility.DecryptQS(HDQuId.Value);


        //string QuCode = CmbQucode.Text;


        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {

            if (PageMode == "New")
            {

                Insert();


            }
            else if (PageMode == "Edit")
            {
                if (string.IsNullOrEmpty(QuSetId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;

                }
                else
                    Edit(int.Parse(QuSetId));


            }


        }
    }

    protected void CallbackPanelReq_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        string[] Parameters = e.Parameter.Split(';');
        string LetterNo = Parameters[1];
        string ReType = Parameters[0];
        switch (ReType)
        {
            case "CheckLetter":
                if (CheckLetterValidationAndFill(LetterNo) < 0)
                {
                    lblErrorMail.ClientVisible = true;
                    txtLetterDate.Text = "";
                    txtMailTitle.Text = "";
                }
                else
                    lblErrorMail.ClientVisible = false;

                break;
        }
    }

    #endregion

    #region Methods
    protected void SetViewMode()
    {
        // TSP.DataManager.QuestionsManager Manager = new TSP.DataManager.QuestionsManager();

        btnSave.Enabled = false;
        btnSave2.Enabled = false;

        this.ViewState["BtnSave"] = btnSave.Enabled;

        PgMode.Value = Utility.EncryptQS("View");
        //txtQuCode.Visible = false;
        //lblCodeNew.Visible = false;

        Disable();
        //CmbQucode.DataBind();
        //CmbQucode.SelectedIndex = 0;
        //FillForm();
    }
    protected void Insert()
    {
        DateTime dt = new DateTime();
        dt = DateTime.Now;
        System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
        string PerDate = string.Format("{0}/{1}/{2}", pDate.GetYear(dt).ToString().PadLeft(4, '0'), pDate.GetMonth(dt).ToString().PadLeft(2, '0'), pDate.GetDayOfMonth(dt).ToString().PadLeft(2, '0'));

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.QuestionSetManager SetManager = new TSP.DataManager.QuestionSetManager();
        TSP.DataManager.QuestionsManager QuManager = (TSP.DataManager.QuestionsManager)Session["question"];
        TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager = new TSP.DataManager.Automation.LetterRelatedTablesManager();
        trans.Add(SetManager);
        trans.Add(QuManager);
        trans.Add(LetterRelatedTablesManager);

        try
        {
            trans.BeginSave();
            int cnt = QuManager.Save();
            if (cnt > 0)
            {
                DataRow drSetQ = SetManager.NewRow();
                drSetQ["QuCode"] = txtQuCode.Text;
                if (!string.IsNullOrEmpty(txtSessionNo.Text))
                    drSetQ["SessionNo"] = txtSessionNo.Text;
                else
                    drSetQ["SessionNo"] = DBNull.Value;
                if (!string.IsNullOrEmpty(txtSessionDate.Text))
                    drSetQ["SessionDate"] = txtSessionDate.Text;
                else
                    drSetQ["SessionDate"] = DBNull.Value;
                drSetQ["LetterNo"] = txtLetterNo.Text;
                drSetQ["LetterDate"] = txtLetterDate.Text;
                drSetQ["CreateDate"] = PerDate;
                drSetQ["Description"] = txtDesc.Text;
                drSetQ["QuDesigner"] = txtDesigner.Text;
                if (!string.IsNullOrEmpty(txtEditNo.Text))
                    drSetQ["EditNo"] = txtEditNo.Text;
                else
                    drSetQ["EditNo"] = DBNull.Value;
                drSetQ["UserId"] = Utility.GetCurrentUser_UserId();
                drSetQ["InActive"] = 0;
                drSetQ["ModifiedDate"] = DateTime.Now;

                SetManager.AddRow(drSetQ);
                SetManager.Save();
                int LetterId = CheckLetterValidationAndFill(txtLetterNo.Text);
                if (LetterId < -1)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "شماره نامه معتبر نمی باشد.";
                    return;
                }
                InsertLetterRelatedTables(LetterRelatedTablesManager, LetterId, int.Parse(SetManager[0]["QuSetId"].ToString()), (int)TSP.DataManager.TableCodes.QuestionSet);
                txtLetterNo.Enabled = false;
                HiddenFieldLetter["LetterNo"] = txtLetterNo.Text;
                trans.EndSave();

                QuManager.DataTable.AcceptChanges();
                ASPxRoundPanel2.HeaderText = "ویرایش";
                PgMode.Value = Utility.EncryptQS("Edit");
                HDQuId.Value = Utility.EncryptQS(SetManager[0]["QuSetId"].ToString());

                CustomAspxDevGridView1.DataSource = QuManager.DataTable;
                CustomAspxDevGridView1.DataBind();

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
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
                if (se.Number == 2627)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "کد وارد شده تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    this.LabelWarning.Text = err.Message;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                this.LabelWarning.Text = err.Message;
            }

        }

    }

    protected void Edit(int QuSetId)
    {
        TSP.DataManager.QuestionsManager QuManager = (TSP.DataManager.QuestionsManager)Session["question"];
        TSP.DataManager.QuestionSetManager SetManager = new TSP.DataManager.QuestionSetManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager = new TSP.DataManager.Automation.LetterRelatedTablesManager();
        trans.Add(QuManager);
        trans.Add(SetManager);
        trans.Add(LetterRelatedTablesManager);

        try
        {
            SetManager.FindByCode(QuSetId);
            if (SetManager.Count == 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "برای ویرایش اطلاعات سری سؤالات مربوطه را انتخاب نمایید";
                return;
            }
            //string QuCode = CmbQucode.Value.ToString();
            //if (!string.IsNullOrEmpty(QuCode))
            //    SetManager.FindByQuCode(QuCode);
            //else
            //{
            //    this.DivReport.Visible = true;
            //    this.LabelWarning.Text = "برای ویرایش اطلاعات سری سؤالات مربوطه را انتخاب نمایید";
            //    return;

            //}
            trans.BeginSave();

            if (SetManager.Count > 0)
            {
                SetManager[0].BeginEdit();
                if (!string.IsNullOrEmpty(txtSessionNo.Text))
                    SetManager[0]["SessionNo"] = txtSessionNo.Text;
                if (!string.IsNullOrEmpty(txtSessionDate.Text))
                    SetManager[0]["SessionDate"] = txtSessionDate.Text;
                SetManager[0]["LetterNo"] = txtLetterNo.Text;
                SetManager[0]["LetterDate"] = txtLetterDate.Text;
                SetManager[0]["QuDesigner"] = txtDesigner.Text;
                SetManager[0]["Description"] = txtDesc.Text;
                if (!string.IsNullOrEmpty(txtEditNo.Text))
                    SetManager[0]["EditNo"] = txtEditNo.Text;
                SetManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                SetManager[0].EndEdit();
                SetManager.Save();
            }

            for (int i = 0; i < QuManager.Count; i++)
            {
                QuManager[i].BeginEdit();
                QuManager[i]["UserId"] = Utility.GetCurrentUser_UserId();
                QuManager[i].EndEdit();

            }

            int cnt = QuManager.Save();
            if (cnt > 0)
            {
               
                if (HiddenFieldLetter["LetterNo"].ToString() != txtLetterNo.Text)
                {
                    int LetterId = CheckLetterValidationAndFill(txtLetterNo.Text);
                    if (LetterId < -1)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "شماره نامه معتبر نمی باشد.";
                        return;
                    }
                    DeleteLetterRelatedTables(LetterRelatedTablesManager, QuSetId, (int)TSP.DataManager.TableCodes.QuestionSet);
                    InsertLetterRelatedTables(LetterRelatedTablesManager, LetterId, QuSetId, (int)TSP.DataManager.TableCodes.QuestionSet);
                  //  txtLetterNo.Enabled = false;
                }
                trans.EndSave();
                QuManager.DataTable.AcceptChanges();

                this.ViewState["BtnSave"] = btnSave.Enabled;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;

                //txtQuCode.Visible = false;

                CustomAspxDevGridView1.DataSource = QuManager.DataTable;
                CustomAspxDevGridView1.DataBind();

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
            }
            else
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
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
                if (se.Number == 2627)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "کد وارد شده تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    this.LabelWarning.Text = err.Message;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                this.LabelWarning.Text = err.Message;
            }

        }

    }
    protected void Enable()
    {
        txtDesc.Enabled = true;
        txtDesigner.Enabled = true;
        txtEditNo.Enabled = true;
        txtLetterDate.Enabled = true;
        txtLetterNo.Enabled = true;
        txtQuCode.Enabled = true;
        txtSessionDate.Enabled = true;
        txtSessionNo.Enabled = true;
        txtStatus.Enabled = true;
    }
    protected void Disable()
    {
        txtDesc.Enabled = false;
        txtDesigner.Enabled = false;
        txtEditNo.Enabled = false;
        txtLetterDate.Enabled = false;
        txtLetterNo.Enabled = false;
        txtQuCode.Enabled = false;
        txtSessionDate.Enabled = false;
        txtSessionNo.Enabled = false;
        txtStatus.Enabled = false;
    }
    protected void ClearForm()
    {
        txtDesc.Text = "";
        txtDesigner.Text = "";
        txtEditNo.Text = "";
        txtLetterDate.Text = "";
        txtLetterNo.Text = "";
        txtQuCode.Text = "";
        txtSessionDate.Text = "";
        txtSessionNo.Text = "";
        txtStatus.Text = "";
    }

    protected void FillForm(int QuSetId)
    {
        TSP.DataManager.QuestionsManager QuManager = new TSP.DataManager.QuestionsManager();
        TSP.DataManager.QuestionSetManager SetManager = new TSP.DataManager.QuestionSetManager();

        try
        {
            SetManager.FindByCode(QuSetId);
            if (SetManager.Count > 0)
            {
                txtDesc.Text = SetManager[0]["Description"].ToString();
                txtDesigner.Text = SetManager[0]["QuDesigner"].ToString();
                txtEditNo.Text = SetManager[0]["EditNo"].ToString();
                txtLetterDate.Text = SetManager[0]["LetterDate"].ToString();
                txtLetterNo.Text = SetManager[0]["LetterNo"].ToString();
                HiddenFieldLetter["LetterNo"] = SetManager[0]["LetterNo"].ToString();
                txtQuCode.Text = SetManager[0]["QuCode"].ToString();
                txtSessionDate.Text = SetManager[0]["SessionDate"].ToString();
                txtSessionNo.Text = SetManager[0]["SessionNo"].ToString();
                lblStatus.Visible = true;
                txtStatus.Visible = true;
                txtStatus.Text = SetManager[0]["ActiveName"].ToString();
            }
            else
                ClearForm();

            string QuCode = SetManager[0]["QuCode"].ToString();
            if (!string.IsNullOrEmpty(QuCode))
                QuManager.FindByQuCode(QuCode);
            Session["question"] = QuManager;
            CustomAspxDevGridView1.DataSource = QuManager.DataTable;
            CustomAspxDevGridView1.DataBind();

        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }

    }

    #region Letters Number
    private int CheckLetterValidationAndFill(string LetterNo)
    {
        int LetterId = -1;
        TSP.DataManager.Automation.LettersManager LettersManager = new TSP.DataManager.Automation.LettersManager();
        LettersManager.FindByLetterNumber(LetterNo);
        if (LettersManager.Count > 0)
        {
            LetterId = int.Parse(LettersManager[0]["LetterId"].ToString());
            txtLetterDate.Text = LettersManager[0]["LetterDate"].ToString();
            txtMailTitle.Text = LettersManager[0]["Title"].ToString();
        }
        return LetterId;
    }

    protected void InsertLetterRelatedTables(TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager, int LetterId, int TableId, int TableType)
    {
        DataRow dr = LetterRelatedTablesManager.NewRow();
        dr["LetterId"] = LetterId;
        dr["TableId"] = TableId;
        dr["TableType"] = TableType;
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        LetterRelatedTablesManager.AddRow(dr);
        LetterRelatedTablesManager.Save();
    }

    private void DeleteLetterRelatedTables(TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager, int TableId, int TableType)
    {
        LetterRelatedTablesManager.FindByTableIdTableType(TableId, TableType);
        if (LetterRelatedTablesManager.Count == 1)
        {
            LetterRelatedTablesManager[0].Delete();
            LetterRelatedTablesManager.Save();
            LetterRelatedTablesManager.DataTable.AcceptChanges();
        }
    }
    #endregion
    #endregion
}

