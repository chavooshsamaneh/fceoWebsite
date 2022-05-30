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


public partial class Settlement_OfficeDocument_OfficeRequestShow : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            if (Request.QueryString["OfId"] == null || Request.QueryString["OfReId"] == null )
            {
                Response.Redirect("OfficeRequest.aspx");
            }

            if (!IsPostBack)
            {
                ViewState["postids"] = System.Guid.NewGuid().ToString();
                Session["postid"] = ViewState["postids"].ToString();
            }
            else
            {
                if (!IsCallback && Session["postid"] != null)
                {
                    if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                    Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
                }
            }

            OfficeId.Value = Request.QueryString["OfId"].ToString();
            OfficeRequest.Value = Request.QueryString["OfReId"].ToString();
            PgMode.Value = Request.QueryString["PageMode"].ToString();
            string PageMode = Utility.DecryptQS(PgMode.Value);
            string OfReId = Utility.DecryptQS(OfficeRequest.Value);
            string OfId = Utility.DecryptQS(OfficeId.Value);

            if (string.IsNullOrEmpty(PageMode) || string.IsNullOrEmpty(OfReId) || string.IsNullOrEmpty(OfId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            switch (PageMode)
            {
                case "View":
                    FillForm(int.Parse(OfId), int.Parse(OfReId));
                    txtdLastRegDate.Enabled = false;
                    txtdSerialNo.Enabled = false;
                    txtdExpDate.Enabled = false;
                    cmbdIsTemporary.Enabled = false;
                    txtReRequestDesc.Enabled = false;
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
                case "Edit":
                    FillForm(int.Parse(OfId), int.Parse(OfReId));                 
                    txtdSerialNo.Enabled = true;
                    txtdExpDate.Enabled = true;
                    txtdLastRegDate.Enabled = true;
                    cmbdIsTemporary.Enabled = true;
                    txtReRequestDesc.Enabled = true;

                    ASPxRoundPanel2.HeaderText = "ویرایش";

                    break;
            }


            this.ViewState["BtnSave"] = btnSave.Enabled;
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string OfReId = Utility.DecryptQS(OfficeRequest.Value);

        if (string.IsNullOrEmpty(OfReId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            Edit(int.Parse(OfReId));
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string OfId = Utility.DecryptQS(OfficeId.Value);
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(OfId))//&& !string.IsNullOrEmpty(Request.QueryString["SrchFlt"])
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            //string SrchFlt = Server.HtmlDecode(Request.QueryString["SrchFlt"].ToString());
            Response.Redirect("OfficeRequest.aspx?PostId=" + OfficeId.Value + "&GrdFlt=" + GrdFlt);// + "&SrchFlt=" + SrchFlt);
        }
        else
        {
            Response.Redirect("OfficeRequest.aspx");
        }
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
       
        switch (e.Item.Name)
        {
            case "Agent":
                Response.Redirect("OfficeAgent.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);// + "&SrchFlt=" + SrchFlt);
                break;
            case "Member":
                Response.Redirect("OfficeMembers.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Letters":
                Response.Redirect("OfficeLetters.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Financial":
                Response.Redirect("OfficeFinancialStatus.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Job":
                Response.Redirect("OfficeJob.aspx?OfId=" + OfficeId.Value + "&PageMode=" + PgMode.Value + "&OfReId=" + OfficeRequest.Value + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
        }

    }
    #endregion

    #region Methods

    protected void FillForm(int OfId, int OfReId)
    {
        TSP.DataManager.OfficeManager OffManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        TSP.DataManager.OfficeRequestManager ReqManager2 = new TSP.DataManager.OfficeRequestManager();

        try
        {
            OffManager.FindByCode(OfId);
            if (OffManager.Count == 1)
            {
                //txtOfAttType.Text = OffManager[0]["OatName"].ToString();

                txtOfDesc.Text = OffManager[0]["Description"].ToString();


                ReqManager.FindByCode(OfReId);
                if (ReqManager.Count > 0)
                {
                    txtOfName.Text = ReqManager[0]["OfName"].ToString();
                    txtOfNameEn.Text = ReqManager[0]["OfNameEn"].ToString();
                    txtOfMobile.Text = ReqManager[0]["MobileNo"].ToString();
                    txtOfAddress.Text = ReqManager[0]["Address"].ToString();
                    txtOfWebsite.Text = ReqManager[0]["Website"].ToString();
                    txtOfEmail.Text = ReqManager[0]["Email"].ToString();
                    txtdMFType.Text = ReqManager[0]["MFTypeName"].ToString();
                    txtOfRegDate.Text = ReqManager[0]["RegOfDate"].ToString();
                    txtOfRegNo.Text = ReqManager[0]["RegOfNo"].ToString();
                    txtOfRegPlace.Text = ReqManager[0]["RegOfPlace"].ToString();
                    txtOfStock.Text = ReqManager[0]["Stock"].ToString();
                    txtOfSubject.Text = ReqManager[0]["Subject"].ToString();
                    txtOfType.Text = ReqManager[0]["OtName"].ToString();
                    txtOfValue.Text = ReqManager[0]["VolumeInvest"].ToString();
                    string Tel1 = ReqManager[0]["Tel1"].ToString();
                    if (ReqManager[0]["Tel1"].ToString() != "")
                    {
                        if (ReqManager[0]["Tel1"].ToString().IndexOf("-") > 0)
                        {
                            txtOfTel1_pre.Text = ReqManager[0]["Tel1"].ToString().Substring(0, ReqManager[0]["Tel1"].ToString().IndexOf("-"));
                            txtOfTel1.Text = ReqManager[0]["Tel1"].ToString().Substring(ReqManager[0]["Tel1"].ToString().IndexOf("-") + 1, ReqManager[0]["Tel1"].ToString().Length - ReqManager[0]["Tel1"].ToString().IndexOf("-") - 1);
                        }
                        else
                        {
                            txtOfTel1.Text = ReqManager[0]["Tel1"].ToString();
                        }
                    }

                    string Tel2 = ReqManager[0]["Tel2"].ToString();
                    if (ReqManager[0]["Tel2"].ToString() != "")
                    {
                        if (ReqManager[0]["Tel2"].ToString().IndexOf("-") > 0)
                        {
                            txtOfTel2_pre.Text = ReqManager[0]["Tel2"].ToString().Substring(0, ReqManager[0]["Tel2"].ToString().IndexOf("-"));
                            txtOfTel2.Text = ReqManager[0]["Tel2"].ToString().Substring(ReqManager[0]["Tel2"].ToString().IndexOf("-") + 1, ReqManager[0]["Tel2"].ToString().Length - ReqManager[0]["Tel2"].ToString().IndexOf("-") - 1);
                        }
                        else
                        {
                            txtOfTel2.Text = ReqManager[0]["Tel2"].ToString();
                        }
                    }

                    string Fax = ReqManager[0]["Fax"].ToString();
                    if (ReqManager[0]["Fax"].ToString() != "")
                    {
                        if (ReqManager[0]["Fax"].ToString().IndexOf("-") > 0)
                        {
                            txtOfFax_pre.Text = ReqManager[0]["Fax"].ToString().Substring(0, ReqManager[0]["Fax"].ToString().IndexOf("-"));
                            txtOfFax.Text = ReqManager[0]["Fax"].ToString().Substring(ReqManager[0]["Fax"].ToString().IndexOf("-") + 1, ReqManager[0]["Fax"].ToString().Length - ReqManager[0]["Fax"].ToString().IndexOf("-") - 1);
                        }
                        else
                        {
                            txtOfFax.Text = ReqManager[0]["Fax"].ToString();
                        }
                    }

                    imgOfArm.ClientVisible = true;
                    imgOfSign.ClientVisible = true;

                    if (!Utility.IsDBNullOrNullValue(ReqManager[0]["SignUrl"]))
                    {
                        imgOfSign.ImageUrl = ReqManager[0]["SignUrl"].ToString();
                    }
                    if (!Utility.IsDBNullOrNullValue(ReqManager[0]["ArmUrl"]))
                    {
                        imgOfArm.ImageUrl = ReqManager[0]["ArmUrl"].ToString();
                    }

                    TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
                    AspxGridFlp.DataSource = attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.OfficeRequest, OfReId);
                    AspxGridFlp.DataBind();

                    txtReRequestDesc.Text = ReqManager[0]["RequestDesc"].ToString();               

                    if (!Utility.IsDBNullOrNullValue(ReqManager[0]["ExpireDate"]))
                        txtdExpDate.Text = ReqManager[0]["ExpireDate"].ToString();
                    if (!Utility.IsDBNullOrNullValue(ReqManager[0]["SerialNo"]))
                        txtdSerialNo.Text = ReqManager[0]["SerialNo"].ToString();
                    if (!Utility.IsDBNullOrNullValue(ReqManager[0]["RegDate"]))
                        txtdLastRegDate.Text = ReqManager[0]["RegDate"].ToString();
                    if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MFNo"]))
                        txtdMFNo.Text = ReqManager[0]["MFNo"].ToString();
                    if (!Utility.IsDBNullOrNullValue(ReqManager[0]["IsTemp"]))
                    {
                        //   cmbdIsTemporary.SelectedIndex = int.Parse(ReqManager[0]["IsTemp"].ToString());
                        //cmbdIsTemporary.DataBind();
                        //cmbdIsTemporary.SelectedIndex = cmbdIsTemporary.Items.IndexOfValue(ReqManager[0]["IsTemp"].ToString());
                        if (Convert.ToBoolean(ReqManager[0]["IsTemp"]))
                            cmbdIsTemporary.SelectedIndex = 1;
                        else
                            cmbdIsTemporary.SelectedIndex = 0;
                    }


                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد ";
                }
                ReqManager2.FindByOfficeId(OfId, -1, (int)TSP.DataManager.OfficeRequestType.SaveFileDocument);//صدور پروانه
                if (ReqManager2.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(ReqManager2[0]["RegDate"]))
                        txtdRegDate.Text = ReqManager2[0]["RegDate"].ToString();
                }
                //else
                //{
                //    Response.Redirect("OfficeRequest.aspx");
                //}
            }

            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان مشاهده اطلاعات وجود ندارد ";
            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است ";
        }
    }

    protected void Edit(int OfReId)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.OfficeRequestManager ReqManager = new TSP.DataManager.OfficeRequestManager();
        try
        {
            ReqManager.FindByCode(OfReId);
            if (ReqManager.Count > 0)
            {
                ReqManager[0].BeginEdit();

                ReqManager[0]["RegDate"] = txtdLastRegDate.Text;
                ReqManager[0]["SerialNo"] = txtdSerialNo.Text;
                ReqManager[0]["ExpireDate"] = txtdExpDate.Text;
                if (cmbdIsTemporary.SelectedItem.Value.ToString() == "0")
                    ReqManager[0]["IsTemp"] = 0;
                else
                    ReqManager[0]["IsTemp"] = 1;

                ReqManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                ReqManager[0]["ModifiedDate"] = DateTime.Now;
                ReqManager[0].EndEdit();

                if (ReqManager.Save() > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                    if (ReqManager[0]["Type"].ToString() == "1")//صدور
                        txtdRegDate.Text = txtdLastRegDate.Text;
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام شد.";
                }


            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";

            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }

   
    #endregion
}
