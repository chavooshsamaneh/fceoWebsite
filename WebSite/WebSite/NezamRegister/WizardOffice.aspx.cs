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
using DevExpress.Web;

public partial class NezamRegister_WizardOffice : System.Web.UI.Page
{
    DataTable dtOffice = new DataTable();

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        SetHelpAddress();
        ASPxMenu1.Items.FindByName("Office").Selected = true;

        #region Set Menue Image
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
        #endregion

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {            
            if (Session["Office"] == null)
            {
                #region CreateDataTable
                dtOffice.Columns.Add("OfName");
                dtOffice.Columns.Add("OfNameEn");
                dtOffice.Columns.Add("PrefixCode");
                dtOffice.Columns.Add("OtId");
                dtOffice.Columns.Add("OtName");
                dtOffice.Columns.Add("OatId");
                dtOffice.Columns.Add("OatName");
                dtOffice.Columns.Add("Tel1");
                dtOffice.Columns.Add("Tel2");
                dtOffice.Columns.Add("Fax");
                dtOffice.Columns.Add("MobileNo");
                dtOffice.Columns.Add("Email");
                dtOffice.Columns.Add("Website");
                dtOffice.Columns.Add("Address");
                dtOffice.Columns.Add("Subject");
                dtOffice.Columns.Add("RegDate");
                dtOffice.Columns.Add("RegOfNo");
                dtOffice.Columns.Add("RegPlace");
                dtOffice.Columns.Add("VolumeInvest");
                dtOffice.Columns.Add("Stock");
                dtOffice.Columns.Add("MeNo");
                dtOffice.Columns.Add("FileNo");
                dtOffice.Columns.Add("CreateDate");
                dtOffice.Columns.Add("Description");
                dtOffice.Columns.Add("MrsId");
                dtOffice.Columns.Add("ModifiedDate");
                dtOffice.Columns.Add("MembershipRequstType");

                Session["Office"] = dtOffice;
                #endregion

            }
            else if (((DataTable)Session["Office"]).Rows.Count > 0)
            {
                #region Fill Form
                dtOffice = (DataTable)Session["Office"];

                txtOfAddress.Text = dtOffice.Rows[0]["Address"].ToString();
                txtOfDesc.Text = dtOffice.Rows[0]["Description"].ToString();
                txtOfEmail.Text = dtOffice.Rows[0]["Email"].ToString();
                //txtOfFileNo.Text = dtOffice.Rows[0]["FileNo"].ToString();
                txtOfMobile.Text = dtOffice.Rows[0]["MobileNo"].ToString();
                txtOfName.Text = dtOffice.Rows[0]["OfName"].ToString();
                txtOfNameEn.Text = dtOffice.Rows[0]["OfNameEn"].ToString();
                txtOfRegDate.Text = dtOffice.Rows[0]["RegDate"].ToString();
                txtOfRegNo.Text = dtOffice.Rows[0]["RegOfNo"].ToString();
                txtOfRegPlace.Text = dtOffice.Rows[0]["RegPlace"].ToString();
                txtOfStock.Text = dtOffice.Rows[0]["Stock"].ToString();
                txtOfSubject.Text = dtOffice.Rows[0]["Subject"].ToString();
                txtOfValue.Text = dtOffice.Rows[0]["VolumeInvest"].ToString();
                txtOfWebsite.Text = dtOffice.Rows[0]["Website"].ToString();


                if (Session["fileOfSign"] != null)
                {
                    imgOfSign.ClientVisible = true;
                    imgOfSign.ImageUrl = "~/image/Temp/" + Path.GetFileName(Session["fileOfSign"].ToString());
                    HDFlpSign["name"] = 1;

                }
                if (Session["fileOfArm"] != null)
                {
                    imgOfArm.ClientVisible = true;
                    imgOfArm.ImageUrl = "~/image/Temp/" + Path.GetFileName(Session["fileOfArm"].ToString());
                    HDFlpArm["name"] = 1;

                }

                if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["MembershipRequstType"]))
                {
                    cmbMembershipRequstType.SelectedIndex = cmbMembershipRequstType.Items.IndexOfValue(dtOffice.Rows[0]["MembershipRequstType"].ToString());
                }

                if (!Utility.IsDBNullOrNullValue(dtOffice.Rows[0]["OtId"]))
                {
                    drdOfType.DataBind();
                    drdOfType.SelectedIndex = drdOfType.Items.IndexOfValue(dtOffice.Rows[0]["OtId"].ToString());
                }

