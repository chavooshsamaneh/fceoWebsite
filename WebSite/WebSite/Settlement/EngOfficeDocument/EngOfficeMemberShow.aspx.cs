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

public partial class Settlement_EngOfficeDocument_EngOfficeMemberShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                EngOfficeId.Value = Server.HtmlDecode(Request.QueryString["EngOfId"]).ToString();
                OffMemberId.Value = Server.HtmlDecode(Request.QueryString["OfmId"]).ToString();
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
            string EOfId = Utility.DecryptQS(EngFileId.Value);

            if ((string.IsNullOrEmpty(EngOfId)) || (string.IsNullOrEmpty(PageMode)))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;

            }


            FillMember(int.Parse(OfmId));
        }
    }
    protected void FillMember(int OfmId)
    {

        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.DocMemberFileManager MeFileManager = new TSP.DataManager.DocMemberFileManager();
        TSP.DataManager.DocMemberFileDetailManager MeFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();

        try
        {
            OfMeManager.selectEngOfficeMember(-1, -1, OfmId);
            if (OfMeManager.Count == 1)
            {
                int PersonId = int.Parse(OfMeManager[0]["PersonId"].ToString());

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
                chbHaghEmza.Checked = (bool)OfMeManager[0]["HasSignRight"];
                if (chbHaghEmza.Checked)
                {
                    imgEmza.ClientVisible = true;
                    imgEmza.ImageUrl = OfMeManager[0]["SignUrl"].ToString();

                }
                else
                    imgEmza.ClientVisible = false;
              

                txtDesc.Text = OfMeManager[0]["Description"].ToString();

                MeManager.FindByCode(PersonId);
                if (MeManager.Count == 1)
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


                    MeFileManager.SelectLastVersion(PersonId, 0);
                    if (MeFileManager.Count > 0)
                    {
                        int MfId = int.Parse(MeFileManager[0]["MfId"].ToString());
                        CustomAspxDevGridView1.DataSource = MeFileDetailManager.SelectById(MfId, PersonId, 0);
                        //CustomAspxDevGridView1.KeyFieldName = "MfdId";
                        CustomAspxDevGridView1.DataBind();
                    }

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات توسط کاربر دیگری تغییر کرده است";
                }
            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EngOfficeMember.aspx?EngOfId=" + EngOfficeId.Value + "&PageMode=" + PgMode.Value + "&EOfId=" + EngFileId.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);

    }
}
