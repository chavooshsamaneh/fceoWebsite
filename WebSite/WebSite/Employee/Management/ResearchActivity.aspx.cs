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

public partial class Employee_Management_ResearchActivity : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            this.DivReport.Visible = false;
            this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
            this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

            TSP.DataManager.Permission per = TSP.DataManager.ResearchActivityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            GridViewResearchAct.Visible = per.CanView;

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
        InsertResearchAtivity(e);

    }

    protected void GridViewResearchAct_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
        EditResearchActivity(e);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int RaId;
        DataRow ResearchRow = GridViewResearchAct.GetDataRow(GridViewResearchAct.FocusedRowIndex);

        if (ResearchRow != null)
        {
            RaId = int.Parse(ResearchRow["RaId"].ToString());

            DeleteResearchActivity(RaId);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region Methods
    private void InsertResearchAtivity( DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        TSP.DataManager.ResearchActivityManager ResearchActivityManager = new TSP.DataManager.ResearchActivityManager();
        

        try
        {
            DataRow ResearchRow = ResearchActivityManager.NewRow();

            ResearchRow["RaName"] = e.NewValues["RaName"];
            ResearchRow["Grade"] = e.NewValues["Grade"];
            ResearchRow["Description"] = e.NewValues["Description"];
            ResearchRow["UserId"] = Utility.GetCurrentUser_UserId();
            ResearchRow["ModifiedDate"] = DateTime.Now;

            ResearchActivityManager.AddRow(ResearchRow);

            int cn = ResearchActivityManager.Save();

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
            GridViewResearchAct.CancelEdit();
        }
        catch (Exception err)
        {
            GridViewResearchAct.CancelEdit();

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

    private void EditResearchActivity(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        TSP.DataManager.ResearchActivityManager ResearchActivityManager = new TSP.DataManager.ResearchActivityManager();
     

        try
        {
           ResearchActivityManager.FindByCode(int.Parse(e.Keys[0].ToString()));
           if (ResearchActivityManager.Count > 0)
           {
               ResearchActivityManager[0].BeginEdit();

               ResearchActivityManager[0]["RaName"] = e.NewValues["RaName"];
               ResearchActivityManager[0]["Grade"] = e.NewValues["Grade"];
               ResearchActivityManager[0]["Description"] = e.NewValues["Description"];
                ResearchActivityManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
               ResearchActivityManager[0]["ModifiedDate"] = DateTime.Now;

               ResearchActivityManager[0].EndEdit();

               int cn = ResearchActivityManager.Save();

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
           GridViewResearchAct.CancelEdit();
        }
        catch (Exception err)
        {
            GridViewResearchAct.CancelEdit();

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

    private void DeleteResearchActivity(int  RaId)
    {

        TSP.DataManager.ResearchActivityManager ResearchActivityManager = new TSP.DataManager.ResearchActivityManager();
        try
        {
            ResearchActivityManager.FindByCode(RaId);
            if (ResearchActivityManager.Count > 0)
            {
                ResearchActivityManager[0].Delete();

                int cn = ResearchActivityManager.Save();

                if (cn > 0)
                {
                    GridViewResearchAct.DataBind();
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
