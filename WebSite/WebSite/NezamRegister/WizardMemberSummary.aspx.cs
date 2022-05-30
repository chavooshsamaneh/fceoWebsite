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

public partial class NezamRegister_WizardMemberSummary : System.Web.UI.Page
{
    DataTable dtMember = new DataTable();
    DataTable dtActivity2 = new DataTable();
    DataTable dtMadrak = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        ASPxMenu1.Items.FindByName("Summary").Selected = true;

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

        if (Session["Member"] == null)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "مدت زمان اعتبار صفحه به پایان رسیده است.مجدداً اقدام نمایید";
            this.btnNext.Enabled = false;
            return;
        }

        if (!IsPostBack)
        {
            if (Session["Member"] != null)
            {
                dtMember = (DataTable)Session["Member"];

                if (dtMember.Rows.Count != 0)
                {
                    //string commission = "";
                    //for (int i = 1; i < chbComId.Items.Count; i++)
                    //{
                    //    if (chbComId.Items[i].Selected)
                    //        commission += chbComId.Items[i].Text + ",";
                    //    // commission = commission + " , " + chbComId.Items[i].Text;
                    //}
                    //if (string.IsNullOrEmpty(commission))
                    //    commission = "هیچکدام";
                    //else
                    //    commission = commission.Remove(commission.Length - 1);

                    //AScommission.Text = commission;

                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["BankAccNo"]))
                        ASbankaccount.Text = dtMember.Rows[0]["BankAccNo"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["BirthDate"]))
                        ASbirthdate.Text = dtMember.Rows[0]["BirthDate"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["BirthPlace"]))
                        ASbirthplace.Text = dtMember.Rows[0]["BirthPlace"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["Description"]))
                        ASdesc.Text = dtMember.Rows[0]["Description"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["Email"]))
                        ASemail.Text = dtMember.Rows[0]["Email"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["FatherName"]))
                        ASPfathername.Text = dtMember.Rows[0]["FatherName"].ToString();
                    //if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["FileNo"]))
                    //    ASfilenno.Text = dtMember.Rows[0]["FileNo"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["FirstName"]))
                        ASfirstname.Text = dtMember.Rows[0]["FirstName"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["FirstNameEn"]))
                        ASfirstnameen.Text = dtMember.Rows[0]["FirstNameEn"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["HomeAdr"]))
                        AShomeaddr.Text = dtMember.Rows[0]["HomeAdr"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["HomePO"]))
                        AShomepo.Text = dtMember.Rows[0]["HomePO"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["IdNo"]))
                        ASIdno.Text = dtMember.Rows[0]["IdNo"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["IssuePlace"]))
                        ASissueplace.Text = dtMember.Rows[0]["IssuePlace"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["LastName"]))
                        ASlastname.Text = dtMember.Rows[0]["LastName"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["LastNameEn"]))
                        ASlastnameen.Text = dtMember.Rows[0]["LastNameEn"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["MobileNo"]))
                        ASmobile.Text = dtMember.Rows[0]["MobileNo"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["Nationality"]))
                        ASnationality.Text = dtMember.Rows[0]["Nationality"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["SSN"]))
                        ASssn.Text = dtMember.Rows[0]["SSN"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["Website"]))
                        ASwebsite.Text = dtMember.Rows[0]["Website"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["WorkAdr"]))
                        ASworkaddr.Text = dtMember.Rows[0]["WorkAdr"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["WorkPO"]))
                        ASworkpo.Text = dtMember.Rows[0]["WorkPO"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["HomeTel"]))
                        AShometel.Text = dtMember.Rows[0]["HomeTel"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["WorkTel"]))
                        ASworktel.Text = dtMember.Rows[0]["WorkTel"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["FaxNo"]))
                        ASfax.Text = dtMember.Rows[0]["FaxNo"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["NezamKardanConfirmURL"]))
                        ImgKardan.ImageUrl = dtMember.Rows[0]["NezamKardanConfirmURL"].ToString();
                    else
                        Image.ImageUrl = "~/Images/person.gif";


                    if (Session["FileOfMember"] != null)
                        Image.ImageUrl = "~/image/Temp/" + Path.GetFileName(Session["FileOfMember"].ToString());
                    else
                        Image.ImageUrl = "~/Images/person.gif";

                    if (string.IsNullOrEmpty(dtMember.Rows[0]["TPrId"].ToString()))
                    {
                        Tr34.Visible = false;
                        Tr36.Visible = false;
                        Tr38.Visible = false;
                    }
                    else
                    {
                        Tr34.Visible = true;
                        Tr36.Visible = true;
                        Tr38.Visible = true;

                        if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["TransferDate"]))
                            ASTDate.Text = dtMember.Rows[0]["TransferDate"].ToString();
                        if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["TFileNo"]))
                            ASTFileNo.Text = dtMember.Rows[0]["TFileNo"].ToString();
                        if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["TMeNo"]))
                            ASTMeNo.Text = dtMember.Rows[0]["TMeNo"].ToString();
                        if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["TPrName"]))
                            ASTPr.Text = dtMember.Rows[0]["TPrName"].ToString();

                        if (Session["LetterImg"] != null)
                            TImage.ImageUrl = "~/image/Temp/" + Session["LetterImg"].ToString();
                    }
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["MarName"]))
                        ASmar.Text = dtMember.Rows[0]["MarName"].ToString();
                    //if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["RelName"]))
                    //    ASRel.Text = dtMember.Rows[0]["RelName"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["SexName"]))
                        ASsex.Text = dtMember.Rows[0]["SexName"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["CitName"]))
                        AScity.Text = dtMember.Rows[0]["CitName"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["AgentName"]))
                        ASAgent.Text = dtMember.Rows[0]["AgentName"].ToString();

                    if (dtMember.Rows[0]["SexName"].ToString() == "مرد")
                    {
                        lblSo.Visible = true;
                        ASsol.Visible = true;
                        if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["SoName"]))
                            ASsol.Text = dtMember.Rows[0]["SoName"].ToString();

                    }
                    //if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["TiName"]))
                    //    AStitle.Text = dtMember.Rows[0]["TiName"].ToString();                  

                }
            }

            if (Session["TblActivity2"] != null && ((DataTable)Session["TblActivity2"]).Rows.Count > 0)
            {
                dtActivity2 = (DataTable)Session["TblActivity2"];
                if (!Utility.IsDBNullOrNullValue(dtActivity2.Rows[0]["AtName"]))
                    ASattype.Text = dtActivity2.Rows[0]["AtName"].ToString();
                if (!Utility.IsDBNullOrNullValue(dtActivity2.Rows[0]["ComName"]))
                    AScommission.Text = dtActivity2.Rows[0]["ComName"].ToString();
            }


            //int IndexLiId;
            //FindMaxLiId(out IndexLiId);
            //AStitle.Text = dtMadrak.Rows[IndexLiId]["TiName"].ToString();

            GGrdvActivity.DataSource = (DataTable)Session["TblActivity"];
            GGrdvActivity.DataBind();
            GrdvJob.DataSource = (DataTable)Session["TblJob"];
            GrdvJob.DataBind();
            GrdvMadrak.DataSource = (DataTable)Session["TblOfMadrak"];
            GrdvMadrak.DataBind();
            GrdvLanguage.DataSource = (DataTable)Session["TblLanguage"];
            GrdvLanguage.DataBind();
            //GrdvResearch.DataSource = (DataTable)Session["TblResearch"];
            //GrdvResearch.DataBind();
           
           
        }

    }

    protected void btnPre_Click(object sender, EventArgs e)
    {
        //Response.Redirect("WizardMemberResearch.aspx");
        Response.Redirect("WizardMemberLanguage.aspx");


    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (!ChbCheck.Checked)
            lblCheck.Visible = true;
        else
        {
            Session["MemberSummary"] = true;
            Response.Redirect("WizardMemberFinish.aspx?PgMode="+Utility.EncryptQS("EPayment"));
        }
    }

    protected void GrdvJob_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartCorporateDate" || e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr"; 
    }

    protected void GrdvMadrak_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartDate" || e.DataColumn.FieldName == "EndDate")
            e.Cell.Style["direction"] = "ltr";
    }

    #region Methods
    int FindMaxLiId(out int Index)
    {
        dtMadrak = (DataTable)Session["TblOfMadrak"];
        Index = -1;
        int Max = 0;
        for (int i = 0; i < dtMadrak.Rows.Count; i++)
        {
            //DataRowView dr = (DataRowView)CustomAspxDevGridView1.GetRow(i);
            if (Max < int.Parse(dtMadrak.Rows[i]["LiId"].ToString()))
            {
                Max = int.Parse(dtMadrak.Rows[i]["LiId"].ToString());
                Index = i;
            }
        }
        return Max;
    }

    int FindMaxLiId()
    {
        int Index;
        return FindMaxLiId(out Index);
    }
    #endregion
}
