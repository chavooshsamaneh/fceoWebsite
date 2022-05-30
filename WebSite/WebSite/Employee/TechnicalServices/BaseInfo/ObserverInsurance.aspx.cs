using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Employee_TechnicalServices_BaseInfo_ObserverInsurance : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ObserverInsuranceManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnActive.Enabled = per.CanEdit;
            btnActive.Enabled = per.CanEdit;
            btnInActive.Enabled = per.CanEdit;
            btnInActive.Enabled = per.CanEdit;
            GridViewObsInsurance.Visible = per.CanView;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnActive"] = btnActive.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            //GridViewObsInsurance.DataBind();

        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnActive"] != null)
            this.btnActive.Enabled = this.btnActive2.Enabled = (bool)this.ViewState["BtnActive"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        GridViewObsInsurance.JSProperties["cpMessage"] = "";

        Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        int ObsInId = -1;
        if (GridViewObsInsurance.FocusedRowIndex > -1)
        {
            try
            {
                DataRow Row = GridViewObsInsurance.GetDataRow(GridViewObsInsurance.FocusedRowIndex);
                ObsInId = Convert.ToInt32(Row["ObsInId"]);
                TSP.DataManager.TechnicalServices.ObserverInsuranceManager ObsInsuranceManager = new TSP.DataManager.TechnicalServices.ObserverInsuranceManager();
                ObsInsuranceManager.FindByCode(ObsInId);
                if (ObsInsuranceManager.Count > 0)
                {
                    DataRow EditRow = ObsInsuranceManager[0];

                    if (EditRow != null)
                    {
                        EditRow.BeginEdit();

                        EditRow["InActive"] = true;
                        EditRow["InActiveDate"] = Utility.GetDateOfToday();
                        EditRow["UserId"] = Utility.GetCurrentUser_UserId();
                        EditRow["ModifiedDate"] = DateTime.Now;
                        EditRow.EndEdit();

                        if (ObsInsuranceManager.Save() > 0)
                        {
                            GridViewObsInsurance.DataBind();
                            ShowMessage("ذخیره انجام شد.");
                        }
                        else
                        {
                            ShowMessage("خطایی در ذخیره انجام گرفته است.");
                        }
                    }
                }
                else
                {
                    ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
                }
            }
            catch (Exception err)
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                Utility.SaveWebsiteError(err);
            }
        }
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        int ObsInId = -1;
        if (GridViewObsInsurance.FocusedRowIndex > -1)
        {
            try
            {
                DataRow Row = GridViewObsInsurance.GetDataRow(GridViewObsInsurance.FocusedRowIndex);
                ObsInId = Convert.ToInt32(Row["ObsInId"]);
                TSP.DataManager.TechnicalServices.ObserverInsuranceManager ObsInsuranceManager = new TSP.DataManager.TechnicalServices.ObserverInsuranceManager();
                ObsInsuranceManager.FindByCode(ObsInId);
                if (ObsInsuranceManager.Count > 0)
                {
                    DataRow EditRow = ObsInsuranceManager[0];

                    if (EditRow != null)
                    {
                        EditRow.BeginEdit();

                        EditRow["InActive"] = false;
                        EditRow["InActiveDate"] = DBNull.Value;
                        EditRow["UserId"] = Utility.GetCurrentUser_UserId();
                        EditRow["ModifiedDate"] = DateTime.Now;
                        EditRow.EndEdit();

                        if (ObsInsuranceManager.Save() > 0)
                        {
                            GridViewObsInsurance.DataBind();
                            ShowMessage("ذخیره انجام شد.");
                        }
                        else
                        {
                            ShowMessage("خطایی در ذخیره انجام گرفته است.");
                        }
                    }
                }
                else
                {
                    ShowMessage("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.");
                }
            }
            catch (Exception err)
            {
                ShowMessage("خطایی در ذخیره انجام گرفته است.");
                Utility.SaveWebsiteError(err);
            }
        }
    }

    protected void GridViewObsInsurance_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        if (Page.IsValid)
        {
            InsertObsInsurance(e);
        }
    }

    protected void GridViewObsInsurance_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;
        EditObsInsurance(e);
    }
    #endregion

    #region Methods
    private void InsertObsInsurance(DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        TSP.DataManager.TechnicalServices.ObserverInsuranceManager ObsInsuranceManager = new TSP.DataManager.TechnicalServices.ObserverInsuranceManager();
        try
        {
            DataRow Row = ObsInsuranceManager.NewRow();
            Row["Price"] = e.NewValues["Price"].ToString();
            Row["PricePercent"] = e.NewValues["PricePercent"].ToString();
            Row["ContractStartDate"] = e.NewValues["ContractStartDate"].ToString();
            Row["ContractEndDate"] = e.NewValues["ContractEndDate"].ToString();
            Row["CreatDate"] = Utility.GetDateOfToday();
            Row["ObserversTypeId"] = e.NewValues["ObserversTypeId"].ToString();
            Row["InActive"] = false;
            Row["UserId"] = Utility.GetCurrentUser_UserId();
            Row["ModifiedDate"] = DateTime.Now;
            Row["CreatDate"] = Utility.GetDateOfToday();
            ObsInsuranceManager.AddRow(Row);
            if (ObsInsuranceManager.Save() > 0)
            {
                GridViewObsInsurance.JSProperties["cpMessage"] = "ذخیره انجام شد.";
            }
            else
            {
                GridViewObsInsurance.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است";
            }
            GridViewObsInsurance.CancelEdit();
            GridViewObsInsurance.DataBind();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            GridViewObsInsurance.CancelEdit();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    GridViewObsInsurance.JSProperties["cpMessage"] = "اطلاعات تکراری می باشد";
                }
                else if (se.Number == 2627)
                {
                    GridViewObsInsurance.JSProperties["cpMessage"] = "کد درس تکراری می باشد.";
                }
                else
                {
                    GridViewObsInsurance.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                GridViewObsInsurance.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است";
            }
        }

    }

    private void EditObsInsurance(DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            TSP.DataManager.TechnicalServices.ObserverInsuranceManager ObsInsuranceManager = new TSP.DataManager.TechnicalServices.ObserverInsuranceManager();
            ObsInsuranceManager.FindByCode(Convert.ToInt32(e.Keys[0]));
            if(ObsInsuranceManager.Count!=1)
            {
                GridViewObsInsurance.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است.";
                return;
            }
            DataRow Row = ObsInsuranceManager[0];

            if (Row != null)
            {

                Row.BeginEdit();
                Row["Price"] = e.NewValues["Price"].ToString();
                Row["PricePercent"] = e.NewValues["PricePercent"].ToString();
                Row["ContractStartDate"] = e.NewValues["ContractStartDate"].ToString();
                Row["ContractEndDate"] = e.NewValues["ContractEndDate"].ToString();
                //Row["CreatDate"] = Utility.GetDateOfToday();
                Row["ObserversTypeId"] = e.NewValues["ObserversTypeId"].ToString();
                //Row["InActive"] = false;
                Row["UserId"] = Utility.GetCurrentUser_UserId();
                Row["ModifiedDate"] = DateTime.Now;
                //Row["CreateDate"] = Utility.GetDateOfToday();

                Row.EndEdit();
                if (ObsInsuranceManager.Save() > 0)
                {
                    GridViewObsInsurance.JSProperties["cpMessage"] = "ذخیره انجام شد.";
                }
                else
                {
                    GridViewObsInsurance.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است.";
                }

            }
            GridViewObsInsurance.CancelEdit();
            GridViewObsInsurance.DataBind();
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            GridViewObsInsurance.CancelEdit();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    GridViewObsInsurance.JSProperties["cpMessage"] = "اطلاعات تکراری می باشد";
                }
                else if (se.Number == 2627)
                {
                    GridViewObsInsurance.JSProperties["cpMessage"] = "کد درس تکراری می باشد.";
                }
                else
                {
                    GridViewObsInsurance.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                GridViewObsInsurance.JSProperties["cpMessage"] = "خطایی در ذخیره انجام گرفته است";
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