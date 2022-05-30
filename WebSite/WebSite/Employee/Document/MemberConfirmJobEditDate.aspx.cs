using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Employee_Document_MemberConfirmJobEditDate : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            TSP.DataManager.Permission per = TSP.DataManager.DocMemberFileJobConfirmationManager.GetUserPermissionEditJobConfirmDate(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            GridViewJobConfirm.Visible = per.CanView;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
 
        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
      
        GridViewJobConfirm.JSProperties["cpMessage"] = "";

        Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void GridViewJobConfirm_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
        EditDocJobConfirmation(e);
    }
    #endregion

    #region Methods

    private void EditDocJobConfirmation(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            TSP.DataManager.DocMemberFileJobConfirmationManager DocMemberFileJobConfirmationManager = new TSP.DataManager.DocMemberFileJobConfirmationManager();
            DocMemberFileJobConfirmationManager.FindByCode(Convert.ToInt32(e.Keys[0]));
            if (DocMemberFileJobConfirmationManager.Count != 1)
            {
                GridViewJobConfirm.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است.";
                return;
            }
            DataRow Row = DocMemberFileJobConfirmationManager[0];

            if (Row != null)
            {

                Row.BeginEdit();
                Row["FromDate"] = e.NewValues["FromDate"].ToString();
                Row["ToDate"] = e.NewValues["ToDate"].ToString();
                Row["UserId"] = Utility.GetCurrentUser_UserId();
                Row["ModifiedDate"] = DateTime.Now;
                Row.EndEdit();
                if (DocMemberFileJobConfirmationManager.Save() > 0)
                {
                    GridViewJobConfirm.JSProperties["cpMessage"] = "ذخیره انجام شد.";
                }
                else
                {
                    GridViewJobConfirm.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است.";
                }

            }
            GridViewJobConfirm.CancelEdit();
            GridViewJobConfirm.DataBind();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            GridViewJobConfirm.CancelEdit();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    GridViewJobConfirm.JSProperties["cpMessage"] = "اطلاعات تکراری می باشد";
                }
                else if (se.Number == 2627)
                {
                    GridViewJobConfirm.JSProperties["cpMessage"] = "کد درس تکراری می باشد.";
                }
                else
                {
                    GridViewJobConfirm.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                GridViewJobConfirm.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
}