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
using DevExpress.Web;

public partial class Employee_TechnicalServices_Plan_PlanControler : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ControlerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
           
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnActive.Enabled = per.CanEdit;
            btnActive2.Enabled = per.CanEdit;
            btnInActive.Enabled = per.CanEdit;
            btnInActive2.Enabled = per.CanEdit;

            GridViewControler.Visible = per.CanView;

            ObjdsContrler.SelectParameters["Type"].DefaultValue = ((int)TSP.DataManager.TSControlerType.Nezam).ToString();

            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnEdit"] = btnActive.Enabled;         
            this.ViewState["btnView"] = btnView.Enabled;
        }

        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
      
        if (this.ViewState["btnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["btnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnActive.Enabled = this.btnActive.Enabled = this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnEdit"];
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        NextPage("New");
    }   

    protected void btnView_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        NextPage("View");
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.TechnicalServices.ControlerManager ControlerManager = new TSP.DataManager.TechnicalServices.ControlerManager();
        try
        {            
            if (GridViewControler.FocusedRowIndex > -1)
            {
                DataRow row = GridViewControler.GetDataRow(GridViewControler.FocusedRowIndex);
                int ControlerId = (int)row["ControlerId"];
                ControlerManager.FindByControlerId(ControlerId);
                if (ControlerManager.Count == 1)
                {
                    ControlerManager[0]["InActive"] = 1;
                    ControlerManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    ControlerManager[0]["ModifiedDate"] = DateTime.Now;
                    if (ControlerManager.Save() > 0)
                    {
                        SetLabelWarning("ذخیره انجام شد.");
                    }
                    else
                    {
                        SetLabelWarning("خطایی در ذخیره انجام گرفته است.");
                    }
                }
                else
                {
                    SetLabelWarning("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است");
                }
            }
            else
            {
                SetLabelWarning("لطفاً ابتدا یک رکورد را انتخاب نمائید");
            }
            GridViewControler.DataBind();
        }
        catch (Exception err)
        {
            SetError(err); 
        }
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        TSP.DataManager.TechnicalServices.ControlerManager ControlerManager = new TSP.DataManager.TechnicalServices.ControlerManager();
        try
        {            
            if (GridViewControler.FocusedRowIndex > -1)
            {
                DataRow row = GridViewControler.GetDataRow(GridViewControler.FocusedRowIndex);
                int ControlerId = (int)row["ControlerId"];
                ControlerManager.FindByControlerId(ControlerId);
                if (ControlerManager.Count == 1)
                {
                    ControlerManager[0]["InActive"] = 0;
                    ControlerManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    ControlerManager[0]["ModifiedDate"] = DateTime.Now;
                    if (ControlerManager.Save() > 0)
                    {
                        SetLabelWarning( "ذخیره انجام شد.");
                    }
                    else
                    {
                        SetLabelWarning("خطایی در ذخیره انجام گرفته است.");
                    }
                }
                else
                {
                    SetLabelWarning("اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است");
                }
            }
            else
            {
                SetLabelWarning( "لطفاً ابتدا یک رکورد را انتخاب نمائید");
            }
            GridViewControler.DataBind();
        }
        catch (Exception err)
        {
            SetError(err);
        }
    }

    protected void btntemp_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        GridViewExporter.FileName = "ComplainsList";
        GridViewExporter.WriteXlsToResponse(true);
    }

    /*************************************************************************************************************/
    protected void GridViewControler_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        switch (e.DataColumn.FieldName)
        {
            case "Date":
                e.Cell.Style["direction"] = "ltr";
                break;
            case "FileNo":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewControler_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "FileNo":
                e.Editor.Style["direction"] = "ltr";
                e.Editor.Attributes["onkeyup"] = "return ltr_override(event)";
                break;
            case "Date":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewControler_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (e.Parameters == "Print")
        {
            Session["DataTable"] = GridViewControler.Columns;
            Session["DataSource"] = ObjdsContrler;
            Session["Title"] = "کنترل کنندگان نقشه";
            GridViewControler.DetailRows.CollapseAllRows();
            GridViewControler.JSProperties["cpDoPrint"] = 1;
        }
    }
    #endregion
    /*************************************************************************************************************/
    #region Methods
    private void NextPage(string Mode)
    {
        int ControlerId = -1;
        int focucedIndex = -1;
        focucedIndex = GridViewControler.FocusedRowIndex;
        if (focucedIndex > -1)
        {
            DataRow row = GridViewControler.GetDataRow(focucedIndex);
            ControlerId = (int)row["ControlerId"];
        }
        if (ControlerId == -1 && Mode != "New")
        {
            SetLabelWarning("لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید");
        }
        else
        {
            if (Mode == "New")
            {
                ControlerId = -1;
                Response.Redirect("AddPlanControler.aspx?CntId=" + Utility.EncryptQS(ControlerId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
            }
            else
            {
                Response.Redirect("AddPlanControler.aspx?CntId=" + Utility.EncryptQS(ControlerId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
            }
        }
    }

    private void SetError(Exception err)
    {
        if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        {
            System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

            if (se.Number == 2601)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 2627)
            {
                SetLabelWarning("اطلاعات تکراری می باشد");
            }
            else if (se.Number == 547)
            {
                SetLabelWarning("اطلاعات وابسته معتبر نمی باشد");
            }
            else
            {
                SetLabelWarning("خطایی در ذخیره انجام گرفته است");
            }
        }
        else
        {
            SetLabelWarning("خطایی در ذخیره انجام گرفته است");
        }
    }

    private void SetLabelWarning(string Warning)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Warning;
    }
    #endregion
}
