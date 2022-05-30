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

public partial class PeriodsView : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        //if (!IsPostBack)
        //{
            if (Request.QueryString["PPId"] == null || Request.QueryString["InsId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            string PeriodId = Server.HtmlDecode(Request.QueryString["PPId"]).ToString();
            string InstitueId = Server.HtmlDecode(Request.QueryString["InsId"]).ToString();

            string PPId = Utility.DecryptQS(PeriodId);
            string InsId = Utility.DecryptQS(InstitueId);

            if (string.IsNullOrEmpty(PPId) || string.IsNullOrEmpty(InsId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            else
                FillForm(int.Parse(PPId), int.Parse(InsId));


            OdbGrades.SelectParameters["PkId"].DefaultValue = PPId;
            OdbGrades.SelectParameters["Type"].DefaultValue = "0";

            CustomAspxDevGridViewGrade.DataBind();
        //}

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["PrePg"] == null)
            Response.Redirect("Period.aspx");
        else
        {
            switch (Utility.DecryptQS(Request.QueryString["PrePg"]))
            {
                case "Home":
                    Response.Redirect("Home.aspx");

                    break;
                case "Period":
                    Response.Redirect("Period.aspx");
                    break;
                default:
                    Response.Redirect("Home.aspx");
                    break;
            }
        }

    }

    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "Date")
        {
            e.Editor.Style["direction"] = "ltr";
        }
    }

    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "Date")
        {
            e.Cell.Style["direction"] = "ltr";
        }
    }
    #endregion

    #region Methods

    protected void FillForm(int PPId, int InsId)
    {
        TSP.DataManager.PeriodPresentManager manager = new TSP.DataManager.PeriodPresentManager();
        TSP.DataManager.ScheduleManager SchManager = new TSP.DataManager.ScheduleManager();
        TSP.DataManager.PeriodPresentRequestManager PeriodPresentRequestManager = new TSP.DataManager.PeriodPresentRequestManager();
        try
        {
            manager.FindByCode(PPId);
            if (manager.Count == 1)
            {
                decimal discount = 0;
                txtCapacity.Text = manager[0]["Capacity"].ToString();
                if (!Utility.IsDBNullOrNullValue(manager[0]["Discount"]))
                {
                    discount = Convert.ToDecimal(manager[0]["Discount"].ToString());
                    txtDiscount.Text = discount.ToString("#,#");
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
                FillInstitue(InsId);
                PeriodPresentRequestManager.FindByPeriodId(PPId);
                if (PeriodPresentRequestManager.Count > 0)
                {
                    PeriodPresentRequestManager.CurrentFilter = "IsConfirm=1";
                    if (PeriodPresentRequestManager.DataTable.DefaultView.Count == 0)
                        PeriodPresentRequestManager.CurrentFilter = "";
                    int PPRId = Convert.ToInt32(PeriodPresentRequestManager[0]["PPRId"]);
                    //SchManager.FindByTtIdTableType(PPId, (int)TSP.DataManager.TableCodes.PeriodPresent);
                    SchManager.FindByPeriodRequest(PPId, PPRId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.PeriodPresentRequest));
                    if (SchManager.Count > 0)
                    {
                        CustomAspxDevGridView1.DataSource = SchManager.DataTable;
                        CustomAspxDevGridView1.DataBind();

                    }
                }
                //SchManager.FindByTtIdTableType(int.Parse(PPId), (int)TSP.DataManager.TableCodes.PeriodPresent);
                //if (SchManager.Count > 0)
                //{
                //    CustomAspxDevGridView1.DataSource = SchManager.DataTable;
                //    CustomAspxDevGridView1.DataBind();

                //}

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
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }


    }

    protected void FillInstitue(int InsId)
    {
        TSP.DataManager.InstitueManager InsManager = new TSP.DataManager.InstitueManager();
        try
        {
            //int InsId = int.Parse(Utility.DecryptQS(InstitueId.Value));
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
                //txtInsRegDate.Text = InsManager[0]["RegDate"].ToString();
                //txtInsRegNo.Text = InsManager[0]["RegNo"].ToString();
                //txtInsRegPlace.Text = InsManager[0]["RegPlace"].ToString();
                //txtInsTel1.Text = InsManager[0]["Tel1"].ToString();
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
    #endregion
}
