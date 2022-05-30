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
using System.Data.SqlClient;

public partial class NezamRegister_WizardMemberActivity : System.Web.UI.Page
{
    DataTable dtActivity = new DataTable();
    DataTable dtActivity2 = new DataTable();


    protected void Page_Load(object sender, EventArgs e)
    {
        ASPxMenu1.Items.FindByName("Activity").Selected = true;

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
            //Session["TblActivity"] = null;
            //Session["TblActivity2"] = null;

            if (Session["TblActivity"] == null)
            {
                dtActivity.Columns.Add("MasId");
                dtActivity.Columns.Add("AtId");
                dtActivity.Columns.Add("AsName");
                dtActivity.Columns.Add("AsPercent");
                dtActivity.Columns.Add("Description");
                dtActivity.Columns["MasId"].AutoIncrement = true;
                dtActivity.Columns["MasId"].AutoIncrementSeed = 1;
                dtActivity.Constraints.Add("PK_ID", dtActivity.Columns["MasId"], true);

                Session["TblActivity"] = dtActivity;
            }

            if (Session["TblActivity2"] == null)
            {
                dtActivity2.Columns.Add("ComId");
                dtActivity2.Columns.Add("ComName");
                dtActivity2.Columns.Add("AtId");
                dtActivity2.Columns.Add("AtName");


                Session["TblActivity2"] = dtActivity2;
            }
            else
            {
                dtActivity2 = (DataTable)Session["TblActivity2"];
                if (dtActivity2.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dtActivity2.Rows[0]["AtId"].ToString()))
                    {
                        ChbAtType.DataBind();
                        ChbAtType.SelectedIndex = ChbAtType.Items.IndexOfValue(dtActivity2.Rows[0]["AtId"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dtActivity2.Rows[0]["ComId"].ToString()))
                    {
                        int comId = 0;

                        comId = int.Parse(dtActivity2.Rows[0]["ComId"].ToString());
                        //if (comId == 0)
                        //{
                        //    chbComId.Items[0].Selected = true;
                        //}
                        //else
                        //{
                        chbComId.DataBind();
                        int i;
                        for (i = 0; i < chbComId.Items.Count; i++)
                        {
                            int y = int.Parse(chbComId.Items[i].Value.ToString());

                            if ((y &= comId) == int.Parse(chbComId.Items[i].Value.ToString()))
                                chbComId.Items[i].Selected = true;
                        }

                    }

                }
            }
        }

        if (Session["TblActivity"] != null)
        {
            dtActivity = (DataTable)Session["TblActivity"];
            CustomAspxDevGridView1.DataSource = dtActivity;
            CustomAspxDevGridView1.DataBind();
        }


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        bool check = false;
        if (CustomAspxDevGridView1.VisibleRowCount > 0)
        {
            CustomAspxDevGridView1.DataSource = (DataTable)Session["TblActivity"];
            CustomAspxDevGridView1.DataBind();

            for (int i = 0; i < CustomAspxDevGridView1.VisibleRowCount; i++)
            {
                DataRowView dr = (DataRowView)CustomAspxDevGridView1.GetRow(i);
                if (dr["AsName"].ToString() == drdAtSubj.SelectedItem.Text)
                {
                    check = true;
                    break;
                }
            }
        }
        if (!check)
        {

            ((DataTable)Session["TblActivity"]).Rows.Add(new object[] { null, drdAtSubj.Value, drdAtSubj.SelectedItem.Text, txtPercent.Text, txtAtDesc.Text });
            //drdAtSubj.Focus();


        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "مدرک وارد شده تکراری می باشد";
            return;

        }

        CustomAspxDevGridView1.DataSource = (DataTable)Session["TblActivity"];
        CustomAspxDevGridView1.DataBind();
        dtActivity = (DataTable)Session["TblActivity"];

        if (dtActivity.Rows.Count > 0)
        {
            ASPxMenu1.Items.FindByName("Activity").Image.Url = "~/Images/icons/button_ok.png";
            ASPxMenu1.Items.FindByName("Activity").Image.Width = 15;
            ASPxMenu1.Items.FindByName("Activity").Image.Height = 15;
        }

        txtAtDesc.Text = "";
        txtPercent.Text = "";
        drdAtSubj.DataBind();
        drdAtSubj.SelectedIndex = -1;

    }
    //protected void btnRefresh_Click(object sender, EventArgs e)
    //{
    //    drdAtSubj.DataBind();
    //    drdAtSubj.SelectedIndex = -1;
    //    txtAtDesc.Text = "";
    //    txtPercent.Text = "";

    //    ChbAtType.DataBind();
    //    ChbAtType.SelectedIndex = -1;
    //    chbComId.DataBind();
    //    chbComId.SelectedIndex = -1;
    //}
    protected void CustomAspxDevGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;

        //CustomAspxDevGridView1.DataSource = (DataTable)Session["TblActivity"];
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

        dtActivity = (DataTable)Session["TblActivity"];
        //dtActivity.Rows[Id].Delete();
        dtActivity.Rows.Find(e.Keys[0]).Delete();

        Session["TblActivity"] = dtActivity;
        CustomAspxDevGridView1.DataSource = (DataTable)Session["TblActivity"];
        CustomAspxDevGridView1.DataBind();
        dtActivity = (DataTable)Session["TblActivity"];
        //btnRefresh_Click(this, new EventArgs());

        if (((DataTable)Session["TblActivity2"]).Rows.Count == 0 && dtActivity.Rows.Count == 0)
        {
            ASPxMenu1.Items.FindByName("Activity").Image.Url = "";
        }

        //}
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        object AtId = DBNull.Value;
        string AtName = "";
        if (ChbAtType.Value != null)
        {
            AtId = ChbAtType.Value;
            AtName = ChbAtType.SelectedItem.Text;

        }

        string commission = "";
        //0
        for (int i = 0; i < chbComId.Items.Count; i++)
        {
            if (chbComId.Items[i].Selected)
            {
                commission += (String.IsNullOrWhiteSpace(commission) == false) ? ", " : "";
                commission += chbComId.Items[i].Text;
            }
            // commission = commission + " , " + chbComId.Items[i].Text;
        }
        if (string.IsNullOrEmpty(commission))
            commission = "هیچکدام";
        else
            commission = commission.Remove(commission.Length - 1);

        int comId = 0;
        for (int i = 0; i < chbComId.Items.Count; i++)
        {
            if (chbComId.Items[i].Selected)
                comId = comId + int.Parse(chbComId.Items[i].Value.ToString());

        }

        //if (comId > 0)
        //    drMember["ComId"] = comId;
        //else
        //    drMember["ComId"] = 0;


        if (Session["TblActivity2"] == null || ((DataTable)Session["TblActivity2"]).Rows.Count == 0)
            ((DataTable)Session["TblActivity2"]).Rows.Add(new object[] { comId, commission, AtId, AtName });
        else
        {
            dtActivity2 = (DataTable)Session["TblActivity2"];
            dtActivity2.Rows[0]["ComId"] = comId;
            dtActivity2.Rows[0]["ComName"] = commission;
            dtActivity2.Rows[0]["AtId"] = AtId;
            dtActivity2.Rows[0]["AtName"] = AtName;

        }

        Response.Redirect("WizardMemberLanguage.aspx");
    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("WizardMemberJob.aspx");
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
        HiddenHelp["HelpAddress"] = "../../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.WizardMemberActivit).ToString());
    }
}
