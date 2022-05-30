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

public partial class Employee_Amoozesh_PeriodOpinionForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

      

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["PPId"]) || string.IsNullOrEmpty(Request.QueryString["QuCode"]))
            {
                Response.Redirect("Periods.aspx");
                return;
            }
            try
            {

                PeriodId.Value = Server.HtmlDecode(Request.QueryString["PPId"]).ToString();
                //MemberId.Value = Server.HtmlDecode(Request.QueryString["MeId"]).ToString();
                MemberId.Value = Server.HtmlDecode(Request.QueryString["QuCode"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PPId = Utility.DecryptQS(PeriodId.Value);
            string QuCode = Utility.DecryptQS(MemberId.Value);

            if (string.IsNullOrEmpty(PPId) || string.IsNullOrEmpty(QuCode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            try
            {
                TSP.DataManager.PeriodPresentManager PPManager = new TSP.DataManager.PeriodPresentManager();
                TSP.DataManager.PeriodOpinionManager PPOpManager = new TSP.DataManager.PeriodOpinionManager();
                PPManager.FindByCode(int.Parse(PPId));//;(18)
                lblInsName.Text = PPManager[0]["InsName"].ToString();
                lblPPName.Text = PPManager[0]["PeriodTitle"].ToString();

                PPOpManager.FindByPeriodId(int.Parse(PPId),2);//;(18, 2)
                lblStartDate.Text = PPOpManager[0]["StartDate"].ToString();
                lblExpDate.Text = PPOpManager[0]["ExpiredDate"].ToString();

            }
            catch (Exception err)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در مشاهده اطلاعات دوره رخ داده است";

            }

            //ObjectDataSource1.FilterParameters[0].DefaultValue = QuCode;//"8"
            OdbGrid.FilterParameters[0].DefaultValue = QuCode;//"8"
            OdbPPTeachers.SelectParameters[0].DefaultValue = PPId; //;"18"
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //string MeId = Utility.DecryptQS(MemberId.Value);
        int MeId = Utility.GetCurrentUser_MeId();
        string PPId = Utility.DecryptQS(PeriodId.Value);
        if (string.IsNullOrEmpty(PPId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

        }
        else
        {
            Insert(int.Parse(PPId), MeId);
        }
        //Insert(18, 1);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PeriodsView.aspx?PPId=" + PeriodId.Value + "&PageMode=" + Request.QueryString["PageMode"]);
    }
    protected void Insert(int PPId, int MeId)
    {
        DateTime dt = new DateTime();
        dt = DateTime.Now;
        System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
        string PerDate = string.Format("{0}/{1}/{2}", pDate.GetYear(dt).ToString().PadLeft(4, '0'), pDate.GetMonth(dt).ToString().PadLeft(2, '0'), pDate.GetDayOfMonth(dt).ToString().PadLeft(2, '0'));

        TSP.DataManager.OpinionsManager OpManager = new TSP.DataManager.OpinionsManager();
        try
        {
            OpManager.FindByTeIdMeIdOfPeriod(PPId, MeId, int.Parse(cmbTeacher.Value.ToString()), 4);
            if (OpManager.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = " شما قبلاً در نظر سنجی این استاد شرکت نموده اید.امکان نظر سنجی مجدد وجود ندارد ";
                btnSave.Enabled = false;
                btnSave2.Enabled = false;
                CustomAspxDevGridView1.Enabled = false;
                return;
            }
            else
            {
                //btnSave.Enabled = true;
                //btnSave2.Enabled = true;
                //CustomAspxDevGridView1.Enabled = true;
            }


            ArrayList ar = new ArrayList();

           
            for (int i = 0; i < CustomAspxDevGridView1.VisibleRowCount; i++)
            {
                DevExpress.Web.ASPxRadioButtonList rdb = (DevExpress.Web.ASPxRadioButtonList)CustomAspxDevGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.GridViewDataColumn)CustomAspxDevGridView1.Columns["AnsTypeId"], "rdb");
               // ar.Add(rdb.Value.ToString());

                if (rdb.Value != null)
                    ar.Add(rdb.Value.ToString());
                else
                    ar.Add(null);

            }
            CustomAspxDevGridView1.DataSource = OpManager.FindByTeIdMeIdOfPeriod(PPId, MeId, -1, 4).Copy();
            CustomAspxDevGridView1.DataBind();

            for (int i = 0; i < CustomAspxDevGridView1.VisibleRowCount; i++)
            {
               
                OpManager.DataSet.EnforceConstraints = false;
                DataRow dr = OpManager.NewRow();
                if (ar[i] != null)
                    dr["AnsTypeId"] = int.Parse(ar[i].ToString());
                else
                    dr["AnsTypeId"] = DBNull.Value;

                dr["QuId"] = CustomAspxDevGridView1.GetDataRow(i)["QuId"].ToString();
                dr["PeriodId"] = PPId;
                dr["MeId"] = MeId;
                dr["TeId"] = int.Parse(cmbTeacher.Value.ToString());
                dr["CreateDate"] = PerDate;
                dr["UserId"] = Utility.GetCurrentUser_UserId();
                dr["UltId"] = 4;
                //dr["Type"] = 2;
                dr["ModifiedDate"] = DateTime.Now;
                OpManager.AddRow(dr);
                //    OpManager.Save();
                //   OpManager.DataTable.AcceptChanges();

            }

            int cnt = OpManager.Save();
            if (cnt > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره رخ داده است";
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
    protected void cmbTeacher_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int TeId = int.Parse(cmbTeacher.Value.ToString());

            string PPId =  Utility.DecryptQS(PeriodId.Value);
            string MeId = Utility.GetCurrentUser_MeId().ToString();

            if (string.IsNullOrEmpty(MeId) || string.IsNullOrEmpty(PPId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            }
            else
            {
                TSP.DataManager.OpinionsManager OpManager = new TSP.DataManager.OpinionsManager();
                OpManager.FindByTeIdMeIdOfPeriod(int.Parse(PPId), int.Parse(MeId), TeId, 4);
              
                if (OpManager.Count > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = " شما قبلاً در نظر سنجی این استاد شرکت نموده اید.امکان نظر سنجی مجدد وجود ندارد ";
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    CustomAspxDevGridView1.DataSource = OpManager.FindByTeIdMeIdOfPeriod(int.Parse(PPId), int.Parse(MeId), TeId, 4);
                    CustomAspxDevGridView1.DataBind();
                    CustomAspxDevGridView1.Enabled = false;

                    return;
                }
                else
                {
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    CustomAspxDevGridView1.DataSource = OpManager.FindByTeIdMeIdOfPeriod(int.Parse(PPId), int.Parse(MeId), -1, 4);
                    CustomAspxDevGridView1.DataBind();
                    CustomAspxDevGridView1.Enabled = true;

                }
            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }

    }

    protected void rdb_DataBinding(object sender, EventArgs e)
    {

    }
}

