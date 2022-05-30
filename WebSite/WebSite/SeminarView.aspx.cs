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

public partial class SeminarView : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            Session["IsEdited_SeminarView"] = false;

            if (string.IsNullOrEmpty(Request.QueryString["SeId"]))
            {
                Response.Redirect("Default.aspx");
                return;
            }

            try
            {
                SeminarId.Value = Server.HtmlDecode(Request.QueryString["SeId"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }


            string SeId = Utility.DecryptQS(SeminarId.Value);


            if (string.IsNullOrEmpty(SeId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            OdbGrades.SelectParameters["PkId"].DefaultValue = SeId;
            OdbGrades.SelectParameters["Type"].DefaultValue = "1";

            CustomAspxDevGridViewGrade.DataBind();


            TSP.DataManager.ScheduleManager ScheduleManager = new TSP.DataManager.ScheduleManager();
            TSP.DataManager.SeminarRequestManager SeminarRequestManager = new TSP.DataManager.SeminarRequestManager();
            int SeReqId = -2;
            SeminarRequestManager.SelectSeminarRequest(int.Parse(SeId), -1, 1);
            if (SeminarRequestManager.Count != 0)
            {
                SeReqId = Convert.ToInt32(SeminarRequestManager[SeminarRequestManager.Count - 1]["SeReqId"]);
            }
            ScheduleManager.FindBySminarRequest(int.Parse(SeId), SeReqId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.Seminar));
            AspxGridSchedule.DataSource = ScheduleManager.DataTable;
            AspxGridSchedule.DataBind();
            FillForm(int.Parse(SeId), SeReqId);
            //OdbSchedule.FilterParameters[0].DefaultValue = SeId;
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");

    }

    protected void AspxGridSchedule_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Date")
            e.Cell.Style["direction"] = "ltr";
    }

    #endregion

    #region Methods

    protected void FillForm(int SeId, int SeReqId)
    {
        TSP.DataManager.SeminarManager SeManager = new TSP.DataManager.SeminarManager();
        TSP.DataManager.TrainingTeachersManager SemTeachManager = new TSP.DataManager.TrainingTeachersManager();

        TSP.DataManager.TrainingJudgmentManager JudgeManager = new TSP.DataManager.TrainingJudgmentManager();

        try
        {
            SeManager.FindByCode(SeId);
            if (SeManager.Count > 0)
            {
                txtDate.Text = SeManager[0]["StartDate"].ToString();
                txtEndDate.Text = SeManager[0]["EndDate"].ToString();
                txtDesc.Text = SeManager[0]["Description"].ToString();
                txtPlace.Text = SeManager[0]["Place"].ToString();
                txtTopic.Text = SeManager[0]["Topic"].ToString();
                txtDuration.Text = SeManager[0]["Duration"].ToString();

                decimal SeminarCost = Convert.ToDecimal(SeManager[0]["SeminarCost"].ToString());
                txtSeminarCost.Text = SeminarCost.ToString("#,#");

                txtSubject.Text = SeManager[0]["Subject"].ToString();
                txtTime.Text = SeManager[0]["Time"].ToString();


                SemTeachManager.FindByPeriodRequestId(SeId, SeReqId, 1);
                // SemTeachManager.FindByPKCode(SeId, 1);
                if (SemTeachManager.Count > 0)
                {
                    Grdv_Teacher.DataSource = SemTeachManager.DataTable;
                    Grdv_Teacher.DataBind();
                }

                TSP.DataManager.AttachmentsManager AttManager = new TSP.DataManager.AttachmentsManager();
                AspxGridFlp.DataSource = AttManager.FindByTablePrimaryKey_AttId((int)TSP.DataManager.TableCodes.Seminar, SeId, 2);
                AspxGridFlp.DataBind();
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

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if (Utility.GetCurrentUser_UserId() < 0)
        {

            ShowMessage("جهت ثبت نام از طریق نام کاربری و رمز عبور وارد پرتال شخصی خود شوید و مجددا اقدام نمایید.تنها اعضای دائم و تایید شده سازمان قادر به ثبت نام در سمینارهای آموزشی می باشند.");
            return;
        }
        if (Utility.GetCurrentUser_UserId() > 0 && Utility.GetCurrentUser_LoginType() != (int)TSP.DataManager.UserType.Member)
        {
            ShowMessage("تنها اعضای دائم و تایید شده سازمان قادر به ثبت نام در سمینارهای آموزشی می باشند.");
            return;
        }
        int SeId = Convert.ToInt32(Utility.DecryptQS(SeminarId.Value));
        if (TSP.DataManager.PeriodRegisterManager.CheckIfRepititiveRegister(SeId, Utility.GetCurrentUser_MeId()))
        {
            ShowMessage("شما قبلا در این سمینار ثبت نام کرده اید.");
            return;
        }
        System.Collections.ArrayList HasCappacity = TSP.DataManager.SeminarManager.HasCapacity(SeId);
        if (!Convert.ToBoolean(HasCappacity[0]))
        {
            ShowMessage(HasCappacity[1].ToString());
            return;
        }

        Response.Redirect("Members/Accounting/EpaymentMultiplePay.aspx?MPt=" + Utility.EncryptQS("SeminarRegister") + "&SeId=" + Utility.EncryptQS(SeId.ToString()));

    }

    protected void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
}