                if (dtOffice.Rows[0]["Tel1"].ToString() != "")
                {
                    if (dtOffice.Rows[0]["Tel1"].ToString().IndexOf("-") > 0)
                    {
                        txtOfTel1_pre.Text = dtOffice.Rows[0]["Tel1"].ToString().Substring(0, dtOffice.Rows[0]["Tel1"].ToString().IndexOf("-"));
                        txtOfTel1.Text = dtOffice.Rows[0]["Tel1"].ToString().Substring(dtOffice.Rows[0]["Tel1"].ToString().IndexOf("-") + 1, dtOffice.Rows[0]["Tel1"].ToString().Length - dtOffice.Rows[0]["Tel1"].ToString().IndexOf("-") - 1);
                    }
                    else
                    {
                        txtOfTel1.Text = dtOffice.Rows[0]["Tel1"].ToString();
                    }
                }
                if (dtOffice.Rows[0]["Tel2"].ToString() != "")
                {
                    if (dtOffice.Rows[0]["Tel2"].ToString().IndexOf("-") > 0)
                    {
                        txtOfTel2_pre.Text = dtOffice.Rows[0]["Tel2"].ToString().Substring(0, dtOffice.Rows[0]["Tel2"].ToString().IndexOf("-"));
                        txtOfTel2.Text = dtOffice.Rows[0]["Tel2"].ToString().Substring(dtOffice.Rows[0]["Tel2"].ToString().IndexOf("-") + 1, dtOffice.Rows[0]["Tel2"].ToString().Length - dtOffice.Rows[0]["Tel2"].ToString().IndexOf("-") - 1);
                    }
                    else
                    {
                        txtOfTel2.Text = dtOffice.Rows[0]["Tel2"].ToString();
                    }
                }
                if (dtOffice.Rows[0]["Fax"].ToString() != "")
                {
                    if (dtOffice.Rows[0]["Fax"].ToString().IndexOf("-") > 0)
                    {
                        txtOfFax_pre.Text = dtOffice.Rows[0]["Fax"].ToString().Substring(0, dtOffice.Rows[0]["Fax"].ToString().IndexOf("-"));
                        txtOfFax.Text = dtOffice.Rows[0]["Fax"].ToString().Substring(dtOffice.Rows[0]["Fax"].ToString().IndexOf("-") + 1, dtOffice.Rows[0]["Fax"].ToString().Length - dtOffice.Rows[0]["Fax"].ToString().IndexOf("-") - 1);
                    }
                    else
                    {
                        txtOfFax.Text = dtOffice.Rows[0]["Fax"].ToString();
                    }
                }
                #endregion
            }
        }
    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
        OfficeManager.FindByRegNoAndRegDate(txtOfRegNo.Text.Trim(), txtOfRegDate.Text.Trim());
        if (OfficeManager.Count > 0)
        {
            string msg = "اطلاعات وارد شده با نام " + OfficeManager[0]["OfName"].ToString() + " و کد عضویت " + OfficeManager[0]["OfId"].ToString() + " در سیستم موجود می باشد";
            this.DivReport.Visible = true;
            this.LabelWarning.Text = msg;
            return;
        }

        if (Session["fileOfSign"] == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "تصویر مهر شرکت را انتخاب نمایید";
            return;
        }

        if (Session["fileOfArm"] == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "تصویر آرم شرکت را انتخاب نمایید";
            return;
        }

        string Tel1 = "", fax = "", Tel2 = "";

        if (txtOfTel1_pre.Text != "" && txtOfTel1.Text != "")
            Tel1 = txtOfTel1_pre.Text + "-" + txtOfTel1.Text;
        else if (txtOfTel1.Text != "")
            Tel1 = txtOfTel1.Text;
        if (txtOfTel2_pre.Text != "" && txtOfTel2.Text != "")
            Tel2 = txtOfTel2_pre.Text + "-" + txtOfTel2.Text;
        else if (txtOfTel2.Text != "")
            Tel2 = txtOfTel2.Text;
        if (txtOfFax_pre.Text != "" && txtOfFax.Text != "")
            fax = txtOfFax_pre.Text + "-" + txtOfFax.Text;
        else if (txtOfFax.Text != "")
            fax = txtOfFax.Text;

        string PerDate = Utility.GetDateOfToday();

        if (Session["Office"] == null || ((DataTable)Session["Office"]).Rows.Count == 0)
        {
            ((DataTable)Session["Office"]).Rows.Add(new object[]
                {txtOfName.Text,txtOfNameEn.Text,DBNull.Value,drdOfType.Value,drdOfType.SelectedItem.Text,DBNull.Value,null,
                    Tel1,Tel2,fax,
                txtOfMobile.Text,txtOfEmail.Text,txtOfWebsite.Text,txtOfAddress.Text,txtOfSubject.Text,txtOfRegDate.Text,txtOfRegNo.Text,
                txtOfRegPlace.Text,txtOfValue.Text,txtOfStock.Text,1,                  
                DBNull.Value,PerDate,txtOfDesc.Text,2,DateTime.Now,cmbMembershipRequstType.Value });

        }
        else
        {
            dtOffice = (DataTable)Session["Office"];
            dtOffice.Rows[0]["Address"] = txtOfAddress.Text;
            dtOffice.Rows[0]["Description"] = txtOfDesc.Text;
            dtOffice.Rows[0]["Email"] = txtOfEmail.Text;
            //dtOffice.Rows[0]["FileNo"] = txtOfFileNo.Text;
            dtOffice.Rows[0]["MobileNo"] = txtOfMobile.Text;
            dtOffice.Rows[0]["OfName"] = txtOfName.Text;
            dtOffice.Rows[0]["OfNameEn"] = txtOfNameEn.Text;
            dtOffice.Rows[0]["RegDate"] = txtOfRegDate.Text;
            dtOffice.Rows[0]["RegOfNo"] = txtOfRegNo.Text;
            dtOffice.Rows[0]["RegPlace"] = txtOfRegPlace.Text;
            dtOffice.Rows[0]["Stock"] = txtOfStock.Text;
            dtOffice.Rows[0]["Subject"] = txtOfSubject.Text;
            dtOffice.Rows[0]["VolumeInvest"] = txtOfValue.Text;
            dtOffice.Rows[0]["Website"] = txtOfWebsite.Text;
            dtOffice.Rows[0]["Tel1"] = Tel1;
            dtOffice.Rows[0]["Tel2"] = Tel2;
            dtOffice.Rows[0]["Fax"] = fax;
            dtOffice.Rows[0]["OtId"] = drdOfType.Value;
            dtOffice.Rows[0]["MembershipRequstType"] = cmbMembershipRequstType.Value != null ? cmbMembershipRequstType.SelectedItem.Value : DBNull.Value;
            //  dtOffice.Rows[0]["ActivityType"] = cmbActivityType.Value != null ? cmbActivityType.SelectedItem.Value : DBNull.Value;
            //dtOffice.Rows[0]["OatId"] = aspxcmbAttype.Value;     

        }

        Response.Redirect("WizardOfficeAgent.aspx");

    }

    protected void flpOfArm_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {

        try
        {
            e.CallbackData = SaveImageArm(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }    

    protected void flpOfSign_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageSign(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    #endregion

    #region Methods
    protected string SaveImageSign(UploadedFile uploadedFile)
    {
        string ret = "";
        string ret2 = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
                ret2 = Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/Office/Sign/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            string tempFileName2 = MapPath("~/Image/Temp/") + ret2;

            uploadedFile.SaveAs(tempFileName, true);
            Utility.FixedSize(tempFileName, tempFileName2, Utility.GetOfficeSign_HorRes(), Utility.GetOfficeSign_VerRes());
            Session["FileOfSign"] = tempFileName2;
            // Session["FileOfSign"] = ret;

        }
        return ret;
    }

    protected string SaveImageArm(UploadedFile uploadedFile)
    {
        string ret = "";
        string ret2 = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
                ret2 = Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/Image/Office/Arm/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            string tempFileName2 = MapPath("~/Image/Temp/") + ret2;

            uploadedFile.SaveAs(tempFileName, true);
            Utility.FixedSize(tempFileName, tempFileName2, Utility.GetOfficeSign_HorRes(), Utility.GetOfficeSign_VerRes());
            Session["FileOfArm"] = tempFileName2;
            // Session["FileOfArm"] = ret;

        }
        return ret;
    }

    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.WizardOffice).ToString());
    }
    #endregion
}
