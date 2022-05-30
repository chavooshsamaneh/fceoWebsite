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
public partial class Employee_Amoozesh_AddPeriodAttenders: System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        cmbMeId.DataBind();
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["PAId"]) || string.IsNullOrEmpty(Request.QueryString["PPId"]))
            {
                Response.Redirect("PeriodAttender.aspx");
                return;
            }

            TSP.DataManager.Permission per = TSP.DataManager.PeriodAttenderManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnDisActive.Enabled = per.CanEdit;
            btnDisActive2.Enabled = per.CanEdit;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                PeriodAtId.Value = Server.HtmlDecode(Request.QueryString["PAId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            string PageMode = Utility.DecryptQS(PgMode.Value);
            string PAId = Utility.DecryptQS(PeriodAtId.Value);
            //if ((!string.IsNullOrEmpty(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PAId"]).ToString()))) && (CrId == -1))
            //    //CrId = int.Parse(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PAId"]).ToString()));
            if (string.IsNullOrEmpty(PageMode))
            {
                Response.Redirect("../../ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            if (!string.IsNullOrEmpty(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PPId"].ToString()))))
            {
                string PPId="";
                try
                {
                    PPId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PPId"].ToString()));
                }
                catch
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }

                TSP.DataManager.PeriodPresentManager PPmanager = new TSP.DataManager.PeriodPresentManager();
                PPmanager.FindByCode(int.Parse(PPId));
                if (PPmanager.Count == 1)
                {
                    if (PPmanager[0]["Type"].ToString() == "0")
                        cmbTypeOfReg.Visible = true;
                    else if (PPmanager[0]["Type"].ToString() == "1")
                    {
                        cmbTypeOfReg.Visible = false;
                        cmbTypeOfReg.SelectedIndex = cmbTypeOfReg.Items.IndexOfValue(2);
                    }
                }
            }
            else
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            switch (PageMode)
            {
                case "View":
                    Disable();

                    if (string.IsNullOrEmpty(PAId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    btnEdit.Enabled = per.CanEdit;
                    btnEdit2.Enabled = per.CanEdit;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    FillForm(int.Parse(PAId));
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    break;
                case "New":
                    Enable();
                    ASPxRoundPanel2.HeaderText = "جدید";
                    btnDisActive.Enabled = false;
                    btnDisActive2.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    ClearForm();
                    break;
                case "Edit":
                    Enable();

                    if (string.IsNullOrEmpty(PAId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    btnDisActive.Enabled =true;
                    btnDisActive2.Enabled =true;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    FillForm(int.Parse(PAId));
                    ASPxRoundPanel2.Enabled = true;
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    break;
            }

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;


        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDisActive"] != null)
            this.btnDisActive.Enabled = this.btnDisActive2.Enabled = (bool)this.ViewState["BtnDisActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["btnDisActiveImg"] != null)
            this.btnDisActive.Image.Url = this.btnDisActive2.Image.Url = (string)this.ViewState["btnDisActiveImg"];
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //Response.Redirect("AddCourse.aspx?PageMode" + Utility.EncryptQS("New"));
        PeriodAtId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();

        TSP.DataManager.Permission per = TSP.DataManager.PeriodAttenderManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnDisActive.Enabled = false;
        btnDisActive2.Enabled = false;
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
        this.ViewState["BtnSave"] = btnSave.Enabled;

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        string PAId = Utility.DecryptQS(PeriodAtId.Value);

        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                Insert();

                //Response.Redirect("AddCourse.aspx?PAId=" + Utility.EncryptQS(CrId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));

            }
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(PAId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    Edit(int.Parse(PAId));
                }

            }

        }



    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {

        string PageMode = Utility.DecryptQS(PgMode.Value);
        string PAId = Utility.DecryptQS(PeriodAtId.Value);

        if (string.IsNullOrEmpty(PAId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        if (string.IsNullOrEmpty(PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {

            if (PageMode == "Edit" && (!string.IsNullOrEmpty(PAId)))
            {
                Delete(int.Parse(PAId));
            }
        }
        //Response.Redirect("AddCourse.aspx?" + PageMode + Utility.EncryptQS("New"));
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(Server.HtmlDecode(Request.QueryString["PPId"].ToString())))
                Response.Redirect("PeriodAttender.aspx?PPId=" + Server.HtmlDecode(Request.QueryString["PPId"].ToString()));
        }        
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string pageMode = Utility.DecryptQS(PgMode.Value);
        string PAId = Utility.DecryptQS(PeriodAtId.Value);

        if (string.IsNullOrEmpty(PAId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (string.IsNullOrEmpty(pageMode) && pageMode != "View")
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            else
            {
                Enable();

                TSP.DataManager.Permission per = TSP.DataManager.PeriodAttenderManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                this.ViewState["BtnSave"] = btnSave.Enabled;

                ASPxRoundPanel2.HeaderText = "ویرایش";
            }

            //Response.Redirect("AddCourse.aspx?PAId=" + Utility.EncryptQS(PAId.ToString()) + "&PageMode=" + Server.HtmlDecode(Request.QueryString["Edit"]));

        }

    }

    protected void chbInActive_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void cmbMemberType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbMemberType.Value.ToString() == "0")
        {
            lbMeID.Visible = false;
            cmbMeId.Visible = false;
            lbMjId.Visible = false;
            cmbMjId.Visible = false;
            lbOtpId.Visible = true;
            cmbOtherPerson.Visible = true;
        }
        else if (cmbMemberType.Value.ToString() == "1")
        {
            lbMeID.Visible = true;
            cmbMeId.Visible = true;
            lbMjId.Visible = false;
            cmbMjId.Visible = false;
            lbOtpId.Visible = false;
            cmbOtherPerson.Visible = false;
        }
        else
        {
            lbMeID.Visible = true;
            cmbMeId.Visible = true;
            lbMjId.Visible = false;
            cmbMjId.Visible = false;
            lbOtpId.Visible = false;
            cmbOtherPerson.Visible = false;
        }
        cmbMeId.DataBind();
        cmbOtherPerson.DataBind();
    }

    protected void BtnOtherPerson_Click(object sender, EventArgs e)
    {
       // try
       // {
            if (!string.IsNullOrEmpty(Server.HtmlDecode(Request.QueryString["PPId"].ToString())))
            {
                string PPId = Utility.DecryptQS(Request.QueryString["PPId"].ToString());
                Response.Redirect("AddOtherPersons.aspx?PAId=" + PeriodAtId.Value + "&PPId=" + Utility.EncryptQS(PPId) + "&PageMode=" + PgMode.Value);
            }
        //}
        //catch(Exception err)
        //{
        //    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
        //    return;
        //}

    }

    protected void btnDisActive_Click(object sender, EventArgs e)
    {
        TSP.DataManager.PeriodAttenderManager PeriodAttenderManager = new TSP.DataManager.PeriodAttenderManager();
        string ObsId = Utility.DecryptQS(PeriodAtId.Value.ToString());
        PeriodAttenderManager.FindByCode(int.Parse(ObsId));
        if (Convert.ToBoolean(PeriodAttenderManager[0]["InActive"].ToString()))
        {
            Active(int.Parse(ObsId));
        }
        else
        {
            InActive(int.Parse(ObsId));
        }
    }
    #endregion 

    #region Methods
    protected void FillForm(int PAId)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        cmbOtherPerson.DataBind();
        cmbTypeOfReg.DataBind();
        cmbMjId.DataBind();
        cmbMemberType.DataBind();
        cmbMeId.DataBind();
        TSP.DataManager.PeriodAttenderManager manager = new TSP.DataManager.PeriodAttenderManager();

        if (!string.IsNullOrEmpty(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PPId"].ToString()))))
        {
            manager.FindByCode(PAId);
            if (manager.Count == 1)
            {
                cmbMemberType.SelectedIndex = cmbMemberType.Items.IndexOfValue(manager[0]["Type"].ToString());
                cmbMemberType_SelectedIndexChanged(this, new EventArgs());
                if (cmbMemberType.Value.ToString() == "1")
                {
                    cmbMeId.SelectedIndex = cmbMeId.Items.IndexOfValue(manager[0]["MeID"]);
                }
                else if (cmbMemberType.Value.ToString() == "0")
                {
                    cmbOtherPerson.SelectedIndex = cmbOtherPerson.Items.IndexOfValue(manager[0]["MeID"]);
                }
                txtDesc.Text = manager[0]["Description"].ToString();
                cmbTypeOfReg.SelectedIndex = cmbTypeOfReg.Items.IndexOfValue(manager[0]["TypeOfReg"].ToString());
               ChangeDisableButtonIcon(Convert.ToBoolean(manager[0]["InActive"].ToString()));
            }
            string PPId = "";
            try
            {
                PPId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PPId"].ToString()));
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            TSP.DataManager.PeriodPresentManager PPmanager = new TSP.DataManager.PeriodPresentManager();
            PPmanager.FindByCode(int.Parse(PPId));
            if (PPmanager.Count == 1)
            {
                if (PPmanager[0]["Type"].ToString() == "0")
                {
                    cmbTypeOfReg.Visible = true;
                    cmbTypeOfReg.SelectedIndex = cmbTypeOfReg.Items.IndexOfValue(manager[0]["TypeOfReg"].ToString());
                }
                else if (PPmanager[0]["Type"].ToString() == "1")
                {
                    cmbTypeOfReg.Visible = false;

                }
            }
        }

    }

    protected void ClearForm()
    {

        txtDesc.Text = "";
        for (int i = 0; i < ASPxRoundPanel2.Controls.Count; i++)
        {
            try
            {
                DevExpress.Web.ASPxCheckBox co = (DevExpress.Web.ASPxCheckBox)ASPxRoundPanel2.Controls[i];
                co.Checked = false;
            }
            catch
            {
            }

            try
            {
                DevExpress.Web.ASPxComboBox cmb = (DevExpress.Web.ASPxComboBox)ASPxRoundPanel2.Controls[i];
                cmb.DataBind();
                //cmb.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
                cmb.SelectedIndex = -1;
            }
            catch
            {
            }


        }


    }

    protected void Disable()
    {
        cmbTypeOfReg.Enabled = false;
        cmbOtherPerson.Enabled = false;
        cmbMjId.Enabled = false;
        cmbMemberType.Enabled = false;
        cmbMeId.Enabled = false;
        txtDesc.Enabled = false;
    }

    protected void Enable()
    {
        cmbTypeOfReg.Enabled = true;
        cmbOtherPerson.Enabled = true;
        cmbMjId.Enabled = true;
        cmbMemberType.Enabled = true;
        cmbMeId.Enabled = true;
        txtDesc.Enabled = true;

    }

    protected void Edit(int PAId)
    {

        TSP.DataManager.PeriodAttenderManager manager = new TSP.DataManager.PeriodAttenderManager();
        manager.FindByCode(PAId);
        if (manager.Count == 1)
        {
            try
            {
                manager[0].BeginEdit();
                manager[0]["Type"] = cmbMemberType.Value;
                if (cmbMemberType.Value.ToString() == "1")
                {
                    manager[0]["MeID"] = cmbMeId.Value;
                }
                else if (cmbMemberType.Value.ToString() == "0")
                {
                    manager[0]["MeID"] = cmbOtherPerson.Value;
                }
                manager[0]["TypeOfReg"] = cmbTypeOfReg.Value;
                manager[0]["Description"] = txtDesc.Text;
                manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                manager[0]["ModifiedDate"] = DateTime.Now;
                manager[0].EndEdit();
                int cn = manager.Save();
                if (cn == 1)
                {
                    //CrId = int.Parse(manager[0]["PAId"].ToString());
                    PeriodAtId.Value = Utility.EncryptQS(manager[0]["PAId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";

                    TSP.DataManager.Permission per = TSP.DataManager.PeriodAttenderManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                    btnDisActive.Enabled = true;
                    btnDisActive2.Enabled = true;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    this.ViewState["BtnEdit"] = btnEdit.Enabled;
                    this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام نشد";
                }
            }

            catch (Exception err)
            {


                if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                {
                    System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                    if (se.Number == 2601)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                    }
                    else if (se.Number == 2627)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "شخص مورد نظر قبلاً در این دوره ثبت نام شده.";
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
        else
        {
        }


    }

    protected void Insert()
    {
        DateTime dt = new DateTime();
        dt = DateTime.Now;
        System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
        string PerDate = string.Format("{0}/{1}/{2}", pDate.GetYear(dt).ToString().PadLeft(4, '0'), pDate.GetMonth(dt).ToString().PadLeft(2, '0'), pDate.GetDayOfMonth(dt).ToString().PadLeft(2, '0'));


        TSP.DataManager.PeriodAttenderManager manager = new TSP.DataManager.PeriodAttenderManager();
        TSP.DataManager.PeriodPresentManager PPmanager = new TSP.DataManager.PeriodPresentManager();
        if (!string.IsNullOrEmpty(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PPId"].ToString()))))
        {

            string PPId = "";
            try
            {
                PPId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PPId"].ToString()));
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            try
            {
                DataRow row = manager.NewRow();
                PPmanager.FindByCode(int.Parse(PPId));
                if (PPmanager.Count == 1)
                {
                    if (PPmanager[0]["Type"].ToString() == "1")
                    {
                        row["TypeOfReg"] = 2;

                    }
                    else
                        row["TypeOfReg"] = cmbTypeOfReg.Value;
                    row["PPId"] = int.Parse(PPId);
                    row["Type"] = cmbMemberType.Value;
                    if (cmbMemberType.Value.ToString() == "1")
                    {
                        row["MeID"] = cmbMeId.Value;
                    }
                    else if (cmbMemberType.Value.ToString() == "0")
                    {
                        row["MeID"] = cmbOtherPerson.Value;
                    }

                    row["CreateDate"] = PerDate;
                    row["Description"] = txtDesc.Text;
                    row["InActive"] = 0;
                    row["UserId"] = Utility.GetCurrentUser_UserId();
                    row["ModifiedDate"] = DateTime.Now;
                    manager.AddRow(row);

                    int cn = manager.Save();
                    if (cn == 1)
                    {
                        //CrId = int.Parse(manager[0]["PAId"].ToString());

                        PeriodAtId.Value = Utility.EncryptQS(manager[0]["PAId"].ToString());
                        PgMode.Value = Utility.EncryptQS("Edit");
                        ASPxRoundPanel2.HeaderText = "ویرایش";
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام شد";

                        TSP.DataManager.Permission per = TSP.DataManager.PeriodAttenderManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                        btnDisActive.Enabled = true;
                        btnDisActive2.Enabled = true;
                        btnEdit2.Enabled = false;
                        btnEdit.Enabled = false;
                        this.ViewState["BtnEdit"] = btnEdit.Enabled;
                        this.ViewState["BtnDisActive"] = btnDisActive.Enabled;
                    }


                    else
                    {

                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "ذخیره انجام نشد";
                    }

                }

            }
            catch (Exception err)
            {
                if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                {
                    System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                    if (se.Number == 2601)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                    }
                    else if (se.Number == 2627)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "شخص مورد نظر قبلاً در این دوره ثبت نام شده.";
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
        else
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }


    }

    protected void Active(int PAId)
    {

        TSP.DataManager.PeriodAttenderManager managerEdit = new TSP.DataManager.PeriodAttenderManager();
        managerEdit.FindByCode(PAId);
        if (managerEdit.Count == 1)
        {

            try
            {

                managerEdit[0].BeginEdit();
                managerEdit[0]["InActive"] = 0;
                managerEdit[0]["UserId"] =Utility.GetCurrentUser_UserId();
                managerEdit[0]["ModifiedDate"] = DateTime.Now;
                managerEdit[0].EndEdit();
                int cn = managerEdit.Save();
                if (cn == 1)
                {
                    ChangeDisableButtonIcon(false);
                    PeriodAtId.Value = Utility.EncryptQS(managerEdit[0]["PAId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "تغییرات انجام شد";

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "تغییرات انجام نشد";
                }
            }
            catch (Exception err)
            {


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
        else
        {
        }




    }

    protected void InActive(int PAId)
    {

        TSP.DataManager.PeriodAttenderManager managerEdit = new TSP.DataManager.PeriodAttenderManager();
        managerEdit.FindByCode(PAId);
        if (managerEdit.Count == 1)
        {

            try
            {

                managerEdit[0].BeginEdit();
                managerEdit[0]["InActive"] = 1;
                managerEdit[0]["UserId"] = Utility.GetCurrentUser_UserId();
                managerEdit[0]["ModifiedDate"] = DateTime.Now;
                managerEdit[0].EndEdit();
                int cn = managerEdit.Save();
                if (cn == 1)
                {
                    ChangeDisableButtonIcon(true);
                    PeriodAtId.Value = Utility.EncryptQS(managerEdit[0]["PAId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "تغییرات انجام شد";

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "تغییرات انجام نشد";
                }
            }
            catch (Exception err)
            {


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
        else
        {
        }




    }

    protected void Delete(int PAId)
    {

        TSP.DataManager.PeriodAttenderManager managerEdit = new TSP.DataManager.PeriodAttenderManager();
        managerEdit.FindByCode(PAId);
        if (managerEdit.Count == 1)
        {
            try
            {
                managerEdit[0].Delete();


                int cn = managerEdit.Save();
                if (cn == 1)
                {
                    //TeacherId.Value = managerEdit[0]["PAId"].ToString();
                    PeriodAtId.Value = Utility.EncryptQS("");
                    PgMode.Value = Utility.EncryptQS("New");
                    ASPxRoundPanel2.HeaderText = "جدید";
                    ClearForm();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "حذف انجام شد";
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    this.ViewState["BtnEdit"] = btnEdit.Enabled;
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "حذف انجام نشد";
                }
            }
            catch (Exception err)
            {

                if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                {
                    System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                    if (se.Number == 547)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                }


            }

        }
        else
        {
        }

    }

    private void ChangeDisableButtonIcon(Boolean InActive)
    {
        if (InActive)
        {
            btnDisActive.Image.Url = "~/Images/icons/button_ok.png";
            btnDisActive.ToolTip = "فعال کردن";
            btnDisActive2.Image.Url = "~/Images/icons/button_ok.png";
            btnDisActive2.ToolTip = "فعال کردن";
        }
        else
        {
            btnDisActive.Image.Url = "~/Images/icons/disactive.png";
            btnDisActive.ToolTip = "غیرفعال کردن";
            btnDisActive2.Image.Url = "~/Images/icons/disactive.png";
            btnDisActive2.ToolTip = "غیرفعال کردن";
        }
        this.ViewState["btnDisActiveImg"] = this.btnDisActive.Image.Url;
    }
    #endregion
}
