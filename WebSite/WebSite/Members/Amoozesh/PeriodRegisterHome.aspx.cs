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

public partial class Members_Amoozesh_PeriodRegisterHome : System.Web.UI.Page
{
    private bool IsPageRefresh = false;

    private int PollId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldCourseRegister["PollId"]);
        }
        set
        {
            HiddenFieldCourseRegister["PollId"] = value;
        }
    }
    private int PPId
    {
        get
        {
            return Convert.ToInt32(HiddenFieldCourseRegister["PPId"]);
        }
        set
        {
            HiddenFieldCourseRegister["PPId"] = value;
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
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }

        if (!IsPostBack)
        {
            TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
            LoginManager.FindByCode(Utility.GetCurrentUser_UserId());
            int MeId = -1;
            if (LoginManager.Count == 1)
            {
                MeId = int.Parse(LoginManager[0]["MeId"].ToString());
                ObjdsPeriodRegister.SelectParameters[1].DefaultValue = MeId.ToString();
            }
            else
            {

                Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
                return;
            }

        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (IsPageRefresh)
            return;
        if (Utility.GetCurrentUser_IsLock())
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما امکان ثبت اطلاعات وجود ندارد.";
            return;
        }


        int PRId = -1;
        if (GridViewCourseRegister.FocusedRowIndex > -1)
        {
            DataRow PPRow = GridViewCourseRegister.GetDataRow(GridViewCourseRegister.FocusedRowIndex);
            PRId = int.Parse(PPRow["PRId"].ToString());
            DeleteRegister(PRId);
        }
    }
    protected void btnPollAnswer_Click(object sender, EventArgs e)
    {
        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresent);
        Response.Redirect("~/Poll/AnswerPollQuestion.aspx?PId=" + Utility.EncryptQS(PollId.ToString()) + "&TT=" + TableType.ToString() + "&TId=" + PPId.ToString());

    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        DataRow row = GridViewCourseRegister.GetDataRow(GridViewCourseRegister.FocusedRowIndex);
        int Count;
        if (!Utility.IsDBNullOrNullValue(row["PPId"]))
        {
            PPId = Convert.ToInt32(row["PPId"].ToString());
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ابتدا باید یک ردیف انتخاب گردد";
            return;
        }
        if (!Utility.IsDBNullOrNullValue(row["IsSeminar"]) && Convert.ToInt32(row["IsSeminar"].ToString()) == 1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "نتایج آزمون مختص به دوره ها است";
            return;
        }
        if (!Utility.IsDBNullOrNullValue(row["PollId"]) && Convert.ToInt32(row["PollId"].ToString()) != 0)
        {

            TSP.DataManager.PollAnswerManager PollAnswerManager = new TSP.DataManager.PollAnswerManager();
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresent);
            Count = PollAnswerManager.SelectCountOfAnswerPollForPeriodPresent(TableType, PPId, Utility.GetCurrentUser_UserId());
            if (Count == 0)
            {
                PopupPoll.ShowOnPageLoad = true;
            }
            else
            {
                ///به دلیل عدم ارتباط با دیتا بیس در صفحه ی بعد و بررسی دوباره ی شرکت در نظر سنجی کد عضویت را به صورت 
                ///کوئری استرینگ اینجا فرستاده ا که در تابع جستجو مذکور با دانستن نام صفحه نتواند نتایج را ببیند
                if (!Utility.IsDBNullOrNullValue(row["PRId"]))
                {
                    Response.Redirect("PeriodRegisterView.aspx?PR=" + Utility.EncryptQS(row["PRId"].ToString()) + "&Me=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString()));
                }
            }

        }
        else
        {
            if (!Utility.IsDBNullOrNullValue(row["PRId"]))
            {
                Response.Redirect("PeriodRegisterView.aspx?PR=" + Utility.EncryptQS(row["PRId"].ToString()) + "&Me=" + Utility.EncryptQS(Utility.GetCurrentUser_MeId().ToString()));
            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (Utility.GetCurrentUser_IsLock())
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما امکان ثبت اطلاعات وجود ندارد.";
            return;
        }

        string PRId = Utility.DecryptQS(HDPRId.Value);
        if (string.IsNullOrEmpty(PRId))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره اطلاعات رخ داده است";
        }
        else
            Insert(int.Parse(PRId));

    }
    protected void btnObjection_Click(object sender, EventArgs e)
    {


        if (GridViewCourseRegister.FocusedRowIndex > -1)
        {

            if (IsPageRefresh)
                return;
            if (Utility.GetCurrentUser_IsLock())
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما امکان ثبت اطلاعات وجود ندارد.";
                return;
            }

            DataRow PPRow = GridViewCourseRegister.GetDataRow(GridViewCourseRegister.FocusedRowIndex);
            HDPRId.Value = Utility.EncryptQS(PPRow["PRId"].ToString());
            int PRId = int.Parse(PPRow["PRId"].ToString());
            int PPId = int.Parse(PPRow["PPId"].ToString());


            try
            {
                TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
                PeriodPresentManager.FindByCode(PPId);
                if (PeriodPresentManager.Count == 1)
                {
                    if (Convert.ToInt32(PeriodPresentManager[0]["Status"]) != (int)TSP.DataManager.PeriodPresentStatus.AnnounceResultAndObjection)//تایید نهایی
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "جهت ثبت اعتراض وضعیت دوره بایستی  اعلام نتایج و اعتراضات باشد";
                        return;
                    }

                    if (Convert.ToBoolean(PeriodPresentManager[0]["IsConfirm"]))//تایید نهایی
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ثبت اعتراض به دلیل به پایان رسیدن اعتراضات و ثبت نهایی نمرات وجود ندارد";
                        return;
                    }
                }


                TSP.DataManager.PeriodTestMarksManager MarkManager = new TSP.DataManager.PeriodTestMarksManager();
                MarkManager.FindByPRCode(PRId);
                if (MarkManager.Count > 0)
                {
                    if (string.Compare(Utility.GetDateOfToday(), MarkManager[0]["ObjectionDate"].ToString()) <= 0)
                    {
                        if (string.IsNullOrEmpty(MarkManager[0]["MeObjectionDate"].ToString()))
                            PopupObjection.ShowOnPageLoad = true;
                        else
                        {
                            lblMeDate.Visible = true;
                            txtMeObjDate.Visible = true;
                            txtMeObjDate.Text = MarkManager[0]["MeObjectionDate"].ToString();
                            txtObjText.Text = MarkManager[0]["MeObjectionText"].ToString();
                            btnSave.Visible = false;

                            if (!string.IsNullOrEmpty(MarkManager[0]["TeObjectionDate"].ToString()))
                            {
                                lblMark.Visible = true;
                                txtFirstMark.Visible = true;
                                lblLastMark.Visible = true;
                                txtLastMark.Visible = true;
                                lblAnsDate.Visible = true;
                                txtAnsDate.Visible = true;
                                lblAns.Visible = true;
                                txtTeObjText.Visible = true;

                                txtAnsDate.Text = MarkManager[0]["TeObjectionDate"].ToString();
                                txtFirstMark.Text = MarkManager[0]["FirstMark"].ToString();
                                txtLastMark.Text = MarkManager[0]["LastMark"].ToString();
                                txtTeObjText.Text = MarkManager[0]["TeObjectionText"].ToString();
                            }


                            PopupObjection.ShowOnPageLoad = true;
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان ثبت اعتراض مجدد برای شما وجود ندارد";

                        }


                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان ثبت اعتراض به دلیل پایان یافتن زمان اعتراض وجود ندارد";
                        return;
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان ثبت اعتراض به دلیل وارد نشدن نمره وجود ندارد";
                    return;
                }

            }
            catch (Exception err)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره اطلاعات رخ داده است";

            }

        }
    }
    protected void GridViewCourseRegister_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "RegisterDate" || e.DataColumn.FieldName == "StartDate" || e.DataColumn.FieldName == "EndDate")
            e.Cell.Style["direction"] = "ltr";

    }
    protected void GridViewCourseRegister_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "RegisterDate" || e.Column.FieldName == "StartDate" || e.Column.FieldName == "EndDate")
            e.Editor.Style["direction"] = "ltr";
    }
    #endregion

    #region Methods
    private void DeleteRegister(int PRId)
    {
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        try
        {
            PeriodRegisterManager.FindByCode(PRId);
            if (PeriodRegisterManager.Count == 1)
            {
                PeriodRegisterManager[0].Delete();
                int cn = PeriodRegisterManager.Save();
                if (cn > 0)
                {
                    GridViewCourseRegister.DataBind();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
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
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
        catch (Exception err)
        {
            SetDeleteError(err);
        }
    }

    private void SetDeleteError(Exception err)
    {

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

    private void Insert(int PRId)
    {
        TSP.DataManager.PeriodTestMarksManager MarkManager = new TSP.DataManager.PeriodTestMarksManager();
        try
        {
            MarkManager.FindByPRCode(PRId);
            if (MarkManager.Count > 0)
            {
                MarkManager[0].BeginEdit();
                MarkManager[0]["MeObjectionText"] = txtObjText.Text;
                MarkManager[0]["MeObjectionDate"] = Utility.GetDateOfToday();
                MarkManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                MarkManager[0]["ModifiedDate"] = DateTime.Now;
                MarkManager[0].EndEdit();
                if (MarkManager.Save() > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره اطلاعات رخ داده است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره اطلاعات رخ داده است";

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
