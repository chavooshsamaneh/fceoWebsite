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

public partial class NezamRegister_WizardOfficeLetters : System.Web.UI.Page
{
    DataTable dtOfLetters = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetHelpAddress();
        }
        ASPxMenu1.Items.FindByName("Letter").Selected = true;

        if (Session["OfficeMembership"] != null && (Boolean)Session["OfficeMembership"] == true)
        {
            ASPxMenu1.Items.FindByName("Membership").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Membership").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Membership").Image.Height = Unit.Pixel(15);
        }
        if (Session["Office"] != null && ((DataTable)Session["Office"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Office").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Office").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Office").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfAgent"] != null && ((DataTable)Session["TblOfAgent"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Agent").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Agent").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Agent").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfLetter"] != null && ((DataTable)Session["TblOfLetter"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Letter").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Letter").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Letter").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfMember"] != null && ((DataTable)Session["TblOfMember"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Member").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Member").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Member").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfJob"] != null && ((DataTable)Session["TblOfJob"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Job").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Job").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Job").Image.Height = Unit.Pixel(15);
        }
        if (Session["OfficeSummary"] != null && (Boolean)Session["OfficeSummary"] == true)
        {
            ASPxMenu1.Items.FindByName("Summary").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Summary").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Summary").Image.Height = Unit.Pixel(15);
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (Session["TblOfLetter"] == null)
            {
                dtOfLetters.Columns.Add("OlId");
                dtOfLetters.Columns.Add("LeNo");
                dtOfLetters.Columns.Add("LePageNo");
                dtOfLetters.Columns.Add("LeDate");
                dtOfLetters.Columns.Add("LeDesc");
                dtOfLetters.Columns["OlId"].AutoIncrement = true;
                dtOfLetters.Columns["OlId"].AutoIncrementSeed = 1;
                dtOfLetters.Constraints.Add("PK_ID", dtOfLetters.Columns["OlId"], true);

                Session["TblOfLetter"] = dtOfLetters;
            }

        }
        if (Session["TblOfLetter"] != null)
        {
            dtOfLetters = (DataTable)Session["TblOfLetter"];
            CustomAspxDevGridView1.DataSource = dtOfLetters;
            CustomAspxDevGridView1.DataBind();
        } 
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool check = false;
        if (txtLeDesc.Text.Length > 255)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "تعداد کاراکتر های فیلد توضیحات بیش از حد مجاز می باشد.بایستی حداکثر255کاراکتر می باشد.";
            return;
        }
        if (CustomAspxDevGridView1.VisibleRowCount > 0)
        {
            CustomAspxDevGridView1.DataSource = (DataTable)Session["TblOfLetter"];
            CustomAspxDevGridView1.DataBind();

            for (int i = 0; i < CustomAspxDevGridView1.VisibleRowCount; i++)
            {
                DataRowView dr = (DataRowView)CustomAspxDevGridView1.GetRow(i);
                if (dr["LeNo"].ToString() == txtLeNo.Text)
                {
                    check = true;
                    break;
                }
            }
        }
        if (!check)
        {
            ((DataTable)Session["TblOfLetter"]).Rows.Add(null, txtLeNo.Text, txtLePageNo.Text, txtDate.Text, txtLeDesc.Text);
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
            return;
        }

        CustomAspxDevGridView1.DataSource = (DataTable)Session["TblOfLetter"];
        CustomAspxDevGridView1.DataBind();
        dtOfLetters = (DataTable)Session["TblOfLetter"];

        if (dtOfLetters.Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Letter").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Letter").Image.Width = 15;
            ASPxMenu1.Items.FindByName("Letter").Image.Height = 15;
        }
        btnRefresh_Click(this, new EventArgs());
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtDate.Text = "";
        txtLeDesc.Text = "";
        txtLeNo.Text = "";
        txtLePageNo.Text = "";
    }

    protected void CustomAspxDevGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        //CustomAspxDevGridView1.DataSource = (DataTable)Session["TblOfLetter"];
        //CustomAspxDevGridView1.DataBind();

        //int Id = -1;
        //if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        //{
        //    Id = CustomAspxDevGridView1.FocusedRowIndex;
        //}
        //if (Id == -1)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
        //    return;

        //}
        //else
        //{

            dtOfLetters = (DataTable)Session["TblOfLetter"];
            dtOfLetters.Rows.Find(e.Keys[0]).Delete();
           
            Session["TblOfLetter"] = dtOfLetters;
            CustomAspxDevGridView1.DataSource = (DataTable)Session["TblOfLetter"];
            CustomAspxDevGridView1.DataBind();
            dtOfLetters = (DataTable)Session["TblOfLetter"];

            if (dtOfLetters.Rows.Count == 0)
                ASPxMenu1.Items.FindByName("Letter").Image.Url = "";
           
            btnRefresh_Click(this, new EventArgs());
        //}
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (CustomAspxDevGridView1.VisibleRowCount == 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ثبت حداقل یک روزنامه رسمی الزامی می باشد";
            return;
        }
        else
            Response.Redirect("WizardOfficeSummary.aspx");

    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardOfficeJob.aspx");
    }
   
    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.WizardOfficeLetters).ToString());
    }
}
