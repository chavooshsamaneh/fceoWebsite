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

public partial class Employee_Groups : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
     
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.GroupManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDelete.Enabled = per.CanView;
            btnDelete2.Enabled = per.CanView;

            CustomAspxDevGridView1.Visible = per.CanView;

            this.ViewState["BtnView"] = btnView.Enabled;
            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;

        }
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = (bool)this.ViewState["BtnView"];
        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddGroup.aspx?GrId=" + Utility.EncryptQS("") + "&PageMode=" + Utility.EncryptQS("New"));

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int GrId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            GrId = (int)row["GrId"];
        }
        if (GrId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("AddGroup.aspx?GrId=" + Utility.EncryptQS(GrId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int GrId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            GrId = (int)row["GrId"];
        }
        if (GrId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اعضاء ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {

            Response.Redirect("AddGroup.aspx?GrId=" + Utility.EncryptQS(GrId.ToString()) + "&PageMode=" + Utility.EncryptQS("View"));
        }

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int GrId = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            GrId = (int)row["GrId"];
        }
        if (GrId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Delete(GrId);
        }
    }
    protected void Delete(int GrId)
    {
        TSP.DataManager.GroupDetailManager DetailManager = new TSP.DataManager.GroupDetailManager();
        TSP.DataManager.GroupManager GrManager = new TSP.DataManager.GroupManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        trans.Add(DetailManager);
        trans.Add(GrManager);

        GrManager.FindByCode(GrId);
        if (GrManager.Count == 1)
        {
            try
            {
                trans.BeginSave();

                DetailManager.FindByGrId(GrId,1);
                if (DetailManager.Count > 0)
                {
                    int c = DetailManager.Count;
                    for (int i = 0; i < c; i++)
                        DetailManager[0].Delete();
                    DetailManager.Save();

                }

                DetailManager.FindByGrId(GrId, 2);
                if (DetailManager.Count > 0)
                {
                    int c = DetailManager.Count;
                    for (int i = 0; i < c; i++)
                        DetailManager[0].Delete();
                    DetailManager.Save();

                }


                GrManager[0].Delete();

                int cn = GrManager.Save();
                if (cn == 1)
                {
                    trans.EndSave();
                    CustomAspxDevGridView1.DataBind();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "حذف انجام شد";

                }
                else
                {
                    trans.CancelSave();
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در  حذف انجام گرفته است";

                }
            }
            catch (Exception err)
            {
                trans.CancelSave();
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
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "چنین ردیفی وجود ندارد.مجددا بازخوانی نمایید";
        }
    }
    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate")
            e.Editor.Style["direction"] = "ltr";

    }
    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate")
            e.Cell.Style["direction"] = "ltr";

    }
}
