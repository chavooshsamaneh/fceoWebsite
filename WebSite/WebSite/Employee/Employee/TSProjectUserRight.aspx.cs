using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Employee_Employee_TSProjectUserRight : System.Web.UI.Page
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
            ViewState["Mode"] = "New";
            int EmpId = -1;
            int UserId = -1;

            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.ProjectUserRightManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;

            gridMunicipality.Selection.UnselectAll();

            if (string.IsNullOrEmpty(Request.QueryString["UserId"]))
            {
                Response.Redirect("Employee.aspx");
                return;
            }
            try
            {
                HiddenFieldQueryString["UserId"] = Server.HtmlDecode(Request.QueryString["UserId"].ToString());
                HiddenFieldQueryString["EmpId"] = Server.HtmlDecode(Request.QueryString["EmpId"].ToString());
                EmpId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldQueryString["EmpId"].ToString()));
                UserId = Convert.ToInt32(Utility.DecryptQS(HiddenFieldQueryString["UserId"].ToString()));
            }
            catch
            {
                this.Response.Redirect("Employee.aspx");
                return;
            }

            TSP.DataManager.EmployeeManager EmpManager = new TSP.DataManager.EmployeeManager();
            EmpManager.FindByCode(EmpId);
            if (EmpManager.Count > 0)
                RoundPanelPermission.HeaderText = "سطح دسترسی : " + EmpManager[0]["FirstName"].ToString() + " " + EmpManager[0]["LastName"].ToString();

            TSP.DataManager.TechnicalServices.ProjectUserRightManager ProjectUserRightManager = new TSP.DataManager.TechnicalServices.ProjectUserRightManager();
            ProjectUserRightManager.FindByLoginId(UserId);
            // DataTable dtPrjRight = ProjectUserRightManager.SelectTSProjectUserRightListForUser(UserId);
            //if (dtPrjRight.Rows.Count > 0)
            if (ProjectUserRightManager.Count > 0)
            {
                ViewState["Mode"] = "Edit";
                FillForm(ProjectUserRightManager.DataTable);// dtPrjRight);
            }
            this.ViewState["BtnSave"] = btnSave.Enabled;
        }
        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string UserId = Utility.DecryptQS(HiddenFieldQueryString["UserId"].ToString());

        if (ViewState["Mode"].ToString() == "New")
        {
            Insert(int.Parse(UserId));
        }
        else if (ViewState["Mode"].ToString() == "Edit")
        {
            Edit(int.Parse(UserId));
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string EmpId = Utility.DecryptQS(HiddenFieldQueryString["EmpId"].ToString());

        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(EmpId))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            Response.Redirect("Employee.aspx?PostId=" + HiddenFieldQueryString["EmpId"] + "&GrdFlt=" + GrdFlt);
        }
        else
        {
            Response.Redirect("Employee.aspx");
        }
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        String UserId = "", GrdFlt = "", EmpId = ""; ;
        try
        {
            UserId = "UserId=" + Request.QueryString["UserId"];
            EmpId = "&EmpId=" + Request.QueryString["EmpId"];
            GrdFlt = "&GrdFlt=" + Request.QueryString["GrdFlt"];
        }
        catch (Exception) { }

        switch (e.Item.Name)
        {
            case "Pages":
                Response.Redirect("UserRight1.aspx?" + UserId + EmpId + GrdFlt);
                break;
            //case "Automation":
            //    Response.Redirect("AutomationUserRight.aspx?" + UserId + EmpId + GrdFlt);
            //    break;
            case "TS":
                Response.Redirect("TSProjectUserRight.aspx?" + UserId + EmpId + GrdFlt);
                break;
        }
    }

    //protected void gridMunicipality_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    //{
    //    if (gridMunicipality.Selection.Count == gridMunicipality.VisibleRowCount)
    //    {
    //        Session["GrAttachCheched"] = true;
    //        //e.Button.Text = "عدم انتخاب همه";
    //        if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.SelectCheckbox)
    //            e.Column.SelectButton.Text = "عدم انتخاب همه";

    //    }
    //    else
    //    {
    //        Session["GrAttachCheched"] = false;
    //        if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.SelectCheckbox)
    //            e.Column.SelectButton.Text = "انتخاب همه";
    //        // e.Button.Text = "انتخاب همه";
    //    }
    //}

    protected void gridMunicipality_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        if (Convert.ToBoolean(Session["GrAttachCheched"]))
        {
            Session["GrAttachCheched"] = false;
            gridMunicipality.Selection.UnselectAll();
            SelectButtonTypes.Text = "انتخاب همه";

        }
        else
        {
            Session["GrAttachCheched"] = true;
            gridMunicipality.Selection.SelectAll();
            SelectButtonTypes.Text = "عدم انتخاب همه";
        }
    }
    #endregion

    #region Methods
    protected void Insert(int UserId)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.ProjectUserRightManager PermissionsManager = new TSP.DataManager.TechnicalServices.ProjectUserRightManager();

        trans.Add(PermissionsManager);

        try
        {
            trans.BeginSave();
            for (int i = 0; i < gridMunicipality.VisibleRowCount; i++)
            {
                DataRow dr = PermissionsManager.NewRow();

                if (gridMunicipality.Selection.IsRowSelected(i))
                {
                    DataRow row = gridMunicipality.GetDataRow(i);
                    dr["LoginId"] = UserId;
                    dr["MunId"] = int.Parse(row["MunId"].ToString());
                    dr["ModifiedDate"] = DateTime.Now;
                    dr["UserId"] = Utility.GetCurrentUser_UserId();
                    PermissionsManager.AddRow(dr);
                    if (PermissionsManager.Save() <= 0)
                    {
                        trans.CancelSave();
                        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));

                        return;
                    }
                    PermissionsManager.DataTable.AcceptChanges();
                }
            }          
            trans.EndSave();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            ViewState["Mode"] = "Edit";
        }

        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            String Error = Utility.Messages.GetExceptionError(err);
            if (String.IsNullOrWhiteSpace(Error) == false)
            {
                ShowMessage(Error);
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }
        }
    }

    protected void Edit(int UserId)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.ProjectUserRightManager PermissionsManager = new TSP.DataManager.TechnicalServices.ProjectUserRightManager();
        trans.Add(PermissionsManager);

        try
        {
            trans.BeginSave();
            PermissionsManager.FindByLoginId(UserId);

            for (int i = 0; i < gridMunicipality.VisibleRowCount; i++)
            {

                DataRow row = gridMunicipality.GetDataRow(i);
                PermissionsManager.CurrentFilter = "MunId=" + row["MunId"];

                if (PermissionsManager.Count > 0)
                {
                    if (!gridMunicipality.Selection.IsRowSelected(i))
                    {
                        PermissionsManager[0].Delete();
                        if (PermissionsManager.Save() <= 0)
                        {
                            trans.CancelSave();
                            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));

                            return;
                        }
                        PermissionsManager.DataTable.AcceptChanges();
                    }
                    else
                    {
                        if (gridMunicipality.Selection.IsRowSelected(i))
                        {
                            DataTable dtMun = PermissionsManager.SearchUserRight(UserId, int.Parse(row["MunId"].ToString()));
                            if (dtMun.Rows.Count == 0)
                            {
                                DataRow dr = PermissionsManager.NewRow();
                                dr["LoginId"] = UserId;
                                dr["MunId"] = int.Parse(row["MunId"].ToString());
                                dr["ModifiedDate"] = DateTime.Now;
                                dr["UserId"] = Utility.GetCurrentUser_UserId();
                                PermissionsManager.AddRow(dr);
                                if (PermissionsManager.Save() <= 0)
                                {
                                    trans.CancelSave();
                                    ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));

                                    return;
                                }
                                PermissionsManager.DataTable.AcceptChanges();
                            }
                        }
                    }
                }
                else
                {
                    if (gridMunicipality.Selection.IsRowSelected(i))
                    {
                        DataTable dtMun = PermissionsManager.SearchUserRight(UserId, int.Parse(row["MunId"].ToString()));
                        if (dtMun.Rows.Count == 0)
                        {
                            DataRow dr = PermissionsManager.NewRow();
                            dr["LoginId"] = UserId;
                            dr["MunId"] = int.Parse(row["MunId"].ToString());
                            dr["ModifiedDate"] = DateTime.Now;
                            dr["UserId"] = Utility.GetCurrentUser_UserId();
                            PermissionsManager.AddRow(dr);
                            if (PermissionsManager.Save() <= 0)
                            {
                                trans.CancelSave();
                                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));

                                return;
                            }

                            PermissionsManager.DataTable.AcceptChanges();
                        }
                    }

                }
            }
            trans.EndSave();
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
            ViewState["Mode"] = "Edit";
        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            String Error = Utility.Messages.GetExceptionError(err);
            if (String.IsNullOrWhiteSpace(Error) == false)
            {
                ShowMessage(Error);
            }
            else
            {
                ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            }
        }
    }

    protected void FillForm(DataTable dtPrjRight)
    {
        try
        {
            gridMunicipality.DataBind();

            for (int i = 0; i < dtPrjRight.Rows.Count; i++)
            {
                for (int j = 0; j < gridMunicipality.VisibleRowCount; j++)
                {
                    DataRow row = gridMunicipality.GetDataRow(j);

                    if (dtPrjRight.Rows[i]["MunId"].ToString() == row["MunId"].ToString())
                        gridMunicipality.Selection.SelectRow(j);
                }
            }

        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطايى در مشاهده اطلاعات رخ داده است");
        }
    }

    void ShowMessage(String Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
    #endregion
}