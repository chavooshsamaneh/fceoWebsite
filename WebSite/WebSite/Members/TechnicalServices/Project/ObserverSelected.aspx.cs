using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Members_TechnicalServices_Project_ObserverSelected : System.Web.UI.Page
{

    int _CountRejectByObs
    {
        get
        {
            return Convert.ToInt32(HDpage["CountRejectByObs"]);
        }
        set
        {
            HDpage["CountRejectByObs"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.DivReport.Visible = false;
            this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
            this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        }
        //در قسمت پست بک قرار ندادیم چون اگر 1 رد کار بود و به طور همزمان دو تا کار را در یک زمان رد کرد تعداد  پارامتر بروزرسانی شود و از کار سوم جریمه رد کار حساب شود
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        ObserverWorkRequestManager.FindByMeId(Utility.GetCurrentUser_MeId());
        if (ObserverWorkRequestManager.Count != 0)
        {
            _CountRejectByObs = Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["CountRejectByObs"]) ? 0 : Convert.ToInt32(ObserverWorkRequestManager[0]["CountRejectByObs"]);
            //   lblWarning.Text = "تعداد رد کار : "+_CountRejectByObs.ToString();
        }
        ObjectDataSourceSelectObs.SelectParameters["MeId"].DefaultValue = Utility.GetCurrentUser_MeId().ToString();
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        int ProjectObserverSelectedId = -2;
        if (GridViewObserverSelected.FocusedRowIndex > -1)
        {
            System.Data.DataRow row = GridViewObserverSelected.GetDataRow(GridViewObserverSelected.FocusedRowIndex);
            ProjectObserverSelectedId = (int)row["ProjectObserverSelectedId"];
        }
        else
        {
            ShowMessage("لطفا یک ردیف انتخاب نمایید.");
        }
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager ProjectObserverSelectedManager = new TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();
        TransactionManager.Add(ProjectObserverSelectedManager);
        TransactionManager.Add(ObserverWorkRequestManager);
        TransactionManager.Add(ProjectCapacityDecrementManager);
        TransactionManager.Add(RequestInActivesManager);

        try
        {
            ProjectObserverSelectedManager.FindByCode(ProjectObserverSelectedId);
            if (ProjectObserverSelectedManager.Count == 0)
                return;


            string CrtEndDate = ProjectObserverSelectedManager[0]["CreateDate"].ToString();
            int ProjectRequestId = Convert.ToInt32(ProjectObserverSelectedManager[0]["ProjectRequestId"]);
            int ProjectId = Convert.ToInt32(ProjectObserverSelectedManager[0]["ProjectId"]);
            int ProjectObserversId = Convert.ToInt32(ProjectObserverSelectedManager[0]["ProjectObserversId"]);
            int _CitId = Convert.ToInt32(ProjectObserverSelectedManager[0]["CitId"]);
            int _IsCharity = Convert.ToInt32(ProjectObserverSelectedManager[0]["IsCharity"]);
            int CapacityDecrement = Convert.ToInt32(ProjectObserverSelectedManager[0]["CapacityDecrement"]);
            if (Convert.ToInt32(ProjectObserverSelectedManager[0]["IsObserverConfirmed"]) == (int)TSP.DataManager.TSProjectObserverSelectedConfirmType.AcceptWorkByObserver)
            {
                ShowMessage("امکان عدم قبول کار وجود ندارد.پیش از این شما قبول این کار را ثبت کرده اید.");
                return;
            }

            if (Convert.ToInt32(ProjectObserverSelectedManager[0]["IsObserverConfirmed"]) == (int)TSP.DataManager.TSProjectObserverSelectedConfirmType.RejectWorkByObserve)
            {
                ShowMessage("امکان عدم قبول کار وجود ندارد.پیش از این شما رد این کار را ثبت کرده اید.");
                return;
            }
            if (Convert.ToInt32(ProjectObserverSelectedManager[0]["IsObserverConfirmed"]) == (int)TSP.DataManager.TSProjectObserverSelectedConfirmType.RejectWorkByNezam)
            {
                ShowMessage("امکان عدم قبول کار وجود ندارد.پیش از این رد این کار توسط کارشناسان سازمان ثبت شده است.");
                return;
            }
            Utility.Date objDate = new Utility.Date(CrtEndDate);
            string TheDayBefore = objDate.AddDays(2);
            string Today = Utility.GetDateOfToday();
            int IsDocExp = string.Compare(Today, TheDayBefore);
            if (IsDocExp >= 0)
            {
                ShowMessage("امکان رد کار وجود ندارد.از زمان ارجاع کار نظارت به شما بیش از دو روز گذشته است و شما ناظر این پروژه می باشید.");
                return;
            }
            TransactionManager.BeginSave();
            ProjectObserverSelectedManager[0].BeginEdit();
            ProjectObserverSelectedManager[0]["IsObserverConfirmed"] = (int)TSP.DataManager.TSProjectObserverSelectedConfirmType.RejectWorkByObserve;
            ProjectObserverSelectedManager[0]["RejectDate"] = Utility.GetDateOfToday();
            ProjectObserverSelectedManager[0].EndEdit();
            if (ProjectObserverSelectedManager.Save() <= 0)
            {
                ShowMessage("خطا در ذخیره ایجاد شده است.");
                TransactionManager.CancelSave();
                return;
            }
            InsertInActive(ProjectObserversId, ProjectRequestId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers), TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest), RequestInActivesManager);
            ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(ProjectId, ProjectObserversId, (int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer);
            if (ProjectCapacityDecrementManager.Count != 1)
            {
                ShowMessage("خطا در بازیابی اطلاعات ایجاد شده است.");
                TransactionManager.CancelSave();
                return;
            }
            ProjectCapacityDecrementManager[0].BeginEdit();
            if (_CountRejectByObs >= 1)
            {
                Utility.Date objDateFree = new Utility.Date(Utility.GetDateOfToday());
                string FineExpireDate = objDateFree.AddMonths(6);
                ProjectCapacityDecrementManager[0]["IsFine"] = 1;
                ProjectCapacityDecrementManager[0]["FineExpireDate"] = FineExpireDate;
            }

            ProjectCapacityDecrementManager[0]["IsFree"] = (int)TSP.DataManager.TSProjectCapacityDecrementIsFreeType.Free;
            ProjectCapacityDecrementManager[0]["FreeDate"] = Utility.GetDateOfToday();
            ProjectCapacityDecrementManager[0]["IsWorkFree"] = (int)TSP.DataManager.TSProjectCapacityDecrementIsFreeType.Free;
            ProjectCapacityDecrementManager[0]["WorkFreeDate"] = Utility.GetDateOfToday();

            ProjectCapacityDecrementManager[0].EndEdit();
            ProjectCapacityDecrementManager.Save();
            ProjectCapacityDecrementManager.DataTable.AcceptChanges();
            CapacityCalculations CapacityCalculations = new CapacityCalculations();
            CapacityCalculations.UpdateWorkRequestCapacityData(ObserverWorkRequestManager, ProjectCapacityDecrementManager, Utility.GetCurrentUser_MeId(), Utility.GetCurrentUser_UserId(), ProjectId, _CitId, Convert.ToBoolean(_IsCharity), TSP.DataManager.TSProjectIngridientType.Observer, null, false, true);
            TransactionManager.EndSave();

            ShowMessage("ذخیره انجام شد.");
            GridViewObserverSelected.DataBind();

        }
        catch (Exception ex)
        {
            ShowMessage("خطا در ذخیره ایجاد شده است.");
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(ex);
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {

    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {

        int ProjectObserverSelectedId = -2;
        if (GridViewObserverSelected.FocusedRowIndex > -1)
        {
            System.Data.DataRow row = GridViewObserverSelected.GetDataRow(GridViewObserverSelected.FocusedRowIndex);
            ProjectObserverSelectedId = (int)row["ProjectObserverSelectedId"];
        }
        else
        {
            ShowMessage("لطفا یک ردیف انتخاب نمایید.");
        }
        TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager ProjectObserverSelectedManager = new TSP.DataManager.TechnicalServices.ProjectObserverSelectedManager();
        try
        {
            ProjectObserverSelectedManager.FindByCode(ProjectObserverSelectedId);
            if (ProjectObserverSelectedManager.Count == 0)
                return;


            string CrtEndDate = ProjectObserverSelectedManager[0]["CreateDate"].ToString();
            int ProjectRequestId = Convert.ToInt32(ProjectObserverSelectedManager[0]["ProjectRequestId"]);
            int ProjectId = Convert.ToInt32(ProjectObserverSelectedManager[0]["ProjectId"]);
            int _CitId = Convert.ToInt32(ProjectObserverSelectedManager[0]["CitId"]);
            int _IsCharity = Convert.ToInt32(ProjectObserverSelectedManager[0]["IsCharity"]);
            #region ناظر در صورتی که دو روز از ارجاع کار گذشته باشد به صورت اتوماتیک ناظر این پروژه می باشد.لذا نیازی به چک کردن این مورد وجود ندارد
            //int CapacityDecrement = Convert.ToInt32(ProjectObserverSelectedManager[0]["CapacityDecrement"]);
            //Utility.Date objDate = new Utility.Date(CrtEndDate);
            //string TheDayBefore = objDate.AddDays(-2);
            //string Today = Utility.GetDateOfToday();
            //int IsDocExp = string.Compare(Today, TheDayBefore);
            //if (IsDocExp >= 0)
            //{
            //    ShowMessage("امکان  قبول کار وجود ندارد.از زمان ارجاع کار نظارت به شما بیش از دو روز گذشته است و شما ناظر این پروژه می باشید.");
            //    //TransactionManager.CancelSave();
            //    return;
            //}
            #endregion
            if (Convert.ToInt32(ProjectObserverSelectedManager[0]["IsObserverConfirmed"]) == (int)TSP.DataManager.TSProjectObserverSelectedConfirmType.RejectWorkByNezam)
            {
                ShowMessage("امکان عدم قبول کار وجود ندارد.پیش از این رد این کار توسط کارشناسان سازمان ثبت شده است.");
                return;
            }

            if (Convert.ToInt32(ProjectObserverSelectedManager[0]["IsObserverConfirmed"]) == (int)TSP.DataManager.TSProjectObserverSelectedConfirmType.RejectWorkByObserve)
            {
                ShowMessage("امکان قبول کار وجود ندارد.پیش از این شما عدم قبول این کار را ثبت کرده اید.");
                return;
            }

            if (Convert.ToInt32(ProjectObserverSelectedManager[0]["IsObserverConfirmed"]) == (int)TSP.DataManager.TSProjectObserverSelectedConfirmType.AcceptWorkByObserver)
            {
                ShowMessage("پیش از این شما قبول این کار را ثبت کرده اید.");
                return;
            }
            ProjectObserverSelectedManager[0].BeginEdit();
            ProjectObserverSelectedManager[0]["IsObserverConfirmed"] = (int)TSP.DataManager.TSProjectObserverSelectedConfirmType.AcceptWorkByObserver;
            ProjectObserverSelectedManager[0].EndEdit();
            if (ProjectObserverSelectedManager.Save() <= 0)
            {
                ShowMessage("خطا در ذخیره ایجاد شده است.");
                return;
            }
            ShowMessage("قبول کار انجام شد.");
            GridViewObserverSelected.DataBind();
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);

        }

    }

    protected void InsertInActive(int TableId, int ReqId, int TableType, int ReTableType, TSP.DataManager.RequestInActivesManager Manager)
    {
        System.Data.DataRow dr = Manager.NewRow();
        dr["TableId"] = TableId;
        dr["TableType"] = TableType;
        dr["ReqId"] = ReqId;
        dr["ReqType"] = ReTableType;
        dr["InActive"] = 1;
        dr["CreateDate"] = Utility.GetDateOfToday();
        dr["UserId"] = Utility.GetCurrentUser_UserId();
        dr["ModifiedDate"] = DateTime.Now;
        Manager.AddRow(dr);
        if (Manager.Save() > 0)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "ذخیره انجام شد.";
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره اطلاعات رخ داده است.";
        }
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }
}