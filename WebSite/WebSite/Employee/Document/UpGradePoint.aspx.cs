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

public partial class Employee_Document_UpGradePoint : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.DocUpGradePointManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            GridViewUpGradePoint.ClientVisible = per.CanView;

            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnView"] = btnView.Enabled;
        }
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];
        if (this.ViewState["BtnView"] != null)
            this.btnView.Enabled = this.btnView2.Enabled = GridViewUpGradePoint.ClientVisible = (bool)this.ViewState["BtnView"];

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        NextPage("New");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        NextPage("Edit");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        NextPage("View");
    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        try
        {
            int UpGrdPId;

            if (GridViewUpGradePoint.FocusedRowIndex < 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ابتدا یک ردیف را انتخاب نمایید";
            } 
            DataRow row = GridViewUpGradePoint.GetDataRow(GridViewUpGradePoint.FocusedRowIndex);
            UpGrdPId = (int)row["UpGrdPId"];
            TSP.DataManager.DocUpGradePointManager DocUpGradePointManager = new TSP.DataManager.DocUpGradePointManager();
            DocUpGradePointManager.FindByCode(UpGrdPId);
            if (DocUpGradePointManager.Count != 1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازیابی اطلاعات ایجاد شده است";
                return;
            }
            DocUpGradePointManager[0].BeginEdit();
            DocUpGradePointManager[0]["InActive"] = 1;
            DocUpGradePointManager[0].EndEdit();
            DocUpGradePointManager.Save();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره با موفقیت انجام شد.";
            GridViewUpGradePoint.DataBind();
        }
        catch (Exception err)
        {

            throw;
        }
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        try
        {
            int UpGrdPId;

            if (GridViewUpGradePoint.FocusedRowIndex < 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ابتدا یک ردیف را انتخاب نمایید";
            }
            DataRow row = GridViewUpGradePoint.GetDataRow(GridViewUpGradePoint.FocusedRowIndex);
            UpGrdPId = (int)row["UpGrdPId"];
            TSP.DataManager.DocUpGradePointManager DocUpGradePointManager = new TSP.DataManager.DocUpGradePointManager();
            DocUpGradePointManager.FindByCode(UpGrdPId);
            if (DocUpGradePointManager.Count != 1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازیابی اطلاعات ایجاد شده است";
                return;
            }
            DocUpGradePointManager[0].BeginEdit();
            DocUpGradePointManager[0]["InActive"] = 0;
            DocUpGradePointManager[0].EndEdit();
            DocUpGradePointManager.Save();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره با موفقیت انجام شد.";
            GridViewUpGradePoint.DataBind();
        }
        catch (Exception err)
        {

            throw;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

    }

  //  protected void GridViewUpGradePoint_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
   // {
        //if (e.RowType != GridViewRowType.Data)
        //    return;
        //int GrdId = int.Parse(e.GetValue("GrdId").ToString());
        //int MjId = int.Parse(e.GetValue("MjId").ToString());
        //int ResId = int.Parse(e.GetValue("ResId").ToString());
        //int GMRId = int.Parse(e.GetValue("GMRId").ToString());
        //string Date = e.GetValue("EditDateTime").ToString();
        //TSP.DataManager.DocUpGradePointManager DocUpGradePointManager = new TSP.DataManager.DocUpGradePointManager();
        //DataTable dtDoc = DocUpGradePointManager.SelectLastVersion(GrdId, MjId, ResId, GMRId);
        //if (dtDoc.Rows.Count > 0)
        //{
        //    string LastGradeDate = dtDoc.Rows[0]["EditDateTime"].ToString();
        //    if (Date == LastGradeDate)
        //    {
        //        e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
        //    }
        //}
    //}
    #endregion

    #region Methods
    private void NextPage(string Mode)
    {
        int UpGrdPId = -1;
        int focucedIndex = GridViewUpGradePoint.FocusedRowIndex;

        if (focucedIndex > -1)
        {
            DataRow row = GridViewUpGradePoint.GetDataRow(focucedIndex);
            UpGrdPId = (int)row["UpGrdPId"];
        }
        if (UpGrdPId == -1 && Mode != "New")
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
        }
        else
        {
            if (Mode == "New")
            {
                UpGrdPId = -1;
                // Response.Redirect("AddUpGradePoint.aspx?UpGrdPId=" + Utility.EncryptQS(UpGrdPId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));
            }

            Response.Redirect("AddUpGradePoint.aspx?UpGrdPId=" + Utility.EncryptQS(UpGrdPId.ToString()) + "&PgMd=" + Utility.EncryptQS(Mode));

        }
    }

    //private void Inactive(int GrdcId)
    //{
    //    TSP.DataManager.DocTestConditionManager DocTestConditionManager = new TSP.DataManager.DocTestConditionManager();
    //    try
    //    {
    //        DocTestConditionManager.FindByCode(GrdcId);
    //        if (DocTestConditionManager.Count == 1)
    //        {
    //            DocTestConditionManager[0].BeginEdit();

    //            DocTestConditionManager[0]["Inactive"] = 1;

    //            DocTestConditionManager[0].EndEdit();
    //            int cn = DocTestConditionManager.Save();
    //            if (cn > 0)
    //            {
    //                GridViewTestCondition.DataBind();
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "ذخیره انجام گرفت.";
    //            }
    //            else
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //            }
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        SetError(err);
    //    }
    //}

    //private void Active(int GrdcId)
    //{
    //    TSP.DataManager.DocTestConditionManager DocTestConditionManager = new TSP.DataManager.DocTestConditionManager();
    //    try
    //    {
    //        DocTestConditionManager.FindByCode(GrdcId);
    //        if (DocTestConditionManager.Count == 1)
    //        {
    //            DocTestConditionManager[0].BeginEdit();

    //            DocTestConditionManager[0]["Inactive"] = 0;

    //            DocTestConditionManager[0].EndEdit();
    //            int cn = DocTestConditionManager.Save();
    //            if (cn > 0)
    //            {
    //                GridViewTestCondition.DataBind();
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "ذخیره انجام گرفت.";
    //            }
    //            else
    //            {
    //                this.DivReport.Visible = true;
    //                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
    //            }
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        SetError(err);
    //    }
    //}

    private void SetError(Exception err)
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

    protected void GridViewUpGradePoint_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {

        switch (e.DataColumn.FieldName)
        {
            case "EditDateTime":
                e.Cell.Style["direction"] = "ltr";
                break;
        }
    }

    protected void GridViewUpGradePoint_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "EditDateTime":
                e.Editor.Style["direction"] = "ltr";
                break;
        }
    }
    #endregion
}
