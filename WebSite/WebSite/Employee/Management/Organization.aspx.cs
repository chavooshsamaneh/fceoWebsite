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

public partial class Employee_Management_Organization : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

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
        if (!IsPostBack)
        {
            
            TSP.DataManager.Permission per = TSP.DataManager.OrganizationManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnInActive.Enabled = per.CanEdit;
            btnInActive2.Enabled = per.CanEdit;
            CustomAspxDevGridView1.Visible = per.CanView;

           
            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }

        //Session["DataTable"] = CustomAspxDevGridView1.Columns;
        //Session["DataSource"] = ODbOfAgent;
        //Session["Title"] = "شعبه ها";
        //Session["Header"] = "شرکت : " + lblOfName.Text;

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("OrganizationInsert.aspx?OrgId=" + Utility.EncryptQS("") + "&PageMode=" + Utility.EncryptQS("New"));
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int OrgId = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {

            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OrgId = (int)row["OrgId"];
          
        }
        if (OrgId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("OrganizationInsert.aspx?OrgId=" + Utility.EncryptQS(OrgId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));
           
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int OrgId = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {

            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OrgId = (int)row["OrgId"];

        }
        if (OrgId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("OrganizationInsert.aspx?OrgId=" + Utility.EncryptQS(OrgId.ToString()) + "&PageMode=" + Utility.EncryptQS("View"));
        }
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        try
        {
            int OrgId = -1;

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                OrgId = (int)row["OrgId"];

            }
            if (OrgId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

            }
            else
            {
                InActive(OrgId);
            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";

        }
    }
    protected void InActive(int OrgId)
    {
        TSP.DataManager.OrganizationManager Manager = new TSP.DataManager.OrganizationManager();

        Manager.FindByCode(OrgId);
        if (Manager.Count == 1)
        {
            if (Convert.ToBoolean(Manager[0]["InActive"]))
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "سازمان مورد نظر غير فعال مى باشد"; ;
                return;
            }

            Manager[0].BeginEdit();
            Manager[0]["InActive"] = 1;
            Manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            Manager[0].EndEdit();

            int cn = Manager.Save();
            if (cn == 1)
            {
                CustomAspxDevGridView1.DataBind();

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
    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr";
    }
    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
       
        if (e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";
    }

}
