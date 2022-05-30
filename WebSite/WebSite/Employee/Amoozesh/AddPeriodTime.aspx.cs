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

public partial class Employee_Amoozesh_AddPeriodTime : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        TSP.DataManager.PeriodPresentManager PeriodManager = new TSP.DataManager.PeriodPresentManager();
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
        PeriodManager.FindByCode(int.Parse(PPId));
        if (PeriodManager.Count == 1)
            txtPeriodName.Text = PeriodManager[0]["PeriodTitle"].ToString();


        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["PageMode"]) || string.IsNullOrEmpty(Request.QueryString["PTTId"]))
            {
                Response.Redirect("PeriodTime.aspx");
                return;
            }

            TSP.DataManager.Permission per = TSP.DataManager.PeriodTimeTableManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnSave.Enabled = per.CanEdit || per.CanNew;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            try
            {
                PgMode.Value = Server.HtmlDecode(Request.QueryString["PageMode"].ToString());
                PeriodTimeId.Value = Server.HtmlDecode(Request.QueryString["PTTId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string PageMode = Utility.DecryptQS(PgMode.Value);
            string PTTId = Utility.DecryptQS(PeriodTimeId.Value);
              if (string.IsNullOrEmpty(PageMode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }
           
            switch (PageMode)
            {
                case "View":
                    Disable();

                    if (string.IsNullOrEmpty(PTTId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    btnEdit.Enabled = per.CanEdit;
                    btnEdit2.Enabled = per.CanEdit;
                    btnSave.Enabled = false;
                    btnSave2.Enabled = false;
                    FillForm(int.Parse(PTTId));
                    ASPxRoundPanel2.HeaderText = "مشاهده";
                    break;


                case "New":
                    Enable();

                    ComboDay.SelectedIndex = 0;

                    ASPxRoundPanel2.HeaderText = "جدید";
                    btnDelete.Enabled = false;
                    btnDelete2.Enabled = false;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;

                    ClearForm();
                    break;
                case "Edit":
                    Enable();

                    if (string.IsNullOrEmpty(PTTId))
                    {
                        this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                        return;
                    }
                    btnDelete.Enabled = per.CanDelete;
                    btnDelete2.Enabled = per.CanDelete;
                    btnEdit2.Enabled = false;
                    btnEdit.Enabled = false;

                    FillForm(int.Parse(PTTId));
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

    protected void FillForm(int PTTId)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        TSP.DataManager.PeriodPresentManager PeriodManager = new TSP.DataManager.PeriodPresentManager();
        TSP.DataManager.PeriodTimeTableManager manager = new TSP.DataManager.PeriodTimeTableManager();

        if (!string.IsNullOrEmpty(Request.QueryString["PPId"]))
        {
            manager.FindByCode(PTTId);

            txtDesc.Text = manager[0]["Description"].ToString();
          
            ComboDay.DataBind();
            ComboDay.SelectedIndex = ComboDay.Items.IndexOfValue(manager[0]["Day"].ToString());
           

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
           
        }

    }
    protected void ClearForm()
    {
        txtDesc.Text = "";
        txtHour.Text = "0";
        txtHourE.Text = "0";
        txtMin.Text = "0";
        txtMinE.Text = "0";
       
        ComboDay.DataBind();
        //TSP.DataManager.PeriodPresentManager PeriodManager = new TSP.DataManager.PeriodPresentManager();
        //string PPId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PPId"].ToString()));
        //PeriodManager.FindByCode(int.Parse(PPId));
        //if (PeriodManager.Count == 1)
        //    txtPeriodName.Text = PeriodManager[0]["PeriodTitle"].ToString();

    }
    protected void Disable()
    {
        txtPeriodName.Enabled = false;
        txtHour.Enabled = false;
        txtHourE.Enabled = false;
        txtMin.Enabled = false;
        txtMinE.Enabled = false;
        txtDesc.Enabled = false;
        ComboDay.Enabled = false;
       
    }
    protected void Enable()
    {
        txtPeriodName.Enabled = true;
        txtHour.Enabled = true;
        txtHourE.Enabled = true;
        txtMin.Enabled = true;
        txtMinE.Enabled = true;
        txtDesc.Enabled = true;
        ComboDay.Enabled = true;
      
    }
    protected void Edit(int PTTId)
    {

        TSP.DataManager.PeriodTimeTableManager manager = new TSP.DataManager.PeriodTimeTableManager();
        manager.FindByCode(PTTId);
        if (manager.Count == 1)
        {

            try
            {

                manager[0].BeginEdit();
               // manager[0]["PPId"] = 
               
                manager[0]["StartTime"] = txtHour.Text.PadLeft(2, '0') + ":" + txtMin.Text.PadLeft(2, '0');
                manager[0]["EndTime"] = txtHourE.Text.PadLeft(2, '0') + ":" + txtMinE.Text.PadLeft(2, '0');
                manager[0]["Day"] = ComboDay.Value;              
                manager[0]["Description"] = txtDesc.Text;               
                manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                manager[0]["ModifiedDate"] = DateTime.Now;
                manager[0].EndEdit();

                int cn = manager.Save();
                if (cn == 1)
                {
                    PeriodTimeId.Value = Utility.EncryptQS(manager[0]["PTTId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";

                    TSP.DataManager.Permission per = TSP.DataManager.PeriodTimeTableManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

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
        TSP.DataManager.PeriodTimeTableManager manager = new TSP.DataManager.PeriodTimeTableManager();
        if (!string.IsNullOrEmpty(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["PPId"]))))
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
                row["PPId"] = int.Parse(PPId);
             
                row["Day"] = ComboDay.Value;
                row["StartTime"] = txtHour.Text.PadLeft(2, '0') + ":" + txtMin.Text.PadLeft(2, '0');
                row["EndTIme"] = txtHourE.Text.PadLeft(2, '0') + ":" + txtMinE.Text.PadLeft(2, '0');
              
                row["Description"] = txtDesc.Text;
               
                row["UserId"] = Utility.GetCurrentUser_UserId();
                row["ModifiedDate"] = DateTime.Now;
                manager.AddRow(row);

                int cn = manager.Save();
                if (cn == 1)
                {
                    PeriodTimeId.Value = Utility.EncryptQS(manager[0]["PTTId"].ToString());
                    PgMode.Value = Utility.EncryptQS("Edit");
                    ASPxRoundPanel2.HeaderText = "ویرایش";
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";

                    TSP.DataManager.Permission per = TSP.DataManager.PeriodTimeTableManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

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
   
    protected void Delete(int PTTId)
    {

        TSP.DataManager.PeriodTimeTableManager managerEdit = new TSP.DataManager.PeriodTimeTableManager();
        managerEdit.FindByCode(PTTId);
        if (managerEdit.Count == 1)
        {
            try
            {
                managerEdit[0].Delete();


                int cn = managerEdit.Save();
                if (cn == 1)
                {
                    PeriodTimeId.Value = Utility.EncryptQS("");
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
       
        PeriodTimeId.Value = Utility.EncryptQS("");
        PgMode.Value = Utility.EncryptQS("New");
        ASPxRoundPanel2.HeaderText = "جدید";

        TSP.DataManager.Permission per = TSP.DataManager.PeriodTimeTableManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        btnDelete.Enabled = false;
        btnDelete2.Enabled = false;
        btnEdit2.Enabled = false;
        btnEdit.Enabled = false;
        btnSave.Enabled = per.CanNew;
        btnSave2.Enabled = per.CanNew;
        this.ViewState["BtnSave"] = btnSave.Enabled;
        this.ViewState["BtnEdit"] = btnEdit.Enabled;
        this.ViewState["BtnDelete"] = btnDelete.Enabled;

        ComboDay.SelectedIndex = 0;
        Enable();
        ClearForm();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PageMode = Utility.DecryptQS(PgMode.Value);

        string PTTId = Utility.DecryptQS(PeriodTimeId.Value);

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

                //Response.Redirect("AddCourse.aspx?PeriodTimeId=" + Utility.EncryptQS(CrId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));

            }
            else if (PageMode == "Edit")
            {

                if (string.IsNullOrEmpty(PTTId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                    return;
                }
                else
                {
                    Edit(int.Parse(PTTId));
                }

            }

        }



    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        string PageMode = Utility.DecryptQS(PgMode.Value);
        string PTTId = Utility.DecryptQS(PeriodTimeId.Value);

        if (string.IsNullOrEmpty(PTTId))
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

            if (PageMode == "Edit" && (!string.IsNullOrEmpty(PTTId)))
            {
                Delete(int.Parse(PTTId));
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(Server.HtmlDecode(Request.QueryString["PPId"].ToString())))
                Response.Redirect("PeriodTime.aspx?PPId=" + Server.HtmlDecode(Request.QueryString["PPId"].ToString()));
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
        string PTTId = Utility.DecryptQS(PeriodTimeId.Value);

        if (string.IsNullOrEmpty(PTTId))
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

                TSP.DataManager.Permission per = TSP.DataManager.PeriodTimeTableManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

                btnDelete.Enabled = per.CanDelete;
                btnDelete2.Enabled = per.CanDelete;
                btnSave.Enabled = per.CanEdit;
                btnSave2.Enabled = per.CanEdit;
                this.ViewState["BtnSave"] = btnSave.Enabled;
                this.ViewState["BtnDelete"] = btnDelete.Enabled;

                PgMode.Value = Utility.EncryptQS("Edit");
                ASPxRoundPanel2.HeaderText = "ویرایش";
            }

        }

    }

   
}
