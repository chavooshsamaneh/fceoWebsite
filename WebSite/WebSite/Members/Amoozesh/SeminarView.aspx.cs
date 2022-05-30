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

public partial class Members_Amoozesh_SeminarView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (string.IsNullOrEmpty(Request.QueryString["SeId"]))
        {
            Response.Redirect("Licence.aspx");
            return;
        }
        string RetPage = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["RetPage"]).ToString());
        if (!string.IsNullOrEmpty(RetPage))
        {
            if (RetPage == "Madrak")
                btnRegister.Visible=btnRegister1.Visible = false;
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

        OdbSchedule.FilterParameters[0].DefaultValue = SeId;

        FillForm(int.Parse(SeId));
    }
    protected void FillForm(int SeId)
    {
        TSP.DataManager.SeminarManager SeManager = new TSP.DataManager.SeminarManager();
        TSP.DataManager.TrainingTeachersManager SemTeachManager = new TSP.DataManager.TrainingTeachersManager();


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
                txtPoint.Text = SeManager[0]["Point"].ToString();

                decimal SeminarCost = Convert.ToDecimal(SeManager[0]["SeminarCost"].ToString());
                txtSeminarCost.Text = SeminarCost.ToString("#,#");
                //decimal TeacherSalary = Convert.ToDecimal(SeManager[0]["TeacherSalary"].ToString());
                //txtTeacherCost.Text = TeacherSalary.ToString("#,#");

                txtSubject.Text = SeManager[0]["Subject"].ToString();
                txtTime.Text = SeManager[0]["Time"].ToString();


                SemTeachManager.FindByPKCode(SeId, 1);
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string RetPage = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["RetPage"]).ToString());
        if (!string.IsNullOrEmpty(RetPage))
        {
            if (RetPage == "Madrak")
                Response.Redirect("Licence.aspx");
            else if (RetPage == "PerReg")
                Response.Redirect("SeminarRegister.aspx");

        }
        //else
        //    Response.Redirect(".aspx");
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
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

        Response.Redirect("../Accounting/EpaymentMultiplePay.aspx?MPt=" + Utility.EncryptQS("SeminarRegister") + "&SeId=" + Utility.EncryptQS(SeId.ToString()));

    }

    protected void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

}
