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

public partial class Members_Amoozesh_PeriodsView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");


        if (string.IsNullOrEmpty(Request.QueryString["PPId"]) || string.IsNullOrEmpty(Request.QueryString["InsId"]))
        {
            Response.Redirect("Periods.aspx");
            return;
        }

        try
        {
            PeriodId.Value = Server.HtmlDecode(Request.QueryString["PPId"]).ToString();
            InstitueId.Value = Server.HtmlDecode(Request.QueryString["InsId"]).ToString();
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        string PPId = Utility.DecryptQS(PeriodId.Value);
        string InsId = Utility.DecryptQS(InstitueId.Value);


        if (string.IsNullOrEmpty(PPId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        FillForm(int.Parse(PPId), int.Parse(InsId));
    }

    private void FillForm(int PPId, int InsId)
    {
        TSP.DataManager.PeriodPresentManager manager = new TSP.DataManager.PeriodPresentManager();
        TSP.DataManager.ScheduleManager SchManager = new TSP.DataManager.ScheduleManager();
        TSP.DataManager.CourseManager CrsManager = new TSP.DataManager.CourseManager();
        TSP.DataManager.TrainingTeachersManager PPTeManager = new TSP.DataManager.TrainingTeachersManager();
        TSP.DataManager.PeriodOpinionManager OpManager = new TSP.DataManager.PeriodOpinionManager();
        TSP.DataManager.InstitueManager InsManager = new TSP.DataManager.InstitueManager();

        try
        {
            manager.FindByCode(PPId);
            if (manager.Count == 1)
            {
                decimal TestCost = 0;
                txtCapacity.Text = manager[0]["Capacity"].ToString();
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


                #region Course
                int CrsId = int.Parse(manager[0]["CrsId"].ToString());

                CrsManager.FindByCode(CrsId);
                if (CrsManager.Count > 0)
                {
                    txtPDuration.Text = CrsManager[0]["Duration"].ToString();
                    txtPoint.Text = CrsManager[0]["Point"].ToString();
                    // txtPTypeName.Text = CrsManager[0]["TypeName"].ToString();
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
                #endregion
                #region Teacher
                CustomAspxDevGridView2.DataSource = PPTeManager.FindByPKCode(PPId, 0);
                CustomAspxDevGridView2.DataBind();
                #endregion
                #region Opinion

                OpManager.FindByPeriodId(PPId, 1);
                if (OpManager.Count > 0)
                {
                    txtOpQucode.Text = OpManager[0]["QuCode"].ToString();
                    txtOpEndDate.Text = OpManager[0]["ExpiredDate"].ToString();
                    txtOpStartDate.Text = OpManager[0]["StartDate"].ToString();
                }
                #endregion
                #region Schedule
                SchManager.FindByTtIdTableType(PPId, (int)TSP.DataManager.TableCodes.PeriodPresent);
                if (SchManager.Count > 0)
                {
                    CustomAspxDevGridView1.DataSource = SchManager.DataTable;
                    CustomAspxDevGridView1.DataBind();

                }
                #endregion
                #region Institue

                InsManager.FindByCode(InsId);
                txtIAddress.Text = InsManager[0]["Address"].ToString();
                txtIDesc.Text = InsManager[0]["Description"].ToString();
                txtIEmail.Text = InsManager[0]["Email"].ToString();
                //txtIFileNo.Text = InsManager[0]["FileNo"].ToString();
                txtIMobileNo.Text = InsManager[0]["MobileNo"].ToString();
                txtInsManager.Text = InsManager[0]["Manager"].ToString();
                txtInsName.Text = InsManager[0]["InsName"].ToString();
                txtIRegDate.Text = InsManager[0]["RegDate"].ToString();
                txtIRegNo.Text = InsManager[0]["RegNo"].ToString();
                txtIRegPlace.Text = InsManager[0]["RegPlace"].ToString();
                txtITel1.Text = InsManager[0]["Tel1"].ToString();
                txtITel2.Text = InsManager[0]["Tel2"].ToString();
                txtIWebSite.Text = InsManager[0]["WebSite"].ToString();

                #endregion

                OdbGrades.SelectParameters["PkId"].DefaultValue = PPId.ToString();
                OdbGrades.SelectParameters["Type"].DefaultValue = "0";

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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["RetPage"] == null)
        {
            Response.Redirect("../MemberHome.aspx");
        }
        string RetPage = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["RetPage"]).ToString());
        if (!string.IsNullOrEmpty(RetPage))
        {
            if (RetPage == "Madrak")
                Response.Redirect("Licence.aspx");
            else if (RetPage == "PerReg")
                Response.Redirect("PeriodRegister.aspx");
            else
                Response.Redirect("Periods.aspx");
        }
        else
            Response.Redirect("Periods.aspx");

    }
}
