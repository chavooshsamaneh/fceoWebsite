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
public partial class Employee_Amoozesh_AddTestObservers : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        //cmbMeId.DataBind();
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["PPId"]) || string.IsNullOrEmpty(Request.QueryString["TestId"]))
            {
                Response.Redirect("TestObserver.aspx");
                return;
            }

            TSP.DataManager.Permission per = TSP.DataManager.TestObserverManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanNew || per.CanEdit;
            //chbInActive.Checked = false;
            try
            {
                PeriodId.Value = Server.HtmlDecode(Request.QueryString["PPId"].ToString());
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                TestID.Value = Server.HtmlDecode(Request.QueryString["TestId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            string PageMode = Utility.DecryptQS(PgMode.Value);
            string TestId = Utility.DecryptQS(TestID.Value);
            string PPID = Utility.DecryptQS(PeriodId.Value);


            if (string.IsNullOrEmpty(PPID))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            //ODBObserver.FilterParameters[0].DefaultValue = PPID;
            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            if (string.IsNullOrEmpty(TestId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            TSP.DataManager.TestObserverManager mngDelete = new TSP.DataManager.TestObserverManager();
            mngDelete.FindByTestId(int.Parse(TestId));
            if (mngDelete.Count > 0 && PageMode == "New")
            {
                PageMode = "Edit";
                PgMode.Value = Utility.EncryptQS("Edit");
            }
            switch (PageMode)
            {
                case "View":
                    Disable();
                    if (string.IsNullOrEmpty(TestId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    btnEdit.Enabled = per.CanEdit;
                    btnEdit2.Enabled = per.CanEdit;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    FillForm(int.Parse(TestId), int.Parse(PPID));
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    break;


                case "New":
                    Enable();
                    ASPxRoundPanel2.HeaderText = "جدید";
                    if (string.IsNullOrEmpty(TestId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    FillForm(int.Parse(TestId), int.Parse(PPID));
                    ClearForm();
                    break;
                case "Edit":
                    Enable();

                    if (string.IsNullOrEmpty(TestId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }

                    FillForm(int.Parse(TestId), int.Parse(PPID));
                    ASPxRoundPanel2.Enabled = true;
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    break;
            }

            this.ViewState["BtnSave"] = btnSave.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

    }

    protected void FillForm(int TestId, int PPId)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);
        CustomAspxDevGridView1.DataBind();
        TSP.DataManager.TestObserverManager manager = new TSP.DataManager.TestObserverManager();
        manager.FindByTestId(TestId);
        //int PpId = int.Parse(manager[0]["PPId"].ToString());
        //TSP.DataManager.PeriodAttenderManager PAmanager = new TSP.DataManager.PeriodAttenderManager();
        //PAmanager.FindByPPId(PPId);
        if ((CustomAspxDevGridView1.VisibleRowCount > 0) && (manager.Count > 0))
        {
            for (int i = 0; i < CustomAspxDevGridView1.VisibleRowCount; i++)
            {
                DataRow row1 = CustomAspxDevGridView1.GetDataRow(i);

                for (int j = 0; j < manager.Count && CustomAspxDevGridView1.Selection.IsRowSelected(i) == false; j++)
                {
                    if (manager[j]["ObId"].ToString() == row1["ObId"].ToString())
                    {
                        CustomAspxDevGridView1.Selection.SelectRow(i);
                        //CustomAspxDevGridView1.Selection.IsRowSelected(i) = true;
                    }
                }
            }
        }

        //if (!string.IsNullOrEmpty(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PPId"].ToString()))))
        //{
        //    manager.FindByCode(TestId);
        //    if (manager.Count == 1)
        //    {
        //        cmbMemberType.SelectedIndex = cmbMemberType.Items.IndexOfValue(manager[0]["Type"].ToString());
        //        cmbMemberType_SelectedIndexChanged(this, new EventArgs());
        //        if (cmbMemberType.Value.ToString() == "1")
        //        {
        //            cmbMeId.SelectedIndex = cmbMeId.Items.IndexOfValue(manager[0]["MeID"].ToString());
        //        }
        //        else if (cmbMemberType.Value.ToString() == "0")
        //        {
        //            cmbOtherPerson.SelectedIndex = cmbOtherPerson.Items.IndexOfValue(manager[0]["MeID"].ToString());
        //        }
        //        txtDesc.Text = manager[0]["Description"].ToString();
        //        cmbTypeOfReg.SelectedIndex = cmbTypeOfReg.Items.IndexOfValue(manager[0]["TypeOfReg"].ToString());
        //        chbInActive.Checked = Convert.ToBoolean(manager[0]["InActive"].ToString());
        //    }
        //    string PPId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PPId"].ToString()));
        //    TSP.DataManager.PeriodPresentManager PPmanager = new TSP.DataManager.PeriodPresentManager();
        //    PPmanager.FindByCode(int.Parse(PPId));
        //    if (PPmanager.Count == 1)
        //    {
        //        if (PPmanager[0]["Type"].ToString() == "0")
        //        {
        //            cmbTypeOfReg.Visible = true;
        //            cmbTypeOfReg.SelectedIndex = cmbTypeOfReg.Items.IndexOfValue(manager[0]["TypeOfReg"].ToString());
        //        }
        //        else if (PPmanager[0]["Type"].ToString() == "1")
        //        {
        //            cmbTypeOfReg.Visible = false;

        //        }
        //    }
        //}

    }
    protected void ClearForm()
    {

        //txtDesc.Text = "";
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
        CustomAspxDevGridView1.Enabled = true;
    }
    protected void Enable()
    {
        CustomAspxDevGridView1.Enabled = true;


    }
    protected void Edit(int TestId, int PPId)
    {
        TSP.DataManager.TestObserverManager mngDelete = new TSP.DataManager.TestObserverManager();
        TSP.DataManager.TestObserverManager manager = new TSP.DataManager.TestObserverManager();
        TSP.DataManager.TransactionManager tr = new TSP.DataManager.TransactionManager();
        tr.Add(mngDelete);
        tr.Add(manager);
        if (!string.IsNullOrEmpty(PPId.ToString()) && !string.IsNullOrEmpty(TestId.ToString()))
        {
            try
            {
                tr.BeginSave();
                mngDelete.FindByTestId(TestId);
                if (mngDelete.Count > 0)
                {
                    for (int k = 0; k < mngDelete.Count; k++)
                    {
                        mngDelete[0].Delete();
                    }
                    mngDelete.Save();
                }
                for (int i = 0; i < CustomAspxDevGridView1.VisibleRowCount; i++)
                {
                    if (CustomAspxDevGridView1.Selection.IsRowSelected(i) == true)
                    {
                        DataRow row = manager.NewRow();
                        row["TestId"] = TestId;
                        DataRow GridRow = CustomAspxDevGridView1.GetDataRow(i);
                        row["ObId"] = GridRow["ObId"];
                        row["Description"] = DBNull.Value;
                        row["UserId"] = Utility.GetCurrentUser_UserId();
                        row["ModifiedDate"] = DateTime.Now;
                        manager.AddRow(row);
                    }
                }

                int cn = manager.Save();
                if (cn > 0)
                {
                    tr.EndSave();
                    TestID.Value = Utility.EncryptQS(manager[0]["TestId"].ToString());
                    //PeriodId.Value = Utility.EncryptQS(manager[0]["PPId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {
                    tr.CancelSave();
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
                        this.LabelWarning.Text = "شخص مورد نظر قبلاً به عنوان ناظر این آزمون انتخاب شده اند.";
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
    protected void Insert(int TestId, int PPId)
    {
        TSP.DataManager.TestObserverManager manager = new TSP.DataManager.TestObserverManager();
        TSP.DataManager.PeriodAttenderManager PPmanager = new TSP.DataManager.PeriodAttenderManager();
        TSP.DataManager.TransactionManager tr = new TSP.DataManager.TransactionManager();
        tr.Add(manager);
        if (!string.IsNullOrEmpty(PPId.ToString()) && !string.IsNullOrEmpty(TestId.ToString()))
        {
            try
            {
                for (int i = 0; i < CustomAspxDevGridView1.VisibleRowCount; i++)
                {
                    if (CustomAspxDevGridView1.Selection.IsRowSelected(i) == true)
                    {
                        DataRow row = manager.NewRow();
                        row["TestId"] = TestId;
                        DataRow GridRow = CustomAspxDevGridView1.GetDataRow(i);
                        row["ObId"] = GridRow["ObId"];
                        row["Description"] = DBNull.Value;
                        row["UserId"] =Utility.GetCurrentUser_UserId();
                        row["ModifiedDate"] = DateTime.Now;
                        manager.AddRow(row);
                    }
                }
                tr.BeginSave();
                int cn = manager.Save();
                if (cn > 0)
                {
                    tr.EndSave();
                    TestID.Value = Utility.EncryptQS(manager[0]["TestId"].ToString());
                    //PeriodId.Value = Utility.EncryptQS(manager[0]["PPId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {
                    tr.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام نشد";
                }
            }
            catch (Exception err)
            {
                tr.CancelSave();
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
                        this.LabelWarning.Text = "شخص مورد نظر قبلاً به عنوان ناظر این آزمون انتخاب شده اند.";
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
    protected void Active(int TestId)
    {

        TSP.DataManager.TestObserverManager managerEdit = new TSP.DataManager.TestObserverManager();
        managerEdit.FindByCode(TestId);
        if (managerEdit.Count == 1)
        {

            try
            {

                managerEdit[0].BeginEdit();
                managerEdit[0]["InActive"] = 0;
                managerEdit[0]["UserId"] = Utility.GetCurrentUser_UserId();
                managerEdit[0]["ModifiedDate"] = DateTime.Now;
                managerEdit[0].EndEdit();
                int cn = managerEdit.Save();
                if (cn == 1)
                {
                    //CrId = int.Parse(managerEdit[0]["TestId"].ToString());
                    TestID.Value = Utility.EncryptQS(managerEdit[0]["TestId"].ToString());
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
    protected void InActive(int TestId)
    {

        TSP.DataManager.TestObserverManager managerEdit = new TSP.DataManager.TestObserverManager();
        managerEdit.FindByCode(TestId);
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
                    //CrId = int.Parse(managerEdit[0]["TestId"].ToString());
                    TestID.Value = Utility.EncryptQS(managerEdit[0]["TestId"].ToString());
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
    protected void Delete(int TestId)
    {

        TSP.DataManager.TestObserverManager managerEdit = new TSP.DataManager.TestObserverManager();
        managerEdit.FindByCode(TestId);
        if (managerEdit.Count == 1)
        {
            try
            {
                managerEdit[0].Delete();


                int cn = managerEdit.Save();
                if (cn == 1)
                {
                    //TeacherId.Value = managerEdit[0]["TestId"].ToString();
                    TestID.Value = Utility.EncryptQS("");
                    PgMode.Value = Utility.EncryptQS("New");
                    ASPxRoundPanel2.HeaderText = "جدید";
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

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        //Response.Redirect("AddCourse.aspx?PageMode" + Utility.EncryptQS("New"));
        TSP.DataManager.Permission per = TSP.DataManager.TestObserverManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        TestID.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";
        ClearForm();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PPID = Utility.DecryptQS(PeriodId.Value);
        string PageMode = Utility.DecryptQS(PgMode.Value);
        string TestId = Utility.DecryptQS(TestID.Value);
        if (string.IsNullOrEmpty(PPID))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else if (string.IsNullOrEmpty(TestId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {
            if (PageMode == "New")
            {
                Insert(int.Parse(TestId), int.Parse(PPID));
                //Response.Redirect("AddCourse.aspx?TestId=" + Utility.EncryptQS(CrId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));

            }
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(TestId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    Edit(int.Parse(TestId), int.Parse(PPID));
                }

            }

        }



    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        string PPID = Utility.DecryptQS(PeriodId.Value);

        string PageMode = Utility.DecryptQS(PgMode.Value);
        string TestId = Utility.DecryptQS(TestID.Value);
        if (string.IsNullOrEmpty(PPID))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else if (string.IsNullOrEmpty(TestId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else
        {

            if (PageMode == "Edit" && (!string.IsNullOrEmpty(TestId)))
            {
                Delete(int.Parse(TestId));
            }
        }
        //Response.Redirect("AddCourse.aspx?" + PageMode + Utility.EncryptQS("New"));
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            if ((!string.IsNullOrEmpty(Server.HtmlDecode(Request.QueryString["PPId"].ToString()))) && (!string.IsNullOrEmpty(Server.HtmlDecode(Request.QueryString["TestId"]).ToString())))
                Response.Redirect("TestObserver.aspx?TestId=" + Server.HtmlDecode(Request.QueryString["TestId"]).ToString() + "&PPId=" + Server.HtmlDecode(Request.QueryString["PPId"].ToString()));
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string PPID = Utility.DecryptQS(PeriodId.Value);

        string pageMode = Utility.DecryptQS(PgMode.Value);
        string TestId = Utility.DecryptQS(TestID.Value);
        if (string.IsNullOrEmpty(PPID))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

            return;
        }
        else if (string.IsNullOrEmpty(TestId))
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
                TSP.DataManager.Permission per = TSP.DataManager.TestObserverManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                this.ViewState["BtnSave"] = btnSave.Enabled;

                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
            }

            //Response.Redirect("AddCourse.aspx?TestId=" + Utility.EncryptQS(TestId.ToString()) + "&PageMode=" + Server.HtmlDecode(Request.QueryString["Edit"]));

        }

    }

    //protected void chbInActive_CheckedChanged(object sender, EventArgs e)
    //{

    //}
    //protected void cmbMemberType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (cmbMemberType.Value.ToString() == "0")
    //    {
    //        lbMeID.Visible = false;
    //        cmbMeId.Visible = false;
    //        lbMjId.Visible = false;
    //        cmbMjId.Visible = false;
    //        lbOtpId.Visible = true;
    //        cmbOtherPerson.Visible = true;
    //    }
    //    else if (cmbMemberType.Value.ToString() == "1")
    //    {
    //        lbMeID.Visible = true;
    //        cmbMeId.Visible = true;
    //        lbMjId.Visible = false;
    //        cmbMjId.Visible = false;
    //        lbOtpId.Visible = false;
    //        cmbOtherPerson.Visible = false;
    //    }
    //    else
    //    {
    //        lbMeID.Visible = true;
    //        cmbMeId.Visible = true;
    //        lbMjId.Visible = false;
    //        cmbMjId.Visible = false;
    //        lbOtpId.Visible = false;
    //        cmbOtherPerson.Visible = false;
    //    }
    //    cmbMeId.DataBind();
    //    cmbOtherPerson.DataBind();
    //}

    //protected void BtnOtherPerson_Click(object sender, EventArgs e)
    //{
    //    if (!string.IsNullOrEmpty(Server.HtmlDecode(Request.QueryString["PPId"].ToString())))
    //        Response.Redirect("AddOtherPerson.aspx?TestId=" + Utility.EncryptQS(TestID.Value) + "&PPId=" + Server.HtmlDecode(Request.QueryString["PPId"].ToString()) + "&PageMode=" + Utility.EncryptQS(PgMode.Value));

    //}
}
