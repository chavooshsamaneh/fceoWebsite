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

public partial class Members_Amoozesh_LicenceView : System.Web.UI.Page
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

            if (string.IsNullOrEmpty(Request.QueryString["MdId"]))
            {
                Response.Redirect("Licence.aspx");
            }

            try
            {
                MadrakId.Value = Server.HtmlDecode(Request.QueryString["MdId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string MdId = Utility.DecryptQS(MadrakId.Value);
            FillForm(int.Parse(MdId));
        }


        btnBack.PostBackUrl = "Licence.aspx";
        ASPxButton6.PostBackUrl = "Licence.aspx";

    }
    protected void FillForm(int MdId)
    {

        TSP.DataManager.MadrakManager manager = new TSP.DataManager.MadrakManager();
        manager.FindByCode(MdId);
        if (manager.Count == 1)
        {
            txtLiNo.Text = manager[0]["LicenceNo"].ToString();
            txtLiDate.Text = manager[0]["LicenceDate"].ToString();
            txtMeFirstName.Text = manager[0]["FirstName"].ToString();
            txtMeLastName.Text = manager[0]["LastName"].ToString();
            txtMeNo.Text = manager[0]["MeId"].ToString();
            HpFile.NavigateUrl = manager[0]["FilePath"].ToString();

            ComboType.DataBind();
            ComboType.SelectedIndex = ComboType.Items.IndexOfValue(manager[0]["Type"].ToString());
            ComboMdType.DataBind();
            ComboMdType.SelectedIndex = ComboMdType.Items.IndexOfValue(manager[0]["MdType"].ToString());
            ComboPrId.DataBind();
            ComboPrId.SelectedIndex = ComboPrId.Items.IndexOfValue(manager[0]["ProvinceId"].ToString());

            if (ComboType.Value.ToString() == "0")//period
            {
                if (!this.Page.IsStartupScriptRegistered("Key"))
                    this.Page.RegisterStartupScript("Key", "<script>SetPeriod();</script>");


                ComboCrsName.DataBind();
                ComboCrsName.SelectedIndex = ComboCrsName.Items.IndexOfValue(manager[0]["CrsId"]);

                txtPPDuration.Text = manager[0]["Duration"].ToString();
                txtPPTeName.Text = manager[0]["TeName"].ToString();
                txtTeFileNo.Text = manager[0]["TeFileNo"].ToString();
                txtInsRegNo.Text = manager[0]["InsRegNo"].ToString();
                txtInsRegDate.Text = manager[0]["InsRegDate"].ToString();
                txtTestDate.Text = manager[0]["TestDate"].ToString();
                txtPPCode.Text = manager[0]["PPCode"].ToString();
                txtTimeMark.Text = manager[0]["TimeMark"].ToString();
                txtTotalMark.Text = manager[0]["TotalMark"].ToString();
            }
            else
            {
                if (!this.Page.IsStartupScriptRegistered("Key1"))
                    this.Page.RegisterStartupScript("Key1", "<script>SetSeminar();</script>");

                txtSeName.Text = manager[0]["SeName"].ToString();
                txtSeDuration.Text = manager[0]["Duration"].ToString();
                txtSeTeName.Text = manager[0]["TeName"].ToString();

            }
            txtStartDate.Text = manager[0]["StartDate"].ToString();
            txtEndDate.Text = manager[0]["EndDate"].ToString();
            txtInsName.Text = manager[0]["InsName"].ToString();
            txtDesc.Text = manager[0]["Description"].ToString();

        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Licence.aspx");

    }
}
