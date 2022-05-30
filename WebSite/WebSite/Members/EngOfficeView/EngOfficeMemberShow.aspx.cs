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
using DevExpress.Web;
using System.IO;

public partial class Members_EngOfficeView_EngOfficeMemberShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(GetType(), "key1", "<script> but.DoClick();</script>");
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            ODBPosition.FilterParameters[0].DefaultValue = "1";

            Session["IsEdited_OffAaza"] = false;

            if (string.IsNullOrEmpty(Request.QueryString["EOfId"]) || string.IsNullOrEmpty(Request.QueryString["EngOfId"]))
            {
                Response.Redirect("Office1.aspx");
                return;
            }

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["aPageMode"].ToString());
                EngOfficeId.Value = Server.HtmlDecode(Request.QueryString["EngOfId"]).ToString();
                OffMemberId.Value = Server.HtmlDecode(Request.QueryString["OfmId"]).ToString();
                OfPersonId.Value = Server.HtmlDecode(Request.QueryString["PersonId"]).ToString();
                EngFileId.Value = Server.HtmlDecode(Request.QueryString["EOfId"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string EngOfId = Utility.DecryptQS(EngOfficeId.Value);
            string OfmId = Utility.DecryptQS(OffMemberId.Value);
            string PersonId = Utility.DecryptQS(OfPersonId.Value);
            string EOfId = Utility.DecryptQS(EngFileId.Value);

            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            switch (PageMode)
            {
                case "View":
                    if (string.IsNullOrEmpty(OfmId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    FillMember(int.Parse(OfmId), int.Parse(PersonId));
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    ASPxRoundPanel2.Enabled = false;
                    break;
            }
        }
    }
    protected void FillMember(int OfmId, int PersonId)
    {

        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.DocMemberFileManager MeFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberFileDetailManager MeFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();

        MeManager.FindByCode(PersonId);

        if (MeManager.Count == 1)
        {
            try
            {
                txtFirstName.Text = MeManager[0]["FirstName"].ToString();
                txtLastName.Text = MeManager[0]["LastName"].ToString();
                txtFatherName.Text = MeManager[0]["FatherName"].ToString();
                txtIdNo.Text = MeManager[0]["IdNo"].ToString();
                txtSSN.Text = MeManager[0]["SSN"].ToString();
                txtBirthPlace.Text = MeManager[0]["BirthPlace"].ToString();
                txtBirthDate.Text = MeManager[0]["BirhtDate"].ToString();
                txtFileNo.Text = MeManager[0]["FileNo"].ToString();
                txtMeNo.Text = PersonId.ToString();
                txtMobile.Text = MeManager[0]["MobileNo"].ToString();
                //txtTel.Text = MeManager[0]["HomeTel"].ToString();
                txtAddress.Text = MeManager[0]["HomeAdr"].ToString();

                if (MeManager[0]["HomeTel"].ToString() != "")
                {
                    if (MeManager[0]["HomeTel"].ToString().IndexOf("-") > 0)
                    {
                        txtTel_pre.Text = MeManager[0]["HomeTel"].ToString().Substring(0, MeManager[0]["HomeTel"].ToString().IndexOf("-"));
                        txtTel.Text = MeManager[0]["HomeTel"].ToString().Substring(MeManager[0]["HomeTel"].ToString().IndexOf("-") + 1, MeManager[0]["HomeTel"].ToString().Length - MeManager[0]["HomeTel"].ToString().IndexOf("-") - 1);
                    }
                    else
                    {
                        txtTel.Text = MeManager[0]["HomeTel"].ToString();
                    }
                }

                if (!Utility.IsDBNullOrNullValue(MeManager[0]["ImageUrl"]))
                    imgMember.ImageUrl = MeManager[0]["ImageUrl"].ToString();

                if (!Utility.IsDBNullOrNullValue(MeManager[0]["SignUrl"]))
                    imgSign.ImageUrl = MeManager[0]["SignUrl"].ToString();

                if (!string.IsNullOrEmpty(MeManager[0]["LastMjId"].ToString()))
                {
                    ComboMjId.DataBind();
                    ComboMjId.SelectedIndex = ComboMjId.Items.IndexOfValue(MeManager[0]["LastMjId"].ToString());
                    txtMjName.Text = MeManager[0]["LastLiName"].ToString();
                }

                //OfMeManager.FindByCode(OfmId);
                OfMeManager.selectEngOfficeMember(-1, -1, OfmId);
                if (OfMeManager.Count == 1)
                {
                    if (!Utility.IsDBNullOrNullValue(OfMeManager[0]["OfpId"]))
                    {
                        drdPosition.DataBind();
                        drdPosition.SelectedIndex = drdPosition.Items.IndexOfValue(OfMeManager[0]["OfpId"].ToString());
                    }

                    ComboTime.DataBind();
                    if (Convert.ToBoolean(OfMeManager[0]["IsFullTime"]) == true)
                        ComboTime.SelectedIndex = 1;
                    else
                        ComboTime.SelectedIndex = 0;

                    txtStartDate.Text = OfMeManager[0]["StartDate"].ToString();
                    //txtEndDate.Text = OfMeManager[0]["EndDate"].ToString();
                    chbHaghEmza.Checked = (bool)OfMeManager[0]["HasSignRight"];
                    txtDesc.Text = OfMeManager[0]["Description"].ToString();

                }
                MeFileManager.SelectLastVersion(PersonId, 0);
                if (MeFileManager.Count > 0)
                {
                    int MfId = int.Parse(MeFileManager[0]["MfId"].ToString());
                    CustomAspxDevGridView1.DataSource = MeFileDetailManager.SelectById(MfId, PersonId, 0);
                    //CustomAspxDevGridView1.KeyFieldName = "MfdId";
                    CustomAspxDevGridView1.DataBind();
                }
            }
            catch
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات توسط کاربر دیگری تغییر کرده است";
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EngOfficeMember.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&EOfId=" + EngFileId.Value);

    }
}