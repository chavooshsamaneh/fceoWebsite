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

public partial class Members_MemberInfo_MemberActivity : System.Web.UI.Page
{
    private bool IsPageRefresh = false;
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


        this.DivReport.Visible = true;
        DivReport.Style["visibility"] = "hidden";

        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {
            Session["IsEdited_MeAct"] = false;
            Session["FillMeActivity"] = null;

            ViewState["PMode"] = "";

            if (string.IsNullOrEmpty(Request.QueryString["MeId"]))
            {
                Response.Redirect("~/Members/MemberHome.aspx");


            }
            try
            {
                MemberId.Value = Server.HtmlDecode(Request.QueryString["MeId"].ToString());
                HDMode.Value = Server.HtmlDecode(Request.QueryString["Mode"].ToString());
                MemberRequest.Value = Server.HtmlDecode(Request.QueryString["MReId"]).ToString();

            }
            catch
            { }

            string MeId = Utility.DecryptQS(MemberId.Value);
            string Mode = Utility.DecryptQS(HDMode.Value);


            if (string.IsNullOrEmpty(MeId) || string.IsNullOrEmpty(Mode))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());

                return;
            }

            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
                Page_Load_TempMember();
            else
                Page_Load_Member();

            this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
        }

        if (this.ViewState["BtnEdit"] != null)
            this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        if (Session["FillMeActivity"] != null)
        {
            Grid_DataBind((DataTable)Session["FillMeActivity"]);
        }
        else
            FillGrid();

    }
    void Page_Load_Member()
    {
        string MeId = Utility.DecryptQS(MemberId.Value);
        TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
        MeManager.FindByCode(int.Parse(MeId));
        if (MeManager.Count == 0)
        {
            DivReport.Style["visibility"] = "block";
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }

        TSP.DataManager.MemberActivitySubjectManager ActManager = new TSP.DataManager.MemberActivitySubjectManager();
        switch (Utility.DecryptQS(HDMode.Value))
        {

            case "Home":
                btnEdit.Enabled = false;
                btnEdit2.Enabled = false;
                btnInActive.Enabled = false;
                btnInActive2.Enabled = false;
                BtnNew.Enabled = false;
                BtnNew2.Enabled = false;

                try
                {
                    if (MeManager[0]["MrsId"].ToString() == "1")//تایید شده
                    {
                        Session["FillMeActivity"] = ActManager.FindByMeRequest(int.Parse(MeId), -1, 1);
                        //CustomAspxDevGridView1.DataSource = ActManager.FindByMeRequest(int.Parse(MeId), -1, 1);
                        //CustomAspxDevGridView1.DataBind();
                    }
                    else
                    {
                        Session["FillMeActivity"] = ActManager.FindByMeRequest(int.Parse(MeId), -1, -1);

                    }
                }
                catch (Exception)
                { }

                break;
            case "Request":

                SetMenuItem();

                string MReId = Utility.DecryptQS(MemberRequest.Value);

                if (string.IsNullOrEmpty(MReId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                //CustomAspxDevGridView1.DataSource = ActManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1);
                //CustomAspxDevGridView1.DataBind();

                if (!CheckPermitionForEdit(int.Parse(MReId)))
                {
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnInActive.Enabled = false;
                    btnInActive2.Enabled = false;
                }

                TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
                ReqManager.FindByCode(int.Parse(MReId));
                if (ReqManager.Count > 0)
                {
                    if (Convert.ToBoolean(ReqManager[0]["Requester"]) || ReqManager[0]["IsConfirm"].ToString() != "0")//FromEmployee//answered
                    {
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                        BtnNew2.Enabled = false;
                        BtnNew.Enabled = false;
                        btnInActive.Enabled = false;
                        btnInActive2.Enabled = false;
                    }

                    if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MsId"]) && Convert.ToInt32(ReqManager[0]["MsId"].ToString()) == (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince)
                    {
                        BtnNew.Enabled = BtnNew2.Enabled = false;
                        btnEdit.Enabled = btnEdit2.Enabled = false;
                        btnInActive.Enabled = btnInActive2.Enabled = false;
                    }
                }
                if ((ReqManager[0]["IsConfirm"].ToString() != "0"))
                    Session["FillMeActivity"] = ActManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1);
                else
                    Session["FillMeActivity"] = ActManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 2);



                break;
        }
    }
    void Page_Load_TempMember()
    {
        string MeId = Utility.DecryptQS(MemberId.Value);
        TSP.DataManager.TempMemberManager MeManager = new TSP.DataManager.TempMemberManager();
        MeManager.FindByCode(int.Parse(MeId));
        if (MeManager.Count == 0)
        {
            DivReport.Style["visibility"] = "block";
            this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
            return;
        }

        TSP.DataManager.TempMemberActivitySubjectManager ActManager = new TSP.DataManager.TempMemberActivitySubjectManager();
        switch (Utility.DecryptQS(HDMode.Value))
        {

            case "Request":

                SetMenuItem();

                string MReId = Utility.DecryptQS(MemberRequest.Value);

                if (string.IsNullOrEmpty(MReId))
                {
                    this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                    return;
                }
                //CustomAspxDevGridView1.DataSource = ActManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1);
                //CustomAspxDevGridView1.DataBind();

                if (!CheckPermitionForEdit(int.Parse(MReId)))
                {
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnInActive.Enabled = false;
                    btnInActive2.Enabled = false;
                }

                TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
                ReqManager.FindByCode(int.Parse(MReId));
                if (ReqManager.Count > 0)
                {
                    if (Convert.ToBoolean(ReqManager[0]["Requester"]) || ReqManager[0]["IsConfirm"].ToString() != "0")//FromEmployee//answered
                    {
                        btnEdit.Enabled = false;
                        btnEdit2.Enabled = false;
                        BtnNew2.Enabled = false;
                        BtnNew.Enabled = false;
                        btnInActive.Enabled = false;
                        btnInActive2.Enabled = false;
                    }

                    if (!Utility.IsDBNullOrNullValue(ReqManager[0]["MsId"]) && Convert.ToInt32(ReqManager[0]["MsId"].ToString()) == (int)TSP.DataManager.MembershipRegistrationStatus.TransferToOtherProvince)
                    {
                        BtnNew.Enabled = BtnNew2.Enabled = false;
                        btnEdit.Enabled = btnEdit2.Enabled = false;
                        btnInActive.Enabled = btnInActive2.Enabled = false;
                    }
                }
                if ((ReqManager[0]["IsConfirm"].ToString() != "0"))
                {
                    ActManager.FindByRequest(int.Parse(MeId), int.Parse(MReId));
                    Session["FillMeActivity"] = ActManager.DataTable;
                }
                else
                {
                    ActManager.FindByRequest(int.Parse(MeId), int.Parse(MReId));
                    Session["FillMeActivity"] = ActManager.DataTable;
                }



                break;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string Mode = Utility.DecryptQS(HDMode.Value);
        if (Mode == "Home")
        {
            Response.Redirect("~/Members/MemberHome.aspx?MeId=" + MemberId.Value);
        }
        else

            Response.Redirect("MemberRequestInsert.aspx?MeId=" + MemberId.Value + "&MReId=" + MemberRequest.Value + "&PageMode=" + Request.QueryString["PageMode"]);

    }
    protected void CustomAspxDevGridView1_PageIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;

        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
        {
            TSP.DataManager.TempMemberManager MemberManager = new TSP.DataManager.TempMemberManager();
            MemberManager.FindByCode(int.Parse(Utility.DecryptQS(MemberId.Value)));
            if (MemberManager == null || MemberManager.Count == 0)
            {
                DivReport.Style["visibility"] = "block";
                this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
                return;
            }
            //if ((bool)MemberManager[0]["IsLock"] == true)
            //{
            //    string lockers = Utility.GetFormattedObject(Session["MemberLockers"]);
            //    DivReport.Style["visibility"] = "block";
            //    this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
            //    return;

            //}

            try
            {
                TSP.DataManager.TempMemberActivitySubjectManager ActManager = new TSP.DataManager.TempMemberActivitySubjectManager();
                DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                ActManager.FindByCode((int)row["TMasId"]);
                ActManager[0].Delete();
                ActManager.Save();
                return;
            }
            catch (Exception err)
            {
                DivReport.Style["visibility"] = "block";
                this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";
                return;
            }
        }
        else
        {
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            MemberManager.FindByCode(int.Parse(Utility.DecryptQS(MemberId.Value)));
            if (MemberManager == null || MemberManager.Count == 0)
            {
                DivReport.Style["visibility"] = "block";
                this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
                return;
            }
            //if ((bool)MemberManager[0]["IsLock"] == true)
            if (Utility.GetCurrentUser_IsLock())
            {
                TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
                string lockers = lockHistoryManager.FindLockers(Utility.GetCurrentUser_MeId(), (int)TSP.DataManager.LockMemberType.Member, 1);
                DivReport.Style["visibility"] = "block";
                this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
                return;

            }



            try
            {
                int MasId = -1;
                int MReId = -1;
                string InActiveName = "";

                if (CustomAspxDevGridView1.FocusedRowIndex > -1)
                {
                    if (Session["FillMeActivity"] != null)
                    {
                        Grid_DataBind((DataTable)Session["FillMeActivity"]);
                    }
                    else
                        FillGrid();

                    DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
                    if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
                        MasId = (int)row["TMasId"];
                    else
                        MasId = (int)row["MasId"];

                    MReId = (int)row["MReId"];
                    InActiveName = row["InActiveName"].ToString();

                }
                if (MasId == -1)
                {
                    DivReport.Style["visibility"] = "block";
                    this.LabelWarning.Text = "لطفا ابتدا یک رکورد را انتخاب نمائید";

                }
                else
                {
                    TSP.DataManager.MemberActivitySubjectManager ActManager = new TSP.DataManager.MemberActivitySubjectManager();

                    ActManager.FindByCode(MasId);
                    if (ActManager.Count == 1)
                    {
                        try
                        {
                            int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));


                            int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

                            if (MReId == CurrentMReId)
                            {
                                ActManager[0].Delete();
                                ActManager.Save();
                                Session["FillMeActivity"] = ActManager.FindByMeRequest(MeId, -1, -1, 2);
                                Grid_DataBind((DataTable)Session["FillMeActivity"]);

                                DivReport.Style["visibility"] = "block";
                                this.LabelWarning.Text = "ذخیره انجام شد";

                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(InActiveName) && InActiveName != "فعال")
                                {
                                    DivReport.Style["visibility"] = "block";
                                    this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                                    return;
                                }
                                InsertInActive(MasId, CurrentMReId, MeId, ActManager);

                                //if (Convert.ToBoolean(ActManager[0]["InActive"]))
                                //{
                                //    DivReport.Style["visibility"] = "block";
                                //    this.LabelWarning.Text = "رکورد مورد نظر غیر فعال می باشد";
                                //    return;
                                //}
                                //else
                                //{
                                //    ActManager[0].BeginEdit();
                                //    ActManager[0]["InActive"] = 1;
                                //    ActManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                                //    ActManager[0].EndEdit();
                                //}
                            }

                            CheckMenuImageCurrentPage(MeId, CurrentMReId);


                        }
                        catch (Exception)
                        {

                            DivReport.Style["visibility"] = "block";
                            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است";

                        }

                    }
                }
            }
            catch (Exception)
            {
                DivReport.Style["visibility"] = "block";
                this.LabelWarning.Text = "خطایی در مشاهده اطلاعات رخ داده است";
            }
        }
    }
    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        string Mode = Utility.DecryptQS(HDMode.Value);
        if (!string.IsNullOrEmpty(Mode))
        {
            if (Mode == "Request")
            {
                if (e.RowType != DevExpress.Web.GridViewRowType.Data)
                    return;
                if (MemberRequest.Value != null)
                {
                    string MReId = Utility.DecryptQS(MemberRequest.Value);
                    if (e.GetValue("MReId") == null)
                        return;
                    string CurretnMReId = e.GetValue("MReId").ToString();
                    if (MReId == CurretnMReId)
                    {
                        e.Row.BackColor = System.Drawing.Color.LightGray;
                    }
                }
            }
        }
    }
    protected void CustomAspxDevGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        try
        {
            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            {
                #region TemporaryMembers
                TSP.DataManager.TempMemberActivitySubjectManager ActManager = new TSP.DataManager.TempMemberActivitySubjectManager();
                TSP.DataManager.TempMemberActivitySubjectManager ActManager2 = new TSP.DataManager.TempMemberActivitySubjectManager();
                TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

                trans.Add(ActManager);
                trans.Add(WorkFlowStateManager);
                int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
                int MReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

                ActManager2.FindByTMeId(MeId);

                for (int i = 0; i < ActManager2.Count; i++)
                {
                    if (ActManager2[i]["AsId"].ToString() == e.NewValues["AsId"].ToString() && ActManager2[i]["InActiveName"].ToString() == "فعال")
                    {

                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "اطلاعات وارد شده تکراری می باشد";
                        CustomAspxDevGridView1.CancelEdit();
                        FillGrid();
                        return;
                    }
                }

                DataRow dr = ActManager.NewRow();

                dr["AsId"] = int.Parse(e.NewValues["AsId"].ToString());

                dr["TMeId"] = MeId;
                dr["UserId"] = Utility.GetCurrentUser_UserId();

                if (e.NewValues["AsPercent"] != null)
                    dr["AsPercent"] = int.Parse(e.NewValues["AsPercent"].ToString());
                if (e.NewValues["Description"] != null)
                    dr["Description"] = e.NewValues["Description"].ToString();
                dr["ModifiedDate"] = DateTime.Now;
                dr["MReId"] = MReId;

                ActManager.AddRow(dr);
                trans.BeginSave();
                int cnt = ActManager.Save();
                if (cnt > 0)
                {
                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_MeAct"].ToString())))
                    {
                        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                        int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberActivity;
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, MReId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), MeId, 1);

                    }
                    if (UpdateState == -4)
                    {
                        trans.CancelSave();
                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = ".خطایی در ذخیره انجام گرفته است";


                    }
                    else
                    {
                        trans.EndSave();
                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = " ذخیره انجام شد";
                        CustomAspxDevGridView1.JSProperties["cpMenu"] = 1;

                        Session["IsEdited_MeAct"] = true;
                        ActManager.FindByRequest(MeId, MReId);
                        Session["FillMeActivity"] = ActManager.DataTable;
                        Grid_DataBind((DataTable)Session["FillMeActivity"]);
                    }

                }
                else
                {
                    trans.CancelSave();

                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                    return;
                }
                #endregion
            }
            else
            {
                #region Members
                TSP.DataManager.MemberActivitySubjectManager ActManager = new TSP.DataManager.MemberActivitySubjectManager();
                TSP.DataManager.MemberActivitySubjectManager ActManager2 = new TSP.DataManager.MemberActivitySubjectManager();
                TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);

                trans.Add(ActManager);
                trans.Add(WorkFlowStateManager);
                int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
                int MReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));

                ActManager2.FindByMeId(MeId);

                for (int i = 0; i < ActManager2.Count; i++)
                {
                    if (ActManager2[i]["AsId"].ToString() == e.NewValues["AsId"].ToString() && ActManager2[i]["InActiveName"].ToString() == "فعال")
                    {

                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "اطلاعات وارد شده تکراری می باشد";
                        CustomAspxDevGridView1.CancelEdit();
                        FillGrid();
                        return;
                    }
                }

                DataRow dr = ActManager.NewRow();

                dr["AsId"] = int.Parse(e.NewValues["AsId"].ToString());

                dr["MeId"] = MeId;
                dr["UserId"] = Utility.GetCurrentUser_UserId();

                if (e.NewValues["AsPercent"] != null)
                    dr["AsPercent"] = int.Parse(e.NewValues["AsPercent"].ToString());
                if (e.NewValues["Description"] != null)
                    dr["Description"] = e.NewValues["Description"].ToString();
                dr["ModifiedDate"] = DateTime.Now;
                dr["MReId"] = MReId;

                ActManager.AddRow(dr);
                trans.BeginSave();
                int cnt = ActManager.Save();
                if (cnt > 0)
                {
                    int UpdateState = -1;
                    if (!(Convert.ToBoolean(Session["IsEdited_MeAct"].ToString())))
                    {
                        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                        int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberActivity;
                        UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, MReId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), MeId, 1);

                    }
                    if (UpdateState == -4)
                    {
                        trans.CancelSave();
                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = ".خطایی در ذخیره انجام گرفته است";


                    }
                    else
                    {
                        trans.EndSave();
                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = " ذخیره انجام شد";
                        CustomAspxDevGridView1.JSProperties["cpMenu"] = 1;

                        Session["IsEdited_MeAct"] = true;
                        Session["FillMeActivity"] = ActManager.FindByMeRequest(MeId, -1, -1, 2);
                        Grid_DataBind((DataTable)Session["FillMeActivity"]);
                    }

                }
                else
                {
                    trans.CancelSave();

                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                    return;
                }
                #endregion
            }
            CustomAspxDevGridView1.CancelEdit();

        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            CustomAspxDevGridView1.CancelEdit();

            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "اطلاعات تکراری می باشد";
                }
                else
                {
                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }
    protected void CustomAspxDevGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.Cancel = true;

        int MasId = (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers) ? int.Parse(e.Keys["TMasId"].ToString()) : int.Parse(e.Keys["MasId"].ToString()); ;
        //int MReId = int.Parse(e.NewValues["MReId"].ToString());
        int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));
        TSP.DataManager.MemberActivitySubjectManager ActManager = new TSP.DataManager.MemberActivitySubjectManager();
        ActManager.FindByCode(MasId);
        int MReId = int.Parse(ActManager[0]["MReId"].ToString());


        int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
        if (MReId == CurrentMReId)
        {
            if (CheckPermitionForEdit(MReId))
            {
                TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
                try
                {
                    if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
                    {
                        #region TemporaryMembers
                        //TSP.DataManager.MemberActivitySubjectManager ActManager = new TSP.DataManager.MemberActivitySubjectManager();
                        TSP.DataManager.TempMemberActivitySubjectManager ActManager2 = new TSP.DataManager.TempMemberActivitySubjectManager();
                        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
                        trans.Add(ActManager);
                        trans.Add(WorkFlowStateManager);

                        ActManager.FindByCode(int.Parse(e.Keys["TMasId"].ToString()));
                        if (ActManager.Count == 1)
                        {

                            ActManager[0].BeginEdit();
                            //if (drdAtSubj.Value != null)
                            ActManager[0]["AsId"] = int.Parse(e.NewValues["AsId"].ToString());
                            if (e.NewValues["AsPercent"] != null)
                                ActManager[0]["AsPercent"] = int.Parse(e.NewValues["AsPercent"].ToString());

                            ActManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            if (e.NewValues["Description"] != null)
                                ActManager[0]["Description"] = e.NewValues["Description"].ToString();
                            ActManager[0]["ModifiedDate"] = DateTime.Now;
                            ActManager[0].EndEdit();
                            trans.BeginSave();
                            int cnt = ActManager.Save();
                            if (cnt > 0)
                            {
                                int UpdateState = -1;
                                if (!(Convert.ToBoolean(Session["IsEdited_MeAct"].ToString())))
                                {
                                    int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                                    int TableId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                                    int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberActivity;
                                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), MeId, 1);

                                }
                                if (UpdateState == -4)
                                {
                                    trans.CancelSave();

                                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = ".خطایی در ذخیره انجام گرفته است";

                                }
                                else
                                {
                                    trans.EndSave();

                                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = " ذخیره انجام شد";
                                    Session["IsEdited_MeAct"] = true;
                                    Session["FillMeActivity"] = ActManager.FindByMeRequest(MeId, -1, -1, 2);
                                    Grid_DataBind((DataTable)Session["FillMeActivity"]);
                                    CustomAspxDevGridView1.CancelEdit();

                                }
                            }
                            else
                            {
                                trans.CancelSave();
                                CustomAspxDevGridView1.CancelEdit();

                                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                                return;
                            }
                        }
                        else
                        {
                            CustomAspxDevGridView1.CancelEdit();

                            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                        }
                        #endregion
                    }
                    else
                    {
                        #region Members
                        //TSP.DataManager.MemberActivitySubjectManager ActManager = new TSP.DataManager.MemberActivitySubjectManager();
                        TSP.DataManager.MemberActivitySubjectManager ActManager2 = new TSP.DataManager.MemberActivitySubjectManager();
                        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(trans);
                        trans.Add(ActManager);
                        trans.Add(WorkFlowStateManager);

                        ActManager.FindByCode(int.Parse(e.Keys["MasId"].ToString()));
                        if (ActManager.Count == 1)
                        {

                            ActManager[0].BeginEdit();
                            //if (drdAtSubj.Value != null)
                            ActManager[0]["AsId"] = int.Parse(e.NewValues["AsId"].ToString());
                            if (e.NewValues["AsPercent"] != null)
                                ActManager[0]["AsPercent"] = int.Parse(e.NewValues["AsPercent"].ToString());

                            ActManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
                            if (e.NewValues["Description"] != null)
                                ActManager[0]["Description"] = e.NewValues["Description"].ToString();
                            ActManager[0]["ModifiedDate"] = DateTime.Now;
                            ActManager[0].EndEdit();
                            trans.BeginSave();
                            int cnt = ActManager.Save();
                            if (cnt > 0)
                            {
                                int UpdateState = -1;
                                if (!(Convert.ToBoolean(Session["IsEdited_MeAct"].ToString())))
                                {
                                    int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
                                    int TableId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
                                    int UpdateTableType = (int)TSP.DataManager.TableCodes.MemberActivity;
                                    UpdateState = WorkFlowStateManager.InsertWorkFlowUpdateState(TableType, TableId, UpdateTableType, "Update", Utility.GetCurrentUser_UserId(), MeId, 1);

                                }
                                if (UpdateState == -4)
                                {
                                    trans.CancelSave();

                                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = ".خطایی در ذخیره انجام گرفته است";

                                }
                                else
                                {
                                    trans.EndSave();

                                    CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                                    CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = " ذخیره انجام شد";
                                    Session["IsEdited_MeAct"] = true;
                                    Session["FillMeActivity"] = ActManager.FindByMeRequest(MeId, -1, -1, 2);
                                    Grid_DataBind((DataTable)Session["FillMeActivity"]);
                                    CustomAspxDevGridView1.CancelEdit();

                                }
                            }
                            else
                            {
                                trans.CancelSave();
                                CustomAspxDevGridView1.CancelEdit();

                                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                                return;
                            }
                        }
                        else
                        {
                            CustomAspxDevGridView1.CancelEdit();

                            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                        }
                        #endregion
                    }

                }
                catch (Exception err)
                {
                    trans.CancelSave();
                    CustomAspxDevGridView1.CancelEdit();
                    Utility.SaveWebsiteError(err);

                    if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
                    {
                        System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                        if (se.Number == 2601)
                        {
                            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "اطلاعات تکراری می باشد";
                        }
                        else
                        {
                            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                        }
                    }
                    else
                    {
                        CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                        CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "خطایی در ذخیره انجام گرفته است";
                    }
                }

            }
            else
            {
                CustomAspxDevGridView1.CancelEdit();
                CustomAspxDevGridView1.JSProperties["cpError"] = 2;
                CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
            }

        }
        else
        {
            CustomAspxDevGridView1.CancelEdit();
            CustomAspxDevGridView1.JSProperties["cpError"] = 2;
            CustomAspxDevGridView1.JSProperties["cpErrorMsg"] = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
        }
    }
    protected void CustomAspxDevGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        CustomAspxDevGridView1.JSProperties["cpShow"] = 1;
        int MeId = int.Parse(Utility.DecryptQS(MemberId.Value));

        string[] Parameters = e.Parameters.Split(new char[] { ';' });
        string PgMd = Parameters[1];
        string VisibleIndex = Parameters[0];


        if (PgMd == "Edit")
        {

            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
                FillGridTempMe();
            else
                FillGrid();

            DataRow row = CustomAspxDevGridView1.GetDataRow(int.Parse(VisibleIndex));
            int MasId = (int)row["MasId"];
            int MReId = (int)row["MReId"];


            int CurrentMReId = int.Parse(Utility.DecryptQS(MemberRequest.Value));
            if (MReId == CurrentMReId)
            {
                if (!CheckPermitionForEdit(MReId))
                {

                    e.Result = "امکان ویرایش اطلاعات در این مرحله از جریان کار برای شما وجود ندارد.";
                    CustomAspxDevGridView1.JSProperties["cpError"] = 1;
                    CustomAspxDevGridView1.JSProperties["cpShow"] = 0;

                }

            }
            else
            {
                //btnSave.Visible = false;
                e.Result = "امکان ویرایش اطلاعات مربوط به درخواست های قبل وجود ندارد.";
                CustomAspxDevGridView1.JSProperties["cpError"] = 1;
                CustomAspxDevGridView1.JSProperties["cpShow"] = 0;
            }


        }
    }
    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
    {
        string Mode = Utility.DecryptQS(HDMode.Value);
        switch (e.Item.Name)
        {
            case "Job":
                Response.Redirect("MemberJob.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value);
                break;
            case "Language":
                Response.Redirect("MemberLanguage.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value);
                break;
            case "Member":
                if (Mode == "Home")
                {
                    Response.Redirect("~/Members/MemberHome.aspx?MeId=" + MemberId.Value);
                }
                else

                    Response.Redirect("MemberRequestInsert.aspx?MeId=" + MemberId.Value + "&MReId=" + MemberRequest.Value + "&PageMode=" + Request.QueryString["PageMode"]);

                break;
            case "Madrak":
                Response.Redirect("MemberLicence.aspx?MeId=" + MemberId.Value + "&PageMode=" + Request.QueryString["PageMode"] + "&MReId=" + MemberRequest.Value + "&Mode=" + HDMode.Value);
                break;

        }
    }
    #endregion

    #region Methods
    protected void FillGrid()
    {
        TSP.DataManager.MemberActivitySubjectManager ActManager = new TSP.DataManager.MemberActivitySubjectManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();


        string Mode = Utility.DecryptQS(HDMode.Value);
        string MReId = Utility.DecryptQS(MemberRequest.Value);
        string MeId = Utility.DecryptQS(MemberId.Value);

        switch (Mode)
        {
            case "Home":

                TSP.DataManager.MemberManager MeManager = new TSP.DataManager.MemberManager();
                MeManager.FindByCode(int.Parse(Utility.DecryptQS(MemberId.Value)));
                if (MeManager == null || MeManager.Count == 0)
                {
                    DivReport.Style["visibility"] = "block";
                    this.LabelWarning.Text = "خطایی در صفحه رخ داده است.صفحه را دوباره بارگذاری نمایید.";
                    return;
                }
                if (MeManager[0]["MrsId"].ToString() == "1")//تایید شده
                {
                    Grid_DataBind(ActManager.FindByMeRequest(int.Parse(MeId), -1, 1));
                }
                else
                {
                    Grid_DataBind(ActManager.FindByMeRequest(int.Parse(MeId), -1, -1));
                }

                break;
            case "Request":

                ReqManager.FindByCode(int.Parse(MReId));
                if ((ReqManager[0]["IsConfirm"].ToString() != "0"))
                    Grid_DataBind(ActManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1));
                else
                    Grid_DataBind(ActManager.FindByMeRequest(int.Parse(MeId), int.Parse(MReId), -1, 2));

                break;

        }
        Session["FillMeActivity"] = CustomAspxDevGridView1.DataSource;

    }
    protected void FillGridTempMe()
    {
        TSP.DataManager.TempMemberActivitySubjectManager ActManager = new TSP.DataManager.TempMemberActivitySubjectManager();
        TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();


        string Mode = Utility.DecryptQS(HDMode.Value);
        string MReId = Utility.DecryptQS(MemberRequest.Value);
        string MeId = Utility.DecryptQS(MemberId.Value);

        switch (Mode)
        {
            case "Request":

                ActManager.FindByRequest(int.Parse(MeId), int.Parse(MReId));
                Grid_DataBind(ActManager.DataTable);

                break;

        }
        Session["FillMeActivity"] = CustomAspxDevGridView1.DataSource;

    }
    protected void InsertInActive(int MasId, int MReId, int MeId, TSP.DataManager.MemberActivitySubjectManager ActManager)
    {
        TSP.DataManager.RequestInActivesManager Manager = new TSP.DataManager.RequestInActivesManager();
        DataRow dr = Manager.NewRow();
        dr["TableId"] = MasId;
        dr["TableType"] = (int)TSP.DataManager.TableCodes.MemberActivity;
        dr["ReqId"] = MReId;
        dr["ReqType"] = (int)TSP.DataManager.TableCodes.MemberRequest;
        dr["InActive"] = 1;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        Manager.AddRow(dr);
        Manager.Save();

        DivReport.Style["visibility"] = "block";
        this.LabelWarning.Text = "ذخیره انجام شد";

        Session["FillMeActivity"] = ActManager.FindByMeRequest(MeId, -1, -1, 2);
        Grid_DataBind((DataTable)Session["FillMeActivity"]);

    }
    protected void SetMenuItem()
    {
        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];

            if ((int)arr[0] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[1] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[2] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[3] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
            }
            if ((int)arr[4] == 1)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Member")].Image.Height = Utility.MenuImgSize;
            }

        }
        else
        {
            if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
                CheckMenuImageTempMe(Utility.GetCurrentUser_MeId(), int.Parse(Utility.DecryptQS(MemberRequest.Value)));
            else
                CheckMenuImage(Utility.GetCurrentUser_MeId(), int.Parse(Utility.DecryptQS(MemberRequest.Value)));

        }
    }
    protected void CheckMenuImage(int MeId, int MReId)
    {
        TSP.DataManager.MemberActivitySubjectManager MemberActivitySubjectManager = new TSP.DataManager.MemberActivitySubjectManager();
        TSP.DataManager.MemberLanguageManager MemberLanguageManager = new TSP.DataManager.MemberLanguageManager();
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        TSP.DataManager.ProjectJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.ProjectJobHistoryManager();

        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Licence
        arr.Add(0);//arr[1]-->Job
        arr.Add(0);//arr[2]-->Language
        arr.Add(0);//arr[3]-->Activity
        arr.Add(0);//arr[4]-->Member

        MemberActivitySubjectManager.FindForDelete(MeId, MReId);
        if (MemberActivitySubjectManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
            arr[3] = 1;
        }
        MemberLanguageManager.FindForDelete(MeId, MReId);
        if (MemberLanguageManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
        }

        MemberLicenceManager.FindForDelete(MeId, MReId);
        if (MemberLicenceManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
            arr[0] = 1;
        }
        //ProjectJobHistoryManager.FindForDelete(0, MReId, (int)TSP.DataManager.TableCodes.MemberRequest);
        //if (ProjectJobHistoryManager.Count > 0)
        //{
        //    ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
        //    ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
        //    ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
        //    arr[1] = 1;
        //}

        Session["MenuArrayList"] = arr;
    }
    protected void CheckMenuImageTempMe(int MeId, int MReId)
    {
        TSP.DataManager.TempMemberActivitySubjectManager MemberActivitySubjectManager = new TSP.DataManager.TempMemberActivitySubjectManager();
        TSP.DataManager.TempMemberLanguageManager MemberLanguageManager = new TSP.DataManager.TempMemberLanguageManager();
        TSP.DataManager.TempMemberLicenceManager MemberLicenceManager = new TSP.DataManager.TempMemberLicenceManager();
        TSP.DataManager.TempMemberJobHistoryManager ProjectJobHistoryManager = new TSP.DataManager.TempMemberJobHistoryManager();

        ArrayList arr = new ArrayList();
        arr.Add(0);//arr[0]-->Licence
        arr.Add(0);//arr[1]-->Job
        arr.Add(0);//arr[2]-->Language
        arr.Add(0);//arr[3]-->Activity
        arr.Add(0);//arr[4]-->Member

        MemberActivitySubjectManager.FindByRequest(MeId, MReId);
        if (MemberActivitySubjectManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
            arr[3] = 1;
        }
        MemberLanguageManager.FindByRequest(MeId, MReId);
        if (MemberLanguageManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Language")].Image.Height = Utility.MenuImgSize;
            arr[2] = 1;
        }

        MemberLicenceManager.FindByRequest(MeId, MReId);
        if (MemberLicenceManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Madrak")].Image.Height = Utility.MenuImgSize;
            arr[0] = 1;
        }
        ProjectJobHistoryManager.FindByRequest(MeId, MReId);
        if (ProjectJobHistoryManager.Count > 0)
        {
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Width = Utility.MenuImgSize;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Job")].Image.Height = Utility.MenuImgSize;
            arr[1] = 1;
        }

        Session["MenuArrayList"] = arr;
    }
    protected void CheckMenuImageCurrentPage(int MeId, int MReId)
    {
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            CheckMenuImageCurrentPageTempMember(MeId, MReId);
        else
            CheckMenuImageCurrentPageMember(MeId, MReId);
    }
    protected void CheckMenuImageCurrentPageMember(int MeId, int MReId)
    {
        TSP.DataManager.MemberActivitySubjectManager MemberActivitySubjectManager = new TSP.DataManager.MemberActivitySubjectManager();
        MemberActivitySubjectManager.FindByMeRequest(MeId, MReId, -1);

        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            if (MemberActivitySubjectManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
                arr[3] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Url = "";
                arr[3] = 0;

            }
            Session["MenuArrayList"] = arr;
        }
        else
        {
            CheckMenuImage(MeId, MReId);
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            if (MemberActivitySubjectManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
                arr[3] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Url = "";
                arr[3] = 0;

            }
            Session["MenuArrayList"] = arr;
        }

    }
    protected void CheckMenuImageCurrentPageTempMember(int MeId, int MReId)
    {
        TSP.DataManager.TempMemberActivitySubjectManager MemberActivitySubjectManager = new TSP.DataManager.TempMemberActivitySubjectManager();
        MemberActivitySubjectManager.FindByRequest(MeId, MReId);

        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            if (MemberActivitySubjectManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
                arr[3] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Url = "";
                arr[3] = 0;

            }
            Session["MenuArrayList"] = arr;
        }
        else
        {
            CheckMenuImage(MeId, MReId);
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            if (MemberActivitySubjectManager.Count > 0)
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Width = Utility.MenuImgSize;
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Height = Utility.MenuImgSize;
                arr[3] = 1;
            }
            else
            {
                ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Url = "";
                arr[3] = 0;

            }
            Session["MenuArrayList"] = arr;
        }

    }
    protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        if (Session["MenuArrayList"] != null)
        {
            ArrayList arr = (ArrayList)Session["MenuArrayList"];
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Url = "~/Images/icons/Check.png";
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Width = 15;
            ASPxMenu1.Items[ASPxMenu1.Items.IndexOfName("Activity")].Image.Height = 15;
            arr[3] = 1;
            Session["MenuArrayList"] = arr;
        }
        else
            CheckMenuImageCurrentPage(Utility.GetCurrentUser_MeId(), int.Parse(Utility.DecryptQS(MemberRequest.Value)));
    }
    void Grid_DataBind(DataTable DataSource)
    {
        CustomAspxDevGridView1.DataSource = DataSource;
        if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.Member)
            CustomAspxDevGridView1.KeyFieldName = "MasId";
        else if (Utility.GetCurrentUser_LoginType() == (int)TSP.DataManager.UserType.TemporaryMembers)
            CustomAspxDevGridView1.KeyFieldName = "TMasId";
        CustomAspxDevGridView1.DataBind();
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        int TableType = (int)TSP.DataManager.TableCodes.MemberRequest;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveMemberInfoForConfirming;

            if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
            {
                return true;
            }

        }
        return false;


    }
    #endregion
}
