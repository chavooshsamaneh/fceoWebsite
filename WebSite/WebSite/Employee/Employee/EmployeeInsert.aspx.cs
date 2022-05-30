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

public partial class Employee_Employee_EmployeeInsert : System.Web.UI.Page
{
    //static string EmplId;
    #region Private Members
    // public static int Id;
    //  public static int EmpId2;
    //string EmplId;
    //string PageMode;

    int _EmpReId
    {
        get
        {
            try
            {
                return Convert.ToInt32(HiddenFieldEmployee["EmpReId"]);
            }
            catch
            {
                return Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["EmpReId"].ToString())));
            }
        }
        set
        {
            HiddenFieldEmployee["EmpReId"] = value;
        }
    }

    int _EmpId
    {
        get
        {
            try
            {
                return Convert.ToInt32(HiddenFieldEmployee["EmpId"]);
            }
            catch
            {
                return Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["EmpId"].ToString())));
            }
        }
        set
        {
            HiddenFieldEmployee["EmpId"] = value;
        }
    }


    string _PageMode
    {
        get
        {
            return HiddenFieldEmployee["PageMode"].ToString();
        }
        set
        {
            HiddenFieldEmployee["PageMode"] = value;
        }
    }

    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        lblImg.Text = "لطفاً تصویری با مشخصات " + Utility.VerRes + "*" + Utility.HorRes + " و " + Utility.dpi + " dpi انتخاب نمایید. ";

        if ((string.IsNullOrEmpty(Request.QueryString["EmpReId"])) || (string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["EmpId"])))
        {
            Response.Redirect("Employee.aspx");
            return;
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            Session["EmpImg"] = null;
            Session["EmpSign"] = null;

            //Check UserPermission
            TSP.DataManager.Permission per = TSP.DataManager.EmployeeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanEdit || per.CanNew;

            SetKeys();

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;

        }

        //if (Session["EmpImg"] != null)
        //{
        //    imgPic.ClientVisible = true;
        //    imgPic.ImageUrl = "~/image/Temp/" + Path.GetFileName(Session["EmpImg"].ToString());
        //    HDFlpMember["name"] = 1;
        //}
        //if (Session["EmpSign"] != null)
        //{
        //    HpSign.ClientVisible = true;
        //    HpSign.NavigateUrl = "~/image/Temp/" + Path.GetFileName(Session["EmpSign"].ToString());
        //}


        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];


        if (Utility.GetCurrentUser_AgentId() != Utility.GetMainAgentId())
        {
            cmbAgent.DataBind();
            this.cmbAgent.Enabled = false;
            this.cmbAgent.Value = Utility.GetCurrentUser_AgentId();
            HiddenFieldEmployee.Set("MainAgent", 0);
        }
        else
            HiddenFieldEmployee.Set("MainAgent", 1);

    }

    //protected void lbtnBack_Click(object sender, EventArgs e)
    //{
    //    Session["EmpImg"] = null;
    //    Response.Redirect("Employee.aspx");
    //}

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (_EmpReId == null)
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        if (string.IsNullOrEmpty(_PageMode) && _PageMode != "View")
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        if (!CheckPermitionForEdit(_EmpReId))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان ویرایش اطلاعات در این مرحله از جریان کار وجود ندارد.";
            return;
        }
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.EmployeeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //Set Button's Enable

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        EnableControls();

        _PageMode = "Edit";
        RoundPanelEmlpoyee.HeaderText = "ویرایش";
    }

    //protected void btnRefresh_Click(object sender, EventArgs e)
    //{
    //    //int i;
    //    //for (i = 0; i < Panel1.Controls.Count; i++)
    //    //{
    //    //    try
    //    //    {
    //    //        ((TextBox)Panel1.Controls[i]).Text = "";

    //    //    }
    //    //    catch
    //    //    {

    //    //    }
    //    //    try
    //    //    {
    //    //        ((DropDownList)Panel1.Controls[i]).DataBind();

    //    //    }
    //    //    catch
    //    //    {

    //    //    }


    //    //}
    //}

    //protected void ButtonRet_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("Employee.aspx");

    //}

    //protected void ASPxButton1_Click(object sender, EventArgs e)
    //{
    //    if (!(String.IsNullOrEmpty(Request.QueryString["EmpId"])))
    //        Response.Redirect("~/ReportForms/EmployeeReport.aspx?EmpId=" + Server.HtmlDecode(Request.QueryString["EmpId"]));

    //    else
    //        Response.Redirect("~/ReportForms/EmployeeReport.aspx?EmpId=" + Utility.EncryptQS(EmplId));

    //}

    protected void btnDeleteImg_Click(object sender, EventArgs e)
    {
        TSP.DataManager.EmployeeManager EmpManager = new TSP.DataManager.EmployeeManager();
        //int ID = int.Parse(Server.HtmlEncode(Request.QueryString["EmpId"]).ToString());
        string s = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["EmpId"]).ToString());

        if (string.IsNullOrEmpty(s))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }

        int ID = int.Parse(s);
        EmpManager.FindByCode(ID);

        if ((!string.IsNullOrEmpty(EmpManager[0]["Image"].ToString())) && (!string.IsNullOrEmpty(EmpManager[0]["ImgUrl"].ToString())) && (System.IO.File.Exists(Server.MapPath(EmpManager[0]["ImgUrl"].ToString()))))
        {
            try
            {
                System.IO.File.Delete(Server.MapPath(EmpManager[0]["ImgUrl"].ToString()));
                EmpManager[0].BeginEdit();
                EmpManager[0]["ImgUrl"] = DBNull.Value;
                EmpManager[0]["Image"] = DBNull.Value;
                EmpManager[0].EndEdit();
                EmpManager.Save();
                imgPic.ImageUrl = "~/images/person.gif/";
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "حذف انجام شد";

            }
            catch
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "حذف فایل امکان پذیر نمی باشد";
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        switch (_PageMode)
        {
            case "New":
                EnableControls();

                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                this.ViewState["BtnEdit"] = btnEdit.Enabled;

                InsertEmployee();
                break;
            case "Edit":
                if (_EmpReId == null)
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                EditEmloyee(_EmpReId);

                break;
            case "Change":
                if (_EmpId == null)
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                InsertEmployeeChangeRequest(_EmpId);
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && _EmpId != null)
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            Response.Redirect("Employee.aspx?PostId=" + Utility.EncryptQS(_EmpId.ToString()) + "&GrdFlt=" + GrdFlt);
        }
        else
        {
            Response.Redirect("Employee.aspx");
        }

    }

    protected void flpImg_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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

    protected void flpSign_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SaveSign(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.Permission per = TSP.DataManager.EmployeeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        _PageMode = "New";
        _EmpId = -1;
        _EmpReId = -1;

        RoundPanelEmlpoyee.HeaderText = "جدید";
        ASPxLabelStatus.Visible = false;
        CmbStatus.Visible = false;
        ClearForm();
        EnableControls();
    }
    #endregion

    #region Methods
    private void InsertEmployee()
    {
        string Address = txtAddress.Text;
        string BirthDate = txtBirthDate.Text;
        string BirthPlace = txtBirthPlace.Text;
        string Description = txtDescription.Text;
        string Email = txtEmail.Text;
        string EmpCode = txtEmpCode.Text;
        string FatherName = txtFatherName.Text;
        string FirstName = txtFirstName.Text;
        string FirstNameEn = txtFirstNameEn.Text;
        string IdNo = txtIdNo.Text;
        string LastName = txtLastName.Text;
        string LastNameEn = txtLastNameEn.Text;
        string MobileNo = txtMobileNo.Text;
        string Nationality = txtNationality.Text;
        string SSN = txtSSN.Text;
        string WebSite = txtWebSite.Text;

        string PropertyCodePC = txtPropertyCodePC.Text;
       
        string TerminalIdPcPos = txtTerminalIdPcPos.Text;
        string ComPortPcPos = txtComPortPcPos.Text;
        string AcceptorIdPcPos = txtAcceptorIdPcPos.Text;
        string SerialNoPcPos = txtSerialNoPcPos.Text;

        string TerminalIdPcPos2 = txtTerminalIdPcPos2.Text;
        string ComPortPcPos2 = txtComPortPcPos2.Text;
        string AcceptorIdPcPos2 = txtAcceptorIdPcPos2.Text;
        string SerialNoPcPos2 = txtSerialNoPcPos2.Text;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.EmployeeManager EmpManager = new TSP.DataManager.EmployeeManager();
        TSP.DataManager.EmployeeRequestManager EmployeeRequestManager = new TSP.DataManager.EmployeeRequestManager();
        TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        trans.Add(WorkFlowStateManager);
        trans.Add(EmpManager);
        trans.Add(LogManager);
        trans.Add(EmployeeRequestManager);

        try
        {

            int CurrentNmcId = FindNmcId();
            if (CurrentNmcId == -1)
                return;
            trans.BeginSave();
            DataRow dr = EmpManager.NewRow();
            dr["Address"] = Address;
            dr["BirthDate"] = BirthDate;
            dr["BirthPlace"] = BirthPlace;
            dr["CreateDate"] = Utility.GetDateOfToday();
            dr["Description"] = Description;
            dr["Email"] = Email;
            dr["EmpCode"] = Utility.GenRandomNum();//EmpCode;
            dr["FatherName"] = FatherName;
            dr["FirstName"] = FirstName;
            dr["FirstNameEn"] = FirstNameEn;
            dr["IdNo"] = IdNo;
            dr["LastName"] = LastName;
            dr["LastNameEn"] = LastNameEn;
            dr["MobileNo"] = MobileNo;
            dr["Nationality"] = Nationality;
            dr["SSN"] = SSN;
            dr["Tel"] = txtTel.Text;
            dr["WebSite"] = WebSite;

           
            dr["PropertyCodePC"] = PropertyCodePC;
            
            dr["TerminalIdPcPos"] = TerminalIdPcPos;
            dr["ComPortPcPos"] = ComPortPcPos;
            dr["AcceptorIdPcPos"] = AcceptorIdPcPos;
            dr["SerialNoPcPos"] = SerialNoPcPos;

            dr["TerminalIdPcPos2"] = TerminalIdPcPos2;
            dr["ComPortPcPos2"] = ComPortPcPos2;
            dr["AcceptorIdPcPos2"] = AcceptorIdPcPos2;
            dr["SerialNoPcPos2"] = SerialNoPcPos2;

            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;

            if (cmbSexId.Value != null)
                dr["SexId"] = cmbSexId.Value;
            else
                dr["SexId"] = DBNull.Value;
            if (cmbMarId.Value != null)
                dr["MarId"] = cmbMarId.Value;
            else
                dr["MarId"] = DBNull.Value;
            if (cmbPartId.Value != null)
                dr["PartId"] = cmbPartId.Value;
            else
                dr["PartId"] = DBNull.Value;
            if (cmbRelId.Value != null)
                dr["RelId"] = cmbRelId.Value;
            else
                dr["RelId"] = DBNull.Value;
            if (cmbAgent.Value != null)
                dr["AgentId"] = cmbAgent.Value;
            else
                dr["AgentId"] = DBNull.Value;

            dr["EmpStatus"] = 0;

            EmpManager.AddRow(dr);
            int cnt = EmpManager.Save();

            if (cnt <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            EmpManager.DataTable.AcceptChanges();
            _EmpId = Convert.ToInt32(EmpManager[0]["EmpId"]);

            DataRow EmpReqRow = EmployeeRequestManager.NewRow();
            EmpReqRow["EmpId"] = _EmpId;
            EmpReqRow["Type"] = 0;
            EmpReqRow["Address"] = EmpManager[0]["Address"].ToString();
            EmpReqRow["BirthDate"] = EmpManager[0]["BirthDate"].ToString();
            EmpReqRow["BirthPlace"] = EmpManager[0]["BirthPlace"].ToString();
            EmpReqRow["CreateDate"] = EmpManager[0]["CreateDate"].ToString();
            EmpReqRow["Description"] = EmpManager[0]["Description"].ToString();
            EmpReqRow["Email"] = EmpManager[0]["Email"].ToString();
            EmpReqRow["EmpCode"] = _EmpId;
            EmpReqRow["FatherName"] = EmpManager[0]["FatherName"].ToString();
            EmpReqRow["FirstName"] = EmpManager[0]["FirstName"].ToString();
            EmpReqRow["FirstNameEn"] = EmpManager[0]["FirstNameEn"].ToString();

            EmpReqRow["IdNo"] = EmpManager[0]["IdNo"].ToString();
            EmpReqRow["LastName"] = EmpManager[0]["LastName"].ToString();
            EmpReqRow["LastNameEn"] = EmpManager[0]["LastNameEn"].ToString();
            EmpReqRow["MobileNo"] = EmpManager[0]["MobileNo"].ToString();
            EmpReqRow["Nationality"] = EmpManager[0]["Nationality"].ToString();
            EmpReqRow["SSN"] = EmpManager[0]["SSN"].ToString();
            EmpReqRow["Tel"] = EmpManager[0]["Tel"].ToString();
            EmpReqRow["WebSite"] = EmpManager[0]["WebSite"].ToString();

          
            EmpReqRow["PropertyCodePC"] = EmpManager[0]["PropertyCodePC"].ToString();
          
            EmpReqRow["TerminalIdPcPos"] = EmpManager[0]["TerminalIdPcPos"].ToString();
            EmpReqRow["ComPortPcPos"] = EmpManager[0]["ComPortPcPos"].ToString();
            EmpReqRow["AcceptorIdPcPos"] = EmpManager[0]["AcceptorIdPcPos"].ToString();
            EmpReqRow["SerialNoPcPos"] = EmpManager[0]["SerialNoPcPos"].ToString();

            EmpReqRow["TerminalIdPcPos2"] = EmpManager[0]["TerminalIdPcPos2"].ToString();
            EmpReqRow["ComPortPcPos2"] = EmpManager[0]["ComPortPcPos2"].ToString();
            EmpReqRow["AcceptorIdPcPos2"] = EmpManager[0]["AcceptorIdPcPos2"].ToString();
            EmpReqRow["SerialNoPcPos2"] = EmpManager[0]["SerialNoPcPos2"].ToString();

            if (cmbSexId.Value != null)
                EmpReqRow["SexId"] = cmbSexId.Value;
            if (cmbMarId.Value != null)
                EmpReqRow["MarId"] = cmbMarId.Value;
            if (cmbPartId.Value != null)
                EmpReqRow["PartId"] = cmbPartId.Value;
            if (cmbRelId.Value != null)
                EmpReqRow["RelId"] = cmbRelId.Value;
            EmpReqRow["AgentId"] = EmpManager[0]["AgentId"].ToString();
            EmpReqRow["EmpStatus"] = EmpManager[0]["EmpStatus"].ToString();
            //EmpReqRow["ImgUrl"] = EmpManager[0]["ImgUrl"].ToString();
            EmpReqRow["IsConfirmed"] = 0;
            EmpReqRow["UserId"] = Utility.GetCurrentUser_UserId();
            EmpReqRow["ModifiedDate"] = DateTime.Now;
            EmployeeRequestManager.AddRow(EmpReqRow);
            if (EmployeeRequestManager.Save() <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            EmployeeRequestManager.DataTable.AcceptChanges();
            _EmpReId = Convert.ToInt32(EmployeeRequestManager[0]["EmpReId"]);

            EmpManager[0].BeginEdit();
            if (Session["EmpImg"] != null)
            {
                EmpManager[0]["ImgUrl"] = "~/image/Employee/Pic/" + "EmpImage" + _EmpReId.ToString() + Path.GetExtension(Session["EmpImg"].ToString());
            }
            if (Session["EmpSign"] != null)
            {
                EmpManager[0]["SignUrl"] = "~/image/Employee/Sign/" + "EmpSign" + _EmpReId.ToString() + Path.GetExtension(Session["EmpSign"].ToString());
            }
            EmpManager[0]["EmpCode"] = _EmpId;
            EmpManager[0].EndEdit();
            EmpManager.Save();

            EmployeeRequestManager[0].BeginEdit();
            if (Session["EmpImg"] != null)
            {
                EmployeeRequestManager[0]["ImgUrl"] = imgPic.ImageUrl = Session["EmpImg"].ToString();// "~/image/Employee/Pic/" + "EmpImage" + _EmpReId.ToString() + Path.GetExtension(Session["EmpImg"].ToString());
                                                                                                     //  imgPic.ImageUrl = "~/image/Employee/Pic/" + "EmpImage" + _EmpReId.ToString() + Path.GetExtension(Session["EmpImg"].ToString());
            }
            if (Session["EmpSign"] != null)
            {
                EmployeeRequestManager[0]["SignUrl"] = HpSign.NavigateUrl = Session["EmpSign"].ToString();// "~/image/Employee/Sign/" + "EmpSign" + _EmpReId.ToString() + Path.GetExtension(Session["EmpSign"].ToString());
                //HpSign.NavigateUrl = "~/image/Employee/Sign/" + "EmpSign" + _EmpReId.ToString() + Path.GetExtension(Session["EmpSign"].ToString());
            }
            EmployeeRequestManager[0].EndEdit();
            EmployeeRequestManager.Save();

            #region UserLog

            DataRow logRow = LogManager.NewRow();
            logRow.BeginEdit();
            logRow["UserName"] = "emp" + _EmpId;
            if (!string.IsNullOrEmpty(EmpManager[0]["IdNo"].ToString()))
                logRow["Password"] = FormsAuthentication.HashPasswordForStoringInConfigFile(EmpManager[0]["IdNo"].ToString(), "sha1");
            logRow["UltId"] = 4;
            logRow["MeId"] = _EmpId;
            logRow["Email"] = EmpManager[0]["Email"].ToString();
            logRow["UserId2"] = Utility.GetCurrentUser_UserId();
            logRow["IsValid"] = 1;
            logRow["ModifiedDate"] = DateTime.Now;
            logRow.EndEdit();
            LogManager.AddRow(logRow);
            if (LogManager.Save() <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            #endregion
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveEmployeeInfo;
            int WfStart = WorkFlowStateManager.StartWorkFlow(_EmpReId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), 0);
            if (WfStart <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            trans.EndSave();

            _PageMode = "Edit";
            RoundPanelEmlpoyee.HeaderText = "ویرایش";

            //imgPic.ImageUrl = EmpManager[0]["ImgUrl"].ToString() + "?ImageType=" + DateTime.Now.Ticks.ToString();
            //HpSign.NavigateUrl = EmpManager[0]["SignUrl"].ToString() + "?ImageType=" + DateTime.Now.Ticks.ToString();

            txtEmpCode.Text = _EmpId.ToString();

            this.DivReport.Visible = true;
            this.LabelWarning.Text = "  ذخیره با نام کاربری " + "emp" + _EmpId + "و رمز عبور  " + EmpManager[0]["IdNo"].ToString() + " انجام شد";


        }
        catch (Exception err)
        {
            trans.CancelSave();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                if (se.Number == 2627)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "کد پرسنلی یا کدملی تکراری می باشد";
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
        //try
        //{
        //    if (Session["EmpImg"] != null)
        //    {

        //        try
        //        {
        //            string ImgSoource = Server.MapPath("~/image/Employee/Pic/") + Path.GetFileName(Session["EmpImg"].ToString());
        //            string ImgTarget = Server.MapPath("~/image/Employee/Pic/") + "EmpImage" + _EmpReId.ToString() + Path.GetExtension(Session["EmpImg"].ToString());
        //            System.IO.File.Copy(ImgSoource, ImgTarget, true);
        //            imgPic.ImageUrl = ImgTarget;
        //            Session["EmpImg"] = null;
        //        }
        //        catch (Exception err)
        //        {
        //            Utility.SaveWebsiteError(err);
        //        }
        //    }

        //    if (Session["EmpSign"] != null)
        //    {

        //        try
        //        {
        //            string ImgSoource = Server.MapPath("~/image/Employee/Sign/") + Path.GetFileName(Session["EmpSign"].ToString());
        //            string ImgTarget = Server.MapPath("~/image/Employee/Sign/") + "EmpSign" + _EmpReId.ToString() + Path.GetExtension(Session["EmpSign"].ToString());
        //            System.IO.File.Copy(ImgSoource, ImgTarget, true);
        //            HpSign.NavigateUrl = ImgTarget;
        //            Session["EmpSign"] = null;
        //        }
        //        catch (Exception err)
        //        {
        //            Utility.SaveWebsiteError(err);
        //        }
        //    }
        //}
        //catch (Exception err)
        //{
        //    Utility.SaveWebsiteError(err);
        //}

    }

    private void EditEmloyee(int EmpReId)
    {
        string Address = txtAddress.Text;
        string BirthDate = txtBirthDate.Text;
        string BirthPlace = txtBirthPlace.Text;
        string Description = txtDescription.Text;
        string Email = txtEmail.Text;
        string EmpCode = txtEmpCode.Text;
        string FatherName = txtFatherName.Text;
        string FirstName = txtFirstName.Text;
        string FirstNameEn = txtFirstNameEn.Text;
        string IdNo = txtIdNo.Text;
        string LastName = txtLastName.Text;
        string LastNameEn = txtLastNameEn.Text;
        string MobileNo = txtMobileNo.Text;
        string Nationality = txtNationality.Text;
        string SSN = txtSSN.Text;
        string Tel = txtTel.Text;
        string WebSite = txtWebSite.Text;
       
        string PropertyCodePC = txtPropertyCodePC.Text;
     
        string TerminalIdPcPos = txtTerminalIdPcPos.Text;
        string ComPortPcPos = txtComPortPcPos.Text;
        string AcceptorIdPcPos = txtAcceptorIdPcPos.Text;
        string SerialNoPcPos = txtSerialNoPcPos.Text;

        string TerminalIdPcPos2 = txtTerminalIdPcPos2.Text;
        string ComPortPcPos2 = txtComPortPcPos2.Text;
        string AcceptorIdPcPos2 = txtAcceptorIdPcPos2.Text;
        string SerialNoPcPos2 = txtSerialNoPcPos2.Text;

        TSP.DataManager.EmployeeRequestManager EmployeeRequestManager = new TSP.DataManager.EmployeeRequestManager();

        EmployeeRequestManager.FindByCode(EmpReId);

        if (EmployeeRequestManager.Count != 1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "اطلاعات توسط کاربر دیگری تغییر یافته است";
            return;
        }
        try
        {
            EmployeeRequestManager[0].BeginEdit();
            EmployeeRequestManager[0]["Address"] = Address;
            EmployeeRequestManager[0]["BirthDate"] = BirthDate;
            EmployeeRequestManager[0]["BirthPlace"] = BirthPlace;
            EmployeeRequestManager[0]["Description"] = Description;
            EmployeeRequestManager[0]["Email"] = Email;
            EmployeeRequestManager[0]["FatherName"] = FatherName;
            EmployeeRequestManager[0]["FirstName"] = FirstName;
            EmployeeRequestManager[0]["FirstNameEn"] = FirstNameEn;
            EmployeeRequestManager[0]["IdNo"] = IdNo;
            EmployeeRequestManager[0]["LastName"] = LastName;
            EmployeeRequestManager[0]["LastNameEn"] = LastNameEn;
            EmployeeRequestManager[0]["MobileNo"] = MobileNo;
            EmployeeRequestManager[0]["Nationality"] = Nationality;
            EmployeeRequestManager[0]["SSN"] = SSN;
            EmployeeRequestManager[0]["Tel"] = Tel;
            EmployeeRequestManager[0]["WebSite"] = WebSite;

          
            EmployeeRequestManager[0]["PropertyCodePC"] = PropertyCodePC;
       
            EmployeeRequestManager[0]["TerminalIdPcPos"] = TerminalIdPcPos;
            EmployeeRequestManager[0]["ComPortPcPos"] = ComPortPcPos;
            EmployeeRequestManager[0]["AcceptorIdPcPos"] = AcceptorIdPcPos;
            EmployeeRequestManager[0]["SerialNoPcPos"] = SerialNoPcPos;

            EmployeeRequestManager[0]["TerminalIdPcPos2"] = TerminalIdPcPos2;
            EmployeeRequestManager[0]["ComPortPcPos2"] = ComPortPcPos2;
            EmployeeRequestManager[0]["AcceptorIdPcPos2"] = AcceptorIdPcPos2;
            EmployeeRequestManager[0]["SerialNoPcPos2"] = SerialNoPcPos2;

            if (cmbPartId.Value != null)
                EmployeeRequestManager[0]["PartId"] = cmbPartId.Value;
            if (cmbSexId.Value != null)
                EmployeeRequestManager[0]["SexId"] = cmbSexId.Value;
            else
                EmployeeRequestManager[0]["SexId"] = DBNull.Value;
            if (cmbMarId.Value != null)
                EmployeeRequestManager[0]["MarId"] = cmbMarId.Value;
            else
                EmployeeRequestManager[0]["MarId"] = DBNull.Value;
            if (cmbRelId.Value != null)
                EmployeeRequestManager[0]["RelId"] = cmbRelId.Value;
            else
                EmployeeRequestManager[0]["RelId"] = DBNull.Value;
            if (cmbAgent.Value != null)
                EmployeeRequestManager[0]["AgentId"] = cmbAgent.Value;
            else
                EmployeeRequestManager[0]["AgentId"] = DBNull.Value;
            EmployeeRequestManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            EmployeeRequestManager[0]["ModifiedDate"] = DateTime.Now;
            if (CmbStatus.Value != null)
                EmployeeRequestManager[0]["EmpStatus"] = CmbStatus.Value;

            if (Session["EmpImg"] != null)
            {

                EmployeeRequestManager[0]["ImgUrl"] = Session["EmpImg"].ToString();// "~/image/Employee/Pic/" + EmpReId.ToString() + Path.GetExtension(Session["EmpImg"].ToString());
                imgPic.ImageUrl = Session["EmpImg"].ToString();
            }
            if (Session["EmpSign"] != null)
            {
                EmployeeRequestManager[0]["SignUrl"] = Session["EmpSign"].ToString();// "~/image/Employee/Sign/" + EmpReId.ToString() + Path.GetExtension(Session["EmpSign"].ToString());
                HpSign.NavigateUrl = Session["EmpSign"].ToString();
                HpSign.ClientVisible = true;
            }

            //imgPic.ImageUrl = EmployeeRequestManager[0]["ImgUrl"].ToString() + "?ImageType=" + DateTime.Now.Ticks.ToString();
            //HpSign.NavigateUrl = EmployeeRequestManager[0]["SignUrl"].ToString() + "?ImageType=" + DateTime.Now.Ticks.ToString();

            EmployeeRequestManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            EmployeeRequestManager[0]["ModifiedDate"] = DateTime.Now;
            EmployeeRequestManager[0].EndEdit();
            if (EmployeeRequestManager.Save() != 1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            this.DivReport.Visible = true;
            this.LabelWarning.Text = " ذخیره انجام شد";
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

    private void InsertEmployeeChangeRequest(int EmpId)
    {
        string Address = txtAddress.Text;
        string BirthDate = txtBirthDate.Text;
        string BirthPlace = txtBirthPlace.Text;
        string Description = txtDescription.Text;
        string Email = txtEmail.Text;
        string EmpCode = txtEmpCode.Text;
        string FatherName = txtFatherName.Text;
        string FirstName = txtFirstName.Text;
        string FirstNameEn = txtFirstNameEn.Text;
        string IdNo = txtIdNo.Text;
        string LastName = txtLastName.Text;
        string LastNameEn = txtLastNameEn.Text;
        string MobileNo = txtMobileNo.Text;
        string Nationality = txtNationality.Text;
        string SSN = txtSSN.Text;
        string WebSite = txtWebSite.Text;
       
        string PropertyCodePC = txtPropertyCodePC.Text;
       
        string TerminalIdPcPos = txtTerminalIdPcPos.Text;
        string ComPortPcPos = txtComPortPcPos.Text;
        string AcceptorIdPcPos = txtAcceptorIdPcPos.Text;
        string SerialNoPcPos = txtSerialNoPcPos.Text;

        string TerminalIdPcPos2 = txtTerminalIdPcPos2.Text;
        string ComPortPcPos2 = txtComPortPcPos2.Text;
        string AcceptorIdPcPos2 = txtAcceptorIdPcPos2.Text;
        string SerialNoPcPos2 = txtSerialNoPcPos2.Text;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.EmployeeRequestManager EmployeeRequestManager = new TSP.DataManager.EmployeeRequestManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.Automation.LetterRelatedTablesManager LetterRelatedTablesManager = new TSP.DataManager.Automation.LetterRelatedTablesManager();

        trans.Add(WorkFlowStateManager);
        trans.Add(EmployeeRequestManager);
        trans.Add(LetterRelatedTablesManager);

        try
        {

            int CurrentNmcId = FindNmcId();
            if (CurrentNmcId == -1)
                return;
            trans.BeginSave();
            DataRow drReq = EmployeeRequestManager.NewRow();
            drReq["EmpId"] = EmpId;
            drReq["Type"] = (int)TSP.DataManager.EmployeeRequestType.ChangeRequest;
            drReq["Address"] = Address;
            drReq["BirthDate"] = BirthDate;
            drReq["BirthPlace"] = BirthPlace;
            drReq["CreateDate"] = Utility.GetDateOfToday();
            drReq["Description"] = Description;
            drReq["Email"] = Email;
            drReq["EmpCode"] = EmpId;
            drReq["FatherName"] = FatherName;
            drReq["FirstName"] = FirstName;
            drReq["FirstNameEn"] = FirstNameEn;
            drReq["IdNo"] = IdNo;
            drReq["LastName"] = LastName;
            drReq["LastNameEn"] = LastNameEn;
            drReq["MobileNo"] = MobileNo;
            drReq["Nationality"] = Nationality;
            drReq["SSN"] = SSN;
            drReq["Tel"] = txtTel.Text;
            drReq["WebSite"] = WebSite;
      
            drReq["PropertyCodePC"] = PropertyCodePC;
           
           
            drReq["TerminalIdPcPos"] = TerminalIdPcPos;
            drReq["ComPortPcPos"] = ComPortPcPos;
            drReq["AcceptorIdPcPos"] = AcceptorIdPcPos;
            drReq["SerialNoPcPos"] = SerialNoPcPos;

            drReq["TerminalIdPcPos2"] = TerminalIdPcPos2;
            drReq["ComPortPcPos2"] = ComPortPcPos2;
            drReq["AcceptorIdPcPos2"] = AcceptorIdPcPos2;
            drReq["SerialNoPcPos2"] = SerialNoPcPos2;

            if (cmbSexId.Value != null)
                drReq["SexId"] = cmbSexId.Value;
            else
                drReq["SexId"] = DBNull.Value;
            if (cmbMarId.Value != null)
                drReq["MarId"] = cmbMarId.Value;
            else
                drReq["MarId"] = DBNull.Value;
            //  drReq["PartId"] = DBNull.Value;
            if (cmbRelId.Value != null)
                drReq["RelId"] = cmbRelId.Value;
            else
                drReq["RelId"] = DBNull.Value;
            if (cmbAgent.Value != null)
                drReq["AgentId"] = cmbAgent.Value;
            else
                drReq["AgentId"] = DBNull.Value;

            if (cmbPartId.Value != null)
                drReq["PartId"] = cmbPartId.Value;
            else
                drReq["PartId"] = DBNull.Value;
            if (CmbStatus.Value != null)
                drReq["EmpStatus"] = CmbStatus.Value;

            drReq["UserId"] = Utility.GetCurrentUser_UserId();
            drReq["ModifiedDate"] = DateTime.Now;
            //  drReq["ImgUrl"] = imgPic.ImageUrl;
            // drReq["SignUrl"] = HpSign.NavigateUrl;

            EmployeeRequestManager.AddRow(drReq);

            if (EmployeeRequestManager.Save() <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            EmployeeRequestManager.DataTable.AcceptChanges();
            _EmpReId = Convert.ToInt32(EmployeeRequestManager[0]["EmpReId"]);
            EmployeeRequestManager[0].BeginEdit();
            if (Session["EmpImg"] != null)
            {
                EmployeeRequestManager[0]["ImgUrl"] = imgPic.ImageUrl = Session["EmpImg"].ToString();// "~/image/Employee/Pic/" + "EmpImage" + _EmpReId.ToString() + Path.GetExtension(Session["EmpImg"].ToString());
            }
            if (Session["EmpSign"] != null)
            {
                EmployeeRequestManager[0]["SignUrl"] = HpSign.NavigateUrl = Session["EmpSign"].ToString(); //"~/image/Employee/Sign/" + "EmpSign" + _EmpReId.ToString() + Path.GetExtension(Session["EmpSign"].ToString());
            }
            EmployeeRequestManager[0].EndEdit();
            EmployeeRequestManager.Save();

            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveEmployeeInfo;
            int WfStart = WorkFlowStateManager.StartWorkFlow(_EmpReId, TaskCode, CurrentNmcId, Utility.GetCurrentUser_UserId(), 0);
            if (WfStart <= 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                trans.CancelSave();
                return;
            }
            trans.EndSave();
            _EmpReId = Convert.ToInt32(EmployeeRequestManager[0]["EmpReId"]);
            _PageMode = "Edit";
            RoundPanelEmlpoyee.HeaderText = "ویرایش";

            //imgPic.ImageUrl = EmployeeRequestManager[0]["ImgUrl"].ToString() + "?ImageType=" + DateTime.Now.Ticks.ToString();
            //HpSign.NavigateUrl = EmployeeRequestManager[0]["SignUrl"].ToString() + "?ImageType=" + DateTime.Now.Ticks.ToString();

            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد.";

        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                if (se.Number == 2627)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "کد پرسنلی یا کد ملی تکراری می باشد";
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

    private void SetKeys()
    {
        _EmpReId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["EmpReId"])));
        _EmpId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["EmpId"])));
        _PageMode = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PageMode"]));

        if (string.IsNullOrEmpty(_PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        SetMode();
        CheckWorkFlowPermission();
    }

    private void SetMode()
    {

        switch (_PageMode)
        {
            case "View":
                SetViewModeKeys();
                break;

            case "New":
                SetNewModeKeys();
                break;

            case "Edit":
                SetEditModeKeys();
                break;

            case "Change":
                SetRequestModeKeys();
                break;
        }
    }

    private void SetViewModeKeys()
    {
        TSP.DataManager.Permission per = TSP.DataManager.EmployeeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        if (per.CanNew)
        {
            btnNew.Enabled = true;
            btnNew2.Enabled = true;
        }
        if (per.CanEdit)
        {
            btnEdit.Enabled = true;
            btnEdit2.Enabled = true;
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;

        //Set Textboxe's & comboboxe's Enabled
        SetEnabled(false);


        if (_EmpReId == -1)
        {
            FillForm(_EmpId);
            ASPxLabelStatus.Visible = false;
            CmbStatus.Visible = false;
        }
        else
        {
            FillFormByRequest(_EmpId, _EmpReId);
        }
        RoundPanelEmlpoyee.HeaderText = "مشاهده";
    }

    private void SetNewModeKeys()
    {
        //Set Button's Enable
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        _EmpReId = -1;
        _EmpId = -1;
        _PageMode = "New";
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

        ClearForm();
        ASPxLabelStatus.Visible = false;
        CmbStatus.Visible = false;

        RoundPanelEmlpoyee.HeaderText = "جدید";
    }

    private void SetEditModeKeys()
    {
        //Check UserPermission
        TSP.DataManager.Permission per = TSP.DataManager.EmployeeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        //ُSet Button's Enable

        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        btnSave.Enabled = per.CanEdit;
        btnSave2.Enabled = per.CanEdit;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        EnableControls();
        _PageMode = "Edit";
        FillFormByRequest(_EmpId, _EmpReId);
        RoundPanelEmlpoyee.HeaderText = "ویرایش";
    }

    private void SetRequestModeKeys()
    {
        ASPxLabelStatus.Visible = true;
        CmbStatus.Visible = true;
        TSP.DataManager.Permission perReq = TSP.DataManager.EmployeeRequestManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission per = TSP.DataManager.EmployeeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnSave.Enabled = perReq.CanNew;
        btnSave2.Enabled = perReq.CanNew;
        if (per.CanNew)
        {
            btnNew.Enabled = true;
            btnNew2.Enabled = true;
        }
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnNew"] = btnNew.Enabled;

        SetEnabled(true);
        FillForm(_EmpId);
        RoundPanelEmlpoyee.HeaderText = "درخواست تغییرات";
    }

    private void ClearForm()
    {
        txtNcName.Text = "";
        txtAddress.Text = "";
        txtBirthDate.Text = "";
        txtBirthPlace.Text = "";
        txtDescription.Text = "";
        txtEmail.Text = "";
        txtEmpCode.Text = "";
        txtFatherName.Text = "";
        txtFirstName.Text = "";
        txtFirstNameEn.Text = "";
        txtIdNo.Text = "";
        txtLastName.Text = "";
        txtLastNameEn.Text = "";
        txtMobileNo.Text = "";
        txtNationality.Text = "";
        txtSSN.Text = "";
        txtTel.Text = "";
        txtWebSite.Text = "";
      
        txtPropertyCodePC.Text = "";
       
        txtTerminalIdPcPos.Text = "";
        txtComPortPcPos.Text = "";
        txtAcceptorIdPcPos.Text = "";
        txtSerialNoPcPos.Text = "";

        txtTerminalIdPcPos2.Text = "";
        txtComPortPcPos2.Text = "";
        txtAcceptorIdPcPos2.Text = "";
        txtSerialNoPcPos2.Text = "";

        imgPic.ImageUrl = "../../Images/person.gif";

        cmbMarId.SelectedIndex = -1;
        cmbPartId.SelectedIndex = -1;
        cmbRelId.SelectedIndex = -1;
        cmbSexId.SelectedIndex = -1;
        cmbAgent.SelectedIndex = -1;

        if (Utility.GetCurrentUser_AgentId() != Utility.GetMainAgentId())
        {
            cmbAgent.DataBind();
            this.cmbAgent.Enabled = false;
            this.cmbAgent.Value = Utility.GetCurrentUser_AgentId();
            HiddenFieldEmployee.Set("MainAgent", 0);
        }
        else
            HiddenFieldEmployee.Set("MainAgent", 1);
    }

    private void EnableControls()
    {
        txtAddress.Enabled = true;

        txtBirthDate.Enabled = true;
        txtBirthPlace.Enabled = true;
        txtDescription.Enabled = true;
        txtEmail.Enabled = true;
        txtEmpCode.Enabled = true;
        txtFatherName.Enabled = true;
        txtFirstName.Enabled = true;
        txtFirstNameEn.Enabled = true;
        txtIdNo.Enabled = true;
        txtLastName.Enabled = true;
        txtLastNameEn.Enabled = true;
        txtMobileNo.Enabled = true;
        txtNationality.Enabled = true;
        txtSSN.Enabled = true;
        txtTel.Enabled = true;
        txtWebSite.Enabled = true;
      
        txtPropertyCodePC.Enabled = true;
   
        txtTerminalIdPcPos.Enabled = true;
        txtComPortPcPos.Enabled = true;
        txtAcceptorIdPcPos.Enabled = true;
        txtSerialNoPcPos.Enabled = true;

        txtTerminalIdPcPos2.Enabled = true;
        txtComPortPcPos2.Enabled = true;
        txtAcceptorIdPcPos2.Enabled = true;
        txtSerialNoPcPos2.Enabled = true;

        cmbMarId.Enabled = true;
        cmbPartId.Enabled = true;
        cmbRelId.Enabled = true;
        cmbSexId.Enabled = true;
        flpImg.Enabled = true;
        flpSign.Enabled = true;

        if (Utility.GetCurrentUser_AgentId() == Utility.GetMainAgentId())
            cmbAgent.Enabled = true;
        else
            cmbAgent.Enabled = false;

    }

    private void SetEnabled(Boolean Enabled)
    {
        txtAddress.Enabled =
        txtBirthPlace.Enabled =
        txtDescription.Enabled =
        txtEmail.Enabled =
        txtEmpCode.Enabled =
        txtFatherName.Enabled =
        txtFirstName.Enabled =
        txtFirstNameEn.Enabled =
        txtIdNo.Enabled =
        txtLastName.Enabled =
        txtLastNameEn.Enabled =
        txtMobileNo.Enabled =
        txtNationality.Enabled =
        txtSSN.Enabled =
        txtTel.Enabled =
        txtWebSite.Enabled =
        
        txtPropertyCodePC.Enabled =
      
        txtTerminalIdPcPos.Enabled =
        txtComPortPcPos.Enabled =
        txtAcceptorIdPcPos.Enabled =
        txtSerialNoPcPos.Enabled =
        txtTerminalIdPcPos2.Enabled =
        txtComPortPcPos2.Enabled =
        txtAcceptorIdPcPos2.Enabled =
        txtSerialNoPcPos2.Enabled =
        txtBirthDate.Enabled =
        flpImg.Enabled =
        flpSign.Enabled =

        cmbMarId.Enabled =
        cmbPartId.Enabled =
        cmbRelId.Enabled =
        cmbSexId.Enabled =
        cmbAgent.Enabled =
        CmbStatus.Enabled = Enabled;
    }

    private void FillForm(int EmpId)
    {
        TSP.DataManager.EmployeeManager EmpManager = new TSP.DataManager.EmployeeManager();
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();
        EmpManager.FindByCode(EmpId);
        if (EmpManager.Count != 1)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }
        NezamMemberChartManager.FindByEmpId(EmpId, (int)TSP.DataManager.UserType.Employee);
        string NcNames = "";
        if (NezamMemberChartManager.Count > 0)
        {
            for (int i = 0; i < NezamMemberChartManager.Count; i++)
            {
                if (!Convert.ToBoolean(NezamMemberChartManager[i]["InActive"]))
                    NcNames += NezamMemberChartManager[i]["NcName"].ToString() + " ; ";
            }
            if (!Utility.IsDBNullOrNullValue(NcNames))
            {
                NcNames = NcNames.Remove(NcNames.Length - 1);
                txtNcName.Text = NcNames;
            }

        }

        txtAddress.Text = EmpManager[0]["Address"].ToString();
        txtBirthDate.Text = EmpManager[0]["BirthDate"].ToString();
        txtBirthPlace.Text = EmpManager[0]["BirthPlace"].ToString();
        txtDescription.Text = EmpManager[0]["Description"].ToString();
        txtEmail.Text = EmpManager[0]["Email"].ToString();
        txtEmpCode.Text = EmpManager[0]["EmpCode"].ToString();
        txtFatherName.Text = EmpManager[0]["FatherName"].ToString();
        txtFirstName.Text = EmpManager[0]["FirstName"].ToString();
        txtFirstNameEn.Text = EmpManager[0]["FirstNameEn"].ToString();
        txtIdNo.Text = EmpManager[0]["IdNo"].ToString();
        txtLastName.Text = EmpManager[0]["LastName"].ToString();
        txtLastNameEn.Text = EmpManager[0]["LastNameEn"].ToString();
        txtMobileNo.Text = EmpManager[0]["MobileNo"].ToString();
        txtNationality.Text = EmpManager[0]["Nationality"].ToString();
        txtSSN.Text = EmpManager[0]["SSN"].ToString();
        txtTel.Text = EmpManager[0]["Tel"].ToString();
        txtWebSite.Text = EmpManager[0]["WebSite"].ToString();
      
        if (!Utility.IsDBNullOrNullValue(EmpManager[0]["PropertyCodePC"]))
            txtPropertyCodePC.Text = EmpManager[0]["PropertyCodePC"].ToString();
     
        
        if (!Utility.IsDBNullOrNullValue(EmpManager[0]["TerminalIdPcPos"]))
            txtTerminalIdPcPos.Text = EmpManager[0]["TerminalIdPcPos"].ToString();
        if (!Utility.IsDBNullOrNullValue(EmpManager[0]["ComPortPcPos"]))
            txtComPortPcPos.Text = EmpManager[0]["ComPortPcPos"].ToString();
        if (!Utility.IsDBNullOrNullValue(EmpManager[0]["AcceptorIdPcPos"]))
            txtAcceptorIdPcPos.Text = EmpManager[0]["AcceptorIdPcPos"].ToString();
        if (!Utility.IsDBNullOrNullValue(EmpManager[0]["SerialNoPcPos"]))
            txtSerialNoPcPos.Text = EmpManager[0]["SerialNoPcPos"].ToString();

        if (!Utility.IsDBNullOrNullValue(EmpManager[0]["TerminalIdPcPos2"]))
            txtTerminalIdPcPos2.Text = EmpManager[0]["TerminalIdPcPos2"].ToString();
        if (!Utility.IsDBNullOrNullValue(EmpManager[0]["ComPortPcPos2"]))
            txtComPortPcPos2.Text = EmpManager[0]["ComPortPcPos2"].ToString();
        if (!Utility.IsDBNullOrNullValue(EmpManager[0]["AcceptorIdPcPos2"]))
            txtAcceptorIdPcPos2.Text = EmpManager[0]["AcceptorIdPcPos2"].ToString();
        if (!Utility.IsDBNullOrNullValue(EmpManager[0]["SerialNoPcPos2"]))
            txtSerialNoPcPos2.Text = EmpManager[0]["SerialNoPcPos2"].ToString();

        if (!Utility.IsDBNullOrNullValue(EmpManager[0]["ImgUrl"]))
        {
            imgPic.ImageUrl = EmpManager[0]["ImgUrl"].ToString();
            HDFlpMember["name"] = 1;
        }
        else
            imgPic.ImageUrl = "~/images/person.gif";

        if (!Utility.IsDBNullOrNullValue(EmpManager[0]["SignUrl"]))
        {
            HpSign.ClientVisible = true;
            HpSign.NavigateUrl = EmpManager[0]["SignUrl"].ToString();
        }

        cmbPartId.DataBind();
        cmbAgent.DataBind();
        cmbMarId.DataBind();
        cmbRelId.DataBind();
        cmbSexId.DataBind();

        cmbMarId.SelectedIndex = cmbMarId.Items.IndexOfValue(EmpManager[0]["MarId"].ToString());
        cmbPartId.SelectedIndex = cmbPartId.Items.IndexOfValue(EmpManager[0]["PartId"].ToString());
        cmbRelId.SelectedIndex = cmbRelId.Items.IndexOfValue(EmpManager[0]["RelId"].ToString());
        cmbSexId.SelectedIndex = cmbSexId.Items.IndexOfValue(EmpManager[0]["SexId"].ToString());
        cmbAgent.SelectedIndex = cmbAgent.Items.FindByValue(EmpManager[0]["AgentId"]).Index;

        if (!Utility.IsDBNullOrNullValue(EmpManager[0]["EmpStatus"]))
        {
            CmbStatus.DataBind();
            CmbStatus.SelectedIndex = CmbStatus.Items.IndexOfValue(EmpManager[0]["EmpStatus"].ToString());
        }

    }

    private void FillFormByRequest(int EmpId, int EmpReId)
    {
        TSP.DataManager.EmployeeRequestManager EmployeeRequestManager = new TSP.DataManager.EmployeeRequestManager();
        TSP.DataManager.NezamMemberChartManager NezamMemberChartManager = new TSP.DataManager.NezamMemberChartManager();

        EmployeeRequestManager.FindByCode(EmpReId);
        if (EmployeeRequestManager.Count != 1)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;

        }
        NezamMemberChartManager.FindByEmpId(EmpId, (int)TSP.DataManager.UserType.Employee);
        string NcNames = "";
        if (NezamMemberChartManager.Count > 0)
        {
            for (int i = 0; i < NezamMemberChartManager.Count; i++)
            {
                if (!Convert.ToBoolean(NezamMemberChartManager[i]["InActive"]))
                    NcNames += NezamMemberChartManager[i]["NcName"].ToString() + " ; ";
            }
            if (NcNames.Length > 1)
                NcNames = NcNames.Remove(NcNames.Length - 1);
            txtNcName.Text = NcNames;
        }
        int ReqType = int.Parse(EmployeeRequestManager[0]["Type"].ToString());
        if (ReqType == (int)TSP.DataManager.EmployeeRequestType.SaveInfo)
        {
            ASPxLabelStatus.Visible = false;
            CmbStatus.Visible = false;
        }
        else
        {
            ASPxLabelStatus.Visible = true;
            CmbStatus.Visible = true;
            if (!Utility.IsDBNullOrNullValue(EmployeeRequestManager[0]["EmpStatus"]))
            {
                CmbStatus.DataBind();
                CmbStatus.SelectedIndex = CmbStatus.Items.IndexOfValue(EmployeeRequestManager[0]["EmpStatus"].ToString());

            }

        }
        txtAddress.Text = EmployeeRequestManager[0]["Address"].ToString();
        txtBirthDate.Text = EmployeeRequestManager[0]["BirthDate"].ToString();
        txtBirthPlace.Text = EmployeeRequestManager[0]["BirthPlace"].ToString();
        txtDescription.Text = EmployeeRequestManager[0]["Description"].ToString();
        txtEmail.Text = EmployeeRequestManager[0]["Email"].ToString();
        txtEmpCode.Text = EmployeeRequestManager[0]["EmpCode"].ToString();
        txtFatherName.Text = EmployeeRequestManager[0]["FatherName"].ToString();
        txtFirstName.Text = EmployeeRequestManager[0]["FirstName"].ToString();
        txtFirstNameEn.Text = EmployeeRequestManager[0]["FirstNameEn"].ToString();
        txtIdNo.Text = EmployeeRequestManager[0]["IdNo"].ToString();
        txtLastName.Text = EmployeeRequestManager[0]["LastName"].ToString();
        txtLastNameEn.Text = EmployeeRequestManager[0]["LastNameEn"].ToString();
        txtMobileNo.Text = EmployeeRequestManager[0]["MobileNo"].ToString();
        txtNationality.Text = EmployeeRequestManager[0]["Nationality"].ToString();
        txtSSN.Text = EmployeeRequestManager[0]["SSN"].ToString();
        txtTel.Text = EmployeeRequestManager[0]["Tel"].ToString();
        txtWebSite.Text = EmployeeRequestManager[0]["WebSite"].ToString();

        if (!Utility.IsDBNullOrNullValue(EmployeeRequestManager[0]["PropertyCodePC"]))
            txtPropertyCodePC.Text = EmployeeRequestManager[0]["PropertyCodePC"].ToString();
       
        if (!Utility.IsDBNullOrNullValue(EmployeeRequestManager[0]["TerminalIdPcPos"]))
            txtTerminalIdPcPos.Text = EmployeeRequestManager[0]["TerminalIdPcPos"].ToString();
        if (!Utility.IsDBNullOrNullValue(EmployeeRequestManager[0]["ComPortPcPos"]))
            txtComPortPcPos.Text = EmployeeRequestManager[0]["ComPortPcPos"].ToString();
        if (!Utility.IsDBNullOrNullValue(EmployeeRequestManager[0]["AcceptorIdPcPos"]))
            txtAcceptorIdPcPos.Text = EmployeeRequestManager[0]["AcceptorIdPcPos"].ToString();
        if (!Utility.IsDBNullOrNullValue(EmployeeRequestManager[0]["SerialNoPcPos"]))
            txtSerialNoPcPos.Text = EmployeeRequestManager[0]["SerialNoPcPos"].ToString();

        if (!Utility.IsDBNullOrNullValue(EmployeeRequestManager[0]["TerminalIdPcPos2"]))
            txtTerminalIdPcPos2.Text = EmployeeRequestManager[0]["TerminalIdPcPos2"].ToString();
        if (!Utility.IsDBNullOrNullValue(EmployeeRequestManager[0]["ComPortPcPos2"]))
            txtComPortPcPos2.Text = EmployeeRequestManager[0]["ComPortPcPos2"].ToString();
        if (!Utility.IsDBNullOrNullValue(EmployeeRequestManager[0]["AcceptorIdPcPos2"]))
            txtAcceptorIdPcPos2.Text = EmployeeRequestManager[0]["AcceptorIdPcPos2"].ToString();
        if (!Utility.IsDBNullOrNullValue(EmployeeRequestManager[0]["SerialNoPcPos2"]))
            txtSerialNoPcPos2.Text = EmployeeRequestManager[0]["SerialNoPcPos2"].ToString();

        if (!Utility.IsDBNullOrNullValue(EmployeeRequestManager[0]["ImgUrl"]))
        {
            imgPic.ImageUrl = EmployeeRequestManager[0]["ImgUrl"].ToString();
            HDFlpMember["name"] = 1;
        }
        else
            imgPic.ImageUrl = "~/images/person.gif";

        if (!Utility.IsDBNullOrNullValue(EmployeeRequestManager[0]["SignUrl"]))
        {
            HpSign.ClientVisible = true;
            HpSign.NavigateUrl = EmployeeRequestManager[0]["SignUrl"].ToString();
        }

        cmbAgent.DataBind();
        cmbMarId.DataBind();
        cmbRelId.DataBind();
        cmbSexId.DataBind();
        cmbPartId.DataBind();

        cmbMarId.SelectedIndex = cmbMarId.Items.IndexOfValue(EmployeeRequestManager[0]["MarId"].ToString());
        cmbRelId.SelectedIndex = cmbRelId.Items.IndexOfValue(EmployeeRequestManager[0]["RelId"].ToString());
        cmbSexId.SelectedIndex = cmbSexId.Items.IndexOfValue(EmployeeRequestManager[0]["SexId"].ToString());
        cmbAgent.SelectedIndex = cmbAgent.Items.FindByValue(EmployeeRequestManager[0]["AgentId"]).Index;
        cmbPartId.SelectedIndex = cmbPartId.Items.IndexOfValue(EmployeeRequestManager[0]["PartId"].ToString());

    }

    //private int FindNmcId()
    //{
    //    int UserId = Utility.GetCurrentUser_UserId();
    //    TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
    //    TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

    //    int NmcId = -1;

    //    NmcId = NezamChartManager.FindNmcId(UserId, LoginManager);
    //    if (NmcId > 0)
    //    {
    //        return NmcId;
    //    }
    //    else
    //    {
    //        DivReport.Visible = true;
    //        LabelWarning.Text = "کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.";
    //        return (-1);
    //    }
    //}  

    private int FindNmcId()
    {
        int TaskId = -1;
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.SaveEmployeeInfo);
        if (WorkFlowTaskManager.Count != 1)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return -1;
        }
        TaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
        int UserId = Utility.GetCurrentUser_UserId();
        TSP.DataManager.NezamChartManager NezamChartManager = new TSP.DataManager.NezamChartManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        int NmcId = -1;
        NmcId = NezamChartManager.FindNmcId(UserId, TaskId, LoginManager);
        if (NmcId > 0)
        {
            return NmcId;
        }
        else
        {
            SetMessage("کاربری با مشخصات جاری در چارت سازمانی وجود ندارد.");
            return (-1);
        }
    }


    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveEmployeeInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.EmployeeRequest;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());

                    if (CurrentTaskCode == TaskCode)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            int FirstUserId = int.Parse(dtWorkFlowState.Rows[0]["UserId"].ToString());
                            if (FirstTaskCode == TaskCode)
                            {
                                //if (FirstUserId ==Utility.GetCurrentUser_UserId())
                                //{
                                int Permission = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, TableId, TaskCode, Utility.GetCurrentUser_UserId());
                                if (Permission > 0)
                                    return true;
                                else
                                    return false;
                                //}
                                //else
                                //{
                                //    return false;
                                //}
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void CheckWorkFlowPermission()
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveEmployeeInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
        }
        if (TaskOrder != 0)
        {
            CheckWorkFlowPermissionForSave(_PageMode);
            if (_PageMode != "New" && _PageMode != "Change")
                CheckWorkFlowPermissionForEdit(_PageMode);
        }
    }

    private void CheckWorkFlowPermissionForSave(string PageMode)
    {

        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();

        int SaveWorkCode = (int)TSP.DataManager.WorkFlowTask.SaveEmployeeInfo;
        int Permission = -1;
        int TableType = (int)TSP.DataManager.TableCodes.EmployeeRequest;
        Permission = TaskDoerManager.CheckWorkFlowPermissionForSaveInfo(TableType, SaveWorkCode, Utility.GetCurrentUser_UserId());
        if (Permission > 0)
        {
            btnNew.Enabled = true;
            btnNew2.Enabled = true;
            switch (PageMode)
            {
                case "New":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
                case "Change":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
                case "View":
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
            }
        }
        else
        {
            btnNew.Enabled = false;
            btnNew2.Enabled = false;
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "شما سطح دسترسی جهت ثبت اطلاعات کارمند را ندارید.";
        }
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnNew"] = btnEdit.Enabled;

    }

    private void CheckWorkFlowPermissionForEdit(string PageMode)
    {
        int TableType = (int)TSP.DataManager.TableCodes.EmployeeRequest;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveEmployeeInfo;
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        int Permisssion = -1;
        Permisssion = TaskDoerManager.CheckWorkFlowPermissionForEditInfo(TableType, _EmpReId, TaskCode, Utility.GetCurrentUser_UserId());
        if (Permisssion > 0)
        {
            switch (PageMode)
            {
                case "Edit":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
                case "View":
                    btnEdit.Enabled = true;
                    btnEdit2.Enabled = true;
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
                case "Change":
                    btnSave.Enabled = true;
                    btnSave2.Enabled = true;
                    break;
            }
        }
        else
        {
            switch (PageMode)
            {
                case "View":
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;

                case "Change":
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    break;
            }
        }

        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;

    }

    private void InsertWorkFlowStateView(int TableType, int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        try
        {
            int ViewState = WorkFlowStateManager.InsertWorkFlowViewState(TableType, TableId, "View", Utility.GetCurrentUser_UserId());
            if (ViewState == -4)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در مشاهده اطلاعات انجام گرفته است";
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

    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";

        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "EmpImage" + _EmpReId.ToString() + Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/image/Employee/Pic/") + ret) == true);

            string tempFileName = "~/image/Employee/Pic/" + ret;

            uploadedFile.SaveAs(MapPath(tempFileName), true);
            Session["EmpImg"] = tempFileName;

        }
        return ret;
    }

    protected string SaveSign(UploadedFile uploadedFile)
    {
        string ret = "";


        if (uploadedFile.IsValid)
        {
            do
            {
                System.IO.FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "EmpSign" + _EmpReId.ToString() + Path.GetRandomFileName() + ImageType.Extension;

            } while (File.Exists(MapPath("~/image/Employee/Sign/") + ret) == true);
            string tempFileName = "~/image/Employee/Sign/" + ret;

            uploadedFile.SaveAs(MapPath(tempFileName), true);
            Session["EmpSign"] = tempFileName;
        }
        return ret;
    }

    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion

}
