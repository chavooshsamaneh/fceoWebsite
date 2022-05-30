using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WorkRequestsrvEngineerToOthersTest;
public partial class Employee_TechnicalServices_Capacity_UpdateWorkRequestDocMeFile : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        //ObserverWorkRequestChangesManager.DoAutomaticObserverWorkRequestChange(MeId, "ثبت درخواست تغییرات آماده بکاری به دلیل تایید درخواست پروانه اشتغال بکار", "تایید اتوماتیک درخواست تغییرات آماده بکاری به دلیل تایید درخواست پروانه اشتغال بکار توسط سیستم"
        //              , TSObserverWorkRequestChangeType.MemberDocumentChange, CurrentUserId, MfId, ObsId, MappingId, DesId, UrbanismId, ObsDate, MappingDate, this[0]["ExpireDate"].ToString(), -2, TransManager);

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtMeId.Text))
        {
            ShowMessage("کد عضویت را وارد نمایید");
            return;
        }
        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TransactionManager.Add(ObserverWorkRequestChangesManager);
        TransactionManager.Add(ObserverWorkRequestManager);
        try
        {
            int MjParentId = -2;
            int MeId = Convert.ToInt32(txtMeId.Text);
            ObserverWorkRequestManager.FindByMeId(MeId);
            if (ObserverWorkRequestManager.Count == 0)
            {
                ShowMessage("برای این کد عضویت آماده بکاری وجود ندارد.");
                return;
            }

            int LastMfId = -1;
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
            if (dtMeFile.Rows.Count <= 0)
            {
                ShowMessage("برای این کد عضویت پروانه اشتغال به کار تایید شده وجود ندارد.");
                return;
            }
            LastMfId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
            TransactionManager.BeginSave();
            int per = ObserverWorkRequestChangesManager.DoAutomaticObserverWorkRequestChange(MeId, "ثبت درخواست تغییرات آماده بکاری به دلیل تایید درخواست پروانه اشتغال بکار", "تایید اتوماتیک درخواست تغییرات آماده بکاری به دلیل تایید درخواست پروانه اشتغال بکار توسط سیستم"
               , TSP.DataManager.TSObserverWorkRequestChangeType.MemberDocumentChange, Utility.GetCurrentUser_UserId(), LastMfId, dtMeFile.Rows[0]["ExpireDate"].ToString(), -2, TransactionManager);
            if (per != 0)
            {
                TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
                string ErrorMsg = WorkFlowPermission.FindRequestErrorMsg(per);
                ShowMessage(ErrorMsg);
                return;
            }
            TransactionManager.EndSave();
            ShowMessage("بروزرسانی انجام شد");
        }
        catch (Exception ex)
        {
            ShowMessage("خطا"+ex.Message);
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(ex);
        }
    }

    protected void btnSaveAgent_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtMeId.Text))
        {
            ShowMessage("کد عضویت را وارد نمایید");
            return;
        }
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TransactionManager.Add(ObserverWorkRequestChangesManager);
        TransactionManager.Add(ObserverWorkRequestManager);
        try
        {
            int MjParentId = -2;
            int MeId = Convert.ToInt32(txtMeId.Text);
            MemberManager.FindByCode(MeId);
            if (MemberManager.Count == 0)
            {
                ShowMessage("اطلاعات یافت نشد.");
                return;
            }
            if (Utility.IsDBNullOrNullValue(MemberManager[0]["AgentId"]))
            {
                ShowMessage("نمایندگی شخص نامشص است.");
                return;
            }
            int AgentId = Convert.ToInt32(MemberManager[0]["AgentId"]);
            ObserverWorkRequestManager.FindByMeId(MeId);
            if (ObserverWorkRequestManager.Count == 0)
            {
                ShowMessage("برای این کد عضویت آماده بکاری وجود ندارد.");
                return;
            }
            int LastMfId = -1;
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
            if (dtMeFile.Rows.Count <= 0)
            {
                ShowMessage("برای این کد عضویت پروانه اشتغال به کار تایید شده وجود ندارد.");
                return;
            }
            LastMfId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
            TransactionManager.BeginSave();
            int per = ObserverWorkRequestChangesManager.DoAutomaticObserverWorkRequestChange(MeId, "ثبت درخواست تغییرات آماده بکاری به دلیل تغییر نمایندگی", "تایید اتوماتیک درخواست تغییرات آماده بکاری به دلیل  تغییر نمایندگی توسط سیستم"
               , TSP.DataManager.TSObserverWorkRequestChangeType.AgentChange, Utility.GetCurrentUser_UserId(), LastMfId, dtMeFile.Rows[0]["ExpireDate"].ToString(), AgentId, TransactionManager);
            if (per != 0)
            {
                TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
                string ErrorMsg = WorkFlowPermission.FindRequestErrorMsg(per);
                ShowMessage(ErrorMsg);
                return;
            }
            TransactionManager.EndSave();
            ShowMessage("بروزرسانی انجام شد");
        }
        catch (Exception ex)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(ex);
        }

    }

    void ShowMessage(string str)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = str;
    }



    protected void btnInActice_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtMeId.Text))
        {
            ShowMessage("کد عضویت را وارد نمایید");
            return;
        }
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager();
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TransactionManager.Add(ObserverWorkRequestChangesManager);
        TransactionManager.Add(ObserverWorkRequestManager);
        try
        {
            int MjParentId = -2;
            int MeId = Convert.ToInt32(txtMeId.Text);
            MemberManager.FindByCode(MeId);
            if (MemberManager.Count == 0)
            {
                ShowMessage("اطلاعات یافت نشد.");
                return;
            }
            if (Utility.IsDBNullOrNullValue(MemberManager[0]["AgentId"]))
            {
                ShowMessage("نمایندگی شخص نامشص است.");
                return;
            }
            int AgentId = Convert.ToInt32(MemberManager[0]["AgentId"]);
            ObserverWorkRequestManager.FindByMeId(MeId);
            if (ObserverWorkRequestManager.Count == 0)
            {
                ShowMessage("برای این کد عضویت آماده بکاری وجود ندارد.");
                return;
            }
            int LastMfId = -1;
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DataTable dtMeFile = DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
            if (dtMeFile.Rows.Count <= 0)
            {
                ShowMessage("برای این کد عضویت پروانه اشتغال به کار تایید شده وجود ندارد.");
                return;
            }
            LastMfId = int.Parse(dtMeFile.Rows[0]["MfId"].ToString());
            TransactionManager.BeginSave();
            int per = ObserverWorkRequestChangesManager.DoAutomaticObserverWorkRequestChange(MeId, "ثبت درخواست غیرفعال آماده بکاری", "تایید اتوماتیک درخواست غیرفعال آماده بکاری توسط سیستم"
               , TSP.DataManager.TSObserverWorkRequestChangeType.InActive, Utility.GetCurrentUser_UserId(), LastMfId, dtMeFile.Rows[0]["ExpireDate"].ToString(), AgentId, TransactionManager);
            if (per != 0)
            {
                TSP.DataManager.WorkFlowPermission WorkFlowPermission = new TSP.DataManager.WorkFlowPermission();
                string ErrorMsg = WorkFlowPermission.FindRequestErrorMsg(per);
                ShowMessage(ErrorMsg);
                return;
            }
            TransactionManager.EndSave();
            ShowMessage("بروزرسانی انجام شد");
        }
        catch (Exception ex)
        {
            TransactionManager.CancelSave();
            Utility.SaveWebsiteError(ex);
        }


    }
    /// <summary>
    /// 
    /// //1-     "RemainCapacityObsReal desc"
    /// //2-    string FilterFoundation =     "RemainCapacityObsReal>="           + FoundationCalculation.ToString();  //***فیلتر بر اساس ظرفیت باقی مانده نظازت واقعی با توجه به متراژ پروژه                    
    /// //      string FilterCountWork = ""; string FilterCountWorkUnder400 =           " OR (CountUnder400MeterWork+CountUnder400MeterWorkDesign) < "       + ((int)CapacityCalculations._CountWorkUnder400).ToString() + ")";
    /// //3-    FilterCountWork =     "CountRemainWorkCount >0";   //CountWorks:8
    /// //4-    string FilterWorkCount =     " AND CountInproccesWorksObs=0";    //***  تعداد کار باقیمانده برابر تعداد کل کارهایی باشد که می تواندبگیر.یعنی تا الان هیج کاری نگرفته است
    /// //5-    Sort =         "PercentOfCapacityUsage asc";
    ///     //6-    Sort = "RemainCapacityObsFuncA desc";
    /// //7-    Sort += ",CountInproccesWorksObs asc";
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdatePercentUsage_Click(object sender, EventArgs e)
    {
        string ListConflictUsedCapacityMeId = "";
        string ListConflictRemainCapacityMeId = "";
        string ListUpdatedMeId = "";
        string ListtxtListConflictZeroMeId = "";
        string ListConflictPercentOfCapacityMeId = "";
        try
        {
            CapacityCalculations CapacityCalculations = new CapacityCalculations();
            TSP.DataManager.TechnicalServices.ObserverWorkRequestManager TSObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
            TSP.DataManager.TechnicalServices.ObserverWorkRequestManager TSObserverWorkRequestManager2 = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
            TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();
            ////
            DataTable dt = TSObserverWorkRequestManager2.SelectForUpdateWorkRequest();

            int _MeId;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _MeId = Convert.ToInt32(dt.Rows[i]["MeId"]);
                if (CapacityCalculations.UpdateWorkRequestCapacityData(TSObserverWorkRequestManager, ProjectCapacityDecrementManager, _MeId, Utility.GetCurrentUser_MeId(), -2, -2, false, TSP.DataManager.TSProjectIngridientType.Nothing, null, false, false) != 0)
                    ListtxtListConflictZeroMeId += _MeId.ToString() + ",";
                else
                    ListUpdatedMeId += _MeId.ToString() + ",";
                #region Comment
                //     int _CountWorkUnder400 = 10;//***********************مهم*******************************
                //     UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementobs = CapacityCalculations.UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer, _MeId, TSP.DataManager.TSMemberType.Member, false, -1, ProjectCapacityDecrementManager, -1, (int)TSP.DataManager.TSDiscountPercent.BonyadMaskan);
                //     UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementdes = CapacityCalculations.UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, (int)TSP.DataManager.TSProjectIngridientType.Designer, _MeId, TSP.DataManager.TSMemberType.Member, false, -1, ProjectCapacityDecrementManager, -1, (int)TSP.DataManager.TSDiscountPercent.BonyadMaskan);
                //     UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementAll = CapacityCalculations.UsedCapacity(-1, -1, _MeId, TSP.DataManager.TSMemberType.Member, false, -1, ProjectCapacityDecrementManager, -1, (int)TSP.DataManager.TSDiscountPercent.BonyadMaskan);
                //     ////////////////
                //     UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementObsunder400 = CapacityCalculations.UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Observer, (int)TSP.DataManager.TSProjectIngridientType.Observer, _MeId, TSP.DataManager.TSMemberType.Member, true, -1, ProjectCapacityDecrementManager, -1, (int)TSP.DataManager.TSDiscountPercent.BonyadMaskan);
                //     UsedCapacityProjectCapacityDecrement UsedCapacityProjectCapacityDecrementDesunder400 = CapacityCalculations.UsedCapacity((int)TSP.DataManager.TSProjectIngridientType.Designer, (int)TSP.DataManager.TSProjectIngridientType.Designer, _MeId, TSP.DataManager.TSMemberType.Member, true, -1, ProjectCapacityDecrementManager, -1, (int)TSP.DataManager.TSDiscountPercent.BonyadMaskan);
                //     double UsedCapacityCountProjectAll = UsedCapacityProjectCapacityDecrementobs.UsedCapacityCountProject + UsedCapacityProjectCapacityDecrementdes.UsedCapacityCountProject;

                //     ///////////////
                //     int TotalUsedCapacityObs = Convert.ToInt32(TSObserverWorkRequestManager[i]["BonyadMaskan"]) + Convert.ToInt32(UsedCapacityProjectCapacityDecrementobs.UsedCapacitySumCapacityDecrement);

                //     //**************

                //     int SumBonyad = Convert.ToInt32(TSObserverWorkRequestManager[i]["BonyadMaskan"]) + Convert.ToInt32(TSObserverWorkRequestManager[i]["BonyadMaskanDesignMeter"]);
                //     int Sumused = SumBonyad + Convert.ToInt32(UsedCapacityProjectCapacityDecrementAll.UsedCapacitySumCapacityDecrement);
                //     TSObserverWorkRequestManager[i].BeginEdit();
                //     TSObserverWorkRequestManager[i]["UsedCapacity"] = UsedCapacityProjectCapacityDecrementAll.UsedCapacitySumCapacityDecrement + SumBonyad;

                //     if (Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacity"]) != Sumused)
                //     {
                //         ListConflictUsedCapacityMeId += _MeId.ToString() + ",";
                //     }

                //     int MaxTotalCapacity = Convert.ToInt32(TSObserverWorkRequestManager[i]["TotalCapacity"]);
                //     int MaxObs = Convert.ToInt32(TSObserverWorkRequestManager[i]["CapacityObs"]);
                //     int MaxDes = Convert.ToInt32(TSObserverWorkRequestManager[i]["CapacityDesign"]);
                //     int TotalUsedCapacityDes = Convert.ToInt32(TSObserverWorkRequestManager[i]["BonyadMaskanDesignMeter"]) + Convert.ToInt32(UsedCapacityProjectCapacityDecrementdes.UsedCapacitySumCapacityDecrement);

                //     //************RemainCapacity
                //     TSObserverWorkRequestManager[i]["RemainCapacity"] = MaxTotalCapacity - (TotalUsedCapacityDes + TotalUsedCapacityObs);
                //     if (Convert.ToInt32(TSObserverWorkRequestManager[i]["RemainCapacity"]) + Sumused != MaxTotalCapacity)
                //     {
                //         ListConflictRemainCapacityMeId += _MeId.ToString() + ",";
                //     }
                //     //**********RemainCapacityObs
                //     TSObserverWorkRequestManager[i]["RemainCapacityObs"] = MaxObs - TotalUsedCapacityObs;
                //     //************RemainCapacityObsReal
                //     TSObserverWorkRequestManager[i]["RemainCapacityObsReal"] =
                //     Math.Min(
                //       (MaxObs - (TotalUsedCapacityObs + (Convert.ToInt32(TSObserverWorkRequestManager[i]["ShirazMunicipalityMeter"]) - Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacityObsShiraz"]))))
                //       , (MaxTotalCapacity - (TotalUsedCapacityObs + (Convert.ToInt32(TSObserverWorkRequestManager[i]["ShirazMunicipalityMeter"]) - Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacityObsShiraz"])))
                //       - (TotalUsedCapacityDes + (Convert.ToInt32(TSObserverWorkRequestManager[i]["ShirazMunicipalityDesignMeter"]) - Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacityDesShirazMun"])))));
                //     //*********PercentOfCapacityUsage
                //     if (Convert.ToDouble(TSObserverWorkRequestManager[i]["CapacityObs"]) == 0)
                //         TSObserverWorkRequestManager[i]["PercentOfCapacityUsage"] = 0;
                //     else
                //     {
                //         if (Math.Round(Convert.ToDouble(TSObserverWorkRequestManager[i]["PercentOfCapacityUsage"]), 2) != Math.Round((Convert.ToDouble(TotalUsedCapacityObs) + (Convert.ToInt32(TSObserverWorkRequestManager[i]["ShirazMunicipalityMeter"]) - Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacityObsShiraz"]))) / Convert.ToDouble(TSObserverWorkRequestManager[i]["CapacityObs"]), 2))
                //         {
                //             ListConflictPercentOfCapacityMeId += _MeId.ToString() + ",";
                //         }
                //         TSObserverWorkRequestManager[i]["PercentOfCapacityUsage"] = (Convert.ToDouble(TotalUsedCapacityObs) + (Convert.ToInt32(TSObserverWorkRequestManager[i]["ShirazMunicipalityMeter"]) - Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacityObsShiraz"]))) / Convert.ToDouble(TSObserverWorkRequestManager[i]["CapacityObs"]);
                //     }

                //     TSObserverWorkRequestManager[i]["CountInproccesWorks"] = UsedCapacityProjectCapacityDecrementobs.UsedCapacityCountProject;
                //     TSObserverWorkRequestManager[i]["CountInproccesWorksObs"] = UsedCapacityProjectCapacityDecrementobs.UsedCapacityCountProject;
                //     TSObserverWorkRequestManager[i]["CountInproccesWorksDesign"] = UsedCapacityProjectCapacityDecrementdes.UsedCapacityCountProject;
                //     //*******************RemainCapacityDesign
                //     TSObserverWorkRequestManager[i]["RemainCapacityDesign"] = MaxDes - TotalUsedCapacityDes;
                //     //****************Under400
                //     TSObserverWorkRequestManager[i]["CountUnder400MeterWork"] = UsedCapacityProjectCapacityDecrementObsunder400.UsedCapacityCountProject;
                //     TSObserverWorkRequestManager[i]["CountUnder400MeterWorkDesign"] = UsedCapacityProjectCapacityDecrementDesunder400.UsedCapacityCountProject;
                //     double CountUnder400 = UsedCapacityProjectCapacityDecrementObsunder400.UsedCapacityCountProject + UsedCapacityProjectCapacityDecrementDesunder400.UsedCapacityCountProject;
                //     int CountWorks = Convert.ToInt32(TSObserverWorkRequestManager[i]["CountWorks"]);
                //     if (CountUnder400 == 0 || CountUnder400 == 1)
                //     {
                //         TSObserverWorkRequestManager[i]["CountRemainWorkCount"] = CountWorks - UsedCapacityCountProjectAll;
                //     }
                //     else if (CountUnder400 <= _CountWorkUnder400)
                //     {

                //         TSObserverWorkRequestManager[i]["CountRemainWorkCount"] = CountWorks - (UsedCapacityCountProjectAll - (CountUnder400 - 1));
                //     }
                //     else
                //     {
                //         TSObserverWorkRequestManager[i]["CountRemainWorkCount"] = CountWorks - (UsedCapacityCountProjectAll - _CountWorkUnder400);

                //     }
                //     //****************
                //     TSObserverWorkRequestManager[i].EndEdit();
                //     TSObserverWorkRequestManager.Save();
                //     TSObserverWorkRequestManager.DataTable.AcceptChanges();
                //     if (Convert.ToInt32(TSObserverWorkRequestManager[i]["RemainCapacityObs"]) < 0
                //|| Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacityObsShiraz"]) < 0
                //|| Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacityObsSadra"]) < 0
                //|| Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacityObsLapooy"]) < 0
                //|| Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacityObsKhanZenyan"]) < 0
                //|| Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacityObsDareyon"]) < 0
                //|| Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacityObsZarghan"]) < 0
                //|| Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacityObsOtherCities"]) < 0
                //|| Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacityDesShirazMun"]) < 0
                //|| Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacityDesOtherCities"]) < 0
                // || Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacityUrbenismTarhShirazMun"]) < 0
                // || Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacityEntebaghShahriShirazMun"]) < 0
                // || Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacityUrbenismTarhOtherCities"]) < 0
                // || Convert.ToInt32(TSObserverWorkRequestManager[i]["UsedCapacityEntebaghShahriOtherCities"]) < 0)
                //     {
                //         ListtxtListConflictZeroMeId += _MeId.ToString() + ",";

                //     }
                //     ListUpdatedMeId += _MeId.ToString() + ",";
                // }
                #endregion
            }
            ShowMessage("آفرین، صد آفرین دختر خوب و نازنین");
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            ShowMessage(ex.Message);
        }
        txtListConflictUsedCapacityMeId.Text = ListConflictUsedCapacityMeId;
        txtListConflictRemainCapacityMeId.Text = ListConflictRemainCapacityMeId;
        txtListMeIdOk.Text = ListUpdatedMeId;
        txtListConflictZeroMeId.Text = ListtxtListConflictZeroMeId;
        txtListConflictPercentOfCapacityMeId.Text = ListConflictPercentOfCapacityMeId;
    }

    protected void btnTestEsup_Click(object sender, EventArgs e)
    {
        TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager TSEsupWebserviceCallingLogManager = new TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager();
        DataTable dtMeList = TSEsupWebserviceCallingLogManager.SelectEsupWebserviceCallingLogForTest();
        for (int i = 0; i < dtMeList.Rows.Count; i++)
        {
            int MeId = Convert.ToInt32(dtMeList.Rows[i]["MeId"]);
            txtMeId.Text = MeId.ToString();
            ShahrdariWebservice("", "", "", 100, 0, 0, 1399, true, MeId);
        }
    }
    protected void NotSentToShahrdari_Click(object sender, EventArgs e)
    {
        TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager TSEsupWebserviceCallingLogManager = new TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager();
        DataTable dtMeList = TSEsupWebserviceCallingLogManager.SelectNotSentToShahrdari();
        for (int i = 0; i < dtMeList.Rows.Count; i++)
        {
            int MeId = Convert.ToInt32(dtMeList.Rows[i]["MeId"]);
            ShahrdariWebservice(1399, MeId);
        }
        ShowMessage("ذخیره شد");
    }

    private void ShahrdariWebservice(Int16 Year, int _MeId)
    {
        //        لیست ClsQtaInputs دارای اطلاعات زیر می باشد
        //CI_Ability : در صورتی که صلاحیت طراحی باشد عدد 2 و صلاحیت نظارت عدد 1 ، صلاحیت مجری عدد 3 ، صلاحیت محاسبه عدد 4
        //Meter: متراژ ظرفیت شیراز
        //date : تاریخ بازگشایی ظرفیت
        //Time  :  ساعت بازگشلیی ظرفیت
        //ChangeBaseDate  : تاریخ تغییر پایه
        try
        {
            decimal MunObsCapacity = 0; decimal MunDesignCapacity = 0; decimal MunUrbenismCapacity = 0;
            int LastMfId = -2; int MjParentId = -2; string DesignDate = ""; string ObsDate = "";
            string ObsQueueDate; string ObsQueueTime;
            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
            TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();


            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            DataTable dtMember = MemberManager.SelectMemberInfoForWorkRequest(_MeId);
            if (dtMember.Rows.Count != 1)
                return;
            MjParentId = Convert.ToInt32(dtMember.Rows[0]["MasterMfMjParentId"]);
            ObsQueueDate = dtMember.Rows[0]["ObsQueueDate"].ToString();
            ObsQueueTime = dtMember.Rows[0]["ObsQueueTime"].ToString().Substring(0, 8);

            System.Data.DataTable dtMeDetailDesign = DocMemberFileDetailManager.FindByResponsibility(LastMfId, _MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design, -1, MjParentId);
            System.Data.DataTable dtMeDetailUrbanism = DocMemberFileDetailManager.FindByResponsibility(LastMfId, _MeId, (int)TSP.DataManager.DocumentResponsibilityType.Urbanism, -1, MjParentId);
            if (dtMeDetailDesign.Rows.Count > 0 && !Utility.IsDBNullOrNullValue(dtMeDetailDesign.Rows[0]["Date"]))
                DesignDate = dtMeDetailDesign.Rows[0]["Date"].ToString();
            else if (dtMeDetailUrbanism.Rows.Count > 0 && !Utility.IsDBNullOrNullValue(dtMeDetailUrbanism.Rows[0]["Date"]))
                DesignDate = dtMeDetailUrbanism.Rows[0]["Date"].ToString();
            else
                DesignDate = "";


            ObserverWorkRequestManager.FindByMeId(_MeId);
            if (ObserverWorkRequestManager.Count != 1)
                return;
            ObsDate = !Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["ObsDate"]) ? ObserverWorkRequestManager[0]["ObsDate"].ToString() : "";
            MunDesignCapacity = !Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["ShirazMunicipalityDesignMeter"]) ? Convert.ToDecimal(ObserverWorkRequestManager[0]["ShirazMunicipalityDesignMeter"]) : 0;
            MunObsCapacity = !Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["ShirazMunicipalityMeter"]) ? Convert.ToDecimal(ObserverWorkRequestManager[0]["ShirazMunicipalityMeter"]) : 0;
            MunUrbenismCapacity = !Utility.IsDBNullOrNullValue(ObserverWorkRequestManager[0]["ShirazMunicipulityUrbenismTarh"]) ? Convert.ToDecimal(ObserverWorkRequestManager[0]["ShirazMunicipulityUrbenismTarh"]) + Convert.ToDecimal(ObserverWorkRequestManager[0]["ShirazMunicipulityUrbenismEntebaghShahri"]) : 0;
            #region Eng_Info
            WorkRequestsrvEngineerToOthers.ClsEngineer1 ClsEngineer1Info = new WorkRequestsrvEngineerToOthers.ClsEngineer1();
            ClsEngineer1Info.Eng_Info = new WorkRequestsrvEngineerToOthers.Eng_Info();
            ClsEngineer1Info.Eng_Info.EngName = dtMember.Rows[0]["FirstName"].ToString();
            ClsEngineer1Info.Eng_Info.EngFamily = dtMember.Rows[0]["LastName"].ToString();
            ClsEngineer1Info.Eng_Info.MunicipalityCode = dtMember.Rows[0]["MeNo"].ToString();// کد عضویت نظام مهندسی
            ClsEngineer1Info.Eng_Info.FatherName = dtMember.Rows[0]["FatherName"].ToString();// نام پدر
            ClsEngineer1Info.Eng_Info.BirthDate = dtMember.Rows[0]["BirhtDate"].ToString(); //تاریخ تولد
            ClsEngineer1Info.Eng_Info.IdNo = dtMember.Rows[0]["IdNo"].ToString();//شماره شناسنامه
            ClsEngineer1Info.Eng_Info.Tel = dtMember.Rows[0]["HomeTel"].ToString();// تلفن
            ClsEngineer1Info.Eng_Info.Address = dtMember.Rows[0]["HomeAdr"].ToString();// آدرس
            ClsEngineer1Info.Eng_Info.NationalCode = dtMember.Rows[0]["SSN"].ToString();// کد ملی
            ClsEngineer1Info.Eng_Info.IdentityCode = _MeId.ToString();//کد عضویت
            ClsEngineer1Info.Eng_Info.AddressWork = ""; //آدرس محل کار
            ClsEngineer1Info.Eng_Info.Email = dtMember.Rows[0]["Email"].ToString();// آدرس ایمیل
            ClsEngineer1Info.Eng_Info.BirthPlace = dtMember.Rows[0]["BirthPlace"].ToString();// محل تولد
            ClsEngineer1Info.Eng_Info.MobileNo = dtMember.Rows[0]["MobileNo"].ToString(); //شماره همراه
            ClsEngineer1Info.Eng_Info.PostalCode = "";//کد پستی
            ClsEngineer1Info.Eng_Info.ArchitectureCode = "";// کد شهرداری
            ClsEngineer1Info.Eng_Info.PostalCodeWork = ""; //کد پستی محل کار
            switch (MjParentId)
            {
                case 1:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 100;
                    break;
                case 2:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 210;
                    break;
                case 3:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 300;
                    break;
                case 4:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 400;
                    break;
                case 5:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 500;
                    break;
                case 6:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 600;
                    break;
                case 7:
                    ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 700;
                    break;
            }
            WorkRequestsrvEngineerToOthers.IsrvEngineerToOthersClient IsrvEngineerToOthersClient = new WorkRequestsrvEngineerToOthers.IsrvEngineerToOthersClient();
            IsrvEngineerToOthersClient.SaveEng_InfoCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveEng_InfoCompletedEventArgs>(GetResultEngineerToOthers);
            IsrvEngineerToOthersClient.SaveEng_InfoAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), ClsEngineer1Info);
            #endregion


            #region MainServer
            int Count = 1;
            if (!string.IsNullOrEmpty(ObsDate) && MunDesignCapacity != 0)
                Count = 2;
            WorkRequestsrvEngineerToOthers.ClsQtaInputs[] ListclsQtaInputs = new WorkRequestsrvEngineerToOthers.ClsQtaInputs[Count];
            //CI_Ability: در صورتی که صلاحیت طراحی باشد عدد 2 و صلاحیت نظارت عدد 1 ، صلاحیت مجری عدد 3 ، صلاحیت محاسبه عدد 4 
            if (!string.IsNullOrEmpty(ObsDate) && MunDesignCapacity != 0)
            {
                #region نظارت و طراحی
                WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsde = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                if (MjParentId == (int)TSP.DataManager.MainMajors.Civil)
                    ClsQtaInputsde.CI_Ability = 4;
                else
                    ClsQtaInputsde.CI_Ability = 2;
                ClsQtaInputsde.Date = ObsQueueDate;
                if (MjParentId == (int)TSP.DataManager.MainMajors.Urbanism)
                    ClsQtaInputsde.Meter = MunUrbenismCapacity;
                else
                    ClsQtaInputsde.Meter = MunDesignCapacity;
                ClsQtaInputsde.Time = ObsQueueTime;
                ClsQtaInputsde.ChangeBaseDate = DesignDate;
                ListclsQtaInputs[0] = ClsQtaInputsde;
                //**نظارت
                WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsOb = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                ClsQtaInputsOb.CI_Ability = 1;
                ClsQtaInputsOb.Date = ObsQueueDate;
                ClsQtaInputsOb.Meter = MunObsCapacity;
                ClsQtaInputsOb.Time = ObsQueueTime;
                ClsQtaInputsOb.ChangeBaseDate = ObsDate;
                ListclsQtaInputs[1] = ClsQtaInputsOb;
                #endregion
            }
            else if ((MunDesignCapacity != 0 || MunUrbenismCapacity != 0) && string.IsNullOrEmpty(ObsDate))
            {
                #region فقظ طراحی یا شهرسازی
                WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsDes = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                if (MjParentId == (int)TSP.DataManager.MainMajors.Civil)
                    ClsQtaInputsDes.CI_Ability = 4;
                else
                    ClsQtaInputsDes.CI_Ability = 2;
                ClsQtaInputsDes.Date = ObsQueueDate;
                if (MjParentId == (int)TSP.DataManager.MainMajors.Urbanism)
                    ClsQtaInputsDes.Meter = MunUrbenismCapacity;
                else
                    ClsQtaInputsDes.Meter = MunDesignCapacity;
                ClsQtaInputsDes.Time = ObsQueueTime;
                ClsQtaInputsDes.ChangeBaseDate = DesignDate;
                ListclsQtaInputs[0] = ClsQtaInputsDes;
                #endregion
            }
            else if (!string.IsNullOrEmpty(ObsDate))
            {
                #region فقظ نظارت
                //**نظارت
                WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsObs = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                ClsQtaInputsObs.CI_Ability = 1;
                ClsQtaInputsObs.Date = ObsQueueDate;
                ClsQtaInputsObs.Meter = MunObsCapacity;
                ClsQtaInputsObs.Time = ObsQueueTime;
                ClsQtaInputsObs.ChangeBaseDate = ObsDate;
                ListclsQtaInputs[0] = ClsQtaInputsObs;
                #endregion
            }

            IsrvEngineerToOthersClient.SaveQtaInfoCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveQtaInfoCompletedEventArgs>(GetGetResultEngineerToOthersClient);
            IsrvEngineerToOthersClient.SaveQtaInfoAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), ListclsQtaInputs, 1, Year, (int)TSP.DataManager.TSSafarayanehWebServiceCallingRefrenceType.QueueListForm);

            txtListMeIdOk.Text += "," + _MeId.ToString();

            #endregion
        }
        catch (Exception ex)
        {
            txtListConflictUsedCapacityMeId.Text += "," + _MeId.ToString();
            Utility.SaveWebsiteError(ex);
            ShowMessage(ex.ToString());
        }
    }
    /// <summary>
    /// پروانه و صلاحیت و اطلاعات پایه 
    /// </summary>
    /// <param name="ObsDate"></param>
    /// <param name="DesignDate"></param>
    /// <param name="UrbenismDate"></param>
    /// <param name="MunObsCapacity"></param>
    /// <param name="MunDesignCapacity"></param>
    /// <param name="MunUrbenismCapacity"></param>
    /// <param name="Year"></param>
    /// <param name="SendMeInfo"></param>
    /// <param name="_MeId"></param>
    private void ShahrdariWebservice(string ObsDate, string DesignDate, string UrbenismDate, decimal MunObsCapacity, decimal MunDesignCapacity, decimal MunUrbenismCapacity, Int16 Year, Boolean SendMeInfo, int _MeId)
    {

        //        لیست ClsQtaInputs دارای اطلاعات زیر می باشد
        //CI_Ability : در صورتی که صلاحیت طراحی باشد عدد 2 و صلاحیت نظارت عدد 1 ، صلاحیت مجری عدد 3 ، صلاحیت محاسبه عدد 4
        //Meter: متراژ ظرفیت شیراز
        //date : تاریخ بازگشایی ظرفیت
        //Time  :  ساعت بازگشلیی ظرفیت
        //ChangeBaseDate  : تاریخ تغییر پایه
        try
        {
            if (Utility.IsesupTestServerUse())
            {
                #region TestServer
                //WorkRequestsrvEngineerToOthersTest.IsrvEngineerToOthersClient IsrvEngineerToOthersClient = new WorkRequestsrvEngineerToOthersTest.IsrvEngineerToOthersClient();
                //if (SendMeInfo)
                //{
                //    TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                //    DataTable dtMember = MemberManager.SelectMemberInfoForWorkRequest(_MeId);
                //    if (dtMember.Rows.Count != 1)
                //        return;
                //    WorkRequestsrvEngineerToOthersTest.ClsEngineer1 ClsEngineer1Info = new WorkRequestsrvEngineerToOthersTest.ClsEngineer1();
                //    ClsEngineer1Info.Eng_Info = new WorkRequestsrvEngineerToOthersTest.Eng_Info();
                //    ClsEngineer1Info.Eng_Info.EngName = dtMember.Rows[0]["FirstName"].ToString();
                //    ClsEngineer1Info.Eng_Info.EngFamily = dtMember.Rows[0]["LastName"].ToString();
                //    ClsEngineer1Info.Eng_Info.MunicipalityCode = dtMember.Rows[0]["MeNo"].ToString();// کد عضویت نظام مهندسی
                //    ClsEngineer1Info.Eng_Info.FatherName = dtMember.Rows[0]["FatherName"].ToString();// نام پدر
                //    ClsEngineer1Info.Eng_Info.BirthDate = dtMember.Rows[0]["BirhtDate"].ToString(); //تاریخ تولد
                //    ClsEngineer1Info.Eng_Info.IdNo = dtMember.Rows[0]["IdNo"].ToString();//شماره شناسنامه
                //    ClsEngineer1Info.Eng_Info.Tel = dtMember.Rows[0]["HomeTel"].ToString();// تلفن
                //    ClsEngineer1Info.Eng_Info.Address = dtMember.Rows[0]["HomeAdr"].ToString();// آدرس
                //    ClsEngineer1Info.Eng_Info.NationalCode = dtMember.Rows[0]["SSN"].ToString();// کد ملی
                //    ClsEngineer1Info.Eng_Info.IdentityCode = _MeId.ToString();//کد عضویت
                //    ClsEngineer1Info.Eng_Info.AddressWork = ""; //آدرس محل کار
                //    ClsEngineer1Info.Eng_Info.Email = dtMember.Rows[0]["Email"].ToString();// آدرس ایمیل
                //    ClsEngineer1Info.Eng_Info.BirthPlace = dtMember.Rows[0]["BirthPlace"].ToString();// محل تولد
                //    ClsEngineer1Info.Eng_Info.MobileNo = dtMember.Rows[0]["MobileNo"].ToString(); //شماره همراه
                //    ClsEngineer1Info.Eng_Info.PostalCode = "";//کد پستی
                //    ClsEngineer1Info.Eng_Info.ArchitectureCode = "";// کد شهرداری
                //    ClsEngineer1Info.Eng_Info.PostalCodeWork = ""; //کد پستی محل کار

                //    IsrvEngineerToOthersClient.SaveEng_InfoCompleted += new EventHandler<SaveEng_InfoCompletedEventArgs>(GetResultEngineerToOthersTest);
                //    IsrvEngineerToOthersClient.SaveEng_InfoAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), ClsEngineer1Info);

                //}
                //int Count = 1;
                //if (!string.IsNullOrEmpty(ObsDate) && MunDesignCapacity != 0)
                //    Count = 2;
                //string time = DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
                //WorkRequestsrvEngineerToOthersTest.ClsQtaInputs[] ListclsQtaInputs = new WorkRequestsrvEngineerToOthersTest.ClsQtaInputs[Count];
                ////CI_Ability: در صورتی که صلاحیت طراحی باشد عدد 2 و صلاحیت نظارت عدد 1 ، صلاحیت مجری عدد 3 ، صلاحیت محاسبه عدد 4 

                //if (!string.IsNullOrEmpty(ObsDate) && MunDesignCapacity != 0)
                //{
                //    #region نظارت و طراحی
                //    //**طراحی
                //    WorkRequestsrvEngineerToOthersTest.ClsQtaInputs ClsQtaInputsde = new WorkRequestsrvEngineerToOthersTest.ClsQtaInputs();
                //    if (MjParentId == (int)TSP.DataManager.MainMajors.Civil)
                //        ClsQtaInputsde.CI_Ability = 4;
                //    else
                //        ClsQtaInputsde.CI_Ability = 2;
                //    ClsQtaInputsde.Date = Utility.GetDateOfToday();
                //    if (MjParentId == (int)TSP.DataManager.MainMajors.Urbanism)
                //        ClsQtaInputsde.Meter = MunUrbenismCapacity;
                //    else
                //        ClsQtaInputsde.Meter = MunDesignCapacity;
                //    ClsQtaInputsde.Time = time;
                //    ClsQtaInputsde.ChangeBaseDate = DesignDate;
                //    ListclsQtaInputs[0] = ClsQtaInputsde;
                //    //**نظارت
                //    WorkRequestsrvEngineerToOthersTest.ClsQtaInputs ClsQtaInputsOb = new WorkRequestsrvEngineerToOthersTest.ClsQtaInputs();
                //    ClsQtaInputsOb.CI_Ability = 1;
                //    //ClsQtaInputsOb.Date = Utility.GetDateOfToday();
                //    ClsQtaInputsOb.Meter = MunObsCapacity;
                //    //ClsQtaInputsOb.Time = time;
                //    ClsQtaInputsOb.ChangeBaseDate = ObsDate;
                //    ListclsQtaInputs[1] = ClsQtaInputsOb;
                //    #endregion
                //}
                //else if ((MunDesignCapacity != 0 || MunUrbenismCapacity != 0) && string.IsNullOrEmpty(ObsDate))
                //{
                //    #region فقظ طراحی یا شهرسازی
                //    //**طراحی
                //    WorkRequestsrvEngineerToOthersTest.ClsQtaInputs ClsQtaInputsDes = new WorkRequestsrvEngineerToOthersTest.ClsQtaInputs();
                //    if (MjParentId == (int)TSP.DataManager.MainMajors.Civil)
                //        ClsQtaInputsDes.CI_Ability = 4;
                //    else
                //        ClsQtaInputsDes.CI_Ability = 2;
                //    ClsQtaInputsDes.Date = Utility.GetDateOfToday();
                //    if (MjParentId == (int)TSP.DataManager.MainMajors.Urbanism)
                //        ClsQtaInputsDes.Meter = MunUrbenismCapacity;
                //    else
                //        ClsQtaInputsDes.Meter = MunDesignCapacity;
                //    ClsQtaInputsDes.Time = time;
                //    ClsQtaInputsDes.ChangeBaseDate = DesignDate;
                //    ListclsQtaInputs[0] = ClsQtaInputsDes;
                //    #endregion
                //}
                //else if (!string.IsNullOrEmpty(ObsDate))
                //{
                //    #region فقظ نظارت
                //    //**نظارت
                //    WorkRequestsrvEngineerToOthersTest.ClsQtaInputs ClsQtaInputsObs = new WorkRequestsrvEngineerToOthersTest.ClsQtaInputs();
                //    ClsQtaInputsObs.CI_Ability = 1;
                //    //ClsQtaInputsObs.Date = Utility.GetDateOfToday();
                //    ClsQtaInputsObs.Meter = MunObsCapacity;
                //    //ClsQtaInputsObs.Time = time;
                //    ClsQtaInputsObs.ChangeBaseDate = ObsDate;
                //    ListclsQtaInputs[0] = ClsQtaInputsObs;
                //    #endregion
                //}
                //IsrvEngineerToOthersClient.SaveQtaInfoCompleted += new EventHandler<WorkRequestsrvEngineerToOthersTest.SaveQtaInfoCompletedEventArgs>(GetGetResultEngineerToOthersClientTest);
                //IsrvEngineerToOthersClient.SaveQtaInfoAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), ListclsQtaInputs, 1, Year, (int)TSP.DataManager.TSSafarayanehWebServiceCallingRefrenceType.WorkRequest);

                #endregion
            }
            else
            {
                #region MainServer

                WorkRequestsrvEngineerToOthers.IsrvEngineerToOthersClient IsrvEngineerToOthersClient = new WorkRequestsrvEngineerToOthers.IsrvEngineerToOthersClient();
                if (SendMeInfo)
                {
                    int LastMfId = -2; int MasterMfMjParentId = -2; int ObsId = -2; ; int DesId = 0; int UrbanismId = 0;
                    Int16 ObsGrad = 0; Int16 DesGrad = 0; Int16 UrbanismGrad = 0;
                    TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
                    DataTable dtMember = MemberManager.SelectMemberInfoForWorkRequest(_MeId);
                    if (dtMember.Rows.Count != 1)
                        return;
                    MasterMfMjParentId = Convert.ToInt32(dtMember.Rows[0]["MasterMfMjParentId"]);
                    ObsId = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["ObsId"]) ? -2 : Convert.ToInt32(dtMember.Rows[0]["ObsId"]);
                    DesId = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["DesId"]) ? -2 : Convert.ToInt32(dtMember.Rows[0]["DesId"]);
                    UrbanismId = Utility.IsDBNullOrNullValue(dtMember.Rows[0]["UrbanismId"]) ? -2 : Convert.ToInt32(dtMember.Rows[0]["UrbanismId"]);

                    switch (ObsId)
                    {
                        case (int)TSP.DataManager.DocumentGrads.Grade1:
                            ObsGrad = 1;
                            break;
                        case (int)TSP.DataManager.DocumentGrads.Grade2:
                            ObsGrad = 2;
                            break;
                        case (int)TSP.DataManager.DocumentGrads.Grade3:
                            ObsGrad = 3;
                            break;
                    }
                    switch (DesId)
                    {
                        case (int)TSP.DataManager.DocumentGrads.Grade1:
                            ObsGrad = 1;
                            break;
                        case (int)TSP.DataManager.DocumentGrads.Grade2:
                            DesGrad = 2;
                            break;
                        case (int)TSP.DataManager.DocumentGrads.Grade3:
                            DesGrad = 3;
                            break;
                    }
                    switch (UrbanismId)
                    {
                        case (int)TSP.DataManager.DocumentGrads.Grade1:
                            UrbanismGrad = 1;
                            break;
                        case (int)TSP.DataManager.DocumentGrads.Grade2:
                            UrbanismGrad = 2;
                            break;
                        case (int)TSP.DataManager.DocumentGrads.Grade3:
                            UrbanismGrad = 3;
                            break;
                    }

                    WorkRequestsrvEngineerToOthers.ClsEngineer1 ClsEngineer1Info = new WorkRequestsrvEngineerToOthers.ClsEngineer1();
                    ClsEngineer1Info.Eng_Info = new WorkRequestsrvEngineerToOthers.Eng_Info();
                    ClsEngineer1Info.Eng_Info.EngName = dtMember.Rows[0]["FirstName"].ToString();
                    ClsEngineer1Info.Eng_Info.EngFamily = dtMember.Rows[0]["LastName"].ToString();
                    ClsEngineer1Info.Eng_Info.MunicipalityCode = dtMember.Rows[0]["MeNo"].ToString();// کد عضویت نظام مهندسی
                    ClsEngineer1Info.Eng_Info.FatherName = dtMember.Rows[0]["FatherName"].ToString();// نام پدر
                    ClsEngineer1Info.Eng_Info.BirthDate = dtMember.Rows[0]["BirhtDate"].ToString(); //تاریخ تولد
                    ClsEngineer1Info.Eng_Info.IdNo = dtMember.Rows[0]["IdNo"].ToString();//شماره شناسنامه
                    ClsEngineer1Info.Eng_Info.Tel = dtMember.Rows[0]["HomeTel"].ToString();// تلفن
                    ClsEngineer1Info.Eng_Info.Address = dtMember.Rows[0]["HomeAdr"].ToString();// آدرس
                    ClsEngineer1Info.Eng_Info.NationalCode = dtMember.Rows[0]["SSN"].ToString();// کد ملی
                    ClsEngineer1Info.Eng_Info.IdentityCode = _MeId.ToString();//کد عضویت
                    ClsEngineer1Info.Eng_Info.AddressWork = ""; //آدرس محل کار
                    ClsEngineer1Info.Eng_Info.Email = dtMember.Rows[0]["Email"].ToString();// آدرس ایمیل
                    ClsEngineer1Info.Eng_Info.BirthPlace = dtMember.Rows[0]["BirthPlace"].ToString();// محل تولد
                    ClsEngineer1Info.Eng_Info.MobileNo = dtMember.Rows[0]["MobileNo"].ToString(); //شماره همراه
                    ClsEngineer1Info.Eng_Info.PostalCode = "";//کد پستی
                    ClsEngineer1Info.Eng_Info.ArchitectureCode = "";// کد شهرداری
                    ClsEngineer1Info.Eng_Info.PostalCodeWork = ""; //کد پستی محل کار
                    switch (MasterMfMjParentId)
                    {
                        case 1:
                            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 100;
                            break;
                        case 2:
                            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 210;
                            break;
                        case 3:
                            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 300;
                            break;
                        case 4:
                            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 400;
                            break;
                        case 5:
                            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 500;
                            break;
                        case 6:
                            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 600;
                            break;
                        case 7:
                            ClsEngineer1Info.Eng_Info.ADPstudyFieldRel = 700;
                            break;
                    }
                    IsrvEngineerToOthersClient.SaveEng_InfoCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveEng_InfoCompletedEventArgs>(GetResultEngineerToOthers);
                    IsrvEngineerToOthersClient.SaveEng_InfoAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), ClsEngineer1Info);

                    #region اطلاعات پروانه
                    WorkRequestsrvEngineerToOthers.Eng_JobAgreement PEngjob = new WorkRequestsrvEngineerToOthers.Eng_JobAgreement();
                    PEngjob.JobAgreementExportDate = dtMember.Rows[0]["FileDate"].ToString();
                    //PEngjob.NIdEng = ClsEngineer1Info.NidEngineer;
                    PEngjob.NIdJobAgreement_tmp = 0;
                    PEngjob.CI_JobAgreementType = 1;
                    PEngjob.CI_JobAgreementBaseExport = 0;
                    PEngjob.CI_Region = 1;

                    IsrvEngineerToOthersClient.SaveEng_JobAgreementCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveEng_JobAgreementCompletedEventArgs>(GetResultJobAgreement);
                    IsrvEngineerToOthersClient.SaveEng_JobAgreementAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), PEngjob);
                    #endregion
                    #region اطلاعات جدول صلاحیت
                    int Len = 1;
                    if (DesGrad > 0 || UrbanismGrad > 0)
                        Len = 2;
                    WorkRequestsrvEngineerToOthers.Eng_Competence[] ListengCompetence = new WorkRequestsrvEngineerToOthers.Eng_Competence[Len];
                    if (ObsId > 0)
                    {

                        WorkRequestsrvEngineerToOthers.Eng_Competence engCompetenceObj = new WorkRequestsrvEngineerToOthers.Eng_Competence();
                        //engCompetenceObj.NIdEng = ClsEngineer1Info.NidEngineer;
                        engCompetenceObj.CI_Ability = 1;
                        engCompetenceObj.CI_Base = ObsGrad;
                        engCompetenceObj.IsActive = true;
                        ListengCompetence[0] = engCompetenceObj;

                    }
                    if (DesGrad > 0 || UrbanismGrad > 0)
                    {
                        WorkRequestsrvEngineerToOthers.Eng_Competence engCompetenceObj = new WorkRequestsrvEngineerToOthers.Eng_Competence();
                        // engCompetenceObj.NIdEng = ClsEngineer1Info.NidEngineer;
                        if (MasterMfMjParentId == (int)TSP.DataManager.MainMajors.Civil)
                            engCompetenceObj.CI_Ability = 4;
                        else
                            engCompetenceObj.CI_Ability = 2;

                        engCompetenceObj.CI_Base = DesGrad > 0 ? DesGrad : UrbanismGrad;
                        engCompetenceObj.IsActive = true;
                        ListengCompetence[1] = engCompetenceObj;
                    }
                    IsrvEngineerToOthersClient.SaveEng_CompetenceCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveEng_CompetenceCompletedEventArgs>(GetResultCompetence);
                    IsrvEngineerToOthersClient.SaveEng_Competence("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), ListengCompetence);

                    #endregion

                }

                #region Comment
                //int Count = 1;
                //if (!string.IsNullOrEmpty(ObsDate) && MunDesignCapacity != 0)
                //    Count = 2;
                //string time = DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
                //WorkRequestsrvEngineerToOthers.ClsQtaInputs[] ListclsQtaInputs = new WorkRequestsrvEngineerToOthers.ClsQtaInputs[Count];
                ////CI_Ability: در صورتی که صلاحیت طراحی باشد عدد 2 و صلاحیت نظارت عدد 1 ، صلاحیت مجری عدد 3 ، صلاحیت محاسبه عدد 4 
                //if (!string.IsNullOrEmpty(ObsDate) && MunDesignCapacity != 0)
                //{
                //    #region نظارت و طراحی
                //    WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsde = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                //    if (MjParentId == (int)TSP.DataManager.MainMajors.Civil)
                //        ClsQtaInputsde.CI_Ability = 4;
                //    else
                //        ClsQtaInputsde.CI_Ability = 2;
                //    ClsQtaInputsde.Date = Utility.GetDateOfToday();
                //    if (MjParentId == (int)TSP.DataManager.MainMajors.Urbanism)
                //        ClsQtaInputsde.Meter = MunUrbenismCapacity;
                //    else
                //        ClsQtaInputsde.Meter = MunDesignCapacity;
                //    ClsQtaInputsde.Time = time;
                //    ClsQtaInputsde.ChangeBaseDate = DesignDate;
                //    ListclsQtaInputs[0] = ClsQtaInputsde;
                //    //**نظارت
                //    WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsOb = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                //    ClsQtaInputsOb.CI_Ability = 1;
                //    //ClsQtaInputsOb.Date = Utility.GetDateOfToday();
                //    ClsQtaInputsOb.Meter = MunObsCapacity;
                //    //ClsQtaInputsOb.Time = time;
                //    ClsQtaInputsOb.ChangeBaseDate = ObsDate;
                //    ListclsQtaInputs[1] = ClsQtaInputsOb;
                //    #endregion
                //}
                //else if ((MunDesignCapacity != 0 || MunUrbenismCapacity != 0) && string.IsNullOrEmpty(ObsDate))
                //{
                //    #region فقظ طراحی یا شهرسازی
                //    WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsDes = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                //    if (MjParentId == (int)TSP.DataManager.MainMajors.Civil)
                //        ClsQtaInputsDes.CI_Ability = 4;
                //    else
                //        ClsQtaInputsDes.CI_Ability = 2;
                //    ClsQtaInputsDes.Date = Utility.GetDateOfToday();
                //    if (MjParentId == (int)TSP.DataManager.MainMajors.Urbanism)
                //        ClsQtaInputsDes.Meter = MunUrbenismCapacity;
                //    else
                //        ClsQtaInputsDes.Meter = MunDesignCapacity;
                //    ClsQtaInputsDes.Time = time;
                //    ClsQtaInputsDes.ChangeBaseDate = DesignDate;
                //    ListclsQtaInputs[0] = ClsQtaInputsDes;
                //    #endregion
                //}
                //else if (!string.IsNullOrEmpty(ObsDate))
                //{
                //    #region فقظ نظارت
                //    //**نظارت
                //    WorkRequestsrvEngineerToOthers.ClsQtaInputs ClsQtaInputsObs = new WorkRequestsrvEngineerToOthers.ClsQtaInputs();
                //    ClsQtaInputsObs.CI_Ability = 1;
                //    //ClsQtaInputsObs.Date = Utility.GetDateOfToday();
                //    ClsQtaInputsObs.Meter = MunObsCapacity;
                //    //ClsQtaInputsObs.Time = time;
                //    ClsQtaInputsObs.ChangeBaseDate = ObsDate;
                //    ListclsQtaInputs[0] = ClsQtaInputsObs;
                //    #endregion
                //}

                //IsrvEngineerToOthersClient.SaveQtaInfoCompleted += new EventHandler<WorkRequestsrvEngineerToOthers.SaveQtaInfoCompletedEventArgs>(GetGetResultEngineerToOthersClient);
                //IsrvEngineerToOthersClient.SaveQtaInfoAsync("gdyFlNN847tyCqSLnUkm5w==", _MeId.ToString(), ListclsQtaInputs, 1, Year, (int)TSP.DataManager.TSSafarayanehWebServiceCallingRefrenceType.WorkRequest);

                #endregion
                #endregion
            }
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
            ShowMessage(ex.ToString());
        }
    }


    private void GetResultEngineerToOthersTest(object sender, SaveEng_InfoCompletedEventArgs e)
    {
        WorkRequestsrvEngineerToOthersTest.ClsEngineer1 ResultClsEngineer1 = e.Result;
        if (ResultClsEngineer1.ErrorResult.BizErrors[0] != null)
        {
            //ok;
        }
        else
        {
            ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
        }
    }

    private void GetGetResultEngineerToOthersClientTest(object sender, SaveQtaInfoCompletedEventArgs e)
    {
        WorkRequestsrvEngineerToOthersTest.ClsErrorResult ErrorResult = e.Result;
        if (ErrorResult.BizErrors.Length == 0)
        {
            //ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.ارسال اطلاعات به شهرداری انجام شد ");
            //ok;
        }
        else
        {
            ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
        }

    }

    private void GetGetResultEngineerToOthersClient(object sender, WorkRequestsrvEngineerToOthers.SaveQtaInfoCompletedEventArgs e)
    {
        try
        {
            //    TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager TSEsupWebserviceCallingLogManager = new TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager();
            //    DataRow dr = TSEsupWebserviceCallingLogManager.NewRow();
            //    dr["MeId"] = txtMeId.Text;
            //    dr["ModifiedDate"] = DateTime.Now;
            //    dr["Type"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogType.SaveQtaInfo;
            WorkRequestsrvEngineerToOthers.ClsErrorResult ErrorResult = e.Result;
            if (ErrorResult.BizErrors.Length == 0)
            {
                //ok;     
                //dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Succes;
            }
            else
            {
                //dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
                txtListConflictPercentOfCapacityMeId.Text += "0";
            }
            //TSEsupWebserviceCallingLogManager.AddRow(dr);
            //TSEsupWebserviceCallingLogManager.Save();
        }
        catch (Exception ex)
        {
            Utility.SaveWebsiteError(ex);
        }
    }

    private void GetResultEngineerToOthers(object sender, WorkRequestsrvEngineerToOthers.SaveEng_InfoCompletedEventArgs e)
    {
        //TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager TSEsupWebserviceCallingLogManager = new TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager();
        //DataRow dr = TSEsupWebserviceCallingLogManager.NewRow();
        try
        {
            //dr["MeId"] = txtMeId.Text;
            //dr["ModifiedDate"] = DateTime.Now;
            //dr["Type"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogType.SaveEngInfo;
            WorkRequestsrvEngineerToOthers.ClsEngineer1 ResultClsEngineer1 = e.Result;
            if (ResultClsEngineer1.ErrorResult.BizErrors[0] != null)
            {
                //ok;
                // dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Succes;
            }
            else
            {
                //   dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
            }
        }
        catch (Exception ex)
        {
            //dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
            Utility.SaveWebsiteError(ex);
            //dr["Message"] = e.Error.InnerException.Message;
        }
        //TSEsupWebserviceCallingLogManager.AddRow(dr);
        //TSEsupWebserviceCallingLogManager.Save();
    }


    private void GetResultJobAgreement(object sender, WorkRequestsrvEngineerToOthers.SaveEng_JobAgreementCompletedEventArgs e)
    {
        //TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager TSEsupWebserviceCallingLogManager = new TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager();
        //DataRow dr = TSEsupWebserviceCallingLogManager.NewRow();
        try
        {
            //dr["MeId"] = txtMeId.Text;
            //dr["ModifiedDate"] = DateTime.Now;
            //dr["Type"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogType.SaveJobAgreeMent;
            WorkRequestsrvEngineerToOthers.Eng_JobAgreement ResultJobAgreement = e.Result;
            if (ResultJobAgreement != null)
            {
                //ok;
                //dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Succes;
            }
            else
            {
                //dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
            }
        }
        catch (Exception ex)
        {
            //dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
            Utility.SaveWebsiteError(ex);
            //dr["Message"] = e.Error.InnerException.Message;
        }
        //TSEsupWebserviceCallingLogManager.AddRow(dr);
        //TSEsupWebserviceCallingLogManager.Save();
    }

    private void GetResultCompetence(object sender, WorkRequestsrvEngineerToOthers.SaveEng_CompetenceCompletedEventArgs e)
    {
        //TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager TSEsupWebserviceCallingLogManager = new TSP.DataManager.TechnicalServices.TSEsupWebserviceCallingLogManager();
        //DataRow dr = TSEsupWebserviceCallingLogManager.NewRow();
        try
        {
            //dr["MeId"] = txtMeId.Text;
            //dr["ModifiedDate"] = DateTime.Now;
            //dr["Type"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogType.SaveCompetence;
            WorkRequestsrvEngineerToOthers.Eng_Competence[] ResultCompetence = e.Result;
            if (ResultCompetence.Length > 0)
            {
                //ok;
                //dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Succes;
            }
            else
            {
                //dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
                ShowMessage("آماده بکاری شما در سازمان نظام مهندسی ثبت شد.خطا در ارسال اطلاعات به شهرداری ایجاد شده است");
            }
        }
        catch (Exception ex)
        {
            //dr["Status"] = (int)TSP.DataManager.TSEsupWebserviceCallingLogStatus.Error;
            Utility.SaveWebsiteError(ex);
            //dr["Message"] = e.Error.InnerException.Message;
        }
        //TSEsupWebserviceCallingLogManager.AddRow(dr);
        //TSEsupWebserviceCallingLogManager.Save();
    }

}