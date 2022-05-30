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

public partial class Institue_Amoozesh_SeminarCosts : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Login"] == null || Session["Login"].ToString() == "0")
        {
            Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            return;
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {            
            btnDelete.Enabled = true;
            btnDelete1.Enabled = true;
            btnEdit.Enabled = true;
            btnEdit1.Enabled = true;
            btnInsert.Enabled = true;
            BtnNew.Enabled = true;
            btnNew1.Enabled = true;
            btnShow.Enabled = true;
            btnShow1.Enabled = true;
            CustomAspxDevGridView1.Visible = true;

            ComboType.DataBind();
            ComboType.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));

            if ((string.IsNullOrEmpty(Request.QueryString["SeId"])))
            {
                Response.Redirect("Seminar.aspx");
                return;
            }

            PgMode.Value = Utility.EncryptQS("New");
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
            int TableType = (int)TSP.DataManager.TableCodes.SeminarCosts;

            ObjectDataSource1.FilterParameters[0].DefaultValue = SeId;
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

            btnInsert.Enabled = true;
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
            txtPrice.Text =float.Parse( CostManager[0]["Price"].ToString()).ToString();
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
        Response.Redirect("AddSeminar.aspx?SeId=" + SeminarId.Value + "&InsId=" + Request.QueryString["InsId"] + "&PageMode=" + Request.QueryString["PageMode"]);
       
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        btnInsert.Visible = true;

        btnInsert.Enabled = true;
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
        string SeId = Utility.DecryptQS(SeminarId.Value);
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
                    Edit(int.Parse(CoId), int.Parse(SeId));
                }

            }

        }
    }
    protected void AspxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
      
        switch (e.Item.Name)
        {
            case "Seminar":
                Response.Redirect("AddSeminar.aspx?SeId=" + SeminarId.Value + "&InsId=" + Request.QueryString["InsId"] + "&PageMode=" + Request.QueryString["PageMode"]);

                break;
        }
    }
    #endregion

    #region Methods
    protected void Insert()
    {

        TSP.DataManager.ExpertCostsManager CostManager = new TSP.DataManager.ExpertCostsManager();
        try
        {
            DataRow dr = CostManager.NewRow();
            dr["EpId"] = int.Parse(Utility.DecryptQS(SeminarId.Value));
            dr["Price"] = txtPrice.Text;
            dr["Body"] = txtBody.Text;
            dr["Description"] = txtDesc.Text;
            if (ComboType.Value == null)
                dr["Type"] = DBNull.Value;
            else
                dr["Type"] = ComboType.Value;
            dr["TableType"] = (int)TSP.DataManager.TableCodes.SeminarCosts;
            dr["UserId"] = (int)Session["Login"];
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
    protected void Edit(int CoId, int SeId)
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

                CostManager[0]["UserId"] = (int)Session["Login"];
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
    #endregion
}
