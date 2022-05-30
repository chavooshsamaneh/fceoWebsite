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

public partial class Members_Office_OfficeMemberShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Login"] == null || Session["Login"].ToString() == "0")
        //{
        //    Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
        //    return;
        //}

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["OfReId"]) || string.IsNullOrEmpty(Request.QueryString["OfId"]))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            try
            {
                OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
                OffMemberId.Value = Server.HtmlDecode(Request.QueryString["OfmId"]).ToString();
                OffMeType.Value = Server.HtmlDecode(Request.QueryString["OfmType"]).ToString();
                OfPersonId.Value = Server.HtmlDecode(Request.QueryString["PersonId"]).ToString();
                OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();


            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string OfId = Utility.DecryptQS(OfficeId.Value);
            string OfmId = Utility.DecryptQS(OffMemberId.Value);
            string OfmType = Utility.DecryptQS(OffMeType.Value);
            string PersonId = Utility.DecryptQS(OfPersonId.Value);
            string OfReId = Utility.DecryptQS(OfficeRequest.Value);

            if ((string.IsNullOrEmpty(OfId)) || (string.IsNullOrEmpty(OfReId)))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }


            TSP.DataManager.OfficeManager OfManager = new TSP.DataManager.OfficeManager();
            OfManager.FindByCode(int.Parse(OfId));
            if (OfManager.Count > 0)
                lblOfName.Text = OfManager[0]["OfName"].ToString();
            FillForm(int.Parse(OfmId), int.Parse(OfmType), int.Parse(PersonId));
            ASPxRoundPanel2.Enabled = false;
        }
    }
    protected void FillForm(int OfmId, int OfmType, int PersonId)
    {
        //member
        if (OfmType == 1)
        {
            ComboType.SelectedIndex = 0;
            SetMember();
            FillMember(OfmId, PersonId);
        }
        //kardan
        else if (OfmType == 2)
        {
            ComboType.SelectedIndex = 1;
            SetKardanMemar();
            FillOthers(OfmId, PersonId);
        }
        //Other
        else if (OfmType == 3)
        {
            ComboType.SelectedIndex = 3;
            SetOther();
            FillOthers(OfmId, PersonId);
        }
        //Memar
        else if (OfmType == 4)
        {
            ComboType.SelectedIndex = 2;
            SetKardanMemar();
            FillOthers(OfmId, PersonId);
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
                // txtDesc.Text = MeManager[0]["Description"].ToString();
                txtMeNo.Text = PersonId.ToString();
                txtMobile.Text = MeManager[0]["MobileNo"].ToString();
                if (!Utility.IsDBNullOrNullValue(MeManager[0]["HomeTel"]))
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
                txtAddress.Text = MeManager[0]["HomeAdr"].ToString();

                if (!Utility.IsDBNullOrNullValue(MeManager[0]["ImageUrl"]))
                {
                    imgMember.ImageUrl = MeManager[0]["ImageUrl"].ToString();

                }

                OfMeManager.FindByCode(OfmId);
                if (OfMeManager.Count == 1)
                {

                    drdPosition.DataBind();
                    drdPosition.SelectedIndex = drdPosition.Items.IndexOfValue(OfMeManager[0]["OfpId"].ToString());

                    ComboTime.DataBind();
                    if (Convert.ToBoolean(OfMeManager[0]["IsFullTime"]) == true)
                        ComboTime.SelectedIndex = 1;
                    else
                        ComboTime.SelectedIndex = 0;

                    txtStartDate.Text = OfMeManager[0]["StartDate"].ToString();
                    //txtEndDate.Text = OfMeManager[0]["EndDate"].ToString();
                    chbHaghEmza.Checked = (bool)OfMeManager[0]["HasSignRight"];
                    if (chbHaghEmza.Checked)
                    {
                        imgEmza.ClientVisible = true;
                        imgEmza.ImageUrl = OfMeManager[0]["SignUrl"].ToString();


                    }
                    else
                    {
                        imgEmza.ClientVisible = false;

                    }

                    txtDesc.Text = OfMeManager[0]["Description"].ToString();

                }
                MeFileManager.SelectLastVersion(PersonId, 0);
                if (MeFileManager.Count > 0)
                {
                    int MfId = int.Parse(MeFileManager[0]["MfId"].ToString());
                    CustomAspxDevGridView1.DataSource = MeFileDetailManager.SelectById(MfId, PersonId, 0);
                    CustomAspxDevGridView1.KeyFieldName = "MfdId";
                    CustomAspxDevGridView1.DataBind();
                }

            }
            catch
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";

            }
        }
    }
    protected void FillOthers(int OfmId, int PersonId)
    {

        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.OtherPersonManager OthManager = new TSP.DataManager.OtherPersonManager();


        OthManager.FindByCode(PersonId);

        if (OthManager.Count == 1)
        {
            try
            {

                txtOtpCode.Text = OthManager[0]["OtpCode"].ToString();
                txtFirstName.Text = OthManager[0]["FirstName"].ToString();
                txtLastName.Text = OthManager[0]["LastName"].ToString();
                txtFatherName.Text = OthManager[0]["FatherName"].ToString();
                txtIdNo.Text = OthManager[0]["IdNo"].ToString();
                txtSSN.Text = OthManager[0]["SSN"].ToString();
                txtBirthPlace.Text = OthManager[0]["BirthPlace"].ToString();
                txtBirthDate.Text = OthManager[0]["BirthDate"].ToString();
                txtFileNo.Text = OthManager[0]["FileNo"].ToString();
                txtFileNoDate.Text = OthManager[0]["FileNoDate"].ToString();
                if (OthManager[0]["Tel"].ToString() != "")
                {
                    if (OthManager[0]["Tel"].ToString().IndexOf("-") > 0)
                    {
                        txtTel_pre.Text = OthManager[0]["Tel"].ToString().Substring(0, OthManager[0]["Tel"].ToString().IndexOf("-"));
                        txtTel.Text = OthManager[0]["Tel"].ToString().Substring(OthManager[0]["Tel"].ToString().IndexOf("-") + 1, OthManager[0]["Tel"].ToString().Length - OthManager[0]["Tel"].ToString().IndexOf("-") - 1);
                    }
                    else
                    {
                        txtTel.Text = OthManager[0]["Tel"].ToString();
                    }
                }
                txtMobile.Text = OthManager[0]["MobileNo"].ToString();
                txtAddress.Text = OthManager[0]["Address"].ToString();
                if (!Utility.IsDBNullOrNullValue(OthManager[0]["ImgUrl"]))
                {
                    imgMember.ClientVisible = true;
                    imgMember.ImageUrl = OthManager[0]["ImgUrl"].ToString();
                }

                if (!Utility.IsDBNullOrNullValue(OthManager[0]["MjId"]))
                {
                    ComboMjId.DataBind();
                    ComboMjId.SelectedIndex = ComboMjId.Items.IndexOfValue(OthManager[0]["MjId"].ToString());

                }
                txtMjName.Text = OthManager[0]["MjName"].ToString();

                OfMeManager.FindByCode(OfmId);
                if (OfMeManager.Count == 1)
                {

                    ComboTime.DataBind();
                    if (Convert.ToBoolean(OfMeManager[0]["IsFullTime"]) == true)
                        ComboTime.SelectedIndex = 1;
                    else
                        ComboTime.SelectedIndex = 0;


                    drdPosition.DataBind();
                    drdPosition.SelectedIndex = drdPosition.Items.IndexOfValue(OfMeManager[0]["OfpId"].ToString());
                    txtStartDate.Text = OfMeManager[0]["StartDate"].ToString();
                    // txtEndDate.Text = OfMeManager[0]["EndDate"].ToString();
                    chbHaghEmza.Checked = (bool)OfMeManager[0]["HasSignRight"];
                    if (chbHaghEmza.Checked)
                    {
                        imgEmza.ClientVisible = true;
                        lbEmza.ClientVisible = true;
                        imgEmza.ImageUrl = OfMeManager[0]["SignUrl"].ToString();


                    }
                    else
                    {
                        imgEmza.ClientVisible = true;
                        lbEmza.ClientVisible = true;
                    }

                    txtDesc.Text = OfMeManager[0]["Description"].ToString();

                }
                if (OthManager[0]["OtpType"].ToString() == "1" || OthManager[0]["OtpType"].ToString() == "3")
                {
                    ASPxRoundPanelGrade.ClientVisible = true;

                    TSP.DataManager.DocOffMemberAcceptedGradeManager GradeManager = new TSP.DataManager.DocOffMemberAcceptedGradeManager();
                    CustomAspxDevGridView1.DataSource = GradeManager.FindByOtpId(PersonId);
                    CustomAspxDevGridView1.KeyFieldName = "MGId";
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
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
        }
    }
    protected void SetMember()
    {
        lblMeNo.ClientVisible = true;
        txtMeNo.ClientVisible = true;
        txtOtpCode.ClientVisible = false;
        lblOtpCode.ClientVisible = false;

        ASPxLabel5.ClientVisible = false;
        ASPxLabel6.ClientVisible = false;
        ComboMjId.ClientVisible = false;
        txtMjName.ClientVisible = false;
        ASPxRoundPanelGrade.ClientVisible = true;
    }
    protected void SetKardanMemar()
    {
        lblMeNo.ClientVisible = false;
        txtMeNo.ClientVisible = false;
        txtOtpCode.ClientVisible = true;
        lblOtpCode.ClientVisible = true;

        ASPxLabel5.ClientVisible = true;
        ASPxLabel6.ClientVisible = true;
        ComboMjId.ClientVisible = true;
        txtMjName.ClientVisible = true;

        ASPxRoundPanelGrade.ClientVisible = true;

    }
    protected void SetOther()
    {
        lblMeNo.ClientVisible = false;
        txtMeNo.ClientVisible = false;
        txtOtpCode.ClientVisible = false;
        lblOtpCode.ClientVisible = false;

      
        ASPxLabel5.ClientVisible = true;
        ASPxLabel6.ClientVisible = true;
        ComboMjId.ClientVisible = true;
        txtMjName.ClientVisible = true;

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("OfficeMember.aspx?OfId=" + OfficeId.Value  + "&OfReId=" + OfficeRequest.Value);

    }
}
