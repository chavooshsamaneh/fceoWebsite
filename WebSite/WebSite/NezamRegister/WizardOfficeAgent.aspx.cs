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

public partial class NezamRegister_WizardOfficeAgent : System.Web.UI.Page
{
    DataTable dtOfAgent = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetHelpAddress();
        }
        ASPxMenu1.Items.FindByName("Agent").Selected = true;

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
            if (Session["TblOfAgent"] == null)
            {
                dtOfAgent.Columns.Add("OagId");
                dtOfAgent.Columns.Add("OagName");
                dtOfAgent.Columns.Add("Responsible");
                dtOfAgent.Columns.Add("Tel");
                dtOfAgent.Columns.Add("Fax");
                dtOfAgent.Columns.Add("Address");
                dtOfAgent.Columns.Add("Website");
                dtOfAgent.Columns.Add("Email");
                dtOfAgent.Columns.Add("Tel_Pre");
                dtOfAgent.Columns.Add("Fax_Pre");
                dtOfAgent.Columns.Add("Tel_");
                dtOfAgent.Columns.Add("Fax_");
                dtOfAgent.Columns["OagId"].AutoIncrement = true;
                dtOfAgent.Columns["OagId"].AutoIncrementSeed = 1;
                dtOfAgent.Constraints.Add("PK_ID", dtOfAgent.Columns["OagId"], true);

                Session["TblOfAgent"] = dtOfAgent;
            }

        }
        if (Session["TblOfAgent"] != null)
        {
            dtOfAgent = (DataTable)Session["TblOfAgent"];
            CustomAspxDevGridView1.DataSource = dtOfAgent;
            CustomAspxDevGridView1.DataBind();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool check = false;
        if (CustomAspxDevGridView1.VisibleRowCount > 0)
        {
            CustomAspxDevGridView1.DataSource = (DataTable)Session["TblOfAgent"];
            CustomAspxDevGridView1.DataBind();

            for (int i = 0; i < CustomAspxDevGridView1.VisibleRowCount ; i++)
            {
                DataRowView dr = (DataRowView)CustomAspxDevGridView1.GetRow(i);
                if (dr["OagName"].ToString() == txtOfAgName.Text)
                {
                    check = true;
                    break;
                }
            }
        }
        if (!check)
        {
            string tel = "", fax = "";
            if (txtOfAgTel_pre.Text != "" && txtOfAgTel.Text != "")
                tel = txtOfAgTel_pre.Text + "-" + txtOfAgTel.Text;
            else if (txtOfAgTel.Text != "")
                tel = txtOfAgTel.Text;
            if (txtOfAgFax_pre.Text != "" && txtOfAgFax.Text != "")
                fax = txtOfAgFax_pre.Text + "-" + txtOfAgFax.Text;
            else if (txtOfAgFax.Text != "")
                fax = txtOfAgFax.Text;
            ((DataTable)Session["TblOfAgent"]).Rows.Add(null,txtOfAgName.Text, txtOfAgResponsible.Text, tel, fax, txtOfAgAddress.Text, txtOfAgWebsite.Text, txtOfAgEmail1.Text ,
                txtOfAgTel_pre.Text,txtOfAgFax_pre.Text,txtOfAgTel.Text,txtOfAgFax.Text);


        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
            return;

        }
        CustomAspxDevGridView1.DataSource = (DataTable)Session["TblOfAgent"];
        CustomAspxDevGridView1.DataBind();
        dtOfAgent = (DataTable)Session["TblOfAgent"];

        if (dtOfAgent.Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Agent").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Agent").Image.Width = 15;
            ASPxMenu1.Items.FindByName("Agent").Image.Height = 15;
        }

        btnRefresh_Click(this, new EventArgs());
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        btnAdd.ClientEnabled = true;

        for (int i = 0; i < ASPxRoundPanel2.Controls.Count; i++)
        {
           
            if (ASPxRoundPanel2.Controls[i] is DevExpress.Web.ASPxTextBox)
            {
                DevExpress.Web.ASPxTextBox co = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel2.Controls[i];
                co.Text = "";
            }
        }
        txtOfAgAddress.Text = "";
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardOfficeMember.aspx");

    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardOffice.aspx");
    }
    protected void CustomAspxDevGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        //CustomAspxDevGridView1.DataSource = (DataTable)Session["TblOfAgent"];
        //CustomAspxDevGridView1.DataBind();

        //int Id = -1;
        //if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        //{
        //    Id = CustomAspxDevGridView1.FocusedRowIndex;
        //    //DataRow row = GrdvJob.GetDataRow(GrdvJob.FocusedRowIndex);
        //    //Id = (int)row["JhId"];
        //}
        //if (Id == -1)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
        //    return;

        //}
        //else
        //{

            dtOfAgent = (DataTable)Session["TblOfAgent"];
            dtOfAgent.Rows.Find(e.Keys[0]).Delete();
           
            Session["TblOfAgent"] = dtOfAgent;
            CustomAspxDevGridView1.DataSource = (DataTable)Session["TblOfAgent"];
            CustomAspxDevGridView1.DataBind();
            dtOfAgent = (DataTable)Session["TblOfAgent"];

            if (dtOfAgent.Rows.Count == 0)
                ASPxMenu1.Items.FindByName("Agent").Image.Url = "";

            btnRefresh_Click(this, new EventArgs());
        //}
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.WizardOfficeAgent).ToString());
    }
}
