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

public partial class Institue_Amoozesh_PeriodCosts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.ExpertCostsManager.GetUserPermission(int.Parse(Session["Login"].ToString()), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDelete.Enabled = per.CanDelete;
            btnDelete1.Enabled = per.CanDelete;
            btnEdit.Enabled = per.CanEdit;
            btnEdit1.Enabled = per.CanEdit;
            btnInsert.Enabled = per.CanNew || per.CanEdit;
            BtnNew.Enabled = per.CanNew;
            btnNew1.Enabled = per.CanNew;
            btnShow.Enabled = per.CanView;
            btnShow1.Enabled = per.CanView;
            CustomAspxDevGridView1.Visible = per.CanView;

            ComboType.DataBind();
            ComboType.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));

            if ((string.IsNullOrEmpty(Request.QueryString["PPId"])))
            {
                Response.Redirect("Period.aspx");
                return;
            }

            PgMode.Value = Utility.EncryptQS("New");
            try
            {
                PeriodId.Value = Server.HtmlDecode(Request.QueryString["PPId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            string PPId = Utility.DecryptQS(PeriodId.Value);
            int TableType = (int)TSP.DataManager.TableCodes.PeriodCosts;

            ObjectDataSource1.FilterParameters[0].DefaultValue = PPId;
            ObjectDataSource1.FilterParameters[1].DefaultValue = TableType.ToString();


            this.ViewState["BtnSave"] = btnInsert.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;


        }
        if (this.ViewState["BtnSave"] != null)
            this.btnInsert.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit1.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete1.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew1.Enabled = (bool)this.ViewState["BtnNew"];
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("Edit");
        btnInsert.Visible = true;
        ASPxPopupControl2.HeaderText = "ویرایش";

        int CoId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            CoId = (int)row["CoId"];

            PkCoId.Value = Utility.EncryptQS(CoId.ToString());

        }
        if (CoId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.DataManager.Permission per = TSP.DataManager.ExpertCostsManager.GetUserPermission(int.Parse(Session["Login"].ToString()), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            btnInsert.Enabled = per.CanNew;
            this.ViewState["BtnSave"] = btnInsert.Enabled;

            //btnInsert1.Enabled = true;
            txtBody.Enabled = true;
            txtDesc.Enabled = true;
            txtPrice.Enabled = true;
            ComboType.Enabled = true;

            TSP.DataManager.ExpertCostsManager CostManager = new TSP.DataManager.ExpertCostsManager();
            CostManager.FindByCode(CoId);
            txtBody.Text = CostManager[0]["Body"].ToString();
            txtDesc.Text = CostManager[0]["Description"].ToString();
            txtPrice.Text = float.Parse(CostManager[0]["Price"].ToString()).ToString();
            ComboType.SelectedIndex = ComboType.Items.IndexOfValue(CostManager[0]["Type"].ToString());
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {

        btnInsert.Visible = false;
        PgMode.Value = Utility.EncryptQS("View");
        ASPxPopupControl2.HeaderText = "مشاهده";

        int CoId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            CoId = (int)row["CoId"];

        }
        if (CoId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای نمایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            btnInsert.Enabled = false;
            this.ViewState["BtnSave"] = btnInsert.Enabled;

            //btnInsert1.Enabled = false;
            txtBody.Enabled = false;
            txtDesc.Enabled = false;
            txtPrice.Enabled = false;
            ComboType.Enabled = false;

            TSP.DataManager.ExpertCostsManager CostManager = new TSP.DataManager.ExpertCostsManager();
            CostManager.FindByCode(CoId);
            txtBody.Text = CostManager[0]["Body"].ToString();
            txtDesc.Text = CostManager[0]["Description"].ToString();
            txtPrice.Text = float.Parse(CostManager[0]["Price"].ToString()).ToString();
            ComboType.SelectedIndex = ComboType.Items.IndexOfValue(CostManager[0]["Type"].ToString());
            // ComboType.Value = CostManager[0]["Type"];

        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        PgMode.Value = Utility.EncryptQS("Delete");
        //ASPxRoundPanel1.HeaderText = "حذف";

        string PageMode = Utility.DecryptQS(PgMode.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        // string CoId = Utility.DecryptQS(PkCoId.Value);
        int CoId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            CoId = (int)row["CoId"];

            PkCoId.Value = Utility.DecryptQS(CoId.ToString());

        }
        if (CoId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {

            if (!string.IsNullOrEmpty(CoId.ToString()))
            {
                Delete(CoId);
            }
            else
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;

            }
        }
        
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddPeriod.aspx?PPId=" + PeriodId.Value + "&InsId=" + Request.QueryString["InsId"] + "&PageMode=" + Request.QueryString["PageMode"] + "&PPRId=" + Request.QueryString["PPRId"]);
       
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        btnInsert.Visible = true;

        TSP.DataManager.Permission per = TSP.DataManager.ExpertCostsManager.GetUserPermission(int.Parse(Session["Login"].ToString()), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnInsert.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnInsert.Enabled;

        PgMode.Value = Utility.EncryptQS("New");
        ASPxPopupControl2.HeaderText = "جدید";
        txtBody.Enabled = true;
        txtDesc.Enabled = true;
        txtPrice.Enabled = true;
        ComboType.Enabled = true;

        txtBody.Text = "";
        txtDesc.Text = "";
        txtPrice.Text = "";
        ComboType.DataBind();
        ComboType.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
        ComboType.SelectedIndex = 0;

    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        //string PageMode = txtMode.Text;
        string PPId = Utility.DecryptQS(PeriodId.Value);
        string CoId = Utility.DecryptQS(PkCoId.Value);

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
                if (string.IsNullOrEmpty(CoId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    Edit(int.Parse(CoId), int.Parse(PPId));
                }

            }

        }
    }
    protected void AspxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        int PPId = int.Parse(Utility.DecryptQS(PeriodId.Value));
        if (string.IsNullOrEmpty(PPId.ToString()))
        {
            Response.Redirect("Period.aspx");
        }


        switch (e.Item.Name)
        {
            case "Period":
                Response.Redirect("AddPeriod.aspx?PPId=" + PeriodId.Value + "&InsId=" + Request.QueryString["InsId"] + "&PageMode=" + Request.QueryString["PageMode"] + "&PPRId=" + Request.QueryString["PPRId"]);

                break;
            case "TestMarks":

                TSP.DataManager.PeriodPresentManager PPManager = new TSP.DataManager.PeriodPresentManager();
                PPManager.FindByCode(PPId);
                if (Convert.ToInt32(PPManager[0]["Status"]) == (int)TSP.DataManager.PeriodPresentStatus.StartTest || Convert.ToInt32(PPManager[0]["Status"]) == (int)TSP.DataManager.PeriodPresentStatus.AnnounceResultAndObjection)
                {
                    Response.Redirect("PeriodTestMarks.aspx?PPId=" + Utility.EncryptQS(PPId.ToString()) + "&InsId=" + Request.QueryString["InsId"] + "&PageMode=" + Request.QueryString["PageMode"] + "&PPRId=" + Request.QueryString["PPRId"]);
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان ثبت نمرات امتحانی در این وضعیت از دوره وجود ندارد";
                    return;
                }

                break;
            case "InValid":
                if (!string.IsNullOrEmpty(PeriodId.Value))
                {
                    int PSCId = -1;
                    int TableType = (int)TSP.DataManager.TableCodes.PeriodPresent;

                    if (!IsInActive(PPId))
                    {
                        Response.Redirect("InActivePeriod.aspx?PPId=" + PeriodId.Value + "&PSCId=" + Utility.DecryptQS(PSCId.ToString()) + "&PgMd=" + Utility.EncryptQS("New") + "&PageMode=" + Request.QueryString["PageMode"] + "&InsId=" + Request.QueryString["InsId"] + "&PPRId=" + Request.QueryString["PPRId"]);
                    }
                    else
                    {
                        TSP.DataManager.TrainingStatusChangeManager TrainingStatusChangeManager = new TSP.DataManager.TrainingStatusChangeManager();

                        TrainingStatusChangeManager.FindByTableType(TableType, PPId, 0);
                        if (TrainingStatusChangeManager.Count > 0)
                        {
                            PSCId = int.Parse(TrainingStatusChangeManager[0]["PSCId"].ToString());
                            Response.Redirect("InActivePeriod.aspx?PPId=" + PeriodId.Value + "&PSCId=" + Utility.EncryptQS(PSCId.ToString()) + "&PgMd=" + Utility.EncryptQS("View") + "&PageMode=" + Request.QueryString["PageMode"] + "&InsId=" + Request.QueryString["InsId"] + "&PPRId=" + Request.QueryString["PPRId"]);
                        }
                        //this.DivReport.Visible = true;
                        //this.LabelWarning.Text = "دوره انتخاب شده لغو شده است.";
                    }
                }
                break;

        }
    }
    private Boolean IsInActive(int PPId)
    {
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        PeriodPresentManager.FindByCode(PPId);
        if (PeriodPresentManager.Count == 1)
        {
            if (Convert.ToInt32(PeriodPresentManager[0]["Status"]) == (int)TSP.DataManager.PeriodPresentStatus.InvalidPeriod)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    protected void Insert()
    {

        TSP.DataManager.ExpertCostsManager CostManager = new TSP.DataManager.ExpertCostsManager();
        try
        {
            DataRow dr = CostManager.NewRow();
            dr["EpId"] = int.Parse(Utility.DecryptQS(PeriodId.Value));
            dr["Price"] = txtPrice.Text;
            dr["Body"] = txtBody.Text;
            dr["Description"] = txtDesc.Text;
            if (ComboType.Value == null)
                dr["Type"] = DBNull.Value;
            else
                dr["Type"] = ComboType.Value;
            dr["TableType"] = (int)TSP.DataManager.TableCodes.PeriodCosts;
            dr["UserId"] =Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;

            CostManager.AddRow(dr);

            int cn = CostManager.Save();
            if (cn == 1)
            {
                CustomAspxDevGridView1.DataBind();
                PkCoId.Value = Utility.EncryptQS(CostManager[0]["CoId"].ToString());
                PgMode.Value = Utility.EncryptQS("Edit");
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
                else if (se.Number == 2627)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "کد درس تکراری می باشد.";
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
    protected void Edit(int CoId, int PPId)
    {
        TSP.DataManager.ExpertCostsManager CostManager = new TSP.DataManager.ExpertCostsManager();
        CostManager.FindByCode(CoId);
        if (CostManager.Count == 1)
        {

            try
            {

                CostManager[0].BeginEdit();
                //CostManager[0]["EpId"] = PPId;
                CostManager[0]["Price"] = txtPrice.Text;
                CostManager[0]["Body"] = txtBody.Text;
                CostManager[0]["Description"] = txtDesc.Text;
                if (ComboType.Value == null)
                    CostManager[0]["Type"] = DBNull.Value;
                else
                    CostManager[0]["Type"] = ComboType.Value;

                CostManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                CostManager[0]["ModifiedDate"] = DateTime.Now;
                CostManager[0].EndEdit();

                int cn = CostManager.Save();
                if (cn == 1)
                {
                    CustomAspxDevGridView1.DataBind();
                    PkCoId.Value = Utility.EncryptQS(CostManager[0]["CoId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    //ASPxRoundPanel1.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام نشد";
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
                    else if (se.Number == 2627)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "کد درس تکراری می باشد.";
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
        else
        {
        }


    }
    protected void Delete(int CoId)
    {

        TSP.DataManager.ExpertCostsManager CostManager = new TSP.DataManager.ExpertCostsManager();

        CostManager.FindByCode(CoId);
        if (CostManager.Count == 1)
        {
            try
            {
                CostManager[0].Delete();


                int cn = CostManager.Save();
                if (cn == 1)
                {
                    //CourseId.Value = managerEdit[0]["CrsId"].ToString();
                    PkCoId.Value = Utility.EncryptQS("");
                    PgMode.Value = Utility.EncryptQS("New");
                    //ASPxRoundPanel1.HeaderText = "جدید";

                    txtBody.Text = "";
                    txtDesc.Text = "";
                    txtPrice.Text = "";
                    ComboType.DataBind();
                    ComboType.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
                    ComboType.SelectedIndex = 0;

                    CustomAspxDevGridView1.DataBind();

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "حذف انجام شد";

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                }
            }
            catch (Exception err)
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

        }
        else
        {
        }

    }
}
