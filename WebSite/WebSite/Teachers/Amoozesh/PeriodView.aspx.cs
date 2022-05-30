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

public partial class Teachers_Amoozesh_PeriodView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["PPId"]))
                {
                    Response.Redirect("Period.aspx");
                    return;
                }


                PeriodId.Value = Server.HtmlDecode(Request.QueryString["PPId"]).ToString();
                InstitueId.Value = Server.HtmlDecode(Request.QueryString["InsId"]).ToString();



                string PPId = Utility.DecryptQS(PeriodId.Value);
                string InsId = Utility.DecryptQS(InstitueId.Value);


                if (string.IsNullOrEmpty(PPId) || string.IsNullOrEmpty(InsId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                }
                else
                {
                    FillForm(int.Parse(PPId));
                }
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
        }
    }

    protected void FillForm(int PPId)
    {
        TSP.DataManager.PeriodPresentManager manager = new TSP.DataManager.PeriodPresentManager();
        TSP.DataManager.ScheduleManager SchManager = new TSP.DataManager.ScheduleManager();
        try
        {
            manager.FindByCode(PPId);
            if (manager.Count == 1)
            {
               
                decimal TestCost = 0;
                txtTestCost.Text = manager[0]["TestCost"].ToString();
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
                FillCourse(int.Parse(manager[0]["CrsId"].ToString()));
                FillTeacher(PPId);
                FillOpinion(PPId);
                FillInstitue();
                SchManager.FindByTtIdTableType(PPId, (int)TSP.DataManager.TableCodes.PeriodPresent);
                if (SchManager.Count > 0)
                {
                    CustomAspxDevGridView1.DataSource = SchManager.DataTable;
                    CustomAspxDevGridView1.DataBind();

                }

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";

        }
    }

    protected void FillCourse(int CrsId)
    {
        TSP.DataManager.CourseManager CrsManager = new TSP.DataManager.CourseManager();
        try
        {

            CrsManager.FindByCode(CrsId);
            if (CrsManager.Count > 0)
            {
                txtPDuration.Text = CrsManager[0]["Duration"].ToString();
                txtPoint.Text = CrsManager[0]["Point"].ToString();
                //txtPTypeName.Text = CrsManager[0]["TypeName"].ToString();
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
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
    }

    protected void FillTeacher(int PPId)
    {
        try
        {
            TSP.DataManager.TrainingTeachersManager PPTeManager = new TSP.DataManager.TrainingTeachersManager();
            CustomAspxDevGridView2.DataSource = PPTeManager.FindByPKCode(PPId, 0);
            CustomAspxDevGridView2.DataBind();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }


    }

    protected void FillOpinion(int PPId)
    {
        TSP.DataManager.PeriodOpinionManager OpManager = new TSP.DataManager.PeriodOpinionManager();
        try
        {
            OpManager.FindByPeriodId(PPId, 1);
            if (OpManager.Count > 0)
            {
                txtAttQuCode.Value = OpManager[0]["QuCode"].ToString();
                txtAttEndDate.Text = OpManager[0]["ExpiredDate"].ToString();
                txtAttStartDate.Text = OpManager[0]["StartDate"].ToString();
            }

            OpManager.FindByPeriodId(PPId, 2);
            if (OpManager.Count > 0)
            {

                txtBazQuCode.Value = OpManager[0]["QuCode"].ToString();
                txtBazEndDate.Text = OpManager[0]["ExpiredDate"].ToString();
                txtBazStartDate.Text = OpManager[0]["StartDate"].ToString();
                txtBazSemat.Text = OpManager[0]["UltName"].ToString();
                txtBazName.Text = OpManager[0]["VName"].ToString();


            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
    }

    protected void FillInstitue()
    {
        TSP.DataManager.InstitueManager InsManager = new TSP.DataManager.InstitueManager();
        try
        {
            int InsId = int.Parse(Utility.DecryptQS(InstitueId.Value));
            InsManager.FindByCode(InsId);
            if (InsManager.Count == 1)
            {
                txtInsAddress.Text = InsManager[0]["Address"].ToString();
                txtInsDesc.Text = InsManager[0]["Description"].ToString();
                txtInsEmail.Text = InsManager[0]["Email"].ToString();
                //txtInsFileNo.Text = InsManager[0]["FileNo"].ToString();
                txtInsManager.Text = InsManager[0]["Manager"].ToString();
                txtInsMobileNo.Text = InsManager[0]["MobileNo"].ToString();
                txtInsName.Text = InsManager[0]["InsName"].ToString();
                txtInsRegDate.Text = InsManager[0]["RegDate"].ToString();
                txtInsRegNo.Text = InsManager[0]["RegNo"].ToString();
                txtInsRegPlace.Text = InsManager[0]["RegPlace"].ToString();
                txtInsTel1.Text = InsManager[0]["Tel1"].ToString();
                txtInsTel2.Text = InsManager[0]["Tel2"].ToString();
                txtInsWebSite.Text = InsManager[0]["WebSite"].ToString();
                txtInsCitName.Text = InsManager[0]["CitName"].ToString();


            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("Period.aspx");
    }

    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Date")
            e.Cell.Style["direction"] = "ltr";
    }

}

