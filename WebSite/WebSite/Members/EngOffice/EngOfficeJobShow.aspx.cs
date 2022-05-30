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

public partial class Members_EngOffice_EngOfficeJobShow : System.Web.UI.Page
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
            if (string.IsNullOrEmpty(Request.QueryString["EOfId"]) || string.IsNullOrEmpty(Request.QueryString["EngOfId"]))
            {
                Response.Redirect("EngOfficeJob.aspx");
                return;
            }
            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                EngOfficeId.Value = Server.HtmlDecode(Request.QueryString["EngOfId"]).ToString();
                JobId.Value = Server.HtmlDecode(Request.QueryString["JhId"]).ToString();
                EngFileId.Value = Server.HtmlDecode(Request.QueryString["EOfId"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string EngOfId = Utility.DecryptQS(EngOfficeId.Value);
            string JhId = Utility.DecryptQS(JobId.Value);
            string EOfId = Utility.DecryptQS(EngFileId.Value);


            if (string.IsNullOrEmpty(JhId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }



            FillForm(int.Parse(JhId));
            ASPxRoundPanel2.Enabled = false;
        }
    }
    protected void FillForm(int JhId)
    {
        TSP.DataManager.ProjectJobHistoryManager JhManager = new TSP.DataManager.ProjectJobHistoryManager();
        JhManager.FindByCode(JhId);
        if (JhManager.Count > 0)
        {
            txtjCity.Text = JhManager[0]["CitName"].ToString();
            txtjCoEndDate.Text = JhManager[0]["EndCorporateDate"].ToString();
            txtjCoStartDate.Text = JhManager[0]["StartCorporateDate"].ToString();
            txtjDesc.Text = JhManager[0]["Description"].ToString();
            txtjEmployer.Text = JhManager[0]["Employer"].ToString();
            txtjEndStatus.Text = JhManager[0]["StatusOfEndDate"].ToString();
            txtjPrName.Text = JhManager[0]["ProjectName"].ToString();
            txtjPrVolume.Text = JhManager[0]["ProjectVolume"].ToString();
            txtjStartDate.Text = JhManager[0]["StartOriginalDate"].ToString();
            txtjStartStatus.Text = JhManager[0]["StatusOfStartDate"].ToString();

            if (!Utility.IsDBNullOrNullValue(JhManager[0]["PJPId"]))
            {
                ComboPosition.DataBind();
                ComboPosition.SelectedIndex = ComboPosition.Items.IndexOfValue(JhManager[0]["PJPId"].ToString());

            }
            if (!Utility.IsDBNullOrNullValue(JhManager[0]["CounId"]))
            {
                CombojCountry.DataBind();
                CombojCountry.SelectedIndex = CombojCountry.Items.IndexOfValue(JhManager[0]["CounId"].ToString());
            }
            if (!Utility.IsDBNullOrNullValue(JhManager[0]["CorTypeId"]))
            {
                CombojIsCorporate.DataBind();
                //CombojIsCorporate.SelectedIndex = CombojIsCorporate.Items.IndexOfValue(JhManager[0]["CorTypeId"].ToString());
                CombojIsCorporate.Value = JhManager[0]["CorTypeId"];
            }
            if (!Utility.IsDBNullOrNullValue(JhManager[0]["PrTypeId"]))
            {
                if (JhManager[0]["PrTypeId"].ToString() == "1")
                {
                    ASPxLabel22.ClientVisible = true;
                    ASPxLabel23.ClientVisible = true;
                    txtjArea.ClientVisible = true;
                    txtjFloor.ClientVisible = true;
                    ASPxLabel10.ClientVisible = true;
                    CombojSazeType.ClientVisible = true;
                    txtjArea.Text = JhManager[0]["Area"].ToString();
                    txtjFloor.Text = JhManager[0]["Floors"].ToString();

                }
                CombojPrType.DataBind();
                CombojPrType.SelectedIndex = CombojPrType.Items.IndexOfValue(JhManager[0]["PrTypeId"].ToString());

                if (!Utility.IsDBNullOrNullValue(JhManager[0]["SazeTypeId"]))
                {
                    CombojSazeType.DataBind();
                    CombojSazeType.SelectedIndex = CombojSazeType.Items.IndexOfValue(JhManager[0]["SazeTypeId"].ToString());
                }
            }


        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد ";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("EngOfficeJob.aspx?EOfId=" + EngFileId.Value + "&PageMode=" + PgMode.Value + "&EngOfId=" + EngOfficeId.Value);

    }
}
