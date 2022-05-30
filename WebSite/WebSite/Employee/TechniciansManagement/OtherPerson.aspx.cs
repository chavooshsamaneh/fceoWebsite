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

public partial class Employee_TechniciansManagement_OtherPerson : System.Web.UI.Page
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

            TSP.DataManager.Permission per = TSP.DataManager.OtherPersonManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
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

        Session["DataTable"] = CustomAspxDevGridView1.Columns;
        Session["DataSource"] = ODbOtherperson;
        Session["Title"] = "دیگر اشخاص";
        //Session["Header"] = "شرکت : " + lblOfName.Text;

        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = CustomAspxDevGridView1.Visible = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("OtherPersonInsert.aspx?OtpId=" + Utility.EncryptQS("") + "&PageMode=" + Utility.EncryptQS("New"));
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int OtpId = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {

            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OtpId = (int)row["OtpId"];

        }
        if (OtpId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("OtherPersonInsert.aspx?OtpId=" + Utility.EncryptQS(OtpId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));

        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int OtpId = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {

            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            OtpId = (int)row["OtpId"];

        }
        if (OtpId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("OtherPersonInsert.aspx?OtpId=" + Utility.EncryptQS(OtpId.ToString()) + "&PageMode=" + Utility.EncryptQS("View"));

        }
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        try
        {
            int OtpId = -1;

            if (CustomAspxDevGridView1.FocusedRowIndex > -1)
            {

                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                OtpId = (int)row["OtpId"];

            }
            if (OtpId == -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

            }
            else
            {
                InActive(OtpId);
            }
        }
        catch (Exception)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";

        }
    }
    protected void InActive(int OtpId)
    {
        TSP.DataManager.OtherPersonManager Manager = new TSP.DataManager.OtherPersonManager();

        Manager.FindByCode(OtpId);
        if (Manager.Count == 1)
        {
            if (Convert.ToBoolean(Manager[0]["InActive"]))
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "شخص مورد نظر غير فعال مى باشد"; ;
                return;
            }

            Manager[0].BeginEdit();
            Manager[0]["InActive"] = 1;
            Manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            Manager[0].EndEdit();

            Manager.Save();

            CustomAspxDevGridView1.DataBind();

            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";

        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "امکان تغییر اطلاعات وجود ندارد";
        }
    }
}
