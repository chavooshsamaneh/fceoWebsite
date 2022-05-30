
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
public partial class Employee_Amoozesh_AddTests : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
       
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["InsId"]))
            {
                Response.Redirect("Test.aspx");
                return;
            }

            TSP.DataManager.Permission per = TSP.DataManager.TestManger.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanEdit || per.CanNew;

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                TestId.Value = Server.HtmlDecode(Request.QueryString["TestID"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            string PageMode = Utility.DecryptQS(PgMode.Value);
            string TestID = Utility.DecryptQS(TestId.Value);
            //if ((!string.IsNullOrEmpty(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["TestID"]).ToString()))) && (CrId == -1))
            //    //CrId = int.Parse(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["TestID"]).ToString()));
            if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
            //if (!string.IsNullOrEmpty(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PPId"].ToString()))))
            //{
            //    string PPId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PPId"].ToString()));
            //    TSP.DataManager.PeriodPresentManager PPmanager = new TSP.DataManager.PeriodPresentManager();
            //    PPmanager.FindByCode(int.Parse(PPId));
            //    if (PPmanager.Count == 1)
            //    {
            //        if (PPmanager[0]["Type"].ToString() =='0')
            //            cmbTypeOfReg.Visible = true;
            //        else if (PPmanager[0]["Type"].ToString() == "1")
            //        {
            //            cmbTypeOfReg.Visible = false;
            //            cmbTypeOfReg.SelectedIndex = cmbTypeOfReg.Items.IndexOfValue(2);
            //        }
            //    }
            //}
            //else
            //{
            //    Response.Redirect("../../ErrorPage.aspx?ErrorType=1");
            //    return;
            //}
            switch (PageMode)
            {
                case "View":
                    Disable();

                    if (string.IsNullOrEmpty(TestID))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    btnEdit.Enabled = per.CanEdit;
                    btnEdit2.Enabled = per.CanEdit;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    FillForm(int.Parse(TestID));
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    break;


                case "New":
                    Enable();
                    ASPxRoundPanel2.HeaderText = "جدید";
                    btnDelete.Enabled = false;
                    btnDelete2.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;

                    ClearForm();

                    chbInActive.Checked = false;
                    break;
                case "Edit":
                    Enable();

                    if (string.IsNullOrEmpty(TestID))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    btnDelete.Enabled = per.CanDelete;
                    btnDelete2.Enabled = per.CanDelete;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;

                    FillForm(int.Parse(TestID));
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

    protected void FillForm(int TestID)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        TSP.DataManager.TestManger manager = new TSP.DataManager.TestManger();

        if (!string.IsNullOrEmpty(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PPId"].ToString()))))
        {
            manager.FindByCode(TestID);

            txtTestCode.Text = manager[0]["TestCode"].ToString();
            txtDate.Text = manager[0]["Date"].ToString();
            string sTime = manager[0]["StartTime"].ToString();
            if (!string.IsNullOrEmpty(sTime))
            {
                if ((sTime.Length == 5) && sTime.IndexOf(":") == 2)
                    txtHour.Text = sTime.Substring(0, 2);
                txtMin.Text = sTime.Substring(3, 2);

            }
            string eTime = manager[0]["EndTime"].ToString();
            if (!string.IsNullOrEmpty(sTime))
            {
                if ((sTime.Length == 5) && eTime.IndexOf(":") == 2)
                    txtHourE.Text = eTime.Substring(0, 2);
                txtMinE.Text = eTime.Substring(3, 2);
            }
            //txtSatrtTime.Text = manager[0]["StartTime"].ToString();
            //txtEndTime.Text = manager[0]["EndTIme"].ToString();
            txtPlace.Text = manager[0]["Place"].ToString();
            txtDesc.Text = manager[0]["Description"].ToString();
            chbInActive.Checked = Convert.ToBoolean(manager[0]["InActive"].ToString());

        }

    }
    protected void ClearForm()
    {

        txtDate.Text = "";
        for (int i = 0; i < ASPxRoundPanel2.Controls.Count; i++)
        {
            if (ASPxRoundPanel2.Controls[i] is DevExpress.Web.ASPxTextBox)
            {
                DevExpress.Web.ASPxTextBox co = (DevExpress.Web.ASPxTextBox)ASPxRoundPanel2.Controls[i];
                co.Text = "";
            }
            else if (ASPxRoundPanel2.Controls[i] is DevExpress.Web.ASPxMemo)
            {
                DevExpress.Web.ASPxMemo co = (DevExpress.Web.ASPxMemo)ASPxRoundPanel2.Controls[i];
                co.Text = "";
            }


        }


    }
    protected void Disable()
    {


        txtTestCode.Enabled = false;
        txtDate.Enabled = false;
        //txtSatrtTime.Enabled = false;
        //txtEndTime.Enabled = false;
        txtPlace.Enabled = false;
        txtDesc.Enabled = false;
        chbInActive.Enabled = false;
    }
    protected void Enable()
    {
        txtTestCode.Enabled = true;
        txtDate.Enabled = true;
        //txtSatrtTime.Enabled = true;
        //txtEndTime.Enabled = true;
        txtPlace.Enabled = true;
        txtDesc.Enabled = true;
        chbInActive.Enabled = true;



    }
    protected void Edit(int TestID)
    {

        TSP.DataManager.TestManger manager = new TSP.DataManager.TestManger();
        manager.FindByCode(TestID);
        if (manager.Count == 1)
        {

            try
            {

                manager[0].BeginEdit();
                manager[0]["TestCode"] = txtTestCode.Text;
                manager[0]["Date"] = txtDate.Text;
                manager[0]["StartTime"] = txtHour.Text.PadLeft(2, '0') + ":" + txtMin.Text.PadLeft(2, '0');
                manager[0]["EndTIme"] = txtHourE.Text.PadLeft(2, '0') + ":" + txtMinE.Text.PadLeft(2, '0');

                manager[0]["Place"] = txtPlace.Text;
                manager[0]["Description"] = txtDesc.Text;
                manager[0]["InActive"] = chbInActive.Checked;
                manager[0]["UserId"] =Utility.GetCurrentUser_UserId();
                manager[0]["ModifiedDate"] = DateTime.Now;
                manager[0].EndEdit();

                int cn = manager.Save();
                if (cn == 1)
                {
                    //CrId = int.Parse(manager[0]["TestID"].ToString());
                    TestId.Value = Utility.EncryptQS(manager[0]["TestID"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";

                    TSP.DataManager.Permission per = TSP.DataManager.TestManger.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                    btnDelete.Enabled = per.CanDelete;
                    btnDelete2.Enabled = per.CanDelete;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    this.ViewState["BtnEdit"] = btnEdit.Enabled;
                    this.ViewState["BtnDelete"] = btnDelete.Enabled;
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
                        this.LabelWarning.Text = "کد آزمون تکراری می باشد";
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
        TSP.DataManager.TestManger manager = new TSP.DataManager.TestManger();
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

            try
            {
                DataRow row = manager.NewRow();
                row["PPId"] = int.Parse(PPId);
                row["TestCode"] = txtTestCode.Text;
                row["Date"] = txtDate.Text;
                row["StartTime"] = txtHour.Text.PadLeft(2, '0') + ":" + txtMin.Text.PadLeft(2, '0');
                row["EndTIme"] = txtHourE.Text.PadLeft(2, '0') + ":" + txtMinE.Text.PadLeft(2, '0');
                row["Place"] = txtPlace.Text;
                row["Description"] = txtDesc.Text;
                row["InActive"] = (chbInActive.Checked);
                row["UserId"] = Utility.GetCurrentUser_UserId();
                row["ModifiedDate"] = DateTime.Now;
                manager.AddRow(row);

                int cn = manager.Save();
                if (cn == 1)
                {
                    //CrId = int.Parse(manager[0]["TestID"].ToString());
                    TestId.Value = Utility.EncryptQS(manager[0]["TestID"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                    TSP.DataManager.Permission per = TSP.DataManager.TestManger.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                    btnDelete.Enabled = per.CanDelete;
                    btnDelete2.Enabled = per.CanDelete;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;
                    this.ViewState["BtnEdit"] = btnEdit.Enabled;
                    this.ViewState["BtnDelete"] = btnDelete.Enabled;

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
                        this.LabelWarning.Text = "کد آزمون تکراری می باشد";
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
    protected void Active(int TestID)
    {

        TSP.DataManager.TestManger managerEdit = new TSP.DataManager.TestManger();
        managerEdit.FindByCode(TestID);
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
                    //CrId = int.Parse(managerEdit[0]["TestID"].ToString());
                    TestId.Value = Utility.EncryptQS(managerEdit[0]["TestID"].ToString());
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
    protected void InActive(int TestID)
    {

        TSP.DataManager.TestManger managerEdit = new TSP.DataManager.TestManger();
        managerEdit.FindByCode(TestID);
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
                    //CrId = int.Parse(managerEdit[0]["TestID"].ToString());
                    TestId.Value = Utility.EncryptQS(managerEdit[0]["TestID"].ToString());
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
    protected void Delete(int TestID)
    {

        TSP.DataManager.TestManger managerEdit = new TSP.DataManager.TestManger();
        managerEdit.FindByCode(TestID);
        if (managerEdit.Count == 1)
        {
            try
            {
                managerEdit[0].Delete();


                int cn = managerEdit.Save();
                if (cn == 1)
                {
                    //TeacherId.Value = managerEdit[0]["TestID"].ToString();
                    TestId.Value = Utility.EncryptQS("");
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
        TestId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";

        TSP.DataManager.Permission per = TSP.DataManager.TestManger.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnDelete.Enabled = false;
        btnDelete2.Enabled = false;
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;

        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnDelete"] = btnDelete.Enabled;
        ClearForm();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        string TestID = Utility.DecryptQS(TestId.Value);

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

                //Response.Redirect("AddCourse.aspx?TestID=" + Utility.EncryptQS(CrId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));

            }
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(TestID))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    Edit(int.Parse(TestID));
                }

            }

        }



    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        string PageMode = Utility.DecryptQS(PgMode.Value);
        string TestID = Utility.DecryptQS(TestId.Value);

        if (string.IsNullOrEmpty(TestID))
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

            if (PageMode == "Edit" && (!string.IsNullOrEmpty(TestID)))
            {
                Delete(int.Parse(TestID));
            }
        }
        //Response.Redirect("AddCourse.aspx?" + PageMode + Utility.EncryptQS("New"));
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(Server.HtmlDecode(Request.QueryString["PPId"].ToString())))
                Response.Redirect("Test.aspx?PPId=" + Server.HtmlDecode(Request.QueryString["PPId"].ToString()));
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
        string TestID = Utility.DecryptQS(TestId.Value);

        if (string.IsNullOrEmpty(TestID))
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

                TSP.DataManager.Permission per = TSP.DataManager.TestManger.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                this.ViewState["BtnSave"] = btnSave.Enabled;

                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
            }


            //Response.Redirect("AddCourse.aspx?TestID=" + Utility.EncryptQS(TestID.ToString()) + "&PageMode=" + Server.HtmlDecode(Request.QueryString["Edit"]));

        }

    }

    protected void chbInActive_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void BtnOtherPerson_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(Server.HtmlDecode(Request.QueryString["PPId"].ToString())))
                Response.Redirect("AddOtherPerson.aspx?TestID=" + Utility.EncryptQS(TestId.Value) + "&PPId=" + Server.HtmlDecode(Request.QueryString["PPId"].ToString()) + "&PageMode=" + Utility.EncryptQS(PgMode.Value));
        }
        catch
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

    }
}
