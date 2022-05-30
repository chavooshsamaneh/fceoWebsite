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

public partial class Members_Office_OfficeShowInfo : System.Web.UI.Page
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
            try
            {
                OfficeId.Value = Server.HtmlDecode(Request.QueryString["OfId"]).ToString();
                OfficeRequest.Value = Server.HtmlDecode(Request.QueryString["OfReId"]).ToString();

            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string OfId = Utility.DecryptQS(OfficeId.Value);

            if (string.IsNullOrEmpty(OfId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            else
            {
                FillForm(int.Parse(OfId));
                ASPxRoundPanel2.Enabled = false;
            }
        }
    }
   
    protected void FillForm(int OfId)
    {
        try
        {
            TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
            OffManager.FindByCode(OfId);
            if (OffManager.Count == 1)
            {

                txtOfName.Text = OffManager[0]["OfName"].ToString();
                txtOfNameEn.Text = OffManager[0]["OfNameEn"].ToString();
                drdOfType.Value = OffManager[0]["OtId"].ToString();


                if (OffManager[0]["Tel1"].ToString() != "")
                {
                    if (OffManager[0]["Tel1"].ToString().IndexOf("-") > 0)
                    {
                        txtOfTel1_pre.Text = OffManager[0]["Tel1"].ToString().Substring(0, OffManager[0]["Tel1"].ToString().IndexOf("-"));
                        txtOfTel1.Text = OffManager[0]["Tel1"].ToString().Substring(OffManager[0]["Tel1"].ToString().IndexOf("-") + 1, OffManager[0]["Tel1"].ToString().Length - OffManager[0]["Tel1"].ToString().IndexOf("-") - 1);
                    }
                    else
                    {
                        txtOfTel1.Text = OffManager[0]["Tel1"].ToString();
                    }
                }
                if (OffManager[0]["Tel2"].ToString() != "")
                {
                    if (OffManager[0]["Tel2"].ToString().IndexOf("-") > 0)
                    {
                        txtOfTel2_pre.Text = OffManager[0]["Tel2"].ToString().Substring(0, OffManager[0]["Tel2"].ToString().IndexOf("-"));
                        txtOfTel2.Text = OffManager[0]["Tel2"].ToString().Substring(OffManager[0]["Tel2"].ToString().IndexOf("-") + 1, OffManager[0]["Tel2"].ToString().Length - OffManager[0]["Tel2"].ToString().IndexOf("-") - 1);
                    }
                    else
                    {
                        txtOfTel2.Text = OffManager[0]["Tel2"].ToString();
                    }
                }
                if (OffManager[0]["Fax"].ToString() != "")
                {
                    if (OffManager[0]["Fax"].ToString().IndexOf("-") > 0)
                    {
                        txtOfFax_pre.Text = OffManager[0]["Fax"].ToString().Substring(0, OffManager[0]["Fax"].ToString().IndexOf("-"));
                        txtOfFax.Text = OffManager[0]["Fax"].ToString().Substring(OffManager[0]["Fax"].ToString().IndexOf("-") + 1, OffManager[0]["Fax"].ToString().Length - OffManager[0]["Fax"].ToString().IndexOf("-") - 1);
                    }
                    else
                    {
                        txtOfFax.Text = OffManager[0]["Fax"].ToString();
                    }
                }
                txtOfMobile.Text = OffManager[0]["MobileNo"].ToString();
                txtOfEmail.Text = OffManager[0]["Email"].ToString();
                txtOfWebsite.Text = OffManager[0]["Website"].ToString();
                txtOfAddress.Text = OffManager[0]["Address"].ToString();
                txtOfSubject.Text = OffManager[0]["Subject"].ToString();
                txtOfRegDate.Text = OffManager[0]["RegDate"].ToString();
                txtOfRegNo.Text = OffManager[0]["RegOfNo"].ToString();
                txtOfRegPlace.Text = OffManager[0]["RegPlace"].ToString();
                txtOfStock.Text = OffManager[0]["Stock"].ToString();
                if (!string.IsNullOrEmpty(OffManager[0]["VolumeInvest"].ToString()))
                    txtOfValue.Text = Decimal.Parse(OffManager[0]["VolumeInvest"].ToString()).ToString("##");
                //txtOfFileNo.Text = OffManager[0]["FileNo"].ToString();
                txtOfDesc.Text = OffManager[0]["Description"].ToString();
                // drdMrsId.Value = OffManager[0]["MrsId"].ToString();

                if ((!Utility.IsDBNullOrNullValue(OffManager[0]["ArmUrl"])))
                {
                    imgOfArm.ImageUrl = OffManager[0]["ArmUrl"].ToString();

                }

                if ((!Utility.IsDBNullOrNullValue(OffManager[0]["SignUrl"])))
                {
                    imgOfSign.ImageUrl = OffManager[0]["SignUrl"].ToString();

                }

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";
            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد";
        }


    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
       
        Response.Redirect("Office.aspx");

    }
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
      
        switch (e.Item.Name)
        {
           
            case "Member":
                Response.Redirect("OfficeMember.aspx?OfId=" + OfficeId.Value + "&OfReId=" + OfficeRequest.Value);
                break;
            case "Job":
                Response.Redirect("OfficeJob.aspx?OfId=" + OfficeId.Value + "&OfReId=" + OfficeRequest.Value);
                break;
        }
        
    }
}
