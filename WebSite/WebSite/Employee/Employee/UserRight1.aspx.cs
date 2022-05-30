using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
public partial class Employee_Management_UserRight1 : System.Web.UI.Page
{
    Boolean IsPageRefresh = false;
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        //TreeListUserAccess.ExpandAll();

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

        //  this.ClientScript.RegisterStartupScript(this.GetType(),"OnLoad","SetAllNode()");
        if (!IsPostBack)
        {

            ViewState["Mode"] = "New";

            TSP.DataManager.Permission per = TSP.DataManager.UserRightManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnSave.Enabled = per.CanNew || per.CanEdit;
            btnSave2.Enabled = per.CanNew || per.CanEdit;
            TreeListUserAccess.Visible = per.CanView;

            try
            {
                HDUserId.Value = Server.HtmlDecode(Request.QueryString["UserId"]).ToString();
            }
            catch
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            string UserId = Utility.DecryptQS(HDUserId.Value);
            TSP.DataManager.LoginManager LogManager = new TSP.DataManager.LoginManager();
            LogManager.FindByCode(int.Parse(UserId));
            if (LogManager.Count == 1)
            {
                int EmpId = Convert.ToInt32(LogManager[0]["MeId"]);
                HDEmpId.Value = Utility.EncryptQS(EmpId.ToString());
                TSP.DataManager.EmployeeManager EmpManager = new TSP.DataManager.EmployeeManager();
                EmpManager.FindByCode(EmpId);
                if (EmpManager.Count > 0)
                    RoundPanelTree.HeaderText = "تعيين سطح دسترسی : " + EmpManager[0]["FirstName"].ToString() + " " + EmpManager[0]["LastName"].ToString();
            }


            TSP.DataManager.UserRightManager Manager = new TSP.DataManager.UserRightManager();
            Manager.SelectUserRightSelected(int.Parse(UserId));

            for (int i = 0; i < Manager.Count; i++)
            {
                ViewState["Mode"] = "Edit";
                if (Convert.ToBoolean(Manager[i]["CanNew"]))
                    HDnew.Add("n" + Manager[i]["TtId"].ToString(), Manager[i]["CanNew"]);
                if (Convert.ToBoolean(Manager[i]["CanEdit"]))
                    HDedit.Add("e" + Manager[i]["TtId"].ToString(), Manager[i]["CanEdit"]);
                if (Convert.ToBoolean(Manager[i]["CanView"]))
                    HDview.Add("v" + Manager[i]["TtId"].ToString(), Manager[i]["CanView"]);
                if (Convert.ToBoolean(Manager[i]["CanDelete"]))
                    HDdelete.Add("d" + Manager[i]["TtId"].ToString(), Manager[i]["CanDelete"]);
            }

            this.ViewState["BtnSave"] = btnSave.Enabled;
        }

