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

public partial class Employee_SMS_SMSTypeCost : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (Request.QueryString["TypeId"] == null)
        {
            Response.Redirect("~/Employee/SMS/SMSType.aspx");
        }
        if (!IsPostBack)
        {
            TSP.DataManager.Permission Per = TSP.DataManager.SmsTypeModifiedManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnNew.Enabled = Per.CanNew;
            btnNew2.Enabled = Per.CanNew;          
            BtnDelete.Enabled = Per.CanDelete;
            btnDelete2.Enabled = Per.CanDelete;
            GridViewTypeCost.Visible = Per.CanView;

            if (!String.IsNullOrEmpty(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["TypeId"]))))
            {
                string SmsTypeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["TypeId"]));                
                ObjdsSmsTypeModified.SelectParameters[0].DefaultValue = SmsTypeId;
                TSP.DataManager.SmsTypeManager SmsTypeManager = new TSP.DataManager.SmsTypeManager();
                SmsTypeManager.FindByCode(int.Parse(SmsTypeId));
                RoundPanelSmsType.HeaderText = "نوع پیام کوتاه: "+SmsTypeManager[0]["SmsTypeName"].ToString();
            }


            this.ViewState["BtnDelete"] = BtnDelete.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;

        }
      
        if (this.ViewState["BtnDelete"] != null)
            this.BtnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        DeleteSmsTypeModified();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Employee/SMS/SMSType.aspx");
    }

    protected void DevGridViewTypeCost_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        if (Page.IsValid)
        {
            InsertSmsTypeModified(e);
        }
    }
    #endregion

    #region Methods
    private void InsertSmsTypeModified(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        TSP.DataManager.SmsTypeModifiedManager SmsTypeModifiedManager = new TSP.DataManager.SmsTypeModifiedManager();
        try
        {
            DataRow d = SmsTypeModifiedManager.NewRow();
            string SmsTypeId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["TypeId"]));
            d["SmsTypeId"] = SmsTypeId;
            if (e.NewValues["HasCost"] != null)
                d["HasCost"] = e.NewValues["HasCost"];
            else
                d["HasCost"] = false;
            d["ModifiedDate"] = Utility.GetDateOfToday() + " " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":" + DateTime.Now.TimeOfDay.Seconds;
            d["UserId"] =Utility.GetCurrentUser_UserId();
            SmsTypeModifiedManager.AddRow(d);
            int cn = SmsTypeModifiedManager.Save();
            GridViewTypeCost.CancelEdit();

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

    private void DeleteSmsTypeModified()
    {
        DataRow RowTypeModified = GridViewTypeCost.GetDataRow(GridViewTypeCost.FocusedRowIndex);
        TSP.DataManager.SmsTypeModifiedManager SmsTypeModifiedManager = new TSP.DataManager.SmsTypeModifiedManager();
        SmsTypeModifiedManager.FindByCode(int.Parse(RowTypeModified["TypeModifiedId"].ToString()));
        if (SmsTypeModifiedManager.Count == 1)
        {
            SmsTypeModifiedManager[0].Delete();
            int cn = SmsTypeModifiedManager.Save();
            GridViewTypeCost.DataBind();
            if (cn > 0)
            {
                DivReport.Visible = true;
                LabelWarning.Text = "ذخیره انجام شد";
            }
            else
            {
                DivReport.Visible = true;
                LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }
    #endregion

}
