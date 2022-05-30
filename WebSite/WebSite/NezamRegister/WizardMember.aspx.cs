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

public partial class NezamRegister_WizardMember : System.Web.UI.Page
{
    DataTable dtMember = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {

        lblImg.Text = "<b>* تصویر پرسنلی انتخابی بایستی :</b>";
        lblImg.Text += "<ul><li>رنگی و پشت سفید باشد(برای خانم ها عکس با مقنعه و حجاب کامل و برای آقایان با لباس رسمی، بدون کراوات و کلاه باشد)</li>";
        lblImg.Text += "<li>حجم فایل تصویر بایستی حداکثر " + (flpImage.MaxSizeForUploadFile / 1000) + "KB باشد</li>";
        lblImg.Text += "<li>در اندازه " + Utility.VerRes + "*" + Utility.HorRes + " و " + Utility.dpi + " dpi باشد" + "</li>";
        lblImg.Text += "<li>طی یکسال گذشته گرفته شده باشد</li></ul>";

        HDFlpMember["Date"] = Utility.GetDateOfToday().Substring(0, 4);

        ASPxMenu1.Items.FindByName("Member").Selected = true;

        #region MarkTab
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

        #endregion

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        #region FillImage
        if (Session["FileOfMember"] != null)
        {
            imgMember.ClientVisible = true;
            imgMember.ImageUrl = "~/image/Temp/" + Path.GetFileName(Session["FileOfMember"].ToString());
            HDFlpMember["name"] = 1;
        }
        if (Session["FileOfSign"] != null)
        {
            ImgSign.ClientVisible = true;
            ImgSign.ImageUrl = "~/image/Temp/" + Path.GetFileName(Session["FileOfSign"].ToString());
            HDFlpSign["name"] = 1;
        }

        if (Session["FileOfIdNo"] != null)
        {
            HpIdNo.ClientVisible = true;
            HpIdNo.NavigateUrl = "~/image/Temp/" + Path.GetFileName(Session["FileOfIdNo"].ToString());
            HDFlpIdNo["name"] = 1;
        }

        if (Session["FileOfIdNoP2"] != null)
        {
            HIdNoP2.ClientVisible = true;
            HIdNoP2.NavigateUrl = "~/image/Temp/" + Path.GetFileName(Session["FileOfIdNoP2"].ToString());
            HDFlpIdNo["IdNoP2"] = 1;
        }

        if (Session["FileOfIdNoPDes"] != null)
        {
            HIdNoPDes.ClientVisible = true;
            HIdNoPDes.NavigateUrl = "~/image/Temp/" + Path.GetFileName(Session["FileOfIdNoPDes"].ToString());
            HDFlpIdNo["IdNoPDes"] = 1;
        }

        if (Session["FileOfSSN"] != null)
        {
            HpSSN.ClientVisible = true;
            HpSSN.NavigateUrl = "~/image/Temp/" + Path.GetFileName(Session["FileOfSSN"].ToString());
            HDFlpSSN["SSNFront"] = 1;
        }

        if (Session["FileOfSSNBack"] != null)
        {
            HpSSNBack.ClientVisible = true;
            HpSSNBack.NavigateUrl = "~/image/Temp/" + Path.GetFileName(Session["FileOfSSNBack"].ToString());
            HDFlpSSN["SSNBack"] = 1;
        }

        if (Session["FileOfSol"] != null)
        {
            HpSoldier.ClientVisible = true;
            HpSoldier.NavigateUrl = "~/image/Temp/" + Path.GetFileName(Session["FileOfSol"].ToString());
            HDFlpSold["name"] = 1;
        }

        if (Session["FileOfSolBack"] != null)
        {
            HpSoldierBack.ClientVisible = true;
            HpSoldierBack.NavigateUrl = "~/image/Temp/" + Path.GetFileName(Session["FileOfSolBack"].ToString());
            HDFlpSold["SolBack"] = 1;
        }

        if (Session["LetterImg"] != null)
        {
            Timg.ClientVisible = true;
            Timg.ImageUrl = Session["LetterImg"].ToString();
            HDFlpLetter["name"] = 1;

        }
        if (Session["FileOfResident"] != null)
        {
            HypLinkResident.ClientVisible = true;
            HypLinkResident.NavigateUrl = "~/image/Temp/" + Path.GetFileName(Session["FileOfResident"].ToString());
            HDFlpResident["name"] = 1;
        }
        if (Session["FileOfKardani"] != null)
        {
            HpKardani.ClientVisible = true;
            HpKardani.NavigateUrl = "~/image/Members/NezamKardani/Request/" + Path.GetFileName(Session["FileOfKardani"].ToString());
            HDFlpResident["Kardani"] = 1;
            ChkBKardani.Checked = true;
            panelKardani.ClientVisible = true;
        }
        #endregion

        if (Convert.ToInt32(drdSexId.Value) == (int)TSP.DataManager.SexManager.Sex.Male)
        {
            lblSol.ClientVisible = true;
            drdSoId.ClientVisible = true;
        }
        else
        {
            lblSol.ClientVisible = false;
            drdSoId.ClientVisible = false;
            MilitaryCommitment.ClientVisible = false;
        }
        if (drdSoId.SelectedIndex == 3 || drdSoId.SelectedIndex == 4 || drdSoId.SelectedIndex == 5)
        {

            MilitaryCommitment.ClientVisible = true;
        }
        else
        {
            MilitaryCommitment.ClientVisible = false;
            MilitaryCommitment.Checked = false;
        }


        if (!IsPostBack)
        {
            SetHelpAddress();

            OdbProvince.FilterParameters[0].DefaultValue = Utility.GetCurrentProvinceNezamCode().ToString();

            if (Session["Member"] == null)
            {
                CreateDataMeDataTable();
            }
            else if (((DataTable)Session["Member"]).Rows.Count > 0)
            {
                dtMember = (DataTable)Session["Member"];
                txtBankAccNo.Text = dtMember.Rows[0]["BankAccNo"].ToString();
                txtBirhtPlace.Text = dtMember.Rows[0]["BirthPlace"].ToString();
                txtBirthDate.Text = dtMember.Rows[0]["BirthDate"].ToString();
                txtDesc.Text = dtMember.Rows[0]["Description"].ToString();
                txtEmail.Text = dtMember.Rows[0]["Email"].ToString();
                txtFatherName.Text = dtMember.Rows[0]["FatherName"].ToString();
                // txtFileNo.Text = ""; dtMember.Rows[0]["FileNo"].ToString();
                txtFirstName.Text = dtMember.Rows[0]["FirstName"].ToString();
                txtFirstNameEn.Text = dtMember.Rows[0]["FirstNameEn"].ToString();
                txtHomeAdr.Text = dtMember.Rows[0]["HomeAdr"].ToString();
                txtHomePO.Text = dtMember.Rows[0]["HomePO"].ToString();
                txtIdNo.Text = dtMember.Rows[0]["IdNo"].ToString();
                txtIssuePlace.Text = dtMember.Rows[0]["IssuePlace"].ToString();
                txtLastName.Text = dtMember.Rows[0]["LastName"].ToString();
                txtLastNameEn.Text = dtMember.Rows[0]["LastNameEn"].ToString();
                txtMobileNo.Text = dtMember.Rows[0]["MobileNo"].ToString();
                txtNationality.Text = dtMember.Rows[0]["Nationality"].ToString();
                txtSSN.Text = dtMember.Rows[0]["SSN"].ToString();
                txtWebsite.Text = dtMember.Rows[0]["Website"].ToString();
                txtWorkAdr.Text = dtMember.Rows[0]["WorkAdr"].ToString();
                txtWorkPO.Text = dtMember.Rows[0]["WorkPO"].ToString();


                if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["TPrId"]))
                {
                    ChEnteghali.Checked = true;
                    //Page.ClientScript.RegisterStartupScript(GetType(), "key", "<script>document.getElementById('tblT').style.display='block';</script>");
                    panelTransfer.ClientVisible = true;

                    ComboTPr.DataBind();
                    ComboTPr.SelectedIndex = ComboTPr.Items.IndexOfValue(dtMember.Rows[0]["TPrId"].ToString());

                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["TransferDate"]))
                    {
                        txtTDate.Text = dtMember.Rows[0]["TransferDate"].ToString();
                    }
                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["TFileNo"]))
                    {
                        txtTFileNo.ClientVisible = true;
                        lblTFileNo.Visible = true;
                        ChbTCheckFileNo.Checked = true;
                        txtTFileNo.Text = dtMember.Rows[0]["TFileNo"].ToString();
                    }

                    if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["TMeNo"]))
                        txtTMeNo.Text = dtMember.Rows[0]["TMeNo"].ToString();


                }

                if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["AgentId"]))
                {
                    drdAgent.DataBind();
                    drdAgent.SelectedIndex = drdAgent.Items.IndexOfValue(dtMember.Rows[0]["AgentId"].ToString());
                }

                if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["MarId"]))
                {
                    drdMarId.DataBind();
                    drdMarId.SelectedIndex = drdMarId.Items.IndexOfValue(dtMember.Rows[0]["MarId"].ToString());
                }
                if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["CitId"]))
                {
                    drdCitId.DataBind();
                    drdCitId.SelectedIndex = drdCitId.Items.IndexOfValue(dtMember.Rows[0]["CitId"].ToString());
                }
                if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["SexId"]))
                {
                    drdSexId.DataBind();
                    drdSexId.SelectedIndex = drdSexId.Items.IndexOfValue(dtMember.Rows[0]["SexId"].ToString());
                    if (Convert.ToInt32(dtMember.Rows[0]["SexId"].ToString()) == (int)TSP.DataManager.SexManager.Sex.Male)
                    {
                        lblSol.ClientVisible = true;
                        drdSoId.ClientVisible = true;
                        lblSolFile.ClientVisible = true;
                        lblWarnSolImage.ClientVisible = true;
                        flpSoldier.ClientVisible = true;
                        drdSoId.ValidationSettings.RequiredField.IsRequired = true;
                        lblSolFileBack.ClientVisible = true;
                        flpSoldierBack.ClientVisible = true;

                    }
                    else
                    {
                        lblSol.ClientVisible = false;
                        drdSoId.ClientVisible = false;
                        lblSolFile.ClientVisible = false;
                        lblWarnSolImage.ClientVisible = false;
                        flpSoldier.ClientVisible = false;
                        drdSoId.ValidationSettings.RequiredField.IsRequired = false;
                        lblSolFileBack.ClientVisible = false;
                        flpSoldierBack.ClientVisible = false;
                        MilitaryCommitment.ClientVisible = false;
                    }
                }
                if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["SoId"]))
                {
                    lblSol.ClientVisible = true;
                    drdSoId.ClientVisible = true;
                    drdSoId.DataBind();
                    drdSoId.SelectedIndex = drdSoId.Items.IndexOfValue(dtMember.Rows[0]["SoId"].ToString());
                    drdSoId.ValidationSettings.RequiredField.IsRequired = true;
                    if (drdSoId.SelectedIndex == 3 || drdSoId.SelectedIndex == 4 || drdSoId.SelectedIndex == 5)
                    {

                        MilitaryCommitment.ClientVisible = true;
                        if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["MilitaryCommitment"]))
                            MilitaryCommitment.Checked = Convert.ToBoolean(dtMember.Rows[0]["MilitaryCommitment"]);

                        MilitaryCommitment.ValidationSettings.RequiredField.IsRequired = true;
                    }
                    else
                    {
                        MilitaryCommitment.ClientVisible = false;
                        MilitaryCommitment.Checked = false;
                    }
                }


                if (dtMember.Rows[0]["HomeTel"].ToString() != "")
                {
                    if (dtMember.Rows[0]["HomeTel"].ToString().IndexOf("-") > 0)
                    {
                        txtHometel_cityCode.Text = dtMember.Rows[0]["HomeTel"].ToString().Substring(0, dtMember.Rows[0]["HomeTel"].ToString().IndexOf("-"));
                        txtHometel.Text = dtMember.Rows[0]["HomeTel"].ToString().Substring(dtMember.Rows[0]["HomeTel"].ToString().IndexOf("-") + 1, dtMember.Rows[0]["HomeTel"].ToString().Length - dtMember.Rows[0]["HomeTel"].ToString().IndexOf("-") - 1);
                    }
                    else
                    {
                        txtHometel.Text = dtMember.Rows[0]["HomeTel"].ToString();
                    }
                }
                if (dtMember.Rows[0]["FaxNo"].ToString() != "")
                {
                    if (dtMember.Rows[0]["FaxNo"].ToString().IndexOf("-") > 0)
                    {
                        txtFaxNo_cityCode.Text = dtMember.Rows[0]["FaxNo"].ToString().Substring(0, dtMember.Rows[0]["FaxNo"].ToString().IndexOf("-"));
                        txtFaxNo.Text = dtMember.Rows[0]["FaxNo"].ToString().Substring(dtMember.Rows[0]["FaxNo"].ToString().IndexOf("-") + 1, dtMember.Rows[0]["FaxNo"].ToString().Length - dtMember.Rows[0]["FaxNo"].ToString().IndexOf("-") - 1);
                    }
                    else
                    {
                        txtFaxNo.Text = dtMember.Rows[0]["FaxNo"].ToString();
                    }
                }
                if (dtMember.Rows[0]["WorkTel"].ToString() != "")
                {
                    if (dtMember.Rows[0]["WorkTel"].ToString().IndexOf("-") > 0)
                    {
                        txtWorkTel_cityCode.Text = dtMember.Rows[0]["WorkTel"].ToString().Substring(0, dtMember.Rows[0]["WorkTel"].ToString().IndexOf("-"));
                        txtWorkTel.Text = dtMember.Rows[0]["WorkTel"].ToString().Substring(dtMember.Rows[0]["WorkTel"].ToString().IndexOf("-") + 1, dtMember.Rows[0]["WorkTel"].ToString().Length - dtMember.Rows[0]["WorkTel"].ToString().IndexOf("-") - 1);
                    }
                    else
                    {
                        txtWorkTel.Text = dtMember.Rows[0]["WorkTel"].ToString();
                    }
                }
                if (!Utility.IsDBNullOrNullValue(dtMember.Rows[0]["NezamKardanConfirmURL"]))
                {
                    Session["FileOfKardani"] = dtMember.Rows[0]["NezamKardanConfirmURL"];
                    HpKardani.NavigateUrl = "~/image/Members/NezamKardani/Request/" + Path.GetFileName(dtMember.Rows[0]["NezamKardanConfirmURL"].ToString());
                    HpKardani.ClientVisible = true;
                    HDFlpResident["Kardani"] = 1;
                    ChkBKardani.Checked = true;
                    panelKardani.ClientVisible = true;
                }
                else
                {
                    Session["FileOfKardani"] =null;
                    HpKardani.NavigateUrl = "";
                    HpKardani.ClientVisible = false;
                    HDFlpResident["Kardani"] = 0;
                    ChkBKardani.Checked = false;
                    panelKardani.ClientVisible = false;
                }

            }
        }

        if (!ChEnteghali.Checked)
        {
            //Page.ClientScript.RegisterStartupScript(GetType(), "key", "<script>document.getElementById('tblT').style.display='none';</script>");
            panelTransfer.ClientVisible = false;
            PersianDateValidatorTransfer.Enabled = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "key9", "<script>ValidatorEnable(document.getElementById('" + PersianDateValidatorTransfer.ClientID + "'),false);</script>");
        }
        else
        {
            //Page.ClientScript.RegisterStartupScript(GetType(), "key", "<script>document.getElementById('tblT').style.display='block';</script>");
            panelTransfer.ClientVisible = true;
            PersianDateValidatorTransfer.Enabled = true;
            Page.ClientScript.RegisterStartupScript(GetType(), "key9", "<script>ValidatorEnable(document.getElementById('" + PersianDateValidatorTransfer.ClientID + "'),true);</script>");

        }
        if (drdSexId.Value != null)
        {
            if (Convert.ToInt32(drdSexId.Value) == (int)TSP.DataManager.SexManager.Sex.Male)
            {
                lblSolFile.ClientVisible = true;
                lblWarnSolImage.ClientVisible = true;
                flpSoldier.ClientVisible = true;
                HpSoldier.ClientVisible = true;

                lblSolFileBack.ClientVisible = true;
                flpSoldierBack.ClientVisible = true;
                HpSoldierBack.ClientVisible = true;
            }
            else
            {
                lblSolFile.ClientVisible = false;
                lblWarnSolImage.ClientVisible = false;
                flpSoldier.ClientVisible = false;
                HpSoldier.ClientVisible = false;

                lblSolFileBack.ClientVisible = false;
                flpSoldierBack.ClientVisible = false;
                HpSoldierBack.ClientVisible = false;
            }
        }
        else
        {
            lblSolFile.ClientVisible = false;
            lblWarnSolImage.ClientVisible = false;
            flpSoldier.ClientVisible = false;
            HpSoldier.ClientVisible = false;

            lblSolFileBack.ClientVisible = false;
            flpSoldierBack.ClientVisible = false;
            HpSoldierBack.ClientVisible = false;
        }


    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["FileOfMember"] == null)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "تصویر را انتخاب نمایید";
                return;
            }
            if (Session["FileOfResident"] == null)/*&& ChkBResident.Checked)*/
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "تصویر مدرک سکونت را انتخاب نمایید";
                return;
            }
            if ((drdSoId.SelectedIndex == 3 || drdSoId.SelectedIndex == 4 || drdSoId.SelectedIndex == 5) && MilitaryCommitment.Checked == false)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "تعهد ارائه کارت پایان خدمت یا معافیت از خدمت جهت اخذ پروانه اشتغال بکار را انتخاب نمایید";
                return;
            }
            if (Session["FileOfKardani"] == null && ChkBKardani.Checked)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "تصویر استعلام عدم عضویت در نظام کاردانی را انتخاب نمایید";
                return;
            }
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();

            DataTable dtMe2 = MemberManager.SelectActiveMembers(-1, txtSSN.Text.Trim(), txtFirstName.Text.Trim(), txtLastName.Text.Trim());
            if (dtMe2.Rows.Count > 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات شما پیش از این در سیستم ثبت شده است.کد عضویت شما " + dtMe2.Rows[0]["MeId"].ToString() + " می باشد.در صورتی که از ثبت نام شما بیش از" + Utility.GetMembershipRegTimeout() + " روز گذشته است و هنوز اقدام به تحویل مدارک خود به سازمان ننموده اید،<b>بدون تلاش برای ورود به پرتال خود</b> جهت ارائه مدارک به سازمان مراجعه نمایید.";
                return;
            }

            TSP.DataManager.TempMemberManager TempMemberManager = new TSP.DataManager.TempMemberManager();
            TempMemberManager.SearchTempMember(-1, txtFirstName.Text.Trim(), txtLastName.Text.Trim());
            if (TempMemberManager.DataTable.Rows.Count > 0)
            {
                DataRow[] drTMe = TempMemberManager.DataTable.Select("SSN= '" + txtSSN.Text.Trim() + "'" + " and MsId<>2" + " and InActive=0");
                if (drTMe.Length > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات شما پیش از این در سیستم ثبت شده است.نام کاربری شما " + "M" + drTMe[0]["TMeId"].ToString() + " می باشد.در صورت عدم تحویل مدارک خود به واحد عضویت در موعد مقرر ،هرچه سریعتر با در دست داشتن مدارک مربوطه به این واحد مراجعه نمایید.";
                    return;
                }
            }

            TempMemberManager.FindBySSN(txtSSN.Text.Trim());
            if (TempMemberManager.Count > 0)
            {
                DataRow[] drTMe = TempMemberManager.DataTable.Select("MsId<>2" + " and InActive=0");
                if (drTMe.Length > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات شما پیش از این در سیستم ثبت شده است.نام کاربری شما " + "M" + TempMemberManager[0]["TMeId"].ToString() + " می باشد.در صورت عدم تحویل مدارک خود به واحد عضویت در موعد مقرر ،هرچه سریعتر با در دست داشتن مدارک مربوطه به این واحد مراجعه نمایید.";
                    return;
                }
            }

            #region EnteghaliValidator
            if (ChEnteghali.Checked == true)
            {
                if (string.IsNullOrEmpty(txtTDate.Text))
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "تاریخ انتقالی را وارد نمایید";
                    return;
                }
                if (Session["LetterImg"] == null)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "تصویر نامه انتقالی لازم می باشد";
                    return;
                }
            }
            #endregion

            string HomeTel = "", fax = "", WorkTel = "";

            if (txtHometel_cityCode.Text != "" && txtHometel.Text != "")
                HomeTel = txtHometel_cityCode.Text + "-" + txtHometel.Text;
            else if (txtHometel.Text != "")
                HomeTel = txtHometel.Text;
            if (txtWorkTel_cityCode.Text != "" && txtWorkTel.Text != "")
                WorkTel = txtWorkTel_cityCode.Text + "-" + txtWorkTel.Text;
            else if (txtWorkTel.Text != "")
                WorkTel = txtWorkTel.Text;
            if (txtFaxNo_cityCode.Text != "" && txtFaxNo.Text != "")
                fax = txtFaxNo_cityCode.Text + "-" + txtFaxNo.Text;
            else if (txtFaxNo.Text != "")
                fax = txtFaxNo.Text;

            object AgentId = DBNull.Value;
            string AgentName = "";

            if (drdAgent.Value != null)
            {
                AgentId = drdAgent.Value;
                AgentName = drdAgent.SelectedItem.Text;
            }

            object MarId = DBNull.Value;
            string MarName = "";

            if (drdMarId.Value != null)
            {
                MarId = drdMarId.Value;
                MarName = drdMarId.SelectedItem.Text;
            }


            object SoId = DBNull.Value;
            string SoName = "";
            Boolean MilitaryCommit = false;
            if (drdSoId.Value != null)
            {
                SoId = drdSoId.Value;
                SoName = drdSoId.SelectedItem.Text;
                if (drdSoId.SelectedIndex == 3 || drdSoId.SelectedIndex == 4 || drdSoId.SelectedIndex == 5)
                {
                    MilitaryCommit = MilitaryCommitment.Checked;
                }

            }

            object CitId = DBNull.Value;
            string CitName = "";

            if (drdCitId.Value != null)
            {
                CitId = drdCitId.Value;
                CitName = drdCitId.SelectedItem.Text;
            }

            object TPr = DBNull.Value;
            string PrName = "";

            if (ComboTPr.Value != null)
            {
                TPr = ComboTPr.Value;
                PrName = ComboTPr.SelectedItem.Text;
            }

            object WorkPO = DBNull.Value;
            if (!string.IsNullOrEmpty(txtWorkPO.Text))
                WorkPO = txtWorkPO.Text;

            object HomePO = DBNull.Value;
            if (!string.IsNullOrEmpty(txtHomePO.Text))
                HomePO = txtHomePO.Text;

            string PerDate = Utility.GetDateOfToday();
            if (Session["Member"] == null)
            {
                CreateDataMeDataTable();
            }
            if (((DataTable)Session["Member"]).Rows.Count == 0)
            {
                #region InsertMember
                dtMember = (DataTable)Session["Member"];
                DataRow drMember = dtMember.NewRow();
                drMember["FirstName"] = txtFirstName.Text;
                drMember["LastName"] = txtLastName.Text;
                drMember["FirstNameEn"] = txtFirstNameEn.Text;
                drMember["LastNameEn"] = txtLastNameEn.Text;
                drMember["FatherName"] = txtFatherName.Text;
                drMember["BirthDate"] = txtBirthDate.Text;
                drMember["BirthPlace"] = txtBirhtPlace.Text;
                drMember["IdNo"] = txtIdNo.Text;
                drMember["IssuePlace"] = txtIssuePlace.Text;
                drMember["SSN"] = txtSSN.Text;
                drMember["MobileNo"] = txtMobileNo.Text;
                drMember["HomeAdr"] = txtHomeAdr.Text;
                drMember["HomeTel"] = HomeTel;
                drMember["HomePO"] = HomePO;
                drMember["WorkAdr"] = txtWorkAdr.Text;
                drMember["WorkTel"] = WorkTel;
                drMember["WorkPO"] = WorkPO;
                drMember["FaxNo"] = fax;
                drMember["BankAccNo"] = txtBankAccNo.Text;
                drMember["SoId"] = SoId;
                drMember["SoName"] = SoName;
                drMember["MilitaryCommitment"] = MilitaryCommit;
                drMember["MsId"] = (int)TSP.DataManager.TemporaryMemberStatus.Pending;
                drMember["MrsId"] = (int)TSP.DataManager.MembershipRegistrationStatus.Pending;
                drMember["SexId"] = drdSexId.Value;
                drMember["SexName"] = drdSexId.SelectedItem.Text;
                drMember["MarId"] = MarId;
                drMember["MarName"] = MarName;
                drMember["MeNo"] = 0;
                drMember["FileNo"] = DBNull.Value;
                drMember["RelId"] = DBNull.Value; ;
                drMember["RelName"] = "";
                drMember["Nationality"] = txtNationality.Text;
                drMember["Website"] = txtWebsite.Text;
                drMember["Email"] = txtEmail.Text;
                drMember["CitId"] = CitId;
                drMember["CitName"] = CitName;
                drMember["AgentId"] = AgentId;
                drMember["AgentName"] = AgentName;
                drMember["MarName"] = MarName;
                drMember["Description"] = txtDesc.Text;
                drMember["CreateDate"] = Utility.GetDateOfToday();
                drMember["ModifiedDate"] = DateTime.Now;
                if (ChEnteghali.Checked)
                {
                    drMember["TPrId"] = TPr;
                    drMember["TPrName"] = PrName;
                    drMember["TransferDate"] = txtTDate.Text;
                    if (ChbTCheckFileNo.Checked)
                        drMember["TFileNo"] = txtTFileNo.Text;
                    else
                        drMember["TFileNo"] = "";
                    drMember["TMeNo"] = txtTMeNo.Text;
                }
                else
                {
                    drMember["TPrId"] = "";
                    drMember["TPrName"] = "";
                    drMember["TransferDate"] = "";
                    drMember["TMeNo"] = "";
                    drMember["TFileNo"] = "";
                }
                if (ChkBKardani.Checked)
                {
                    drMember["NezamKardanConfirmURL"] = "~/image/Members/NezamKardani/Request/"+ Session["FileOfKardani"];
                }
                else
                {
                    drMember["NezamKardanConfirmURL"] = DBNull.Value;
                }
                dtMember.Rows.Add(drMember);

                #endregion
            }
            else
            {
                #region EditMember
                dtMember = (DataTable)Session["Member"];
                DataRow drMember = dtMember.Rows[0];
                drMember.BeginEdit();
                drMember["FirstName"] = txtFirstName.Text;
                drMember["LastName"] = txtLastName.Text;
                drMember["FirstNameEn"] = txtFirstNameEn.Text;
                drMember["LastNameEn"] = txtLastNameEn.Text;
                drMember["FatherName"] = txtFatherName.Text;
                drMember["BirthDate"] = txtBirthDate.Text;
                drMember["BirthPlace"] = txtBirhtPlace.Text;
                drMember["IdNo"] = txtIdNo.Text;
                drMember["IssuePlace"] = txtIssuePlace.Text;
                drMember["SSN"] = txtSSN.Text;
                drMember["MobileNo"] = txtMobileNo.Text;
                drMember["HomeAdr"] = txtHomeAdr.Text;
                drMember["HomeTel"] = HomeTel;
                drMember["HomePO"] = HomePO;
                drMember["WorkAdr"] = txtWorkAdr.Text;
                drMember["WorkTel"] = WorkTel;
                drMember["WorkPO"] = WorkPO;
                drMember["FaxNo"] = fax;
                drMember["BankAccNo"] = txtBankAccNo.Text;
                drMember["SoId"] = SoId;
                drMember["MilitaryCommitment"] = MilitaryCommit;
                drMember["SoName"] = SoName;
                drMember["SexId"] = drdSexId.Value;
                drMember["SexName"] = drdSexId.SelectedItem.Text;
                drMember["MarId"] = MarId;
                drMember["MarName"] = MarName;
                drMember["MeNo"] = 0;
                drMember["FileNo"] = DBNull.Value;
                drMember["RelId"] = DBNull.Value;
                drMember["RelName"] = "";
                drMember["Nationality"] = txtNationality.Text;
                drMember["Website"] = txtWebsite.Text;
                drMember["Email"] = txtEmail.Text;
                drMember["CitId"] = CitId;
                drMember["CitName"] = CitName;
                drMember["AgentId"] = AgentId;
                drMember["AgentName"] = AgentName;
                drMember["MarName"] = MarName;
                drMember["Description"] = txtDesc.Text;
                if (ChEnteghali.Checked)
                {
                    drMember["TPrId"] = TPr;
                    drMember["TPrName"] = PrName;
                    drMember["TransferDate"] = txtTDate.Text;
                    if (ChbTCheckFileNo.Checked)
                        drMember["TFileNo"] = txtTFileNo.Text;
                    else
                        drMember["TFileNo"] = "";
                    drMember["TMeNo"] = txtTMeNo.Text;
                }
                else
                {
                    drMember["TPrId"] = "";
                    drMember["TPrName"] = "";
                    drMember["TransferDate"] = "";
                    drMember["TMeNo"] = "";
                    drMember["TFileNo"] = "";
                }
                if (ChkBKardani.Checked)
                {
                    drMember["NezamKardanConfirmURL"] = "~/image/Members/NezamKardani/Request/" + Session["FileOfKardani"];
                }
                else
                {
                    drMember["NezamKardanConfirmURL"] = DBNull.Value;
                }
                drMember.EndEdit();

                #endregion
            }

            Session["Member"] = dtMember;
            Response.Redirect("WizardMemberLicence.aspx");
        }
        catch (Exception ex)
        {

        }
    }
    private void CreateDataMeDataTable()
    {
        dtMember.Columns.Add("FirstName");
        dtMember.Columns.Add("LastName");
        dtMember.Columns.Add("FirstNameEn");
        dtMember.Columns.Add("LastNameEn");
        dtMember.Columns.Add("FatherName");
        dtMember.Columns.Add("BirthDate");
        dtMember.Columns.Add("BirthPlace");
        dtMember.Columns.Add("IdNo");
        dtMember.Columns.Add("IssuePlace");
        dtMember.Columns.Add("SSN");
        dtMember.Columns.Add("MobileNo");
        dtMember.Columns.Add("HomeAdr");
        dtMember.Columns.Add("HomeTel");
        dtMember.Columns.Add("HomePO");
        dtMember.Columns.Add("WorkAdr");
        dtMember.Columns.Add("WorkTel");
        dtMember.Columns.Add("WorkPO");
        dtMember.Columns.Add("FaxNo");
        dtMember.Columns.Add("BankAccNo");
        dtMember.Columns.Add("SoId");
        dtMember.Columns.Add("MilitaryCommitment");
        dtMember.Columns.Add("SoName");
        dtMember.Columns.Add("MsId");
        dtMember.Columns.Add("MrsId");
        dtMember.Columns.Add("SexId");
        dtMember.Columns.Add("SexName");
        dtMember.Columns.Add("MarId");
        dtMember.Columns.Add("MarName");
        dtMember.Columns.Add("MeNo");
        dtMember.Columns.Add("FileNo");
        dtMember.Columns.Add("RelId");
        dtMember.Columns.Add("RelName");
        dtMember.Columns.Add("Nationality");
        dtMember.Columns.Add("Website");
        dtMember.Columns.Add("Email");
        dtMember.Columns.Add("Description");
        dtMember.Columns.Add("CreateDate");
        dtMember.Columns.Add("ModifiedDate");
        dtMember.Columns.Add("TPrId");
        dtMember.Columns.Add("TPrName");
        dtMember.Columns.Add("TransferDate");
        dtMember.Columns.Add("TMeNo");
        dtMember.Columns.Add("TFileNo");
        dtMember.Columns.Add("CitId");
        dtMember.Columns.Add("CitName");
        dtMember.Columns.Add("AgentId");
        dtMember.Columns.Add("AgentName");
        dtMember.Columns.Add("NezamKardanConfirmURL");

        Session["Member"] = dtMember;
    }

    #region SaveImage
    protected void flpImage_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImage(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected string SaveImage(UploadedFile uploadedFile)
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

            } while (File.Exists(MapPath("~/image/Members/Person/Request/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            string tempFileName2 = MapPath("~/Image/Temp/") + ret2;

            uploadedFile.SaveAs(tempFileName, true);
            Utility.FixedSize(tempFileName, tempFileName2, Utility.HorRes, Utility.VerRes);
            Session["FileOfMember"] = tempFileName2;
            // Session["FileOfMember"] = ret;

        }
        return ret;
    }

    protected void FlpTLetter_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveImageLetter(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected string SaveImageLetter(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/image/Members/Transport/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            // Session["ExPlaceUpload"] = tempFileName;
            Session["LetterImg"] = tempFileName;

        }
        return ret;
    }

    protected void flpSign_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
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

            } while (File.Exists(MapPath("~/image/Members/Sign/Request/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            //   string tempFileName2 = MapPath("~/Image/Temp/") + ret2;

            uploadedFile.SaveAs(tempFileName, true);
            //  Utility.FixedSize(tempFileName, tempFileName2, Utility.GetMeSign_HorRes(), Utility.GetMeSign_VerRes());
            Session["FileOfSign"] = tempFileName;
            //Session["FileMeSign"] = ret;

        }
        return ret;
    }

    protected void flpIdNo_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageIdNo(e.UploadedFile, f.ID);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected string SaveImageIdNo(UploadedFile uploadedFile, string id)
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

            } while (File.Exists(MapPath("~/image/Members/IdNo/Request/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            // string tempFileName2 = MapPath("~/Image/Temp/") + ret2;

            uploadedFile.SaveAs(tempFileName, true);
            //  Utility.FixedSize(tempFileName, tempFileName2, Utility.GetIdNo_HorRes(), Utility.GetIdNo_VerRes());

            if (id == "flpIdNo")
                Session["FileOfIdNo"] = tempFileName;

            if (id == "flpIdNoP2")
                Session["FileOfIdNoP2"] = tempFileName;

            if (id == "flpIdNoPDes")
                Session["FileOfIdNoPDes"] = tempFileName;

        }
        return ret;
    }

    protected void flpSSN_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageSSN(e.UploadedFile, f.ID);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected string SaveImageSSN(UploadedFile uploadedFile, string id)
    {
        string ret = "";
        string ret2 = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
                // ret2 = Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/image/Members/SSN/Request/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            //     string tempFileName2 = MapPath("~/Image/Temp/") + ret2;

            uploadedFile.SaveAs(tempFileName, true);
            //  Utility.FixedSize(tempFileName, tempFileName2, Utility.GetSSN_HorRes(), Utility.GetSSN_VerRes());
            if (id == "flpSSN")
                Session["FileOfSSN"] = tempFileName;

            if (id == "flpSSNBack")
                Session["FileOfSSNBack"] = tempFileName;

        }
        return ret;
    }

    protected void flpSoldier_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageSoldier(e.UploadedFile, f.ID);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected string SaveImageSoldier(UploadedFile uploadedFile, string id)
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

            } while (File.Exists(MapPath("~/image/Members/Soldier/Request/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            //  string tempFileName2 = MapPath("~/Image/Temp/") + ret2;

            uploadedFile.SaveAs(tempFileName, true);
            //Utility.FixedSize(tempFileName, tempFileName2, Utility.GetSoldierCard_HorRes(), Utility.GetSoldierCard_VerRes());
            if (id == "flpSoldier")
                Session["FileOfSol"] = tempFileName;
            //Session["FileOfSol"] = ret;
            if (id == "flpSoldierBack")
                Session["FileOfSolBack"] = tempFileName;

        }
        return ret;
    }

    protected void flpResident_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageResident(e.UploadedFile, f.ID);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected string SaveImageResident(UploadedFile uploadedFile, string id)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/image/Members/Resident/Request/") + ret) == true || File.Exists(MapPath("~/Image/Temp/") + ret) == true);
            string tempFileName = MapPath("~/Image/Temp/") + ret;
            uploadedFile.SaveAs(tempFileName, true);

            if (id == "flpResident")
                Session["FileOfResident"] = ret;
        }
        return ret;
    }

    protected void flpKardani_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            ASPxUploadControl f = (ASPxUploadControl)sender;
            e.CallbackData = SaveImageKardani(e.UploadedFile, f.ID);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }
    protected string SaveImageKardani(UploadedFile uploadedFile, string id)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/image/Members/NezamKardani/Request/") + ret) == true);
            string tempFileName = MapPath("~/image/Members/NezamKardani/Request/") + ret;
            uploadedFile.SaveAs(tempFileName, true);

            if (id == "flpKardani")
                Session["FileOfKardani"] = ret;
        }
        return ret;
    }
    #endregion
    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.WizardMember).ToString());
    }


}
