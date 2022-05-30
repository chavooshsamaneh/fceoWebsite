using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Employee_Document_GasOfficeMembersManagement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewGasOfficeMembersManagment.JSProperties["cpError"] = 2;

            TSP.DataManager.Permission per = TSP.DataManager.DocGasOfficeMembersManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnInActive.Enabled = per.CanDelete;
            btnInActive2.Enabled = per.CanDelete;
            btnOff.Enabled = per.CanEdit;
            btnOff2.Enabled = per.CanEdit;
            btnNew.Enabled = per.CanNew;
            btnNew2.Enabled = per.CanNew;


            this.ViewState["BtnOff"] = btnOff.Enabled;
            this.ViewState["BtnInActive"] = btnInActive.Enabled;
            this.ViewState["BtnNew"] = btnNew.Enabled;
        }

        if (this.ViewState["BtnOff"] != null)
            this.btnOff.Enabled = this.btnOff2.Enabled = (bool)this.ViewState["BtnOff"];
        if (this.ViewState["BtnInActive"] != null)
            this.btnInActive.Enabled = this.btnInActive2.Enabled = (bool)this.ViewState["BtnInActive"];
        if (this.ViewState["BtnNew"] != null)
            this.btnNew.Enabled = this.btnNew2.Enabled = (bool)this.ViewState["BtnNew"];

        this.DivReport.Visible = true;
        Page.ClientScript.RegisterStartupScript(GetType(), "Key", "<script>document.getElementById('" + DivReport.ClientID + "').style.visibility='hidden'; </script>");
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }



    protected void GridViewGasOfficeMembersManagment_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Cancel = true;
        GridViewGasOfficeMembersManagment.JSProperties["cpError"] = 1;
        TSP.DataManager.DocGasOfficeMembersManager DocGasOfficeMembersManager = new TSP.DataManager.DocGasOfficeMembersManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TransactionManager.Add(ObserverWorkRequestChangesManager);
        TransactionManager.Add(ObserverWorkRequestManager);
        TransactionManager.Add(DocGasOfficeMembersManager);
        try
        {
            DocGasOfficeMembersManager.FindByGasMeId(Convert.ToInt32(e.Keys["GasMeId"]));
            if (DocGasOfficeMembersManager.Count == 1 && Convert.ToInt32(DocGasOfficeMembersManager[0]["Status"]) != 1)
            {
                GridViewGasOfficeMembersManagment.JSProperties["cpMsg"] = "وضعیت عضو انتخاب شده باید در حالت فعال باشد.";
                return;
            }
        }
        catch (Exception ex)
        {
            e.Cancel = true;
            GridViewGasOfficeMembersManagment.JSProperties["cpMsg"] = ex.ToString();
            hiddenFieldPage["btnOff"] = 0;
            hiddenFieldPage["btnInActive"] = 0;
        }
        try
        {

            TransactionManager.BeginSave();
            DocGasOfficeMembersManager[0].BeginEdit();
            if (hiddenFieldPage["btnOff"].ToString() == "1")
            {
                DocGasOfficeMembersManager[0]["Status"] = TSP.DataManager.GasOfficeMemberStatus.Off;
            }
            if (hiddenFieldPage["btnInActive"].ToString() == "1")
            {
                DocGasOfficeMembersManager[0]["Status"] = TSP.DataManager.GasOfficeMemberStatus.InActive;
            }

            DocGasOfficeMembersManager[0]["StatusChangeDate"] = Utility.GetDateOfToday();
            DocGasOfficeMembersManager[0]["UserId"] = Utility.GetCurrentUser_UserId();
            DocGasOfficeMembersManager[0]["ModifiedDate"] = DateTime.Now;
            DocGasOfficeMembersManager[0].EndEdit();
            if (DocGasOfficeMembersManager.Save() <= 0)
            {
                TransactionManager.CancelSave();
                GridViewGasOfficeMembersManagment.JSProperties["cpMsg"] = "خطایی در ذخیره انجام گرفته است.";
                return;
            }
            int MeId = Convert.ToInt32(DocGasOfficeMembersManager[0]["MeId"]);
            #region بروزرسانی آماده بکاری
            ObserverWorkRequestManager.FindByMeId(MeId);
            if (ObserverWorkRequestManager.Count != 0)
            {
                int LastMfId = -1;
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
                if (dtMeFile.Rows.Count <= 0)
                {
                    GridViewGasOfficeMembersManagment.JSProperties["cpMsg"] = "برای این کد عضویت پروانه اشتغال به کار تایید شده وجود ندارد.";
                    TransactionManager.CancelSave();
                    return;
                }
                LastMfId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());

                int per = ObserverWorkRequestChangesManager.DoAutomaticObserverWorkRequestChange(MeId, "ثبت درخواست تغییرات آماده بکاری به دلیل غیرفعال/مرخصی در عضویت دفتر گاز", "تایید اتوماتیک درخواست تغییرات آماده بکاری به دلیل غیرفعال/مرخصی در عضویت دفتر گاز"
                   , TSP.DataManager.TSObserverWorkRequestChangeType.InActiveGasOfficeMembership, Utility.GetCurrentUser_UserId(), LastMfId, dtMeFile.Rows[0]["ExpireDate"].ToString(), -2, TransactionManager);
                if (per != 0)
                {
                    TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
                    string ErrorMsg = WorkFlowPermission.FindRequestErrorMsg(per);
                    GridViewGasOfficeMembersManagment.JSProperties["cpMsg"] = ErrorMsg;
                    TransactionManager.CancelSave();
                    return;
                }
            }
            #endregion

            TransactionManager.EndSave();
            GridViewGasOfficeMembersManagment.JSProperties["cpMsg"] = "ذخیره انجام شد.";

            hiddenFieldPage["btnOff"] = 0;
            hiddenFieldPage["btnInActive"] = 0;
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            e.Cancel = true;
            GridViewGasOfficeMembersManagment.JSProperties["cpMsg"] = err.ToString();
            hiddenFieldPage["btnOff"] = 0;
            hiddenFieldPage["btnInActive"] = 0;
        }

    }

    protected void GridViewGasOfficeMembersManagment_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
        GridViewGasOfficeMembersManagment.JSProperties["cpError"] = 1;
        int MeId = -1;
        if (Utility.IsDBNullOrNullValue(e.NewValues["MeId"]))
            return;
        int.TryParse(e.NewValues["MeId"].ToString(), out MeId);
        if (MeId == -1)
        {
            GridViewGasOfficeMembersManagment.JSProperties["cpMsg"] = "کد عضویت وارد شده صحیح نمی باشد.";
            return;
        }
        TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
        DataTable dt = MemberLicenceManager.SelectMemberLicenceByMeIdForGasMemberOffice(MeId, Convert.ToInt32(TSP.DataManager.MainMajors.Mechanic));
        if (dt.Rows.Count == 0)
        {
            GridViewGasOfficeMembersManagment.JSProperties["cpMsg"] = "عضو مورد نظر دارای مدرک مهندسی مکانیک نمی باشد.";
            return;
        }

        TSP.DataManager.DocGasOfficeMembersManager DocGasOfficeMembersManager = new TSP.DataManager.DocGasOfficeMembersManager();
        DocGasOfficeMembersManager.FindByMeId(MeId, Convert.ToInt32(TSP.DataManager.GasOfficeMemberStatus.Confirmed));
        if (DocGasOfficeMembersManager.Count == 1)
        {
            GridViewGasOfficeMembersManagment.JSProperties["cpMsg"] = "در حال حاضر اطلاعات عضو مورد به عنوان عضو فعال دفتر گاز ثبت است .";
            return;
        }
        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TransactionManager.Add(ObserverWorkRequestChangesManager);
        TransactionManager.Add(ObserverWorkRequestManager);
        TransactionManager.Add(DocGasOfficeMembersManager);

        try
        {
            TransactionManager.BeginSave();
            #region GasOfficeMembers
            DataRow row = DocGasOfficeMembersManager.NewRow();

            row["MeId"] = e.NewValues["MeId"];
            row["CreateDate"] = Utility.GetDateOfToday();
            row["Status"] = TSP.DataManager.GasOfficeMemberStatus.Confirmed;
            row["InActive"] = 1;
            row["Description"] = e.NewValues["Description"];
            row["UserId"] = Utility.GetCurrentUser_UserId();
            row["ModifiedDate"] = DateTime.Now;
            DocGasOfficeMembersManager.AddRow(row);
            DocGasOfficeMembersManager.Save();
            DocGasOfficeMembersManager.DataTable.AcceptChanges();

            #endregion

            #region بروزرسانی آماده بکاری
            ObserverWorkRequestManager.FindByMeId(MeId);
            if (ObserverWorkRequestManager.Count != 0)
            {
                int sum = Convert.ToInt32(ObserverWorkRequestManager[0]["ShirazMunicipalityMeter"])
                    + Convert.ToInt32(ObserverWorkRequestManager[0]["ShirazMunicipalityDesignMeter"])
                     + Convert.ToInt32(ObserverWorkRequestManager[0]["KhanZenyanObserveMeter"])
                     + Convert.ToInt32(ObserverWorkRequestManager[0]["LapooyObserveMeter"])
                     + Convert.ToInt32(ObserverWorkRequestManager[0]["ZarghanObserveMeter"])
                     + Convert.ToInt32(ObserverWorkRequestManager[0]["DareyonObserveMeter"])
                     + Convert.ToInt32(ObserverWorkRequestManager[0]["BonyadMaskan"])
                     + Convert.ToInt32(ObserverWorkRequestManager[0]["BonyadMaskanDesignMeter"]);
                int TotalCapacity = Convert.ToInt32(ObserverWorkRequestManager[0]["TotalCapacity"]);

                if (sum > TotalCapacity - (TotalCapacity / 4))
                {
                    GridViewGasOfficeMembersManagment.JSProperties["cpMsg"] = "عضو انتخاب شده دارای کسری ظرفیت جهت بردن 25% از ظرفیت خود جهت عضویت در دفتر گاز می باشد.پیش از ذخیره این عضو به عنوان عضو دفتر گاز بایستی از پرتال عضو درخواست تغییرات جهت کاهش 25% از مجموع ظرفیت های برده شده به مراکز کاری مختلف(شهرداری ها و بنیاد مسکن) ثبت شود";
                    TransactionManager.CancelSave();
                    return;
                }
                int LastMfId = -1;
                TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
                DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
                if (dtMeFile.Rows.Count <= 0)
                {
                    GridViewGasOfficeMembersManagment.JSProperties["cpMsg"] = "برای این کد عضویت پروانه اشتغال به کار تایید شده وجود ندارد.";
                    TransactionManager.CancelSave();
                    return;
                }
                LastMfId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());

                int per = ObserverWorkRequestChangesManager.DoAutomaticObserverWorkRequestChange(MeId, "ثبت درخواست تغییرات آماده بکاری به دلیل ثبت در عضویت دفتر گاز", "تایید اتوماتیک درخواست تغییرات آماده بکاری به دلیل ثبت در عضویت دفتر گاز"
                   , TSP.DataManager.TSObserverWorkRequestChangeType.GasOfficeMembership, Utility.GetCurrentUser_UserId(), LastMfId, dtMeFile.Rows[0]["ExpireDate"].ToString(), -2, TransactionManager);
                if (per != 0)
                {
                    TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
                    string ErrorMsg = WorkFlowPermission.FindRequestErrorMsg(per);
                    GridViewGasOfficeMembersManagment.JSProperties["cpMsg"] = ErrorMsg;
                    TransactionManager.CancelSave();
                    return;
                }
            }
            #endregion
            TransactionManager.EndSave();
            GridViewGasOfficeMembersManagment.JSProperties["cpMsg"] = "ذخیره انجام گرفت.";

            GridViewGasOfficeMembersManagment.CancelEdit();
            GridViewGasOfficeMembersManagment.DataBind();
        }
        catch (Exception err)
        {
            TransactionManager.CancelSave();
            e.Cancel = true;
            GridViewGasOfficeMembersManagment.JSProperties["cpMsg"] = err.ToString();
        }
    }


    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "GasMember";
        GridViewExporter.WriteXlsToResponse(true);
    }
}