        string Fl = "";
        if (!(string.IsNullOrEmpty(Request.QueryString["Fl"])))
        {
            Fl = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["Fl"]).ToString());
            ObjectDataSourceTree.FilterExpression = "ParentId=" + Fl + " OR TtId=" + Fl;
        }

        if (this.ViewState["BtnSave"] != null)
            this.btnSave.Enabled = this.btnSave2.Enabled = (bool)this.ViewState["BtnSave"];
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string UserId = Utility.DecryptQS(HDUserId.Value);
        if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(ViewState["Mode"].ToString()))
        {
            this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
            return;
        }

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
        //*****?????????????************///
        //string Fl = "";
        //if (!(string.IsNullOrEmpty(Request.QueryString["Fl"])))
        //    Fl = Server.HtmlDecode(Request.QueryString["Fl"]).ToString();

        //if (Fl == "")
        //    Response.Redirect("Users1.aspx");
        //else
        //    Response.Redirect("Users1.aspx?Fl=" + Fl);

        string EmpId = Utility.DecryptQS(HDEmpId.Value);

        if (!string.IsNullOrEmpty(Request.QueryString["GrdFlt"]) && !string.IsNullOrEmpty(EmpId))
        {
            string GrdFlt = Server.HtmlDecode(Request.QueryString["GrdFlt"].ToString());
            Response.Redirect("Employee.aspx?PostId=" + HDEmpId.Value + "&GrdFlt=" + GrdFlt);
        }
        else
        {
            Response.Redirect("Employee.aspx");
        }
    }

    //protected void TreeListUserAccess_SelectionChanged(object sender, EventArgs e)
    //{
    //    for (int n = 0; n < TreeListUserAccess.Nodes.Count; n++)
    //    {
    //        if (TreeListUserAccess.Nodes[n].Selected)
    //        {
    //            for (int j = 0; j < TreeListUserAccess.Nodes[n].ChildNodes.Count; j++)
    //            {
    //                HDnew.Add("n" + TreeListUserAccess.Nodes[n].ChildNodes[j].Key, true);
    //                HDedit.Add("e" + TreeListUserAccess.Nodes[n].ChildNodes[j].Key, true);
    //                HDview.Add("v" + TreeListUserAccess.Nodes[n].ChildNodes[j].Key, true);
    //                HDdelete.Add("d" + TreeListUserAccess.Nodes[n].ChildNodes[j].Key, true);



    //            }
    //        }
    //    }
    //}

    protected void TreeListUserAccess_HtmlRowPrepared(object sender, DevExpress.Web.ASPxTreeList.TreeListHtmlRowEventArgs e)
    {
        if (e.RowKind != DevExpress.Web.ASPxTreeList.TreeListRowKind.Data)
            return;

        if (e.GetValue("ParentId") == null)
            return;
        object ParentId = e.GetValue("ParentId");
        //e.Row.ForeColor = Color.LightGray;

        if (Utility.IsDBNullOrNullValue(ParentId))
        {
            e.Row.Font.Bold = true;
            //e.Row.ForeColor = Color.DarkSlateGray;
        }
        else if (SetColor(e.NodeKey))
        {
            e.Row.Font.Bold = true;
            e.Row.ForeColor = Color.DarkSlateGray;
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
            case "Automation":
                Response.Redirect("AutomationUserRight.aspx?" + UserId + EmpId + GrdFlt);
                break;
            case "Session":
                Response.Redirect("SessionUserRight.aspx?" + UserId + EmpId + GrdFlt);
                break;
            case"TS":
                Response.Redirect("TSProjectUserRight.aspx?" + UserId + EmpId + GrdFlt);
                break;
        }
    }
    #endregion

    #region Methods

    protected bool SetColor(string TtId)
    {
        TSP.DataManager.TableTypeManager Manager = new TSP.DataManager.TableTypeManager();
        Manager.FindByParentId(int.Parse(TtId));
        if (Manager.Count > 0)
            return true;
        else
            return false;
    }

    protected void Insert(int UserId)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.UserRightManager Manager = new TSP.DataManager.UserRightManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        trans.Add(Manager);

        try
        {
            Manager.FindByLoginId(UserId);
            trans.BeginSave();

            for (int i = 0; i < TreeListUserAccess.Nodes.Count; i++)
            {

                DataRow dr = Manager.NewRow();
                if (HDnew.Contains("n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))))
                    dr["CanNew"] = (bool)HDnew["n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))];
                else
                    dr["CanNew"] = false;
                if (HDedit.Contains("e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))))
                    dr["CanEdit"] = (bool)HDedit["e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))];
                else
                    dr["CanEdit"] = false;
                if (HDview.Contains("v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))))
                    dr["CanView"] = (bool)HDview["v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))];
                else
                    dr["CanView"] = false;
                if (HDdelete.Contains("d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))))
                    dr["CanDelete"] = (bool)HDdelete["d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))];
                else
                    dr["CanDelete"] = false;


                dr["LoginId"] = UserId;
                dr["TtId"] = TreeListUserAccess.Nodes[i].Key;
                dr["UserId"] = Utility.GetCurrentUser_UserId();
                dr["ModifiedDate"] = DateTime.Now;

                Manager.AddRow(dr);
                //Manager.Save();
                //Manager.DataTable.AcceptChanges();

                for (int j = 0; j < TreeListUserAccess.Nodes[i].ChildNodes.Count; j++)
                {
                    DataRow drChild = Manager.NewRow();

                    if (HDnew.Contains("n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))))
                        drChild["CanNew"] = (bool)HDnew["n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))];
                    else
                        drChild["CanNew"] = false;
                    if (HDedit.Contains("e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))))
                        drChild["CanEdit"] = (bool)HDedit["e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))];
                    else
                        drChild["CanEdit"] = false;
                    if (HDview.Contains("v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))))
                        drChild["CanView"] = (bool)HDview["v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))];
                    else
                        drChild["CanView"] = false;
                    if (HDdelete.Contains("d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))))
                        drChild["CanDelete"] = (bool)HDdelete["d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))];
                    else
                        drChild["CanDelete"] = false;


                    drChild["LoginId"] = UserId;
                    drChild["TtId"] = Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()));
                    drChild["UserId"] = Utility.GetCurrentUser_UserId();
                    drChild["ModifiedDate"] = DateTime.Now;
                    Manager.AddRow(drChild);
                    //Manager.Save();
                    //Manager.DataTable.AcceptChanges();


                    for (int x = 0; x < TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes.Count; x++)
                    {
                        DataRow drChild2 = Manager.NewRow();

                        if (HDnew.Contains("n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))))
                            drChild2["CanNew"] = (bool)HDnew["n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))];
                        else
                            drChild2["CanNew"] = false;
                        if (HDedit.Contains("e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))))
                            drChild2["CanEdit"] = (bool)HDedit["e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))];
                        else
                            drChild2["CanEdit"] = false;
                        if (HDview.Contains("v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))))
                            drChild2["CanView"] = (bool)HDview["v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))];
                        else
                            drChild2["CanView"] = false;
                        if (HDdelete.Contains("d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))))
                            drChild2["CanDelete"] = (bool)HDdelete["d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))];
                        else
                            drChild2["CanDelete"] = false;


                        drChild2["LoginId"] = UserId;
                        drChild2["TtId"] = Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()));
                        drChild2["UserId"] = Utility.GetCurrentUser_UserId();
                        drChild2["ModifiedDate"] = DateTime.Now;
                        Manager.AddRow(drChild2);

                    }
                }
            }
            if (Manager.Save() > 0)
            {
                trans.EndSave();
                ViewState["Mode"] = "Edit";
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "ذخیره انجام شد";
            }
            else
            {
                trans.CancelSave();

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
            }


        }
        catch (Exception err)
        {
            trans.CancelSave();

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
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    this.LabelWarning.Text = err.Message;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                this.LabelWarning.Text = err.Message;
            }
        }
    }

    protected void Edit(int UserId)
    {
        if (IsPageRefresh)
            return;

        TSP.DataManager.UserRightManager Manager = new TSP.DataManager.UserRightManager();
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        trans.Add(Manager);

        try
        {
            Manager.FindByLoginId(UserId);
            trans.BeginSave();

            for (int i = 0; i < TreeListUserAccess.Nodes.Count; i++)
            {
                Manager.CurrentFilter = "TtId=" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()));

                if (Manager.Count != 0)
                {
                    #region Edit levele1
                    Manager[0].BeginEdit();
                    if (HDnew.Contains("n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))))
                        Manager[0]["CanNew"] = (bool)HDnew["n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))];
                    else
                        Manager[0]["CanNew"] = false;
                    if (HDedit.Contains("e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))))
                        Manager[0]["CanEdit"] = (bool)HDedit["e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))];
                    else
                        Manager[0]["CanEdit"] = false;
                    if (HDview.Contains("v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))))
                        Manager[0]["CanView"] = (bool)HDview["v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))];
                    else
                        Manager[0]["CanView"] = false;
                    if (HDdelete.Contains("d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))))
                        Manager[0]["CanDelete"] = (bool)HDdelete["d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))];
                    else
                        Manager[0]["CanDelete"] = false;

                    Manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                    Manager[0]["ModifiedDate"] = DateTime.Now;
                    Manager[0].EndEdit();
                    #endregion
                }
                else//New Row
                {
                    #region New Row  levele1
                    DataRow dr = Manager.NewRow();
                    if (HDnew.Contains("n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))))
                        dr["CanNew"] = (bool)HDnew["n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))];
                    else
                        dr["CanNew"] = false;
                    if (HDedit.Contains("e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))))
                        dr["CanEdit"] = (bool)HDedit["e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))];
                    else
                        dr["CanEdit"] = false;
                    if (HDview.Contains("v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))))
                        dr["CanView"] = (bool)HDview["v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))];
                    else
                        dr["CanView"] = false;
                    if (HDdelete.Contains("d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))))
                        dr["CanDelete"] = (bool)HDdelete["d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].Key.ToString()))];
                    else
                        dr["CanDelete"] = false;


                    dr["LoginId"] = UserId;
                    dr["TtId"] = TreeListUserAccess.Nodes[i].Key;
                    dr["UserId"] = Utility.GetCurrentUser_UserId();
                    dr["ModifiedDate"] = DateTime.Now;

                    Manager.AddRow(dr);
                    #endregion
                }
                #region Level 2
                for (int j = 0; j < TreeListUserAccess.Nodes[i].ChildNodes.Count; j++)
                {
                    Manager.CurrentFilter = "TtId=" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()));

                    if (Manager.Count != 0)
                    {
                        #region Edit levele2
                        Manager[0].BeginEdit();

                        if (HDnew.Contains("n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))))
                            Manager[0]["CanNew"] = (bool)HDnew["n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))];
                        else
                            Manager[0]["CanNew"] = false;
                        if (HDedit.Contains("e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))))
                            Manager[0]["CanEdit"] = (bool)HDedit["e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))];
                        else
                            Manager[0]["CanEdit"] = false;
                        if (HDview.Contains("v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))))
                            Manager[0]["CanView"] = (bool)HDview["v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))];
                        else
                            Manager[0]["CanView"] = false;
                        if (HDdelete.Contains("d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))))
                            Manager[0]["CanDelete"] = (bool)HDdelete["d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))];
                        else
                            Manager[0]["CanDelete"] = false;

                        Manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                        Manager[0]["ModifiedDate"] = DateTime.Now;
                        Manager[0].EndEdit();
                        #endregion                       
                    }
                    else//New Row
                    {
                        #region New levele2
                        DataRow drChild = Manager.NewRow();

                        if (HDnew.Contains("n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))))
                            drChild["CanNew"] = (bool)HDnew["n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))];
                        else
                            drChild["CanNew"] = false;
                        if (HDedit.Contains("e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))))
                            drChild["CanEdit"] = (bool)HDedit["e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))];
                        else
                            drChild["CanEdit"] = false;
                        if (HDview.Contains("v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))))
                            drChild["CanView"] = (bool)HDview["v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))];
                        else
                            drChild["CanView"] = false;
                        if (HDdelete.Contains("d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))))
                            drChild["CanDelete"] = (bool)HDdelete["d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()))];
                        else
                            drChild["CanDelete"] = false;


                        drChild["LoginId"] = UserId;
                        drChild["TtId"] = Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].Key.ToString()));
                        drChild["UserId"] = Utility.GetCurrentUser_UserId();
                        drChild["ModifiedDate"] = DateTime.Now;
                        Manager.AddRow(drChild);
                        #endregion

                        #region Comments !!!!!!!!!!!!!!!!!11
                        //for (int x = 0; x < TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes.Count; x++)
                        //{    
                        //    #region New Row  levele3
                        //    DataRow drChild2 = Manager.NewRow();
                        
                        //    if (HDnew.Contains("n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))))
                        //        drChild2["CanNew"] = (bool)HDnew["n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))];
                        //    else
                        //        drChild2["CanNew"] = false;
                        //    if (HDedit.Contains("e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))))
                        //        drChild2["CanEdit"] = (bool)HDedit["e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))];
                        //    else
                        //        drChild2["CanEdit"] = false;
                        //    if (HDview.Contains("v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))))
                        //        drChild2["CanView"] = (bool)HDview["v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))];
                        //    else
                        //        drChild2["CanView"] = false;
                        //    if (HDdelete.Contains("d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))))
                        //        drChild2["CanDelete"] = (bool)HDdelete["d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))];
                        //    else
                        //        drChild2["CanDelete"] = false;


                        //    drChild2["LoginId"] = UserId;
                        //    drChild2["TtId"] = Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()));
                        //    drChild2["UserId"] = Utility.GetCurrentUser_UserId();
                        //    drChild2["ModifiedDate"] = DateTime.Now;
                        //    Manager.AddRow(drChild2);
                        //    #endregion
                        //}
                        #endregion
                    }
                    #region Level 3
                    for (int x = 0; x < TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes.Count; x++)
                    {

                        Manager.CurrentFilter = "TtId=" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()));
                        if (Manager.Count != 0)
                        {
                            #region Edit Row  levele3
                            Manager[0].BeginEdit();

                            if (HDnew.Contains("n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))))
                                Manager[0]["CanNew"] = (bool)HDnew["n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))];
                            else
                                Manager[0]["CanNew"] = false;
                            if (HDedit.Contains("e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))))
                                Manager[0]["CanEdit"] = (bool)HDedit["e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))];
                            else
                                Manager[0]["CanEdit"] = false;
                            if (HDview.Contains("v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))))
                                Manager[0]["CanView"] = (bool)HDview["v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))];
                            else
                                Manager[0]["CanView"] = false;
                            if (HDdelete.Contains("d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))))
                                Manager[0]["CanDelete"] = (bool)HDdelete["d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))];
                            else
                                Manager[0]["CanDelete"] = false;

                            Manager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            Manager[0]["ModifiedDate"] = DateTime.Now;

                            Manager[0].EndEdit();
                            #endregion
                        }
                        else
                        {
                            #region New Row  levele3
                            DataRow drChild2 = Manager.NewRow();

                            if (HDnew.Contains("n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))))
                                drChild2["CanNew"] = (bool)HDnew["n" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))];
                            else
                                drChild2["CanNew"] = false;
                            if (HDedit.Contains("e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))))
                                drChild2["CanEdit"] = (bool)HDedit["e" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))];
                            else
                                drChild2["CanEdit"] = false;
                            if (HDview.Contains("v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))))
                                drChild2["CanView"] = (bool)HDview["v" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))];
                            else
                                drChild2["CanView"] = false;
                            if (HDdelete.Contains("d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))))
                                drChild2["CanDelete"] = (bool)HDdelete["d" + Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()))];
                            else
                                drChild2["CanDelete"] = false;


                            drChild2["LoginId"] = UserId;
                            drChild2["TtId"] = Math.Abs(int.Parse(TreeListUserAccess.Nodes[i].ChildNodes[j].ChildNodes[x].Key.ToString()));
                            drChild2["UserId"] = Utility.GetCurrentUser_UserId();
                            drChild2["ModifiedDate"] = DateTime.Now;
                            Manager.AddRow(drChild2);
                            #endregion
                        }
                    }
                    #endregion

                }
                #endregion
            }

            if (Manager.Save() <= 0)
            {
                trans.CancelSave();

                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
            trans.EndSave();
            ViewState["Mode"] = "Edit";

            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد";


        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
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
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                    this.LabelWarning.Text = err.Message;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                this.LabelWarning.Text = err.Message;
            }
        }
    }
    #endregion
}
