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

public partial class Employee_Amoozesh_TrainingGroups : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.TrainingGroupsManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;

            CustomAspxDevTreeList1.ClientVisible = per.CanView;

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = CustomAspxDevTreeList1.ClientVisible;

         
            CmbParent.DataBind();
            CmbParent.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));

            PgMode["col"] = "";
            GroupId["col"] = "";
            
        }
        CustomAspxDevTreeList1.DataBind();

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnView"] != null)
            this.CustomAspxDevTreeList1.ClientVisible = (bool)this.ViewState["BtnView"];
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string TgrId = GroupId["col"].ToString();
       
        if (string.IsNullOrEmpty(TgrId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        else
            Delete(int.Parse(TgrId));
      
    }
   
    protected void FillForm(int Id)
    {
        TSP.DataManager.TrainingGroupsManager Manager = new TSP.DataManager.TrainingGroupsManager();
        try
        {
            Manager.FindByCode(Id);
            if (Manager.Count == 1)
            {
                txtDesc.Text = Manager[0]["Description"].ToString();
                txtName.Text = Manager[0]["Name"].ToString();
                if (Utility.IsDBNullOrNullValue(Manager[0]["ParentId"]))
                {
                    CmbParent.DataBind();
                    CmbParent.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
                    CmbParent.SelectedIndex = 0;
                }
                else
                {
                    CmbParent.DataBind();
                    CmbParent.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
                    CmbParent.SelectedIndex = CmbParent.Items.IndexOfValue(Manager[0]["ParentId"].ToString());
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
        }
    }
    protected void Insert()
    {
        TSP.DataManager.TrainingGroupsManager GroupManager = new TSP.DataManager.TrainingGroupsManager();
        try
        {
            DataRow dr = GroupManager.NewRow();
            if (CmbParent.Value != null)
                dr["ParentId"] = CmbParent.Value;
            else
                dr["ParentId"] = DBNull.Value;
            dr["Name"] = txtName.Text;
            dr["Description"] = txtDesc.Text;
            dr["UserId"] = Utility.GetCurrentUser_UserId();
            dr["ModifiedDate"] = DateTime.Now;
            GroupManager.AddRow(dr);
            int cnt = GroupManager.Save();
            if (cnt > 0)
            {
                ClearForm();
                CustomAspxDevTreeList1.DataBind();
               

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
    protected void Edit(int Id)
    {
        TSP.DataManager.TrainingGroupsManager GroupManager = new TSP.DataManager.TrainingGroupsManager();
        try
        {
           // CmbParent.DataBind();
            GroupManager.FindByCode(Id);
            if (GroupManager.Count == 1)
            {
                GroupManager[0].BeginEdit();
                if (CmbParent.Value != null)
                    GroupManager[0]["ParentId"] = CmbParent.Value;
                else
                    GroupManager[0]["ParentId"] = DBNull.Value;
                GroupManager[0]["UserId"] =Utility.GetCurrentUser_UserId();
                GroupManager[0]["Name"] = txtName.Text;
                GroupManager[0]["Description"] = txtDesc.Text;
                GroupManager[0].EndEdit();
                if (GroupManager.Save() > 0)
                {
                    ClearForm();
                    CustomAspxDevTreeList1.DataBind();
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
                this.LabelWarning.Text = "رکورد مورد نظر را انتخاب نمایید";
                return;
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
    protected void Delete(int Id)
    {
        TSP.DataManager.TrainingGroupsManager Manager = new TSP.DataManager.TrainingGroupsManager();
        try
        {
            Manager.FindByCode(Id);
            if (Manager.Count == 1)
            {
                Manager[0].Delete();
                Manager.Save();
                CustomAspxDevTreeList1.DataBind();
                GroupId["col"] = "-1";                 
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "حذف انجام شد";
 
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "رکورد مورد نظر را انتخاب نمایید";
 
            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {

        string TgrId = GroupId["col"].ToString();


        if (string.IsNullOrEmpty(TgrId))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }
        FillForm(int.Parse(TgrId));
        ASPxPopupControl1.ShowOnPageLoad = true;
    }
    protected void ClearForm()
    {
        txtDesc.Text = "";
        txtName.Text = "";
        CmbParent.DataBind();
        CmbParent.Items.Insert(0, new DevExpress.Web.ListEditItem("--------", null));
    }
    protected void ASPxCallbackPanel1_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        ASPxCallbackPanel1.JSProperties["cpPopShow"] = 0;
        String[] Params = e.Parameter.Split(';');
        string PageMode = Params[0];
        string TgrId = Params[1];

        if (PageMode == "New")
            Insert();
        else if (PageMode == "Edit")
        {
            if (string.IsNullOrEmpty(TgrId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            Edit(int.Parse(TgrId));
        }
        else if (PageMode == "FillForm")
        {
           // string TgrId = GroupId["col"].ToString();


            if (string.IsNullOrEmpty(TgrId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            FillForm(int.Parse(TgrId));
            ASPxCallbackPanel1.JSProperties["cpPopShow"] = 1;
            //ASPxPopupControl1.ShowOnPageLoad = true;
        }
        
        CustomAspxDevTreeList1.DataBind();
    }
    protected void CustomAspxDevTreeList1_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
    {
        //CustomAspxDevTreeList1.DataBind();
    }
}
