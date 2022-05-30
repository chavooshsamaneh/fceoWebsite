using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Employee_TechnicalServices_Capacity_ObserverRestriction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewObsRestriction.JSProperties["cpError"] = 2;
            TSP.DataManager.Permission per = TSP.DataManager.TechnicalServices.MemberRestrictionForObserverWorkRequestManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnDelete.Enabled = per.CanDelete;
            btnDelete2.Enabled = per.CanDelete;
            //btnEdit.Enabled = per.CanEdit;
            //btnEdit2.Enabled = per.CanEdit;
            BtnNew.Enabled = per.CanNew;
            BtnNew2.Enabled = per.CanNew;
            GridViewObsRestriction.Visible = per.CanView;

            //this.ViewState["BtnEdit"] = btnEdit.Enabled;
            this.ViewState["BtnDelete"] = btnDelete.Enabled;
            this.ViewState["BtnNew"] = BtnNew.Enabled;
        }

        //if (this.ViewState["BtnEdit"] != null)
        //    this.btnEdit.Enabled = this.btnEdit2.Enabled = (bool)this.ViewState["BtnEdit"];
        if (this.ViewState["BtnDelete"] != null)
            this.btnDelete.Enabled = this.btnDelete2.Enabled = (bool)this.ViewState["BtnDelete"];
        if (this.ViewState["BtnNew"] != null)
            this.BtnNew.Enabled = this.BtnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = true;
        Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.MemberRestrictionForObserverWorkRequestManager MemberRestrictionForObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.MemberRestrictionForObserverWorkRequestManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
        TSP.DataManager.TechnicalServices.WorkFlowStateObserverWorkManager WorkFlowStateManager = new TSP.DataManager.TechnicalServices.WorkFlowStateObserverWorkManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TransactionManager.Add(MemberRestrictionForObserverWorkRequestManager);
        TransactionManager.Add(ObserverWorkRequestManager);
        TransactionManager.Add(ObserverWorkRequestChangesManager);
        TransactionManager.Add(WorkFlowStateManager);

        DataRow row = GridViewObsRestriction.GetDataRow(GridViewObsRestriction.FocusedRowIndex);
        try
        {
            TransactionManager.BeginSave();
            int MeId = (int)row["MeId"];
            MemberRestrictionForObserverWorkRequestManager.FindByMembeRestrictionId((int)row["MembeRestrictionId"]);
            if (MemberRestrictionForObserverWorkRequestManager.Count == 1)
            {
                if (Convert.ToInt32(MemberRestrictionForObserverWorkRequestManager[0]["InActive"]) == 1)
                {
                    GridViewObsRestriction.JSProperties["cpMsg"] = "وضعیت عضو انتخاب شده باید در حالت فعال باشد.";
                    TransactionManager.CancelSave();
                    return;
                }
                MemberRestrictionForObserverWorkRequestManager[0].BeginEdit();

                MemberRestrictionForObserverWorkRequestManager[0]["InActive"] = 1;
                MemberRestrictionForObserverWorkRequestManager[0]["InActiveDate"] = Utility.GetDateOfToday();
                MemberRestrictionForObserverWorkRequestManager[0]["InActiveUserId"] = Utility.GetCurrentUser_UserId();
                MemberRestrictionForObserverWorkRequestManager[0]["ModifiedDate"] = DateTime.Now;

                MemberRestrictionForObserverWorkRequestManager[0].EndEdit();
                if (MemberRestrictionForObserverWorkRequestManager.Save() <= 0)
                {
                    GridViewObsRestriction.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است.";
                    TransactionManager.CancelSave();
                    return;
                }
                #region بروزرسانی آماده بکاری
                ObserverWorkRequestManager.FindByMeId(MeId);
                if (ObserverWorkRequestManager.Count != 0)
                {
                    int LastMfId = -1;
                    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                    DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
                    if (dtMeFile.Rows.Count <= 0)
                    {
                        GridViewObsRestriction.JSProperties["cpMsg"] = "برای این کد عضویت پروانه اشتغال به کار تایید شده وجود ندارد.";
                        TransactionManager.CancelSave();
                        return;
                    }
                    LastMfId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());

                    int per = ObserverWorkRequestChangesManager.DoAutomaticObserverWorkRequestChange(MeId, "ثبت درخواست تغییرات آماده بکاری به دلیل غیرفعال شدن در اعضای محروم از نظارت", "تایید اتوماتیک درخواست تغییرات آماده بکاری به دلیل غیرفعال شدن در اعضای محروم از نظارت"
                       , TSP.DataManager.TSObserverWorkRequestChangeType.InActiveObserverRestrictionMembership, Utility.GetCurrentUser_UserId(), LastMfId, dtMeFile.Rows[0]["ExpireDate"].ToString(), -2, TransactionManager);
                    if (per != 0)
                    {
                        TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
                        string ErrorMsg = WorkFlowPermission.FindRequestErrorMsg(per);
                        GridViewObsRestriction.JSProperties["cpMsg"] = ErrorMsg;
                        TransactionManager.CancelSave();
                        return;
                    }
                }
                #endregion

                TransactionManager.EndSave();
                GridViewObsRestriction.JSProperties["cpMsg"] = "ذخیره انجام شد.";
                GridViewObsRestriction.CancelEdit();

                GridViewObsRestriction.DataBind();

            }
            GridViewObsRestriction.DataBind();
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    GridViewObsRestriction.JSProperties["cpMsg"] = "اطلاعات تکراری می باشد";
                }
                else
                {
                    GridViewObsRestriction.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                GridViewObsRestriction.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }
    protected void GridViewObsRestriction_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.MemberRestrictionForObserverWorkRequestManager MemberRestrictionForObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.MemberRestrictionForObserverWorkRequestManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
        TSP.DataManager.TechnicalServices.WorkFlowStateObserverWorkManager WorkFlowStateManager = new TSP.DataManager.TechnicalServices.WorkFlowStateObserverWorkManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TransactionManager.Add(MemberRestrictionForObserverWorkRequestManager);
        TransactionManager.Add(ObserverWorkRequestManager);
        TransactionManager.Add(ObserverWorkRequestChangesManager);
        TransactionManager.Add(WorkFlowStateManager);

        GridViewObsRestriction.JSProperties["cpError"] = 1;
        try
        {
            MemberRestrictionForObserverWorkRequestManager.FindActiveMembersId(Convert.ToInt32(e.NewValues["MeId"]));
            if (MemberRestrictionForObserverWorkRequestManager.Count > 0)
            {
                GridViewObsRestriction.JSProperties["cpMsg"] = "عضویت مورد نظر در لیست اعضای محروم از نظارت وجود دارد.";
                GridViewObsRestriction.CancelEdit();
                return;
            }
            int LastMfId = -1;
            string ExpireDate = "";
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(Convert.ToInt32(e.NewValues["MeId"]), 0, 1);
            if (dtMeFile.Rows.Count > 0)
            {
                LastMfId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
                ExpireDate = dtMeFile.Rows[0]["ExpireDate"].ToString();
            }

            MemberManager.FindByCode(Convert.ToInt32(e.NewValues["MeId"]));
            if (MemberManager.Count == 0)
            {
                TransactionManager.CancelSave();
                GridViewObsRestriction.JSProperties["cpMsg"] = "خطایی در بازیابی اطلاعات رخ داده است";
                return;
            }
            if (Utility.IsDBNullOrNullValue(MemberManager[0]["AgentId"]))
            {
                TransactionManager.CancelSave();
                GridViewObsRestriction.JSProperties["cpMsg"] = "نمایندگی عضو مورد نظر در واحد عضویت نامشخص می باشد";
                return;
            }
            int AgentId = Convert.ToInt32(MemberManager[0]["AgentId"]);

            TransactionManager.BeginSave();
            #region SaveMemberRestriction
            DataRow row = MemberRestrictionForObserverWorkRequestManager.NewRow();
            row["Type"] = e.NewValues["Type"];
            row["MeId"] = e.NewValues["MeId"];
            row["CreateDate"] = Utility.GetDateOfToday();
            row["InActive"] = 0;
            row["Description"] = e.NewValues["Description"];
            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;
            MemberRestrictionForObserverWorkRequestManager.AddRow(row);
            MemberRestrictionForObserverWorkRequestManager.Save();
            MemberRestrictionForObserverWorkRequestManager.DataTable.AcceptChanges();

            #endregion

            DataTable dtObsWorkReq = ObserverWorkRequestManager.SelectTSObserverWorkRequestByMember(Convert.ToInt32(e.NewValues["MeId"]), TSP.DataManager.TSObserverWorkRequestStatus.All);

            if (dtObsWorkReq.Rows.Count > 1)
            {
                TransactionManager.CancelSave();
                GridViewObsRestriction.JSProperties["cpMsg"] = "خطایی در بازیابی اطلاعات رخ داده است";
                return;
            }
            if (dtObsWorkReq.Rows.Count == 1 &&
                (Convert.ToInt32(e.NewValues["Type"]) == (int)TSP.DataManager.MemberRestrictionTypeForObserverWorkRequest.ShorayeEntezami
                || Convert.ToInt32(e.NewValues["Type"]) == (int)TSP.DataManager.MemberRestrictionTypeForObserverWorkRequest.AnbohSazan
                || Convert.ToInt32(e.NewValues["Type"]) == (int)TSP.DataManager.MemberRestrictionTypeForObserverWorkRequest.SherkatAzemayeshgahi
                || Convert.ToInt32(e.NewValues["Type"]) == (int)TSP.DataManager.MemberRestrictionTypeForObserverWorkRequest.PeymanKar
                ))
            {
                #region save obsRequest
                string Des = "";
                TSP.DataManager.TSObserverWorkRequestChangeType RequestChangeType = TSP.DataManager.TSObserverWorkRequestChangeType.Change;
                switch (Convert.ToInt32(e.NewValues["Type"]))
                {
                    case (int)TSP.DataManager.MemberRestrictionTypeForObserverWorkRequest.ShorayeEntezami:
                        RequestChangeType = TSP.DataManager.TSObserverWorkRequestChangeType.ShorayeEntezami;
                        Des = " به دلیل حکم شورای انتظامی";
                        break;

                    case (int)TSP.DataManager.MemberRestrictionTypeForObserverWorkRequest.AnbohSazan:
                        RequestChangeType = TSP.DataManager.TSObserverWorkRequestChangeType.AnbohSazan;
                        Des = " به دلیل عضویت در انبوه سازان";
                        break;

                    case (int)TSP.DataManager.MemberRestrictionTypeForObserverWorkRequest.SherkatAzemayeshgahi:
                        RequestChangeType = TSP.DataManager.TSObserverWorkRequestChangeType.SherkatAzemayeshgahi;
                        Des = " به دلیل عضویت در شرکت آزمایشگاهی";
                        break;

                    case (int)TSP.DataManager.MemberRestrictionTypeForObserverWorkRequest.PeymanKar:
                        RequestChangeType = TSP.DataManager.TSObserverWorkRequestChangeType.PeymanKar;
                        Des = " به دلیل ثبت به عنوان پیمانکار";
                        break;
                }
                int per = ObserverWorkRequestChangesManager.DoAutomaticObserverWorkRequestChange(Convert.ToInt32(e.NewValues["MeId"]), "ثبت درخواست تغییرات آماده بکاری" + Des, "تایید اتوماتیک درخواست تغییرات آماده بکاری توسط مدیر سیستم " + Des
                   , RequestChangeType, Utility.GetCurrentUser_UserId(), LastMfId, ExpireDate, AgentId, TransactionManager);
                if (per != 0)
                {
                    TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
                    string ErrorMsg = WorkFlowPermission.FindRequestErrorMsg(per);
                    TransactionManager.CancelSave();
                    GridViewObsRestriction.JSProperties["cpMsg"] = ErrorMsg;
                    return;
                }
                #endregion
            }


            TransactionManager.EndSave();
            GridViewObsRestriction.JSProperties["cpMsg"] = "ذخیره انجام گرفت.";

            GridViewObsRestriction.CancelEdit();
            GridViewObsRestriction.DataBind();
        }
        catch (Exception err)
        {
            e.Cancel = true;
            TransactionManager.CancelSave();
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    GridViewObsRestriction.JSProperties["cpMsg"] = "اطلاعات تکراری می باشد";
                }
                else if (se.Number == 2627)
                {
                    GridViewObsRestriction.JSProperties["cpMsg"] = "کد شهر تکراری می باشد.";
                }
                else
                {
                    GridViewObsRestriction.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                GridViewObsRestriction.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }
    protected void GridViewObsRestriction_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;
        GridViewObsRestriction.JSProperties["cpError"] = 1;
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.MemberRestrictionForObserverWorkRequestManager MemberRestrictionForObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.MemberRestrictionForObserverWorkRequestManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
        TSP.DataManager.TechnicalServices.WorkFlowStateObserverWorkManager WorkFlowStateManager = new TSP.DataManager.TechnicalServices.WorkFlowStateObserverWorkManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TransactionManager.Add(MemberRestrictionForObserverWorkRequestManager);
        TransactionManager.Add(ObserverWorkRequestManager);
        TransactionManager.Add(ObserverWorkRequestChangesManager);
        TransactionManager.Add(WorkFlowStateManager);

        try
        {
            int MeId = Convert.ToInt32(e.Values["MeId"]);
            TransactionManager.BeginSave();
            MemberRestrictionForObserverWorkRequestManager.FindByMembeRestrictionId(Convert.ToInt32(e.Keys["MembeRestrictionId"]));
            if (MemberRestrictionForObserverWorkRequestManager.Count == 1 )
            {
                if (Convert.ToBoolean(MemberRestrictionForObserverWorkRequestManager[0]["InActive"]))
                {
                    GridViewObsRestriction.JSProperties["cpMsg"] = "وضعیت عضو انتخاب شده باید در حالت فعال باشد.";
                    TransactionManager.CancelSave();
                    return;
                }
                MemberRestrictionForObserverWorkRequestManager[0].BeginEdit();
                MemberRestrictionForObserverWorkRequestManager[0]["InActive"] = 1;
                MemberRestrictionForObserverWorkRequestManager[0]["InActiveDate"] = Utility.GetDateOfToday();
                MemberRestrictionForObserverWorkRequestManager[0]["InActiveUserId"] = Utility.GetCurrentUser_UserId();
                MemberRestrictionForObserverWorkRequestManager[0]["ModifiedDate"] = DateTime.Now;
                MemberRestrictionForObserverWorkRequestManager[0].EndEdit();
                if (MemberRestrictionForObserverWorkRequestManager.Save() <= 0)
                {
                    GridViewObsRestriction.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است.";
                    TransactionManager.CancelSave();
                    return;
                }
                
                #region بروزرسانی آماده بکاری
                ObserverWorkRequestManager.FindByMeId(MeId);
                if (ObserverWorkRequestManager.Count != 0)
                {
                    int LastMfId = -1;
                    TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                    DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
                    if (dtMeFile.Rows.Count <= 0)
                    {
                        GridViewObsRestriction.JSProperties["cpMsg"] = "برای این کد عضویت پروانه اشتغال به کار تایید شده وجود ندارد.";
                        TransactionManager.CancelSave();
                        return;
                    }
                    LastMfId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());

                    int per = ObserverWorkRequestChangesManager.DoAutomaticObserverWorkRequestChange(MeId, "ثبت درخواست تغییرات آماده بکاری به دلیل غیرفعال شدن در اعضای محروم از نظارت", "تایید اتوماتیک درخواست تغییرات آماده بکاری به دلیل غیرفعال شدن در اعضای محروم از نظارت"
                       , TSP.DataManager.TSObserverWorkRequestChangeType.InActiveObserverRestrictionMembership, Utility.GetCurrentUser_UserId(), LastMfId, dtMeFile.Rows[0]["ExpireDate"].ToString(), -2, TransactionManager);
                    if (per != 0)
                    {
                        TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
                        string ErrorMsg = WorkFlowPermission.FindRequestErrorMsg(per);
                        GridViewObsRestriction.JSProperties["cpMsg"] = ErrorMsg;
                        TransactionManager.CancelSave();
                        return;
                    }
                }
                #endregion

                TransactionManager.EndSave();
                GridViewObsRestriction.CancelEdit();

                GridViewObsRestriction.DataBind();

            }
            else
            {
                GridViewObsRestriction.JSProperties["cpMsg"] = "قبلا این مورد غیرفعال شده است.";
            }
        }
        catch (Exception err)
        {
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 2601)
                {
                    GridViewObsRestriction.JSProperties["cpMsg"] = "اطلاعات تکراری می باشد";
                }
                else
                {
                    GridViewObsRestriction.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است";
                }
            }
            else
            {
                GridViewObsRestriction.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است";
            }
        }
    }
}
