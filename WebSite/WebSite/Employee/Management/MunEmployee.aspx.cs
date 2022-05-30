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

public partial class Employee_Management_MunEmployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            ObjdsEmployee.CacheDuration = Utility.GetCacheDuration();

            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.MunicipalityManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDisActive.Enabled = per.CanEdit;
            btnDisActive2.Enabled = per.CanEdit;
            btnEdit.Enabled = per.CanEdit;
            btnEdit1.Enabled = per.CanEdit;
            btnUserRight.Enabled = per.CanView;
            btnUserRight1.Enabled = per.CanView;
            btnNew1.Enabled = per.CanNew;
            BtnNew.Enabled = per.CanNew;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnReset.Enabled = per.CanEdit;
            btnReset1.Enabled = per.CanEdit;
            GridViewEmployee.ClientVisible = per.CanView;
         
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
           
        }
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = this.GridViewEmployee.ClientVisible = this.btnPrint.Enabled = this.btnPrint2.Enabled = this.btnPrint.Enabled = this.btnPrint2.Enabled = btnUserRight.Enabled = btnUserRight1.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit1.Enabled = this.btnReset.Enabled = this.btnDisActive2.Enabled = this.btnDisActive.Enabled = this.btnReset1.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew1.Enabled = (bool)this.ViewState["BtnNew"];
      
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        Session["DataTable"] = GridViewEmployee.Columns;
        Session["DataSource"] = ObjdsEmployee;
        Session["Title"] = "کارمندان شهرداری";
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("MunEmployeeInsert.aspx?PageMode=" + Utility.EncryptQS("New") + "&EmpId=" + Utility.EncryptQS(""));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int EmpId = -1;

        if (GridViewEmployee.FocusedRowIndex > -1)
        {

            DataRow row = GridViewEmployee.GetDataRow(GridViewEmployee.FocusedRowIndex);
            EmpId = (int)row["EmpId"];

        }
        if (EmpId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("MunEmployeeInsert.aspx?PageMode=" + Utility.EncryptQS("Edit") + "&EmpId=" + Utility.EncryptQS(EmpId.ToString()));
           
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int EmpId = -1;

        if (GridViewEmployee.FocusedRowIndex > -1)
        {

            DataRow row = GridViewEmployee.GetDataRow(GridViewEmployee.FocusedRowIndex);
            EmpId = (int)row["EmpId"];

        }
        if (EmpId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("MunEmployeeInsert.aspx?PageMode=" + Utility.EncryptQS("View") + "&EmpId=" + Utility.EncryptQS(EmpId.ToString()));

        }
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        int EmpId = -1;

        if (GridViewEmployee.FocusedRowIndex > -1)
        {

            DataRow row = GridViewEmployee.GetDataRow(GridViewEmployee.FocusedRowIndex);
            EmpId = (int)row["EmpId"];

        }
        if (EmpId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            InActive(EmpId);
        }

    }
    protected void InActive(int EmpId)
    {
        TSP.DataManager.EmployeeManager Manager = new TSP.DataManager.EmployeeManager();
        try
        {
            Manager.FindMunEmpByEmpId(EmpId);
            if (Manager.Count == 1)
            {
                if (Convert.ToBoolean(Manager[0]["EmpStatus"]))
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "کارمند مورد نظر غير فعال مى باشد"; ;
                    return;
                }

                Manager[0].BeginEdit();
                Manager[0]["EmpStatus"] = 1;
                Manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                Manager[0].EndEdit();

                int cn = Manager.Save();
                if (cn == 1)
                {
                    GridViewEmployee.DataBind();

                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "ذخیره انجام شد";
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
                this.LabelWarning.Text = "امکان تغییر اطلاعات وجود ندارد";
            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            
        }
    }
    protected void btnUserRight_Click(object sender, EventArgs e)
    {
        //string GridFilterString = GridViewEmployee.FilterExpression;

        //int EmpId = -1;
        //int UserId = -1;

        //if (GridViewEmployee.FocusedRowIndex > -1)
        //{
        //    DataRow row = GridViewEmployee.GetDataRow(GridViewEmployee.FocusedRowIndex);
        //    EmpId = (int)row["EmpId"];
        //    UserId = (int)row["LoginUserId"];

        //}
        //if (EmpId == -1)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
        //}
        //else

        //    Response.Redirect("~/Employee/Employee/UserRight1.aspx?UserId=" + Utility.EncryptQS(UserId.ToString()) + "&GrdFlt=" + Utility.EncryptQS(GridFilterString));

    }
    protected void btnResetSave_Click(object sender, EventArgs e)
    {
        int EmpId = -1;
        string RsType = "";
        if (GridViewEmployee.FocusedRowIndex > -1)
        {
            DataRow row = GridViewEmployee.GetDataRow(GridViewEmployee.FocusedRowIndex);
            EmpId = (int)row["EmpId"];
            RsType = ((int)TSP.DataManager.ResetPasswordType.MunEmployee).ToString();
        }
        if (EmpId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";
        }
        else
            Response.Redirect("~/Users/ResetPassword.aspx?ID=" + Utility.EncryptQS(EmpId.ToString()) + "&Type=" + Utility.EncryptQS(RsType));

    }
    protected void GridViewEmployee_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr";

    }
    protected void GridViewEmployee_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate" )
            e.Editor.Style["direction"] = "ltr";
    }
}
