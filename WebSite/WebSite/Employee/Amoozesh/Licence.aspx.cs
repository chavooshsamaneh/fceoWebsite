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

public partial class Employee_Amoozesh_Licence : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            this.DivReport.Visible = false;
            this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
            this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

            TSP.DataManager.Permission per = TSP.DataManager.LicenceManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            GridViewLicence.Visible = per.CanView;
            
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
       
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }

    protected void GridViewResearchAct_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        InsertLicence(e);
    }

    protected void GridViewResearchAct_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
        EditLicence(e);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int LiId;
        DataRow LicenceRow = GridViewLicence.GetDataRow(GridViewLicence.FocusedRowIndex);

        if (LicenceRow != null)
        {
            LiId = int.Parse(LicenceRow["LiId"].ToString());

            DeleteLicence(LiId);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region Methods

    private void InsertLicence(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        TSP.DataManager.LicenceManager LicenceManager = new TSP.DataManager.LicenceManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        LoginManager.FindByCode(Utility.GetCurrentUser_UserId());

        if (LoginManager.Count < 0)
        {
            Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            return;
        }

        try
        {
            DataRow ResearchRow = LicenceManager.NewRow();

            ResearchRow["LiName"] = e.NewValues["LiName"];
            ResearchRow["LiCode"] = e.NewValues["LiCode"];
            ResearchRow["Grade"] = e.NewValues["Grade"];
            ResearchRow["Description"] = e.NewValues["Description"];
            ResearchRow["UserId"] = (int)(LoginManager[0]["MeId"]);
            ResearchRow["ModifiedDate"] = DateTime.Now;

            LicenceManager.AddRow(ResearchRow);

            int cn = LicenceManager.Save();

            if (cn > 0)
            {

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد.";
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";

            }
            GridViewLicence.CancelEdit();
        }
        catch (Exception err)
        {
            GridViewLicence.CancelEdit();

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
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                }
            }
        }
    }

    private void EditLicence(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        TSP.DataManager.LicenceManager LicenceManager = new TSP.DataManager.LicenceManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();

        LoginManager.FindByCode(Utility.GetCurrentUser_UserId());

        if (LoginManager.Count < 0)
        {
            Response.Redirect("../../Login.aspx?ReferPage=" + Request.Url.AbsoluteUri);
            return;
        }

        try
        {
            LicenceManager.FindByCode(int.Parse(e.Keys[0].ToString()));
            if (LicenceManager.Count > 0)
            {
                LicenceManager[0].BeginEdit();

                LicenceManager[0]["LiName"] = e.NewValues["LiName"];
                LicenceManager[0]["LiCode"] = e.NewValues["LiCode"];
                LicenceManager[0]["Grade"] = e.NewValues["Grade"];
                LicenceManager[0]["Description"] = e.NewValues["Description"];
                LicenceManager[0]["UserId"] = (int)(LoginManager[0]["MeId"]);
                LicenceManager[0]["ModifiedDate"] = DateTime.Now;

                LicenceManager[0].EndEdit();

                int cn = LicenceManager.Save();

                if (cn > 0)
                {

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";

                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
            GridViewLicence.CancelEdit();
        }
        catch (Exception err)
        {
            GridViewLicence.CancelEdit();

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
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
                }
            }
        }
    }

    private void DeleteLicence(int LiId)
    {
        TSP.DataManager.LicenceManager LicenceManager = new TSP.DataManager.LicenceManager();
        try
        {
            LicenceManager.FindByCode(LiId);
            if (LicenceManager.Count > 0)
            {
                LicenceManager[0].Delete();

                int cn = LicenceManager.Save();

                if (cn > 0)
                {
                    GridViewLicence.DataBind();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "حذف انجام شد.";
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
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
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
    #endregion
}
