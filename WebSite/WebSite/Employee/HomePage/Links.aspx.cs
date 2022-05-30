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
using System.IO;

public partial class Employee_HomePage_Links : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
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

        if (!IsPostBack)
        {
            TSP.DataManager.Permission per = TSP.DataManager.LinksManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            btnEdit.Enabled = per.CanEdit;
            btnEdit2.Enabled = per.CanEdit;
            btnView.Enabled = per.CanView;
            btnView2.Enabled = per.CanView;
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            GridViewLink.Visible = per.CanView;

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
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        //  this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        this.DivReport.Attributes.Add("style", "display:block");
        this.DivReport.Attributes.Add("style", "display:none");

    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        Response.Redirect("AddLink.aspx?LiId=" + Utility.EncryptQS("") + "&PageMode=" + Utility.EncryptQS("New"));
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        int LiId = -1;
        if (GridViewLink.FocusedRowIndex > -1)
        {
            DataRow row = GridViewLink.GetDataRow(GridViewLink.FocusedRowIndex);
            LiId = (int)row["LiId"];
        }
        if (LiId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("AddLink.aspx?LiId=" + Utility.EncryptQS(LiId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit"));
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        int LiId = -1;
        if (GridViewLink.FocusedRowIndex > -1)
        {
            DataRow row = GridViewLink.GetDataRow(GridViewLink.FocusedRowIndex);
            LiId = (int)row["LiId"];
        }
        if (LiId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            Response.Redirect("AddLink.aspx?LiId=" + Utility.EncryptQS(LiId.ToString()) + "&PageMode=" + Utility.EncryptQS("View"));
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        int LiId = -1;
        if (GridViewLink.FocusedRowIndex > -1)
        {
            DataRow row = GridViewLink.GetDataRow(GridViewLink.FocusedRowIndex);
            LiId = (int)row["LiId"];
        }
        if (LiId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.DataManager.TransactionManager TransactManager = new TSP.DataManager.TransactionManager();
            TSP.DataManager.LinksManager managerEdit = new TSP.DataManager.LinksManager();
            TransactManager.Add(managerEdit);
            TransactManager.BeginSave();
            managerEdit.FindByCode(LiId);
            if (managerEdit.Count == 1)
            {
                try
                {
                    int OrderCode = Convert.ToInt32(managerEdit[0]["OrderCode"]);
                    string filename = Server.MapPath(managerEdit[0]["ImageUrl"].ToString());


                    managerEdit[0].Delete();
                    int cn = managerEdit.Save();
                    if (cn == 1)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام شد";
                        if (System.IO.File.Exists(filename))
                            System.IO.File.Delete(filename);

                        //if (!DeleteImage(LiId))
                        //{
                        //    this.DivReport.Visible = true;
                        //    this.LabelWarning.Text = "حذف انجام شد. خطایی در حذف تصویر انجام گرفته است";
                        //}
                        if(!managerEdit.UpdateLinksOrderCode(OrderCode))
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "خطایی در جابجایی لینک ها انجام گرفته است";
                            TransactManager.CancelSave();
                            return;
                        }
                        TransactManager.EndSave();
                        GridViewLink.DataBind();
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "حذف انجام نشد";
                        TransactManager.CancelSave();
                        return;
                    }

                }
                catch (Exception err)
                {
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
                    TransactManager.CancelSave();
                    return;
                }
            }
        }
    }

    public bool DeleteImage(int LiId)
    {
        try
        {
            TSP.DataManager.LinksManager LinksManager = new TSP.DataManager.LinksManager();
            LinksManager.FindByCode(LiId);
            if (LinksManager.Count == 1)
            {
                string filename = Server.MapPath(LinksManager[0]["ImageUrl"].ToString());
                if (System.IO.File.Exists(filename))
                    System.IO.File.Delete(filename);
            }
            else return false;
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void GridViewLink_CustomDataCallback(object sender, DevExpress.Web.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (IsPageRefresh)
            return;
        ResetCpProperties();

        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.LinksManager LinksManager = new TSP.DataManager.LinksManager();
        TSP.DataManager.LinksManager LinksManager2 = new TSP.DataManager.LinksManager();
        TransactionManager.Add(LinksManager);
        try
        {
            int LiId = -1;
            if (GridViewLink.FocusedRowIndex > -1)
            {
                DataRow row = GridViewLink.GetDataRow(GridViewLink.FocusedRowIndex);
                LiId = (int)row["LiId"];
            }
            string[] Parameters = e.Parameters.Split(new char[] { ';' });
            string ButtonId = Parameters[1];
            string VisibleIndex = Parameters[0];
            TransactionManager.BeginSave();
            #region UP
            if (ButtonId == "Up")
            {
                LinksManager.FindByCode(LiId);
                if (LinksManager.Count == 1)
                {
                    if (Convert.ToBoolean(LinksManager[0]["ShowInHomePage"]))
                    {
                        if (!Utility.IsDBNullOrNullValue(LinksManager[0]["OrderCode"]))
                        {
                            int CurrentOrderCode = Convert.ToInt32(LinksManager[0]["OrderCode"]);
                            int PreviousOrderCode = CurrentOrderCode - 1;

                            LinksManager2.FindByOrderCode(PreviousOrderCode);
                            int PreLiId = -1;
                            if (LinksManager2.Count == 1)
                                PreLiId = Convert.ToInt32(LinksManager2[0]["LiId"]);
                            if (PreLiId == -1)
                            {
                                TransactionManager.CancelSave();
                                ShowCallBackMessage("امکان تغییر اولویت لینک انتخابی وجود ندارد.");
                                return;
                            }

                            if (CurrentOrderCode != 1)
                            {
                                LinksManager[0].BeginEdit();
                                LinksManager[0]["OrderCode"] = PreviousOrderCode;
                                LinksManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                                LinksManager[0]["ModifiedDate"] = DateTime.Now;
                                LinksManager[0].EndEdit();
                                if (LinksManager.Save() > 0)
                                {
                                    LinksManager.DataTable.AcceptChanges();
                                    LinksManager.ClearBeforeFill = true;
                                    LinksManager.FindByCode(PreLiId);
                                    if (LinksManager.Count == 1)
                                    {
                                        LinksManager[0].BeginEdit();
                                        LinksManager[0]["OrderCode"] = CurrentOrderCode;
                                        LinksManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                                        LinksManager[0]["ModifiedDate"] = DateTime.Now;
                                        LinksManager[0].EndEdit();
                                        if (LinksManager.Save() > 0)
                                        {
                                            LinksManager.DataTable.AcceptChanges();
                                            TransactionManager.EndSave();
                                            ShowCallBackMessage("ذخیره انجام شد");
                                            //GridViewLink.DataBind();
                                            GridViewLink.JSProperties["cpUpdate"] = 1;
                                        }
                                        else
                                        {
                                            TransactionManager.CancelSave();
                                            ShowCallBackMessage("خطایی در ذخیره انجام گرفته است");
                                        }
                                    }
                                    else
                                    {
                                        TransactionManager.CancelSave();
                                        ShowCallBackMessage("خطایی در ذخیره انجام گرفته است");
                                    }
                                }
                                else
                                {
                                    TransactionManager.CancelSave();
                                    ShowCallBackMessage("خطایی در ذخیره انجام گرفته است");
                                }
                            }
                            else
                            {
                                TransactionManager.CancelSave();
                                ShowCallBackMessage("امکان تغییر اولویت لینک انتخابی وجود ندارد.");
                            }
                        }
                        else
                        {
                            TransactionManager.CancelSave();
                            ShowCallBackMessage("امکان تغییر اولویت لینک انتخابی وجود ندارد.");
                        }
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        ShowCallBackMessage("لینک انتخاب شده در وضعیت نمایش در صفحه اول نمی باشد");
                    }
                }
                else
                {
                    TransactionManager.CancelSave();
                    ShowCallBackMessage("امکان تغییر اولویت لینک انتخابی وجود ندارد.");
                }
            }
            #endregion
            else
            #region Down
            {
                LinksManager.FindByCode(LiId);
                if (LinksManager.Count == 1)
                {
                    if (Convert.ToBoolean(LinksManager[0]["ShowInHomePage"]))
                    {
                        if (!Utility.IsDBNullOrNullValue(LinksManager[0]["OrderCode"]))
                        {
                            int CurrentOrderCode = Convert.ToInt32(LinksManager[0]["OrderCode"]);
                            int NextOrderCode = CurrentOrderCode + 1;

                            LinksManager2.FindByOrderCode(NextOrderCode);
                            int NextLiId = -1;
                            if (LinksManager2.Count == 1)
                                NextLiId = Convert.ToInt32(LinksManager2[0]["LiId"]);
                            if (NextLiId == -1)
                            {
                                TransactionManager.CancelSave();
                                ShowCallBackMessage("امکان تغییر اولویت لینک انتخابی وجود ندارد.");
                                return;
                            }
                            //if (CurrentOrderCode != 1)
                            //{
                            LinksManager[0].BeginEdit();
                            LinksManager[0]["OrderCode"] = NextOrderCode;
                            LinksManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            LinksManager[0]["ModifiedDate"] = DateTime.Now;
                            LinksManager[0].EndEdit();
                            if (LinksManager.Save() > 0)
                            {
                                LinksManager.ClearBeforeFill = true;
                                LinksManager.FindByCode(NextLiId);
                                if (LinksManager.Count == 1)
                                {
                                    LinksManager[0].BeginEdit();
                                    LinksManager[0]["OrderCode"] = CurrentOrderCode;
                                    LinksManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                                    LinksManager[0]["ModifiedDate"] = DateTime.Now;
                                    LinksManager[0].EndEdit();
                                    if (LinksManager.Save() > 0)
                                    {
                                        TransactionManager.EndSave();
                                        ShowCallBackMessage("ذخیره انجام شد");
                                        // GridViewLink.DataBind();
                                        GridViewLink.JSProperties["cpUpdate"] = 1;
                                    }
                                    else
                                    {
                                        TransactionManager.CancelSave();
                                        ShowCallBackMessage("خطایی در ذخیره انجام گرفته است");
                                    }
                                }
                                else
                                {
                                    TransactionManager.CancelSave();
                                    ShowCallBackMessage("خطایی در ذخیره انجام گرفته است");
                                }
                            }
                            else
                            {
                                TransactionManager.CancelSave();
                                ShowCallBackMessage("خطایی در ذخیره انجام گرفته است");
                            }
                            //}
                            //else
                            //{
                            //    TransactionManager.CancelSave();
                            //    e.Result = "امکان تغییر اولویت لینک انتخابی وجود ندارد.";
                            //}
                        }
                        else
                        {
                            TransactionManager.CancelSave();
                            ShowCallBackMessage("امکان تغییر اولویت لینک انتخابی وجود ندارد.");
                        }
                    }
                    else
                    {
                        TransactionManager.CancelSave();
                        ShowCallBackMessage("لینک انتخاب شده در وضعیت نمایش در صفحه اول نمی باشد");
                    }
                }
                else
                {
                    TransactionManager.CancelSave();
                    ShowCallBackMessage("امکان تغییر اولویت لینک انتخابی وجود ندارد.");
                }
            }
            #endregion
            // GridViewLink.DataBind();
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            SetError(err);
        }
    }

    protected void GridViewLink_OnCustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
    {
        GridViewLink.DataBind();
    }

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
            else if (se.Number == 2627)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "کد تکراری می باشد";
            }
            else if (se.Number == 547)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
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
    void ShowCallBackMessage(string Msg)
    {
        GridViewLink.JSProperties["cpMsg"] = Msg;
        GridViewLink.JSProperties["cpError"] = 1;
    }

    void ResetCpProperties()
    {
        GridViewLink.JSProperties["cpMsg"] = "";
        GridViewLink.JSProperties["cpError"] = 0;
        GridViewLink.JSProperties["cpUpdate"] = 0;
    }
}

