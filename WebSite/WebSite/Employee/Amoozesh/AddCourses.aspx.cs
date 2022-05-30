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

public partial class Employee_Amoozesh_AddCourses : System.Web.UI.Page
{
    #region Private Members
    int CrId = -1;
    string _PageMode
    {
        get
        {
            return PgMode.Value;
        }
        set
        {
            PgMode.Value = value;
        }
    }


    int _CourseId
    {
        get
        {

            return Convert.ToInt32(CourseId.Value);
        }
        set
        {
            CourseId.Value = value.ToString();
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {


        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            if ((string.IsNullOrEmpty(Request.QueryString["PageMode"])) || (string.IsNullOrEmpty(Request.QueryString["CrsId"])))
            {
                Response.Redirect("Course.aspx");
                return;
            }


            TSP.DataManager.Permission per = TSP.DataManager.CourseManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;

            SetKey();
            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;


        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        SetNewMode();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        SetEditMode();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(_PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        if (_PageMode == "New")
        {
            Insert();
        }
        else if (_PageMode == "Edit")
        {
            if (Utility.IsDBNullOrNullValue(_CourseId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            Edit(_CourseId);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && _CourseId != -1)
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            Response.Redirect("Course.aspx?PostId=" + Utility.EncryptQS(_CourseId.ToString()) + "&GrdFlt=" + GrdFlt);
        }
        else
        {
            Response.Redirect("Course.aspx");
        }

    }

    protected void MenuCourseDetails_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        switch (e.Item.Name)
        {
            case "Prerequisite":
                Response.Redirect("~/Employee/Amoozesh/CoursePrerequisite.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "CourseRefrence":
                Response.Redirect("~/Employee/Amoozesh/CourseRefrences.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "CourseDetail":
                Response.Redirect("AddCourseDetails.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Group":
                Response.Redirect("CourseGroups.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
            case "Attachment":
                Response.Redirect("CourseAttachments.aspx?CrsId=" + Utility.EncryptQS(_CourseId.ToString()) + "&PageMode=" + Utility.EncryptQS(_PageMode) + "&GrdFlt=" + Request.QueryString["GrdFlt"]);
                break;
        }
    }
    #endregion

    #region methods
    private void SetKey()
    {
        try
        {
            _PageMode = Utility.DecryptQS(Request.QueryString["PageMode"]);
            _CourseId = Convert.ToInt32(Utility.DecryptQS(Request.QueryString["CrsId"]));
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

        if (string.IsNullOrEmpty(_PageMode))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        } 
        if (Utility.IsDBNullOrNullValue(_CourseId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        switch (_PageMode)
        {
            case "View":
                SetViewMode();
                break;
            case "New":
                SetNewMode();
                break;
            case "Edit":
                SetEditMode();
                break;

            case "Clear":
                ClearForm();
                break;
        }
    }

    private void SetNewMode()
    {
        _CourseId = -1;
        _PageMode = "New";

        SetEnabled(true);
        ClearForm();
        MenuCourseDetails.Enabled = false;
        RoundPanelContent.HeaderText = "جدید";

        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;

        TSP.DataManager.Permission per = TSP.DataManager.CourseManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
    }

    private void SetEditMode()
    {
        if (Utility.IsDBNullOrNullValue(_CourseId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        } if (string.IsNullOrEmpty(_PageMode) && _PageMode != "View")
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        SetEnabled(true);
        MenuCourseDetails.Enabled = true;

        FillForm(_CourseId);
        RoundPanelContent.Enabled = true;
        _PageMode = "Edit";
        RoundPanelContent.HeaderText = "ویرایش";

        TSP.DataManager.Permission per = TSP.DataManager.CourseManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        this.ViewState["BtnSave"] = btnSave.Enabled;
    }

    private void SetViewMode()
    {
        SetEnabled(false);
        FillForm(_CourseId);
        MenuCourseDetails.Enabled = true;
        btnSave.Enabled = false;
        btnSave2.Enabled = false;
        RoundPanelContent.HeaderText = "مشاهده";
    }

    protected void FillForm(int CrsId)
    {
        TSP.DataManager.CourseManager manager = new TSP.DataManager.CourseManager();

        manager.FindByCode(CrsId);
        if (manager.Count == 1)
        {
            if (Convert.ToBoolean(manager[0]["InActive"]) == true)
            {
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
            }

            txtCourseId.Text = manager[0]["CrsCode"].ToString();
            txtCourseName.Text = manager[0]["CrsName"].ToString();
            txtDuration.Text = manager[0]["Duration"].ToString();
            txtPoint.Text = manager[0]["Point"].ToString();
            txtValidDiuration.Text = manager[0]["ValidDuration"].ToString();
            txtbPracticalDuration.Text = manager[0]["PracticalDuration"].ToString();
            txtbWorkroomDuration.Text = manager[0]["WorkroomDuration"].ToString();
            txtbNonPracticalDuration.Text = manager[0]["NonPracticalDuration"].ToString();
        }
    }

    protected void ClearForm()
    {
        txtbNonPracticalDuration.Text =
            txtbWorkroomDuration.Text =
            txtbPracticalDuration.Text =
            txtPoint.Text =
            txtValidDiuration.Text = "0";

        txtCourseId.Text =
            txtCourseName.Text =
            txtDuration.Text = "";
    }

    protected void SetEnabled(Boolean Enabled)
    {
        txtCourseId.Enabled =
        txtCourseName.Enabled =
        txtDuration.Enabled =
        txtPoint.Enabled =
        txtValidDiuration.Enabled =
        txtbNonPracticalDuration.Enabled =
        txtbPracticalDuration.Enabled =
        txtbWorkroomDuration.Enabled = Enabled;
    }
 
    private void SetMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    protected void Edit(int CrsId)
    {
        // TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.CourseManager manager = new TSP.DataManager.CourseManager();
        //  TSP.DataManager.CourseGroupsManager GroupManager = new TSP.DataManager.CourseGroupsManager();
        //  trans.Add(manager);
        // trans.Add(GroupManager);


        manager.FindByCode(CrsId);
        if (manager.Count != 1)
        {
            SetMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return;
        }

        try
        {
            // trans.BeginSave();
            manager[0].BeginEdit();
            manager[0]["CrsCode"] = txtCourseId.Text;
            manager[0]["CrsName"] = txtCourseName.Text;
            manager[0]["Duration"] = int.Parse(txtDuration.Text);
            manager[0]["Point"] = float.Parse(txtPoint.Text);
            if (!string.IsNullOrEmpty(txtValidDiuration.Text))
                manager[0]["ValidDuration"] = Int16.Parse(txtValidDiuration.Text);

            //manager[0]["Type"] = cmbType.Value;
            manager[0]["PracticalDuration"] = int.Parse(txtbPracticalDuration.Text);
            manager[0]["NonPracticalDuration"] = int.Parse(txtbNonPracticalDuration.Text);
            manager[0]["WorkroomDuration"] = int.Parse(txtbWorkroomDuration.Text);
            manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            manager[0]["ModifiedDate"] = DateTime.Now;
            manager[0].EndEdit();

            if (manager.Save() != 1)
            {
                // trans.CancelSave();

                SetMessage("خطایی در ذخیره انجام گرفته است");
            }

            _CourseId = Convert.ToInt32(manager[0]["CrsId"]);
            _PageMode = "Edit";
            // trans.EndSave();

            RoundPanelContent.HeaderText = "ویرایش";
            SetMessage("ذخیره انجام شد");

        }
        catch (Exception err)
        {
            // trans.CancelSave();
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetExceptionError(err));
        }
    }

    protected void Insert()
    {
        try
        {
            TSP.DataManager.CourseManager manager = new TSP.DataManager.CourseManager();
            int Duration = int.Parse(txtDuration.Text);
            int PracticalDuration = 0;
            int NonPracticalDuration = 0;
            int WorkroomDuration = 0;

            if (!string.IsNullOrEmpty(txtbPracticalDuration.Text))
                PracticalDuration = int.Parse(txtbPracticalDuration.Text);

            if (!string.IsNullOrEmpty(txtbNonPracticalDuration.Text))
                NonPracticalDuration = int.Parse(txtbNonPracticalDuration.Text);

            if (!string.IsNullOrEmpty(txtbWorkroomDuration.Text))
                WorkroomDuration = int.Parse(txtbWorkroomDuration.Text);

            if (PracticalDuration + NonPracticalDuration + WorkroomDuration != Duration)
            {
                SetMessage("تعداد ساعات تعیین شده با طول دوره یکسان نمی باشد ");
                return;
            }

            DataRow row = manager.NewRow();
            row["CrsCode"] = txtCourseId.Text;
            row["CrsName"] = txtCourseName.Text;
            row["Duration"] = Duration;
            row["Point"] = float.Parse(txtPoint.Text);
            if (!string.IsNullOrEmpty(txtValidDiuration.Text))
                row["ValidDuration"] = Int16.Parse(txtValidDiuration.Text);
            else
                row["ValidDuration"] = DBNull.Value;

            row["InActive"] = 0;
            row["IsProposal"] = 0;
            row["PracticalDuration"] = PracticalDuration;
            row["NonPracticalDuration"] = NonPracticalDuration;
            row["WorkroomDuration"] = WorkroomDuration;
            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;

            manager.AddRow(row);
            if (manager.Save() != 1)
            {
                SetMessage("خطایی در ذخیره انجام گرفته است");
                return;
            }

            _CourseId = Convert.ToInt32(manager[0]["CrsId"]);
            _PageMode = "Edit";
            RoundPanelContent.HeaderText = "ویرایش";
            MenuCourseDetails.Enabled = true;
            SetMessage("ذخیره انجام شد");

            btnEdit2.Enabled = false;
            btnEdit.Enabled = false;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            SetMessage(Utility.Messages.GetExceptionError(err));
            //    else if (se.Number == 2627)
            //    {
            //        this.DivReport.Visible = true;
            //        this.LabelWarning.Text = "کد درس تکراری می باشد.";
            //    }
        }
    }

    protected void Delete(int CrsId)
    {

        TSP.DataManager.CourseManager managerEdit = new TSP.DataManager.CourseManager();
        managerEdit.FindByCode(CrsId);
        if (managerEdit.Count == 1)
        {
            try
            {
                managerEdit[0].Delete();


                int cn = managerEdit.Save();
                if (cn == 1)
                {
                    //CourseId.Value = managerEdit[0]["CrsId"].ToString();
                    CourseId.Value = Utility.EncryptQS("");
                    PgMode.Value = Utility.EncryptQS("New");
                    RoundPanelContent.HeaderText = "جدید";
                    MenuCourseDetails.Enabled = false;
                    ClearForm();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "حذف انجام شد";

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
    #endregion
}
