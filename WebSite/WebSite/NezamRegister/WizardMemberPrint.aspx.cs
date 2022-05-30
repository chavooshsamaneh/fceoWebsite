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

public partial class NezamRegister_WizardMemberPrint : System.Web.UI.Page
{
    DataTable dtMember = new DataTable();
    DataTable dtActivity2 = new DataTable();
    DataTable dtMadrak = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        int UserId = -1;
        string Pass = "";
        String Code = "";

        try
        {

            UserId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["UId"].ToString())));
            Pass = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["P"].ToString()));
            Code = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["C"].ToString()));
        }
        catch (Exception)
        {
            //this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        }


        try
        {
            if (UserId == -1)
                return;

            TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
            LoginManager.FindByCode(UserId);
            if (LoginManager.Count == 0)
                return;

            TSP.DataManager.TempMemberManager MemberManager = new TSP.DataManager.TempMemberManager();
            MemberManager.FindByCode(Convert.ToInt32(LoginManager[0]["MeId"]));
            if (MemberManager.Count > 0)
            {
                int MeId = Convert.ToInt32(LoginManager[0]["MeId"]);
                dtMember = MemberManager.DataTable;

                if (dtMember.Rows.Count != 0)
                {
                    //if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["BankAccNo"]))
                    //    ASbankaccount.Text = dtMember.Rows[0]["BankAccNo"].ToString();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["BirhtDate"]))
                        ASbirthdate.Text = dtMember.Rows[0]["BirhtDate"].ToString();
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

                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["ImageUrl"]))
                        Image.ImageUrl = "~/image/Members/Person/Request/" + System.IO.Path.GetFileName(dtMember.Rows[0]["ImageUrl"].ToString());
                    else
                        Image.ImageUrl = "~/Images/person.gif";

                    TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
                    MemberRequestManager.FindByMemberId(MeId, 0, 1);
                    if (MemberRequestManager.Count > 0)
                    {
                        TSP.DataManager.TransferMemberManager TransferMemberManager = new TSP.DataManager.TransferMemberManager();
                        TransferMemberManager.FindByMemberId(Convert.ToInt32(MemberRequestManager[0]["MReId"]), 1);
                        if (TransferMemberManager.Count == 0)
                        {
                            for (int i = 23; i <= 25; i++)
                                TABLE2.Rows[i].Visible = false;
                        }
                        else
                        {
                            for (int i = 23; i <= 25; i++)
                                TABLE2.Rows[i].Visible = true;

                            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["TransferDate"]))
                                ASTDate.Text = TransferMemberManager[0]["TransferDate"].ToString();
                            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["FileNo"]))
                                ASTFileNo.Text = TransferMemberManager[0]["FileNo"].ToString();
                            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["MeNo"]))
                                ASTMeNo.Text = TransferMemberManager[0]["MeNo"].ToString();
                            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["PrName"]))
                                ASTPr.Text = TransferMemberManager[0]["PrName"].ToString();

                            if (!Utility.IsDBNullOrNullValue(TransferMemberManager[0]["ImageUrl"]))
                                TImage.ImageUrl = TransferMemberManager[0]["ImageUrl"].ToString();
                        }
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

                    //if (Session["TblActivity2"] != null)
                    //{
                    //    dtActivity2 = (DataTable)Session["TblActivity2"];
                    //    if (!Utility.IsDBNullOrNullValue(dtActivity2.Rows[0]["AtName"]))
                    //        ASattype.Text = dtActivity2.Rows[0]["AtName"].ToString();
                    //    if (!Utility.IsDBNullOrNullValue(dtActivity2.Rows[0]["ComName"]))
                    //        AScommission.Text = dtActivity2.Rows[0]["ComName"].ToString();
                    //}

                    String Commission = "";
                    TSP.DataManager.CommissionManager CommissionManager = new TSP.DataManager.CommissionManager();
                    CommissionManager.Fill();
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["ComId"]) && CommissionManager.Count > 0)
                    {
                        int comId = int.Parse(dtMember.Rows[0]["ComId"].ToString());
                        //if (comId == 0)
                        //{
                        //    chbComId.Items[0].Selected = true;
                        //}
                        //else
                        //{
                        for (int i = 0; i < CommissionManager.Count; i++)
                        {
                            int y = int.Parse(CommissionManager[i]["ComId"].ToString());

                            if ((y &= comId) == int.Parse(CommissionManager[i]["ComId"].ToString()))
                            {
                                Commission += (String.IsNullOrEmpty(Commission)) ? "" : ", ";
                                Commission += CommissionManager[i]["ComName"].ToString();
                            }
                        }
                        if (String.IsNullOrEmpty(Commission) == false)
                            AScommission.Text = Commission;
                    }

                    if (String.IsNullOrEmpty(dtMember.Rows[0]["AtId"].ToString().Trim()) == false)
                    {
                        TSP.DataManager.ActivityTypeManager ActivityTypeManager = new TSP.DataManager.ActivityTypeManager();
                        ActivityTypeManager.Fill();
                        ActivityTypeManager.CurrentFilter = "AtId=" + dtMember.Rows[0]["AtId"].ToString();
                        if (ActivityTypeManager.Count > 0)
                            ASattype.Text = ActivityTypeManager[0]["AtName"].ToString();
                    }

                    ASPassword.Text = Pass;
                    ASUserName.Text = LoginManager[0]["UserName"].ToString();
                    lblFollowCode.Text = Code;
                    //TSP.DataManager.MemberRequestManager MemberRequestManager = new TSP.DataManager.MemberRequestManager();
                    //MemberRequestManager.FindByMemberId(MeId, -1, 1);
                    //if (MemberRequestManager.Count == 1)
                    //{
                    //    lblFollowCode.Text = MemberRequestManager[0]["FollowCode"].ToString();
                    //}
                }

                //int IndexLiId;
                //FindMaxLiId(out IndexLiId);
                //AStitle.Text = dtMadrak.Rows[IndexLiId]["TiName"].ToString();

                TSP.DataManager.TempMemberActivitySubjectManager MemberActivitySubjectManager = new TSP.DataManager.TempMemberActivitySubjectManager();
                MemberActivitySubjectManager.FindByTMeId(MeId);
                GGrdvActivity.DataSource = MemberActivitySubjectManager.DataTable;
                GGrdvActivity.DataBind();

                TSP.DataManager.TempMemberJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.TempMemberJobHistoryManager();
                ProjectJobHistoryManager.FindByTMeId(MeId);
                GrdvJob.DataSource = ProjectJobHistoryManager.DataTable;
                GrdvJob.DataBind();

                TSP.DataManager.TempMemberLicenceManager MemberLicenceManager = new TSP.DataManager.TempMemberLicenceManager();
                MemberLicenceManager.FindByTMeId(MeId);
                GrdvMadrak.DataSource = MemberLicenceManager.DataTable;
                GrdvMadrak.DataBind();

                TSP.DataManager.TempMemberLanguageManager MemberLanguageManager = new TSP.DataManager.TempMemberLanguageManager();
                MemberLanguageManager.FindByTMeId(MeId);
                GrdvLanguage.DataSource = MemberLanguageManager.DataTable;
                GrdvLanguage.DataBind();
                //GrdvResearch.DataSource = (DataTable)Session["TblResearch"];
                //GrdvResearch.DataBind();

            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
    }
    //int FindMaxLiId(out int Index)
    //{
    //    dtMadrak = (DataTable)Session["TblOfMadrak"];
    //    Index = -1;
    //    int Max = 0;
    //    for (int i = 0; i < dtMadrak.Rows.Count; i++)
    //    {
    //        //DataRowView dr = (DataRowView)CustomAspxDevGridView1.GetRow(i);
    //        if (Max < int.Parse(dtMadrak.Rows[i]["LiId"].ToString()))
    //        {
    //            Max = int.Parse(dtMadrak.Rows[i]["LiId"].ToString());
    //            Index = i;
    //        }
    //    }
    //    return Max;
    //}
    //int FindMaxLiId()
    //{
    //    int Index;
    //    return FindMaxLiId(out Index);
    //}
    protected void GrdvJob_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartCorporateDate" || e.DataColumn.FieldName == "EndCorporateDate" || e.DataColumn.FieldName == "StartOriginalDate")
            e.Cell.Style["direction"] = "ltr";
    }
    protected void GrdvMadrak_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "StartDate" || e.DataColumn.FieldName == "EndDate")
            e.Cell.Style["direction"] = "ltr";
    }
}
