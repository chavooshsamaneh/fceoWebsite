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
using System.IO;

public partial class Employee_Amoozesh_TechearSummery : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (Request.QueryString["PgMd"] == null || Request.QueryString["TeId"] == null)
            {
                Response.Redirect("Teachers.aspx");
            }

            MenuTeacherInfo.Enabled = true;

            HiddenFieldTeacher["TeacherId"] = Request.QueryString["TeId"].ToString();
            HiddenFieldTeacher["PrePageMode"] = Request.QueryString["PgMd"].ToString();
            string TeId = Utility.DecryptQS(HiddenFieldTeacher["TeacherId"].ToString());

            TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
            GridViewAttachment.DataSource = attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.Teachers, int.Parse(TeId));
            GridViewAttachment.DataBind();

            ObjdsTeacherResearch.SelectParameters[0].DefaultValue = TeId;
            ObjdsTeacherJobHistory.SelectParameters[0].DefaultValue = TeId;
            ObjdsTeacherLicence.SelectParameters[0].DefaultValue = TeId;

            //   string TeacherId = Utility.DecryptQS(HiddenFieldTeacher["TeacherId"].ToString());
            FillForm(int.Parse(TeId));

            TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
            TeacherManager.FindByCode(int.Parse(TeId));
            if (TeacherManager.Count > 0)
            {
                RoundPanelTeacherSummery.HeaderText = TeacherManager[0]["Name"].ToString() + " " + TeacherManager[0]["Family"].ToString();
            }
            else
            {
                Response.Redirect("Teachers.aspx");
                return;
            }
            SetObjectDataSourceResearch();

        }
    }

    protected void ASPxHyperLink1_DataBinding(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxHyperLink hp = (DevExpress.Web.ASPxHyperLink)sender;
        hp.Text = Path.GetFileName(hp.Value.ToString());
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Madrak":
                // string TeId = Utility.DecryptQS();
                Response.Redirect("TeachersLicence.aspx?TeacherId=" + HiddenFieldTeacher["TeacherId"] + "&PageMode=" + HiddenFieldTeacher["PrePageMode"]);
                break;
            case "Research":
                Response.Redirect("TeacherResearchAct.aspx?TeacherId=" + HiddenFieldTeacher["TeacherId"] + "&Pagemode=" + HiddenFieldTeacher["PrePageMode"]);
                break;
            case "Job":
                Response.Redirect("TeacherJobHistory.aspx?TeacherId=" + HiddenFieldTeacher["TeacherId"] + "&Pagemode=" + HiddenFieldTeacher["PrePageMode"]);
                break;
            case "Atachment":
                Response.Redirect("TeacherAttachment.aspx?TeId=" + HiddenFieldTeacher["TeacherId"] + "&PgMd=" + HiddenFieldTeacher["PrePageMode"]);
                break;
            case "BasicInfo":
                Response.Redirect("AddTeachers.aspx?TeId=" + HiddenFieldTeacher["TeacherId"] + "&PageMode=" + HiddenFieldTeacher["PrePageMode"]);
                break;
        }
    }

    protected void cmbMemberType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (Utility.DecryptQS(PgMode.Value) != "View")
        //{
        //    if ((Utility.DecryptQS(PgMode.Value) == "Edit") && CanChangeMemberType == false)
        //    {
        //        if (cmbMemberType.SelectedItem.Value.ToString() == "0")
        //            cmbMemberType.SelectedIndex = 1;
        //        else
        //            cmbMemberType.SelectedIndex = 0;
        //        this.DivReport.Visible = true;
        //        this.LabelWarning.Text = "امکان تغییر نوع عضویت وجود ندارد";
        //    }
        //    else
        //    {
        //        EmlpoyeeKindeChange = true;

        //        if (cmbMemberType.SelectedItem.Value.ToString() == "0")//Memebr
        //        {
        //            txtSSN.Enabled = false;
        //            lbIsMember.Visible = true;
        //            txtMeID.Visible = true;
        //            cmbName.Visible = true;

        //            lbNameFamily.Visible = true;
        //            cmbName.Visible = true;

        //            cmbName.SelectedIndex = 0;
        //            txtName.Visible = false;
        //            txtFamily.Visible = false;
        //            lblName.Visible = false;
        //            lblFamily.Visible = false;

        //            lblLastMajor.Visible = true;
        //            txtbLastMajor.Visible = true;

        //            lblMajor.Visible = false;
        //            cmbMajor.Visible = false;

        //            lblicence.Visible = false;
        //            cmbLicence.Visible = false;


        //            txtName.Enabled = false;
        //            txtFamily.Enabled = false;

        //            txtFatherName.Enabled = false;
        //            txtIdNo.Enabled = false;

        //            txtTel.Enabled = false;
        //            txtMobileNo.Enabled = false;
        //            txtBrithDate.Enabled = false;
        //            txtAddress.Enabled = false;
        //            txtEmail.Enabled = false;

        //            txtBrithDate.Text = "";
        //            txtDesc.Text = "";
        //            txtAddress.Text = "";
        //            txtEmail.Text = "";
        //            txtFamily.Text = "";
        //            txtFatherName.Text = "";
        //            //  txtFileNo.Text = "";
        //            txtIdNo.Text = "";
        //            txtMeID.Text = "";
        //            txtMobileNo.Text = "";
        //            txtName.Text = "";
        //            txtSSN.Text = "";
        //            txtTel.Text = "";

        //            lblLastMajor.Visible = true;
        //            txtbLastMajor.Visible = true;

        //            cmbLicence.SelectedIndex = -1;
        //            cmbMajor.SelectedIndex = -1;

        //            lblTiId.Visible = true;
        //            cmbTiId.Visible = true;

        //            cmbName.SelectedIndex = -1;
        //            cmbTiId.SelectedIndex = -1;

        //            chbInActive.Checked = false;
        //        }
        //        else//Is not Member
        //        {

        //            cmbName.Visible = false;
        //            lbIsMember.Visible = false;
        //            txtMeID.Visible = false;

        //            lbNameFamily.Visible = false;
        //            cmbName.Visible = false;
        //            lblName.Visible = true;
        //            lblFamily.Visible = true;
        //            txtName.Visible = true;
        //            txtFamily.Visible = true;

        //            txtName.Enabled = true;
        //            txtFamily.Enabled = true;
        //            txtFatherName.Enabled = true;
        //            txtIdNo.Enabled = true;
        //            txtSSN.Enabled = true;
        //            txtTel.Enabled = true;
        //            txtMobileNo.Enabled = true;
        //            txtBrithDate.Enabled = true;
        //            txtAddress.Enabled = true;
        //            txtEmail.Enabled = true;

        //            txtBrithDate.Text = "";
        //            txtDesc.Text = "";
        //            txtAddress.Text = "";
        //            txtEmail.Text = "";
        //            txtFamily.Text = "";
        //            txtFatherName.Text = "";
        //            // txtFileNo.Text = "";
        //            txtIdNo.Text = "";
        //            txtMeID.Text = "";
        //            txtMobileNo.Text = "";
        //            txtName.Text = "";
        //            txtSSN.Text = "";
        //            txtTel.Text = "";

        //            lblTiId.Visible = false;
        //            cmbTiId.Visible = false;

        //            lblLastMajor.Visible = false;
        //            txtbLastMajor.Visible = false;

        //            lblMajor.Visible = true;
        //            cmbMajor.Visible = true;

        //            lblicence.Visible = true;
        //            cmbLicence.Visible = true;


        //            cmbLicence.SelectedIndex = -1;
        //            cmbMajor.SelectedIndex = -1;

        //            cmbName.SelectedIndex = -1;
        //            cmbTiId.SelectedIndex = -1;

        //            chbInActive.Checked = true;
        //        }
        //    }
        //}
    }

    #endregion

    #region Methods
    protected void FillForm(int TeId)
    {
        //   string PageMode = Utility.DecryptQS(PgMode.Value);
        cmbMajor.DataBind();
        cmbTiId.DataBind();
        cmbLicence.DataBind();

        TSP.DataManager.TeacherManager manager = new TSP.DataManager.TeacherManager();
        manager.FindByCode(TeId);
        if (manager.Count == 1)
        {
            cmbTiId.SelectedIndex = cmbTiId.Items.IndexOfValue(manager[0]["TiId"].ToString());
            txtName.Text = manager[0]["Name"].ToString();
            txtFamily.Text = manager[0]["Family"].ToString();
            txtFatherName.Text = manager[0]["Father"].ToString();
            txtBrithDate.Text = manager[0]["BirthDate"].ToString();
            txtIdNo.Text = manager[0]["IdNo"].ToString();
            txtSSN.Text = manager[0]["SSN"].ToString();
            txtTel.Text = manager[0]["Tel"].ToString();
            txtMobileNo.Text = manager[0]["MobileNo"].ToString();
            txtAddress.Text = manager[0]["Address"].ToString();
            txtEmail.Text = manager[0]["Email"].ToString();
            txtFileNo.Text = manager[0]["FileNo"].ToString();
            cmbLicence.SelectedIndex = cmbLicence.Items.IndexOfValue(manager[0]["LiId"].ToString());
            cmbMajor.SelectedIndex = cmbMajor.Items.IndexOfValue(manager[0]["MjId"].ToString());
            txtDesc.Text = manager[0]["Description"].ToString();
            chbInActive.Checked = Convert.ToBoolean(manager[0]["InActive"].ToString());
            txtFileNo.Text = manager[0]["FileNo"].ToString();
            txtFileDate.Text = manager[0]["FileDate"].ToString();
            if (!string.IsNullOrEmpty(manager[0]["MeId"].ToString()))
            {
                cmbMemberType.SelectedIndex = 0;
                cmbMemberType_SelectedIndexChanged(this, new EventArgs());
                txtMeID.Text = manager[0]["MeID"].ToString();
                ////
                ObjectDataSourceMember.SelectParameters[2].DefaultValue = manager[0]["MeId"].ToString();
                cmbName.DataBind();
                cmbName.SelectedIndex = 0;
                FillFormByMeIdCmb(int.Parse(txtMeID.Text));

                // txtMeID.Text = manager[0]["MeId"].ToString();
            }
            else
            {
                cmbMemberType.SelectedIndex = 1;
                cmbMemberType_SelectedIndexChanged(this, new EventArgs());
            }

        }
    }

    protected void FillFormByMeIdCmb(int MeId)
    {
        cmbMajor.DataBind();
        cmbTiId.DataBind();
        cmbLicence.DataBind();
        cmbName.DataBind();

        TSP.DataManager.MemberManager MemManager = new TSP.DataManager.MemberManager();
        MemManager.FindByCode(MeId);
        if (MemManager.Count > 0)
        {
            cmbTiId.SelectedIndex = cmbTiId.Items.IndexOfValue(MemManager[0]["TiId"].ToString());
            txtMeID.Text = MeId.ToString();
            txtName.Text = MemManager[0]["FirstName"].ToString();
            txtFamily.Text = MemManager[0]["LastName"].ToString();
            txtFatherName.Text = MemManager[0]["FatherName"].ToString();
            txtBrithDate.Text = MemManager[0]["BirhtDate"].ToString();
            txtIdNo.Text = MemManager[0]["IdNo"].ToString();
            txtSSN.Text = MemManager[0]["SSN"].ToString();
            txtTel.Text = MemManager[0]["HomeTel"].ToString();
            txtMobileNo.Text = MemManager[0]["MobileNo"].ToString();
            txtAddress.Text = MemManager[0]["HomeAdr"].ToString();
            txtEmail.Text = MemManager[0]["Email"].ToString();
            txtbLastMajor.Text = MemManager[0]["LastMjName"].ToString();
            LastMjId.Value = MemManager[0]["LastMjId"].ToString();
            LastLiId.Value = MemManager[0]["LastLiId"].ToString();
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "عضوی با کد عضویت داده شده وجود ندارد.";
        }
    }

    private void SetObjectDataSourceResearch()
    {
        TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        string TeId = Utility.DecryptQS(HiddenFieldTeacher["TeacherId"].ToString());
        TeacherManager.FindByCode(int.Parse(TeId));

        if (TeacherManager.Count > 0)
        {
            if (!string.IsNullOrEmpty(TeacherManager[0]["MeId"].ToString()))
            {
                ObjdsMemberResearchActivity.DataBind();
                GridViewTeacherResearch.DataSource = ObjdsMemberResearchActivity;
                GridViewTeacherResearch.KeyFieldName = "MraId";
                ObjdsMemberResearchActivity.SelectParameters[0].DefaultValue = TeacherManager[0]["MeId"].ToString();
                GridViewTeacherResearch.DataBind();
            }
            else
            {
                ObjdsTeacherResearch.DataBind();
                GridViewTeacherResearch.DataSource = ObjdsTeacherResearch;
                GridViewTeacherResearch.KeyFieldName = "TResearchId";
                ObjdsTeacherResearch.SelectParameters[0].DefaultValue = TeId;
                GridViewTeacherResearch.DataBind();
            }
        }
    }


    #endregion
}
