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

public partial class Employee_SMS_SMSType : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
       
        Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");
        //this.DivReport.Attributes.Add("Style", "display:none");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            this.DivReport.Visible = true;
            TSP.DataManager.Permission Per = TSP.DataManager.SmsTypeManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = Per.CanNew;
            btnNew2.Enabled = Per.CanNew;
            btnEdit.Enabled = Per.CanEdit;
            btnEdit2.Enabled = Per.CanEdit;
            btnInActive.Enabled = Per.CanEdit;
            BtnInActive1.Enabled = Per.CanEdit;
            GridViewSMSType.Visible = Per.CanView;

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnInActive"] = BtnInActive1.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;

        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.BtnInActive1.Enabled = this.btnInActive.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void GridViewSMSType_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
        TSP.DataManager.SmsTypeManager SmsTypeManager = new TSP.DataManager.SmsTypeManager();
        TSP.DataManager.SmsTypeModifiedManager SmsTypeModifiedManager = new TSP.DataManager.SmsTypeModifiedManager();

        SmsTypeManager.Fill();
        DataRow rowType = SmsTypeManager.DataTable.Rows.Find(e.Keys["SmsTypeId"]);

        if (rowType != null)
        {
            DataTable tableTypeModified = SmsTypeModifiedManager.FindByTypeId(int.Parse(e.Keys["SmsTypeId"].ToString()));
            try
            {
                rowType.BeginEdit();
                rowType["SmsTypeName"] = e.NewValues["SmsTypeName"];
                rowType["InActive"] = e.NewValues["InActive"];
                rowType["SmsTypeDescription"] = e.NewValues["SmsTypeDescription"];
                rowType.EndEdit();
                int cn = SmsTypeManager.Save();
                GridViewSMSType.CancelEdit();
                if (cn > 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }

                GridViewSMSType.DataBind();
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

    }

    protected void GridViewSMSType_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        if (Page.IsValid)
        {
            InsertSMSType(e);

        }
    }

    protected void ObjdsSMSType_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {

    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        DataRow SmsTypeModifiedRow = GridViewSMSType.GetDataRow(GridViewSMSType.FocusedRowIndex);
        if (SmsTypeModifiedRow != null)
        {
            if ((int)SmsTypeModifiedRow["SmsTypeId"] == (int)TSP.DataManager.SMSType.AnswerOfRecieved)
            {
                DivReport.Visible = true;
                LabelWarning.Text = "امکان غیرفعال کردن این نوع خاص از پیام کوتاه وجود ندارد.";
            }
            else
            {
                TSP.DataManager.SmsTypeManager SmsTypeManager = new TSP.DataManager.SmsTypeManager();
                try
                {
                    SmsTypeManager.FindByCode(int.Parse(SmsTypeModifiedRow["SmsTypeId"].ToString()));

                    if (SmsTypeManager.Count == 1)
                    {

                        //SmsTypeManager[0].Delete();
                        SmsTypeManager[0].BeginEdit();
                        SmsTypeManager[0]["InActive"] = 1;
                        SmsTypeManager[0].EndEdit();

                        int cn = SmsTypeManager.Save();
                        if (cn > 0)
                        {
                            GridViewSMSType.DataBind();
                            DivReport.Visible = true;
                            LabelWarning.Text = "ذخیره انجام شد";
                        }
                        else
                        {
                            DivReport.Visible = true;
                            LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                        }
                    }
                    else
                    {
                        DivReport.Visible = true;
                        LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است";
                    }
                }
                catch (Exception err)
                {
                    SetError(err);
                }
            }
        }
    }

    protected void btnSmsTypeCost_Click(object sender, EventArgs e)
    {
        DataRow d = GridViewSMSType.GetDataRow(GridViewSMSType.FocusedRowIndex);
        Response.Redirect("~/Employee/SMS/SMSTypeCost.aspx?TypeId=" + Utility.EncryptQS(d["SmsTypeId"].ToString()));
    }

    protected void GridViewSMSType_CustomDataCallback(object sender, DevExpress.Web.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string[] Parameters = e.Parameters.Split(new char[] { ';' });
        string PgMd = Parameters[1];
        string VisibleIndex = Parameters[0];
        GridViewSMSType.JSProperties["cpShow"] = 0;
        if (PgMd == "Edit")
        {
            DataRow SmsTypeRow = GridViewSMSType.GetDataRow(int.Parse(VisibleIndex));
            if (SmsTypeRow != null)
            {
                int SmsTypeId = (int)SmsTypeRow["SmsTypeId"];
                if (SmsTypeId == (int)TSP.DataManager.SMSType.AnswerOfRecieved)
                {
                    e.Result = "امکان ویرایش این نوع خاص از پیام کوتاه وجود ندارد.";
                    GridViewSMSType.JSProperties["cpError"] = 1;
                    GridViewSMSType.JSProperties["cpShow"] = 1;
                }
            }
        }
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        DataRow SmsTypeModifiedRow = GridViewSMSType.GetDataRow(GridViewSMSType.FocusedRowIndex);
        if (SmsTypeModifiedRow != null)
        {
            TSP.DataManager.SmsTypeManager SmsTypeManager = new TSP.DataManager.SmsTypeManager();
            try
            {
                SmsTypeManager.FindByCode(int.Parse(SmsTypeModifiedRow["SmsTypeId"].ToString()));

                if (SmsTypeManager.Count == 1)
                {
                    SmsTypeManager[0].BeginEdit();
                    SmsTypeManager[0]["InActive"] = 0;
                    SmsTypeManager[0].EndEdit();

                    int cn = SmsTypeManager.Save();
                    if (cn > 0)
                    {
                        GridViewSMSType.DataBind();
                        DivReport.Visible = true;
                        LabelWarning.Text = "ذخیره انجام شد";
                    }
                    else
                    {
                        DivReport.Visible = true;
                        LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    }
                }
                else
                {
                    DivReport.Visible = true;
                    LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است";
                }
            }
            catch (Exception err)
            {
                SetError(err);
            }
        }
    }

    protected void GridViewSMSType_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        TSP.DataManager.SmsTypeManager SmsTypeManager = new TSP.DataManager.SmsTypeManager();
        SmsTypeManager.Fill();
        if (SmsTypeManager.Count > 0)
        {
            for (int i = 0; i < SmsTypeManager.Count; i++)
            {
                if (SmsTypeManager[i]["SmsTypeName"].ToString() == e.NewValues["SmsTypeName"].ToString())
                {
                    e.RowError = "نوع پیام کوتاه وارد شده تکراری می باشد.";
                }
            }
        }
    }
    #endregion

    #region Methods
    private void InsertSMSType(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        TSP.DataManager.SmsTypeManager SmsTypeManager = new TSP.DataManager.SmsTypeManager();
        // TSP.DataManager.SmsTypeModifiedManager SmsTypeModifiedManager = new TSP.DataManager.SmsTypeModifiedManager();

        try
        {
            DataRow d = SmsTypeManager.NewRow();
            d["SmsTypeName"] = e.NewValues["SmsTypeName"];
            if (e.NewValues["InActive"] != null)
                d["InActive"] = e.NewValues["InActive"];
            else
                d["InActive"] = false;
            d["SmsTypeDescription"] = e.NewValues["SmsTypeDescription"];
            d["UserId"] = Utility.GetCurrentUser_UserId();
            d["ModifiedDate"] = DateTime.Now;
            SmsTypeManager.AddRow(d);
            int cn = SmsTypeManager.Save();
            if (cn > 0)
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "ذخیره انجام شد";
            }
            else
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
            GridViewSMSType.CancelEdit();
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
                }
                else
                {
                    this.DivReport.Attributes.Add("Style", "display:block");
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Attributes.Add("Style", "display:block");
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }

        }
    }

    private void SetDeleteError(Exception err)
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
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
            }
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است.";
        }
    }

    private void SetError(Exception err)
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

    #endregion   
}
