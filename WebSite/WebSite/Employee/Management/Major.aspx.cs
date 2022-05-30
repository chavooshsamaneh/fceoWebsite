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

public partial class Employee_Management_Major : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.MajorManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            DevGridViewMajor.Visible = per.CanView;

            //  BindCombo(DropDownParentId1);


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

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");


    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        btnEdit.Enabled = false;
        btnEdit2.Enabled = false;
        NextPage("New");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (DevGridViewMajor.FocusedRowIndex > -1)
        {
            TSP.DataManager.MajorManager majorManager = new TSP.DataManager.MajorManager();
            DataRow dr = DevGridViewMajor.GetDataRow(DevGridViewMajor.FocusedRowIndex);
            int ParentId = (int)dr["ParentId"];
            if (ParentId == 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان ویرایش رشته های اصلی وجود ندارد.";
            }
            else
            {
                NextPage("Edit");
            }
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Deletemajor();
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        Activation(true);
    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        Activation(false);
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporterMajor.FileName = "Majors";
        GridViewExporterMajor.WriteXlsToResponse(true);
    }

    protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {

    }
    #endregion
    #region Methods
    private void NextPage(string Mode)
    {
        TSP.DataManager.MajorManager cityManager = new TSP.DataManager.MajorManager();
        String GridFilterString = DevGridViewMajor.FilterExpression;
        int mjId = -1;


        if (DevGridViewMajor.FocusedRowIndex > -1)
        {
            DataRow dr = DevGridViewMajor.GetDataRow(DevGridViewMajor.FocusedRowIndex);
            mjId = (int)dr["MjId"];
            switch (Mode)
            {
                case "Edit":
                    DataRow row = DevGridViewMajor.GetDataRow(DevGridViewMajor.FocusedRowIndex);
                    mjId = (int)row["MjId"];
                    break;
                case "New":
                    break;
                case "View":
                    DataRow row2 = DevGridViewMajor.GetDataRow(DevGridViewMajor.FocusedRowIndex);
                    mjId = (int)row2["MjId"];
                    break;
            }
        }
        if (mjId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        else
        {
            if (Mode == "New")
            {
                mjId = -1;
                Response.Redirect("MajorInsert.aspx?mjId=" + Utility.EncryptQS(mjId.ToString()) +
                    "&PageMode=" + Utility.EncryptQS(Mode) +
                    "&GrdFlt=" + Utility.EncryptQS(GridFilterString));
            }
            else
            {
                Response.Redirect("MajorInsert.aspx?mjId=" + Utility.EncryptQS(mjId.ToString()) +
                    "&PageMode=" + Utility.EncryptQS(Mode) +
                    "&GrdFlt=" + Utility.EncryptQS(GridFilterString));
            }
        }
    }
    private void Deletemajor()
    {
        TSP.DataManager.MajorManager MjManager = new TSP.DataManager.MajorManager();
        DataRow Row = DevGridViewMajor.GetDataRow(DevGridViewMajor.FocusedRowIndex);
        try
        {
            MjManager.FindByCode((int)Row["MjId"]);
            if (MjManager.Count == 1)
            {
                int ParentId = int.Parse(MjManager[0]["ParentId"].ToString());
                if (ParentId == 0)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان حذف رشته های اصلی وجود ندارد.";
                }
                else
                {
                    MjManager[0].Delete();
                    int cn = MjManager.Save();
                    if (cn == 1)
                    {
                        DevGridViewMajor.DataBind();
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام شد";
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
                this.LabelWarning.Text = "اطلاعات توسط کاربر دیگری تغییر یافته است";
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);

            this.DivReport.Visible = true;
            this.LabelWarning.Text = Utility.Messages.GetExceptionError(err);
        }

        //int majorId = -1;
        //if (DevGridViewMajor.FocusedRowIndex > -1)
        //{
        //    DataRow dr = DevGridViewMajor.GetDataRow(DevGridViewMajor.FocusedRowIndex);
        //    majorId = (int)dr["MjId"];
        //}

        //if (majorId == -1)
        //{
        //    this.DivReport.Visible = true;
        //    this.LabelWarning.Text = "لطفا ابتدا یک رکورد را انتخاب کنید";
        //}
        //else
        //{
        //    TSP.DataManager.MajorManager majorManager = new TSP.DataManager.MajorManager();
        //    majorManager.FindByCode(majorId);

        //    if (majorManager.Count == 1)
        //    {
        //        try
        //        {
        //            majorManager[0].Delete();
        //            int cn = majorManager.Save();
        //            if (cn == 1)
        //            {
        //                DevGridViewMajor.DataBind();
        //                this.DivReport.Visible = true;
        //                this.LabelWarning.Text = "حذف انجام شد";
        //            }
        //            else
        //            {
        //                this.DivReport.Visible = true;
        //                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
        //            }
        //        }
        //        catch (Exception err)
        //        {
        //            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
        //            {
        //                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;

        //                if (se.Number == 2601)
        //                {
        //                    this.DivReport.Visible = true;
        //                    this.LabelWarning.Text = "اطلاعات تکراری می باشد";
        //                }
        //                else if (se.Number == 2627)
        //                {
        //                    this.DivReport.Visible = true;
        //                    this.LabelWarning.Text = "کد تکراری می باشد";
        //                }
        //                else if (se.Number == 547)
        //                {
        //                    this.DivReport.Visible = true;
        //                    this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
        //                }
        //                else
        //                {
        //                    this.DivReport.Visible = true;
        //                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        //                }
        //            }
        //            else
        //            {
        //                this.DivReport.Visible = true;
        //                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
        //            }
        //        }
        //    }
        // }
    }
    private void Activation(bool active)
    {
        TSP.DataManager.MajorManager MjManager = new TSP.DataManager.MajorManager();
        DataRow Row = DevGridViewMajor.GetDataRow(DevGridViewMajor.FocusedRowIndex);
        try
        {
            MjManager.FindByCode((int)Row["MjId"]);
            if (MjManager.Count == 1)
            {
                int ParentId = int.Parse(MjManager[0]["ParentId"].ToString());
                if (ParentId == 0)
                {
                    ShowMessage("امکان غیر فعال یا فعال کردن رشته های اصلی وجود ندارد.");
                }
                else
                {
                    MjManager[0].BeginEdit();
                    MjManager[0]["InActiveMajor"] = active;
                    MjManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    MjManager[0]["ModifiedDate"] = DateTime.Now;
                    MjManager[0].EndEdit();
                    int cn = MjManager.Save();
                    if (cn == 1)
                    {
                        DevGridViewMajor.DataBind();
                        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
                    }
                    else
                    {
                        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInInActive));
                    }
                }
            }
            else
            {
                ShowMessage("اطلاعات توسط کاربر دیگری تغییر یافته است");
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);

            this.DivReport.Visible = true;
            this.LabelWarning.Text = Utility.Messages.GetExceptionError(err);
        }
    }

    protected void DevGridViewMajor_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

    }

    private void ShowMessage(string message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = message;
    }
    #endregion
}
