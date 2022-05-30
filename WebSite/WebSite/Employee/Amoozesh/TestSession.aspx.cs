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
using DevExpress.Web;
using System.IO;

public partial class Employee_Amoozesh_TestSession : System.Web.UI.Page
{
    DataTable dtObserver = null;
    DataTable dtWrong = null;
    DataTable dtAbsent = null;

    #region Events
    int _PPId
    {
        get
        {
            return Convert.ToInt32(PeriodId.Value);
        }
        set
        {
            PeriodId.Value = value.ToString();
        }
    }

    string _PageMode
    {
        get
        {
            return PageMode.Value;
        }
        set
        {
            PageMode.Value = value;
        }
    }

    int _TsId
    {
        get
        {
            return Convert.ToInt32(SessionId.Value);
        }
        set
        {
            SessionId.Value = value.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            try
            {
                #region Set Datatable Sessions
                Session["TestSessionFile"] = null;
                Session["TestObserver"] = null;
                Session["TestAbsent"] = null;
                Session["TestWrong"] = null;

                if (Session["TestObserver"] == null)
                {
                    dtObserver = new DataTable();
                    dtObserver.Columns.Add("EmpId");
                    dtObserver.Columns.Add("Code");
                    dtObserver.Columns.Add("FirstName");
                    dtObserver.Columns.Add("LastName");
                    dtObserver.Columns.Add("TypeName");
                    dtObserver.Columns.Add("Type");
                    dtObserver.Columns.Add("Description");
                    dtObserver.Columns.Add("Id");                    
                    dtObserver.Columns["Id"].AutoIncrement = true;
                    dtObserver.Columns["Id"].AutoIncrementSeed = 1;
                    dtObserver.Constraints.Add("PK_ID", dtObserver.Columns["Id"], true);
                    Session["TestObserver"] = dtObserver;
                }
                else
                    dtObserver = (DataTable)Session["TestObserver"];

                Grdv_Moragheb.DataSource = dtObserver;
                Grdv_Moragheb.DataBind();

                if (Session["TestWrong"] == null)
                {
                    dtWrong = new DataTable();
                    dtWrong.Columns.Add("Code");
                    dtWrong.Columns.Add("FirstName");
                    dtWrong.Columns.Add("LastName");
                    dtWrong.Columns.Add("TypeName");
                    dtWrong.Columns.Add("Type");
                    dtWrong.Columns.Add("Description");
                    dtWrong.Columns.Add("Id");
                    dtWrong.Columns["Id"].AutoIncrement = true;
                    dtWrong.Columns["Id"].AutoIncrementSeed = 1;
                    dtWrong.Constraints.Add("PK_ID", dtWrong.Columns["Id"], true);
                    Session["TestWrong"] = dtWrong;
                }
                else
                    dtWrong = (DataTable)Session["TestWrong"];

                Grdv_Mo.DataSource = dtWrong;
                Grdv_Mo.DataBind();

                if (Session["TestAbsent"] == null)
                {
                    dtAbsent = new DataTable();
                    dtAbsent.Columns.Add("Code");
                    dtAbsent.Columns.Add("FirstName");
                    dtAbsent.Columns.Add("LastName");
                    dtAbsent.Columns.Add("TypeName");
                    dtAbsent.Columns.Add("Type");
                    dtAbsent.Columns.Add("Description");
                    dtAbsent.Columns.Add("Id");
                    dtAbsent.Columns["Id"].AutoIncrement = true;
                    dtAbsent.Columns["Id"].AutoIncrementSeed = 1;
                    dtAbsent.Constraints.Add("PK_ID", dtAbsent.Columns["Id"], true);
                    Session["TestAbsent"] = dtAbsent;
                }
                else
                    dtAbsent = (DataTable)Session["TestAbsent"];

                Grdv_Absent.DataSource = dtAbsent;
                Grdv_Absent.DataBind();
                #endregion
                #region SetData
                if (string.IsNullOrEmpty(Request.QueryString["PPId"]) || string.IsNullOrEmpty(Request.QueryString["PgM"]) || string.IsNullOrEmpty(Request.QueryString["TsId"]))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }

                _PPId = Convert.ToInt32(Utility.DecryptQS((Request.QueryString["PPId"])));
                _PageMode = Utility.DecryptQS(Request.QueryString["PgM"]);
                _TsId = Convert.ToInt32(Utility.DecryptQS((Request.QueryString["TsId"])));

                FillPeriodInfo(_PPId);
                TSP.DataManager.TestSessionManager SessionManager = new TSP.DataManager.TestSessionManager();
                switch (_PageMode)
                {
                    case "Edit":
                        SessionManager.FindByCode(_TsId);
                        if (SessionManager.Count == 1)
                        {
                            FillForm(_TsId);
                        }
                        RoundPanelPage.HeaderText = "ویرایش";
                        break;
                    case "View":
                        SessionManager.FindByCode(_TsId);
                        if (SessionManager.Count == 1)
                        {
                            FillForm(_TsId);
                        }
                        Disable();
                        btnSave.Enabled = false;
                        btnSave2.Enabled = false;
                        RoundPanelPage.HeaderText = "مشاهده";
                        break;
                    case "New":
                        RoundPanelPage.HeaderText = "جدید";
                        break;
                }


                OdbPeriodRegister.SelectParameters["PPId"].DefaultValue = _PPId.ToString();

                this.ViewState["BtnSave"] = btnSave.Enabled;
                #endregion
            }
            catch (Exception err)
            {
                Utility.SaveWebsiteError(err);
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        dtObserver = (DataTable)Session["TestObserver"];
        if (dtObserver.DefaultView.Count == 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "مراقبین جلسه را مشخص نمایید";
            return;
        }
        dtAbsent = (DataTable)Session["TestAbsent"];
        int AbCount = dtAbsent.DefaultView.Count;
        if (AbCount != int.Parse(txtAbCount.Text))
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "تمامی غائبین جلسه مشخص نشده است";
            return;
        }
        if (_PageMode=="New")
            Insert(_PPId);
        else if (_PageMode == "Edit")
        {
            Edit(_TsId);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["TestSessionFile"] = null;
        Session["TestObserver"] = null;
        Session["TestAbsent"] = null;
        Session["TestWrong"] = null;        
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && _PPId==null)
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            Response.Redirect("Periods.aspx?PostId=" + Utility.EncryptQS(_PPId.ToString()) + "&GrdFlt=" + GrdFlt);
        }
        else
        {
            Response.Redirect("Periods.aspx");
        }

    }

    protected void flp_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
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

    protected void txtMeNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ComboType.Value != null)
            {
                if (ComboType.Value.ToString() == "0")//Emp
                {
                    TSP.DataManager.EmployeeManager EmpManager = new TSP.DataManager.EmployeeManager();
                    if (!(string.IsNullOrEmpty(txtMeNo.Text)))
                    {
                        EmpManager.FindByEmpCode(txtMeNo.Text);
                        if (EmpManager.Count > 0)
                        {
                            txtMeFirstName.Text = EmpManager[0]["FirstName"].ToString();
                            txtMeLastName.Text = EmpManager[0]["LastName"].ToString();
                            txtEmpId.Text = EmpManager[0]["EmpId"].ToString();

                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "چنین کد کارمندی وجود ندارد.مجددا وارد نمایید";
                            return;
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "کد کارمندی را وارد نمایید";
                    }
                }
                else //Me
                {
                    TSP.DataManager.MemberManager MemManager = new TSP.DataManager.MemberManager();

                    if (!(string.IsNullOrEmpty(txtMeNo.Text)))
                    {
                        MemManager.FindByCode(int.Parse(txtMeNo.Text));
                        if (MemManager.Count > 0)
                        {

                            txtMeFirstName.Text = MemManager[0]["FirstName"].ToString();
                            txtMeLastName.Text = MemManager[0]["LastName"].ToString();

                        }
                        else
                        {

                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "چنین کدعضویتی وجود ندارد.مجددا وارد نمایید";
                            return;
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "کدعضویت را وارد نمایید";
                    }
                }

            }

            else
            {

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "سمت مراقب را انتخاب نمایید";
                return;
            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی رخ داده است. مجدداً وارد نمایید";
        }
    }

    protected void btnAddMoragheb_Click(object sender, EventArgs e)
    {

        if (Session["TestObserver"] == null)
        {
            SetMessage("اعتبار صفحه به پایان رسیده است.");
            return;
        }
        dtObserver = (DataTable)Session["TestObserver"];

        DataRow dr = dtObserver.NewRow();

        try
        {
            if (Grdv_Moragheb.VisibleRowCount > 0)
            {
                for (int i = 0; i < Grdv_Moragheb.VisibleRowCount; i++)
                {
                    Grdv_Moragheb.DataSource = (DataTable)Session["TestObserver"];
                    Grdv_Moragheb.DataBind();

                    DataRowView row = (DataRowView)Grdv_Moragheb.GetRow(i);
                    if (row["Code"].ToString() == txtMeNo.Text && row["FirstName"].ToString() == txtMeFirstName.Text && row["LastName"].ToString() == txtMeLastName.Text)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                        return;
                    }
                }
            }
            if (ComboType.Value.ToString() == "0")//Emp
            {

                dr["TypeName"] = ComboType.SelectedItem.Text;
                dr["Type"] = (int)TSP.DataManager.UserType.Employee;//UltId
                dr["Code"] = txtMeNo.Text;
                dr["EmpId"] = txtEmpId.Text;
                dr["FirstName"] = txtMeFirstName.Text;
                dr["LastName"] = txtMeLastName.Text;
            }
            else if (ComboType.Value.ToString() == "1")//Me
            {
                dr["TypeName"] = ComboType.SelectedItem.Text;
                dr["Type"] = (int)TSP.DataManager.UserType.Member;//UltId
                dr["Code"] = txtMeNo.Text;
                dr["FirstName"] = txtMeFirstName.Text;
                dr["LastName"] = txtMeLastName.Text;
            }

            dr["Description"] = txtMorDesc.Text;

            dtObserver.Rows.Add(dr);
            Grdv_Moragheb.DataSource = dtObserver;
            Grdv_Moragheb.DataBind();


            ComboType.SelectedIndex = -1;
            txtMeFirstName.Text = "";
            txtMeLastName.Text = "";
            txtMeNo.Text = "";
            txtMorDesc.Text = "";


        }
        catch
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
        }



    }

    protected void btnAddAbsent_Click(object sender, EventArgs e)
    {
        if (Session["TestAbsent"] == null)
        {
            SetMessage("اعتبار صفحه به پایان رسیده است.");
            return;
        }
        dtAbsent = (DataTable)Session["TestAbsent"];

        DataRow dr = dtAbsent.NewRow();

        try
        {
            if (Grdv_Absent.VisibleRowCount > 0)
            {
                for (int i = 0; i < Grdv_Absent.VisibleRowCount; i++)
                {
                    Grdv_Absent.DataSource = (DataTable)Session["TestAbsent"];
                    Grdv_Absent.DataBind();

                    DataRowView row = (DataRowView)Grdv_Absent.GetRow(i);
                    if (row["Code"].ToString() == TextAbId.Text && row["FirstName"].ToString() == txtAbFirstName.Text && row["LastName"].ToString() == txtAbLastName.Text)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                        return;
                    }
                }
            }

            dr["TypeName"] = txtAbTypeName.Text;

            if (TextAbType.Text == "true")//member
                dr["Type"] = 1;//UltId     
            else
                dr["Type"] = 0;//other person
            dr["Code"] = TextAbId.Text;
            dr["FirstName"] = txtAbFirstName.Text;
            dr["LastName"] = txtAbLastName.Text;
            dr["Description"] = txtAbDesc.Text;

            dtAbsent.Rows.Add(dr);
            Session["TestAbsent"] = dtAbsent;
            BindAbsentGrid();

            TextAbId.Text = "";
            txtAbTypeName.Text = "";
            txtAbFirstName.Text = "";
            txtAbLastName.Text = "";
            txtAbDesc.Text = "";


        }
        catch
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
        }

    }

    protected void btnAddMo_Click(object sender, EventArgs e)
    {
        if (Session["TestWrong"] == null)
        {
            SetMessage("اعتبار صفحه به پایان رسیده است.");
            return;
        }
        dtWrong = (DataTable)Session["TestWrong"];

        DataRow dr = dtWrong.NewRow();

        try
        {
            if (Grdv_Mo.VisibleRowCount > 0)
            {
                for (int i = 0; i < Grdv_Mo.VisibleRowCount; i++)
                {
                    Grdv_Mo.DataSource = (DataTable)Session["TestWrong"];
                    Grdv_Mo.DataBind();

                    DataRowView row = (DataRowView)Grdv_Mo.GetRow(i);
                    if (row["Code"].ToString() == TextMoId.Text && row["FirstName"].ToString() == txtMoFirstName.Text && row["LastName"].ToString() == txtMoLastName.Text)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات وارد شده تکراری می باشد";
                        return;
                    }
                }
            }

            dr["TypeName"] = txtMoTypeName.Text;
            if (TextMoType.Text == "true")//member
                dr["Type"] = 1;//UltId     
            else
                dr["Type"] = 0;//other person
            dr["Code"] = TextMoId.Text;
            dr["FirstName"] = txtMoFirstName.Text;
            dr["LastName"] = txtMoLastName.Text;
            dr["Description"] = txtMoDesc.Text;

            dtWrong.Rows.Add(dr);
            Grdv_Mo.DataSource = dtWrong;
            Grdv_Mo.DataBind();

            TextMoId.Text = "";
            txtMoTypeName.Text = "";
            txtMoFirstName.Text = "";
            txtMoLastName.Text = "";
            txtMoDesc.Text = "";


        }
        catch
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در اضافه کردن رخ داده است";
        }
    }

    protected void OnPageIndexChanged_Grdv_Absent(object sender, EventArgs e)
    {
        BindAbsentGrid();
    }

    protected void Grdv_Moragheb_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;
        Grdv_Moragheb.DataSource = (DataTable)Session["TestObserver"];
        Grdv_Moragheb.DataBind();

        int Id = -1;
        if (Grdv_Moragheb.FocusedRowIndex > -1)
        {
            Id = Grdv_Moragheb.FocusedRowIndex;
        }
        if (Id == -1)
        {
            Grdv_Moragheb.JSProperties["cpMsg"] = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            Grdv_Moragheb.JSProperties["cpError"] = 1;
            Grdv_Moragheb.CancelEdit();
            return;
        }
        else
        {
            dtObserver = (DataTable)Session["TestObserver"];
            DataRow dr = dtObserver.Rows.Find(e.Keys[0]);

            dtObserver.Rows.Find(e.Keys[0]).Delete();
            Session["TestObserver"] = dtObserver;
            Grdv_Moragheb.DataSource = (DataTable)Session["TestObserver"];
            Grdv_Moragheb.DataBind();
            dtObserver = (DataTable)Session["TestObserver"];
        }
    }

    protected void Grdv_Absent_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;
        Grdv_Absent.DataSource = (DataTable)Session["TestAbsent"];
        Grdv_Absent.DataBind();

        int Id = -1;
        if (Grdv_Absent.FocusedRowIndex > -1)
        {
            Id = Grdv_Absent.FocusedRowIndex;
        }
        if (Id == -1)
        {
            Grdv_Absent.JSProperties["cpMsg"] = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            Grdv_Absent.JSProperties["cpError"] = 1;
            Grdv_Absent.CancelEdit();
            return;
        }
        else
        {
            dtAbsent = (DataTable)Session["TestAbsent"];
            DataRow dr = dtAbsent.Rows.Find(e.Keys[0]);

            dtAbsent.Rows.Find(e.Keys[0]).Delete();
            Session["TestAbsent"] = dtAbsent;
            Grdv_Absent.DataSource = (DataTable)Session["TestAbsent"];
            Grdv_Absent.DataBind();
            dtAbsent = (DataTable)Session["TestAbsent"];
        }
    }

    protected void Grdv_Mo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;
        Grdv_Mo.DataSource = (DataTable)Session["TestWrong"];
        Grdv_Mo.DataBind();

        int Id = -1;
        if (Grdv_Mo.FocusedRowIndex > -1)
        {
            Id = Grdv_Mo.FocusedRowIndex;
        }
        if (Id == -1)
        {
            Grdv_Mo.JSProperties["cpMsg"] = "لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید";
            Grdv_Mo.JSProperties["cpError"] = 1;
            Grdv_Mo.CancelEdit();
            return;
        }
        else
        {
            dtWrong = (DataTable)Session["TestWrong"];
            DataRow dr = dtWrong.Rows.Find(e.Keys[0]);

            dtWrong.Rows.Find(e.Keys[0]).Delete();
            Session["TestWrong"] = dtWrong;
            Grdv_Mo.DataSource = (DataTable)Session["TestWrong"];
            Grdv_Mo.DataBind();
            dtWrong = (DataTable)Session["TestWrong"];
        }
    }
    #endregion

    #region Methods

    private void BindAbsentGrid()
    {
        if (Session["TestAbsent"] != null)
        {
            Grdv_Absent.DataSource = (DataTable)Session["TestAbsent"];
            Grdv_Absent.DataBind();
        }
    }

    protected void FillForm(int TsId)
    {
        TSP.DataManager.TestSessionManager SessionManager = new TSP.DataManager.TestSessionManager();
        TSP.DataManager.TestSessionMembersManager SessionMemManager = new TSP.DataManager.TestSessionMembersManager();
        try
        {
            SessionManager.FindByCode(TsId);
            if (SessionManager.Count == 1)
            {
                txtSeStartTime.Text = SessionManager[0]["StartTime"].ToString();
                txtSeEndTime.Text = SessionManager[0]["EndTime"].ToString();
                txtParCount.Text = SessionManager[0]["ParticipantCount"].ToString();
                txtAttCount.Text = SessionManager[0]["AttendantCount"].ToString();
                txtAbCount.Text = SessionManager[0]["AbsentCount"].ToString();
                txtSessionNo.Text = SessionManager[0]["SessionNo"].ToString();
                txtSessionDate.Text = SessionManager[0]["SessionDate"].ToString();
                txtSeDesc.Text = SessionManager[0]["Description"].ToString();
                HpLinkFile.NavigateUrl = SessionManager[0]["FileUrl"].ToString();

                #region Observers
                //attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.ExpertPlace, EpId);
                SessionMemManager.FindBySessionType(TsId, 1);
                dtObserver = (DataTable)Session["TestObserver"];
                for (int i = 0; i < SessionMemManager.Count; i++)
                {
                    DataRow dr = dtObserver.NewRow();
                    if (SessionMemManager[i]["UltId"].ToString() == "1")
                        dr["Code"] = SessionMemManager[i]["PkId"].ToString();
                    else
                        dr["Code"] = SessionMemManager[i]["Code"].ToString();
                    dr["FirstName"] = SessionMemManager[i]["FirstName"].ToString();
                    dr["LastName"] = SessionMemManager[i]["LastName"].ToString();
                    dr["Description"] = SessionMemManager[i]["Description"].ToString();
                    dr["TypeName"] = SessionMemManager[i]["TypeName"].ToString();

                    dr["Id"] = SessionMemManager[i]["TsmId"];
                    dtObserver.Rows.Add(dr);

                }
                dtObserver.AcceptChanges();
                Grdv_Moragheb.DataSource = dtObserver;
                Grdv_Moragheb.DataBind();
                #endregion
                #region Absents
                //attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.ExpertPlace, EpId);
                SessionMemManager.FindBySessionType(TsId, 2);
                dtAbsent = (DataTable)Session["TestAbsent"];
                for (int i = 0; i < SessionMemManager.Count; i++)
                {
                    DataRow dr = dtAbsent.NewRow();
                    dr["Code"] = SessionMemManager[i]["PkId"].ToString();
                    dr["FirstName"] = SessionMemManager[i]["FirstName"].ToString();
                    dr["LastName"] = SessionMemManager[i]["LastName"].ToString();
                    dr["Description"] = SessionMemManager[i]["Description"].ToString();
                    dr["TypeName"] = SessionMemManager[i]["TypeName"].ToString();

                    dr["Id"] = SessionMemManager[i]["TsmId"];
                    dtAbsent.Rows.Add(dr);

                }
                dtAbsent.AcceptChanges();
                Grdv_Absent.DataSource = dtAbsent;
                Grdv_Absent.DataBind();
                #endregion
                #region Wrong
                //attachManager.FindByTablePrimaryKey((int)TSP.DataManager.TableCodes.ExpertPlace, EpId);
                SessionMemManager.FindBySessionType(TsId, 3);
                dtWrong = (DataTable)Session["TestWrong"];
                for (int i = 0; i < SessionMemManager.Count; i++)
                {
                    DataRow dr = dtWrong.NewRow();
                    dr["Code"] = SessionMemManager[i]["PkId"].ToString();
                    dr["FirstName"] = SessionMemManager[i]["FirstName"].ToString();
                    dr["LastName"] = SessionMemManager[i]["LastName"].ToString();
                    dr["Description"] = SessionMemManager[i]["Description"].ToString();
                    dr["TypeName"] = SessionMemManager[i]["TypeName"].ToString();

                    dr["Id"] = SessionMemManager[i]["TsmId"];
                    dtWrong.Rows.Add(dr);

                }
                dtWrong.AcceptChanges();
                Grdv_Mo.DataSource = dtWrong;
                Grdv_Mo.DataBind();
                #endregion
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
            }


        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
    }

    protected void Disable()
    {
        //for (int i = 0; i < ASPxRoundPanel4.Controls.Count; i++)
        //{
        //    if (ASPxRoundPanel4.Controls[i] is DevExpress.Web.ASPxTextBox)
        //    {
        //        DevExpress.Web.ASPxTextBox txt = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel4.Controls[i];
        //        txt.ReadOnly = true;
        //    }

        //}
        RoundPanelPage.Enabled = false;
        tblAbsents.Visible = tblMor.Visible = tblMot.Visible = false;
        //txtSessionDate.ReadOnly = true;
        //lblFileNew.Visible = false;
        //flp.Visible = false;
        //HpLinkFile.Visible = true;

        //for (int i = 0; i <= 3; i++)
        //    tblMor.Rows[i].Visible = false;
        //for (int j = 0; j <= 3; j++)
        //    tblMot.Rows[j].Visible = false;
        //for (int z = 0; z <= 3; z++)
        //    tblAb.Rows[z].Visible = false;
    }

    protected void Insert(int PPId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TestSessionManager SessionManager = new TSP.DataManager.TestSessionManager();
        TSP.DataManager.TestSessionMembersManager SessionMemManager = new TSP.DataManager.TestSessionMembersManager();
        TSP.DataManager.PeriodPresentManager PPManager = new TSP.DataManager.PeriodPresentManager();
        trans.Add(SessionManager);
        trans.Add(SessionMemManager);
        trans.Add(PPManager);


        try
        {
            trans.BeginSave();
            DataRow drSe = SessionManager.NewRow();
            drSe["PPId"] = PPId;
            drSe["StartTime"] = txtSeStartTime.Text;
            drSe["EndTime"] = txtSeEndTime.Text;
            drSe["ParticipantCount"] = txtParCount.Text;
            drSe["AttendantCount"] = txtAttCount.Text;
            drSe["AbsentCount"] = txtAbCount.Text;
            drSe["SessionNo"] = txtSessionNo.Text;
            drSe["SessionDate"] = txtSessionDate.Text;
            drSe["Description"] = txtSeDesc.Text;
            drSe["UserId"] = Utility.GetCurrentUser_UserId();
            drSe["ModifiedDate"] = DateTime.Now;

            if (Session["TestSessionFile"] != null)
            {
                drSe["FileUrl"] = "~/Image/Employee/Amoozesh/TestSession/" + Path.GetFileName(Session["TestSessionFile"].ToString());
            }

            SessionManager.AddRow(drSe);

            int cnt = SessionManager.Save();
            if (cnt <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;

            }
            int TsId = int.Parse(SessionManager[0]["TsId"].ToString());

            //if (Session["TestSessionFile"] != null)
            //{
            //    string ImgSoource = Server.MapPath("~/image/Temp/") + Path.GetFileName(Session["TestSessionFile"].ToString());
            //    string ImgTarget = Server.MapPath(SessionManager[0]["FileUrl"].ToString());
            //    File.Copy(ImgSoource, ImgTarget, true);

            //}

            //PPManager.FindByCode(PPId);
            //PPManager[0].BeginEdit();
            //PPManager[0]["Status"] = (int)TSP.DataManager.PeriodPresentStatus.StartTest;
            //PPManager[0].EndEdit();
            //PPManager.Save();

            dtObserver = (DataTable)Session["TestObserver"];

            if (dtObserver.DefaultView.Count > 0)
            {
                for (int i = 0; i < dtObserver.DefaultView.Count; i++)
                {
                    DataRow drObserver = SessionMemManager.NewRow();
                    drObserver["TsId"] = TsId;
                    if (dtObserver.Rows[i]["Type"].ToString() == "1")//Me
                        drObserver["PkId"] = dtObserver.Rows[i]["Code"].ToString();
                    else
                        drObserver["PkId"] = dtObserver.Rows[i]["EmpId"].ToString();
                    drObserver["UltId"] = dtObserver.Rows[i]["Type"].ToString();
                    drObserver["Type"] = 1;
                    drObserver["Description"] = dtObserver.Rows[i]["Description"].ToString();
                    drObserver["UserId"] = Utility.GetCurrentUser_UserId();
                    drObserver["ModifiedDate"] = DateTime.Now;
                    SessionMemManager.AddRow(drObserver);

                }
            }

            dtAbsent = (DataTable)Session["TestAbsent"];

            if (dtAbsent.DefaultView.Count > 0)
            {
                for (int i = 0; i < dtAbsent.DefaultView.Count; i++)
                {
                    DataRow drAbsent = SessionMemManager.NewRow();
                    drAbsent["TsId"] = TsId;
                    drAbsent["PkId"] = dtAbsent.Rows[i]["Code"].ToString();
                    drAbsent["UltId"] = dtAbsent.Rows[i]["Type"].ToString();
                    drAbsent["Type"] = 2;
                    drAbsent["Description"] = dtAbsent.Rows[i]["Description"].ToString();
                    drAbsent["UserId"] = Utility.GetCurrentUser_UserId();
                    drAbsent["ModifiedDate"] = DateTime.Now;
                    SessionMemManager.AddRow(drAbsent);

                }
            }

            dtWrong = (DataTable)Session["TestWrong"];

            if (dtWrong.DefaultView.Count > 0)
            {
                for (int i = 0; i < dtWrong.DefaultView.Count; i++)
                {
                    DataRow drWrong = SessionMemManager.NewRow();
                    drWrong["TsId"] = TsId;
                    drWrong["PkId"] = dtWrong.Rows[i]["Code"].ToString();
                    drWrong["UltId"] = dtWrong.Rows[i]["Type"].ToString();
                    drWrong["Type"] = 3;
                    drWrong["Description"] = dtWrong.Rows[i]["Description"].ToString();
                    drWrong["UserId"] = Utility.GetCurrentUser_UserId();
                    drWrong["ModifiedDate"] = DateTime.Now;
                    SessionMemManager.AddRow(drWrong);

                }
            }

            SessionMemManager.Save();
            trans.EndSave();
            btnSave.Enabled = false;
            btnSave2.Enabled = false;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";


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

    private void Edit(int TsId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TestSessionManager SessionManager = new TSP.DataManager.TestSessionManager();
        TSP.DataManager.TestSessionMembersManager SessionMemManager = new TSP.DataManager.TestSessionMembersManager();
        TSP.DataManager.PeriodPresentManager PPManager = new TSP.DataManager.PeriodPresentManager();
        trans.Add(SessionManager);
        trans.Add(SessionMemManager);
        trans.Add(PPManager);

        try
        {
            trans.BeginSave();
            SessionManager.FindByCode(TsId);
            if (SessionManager.Count != 1)
            {
                SetMessage("خطایی در ذخیره رخ داده است");
                trans.CancelSave();
                return;
            }
            SessionManager[0].BeginEdit();
            SessionManager[0]["PPId"] = _PPId;
            SessionManager[0]["StartTime"] = txtSeStartTime.Text;
            SessionManager[0]["EndTime"] = txtSeEndTime.Text;
            SessionManager[0]["ParticipantCount"] = txtParCount.Text;
            SessionManager[0]["AttendantCount"] = txtAttCount.Text;
            SessionManager[0]["AbsentCount"] = txtAbCount.Text;
            SessionManager[0]["SessionNo"] = txtSessionNo.Text;
            SessionManager[0]["SessionDate"] = txtSessionDate.Text;
            SessionManager[0]["Description"] = txtSeDesc.Text;
            SessionManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            SessionManager[0]["ModifiedDate"] = DateTime.Now;

            if (Session["TestSessionFile"] != null)
            {
                SessionManager[0]["FileUrl"] = "~/Image/Employee/Amoozesh/TestSession/" + Path.GetFileName(Session["TestSessionFile"].ToString());
            }

            SessionManager[0].EndEdit();
            if (SessionManager.Save() <= 0)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            //int TsId = int.Parse(SessionManager[0]["TsId"].ToString());
            //PPManager.FindByCode(_PPId);
            //PPManager[0].BeginEdit();
            //PPManager[0]["Status"] = (int)TSP.DataManager.PeriodPresentStatus.StartTest;
            //PPManager[0].EndEdit();
            //PPManager.Save();
            #region Observer
            dtObserver = (DataTable)Session["TestObserver"];
            if (dtObserver.GetChanges() != null)
            {
                DataRow[] delRows = dtObserver.Select(null, null, DataViewRowState.Deleted);
                DataRow[] EditRows = dtObserver.Select(null, null, DataViewRowState.ModifiedCurrent);
                DataRow[] insRows = dtObserver.Select(null, null, DataViewRowState.Added);
                if (delRows.Length > 0)
                {
                    for (int i = 0; i < delRows.Length; i++)
                    {
                        SessionMemManager.FindByCode(int.Parse(delRows[i]["Id", DataRowVersion.Original].ToString()));
                        SessionMemManager[0].Delete();
                        SessionMemManager.Save();
                        SessionMemManager.DataTable.AcceptChanges();
                    }
                }
                if (EditRows.Length > 0)
                {
                    for (int i = 0; i < EditRows.Length; i++)
                    {
                        SessionMemManager.FindByCode(int.Parse(EditRows[i]["Id"].ToString()));

                        SessionMemManager[0].BeginEdit();
                        SessionMemManager[0]["TsId"] = TsId;
                        if (EditRows[i]["Type"].ToString() == "1")//Me
                            SessionMemManager[0]["PkId"] = EditRows[i]["Code"].ToString();
                        else
                            SessionMemManager[0]["PkId"] = EditRows[i]["EmpId"].ToString();
                        SessionMemManager[0]["UltId"] = EditRows[i]["Type"].ToString();
                        SessionMemManager[0]["Type"] = 1;
                        SessionMemManager[0]["Description"] = EditRows[i]["Description"].ToString();
                        SessionMemManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        SessionMemManager[0]["ModifiedDate"] = DateTime.Now;
                        SessionMemManager[0].EndEdit();
                        SessionMemManager.Save();
                        SessionMemManager.DataTable.AcceptChanges();
                    }
                }

                if (insRows.Length > 0)
                {
                    for (int i = 0; i < insRows.Length; i++)
                    {
                        DataRow drObserver = SessionMemManager.NewRow();
                        drObserver["TsId"] = TsId;
                        if (insRows[i]["Type"].ToString() == "1")//Me
                            drObserver["PkId"] = insRows[i]["Code"].ToString();
                        else
                            drObserver["PkId"] = insRows[i]["EmpId"].ToString();
                        drObserver["UltId"] = insRows[i]["Type"].ToString();
                        drObserver["Type"] = 1;
                        drObserver["Description"] = insRows[i]["Description"].ToString();
                        drObserver["UserId"] = Utility.GetCurrentUser_UserId();
                        drObserver["ModifiedDate"] = DateTime.Now;
                        SessionMemManager.AddRow(drObserver); 
                        SessionMemManager.Save();
                        SessionMemManager.DataTable.AcceptChanges();
                    }
                }
            }
            #endregion
            #region TestAbsent
            dtAbsent = (DataTable)Session["TestAbsent"];
            if (dtAbsent.GetChanges() != null)
            {
                DataRow[] delRows = dtAbsent.Select(null, null, DataViewRowState.Deleted);
                DataRow[] EditRows = dtAbsent.Select(null, null, DataViewRowState.ModifiedCurrent);
                DataRow[] insRows = dtAbsent.Select(null, null, DataViewRowState.Added);
                if (delRows.Length > 0)
                {
                    for (int i = 0; i < delRows.Length; i++)
                    {
                        SessionMemManager.FindByCode(int.Parse(delRows[i]["Id", DataRowVersion.Original].ToString()));
                        SessionMemManager[0].Delete();
                        SessionMemManager.Save();
                        SessionMemManager.DataTable.AcceptChanges();
                    }
                }
                if (EditRows.Length > 0)
                {
                    for (int i = 0; i < EditRows.Length; i++)
                    {
                        SessionMemManager.FindByCode(int.Parse(EditRows[i]["Id"].ToString()));

                        SessionMemManager[0].BeginEdit();
                        SessionMemManager[0]["TsId"] = TsId;
                        SessionMemManager[0]["PkId"] = EditRows[i]["Code"].ToString();
                        SessionMemManager[0]["UltId"] = EditRows[i]["Type"].ToString();
                        SessionMemManager[0]["Type"] = 2;
                        SessionMemManager[0]["Description"] = EditRows[i]["Description"].ToString();
                        SessionMemManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        SessionMemManager[0]["ModifiedDate"] = DateTime.Now;
                        SessionMemManager[0].EndEdit();
                        SessionMemManager.Save();
                        SessionMemManager.DataTable.AcceptChanges();
                    }
                }

                if (insRows.Length > 0)
                {
                    for (int i = 0; i < insRows.Length; i++)
                    {
                        DataRow drObserver = SessionMemManager.NewRow();
                        drObserver["TsId"] = TsId;
                        drObserver["PkId"] = insRows[i]["Code"].ToString();
                        drObserver["UltId"] = insRows[i]["Type"].ToString();
                        drObserver["Type"] = 2;
                        drObserver["Description"] = insRows[i]["Description"].ToString();
                        drObserver["UserId"] = Utility.GetCurrentUser_UserId();
                        drObserver["ModifiedDate"] = DateTime.Now;
                        SessionMemManager.AddRow(drObserver);
                        SessionMemManager.Save();
                        SessionMemManager.DataTable.AcceptChanges();
                    }
                }
            }
            #endregion
            #region TestWrong
            dtWrong = (DataTable)Session["TestWrong"];
            if (dtWrong.GetChanges() != null)
            {
                DataRow[] delRows = dtAbsent.Select(null, null, DataViewRowState.Deleted);
                DataRow[] EditRows = dtAbsent.Select(null, null, DataViewRowState.ModifiedCurrent);
                DataRow[] insRows = dtAbsent.Select(null, null, DataViewRowState.Added);
                if (delRows.Length > 0)
                {
                    for (int i = 0; i < delRows.Length; i++)
                    {
                        SessionMemManager.FindByCode(int.Parse(delRows[i]["TsmId", DataRowVersion.Original].ToString()));
                        SessionMemManager[0].Delete();
                        SessionMemManager.Save();
                        SessionMemManager.DataTable.AcceptChanges();
                    }
                }
                if (EditRows.Length > 0)
                {
                    for (int i = 0; i < EditRows.Length; i++)
                    {
                        SessionMemManager.FindByCode(int.Parse(EditRows[i]["TsmId"].ToString()));

                        SessionMemManager[0].BeginEdit();
                        SessionMemManager[0]["TsId"] = TsId;
                        SessionMemManager[0]["PkId"] = EditRows[i]["Code"].ToString();
                        SessionMemManager[0]["UltId"] = EditRows[i]["Type"].ToString();
                        SessionMemManager[0]["Type"] = 3;
                        SessionMemManager[0]["Description"] = EditRows[i]["Description"].ToString();
                        SessionMemManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        SessionMemManager[0]["ModifiedDate"] = DateTime.Now;
                        SessionMemManager[0].EndEdit();
                        SessionMemManager.Save();
                        SessionMemManager.DataTable.AcceptChanges();
                    }
                }

                if (insRows.Length > 0)
                {
                    for (int i = 0; i < insRows.Length; i++)
                    {
                        DataRow drObserver = SessionMemManager.NewRow();
                        drObserver["TsId"] = TsId;
                        drObserver["PkId"] = insRows[i]["Code"].ToString();
                        drObserver["UltId"] = insRows[i]["Type"].ToString();
                        drObserver["Type"] = 3;
                        drObserver["Description"] = insRows[i]["Description"].ToString();
                        drObserver["UserId"] = Utility.GetCurrentUser_UserId();
                        drObserver["ModifiedDate"] = DateTime.Now;
                        SessionMemManager.AddRow(drObserver);
                        SessionMemManager.Save();
                        SessionMemManager.DataTable.AcceptChanges();
                    }
                }
            }
            #endregion
            trans.EndSave();
            //btnSave.Enabled = false;
            //btnSave2.Enabled = false;
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";


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
                    SetMessage("اطلاعات تکراری می باشد");
                }

                else
                {
                    SetMessage("خطایی در ذخیره انجام گرفته است");
                }
            }
            else
            {
                SetMessage("خطایی در ذخیره انجام گرفته است");
            }
        }
    }

    protected void FillPeriodInfo(int PPId)
    {
        TSP.DataManager.PeriodPresentManager Manager = new TSP.DataManager.PeriodPresentManager();
        try
        {
            Manager.FindByCode(PPId);
            if (Manager.Count == 1)
            {
                txtPPTitle.Text = Manager[0]["PeriodTitle"].ToString();
                txtTestDate.Text = Manager[0]["TestDate"].ToString();
                txtTestHour.Text = Manager[0]["TestHour"].ToString();
                txtTestPlace.Text = Manager[0]["TestPlace"].ToString();

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در مشاهده اطلاعات دوره رخ داده است";
            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات دوره رخ داده است";
        }

    }

    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo ImageType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = Path.GetRandomFileName() + ImageType.Extension;
            } while (File.Exists(MapPath("~/Image/Employee/Amoozesh/TestSession/") + ret) == true);
            string tempFileName = MapPath("~/Image/Employee/Amoozesh/TestSession/") + ret;
            uploadedFile.SaveAs(tempFileName, true);
            Session["TestSessionFile"] = tempFileName;
        }
        return ret;
    }

    private void SetMessage(string Msg)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Msg;
    }
    #endregion

}
