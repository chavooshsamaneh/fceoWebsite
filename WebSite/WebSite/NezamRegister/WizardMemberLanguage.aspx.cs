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

public partial class NezamRegister_WizardMemberLanguage : System.Web.UI.Page
{
    DataTable dtLanguage = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        ASPxMenu1.Items.FindByName("Language").Selected = true;

        if (Session["MemberMembership"] != null && (Boolean)Session["MemberMembership"] == true)
        {
            ASPxMenu1.Items.FindByName("Membership").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Membership").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Membership").Image.Height = Unit.Pixel(15);
        }
        if (Session["Member"] != null && ((DataTable)Session["Member"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Member").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Member").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Member").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblOfMadrak"] != null && ((DataTable)Session["TblOfMadrak"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Madrak").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Madrak").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Madrak").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblJob"] != null && ((DataTable)Session["TblJob"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Job").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Job").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Job").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblActivity"] != null && Session["TblActivity2"] != null && ((DataTable)Session["TblActivity"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Activity").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Activity").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Activity").Image.Height = Unit.Pixel(15);
        }
        if (Session["TblLanguage"] != null && ((DataTable)Session["TblLanguage"]).Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Language").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Language").Image.Width = Unit.Pixel(15);
            ASPxMenu1.Items.FindByName("Language").Image.Height = Unit.Pixel(15);
        }
        if (Session["MemberSummary"] != null && (Boolean)Session["MemberSummary"] == true)
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
            SetHelpAddress();
            //Session["TblLanguage"] = null;
            if (Session["TblLanguage"] == null)
            {
                dtLanguage.Columns.Add("MlanId");
                dtLanguage.Columns.Add("LanId");
                dtLanguage.Columns.Add("LanName");
                dtLanguage.Columns.Add("LqId");
                dtLanguage.Columns.Add("LqName");
                dtLanguage.Columns.Add("Description");
                dtLanguage.Columns["MlanId"].AutoIncrement = true;
                dtLanguage.Columns["MlanId"].AutoIncrementSeed = 1;
                dtLanguage.Constraints.Add("PK_ID", dtLanguage.Columns["MlanId"], true);

                Session["TblLanguage"] = dtLanguage;
            }
        }

        if (Session["TblLanguage"] != null)
        {
            dtLanguage = (DataTable)Session["TblLanguage"];
            CustomAspxDevGridView1.DataSource = dtLanguage;
            CustomAspxDevGridView1.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool check = false;
        if (txtDesc.Text.Length > 255)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "تعداد کاراکتر های فیلد توضیحات بیش از حد مجاز می باشد.بایستی حداکثر255کاراکتر می باشد.";
            return;
        }
        if (CustomAspxDevGridView1.VisibleRowCount > 0)
        {
            for (int i = 0; i < CustomAspxDevGridView1.VisibleRowCount ; i++)
            {
                CustomAspxDevGridView1.DataSource = (DataTable)Session["TblLanguage"];
                CustomAspxDevGridView1.DataBind();

                DataRowView dr = (DataRowView)CustomAspxDevGridView1.GetRow(i);
                if (dr["LanName"].ToString() == drdLanName.SelectedItem.Text)
                {
                    check = true;
                    break;
                }
            }
        }
        if (!check)
        {

            ((DataTable)Session["TblLanguage"]).Rows.Add(new object[] {null,drdLanName.Value, drdLanName.SelectedItem.Text, drdLanQuality.Value, drdLanQuality.SelectedItem.Text, txtDesc.Text });


        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";

        }
        CustomAspxDevGridView1.DataSource = (DataTable)Session["TblLanguage"];
        CustomAspxDevGridView1.DataBind();
        dtLanguage = (DataTable)Session["TblLanguage"];

        if (dtLanguage.Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Language").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Language").Image.Width = 15;
            ASPxMenu1.Items.FindByName("Language").Image.Height = 15;
        }

        btnRefresh_Click(this, new EventArgs());
    }
   
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtDesc.Text = "";
        drdLanName.DataBind();
        drdLanName.SelectedIndex = -1;
        drdLanQuality.DataBind();
        drdLanQuality.SelectedIndex = -1;
    }

    protected void CustomAspxDevGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        //CustomAspxDevGridView1.DataSource = (DataTable)Session["TblLanguage"];
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

            dtLanguage = (DataTable)Session["TblLanguage"];
            //dtLanguage.Rows[Id].Delete();
            dtLanguage.Rows.Find(e.Keys[0]).Delete();

            Session["TblLanguage"] = dtLanguage;
            CustomAspxDevGridView1.DataSource = (DataTable)Session["TblLanguage"];
            CustomAspxDevGridView1.DataBind();
            dtLanguage = (DataTable)Session["TblLanguage"];

            if (dtLanguage.Rows.Count == 0)
                ASPxMenu1.Items.FindByName("Language").Image.Url = "";

        //}
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardMemberActivity.aspx");
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        //Response.Redirect("WizardMemberResearch.aspx");
        Response.Redirect("WizardMemberSummary.aspx");


    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Member":
                Response.Redirect("WizardMember.aspx");
                break;
            case "Madrak":
                Response.Redirect("WizardMemberMadrak.aspx");
                break;
            case "Job":
                Response.Redirect("WizardMemberJob.aspx");
                break;
            case "Activity":
                Response.Redirect("WizardMemberActivity.aspx");
                break;
            case "Language":
                Response.Redirect("WizardMemberLanguage.aspx");
                break;
            case "Research":
                Response.Redirect("WizardMemberResearch.aspx");
                break;
            case "Summary":
                Response.Redirect("WizardMemberSummary.aspx");
                break;
            case "End":
                Response.Redirect("WizardMemberFinish.aspx");
                break;
        }
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.WizardMemberLanguage).ToString());
    }
}
