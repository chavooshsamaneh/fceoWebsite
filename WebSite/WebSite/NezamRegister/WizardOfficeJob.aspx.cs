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

public partial class NezamRegister_WizardOfficeJob : System.Web.UI.Page
{
    DataTable dtJob = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetHelpAddress();
        }
        ASPxMenu1.Items.FindByName("Job").Selected = true;

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
            ODBJobCountry.CacheDuration = Utility.GetCacheDuration();
            //Session["TblOfJob"] = null;   
            if (Session["TblOfJob"] == null)
            {
                dtJob.Columns.Add("JhId");
                dtJob.Columns.Add("PrTypeId");
                dtJob.Columns.Add("PrTypeName");
                dtJob.Columns.Add("SazeTypeId");
                dtJob.Columns.Add("SazeTypeName");
                dtJob.Columns.Add("ProjectName");
                dtJob.Columns.Add("Employer");
                dtJob.Columns.Add("CitName");
                dtJob.Columns.Add("CounId");
                dtJob.Columns.Add("CounName");
                dtJob.Columns.Add("PJPId");
                dtJob.Columns.Add("ProjectPosition");
                dtJob.Columns.Add("StartOriginalDate");
                dtJob.Columns.Add("StartCorporateDate");
                dtJob.Columns.Add("StatusOfStartDate");
                dtJob.Columns.Add("EndCorporateDate");
                dtJob.Columns.Add("StatusOfEndDate");
                dtJob.Columns.Add("ProjectVolume");
                dtJob.Columns.Add("Area");
                dtJob.Columns.Add("Floors");
                dtJob.Columns.Add("CorTypeId");
                dtJob.Columns.Add("CorTypeName");
                dtJob.Columns.Add("Description");
                dtJob.Columns["JhId"].AutoIncrement = true;
                dtJob.Columns["JhId"].AutoIncrementSeed = 1;
                dtJob.Constraints.Add("PK_ID", dtJob.Columns["JhId"], true);

                Session["TblOfJob"] = dtJob;
            }

        }

        if (Session["TblOfJob"] != null)
        {
            dtJob = (DataTable)Session["TblOfJob"];
            GrdvJob.DataSource = dtJob;
            GrdvJob.DataBind();
        }

    }

    protected void btnJob_Click(object sender, EventArgs e)
    {
        bool check = false;
        if (txtjDesc.Text.Length > 255)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "تعداد کاراکتر های فیلد توضیحات بیش از حد مجاز می باشد.بایستی حداکثر255کاراکتر می باشد.";
            return;
        }
        if (GrdvJob.VisibleRowCount > 0)
        {
            GrdvJob.DataSource = (DataTable)Session["TblOfJob"];
            GrdvJob.DataBind();

            for (int i = 0; i < GrdvJob.VisibleRowCount; i++)
            {
                DataRowView dr = (DataRowView)GrdvJob.GetRow(i);
                if (dr["ProjectName"].ToString() == txtjPrName.Text && dr["Employer"].ToString() == txtjEmployer.Text && dr["PrTypeName"].ToString() == CombojPrType.SelectedItem.Text)
                {
                    check = true;
                    break;
                }
            }
        }

        object SazeId = DBNull.Value;
        if (CombojSazeType.Value != null)
            SazeId = CombojSazeType.Value;


        string SazeName = "";
        if (CombojSazeType.Value != null)
            SazeName = CombojSazeType.SelectedItem.Text;



        if (!check)
        {

            ((DataTable)Session["TblOfJob"]).Rows.Add(new object[]
                {null, CombojPrType.Value,CombojPrType.SelectedItem.Text,SazeId,SazeName,txtjPrName.Text,txtjEmployer.Text,txtjCity.Text,CombojCountry.Value,CombojCountry.SelectedItem.Text,ComboPosition.Value,ComboPosition.SelectedItem.Text,
                txtjStartDate.Text,txtjCoStartDate.Text,txtjStartStatus.Text,txtjCoEndDate.Text,txtjEndStatus.Text,txtjPrVolume.Text,txtjArea.Text,txtjFloor.Text,CombojIsCorporate.Value,CombojIsCorporate.SelectedItem.Text,txtjDesc.Text});
            //drdJob.Focus();


        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
            return;

        }

        dtJob = (DataTable)Session["TblOfJob"];
        GrdvJob.DataSource = dtJob;
        GrdvJob.DataBind();

        if (dtJob.Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Job").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Job").Image.Width = 15;
            ASPxMenu1.Items.FindByName("Job").Image.Height = 15;
        }

        btnJobRefresh_Click(this, new EventArgs());

    }
    protected void GrdvJob_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        //GrdvJob.DataSource = (DataTable)Session["TblOfJob"];
        //GrdvJob.DataBind();

        //int Id = -1;
        //if (GrdvJob.FocusedRowIndex > -1)
        //{
        //    Id = GrdvJob.FocusedRowIndex;

        //}
        //if (Id == -1)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
        //    return;

        //}
        //else
        //{

        dtJob = (DataTable)Session["TblOfJob"];
        //dtJob.Rows[Id].Delete();
        dtJob.Rows.Find(e.Keys[0]).Delete();

        Session["TblOfJob"] = dtJob;
        GrdvJob.DataSource = (DataTable)Session["TblOfJob"];
        GrdvJob.DataBind();
        dtJob = (DataTable)Session["TblOfJob"];

        if (dtJob.Rows.Count == 0)
            ASPxMenu1.Items.FindByName("Job").Image.Url = "";

        btnJobRefresh_Click(this, new EventArgs());
        //}
    }
    protected void btnJobRefresh_Click(object sender, EventArgs e)
    {
        btnJob.ClientEnabled = true;

        for (int i = 0; i < ASPxRoundPanel1.Controls.Count; i++)
        {
            if (ASPxRoundPanel1.Controls[i] is DevExpress.Web.ASPxComboBox)
            {
                DevExpress.Web.ASPxComboBox co = (DevExpress.Web.ASPxComboBox)ASPxRoundPanel1.Controls[i];
                co.DataBind();
                co.SelectedIndex = -1;
            }

            if (ASPxRoundPanel1.Controls[i] is DevExpress.Web.ASPxTextBox)
            {
                DevExpress.Web.ASPxTextBox co = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel1.Controls[i];
                co.Text = "";
            }
        }
        txtjDesc.Text = "";
        txtjCoEndDate.Text = "";
        txtjStartDate.Text = "";
        txtjCoStartDate.Text = "";
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardOfficeLetters.aspx");

    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardOfficeMember.aspx");
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.WizardOfficeJob).ToString());
    }
   
}
